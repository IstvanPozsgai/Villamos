using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Ablakok;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos
{

    public partial class Ablak_kidobó
    {

        #region Kezelők
        readonly Kezelő_Kidobó KézKidobó = new Kezelő_Kidobó();
        readonly Kezelő_Kidobó_Segéd KézSegéd = new Kezelő_Kidobó_Segéd();
        readonly Kezelő_Kiegészítő_Sérülés KézSérülés = new Kezelő_Kiegészítő_Sérülés();
        readonly Kezelő_Telep_Kiegészítő_Kidobó KézKiegdob = new Kezelő_Telep_Kiegészítő_Kidobó();
        readonly Kezelő_Kidobó_Változat KézVáltozat = new Kezelő_Kidobó_Változat();
        readonly Kezelő_Telep_Kieg_Fortetípus KézFortetípus = new Kezelő_Telep_Kieg_Fortetípus();
        #endregion

        readonly Beállítás_Betű BeBetű10 = new Beállítás_Betű { Név = "Arial", Méret = 10 };
        readonly Beállítás_Betű BeBetű10V = new Beállítás_Betű { Név = "Arial", Méret = 10, Vastag = true };
        readonly Beállítás_Betű BeBetű12 = new Beállítás_Betű { Név = "Arial", Méret = 12 };
        readonly Beállítás_Betű BeBetű14 = new Beállítás_Betű { Név = "Arial", Méret = 14 };
        readonly Beállítás_Betű BeBetű14V = new Beállítás_Betű { Név = "Arial", Méret = 14, Vastag = true };
        readonly Beállítás_Betű BeBetű16 = new Beállítás_Betű { Név = "Arial", Méret = 16 };
        readonly Beállítás_Betű BeBetű16V = new Beállítás_Betű { Név = "Arial", Méret = 16, Vastag = true };
        readonly Beállítás_Betű BeBetű36 = new Beállítás_Betű { Név = "Arial", Méret = 36 };
        readonly Beállítás_Betű BeBetű30 = new Beállítás_Betű { Név = "Arial", Méret = 30 };

        Ablak_Kidobó_Ismétlődő Új_Ablak_Kidobó_Ismétlődő;
        Ablak_Kidobó_Napi Új_Ablak_Kidobó_Napi;
        Ablak_Kereső Új_Ablak_Kereső;
        string AlsóPanels = "_";
        Adat_Kidobó_Segéd Segéd_adat = null;
        Adat_Kidobó Napi_Adat = null;
        readonly List<string> Forte_típus = new List<string>();

        #region Alap
        public Ablak_kidobó()
        {
            InitializeComponent();
            Start();
        }

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
                Dátum.Value = DateTime.Today;
                Alsópanelkitöltés();
                VáltozatCombofeltölt();
                Gombok();
                Label18.Text = "";

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

        private void Ablak_kidobó_Load(object sender, EventArgs e)
        {

        }

        private void Jogosultságkiosztás()
        {
            int melyikelem;
            // ide kell az összes gombot tenni amit szabályozni akarunk false
            ForteBetöltés.Visible = false;

            melyikelem = 178;

            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                ForteBetöltés.Visible = true;
            }
            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            {
            }

            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
            }
        }

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Kidobó.html";
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

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                List<Adat_Kiegészítő_Sérülés> Adatok = KézSérülés.Lista_Adatok();
                foreach (Adat_Kiegészítő_Sérülés rekord in Adatok)
                    Cmbtelephely.Items.Add(rekord.Név);

                Cmbtelephely.Refresh();
                if (Program.PostásTelephely == "Főmérnökség" || Program.PostásTelephely.Contains("törzs"))
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim();
                else
                    Cmbtelephely.Text = Program.PostásTelephely;

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


        private void Ablak_kidobó_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Becsukjuk az kiegészítő ablakokat
            Új_Ablak_Kidobó_Ismétlődő?.Close();
            Új_Ablak_Kereső?.Close();
            Új_Ablak_Kidobó_Napi?.Close();
        }

        private void Ablak_kidobó_KeyDown(object sender, KeyEventArgs e)
        {
            //Esc
            if ((int)e.KeyCode == 27)
            {
                //Becsukjuk az ablakokat
                Új_Ablak_Kidobó_Ismétlődő?.Close();
                Új_Ablak_Kidobó_Napi?.Close();
                Új_Ablak_Kereső?.Close();
            }

            //Ctrl+F
            if (e.Control && e.KeyCode == Keys.F)
            {
                Keresés_metódus();
            }
        }
        #endregion


        #region Beolvasás
        private void ForteBetöltés_Click(object sender, EventArgs e)
        {
            string szöveg = "";
            string fájlexc = "";
            try
            {

                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Forte-s Adatok betöltése",
                    FileName = "",
                    Filter = "XML Files|*.xml|Excel |*.xlsx "
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;



                DateTime MelyikNap = MyX.KidobóDátumEll(fájlexc);
                if (MelyikNap.ToShortDateString() != Dátum.Value.ToShortDateString())
                {
                    // ha nem egyezik akkor       
                    Holtart.Ki();
                    throw new HibásBevittAdat("A betölteni kívánt adatok nem egyeznek meg a beállított nappal !");
                }


                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\{Dátum.Value.Year}\{Dátum.Value:yyyyMMdd}Forte.mdb";
                if (File.Exists(hely))
                {
                    if (MessageBox.Show("Már van az adott napra feltöltve adat ! Módosítjuk az adatokat ?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        // Nemet választottuk
                        Holtart.Ki();
                        return;
                    }
                    else
                    {
                        // ha létezik akkor töröljük
                        // igent választottuk
                        File.Delete(hely);
                    }
                }

                List<string> hibak;
                List<Adat_Kidobó> adatok = MyX.BeolvasKidobo(fájlexc, out hibak);
                if (hibak.Count > 0)
                {
                    foreach (var h in hibak)
                        szöveg += $"Hiba: {h}/n";
                    throw new Exception(szöveg);
                }

                KézKidobó.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value, adatok);

                Gombok();

                // kitöröljük a betöltött fájlt
                File.Delete(fájlexc);

                Holtart.Ki();
                MessageBox.Show("Az adat konvertálás befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.StackTrace.Contains("System.IO.File.InternalDelete"))
                    MessageBox.Show($"A programnak a beolvasott adatokat tartalmazó fájlt nem sikerült törölni.\n Valószínüleg a {fájlexc} nyitva van.\n\nAz adat konvertálás befejeződött!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    HibaNapló.Log(ex.Message, $"{this}\n\n{szöveg}", ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Forte_Beolvasás_Click(object sender, EventArgs e)
        {
            string szöveg = "";
            string fájlexc = "";
            try
            {
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Forte-s Adatok betöltése",
                    FileName = "",
                    Filter = "XML Files|*.xml|Excel |*.xlsx "
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                DateTime MelyikNap = MyX.KidobóDátumEll(fájlexc);
                if (MelyikNap.ToShortDateString() != Dátum.Value.ToShortDateString())
                {
                    // ha nem egyezik akkor       
                    Holtart.Ki();
                    throw new HibásBevittAdat("A betölteni kívánt adatok nem egyeznek meg a beállított nappal !");
                }

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\{Dátum.Value.Year}\{Dátum.Value:yyyyMMdd}Forte.mdb";
                if (!File.Exists(hely))
                {
                    MessageBox.Show("A választott napra még nincs feltöltve adat ! ", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Holtart.Ki();
                    return;
                }

                List<string> hibak;
                List<Adat_Kidobó> adatok = MyX.BeolvasKidobo(fájlexc, out hibak);
                if (hibak.Count > 0)
                {
                    foreach (var h in hibak)
                        szöveg += $"Hiba: {h}/n";
                    throw new Exception(szöveg);
                }

                KézKidobó.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value, adatok);

                Gombok();

                MyX.ExcelBezárás();
                // kitöröljük a betöltött fájlt
                File.Delete(fájlexc);
                Holtart.Ki();
                MessageBox.Show("Az adat konvertálás befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.StackTrace.Contains("System.IO.File.InternalDelete"))
                    MessageBox.Show($"A programnak a beolvasott adatokat tartalmazó fájlt nem sikerült törölni.\n Valószínüleg a {fájlexc} nyitva van.\n\nAz adat konvertálás befejeződött!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion


        #region Napi
        private void Napi_Adatok_Módosítása()
        {
            try
            {
                if (Napi_Adat == null) throw new HibásBevittAdat("Nincs kiválasztva elem.");
                Új_Ablak_Kidobó_Napi?.Close();
                Új_Ablak_Kidobó_Ismétlődő?.Close();

                Új_Ablak_Kidobó_Napi = new Ablak_Kidobó_Napi(Cmbtelephely.Text.Trim(), Napi_Adat, Dátum.Value, AlsóPanels);
                Új_Ablak_Kidobó_Napi.FormClosed += Új_Ablak_Kidobó_Napi_Closed;
                Új_Ablak_Kidobó_Napi.Top = 400;
                Új_Ablak_Kidobó_Napi.Left = 600;
                Új_Ablak_Kidobó_Napi.Show();
                Új_Ablak_Kidobó_Napi.Ismétlődő_Változás += NapiAdatokListázása;
                Új_Ablak_Kidobó_Napi.Ismétlődő_Változás += VáltozatCombofeltölt;
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

        private void Új_Ablak_Kidobó_Napi_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kidobó_Napi = null;
        }
        #endregion


        #region Ismétlődőt
        private void Új_Ablak_Kidobó_Ismétlődő_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kidobó_Ismétlődő = null;
        }
        #endregion


        #region Gombok
        private void Alsópanelkitöltés()
        {
            List<Adat_Telep_Kiegészítő_Kidobó> Adatok = KézKiegdob.Lista_Adatok(Cmbtelephely.Text.Trim());

            Adat_Telep_Kiegészítő_Kidobó AdatokKidob = (from a in Adatok
                                                        where a.Id == 1
                                                        select a).FirstOrDefault();
            if (AdatokKidob != null) AlsóPanels = AdatokKidob.Telephely;
        }

        private void Gombok()
        {
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\Kidobó\{Dátum.Value.Year}\{Dátum.Value:yyyyMMdd}Forte.mdb";
            if (!File.Exists(hely))
            {
                Command1.Enabled = false;
                Command2.Enabled = false;
                Command11.Enabled = false;
            }
            else
            {
                Command1.Enabled = true;
                Command2.Enabled = true;
                Command11.Enabled = true;
            }
        }

        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            Gombok();
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\{Dátum.Value.Year}";
            if (Directory.Exists(hely) == false) System.IO.Directory.CreateDirectory(hely);
        }

        private void Command2_Click(object sender, EventArgs e)
        {
            NapiAdatokListázása();
        }

        private void NapiAdatokListázása()
        {
            Label18.Text = "Adott napi adatok:";
            Tábla1.Visible = false;
            Tábla.Visible = true;
            Táblaíró();
            if (Új_Ablak_Kidobó_Napi != null && Új_Ablak_Kidobó_Napi.Rekord != null)
            {
                Napi_Adat = Új_Ablak_Kidobó_Napi.Rekord;
                Segéd_adat = new Adat_Kidobó_Segéd(Napi_Adat.Forgalmiszám,
                                                    Napi_Adat.Szolgálatiszám,
                                                    Napi_Adat.Kezdés,
                                                    Napi_Adat.Végzés,
                                                    Napi_Adat.Kezdéshely,
                                                    Napi_Adat.Végzéshely,
                                                    VáltozatCombo.Text.Trim(),
                                                    Napi_Adat.Megjegyzés);
            }

        }

        private void Táblaíró()
        {
            try
            {
                DataTable AdatTábla = new DataTable();

                AdatTábla.Columns.Clear();
                AdatTábla.Columns.Add("Visz.");
                AdatTábla.Columns.Add("Forg.");
                AdatTábla.Columns.Add("Szolg.");
                AdatTábla.Columns.Add("Jvez.");
                AdatTábla.Columns.Add("Kezdés");
                AdatTábla.Columns.Add("Végzés");
                AdatTábla.Columns.Add("Kezdési hely");
                AdatTábla.Columns.Add("Végzési hely");
                AdatTábla.Columns.Add("Tárolásihely");
                AdatTábla.Columns.Add("Kocsi");
                AdatTábla.Columns.Add("Megjegyzés");
                AdatTábla.Columns.Add("Típus");

                AdatTábla.Clear();

                Tábla.Visible = false;

                List<Adat_Kidobó> Adatok = KézKidobó.Lista_Adat(Cmbtelephely.Text.Trim(), Dátum.Value);

                foreach (Adat_Kidobó rekord in Adatok)
                {
                    DataRow Soradat = AdatTábla.NewRow();

                    Soradat["Visz."] = rekord.Viszonylat.Trim();
                    Soradat["Forg."] = rekord.Forgalmiszám.Trim();
                    Soradat["Szolg."] = rekord.Szolgálatiszám.Trim();
                    Soradat["Jvez."] = rekord.Jvez.Trim();
                    Soradat["Kezdés"] = rekord.Kezdés.ToString("HH:mm");
                    Soradat["Végzés"] = rekord.Végzés.ToString("HH:mm");
                    Soradat["Kezdési hely"] = rekord.Kezdéshely.Trim();
                    Soradat["Végzési hely"] = rekord.Végzéshely.Trim();
                    Soradat["Tárolásihely"] = rekord.Tárolásihely.Trim();
                    Soradat["Kocsi"] = rekord.Villamos.Trim();
                    Soradat["Megjegyzés"] = rekord.Megjegyzés.Trim();
                    Soradat["Típus"] = rekord.Szerelvénytípus.Trim();

                    AdatTábla.Rows.Add(Soradat);
                }
                Tábla.CleanFilterAndSort();
                Tábla.DataSource = AdatTábla;

                Tábla.Columns["Visz."].Width = 80;
                Tábla.Columns["Forg."].Width = 80;
                Tábla.Columns["Szolg."].Width = 100;
                Tábla.Columns["Jvez."].Width = 250;
                Tábla.Columns["Kezdés"].Width = 100;
                Tábla.Columns["Végzés"].Width = 100;
                Tábla.Columns["Kezdési hely"].Width = 200;
                Tábla.Columns["Végzési hely"].Width = 200;
                Tábla.Columns["Tárolásihely"].Width = 100;
                Tábla.Columns["Kocsi"].Width = 100;
                Tábla.Columns["Megjegyzés"].Width = 100;
                Tábla.Columns["Típus"].Width = 100;

                Tábla.Visible = true;
                Tábla.Refresh();
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

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {       // leellenőrizzük, hogy van-e adat
                if (Tábla.Rows.Count < 1) return;
                if (e.RowIndex < 0) return;

                Napi_Adat = new Adat_Kidobó(
                     Tábla.Rows[e.RowIndex].Cells[0].Value.ToStrTrim(),
                     Tábla.Rows[e.RowIndex].Cells[1].Value.ToStrTrim(),
                     Tábla.Rows[e.RowIndex].Cells[2].Value.ToStrTrim(),
                     Tábla.Rows[e.RowIndex].Cells[3].Value.ToStrTrim(),
                     DateTime.Parse(Tábla.Rows[e.RowIndex].Cells[4].Value.ToStrTrim()),
                     DateTime.Parse(Tábla.Rows[e.RowIndex].Cells[5].Value.ToStrTrim()),
                     Tábla.Rows[e.RowIndex].Cells[6].Value.ToStrTrim(),
                     Tábla.Rows[e.RowIndex].Cells[7].Value.ToStrTrim(),
                     "",
                     Tábla.Rows[e.RowIndex].Cells[8].Value.ToStrTrim(),
                     Tábla.Rows[e.RowIndex].Cells[9].Value.ToStrTrim(),
                     Tábla.Rows[e.RowIndex].Cells[10].Value.ToStrTrim(),
                     Tábla.Rows[e.RowIndex].Cells[11].Value.ToStrTrim()
                    );
                Napi_Adatok_Módosítása();

                Segéd_adat = new Adat_Kidobó_Segéd(
                     Tábla.Rows[e.RowIndex].Cells[1].Value.ToStrTrim(),
                     Tábla.Rows[e.RowIndex].Cells[2].Value.ToStrTrim(),
                     DateTime.Parse(Tábla.Rows[e.RowIndex].Cells[4].Value.ToStrTrim()),
                     DateTime.Parse(Tábla.Rows[e.RowIndex].Cells[5].Value.ToStrTrim()),
                     Tábla.Rows[e.RowIndex].Cells[6].Value.ToStrTrim(),
                     Tábla.Rows[e.RowIndex].Cells[7].Value.ToStrTrim(),
                     VáltozatCombo.Text.Trim(),
                     Tábla.Rows[e.RowIndex].Cells[10].Value.ToStrTrim()
                     );
                if (Új_Ablak_Kidobó_Ismétlődő != null) Napi_Adatok_Módosítása();

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

        private void Command11_Click(object sender, EventArgs e)
        {
            try
            {
                if (VáltozatCombo.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva a változat amivel lehet módosítani.");

                // ha nincs olyan akkor rögzít különben módosít
                Holtart.Be(20);

                List<Adat_Kidobó_Segéd> AdatokÖ = KézSegéd.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Kidobó_Segéd> Adatok = (from a in AdatokÖ
                                                  where a.Változatnév == VáltozatCombo.Text.Trim()
                                                  select a).ToList();

                List<Adat_Kidobó> AdatokKidobó = KézKidobó.Lista_Adat(Cmbtelephely.Text.Trim(), Dátum.Value);

                List<Adat_Kidobó> Adatok_Módosítás = new List<Adat_Kidobó>();

                foreach (Adat_Kidobó_Segéd rekord in Adatok)
                {
                    // megkeressük a táblázatban a módosítandó sort
                    Adat_Kidobó AdatKidobó = (from a in AdatokKidobó
                                              where a.Szolgálatiszám == rekord.Szolgálatiszám.Trim()
                                              select a).FirstOrDefault();

                    if (AdatKidobó != null)
                    {
                        Adat_Kidobó ADAT = new Adat_Kidobó(AdatKidobó.Viszonylat,
                                                           AdatKidobó.Forgalmiszám,
                                                           AdatKidobó.Szolgálatiszám,
                                                           AdatKidobó.Jvez,
                                                           rekord.Kezdés,
                                                           rekord.Végzés,
                                                           rekord.Kezdéshely.Trim(),
                                                           rekord.Végzéshely.Trim(),
                                                           AdatKidobó.Kód,
                                                           AdatKidobó.Tárolásihely,
                                                           AdatKidobó.Villamos,
                                                           rekord.Megjegyzés.Trim(),
                                                           AdatKidobó.Szerelvénytípus);

                        Adatok_Módosítás.Add(ADAT);
                        Holtart.Lép();
                    }
                }
                if (Adatok_Módosítás != null && Adatok_Módosítás.Count > 0) KézKidobó.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value, Adatok_Módosítás);
                Holtart.Ki();
                Label18.Text = "Adott napi adatok:";
                Tábla1.Visible = false;
                Tábla.Visible = true;
                Táblaíró();
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


        #region Excel
        private void Command1_Click(object sender, EventArgs e)
        {
            try
            {
                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Kidobó készítés",
                    FileName = $"Kidobó_{Dátum.Value:yyyy.MM.dd}_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Holtart.Be();
                // Létrehozzuk az excelt
                string munkalap = "A változat";
                MyX.ExcelLétrehozás(munkalap);

                MyX.Munkalap_Új("B változat");
                MyX.Munkalap_Új("Száva változat");

                A_változat();
                B_változat();
                Száva_változat();

                // bezárjuk az Excel-t
                MyX.Munkalap_aktív(munkalap);

                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                Holtart.Ki();
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

        private void A_változat()
        {
            try
            {
                string munkalap = "A változat";
                MyX.Munkalap_betű(munkalap, BeBetű16);
                int sor;
                MyX.Munkalap_aktív(munkalap);

                MyX.Oszlopszélesség(munkalap, "a:a", 9);
                MyX.Oszlopszélesség(munkalap, "b:c", 15);
                MyX.Oszlopszélesség(munkalap, "d:d", 9);
                MyX.Oszlopszélesség(munkalap, "e:e", 38);
                MyX.Oszlopszélesség(munkalap, "f:f", 45);
                MyX.Oszlopszélesség(munkalap, "g:g", 10);
                MyX.Oszlopszélesség(munkalap, "h:h", 40);
                MyX.Oszlopszélesség(munkalap, "i:i", 5);
                MyX.Oszlopszélesség(munkalap, "j:j", 9);
                MyX.Egyesít(munkalap, "a1:j1");
                MyX.Betű(munkalap, "a1", BeBetű36);
                MyX.Sormagasság(munkalap, "1:1", 45);
                MyX.Kiir(Dátum.Value.ToString("yyyy.MMMM.dd. dddd"), "a1");
                MyX.Egyesít(munkalap, "a3:i3");
                sor = 3;
                MyX.Betű(munkalap, $"a{sor}", BeBetű30);
                MyX.Kiir("Délelőtti kiállók", $"a{sor}");
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 39);
                sor += 1;
                // fejléc
                // délelőtti lista
                MyX.Kiir("Szolg.szám", $"b{sor}");
                MyX.Kiir("Forg.szám", $"c{sor}");
                MyX.Kiir("Kezd", $"a{sor}");
                MyX.Kiir("Végez", $"d{sor}");
                MyX.Kiir("Név", $"e{sor}");
                MyX.Kiir("Pályaszám(ok)", $"f{sor}");
                MyX.Kiir("Vágány", $"g{sor}");
                MyX.Kiir("Megjegyzés", $"h{sor}");
                MyX.Kiir("Típus", $"j{sor}");
                MyX.Rácsoz(munkalap, $"a{sor}:j{sor}");

                DateTime Határóra = new DateTime(1900, 01, 01, 12, 0, 0);
                List<Adat_Kidobó> AdatokÖ = KézKidobó.Lista_Adat(Cmbtelephely.Text.Trim(), Dátum.Value);
                List<Adat_Kidobó> Adatok = (from a in AdatokÖ
                                            where a.Kezdéshely == AlsóPanels.Trim()
                                            && a.Kezdés < Határóra
                                            orderby a.Kezdés
                                            select a).ToList();

                Holtart.Be(20);
                int i = 0;
                if (Adatok.Count < 1) sor += 1;
                foreach (Adat_Kidobó rekord in Adatok)
                {
                    sor += 1;
                    MyX.Kiir(rekord.Szolgálatiszám.Trim().Replace("/", "|"), $"b{sor}");
                    MyX.Kiir(rekord.Forgalmiszám.Trim(), $"c{sor}");
                    MyX.Kiir(rekord.Kezdés.ToString("HH:mm"), $"a{sor}");
                    MyX.Kiir(rekord.Végzés.ToString("HH:mm"), $"d{sor}");
                    MyX.Kiir(rekord.Jvez.Trim(), $"e{sor}");
                    MyX.Kiir(rekord.Villamos.Trim(), $"f{sor}");
                    MyX.Kiir(rekord.Tárolásihely.Trim(), $"g{sor}");
                    MyX.Kiir(rekord.Megjegyzés.Trim(), $"h{sor}");
                    MyX.Kiir($"{i + 1}", $"i{sor}");
                    MyX.Kiir(rekord.Szerelvénytípus.Trim(), $"j{sor}");
                    if (rekord.Végzés.Hour < 12 && rekord.Végzéshely.Trim() == AlsóPanels.Trim())
                    {
                        // a beállókat kiemeli
                        MyX.Betű(munkalap, $"a{sor}:j{sor}", BeBetű16V);
                        MyX.Háttérszín(munkalap, $"a{sor}:j{sor}", Color.Yellow);
                    }
                    i++;
                    Holtart.Lép();
                }

                MyX.Rácsoz(munkalap, $"a5:j{sor}");
                MyX.Sormagasság(munkalap, $"5:{sor}", 30);
                sor += 2;

                MyX.Egyesít(munkalap, $"a{sor}:j{sor}");
                MyX.Kiir("Délutáni kiállók", $"a{sor}");
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 39);
                MyX.Betű(munkalap, $"a{sor}", BeBetű30);
                sor += 1;
                int blokkeleje = sor;
                // fejléc
                // délutáni lista
                MyX.Kiir("Szolg.szám", $"b{sor}");
                MyX.Kiir("Forg.szám", $"c{sor}");
                MyX.Kiir("Kezd", $"a{sor}");
                MyX.Kiir("Végez", $"d{sor}");
                MyX.Kiir("Név", $"e{sor}");
                MyX.Kiir("Pályaszám(ok)", $"f{sor}");
                MyX.Kiir("Vágány", $"g{sor}");
                MyX.Kiir("Megjegyzés", $"h{sor}");
                MyX.Kiir("Típus", $"j{sor}");
                MyX.Vastagkeret(munkalap, $"a{sor}:j{sor}");

                Adatok = (from a in AdatokÖ
                          where a.Kezdéshely == AlsóPanels.Trim()
                          && a.Kezdés > Határóra
                          orderby a.Kezdés
                          select a).ToList();
                Holtart.Lép();
                i = 0;
                Holtart.Lép();

                foreach (Adat_Kidobó rekord in Adatok)
                {
                    sor += 1;
                    MyX.Kiir(rekord.Szolgálatiszám.Trim().Replace("/", "|"), $"b{sor}");
                    MyX.Kiir(rekord.Forgalmiszám.Trim(), $"c{sor}");
                    MyX.Kiir(rekord.Kezdés.ToString("HH:mm"), $"a{sor}");
                    MyX.Kiir(rekord.Végzés.ToString("HH:mm"), $"d{sor}");
                    MyX.Kiir(rekord.Jvez.Trim(), $"e{sor}");
                    MyX.Kiir(rekord.Villamos.Trim(), $"f{sor}");
                    MyX.Kiir(rekord.Tárolásihely.Trim(), $"g{sor}");
                    MyX.Kiir(rekord.Megjegyzés.Trim(), $"h{sor}");
                    MyX.Kiir($"{(i + 1)}", $"i{sor}");
                    MyX.Kiir(rekord.Szerelvénytípus.Trim(), $"j{sor}");
                    i++;
                    Holtart.Lép();
                }

                MyX.Rácsoz(munkalap, $"a{blokkeleje}:j{sor}");
                MyX.Sormagasság(munkalap, $"{blokkeleje}:{sor}", 30);

                // ////////////////////////////////////
                // / telepen kívüli váltások
                // ////////////////////////////////////
                sor += 2;

                MyX.Egyesít(munkalap, $"a{sor}:j{sor}");
                MyX.Kiir("Végállomási kezdők", $"a{sor}");
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 39);
                MyX.Betű(munkalap, $"a{sor}", BeBetű30);
                sor += 1;
                blokkeleje = sor;
                // fejléc

                MyX.Kiir("Szolg.szám", $"b{sor}");
                MyX.Kiir("Forg.szám", $"c{sor}");
                MyX.Kiir("Kezd", $"a{sor}");
                MyX.Kiir("Végez", $"d{sor}");
                MyX.Kiir("Név", $"e{sor}");
                MyX.Kiir("Pályaszám(ok)", $"f{sor}");
                MyX.Kiir("Vágány", $"g{sor}");
                MyX.Kiir("Megjegyzés", $"h{sor}");
                MyX.Vastagkeret(munkalap, $"a{sor}:j{sor}");
                MyX.Kiir("Típus", $"j{sor}");

                Adatok = (from a in AdatokÖ
                          where a.Kezdéshely != AlsóPanels.Trim()
                          orderby a.Kezdés
                          select a).ToList();
                i = 0;
                Holtart.Lép();

                foreach (Adat_Kidobó rekord in Adatok)
                {
                    sor += 1;
                    MyX.Kiir(rekord.Szolgálatiszám.Trim().Replace("/", "|"), $"b{sor}");
                    MyX.Kiir(rekord.Forgalmiszám.Trim(), $"c{sor}");
                    MyX.Kiir(rekord.Kezdés.ToString("HH:mm"), $"a{sor}");
                    MyX.Kiir(rekord.Végzés.ToString("HH:mm"), $"d{sor}");
                    MyX.Kiir(rekord.Jvez.Trim(), $"e{sor}");
                    MyX.Kiir(rekord.Villamos.Trim(), $"f{sor}");
                    MyX.Kiir(rekord.Tárolásihely.Trim(), $"g{sor}");
                    MyX.Kiir(rekord.Megjegyzés.Trim(), $"h{sor}");
                    MyX.Kiir($"{(i + 1)}", $"i{sor}");
                    MyX.Kiir(rekord.Szerelvénytípus.Trim(), $"j{sor}");
                    i++;
                    Holtart.Lép();
                }


                MyX.Rácsoz(munkalap, $"a{blokkeleje + 1}:j{sor}");

                MyX.Sormagasság(munkalap, $"{blokkeleje}:{sor}", 30);
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:J{sor}",
                    LapSzéles = 1,
                    FejlécKözép = Program.PostásNév.Trim(),
                    FejlécJobb = DateTime.Now.ToString("yyyy.MM.dd HH:mm"),
                    LáblécKözép = @"&P/&N"
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);
                MyX.Munkalap_aktív(munkalap);
                MyX.Aktív_Cella(munkalap, "A1");
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

        private void B_változat()
        {
            try
            {
                // ***********************************
                // * Másik változat  B
                // ***********************************
                string munkalap = "B változat";

                MyX.Munkalap_aktív(munkalap);
                MyX.Munkalap_betű(munkalap, BeBetű16);

                int sor;
                int blokkeleje;

                MyX.Oszlopszélesség(munkalap, "a:a", 9);
                MyX.Oszlopszélesség(munkalap, "b:c", 15);
                MyX.Oszlopszélesség(munkalap, "d:d", 9);
                MyX.Oszlopszélesség(munkalap, "e:e", 38);
                MyX.Oszlopszélesség(munkalap, "f:f", 24);
                MyX.Oszlopszélesség(munkalap, "g:g", 30);
                MyX.Oszlopszélesség(munkalap, "h:h", 40);
                MyX.Oszlopszélesség(munkalap, "i:i", 9);
                MyX.Egyesít(munkalap, "a1:i1");
                MyX.Betű(munkalap, "a1", BeBetű36);
                MyX.Sormagasság(munkalap, "1:1", 45);
                MyX.Kiir(Dátum.Value.ToString("yyyy.MMMM.dd. dddd"), "a1");
                MyX.Egyesít(munkalap, "a3:h3");
                sor = 3;
                MyX.Betű(munkalap, $"a{sor}", BeBetű30);
                MyX.Kiir("Délelőtti kiállók", $"a{sor}");
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 39);
                sor += 1;
                // fejléc
                // délelőtti lista
                MyX.Kiir("Szolg.szám", $"b{sor}");
                MyX.Kiir("Forg.szám", $"c{sor}");
                MyX.Kiir("Kezd", $"a{sor}");
                MyX.Kiir("Végez", $"d{sor}");
                MyX.Kiir("Név", $"e{sor}");
                MyX.Kiir("Pályaszám(ok)", $"f{sor}");
                MyX.Kiir("Vágány", $"g{sor}");
                MyX.Kiir("Megjegyzés", $"h{sor}");
                MyX.Kiir("Típus", $"i{sor}");
                MyX.Vastagkeret(munkalap, $"a{sor}" + $":i{sor}");

                DateTime Határóra = new DateTime(1900, 1, 1, 12, 0, 0);
                List<Adat_Kidobó> AdatokÖ = KézKidobó.Lista_Adat(Cmbtelephely.Text.Trim(), Dátum.Value);
                List<Adat_Kidobó> Adatok = (from a in AdatokÖ
                                            where a.Kezdéshely == AlsóPanels.Trim()
                                            && a.Kezdés < Határóra
                                            orderby a.Viszonylat, a.Kezdés
                                            select a).ToList();
                Holtart.Be(20);
                int i = 0;
                string utolsóviszonylat = "";

                foreach (Adat_Kidobó rekord in Adatok)
                {
                    if (utolsóviszonylat.Trim() == "")
                        utolsóviszonylat = rekord.Viszonylat.Trim();

                    if (i == 0)
                    {

                        sor += 1;
                        MyX.Egyesít(munkalap, $"a{sor}:i{sor}");
                        MyX.Kiir(rekord.Viszonylat.Trim() + " Viszonylat", $"a{sor}");
                        MyX.Háttérszín(munkalap, $"a{sor}", Color.Yellow);
                    }

                    if (utolsóviszonylat.Trim() != rekord.Viszonylat.Trim())
                    {
                        sor += 1;
                        MyX.Kiir("Tartalék:", $"e{sor}");
                        MyX.Háttérszín(munkalap, $"e{sor}", Color.Green);
                        sor += 1;
                        MyX.Egyesít(munkalap, $"a{sor}:i{sor}");
                        MyX.Kiir(rekord.Viszonylat.Trim() + " Viszonylat", $"a{sor}");
                        MyX.Háttérszín(munkalap, $"a{sor}", Color.Yellow);
                        utolsóviszonylat = rekord.Viszonylat.Trim();
                    }
                    sor += 1;
                    MyX.Kiir(rekord.Szolgálatiszám.Trim().Replace("/", "|"), $"b{sor}");
                    MyX.Kiir(rekord.Forgalmiszám.Trim(), $"c{sor}");
                    MyX.Kiir(rekord.Kezdés.ToString("HH: mm"), $"a{sor}");
                    MyX.Kiir(rekord.Végzés.ToString("HH: mm"), $"d{sor}");
                    MyX.Kiir(rekord.Jvez.Trim(), $"e{sor}");
                    MyX.Kiir(rekord.Villamos.Trim(), $"f{sor}");
                    MyX.Kiir(rekord.Tárolásihely.Trim(), $"g{sor}");
                    MyX.Kiir(rekord.Megjegyzés.Trim(), $"h{sor}");
                    MyX.Kiir(rekord.Szerelvénytípus.Trim(), $"i{sor}");
                    if (rekord.Végzés.Hour < 12 && rekord.Végzéshely.Trim() == AlsóPanels.Trim())
                    {
                        // a beállókat kiemeli
                        MyX.Betű(munkalap, $"a{sor}:i{sor}", BeBetű16V);
                        MyX.Háttérszín(munkalap, $"a{sor}:i{sor}", Color.Yellow);
                    }
                    i++;
                    Holtart.Lép();
                }


                sor += 1;
                MyX.Kiir("Tartalék:", $"e{sor}");
                MyX.Háttérszín(munkalap, $"e{sor}", Color.Green);
                MyX.Rácsoz(munkalap, $"a5:i{sor}");
                MyX.Sormagasság(munkalap, $"5:{sor}", 30);
                sor += 2;

                MyX.Egyesít(munkalap, $"a{sor}:i{sor}");
                MyX.Kiir("Délutáni kiállók", $"a{sor}");
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 39);
                MyX.Betű(munkalap, $"a{sor}", BeBetű30);
                sor += 1;
                blokkeleje = sor;
                // fejléc
                // délutáni lista
                MyX.Kiir("Szolg.szám", $"b{sor}");
                MyX.Kiir("Forg.szám", $"c{sor}");
                MyX.Kiir("Kezd", $"a{sor}");
                MyX.Kiir("Végez", $"d{sor}");
                MyX.Kiir("Név", $"e{sor}");
                MyX.Kiir("Pályaszám(ok)", $"f{sor}");
                MyX.Kiir("Vágány", $"g{sor}");
                MyX.Kiir("Megjegyzés", $"h{sor}");
                MyX.Kiir("Típus", $"i{sor}");
                MyX.Vastagkeret(munkalap, $"a{sor}" + $":i{sor}");

                i = 0;
                Holtart.Lép();
                utolsóviszonylat = "";

                foreach (Adat_Kidobó rekord in Adatok)
                {
                    if (utolsóviszonylat.Trim() == "")
                        utolsóviszonylat = rekord.Viszonylat.Trim();

                    if (i == 0)
                    {
                        sor += 1;
                        MyX.Egyesít(munkalap, $"a{sor}:i{sor}");
                        MyX.Kiir(rekord.Viszonylat.Trim() + " Viszonylat", $"a{sor}");
                        MyX.Háttérszín(munkalap, $"a{sor}", Color.Yellow);
                    }

                    if (utolsóviszonylat.Trim() != rekord.Viszonylat.Trim())
                    {
                        sor += 1;
                        MyX.Kiir("Tartalék:", $"e{sor}");
                        MyX.Háttérszín(munkalap, $"e{sor}", Color.Green);
                        sor += 1;
                        MyX.Egyesít(munkalap, $"a{sor}:i{sor}");
                        MyX.Kiir(rekord.Viszonylat.Trim() + " Viszonylat", $"a{sor}");

                        MyX.Háttérszín(munkalap, $"a{sor}", Color.Yellow);
                        utolsóviszonylat = rekord.Viszonylat.Trim();
                    }
                    sor += 1;

                    MyX.Kiir(rekord.Szolgálatiszám.Trim().Replace("/", "|"), $"b{sor}");
                    MyX.Kiir(rekord.Forgalmiszám.Trim(), $"c{sor}");
                    MyX.Kiir(rekord.Kezdés.ToString("HH: mm"), $"a{sor}");
                    MyX.Kiir(rekord.Végzés.ToString("HH: mm"), $"d{sor}");
                    MyX.Kiir(rekord.Jvez.Trim(), $"e{sor}");
                    MyX.Kiir(rekord.Villamos.Trim(), $"f{sor}");
                    MyX.Kiir(rekord.Tárolásihely.Trim(), $"g{sor}");
                    MyX.Kiir(rekord.Megjegyzés.Trim(), $"h{sor}");
                    MyX.Kiir(rekord.Szerelvénytípus.Trim(), $"i{sor}");
                    i++;
                    Holtart.Lép();
                }

                sor += 1;
                MyX.Kiir("Tartalék:", $"e{sor}");
                MyX.Háttérszín(munkalap, $"e{sor}", Color.Green);
                MyX.Rácsoz(munkalap, $"a{blokkeleje + 1}:i{sor}");
                MyX.Sormagasság(munkalap, $"{blokkeleje}:{sor}", 30);
                // nyomtatási beállítások
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:I{sor}",
                    LapSzéles = 1,
                    FejlécKözép = Program.PostásNév.Trim(),
                    FejlécJobb = DateTime.Now.ToString("yyyy.MM.dd HH:mm"),
                    LáblécKözép = @"&P/&N"
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);
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

        private void Száva_változat()
        {
            try
            {
                string munkalap = "Száva változat";

                MyX.Munkalap_aktív(munkalap);
                MyX.Munkalap_betű(munkalap, BeBetű10);

                MyX.Sormagasság(munkalap, "1:43", 24);
                MyX.Sormagasság(munkalap, "44:60", 48);

                // Oszlop szélességek
                MyX.Oszlopszélesség(munkalap, "a:a", 7);
                MyX.Oszlopszélesség(munkalap, "b:b", 7);
                MyX.Oszlopszélesség(munkalap, "c:c", 7);
                MyX.Oszlopszélesség(munkalap, "d:d", 4);
                MyX.Oszlopszélesség(munkalap, "e:e", 10);
                MyX.Oszlopszélesség(munkalap, "f:f", 18);
                MyX.Oszlopszélesség(munkalap, "g:g", 7);
                // jobb oldal
                MyX.Oszlopszélesség(munkalap, "h:h", 7);
                MyX.Oszlopszélesség(munkalap, "i:i", 7);
                MyX.Oszlopszélesség(munkalap, "j:j", 7);
                MyX.Oszlopszélesség(munkalap, "k:k", 4);
                MyX.Oszlopszélesség(munkalap, "l:l", 10);
                MyX.Oszlopszélesség(munkalap, "m:m", 10);
                MyX.Oszlopszélesség(munkalap, "n:n", 7);
                MyX.Oszlopszélesség(munkalap, "o:o", 7);
                // fejléc készítés
                MyX.Egyesít(munkalap, "a1:o1");
                MyX.Kiir(Dátum.Value.ToString("yyyy.MM.dd dddd"), "a1");
                MyX.Betű(munkalap, "a1", BeBetű14V);

                MyX.Vastagkeret(munkalap, "a1:o1");

                MyX.Kiir("Sz.", "a2");
                MyX.Kiir("Fo.", "b2");
                MyX.Kiir("Kezd", "c2");
                MyX.Kiir("Vá.", "d2");
                MyX.Kiir("Kocsi", "e2");
                MyX.Kiir("Név", "f2");
                MyX.Kiir("Beáll", "g2");
                MyX.Rácsoz(munkalap, "a2:g2");

                // jobb oldal
                MyX.Kiir("Sz.", "h2");
                MyX.Kiir("Fo.", "i2");
                MyX.Kiir("Kezd", "j2");
                MyX.Kiir("Vá.", "k2");
                MyX.Kiir("Kocsi", "l2");
                MyX.Egyesít(munkalap, "m2:n2");
                MyX.Kiir("Név", "m2");
                MyX.Kiir("Beáll", "o2");
                MyX.Rácsoz(munkalap, "h2:o2");


                int sor = 2;
                int blokkeleje = 4;
                int baloldal = 1;
                string ideig;

                int[] típusdb = new int[Forte_típus.Count + 1];

                // érdemi rész
                DateTime Határóra = new DateTime(1900, 1, 1, 12, 0, 0);
                List<Adat_Kidobó> AdatokÖ = KézKidobó.Lista_Adat(Cmbtelephely.Text.Trim(), Dátum.Value);
                List<Adat_Kidobó> Adatok = (from a in AdatokÖ
                                            where a.Kezdéshely == AlsóPanels.Trim()
                                            orderby a.Viszonylat, a.Kezdés
                                            select a).ToList();
                string utolsóviszonylat = "";

                int i = 0;
                Holtart.Lép();
                foreach (Adat_Kidobó rekord in Adatok)
                {
                    if (utolsóviszonylat.Trim() == "")
                        utolsóviszonylat = rekord.Viszonylat.Trim();

                    if (i == 0)
                    {
                        // legelső alkalom viszonlat
                        sor += 1;
                        MyX.Egyesít(munkalap, $"a{sor}:d{sor}");
                        MyX.Egyesít(munkalap, $"e{sor}:g{sor}");
                        if (MyF.Szöveg_Tisztítás(rekord.Viszonylat, 3, 1) == "0")
                            ideig = MyF.Szöveg_Tisztítás(rekord.Viszonylat, 1, 2);
                        else
                            ideig = MyF.Szöveg_Tisztítás(rekord.Viszonylat, 1, 3);

                        MyX.Kiir(ideig + " Viszonylat", $"a{sor}");
                        MyX.Háttérszín(munkalap, $"a{sor}", Color.Yellow);
                        MyX.Betű(munkalap, $"a{sor}", BeBetű16);
                        MyX.Vastagkeret(munkalap, $"a{sor}:d{sor}");
                        MyX.Betű(munkalap, $"e{sor}", BeBetű16);
                        MyX.Vastagkeret(munkalap, $"e{sor}:g{sor}");

                        // lenullázuk a darabszámokat
                        for (int j = 0; j < Forte_típus.Count; j++)
                            típusdb[j] = 0;
                    }

                    if (utolsóviszonylat.Trim() != rekord.Viszonylat.Trim())
                    {
                        // rácsozás
                        if (baloldal == 1)
                        {
                            MyX.Rácsoz(munkalap, $"a{blokkeleje}:g{sor}");
                        }
                        else
                        {
                            MyX.Rácsoz(munkalap, $"h{blokkeleje}:o{sor}");
                        }

                        // típus darabszámok
                        ideig = "";

                        for (int j = 0; j < Forte_típus.Count; j++)
                        {
                            if (típusdb[j] != 0)
                                ideig += típusdb[j].ToString() + "-" + Forte_típus[j].ToString() + ";";

                            // lenullázzuk
                            típusdb[j] = 0;
                        }
                        if (baloldal == 1)
                            MyX.Kiir(ideig, $"e{blokkeleje - 1}");
                        else
                            MyX.Kiir(ideig, $"l{blokkeleje - 1}");


                        // ha új viszonylat lesz
                        sor += 1;
                        if (sor == 38)
                        {
                            // az eddigi rácsozása
                            MyX.Rácsoz(munkalap, $"a{blokkeleje}:g{sor - 1}");

                            sor = 3;
                            baloldal = 2;
                            blokkeleje = 3;
                        }

                        if (baloldal == 1)
                        {
                            MyX.Egyesít(munkalap, $"a{sor}:d{sor}");
                            MyX.Egyesít(munkalap, $"e{sor}:g{sor}");

                            if (MyF.Szöveg_Tisztítás(rekord.Viszonylat, 3, 1) == "0")
                                ideig = MyF.Szöveg_Tisztítás(rekord.Viszonylat, 1, 2);
                            else
                                ideig = MyF.Szöveg_Tisztítás(rekord.Viszonylat, 1, 3);


                            MyX.Kiir(ideig + " Viszonylat", $"a{sor}");

                            MyX.Betű(munkalap, $"a{sor}", BeBetű16);
                            MyX.Háttérszín(munkalap, $"a{sor}", Color.Yellow);
                            MyX.Vastagkeret(munkalap, $"a{sor}" + $":d{sor}");
                            MyX.Betű(munkalap, $"e{sor}", BeBetű16);
                            MyX.Vastagkeret(munkalap, $"e{sor}:g{sor}");

                            utolsóviszonylat = rekord.Viszonylat.Trim();
                        }
                        else
                        {
                            MyX.Egyesít(munkalap, $"h{sor}:k{sor}");
                            MyX.Egyesít(munkalap, $"l{sor}:o{sor}");
                            MyX.Kiir(rekord.Viszonylat.Trim() + " Viszonylat", $"h{sor}");

                            if (MyF.Szöveg_Tisztítás(rekord.Viszonylat, 3, 1) == "0")
                                ideig = MyF.Szöveg_Tisztítás(rekord.Viszonylat, 1, 2);
                            else
                                ideig = MyF.Szöveg_Tisztítás(rekord.Viszonylat, 1, 3);

                            MyX.Kiir(ideig + " Viszonylat", $"h{sor}");

                            MyX.Betű(munkalap, $"h{sor}", BeBetű16);
                            MyX.Háttérszín(munkalap, $"h{sor}:o{sor}", Color.Yellow);
                            MyX.Vastagkeret(munkalap, $"h{sor}:k{sor}");
                            MyX.Vastagkeret(munkalap, $"l{sor}:o{sor}");
                            MyX.Betű(munkalap, $"l{sor}", BeBetű16);
                            utolsóviszonylat = rekord.Viszonylat.Trim();

                            blokkeleje = sor + 1;
                        }
                    }

                    sor += 1;

                    if (sor == 38)
                    {
                        // az eddigi rácsozása
                        MyX.Rácsoz(munkalap, $"a{blokkeleje}:g{sor - 1}");
                        sor = 3;
                        baloldal = 2;
                        blokkeleje = 3;
                    }

                    // *********************************************
                    // Adatok kiírása
                    // *********************************************
                    // típust számolunk
                    // lenullázuk a darabszámokat

                    for (int j = 0; j < Forte_típus.Count; j++)
                    {
                        if (rekord.Kezdés.Hour < 12)
                        {
                            if (Forte_típus[j].ToStrTrim() == MyF.Szöveg_Tisztítás(rekord.Szerelvénytípus, 0, Forte_típus[j].ToString().Length))
                                típusdb[j] += 1;
                        }
                    }

                    if (baloldal == 1)
                    {
                        // bal oldal
                        ideig = rekord.Szolgálatiszám.Trim();
                        ideig = ideig.Replace(rekord.Viszonylat.Trim() + '/', "");

                        MyX.Kiir(ideig + "_", $"a{sor}");
                        if (MyF.Szöveg_Tisztítás(rekord.Szerelvénytípus, 0, 3) == "CAF" && rekord.Kezdés.Hour < 12)
                            MyX.Betű(munkalap, $"a{sor}", BeBetű10V);

                        MyX.Kiir(rekord.Forgalmiszám.Trim(), $"b{sor}");
                        DateTime ideigdátum = rekord.Kezdés.AddMinutes(20);

                        MyX.Kiir(ideigdátum.ToString("HH:mm"), $"c{sor}");
                        MyX.Kiir(rekord.Végzés.ToString("HH: mm"), $"g{sor}");
                        MyX.Betű(munkalap, $"b{sor}", BeBetű12);
                        MyX.Betű(munkalap, $"c{sor}", BeBetű12);
                        MyX.Betű(munkalap, $"g{sor}", BeBetű12);

                        MyX.Kiir(rekord.Jvez.Trim(), $"f{sor}");

                        if (ideigdátum.Hour > 12)
                        {
                            MyX.Betű(munkalap, $"a{sor}:g{sor}", BeBetű10V);
                            MyX.Háttérszín(munkalap, $"a{sor}:g{sor}", Color.Yellow);
                        }

                        if (rekord.Végzés.Hour < 12 && rekord.Végzéshely.Trim() == AlsóPanels.Trim())
                        {
                            // a beállókat kiemeli
                            MyX.Betű(munkalap, $"f{sor}:g{sor}", BeBetű10V);
                            MyX.Háttérszín(munkalap, $"f{sor}:g{sor}", Color.Yellow);
                        }
                    }
                    else
                    {
                        ideig = rekord.Szolgálatiszám.Trim();
                        ideig = ideig.Replace(rekord.Viszonylat.Trim() + '/', "");
                        MyX.Kiir(ideig + "_", $"h{sor}");
                        if (MyF.Szöveg_Tisztítás(rekord.Szerelvénytípus, 0, 3) == "CAF")
                            MyX.Betű(munkalap, $"h{sor}", BeBetű10V);

                        MyX.Kiir(rekord.Forgalmiszám.Trim(), $"i{sor}");
                        DateTime ideigdátum = rekord.Kezdés.AddMinutes(20);
                        MyX.Kiir(ideigdátum.ToString("HH:mm"), $"j{sor}");
                        MyX.Kiir(rekord.Végzés.ToString("HH: mm"), $"o{sor}");
                        MyX.Egyesít(munkalap, $"m{sor}:n{sor}");
                        MyX.Betű(munkalap, $"i{sor}", BeBetű12);
                        MyX.Betű(munkalap, $"j{sor}", BeBetű12);
                        MyX.Betű(munkalap, $"o{sor}", BeBetű12);
                        MyX.Kiir(rekord.Jvez.Trim(), $"m{sor}");


                        if (ideigdátum.Hour > 12)
                        {
                            MyX.Betű(munkalap, $"h{sor}:o{sor}", BeBetű10V);
                            MyX.Háttérszín(munkalap, $"h{sor}:o{sor}", Color.Yellow);
                        }

                        if (rekord.Végzés.Hour < 12 && rekord.Végzéshely.Trim() == AlsóPanels.Trim())
                        {
                            // a beállókat kiemeli
                            MyX.Betű(munkalap, $"m{sor}:o{sor}", BeBetű10V);
                            MyX.Háttérszín(munkalap, $"m{sor}:o{sor}", Color.Yellow);
                        }

                    }

                    i++;
                    Holtart.Lép();
                }

                // utolsó felvonás
                if (baloldal == 1)
                {
                    MyX.Rácsoz(munkalap, $"a{blokkeleje}:g{sor}");

                }
                else
                {
                    MyX.Rácsoz(munkalap, $"h{blokkeleje}:o{sor}");

                }

                // típus darabszámok
                ideig = "";

                for (int j = 0; j < Forte_típus.Count; j++)
                {
                    if (típusdb[j] != 0)
                        ideig += típusdb[j].ToString() + "-" + Forte_típus[j].ToStrTrim() + ";";

                    // lenullázzuk
                    típusdb[j] = 0;
                }
                if (baloldal == 1)

                    MyX.Kiir(ideig, "e" + (blokkeleje - 1).ToString());
                else
                    MyX.Kiir(ideig, "l" + (blokkeleje - 1).ToString());

                // hátoldal

                // fejléc készítés
                MyX.Egyesít(munkalap, "a40:o40");
                MyX.Kiir(Dátum.Value.ToString("yyyy.MM.dd dddd"), "a40");
                MyX.Betű(munkalap, "a40", BeBetű14V);
                MyX.Vastagkeret(munkalap, "a40:o40");

                MyX.Egyesít(munkalap, "a41:o41");
                MyX.Vastagkeret(munkalap, "a41:o41");

                MyX.Egyesít(munkalap, "a42:o42");
                MyX.Vastagkeret(munkalap, "a42:o42");
                MyX.Kiir("MŰSZAKI RÉSZ", "a42");

                // fejléc
                MyX.Kiir("Visz", "a43");
                MyX.Kiir("Idő", "b43");
                MyX.Egyesít(munkalap, "c43:d43");
                MyX.Kiir("Kocsi szám", "c43");
                MyX.Egyesít(munkalap, "e43:g43");
                MyX.Kiir("Beírt hiba", "e43");
                MyX.Egyesít(munkalap, "h43:l43");
                MyX.Kiir("Javított hiba", "h43");
                MyX.Kiir("Csere kocsi", "m43");
                MyX.Egyesít(munkalap, "n43:o43");
                MyX.Kiir("Csere ideje, helye", "n43");
                MyX.Rácsoz(munkalap, "a43:o43");

                for (int ii = 44; ii <= 60; ii++)
                {
                    MyX.Egyesít(munkalap, $"c{ii}:d{ii}");
                    MyX.Egyesít(munkalap, $"e{ii}:g{ii}");
                    MyX.Egyesít(munkalap, $"h{ii}:l{ii}");
                    MyX.Egyesít(munkalap, $"n{ii}:o{ii}");
                }
                MyX.Rácsoz(munkalap, "a44:o60");

                // JAVÍTANDÓ:       MyX.FerdeVonal("A44:A60");

                // nyomtatási beállítás
                // nyomtatási beállítások
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = "A1:O60",
                    LapSzéles = 1,
                    LapMagas = 2,
                    FejlécKözép = Program.PostásNév.Trim(),
                    FejlécJobb = DateTime.Now.ToString("yyyy.MM.dd HH:mm"),
                    LáblécKözép = @"&P/&N",
                    BalMargó = 5,
                    JobbMargó = 5,
                    AlsóMargó = 5,
                    FelsőMargó = 5,
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);
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


        #region Kereső
        private void Keresés_táblázatban()
        {
            try
            {
                // megkeressük a szöveget a táblázatban
                if (Új_Ablak_Kereső.Keresendő == null) return;
                if (Új_Ablak_Kereső.Keresendő.Trim() == "") return;
                if (Tábla.Rows.Count < 0) return;

                for (int i = 0; i < Tábla.Rows.Count; i++)
                {
                    for (int j = 0; j < Tábla.Columns.Count; j++)
                    {
                        if (Tábla.Rows[i].Cells[j].Value.ToStrTrim().Contains(Új_Ablak_Kereső.Keresendő.Trim()))
                        {
                            Tábla.Rows[i].Cells[j].Style.BackColor = Color.Orange;
                            Tábla.FirstDisplayedScrollingRowIndex = i;
                            Tábla.CurrentCell = Tábla.Rows[i].Cells[0];
                        }
                    }
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

        private void Keresés_Click(object sender, EventArgs e)
        {
            Keresés_metódus();
        }

        void Keresés_metódus()
        {
            try
            {
                if (Új_Ablak_Kereső == null)
                {
                    Új_Ablak_Kereső = new Ablak_Kereső();
                    Új_Ablak_Kereső.FormClosed += Új_Ablak_Kereső_Closed;
                    Új_Ablak_Kereső.Top = 50;
                    Új_Ablak_Kereső.Left = 50;
                    Új_Ablak_Kereső.Show();
                    Új_Ablak_Kereső.Ismétlődő_Változás += Keresés_táblázatban;
                }
                else
                {
                    Új_Ablak_Kereső.Activate();
                    Új_Ablak_Kereső.WindowState = FormWindowState.Normal;
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

        private void Új_Ablak_Kereső_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kereső = null;
        }
        #endregion


        #region Változat
        private void Command12_Click(object sender, EventArgs e)
        {
            Változatok_listázása();
        }

        private void Változatok_listázása()
        {

            Label18.Text = "Változat lista:";
            Tábla1.Visible = true;
            Tábla.Visible = false;

            List<Adat_Kidobó_Segéd> AdatokÖ = KézSegéd.Lista_Adatok(Cmbtelephely.Text.Trim());
            // ha nincs olyan akkor rögzít különben módosít
            List<Adat_Kidobó_Segéd> Adatok;
            if (VáltozatCombo.Text.Trim() == "")
                Adatok = AdatokÖ;
            else
                Adatok = (from a in AdatokÖ
                          where a.Változatnév == VáltozatCombo.Text.Trim()
                          select a).ToList();

            Tábla1.Rows.Clear();
            Tábla1.Columns.Clear();
            Tábla1.Refresh();
            Tábla1.Visible = false;
            Tábla1.ColumnCount = 8;

            // fejléc elkészítése
            Tábla1.Columns[0].HeaderText = "Szolg.";
            Tábla1.Columns[0].Width = 100;
            Tábla1.Columns[1].HeaderText = "Forg.";
            Tábla1.Columns[1].Width = 100;
            Tábla1.Columns[2].HeaderText = "Kezdés";
            Tábla1.Columns[2].Width = 100;
            Tábla1.Columns[3].HeaderText = "Végzés";
            Tábla1.Columns[3].Width = 100;
            Tábla1.Columns[4].HeaderText = "Kezdési hely";
            Tábla1.Columns[4].Width = 200;
            Tábla1.Columns[5].HeaderText = "Végzési hely";
            Tábla1.Columns[5].Width = 200;
            Tábla1.Columns[6].HeaderText = "Megjegyzés";
            Tábla1.Columns[6].Width = 100;
            Tábla1.Columns[7].HeaderText = "Változat";
            Tábla1.Columns[7].Width = 100;


            int i;
            foreach (Adat_Kidobó_Segéd rekord in Adatok)
            {

                Tábla1.RowCount++;
                i = Tábla1.RowCount - 1;
                Tábla1.Rows[i].Cells[0].Value = rekord.Szolgálatiszám.Trim();
                Tábla1.Rows[i].Cells[1].Value = rekord.Forgalmiszám.Trim();
                Tábla1.Rows[i].Cells[2].Value = rekord.Kezdés.ToString("HH:mm");
                Tábla1.Rows[i].Cells[3].Value = rekord.Végzés.ToString("HH:mm");
                Tábla1.Rows[i].Cells[4].Value = rekord.Kezdéshely.Trim();
                Tábla1.Rows[i].Cells[5].Value = rekord.Végzéshely.Trim();
                Tábla1.Rows[i].Cells[6].Value = rekord.Megjegyzés.Trim();
                Tábla1.Rows[i].Cells[7].Value = rekord.Változatnév.Trim();
            }
            Tábla1.Visible = true;
            Tábla1.Refresh();
        }

        public void VáltozatCombofeltölt()
        {
            try
            {
                List<Adat_Kidobó_Változat> Adatok = KézVáltozat.Lista_Adat(Cmbtelephely.Text.Trim());

                VáltozatCombo.Items.Clear();
                VáltozatCombo.Items.Add("");
                foreach (Adat_Kidobó_Változat elem in Adatok)
                    VáltozatCombo.Items.Add(elem.Változatnév);

                VáltozatCombo.Refresh();

                // típusok feltöltése
                List<Adat_Telep_Kieg_Fortetípus> AdatokFort = KézFortetípus.Lista_Adatok(Cmbtelephely.Text.Trim());
                Forte_típus.Clear();
                foreach (Adat_Telep_Kieg_Fortetípus elem in AdatokFort)
                    Forte_típus.Add(elem.Ftípus);
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

        private void Command8_Click(object sender, EventArgs e)
        {
            Ablakot_Nyit();
        }

        private void Ablakot_Nyit()
        {
            try
            {
                if (Segéd_adat == null) throw new HibásBevittAdat("Nincs kiválasztva elem.");
                Új_Ablak_Kidobó_Ismétlődő?.Close();

                Új_Ablak_Kidobó_Ismétlődő = new Ablak_Kidobó_Ismétlődő(Cmbtelephely.Text.Trim(), Segéd_adat, Dátum.Value, AlsóPanels);
                Új_Ablak_Kidobó_Ismétlődő.FormClosed += Új_Ablak_Kidobó_Ismétlődő_Closed;
                Új_Ablak_Kidobó_Ismétlődő.Top = 10;
                Új_Ablak_Kidobó_Ismétlődő.Left = 600;
                Új_Ablak_Kidobó_Ismétlődő.Show();
                Új_Ablak_Kidobó_Ismétlődő.Ismétlődő_Változás += Változatok_listázása;
                Új_Ablak_Kidobó_Ismétlődő.Ismétlődő_Változás += VáltozatCombofeltölt;
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

        private void Tábla1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                /// táblázatból kiolvassuk az adatokat
                Segéd_adat = new Adat_Kidobó_Segéd(
                                Tábla1.Rows[e.RowIndex].Cells[1].Value.ToStrTrim(),
                                Tábla1.Rows[e.RowIndex].Cells[0].Value.ToStrTrim(),
                                DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[2].Value.ToStrTrim()),
                                DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[3].Value.ToStrTrim()),
                                Tábla1.Rows[e.RowIndex].Cells[4].Value.ToStrTrim(),
                                Tábla1.Rows[e.RowIndex].Cells[5].Value.ToStrTrim(),
                                Tábla1.Rows[e.RowIndex].Cells[7].Value.ToStrTrim(),
                                Tábla1.Rows[e.RowIndex].Cells[6].Value.ToStrTrim()
                                );
                Ablakot_Nyit();
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


        #region Ittassági
        private void Btn_Ittasági_Click(object sender, EventArgs e)
        {
            try
            {
                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Ittasságvizsgálatilap készítés",
                    FileName = $"IttasságVizgyLap_{DateTime.Now:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Holtart.Be();
                // Létrehozzuk az excelt
                string munkalap = "Munka1";
                MyX.ExcelLétrehozás(munkalap);

                MyX.Munkalap_betű(munkalap, BeBetű16);

                IttaságiTartalom();

                // bezárjuk az Excel-t
                MyX.Munkalap_aktív(munkalap);
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                Holtart.Ki();
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

        private void IttaságiTartalom()
        {
            try
            {
                string munkalap = "Munka1";
                int sor;
                MyX.Munkalap_aktív(munkalap);

                MyX.Oszlopszélesség(munkalap, "a:a", 17);
                MyX.Oszlopszélesség(munkalap, "b:b", 8);
                MyX.Oszlopszélesség(munkalap, "c:c", 15);
                MyX.Oszlopszélesség(munkalap, "d:d", 38);
                MyX.Oszlopszélesség(munkalap, "e:e", 12);
                MyX.Oszlopszélesség(munkalap, "f:f", 30);
                MyX.Oszlopszélesség(munkalap, "g:g", 12);
                MyX.Oszlopszélesség(munkalap, "h:h", 30);
                MyX.Oszlopszélesség(munkalap, "i:i", 19);
                MyX.Oszlopszélesség(munkalap, "j:j", 30);
                MyX.Oszlopszélesség(munkalap, "k:k", 20);
                MyX.Egyesít(munkalap, "a1:k1");
                MyX.Betű(munkalap, "a1:k1", BeBetű36);
                MyX.Sormagasság(munkalap, "1:1", 45);
                MyX.Kiir(Dátum.Value.ToString("yyyy.MMMM.dd. dddd") + " Ittasság-vizgálati lap", "a1");
                sor = 3;
                Holtart.Be(20);
                // délelőtti kiállás
                DateTime Határóra = new DateTime(1899, 12, 30, 12, 0, 0);
                List<Adat_Kidobó> AdatokÖ = KézKidobó.Lista_Adat(Cmbtelephely.Text.Trim(), Dátum.Value, true);
                List<Adat_Kidobó> Adatok = (from a in AdatokÖ
                                            where a.Kezdéshely == AlsóPanels.Trim()
                                            && a.Kezdés < Határóra
                                            orderby a.Jvez
                                            select a).ToList();
                TáblázatIttasságihoz(ref sor, munkalap, Adatok, "Délelőtti kiállás");
                sor += 2;

                // délutáni kiálló
                Adatok = (from a in AdatokÖ
                          where a.Kezdéshely == AlsóPanels.Trim()
                          && a.Kezdés > Határóra
                          orderby a.Jvez
                          select a).ToList();
                TáblázatIttasságihoz(ref sor, munkalap, Adatok, "Délutáni kiállás");
                sor += 2;

                // délelőtti beállás
                Adatok = (from a in AdatokÖ
                          where a.Végzéshely == AlsóPanels.Trim()
                          && a.Kezdés < Határóra
                          orderby a.Jvez
                          select a).ToList();
                TáblázatIttasságihoz(ref sor, munkalap, Adatok, "Délelőtti beállás");
                sor += 2;

                // délutáni beállás
                Adatok = (from a in AdatokÖ
                          where a.Végzéshely == AlsóPanels.Trim()
                          && a.Kezdés > Határóra
                          orderby a.Jvez
                          select a).ToList();
                TáblázatIttasságihoz(ref sor, munkalap, Adatok, "Délutáni beállás");
                sor += 2;
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:K{sor}",
                    LapSzéles = 1,
                    Álló = false,
                    FejlécKözép = Program.PostásNév.Trim(),
                    FejlécJobb = DateTime.Now.ToString("yyyy.MM.dd HH:mm"),
                    LáblécKözép = @"&P/&N"
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);
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

        private void TáblázatIttasságihoz(ref int sor, string munkalap, List<Adat_Kidobó> Adatok, string Kategória)
        {
            try
            {
                int blokkeleje = sor;

                MyX.Egyesít(munkalap, $"a{sor}:k{sor}");
                MyX.Kiir(Kategória, $"a{sor}");
                MyX.Vastagkeret(munkalap, $"a{sor}:k{sor}");
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 45);
                MyX.Betű(munkalap, $"a{sor}", BeBetű30);
                sor++;

                MyX.Kiir("Viszonylat/\nSzolg.szám", $"a{sor}");
                MyX.Sortörésseltöbbsorba(munkalap, $"a{sor}");
                MyX.Kiir("Forg.\nszám", $"b{sor}");
                MyX.Sortörésseltöbbsorba(munkalap, $"b{sor}");
                MyX.Kiir("Törzsszám", $"c{sor}");
                MyX.Kiir("Járművezető neve", $"d{sor}");
                MyX.Kiir("Kezdési\n idő", $"e{sor}");
                MyX.Sortörésseltöbbsorba(munkalap, $"e{sor}");
                MyX.Kiir("Kezdési hely", $"f{sor}");
                MyX.Kiir("Végzési\n idő", $"g{sor}");
                MyX.Sortörésseltöbbsorba(munkalap, $"g{sor}");
                MyX.Kiir("Végzési hely", $"h{sor}");
                MyX.Kiir("Eredmény", $"i{sor}");
                MyX.Kiir("Járművezető aláírása", $"j{sor}");
                MyX.Kiir("Diszpécser", $"k{sor}");
                MyX.Rácsoz(munkalap, $"a{sor}:k{sor}");
                MyX.Háttérszín(munkalap, $"a{sor}:k{sor}", Color.Yellow);

                foreach (Adat_Kidobó rekord in Adatok)
                {
                    sor += 1;
                    MyX.Kiir(rekord.Szolgálatiszám.Trim().Replace("/", "|"), $"a{sor}");
                    MyX.Kiir(rekord.Forgalmiszám.Trim(), $"b{sor}");
                    MyX.Kiir(rekord.Törzsszám.Trim(), $"c{sor}");
                    MyX.Kiir(rekord.Jvez.Trim(), $"d{sor}");
                    MyX.Kiir(rekord.Kezdés.ToString("HH:mm"), $"e{sor}");
                    MyX.Kiir(rekord.Kezdéshely, $"f{sor}");
                    MyX.Kiir(rekord.Végzés.ToString("HH:mm"), $"g{sor}");
                    MyX.Kiir(rekord.Végzéshely, $"h{sor}");
                    Holtart.Lép();
                }

                MyX.Rácsoz(munkalap, $"a{blokkeleje + 1}:k{sor}");
                MyX.Sormagasság(munkalap, $"{blokkeleje}:{blokkeleje + 1}", 45);
                MyX.Sormagasság(munkalap, $"{blokkeleje + 2}:{sor}", 30);

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