using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Adatszerkezet;

using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok
{
    public delegate void Event_Kidobó();
    public partial class Ablak_Kidobó_változat : Form
    {
        public event Event_Kidobó Változat_Változás;
        readonly Kezelő_Kidobó_Változat KézVáltozat = new Kezelő_Kidobó_Változat();
        readonly Kezelő_Kidobó_Segéd KézKidobSeg = new Kezelő_Kidobó_Segéd();

        public string Cmbtelephely { get; private set; }



        public Ablak_Kidobó_változat(string cmbtelephely)
        {
            InitializeComponent();
            Cmbtelephely = cmbtelephely;
            Start();
        }

        public Ablak_Kidobó_változat()
        {
            InitializeComponent();
        }

        private void Start()
        {
            Változatlista1();
        }

        private void Ablak_Kidobó_változat_Load(object sender, EventArgs e)
        {
        }


        #region Változat nevek karbantartása
        private void Változatlista1()
        {
            try
            {

                Változatalaplista.Items.Clear();

                List<Adat_Kidobó_Változat> Adatok = KézVáltozat.Lista_Adat(Cmbtelephely.Trim());
                foreach (Adat_Kidobó_Változat Elem in Adatok)
                    Változatalaplista.Items.Add(Elem.Változatnév);

                Változatalaplista.Refresh();
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

        private void ÚjváltozatRögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Újváltozat.Text.Trim() == "") return;
                List<Adat_Kidobó_Változat> AdatokKidobSeg = KézVáltozat.Lista_Adat(Cmbtelephely.Trim());

                long utolsó = 1;
                if (AdatokKidobSeg.Count > 0) utolsó = AdatokKidobSeg.Max(a => a.Id) + 1;

                Adat_Kidobó_Változat AdatKidobSeg = (from a in AdatokKidobSeg
                                                     where a.Változatnév == MyF.Szöveg_Tisztítás(Újváltozat.Text, 0, 50)
                                                     orderby a.Id
                                                     select a).FirstOrDefault();

                if (AdatKidobSeg == null)
                {
                    Adat_Kidobó_Változat ADAT = new Adat_Kidobó_Változat(0, MyF.Szöveg_Tisztítás(Újváltozat.Text, 0, 50));
                    KézVáltozat.Rögzítés(Cmbtelephely.Trim(), ADAT);
                }

                Újváltozat.Text = "";
                Változatlista1();
                Változat_Változás?.Invoke();
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

        private void VáltozatTörlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Újváltozat.Text.Trim() == "") return;

                List<Adat_Kidobó_Változat> AdatokKidobSeg = KézVáltozat.Lista_Adat(Cmbtelephely.Trim());

                Adat_Kidobó_Változat AdatKidobSeg = (from a in AdatokKidobSeg
                                                     where a.Változatnév == Újváltozat.Text.Trim()
                                                     orderby a.Id
                                                     select a).FirstOrDefault();

                if (AdatKidobSeg != null)
                {
                    KézKidobSeg.Törlés(Cmbtelephely.Trim(), Újváltozat.Text.Trim());
                    KézVáltozat.Törlés(Cmbtelephely.Trim(), AdatKidobSeg);
                }
                Újváltozat.Text = "";
                Változatlista1();

                Változat_Változás?.Invoke();
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

        private void Változatalaplista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Változatalaplista.SelectedIndex < 0) return;
            Újváltozat.Text = Változatalaplista.Items[Változatalaplista.SelectedIndex].ToString();
        }
        #endregion

        private void Ablak_Kidobó_változat_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }
    }
}
