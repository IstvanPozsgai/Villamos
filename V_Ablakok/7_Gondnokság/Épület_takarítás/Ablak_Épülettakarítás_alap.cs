using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos
{
    public partial class Ablak_épülettakarítás_alap
    {
        readonly Kezelő_Épület_Takarítás_Osztály KézOsztály = new Kezelő_Épület_Takarítás_Osztály();
        readonly Kezelő_Épület_Adattábla KézÉpület = new Kezelő_Épület_Adattábla();
        readonly Kezelő_Takarítás_Opció KézOpció = new Kezelő_Takarítás_Opció();


        List<Adat_Épület_Takarítás_Osztály> AdatokTakOsztály = new List<Adat_Épület_Takarítás_Osztály>();
        List<Adat_Épület_Adattábla> AdatokÉptakarításAdat = new List<Adat_Épület_Adattábla>();
        List<Adat_Takarítás_Opció> AdatokTakOpció = new List<Adat_Takarítás_Opció>();

        readonly string munkalap = "Munka1";
#pragma warning disable IDE0044 // Add readonly modifier
        DataTable AdatTábla = new DataTable();
        DataTable AdatTábla1 = new DataTable();

#pragma warning restore IDE0044 // Add readonly modifier


        #region Alap
        public Ablak_épülettakarítás_alap()
        {
            InitializeComponent();
            Start();
        }

        /// <summary>
        /// Ablak betöltésekor hívódik meg, itt történik a kezdeti beállítások elvégzése.
        /// </summary>

        private void Start()
        {
            try
            {
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }
                else
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }
                Combofeltöltése();
                LapFülek.SelectedIndex = 0;
                Fülekkitöltése();

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

        private void Ablak_épülettakarítás_alap_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Ablakfül változásakor betölti a szükséges lapok alap adatait.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        /// <summary>
        /// Ablak lapfül választott lap betöltésének eseménye
        /// </summary>
        private void Fülekkitöltése()
        {
            switch (LapFülek.SelectedIndex)
            {
                case 0:
                    {
                        Osztályürítés();
                        Osztálykiirás();
                        AcceptButton = Osztály_rögzít;
                        break;
                    }
                case 1:
                    {
                        Helységürítés();
                        Helységlistáz();
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                    {
                        OpcióListaFeltöltés();
                        break;
                    }
            }
        }

        /// <summary>
        /// Jogosultságok kiosztása a gombokhoz, hogy ki mit tud csinálni az ablakon.
        /// </summary>
        private void Jogosultságkiosztás()
        {
            int melyikelem;
            // ide kell az összes gombot tenni amit szabályozni akarunk false
            Részletes_Kuka.Enabled = false;
            Részletes_feljebb.Enabled = false;
            Részletes_rögzít.Enabled = false;
            Helység_feljebb.Enabled = false;
            Osztály_rögzít.Enabled = false;
            Osztály_törlés.Enabled = false;
            Osztály_feljebb.Enabled = false;
            Adatok_beolvasása.Enabled = false;
            Opció_OK.Enabled = false;


            melyikelem = 235;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Osztály_rögzít.Enabled = true;
                Osztály_törlés.Enabled = true;
                Osztály_feljebb.Enabled = true;
                Adatok_beolvasása.Enabled = true;
                Opció_OK.Enabled = true;
            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Helység_feljebb.Enabled = true;
            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
                Részletes_Kuka.Enabled = true;
                Részletes_feljebb.Enabled = true;
                Részletes_rögzít.Enabled = true;
            }
        }

        /// <summary>
        /// Telephelyek feltöltése a comboboxba, hogy mely telephelyek vannak a rendszerben.
        /// </summary>
        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim(); }
                else if (Program.PostásTelephely.Contains("törzs"))
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim(); }
                else
                { Cmbtelephely.Text = Program.PostásTelephely; }


                Cmbtelephely.Enabled = Program.Postás_Vezér;
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
        private void TelephelyekFeltöltéseÚj()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Adat in GombLathatosagKezelo.Telephelyek(this.Name))
                    Cmbtelephely.Items.Add(Adat.Trim());
                //Alapkönyvtárat beállítjuk 
                if (Cmbtelephely.Items.Cast<string>().Contains(Program.PostásTelephely))
                    Cmbtelephely.Text = Program.PostásTelephely;
                else
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim();
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
        /// Súgó gomb megnyomásakor megnyitja a súgó fájlt, ha létezik.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Épület_törzsadatok.html";
                MyF.Megnyitás(hely);
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


        #region Takarítási osztály
        /// <summary>
        /// A beviteli mezőket alaphelyzetbe állítjuk, hogy új osztályt lehessen rögzíteni.
        /// </summary>
        private void Osztályürítés()
        {
            Sorszám.Text = "";
            Osztálynév.Text = "";
            E1ár.Text = "0";
            E2ár.Text = "0";
            E3ár.Text = "0";
        }

        private void Tábla1_író()
        {
            try
            {
                Tábla1.Visible = false;
                Tábla1.CleanFilterAndSort();
                Tábla1Fejléc();
                Tábla1Tartalom();
                Tábla1.DataSource = AdatTábla1;
                Tábla1OszlopSzélesség();
                Tábla1.Visible = true;
                Tábla1.Refresh();
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

        private void Tábla1OszlopSzélesség()
        {
            Tábla1.Columns["Sorszám"].Width = 150;
            Tábla1.Columns["Osztály"].Width = 400;
            Tábla1.Columns["E1 takarítási ár"].Width = 200;
            Tábla1.Columns["E2 takarítási ár"].Width = 200;
            Tábla1.Columns["E3 takarítási ár"].Width = 200;
        }

        private void Tábla1Tartalom()
        {
            List<Adat_Épület_Takarítás_Osztály> Adatok = KézOsztály.Lista_Adatok(Cmbtelephely.Text.Trim());
            Adatok = Adatok.Where(a => a.Státus == false).ToList();
            AdatTábla1.Clear();
            foreach (Adat_Épület_Takarítás_Osztály rekord in Adatok)
            {
                DataRow Soradat = AdatTábla1.NewRow();

                Soradat["Sorszám"] = rekord.Id;
                Soradat["Osztály"] = rekord.Osztály;
                Soradat["E1 takarítási ár"] = rekord.E1Ft;
                Soradat["E2 takarítási ár"] = rekord.E2Ft;
                Soradat["E3 takarítási ár"] = rekord.E3Ft;
                AdatTábla1.Rows.Add(Soradat);
            }
        }

        private void Tábla1Fejléc()
        {
            AdatTábla1.Columns.Clear();
            AdatTábla1.Columns.Add("Sorszám", typeof(int));
            AdatTábla1.Columns.Add("Osztály", typeof(string));
            AdatTábla1.Columns.Add("E1 takarítási ár", typeof(double));
            AdatTábla1.Columns.Add("E2 takarítási ár", typeof(double));
            AdatTábla1.Columns.Add("E3 takarítási ár", typeof(double));
        }


        /// <summary>
        /// Táblát felöltjük az osztály adatokkal
        /// </summary>
        private void Osztálykiirás()
        {
            try
            {
                Tábla1_író();
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
        /// A táblázat cellájának kattintásakor beállítja a beviteli mezőkbe az adott osztály adatait.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tábla1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Sorszám.Text = Tábla1.Rows[e.RowIndex].Cells[0].Value.ToString();
            Osztálynév.Text = Tábla1.Rows[e.RowIndex].Cells[1].Value.ToString();
            E1ár.Text = Tábla1.Rows[e.RowIndex].Cells[2].Value.ToString();
            E2ár.Text = Tábla1.Rows[e.RowIndex].Cells[3].Value.ToString();
            E3ár.Text = Tábla1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        /// <summary>
        /// Osztály új gomb megnyomásakor az osztály beviteli mezőket üríti, hogy új osztályt lehessen rögzíteni.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Osztály_Új_Click(object sender, EventArgs e)
        {
            Osztályürítés();
        }

        /// <summary>
        /// Rögzíti és/vagy módosítja az osztály adatait a beviteli mezőkben megadott értékek alapján.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Osztály_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Osztálynév.Text.Trim() == "") throw new HibásBevittAdat("Oszálynevet meg kell adni.");
                if (!double.TryParse(E1ár.Text, out double E1Ár) && E1Ár < 0) throw new HibásBevittAdat("Az E1 takarítási árnak számnak kell lennie és nem lehet negatív szám.");
                if (!double.TryParse(E2ár.Text, out double E2Ár) && E2Ár < 0) throw new HibásBevittAdat("Az E2 takarítási árnak számnak kell lennie és nem lehet negatív szám.");
                if (!double.TryParse(E3ár.Text, out double E3Ár) && E3Ár < 0) throw new HibásBevittAdat("Az E3 takarítási árnak számnak kell lennie és nem lehet negatív szám.");

                AdatokTakOsztály = KézOsztály.Lista_Adatok(Cmbtelephely.Text.Trim());

                if (!int.TryParse(Sorszám.Text, out int sorszám))
                {
                    sorszám = 1;
                    if (AdatokTakOsztály.Count > 0)
                        sorszám = AdatokTakOsztály.Max(a => a.Id) + 1;
                }

                Adat_Épület_Takarítás_Osztály AdatTakOsztály = (from a in AdatokTakOsztály
                                                                where a.Id == sorszám
                                                                select a).FirstOrDefault();
                Adat_Épület_Takarítás_Osztály ADAT = new Adat_Épület_Takarítás_Osztály(
                                    sorszám,
                                    Osztálynév.Text.Trim(),
                                    E1Ár,
                                    E2Ár,
                                    E3Ár,
                                    false);
                if (AdatTakOsztály != null)
                    KézOsztály.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                else
                    KézOsztály.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);

                Osztályürítés();
                Osztálykiirás();
                Combofeltöltése();
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
        /// Az oszály törlés gomb megnyomásakor törli a kijelölt osztályt a táblázatból és az adatbázisból.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Osztálytörlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Sorszám.Text, out int sorszám)) return;

                AdatokTakOsztály = KézOsztály.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Épület_Takarítás_Osztály AdatTakOsztály = (from a in AdatokTakOsztály
                                                                where a.Id == sorszám
                                                                select a).FirstOrDefault();

                if (AdatTakOsztály != null) KézOsztály.Törlés(Cmbtelephely.Text.Trim(), sorszám);

                Osztályürítés();
                Osztálykiirás();
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
        /// Eggyel előrébb rakja a választott elemet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Felljebb_Click(object sender, EventArgs e)
        {
            try
            {
                if (Sorszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy elem sem.");
                if (!int.TryParse(Sorszám.Text.Trim(), out int sorszám)) throw new HibásBevittAdat("A sorszám mezőben számot kell megadni.");

                KézOsztály.Csere(Cmbtelephely.Text.Trim(), sorszám);
                Osztályürítés();
                Osztálykiirás();
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
        /// Táblázat elemeit Excel fájlba exportálja, ha van adat a táblázatban.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Oszály_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla1.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Épület_osztály_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.DataGridViewToXML(fájlexc, Tábla1);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.Megnyitás(fájlexc);
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
        /// Beviteli Excel táblázat beolvasása, amelyben az osztályok adatai vannak, melyet a felhasználó állított be.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Adatok_beolvasása_Click(object sender, EventArgs e)
        {
            try
            {
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Épület takarítási árak betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;
                
                MyX.ExcelMegnyitás(fájlexc);
                MyX.Munkalap_aktív(munkalap);
                int sor = 2;
                List<Adat_Épület_Takarítás_Osztály> AdatokGyM = new List<Adat_Épület_Takarítás_Osztály>();
                List<Adat_Épület_Takarítás_Osztály> AdatokGyR = new List<Adat_Épület_Takarítás_Osztály>();
                while (MyX.Beolvas(munkalap,$"A{sor}").Trim() != "_")
                {
                    string osztály = MyX.Beolvas(munkalap, "A" + sor.ToString()).Trim();
                    double E3 = double.TryParse(MyX.Beolvas(munkalap, $"D{sor}").Trim(), out double E3P) ? E3P : 0;
                    double E1 = double.TryParse(MyX.Beolvas(munkalap, $"B{sor}").Trim(), out double E1P) ? E1P : 0;
                    double E2 = double.TryParse(MyX.Beolvas(munkalap, $"C{sor}").Trim(), out double E2P) ? E2P : 0;

                    Adat_Épület_Takarítás_Osztály AdatTakOsztály = (from a in AdatokTakOsztály
                                                                    where a.Osztály == osztály.Trim()
                                                                    select a).FirstOrDefault();
                    Adat_Épület_Takarítás_Osztály ADAT = new Adat_Épület_Takarítás_Osztály(
                                    0, // sorszámot nem tudjuk, mert új lesz
                                    osztály.Trim(),
                                    E1,
                                    E2,
                                    E3,
                                    false);
                    if (AdatTakOsztály != null)
                        AdatokGyM.Add(ADAT);
                    else
                        AdatokGyR.Add(ADAT);
                    sor++;
                }
                if (AdatokGyR.Count > 1) KézOsztály.Rögzítés(Cmbtelephely.Text.Trim(), AdatokGyR);
                if (AdatokGyM.Count > 1) KézOsztály.Módosítás(Cmbtelephely.Text.Trim(), AdatokGyM);
                MyX.ExcelBezárás();
                MessageBox.Show("Az Excel tábla feldolgozása megtörtént. !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Osztálykiirás();

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
        /// Beviteli táblakészítés gomb megnyomásakor létrehozza az Excel táblát, amibe be lehet írni az adatokat.
        /// Majd ezt követően ezzel a fájllal lehet feltölteni az adatokat az adatbázisba.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Beviteli_táblakészítés_Click(object sender, EventArgs e)
        {
            try
            {
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Excel tábla készítés adatok beolvasásához",
                    FileName = $"Beolvasó_Takarítás_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyX.ExcelLétrehozás();

                MyX.Kiir("Megnevezés", "A1");
                MyX.Kiir("E1 Egységár", "B1");
                MyX.Kiir("E2 Egységár", "C1");
                MyX.Kiir("E3 Egységár", "D1");
                int sor = 1;
                //kitöljük az megnevezéseket
                List<Adat_Épület_Takarítás_Osztály> Adatok = KézOsztály.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adatok = (from a in Adatok
                          where a.Státus == false
                          orderby a.Id
                          select a).ToList();
                foreach (Adat_Épület_Takarítás_Osztály rekord in Adatok)
                {
                    sor++;
                    MyX.Kiir(rekord.Osztály, "A" + sor);
                }

                MyX.Oszlopszélesség(munkalap, "A:D");
                MyX.Rácsoz(munkalap, "a1:D" + sor);
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    NyomtatásiTerület = "A1:D" + sor,
                    IsmétlődőSorok = "",       
                    IsmétlődőOszlopok = "",
                    Álló = true,              

                    LapSzéles = 1,
                    LapMagas = 1,
                    Papírméret = "A4",
                    BalMargó = 15,
                    JobbMargó = 15,
                    FelsőMargó = 20,
                    AlsóMargó = 20,
                    FejlécMéret = 13,
                    LáblécMéret = 13,

                    FejlécKözép = Program.PostásNév.Trim(),
                    FejlécJobb = DateTime.Now.ToString("yyyy.MM.dd HH:mm"),
                    LáblécKözép = "&P/&N",

                    FüggKözép = false,
                    VízKözép = false
                };

                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);

                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();
                MyF.Megnyitás(fájlexc);

                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.Megnyitás(fájlexc + ".xlsx");
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


        #region Helység lista
        /// <summary>
        /// Helyiség adatokat listázza.
        /// </summary>
        private void Helységlistáz()
        {
            try
            {
                List<Adat_Épület_Adattábla> Adatok = KézÉpület.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adatok = (from a in Adatok
                          where a.Státus == false
                          orderby a.ID
                          select a).ToList();
                Tábla2.Rows.Clear();
                Tábla2.Columns.Clear();
                Tábla2.Refresh();
                Tábla2.Visible = false;
                Tábla2.ColumnCount = 15;

                // fejléc elkészítése
                Tábla2.Columns[0].HeaderText = "Sorszám";
                Tábla2.Columns[0].Width = 100;
                Tábla2.Columns[1].HeaderText = "Megnevezés";
                Tábla2.Columns[1].Width = 450;
                Tábla2.Columns[2].HeaderText = "Osztály";
                Tábla2.Columns[2].Width = 350;
                Tábla2.Columns[3].HeaderText = "Méret";
                Tábla2.Columns[3].Width = 100;
                Tábla2.Columns[4].HeaderText = "helységkód";
                Tábla2.Columns[4].Width = 130;
                Tábla2.Columns[5].HeaderText = "E1évdb";
                Tábla2.Columns[5].Width = 100;
                Tábla2.Columns[6].HeaderText = "E2évdb";
                Tábla2.Columns[6].Width = 100;
                Tábla2.Columns[7].HeaderText = "E3évdb";
                Tábla2.Columns[7].Width = 100;
                Tábla2.Columns[8].HeaderText = "Kezd";
                Tábla2.Columns[8].Width = 100;
                Tábla2.Columns[9].HeaderText = "Végez";
                Tábla2.Columns[9].Width = 100;
                Tábla2.Columns[10].HeaderText = "Ellenőrneve";
                Tábla2.Columns[10].Width = 250;
                Tábla2.Columns[11].HeaderText = "Ellenőremail";
                Tábla2.Columns[11].Width = 250;
                Tábla2.Columns[12].HeaderText = "Ellenőrtelefonszám";
                Tábla2.Columns[12].Width = 200;
                Tábla2.Columns[13].HeaderText = "Kapcsolthelység";
                Tábla2.Columns[13].Width = 200;
                Tábla2.Columns[14].HeaderText = "Szemetes";
                Tábla2.Columns[14].Width = 100;

                // kiirjuk a tartalmat
                foreach (Adat_Épület_Adattábla rekord in Adatok)
                {

                    Tábla2.RowCount++;
                    int i = Tábla2.RowCount - 1;
                    Tábla2.Rows[i].Cells[0].Value = rekord.ID;
                    Tábla2.Rows[i].Cells[1].Value = rekord.Megnevezés.Trim();
                    Tábla2.Rows[i].Cells[2].Value = rekord.Osztály.Trim();
                    Tábla2.Rows[i].Cells[3].Value = rekord.Méret;
                    Tábla2.Rows[i].Cells[4].Value = rekord.Helységkód.Trim();
                    Tábla2.Rows[i].Cells[5].Value = rekord.E1évdb;
                    Tábla2.Rows[i].Cells[6].Value = rekord.E2évdb;
                    Tábla2.Rows[i].Cells[7].Value = rekord.E3évdb;
                    Tábla2.Rows[i].Cells[8].Value = rekord.Kezd.Trim();
                    Tábla2.Rows[i].Cells[9].Value = rekord.Végez.Trim();
                    Tábla2.Rows[i].Cells[10].Value = rekord.Ellenőrneve.Trim();
                    Tábla2.Rows[i].Cells[11].Value = rekord.Ellenőremail.Trim();
                    Tábla2.Rows[i].Cells[12].Value = rekord.Ellenőrtelefonszám.Trim();
                    Tábla2.Rows[i].Cells[13].Value = rekord.Kapcsolthelység.Trim();
                    Tábla2.Rows[i].Cells[14].Value = rekord.Szemetes ? "Van" : "Nincs";
                }
                Tábla2.Visible = true;
                Tábla2.Refresh();
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

        private void Helység_frissít_Click(object sender, EventArgs e)
        {
            Helységlistáz();
        }

        private void Tábla2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Hsorszám.Text = Tábla2.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
                Hmegnevezés.Text = Tábla2.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
                Combo1.Text = Tábla2.Rows[e.RowIndex].Cells[2].Value.ToStrTrim();
                Hméret.Text = Tábla2.Rows[e.RowIndex].Cells[3].Value.ToStrTrim();
                Hhelyiségkód.Text = Tábla2.Rows[e.RowIndex].Cells[4].Value.ToStrTrim();
                He1évdb.Text = Tábla2.Rows[e.RowIndex].Cells[5].Value.ToStrTrim();
                He2évdb.Text = Tábla2.Rows[e.RowIndex].Cells[6].Value.ToStrTrim();
                He3évdb.Text = Tábla2.Rows[e.RowIndex].Cells[7].Value.ToStrTrim();
                Hkezd.Text = Tábla2.Rows[e.RowIndex].Cells[8].Value.ToStrTrim();
                Hvégez.Text = Tábla2.Rows[e.RowIndex].Cells[9].Value.ToStrTrim();
                Hellenőrneve.Text = Tábla2.Rows[e.RowIndex].Cells[10].Value.ToStrTrim();
                Hellenőremail.Text = Tábla2.Rows[e.RowIndex].Cells[11].Value.ToStrTrim();
                Hellenőrtelefon.Text = Tábla2.Rows[e.RowIndex].Cells[12].Value.ToStrTrim();
                Kapcsolthelység.Text = Tábla2.Rows[e.RowIndex].Cells[13].Value.ToStrTrim();
                Check1.Checked = Tábla2.Rows[e.RowIndex].Cells[14].Value.ToStrTrim() == "Van";
                LapFülek.SelectedIndex = 2;
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

        private void Helység_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla2.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Helyiség_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.DataGridViewToXML(fájlexc, Tábla2);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.Megnyitás(fájlexc);
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

        private void Helységürítés()
        {
            Hsorszám.Text = "";
            Combo1.Text = "";
            Hméret.Text = 0.ToString();
            Hkezd.Text = "";
            Hvégez.Text = "";
            Hellenőremail.Text = "";
            Hellenőrneve.Text = "";
            Hellenőrtelefon.Text = "";
            He1évdb.Text = 0.ToString();
            He2évdb.Text = 0.ToString();
            He3évdb.Text = 0.ToString();
            Hmegnevezés.Text = "";
            Hhelyiségkód.Text = "";
            Kapcsolthelység.Text = "";
            Check1.Checked = false;
        }

        private void Helység_feljebb_Click(object sender, EventArgs e)
        {
            Elől_teszi();
        }

        private void Elől_teszi()
        {
            try
            {
                if (Hsorszám.Text.Trim() == "") throw new HibásBevittAdat("A sorszámot ki kell választani.");
                if (!int.TryParse(Hsorszám.Text.Trim(), out int sorszám)) throw new HibásBevittAdat("A sorszámnak számnak kell lennie.");
                if (sorszám == 1) throw new HibásBevittAdat("Az első elemet nem lehet előrébb tenni.");
                KézÉpület.Csere(Cmbtelephely.Text.Trim(), sorszám);
                Helységlistáz();
                LapFülek.SelectedIndex = 1;
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

        #region Részletes lap
        private void Részletes_feljebb_Click(object sender, EventArgs e)
        {
            Elől_teszi();
        }


        private void Combofeltöltése()
        {
            try
            {
                AdatokTakOsztály = KézOsztály.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokTakOsztály = (from a in AdatokTakOsztály
                                    where a.Státus == false
                                    orderby a.Id
                                    select a).ToList();
                foreach (Adat_Épület_Takarítás_Osztály Elem in AdatokTakOsztály)
                    Combo1.Items.Add(Elem.Osztály);

                Combo1.Refresh();
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


        private void Részletes_Kuka_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Hsorszám.Text.Trim(), out int Sorszám)) return;

                List<Adat_Épület_Adattábla> AdatokÉptakarításAdat = KézÉpület.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Épület_Adattábla AdatÉptakarításAdat = (from a in AdatokÉptakarításAdat
                                                             where a.ID == Sorszám
                                                             select a).FirstOrDefault();
                if (AdatÉptakarításAdat != null) KézÉpület.Módosítás(Cmbtelephely.Text.Trim(), Sorszám);

                Helységlistáz();
                Helységürítés();
                LapFülek.SelectedIndex = 1;
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



        private void Részletes_Új_Click(object sender, EventArgs e)
        {
            Helységürítés();
        }

        private void Részletes_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Hmegnevezés.Text.Trim() == "") throw new HibásBevittAdat("A Megnevezés mezőt ki kell tölteni.");
                if (Combo1.Text.Trim() == "") throw new HibásBevittAdat("A Osztály mezőt ki kell tölteni.");
                if (Hméret.Text.Trim() == "") throw new HibásBevittAdat("A Méret mezőt ki kell tölteni.");
                if (He1évdb.Text.Trim() == "") throw new HibásBevittAdat("Az E1 éves mennyiség mezőt ki kell tölteni.");
                if (He2évdb.Text.Trim() == "") throw new HibásBevittAdat("Az E2 éves mennyiség mezőt ki kell tölteni.");
                if (He3évdb.Text.Trim() == "") throw new HibásBevittAdat("Az E3 éves mennyiség mezőt ki kell tölteni.");
                if (Hkezd.Text.Trim() == "") throw new HibásBevittAdat("Az Kezd mezőt ki kell tölteni.");
                if (Hvégez.Text.Trim() == "") throw new HibásBevittAdat("Az Végez mezőt ki kell tölteni.");

                if (Hellenőrneve.Text.Trim() == "") throw new HibásBevittAdat("Az Ellenőr neve mezőt ki kell tölteni.");
                if (Hellenőremail.Text.Trim() == "") throw new HibásBevittAdat("Az Ellenőr e-mail mezőt ki kell tölteni.");
                if (Hellenőrtelefon.Text.Trim() == "") throw new HibásBevittAdat("Az Ellenőr telefonszáma mezőt ki kell tölteni.");

                if (!int.TryParse(He1évdb.Text.Trim(), out int E1)) throw new HibásBevittAdat("Az E1 éves mennyiségnek egész számnak kell lennie.");
                if (!int.TryParse(He2évdb.Text.Trim(), out int E2)) throw new HibásBevittAdat("Az E2 éves mennyiségnek egész számnak kell lennie.");
                if (!int.TryParse(He3évdb.Text.Trim(), out int E3)) throw new HibásBevittAdat("Az E3 éves mennyiségnek egész számnak kell lennie.");
                if (!double.TryParse(Hméret.Text.Trim(), out double HelyMéret)) throw new HibásBevittAdat("A Méret mezőnek számnak kell lennie.");

                AdatokÉptakarításAdat = KézÉpület.Lista_Adatok(Cmbtelephely.Text.Trim());

                if (!int.TryParse(Hsorszám.Text, out int hsorszám)) hsorszám = 0;
                Adat_Épület_Adattábla AdatÉptakarításAdat = (from a in AdatokÉptakarításAdat
                                                             where a.ID == hsorszám
                                                             select a).FirstOrDefault();
                Adat_Épület_Adattábla ADAT = new Adat_Épület_Adattábla(
                            hsorszám,
                            Hmegnevezés.Text.Trim(),
                            Combo1.Text.Trim(),
                            HelyMéret,
                            Hsorszám.Text.Trim(),
                            false, // státusz
                            E1,
                            E2,
                            E3,
                            Hkezd.Text.Trim(),
                            Hvégez.Text.Trim(),
                            Hellenőremail.Text.Trim(),
                            Hellenőrneve.Text.Trim(),
                            Hellenőrtelefon.Text.Trim(),
                            Check1.Checked, // szemetes
                            Kapcsolthelység.Text.Trim());

                if (AdatÉptakarításAdat == null)
                    KézÉpület.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);
                else
                    KézÉpület.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                Helységlistáz();
                LapFülek.SelectedIndex = 1;
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


        #region Opcionális
        private void OpcióListaFeltöltés()
        {
            try
            {
                AdatokTakOpció.Clear();
                AdatokTakOpció = KézOpció.Lista_Adatok();
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

        private void Opció_Frissít_Click(object sender, EventArgs e)
        {
            OpcióListaFeltöltés();
            OpcióTáblaListázás();
        }

        private void OpcióTáblaListázás()
        {
            try
            {
                AdatTábla.Clear();
                Opció_Tábla.CleanFilterAndSort();
                ABFejléc();
                ABFeltöltése();
                Opció_Tábla.DataSource = AdatTábla;
                ABOszlopSzélesség();
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

        private void ABFejléc()
        {
            try
            {
                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Sorszám", typeof(int));
                AdatTábla.Columns.Add("Megnevezés");
                AdatTábla.Columns.Add("Mennyisége");
                AdatTábla.Columns.Add("Ár");
                AdatTábla.Columns.Add("Kezdet", typeof(DateTime));
                AdatTábla.Columns.Add("Vég", typeof(DateTime));

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

        private void ABFeltöltése()
        {
            try
            {
                AdatTábla.Clear();
                foreach (Adat_Takarítás_Opció rekord in AdatokTakOpció)
                {
                    DataRow Soradat = AdatTábla.NewRow();
                    Soradat["Sorszám"] = rekord.Id;
                    Soradat["Megnevezés"] = rekord.Megnevezés;
                    Soradat["Mennyisége"] = rekord.Mennyisége;
                    Soradat["Ár"] = rekord.Ár;
                    Soradat["Kezdet"] = rekord.Kezdet;
                    Soradat["Vég"] = rekord.Vég;
                    AdatTábla.Rows.Add(Soradat);
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

        private void ABOszlopSzélesség()
        {
            Opció_Tábla.Columns["Sorszám"].Width = 100;
            Opció_Tábla.Columns["Megnevezés"].Width = 400;
            Opció_Tábla.Columns["Mennyisége"].Width = 150;
            Opció_Tábla.Columns["Ár"].Width = 150;
            Opció_Tábla.Columns["Kezdet"].Width = 150;
            Opció_Tábla.Columns["Vég"].Width = 150;
        }

        private void Opció_Új_Click(object sender, EventArgs e)
        {
            Opció_Id.Text = "";
            Opció_Megnevezés.Text = "";
            Opció_Mennyisége.Text = "";
            Opció_Ár.Text = "";
            Opció_Kezdet.Value = new DateTime(1900, 1, 1);
            Opció_Vég.Value = new DateTime(1900, 1, 1);
        }

        private void Opció_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Adat_Takarítás_Opció Elem = AdatokTakOpció.Where(a => a.Id == Opció_Tábla.Rows[e.RowIndex].Cells[0].Value.ToÉrt_Int()).FirstOrDefault();
            if (Elem != null)
            {
                Opció_Id.Text = Elem.Id.ToString();
                Opció_Megnevezés.Text = Elem.Megnevezés;
                Opció_Mennyisége.Text = Elem.Mennyisége;
                Opció_Ár.Text = Elem.Ár.ToString();
                Opció_Kezdet.Value = Elem.Kezdet;
                Opció_Vég.Value = Elem.Vég;
            }
        }

        private void Opció_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!double.TryParse(Opció_Ár.Text, out double Ár)) throw new HibásBevittAdat("Az Ár mezőben számnak kell lennie.");
                if (Opció_Megnevezés.Text.Trim() == "") throw new HibásBevittAdat("Megnevezés mezőt ki kell tölteni.");
                if (Opció_Mennyisége.Text.Trim() == "") throw new HibásBevittAdat("Mennyiség egység mezőt ki kell tölteni.");

                OpcióListaFeltöltés();

                if (int.TryParse(Opció_Id.Text, out int ID))
                {
                    KézOpció.Módosít(new Adat_Takarítás_Opció(ID,
                                                                             Opció_Megnevezés.Text.Trim(),
                                                                             Opció_Mennyisége.Text.Trim(),
                                                                             Ár,
                                                                             Opció_Kezdet.Value,
                                                                             Opció_Vég.Value));
                }
                else
                {
                    if (AdatokTakOpció.Count == 0)
                        ID = 1;
                    else
                        ID = AdatokTakOpció.Max(a => a.Id) + 1;
                    KézOpció.Rögzít(new Adat_Takarítás_Opció(ID,
                                                         MyF.Szöveg_Tisztítás(Opció_Megnevezés.Text.Trim()),
                                                         Opció_Mennyisége.Text.Trim(),
                                                         Ár,
                                                         Opció_Kezdet.Value,
                                                         Opció_Vég.Value));
                }
                OpcióListaFeltöltés();
                OpcióTáblaListázás();
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

        private void Opció_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Opció_Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Opcionális_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.DataGridViewToXML(fájlexc, Opció_Tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.Megnyitás(fájlexc);
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

        private void Másol_Click(object sender, EventArgs e)
        {
            try
            {
                if (Opció_Tábla.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kiválasztva másolnadó sor.");
                int ID = AdatokTakOpció.Max(a => a.Id);
                List<Adat_Takarítás_Opció> AdatokGy = new List<Adat_Takarítás_Opció>();
                for (int i = 0; i < Opció_Tábla.SelectedRows.Count; i++)
                {
                    ID++;
                    Adat_Takarítás_Opció ADAT = new Adat_Takarítás_Opció(ID,
                                                     Opció_Tábla.SelectedRows[i].Cells["Megnevezés"].Value.ToString(),
                                                     Opció_Tábla.SelectedRows[i].Cells["Mennyisége"].Value.ToString(),
                                                     0,
                                                     Opció_Kezdet.Value,
                                                     Opció_Vég.Value);
                    AdatokGy.Add(ADAT);
                }
                if (AdatokGy.Count > 0) KézOpció.Rögzít(AdatokGy);
                OpcióListaFeltöltés();
                OpcióTáblaListázás();
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

        private void Lezárja_Click(object sender, EventArgs e)
        {
            try
            {
                if (Opció_Tábla.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kiválasztva másolnadó sor.");

                List<Adat_Takarítás_Opció> AdatokGy = new List<Adat_Takarítás_Opció>();
                for (int i = 0; i < Opció_Tábla.SelectedRows.Count; i++)
                {
                    Adat_Takarítás_Opció ADAT = new Adat_Takarítás_Opció(
                                                     Opció_Tábla.SelectedRows[i].Cells["Sorszám"].Value.ToÉrt_Int(),
                                                     Opció_Vég.Value);
                    AdatokGy.Add(ADAT);
                }
                if (AdatokGy.Count > 0) KézOpció.Módosít(AdatokGy);
                OpcióListaFeltöltés();
                OpcióTáblaListázás();
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

        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
                if (Cmbtelephely.Text.Trim() == "") return;
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                else
                {

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


    }
}