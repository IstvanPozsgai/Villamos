using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Adatszerkezet;
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
        readonly Kezelő_CAF_Adatok KezCafAdatok = new Kezelő_CAF_Adatok();
        readonly Kezelő_kiegészítő_telephely KézTelephely = new Kezelő_kiegészítő_telephely();
        readonly Kezelő_Főkönyv_Zser_Km KézZSerKm = new Kezelő_Főkönyv_Zser_Km();
        readonly Kezelő_CAF_KM_Attekintes KézCafKm = new Kezelő_CAF_KM_Attekintes();

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

        public Ablak_CAF_Alapadat()
        {
            InitializeComponent();
        }

        private void Start()
        {
            AdatokCiklus = KézCiklus.Lista_Adatok(true);

            //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
            //ha nem akkor a régit használjuk
            if (Program.PostásJogkör.Substring(0, 1) == "R")
                GombLathatosagKezelo.Beallit(this);
            else
                Jogosultságkiosztás();

            Pályaszámokfeltöltése();
            Vizsgsorszámcombofeltölés();
            Üzemek_listázása();


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

                List<Adat_kiegészítő_telephely> Adatok = KézTelephely.Lista_Adatok().OrderBy(a => a.Sorszám).ToList();

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
                // Ebben az eljárásban minden kocsi adatát akarjuk  ellenőrizni
                List<Adat_CAF_alap> Adatok = KézCAFAlap.Lista_Adatok(true);

                AdatokZser.Clear();
                AdatokZser = KézZSerKm.Lista_adatok(DateTime.Today.Year - 1);
                List<Adat_Főkönyv_Zser_Km> Ideig = KézZSerKm.Lista_adatok(DateTime.Today.Year);
                AdatokZser.AddRange(Ideig);

                if (Adatok != null)
                {
                    Holtart.Be();
                    List<Adat_CAF_alap> AdatokGy = new List<Adat_CAF_alap>();
                    foreach (Adat_CAF_alap rekord in Adatok)
                    {
                        long havikm = 0;

                        List<Adat_Főkönyv_Zser_Km> vane = (from a in AdatokZser
                                                           where a.Azonosító.Trim() == rekord.Azonosító.Trim()
                                                           && a.Dátum >= DateTime.Now.AddDays(-30)
                                                           select a).ToList();

                        if (vane != null) havikm = vane.Sum(t => t.Napikm);

                        vane = (from t in AdatokZser
                                where t.Azonosító.Trim() == rekord.Azonosító.Trim()
                                && t.Dátum > rekord.Vizsgdátum_km
                                select t).ToList();

                        //Számláló az utolsó vizsgálat km óra állása
                        //kmu ==Jelenlegi becsült KM állás
                        long kmukm = rekord.Számláló;
                        if (vane != null)
                        {
                            kmukm += vane.Sum(t => t.Napikm);
                            Utolsó_vizsgóta.Text = vane.Sum(t => t.Napikm).ToString();
                        }

                        Adat_CAF_alap ADAT = new Adat_CAF_alap(
                                            rekord.Azonosító.Trim(),
                                            havikm,
                                            kmukm,
                                            DateTime.Today);
                        AdatokGy.Add(ADAT);

                        Adat_CAF_Adatok utolso_km = KezCafAdatok.Utolso_Km_Vizsgalat_Adatai(rekord.Azonosító.Trim());
                        Adat_CAF_Adatok utolso_ido = KezCafAdatok.Utolso_Ido_Vizsgalat_Adatai(rekord.Azonosító.Trim());

                        if (utolso_ido != null) KézCAFAlap.Módosítás_Kész_Ido(utolso_ido.Vizsgálat, utolso_ido.IDŐ_Sorszám, utolso_ido.Dátum, utolso_ido.Telephely, utolso_ido.Számláló, utolso_ido.Azonosító);
                        if (utolso_km != null) KézCAFAlap.Módosítás_Kész_Km(utolso_km.Vizsgálat, utolso_km.KM_Sorszám, utolso_km.Dátum, utolso_km.Telephely, utolso_km.Számláló, utolso_km.Azonosító);

                        Holtart.Lép();

                    }
                    KézCAFAlap.Módosítás_kmAdat(AdatokGy);
                    
                    //Frissítem a km adatokat a CAF_KM_Attekintes táblában
                    foreach (Adat_CAF_alap rekord in Adatok)
                        KézCafKm.Erteket_Frissit_Osszes(rekord.Azonosító);
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

        private void Alapadatokat_kiír()
        {
            try
            {
                if (Alap_pályaszám.Text.Trim() == "") return;
                List<Adat_CAF_alap> Adatok = KézCAFAlap.Lista_Adatok();
                Adat_CAF_alap Adat = Adatok.FirstOrDefault(a => a.Azonosító.Trim() == Alap_pályaszám.Text.Trim());

                //Lekérem az adatok táblából a tervezési(0)/ütemezési(2) státuszu járműveket, hogy megkapjam az időbeli vizsgálathoz az utolsó rögzített km állást.
                List<Adat_CAF_Adatok> Caf_Adatok_Tabla = KezCafAdatok.Lista_Adatok();
                Adat_CAF_Adatok Caf_Adatok_Tabla_Adat = Caf_Adatok_Tabla.Where(a => a.Státus <= 2)
                    .FirstOrDefault(a => a.Azonosító.Trim() == Alap_pályaszám.Text.Trim());

                Adat_CAF_Adatok utolso_km = KezCafAdatok.Utolso_Km_Vizsgalat_Adatai(Alap_pályaszám.Text.Trim());
                Adat_CAF_Adatok utolso_ido = KezCafAdatok.Utolso_Ido_Vizsgalat_Adatai(Alap_pályaszám.Text.Trim());
                //Adat_CAF_alap zser = Kalkulál_Temp(KezCafAdatok.Lista_Adatok().FirstOrDefault(a => a.Azonosító == Alap_pályaszám.Text.Trim()));

                if (Adat != null && utolso_km != null && utolso_ido != null)
                {
                    Alap_ciklus_idő.Text = Adat.Ciklusnap;
                    Ciklus_IDŐ_Sorszám_feltöltés();
                    Alap_vizsg_idő.Text = utolso_ido.Vizsgálat.ToString();
                    Alap_vizsg_sorszám_idő.Text = utolso_ido.IDŐ_Sorszám.ToString();
                    ALAP_Üzemek_nap.Text = utolso_ido.Telephely;
                    Alap_dátum_idő.Value = utolso_ido.Dátum;


                    Alap_ciklus_km.Text = Adat.Cikluskm;
                    Ciklus_KM_Sorszám_feltöltés();
                    Alap_vizsg_km.Text = utolso_km.Vizsgálat.ToString();
                    Alap_vizsg_sorszám_km.Text = utolso_km.KM_Sorszám.ToString();
                    ALAP_Üzemek_km.Text = utolso_km.Telephely.ToString();
                    Alap_dátum_km.Value = utolso_km.Dátum;

                    if (utolso_km.KmRogzitett_e || utolso_km.Számláló == 0)
                    {
                        Alap_KM_számláló.BackColor = Color.Red;
                    }
                    else
                    {
                        Alap_KM_számláló.BackColor = Color.Green;
                    }
                    Alap_KM_számláló.Text = utolso_km.Számláló.ToString();

                    if (utolso_ido.KmRogzitett_e || utolso_ido.Számláló == 0)
                    {
                        utolso_vizsgalat_km.BackColor = Color.Red;
                    }
                    else
                    {
                        utolso_vizsgalat_km.BackColor = Color.Green;
                    }
                    utolso_vizsgalat_km.Text = utolso_ido.Számláló.ToString();

                    Alap_Havi_km.Text = Adat.Havikm.ToString();
                    if (utolso_km.Számláló > utolso_ido.Számláló)
                    {
                        Alap_KMU.Text = utolso_km.Számláló.ToString();
                        vegso_km_tbox.Text = (utolso_km.Számláló + Adat.Havikm).ToString();
                    }
                    else
                    {
                        Alap_KMU.Text = utolso_ido.Számláló.ToString();
                        vegso_km_tbox.Text = (utolso_ido.Számláló + Adat.Havikm).ToString();
                    }
                    Alap_Össz_km.Text = Adat.Teljeskm.ToString();
                    Alap_Dátum_frissítés.Value = Adat.KMUdátum;
                    Alap_Típus.Text = Adat.Típus;
                    Alap_felújítás.Value = Adat.Fudátum;
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
                Pályaszámokfeltöltése();
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
                    Alap_vizsg_sorszám_km.Items.Add(item.Sorszám);
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

        private void TörliBeviteliMezőket()
        {
            Alap_ciklus_idő.Text = "";

            Alap_vizsg_idő.Text = "";
            Alap_vizsg_sorszám_idő.Text = "";
            ALAP_Üzemek_nap.Text = "";
            Alap_dátum_idő.Value = new DateTime(1900, 1, 1);

            Alap_ciklus_km.Text = "";

            Alap_vizsg_km.Text = "";
            Alap_vizsg_sorszám_km.Text = "";
            ALAP_Üzemek_km.Text = "";
            Alap_dátum_km.Value = new DateTime(1900, 1, 1);
            Alap_KM_számláló.Text = "";

            Alap_Havi_km.Text = "";
            Alap_KMU.Text = "";
            Alap_Össz_km.Text = "";
            Alap_Dátum_frissítés.Value = new DateTime(1900, 1, 1);
            Alap_Típus.Text = "";
            Alap_felújítás.Value = new DateTime(1900, 1, 1);

            Utolsó_vizsgóta.Text = "";
            Alap_Státus.Checked = false;
            Alap_Garancia.Checked = false;
        }


        #region Pályaszám
        private void Alap_pályaszám_TextUpdate(object sender, EventArgs e)
        {
            if (Alap_pályaszám.Items.Contains(Alap_pályaszám.Text))
                Alapadatokat_kiír();
            else
                TörliBeviteliMezőket();
        }

        private void Pályaszámokfeltöltése()
        {
            try
            {
                AdatokCAFAlap = KézCAFAlap.Lista_Adatok();
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

        private void Alap_pályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Alapadatokat_kiír();
        }
        #endregion
    }
}
