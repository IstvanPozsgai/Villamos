using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;
using static System.IO.File;
using MyA = Adatbázis;
using MyEn = Villamos.V_MindenEgyéb.Enumok;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_Akkumulátor
    {

        public Ablak_Akkumulátor()
        {
            InitializeComponent();
        }

        readonly Kezelő_Akkumulátor KézAkku = new Kezelő_Akkumulátor();
        readonly Kezelő_Akkumulátor_Mérés KézAkkuMér = new Kezelő_Akkumulátor_Mérés();
        readonly Kezelő_Jármű Kéz_Jármű = new Kezelő_Jármű();

        List<Adat_Akkumulátor_Mérés> AdatokAkkuMér = new List<Adat_Akkumulátor_Mérés>();
        List<Adat_Akkumulátor> AdatokAkku = new List<Adat_Akkumulátor>();
        List<Adat_Jármű> Adatok_Jármű = new List<Adat_Jármű>();

        private void AblakAkkumulátor_Load(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + $@"\Főmérnökség\adatok\Akkumulátor\Akkunapló{Dátumtól.Value.Year}.mdb";
                if (!Exists(hely))
                    Adatbázis_Létrehozás.Akku_Mérés(hely);

                hely = Application.StartupPath + @"\Főmérnökség\adatok\Akkumulátor\Akku.mdb";
                if (!Exists(hely))
                    Adatbázis_Létrehozás.Akku_adatok(hely);

                Jogosultságkiosztás();
                Telephelyekfeltöltése();
                Fülekkitöltése();
                Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;

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


        #region Alap
        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk
                Btnakurögzít.Enabled = false;
                btnrögzítés.Enabled = false;
                CmbTelephely.Enabled = false;

                Beépít.Enabled = false;
                Kiépít.Enabled = false;
                SelejtElő.Enabled = false;
                Törölt.Enabled = false;
                Leselejtezett.Enabled = false;
                Használt.Enabled = false;
                TelephelyEllenőr.Enabled = false;
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {

                }
                else
                {
                }

                melyikelem = 190;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    // akku alapadat
                    Btnakurögzít.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    // mérés rögzítés
                    btnrögzítés.Enabled = true;
                }
                // módosítás 3

                if (MyF.Vanjoga(melyikelem, 3))
                {
                    // szabad telephely választás
                    CmbTelephely.Enabled = true;
                }

                melyikelem = 191;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    // akku alapadat
                    Beépít.Enabled = true;
                    Kiépít.Enabled = true;
                    SelejtElő.Enabled = true;
                    Törölt.Enabled = true;
                    Leselejtezett.Enabled = true;
                    Használt.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    TelephelyEllenőr.Enabled = true;
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


        private void Fülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
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


        private void Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\akkumulátor.html";
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


        private void Fülekkitöltése()
        {
            switch (Fülek.SelectedIndex)
            {
                case 0:
                    {
                        //Alapadatok
                        Combokfeltöltése();
                        Dgyártásiidő.Value = new DateTime(1900, 1, 1);
                        Dgarancia.Value = new DateTime(1900, 1, 1);
                        Dbeépítésdátum.Value = new DateTime(1900, 1, 1);
                        break;
                    }
                case 1:
                    {
                        //Mérések
                        mérdátum.Value = new DateTime(2020, 1, 1, 0, 0, 0);
                        break;
                    }
                case 2:
                    {
                        //Beépítés átépítés
                        Beép_Státus_Feltöltés();
                        Beép_Pályaszám_fetöltés();
                        StátusVálasztás();
                        break;
                    }
                case 3:
                    {
                        //Alapadatok listázása
                        AlapListaComboFeltöltés();
                        break;
                    }
                case 4:
                    {
                        //Mérések listázása
                        Dátumtól.Value = new DateTime(DateTime.Today.Year, 1, 1);
                        dátumig.Value = new DateTime(DateTime.Today.Year, 12, 31);
                        break;
                    }
            }
        }


        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }


        private void Telephelyekfeltöltése()
        {
            try
            {
                CmbTelephely.Items.Clear();
                CmbTelephely.Items.AddRange(Listák.TelephelyLista_Jármű());
                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                { CmbTelephely.Text = CmbTelephely.Items[0].ToString().Trim(); }
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
        #endregion


        #region Alapadatok_Lapfül
        private void Btnakuúj_Click(object sender, EventArgs e)
        {
            try
            {
                Textgyáriszám.Text = "";
                Combofajta.Text = "";
                Combogyártó.Text = "";
                Combotípus.Text = "";
                Dgarancia.Value = new DateTime(1900, 1, 1);
                Dgyártásiidő.Value = new DateTime(1900, 1, 1);
                Státus_alap.Text = "1 - Új";
                Textbeépítve.Text = "_";
                Dbeépítésdátum.Value = new DateTime(1900, 1, 1);
                TextMegjegyzés.Text = "";
                Telephely_alap.Text = CmbTelephely.Text.Trim();
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


        private void Alap_üres()
        {
            Textgyáriszám.Text = "";
            Combofajta.Text = "";
            Combogyártó.Text = "";
            Combotípus.Text = "";
            Dgarancia.Value = new DateTime(1900, 1, 1);
            Dgyártásiidő.Value = new DateTime(1900, 1, 1);
            Státus_alap.Text = "";
            Textbeépítve.Text = "";
            Dbeépítésdátum.Value = new DateTime(1900, 1, 1);
            TextMegjegyzés.Text = "";
            Telephely_alap.Text = "";


        }


        private void Alap_Egy_kiírás(string Gyáriszám)
        {
            try
            {

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Akkumulátor\akku.mdb";
                string jelszó = "kasosmiklós";
                string szöveg = $"SELECT * FROM akkutábla WHERE gyáriszám='{Gyáriszám.Trim()}'";


                Kezelő_Akkumulátor Kéz = new Kezelő_Akkumulátor();
                Adat_Akkumulátor Adat = Kéz.Egy_Adat(hely, jelszó, szöveg);
                Alap_üres();

                if (Adat != null)
                {
                    Textgyáriszám.Text = Adat.Gyáriszám.Trim();
                    Combofajta.Text = Adat.Fajta.Trim();
                    Combogyártó.Text = Adat.Gyártó.Trim();
                    Combotípus.Text = Adat.Típus.Trim();
                    Dgarancia.Value = Adat.Garancia;
                    Dgyártásiidő.Value = Adat.Gyártásiidő;
                    Státus_alap.Text = $"{Adat.Státus} - {Enum.GetName(typeof(MyEn.Akku_Státus), Adat.Státus)}";
                    Textbeépítve.Text = Adat.Beépítve.Trim();
                    Dbeépítésdátum.Value = Adat.Módosításdátuma;
                    TextMegjegyzés.Text = Adat.Megjegyzés.Trim();
                    Kapacitás_Alap.Text = Adat.Kapacitás.ToString();
                    Telephely_alap.Text = Adat.Telephely.Trim();
                }
                else
                    throw new HibásBevittAdat("Nincs a feltételeknek megfelelő adat.");
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

        private void BtnAkufriss_Click(object sender, EventArgs e)
        {
            try
            {
                if (Textgyáriszám.Text.Trim() == "")
                    throw new HibásBevittAdat("A gyáriszám mező nem lehet üres.");
                Alap_Egy_kiírás(Textgyáriszám.Text.Trim());
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

        private void Btnakurögzít_Click(object sender, EventArgs e)
        {
            try
            {
                // megnézzük, hogy létezik-e a pályaszám
                if (Textgyáriszám.Text.Trim() == "") throw new HibásBevittAdat("A gyáriszám mező nem lehet üres.");
                if (Combogyártó.Text.Trim() == "") throw new HibásBevittAdat("A gyártó mező nem lehet üres");
                if (Combofajta.Text.Trim() == "") throw new HibásBevittAdat("A fajta mező nem lehet üres");
                if (Combotípus.Text.Trim() == "") throw new HibásBevittAdat("A típus mező nem lehet üres");
                if (TextMegjegyzés.Text.Trim() == "") TextMegjegyzés.Text = "_";
                if (!int.TryParse(Kapacitás_Alap.Text, out int kapacitás)) throw new HibásBevittAdat("A kapacitás mezőbe csak egész számot lehet rögzíteni.");
                Textgyáriszám.Text = Textgyáriszám.Text.ToUpper().Trim();
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Akkumulátor\akku.mdb";
                string jelszó = "kasosmiklós";
                string szöveg;

                AdatokAkkuListázás();

                Adat_Akkumulátor AdatAkku = (from a in AdatokAkku
                                             where a.Gyáriszám == Textgyáriszám.Text.Trim()
                                             select a).FirstOrDefault();

                if (AdatAkku == null)
                {
                    Textbeépítve.Text = "_";
                    Telephely_alap.Text = CmbTelephely.Text.Trim();
                    // Új
                    szöveg = "INSERT INTO akkutábla ";
                    szöveg += "(beépítve, fajta, gyártó, Gyáriszám, típus, garancia, gyártásiidő, státus, Megjegyzés, Módosításdátuma, kapacitás, Telephely)";
                    szöveg += " VALUES (";
                    szöveg += $"'{Textbeépítve.Text.Trim()}', "; //beépítve       ,
                    szöveg += $"'{Combofajta.Text.Trim()}', "; //fajta,
                    szöveg += $"'{Combogyártó.Text.Trim()}', "; //gyártó,
                    szöveg += $"'{Textgyáriszám.Text.Trim()}', "; //Gyáriszám,
                    szöveg += $"'{Combotípus.Text.Trim()}', "; //típus,
                    szöveg += $"'{Dgarancia.Value}', "; //garancia,
                    szöveg += $"'{Dgyártásiidő.Value}', "; //gyártásiidő,
                    szöveg += "1, "; //státus,
                    szöveg += $"'{TextMegjegyzés.Text.Trim()}', "; //Megjegyzés,
                    szöveg += $"'{DateTime.Today}', "; //Módosításdátuma,
                    szöveg += $"{kapacitás}, "; //kapacitás,
                    szöveg += $"'{CmbTelephely.Text.Trim()}' )"; //Telephely
                }
                else
                {
                    // Módosítás
                    szöveg = " UPDATE akkutábla SET ";
                    szöveg += $" fajta='{Combofajta.Text.Trim()}', ";
                    szöveg += $" gyártó='{Combogyártó.Text.Trim()}', ";
                    szöveg += $" típus='{Combotípus.Text.Trim()}', ";
                    szöveg += $" garancia='{Dgarancia.Value}', ";
                    szöveg += $" gyártásiidő='{Dgyártásiidő.Value}', ";
                    szöveg += $" Megjegyzés='{TextMegjegyzés.Text.Trim()}', ";
                    szöveg += $" Módosításdátuma='{DateTime.Today}', ";
                    szöveg += $" kapacitás={kapacitás} ";
                    szöveg += $" WHERE Gyáriszám='{Textgyáriszám.Text.Trim()}' ";
                }

                MyA.ABMódosítás(hely, jelszó, szöveg);

                // naplózás
                hely = Application.StartupPath + $@"\Főmérnökség\adatok\Akkumulátor\Akkunapló{DateTime.Now.Year}.mdb";
                szöveg = "INSERT INTO Akkutábla_Napló ";
                szöveg += "(beépítve, fajta, gyártó, Gyáriszám, típus, garancia, gyártásiidő, státus, Megjegyzés, Módosításdátuma, kapacitás, Telephely, Rögzítés, Rögzítő )";
                szöveg += " VALUES (";
                szöveg += $"'{Textbeépítve.Text.Trim()}', "; //beépítve       ,
                szöveg += $"'{Combofajta.Text.Trim()}', "; //fajta,
                szöveg += $"'{Combogyártó.Text.Trim()}', "; //gyártó,
                szöveg += $"'{Textgyáriszám.Text.Trim()}', "; //Gyáriszám,
                szöveg += $"'{Combotípus.Text.Trim()}', "; //típus,
                szöveg += $"'{Dgarancia.Value}', "; //garancia,
                szöveg += $"'{Dgyártásiidő.Value}', "; //gyártásiidő,
                szöveg += "1, "; //státus,
                szöveg += $"'{TextMegjegyzés.Text.Trim()}', "; //Megjegyzés,
                szöveg += $"'{DateTime.Today}', "; //Módosításdátuma,
                szöveg += $"{kapacitás}, "; //kapacitás,
                szöveg += $"'{Telephely_alap.Text.Trim()}', "; //Telephely
                szöveg += $"'{DateTime.Now}', "; //Rögzítés,
                szöveg += $"'{Program.PostásNév.Trim()}') "; //Rögzítő
                MyA.ABMódosítás(hely, jelszó, szöveg);

                MessageBox.Show("Az adatok rögzítése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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



        private void Combokfeltöltése()
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Akkumulátor\akku.mdb";
                string jelszó = "kasosmiklós";
                string szöveg = "SELECT DISTINCT gyártó FROM akkutábla ORDER BY gyártó";

                Kezelő_Általános_String Kéz = new Kezelő_Általános_String();
                List<string> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg, "gyártó");
                Combogyártó.Items.Clear();

                foreach (string Elem in Adatok)
                    Combogyártó.Items.Add(Elem);
                Combogyártó.Refresh();

                szöveg = "SELECT DISTINCT fajta FROM akkutábla ORDER BY fajta";
                Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg, "fajta");
                Combofajta.Items.Clear();

                foreach (string Elem in Adatok)
                    Combofajta.Items.Add(Elem);
                Combofajta.Refresh();

                szöveg = "SELECT DISTINCT típus FROM akkutábla ORDER BY típus";
                Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg, "típus");
                Combotípus.Items.Clear();

                foreach (string Elem in Adatok)
                    Combotípus.Items.Add(Elem);
                Combotípus.Refresh();
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


        #region Akku_listázás
        private void Akku_Tábla_Listázás_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Akkumulátor\akku.mdb";
                string jelszó = "kasosmiklós";
                Holtart.Be();
                Tábla2.Rows.Clear();
                Tábla2.Columns.Clear();
                Tábla2.Refresh();
                Tábla2.Visible = false;
                Tábla2.ColumnCount = 12;

                // fejléc elkészítése
                Tábla2.Columns[0].HeaderText = "Gyáriszám";
                Tábla2.Columns[0].Width = 120;
                Tábla2.Columns[1].HeaderText = "Státus";
                Tábla2.Columns[1].Width = 120;
                Tábla2.Columns[2].HeaderText = "Pályaszám";
                Tábla2.Columns[2].Width = 120;
                Tábla2.Columns[3].HeaderText = "Módosítás dátuma";
                Tábla2.Columns[3].Width = 120;
                Tábla2.Columns[4].HeaderText = "Megjegyzés";
                Tábla2.Columns[4].Width = 120;
                Tábla2.Columns[5].HeaderText = "Fajta";
                Tábla2.Columns[5].Width = 120;
                Tábla2.Columns[6].HeaderText = "Gyártó";
                Tábla2.Columns[6].Width = 120;
                Tábla2.Columns[7].HeaderText = "Típus";
                Tábla2.Columns[7].Width = 120;
                Tábla2.Columns[8].HeaderText = "Garancia dátuma";
                Tábla2.Columns[8].Width = 120;
                Tábla2.Columns[9].HeaderText = "Gyártási idő";
                Tábla2.Columns[9].Width = 120;
                Tábla2.Columns[10].HeaderText = "Kapacitás";
                Tábla2.Columns[10].Width = 120;
                Tábla2.Columns[11].HeaderText = "Telephely";
                Tábla2.Columns[11].Width = 120;



                int jel = 0;
                string szöveg = "SELECT * FROM akkutábla ";
                if (ComboStátuslek.Text.Trim() != "" || TextPszlek.Text.Trim() != "" || txtgyáriszám.Text.Trim() != "" || Telephely_Szűrő.Text.Trim() != "")
                    szöveg += " Where ";

                if (ComboStátuslek.Text.Trim() != "")
                {
                    szöveg += " státus=" + ComboStátuslek.Text.Trim().Substring(0, 1);
                    jel = 1;
                }

                if (TextPszlek.Text.Trim() != "" & jel == 1)
                {
                    szöveg += " AND ";
                    jel = 0;
                }
                if (TextPszlek.Text.Trim() != "")
                {
                    szöveg += " beépítve='" + TextPszlek.Text.Trim() + "'";
                    jel = 1;
                }

                if (txtgyáriszám.Text.Trim() != "" & jel == 1)
                {
                    szöveg += " AND ";
                    jel = 0;
                }
                if (txtgyáriszám.Text.Trim() != "")
                {
                    szöveg += "  gyáriszám Like '%" + txtgyáriszám.Text.Trim() + "%'";
                    jel = 1;
                }

                if (Telephely_Szűrő.Text.Trim() != "" & jel == 1)
                {
                    szöveg += " AND ";
                    jel = 0;
                }
                if (Telephely_Szűrő.Text.Trim() != "")
                {
                    szöveg += $"  telephely='{Telephely_Szűrő.Text.Trim()}'";
                    jel = 1;
                }
                szöveg += " order by gyáriszám desc";


                Kezelő_Akkumulátor Kéz = new Kezelő_Akkumulátor();
                List<Adat_Akkumulátor> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Akkumulátor rekord in Adatok)
                {
                    Tábla2.RowCount++;
                    int i = Tábla2.RowCount - 1;
                    Tábla2.Rows[i].Cells[0].Value = rekord.Gyáriszám.Trim();
                    Tábla2.Rows[i].Cells[5].Value = rekord.Fajta.Trim();
                    Tábla2.Rows[i].Cells[6].Value = rekord.Gyártó.Trim();
                    Tábla2.Rows[i].Cells[7].Value = rekord.Típus.Trim();
                    Tábla2.Rows[i].Cells[8].Value = rekord.Garancia.ToString("yyyy.MM.dd");
                    Tábla2.Rows[i].Cells[9].Value = rekord.Gyártásiidő.ToString("yyyy.MM.dd");
                    Tábla2.Rows[i].Cells[1].Value = $"{rekord.Státus} - {Enum.GetName(typeof(MyEn.Akku_Státus), rekord.Státus)}";
                    Tábla2.Rows[i].Cells[2].Value = rekord.Beépítve.Trim();
                    Tábla2.Rows[i].Cells[3].Value = rekord.Módosításdátuma.ToString("yyyy.MM.dd");
                    Tábla2.Rows[i].Cells[4].Value = rekord.Megjegyzés.Trim();
                    Tábla2.Rows[i].Cells[10].Value = rekord.Kapacitás;
                    Tábla2.Rows[i].Cells[11].Value = rekord.Telephely.Trim();
                    Holtart.Lép();
                }
                Tábla2.Refresh();
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


        private void AlapListaComboFeltöltés()
        {
            ComboStátuslek.Items.Clear();
            ComboStátuslek.Items.Add("");
            ComboStátuslek.Items.Add("1 - Új");
            ComboStátuslek.Items.Add("2 - Beépített");
            ComboStátuslek.Items.Add("3 - Használt");
            ComboStátuslek.Items.Add("4 - Seletezésre javasolt");
            ComboStátuslek.Items.Add("5 - Leselejtezett");
            ComboStátuslek.Items.Add("9 - Törölt");


            //Telephely_Szűrő
            string hely = Application.StartupPath + @"\Főmérnökség\adatok\Akkumulátor\akku.mdb";
            string jelszó = "kasosmiklós";
            string szöveg = "SELECT DISTINCT telephely FROM akkutábla ORDER BY telephely";

            Kezelő_Általános_String Kéz = new Kezelő_Általános_String();
            List<string> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg, "Telephely");
            Telephely_Szűrő.Items.Clear();

            foreach (string Elem in Adatok)
                Telephely_Szűrő.Items.Add(Elem);
            Telephely_Szűrő.Refresh();
        }


        private void ExcelAlapLista_Click(object sender, EventArgs e)
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
                    FileName = $"Akkumulátorok_listája_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
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

        private void Tábla2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                if (Tábla2.Columns[0].HeaderText == "Gyáriszám")
                {
                    string küld = Tábla2.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (küld.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva érvényes adat, így nem lehet kiírni az alapadotkat.");

                    Alap_Egy_kiírás(küld);
                    Fülek.SelectedIndex = 0;
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


        private void Teljesség_Click(object sender, EventArgs e)
        {
            try
            {
                PályaszámListaFeltöltés(true);
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Akkumulátor\akku.mdb";
                string jelszó = "kasosmiklós";
                Holtart.Be();
                Tábla2.Rows.Clear();
                Tábla2.Columns.Clear();
                Tábla2.Refresh();
                //   Tábla2.Visible = false;
                Tábla2.ColumnCount = 1;

                Tábla2.Columns[0].HeaderText = "Pályaszám";
                Tábla2.Columns[0].Width = 100;


                string szöveg = "SELECT * FROM akkutábla WHERE Beépítve<>'_' ";
                if (!Program.Postás_Vezér)
                    szöveg += $" AND telephely='{CmbTelephely.Text.Trim()}'";

                szöveg += " ORDER BY Beépítve";

                Kezelő_Akkumulátor Kéz = new Kezelő_Akkumulátor();
                List<Adat_Akkumulátor> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                int oszlop = 0;
                foreach (Adat_Jármű rekord in Adatok_Jármű)
                {
                    Tábla2.RowCount++;
                    int i = Tábla2.Rows.Count - 1;
                    Tábla2.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();
                    List<Adat_Akkumulátor> Akkuk = (from a in Adatok
                                                    where a.Beépítve.Trim() == rekord.Azonosító.Trim()
                                                    select a).ToList();
                    if (Akkuk != null && Akkuk.Count > 0)
                    {
                        foreach (Adat_Akkumulátor elem in Akkuk)
                        {
                            oszlop++;
                            if (Tábla2.Columns.Count <= oszlop)
                            {
                                Tábla2.ColumnCount++;
                                Tábla2.Columns[oszlop].HeaderText = "Gyáriszám " + oszlop;
                                Tábla2.Columns[oszlop].Width = 140;
                            }

                            Tábla2.Rows[i].Cells[oszlop].Value = elem.Gyáriszám.Trim();
                            Holtart.Lép();
                        }
                        oszlop = 0;
                    }
                }

                Holtart.Ki();
                Tábla2.Refresh();
                Tábla2.Visible = true;
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


        private void Mérés_Click(object sender, EventArgs e)
        {
            try
            {

                Holtart.Be();
                Tábla2.Rows.Clear();
                Tábla2.Columns.Clear();
                Tábla2.Refresh();
                Tábla2.Visible = false;
                Tábla2.ColumnCount = 9;

                Tábla2.Columns[0].HeaderText = "Gyáriszám";
                Tábla2.Columns[0].Width = 140;
                Tábla2.Columns[1].HeaderText = "Pályaszám";
                Tábla2.Columns[1].Width = 120;
                Tábla2.Columns[2].HeaderText = "Névleges kapacitás";
                Tábla2.Columns[2].Width = 120;
                Tábla2.Columns[3].HeaderText = "Utolsó kapacitás";
                Tábla2.Columns[3].Width = 120;
                Tábla2.Columns[4].HeaderText = "%-os érték";
                Tábla2.Columns[4].Width = 120;
                Tábla2.Columns[5].HeaderText = "Beépítés dátuma";
                Tábla2.Columns[5].Width = 120;
                Tábla2.Columns[6].HeaderText = "Utolsó mérés dátuma";
                Tábla2.Columns[6].Width = 120;
                Tábla2.Columns[7].HeaderText = "Státus";
                Tábla2.Columns[7].Width = 120;
                Tábla2.Columns[8].HeaderText = "Telephely";
                Tábla2.Columns[8].Width = 120;

                string szöveg = "SELECT * FROM akkutábla WHERE státus<4 ";
                if (!Program.Postás_Vezér)
                    szöveg += $" AND telephely='{CmbTelephely.Text.Trim()}'";

                szöveg += " ORDER BY Beépítve";
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Akkumulátor\akku.mdb";
                string jelszó = "kasosmiklós";
                Kezelő_Akkumulátor Kéz = new Kezelő_Akkumulátor();
                List<Adat_Akkumulátor> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                Kezelő_Akkumulátor_Mérés Kézmérés = new Kezelő_Akkumulátor_Mérés();
                szöveg = "SELECT * FROM méréstábla WHERE Rögzítő<>'TÖRÖLT'  ORDER BY gyáriszám, Mérésdátuma asc";

                hely = Application.StartupPath + $@"\Főmérnökség\adatok\Akkumulátor\Akkunapló{Dátumtól.Value.Year}.mdb";
                if (!Exists(hely))
                    Adatbázis_Létrehozás.Akku_Mérés(hely);
                List<Adat_Akkumulátor_Mérés> AdatokMérés = Kézmérés.Lista_Adatok(hely, jelszó, szöveg);

                hely = Application.StartupPath + $@"\Főmérnökség\adatok\Akkumulátor\Akkunapló{Dátumtól.Value.AddYears(-1).Year}.mdb";
                if (!Exists(hely))
                    Adatbázis_Létrehozás.Akku_Mérés(hely);
                List<Adat_Akkumulátor_Mérés> AdatokMérés1 = Kézmérés.Lista_Adatok(hely, jelszó, szöveg);

                hely = Application.StartupPath + $@"\Főmérnökség\adatok\Akkumulátor\Akkunapló{Dátumtól.Value.AddYears(-2).Year}.mdb";
                if (!Exists(hely))
                    Adatbázis_Létrehozás.Akku_Mérés(hely);
                List<Adat_Akkumulátor_Mérés> AdatokMérés2 = Kézmérés.Lista_Adatok(hely, jelszó, szöveg);




                foreach (Adat_Akkumulátor rekord in Adatok)
                {
                    Tábla2.RowCount++;
                    int i = Tábla2.RowCount - 1;
                    Tábla2.Rows[i].Cells[0].Value = rekord.Gyáriszám.Trim();
                    Tábla2.Rows[i].Cells[1].Value = rekord.Beépítve.Trim();
                    Tábla2.Rows[i].Cells[2].Value = rekord.Kapacitás;
                    Tábla2.Rows[i].Cells[5].Value = rekord.Módosításdátuma.ToString("yyyy.MM.dd");
                    Tábla2.Rows[i].Cells[7].Value = $"{rekord.Státus} - {Enum.GetName(typeof(MyEn.Akku_Státus), rekord.Státus)}";
                    Tábla2.Rows[i].Cells[8].Value = rekord.Telephely.Trim();

                    var eredmény = (AdatokMérés.FirstOrDefault(a => a.Gyáriszám.Trim() == rekord.Gyáriszám.Trim()))
                         ?? (AdatokMérés1.FirstOrDefault(a => a.Gyáriszám.Trim() == rekord.Gyáriszám.Trim()))
                         ?? (AdatokMérés2.FirstOrDefault(a => a.Gyáriszám.Trim() == rekord.Gyáriszám.Trim()));

                    if (eredmény != null)
                    {

                        Tábla2.Rows[i].Cells[3].Value = eredmény.Kapacitás;
                        Tábla2.Rows[i].Cells[4].Value = Math.Round(eredmény.Kapacitás / (double)rekord.Kapacitás, 4) * 100;
                        Tábla2.Rows[i].Cells[6].Value = eredmény.Mérésdátuma.ToString("yyyy.MM.dd");
                    }
                    Holtart.Lép();
                }
                Tábla2.Refresh();
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



        #endregion



        #region Mérések Listázása
        private void BtnMéréslista_Click(object sender, EventArgs e)
        {
            Mérési_lista();
        }


        void Mérési_lista()
        {
            try
            {
                string hely = Application.StartupPath + $@"\Főmérnökség\adatok\Akkumulátor\Akkunapló{Dátumtól.Value.Year}.mdb";
                string jelszó = "kasosmiklós";
                if (!Exists(hely)) throw new HibásBevittAdat("Ebben az évben nem volt még mérés.");

                Tábla4.Rows.Clear();
                Tábla4.Columns.Clear();
                Tábla4.Refresh();
                Tábla4.Visible = false;
                Tábla4.ColumnCount = 15;

                // fejléc elkészítése
                Tábla4.Columns[0].HeaderText = "Gyáriszám";
                Tábla4.Columns[0].Width = 120;
                Tábla4.Columns[1].HeaderText = "Kisütési áram";
                Tábla4.Columns[1].Width = 120;
                Tábla4.Columns[2].HeaderText = "Kezdeti fesz";
                Tábla4.Columns[2].Width = 120;
                Tábla4.Columns[3].HeaderText = "Vég fesz";
                Tábla4.Columns[3].Width = 120;
                Tábla4.Columns[4].HeaderText = "Kisütési idő";
                Tábla4.Columns[4].Width = 120;
                Tábla4.Columns[5].HeaderText = "Kapacitás";
                Tábla4.Columns[5].Width = 120;
                Tábla4.Columns[6].HeaderText = "12V/24V mérés";
                Tábla4.Columns[6].Width = 120;
                Tábla4.Columns[7].HeaderText = "Megjegyzés";
                Tábla4.Columns[7].Width = 120;
                Tábla4.Columns[8].HeaderText = "Mérés dátuma";
                Tábla4.Columns[8].Width = 120;
                Tábla4.Columns[9].HeaderText = "Sorszám";
                Tábla4.Columns[9].Width = 120;
                Tábla4.Columns[10].HeaderText = "Pályaszám";
                Tábla4.Columns[10].Width = 120;
                Tábla4.Columns[11].HeaderText = "Rögzítette";
                Tábla4.Columns[11].Width = 120;
                Tábla4.Columns[12].HeaderText = "Rögzítés";
                Tábla4.Columns[12].Width = 120;
                Tábla4.Columns[13].HeaderText = "Névleges kapacitás";
                Tábla4.Columns[13].Width = 120;
                Tábla4.Columns[14].HeaderText = "%-os";
                Tábla4.Columns[14].Width = 120;

                string szöveg = "SELECT * FROM méréstábla WHERE ";
                szöveg += $" [mérésdátuma]>=#{Dátumtól.Value:M-d-yy} 00:00:0#";
                szöveg += $" and [mérésdátuma]<#{dátumig.Value:M-d-yy} 23:59:0#";
                szöveg += " AND Rögzítő<>'TÖRÖLT' ";
                if (MérésLekGyári.Text.Trim() != "")
                    szöveg += " AND  gyáriszám Like '%" + MérésLekGyári.Text.Trim() + "%'";
                szöveg += " ORDER BY gyáriszám";

                Kezelő_Akkumulátor_Mérés Kéz = new Kezelő_Akkumulátor_Mérés();
                List<Adat_Akkumulátor_Mérés> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                hely = Application.StartupPath + @"\Főmérnökség\adatok\Akkumulátor\akku.mdb";
                szöveg = "SELECT * FROM akkutábla ORDER BY gyáriszám";
                Kezelő_Akkumulátor Kéz_alap = new Kezelő_Akkumulátor();
                List<Adat_Akkumulátor> Adatok_Alap = Kéz_alap.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Akkumulátor_Mérés rekord in Adatok)
                {
                    Tábla4.RowCount++;
                    int i = Tábla4.RowCount - 1;
                    Tábla4.Rows[i].Cells[0].Value = rekord.Gyáriszám.Trim();
                    Tábla4.Rows[i].Cells[1].Value = rekord.Kisütésiáram;
                    Tábla4.Rows[i].Cells[2].Value = rekord.Kezdetifesz;
                    Tábla4.Rows[i].Cells[3].Value = rekord.Végfesz;
                    Tábla4.Rows[i].Cells[4].Value = rekord.Kisütésiidő.ToString("hh:mm");
                    Tábla4.Rows[i].Cells[5].Value = rekord.Kapacitás;
                    Tábla4.Rows[i].Cells[7].Value = rekord.Megjegyzés.Trim();
                    Tábla4.Rows[i].Cells[8].Value = rekord.Mérésdátuma.ToString("yyyy.MM.dd");
                    Tábla4.Rows[i].Cells[9].Value = rekord.Id;

                    Tábla4.Rows[i].Cells[11].Value = rekord.Rögzítő.Trim();
                    Tábla4.Rows[i].Cells[12].Value = rekord.Rögzítés.ToString("yyyy.MM.dd");

                    if (rekord.Van.Trim() == "0")
                        Tábla4.Rows[i].Cells[6].Value = "12V";
                    else
                        Tábla4.Rows[i].Cells[6].Value = "24V";

                    Adat_Akkumulátor Elem = (from a in Adatok_Alap
                                             where a.Gyáriszám == rekord.Gyáriszám
                                             select a).FirstOrDefault();
                    if (Elem != null)

                    {
                        Tábla4.Rows[i].Cells[10].Value = Elem.Beépítve;
                        Tábla4.Rows[i].Cells[13].Value = Elem.Kapacitás;
                        if (rekord.Kapacitás != 0)
                        {
                            Tábla4.Rows[i].Cells[14].Value = Math.Round(Elem.Kapacitás.ToÉrt_Double() / rekord.Kapacitás.ToÉrt_Double() * 100, 1);
                        }
                    }
                }

                Tábla4.Visible = true;
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


        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {

                if (Tábla4.Rows.Count <= 0)
                    return;
                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Akkumulátorok_mérési_listája_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, Tábla4, true);

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

        private void R_törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla4.SelectedRows.Count < 0) throw new HibásBevittAdat("Nincs kijelölve egy törlendő elem sem.");

                string hely = Application.StartupPath + $@"\Főmérnökség\adatok\Akkumulátor\Akkunapló{Dátumtól.Value.Year}.mdb";
                string jelszó = "kasosmiklós";

                AdatokAkkuMérListázás();
                if (AdatokAkkuMér == null) return;
                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < Tábla4.SelectedRows.Count; i++)
                {
                    if (!int.TryParse(Tábla4.Rows[i].Cells[9].Value.ToString(), out int ID))
                        ID = 0;
                    Adat_Akkumulátor_Mérés AdatAkkuMér = (from a in AdatokAkkuMér
                                                          where a.Id == ID
                                                          select a).FirstOrDefault();
                    if (AdatAkkuMér != null)
                    {
                        string szöveg = $"UPDATE  méréstábla SET Rögzítő='TÖRÖLT' WHERE ID={ID}";
                        SzövegGy.Add(szöveg);
                    }
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
                MessageBox.Show("Az adatok törlése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Mérési_lista();
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



        #region Mérés_Rögzítés
        private void Btnrögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Textgyárimérés.Text.Trim() == "") throw new HibásBevittAdat("A gyári szám mezőt ki kell tölteni.");
                if (Textmérkisütésiáram.Text.Trim() == "") throw new HibásBevittAdat("A kisütési áram mezőt ki kell tölteni.");
                if (Textmérkezdetifesz.Text.Trim() == "") throw new HibásBevittAdat("A kezdeti feszültség mezőt ki kell tölteni.");
                if (Textmérvégfesz.Text.Trim() == "") throw new HibásBevittAdat("A Vég feszültség mezőt ki kell tölteni.");
                if (Textmérkapacitás.Text.Trim() == "") throw new HibásBevittAdat("A Kapacitás mezőt ki kell tölteni.");
                if (!double.TryParse(Textmérkisütésiáram.Text, out double Kisütési)) throw new HibásBevittAdat("A kisütési áram mezőnek számnak kell lennie.");
                if (!double.TryParse(Textmérkezdetifesz.Text, out double KezdetiFesz)) throw new HibásBevittAdat("A kezdeti feszültség mezőnek számnak kell lennie.");
                if (!double.TryParse(Textmérvégfesz.Text, out double VégFesz)) throw new HibásBevittAdat("A  vég feszültség mezőnek számnak kell lennie.");
                if (!double.TryParse(Textmérkapacitás.Text, out double Kapacitás)) throw new HibásBevittAdat("A Kapacitás mezőnek számnak kell lennie.");
                if (TextMérmegjegyzés.Text.Trim() == "") TextMérmegjegyzés.Text = "_";

                Textgyárimérés.Text = Textgyárimérés.Text.Trim().ToUpper();

                string jelszó = "kasosmiklós";
                string szöveg;

                AdatokAkkuListázás();
                if (AdatokAkku == null) return;

                Akkuszám_ellenőrzés();


                Adat_Akkumulátor AdatAkku = (from a in AdatokAkku
                                             where a.Gyáriszám == Textgyárimérés.Text.Trim()
                                             select a).FirstOrDefault();


                string hely = Application.StartupPath + $@"\Főmérnökség\adatok\Akkumulátor\Akkunapló{MérésDátuma.Value.Year}.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Akku_Mérés(hely);

                AdatokAkkuMérListázás();

                long id = 1;
                if (AdatokAkkuMér != null) id = AdatokAkkuMér.Max(a => a.Id) + 1;

                szöveg = "INSERT INTO méréstábla ";
                szöveg += "(Gyáriszám, Kisütésiáram, Kezdetifesz, Végfesz, Kisütésiidő, Kapacitás, Megjegyzés, Van, Mérésdátuma, Rögzítés, Rögzítő, id)";
                szöveg += " VALUES (";
                szöveg += $"'{Textgyárimérés.Text.Trim()}', ";//Gyáriszám
                szöveg += $"{Kisütési.ToString().Replace(',', '.')}, ";//kisütésiáram
                szöveg += $"{KezdetiFesz.ToString().Replace(',', '.')}, ";//kezdetifesz
                szöveg += $"{VégFesz.ToString().Replace(',', '.')}, ";//végfesz]
                szöveg += $"'{mérdátum.Value}', ";//kisütésiidő
                szöveg += $" {Kapacitás.ToString().Replace(',', '.')}, ";//kapacitás
                szöveg += $"'{TextMérmegjegyzés.Text.Trim()}', ";//Megjegyzés
                if (Check1.Checked)//van
                    szöveg += " '1', ";
                else
                    szöveg += " '0', ";
                szöveg += $"'{MérésDátuma.Value:yyyy.MM.dd}', ";//Mérésdátuma
                szöveg += $"'{DateTime.Now}', ";//Rögzítés
                szöveg += $"'{Program.PostásNév.Trim()}', ";//Rögzítő
                szöveg += $"{id})";//id
                MyA.ABMódosítás(hely, jelszó, szöveg);

                MessageBox.Show("Az adatok rögzítése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void Akkuszám_ellenőrzés()
        {
            List<string> Elem = (from a in AdatokAkku
                                 where a.Gyáriszám.Contains(Textgyárimérés.Text.Trim())
                                 select a.Gyáriszám).ToList();
            if (Elem == null || Elem.Count == 0) throw new HibásBevittAdat("Nincs ilyen gyáriszámú akkumulátor!");
            if (Elem.Count == 1) Textgyárimérés.Text = Elem[0];
            if (Elem.Count > 1) throw new HibásBevittAdat($"Kérem pontosítsd, melyik akkumulátorra gondolsz:\n {string.Join("\n ", Elem)}");
        }

        private void Btnmérúj_Click(object sender, EventArgs e)
        {
            Méréshez_új();
        }

        private void Résztörlés_Click(object sender, EventArgs e)
        {
            Textgyárimérés.Text = "";
            Textgyáriszám.Text = "";
            Textmérkezdetifesz.Text = "";
            mérdátum.Value = new DateTime(2020, 1, 1, 0, 0, 0);
            Textmérkapacitás.Text = "";
            TextMérmegjegyzés.Text = "";
        }

        private void Méréshez_új()
        {
            try
            {
                Textgyárimérés.Text = "";
                Textgyáriszám.Text = "";
                Textmérkisütésiáram.Text = "";
                Textmérkezdetifesz.Text = "";
                Textmérvégfesz.Text = "";
                mérdátum.Value = new DateTime(2020, 1, 1, 0, 0, 0);
                Textmérkapacitás.Text = "";
                TextMérmegjegyzés.Text = "";
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

        private void TelephelyEllenőr_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();

                Kezelő_Akkumulátor KézAkku = new Kezelő_Akkumulátor();
                PályaszámListaFeltöltés(false);

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Akkumulátor\akku.mdb";
                string jelszó = "kasosmiklós";
                string szöveg = "SELECT * FROM akkutábla where Státus=2 ORDER BY gyáriszám";
                List<Adat_Akkumulátor> Adatok_Akku = KézAkku.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Akkumulátor Rekord in Adatok_Akku)
                {
                    var eredmény = Adatok_Jármű.FirstOrDefault(a => a.Azonosító.Trim() == Rekord.Beépítve.Trim());
                    if (eredmény != null)
                    {
                        if (eredmény.Üzem.Trim() != Rekord.Telephely.Trim())
                        {
                            Telephely_Változás(eredmény.Üzem.Trim(), Rekord.Gyáriszám.Trim());
                            Telephely_Változás_Napló(eredmény.Üzem.Trim(), Rekord.Gyáriszám.Trim());
                        }
                    }

                    Holtart.Lép();
                }
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

        void Telephely_Változás(string Telep, string gyáriszám)
        {
            string szöveg = "", hely = "";
            try
            {
                hely = Application.StartupPath + @"\Főmérnökség\adatok\Akkumulátor\akku.mdb";
                string jelszó = "kasosmiklós";
                szöveg = " UPDATE akkutábla SET ";
                szöveg += $" telephely='{Telep.Trim()}' ";
                szöveg += $" WHERE Gyáriszám='{gyáriszám.Trim()}' ";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString() + $"\n{hely},\n{szöveg}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        void Telephely_Változás_Napló(string Telep, string gyáriszám)
        {
            string hely = Application.StartupPath + $@"\Főmérnökség\adatok\Akkumulátor\Akkunapló{DateTime.Now.Year}.mdb";
            string jelszó = "kasosmiklós";
            string szöveg = "INSERT INTO Akkutábla_Napló ";
            szöveg += "(beépítve, fajta, gyártó, Gyáriszám, típus, garancia, gyártásiidő, státus, Megjegyzés, Módosításdátuma, kapacitás, Telephely, Rögzítés, Rögzítő )";
            szöveg += " VALUES (";
            szöveg += $"'_', "; //beépítve       ,
            szöveg += $"'_', "; //fajta,
            szöveg += $"'_', "; //gyártó,
            szöveg += $"'{gyáriszám.Trim()}', "; //Gyáriszám,
            szöveg += $"'_', "; //típus,
            szöveg += $"'{new DateTime(1900, 1, 1)}', "; //garancia,
            szöveg += $"'{new DateTime(1900, 1, 1)}', "; //gyártásiidő,
            szöveg += $"0, "; //státus,
            szöveg += $"'Telephely változás', "; //Megjegyzés,
            szöveg += $"'{new DateTime(1900, 1, 1)}', "; //Módosításdátuma,
            szöveg += $"0, "; //kapacitás,
            szöveg += $"'{Telep}', "; //Telephely
            szöveg += $"'{DateTime.Now}', "; //Rögzítés,
            szöveg += $"'{Program.PostásNév.Trim()}') "; //Rögzítő
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        #endregion

        #region Kiépítés-Beépítés

        void Beép_Státus_Feltöltés()
        {

            Beép_Státus.Items.Clear();
            Beép_Státus.Items.Add("");

            foreach (MyEn.Akku_Státus adat in (MyEn.Akku_Státus[])Enum.GetValues(typeof(MyEn.Akku_Státus)))
                Beép_Státus.Items.Add($"{((MyEn.Akku_Státus)Enum.Parse(typeof(MyEn.Akku_Státus), adat.ToString())).GetHashCode()} - {adat}");
            Beép_Státus.Refresh();

        }

        private void Beép_Pályaszám_fetöltés()
        {
            try
            {
                Beép_Psz.Items.Clear();
                PályaszámListaFeltöltés(false);
                foreach (Adat_Jármű rekord in Adatok_Jármű)
                    Beép_Psz.Items.Add(rekord.Azonosító);
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

        private void PályaszámListaFeltöltés(bool kell)
        {
            try
            {
                string hely, jelszó, szöveg;
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    hely = Application.StartupPath + @"\Főmérnökség\Adatok\villamos.mdb";
                    jelszó = "pozsgaii";
                    szöveg = "SELECT * FROM állománytábla where törölt=0 order by  azonosító";
                }
                else
                {
                    hely = $@"{Application.StartupPath}\" + CmbTelephely.Text.Trim() + @"\Adatok\villamos\villamos.mdb";
                    jelszó = "pozsgaii";
                    szöveg = "SELECT * FROM állománytábla where törölt=0 order by  azonosító";
                }

                if (kell)
                {
                    hely = $@"{Application.StartupPath}\" + CmbTelephely.Text.Trim() + @"\Adatok\villamos\villamos.mdb";
                    jelszó = "pozsgaii";
                    szöveg = "SELECT * FROM állománytábla where törölt=0 order by  azonosító";
                }

                Adatok_Jármű.Clear();
                Adatok_Jármű = Kéz_Jármű.Lista_Adatok(hely, jelszó, szöveg);
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

        private void Beép_Frissít_Click(object sender, EventArgs e)
        {
            Beépítés_Listázás();
        }


        void Beépítés_Listázás()
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Akkumulátor\akku.mdb";
                string jelszó = "kasosmiklós";
                bool Volt = false;
                int státus;

                if (Beép_Státus.Text.Trim() == "")
                    státus = 0;
                else if (!int.TryParse(Beép_Státus.Text.Substring(0, 1), out státus))
                    státus = 0;

                string szöveg = "SELECT * FROM akkutábla ";
                if (státus != 0 || !Program.Postás_Vezér)
                    szöveg += " WHERE ";

                if (Beép_Státus.Text.Trim() != "")
                {
                    szöveg += $" státus={Beép_Státus.Text.Substring(0, 1)}";
                    Volt = true;
                }



                if (Beép_Gyári.Text.Trim() != "")
                {
                    if (Volt == true)
                    {
                        szöveg += " AND ";
                        Volt = false;
                    }
                    szöveg += $"  gyáriszám Like '%{Beép_Gyári.Text.Trim()}%'";
                    Volt = true;
                }


                if (!Program.Postás_Vezér)
                {
                    if (Volt == true)
                    {
                        szöveg += " AND ";
                        Volt = false;
                    }
                    szöveg += $" Telephely='{CmbTelephely.Text.Trim()}'";
                    Volt = true;
                }

                if (státus == 2 && Beép_Psz.Text.Trim() != "")
                {
                    if (Volt == true)
                    {
                        szöveg += " AND ";
                        Volt = false;
                    }
                    szöveg += $" beépítve='{Beép_Psz.Text.Trim()}'";
                }

                szöveg += " ORDER BY gyáriszám";

                Holtart.Be();
                Tábla_Beép.Rows.Clear();
                Tábla_Beép.Columns.Clear();
                Tábla_Beép.Refresh();
                Tábla_Beép.Visible = false;
                Tábla_Beép.ColumnCount = 12;

                // fejléc elkészítése
                Tábla_Beép.Columns[0].HeaderText = "Gyáriszám";
                Tábla_Beép.Columns[0].Width = 160;
                Tábla_Beép.Columns[1].HeaderText = "Státus";
                Tábla_Beép.Columns[1].Width = 180;
                Tábla_Beép.Columns[2].HeaderText = "Pályaszám";
                Tábla_Beép.Columns[2].Width = 120;
                Tábla_Beép.Columns[3].HeaderText = "Módosítás dátuma";
                Tábla_Beép.Columns[3].Width = 120;
                Tábla_Beép.Columns[4].HeaderText = "Megjegyzés";
                Tábla_Beép.Columns[4].Width = 120;
                Tábla_Beép.Columns[5].HeaderText = "Fajta";
                Tábla_Beép.Columns[5].Width = 120;
                Tábla_Beép.Columns[6].HeaderText = "Gyártó";
                Tábla_Beép.Columns[6].Width = 120;
                Tábla_Beép.Columns[7].HeaderText = "Típus";
                Tábla_Beép.Columns[7].Width = 120;
                Tábla_Beép.Columns[8].HeaderText = "Garancia dátuma";
                Tábla_Beép.Columns[8].Width = 120;
                Tábla_Beép.Columns[9].HeaderText = "Gyártási idő";
                Tábla_Beép.Columns[9].Width = 120;
                Tábla_Beép.Columns[10].HeaderText = "Kapacitás";
                Tábla_Beép.Columns[10].Width = 120;
                Tábla_Beép.Columns[11].HeaderText = "Telephely";
                Tábla_Beép.Columns[11].Width = 120;


                Kezelő_Akkumulátor Kéz = new Kezelő_Akkumulátor();
                List<Adat_Akkumulátor> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                foreach (Adat_Akkumulátor rekord in Adatok)
                {
                    Tábla_Beép.RowCount++;
                    int i = Tábla_Beép.RowCount - 1;
                    Tábla_Beép.Rows[i].Cells[0].Value = rekord.Gyáriszám.Trim();
                    Tábla_Beép.Rows[i].Cells[5].Value = rekord.Fajta.Trim();
                    Tábla_Beép.Rows[i].Cells[6].Value = rekord.Gyártó.Trim();
                    Tábla_Beép.Rows[i].Cells[7].Value = rekord.Típus.Trim();
                    Tábla_Beép.Rows[i].Cells[8].Value = rekord.Garancia.ToString("yyyy.MM.dd");
                    Tábla_Beép.Rows[i].Cells[9].Value = rekord.Gyártásiidő.ToString("yyyy.MM.dd");
                    Tábla_Beép.Rows[i].Cells[1].Value = $"{rekord.Státus} - {Enum.GetName(typeof(MyEn.Akku_Státus), rekord.Státus)}";
                    Tábla_Beép.Rows[i].Cells[2].Value = rekord.Beépítve.Trim();
                    Tábla_Beép.Rows[i].Cells[3].Value = rekord.Módosításdátuma.ToString("yyyy.MM.dd");
                    Tábla_Beép.Rows[i].Cells[4].Value = rekord.Megjegyzés.Trim();
                    Tábla_Beép.Rows[i].Cells[10].Value = rekord.Kapacitás;
                    Tábla_Beép.Rows[i].Cells[11].Value = rekord.Telephely.Trim();
                    Holtart.Lép();
                }
                Tábla_Beép.Refresh();
                Tábla_Beép.Visible = true;
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

        private void Beép_Státus_SelectedIndexChanged(object sender, EventArgs e)
        {
            StátusVálasztás();
        }


        void StátusVálasztás()
        {
            try
            {
                if (Beép_Státus.Text.Trim() == "")
                    Beép_Státus.Text = " ";
                if (!int.TryParse(Beép_Státus.Text.Substring(0, 1), out int státus)) státus = 0;

                //Pályaszám
                Gombok_Ki();

                switch (státus)
                {
                    case 1://új
                        KIBE_Panel.Visible = true;
                        Beépít.Visible = true;
                        break;
                    case 2://Beépített
                        Pályaszám_Szűrő.Visible = true;
                        Kiépít.Visible = true;
                        break;
                    case 3://Használt
                        KIBE_Panel.Visible = true;
                        Beépít.Visible = true;
                        SelejtElő.Visible = true;
                        break;
                    case 4://Selejt előkészítés
                        Használt.Visible = true;
                        Leselejtezett.Visible = true;
                        break;
                    case 5://Leselejtezett
                        SelejtElő.Visible = true;
                        Törölt.Visible = true;
                        break;
                    case 9://Törölt
                        Leselejtezett.Visible = true;
                        break;
                    default:
                        break;
                }
                Beépítés_Listázás();
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


        void Gombok_Ki()
        {

            Pályaszám_Szűrő.Visible = false;
            KIBE_Panel.Visible = false;
            Beépít.Visible = false;
            Kiépít.Visible = false;
            SelejtElő.Visible = false;
            Törölt.Visible = false;
            Leselejtezett.Visible = false;
            Használt.Visible = false;
        }


        private void Beépít_Click(object sender, EventArgs e)
        {
            try
            {
                if (BePSz.Text.Trim() == "") throw new HibásBevittAdat("Nincs megadva a jármű pályaszáma.");
                Adat_Jármű lekerd = (from a in Adatok_Jármű
                                     where a.Azonosító == BePSz.Text.Trim()
                                     select a).FirstOrDefault() ?? throw new HibásBevittAdat("Nincs a telephelyen ez a jármű.");
                if (Tábla_Beép.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kiválsztva akkumulátor beépítésre.");

                foreach (DataGridViewRow SOR in Tábla_Beép.SelectedRows)
                {
                    Státus_Módosítás(BePSz.Text.Trim(), 2, SOR.Cells[0].Value.ToString().Trim());
                    Státus_Módosítás_Napló(BePSz.Text.Trim(), 2, SOR.Cells[0].Value.ToString().Trim());

                }

                Beép_Gyári.Text = "";
                Beépítés_Listázás();

                MessageBox.Show("Az adatok rögzítése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        void Státus_Módosítás(string Hova, int státus, string gyáriszám)
        {
            string hely = Application.StartupPath + @"\Főmérnökség\adatok\Akkumulátor\akku.mdb";
            string jelszó = "kasosmiklós";

            string szöveg = " UPDATE akkutábla SET ";
            szöveg += $" beépítve='{Hova.Trim()}', ";
            szöveg += $" státus={státus}, ";
            szöveg += $" Módosításdátuma='{DateTime.Today:yyyy.MM.dd}' ";
            szöveg += $" WHERE Gyáriszám='{gyáriszám.Trim()}' ";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }


        void Státus_Módosítás_Napló(string Hova, int státus, string gyáriszám)
        {
            string hely = Application.StartupPath + $@"\Főmérnökség\adatok\Akkumulátor\Akkunapló{DateTime.Now.Year}.mdb";
            string jelszó = "kasosmiklós";
            string szöveg = "INSERT INTO Akkutábla_Napló ";
            szöveg += "(beépítve, fajta, gyártó, Gyáriszám, típus, garancia, gyártásiidő, státus, Megjegyzés, Módosításdátuma, kapacitás, Telephely, Rögzítés, Rögzítő )";
            szöveg += " VALUES (";
            szöveg += $"'{Hova.Trim()}', "; //beépítve       ,
            szöveg += $"'_', "; //fajta,
            szöveg += $"'_', "; //gyártó,
            szöveg += $"'{gyáriszám.Trim()}', "; //Gyáriszám,
            szöveg += $"'_', "; //típus,
            szöveg += $"'{new DateTime(1900, 1, 1)}', "; //garancia,
            szöveg += $"'{new DateTime(1900, 1, 1)}', "; //gyártásiidő,
            szöveg += $"{státus}, "; //státus,
            szöveg += $"'_', "; //Megjegyzés,
            szöveg += $"'{DateTime.Today:yyyy.MM.dd}', "; //Módosításdátuma,
            szöveg += $"0, "; //kapacitás,
            szöveg += $"'_', "; //Telephely
            szöveg += $"'{DateTime.Now}', "; //Rögzítés,
            szöveg += $"'{Program.PostásNév.Trim()}') "; //Rögzítő
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }


        private void Kiépít_Click(object sender, EventArgs e)
        {
            Használt_állapot();
        }


        void Használt_állapot()
        {
            try
            {
                if (Tábla_Beép.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kiválsztva akkumulátor.");

                foreach (DataGridViewRow SOR in Tábla_Beép.SelectedRows)
                {
                    Státus_Módosítás("_", 3, SOR.Cells[0].Value.ToString().Trim());
                    Státus_Módosítás_Napló("_", 3, SOR.Cells[0].Value.ToString().Trim());
                }

                Beépítés_Listázás();
                MessageBox.Show("Az adatok rögzítése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void Használt_Click(object sender, EventArgs e)
        {
            Használt_állapot();
        }


        private void SelejtElő_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_Beép.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kiválsztva akkumulátor.");


                foreach (DataGridViewRow SOR in Tábla_Beép.SelectedRows)
                {
                    Státus_Módosítás("_", 4, SOR.Cells[0].Value.ToString().Trim());
                    Státus_Módosítás_Napló("_", 4, SOR.Cells[0].Value.ToString().Trim());
                }

                Beépítés_Listázás();
                MessageBox.Show("Az adatok rögzítése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void Leselejtezett_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_Beép.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kiválsztva akkumulátor.");

                foreach (DataGridViewRow SOR in Tábla_Beép.SelectedRows)
                {
                    Státus_Módosítás("_", 5, SOR.Cells[0].Value.ToString().Trim());
                    Státus_Módosítás_Napló("_", 5, SOR.Cells[0].Value.ToString().Trim());
                }

                Beépítés_Listázás();
                MessageBox.Show("Az adatok rögzítése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void Törölt_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_Beép.SelectedRows.Count < 1)
                    throw new HibásBevittAdat("Nincs kiválsztva akkumulátor.");

                foreach (DataGridViewRow SOR in Tábla_Beép.SelectedRows)
                {
                    Státus_Módosítás("_", 9, SOR.Cells[0].Value.ToString().Trim());
                    Státus_Módosítás_Napló("_", 9, SOR.Cells[0].Value.ToString().Trim());
                }

                Beépítés_Listázás();
                MessageBox.Show("Az adatok rögzítése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        #region Listák feltöltése

        private void AdatokAkkuMérListázás()
        {
            try
            {
                AdatokAkkuMér.Clear();

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Akkumulátor\Akkunapló{Dátumtól.Value.Year}.mdb";
                string jelszó = "kasosmiklós";
                string szöveg = "SELECT * FROM méréstábla";

                AdatokAkkuMér = KézAkkuMér.Lista_Adatok(hely, jelszó, szöveg);
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

        private void AdatokAkkuListázás()
        {
            try
            {
                AdatokAkku.Clear();

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Akkumulátor\akku.mdb";
                string jelszó = "kasosmiklós";
                string szöveg = "SELECT * FROM akkutábla";

                AdatokAkku = KézAkku.Lista_Adatok(hely, jelszó, szöveg);
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