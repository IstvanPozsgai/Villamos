using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.V_Ablakok._4_Nyilvántartások.Eszterga_Karbantartás;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;
using Funkcio = Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga.Eszterga_Funkció;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga
{
    public partial class Ablak_Eszterga_Karbantartás_Módosít : Form
    {
        #region Osztalyszintű elemek
        public event Event_Kidobó Eszterga_Valtozas;
        readonly bool Baross = Program.PostásTelephely.Trim() == "Angyalföld";
        private bool frissul = false;
        readonly DataTable AdatTabla = new DataTable();
        readonly DataTable AdatTablaUtolag = new DataTable();
        DataTable AdatTablaNaplo = new DataTable();
        #endregion

        #region Listák
        private List<Adat_Eszterga_Muveletek> AdatokMuvelet;
        private List<Adat_Eszterga_Uzemora> AdatokUzemora;
        private List<Adat_Eszterga_Muveletek_Naplo> AdatokNaplo;
        #endregion

        #region Kezelők
        readonly private Kezelő_Eszterga_Műveletek KézMűveletek = new Kezelő_Eszterga_Műveletek();
        readonly private Kezelő_Eszterga_Műveletek_Napló KézNapló = new Kezelő_Eszterga_Műveletek_Napló();
        readonly private Kezelő_Eszterga_Üzemóra KezUzemora = new Kezelő_Eszterga_Üzemóra();
        #endregion

        #region Alap
        /// <summary>
        /// Ablak inicializálása és adatok betöltése a vezérlőelemekbe
        /// </summary>
        public Ablak_Eszterga_Karbantartás_Módosít()
        {
            InitializeComponent();
            TablaListazasMuvelet();
            TablaNaploListazas();
            TablaListazasMuveletUtolag();
            TxtBxId.Enabled = false;
            CmbxEgység.DataSource = Enum.GetValues(typeof(EsztergaEgyseg));
        }
        /// <summary>
        /// Az ablak betöltésekor lefutó inicializálási műveletek
        /// </summary>
        private void Ablak_Eszterga_Karbantartás_Módosít_Load(object sender, EventArgs e)
        {
            Eszterga_Valtozas?.Invoke();
            TablaMuvelet.ClearSelection();
            TablaNaplo.ClearSelection();
            TablaUtolagMuvelet.ClearSelection();
            JogosultsagKiosztas();
            Btn_Csere.Visible = false;
            Btn_Sorrend.Visible = false;
            TablaMuvelet.CellFormatting += TáblaMűvelet_CellFormatting;
            EgysegBeallitasa();
            UzemoraKiolvasasEsBeiras(DtmPckrUtolagos.Value, TxtBxUtolagUzemora);
        }
        /// <summary>
        /// Jogosultságok alapján gombok láthatóságát és engedélyezettségét állítja be
        /// </summary>
        private void JogosultsagKiosztas()
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
            Datum = 1,
            Uzemora = 2,
            Bekovetkezes = 3
        }
        private void EgysegEllenorzes(string Egyseg)
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

                        Adat_Eszterga_Uzemora uzemoraRekordDatum = KeresÜzemóra(0, DtmPckrUtolsóDátum.Value, EsztergaEgyseg.Datum);
                        TxtBxUtolsóÜzemóraÁllás.Text = uzemoraRekordDatum != null ? uzemoraRekordDatum.Uzemora.ToStrTrim() : "0";
                        break;

                    case "Üzemóra":
                        TxtBxMennyiNap.Enabled = false;
                        TxtBxMennyiNap.Text = "0";
                        DtmPckrUtolsóDátum.Enabled = false;

                        if (long.TryParse(TxtBxUtolsóÜzemóraÁllás.Text, out long uzemora))
                        {
                            Adat_Eszterga_Uzemora uzemoraRekordUzemora = KeresÜzemóra(uzemora, DateTime.MinValue, EsztergaEgyseg.Uzemora);
                            DtmPckrUtolsóDátum.Value = uzemoraRekordUzemora?.Dátum ?? new DateTime(1900, 1, 1);
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
            EgysegEllenorzes(kivalasztottEgyseg);
        }
        private void EgysegBeallitasa()
        {
            try
            {
                CmbxEgység.SelectedItem = EsztergaEgyseg.Bekovetkezes;
                List<Adat_Eszterga_Muveletek> AdatokMűvelet = Funkcio.Eszterga_KarbantartasFeltolt();
                int KovetkezoID = AdatokMűvelet.Any() ? AdatokMűvelet.Max(a => a.ID) + 1 : 1;
                TxtBxId.Text = KovetkezoID.ToStrTrim();
                EgysegEllenorzes(EsztergaEgyseg.Bekovetkezes.ToStrTrim());
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
        /// <summary>
        /// A karbantartási műveletek adatainak betöltése és megjelenítése a TáblaMűveletbe
        /// </summary>
        private void TablaListazasMuvelet()
        {
            try
            {
                AdatTabla.Columns.Clear();
                AdatTabla.Columns.Add("Sorszám");
                AdatTabla.Columns.Add("Művelet");
                AdatTabla.Columns.Add("Egység");
                AdatTabla.Columns.Add("Nap");
                AdatTabla.Columns.Add("Óra");
                AdatTabla.Columns.Add("Státusz");
                AdatTabla.Columns.Add("Utolsó Dátum");
                AdatTabla.Columns.Add("Utolsó Üzemóra");

                AdatokMuvelet = Funkcio.Eszterga_KarbantartasFeltolt();
                AdatokUzemora = Funkcio.Eszterga_UzemoraFeltolt();
                AdatTabla.Clear();

                foreach (Adat_Eszterga_Muveletek rekord in AdatokMuvelet)
                {
                    DataRow Soradat = AdatTabla.NewRow();

                    Soradat["Sorszám"] = rekord.ID;
                    Soradat["Művelet"] = rekord.Művelet;
                    Soradat["Egység"] = Enum.GetName(typeof(EsztergaEgyseg), rekord.Egység);
                    Soradat["Nap"] = rekord.Mennyi_Dátum;
                    Soradat["Óra"] = rekord.Mennyi_Óra;
                    Soradat["Státusz"] = rekord.Státus ? "Törölt" : "Aktív";
                    Soradat["Utolsó Dátum"] = rekord.Utolsó_Dátum.ToShortDateString();
                    Adat_Eszterga_Uzemora uzemoraRekord = AdatokUzemora
                        .FirstOrDefault(a => a.Dátum.Date == rekord.Utolsó_Dátum.Date && a.Státus == false);

                    if (uzemoraRekord != null)
                        Soradat["Utolsó Üzemóra"] = uzemoraRekord.Uzemora;
                    else
                        Soradat["Utolsó Üzemóra"] = rekord.Utolsó_Üzemóra_Állás;

                    AdatTabla.Rows.Add(Soradat);
                }

                TablaMuvelet.DataSource = AdatTabla;
                OszlopSzelessegMuvelet();
                TablaMuvelet.Visible = true;
                TablaMuvelet.ClearSelection();
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

        /// <summary>
        /// A karbantartási művelet tábla oszlopszélességeit állítja be
        /// </summary>
        private void OszlopSzelessegMuvelet()
        {
            TablaMuvelet.Columns["Sorszám"].Width = 100;
            TablaMuvelet.Columns["Művelet"].Width = 1155;
            TablaMuvelet.Columns["Egység"].Width = 110;
            TablaMuvelet.Columns["Nap"].Width = 60;
            TablaMuvelet.Columns["Óra"].Width = 60;
            TablaMuvelet.Columns["Státusz"].Width = 85;
            TablaMuvelet.Columns["Utolsó Dátum"].Width = 120;
            TablaMuvelet.Columns["Utolsó Üzemóra"].Width = 160;
        }

        /// <summary>
        /// A karbantartási műveletek naplóbejegyzései betöltése és megjelenítése a TáblaMűveletbe
        /// </summary>
        private void TablaNaploListazas()
        {
            // Az eredeti táblát megtartjuk, de ideiglenes klónban építjük fel a tartalmat
            DataTable IdeiglenesTabla = new DataTable();
            IdeiglenesTabla.Columns.Add("Művelet Sorszáma");
            IdeiglenesTabla.Columns.Add("Művelet");
            IdeiglenesTabla.Columns.Add("Utolsó Dátum");
            IdeiglenesTabla.Columns.Add("Utolsó Üzemóra");
            IdeiglenesTabla.Columns.Add("Megjegyzés");
            IdeiglenesTabla.Columns.Add("Rögzítő");
            IdeiglenesTabla.Columns.Add("Rögzítés Dátuma");

            AdatokNaplo = Funkcio.Eszterga_KarbantartasNaplóFeltölt();

            foreach (Adat_Eszterga_Muveletek_Naplo rekord in AdatokNaplo)
            {
                DataRow sor = IdeiglenesTabla.NewRow();

                sor["Művelet Sorszáma"] = rekord.ID;
                sor["Művelet"] = rekord.Művelet;
                sor["Utolsó Dátum"] = rekord.Utolsó_Dátum.ToShortDateString();
                sor["Utolsó Üzemóra"] = rekord.Utolsó_Üzemóra_Állás;
                sor["Megjegyzés"] = rekord.Megjegyzés;
                sor["Rögzítő"] = rekord.Rögzítő;
                sor["Rögzítés Dátuma"] = rekord.Rögzítés_Dátuma.ToShortDateString();

                IdeiglenesTabla.Rows.Add(sor);
            }

            IEnumerable<DataRow> rendezettAdatok = IdeiglenesTabla.AsEnumerable()
                .OrderBy(sor => DateTime.Parse(sor["Utolsó Dátum"].ToStrTrim()))
                .ThenBy(sor => int.Parse(sor["Művelet Sorszáma"].ToStrTrim()));

            // Eredeti táblát újratöltjük friss, tiszta sorokkal
            AdatTablaNaplo = IdeiglenesTabla.Clone(); // struktúra másolása
            foreach (DataRow sor in rendezettAdatok)
                AdatTablaNaplo.ImportRow(sor);

            TablaNaplo.DataSource = AdatTablaNaplo;

            OszlopSzelessegNaplo();

            for (int i = 0; i < TablaNaplo.Columns.Count; i++)
                TablaNaplo.Columns[i].ReadOnly = true;

            TablaNaplo.Visible = true;
            TablaMuvelet.Visible = true;
            TablaMuvelet.ClearSelection();
        }


        /// <summary>
        /// A naplótábla oszlopszélességeit állítja be
        /// </summary>
        private void OszlopSzelessegNaplo()
        {
            TablaNaplo.Columns["Művelet Sorszáma"].Width = 110;
            TablaNaplo.Columns["Művelet"].Width = 550;
            TablaNaplo.Columns["Utolsó Dátum"].Width = 105;
            TablaNaplo.Columns["Utolsó Üzemóra"].Width = 120;
            TablaNaplo.Columns["Megjegyzés"].Width = 220;
            TablaNaplo.Columns["Rögzítő"].Width = 150;
            TablaNaplo.Columns["Rögzítés Dátuma"].Width = 105;
        }

        /// <summary>
        /// Betölti és megjeleníti az utólagos karbantartási műveleteket a TáblaMűveletben
        /// </summary>
        private void TablaListazasMuveletUtolag()
        {
            try
            {
                AdatTablaUtolag.Columns.Clear();
                AdatTablaUtolag.Columns.Add("Sorszám");
                AdatTablaUtolag.Columns.Add("Művelet");
                AdatTablaUtolag.Columns.Add("Státusz");

                AdatokMuvelet = Funkcio.Eszterga_KarbantartasFeltolt();
                AdatTablaUtolag.Clear();

                foreach (Adat_Eszterga_Muveletek rekord in AdatokMuvelet)
                {
                    DataRow Soradat = AdatTablaUtolag.NewRow();

                    Soradat["Sorszám"] = rekord.ID;
                    Soradat["Művelet"] = rekord.Művelet;
                    Soradat["Státusz"] = rekord.Státus ? "Törölt" : "Aktív";
                    AdatTablaUtolag.Rows.Add(Soradat);
                }

                TablaUtolagMuvelet.DataSource = AdatTablaUtolag;
                OszlopSzelessegMuveletUtolag();
                TablaUtolagMuvelet.Visible = true;
                TablaUtolagMuvelet.ClearSelection();
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

        /// <summary>
        /// Az utólagos művelet TáblaMűvelet oszlopszélességeit állítja be
        /// </summary>
        private void OszlopSzelessegMuveletUtolag()
        {
            TablaUtolagMuvelet.Columns["Sorszám"].Width = 100;
            TablaUtolagMuvelet.Columns["Művelet"].Width = 1160;
            TablaUtolagMuvelet.Columns["Státusz"].Width = 100;
        }

        #endregion

        #region Metodusok

        /// <summary>
        /// Ellenőrzi a törlés gomb láthatóságát a státusz checkbox alapján
        /// </summary>
        private void TorlesEllenorzes()
        {
            Btn_Törlés.Visible = !ChckBxStátus.Checked;
        }

        /// <summary>
        /// Ellenőrzi a megadott adatokat a felhasználói űrlapon. 
        /// Új rekord hozzáadása esetén ellenőrzi, hogy az azonosító már létezik-e az adatbázisban, 
        /// illetve biztosítja, hogy minden mező érvényes adatokat tartalmazzon.
        /// </summary>
        private int TxtBxEllenorzes(bool ujRekord = false)
        {
            try
            {
                int hiba = 0;
                List<string> Hibak = new List<string>();

                bool VanE = AdatokMuvelet.Any(a => a.ID == TxtBxId.Text.ToÉrt_Int());

                if (ujRekord && VanE)
                {
                    Hibak.Add("Az azonosító már létezik az adatbázisban.");
                    hiba++;
                }

                if (!ujRekord && TablaMuvelet.SelectedRows.Count < 1)
                {
                    hiba = 90;
                    Hibak.Add("Nincs kiválasztott művelet.");
                    return hiba;
                }

                if (string.IsNullOrEmpty(TxtBxId.Text))
                {
                    Hibak.Add("Töltse ki az Azonosító mezőt.");
                    hiba++;
                }

                if (!ujRekord && !VanE)
                {
                    Hibak.Add("Az azonosító nem található az adatbázisban.");
                    hiba++;
                }

                if (true)
                {
                    string Egyseg = CmbxEgység.SelectedItem?.ToStrTrim();
                    bool Nap = int.TryParse(TxtBxMennyiNap.Text, out int MennyiNap);
                    bool Ora = int.TryParse(TxtBxMennyiÓra.Text, out int MennyiÓra);

                    if (Egyseg == "Dátum" && (!Nap || MennyiNap <= 0))
                    {
                        Hibak.Add("A Nap mezőben csak pozitív egész szám szerepelhet.");
                        hiba++;
                    }
                    else if (Egyseg == "Üzemóra" && (!Ora || MennyiÓra <= 0))
                    {
                        Hibak.Add("Az Óra mezőben csak pozitív egész szám szerepelhet.");
                        hiba++;
                    }
                    else if (Egyseg == "Bekövetkezés" && (!Nap || !Ora || MennyiNap <= 0 || MennyiÓra <= 0))
                    {
                        Hibak.Add("A Nap és Óra mezőkben csak pozitív egész szám szerepelhetnek.");
                        hiba++;
                    }

                    if (string.IsNullOrEmpty(TxtBxMűvelet.Text))
                    {
                        Hibak.Add("Töltse ki a Művelet mezőt.");
                        hiba++;
                    }

                    if (Egyseg == "Üzemóra" || Egyseg == "Bekövetkezés")
                    {
                        AdatokUzemora = Funkcio.Eszterga_UzemoraFeltolt();

                        if (string.IsNullOrEmpty(TxtBxUtolsóÜzemóraÁllás.Text) || TxtBxUtolsóÜzemóraÁllás.Text == "0" ||
                            !long.TryParse(TxtBxUtolsóÜzemóraÁllás.Text, out _))
                        {
                            Hibak.Add("Az Utolsó Üzemóra Állás mező csak pozitív egész számot tartalmazhat.");
                            hiba++;
                        }
                        else
                        {
                            long aktualisUzemora = AdatokUzemora.Count > 0 ? AdatokUzemora.Max(u => u.Uzemora) : 0;
                            if (long.Parse(TxtBxUtolsóÜzemóraÁllás.Text) > aktualisUzemora)
                            {
                                Hibak.Add("Az Utolsó Üzemóra Állás nem lehet nagyobb, mint az aktuális Üzemóra érték.");
                                hiba++;
                            }
                        }
                    }
                }

                if (Hibak.Count > 0)
                {
                    string HibaUzenet = Hibak.Count == 1
                        ? Hibak[0]
                        : $"Ellenőrizze a hibákat és javítsa ki a megfelelő adatokra:\n{string.Join("\n", Hibak)}";

                    MessageBox.Show(HibaUzenet, "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return hiba;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 91;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 92;
            }
        }

        /// <summary>
        /// Az eszterga karbantartási rekordokat rendezi az ID alapján, és szükség esetén módosítja az ID értékeket,
        /// hogy folyamatosan növekvő sorrendben legyenek.
        /// </summary>
        private void Rendezes()
        {
            List<Adat_Eszterga_Muveletek> rekordok = Funkcio.Eszterga_KarbantartasFeltolt()
                .OrderBy(r => r.ID)
                .ToList();

            int KovetkezoID = 1;

            foreach (Adat_Eszterga_Muveletek rekord in rekordok)
            {
                if (rekord.ID != KovetkezoID)
                {
                    Adat_Eszterga_Muveletek ADAT = new Adat_Eszterga_Muveletek(rekord.ID);
                    KézMűveletek.Rendezés(ADAT, KovetkezoID);
                    rekord.ID = KovetkezoID;
                }

                KovetkezoID++;
            }
        }



        /// <summary>
        /// Kiválasztja a kijelölt sorokat a TáblaMűveletből, és visszaadja az id-jük alapján a megfelelő rekordokat
        /// az adatbázisból.
        /// </summary>
        private List<Adat_Eszterga_Muveletek> SorKivalasztas()
        {
            List<Adat_Eszterga_Muveletek> rekordok = new List<Adat_Eszterga_Muveletek>();

            foreach (DataGridViewRow sor in TablaMuvelet.SelectedRows)
            {
                int id = sor.Cells[0].Value.ToÉrt_Int();

                AdatokMuvelet = Funkcio.Eszterga_KarbantartasFeltolt();

                Adat_Eszterga_Muveletek rekord = (from a in AdatokMuvelet
                                                  where a.ID == id
                                                  select a).FirstOrDefault();

                if (rekord != null)
                    rekordok.Add(rekord);
                else
                    throw new HibásBevittAdat("A kijelölt sorok nem találhatóak az adatbázisban.");
            }

            return rekordok;
        }

        /// <summary>
        /// Ellenőrzi, hogy a megadott adat megegyezik-e az adatbázisban lévővel.
        /// Műveleti mód esetén a művelet adatait, üzemóra mód esetén pedig az üzemórát és dátumot ellenőrzi.
        /// </summary>
        private bool ModositasEll(bool Muvelet = false)
        {
            if (Muvelet)
            {
                AdatokMuvelet = Funkcio.Eszterga_KarbantartasFeltolt();
                Adat_Eszterga_Muveletek rekord = AdatokMuvelet.FirstOrDefault(a => a.ID == int.Parse(TxtBxId.Text));
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
                AdatokUzemora = Funkcio.Eszterga_UzemoraFeltolt();
                Adat_Eszterga_Uzemora rekord = AdatokUzemora
                    .FirstOrDefault(a => a.Dátum == Uj_ablak_EsztergaUzemora.DtmPckrDátum.Value.Date) ?? throw new HibásBevittAdat("Nem található rekord a megadott dátummal.");

                if (rekord.Uzemora == int.Parse(Uj_ablak_EsztergaUzemora.TxtBxÜzem.Text) &&
                    rekord.Státus == Uj_ablak_EsztergaUzemora.ChckBxStátus.Checked)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Színezi a táblázat sorait a státusz alapján, ha a státusz "Törölt".
        /// Ha a státusz "Törölt", a sor háttérszíne piros, szövege fekete, és áthúzott betűtípust kap.
        /// Ha a státusz nem "Törölt", visszaáll a szokásos megjelenítés fehér háttérre.
        /// </summary>
        private void ToroltTablaSzinezes(Zuby.ADGV.AdvancedDataGridView tabla, DataGridViewCellFormattingEventArgs e, string statuszOszlop)
        {
            if (tabla.Columns[e.ColumnIndex].Name == statuszOszlop && e.Value is string statusz)
            {
                DataGridViewRow sor = tabla.Rows[e.RowIndex];

                if (statusz == "Törölt")
                {
                    sor.DefaultCellStyle.BackColor = Color.IndianRed;
                    sor.DefaultCellStyle.ForeColor = Color.Black;
                    sor.DefaultCellStyle.Font = new Font(tabla.DefaultCellStyle.Font, FontStyle.Strikeout);
                }
                else
                {
                    sor.DefaultCellStyle.BackColor = Color.White;
                    sor.DefaultCellStyle.ForeColor = Color.Black;
                    sor.DefaultCellStyle.Font = new Font(tabla.DefaultCellStyle.Font, FontStyle.Regular);
                }
            }
        }

        /// <summary>
        /// Keres egy üzemórát az adatbázisban a megadott feltételek alapján.
        /// Az üzemóra keresése az 'Üzemóra', 'Dátum' és 'Bekövetkezés' egységek szerint történik.
        /// Ha a 'Bekövetkezés' egységet választjuk, akkor a függvény null-t ad vissza.
        /// A 'Üzemóra' és 'Dátum' esetén az adatokat az AdatokUzemora lista alapján keresük.
        /// </summary>
        private Adat_Eszterga_Uzemora KeresÜzemóra(long uzemora, DateTime datum, EsztergaEgyseg egyseg)
        {
            if (egyseg == EsztergaEgyseg.Bekovetkezes)
                return null;
            if (egyseg == EsztergaEgyseg.Uzemora)
                return AdatokUzemora.FirstOrDefault(u => u.Uzemora == uzemora && !u.Státus);
            if (egyseg == EsztergaEgyseg.Datum)
                return AdatokUzemora.FirstOrDefault(u => u.Dátum.Date == datum.Date && !u.Státus);

            return null;
        }

        /// <summary>
        /// Az üzemórát kiolvassa az adatbázisból a megadott dátum alapján, és beírja a TextBox-ba.
        /// Ha található üzemóra rekord, akkor az üzemóra értékét beírja a TextBox-ba, és letiltja a szerkeszthetőséget.
        /// Ha nincs találat, akkor 0-t ír be és engedélyezi a TextBox szerkesztését.
        /// </summary>
        private void UzemoraKiolvasasEsBeiras(DateTime datum, TextBox txt)
        {
            Adat_Eszterga_Uzemora uzemoraRekord = KeresÜzemóra(0, datum, EsztergaEgyseg.Datum);

            if (uzemoraRekord != null)
            {
                txt.Text = uzemoraRekord.Uzemora.ToStrTrim();
                txt.Enabled = false;
            }
            else
            {
                txt.Text = "0";
                txt.Enabled = true;
            }
        }

        /// <summary>
        /// Új üzemóra rekordot ad hozzá az adatbázishoz a megadott dátum, üzemóra érték és státusz alapján.
        /// Az új üzemórát csak akkor rögzíti, ha az érték az előző és következő üzemóra értékek között helyezkedik el.
        /// Ha a feltételek nem teljesülnek, akkor figyelmeztetést ad, és nem rögzíti az új üzemórát.
        /// </summary>
        private bool UjUzemoraHozzaadasa(DateTime UjDatum, long UjUzemora, bool UjStatus)
        {
            long ElozoUzemora = (from a in AdatokUzemora
                                 where a.Dátum < UjDatum && a.Státus == false
                                 orderby a.Dátum descending
                                 select a.Uzemora).FirstOrDefault();

            long UtanaUzemora = (from a in AdatokUzemora
                                 where a.Dátum > UjDatum && a.Státus == false
                                 orderby a.Dátum
                                 select a.Uzemora).FirstOrDefault();

            if (UjUzemora <= ElozoUzemora || (UtanaUzemora != 0 && UjUzemora >= UtanaUzemora))
                throw new HibásBevittAdat($"Az üzemóra értéknek az előző: {ElozoUzemora} és következő: {UtanaUzemora} közé kell esnie.");

            Adat_Eszterga_Uzemora ADAT = new Adat_Eszterga_Uzemora(0,
                                              UjUzemora,
                                              UjDatum,
                                              UjStatus);
            KezUzemora.Rogzites(ADAT);

            return true;
        }

        #endregion

        #region Gombok,Muveletek

        /// <summary>
        /// Ha van kijelölt rekord, akkor módosítja azt az új adatokkal, ha nem, akkor új rekordot ad hozzá.
        /// Ellenőrzi, hogy a szükséges adatokat megfelelően kitöltötték-e, majd végrehajtja a módosítást vagy a hozzáadást.
        /// </summary>
        private void Btn_Módosít_Click(object sender, EventArgs e)
        {
            try
            {
                bool UjRekord = string.IsNullOrEmpty(TxtBxId.Text) || !AdatokMuvelet.Any(a => a.ID == TxtBxId.Text.ToÉrt_Int());
                int hiba = TxtBxEllenorzes(UjRekord);
                if (hiba == 90)
                    throw new HibásBevittAdat("Nincsen kiválasztva egy sor sem.");

                if (hiba >= 1)
                    return;
                bool Valtozott = ModositasEll(true);

                if (!Valtozott)
                    throw new HibásBevittAdat("Nem történt változás.");

                DateTime UjDatum = DtmPckrUtolsóDátum.Value.Date;
                long UjUzemora = TxtBxUtolsóÜzemóraÁllás.Text.ToÉrt_Long();

                Adat_Eszterga_Muveletek rekord = AdatokMuvelet
                    .FirstOrDefault(a => a.ID == TxtBxId.Text.ToÉrt_Int());

                if (rekord != null)
                {
                    Adat_Eszterga_Muveletek ADAT = new Adat_Eszterga_Muveletek(TxtBxId.Text.ToÉrt_Int(),
                                                                                TxtBxMűvelet.Text.ToStrTrim(),
                                                                                (int)CmbxEgység.SelectedItem,
                                                                                TxtBxMennyiNap.Text.ToÉrt_Int(),
                                                                                TxtBxMennyiÓra.Text.ToÉrt_Int(),
                                                                                ChckBxStátus.Checked.ToÉrt_Bool(),
                                                                                UjDatum,
                                                                                UjUzemora);
                    KézMűveletek.MeglevoMuvelet_Modositas(ADAT);
                }
                else
                {

                    Adat_Eszterga_Muveletek ADAT = new Adat_Eszterga_Muveletek(0,
                                                                               TxtBxMűvelet.Text.ToStrTrim(),
                                                                               (int)CmbxEgység.SelectedItem,
                                                                               TxtBxMennyiNap.Text.ToÉrt_Int(),
                                                                               TxtBxMennyiÓra.Text.ToÉrt_Int(),
                                                                               (ChckBxStátus.Checked ? "True" : "False").ToÉrt_Bool(),
                                                                               UjDatum,
                                                                               UjUzemora);
                    KézMűveletek.Rogzites(ADAT);
                }

                Eszterga_Valtozas?.Invoke();
                TablaListazasMuvelet();
                TablaListazasMuveletUtolag();
                TorlesEllenorzes();
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

        /// <summary>
        /// Ha van kijelölt sor, és az még nincs törölve, akkor a hozzá tartozó rekordot eltávolítja az adatbázisból.
        /// Először ellenőrzi a kijelölést és a sor állapotát (törölt-e), majd végrehajtja a törlést.
        /// </summary>
        private void Btn_Törlés_Click(object sender, EventArgs e)
        {
            try
            {
                int hiba = TxtBxEllenorzes();
                if (hiba >= 90)
                    throw new HibásBevittAdat("Nincsen kiválasztva egy sor sem.");

                if (hiba >= 1)
                    return;

                foreach (DataGridViewRow row in TablaMuvelet.SelectedRows)
                {
                    bool Torolt = (row.Cells[5].Value.ToStrTrim() == "Törölt").ToÉrt_Bool();
                    if (Torolt)
                        throw new HibásBevittAdat("Csak olyan sorokat lehet törölni, amik nincsenek törölve.");
                }

                AdatokMuvelet = Funkcio.Eszterga_KarbantartasFeltolt();

                List<int> rekordok = new List<int>();
                foreach (DataGridViewRow row in TablaMuvelet.SelectedRows)
                    rekordok.Add(row.Cells[0].Value.ToÉrt_Int());

                foreach (int Id in rekordok)
                {
                    Adat_Eszterga_Muveletek rekord = AdatokMuvelet.FirstOrDefault(a => a.ID == Id);
                    if (rekord != null)
                    {
                        Adat_Eszterga_Muveletek ADAT = new Adat_Eszterga_Muveletek(Id);
                        KézMűveletek.Törlés(ADAT, true);
                    }
                }
                Eszterga_Valtozas?.Invoke();
                TablaListazasMuvelet();
                TablaListazasMuveletUtolag();
                TorlesEllenorzes();
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

        /// <summary>
        /// Előkészíti az új rekord felvételéhez szükséges mezőket: új ID-t generál, kiüríti a mezőket, beállítja az alapértékeket,
        /// és lekéri az utolsó üzemóra állását a listából.
        /// </summary>
        private void Btn_ÚjFelvétel_Click(object sender, EventArgs e)
        {
            try
            {
                Btn_Törlés.Visible = false;
                AdatokMuvelet = Funkcio.Eszterga_KarbantartasFeltolt();
                TxtBxId.Text = (AdatokMuvelet.Any() ? AdatokMuvelet.Max(a => a.ID) + 1 : 1).ToStrTrim();
                TxtBxMűvelet.Text = "";
                CmbxEgység.SelectedItem = EsztergaEgyseg.Bekovetkezes;
                TxtBxMennyiNap.Text = "0";
                TxtBxMennyiÓra.Text = "0";
                ChckBxStátus.Checked = false;
                DtmPckrUtolsóDátum.Value = DateTime.Today;
                Adat_Eszterga_Uzemora legutolsoUzemora = (from a in AdatokUzemora
                                                          where !a.Státus
                                                          orderby a.Dátum descending
                                                          select a).FirstOrDefault();

                TxtBxUtolsóÜzemóraÁllás.Text = legutolsoUzemora != null ? legutolsoUzemora.Uzemora.ToStrTrim() : "0";
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

        /// Két kijelölt rekord sorrendjének felcserélése az adatbázisban.
        /// Ellenőrzi, hogy pontosan két sor van-e kijelölve, majd végrehajtja a cserét, frissíti a táblázatot.
        /// </summary>
        private void Btn_Csere_Click(object sender, EventArgs e)
        {
            try
            {
                if (TablaMuvelet.SelectedRows.Count != 2)
                    throw new HibásBevittAdat("A cseréhez 2 sor kijelölésére van szükség");

                List<Adat_Eszterga_Muveletek> rekordok = SorKivalasztas();

                Adat_Eszterga_Muveletek rekord1 = rekordok[0];
                Adat_Eszterga_Muveletek rekord2 = rekordok[1];

                KézMűveletek.MuveletCsere(rekord1, rekord2);
                Rendezes();
                TablaListazasMuvelet();
                TablaListazasMuveletUtolag();
                TorlesEllenorzes();
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

        /// <summary>
        /// Két kijelölt sor sorrendjét felcseréli az adatbázisban úgy, hogy az egyik rekord a másik helyére kerül.
        /// Először ellenőrzi, hogy pontosan két sor van-e kijelölve, majd a cserét végrehajtja, frissíti a listát.
        /// </summary>
        private void Btn_Sorrend_Click(object sender, EventArgs e)
        {
            try
            {
                if (TablaMuvelet.SelectedRows.Count != 2)
                    throw new HibásBevittAdat("A sorrend módosításához 2 sor kijelölésére van szükség");

                List<Adat_Eszterga_Muveletek> rekordok = SorKivalasztas();

                Adat_Eszterga_Muveletek elso = rekordok[1];
                Adat_Eszterga_Muveletek masodik = rekordok[0];
                int ElsoID = elso.ID;
                int MasodikID = masodik.ID;

                KézMűveletek.MuveletSorrend(ElsoID, MasodikID);
                Rendezes();
                TablaListazasMuvelet();
                TablaListazasMuveletUtolag();
                TorlesEllenorzes();
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

        /// <summary>
        /// A táblázat teljes tartalmát Excel fájlba menti, ha van legalább egy sor.
        /// A felhasználó kiválaszthatja a mentés helyét, majd a fájl automatikusan létrejön és megnyílik.
        /// </summary>
        private void Btn_Excel_Click(object sender, EventArgs e)
        {
            if (TablaMuvelet.Rows.Count <= 0) throw new HibásBevittAdat("Nincs sora a táblázatnak!");
            string fajlexc;
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog
            {
                InitialDirectory = "MyDocuments",
                Title = "Teljes tartalom mentése Excel fájlba",
                FileName = $"Eszterga_Karbantartás_Műveletek_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                Filter = "Excel |*.xlsx"
            };
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                fajlexc = SaveFileDialog1.FileName;
            else
                return;
            fajlexc = fajlexc.Substring(0, fajlexc.Length - 5);

            MyE.EXCELtábla(fajlexc, TablaMuvelet, false);
            MessageBox.Show("Elkészült az Excel tábla: " + fajlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

            MyE.Megnyitás($"{fajlexc}.xlsx");
        }

        /// <summary>
        /// A táblázat formázási eseménye során beállítja a "Státusz" oszlop alapján a sorok megjelenítését (pl. törölt sorok színezése).
        /// Csak akkor hajtódik végre, ha a forrás egy megfelelő típusú adatgrid.
        /// </summary>
        private void TáblaMűvelet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (sender is Zuby.ADGV.AdvancedDataGridView tabla)
                ToroltTablaSzinezes(tabla, e, "Státusz");
        }

        /// <summary>
        /// A táblázat sorainak formázását végzi a "Státusz" oszlop alapján.  
        /// Ha a sor törölt, a megjelenítése módosul. Csak akkor történik meg, ha a forrás megfelelő típus.
        /// </summary>
        private void TáblaUtólagMűvelet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (sender is Zuby.ADGV.AdvancedDataGridView tabla)
                ToroltTablaSzinezes(tabla, e, "Státusz");
        }

        private void TáblaUtólagMűvelet_SelectionChanged(object sender, EventArgs e)
        {
            if (TablaUtolagMuvelet.Focused && TablaUtolagMuvelet.SelectedRows.Count == 1)
            {
                TablaNaplo.ClearSelection();
                DtmPckrUtolagos.Value = DateTime.Today;
                TxtBxUtolagMegjegyzes.Text = "";
            }
        }

        /// <summary>
        /// Ellenőrzi, hogy pontosan két sor van-e kijelölve a táblában.
        /// Ha igen, akkor megjeleníti a csere- és sorrendgombokat, egyébként elrejti őket.
        /// </summary>
        private void Tábla_SelectionChanged(object sender, EventArgs e)
        {
            int Sorok = TablaMuvelet.SelectedRows.Count;

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

        /// <summary>
        /// Ha egy sorra kattintanak, a sor adatai betöltődnek a beviteli mezőkbe szerkesztés céljából.
        /// Dátum és enum érték is feldolgozásra kerül.
        /// </summary>
        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = TablaMuvelet.Rows[e.RowIndex];
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
                    TorlesEllenorzes();
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

        /// <summary>
        /// Ha a felhasználó módosítja az utolsó dátum értékét, lekérdezi az ahhoz tartozó üzemóra adatot,
        /// és beírja a megfelelő mezőbe. A jövőbeni dátumokra figyelmeztet.
        /// </summary>
        private void DtmPckrUtolsóDátum_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (frissul) return;

                frissul = true;

                DateTime ValasztottDatum = DtmPckrUtolsóDátum.Value;

                if (ValasztottDatum > DateTime.Today)
                    throw new HibásBevittAdat($"A választott dátum nem lehet később mint a mai nap {DateTime.Today}");

                UzemoraKiolvasasEsBeiras(ValasztottDatum, TxtBxUtolsóÜzemóraÁllás);
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

        /// <summary>
        /// Ha az üzemóra mező értéke megváltozik, annak megfelelő dátumot keres az adatbázisban,
        /// és automatikusan beállítja a dátummezőt.
        /// </summary>
        private void TxtBxUtolsóÜzemóraÁllás_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (frissul) return;

                frissul = true;

                if (!long.TryParse(TxtBxUtolsóÜzemóraÁllás.Text, out long ValasztottUzemora))
                    throw new HibásBevittAdat("Csak pozitív egész szám lehet az üzemóra állásánál.");

                Adat_Eszterga_Uzemora uzemoraRekord = KeresÜzemóra(ValasztottUzemora, DateTime.MinValue, EsztergaEgyseg.Uzemora);

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

        /// <summary>
        /// Amikor a felhasználó megváltoztatja az utólagos dátumot, automatikusan kiolvassa és beírja a megfelelő üzemóra értéket.
        /// </summary>
        private void DtmPckrUtolagos_ValueChanged(object sender, EventArgs e)
        {
            UzemoraKiolvasasEsBeiras(DtmPckrUtolagos.Value, TxtBxUtolagUzemora);
        }

        /// <summary>
        /// Ellenőrzi és rögzíti az utólagos naplózási adatokat, ha a kiválasztott sor érvényes és nincs már rögzítve ugyanarra a napra.
        /// </summary>
        private void BttnUtolag_Modosit_Click(object sender, EventArgs e)
        {
            try
            {
                if (TablaUtolagMuvelet.SelectedRows.Count == 0 && TablaNaplo.SelectedRows.Count == 0)
                    throw new HibásBevittAdat("Kérlek, válassz ki egy sort a listából!");

                if (DtmPckrUtolagos.Value.Date > DateTime.Today)
                    throw new HibásBevittAdat("A kiválasztott dátum nem lehet későbbi, mint a mai dátum.");

                if (TablaUtolagMuvelet.SelectedRows.Count != 0)
                    UjUtolagosNaplozas();
                else if (TablaNaplo.SelectedRows.Count != 0)
                    NaploModositas();

                TablaNaploListazas();
                MessageBox.Show("Sikeres rögzítés a naplóba.", "Rögzítve", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void NaploModositas()
        {
            if (TablaNaplo.SelectedRows.Count == 0)
                throw new HibásBevittAdat("Kérlek, válassz ki legalább egy sort a naplóból!");

            foreach (DataGridViewRow sor in TablaNaplo.SelectedRows)
            {
                int id = sor.Cells["Művelet Sorszáma"].Value.ToÉrt_Int();
                DateTime eredetiDatum = sor.Cells["Utolsó Dátum"].Value.ToÉrt_DaTeTime();

                Adat_Eszterga_Muveletek_Naplo eredeti = AdatokNaplo.FirstOrDefault(
                    a => a.ID == id && a.Utolsó_Dátum.Date == eredetiDatum.Date)
                    ?? throw new HibásBevittAdat($"A(z) {id} azonosítójú naplózott sor nem található a memóriában.");

                DateTime ujDatum = DtmPckrUtolagos.Value.Date;
                long ujUzemora = TxtBxUtolagUzemora.Text.ToÉrt_Long();
                string ujMegjegyzes = TxtBxUtolagMegjegyzes.Text.Trim();

                bool Valtozas =
                    ujDatum != eredeti.Utolsó_Dátum.Date ||
                    ujUzemora != eredeti.Utolsó_Üzemóra_Állás ||
                    ujMegjegyzes != eredeti.Megjegyzés;

                if (!Valtozas)
                    continue;

                Adat_Eszterga_Muveletek_Naplo modositott = new Adat_Eszterga_Muveletek_Naplo(
                    id,
                    eredeti.Művelet,
                    eredeti.Mennyi_Dátum,
                    eredeti.Mennyi_Óra,
                    ujDatum,
                    ujUzemora,
                    ujMegjegyzes,
                    Program.PostásNév.ToStrTrim(),
                    DateTime.Today
                );

                KézNapló.Update(modositott, eredetiDatum);
            }
        }

        private void UjUtolagosNaplozas()
        {
            DateTime datum = DtmPckrUtolagos.Value;
            string megjegyzes = TxtBxUtolagMegjegyzes.Text.Trim();

            if (TxtBxUtolagUzemora.Enabled)
            {
                if (!int.TryParse(TxtBxUtolagUzemora.Text, out int uzemora))
                    throw new HibásBevittAdat("Hibás üzemóra érték! Kérlek, csak számot adj meg.");

                bool sikeres = UjUzemoraHozzaadasa(datum, uzemora, false);

                if (!sikeres)
                    throw new HibásBevittAdat("Az üzemóra rögzítése sikertelen volt.");
            }

            foreach (DataGridViewRow sor in TablaUtolagMuvelet.SelectedRows)
            {
                int id = sor.Cells[0].Value.ToÉrt_Int();
                Adat_Eszterga_Muveletek rekord = AdatokMuvelet.FirstOrDefault(a => a.ID == id)
                    ?? throw new HibásBevittAdat($"A(z) {id} azonosítójú művelet nem található.");

                if (rekord.Státus)
                    throw new HibásBevittAdat("Törölt műveletet nem lehet naplózni.");

                bool VanE = AdatokNaplo.Any(a => a.ID == id && a.Utolsó_Dátum.Date == datum.Date);

                if (VanE)
                    throw new HibásBevittAdat("Erre a dátumra már rögzítve lett ez a feladat egyszer.");
                string muvelet = rekord.Művelet;
                int mennyiNap = rekord.Mennyi_Dátum;
                int mennyiÓra = rekord.Mennyi_Óra;
                DateTime utolsoDatum = DtmPckrUtolagos.Value.Date;
                long utolsoUzemora = TxtBxUtolagUzemora.Text.ToÉrt_Long();
                string rogzito = Program.PostásNév.ToStrTrim();
                DateTime maiDatum = DateTime.Today;

                Adat_Eszterga_Muveletek_Naplo adat = new Adat_Eszterga_Muveletek_Naplo(
                    id,
                    muvelet,
                    mennyiNap,
                    mennyiÓra,
                    utolsoDatum,
                    utolsoUzemora,
                    megjegyzes,
                    rogzito,
                    maiDatum
                );

                KézNapló.EsztergaNaplózás(adat);
            }
        }

        private void TáblaNapló_SelectionChanged(object sender, EventArgs e)
        {
            if (TablaNaplo.Focused && TablaNaplo.SelectedRows.Count == 1)
            {
                TablaUtolagMuvelet.ClearSelection();

                DataGridViewRow sor = TablaNaplo.SelectedRows[0];
                DtmPckrUtolagos.Value = sor.Cells["Utolsó Dátum"].Value.ToÉrt_DaTeTime();
                TxtBxUtolagUzemora.Text = sor.Cells["Utolsó Üzemóra"].Value.ToStrTrim();
                TxtBxUtolagMegjegyzes.Text = sor.Cells["Megjegyzés"].Value.ToStrTrim();
            }
        }

        #endregion

        #region Ablakok
        Ablak_Eszterga_Karbantartás_Üzemóra Uj_ablak_EsztergaUzemora;

        /// <summary>
        /// Megnyitja az Eszterga üzemóra ablakot, ha még nincs megnyitva.  
        /// Ha az ablak már létezik, akkor előtérbe hozza és maximalizálja.
        /// Az ablak bezárásakor visszaállítja a hivatkozást, és frissítést kér a fő táblára, ha adatváltozás történt.
        /// </summary>
        private void Üzemóra_Oldal_Click(object sender, EventArgs e)
        {
            if (Uj_ablak_EsztergaUzemora == null)
            {
                Uj_ablak_EsztergaUzemora = new Ablak_Eszterga_Karbantartás_Üzemóra();
                Uj_ablak_EsztergaUzemora.FormClosed += Új_ablak_EsztergaÜzemóra_Closed;
                Uj_ablak_EsztergaUzemora.Show();
                Uj_ablak_EsztergaUzemora.Eszterga_Változás += TablaListazasMuvelet;
            }
            else
            {
                Uj_ablak_EsztergaUzemora.Activate();
                Uj_ablak_EsztergaUzemora.WindowState = FormWindowState.Maximized;
            }
        }

        /// <summary>
        /// Az Eszterga módosító ablak bezárásakor automatikusan bezárja az üzemóra ablakot is, ha nyitva van.
        /// </summary>
        private void Új_ablak_EsztergaMódosít_Closed(object sender, FormClosedEventArgs e)
        {
            Uj_ablak_EsztergaUzemora?.Close();
        }

        /// <summary>
        /// Az üzemóra ablak bezárásakor nullára állítja a hozzá tartozó hivatkozást,
        /// így lehetővé teszi annak újranyitását.
        /// </summary>
        private void Új_ablak_EsztergaÜzemóra_Closed(object sender, FormClosedEventArgs e)
        {
            Uj_ablak_EsztergaUzemora = null;
        }


        #endregion
    }
}