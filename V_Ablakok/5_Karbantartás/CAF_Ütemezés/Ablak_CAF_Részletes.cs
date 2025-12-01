using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyCaf = Villamos.Villamos_Ablakok.CAF_Ütemezés.CAF_Közös_Eljárások;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    public partial class Ablak_CAF_Részletes : Form
    {
        public event Event_Kidobó Változás;
        public CAF_Segéd_Adat Posta_Segéd { get; private set; }
        public DateTime Elő_Dátumig { get; private set; }

        readonly Kezelő_CAF_Adatok KézAdatok = new Kezelő_CAF_Adatok();
        readonly Kezelő_CAF_alap AlapKéz = new Kezelő_CAF_alap();
        readonly Kezelő_Ciklus Kéz_Ciklus = new Kezelő_Ciklus();
        readonly Kezelő_CAF_KM_Attekintes KézCafKm = new Kezelő_CAF_KM_Attekintes();

        Adat_CAF_alap EgyCAF;

        List<Adat_Ciklus> Ciklus = new List<Adat_Ciklus>();
        List<Adat_Ciklus> Ciklus_Idő = null;
        List<Adat_Ciklus> Ciklus_Km = null;

        #region Alap
        public Ablak_CAF_Részletes(CAF_Segéd_Adat posta_Segéd, DateTime elő_Dátumig)
        {
            InitializeComponent();

            Posta_Segéd = posta_Segéd;
            Elő_Dátumig = elő_Dátumig;
            Start();

            if (Program.PostásNév == "POZSGAII" || Program.PostásNév == "PAPR")
            {
                btn_elso_futtatas.Visible = true;
            }
        }

        public Ablak_CAF_Részletes()
        {
            InitializeComponent();
        }
        private void Start()
        {
            EgyCAF = AlapKéz.Egy_Adat(Posta_Segéd.Azonosító.Trim());

            Státus_feltöltés();
            Ütem_Pályaszámokfeltöltése();

            Ciklus = Kéz_Ciklus.Lista_Adatok(true);
            Ütem_Ciklus_IDŐ_Sorszám_feltöltés();
            Ütem_Ciklus_KM_Sorszám_feltöltés();
            if (Posta_Segéd.Sorszám > 0)
            {
                AdatokKiírása();
                KiírJobbOldal();
            }
            else
            {
                AdatokKeresés();
                KiírJobbOldal();
            }
            GombLathatosagKezelo.Beallit(this);
            Jogosultságkiosztás();
        }

        private void Ablak_CAF_Részletes_Load(object sender, EventArgs e)
        {

        }

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Ütem_Rögzít.Enabled = false;

                // csak főmérnökségi belépéssel módosítható

                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Ütem_Rögzít.Visible = true;
                }
                else
                {
                    Ütem_Rögzít.Visible = false;
                }


                melyikelem = 118;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Ütem_Rögzít.Enabled = true;
                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                {

                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {

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


        // JAVÍTANDÓ:ez lehetne enum
        private void Státus_feltöltés()
        {
            Ütem_státus.Items.Clear();
            Ütem_státus.Items.Add("0- Tervezési");
            Ütem_státus.Items.Add("2- Ütemezett");
            Ütem_státus.Items.Add("4- Előjegyzett");
            Ütem_státus.Items.Add("6- Elvégzett");
            Ütem_státus.Items.Add("8- Tervezésisegéd");
            Ütem_státus.Items.Add("9- Törölt");

            Ütem_Köv_Státus.Items.Clear();
            Ütem_Köv_Státus.Items.Add("0- Tervezési");
            Ütem_Köv_Státus.Items.Add("2- Ütemezett");
            Ütem_Köv_Státus.Items.Add("4- Előjegyzett");
            Ütem_Köv_Státus.Items.Add("6- Elvégzett");
            Ütem_Köv_Státus.Items.Add("8- Tervezésisegéd");
            Ütem_Köv_Státus.Items.Add("9- Törölt");
        }

        private void Ütem_Pályaszámokfeltöltése()
        {
            try
            {
                List<Adat_CAF_alap> Adatok = AlapKéz.Lista_Adatok(true);
                Ütem_pályaszám.Items.Clear();
                foreach (Adat_CAF_alap item in Adatok)
                    Ütem_pályaszám.Items.Add(item.Azonosító);
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

        private void Ütem_Ciklus_IDŐ_Sorszám_feltöltés()
        {
            try
            {
                Ütem_Ciklus_IDŐ.Text = EgyCAF.Ciklusnap;
                Ütem_vizsg_sorszám_idő.Items.Clear();
                Ciklus_Idő = Ciklus.Where(a => a.Típus == EgyCAF.Ciklusnap).OrderBy(a => a.Sorszám).ToList();

                foreach (Adat_Ciklus item in Ciklus_Idő)
                    Ütem_vizsg_sorszám_idő.Items.Add(item.Sorszám);
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

        private void Ütem_Ciklus_KM_Sorszám_feltöltés()
        {
            try
            {
                Ütem_Ciklus_KM.Text = EgyCAF.Cikluskm;
                Ütem_vizsg_sorszám_km.Items.Clear();
                Ciklus_Km = Ciklus.Where(a => a.Típus == EgyCAF.Cikluskm).OrderBy(a => a.Sorszám).ToList();

                foreach (Adat_Ciklus item in Ciklus_Km)
                    Ütem_vizsg_sorszám_km.Items.Add(item.Sorszám);
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
        #endregion


        #region Kiírások
        private void AdatokKiírása()
        {
            try
            {
                List<Adat_CAF_Adatok> Adatok = KézAdatok.Lista_Adatok();
                //kiírjuk azt a sorszámot, amire rá kattintottunk
                Adat_CAF_Adatok Adat = (from a in Adatok
                                        where a.Id == Posta_Segéd.Sorszám
                                        select a).FirstOrDefault();
                if (Adat != null) KiírEgyAdatot(Adat);

                //Kiírjuk azokat az adatokat ami megelőzte a kiválasztottat
                Adat = (from a in Adatok
                        where a.Azonosító.Trim() == Ütem_pályaszám.Text.Trim()
                        && a.Státus < 8 // nem  törölt
                        && a.Dátum < Ütem_Köv_Dátum.Value
                        orderby a.Dátum descending
                        select a).FirstOrDefault();
                if (Adat != null) KiírElőzőAdatot(Adat);

                Ütem_Köv_Számláló.ReadOnly = Ütem_Köv_Státus.SelectedItem.ToString() != "6- Elvégzett";

                if (Adat != null)
                {
                    if ((int.Parse(Ütem_számláló.Text) > int.Parse(Ütem_Köv_Számláló.Text) || int.Parse(Ütem_számláló.Text) == 0) && Ütem_státus.SelectedItem.ToString() == "6- Elvégzett")
                    {
                        Ütem_Köv_Számláló.BackColor = Color.LightPink;
                    }
                }                
                KiirPvizsgalat();
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

        private void KiírEgyAdatot(Adat_CAF_Adatok rekord)
        {
            try
            {
                if (rekord != null)
                {
                    Ütem_pályaszám.Text = rekord.Azonosító.Trim();
                    Ütem_köv_sorszám.Text = rekord.Id.ToString();
                    Ütem_Köv_Vizsgálat.Text = rekord.Vizsgálat;
                    Ütem_Köv_Dátum.Value = rekord.Dátum;
                    Ütem_Köv_Számláló.Text = rekord.Számláló.ToString();

                    for (int i = 0; i < Ütem_Köv_Státus.Items.Count; i++)
                    {
                        if (Ütem_Köv_Státus.Items[i].ToString().Contains(rekord.Státus.ToString()))
                        {
                            Ütem_Köv_Státus.Text = Ütem_Köv_Státus.Items[i].ToString();
                            break;
                        }
                    }

                    switch (rekord.IDŐvKM)
                    {
                        case 0:
                            {
                                Ütem_Köv_IDŐvKM.Text = "?";
                                break;
                            }
                        case 1:
                            {
                                Ütem_Köv_IDŐvKM.Text = "Idő";
                                break;
                            }
                        case 2:
                            {
                                Ütem_Köv_IDŐvKM.Text = "Km";
                                break;
                            }
                    }
                    Ütem_vizsg_sorszám_km.Text = rekord.KM_Sorszám.ToString();
                    Ütem_vizsg_sorszám_idő.Text = rekord.IDŐ_Sorszám.ToString();
                    Ütem_megjegyzés.Text = rekord.Megjegyzés.Trim();
                    Ütem_dátum_program.Value = rekord.Dátum_program;
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

        private void KiirPvizsgalat()
        {
            if (Ütem_pályaszám.Text.Trim() == "") return;

            Adat_CAF_KM_Attekintes teszt_adat = KézCafKm.Egy_Adat(Ütem_pályaszám.Text);

            if (teszt_adat != null)
            {
                // Eltárolom a szükséges értékeket egy változóba a Ciklusrend adb-ből, így nem kell minden egyes művelet során adb-ből olvasni őket.
                long nevlegesKmErtek = Kéz_Ciklus.Lista_Adatok().FirstOrDefault(a => a.Típus == "CAF_km").Névleges;
                long alsoKmErtek = Kéz_Ciklus.Lista_Adatok().FirstOrDefault(a => a.Típus == "CAF_km").Alsóérték;

                // Lekéri a Ciklusrend adatbázisból a vizsgálatok közötti megtehető km értékét.
                tb_ciklusrend.Text = $"{nevlegesKmErtek}";

                // Alább kiszámolja és visszaadja a ciklusrendben meghatározott tűréshatár értékét százalékos formában.
                // Így nem szükséges tárolnunk a tűréshatárt, de mégis módosítható.
                // A double castolásra szükség van, hiszen pl. 10 % alatt olyan kicsi számmal dolgozunk, amely már nem fér bele az INT típus értékkészletébe.
                // A következő képletet használja: (Névleges - Alsóérték) / (Névleges * 100)
                tb_tureshatar.Text = $"{(double)(nevlegesKmErtek - alsoKmErtek) / nevlegesKmErtek * 100}";

                // Utoljára teljesített vizsgálat sorszáma
                Adat_CAF_Adatok VizsgáltElem = KézAdatok.Utolso_Km_Vizsgalat_Adatai(Ütem_pályaszám.Text);
                if (VizsgáltElem != null)
                    tb_utolso_teljesitett.Text = $"({VizsgáltElem.KM_Sorszám}. {VizsgáltElem.KM_Sorszám * 14000} Km)";
                else
                    tb_utolso_teljesitett.Text = "Még nem történt.";

                // Előző vizsgálat tervezett állása
                tb_tervezetthez_kepest.Text = string.IsNullOrEmpty(teszt_adat.utolso_vizsgalat_valos_allasa?.ToString()) ? "Még nem történt." : $"{teszt_adat.utolso_vizsgalat_valos_allasa}";
                if (tb_tervezetthez_kepest.Text != "Még nem történt.")  //SzinezdTextBox(tb_tervezetthez_kepest, 0, -14000, true);

                    // P0
                    tb_futhatmeg_p0.Text = string.IsNullOrEmpty(teszt_adat.kov_p0?.ToString()) ? "Még nem történt." : $"{teszt_adat.kov_p0}";
                if (tb_futhatmeg_p0.Text != "Még nem történt.") SzinezdFuthatMeg(tb_futhatmeg_p0, (1 * nevlegesKmErtek) / tb_tureshatar.Text.ToÉrt_Int());

                // P1
                tb_futhatmeg_p1.Text = string.IsNullOrEmpty(teszt_adat.kov_p1?.ToString()) ? "Még nem történt." : $"{teszt_adat.kov_p1}";
                if (tb_futhatmeg_p1.Text != "Még nem történt.") SzinezdFuthatMeg(tb_futhatmeg_p1, (5 * nevlegesKmErtek) / tb_tureshatar.Text.ToÉrt_Int());

                // P2
                tb_futhatmeg_p2.Text = string.IsNullOrEmpty(teszt_adat.kov_p2?.ToString()) ? "Még nem történt." : $"{teszt_adat.kov_p2}";
                if (tb_futhatmeg_p2.Text != "Még nem történt.") SzinezdFuthatMeg(tb_futhatmeg_p2, (20 * nevlegesKmErtek) / tb_tureshatar.Text.ToÉrt_Int());


                // A SzinezdFuthatMeg a kovetkezokepp mukodik:
                // Az 1, 5, 20 konstans a P0, P1 és P2 vizsgálatot jelölik, és ezek a mezők "visszaszamlalnak" a névértékben meghatározott Km és Vizsgalat alapján, hogy mennyit futhat meg a villamos.
                // Pl. 14.000 a neveleges vizsgalati Km eseten a visszaszamlalas innen indul (P0: 14.000*1=14.000, P1: 14.000*5=70.000, P2: 14.000*20=280.000).
                // A metodusnak eleg csak azt atadni, hogy miutan adjon pirosat, hiszen a visszaszamlalas miatt 0 felett mindig zold
                // Amikor eleri az atadott Piros hatart akkor piros, egyebkent pedig sarga.
                // Azert adom at tureshatarral, hiszen a meghatarozott tureshatart atlepve kell pirossa valtoznia.

                // Megtett P0
                tb_megtett_p0.Text = string.IsNullOrEmpty(teszt_adat.utolso_p0_kozott?.ToString()) ? "Még nem történt." : $"{teszt_adat.utolso_p0_kozott}";
                if (tb_megtett_p0.Text != "Még nem történt.") SzinezdTextBox(tb_megtett_p0, teszt_adat.utolso_p0_sorszam, nevlegesKmErtek, tb_tureshatar.Text.ToÉrt_Int());

                // Megtett P1
                tb_megtett_p1.Text = string.IsNullOrEmpty(teszt_adat.utolso_p1_kozott?.ToString()) ? "Még nem történt." : $"{teszt_adat.utolso_p1_kozott}";
                if (tb_megtett_p1.Text != "Még nem történt.") SzinezdTextBox(tb_megtett_p1, teszt_adat.utolso_p1_sorszam, nevlegesKmErtek, tb_tureshatar.Text.ToÉrt_Int());

                // P2 rendben
                tb_rendben_p2.Text = string.IsNullOrEmpty(teszt_adat.elso_p2?.ToString()) ? "Még nem történt." : $"{teszt_adat.elso_p2}";
                if (tb_rendben_p2.Text != "Még nem történt.") SzinezdTextBox(tb_rendben_p2, 20, nevlegesKmErtek, tb_tureshatar.Text.ToÉrt_Int());

                // P3 rendben
                tb_rendben_p3.Text = string.IsNullOrEmpty(teszt_adat.elso_p3?.ToString()) ? "Még nem történt." : $"{teszt_adat.elso_p3}";
                if (tb_rendben_p3.Text != "Még nem történt.") SzinezdTextBox(tb_rendben_p3, 40, nevlegesKmErtek, tb_tureshatar.Text.ToÉrt_Int());

                // P3–P2 közötti futás
                tb_p3_p2_kozott.Text = string.IsNullOrEmpty(teszt_adat.utolso_p3_es_p2_kozott?.ToString()) ? "Még nem történt." : $"{teszt_adat.utolso_p3_es_p2_kozott}";
                if (tb_p3_p2_kozott.Text != "Még nem történt.") SzinezdTextBox(tb_p3_p2_kozott, 20, nevlegesKmErtek, tb_tureshatar.Text.ToÉrt_Int());

                // A SzinzedTextBox a kovetkezokepp mukodik:
                // Megkapja az utolso elvegzett Km sorszamat a 2 azonos vizsgalat kozotti mezoknel, illetve az elso vizsgalatos mezoknel az elso vizsgalat sorszamat.
                // Ezekbol kiszamolja a nevleges Km es tureshatar segitsegevel a szinezesi hatarokat.
            }
            else
            {
                tb_futhatmeg_p0.Text = "Nincs adat";
                tb_futhatmeg_p1.Text = "Nincs adat";
                tb_futhatmeg_p2.Text = "Nincs adat";
                tb_megtett_p0.Text = "Nincs adat";
                tb_megtett_p1.Text = "Nincs adat";
                tb_rendben_p2.Text = "Nincs adat";
                tb_rendben_p3.Text = "Nincs adat";
                tb_p3_p2_kozott.Text = "Nincs adat";
            }





        }

        // Ez a 2 vizsgalat kozotti, illetve az elso vizsgalatok szinezeset vegzi.
        private void SzinezdTextBox(TextBox tb, long? vizsgalatiSorszam, long nevlegesKmErtek, int tureshatar)
        {
            if (int.TryParse(tb.Text, out int ertek))
            {
                long? alap = vizsgalatiSorszam * nevlegesKmErtek;
                double? sargaHatar = alap * (1 + tureshatar / 100.0);

                if (ertek <= alap)
                    tb.BackColor = Color.LightGreen;
                else if (ertek <= sargaHatar)
                    tb.BackColor = Color.PaleGoldenrod;
                else
                    tb.BackColor = Color.LightCoral;

                tb.Text += " Km";
            }
            else
            {
                tb.BackColor = SystemColors.Window;
            }
        }


        // Ez a "visszaszamlalos" mezok szinezeset vegzi.
        void SzinezdFuthatMeg(TextBox tb, long pirosHatar)
        {
            if (int.TryParse(tb.Text, out int ertek))
            {
                if (ertek < -pirosHatar)
                {
                    tb.BackColor = ControlPaint.Light(Color.Red);
                }
                else if (ertek < 0)
                {
                    tb.BackColor = Color.PaleGoldenrod;
                }
                else
                {
                    tb.BackColor = Color.LightGreen;
                }
                tb.Text += " Km";
            }
            else
            {
                tb.BackColor = SystemColors.Window;
            }
        }

        private void KiírJobbOldal()
        {
            try
            {
                KM_ciklus_kiirás();
                int KM_felső = int.TryParse(Ütem_KM_felső.Text, out KM_felső) ? KM_felső : 0;
                Ütem_számláló_KM.Text = EgyCAF.Számláló.ToString();
                Ütem_Utolsó_futott.Text = (EgyCAF.KMUkm - EgyCAF.Számláló).ToString();
                Ütem_km_KMU.Text = EgyCAF.KMUkm.ToString();
                Ütem_havifutás.Text = EgyCAF.Havikm.ToString();
                Ütem_Napkm.Text = ((int)(EgyCAF.Havikm / 30)).ToString();
                // Kérdés: Itt ha nincs adat 0-val való osztás hibát dob. Kapjon egy saját catch-et, vagy hagyjuk, mivel papíron nem kellene olyannak lennie,
                // hogy nincs havi km, kivéve ha törött/selejtes.
                if (EgyCAF.Havikm != 0)
                    Ütem_KM_futhatmég.Text = ((KM_felső - (EgyCAF.KMUkm - EgyCAF.Számláló)) / ((int)(EgyCAF.Havikm / 30))).ToString();
                else
                    Ütem_KM_futhatmég.Text = "Nincs havi futás.";
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "KiírJobbOldal", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ütem_mezők_ürítése()
        {
            Ütem_köv_sorszám.Text = "";
            Ütem_Köv_Vizsgálat.Text = "";
            Ütem_Köv_Dátum.Value = new DateTime(1900, 1, 1);
            Ütem_dátum_program.Value = new DateTime(1900, 1, 1);
            Ütem_Köv_Számláló.Text = "";
            Ütem_Köv_Státus.Text = "";
            Ütem_vizsg_sorszám_km.Text = "";
            Ütem_vizsg_sorszám_idő.Text = "";
            Ütem_Köv_IDŐvKM.Text = "";
        }

        private void KiírElőzőAdatot(Adat_CAF_Adatok rekord)
        {
            try
            {
                if (rekord != null)
                {
                    Ütem_sorszám.Text = rekord.Id.ToString();
                    Ütem_vizsgálat.Text = rekord.Vizsgálat;
                    Ütem_dátum.Value = rekord.Dátum;
                    Ütem_számláló.Text = rekord.Számláló.ToString();
                    Ütem_státus.Text = rekord.Státus.ToString();

                    for (int i = 0; i < Ütem_státus.Items.Count; i++)
                    {
                        if (Ütem_státus.Items[i].ToString().Contains(rekord.Státus.ToString()))
                            Ütem_státus.Text = Ütem_státus.Items[i].ToString();
                    }

                    Ütem_Km_sorszám.Text = rekord.KM_Sorszám.ToString();
                    Ütem_idő_sorszám.Text = rekord.IDŐ_Sorszám.ToString();
                    switch (rekord.IDŐvKM)
                    {
                        case 0:
                            {
                                Ütem_IDŐvKM.Text = "?";
                                break;
                            }
                        case 1:
                            {
                                Ütem_IDŐvKM.Text = "Idő";
                                break;
                            }
                        case 2:
                            {
                                Ütem_IDŐvKM.Text = "Km";
                                break;
                            }
                    }
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

        private void AdatokKeresés()
        {
            try
            {
                Adat_CAF_Adatok Adat = KézAdatok.Egy_Adat_Spec(Posta_Segéd.Azonosító, Posta_Segéd.Dátum);
                KiírEgyAdatot(Adat);
                KiírElőzőAdatot(Adat);
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

        private void Ütem_pályaszám_SelectionChangeCommitted(object sender, EventArgs e)
        {

            if (Ütem_pályaszám.Text.Trim() != "")
            {
                Ütem_mezők_ürítése();

                Adat_CAF_Adatok Adat = KézAdatok.Egy_Adat(Ütem_pályaszám.Items[Ütem_pályaszám.SelectedIndex].ToStrTrim());
                KiírEgyAdatot(Adat);

                Adat = KézAdatok.Egy_Adat_Id_Előző(Posta_Segéd.Azonosító.Trim(), Adat.Id);
                KiírElőzőAdatot(Adat);

                KiírJobbOldal();
            }
        }

        private void Ütem_vizsg_sorszám_idő_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Ütem_Ciklus_IDŐ.Text.Trim() == "") return;
            if (Ütem_vizsg_sorszám_idő.Text.Trim() == "") return;

            Adat_Ciklus Adat = (from a in Ciklus
                                where a.Törölt == "0"
                                && a.Sorszám == Ütem_vizsg_sorszám_idő.Items[Ütem_vizsg_sorszám_idő.SelectedIndex].ToÉrt_Long()
                                && a.Típus == Ütem_Ciklus_IDŐ.Text.Trim()
                                select a).FirstOrDefault();

            if (Adat != null)
            {
                Ütem_vizsgálat_IDŐ.Text = Adat.Vizsgálatfok;
                Ütem_névleges_nap.Text = Adat.Névleges.ToString();
                Ütem_Köv_Vizsgálat.Text = Ütem_vizsgálat_IDŐ.Text;
            }
        }

        private void Ütem_vizsg_sorszám_km_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Ütem_Ciklus_KM.Text.Trim() == "") return;
            if (Ütem_vizsg_sorszám_km.Text.Trim() == "") return;

            Adat_Ciklus Adat = (from a in Ciklus
                                where a.Törölt == "0"
                                && a.Sorszám == Ütem_vizsg_sorszám_km.Items[Ütem_vizsg_sorszám_km.SelectedIndex].ToÉrt_Long()
                                && a.Típus == Ütem_Ciklus_KM.Text.Trim()
                                select a).FirstOrDefault();

            if (Adat != null)
            {
                Ütem_vizsgálat_KM.Text = Adat.Vizsgálatfok;
                Ütem_KM_alsó.Text = Adat.Alsóérték.ToString();
                Ütem_KM_felső.Text = Adat.Felsőérték.ToString();
                Ütem_KM_névleges.Text = Adat.Névleges.ToString();
                Ütem_Köv_Vizsgálat.Text = Ütem_vizsgálat_KM.Text;
            }
        }

        private void Ütem_Köv_Státus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (Ütem_Köv_Státus.SelectedItem.ToString() == "6- Elvégzett")
            {
                Ütem_Köv_Számláló.ReadOnly = false;
            }
            else
            {
                Ütem_Köv_Számláló.ReadOnly = true;
            }
        }

        private void KM_ciklus_kiirás()
        {
            try
            {
                if (Ütem_vizsg_sorszám_km.Text.Trim() == "") return;
                Adat_Ciklus Adat = (from a in Ciklus
                                    where a.Törölt == "0"
                                    && a.Sorszám == Ütem_vizsg_sorszám_km.Text.ToÉrt_Long()
                                    && a.Típus == Ütem_Ciklus_KM.Text.Trim()
                                    select a).FirstOrDefault();

                if (Adat != null)
                {
                    Ütem_vizsgálat_KM.Text = Adat.Vizsgálatfok;
                    Ütem_KM_alsó.Text = Adat.Alsóérték.ToString();
                    Ütem_KM_felső.Text = Adat.Felsőérték.ToString();
                    Ütem_KM_névleges.Text = Adat.Névleges.ToString();
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
        #endregion


        #region KmUgrás
        private void KM_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ütem_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a pályaszám mező.");
                //Jármű tulajdonsága
                EgyCAF = AlapKéz.Egy_Adat(Ütem_pályaszám.Text.Trim());

                // utolsó ütemezett
                Adat_CAF_Adatok Előző = KézAdatok.Egy_Adat(Ütem_pályaszám.Text.Trim(), 2);
                KiírElőzőAdatot(Előző);

                // következő km szerinti
                Adat_CAF_Adatok Adat = MyCaf.Következő_Km(Ciklus_Km, Előző, EgyCAF);
                KiírEgyAdatot(Adat);

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
        #endregion


        #region Ütemezés
        private void Ütem_átütemezés_Click(object sender, EventArgs e)
        {
            if (Ütem_köv_sorszám.Text.Trim() == "") throw new HibásBevittAdat("Az elemet nem lehet ütemezni.");
            if (!double.TryParse(Ütem_köv_sorszám.Text.Trim(), out double Sorszám)) throw new HibásBevittAdat("Az elemet nem lehet ütemezni.");

            List<Adat_CAF_Adatok> Adatok = KézAdatok.Lista_Adatok();

            Adat_CAF_Adatok rekord = (from a in Adatok
                                      where a.Id == Sorszám
                                      select a).FirstOrDefault();

            switch (rekord.IDŐvKM)
            {
                case 1:
                    Idő_átütemezés();
                    break;
                case 2:
                    Km_átütemezés();
                    break;
            }

            Változás?.Invoke();
            MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Idő_átütemezés()
        {
            try
            {
                if (Ütem_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a pályaszám mező.");
                if (Ütem_Köv_Vizsgálat.Text.Trim() == "") throw new HibásBevittAdat("Nincs meghatározva, hogy milyen vizsgálat lesz a következő");
                if (!int.TryParse(Ütem_vizsg_sorszám_km.Text.Trim(), out int Kmsorszám)) throw new HibásBevittAdat("Nincs kitöltve a km sorszám.");
                if (!int.TryParse(Ütem_vizsg_sorszám_idő.Text.Trim(), out int Idősorszám)) throw new HibásBevittAdat("Nincs kitöltve az idő sorszám.");
                if (Ütem_köv_sorszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve érvényes adat.");
                if (!double.TryParse(Ütem_köv_sorszám.Text, out double ÜtemKövSorszám)) throw new HibásBevittAdat("Nem érvényes a sorszám.");
                if (!int.TryParse(MyF.Szöveg_Tisztítás(Ütem_Köv_Státus.Text, 0, 1), out int státus)) throw new HibásBevittAdat("Nincs kitöltve a státus.");
                if (Ütem_megjegyzés.Text.Trim() == "") Ütem_megjegyzés.Text = "_";
                Ütem_megjegyzés.Text = MyF.Szöveg_Tisztítás(Ütem_megjegyzés.Text, 0, 254);
                if (!long.TryParse(Ütem_Köv_Számláló.Text.Trim(), out long számláló)) számláló = 0;

                List<Adat_CAF_Adatok> Adatok = KézAdatok.Lista_Adatok();
                Adat_CAF_Adatok Adat = (from a in Adatok
                                        where a.Id == ÜtemKövSorszám
                                        select a).FirstOrDefault();
                // ha nem raktuk át másik napra akkor kilépünk
                if (Adat.Dátum == Ütem_Köv_Dátum.Value) throw new HibásBevittAdat("Nem történt meg az átütemezés");

                if (Adat != null)
                {
                    // rögzítjük az új dátumra az adatot
                    Adat_CAF_Adatok ADAT = new Adat_CAF_Adatok(
                           ÜtemKövSorszám,
                           Ütem_pályaszám.Text.Trim(),
                           Ütem_Köv_Vizsgálat.Text.Trim(),
                           Adat.Dátum, //Külön küldöm
                           Adat.Dátum_program,
                           számláló,
                           státus,
                           Kmsorszám,
                           Idősorszám,
                           Ütem_Köv_IDŐvKM.Text.Trim() == "Idő" ? 1 : 2,
                           Ütem_megjegyzés.Text.Trim());


                    MyCaf.Idő_átütemezés(Adatok, ADAT, Ütem_Köv_Dátum.Value, Elő_Dátumig);
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

        private void Km_átütemezés()
        {
            try
            {
                // töröltre állítjuk az aktuális sorszámot
                if (Ütem_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a pályaszám mező.");
                if (Ütem_Köv_Vizsgálat.Text.Trim() == "") throw new HibásBevittAdat("Nincs meghatározva, hogy milyen vizsgálat lesz a következő");
                if (!int.TryParse(MyF.Szöveg_Tisztítás(Ütem_Köv_Státus.Text, 0, 1), out int státus))
                {
                    státus = 0;
                    Ütem_Köv_Státus.Text = "0";
                }
                if (!int.TryParse(Ütem_vizsg_sorszám_km.Text.Trim(), out int Kmsorszám)) throw new HibásBevittAdat("Nincs kitöltve a km sorszám.");
                if (!int.TryParse(Ütem_vizsg_sorszám_idő.Text.Trim(), out int Idősorszám)) throw new HibásBevittAdat("Nincs kitöltve az idő sorszám.");
                if (Ütem_köv_sorszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve érvényes adat.");
                if (!int.TryParse(Ütem_köv_sorszám.Text, out int ÜtemKövSorszám)) throw new HibásBevittAdat("Nem érvényes a sorszám.");
                if (!long.TryParse(Ütem_Köv_Számláló.Text.Trim(), out long számláló)) számláló = 0;
                if (Ütem_megjegyzés.Text.Trim() == "") Ütem_megjegyzés.Text = "_";
                Ütem_megjegyzés.Text = MyF.Szöveg_Tisztítás(Ütem_megjegyzés.Text, 0, 254);

                List<Adat_CAF_Adatok> Adatok = KézAdatok.Lista_Adatok();
                Adat_CAF_Adatok Adat = (from a in Adatok
                                        where a.Id == ÜtemKövSorszám
                                        select a).FirstOrDefault();

                if (Adat.Dátum != Ütem_Köv_Dátum.Value)
                {
                    if (Adat != null)
                    {
                        Adat_CAF_Adatok ADAT = new Adat_CAF_Adatok(
                                          ÜtemKövSorszám,
                                          Ütem_pályaszám.Text.Trim(),
                                          Ütem_Köv_Vizsgálat.Text.Trim(),
                                          Adat.Dátum, //Külön lesz küldve
                                          Ütem_dátum_program.Value,
                                          számláló,
                                          státus,
                                          Kmsorszám,
                                          Idősorszám,
                                          Ütem_Köv_IDŐvKM.Text.Trim() == "Idő" ? 1 : 2,
                                          Ütem_megjegyzés.Text.Trim());
                        MyCaf.Km_átütemezés(Adatok, ADAT, Ütem_Köv_Dátum.Value, Elő_Dátumig);
                    }
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
        #endregion


        #region Rögzítés
        private void Ütem_Rögzít_Click(object sender, EventArgs e)
        {
            Rögzíti_ütemet();

        }

        private void Rögzíti_ütemet()
        {
            try
            {
                if (!long.TryParse(Ütem_Köv_Számláló.Text.Trim(), out long számláló)) számláló = 0;
                if (!long.TryParse(Ütem_számláló.Text.Trim(), out long eszámláló)) eszámláló = 0;
                if (Ütem_Köv_Státus.Text.Substring(0, 1) == "6" && eszámláló > számláló)
                    throw new HibásBevittAdat($"Az adatok rögzítése sikertelen!\nAz új számláló állása {számláló}km kevesebb,\n mint az előző {eszámláló}km !");

                if (Ütem_pályaszám.Text.Trim() == "") return;
                if (Ütem_Köv_Vizsgálat.Text.Trim() == "") return;
                if (!int.TryParse(MyF.Szöveg_Tisztítás(Ütem_Köv_Státus.Text, 0, 1), out int státus)) return;
                if (!int.TryParse(Ütem_vizsg_sorszám_km.Text.Trim(), out int Kmsorszám)) return;
                if (!int.TryParse(Ütem_vizsg_sorszám_idő.Text.Trim(), out int Idősorszám)) return;
                if (Ütem_megjegyzés.Text.Trim() == "") Ütem_megjegyzés.Text = "_";

                if (!double.TryParse(Ütem_köv_sorszám.Text, out double ID)) ID = 0;
                Ütem_megjegyzés.Text = MyF.Szöveg_Tisztítás(Ütem_megjegyzés.Text, 0, 254);

                Adat_CAF_Adatok ADAT = new Adat_CAF_Adatok(
                                   ID,
                                   Ütem_pályaszám.Text.Trim(),
                                   Ütem_Köv_Vizsgálat.Text.Trim(),
                                   Ütem_Köv_Dátum.Value,
                                   Ütem_dátum_program.Value,
                                   számláló,
                                   státus,
                                   Kmsorszám,
                                   Idősorszám,
                                   Ütem_Köv_IDŐvKM.Text.Trim() == "Idő" ? 1 : 2,
                                   Ütem_megjegyzés.Text.Trim());
                KézAdatok.Döntés(ADAT);
                Változás?.Invoke();
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
        #endregion


        #region IdőUgrás
        private void IDŐ_Click(object sender, EventArgs e)
        {
            try
            {
                if (Ütem_pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a pályaszám mező.");

                //Jármű tulajdonsága
                EgyCAF = AlapKéz.Egy_Adat(Ütem_pályaszám.Text.Trim());
                // utolsó ütemezett
                Adat_CAF_Adatok Előző = KézAdatok.Egy_Adat(Ütem_pályaszám.Text.Trim());
                if (Előző != null) KiírElőzőAdatot(Előző);

                // következő idő szerinti
                Adat_CAF_Adatok Adat = MyCaf.Következő_Idő(Ciklus_Idő, Előző, EgyCAF);
                KiírEgyAdatot(Adat);

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
        #endregion

        private void Btn_frissit_Click(object sender, EventArgs e)
        {
            try
            {
                //KézCafKm.Tabla_Feltoltese();
                KézCafKm.Erteket_Frissit_Egyeni(Posta_Segéd.Azonosító);
                KiirPvizsgalat();
                MessageBox.Show("Sikeres frissítés!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        //Javítás :élesítés után törlendő
        private void Btn_elso_futtatas_Click(object sender, EventArgs e)
        {
            try
            {
                Kezelő_CAF_KM_Attekintes.InitializeCache(KézAdatok);
                KézCafKm.Tabla_Feltoltese();
                MessageBox.Show("Sikeres feltöltés!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Tb_ciklusrend_modosit_Click(object sender, EventArgs e)
        {
            try
            {
                // Elmenti a ciklusrend adatbázisba a módosított értékeket..
                Kéz_Ciklus.Módosítás_CAF(long.Parse(tb_ciklusrend.Text), int.Parse(tb_tureshatar.Text));
                MessageBox.Show("Sikeres módosítás!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
