using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Ablakok;
using Villamos.Villamos_Ablakok.Kerék_nyilvántartás;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;
using MyKerék = Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerék_nyilvántartás.KerékNyilvántartás_funkciók;


namespace Villamos
{

    public partial class Ablak_keréknyilvántartás
    {
        readonly Kezelő_Kerék_Mérés KézMérés = new Kezelő_Kerék_Mérés();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Kerék_Tábla KézKerék = new Kezelő_Kerék_Tábla();
        readonly Kezelő_Osztály_Adat KézCsat = new Kezelő_Osztály_Adat();
        readonly Kezelő_Kerék_Erő KézErőTám = new Kezelő_Kerék_Erő();
        readonly Kezelő_Kerék_Eszterga KézEszterga = new Kezelő_Kerék_Eszterga();
        readonly Kezelő_Kerék_Eszterga_Igény KézEsztIgény = new Kezelő_Kerék_Eszterga_Igény();
        readonly Kezelő_Nap_Hiba KézHiba = new Kezelő_Nap_Hiba();
        readonly Kezelő_Kiegészítő_Jelenlétiív KézKiegJelenlét = new Kezelő_Kiegészítő_Jelenlétiív();

        List<Adat_Kerék_Mérés> AdatokMérés = new List<Adat_Kerék_Mérés>();
        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        List<Adat_Kerék_Tábla> AdatokKerék = new List<Adat_Kerék_Tábla>();
        List<Adat_Osztály_Adat> AdatokCsat = new List<Adat_Osztály_Adat>();
        List<Adat_Kerék_Erő> AdatokErőTám = new List<Adat_Kerék_Erő>();
        List<Adat_Kerék_Eszterga> AdatokEszterga = new List<Adat_Kerék_Eszterga>();
        List<Adat_Kerék_Eszterga_Igény> AdatokIgény = new List<Adat_Kerék_Eszterga_Igény>();
        List<Adat_Nap_Hiba> AdatokHiba = new List<Adat_Nap_Hiba>();
        List<Adat_Kiegészítő_Jelenlétiív> AdatokKiegJelenlét = new List<Adat_Kiegészítő_Jelenlétiív>();

        public Ablak_keréknyilvántartás()
        {
            InitializeComponent();
        }

        private void Ablak_keréknyilvántartás_Load(object sender, EventArgs e)
        {
        }

        private void Ablak_keréknyilvántartás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kerék_segéd?.Close();
            Új_Ablak_Kerék_gyűjtő?.Close();
        }

        private void Ablak_keréknyilvántartás_Shown(object sender, EventArgs e)
        {
            try
            {
                string hely;
                hely = Application.StartupPath + @"\Főmérnökség\Adatok\Kerék.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Kerékbeolvasástábla(hely);

                hely = Application.StartupPath + @"\Főmérnökség\adatok\" + DateTime.Today.Year;
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

                hely += @"\telepikerék.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Méréstáblakerék(hely);

                Telephelyekfeltöltése();
                Pályaszámfeltöltés();
                Állapotfeltöltés();
                Dátumig.Value = DateTime.Today;
                Eszterga.Value = DateTime.Today;
                Dátumtól.Value = new DateTime(DateTime.Today.Year, 1, 1);
                Jogosultságkiosztás();
                Irányítófeltöltés();
                Jegyzettömb.Visible = false;
                Tábla1.Visible = true;
                LapFülek.DrawMode = TabDrawMode.OwnerDrawFixed;
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


        #region alap

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
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

        private void Button13_Click(object sender, EventArgs e)
        {

            string hely = Application.StartupPath + @"\Súgó\VillamosLapok\berendezés_kerék.html";
            Module_Excel.Megnyitás(hely);

        }

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                BtnSAP.Enabled = false;
                Panel5.Enabled = false;

                Rögzítrögzít.Enabled = false;
                GyűjtőRögzítés.Enabled = false;
                Command3.Enabled = false;
                Command6.Enabled = false;

                Command10.Enabled = false;
                Command8.Enabled = false;
                Command7.Enabled = false;



                // csak főmérnökségi belépéssel törölhető
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Panel5.Visible = true;
                }
                else
                {
                    Panel5.Visible = false;
                }
                melyikelem = 186;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    BtnSAP.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {

                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Panel5.Enabled = true;
                }

                melyikelem = 187;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))

                {
                    Rögzítrögzít.Enabled = true;
                    GyűjtőRögzítés.Enabled = true;
                    Command3.Enabled = true;
                    Command6.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))

                {
                    Command10.Enabled = true;
                    Command8.Enabled = true;
                    Command7.Enabled = true;
                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))

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

        private void LAPFülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Fülekkitöltése()
        {

            switch (LapFülek.SelectedIndex)
            {
                case 0:
                    {
                        break;
                    }

                case 1:
                    {
                        break;
                    }


                case 3:
                    {
                        Típus_Feltöltés();
                        break;
                    }

                case 4:
                    {
                        break;
                    }

                case 5:
                    {
                        break;
                    }

            }
        }


        private void Pályaszámfeltöltés()
        {
            try
            {
                List<Adat_Jármű> Adatok = new List<Adat_Jármű>();
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                    Adatok = KézJármű.Lista_Adatok("Főmérnökség");
                else
                    Adatok = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adatok = (from a in Adatok
                          where a.Törölt == false
                          orderby a.Azonosító
                          select a).ToList();


                SAPPályaszám.Items.Clear();
                PályaszámCombo2.Items.Clear();
                RögzítPályaszám.Items.Clear();
                foreach (Adat_Jármű Elem in Adatok)
                {
                    SAPPályaszám.Items.Add(Elem.Azonosító);
                    PályaszámCombo2.Items.Add(Elem.Azonosító);
                    RögzítPályaszám.Items.Add(Elem.Azonosító);
                }

                SAPPályaszám.Refresh();
                PályaszámCombo2.Refresh();
                RögzítPályaszám.Refresh();

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


        private void Állapotfeltöltés()
        {
            RögzítÁllapot.Items.Add("1   Frissen esztergált");
            RögzítÁllapot.Items.Add("2   Üzemszerűen kopott forgalomban");
            RögzítÁllapot.Items.Add("3   Forgalomképes esztergálandó");
            RögzítÁllapot.Items.Add("4   Forgalomképtelen azonnali esztergálást igényel");
        }


        private void Irányítófeltöltés()
        {
            try
            {
                Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adatok = (from a in Adatok
                          where a.Kilépésiidő.ToShortDateString() == new DateTime(1900, 1, 1).ToShortDateString()
                          && a.Főkönyvtitulus.Trim() != "" && a.Főkönyvtitulus.Trim() != "_"
                          orderby a.DolgozóNév
                          select a).ToList();

                Kiadta.Items.Clear();
                Kiadta.Items.Add("");
                foreach (Adat_Dolgozó_Alap Elem in Adatok)
                    Kiadta.Items.Add(Elem.DolgozóNév);
                Kiadta.Refresh();
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

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pályaszámfeltöltés();
        }

        private void Lapfülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Határozza meg, hogy melyik lap van jelenleg kiválasztva
            TabPage SelectedTab = LapFülek.TabPages[e.Index];

            // Szerezze be a lap fejlécének területét
            Rectangle HeaderRect = LapFülek.GetTabRect(e.Index);

            // Hozzon létreecsetet a szöveg megfestéséhez
            SolidBrush BlackTextBrush = new SolidBrush(Color.Black);

            // Állítsa be a szöveg igazítását
            StringFormat sf = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Festse meg a szöveget a megfelelő félkövér és szín beállítással
            if ((e.State & DrawItemState.Selected) != 0)
            {
                Font BoldFont = new Font(LapFülek.Font.Name, LapFülek.Font.Size, FontStyle.Bold);
                // háttér szín beállítása
                e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), e.Bounds);
                Rectangle paddedBounds = e.Bounds;
                paddedBounds.Inflate(0, 0);
                e.Graphics.DrawString(SelectedTab.Text, BoldFont, BlackTextBrush, paddedBounds, sf);
            }
            else
            {
                e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);
            }
            // Munka kész – dobja ki a keféket
            BlackTextBrush.Dispose();

        }
        #endregion


        #region SAP Adatok lapfül

        #region Listázás
        private void BtnListáz_Click(object sender, EventArgs e)
        {
            if (Csakkerék.Checked == true)
                Berendezés_adatok("Kerék");
            else
                Berendezés_adatok("Minden");

            Berendezés_ellemőrzés();
        }

        private void Berendezés_adatok(string Választ)
        {
            try
            {

                if (SAPPályaszám.Text.Trim() == "") return;
                Erőtámvan.Visible = MyKerék.Erőtámkiolvasás(SAPPályaszám.Text.Trim());

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Kerék.mdb";
                string jelszó = "szabólászló";
                string szöveg = "";

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 8;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Psz";
                Tábla.Columns[0].Width = 80;
                Tábla.Columns[1].HeaderText = "Berendezésszám";
                Tábla.Columns[1].Width = 150;
                Tábla.Columns[2].HeaderText = "Gyári szám";
                Tábla.Columns[2].Width = 150;
                Tábla.Columns[3].HeaderText = "Pozíció";
                Tábla.Columns[3].Width = 100;
                Tábla.Columns[4].HeaderText = "Mérés Dátuma";
                Tábla.Columns[4].Width = 170;
                Tábla.Columns[5].HeaderText = "Állapot";
                Tábla.Columns[5].Width = 180;
                Tábla.Columns[6].HeaderText = "Méret";
                Tábla.Columns[6].Width = 100;
                Tábla.Columns[7].HeaderText = "Megnevezés";
                Tábla.Columns[7].Width = 300;

                int i;
                switch (Választ)
                {
                    case "Kerék":
                        szöveg = "SELECT * FROM tábla where [azonosító]='" + SAPPályaszám.Text.Trim() + "'";
                        szöveg += " and objektumfajta='V.KERÉKPÁR' order by pozíció ";
                        break;
                    case "Forgóváz":
                        szöveg = "SELECT * FROM tábla where [azonosító]='" + SAPPályaszám.Text.Trim() + "'";
                        szöveg += " and (objektumfajta='V.KERÉKPÁR' or objektumfajta='FORGVKERET') order by pozíció ";
                        break;
                    case "Minden":
                        szöveg = "SELECT * FROM tábla where [azonosító]='" + SAPPályaszám.Text.Trim() + "' order by pozíció ";
                        break;
                    default:
                        break;
                }

                List<Adat_Kerék_Tábla> Adatok = KézKerék.Lista_Adatok(hely, jelszó, szöveg);
                MérésiListázás();


                foreach (Adat_Kerék_Tábla rekord in Adatok)
                {

                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Tábla.Rows[i].Cells[1].Value = rekord.Kerékberendezés.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Kerékgyártásiszám.Trim();
                    Tábla.Rows[i].Cells[3].Value = rekord.Pozíció.Trim();
                    Tábla.Rows[i].Cells[4].Value = "1900.01.01";
                    Tábla.Rows[i].Cells[5].Value = "_";
                    Tábla.Rows[i].Cells[6].Value = "_";
                    Tábla.Rows[i].Cells[7].Value = rekord.Kerékmegnevezés.Trim();
                    if (AdatokMérés != null)
                    {
                        Adat_Kerék_Mérés Mérés = (from a in AdatokMérés
                                                  where a.Kerékberendezés == rekord.Kerékberendezés
                                                  orderby a.Mikor ascending
                                                  select a).LastOrDefault();
                        if (Mérés != null)
                        {
                            Tábla.Rows[i].Cells[4].Value = Mérés.Mikor.ToString("yyyy.MM.dd");
                            Tábla.Rows[i].Cells[5].Value = MilyenÁllapot(Mérés.Állapot);
                            Tábla.Rows[i].Cells[6].Value = Mérés.Méret;
                        }
                    }
                }

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



        private void SAPPályaszám_TextUpdate(object sender, EventArgs e)
        {
            Erőtámvan.Visible = MyKerék.Erőtámkiolvasás(SAPPályaszám.Text.Trim());
        }


        private void Berendezés_ellemőrzés()
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Kerék.mdb";
                string jelszó = "szabólászló";
                string szöveg = "SELECT * FROM tábla ORDER BY kerékberendezés";


                List<Adat_Kerék_Tábla> Adatok = KézKerék.Lista_Adatok(hely, jelszó, szöveg);

                string előző = "";
                List<string> SzövegMásolGY = new List<string>();
                List<string> szövegTörölGY = new List<string>();
                foreach (Adat_Kerék_Tábla rekord in Adatok)
                {
                    if (előző == rekord.Kerékberendezés)
                    {
                        // ha egyforma akkor töröljük
                        string szövegmásol = "INSERT INTO tábla (kerékberendezés, kerékmegnevezés, kerékgyártásiszám, föléberendezés, azonosító, pozíció, objektumfajta, dátum) VALUES (";
                        szövegmásol += $"'{rekord.Kerékberendezés}', "; // kerékberendezés
                        szövegmásol += $"'{rekord.Kerékmegnevezés}', "; // kerékmegnevezés
                        szövegmásol += $"'{rekord.Kerékgyártásiszám}', "; // kerékgyártásiszám
                        szövegmásol += $"'{rekord.Föléberendezés}', "; // föléberendezés
                        szövegmásol += $"'{rekord.Azonosító}', "; // azonosító
                        szövegmásol += $"'{rekord.Pozíció}', "; // pozíció
                        szövegmásol += $"'{rekord.Objektumfajta}', "; // objektumfajta
                        szövegmásol += $"'{rekord.Dátum:yyyy.MM.dd}') "; // dátum
                        SzövegMásolGY.Add(szövegmásol);

                        string szövegtöröl = $"DELETE FROM tábla WHERE [kerékberendezés]='{rekord.Kerékberendezés}'";
                        szövegTörölGY.Add(szövegtöröl);
                    }
                    else
                    {
                        előző = rekord.Kerékberendezés;
                    }
                }
                MyA.ABtörlés(hely, jelszó, szövegTörölGY);
                MyA.ABMódosítás(hely, jelszó, SzövegMásolGY);

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

        private void Command8_Click(object sender, EventArgs e)
        {
            try
            {
                // ha üres a tábla akkor kilép
                if (SAPPályaszám.Text.Trim() == "") return;

                JelenlétiListázás();
                Adat_Kiegészítő_Jelenlétiív AdatKiegJelenlét = (from a in AdatokKiegJelenlét
                                                                where a.Id == 1
                                                                select a).FirstOrDefault();

                AdatokJárműLista();
                Adat_Jármű AdatJármű = (from a in AdatokJármű
                                        where a.Azonosító == SAPPályaszám.Text.Trim()
                                        select a).FirstOrDefault();
                Csakkerék.Checked = true;
                Berendezés_adatok("Forgóváz");

                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Kerék esztergálási adatlap készítés",
                    FileName = "Kerék_esztergálási_tábla_" + SAPPályaszám.Text.Trim() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                // megnyitjuk az excelt
                MyE.ExcelLétrehozás();

                Holtart.Be(3);
                string munkalap = "Munka1";
                int újlap = 0;

                MyE.Oszlopszélesség("Munka1", "b:i", 12);
                MyE.Oszlopszélesség("Munka1", "a:a", 10);
                MyE.Oszlopszélesség("Munka1", "e:e", 14);
                MyE.Oszlopszélesség("Munka1", "f:f", 13);
                MyE.Oszlopszélesség("Munka1", "g:g", 10);

                // betűméret
                MyE.Munkalap_betű("Arial", 12);

                int sor = 1;
                int eleje;
                for (int i = 1; i <= 2; i++)
                {
                    Holtart.Lép();
                    // Lap felsőrész
                    MyE.Egyesít("Munka1", "a" + sor.ToString() + ":H" + sor.ToString());
                    if (AdatKiegJelenlét != null) MyE.Kiir(AdatKiegJelenlét.Szervezet, "a" + sor.ToString());

                    sor += 2;
                    MyE.Egyesít("Munka1", "a" + sor.ToString() + ":h" + sor.ToString());
                    MyE.Kiir("Munkafelvételilap kerék esztergáláshoz", "a" + sor.ToString());
                    MyE.Betű("A" + sor.ToString() + ":h" + sor.ToString(), 14);

                    sor += 2;
                    MyE.Egyesít("Munka1", "A" + sor.ToString() + ":b" + sor.ToString());
                    MyE.Kiir("Jármű pályaszáma:", "a" + sor.ToString());
                    MyE.Kiir(SAPPályaszám.Text.Trim(), "C" + sor.ToString());
                    MyE.Egyesít("Munka1", "e" + sor.ToString() + ":f" + sor.ToString());
                    MyE.Kiir("Jármű típusa:", "e" + sor.ToString());

                    string Jármű_típus = "";

                    if (AdatJármű != null)
                    {
                        MyE.Kiir(Jármű_típus, "g" + sor.ToString());
                        Jármű_típus = AdatJármű.Típus;
                    }


                    sor += 2;
                    MyE.Egyesít("Munka1", "A" + sor.ToString() + ":C" + sor.ToString());
                    MyE.Kiir("Utolsó felújítás óta futott:", "A" + sor.ToString());
                    MyE.Kiir(Km_Adat(SAPPályaszám.Text.Trim(), Jármű_típus) + " km", "D" + sor.ToString());



                    sor += 2;
                    eleje = sor;
                    // Fejléc táblázat
                    MyE.Egyesít("Munka1", "a" + sor.ToString() + ":" + "a" + (sor + 1).ToString());
                    MyE.Kiir("Pozíció", "a" + sor.ToString());
                    MyE.Egyesít("Munka1", "b" + sor.ToString() + ":" + "b" + (sor + 1).ToString());
                    MyE.Kiir("Gyári szám", "b" + sor.ToString());
                    MyE.Egyesít("Munka1", "c" + sor.ToString() + ":" + "d" + (sor + 1).ToString());
                    MyE.Kiir("SAP megnevezés", "c" + sor.ToString());
                    MyE.Egyesít("Munka1", "e" + sor.ToString() + ":" + "g" + sor.ToString());
                    MyE.Kiir("Előző mérési eredmények", "e" + sor.ToString());
                    MyE.Kiir("Esztergált", "h" + sor.ToString());
                    sor += 1;
                    MyE.Kiir("Dátum", "e" + sor.ToString());
                    MyE.Kiir("Állapot", "f" + sor.ToString());
                    MyE.Kiir("Méret", "g" + sor.ToString());
                    MyE.Kiir("Méret", "h" + sor.ToString());
                    MyE.Rácsoz("a" + eleje.ToString() + ":g" + sor.ToString());
                    MyE.Vastagkeret("a" + eleje.ToString() + ":h" + sor.ToString());

                    // Átmásoljuk a táblázatos értékeket
                    for (int j = 0; j <= Tábla.Rows.Count - 1; j++)
                    {
                        sor += 1;
                        MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 40);
                        MyE.Kiir(Tábla.Rows[j].Cells[3].Value.ToString().Trim(), "a" + sor.ToString()); // pozíció
                        MyE.Kiir(Tábla.Rows[j].Cells[2].Value.ToString().Trim(), "b" + sor.ToString()); // kerékgyártásiszám
                        MyE.Kiir(Tábla.Rows[j].Cells[7].Value.ToString().Trim(), "c" + sor.ToString()); // kerékmegnevezés
                        if (Tábla.Rows[j].Cells[4].Value.ToString().Trim() != "")
                            MyE.Kiir(Tábla.Rows[j].Cells[4].Value.ToÉrt_DaTeTime().ToString("yyyy.MM.dd"), "e" + sor.ToString()); // mikor
                        MyE.Kiir(Tábla.Rows[j].Cells[5].Value.ToString().Trim(), "f" + sor.ToString()); // állapot
                        MyE.Sortörésseltöbbsorba_egyesített("f" + sor.ToString());
                        MyE.Betű("f" + sor.ToString(), 10);
                        MyE.Kiir(Tábla.Rows[j].Cells[6].Value.ToString().Trim(), "g" + sor.ToString()); // méret
                    }

                    MyE.Rácsoz("a" + (eleje + 2).ToString() + ":h" + sor.ToString());
                    MyE.Vastagkeret("a" + (eleje + 2).ToString() + ":h" + sor.ToString());
                    sor += 2;
                    MyE.Kiir("Kelt, Budapest " + DateTime.Today.ToString("yyyy.MM.dd"), "a" + sor.ToString());
                    MyE.Kiir("Elkészült:", "f" + sor.ToString());
                    MyE.Egyesít("Munka1", "g" + (sor + 1).ToString() + ":h" + (sor + 1).ToString());
                    MyE.Aláírásvonal("g" + (sor + 1).ToString() + ":h" + (sor + 1).ToString());
                    sor += 4;
                    MyE.Egyesít("Munka1", "b" + sor.ToString() + ":c" + sor.ToString());
                    MyE.Kiir("Esztergálást igénylő", "b" + sor.ToString());
                    MyE.Egyesít("Munka1", "g" + sor.ToString() + ":h" + sor.ToString());
                    MyE.Kiir("Esztergálást végző", "g" + sor.ToString());
                    MyE.Aláírásvonal("b" + sor.ToString() + ":c" + sor.ToString());
                    MyE.Aláírásvonal("g" + sor.ToString() + ":h" + sor.ToString());
                    sor += 1;
                    MyE.Egyesít("Munka1", "b" + sor.ToString() + ":c" + sor.ToString());
                    MyE.Kiir(Kiadta.Text.Trim(), "b" + sor.ToString());
                    if (i == 1)
                    {
                        sor += 4;
                        újlap = sor;
                    }

                }
                Holtart.Lép();
                MyE.NyomtatásiTerület_részletes("Munka1", "a1:h" + sor.ToString(), "", "", true);
                if (sor > 30)
                {
                    MyE.Nyom_Oszt(munkalap, "A" + újlap.ToString(), újlap, oldaltörés: 1);
                }
                // bezárjuk az Excel-t
                MyE.Aktív_Cella("Munka1", "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
                MyE.Megnyitás(fájlexc);
                Holtart.Ki();
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


        private void Command10_Click(object sender, EventArgs e)
        {
            try
            {
                // ha üres a tábla akkor kilép
                if (SAPPályaszám.Text.Trim() == "")
                    return;


                Csakkerék.Checked = true;
                Berendezés_adatok("Kerék");
                Nyomtatvány_készítés();
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

        private void Nyomtatvány_készítés()
        {
            try
            {
                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Kerékmérés adatlap készítés",
                    FileName = "Kerékmérés_tábla_" + SAPPályaszám.Text.Trim() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;


                // megnyitjuk az excelt
                MyE.ExcelLétrehozás();

                Holtart.Be(10);

                MyE.Oszlopszélesség("Munka1", "b:i", 12);
                MyE.Oszlopszélesség("Munka1", "a:a", 10);
                MyE.Oszlopszélesség("Munka1", "e:e", 14);
                MyE.Oszlopszélesség("Munka1", "f:f", 13);
                MyE.Oszlopszélesség("Munka1", "g:g", 10);
                // betűméret
                MyE.Munkalap_betű("Arial", 12);

                int sor = 1;
                int eleje;

                JelenlétiListázás();
                Adat_Kiegészítő_Jelenlétiív AdatKiegJelenlét = (from a in AdatokKiegJelenlét
                                                                where a.Id == 1
                                                                select a).FirstOrDefault();

                JárműListázás();
                Adat_Jármű AdatJármű = (from a in AdatokJármű
                                        where a.Azonosító == SAPPályaszám.Text.Trim()
                                        select a).FirstOrDefault();


                for (int i = 1; i <= 2; i++)
                {
                    Holtart.Lép();
                    // Lap felsőrész
                    MyE.Egyesít("Munka1", "a" + sor.ToString() + ":i" + sor.ToString());

                    if (AdatKiegJelenlét != null) MyE.Kiir(AdatKiegJelenlét.Szervezet, "a" + sor.ToString());

                    sor += 2;
                    MyE.Egyesít("Munka1", "a" + sor.ToString() + ":i" + sor.ToString());
                    MyE.Kiir("Kerék mérés", "A" + sor.ToString());
                    MyE.Egyesít("Munka1", "A" + sor.ToString() + ":b" + sor.ToString());
                    MyE.Betű("A" + sor.ToString() + ":b" + sor.ToString(), 14);
                    sor += 2;
                    MyE.Kiir("Jármű pályaszáma:", "a" + sor.ToString());
                    MyE.Kiir(SAPPályaszám.Text.Trim(), "C" + sor.ToString());
                    MyE.Egyesít("Munka1", "e" + sor.ToString() + ":f" + sor.ToString());
                    MyE.Kiir("Jármű típusa:", "e" + sor.ToString());

                    if (AdatJármű != null) MyE.Kiir(AdatJármű.Típus, "g" + sor.ToString());

                    sor += 2;
                    eleje = sor;
                    // Fejléc táblázat
                    MyE.Egyesít("Munka1", "a" + sor.ToString() + ":" + "a" + (sor + 1).ToString());
                    MyE.Kiir("Pozíció", "a" + sor.ToString());
                    MyE.Egyesít("Munka1", "b" + sor.ToString() + ":" + "b" + (sor + 1).ToString());
                    MyE.Kiir("Gyári szám", "b" + sor.ToString());
                    MyE.Egyesít("Munka1", "c" + sor.ToString() + ":" + "d" + (sor + 1).ToString());
                    MyE.Kiir("SAP megnevezés", "c" + sor.ToString());
                    MyE.Egyesít("Munka1", "e" + sor.ToString() + ":" + "g" + sor.ToString());
                    MyE.Kiir("Előző mérési eredmények", "e" + sor.ToString());
                    MyE.Egyesít("Munka1", "h" + sor.ToString() + ":" + "i" + sor.ToString());
                    MyE.Kiir("Mért eredmények", "h" + sor.ToString());
                    sor += 1;
                    MyE.Kiir("Dátum", "e" + sor.ToString());
                    MyE.Kiir("Állapot", "f" + sor.ToString());
                    MyE.Kiir("Méret", "g" + sor.ToString());
                    MyE.Kiir("Állapot", "h" + sor.ToString());
                    MyE.Kiir("Méret", "i" + sor.ToString());
                    MyE.Rácsoz("a" + eleje.ToString() + ":i" + sor.ToString());
                    MyE.Vastagkeret("a" + eleje.ToString() + ":i" + sor.ToString());

                    // Átmásoljuk a táblázatos értékeket

                    for (int j = 0; j < Tábla.Rows.Count; j++)
                    {
                        sor += 1;
                        MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 40);
                        MyE.Kiir(Tábla.Rows[j].Cells[3].Value.ToString().Trim(), "a" + sor.ToString()); // pozíció
                        MyE.Kiir(Tábla.Rows[j].Cells[2].Value.ToString().Trim(), "b" + sor.ToString()); // kerékgyártásiszám
                        MyE.Kiir(Tábla.Rows[j].Cells[7].Value.ToString().Trim(), "c" + sor.ToString()); // kerékmegnevezés
                        if (Tábla.Rows[j].Cells[4].Value.ToString().Trim() != "")
                        {
                            MyE.Kiir(Tábla.Rows[j].Cells[4].Value.ToÉrt_DaTeTime().ToString("yyyy.MM.dd"), "e" + sor.ToString()); // mikor
                        }
                        MyE.Kiir(Tábla.Rows[j].Cells[5].Value.ToString().Trim(), "f" + sor.ToString()); // állapot
                        MyE.Kiir(Tábla.Rows[j].Cells[6].Value.ToString().Trim(), "g" + sor.ToString()); // méret
                    }
                    MyE.Rácsoz("a" + (eleje + 2).ToString() + ":i" + sor.ToString());
                    MyE.Vastagkeret("a" + (eleje + 2).ToString() + ":i" + sor.ToString());
                    sor += 2;
                    MyE.Kiir("Erőtám:     van   /   nincs", "a" + sor.ToString());
                    sor += 2;
                    MyE.Kiir("Kelt, Budapest " + DateTime.Today.ToString("yyyy.MM.dd").ToString(), "a" + sor.ToString());
                    sor += 2;
                    MyE.Egyesít("Munka1", "b" + sor.ToString() + ":c" + sor.ToString());
                    MyE.Egyesít("Munka1", "h" + sor.ToString() + ":i" + sor.ToString());
                    sor += 1;
                    MyE.Aláírásvonal("b" + sor.ToString() + ":c" + sor.ToString());
                    MyE.Egyesít("Munka1", "b" + sor.ToString() + ":c" + sor.ToString());
                    MyE.Kiir("Mérést végezte", "b" + sor.ToString());
                    MyE.Egyesít("Munka1", "h" + sor.ToString() + ":i" + sor.ToString());
                    MyE.Kiir("Ellenőrizte", "H" + sor.ToString());
                    MyE.Aláírásvonal("b" + sor.ToString() + ":c" + sor.ToString());
                    MyE.Aláírásvonal("h" + sor.ToString() + ":i" + sor.ToString());
                    sor += 1;
                    MyE.Egyesít("Munka1", "h" + sor.ToString() + ":i" + sor.ToString());
                    MyE.Kiir(Kiadta.Text.Trim(), "H" + sor.ToString());
                    if (i == 1)
                        sor += 4;
                }
                Holtart.Lép();
                MyE.NyomtatásiTerület_részletes("Munka1", "a1:i" + sor.ToString(), "", "", true);


                // bezárjuk az Excel-t
                MyE.Aktív_Cella("Munka1", "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();


                MyE.Megnyitás(fájlexc);
                Holtart.Ki();
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


        private string Km_Adat(string Azonosító, string Típus)
        {
            long KMU = 0;

            string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\T5C5\Villamos4T5C5.mdb";
            string jelszó = "pocsaierzsi";
            string szöveg = "Select * FROM KMtábla";

            Kezelő_T5C5_Kmadatok KézT5C5Elő = new Kezelő_T5C5_Kmadatok("T5C5");
            List<Adat_T5C5_Kmadatok> AdatokT5CElő = KézT5C5Elő.Lista_Adat(hely, jelszó, szöveg);

            Adat_T5C5_Kmadatok AdatT5C5Elő = (from a in AdatokT5CElő
                                              where a.Törölt == false
                                              && a.Azonosító == Azonosító.Trim()
                                              orderby a.Vizsgdátumk descending
                                              select a).FirstOrDefault();

            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ICSKCSV\Villamos4ICS.mdb";

            List<Adat_T5C5_Kmadatok> AdatokICSelő = KézT5C5Elő.Lista_Adat(hely, jelszó, szöveg);
            Adat_T5C5_Kmadatok AdatICSelő = (from a in AdatokICSelő
                                             where a.Törölt == false
                                             && a.Azonosító == Azonosító.Trim()
                                             orderby a.Vizsgdátumk descending
                                             select a).FirstOrDefault();

            switch (Típus)
            {
                case "T5C5K2":
                    KMU = AdatT5C5Elő.KMUkm;
                    break;

                case "T5C5":
                    KMU = AdatT5C5Elő.KMUkm;
                    break;

                case "ICS":
                    KMU = AdatICSelő.KMUkm;
                    break;

                case "KCSV-7":
                    KMU = AdatICSelő.KMUkm;
                    break;
                default:
                    break;
            }
            return KMU.ToString();
        }

        private void Command7_Click(object sender, EventArgs e)
        {

            try
            {      // ha üres a tábla akkor kilép
                if (SAPPályaszám.Text.Trim() == "")
                    return;

                Csakkerék.Checked = false;
                Berendezés_adatok("Minden");

                Nyomtatvány_készítés1();
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

        private void Nyomtatvány_készítés1()
        {
            try
            {
                //string hely, jelszó, szöveg;
                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Berendezések adatlap készítés",
                    FileName = "Berendezés_tábla_" + SAPPályaszám.Text.Trim() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;


                // megnyitjuk az excelt
                MyE.ExcelLétrehozás();

                Holtart.Be(10);

                MyE.Oszlopszélesség("Munka1", "b:i", 12);
                MyE.Oszlopszélesség("Munka1", "h:i", 16);

                // betűméret
                MyE.Munkalap_betű("Arial", 12);

                int sor = 1;
                int eleje;

                //Új

                JelenlétiListázás();

                Adat_Kiegészítő_Jelenlétiív AdatKiegJelenlét = (from a in AdatokKiegJelenlét
                                                                where a.Id == 1
                                                                select a).FirstOrDefault();

                JárműListázás();

                Adat_Jármű AdatJármű = (from a in AdatokJármű
                                        where a.Azonosító == SAPPályaszám.Text.Trim()
                                        select a).FirstOrDefault();

                Holtart.Lép();
                // Lap felsőrész
                MyE.Egyesít("Munka1", $"a{sor}" + ":i" + sor.ToString());

                if (AdatKiegJelenlét != null) MyE.Kiir(AdatKiegJelenlét.Szervezet, $"a{sor}");

                sor += 2;
                MyE.Egyesít("Munka1", $"a{sor}" + ":i" + sor.ToString());
                MyE.Kiir("Forgalombiztonsági kiemelt szerkezeti elemek azonosítószám ellenőrző lapja", $"a{sor}");
                MyE.Betű($"a{sor}", 14);
                sor += 2;
                MyE.Egyesít("Munka1", $"a{sor}" + ":b" + sor.ToString());

                MyE.Kiir("Jármű pályaszáma:", $"a{sor}");
                MyE.Kiir(SAPPályaszám.Text.Trim(), $"c{sor}");
                MyE.Egyesít("Munka1", "e" + sor.ToString() + ":f" + sor.ToString());
                MyE.Kiir("Jármű típusa:", "e" + sor.ToString());

                if (AdatJármű != null) MyE.Kiir(AdatJármű.Típus, $"g{sor}");

                sor += 2;
                eleje = sor;
                // Fejléc táblázat
                MyE.Egyesít("Munka1", $"a{sor}" + ":" + $"a{sor}");
                MyE.Kiir("Pozíció", $"a{sor}");
                MyE.Egyesít("Munka1", $"b{sor}" + ":" + $"c{sor}");
                MyE.Kiir("Gyári szám", $"b{sor}");
                MyE.Egyesít("Munka1", $"d{sor}" + ":" + $"g{sor}");
                MyE.Kiir("SAP megnevezés", $"d{sor}");
                MyE.Egyesít("Munka1", $"h{sor}" + ":" + $"h{sor}");
                MyE.Kiir("Megfelelő", $"h{sor}");
                MyE.Egyesít("Munka1", $"i{sor}" + ":" + $"i{sor}");
                MyE.Kiir("Nem megfelelő", $"i{sor}");

                MyE.Rácsoz($"a{sor}" + ":i" + sor.ToString());
                MyE.Vastagkeret($"a{sor}" + ":i" + sor.ToString());

                // Átmásoljuk a táblázatos értékeket

                for (int j = 0; j < Tábla.Rows.Count; j++)
                {
                    sor += 1;
                    MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 40);
                    MyE.Kiir(Tábla.Rows[j].Cells[3].Value.ToString().Trim(), $"a{sor}"); // pozíció
                    MyE.Egyesít("Munka1", $"b{sor}" + ":" + $"c{sor}");
                    MyE.Kiir(Tábla.Rows[j].Cells[2].Value.ToString().Trim(), $"b{sor}"); // kerékgyártásiszám
                    MyE.Egyesít("Munka1", $"d{sor}" + ":" + $"g{sor}");
                    MyE.Kiir(Tábla.Rows[j].Cells[7].Value.ToString().Trim(), $"d{sor}"); // kerékmegnevezés

                }
                MyE.Rácsoz("a" + (eleje + 1).ToString() + ":i" + sor.ToString());
                MyE.Vastagkeret("a" + (eleje + 1).ToString() + ":i" + sor.ToString());
                sor += 2;
                MyE.Kiir("Kelt, Budapest " + DateTime.Today.ToString("yyyy.MM.dd"), $"a{sor}");
                sor += 2;
                MyE.Egyesít("Munka1", $"h{sor}" + ":i" + sor.ToString());
                MyE.Kiir("Ellenőrizte", $"h{sor}");
                MyE.Aláírásvonal($"h{sor}" + ":i" + sor.ToString());
                sor += 1;
                MyE.Egyesít("Munka1", $"h{sor}" + ":i" + sor.ToString());
                MyE.Kiir(Kiadta.Text.Trim(), $"h{sor}");

                Holtart.Lép();
                MyE.NyomtatásiTerület_részletes("Munka1", "a1:i" + sor.ToString(), "", "", true);


                // bezárjuk az Excel-t
                MyE.Aktív_Cella("Munka1", "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
                MyE.Megnyitás(fájlexc);
                Holtart.Ki();
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

        #region SAP gomb eseményei
        private void BtnSAP_Click(object sender, EventArgs e)
        {
            Beolvas_SAP();
        }

        private void Beolvas_SAP()
        {
            string fájlexc = "";
            try
            {
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                DateTime Eleje = DateTime.Now;
                //Adattáblába tesszük
                DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexc);

                if (!MyF.Betöltéshelyes("Kerék", Tábla)) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");

                //Készítünk egy listát az adatszerkezetnek megfelelően
                List<Adat_Kerék_Tábla> Excel_Listában = Excel_Kerék_Beolvas(Tábla);

                if (Excel_Listában != null) SAP(Excel_Listában);

                DateTime Vége = DateTime.Now;
                Holtart.Ki();
                //kitöröljük a betöltött fájlt
                Delete(fájlexc);

                MessageBox.Show($"Az adat konvertálás befejeződött!\nidő:{Vége - Eleje}", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private List<Adat_Kerék_Tábla> Excel_Kerék_Beolvas(DataTable EgyTábla)
        {
            List<Adat_Kerék_Tábla> Adatok = new List<Adat_Kerék_Tábla>();
            if (EgyTábla != null)
            {
                for (int i = 0; i < EgyTábla.Rows.Count; i++)
                {
                    Adat_Kerék_Tábla Adat = new Adat_Kerék_Tábla(
                                                EgyTábla.Rows[i]["Berendezés"].ToStrTrim(),
                                                EgyTábla.Rows[i]["Megnevezés"].ToStrTrim(),
                                                EgyTábla.Rows[i]["Gyártási szám"].ToStrTrim(),
                                                EgyTábla.Rows[i]["FölérendBerend."].ToStrTrim().Replace(",", ""),
                                                EgyTábla.Rows[i]["FölérendBerend."].ToStrTrim() == "" ? "_" : EgyTábla.Rows[i]["FölérendBerend."].ToStrTrim().Replace(",", "").Replace("V", "").Replace("F", ""),
                                                EgyTábla.Rows[i]["Tétel"].ToStrTrim(),
                                                EgyTábla.Rows[i]["Módosít. dátuma"].ToÉrt_DaTeTime(),
                                                EgyTábla.Rows[i]["Objektumfajta"].ToStrTrim()
                                                 );
                    Adatok.Add(Adat);
                }
            }
            return Adatok;
        }


        private void SAP(List<Adat_Kerék_Tábla> ELista)
        {
            try
            {

                Holtart.Be(ELista.Count + 1);

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Kerék.mdb";
                string jelszó = "szabólászló";
                string szöveg = "SELECT * FROM tábla ";


                List<Adat_Kerék_Tábla> Adatok = KézKerék.Lista_Adatok(hely, jelszó, szöveg);
                List<string> szövegGY = new List<string>();
                if (Adatok != null)
                {
                    foreach (Adat_Kerék_Tábla Elem in ELista)
                    {
                        // a pozícióban eddig volt berendezést felszabadítja
                        string RégiBerszám = (from a in Adatok
                                              where a.Pozíció == Elem.Pozíció && a.Azonosító == Elem.Azonosító && a.Kerékberendezés != Elem.Kerékberendezés
                                              select a.Kerékberendezés).FirstOrDefault();
                        if (RégiBerszám != null)
                        {
                            szöveg = " UPDATE tábla SET";
                            szöveg += " [pozíció]='_',  azonosító='_', föléberendezés='_' ";
                            szöveg += $" WHERE [kerékberendezés]='{RégiBerszám}'";
                            szövegGY.Add(szöveg);
                            //   MyA.ABMódosítás(hely, jelszó, szöveg);
                        }
                        //Ha benne van, de rossz helyen
                        Adat_Kerék_Tábla Rekord_berendezés = (from a in Adatok
                                                              where (a.Kerékberendezés == Elem.Kerékberendezés && a.Azonosító != Elem.Azonosító)
                                                                 || (a.Kerékberendezés == Elem.Kerékberendezés && a.Pozíció != Elem.Pozíció)
                                                              select a).FirstOrDefault();
                        if (Rekord_berendezés != null) szövegGY.Add(Kerék_módosítás(Méretrevág(Elem)));
                        //if (Rekord_berendezés != null) Kerék_módosítás(Méretrevág(Elem));

                        //Ha nincs benne
                        Rekord_berendezés = (from a in Adatok
                                             where (a.Kerékberendezés == Elem.Kerékberendezés)
                                             select a).FirstOrDefault();
                        //    if (Rekord_berendezés == null) Kerék_rögzítés(Méretrevág(Elem));
                        if (Rekord_berendezés == null) szövegGY.Add(Kerék_rögzítés(Méretrevág(Elem)));

                        Holtart.Lép();
                    }
                    MyA.ABMódosítás(hely, jelszó, szövegGY);
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


        private string Kerék_módosítás(Adat_Kerék_Tábla SAP_kerék)
        {
            string szöveg = "";
            try
            {
                szöveg = "UPDATE tábla SET";
                szöveg += $" kerékmegnevezés='{SAP_kerék.Kerékmegnevezés}', ";
                szöveg += $" kerékgyártásiszám='{SAP_kerék.Kerékgyártásiszám}', ";
                szöveg += $" föléberendezés='{SAP_kerék.Föléberendezés}', ";
                szöveg += $" azonosító='{SAP_kerék.Azonosító}', ";
                szöveg += $" pozíció='{SAP_kerék.Pozíció}', ";
                szöveg += $" objektumfajta='{SAP_kerék.Objektumfajta}', ";
                szöveg += $" dátum='{SAP_kerék.Dátum:yyyy.MM.dd}' ";
                szöveg += $" WHERE  [kerékberendezés]='{SAP_kerék.Kerékberendezés}'";

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
            return szöveg;
        }

        private string Kerék_rögzítés(Adat_Kerék_Tábla SAP_kerék)
        {
            string szöveg = "";
            try
            {
                szöveg = "INSERT INTO tábla (kerékberendezés, kerékmegnevezés, kerékgyártásiszám, föléberendezés, azonosító, pozíció, objektumfajta, dátum) VALUES (";
                szöveg += $"'{SAP_kerék.Kerékberendezés}', ";
                szöveg += $"'{SAP_kerék.Kerékmegnevezés}', ";
                szöveg += $"'{SAP_kerék.Kerékgyártásiszám}', ";
                szöveg += $"'{SAP_kerék.Föléberendezés}', ";
                szöveg += $"'{SAP_kerék.Azonosító}', ";
                szöveg += $"'{SAP_kerék.Pozíció}', ";
                szöveg += $"'{SAP_kerék.Objektumfajta}', ";
                szöveg += $"'{SAP_kerék.Dátum:yyyy.MM.dd}') ";
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
            return szöveg;
        }

        #endregion




        private Adat_Kerék_Tábla Méretrevág(Adat_Kerék_Tábla elem)
        {
            Adat_Kerék_Tábla válasz = new Adat_Kerék_Tábla(
                        MyF.Szöveg_Tisztítás(elem.Kerékberendezés, 0, 10),
                        MyF.Szöveg_Tisztítás(elem.Kerékmegnevezés, 0, 255),
                        MyF.Szöveg_Tisztítás(elem.Kerékgyártásiszám, 0, 30),
                        MyF.Szöveg_Tisztítás(elem.Föléberendezés, 0, 10),
                        MyF.Szöveg_Tisztítás(elem.Azonosító, 0, 10),
                        MyF.Szöveg_Tisztítás(elem.Pozíció, 0, 10),
                                             elem.Dátum,
                        MyF.Szöveg_Tisztítás(elem.Objektumfajta, 0, 20)
                    );
            return válasz;
        }
        #endregion


        #region Eredmények listázása lapfül
        private void Command5_Click(object sender, EventArgs e)
        {
            try
            {
                Feltöltések();

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Kerék.mdb";

                // oszlopok számának meghatározása
                int oszlop = 0;

                string szöveg, helyvill, jelszóvill;
                if (Típus_Szűrő.Text.Trim() == "")
                    szöveg = "SELECT * FROM állománytábla where törölt=0 order by  azonosító";
                else
                    szöveg = $"SELECT * FROM állománytábla where típus='{Típus_Szűrő.Text.Trim()}' AND törölt=0 order by  azonosító";


                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    helyvill = Application.StartupPath + @"\Főmérnökség\Adatok\villamos.mdb";
                    jelszóvill = "pozsgaii";
                }
                else
                {
                    helyvill = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\villamos\villamos.mdb";
                    jelszóvill = "pozsgaii";
                }

                Kezelő_Jármű KézJármű = new Kezelő_Jármű();
                List<Adat_Jármű> Jármű = KézJármű.Lista_Adatok(helyvill, jelszóvill, szöveg);
                Holtart.Be(Jármű.Count + 1);
                foreach (Adat_Jármű Elem in Jármű)
                {
                    Holtart.Lép();
                    List<Adat_Kerék_Tábla> poz = (from a in AdatokKerék
                                                  where a.Azonosító == Elem.Azonosító.Trim()
                                                  select a).ToList();
                    if (oszlop < poz.Count)
                        oszlop = poz.Count;
                }

                Tábla2.Rows.Clear();
                Tábla2.Columns.Clear();
                Tábla2.Refresh();
                Tábla2.Visible = false;
                Tábla2.ColumnCount = 7 + oszlop * 3;
                // fejléc elkészítése
                Tábla2.Columns[0].HeaderText = "Psz";
                Tábla2.Columns[0].Width = 60;
                Tábla2.Columns[1].HeaderText = "Erőtám";
                Tábla2.Columns[1].Width = 100;
                Tábla2.Columns[2].HeaderText = "Esztergálás";
                Tábla2.Columns[2].Width = 100;
                Tábla2.Columns[3].HeaderText = "Típus";
                Tábla2.Columns[3].Width = 100;
                Tábla2.Columns[4].HeaderText = "Csatolhatóság";
                Tábla2.Columns[4].Width = 100;
                Tábla2.Columns[5].HeaderText = "Kerékszám";
                Tábla2.Columns[5].Width = 100;
                Tábla2.Columns[6].HeaderText = "Meghibásodás";
                Tábla2.Columns[6].Width = 200;
                if (oszlop > 1)
                {
                    for (int k = 1; k <= oszlop; k++)
                    {
                        Tábla2.Columns[7 + 3 * (k - 1)].HeaderText = "Poz.:";
                        Tábla2.Columns[7 + 3 * (k - 1)].Width = 60;
                        Tábla2.Columns[8 + 3 * (k - 1)].HeaderText = "Áll.:";
                        Tábla2.Columns[8 + 3 * (k - 1)].Width = 60;
                        Tábla2.Columns[9 + 3 * (k - 1)].HeaderText = "Átm.:";
                        Tábla2.Columns[9 + 3 * (k - 1)].Width = 60;
                    }
                }

                // Pályaszámok kiírása
                Feltöltések();

                Tábla2.RowCount = Jármű.Count;
                int j = 0;
                if (Jármű != null)
                {
                    foreach (Adat_Jármű Elem in Jármű)
                    {
                        Holtart.Lép();
                        Tábla2.Rows[j].Cells[0].Value = Elem.Azonosító.Trim();
                        if (oszlop > 1)
                        {
                            Tábla2.Rows[j].Cells[2].Value = "";
                            Tábla2.Rows[j].Cells[7].Value = "";
                            Tábla2_típus(Elem.Azonosító, j);
                            Tábla2_kerékszám(Elem.Azonosító, j);
                            Tábla2_Csatolhatóság(Elem.Azonosító, j);
                            Tábla2_erőtám(Elem.Azonosító, j);
                            Tábla2_esztergálás(Elem.Azonosító, j);
                            Tábla2_Eszt_igény(Elem.Azonosító, j);
                            Tábla2_méretek(Elem.Azonosító, j);
                            Hiba_listázása(Elem.Azonosító, j);
                            j++;
                            Holtart.Lép();
                        }
                    }
                }
                Tábla2.Visible = true;
                Holtart.Ki();
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


        private void Feltöltések()
        {
            Holtart.Be();
            AdatokJárműLista();
            Holtart.Lép();
            AdatokKerékLista();
            Holtart.Lép();
            AdatokCsatLista();
            Holtart.Lép();
            AdatokErőTámLista();
            Holtart.Lép();
            AdatokEsztergaLista();
            Holtart.Lép();
            AdatokEsztergaIgényLista();
            Holtart.Lép();
            MérésiListázás();
            Holtart.Lép();
            HibaLista();
            Holtart.Ki();
        }

        private void Tábla2_típus(string azonosító, int sor)
        {
            try
            {
                if (AdatokJármű != null)
                {
                    Adat_Jármű rekordszer = (from a in AdatokJármű
                                             where a.Azonosító == azonosító
                                             select a).FirstOrDefault();

                    if (rekordszer != null)
                        Tábla2.Rows[sor].Cells[3].Value = rekordszer.Típus.Trim();
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
        private void Tábla2_kerékszám(string azonosító, int sor)
        {
            try
            {
                Tábla2.Rows[sor].Cells[5].Value = 0;
                if (AdatokKerék != null)
                {
                    List<Adat_Kerék_Tábla> Elem = (from a in AdatokKerék
                                                   where a.Azonosító == azonosító
                                                   select a).ToList();
                    if (Elem != null)
                        Tábla2.Rows[sor].Cells[5].Value = Elem.Count;
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
        private void Tábla2_Csatolhatóság(string azonosító, int sor)
        {
            try
            {
                if (AdatokCsat != null)
                {
                    Adat_Osztály_Adat rekordszer = (from a in AdatokCsat
                                                    where a.Azonosító == azonosító
                                                    select a).FirstOrDefault();
                    if (rekordszer != null) Tábla2.Rows[sor].Cells[4].Value = KézCsat.Érték(rekordszer, "Csatolhatóság");
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
        private void Tábla2_erőtám(string azonosító, int sor)
        {
            try
            {
                if (AdatokErőTám != null)
                {
                    Adat_Kerék_Erő rekordszer = (from a in AdatokErőTám
                                                 where a.Azonosító == azonosító
                                                 select a).LastOrDefault();
                    if (rekordszer != null)
                    {
                        if (rekordszer.Van.Trim() == "1")
                            Tábla2.Rows[sor].Cells[1].Value = "Igen";
                        else
                            Tábla2.Rows[sor].Cells[1].Value = "Nem";
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
        private void Tábla2_esztergálás(string azonosító, int sor)
        {
            try
            {
                if (AdatokEszterga != null)
                {
                    Adat_Kerék_Eszterga rekordszer = (from a in AdatokEszterga
                                                      where a.Azonosító == azonosító
                                                      select a).LastOrDefault();
                    if (rekordszer != null)
                        Tábla2.Rows[sor].Cells[2].Value = rekordszer.Eszterga.ToString("yyyy.MM.dd");
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
        private void Tábla2_Eszt_igény(string azonosító, int sor)
        {
            try
            {
                if (AdatokIgény != null)
                {
                    Adat_Kerék_Eszterga_Igény Keres = (from a in AdatokIgény
                                                       where a.Pályaszám.Contains(azonosító)
                                                       select a).LastOrDefault();
                    if (Keres != null)
                    {
                        if (Keres.Státus == 0)
                            Tábla2.Rows[sor].Cells[2].Style.BackColor = Color.Yellow;
                        if (Keres.Státus == 2)
                            Tábla2.Rows[sor].Cells[2].Style.BackColor = Color.Orange;
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
        private void Tábla2_méretek(string azonosító, int sor)
        {
            try
            {
                if (AdatokMérés != null)
                {
                    List<Adat_Kerék_Mérés> Mérések = (from a in AdatokMérés
                                                      where a.Azonosító == azonosító
                                                      orderby a.Mikor.ToString("yyyy.MM.dd") descending, a.Pozíció ascending
                                                      select a).ToList();

                    int oszlop = 7;
                    string előző = "";
                    int kerékszám = Tábla2.Rows[sor].Cells[5].Value.ToÉrt_Int();
                    foreach (Adat_Kerék_Mérés rekord in Mérések)
                    {
                        if (Tábla2.Columns.Count <= oszlop + 2) break;
                        if ((kerékszám * 3) + 5 < oszlop) break;
                        if (rekord.Pozíció.Trim() != előző)
                        {
                            Tábla2.Rows[sor].Cells[oszlop].Value = rekord.Pozíció;
                            előző = rekord.Pozíció;
                            Tábla2.Rows[sor].Cells[oszlop + 1].Value = MilyenÁllapot(rekord.Állapot);
                            switch (rekord.Állapot.Substring(0, 1))
                            {
                                case "2":
                                    {
                                        Tábla2.Rows[sor].Cells[oszlop + 1].Style.BackColor = Color.Yellow;
                                        break;
                                    }
                                case "3":
                                    {
                                        Tábla2.Rows[sor].Cells[oszlop + 1].Style.BackColor = Color.Orange;
                                        break;
                                    }
                                case "4":
                                    {
                                        Tábla2.Rows[sor].Cells[oszlop + 1].Style.BackColor = Color.Red;
                                        break;
                                    }
                            }
                            Tábla2.Rows[sor].Cells[oszlop + 2].Value = rekord.Méret;

                            if (rekord.Méret <= 630)
                            {
                                Tábla2.Rows[sor].Cells[oszlop + 2].Style.BackColor = Color.Red;
                            }
                            else if (rekord.Méret <= 634)
                            {
                                Tábla2.Rows[sor].Cells[oszlop + 2].Style.BackColor = Color.Yellow;
                            }
                            oszlop += 3;

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
        private void Hiba_listázása(string azonosító, int sor)
        {
            try
            {
                if (AdatokHiba == null) return;
                Adat_Nap_Hiba rekordszer = (from a in AdatokHiba
                                            where a.Azonosító == azonosító
                                            select a).FirstOrDefault();
                if (rekordszer != null)
                    Tábla2.Rows[sor].Cells[6].Value = rekordszer.Üzemképtelen + "-" + rekordszer.Beálló + "-" + rekordszer.Üzemképeshiba;
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

        private void ExcelKöltség_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla2.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Kerékméretek_export_" + Program.PostásTelephely.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    fájlexc = SaveFileDialog1.FileName;
                }
                else
                {
                    return;
                }

                Module_Excel.DataGridViewToExcel(fájlexc, Tábla2);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Module_Excel.Megnyitás(fájlexc + ".xlsx");
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

        private void Típus_Feltöltés()
        {
            try
            {
                List<Adat_Jármű> Adatok = new List<Adat_Jármű>();
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                    Adatok = KézJármű.Lista_Adatok("Főmérnökség");
                else
                    Adatok = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adatok = (from a in Adatok
                          where a.Törölt == false
                          orderby a.Típus
                          select a).ToList();
                List<string> Típusok = Adatok.Select(a => a.Típus).Distinct().ToList();



                Típus_Szűrő.Items.Clear();
                Típus_Szűrő.Items.Add("");
                foreach (string Elem in Típusok)
                    Típus_Szűrő.Items.Add(Elem);

                Típus_Szűrő.Refresh();
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


        #region Rögzítések listázása

        private void Command4_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dátumtól.Value == Dátumig.Value) throw new HibásBevittAdat("A kezdő és a vég dátumnak különbözőnek kell lennie.");
                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 9;


                // fejléc elkészítése

                Tábla1.Columns[0].HeaderText = "Psz";
                Tábla1.Columns[0].Width = 70;
                Tábla1.Columns[1].HeaderText = "Berendezésszám";
                Tábla1.Columns[1].Width = 140;
                Tábla1.Columns[2].HeaderText = "Gyári szám";
                Tábla1.Columns[2].Width = 90;
                Tábla1.Columns[3].HeaderText = "Pozíció";
                Tábla1.Columns[3].Width = 90;
                Tábla1.Columns[4].HeaderText = "Mérés Dátuma";
                Tábla1.Columns[4].Width = 170;
                Tábla1.Columns[5].HeaderText = "Állapot";
                Tábla1.Columns[5].Width = 180;
                Tábla1.Columns[6].HeaderText = "Méret";
                Tábla1.Columns[6].Width = 100;
                Tábla1.Columns[7].HeaderText = "Megnevezés";
                Tábla1.Columns[7].Width = 170;
                Tábla1.Columns[8].HeaderText = "Mérés Oka";
                Tábla1.Columns[8].Width = 170;

                List<Adat_Kerék_Mérés> Adatok = new List<Adat_Kerék_Mérés>();
                for (int Év = Dátumtól.Value.Year; Év <= Dátumig.Value.Year; Év++)
                {
                    List<Adat_Kerék_Mérés> Ideig = KézMérés.Lista_Adatok(Év);
                    Adatok.AddRange(Ideig);

                }

                Adatok = (from a in Adatok
                          where a.Mikor >= Dátumtól.Value
                          && a.Mikor <= Dátumig.Value
                          orderby a.Azonosító, a.Pozíció
                          select a).ToList();


                if (PályaszámCombo2.Text.Trim() != "")
                    Adatok = Adatok.Where(a => a.Azonosító == PályaszámCombo2.Text.Trim()).ToList();

                foreach (Adat_Kerék_Mérés rekord in Adatok)
                {
                    Tábla1.RowCount++;
                    int i = Tábla1.RowCount - 1;
                    Tábla1.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Tábla1.Rows[i].Cells[1].Value = rekord.Kerékberendezés.Trim();
                    Tábla1.Rows[i].Cells[2].Value = rekord.Kerékgyártásiszám.Trim();
                    Tábla1.Rows[i].Cells[3].Value = rekord.Pozíció.Trim();
                    Tábla1.Rows[i].Cells[4].Value = rekord.Mikor.ToString();
                    Tábla1.Rows[i].Cells[5].Value = MilyenÁllapot(rekord.Állapot);
                    Tábla1.Rows[i].Cells[6].Value = rekord.Méret.ToString();
                    Tábla1.Rows[i].Cells[7].Value = rekord.Módosító.Trim();
                    Tábla1.Rows[i].Cells[8].Value = rekord.Oka.Trim();
                }


                Tábla1.Visible = true;

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

        string MilyenÁllapot(string Állapot)
        {

            //Van rá enum
            string MilyenÁllapot = "";
            switch (Állapot.Trim().Substring(0, 1))
            {
                case "1":
                    MilyenÁllapot = "1 Frissen esztergált";
                    break;
                case "2":
                    MilyenÁllapot = "2 Üzemszerűen kopott forgalomban";
                    break;
                case "3":
                    MilyenÁllapot = "3 Forgalomképes esztergálandó";
                    break;
                case "4":
                    MilyenÁllapot = "4 Forgalomképtelen azonnali esztergálást igényel";
                    break;
            }
            return MilyenÁllapot;

        }

        private void Command9_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla1.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Kerékméretek_export_" + Program.PostásTelephely.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    fájlexc = SaveFileDialog1.FileName;
                }
                else
                {
                    return;
                }

                Module_Excel.DataGridViewToExcel(fájlexc, Tábla1);
                MessageBox.Show("Elkészült az Excel tábla: \n" + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Module_Excel.Megnyitás(fájlexc + ".xlsx");
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


        #region Főmérnökség adatok feltöltése

        private void Command4Főm_Click(object sender, EventArgs e)
        {
            Főmérnökség_Frissítés();

        }


        void Főmérnökség_Frissítés()
        {
            Tábla1írófőm();
            Jegyzettömbírófőm();
        }

        private void Command5Főm_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla1.Visible == false)
                {
                    Jegyzettömb.Visible = false;
                    Tábla1.Visible = true;
                }
                else
                {
                    Jegyzettömb.Visible = true;
                    Tábla1.Visible = false;

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

        private void Tábla1írófőm()
        {
            try
            {
                string hely, jelszó, szöveg;
                hely = Application.StartupPath + @"\főmérnökség\adatok\" + Dátumtól.Value.Year + @"\telepikerék.mdb";
                jelszó = "szabólászló";

                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 9;


                // fejléc elkészítése

                Tábla1.Columns[0].HeaderText = "Psz";
                Tábla1.Columns[0].Width = 70;
                Tábla1.Columns[1].HeaderText = "Berendezésszám";
                Tábla1.Columns[1].Width = 140;
                Tábla1.Columns[2].HeaderText = "Gyári szám";
                Tábla1.Columns[2].Width = 90;
                Tábla1.Columns[3].HeaderText = "Pozíció";
                Tábla1.Columns[3].Width = 90;
                Tábla1.Columns[4].HeaderText = "Mérés Dátuma";
                Tábla1.Columns[4].Width = 170;
                Tábla1.Columns[5].HeaderText = "Állapot";
                Tábla1.Columns[5].Width = 100;
                Tábla1.Columns[6].HeaderText = "Méret";
                Tábla1.Columns[6].Width = 100;
                Tábla1.Columns[7].HeaderText = "Megnevezés";
                Tábla1.Columns[7].Width = 170;
                Tábla1.Columns[8].HeaderText = "Mérés Oka";
                Tábla1.Columns[8].Width = 170;

                int i;

                szöveg = "SELECT * FROM keréktábla where ";
                if (PályaszámCombo2.Text.ToString().Trim() != "")
                {
                    szöveg += " azonosító='" + PályaszámCombo2.Text.Trim() + "' and ";
                }
                if (SAPba.Checked)
                {
                    szöveg += " SAP=1 and ";
                }
                else
                {
                    szöveg += " SAP<>1 and ";
                }
                if (Dátumtól.Value == Dátumig.Value)
                {
                    szöveg += " mikor>= #" + Dátumtól.Value.ToString("yyyy-MM-dd") + " 00:00:00#";
                    szöveg += " and mikor<= #" + Dátumtól.Value.ToString("yyyy-MM-dd") + " 23:59:59#";
                }
                else
                {
                    szöveg += " mikor>= #" + Dátumtól.Value.ToString("yyyy-MM-dd") + " 00:00:00#";
                    szöveg += " and mikor<= #" + Dátumig.Value.ToString("yyyy-MM-dd") + " 23:59:59#";
                }
                szöveg += " order by azonosító,pozíció ";


                List<Adat_Kerék_Mérés> Adatok = KézMérés.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Kerék_Mérés rekord in Adatok)
                {

                    Tábla1.RowCount++;
                    i = Tábla1.RowCount - 1;
                    Tábla1.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    Tábla1.Rows[i].Cells[1].Value = rekord.Kerékberendezés.Trim();
                    Tábla1.Rows[i].Cells[2].Value = rekord.Kerékgyártásiszám.Trim();
                    Tábla1.Rows[i].Cells[3].Value = rekord.Pozíció.Trim();
                    Tábla1.Rows[i].Cells[4].Value = rekord.Mikor.ToString();
                    Tábla1.Rows[i].Cells[5].Value = MilyenÁllapot(rekord.Állapot);
                    Tábla1.Rows[i].Cells[6].Value = rekord.Méret;
                    Tábla1.Rows[i].Cells[7].Value = rekord.Módosító.Trim();
                    Tábla1.Rows[i].Cells[8].Value = rekord.Oka.Trim();
                }

                Tábla1.Visible = true;
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

        private void Jegyzettömbírófőm()
        {
            try
            {
                string hely, jelszó, szöveg;
                hely = Application.StartupPath + @"\főmérnökség\adatok\" + Dátumtól.Value.Year + @"\telepikerék.mdb";
                jelszó = "szabólászló";

                Jegyzettömb.Text = "";

                szöveg = "SELECT * FROM keréktábla where ";
                if (PályaszámCombo2.Text.Trim() != "")
                    szöveg = szöveg + " azonosító='" + PályaszámCombo2.Text.Trim() + "' and ";

                if (SAPba.Checked)
                    szöveg += " SAP=1 and ";
                else
                    szöveg += " SAP<>1 and ";

                if (Dátumtól.Value == Dátumig.Value)
                {
                    szöveg += " mikor>= #" + Dátumtól.Value.ToString("yyyy-MM-dd") + " 00:00:00#";
                    szöveg += " and mikor<= #" + Dátumtól.Value.ToString("yyyy-MM-dd") + " 23:59:59#";
                }
                else
                {
                    szöveg += " mikor>= #" + Dátumtól.Value.ToString("yyyy-MM-dd") + " 00:00:00#";
                    szöveg += " and mikor<= #" + Dátumig.Value.ToString("yyyy-MM-dd") + " 23:59:59#";
                }
                szöveg += " order by azonosító,pozíció ";


                List<Adat_Kerék_Mérés> Adatok = KézMérés.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Kerék_Mérés rekord in Adatok)
                {

                    szöveg = rekord.Kerékberendezés.Trim() + "\t";
                    szöveg += "ATM_J" + "\t" + "\t" + "\t" + "\t" + "\t";
                    szöveg += "0" + "\t" + "\t" + "\t";
                    szöveg += rekord.Mikor.ToString("yyyy.MM.dd") + "\t";
                    szöveg += rekord.Mikor.ToString("hh:mm:ss") + "\t";
                    if (rekord.Módosító.Trim().Length < 12)
                        szöveg += rekord.Módosító.Trim() + "\t";
                    else
                        szöveg += rekord.Módosító.Trim().Substring(0, 12) + "\t";


                    szöveg += rekord.Méret.ToString() + "\t" + "\t";

                    if (rekord.Oka.Trim().Length < 39)
                        szöveg += rekord.Oka.Trim();
                    else
                        szöveg += rekord.Oka.Trim().Substring(0, 40);

                    szöveg += "\r" + "\n";

                    Jegyzettömb.Text += szöveg;
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

        private void Command7Főm_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\főmérnökség\adatok\" + Dátumtól.Value.Year + @"\telepikerék.mdb";
                string jelszó = "szabólászló";
                string kerékberendezés;

                Jegyzettömb.Visible = false;

                Tábla1.Visible = true;
                Holtart.Be(Tábla1.Rows.Count + 2);

                KerékMérésListázás();

                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < Tábla1.Rows.Count; i++)
                {
                    kerékberendezés = Tábla1.Rows[i].Cells[1].Value.ToString();

                    DateTime rögzítésdátuma = DateTime.Parse(Tábla1.Rows[i].Cells[4].Value.ToString());
                    Adat_Kerék_Mérés AdatKerék = (from a in AdatokMérés
                                                  where a.Kerékberendezés == kerékberendezés.Trim()
                                                  && a.Mikor.ToShortDateString() == rögzítésdátuma.ToShortDateString()
                                                  select a).FirstOrDefault();
                    if (AdatKerék != null)
                    {
                        string szöveg = "UPDATE keréktábla  SET SAP=1 WHERE ";
                        szöveg += " kerékberendezés='" + kerékberendezés.Trim() + "' and ";
                        szöveg += " mikor=#" + rögzítésdátuma.ToString("yyyy-MM-dd HH:mm:ss") + "#";
                        SzövegGy.Add(szöveg);
                    }
                    Holtart.Lép();
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

                Holtart.Ki();
                MessageBox.Show("Az adatok státus állítása megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Főmérnökség_Frissítés();
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

        private void Command3Főm_Click(object sender, EventArgs e)
        {
            try
            {
                if (Jegyzettömb.Text.Trim() == "")
                    return;
                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Kerék esztergálási adatok előkészítése SAP-ba való feltöltéshez",
                    FileName = "Kerékmérések_export_" + Dátumtól.Value.ToString("yyyyMMdd") + "_" + Dátumig.Value.ToString("yyyyMMdd"),
                    Filter = "Normal text file |*.txt"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    fájlexc = SaveFileDialog1.FileName;
                }
                else
                {
                    return;
                }
                TextWriter Writer = new StreamWriter(fájlexc);
                Writer.Write(Jegyzettömb.Text);
                Writer.Close();
                //Vágólapra másoljuk az elérési utat
                Clipboard.SetText(fájlexc);

                MessageBox.Show("Az adatok mentése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

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


        #region Mérési adatok lapfül

        private void RögzítPályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Új_Gyári.Visible = false;
            Pályaszám_választás();
        }

        private void Pályaszám_választás()
        {
            try
            {
                if (RögzítPályaszám.Text.Trim() == "") return;
                Rögzítürít();

                List<Adat_Kerék_Tábla> Adatok = KézKerék.Lista_Adatok();
                List<Adat_Kerék_Tábla> EgyKocsi = (from a in Adatok
                                                   where a.Azonosító == RögzítPályaszám.Text.Trim()
                                                   orderby a.Pozíció
                                                   select a).ToList();
                Rögzítpozíció.Items.Clear();
                foreach (Adat_Kerék_Tábla Elem in EgyKocsi)
                    Rögzítpozíció.Items.Add(Elem.Pozíció);

                Rögzítpozíció.Refresh();

                ChkErőtám.Checked = MyKerék.Erőtámkiolvasás(SAPPályaszám.Text.Trim());

                EsztergaDátum.Text = "";
                KMU_old.Text = "";
                Adat_Kerék_Eszterga rekord = MyKerék.Esztergakiolvasás(SAPPályaszám.Text.Trim());
                if (rekord != null)
                {
                    EsztergaDátum.Text = rekord.Eszterga.ToString("yyyy.MM.dd");
                    KMU_old.Text = rekord.KMU.ToString();
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

        private void Button1_Click(object sender, EventArgs e)
        {
            Pályaszám_választás();
            Új_Gyári.Visible = false;
        }

        private void Rögzítürít()
        {
            ChkErőtám.Checked = false;
            Rögzítpozíció.Text = "";
            Gyártási.Text = "";
            Berendezés.Text = "";
            Megnevezés.Text = "";
            RögzítÁllapot.Text = "";
            Állapot.Text = "";
            Méret.Text = "";
            Oka.Text = "";
            EsztergaDátum.Text = "";
        }

        private void Rögzítpozíció_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string hely, jelszó, szöveg;
                if (Rögzítpozíció.Text.Trim() == "") return;
                if (RögzítPályaszám.Text.Trim() == "") return;

                // betöltjük, ha van a pozíciókat
                hely = Application.StartupPath + @"\Főmérnökség\adatok\kerék.mdb";
                jelszó = "szabólászló";

                szöveg = "SELECT * FROM tábla where [azonosító]='" + RögzítPályaszám.Text.Trim() + "' ";
                szöveg += " and pozíció='" + Rögzítpozíció.Text.Trim() + "'";


                Adat_Kerék_Tábla Elem = KézKerék.Egy_Adat(hely, jelszó, szöveg);

                Gyártási.Text = "";
                Megnevezés.Text = "";
                Berendezés.Text = "";

                if (Elem != null)
                {
                    Gyártási.Text = Elem.Kerékgyártásiszám.Trim();
                    Megnevezés.Text = Elem.Kerékmegnevezés.Trim();
                    Berendezés.Text = Elem.Kerékberendezés.Trim();
                }

                // kiírjuk az utolsó értékeket
                Állapot.Text = "";
                Méret.Text = "";
                Oka.Text = "";
                int volt = 0;

                for (int l = 1; l <= 2; l++)
                {
                    if (l == 1)
                        hely = Application.StartupPath + @"\Főmérnökség\adatok\" + DateTime.Today.Year + @"\telepikerék.mdb";
                    else
                        hely = Application.StartupPath + @"\Főmérnökség\adatok\" + DateTime.Today.AddYears(-1).Year + @"\telepikerék.mdb";


                    szöveg = "SELECT * FROM keréktábla where azonosító='" + RögzítPályaszám.Text.Trim() + "' ";
                    szöveg += " and kerékberendezés='" + Berendezés.Text.Trim() + "' ";
                    szöveg += " order by mikor desc";


                    Adat_Kerék_Mérés Mérés = KézMérés.Egy_Adat(hely, jelszó, szöveg);

                    if (Mérés != null)
                    {
                        switch (Mérés.Állapot.Trim().Substring(0, 1))
                        {
                            case "1":
                                Állapot.Text = "1 Frissen esztergált";
                                break;
                            case "2":
                                Állapot.Text = "2 Üzemszerűen kopott forgalomban";
                                break;
                            case "3":
                                Állapot.Text = "3 Forgalomképes esztergálandó";
                                break;
                            case "4":
                                Állapot.Text = "4 Forgalomképtelen azonnali esztergálást igényel";
                                break;
                        }
                        Méret.Text = Mérés.Méret.ToString();
                        Oka.Text = Mérés.Oka.Trim();
                        volt = 1;
                    }

                    if (volt == 1)
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

        private void Rögzítrögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (RögzítPályaszám.Text.Trim() == "") throw new HibásBevittAdat("A jármű pályaszámát meg kell adni.");
                if (Rögzítpozíció.Text.Trim() == "") throw new HibásBevittAdat("A poizíciót meg kell adni.");
                if (RögzítÁllapot.Text.Trim() == "") throw new HibásBevittAdat("Az állapotot meg kell adni.");
                if (RögzítOka.Text.Trim() == "") throw new HibásBevittAdat("A rögzítés okát meg kell adni.");
                if (!int.TryParse(RögzítMéret.Text, out int Méret)) throw new HibásBevittAdat("A méret mezőnek egész számnak kell lennie.");
                if (Méret > 1000) throw new HibásBevittAdat("Biztos, hogy a kerék mérete 1000 mm-nél nagyobb?");
                if (Új_Gyári.Visible == true)
                {
                    if (Új_Gyári.Text.Trim() == "") throw new HibásBevittAdat("A gyári szám mezót ki kell tölteni.");
                    ÚjGyáriKitöltése();
                }

                Adat_Kerék_Mérés ADAT = new Adat_Kerék_Mérés(
                                        RögzítPályaszám.Text.Trim(),
                                        Rögzítpozíció.Text.Trim(),
                                        Berendezés.Text.Trim(),
                                        Gyártási.Text.Trim(),
                                        RögzítÁllapot.Text.Trim().Substring(0, 1),
                                        Méret,
                                        Program.PostásNév,
                                        DateTime.Now,
                                        RögzítOka.Text.Trim(),
                                        Új_Gyári.Visible ? 1 : 0);
                KézMérés.Rögzítés(DateTime.Today.Year, ADAT);

                MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Rögzítpozíció.Text = "";
                if (Új_Gyári.Visible == true)
                {
                    Új_Gyári.Text = "";
                    Gyártási.Text = "";
                    Berendezés.Text = "";
                    Megnevezés.Text = "";
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

        private void ÚjGyáriKitöltése()
        {
            Gyártási.Text = Új_Gyári.Text.Trim();
            Berendezés.Text = "Ideiglenes";
            Megnevezés.Text = "Ideiglenes fődarab";
        }

        private void Command3_Click(object sender, EventArgs e)
        {
            try
            {
                if (RögzítPályaszám.Text.Trim() == "") throw new HibásBevittAdat("A pályaszámot meg kell adni.");

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\" + DateTime.Today.Year + @"\telepikerék.mdb";
                string jelszó = "szabólászló";
                string szöveg = $"SELECT * FROM erőtábla";

                AdatokErőTám = KézErőTám.Lista_Adatok(hely, jelszó, szöveg);

                Adat_Kerék_Erő AdatKerékErő = (from a in AdatokErőTám
                                               where a.Azonosító == RögzítPályaszám.Text.Trim()
                                               select a).FirstOrDefault();


                if (AdatKerékErő != null)
                {
                    szöveg = "UPDATE erőtábla SET ";
                    if (ChkErőtám.Checked)
                        szöveg += " van='1', ";
                    else
                        szöveg += " van='0', ";
                    szöveg += $" mikor='{DateTime.Now}', ";
                    szöveg += $" módosító='{Program.PostásNév.Trim()}' ";
                    szöveg += $" WHERE azonosító = '{RögzítPályaszám.Text.Trim()}'";
                }
                else
                {

                    szöveg = "INSERT INTO erőtábla (van, mikor, módosító, azonosító)  VALUES (";

                    if (ChkErőtám.Checked)
                    {
                        szöveg += "'1', ";
                    }
                    else
                    {
                        szöveg += "'0', ";
                    }
                    szöveg += "'" + DateTime.Now.ToString() + "', ";
                    szöveg += "'" + Program.PostásNév.Trim() + "', ";
                    szöveg += "'" + RögzítPályaszám.Text.Trim() + "') ";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);

                MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void Command6_Click(object sender, EventArgs e)
        {
            try
            {

                if (RögzítPályaszám.Text.Trim() == "") throw new HibásBevittAdat("A jármű pályaszámát meg kell adni.");
                if (!long.TryParse(KMU_új.Text.Trim(), out long KMU_érték))
                {
                    KMU_új.Text = "0";
                    KMU_érték = 0;
                }

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\" + DateTime.Today.Year + @"\telepikerék.mdb";
                string jelszó = "szabólászló";

                string szöveg = "INSERT INTO esztergatábla (eszterga, mikor, módosító, azonosító, kmu)  VALUES (";
                szöveg += "'" + Eszterga.Value.ToString("yyyy.MM.dd") + "', ";
                szöveg += "'" + DateTime.Now.ToString() + "', ";
                szöveg += "'" + Program.PostásNév.Trim() + "', ";
                szöveg += "'" + RögzítPályaszám.Text.ToString().Trim() + "', ";
                szöveg += KMU_érték + " )";
                MyA.ABMódosítás(hely, jelszó, szöveg);

                MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Új_Pozíció_Click(object sender, EventArgs e)
        {
            try
            {
                if (RögzítPályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva pályaszám.");
                AdatokJárműLista();
                AdatokKerékLista();

                string típus = (from a in AdatokJármű
                                where a.Azonosító == RögzítPályaszám.Text.Trim()
                                select a.Valóstípus).FirstOrDefault();
                if (típus != null)
                {
                    List<string> pozíciók = (from a in AdatokJármű
                                             join b in AdatokKerék on a.Azonosító equals b.Azonosító
                                             where a.Valóstípus == típus && b.Objektumfajta == "V.KERÉKPÁR"
                                             orderby b.Pozíció
                                             select b.Pozíció).Distinct().ToList();

                    Rögzítpozíció.Items.Clear();
                    Rögzítpozíció.BeginUpdate();

                    foreach (string Elem in pozíciók)
                        Rögzítpozíció.Items.Add(Elem);


                    Rögzítpozíció.EndUpdate();
                    Rögzítpozíció.Refresh();
                    Új_gyári_be();
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

        private void Új_gyári_be()
        {
            Új_Gyári.Left = 313;
            Új_Gyári.Top = 62;
            Új_Gyári.Visible = true;

        }

        #endregion


        #region Kerékesztergára ütemez
        Ablak_Kerék_segéd Új_Ablak_Kerék_segéd;
        private void Kerék_Ütemez_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla2.SelectedRows.Count <= 0)
                    throw new HibásBevittAdat("Nincs kijelölve egy jármű sem kerékesztergálásra.");

                List<string> Küld = new List<string>();
                int tengely = 0;
                int prioritás = 0;
                int[] prioritásdb = { 0, 0, 0, 0 };
                int norma = 0;
                string típusküld = Tábla2.SelectedRows[0].Cells[3].Value.ToString().Trim();

                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";
                string jelszó = "RónaiSándor";
                string szöveg = $"SELECT * FROM tengely WHERE típus='{típusküld}' ORDER BY  állapot";
                Kezelő_Kerék_Eszterga_Tengely kéz = new Kezelő_Kerék_Eszterga_Tengely();
                List<Adat_Kerék_Eszterga_Tengely> Normaidő = kéz.Lista_Adatok(hely, jelszó, szöveg);

                foreach (DataGridViewRow Sor in Tábla2.SelectedRows)
                {
                    Küld.Add(Sor.Cells[0].Value.ToString().Trim());
                    for (int oszlop = 8; oszlop < Tábla2.ColumnCount - 1; oszlop += 3)
                    {
                        if (Sor.Cells[oszlop].Value != null)
                        {
                            string beolvasott = Sor.Cells[oszlop].Value.ToStrTrim();
                            if (beolvasott == "") beolvasott = "1";
                            switch (beolvasott.Substring(0, 1))
                            {
                                case "1":
                                    prioritásdb[0]++;
                                    if (prioritás < 1) prioritás = 1;
                                    break;
                                case "2":
                                    tengely++;
                                    prioritásdb[1]++;
                                    if (prioritás < 2) prioritás = 2;
                                    break;
                                case "3":
                                    prioritásdb[2]++;
                                    if (prioritás < 3) prioritás = 3;
                                    tengely++;
                                    break;
                                case "4":
                                    prioritásdb[3]++;
                                    tengely++;
                                    if (prioritás < 4) prioritás = 4;
                                    break;
                            }
                        }
                    }
                }

                foreach (Adat_Kerék_Eszterga_Tengely rekord in Normaidő)
                {
                    norma += prioritásdb[rekord.Állapot - 1] * rekord.Munkaidő;
                }

                Küld.Sort();
                string Szerelvény = "";
                foreach (string elem in Küld)
                    Szerelvény += elem + "-";

                Szerelvény = Szerelvény.Substring(0, Szerelvény.Length - 1);

                Új_Ablak_Kerék_segéd?.Close();

                Új_Ablak_Kerék_segéd = new Ablak_Kerék_segéd(Cmbtelephely.Text.Trim(), Szerelvény.Trim(), tengely, prioritás, típusküld, norma);
                Új_Ablak_Kerék_segéd.FormClosed += Ablak_Kerék_segéd_Closed;
                Új_Ablak_Kerék_segéd.Változás += Tábla2_Eszt_igényLista;
                Új_Ablak_Kerék_segéd.Show();
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

        private void Tábla2_Eszt_igényLista()
        {
            try
            {
                AdatokEsztergaIgényLista();
                if (AdatokIgény != null)
                {
                    for (int i = 0; i < Tábla2.Rows.Count; i++)
                    {
                        string PályaSzám = Tábla2.Rows[i].Cells[0].Value.ToString().Trim();
                        //Adat_Kerék_Eszterga_Igény Keres = Összes.Find(s => s.Pályaszám.Trim() == PályaSzám.Trim());
                        Adat_Kerék_Eszterga_Igény Keres = AdatokIgény.Find(x => x.Pályaszám.Contains(PályaSzám));
                        if (Keres != null)
                        {
                            if (Keres.Státus == 0)
                                Tábla2.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
                            if (Keres.Státus == 2)
                                Tábla2.Rows[i].Cells[2].Style.BackColor = Color.Orange;
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


        private void Ablak_Kerék_segéd_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kerék_segéd = null;
        }
        #endregion

        #region Táblázatos rögzítés
        Ablak_Kerék_Gyűjtő Új_Ablak_Kerék_gyűjtő;

        private void GyűjtőRögzítés_Click(object sender, EventArgs e)
        {
            Új_Ablak_Kerék_gyűjtő?.Close();

            Új_Ablak_Kerék_gyűjtő = new Ablak_Kerék_Gyűjtő(RögzítPályaszám.Text.Trim());
            Új_Ablak_Kerék_gyűjtő.FormClosed += Ablak_Kerék_Gyűjtő_Closed;
            Új_Ablak_Kerék_gyűjtő.Show();
        }

        private void Ablak_Kerék_Gyűjtő_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kerék_gyűjtő = null;
        }
        #endregion


        #region Listák
        private void MérésiListázás()
        {
            AdatokMérés.Clear();
            string hely = Application.StartupPath + @"\Főmérnökség\adatok\" + DateTime.Today.AddYears(-1).Year + @"\telepikerék.mdb";

            string jelszó = "szabólászló";
            string szöveg = "SELECT * FROM keréktábla ORDER BY kerékberendezés asc, mikor desc";

            AdatokMérés = KézMérés.Lista_Adatok(hely, jelszó, szöveg);

            hely = Application.StartupPath + @"\Főmérnökség\adatok\" + DateTime.Today.Year + @"\telepikerék.mdb";
            List<Adat_Kerék_Mérés> AdatokMérés1 = KézMérés.Lista_Adatok(hely, jelszó, szöveg);
            AdatokMérés.AddRange(AdatokMérés1);
        }
        private void AdatokJárműLista()
        {
            try
            {
                AdatokJármű.Clear();
                string hely = $@"{Application.StartupPath}\főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytábla ORDER BY azonosító";
                AdatokJármű = KézJármű.Lista_Adatok(hely, jelszó, szöveg);
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
        private void AdatokKerékLista()
        {
            try
            {
                AdatokKerék.Clear();
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Kerék.mdb";
                string jelszó = "szabólászló";
                string szöveg = "SELECT *  FROM tábla where objektumfajta='V.KERÉKPÁR'";
                AdatokKerék = KézKerék.Lista_Adatok(hely, jelszó, szöveg);
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
        private void AdatokCsatLista()
        {
            try
            {
                AdatokCsat.Clear();
                AdatokCsat = KézCsat.Lista_Adat();
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
        private void AdatokErőTámLista()
        {
            try
            {
                AdatokErőTám.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today.Year - 1}\telepikerék.mdb";
                string jelszó = "szabólászló";
                string szöveg = "SELECT * FROM erőtábla ORDER BY azonosító, mikor desc";

                AdatokErőTám = KézErőTám.Lista_Adatok(hely, jelszó, szöveg);

                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today.Year}\telepikerék.mdb";
                List<Adat_Kerék_Erő> AdatokErőTám1 = KézErőTám.Lista_Adatok(hely, jelszó, szöveg);
                AdatokErőTám.AddRange(AdatokErőTám1);

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
        private void AdatokEsztergaLista()
        {
            try
            {
                AdatokEszterga.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today.AddYears(-1).Year}\telepikerék.mdb";
                string jelszó = "szabólászló";
                string szöveg = "SELECT * FROM esztergatábla ORDER BY azonosító, mikor ";
                AdatokEszterga = KézEszterga.Lista_Adatok(hely, jelszó, szöveg);

                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today.Year}\telepikerék.mdb";
                List<Adat_Kerék_Eszterga> AdatokEszterga1 = KézEszterga.Lista_Adatok(hely, jelszó, szöveg);
                AdatokEszterga.AddRange(AdatokEszterga1);
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
        private void AdatokEsztergaIgényLista()
        {
            try
            {
                AdatokIgény.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{DateTime.Today.AddYears(-1).Year}_Igény.mdb";
                string jelszó = "RónaiSándor";
                string szöveg = $"SELECT * FROM Igény WHERE státus<8 ";
                AdatokIgény = KézEsztIgény.Lista_Adatok(hely, jelszó, szöveg);

                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{DateTime.Today.Year}_Igény.mdb";
                List<Adat_Kerék_Eszterga_Igény> AdatokIgény1 = KézEsztIgény.Lista_Adatok(hely, jelszó, szöveg);
                AdatokIgény.AddRange(AdatokIgény1);
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
        private void HibaLista()
        {
            try
            {
                if (Program.PostásTelephely != "Főmérnökség") Főkönyv_Funkciók.Napiállók(Cmbtelephely.Text.Trim());
                AdatokHiba.Clear();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\Új_napihiba.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM hiba  ORDER BY azonosító";
                AdatokHiba = KézHiba.Lista_adatok(hely, jelszó, szöveg);
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
        private void JelenlétiListázás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM jelenlétiív";
                AdatokKiegJelenlét.Clear();
                AdatokKiegJelenlét = KézKiegJelenlét.Lista_Adatok(hely, jelszó, szöveg);
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

        private void JárműListázás()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytábla";
                AdatokJármű.Clear();
                AdatokJármű = KézJármű.Lista_Adatok(hely, jelszó, szöveg);
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

        private void KerékMérésListázás()
        {
            try
            {
                string hely;
                if (Dátumtól.Value != null)
                    hely = Application.StartupPath + @"\főmérnökség\adatok\" + Dátumtól.Value.Year + @"\telepikerék.mdb";
                else
                    hely = Application.StartupPath + @"\főmérnökség\adatok\" + DateTime.Today.Year + @"\telepikerék.mdb";

                string jelszó = "szabólászló";
                string szöveg = "SELECT * FROM keréktábla ";

                AdatokMérés.Clear();
                AdatokMérés = KézMérés.Lista_Adatok(hely, jelszó, szöveg);
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
    }
}