using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;

namespace Villamos.V_Ablakok._1_Bejelentkezés
{
    public partial class Ablak_Ideig : Form
    {
        readonly Kezelő_Kiegészítő_Sérülés KézSérülés = new Kezelő_Kiegészítő_Sérülés();

        public Ablak_Ideig()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_Ideig_Load(object sender, EventArgs e)
        {

        }

        private void Start()
        {
            Telephelyekfeltöltése();
        }


        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                List<Adat_Kiegészítő_Sérülés> Adatok = KézSérülés.Lista_Adatok();
                foreach (Adat_Kiegészítő_Sérülés rekord in Adatok)
                    Cmbtelephely.Items.Add(rekord.Név);

                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim(); }
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


    }
}
