using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    public partial class Ablak_CAF_Alapadat : Form
    {
        public string Azonosító { get; private set; }

        #region Kezelők és Listák


        readonly Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();
        readonly Kezelő_Főkönyv_Zser_Km KézZser = new Kezelő_Főkönyv_Zser_Km();
        readonly Kezelő_CAF_alap KézCAFAlap = new Kezelő_CAF_alap();
        readonly Kezelő_kiegészítő_telephely KézTelephely = new Kezelő_kiegészítő_telephely();

        List<Adat_CAF_alap> AdatokCAFAlap = new List<Adat_CAF_alap>();
        List<Adat_Főkönyv_Zser_Km> AdatokZser = new List<Adat_Főkönyv_Zser_Km>();
        List<Adat_Ciklus> AdatokCiklus = new List<Adat_Ciklus>();
        #endregion

        public Ablak_CAF_Alapadat(string azonosító)
        {
            InitializeComponent();
            Azonosító = azonosító;
            Start();

        }

        void Start()
        {
            Jogosultságkiosztás();
            Pályaszámokfeltöltése();
            Vizsgsorszámcombofeltölés();
            Üzemek_listázása();
            AdatokCiklus = KézCiklus.Lista_Adatok(true);
            AdatokCAFAlap = KézCAFAlap.Lista_Adatok();

            CiklusrendCombok_feltöltés();
            CiklusrendCombok_feltöltés();
            if (Azonosító.Trim() != "")
            {
                Alap_pályaszám.Text = Azonosító;
                Alapadatokat_kiír();
            }
        }

        private void Ablak_CAF_Alapadat_Load(object sender, EventArgs e)
        {

        }

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Alap_rögzít.Enabled = false;

                Kalkulál.Enabled = false;


                // csak főmérnökségi belépéssel módosítható

                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                    Alap_rögzít.Visible = true;
                else
                    Alap_rögzít.Visible = false;


                melyikelem = 115;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Alap_rögzít.Enabled = true;
                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                {

                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Kalkulál.Enabled = true;
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

        private void Pályaszámokfeltöltése()
        {
            try
            {
                Alap_pályaszám.Items.Clear();

                foreach (Adat_CAF_alap Elem in AdatokCAFAlap)
                    Alap_pályaszám.Items.Add(Elem.Azonosító);
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

        private void Vizsgsorszámcombofeltölés()
        {
            try
            {
                Alap_ciklus_idő.Items.Clear();
                Alap_ciklus_km.Items.Clear();

                AdatokCiklus = KézCiklus.Lista_Adatok();
                List<string> AdatokSzöveg = AdatokCiklus.OrderBy(a => a.Típus).Where(a => a.Törölt == "0").Select(t => t.Típus).Distinct().ToList();
                foreach (string elem in AdatokSzöveg)
                {
                    Alap_ciklus_idő.Items.Add(elem);
                    Alap_ciklus_km.Items.Add(elem);
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

        private void Üzemek_listázása()
        {
            try
            {
                ALAP_Üzemek_km.Items.Clear();
                ALAP_Üzemek_nap.Items.Clear();

                List<Adat_kiegészítő_telephely> Adatok = KézTelephely.Lista_adatok().OrderBy(a => a.Sorszám).ToList();

                foreach (Adat_kiegészítő_telephely Elem in Adatok)
                {
                    ALAP_Üzemek_km.Items.Add(Elem.Telephelykönyvtár);
                    ALAP_Üzemek_nap.Items.Add(Elem.Telephelykönyvtár);
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

        private void Kalkulál_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\CAF\CAF.mdb";
                string jelszó = "CzabalayL";
                string szöveg = "SELECT * FROM alap WHERE törölt=false ORDER BY azonosító";

                List<Adat_CAF_alap> Adatok = KézCAFAlap.Lista_Adatok();

                NapiZSerListaFeltöltés();
                if (Adatok != null)
                {
                    Holtart.Be();
                    List<string> SzövegGy = new List<string>();
                    foreach (Adat_CAF_alap rekord in Adatok)
                    {
                        long havikm = 0;
                        double kmukm = 0;

                        List<Adat_Főkönyv_Zser_Km> vane = (from a in AdatokZser
                                                           where a.Azonosító.Trim() == rekord.Azonosító.Trim()
                                                           orderby a.Dátum descending
                                                           select a).Take(30).ToList();
                        if (vane != null) havikm = vane.Sum(t => t.Napikm);

                        vane = (from t in AdatokZser
                                where t.Azonosító.Trim() == rekord.Azonosító.Trim()
                                && t.Dátum > rekord.Vizsgdátum_km
                                select t).ToList();
                        if (vane != null) kmukm = vane.Sum(t => t.Napikm);

                        // módosítjuk az adatokat
                        szöveg = "UPDATE alap  SET ";
                        szöveg += $" kmukm={kmukm}, ";
                        szöveg += $" havikm={havikm}, ";
                        szöveg += $" KMUdátum=#{DateTime.Today:M-d-yy}# ";
                        szöveg += $" WHERE azonosító='{rekord.Azonosító.Trim()}'";
                        SzövegGy.Add(szöveg);

                        Holtart.Lép();
                    }
                    MyA.ABMódosítás(hely, jelszó, SzövegGy);
                }

                Holtart.Ki();
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

        private void Lekérdezés_lekérdezés_Click(object sender, EventArgs e)
        {
            Alapadatokat_kiír();
        }

        private void Alap_pályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Alapadatokat_kiír();
        }

        private void Alapadatokat_kiír()
        {
            try
            {
                if (Alap_pályaszám.Text.Trim() == "") return;
                List<Adat_CAF_alap> Adatok = KézCAFAlap.Lista_Adatok();
                Adat_CAF_alap Adat = Adatok.FirstOrDefault(a => a.Azonosító.Trim() == Alap_pályaszám.Text.Trim());
                if (Adat != null)
                {
                    Alap_ciklus_idő.Text = Adat.Ciklusnap;
                    Ciklus_IDŐ_Sorszám_feltöltés();
                    Alap_vizsg_idő.Text = Adat.Utolsó_Nap;
                    Alap_vizsg_sorszám_idő.Text = Adat.Utolsó_Nap_sorszám.ToString();
                    ALAP_Üzemek_nap.Text = Adat.Végezte_nap;
                    Alap_dátum_idő.Value = Adat.Vizsgdátum_nap;

                    Alap_ciklus_km.Text = Adat.Cikluskm;
                    Ciklus_KM_Sorszám_feltöltés();
                    Alap_vizsg_km.Text = Adat.Utolsó_Km;
                    Alap_vizsg_sorszám_km.Text = Adat.Utolsó_Km_sorszám.ToString();
                    ALAP_Üzemek_km.Text = Adat.Végezte_km;
                    Alap_dátum_km.Value = Adat.Vizsgdátum_km;
                    Alap_KM_számláló.Text = Adat.Számláló.ToString();

                    Alap_Havi_km.Text = Adat.Havikm.ToString();
                    Alap_KMU.Text = Adat.KMUkm.ToString();
                    Alap_Össz_km.Text = Adat.Teljeskm.ToString();
                    Alap_Dátum_frissítés.Value = Adat.KMUdátum;
                    Alap_Típus.Text = Adat.Típus;
                    Alap_felújítás.Value = Adat.Fudátum;

                    Utolsó_vizsgóta.Text = (Adat.KMUkm - Adat.Számláló).ToString();
                    Alap_Státus.Checked = Adat.Törölt;
                    Alap_Garancia.Checked = Adat.Garancia;
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

        private void Ciklus_IDŐ_Sorszám_feltöltés()
        {
            try
            {
                Alap_vizsg_sorszám_idő.Items.Clear();
                if (Alap_ciklus_idő.Text.Trim() == "") return;

                List<Adat_Ciklus> Adatok = AdatokCiklus.Where(a => a.Típus.Trim() == Alap_ciklus_idő.Text.Trim()).ToList();
                foreach (Adat_Ciklus item in Adatok)
                    Alap_vizsg_sorszám_idő.Items.Add(item.Sorszám);

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

        private void E_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Alap_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Pályaszám mező nincs kitöltve.");
                if (Alap_ciklus_idő.Text.Trim() == "") Alap_ciklus_idő.Text = "_";
                if (Alap_vizsg_idő.Text.Trim() == "") Alap_vizsg_idő.Text = "_";
                if (ALAP_Üzemek_nap.Text.Trim() == "") ALAP_Üzemek_nap.Text = "_";
                if (Alap_ciklus_km.Text.Trim() == "") Alap_ciklus_km.Text = "_";
                if (Alap_vizsg_km.Text.Trim() == "") Alap_vizsg_km.Text = "_";
                if (ALAP_Üzemek_km.Text.Trim() == "") ALAP_Üzemek_km.Text = "_";
                if (Alap_KM_számláló.Text.Trim() == "") Alap_KM_számláló.Text = "0";
                if (Alap_Havi_km.Text.Trim() == "") Alap_Havi_km.Text = "0";
                if (Alap_KMU.Text.Trim() == "") Alap_KMU.Text = "0";
                if (Alap_Össz_km.Text.Trim() == "") Alap_Össz_km.Text = "0";
                if (Alap_vizsg_sorszám_idő.Text.Trim() == "") Alap_vizsg_sorszám_idő.Text = "0";
                if (Alap_vizsg_sorszám_km.Text.Trim() == "") Alap_vizsg_sorszám_km.Text = "0";
                if (!int.TryParse(Alap_KM_számláló.Text, out int alap_KM_számláló)) alap_KM_számláló = 0;
                if (!int.TryParse(Alap_Havi_km.Text, out int alap_Havi_km)) alap_Havi_km = 0;
                if (!int.TryParse(Alap_KMU.Text, out int alap_KMU)) alap_KMU = 0;
                if (!int.TryParse(Alap_Össz_km.Text, out int alap_Össz_km)) alap_Össz_km = 0;
                if (!int.TryParse(Alap_vizsg_sorszám_idő.Text, out int alap_vizsg_sorszám_idő)) alap_vizsg_sorszám_idő = 0;
                if (!int.TryParse(Alap_vizsg_sorszám_km.Text, out int alap_vizsg_sorszám_km)) alap_vizsg_sorszám_km = 0;


                bool vane = AdatokCAFAlap.Any(a => a.Azonosító.Trim() == Alap_pályaszám.Text.Trim());
                Adat_CAF_alap ADAT = new Adat_CAF_alap(
                                        Alap_pályaszám.Text.Trim(),
                                        Alap_ciklus_idő.Text.Trim(),
                                        Alap_vizsg_idő.Text.Trim(),
                                        alap_vizsg_sorszám_idő,
                                        ALAP_Üzemek_nap.Text.Trim(),
                                        Alap_dátum_idő.Value,
                                        Alap_ciklus_km.Text.Trim(),
                                        Alap_vizsg_km.Text.Trim(),
                                        alap_vizsg_sorszám_km,
                                        ALAP_Üzemek_km.Text.Trim(),
                                        Alap_dátum_km.Value,
                                        alap_KM_számláló,
                                        alap_Havi_km,
                                        alap_KMU,
                                        Alap_Dátum_frissítés.Value,
                                        Alap_felújítás.Value,
                                        alap_Össz_km,
                                        Alap_Típus.Text.Trim(),
                                        Alap_Garancia.Checked,
                                        Alap_Státus.Checked);
                if (vane)
                    KézCAFAlap.Módosítás(ADAT);
                else
                    KézCAFAlap.Rögzítés(ADAT);
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

        private void Alap_ciklus_idő_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ciklus_IDŐ_Sorszám_feltöltés();
        }

        private void Alap_ciklus_km_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ciklus_KM_Sorszám_feltöltés();
        }

        private void Ciklus_KM_Sorszám_feltöltés()
        {
            try
            {
                Alap_vizsg_sorszám_km.Items.Clear();
                if (Alap_ciklus_km.Text.Trim() == "") return;

                List<Adat_Ciklus> Adatok = AdatokCiklus.Where(a => a.Típus.Trim() == Alap_ciklus_km.Text.Trim()).ToList();
                foreach (Adat_Ciklus item in Adatok)
                    Alap_vizsg_sorszám_idő.Items.Add(item.Sorszám);
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

        private void CiklusrendCombok_feltöltés()
        {
            try
            {
                Alap_ciklus_idő.Items.Clear();
                Alap_ciklus_km.Items.Clear();

                List<string> Adatok = (from a in AdatokCiklus
                                       orderby a.Típus
                                       select a.Típus).Distinct().ToList();
                foreach (string item in Adatok)
                {
                    Alap_ciklus_idő.Items.Add(item);
                    Alap_ciklus_km.Items.Add(item);
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

        private void Alap_vizsg_sorszám_idő_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Alap_ciklus_idő.Text.Trim() == "") return;
                if (Alap_vizsg_sorszám_idő.Text.Trim() == "") return;

                if (!long.TryParse(Alap_vizsg_sorszám_idő.Text.Trim(), out long SorSzám)) SorSzám = 0;
                Adat_Ciklus vane = (from a in AdatokCiklus
                                    where a.Törölt == "0" &&
                                    a.Sorszám == SorSzám &&
                                    a.Típus.Trim() == Alap_ciklus_idő.Text.Trim()
                                    select a).FirstOrDefault();

                if (vane != null) Alap_vizsg_idő.Text = vane.Vizsgálatfok;
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

        private void Alap_vizsg_sorszám_km_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Alap_ciklus_km.Text.Trim() == "") return;
                if (Alap_vizsg_sorszám_km.Text.Trim() == "") return;
                if (!long.TryParse(Alap_vizsg_sorszám_idő.Text.Trim(), out long SorSzám)) SorSzám = 0;
                Adat_Ciklus vane = (from a in AdatokCiklus
                                    where a.Törölt == "0" &&
                                    a.Sorszám == SorSzám &&
                                    a.Típus.Trim() == Alap_ciklus_km.Text.Trim()
                                    select a).FirstOrDefault();
                if (vane != null) Alap_vizsg_km.Text = vane.Vizsgálatfok;
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

        private void Ablak_CAF_Alapadat_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }

        private void NapiZSerListaFeltöltés()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today.Year - 1}\Napi_km_Zser_{DateTime.Today.Year - 1}.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM Tábla";
                List<Adat_Főkönyv_Zser_Km> Ideig = new List<Adat_Főkönyv_Zser_Km>();
                AdatokZser.Clear();
                if (File.Exists(hely)) AdatokZser = KézZser.Lista_adatok(hely, jelszó, szöveg);
                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{DateTime.Today.Year}\Napi_km_Zser_{DateTime.Today.Year}.mdb";
                if (File.Exists(hely)) Ideig = KézZser.Lista_adatok(hely, jelszó, szöveg);
                if (Ideig.Any()) AdatokZser.AddRange(Ideig);
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
