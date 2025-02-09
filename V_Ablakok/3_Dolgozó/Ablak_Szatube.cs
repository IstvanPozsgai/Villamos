using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;


namespace Villamos
{
    public partial class Ablak_Szatube
    {

        string Texttelephely;
        string Textlista;
        string hely;
        readonly string jelszó = "kertitörpe";

        readonly Kezelő_Szatube_Szabadság Kéz_Szabadság = new Kezelő_Szatube_Szabadság();
        readonly Kezelő_Dolgozó_Személyes KézSzemélyes = new Kezelő_Dolgozó_Személyes();
        List<Adat_Szatube_Szabadság> Adatok_Szabadság = new List<Adat_Szatube_Szabadság>();

        public Ablak_Szatube()
        {
            InitializeComponent();
        }


        private void Ablak_Szatube_Load(object sender, EventArgs e)
        {
            Telephelyekfeltöltése();

            hely = $@"{Application.StartupPath}\{CmbTelephely.Text.Trim()}\adatok\Szatubecs";
            if (!Exists(hely)) Directory.Exists(hely);

            hely = $@"{Application.StartupPath}\{CmbTelephely.Text.Trim()}\adatok\Szatubecs\{DateTime.Now.Year}Szatubecs.mdb";
            if (!Exists(hely)) Adatbázis_Létrehozás.SzaTuBe_tábla(hely);

            Évek_Feltöltése();

            Névfeltöltés();
            Munkahely();
            TabFülek.TabIndex = 0;

            // Gombok nem láthatóak mert mindent listáz
            SzabLeadás.Visible = false;
            SzabNyomtatás.Visible = false;
            EgyéniTúlNyom.Visible = false;
            TúlCsopNyom.Visible = false;
            Túl_Eng_Beáll.Visible = false;

            Jogosultságkiosztás();

            TabFülek.DrawMode = TabDrawMode.OwnerDrawFixed;
        }


        #region  Alap

        private void Telephelyekfeltöltése()
        {
            try
            {
                CmbTelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    CmbTelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség")
                { CmbTelephely.Text = CmbTelephely.Items[0].ToStrTrim(); }
                else
                { CmbTelephely.Text = Program.PostásTelephely; }

                CmbTelephely.Text = Program.PostásTelephely;
                CmbTelephely.Enabled = Program.Postás_Vezér;
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


        private void Névfeltöltés()
        {
            Dolgozónév.Items.Clear();
            Dolgozónév.BeginUpdate();
            string helyn = $@"{Application.StartupPath}\{CmbTelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
            string jelszón = "forgalmiutasítás";
            string szövegn;

            if (Kilépettjel.Checked)
                szövegn = "SELECT * FROM Dolgozóadatok ORDER BY DolgozóNév asc";
            else
                szövegn = $"SELECT * FROM Dolgozóadatok WHERE kilépésiidő=#01-01-1900# ORDER BY DolgozóNév asc";

            Kezelő_Dolgozó_Alap Kéz = new Kezelő_Dolgozó_Alap();
            List<Adat_Dolgozó_Alap> Adatok = Kéz.Lista_Adatok(helyn, jelszón, szövegn);

            foreach (Adat_Dolgozó_Alap rekord in Adatok)
                Dolgozónév.Items.Add(rekord.DolgozóNév.Trim() + " = " + rekord.Dolgozószám.Trim());

            Dolgozónév.EndUpdate();
        }


        private void Kilépettjel_CheckStateChanged(object sender, EventArgs e)
        {
            Névfeltöltés();
        }


        private void Munkahely()
        {
            Kezelő_Kiegészítő_Jelenlétiív Kéz = new Kezelő_Kiegészítő_Jelenlétiív();
            Adat_Kiegészítő_Jelenlétiív Rekord = Kéz.Lista_Adatok(CmbTelephely.Text.Trim()).Where(a => a.Id == 4).FirstOrDefault();
            if (Rekord != null)
                Texttelephely = Rekord.Szervezet.Trim();
        }


        private void Jogosultságkiosztás()
        {
            int melyikelem;
            // ide kell az összes gombot tenni amit szabályozni akarunk
            // Szabi gombok
            SzabNyilat.Enabled = false;
            SzabNyomtatás.Enabled = false;
            SzabLeadás.Enabled = false;
            Éves_Összesítő.Enabled = false;
            Szab_Rögzít.Enabled = false;
            // túlóra gombok
            Túl_Eng_Beáll.Enabled = false;
            EgyéniTúlNyom.Enabled = false;
            TúlCsopNyom.Enabled = false;
            // telephely szabadválasztás
            CmbTelephely.Enabled = false;

            // Szabadság
            melyikelem = 61;
            // módosítás 1
            if (MyF.Vanjoga(melyikelem, 1))
            {
                SzabNyomtatás.Enabled = true;
                SzabLeadás.Enabled = true;
            }
            // módosítás 2
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Éves_Összesítő.Enabled = true;
                Szab_Rögzít.Enabled = true;
            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
                SzabNyilat.Enabled = true;
            }

            // túlóra
            melyikelem = 62;
            // módosítás 1
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Túl_Eng_Beáll.Enabled = true;
            }
            // módosítás 2
            if (MyF.Vanjoga(melyikelem, 2))
            {
                EgyéniTúlNyom.Enabled = true;
                TúlCsopNyom.Enabled = true;
            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            { }

            // telephely választás
            melyikelem = 63;
            // módosítás 1
            if (MyF.Vanjoga(melyikelem, 1))
            {
                CmbTelephely.Enabled = true;
            }
            // módosítás 2
            if (MyF.Vanjoga(melyikelem, 2))
            { }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            { }
        }


        private void Tabfülek_DrawItem(object sender, DrawItemEventArgs e)
        {

            // Határozza meg, hogy melyik lap van jelenleg kiválasztva
            TabPage SelectedTab = TabFülek.TabPages[e.Index];

            // Szerezze be a lap fejlécének területét
            Rectangle HeaderRect = TabFülek.GetTabRect(e.Index);

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
                Font BoldFont = new Font(TabFülek.Font.Name, TabFülek.Font.Size, FontStyle.Bold);
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


        private void Évek_Feltöltése()
        {
            try
            {
                Adat_Évek.Items.Clear();
                string hely = $@"{Application.StartupPath}\{CmbTelephely.Text.Trim()}\adatok\Szatubecs";

                foreach (string file in System.IO.Directory.GetFiles(hely))
                {
                    string[] Darabol = file.Split('\\');

                    string Évek = Darabol[Darabol.Length - 1].Substring(0, 4);
                    Adat_Évek.Items.Add(Évek);
                }
                Adat_Évek.Text = DateTime.Today.Year.ToString();

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


        private void Adat_Évek_SelectedIndexChanged(object sender, EventArgs e)
        {
            hely = $@"{Application.StartupPath}\{CmbTelephely.Text.Trim()}\adatok\Szatubecs\{Adat_Évek.Text.Trim()}Szatubecs.mdb";
            Text = Adat_Évek.Text + " - Szabadság - Túlóra - Betegállomány -  AFT- Csúsztatás";
        }


        private void Fülekkitöltése()
        {
            switch (TabFülek.SelectedIndex)
            {
                case 0:
                    {
                        Szabadságkiírása(1);
                        break;
                    }
                case 1:
                    {
                        Szabadságokfeltölt();
                        Táblatörlése();
                        break;
                    }
                case 2:
                    {
                        Túlórakiírás(1);
                        break;
                    }
                case 3:
                    {
                        Táblatörlése();
                        break;
                    }
                case 4:
                    {
                        Betegkiírása(1);
                        break;
                    }
                case 5:
                    {
                        Táblatörlése();
                        break;
                    }
                case 6:
                    {
                        Csúsztatáskiírása(1);
                        break;
                    }
                case 7:
                    {
                        Táblatörlése();
                        break;
                    }
                case 8:
                    {
                        AFTkiírása(1);
                        break;
                    }
                case 9:
                    {

                        Táblatörlése();
                        break;
                    }

            }
        }


        private void TabFülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }


        private void Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\szatubecs.html";
                MyE.Megnyitás(hely);
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


        private void CmbTelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            Névfeltöltés();
        }


        private void BtnExcelkimenet_Click(object sender, EventArgs e)
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
                    FileName = $"SzaTubecs_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla, true);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás(fájlexc + ".xlsx");
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


        #region Általános
        private void Tábla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        { }

        private void Tábla_Formázás()
        {

            // cellák színezése
            switch (Textlista)
            {
                case "Szabadság":
                    {
                        for (int sor = 0; sor < Tábla.Rows.Count; sor++)
                        {
                            if (Tábla.Rows[sor].Cells[6].Value != null)
                            {
                                if (Tábla.Rows[sor].Cells[6].Value.ToString().ToUpper().Contains("ÉVKÖZI") != false || Tábla.Rows[sor].Cells[6].Value.ToString().ToUpper().Contains("PÓT") != false)
                                {
                                    for (int i = 5; i < 7; i++)
                                    {
                                        Tábla.Rows[sor].Cells[i].Style.BackColor = Color.DarkCyan;
                                        Tábla.Rows[sor].Cells[i].Style.ForeColor = Color.Red;
                                        Tábla.Rows[sor].Cells[i].Style.Font = new Font("Arial Narrow", 12f, FontStyle.Bold);
                                    }
                                }
                                if (Tábla.Rows[sor].Cells[6].Value.ToString().ToUpper().Contains("ALAP") != true)
                                {
                                    for (int i = 5; i < 7; i++)
                                    {
                                        Tábla.Rows[sor].Cells[i].Style.BackColor = Color.Cyan;
                                        Tábla.Rows[sor].Cells[i].Style.ForeColor = Color.Green;
                                        Tábla.Rows[sor].Cells[i].Style.Font = new Font("Arial Narrow", 12f, FontStyle.Italic);
                                    }
                                }
                            }


                            if ((Tábla.Rows[sor].Cells[7].Value) != null)
                            {
                                switch (Tábla.Rows[sor].Cells[7].Value)
                                {
                                    case "Nyomtatott":
                                        {
                                            Tábla.Rows[sor].Cells[7].Style.BackColor = Color.Blue;
                                            Tábla.Rows[sor].Cells[7].Style.ForeColor = Color.White;
                                            Tábla.Rows[sor].Cells[7].Style.Font = new Font("Arial Narrow", 12f, FontStyle.Italic);
                                            break;
                                        }
                                    case "Leadott":
                                        {
                                            Tábla.Rows[sor].Cells[7].Style.BackColor = Color.DarkGreen;
                                            Tábla.Rows[sor].Cells[7].Style.ForeColor = Color.White;
                                            Tábla.Rows[sor].Cells[7].Style.Font = new Font("Arial Narrow", 12f, FontStyle.Bold);
                                            break;
                                        }
                                    case "Törölt":
                                        {
                                            // egész sor színezése ha törölt
                                            for (int i = 0; i < 8; i++)
                                            {
                                                Tábla.Rows[sor].Cells[i].Style.BackColor = Color.IndianRed;
                                                Tábla.Rows[sor].Cells[i].Style.ForeColor = Color.White;
                                                Tábla.Rows[sor].Cells[i].Style.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
                                            }
                                            break;
                                        }
                                }
                            }


                        }

                        break;
                    }
                case "Beteg":
                    {
                        for (int sor = 0; sor < Tábla.Rows.Count; sor++)
                        {
                            if ((Tábla.Rows[sor].Cells[7].Value) != null)
                            {
                                switch (Tábla.Rows[sor].Cells[7].Value)
                                {
                                    case "Igény":
                                        {
                                            break;
                                        }

                                    case "Nyomtatott":
                                        {
                                            Tábla.Rows[sor].Cells[7].Style.BackColor = Color.Blue;
                                            Tábla.Rows[sor].Cells[7].Style.ForeColor = Color.White;
                                            Tábla.Rows[sor].Cells[7].Style.Font = new Font("Arial Narrow", 12f, FontStyle.Italic);
                                            break;
                                        }
                                    case "Leadott":
                                        {
                                            Tábla.Rows[sor].Cells[7].Style.BackColor = Color.DarkGreen;
                                            Tábla.Rows[sor].Cells[7].Style.ForeColor = Color.White;
                                            Tábla.Rows[sor].Cells[7].Style.Font = new Font("Arial Narrow", 12f, FontStyle.Bold);
                                            break;
                                        }
                                    case "Törölt":
                                        {
                                            for (int i = 0; i < 8; i++)
                                            {
                                                // egész sor színezése ha törölt
                                                Tábla.Rows[sor].Cells[i].Style.BackColor = Color.IndianRed;
                                                Tábla.Rows[sor].Cells[i].Style.ForeColor = Color.White;
                                                Tábla.Rows[sor].Cells[i].Style.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
                                            }
                                            break;
                                        }
                                }
                            }

                        }
                        break;

                    }

                default:
                    {

                        for (int sor = 0; sor < Tábla.Rows.Count; sor++)

                        {
                            if ((Tábla.Rows[sor].Cells[6].Value) != null)
                            {
                                switch (Tábla.Rows[sor].Cells[6].Value.ToString())
                                {
                                    case "Igény":
                                        {
                                            break;
                                        }

                                    case "Nyomtatott":
                                        {
                                            Tábla.Rows[sor].Cells[6].Style.BackColor = Color.Blue;
                                            Tábla.Rows[sor].Cells[6].Style.ForeColor = Color.White;
                                            Tábla.Rows[sor].Cells[6].Style.Font = new Font("Arial Narrow", 12f, FontStyle.Italic);
                                            break;
                                        }
                                    case "Leadott":
                                        {
                                            Tábla.Rows[sor].Cells[6].Style.BackColor = Color.DarkGreen;
                                            Tábla.Rows[sor].Cells[6].Style.ForeColor = Color.White;
                                            Tábla.Rows[sor].Cells[6].Style.Font = new Font("Arial Narrow", 12f, FontStyle.Bold);
                                            break;
                                        }
                                    case "Törölt":
                                        {
                                            // egész sor színezése ha törölt
                                            for (int i = 0; i < 7; i++)
                                            {
                                                Tábla.Rows[sor].Cells[i].Style.BackColor = Color.IndianRed;
                                                Tábla.Rows[sor].Cells[i].Style.ForeColor = Color.White;
                                                Tábla.Rows[sor].Cells[i].Style.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
                                            }
                                            break;
                                        }

                                }
                            }

                        }
                        break;

                    }

            }
        }


        private void Táblatörlése()
        {
            Tábla.Rows.Clear();
            Tábla.Columns.Clear();
            Tábla.Refresh();
        }

        #endregion


        #region Szabadság Gyűjtő
        private void SzabNyomtatás_Click(object sender, EventArgs e)
        {
            NyomtatásSzabi();
        }


        private void NyomtatásSzabi()
        {
            try
            {
                string fájlexcel = $@"{Application.StartupPath}\{CmbTelephely.Text.Trim()}\nyomtatvány\Szabadságlap.xlsx";
                if (!Exists(fájlexcel))
                    throw new HibásBevittAdat("Hiányzik az kitöltendő táblázat!");

                // 0 sorszámút ki kell jelölni.
                for (int i = 0; i < Tábla.Rows.Count; i++)
                    if (long.Parse(Tábla.Rows[i].Cells[0].Value.ToString()) == 0)
                        Tábla.Rows[i].Selected = false;


                if (Tábla.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy sor sem.");


                string munkalap = "Munka1";
                DateTime IdeigDátum;

                //Beolvassuk a táblázat adatait egy listába
                string szöveg = "SELECT * FROM szabadság WHERE Szabiok LIKE '%ivétel%' AND ";
                if (!Mind.Checked)// mind
                {
                    // Igényelt
                    if (Kért.Checked)
                        szöveg += " státus=0";
                    // nyomtatott
                    if (Nyomtatott.Checked)
                        szöveg += " státus=1";
                    // Rögzített
                    if (Rögzített.Checked)
                        szöveg += " státus=2";
                }
                szöveg += " ORDER BY kezdődátum";

                Kezelő_Szatube_Szabadság Kéz = new Kezelő_Szatube_Szabadság();
                List<Adat_Szatube_Szabadság> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                List<double> Sorszámok = new List<double>();
                // Beolvassuk egy listába azokat a sorszámokat amik ki vannak jelölve
                for (int i = 0; i < Tábla.SelectedRows.Count; i++)
                {
                    long utolsó = long.Parse(Tábla.SelectedRows[i].Cells[0].Value.ToString());
                    Sorszámok.Add(utolsó);
                }
                Sorszámok = Sorszámok.OrderBy(num => num).ToList();
                List<double> SzűrtLista = Sorszámok.Distinct().ToList();

                string utolsókivettnap;
                // excel tábla megnyitása
                MyE.ExcelMegnyitás(fájlexcel);
                int elem = 0;
                Holtart.Be();
                for (int i = 0; i < SzűrtLista.Count; i++)
                {
                    elem++;
                    List<Adat_Szatube_Szabadság> Szabadság = (from a in Adatok
                                                              where a.Sorszám == SzűrtLista[i]
                                                              select a).ToList();
                    double mostKivesz = Szabadság.Sum(szám => szám.Kivettnap);
                    Adat_Szatube_Szabadság Kezdet = Szabadság.First(a => a.Kezdődátum == Szabadság.Min(b => b.Kezdődátum));
                    Adat_Szatube_Szabadság Vége = Szabadság.First(a => a.Kezdődátum == Szabadság.Max(b => b.Kezdődátum));

                    utolsókivettnap = Tábla.SelectedRows[i].Cells[5].Value.ToString();
                    switch (elem)
                    {
                        case 1:
                            {
                                IdeigDátum = Kezdet.Kezdődátum;
                                MyE.Kiir(Kezdet.Sorszám + " /_" + Text.Substring(0, 4), "m1");
                                MyE.Kiir(Kezdet.Szabiok.Trim(), "f4");
                                MyE.Kiir(DateTime.Now.Year.ToString(), "i5");
                                MyE.Kiir(DateTime.Now.Month.ToString(), "k5");
                                MyE.Kiir(DateTime.Now.Day.ToString(), "m5");
                                MyE.Kiir(Kezdet.Dolgozónév.Trim(), "B9");
                                MyE.Kiir(Kezdet.Törzsszám.Trim(), "i9");
                                MyE.Kiir(Kezdet.Kezdődátum.ToString("yyyy.MM.dd"), "b27");
                                MyE.Kiir(Vége.Befejeződátum.ToString("yyyy.MM.dd"), "B30");
                                MyE.Kiir(mostKivesz.ToString(), "g27");
                                MyE.Kiir(IdeigDátum.Year.ToString(), "d17");
                                MyE.Kiir(Összesnapja(Kezdet.Törzsszám.Trim()).ToString(), "g17");
                                MyE.Kiir(IdeigDátum.Year.ToString(), "d21");
                                MyE.Kiir(Kivettnapja(Kezdet.Törzsszám.Trim(), IdeigDátum).ToString(), "g21");
                                MyE.Kiir(Texttelephely.Trim(), "B13");
                                break;
                            }
                        case 2:
                            {
                                IdeigDátum = Kezdet.Kezdődátum;
                                MyE.Kiir(Kezdet.Sorszám + " /_" + Text.Substring(0, 4), "ab1");
                                MyE.Kiir(Kezdet.Szabiok.Trim(), "u4");
                                MyE.Kiir(DateTime.Now.Year.ToString(), "x5");
                                MyE.Kiir(DateTime.Now.Month.ToString(), "z5");
                                MyE.Kiir(DateTime.Now.Day.ToString(), "ab5");
                                MyE.Kiir(Kezdet.Dolgozónév.Trim(), "q9");
                                MyE.Kiir(Kezdet.Törzsszám.Trim(), "x9");
                                MyE.Kiir(Kezdet.Kezdődátum.ToString("yyyy.MM.dd"), "q27");
                                MyE.Kiir(Vége.Befejeződátum.ToString("yyyy.MM.dd"), "q30");
                                MyE.Kiir(mostKivesz.ToString(), "v27");
                                MyE.Kiir(IdeigDátum.Year.ToString(), "s17");
                                MyE.Kiir(Összesnapja(Kezdet.Törzsszám.Trim()).ToString(), "v17");
                                MyE.Kiir(IdeigDátum.Year.ToString(), "s21");
                                MyE.Kiir(Kivettnapja(Kezdet.Törzsszám.Trim(), IdeigDátum).ToString(), "v21");
                                MyE.Kiir(Texttelephely.Trim(), "q13");
                                break;
                            }
                        case 3:
                            {
                                IdeigDátum = Kezdet.Kezdődátum;
                                MyE.Kiir(Kezdet.Sorszám + " /_" + Text.Substring(0, 4), "m33");
                                MyE.Kiir(Kezdet.Szabiok.Trim(), "f36");
                                MyE.Kiir(DateTime.Now.Year.ToString(), "i37");
                                MyE.Kiir(DateTime.Now.Month.ToString(), "k37");
                                MyE.Kiir(DateTime.Now.Day.ToString(), "m37");
                                MyE.Kiir(Kezdet.Dolgozónév.Trim(), "B41");
                                MyE.Kiir(Kezdet.Törzsszám.Trim(), "i41");
                                MyE.Kiir(Kezdet.Kezdődátum.ToString("yyyy.MM.dd"), "b59");
                                MyE.Kiir(Vége.Befejeződátum.ToString("yyyy.MM.dd"), "B62");
                                MyE.Kiir(mostKivesz.ToString(), "g59");
                                MyE.Kiir(IdeigDátum.Year.ToString(), "d49");
                                MyE.Kiir(Összesnapja(Kezdet.Törzsszám.Trim()).ToString(), "g49");
                                MyE.Kiir(IdeigDátum.Year.ToString(), "d53");
                                MyE.Kiir(Kivettnapja(Kezdet.Törzsszám.Trim(), IdeigDátum).ToString(), "g53");
                                MyE.Kiir(Texttelephely.Trim(), "B45");
                                break;
                            }
                        case 4:
                            {
                                IdeigDátum = Kezdet.Kezdődátum;
                                MyE.Kiir(Kezdet.Sorszám + " /_" + Text.Substring(0, 4), "ab33");
                                MyE.Kiir(Kezdet.Szabiok.Trim(), "u36");
                                MyE.Kiir(DateTime.Now.Year.ToString(), "x37");
                                MyE.Kiir(DateTime.Now.Month.ToString(), "z37");
                                MyE.Kiir(DateTime.Now.Day.ToString(), "ab37");
                                MyE.Kiir(Kezdet.Dolgozónév.Trim(), "q41");
                                MyE.Kiir(Kezdet.Törzsszám.Trim(), "x41");
                                MyE.Kiir(Kezdet.Kezdődátum.ToString("yyyy.MM.dd"), "q59");
                                MyE.Kiir(Vége.Befejeződátum.ToString("yyyy.MM.dd"), "q62");
                                MyE.Kiir(mostKivesz.ToString(), "v59");
                                MyE.Kiir(IdeigDátum.Year.ToString(), "s49");
                                MyE.Kiir(Összesnapja(Kezdet.Törzsszám.Trim()).ToString(), "v49");
                                MyE.Kiir(IdeigDátum.Year.ToString(), "s53");
                                MyE.Kiir(Kivettnapja(Kezdet.Törzsszám.Trim(), IdeigDátum).ToString(), "v53");
                                MyE.Kiir(Texttelephely.Trim(), "q45");
                                break;
                            }
                    }
                    // ha négy név van vagy ha a jelöltek számát elértük, akkor nyomtat majd a beírt adatokat törli
                    if (elem == 4)
                    {
                        MyE.Nyomtatás(munkalap, 1, 1);
                        Laptisztítás();
                        elem = 0;
                    }
                    Holtart.Lép();
                }
                if (elem != 0)
                {
                    MyE.Nyomtatás(munkalap, 1, 1);
                    Laptisztítás();
                }
                Laptisztítás();
                MyE.ExcelMentés();
                MyE.ExcelBezárás();

                // a státusokat átállítja
                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < SzűrtLista.Count; i++)
                {
                    if (SzűrtLista[i] != 0)
                    {
                        szöveg = $"Update  szabadság set státus=1 Where sorszám={SzűrtLista[i]} AND státus<>3";
                        SzövegGy.Add(szöveg);
                    }
                    Holtart.Lép();
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

                Kért.Checked = true;
                Szabadságkiírása(1);
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


        private void Laptisztítás()
        {
            MyE.Kiir("", "i5");
            MyE.Kiir("", "k5");
            MyE.Kiir("", "m5");
            MyE.Kiir("", "B9");
            MyE.Kiir("", "i9");
            MyE.Kiir("", "d17");
            MyE.Kiir("", "g17");
            MyE.Kiir("", "d21");
            MyE.Kiir("", "g21");
            MyE.Kiir("", "b27");
            MyE.Kiir("", "B30");
            MyE.Kiir("", "g27");
            MyE.Kiir("", "x5");
            MyE.Kiir("", "z5");
            MyE.Kiir("", "ab5");
            MyE.Kiir("", "q9");
            MyE.Kiir("", "x9");
            MyE.Kiir("", "s17");
            MyE.Kiir("", "v17");
            MyE.Kiir("", "s21");
            MyE.Kiir("", "v21");
            MyE.Kiir("", "q27");
            MyE.Kiir("", "q30");
            MyE.Kiir("", "v27");
            MyE.Kiir("", "i37");
            MyE.Kiir("", "k37");
            MyE.Kiir("", "m37");
            MyE.Kiir("", "B41");
            MyE.Kiir("", "i41");
            MyE.Kiir("", "d49");
            MyE.Kiir("", "g49");
            MyE.Kiir("", "d53");
            MyE.Kiir("", "g53");
            MyE.Kiir("", "b59");
            MyE.Kiir("", "B62");
            MyE.Kiir("", "g59");
            MyE.Kiir("", "x37");
            MyE.Kiir("", "z37");
            MyE.Kiir("", "ab37");
            MyE.Kiir("", "q41");
            MyE.Kiir("", "x41");
            MyE.Kiir("", "s49");
            MyE.Kiir("", "v49");
            MyE.Kiir("", "s53");
            MyE.Kiir("", "v53");
            MyE.Kiir("", "q59");
            MyE.Kiir("", "q62");
            MyE.Kiir("", "v59");
            MyE.Kiir("", "m1");
            MyE.Kiir("", "ab1");
            MyE.Kiir("", "m33");
            MyE.Kiir("", "ab33");
            MyE.Kiir("", "f4");
            MyE.Kiir("", "u4");
            MyE.Kiir("", "f36");
            MyE.Kiir("", "u36");

            MyE.Kiir("", "b13");
            MyE.Kiir("", "q13");
            MyE.Kiir("", "b45");
            MyE.Kiir("", "q45");
        }


        private void BtnÖsszSzabiLista_Click(object sender, EventArgs e)
        {
            Szabadságkiírása(1);
        }


        private void Szabadságkiírása(int csoport)
        {
            try
            {
                Textlista = "Szabadság";
                Táblatörlése();
                double összes = 0d;
                Tábla.ColumnCount = 8;
                Tábla.RowCount = 0;

                Tábla.Columns[0].HeaderText = "Sorszám";
                Tábla.Columns[0].Width = 70;
                Tábla.Columns[1].HeaderText = "HR azonosító";
                Tábla.Columns[1].Width = 120;
                Tábla.Columns[2].HeaderText = "Dolgozó név";
                Tábla.Columns[2].Width = 250;
                Tábla.Columns[3].HeaderText = "Szab. kezdete";
                Tábla.Columns[3].Width = 150;
                Tábla.Columns[4].HeaderText = "Szab. vége";
                Tábla.Columns[4].Width = 120;
                Tábla.Columns[5].HeaderText = "Napok száma";
                Tábla.Columns[5].Width = 120;
                Tábla.Columns[6].HeaderText = "Kivétel oka";
                Tábla.Columns[6].Width = 300;
                Tábla.Columns[7].HeaderText = "Státusz";
                Tábla.Columns[7].Width = 120;

                if (!Exists(hely)) throw new HibásBevittAdat("Ebben az évben nem lett létrehozva adatbázis.");
                if (csoport == 0 && Dolgozónév.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");

                string[] darabol = Dolgozónév.Text.Trim().Split('=');

                SzabadságListaFeltöltés();

                List<Adat_Szatube_Szabadság> Adatok = new List<Adat_Szatube_Szabadság>();

                if (csoport == 0)
                    Adatok = (from a in Adatok_Szabadság
                              where a.Törzsszám == darabol[1].Trim()
                              && a.Státus != 3
                              orderby a.Kezdődátum
                              select a).ToList();

                else if (!Mind.Checked)// mind
                {
                    // Igényelt
                    if (Kért.Checked)
                        Adatok = (from a in Adatok_Szabadság
                                  where a.Státus == 0
                                  orderby a.Kezdődátum
                                  select a).ToList();
                    // nyomtatott
                    if (Nyomtatott.Checked)
                        Adatok = (from a in Adatok_Szabadság
                                  where a.Státus == 1
                                  orderby a.Kezdődátum
                                  select a).ToList();
                    // Rögzített
                    if (Rögzített.Checked)
                        Adatok = (from a in Adatok_Szabadság
                                  where a.Státus == 2
                                  orderby a.Kezdődátum
                                  select a).ToList();
                }
                else
                {
                    Adatok = (from a in Adatok_Szabadság
                              orderby a.Kezdődátum
                              select a).ToList();
                }

                Tábla.Visible = false;
                int i;
                foreach (Adat_Szatube_Szabadság rekord in Adatok)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Sorszám;
                    Tábla.Rows[i].Cells[1].Value = rekord.Törzsszám.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Dolgozónév.Trim();
                    Tábla.Rows[i].Cells[5].Value = rekord.Kivettnap;
                    Tábla.Rows[i].Cells[6].Value = rekord.Szabiok.Trim();

                    if (rekord.Szabiok.Trim().ToUpper().Contains("ALAP") || rekord.Szabiok.Trim().ToUpper().Contains("PÓT") || rekord.Szabiok.Trim().ToUpper().Contains("ÉVKÖZI"))
                    {
                    }
                    else
                    {
                        Tábla.Rows[i].Cells[3].Value = rekord.Kezdődátum.ToString("yyyy.MM.dd");
                        Tábla.Rows[i].Cells[4].Value = rekord.Befejeződátum.ToString("yyyy.MM.dd");
                    }
                    Tábla.Rows[i].Cells[7].Value = SzabStátus(rekord.Státus);

                    if (csoport == 0)
                    {
                        if (rekord.Szabiok.ToUpper().Contains("KIVÉTEL"))
                            összes -= rekord.Kivettnap;
                        else
                            összes += rekord.Kivettnap;
                    }
                }
                if (csoport == 0)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[2].Value = "Szabadság összesen:";
                    Tábla.Rows[i].Cells[5].Value = összes;
                }
                Tábla.Refresh();

                Tábla_Formázás();

                Tábla.Visible = true;
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


        string SzabStátus(int státus)
        {

            switch (státus)
            {
                case 0:
                    {
                        return "Igény";

                    }
                case 1:
                    {
                        return "Nyomtatott";

                    }
                case 2:
                    {
                        return "Leadott";

                    }
                case 3:
                    {
                        return "Törölt";

                    }
            }
            return "";
        }


        private void SzabLeadás_Click(object sender, EventArgs e)
        {
            try
            {
                if (Textlista != "Szabadság")
                    throw new HibásBevittAdat("A táblázat tartalma nem Szabadság adat.");

                if (Tábla.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy sor sem!");

                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < Tábla.SelectedRows.Count; i++)
                {
                    if (!int.TryParse(Tábla.SelectedRows[i].Cells[0].Value.ToString(), out int sorszám))
                        throw new HibásBevittAdat("Nincs érvényes sor kiválasztva.");

                    if (sorszám != 0)
                    {
                        string szöveg = $"Update  szabadság set státus=2 Where sorszám={sorszám} AND státus=1";
                        SzövegGy.Add(szöveg);
                    }
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

                Nyomtatott.Checked = true;
                Szabadságkiírása(1);
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


        private int Kivettnapja(string törzsszám, DateTime dátum)
        {
            int válasz = 0;
            string szöveg = $"SELECT * FROM szabadság Where törzsszám='{törzsszám.Trim()}' AND Kezdődátum<#{dátum:yyyy-MM-dd}# Order by Kezdődátum asc";

            Kezelő_Szatube_Szabadság Kéz = new Kezelő_Szatube_Szabadság();
            List<Adat_Szatube_Szabadság> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

            foreach (Adat_Szatube_Szabadság rekord in Adatok)
            {
                if (rekord.Szabiok.ToUpper().Contains("KIVÉTEL") && rekord.Státus != 3)
                    válasz += rekord.Kivettnap;
            }
            return válasz;
        }


        int Összesnapja(string törzsszám)
        {
            int válasz = 0;
            try
            {

                string szöveg = $"SELECT * FROM szabadság Where törzsszám='{törzsszám.Trim()}'  Order by Kezdődátum asc";
                Kezelő_Szatube_Szabadság Kéz = new Kezelő_Szatube_Szabadság();
                List<Adat_Szatube_Szabadság> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Szatube_Szabadság rekord in Adatok)
                {
                    if (rekord.Szabiok.Trim() == "Alap")
                        válasz += rekord.Kivettnap;
                    // 3 a törölt szabadság
                    if (rekord.Szabiok.ToUpper().Contains("PÓT") && rekord.Státus != 3)
                        válasz += rekord.Kivettnap;
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
            return válasz;
        }


        private void Mind_Click(object sender, EventArgs e)
        {
            SzabLeadás.Visible = false;
            SzabNyomtatás.Visible = false;
            Szabadságkiírása(1);
        }


        private void Kért_Click(object sender, EventArgs e)
        {
            SzabNyomtatás.Visible = true;
            SzabLeadás.Visible = false;
            Szabadságkiírása(1);
        }


        private void Nyomtatott_Click(object sender, EventArgs e)
        {
            SzabNyomtatás.Visible = true;
            SzabLeadás.Visible = true;
            Szabadságkiírása(1);
        }


        private void Rögzített_Click(object sender, EventArgs e)
        {
            SzabNyomtatás.Visible = true;
            SzabLeadás.Visible = false;
            Szabadságkiírása(1);
        }
        #endregion


        #region Szabadság Egyéni
        private void Szabi_Egyéni_Listáz_Click(object sender, EventArgs e)
        {
            Szabadságkiírása(0);
        }


        private void Szab_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Szabiok.Text.Trim() == "") throw new HibásBevittAdat("Nincs megadva a szabadság módosítás szöveges eleme.");
                if (Dolgozónév.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");
                if (!int.TryParse(Szabipótnap.Text, out int Nap)) throw new HibásBevittAdat("A szabadság napnak egész számnak kell lennie.");

                SzabadságListaFeltöltés();

                double id = 1;
                if (Adatok_Szabadság.Count > 0) id = Adatok_Szabadság.Max(a => a.Sorszám) + 1;

                string[] darabol = Dolgozónév.Text.Split('=');

                string szöveg = "INSERT INTO szabadság ";
                szöveg += " (Sorszám ,törzsszám, dolgozónév, kezdődátum, befejeződátum, kivettnap, Szabiok, Státus, rögzítette, rögzítésdátum )";
                szöveg += " VALUES (";
                szöveg += $"{id},";                         //Sorszám
                szöveg += $"'{darabol[1].Trim()}', ";       //törzsszám
                szöveg += $"'{darabol[0].Trim()}', ";       //dolgozónév
                szöveg += "'1900.01.01', ";                  //kezdődátum
                szöveg += "'1900.01.01', ";                  //befejeződátum
                szöveg += $"{Nap}, ";                       //kivettnap
                szöveg += $"'{Szabiok.Text.Trim()}', ";      //Szabiok
                szöveg += "2, ";                             //Státus
                szöveg += $"'{Program.PostásNév.Trim()}', "; //rögzítette
                szöveg += $"'{DateTime.Now}')";  //rögzítésdátum
                MyA.ABMódosítás(hely, jelszó, szöveg);
                MessageBox.Show("Az adatok rögzítésre kerültek.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Szabadságkiírása(0);
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


        private void Éves_Összesítő_Click(object sender, EventArgs e)
        {
            try
            {

                if (Dolgozónév.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");

                string[] darabol = Dolgozónév.Text.Trim().Split('=');


                string fájlexc;
                Holtart.Be();


                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Dolgozó éves szabadság felhasználása ",
                    FileName = "Éves_" + Dolgozónév.Text.Trim() + "-" + DateTime.Now.ToString("yyyyMMdd"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, (fájlexc).Length - 5);
                string munkalap = "Munka1";
                MyE.ExcelLétrehozás();

                // elkészítjük a fejlécet
                MyE.Oszlopszélesség(munkalap, "A:A", 35);
                MyE.Oszlopszélesség(munkalap, "B:D", 12);
                MyE.Oszlopszélesség(munkalap, "e:e", 18);
                // dolgozó törzszáma

                MyE.Egyesít(munkalap, "b1:d1");
                MyE.Kiir("Szabadság Összesítő a " + Text.Substring(0, 4) + " évre", "b1");
                MyE.Betű("b1");


                MyE.Kiir("Név:", "a3");
                MyE.Egyesít(munkalap, "b3:e3");
                MyE.Kiir(darabol[0].Trim(), "b3");
                MyE.Betű("b3");

                MyE.Kiir("Azonosító:", "a5");
                MyE.Egyesít(munkalap, "b5:e5");
                MyE.Kiir(darabol[1].Trim(), "b5");
                MyE.Betű("b5");

                MyE.Egyesít(munkalap, "a9:b9");
                MyE.Kiir("Felhasználható szabadságok", "a9");
                MyE.Betű("a9");
                MyE.Kiir("Jogcím", "a10");
                MyE.Betű("a10");
                MyE.Kiir("Nap", "b10");
                MyE.Betű("b10");

                int sor = 11;
                int összesen = 0;

                string szöveg = "SELECT * FROM szabadság ";
                szöveg += $" WHERE Törzsszám='{darabol[1].Trim()}'  AND státus<>3 AND (szabiok Like '%pót%' OR szabiok='Alap')";
                szöveg += " order by kezdődátum";

                Kezelő_Szatube_Szabadság Kéz = new Kezelő_Szatube_Szabadság();
                List<Adat_Szatube_Szabadság> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);


                foreach (Adat_Szatube_Szabadság rekord in Adatok)
                {
                    // ha nincs dátum akkor jogcím
                    MyE.Kiir(rekord.Szabiok.Trim(), "a" + sor);
                    MyE.Kiir(rekord.Kivettnap.ToString(), "b" + sor);
                    összesen += rekord.Kivettnap;
                    sor += 1;
                    Holtart.Lép();
                }
                MyE.Kiir("Összesen:", "a" + sor);
                MyE.Betű("A" + sor, false, true, true);
                MyE.Kiir(összesen.ToString(), "b" + sor);
                MyE.Betű("B" + sor, false, true, true);

                MyE.Rácsoz("a9:b" + sor);
                MyE.Vastagkeret("a9:b" + sor);
                MyE.Vastagkeret("a10:b" + sor);
                MyE.Vastagkeret("a" + sor + ":b" + sor);
                sor += 3;
                int eleje = sor;
                MyE.Egyesít(munkalap, "a" + sor + ":e" + sor);
                MyE.Kiir("Szabadság felhasználás", "a" + sor);
                MyE.Betű("a" + sor);
                sor += 1;
                MyE.Kiir("Sorszám", "a" + sor);
                MyE.Kiir("Kezdete", "b" + sor);
                MyE.Kiir("Vége", "c" + sor);
                MyE.Kiir("Kivett nap", "d" + sor);
                MyE.Kiir("Kivétel oka", "e" + sor);
                MyE.Betű("a" + sor + ":e" + sor);
                sor += 1;

                int kivett = 0;

                szöveg = "SELECT * FROM szabadság ";
                szöveg += $" WHERE Törzsszám='{darabol[1].Trim()}'  AND státus<>3 AND szabiok Like '%ivétel%'";
                szöveg += " order by kezdődátum";
                Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Szatube_Szabadság rekord in Adatok)
                {
                    // ha nincs dátum akkor jogcím
                    MyE.Kiir(rekord.Sorszám.ToString(), "a" + sor);
                    MyE.Kiir(rekord.Kezdődátum.ToString("yyyy.MM.dd"), "b" + sor);
                    MyE.Kiir(rekord.Befejeződátum.ToString("yyyy.MM.dd"), "c" + sor);
                    MyE.Kiir(rekord.Kivettnap.ToString(), "d" + sor);
                    MyE.Kiir(rekord.Szabiok.Trim(), "e" + sor);
                    kivett += rekord.Kivettnap;
                    sor += 1;
                    Holtart.Lép();
                }

                MyE.Kiir("Összesen:", "a" + sor);
                MyE.Betű("A" + sor, false, true, true);
                MyE.Kiir(kivett.ToString(), "d" + sor);
                MyE.Betű("D" + sor, false, true, true);
                MyE.Rácsoz("a" + eleje.ToString() + ":e" + sor);
                MyE.Vastagkeret("a" + eleje.ToString() + ":e" + sor);
                MyE.Vastagkeret("a" + eleje.ToString() + ":e" + (eleje + 1).ToString());
                MyE.Vastagkeret("a" + sor + ":e" + sor);
                sor += 2;
                MyE.Kiir("A " + Text.Substring(0, 4) + " évről marad:", "a" + sor);
                MyE.Kiir((összesen - kivett).ToString(), "d" + sor);
                MyE.Betű(sor + ":" + sor, false, true, true);

                MyE.NyomtatásiTerület_részletes(munkalap, "a1:e" + sor, "", "", true);
                MyE.Aktív_Cella(munkalap, "A1");

                MyE.ExcelMentés(fájlexc + ".xlsx");
                MyE.ExcelBezárás();
                Holtart.Ki();

                MessageBox.Show("Elkészült az Excel tábla.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

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


        private void SzabNyilat_Click(object sender, EventArgs e)
        {
            try
            {

                if (Dolgozónév.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");

                string fájlexcel = $@"{Application.StartupPath}\" + CmbTelephely.Text.ToString() + @"\nyomtatvány\Szabadságkivétel_egylapos.xlsx";
                if (!Exists(fájlexcel))
                    throw new HibásBevittAdat("Hiányzik az kitöltendő táblázat!");

                Holtart.Be();
                MyE.ExcelMegnyitás(fájlexcel);
                string munkalap = "Munka1";
                string[] darabol = Dolgozónév.Text.Split('=');

                Holtart.Lép();

                List<Adat_Dolgozó_Személyes> Adatok = KézSzemélyes.Lista_Adatok();
                Adat_Dolgozó_Személyes Rekord = (from a in Adatok
                                                 where a.Dolgozószám == darabol[1].Trim()
                                                 select a).FirstOrDefault();
                if (Rekord != null)
                {
                    MyE.Kiir(Text.Substring(0, 4), "aa1");
                    MyE.Kiir(darabol[0].Trim(), "aa2");
                    MyE.Kiir(Rekord.Leánykori.Trim(), "aa3");
                    MyE.Kiir(Rekord.Születésihely.Trim(), "aa4");
                    MyE.Kiir(Rekord.Születésiidő.Year.ToString(), "aa5");
                    MyE.Kiir(Rekord.Születésiidő.Month.ToString(), "aa6");
                    MyE.Kiir(Rekord.Születésiidő.Day.ToString(), "aa7");
                    MyE.Kiir(Rekord.Anyja.ToString(), "aa8");
                    MyE.Kiir(Texttelephely.Trim(), "aa9");
                    MyE.Kiir(darabol[1].Trim(), "aa10");
                }
                MyE.Nyomtatás(munkalap, 1, 1);
                for (int i = 1; i < 11; i++)
                    MyE.Kiir("", "AA" + i);

                MyE.ExcelMentés();
                MyE.ExcelBezárás();
                Holtart.Ki();
                MessageBox.Show("Elkészült az Excel tábla.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void Szabadságokfeltölt()
        {
            try
            {
                string Hely = $@"{Application.StartupPath}\{CmbTelephely.Text.Trim()}\Adatok\Segéd\kiegészítő.mdb";
                string Jelszó = "Mocó";
                string szöveg = "SELECT * FROM szabadságok WHERE NOT megnevezés like '%kivétel%' order by megnevezés asc";
                Szabiok.Items.Clear();
                Szabiok.BeginUpdate();
                Kezelő_Kiegészítő_Szabadságok Kéz = new Kezelő_Kiegészítő_Szabadságok();
                List<Adat_Kiegészítő_Szabadságok> Adatok = Kéz.Lista_Adatok(Hely, Jelszó, szöveg);

                foreach (Adat_Kiegészítő_Szabadságok rekord in Adatok)
                    Szabiok.Items.Add(rekord.Megnevezés.Trim());

                Szabiok.EndUpdate();
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


        #region Túlóra összes
        private void Túlóramind_Click(object sender, EventArgs e)
        {
            Túlórakiírás(1);
            EgyéniTúlNyom.Visible = false;
            TúlCsopNyom.Visible = false;
            Túl_Eng_Beáll.Visible = false;
        }


        private void Túlóraigényelt_Click(object sender, EventArgs e)
        {
            Túlórakiírás(1);
            Túl_Eng_Beáll.Visible = false;
            EgyéniTúlNyom.Visible = true;
            TúlCsopNyom.Visible = true;
        }


        private void Túlóranyomtatott_Click(object sender, EventArgs e)
        {
            Túlórakiírás(1);
            EgyéniTúlNyom.Visible = true;
            TúlCsopNyom.Visible = true;
            Túl_Eng_Beáll.Visible = true;
        }


        private void Túlórarögzített_Click(object sender, EventArgs e)
        {
            Túlórakiírás(1);
            Túl_Eng_Beáll.Visible = false;
            EgyéniTúlNyom.Visible = true;
            TúlCsopNyom.Visible = true;
        }


        private void BtnTúlóraÖsszlekérd_Click(object sender, EventArgs e)
        {
            Túlórakiírás(1);
        }


        private void Túl_Eng_Beáll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy sor sem.");

                // a státusokat átállítja
                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < Tábla.SelectedRows.Count; i++)
                {
                    string szöveg = "Update  túlóra set státus=2 Where sorszám=" + Tábla.SelectedRows[i].Cells[0].Value.ToString();
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

                Túlóranyomtatott.Checked = true;
                Túlórakiírás(1);
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


        private void EgyéniTúlNyom_Click(object sender, EventArgs e)
        {
            Egyéninyomtatás_más();
        }


        private void TúlCsopNyom_Click(object sender, EventArgs e)
        {
            try
            {

                if (Tábla.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy érvényes sor sem.");
                Holtart.Be();

                string fájlexc = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Túlóra_{Program.PostásTelephely.Trim()}_{DateTime.Now:yyyyMMddhhmmss}";

                MyE.ExcelLétrehozás();
                // ************************
                // excel tábla érdemi része
                // ************************
                string munkalap = "Munka1";
                MyE.Munkalap_betű("Calibri", 12);

                MyE.Oszlopszélesség(munkalap, "A:A", 4);
                MyE.Oszlopszélesség(munkalap, "b:b", 30);
                MyE.Oszlopszélesség(munkalap, "c:c", 10);
                MyE.Oszlopszélesség(munkalap, "d:d", 30);
                MyE.Oszlopszélesség(munkalap, "e:f", 17);
                MyE.Oszlopszélesség(munkalap, "g:g", 10);
                MyE.Oszlopszélesség(munkalap, "h:h", 25);
                MyE.Oszlopszélesség(munkalap, "i:i", 30);
                MyE.Oszlopszélesség(munkalap, "j:j", 20);
                MyE.Oszlopszélesség(munkalap, "k:k", 25);
                // kiírjuk a szervezeteket
                MyE.Egyesít(munkalap, "i1:k1");
                MyE.Egyesít(munkalap, "i2:k2");
                MyE.Egyesít(munkalap, "i3:k3");
                string helym = $@"{Application.StartupPath}\{CmbTelephely.Text.Trim()}\adatok\segéd\kiegészítő.mdb";
                string jelszóm = "Mocó";
                string szöveg = "Select * FROM jelenlétiív where id>1 ORDER BY id";
                Kezelő_Kiegészítő_Jelenlétiív Kéz = new Kezelő_Kiegészítő_Jelenlétiív();
                List<Adat_Kiegészítő_Jelenlétiív> Adatok = Kéz.Lista_Adatok(helym, jelszóm, szöveg);

                szöveg = "SELECT * FROM főkönyvtábla";
                Kezelő_Kiegészítő_főkönyvtábla KézFő = new Kezelő_Kiegészítő_főkönyvtábla();
                List<Adat_Kiegészítő_főkönyvtábla> FőkönyAdatok = KézFő.Lista_Adatok(helym, jelszóm, szöveg);

                int P = 0;
                foreach (Adat_Kiegészítő_Jelenlétiív rekord in Adatok)
                {
                    if (P == 2) MyE.Kiir(rekord.Szervezet.Trim(), "i1");
                    if (P == 3) MyE.Kiir(rekord.Szervezet.Trim(), "i2");
                    if (P == 4) MyE.Kiir(rekord.Szervezet.Trim(), "i3");
                    P++;
                }


                helym = $@"{Application.StartupPath}\{CmbTelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
                jelszóm = "forgalmiutasítás";
                szöveg = "SELECT * FROM dolgozóadatok";
                Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> DolgAdatok = KézDolg.Lista_Adatok(helym, jelszóm, szöveg);


                // vastag vonal
                MyE.VastagFelső("A5:K5");


                // logó beszúrása
                MyE.Kép_beillesztés(munkalap, "A1", Application.StartupPath + @"\Főmérnökség\adatok\BKV.png", 5, 5, 40, 100);

                MyE.Egyesít(munkalap, "a7:k7");
                MyE.Kiir("Rendkívüli munka elrendelő lap (csoportos)", "a7");
                // fejléc elkészítése
                MyE.Sortörésseltöbbsorba("9:9");
                MyE.Kiir("S.sz.", "A9");
                MyE.Kiir("Név", "B9");
                MyE.Kiir("Azonosító", "C9");
                MyE.Kiir("Munkakör", "D9");
                MyE.Kiir("A rendkívüli\nmunkavégzés \nidőpontja \n(dátum, óra)", "E9");
                MyE.Kiir("A rendkívüli \nmunkavégzés vége \n(dátum, óra)", "F9");
                MyE.Kiir("Időtartam\n(órában)", "G9");
                MyE.Kiir("Rendkívüli munka fajtája", "H9");
                MyE.Kiir("Az elvégzendő munka leírása, indoka.", "I9");
                MyE.Kiir("Megváltás módja", "J9");
                MyE.Kiir("Munkavállaló " + '\n' + "aláírása", "K9");
                MyE.Betű("a9:k9");
                MyE.Rácsoz("a9:k9");
                MyE.Vastagkeret("a9:k9");
                int sor = 10;

                DateTime eleje;
                DateTime vége;



                for (int i = 0; i < Tábla.Rows.Count; i++)
                {
                    Holtart.Lép();
                    if (Tábla.Rows[i].Selected)
                    {
                        // sor formázása
                        MyE.Sormagasság($"{sor}:{sor}", 45);

                        // adatok kiírsa
                        MyE.Kiir(Tábla.Rows[i].Cells[0].Value.ToStrTrim(), "a" + sor);
                        MyE.Igazít_vízszintes($"A{sor}", "bal");
                        MyE.Kiir(Tábla.Rows[i].Cells[2].Value.ToStrTrim(), "b" + sor);
                        MyE.Kiir(Tábla.Rows[i].Cells[1].Value.ToStrTrim(), "c" + sor);
                        MyE.Igazít_vízszintes($"C{sor}", "bal");
                        string Munkakör = (from a in DolgAdatok
                                           where a.Dolgozószám.Trim() == Tábla.Rows[i].Cells[1].Value.ToStrTrim()
                                           select a.Munkakör).FirstOrDefault();

                        if (Munkakör != null) MyE.Kiir(Munkakör, "d" + sor);
                        MyE.Sortörésseltöbbsorba($"d{sor}");

                        eleje = DateTime.Parse(Tábla.Rows[i].Cells[7].Value.ToStrTrim());
                        string válasz = Tábla.Rows[i].Cells[3].Value.ToStrTrim() + " " + Tábla.Rows[i].Cells[7].Value.ToStrTrim();
                        MyE.Kiir(válasz, "e" + sor);
                        vége = DateTime.Parse(Tábla.Rows[i].Cells[8].Value.ToStrTrim());
                        if (eleje < vége)
                        {
                            // nappal
                            válasz = Tábla.Rows[i].Cells[3].Value.ToStrTrim() + " " + Tábla.Rows[i].Cells[8].Value.ToStrTrim();
                            MyE.Kiir(válasz, "f" + sor);
                        }
                        else
                        {
                            // éjszaka
                            válasz = Tábla.Rows[i].Cells[3].Value.ToStrTrim() + " " + Tábla.Rows[i].Cells[8].Value.ToStrTrim();
                            MyE.Kiir(válasz, "f" + sor);
                        }


                        MyE.Kiir("=" + Tábla.Rows[i].Cells[4].Value.ToStrTrim() + "/60", "g" + sor);
                        MyE.Betű("g" + sor, "", "0.00");
                        MyE.Igazít_vízszintes($"G{sor}", "bal");

                        válasz = Tábla.Rows[i].Cells[5].Value.ToStrTrim();
                        MyE.Sortörésseltöbbsorba($"i{sor}");
                        if (válasz.Contains("&T"))
                        {
                            válasz = válasz.Substring(2, válasz.Length - 2).Trim();
                            MyE.Kiir(válasz, "i" + sor);
                            MyE.Kiir("50% bérpótlék", "j" + sor);
                            MyE.Kiir("Túlóra", "h" + sor);
                        }
                        else if (válasz.Contains("&EB"))
                        {
                            válasz = válasz.Substring(3, válasz.Length - 3).Trim();
                            MyE.Kiir(válasz, "i" + sor);
                            MyE.Kiir("100% bérpótlék", "j" + sor);
                            MyE.Kiir("Elvont pihenő", "h" + sor);
                        }
                        else if (válasz.Contains("&EP"))
                        {
                            válasz = válasz.Substring(3, válasz.Length - 3).Trim();
                            MyE.Kiir(válasz, "i" + sor);
                            MyE.Kiir("100% bérpótlék", "j" + sor);
                            MyE.Kiir("Elvont pihenő", "h" + sor);
                        }
                        else if (válasz.Contains("&V"))
                        {
                            válasz = válasz.Substring(2, válasz.Length - 2).Trim();
                            MyE.Kiir(válasz, "i" + sor);
                            MyE.Kiir("50% bérpótlék", "j" + sor);
                            MyE.Kiir("visszaadott pihenő", "h" + sor);
                        }
                        sor++;
                    }
                }
                MyE.Rácsoz("a10:k" + (sor - 1).ToString());
                MyE.Vastagkeret("a10:k" + (sor - 1).ToString());

                // dátum
                sor += 1;
                MyE.Kiir("Dátum: " + DateTime.Now.ToString("yyyy.MM.dd"), "a" + sor);
                sor += 1;
                MyE.Sormagasság(sor + ":" + sor, 45);
                sor += 1;
                MyE.Egyesít(munkalap, "a" + sor + ":b" + sor);
                MyE.Egyesít(munkalap, "d" + sor + ":e" + sor);
                MyE.Egyesít(munkalap, "g" + sor + ":h" + sor);
                MyE.Egyesít(munkalap, "j" + sor + ":k" + sor);
                MyE.Kiir("Kiállította, ellenőrizte:", "a" + sor);
                MyE.Kiir("Túlórát elrendelte:", "d" + sor);
                MyE.Kiir("Túlmunka végrehajtását igazolja:", "g" + sor);
                MyE.Kiir("Túlmunka végzés kifiztetését engedélyezte:", "j" + sor);
                MyE.Aláírásvonal("a" + sor + ":b" + sor);
                MyE.Aláírásvonal("d" + sor + ":e" + sor);
                MyE.Aláírásvonal("g" + sor + ":h" + sor);
                MyE.Aláírásvonal("j" + sor + ":k" + sor);
                sor += 1;
                MyE.Egyesít(munkalap, "a" + sor + ":b" + sor);
                MyE.Egyesít(munkalap, "d" + sor + ":e" + sor);
                MyE.Egyesít(munkalap, "g" + sor + ":h" + sor);
                MyE.Egyesít(munkalap, "j" + sor + ":k" + sor);


                string Név = (from a in FőkönyAdatok
                              where a.Id == 2
                              select a.Név).FirstOrDefault();

                // aláíró név

                MyE.Kiir(Név, "d" + sor);
                MyE.Kiir(Név, "g" + sor);

                Név = (from a in FőkönyAdatok
                       where a.Id == 3
                       select a.Név).FirstOrDefault();
                MyE.Kiir(Név, "J" + sor);


                // beosztás
                sor += 1;
                MyE.Egyesít(munkalap, "a" + sor + ":b" + sor);
                MyE.Egyesít(munkalap, "d" + sor + ":e" + sor);
                MyE.Egyesít(munkalap, "g" + sor + ":h" + sor);
                MyE.Egyesít(munkalap, "j" + sor + ":k" + sor);
                string Beosztás = (from a in FőkönyAdatok
                                   where a.Id == 2
                                   select a.Beosztás).FirstOrDefault();

                MyE.Kiir(Beosztás, "d" + sor);
                MyE.Kiir(Beosztás, "g" + sor);
                Beosztás = (from a in FőkönyAdatok
                            where a.Id == 3
                            select a.Beosztás).FirstOrDefault();

                MyE.Kiir(Beosztás, "j" + sor);

                // ****************************
                // excel tábla érdemi rész vége
                // ****************************
                MyE.NyomtatásiTerület_részletes(munkalap, "a1:k" + sor, "", "", false);
                MyE.Nyomtatás(munkalap, 1, 1);


                Holtart.Ki();
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc + ".xlsx");
                MyE.ExcelBezárás();

                if (!CheckBox2.Checked)
                {
                    Delete(fájlexc + ".xlsx");
                }
                // a státusokat átállítja

                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < Tábla.SelectedRows.Count; i++)
                {
                    szöveg = "Update  túlóra set státus=1 Where sorszám=" + Tábla.SelectedRows[i].Cells[0].Value.ToStrTrim();
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

                Túlórakiírás(1);
                MessageBox.Show("A kijelölt tételek nyomtatása megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void Egyéninyomtatás_más()
        {

            try
            {

                if (Tábla.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kiválasztva érvényes sor a táblázatban.");
                Holtart.Be();

                string fájlexc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + "Túlóra_egyéni_" + Program.PostásNév.Trim() + "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
                string válasz;
                string munkalap = "Munka1";
                DateTime eleje;
                DateTime vége;

                MyE.ExcelLétrehozás();

                // ************************
                // excel tábla érdemi része
                // ************************
                MyE.Munkalap_betű("Calibri", 10);

                MyE.Oszlopszélesség(munkalap, "A:A", 19);
                MyE.Oszlopszélesség(munkalap, "b:g", 7);
                MyE.Oszlopszélesség(munkalap, "h:i", 1);
                MyE.Oszlopszélesség(munkalap, "j:j", 19);
                MyE.Oszlopszélesség(munkalap, "k:p", 7);

                // kiírjuk a szervezeteket
                MyE.Egyesít(munkalap, "d1:g1");
                MyE.Egyesít(munkalap, "m1:p1");
                MyE.Egyesít(munkalap, "d2:g2");
                MyE.Egyesít(munkalap, "m2:p2");
                MyE.Egyesít(munkalap, "d3:g3");
                MyE.Egyesít(munkalap, "m3:p3");

                string helym = $@"{Application.StartupPath}\{CmbTelephely.Text.Trim()}\adatok\segéd\kiegészítő.mdb";
                string jelszóm = "Mocó";
                string szöveg = "Select * FROM jelenlétiív where id>1 ORDER BY id";
                Kezelő_Kiegészítő_Jelenlétiív Kéz = new Kezelő_Kiegészítő_Jelenlétiív();
                List<Adat_Kiegészítő_Jelenlétiív> Adatok = Kéz.Lista_Adatok(helym, jelszóm, szöveg);

                szöveg = "SELECT * FROM főkönyvtábla";
                Kezelő_Kiegészítő_főkönyvtábla KézFő = new Kezelő_Kiegészítő_főkönyvtábla();
                List<Adat_Kiegészítő_főkönyvtábla> FőkönyAdatok = KézFő.Lista_Adatok(helym, jelszóm, szöveg);


                foreach (Adat_Kiegészítő_Jelenlétiív rekord in Adatok)
                {
                    switch (rekord.Id)
                    {
                        case 2:
                            MyE.Kiir(rekord.Szervezet, "d1");
                            MyE.Kiir(rekord.Szervezet, "m1");
                            break;
                        case 3:
                            MyE.Kiir(rekord.Szervezet, "d2");
                            MyE.Kiir(rekord.Szervezet, "m2");
                            break;
                        case 4:
                            MyE.Kiir(rekord.Szervezet, "d3");
                            MyE.Kiir(rekord.Szervezet, "m3");
                            break;

                    }
                }


                // logók beszúrása
                MyE.Kép_beillesztés(munkalap, "A1", Application.StartupPath + @"\Főmérnökség\adatok\BKV.png", 5, 5, 40, 120);
                MyE.Kép_beillesztés(munkalap, "A1", Application.StartupPath + @"\Főmérnökség\adatok\BKV.png", 370, 5, 40, 120);

                // tábla fejléc
                MyE.Egyesít(munkalap, "a5:g5");
                MyE.Kiir("Rendkívüli munka elrendelő lap (egyéni)", "a5");
                MyE.Egyesít(munkalap, "j5:p5");
                MyE.Kiir("Rendkívüli munka elrendelő lap (egyéni)", "j5");
                MyE.Vastagkeret("a5:g5");
                MyE.Vastagkeret("j5:p5");
                // táblázat rajzolás
                MyE.Egyesít(munkalap, "a6:b7");
                MyE.Egyesít(munkalap, "j6:k7");

                MyE.Kiir("Név, HR azonosító,munakör:", "a6");
                MyE.Kiir("Név, HR azonosító,munakör:", "j6");
                MyE.Egyesít(munkalap, "c6:g6");
                MyE.Egyesít(munkalap, "c7:g7");
                MyE.Egyesít(munkalap, "l6:p6");
                MyE.Egyesít(munkalap, "l7:p7");


                MyE.Egyesít(munkalap, "a8:b9");
                MyE.Egyesít(munkalap, "j8:k9");
                MyE.Kiir("A rendkívüli munkavégzés indoka:", "a8");
                MyE.Kiir("A rendkívüli munkavégzés indoka:", "j8");
                MyE.Egyesít(munkalap, "c8:g9");
                MyE.Egyesít(munkalap, "l8:p9");

                MyE.Egyesít(munkalap, "a10:b11");
                MyE.Egyesít(munkalap, "j10:k11");
                MyE.Kiir("Rendkívüli munka fajtája :", "a10");
                MyE.Kiir("Rendkívüli munka fajtája :", "j10");
                MyE.Egyesít(munkalap, "c10:g11");
                MyE.Egyesít(munkalap, "l10:p11");

                MyE.Egyesít(munkalap, "a12:b13");
                MyE.Egyesít(munkalap, "j12:k13");
                MyE.Sortörésseltöbbsorba_egyesített("a12:b13");
                MyE.Sortörésseltöbbsorba_egyesített("j12:k13");
                MyE.Kiir("A rendkívüli munkavégzés időpontja:", "a12");
                MyE.Kiir("A rendkívüli munkavégzés időpontja:", "j12");
                MyE.Egyesít(munkalap, "c12:g12");
                MyE.Egyesít(munkalap, "c13:g13");
                MyE.Egyesít(munkalap, "l12:p12");
                MyE.Egyesít(munkalap, "l13:p13");

                MyE.Egyesít(munkalap, "a14:b15");
                MyE.Egyesít(munkalap, "j14:k15");
                MyE.Kiir("Időtartama:", "a14");
                MyE.Kiir("Időtartama:", "j14");
                MyE.Egyesít(munkalap, "c14:g15");
                MyE.Egyesít(munkalap, "l14:p15");

                MyE.Egyesít(munkalap, "a16:b17");
                MyE.Egyesít(munkalap, "j16:k17");
                MyE.Sortörésseltöbbsorba_egyesített("a16:b17");
                MyE.Sortörésseltöbbsorba_egyesített("j16:k17");
                MyE.Kiir("Elvont pihenőnap esetén a megváltás módja:", "a16");
                MyE.Kiir("Elvont pihenőnap esetén a megváltás módja:", "j16");
                MyE.Egyesít(munkalap, "c16:g17");
                MyE.Egyesít(munkalap, "l16:p17");
                MyE.Sortörésseltöbbsorba_egyesített("C16:G17");
                MyE.Sortörésseltöbbsorba_egyesített("l16:p17");

                MyE.Rácsoz("a6:g17");
                MyE.Vastagkeret("a6:g17");
                MyE.Rácsoz("j6:p17");
                MyE.Vastagkeret("j6:p17");

                MyE.Sormagasság("A6:B17", 15);

                // dátum kiírása
                MyE.Kiir("Budapest, " + DateTime.Now.ToString("yyyy. MMMM. dd"), "a18");
                MyE.Kiir("Budapest, " + DateTime.Now.ToString("yyyy. MMMM. dd"), "j18");

                MyE.Egyesít(munkalap, "d19:g19");
                MyE.Egyesít(munkalap, "m19:p19");
                MyE.Kiir("Rendkívüli munkavégzést elrendelte:", "d19");
                MyE.Kiir("Rendkívüli munkavégzést elrendelte:", "m19");

                for (int i = 20; i < 31; i++)
                {
                    MyE.Egyesít(munkalap, "a" + i.ToString() + ":b" + i.ToString());
                    MyE.Egyesít(munkalap, "d" + i.ToString() + ":g" + i.ToString());
                    MyE.Egyesít(munkalap, "j" + i.ToString() + ":k" + i.ToString());
                    MyE.Egyesít(munkalap, "m" + i.ToString() + ":p" + i.ToString());
                }

                MyE.Kiir("Kiállította, ellenőrizte", "a21");
                MyE.Kiir("Kiállította, ellenőrizte", "j21");
                MyE.Kiir("Átvettem:", "a23");
                MyE.Kiir("Átvettem:", "j23");
                MyE.Kiir("Végrehajtás igazolása:", "d23");
                MyE.Kiir("Végrehajtás igazolása:", "m23");
                MyE.Kiir("munkavállaló aláírása", "a25");
                MyE.Kiir("munkavállaló aláírása", "j25");
                MyE.Kiir("A kifizetést engedélyezem:", "a28");
                MyE.Kiir("A kifizetést engedélyezem:", "j28");

                MyE.Sormagasság("20:20", 35);
                MyE.Sormagasság("24:24", 35);
                MyE.Sormagasság("27:28", 35);

                MyE.Aláírásvonal("a21:b21");
                MyE.Aláírásvonal("d21:g21");
                MyE.Aláírásvonal("j21:k21");
                MyE.Aláírásvonal("m21:p21");
                MyE.Aláírásvonal("a25:b25");
                MyE.Aláírásvonal("d25:g25");
                MyE.Aláírásvonal("j25:k25");
                MyE.Aláírásvonal("m25:p25");
                MyE.Aláírásvonal("d29:g29");
                MyE.Aláírásvonal("m29:p29");

                string Beosztás = (from a in FőkönyAdatok
                                   where a.Id == 2
                                   select a.Beosztás).FirstOrDefault();
                string Név = (from a in FőkönyAdatok
                              where a.Id == 2
                              select a.Név).FirstOrDefault();
                if (Név != null)
                {
                    MyE.Kiir(Név, "d21");
                    MyE.Kiir(Név, "m21");
                    MyE.Kiir(Név, "d25");
                    MyE.Kiir(Név, "m25");
                }
                if (Beosztás != null)
                {
                    MyE.Kiir(Beosztás, "d22");
                    MyE.Kiir(Beosztás, "m22");
                    MyE.Kiir(Beosztás, "d26");
                    MyE.Kiir(Beosztás, "m26");
                }
                Beosztás = (from a in FőkönyAdatok
                            where a.Id == 3
                            select a.Beosztás).FirstOrDefault();
                Név = (from a in FőkönyAdatok
                       where a.Id == 3
                       select a.Név).FirstOrDefault();
                if (Név != null)
                {
                    MyE.Kiir(Név, "d29");
                    MyE.Kiir(Név, "m29");
                }
                if (Beosztás != null)
                {
                    MyE.Kiir(Beosztás, "d30");
                    MyE.Kiir(Beosztás, "m30");
                }
                MyE.NyomtatásiTerület_részletes(munkalap, "a1:p30", "", "", false);

                helym = $@"{Application.StartupPath}\{CmbTelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
                jelszóm = "forgalmiutasítás";
                szöveg = "SELECT * FROM dolgozóadatok";
                Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> DolgAdatok = KézDolg.Lista_Adatok(helym, jelszóm, szöveg);


                for (int i = 0; i < Tábla.SelectedRows.Count; i++)
                {
                    Holtart.Lép();

                    MyE.Kiir("Sorszám: " + Tábla.SelectedRows[i].Cells[0].Value.ToStrTrim(), "a4");
                    MyE.Kiir("Sorszám: " + Tábla.SelectedRows[i].Cells[0].Value.ToStrTrim(), "j4");

                    MyE.Kiir(Tábla.SelectedRows[i].Cells[2].Value.ToStrTrim() + "(" + Tábla.SelectedRows[i].Cells[1].Value.ToStrTrim() + ")", "c6");
                    MyE.Kiir(Tábla.SelectedRows[i].Cells[2].Value.ToStrTrim() + "(" + Tábla.SelectedRows[i].Cells[1].Value.ToStrTrim() + ")", "l6");


                    szöveg = "SELECT * FROM dolgozóadatok WHERE dolgozószám='{ Tábla.SelectedRows[i].Cells[1].Value.ToStrTrim()}'";
                    válasz = (from a in DolgAdatok
                              where a.Dolgozószám.Trim() == Tábla.SelectedRows[i].Cells[1].Value.ToStrTrim()
                              select a.Munkakör.Trim()).FirstOrDefault();

                    if (válasz != null)
                    {
                        MyE.Kiir(válasz, "c7");
                        MyE.Kiir(válasz, "l7");
                    }

                    válasz = Tábla.SelectedRows[i].Cells[5].Value.ToStrTrim();
                    if (válasz.Contains("&T"))
                    {
                        válasz = válasz.Substring(2, válasz.Length - 2).Trim();
                        MyE.Kiir(válasz, "c8");
                        MyE.Kiir(válasz, "l8");
                        MyE.Kiir("Túlóra", "c10");
                        MyE.Kiir("Túlóra", "l10");
                        MyE.Kiir("-", "c16");
                        MyE.Kiir("-", "l16");
                    }
                    else if (válasz.Contains("&EB"))
                    {
                        válasz = válasz.Substring(3, válasz.Length - 3).Trim();
                        MyE.Kiir(válasz, "c8");
                        MyE.Kiir(válasz, "l8");
                        MyE.Kiir("Elvont pihenő", "c10");
                        MyE.Kiir("Elvont pihenő", "l10");
                        MyE.Kiir("100 % bérpótlék", "c16");
                        MyE.Kiir("100 % bérpótlék", "l16");
                    }

                    else if (válasz.Contains("&EP"))
                    {
                        válasz = válasz.Substring(3, válasz.Length - 3).Trim();
                        MyE.Kiir(válasz, "c8");
                        MyE.Kiir(válasz, "l8");
                        MyE.Kiir("Elvont pihenő", "c10");
                        MyE.Kiir("Elvont pihenő", "l10");
                        MyE.Kiir("100 % bérpótlék", "c16");
                        MyE.Kiir("100 % bérpótlék", "l16");
                    }

                    else if (válasz.Contains("&V"))
                    {
                        válasz = válasz.Substring(2, válasz.Length - 2).Trim();
                        MyE.Kiir(válasz, "c8");
                        MyE.Kiir(válasz, "l8");
                        MyE.Kiir("visszaadott pihenő", "c10");
                        MyE.Kiir("visszaadott pihenő", "l10");
                        MyE.Kiir("-", "c16");
                        MyE.Kiir("-", "l16");

                    }

                    MyE.Sortörésseltöbbsorba_egyesített("C8");
                    MyE.Sortörésseltöbbsorba_egyesített("l8");

                    eleje = DateTime.Parse(Tábla.SelectedRows[i].Cells[7].Value.ToString());
                    vége = DateTime.Parse(Tábla.SelectedRows[i].Cells[8].Value.ToString());
                    válasz = Tábla.SelectedRows[i].Cells[3].Value.ToStrTrim() + " nap " + Tábla.SelectedRows[i].Cells[7].Value.ToStrTrim() + " -tól";
                    MyE.Kiir(válasz, "c12");
                    MyE.Kiir(válasz, "l12");

                    if (eleje < vége)
                    {
                        // nappal
                        válasz = Tábla.SelectedRows[i].Cells[3].Value.ToStrTrim() + " nap " + Tábla.SelectedRows[i].Cells[8].Value.ToStrTrim() + " -ig";
                        MyE.Kiir(válasz, "c13");
                        MyE.Kiir(válasz, "l13");
                    }
                    else
                    {
                        // éjszaka
                        válasz = Tábla.SelectedRows[i].Cells[3].Value.ToStrTrim() + " nap " + Tábla.SelectedRows[i].Cells[8].Value.ToStrTrim() + " -ig";
                        MyE.Kiir(válasz, "c13");
                        MyE.Kiir(válasz, "l13");
                    }


                    MyE.Kiir(Math.Round((double.Parse(Tábla.SelectedRows[i].Cells[4].Value.ToString()) / 60d), 2) + " óra", "c14");
                    MyE.Kiir(Math.Round((double.Parse(Tábla.SelectedRows[i].Cells[4].Value.ToString()) / 60d), 2) + " óra", "l14");

                    MyE.Nyomtatás(munkalap, 1, 1);
                }


                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < Tábla.SelectedRows.Count; i++)
                {
                    szöveg = "Update  túlóra set státus=1 Where sorszám=" + Tábla.SelectedRows[i].Cells[0].Value.ToString();
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

                // ****************************
                // excel tábla érdemi rész vége
                // ****************************

                Holtart.Ki();
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc + ".xlsx");
                MyE.ExcelBezárás();

                if (!CheckBox2.Checked)
                {
                    Delete(fájlexc + ".xlsx");
                }

                Túlórakiírás(1);
                MessageBox.Show("A kijelölt tételek nyomtatása megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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




        private void Túlórakiírás(int csoport)
        {
            try
            {
                Textlista = "Túlóra";
                Táblatörlése();
                Tábla.Visible = false;
                Tábla.ColumnCount = 9;
                Tábla.RowCount = 0;
                int összes = 0;
                Tábla.Columns[0].HeaderText = "Sorszám";
                Tábla.Columns[0].Width = 70;
                Tábla.Columns[1].HeaderText = "HR azonosító";
                Tábla.Columns[1].Width = 120;
                Tábla.Columns[2].HeaderText = "Dolgozó név";
                Tábla.Columns[2].Width = 250;
                Tábla.Columns[3].HeaderText = "Túlóranap";
                Tábla.Columns[3].Width = 150;
                Tábla.Columns[4].HeaderText = "Túlóra percben";
                Tábla.Columns[4].Width = 60;
                Tábla.Columns[5].HeaderText = "Túlóra oka";
                Tábla.Columns[5].Width = 400;
                Tábla.Columns[6].HeaderText = "Státusz";
                Tábla.Columns[6].Width = 90;
                Tábla.Columns[7].HeaderText = "Kezdete";
                Tábla.Columns[7].Width = 60;
                Tábla.Columns[8].HeaderText = "Vége";
                Tábla.Columns[8].Width = 60;

                if (!Exists(hely))
                    throw new HibásBevittAdat("Ebben az évben nem lett létrehozva adatbázis.");

                if (csoport == 0 && Dolgozónév.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");

                string szöveg = "SELECT * FROM Túlóra ";

                string[] darabol = Dolgozónév.Text.Trim().Split('=');

                if (csoport == 0)
                    szöveg += $" WHERE Törzsszám='{darabol[1].Trim()}' ";
                else if (!Túlóramind.Checked) // mind
                {
                    // Igényelt
                    if (Túlóraigényelt.Checked)
                        szöveg += " WHERE státus=0";
                    // nyomtatott
                    if (Túlóranyomtatott.Checked)
                        szöveg += " WHERE státus=1";
                    // Rögzített
                    if (Túlórarögzített.Checked)
                        szöveg += " WHERE státus=2";
                }
                szöveg += " order by kezdődátum";

                Kezelő_Szatube_Túlóra Kéz = new Kezelő_Szatube_Túlóra();
                List<Adat_Szatube_Túlóra> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                int i;

                foreach (Adat_Szatube_Túlóra rekord in Adatok)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Sorszám;
                    Tábla.Rows[i].Cells[1].Value = rekord.Törzsszám.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Dolgozónév.Trim();
                    Tábla.Rows[i].Cells[3].Value = rekord.Kezdődátum.ToString("yyyy.MM.dd");
                    Tábla.Rows[i].Cells[4].Value = rekord.Kivettnap;
                    if (rekord.Státus != 3)
                        összes += rekord.Kivettnap;
                    Tábla.Rows[i].Cells[5].Value = rekord.Szabiok.Trim();
                    Tábla.Rows[i].Cells[7].Value = rekord.Kezdőidő.ToString("HH:mm");
                    Tábla.Rows[i].Cells[8].Value = rekord.Befejezőidő.ToString("HH:mm");
                    Tábla.Rows[i].Cells[6].Value = SzabStátus(rekord.Státus);
                }
                if (csoport == 0)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[2].Value = "Túlóra összesen:";
                    Tábla.Rows[i].Cells[4].Value = összes;
                }
                Tábla.Refresh();

                Tábla_Formázás();

                Tábla.Visible = true;

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


        private void AblakSzaTuBe_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 17)
                Chk_CTRL.Checked = true;
        }


        private void AblakSzaTuBe_KeyUp(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 17)
                Chk_CTRL.Checked = false;

        }


        private void Panel6_MouseClick(object sender, MouseEventArgs e)
        {
            if (Chk_CTRL.Checked == true)
            {
                if (CheckBox2.Visible == true)
                {
                    CheckBox2.Visible = false;
                }
                else
                {
                    CheckBox2.Visible = true;
                }
            }
        }

        #endregion


        #region Túlóra egyéni
        private void Túl_egy_kiirás_Click(object sender, EventArgs e)
        {
            Túlórakiírás(0);
        }
        #endregion


        #region Beteg
        private void Beteg_Össz_Click(object sender, EventArgs e)
        {
            Betegkiírása(1);
        }


        private void Beteg_Egy_Click(object sender, EventArgs e)
        {
            Betegkiírása(0);
        }


        private void Betegkiírása(int csoport)
        {
            try
            {
                Textlista = "Beteg";
                Táblatörlése();
                int összes = 0;
                Tábla.ColumnCount = 8;
                Tábla.RowCount = 0;

                Tábla.Columns[0].HeaderText = "Sorszám";
                Tábla.Columns[0].Width = 70;
                Tábla.Columns[1].HeaderText = "HR azonosító";
                Tábla.Columns[1].Width = 120;
                Tábla.Columns[2].HeaderText = "Dolgozó név";
                Tábla.Columns[2].Width = 250;
                Tábla.Columns[3].HeaderText = "Beteg nap";
                Tábla.Columns[3].Width = 150;
                Tábla.Columns[4].HeaderText = "Kezdete";
                Tábla.Columns[4].Width = 120;
                Tábla.Columns[5].HeaderText = "Vége";
                Tábla.Columns[5].Width = 120;
                Tábla.Columns[6].HeaderText = "Oka";
                Tábla.Columns[6].Width = 300;
                Tábla.Columns[7].HeaderText = "Státusz";
                Tábla.Columns[7].Width = 120;

                if (!Exists(hely))
                    throw new HibásBevittAdat("Az adatbázis ebben az évben nem lett létrehozva");
                if (csoport == 0 && Dolgozónév.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kijelölve dolgozó.");

                string[] darabol = Dolgozónév.Text.Trim().Split('=');

                string szöveg = "SELECT * FROM beteg ";

                if (csoport == 0)
                {
                    szöveg += $" WHERE Törzsszám='{darabol[1].Trim()}'";
                }
                szöveg += " order by kezdődátum";

                Kezelő_Szatube_Beteg Kéz = new Kezelő_Szatube_Beteg();
                List<Adat_Szatube_Beteg> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);
                int i;
                Tábla.Visible = false;
                foreach (Adat_Szatube_Beteg rekord in Adatok)
                {

                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Sorszám;
                    Tábla.Rows[i].Cells[1].Value = rekord.Törzsszám.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Dolgozónév.Trim();
                    Tábla.Rows[i].Cells[3].Value = rekord.Kivettnap.ToString();
                    Tábla.Rows[i].Cells[4].Value = rekord.Kezdődátum.ToString("yyyy.MM.dd");
                    Tábla.Rows[i].Cells[5].Value = rekord.Befejeződátum.ToString("yyyy.MM.dd");

                    Tábla.Rows[i].Cells[6].Value = rekord.Szabiok.Trim();
                    Tábla.Rows[i].Cells[7].Value = SzabStátus(rekord.Státus);

                    if (rekord.Státus != 3) összes += rekord.Kivettnap;
                }
                if (csoport == 0)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[2].Value = "Betegállomány összesen:";
                    Tábla.Rows[i].Cells[3].Value = összes;
                }
                Tábla.Refresh();

                Tábla_Formázás();

                Tábla.Visible = true;
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


        #region Csúsztatás
        private void Csúsztatáskiírása(int csoport)
        {
            try
            {
                Textlista = "Csúsztatás";
                Táblatörlése();
                int összes = 0;
                Tábla.ColumnCount = 9;
                Tábla.RowCount = 0;

                Tábla.Columns[0].HeaderText = "Sorszám";
                Tábla.Columns[0].Width = 70;
                Tábla.Columns[1].HeaderText = "HR azonosító";
                Tábla.Columns[1].Width = 120;
                Tábla.Columns[2].HeaderText = "Dolgozó név";
                Tábla.Columns[2].Width = 250;
                Tábla.Columns[3].HeaderText = "Csúsztatás napja";
                Tábla.Columns[3].Width = 150;
                Tábla.Columns[4].HeaderText = "Idő órában";
                Tábla.Columns[4].Width = 60;
                Tábla.Columns[5].HeaderText = "Oka";
                Tábla.Columns[5].Width = 430;
                Tábla.Columns[6].HeaderText = "Státusz";
                Tábla.Columns[6].Width = 60;
                Tábla.Columns[7].HeaderText = "Kezdete";
                Tábla.Columns[7].Width = 120;
                Tábla.Columns[8].HeaderText = "Vége";
                Tábla.Columns[8].Width = 120;
                if (!Exists(hely))
                    throw new HibásBevittAdat("Ebben az évben nem lett létrehozva adatbázis.");

                if (csoport == 0 && Dolgozónév.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");


                string szöveg = "SELECT * FROM Csúsztatás ";
                string[] darabol = Dolgozónév.Text.Trim().Split('=');

                if (csoport == 0)
                    szöveg += $" WHERE Törzsszám='{darabol[1].Trim()}'";
                szöveg += " order by kezdődátum";

                Kezelő_Szatube_Csúsztatás Kéz = new Kezelő_Szatube_Csúsztatás();
                List<Adat_Szatube_Csúsztatás> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                int i;
                Tábla.Visible = false;

                foreach (Adat_Szatube_Csúsztatás rekord in Adatok)
                {

                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Sorszám;
                    Tábla.Rows[i].Cells[1].Value = rekord.Törzsszám.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Dolgozónév.Trim();
                    Tábla.Rows[i].Cells[3].Value = rekord.Kezdődátum.ToString("yyyy.MM.dd");
                    Tábla.Rows[i].Cells[4].Value = rekord.Kivettnap;
                    Tábla.Rows[i].Cells[5].Value = rekord.Szabiok.Trim();
                    Tábla.Rows[i].Cells[6].Value = SzabStátus(rekord.Státus);
                    Tábla.Rows[i].Cells[7].Value = rekord.Kezdőidő.ToString("HH:mm");
                    Tábla.Rows[i].Cells[8].Value = rekord.Befejezőidő.ToString("HH:mm");
                    if (rekord.Státus != 3)
                        összes += rekord.Kivettnap;
                }
                if (csoport == 0)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[2].Value = "Rendelkezésre áll összesen:";
                    Tábla.Rows[i].Cells[4].Value = összes;
                }
                Tábla.Refresh();
                Tábla_Formázás();
                Tábla.Visible = true;
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


        private void Csúsz_Össz_lista_Click(object sender, EventArgs e)
        {
            Csúsztatáskiírása(1);
        }


        private void Csúsz_Egy_lista_Click(object sender, EventArgs e)
        {
            Csúsztatáskiírása(0);
        }

        #endregion


        #region AFT

        private void Aft_Össz_Lista_Click(object sender, EventArgs e)
        {
            AFTkiírása(1);
        }


        private void Aft_Egy_Lista_Click(object sender, EventArgs e)
        {
            AFTkiírása(0);
        }


        private void AFTkiírása(int csoport)
        {
            try
            {
                Textlista = "AFT";
                Táblatörlése();
                int összes = 0;
                Tábla.ColumnCount = 7;
                Tábla.RowCount = 0;

                Tábla.Columns[0].HeaderText = "Sorszám";
                Tábla.Columns[0].Width = 70;
                Tábla.Columns[1].HeaderText = "HR azonosító";
                Tábla.Columns[1].Width = 120;
                Tábla.Columns[2].HeaderText = "Dolgozó név";
                Tábla.Columns[2].Width = 250;
                Tábla.Columns[3].HeaderText = "AFT napja";
                Tábla.Columns[3].Width = 150;
                Tábla.Columns[4].HeaderText = "Idő órában";
                Tábla.Columns[4].Width = 60;
                Tábla.Columns[5].HeaderText = "AFT oka";
                Tábla.Columns[5].Width = 430;
                Tábla.Columns[6].HeaderText = "Státusz";
                Tábla.Columns[6].Width = 60;

                if (!Exists(hely))
                    throw new HibásBevittAdat("Ebben az évben nem lett létrehozva adatbázis.");

                if (csoport == 0 && Dolgozónév.Text.Trim() == "")
                    throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");

                string szöveg = "SELECT * FROM AFT ";
                string[] darabol = Dolgozónév.Text.Trim().Split('=');

                if (csoport == 0)
                    szöveg += $" WHERE Törzsszám='{darabol[1].Trim()}' ";
                szöveg += " order by dátum";

                Kezelő_Szatube_Aft Kéz = new Kezelő_Szatube_Aft();
                List<Adat_Szatube_AFT> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                int i;

                Tábla.Visible = false;

                foreach (Adat_Szatube_AFT rekord in Adatok)
                {

                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Sorszám;
                    Tábla.Rows[i].Cells[1].Value = rekord.Törzsszám.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Dolgozónév.Trim();
                    Tábla.Rows[i].Cells[3].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                    Tábla.Rows[i].Cells[4].Value = rekord.AFTóra;
                    Tábla.Rows[i].Cells[5].Value = rekord.AFTok.Trim();
                    if (rekord.Státus != 3)
                        összes += rekord.AFTóra;
                    Tábla.Rows[i].Cells[6].Value = SzabStátus(rekord.Státus);
                }
                Tábla.Refresh();
                if (csoport == 0)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[2].Value = "AFT összesen:";
                    Tábla.Rows[i].Cells[4].Value = összes;
                }

                Tábla_Formázás();
                Tábla.Visible = true;
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

        #region Határnapi összesítés

        private void Határnapig_Összesít_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Exists(hely))
                    throw new HibásBevittAdat("Ebben az évben nem lett létrehozva adatbázis.");

                string szöveg = $"SELECT * FROM szabadság WHERE Státus=0 AND sorszám=0 AND Kezdődátum<=#{Határnap.Value:yyyy-MM-dd}# ORDER BY törzsszám,kezdődátum";

                Holtart.Be();
                Kezelő_Szatube_Szabadság Kéz = new Kezelő_Szatube_Szabadság();
                List<Adat_Szatube_Szabadság> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);
                Adat_Szatube_Szabadság Ideig = null;

                string Első_HR = "";
                DateTime ELső_Kezdő = new DateTime(1900, 1, 1);
                DateTime ELőző_Kezdő = new DateTime(1900, 1, 1);
                DateTime Első_Befejező = new DateTime(1900, 1, 1);

                foreach (Adat_Szatube_Szabadság rekord in Adatok)
                {
                    if (Első_HR == "")
                    {
                        // első alkalommal feltöltjük 
                        Első_HR = rekord.Törzsszám.Trim();
                        ELső_Kezdő = rekord.Kezdődátum;
                        ELőző_Kezdő = rekord.Kezdődátum;
                        Első_Befejező = rekord.Befejeződátum;
                    }

                    if (Első_HR.Trim() != rekord.Törzsszám.Trim())
                    {
                        Ideig = new Adat_Szatube_Szabadság(0, Első_HR, "", ELső_Kezdő, Első_Befejező, 1, "", 0, "", new DateTime(1900, 1, 1));
                        Csoportosítja_Elemeket(Ideig);
                        Első_HR = rekord.Törzsszám.Trim();
                        ELső_Kezdő = rekord.Kezdődátum;
                        ELőző_Kezdő = rekord.Kezdődátum;
                        Első_Befejező = rekord.Befejeződátum;
                    }
                    else
                    { //Ha egy forma
                      //Ha előzőt követi akkor tovább megy
                        if (Első_Befejező.AddDays(1) == rekord.Befejeződátum)
                        {
                            Első_Befejező = rekord.Befejeződátum;
                            ELőző_Kezdő = rekord.Kezdődátum;
                        }
                        else
                        {
                            // Ha van közben szünet

                            if (VanKözötte(Első_HR, ELőző_Kezdő, rekord.Kezdődátum))
                            {
                                //ha van a két dátum között beosztás, akkor befejezi az előző adagot
                                Ideig = new Adat_Szatube_Szabadság(0, Első_HR, "", ELső_Kezdő, Első_Befejező, 1, "", 0, "", new DateTime(1900, 1, 1));
                                Csoportosítja_Elemeket(Ideig);
                                Első_HR = rekord.Törzsszám.Trim();
                                ELső_Kezdő = rekord.Kezdődátum;
                                ELőző_Kezdő = rekord.Kezdődátum;
                                Első_Befejező = rekord.Befejeződátum;
                            }
                            else
                            {
                                //ha nincs akkor tovább megyünk
                                Első_Befejező = rekord.Befejeződátum;
                                ELőző_Kezdő = rekord.Kezdődátum;
                            }
                        }
                    }
                    Holtart.Lép();
                }
                //Az utolsó elemet is rögzítjük    
                // ha kilépünk a ciklusból és az utolsót nem rögzítettünk
                Ideig = new Adat_Szatube_Szabadság(0, Első_HR, "", ELső_Kezdő, Első_Befejező, 1, "", 0, "", new DateTime(1900, 1, 1));
                Csoportosítja_Elemeket(Ideig);

                Szabadságkiírása(1);
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


        void Csoportosítja_Elemeket(Adat_Szatube_Szabadság rekord)
        {
            SzabadságListaFeltöltés();

            double sorszám = 1;
            if (Adatok_Szabadság.Count > 0) sorszám = Adatok_Szabadság.Max(a => a.Sorszám) + 1;

            string szöveg = $"UPDATE szabadság SET sorszám={sorszám} WHERE Törzsszám='{rekord.Törzsszám.Trim()}' AND Kezdődátum>=#{rekord.Kezdődátum:yyyy-MM-dd}#";
            szöveg += $"AND  Befejeződátum<=#{rekord.Befejeződátum:yyyy-MM-dd}# AND státus<>3";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        bool VanKözötte(string Első_HR, DateTime ELőző_Kezdő, DateTime Aktuális)
        {
            bool válasz = false;
            string szöveg = $"SELECT * FROM beosztás WHERE Dolgozószám='{Első_HR.Trim()}' AND nap>#{ELőző_Kezdő:yyyy-MM-dd}# AND nap<#{Aktuális:yyyy-MM-dd}#";
            string helyb = $@"{Application.StartupPath}\{CmbTelephely.Text.Trim()}\Adatok\Beosztás\{ELőző_Kezdő.Year}\Ebeosztás{ELőző_Kezdő:yyyyMM}.mdb";
            string jelszób = "kiskakas";

            Kezelő_Dolgozó_Beosztás KézBeosztás = new Kezelő_Dolgozó_Beosztás();
            Adat_Dolgozó_Beosztás Elem = KézBeosztás.Egy_Adat(helyb, jelszób, szöveg);

            if (Elem != null) válasz = true;

            return válasz;
        }


        #endregion

        #region Listák

        private void SzabadságListaFeltöltés()
        {
            try
            {
                Adatok_Szabadság.Clear();
                string szöveg = "SELECT * FROM szabadság ";
                Adatok_Szabadság = Kéz_Szabadság.Lista_Adatok(hely, jelszó, szöveg);
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
