using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_Ablakok._5_Karbantartás.Karbantartás_Közös
{
    public partial class Karbantartás_Rögzítés : Form
    {
        static string Típus { get; set; }
        Adat_T5C5_Kmadatok Adat { get; set; }
        string _fájlexc;

        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_T5C5_Kmadatok KézKmAdatok;
        readonly Kezelő_Ciklus KézCiklus = new Kezelő_Ciklus();
        readonly Kezelő_Ciklus_Sorrend KézSorrend = new Kezelő_Ciklus_Sorrend();
        readonly Kezelő_kiegészítő_telephely KézKieg = new Kezelő_kiegészítő_telephely();

        readonly List<Adat_Ciklus> AdatokCiklus = new List<Adat_Ciklus>();
        List<Adat_Ciklus_Sorrend> AdatokSorrend = new List<Adat_Ciklus_Sorrend>();
        List<Adat_T5C5_Kmadatok> AdatokKmAdatok = new List<Adat_T5C5_Kmadatok>();

        public Karbantartás_Rögzítés(string típus, Adat_T5C5_Kmadatok adat)
        {
            InitializeComponent();
            Típus = típus;
            Adat = adat;
            KézKmAdatok = new Kezelő_T5C5_Kmadatok(Típus);
            Start();
        }

        private void Start()
        {

            Kiír();
            Üzemek_listázása();
            CiklusrendCombo_feltöltés();
            this.Text = $"Pályaszámú {Adat.Azonosító} jármű {Adat.ID} számú vizsgálata";
            Jogosultságkiosztás();
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

        private void Kiír()
        {
            Pályaszám.Text = Adat.Azonosító;
            Jjavszám.Text = Adat.Jjavszám.ToString();
            Utolsófelújításdátuma.Value = Adat.Fudátum;
            TEljesKmText.Text = Adat.Teljeskm.ToString();

            Sorszám.Text = Adat.ID.ToString();
            CiklusrendCombo.Text = Adat.Ciklusrend.Trim();

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
                List<Adat_Ciklus> Adatok = KézCiklus.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Típus.Trim() == CiklusrendCombo.Text.Trim()
                          orderby a.Sorszám
                          select a).ToList();

                foreach (Adat_Ciklus Elem in Adatok)
                    Vizsgsorszám.Items.Add(Elem.Sorszám);
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
                                        && (a.Valóstípus.Contains("T5C5") || a.Típus.Contains("T5C5"))
                                        select a).FirstOrDefault();

                if (ElemJármű != null)
                {
                    AdatokKmAdatok = KézKmAdatok.Lista_Adatok().OrderByDescending(a => a.ID).ToList();
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
                    MessageBox.Show("A pályaszám nem T5C5! ", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //  Kiirjaatörténelmet();


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
                        //  Kiirjaatörténelmet();

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
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    _fájlexc = OpenFileDialog1.FileName;
                else
                    return;


                timer1.Enabled = true;
                Holtart.Be();
                await Task.Run(() => SAP_Adatokbeolvasása.Km_beolvasó(_fájlexc, "T5C5"));
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

                // csak főmérnökségi belépéssel törölhető
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Töröl.Visible = true;
                    Új_adat.Visible = true;
                }
                else
                {
                    Töröl.Visible = false;
                    Új_adat.Visible = false;
                }

                melyikelem = 107;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Utolsó_V_rögzítés.Enabled = true;
                    Töröl.Enabled = true;
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
    }
}
