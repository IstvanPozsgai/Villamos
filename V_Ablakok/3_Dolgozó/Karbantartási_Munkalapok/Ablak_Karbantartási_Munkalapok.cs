using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.MindenEgyéb;
using Villamos.Villamos_Ablakok._3_Dolgozó.Karbantartási_Munkalapok;
using Villamos.Villamos_Adatszerkezet;
using MyColor = Villamos.V_MindenEgyéb.Kezelő_Szín;
using MyF = Függvénygyűjtemény;
using MyLista = Villamos.Villamos_Ablakok._3_Dolgozó.Karbantartási_Munkalapok.Karbantartási_ListaFeltöltés;
using MyPDF = Villamos.MindenEgyéb.PDF_Töltés;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Karbantartási_Munkalapok : Form
    {

        readonly Kezelő_Főkönyv_Zser_Km KézZser = new Kezelő_Főkönyv_Zser_Km();
        readonly Kezelő_Dolgozó_Beosztás_Új KézBeosztás = new Kezelő_Dolgozó_Beosztás_Új();
        readonly Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_DigitálisMunkalap_Dolgozó KézDigDolg = new Kezelő_DigitálisMunkalap_Dolgozó();
        readonly Kezelő_DigitálisMunkalap_Fej KézDigFej = new Kezelő_DigitálisMunkalap_Fej();
        readonly Kezelő_DigitálisMunkalap_Kocsik KézDigKocsi = new Kezelő_DigitálisMunkalap_Kocsik();
        readonly Kezelő_T5C5_Kmadatok KézKM = new Kezelő_T5C5_Kmadatok("T5C5");
        readonly Kezelő_Vezénylés KézVezénylés = new Kezelő_Vezénylés();

        List<Adat_Technológia_Rendelés> AdatokRendelés = new List<Adat_Technológia_Rendelés>();
        List<Adat_technológia_Ciklus> AdatokCiklus = new List<Adat_technológia_Ciklus>();
        List<Adat_Technológia_Kivételek> AdatokKivétel = new List<Adat_Technológia_Kivételek>();
        List<string> AdatokKivételCsop = new List<string>();
        List<Adat_Dolgozó_Alap> AdatokDolgozó = new List<Adat_Dolgozó_Alap>();
        List<Adat_Technológia_Alap> AdatokTípusT = new List<Adat_Technológia_Alap>();
        List<Adat_Technológia_Változat> AdatokVáltozat = new List<Adat_Technológia_Változat>();
        List<Adat_Technológia_Új> AdatokTechnológia = new List<Adat_Technológia_Új>();
        List<Adat_Kiegészítő_Csoportbeosztás> AdatokCsoport = new List<Adat_Kiegészítő_Csoportbeosztás>();


        List<string> PályaszámLista = new List<string>();
        List<string> Pályaszám_TáblaAdatok = new List<string>();
#pragma warning disable IDE0044
        List<string> Fájlok = new List<string>();
        Dictionary<string, string> Személy = new Dictionary<string, string>();
#pragma warning restore IDE0044



        /// <summary>
        /// Betű beállítások
        /// </summary>
        #region Betű Beállítás
        readonly Beállítás_Betű BeBetű = new Beállítás_Betű { Név = "Arial", Méret = 12 };
        readonly Beállítás_Betű BeBetűV = new Beállítás_Betű { Név = "Arial", Méret = 12, Vastag = true };
        readonly Beállítás_Betű BeBetűVD = new Beállítás_Betű { Név = "Arial", Méret = 12, Vastag = true, Dőlt = true };
        readonly Beállítás_Betű BeBetűD = new Beállítás_Betű { Név = "Arial", Méret = 12, Dőlt = true };
        readonly Beállítás_Betű BeBetűVG = new Beállítás_Betű { Név = "Arial", Méret = 12, Vastag = true, Szín = Color.Green };
        #endregion

        Byte[] bytes;

        /// <summary>
        /// Ez a változó jegyzi meg, hogy melyik sorszámtól kell a feladandó Excelt kiírni
        /// </summary>
        long NapiSorszám = -1;
        readonly int sormagagasság = 30;
        readonly string munkalap = "Munka1";

        List<string> Munka_végzi = new List<string>();

        string kiv_tartalom = "";
        int kijelölt_sor = -1;
        public bool csoportos = false;
        string elérés = "";
        int sor = 0;
        string munkafejléchelye = "";
        long KM_korr = 0;



        #region Alap
        public Ablak_Karbantartási_Munkalapok()
        {
            InitializeComponent();

        }

        private void Start()
        {
            try
            {
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }
                else
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }

                Dátum.Value = DateTime.Today;
                Típus_feltöltés();
                Irányítófeltöltés();
                Csoportfeltöltés();
                Dolgozók_feltöltése();
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Munkalap";
                if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);
                CHKMinta.Checked = false;
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


        private void Ablak_Karbantartási_Munkalapok_Load(object sender, EventArgs e)
        {
            Start();
        }


        private void Ablak_Karbantartási_Munkalapok_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Karbantartási_Rendelés?.Close();
            Új_Ablak_Karbantartás_Csoport?.Close();
        }

        private void Jogosultságkiosztás()
        {
            Digitális.Visible = false;
            FelExcel.Visible = false;
            if (Program.PostásTelephely == "Főmérnökség")
                CHKMinta.Visible = true;
            else
                CHKMinta.Visible = false;


            int melyikelem;
            // ide kell az összes gombot tenni amit szabályozni akarunk false

            melyikelem = 170;
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
                //Digitális.Visible = true;
                //FelExcel.Visible = true;
            }
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Kezelő_Kiegészítő_Sérülés KézSérülés = new Kezelő_Kiegészítő_Sérülés();
                Cmbtelephely.Items.Clear();
                List<Adat_Kiegészítő_Sérülés> Adatok = KézSérülés.Lista_Adatok();
                foreach (Adat_Kiegészítő_Sérülés rekord in Adatok)
                    Cmbtelephely.Items.Add(rekord.Név);

                Cmbtelephely.Refresh();

                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim(); }
                else
                { Cmbtelephely.Text = Program.PostásTelephely; }

                Cmbtelephely.Enabled = Program.Postás_Vezér;
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

        private void TelephelyekFeltöltéseÚj()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Adat in GombLathatosagKezelo.Telephelyek(this.Name))
                    Cmbtelephely.Items.Add(Adat.Trim());
                //Alapkönyvtárat beállítjuk 
                if (Cmbtelephely.Items.Cast<string>().Contains(Program.PostásTelephely))
                    Cmbtelephely.Text = Program.PostásTelephely;
                else
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim();
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



        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Karbantartási_Munkalap.html";
                MyF.Megnyitás(hely);
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

        private void Dolgozók_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((int)e.KeyChar == 13)
                {
                    if (Dolgozók.Text.Trim() != "" && kijelölt_sor != -1)
                    {
                        Dolgozó_hozzárendelés_elj();
                        if (Tábla_Beosztás.Rows.Count - 1 > kijelölt_sor) kijelölt_sor++;
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


        #region Feltöltések
        private void Irányítófeltöltés()
        {
            try
            {
                Kiadta.Items.Clear();
                AdatokDolgozó = MyLista.DolgozóLista(Cmbtelephely.Text.Trim());
                List<Adat_Dolgozó_Alap> Adatok = (from a in AdatokDolgozó
                                                  where a.Főkönyvtitulus != "" && a.Főkönyvtitulus != "_"
                                                  select a).ToList();
                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    Kiadta.Items.Add($"{rekord.DolgozóNév}_{rekord.Dolgozószám}-{rekord.Főkönyvtitulus}");
                Kiadta.EndUpdate();
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

        private void Típus_feltöltés()
        {
            try
            {
                Járműtípus.Items.Clear();
                AdatokTípusT = MyLista.TípustáblaLista();

                foreach (Adat_Technológia_Alap rekord in AdatokTípusT)
                    Járműtípus.Items.Add(rekord.Típus);

                Járműtípus.Refresh();
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

        private void Járműtípus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Járműtípus.Text.Trim() == "") return;

                Pályaszám.Items.Clear();
                Tábla_psz.Rows.Clear();
                Tábla_psz.Columns.Clear();
                AdatokTechnológia = MyLista.TechnológiaLista(Járműtípus.Text.Trim());
                AdatokCiklus = MyLista.KarbCiklusLista(Járműtípus.Text.Trim());
                AdatokVáltozat = MyLista.VáltozatLista(Járműtípus.Text.Trim(), Cmbtelephely.Text.Trim());
                Ciklus_feltöltés();

                AdatokTípusT = MyLista.AlTípustáblaLista(Járműtípus.Text.Trim());
                PályaszámLista = MyLista.Minden(Cmbtelephely.Text.Trim(), AdatokTípusT);
                Pályaszám_feltöltés();

                elérés = "Üres";
                Pályaszám_Variáció();
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

        private void Ciklus_feltöltés()
        {

            Combo_KarbCiklus.Text = "";
            Combo_KarbCiklus.Items.Clear();

            foreach (Adat_technológia_Ciklus rekord in AdatokCiklus)
                Combo_KarbCiklus.Items.Add(rekord.Fokozat);
            Combo_KarbCiklus.Refresh();
        }

        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Járműtípus.Text = "";
                Combo_KarbCiklus.Items.Clear();
                Combo_KarbCiklus.Text = "";
                Pályaszám.Items.Clear();
                Tábla_psz.Rows.Clear();
                Tábla_psz.Columns.Clear();

                Pályaszám_TáblaAdatok.Clear();

                if (Járműtípus.Text.Trim() != "")
                {
                    AdatokTechnológia = MyLista.TechnológiaLista(Járműtípus.Text.Trim());
                    AdatokTechnológia = (from a in AdatokTechnológia
                                         where a.Érv_kezdete >= Dátum.Value && a.Érv_vége <= Dátum.Value
                                         select a).ToList();
                }
                AdatokRendelés = MyLista.RendelésLista(Cmbtelephely.Text.Trim(), Dátum.Value);
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

        private void Hiba_sor_ValueChanged(object sender, EventArgs e)
        {
            Chk_hibássorok.Checked = true;
        }

        private void Szerszám_sor_ValueChanged(object sender, EventArgs e)
        {
            Chk_szerszám.Checked = true;
        }

        private void Combo_KarbCiklus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Adat_technológia_Ciklus Tech_Adat = (from a in AdatokCiklus
                                                 where a.Fokozat == Combo_KarbCiklus.Text.Trim()
                                                 select a).FirstOrDefault();
            if (Tech_Adat != null)
            {
                if (Tech_Adat.Csoportos == 1)
                    csoportos = true;
                else csoportos = false;
                elérés = Tech_Adat.Elérés.Trim();
            }

            Pályaszám_Variáció();
            Munkalap_Változatnév_Feltöltlés();
            Tábla_Beosztás.Rows.Clear();
            Tábla_Beosztás.Columns.Clear();
            Tábla_Beosztás.ClearSelection();
            Személy.Clear();
        }
        #endregion


        #region Pályaszám kezelés
        private void Pályaszám_feltöltés()
        {
            try
            {
                Pályaszám.Items.Clear();
                if (Járműtípus.Text.Trim() == "") return;

                for (int i = 0; i < PályaszámLista.Count; i++)
                    Pályaszám.Items.Add(PályaszámLista[i].Trim());
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

        private void Pályaszám_Variáció()
        {
            try
            {
                List<Adat_Technológia_Alap> AdatokTípus = MyLista.AlTípustáblaLista(Járműtípus.Text.Trim());
                if (elérés == "Üres") return;
                switch (elérés)
                {
                    case "Alap":
                        {
                            Pályaszám_TáblaAdatok = MyLista.Minden(Cmbtelephely.Text.Trim(), AdatokTípus);
                            break;
                        }
                    case "T5C5_E2":
                        {
                            Pályaszám_TáblaAdatok = MyLista.T5C5_E2(Cmbtelephely.Text.Trim(), Dátum.Value, PályaszámLista);
                            break;
                        }
                    case "T5C5_E3":
                        {
                            Pályaszám_TáblaAdatok = MyLista.T5C5_KarbFokozat(Cmbtelephely.Text.Trim(), Dátum.Value, "E3", PályaszámLista);
                            break;
                        }
                    case "T5C5_V1":
                        {
                            Pályaszám_TáblaAdatok = MyLista.T5C5_KarbFokozat(Cmbtelephely.Text.Trim(), Dátum.Value, "V1", PályaszámLista);
                            break;
                        }
                    case "T5C5_V2":
                        {
                            Pályaszám_TáblaAdatok = MyLista.T5C5_KarbFokozat(Cmbtelephely.Text.Trim(), Dátum.Value, "V2", PályaszámLista);
                            break;
                        }
                    case "T5C5_V3":
                        {
                            Pályaszám_TáblaAdatok = MyLista.T5C5_KarbFokozat(Cmbtelephely.Text.Trim(), Dátum.Value, "V3", PályaszámLista);
                            break;
                        }
                }
                Táblázat_kitöltés();
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

        private void Táblázat_kitöltés()
        {
            try
            {
                Tábla_psz.Rows.Clear();
                Tábla_psz.Columns.Clear();
                Tábla_psz.ColumnCount = 10;
                Tábla_psz.RowCount = 1;
                if (Pályaszám_TáblaAdatok.Count < 1) return;

                Tábla_psz.Columns[0].HeaderText = "";
                Tábla_psz.Columns[0].Width = 70;

                Tábla_psz.Columns[1].HeaderText = "";
                Tábla_psz.Columns[1].Width = 70;

                Tábla_psz.Columns[2].HeaderText = "";
                Tábla_psz.Columns[2].Width = 70;

                Tábla_psz.Columns[3].HeaderText = "";
                Tábla_psz.Columns[3].Width = 70;

                Tábla_psz.Columns[4].HeaderText = "";
                Tábla_psz.Columns[4].Width = 70;

                Tábla_psz.Columns[5].HeaderText = "";
                Tábla_psz.Columns[5].Width = 70;

                Tábla_psz.Columns[6].HeaderText = "";
                Tábla_psz.Columns[6].Width = 70;

                Tábla_psz.Columns[7].HeaderText = "";
                Tábla_psz.Columns[7].Width = 70;

                Tábla_psz.Columns[8].HeaderText = "";
                Tábla_psz.Columns[8].Width = 70;

                Tábla_psz.Columns[9].HeaderText = "";
                Tábla_psz.Columns[9].Width = 70;


                int sor = 0;
                int oszlop = 0;
                for (int i = 0; i < Pályaszám_TáblaAdatok.Count; i++)
                {
                    Tábla_psz.Rows[sor].Cells[oszlop].Value = Pályaszám_TáblaAdatok[i].Trim();
                    oszlop++;
                    if (oszlop == 10)
                    {
                        sor++;
                        oszlop = 0;
                        Tábla_psz.RowCount++;
                    }
                }
                Tábla_psz.ClearSelection();

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

        private void Minden_Pályaszám_Click(object sender, EventArgs e)
        {
            Pályaszám_TáblaAdatok = PályaszámLista;
            Táblázat_kitöltés();
        }

        private void Tábla_ürítés_Click(object sender, EventArgs e)
        {
            Pályaszám_TáblaAdatok.Clear();
            Táblázat_kitöltés();
        }

        private void Hozzá_ad_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva egy kocsi sem.");
                bool volt = false;
                for (int i = 0; i < Pályaszám.Items.Count; i++)
                {
                    if (Pályaszám.Items[i].ToString().Contains(Pályaszám.Text.Trim()))
                    {
                        volt = true;
                    }
                }
                if (!volt)
                    throw new HibásBevittAdat($"Ez a {Pályaszám.Text.Trim()} pályaszám nem eleme ennek a típusnak.");

                if (Pályaszám_TáblaAdatok.Contains(Pályaszám.Text.Trim()))
                    throw new HibásBevittAdat($"Ezt a {Pályaszám.Text.Trim()} pályaszámot már tartalmazza a táblázat.");

                Pályaszám_TáblaAdatok.Add(Pályaszám.Text.Trim());
                Pályaszám_TáblaAdatok.Sort();
                Táblázat_kitöltés();
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

        private void Elem_törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (kiv_tartalom.Trim() == "") throw new HibásBevittAdat("A táblázatban nincs kiválasztva érvényes elem.");
                Pályaszám_TáblaAdatok.Remove(kiv_tartalom.Trim());
                Táblázat_kitöltés();
                kiv_tartalom = "";
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

        private void Tábla_psz_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Tábla_psz.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null) return;

            kiv_tartalom = Tábla_psz.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToStrTrim();
        }
        #endregion


        #region Dolgozói adatok
        private void Csoport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dolgozók_feltöltése();
        }

        private void Dolgozók_feltöltése()
        {
            try
            {
                Dolgozók.Items.Clear();

                List<Adat_Dolgozó_Alap> Adatok;
                if (Csoport.Text.Trim() == "")
                    Adatok = AdatokDolgozó;
                else
                    Adatok = (from a in AdatokDolgozó
                              where a.Csoport == Csoport.Text.Trim()
                              select a).ToList();

                if (Beosztás.Checked) Adatok = KézDolgozó.MunkaVégzőLista(Cmbtelephely.Text.Trim(), Dátum.Value, Adatok);

                foreach (Adat_Dolgozó_Alap A in Adatok)
                    Dolgozók.Items.Add(A.DolgozóNév.Trim() + "_" + A.Dolgozószám.Trim());
                Dolgozók.EndUpdate();
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

        private void Csoportfeltöltés()
        {
            Csoport.Items.Clear();
            AdatokCsoport = MyLista.CsoportLista(Cmbtelephely.Text.Trim());

            Csoport.Items.Add("");
            foreach (Adat_Kiegészítő_Csoportbeosztás rekord in AdatokCsoport)
                Csoport.Items.Add(rekord.Csoportbeosztás);
            Csoport.EndUpdate();
        }

        private void Beosztás_CheckedChanged(object sender, EventArgs e)
        {
            Dolgozók_feltöltése();
        }

        private void Dolgozó_Hozzárendelés_Click(object sender, EventArgs e)
        {
            Dolgozó_hozzárendelés_elj();
            kijelölt_sor = -1;
        }

        private void Dolgozó_hozzárendelés_elj()
        {
            try
            {
                if (Tábla_Beosztás.Rows.Count <= 0) throw new HibásBevittAdat("Előbb ki kell választani a változatot!");
                if (kijelölt_sor < 0) throw new HibásBevittAdat("A táblázatban nincs kijelölve a rögzítéshez sor.");
                if (Dolgozók.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva dolgozó.");

                Tábla_Beosztás.Rows[kijelölt_sor].Cells[1].Value = Dolgozók.Text.Trim();

                string a = Tábla_Beosztás.Rows[kijelölt_sor].Cells[0].Value == null ? "_" : Tábla_Beosztás.Rows[kijelölt_sor].Cells[0].Value.ToStrTrim();
                string b = Tábla_Beosztás.Rows[kijelölt_sor].Cells[1].Value == null ? "_" : Tábla_Beosztás.Rows[kijelölt_sor].Cells[1].Value.ToStrTrim();
                if (Személy.ContainsKey(a))
                    Személy[a] = b;
                else
                    Személy.Add(a, b);
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

        private void Munkalap_Változatnév_SelectedIndexChanged(object sender, EventArgs e)
        {
            Személy.Clear();
            Tábla_Beosztás_feltöltés();
        }

        private void Tábla_Beosztás_feltöltés()
        {
            try
            {
                Tábla_Beosztás.Rows.Clear();
                Tábla_Beosztás.Columns.Clear();
                Tábla_Beosztás.ColumnCount = 2;

                // fejléc elkészítése
                Tábla_Beosztás.Columns[0].HeaderText = "Csoportosítási elnevezés";
                Tábla_Beosztás.Columns[0].Width = 200;
                Tábla_Beosztás.Columns[1].HeaderText = "Dolgozónév";
                Tábla_Beosztás.Columns[1].Width = 300;

                Munka_végzi.Clear();
                Munka_végzi = (from a in AdatokVáltozat
                               where a.Változatnév == Munkalap_Változatnév.Text.Trim()
                               orderby a.Végzi
                               select a.Végzi).Distinct().ToList();

                Tábla_Beosztás.RowCount = Munka_végzi.Count;
                for (int i = 0; i < Munka_végzi.Count; i++)
                {
                    Tábla_Beosztás.Rows[i].Cells[0].Value = Munka_végzi[i].Trim();
                }
                kijelölt_sor = -1;

                Tábla_Beosztás.ClearSelection();
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

        private void Tábla_Beosztás_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                kijelölt_sor = e.RowIndex;
                if (Tábla_Beosztás.Rows.Count <= 0) throw new HibásBevittAdat("Előbb ki kell választani a változatot!");
                if (Dolgozók.Text.Trim() != "")
                {

                    if (e.RowIndex < 0) return;
                    Tábla_Beosztás.Rows[e.RowIndex].Cells[1].Value = Dolgozók.Text;

                    string a = Tábla_Beosztás.Rows[e.RowIndex].Cells[0].Value == null ? "_" : Tábla_Beosztás.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
                    string b = Tábla_Beosztás.Rows[e.RowIndex].Cells[1].Value == null ? "_" : Tábla_Beosztás.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
                    if (Személy.ContainsKey(a))
                        Személy[a] = b;
                    else
                        Személy.Add(a, b);
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

        private void Új_sor_Click(object sender, EventArgs e)
        {
            try
            {
                Dolgozók.Text = "";
                if (kijelölt_sor < 0) throw new HibásBevittAdat("Nincs kijelölve egy sor sem.");

                string tartalom = Tábla_Beosztás.Rows[kijelölt_sor].Cells[0].Value.ToStrTrim();
                string[] daraboló = tartalom.Split('_');

                if (!int.TryParse(daraboló[daraboló.Length - 1], out int sorszám))
                {
                    sorszám++;
                    Tábla_Beosztás.Rows[kijelölt_sor].Cells[0].Value = daraboló[0] + "_" + sorszám;
                }
                bool folytat = true;
                while (folytat)
                {
                    if (Tábla_Beosztás.Rows.Count - 1 > kijelölt_sor)
                    {
                        if (Tábla_Beosztás.Rows[kijelölt_sor + 1].Cells[0].Value.ToStrTrim().Contains(daraboló[0]))
                        {
                            sorszám++;
                            kijelölt_sor++;
                        }
                        else
                            folytat = false;
                    }
                    else
                    {
                        folytat = false;
                    }

                }
                Tábla_Beosztás.Rows.Add(daraboló[0] + "_" + (sorszám + 1).ToString());
                Tábla_Beosztás.Sort(Tábla_Beosztás.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
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

        private void Munkalap_Változatnév_Feltöltlés()
        {
            try
            {
                if (Járműtípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva járműtípus.");
                if (Combo_KarbCiklus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva karbantartási ciklus.");

                List<string> ELemek = (from a in AdatokVáltozat
                                       where a.Karbantartási_fokozat == Combo_KarbCiklus.Text.Trim()
                                       orderby a.Végzi
                                       select a.Változatnév).Distinct().ToList();
                Munkalap_Változatnév.Items.Clear();
                foreach (string elem in ELemek)
                    Munkalap_Változatnév.Items.Add(elem);

                Munkalap_Változatnév.Refresh();
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


        #region Rendelés adatok
        Ablak_Karbantartási_Rendelés Új_Ablak_Karbantartási_Rendelés;
        private void RendelésAdatok_Click(object sender, EventArgs e)
        {
            Új_Ablak_Karbantartási_Rendelés?.Close();
            Új_Ablak_Karbantartási_Rendelés = new Ablak_Karbantartási_Rendelés(Cmbtelephely.Text.Trim());
            Új_Ablak_Karbantartási_Rendelés.FormClosed += Új_Ablak_Karbantartási_Rendelés_Closed;
            Új_Ablak_Karbantartási_Rendelés.Show();
        }

        private void Új_Ablak_Karbantartási_Rendelés_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Karbantartási_Rendelés = null;
        }
        #endregion


        #region Csoportosítás
        Ablak_Karbantartás_Csoport Új_Ablak_Karbantartás_Csoport;
        private void BtnCsoportosítás_Click(object sender, EventArgs e)
        {
            Új_Ablak_Karbantartás_Csoport?.Close();
            Új_Ablak_Karbantartás_Csoport = new Ablak_Karbantartás_Csoport(Cmbtelephely.Text.Trim());
            Új_Ablak_Karbantartás_Csoport.FormClosed += Új_Új_Ablak_Karbantartás_Csoport_Closed;
            Új_Ablak_Karbantartás_Csoport.Show();
        }

        private void Új_Új_Ablak_Karbantartás_Csoport_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Karbantartás_Csoport = null;
        }
        #endregion


        #region Excel kimenet
        private void Excel_mentés_Click(object sender, EventArgs e)
        {
            try
            {
                Fájlok.Clear();
                DateTime Eleje = DateTime.Now;
                if (Combo_KarbCiklus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy ciklus fokozat sem!");
                if (Járműtípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy járműtípus sem!");
                if (Pályaszám_TáblaAdatok.Count < 1) throw new HibásBevittAdat("Nincs a táblázatba felvéve egy pályaszám sem!");

                Excel_mentés.Visible = false;
                string könyvtár = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                if (csoportos)
                {
                    string fájlnév = $"Technológia_{Program.PostásNév}_{Járműtípus.Text.Trim()}_{Combo_KarbCiklus.Text.Trim()}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                    string fájlexc = $@"{könyvtár}\{fájlnév}";
                    Excel_tábla(fájlexc);
                    if (Nyomtat_igen.Checked) MyF.ExcelNyomtatás(Fájlok);
                    //fájl törlése
                    if (!Töröl_igen.Checked) MyF.Megnyitás(fájlexc);
                }
                else
                {
                    foreach (string psz in Pályaszám_TáblaAdatok)
                    {
                        string fájlnév = $"Technológia_{Program.PostásNév}_{psz}_{Járműtípus.Text.Trim()}_{Combo_KarbCiklus.Text.Trim()}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                        string fájlexc = $@"{könyvtár}\{fájlnév}";
                        Pályaszám.Text = psz;
                        Excel_tábla(fájlexc);
                    }
                    if (Nyomtat_igen.Checked) MyF.ExcelNyomtatás(Fájlok, Töröl_igen.Checked);
                    if (!Töröl_igen.Checked) MyF.Megnyitások(Fájlok);
                }

                DateTime Vége = DateTime.Now;
                MessageBox.Show($"A nyomtatvány elkészült:{Vége - Eleje}", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Holtart.Ki();
                Excel_mentés.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Excel_mentés.Visible = true;
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Excel_tábla(string fájlexc)
        {
            try
            {
                Adat_technológia_Ciklus AdatCikk = (from a in AdatokCiklus
                                                    where a.Fokozat == Combo_KarbCiklus.Text.Trim()
                                                    select a).FirstOrDefault();

                Holtart.Be(25, MyColor.ColorToHex(Color.DeepSkyBlue));

                //Változatok
                List<Adat_Technológia_Változat> VÁLTAdatok = (from a in AdatokVáltozat
                                                              where a.Változatnév == Munkalap_Változatnév.Text.Trim()
                                                              select a).ToList();

                //pályaszám kivételei
                AdatokKivétel = MyLista.KivételekLista(Járműtípus.Text.Trim());
                AdatokKivételCsop = CsoportosKivételek();


                List<Adat_Technológia_Új> Adatok = (from a in AdatokTechnológia
                                                    where a.Karb_ciklus_eleje <= AdatCikk.Sorszám && a.Karb_ciklus_vége >= AdatCikk.Sorszám
                                                    && a.Érv_kezdete <= Dátum.Value && a.Érv_vége >= Dátum.Value
                                                    orderby a.Részegység, a.Munka_utasítás_szám, a.ID
                                                    select a).ToList();
                KM_korr = 0;
                //Egyedi munkalapokon kiírja a km adatokat
                if (CHKKMU.Checked && !csoportos)
                {
                    //KMU érték
                    List<Adat_T5C5_Kmadatok> KmAdatok = KézKM.Lista_Adatok();
                    Adat_T5C5_Kmadatok EgyKmAdat = (from a in KmAdatok
                                                    where a.Azonosító == Pályaszám.Text.Trim()
                                                    orderby a.Vizsgdátumk descending
                                                    select a).FirstOrDefault();
                    KM_korr = 0;
                    if (EgyKmAdat != null) KM_korr = EgyKmAdat.KMUkm;

                    //KMU korrekció
                    List<Adat_Főkönyv_Zser_Km> AdatokZSER = KézZser.Lista_adatok(Dátum.Value.Year);
                    if (Dátum.Value.Month < 4)
                    {
                        List<Adat_Főkönyv_Zser_Km> AdatokZSERelőző = KézZser.Lista_adatok(Dátum.Value.Year - 1);
                        AdatokZSER.AddRange(AdatokZSERelőző);
                    }


                    if (AdatokZSER != null && EgyKmAdat != null)
                    {
                        List<Adat_Főkönyv_Zser_Km> KorNapikmLista = (from a in AdatokZSER
                                                                     where a.Azonosító == Pályaszám.Text.Trim() && a.Dátum > EgyKmAdat.KMUdátum
                                                                     select a).ToList();
                        long KorNapikm = 0;
                        if (KorNapikmLista != null)
                            KorNapikm = KorNapikmLista.Sum(a => a.Napikm);
                        KM_korr += KorNapikm;
                    }
                }


                //legkisebb dátum
                DateTime hatályos = Adatok.Min(a => a.Érv_kezdete);

                string munkalap = "Munka1";
                string hatályos_str = $"Hatálybalépés dátuma:{hatályos:yyyy.MM.dd}";

                string Verzió = $"{Járműtípus.Text.Trim()}_{Combo_KarbCiklus.Text.Trim()}_{AdatCikk.Verzió}";
                if (Járműtípus.Text.Trim().Length > 15) Verzió = $"{Járműtípus.Text.Trim()}\n_{Combo_KarbCiklus.Text.Trim()}_{AdatCikk.Verzió}";


                MyX.ExcelLétrehozás(munkalap);
                ExcelMunkalap();
                int sormagagasság = 30;
                sor = 1;
                sor = Díszesblokk(sor, Verzió);
                sor = FejlécÁltalános(sor);
                sor = MunkaFejléc(sor);
                sor = Fejlécspec(sor);

                if (csoportos)
                {
                    foreach (string dolgnév in Személy.OrderBy(a => a.Value).Select(a => a.Value).Distinct())
                    {
                        sor = CsoportosPályaszámokÚj(sor, dolgnév);
                    }
                }

                //Tartalom
                sor = Részletes(munkalap, Adatok, AdatokKivétel, sormagagasság, VÁLTAdatok, sor);


                Holtart.Be(7, MyColor.ColorToHex(Color.Green));
                //Karbantartó tevékenység
                if (Chk_hibássorok.Checked) sor = KarbantartóSorok(sor);
                Holtart.Lép();

                //Szerszámok
                if (Chk_szerszám.Checked == true) sor = SzerszámokSorok(sor);
                Holtart.Lép();

                //Megjegyzések
                sor = MegjegyzésSorok(sor);
                Holtart.Lép();

                //     Nyomtatási beállítások
                Beállítás_Nyomtatás Benyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:Q{sor}",
                    IsmétlődőSorok = munkafejléchelye,
                    LáblécBal = hatályos_str,
                    LáblécKözép = "&P / &N oldal",
                    LáblécJobb = Verzió,
                    LapSzéles = 1,
                    AlsóMargó = 14,
                    FelsőMargó = 9,
                    BalMargó = 6,
                    JobbMargó = 6,
                    FejlécMéret = 8,
                    LáblécMéret = 8

                };
                MyX.NyomtatásiTerület_részletes(munkalap, Benyom);

                Holtart.Lép();

                MyX.ExcelMentés(fájlexc);
                Fájlok.Add(fájlexc);
                Holtart.Ki();
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

        private List<string> CsoportosKivételek()
        {
            List<string> Válasz = new List<string>();
            try
            {
                foreach (string Elem in PályaszámLista)
                {
                    Adat_Technológia_Kivételek AdatPSz = AdatokKivétel.FirstOrDefault(a => a.Azonosító == Elem);
                    if (AdatPSz != null) Válasz.Add(AdatPSz.Altípus);
                }
                Válasz = Válasz.Distinct().ToList();
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
            return Válasz;
        }

        private int MunkaFejléc(int sor)
        {
            //Munkalap fejléc
            sor++;
            MyX.Egyesít(munkalap, $"K{sor}:L{sor}");
            MyX.Kiir("Státusz** ", $"K{sor}");

            sor++;
            MyX.Sormagasság(munkalap, $"{sor}:{sor}", 20);
            MyX.Sormagasság(munkalap, $"{sor - 1}:{sor - 1}", 20);
            MyX.Egyesít(munkalap, $"A{sor - 1}:A{sor}");
            MyX.Kiir("Nr.", $"A{sor - 1}");

            MyX.Egyesít(munkalap, $"B{sor - 1}:I{sor}");
            MyX.Kiir("MUNKAUTASÍTÁS LEÍRÁSA", $"B{sor - 1}");

            MyX.Egyesít(munkalap, $"J{sor - 1}:J{sor}");
            MyX.Kiir("Karb. Cikl.", $"J{sor - 1}:J{sor}");
            MyX.Sortörésseltöbbsorba(munkalap, $"J{sor - 1}:J{sor}", true);

            MyX.Kiir("OK", $"K{sor}");
            MyX.Kiir("Jav.*", $"L{sor}");

            MyX.Egyesít(munkalap, $"M{sor - 1}:O{sor}");
            MyX.Kiir("Utasítást Végrehajtotta***", $"M{sor - 1}");
            MyX.Sortörésseltöbbsorba(munkalap, $"M{sor - 1}:O{sor}", true);

            MyX.Egyesít(munkalap, $"P{sor - 1}:Q{sor}");
            MyX.Kiir("Aláírás", $"P{sor - 1}");

            MyX.Rácsoz(munkalap, $"A{sor - 1}:Q{sor}");
            MyX.Betű(munkalap, $"{sor - 1}:{sor}", BeBetűV);
            MyX.Háttérszín(munkalap, $"A{sor - 1}:Q{sor}", Color.Gainsboro);


            munkafejléchelye = $"${sor - 1}:${sor + 1}";
            return sor;
        }

        private int MegjegyzésSorok(int sor)
        {
            sor += 2;
            MyX.Egyesít(munkalap, $"A{sor}:Q{sor}");
            string szövegrész = "Megjegyzés: \n" +
                "(*) Nemmegfelelősségeket jelezd részletsesen írásban\n" +
                "(**) Státusz oszlopba pipálással jelezd a munkafolyamat eredeményét\n" +
                "(***) Aláírásommal igazolom, hogy a felsorolt járműveken, a típusra aktuálisan" +
                " érvényes Főtechnológia jelölt karbantartási ciklusban előírt feladatait elvégeztem.";
            MyX.Kiir(szövegrész, $"A{sor}");
            MyX.Igazít_vízszintes(munkalap, $"A{sor}:Q{sor}", "bal");
            MyX.Sormagasság(munkalap, $"{sor}:{sor}", 83);

            sor += 2;
            MyX.Egyesít(munkalap, $"A{sor}" + ":J" + sor);
            MyX.Egyesít(munkalap, "K" + sor + ":M" + sor);
            MyX.Egyesít(munkalap, "N" + sor + ":Q" + sor);
            MyX.Kiir("Az ellenőrzések, javítások elvégzését követően a jármű forgalomképes.", $"A{sor}");
            MyX.Kiir("Ellenőrizte:", "K" + sor);
            MyX.Igazít_vízszintes(munkalap, $"A{sor}:Q{sor}", "bal");

            sor++;
            MyX.Egyesít(munkalap, $"M{sor}:Q{sor}");
            MyX.Pontvonal(munkalap, $"M{sor}:Q{sor}");
            if (Kiadta.Text.Trim() == "")
                MyX.Kiir("Irányító", $"M{sor}");
            else
            {
                string ideig = Kiadta.Text.Trim().Replace("-", "\n");
                MyX.Kiir(ideig, $"M{sor}");
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 52);
                MyX.Sortörésseltöbbsorba(munkalap, $"M{sor}", true);
            }
            MyX.Igazít_vízszintes(munkalap, $"A{sor}:Q{sor}", "közép");
            return sor;
        }

        private int SzerszámokSorok(int sor)
        {
            sor++;
            MyX.Egyesít(munkalap, $"A{sor}:Q{sor}");
            MyX.Kiir("A KARBANTARTÓ TEVÉKENYSÉG SORÁN HASZNÁLT KALIBRÁLT ESZKÖZÖK, SZERSZÁMOK LISTÁJA", $"A{sor}");
            MyX.Vastagkeret(munkalap, $"A{sor}:Q{sor}");
            MyX.Sormagasság(munkalap, $"{sor}:{sor}", sormagagasság);
            MyX.Háttérszín(munkalap, $"A{sor}:Q{sor}", System.Drawing.Color.Gainsboro);

            sor++;
            MyX.Egyesít(munkalap, $"A{sor}" + ":F" + sor);
            MyX.Egyesít(munkalap, "G" + sor + ":I" + sor);
            MyX.Egyesít(munkalap, "J" + sor + ":N" + sor);
            MyX.Egyesít(munkalap, "O" + sor + ":Q" + sor);

            MyX.Kiir("ESZKÖZ, SZERSZÁM TÍPUSA", $"A{sor}");
            MyX.Kiir("SOROZATSZÁMA", "G" + sor);
            MyX.Kiir("MUNKAUTASÍTÁS SORSZÁMA", "J" + sor);
            MyX.Kiir("ALÁÍRÁS", "O" + sor);
            MyX.Rácsoz(munkalap, $"A{sor}:Q{sor}");
            MyX.Sormagasság(munkalap, $"{sor}:{sor}", sormagagasság);

            int maximum = (int)Szerszám_sor.Value;
            int soreleje = sor + 1;
            for (int i = 0; i < maximum; i++)
            {
                sor++;
                MyX.Egyesít(munkalap, $"A{sor}" + ":F" + sor);
                MyX.Egyesít(munkalap, "G" + sor + ":I" + sor);
                MyX.Egyesít(munkalap, "J" + sor + ":N" + sor);
                MyX.Egyesít(munkalap, "O" + sor + ":Q" + sor);

            }
            MyX.Rácsoz(munkalap, $"A{soreleje}:Q{sor}");
            MyX.Sormagasság(munkalap, soreleje.ToString() + ":" + $"{sor}", sormagagasság);
            return sor;
        }

        private int KarbantartóSorok(int sor)
        {
            sor++;
            MyX.Egyesít(munkalap, $"A{sor}:Q{sor}");
            MyX.Kiir("A KARBANTARTÓ TEVÉKENYSÉG SORÁN FELMERÜLŐ ÉSZREVÉTELEK, JAVÍTÁSOK", $"A{sor}");
            MyX.Vastagkeret(munkalap, $"A{sor}:Q{sor}");
            MyX.Sormagasság(munkalap, $"{sor}:{sor}", sormagagasság);
            MyX.Háttérszín(munkalap, $"A{sor}:Q{sor}", System.Drawing.Color.Gainsboro);

            int maximum = (int)Hiba_sor.Value;
            int soreleje = sor + 1;
            for (int i = 0; i < maximum; i++)
            {
                sor++;
                MyX.Egyesít(munkalap, $"A{sor}:Q{sor}");
            }
            MyX.Rácsoz(munkalap, $"A{soreleje}:Q{sor}");
            MyX.Sormagasság(munkalap, $"{soreleje}:{sor}", sormagagasság);
            return sor;
        }


        private int Részletes(string munkalap, List<Adat_Technológia_Új> Adatok, List<Adat_Technológia_Kivételek> KivételAdatok, int sormagagasság,
                  List<Adat_Technológia_Változat> VÁLTAdatok, int sor)
        {
            Holtart.Be(Adatok.Count + 2, MyColor.ColorToHex(Color.Orange));

            //munkalap érdemi része
            foreach (Adat_Technológia_Új a in Adatok)
            {
                //Ha speciális, akkor kiírja különben kihagy
                if (Ki_kell_írni(a.Altípus, csoportos, KivételAdatok))
                {
                    sor++;
                    if (a.Munka_utasítás_szám.Trim() == "0")
                    {
                        //főcímsor
                        Főcím_kiírása(sor, sormagagasság, munkalap, a.Részegység, a.Utasítás_Cím);
                    }
                    else
                    {
                        MyX.Egyesít(munkalap, $"B{sor}:I{sor}");
                        MyX.Sortörésseltöbbsorba(munkalap, $"B{sor}:I{sor}", true);
                        MyX.Egyesít(munkalap, $"M{sor}:O{sor}");
                        MyX.Egyesít(munkalap, $"P{sor}:Q{sor}");
                        MyX.Kiir(a.Részegység.Trim() + ". " + a.Munka_utasítás_szám.Trim(), $"A{sor}");


                        if (Chk_paraméter.Checked && Chk_utasítás.Checked)
                        {
                            //Minden kiírás
                            Minden_kiírása(sor, a.Utasítás_Cím, a.Utasítás_leírás, a.Paraméter);
                        }
                        else if (Chk_paraméter.Checked && !Chk_utasítás.Checked)
                        {
                            //Paraméter
                            Minden_kiírása(sor, a.Utasítás_Cím, a.Utasítás_leírás, a.Paraméter);
                        }
                        else if (!Chk_paraméter.Checked && Chk_utasítás.Checked)
                        {
                            Utasítás_kiírása(sor, a.Utasítás_Cím, a.Utasítás_leírás);
                        }
                        else
                        {
                            //csak utasítás cím
                            Cím_kiírása(sor, a.Utasítás_Cím);
                        }
                        MyX.Sortörésseltöbbsorba(munkalap, $"B{sor}", true);
                        if (VÁLTAdatok.Count > 0)
                        {
                            string ideignév = Dolgozónév_kiíratása(VÁLTAdatok, a.ID, Személy);
                            ideignév = ideignév.Trim() != "_" ? ideignév : "";
                            MyX.Kiir(ideignév.Replace("_", "\n"), $"M{sor}");// kicseréljük a _-t sortörésre, hogy a cella magassága jó legyen.
                            MyX.Sortörésseltöbbsorba(munkalap, $"M{sor}", true);
                        }

                        MyX.Igazít_vízszintes(munkalap, $"B{sor}", "bal");
                        Adat_technológia_Ciklus cikluselelje = AdatokCiklus.Where(B => B.Sorszám == a.Karb_ciklus_eleje).FirstOrDefault();
                        if (cikluselelje != null) MyX.Kiir(cikluselelje.Fokozat, $"J{sor}");
                        MyX.Sormagasság(munkalap, $"{sor}:{sor}", -1);
                        MyX.Igazít_vízszintes(munkalap, $"J{sor}", "közép");
                        MyX.Rácsoz(munkalap, $"A{sor}:Q{sor}");
                    }
                }
                Holtart.Lép();
            }
            return sor;
        }

        private void Cím_kiírása(int sor, string utasítás_Cím)
        {
            MyX.Kiir(utasítás_Cím.Trim(), $"B{sor}");
            MyX.Betű(munkalap, $"B{sor}", BeBetűV);
        }

        private void Utasítás_kiírása(int sor, string utasítás_Cím, string utasítás_leírás)
        {
            //Utasítás szövege
            MyX.Kiir($"{utasítás_Cím.Trim()}\n{utasítás_leírás.Trim()}", $"B{sor}");
            //Vastag
            RichTextRun TextBe = new RichTextRun
            {
                Start = 0,
                Hossz = utasítás_Cím.Trim().Length,
                Vastag = true
            };
            Beállítás_CellaSzöveg BeCell = new Beállítás_CellaSzöveg
            {
                MunkalapNév = munkalap,
                Cella = $"B{sor}",
                FullText = $"{utasítás_Cím.Trim()}\n{utasítás_leírás.Trim()}",
                Betű = BeBetű,
                Beállítások = new List<RichTextRun> { TextBe }
            };
            MyX.Cella_Betű(BeCell);
        }

        private void Főcím_kiírása(int sor, int sormagasság, string munkalap, string Részegység, string Utasítás_Cím)
        {
            //főcímsor
            MyX.Egyesít(munkalap, $"B{sor}" + ":Q" + sor);
            MyX.Sormagasság(munkalap, $"{sor}:{sor}", sormagasság);
            MyX.Háttérszín(munkalap, $"A{sor}:Q{sor}", Color.YellowGreen);
            MyX.Kiir(Részegység.Trim(), $"A{sor}");
            MyX.Igazít_vízszintes(munkalap, $"A{sor}", "bal");
            MyX.Kiir(Utasítás_Cím.Trim(), $"B{sor}");
            MyX.Igazít_vízszintes(munkalap, $"B{sor}", "bal");
            MyX.Rácsoz(munkalap, $"A{sor}:Q{sor}");
        }

        private void Minden_kiírása(int sor, string Utasítás_Cím, string Utasítás_leírás, string Paraméter)
        {
            //Minden kiírás
            string szöveg;
            if (Paraméter.Trim() != "_")
                szöveg = $"{Utasítás_Cím.Trim()} \n{Utasítás_leírás.Trim()} \n{Paraméter} ";
            else
                szöveg = $"{Utasítás_Cím.Trim()} \n{Utasítás_leírás.Trim()}";

            MyX.Kiir(szöveg, $"B{sor}");
            RichTextRun TextBeV = new RichTextRun
            {
                Start = 0,
                Hossz = Utasítás_Cím.Trim().Length,
                Vastag = true
            };
            RichTextRun TextBeD = new RichTextRun
            {
                Start = ($"{Utasítás_Cím.Trim()} \n{Utasítás_leírás.Trim()}").Length - 1,
                Hossz = Paraméter.Trim().Length,
                Dőlt = true
            };
            if (Paraméter.Trim() != "_")
            {
                Beállítás_CellaSzöveg BeCell = new Beállítás_CellaSzöveg
                {
                    MunkalapNév = munkalap,
                    Cella = $"B{sor}",
                    FullText = $"{Utasítás_Cím.Trim()} \n{Utasítás_leírás.Trim()} \n{Paraméter} ",
                    Betű = BeBetű,
                    Beállítások = new List<RichTextRun> { TextBeV, TextBeD }
                };
                MyX.Cella_Betű(BeCell);
            }
            else
            {
                Beállítás_CellaSzöveg BeCell = new Beállítás_CellaSzöveg
                {
                    MunkalapNév = munkalap,
                    Cella = $"B{sor}",
                    FullText = $"{Utasítás_Cím.Trim()} \n{Utasítás_leírás.Trim()} ",
                    Betű = BeBetű,
                    Beállítások = new List<RichTextRun> { TextBeV }
                };
                MyX.Cella_Betű(BeCell);
            }
        }

        private string Dolgozónév_kiíratása(List<Adat_Technológia_Változat> VÁLTAdatok, long ID, Dictionary<string, string> Személy)
        {
            string ideigdolgozó = "";
            string Ideignév = "";

            Ideignév = (from a in VÁLTAdatok
                        where a.Technológia_Id == ID
                        select a.Végzi).FirstOrDefault();
            //Kiírjuk a változatnevet

            if (Ideignév != null)
            {
                List<string> Elem = (from a in Személy
                                     where a.Key.Contains(Ideignév.Trim())
                                     select a.Value).ToList();
                foreach (string item in Elem)
                {
                    if (ideigdolgozó.Trim() != "") ideigdolgozó += "\n\n";
                    ideigdolgozó += item.Trim();
                }
            }
            return ideigdolgozó;
        }

        private bool Ki_kell_írni(string Altípus, bool csoportos, List<Adat_Technológia_Kivételek> KivételAdatok)
        {
            bool válasz = false;
            if (CHKMinta.Checked) return true;
            if (Altípus.Trim() == "" || Altípus.Trim() == "_") return true; //alap beállítást mindig kiírjuk
            if (Altípus.Trim() != "" && KivételAdatok.Count == 0) return válasz; //Ha nincs kivétel akkor ki kell írni

            if (!csoportos)
            {  //ha volt altípus a kivétel listában akkor kiírjuk 
                List<Adat_Technológia_Kivételek> Szűrt = (from a in KivételAdatok
                                                          where a.Altípus == Altípus.Trim()
                                                          && a.Azonosító == Pályaszám.Text.Trim()
                                                          select a).ToList();
                if (Szűrt != null && Szűrt.Count != 0) válasz = true;
            }
            else
            {
                // csoportos esetén nem vizsgálunk, de vizsgáljuk
                if (AdatokKivételCsop.Contains(Altípus)) return true;
            }


            return válasz;
        }

        private void ExcelMunkalap()
        {
            MyX.Munkalap_betű(munkalap, BeBetű);
            MyX.Oszlopszélesség(munkalap, "A:A", 8);
            MyX.Oszlopszélesség(munkalap, "B:Q", 7);
            MyX.Oszlopszélesség(munkalap, "AS:AS", 70);
            MyX.Oszlopszélesség(munkalap, "AT:AT", 21);
        }

        private int Díszesblokk(int sor, string Verzió)
        {
            try
            {
                string Kép = $@"{Application.StartupPath}\Főmérnökség\Adatok\Ábrák\BKV.png";
                MyX.Kép_beillesztés(munkalap, "A1", Kép, 5, 5, 1, 1);

                sor++;
                MyX.Egyesít(munkalap, $"E{sor}:Q{sor}");
                MyX.Kiir("Budapesti Közlekedési Zártkörűen Működő Részvénytársaság", $"E{sor}");
                MyX.Betű(munkalap, $"E{sor}", BeBetűV);

                MyX.Igazít_vízszintes(munkalap, $"E{sor}", "jobb");

                sor++;
                MyX.Egyesít(munkalap, $"E{sor}:Q{sor}");
                MyX.Kiir("MEGELŐZŐ KARBANTARTÁS MUNKACSOMAG", $"E{sor}");
                MyX.Betű(munkalap, $"E{sor}", BeBetűVG);
                MyX.Igazít_vízszintes(munkalap, $"E{sor}", "jobb");
                sor++;
                MyX.Vastagkeret(munkalap, $"A1:Q{sor}");

                sor += 5;
                MyX.Vastagkeret(munkalap, $"A5:Q10");
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", sormagagasság);
                MyX.Egyesít(munkalap, $"A{sor}:D{sor}");
                MyX.Kiir("Km óra állás:", $"A{sor}");
                MyX.Betű(munkalap, $"A{sor}:Q{sor}", BeBetűVD);
                MyX.Vastagkeret(munkalap, $"A{sor}:D{sor}");

                MyX.Egyesít(munkalap, $"N{sor}:Q{sor}");
                MyX.Kiir("Verzió:", $"N{sor}");
                MyX.Betű(munkalap, $"A{sor}:Q{sor}", BeBetűVD);

                sor++;
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", sormagagasság);

                MyX.Rácsoz(munkalap, $"A{sor - 1}:D{sor}");
                MyX.Egyesít(munkalap, $"A{sor}:D{sor}");

                if (csoportos)
                {
                    //Beállítás_Ferde Beállítás = new Beállítás_Ferde { Terület = $"A{sor}:D{sor}", Munkalap = munkalap };
                    //MyX.FerdeVonal(Beállítás);
                    MyX.Kiir("-------------------------", $"A{sor}");
                }
                else
                    MyX.Kiir($"{KM_korr}", $"A{sor}");

                MyX.Egyesít(munkalap, $"N{sor}:Q{sor}");
                MyX.Kiir(Verzió, $"N{sor}");
                MyX.Betű(munkalap, $"A{sor}:Q{sor}", BeBetűVD);
                MyX.Rácsoz(munkalap, $"N{sor - 1}:Q{sor}");
                MyX.Rácsoz(munkalap, $"A{sor - 1}:D{sor}");

                sor++;
                Kép = $@"{Application.StartupPath}\Főmérnökség\Adatok\Ábrák\Villamos_{Járműtípus.Text.Trim()}.png";
                if (File.Exists(Kép)) MyX.Kép_beillesztés(munkalap, "F5", Kép, 300, 100, 0.4, 0.4);

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
            return sor;
        }

        public int FejlécÁltalános(int sor, long Sorszám = 0)
        {
            //Dátum fej
            MyX.Egyesít(munkalap, $"A{sor}:D{sor}");
            MyX.Egyesít(munkalap, $"E{sor}:H{sor}");
            MyX.Egyesít(munkalap, $"I{sor}:M{sor}");
            MyX.Egyesít(munkalap, $"N{sor}:Q{sor}");

            MyX.Kiir("Kezdő Dátum", $"A{sor}");
            MyX.Kiir("Befejező Dátum", $"E{sor}");
            MyX.Kiir("Rendelés Szám:", $"I{sor}");
            MyX.Kiir("Telephely", $"N{sor}");
            MyX.Betű(munkalap, $"A{sor}:Q{sor}", BeBetűVD);
            MyX.Sormagasság(munkalap, $"{sor}:{sor}", sormagagasság);
            sor++;
            MyX.Egyesít(munkalap, $"A{sor}:D{sor}");
            MyX.Egyesít(munkalap, $"E{sor}:H{sor}");
            MyX.Egyesít(munkalap, $"I{sor}:M{sor}");
            MyX.Egyesít(munkalap, $"N{sor}:Q{sor}");
            MyX.Kiir(Dátum.Value.ToString("yyyy.MM.dd"), $"A{sor}");

            //rendelési szám
            MyX.Kiir(Rendelés_Keresés(Sorszám), $"I{sor}");
            MyX.Kiir(Cmbtelephely.Text.Trim(), $"N{sor}");
            MyX.Sormagasság(munkalap, $"{sor}:{sor}", sormagagasság);
            MyX.Rácsoz(munkalap, $"A{sor - 1}:Q{sor}");
            return sor;
        }

        private int CsoportosPályaszámokÚj(int sor, string DolgNeve)
        {
            sor++;
            MyX.Egyesít(munkalap, $"A{sor}:H{sor}");
            MyX.Egyesít(munkalap, $"I{sor}:Q{sor}");
            MyX.Kiir(DolgNeve, $"A{sor}");
            MyX.Kiir(" Pályaszám(ok) melyeken elvégezte a karbantartást:", $"I{sor}");
            MyX.Rácsoz(munkalap, $"A{sor}:Q{sor}");
            MyX.Sormagasság(munkalap, $"{sor}:{sor}", sormagagasság);
            sor++;
            int soreleje = sor;
            int oszlop = 1;
            for (int i = 0; i < Pályaszám_TáblaAdatok.Count; i++)
            {
                MyX.Kiir(Pályaszám_TáblaAdatok[i].ToStrTrim(), MyF.Oszlopnév(oszlop) + $"{sor}");
                oszlop++;
                if (oszlop == 18)
                {
                    oszlop = 1;
                    sor++;
                }
            }
            MyX.Rácsoz(munkalap, $"A{soreleje}:Q{sor}");
            MyX.Sormagasság(munkalap, $"{soreleje}:{sor}", sormagagasság);
            return sor;
        }

        private int Fejlécspec(int sor)
        {
            sor++;
            MyX.Egyesít(munkalap, $"A{sor}:E{sor}");
            MyX.Egyesít(munkalap, $"F{sor}:L{sor}");
            MyX.Egyesít(munkalap, $"M{sor}:Q{sor}");

            //Feltétel mező
            if (!csoportos)
            {
                MyX.Kiir($"Pályaszám:{Pályaszám.Text.Trim()}", $"A{sor}");
                MyX.Betű(munkalap, $"A{sor}", BeBetűV);
            }
            string szöveg = Járműtípus.Text.Trim();
            if (Járműtípus.Text.Trim().Length > 15) szöveg += "\n";
            szöveg += $" - {Combo_KarbCiklus.Text.Trim()} Karbantartási munkalap";

            MyX.Kiir(szöveg, $"F{sor}");
            MyX.Betű(munkalap, $"F{sor}", BeBetűV);

            MyX.Kiir($"Készítve: {DateTime.Now}", $"M{sor}");
            MyX.Betű(munkalap, $"M{sor}", BeBetűD);

            MyX.Sormagasság(munkalap, $"{sor}:{sor}", sormagagasság);
            MyX.Rácsoz(munkalap, $"A{sor}:Q{sor}");
            return sor;
        }

        private string Rendelés_Keresés(long Sorszám, string Azonosító = "")
        {
            string válasz = "";
            try
            {
                if (Sorszám == 0)
                {
                    Adat_Technológia_Rendelés Elem = (from a in AdatokRendelés
                                                      where a.Év == Dátum.Value.Year && a.Technológia_típus == Járműtípus.Text.Trim()
                                                      && a.Karbantartási_fokozat == Combo_KarbCiklus.Text.Trim()
                                                      select a).FirstOrDefault();
                    if (Elem != null) válasz = Elem.Rendelésiszám;
                    switch (válasz)
                    {
                        case "T5C5":
                            List<Adat_Vezénylés> Adatok = KézVezénylés.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);

                            Adat_Vezénylés Adat = (from a in Adatok
                                                   where a.Dátum == Dátum.Value && a.Törlés == 0 && a.Vizsgálat == Combo_KarbCiklus.Text.Trim() && a.Azonosító == Pályaszám.Text.Trim()
                                                   select a).FirstOrDefault();
                            if (Adat != null)
                                válasz = Adat.Rendelésiszám;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    List<Adat_DigitálisMunkalap_Kocsik> Adatok = KézDigKocsi.Lista_Adatok();
                    Adatok = Adatok.Where(a => a.Fej_Id == Sorszám).ToList();
                    Adat_DigitálisMunkalap_Kocsik Adat = null;
                    if (Azonosító.Trim() == "")
                        Adat = Adatok.FirstOrDefault();
                    else
                        Adat = Adatok.Where(a => a.Azonosító == Azonosító).FirstOrDefault();
                    if (Adat != null) válasz = Adat.Rendelés;
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
            return válasz;
        }

        #endregion

        #region Digi
        private void DigiMentés(long Sorszám)
        {
            try
            {
                Kezelő_T5C5_Kmadatok KézKM = new Kezelő_T5C5_Kmadatok("T5C5");
                List<Adat_T5C5_Kmadatok> AdatokKmAdatok = KézKM.Lista_Adatok();
                List<Adat_DigitálisMunkalap_Kocsik> AdatokDigiKocsik = new List<Adat_DigitálisMunkalap_Kocsik>();
                List<Adat_DigitálisMunkalap_Dolgozó> AdatokDigiDolgozó = new List<Adat_DigitálisMunkalap_Dolgozó>();

                string[] darabol = Kiadta.Text.Split('-');
                string[] darabol2 = darabol[0].Split('_');
                Adat_DigitálisMunkalap_Fej ADATDigiFej = new Adat_DigitálisMunkalap_Fej(
                                        Sorszám,
                                        Járműtípus.Text.Trim(),
                                        Combo_KarbCiklus.Text.Trim(),
                                        darabol2[0],
                                        darabol2[1],
                                        Cmbtelephely.Text.Trim(),
                                        Dátum.Value
                                        );
                KézDigFej.Rögzítés(ADATDigiFej);


                foreach (string azonosító in Pályaszám_TáblaAdatok)
                {
                    Adat_T5C5_Kmadatok Adatkm = AdatokKmAdatok.Where(a => a.Azonosító == azonosító).FirstOrDefault();
                    long KMU = 0;
                    if (Adatkm != null) KMU = Adatkm.KMUkm;

                    string rendelés = Rendelés_Keresés(0);

                    Adat_DigitálisMunkalap_Kocsik AdatKocsik = new Adat_DigitálisMunkalap_Kocsik(
                                            Sorszám,
                                            azonosító,
                                            KMU,
                                            rendelés);
                    AdatokDigiKocsik.Add(AdatKocsik);
                }
                KézDigKocsi.Rögzítés(AdatokDigiKocsik);


                //Változatok
                AdatokVáltozat = MyLista.VáltozatLista(Járműtípus.Text.Trim(), Cmbtelephely.Text.Trim());
                List<Adat_Technológia_Változat> VÁLTAdatok = (from a in AdatokVáltozat
                                                              where a.Változatnév == Munkalap_Változatnév.Text.Trim()
                                                              select a).ToList();

                //pályaszám kivételei
                AdatokKivétel = MyLista.KivételekLista(Járműtípus.Text.Trim());
                AdatokKivételCsop = CsoportosKivételek();

                AdatokCiklus = MyLista.KarbCiklusLista(Járműtípus.Text.Trim());
                Adat_technológia_Ciklus AdatCikk = (from a in AdatokCiklus
                                                    where a.Fokozat == Combo_KarbCiklus.Text.Trim()
                                                    select a).FirstOrDefault();

                AdatokTechnológia = MyLista.TechnológiaLista(Járműtípus.Text.Trim());
                List<Adat_Technológia_Új> Adatok = (from a in AdatokTechnológia
                                                    where a.Karb_ciklus_eleje <= AdatCikk.Sorszám && a.Karb_ciklus_vége >= AdatCikk.Sorszám
                                                    && a.Érv_kezdete <= Dátum.Value && a.Érv_vége >= Dátum.Value
                                                    orderby a.Részegység, a.Munka_utasítás_szám, a.ID
                                                    select a).ToList();

                //munkalap érdemi része
                foreach (Adat_Technológia_Új Rekorda in Adatok)
                {
                    //Ha speciális, akkor kiírja különben kihagy
                    if (Ki_kell_írni(Rekorda.Altípus, csoportos, AdatokKivétel))
                    {
                        string dolgozónév = "";
                        string dolgozószám = "";

                        if (VÁLTAdatok.Count > 0)
                        {
                            string Ideignév = (from b in VÁLTAdatok
                                               where b.Technológia_Id == Rekorda.ID
                                               select b.Végzi).FirstOrDefault();
                            if (Ideignév != null)
                            {
                                List<string> Elem = (from a in Személy
                                                     where a.Key.Contains(Ideignév.Trim())
                                                     select a.Value).ToList();
                                foreach (string item in Elem)
                                {
                                    string[] Darabol = item.Split('_');
                                    Adat_DigitálisMunkalap_Dolgozó ADATDOLGOZÓ = new Adat_DigitálisMunkalap_Dolgozó(
                                                                     Darabol[0].Trim(),
                                                                     Darabol[1].Trim(),
                                                                     Sorszám,
                                                                     Rekorda.ID);
                                    AdatokDigiDolgozó.Add(ADATDOLGOZÓ);
                                }
                                if (Elem.Count == 0)
                                {
                                    Adat_DigitálisMunkalap_Dolgozó ADATDOLGOZÓ = new Adat_DigitálisMunkalap_Dolgozó(
                                                                      dolgozónév,
                                                                      dolgozószám,
                                                                      Sorszám,
                                                                      Rekorda.ID);
                                    AdatokDigiDolgozó.Add(ADATDOLGOZÓ);
                                }
                            }
                        }
                        else
                        {
                            Adat_DigitálisMunkalap_Dolgozó ADATDOLGOZÓ = new Adat_DigitálisMunkalap_Dolgozó(
                                  dolgozónév,
                                  dolgozószám,
                                  Sorszám,
                                  Rekorda.ID);
                            AdatokDigiDolgozó.Add(ADATDOLGOZÓ);
                        }
                    }
                }
                KézDigDolg.Rögzítés(AdatokDigiDolgozó);
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

        private void Digitális_Click(object sender, EventArgs e)
        {
            try
            {
                if (Combo_KarbCiklus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy ciklus fokozat sem!");
                if (Járműtípus.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve egy járműtípus sem!");
                if (Pályaszám_TáblaAdatok.Count < 1) throw new HibásBevittAdat("Nincs a táblázatba felvéve egy pályaszám sem!");
                if (Kiadta.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve az ellenőrző személy!");

                long Sorszám = KézDigFej.Sorszám();
                if (NapiSorszám == -1) NapiSorszám = Sorszám;
                if (Combo_KarbCiklus.Text.Trim() == "E1" || Combo_KarbCiklus.Text.Trim() == "E2")
                    DigiMentés(Sorszám);
                else
                    DigiMentéstöbbi(Sorszám);

                MessageBox.Show($"Az adatok mentése elkészült", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void DigiMentéstöbbi(long Sorszám)
        {
            try
            {
                Kezelő_T5C5_Kmadatok KézKM = new Kezelő_T5C5_Kmadatok("T5C5");
                List<Adat_T5C5_Kmadatok> AdatokKmAdatok = KézKM.Lista_Adatok();
                List<Adat_DigitálisMunkalap_Kocsik> AdatokDigiKocsik = new List<Adat_DigitálisMunkalap_Kocsik>();
                List<Adat_DigitálisMunkalap_Dolgozó> AdatokDigiDolgozó = new List<Adat_DigitálisMunkalap_Dolgozó>();

                foreach (string azonosító in Pályaszám_TáblaAdatok)
                {
                    //Fejadatok
                    string[] darabol = Kiadta.Text.Split('-');
                    string[] darabol2 = darabol[0].Split('_');
                    Adat_DigitálisMunkalap_Fej ADATDigiFej = new Adat_DigitálisMunkalap_Fej(
                                            Sorszám,
                                            Járműtípus.Text.Trim(),
                                            Combo_KarbCiklus.Text.Trim(),
                                            darabol2[0],
                                            darabol2[1],
                                            Cmbtelephely.Text.Trim(),
                                            Dátum.Value
                                            );
                    KézDigFej.Rögzítés(ADATDigiFej);

                    //Kocsik
                    AdatokDigiKocsik.Clear();
                    Adat_T5C5_Kmadatok Adatkm = AdatokKmAdatok.Where(a => a.Azonosító == azonosító).FirstOrDefault();
                    long KMU = 0;
                    if (Adatkm != null) KMU = Adatkm.KMUkm;

                    string rendelés = Rendelés_Keresés(0);

                    Adat_DigitálisMunkalap_Kocsik AdatKocsik = new Adat_DigitálisMunkalap_Kocsik(
                                            Sorszám,
                                            azonosító,
                                            KMU,
                                            rendelés);
                    AdatokDigiKocsik.Add(AdatKocsik);

                    KézDigKocsi.Rögzítés(AdatokDigiKocsik);


                    //Változatok
                    AdatokVáltozat = MyLista.VáltozatLista(Járműtípus.Text.Trim(), Cmbtelephely.Text.Trim());
                    List<Adat_Technológia_Változat> VÁLTAdatok = (from a in AdatokVáltozat
                                                                  where a.Változatnév == Munkalap_Változatnév.Text.Trim()
                                                                  select a).ToList();

                    //pályaszám kivételei
                    AdatokKivétel = MyLista.KivételekLista(Járműtípus.Text.Trim());
                    AdatokKivételCsop = CsoportosKivételek();

                    AdatokCiklus = MyLista.KarbCiklusLista(Járműtípus.Text.Trim());
                    Adat_technológia_Ciklus AdatCikk = (from a in AdatokCiklus
                                                        where a.Fokozat == Combo_KarbCiklus.Text.Trim()
                                                        select a).FirstOrDefault();

                    AdatokTechnológia = MyLista.TechnológiaLista(Járműtípus.Text.Trim());
                    List<Adat_Technológia_Új> Adatok = (from a in AdatokTechnológia
                                                        where a.Karb_ciklus_eleje <= AdatCikk.Sorszám && a.Karb_ciklus_vége >= AdatCikk.Sorszám
                                                        && a.Érv_kezdete <= Dátum.Value && a.Érv_vége >= Dátum.Value
                                                        orderby a.Részegység, a.Munka_utasítás_szám, a.ID
                                                        select a).ToList();
                    AdatokDigiDolgozó.Clear();
                    //munkalap érdemi része
                    foreach (Adat_Technológia_Új Rekorda in Adatok)
                    {
                        //Ha speciális, akkor kiírja különben kihagy
                        if (Ki_kell_írni(Rekorda.Altípus, csoportos, AdatokKivétel))
                        {
                            string dolgozónév = "";
                            string dolgozószám = "";

                            if (VÁLTAdatok.Count > 0)
                            {
                                string Ideignév = (from b in VÁLTAdatok
                                                   where b.Technológia_Id == Rekorda.ID
                                                   select b.Végzi).FirstOrDefault();
                                if (Ideignév != null)
                                {
                                    List<string> Elem = (from a in Személy
                                                         where a.Key.Contains(Ideignév.Trim())
                                                         select a.Value).ToList();
                                    foreach (string item in Elem)
                                    {
                                        string[] Darabol = item.Split('_');
                                        Adat_DigitálisMunkalap_Dolgozó ADATDOLGOZÓ = new Adat_DigitálisMunkalap_Dolgozó(
                                                                         Darabol[0].Trim(),
                                                                         Darabol[1].Trim(),
                                                                         Sorszám,
                                                                         Rekorda.ID);
                                        AdatokDigiDolgozó.Add(ADATDOLGOZÓ);
                                    }
                                    if (Elem.Count == 0)
                                    {
                                        Adat_DigitálisMunkalap_Dolgozó ADATDOLGOZÓ = new Adat_DigitálisMunkalap_Dolgozó(
                                                                          dolgozónév,
                                                                          dolgozószám,
                                                                          Sorszám,
                                                                          Rekorda.ID);
                                        AdatokDigiDolgozó.Add(ADATDOLGOZÓ);
                                    }
                                }
                            }
                            else
                            {
                                Adat_DigitálisMunkalap_Dolgozó ADATDOLGOZÓ = new Adat_DigitálisMunkalap_Dolgozó(
                                      dolgozónév,
                                      dolgozószám,
                                      Sorszám,
                                      Rekorda.ID);
                                AdatokDigiDolgozó.Add(ADATDOLGOZÓ);
                            }
                        }
                    }
                    KézDigDolg.Rögzítés(AdatokDigiDolgozó);
                    Sorszám++;
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


        private void FelExcel_Click(object sender, EventArgs e)
        {
            if (NapiSorszám == -1) return;
            DigiFej();
            DigiKocsi();
            DigiDolgozó();
            MessageBox.Show($"Az Exceltáblák elkészültek", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            NapiSorszám = -1;
        }

        private void DigiFej()
        {
            Holtart.Be();
            List<Adat_DigitálisMunkalap_Fej> AdatokFej = KézDigFej.Lista_Adatok().Where(a => a.Id >= NapiSorszám).ToList();
            string könyvtár = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Clear();
            dataTable.Columns.Add("Azonosító");
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Típus");
            dataTable.Columns.Add("Karbantartási fokozat");
            dataTable.Columns.Add("Ellenőrző Dolgozó Név");
            dataTable.Columns.Add("Ellenőrző Dolgozószám");
            dataTable.Columns.Add("Telephely");
            dataTable.Columns.Add("Dátum");

            foreach (Adat_DigitálisMunkalap_Fej rekord in AdatokFej)
            {
                DataRow Soradat = dataTable.NewRow();
                Soradat["Azonosító"] = rekord.Id;
                Soradat["ID"] = rekord.Id;
                Soradat["Típus"] = rekord.Típus;
                Soradat["Karbantartási fokozat"] = rekord.Karbantartási_fokozat;
                Soradat["Ellenőrző Dolgozó Név"] = rekord.EllDolgozóNév;
                Soradat["Ellenőrző Dolgozószám"] = rekord.EllDolgozószám;
                Soradat["Telephely"] = rekord.Telephely;
                Soradat["Dátum"] = rekord.Dátum;
                dataTable.Rows.Add(Soradat);
                Holtart.Lép();
            }
            string hely = $@"{könyvtár}\Munkautasítás.xlsx";
            MyX.DataTableToXML(hely, dataTable);
            Holtart.Ki();
        }

        private void DigiKocsi()
        {
            Holtart.Be();
            string könyvtár = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            List<Adat_DigitálisMunkalap_Kocsik> AdatokKocsik = KézDigKocsi.Lista_Adatok().Where(a => a.Fej_Id >= NapiSorszám).ToList();
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            dataTable.Columns.Clear();
            dataTable.Columns.Add("_Azonosító");
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Azonosító");
            dataTable.Columns.Add("KMU");
            dataTable.Columns.Add("Rendelés");

            foreach (Adat_DigitálisMunkalap_Kocsik rekord in AdatokKocsik)
            {
                DataRow Soradat = dataTable.NewRow();
                Soradat["_Azonosító"] = rekord.Fej_Id;
                Soradat["ID"] = rekord.Fej_Id;
                Soradat["Azonosító"] = rekord.Azonosító;
                Soradat["KMU"] = rekord.KMU;
                Soradat["Rendelés"] = rekord.Rendelés;

                dataTable.Rows.Add(Soradat);
                Holtart.Lép();
            }
            string hely = $@"{könyvtár}\Kocsi.xlsx";
            MyX.DataTableToXML(hely, dataTable);
            Holtart.Ki();
        }

        private void DigiDolgozó()
        {
            Holtart.Be();
            string könyvtár = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);


            List<Adat_DigitálisMunkalap_Dolgozó> AdatokDolgozó = KézDigDolg.Lista_Adatok().Where(a => a.Fej_Id >= NapiSorszám).ToList();
            DataTable dataTable = new DataTable();
            dataTable.Clear();
            dataTable.Columns.Clear();
            dataTable.Columns.Add("Azonosító");
            dataTable.Columns.Add("Dolgozó Név");
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Dolgozószám");
            dataTable.Columns.Add("Technológia ID");

            foreach (Adat_DigitálisMunkalap_Dolgozó rekord in AdatokDolgozó)
            {
                DataRow Soradat = dataTable.NewRow();
                Soradat["Azonosító"] = rekord.Fej_Id;
                Soradat["Dolgozó Név"] = rekord.DolgozóNév;
                Soradat["ID"] = rekord.Fej_Id;
                Soradat["Dolgozószám"] = rekord.Dolgozószám;
                Soradat["Technológia ID"] = rekord.Technológia_Id;

                dataTable.Rows.Add(Soradat);
                Holtart.Lép();
            }
            string hely = $@"{könyvtár}\Dolgozó.xlsx";
            MyX.DataTableToXML(hely, dataTable);
            Holtart.Ki();
            Holtart.Ki();
        }
        #endregion


        #region PDF munkalap



        private void PDF_tábla(string fájlexc)
        {

            try
            {
                Adat_technológia_Ciklus AdatCikk = (from a in AdatokCiklus
                                                    where a.Fokozat == Combo_KarbCiklus.Text.Trim()
                                                    select a).FirstOrDefault();

                Holtart.Be(25, MyColor.ColorToHex(Color.DeepSkyBlue));

                //Változatok
                List<Adat_Technológia_Változat> VÁLTAdatok = (from a in AdatokVáltozat
                                                              where a.Változatnév == Munkalap_Változatnév.Text.Trim()
                                                              select a).ToList();

                //pályaszám kivételei
                AdatokKivétel = MyLista.KivételekLista(Járműtípus.Text.Trim());
                AdatokKivételCsop = CsoportosKivételek();


                List<Adat_Technológia_Új> Adatok = (from a in AdatokTechnológia
                                                    where a.Karb_ciklus_eleje <= AdatCikk.Sorszám && a.Karb_ciklus_vége >= AdatCikk.Sorszám
                                                    && a.Érv_kezdete <= Dátum.Value && a.Érv_vége >= Dátum.Value
                                                    orderby a.Részegység, a.Munka_utasítás_szám, a.ID
                                                    select a).ToList();
                KM_korr = 0;
                //Egyedi munkalapokon kiírja a km adatokat
                if (CHKKMU.Checked && !csoportos)
                {
                    //KMU érték
                    List<Adat_T5C5_Kmadatok> KmAdatok = KézKM.Lista_Adatok();
                    Adat_T5C5_Kmadatok EgyKmAdat = (from a in KmAdatok
                                                    where a.Azonosító == Pályaszám.Text.Trim()
                                                    orderby a.Vizsgdátumk descending
                                                    select a).FirstOrDefault();
                    KM_korr = 0;
                    if (EgyKmAdat != null) KM_korr = EgyKmAdat.KMUkm;

                    //KMU korrekció
                    List<Adat_Főkönyv_Zser_Km> AdatokZSER = KézZser.Lista_adatok(Dátum.Value.Year);
                    if (Dátum.Value.Month < 4)
                    {
                        List<Adat_Főkönyv_Zser_Km> AdatokZSERelőző = KézZser.Lista_adatok(Dátum.Value.Year - 1);
                        AdatokZSER.AddRange(AdatokZSERelőző);
                    }


                    if (AdatokZSER != null && EgyKmAdat != null)
                    {
                        List<Adat_Főkönyv_Zser_Km> KorNapikmLista = (from a in AdatokZSER
                                                                     where a.Azonosító == Pályaszám.Text.Trim() && a.Dátum > EgyKmAdat.KMUdátum
                                                                     select a).ToList();
                        long KorNapikm = 0;
                        if (KorNapikmLista != null)
                            KorNapikm = KorNapikmLista.Sum(a => a.Napikm);
                        KM_korr += KorNapikm;
                    }
                }


                //legkisebb dátum
                DateTime hatályos = Adatok.Min(a => a.Érv_kezdete);
                string hatályos_str = $"Hatálybalépés dátuma:{hatályos:yyyy.MM.dd}";

                string Verzió = $"{Járműtípus.Text.Trim()}_{Combo_KarbCiklus.Text.Trim()}_{AdatCikk.Verzió}";
                if (Járműtípus.Text.Trim().Length > 15) Verzió = $"{Járműtípus.Text.Trim()}\n_{Combo_KarbCiklus.Text.Trim()}_{AdatCikk.Verzió}";


                using (MemoryStream ms = new MemoryStream())
                {
                    using (Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 15f, 15f))
                    {
                        using (PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms))
                        {
                            writer.PageEvent = new CustomFooter(hatályos_str, Verzió);
                            pdfDoc.Open();
                            PdfPTable Tábla = BKVfejléc();
                            Tábla.WidthPercentage = 100;
                            pdfDoc.Add(Tábla);

                            Tábla = Kmóraállás(Verzió, KM_korr);
                            Tábla.WidthPercentage = 100;
                            pdfDoc.Add(Tábla);

                            Tábla = DátumTábla();
                            Tábla.WidthPercentage = 100;
                            pdfDoc.Add(Tábla);

                            if (csoportos)
                            {
                                foreach (string dolgnév in Személy.OrderBy(a => a.Value).Select(a => a.Value).Distinct())
                                {
                                    Tábla = PályaszámokCsoportos(dolgnév);
                                    Tábla.WidthPercentage = 100;
                                    pdfDoc.Add(Tábla);
                                }
                            }

                            Tábla = Tartalom(Adatok, AdatokKivétel, VÁLTAdatok);
                            Tábla.WidthPercentage = 100;
                            pdfDoc.Add(Tábla);

                            if (Chk_hibássorok.Checked)
                            {
                                Tábla = Javítások();
                                Tábla.WidthPercentage = 100;
                                pdfDoc.Add(Tábla);
                            }
                            if (Chk_szerszám.Checked == true)
                            {
                                Tábla = Szerszámok();
                                Tábla.WidthPercentage = 100;
                                pdfDoc.Add(Tábla);
                            }
                            Tábla = Megjegyzés();
                            Tábla.WidthPercentage = 100;
                            pdfDoc.Add(Tábla);

                            Tábla = Aláírás();
                            Tábla.WidthPercentage = 100;
                            pdfDoc.Add(Tábla);
                            pdfDoc.Close();
                        }
                    }
                    bytes = ms.ToArray();
                }


                System.IO.File.WriteAllBytes(fájlexc, bytes);

                Holtart.Ki();
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

        private PdfPTable PályaszámokCsoportos(string dolgozónév)
        {
            PdfPTable Válasz = new PdfPTable(17);
            try
            {
                Válasz.WidthPercentage = 100;
                PdfPCell ECell = MyPDF.Cella(MyPDF.Kiírás(dolgozónév, "N"));
                ECell.Colspan = 8;
                Válasz.AddCell(ECell);

                ECell = MyPDF.Cella(MyPDF.Kiírás(" Pályaszám(ok) melyeken elvégezte a karbantartást:", "N"));
                ECell.Colspan = 9;
                Válasz.AddCell(ECell);


                int j = 1;
                for (int i = 0; i < Pályaszám_TáblaAdatok.Count; i++)
                {
                    Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(Pályaszám_TáblaAdatok[i].ToStrTrim(), "N", 10f, 1, 20f)));
                    j++;
                    if (j > 17) j = 1;

                }
                for (int i = 0; i < 18 - j; i++)
                {
                    Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(" ", "N", 10f, 1, 20f)));
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
            return Válasz;
        }

        private PdfPTable DátumTábla(long Sorszám = 0)
        {
            PdfPTable Válasz = new PdfPTable(4);
            try
            {
                Válasz.WidthPercentage = 100;
                Válasz.SetWidths(new float[] { 1, 1, 1, 1 });

                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás("Kezdő Dátum", "VD")));
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás("Befejező Dátum", "VD")));
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás("Rendelés Szám:", "VD")));
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás("Telephely", "VD")));

                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(Dátum.Value.ToString("yyyy.MM.dd"))));
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(" ", "N", 10f, 1, 20f)));
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(Rendelés_Keresés(Sorszám))));
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(Cmbtelephely.Text.Trim())));

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
            return Válasz;
        }

        private PdfPTable Kmóraállás(string verzió, long kM_korr)
        {
            PdfPTable Válasz = new PdfPTable(1);
            try
            {
                PdfPTable pdfTable = new PdfPTable(2)
                {
                    WidthPercentage = 100
                };
                pdfTable.SetWidths(new float[] { 1, 3 });

                //Kép
                iTextSharp.text.Image Kép = iTextSharp.text.Image.GetInstance($@"{Application.StartupPath}\Főmérnökség\Adatok\Ábrák\Villamos_{Járműtípus.Text.Trim()}.png");
                Kép.ScaleToFit(200, 500);
                PdfPCell imageCell = new PdfPCell(Kép)
                {
                    Border = PdfPCell.NO_BORDER,
                    PaddingLeft = 5, // Move image 5 points to the jobbra
                    PaddingTop = 2, // Move image 2 points le
                    HorizontalAlignment = PdfPCell.ALIGN_CENTER,
                    VerticalAlignment = PdfPCell.ALIGN_CENTER
                };

                PdfPTable Tábla = new PdfPTable(1);
                Tábla.AddCell(MyPDF.Cella(MyPDF.Kiírás("Km óra állás:", "VD")));
                Tábla.AddCell(MyPDF.Cella(MyPDF.Kiírás(kM_korr.ToString(), "VD")));
                Tábla.AddCell(MyPDF.Cella(MyPDF.Kiírás("Verzió:", "VD"), true));
                Tábla.AddCell(MyPDF.Cella(MyPDF.Kiírás(verzió, "VD"), true));

                PdfPCell TáblaCell = new PdfPCell(Tábla)
                {
                    Border = PdfPCell.NO_BORDER
                };
                pdfTable.AddCell(TáblaCell);
                pdfTable.AddCell(imageCell);

                Válasz.AddCell(pdfTable);
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
            return Válasz;
        }

        private PdfPTable BKVfejléc()
        {
            PdfPTable Válasz = new PdfPTable(1);
            try
            {
                PdfPTable pdfTable = new PdfPTable(2)
                {
                    WidthPercentage = 100
                };
                pdfTable.SetWidths(new float[] { 1, 2 });

                //Kép
                iTextSharp.text.Image Kép = iTextSharp.text.Image.GetInstance($@"{Application.StartupPath}\Főmérnökség\Adatok\Ábrák\BKV.png");
                Kép.ScaleToFit(100, 250);
                PdfPCell imageCell = new PdfPCell(Kép)
                {
                    Border = PdfPCell.NO_BORDER,
                    PaddingLeft = 5, // Move image 5 points to the jobbra
                    PaddingTop = 2 // Move image 2 points le
                };
                pdfTable.AddCell(imageCell);


                //Szöveg jobbra igazítva
                PdfPCell textCell = new PdfPCell
                {
                    Border = PdfPCell.NO_BORDER
                };

                // Betűtípus az adott cella szövegszínével
                // Betűtípus betöltése (Arial, Unicode támogatás)
                string betűtípus = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                BaseFont alapFont = BaseFont.CreateFont(betűtípus, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                iTextSharp.text.Font betűvastagFekete = new iTextSharp.text.Font(alapFont, 10f, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                iTextSharp.text.Font betűvastagZöld = new iTextSharp.text.Font(alapFont, 10f, iTextSharp.text.Font.BOLD, BaseColor.GREEN);
                string szöveg = "Budapesti Közlekedési Zártkörűen Működő Részvénytársaság";
                string szöveg1 = "MEGELŐZŐ KARBANTARTÁS MUNKACSOMAG";
                Paragraph p1 = new Paragraph(szöveg, betűvastagFekete)
                {
                    Alignment = Element.ALIGN_RIGHT
                };
                Paragraph p2 = new Paragraph(szöveg1, betűvastagZöld)
                {
                    Alignment = Element.ALIGN_RIGHT
                };

                textCell.AddElement(p1);
                textCell.AddElement(p2);
                pdfTable.AddCell(textCell);

                Válasz.AddCell(pdfTable);
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
            return Válasz;
        }

        private PdfPTable Tartalom(List<Adat_Technológia_Új> Adatok, List<Adat_Technológia_Kivételek> KivételAdatok,
                  List<Adat_Technológia_Változat> VÁLTAdatok)
        {
            PdfPTable Válasz = new PdfPTable(8);
            try
            {
                Válasz.WidthPercentage = 100;
                Válasz.SetWidths(new float[] { 15f, 47f, 47f, 11f, 11f, 11f, 35f, 25f });

                string szöveg = Járműtípus.Text.Trim();
                if (Járműtípus.Text.Trim().Length > 15) szöveg += "\n";
                szöveg += $" - {Combo_KarbCiklus.Text.Trim()} Karbantartási munkalap";
                //Nulladik sor
                PdfPCell ECell;
                if (csoportos)
                    ECell = MyPDF.Cella(MyPDF.Kiírás($" ", "V"));
                else
                    ECell = MyPDF.Cella(MyPDF.Kiírás($"Pályaszám:{Pályaszám.Text.Trim()}", "V"));
                ECell.Colspan = 2;
                Válasz.AddCell(ECell);

                ECell = MyPDF.Cella(MyPDF.Kiírás(szöveg, "V"));
                ECell.Colspan = 4;
                Válasz.AddCell(ECell);

                ECell = MyPDF.Cella(MyPDF.Kiírás($"Készítve: {DateTime.Now}", "D"));
                ECell.Colspan = 2;
                Válasz.AddCell(ECell);


                //Munkalap fejléc kiírása  első sor
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás("Nr.", "V"), true, false, true, "LIGHT_GRAY"));
                ECell = MyPDF.Cella(MyPDF.Kiírás("MUNKAUTASÍTÁS LEÍRÁSA", "V"), true, false, true, "LIGHT_GRAY");
                ECell.Colspan = 2;
                Válasz.AddCell(ECell);
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás("Karb.", "V"), true, false, true, "LIGHT_GRAY"));

                PdfPCell mergedCell = MyPDF.Cella(MyPDF.Kiírás("Státusz** ", "V"), true, true, true, "LIGHT_GRAY");
                mergedCell.Colspan = 2;
                Válasz.AddCell(mergedCell);

                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás("Utasítást", "V"), true, false, true, "LIGHT_GRAY"));
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás("Aláírás", "V"), true, false, true, "LIGHT_GRAY"));

                //Második sor
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(" ", "V"), true, true, false, "LIGHT_GRAY"));
                ECell = MyPDF.Cella(MyPDF.Kiírás(" ", "V"), true, true, false, "LIGHT_GRAY");
                ECell.Colspan = 2;
                Válasz.AddCell(ECell);
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(" Cikl.", "V"), true, true, false, "LIGHT_GRAY"));
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás("OK", "V"), true, true, true, "LIGHT_GRAY"));
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás("Jav.*", "V"), true, true, true, "LIGHT_GRAY"));
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás("Végrehajtotta***", "V"), true, true, false, "LIGHT_GRAY"));
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(" ", "V"), true, true, false, "LIGHT_GRAY"));
                Válasz.HeaderRows = 3;

                Holtart.Be(Adatok.Count + 2, MyColor.ColorToHex(Color.Orange));

                //munkalap érdemi része
                foreach (Adat_Technológia_Új a in Adatok)
                {
                    //Ha speciális, akkor kiírja különben kihagy
                    if (Ki_kell_írni(a.Altípus, csoportos, KivételAdatok))
                    {
                        sor++;
                        if (a.Munka_utasítás_szám.Trim() == "0")
                        {
                            //főcímsor
                            //    Főcím_kiírása(sor, sormagagasság, munkalap, a.Részegység, a.Utasítás_Cím);
                        }
                        else
                        {
                            Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(a.Részegység.Trim() + ". " + a.Munka_utasítás_szám.Trim(), "N")));

                            Paragraph Cím = MyPDF.Kiírás(a.Utasítás_Cím, "V", 10, 0);
                            Paragraph Leírás = MyPDF.Kiírás(a.Utasítás_leírás, "N", 10, 0);
                            Paragraph Paraméter = MyPDF.Kiírás(a.Paraméter, "D", 10, 0);
                            PdfPCell Egyesít = new PdfPCell();
                            if (Chk_paraméter.Checked && Chk_utasítás.Checked)
                            {
                                //Minden kiírás
                                if (a.Utasítás_Cím.Trim() != "_") Egyesít.AddElement(Cím);
                                if (a.Utasítás_leírás.Trim() != "_") Egyesít.AddElement(Leírás);
                                if (a.Paraméter.Trim() != "_") Egyesít.AddElement(Paraméter);

                                Egyesít.Colspan = 2;
                                Válasz.AddCell(Egyesít);
                            }
                            else if (Chk_paraméter.Checked && !Chk_utasítás.Checked)
                            {
                                if (a.Utasítás_Cím.Trim() != "_") Egyesít.AddElement(Cím);
                                if (a.Paraméter.Trim() != "_") Egyesít.AddElement(Paraméter);
                                Egyesít.Colspan = 2;
                                Válasz.AddCell(Egyesít);
                            }
                            else if (!Chk_paraméter.Checked && Chk_utasítás.Checked)
                            {
                                if (a.Utasítás_Cím.Trim() != "_") Egyesít.AddElement(Cím);
                                if (a.Utasítás_leírás.Trim() != "_") Egyesít.AddElement(Leírás);
                                Egyesít.Colspan = 2;
                                Válasz.AddCell(Egyesít);
                            }
                            else
                            {
                                if (a.Utasítás_Cím.Trim() != "_") Egyesít.AddElement(Cím);
                                Egyesít.Colspan = 2;
                                Válasz.AddCell(Egyesít);
                            }

                            Adat_technológia_Ciklus cikluselelje = AdatokCiklus.Where(B => B.Sorszám == a.Karb_ciklus_eleje).FirstOrDefault();
                            Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(cikluselelje.Fokozat, "N")));
                            Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(" ", "N")));
                            Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(" ", "N")));

                            if (VÁLTAdatok.Count > 0)
                            {
                                string ideignév = Dolgozónév_kiíratása(VÁLTAdatok, a.ID, Személy);
                                ideignév = ideignév.Trim() != "_" ? ideignév : "";
                                szöveg = ideignév.Replace("_", "\n");
                                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(szöveg, "N")));
                            }
                            else
                                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(" ", "N")));
                            Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(" ", "N")));
                        }
                    }
                    Holtart.Lép();
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
            return Válasz;
        }

        private PdfPTable Javítások()
        {
            PdfPTable Válasz = new PdfPTable(1);
            try
            {
                Válasz.WidthPercentage = 100;
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás("A KARBANTARTÓ TEVÉKENYSÉG SORÁN FELMERÜLŐ ÉSZREVÉTELEK, JAVÍTÁSOK", "N"), true, true, true, "LIGHT_GRAY"));
                for (int i = 0; i < Hiba_sor.Value; i++)
                {
                    Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás(" ", "N", 10, 1, 20f)));
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
            return Válasz;
        }

        private PdfPTable Szerszámok()
        {
            PdfPTable Válasz = new PdfPTable(1);
            try
            {
                Válasz.WidthPercentage = 100;
                Válasz.AddCell(MyPDF.Cella(MyPDF.Kiírás("A KARBANTARTÓ TEVÉKENYSÉG SORÁN HASZNÁLT KALIBRÁLT ESZKÖZÖK, SZERSZÁMOK LISTÁJA", "N"), true, true, true, "LIGHT_GRAY"));
                PdfPTable pdfTable = new PdfPTable(4);
                pdfTable.SetWidths(new float[] { 6, 3, 5, 3 });
                pdfTable.AddCell(MyPDF.Cella(MyPDF.Kiírás("ESZKÖZ, SZERSZÁM TÍPUSA", "N")));
                pdfTable.AddCell(MyPDF.Cella(MyPDF.Kiírás("SOROZATSZÁMA", "N")));
                pdfTable.AddCell(MyPDF.Cella(MyPDF.Kiírás("MUNKAUTASÍTÁS SORSZÁMA", "N")));
                pdfTable.AddCell(MyPDF.Cella(MyPDF.Kiírás("ALÁÍRÁS", "N")));
                for (int i = 0; i < Szerszám_sor.Value * 4; i++)
                {
                    pdfTable.AddCell(MyPDF.Cella(MyPDF.Kiírás(" ", "N", 10, 1, 20f)));
                }
                Válasz.AddCell(pdfTable);
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
            return Válasz;
        }

        private PdfPTable Megjegyzés()
        {
            PdfPTable Válasz = new PdfPTable(1);
            try
            {
                Válasz.WidthPercentage = 100;
                PdfPCell textCell = new PdfPCell();
                string szövegrész = "Megjegyzés: \n" +
                   "(*) Nemmegfelelősségeket jelezd részletsesen írásban\n" +
                   "(**) Státusz oszlopba pipálással jelezd a munkafolyamat eredeményét\n" +
                   "(***) Aláírásommal igazolom, hogy a felsorolt járműveken, a típusra aktuálisan" +
                   " érvényes Főtechnológia jelölt karbantartási ciklusban előírt feladatait elvégeztem.";
                textCell.AddElement(MyPDF.Kiírás(szövegrész, "N", 10f, 0));
                textCell.Border = PdfPCell.NO_BORDER;
                Válasz.AddCell(textCell);

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
            return Válasz;
        }

        private PdfPTable Aláírás()
        {
            PdfPTable Válasz = new PdfPTable(2);
            try
            {
                Válasz.WidthPercentage = 100;
                Válasz.SetWidths(new float[] { 11, 5 });

                PdfPCell textCell = MyPDF.Cella(MyPDF.Kiírás("\n\nAz ellenőrzések, javítások elvégzését követően a jármű forgalomképes. Ellenőrizte:", "N"));
                textCell.Border = PdfPCell.NO_BORDER;
                Válasz.AddCell(textCell);
                textCell = MyPDF.Cella(MyPDF.Kiírás(" ", "N"));
                textCell.Border = PdfPCell.NO_BORDER;
                Válasz.AddCell(textCell);

                textCell = MyPDF.Cella(MyPDF.Kiírás(" ", "N"));
                textCell.Border = PdfPCell.NO_BORDER;
                Válasz.AddCell(textCell);
                if (Kiadta.Text.Trim() == "")
                {
                    textCell = MyPDF.Cella(MyPDF.Kiírás("Irányító", "N"));
                    textCell.Border = PdfPCell.NO_BORDER;
                }
                else
                {
                    string ideig = Kiadta.Text.Trim().Replace("-", "\n");
                    textCell = MyPDF.Cella(MyPDF.Kiírás(ideig, "N"));
                    textCell.Border = PdfPCell.NO_BORDER;
                }
                Válasz.AddCell(textCell);

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
            return Válasz;
        }
        #endregion

        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
                if (Cmbtelephely.Text.Trim() == "") return;
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                else
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
    }
}
