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

        readonly Kezelő_CAF_Adatok KézAdatok = new Kezelő_CAF_Adatok();
        
        
        public Ablak_CAF_km_mod_seged(double ID)
        {
            InitializeComponent();
            Adat = KézAdatok.Lista_Adatok().FirstOrDefault(a=> a.Id == ID);
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
                    seged_kov_stat.Text = "6- Elvégzett";         
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
                ////if (Segéd_Vizsg.Text.Trim() == "") throw new HibásBevittAdat("Vizsgálat neve nem lehet üres.");
                //if (Segéd_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("A pályaszám mező nem lehet üres.");

                ////if (!int.TryParse(Segéd_darab.Text, out int Darab)) throw new HibásBevittAdat("A darab mező nem lehet üres és pozitív egész számnak kell lennie.");
                //if (Darab <= 0) throw new HibásBevittAdat("A darab mező nem lehet nullánál kisebb.");

                //for (int i = 0; i < Darab; i++)
                //{
                //    DateTime újnap = Segéd_dátum.Value.AddDays(i);

                //    // következő sorszám
                //    double Segéd_Sorszám = KézAdatok.Sorszám();
                //    Segéd_KM_allas.Text = Segéd_Sorszám.ToString();

                //    Adat_CAF_Adatok rekord = new Adat_CAF_Adatok(
                //        0,
                //        Segéd_pályaszám.Text.Trim(),
                //        Segéd_Vizsg.Text.Trim(),
                //        újnap,
                //        new DateTime(1900, 1, 1), 0, 8, 0, 0, 0,
                //        "Ütemezési Segéd");
                //    KézAdatok.Döntés(rekord);
                //}

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
