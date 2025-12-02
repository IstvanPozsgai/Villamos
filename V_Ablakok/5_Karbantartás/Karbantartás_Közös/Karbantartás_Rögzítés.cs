using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Ablakok;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_Ablakok._5_Karbantartás.Karbantartás_Közös
{
    public partial class Karbantartás_Rögzítés : Form
    {
        public event Event_Kidobó Változás;
        static string Típus { get; set; }
        static bool UtolsóElem { get; set; }
        Adat_T5C5_Kmadatok Adat { get; set; }
        string _fájlexc;

        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_T5C5_Kmadatok KézKmAdatok;
        readonly Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();
        readonly Kezelő_Ciklus_Sorrend KézSorrend = new Kezelő_Ciklus_Sorrend();
        readonly Kezelő_kiegészítő_telephely KézKieg = new Kezelő_kiegészítő_telephely();
        readonly Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();

        List<Adat_Ciklus_Sorrend> AdatokSorrend = new List<Adat_Ciklus_Sorrend>();
        List<Adat_T5C5_Kmadatok> AdatokKmAdatok = new List<Adat_T5C5_Kmadatok>();
        List<Adat_Ciklus> AdatokCiklus = new List<Adat_Ciklus>();

        public Karbantartás_Rögzítés(string típus, Adat_T5C5_Kmadatok adat, bool utolsóelem)
        {
            InitializeComponent();
            Típus = típus;
            Adat = adat;
            UtolsóElem = utolsóelem;
            KézKmAdatok = new Kezelő_T5C5_Kmadatok(Típus);
            Start();
        }

        public Karbantartás_Rögzítés()
        {
            InitializeComponent();
        }

        private void Start()
        {

            Kiír();
            Üzemek_listázása();
            CiklusrendCombo_feltöltés();
            this.Text = $"Pályaszámú {Adat.Azonosító} jármű {Adat.ID} számú vizsgálata";
            //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
            //ha nem akkor a régit használjuk
            if (Program.PostásJogkör.Substring(0, 1) == "R")
                GombLathatosagKezelo.Beallit(this);
            else
                Jogosultságkiosztás();

            AdatokKmAdatok = KézKmAdatok.Lista_Adatok();
        }

        private void Új_adat_Click(object sender, EventArgs e)
        {
            Kiüríti_lapfül();
            this.Text = $"Pályaszámú {Adat.Azonosító} jármű ? számú vizsgálata";
        }

        private void Kiüríti_lapfül()
        {
            Sorszám.Text = "";

            Vizsgsorszám.Text = "0";
            Vizsgfok.Text = "";
            Vizsgdátumk.Value = DateTime.Today;
            Vizsgdátumv.Value = DateTime.Today;
            VizsgKm.Text = "0";
            Üzemek.Text = "";

            KMUkm.Text = "0";
            KMUdátum.Value = DateTime.Today;

            HaviKm.Text = "0";
            KMUdátum.Value = DateTime.Today;

            KövV.Text = "";
            KövV_Sorszám.Text = "";
            KövV1km.Text = "0";
            KövV2.Text = "";
            KövV2_Sorszám.Text = "";
            KövV2_számláló.Text = "0";
            KövV2km.Text = "0";
        }

        private void MezőEngedélyezés(bool Engedély)
        {
            Sorszám.Enabled = Engedély;

            Vizsgsorszám.Enabled = Engedély;
            Vizsgfok.Enabled = Engedély;
            Vizsgdátumk.Enabled = Engedély;
            Vizsgdátumv.Enabled = Engedély;
            VizsgKm.Enabled = Engedély;
            Üzemek.Enabled = Engedély;

            KMUkm.Enabled = Engedély;
            KMUdátum.Enabled = Engedély;

            HaviKm.Enabled = Engedély;
            KMUdátum.Enabled = Engedély;

            KövV.Enabled = Engedély;
            KövV_Sorszám.Enabled = Engedély;
            KövV1km.Enabled = Engedély;
            KövV2.Enabled = Engedély;
            KövV2_Sorszám.Enabled = Engedély;
            KövV2_számláló.Enabled = Engedély;
            KövV2km.Enabled = Engedély;
            Vizsgfok.Enabled = Engedély;
            CiklusrendCombo.Enabled = Engedély;
            Jjavszám.Enabled = Engedély;
            TEljesKmText.Enabled = Engedély;
            Utolsófelújításdátuma.Enabled = Engedély;
            UtolsóVSzámláló.Enabled = Engedély;
        }

        private void Kiír()
        {
            Pályaszám.Text = Adat.Azonosító;
            Jjavszám.Text = Adat.Jjavszám.ToString();
            Utolsófelújításdátuma.Value = Adat.Fudátum;
            TEljesKmText.Text = Adat.Teljeskm.ToString();

            Sorszám.Text = Adat.ID.ToString();
            CiklusrendCombo.Text = Adat.Ciklusrend.Trim();
            Vizsgsorszámcombofeltölés();
            Vizsgsorszám.Text = Adat.Vizsgsorszám.ToString();


            Vizsgfok.Text = Adat.Vizsgfok;
            Vizsgdátumk.Value = Adat.Vizsgdátumk;
            Vizsgdátumv.Value = Adat.Vizsgdátumv;
            VizsgKm.Text = Adat.Vizsgkm.ToString();
            Üzemek.Text = Adat.V2végezte;

            KMUkm.Text = Adat.KMUkm.ToString();
            KMUdátum.Value = Adat.KMUdátum;

            HaviKm.Text = Adat.Havikm.ToString();


            KövV.Text = Adat.KövV;
            KövV_Sorszám.Text = Adat.KövV_sorszám.ToString();
            UtolsóVSzámláló.Text = Adat.Vizsgkm.ToString();
            KövV1km.Text = (Adat.KMUkm - Adat.Vizsgkm).ToString();
            KövV2.Text = Adat.KövV2;
            KövV2_Sorszám.Text = Adat.KövV2_sorszám.ToString();
            KövV2_számláló.Text = Adat.V2V3Számláló.ToString();
            KövV2km.Text = (Adat.KMUkm - Adat.V2V3Számláló).ToString();
        }

        private void Vizsgsorszámcombofeltölés()
        {
            try
            {
                Vizsgsorszám.Items.Clear();

                if (CiklusrendCombo.Text.Trim() == "") return;
                AdatokCiklus = KézCiklus.Lista_Adatok();
                AdatokCiklus = (from a in AdatokCiklus
                                where a.Típus.Trim() == CiklusrendCombo.Text.Trim()
                                orderby a.Sorszám
                                select a).ToList();

                foreach (Adat_Ciklus Elem in AdatokCiklus)
                    Vizsgsorszám.Items.Add(Elem.Sorszám.ToString());
                Vizsgsorszám.Refresh();
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

        private void CiklusrendCombo_feltöltés()
        {
            try
            {
                AdatokSorrend = KézSorrend.Lista_Adatok().Where(a => a.JárműTípus == Típus.Trim() && a.Sorszám >= 0).ToList();
                CiklusrendCombo.Items.Clear();

                foreach (Adat_Ciklus_Sorrend Elem in AdatokSorrend)
                    CiklusrendCombo.Items.Add(Elem.CiklusNév);

                CiklusrendCombo.Refresh();
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

        private void CiklusrendCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Vizsgsorszámcombofeltölés();
        }

        private void Üzemek_listázása()
        {
            try
            {
                Üzemek.Items.Clear();
                List<Adat_kiegészítő_telephely> Adatok = KézKieg.Lista_Adatok();
                foreach (Adat_kiegészítő_telephely Elem in Adatok)
                    Üzemek.Items.Add(Elem.Telephelykönyvtár);
                Üzemek.Refresh();
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

        private void Vizsgsorszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int i = Vizsgsorszám.SelectedIndex;
                if (CiklusrendCombo.Text.Trim() == "") return;

                List<Adat_Ciklus> CiklusAdat = KézCiklus.Lista_Adatok();
                CiklusAdat = CiklusAdat.Where(a => a.Típus.Trim() == CiklusrendCombo.Text.Trim()).OrderBy(a => a.Sorszám).ToList();
                string Vizsgálatfok = (from a in CiklusAdat
                                       where a.Sorszám == i
                                       select a.Vizsgálatfok).FirstOrDefault();

                if (Vizsgálatfok != null)
                    Vizsgfok.Text = Vizsgálatfok;

                // következő vizsgálat sorszáma
                Vizsgálatfok = (from a in CiklusAdat
                                where a.Sorszám == i + 1
                                select a.Vizsgálatfok).FirstOrDefault();
                if (Vizsgálatfok != null)
                    KövV.Text = Vizsgálatfok;

                KövV_Sorszám.Text = (i + 1).ToString();
                // követekező V2-V3
                KövV2.Text = "J";
                KövV2_Sorszám.Text = "0";
                for (int j = i + 1; j < CiklusAdat.Count; j++)
                {
                    if (CiklusAdat[j].Vizsgálatfok.Contains("V2"))
                    {
                        KövV2.Text = CiklusAdat[j].Vizsgálatfok;
                        KövV2_Sorszám.Text = j.ToString();
                        break;
                    }
                    if (CiklusAdat[j].Vizsgálatfok.Contains("V3"))
                    {
                        KövV2.Text = CiklusAdat[j].Vizsgálatfok;
                        KövV2_Sorszám.Text = j.ToString();
                        break;
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

        private void Utolsó_V_rögzítés_Click(object sender, EventArgs e)
        {
            AdatokRögzítés();
        }

        private void AdatokRögzítés()
        {
            try
            {
                // leellenőrizzük, hogy minden adat ki van-e töltve
                if (!int.TryParse(VizsgKm.Text, out int vizsgkm)) throw new HibásBevittAdat("Vizsgálat km számláló állása mező nem lehet üres és egész számnak kell lennie.");
                if (Vizsgfok.Text.Trim() == "") throw new HibásBevittAdat("Vizsgálat foka mezőt ki kell tölteni");
                if (!int.TryParse(Vizsgsorszám.Text, out int vizsgsorszám)) throw new HibásBevittAdat("Vizsgálat sorszáma mező nem lehet üres és egész számnak kell lennie.");
                if (!int.TryParse(KMUkm.Text, out int kmukm)) throw new HibásBevittAdat("Utolsó felújítás óta futott km mező nem lehet üres és egész számnak kell lennie.");
                if (!int.TryParse(HaviKm.Text, out int havikm)) throw new HibásBevittAdat("Havi futásteljesítmény mező nem lehet üres és egész számnak kell lennie.");
                if (!int.TryParse(Jjavszám.Text, out int jjavszám)) throw new HibásBevittAdat("Felújítás sorszáma mező nem lehet üres és egész számnak kell lennie.");
                if (!int.TryParse(TEljesKmText.Text, out int teljesKmText)) throw new HibásBevittAdat("Üzembehelyezés óta futott km mező nem lehet üres és egész számnak kell lennie.");
                if (CiklusrendCombo.Text.Trim() == "") throw new HibásBevittAdat("Ütemezés típusa mezőt ki kell tölteni");
                if (!int.TryParse(KövV2_Sorszám.Text, out int kövV2_Sorszám)) throw new HibásBevittAdat("Következő V2-V3 sorszám mező nem lehet üres és egész számnak kell lennie.");
                if (!int.TryParse(KövV_Sorszám.Text, out int kövV_Sorszám)) throw new HibásBevittAdat("Következő V mező nem lehet üres és egész számnak kell lennie.");
                if (!int.TryParse(KövV2km.Text, out int kövV2km)) throw new HibásBevittAdat("V2-V3-tól futott km mező nem lehet üres és egész számnak kell lennie.");
                if (!long.TryParse(KövV2_számláló.Text, out long kövV2_számláló)) throw new HibásBevittAdat("V2-V3 számláló állás mező nem lehet üres és egész számnak kell lennie.");

                // megnézzük az adatbázist, ha nincs ilyen kocsi T5C5 benne akkor rögzít máskülönben az adatokat módosítja
                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");

                Adat_Jármű ElemJármű = (from a in AdatokJármű
                                        where a.Azonosító == Pályaszám.Text.Trim()
                                        && (a.Valóstípus.Contains(Típus) || a.Típus.Contains(Típus))
                                        select a).FirstOrDefault();

                if (ElemJármű != null)
                {
                    AdatokKmAdatok = AdatokKmAdatok.OrderByDescending(a => a.ID).ToList();
                    if (!long.TryParse(Sorszám.Text, out long sorszám))
                    {
                        sorszám = 1;
                        if (AdatokKmAdatok.Count > 0) sorszám = AdatokKmAdatok.Max(a => a.ID) + 1;
                    }

                    Adat_T5C5_Kmadatok ADAT = new Adat_T5C5_Kmadatok(
                        sorszám,
                        MyF.Szöveg_Tisztítás(Pályaszám.Text.Trim()),
                        jjavszám,
                        kmukm,
                        KMUdátum.Value,
                        MyF.Szöveg_Tisztítás(Vizsgfok.Text.Trim()),
                        Vizsgdátumk.Value,
                        Vizsgdátumv.Value,
                        vizsgkm,
                        havikm,
                        vizsgsorszám,
                        Utolsófelújításdátuma.Value,
                        teljesKmText,
                        MyF.Szöveg_Tisztítás(CiklusrendCombo.Text.Trim()),
                        MyF.Szöveg_Tisztítás(Üzemek.Text.Trim()),
                        kövV2_Sorszám,
                        MyF.Szöveg_Tisztítás(KövV2.Text.Trim()),
                        kövV_Sorszám,
                        MyF.Szöveg_Tisztítás(KövV.Text.Trim()),
                        false,
                        kövV2_számláló);


                    if (Sorszám.Text == "")
                        KézKmAdatok.Rögzítés(ADAT);                          // Új adat
                    else
                        KézKmAdatok.Módosítás(ADAT);  // módosítjuk az adatokat
                    MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"A pályaszám nem {Típus}! ", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Változás?.Invoke();
                this.Close();
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

        private void Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (long.TryParse(Sorszám.Text.Trim(), out long sorSzám))
                {
                    if (MessageBox.Show("Valóban töröljük az adatsort?", "Biztonsági kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        KézKmAdatok.Törlés(sorSzám);
                        Változás?.Invoke();
                        this.Close();
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

        private async void SAP_adatok_Click(object sender, EventArgs e)
        {
            try
            {
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel (*.xlsx)|*.xlsx|Excel 97-2003 (*.xls)|*.xls"
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                    _fájlexc = OpenFileDialog1.FileName;
                else
                    return;


                timer1.Enabled = true;
                Holtart.Be();
                await Task.Run(() => SAP_Adatokbeolvasása.Km_beolvasó(_fájlexc, Típus));
                timer1.Enabled = false;
                Holtart.Ki();
                MessageBox.Show("Az adatok beolvasása megtörtént !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Utolsó_V_rögzítés.Enabled = false;
                Töröl.Enabled = false;


                Btn_Követekező_Ciklus.Enabled = false;
                Btn_SelejtreFutat.Enabled = false;
                Btn_Biztonsági.Enabled = false;
                Btn_V1Plusz.Enabled = false;
                JJavítás.Enabled = false;
                Vezényel.Enabled = false;

                // csak főmérnökségi belépéssel törölhető
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    MezőEngedélyezés(true);
                    Töröl.Visible = true;
                    Új_adat.Visible = true;
                    Utolsó_V_rögzítés.Visible = true;
                    Következő_V.Visible = true;
                }
                else
                {
                    MezőEngedélyezés(false);
                    Töröl.Visible = false;
                    Új_adat.Visible = false;
                    Utolsó_V_rögzítés.Visible = false;
                    Következő_V.Visible = false;
                }

                melyikelem = 107;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Utolsó_V_rögzítés.Enabled = true;
                    Töröl.Enabled = true;
                    Új_adat.Visible = false;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Btn_Követekező_Ciklus.Enabled = true;
                    Btn_SelejtreFutat.Enabled = true;
                    Btn_Biztonsági.Enabled = true;
                    Btn_V1Plusz.Enabled = true;
                    JJavítás.Enabled = true;
                    Vezényel.Enabled = true;
                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {

                }
                melyikelem = 109;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Utolsó_V_rögzítés.Enabled = true;
                    Töröl.Enabled = true;
                    Új_adat.Visible = false;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Btn_Követekező_Ciklus.Enabled = true;
                    Btn_SelejtreFutat.Enabled = true;
                    Btn_Biztonsági.Enabled = true;
                    Btn_V1Plusz.Enabled = true;
                    JJavítás.Enabled = true;
                    Vezényel.Enabled = true;
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

        /// <summary>
        /// Következő vizsgálat sorszámát kiírja a mezőbe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Következő_V_Click(object sender, EventArgs e)
        {
            if (Vizsgsorszám.Text.Trim() == "") return;
            Sorszám.Text = "";
            Vizsgsorszám.Text = (int.Parse(Vizsgsorszám.Text) + 1).ToString();
            Vizsgdátumk.Value = DateTime.Today;
            Vizsgdátumv.Value = DateTime.Today;
            VizsgKm.Text = KMUkm.Text;
        }

        private void Karbantartás_Rögzítés_Load(object sender, EventArgs e)
        {

        }

        private void Btn_Követekező_Ciklus_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UtolsóElem) throw new HibásBevittAdat("A jármű utolsó karbantartási sora esetén lehet elvégezni.");
                //kiválasztjuk azt ami a textben van
                int index = CiklusrendCombo.Items.IndexOf(CiklusrendCombo.Text);
                if (index < 0) throw new HibásBevittAdat("A kiválasztott Mezőben lévő szöveg nem eleme a választási listának.");
                if (CiklusrendCombo.SelectedIndex == CiklusrendCombo.Items.Count - 1) throw new HibásBevittAdat("Nincs több választható cillus rend.");
                if (CiklusrendCombo.Items.Count <= index + 1) throw new HibásBevittAdat("A kiválasztott Ciklus rend az utolós így nem lehet tovább léptetni.");
                if (!Adat.KövV.Contains("V3")) throw new HibásBevittAdat($"A következő sorszámú {Adat.KövV_sorszám} vizsgálata {Adat.KövV}, \nmely esetén nem lehet ciklus rendet változtatni.");


                //Megnézzük, hogy mi volt az utolsó rögzített
                Adat_T5C5_Kmadatok UtolsóKM = (from a in AdatokKmAdatok
                                               where a.Azonosító == Adat.Azonosító
                                               && a.Vizsgdátumk < Adat.Vizsgdátumk
                                               orderby a.Vizsgdátumk descending
                                               select a).FirstOrDefault();
                if (UtolsóKM == null) return;
                // visszakeressük az előző ciklus sorszámát
                int indexUtolsó = CiklusrendCombo.Items.IndexOf(UtolsóKM.Ciklusrend);

                if (indexUtolsó != index) throw new HibásBevittAdat("Csak egy ciklusrendet lehet léptetni.");

                //Következő ciklus kiírása
                CiklusrendCombo.Text = CiklusrendCombo.Items[index + 1].ToString();

                // Ugyan azt a sorszámot választjuk
                int sorszámIndex = Vizsgsorszám.Items.IndexOf(Vizsgsorszám.Text);
                if (sorszámIndex < 0) throw new HibásBevittAdat("A kiválasztott Mezőben lévő sorszám nem eleme a választási listának.");

                Vizsgsorszám.Text = Vizsgsorszám.Items[sorszámIndex].ToString();

                AdatokRögzítés();
                Változás?.Invoke();
                this.Close();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Btn_SelejtreFutat_Click(object sender, EventArgs e)
        {

            try
            {
                if (!UtolsóElem) throw new HibásBevittAdat("A jármű utolsó karbantartási sora esetén lehet elvégezni.");
                string újNév = "";
                if (Adat.KövV_sorszám != 0) throw new HibásBevittAdat($"A következő sorszámú {Adat.KövV_sorszám} vizsgálata {Adat.KövV}, \nmely esetén nem lehet beállítani V2 vizsgálatot.");

                //Felírjuk a plusszos V2 nevét és növeljük eggyel
                List<Adat_T5C5_Kmadatok> KMAdatok = KézKmAdatok.Lista_Adatok();

                Adat_T5C5_Kmadatok ElőzőV2 = (from a in KMAdatok
                                              where a.Azonosító == Adat.Azonosító
                                              && (a.Vizsgfok.Contains("V2") || a.Vizsgfok.Contains("V3"))
                                              && a.Törölt == false
                                              orderby a.Vizsgdátumk descending
                                              select a).FirstOrDefault();
                if (ElőzőV2.Vizsgfok.Contains("P"))
                {
                    string[] darabol = ElőzőV2.Vizsgfok.Split('P');
                    újNév = "V2" + "P" + (int.Parse(darabol[1]) + 1).ToString();
                }
                else
                {
                    string[] darabol = ElőzőV2.Vizsgfok.Split('_');
                    újNév = "V2" + "_P1";
                }

                KövV.Text = újNév;
                KövV2.Text = újNév;
                KövV_Sorszám.Text = ElőzőV2.Vizsgsorszám.ToString();
                KövV2_Sorszám.Text = ElőzőV2.Vizsgsorszám.ToString();

                AdatokRögzítés();
                Változás?.Invoke();
                this.Close();


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

        private void Btn_Biztonsági_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UtolsóElem) throw new HibásBevittAdat("A jármű utolsó karbantartási sora esetén lehet elvégezni.");
                KövV.Text = "V1_B";
                KövV_Sorszám.Text = Vizsgsorszám.Text.Trim();

                AdatokRögzítés();
                Változás?.Invoke();
                this.Close();
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

        private void Btn_V1Plusz_Click(object sender, EventArgs e)
        {

            try
            {
                if (!UtolsóElem) throw new HibásBevittAdat("A jármű utolsó karbantartási sora esetén lehet elvégezni.");
                if (!Vizsgfok.Text.Contains("V1")) throw new HibásBevittAdat("Csak V1 vizsgálat után lehet alkalmazni.");
                string[] darabol = Vizsgfok.Text.Split('_');
                if (darabol[1] == "B")
                {
                    //Ha V1_B volt az utolsó akkor az eredetinek megfelelő sorszámot léptetjük
                    Adat_Ciklus EgyCiklus = (from a in AdatokCiklus
                                             where a.Sorszám == Vizsgsorszám.Text.ToÉrt_Long()
                                             select a).FirstOrDefault();
                    if (EgyCiklus != null)
                    {
                        string[] darabol1 = EgyCiklus.Vizsgálatfok.Split('_');
                        KövV.Text = $"{darabol[0]}_{darabol1[1].ToÉrt_Int() + 1}";
                    }

                }
                else
                {
                    KövV.Text = $"{darabol[0]}_{darabol[1].ToÉrt_Int() + 1}";

                }
                KövV_Sorszám.Text = Vizsgsorszám.Text.Trim();
                AdatokRögzítés();
                Változás?.Invoke();
                this.Close();
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

        private void Vezényel_Click(object sender, EventArgs e)
        {
            try
            {
                if (CiklusrendCombo.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva ciklusrend.");
                if (Vizsgsorszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs feltöltve a ciklusrend.");
                if (!UtolsóElem) throw new HibásBevittAdat("A jármű utolsó karbantartási sora esetén lehet elvégezni.");
                int index = Vizsgsorszám.Items.IndexOf(Vizsgsorszám.Text);
                KézHiba.Ütemezés_általános(true, true, Adat.Azonosító, Adat.KövV, Adat.KövV_sorszám, DateTime.Today, Típus);
                this.Close();
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

        private void JJavítás_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UtolsóElem) throw new HibásBevittAdat("A jármű utolsó karbantartási sora esetén lehet elvégezni.");
                CiklusrendCombo.Text = CiklusrendCombo.Items[0].ToString();
                KövV.Text = "J";
                KövV2.Text = "J";
                KövV_Sorszám.Text = "0";
                KövV2_Sorszám.Text = "0";

                AdatokRögzítés();
                Változás?.Invoke();
                this.Close();
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
