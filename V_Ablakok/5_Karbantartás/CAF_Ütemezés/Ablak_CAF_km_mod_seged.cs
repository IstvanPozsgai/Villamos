using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyCaf = Villamos.Villamos_Ablakok.CAF_Ütemezés.CAF_Közös_Eljárások;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    public partial class Ablak_CAF_km_mod_seged : Form
    {
        public event Event_Kidobó Változás;
        public Adat_CAF_Adatok Adat { get; private set; }
        public double id { get; private set; }

        readonly Kezelő_CAF_Adatok KézAdatok = new Kezelő_CAF_Adatok();
        
        
        public Ablak_CAF_km_mod_seged(double ID)
        {
            InitializeComponent();
            Adat = KézAdatok.Lista_Adatok().FirstOrDefault(a=> a.Id == ID);
            id = ID;
            Start();
        }

        private void Start()
        {
        }

        private void Ablak_CAF_km_mod_seged_Load(object sender, EventArgs e)
        {
            try
            {
                if (Adat != null) Kiír();
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

        private void Kiír()
        {
            try
            {
                if (Adat != null)
                {
                    Segéd_pályaszám.Text = Adat.Azonosító;
                    Segéd_KM_allas.Text = Adat.Számláló.ToString();
                    Segéd_dátum.Value = Adat.Dátum;
                    seged_kov_stat.Text = "6 - Elvégzett";         
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
        private void Segéd_Pót_Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                //A rögzítés nem működik még.
                KézAdatok.Módosítás_StátusEsKm(id, Segéd_KM_allas.Text.ToString());
                //Változás?.Invoke();
                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Segéd_pályaszám_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
