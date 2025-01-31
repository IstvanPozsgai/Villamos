using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Kidobó_Ismétlődő : Form
    {
        public event Event_Kidobó Ismétlődő_Változás;

        readonly Kezelő_Kidobó_Változat KézVáltozat = new Kezelő_Kidobó_Változat();
        readonly Kezelő_Kidobó_Segéd KézKidobSeg = new Kezelő_Kidobó_Segéd();

        public string Cmbtelephely { get; private set; }
        public string Alsópanel { get; private set; }
        public Adat_Kidobó_Segéd Rekord { get; private set; }
        public DateTime Dátum { get; private set; }

        public Ablak_Kidobó_Ismétlődő(string cmbtelephely, Adat_Kidobó_Segéd rekord, DateTime dátum, string alsópanel)
        {
            Cmbtelephely = cmbtelephely;
            Dátum = dátum;
            Rekord = rekord;
            Alsópanel = alsópanel;

            InitializeComponent();
            Combováltozatfeltölt();
            Adatok_kiírása(rekord);
        }

        Ablak_Kidobó_változat Új_Ablak_Kidobó_változat;

        private void Ablak_Kidobó_Ismétlődő_Load(object sender, EventArgs e)
        {

        }

        void Adatok_kiírása(Adat_Kidobó_Segéd rekord)
        {
            if (rekord != null)
            {
                Frame3KezdésiHely.Text = rekord.Kezdéshely.Trim();
                Frame3VégzésiHely.Text = rekord.Végzéshely.Trim();
                Frame3Megjegyzés.Text = rekord.Megjegyzés.Trim();
                Frame3KezdésiIdő.Value = new DateTime(Dátum.Year, Dátum.Month, Dátum.Day, rekord.Kezdés.Hour, rekord.Kezdés.Minute, rekord.Kezdés.Second);
                Frame3VégzésiIdő.Value = new DateTime(Dátum.Year, Dátum.Month, Dátum.Day, rekord.Végzés.Hour, rekord.Végzés.Minute, rekord.Végzés.Second);
                Frame3Szolgálatiszám.Text = rekord.Szolgálatiszám.Trim();
                Frame3ForgalmiSzám.Text = rekord.Forgalmiszám.Trim();
                ComboVáltozat.Text = rekord.Változatnév.Trim();
            }
        }

        private void Command5_Click(object sender, EventArgs e)
        {
            Frame3KezdésiHely.Text = Alsópanel;
        }

        private void Command4_Click(object sender, EventArgs e)
        {
            Frame3VégzésiHely.Text = Alsópanel;
        }

        private void Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (ComboVáltozat.Text.Trim() == "") throw new HibásBevittAdat("A változat nevét meg kell adni.");
                if (Frame3Szolgálatiszám.Text.Trim() == "") throw new HibásBevittAdat("A szolgálati szám nem lehet üres mező.");

                List<Adat_Kidobó_Segéd> AdatokÖ = KézKidobSeg.Lista_Adatok(Cmbtelephely.Trim());
                Adat_Kidobó_Segéd AdatKidobSeg = (from a in AdatokÖ
                                                  where a.Szolgálatiszám == Frame3Szolgálatiszám.Text.Trim()
                                                  && a.Változatnév == ComboVáltozat.Text.Trim()
                                                  select a).FirstOrDefault();


                Adat_Kidobó_Segéd ADAT = new Adat_Kidobó_Segéd(Frame3ForgalmiSzám.Text.Trim(),
                                               Frame3Szolgálatiszám.Text.Trim(),
                                               Frame3KezdésiIdő.Value,
                                               Frame3VégzésiIdő.Value,
                                               Frame3KezdésiHely.Text.Trim(),
                                               Frame3VégzésiHely.Text.Trim(),
                                               ComboVáltozat.Text.Trim(),
                                               Frame3Megjegyzés.Text.Trim());
                if (AdatKidobSeg != null)
                    KézKidobSeg.Módosítás(Cmbtelephely.Trim(), ADAT);
                else
                    KézKidobSeg.Rögzítés(Cmbtelephely.Trim(), ADAT);

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

        private void Törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (ComboVáltozat.Text == "") throw new HibásBevittAdat("A változat neve nem lehet üres!");
                if (Frame3Szolgálatiszám.Text.Trim() == "") throw new HibásBevittAdat("A Szolgálatszám mezőnek értéket kell tartalmaznia.");
                // menti a változat adatait.

                List<Adat_Kidobó_Segéd> AdatokKidobSeg = KézKidobSeg.Lista_Adatok(Cmbtelephely.Trim());

                Adat_Kidobó_Segéd AdatKidobSeg = (from a in AdatokKidobSeg
                                                  where a.Szolgálatiszám == Frame3Szolgálatiszám.Text.Trim()
                                                  && a.Változatnév == ComboVáltozat.Text.Trim()
                                                  select a).FirstOrDefault();

                if (AdatKidobSeg != null)
                {
                    KézKidobSeg.Törlés(Cmbtelephely.Trim(), ComboVáltozat.Text.Trim(), Frame3Szolgálatiszám.Text.Trim());
                    Ismétlődő_Változás?.Invoke();
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

        private void Ablak_Kidobó_Ismétlődő_KeyDown(object sender, KeyEventArgs e)
        {
            //Esc
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }


        #region Változat
        private void Combováltozatfeltölt()
        {
            List<Adat_Kidobó_Változat> Adatok = KézVáltozat.Lista_Adat(Cmbtelephely.Trim());

            ComboVáltozat.Items.Clear();
            ComboVáltozat.Items.Add("");
            foreach (Adat_Kidobó_Változat Elem in Adatok)
                ComboVáltozat.Items.Add(Elem.Változatnév);

            ComboVáltozat.Refresh();
            Ismétlődő_Változás?.Invoke();
        }

        public void Változatkarb_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Kidobó_változat == null)
            {
                Új_Ablak_Kidobó_változat = new Ablak_Kidobó_változat(Cmbtelephely.Trim());
                Új_Ablak_Kidobó_változat.FormClosed += Ablak_Kidobó_változat_Closed;
                Új_Ablak_Kidobó_változat.Top = 350;
                Új_Ablak_Kidobó_változat.Left = 500;
                Új_Ablak_Kidobó_változat.Show();
                Új_Ablak_Kidobó_változat.Változat_Változás += Combováltozatfeltölt;
            }
            else
            {
                Új_Ablak_Kidobó_változat.Activate();
            }
        }

        private void Ablak_Kidobó_változat_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kidobó_változat = null;
        }

        private void Ablak_Kidobó_Ismétlődő_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kidobó_változat?.Close();
        }
        #endregion
    }
}
