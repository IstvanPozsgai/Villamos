using System;
using System.IO;
using System.Windows.Forms;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok._6_Kiadási_adatok.Főkönyv
{
    public partial class Ablak_Főkönyv_Napi_Adatok : Form
    {
        public string Cmbtelephely { get; private set; }
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

                string honnan = $@"{Application.StartupPath}\{Cmbtelephely}\adatok\főkönyv\{Dátumról.Value.Year}\nap\{Dátumról.Value.ToString("yyyyMMdd")}";
                if (RadioButton1.Checked)
                    honnan += "denap.mdb";
                else
                    honnan += "dunap.mdb";

                if (!File.Exists(honnan))
                    throw new HibásBevittAdat("A másolandó adat állomány nem létezik.");


                string hova = $@"{Application.StartupPath}\{Cmbtelephely}\adatok\főkönyv\{Dátumra.Value.Year}\nap\{Dátumra.Value.ToString("yyyyMMdd")}";
                if (RadioButton4.Checked)
                    hova += "denap.mdb";
                else
                    hova += "dunap.mdb";

                if (File.Exists(hova))
                {
                    // ha létezik akkor töröljük
                    if (MessageBox.Show("Már van az adott napra feltöltve adat ! Módosítjuk az adatokat ?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        File.Delete(hova);
                    else
                        return;
                }

                File.Copy(honnan, hova);
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
