using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.ICS_KCSV
{
    public partial class Ablak_ICS_KCSV_segéd : Form
    {
        public event Event_Kidobó Változás;
        public DateTime Dátum_ütem { get; private set; }
        public string Telephely { get; private set; }

        public Adat_ICS_Ütem Adat { get; private set; }
        public Ablak_ICS_KCSV_segéd(DateTime dátum_ütem, string telephely, Adat_ICS_Ütem adat)
        {
            InitializeComponent();
            Dátum_ütem = dátum_ütem;
            Telephely = telephely;
            Adat = adat;
            Jogosultságkiosztás();
        }


        private void Ablak_ICS_KCSV_segéd_Load(object sender, EventArgs e)
        {
            this.Text = Adat.Azonosító;
            Kiírja_Kocsi();
        }

        private void Kiírja_Kocsi()
        {
            try
            {
                switch (Adat.Állapot)
                {
                    case 3:
                        {
                            Bennmarad_1.Checked = false;
                            break;
                        }
                    case 4:
                        {
                            Bennmarad_1.Checked = true;
                            break;
                        }

                    default:
                        {
                            Bennmarad_1.Checked = false;
                            break;
                        }
                }
                Vizsgálatrütemez_1.Checked = Adat.Ütemez;
                Rendelésiszám_1.Text = Adat.Rendelésiszám;
                V_Sorszám_1.Text = Adat.V_Sorszám.ToString();
                V_Megnevezés_1.Text = Adat.V_Megnevezés;
                V_km_1.Text = Adat.V_km_.ToString();
                Következő_V.Text = Adat.Következő_V;
                Következővizsgálatszám_1.Text = Adat.Következővizsgálatszám.ToString();
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


        private void Rögzít_1_Click(object sender, EventArgs e)
        {
            try
            {

                string hely = $@"{Application.StartupPath}\{Telephely.Trim()}\Adatok\főkönyv\futás\{Dátum_ütem.Year}\vezénylés{Dátum_ütem.Year}.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Vezényléstábla(hely);
                string jelszó = "tápijános";

                Kezelő_Vezénylés KézVezénylés = new Kezelő_Vezénylés();
                string szöveg = $"SELECT * FROM vezényléstábla";
                List<Adat_Vezénylés> Adatok = KézVezénylés.Lista_Adatok(hely, jelszó, szöveg);

                Adat_Vezénylés Elem = (from a in Adatok
                                       where a.Azonosító == Adat.Azonosító.Trim()
                                       && a.Dátum.ToShortDateString() == Dátum_ütem.ToShortDateString()
                                       && a.Törlés == 0
                                       select a).FirstOrDefault();

                if (Elem == null)
                {
                    // ha van akkor rögzíteni kell
                    szöveg = "INSERT INTO vezényléstábla ";
                    szöveg += "(azonosító, Dátum, Státus, vizsgálatraütemez, takarításraütemez, vizsgálat, vizsgálatszám, rendelésiszám, törlés, szerelvényszám, fusson, álljon, típus) VALUES (";
                    szöveg += "'" + Adat.Azonosító.Trim() + "', ";
                    szöveg += "'" + Dátum_ütem.ToString("yyyy.MM.dd") + "', ";
                    if (Bennmarad_1.Checked)
                        szöveg += "4, ";
                    else
                        szöveg += "3,";
                    if (!Vizsgálatrütemez_1.Checked)
                        szöveg += "0, ";
                    else
                        szöveg += "1,";
                    szöveg += "0, ";
                    // következő V
                    if (Vizsgálatrütemez_1.Checked)
                        szöveg += "'" + Következő_V.Text.Trim() + "', ";
                    else
                        szöveg += " '_', ";

                    szöveg += Következővizsgálatszám_1.Text + ", ";
                    if (Rendelésiszám_1.Text.Trim() == "")
                        szöveg += "'_', ";
                    else
                        szöveg += "'" + Rendelésiszám_1.Text.Trim() + "',";
                    szöveg += "0, ";
                    szöveg += "0, ";
                    szöveg += " 0, 0, 'ICS')";
                }

                else
                {
                    // módosítás
                    szöveg = "UPDATE vezényléstábla SET ";
                    if (Bennmarad_1.Checked)
                        szöveg += " Státus=4, ";
                    else
                        szöveg += " Státus=3, ";
                    if (!Vizsgálatrütemez_1.Checked)
                        szöveg += " vizsgálatraütemez=0, ";
                    else
                        szöveg += " vizsgálatraütemez=1, ";
                    szöveg += " takarításraütemez=0, ";
                    if (Vizsgálatrütemez_1.Checked)
                        szöveg += "vizsgálat = '" + Következő_V.Text.Trim() + "', ";
                    else
                        szöveg += "vizsgálat ='_', ";

                    szöveg += " vizsgálatszám=" + Következővizsgálatszám_1.Text + ", ";
                    if (Rendelésiszám_1.Text.Trim() == "" || Rendelésiszám_1.Text.Trim() == "_")
                        szöveg += " rendelésiszám='_' ";
                    else
                        szöveg += " rendelésiszám='" + Rendelésiszám_1.Text.Trim() + "' ";

                    szöveg += $" WHERE [azonosító] ='{Adat.Azonosító.Trim()}' AND [dátum]=#" + Dátum_ütem.ToString("M-d-yy") + "#";
                    szöveg += " AND [törlés]=0";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
                if (Változás != null) Változás();
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

        private void Töröl_1_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + $@"\{Telephely.Trim()}\Adatok\főkönyv\futás\{Dátum_ütem.Year}\vezénylés{Dátum_ütem.Year}.mdb";
                string jelszó = "tápijános";
                Kezelő_Vezénylés KézVezénylés = new Kezelő_Vezénylés();
                string szöveg = $"SELECT * FROM vezényléstábla";
                List<Adat_Vezénylés> Adatok = KézVezénylés.Lista_Adatok(hely, jelszó, szöveg);

                Adat_Vezénylés Elem = (from a in Adatok
                                       where a.Azonosító == Adat.Azonosító.Trim()
                                       && a.Dátum.ToShortDateString() == Dátum_ütem.ToShortDateString()
                                       && a.Törlés == 0
                                       select a).FirstOrDefault();

                if (Elem == null)
                {
                    szöveg = "UPDATE vezényléstábla SET törlés=1 ";
                    szöveg += $" WHERE [azonosító] ='{Adat.Azonosító.Trim()}' AND [dátum]=#{Dátum_ütem:M-d-yy}#  AND [törlés]=0";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                    if (Változás != null) Változás();
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

        private void Ablak_ICS_KCSV_segéd_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false

                Rögzít_1.Enabled = false;
                Töröl_1.Enabled = false;


                // csak főmérnökségi belépéssel törölhető
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {

                }
                else
                {

                }

                melyikelem = 113;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {

                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {

                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Rögzít_1.Enabled = true;
                    Töröl_1.Enabled = true;
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

    }
}
