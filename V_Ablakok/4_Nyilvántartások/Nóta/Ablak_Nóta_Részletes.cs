using System;
using System.Windows.Forms;
using static Villamos.V_MindenEgyéb.Enumok;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Nóta
{
    public partial class Ablak_Nóta_Részletes : Form
    {
        public int Sorszám { get; private set; }
        public Ablak_Nóta_Részletes(int sorszám)
        {
            InitializeComponent();
            Sorszám = sorszám;
        }

        private void Ablak_Nóta_Részletes_Load(object sender, EventArgs e)
        {
            TelephelyFeltöltés();
            StátusFeltöltés();
            Kerékállapotfeltöltés();
            BeépíthetőFeltöltés();
            AdatokKiírása();
        }

        private void BeépíthetőFeltöltés()
        {
            Beépíthető.Items.Add("");
            Beépíthető.Items.Add("Igen");
            Beépíthető.Items.Add("Nem");
        }

        private void Kerékállapotfeltöltés()
        {
            try
            {
                foreach (Kerék_Állapot elem in Enum.GetValues(typeof(Kerék_Állapot)))
                    Állapot.Items.Add($"{(int)elem} - {elem.ToString().Replace('_', ' ')}");

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

        private void StátusFeltöltés()
        {
            try
            {
                foreach (Nóta_Státus elem in Enum.GetValues(typeof(Nóta_Státus)))
                    Státus.Items.Add($"{(int)elem} - {elem.ToString().Replace('_', ' ')}");

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

        private void TelephelyFeltöltés()
        {
            try
            {
                Telephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Telephely.Items.Add(Elem);
                Telephely.Items.Add("VJSZ");

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

        private void AdatokKiírása()
        {
            try
            {


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
