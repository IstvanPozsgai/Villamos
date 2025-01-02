using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using Application = System.Windows.Forms.Application;
using Funkció = Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga.Eszterga_Funkció;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga
{
    public partial class Ablak_Eszterga_Karbantartás_Módosít : Form
    {
        #region osztalyszintű elemek

        public event Event_Kidobó Eszterga_Változás;
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Eszterga_Karbantartás.mdb";
        readonly string jelszó = "bozaim";
        private List<Adat_Eszterga_Műveletek> AdatokMűvelet;
        private List<Adat_Eszterga_Üzemóra> AdatokÜzemóra;
        #endregion

        #region Alap
        public Ablak_Eszterga_Karbantartás_Módosít()
        {
            InitializeComponent();
            TáblaListázásMűvelet();
            TáblaListázásÜzem();
            TxtBxId.Enabled = false;
            CmbxEgység.DataSource = Enum.GetValues(typeof(EsztergaEgyseg));
        }
        private void Ablak_Eszterga_Karbantartás_Módosít_Load(object sender, EventArgs e)
        {
            Eszterga_Változás?.Invoke();
            Tábla.ClearSelection();
            TáblaÜzem.ClearSelection();
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
                switch (Egyseg)
                {
                    case "Dátum":
                        TxtBxMennyiÓra.Enabled = false;
                        TxtBxMennyiÓra.Text = "0";
                        break;
                    case "Üzemóra":
                        TxtBxMennyiNap.Enabled = false;
                        TxtBxMennyiNap.Text = "0";
                        break;
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
        private void CmbxEgység_SelectedIndexChanged(object sender, EventArgs e)
        {
            string kivalasztottEgyseg = CmbxEgység.SelectedItem.ToString();
            EgysegEllenőrzés(kivalasztottEgyseg);
        }
        #endregion

        #region Metodusok
        private void TörlésEllenőrzés()
        {
            Btn_Törlés.Visible = !ChckBxStátus.Checked;
            Btn_TörlésÜzem.Visible = !ChckBxStátusÜzem.Checked;
        }
        private void SzerkesztőEllenőrzés(bool ÚjFelvétel = false)
        {
            if (ÚjFelvétel)
            {
                LblSorsz.Visible = true;
                TxtBxId.Visible = true;
                LblMűvelet.Visible = true;
                TxtBxMűvelet.Visible = true;
                LblNap.Visible = true;
                TxtBxMennyiNap.Visible = true;
                LblÓra.Visible = true;
                TxtBxMennyiÓra.Visible = true;
                LblEgység.Visible = true;
                CmbxEgység.Visible = true;
                LblUtolsoÜzemÓ.Visible = true;
                TxtBxUtolsóÜzemóraÁllás.Visible = true;
                LblUtolsóDát.Visible = true;
                DtmPckrUtolsóDátum.Visible = true;
                LblStát.Visible = true;
                ChckBxStátus.Visible = true;
            }
            else
            {
                int ID = TxtBxId.Text.ToÉrt_Int();
                bool VanID = Tábla.Rows.Cast<DataGridViewRow>().Any(row => row.Cells[0].Value.ToÉrt_Int() == ID);

                if (VanID)
                {
                    LblSorsz.Visible = true;
                    TxtBxId.Visible = true;
                    LblMűvelet.Visible = true;
                    TxtBxMűvelet.Visible = true;
                    LblNap.Visible = true;
                    TxtBxMennyiNap.Visible = true;
                    LblÓra.Visible = true;
                    TxtBxMennyiÓra.Visible = true;
                    LblEgység.Visible = true;
                    CmbxEgység.Visible = true;
                    LblUtolsoÜzemÓ.Visible = true;
                    TxtBxUtolsóÜzemóraÁllás.Visible = true;
                    LblUtolsóDát.Visible = true;
                    DtmPckrUtolsóDátum.Visible = true;
                    LblStát.Visible = true;
                    ChckBxStátus.Visible = true;
                }
                else
                {
                    LblSorsz.Visible = false;
                    TxtBxId.Visible = false;
                    LblMűvelet.Visible = false;
                    TxtBxMűvelet.Visible = false;
                    LblNap.Visible = false;
                    TxtBxMennyiNap.Visible = false;
                    LblÓra.Visible = false;
                    TxtBxMennyiÓra.Visible = false;
                    LblEgység.Visible = false;
                    CmbxEgység.Visible = false;
                    LblUtolsoÜzemÓ.Visible = false;
                    TxtBxUtolsóÜzemóraÁllás.Visible = false;
                    LblUtolsóDát.Visible = false;
                    DtmPckrUtolsóDátum.Visible = false;
                    LblStát.Visible = false;
                    ChckBxStátus.Visible = false;
                }
                TörlésEllenőrzés();
            }
        }
        private void SzerkesztőEllenőrzésÜzem()
        {
            try
            {
                if (!string.IsNullOrEmpty(TxtBxÜzem.Text))
                {
                    LblÜzem.Visible = true;
                    TxtBxÜzem.Visible = true;
                    LblDátum.Visible = true;
                    DtmPckrDátum.Visible = true;
                    LblStátuszÜzem.Visible = true;
                    ChckBxStátusÜzem.Visible = true;
                }
                TörlésEllenőrzés();
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

        private void TáblaListázásMűvelet()
        {

            DataTable AdatTábla = new DataTable();

            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Sorszám       ");
            AdatTábla.Columns.Add("Művelet       ");
            AdatTábla.Columns.Add("Egység        ");
            AdatTábla.Columns.Add("Nap           ");
            AdatTábla.Columns.Add("Óra           ");
            AdatTábla.Columns.Add("Státusz       ");
            AdatTábla.Columns.Add("Utolsó Dátum  ");
            AdatTábla.Columns.Add("Utolsó Üzemóra");

            AdatokMűvelet = Funkció.Eszterga_KarbantartasFeltölt();
            AdatTábla.Clear();
            foreach (Adat_Eszterga_Műveletek rekord in AdatokMűvelet)
            {
                if (ChckBxTörölt.Checked || !rekord.Státus)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Sorszám       "] = rekord.ID;
                    Soradat["Művelet       "] = rekord.Művelet;
                    Soradat["Egység        "] = rekord.Egység;
                    Soradat["Nap           "] = rekord.Mennyi_Dátum;
                    Soradat["Óra           "] = rekord.Mennyi_Óra;
                    Soradat["Státusz       "] = rekord.Státus ? "Törölt" : "Aktív";
                    Soradat["Utolsó Dátum  "] = rekord.Utolsó_Dátum.ToShortDateString();
                    Soradat["Utolsó Üzemóra"] = rekord.Utolsó_Üzemóra_Állás;

                    AdatTábla.Rows.Add(Soradat);
                }
            }
            Tábla.DataSource = AdatTábla;

            Tábla.Columns["Sorszám       "].Width = 90;
            Tábla.Columns["Művelet       "].Width = 525;
            Tábla.Columns["Egység        "].Width = 80;
            Tábla.Columns["Nap           "].Width = 60;
            Tábla.Columns["Óra           "].Width = 60;
            Tábla.Columns["Státusz       "].Width = 80;
            Tábla.Columns["Utolsó Dátum  "].Width = 110;
            Tábla.Columns["Utolsó Üzemóra"].Width = 140;

            Tábla.Visible = true;
            Tábla.ClearSelection();
        }

        private void TáblaListázásÜzem()
        {
            DataTable AdatTábla = new DataTable();

            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Üzemóra");
            AdatTábla.Columns.Add("Dátum");
            AdatTábla.Columns.Add("Státusz");

            AdatokÜzemóra = Funkció.Eszterga_ÜzemóraFeltölt();
            AdatTábla.Clear();
            foreach (Adat_Eszterga_Üzemóra rekord in AdatokÜzemóra)
            {
                if (ChckBxTörölt.Checked || !rekord.Státus)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Üzemóra"] = rekord.Üzemóra;
                    Soradat["Dátum"] = rekord.Dátum.ToShortDateString();
                    Soradat["Státusz"] = rekord.Státus ? "Törölt" : "Aktív";
                }
            }
            TáblaÜzem.DataSource = AdatTábla;

            TáblaÜzem.Columns["Üzemóra"].Width = 120;
            TáblaÜzem.Columns["Dátum"].Width = 110;
            TáblaÜzem.Columns["Státusz"].Width = 80;

            TáblaÜzem.Visible = true;
            TáblaÜzem.ClearSelection();
        }


        private int TxtBxEllenőrzés()
        {
            int hiba = 0;
            List<string> Hibák = new List<string>();

            if (!TxtBxId.Visible)
            {
                hiba = 90;
                return hiba;
            }

            if (TxtBxId.Text == null || TxtBxId.Text == "" || TxtBxId.Text == "0")
            {
                Hibák.Add("Töltse ki az Azonosító mezőt.");
                hiba++;
            }

            string Egyseg = CmbxEgység.SelectedItem?.ToString();
            bool Nap = int.TryParse(TxtBxMennyiNap.Text, out int MennyiNap);
            bool Óra = int.TryParse(TxtBxMennyiÓra.Text, out int MennyiÓra);

            if (Egyseg == "Dátum" && (!Nap || MennyiNap <= 0))
            {
                Hibák.Add("A Nap mező nem lehet nulla vagy üres.");
                hiba++;
            }
            else if (Egyseg == "Üzemóra" && (!Óra || MennyiÓra <= 0))
            {
                Hibák.Add("Az Óra mező nem lehet nulla vagy üres.");
                hiba++;
            }
            else if (Egyseg == "Bekövetkezés" && (!Nap || !Óra || MennyiNap <= 0 || MennyiÓra <= 0))
            {
                Hibák.Add("A Nap és Óra mezők nem lehetnek nulla vagy üres értékek.");
                hiba++;
            }

            if (TxtBxMűvelet.Text == "")
            {
                Hibák.Add("Töltse ki a Művelet mezőt.");
                hiba++;
            }

            if (TxtBxUtolsóÜzemóraÁllás.Text == "" || TxtBxUtolsóÜzemóraÁllás.Text == "0" || !long.TryParse(TxtBxUtolsóÜzemóraÁllás.Text, out _))
            {
                Hibák.Add("Az Utolsó Üzemóra Állás mező csak egész számot tartalmazhat.");
                hiba++;
            }
            else
            {
                long aktualisUzemora = 0;
                if (AdatokÜzemóra.Count > 0) aktualisUzemora = AdatokÜzemóra.Max(u => u.Üzemóra);
                if (TxtBxUtolsóÜzemóraÁllás.Text.ToÉrt_Long() > aktualisUzemora)
                {
                    Hibák.Add("Az Utolsó Üzemóra Állás nem lehet nagyobb, mint az aktuális Üzemóra érték.");
                    hiba++;
                }
            }
            if (DtmPckrDátum.Value.Date > DateTime.Now.Date)
            {
                Hibák.Add("A kiválasztott dátum nem lehet későbbi, mint a mai dátum.");
                hiba++;
            }

            if (Hibák.Count > 0)
            {
                string HibaÜzenet = Hibák.Count == 1 ? Hibák[0]
                    : $"Ellenőrizze a hibákat és javítsa ki a megfelelő adatokra:\n{string.Join("\n", Hibák)}";

                MessageBox.Show(HibaÜzenet, "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return hiba;
        }
        private void Rendezés()
        {
            List<Adat_Eszterga_Műveletek> rekordok = Funkció.Eszterga_KarbantartasFeltölt();

            rekordok.Sort((x, y) => x.ID.CompareTo(y.ID));

            int KovetkezoID = 1;

            for (int i = 0; i < rekordok.Count; i++)
            {
                Adat_Eszterga_Műveletek rekord = rekordok[i];

                if (rekord.ID != KovetkezoID)
                {
                    string szöveg = $"UPDATE Műveletek SET ID = {KovetkezoID} WHERE ID = {rekord.ID}";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                    rekord.ID = KovetkezoID;
                }

                KovetkezoID++;
            }
        }
        private List<Adat_Eszterga_Műveletek> SorKivalasztas()
        {
            List<Adat_Eszterga_Műveletek> rekordok = new List<Adat_Eszterga_Műveletek>();

            foreach (DataGridViewRow sor in Tábla.SelectedRows)
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
                Adat_Eszterga_Műveletek rekord = AdatokMűvelet.FirstOrDefault(a => a.ID == int.Parse(TxtBxId.Text));
                Enum.TryParse(CmbxEgység.SelectedItem.ToString(), out EsztergaEgyseg Egyseg);
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
                Adat_Eszterga_Üzemóra rekord = AdatokÜzemóra
                    .FirstOrDefault(a => a.Dátum == DtmPckrDátum.Value.Date);

                if (rekord != null &&
                    rekord.Üzemóra == int.Parse(TxtBxÜzem.Text) &&
                    rekord.Státus == ChckBxStátusÜzem.Checked)
                    return false;
            }

            return true;
        }
        #endregion

        #region Gombok,Muveletek
        private void Btn_Módosít_Click(object sender, EventArgs e)
        {
            try
            {
                int hiba = TxtBxEllenőrzés();
                if (hiba == 90)
                    throw new HibásBevittAdat("Nincsen kiválasztva egy sor sem.");

                if (hiba >= 1)
                    return;

                string szöveg = "";
                bool Valtozott = ModositasEll(true);

                if (!Valtozott)
                {
                    MessageBox.Show("Nem történt változás.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Adat_Eszterga_Műveletek rekord = (from a in Funkció.Eszterga_KarbantartasFeltölt()
                                                  where a.ID == TxtBxId.Text.ToÉrt_Int()
                                                  select a).FirstOrDefault();
                if (rekord != null)
                {
                    szöveg = $"UPDATE Műveletek SET ";
                    szöveg += $"Művelet='{TxtBxMűvelet.Text.Trim()}', ";
                    szöveg += $"Egység={(int)CmbxEgység.SelectedItem}, ";
                    szöveg += $"Mennyi_Dátum={TxtBxMennyiNap.Text.Trim()}, ";
                    szöveg += $"Mennyi_Óra={TxtBxMennyiÓra.Text.Trim()}, ";
                    szöveg += $"Státus={(ChckBxStátus.Checked ? "True" : "False")}, ";
                    szöveg += $"Utolsó_Dátum=#{DtmPckrUtolsóDátum.Value:yyyy-MM-dd}#, ";
                    szöveg += $"Utolsó_Üzemóra_állás={TxtBxUtolsóÜzemóraÁllás.Text.Trim()} ";
                    szöveg += $"WHERE ID = {rekord.ID} ";
                }
                else
                {
                    szöveg = $"INSERT INTO Műveletek (ID, Művelet, Egység, Mennyi_Dátum, Mennyi_Óra, Státus, Utolsó_Dátum, Utolsó_Üzemóra_Állás) VALUES(";
                    szöveg += $"'{TxtBxId.Text.Trim()}', ";
                    szöveg += $"'{TxtBxMűvelet.Text.Trim()}', ";
                    szöveg += $"{(int)CmbxEgység.SelectedItem}, ";
                    szöveg += $"{TxtBxMennyiNap.Text.Trim()}, ";
                    szöveg += $"{TxtBxMennyiÓra.Text.Trim()}, ";
                    szöveg += $"{(ChckBxStátus.Checked ? "True" : "False")}, ";
                    szöveg += $"#{DtmPckrUtolsóDátum.Value:yyyy-MM-dd}#, ";
                    szöveg += $"{TxtBxUtolsóÜzemóraÁllás.Text.Trim()})";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
                Eszterga_Változás?.Invoke();
                TáblaListázásMűvelet();
                TörlésEllenőrzés();
                SzerkesztőEllenőrzés(false);
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Rögzítve.", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void Btn_Törlés_Click(object sender, EventArgs e)
        {
            try
            {
                int hiba = TxtBxEllenőrzés();
                if (hiba == 90)
                    throw new HibásBevittAdat("Nincsen kiválasztva egy sor sem.");

                if (hiba >= 1)
                    return;

                foreach (DataGridViewRow row in Tábla.SelectedRows)
                {
                    bool Torolt = (row.Cells[5].Value.ToStrTrim() == "Törölt").ToÉrt_Bool();
                    if (Torolt)
                        throw new HibásBevittAdat("Csak olyan sorokat lehet törölni, amik nincsenek törölve.");
                }

                string szöveg = "";
                AdatokMűvelet = Funkció.Eszterga_KarbantartasFeltölt();

                List<int> AktivID = new List<int>();
                foreach (DataGridViewRow row in Tábla.SelectedRows)
                    AktivID.Add(row.Cells[0].Value.ToÉrt_Int());

                foreach (int Id in AktivID)
                {
                    Adat_Eszterga_Műveletek rekord = AdatokMűvelet.FirstOrDefault(a => a.ID == Id);
                    if (rekord != null)
                    {
                        szöveg = $"UPDATE Műveletek SET Státus=True WHERE ID={Id}";
                        MyA.ABMódosítás(hely, jelszó, szöveg);
                    }
                }
                Eszterga_Változás?.Invoke();
                TáblaListázásMűvelet();
                SzerkesztőEllenőrzés(true);
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
                DtmPckrUtolsóDátum.Value = DateTime.Today;
                TxtBxUtolsóÜzemóraÁllás.Text = "0";
                SzerkesztőEllenőrzés(true);
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
        private void Tábla_SelectionChanged(object sender, EventArgs e)
        {
            int Sorok = Tábla.SelectedRows.Count;

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
        private void Btn_Csere_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count != 2)
                    throw new HibásBevittAdat("A cseréhez 2 sor kijelölésére van szükség");

                List<Adat_Eszterga_Műveletek> rekordok = SorKivalasztas();

                Adat_Eszterga_Műveletek rekord1 = rekordok[0];
                Adat_Eszterga_Műveletek rekord2 = rekordok[1];

                string szöveg1 = $"UPDATE Műveletek SET Művelet='{rekord2.Művelet}', ";
                szöveg1 += $"Egység={rekord2.Egység}, ";
                szöveg1 += $"Mennyi_Dátum={rekord2.Mennyi_Dátum}, ";
                szöveg1 += $"Mennyi_Óra={rekord2.Mennyi_Óra}, ";
                szöveg1 += $"Státus={(rekord2.Státus ? "True" : "False")},";
                szöveg1 += $"Utolsó_Dátum=#{rekord2.Utolsó_Dátum:yyyy-MM-dd}#,";
                szöveg1 += $"Utolsó_Üzemóra_állás={rekord2.Utolsó_Üzemóra_Állás} ";
                szöveg1 += $"WHERE ID={rekord1.ID}";

                string szöveg2 = $"UPDATE Műveletek SET Művelet='{rekord1.Művelet}', ";
                szöveg2 += $"Egység={rekord1.Egység}, ";
                szöveg2 += $"Mennyi_Dátum={rekord1.Mennyi_Dátum}, ";
                szöveg2 += $"Mennyi_Óra={rekord1.Mennyi_Óra}, ";
                szöveg2 += $"Státus={(rekord1.Státus ? "True" : "False")},";
                szöveg2 += $"Utolsó_Dátum=#{rekord1.Utolsó_Dátum:yyyy-MM-dd}#,";
                szöveg2 += $"Utolsó_Üzemóra_állás={rekord1.Utolsó_Üzemóra_Állás} ";
                szöveg2 += $"WHERE ID={rekord2.ID}";

                MyA.ABMódosítás(hely, jelszó, szöveg1);
                MyA.ABMódosítás(hely, jelszó, szöveg2);
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
                if (Tábla.SelectedRows.Count != 2)
                    throw new HibásBevittAdat("A sorrend módosításához 2 sor kijelölésére van szükség");

                List<Adat_Eszterga_Műveletek> rekordok = SorKivalasztas();

                Adat_Eszterga_Műveletek elso = rekordok[1];
                Adat_Eszterga_Műveletek masodik = rekordok[0];
                string szöveg = "";
                string szövegMozog = "";
                int ElsoID = elso.ID;
                int MasodikID = masodik.ID;

                if (ElsoID < MasodikID)
                {
                    szöveg = $"UPDATE Műveletek SET ID = ID + 1 WHERE ID >= {MasodikID}";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                    szövegMozog = $"UPDATE Műveletek SET ID = {MasodikID} WHERE ID = {ElsoID}";
                    MyA.ABMódosítás(hely, jelszó, szövegMozog);
                }
                else
                {
                    szöveg = $"UPDATE Műveletek SET ID = ID + 1 WHERE ID >= {MasodikID}";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                    szövegMozog = $"UPDATE Műveletek SET ID = {MasodikID} WHERE ID = {ElsoID + 1}";
                    MyA.ABMódosítás(hely, jelszó, szövegMozog);
                }
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
        private void ChckBxTörölt_CheckedChanged(object sender, EventArgs e)
        {
            TáblaListázásMűvelet();
            TáblaListázásÜzem();
        }
        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = Tábla.Rows[e.RowIndex];
                    TxtBxId.Text = row.Cells[0].Value.ToStrTrim();
                    TxtBxMűvelet.Text = row.Cells[1].Value.ToStrTrim();

                    int egysegValue = row.Cells[2].Value.ToÉrt_Int();
                    EsztergaEgyseg egysegEnum = (EsztergaEgyseg)egysegValue;
                    CmbxEgység.SelectedItem = egysegEnum;

                    TxtBxMennyiNap.Text = row.Cells[3].Value.ToStrTrim();
                    TxtBxMennyiÓra.Text = row.Cells[4].Value.ToStrTrim();
                    ChckBxStátus.Checked = row.Cells[5].Value.ToStrTrim() == "Törölt";
                    DtmPckrUtolsóDátum.Value = row.Cells[6].Value.ToÉrt_DaTeTime();
                    TxtBxUtolsóÜzemóraÁllás.Text = row.Cells[7].Value.ToStrTrim();
                    SzerkesztőEllenőrzés();
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

        private void Btn_TörlésÜzem_Click(object sender, EventArgs e)
        {
            try
            {
                if (TáblaÜzem.SelectedRows.Count == 0)
                    throw new HibásBevittAdat("Nincsen kiválasztva egy sor sem.");

                foreach (DataGridViewRow row in TáblaÜzem.SelectedRows)
                {
                    bool Torolt = (row.Cells[2].Value.ToStrTrim() == "Törölt").ToÉrt_Bool();
                    if (Torolt)
                        throw new HibásBevittAdat("Csak olyan sorokat lehet törölni, amik nincsenek törölve.");
                }

                string szöveg = "";

                foreach (DataGridViewRow row in TáblaÜzem.SelectedRows)
                {
                    int Uzemora = row.Cells[0].Value.ToÉrt_Int();
                    DateTime Datum = DateTime.Parse(row.Cells[1].Value.ToString().Trim());

                    szöveg = $"UPDATE Üzemóra SET Státus=True WHERE Üzemóra={Uzemora} AND Dátum=#{Datum:yyyy-MM-dd}#";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
                Eszterga_Változás?.Invoke();
                TáblaListázásÜzem();
                Btn_TörlésÜzem.Visible = false;
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
        private void Btn_MódosítÜzem_Click(object sender, EventArgs e)
        {
            try
            {
                if (TáblaÜzem.SelectedRows.Count == 0)
                    throw new HibásBevittAdat("Nincsen kiválasztva egy sor sem.");
                bool Valtozott = ModositasEll(false);

                if (!Valtozott)
                {
                    MessageBox.Show("Nem történt változás.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DataGridViewRow KivalasztottSor = TáblaÜzem.SelectedRows[0];

                int UjUzemora = TxtBxÜzem.Text.ToÉrt_Int();
                DateTime UjDatum = DtmPckrDátum.Value.Date;
                bool UjStatus = ChckBxStátusÜzem.Checked;
                string szöveg = "";
                int ElozoUzemora = KivalasztottSor.Cells[0].Value.ToÉrt_Int();
                DateTime EozoDatum = DateTime.Parse(KivalasztottSor.Cells[1].Value.ToString());

                szöveg = $"UPDATE Üzemóra SET ";
                szöveg += $"Üzemóra={UjUzemora}, ";
                szöveg += $"Dátum=#{UjDatum:yyyy-MM-dd}#, ";
                szöveg += $"Státus={(UjStatus ? "True" : "False")} ";
                szöveg += $"WHERE Üzemóra={ElozoUzemora} AND Dátum=#{EozoDatum:yyyy-MM-dd}#";

                MyA.ABMódosítás(hely, jelszó, szöveg);

                Eszterga_Változás?.Invoke();
                TáblaListázásÜzem();
                SzerkesztőEllenőrzésÜzem();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Rögzítve.", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void TáblaÜzem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = TáblaÜzem.Rows[e.RowIndex];
                    TxtBxÜzem.Text = row.Cells[0].Value.ToStrTrim();
                    DtmPckrDátum.Value = row.Cells[1].Value.ToÉrt_DaTeTime();
                    ChckBxStátusÜzem.Checked = row.Cells[2].Value.ToStrTrim() == "Törölt";
                    SzerkesztőEllenőrzésÜzem();
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

        private void Btn_Excel_Click(object sender, EventArgs e)
        {
            if (Tábla.Rows.Count <= 0) throw new HibásBevittAdat("Nincs sora a táblázatnak!");
            string fájlexc;
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = "MyDocuments",
                Title = "Teljes tartalom mentése Excel fájlba",
                FileName = $"Eszterga_Karbantartás_Teljes_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                Filter = "Excel |*.xlsx"
            };
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                fájlexc = SaveFileDialog1.FileName;
            else
                return;
            fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);

            MyE.EXCELtábla(fájlexc, Tábla, true);
            MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MyE.Megnyitás($"{fájlexc}.xlsx");
        }

        #endregion
    }
}
