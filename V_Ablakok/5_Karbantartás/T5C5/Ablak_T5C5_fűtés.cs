using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;


namespace Villamos
{
    public partial class Ablak_T5C5_fűtés
    {
        string MelyikAdat = "";

        public Ablak_T5C5_fűtés()
        {
            InitializeComponent();
            Start();
        }


        void Start()
        {
            Telephelyekfeltöltése();

            // Szakszolgálati lekérdezés esetén működik csak a lekérdezés

            Kimutatás_készítés.Visible = Cmbtelephely.Enabled;


            string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{DateTime.Now.Year}\T5C5_Fűtés.mdb";
            if (!Exists(hely)) Adatbázis_Létrehozás.T5C5_fűtés_tábla(hely);
            Jogosultságkiosztás();
            Dátum_év.Value = DateTime.Today;
            Dátum.Value = DateTime.Today;

            // virtuálisan megnyitjuk a képet
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\T5C5\Fűtés_beállítás.jpg";
            Kezelő_Kép.KépMegnyitás(PictureBox2, hely, ToolTip1);
            PictureBox2.Top = 10;
            PictureBox2.Left = 10;
            PictureBox2.Width = 450;
            PictureBox2.Height = 570;
            PictureBox2.Visible = false;
        }


        private void Form1_Load(object sender, EventArgs e)
        { }


        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim(); }
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


        private void Form1_Shown(object sender, EventArgs e)
        {
            Dolgozófeltöltés();
            Pályaszámok_feltöltése();
        }



        #region Alap
        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\T5C5_fűtés.html";
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


        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Rögzít.Visible = false;

                melyikelem = 177;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Rögzít.Visible = true;

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
            }
        }


        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }


        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dolgozófeltöltés();
            Pályaszámok_feltöltése();
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
        #endregion


        #region Rögzítés
        private void Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                // Üres mezőket nem engedünk rögzíteni
                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat(" Pályaszámot meg kell adni.");
                if (Dolgozó.Text.Trim() == "") throw new HibásBevittAdat("A mérést végezte mező nem lehet üres.");
                if (I_szakasz.Text.Trim() == "") throw new HibásBevittAdat("Az I szakasz mért áramértéke nem lehet üres.");
                if (II_szakasz.Text.Trim() == "") throw new HibásBevittAdat("Az II szakasz mért áramértéke nem lehet üres.");
                if (Megjegyzés.Text.Trim() == "") Megjegyzés.Text = "_";

                // Számnak kell lennie
                if (!double.TryParse(I_szakasz.Text, out double szakasz_1)) throw new HibásBevittAdat("Az I szakasz mért áramértékének számnak kell lennie.");
                if (!double.TryParse(II_szakasz.Text, out double szakasz_2)) throw new HibásBevittAdat("Az II szakasz mért áramértékének számnak kell lennie.");

                // tisztítjuk a szöveget
                Megjegyzés.Text = MyF.Szöveg_Tisztítás(Megjegyzés.Text, 0, 255, true);
                Dolgozó.Text = MyF.Szöveg_Tisztítás(Dolgozó.Text, 0, 50, true); ;
                Pályaszám.Text = MyF.Szöveg_Tisztítás(Pályaszám.Text, 0, 10, true); ;

                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Dátum.Value.Year}";
                if (!Exists(hely)) System.IO.Directory.CreateDirectory(hely);
                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Dátum.Value.Year}\T5C5_Fűtés.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.T5C5_fűtés_tábla(hely);
                string jelszó = "RózsahegyiK";

                string szöveg = "SELECT * FROM Fűtés_tábla";
                Kezelő_T5C5_Fűtés Kéz = new Kezelő_T5C5_Fűtés();
                List<Adat_T5C5_Fűtés> AdatokÖ = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                long id = 1;
                if (AdatokÖ.Count > 0) id = AdatokÖ.Max(a => a.ID) + 1;

                int fűtés_típusa = 0;
                if (RadioButton1.Checked)
                    fűtés_típusa = 0;

                else if (RadioButton2.Checked)
                    fűtés_típusa = 1;
                else
                    fűtés_típusa = 2;

                int beállításiérték = 0;
                if (!Beállítási_értékek.Visible) // Beállítási_értékek,
                    beállításiérték = 0;
                else if (Beállítási_értékek.Checked)
                    beállításiérték = 2;
                else
                    beállításiérték = 1;

                Adat_T5C5_Fűtés AdatKüld = new Adat_T5C5_Fűtés(id,
                                                               Pályaszám.Text.Trim(),
                                                               Cmbtelephely.Text.Trim(),
                                                               Dátum.Value,
                                                               Dolgozó.Text.Trim(),
                                                               szakasz_1,
                                                               szakasz_2,
                                                               fűtés_típusa,
                                                               Jófűtés(),
                                                               Megjegyzés.Text.Trim(),
                                                               beállításiérték,
                                                               Program.PostásNév,
                                                               DateTime.Now
                                                               );

                Kéz.Rögzítés(hely, jelszó, AdatKüld);

                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Mezők_ürítése_szűk();
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

        private string Jófűtés()
        {
            string Jó_fűtés = "_";
            Jó_fűtés += Fűtünk(CheckBox1.Visible, CheckBox1.Checked);
            Jó_fűtés += Fűtünk(CheckBox2.Visible, CheckBox2.Checked);
            Jó_fűtés += Fűtünk(CheckBox3.Visible, CheckBox3.Checked);
            Jó_fűtés += Fűtünk(CheckBox4.Visible, CheckBox4.Checked);
            Jó_fűtés += Fűtünk(CheckBox5.Visible, CheckBox5.Checked);
            Jó_fűtés += Fűtünk(CheckBox6.Visible, CheckBox6.Checked);
            Jó_fűtés += Fűtünk(CheckBox7.Visible, CheckBox7.Checked);
            Jó_fűtés += Fűtünk(CheckBox8.Visible, CheckBox8.Checked);
            Jó_fűtés += Fűtünk(CheckBox9.Visible, CheckBox9.Checked);
            Jó_fűtés += Fűtünk(CheckBox10.Visible, CheckBox10.Checked);
            Jó_fűtés += Fűtünk(CheckBox11.Visible, CheckBox11.Checked);
            Jó_fűtés += Fűtünk(CheckBox12.Visible, CheckBox12.Checked);
            Jó_fűtés += Fűtünk(CheckBox13.Visible, CheckBox13.Checked);
            Jó_fűtés += Fűtünk(CheckBox14.Visible, CheckBox14.Checked);
            Jó_fűtés += Fűtünk(CheckBox15.Visible, CheckBox15.Checked);
            Jó_fűtés += Fűtünk(CheckBox16.Visible, CheckBox16.Checked);
            Jó_fűtés += Fűtünk(CheckBox17.Visible, CheckBox17.Checked);
            Jó_fűtés += Fűtünk(CheckBox18.Visible, CheckBox18.Checked);
            Jó_fűtés += Fűtünk(CheckBox19.Visible, CheckBox19.Checked);
            return Jó_fűtés;
        }

        private string Fűtünk(bool Látható, bool Jelölt)
        {
            if (!Látható)
                return "0";
            else if (Jelölt)
                return "1";
            else
                return "2";
        }
        #region Gombok


        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Gombválasztás();
            Beállítási_értékek.Visible = false;
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Gombválasztás();
            Beállítási_értékek.Visible = false;
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {
            Gombválasztás();
            Beállítási_értékek.Visible = true;
        }

        private void Gombválasztás()
        {
            if (RadioButton1.Checked)
                T5C5_fűtések();

            else if (RadioButton2.Checked)
                T5C5K2_fűtések();
            else
                T5C5K2_automata();
        }


        private void T5C5_fűtések()
        {
            CheckBox1.Text = "1";
            CheckBox1.BackColor = Color.Gray;
            CheckBox1.Visible = true;
            CheckBox2.Text = "";
            CheckBox2.BackColor = Color.Gray;
            CheckBox2.Visible = false;
            CheckBox3.Text = "2";
            CheckBox3.BackColor = Color.Gray;
            CheckBox3.Visible = true;
            CheckBox4.Text = "3";
            CheckBox4.BackColor = Color.Gray;
            CheckBox4.Visible = true;
            CheckBox5.Text = "4";
            CheckBox5.BackColor = Color.Gray;
            CheckBox5.Visible = true;
            CheckBox6.Text = "5";
            CheckBox6.BackColor = Color.Gray;
            CheckBox6.Visible = true;
            CheckBox7.Text = "6";
            CheckBox7.BackColor = Color.Gray;
            CheckBox7.Visible = true;
            CheckBox8.Text = "7";
            CheckBox8.BackColor = Color.Gray;
            CheckBox8.Visible = true;
            CheckBox9.Text = "";
            CheckBox9.BackColor = Color.Gray;
            CheckBox9.Visible = false;
            CheckBox10.Text = "";
            CheckBox10.BackColor = Color.Gray;
            CheckBox10.Visible = false;
            CheckBox11.Text = "8";
            CheckBox11.BackColor = Color.Gray;
            CheckBox11.Visible = true;
            CheckBox12.Text = "9";
            CheckBox12.BackColor = Color.Gray;
            CheckBox12.Visible = true;
            CheckBox13.Text = "";
            CheckBox13.BackColor = Color.Gray;
            CheckBox13.Visible = false;
            CheckBox14.Text = "";
            CheckBox14.BackColor = Color.Gray;
            CheckBox14.Visible = false;
            CheckBox15.Text = "10";
            CheckBox15.BackColor = Color.Gray;
            CheckBox15.Visible = true;
        }


        private void T5C5K2_fűtések()
        {
            CheckBox1.Text = "1";
            CheckBox1.BackColor = Color.Aqua;
            CheckBox1.Visible = true;
            CheckBox2.Text = "";
            CheckBox2.BackColor = Color.Aqua;
            CheckBox2.Visible = false;
            CheckBox3.Text = "2";
            CheckBox3.BackColor = Color.Aqua;
            CheckBox3.Visible = true;
            CheckBox4.Text = "3";
            CheckBox4.BackColor = Color.Aqua;
            CheckBox4.Visible = true;
            CheckBox5.Text = "";
            CheckBox5.BackColor = Color.Aqua;
            CheckBox5.Visible = false;
            CheckBox6.Text = "";
            CheckBox6.BackColor = Color.Aqua;
            CheckBox6.Visible = false;
            CheckBox7.Text = "6";
            CheckBox7.BackColor = Color.Aqua;
            CheckBox7.Visible = true;
            CheckBox8.Text = "7";
            CheckBox8.BackColor = Color.Aqua;
            CheckBox8.Visible = true;
            CheckBox9.Text = "";
            CheckBox9.BackColor = Color.Aqua;
            CheckBox9.Visible = false;
            CheckBox10.Text = "";
            CheckBox10.BackColor = Color.Aqua;
            CheckBox10.Visible = false;
            CheckBox11.Text = "8";
            CheckBox11.BackColor = Color.Aqua;
            CheckBox11.Visible = true;
            CheckBox12.Text = "9";
            CheckBox12.BackColor = Color.Aqua;
            CheckBox12.Visible = true;
            CheckBox13.Text = "";
            CheckBox13.BackColor = Color.Aqua;
            CheckBox13.Visible = false;
            CheckBox14.Text = "";
            CheckBox14.BackColor = Color.Aqua;
            CheckBox14.Visible = false;
            CheckBox15.Text = "10";
            CheckBox15.BackColor = Color.Aqua;
            CheckBox15.Visible = true;
        }


        private void T5C5K2_automata()
        {
            CheckBox1.Text = "1";
            CheckBox1.BackColor = Color.BlueViolet;
            CheckBox1.Visible = true;
            CheckBox2.Text = "2";
            CheckBox2.BackColor = Color.BlueViolet;
            CheckBox2.Visible = true;
            CheckBox3.Text = "3";
            CheckBox3.BackColor = Color.BlueViolet;
            CheckBox3.Visible = true;
            CheckBox4.Text = "4";
            CheckBox4.BackColor = Color.BlueViolet;
            CheckBox4.Visible = true;
            CheckBox5.Text = "5";
            CheckBox5.BackColor = Color.BlueViolet;
            CheckBox5.Visible = true;
            CheckBox6.Text = "6";
            CheckBox6.BackColor = Color.BlueViolet;
            CheckBox6.Visible = true;
            CheckBox7.Text = "7";
            CheckBox7.BackColor = Color.BlueViolet;
            CheckBox7.Visible = true;
            CheckBox8.Text = "8";
            CheckBox8.BackColor = Color.BlueViolet;
            CheckBox8.Visible = true;
            CheckBox9.Text = "9";
            CheckBox9.BackColor = Color.BlueViolet;
            CheckBox9.Visible = true;
            CheckBox10.Text = "10";
            CheckBox10.BackColor = Color.BlueViolet;
            CheckBox10.Visible = true;
            CheckBox11.Text = "11";
            CheckBox11.BackColor = Color.BlueViolet;
            CheckBox11.Visible = true;
            CheckBox12.Text = "12";
            CheckBox12.BackColor = Color.BlueViolet;
            CheckBox12.Visible = true;
            CheckBox13.Text = "13";
            CheckBox13.BackColor = Color.BlueViolet;
            CheckBox13.Visible = true;
            CheckBox14.Text = "14";
            CheckBox14.BackColor = Color.BlueViolet;
            CheckBox14.Visible = true;
            CheckBox15.Text = "15";
            CheckBox15.BackColor = Color.BlueViolet;
            CheckBox15.Visible = true;
        }


        private void Btnkilelöltörlés_Click(object sender, EventArgs e)
        {
            Mezők_ürítése_check();
        }


        private void Mezők_ürítése_check()
        {
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
            CheckBox4.Checked = false;
            CheckBox5.Checked = false;
            CheckBox6.Checked = false;
            CheckBox7.Checked = false;
            CheckBox8.Checked = false;
            CheckBox9.Checked = false;
            CheckBox10.Checked = false;
            CheckBox11.Checked = false;
            CheckBox12.Checked = false;
            CheckBox13.Checked = false;
            CheckBox14.Checked = false;
            CheckBox15.Checked = false;
            CheckBox16.Checked = false;
            CheckBox17.Checked = false;
            CheckBox18.Checked = false;
            CheckBox19.Checked = false;
        }


        private void BtnKijelölcsop_Click(object sender, EventArgs e)
        {
            CheckBox1.Checked = true;
            CheckBox2.Checked = true;
            CheckBox3.Checked = true;
            CheckBox4.Checked = true;
            CheckBox5.Checked = true;
            CheckBox6.Checked = true;
            CheckBox7.Checked = true;
            CheckBox8.Checked = true;
            CheckBox9.Checked = true;
            CheckBox10.Checked = true;
            CheckBox11.Checked = true;
            CheckBox12.Checked = true;
            CheckBox13.Checked = true;
            CheckBox14.Checked = true;
            CheckBox15.Checked = true;
            CheckBox16.Checked = true;
            CheckBox17.Checked = true;
            CheckBox18.Checked = true;
            CheckBox19.Checked = true;
        }


        private void Beállítási_értékek_MouseLeave(object sender, EventArgs e)
        {
            PictureBox2.Visible = false;
        }


        private void Beállítási_értékek_MouseEnter(object sender, EventArgs e)
        {
            PictureBox2.Visible = true;
        }
        #endregion


        #region Feltöltések
        private void Pályaszámok_feltöltése()
        {
            try
            {
                Pályaszám.Items.Clear();
                if (Cmbtelephely.Text.ToStrTrim() == "") return;

                Kezelő_Jármű KézJármű = new Kezelő_Jármű();
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok("Főmérnökség");
                Adatok = (from a in Adatok
                          where a.Törölt == false
                          && a.Valóstípus.Contains("T5C5")
                          orderby a.Azonosító
                          select a).ToList();
                if (Program.Postás_Vezér) Adatok = Adatok.Where(a => a.Üzem == Cmbtelephely.Text.ToStrTrim()).ToList();

                // feltöltjük az összes pályaszámot a Comboba
                foreach (Adat_Jármű Elem in Adatok)
                    Pályaszám.Items.Add(Elem.Azonosító);
                Pályaszám.Refresh();
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

        private void Dolgozófeltöltés()
        {
            try
            {
                Dolgozó.Items.Clear();
                Dolgozó.Items.Add("");
                string hely, jelszó, szöveg;
                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Dolgozók.mdb";
                if (!File.Exists(hely)) return;
                jelszó = "forgalmiutasítás";
                szöveg = "SELECT * FROM Dolgozóadatok where kilépésiidő=#1/1/1900#  order by DolgozóNév asc";

                Kezelő_Dolgozó_Alap Kéz = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Dolgozó_Alap Elem in Adatok)
                    Dolgozó.Items.Add(Elem.DolgozóNév);
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
            Pályaszámok_feltöltése();
            Dolgozófeltöltés();
            Mezők_ürítése_check();
        }


        private void Mezők_ürítése_szűk()
        {
            I_szakasz.Text = "";
            II_szakasz.Text = "";
            Megjegyzés.Text = "";
            Beállítási_értékek.Checked = false;
        }


        private void Új_elem_Click(object sender, EventArgs e)
        {
            Mezők_ürítése_szűk();
        }
        #endregion


        #region Lekérdezés
        private void Lekérdezés_Click(object sender, EventArgs e)
        {
            try
            {
                MelyikAdat = "Fő";
                string hely, jelszó, szöveg;
                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Dátum_év.Value.Year}";
                if (!Exists(hely)) System.IO.Directory.CreateDirectory(hely);
                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Dátum_év.Value.Year}\T5C5_Fűtés.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.T5C5_fűtés_tábla(hely);
                jelszó = "RózsahegyiK";
                bool minden;


                if (Lekérdezés_minden.Checked)
                {
                    // Minden rögzítés
                    szöveg = "SELECT * FROM Fűtés_tábla order by pályaszám,id";
                    minden = true;
                }
                else
                {
                    // elemenként egy lekérdezés
                    szöveg = "SELECT * FROM Fűtés_tábla order by pályaszám asc, id desc";
                    minden = false;
                }

                string előzőpsz;

                Lekérdezés_Tábla.Rows.Clear();
                Lekérdezés_Tábla.Columns.Clear();
                Lekérdezés_Tábla.Refresh();
                Lekérdezés_Tábla.Visible = false;
                Lekérdezés_Tábla.ColumnCount = 13;

                // fejléc elkészítése
                Lekérdezés_Tábla.Columns[0].HeaderText = "Sorszám";
                Lekérdezés_Tábla.Columns[0].Width = 100;
                Lekérdezés_Tábla.Columns[1].HeaderText = "Pályaszám";
                Lekérdezés_Tábla.Columns[1].Width = 100;
                Lekérdezés_Tábla.Columns[2].HeaderText = "Telephely";
                Lekérdezés_Tábla.Columns[2].Width = 100;
                Lekérdezés_Tábla.Columns[3].HeaderText = "Dátum";
                Lekérdezés_Tábla.Columns[3].Width = 100;
                Lekérdezés_Tábla.Columns[4].HeaderText = "Dolgozó";
                Lekérdezés_Tábla.Columns[4].Width = 100;
                Lekérdezés_Tábla.Columns[5].HeaderText = "I szakasz";
                Lekérdezés_Tábla.Columns[5].Width = 100;
                Lekérdezés_Tábla.Columns[6].HeaderText = "II szakasz";
                Lekérdezés_Tábla.Columns[6].Width = 100;
                Lekérdezés_Tábla.Columns[7].HeaderText = "Fűtés típusa";
                Lekérdezés_Tábla.Columns[7].Width = 100;
                Lekérdezés_Tábla.Columns[8].HeaderText = "Jó fűtések";
                Lekérdezés_Tábla.Columns[8].Width = 100;
                Lekérdezés_Tábla.Columns[9].HeaderText = "Megjegyzés";
                Lekérdezés_Tábla.Columns[9].Width = 100;
                Lekérdezés_Tábla.Columns[10].HeaderText = "Beállítási értékek";
                Lekérdezés_Tábla.Columns[10].Width = 100;
                Lekérdezés_Tábla.Columns[11].HeaderText = "Rögzítő";
                Lekérdezés_Tábla.Columns[11].Width = 100;
                Lekérdezés_Tábla.Columns[12].HeaderText = "Rögzítés ideje";
                Lekérdezés_Tábla.Columns[12].Width = 200;

                Kezelő_T5C5_Fűtés Kéz = new Kezelő_T5C5_Fűtés();
                List<Adat_T5C5_Fűtés> AdatokÖ = Kéz.Lista_Adatok(hely, jelszó, szöveg);
                List<Adat_T5C5_Fűtés> Adatok = new List<Adat_T5C5_Fűtés>();
                if (!Cmbtelephely.Enabled)
                    Adatok = (from a in AdatokÖ
                              where a.Telephely == Cmbtelephely.Text.Trim()
                              select a).ToList();
                else
                    Adatok.AddRange(AdatokÖ);

                int i;

                előzőpsz = "_";
                foreach (Adat_T5C5_Fűtés rekord in Adatok)
                {
                    if (minden == true || előzőpsz.Trim() != rekord.Pályaszám)
                    {
                        Lekérdezés_Tábla.RowCount++;
                        i = Lekérdezés_Tábla.RowCount - 1;
                        Lekérdezés_Tábla.Rows[i].Cells[0].Value = rekord.ID;
                        Lekérdezés_Tábla.Rows[i].Cells[1].Value = rekord.Pályaszám;
                        előzőpsz = rekord.Pályaszám;
                        Lekérdezés_Tábla.Rows[i].Cells[2].Value = rekord.Telephely;
                        Lekérdezés_Tábla.Rows[i].Cells[3].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                        Lekérdezés_Tábla.Rows[i].Cells[4].Value = rekord.Dolgozó;
                        Lekérdezés_Tábla.Rows[i].Cells[5].Value = rekord.I_szakasz;
                        Lekérdezés_Tábla.Rows[i].Cells[6].Value = rekord.II_szakasz;
                        Lekérdezés_Tábla.Rows[i].Cells[7].Value = rekord.Fűtés_típusa;
                        Lekérdezés_Tábla.Rows[i].Cells[8].Value = rekord.Jófűtés;
                        Lekérdezés_Tábla.Rows[i].Cells[9].Value = rekord.Megjegyzés;
                        Lekérdezés_Tábla.Rows[i].Cells[10].Value = rekord.Beállítási_értékek;
                        Lekérdezés_Tábla.Rows[i].Cells[11].Value = rekord.Módosító;
                        Lekérdezés_Tábla.Rows[i].Cells[12].Value = rekord.Mikor;
                    }
                }
                Lekérdezés_Tábla.Visible = true;
                Lekérdezés_Tábla.Refresh();
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


        private void Lekérdezés_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {        // visszaírjuk az adatokat a másik lapra
                if (e.RowIndex < 0) return;
                if (MelyikAdat != "Fő") return;

                Pályaszám.Text = Lekérdezés_Tábla.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
                Dátum.Value = Convert.ToDateTime(Lekérdezés_Tábla.Rows[e.RowIndex].Cells[3].Value);
                Dolgozó.Text = Lekérdezés_Tábla.Rows[e.RowIndex].Cells[4].Value.ToStrTrim();
                I_szakasz.Text = Lekérdezés_Tábla.Rows[e.RowIndex].Cells[5].Value.ToStrTrim();
                II_szakasz.Text = Lekérdezés_Tábla.Rows[e.RowIndex].Cells[6].Value.ToStrTrim();
                switch (Lekérdezés_Tábla.Rows[e.RowIndex].Cells[7].Value.ToStrTrim())
                {
                    case "0":
                        {
                            RadioButton1.Checked = true;
                            break;
                        }
                    case "1":
                        {
                            RadioButton2.Checked = true;
                            break;
                        }
                    case "2":
                        {
                            RadioButton3.Checked = true;
                            break;
                        }
                }
                Megjegyzés.Text = Lekérdezés_Tábla.Rows[e.RowIndex].Cells[9].Value.ToStrTrim();

                switch (Lekérdezés_Tábla.Rows[e.RowIndex].Cells[10].Value.ToStrTrim())
                {
                    case "0":
                        {
                            Beállítási_értékek.Visible = false;
                            break;
                        }
                    case "1":
                        {
                            Beállítási_értékek.Visible = true;
                            Beállítási_értékek.Checked = false;
                            break;
                        }
                    case "2":
                        {
                            Beállítási_értékek.Visible = true;
                            Beállítási_értékek.Checked = true;
                            break;
                        }
                }
                string ideigszó = Lekérdezés_Tábla.Rows[e.RowIndex].Cells[8].Value.ToStrTrim();
                CheckBox1.Visible = Látszik(ideigszó.Substring(1, 1) ?? "");
                CheckBox1.Checked = Jelölt(ideigszó.Substring(1, 1) ?? "");
                CheckBox2.Visible = Látszik(ideigszó.Substring(2, 1) ?? "");
                CheckBox2.Checked = Jelölt(ideigszó.Substring(2, 1) ?? "");
                CheckBox3.Visible = Látszik(ideigszó.Substring(3, 1) ?? "");
                CheckBox3.Checked = Jelölt(ideigszó.Substring(3, 1) ?? "");
                CheckBox4.Visible = Látszik(ideigszó.Substring(4, 1) ?? "");
                CheckBox4.Checked = Jelölt(ideigszó.Substring(4, 1) ?? "");
                CheckBox5.Visible = Látszik(ideigszó.Substring(5, 1) ?? "");
                CheckBox5.Checked = Jelölt(ideigszó.Substring(5, 1) ?? "");
                CheckBox6.Visible = Látszik(ideigszó.Substring(6, 1) ?? "");
                CheckBox6.Checked = Jelölt(ideigszó.Substring(6, 1) ?? "");
                CheckBox7.Visible = Látszik(ideigszó.Substring(7, 1) ?? "");
                CheckBox7.Checked = Jelölt(ideigszó.Substring(7, 1) ?? "");
                CheckBox8.Visible = Látszik(ideigszó.Substring(8, 1) ?? "");
                CheckBox8.Checked = Jelölt(ideigszó.Substring(8, 1) ?? "");
                CheckBox9.Visible = Látszik(ideigszó.Substring(9, 1) ?? "");
                CheckBox9.Checked = Jelölt(ideigszó.Substring(9, 1) ?? "");
                CheckBox10.Visible = Látszik(ideigszó.Substring(10, 1) ?? "");
                CheckBox10.Checked = Jelölt(ideigszó.Substring(10, 1) ?? "");
                CheckBox11.Visible = Látszik(ideigszó.Substring(11, 1) ?? "");
                CheckBox11.Checked = Jelölt(ideigszó.Substring(11, 1) ?? "");
                CheckBox12.Visible = Látszik(ideigszó.Substring(12, 1) ?? "");
                CheckBox12.Checked = Jelölt(ideigszó.Substring(12, 1) ?? "");
                CheckBox13.Visible = Látszik(ideigszó.Substring(13, 1) ?? "");
                CheckBox13.Checked = Jelölt(ideigszó.Substring(13, 1) ?? "");
                CheckBox14.Visible = Látszik(ideigszó.Substring(14, 1) ?? "");
                CheckBox14.Checked = Jelölt(ideigszó.Substring(14, 1) ?? "");
                CheckBox15.Visible = Látszik(ideigszó.Substring(15, 1) ?? "");
                CheckBox15.Checked = Jelölt(ideigszó.Substring(15, 1) ?? "");
                CheckBox16.Visible = Látszik(ideigszó.Substring(16, 1) ?? "");
                CheckBox16.Checked = Jelölt(ideigszó.Substring(16, 1) ?? "");
                CheckBox17.Visible = Látszik(ideigszó.Substring(17, 1) ?? "");
                CheckBox17.Checked = Jelölt(ideigszó.Substring(17, 1) ?? "");
                CheckBox18.Visible = Látszik(ideigszó.Substring(18, 1) ?? "");
                CheckBox18.Checked = Jelölt(ideigszó.Substring(18, 1) ?? "");
                CheckBox19.Visible = Látszik(ideigszó.Substring(19, 1) ?? "");
                CheckBox19.Checked = Jelölt(ideigszó.Substring(19, 1) ?? "");
                Fülek.SelectedIndex = 0;
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


        private bool Jelölt(string Betű)
        {
            bool válasz = false;
            if (Betű == "1") válasz = true;
            return válasz;

        }


        private bool Látszik(string Betű)
        {
            bool válasz = true;
            if (Betű == "0") válasz = false;
            return válasz;
        }


        private void BtnExcelkimenet_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lekérdezés_Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "T5C5_fűtés_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMdd"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, Lekérdezés_Tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void PSZ_hiány_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Dátum_év.Value.Year}\T5C5_Fűtés.mdb";
                if (!Exists(hely)) throw new HibásBevittAdat("Ebben az évben még nincsenek mérési adatok.");
                MelyikAdat = "Kieg";

                if (Cmbtelephely.Text.ToStrTrim() == "") return;
                hely = Application.StartupPath + @"\Főmérnökség\Adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg;
                // ha nem telephelyről kérdezzük le akkor minden kocsit kiír
                int volt = 0;

                for (int ij = 0; ij < Cmbtelephely.Items.Count; ij++)
                {
                    if (Cmbtelephely.Items[ij].ToStrTrim() == Program.PostásTelephely)
                        volt = 1;
                }
                if (volt == 1)
                {
                    szöveg = "Select * FROM Állománytábla WHERE Üzem='" + Cmbtelephely.Text.ToStrTrim() + "' AND ";
                    szöveg += " törölt=0 AND valóstípus Like  '%T5C5%' ORDER BY azonosító";
                }
                else
                {
                    szöveg = "Select * FROM Állománytábla WHERE  törölt=0 AND valóstípus Like  '%T5C5%' ORDER BY azonosító";
                }
                // feltöltjük az összes pályaszámot a Comboba
                Lekérdezés_Tábla.Rows.Clear();
                Lekérdezés_Tábla.Columns.Clear();
                Lekérdezés_Tábla.Refresh();
                Lekérdezés_Tábla.Visible = false;
                Lekérdezés_Tábla.ColumnCount = 3;

                // fejléc elkészítése

                Lekérdezés_Tábla.Columns[0].HeaderText = "Pályaszám";
                Lekérdezés_Tábla.Columns[0].Width = 100;
                Lekérdezés_Tábla.Columns[1].HeaderText = "Telephely";
                Lekérdezés_Tábla.Columns[1].Width = 100;
                Lekérdezés_Tábla.Columns[2].HeaderText = "Ellenőrzés elkészült";
                Lekérdezés_Tábla.Columns[2].Width = 100;

                Kezelő_Jármű Kéz = new Kezelő_Jármű();
                List<Adat_Jármű> AdatokJármű = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Dátum_év.Value.Year}\T5C5_Fűtés.mdb";
                jelszó = "RózsahegyiK";
                szöveg = "SELECT * FROM Fűtés_tábla";

                Kezelő_T5C5_Fűtés KézFűtés = new Kezelő_T5C5_Fűtés();
                List<Adat_T5C5_Fűtés> AdatokFűtés = KézFűtés.Lista_Adatok(hely, jelszó, szöveg);

                Holtart.Be(AdatokJármű.Count + 1);

                int i;
                foreach (Adat_Jármű rekord in AdatokJármű)
                {
                    Lekérdezés_Tábla.RowCount++;
                    i = Lekérdezés_Tábla.RowCount - 1;

                    Lekérdezés_Tábla.Rows[i].Cells[0].Value = rekord.Azonosító;
                    Lekérdezés_Tábla.Rows[i].Cells[1].Value = rekord.Üzem;


                    Adat_T5C5_Fűtés Elem = (from a in AdatokFűtés
                                            where a.Pályaszám == rekord.Azonosító
                                            select a).FirstOrDefault();
                    if (Elem != null)
                        Lekérdezés_Tábla.Rows[i].Cells[2].Value = "Igen";
                    else
                        Lekérdezés_Tábla.Rows[i].Cells[2].Value = "Nem";
                    Holtart.Lép();
                }
                Holtart.Ki();
                Lekérdezés_Tábla.Visible = true;
                Lekérdezés_Tábla.Refresh();
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



        private void Kimutatás_készítés_Click(object sender, EventArgs e)
        {
            try
            {
                MelyikAdat = "Összesítés";
                string hely, jelszó, szöveg;
                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Dátum_év.Value.Year}";
                if (!Exists(hely))
                    System.IO.Directory.CreateDirectory(hely);
                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Dátum_év.Value.Year}\T5C5_Fűtés.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.T5C5_fűtés_tábla(hely);
                jelszó = "RózsahegyiK";
                // elemenként egy lekérdezés
                szöveg = "SELECT * FROM Fűtés_tábla order by pályaszám asc, id desc";

                string előzőpsz;

                Lekérdezés_Tábla.Rows.Clear();
                Lekérdezés_Tábla.Columns.Clear();
                Lekérdezés_Tábla.Refresh();
                Lekérdezés_Tábla.Visible = false;
                Lekérdezés_Tábla.ColumnCount = 10;

                // fejléc elkészítése

                Lekérdezés_Tábla.Columns[0].HeaderText = "Pályaszám";
                Lekérdezés_Tábla.Columns[0].Width = 100;
                Lekérdezés_Tábla.Columns[1].HeaderText = "Telephely";
                Lekérdezés_Tábla.Columns[1].Width = 100;
                Lekérdezés_Tábla.Columns[2].HeaderText = "Dátum";
                Lekérdezés_Tábla.Columns[2].Width = 100;
                Lekérdezés_Tábla.Columns[3].HeaderText = "Dolgozó";
                Lekérdezés_Tábla.Columns[3].Width = 150;
                Lekérdezés_Tábla.Columns[4].HeaderText = "I szakasz";
                Lekérdezés_Tábla.Columns[4].Width = 100;
                Lekérdezés_Tábla.Columns[5].HeaderText = "II szakasz";
                Lekérdezés_Tábla.Columns[5].Width = 100;
                Lekérdezés_Tábla.Columns[6].HeaderText = "Fűtés típusa";
                Lekérdezés_Tábla.Columns[6].Width = 150;
                Lekérdezés_Tábla.Columns[7].HeaderText = "Karakterisztika";
                Lekérdezés_Tábla.Columns[7].Width = 150;
                Lekérdezés_Tábla.Columns[8].HeaderText = "Hibás fűtések";
                Lekérdezés_Tábla.Columns[8].Width = 250;
                Lekérdezés_Tábla.Columns[9].HeaderText = "Megjegyzés";
                Lekérdezés_Tábla.Columns[9].Width = 300;



                string ideigkód = "_,1,0,2,3,4,5,6,7,0,0,8,9,0,0,10,TP1,TP4,TP2,TP3";
                string ideigkódK = "_,1,0,2,3,0,0,6,7,0,0,8,9,0,0,10,TP1,TP4,TP2,TP3";
                string ideigkódA = "_,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,TP1,TP4,TP2,TP3";
                string[] hagyományos = ideigkód.Split(',');
                string[] hagyományosK = ideigkódK.Split(',');
                string[] automata = ideigkódA.Split(',');

                Kezelő_T5C5_Fűtés KézFűtés = new Kezelő_T5C5_Fűtés();
                List<Adat_T5C5_Fűtés> AdatokFűtés = KézFűtés.Lista_Adatok(hely, jelszó, szöveg);

                int i;

                előzőpsz = "_";
                Holtart.Be(AdatokFűtés.Count + 1);
                foreach (Adat_T5C5_Fűtés rekord in AdatokFűtés)
                {
                    if (előzőpsz.Trim() != rekord.Pályaszám)
                    {
                        Lekérdezés_Tábla.RowCount++;
                        i = Lekérdezés_Tábla.RowCount - 1;
                        Lekérdezés_Tábla.Rows[i].Cells[0].Value = rekord.Pályaszám;
                        előzőpsz = rekord.Pályaszám;
                        Lekérdezés_Tábla.Rows[i].Cells[1].Value = rekord.Telephely;
                        Lekérdezés_Tábla.Rows[i].Cells[2].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                        Lekérdezés_Tábla.Rows[i].Cells[3].Value = rekord.Dolgozó;
                        Lekérdezés_Tábla.Rows[i].Cells[4].Value = rekord.I_szakasz;
                        Lekérdezés_Tábla.Rows[i].Cells[5].Value = rekord.II_szakasz;
                        switch (rekord.Fűtés_típusa)
                        {
                            case 0:
                                {
                                    Lekérdezés_Tábla.Rows[i].Cells[6].Value = "Hagyományos";
                                    break;
                                }
                            case 1:
                                {
                                    Lekérdezés_Tábla.Rows[i].Cells[6].Value = "Hagyományos K-s";
                                    break;
                                }
                            case 2:
                                {
                                    Lekérdezés_Tábla.Rows[i].Cells[6].Value = "Automata";
                                    break;
                                }
                        }
                        switch (rekord.Beállítási_értékek)
                        {
                            case 0:
                                {
                                    Lekérdezés_Tábla.Rows[i].Cells[7].Value = "";
                                    break;
                                }
                            case 1:
                                {
                                    Lekérdezés_Tábla.Rows[i].Cells[7].Value = "Hibás";
                                    break;
                                }
                            case 2:
                                {
                                    Lekérdezés_Tábla.Rows[i].Cells[7].Value = "Rendben";
                                    break;
                                }
                        }
                        Lekérdezés_Tábla.Rows[i].Cells[8].Value = "";
                        // ha van benne rossz fűtés, akkor kiírjuk a rosszakat
                        string darabolandó = rekord.Jófűtés;
                        if (darabolandó.Contains("2"))
                        {
                            Lekérdezés_Tábla.Rows[i].Cells[8].Value = "";
                            for (int betű = 1; betű < darabolandó.Length; betű++)
                            {
                                if (darabolandó.Substring(betű, 1) == "2")
                                {
                                    switch (Lekérdezés_Tábla.Rows[i].Cells[6].Value.ToStrTrim())
                                    {
                                        case "Hagyományos":
                                            {
                                                Lekérdezés_Tábla.Rows[i].Cells[8].Value += hagyományos[betű] + "-";
                                                break;
                                            }
                                        case "Hagyományos K-s":
                                            {
                                                Lekérdezés_Tábla.Rows[i].Cells[8].Value += hagyományosK[betű] + "-";
                                                break;
                                            }
                                        case "Automata":
                                            {
                                                Lekérdezés_Tábla.Rows[i].Cells[8].Value += automata[betű] + "-";
                                                break;
                                            }
                                    }

                                }
                            }
                        }
                        Lekérdezés_Tábla.Rows[i].Cells[9].Value = rekord.Megjegyzés;
                    }
                    Holtart.Lép();
                }

                Holtart.Ki();
                Lekérdezés_Tábla.Visible = true;
                Lekérdezés_Tábla.Refresh();
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