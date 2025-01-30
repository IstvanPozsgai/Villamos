using System;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    public partial class Ablak_CAF_Eszterga_Beállítás : Form
    {
        public Ablak_CAF_Eszterga_Beállítás()
        {
            InitializeComponent();
            Start();
        }


        void Start()
        {
            Jogosultságkiosztás();
            Pályaszámokfeltöltése();
        }


        private void Ablak_CAF_Eszterga_Beállítás_Load(object sender, EventArgs e)
        {
            string hely = Application.StartupPath + @"\Főmérnökség\adatok\Kerék.mdb";
            string jelszó = "szabólászló";
            string szöveg = $"SELECT * FROM Eszterga_Beállítás";
            if (!Adatbázis.ABvanTábla(hely, jelszó, szöveg))
                Adatbázis_Létrehozás.Kerék_Eszterga_Beállítás(hely);
            Alaphelyzet();
        }


        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Alap_rögzít.Enabled = false;

                // csak főmérnökségi belépéssel módosítható

                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                    Alap_rögzít.Visible = true;
                else
                    Alap_rögzít.Visible = false;


                melyikelem = 115;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Alap_rögzít.Enabled = true;
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


        private void Pályaszámokfeltöltése()
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                string szöveg = "SELECT * FROM alap ORDER BY azonosító";
                Alap_pályaszám.Items.Clear();
                Alap_pályaszám.BeginUpdate();
                Alap_pályaszám.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "azonosító"));
                Alap_pályaszám.EndUpdate();
                Alap_pályaszám.Refresh();

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


        private void Ablak_CAF_Eszterga_Beállítás_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }

            //Ctrl gomb nyomása
            if ((int)e.KeyCode == 17)



                //Ctrl+F
                if (e.Control && e.KeyCode == Keys.F)
                {

                }
        }


        void Pályaszám_kiirás()
        {
            try
            {
                if (Alap_pályaszám.Text.Trim() == "")
                    throw new HibásBevittAdat("A pályaszámot meg kell adni.");
                Alaphelyzet();
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Kerék.mdb";
                string jelszó = "szabólászló";
                string szöveg = $"SELECT * FROM Eszterga_Beállítás WHERE azonosító='{Alap_pályaszám.Text.Trim()}'";

                Kezelő_Kerék_Eszterga_Beállítás kéz = new Kezelő_Kerék_Eszterga_Beállítás();
                Adat_Kerék_Eszterga_Beállítás Adat = kéz.Egy_Adat(hely, jelszó, szöveg);
                if (Adat != null)
                {
                    Km_Lépés.Text = Adat.KM_lépés.ToString();
                    Idő_Lépés.Text = Adat.Idő_lépés.ToString();
                    if (Adat.KM_IDŐ)
                        KM_alapú.Checked = true;
                    else
                        Idő_alapú.Checked = true;
                    Dátum.Value = Adat.Ütemezve;
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }


        private void KM_alapú_CheckedChanged(object sender, EventArgs e)
        {
            if (KM_alapú.Checked)
            {
                Km_Lépés.Enabled = true;
                Idő_Lépés.Enabled = false;
            }
            else
            {
                Km_Lépés.Enabled = false;
                Idő_Lépés.Enabled = true;
            }
        }

        void Alaphelyzet()
        {
            KM_alapú.Checked = true;
            Km_Lépés.Text = "0";
            Idő_Lépés.Text = "0";
            Km_Lépés.Enabled = true;
            Idő_Lépés.Enabled = false;
            Dátum.Value = new DateTime(1900, 1, 1);
        }


        private void Alap_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                int KMalap = 0, Időalap = 0;
                if (Alap_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("A pályaszámot meg kell adni.");
                if (KM_alapú.Checked && !int.TryParse(Km_Lépés.Text, out KMalap)) throw new HibásBevittAdat("Km alapú vezénylés van választva és nincs kitöltve a km ütemezés mező, vagy nem egész szám.");
                if (Idő_alapú.Checked && !int.TryParse(Idő_Lépés.Text, out Időalap)) throw new HibásBevittAdat("Idő alapú vezénylés van választva és nincs kitöltve a Idő ütemezés mező, vagy nem egész szám.");
                if (Időalap < 0 || KMalap < 0) throw new HibásBevittAdat("Az ütemezés mezők nem vehetnek fel negatív értéket.");

                Kezelő_Kerék_Eszterga_Beállítás kéz = new Kezelő_Kerék_Eszterga_Beállítás();
                Adat_Kerék_Eszterga_Beállítás Adat = new Adat_Kerék_Eszterga_Beállítás(Alap_pályaszám.Text.Trim(), KMalap, Időalap, KM_alapú.Checked, Dátum.Value);
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\Kerék.mdb";
                string jelszó = "szabólászló";
                kéz.Rögzít(hely, jelszó, Adat);
                MessageBox.Show("Az adatok rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void Alap_pályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pályaszám_kiirás();
        }
    }
}
