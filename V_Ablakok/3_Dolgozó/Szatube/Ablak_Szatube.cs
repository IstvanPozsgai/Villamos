using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Adatszerkezet;
using static System.IO.File;
using MyX = Villamos.MyClosedXML_Excel;
using MyF = Függvénygyűjtemény;
using Villamos.V_Ablakok._3_Dolgozó.Szatube;


namespace Villamos
{
    public partial class Ablak_Szatube
    {
        string Texttelephely;
        string Textlista;

        readonly Kezelő_Dolgozó_Személyes KézSzemélyes = new Kezelő_Dolgozó_Személyes();
        readonly Kezelő_Dolgozó_Alap KézDolgAlap = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_Dolgozó_Beosztás_Új KézBeosztás = new Kezelő_Dolgozó_Beosztás_Új();
        readonly Kezelő_Szatube_Aft KézAft = new Kezelő_Szatube_Aft();
        readonly Kezelő_Szatube_Beteg KézBeteg = new Kezelő_Szatube_Beteg();
        readonly Kezelő_Szatube_Csúsztatás KézCsúsztatás = new Kezelő_Szatube_Csúsztatás();
        readonly Kezelő_Szatube_Szabadság KézSzabadság = new Kezelő_Szatube_Szabadság();
        readonly Kezelő_Szatube_Túlóra KézTúlóra = new Kezelő_Szatube_Túlóra();
        readonly Kezelő_Kiegészítő_Szabadságok KézKiegSzab = new Kezelő_Kiegészítő_Szabadságok();
        readonly Kezelő_Kiegészítő_Jelenlétiív KézJelenléti = new Kezelő_Kiegészítő_Jelenlétiív();
        readonly Kezelő_Kiegészítő_főkönyvtábla KézFő = new Kezelő_Kiegészítő_főkönyvtábla();

        List<Adat_Szatube_Szabadság> Adatok_Szabadság = new List<Adat_Szatube_Szabadság>();
        List<string > NyomtatásiFájlok= new List<string>();

        public Ablak_Szatube()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_Szatube_Load(object sender, EventArgs e)
        {

        }

        #region  Alap
        private void Start()
        {
            try
            {
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, CmbTelephely.Text.Trim());
                }
                else
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }

                Évek_Feltöltése();
                Névfeltöltés();
                Munkahely();
                TabFülek.TabIndex = 0;

                // Gombok nem láthatóak mert mindent listáz
                SzabLeadás.Enabled = false;
                SzabNyomtatás.Enabled = false;
                EgyéniTúlNyom.Enabled = false;
                TúlCsopNyom.Enabled = false;
                Túl_Eng_Beáll.Enabled = false;
                TabFülek.DrawMode = TabDrawMode.OwnerDrawFixed;
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

        private void TelephelyekFeltöltéseÚj()
        {
            try
            {
                CmbTelephely.Items.Clear();
                foreach (string Adat in GombLathatosagKezelo.Telephelyek(this.Name))
                    CmbTelephely.Items.Add(Adat.Trim());
                //Alapkönyvtárat beállítjuk 
                if (CmbTelephely.Items.Cast<string>().Contains(Program.PostásTelephely))
                    CmbTelephely.Text = Program.PostásTelephely;
                else
                    CmbTelephely.Text = CmbTelephely.Items[0].ToStrTrim();
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
            List<Adat_Dolgozó_Alap> Adatok = KézDolgAlap.Lista_Adatok(CmbTelephely.Text.Trim(), !Kilépettjel.Checked);

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
            Adat_Kiegészítő_Jelenlétiív Rekord = KézJelenléti.Lista_Adatok(CmbTelephely.Text.Trim()).Where(a => a.Id == 4).FirstOrDefault();
            if (Rekord != null) Texttelephely = Rekord.Szervezet.Trim();
        }

        private void Jogosultságkiosztás()
        {
            int melyikelem;
            // ide kell az összes gombot tenni amit szabályozni akarunk
            // Szabi gombok
            SzabNyilat.Visible = false;
            SzabNyomtatás.Visible = false;
            SzabLeadás.Visible = false;
            Éves_Összesítő.Visible = false;
            Szab_Rögzít.Visible = false;
            // túlóra gombok
            Túl_Eng_Beáll.Visible = false;
            EgyéniTúlNyom.Visible = false;
            TúlCsopNyom.Visible = false;
            // telephely szabadválasztás
            CmbTelephely.Visible = false;

            // Szabadság
            melyikelem = 61;
            // módosítás 1
            if (MyF.Vanjoga(melyikelem, 1))
            {
                SzabNyomtatás.Visible = true;
                SzabLeadás.Visible = true;
            }
            // módosítás 2
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Éves_Összesítő.Visible = true;
                Szab_Rögzít.Visible = true;
            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
                SzabNyilat.Visible = true;
            }

            // túlóra
            melyikelem = 62;
            // módosítás 1
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Túl_Eng_Beáll.Visible = true;
            }
            // módosítás 2
            if (MyF.Vanjoga(melyikelem, 2))
            {
                EgyéniTúlNyom.Visible = true;
                TúlCsopNyom.Visible = true;
            }
            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            { }

            // telephely választás
            melyikelem = 63;
            // módosítás 1
            if (MyF.Vanjoga(melyikelem, 1))
            {
                CmbTelephely.Visible = true;
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
                MyX.ExcelMegnyitás(hely);
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
            TelephelyVáltás();
        }

        private void TelephelyVáltás()
        {
            Névfeltöltés();
        }

        private void BtnExcelkimenet_Click(object sender, EventArgs e)
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
                    FileName = $"SzaTubecs_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.DataGridViewToXML(fájlexc, Tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyX.ExcelMegnyitás(fájlexc);
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

        private void CmbTelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                CmbTelephely.Text = CmbTelephely.Items[CmbTelephely.SelectedIndex].ToStrTrim();
                if (CmbTelephely.Text.Trim() == "") return;
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                    GombLathatosagKezelo.Beallit(this, CmbTelephely.Text.Trim());
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
        #endregion


        #region Általános
        private void Tábla_Formázás()
        {
            try
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
                if (!Exists(fájlexcel)) throw new HibásBevittAdat("Hiányzik az kitöltendő táblázat!");

                             // 0 sorszámút ki kell jelölni.
                for (int i = 0; i < Tábla.Rows.Count; i++)
                    if (long.Parse(Tábla.Rows[i].Cells[0].Value.ToString()) == 0)
                        Tábla.Rows[i].Selected = false;


                if (Tábla.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kijelölve egy sor sem.");

                //Beolvassuk a táblázat adatait egy listába
                List<Adat_Szatube_Szabadság> Adatok = KézSzabadság.Lista_Adatok(CmbTelephely.Text.Trim(), Adat_Évek.Text.ToÉrt_Int());
                Adatok = (from a in Adatok
                          where a.Szabiok.ToLower().Contains("kivétel")
                          orderby a.Kezdődátum
                          select a).ToList();

                if (!Mind.Checked)// mind
                {
                    // Igényelt
                    if (Kért.Checked)
                        Adatok = Adatok.Where(a => a.Státus == 0).ToList();
                    // nyomtatott
                    if (Nyomtatott.Checked)
                        Adatok = Adatok.Where(a => a.Státus == 1).ToList();
                    // Rögzített
                    if (Rögzített.Checked)
                        Adatok = Adatok.Where(a => a.Státus == 2).ToList();
                }

                List<double> Sorszámok = new List<double>();
                // Beolvassuk egy listába azokat a sorszámokat amik ki vannak jelölve
                for (int i = 0; i < Tábla.SelectedRows.Count; i++)
                {
                    long utolsó = long.Parse(Tábla.SelectedRows[i].Cells[0].Value.ToString());
                    Sorszámok.Add(utolsó);
                }
                Sorszámok = Sorszámok.OrderBy(num => num).ToList();
                List<double> SzűrtLista = Sorszámok.Distinct().ToList();

                string Telephely_ = CmbTelephely.Text.Trim();
                int Évek_ = Adat_Évek.Text.ToÉrt_Int();

                Szatube_NyomtatasSzabi NyomtatSzabi = new Szatube_NyomtatasSzabi(KézSzabadság, Telephely_, Évek_);
                NyomtatSzabi.Kiir(fájlexcel, Tábla, SzűrtLista, Adatok);

                // a státusokat átállítja
                List<double> Idek = new List<double>();
                for (int i = 0; i < SzűrtLista.Count; i++)
                {
                    if (SzűrtLista[i] != 0)
                    {
                        Idek.Add(SzűrtLista[i]);
                    }
                    Holtart.Lép();
                }
                KézSzabadság.StátusÁllítás(CmbTelephely.Text.Trim(), Adat_Évek.Text.ToÉrt_Int(), 1, Idek);

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

                if (csoport == 0 && Dolgozónév.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");

                string[] darabol = Dolgozónév.Text.Trim().Split('=');

                Adatok_Szabadság = KézSzabadság.Lista_Adatok(CmbTelephely.Text.Trim(), Adat_Évek.Text.ToÉrt_Int());

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
                if (Textlista != "Szabadság") throw new HibásBevittAdat("A táblázat tartalma nem Szabadság adat.");
                if (Tábla.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy sor sem!");

                List<double> Sorszámok = new List<double>();
                for (int i = 0; i < Tábla.SelectedRows.Count; i++)
                {
                    if (!int.TryParse(Tábla.SelectedRows[i].Cells[0].Value.ToString(), out int sorszám)) throw new HibásBevittAdat("Nincs érvényes sor kiválasztva.");
                    if (sorszám != 0) Sorszámok.Add(sorszám);
                }
                KézSzabadság.StátusÁllítás(CmbTelephely.Text.Trim(), Adat_Évek.Text.ToÉrt_Int(), 2, Sorszámok);

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

        private void Mind_Click(object sender, EventArgs e)
        {
            SzabLeadás.Enabled = false;
            SzabNyomtatás.Enabled = false;
            Szabadságkiírása(1);
        }

        private void Kért_Click(object sender, EventArgs e)
        {
            SzabNyomtatás.Enabled = true;
            SzabLeadás.Enabled = false;
            Szabadságkiírása(1);
        }

        private void Nyomtatott_Click(object sender, EventArgs e)
        {
            SzabNyomtatás.Enabled = true;
            SzabLeadás.Enabled = true;
            Szabadságkiírása(1);
        }

        private void Rögzített_Click(object sender, EventArgs e)
        {
            SzabNyomtatás.Enabled = true;
            SzabLeadás.Enabled = false;
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

                Adatok_Szabadság = KézSzabadság.Lista_Adatok(CmbTelephely.Text.Trim(), Adat_Évek.Text.ToÉrt_Int());

                double id = 1;
                if (Adatok_Szabadság.Count > 0) id = Adatok_Szabadság.Max(a => a.Sorszám) + 1;

                string[] darabol = Dolgozónév.Text.Split('=');
                Adat_Szatube_Szabadság ADAT = new Adat_Szatube_Szabadság(
                                    id,
                                    darabol[1].Trim(),
                                    darabol[0].Trim(),
                                    new DateTime(1900, 1, 1),
                                    new DateTime(1900, 1, 1),
                                    Nap,
                                    Szabiok.Text.Trim(),
                                    2,
                                    Program.PostásNév.Trim(),
                                    DateTime.Now);
                KézSzabadság.Rögzítés(CmbTelephely.Text.Trim(), Adat_Évek.Text.ToÉrt_Int(), ADAT);
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
                if (Dolgozónév.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");
                Szabadságkiírása(0);

                string[] darabol = Dolgozónév.Text.Trim().Split('=');
                string fájlexc;
                Holtart.Be();

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Dolgozó éves szabadság felhasználása ",
                    FileName = $"Éves_{Dolgozónév.Text.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                string Telephely_ = CmbTelephely.Text.Trim();
                int Evek_ = Adat_Évek.Text.Trim().ToÉrt_Int();

                Szatube_Eves_Osszesito eves_osszesito_excel = new Szatube_Eves_Osszesito();
                eves_osszesito_excel.Eves_Osszesito(fájlexc, darabol, KézSzabadság, Telephely_, Evek_);

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

        private void SzabNyilat_Click(object sender, EventArgs e)
        {
            try
            {
                if (Dolgozónév.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");

                string fájlexcel = $@"{Application.StartupPath}\{CmbTelephely.Text.ToString()}\nyomtatvány\Szabadságkivétel_egylapos.xlsx";
                if (!Exists(fájlexcel)) throw new HibásBevittAdat("Hiányzik az kitöltendő táblázat!");

                Holtart.Be();
                MyX.ExcelMegnyitás(fájlexcel);
                string[] darabol = Dolgozónév.Text.Split('=');

                Holtart.Lép();

                List<Adat_Dolgozó_Személyes> Adatok = KézSzemélyes.Lista_Adatok();
                Adat_Dolgozó_Személyes Rekord = (from a in Adatok
                                                 where a.Dolgozószám == darabol[1].Trim()
                                                 select a).FirstOrDefault();

                string könyvtár = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string fájlnév = $"Szabadság_Nyilatkozat_{Program.PostásNév}_{Rekord.Dolgozószám}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                string MentésiFájl = $@"{könyvtár}\{fájlnév}";
                NyomtatásiFájlok.Clear();

                string munkalap = "Munka1";
                MyX.Munkalap_aktív(munkalap );

                if (Rekord != null)
                {
                    MyX.Kiir(Text.Substring(0, 4), "aa1");
                    MyX.Kiir(darabol[0].Trim(), "aa2");
                    MyX.Kiir(Rekord.Leánykori.Trim(), "aa3");
                    MyX.Kiir(Rekord.Születésihely.Trim(), "aa4");
                    MyX.Kiir(Rekord.Születésiidő.Year.ToString(), "aa5");
                    MyX.Kiir(Rekord.Születésiidő.Month.ToString(), "aa6");
                    MyX.Kiir(Rekord.Születésiidő.Day.ToString(), "aa7");
                    MyX.Kiir(Rekord.Anyja.ToString(), "aa8");
                    MyX.Kiir(Texttelephely.Trim(), "aa9");
                    MyX.Kiir(darabol[1].Trim(), "aa10");
                }
                //Elmentjük a nyomtatáshoz
                MyX.ExcelMentés(MentésiFájl);
                NyomtatásiFájlok.Add(MentésiFájl);

                MyX.ExcelBezárás();
                Holtart.Ki();
                MessageBox.Show("Elkészült az Excel tábla.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.ExcelNyomtatás(NyomtatásiFájlok, true);
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
                Szabiok.Items.Clear();
                Szabiok.BeginUpdate();

                List<Adat_Kiegészítő_Szabadságok> Adatok = KézKiegSzab.Lista_Adatok(CmbTelephely.Text.Trim());
                Adatok = Adatok.Where(a => !a.Megnevezés.ToUpper().Contains("KIVÉTEL")).ToList();
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
            EgyéniTúlNyom.Enabled = false;
            TúlCsopNyom.Enabled = false;
            Túl_Eng_Beáll.Enabled = false;
        }

        private void Túlóraigényelt_Click(object sender, EventArgs e)
        {
            Túlórakiírás(1);
            Túl_Eng_Beáll.Enabled = false;
            EgyéniTúlNyom.Enabled = true;
            TúlCsopNyom.Enabled = true;
        }

        private void Túlóranyomtatott_Click(object sender, EventArgs e)
        {
            Túlórakiírás(1);
            EgyéniTúlNyom.Enabled = true;
            TúlCsopNyom.Enabled = true;
            Túl_Eng_Beáll.Enabled = true;
        }

        private void Túlórarögzített_Click(object sender, EventArgs e)
        {
            Túlórakiírás(1);
            Túl_Eng_Beáll.Enabled = false;
            EgyéniTúlNyom.Enabled = true;
            TúlCsopNyom.Enabled = true;
        }

        private void BtnTúlóraÖsszlekérd_Click(object sender, EventArgs e)
        {
            Túlórakiírás(1);
        }

        private void Túl_Eng_Beáll_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy sor sem.");

                // a státusokat átállítja
                List<double> Sorszámok = new List<double>();
                for (int i = 0; i < Tábla.SelectedRows.Count; i++)
                    Sorszámok.Add(Tábla.SelectedRows[i].Cells[0].Value.ToÉrt_Double());

                KézTúlóra.Státus(CmbTelephely.Text.Trim(), Adat_Évek.Text.ToÉrt_Int(), Sorszámok, 2);

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

                string fájlexc = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Túlóra_{Program.PostásTelephely.Trim()}_{DateTime.Now:yyyyMMddhhmmss}.xlsx";
                string Telephely_ = CmbTelephely.Text.Trim();

                Szatube_TúlCsopNyom TúlCsopNyomtatas = new Szatube_TúlCsopNyom();
                TúlCsopNyomtatas.TúlCsopNyomtat(KézJelenléti, KézFő, KézDolgAlap, Telephely_, fájlexc, Tábla, CheckBox2.Checked);

                // a státusokat átállítja
                List<double> Sorszámok = new List<double>();
                for (int i = 0; i < Tábla.SelectedRows.Count; i++)
                    Sorszámok.Add(Tábla.SelectedRows[i].Cells[0].Value.ToÉrt_Double());

                KézTúlóra.Státus(CmbTelephely.Text.Trim(), Adat_Évek.Text.ToÉrt_Int(), Sorszámok, 1);

                Túlórakiírás(1);
                MessageBox.Show("A kijelölt tételek nyomtatása megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Egyéninyomtatás_más()
        {
            try
            {
                if (Tábla.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kiválasztva érvényes sor a táblázatban.");
                Holtart.Be();

                int AdatEvek = Adat_Évek.Text.ToÉrt_Int();

                Szatube_EgyeniNyomtatas egyeni_nyom = new Szatube_EgyeniNyomtatas();
                egyeni_nyom.EgyeniNyomtatas(CmbTelephely.Text.Trim(), AdatEvek, KézJelenléti, KézFő, KézDolgAlap, KézTúlóra, Tábla);
                Holtart.Lép();
                Túlórakiírás(1);
                MessageBox.Show("A kijelölt tételek nyomtatása megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                if (csoport == 0 && Dolgozónév.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");

                string[] darabol = Dolgozónév.Text.Trim().Split('=');
                List<Adat_Szatube_Túlóra> Adatok = KézTúlóra.Lista_Adatok(CmbTelephely.Text.Trim(), Adat_Évek.Text.ToÉrt_Int());
                if (csoport == 0)
                    Adatok = Adatok.Where(a => a.Törzsszám.Trim() == darabol[1].Trim()).ToList();
                else if (!Túlóramind.Checked) // mind
                {
                    // Igényelt
                    if (Túlóraigényelt.Checked)
                        Adatok = Adatok.Where(a => a.Státus == 0).ToList();
                    // nyomtatott
                    if (Túlóranyomtatott.Checked)
                        Adatok = Adatok.Where(a => a.Státus == 1).ToList();
                    // Rögzített
                    if (Túlórarögzített.Checked)
                        Adatok = Adatok.Where(a => a.Státus == 2).ToList();
                }

                Adatok = Adatok.OrderBy(a => a.Kezdődátum).ToList();
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

                if (csoport == 0 && Dolgozónév.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve dolgozó.");

                string[] darabol = Dolgozónév.Text.Trim().Split('=');

                List<Adat_Szatube_Beteg> Adatok = KézBeteg.Lista_Adatok(CmbTelephely.Text.Trim(), Adat_Évek.Text.ToÉrt_Int());
                if (csoport == 0)
                    Adatok = Adatok.Where(a => a.Törzsszám.Trim() == darabol[1].Trim()).ToList();
                Adatok = Adatok.OrderBy(a => a.Kezdődátum).ToList();

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

                if (csoport == 0 && Dolgozónév.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");

                string[] darabol = Dolgozónév.Text.Trim().Split('=');

                List<Adat_Szatube_Csúsztatás> Adatok = KézCsúsztatás.Lista_Adatok(CmbTelephely.Text.Trim(), Adat_Évek.Text.ToÉrt_Int());
                if (csoport == 0)
                    Adatok = Adatok.Where(a => a.Törzsszám.Trim() == darabol[1].Trim()).ToList();
                Adatok = Adatok.OrderBy(a => a.Kezdődátum).ToList();

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

                if (csoport == 0 && Dolgozónév.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");

                string[] darabol = Dolgozónév.Text.Trim().Split('=');

                List<Adat_Szatube_AFT> Adatok = KézAft.Lista_Adatok(CmbTelephely.Text.Trim(), Adat_Évek.Text.ToÉrt_Int());
                if (csoport == 0)
                    Adatok = Adatok.Where(a => a.Törzsszám.Trim() == darabol[1].Trim()).ToList();
                Adatok = Adatok.OrderBy(a => a.Dátum).ToList();
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
                Holtart.Be();

                List<Adat_Szatube_Szabadság> Adatok = KézSzabadság.Lista_Adatok(CmbTelephely.Text.Trim(), Határnap.Value.Year);
                Adatok = (from a in Adatok
                          where a.Státus == 0 &&
                          a.Sorszám == 0 &&
                          a.Kezdődátum <= Határnap.Value
                          orderby a.Törzsszám, a.Kezdődátum
                          select a).ToList();
                if (Adatok == null || Adatok.Count < 1) throw new HibásBevittAdat("A kijelölt adatokat nem kell összevonni.");
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
                Holtart.Ki();
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Csoportosítja_Elemeket(Adat_Szatube_Szabadság rekord)
        {
            Adatok_Szabadság = KézSzabadság.Lista_Adatok(CmbTelephely.Text.Trim(), Adat_Évek.Text.ToÉrt_Int());

            double sorszám = 1;
            if (Adatok_Szabadság.Count > 0) sorszám = Adatok_Szabadság.Max(a => a.Sorszám) + 1;

            KézSzabadság.Módosítás(CmbTelephely.Text.Trim(), Adat_Évek.Text.ToÉrt_Int(), rekord, sorszám);
        }

        private bool VanKözötte(string Első_HR, DateTime ELőző_Kezdő, DateTime Aktuális)
        {
            bool válasz = false;
            List<Adat_Dolgozó_Beosztás_Új> Adatok = KézBeosztás.Lista_Adatok(CmbTelephely.Text.Trim(), ELőző_Kezdő);
            Adat_Dolgozó_Beosztás_Új Elem = (from a in Adatok
                                             where a.Dolgozószám == Első_HR.Trim() &&
                                             a.Nap > ELőző_Kezdő && a.Nap < Aktuális
                                             select a).FirstOrDefault();

            if (Elem != null) válasz = true;
            return válasz;
        }
        #endregion
    }
}
