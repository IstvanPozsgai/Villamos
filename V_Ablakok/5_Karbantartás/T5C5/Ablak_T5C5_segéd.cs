using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_T5C5_Segéd : Form
    {
        public event Event_Kidobó Változás;

        public List<Adat_T5C5_Posta> PostaAdat { get; private set; }
        public string Honnan { get; private set; }
        public bool Terv { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Cmbtelephely { get; private set; }

        readonly Kezelő_Vezénylés KézVezény = new Kezelő_Vezénylés();
        readonly Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();
        readonly Kezelő_Jármű KézÁllomány = new Kezelő_Jármű();
        readonly Kezelő_Hétvége_Beosztás KézHétBeosztás = new Kezelő_Hétvége_Beosztás();
        readonly Kezelő_Hétvége_Előírás KézHétElőírás = new Kezelő_Hétvége_Előírás();

        List<Adat_Vezénylés> AdatokVezénylés = new List<Adat_Vezénylés>();
        List<Adat_Jármű_hiba> AdatokJárműHiba = new List<Adat_Jármű_hiba>();
        List<Adat_Jármű> AdatokÁllomány = new List<Adat_Jármű>();
        List<Adat_Hétvége_Beosztás> AdatokElőírt = new List<Adat_Hétvége_Beosztás>();
        List<Adat_Hétvége_Előírás> Szín_Adatok = null;

        #region Alap
        public Ablak_T5C5_Segéd(List<Adat_T5C5_Posta> postaAdat, string honnan, DateTime dátum, string telephely, bool terv)
        {
            PostaAdat = postaAdat;
            Dátum = dátum;
            Cmbtelephely = telephely;


            InitializeComponent();
            Honnan = honnan;
            Terv = terv;

            Színek_Betöltése();
            Start();
        }

        private void Start()
        {
            string[] darab;
            if (Honnan == "Nap")
                darab = PostaAdat[0].Tényleges_szerelvény.Split('-');
            else
            {
                if (Terv)
                    darab = PostaAdat[0].Előírt_szerelvény.Split('-');
                else
                    darab = PostaAdat[0].Tényleges_szerelvény.Split('-');
            }

            switch (darab.Length)
            {
                case 1:
                    Kiir_1();
                    //Átméretezzük az ablakot
                    Width = 215;
                    Height = 440;

                    break;
                case 2:
                    Kiir_1();
                    Kiir_2();
                    //Átméretezzük az ablakot
                    Width = 415;
                    Height = 440;
                    break;
                case 3:
                    Kiir_1();
                    Kiir_2();
                    Kiir_3();
                    //Átméretezzük az ablakot
                    Width = 615;
                    Height = 440;
                    break;
                case 4:
                    Kiir_1();
                    Kiir_2();
                    Kiir_3();
                    Kiir_4();
                    //Átméretezzük az ablakot
                    Width = 815;
                    Height = 440;
                    break;
                case 5:
                    Kiir_1();
                    Kiir_2();
                    Kiir_3();
                    Kiir_4();
                    Kiir_5();
                    //Átméretezzük az ablakot
                    Width = 1015;
                    Height = 440;
                    break;
                case 6:
                    Kiir_1();
                    Kiir_2();
                    Kiir_3();
                    Kiir_4();
                    Kiir_5();
                    Kiir_6();
                    //Átméretezzük az ablakot
                    Width = 1215;
                    Height = 440;
                    break;
            }

            Kiírja_Vizsgálat();
            Jogosultságkiosztás();
            Vonalfeltöltés();
            Panel_Váltás();
        }

        private void Panel_Váltás()
        {
            if (Honnan != "Nap")
            {
                Panel_Nap_1.Visible = false;
                Panel_Nap_2.Visible = false;
                Panel_Nap_3.Visible = false;
                Panel_Nap_4.Visible = false;
                Panel_Nap_5.Visible = false;
                Panel_Nap_6.Visible = false;

                Panel_V_1.Left = 3;
                Panel_V_1.Top = 283;
                Panel_V_2.Left = 3;
                Panel_V_2.Top = 283;
                Panel_V_3.Left = 3;
                Panel_V_3.Top = 283;
                Panel_V_4.Left = 3;
                Panel_V_4.Top = 283;
                Panel_V_5.Left = 3;
                Panel_V_5.Top = 283;
                Panel_V_6.Left = 3;
                Panel_V_6.Top = 283;
            }
        }

        private void Ablak_T5C5_Segéd_Load(object sender, EventArgs e)
        {

        }

        private void Jogosultságkiosztás()
        {

            int melyikelem;

            Töröl_Nap_1.Visible = false;
            Töröl_Nap_2.Visible = false;
            Töröl_Nap_3.Visible = false;
            Töröl_Nap_4.Visible = false;
            Töröl_Nap_5.Visible = false;
            Töröl_Nap_6.Visible = false;

            Rögzít_Nap_1.Visible = false;
            Rögzít_Nap_2.Visible = false;
            Rögzít_Nap_3.Visible = false;
            Rögzít_Nap_4.Visible = false;
            Rögzít_Nap_5.Visible = false;
            Rögzít_Nap_6.Visible = false;

            Ütemez_Nap_1.Visible = false;
            Ütemez_Nap_2.Visible = false;
            Ütemez_Nap_3.Visible = false;
            Ütemez_Nap_4.Visible = false;
            Ütemez_Nap_5.Visible = false;
            Ütemez_Nap_6.Visible = false;

            melyikelem = 102;
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Rögzít_Nap_1.Visible = true;
                Rögzít_Nap_2.Visible = true;
                Rögzít_Nap_3.Visible = true;
                Rögzít_Nap_4.Visible = true;
                Rögzít_Nap_5.Visible = true;
                Rögzít_Nap_6.Visible = true;
            }

            // módosítás 2 
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Töröl_Nap_1.Visible = true;
                Töröl_Nap_2.Visible = true;
                Töröl_Nap_3.Visible = true;
                Töröl_Nap_4.Visible = true;
                Töröl_Nap_5.Visible = true;
                Töröl_Nap_6.Visible = true;
            }

            // módosítás 3
            if (MyF.Vanjoga(melyikelem, 3))
            {
                Ütemez_Nap_1.Visible = true;
                Ütemez_Nap_2.Visible = true;
                Ütemez_Nap_3.Visible = true;
                Ütemez_Nap_4.Visible = true;
                Ütemez_Nap_5.Visible = true;
                Ütemez_Nap_6.Visible = true;
            }

            melyikelem = 101;
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
            }
        }

        private void Ablak_T5C5_Segéd_KeyDown(object sender, KeyEventArgs e)
        {

            // ESC
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }

            //Ctrl+F
            if (e.Control && e.KeyCode == Keys.F)
            {

            }
        }

        private void Vonalfeltöltés()
        {
            try
            {
                Ütemező_vonal.Items.Clear();
                List<Adat_Hétvége_Előírás> Adatok = KézHétElőírás.Lista_Adatok(Cmbtelephely.Trim());
                foreach (Adat_Hétvége_Előírás Elem in Adatok)
                    Ütemező_vonal.Items.Add(Elem.Vonal);

                Ütemező_vonal.Refresh();
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
        private void Kiir_1()
        {
            try
            {
                Azonosító_1.Text = PostaAdat[0].Azonosító.Trim();
                Típus_1.Text = PostaAdat[0].Típus.Trim();
                Csatolható_1.Text = PostaAdat[0].Csatolható.Trim();
                E3_Sorszám_1.Text = PostaAdat[0].E3_sorszám.ToString();


                V2_Futott_Km_1.Text = PostaAdat[0].V2_Futott_Km.ToString();
                V2_következő_1.Text = PostaAdat[0].V2_következő.Trim();

                V_Sorszám_1.Text = PostaAdat[0].V_Sorszám.ToString();
                V_Következő_1.Text = PostaAdat[0].V_Következő.Trim();
                V_futott_Km_1.Text = PostaAdat[0].V_futott_Km.ToString();

                Terv_Nap_1.Text = PostaAdat[0].Terv_Nap.Trim();
                Panel_Nap_1.BackColor = Színez_Ütemez(PostaAdat[0].Terv_Nap.Trim());

                Napszám_1.Text = PostaAdat[0].Napszám.ToString();
                Hiba_1.Text = PostaAdat[0].Hiba.Trim();
                this.toolTip1.SetToolTip(this.Hiba_1, PostaAdat[0].Hiba.Trim());
                Előírt_szerelvény_1.Text = PostaAdat[0].Előírt_szerelvény.Trim();
                Tényleges_szerelvény_1.Text = PostaAdat[0].Tényleges_szerelvény.Trim();
                Rendelésszám_1.Text = PostaAdat[0].Rendelésszám.Trim();
                Panel_Adat_1.BackColor = Színez_Státus(PostaAdat[0].Státus);
                Szerelvény_szám_1.Text = PostaAdat[0].Szerelvényszám.ToString();
                BennMarad_1.Checked = PostaAdat[0].Marad == 4;
                VizsgálatÜtemez_1.Checked = PostaAdat[0].Vizsgál == 1;
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

        private void Kiir_2()
        {
            try
            {
                Azonosító_2.Text = PostaAdat[1].Azonosító.Trim();
                Típus_2.Text = PostaAdat[1].Típus.Trim();
                Csatolható_2.Text = PostaAdat[1].Csatolható.Trim();
                E3_Sorszám_2.Text = PostaAdat[1].E3_sorszám.ToString();

                V2_Futott_Km_2.Text = PostaAdat[1].V2_Futott_Km.ToString();
                V2_következő_2.Text = PostaAdat[1].V2_következő.Trim();

                V_Sorszám_2.Text = PostaAdat[1].V_Sorszám.ToString();
                V_Következő_2.Text = PostaAdat[1].V_Következő.Trim();
                V_futott_Km_2.Text = PostaAdat[1].V_futott_Km.ToString();

                Terv_Nap_2.Text = PostaAdat[1].Terv_Nap.Trim();
                Panel_Nap_2.BackColor = Színez_Ütemez(PostaAdat[1].Terv_Nap.Trim());

                Napszám_2.Text = PostaAdat[1].Napszám.ToString();
                Hiba_2.Text = PostaAdat[1].Hiba.Trim();
                this.toolTip1.SetToolTip(this.Hiba_2, PostaAdat[1].Hiba.Trim());
                Előírt_szerelvény_2.Text = PostaAdat[1].Előírt_szerelvény.Trim();
                Tényleges_szerelvény_2.Text = PostaAdat[1].Tényleges_szerelvény.Trim();
                Rendelésszám_2.Text = PostaAdat[1].Rendelésszám.Trim();
                Panel_Adat_2.BackColor = Színez_Státus(PostaAdat[1].Státus);
                Szerelvény_szám_2.Text = PostaAdat[1].Szerelvényszám.ToString();

                BennMarad_2.Checked = PostaAdat[1].Marad == 4;
                VizsgálatÜtemez_2.Checked = PostaAdat[1].Vizsgál == 1;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kiir_3()
        {
            try
            {
                Azonosító_3.Text = PostaAdat[2].Azonosító.Trim();
                Típus_3.Text = PostaAdat[2].Típus.Trim();
                Csatolható_3.Text = PostaAdat[2].Csatolható.Trim();
                E3_Sorszám_3.Text = PostaAdat[2].E3_sorszám.ToString();

                V2_Futott_Km_3.Text = PostaAdat[2].V2_Futott_Km.ToString();
                V2_következő_3.Text = PostaAdat[2].V2_következő.Trim();

                V_Sorszám_3.Text = PostaAdat[2].V_Sorszám.ToString();
                V_Következő_3.Text = PostaAdat[2].V_Következő.Trim();
                V_futott_Km_3.Text = PostaAdat[2].V_futott_Km.ToString();

                Terv_Nap_3.Text = PostaAdat[2].Terv_Nap.Trim();
                Panel_Nap_3.BackColor = Színez_Ütemez(PostaAdat[2].Terv_Nap.Trim());

                Napszám_3.Text = PostaAdat[2].Napszám.ToString();
                Hiba_3.Text = PostaAdat[2].Hiba.Trim();
                this.toolTip1.SetToolTip(this.Hiba_3, PostaAdat[2].Hiba.Trim());
                Előírt_szerelvény_3.Text = PostaAdat[2].Előírt_szerelvény.Trim();
                Tényleges_szerelvény_3.Text = PostaAdat[2].Tényleges_szerelvény.Trim();
                Rendelésszám_3.Text = PostaAdat[2].Rendelésszám.Trim();
                Panel_Adat_3.BackColor = Színez_Státus(PostaAdat[2].Státus);
                Szerelvény_szám_3.Text = PostaAdat[2].Szerelvényszám.ToString();

                BennMarad_3.Checked = PostaAdat[2].Marad == 4;
                VizsgálatÜtemez_3.Checked = PostaAdat[2].Vizsgál == 1;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kiir_4()
        {
            try
            {
                Azonosító_4.Text = PostaAdat[3].Azonosító.Trim();
                Típus_4.Text = PostaAdat[3].Típus.Trim();
                Csatolható_4.Text = PostaAdat[3].Csatolható.Trim();
                E3_Sorszám_4.Text = PostaAdat[3].E3_sorszám.ToString();
                V2_Futott_Km_4.Text = PostaAdat[3].V2_Futott_Km.ToString();
                V2_következő_4.Text = PostaAdat[3].V2_következő.Trim();

                V_Sorszám_4.Text = PostaAdat[3].V_Sorszám.ToString();
                V_Következő_4.Text = PostaAdat[3].V_Következő.Trim();
                V_futott_Km_4.Text = PostaAdat[3].V_futott_Km.ToString();

                Terv_Nap_4.Text = PostaAdat[3].Terv_Nap.Trim();
                Panel_Nap_4.BackColor = Színez_Ütemez(PostaAdat[3].Terv_Nap.Trim());

                Napszám_4.Text = PostaAdat[3].Napszám.ToString();
                Hiba_4.Text = PostaAdat[3].Hiba.Trim();
                this.toolTip1.SetToolTip(this.Hiba_4, PostaAdat[3].Hiba.Trim());
                Előírt_szerelvény_4.Text = PostaAdat[3].Előírt_szerelvény.Trim();
                Tényleges_szerelvény_4.Text = PostaAdat[3].Tényleges_szerelvény.Trim();
                Rendelésszám_4.Text = PostaAdat[3].Rendelésszám.Trim();
                Panel_Adat_4.BackColor = Színez_Státus(PostaAdat[3].Státus);
                Szerelvény_szám_4.Text = PostaAdat[3].Szerelvényszám.ToString();

                BennMarad_4.Checked = PostaAdat[3].Marad == 4;
                VizsgálatÜtemez_4.Checked = PostaAdat[3].Vizsgál == 1;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kiir_5()
        {
            try
            {
                Azonosító_5.Text = PostaAdat[4].Azonosító.Trim();
                Típus_5.Text = PostaAdat[4].Típus.Trim();
                Csatolható_5.Text = PostaAdat[4].Csatolható.Trim();
                E3_Sorszám_5.Text = PostaAdat[4].E3_sorszám.ToString();
                V2_Futott_Km_5.Text = PostaAdat[4].V2_Futott_Km.ToString();
                V2_következő_5.Text = PostaAdat[4].V2_következő.Trim();

                V_Sorszám_5.Text = PostaAdat[4].V_Sorszám.ToString();
                V_Következő_5.Text = PostaAdat[4].V_Következő.Trim();
                V_futott_Km_5.Text = PostaAdat[4].V_futott_Km.ToString();

                Terv_Nap_5.Text = PostaAdat[4].Terv_Nap.Trim();
                Panel_Nap_5.BackColor = Színez_Ütemez(PostaAdat[4].Terv_Nap.Trim());

                Napszám_5.Text = PostaAdat[4].Napszám.ToString();
                Hiba_5.Text = PostaAdat[4].Hiba.Trim();
                this.toolTip1.SetToolTip(this.Hiba_5, PostaAdat[4].Hiba.Trim());
                Előírt_szerelvény_5.Text = PostaAdat[4].Előírt_szerelvény.Trim();
                Tényleges_szerelvény_5.Text = PostaAdat[4].Tényleges_szerelvény.Trim();
                Rendelésszám_5.Text = PostaAdat[4].Rendelésszám.Trim();
                Panel_Adat_5.BackColor = Színez_Státus(PostaAdat[4].Státus);
                Szerelvény_szám_5.Text = PostaAdat[4].Szerelvényszám.ToString();

                BennMarad_5.Checked = PostaAdat[4].Marad == 4;
                VizsgálatÜtemez_5.Checked = PostaAdat[4].Vizsgál == 1;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Kiir_6()
        {
            try
            {
                Azonosító_6.Text = PostaAdat[5].Azonosító.Trim();
                Típus_6.Text = PostaAdat[5].Típus.Trim();
                Csatolható_6.Text = PostaAdat[5].Csatolható.Trim();
                E3_Sorszám_6.Text = PostaAdat[5].E3_sorszám.ToString();
                V2_Futott_Km_6.Text = PostaAdat[5].V2_Futott_Km.ToString();
                V2_következő_6.Text = PostaAdat[5].V2_következő.Trim();

                V_Sorszám_6.Text = PostaAdat[5].V_Sorszám.ToString();
                V_Következő_6.Text = PostaAdat[5].V_Következő.Trim();
                V_futott_Km_6.Text = PostaAdat[5].V_futott_Km.ToString();

                Terv_Nap_6.Text = PostaAdat[5].Terv_Nap.Trim();
                Panel_Nap_6.BackColor = Színez_Ütemez(PostaAdat[5].Terv_Nap.Trim());

                Napszám_6.Text = PostaAdat[5].Napszám.ToString();
                Hiba_6.Text = PostaAdat[5].Hiba.Trim();
                this.toolTip1.SetToolTip(this.Hiba_6, PostaAdat[5].Hiba.Trim());
                Előírt_szerelvény_6.Text = PostaAdat[5].Előírt_szerelvény.Trim();
                Tényleges_szerelvény_6.Text = PostaAdat[5].Tényleges_szerelvény.Trim();
                Rendelésszám_6.Text = PostaAdat[5].Rendelésszám.Trim();

                Panel_Adat_6.BackColor = Színez_Státus(PostaAdat[5].Státus);
                Szerelvény_szám_6.Text = PostaAdat[5].Szerelvényszám.ToString();

                BennMarad_6.Checked = PostaAdat[5].Marad == 4;
                VizsgálatÜtemez_6.Checked = PostaAdat[5].Vizsgál == 1;
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Color Színez_Ütemez(string Terv)
        {
            Color Válasz = Color.Silver;
            string[] darab = Terv.Trim().Split('-');
            switch (darab[darab.Length - 1])
            {
                case "e":
                    {
                        Válasz = Color.Olive;
                        break;
                    }
                case "a":
                    {
                        Válasz = Color.BlueViolet;
                        break;
                    }
                case "u":
                    {
                        Válasz = Color.Gray;
                        break;
                    }
            }
            return Válasz;
        }

        private Color Színez_Státus(int Státus)
        {
            Color Válasz = Color.Silver;

            switch (Státus)
            {
                case 4:
                    Válasz = Color.Red;
                    break;
                case 3:
                    Válasz = Color.Yellow;
                    break;
            }
            return Válasz;
        }
        #endregion


        #region Napi rögzítések
        private void Rögzít_Click(object sender, EventArgs e)
        {
            Rögzítés_Napi(Azonosító_1.Text.Trim(), BennMarad_1.Checked, VizsgálatÜtemez_1.Checked, Rendelésszám_1.Text.Trim(), Szerelvény_szám_1.Text.Trim(), int.Parse(V_Sorszám_1.Text));
        }

        private void Rögzít_Nap_2_Click(object sender, EventArgs e)
        {
            Rögzítés_Napi(Azonosító_2.Text.Trim(), BennMarad_2.Checked, VizsgálatÜtemez_2.Checked, Rendelésszám_2.Text.Trim(), Szerelvény_szám_2.Text.Trim(), int.Parse(V_Sorszám_2.Text));
        }

        private void Rögzít_Nap_3_Click(object sender, EventArgs e)
        {
            Rögzítés_Napi(Azonosító_3.Text.Trim(), BennMarad_3.Checked, VizsgálatÜtemez_3.Checked, Rendelésszám_3.Text.Trim(), Szerelvény_szám_3.Text.Trim(), int.Parse(V_Sorszám_3.Text));
        }

        private void Rögzít_Nap_4_Click(object sender, EventArgs e)
        {
            Rögzítés_Napi(Azonosító_4.Text.Trim(), BennMarad_4.Checked, VizsgálatÜtemez_4.Checked, Rendelésszám_4.Text.Trim(), Szerelvény_szám_4.Text.Trim(), int.Parse(V_Sorszám_4.Text));
        }

        private void Rögzít_Nap_5_Click(object sender, EventArgs e)
        {
            Rögzítés_Napi(Azonosító_5.Text.Trim(), BennMarad_5.Checked, VizsgálatÜtemez_5.Checked, Rendelésszám_5.Text.Trim(), Szerelvény_szám_5.Text.Trim(), int.Parse(V_Sorszám_5.Text));
        }

        private void Rögzít_Nap_6_Click(object sender, EventArgs e)
        {
            Rögzítés_Napi(Azonosító_6.Text.Trim(), BennMarad_6.Checked, VizsgálatÜtemez_6.Checked, Rendelésszám_6.Text.Trim(), Szerelvény_szám_6.Text.Trim(), int.Parse(V_Sorszám_6.Text));
        }
        //
        private void Rögzítés_Napi(string azonosító, bool Bennmarad, bool Vizsgálatrütemez, string Rendelésiszám, string Szerelvényszám, int Következővizsgálatszám)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\főkönyv\futás\{Dátum.Year}\vezénylés{Dátum.Year}.mdb";
                string jelszó = "tápijános";

                string szöveg;

                AdatokVezénylésListázás();
                if (AdatokVezénylés == null) return;
                Adat_Vezénylés AdatVezénylés = (from a in AdatokVezénylés
                                                where a.Azonosító == azonosító.Trim()
                                                && a.Dátum.ToShortDateString() == Dátum.ToShortDateString()
                                                && a.Törlés == 0
                                                select a).FirstOrDefault();


                if (AdatVezénylés == null)
                {
                    // ha van akkor rögzíteni kell
                    szöveg = "INSERT INTO vezényléstábla ";
                    szöveg += "(azonosító, Dátum, Státus, vizsgálatraütemez, takarításraütemez, vizsgálat, vizsgálatszám, rendelésiszám, törlés, szerelvényszám, fusson, álljon, típus) VALUES (";
                    szöveg += $"'{azonosító.Trim()}',";
                    szöveg += "'" + Dátum.ToString("yyyy.MM.dd") + "', ";
                    if (Bennmarad)
                        szöveg += "4, ";
                    else
                        szöveg += "3,";
                    if (!Vizsgálatrütemez)
                        szöveg += "0, ";
                    else
                        szöveg += "1,";
                    szöveg += "0, "; // takarításraütemez nem használt
                    if (Vizsgálatrütemez && (Rendelésiszám.Trim() == "" || Rendelésiszám.Trim() == "_"))
                    {
                        if (Vizsgálatrütemez)
                        {
                            szöveg += "'E3', ";
                        }
                        else
                        {
                            szöveg += "'_', ";
                        }
                    }
                    else
                    {
                        szöveg += "'V1',";
                    }
                    szöveg += Következővizsgálatszám.ToString() + ", ";
                    if (Rendelésiszám.Trim() == "")
                        szöveg += "'_', ";
                    else
                        szöveg += "'" + Rendelésiszám.Trim() + "',";
                    szöveg += "0, ";
                    szöveg += Szerelvényszám.Trim() + ", ";
                    szöveg += " 0, 0, 'T5C5')";
                }

                else
                {
                    // módosítás
                    szöveg = "UPDATE vezényléstábla SET ";
                    if (Bennmarad)
                        szöveg += " Státus=4, ";
                    else
                        szöveg += " Státus=3, ";
                    if (!Vizsgálatrütemez)
                        szöveg += " vizsgálatraütemez=0, ";
                    else
                        szöveg += " vizsgálatraütemez=1, ";
                    szöveg += " takarításraütemez=0, ";
                    if (Vizsgálatrütemez && (Rendelésiszám.Trim() == "" || Rendelésiszám.Trim() == "_"))
                    {
                        if (Vizsgálatrütemez)
                        {
                            szöveg += "vizsgálat ='E3', ";
                        }
                        else
                        {
                            szöveg += "vizsgálat ='_', ";
                        }
                    }

                    else
                    {
                        szöveg += "vizsgálat ='V1', ";
                    }
                    szöveg += " vizsgálatszám=" + Következővizsgálatszám + ", ";
                    if (Rendelésiszám.Trim() == "" || Rendelésiszám.Trim() == "_")
                        szöveg += " rendelésiszám='_', ";
                    else
                        szöveg += " rendelésiszám='" + Rendelésiszám.Trim() + "', ";
                    szöveg += " szerelvényszám=" + Szerelvényszám.Trim();
                    szöveg += $" WHERE [azonosító] ='{azonosító.Trim()}' AND [dátum]=#" + Dátum.ToString("M-d-yy") + "#";
                    szöveg += " AND [törlés]=0";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);

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


        #region Napi Törlések
        private void Töröl_Click(object sender, EventArgs e)
        {

            Töröl_napi(Azonosító_1.Text.Trim());
        }

        private void Töröl_Nap_2_Click(object sender, EventArgs e)
        {
            Töröl_napi(Azonosító_2.Text.Trim());
        }

        private void Töröl_Nap_3_Click(object sender, EventArgs e)
        {
            Töröl_napi(Azonosító_3.Text.Trim());
        }

        private void Töröl_Nap_4_Click(object sender, EventArgs e)
        {
            Töröl_napi(Azonosító_4.Text.Trim());
        }

        private void Töröl_Nap_5_Click(object sender, EventArgs e)
        {
            Töröl_napi(Azonosító_5.Text.Trim());
        }

        private void Töröl_Nap_6_Click(object sender, EventArgs e)
        {
            Töröl_napi(Azonosító_6.Text.Trim());
        }
        //
        private void Töröl_napi(string azonosító)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\főkönyv\futás\{Dátum.Year}\vezénylés{Dátum.Year}.mdb";
                string jelszó = "tápijános";

                string szöveg;
                AdatokVezénylésListázás();
                if (AdatokVezénylés == null) return;
                Adat_Vezénylés AdatVezénylés = (from a in AdatokVezénylés
                                                where a.Azonosító == azonosító.Trim()
                                                && a.Dátum.ToShortDateString() == Dátum.ToShortDateString()
                                                && a.Törlés == 0
                                                select a).FirstOrDefault();

                if (AdatVezénylés != null)
                {
                    szöveg = "UPDATE vezényléstábla SET törlés=1 ";
                    szöveg += $" WHERE [azonosító] ='{azonosító.Trim()}' AND [dátum]=#" + Dátum.ToString("M-d-yy") + "#";
                    szöveg += " AND [törlés]=0";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
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
        #endregion


        #region Napi Ütemezés
        private void Ütemez_Nap_1_Click(object sender, EventArgs e)
        {
            try
            {
                string Milyen;
                if (Rendelésszám_1.Text.Trim() != "")
                    Milyen = V_Következő_1.Text.Trim();
                else
                    Milyen = "E3";
                Ütemezés_általános(VizsgálatÜtemez_1.Checked, BennMarad_1.Checked, Azonosító_1.Text.Trim(), Milyen, V_Sorszám_1.Text.Trim());
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

        private void Ütemez_Nap_2_Click(object sender, EventArgs e)
        {
            try
            {
                string Milyen;
                if (Rendelésszám_2.Text.Trim() != "")
                    Milyen = V_Következő_2.Text.Trim();
                else
                    Milyen = "E3";
                Ütemezés_általános(VizsgálatÜtemez_2.Checked, BennMarad_2.Checked, Azonosító_2.Text.Trim(), Milyen, V_Sorszám_2.Text.Trim());
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

        private void Ütemez_Nap_3_Click(object sender, EventArgs e)
        {
            try
            {
                string Milyen;
                if (Rendelésszám_3.Text.Trim() != "")
                    Milyen = V_Következő_3.Text.Trim();
                else
                    Milyen = "E3";
                Ütemezés_általános(VizsgálatÜtemez_3.Checked, BennMarad_3.Checked, Azonosító_3.Text.Trim(), Milyen, V_Sorszám_3.Text.Trim());
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

        private void Ütemez_Nap_4_Click(object sender, EventArgs e)
        {
            try
            {
                string Milyen;
                if (Rendelésszám_4.Text.Trim() != "")
                    Milyen = V_Következő_4.Text.Trim();
                else
                    Milyen = "E3";
                Ütemezés_általános(VizsgálatÜtemez_4.Checked, BennMarad_4.Checked, Azonosító_4.Text.Trim(), Milyen, V_Sorszám_4.Text.Trim());
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

        private void Ütemez_Nap_5_Click(object sender, EventArgs e)
        {
            try
            {
                string Milyen;
                if (Rendelésszám_5.Text.Trim() != "")
                    Milyen = V_Következő_5.Text.Trim();
                else
                    Milyen = "E3";
                Ütemezés_általános(VizsgálatÜtemez_5.Checked, BennMarad_5.Checked, Azonosító_5.Text.Trim(), Milyen, V_Sorszám_5.Text.Trim());
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

        private void Ütemez_Nap_6_Click(object sender, EventArgs e)
        {
            try
            {
                string Milyen;
                if (Rendelésszám_6.Text.Trim() != "")
                    Milyen = V_Következő_6.Text.Trim();
                else
                    Milyen = "E3";
                Ütemezés_általános(VizsgálatÜtemez_6.Checked, BennMarad_6.Checked, Azonosító_6.Text.Trim(), Milyen, V_Sorszám_6.Text.Trim());
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
        //
        private void Ütemezés_általános(bool Vizsgálatraütemez, bool BennMarad, string Azonosító, string MireÜtemez, string Sorszám)
        {
            try
            {
                string helyhiba = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\villamos\hiba.mdb";
                string jelszó = "pozsgaii";
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\villamos\villamos.mdb";
                // naplózás
                string helynapló = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\hibanapló";
                helynapló += @"\" + DateTime.Now.ToString("yyyyMM") + "hibanapló.mdb";
                if (!File.Exists(helynapló)) Adatbázis_Létrehozás.Hibatáblalap(helynapló);

                bool talált;
                int szín;
                long státus;
                long újstátus = 0;
                string típusa = "";
                long hibáksorszáma;
                long hiba;
                DateTime mikor;

                if (Vizsgálatraütemez)
                {
                    // hiba leírása
                    string szöveg1 = "";
                    string szöveg3 = "KARÓRARUGÓ";
                    if (Vizsgálatraütemez)
                    {
                        if (MireÜtemez.Contains("V"))
                        {
                            szöveg1 += MireÜtemez.Trim() + "-" + Sorszám;
                            szöveg3 = szöveg1;
                        }
                        else
                        {
                            szöveg1 += MireÜtemez.Trim() + " ";
                        }
                    }

                    if (BennMarad)
                        szöveg1 += "-" + Dátum.ToString("yyyy.MM.dd.") + " Maradjon benn ";
                    else
                        szöveg1 += "-" + Dátum.ToString("yyyy.MM.dd.") + " Beálló ";

                    // Megnézzük, hogy volt-e már rögzítve ilyen szöveg
                    talált = false;

                    AdatJárműHibaListázás();
                    Adat_Jármű_hiba AdatJárműHiba = (from a in AdatokJárműHiba
                                                     where a.Azonosító == Azonosító.Trim()
                                                     && a.Hibaleírása.Contains(szöveg3.Trim())
                                                     select a).FirstOrDefault();
                    if (AdatJárműHiba != null) talált = true;


                    AdatJárműHiba = (from a in AdatokJárműHiba
                                     where a.Azonosító == Azonosító.Trim()
                                     && a.Hibaleírása.Contains(szöveg1.Trim())
                                     select a).FirstOrDefault();
                    if (AdatJárműHiba != null) talált = true;

                    AdatÁllományListázás();
                    Adat_Jármű AdatÁllomány = (from a in AdatokÁllomány
                                               where a.Azonosító == Azonosító.Trim()
                                               select a).FirstOrDefault();

                    szín = 0;
                    // ha már volt ilyen szöveg rögzítve a pályaszámhoz akkor nem rögzítjük mégegyszer
                    if (!talált)
                    {
                        // hibák számát emeljük és státus állítjuk ha kell
                        hibáksorszáma = AdatÁllomány.Hibák;
                        // If hibáksorszáma < 7 Then
                        szín = 1;
                        hiba = hibáksorszáma + 1;
                        típusa = AdatÁllomány.Típus;
                        státus = AdatÁllomány.Státus;
                        újstátus = 0;
                        if (státus != 4) // ha 4 státusa akkor nem kell módosítani.
                        {
                            if (BennMarad)
                            {
                                státus = 4;
                                mikor = DateTime.Now;
                            }
                            else
                            {
                                státus = 3;
                            }

                        }
                        else
                        {
                            újstátus = 1;
                        }

                        // rögzítjük a villamos.mdb-be
                        string szöveg = "UPDATE állománytábla SET ";
                        szöveg += " hibák=" + hiba.ToString() + ", ";
                        // csak akkor módosítkjuk a dátumot, ha nem áll
                        if (BennMarad && újstátus == 0)
                            szöveg += " miótaáll='" + DateTime.Now + "', ";
                        szöveg += " státus=" + státus.ToString();
                        szöveg += " WHERE  [azonosító]='" + Azonosító.Trim() + "'";
                        MyA.ABMódosítás(hely, jelszó, szöveg);


                        // beírjuk a hibákat
                        // ha 7-nál kevesebb hibája van akkor rögzítjük
                        if (szín == 1)
                        {
                            szöveg = "INSERT INTO hibatábla (létrehozta, korlát, hibaleírása, idő, javítva, típus, azonosító, hibáksorszáma ) VALUES (";
                            szöveg += "'" + Program.PostásNév.Trim() + "', ";
                            // ha a következő napra ütemez

                            if (BennMarad)
                                szöveg += " 4, ";
                            else
                                szöveg += " 3, ";

                            szöveg += "'" + szöveg1.Trim() + "', ";
                            szöveg += "'" + DateTime.Now + "', false, ";
                            szöveg += "'" + típusa.Trim() + "', ";
                            szöveg += "'" + Azonosító.Trim() + "', " + hibáksorszáma.ToString() + ")";
                            MyA.ABMódosítás(helyhiba, jelszó, szöveg);
                            // naplózzuk a hibákat
                            MyA.ABMódosítás(helynapló, jelszó, szöveg);

                            MessageBox.Show("Az adatok rögzítése megtörtént!", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                    throw new HibásBevittAdat("Nem lett a vizsgálat elvégzése kijelölve.");
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


        #region Vizsgálat rögzítés
        private void Kiírja_Vizsgálat()
        {
            try
            {
                AlapSzín();

                if (PostaAdat[0].Vonal.Trim() != "")
                {
                    Ütemező_vonal.Text = PostaAdat[0].Vonal.Trim();
                    string[] darabol = PostaAdat[0].Vissza.Split('-');
                    Visszacsatol_1.Checked = darabol[0].Trim() != "0";
                    Visszacsatol_2.Checked = darabol[1].Trim() != "0";
                    Visszacsatol_3.Checked = darabol[2].Trim() != "0";
                    Visszacsatol_4.Checked = darabol[3].Trim() != "0";
                    Visszacsatol_5.Checked = darabol[4].Trim() != "0";
                    Visszacsatol_6.Checked = darabol[5].Trim() != "0";

                    Palette_színezése(Ütemező_vonal.Text, darabol);
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

        private void AlapSzín()
        {
            Panel_Rendelés_1.BackColor = Color.Silver;
            Panel_V_1.BackColor = Color.Silver;

            Panel_Rendelés_2.BackColor = Color.Silver;
            Panel_V_2.BackColor = Color.Silver;

            Panel_Rendelés_3.BackColor = Color.Silver;
            Panel_V_3.BackColor = Color.Silver;

            V_Sorszám_5.BackColor = Color.Silver;
            Panel_V_4.BackColor = Color.Silver;

            Panel_Rendelés_5.BackColor = Color.Silver;
            Panel_V_5.BackColor = Color.Silver;

            Panel_Rendelés_6.BackColor = Color.Silver;
            Panel_V_6.BackColor = Color.Silver;
        }

        private void Rögzít_Vizsgálat(object sender, EventArgs e)
        {
            Rögzít_Metódus();
        }

        private void Rögzít_Metódus()
        {
            try
            {
                AlapSzín();
                if (Ütemező_vonal.Text.Trim() == "") throw new HibásBevittAdat("A vonalat meg kell adni.");

                Előíráslistázás();
                Adat_Hétvége_Beosztás AdatElőírt = (from a in AdatokElőírt
                                                    where a.Kocsi1 == Azonosító_1.Text.Trim()
                                                    select a).FirstOrDefault();

                string vissza1 = Visszacsatol_1.Checked ? "1" : "0";
                string vissza2 = Visszacsatol_2.Checked ? "1" : "0";
                string vissza3 = Visszacsatol_3.Checked ? "1" : "0";
                string vissza4 = Visszacsatol_4.Checked ? "1" : "0";
                string vissza5 = Visszacsatol_5.Checked ? "1" : "0";
                string vissza6 = Visszacsatol_6.Checked ? "1" : "0";

                string kapcsolót = Visszacsatol_1.Checked ? "1" : "0";
                kapcsolót += Visszacsatol_2.Checked ? "-1" : "-0";
                kapcsolót += Visszacsatol_3.Checked ? "-1" : "-0";
                kapcsolót += Visszacsatol_4.Checked ? "-1" : "-0";
                kapcsolót += Visszacsatol_5.Checked ? "-1" : "-0";
                kapcsolót += Visszacsatol_6.Checked ? "-1" : "-0";

                Adat_Hétvége_Beosztás ADAT = new Adat_Hétvége_Beosztás(
                                       0,
                                       Ütemező_vonal.Text.Trim(),
                                       Azonosító_1.Text.Trim(),
                                       Azonosító_2.Text.Trim(),
                                       Azonosító_3.Text.Trim(),
                                       Azonosító_4.Text.Trim(),
                                       Azonosító_5.Text.Trim(),
                                       Azonosító_6.Text.Trim(),
                                       vissza1,
                                       vissza2,
                                       vissza3,
                                       vissza4,
                                       vissza5,
                                       vissza6);


                if (AdatElőírt != null)
                    KézHétBeosztás.Módosítás(Cmbtelephely.Trim(), ADAT);
                else
                    KézHétBeosztás.Rögzítés(Cmbtelephely.Trim(), ADAT);

                string[] darabol = kapcsolót.Split('-');

                Palette_színezése(Ütemező_vonal.Text.Trim(), darabol);

                MessageBox.Show("Az adatok rögzítése megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Palette_színezése(string Vonala, string[] darabol)
        {
            foreach (Adat_Hétvége_Előírás Elem in Szín_Adatok)
            {

                if (Vonala.Trim() == Elem.Vonal.Trim())
                {
                    if (darabol[0].Trim() == "0")
                    {
                        Panel_Rendelés_1.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                        Panel_V_1.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                    }
                    if (darabol[1].Trim() == "0")
                    {
                        Panel_Rendelés_2.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                        Panel_V_2.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                    }
                    if (darabol[2].Trim() == "0")
                    {
                        Panel_Rendelés_3.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                        Panel_V_3.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                    }
                    if (darabol[3].Trim() == "0")
                    {
                        V_Sorszám_5.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                        Panel_V_4.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                    }
                    if (darabol[4].Trim() == "0")
                    {
                        Panel_Rendelés_5.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                        Panel_V_5.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                    }
                    if (darabol[5].Trim() == "0")
                    {
                        Panel_Rendelés_6.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                        Panel_V_6.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                    }
                    break;
                }
            }
        }

        private void Töröl_Vizsgálat(object sender, EventArgs e)
        {
            try
            {
                Előíráslistázás();

                Adat_Hétvége_Beosztás AdatElőírt = (from a in AdatokElőírt
                                                    where a.Kocsi1 == Azonosító_1.Text.Trim()
                                                    select a).FirstOrDefault();

                if (AdatElőírt != null) KézHétBeosztás.Törlés(Cmbtelephely.Trim(), Azonosító_1.Text.Trim());

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

        private void Színek_Betöltése()
        {
            Kezelő_Hétvége_Előírás Kéz = new Kezelő_Hétvége_Előírás();
            Szín_Adatok = Kéz.Lista_Adatok(Cmbtelephely.Trim());
        }

        private void Panel_V_1_DoubleClick(object sender, EventArgs e)
        {
            Ütemező_vonal.Text = "Nem kiadható";
            Rögzít_Metódus();
        }
        #endregion


        #region Ütemezés
        private void Ütemez_V_1_Click(object sender, EventArgs e)
        {
            Ütemezés_általános(true, true, Azonosító_1.Text.Trim(), V_Következő_1.Text.Trim(), V_Sorszám_1.Text.Trim());
        }

        private void Ütemez_V_2_Click(object sender, EventArgs e)
        {
            Ütemezés_általános(true, true, Azonosító_2.Text.Trim(), V_Következő_2.Text.Trim(), V_Sorszám_2.Text.Trim());
        }

        private void Ütemez_V_3_Click(object sender, EventArgs e)
        {
            Ütemezés_általános(true, true, Azonosító_3.Text.Trim(), V_Következő_3.Text.Trim(), V_Sorszám_3.Text.Trim());
        }

        private void Ütemez_V_4_Click(object sender, EventArgs e)
        {
            Ütemezés_általános(true, true, Azonosító_4.Text.Trim(), V_Következő_4.Text.Trim(), V_Sorszám_4.Text.Trim());
        }

        private void Ütemez_V_5_Click(object sender, EventArgs e)
        {
            Ütemezés_általános(true, true, Azonosító_5.Text.Trim(), V_Következő_5.Text.Trim(), V_Sorszám_5.Text.Trim());
        }

        private void Ütemez_V_6_Click(object sender, EventArgs e)
        {
            Ütemezés_általános(true, true, Azonosító_6.Text.Trim(), V_Következő_6.Text.Trim(), V_Sorszám_6.Text.Trim());
        }
        #endregion


        #region Listák feltöltése
        private void Előíráslistázás()
        {
            try
            {
                AdatokElőírt.Clear();
                AdatokElőírt = KézHétBeosztás.Lista_Adatok(Cmbtelephely.Trim());
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

        private void AdatÁllományListázás()
        {
            try
            {
                AdatokÁllomány.Clear();
                AdatokÁllomány = KézÁllomány.Lista_Adatok(Cmbtelephely.Trim());
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

        private void AdatJárműHibaListázás()
        {

            try
            {
                AdatokJárműHiba.Clear();
                AdatokJárműHiba = KézHiba.Lista_Adatok(Cmbtelephely.Trim());
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

        private void AdatokVezénylésListázás()
        {
            try
            {
                AdatokVezénylés.Clear();
                AdatokVezénylés = KézVezény.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
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
    }
}
