using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Ablakok._6_Kiadási_adatok.Főkönyv
{
    public partial class Ablak_Főkönyv_Napi : Form
    {
        public string Cmbtelephely { get; private set; }
        public bool Délelőtt { get; private set; }
        public DateTime Dátum { get; private set; }
        public Adat_Főkönyv_ZSER ZserAdat { get; private set; }

        public event Event_Kidobó Változás;

        readonly Kezelő_Főkönyv_ZSER KézZser = new Kezelő_Főkönyv_ZSER();
        readonly Kezelő_Telep_Kieg_Fortetípus KézForte = new Kezelő_Telep_Kieg_Fortetípus();

        public Ablak_Főkönyv_Napi(string cmbtelephely, bool délelőtt, DateTime dátum, Adat_Főkönyv_ZSER zserAdat)
        {
            Cmbtelephely = cmbtelephely;
            Délelőtt = délelőtt;
            Dátum = dátum;
            ZserAdat = zserAdat;
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Forte_típus_feltöltése();
            Napszak_feltöltés();
            ZSER_részletes_adatok();
        }

        private void Ablak_Főkönyv_Napi_Load(object sender, EventArgs e)
        {
        }

        private void Forte_típus_feltöltése()
        {
            try
            {
                List<Adat_Telep_Kieg_Fortetípus> Adatok = KézForte.Lista_Adatok(Cmbtelephely);
                ZSER_fortetípus.Items.Clear();
                foreach (Adat_Telep_Kieg_Fortetípus Elem in Adatok)
                    ZSER_fortetípus.Items.Add(Elem.Ftípus);
                ZSER_fortetípus.Refresh();
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

        private void Napszak_feltöltés()
        {
            ZSER_napszak.Items.Clear();
            ZSER_napszak.Items.Add("DE");
            ZSER_napszak.Items.Add("DU");
            ZSER_napszak.Items.Add("É");
            ZSER_napszak.Items.Add("X");
            ZSER_napszak.Items.Add("*");
        }

        private void ZSER_részletes_adatok()
        {
            try
            {
                ZSER_viszonylat.Text = ZserAdat.Viszonylat;
                ZSER_forgalmiszám.Text = ZserAdat.Forgalmiszám;
                ZSER_napszak.Text = ZserAdat.Napszak;
                ZSER_tervindulás.Value = ZserAdat.Tervindulás;
                ZSER_tényidulás.Value = ZserAdat.Tényindulás;
                ZSER_tervérkezés.Value = ZserAdat.Tervérkezés;
                zser_tényérkezés.Value = ZserAdat.Tényérkezés;
                ZSER_fortetípus.Text = ZserAdat.Szerelvénytípus;
                ZSER_kocsiszám.Text = ZserAdat.Kocsikszáma.ToString();
                ZSER_megjegyzés.Text = ZserAdat.Megjegyzés;
                ZSER_kocsi1.Text = ZserAdat.Kocsi1;
                ZSER_kocsi2.Text = ZserAdat.Kocsi2;
                ZSER_kocsi3.Text = ZserAdat.Kocsi3;
                ZSER_kocsi4.Text = ZserAdat.Kocsi4;
                ZSER_kocsi5.Text = ZserAdat.Kocsi5;
                ZSER_kocsi6.Text = ZserAdat.Kocsi6;
                ZSER_státus.Text = ZserAdat.Státus;
                ZSER_ellenőrző.Text = ZserAdat.Ellenőrző;
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

        private void ZSER_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_Főkönyv_ZSER> Adatok = KézZser.Lista_Adatok(Cmbtelephely, Dátum, Délelőtt ? "de" : "du");
                if (Adatok == null || Adatok.Count == 0) return;

                Adat_Főkönyv_ZSER Elem = (from a in Adatok
                                          where a.Viszonylat == ZSER_viszonylat.Text.Trim()
                                          && a.Forgalmiszám == ZSER_forgalmiszám.Text.Trim()
                                          && a.Tervindulás.ToString("MM-dd-yyyy HH:mm:ss") == ZSER_tervindulás.Value.ToString("MM-dd-yyyy HH:mm:ss")
                                          select a).FirstOrDefault();

                if (Elem != null)
                {
                    Adat_Főkönyv_ZSER ADAT = new Adat_Főkönyv_ZSER(
                             ZSER_viszonylat.Text.Trim(),
                             ZSER_forgalmiszám.Text.Trim(),
                             ZSER_tervindulás.Value);
                    KézZser.Törlés(Cmbtelephely, Dátum, Délelőtt ? "de" : "du", ADAT);
                    MessageBox.Show("Az adat törlése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (Változás != null) Változás();
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

        private void ZSER_adat_rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (ZSER_viszonylat.Text.Trim() == "") throw new HibásBevittAdat("Viszonylatot meg kell adni.");
                if (ZSER_forgalmiszám.Text.Trim() == "") throw new HibásBevittAdat("Forgalmi számot meg kell adni.");
                if (ZSER_napszak.Text.Trim() == "") ZSER_napszak.Text = "_";
                if (ZSER_fortetípus.Text.Trim() == "") ZSER_fortetípus.Text = "_";
                if (ZSER_kocsiszám.Text.Trim() == "") ZSER_napszak.Text = "0";
                if (!long.TryParse(ZSER_kocsiszám.Text, out long kocsiszám)) ZSER_kocsiszám.Text = "0";
                if (ZSER_megjegyzés.Text.Trim() == "") ZSER_megjegyzés.Text = "_";
                if (ZSER_kocsi1.Text.Trim() == "") ZSER_kocsi1.Text = "_";
                if (ZSER_kocsi2.Text.Trim() == "") ZSER_kocsi2.Text = "_";
                if (ZSER_kocsi3.Text.Trim() == "") ZSER_kocsi3.Text = "_";
                if (ZSER_kocsi4.Text.Trim() == "") ZSER_kocsi4.Text = "_";
                if (ZSER_kocsi5.Text.Trim() == "") ZSER_kocsi5.Text = "_";
                if (ZSER_kocsi6.Text.Trim() == "") ZSER_kocsi6.Text = "_";
                if (ZSER_státus.Text.Trim() == "") ZSER_státus.Text = "_";
                if (ZSER_ellenőrző.Text.Trim() == "") ZSER_ellenőrző.Text = "_";

                List<Adat_Főkönyv_ZSER> Adatok = KézZser.Lista_Adatok(Cmbtelephely, Dátum, Délelőtt ? "de" : "du");
                if (Adatok == null || Adatok.Count == 0) return;

                Adat_Főkönyv_ZSER Elem = (from a in Adatok
                                          where a.Viszonylat == ZSER_viszonylat.Text.Trim()
                                          && a.Forgalmiszám == ZSER_forgalmiszám.Text.Trim()
                                          && a.Tervindulás.ToString("MM-dd-yyyy HH:mm:ss") == ZSER_tervindulás.Value.ToString("MM-dd-yyyy HH:mm:ss")
                                          select a).FirstOrDefault();

                Adat_Főkönyv_ZSER ADAT = new Adat_Főkönyv_ZSER(
                   ZSER_viszonylat.Text.Trim(),
                   ZSER_forgalmiszám.Text.Trim(),
                   ZSER_tervindulás.Value,
                   ZSER_tényidulás.Value,
                   ZSER_tervérkezés.Value,
                   zser_tényérkezés.Value,
                   ZSER_napszak.Text.Trim(),
                   ZSER_fortetípus.Text.Trim(),
                   kocsiszám,
                   ZSER_megjegyzés.Text.Trim(),
                   ZSER_kocsi1.Text.Trim(),
                   ZSER_kocsi2.Text.Trim(),
                   ZSER_kocsi3.Text.Trim(),
                   ZSER_kocsi4.Text.Trim(),
                   ZSER_kocsi5.Text.Trim(),
                   ZSER_kocsi6.Text.Trim(),
                   ZSER_ellenőrző.Text.Trim(),
                   ZSER_státus.Text.Trim());

                if (Elem != null)
                    KézZser.Módosítás(Cmbtelephely, Dátum, Délelőtt ? "de" : "du", ADAT);
                else
                    KézZser.Rögzítés(Cmbtelephely, Dátum, Délelőtt ? "de" : "du", ADAT);

                MessageBox.Show("Az adat rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (Változás != null) Változás();
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
