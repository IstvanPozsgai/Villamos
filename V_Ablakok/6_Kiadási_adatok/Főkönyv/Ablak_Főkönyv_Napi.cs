using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok._6_Kiadási_adatok.Főkönyv
{
    public partial class Ablak_Főkönyv_Napi : Form
    {
        public string Cmbtelephely { get; private set; }
        public bool Délelőtt { get; private set; }
        public DateTime Dátum { get; private set; }
        public Adat_Főkönyv_ZSER ZserAdat { get; private set; }

        public event Event_Kidobó Változás;

        public Ablak_Főkönyv_Napi(string cmbtelephely, bool délelőtt, DateTime dátum, Adat_Főkönyv_ZSER zserAdat)
        {
            Cmbtelephely = cmbtelephely;
            Délelőtt = délelőtt;
            Dátum = dátum;
            ZserAdat = zserAdat;
            InitializeComponent();
        }




        private void Ablak_Főkönyv_Napi_Load(object sender, EventArgs e)
        {
            Forte_típus_feltöltése();
            Napszak_feltöltés();
            ZSER_részletes_adatok();
        }


        private void Forte_típus_feltöltése()
        {
            string hely = $@"{Application.StartupPath}\{Cmbtelephely}\adatok\segéd\Kiegészítő.mdb";
            string jelszó = "Mocó";
            string szöveg = "SELECT * FROM fortetipus ORDER BY ftípus";
            ZSER_fortetípus.Items.Clear();
            ZSER_fortetípus.BeginUpdate();
            ZSER_fortetípus.Items.AddRange(MyF.ComboFeltöltés(hely, jelszó, szöveg, "ftípus"));
            ZSER_fortetípus.EndUpdate();
            ZSER_fortetípus.Refresh();
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
                string hely = $@"{Application.StartupPath}\{Cmbtelephely}\adatok\főkönyv\{Dátum.Year}\zser\zser{Dátum:yyyyMMdd}";
                if (Délelőtt)
                    hely += "de.mdb";
                else
                    hely += "du.mdb";

                if (!System.IO.File.Exists(hely)) return;

                string jelszó = "lilaakác";

                Kezelő_Főkönyv_ZSER KézZser = new Kezelő_Főkönyv_ZSER();
                string szöveg = "SELECT * FROM zseltábla";
                List<Adat_Főkönyv_ZSER> Adatok = KézZser.Lista_adatok(hely, jelszó, szöveg);

                Adat_Főkönyv_ZSER Elem = (from a in Adatok
                                          where a.Viszonylat == ZSER_viszonylat.Text.Trim()
                                          && a.Forgalmiszám == ZSER_forgalmiszám.Text.Trim()
                                          && a.Tervindulás.ToString("MM-dd-yyyy HH:mm:ss") == ZSER_tervindulás.Value.ToString("MM-dd-yyyy HH:mm:ss")
                                          select a).FirstOrDefault();

                if (Elem != null)
                {
                    szöveg = "DELETE FROM zseltábla  WHERE viszonylat='" + ZSER_viszonylat.Text.Trim() + "' ";
                    szöveg += " And forgalmiszám='" + ZSER_forgalmiszám.Text.Trim() + "' ";
                    szöveg += "And tervindulás=#" + ZSER_tervindulás.Value.ToString("MM-dd-yyyy HH:mm:ss") + "#";
                    MyA.ABtörlés(hely, jelszó, szöveg);
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
                if (!int.TryParse(ZSER_kocsiszám.Text, out int result)) ZSER_napszak.Text = "0";
                if (ZSER_megjegyzés.Text.Trim() == "") ZSER_megjegyzés.Text = "_";
                if (ZSER_kocsi1.Text.Trim() == "") ZSER_kocsi1.Text = "_";
                if (ZSER_kocsi2.Text.Trim() == "") ZSER_kocsi2.Text = "_";
                if (ZSER_kocsi3.Text.Trim() == "") ZSER_kocsi3.Text = "_";
                if (ZSER_kocsi4.Text.Trim() == "") ZSER_kocsi4.Text = "_";
                if (ZSER_kocsi5.Text.Trim() == "") ZSER_kocsi5.Text = "_";
                if (ZSER_kocsi6.Text.Trim() == "") ZSER_kocsi6.Text = "_";
                if (ZSER_státus.Text.Trim() == "") ZSER_státus.Text = "_";
                if (ZSER_ellenőrző.Text.Trim() == "") ZSER_ellenőrző.Text = "_";

                string hely = $@"{Application.StartupPath}\{Cmbtelephely}\adatok\főkönyv\{Dátum.Year}\zser\zser{Dátum:yyyyMMdd}";

                if (Délelőtt)
                    hely += "de.mdb";
                else
                    hely += "du.mdb";

                if (!System.IO.File.Exists(hely)) return;
                string jelszó = "lilaakác";
                Kezelő_Főkönyv_ZSER KézZser = new Kezelő_Főkönyv_ZSER();
                string szöveg = "SELECT * FROM zseltábla";
                List<Adat_Főkönyv_ZSER> Adatok = KézZser.Lista_adatok(hely, jelszó, szöveg);

                Adat_Főkönyv_ZSER Elem = (from a in Adatok
                                          where a.Viszonylat == ZSER_viszonylat.Text.Trim()
                                          && a.Forgalmiszám == ZSER_forgalmiszám.Text.Trim()
                                          && a.Tervindulás.ToString("MM-dd-yyyy HH:mm:ss") == ZSER_tervindulás.Value.ToString("MM-dd-yyyy HH:mm:ss")
                                          select a).FirstOrDefault();

                if (Elem != null)
                {
                    // ha van 
                    szöveg = "UPDATE zseltábla  SET ";
                    szöveg += "tényindulás='" + ZSER_tényidulás.Value.ToString("MM-dd-yyyy HH:mm:ss") + "', "; // tényindulás
                    szöveg += "tervérkezés='" + ZSER_tervérkezés.Value.ToString("MM-dd-yyyy HH:mm:ss") + "', "; // tervérkezés
                    szöveg += "tényérkezés='" + zser_tényérkezés.Value.ToString("MM-dd-yyyy HH:mm:ss") + "', "; // tényérkezés
                    szöveg += "napszak='" + ZSER_napszak.Text.Trim() + "', "; // napszak
                    szöveg += "szerelvénytípus='" + ZSER_fortetípus.Text.Trim() + "', "; // szerelvénytípus
                    szöveg += "kocsikszáma=" + ZSER_kocsiszám.Text + ", "; // kocsikszáma
                    szöveg += "megjegyzés='" + ZSER_megjegyzés.Text.Trim() + "', "; // megjegyzés
                    szöveg += "kocsi1='" + ZSER_kocsi1.Text.Trim() + "', "; // kocsi1
                    szöveg += "kocsi2='" + ZSER_kocsi2.Text.Trim() + "', "; // kocsi2
                    szöveg += "kocsi3='" + ZSER_kocsi3.Text.Trim() + "', "; // kocsi3
                    szöveg += "kocsi4='" + ZSER_kocsi4.Text.Trim() + "', "; // kocsi4
                    szöveg += "kocsi5='" + ZSER_kocsi5.Text.Trim() + "', "; // kocsi5
                    szöveg += "kocsi6='" + ZSER_kocsi6.Text.Trim() + "', "; // kocsi6
                    szöveg += "ellenőrző='" + ZSER_ellenőrző.Text.Trim() + "', "; // ellenőrző
                    szöveg += "Státus='" + ZSER_státus.Text.Trim() + "'"; // Státus
                    szöveg += " WHERE viszonylat='" + ZSER_viszonylat.Text.Trim() + "' ";
                    szöveg += " And forgalmiszám='" + ZSER_forgalmiszám.Text.Trim() + "' ";
                    szöveg += "And tervindulás=#" + ZSER_tervindulás.Value.ToString("MM-dd-yyyy HH:mm:ss") + "#";
                }
                else
                {
                    // ha nincs
                    szöveg = "INSERT INTO zseltábla (viszonylat, forgalmiszám, tervindulás, tényindulás, tervérkezés,";
                    szöveg += " tényérkezés, napszak, szerelvénytípus, kocsikszáma, megjegyzés, kocsi1, kocsi2, kocsi3, kocsi4, kocsi5, kocsi6, ellenőrző, Státus ) VALUES (";
                    szöveg += "'" + ZSER_viszonylat.Text.Trim() + "', "; // viszonylat
                    szöveg += "'" + ZSER_forgalmiszám.Text.Trim() + "', "; // forgalmiszám
                    szöveg += "'" + ZSER_tervindulás.Value.ToString("MM-dd-yyyy HH:mm:ss") + "', "; // tervindulás
                    szöveg += "'" + ZSER_tényidulás.Value.ToString("MM-dd-yyyy HH:mm:ss") + "', "; // tényindulás
                    szöveg += "'" + ZSER_tervérkezés.Value.ToString("MM-dd-yyyy HH:mm:ss") + "', "; // tervérkezés
                    szöveg += "'" + zser_tényérkezés.Value.ToString("MM-dd-yyyy HH:mm:ss") + "', "; // tényérkezés
                    szöveg += "'" + ZSER_napszak.Text.Trim() + "', "; // napszak
                    szöveg += "'" + ZSER_fortetípus.Text.Trim() + "', "; // szerelvénytípus
                    szöveg += ZSER_kocsiszám.Text + ", "; // kocsikszáma
                    szöveg += "'" + ZSER_megjegyzés.Text.Trim() + "', "; // megjegyzés
                    szöveg += "'" + ZSER_kocsi1.Text.Trim() + "', "; // kocsi1
                    szöveg += "'" + ZSER_kocsi2.Text.Trim() + "', "; // kocsi2
                    szöveg += "'" + ZSER_kocsi3.Text.Trim() + "', "; // kocsi3
                    szöveg += "'" + ZSER_kocsi4.Text.Trim() + "', "; // kocsi4
                    szöveg += "'" + ZSER_kocsi5.Text.Trim() + "', "; // kocsi5
                    szöveg += "'" + ZSER_kocsi6.Text.Trim() + "', "; // kocsi6
                    szöveg += "'" + ZSER_ellenőrző.Text.Trim() + "', "; // ellenőrző
                    szöveg += "'" + ZSER_státus.Text.Trim() + "')";
                } // Státus
                MyA.ABMódosítás(hely, jelszó, szöveg);
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
