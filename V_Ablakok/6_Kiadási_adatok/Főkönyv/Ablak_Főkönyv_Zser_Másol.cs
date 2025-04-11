using System;
using System.Collections.Generic;
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
                int nap = ZSER_dátumig.Value.Day - ZSER_dátumtól.Value.Day; // ennyi napot kell arrébb tolni

                // leellenőrizzük, hogy létezik-e már a létrehozni kívánt adat
                List<Adat_Főkönyv_ZSER> Adatok = Kéz.Lista_Adatok(Cmbtelephely, ZSER_dátumtól.Value, ZSER_DE_tól.Checked ? "de" : "du");
                if (Adatok == null || Adatok.Count == 0) throw new HibásBevittAdat("A másolandó adat állomány nem létezik.");

                List<Adat_Főkönyv_ZSER> ÚjAdatok = Kéz.Lista_Adatok(Cmbtelephely, ZSER_dátumig.Value, ZSER_De_ig.Checked ? "de" : "du");

                if (ÚjAdatok != null && ÚjAdatok.Count != 0)
                {
                    // ha létezik akkor töröljük
                    if (MessageBox.Show("Már van az adott napra feltöltve adat ! Módosítjuk az adatokat ?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        Kéz.Törlés(Cmbtelephely, ZSER_dátumig.Value, ZSER_De_ig.Checked ? "de" : "du");
                    else
                        return;
                }


                ÚjAdatok.Clear();
                foreach (Adat_Főkönyv_ZSER Adat in Adatok)
                {
                    Adat_Főkönyv_ZSER ADAT = new Adat_Főkönyv_ZSER(
                                Adat.Viszonylat,
                                Adat.Forgalmiszám,
                                Adat.Tervindulás.AddDays(nap),
                                Adat.Tényindulás.AddDays(nap),
                                Adat.Tervérkezés.AddDays(nap),
                                Adat.Tényérkezés.AddDays(nap),
                                "*",
                                Adat.Szerelvénytípus,
                                Adat.Kocsikszáma,
                                Adat.Megjegyzés,
                                Adat.Kocsi1, Adat.Kocsi2, Adat.Kocsi3,
                                Adat.Kocsi4, Adat.Kocsi5, Adat.Kocsi6,
                                "", "");
                    ÚjAdatok.Add(ADAT);
                }
                // beírjuk az új adatokat
                Kéz.Rögzítés(Cmbtelephely, ZSER_dátumig.Value, ZSER_De_ig.Checked ? "de" : "du", ÚjAdatok);

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
    }
}
