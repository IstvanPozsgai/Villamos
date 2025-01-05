using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Ablakok.Beosztás;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_Túlóra_Figyelés
    {
        double Határ = 0;

        public Ablak_Túlóra_Figyelés()
        {
            InitializeComponent();
        }



        #region Alap
        private void Ablak_Túlóra_Figyelés_Load(object sender, EventArgs e)
        {
            Dátum.Value = DateTime.Today;
            Telephelyekfeltöltése();

            // elvont feltöltés
            Félév_feltöltés();
            Év_feltöltés();
            Subtelephelyiváltozat();
            Turnuskiirás();
            // Munkaidő keret feltöltés
            Kötelezőidők();
            Jogosultságkiosztás();
            Fülekkitöltése();
            Csoportfeltöltés();
            Névfeltöltés();

            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
        }


        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }


        private void Fülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Határozza meg, hogy melyik lap van jelenleg kiválasztva
            TabPage SelectedTab = Fülek.TabPages[e.Index];

            // Szerezze be a lap fejlécének területét
            Rectangle HeaderRect = Fülek.GetTabRect(e.Index);

            // Hozzon létreecsetet a szöveg megfestéséhez
            SolidBrush BlackTextBrush = new SolidBrush(Color.Black);

            // Állítsa be a szöveg igazítását
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Festse meg a szöveget a megfelelő félkövér és szín beállítással
            if ((e.State & DrawItemState.Selected) != 0)
            {
                Font BoldFont = new Font(Fülek.Font.Name, Fülek.Font.Size, FontStyle.Bold);
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


        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                Cmbtelephely.Items.AddRange(Listák.TelephelyLista_Személy(true));
                if (Program.PostásTelephely == "Főmérnökség")
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


        private void Fülekkitöltése()
        {
            switch (Fülek.SelectedIndex)
            {
                case 0:
                    {
                        break;
                    }

                case 1:
                    {
                        break;
                    }

                case 2:
                    {
                        break;
                    }
            }
        }


        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\munkaidőkeret.html";
                Module_Excel.Megnyitás(hely);
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


        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            Év_feltöltés();
            Kötelezőidők();
            Subtelephelyiváltozat();
        }


        private void Jogosultságkiosztás()
        {
            // ide kell az összes gombot tenni amit szabályozni akarunk false
            Rögzítés.Enabled = false;
            int melyikelem = 78;

            // módosítás 1  visszamenőleges beosztás rögzítés
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Rögzítés.Enabled = true;
            }

            if (MyF.Vanjoga(melyikelem, 2))
            {
            }

            if (MyF.Vanjoga(melyikelem, 3))
            {

            }

        }

        #endregion


        #region Túlóra
        private void Excel_Keret_Click(object sender, EventArgs e)
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
                    FileName = "Túlórakeret_Ellenőrzés_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, Tábla2, false);
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


        private void Command20_Click(object sender, EventArgs e)
        {
            Tábla2_egyik();
            Tábla2_másik();
        }


        private void Tábla2_egyik()
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\kiegészítő1.mdb";
                string jelszó = "Mocó";
                string szöveg = $"SELECT * FROM túlórakeret";
                Kezelő_Kiegészítő_Túlórakeret KézTúl = new Kezelő_Kiegészítő_Túlórakeret();
                List<Adat_Kiegészítő_Túlórakeret> AdatokTúl = KézTúl.Lista_Adatok(hely, jelszó, szöveg);
                if (AdatokTúl != null)
                {
                    string ideig = (from a in AdatokTúl
                                    where a.Telephely.Trim() == Program.PostásTelephely.Trim() && a.Parancs == 5
                                    select a.Telephely).FirstOrDefault();
                    if (ideig == null)
                    {
                        Határ = (from a in AdatokTúl
                                 where a.Telephely.Trim() == "_" && a.Parancs == 5
                                 select a.Határ).FirstOrDefault();
                    }
                    else
                    {
                        Határ = (from a in AdatokTúl
                                 where a.Telephely.Trim() == Program.PostásTelephely.Trim() && a.Parancs == 5
                                 select a.Határ).FirstOrDefault();
                    }
                    Határ = Határ / 12 * DateTime.Today.Month;
                }

                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
                jelszó = "forgalmiutasítás";
                szöveg = "SELECT * FROM Dolgozóadatok where kilépésiidő=#1/1/1900#  ORDER BY DolgozóNév ";

                Tábla2.Rows.Clear();
                Tábla2.Columns.Clear();
                Tábla2.Refresh();
                // .Visible = False
                Tábla2.ColumnCount = 5;
                // fejléc elkészítése
                Tábla2.Columns[0].HeaderText = "Név";
                Tábla2.Columns[0].Width = 300;
                Tábla2.Columns[1].HeaderText = "HR azonosító";
                Tábla2.Columns[1].Width = 150;
                Tábla2.Columns[2].HeaderText = "Csoportkód";
                Tábla2.Columns[2].Width = 100;
                Tábla2.Columns[3].HeaderText = "Rögzített túlórák [perc]";
                Tábla2.Columns[3].Width = 200;
                Tábla2.Columns[4].HeaderText = "Rögzített túlórák [óra]";
                Tábla2.Columns[4].Width = 200;

                Kezelő_Dolgozó_Alap Kéz = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                {
                    Tábla2.RowCount++;
                    int i = Tábla2.RowCount - 1;

                    Tábla2.Rows[i].Cells[0].Value = rekord.DolgozóNév.Trim();
                    Tábla2.Rows[i].Cells[1].Value = rekord.Dolgozószám.Trim();
                    Tábla2.Rows[i].Cells[2].Value = rekord.Csoportkód.Trim();
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


        private void Tábla2_másik()
        {

            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\szatubecs\{Dátum.Value.Year}szatubecs.mdb";
                string jelszó = "kertitörpe";
                string szöveg = "SELECT * FROM túlóra WHERE státus<>3";

                Kezelő_Szatube_Túlóra Kéz = new Kezelő_Szatube_Túlóra();
                List<Adat_Szatube_Túlóra> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                Tábla2.Refresh();

                for (int i = 0; i < Tábla2.Rows.Count; i++)
                {
                    Tábla2.Rows[i].Cells[3].Value = 0;
                    Tábla2.Rows[i].Cells[4].Value = 0;
                    string törzsszám = Tábla2.Rows[i].Cells[1].Value.ToString().Trim();
                    int eredmény = (from a in Adatok
                                    where a.Törzsszám.Trim() == törzsszám
                                    select a).Sum(a => a.Kivettnap);

                    Tábla2.Rows[i].Cells[3].Value = eredmény;
                    Tábla2.Rows[i].Cells[4].Value = Math.Round((double)(eredmény / 60), 2);

                    if (Határ <= Math.Round((double)(eredmény / 60), 2))
                    {
                        Tábla2.Rows[i].Cells[3].Style.BackColor = Color.Red;
                        Tábla2.Rows[i].Cells[4].Style.BackColor = Color.Red;
                        Tábla2.Rows[i].Cells[3].Style.ForeColor = Color.White;
                        Tábla2.Rows[i].Cells[4].Style.ForeColor = Color.White;
                    }
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
        #endregion


        #region Munkaidő keret
        private void Kötelezőidők()
        {
            string hely = Application.StartupPath + @"\Főmérnökség\adatok\kiegészítő2.mdb";
            string jelszó = "Mocó";
            string szöveg = "SELECT * FROM munkaidő where munkarendelnevezés='8'";
            Kezelő_Kiegészítő_Munkaidő KézMunka = new Kezelő_Kiegészítő_Munkaidő();
            List<Adat_Kiegészítő_Munkaidő> AdatokMunka = KézMunka.Lista_Adatok(hely, jelszó, szöveg);

            Adat_Kiegészítő_Munkaidő Elem = (from a in AdatokMunka
                                             where a.Munkarendelnevezés =="8" 
                                             select a                                             ).FirstOrDefault ();
            double óra;
            if (Elem != null) óra = Elem.Munkaidő; else óra = 0;

            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\munkaidőnaptár.mdb";
            jelszó = "katalin";
            szöveg = $" SELECT * from Naptár";

            Kezelő_Váltós_Naptár Kéz = new Kezelő_Váltós_Naptár();
            List<Adat_Váltós_Naptár> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);
            int első = (from a in Adatok
                        where a.Dátum < new DateTime(Dátum.Value.Year, 7, 1) && a.Nap == "1"
                        select a).Count();

            int második = (from a in Adatok
                           where a.Dátum >= new DateTime(Dátum.Value.Year, 7, 1) && a.Nap == "1"
                           select a).Count();
            Munka1fél.Text = "0";
            Munka2fél.Text = "0";
            Munkaév.Text = "0";
            Munka1fél.Text = (első * óra).ToString();
            Munka2fél.Text = (második * óra).ToString();
            Munkaév.Text = (első * óra + második * óra).ToString();
        }


        private void Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla3.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Munkaidőkeret_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, Tábla3, false, true);
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


        private void Ellenőrzés_Click(object sender, EventArgs e)
        {
            Tábla3_első_új();
            Tábla3_második();
            Tábla3_harmadik();
        }


        private void Tábla3_első_új()
        {
            try
            {
                if (Dolgozónév.CheckedItems.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy dolgozó sem.");

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
                string jelszó = "forgalmiutasítás";

                string szöveg = "SELECT * FROM Dolgozóadatok ";
                if (!Kilépettjel.Checked)
                    szöveg += " WHERE kilépésiidő=#01-01-1900# ";
                szöveg += " ORDER BY DolgozóNév asc";

                Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> AdatokDolg = KézDolg.Lista_Adatok(hely, jelszó, szöveg);

                Holtart.Be();

                Tábla3.Rows.Clear();
                Tábla3.Columns.Clear();
                Tábla3.Refresh();
                Tábla3.Visible = false;
                Tábla3.ColumnCount = 19;

                // fejléc elkészítése
                Tábla3.Columns[0].HeaderText = "Név";
                Tábla3.Columns[0].Width = 200;
                Tábla3.Columns[1].HeaderText = "HR azon.";
                Tábla3.Columns[1].Width = 70;
                Tábla3.Columns[2].HeaderText = "Csop.kód";
                Tábla3.Columns[2].Width = 80;
                Tábla3.Columns[3].HeaderText = "";
                Tábla3.Columns[3].Width = 120;
                for (int i = 4; i <= 15; i++)
                {
                    Tábla3.Columns[i].HeaderText = (i - 3).ToString();
                    Tábla3.Columns[i].Width = 60;
                }
                Tábla3.Columns[16].HeaderText = "1 félév";
                Tábla3.Columns[16].Width = 60;
                Tábla3.Columns[17].HeaderText = "2 félév";
                Tábla3.Columns[17].Width = 60;
                Tábla3.Columns[18].HeaderText = "Év összesen";
                Tábla3.Columns[18].Width = 120;

                for (int j = 0; j < Dolgozónév.CheckedItems.Count; j++)
                {

                    Tábla3.RowCount++;
                    int i = Tábla3.RowCount - 1;
                    string[] darabol = Dolgozónév.CheckedItems[j].ToString().Split('=');

                    Tábla3.Rows[i].Cells[0].Value = darabol[0].Trim();
                    Tábla3.Rows[i].Cells[1].Value = darabol[1].Trim();

                    string csoportkód = (from a in AdatokDolg
                                         where a.Dolgozószám.Trim() == darabol[1].Trim()
                                         select a.Csoportkód).FirstOrDefault() ?? "_";
                    if (csoportkód != "_")
                        Tábla3.Rows[i].Cells[2].Value = csoportkód;


                    int részmunkaidőperc = (int)(from a in AdatokDolg
                                                 where a.Dolgozószám.Trim() == darabol[1].Trim()
                                                 select a.Részmunkaidőperc).FirstOrDefault();

                    if (részmunkaidőperc != 0)
                        Tábla3.Rows[i].Cells[2].Value = "R" + részmunkaidőperc.ToString();


                    Tábla3.Rows[i].Cells[3].Value = "Havi előírás";
                    Tábla3.RowCount++;
                    i = Tábla3.RowCount - 1;
                    Tábla3.Rows[i].Cells[3].Value = "Túlmunka";
                    Tábla3.RowCount++;
                    i = Tábla3.RowCount - 1;
                    Tábla3.Rows[i].Cells[3].Value = "Tény Munkaidő";

                    Holtart.Lép();
                }
                //Minden második személy színezve
                bool páros = false;
                for (int i = 0; i < Tábla3.Rows.Count; i += 3)
                {
                    if (!páros)
                    {
                        Tábla3.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
                        Tábla3.Rows[i + 1].DefaultCellStyle.BackColor = Color.Aqua;
                        Tábla3.Rows[i + 2].DefaultCellStyle.BackColor = Color.Aqua;
                        páros = true;
                    }
                    else
                        páros = false;
                    Holtart.Lép();
                }

                // kinullázzuk a táblázatot

                for (int j = 4; j <= 18; j++)
                {
                    for (int i = 0; i < Tábla3.Rows.Count; i++)
                    {
                        Tábla3.Rows[i].Cells[j].Value = 0;
                    }
                    Holtart.Lép();
                }
                Tábla3.Refresh();
                Tábla3.Visible = true;

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


        private void Tábla3_második()
        {
            try
            {
                // kiirjuk az előírásokat a nyolc órásoknak
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\munkaidőnaptár.mdb";
                string jelszó = "katalin";
                string szöveg = " SELECT * FROM összesítő ORDER BY dátum";
                Kezelő_Váltós_Összesítő Kéz = new Kezelő_Váltós_Összesítő();
                List<Adat_Váltós_Összesítő> Adatok = new List<Adat_Váltós_Összesítő>();
                List<Adat_Váltós_Összesítő> Ideig = Kéz.Lista_Adatok(hely, jelszó, szöveg, "N");
                Adatok.AddRange(Ideig);

                szöveg = " SELECT * FROM összesítő1 ORDER BY dátum";
                Ideig = Kéz.Lista_Adatok(hely, jelszó, szöveg, "6_1");
                Adatok.AddRange(Ideig);

                szöveg = " SELECT * FROM összesítő2 ORDER BY dátum";
                Ideig = Kéz.Lista_Adatok(hely, jelszó, szöveg, "6_2");
                Adatok.AddRange(Ideig);

                szöveg = " SELECT * FROM összesítő3 ORDER BY dátum";
                Ideig = Kéz.Lista_Adatok(hely, jelszó, szöveg, "6_3");
                Adatok.AddRange(Ideig);

                szöveg = " SELECT * FROM összesítő4 ORDER BY dátum";
                Ideig = Kéz.Lista_Adatok(hely, jelszó, szöveg, "6_4");
                Adatok.AddRange(Ideig);

                szöveg = " SELECT * FROM összesítő5 ORDER BY dátum";
                Ideig = Kéz.Lista_Adatok(hely, jelszó, szöveg, "É_1");
                Adatok.AddRange(Ideig);

                szöveg = " SELECT * FROM összesítő6 ORDER BY dátum";
                Ideig = Kéz.Lista_Adatok(hely, jelszó, szöveg, "É_2");
                Adatok.AddRange(Ideig);

                if (Adatok != null)
                {
                    for (int i = 0; i < Tábla3.Rows.Count; i += 3)
                    {
                        Holtart.Lép();
                        double első = 0;
                        double második = 0;
                        string beolvasott = Tábla3.Rows[i].Cells[2].Value.ToStrTrim();
                        if (beolvasott == "") beolvasott = "N";
                        if (beolvasott.Contains('.')) beolvasott = beolvasott.Replace('.', '_');

                        if (beolvasott.Substring(0, 1) != "R")
                        {
                            for (int j = 1; j <= 12; j++)
                            {
                                double kötelező = (from a in Adatok
                                                   where a.Dátum.Month == j && a.Csoport.Trim() == beolvasott.Trim()
                                                   select a.Perc).FirstOrDefault();
                                Tábla3.Rows[i].Cells[j + 3].Value = kötelező.ToString();
                                if (j < 7)
                                    első += kötelező;
                                else
                                    második += kötelező;
                            }
                        }
                        else
                        // részmunkaidős
                        {
                            for (int j = 1; j <= 12; j++)
                            {
                                double kötelező = (from a in Adatok
                                                   where a.Dátum.Month == j && a.Csoport.Trim() == "N"
                                                   select a.Perc).FirstOrDefault();
                                if (!double.TryParse(beolvasott.Substring(1, beolvasott.Length - 1), out double Keret)) Keret = 1;
                                double részidő = kötelező / 480 * Keret;
                                Tábla3.Rows[i].Cells[j + 3].Value = részidő;
                                if (j < 7)
                                    első += részidő;
                                else
                                    második += részidő;
                            }
                        }
                        Tábla3.Rows[i].Cells[16].Value = első;
                        Tábla3.Rows[i].Cells[17].Value = második;
                        Tábla3.Rows[i].Cells[18].Value = első + második;
                    }
                    Tábla3.Refresh();
                    Tábla3.Visible = true;
                }
                Tábla3.Refresh();
                Tábla3.Visible = true;
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

        private void Tábla3_harmadik()
        {
            try
            {
                Holtart.Be();
                Tábla3.Refresh();
                Tábla3.Visible = false;
                for (int j = 4; j < 16; j++)
                {
                    string hónap = j - 3 > 9 ? (j - 3).ToString() : "0" + (j - 3);
                    string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\Beosztás\{Dátum.Value.Year}\Ebeosztás{Dátum.Value.Year}{hónap}.mdb";
                    if (File.Exists(hely))
                    {
                        string jelszó = "kiskakas";
                        string szöveg = "SELECT * FROM beosztás";

                        Kezelő_Dolgozó_Beosztás_Új Kéz = new Kezelő_Dolgozó_Beosztás_Új();
                        List<Adat_Dolgozó_Beosztás_Új> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);


                        if (Adatok != null)
                        {
                            for (int i = 0; i < Tábla3.Rows.Count; i += 3)
                            {
                                Holtart.Lép();
                                string Hrazonosító = Tábla3.Rows[i].Cells[1].Value.ToString().Trim();

                                int vmunkaóra = (from a in Adatok
                                                 where a.Dolgozószám.Trim() == Hrazonosító && !a.Beosztáskód.Contains("FÜ")
                                                 select a).Sum(a => a.Ledolgozott);

                                int velvont = (from a in Adatok
                                               where a.Dolgozószám.Trim() == Hrazonosító && (a.Túlóraok.Contains("&EP") || a.Túlóraok.Contains("&EB"))
                                               select a).Sum(a => a.Túlóra);

                                int vtúlóra = (from a in Adatok
                                               where a.Dolgozószám.Trim() == Hrazonosító && a.Túlóraok.Contains("&T")
                                               select a).Sum(a => a.Túlóra);

                                Tábla3.Rows[i + 1].Cells[j].Value = velvont + vtúlóra;
                                Tábla3.Rows[i + 2].Cells[j].Value = vmunkaóra - (velvont + vtúlóra);
                            }
                        }
                    }
                }
                Tábla3.Refresh();
                Tábla3.Visible = true;

                int előírt;
                int felhasznált;
                //Összesítés
                for (int i = 0; i < Tábla3.Rows.Count; i += 3)
                {
                    Holtart.Lép();
                    int Túlóra1 = 0;
                    int felhasznált1 = 0;
                    int Túlóra2 = 0;
                    int felhasznált2 = 0;
                    for (int j = 4; j < 16; j++)
                    {
                        előírt = int.Parse(Tábla3.Rows[i + 1].Cells[j].Value.ToString());
                        felhasznált = int.Parse(Tábla3.Rows[i + 2].Cells[j].Value.ToString());
                        if (j < 10)
                        {
                            Túlóra1 += előírt;
                            felhasznált1 += felhasznált;
                        }
                        else
                        {
                            Túlóra2 += előírt;
                            felhasznált2 += felhasznált;
                        }
                    }
                    Tábla3.Rows[i + 1].Cells[16].Value = Túlóra1;
                    Tábla3.Rows[i + 2].Cells[16].Value = felhasznált1;
                    Tábla3.Rows[i + 1].Cells[17].Value = Túlóra2;
                    Tábla3.Rows[i + 2].Cells[17].Value = felhasznált2;
                    Tábla3.Rows[i + 1].Cells[18].Value = Túlóra1 + Túlóra2;
                    Tábla3.Rows[i + 2].Cells[18].Value = felhasznált1 + felhasznált2;
                }



                // összehasonlítás havonta

                Tábla3.Refresh();
                Tábla3.Visible = false;

                for (int i = 0; i < Tábla3.Rows.Count; i += 3)
                {
                    Holtart.Lép();
                    //havi adatok ellenőrzése
                    for (int j = 4; j < 16; j++)
                    {
                        előírt = int.Parse(Tábla3.Rows[i].Cells[j].Value.ToString());
                        felhasznált = int.Parse(Tábla3.Rows[i + 2].Cells[j].Value.ToString());
                        Színezünk(előírt, felhasznált, i + 2, j);
                    }
                    //első félév
                    előírt = int.Parse(Munka1fél.Text);
                    felhasznált = int.Parse(Tábla3.Rows[i + 2].Cells[16].Value.ToString());
                    Színezünk(előírt, felhasznált, i + 2, 16);
                    //másidik félév
                    előírt = int.Parse(Munka2fél.Text);
                    felhasznált = int.Parse(Tábla3.Rows[i + 2].Cells[17].Value.ToString());
                    Színezünk(előírt, felhasznált, i + 2, 17);
                }
                Tábla3.Refresh();
                Tábla3.Visible = true;

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

        private void Színezünk(int előírt, int felhasznált, int sor, int oszlop)
        {
            if (előírt == felhasznált)
            {
                // ha jó
                Tábla3.Rows[sor].Cells[oszlop].Style.BackColor = Color.Green;
                Tábla3.Rows[sor].Cells[oszlop].Style.ForeColor = Color.Black;
            }

            else if (előírt < felhasznált)
            {
                // ha nagyobb
                Tábla3.Rows[sor].Cells[oszlop].Style.BackColor = Color.Red;
                Tábla3.Rows[sor].Cells[oszlop].Style.ForeColor = Color.White;
            }

            else
            {
                // ha kisebb
                Tábla3.Rows[sor].Cells[oszlop].Style.BackColor = Color.Yellow;
                Tábla3.Rows[sor].Cells[oszlop].Style.ForeColor = Color.Black;
            }
        }
        #endregion


        #region váltóműszak  elvont feladás
        private void Táblakiírás_Click(object sender, EventArgs e)
        {
            Táblakiírás_feladás();
        }


        private void Táblakiírás_feladás()
        {
            try
            {
                if (Csoport.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva listázandó csoport.");
                if (Év.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva listázandó Év.");
                if (Félév.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva listázandó Félév.");
                if (TelephelyiVáltozat.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva listázandó telephelyi változat.");

                string helyelv = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\munkaidőnaptár.mdb";
                string jelszóelv = "katalin";
                string szövegelv = "SELECT * FROM kijelöltnapok ";
                if (Félév.Text == "1")
                    szövegelv += " where (dátum>=#1/1/" + Év.Text + "# and dátum  <=#6/30/" + Év.Text + "#)";
                else
                    szövegelv += " where (dátum>=#7/1/" + Év.Text + "# and dátum  <=#12/31/" + Év.Text + "#)";

                szövegelv += $" and telephely='{TelephelyiVáltozat.Text.Trim()}'";
                szövegelv += $" and csoport='{Csoport.Text.Trim()}'";
                Kezelő_Váltós_Kijelöltnapok KézVált = new Kezelő_Váltós_Kijelöltnapok();
                List<Adat_Váltós_Kijelöltnapok> AdatokVált = KézVált.Lista_Adatok(helyelv, jelszóelv, szövegelv) ?? throw new HibásBevittAdat("Nincs a feltételeknek megfelelő beállított érték.");

                switch (Csoport.Text.Trim())
                {
                    case "6.1":
                        szövegelv = "SELECT * FROM Naptár1";
                        break;
                    case "6.2":
                        szövegelv = "SELECT * FROM Naptár2";
                        break;
                    case "6.3":
                        szövegelv = "SELECT * FROM Naptár3";
                        break;
                    case "6.4":
                        szövegelv = "SELECT * FROM Naptár4";
                        break;
                    case "É.1":
                        szövegelv = "SELECT * FROM Naptár5";
                        break;
                    case "É.2":
                        szövegelv = "SELECT * FROM Naptár6";
                        break;
                }
                Kezelő_Váltós_Naptár KézNaptár = new Kezelő_Váltós_Naptár();
                List<Adat_Váltós_Naptár> AdatokNaptár = KézNaptár.Lista_Adatok(helyelv, jelszóelv, szövegelv);

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
                string jelszó = "forgalmiutasítás";
                string szöveg = "SELECT * FROM Dolgozóadatok where kilépésiidő=#1/1/1900#";
                if (Csoport.Text.Trim() == "Összes")
                    szöveg += " and csoportkód<>''";
                else
                    szöveg += $" and csoportkód='{Csoport.Text.Trim()}'";

                szöveg += " order by DolgozóNév asc ";

                Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> AdatokDolg = KézDolg.Lista_Adatok(hely, jelszó, szöveg) ?? throw new HibásBevittAdat("Nincs a feltételeknek megfelelő dolgozók listája.");

                hely = Application.StartupPath + @"\Főmérnökség\Adatok\kiegészítő2.mdb";
                jelszó = "Mocó";
                szöveg = "SELECT * FROM beosztásciklus";
                Kezelő_Kiegészítő_Beosztásciklus KÉZBEO = new Kezelő_Kiegészítő_Beosztásciklus();
                List<Adat_Kiegészítő_Beosztásciklus> AdatokVáltCiklus = KÉZBEO.Lista_Adatok(hely, jelszó, szöveg);

                szöveg = "SELECT * FROM éjszakásCiklus";
                List<Adat_Kiegészítő_Beosztásciklus> AdatokÉCiklus = KÉZBEO.Lista_Adatok(hely, jelszó, szöveg);

                szöveg = "SELECT * FROM váltósbeosztás";
                Kezelő_Kiegészítő_Váltóstábla KézVáltBeo = new Kezelő_Kiegészítő_Váltóstábla();
                List<Adat_Kiegészítő_Váltóstábla> AdatokVáltBeo = KézVáltBeo.Lista_Adatok(hely, jelszó, szöveg);

                hely = Application.StartupPath + @"\Főmérnökség\Adatok\kiegészítő1.mdb";
                szöveg = "SELECT * FROM beosegéd ";
                Kezelő_Kiegészítő_Beosegéd KézBEOsegéd = new Kezelő_Kiegészítő_Beosegéd();
                List<Adat_Kiegészítő_Beosegéd> AdatokBEOsegéd = KézBEOsegéd.Lista_Adatok(hely, jelszó, szöveg);


                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();

                Tábla.ColumnCount = 9;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Név:";
                Tábla.Columns[0].Width = 300;
                Tábla.Columns[1].HeaderText = "HR azonosító";
                Tábla.Columns[1].Width = 150;
                Tábla.Columns[2].HeaderText = "Csoportkód";
                Tábla.Columns[2].Width = 120;
                Tábla.Columns[3].HeaderText = "Elvont nap";
                Tábla.Columns[3].Width = 120;
                Tábla.Columns[4].HeaderText = "Beosztáskód";
                Tábla.Columns[4].Width = 110;
                Tábla.Columns[5].HeaderText = "Túlóra";
                Tábla.Columns[5].Width = 100;
                Tábla.Columns[6].HeaderText = "Túlóra kezdete";
                Tábla.Columns[6].Width = 100;
                Tábla.Columns[7].HeaderText = "Túlóra vége";
                Tábla.Columns[7].Width = 100;
                Tábla.Columns[8].HeaderText = "Túlóra oka";
                Tábla.Columns[8].Width = 400;

                foreach (Adat_Dolgozó_Alap rekord in AdatokDolg)
                {
                    foreach (Adat_Váltós_Kijelöltnapok rekordelv in AdatokVált)
                    {
                        Tábla.RowCount++;
                        int i = Tábla.RowCount - 1;

                        Tábla.Rows[i].Cells[0].Value = rekord.DolgozóNév.Trim();
                        Tábla.Rows[i].Cells[1].Value = rekord.Dolgozószám.Trim();
                        Tábla.Rows[i].Cells[2].Value = rekord.Csoportkód.Trim();
                        Tábla.Rows[i].Cells[3].Value = rekordelv.Dátum.ToString("yyy.MM.dd");

                        string BeosztásKód = (from a in AdatokNaptár
                                              where a.Dátum == rekordelv.Dátum
                                              select a.Nap).FirstOrDefault();

                        if (BeosztásKód != null)
                        {
                            switch (BeosztásKód)
                            {
                                case "E":
                                    {
                                        if (!rekord.Csoportkód.Contains("É"))
                                            BeosztásKód = "7"; // ha váltós
                                        else
                                            BeosztásKód = "8";// ha állandó éjszakás
                                        break;
                                    }
                                case "Z":
                                    {
                                        if (!rekord.Csoportkód.Contains("É"))
                                            BeosztásKód = "7"; // ha váltós
                                        else
                                            BeosztásKód = "8";// ha állandó éjszakás
                                        break;
                                    }
                                case "P":
                                    {
                                        BeosztásKód = "";
                                        break;
                                    }
                            }
                        }
                        Tábla.Rows[i].Cells[4].Value = BeosztásKód.Trim();
                        Adat_Kiegészítő_Beosegéd Elem = (from a in AdatokBEOsegéd
                                                         where a.Telephely.Trim() == TelephelyiVáltozat.Text.Trim() && a.Beosztáskód.Trim() == BeosztásKód
                                                         select a).FirstOrDefault();
                        if (Elem != null)
                        {
                            Tábla.Rows[i].Cells[5].Value = Elem.Túlóra;
                            Tábla.Rows[i].Cells[6].Value = Elem.Kezdőidő.ToString("HH:mm");
                            Tábla.Rows[i].Cells[7].Value = Elem.Végeidő.ToString("HH:mm");
                            Tábla.Rows[i].Cells[8].Value = Elem.Túlóraoka.Trim();
                        }

                    }
                }
                Tábla.Visible = true;
                Tábla.Refresh();
                Tábla.ClearSelection();

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


        private void Excel_Elvont_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Elvont_pihenőnapok_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, Tábla, true);
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


        private void Subtelephelyiváltozat()
        {
            if (Év.Text.Trim() == "")
                return;
            TelephelyiVáltozat.Items.Clear();
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\munkaidőnaptár.mdb";
            string jelszó = "katalin";
            string szöveg = $"SELECT DISTINCT telephely FROM kijelöltnapok where (dátum>=#1/1/{Év.Text.Trim()}# and dátum<=#12/31/{Év.Text.Trim()}#) ORDER BY telephely";

            TelephelyiVáltozat.BeginUpdate();
            TelephelyiVáltozat.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "telephely"));
            TelephelyiVáltozat.EndUpdate();
        }


        private void Félév_feltöltés()
        {

            Félév.Items.Clear();
            Félév.BeginUpdate();
            Félév.Items.Add("1");
            Félév.Items.Add("2");
            Félév.EndUpdate();
        }


        private void Év_feltöltés()
        {
            Év.Items.Clear();
            Év.BeginUpdate();
            Év.Items.Add(Dátum.Value.Year);

            Év.EndUpdate();
            Év.Text = Év.Items[Év.Items.Count - 1].ToString();
        }


        private void Turnuskiirás()
        {
            if (Év.Text.Trim() == "")
                return;
            Csoport.Items.Clear();
            string hely = Application.StartupPath + @"\Főmérnökség\adatok\kiegészítő1.mdb";
            string jelszó = "Mocó";
            string szöveg = "SELECT * FROM turnusok order by csoport";

            Csoport.BeginUpdate();
            Csoport.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "csoport"));
            Csoport.EndUpdate();
        }


        private void Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Év.Text, out int VáltÉv))
                    throw new HibásBevittAdat("Az évnek egész számnak kell lennie és a mező nem lehet üres.");
                if (!int.TryParse(Félév.Text, out int VáltFélév))
                    throw new HibásBevittAdat("Az félévnek egész számnak kell lennie és a mező nem lehet üres.");
                if (TelephelyiVáltozat.Text.Trim() == "")
                    throw new HibásBevittAdat("A telephelyi változat nem lehet üres.");
                if (Csoport.Text.Trim() == "")
                    throw new HibásBevittAdat("A csoport mező nem lehet üres.");

                Táblakiírás_feladás();

                if (Tábla.Rows.Count < 1)
                    throw new HibásBevittAdat("A táblázat nem tartalmaz elemet, így nincs mit rögzíteni.");

                DateTime vdátum = DateTime.Parse(Tábla.Rows[0].Cells[3].Value.ToString());
                // leellenőrizzük, hogy van-e adatbázis
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\Beosztás\{vdátum.Year}";
                if (!Exists(hely))
                    Directory.CreateDirectory(hely);// Megnézzük, hogy létezik-e a könyvtár, ha nem létrehozzuk

                Beosztás_Rögzítés BR = new Beosztás_Rögzítés();
                Holtart.Be();
                for (int j = 0; j < Tábla.Rows.Count; j++)
                {
                    vdátum = DateTime.Parse(Tábla.Rows[j].Cells[3].Value.ToString());
                    hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\Beosztás\{vdátum.Year}\Ebeosztás{vdátum:yyyyMM}.mdb";
                    if (!Exists(hely))
                        Adatbázis_Létrehozás.Dolgozói_Beosztás_Adatok_Új(hely);

                    string dolgozónév = Tábla.Rows[j].Cells[0].Value.ToString().Trim();
                    string dolgozószám = Tábla.Rows[j].Cells[1].Value.ToString().Trim();
                    string Beosztáskód = Tábla.Rows[j].Cells[4].Value.ToString().Trim();
                    int Túlóra = int.Parse(Tábla.Rows[j].Cells[5].Value.ToString());
                    DateTime Kezdőidő = DateTime.Parse(Tábla.Rows[j].Cells[6].Value.ToString());
                    DateTime Végeidő = DateTime.Parse(Tábla.Rows[j].Cells[7].Value.ToString());
                    string Túlóraoka = Tábla.Rows[j].Cells[8].Value.ToString().Trim();
                    BR.Rögzít_Túlóra(Cmbtelephely.Text.Trim(), vdátum, Beosztáskód, dolgozószám, Túlóra, Kezdőidő, Végeidő, Túlóra, Túlóraoka, dolgozónév);
                    Holtart.Lép();
                }
                Holtart.Ki();
                MessageBox.Show("Az elvont pihenőnapok rögzítésre kerültek.", "Tájéloztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Csoport választás
        private void Csoportfeltöltés()
        {
            Csoportlista.Items.Clear();
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Segéd\kiegészítő.mdb";
            string jelszó = "Mocó";

            string szöveg = "SELECT * FROM csoportbeosztás order by Sorszám";

            Csoportlista.BeginUpdate();
            Csoportlista.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "csoportbeosztás"));
            Csoportlista.EndUpdate();
        }


        private void NyitCsoport_Click(object sender, EventArgs e)
        {
            Csoportlista.Height = 300;
            CsukCsoport.Visible = true;
            NyitCsoport.Visible = false;
        }


        private void CsukCsoport_Click(object sender, EventArgs e)
        {
            Visszacsukcsoport();
        }


        private void Visszacsukcsoport()
        {
            Csoportlista.Height = 25;
            CsukCsoport.Visible = false;
            NyitCsoport.Visible = true;
        }


        private void Csoportkijelölmind_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Csoportlista.Items.Count; i++)
                Csoportlista.SetItemChecked(i, true);
            Visszacsukcsoport();
            Csoport_listáz();
        }


        private void Csoportvissza_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Csoportlista.Items.Count; i++)
                Csoportlista.SetItemChecked(i, false);
            Visszacsukcsoport();
            Csoport_listáz();
        }


        private void CsoportFrissít_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // homok óra kezdete
            Visszacsukcsoport();
            Csoport_listáz();
            Panel2.Visible = false;
            Cursor = Cursors.Default; // homokóra vége
        }


        private void Csoport_listáz()
        {
            // minden kijelölést töröl
            for (int i = 0; i < Dolgozónév.Items.Count; i++)
                Dolgozónév.SetItemChecked(i, false);
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
            string jelszó = "forgalmiutasítás";

            for (int j = 0; j < Csoportlista.CheckedItems.Count; j++)
            {
                // lekéredezzük a csoport tagjait
                string szöveg = $"SELECT * FROM  dolgozóadatok WHERE csoport='{Csoportlista.CheckedItems[j].ToString().Trim()}' order by dolgozónév";
                Kezelő_Dolgozó_Alap Kéz = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                for (int i = 0; i < Dolgozónév.Items.Count; i++)
                {
                    string[] darabol = Dolgozónév.Items[i].ToString().Split('=');
                    string Elem = (from a in Adatok
                                   where a.Dolgozószám.Trim() == darabol[1].Trim()
                                   select a.Dolgozószám).FirstOrDefault();
                    if (Elem != null)
                        Dolgozónév.SetItemChecked(i, true);
                }
            }
        }
        #endregion


        #region Dolgozónév választás
        private void Névfeltöltés()
        {
            Dolgozónév.Items.Clear();
            Dolgozónév.BeginUpdate();
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
            string jelszó = "forgalmiutasítás";

            string szöveg = "SELECT * FROM Dolgozóadatok ";
            if (!Kilépettjel.Checked)
                szöveg += " WHERE kilépésiidő=#01-01-1900# ";
            szöveg += " ORDER BY DolgozóNév asc";

            Kezelő_Dolgozó_Alap Kéz = new Kezelő_Dolgozó_Alap();
            List<Adat_Dolgozó_Alap> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

            foreach (Adat_Dolgozó_Alap rekord in Adatok)
            {
                Dolgozónév.Items.Add(rekord.DolgozóNév.Trim() + " = " + rekord.Dolgozószám.Trim());
            }
            Dolgozónév.EndUpdate();
        }


        private void Nyitdolgozó_Click(object sender, EventArgs e)
        {
            Dolgozónév.Height = 500;
            CsukDolgozó.Visible = true;
            NyitDolgozó.Visible = false;
        }


        private void Csukdolgozó_Click(object sender, EventArgs e)
        {
            Visszacsukjadolgozó();
        }


        private void Visszacsukjadolgozó()
        {
            Dolgozónév.Height = 25;
            CsukDolgozó.Visible = false;
            NyitDolgozó.Visible = true;
        }


        private void Dolgozókijelölmind_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Dolgozónév.Items.Count; i++)
                Dolgozónév.SetItemChecked(i, true);
            Visszacsukjadolgozó();
        }


        private void Dolgozóvissza_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Dolgozónév.Items.Count; i++)
                Dolgozónév.SetItemChecked(i, false);
            Visszacsukjadolgozó();
        }


        private void CsoportFrissít_Click_1(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; // homok óra kezdete
            Visszacsukcsoport();
            Csoport_listáz();
            Cursor = Cursors.Default; // homokóra vége
        }


        private void Kilépettjel_CheckedChanged(object sender, EventArgs e)
        {
            Névfeltöltés();
        }
        #endregion

    }
}