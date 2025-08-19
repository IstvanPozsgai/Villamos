using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Kidobó_Napi : Form
    {
        readonly Kezelő_Kidobó KézKidobó = new Kezelő_Kidobó();

        public event Event_Kidobó Ismétlődő_Változás;
        public string Cmbtelephely { get; private set; }
        public string Alsópanel { get; private set; }
        public Adat_Kidobó Rekord { get; private set; }
        public DateTime Dátum { get; private set; }

        public Ablak_Kidobó_Napi(string cmbtelephely, Adat_Kidobó rekord, DateTime dátum, string alsópanel)
        {
            Cmbtelephely = cmbtelephely;
            Dátum = dátum;
            Rekord = rekord;
            Alsópanel = alsópanel;

            InitializeComponent();
            Adatak_kiírása();
        }

        public Ablak_Kidobó_Napi()
        {
            InitializeComponent();
        }

        private void Ablak_Kidobó_Napi_Load(object sender, EventArgs e)
        {

        }

        private void Adatak_kiírása()
        {
            KezdésiHely.Text = Rekord.Kezdéshely.Trim();
            VégzésiHely.Text = Rekord.Végzéshely.Trim();
            KezdésiIdő.Value = Rekord.Kezdés;
            VégzésiIdő.Value = Rekord.Végzés;
            Megjegyzés.Text = Rekord.Megjegyzés.Trim();
        }

        private void Plusz_Click(object sender, EventArgs e)
        {
            KezdésiHely.Text = Alsópanel;
        }

        private void Command3_Click(object sender, EventArgs e)
        {
            VégzésiHely.Text = Alsópanel;
        }

        private void Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_Kidobó> Adatok = KézKidobó.Lista_Adat(Cmbtelephely.Trim(), Dátum);

                Adat_Kidobó Elem = (from a in Adatok
                                    where a.Szolgálatiszám == Rekord.Szolgálatiszám
                                    select a).FirstOrDefault();

                if (Elem != null)
                {
                    Rekord = new Adat_Kidobó(Rekord.Viszonylat,
                                           Rekord.Forgalmiszám,
                                           Rekord.Szolgálatiszám,
                                           Rekord.Jvez,
                                           KezdésiIdő.Value,
                                           VégzésiIdő.Value,
                                           KezdésiHely.Text,
                                           VégzésiHely.Text,
                                           Rekord.Kód,
                                           Rekord.Tárolásihely,
                                           Rekord.Villamos,
                                           Megjegyzés.Text,
                                           Rekord.Szerelvénytípus);
                    KézKidobó.Módosítás(Cmbtelephely.Trim(), Dátum, Rekord);
                }
                Ismétlődő_Változás?.Invoke();
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

        private void Ablak_Kidobó_Napi_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }
    }
}
