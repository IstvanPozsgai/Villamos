using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Ablakok._6_Kiadási_adatok.Főkönyv
{
    public partial class Ablak_Főkönyv_Zser_Másol : Form
    {
        public string Cmbtelephely { get; private set; }
        readonly Kezelő_Főkönyv_ZSER Kéz = new Kezelő_Főkönyv_ZSER();

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

                if (!File.Exists(honnan)) throw new HibásBevittAdat("A másolandó adat állomány nem létezik.");

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
            try
            {
                int nap = ZSER_dátumig.Value.Day - ZSER_dátumtól.Value.Day; // ennyi napot kell arrébb tolni

                List<Adat_Főkönyv_ZSER> Adatok = Kéz.Lista_adatok(Cmbtelephely, ZSER_dátumig.Value, ZSER_De_ig.Checked ? "de" : "du");

                List<Adat_Főkönyv_ZSER> AdatokGy = new List<Adat_Főkönyv_ZSER>();
                foreach (Adat_Főkönyv_ZSER rekord in Adatok)
                {
                    Adat_Főkönyv_ZSER ADAT = new Adat_Főkönyv_ZSER(
                                        rekord.Viszonylat,
                                        rekord.Forgalmiszám,
                                        rekord.Tervindulás,
                                        rekord.Tényindulás,
                                        rekord.Tényérkezés,
                                        rekord.Tényérkezés);
                    AdatokGy.Add(ADAT);
                }
                Kéz.Módosítás(Cmbtelephely, ZSER_dátumig.Value, ZSER_De_ig.Checked ? "de" : "du", AdatokGy, nap);
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
