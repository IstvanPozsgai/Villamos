using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Ablakok;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_kidobó
    {
        Ablak_Kidobó_Ismétlődő Új_Ablak_Kidobó_Ismétlődő;
        Ablak_Kidobó_Napi Új_Ablak_Kidobó_Napi;
        Ablak_Kereső Új_Ablak_Kereső;
        string AlsóPanels = "_";
        Adat_Kidobó_Segéd Segéd_adat = null;
        Adat_Kidobó Napi_Adat = null;

        public Ablak_kidobó()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Telephelyekfeltöltése();

            Dátum.Value = DateTime.Today;
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó";
            if (!Directory.Exists(hely))
                Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\{DateTime.Today.Year}";
            if (!Directory.Exists(hely)) System.IO.Directory.CreateDirectory(hely);
            // következő év
            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\{DateTime.Today.Year + 1}";
            if (!Directory.Exists(hely)) System.IO.Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\kidobósegéd.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kidobósegédadattábla(hely);

            Alsópanelkitöltés();
            VáltozatCombofeltölt();
            Gombok();
            Label18.Text = "";
            Jogosultságkiosztás();
        }


        #region Alap

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
            string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Kidobó.html";
            Module_Excel.Megnyitás(hely);
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                Cmbtelephely.Items.AddRange(Listák.TelephelyLista_Jármű());
                if (Program.PostásTelephely == "Főmérnökség" || Program.PostásTelephely.Contains("törzs"))
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim(); }
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


        //Becsukjuk az kiegészítő ablakokat
        private void Ablak_kidobó_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kidobó_Ismétlődő?.Close();
            Új_Ablak_Kereső?.Close();
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
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                // megnyitjuk a beolvasandó táblát
                MyE.ExcelMegnyitás(fájlexc);

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\{Dátum.Value.Year}\{Dátum.Value:yyyyMMdd}Forte.mdb";
                string jelszó = "lilaakác";

                // leellenőrizzük, hogy az adat nap egyezik-e
                szöveg = MyE.Beolvas("a2").Trim().Replace(".", "");
                if (MyE.Beolvas("a2").Trim().Replace(".", "") != Dátum.Value.ToString("yyyyMMdd"))
                {
                    // ha nem egyezik akkor       
                    // az excel tábla bezárása
                    MyE.ExcelBezárás();
                    Holtart.Ki();
                    throw new HibásBevittAdat("A betölteni kívánt adatok nem egyeznek meg a beállított nappal !");
                }

                if (!File.Exists(hely))
                {
                    Adatbázis_Létrehozás.Kidobóadattábla(hely);
                }
                else if (MessageBox.Show("Már van az adott napra feltöltve adat ! Módosítjuk az adatokat ?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    // Nemet választottuk
                    MyE.ExcelBezárás();
                    Holtart.Ki();
                    return;
                }
                else
                {
                    // ha létezik akkor töröljük
                    // igent választottuk
                    File.Delete(hely);
                    Adatbázis_Létrehozás.Kidobóadattábla(hely);
                }
                // megnézzük, hogy hány sorból áll a tábla
                int ii = 4;
                int utolsó = 0;

                while (MyE.Beolvas($"a{ii}").Trim() != "_")
                {
                    utolsó = ii;
                    ii += 1;
                    szöveg = MyE.Beolvas("a" + ii.ToString()).Trim();
                }
                string ideig;
                string viszonylat;
                Holtart.Be(utolsó + 1);
                if (utolsó > 1)
                {
                    // megnyitjuk a táblát
                    List<string> SzövegGy = new List<string>();
                    for (int i = 5; i <= utolsó; i++)
                    {

                        szöveg = "INSERT INTO Kidobótábla (viszonylat, forgalmiszám, szolgálatiszám, ";
                        szöveg += " jvez, kezdés, végzés, ";
                        szöveg += " Kezdéshely, Végzéshely, Kód, ";
                        szöveg += " Tárolásihely, Villamos, Megjegyzés, ";
                        szöveg += " szerelvénytípus ) VALUES (";
                        ideig = MyE.Beolvas($"a{i}");
                        viszonylat = "";

                        string[] darabol = ideig.Split('/');

                        viszonylat = darabol[0].Trim();

                        szöveg += $"'{MyF.Szöveg_Tisztítás(viszonylat, 0, 6)}', "; // viszonylat
                        szöveg += $"'{MyF.Szöveg_Tisztítás(MyE.Beolvas($"b{i}").Trim(), 0, 6)}', "; // forgalmiszám
                        szöveg += $"'{MyF.Szöveg_Tisztítás(ideig.Trim(), 0, 20)}', "; // szolgálatiszám
                        szöveg += $"'{MyF.Szöveg_Tisztítás(MyE.Beolvas($"d{i}").Trim(), 0, 100)}', "; // jvez
                        szöveg += $"'{MyE.Beolvasidő($"f{i}"):HH:mm:ss}', "; // kezdés
                        szöveg += $"'{MyE.Beolvasidő($"h{i}"):HH:mm:ss}', "; // végzés
                        szöveg += $"'{MyF.Szöveg_Tisztítás(MyE.Beolvas($"g{i}").Trim(), 0, 50)}', "; // kezdéshely
                        szöveg += $"'{MyF.Szöveg_Tisztítás(MyE.Beolvas($"i{i}").Trim(), 0, 50)}', "; // végzéshely
                        szöveg += $"'{MyF.Szöveg_Tisztítás(MyE.Beolvas($"e{i}").Trim(), 0, 3)}', "; // kód
                        szöveg += "'_', "; // tárolásihely
                        szöveg += "'_', "; // villamos
                        szöveg += "'_', "; // megjegyzés
                        szöveg += $"'{MyF.Szöveg_Tisztítás(MyE.Beolvas($"k{i}").Trim(), 0, 30)}') "; // szerelvénytípus
                        SzövegGy.Add(szöveg);
                        Holtart.Lép();
                    }
                    MyA.ABMódosítás(hely, jelszó, SzövegGy);
                }

                Gombok();

                MyE.ExcelBezárás();
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
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;


                // megnyitjuk a beolvasandó táblát
                MyE.ExcelMegnyitás(fájlexc);

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\" + Dátum.Value.Year.ToString() + @"\" + Dátum.Value.ToString("yyyyMMdd") + "Forte.mdb";
                string jelszó = "lilaakác";

                // leellenőrizzük, hogy az adat nap egyezik-e
                string szöveg = MyE.Beolvas("a2").Trim().Replace(".", "");
                if (MyE.Beolvas("a2").Trim().Replace(".", "") != Dátum.Value.ToString("yyyyMMdd"))
                {
                    // ha nem egyezik akkor
                    MessageBox.Show("A betölteni kívánt adatok nem egyeznek meg a beállított nappal !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // az excel tábla bezárása
                    MyE.ExcelBezárás();
                    Holtart.Ki();
                    return;
                }

                if (!File.Exists(hely))
                {
                    MessageBox.Show("A választott napra még nincs feltöltve adat ! ", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MyE.ExcelBezárás();
                    Holtart.Ki();
                    return;
                }


                // megnézzük, hogy hány sorból áll a tábla
                int ii = 4;
                int utolsó = 0;

                while (MyE.Beolvas("a" + ii.ToString()).Trim() != "_")
                {
                    utolsó = ii;
                    ii += 1;
                    szöveg = MyE.Beolvas("a" + ii.ToString());
                }
                string ideig;
                string viszonylat;
                Holtart.Be(utolsó + 1);
                if (utolsó > 1)
                {
                    // megnyitjuk a táblát
                    List<string> SzövegGy = new List<string>();
                    for (int i = 5; i <= utolsó; i++)
                    {

                        szöveg = "INSERT INTO Kidobótábla (viszonylat, forgalmiszám, szolgálatiszám, ";
                        szöveg += " jvez, kezdés, végzés, ";
                        szöveg += " Kezdéshely, Végzéshely, Kód, ";
                        szöveg += " Tárolásihely, Villamos, Megjegyzés, ";
                        szöveg += " szerelvénytípus ) VALUES (";
                        ideig = MyE.Beolvas("a" + i.ToString());
                        viszonylat = "";
                        string[] darab = ideig.Split('/');
                        viszonylat = darab[0].Trim();

                        szöveg += "'" + viszonylat + "', "; // viszonylat
                        szöveg += "'" + MyE.Beolvas("b" + i.ToString()).Trim() + "', "; // forgalmiszám
                        szöveg += "'" + ideig.Trim() + "', "; // szolgálatiszám
                        szöveg += "'" + MyE.Beolvas("d" + i.ToString()).Trim() + "', "; // jvez
                        szöveg += "'" + MyE.Beolvasidő("f" + i.ToString()).ToString("HH:mm:ss") + "', "; // kezdés
                        szöveg += "'" + MyE.Beolvasidő("h" + i.ToString()).ToString("HH:mm:ss") + "', "; // végzés
                        szöveg += "'" + MyE.Beolvas("g" + i.ToString()).Trim() + "', "; // kezdéshely
                        szöveg += "'" + MyE.Beolvas("i" + i.ToString()).Trim() + "', "; // végzéshely
                        szöveg += "'" + MyE.Beolvas("e" + i.ToString()).Trim() + "', "; // kód
                        szöveg += "'_', "; // tárolásihely
                        szöveg += "'_', "; // villamos
                        szöveg += "'_', "; // megjegyzés
                        szöveg += "'" + MyE.Beolvas("k" + i.ToString()).Trim() + "') "; // szerelvénytípus
                        SzövegGy.Add(szöveg);
                        Holtart.Lép();
                    }
                    MyA.ABMódosítás(hely, jelszó, SzövegGy);
                }

                Gombok();

                MyE.ExcelBezárás();
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


        #region Gombok
        private void Alsópanelkitöltés()
        {
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\Segéd\kiegészítő.mdb";

            Kezelő_Telep_Kiegészítő_Kidobó KézKidob = new Kezelő_Telep_Kiegészítő_Kidobó();
            List<Adat_Telep_Kiegészítő_Kidobó> Adatok = KézKidob.Lista_Adatok(hely);

            Adat_Telep_Kiegészítő_Kidobó AdatokKidob = (from a in Adatok
                                                        where a.Id == 1
                                                        select a).FirstOrDefault();

            if (AdatokKidob != null) AlsóPanels = AdatokKidob.Telephely;
        }


        private void Gombok()
        {
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\Kidobó\" + Dátum.Value.Year.ToString() + @"\" + Dátum.Value.ToString("yyyyMMdd") + "Forte.mdb";
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
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\" + Dátum.Value.Year.ToString();
            if (Directory.Exists(hely) == false)
                System.IO.Directory.CreateDirectory(hely);
        }


        private void Command2_Click(object sender, EventArgs e)
        {
            Label18.Text = "Adott napi adatok:";
            Tábla1.Visible = false;
            Tábla.Visible = true;
            Táblaíró();
        }


        private void Táblaíró()
        {
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\" + Dátum.Value.Year.ToString() + @"\" + Dátum.Value.ToString("yyyyMMdd") + "Forte.mdb";
            if (!File.Exists(hely)) return;

            string jelszó = "lilaakác";
            string szöveg = "SELECT * FROM kidobótábla  order by szolgálatiszám";

            Tábla.Rows.Clear();
            Tábla.Columns.Clear();
            Tábla.Refresh();
            Tábla.Visible = false;
            Tábla.ColumnCount = 12;

            // fejléc elkészítése
            Tábla.Columns[0].HeaderText = "Visz.";
            Tábla.Columns[0].Width = 80;
            Tábla.Columns[1].HeaderText = "Forg.";
            Tábla.Columns[1].Width = 80;
            Tábla.Columns[2].HeaderText = "Szolg.";
            Tábla.Columns[2].Width = 100;
            Tábla.Columns[3].HeaderText = "Jvez.";
            Tábla.Columns[3].Width = 250;
            Tábla.Columns[4].HeaderText = "Kezdés";
            Tábla.Columns[4].Width = 100;
            Tábla.Columns[5].HeaderText = "Végzés";
            Tábla.Columns[5].Width = 100;
            Tábla.Columns[6].HeaderText = "Kezdési hely";
            Tábla.Columns[6].Width = 200;
            Tábla.Columns[7].HeaderText = "Végzési hely";
            Tábla.Columns[7].Width = 200;
            Tábla.Columns[8].HeaderText = "Tárolásihely";
            Tábla.Columns[8].Width = 100;
            Tábla.Columns[9].HeaderText = "Kocsi";
            Tábla.Columns[9].Width = 100;
            Tábla.Columns[10].HeaderText = "Megjegyzés";
            Tábla.Columns[10].Width = 100;
            Tábla.Columns[11].HeaderText = "Típus";
            Tábla.Columns[11].Width = 100;

            Kezelő_Kidobó kéz = new Kezelő_Kidobó();
            List<Adat_Kidobó> Adatok = kéz.Lista_Adat(hely, jelszó, szöveg);
            int i;
            foreach (Adat_Kidobó rekord in Adatok)
            {
                Tábla.RowCount++;
                i = Tábla.RowCount - 1;
                Tábla.Rows[i].Cells[0].Value = rekord.Viszonylat.Trim();
                Tábla.Rows[i].Cells[1].Value = rekord.Forgalmiszám.Trim();
                Tábla.Rows[i].Cells[2].Value = rekord.Szolgálatiszám.Trim();
                Tábla.Rows[i].Cells[3].Value = rekord.Jvez.Trim();
                Tábla.Rows[i].Cells[4].Value = rekord.Kezdés.ToString("HH:mm");
                Tábla.Rows[i].Cells[5].Value = rekord.Végzés.ToString("HH:mm");
                Tábla.Rows[i].Cells[6].Value = rekord.Kezdéshely.Trim();
                Tábla.Rows[i].Cells[7].Value = rekord.Végzéshely.Trim();
                Tábla.Rows[i].Cells[8].Value = rekord.Tárolásihely.Trim();
                Tábla.Rows[i].Cells[9].Value = rekord.Villamos.Trim();
                Tábla.Rows[i].Cells[10].Value = rekord.Megjegyzés.Trim();
                Tábla.Rows[i].Cells[11].Value = rekord.Szerelvénytípus.Trim();
            }
            Tábla.Visible = true;
            Tábla.Refresh();
        }


        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // leellenőrizzük, hogy van-e adat
            if (Tábla.Rows.Count < 1)
                return;
            if (e.RowIndex < 0)
                return;

            Napi_Adat = new Adat_Kidobó(
                 Tábla.Rows[e.RowIndex].Cells[0].Value.ToString().Trim(),
                 Tábla.Rows[e.RowIndex].Cells[1].Value.ToString().Trim(),
                 Tábla.Rows[e.RowIndex].Cells[2].Value.ToString().Trim(),
                 Tábla.Rows[e.RowIndex].Cells[3].Value.ToString().Trim(),
                 DateTime.Parse(Tábla.Rows[e.RowIndex].Cells[4].Value.ToString().Trim()),
                 DateTime.Parse(Tábla.Rows[e.RowIndex].Cells[5].Value.ToString().Trim()),
                 Tábla.Rows[e.RowIndex].Cells[6].Value.ToString().Trim(),
                 Tábla.Rows[e.RowIndex].Cells[7].Value.ToString().Trim(),
                 "",
                 Tábla.Rows[e.RowIndex].Cells[8].Value.ToString().Trim(),
                 Tábla.Rows[e.RowIndex].Cells[9].Value.ToString().Trim(),
                 Tábla.Rows[e.RowIndex].Cells[10].Value.ToString().Trim(),
                 Tábla.Rows[e.RowIndex].Cells[11].Value.ToString().Trim()
                );
            Napi_Adatok_Módosítása();

            Segéd_adat = new Adat_Kidobó_Segéd(
                 Tábla.Rows[e.RowIndex].Cells[1].Value.ToString().Trim(),
                 Tábla.Rows[e.RowIndex].Cells[2].Value.ToString().Trim(),
                 DateTime.Parse(Tábla.Rows[e.RowIndex].Cells[4].Value.ToString().Trim()),
                 DateTime.Parse(Tábla.Rows[e.RowIndex].Cells[5].Value.ToString().Trim()),
                 Tábla.Rows[e.RowIndex].Cells[6].Value.ToString().Trim(),
                 Tábla.Rows[e.RowIndex].Cells[7].Value.ToString().Trim(),
                 VáltozatCombo.Text.Trim(),
                 Tábla.Rows[e.RowIndex].Cells[10].Value.ToString().Trim()
                 );
            if (Új_Ablak_Kidobó_Ismétlődő != null) Napi_Adatok_Módosítása();

        }


        private void Napi_Adatok_Módosítása()
        {
            try
            {
                if (Napi_Adat == null)
                    throw new HibásBevittAdat("Nincs kiválasztva elem.");


                Új_Ablak_Kidobó_Napi?.Close();

                Új_Ablak_Kidobó_Napi = new Ablak_Kidobó_Napi(Cmbtelephely.Text.Trim(), Napi_Adat, Dátum.Value, AlsóPanels);
                Új_Ablak_Kidobó_Napi.FormClosed += Új_Ablak_Kidobó_Ismétlődő_Closed;
                Új_Ablak_Kidobó_Napi.Top = 400;
                Új_Ablak_Kidobó_Napi.Left = 600;
                Új_Ablak_Kidobó_Napi.Show();
                Új_Ablak_Kidobó_Napi.Ismétlődő_Változás += Változatok_listázása;
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

        private void Command11_Click(object sender, EventArgs e)
        {
            try
            {
                // változattal felülír
                string helyvált = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\kidobósegéd.mdb";
                string jelszóvált = "erzsébet";

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\" + Dátum.Value.Year.ToString() + @"\" + Dátum.Value.ToString("yyyyMMdd") + "Forte.mdb";
                string jelszó = "lilaakác";

                if (!File.Exists(helyvált)) throw new HibásBevittAdat("Nincs változati fájl.");
                if (!File.Exists(hely)) throw new HibásBevittAdat("Nincs módosítandó fájl.");
                if (VáltozatCombo.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva a változat amivel lehet módosítani.");

                // ha nincs olyan akkor rögzít különben módosít

                string szöveg = $"SELECT * FROM Kidobósegédtábla where változatnév='{VáltozatCombo.Text.Trim()}' order by  szolgálatiszám";

                Holtart.Be(20);

                Kezelő_Kidobó_Segéd kéz = new Kezelő_Kidobó_Segéd();
                List<Adat_Kidobó_Segéd> Adatok = kéz.Lista_Adat(helyvált, jelszóvált, szöveg);


                //Új

                szöveg = "Select * from Kidobótábla";
                Kezelő_Kidobó KézKidobó = new Kezelő_Kidobó();
                List<Adat_Kidobó> AdatokKidobó = KézKidobó.Lista_Adat(hely, jelszó, szöveg);

                Adat_Kidobó AdatKidobó;

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kidobó_Segéd rekord in Adatok)
                {
                    // megkeressük a táblázatban a módosítandó sort
                    AdatKidobó = (from a in AdatokKidobó
                                  where a.Szolgálatiszám == rekord.Szolgálatiszám.Trim()
                                  select a).FirstOrDefault();

                    if (AdatKidobó != null)
                    {
                        szöveg = "UPDATE kidobótábla  SET ";
                        szöveg += "Kezdéshely='" + rekord.Kezdéshely.Trim() + "', ";
                        szöveg += "Végzéshely='" + rekord.Végzéshely.Trim() + "', ";
                        szöveg += "megjegyzés='" + rekord.Megjegyzés.Trim() + "', ";
                        szöveg += " Kezdés='" + rekord.Kezdés.ToString("HH:mm") + "', ";
                        szöveg += " végzés='" + rekord.Végzés.ToString("HH:mm") + "' ";
                        szöveg += " WHERE szolgálatiszám='" + rekord.Szolgálatiszám.Trim() + "'";
                        SzövegGy.Add(szöveg);
                        Holtart.Lép();
                    }
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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
                    Title = "Berendezések adatlap készítés",
                    FileName = "Kidobó_" + Dátum.Value.ToString("yyyy.MM.dd"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Holtart.Be();
                // Létrehozzuk az excelt
                MyE.ExcelLétrehozás();

                MyE.Munkalap_betű("Arial", 16);

                string munkalap = "A változat";
                MyE.Munkalap_átnevezés("Munka1", "A változat");
                MyE.Új_munkalap("B változat");
                MyE.Új_munkalap("Száva változat");

                A_változat();
                B_változat();
                Száva_változat();

                // bezárjuk az Excel-t
                MyE.Munkalap_aktív(munkalap);
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                Holtart.Ki();
                MyE.Megnyitás(fájlexc);
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


        void A_változat()
        {
            try
            {
                string munkalap = "A változat";
                int sor;
                MyE.Munkalap_aktív(munkalap);

                MyE.Oszlopszélesség(munkalap, "a:a", 9);
                MyE.Oszlopszélesség(munkalap, "b:c", 15);
                MyE.Oszlopszélesség(munkalap, "d:d", 9);
                MyE.Oszlopszélesség(munkalap, "e:e", 38);
                MyE.Oszlopszélesség(munkalap, "f:f", 45);
                MyE.Oszlopszélesség(munkalap, "g:g", 10);
                MyE.Oszlopszélesség(munkalap, "h:h", 40);
                MyE.Oszlopszélesség(munkalap, "i:i", 5);
                MyE.Oszlopszélesség(munkalap, "j:j", 9);
                MyE.Egyesít(munkalap, "a1:j1");
                MyE.Betű("a1", 36);
                MyE.Sormagasság("1:1", 45);
                MyE.Kiir(Dátum.Value.ToString("yyyy.MMMM.dd. dddd"), "a1");
                MyE.Egyesít(munkalap, "a3:i3");
                sor = 3;
                MyE.Betű("a" + sor.ToString(), 30);
                MyE.Kiir("Délelőtti kiállók", "A" + sor.ToString());
                MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 39);
                sor += 1;
                // fejléc
                // délelőtti lista
                MyE.Kiir("Szolg.szám", "b" + sor.ToString());
                MyE.Kiir("Forg.szám", "c" + sor.ToString());
                MyE.Kiir("Kezd", "a" + sor.ToString());
                MyE.Kiir("Végez", "d" + sor.ToString());
                MyE.Kiir("Név", "e" + sor.ToString());
                MyE.Kiir("Pályaszám(ok)", "f" + sor.ToString());
                MyE.Kiir("Vágány", "g" + sor.ToString());
                MyE.Kiir("Megjegyzés", "h" + sor.ToString());
                MyE.Kiir("Típus", "j" + sor.ToString());
                MyE.Vastagkeret("a" + sor.ToString() + ":j" + sor.ToString());

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\" + Dátum.Value.Year.ToString() + @"\" + Dátum.Value.ToString("yyyyMMdd") + "Forte.mdb";
                string jelszó = "lilaakác";
                string szöveg = "SELECT * FROM kidobótábla where Kezdéshely='" + AlsóPanels.Trim() + "'";
                szöveg += " and [Kezdés]< #12:00:00#  order by kezdés";

                Kezelő_Kidobó kéz = new Kezelő_Kidobó();
                List<Adat_Kidobó> Adatok = kéz.Lista_Adat(hely, jelszó, szöveg);

                Holtart.Be(20);
                int i = 0;
                foreach (Adat_Kidobó rekord in Adatok)
                {
                    sor += 1;
                    MyE.Kiir(rekord.Szolgálatiszám.Trim() + "_", "b" + sor.ToString());
                    MyE.Kiir(rekord.Forgalmiszám.Trim(), "c" + sor.ToString());
                    MyE.Kiir(rekord.Kezdés.ToString("HH:mm"), "a" + sor.ToString());
                    MyE.Kiir(rekord.Végzés.ToString("HH:mm"), "d" + sor.ToString());
                    MyE.Kiir(rekord.Jvez.Trim(), "e" + sor.ToString());
                    MyE.Kiir(rekord.Villamos.Trim(), "f" + sor.ToString());
                    MyE.Kiir(rekord.Tárolásihely.Trim(), "g" + sor.ToString());
                    MyE.Kiir(rekord.Megjegyzés.Trim(), "h" + sor.ToString());
                    MyE.Kiir((i + 1).ToString(), "i" + sor.ToString());
                    MyE.Kiir(rekord.Szerelvénytípus.Trim(), "j" + sor.ToString());
                    if (rekord.Végzés.Hour < 12 && rekord.Végzéshely.Trim() == AlsóPanels.Trim())
                    {
                        // a beállókat kiemeli
                        MyE.Betű("a" + sor.ToString() + ":j" + sor.ToString(), false, false, true);
                        MyE.Háttérszín("a" + sor.ToString() + ":j" + sor.ToString(), Color.Yellow);
                    }
                    i++;
                    Holtart.Lép();
                }


                MyE.Rácsoz("a5:j" + sor.ToString());
                MyE.Vastagkeret("a5:j" + sor.ToString());
                MyE.Sormagasság("5:" + sor.ToString(), 30);
                sor += 2;

                MyE.Egyesít(munkalap, "a" + sor.ToString() + ":j" + sor.ToString());
                MyE.Kiir("Délutáni kiállók", "A" + sor.ToString());
                MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 39);
                MyE.Betű("a" + sor.ToString(), 30);
                sor += 1;
                int blokkeleje = sor;
                // fejléc
                // délutáni lista
                MyE.Kiir("Szolg.szám", "b" + sor.ToString());
                MyE.Kiir("Forg.szám", "c" + sor.ToString());
                MyE.Kiir("Kezd", "a" + sor.ToString());
                MyE.Kiir("Végez", "d" + sor.ToString());
                MyE.Kiir("Név", "e" + sor.ToString());
                MyE.Kiir("Pályaszám(ok)", "f" + sor.ToString());
                MyE.Kiir("Vágány", "g" + sor.ToString());
                MyE.Kiir("Megjegyzés", "h" + sor.ToString());
                MyE.Kiir("Típus", "j" + sor.ToString());
                MyE.Vastagkeret("a" + sor.ToString() + ":j" + sor.ToString());

                szöveg = "SELECT * FROM kidobótábla where Kezdéshely='" + AlsóPanels.Trim() + "'";
                szöveg += " and [Kezdés]> #12:00:00#  order by kezdés";
                Holtart.Lép();
                Adatok = kéz.Lista_Adat(hely, jelszó, szöveg);

                i = 0;
                Holtart.Lép();

                foreach (Adat_Kidobó rekord in Adatok)
                {


                    sor += 1;
                    MyE.Kiir(rekord.Szolgálatiszám.Trim() + "_", "b" + sor.ToString());
                    MyE.Kiir(rekord.Forgalmiszám.Trim(), "c" + sor.ToString());
                    MyE.Kiir(rekord.Kezdés.ToString("HH:mm"), "a" + sor.ToString());
                    MyE.Kiir(rekord.Végzés.ToString("HH:mm"), "d" + sor.ToString());
                    MyE.Kiir(rekord.Jvez.Trim(), "e" + sor.ToString());
                    MyE.Kiir(rekord.Villamos.Trim(), "f" + sor.ToString());
                    MyE.Kiir(rekord.Tárolásihely.Trim(), "g" + sor.ToString());
                    MyE.Kiir(rekord.Megjegyzés.Trim(), "h" + sor.ToString());
                    MyE.Kiir((i + 1).ToString(), "i" + sor.ToString());
                    MyE.Kiir(rekord.Szerelvénytípus.Trim(), "j" + sor.ToString());
                    i++;
                    Holtart.Lép();
                }


                MyE.Rácsoz("a" + (blokkeleje + 1).ToString() + ":j" + sor.ToString());
                MyE.Vastagkeret("a" + blokkeleje.ToString() + ":j" + sor.ToString());
                MyE.Sormagasság(blokkeleje.ToString() + ":" + sor.ToString(), 30);

                // ////////////////////////////////////
                // / telepen kívüli váltások
                // ////////////////////////////////////
                sor += 2;

                MyE.Egyesít(munkalap, "a" + sor.ToString() + ":j" + sor.ToString());
                MyE.Kiir("Végállomási kezdők", "A" + sor.ToString());
                MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 39);
                MyE.Betű("a" + sor.ToString(), 30);
                sor += 1;
                blokkeleje = sor;
                // fejléc

                MyE.Kiir("Szolg.szám", "b" + sor.ToString());
                MyE.Kiir("Forg.szám", "c" + sor.ToString());
                MyE.Kiir("Kezd", "a" + sor.ToString());
                MyE.Kiir("Végez", "d" + sor.ToString());
                MyE.Kiir("Név", "e" + sor.ToString());
                MyE.Kiir("Pályaszám(ok)", "f" + sor.ToString());
                MyE.Kiir("Vágány", "g" + sor.ToString());
                MyE.Kiir("Megjegyzés", "h" + sor.ToString());
                MyE.Vastagkeret("a" + sor.ToString() + ":i" + sor.ToString());
                MyE.Kiir("Típus", "j" + sor.ToString());

                szöveg = "SELECT * FROM kidobótábla where Kezdéshely<>'" + AlsóPanels.Trim() + "'  order by kezdés";

                Adatok = kéz.Lista_Adat(hely, jelszó, szöveg);

                i = 0;
                Holtart.Lép();

                foreach (Adat_Kidobó rekord in Adatok)
                {
                    sor += 1;
                    MyE.Kiir(rekord.Szolgálatiszám.Trim() + "_", "b" + sor.ToString());
                    MyE.Kiir(rekord.Forgalmiszám.Trim(), "c" + sor.ToString());
                    MyE.Kiir(rekord.Kezdés.ToString("HH:mm"), "a" + sor.ToString());
                    MyE.Kiir(rekord.Végzés.ToString("HH:mm"), "d" + sor.ToString());
                    MyE.Kiir(rekord.Jvez.Trim(), "e" + sor.ToString());
                    MyE.Kiir(rekord.Villamos.Trim(), "f" + sor.ToString());
                    MyE.Kiir(rekord.Tárolásihely.Trim(), "g" + sor.ToString());
                    MyE.Kiir(rekord.Megjegyzés.Trim(), "h" + sor.ToString());
                    MyE.Kiir((i + 1).ToString(), "i" + sor.ToString());
                    MyE.Kiir(rekord.Szerelvénytípus.Trim(), "j" + sor.ToString());
                    i++;
                    Holtart.Lép();
                }


                MyE.Rácsoz("a" + (blokkeleje + 1).ToString() + ":j" + sor.ToString());
                MyE.Vastagkeret("a" + blokkeleje.ToString() + ":j" + sor.ToString());
                MyE.Sormagasság(blokkeleje.ToString() + ":" + sor.ToString(), 30);

                MyE.NyomtatásiTerület_részletes(munkalap, "A1:J" + sor.ToString(), "", "", true);
                MyE.Munkalap_aktív(munkalap);
                MyE.Aktív_Cella(munkalap, "A1");
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

            // ***********************************
            // * Másik változat  B
            // ***********************************
            string munkalap = "B változat";

            MyE.Munkalap_aktív(munkalap);
            MyE.Munkalap_betű("Arial", 16);

            int sor;
            int blokkeleje;

            MyE.Oszlopszélesség(munkalap, "a:a", 9);
            MyE.Oszlopszélesség(munkalap, "b:c", 15);
            MyE.Oszlopszélesség(munkalap, "d:d", 9);
            MyE.Oszlopszélesség(munkalap, "e:e", 38);
            MyE.Oszlopszélesség(munkalap, "f:f", 24);
            MyE.Oszlopszélesség(munkalap, "g:g", 30);
            MyE.Oszlopszélesség(munkalap, "h:h", 40);
            MyE.Oszlopszélesség(munkalap, "i:i", 9);
            MyE.Egyesít(munkalap, "a1:i1");
            MyE.Betű("a1", 36);
            MyE.Sormagasság("1:1", 45);
            MyE.Kiir(Dátum.Value.ToString("yyyy.MMMM.dd. dddd"), "a1");
            MyE.Egyesít(munkalap, "a3:h3");
            sor = 3;
            MyE.Betű("a" + sor.ToString(), 30);
            MyE.Kiir("Délelőtti kiállók", "A" + sor.ToString());
            MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 39);
            sor += 1;
            // fejléc
            // délelőtti lista
            MyE.Kiir("Szolg.szám", "b" + sor.ToString());
            MyE.Kiir("Forg.szám", "c" + sor.ToString());
            MyE.Kiir("Kezd", "a" + sor.ToString());
            MyE.Kiir("Végez", "d" + sor.ToString());
            MyE.Kiir("Név", "e" + sor.ToString());
            MyE.Kiir("Pályaszám(ok)", "f" + sor.ToString());
            MyE.Kiir("Vágány", "g" + sor.ToString());
            MyE.Kiir("Megjegyzés", "h" + sor.ToString());
            MyE.Kiir("Típus", "i" + sor.ToString());
            MyE.Vastagkeret("a" + sor.ToString() + ":i" + sor.ToString());

            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\" + Dátum.Value.Year.ToString() + @"\" + Dátum.Value.ToString("yyyyMMdd") + "Forte.mdb";
            string jelszó = "lilaakác";
            string szöveg = "SELECT * FROM kidobótábla where Kezdéshely='" + AlsóPanels.Trim() + "'";
            szöveg += " and [Kezdés]< #12:00:00# order by viszonylat,kezdés";

            Kezelő_Kidobó kéz = new Kezelő_Kidobó();
            List<Adat_Kidobó> Adatok = kéz.Lista_Adat(hely, jelszó, szöveg);

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
                    MyE.Egyesít(munkalap, "a" + sor.ToString() + ":i" + sor.ToString());
                    MyE.Kiir(rekord.Viszonylat.Trim() + " Viszonylat", "A" + sor.ToString());
                    MyE.Háttérszín("A" + sor.ToString(), Color.Yellow);
                }

                if (utolsóviszonylat.Trim() != rekord.Viszonylat.Trim())
                {
                    sor += 1;
                    MyE.Kiir("Tartalék:", "e" + sor.ToString());
                    MyE.Háttérszín("e" + sor.ToString(), Color.Green);
                    sor += 1;
                    MyE.Egyesít(munkalap, "a" + sor.ToString() + ":i" + sor.ToString());
                    MyE.Kiir(rekord.Viszonylat.Trim() + " Viszonylat", "A" + sor.ToString());
                    MyE.Háttérszín("A" + sor.ToString(), Color.Yellow);
                    utolsóviszonylat = rekord.Viszonylat.Trim();
                }
                sor += 1;
                MyE.Kiir(rekord.Szolgálatiszám.Trim() + "_", "b" + sor.ToString());
                MyE.Kiir(rekord.Forgalmiszám.Trim(), "c" + sor.ToString());
                MyE.Kiir(rekord.Kezdés.ToString("HH: mm"), "a" + sor.ToString());
                MyE.Kiir(rekord.Végzés.ToString("HH: mm"), "d" + sor.ToString());
                MyE.Kiir(rekord.Jvez.Trim(), "e" + sor.ToString());
                MyE.Kiir(rekord.Villamos.Trim(), "f" + sor.ToString());
                MyE.Kiir(rekord.Tárolásihely.Trim(), "g" + sor.ToString());
                MyE.Kiir(rekord.Megjegyzés.Trim(), "h" + sor.ToString());
                MyE.Kiir(rekord.Szerelvénytípus.Trim(), "i" + sor.ToString());
                if (rekord.Végzés.Hour < 12 && rekord.Végzéshely.Trim() == AlsóPanels.Trim())
                {
                    // a beállókat kiemeli
                    MyE.Betű("a" + sor.ToString() + ":i" + sor.ToString(), false, false, true);
                    MyE.Háttérszín("a" + sor.ToString() + ":i" + sor.ToString(), Color.Yellow);
                }
                i++;
                Holtart.Lép();
            }


            sor += 1;
            MyE.Kiir("Tartalék:", "e" + sor.ToString());
            MyE.Háttérszín("e" + sor.ToString(), Color.Green);
            MyE.Rácsoz("a5:i" + sor.ToString());
            MyE.Vastagkeret("a5:i" + sor.ToString());
            MyE.Sormagasság("5:" + sor.ToString(), 30);
            sor += 2;

            MyE.Egyesít(munkalap, "a" + sor.ToString() + ":i" + sor.ToString());
            MyE.Kiir("Délutáni kiállók", "A" + sor.ToString());
            MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 39);
            MyE.Betű("a" + sor.ToString(), 30);
            sor += 1;
            blokkeleje = sor;
            // fejléc
            // délutáni lista
            MyE.Kiir("Szolg.szám", "b" + sor.ToString());
            MyE.Kiir("Forg.szám", "c" + sor.ToString());
            MyE.Kiir("Kezd", "a" + sor.ToString());
            MyE.Kiir("Végez", "d" + sor.ToString());
            MyE.Kiir("Név", "e" + sor.ToString());
            MyE.Kiir("Pályaszám(ok)", "f" + sor.ToString());
            MyE.Kiir("Vágány", "g" + sor.ToString());
            MyE.Kiir("Megjegyzés", "h" + sor.ToString());
            MyE.Kiir("Típus", "i" + sor.ToString());
            MyE.Vastagkeret("a" + sor.ToString() + ":i" + sor.ToString());

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
                    MyE.Egyesít(munkalap, "a" + sor.ToString() + ":i" + sor.ToString());
                    MyE.Kiir(rekord.Viszonylat.Trim() + " Viszonylat", "A" + sor.ToString());
                    MyE.Háttérszín("A" + sor.ToString(), Color.Yellow);
                }

                if (utolsóviszonylat.Trim() != rekord.Viszonylat.Trim())
                {
                    sor += 1;
                    MyE.Kiir("Tartalék:", "e" + sor.ToString());
                    MyE.Háttérszín("e" + sor.ToString(), Color.Green);
                    sor += 1;
                    MyE.Egyesít(munkalap, "a" + sor.ToString() + ":i" + sor.ToString());
                    MyE.Kiir(rekord.Viszonylat.Trim() + " Viszonylat", "A" + sor.ToString());

                    MyE.Háttérszín("A" + sor.ToString(), Color.Yellow);
                    utolsóviszonylat = rekord.Viszonylat.Trim();
                }
                sor += 1;

                MyE.Kiir(rekord.Szolgálatiszám.Trim() + "_", "b" + sor.ToString());
                MyE.Kiir(rekord.Forgalmiszám.Trim(), "c" + sor.ToString());
                MyE.Kiir(rekord.Kezdés.ToString("HH: mm"), "a" + sor.ToString());
                MyE.Kiir(rekord.Végzés.ToString("HH: mm"), "d" + sor.ToString());
                MyE.Kiir(rekord.Jvez.Trim(), "e" + sor.ToString());
                MyE.Kiir(rekord.Villamos.Trim(), "f" + sor.ToString());
                MyE.Kiir(rekord.Tárolásihely.Trim(), "g" + sor.ToString());
                MyE.Kiir(rekord.Megjegyzés.Trim(), "h" + sor.ToString());
                MyE.Kiir(rekord.Szerelvénytípus.Trim(), "i" + sor.ToString());

                i++;
                Holtart.Lép();
            }

            sor += 1;
            MyE.Kiir("Tartalék:", "e" + sor.ToString());
            MyE.Háttérszín("e" + sor.ToString(), Color.Green);
            MyE.Rácsoz("a" + (blokkeleje + 1).ToString() + ":i" + sor.ToString());
            MyE.Vastagkeret("a" + blokkeleje.ToString() + ":i" + sor.ToString());
            MyE.Sormagasság(blokkeleje.ToString() + ":" + sor.ToString(), 30);
            // nyomtatási beállítások
            MyE.NyomtatásiTerület_részletes(munkalap, "A1:I" + sor.ToString(), "", "", true);
            MyE.Munkalap_aktív(munkalap);
            MyE.Aktív_Cella(munkalap, "A1");
        }


        private void Száva_változat()
        {
            string munkalap = "Száva változat";

            MyE.Munkalap_aktív(munkalap);
            MyE.Munkalap_betű("Arial", 10);

            MyE.Sormagasság("1:43", 24);
            MyE.Sormagasság("44:60", 48);

            // Oszlop szélességek
            MyE.Oszlopszélesség(munkalap, "a:a", 7);
            MyE.Oszlopszélesség(munkalap, "b:b", 7);
            MyE.Oszlopszélesség(munkalap, "c:c", 7);
            MyE.Oszlopszélesség(munkalap, "d:d", 4);
            MyE.Oszlopszélesség(munkalap, "e:e", 10);
            MyE.Oszlopszélesség(munkalap, "f:f", 18);
            MyE.Oszlopszélesség(munkalap, "g:g", 7);
            // jobb oldal
            MyE.Oszlopszélesség(munkalap, "h:h", 7);
            MyE.Oszlopszélesség(munkalap, "i:i", 7);
            MyE.Oszlopszélesség(munkalap, "j:j", 7);
            MyE.Oszlopszélesség(munkalap, "k:k", 4);
            MyE.Oszlopszélesség(munkalap, "l:l", 10);
            MyE.Oszlopszélesség(munkalap, "m:m", 10);
            MyE.Oszlopszélesség(munkalap, "n:n", 7);
            MyE.Oszlopszélesség(munkalap, "o:o", 7);
            // fejléc készítés
            MyE.Egyesít(munkalap, "a1:o1");
            MyE.Kiir(Dátum.Value.ToString("yyyy.MM.dd dddd"), "a1");
            MyE.Betű("a1", 14);
            MyE.Betű("a1", false, false, true);
            MyE.Vastagkeret("a1:o1");

            MyE.Kiir("Sz.", "a2");
            MyE.Kiir("Fo.", "b2");
            MyE.Kiir("Kezd", "c2");
            MyE.Kiir("Vá.", "d2");
            MyE.Kiir("Kocsi", "e2");
            MyE.Kiir("Név", "f2");
            MyE.Kiir("Beáll", "g2");
            MyE.Rácsoz("a2:g2");
            MyE.Vastagkeret("a2:g2");
            // jobb oldal
            MyE.Kiir("Sz.", "h2");
            MyE.Kiir("Fo.", "i2");
            MyE.Kiir("Kezd", "j2");
            MyE.Kiir("Vá.", "k2");
            MyE.Kiir("Kocsi", "l2");
            MyE.Egyesít(munkalap, "m2:n2");
            MyE.Kiir("Név", "m2");
            MyE.Kiir("Beáll", "o2");
            MyE.Rácsoz("h2:o2");
            MyE.Vastagkeret("h2:o2");

            int sor = 2;
            int blokkeleje = 4;
            int baloldal = 1;
            string ideig;

            int[] típusdb = new int[Forte_típus.Items.Count + 1];

            // érdemi rész
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\" + Dátum.Value.Year.ToString() + @"\" + Dátum.Value.ToString("yyyyMMdd") + "Forte.mdb";
            string jelszó = "lilaakác";
            string szöveg = "SELECT * FROM kidobótábla where Kezdéshely='" + AlsóPanels.Trim() + "' order by viszonylat,kezdés";

            Kezelő_Kidobó kéz = new Kezelő_Kidobó();
            List<Adat_Kidobó> Adatok = kéz.Lista_Adat(hely, jelszó, szöveg);

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
                    MyE.Egyesít(munkalap, "a" + sor.ToString() + ":d" + sor.ToString());
                    MyE.Egyesít(munkalap, "e" + sor.ToString() + ":g" + sor.ToString());
                    if (MyF.Szöveg_Tisztítás(rekord.Viszonylat, 3, 1) == "0")
                        ideig = MyF.Szöveg_Tisztítás(rekord.Viszonylat, 1, 2);
                    else
                        ideig = MyF.Szöveg_Tisztítás(rekord.Viszonylat, 1, 3);

                    MyE.Kiir(ideig + " Viszonylat", "a" + sor.ToString());
                    MyE.Háttérszín("A" + sor.ToString(), Color.Yellow);
                    MyE.Betű("A" + sor.ToString(), 16);
                    MyE.Vastagkeret("a" + sor.ToString() + ":d" + sor.ToString());
                    MyE.Betű("e" + sor.ToString(), 16);
                    MyE.Vastagkeret("e" + sor.ToString() + ":g" + sor.ToString());

                    // lenullázuk a darabszámokat
                    for (int j = 0; j < Forte_típus.Items.Count; j++)
                        típusdb[j] = 0;
                }

                if (utolsóviszonylat.Trim() != rekord.Viszonylat.Trim())
                {
                    // rácsozás
                    if (baloldal == 1)
                    {
                        MyE.Rácsoz("a" + blokkeleje.ToString() + ":g" + sor.ToString());
                        MyE.Vastagkeret("a" + blokkeleje.ToString() + ":g" + sor.ToString());
                    }
                    else
                    {
                        MyE.Rácsoz("h" + blokkeleje.ToString() + ":o" + sor.ToString());
                        MyE.Vastagkeret("h" + blokkeleje.ToString() + ":o" + sor.ToString());
                    }

                    // típus darabszámok
                    ideig = "";

                    for (int j = 0; j < Forte_típus.Items.Count; j++)
                    {
                        if (típusdb[j] != 0)
                            ideig += típusdb[j].ToString() + "-" + Forte_típus.Items[j].ToString() + ";";

                        // lenullázzuk
                        típusdb[j] = 0;
                    }
                    if (baloldal == 1)
                        MyE.Kiir(ideig, "e" + (blokkeleje - 1).ToString());
                    else
                        MyE.Kiir(ideig, "l" + (blokkeleje - 1).ToString());


                    // ha új viszonylat lesz
                    sor += 1;
                    if (sor == 38)
                    {
                        // az eddigi rácsozása
                        MyE.Rácsoz("a" + blokkeleje.ToString() + ":g" + (sor - 1).ToString());
                        MyE.Vastagkeret("a" + blokkeleje.ToString() + ":g" + (sor - 1).ToString());
                        sor = 3;
                        baloldal = 2;
                        blokkeleje = 3;
                    }

                    if (baloldal == 1)
                    {
                        MyE.Egyesít(munkalap, "a" + sor.ToString() + ":d" + sor.ToString());
                        MyE.Egyesít(munkalap, "e" + sor.ToString() + ":g" + sor.ToString());

                        if (MyF.Szöveg_Tisztítás(rekord.Viszonylat, 3, 1) == "0")
                            ideig = MyF.Szöveg_Tisztítás(rekord.Viszonylat, 1, 2);
                        else
                            ideig = MyF.Szöveg_Tisztítás(rekord.Viszonylat, 1, 3);


                        MyE.Kiir(ideig + " Viszonylat", "A" + sor.ToString());

                        MyE.Betű("A" + sor.ToString(), 16);
                        MyE.Háttérszín("A" + sor.ToString(), Color.Yellow);
                        MyE.Vastagkeret("a" + sor.ToString() + ":d" + sor.ToString());
                        MyE.Betű("e" + sor.ToString(), 16);
                        MyE.Vastagkeret("e" + sor.ToString() + ":g" + sor.ToString());

                        utolsóviszonylat = rekord.Viszonylat.Trim();
                    }
                    else
                    {
                        MyE.Egyesít(munkalap, "h" + sor.ToString() + ":k" + sor.ToString());
                        MyE.Egyesít(munkalap, "l" + sor.ToString() + ":o" + sor.ToString());
                        MyE.Kiir(rekord.Viszonylat.Trim() + " Viszonylat", "h" + sor.ToString());

                        if (MyF.Szöveg_Tisztítás(rekord.Viszonylat, 3, 1) == "0")
                            ideig = MyF.Szöveg_Tisztítás(rekord.Viszonylat, 1, 2);
                        else
                            ideig = MyF.Szöveg_Tisztítás(rekord.Viszonylat, 1, 3);

                        MyE.Kiir(ideig + " Viszonylat", "h" + sor.ToString());

                        MyE.Betű("h" + sor.ToString(), 16);
                        MyE.Háttérszín("h" + sor.ToString(), Color.Yellow);
                        MyE.Vastagkeret("h" + sor.ToString() + ":k" + sor.ToString());
                        MyE.Vastagkeret("l" + sor.ToString() + ":o" + sor.ToString());
                        MyE.Betű("l" + sor.ToString(), 16);
                        utolsóviszonylat = rekord.Viszonylat.Trim();

                        blokkeleje = sor + 1;
                    }
                }

                sor += 1;

                if (sor == 38)
                {
                    // az eddigi rácsozása
                    MyE.Rácsoz("a" + blokkeleje.ToString() + ":g" + (sor - 1).ToString());
                    MyE.Vastagkeret("a" + blokkeleje.ToString() + ":g" + (sor - 1).ToString());
                    sor = 3;
                    baloldal = 2;
                    blokkeleje = 3;
                }

                // *********************************************
                // Adatok kiírása
                // *********************************************
                // típust számolunk
                // lenullázuk a darabszámokat

                for (int j = 0; j < Forte_típus.Items.Count; j++)
                {
                    if (rekord.Kezdés.Hour < 12)
                    {
                        if (Forte_típus.Items[j].ToString().Trim() == MyF.Szöveg_Tisztítás(rekord.Szerelvénytípus, 0, Forte_típus.Items[j].ToString().Length))
                            típusdb[j] += 1;
                    }
                }

                if (baloldal == 1)
                {
                    // bal oldal
                    ideig = rekord.Szolgálatiszám.Trim();
                    ideig = ideig.Replace(rekord.Viszonylat.Trim() + '/', "");

                    MyE.Kiir(ideig + "_", "a" + sor.ToString());
                    if (MyF.Szöveg_Tisztítás(rekord.Szerelvénytípus, 0, 3) == "CAF" && rekord.Kezdés.Hour < 12)
                        MyE.Háttérszíninverz("a" + sor.ToString(), Color.Black);

                    MyE.Kiir(rekord.Forgalmiszám.Trim(), "b" + sor.ToString());
                    DateTime ideigdátum = rekord.Kezdés.AddMinutes(20);

                    MyE.Kiir(ideigdátum.ToString("HH:mm"), "c" + sor.ToString());
                    MyE.Kiir(rekord.Végzés.ToString("HH: mm"), "g" + sor.ToString());
                    MyE.Betű("b" + sor.ToString(), 12);
                    MyE.Betű("c" + sor.ToString(), 12);
                    MyE.Betű("g" + sor.ToString(), 12);

                    MyE.Kiir(rekord.Jvez.Trim(), "f" + sor.ToString());
                    MyE.Kicsinyít("f" + sor.ToString());

                    if (ideigdátum.Hour > 12)
                    {
                        MyE.Betű("a" + sor.ToString() + ":g" + sor.ToString(), false, false, true);
                        MyE.Háttérszín("a" + sor.ToString() + ":g" + sor.ToString(), Color.Yellow);
                    }

                    if (rekord.Végzés.Hour < 12 && rekord.Végzéshely.Trim() == AlsóPanels.Trim())
                    {
                        // a beállókat kiemeli
                        MyE.Betű("f" + sor.ToString() + ":g" + sor.ToString(), false, false, true);
                        MyE.Háttérszín("f" + sor.ToString() + ":g" + sor.ToString(), Color.Yellow);
                    }
                }
                else
                {
                    ideig = rekord.Szolgálatiszám.Trim();
                    ideig = ideig.Replace(rekord.Viszonylat.Trim() + '/', "");
                    MyE.Kiir(ideig + "_", "h" + sor.ToString());
                    if (MyF.Szöveg_Tisztítás(rekord.Szerelvénytípus, 0, 3) == "CAF")
                        MyE.Háttérszíninverz("h" + sor.ToString(), Color.Black);

                    MyE.Kiir(rekord.Forgalmiszám.Trim(), "i" + sor.ToString());
                    DateTime ideigdátum = rekord.Kezdés.AddMinutes(20);
                    MyE.Kiir(ideigdátum.ToString("HH:mm"), "j" + sor.ToString());
                    MyE.Kiir(rekord.Végzés.ToString("HH: mm"), "o" + sor.ToString());
                    MyE.Egyesít(munkalap, "m" + sor.ToString() + ":n" + sor.ToString());
                    MyE.Betű("i" + sor.ToString(), 12);
                    MyE.Betű("j" + sor.ToString(), 12);
                    MyE.Betű("o" + sor.ToString(), 12);
                    MyE.Kiir(rekord.Jvez.Trim(), "m" + sor.ToString());
                    MyE.Kicsinyít("m" + sor.ToString());

                    if (ideigdátum.Hour > 12)
                    {
                        MyE.Betű("h" + sor.ToString() + ":o" + sor.ToString(), false, false, true);
                        MyE.Háttérszín("h" + sor.ToString() + ":o" + sor.ToString(), Color.Yellow);
                    }

                    if (rekord.Végzés.Hour < 12 && rekord.Végzéshely.Trim() == AlsóPanels.Trim())
                    {
                        // a beállókat kiemeli
                        MyE.Betű("m" + sor.ToString() + ":o" + sor.ToString(), false, false, true);
                        MyE.Háttérszín("m" + sor.ToString() + ":o" + sor.ToString(), Color.Yellow);
                    }

                }

                i++;
                Holtart.Lép();
            }

            // utolsó felvonás
            if (baloldal == 1)
            {
                MyE.Rácsoz("a" + blokkeleje.ToString() + ":g" + sor.ToString());
                MyE.Vastagkeret("a" + blokkeleje.ToString() + ":g" + sor.ToString());
            }
            else
            {
                MyE.Rácsoz("h" + blokkeleje.ToString() + ":o" + sor.ToString());
                MyE.Vastagkeret("h" + blokkeleje.ToString() + ":o" + sor.ToString());
            }

            // típus darabszámok
            ideig = "";

            for (int j = 0; j < Forte_típus.Items.Count; j++)
            {
                if (típusdb[j] != 0)
                    ideig += típusdb[j].ToString() + "-" + Forte_típus.Items[j].ToString().Trim() + ";";

                // lenullázzuk
                típusdb[j] = 0;
            }
            if (baloldal == 1)

                MyE.Kiir(ideig, "e" + (blokkeleje - 1).ToString());
            else
                MyE.Kiir(ideig, "l" + (blokkeleje - 1).ToString());

            // hátoldal

            // fejléc készítés
            MyE.Egyesít(munkalap, "a40:o40");
            MyE.Kiir(Dátum.Value.ToString("yyyy.MM.dd dddd"), "a40");
            MyE.Betű("a40", 14);
            MyE.Betű("a40", false, false, true);
            MyE.Vastagkeret("a40:o40");

            MyE.Egyesít(munkalap, "a41:o41");
            MyE.Vastagkeret("a41:o41");

            MyE.Egyesít(munkalap, "a42:o42");
            MyE.Vastagkeret("a42:o42");
            MyE.Kiir("MŰSZAKI RÉSZ", "a42");

            // fejléc
            MyE.Kiir("Visz", "a43");
            MyE.Kiir("Idő", "b43");
            MyE.Egyesít(munkalap, "c43:d43");
            MyE.Kiir("Kocsi szám", "c43");
            MyE.Egyesít(munkalap, "e43:g43");
            MyE.Kiir("Beírt hiba", "e43");
            MyE.Egyesít(munkalap, "h43:l43");
            MyE.Kiir("Javított hiba", "h43");
            MyE.Kiir("Csere kocsi", "m43");
            MyE.Egyesít(munkalap, "n43:o43");
            MyE.Kiir("Csere ideje, helye", "n43");
            MyE.Rácsoz("a43:o43");
            MyE.Vastagkeret("a43:o43");
            for (int ii = 44; ii <= 60; ii++)
            {
                MyE.Egyesít(munkalap, "c" + ii.ToString() + ":d" + ii.ToString());
                MyE.Egyesít(munkalap, "e" + ii.ToString() + ":g" + ii.ToString());
                MyE.Egyesít(munkalap, "h" + ii.ToString() + ":l" + ii.ToString());
                MyE.Egyesít(munkalap, "n" + ii.ToString() + ":o" + ii.ToString());
            }
            MyE.Rácsoz("a44:o60");
            MyE.Vastagkeret("a44:o60");
            MyE.FerdeVonal("A44:A60");

            // nyomtatási beállítás
            MyE.NyomtatásiTerület_részletes(munkalap, "A1:O60",
                                balMargó: 0.196850393700787d, jobbMargó: 0.196850393700787d,
                                alsóMargó: 0.196850393700787d, felsőMargó: 0.196850393700787d, oldalszéles: "1", oldalmagas: "");
            MyE.Nyom_Oszt(munkalap, "A40", 40, oldaltörés: 2);

            MyE.Munkalap_aktív(munkalap);
            MyE.Aktív_Cella(munkalap, "A1");
        }

        #endregion


        #region Kereső

        private void Keresés_táblázatban()
        {
            try
            {
                // megkeressük a szöveget a táblázatban
                if (Új_Ablak_Kereső.Keresendő == null)
                    return;
                if (Új_Ablak_Kereső.Keresendő.Trim() == "")
                    return;

                if (Tábla.Rows.Count < 0)
                    return;

                for (int i = 0; i < Tábla.Rows.Count; i++)
                {
                    for (int j = 0; j < Tábla.Columns.Count; j++)
                    {
                        if (Tábla.Rows[i].Cells[j].Value.ToString().Trim().Contains(Új_Ablak_Kereső.Keresendő.Trim()))
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
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\kidobósegéd.mdb";
            string jelszó = "erzsébet";
            string szöveg;

            // ha nincs olyan akkor rögzít különben módosít
            if (VáltozatCombo.Text.Trim() == "")
                szöveg = "SELECT * FROM Kidobósegédtábla  order by változatnév, szolgálatiszám";
            else
                szöveg = "SELECT * FROM Kidobósegédtábla where változatnév='" + VáltozatCombo.Text.Trim() + "'  order by változatnév, szolgálatiszám";

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

            Kezelő_Kidobó_Segéd kéz = new Kezelő_Kidobó_Segéd();
            List<Adat_Kidobó_Segéd> Adatok = kéz.Lista_Adat(hely, jelszó, szöveg);
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

            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Főkönyv\Kidobó\kidobósegéd.mdb";
            string jelszó = "erzsébet";

            string szöveg = "SELECT * FROM Változattábla  order by id";

            VáltozatCombo.Items.Clear();
            VáltozatCombo.Items.Add("");
            VáltozatCombo.BeginUpdate();
            VáltozatCombo.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "Változatnév"));
            VáltozatCombo.EndUpdate();
            VáltozatCombo.Refresh();

            // típusok feltöltése
            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\segéd\kiegészítő.mdb";
            szöveg = "SELECT *  FROM fortetipus ORDER BY ftípus";
            jelszó = "Mocó";

            Forte_típus.Items.Clear();
            Forte_típus.BeginUpdate();
            Forte_típus.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "ftípus"));
            Forte_típus.EndUpdate();
            Forte_típus.Refresh();

        }


        private void Command8_Click(object sender, EventArgs e)
        {
            Ablakot_Nyit();
        }


        void Ablakot_Nyit()
        {
            try
            {
                if (Segéd_adat == null)
                    throw new HibásBevittAdat("Nincs kiválasztva elem.");


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


        private void Új_Ablak_Kidobó_Ismétlődő_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kidobó_Ismétlődő = null;
        }


        private void Tábla1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            /// táblázatból kiolvassuk az adatokat
            Segéd_adat = new Adat_Kidobó_Segéd(
                            Tábla1.Rows[e.RowIndex].Cells[1].Value.ToString().Trim(),
                            Tábla1.Rows[e.RowIndex].Cells[0].Value.ToString().Trim(),
                            DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[2].Value.ToString().Trim()),
                            DateTime.Parse(Tábla1.Rows[e.RowIndex].Cells[3].Value.ToString().Trim()),
                            Tábla1.Rows[e.RowIndex].Cells[4].Value.ToString().Trim(),
                            Tábla1.Rows[e.RowIndex].Cells[5].Value.ToString().Trim(),
                            Tábla1.Rows[e.RowIndex].Cells[7].Value.ToString().Trim(),
                            Tábla1.Rows[e.RowIndex].Cells[6].Value.ToString().Trim()
                            );
            Ablakot_Nyit();
        }
        #endregion

    }
}