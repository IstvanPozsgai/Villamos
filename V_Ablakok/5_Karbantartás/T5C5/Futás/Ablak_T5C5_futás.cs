using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;

using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;
using T5C5 = Villamos.T5C5_Funkciók;

namespace Villamos
{
    public partial class Ablak_T5C5_futás
    {
        readonly Kezelő_T5C5_Futás Kéz_Futás = new Kezelő_T5C5_Futás();
        readonly Kezelő_Jármű Kéz_Jármű = new Kezelő_Jármű();
        readonly Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();

        bool CTRL_le = false;
        private string FájlExcel_;
        private int Gombok_száma = 0;
        private int Utolsó_Gomb = 0;
        string GombNév = "";
        string Gombfelirat = "";

        List<string> Pályaszám = new List<string>();
        List<Adat_T5C5_Göngyöl_DátumTábla> AdatokGöngyöl = new List<Adat_T5C5_Göngyöl_DátumTábla>();
        List<Adat_T5C5_Futás> AdatokFutás = new List<Adat_T5C5_Futás>();


        public Ablak_T5C5_futás()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Telephelyekfeltöltése();
            Jogosultságkiosztás();
            Pályaszám = Pályaszám_feltöltés();
        }

        private void Ablak_T5C5_futás_Load(object sender, EventArgs e)
        {
            Combo_feltöltés();
            Táblaellenőrzés();

            // létrehozzuk a gyűjtő adatbázist


            Dátum.MaxDate = DateTime.Today;
            Dátum.Value = DateTime.Today;

            Gombok_vezérlése();
        }



        #region Alap
        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
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

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Napadatai.Enabled = false;
                Zserbeolvasás.Enabled = false;
                Zseradategyeztetés.Enabled = false;
                Rögzít.Enabled = false;



                melyikelem = 101;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Napadatai.Enabled = true;
                    Zserbeolvasás.Enabled = true;
                    Zseradategyeztetés.Enabled = true;
                    Rögzít.Enabled = true;
                }

                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                { }

                // módosítás 3
                if (MyF.Vanjoga(melyikelem, 3))
                { }
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
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\T5C5_futás.html";
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

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Pályaszám.Clear();
                Pályaszám = Pályaszám_feltöltés();
                if (Gombok_száma != 0)
                {
                    // ha nem nulla akkor előbb a gombokat le kell szedni
                    Panel3.Controls.Clear();
                    Gombok_száma = 0;
                }
                Bevitelilap.Visible = false;
                Gombok_vezérlése();
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

        private void Ablak_T5C5_futás_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                Bevitelilap.Visible = false;
            }
            // F1 utolsó gombeltávolítás visszavonása
            if ((int)e.KeyCode == 112)
            {
                if (Utolsó_Gomb != 0)
                {
                    Panel3.Controls[Utolsó_Gomb].Visible = true;
                    Utolsó_Gomb = 0;
                }
            }
            if (e.Control)
                CTRL_le = true;
        }
        #endregion


        #region Segédablak
        private void Combo_feltöltés()
        {
            // combo feltöltése adatokkal
            Kategória.Items.Clear();
            Kategória.Items.Add("Forgalomban");
            Kategória.Items.Add("Hibás");
            Kategória.Items.Add("E3");
            Kategória.Items.Add("V1");
            Kategória.Items.Add("V2");
            Kategória.Items.Add("V3");
            Kategória.Items.Add("J1");
            Kategória.Items.Add("-");
            Kategória.Refresh();
        }

        #endregion


        #region háttér folyamatok
        //
        private void Táblaellenőrzés()
        {
            string jelszó = "pozsgaii";

            string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\T5C5\{Dátum.Value.Year}";
            if (!Directory.Exists(hely))
                System.IO.Directory.CreateDirectory(hely);

            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\T5C5\{Dátum.Value.Year}\havi{Dátum.Value:yyyyMM}.mdb";
            if (!File.Exists(hely))
            {
                //Ha nincs létrehozza az adattáblát
                Adatbázis_Létrehozás.Havifutástábla_Létrehozás(hely);

                //új hónap esetén a psz-okat feltölti
                Hónapkezdés();

                //előző hónap ha van akkor kiolvassa a az utolsó rögzített napot, ha nincs akkor az előző hónap utolsó napja
                DateTime Előzőhónap = Dátum.Value.AddMonths(-1);

                string helyhova = $@"{Application.StartupPath}\Főmérnökség\Adatok\T5C5\{Előzőhónap.Year}\havi{Előzőhónap:yyyyMM}.mdb";
                string szöveg = "SELECT * FROM Állománytábla";
                Kezelő_T5C5_Havi_Nap Fut_kéz = new Kezelő_T5C5_Havi_Nap();
                List<Adat_T5C5_Havi_Nap> Fut_Adatok = Fut_kéz.Lista_Adat(helyhova, jelszó, szöveg);

                if (File.Exists(helyhova))
                {
                    //megnézzük, hogy mi az utolsó rögzített adat
                    Holtart.Be(Pályaszám.Count + 1);


                    List<string> szövegGy = new List<string>();
                    foreach (string elem in Pályaszám)
                    {
                        Adat_T5C5_Havi_Nap Rekord = (from a in Fut_Adatok
                                                     where a.Azonosító == elem.Trim()
                                                     select a).FirstOrDefault();
                        if (Rekord != null)
                        {
                            szöveg = $"UPDATE Állománytábla SET futásnap={Rekord.Futásnap} WHERE azonosító='{elem}'";
                            szövegGy.Add(szöveg);
                        }
                        Holtart.Lép();

                    }
                    MyA.ABMódosítás(hely, jelszó, szövegGy);
                    Holtart.Ki();
                }
            }
        }
        //
        private void Táblaellenőrzés1()
        {
            try
            {
                // elkészítjük az alaptábla adatait

                Holtart.Be(Pályaszám.Count + 1);


                List<string> szövegGy = new List<string>();
                foreach (string elem in Pályaszám)
                {
                    string szöveg = "INSERT INTO Állománytábla (azonosító, utolsórögzítés, vizsgálatdátuma, Vizsgálatfokozata, vizsgálatszáma, futásnap, telephely, utolsóforgalminap ) VALUES (";
                    szöveg += $@"'{elem.Trim()}', '1900.01.01', '1900.01.01', '.', 0,  0, '.', '1900.01.01')";
                    szövegGy.Add(szöveg);
                    Holtart.Lép();

                }

                string jelszó = "pozsgaii";
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\T5C5\villamos3.mdb";
                MyA.ABMódosítás(hely, jelszó, szövegGy);
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
        //
        private void Hónapkezdés()
        {
            try
            {
                // elkészítjük az alaptábla adatait
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\T5C5\{Dátum.Value.Year}\havi{Dátum.Value:yyyyMM}.mdb";
                string jelszó = "pozsgaii";

                Holtart.Be(Pályaszám.Count + 1);

                List<string> szövegGy = new List<string>();
                foreach (string elem in Pályaszám)
                {
                    string szöveg = "INSERT INTO Állománytábla (azonosító, N1, N2, N3, N4, N5, N6, N7, N8, N9, N10, N11, N12, N13, N14, N15, N16, N17, N18, N19, N20, ";
                    szöveg += "N21, N22, N23, N24, N25, N26, N27, N28, N29, N30, N31, futásnap ) VALUES (";
                    szöveg += $"'{elem.Trim()}', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.',";
                    szöveg += " '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', 0)";
                    szövegGy.Add(szöveg);
                    Holtart.Lép();

                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);
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

        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            try
            {

                if (Gombok_száma != 0)
                {
                    // ha nem nulla akkor előbb a gombokat le kell szedni
                    Panel3.Controls.Clear();
                    Gombok_száma = 0;
                }
                Bevitelilap.Visible = false;
                Gombok_vezérlése();
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

        private void Gombok_vezérlése()
        {
            try
            {
                string hely = Fájlnév_meghatározás();
                if (!File.Exists(hely))
                {
                    Lista.Visible = false;
                    Napkinyitása.Visible = false;
                    Zseradategyeztetés.Visible = false;
                    Zserbeolvasás.Visible = false;
                    Naplezárása.Visible = false;
                    Göngyölés.Visible = false;
                    Vissza.Visible = false;
                    Napadatai.Visible = true;
                }
                else
                {
                    Lista.Visible = true;
                    Napkinyitása.Visible = false;
                    Zseradategyeztetés.Visible = true;
                    Zserbeolvasás.Visible = true;
                    Naplezárása.Visible = true;
                    Napadatai.Visible = true;
                    // ha létezik,akkor megnézzük, hogy le van-e zárva

                    Adat_T5C5_Futás1 Elem = NapÁllapot();
                    if (Elem.Státus == 1)
                    {
                        // minden gomb inaktív kivéve visszanyit
                        Napadatai.Visible = false;
                        Zseradategyeztetés.Visible = false;
                        Zserbeolvasás.Visible = false;
                        Naplezárása.Visible = false;
                        Napkinyitása.Visible = true;
                        Göngyölés.Visible = true;
                        Vissza.Visible = true;
                    }
                    else
                    {
                        Napadatai.Visible = true;
                        Zseradategyeztetés.Visible = true;
                        Zserbeolvasás.Visible = true;
                        Naplezárása.Visible = true;
                        Napkinyitása.Visible = false;
                        Göngyölés.Visible = false;
                        Vissza.Visible = false;
                    }
                    // gönygyölés csak akkor aktív ha a soron következő nap
                    AdatokGöngyöl = T5C5.AdatokGöngyöl_feltöltése();

                    Adat_T5C5_Göngyöl_DátumTábla EgyTelep = (from a in AdatokGöngyöl
                                                             where a.Telephely == Cmbtelephely.Text.Trim()
                                                             select a).FirstOrDefault();

                    if (EgyTelep != null)
                    {
                        // 'ha van
                        if (EgyTelep.Utolsórögzítés.AddDays(1) == Dátum.Value)
                        {
                            Göngyölés.Enabled = true;
                            Napkinyitása.Enabled = true;
                            Göngyölés.BackColor = Color.Green;
                            Napkinyitása.BackColor = Color.Green;
                        }
                        else
                        {
                            Göngyölés.Enabled = false;
                            Napkinyitása.Enabled = false;
                            Göngyölés.BackColor = Color.Red;
                            Napkinyitása.BackColor = Color.Red;
                        }

                        if (EgyTelep.Utolsórögzítés == Dátum.Value)
                        {
                            Vissza.Enabled = true;
                            Vissza.BackColor = Color.Green;
                        }
                        else
                        {
                            Vissza.Enabled = false;
                            Vissza.BackColor = Color.Red;
                        }

                    }
                    else
                    {
                        Göngyölés.Enabled = false;
                        Göngyölés.BackColor = Color.Red;
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
        #endregion


        #region Adat olvasó Gombok
        private void Lista_Click(object sender, EventArgs e)
        {
            Kocsikiirása();
        }

        //
        private void Kocsikiirása()
        {
            try
            {
                Panel3.Controls.Clear();
                // ha nem nulla akkor előbb a gombokat le kell szedni
                if (Gombok_száma != 0) Gombok_száma = 0;
                int darab = 0;
                //int darab = Panel3.Controls.OfType<TextBox>().ToList().Count;
                string hely = Fájlnév_meghatározás();

                if (!File.Exists(hely))
                    Napadatai_eseménye();

                // ismét meg kell határozni, mert ha létrehoz elfelejti
                hely = Fájlnév_meghatározás();

                string szöveg = "SELECT * FROM futástábla order by azonosító";
                string jelszó = "lilaakác";

                int i = 1;
                int j = 1;
                int k = 1;


                AdatokFutás = Kéz_Futás.Lista_Adat(hely, jelszó, szöveg);

                if (AdatokFutás.Count > 0)
                {
                    foreach (Adat_T5C5_Futás rekord in AdatokFutás)
                    {
                        Button Telephelygomb = new Button()
                        {
                            Location = new Point(10 + 80 * (k - 1), 10 + 40 * (j - 1)),
                            Size = new Size(70, 30),
                            Name = "Kocsi_" + (darab + 1),
                            Text = rekord.Azonosító.Trim()
                        };


                        switch (rekord.Futásstátus.Trim())
                        {
                            case "Forgalomban":
                                {
                                    // zöld
                                    Telephelygomb.BackColor = Color.LimeGreen;
                                    break;
                                }
                            case "-":
                                {
                                    // szürke
                                    Telephelygomb.BackColor = Color.Silver;
                                    break;
                                }
                            case "_":
                                {
                                    // szürke
                                    Telephelygomb.BackColor = Color.Silver;
                                    break;
                                }
                            case "E3":
                                {
                                    // kék
                                    Telephelygomb.BackColor = Color.Blue;
                                    Telephelygomb.ForeColor = Color.White;
                                    break;
                                }
                            case "V1":
                                {
                                    // sárga
                                    Telephelygomb.BackColor = Color.Yellow;
                                    break;
                                }
                            case "V2":
                                {
                                    // narancssárga
                                    Telephelygomb.BackColor = Color.DarkOrange;
                                    break;
                                }
                            case "V3":
                                {
                                    // narancssárga
                                    Telephelygomb.BackColor = Color.DarkOrange;
                                    break;
                                }
                            case "J1":
                                {
                                    // narancssárga
                                    Telephelygomb.BackColor = Color.DarkOrange;
                                    break;
                                }
                            case "J2":
                                {
                                    // narancssárga
                                    Telephelygomb.BackColor = Color.DarkOrange;
                                    break;
                                }
                            case "Hibás":
                                {
                                    // piros
                                    Telephelygomb.BackColor = Color.Red;
                                    Telephelygomb.ForeColor = Color.White;
                                    break;
                                }
                        }
                        Telephelygomb.Visible = true;
                        ToolTip1.SetToolTip(Telephelygomb, rekord.Futásstátus.Trim());

                        // AddHandler Telephelygomb.Click, AddressOf Telephelyre_Click
                        Telephelygomb.MouseDown += Telephelyre_MouseDown;

                        Panel3.Controls.Add(Telephelygomb);
                        Gombok_száma = i;

                        k += 1;
                        if (k == 11)
                        {
                            k = 1;
                            j += 1;
                        }
                        i += 1;
                        darab += 1;
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

        private void Napadatai_Click(object sender, EventArgs e)
        {
            Napadatai_eseménye();
            MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //
        private void Napadatai_eseménye()
        {
            try
            {
                // elkészítjük az alaptáblát
                Konvertálás();

                // megnézzük, hogy készült-e az adott nap főkönyv ha készült akkor abból vesszük  át
                //ha van délután akkor abból olvassa be
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text}\Adatok\főkönyv\{Dátum.Value.Year}\Nap\{Dátum.Value:yyyyMMdd}dunap.mdb";
                if (File.Exists(hely))
                    Adatáttöltés(hely);
                else
                {
                    //Han nincs akkor a délelőttivel próbálkozik
                    hely = $@"{Application.StartupPath}\{Cmbtelephely.Text}\Adatok\főkönyv\{Dátum.Value.Year}\Nap\{Dátum.Value:yyyyMMdd}denap.mdb";
                    if (File.Exists(hely)) Adatáttöltés(hely);
                }

                Gombok_vezérlése();
                Napkinyitása.Visible = false;
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
        //
        private void Konvertálás()
        {
            try
            {
                // létrehozzuk a napi adatokat
                string hely = Fájlnév_meghatározás();

                if (!File.Exists(hely))
                {
                    Adatbázis_Létrehozás.Futásnapalap(hely);
                }
                // ha létezik akkor töröljük
                else
                {
                    if (MessageBox.Show("Már van az adott napra feltöltve adat ! Módosítjuk az adatokat ?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        File.Delete(hely);
                        Adatbázis_Létrehozás.Futásnapalap(hely);
                    }
                    else
                    {
                        return;
                    }
                }

                // Kitöltjük az aktuális kocsilistával
                string honnan = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                string jelszó = "pozsgaii";

                string jelszó1 = "lilaakác";
                // beemeljük az adatokat
                string szöveg = $"Select * FROM Állománytábla WHERE Üzem='{Cmbtelephely.Text.Trim()}' AND ";
                szöveg += " törölt=0 AND típus Like '%T5C5%' ORDER BY azonosító";

                Kezelő_Jármű kéz = new Kezelő_Jármű();
                List<Adat_Jármű> Adatok = kéz.Lista_Adatok(honnan, jelszó, szöveg);

                Holtart.Be(Adatok.Count + 1);

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Jármű rekord in Adatok)
                {
                    // rögzítjük a kocsikat
                    szöveg = "INSERT INTO Futástábla (azonosító, Dátum, Futásstátus, Státus) VALUES ( ";
                    szöveg += $"'{rekord.Azonosító.Trim()}', '{Dátum.Value:yyyy.MM.dd}', '-', {rekord.Státus})";
                    SzövegGy.Add(szöveg);
                    Holtart.Lép();
                }
                szöveg = "INSERT INTO Futástábla1 (Státus) VALUES (0)";
                SzövegGy.Add(szöveg);
                MyA.ABMódosítás(hely, jelszó1, SzövegGy);

                // hibás kocsikat hibaszövegét feldolgozzuk

                Holtart.Be(Pályaszám.Count + 1);

                List<Adat_Jármű_hiba> HibaAdat = KézHiba.Lista_Adatok(Cmbtelephely.Text.Trim());

                SzövegGy.Clear();
                foreach (string elem in Pályaszám)
                {
                    Holtart.Lép();
                    List<Adat_Jármű_hiba> PályaszámRész = (from a in HibaAdat
                                                           where a.Azonosító == elem
                                                           select a).ToList();

                    if (PályaszámRész.Count > 0)
                    {
                        string hibaszöveg = "";
                        int volt = 0;
                        foreach (Adat_Jármű_hiba rekordhiba in PályaszámRész)
                            hibaszöveg += rekordhiba.Hibaleírása.Trim();

                        hibaszöveg = hibaszöveg.ToUpper();
                        // hibák elemzése
                        if (hibaszöveg.Contains("E3"))
                        {
                            szöveg = $"UPDATE Futástábla SET Futásstátus='E3' WHERE azonosító='{elem.Trim()}'";
                            volt = 1;
                        }
                        if (hibaszöveg.Contains("V1"))
                        {
                            szöveg = $"UPDATE Futástábla SET Futásstátus='V1' WHERE azonosító='{elem.Trim()}'";
                            volt = 1;
                        }
                        if (hibaszöveg.Contains("V2"))
                        {
                            szöveg = $"UPDATE Futástábla SET Futásstátus='V2' WHERE azonosító='{elem.Trim()}'";
                            volt = 1;
                        }
                        if (hibaszöveg.Contains("V3"))
                        {
                            szöveg = $"UPDATE Futástábla SET Futásstátus='V3' WHERE azonosító='{elem.Trim()}'";
                            volt = 1;
                        }
                        if (hibaszöveg.Contains("J1"))
                        {
                            szöveg = $"UPDATE Futástábla SET Futásstátus='J2' WHERE azonosító='{elem.Trim()}'";
                            volt = 1;
                        }
                        if (hibaszöveg.Contains("J2"))
                        {
                            szöveg = $"UPDATE Futástábla SET Futásstátus='J2' WHERE azonosító='{elem.Trim()}'";
                            volt = 1;
                        }
                        if (volt == 0 && hibaszöveg.Trim() != "")
                        {
                            szöveg = $"UPDATE Futástábla SET Futásstátus='Hibás' WHERE azonosító='{elem.Trim()}'";
                            volt = 1;
                        }
                        if (volt == 1) SzövegGy.Add(szöveg);
                    }
                }
                MyA.ABMódosítás(hely, jelszó1, SzövegGy);

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
        //
        private void Adatáttöltés(string Honnan)
        {
            try
            {
                // létrehozzuk a napi adatokat
                string hely = Fájlnév_meghatározás();
                string jelszó = "lilaakác";

                Kocsikiirása();
                Panel3.Refresh();

                Holtart.Be(Panel3.Controls.Count + 1);
                string szöveg = "SELECT * FROM adattábla";

                Kezelő_Főkönyv_Nap kéz = new Kezelő_Főkönyv_Nap();
                List<Adat_Főkönyv_Nap> Adatok = kéz.Lista_adatok(Honnan, jelszó, szöveg);
                List<string> szövegGy = new List<string>();
                for (int i = 0; i < Panel3.Controls.Count; i++)
                {
                    Holtart.Lép();
                    Adat_Főkönyv_Nap rekord = (from a in Adatok
                                               where a.Azonosító == Panel3.Controls[i].Text.Trim()
                                               select a).FirstOrDefault();

                    string Futásstátus = "-";
                    long jármű_státus = 0;
                    if (rekord != null)
                    {
                        jármű_státus = rekord.Státus;
                        if (rekord.Státus == 4)
                            Futásstátus = "Hibás";
                        if (rekord.Viszonylat.Trim() != "-")
                            Futásstátus = "Forgalomban";
                        if (rekord.Hibaleírása.ToUpper().Contains("E3"))
                            Futásstátus = "E3";
                        if (rekord.Hibaleírása.ToUpper().Contains("V1"))
                            Futásstátus = "V1";
                        if (rekord.Hibaleírása.ToUpper().Contains("V2"))
                            Futásstátus = "V2";
                        if (rekord.Hibaleírása.ToUpper().Contains("V3"))
                            Futásstátus = "V3";
                        if (rekord.Hibaleírása.ToUpper().Contains("#J"))
                            Futásstátus = "J";
                    }

                    szöveg = "UPDATE futástábla SET ";
                    szöveg += $" dátum='{Dátum.Value:yyyy.MM.dd}', ";
                    szöveg += $" Futásstátus='{Futásstátus}', ";
                    szöveg += $" státus={jármű_státus} ";
                    szöveg += $" WHERE azonosító='{Panel3.Controls[i].Text.Trim()}'";
                    szövegGy.Add(szöveg);

                    Panel3.Controls[i].Visible = false;
                    Panel3.Refresh();
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);
                Holtart.Ki();
                MessageBox.Show("Az adat konvertálás befejeződött!", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Kocsikiirása();
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


        #region Adatrögzítés
        private void Telephelyre_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                Rögzít.Visible = !Napkinyitása.Visible;
                Bevitelilap.Visible = false;
                GombNév = (sender as Button).Name;
                Gombfelirat = (sender as Button).Text;
                string[] név = GombNév.Split('_');
                Utolsó_Gomb = int.Parse(név[1]) - 1;

                // jobb egér gomb
                if (e.Button == MouseButtons.Right) Panel3.Controls[Utolsó_Gomb].Visible = false;

                // ha bal egérgomb
                if (e.Button == MouseButtons.Left)
                {
                    Bevitelilap.Left = Panel3.Controls[Utolsó_Gomb].Left + Panel3.Left;
                    Bevitelilap.Top = Panel3.Controls[Utolsó_Gomb].Top + Panel3.Top;
                    Label4.Text = Gombfelirat;

                    Adat_T5C5_Futás EgyElem = (from a in AdatokFutás
                                               where a.Azonosító == Gombfelirat
                                               select a).FirstOrDefault();

                    if (EgyElem != null) Kategória.Text = EgyElem.Futásstátus;

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
        //
        private void Rögzít_Click(object sender, EventArgs e)
        {
            if (Kategória.Text.Trim() == "") return;
            if (Label4.Text.Trim() == "") return;

            string hely = Fájlnév_meghatározás();
            string jelszó = "lilaakác";
            string szöveg = $"UPDATE futástábla  SET  futásstátus='{Kategória.Text.Trim()}' WHERE azonosító='{Label4.Text.Trim()}'";

            MyA.ABMódosítás(hely, jelszó, szöveg);

            Gombok_Színezése(Utolsó_Gomb, Kategória.Text.Trim());

            Bevitelilap.Visible = false;
        }
        #endregion


        #region Göngyölés gönygyölés
        //
        private void Göngyölés_Click(object sender, EventArgs e)
        {
            try
            {
                AdatokGöngyöl = T5C5.AdatokGöngyöl_feltöltése();
                if (AdatokGöngyöl == null) return;
                T5C5.Zároljuk(AdatokGöngyöl, Cmbtelephely.Text.Trim());

                // a pályaszámokat ellenőrizzük
                Pályaszám_ellenőrzés();

                // A GÖNGYÖLÉS ELŐTTI ÁLLAPOTOT rögzíjük egy napi táblába
                string honnan = Application.StartupPath + @"\Főmérnökség\adatok\T5C5\villamos3.mdb";
                string hova = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\futás\{Dátum.Value.Year}";
                hova += $@"\Villamos3-{Dátum.Value.AddDays(-1):yyyyMMdd}.mdb";
                if (!File.Exists(hova)) File.Copy(honnan, hova);

                Telepalaphelyzetbe(hova);
                TelepBeállítás(hova);
                Göngyöl();
                MessageBox.Show("Az adatok gönygyölése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            finally
            {
                T5C5.Kinyitjuk(Cmbtelephely.Text.Trim());
            }
        }
        //
        private void TelepBeállítás(string hova)
        {
            try
            {
                // telephely adataiban csak a telephelyen lévő kocsik szerepelnek
                string hely = Fájlnév_meghatározás();
                string jelszó = "lilaakác";

                string szöveg = "SELECT * FROM futástábla order by azonosító";
                Kezelő_T5C5_Futás Fut_kéz = new Kezelő_T5C5_Futás();
                List<Adat_T5C5_Futás> Fut_Adatok = Fut_kéz.Lista_Adat(hely, jelszó, szöveg);
                Holtart.Be(Fut_Adatok.Count + 1);

                List<string> szövegGy = new List<string>();
                foreach (Adat_T5C5_Futás rekord in Fut_Adatok)
                {
                    szöveg = $"UPDATE állománytábla SET telephely='{Cmbtelephely.Text.Trim()}' WHERE [azonosító]='{rekord.Azonosító.Trim()}'";
                    szövegGy.Add(szöveg);
                    Holtart.Lép();
                }
                string jelszóold = "pozsgaii";
                MyA.ABMódosítás(hova, jelszóold, szövegGy);
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
        //
        private void Telepalaphelyzetbe(string hova)
        {
            try
            {    // minden kocsi telephelyét beállítjuk üresnek
                string szöveg = "SELECT * FROM állománytábla order by azonosító";
                string jelszó = "pozsgaii";

                Kezelő_T5C5_Állomány kéz = new Kezelő_T5C5_Állomány();
                List<Adat_T5C5_Állomány> Adatok = kéz.Lista_Adat(hova, jelszó, szöveg);

                Holtart.Be();

                List<string> szövegGy = new List<string>();
                foreach (Adat_T5C5_Állomány rekord in Adatok)
                {
                    szöveg = $"UPDATE állománytábla SET telephely='_' WHERE [azonosító] ='{rekord.Azonosító.Trim()}'";
                    szövegGy.Add(szöveg);
                    Holtart.Lép();
                }
                MyA.ABMódosítás(hova, jelszó, szövegGy);
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
        //
        private void Pályaszám_ellenőrzés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\T5C5\villamos3.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytábla ORDER BY azonosító";

                Kezelő_T5C5_Állomány kéz = new Kezelő_T5C5_Állomány();
                List<Adat_T5C5_Állomány> Adatok = kéz.Lista_Adat(hely, jelszó, szöveg);

                string előző = "";
                string előzőtelep = "";
                string szövegmásol;
                string szövegtöröl;

                List<string> Töröl = new List<string>();
                List<string> Másol = new List<string>();
                foreach (Adat_T5C5_Állomány rekord in Adatok)
                {
                    if (előző.Trim() == rekord.Azonosító.Trim() && előzőtelep.Trim() == rekord.Telephely.Trim())
                    {
                        // ha egyforma akkor töröljük
                        szövegmásol = "INSERT INTO állománytábla (azonosító, utolsórögzítés, vizsgálatdátuma, utolsóforgalminap, Vizsgálatfokozata, vizsgálatszáma, futásnap, telephely  ) VALUES (";
                        szövegmásol += $"'{rekord.Azonosító}', ";                       // azonosító
                        szövegmásol += $"'{rekord.Utolsórögzítés:yyyy.MM.dd}', ";       // utolsórögzítés
                        szövegmásol += $"'{rekord.Vizsgálatdátuma:yyyy.MM.dd}', ";      // vizsgálatdátuma
                        szövegmásol += $"'{rekord.Utolsóforgalminap:yyyy.MM.dd}', ";    // utolsóforgalminap
                        szövegmásol += $"'{rekord.Vizsgálatfokozata}', ";               // Vizsgálatfokozata
                        szövegmásol += $"{rekord.Vizsgálatszáma}, ";                    // vizsgálatszáma
                        szövegmásol += $"{rekord.Futásnap}, ";                          // futásnap
                        szövegmásol += $"'{rekord.Telephely}')";                        // telephely
                        Másol.Add(szövegmásol);

                        szövegtöröl = $"DELETE FROM állománytábla WHERE [azonosító]='{rekord.Azonosító.Trim()}'";
                        Töröl.Add(szövegtöröl);
                    }
                    else
                    {
                        előző = rekord.Azonosító.Trim();
                        előzőtelep = rekord.Telephely.Trim();
                    }
                }
                MyA.ABtörlés(hely, jelszó, Töröl);
                MyA.ABMódosítás(hely, jelszó, Másol);
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
        //
        private void Göngyöl()
        {
            try
            {
                // a napi adatokat pörgetjük végig
                string helynapi = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\futás\{Dátum.Value.Year}\futás{Dátum.Value:yyyyMMdd}nap.mdb";
                string jelszónapi = "lilaakác";
                string szöveg = "Select * FROM futástábla order by azonosító";
                Kezelő_T5C5_Futás Kéz_Fut = new Kezelő_T5C5_Futás();
                List<Adat_T5C5_Futás> AdatokNapi = Kéz_Fut.Lista_Adat(helynapi, jelszónapi, szöveg);

                //a havi adatokat betöltése
                string helyHavi = $@"{Application.StartupPath}\Főmérnökség\adatok\T5C5\{Dátum.Value.Year}\Havi{Dátum.Value:yyyyMM}.mdb";
                string jelszóhavi = "pozsgaii";
                szöveg = "SELECT * FROM állománytábla";
                Kezelő_T5C5_Havi_Nap KézHavi = new Kezelő_T5C5_Havi_Nap();
                List<Adat_T5C5_Havi_Nap> AdatokHavi = KézHavi.Lista_Adat(helyHavi, jelszóhavi, szöveg);

                //bázis adatokat betölti
                string helyBázis = $@"{Application.StartupPath}\Főmérnökség\adatok\T5C5\Villamos3.mdb";
                string jelszóBázis = "pozsgaii";
                szöveg = "SELECT * FROM állománytábla";
                Kezelő_T5C5_Állomány kéz = new Kezelő_T5C5_Állomány();
                List<Adat_T5C5_Állomány> AdatokBázis = kéz.Lista_Adat(helyBázis, jelszóBázis, szöveg);

                //Telephelyi göngyölés
                szöveg = $"SELECT * FROM dátumtábla";
                Kezelő_T5C5_Göngyöl_DátumTábla KézDátum = new Kezelő_T5C5_Göngyöl_DátumTábla();
                List<Adat_T5C5_Göngyöl_DátumTábla> AdatokDátum = KézDátum.Lista_Adatok();

                Holtart.Be(AdatokNapi.Count);
                List<string> szövegGyHavi = new List<string>();
                List<string> szövegGyBázis = new List<string>();
                foreach (Adat_T5C5_Futás rekord in AdatokNapi)
                {
                    string napikód = "0";
                    int futásnap = -1;
                    DateTime vizsgálatdátuma = new DateTime(1900, 1, 1);
                    DateTime utolsóforgalminap = new DateTime(1900, 1, 1);
                    string vizsgálatfokozata = "_";
                    int vizsgálatszáma = -1;

                    Adat_T5C5_Állomány AdatBázis = (from a in AdatokBázis
                                                    where a.Azonosító == rekord.Azonosító
                                                    select a).FirstOrDefault();
                    if (AdatBázis != null)
                    {
                        vizsgálatszáma = AdatBázis.Vizsgálatszáma;
                        futásnap = AdatBázis.Futásnap;
                        vizsgálatdátuma = AdatBázis.Vizsgálatdátuma;
                        vizsgálatfokozata = AdatBázis.Vizsgálatfokozata;
                        utolsóforgalminap = AdatBázis.Utolsóforgalminap;
                    }
                    napikód = "_";
                    switch (rekord.Futásstátus.Trim())
                    {
                        case "Forgalomban":
                            {
                                napikód = "F";
                                futásnap++;
                                utolsóforgalminap = Dátum.Value;
                                break;
                            }
                        case "Hibás":
                            {
                                napikód = "R";
                                break;
                            }

                        case "V1":
                            {
                                napikód = "V1";
                                futásnap = 0;
                                vizsgálatdátuma = Dátum.Value;
                                utolsóforgalminap = Dátum.Value;
                                vizsgálatfokozata = "V1";
                                vizsgálatszáma = 0;
                                break;
                            }
                        case "V2":
                            {
                                napikód = "V2";
                                futásnap = 0;
                                vizsgálatdátuma = Dátum.Value;
                                utolsóforgalminap = Dátum.Value;
                                vizsgálatfokozata = "V2";
                                vizsgálatszáma = 0;
                                break;
                            }
                        case "V3":
                            {
                                napikód = "V3";
                                futásnap = 0;
                                vizsgálatdátuma = Dátum.Value;
                                utolsóforgalminap = Dátum.Value;
                                vizsgálatfokozata = "V3";
                                vizsgálatszáma = 0;
                                break;
                            }
                        case "J1":
                            {
                                napikód = "J1";
                                futásnap = 0;
                                vizsgálatdátuma = Dátum.Value;
                                utolsóforgalminap = Dátum.Value;
                                vizsgálatfokozata = "J1";
                                vizsgálatszáma = 0;
                                break;
                            }
                        case "J2":
                            {
                                napikód = "J2";
                                futásnap = 0;
                                vizsgálatdátuma = Dátum.Value;
                                utolsóforgalminap = Dátum.Value;
                                vizsgálatfokozata = "J2";
                                vizsgálatszáma = 0;
                                break;
                            }
                        case "#J":
                            {
                                napikód = "J1";
                                futásnap = 0;
                                vizsgálatdátuma = Dátum.Value;
                                utolsóforgalminap = Dátum.Value;
                                vizsgálatfokozata = "J1";
                                vizsgálatszáma = 0;
                                break;
                            }
                        case "J":
                            {
                                napikód = "J1";
                                futásnap = 0;
                                vizsgálatdátuma = Dátum.Value;
                                utolsóforgalminap = Dátum.Value;
                                vizsgálatfokozata = "J1";
                                vizsgálatszáma = 0;
                                break;
                            }
                        case "E3":
                            {
                                napikód = "E3";
                                futásnap = 0;
                                vizsgálatdátuma = Dátum.Value;
                                utolsóforgalminap = Dátum.Value;
                                vizsgálatfokozata = "E3";

                                vizsgálatszáma += 1;
                                break;
                            }
                        case "-":
                            {
                                napikód = "-";
                                break;
                            }

                    }
                    // Havi tábla
                    Adat_T5C5_Havi_Nap AdatHavi = (from a in AdatokHavi
                                                   where a.Azonosító == rekord.Azonosító
                                                   select a).FirstOrDefault();

                    string szöveghavi;
                    if (AdatHavi == null)
                    {
                        szöveghavi = "INSERT INTO Állománytábla (azonosító, N1, N2, N3, N4, N5, N6, N7, N8, N9, N10, N11, N12, N13, N14, N15, N16, N17, N18, N19, N20, ";
                        szöveghavi += "N21, N22, N23, N24, N25, N26, N27, N28, N29, N30, N31, futásnap,telephely) VALUES (";
                        szöveghavi += $"'{rekord.Azonosító}', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.',";
                        szöveghavi += " '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', '.', 0, '_')";
                        szövegGyHavi.Add(szöveghavi);
                    }
                    szöveghavi = "UPDATE állománytábla SET ";
                    szöveghavi += $" N{Dátum.Value.Day}='{napikód}', ";
                    szöveghavi += $" futásnap={futásnap}, ";
                    szöveghavi += $" telephely='{Cmbtelephely.Text.Trim()}' ";
                    szöveghavi += $" WHERE azonosító='{rekord.Azonosító}'";
                    szövegGyHavi.Add(szöveghavi);

                    // Villamos3 tábla
                    string szövegBázis;
                    if (AdatBázis == null)
                    {
                        szövegBázis = "INSERT INTO Állománytábla (azonosító, utolsórögzítés, vizsgálatdátuma, Vizsgálatfokozata, vizsgálatszáma, futásnap, telephely, utolsóforgalminap ) VALUES (";
                        szövegBázis += $"'{rekord.Azonosító}', '1900.01.01', '1900.01.01', '.', 0,  0, '.', '1900.01.01')";
                        szövegGyBázis.Add(szövegBázis);
                    }
                    szövegBázis = "UPDATE állománytábla SET ";
                    szövegBázis += $" utolsórögzítés='{Dátum.Value:yyyy.MM.dd}', ";
                    szövegBázis += $" vizsgálatdátuma='{vizsgálatdátuma:yyyy.MM.dd}', ";
                    szövegBázis += $" Vizsgálatfokozata='{vizsgálatfokozata}', ";
                    szövegBázis += $" vizsgálatszáma={vizsgálatszáma}, ";
                    szövegBázis += $" futásnap={futásnap}, ";
                    szövegBázis += $" utolsóforgalminap='{utolsóforgalminap:yyyy.MM.dd}', ";
                    szövegBázis += $" telephely='{Cmbtelephely.Text.Trim()}' ";
                    szövegBázis += $" WHERE azonosító='{rekord.Azonosító}'";
                    szövegGyBázis.Add(szövegBázis);

                    Holtart.Lép();
                }
                MyA.ABMódosítás(helyHavi, jelszóBázis, szövegGyHavi);
                MyA.ABMódosítás(helyBázis, jelszóBázis, szövegGyBázis);

                // átállítjuk a dátumot
                Adat_T5C5_Göngyöl_DátumTábla Telepdátum = (from a in AdatokDátum
                                                           where a.Telephely == Cmbtelephely.Text.Trim()
                                                           select a).FirstOrDefault();

                if (Telepdátum != null)
                {
                    // ha van
                    szöveg = $"UPDATE dátumtábla SET  utolsórögzítés='{Dátum.Value:yyyy.MM.dd}' ";
                    szöveg += $" WHERE telephely='{Cmbtelephely.Text.Trim()}'";
                }
                else
                {
                    szöveg = "INSERT INTO dátumtábla (telephely, utolsórögzítés) VALUES (";
                    szöveg += $"'{Cmbtelephely.Text.Trim()}', '{Dátum.Value:yyyy.MM.dd}' )";
                }
                MyA.ABMódosítás(helyBázis, jelszóBázis, szöveg);

                Gombok_vezérlése();

                Holtart.Ki();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                Vissza_esemény();
                HibaNapló.Log(ex.Message, "A göngyölés közben hiba keletkezett, ezért a félbemaradt göngyölés visszagöngyöltem.\n Próbálja ismételten göngyölni.", ex.StackTrace, ex.Source, ex.HResult);

                MessageBox.Show("A göngyölés közben hiba keletkezett, ezért a félbemaradt göngyölés visszagöngyöltem.\n Próbálja ismételten göngyölni.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Vissza gönygyölés
        private void Vissza_Click(object sender, EventArgs e)
        {
            AdatokGöngyöl = T5C5.AdatokGöngyöl_feltöltése();
            if (AdatokGöngyöl == null) return;
            T5C5.Zároljuk(AdatokGöngyöl, Cmbtelephely.Text.Trim());
            try
            {
                Vissza_esemény();
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
            finally
            {
                T5C5.Kinyitjuk(Cmbtelephely.Text.Trim());
            }
        }
        //
        private void Vissza_esemény()
        {
            try
            {        // a havi táblába kitöröljük az adatokat és visszaírjuk a futásnapot
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\T5C5\{Dátum.Value.Year}\Havi{Dátum.Value:yyyyMM}.mdb";
                string helyhonnan = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\futás\{Dátum.Value.Year}";
                helyhonnan += $@"\Villamos3-{Dátum.Value.AddDays(-1):yyyyMMdd}.mdb";
                string hova = $@"{Application.StartupPath}\Főmérnökség\adatok\T5C5\Villamos3.mdb";

                string jelszó = "pozsgaii";
                string szöveg = $"Select * FROM állománytábla WHERE telephely='{Cmbtelephely.Text.Trim()}' ORDER BY azonosító";

                Kezelő_T5C5_Állomány kéz = new Kezelő_T5C5_Állomány();
                List<Adat_T5C5_Állomány> Áll_Adatok = kéz.Lista_Adat(helyhonnan, jelszó, szöveg);


                Holtart.Be();

                List<string> szövegGy = new List<string>();
                List<string> szövegGy1 = new List<string>();
                foreach (Adat_T5C5_Állomány rekord in Áll_Adatok)
                {
                    Holtart.Lép();

                    // előző napi futás adatokat írjuk vissza a havi táblában
                    szöveg = "UPDATE állománytábla SET ";
                    szöveg += $" N{Dátum.Value.Day}='/', futásnap={rekord.Futásnap} ";
                    szöveg += $" WHERE azonosító='{rekord.Azonosító}'";
                    szövegGy.Add(szöveg);

                    // módosítjuk a villamos3 adatait
                    szöveg = "UPDATE állománytábla SET ";
                    szöveg += $" utolsórögzítés='{rekord.Utolsórögzítés}', ";
                    szöveg += $" vizsgálatdátuma='{rekord.Vizsgálatdátuma}', ";
                    szöveg += $" Vizsgálatfokozata='{rekord.Vizsgálatfokozata}', ";
                    szöveg += $" utolsóforgalminap='{rekord.Utolsóforgalminap}', ";
                    szöveg += $" vizsgálatszáma={rekord.Vizsgálatszáma}, ";
                    szöveg += $" futásnap={rekord.Futásnap}, ";
                    szöveg += $" telephely='{rekord.Telephely}' ";
                    szöveg += $" WHERE azonosító='{rekord.Azonosító}'";
                    szövegGy1.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);
                MyA.ABMódosítás(hova, jelszó, szövegGy1);

                Holtart.Ki();


                // visszaállítjuk az utolsó napot Villamos3-naplófáljból
                List<Adat_T5C5_Göngyöl_DátumTábla> ElőzőNapi = T5C5.AdatokGöngyöl_feltöltése(helyhonnan);


                szöveg = $"Select * FROM dátumtábla WHERE telephely='{Cmbtelephely.Text.Trim()}'";
                Adat_T5C5_Göngyöl_DátumTábla rögzítés = (from a in ElőzőNapi
                                                         where a.Telephely == Cmbtelephely.Text.Trim()
                                                         select a).FirstOrDefault();
                if (rögzítés != null)
                {
                    szöveg = $"UPDATE dátumtábla SET utolsórögzítés='{rögzítés.Utolsórögzítés:yyyy.MM.dd}' WHERE telephely='{Cmbtelephely.Text.Trim()}'";
                    MyA.ABMódosítás(hova, jelszó, szöveg);

                    // kitöröljük a Villamos3-naplófáljból fájlt miutón frissítettük a villamos3-t
                    if (File.Exists(helyhonnan)) File.Delete(helyhonnan);
                }

                Gombok_vezérlése();
                MessageBox.Show("Az adatok visszagöngyölésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Nap nyitás-zárás
        private void Naplezárása_Click(object sender, EventArgs e)
        {
            Naplezárása_esemény();
        }
        //
        private void Naplezárása_esemény()
        {
            try
            {
                Adat_T5C5_Futás1 Elem = NapÁllapot();
                string szöveg;
                if (Elem != null)
                    szöveg = "Update futástábla1  SET  státus=1";
                else
                    szöveg = "INSERT INTO futástábla1 (státus) VALUES (1)";

                string hely = Fájlnév_meghatározás();
                string jelszó = "lilaakác";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                Gombok_vezérlése();
                Bevitelilap.Visible = false;
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

        private void Napkinyitása_Click(object sender, EventArgs e)
        {
            Napkinyitása_esemény();
        }
        //
        private void Napkinyitása_esemény()
        {
            try
            {
                Adat_T5C5_Futás1 Elem = NapÁllapot();

                if (Elem != null)
                {
                    string hely = Fájlnév_meghatározás();
                    string jelszó = "lilaakác";
                    string szöveg = " Update futástábla1  SET  státus=0";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }

                Gombok_vezérlése();
                Bevitelilap.Visible = false;
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
        //
        private Adat_T5C5_Futás1 NapÁllapot()
        {
            Adat_T5C5_Futás1 Válasz = null;
            try
            {
                string hely = Fájlnév_meghatározás();
                string jelszó = "lilaakác";
                string szöveg = "SELECT * FROM futástábla1 ";

                Kezelő_T5C5_Futás1 Kéz = new Kezelő_T5C5_Futás1();
                Válasz = Kéz.Egy_Adat(hely, jelszó, szöveg);
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
            return Válasz;
        }
        #endregion


        #region ZSER
        //
        private void Zserbeolvasás_Click(object sender, EventArgs e)
        {
            try
            {
                // megnézzük, hogy létezik-e adott új helyen napi tábla
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\{Dátum.Value.Year}\ZSER\zser{Dátum.Value:yyyyMMdd}.mdb";
                if (!File.Exists(hely))
                {
                    Adatbázis_Létrehozás.Zseltáblaalap(hely);
                }
                else if (MessageBox.Show("Már van az adott napra feltöltve adat ! Módosítjuk az adatokat ?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {    // ha létezik akkor töröljük
                    File.Delete(hely);
                    Adatbázis_Létrehozás.Zseltáblaalap(hely);
                }
                else
                    return;

                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;
                DateTime Kezdet = DateTime.Now;
                Holtart.Be();
                Főkönyv_Funkciók.ZSER_Betöltés(Cmbtelephely.Text.Trim(), Dátum.Value, "", fájlexc);
                Holtart.Ki();
                DateTime Vég = DateTime.Now;
                MessageBox.Show($"Az adat konvertálás befejeződött!\n Eltelt idő{Vég - Kezdet}", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        //
        private void Zseradategyeztetés_Click(object sender, EventArgs e)
        {
            try
            {
                // megnézzük, hogy létezik-e adott napi tábla
                string hely = Fájlnév_meghatározás();
                if (!File.Exists(hely)) throw new HibásBevittAdat("Hiányzonak a napi adatok!");

                // leellnőrizzük a zser adatokat hogy megvannak-e
                string helyzser = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\{Dátum.Value.Year}\ZSER\zser{Dátum.Value:yyyyMMdd}.mdb";
                if (!File.Exists(helyzser))
                {
                    helyzser = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\{Dátum.Value.Year}\ZSER\zser{Dátum.Value:yyyyMMdd}du.mdb";
                    if (!File.Exists(helyzser))
                    {
                        helyzser = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\{Dátum.Value.Year}\ZSER\zser{Dátum.Value:yyyyMMdd}de.mdb";
                        if (!File.Exists(helyzser)) throw new HibásBevittAdat("Hiányzonak a napi ZSER adatok!");
                    }
                }

                Holtart.Be();

                string jelszó = "lilaakác";
                string szöveg = "SELECT * FROM futástábla ORDER BY azonosító";
                Kezelő_T5C5_Futás kéz = new Kezelő_T5C5_Futás();
                List<Adat_T5C5_Futás> AdatokFutás = kéz.Lista_Adat(hely, jelszó, szöveg);

                szöveg = "SELECT * FROM zseltábla";
                Kezelő_Főkönyv_ZSER KézZser = new Kezelő_Főkönyv_ZSER();
                List<Adat_Főkönyv_ZSER> AdatokZser = KézZser.Lista_adatok(helyzser, jelszó, szöveg);

                List<string> szövegGy = new List<string>();
                foreach (Adat_T5C5_Futás rekord in AdatokFutás)
                {
                    Holtart.Lép();

                    Adat_Főkönyv_ZSER Kiadás = (from a in AdatokZser
                                                where a.Kocsi1 == rekord.Azonosító.Trim() || a.Kocsi2 == rekord.Azonosító.Trim() ||
                                                      a.Kocsi3 == rekord.Azonosító.Trim() || a.Kocsi4 == rekord.Azonosító.Trim() ||
                                                      a.Kocsi5 == rekord.Azonosító.Trim() || a.Kocsi6 == rekord.Azonosító.Trim()
                                                select a).FirstOrDefault();
                    if (Kiadás != null)
                    {
                        if (!(rekord.Futásstátus.Contains("E") || rekord.Futásstátus.Contains("V") || rekord.Futásstátus.Contains("J")))
                        {
                            szöveg = $"UPDATE futástábla  SET futásstátus='Forgalomban' WHERE azonosító='{rekord.Azonosító.Trim()}'";
                            szövegGy.Add(szöveg);
                        }
                    }
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);

                Holtart.Ki();

                MessageBox.Show("Az adat konvertálás befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Funkciók
        //
        private string Fájlnév_meghatározás()
        {
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\főkönyv\futás\";
            if (!Directory.Exists(hely)) System.IO.Directory.CreateDirectory(hely);
            hely += $@"\{Dátum.Value.Year}";
            if (!Directory.Exists(hely)) System.IO.Directory.CreateDirectory(hely);
            hely += $@"\futás{Dátum.Value:yyyyMMdd}nap.mdb";
            return hely;
        }

        private void Gombok_Színezése(int Gombszám, string Kategória)
        {
            // frissítjük a színezést
            switch (Kategória.Trim())
            {
                case "Forgalomban":
                    {
                        // zöld
                        Panel3.Controls[Gombszám].BackColor = Color.LimeGreen;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "Hibás");
                        break;
                    }
                case "-":
                    {
                        // szürke
                        Panel3.Controls[Gombszám].BackColor = Color.Silver;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "-");
                        break;
                    }
                case "_":
                    {
                        // szürke
                        Panel3.Controls[Gombszám].BackColor = Color.Silver;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "_");
                        break;
                    }
                case "E3":
                    {
                        // kék
                        Panel3.Controls[Gombszám].BackColor = Color.Blue;
                        Panel3.Controls[Gombszám].ForeColor = Color.White;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "E3");
                        break;
                    }
                case "V1":
                    {
                        // sárga
                        Panel3.Controls[Gombszám].BackColor = Color.Yellow;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "V1");
                        break;
                    }
                case "V2":
                    {
                        // narancssárga
                        Panel3.Controls[Gombszám].BackColor = Color.DarkOrange;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "V2");
                        break;
                    }
                case "V3":
                    {
                        // narancssárga
                        Panel3.Controls[Gombszám].BackColor = Color.DarkOrange;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "V3");
                        break;
                    }
                case "J1":
                    {
                        // narancssárga
                        Panel3.Controls[Gombszám].BackColor = Color.DarkOrange;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "J1");
                        break;
                    }
                case "J2":
                    {
                        // narancssárga
                        Panel3.Controls[Gombszám].BackColor = Color.DarkOrange;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "J2");
                        break;
                    }
                case "Hibás":
                    {
                        // piros
                        Panel3.Controls[Gombszám].BackColor = Color.Red;
                        Panel3.Controls[Gombszám].ForeColor = Color.White;
                        ToolTip1.SetToolTip(Panel3.Controls[Gombszám], "Hibás");
                        break;
                    }
            }

        }
        #endregion

        private void SAP_adatok_Click(object sender, EventArgs e)
        {
            try
            {
                SAP_adatok.Visible = false;
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                {
                    SAP_adatok.Visible = true;
                    return;
                }

                Holtart.Be();

                timer1.Enabled = true;
                FájlExcel_ = fájlexc;

                SZál_KM_Beolvasás(() =>
                {
                    //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                    timer1.Enabled = false;
                    Holtart.Ki();
                });

                SAP_adatok.Visible = true;
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

        private void SZál_KM_Beolvasás(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                //beolvassuk az adatokat
                SAP_Adatokbeolvasása_km.Km_beolvasó(FájlExcel_);
                MessageBox.Show("Az adat konvertálás befejeződött!", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Invoke(callback, new object[] { });
            });
            proc.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
        }
        //
        private void Label13_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (CTRL_le)
                {
                    string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\T5C5\villamos3.mdb";
                    string szöveg = $" UPDATE Dátumtábla SET Zárol = False WHERE Zárol = True";
                    string jelszó = "pozsgaii";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                    MessageBox.Show($"Minden telephelyi zárolás feloldásra került!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #region Listák
        private List<string> Pályaszám_feltöltés()
        {
            List<string> Válasz = new List<string>();
            try
            {
                if (Cmbtelephely.Text.Trim() == "") return Válasz;
                List<Adat_Jármű> Adatok = Kéz_Jármű.Lista_Adatok("Főmérnökség");

                Válasz = (from a in Adatok
                          where a.Üzem == Cmbtelephely.Text.Trim()
                          && a.Törölt == false
                          && a.Valóstípus.Contains("T5C5")
                          orderby a.Azonosító
                          select a.Azonosító).ToList();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Pályaszám_feltöltés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Válasz;
        }
        #endregion
    }
}