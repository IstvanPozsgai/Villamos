using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.ICS_KCSV
{
    public partial class Ablak_ICS_KCSV_segéd : Form
    {
        readonly Kezelő_Vezénylés KézVezénylés = new Kezelő_Vezénylés();

        public event Event_Kidobó Változás;
        public DateTime Dátum_ütem { get; private set; }
        public string Telephely { get; private set; }

        public Adat_ICS_Ütem Adat { get; private set; }
        public Ablak_ICS_KCSV_segéd(DateTime dátum_ütem, string telephely, Adat_ICS_Ütem adat)
        {
            InitializeComponent();
            Dátum_ütem = dátum_ütem;
            Telephely = telephely;
            Adat = adat;
            Jogosultságkiosztás();
        }

        private void Ablak_ICS_KCSV_segéd_Load(object sender, EventArgs e)
        {
            this.Text = Adat.Azonosító;
            Kiírja_Kocsi();
        }

        private void Kiírja_Kocsi()
        {
            try
            {
                switch (Adat.Állapot)
                {
                    case 3:
                        {
                            Bennmarad_1.Checked = false;
                            break;
                        }
                    case 4:
                        {
                            Bennmarad_1.Checked = true;
                            break;
                        }

                    default:
                        {
                            Bennmarad_1.Checked = false;
                            break;
                        }
                }
                Vizsgálatrütemez_1.Checked = Adat.Ütemez;
                Rendelésiszám_1.Text = Adat.Rendelésiszám;
                V_Sorszám_1.Text = Adat.V_Sorszám.ToString();
                V_Megnevezés_1.Text = Adat.V_Megnevezés;
                V_km_1.Text = Adat.V_km_.ToString();
                Következő_V.Text = Adat.Következő_V;
                Következővizsgálatszám_1.Text = Adat.Következővizsgálatszám.ToString();
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

        private void Rögzít_1_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_Vezénylés> Adatok = KézVezénylés.Lista_Adatok(Telephely.Trim(), Dátum_ütem);

                Adat_Vezénylés Elem = (from a in Adatok
                                       where a.Azonosító == Adat.Azonosító.Trim()
                                       && a.Dátum.ToShortDateString() == Dátum_ütem.ToShortDateString()
                                       && a.Törlés == 0
                                       select a).FirstOrDefault();
                Adat_Vezénylés ADAT = new Adat_Vezénylés(
                       Adat.Azonosító.Trim(),
                       Dátum_ütem,
                       Bennmarad_1.Checked ? 4 : 3,
                       Vizsgálatrütemez_1.Checked ? 1 : 0,
                       0,
                       Vizsgálatrütemez_1.Checked ? Következő_V.Text.Trim() : "_",
                       Következővizsgálatszám_1.Text.ToÉrt_Int(),
                       Rendelésiszám_1.Text.Trim() == "" ? "_" : Rendelésiszám_1.Text.Trim(),
                       0, 0, 0, 0, "ICS");
                if (Elem == null)
                    KézVezénylés.Rögzítés(Telephely.Trim(), Dátum_ütem, ADAT);
                else
                    KézVezénylés.Módosítás(Telephely.Trim(), Dátum_ütem, ADAT);

                Változás?.Invoke();
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

        private void Töröl_1_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_Vezénylés> Adatok = KézVezénylés.Lista_Adatok(Telephely.Trim(), Dátum_ütem);

                Adat_Vezénylés Elem = (from a in Adatok
                                       where a.Azonosító == Adat.Azonosító.Trim()
                                       && a.Dátum.ToShortDateString() == Dátum_ütem.ToShortDateString()
                                       && a.Törlés == 0
                                       select a).FirstOrDefault();

                if (Elem == null)
                {
                    KézVezénylés.Módosítás(Telephely.Trim(), Dátum_ütem, Adat.Azonosító.Trim(), Dátum_ütem);
                    Változás?.Invoke();
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

        private void Ablak_ICS_KCSV_segéd_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false

                Rögzít_1.Enabled = false;
                Töröl_1.Enabled = false;


                // csak főmérnökségi belépéssel törölhető
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {

                }
                else
                {

                }

                melyikelem = 113;
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
                    Rögzít_1.Enabled = true;
                    Töröl_1.Enabled = true;
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
    }
}
