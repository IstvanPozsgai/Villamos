using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok._6_Kiadási_adatok.Főkönyv
{
    public partial class Ablak_Főkönyv_Napi_Adatok : Form
    {
        public string Cmbtelephely { get; private set; }

        readonly Kezelő_Főkönyv_Nap Kéz = new Kezelő_Főkönyv_Nap();

        public Ablak_Főkönyv_Napi_Adatok(string cmbtelephely)
        {
            Cmbtelephely = cmbtelephely;
            InitializeComponent();
        }

        private void Ablak_Főkönyv_Napi_Adatok_Load(object sender, EventArgs e)
        {
            Dátumról.Value = DateTime.Today;
            Dátumra.Value = DateTime.Today;
            Jogosultságkiosztás();
        }

        private void Jogosultságkiosztás()
        {
            int melyikelem;
            // ide kell az összes gombot tenni amit szabályozni akarunk false

            Adatok_Másolása.Visible = false;


            melyikelem = 96;
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
                Adatok_Másolása.Visible = true;
            }
        }

        private void Adatok_Másolása_Click(object sender, EventArgs e)
        {
            try
            {
                // leellenőrizzük, hogy létezik-e már a létrehozni kívánt adat
                List<Adat_Főkönyv_Nap> Adatok = Kéz.Lista_Adatok(Cmbtelephely, Dátumról.Value, RadioButton1.Checked ? "de" : "du");
                if (Adatok == null || Adatok.Count == 0) throw new HibásBevittAdat("A másolandó adat állomány nem létezik.");

                List<Adat_Főkönyv_Nap> AdatokÚj = Kéz.Lista_Adatok(Cmbtelephely, Dátumra.Value, RadioButton4.Checked ? "de" : "du");


                if (AdatokÚj != null && AdatokÚj.Count > 0)
                {
                    // ha létezik akkor töröljük
                    if (MessageBox.Show("Már van az adott napra feltöltve adat ! Módosítjuk az adatokat ?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        Kéz.Törlés(Cmbtelephely, Dátumra.Value, RadioButton4.Checked ? "de" : "du");
                    else
                        return;
                }

                AdatokÚj.Clear();
                foreach (Adat_Főkönyv_Nap adat in Adatok)
                {
                    Adat_Főkönyv_Nap ADAT = new Adat_Főkönyv_Nap(
                        adat.Státus,
                        adat.Hibaleírása,
                        adat.Típus,
                        adat.Azonosító,
                        adat.Szerelvény,
                        "-", "-",
                        adat.Kocsikszáma,
                        new DateTime(1900, 1, 1, 0, 0, 0),
                        new DateTime(1900, 1, 1, 0, 0, 0),
                        new DateTime(1900, 1, 1, 0, 0, 0),
                        new DateTime(1900, 1, 1, 0, 0, 0),
                        adat.Miótaáll,
                        "-", "*");
                    AdatokÚj.Add(ADAT);
                }
                Kéz.Rögzítés(Cmbtelephely, Dátumra.Value, RadioButton4.Checked ? "de" : "du", AdatokÚj);
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
