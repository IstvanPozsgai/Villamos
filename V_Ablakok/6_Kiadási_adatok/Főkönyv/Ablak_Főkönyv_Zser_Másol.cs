using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos_Ablakok._6_Kiadási_adatok.Főkönyv
{
    public partial class Ablak_Főkönyv_Zser_Másol : Form
    {
        public string Cmbtelephely { get; private set; }


        public Ablak_Főkönyv_Zser_Másol(string cmbtelephely)
        {
            Cmbtelephely = cmbtelephely;
            InitializeComponent();
        }

        private void Ablak_Főkönyv_Zser_Másol_Load(object sender, EventArgs e)
        {
            ZSER_dátumtól.Value = DateTime.Today;
            ZSER_dátumig.Value = DateTime.Today;
        }


        private void Zser_másol_Gomb_Click(object sender, EventArgs e)
        {
            try
            {
                // leellenőrizzük, hogy létezik-e már a létrehozni kívánt adat

                string honnan = $@"{Application.StartupPath}\{Cmbtelephely}\adatok\főkönyv\{ZSER_dátumtól.Value.Year}\ZSER\zser{ZSER_dátumtól.Value:yyyyMMdd}";
                if (ZSER_DE_tól.Checked)
                    honnan += "de.mdb";
                else
                    honnan += "du.mdb";

                if (!File.Exists(honnan))
                    throw new HibásBevittAdat("A másolandó adat állomány nem létezik.");

                string hova = $@"{Application.StartupPath}\{Cmbtelephely}\adatok\főkönyv\{ZSER_dátumig.Value.Year}\ZSER\zser{ZSER_dátumig.Value:yyyyMMdd}";

                if (ZSER_De_ig.Checked)
                    hova += "de.mdb";
                else
                    hova += "du.mdb";

                if (File.Exists(hova))
                {
                    // ha létezik akkor töröljük
                    if (MessageBox.Show("Már van az adott napra feltöltve adat ! Módosítjuk az adatokat ?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        File.Delete(hova);
                    else
                        return;

                }
                File.Copy(honnan, hova);
                ZSer_adatok_napolása();
                MessageBox.Show("Az adatok másolása megtörtént.", "Rögzítés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void ZSer_adatok_napolása()
        {

            int nap = (int)(ZSER_dátumig.Value - ZSER_dátumtól.Value).TotalDays; // ennyi napot kell arrébb tolni
            string hely = $@"{Application.StartupPath}\{Cmbtelephely}\adatok\főkönyv\{ZSER_dátumig.Value.Year}\ZSER\zser{ZSER_dátumig.Value:yyyyMMdd}";
            
            if (ZSER_De_ig.Checked )
                hely += "de.mdb";
            else
                hely += "du.mdb";

            string jelszó = "lilaakác";
            string szöveg = "SELECT * FROM Zseltábla ORDER BY viszonylat, forgalmiszám, tervindulás ";

            Kezelő_Főkönyv_ZSER KFZS_kéz = new Kezelő_Főkönyv_ZSER();
            List<Adat_Főkönyv_ZSER> Adatok = KFZS_kéz.Lista_adatok(hely, jelszó, szöveg);

            List<string> SzövegGy = new List<string>();
            foreach (Adat_Főkönyv_ZSER rekord in Adatok)
            {
                szöveg = "UPDATE Zseltábla  SET ";
                szöveg += " tervindulás='" + rekord.Tervindulás.AddDays(nap).ToString() + "', ";
                szöveg += " tényindulás='" + rekord.Tényindulás.AddDays(nap).ToString() + "', ";
                szöveg += " tervérkezés='" + rekord.Tervérkezés.AddDays(nap).ToString() + "', ";
                szöveg += " tényérkezés='" + rekord.Tényérkezés.AddDays(nap).ToString() + "' ";
                szöveg += " WHERE viszonylat='" + rekord.Viszonylat.Trim() + "' AND ";
                szöveg += "  forgalmiszám='" + rekord.Forgalmiszám.Trim() + "' AND ";
                szöveg += "  tervindulás=#" + rekord.Tervindulás.ToString("MM-dd-yyyy HH:mm:ss") + "#";
                SzövegGy.Add(szöveg);
            }
            MyA.ABMódosítás(hely, jelszó, SzövegGy);
        }

    }
}
