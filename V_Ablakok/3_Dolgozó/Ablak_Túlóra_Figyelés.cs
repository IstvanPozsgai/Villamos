using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Ablakok.Beosztás;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_Túlóra_Figyelés
    {
        double Határ = 0;
        #region Kezelők
        readonly Kezelő_Kiegészítő_Túlórakeret KézTúl = new Kezelő_Kiegészítő_Túlórakeret();
        readonly Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_Szatube_Túlóra KézTúlóra = new Kezelő_Szatube_Túlóra();
        readonly Kezelő_Kiegészítő_Munkaidő KézMunka = new Kezelő_Kiegészítő_Munkaidő();
        readonly Kezelő_Váltós_Naptár KézVáltósNaptár = new Kezelő_Váltós_Naptár();
        readonly Kezelő_Váltós_Összesítő KézÖsszesítő = new Kezelő_Váltós_Összesítő();
        readonly Kezelő_Dolgozó_Beosztás_Új KézBeosztás = new Kezelő_Dolgozó_Beosztás_Új();
        readonly Kezelő_Váltós_Kijelöltnapok KézVáltKijelölt = new Kezelő_Váltós_Kijelöltnapok();
        readonly Kezelő_Kiegészítő_Beosztásciklus KÉZBEO = new Kezelő_Kiegészítő_Beosztásciklus();
        readonly Kezelő_Kiegészítő_Váltóstábla KézVáltBeo = new Kezelő_Kiegészítő_Váltóstábla();
        readonly Kezelő_Kiegészítő_Beosegéd KézBEOsegéd = new Kezelő_Kiegészítő_Beosegéd();
        readonly Kezelő_Kiegészítő_Turnusok KézTurnus = new Kezelő_Kiegészítő_Turnusok();
        readonly Kezelő_Kiegészítő_Csoportbeosztás KézCSopBeo = new Kezelő_Kiegészítő_Csoportbeosztás();
        #endregion


        #region Alap
        public Ablak_Túlóra_Figyelés()
        {
            InitializeComponent();
        }

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
            Csoportfeltöltés();
            Névfeltöltés();

            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
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
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    Cmbtelephely.Items.Add(Elem); ;
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
                if (Tábla2.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Túlórakeret_Ellenőrzés_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

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

        private void Command20_Click(object sender, EventArgs e)
        {
            Tábla2_egyik();
            Tábla2_másik();
        }

        private void Tábla2_egyik()
        {
            try
            {
                List<Adat_Kiegészítő_Túlórakeret> AdatokTúl = KézTúl.Lista_Adatok();
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

                List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim()).Where(a => a.Kilépésiidő.ToShortDateString() == MyF.ElsőNap().ToShortDateString()).ToList();

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
                List<Adat_Szatube_Túlóra> Adatok = KézTúlóra.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year).Where(a => a.Státus != 3).ToList();

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

            List<Adat_Kiegészítő_Munkaidő> AdatokMunka = KézMunka.Lista_Adatok();
            Adat_Kiegészítő_Munkaidő Elem = (from a in AdatokMunka
                                             where a.Munkarendelnevezés == "8"
                                             select a).FirstOrDefault();
            double óra;
            if (Elem != null) óra = Elem.Munkaidő; else óra = 0;

            List<Adat_Váltós_Naptár> Adatok = KézVáltósNaptár.Lista_Adatok(Dátum.Value.Year, "");
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
                if (Tábla3.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Munkaidőkeret_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
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

        /// <summary>
        /// Üres táblát készíti el a nevekkel és hónapokkal minden második embert más színnel színezi, hogy ne legyen összefolyós
        /// 
        /// </summary>
        private void Tábla3_első_új()
        {
            try
            {
                if (Dolgozónév.CheckedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy dolgozó sem.");

                List<Adat_Dolgozó_Alap> AdatokDolg = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());
                if (!Kilépettjel.Checked) AdatokDolg = AdatokDolg.Where(a => a.Kilépésiidő.ToShortDateString() == MyF.ElsőNap().ToShortDateString()).ToList();
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

        /// <summary>
        /// Kiírja az előírás értékeket
        /// </summary>
        private void Tábla3_második()
        {
            try
            {
                // kiirjuk az előírásokat a nyolc órásoknak
                List<Adat_Váltós_Összesítő> Adatok = new List<Adat_Váltós_Összesítő>();
                List<Adat_Váltós_Összesítő> Ideig = KézÖsszesítő.Lista_Adatok(Dátum.Value.Year, "");
                Adatok.AddRange(Ideig);

                Ideig = KézÖsszesítő.Lista_Adatok(Dátum.Value.Year, "1");
                Adatok.AddRange(Ideig);

                Ideig = KézÖsszesítő.Lista_Adatok(Dátum.Value.Year, "2");
                Adatok.AddRange(Ideig);

                Ideig = KézÖsszesítő.Lista_Adatok(Dátum.Value.Year, "3");
                Adatok.AddRange(Ideig);

                Ideig = KézÖsszesítő.Lista_Adatok(Dátum.Value.Year, "4");
                Adatok.AddRange(Ideig);

                Ideig = KézÖsszesítő.Lista_Adatok(Dátum.Value.Year, "5");
                Adatok.AddRange(Ideig);

                Ideig = KézÖsszesítő.Lista_Adatok(Dátum.Value.Year, "6");
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
                        if (beolvasott == "É.1") beolvasott = "É_5";
                        if (beolvasott == "É.2") beolvasott = "É_6";

                        if (beolvasott.Substring(0, 1) != "R")
                        {
                            for (int j = 1; j <= 12; j++)
                            {
                                string[] darabol = beolvasott.Split('_');
                                Adat_Váltós_Összesítő kötelező = (from a in Adatok
                                                                  where a.Dátum.Month == j && a.Csoport.Trim() == darabol[1].Trim()
                                                                  select a).FirstOrDefault();
                                if (kötelező != null)
                                {
                                    Tábla3.Rows[i].Cells[j + 3].Value = kötelező.Perc.ToString();
                                    if (j < 7)
                                        első += kötelező.Perc;
                                    else
                                        második += kötelező.Perc;
                                }
                            }
                        }
                        else
                        // részmunkaidős
                        {
                            for (int j = 1; j <= 12; j++)
                            {
                                Adat_Váltós_Összesítő kötelező = (from a in Adatok
                                                                  where a.Dátum.Month == j && a.Csoport.Trim() == "N"
                                                                  select a).FirstOrDefault();
                                if (kötelező != null)
                                {
                                    if (!double.TryParse(beolvasott.Substring(1, beolvasott.Length - 1), out double Keret)) Keret = 1;
                                    double részidő = kötelező.Perc / 480 * Keret;
                                    Tábla3.Rows[i].Cells[j + 3].Value = részidő;
                                    if (j < 7)
                                        első += részidő;
                                    else
                                        második += részidő;
                                }
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

        /// <summary>
        /// Kiírja a táblázatba a tényadatokat és mind a beosztási és mind a túlóra adatokat.
        /// </summary>
        private void Tábla3_harmadik()
        {
            try
            {
                Holtart.Be();
                Tábla3.Refresh();
                Tábla3.Visible = false;
                for (int j = 4; j < 16; j++)
                {
                    DateTime MelyikHónap = new DateTime(Dátum.Value.Year, j - 3, 1);
                    List<Adat_Dolgozó_Beosztás_Új> Adatok = KézBeosztás.Lista_Adatok(Cmbtelephely.Text.Trim(), MelyikHónap);
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
                if (Csoport.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva listázandó csoport.");
                if (!int.TryParse(Év.Text.Trim(), out int ÉV)) throw new HibásBevittAdat("Nincs kiválasztva listázandó Év.");
                if (!int.TryParse(Félév.Text.Trim(), out int FélÉV)) throw new HibásBevittAdat("Nincs kiválasztva listázandó Félév.");
                if (TelephelyiVáltozat.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva listázandó telephelyi változat.");

                List<Adat_Váltós_Kijelöltnapok> AdatokVált = KézVáltKijelölt.Lista_Adatok(Dátum.Value.Year);
                AdatokVált = (from a in AdatokVált
                              where a.Telephely == TelephelyiVáltozat.Text.Trim()
                              && a.Csoport == Csoport.Text.Trim()
                              select a).ToList();

                if (FélÉV == 1)
                    AdatokVált = (from a in AdatokVált
                                  where a.Dátum >= MyF.Év_elsőnapja(ÉV)
                                  && a.Dátum <= MyF.Félév_utolsónapja(ÉV)
                                  select a).ToList();
                else
                    AdatokVált = (from a in AdatokVált
                                  where a.Dátum >= MyF.Félév_elsőnapja(ÉV)
                                  && a.Dátum <= MyF.Év_utolsónapja(ÉV)
                                  select a).ToList();

                if (AdatokVált.Count == 0) throw new HibásBevittAdat("Nincs a feltételeknek megfelelő beállított érték.");

                string szövegelv = "";
                switch (Csoport.Text.Trim())
                {
                    case "6.1":
                        szövegelv = "1";
                        break;
                    case "6.2":
                        szövegelv = "2";
                        break;
                    case "6.3":
                        szövegelv = "3";
                        break;
                    case "6.4":
                        szövegelv = "4";
                        break;
                    case "É.1":
                        szövegelv = "5";
                        break;
                    case "É.2":
                        szövegelv = "6";
                        break;
                }
                List<Adat_Váltós_Naptár> AdatokNaptár = KézVáltósNaptár.Lista_Adatok(ÉV, szövegelv);

                List<Adat_Dolgozó_Alap> AdatokDolg = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim()).Where(a => a.Kilépésiidő == MyF.ElsőNap()).ToList();
                if (Csoport.Text.Trim() == "Összes")
                    AdatokDolg = AdatokDolg.Where(a => a.Csoport != "").ToList();
                else
                    AdatokDolg = AdatokDolg.Where(a => a.Csoport == Csoport.Text.Trim()).ToList();
                if (AdatokDolg.Count == 0) throw new HibásBevittAdat("Nincs a feltételeknek megfelelő dolgozók listája.");

                List<Adat_Kiegészítő_Beosztásciklus> AdatokVáltCiklus = KÉZBEO.Lista_Adatok("beosztásciklus");
                List<Adat_Kiegészítő_Beosztásciklus> AdatokÉCiklus = KÉZBEO.Lista_Adatok("éjszakásCiklus");
                List<Adat_Kiegészítő_Váltóstábla> AdatokVáltBeo = KézVáltBeo.Lista_Adatok();
                List<Adat_Kiegészítő_Beosegéd> AdatokBEOsegéd = KézBEOsegéd.Lista_Adatok();


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
                if (Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Elvont_pihenőnapok_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Module_Excel.DataGridViewToExcel(fájlexc, Tábla);
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
            if (Év.Text.Trim() == "") return;
            TelephelyiVáltozat.Items.Clear();

            List<Adat_Váltós_Kijelöltnapok> Adatok = KézVáltKijelölt.Lista_Adatok(Dátum.Value.Year);
            List<string> Lista = Adatok.Select(a => a.Telephely).Distinct().ToList();

            foreach (string Elem in Lista)
                TelephelyiVáltozat.Items.Add(Elem);
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
            if (Év.Text.Trim() == "") return;
            Csoport.Items.Clear();
            List<Adat_Kiegészítő_Turnusok> Adatok = KézTurnus.Lista_Adatok();

            foreach (Adat_Kiegészítő_Turnusok Elem in Adatok)
                Csoport.Items.Add(Elem.Csoport);
        }

        // aaa  Ezt még egyesíteni kell a rögzítés során
        private void Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Év.Text, out int VáltÉv)) throw new HibásBevittAdat("Az évnek egész számnak kell lennie és a mező nem lehet üres.");
                if (!int.TryParse(Félév.Text, out int VáltFélév)) throw new HibásBevittAdat("Az félévnek egész számnak kell lennie és a mező nem lehet üres.");
                if (TelephelyiVáltozat.Text.Trim() == "") throw new HibásBevittAdat("A telephelyi változat nem lehet üres.");
                if (Csoport.Text.Trim() == "") throw new HibásBevittAdat("A csoport mező nem lehet üres.");

                Táblakiírás_feladás();

                if (Tábla.Rows.Count < 1) throw new HibásBevittAdat("A táblázat nem tartalmaz elemet, így nincs mit rögzíteni.");

                DateTime vdátum = DateTime.Parse(Tábla.Rows[0].Cells[3].Value.ToString());

                Beosztás_Rögzítés BR = new Beosztás_Rögzítés();
                Holtart.Be();
                for (int j = 0; j < Tábla.Rows.Count; j++)
                {
                    vdátum = DateTime.Parse(Tábla.Rows[j].Cells[3].Value.ToString());
                    string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\Beosztás\{vdátum.Year}\Ebeosztás{vdátum:yyyyMM}.mdb";


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
            List<Adat_Kiegészítő_Csoportbeosztás> Adatok = KézCSopBeo.Lista_Adatok(Cmbtelephely.Text.Trim());

            foreach (Adat_Kiegészítő_Csoportbeosztás Elem in Adatok)
                Csoportlista.Items.Add(Elem.Csoportbeosztás);
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

            List<Adat_Dolgozó_Alap> AdatokÖ = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());

            for (int j = 0; j < Csoportlista.CheckedItems.Count; j++)
            {
                // lekéredezzük a csoport tagjait
                List<Adat_Dolgozó_Alap> Adatok = (from a in AdatokÖ
                                                  where a.Csoport == Csoportlista.CheckedItems[j].ToString().Trim()
                                                  select a).ToList();
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
        /// <summary>
        /// Dolgozókat tölti fel a dolgozók listáját a telephelyhez tartozóan.
        /// </summary>
        private void Névfeltöltés()
        {
            Dolgozónév.Items.Clear();
            Dolgozónév.BeginUpdate();

            List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());
            if (!Kilépettjel.Checked)
                Adatok = Adatok.Where(a => a.Kilépésiidő == MyF.ElsőNap()).ToList();

            foreach (Adat_Dolgozó_Alap rekord in Adatok)
                Dolgozónév.Items.Add(rekord.DolgozóNév.Trim() + " = " + rekord.Dolgozószám.Trim());

            Dolgozónév.EndUpdate();
        }

        /// <summary>
        ///  A dolgozónév checklistbox nyitása, hogy a dolgozók listája látható legyen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Nyitdolgozó_Click(object sender, EventArgs e)
        {
            Dolgozónév.Height = 500;
            CsukDolgozó.Visible = true;
            NyitDolgozó.Visible = false;
        }

        /// <summary>
        /// A dolgozónév checklistbox bezárása, hogy a dolgozók listája ne legyen látható.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Csukdolgozó_Click(object sender, EventArgs e)
        {
            Visszacsukjadolgozó();
        }

        /// <summary>
        /// A dolgozónév checklistbox bezárása, hogy a dolgozók listája ne legyen látható.
        /// </summary>
        private void Visszacsukjadolgozó()
        {
            Dolgozónév.Height = 25;
            CsukDolgozó.Visible = false;
            NyitDolgozó.Visible = true;
        }

        /// <summary>
        /// Minden dolgozó checkbox kijelölése a dolgozók listájában.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dolgozókijelölmind_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Dolgozónév.Items.Count; i++)
                Dolgozónév.SetItemChecked(i, true);
            Visszacsukjadolgozó();
        }

        /// <summary>
        /// Minden dolgozó checkbox kijelölésének törlése a dolgozók listájában.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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