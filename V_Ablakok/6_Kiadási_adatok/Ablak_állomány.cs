using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyColor = Villamos.V_MindenEgyéb.Kezelő_Szín;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_állomány
    {
        private Button Telephelygomb;
        private int gombokszáma = 0;
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Jármű_Vendég KézJárműVendég = new Kezelő_Jármű_Vendég();
        readonly Kezelő_Kiegészítő_Típuszínektábla KézSzínek = new Kezelő_Kiegészítő_Típuszínektábla();
        readonly Kezelő_kiegészítő_telephely KézTelephely = new Kezelő_kiegészítő_telephely();

        Adat_Jármű_Vendég Adat;
        string GombNév = "";
        string pályaszám, típus;


        #region Alap
        public Ablak_állomány()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            try
            {
                //Ha van 0-tól különböző akkor a régi jogosultságkiosztást használjuk
                //ha mind 0 akkor a GombLathatosagKezelo-t használjuk
                if (Program.PostásJogkör.Any(c => c != '0'))
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }
                else
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }
                Kocsikiirása_gombok();
                Telephelyeklistázasa();
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


        private void Ablak_állomány_Load(object sender, EventArgs e)
        {
        }

        private void Ablak_állomány_Shown(object sender, EventArgs e)
        {

        }

        private void Jogosultságkiosztás()
        {
            int melyikelem;
            // ide kell az összes gombot tenni amit szabályozni akarunk false

            Rögzít.Enabled = false;

            melyikelem = 179;
            // módosítás 1 
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Rögzít.Enabled = true;
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
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\állomány.html";
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

        private void Telephelyekfeltöltése()
        {
            Cmbtelephely.Items.Clear();
            foreach (string Elem in Listák.TelephelyLista_Jármű())
                Cmbtelephely.Items.Add(Elem);

            if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim();
            else
                Cmbtelephely.Text = Program.PostásTelephely;

            Cmbtelephely.Enabled = Program.Postás_Vezér;
        }

        private void TelephelyekFeltöltéseÚj()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Adat in GombLathatosagKezelo.Telephelyek(this.Name))
                    Cmbtelephely.Items.Add(Adat.Trim());
                //Alapkönyvtárat beállítjuk 
                Cmbtelephely.Text = Program.PostásTelephely;
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


        #region Megjelenítés
        private void Kocsikiirása_gombok()
        {
            try
            {
                PanelKocsik.Controls.Clear();

                if (gombokszáma != 0)
                {
                    // ha nem nulla akkor előbb a gombokat le kell szedni
                    gombokszáma = 0;
                    PanelKocsik.Controls.Clear();
                }
                //Színadatok
                List<Adat_Kiegészítő_Típuszínektábla> AdatokSzín = KézSzínek.Lista_Adatok(Cmbtelephely.Text.Trim());
                //Idegen telephely adatok 
                List<Adat_Jármű_Vendég> AdatokTelep = KézJárműVendég.Lista_Adatok();
                // Adatok betöltése
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adatok = (from a in Adatok
                          orderby a.Típus, a.Azonosító
                          select a).ToList();
                int i = 1;
                int j = 1;
                int k = 1;

                if (Adatok != null)
                {
                    foreach (Adat_Jármű A in Adatok)
                    {
                        Telephelygomb = new Button
                        {
                            Location = new Point(10 + 85 * (k - 1), 10 + 60 * (j - 1)),
                            Size = new Size(80, 50),
                            Name = "Kocsi_" + (gombokszáma + 1),
                            Text = A.Azonosító.Trim() + "-\n" + A.Típus.Trim()
                        };

                        Adat_Kiegészítő_Típuszínektábla AdatSzín = AdatokSzín.FirstOrDefault(a => a.Típus == A.Típus);
                        if (AdatSzín != null)
                        {
                            Szín_kódolás Szín = MyColor.Szín_váltó(AdatSzín.Színszám);
                            Telephelygomb.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                        }
                        // ha a telephely ki van töltve, akkor más formájú a szöveg
                        Adat_Jármű_Vendég AdatTelep = AdatokTelep.FirstOrDefault(a => a.Azonosító == A.Azonosító);
                        if (AdatTelep != null) Telephelygomb.Visible = true;

                        if (AdatTelep == null)
                            ToolTip1.SetToolTip(Telephelygomb, A.Azonosító.Trim());
                        else
                        {
                            ToolTip1.SetToolTip(Telephelygomb, $"{A.Azonosító.Trim()}-{AdatTelep.KiadóTelephely}");
                            Telephelygomb.Text += $"\n-{AdatTelep.KiadóTelephely}";
                            Telephelygomb.Font = new Font("Arial Narrow", 11, FontStyle.Bold);

                            AdatSzín = AdatokSzín.FirstOrDefault(a => a.Típus == AdatTelep.KiadóTelephely);
                            if (AdatSzín != null)
                            {
                                Szín_kódolás Szín = MyColor.Szín_váltó(AdatSzín.Színszám);
                                Telephelygomb.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                            }
                        }

                        Telephelygomb.MouseDown += Telephelygomb_MouseDown;
                        PanelKocsik.Controls.Add(Telephelygomb);

                        k += 1;
                        if (k == 16)
                        {
                            k = 1;
                            j += 1;
                        }
                        i += 1;
                        gombokszáma += 1;
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

        private void Alap_excel_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                if (AdatokJármű != null && AdatokJármű.Count < 1) return;
                AdatokJármű = (from a in AdatokJármű
                               orderby a.Típus, a.Azonosító
                               select a).ToList();

                //Színadatok
                List<Adat_Kiegészítő_Típuszínektábla> AdatokSzín = KézSzínek.Lista_Adatok(Cmbtelephely.Text.Trim());

                //Idegen telephely adatok 
                List<Adat_Jármű_Vendég> AdatokTelep = KézJárműVendég.Lista_Adatok();

                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Állomány tábla készítés",
                    FileName = $"Állomány_tábla_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                // megnyitjuk az excelt
                MyE.ExcelLétrehozás();

                int sor = 1;
                Holtart.Be(AdatokJármű.Count + 1);

                MyE.Munkalap_betű("Arial", 12);
                // fejléc kiírása
                MyE.Kiir("Pályaszám", "A1");
                MyE.Kiir("Típus", "B1");
                MyE.Kiir("Kiadó telephely", "C1");

                // tartalom kiírása
                foreach (Adat_Jármű A in AdatokJármű)
                {
                    sor++;
                    MyE.Kiir(A.Azonosító.Trim(), $"A{sor}");
                    MyE.Kiir(A.Típus.Trim(), $"B{sor}");
                    Adat_Jármű_Vendég AdatTelep = AdatokTelep.FirstOrDefault(a => a.Azonosító == A.Azonosító);
                    if (AdatTelep != null) MyE.Kiir(AdatTelep.KiadóTelephely.Trim(), $"C{sor}");
                    Holtart.Lép();
                }
                // megformázzuk
                MyE.Rácsoz($"A1:C{sor}");
                MyE.Vastagkeret("A1:C1");
                MyE.Vastagkeret($"A1:C{sor}");

                //Első sor sárga
                MyE.Háttérszín("A1:C1", Color.Yellow);

                MyE.Szűrés("Munka1", 1, 3, 1);

                // Oszlopok beállítása
                MyE.Oszlopszélesség("Munka1", "A:C");

                //Nyomtatási terület
                MyE.NyomtatásiTerület_részletes("Munka1", $"A1:C{sor}", "1:1", "", true);

                MyE.Aktív_Cella("Munka1", "A1");

                MyE.Új_munkalap("Színes");
                MyE.Munkalap_átnevezés("Munka1", "Táblázatos");

                //***************************************************************************************
                MyE.Munkalap_aktív("Színes");
                MyE.Munkalap_betű("Arial", 12);

                // fejlécet kéazítünk
                string munkalap = "Színes";
                MyE.Kiir("Típus", "A1");
                MyE.Egyesít(munkalap, "B1:U1");
                MyE.Kiir("Pályaszámok", "B1");
                MyE.Kiir("Darab", "V1");

                int j = 2;
                int k = 2;
                int darab = 0;
                int elsősor = 2;
                string előzőtípus = "";
                string utolsótípus = "";
                Holtart.Be(AdatokJármű.Count + 1);
                foreach (Adat_Jármű A in AdatokJármű)
                {

                    if (előzőtípus.Trim() == null || előzőtípus.Trim() == "")
                        előzőtípus = A.Típus.Trim();

                    if (előzőtípus.Trim() != A.Típus.Trim())
                    {
                        if (elsősor != j)
                        {
                            MyE.Egyesít(munkalap, $"a{elsősor}:a{j}");
                            MyE.Egyesít(munkalap, $"v{elsősor}:v{j}");
                            MyE.Kiir(előzőtípus, $"a{elsősor}");
                            MyE.Kiir(darab.ToString(), $"v{elsősor}");
                            darab = 0;
                        }
                        else
                        {
                            MyE.Kiir(előzőtípus, $"a{elsősor}");
                            MyE.Kiir(darab.ToString(), $"v{elsősor}");
                            darab = 0;
                        }
                        k = 2;
                        j += 1;
                        elsősor = j;
                        előzőtípus = A.Típus.Trim();
                    }

                    MyE.Kiir(A.Azonosító.Trim(), MyE.Oszlopnév(k) + $"{j}");
                    Adat_Kiegészítő_Típuszínektábla AdatSzín = AdatokSzín.FirstOrDefault(a => a.Típus == A.Típus);
                    if (AdatSzín != null)
                    {
                        double szine = AdatSzín.Színszám;
                        MyE.Háttérszín(MyE.Oszlopnév(k) + $"{j}", szine);
                    }
                    Adat_Jármű_Vendég AdatTelep = AdatokTelep.FirstOrDefault(a => a.Azonosító == A.Azonosító);
                    if (AdatTelep != null)
                    {
                        AdatSzín = AdatokSzín.FirstOrDefault(a => a.Típus == AdatTelep.KiadóTelephely);
                        if (AdatSzín != null)
                        {
                            double szine = AdatSzín.Színszám;
                            MyE.Háttérszín(MyE.Oszlopnév(k) + $"{j}", szine);
                        }
                    }

                    k += 1;
                    if (k == 22)
                    {
                        k = 2;
                        j += 1;
                    }
                    darab++;
                    utolsótípus = A.Típus.Trim();
                    Holtart.Lép();
                }
                // az utolsó típus adatai
                if (elsősor != j - 1)
                {
                    MyE.Egyesít(munkalap, $"a{elsősor}:a{j}");
                    MyE.Egyesít(munkalap, $"v{elsősor}:v{j}");
                    MyE.Kiir(utolsótípus, $"a{elsősor}");
                    MyE.Kiir(darab.ToString(), $"v{elsősor}");
                    darab = 0;
                }
                else
                {
                    MyE.Kiir(utolsótípus, $"a{elsősor}");
                    MyE.Kiir(darab.ToString(), $"v{elsősor}");
                    darab = 0;
                }

                // formázás
                MyE.Oszlopszélesség(munkalap, "A:V");

                MyE.Rácsoz($"A1:V{j}");
                MyE.NyomtatásiTerület_részletes(munkalap, $"A1:V{j}", "", "", true);

                MyE.Aktív_Cella(munkalap, "A1");

                MyE.Munkalap_aktív("Táblázatos");


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

        private void Button1_Click(object sender, EventArgs e)
        {
            Kocsikiirása_gombok();
        }
        #endregion


        #region Beviteli lap
        private void Telephelyeklistázasa()
        {
            try
            {
                List<Adat_kiegészítő_telephely> Adatok = KézTelephely.Lista_Adatok();

                Telephely.Items.Clear();
                Telephely.Items.Add("");

                foreach (Adat_kiegészítő_telephely rekord in Adatok)
                    Telephely.Items.Add(rekord.Telephelynév);

                Telephely.Refresh();
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

        private void Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                Bevitelilap.Visible = false;
                Adat = new Adat_Jármű_Vendég(pályaszám, típus, Program.PostásTelephely, Telephely.Text.Trim());

                // töröljük, ha üresre van állítva
                if ((Telephely.Text == null || Telephely.Text.Trim() == "") || (Adat.BázisTelephely.Trim() == Adat.KiadóTelephely.Trim()))
                    KézJárműVendég.Törlés(Adat);
                else
                    KézJárműVendég.Rögzítés(Adat);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Kocsikiirása_gombok();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Telephelygomb_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Bevitelilap.Visible = false;
                string[] név = (sender as Button).Text.Split('-');
                pályaszám = név[0].ToString();
                típus = név[1].ToString();

                string Gombfelirat = (sender as Button).Text;
                GombNév = (sender as Button).Name;


                // ha bal egérgomb
                if (e.Button == MouseButtons.Left)
                {
                    Bevitelilap.Left = PanelKocsik.Controls[GombNév].Left + PanelKocsik.Left;
                    Bevitelilap.Top = PanelKocsik.Controls[GombNév].Top + PanelKocsik.Top;
                    Label4.Text = pályaszám.Trim();

                    Bevitelilap.Refresh();
                    Bevitelilap.Visible = true;
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

        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
                if (Cmbtelephely.Text.Trim() == "") return;
                if (Program.PostásJogkör.Any(c => c != '0'))
                {

                }
                else
                {
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
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

        private void Ablak_állomány_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                Bevitelilap.Visible = false;
            }

        }
        #endregion
    }
}
