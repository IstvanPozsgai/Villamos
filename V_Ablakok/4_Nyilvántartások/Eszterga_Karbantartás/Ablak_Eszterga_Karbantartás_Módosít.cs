using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.V_Ablakok._4_Nyilvántartások.Eszterga_Karbantartás;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;
using Funkció = Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga.Eszterga_Funkció;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga
{
    public partial class Ablak_Eszterga_Karbantartás_Módosít : Form
    {
        #region Osztalyszintű elemek
        public event Event_Kidobó Eszterga_Változás;
        readonly DateTime MaiDatum = DateTime.Today;
        readonly bool Baross = Program.PostásTelephely.Trim() == "Angyalföld";
        private bool frissul = false;
        readonly DataTable AdatTábla = new DataTable();
        readonly DataTable AdatTáblaUtólag = new DataTable();
        #endregion

        #region Listák
        private List<Adat_Eszterga_Műveletek> AdatokMűvelet;
        private List<Adat_Eszterga_Üzemóra> AdatokÜzemóra;
        private List<Adat_Eszterga_Műveletek_Napló> AdatokNapló;
        #endregion

        #region Kezelők
        private Kezelő_Eszterga_Műveletek KézMűveletek = new Kezelő_Eszterga_Műveletek();
        private Kezelő_Eszterga_Műveletek_Napló KézNapló = new Kezelő_Eszterga_Műveletek_Napló();
        #endregion

        #region Alap
        public Ablak_Eszterga_Karbantartás_Módosít()
        {
            InitializeComponent();
            TáblaListázásMűvelet();
            TáblaNaplóListázás();
            TáblaListázásMűveletUtólag();
            TxtBxId.Enabled = false;
            CmbxEgység.DataSource = Enum.GetValues(typeof(EsztergaEgyseg));
        }
        private void Ablak_Eszterga_Karbantartás_Módosít_Load(object sender, EventArgs e)
        {
            Eszterga_Változás?.Invoke();
            TáblaMűvelet.ClearSelection();
            Jogosultságkiosztás();
            Btn_Csere.Visible = false;
            Btn_Sorrend.Visible = false;
            TáblaMűvelet.CellFormatting += TáblaMűvelet_CellFormatting;
            EgységBeállítása();
        }
        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem = 160;
                Btn_Módosít.Visible = Baross;
                Btn_Sorrend.Visible = Baross;
                Btn_Törlés.Visible = Baross;
                Btn_ÚjFelvétel.Visible = Baross;
                Btn_Csere.Visible = Baross;

                // módosítás 1 
                //Ablak_Eszterga_Karbantartás_Segéd oldal használja az 1. módosításokat

                // módosítás 2
                //Ablak_Eszterga_Karbantartás oldal használja a 2. módosításokat

                // módosítás 3 
                Btn_Módosít.Enabled = MyF.Vanjoga(melyikelem, 3);
                Btn_Sorrend.Enabled = MyF.Vanjoga(melyikelem, 3);
                Btn_Törlés.Enabled = MyF.Vanjoga(melyikelem, 3);
                Btn_ÚjFelvétel.Enabled = MyF.Vanjoga(melyikelem, 3);
                Btn_Csere.Enabled = MyF.Vanjoga(melyikelem, 3);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Egyseg
        public enum EsztergaEgyseg
        {
            Dátum = 1,
            Üzemóra = 2,
            Bekövetkezés = 3
        }
        private void EgysegEllenőrzés(string Egyseg)
        {
            try
            {
                TxtBxMennyiÓra.Enabled = true;
                TxtBxMennyiNap.Enabled = true;
                TxtBxUtolsóÜzemóraÁllás.Enabled = true;
                DtmPckrUtolsóDátum.Enabled = true;

                switch (Egyseg)
                {
                    case "Dátum":
                        TxtBxMennyiÓra.Enabled = false;
                        TxtBxMennyiÓra.Text = "0";
                        TxtBxUtolsóÜzemóraÁllás.Enabled = false;

                        Adat_Eszterga_Üzemóra uzemoraRekordDátum = KeresÜzemóra(0, DtmPckrUtolsóDátum.Value, EsztergaEgyseg.Dátum);
                        TxtBxUtolsóÜzemóraÁllás.Text = uzemoraRekordDátum != null ? uzemoraRekordDátum.Üzemóra.ToStrTrim() : "0";
                        break;

                    case "Üzemóra":
                        TxtBxMennyiNap.Enabled = false;
                        TxtBxMennyiNap.Text = "0";
                        DtmPckrUtolsóDátum.Enabled = false;

                        if (long.TryParse(TxtBxUtolsóÜzemóraÁllás.Text, out long uzemora))
                        {
                            Adat_Eszterga_Üzemóra uzemoraRekordÜzemóra = KeresÜzemóra(uzemora, DateTime.MinValue, EsztergaEgyseg.Üzemóra);
                            DtmPckrUtolsóDátum.Value = uzemoraRekordÜzemóra?.Dátum ?? new DateTime(1900, 1, 1);
                        }
                        else
                            DtmPckrUtolsóDátum.Value = new DateTime(1900, 1, 1);
                        break;

                    case "Bekövetkezés":
                        TxtBxMennyiÓra.Enabled = true;
                        TxtBxMennyiNap.Enabled = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CmbxEgység_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kivalasztottEgyseg = CmbxEgység.SelectedItem.ToStrTrim();
            EgysegEllenőrzés(kivalasztottEgyseg);
        }
        private void EgységBeállítása()
        {
            try
            {
                CmbxEgység.SelectedItem = EsztergaEgyseg.Bekövetkezés;
                List<Adat_Eszterga_Műveletek> AdatokMűvelet = Funkció.Eszterga_KarbantartasFeltölt();
                int KovetkezoID = AdatokMűvelet.Any() ? AdatokMűvelet.Max(a => a.ID) + 1 : 1;
                TxtBxId.Text = KovetkezoID.ToStrTrim();
                EgysegEllenőrzés(EsztergaEgyseg.Bekövetkezés.ToStrTrim());
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Táblák Listázása
        private void TáblaListázásMűvelet()
        {
            try
            {
                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Sorszám");
                AdatTábla.Columns.Add("Művelet");
                AdatTábla.Columns.Add("Egység");
                AdatTábla.Columns.Add("Nap");
                AdatTábla.Columns.Add("Óra");
                AdatTábla.Columns.Add("Státusz");
                AdatTábla.Columns.Add("Utolsó Dátum");
                AdatTábla.Columns.Add("Utolsó Üzemóra");

                AdatokMűvelet = Funkció.Eszterga_KarbantartasFeltölt();
                AdatokÜzemóra = Funkció.Eszterga_ÜzemóraFeltölt();
                AdatTábla.Clear();

                foreach (Adat_Eszterga_Műveletek rekord in AdatokMűvelet)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Sorszám"] = rekord.ID;
                    Soradat["Művelet"] = rekord.Művelet;
                    Soradat["Egység"] = Enum.GetName(typeof(EsztergaEgyseg), rekord.Egység);
                    Soradat["Nap"] = rekord.Mennyi_Dátum;
                    Soradat["Óra"] = rekord.Mennyi_Óra;
                    Soradat["Státusz"] = rekord.Státus ? "Törölt" : "Aktív";
                    Soradat["Utolsó Dátum"] = rekord.Utolsó_Dátum.ToShortDateString();
                    Adat_Eszterga_Üzemóra uzemoraRekord = AdatokÜzemóra
                        .FirstOrDefault(a => a.Dátum.Date == rekord.Utolsó_Dátum.Date && a.Státus == false);

                    if (uzemoraRekord != null)
                        Soradat["Utolsó Üzemóra"] = uzemoraRekord.Üzemóra;
                    else
                        Soradat["Utolsó Üzemóra"] = rekord.Utolsó_Üzemóra_Állás;

                    AdatTábla.Rows.Add(Soradat);
                }

                TáblaMűvelet.DataSource = AdatTábla;
                OszlopSzélességMűvelet();
                TáblaMűvelet.Visible = true;
                TáblaMűvelet.ClearSelection();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OszlopSzélességMűvelet()
        {
            TáblaMűvelet.Columns["Sorszám"].Width = 100;
            TáblaMűvelet.Columns["Művelet"].Width = 1155;
            TáblaMűvelet.Columns["Egység"].Width = 110;
            TáblaMűvelet.Columns["Nap"].Width = 60;
            TáblaMűvelet.Columns["Óra"].Width = 60;
            TáblaMűvelet.Columns["Státusz"].Width = 85;
            TáblaMűvelet.Columns["Utolsó Dátum"].Width = 120;
            TáblaMűvelet.Columns["Utolsó Üzemóra"].Width = 160;
        }
        private void TáblaNaplóListázás()
        {
            TáblaNapló.DataSource = null;
            TáblaNapló.Rows.Clear();
            TáblaNapló.Columns.Clear();
            DataTable AdatTábla = new DataTable();
            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Művelet Sorszáma");
            AdatTábla.Columns.Add("Művelet");
            AdatTábla.Columns.Add("Utolsó Dátum");
            AdatTábla.Columns.Add("Utolsó Üzemóra");
            AdatTábla.Columns.Add("Megjegyzés");
            AdatTábla.Columns.Add("Rögzítő");
            AdatTábla.Columns.Add("Rögzítés Dátuma");

            AdatokNapló = Funkció.Eszterga_KarbantartasNaplóFeltölt();
            List<DataRow> RendezettSorok = new List<DataRow>();
            foreach (Adat_Eszterga_Műveletek_Napló rekord in AdatokNapló)
            {
                DataRow Soradat = AdatTábla.NewRow();

                Soradat["Művelet Sorszáma"] = rekord.ID;
                Soradat["Művelet"] = rekord.Művelet;
                Soradat["Utolsó Dátum"] = rekord.Utolsó_Dátum.ToShortDateString();
                Soradat["Utolsó Üzemóra"] = rekord.Utolsó_Üzemóra_Állás;
                Soradat["Megjegyzés"] = rekord.Megjegyzés;
                Soradat["Rögzítő"] = rekord.Rögzítő;
                Soradat["Rögzítés Dátuma"] = rekord.Rögzítés_Dátuma.ToShortDateString();

                RendezettSorok.Add(Soradat);
            }
            IEnumerable<DataRow> RendezettAdatok = RendezettSorok
                .OrderBy(sor => DateTime.Parse(sor["Utolsó Dátum"].ToStrTrim()))
                .ThenBy(sor => int.Parse(sor["Művelet Sorszáma"].ToStrTrim()));

            foreach (DataRow sor in RendezettAdatok)
                AdatTábla.Rows.Add(sor);

            TáblaNapló.DataSource = AdatTábla;

            TáblaNapló.Columns["Művelet Sorszáma"].Width = 110;
            TáblaNapló.Columns["Művelet"].Width = 550;
            TáblaNapló.Columns["Utolsó Dátum"].Width = 105;
            TáblaNapló.Columns["Utolsó Üzemóra"].Width = 120;
            TáblaNapló.Columns["Megjegyzés"].Width = 221;
            TáblaNapló.Columns["Rögzítő"].Width = 150;
            TáblaNapló.Columns["Rögzítés Dátuma"].Width = 115;
            for (int i = 0; i < 7; i++)
                TáblaNapló.Columns[i].ReadOnly = true;
            TáblaNapló.Visible = true;
            TáblaNapló.ClearSelection();
        }
        private void TáblaListázásMűveletUtólag()
        {
            try
            {
                AdatTáblaUtólag.Columns.Clear();
                AdatTáblaUtólag.Columns.Add("Sorszám");
                AdatTáblaUtólag.Columns.Add("Művelet");
                AdatTáblaUtólag.Columns.Add("Státusz");

                AdatokMűvelet = Funkció.Eszterga_KarbantartasFeltölt();
                AdatTáblaUtólag.Clear();

                foreach (Adat_Eszterga_Műveletek rekord in AdatokMűvelet)
                {
                    DataRow Soradat = AdatTáblaUtólag.NewRow();

                    Soradat["Sorszám"] = rekord.ID;
                    Soradat["Művelet"] = rekord.Művelet;
                    Soradat["Státusz"] = rekord.Státus ? "Törölt" : "Aktív";
                    AdatTáblaUtólag.Rows.Add(Soradat);
                }

                TáblaUtólagMűvelet.DataSource = AdatTáblaUtólag;
                OszlopSzélességMűveletUtólag();
                TáblaUtólagMűvelet.Visible = true;
                TáblaUtólagMűvelet.ClearSelection();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void OszlopSzélességMűveletUtólag()
        {
            TáblaUtólagMűvelet.Columns["Sorszám"].Width = 100;
            TáblaUtólagMűvelet.Columns["Művelet"].Width = 900;
            TáblaUtólagMűvelet.Columns["Státusz"].Width = 100;
        }
        #endregion

        #region Metodusok
        private void TörlésEllenőrzés()
        {
            Btn_Törlés.Visible = !ChckBxStátus.Checked;
        }
        private int TxtBxEllenőrzés(bool újRekord = false)
        {
            int hiba = 0;
            List<string> Hibák = new List<string>();

            bool VanE = AdatokMűvelet.Any(a => a.ID == TxtBxId.Text.ToÉrt_Int());

            if (újRekord && VanE)
            {
                Hibák.Add("Az azonosító már létezik az adatbázisban.");
                hiba++;
            }

            if (!újRekord && TáblaMűvelet.SelectedRows.Count < 1)
            {
                hiba = 90;
                Hibák.Add("Nincs kiválasztott művelet.");
                return hiba;
            }

            if (string.IsNullOrEmpty(TxtBxId.Text))
            {
                Hibák.Add("Töltse ki az Azonosító mezőt.");
                hiba++;
            }

            if (!újRekord && !VanE)
            {
                Hibák.Add("Az azonosító nem található az adatbázisban.");
                hiba++;
            }

            if (true)
            {
                string Egyseg = CmbxEgység.SelectedItem?.ToStrTrim();
                bool Nap = int.TryParse(TxtBxMennyiNap.Text, out int MennyiNap);
                bool Óra = int.TryParse(TxtBxMennyiÓra.Text, out int MennyiÓra);

                if (Egyseg == "Dátum" && (!Nap || MennyiNap <= 0))
                {
                    Hibák.Add("A Nap mezőben csak pozitív egész szám szerepelhet.");
                    hiba++;
                }
                else if (Egyseg == "Üzemóra" && (!Óra || MennyiÓra <= 0))
                {
                    Hibák.Add("Az Óra mezőben csak pozitív egész szám szerepelhet.");
                    hiba++;
                }
                else if (Egyseg == "Bekövetkezés" && (!Nap || !Óra || MennyiNap <= 0 || MennyiÓra <= 0))
                {
                    Hibák.Add("A Nap és Óra mezőkben csak pozitív egész szám szerepelhetnek.");
                    hiba++;
                }

                if (string.IsNullOrEmpty(TxtBxMűvelet.Text))
                {
                    Hibák.Add("Töltse ki a Művelet mezőt.");
                    hiba++;
                }

                if (Egyseg == "Üzemóra" || Egyseg == "Bekövetkezés")
                {
                    AdatokÜzemóra = Funkció.Eszterga_ÜzemóraFeltölt();

                    if (string.IsNullOrEmpty(TxtBxUtolsóÜzemóraÁllás.Text) || TxtBxUtolsóÜzemóraÁllás.Text == "0" ||
                        !long.TryParse(TxtBxUtolsóÜzemóraÁllás.Text, out _))
                    {
                        Hibák.Add("Az Utolsó Üzemóra Állás mező csak pozitív egész számot tartalmazhat.");
                        hiba++;
                    }
                    else
                    {
                        long aktualisUzemora = AdatokÜzemóra.Count > 0 ? AdatokÜzemóra.Max(u => u.Üzemóra) : 0;
                        if (long.Parse(TxtBxUtolsóÜzemóraÁllás.Text) > aktualisUzemora)
                        {
                            Hibák.Add("Az Utolsó Üzemóra Állás nem lehet nagyobb, mint az aktuális Üzemóra érték.");
                            hiba++;
                        }
                    }
                }

                //if (Új_ablak_EsztergaÜzemóra.DtmPckrDátum.Value.Date > MaiDatum.Date)
                //{
                //    Hibák.Add("A kiválasztott dátum nem lehet későbbi, mint a mai dátum.");
                //    hiba++;
                //}
            }

            if (Hibák.Count > 0)
            {
                string HibaÜzenet = Hibák.Count == 1
                    ? Hibák[0]
                    : $"Ellenőrizze a hibákat és javítsa ki a megfelelő adatokra:\n{string.Join("\n", Hibák)}";

                MessageBox.Show(HibaÜzenet, "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return hiba;
        }
        private void Rendezés()
        {
            List<Adat_Eszterga_Műveletek> rekordok = Funkció.Eszterga_KarbantartasFeltölt();

            rekordok.Sort((a, b) => a.ID.CompareTo(b.ID));

            int KovetkezoID = 1;
            int UtolsoID = 1;

            for (int i = 0; i < rekordok.Count; i++)
            {
                Adat_Eszterga_Műveletek rekord = rekordok[i];

                if (rekord.ID != KovetkezoID)
                {
                    UtolsoID = rekord.ID;
                    Adat_Eszterga_Műveletek ADAT = new Adat_Eszterga_Műveletek(rekord.ID);
                    KézMűveletek.Rendezés(ADAT, KovetkezoID);
                    rekord.ID = KovetkezoID;
                }

                KovetkezoID++;
            }
        }
        private List<Adat_Eszterga_Műveletek> SorKivalasztas()
        {
            List<Adat_Eszterga_Műveletek> rekordok = new List<Adat_Eszterga_Műveletek>();

            foreach (DataGridViewRow sor in TáblaMűvelet.SelectedRows)
            {
                int id = sor.Cells[0].Value.ToÉrt_Int();

                AdatokMűvelet = Funkció.Eszterga_KarbantartasFeltölt();

                Adat_Eszterga_Műveletek rekord = (from a in AdatokMűvelet
                                                  where a.ID == id
                                                  select a).FirstOrDefault();

                if (rekord != null)
                    rekordok.Add(rekord);
                else
                    throw new HibásBevittAdat("A kijelölt sorok nem találhatóak az adatbázisban.");
            }

            return rekordok;
        }
        private bool ModositasEll(bool Muvelet = false)
        {
            if (Muvelet)
            {
                AdatokMűvelet = Funkció.Eszterga_KarbantartasFeltölt();
                Adat_Eszterga_Műveletek rekord = AdatokMűvelet.FirstOrDefault(a => a.ID == int.Parse(TxtBxId.Text));
                Enum.TryParse(CmbxEgység.SelectedItem.ToStrTrim(), out EsztergaEgyseg Egyseg);
                if (rekord != null &&
                    rekord.Művelet.Trim() == TxtBxMűvelet.Text.Trim() &&
                    rekord.Egység == (int)Egyseg &&
                    rekord.Státus == ChckBxStátus.Checked &&
                    rekord.Mennyi_Dátum == int.Parse(TxtBxMennyiNap.Text) &&
                    rekord.Mennyi_Óra == int.Parse(TxtBxMennyiÓra.Text) &&
                    rekord.Utolsó_Dátum == DtmPckrUtolsóDátum.Value &&
                    rekord.Utolsó_Üzemóra_Állás == int.Parse(TxtBxUtolsóÜzemóraÁllás.Text))
                    return false;
            }
            else
            {
                AdatokÜzemóra = Funkció.Eszterga_ÜzemóraFeltölt();
                Adat_Eszterga_Üzemóra rekord = AdatokÜzemóra
                    .FirstOrDefault(a => a.Dátum == Új_ablak_EsztergaÜzemóra.DtmPckrDátum.Value.Date);

                if (rekord == null)
                {
                    MessageBox.Show("Nem található rekord a megadott dátummal.", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (rekord.Üzemóra == int.Parse(Új_ablak_EsztergaÜzemóra.TxtBxÜzem.Text) &&
                    rekord.Státus == Új_ablak_EsztergaÜzemóra.ChckBxStátus.Checked)
                    return false;
            }

            return true;
        }
        private void TöröltTáblaSzínezés(Zuby.ADGV.AdvancedDataGridView tábla, DataGridViewCellFormattingEventArgs e, string státuszOszlop)
        {
            if (tábla.Columns[e.ColumnIndex].Name == státuszOszlop && e.Value is string státusz)
            {
                DataGridViewRow sor = tábla.Rows[e.RowIndex];

                if (státusz == "Törölt")
                {
                    sor.DefaultCellStyle.BackColor = Color.IndianRed;
                    sor.DefaultCellStyle.ForeColor = Color.Black;
                    sor.DefaultCellStyle.Font = new Font(tábla.DefaultCellStyle.Font, FontStyle.Strikeout);
                }
                else
                {
                    sor.DefaultCellStyle.BackColor = Color.White;
                    sor.DefaultCellStyle.ForeColor = Color.Black;
                    sor.DefaultCellStyle.Font = new Font(tábla.DefaultCellStyle.Font, FontStyle.Regular);
                }
            }
        }
        private Adat_Eszterga_Üzemóra KeresÜzemóra(long uzemora, DateTime datum, EsztergaEgyseg egyseg)
        {
            if (egyseg == EsztergaEgyseg.Bekövetkezés)
                return null;
            if (egyseg == EsztergaEgyseg.Üzemóra)
                return AdatokÜzemóra.FirstOrDefault(u => u.Üzemóra == uzemora && !u.Státus);
            if (egyseg == EsztergaEgyseg.Dátum)
                return AdatokÜzemóra.FirstOrDefault(u => u.Dátum.Date == datum.Date && !u.Státus);

            return null;
        }
        #endregion

        #region Gombok,Muveletek
        private void Btn_Módosít_Click(object sender, EventArgs e)
        {
            try
            {
                bool ÚjRekord = string.IsNullOrEmpty(TxtBxId.Text) || !AdatokMűvelet.Any(a => a.ID == TxtBxId.Text.ToÉrt_Int());
                int hiba = TxtBxEllenőrzés(ÚjRekord);
                if (hiba == 90)
                    MessageBox.Show("Nincsen kiválasztva egy sor sem.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (hiba >= 1)
                    return;
                bool Valtozott = ModositasEll(true);

                if (!Valtozott)
                {
                    MessageBox.Show("Nem történt változás.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                DateTime UjDatum = DtmPckrUtolsóDátum.Value.Date;
                long UjUzemora = TxtBxUtolsóÜzemóraÁllás.Text.ToÉrt_Long();

                Adat_Eszterga_Műveletek rekord = AdatokMűvelet
                    .FirstOrDefault(a => a.ID == TxtBxId.Text.ToÉrt_Int());

                if (rekord != null)
                {
                    Adat_Eszterga_Műveletek ADAT = new Adat_Eszterga_Műveletek(TxtBxId.Text.ToÉrt_Int(),
                                                                                TxtBxMűvelet.Text.ToStrTrim(),
                                                                                (int)CmbxEgység.SelectedItem,
                                                                                TxtBxMennyiNap.Text.ToÉrt_Int(),
                                                                                TxtBxMennyiÓra.Text.ToÉrt_Int(),
                                                                                ChckBxStátus.Checked.ToÉrt_Bool(),
                                                                                UjDatum,
                                                                                UjUzemora);
                    KézMűveletek.MeglévőMűvelet_Módosítás(ADAT);
                }
                else
                {

                    Adat_Eszterga_Műveletek ADAT = new Adat_Eszterga_Műveletek(0,
                                                                               TxtBxMűvelet.Text.ToStrTrim(),
                                                                               (int)CmbxEgység.SelectedItem,
                                                                               TxtBxMennyiNap.Text.ToÉrt_Int(),
                                                                               TxtBxMennyiÓra.Text.ToÉrt_Int(),
                                                                               (ChckBxStátus.Checked ? "True" : "False").ToÉrt_Bool(),
                                                                               UjDatum,
                                                                               UjUzemora);
                    KézMűveletek.Rögzítés(ADAT);
                }

                Eszterga_Változás?.Invoke();
                TáblaListázásMűvelet();
                TörlésEllenőrzés();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Rögzítve.", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Btn_Törlés_Click(object sender, EventArgs e)
        {
            try
            {
                int hiba = TxtBxEllenőrzés();
                if (hiba >= 90)
                    throw new HibásBevittAdat("Nincsen kiválasztva egy sor sem.");

                if (hiba >= 1)
                    return;

                foreach (DataGridViewRow row in TáblaMűvelet.SelectedRows)
                {
                    bool Torolt = (row.Cells[5].Value.ToStrTrim() == "Törölt").ToÉrt_Bool();
                    if (Torolt)
                        throw new HibásBevittAdat("Csak olyan sorokat lehet törölni, amik nincsenek törölve.");
                }

                AdatokMűvelet = Funkció.Eszterga_KarbantartasFeltölt();

                List<int> rekordok = new List<int>();
                foreach (DataGridViewRow row in TáblaMűvelet.SelectedRows)
                    rekordok.Add(row.Cells[0].Value.ToÉrt_Int());

                foreach (int Id in rekordok)
                {
                    Adat_Eszterga_Műveletek rekord = AdatokMűvelet.FirstOrDefault(a => a.ID == Id);
                    if (rekord != null)
                    {
                        Adat_Eszterga_Műveletek ADAT = new Adat_Eszterga_Műveletek(Id);
                        KézMűveletek.Törlés(ADAT, true);
                    }
                }
                Eszterga_Változás?.Invoke();
                TáblaListázásMűvelet();
                Btn_Törlés.Visible = false;
                MessageBox.Show("Az adatok törlése megtörtént.", "Törölve.", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Btn_ÚjFelvétel_Click(object sender, EventArgs e)
        {
            try
            {
                Btn_Törlés.Visible = false;
                List<Adat_Eszterga_Műveletek> AdatokMűvelet = Funkció.Eszterga_KarbantartasFeltölt();
                TxtBxId.Text = (AdatokMűvelet.Any() ? AdatokMűvelet.Max(a => a.ID) + 1 : 1).ToStrTrim();
                TxtBxMűvelet.Text = "";
                CmbxEgység.SelectedItem = EsztergaEgyseg.Bekövetkezés;
                TxtBxMennyiNap.Text = "0";
                TxtBxMennyiÓra.Text = "0";
                ChckBxStátus.Checked = false;
                DtmPckrUtolsóDátum.Value = MaiDatum;
                Adat_Eszterga_Üzemóra legutolsóÜzemóra = (from a in AdatokÜzemóra
                                                          where !a.Státus
                                                          orderby a.Dátum descending
                                                          select a).FirstOrDefault();

                TxtBxUtolsóÜzemóraÁllás.Text = legutolsóÜzemóra != null ? legutolsóÜzemóra.Üzemóra.ToStrTrim() : "0";
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Btn_Csere_Click(object sender, EventArgs e)
        {
            try
            {
                if (TáblaMűvelet.SelectedRows.Count != 2)
                    throw new HibásBevittAdat("A cseréhez 2 sor kijelölésére van szükség");

                List<Adat_Eszterga_Műveletek> rekordok = SorKivalasztas();

                Adat_Eszterga_Műveletek rekord1 = rekordok[0];
                Adat_Eszterga_Műveletek rekord2 = rekordok[1];

                KézMűveletek.MűveletCsere(rekord1, rekord2);
                Rendezés();
                TáblaListázásMűvelet();
                MessageBox.Show("A sorok sorszámai sikeresen kicserélve.", "Sikeres csere", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Btn_Sorrend_Click(object sender, EventArgs e)
        {
            try
            {
                if (TáblaMűvelet.SelectedRows.Count != 2)
                    throw new HibásBevittAdat("A sorrend módosításához 2 sor kijelölésére van szükség");

                List<Adat_Eszterga_Műveletek> rekordok = SorKivalasztas();

                Adat_Eszterga_Műveletek elso = rekordok[1];
                Adat_Eszterga_Műveletek masodik = rekordok[0];
                int ElsoID = elso.ID;
                int MasodikID = masodik.ID;

                KézMűveletek.MűveletSorrend(ElsoID, MasodikID);
                Rendezés();
                TáblaListázásMűvelet();
                MessageBox.Show("A sorrend módosítása sikeresen megtörtént.", "Sikeres módosítás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Btn_Excel_Click(object sender, EventArgs e)
        {
            if (TáblaMűvelet.Rows.Count <= 0) throw new HibásBevittAdat("Nincs sora a táblázatnak!");
            string fájlexc;
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = "MyDocuments",
                Title = "Teljes tartalom mentése Excel fájlba",
                FileName = $"Eszterga_Karbantartás_Műveletek_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                Filter = "Excel |*.xlsx"
            };
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                fájlexc = SaveFileDialog1.FileName;
            else
                return;
            fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);

            MyE.EXCELtábla(fájlexc, TáblaMűvelet, false);
            MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MyE.Megnyitás($"{fájlexc}.xlsx");
        }
        private void TáblaMűvelet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (sender is Zuby.ADGV.AdvancedDataGridView tábla)
                TöröltTáblaSzínezés(tábla, e, "Státusz");
        }
        private void Tábla_SelectionChanged(object sender, EventArgs e)
        {
            int Sorok = TáblaMűvelet.SelectedRows.Count;

            if (Sorok == 2)
            {
                Btn_Csere.Visible = true;
                Btn_Sorrend.Visible = true;
            }
            else
            {
                Btn_Csere.Visible = false;
                Btn_Sorrend.Visible = false;
            }
        }
        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = TáblaMűvelet.Rows[e.RowIndex];
                    TxtBxId.Text = row.Cells[0].Value.ToStrTrim();
                    TxtBxMűvelet.Text = row.Cells[1].Value.ToStrTrim();

                    string egysegValue = row.Cells[2].Value.ToStrTrim();
                    if (Enum.TryParse(egysegValue, out EsztergaEgyseg egysegEnum))
                        CmbxEgység.SelectedItem = egysegEnum;

                    TxtBxMennyiNap.Text = row.Cells[3].Value.ToStrTrim();
                    TxtBxMennyiÓra.Text = row.Cells[4].Value.ToStrTrim();
                    ChckBxStátus.Checked = row.Cells[5].Value.ToStrTrim() == "Törölt";
                    DtmPckrUtolsóDátum.Value = row.Cells[6].Value.ToÉrt_DaTeTime();
                    TxtBxUtolsóÜzemóraÁllás.Text = row.Cells[7].Value.ToStrTrim();
                    TörlésEllenőrzés();
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void DtmPckrUtolsóDátum_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (frissul) return;

                frissul = true;

                DateTime ValasztottDatum = DtmPckrUtolsóDátum.Value;

                if (ValasztottDatum > MaiDatum)
                {
                    MessageBox.Show($"A választott dátum nem lehet később mint a mai nap {MaiDatum}", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Adat_Eszterga_Üzemóra uzemoraRekord = KeresÜzemóra(0, ValasztottDatum, EsztergaEgyseg.Dátum);

                if (uzemoraRekord != null)
                    TxtBxUtolsóÜzemóraÁllás.Text = uzemoraRekord.Üzemóra.ToStrTrim();
                else
                    TxtBxUtolsóÜzemóraÁllás.Text = "0";
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                frissul = false;
            }
        }
        private void TxtBxUtolsóÜzemóraÁllás_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (frissul) return;

                frissul = true;

                if (!long.TryParse(TxtBxUtolsóÜzemóraÁllás.Text, out long ValasztottUzemora))
                {
                    MessageBox.Show("Csak pozitív egész szám lehet az üzemóra állásánál.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Adat_Eszterga_Üzemóra uzemoraRekord = KeresÜzemóra(ValasztottUzemora, DateTime.MinValue, EsztergaEgyseg.Üzemóra);

                if (uzemoraRekord != null)
                    DtmPckrUtolsóDátum.Value = uzemoraRekord.Dátum;
                else
                    DtmPckrUtolsóDátum.Value = new DateTime(1900, 1, 1);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n A hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                frissul = false;
            }
        }
        private void DtmPckrUtolagos_ValueChanged(object sender, EventArgs e)
        {

        }
        private void BttnUtolag_Modosit_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region Ablakok
        Ablak_Eszterga_Karbantartás_Üzemóra Új_ablak_EsztergaÜzemóra;
        private void Üzemóra_Oldal_Click(object sender, EventArgs e)
        {
            if (Új_ablak_EsztergaÜzemóra == null)
            {
                Új_ablak_EsztergaÜzemóra = new Ablak_Eszterga_Karbantartás_Üzemóra();
                Új_ablak_EsztergaÜzemóra.FormClosed += Új_ablak_EsztergaÜzemóra_Closed;
                Új_ablak_EsztergaÜzemóra.Show();
                Új_ablak_EsztergaÜzemóra.Eszterga_Változás += TáblaListázásMűvelet;
            }
            else
            {
                Új_ablak_EsztergaÜzemóra.Activate();
                Új_ablak_EsztergaÜzemóra.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_ablak_EsztergaMódosít_Closed(object sender, FormClosedEventArgs e)
        {
            Új_ablak_EsztergaÜzemóra?.Close();
        }
        private void Új_ablak_EsztergaÜzemóra_Closed(object sender, FormClosedEventArgs e)
        {
            Új_ablak_EsztergaÜzemóra = null;
        }
        #endregion
    }
}
