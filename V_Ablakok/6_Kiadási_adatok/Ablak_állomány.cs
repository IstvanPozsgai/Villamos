using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Kezelők;
using MyColor = Villamos.V_MindenEgyéb.Kezelő_Szín;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_állomány
    {
        private Button Telephelygomb;
        private int gombokszáma = 0;
        readonly Kezelő_Jármű Kadat = new Kezelő_Jármű();
        readonly Kezelő_Alap_Kiadás KJadat = new Kezelő_Alap_Kiadás();
        readonly Kezelő_Jármű_Vendég KJVAdat = new Kezelő_Jármű_Vendég();
        Adat_Jármű_Vendég Adat;
        string GombNév = "";
        string pályaszám, típus;

        public Ablak_állomány()
        {
            InitializeComponent();
        }

        private void Ablak_állomány_Load(object sender, EventArgs e)
        {
            Telephelyekfeltöltése();
            Jogosultságkiosztás();
            Telephelyeklistázasa();
        }


        private void Ablak_állomány_Shown(object sender, EventArgs e)
        {
            Kocsikiirása_gombok();
        }

        #region Alap
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
            string hely = Application.StartupPath + @"\Súgó\VillamosLapok\állomány.html";
            MyE.Megnyitás(hely);
        }


        private void Telephelyekfeltöltése()
        {
            Cmbtelephely.Items.Clear();
            Cmbtelephely.Items.AddRange(Listák.TelephelyLista_Jármű());

            if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim();
            else
                Cmbtelephely.Text = Program.PostásTelephely;

            Cmbtelephely.Enabled = Program.Postás_Vezér;
        }


        #endregion
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

                string helykieg = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő1.mdb";
                string jelszókieg = "Mocó";
                string szöveg = "SELECT * FROM Típuszínektábla ORDER BY típus";
                Dictionary<string, long> AdatSzín = KJadat.Szótár_TípusSzín(helykieg, jelszókieg, szöveg);

                //Idegen telephely adatok 
                string helyfőm = Application.StartupPath + @"\Főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                szöveg = "SELECT * FROM vendégtábla ORDER BY azonosító";

                Dictionary<string, string> AdatTelep = KJVAdat.Szótár(helyfőm, jelszó, szöveg);

                // Adatok betöltése
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                if (!File.Exists(hely)) return;
                szöveg = "SELECT * FROM Állománytábla order by típus,azonosító";

                int i = 1;
                int j = 1;
                int k = 1;

                List<Adat_Jármű> Adatok = Kadat.Lista_Jármű_állomány(hely, jelszó, szöveg);
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


                        if (AdatSzín.TryGetValue(A.Típus, out long szine))
                        {
                            Szín_kódolás Szín;

                            Szín = MyColor.Szín_váltó(szine);
                            Telephelygomb.BackColor = Color.FromArgb(Szín.Piros, Szín.Zöld, Szín.Kék);
                        }
                        // ha a telephely ki van töltve, akkor más formájú a szöveg
                        if (AdatTelep.TryGetValue(A.Azonosító, out string hova)) Telephelygomb.Visible = true;
                        if (hova == null || hova.Trim() == "")
                            ToolTip1.SetToolTip(Telephelygomb, A.Azonosító.Trim());
                        else
                        {
                            ToolTip1.SetToolTip(Telephelygomb, A.Azonosító.Trim() + "-" + hova);
                            Telephelygomb.Text += "\n-" + hova.Trim();
                            Telephelygomb.Font = new Font("Arial Narrow", 11, FontStyle.Bold);
                            if (AdatSzín.TryGetValue(hova, out long szines))
                            {
                                Szín_kódolás Szín;
                                Szín = MyColor.Szín_váltó(szines);
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
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                if (!File.Exists(hely)) return;


                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Állomány tábla készítés",
                    FileName = "Állomány_tábla" + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytábla  order by típus, azonosító";
                List<Adat_Jármű> Adatok = Kadat.Lista_Jármű_állomány(hely, jelszó, szöveg);

                // megnyitjuk az excelt
                MyE.ExcelLétrehozás();

                int sor = 1;
                Holtart.Be(Adatok.Count + 1);

                MyE.Munkalap_betű("Arial", 12);
                // fejléc kiírása
                MyE.Kiir("Pályaszám", "A1");
                MyE.Kiir("Típus", "B1");

                // tartalom kiírása
                foreach (Adat_Jármű A in Adatok)
                {
                    sor++;
                    MyE.Kiir(A.Azonosító.Trim(), "A" + sor);
                    MyE.Kiir(A.Típus.Trim(), "B" + sor);
                    Holtart.Lép();
                }
                // megformázzuk
                MyE.Rácsoz("A1:B" + sor);
                MyE.Vastagkeret("A1:B1");
                MyE.Vastagkeret("A1:B" + sor);

                //Első sor sárga
                MyE.Háttérszín("A1:B1", Color.Yellow);

                MyE.Szűrés("Munka1", 1, 2, 1);

                // Oszlopok beállítása
                MyE.Oszlopszélesség("Munka1", "A:B");

                //Nyomtatási terület
                MyE.NyomtatásiTerület_részletes("Munka1", "A1:B" + sor, "1:1", "", true);

                MyE.Aktív_Cella("Munka1", "A1");

                MyE.Új_munkalap("Színes");
                MyE.Munkalap_átnevezés("Munka1", "Táblázatos");

                //***************************************************************************************
                MyE.Munkalap_aktív("Színes");

                MyE.Munkalap_betű("Arial", 12);

                // fejlécet kéazítünk
                string munkalap = "Színes";
                MyE.Kiir("Típus", "a1");
                MyE.Egyesít(munkalap, "B1:U1");
                MyE.Kiir("Pályaszámok", "b1");
                MyE.Kiir("Darab", "v1");

                //Színadatok

                string helykieg = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\segéd\Kiegészítő1.mdb";
                string jelszókieg = "Mocó";
                szöveg = "SELECT * FROM Típuszínektábla ORDER BY típus";
                Dictionary<string, long> AdatSzín = KJadat.Szótár_TípusSzín(helykieg, jelszókieg, szöveg);

                //Idegen telephely adatok 
                string helyfőm = Application.StartupPath + @"\Főmérnökség\adatok\villamos.mdb";
                jelszó = "pozsgaii";
                szöveg = "SELECT * FROM vendégtábla ORDER BY azonosító";

                Dictionary<string, string> AdatTelep = KJVAdat.Szótár(helyfőm, jelszó, szöveg);

                int j = 2;
                int k = 2;
                int darab = 0;
                int elsősor = 2;
                string előzőtípus = "";
                string utolsótípus = "";
                Holtart.Be(Adatok.Count + 1);
                foreach (Adat_Jármű A in Adatok)
                {

                    if (előzőtípus.Trim() == null || előzőtípus.Trim() == "")
                        előzőtípus = A.Típus.Trim();

                    if (előzőtípus.Trim() != A.Típus.Trim())
                    {
                        if (elsősor != j)
                        {
                            MyE.Egyesít(munkalap, "a" + elsősor.ToString() + ":a" + j.ToString());
                            MyE.Egyesít(munkalap, "v" + elsősor.ToString() + ":v" + j.ToString());
                            MyE.Kiir(előzőtípus, "a" + elsősor.ToString());
                            MyE.Kiir(darab.ToString(), "v" + elsősor.ToString());
                            darab = 0;
                        }
                        else
                        {
                            MyE.Kiir(előzőtípus, "a" + elsősor.ToString());
                            MyE.Kiir(darab.ToString(), "v" + elsősor.ToString());
                            darab = 0;
                        }
                        k = 2;
                        j += 1;
                        elsősor = j;
                        előzőtípus = A.Típus.Trim();
                    }

                    MyE.Kiir(A.Azonosító.Trim(), MyE.Oszlopnév(k) + j.ToString());
                    if (AdatSzín.TryGetValue(A.Típus, out long szine))
                        MyE.Háttérszín("a" + j.ToString() + ":v" + j.ToString(), szine);


                    k += 1;
                    if (k == 22)
                    {
                        k = 2;
                        j += 1;
                    }
                    darab++;
                    utolsótípus = A.Típus.Trim();
                }
                // az utolsó típus adatai
                if (elsősor != j - 1)
                {
                    MyE.Egyesít(munkalap, "a" + elsősor.ToString() + ":a" + j.ToString());
                    MyE.Egyesít(munkalap, "v" + elsősor.ToString() + ":v" + j.ToString());
                    MyE.Kiir(utolsótípus, "a" + elsősor.ToString());
                    MyE.Kiir(darab.ToString(), "v" + elsősor.ToString());
                    darab = 0;
                }
                else
                {
                    MyE.Kiir(utolsótípus, "a" + elsősor.ToString());
                    MyE.Kiir(darab.ToString(), "v" + elsősor.ToString());
                    darab = 0;
                }

                // formázás
                MyE.Oszlopszélesség(munkalap, "A:V");

                MyE.Rácsoz("A1:V" + j.ToString());
                MyE.NyomtatásiTerület_részletes(munkalap, "A1:V" + j.ToString(), "", "", true);

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


        #region Beviteli lap
        private void Telephelyeklistázasa()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
                string jelszó = "Mocó";
                string szöveg = "SELECT * FROM telephelytábla  order by sorszám";
                Telephely.Items.Clear();
                Telephely.BeginUpdate();
                Telephely.Items.Add("");
                Telephely.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "Telephelynév"));
                Telephely.EndUpdate();
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
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                Bevitelilap.Visible = false;
                Adat = new Adat_Jármű_Vendég(pályaszám, típus, Program.PostásTelephely, Telephely.Text.Trim());

                // töröljük, ha üresre van állítva
                if ((Telephely.Text == null || Telephely.Text.Trim() == "") || (Adat.BázisTelephely.Trim() == Adat.KiadóTelephely.Trim()))
                {
                    KJVAdat.Törlés_Vendég(hely, jelszó, Adat);
                }
                else
                {
                    KJVAdat.Rögzítés_Vendég(hely, jelszó, Adat);
                }

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
