﻿using System;
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
    public partial class Ablak_CAF_Részletes_WIP : Form
    {
        public event Event_Kidobó Változás;
        public CAF_Segéd_Adat Posta_Segéd { get; private set; }
        public DateTime Elő_Dátumig { get; private set; }

        readonly Kezelő_CAF_Adatok KézAdatok = new Kezelő_CAF_Adatok();
        readonly Kezelő_CAF_alap AlapKéz = new Kezelő_CAF_alap();
        readonly Kezelő_Ciklus Kéz_Ciklus = new Kezelő_Ciklus();

        Adat_CAF_alap EgyCAF;

        List<Adat_Ciklus> Ciklus = new List<Adat_Ciklus>();
        List<Adat_Ciklus> Ciklus_Idő = null;
        List<Adat_Ciklus> Ciklus_Km = null;

        #region Alap
        public Ablak_CAF_Részletes_WIP(CAF_Segéd_Adat posta_Segéd, DateTime elő_Dátumig)
        {
            InitializeComponent();

            Posta_Segéd = posta_Segéd;
            Elő_Dátumig = elő_Dátumig;
            Start();

        }

        private void Ablak_CAF_Részletes_Load(object sender, EventArgs e)
        {
            Jogosultságkiosztás();
        }

        private void Ablak_CAF_Részletes_FormClosed(object sender, FormClosedEventArgs e)
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
        }

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

                if ((int.Parse(Ütem_számláló.Text) > int.Parse(Ütem_Köv_Számláló.Text) || int.Parse(Ütem_számláló.Text) == 0) && Ütem_státus.SelectedItem.ToString() == "6- Elvégzett")
                {
                    Ütem_Köv_Számláló.BackColor = Color.LightPink;
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

                    /// P0: határ -1400
                    tb_futhatmeg_p0.Text = $"{Kovetkezo_P0_Vizsgalat_KM_Erteke(rekord.Azonosító)}";
                    SzinezdFuthatMeg(tb_futhatmeg_p0, 1400);

                    // P1: határ -7000
                    tb_futhatmeg_p1.Text = $"{Kovetkezo_P1_Vizsgalat_KM_Erteke(rekord.Azonosító)}";
                    SzinezdFuthatMeg(tb_futhatmeg_p1, 7000);

                    // P2: határ -28000
                    tb_futhatmeg_p2.Text = $"{Kovetkezo_P2_Vizsgalat_KM_Erteke(rekord.Azonosító)}";
                    SzinezdFuthatMeg(tb_futhatmeg_p2, 28000);


                    // Megtett P0
                    tb_megtett_p0.Text = $"{P0_vizsgalatok_kozott_megtett_KM_Erteke(rekord.Azonosító)}";
                    SzinezdTextBox(tb_megtett_p0, 14000, 15400);

                    // Megtett P1
                    tb_megtett_p1.Text = $"{P1_vizsgalatok_kozott_megtett_KM_Erteke(rekord.Azonosító)}";
                    SzinezdTextBox(tb_megtett_p1, 70000, 77000);

                    // P2 rendben
                    tb_rendben_p2.Text = $"{Elso_P2_rendben_van_e(rekord.Azonosító)}";
                    SzinezdTextBox(tb_rendben_p2, 280000, 308000);

                    // P3 rendben
                    tb_rendben_p3.Text = $"{Elso_P3_rendben_van_e(rekord.Azonosító)}";
                    SzinezdTextBox(tb_rendben_p3, 560000, 616000);

                    // P3–P2 közötti futás
                    tb_p3_p2_kozott.Text = $"{Utolso_P3_es_P2_kozotti_futas(rekord.Azonosító)}";
                    SzinezdTextBox(tb_p3_p2_kozott, 280000, 308000);
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

        private void SzinezdTextBox(TextBox tb, int alsoHatar, int felsoHatar)
        {
            if (int.TryParse(tb.Text, out int ertek))
            {
                if (ertek <= alsoHatar)
                {
                    tb.BackColor = Color.LightGreen;
                }
                else if (ertek <= felsoHatar)
                {
                    tb.BackColor = Color.PaleGoldenrod;
                }
                else
                {
                    tb.BackColor = ControlPaint.Light(Color.Red);
                }
                tb.Text = tb.Text + " Km";
            }
            else
            {
                tb.BackColor = SystemColors.Window;
            }
        }

        void SzinezdFuthatMeg(TextBox tb, int pirosHatar)
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
                tb.Text = tb.Text + " Km";
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
                Ütem_KM_futhatmég.Text = ((KM_felső - (EgyCAF.KMUkm - EgyCAF.Számláló)) / ((int)(EgyCAF.Havikm / 30))).ToString();
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


        #region KM vizsgálatok
        // JAVÍTANDÓ: a ciklusrendben van meghatározva, hogy mennyi km van egy vizsgálat között.
        //Tételezzük fel, hogy változik az üzemeltetési tapasztalatok alapján, vagy gazdasági okokból a 14 helyett 15 lesz a következő vizsgálat.
        //Akkor valakinek tudnia kell programozni, hogy ezt átírja. 
        //Próbálj általánosítani.
        const int Vizsgalatok_Kozott_Megteheto_Km = 14000;

        // Visszaadja az összes ADB-t összefűzve. Muszáj végigmenni rajtuk. Sebességben picit jobb, mintha egyesével beolvassa őket.
        // Ezt kiszervezem a kezelőbe.
        private IEnumerable<Adat_CAF_Adatok> ÖsszesCAFAdat()
        {
            return KézAdatok.Lista_Adatok()
                .Concat(Enumerable.Range(2016, DateTime.Now.Year - 2016 + 1)
                    .SelectMany(ev => KézAdatok.ElőzőÉvek(ev)));
        }

        // Itt a metódusokban lévő utolsó KM kivételeket egységesíteni kell.
        private long Kovetkezo_P0_Vizsgalat_KM_Erteke(string Aktualis_palyaszam)
        {
            // Kiveszi az utolsó teljesített km alapú vizsgálatot.
            Adat_CAF_Adatok Adott_Villamos = KézAdatok.Lista_Adatok()
                                                       .Where(a => a.IDŐvKM == 2 && a.Státus == 6 && a.Azonosító == Aktualis_palyaszam)
                                                       .OrderByDescending(a => a.Dátum)
                                                       .First();
            // Visszaadja a következő P vizsgálat KM várt értékét.

            return ((Adott_Villamos.KM_Sorszám + 1) * Vizsgalatok_Kozott_Megteheto_Km) - Utolso_KM_Vizsgalat_Erteke(Aktualis_palyaszam);

        }

        // Ez már benne van a kezelőben félig meddig, overload-olva beleteszem ezt a verziót is később
        private long Utolso_KM_Vizsgalat_Erteke(string Aktualis_palyaszam)
        {
            // Kiveszi az utolsó teljesített km alapú vizsgálatot.
            Adat_CAF_Adatok Adott_Villamos = KézAdatok.Lista_Adatok()
                                                       .Where(a => a.IDŐvKM == 2 && a.Státus == 6 && a.Azonosító == Aktualis_palyaszam)
                                                       .OrderByDescending(a => a.Dátum)
                                                       .First();
            // Visszaadja a következő sorszámú vizsgálat KM várt értékét.
            return Adott_Villamos.Számláló;

        }

        private long Kovetkezo_P1_Vizsgalat_KM_Erteke(string Aktualis_palyaszam)
        {
            // Kiveszi az utolsó teljesített km alapú vizsgálatot.
            Adat_CAF_Adatok Adott_Villamos = KézAdatok.Lista_Adatok()
                                                       .Where(a => a.IDŐvKM == 2 && a.Státus == 6 && a.Azonosító == Aktualis_palyaszam)
                                                       .OrderByDescending(a => a.Dátum)
                                                       .First();
            // Ha 5-el osztható, de 20-al nem, akkor P1 vizsgálat.
            for (int i = Adott_Villamos.KM_Sorszám; i < 80; i++)
            {
                if (i % 5 == 0 && i % 20 != 0)
                {
                    return (i * Vizsgalatok_Kozott_Megteheto_Km) - Utolso_KM_Vizsgalat_Erteke(Aktualis_palyaszam);
                }
            }
            return 0;
        }

        private long Kovetkezo_P2_Vizsgalat_KM_Erteke(string Aktualis_palyaszam)
        {
            // Kiveszi az utolsó teljesített km alapú vizsgálatot.
            Adat_CAF_Adatok Adott_Villamos = KézAdatok.Lista_Adatok()
                                                       .Where(a => a.IDŐvKM == 2 && a.Státus == 6 && a.Azonosító == Aktualis_palyaszam)
                                                       .OrderByDescending(a => a.Dátum)
                                                       .First();
            // Ha csak 20-al osztható, akkor P2/P3 vizsgálat.
            for (int i = Adott_Villamos.KM_Sorszám; i < 80; i++)
            {
                if (i % 20 == 0)
                {
                    return (i * Vizsgalatok_Kozott_Megteheto_Km) - Utolso_KM_Vizsgalat_Erteke(Aktualis_palyaszam);
                }
            }
            return 0;
        }

        private long P0_vizsgalatok_kozott_megtett_KM_Erteke(string Aktualis_palyaszam)
        {

            // P0: nem osztható 5-tel
            var p0Vizsgalatok = ÖsszesCAFAdat()
                .Where(a => a.IDŐvKM == 2 &&
                            a.Státus == 6 &&
                            a.Azonosító == Aktualis_palyaszam &&
                            a.KM_Sorszám % 5 != 0)
                .OrderByDescending(a => a.Dátum)
                .Take(2)
                .ToList();

            if (p0Vizsgalatok.Count < 2)
                return 0;

            return p0Vizsgalatok[0].Számláló - p0Vizsgalatok[1].Számláló;
        }

        private long P1_vizsgalatok_kozott_megtett_KM_Erteke(string Aktualis_palyaszam)
        {
            // P1: osztható 5-tel, de nem 20-szal
            var p1Vizsgalatok = ÖsszesCAFAdat()
                .Where(a => a.IDŐvKM == 2 &&
                            a.Státus == 6 &&
                            a.Azonosító == Aktualis_palyaszam &&
                            a.KM_Sorszám % 5 == 0 &&
                            a.KM_Sorszám % 20 != 0)
                .OrderByDescending(a => a.Dátum)
                .Take(2)
                .ToList();

            if (p1Vizsgalatok.Count < 2)
                return 0;

            return p1Vizsgalatok[0].Számláló - p1Vizsgalatok[1].Számláló;
        }

        private string Utolso_P3_es_P2_kozotti_futas(string Aktualis_palyaszam)
        {
            // Utolsó P3 keresése
            Adat_CAF_Adatok P3 = ÖsszesCAFAdat()
                .Where(a => a.IDŐvKM == 2 &&
                            a.Státus == 6 &&
                            a.Azonosító == Aktualis_palyaszam &&
                            (a.Vizsgálat == "P3/2P2" || a.Vizsgálat == "2P3") &&
                            a.Megjegyzés != "Ütemezési Segéd")
                .OrderByDescending(a => a.Dátum)
                .FirstOrDefault();

            // Ha nem találunk P3 vizsgálatot
            if (P3 == null)
                return "Nem történt P3";

            // Utolsó P2 keresése
            Adat_CAF_Adatok P2 = ÖsszesCAFAdat()
                .Where(a => a.IDŐvKM == 2 &&
                            a.Státus == 6 &&
                            a.Azonosító == Aktualis_palyaszam &&
                            (a.Vizsgálat == "P2" || a.Vizsgálat == "3P2") &&
                            a.Megjegyzés != "Ütemezési Segéd")
                .OrderByDescending(a => a.Dátum)
                .FirstOrDefault();

            // Ha nem találunk P2 vizsgálatot.
            if (P2 == null)
                return "Nem történt P2";

            return $"{P3.Számláló - P2.Számláló}";
        }

        // A 2 alábbi metódus nem fog minden megnyíláskor lefutni, kapni fog ADB-ben 3 mezőt és verziócserekor lefuttatjuk őket.
        // A jövőben az új villamosok miatt figyelni kell majd, hogy ezek a mezők csak 1x frissülhetnek a villamos élete során, tehát amikor a 20. és 40. vizsgálat megvolt.
        private string Elso_P2_rendben_van_e(string Aktualis_palyaszam)
        {
            // Megkeresem az első P2 vizsgálatot.
            Adat_CAF_Adatok elsoP2 = ÖsszesCAFAdat()
                .Where(a => a.IDŐvKM == 2 &&
                            a.Státus == 6 &&
                            a.Azonosító == Aktualis_palyaszam &&
                            (a.Vizsgálat == "P2" || a.Vizsgálat == "3P2") &&
                            a.Megjegyzés != "Ütemezési Segéd")
                .OrderBy(a => a.Dátum)
                .FirstOrDefault();

            return elsoP2 != null
                ? $"{elsoP2.Számláló}"
                : "Nem történt P2.";
        }

        private string Elso_P3_rendben_van_e(string Aktualis_palyaszam)
        {
            // Megkeresem az első P3 vizsgálatot
            Adat_CAF_Adatok elsoP3 = ÖsszesCAFAdat()
                .Where(a => a.IDŐvKM == 2 &&
                            a.Státus == 6 &&
                            a.Azonosító == Aktualis_palyaszam &&
                            (a.Vizsgálat == "P3/2P2" || a.Vizsgálat == "2P3") &&
                            a.Megjegyzés != "Ütemezési Segéd")
                .OrderBy(a => a.Dátum)
                .FirstOrDefault();

            return elsoP3 != null
                ? $"{elsoP3.Számláló}"
                : "Nem történt P3.";
        }



        #endregion
    }
}
