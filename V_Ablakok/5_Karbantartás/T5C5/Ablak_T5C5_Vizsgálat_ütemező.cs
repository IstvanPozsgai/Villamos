using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Ablakok._5_Karbantartás.T5C5;
using Villamos.V_Ablakok.Közös;
using Villamos.Villamos_Ablakok;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_T5C5_Vizsgálat_ütemező
    {
        string AlsóPanel1;
        Ablak_Kereső Új_Ablak_Kereső;
        Ablak_T5C5_Segéd Új_Ablak_T5C5_Segéd;
#pragma warning disable IDE0044 // Add readonly modifier
        //      List<Adat_Általános_String_Dátum> Frissítés = new List<Adat_Általános_String_Dátum>();
        List<Adat_T5C5_Posta> Posta_lista = new List<Adat_T5C5_Posta>();
#pragma warning restore IDE0044 // Add readonly modifier

        readonly Kezelő_Szerelvény KézSzer = new Kezelő_Szerelvény();
        readonly Kezelő_Nap_Hiba KézHiba = new Kezelő_Nap_Hiba();
        readonly Kezelő_Főkönyv_Zser_Km KézZser = new Kezelő_Főkönyv_Zser_Km();
        readonly Kezelő_T5C5_Kmadatok KézVkm = new Kezelő_T5C5_Kmadatok("T5C5");
        readonly Kezelő_Osztály_Adat KézCsat = new Kezelő_Osztály_Adat();
        readonly Kezelő_T5C5_Göngyöl KézFutás = new Kezelő_T5C5_Göngyöl();
        readonly Kezelő_Hétvége_Előírás KézElőírás = new Kezelő_Hétvége_Előírás();
        readonly Kezelő_Kerék_Tábla KézKerék = new Kezelő_Kerék_Tábla();
        readonly Kezelő_Kerék_Mérés Mérés_kéz = new Kezelő_Kerék_Mérés();
        readonly Kezelő_Vezénylés KézVezény = new Kezelő_Vezénylés();
        readonly Kezelő_Hétvége_Beosztás KézHBeosztás = new Kezelő_Hétvége_Beosztás();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();

        List<Adat_Szerelvény> AdatokSzer = new List<Adat_Szerelvény>();
        List<Adat_Szerelvény> AdatokSzerelvényElő = new List<Adat_Szerelvény>();
        List<Adat_Nap_Hiba> AdatokHiba = new List<Adat_Nap_Hiba>();
        List<Adat_Főkönyv_Zser_Km> AdatokZSER = new List<Adat_Főkönyv_Zser_Km>();
        List<Adat_T5C5_Kmadatok> AdatokVkm = new List<Adat_T5C5_Kmadatok>();
        List<Adat_Osztály_Adat> AdatokCsatoló = new List<Adat_Osztály_Adat>();
        List<Adat_T5C5_Göngyöl> AdatokFutás = new List<Adat_T5C5_Göngyöl>();
        List<Adat_Hétvége_Beosztás> AdatokHBeosztás = new List<Adat_Hétvége_Beosztás>();
        List<Adat_Hétvége_Előírás> AdatokElőírás = new List<Adat_Hétvége_Előírás>();
        List<Adat_Kerék_Mérés> Mérés_Adatok = new List<Adat_Kerék_Mérés>();
        List<Adat_Vezénylés> AdatokVezény = new List<Adat_Vezénylés>();


        bool Terv = false;
        // Számításhoz
        long KorNapikm = 0;
        long VUtánFutot = 0;
        long ElőzőVtől = 0;
        long ElőzőV2V3 = 0;

        #region Alap
        public Ablak_T5C5_Vizsgálat_ütemező()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            try
            {
                //Ha van 0-tól különböző akkor a régi jogosultságkiosztást használjuk
                //ha mind 0 akkor a GombLathatosagKezelo-t használjuk
                if (Program.PostásJogkör.Any(c => c != '0'))
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }
                else
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }
                VonalasFrissít();

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

        private void Ablak_Vizsgálat_ütemező_Load(object sender, EventArgs e)
        {
        }

        private void Ablak_T5C5_Vizsgálat_ütemező_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kereső?.Close();
            Új_Ablak_T5C5_Segéd?.Close();
            Új_Ablak_Utasítás_Generálás?.Close();
            Új_Ablak_T5C5_Vonalak?.Close();
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.PostásTelephely.Contains("törzs"))
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim(); }
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
                Cmbtelephely.Text = Program.PostásTelephely;
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

                melyikelem = 103;
                // módosítás 1 Dolgozók ki és beléptetése

                if (MyF.Vanjoga(melyikelem, 1))
                {
                }
                // módosítás 2 Állományba vétel

                if (MyF.Vanjoga(melyikelem, 1))
                {
                }
                // módosítás 3 Vezénylés

                if (MyF.Vanjoga(melyikelem, 1))
                {
                }
                melyikelem = 104;
                // módosítás 1

                if (MyF.Vanjoga(melyikelem, 1))
                {
                }
                // módosítás 2 

                if (MyF.Vanjoga(melyikelem, 1))
                {
                }

                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                }

                melyikelem = 105;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {

                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                }
                // módosítás 3

                if (MyF.Vanjoga(melyikelem, 1))
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

        private void Ablak_Vizsgálat_ütemező_KeyDown(object sender, KeyEventArgs e)
        {

            // ESC
            if ((int)e.KeyCode == 27)
            {
                Új_Ablak_Kereső?.Close();

                Új_Ablak_T5C5_Segéd?.Close();
            }
            //ctrl+F
            if (e.Control && e.KeyCode == Keys.F)
            {
                Keresés_metódus();
            }

        }
        #endregion



        #region V ütemező
        private void Tábla_kitöltés()
        {
            try
            {
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 38;

                Listák_Feltöltése();

                // fejléc elkészítése 
                Tábla.Columns[0].HeaderText = "Ssz";
                Tábla.Columns[0].Width = 60;
                Tábla.Columns[1].HeaderText = "Psz";
                Tábla.Columns[1].Width = 70;
                Tábla.Columns[2].HeaderText = "Típus";
                Tábla.Columns[2].Width = 70;
                Tábla.Columns[2].Frozen = true;
                Tábla.Columns[3].HeaderText = "Vizsg. foka";
                Tábla.Columns[3].Width = 70;
                Tábla.Columns[4].HeaderText = "Vizsg. Ssz.";
                Tábla.Columns[4].Width = 70;
                Tábla.Columns[5].HeaderText = "Vizsg. Vége";
                Tábla.Columns[5].Width = 110;
                Tábla.Columns[6].HeaderText = "V után futott korr";
                Tábla.Columns[6].Width = 70;
                Tábla.Columns[7].HeaderText = "Havi km";
                Tábla.Columns[7].Width = 70;
                Tábla.Columns[8].HeaderText = "Köv. V";
                Tábla.Columns[8].Width = 80;
                Tábla.Columns[9].HeaderText = "Köv. V Ssz";
                Tábla.Columns[9].Width = 80;
                Tábla.Columns[10].HeaderText = "Előző V-től km korr";
                Tábla.Columns[10].Width = 80;

                Tábla.Columns[11].HeaderText = "Köv. V2/V3";
                Tábla.Columns[11].Width = 80;
                Tábla.Columns[12].HeaderText = "Előző V2/V3-től km korr";
                Tábla.Columns[12].Width = 80;
                Tábla.Columns[13].HeaderText = "Jármű státusz";
                Tábla.Columns[13].Width = 120;
                Tábla.Columns[14].HeaderText = "Hiba leírása";
                Tábla.Columns[14].Width = 300;

                Tábla.Columns[15].HeaderText = "Előírt Szerelvény";
                Tábla.Columns[15].Width = 160;
                Tábla.Columns[16].HeaderText = "Csatolhatóság";
                Tábla.Columns[16].Width = 70;
                Tábla.Columns[17].HeaderText = "Kerék átmérő Min";
                Tábla.Columns[17].Width = 80;
                Tábla.Columns[18].HeaderText = "KMU";
                Tábla.Columns[18].Width = 80;
                Tábla.Columns[19].HeaderText = "Ciklus";
                Tábla.Columns[19].Width = 120;

                Tábla.Columns[20].HeaderText = "Vonal";
                Tábla.Columns[20].Width = 70;
                Tábla.Columns[21].HeaderText = "Napos utolsó";
                Tábla.Columns[21].Width = 70;
                Tábla.Columns[22].HeaderText = "Napos szám";
                Tábla.Columns[22].Width = 70;
                Tábla.Columns[23].HeaderText = "E3 nap";
                Tábla.Columns[23].Width = 70;
                Tábla.Columns[24].HeaderText = "Tény Szer.sz";
                Tábla.Columns[24].Width = 70;
                Tábla.Columns[25].HeaderText = "Tény Szerelvény";
                Tábla.Columns[25].Width = 70;
                Tábla.Columns[26].HeaderText = "Előírt Szer Sz";
                Tábla.Columns[26].Width = 70;
                Tábla.Columns[27].HeaderText = "Előírt Szerelvény1";
                Tábla.Columns[27].Width = 70;
                Tábla.Columns[28].HeaderText = "EÍ Szer hossz";
                Tábla.Columns[28].Width = 70;
                Tábla.Columns[29].HeaderText = "Státus";
                Tábla.Columns[29].Width = 70;
                Tábla.Columns[30].HeaderText = "E3 vezénylés";
                Tábla.Columns[30].Width = 70;
                Tábla.Columns[31].HeaderText = "Vissza";
                Tábla.Columns[31].Width = 70;
                Tábla.Columns[32].HeaderText = "Kiad";
                Tábla.Columns[32].Width = 70;
                Tábla.Columns[33].HeaderText = "Korrigált km";
                Tábla.Columns[33].Width = 70;
                Tábla.Columns[34].HeaderText = "V után futott";
                Tábla.Columns[34].Width = 70;
                Tábla.Columns[35].HeaderText = "Előző V-től km ";
                Tábla.Columns[35].Width = 80;
                Tábla.Columns[36].HeaderText = "Előző V2/V3-től km ";
                Tábla.Columns[36].Width = 70;
                Tábla.Columns[37].HeaderText = "Friss dátum";
                Tábla.Columns[37].Width = 110;

                // kilistázzuk a adatbázis adatait
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adatok = (from a in Adatok
                          where a.Törölt == false
                          && a.Valóstípus.Contains("T5C5")
                          orderby a.Azonosító
                          select a).ToList();
                Holtart.Be();

                foreach (Adat_Jármű rekord in Adatok)
                {
                    KorNapikm = 0;
                    VUtánFutot = 0;
                    ElőzőVtől = 0;
                    ElőzőV2V3 = 0;

                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;

                    Tábla.Rows[i].Cells[1].Value = rekord.Azonosító;
                    Tábla.Rows[i].Cells[2].Value = rekord.Típus;
                    Tábla.Rows[i].Cells[17].Value = 0;
                    Tábla.Rows[i].Cells[19].Value = "";
                    Tábla.Rows[i].Cells[23].Value = 0;
                    Tábla.Rows[i].Cells[31].Value = "_";
                    Tábla.Rows[i].Cells[32].Value = "_";
                    Tábla.Rows[i].Cells[24].Value = "_";
                    Tábla.Rows[i].Cells[33].Value = 0;
                    Tábla.Rows[i].Cells[34].Value = 0;
                    Tábla.Rows[i].Cells[35].Value = 0;
                    Tábla.Rows[i].Cells[36].Value = 0;
                    switch (rekord.Státus)
                    {
                        case 0:
                            {
                                Tábla.Rows[i].Cells[13].Value = "Üzemképes";
                                break;
                            }
                        case 1:
                            {
                                Tábla.Rows[i].Cells[13].Value = "Szabad";
                                break;
                            }
                        case 2:
                            {
                                Tábla.Rows[i].Cells[13].Value = "Beálló";
                                break;
                            }
                        case 3:
                            {
                                Tábla.Rows[i].Cells[13].Value = "Beállóba adott";
                                break;
                            }
                        case 4:
                            {
                                // üzemképtelennél a pályaszám piros és a státus
                                Tábla.Rows[i].Cells[13].Value = "Üzemképtelen";
                                break;
                            }
                    }
                    Tábla.Rows[i].Cells[29].Value = rekord.Státus;
                    Tábla.Rows[i].Cells[24].Value = rekord.Szerelvénykocsik;
                    Szerelvények_listázása(rekord.Szerelvénykocsik, i);
                    Szerelvények_listázása_előírt(rekord.Azonosító, i);
                    Hiba_listázása(rekord.Azonosító, i);
                    V_km_adatok(rekord.Azonosító, i);
                    Csatolhatóság_listázása(rekord.Azonosító, i);
                    Futásadat_listázása(rekord.Azonosító, i);
                    Előírás_listázás(rekord.Azonosító, i);
                    Kerékátmérő(rekord.Azonosító, i);
                    Vezénylés_listázása(rekord.Azonosító, i);
                    Tábla.Rows[i].Cells[33].Value = 0;
                    Korrekció_km(rekord.Azonosító, i);

                    Tábla.Rows[i].Cells[6].Value = KorNapikm + VUtánFutot;
                    Tábla.Rows[i].Cells[10].Value = KorNapikm + ElőzőVtől;
                    Tábla.Rows[i].Cells[12].Value = KorNapikm + ElőzőV2V3;
                    Holtart.Lép();
                }
                Tábla.Refresh();
                AlsóPanel1 = "lista";

                Tábla.Sort(Tábla.Columns[12], System.ComponentModel.ListSortDirection.Descending);
                for (int ii = 0; ii < Tábla.Rows.Count; ii++)
                {
                    Tábla.Rows[ii].Cells[0].Value = ii + 1;
                }
                Tábla.Sort(Tábla.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                Holtart.Ki();
                TáblaSzínezés();
                Tábla.ClearSelection();
                Tábla.Visible = true;
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

        private void Listák_Feltöltése()
        {
            Szerelvény();
            ElőSzerelvény();
            HibaLista();
            ZSERlista();
            V_km_adatok_lista();
            CsatolLista();
            Futásadatlistázása();
            Előíráslistázás();
            KerékátmérőLista();
            Vezényléslistázása();
        }

        private void Szerelvény()
        {
            try
            {
                AdatokSzer.Clear();
                AdatokSzer = KézSzer.Lista_Adatok(Cmbtelephely.Text.Trim());
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

        private void ElőSzerelvény()
        {
            try
            {
                AdatokSzerelvényElő.Clear();
                AdatokSzerelvényElő = KézSzer.Lista_Adatok(Cmbtelephely.Text.Trim(), true);
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

        private void HibaLista()
        {
            try
            {
                Főkönyv_Funkciók.Napiállók(Cmbtelephely.Text.Trim());
                AdatokHiba.Clear();
                AdatokHiba = KézHiba.Lista_Adatok(Cmbtelephely.Text.Trim());
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

        private void ZSERlista()
        {
            try
            {
                AdatokZSER.Clear();
                AdatokZSER = KézZser.Lista_adatok(DateTime.Today.Year);
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

        private void V_km_adatok_lista()
        {
            try
            {
                AdatokVkm.Clear();
                AdatokVkm = KézVkm.Lista_Adatok();
                AdatokVkm = (from a in AdatokVkm
                             where a.Törölt == false
                             orderby a.Azonosító ascending, a.Vizsgdátumk descending
                             select a).ToList();
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

        private void CsatolLista()
        {
            try
            {
                AdatokCsatoló.Clear();
                AdatokCsatoló = KézCsat.Lista_Adat();
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

        private void Futásadatlistázása()
        {
            try
            {
                AdatokFutás.Clear();
                AdatokFutás = KézFutás.Lista_Adatok("Főmérnökség", DateTime.Today);
                AdatokFutás = (from a in AdatokFutás
                               where a.Telephely == Cmbtelephely.Text.Trim()
                               orderby a.Azonosító
                               select a).ToList();
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

        private void Előíráslistázás()
        {
            try
            {
                AdatokHBeosztás.Clear();
                AdatokHBeosztás = KézHBeosztás.Lista_Adatok(Cmbtelephely.Text.Trim());
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

        private void KerékátmérőLista()
        {
            try
            {
                Mérés_Adatok.Clear();
                Mérés_Adatok = Mérés_kéz.Lista_Adatok(DateTime.Today.Year);
                List<Adat_Kerék_Mérés> Mérés_AdatokE = Mérés_kéz.Lista_Adatok(DateTime.Today.Year - 1);
                if (Mérés_AdatokE != null)
                    Mérés_Adatok.AddRange(Mérés_AdatokE);
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

        private void Vezényléslistázása()
        {
            try
            {
                AdatokVezény.Clear();
                AdatokVezény = KézVezény.Lista_Adatok(Cmbtelephely.Text.Trim(), DateTime.Today);
                AdatokVezény = (from a in AdatokVezény
                                where a.Dátum >= DateTime.Today.AddDays(-1)
                                && a.Törlés == 0
                                orderby a.Azonosító
                                select a).ToList();
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

        private void Szerelvények_listázása(long Szerelvény_ID, int sor)
        {
            try
            {
                if (AdatokSzer == null) return;
                Adat_Szerelvény rekordszer = (from a in AdatokSzer
                                              where a.Szerelvény_ID == Szerelvény_ID
                                              select a).FirstOrDefault();
                if (rekordszer != null)
                {
                    string ideig = "";
                    // ha egyforma akkor kiírjuk
                    if (rekordszer.Kocsi1.Trim() != "_" && rekordszer.Kocsi1.Trim() != "0") ideig += rekordszer.Kocsi1.Trim();
                    if (rekordszer.Kocsi2.Trim() != "_" && rekordszer.Kocsi2.Trim() != "0") ideig += "-" + rekordszer.Kocsi2.Trim();
                    if (rekordszer.Kocsi3.Trim() != "_" && rekordszer.Kocsi3.Trim() != "0") ideig += "-" + rekordszer.Kocsi3.Trim();
                    if (rekordszer.Kocsi4.Trim() != "_" && rekordszer.Kocsi4.Trim() != "0") ideig += "-" + rekordszer.Kocsi4.Trim();
                    if (rekordszer.Kocsi5.Trim() != "_" && rekordszer.Kocsi5.Trim() != "0") ideig += "-" + rekordszer.Kocsi5.Trim();
                    if (rekordszer.Kocsi6.Trim() != "_" && rekordszer.Kocsi6.Trim() != "0") ideig += "-" + rekordszer.Kocsi6.Trim();
                    Tábla.Rows[sor].Cells[25].Value = ideig;
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

        private void Szerelvények_listázása_előírt(string azonosító, int sor)
        {
            try
            {
                if (AdatokSzerelvényElő == null) return;
                Adat_Szerelvény Elem = (from a in AdatokSzerelvényElő
                                        where a.Kocsi1 == azonosító || a.Kocsi2 == azonosító || a.Kocsi3 == azonosító ||
                                              a.Kocsi4 == azonosító || a.Kocsi5 == azonosító || a.Kocsi6 == azonosító
                                        select a).FirstOrDefault();
                if (Elem != null)
                {
                    string ideig = Elem.Kocsi1.Trim();
                    ideig += Elem.Kocsi2.Trim() == "_" ? "" : "-" + Elem.Kocsi2.Trim();
                    ideig += Elem.Kocsi3.Trim() == "_" ? "" : "-" + Elem.Kocsi3.Trim();
                    ideig += Elem.Kocsi4.Trim() == "_" ? "" : "-" + Elem.Kocsi4.Trim();
                    ideig += Elem.Kocsi5.Trim() == "_" ? "" : "-" + Elem.Kocsi5.Trim();
                    ideig += Elem.Kocsi6.Trim() == "_" ? "" : "-" + Elem.Kocsi6.Trim();

                    Tábla.Rows[sor].Cells[26].Value = Elem.Szerelvény_ID;
                    Tábla.Rows[sor].Cells[15].Value = ideig;
                    Tábla.Rows[sor].Cells[27].Value = ideig;
                    Tábla.Rows[sor].Cells[28].Value = Elem.Szerelvényhossz;
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

        private void Hiba_listázása(string azonosító, int sor)
        {
            try
            {
                if (AdatokHiba == null) return;
                Adat_Nap_Hiba rekordszer = (from a in AdatokHiba
                                            where a.Azonosító == azonosító
                                            select a).FirstOrDefault();
                if (rekordszer != null)
                    Tábla.Rows[sor].Cells[14].Value = rekordszer.Üzemképtelen + "-" + rekordszer.Beálló + "-" + rekordszer.Üzemképeshiba;
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

        private void V_km_adatok(string azonosító, int sor)
        {
            try
            {
                if (AdatokVkm != null)
                {
                    Adat_T5C5_Kmadatok rekordszer = (from a in AdatokVkm
                                                     where a.Azonosító == azonosító
                                                     select a).FirstOrDefault();
                    if (rekordszer != null)
                    {
                        Tábla.Rows[sor].Cells[3].Value = rekordszer.Vizsgfok;
                        Tábla.Rows[sor].Cells[4].Value = rekordszer.Vizsgsorszám;
                        Tábla.Rows[sor].Cells[5].Value = rekordszer.Vizsgdátumv.ToString("yyyy.MM.dd");
                        if (rekordszer.Vizsgsorszám == 0)
                        {
                            // ha J akkor nem kell különbséget képezni
                            Tábla.Rows[sor].Cells[34].Value = rekordszer.KMUkm;
                            VUtánFutot = rekordszer.KMUkm;
                        }
                        else
                        {
                            Tábla.Rows[sor].Cells[34].Value = rekordszer.KMUkm - rekordszer.Vizsgkm;
                            VUtánFutot = rekordszer.KMUkm - rekordszer.Vizsgkm;
                        }
                        Tábla.Rows[sor].Cells[7].Value = rekordszer.Havikm;
                        Tábla.Rows[sor].Cells[8].Value = rekordszer.KövV;
                        Tábla.Rows[sor].Cells[9].Value = rekordszer.KövV_sorszám;
                        Tábla.Rows[sor].Cells[35].Value = rekordszer.KMUkm - rekordszer.Vizsgkm;
                        ElőzőVtől = rekordszer.KMUkm - rekordszer.Vizsgkm;
                        Tábla.Rows[sor].Cells[11].Value = rekordszer.KövV2;
                        Tábla.Rows[sor].Cells[36].Value = rekordszer.KMUkm - rekordszer.V2V3Számláló;
                        ElőzőV2V3 = rekordszer.KMUkm - rekordszer.V2V3Számláló;
                        Tábla.Rows[sor].Cells[18].Value = rekordszer.KMUkm;
                        Tábla.Rows[sor].Cells[19].Value = rekordszer.Ciklusrend.Trim();
                        Tábla.Rows[sor].Cells[37].Value = rekordszer.KMUdátum.ToString("yyyy.MM.dd");
                    }
                    else
                    {
                        Tábla.Rows[sor].Cells[3].Value = "_";
                        Tábla.Rows[sor].Cells[4].Value = 0;
                        Tábla.Rows[sor].Cells[5].Value = "1900.01.01";
                        Tábla.Rows[sor].Cells[34].Value = 0;
                        Tábla.Rows[sor].Cells[7].Value = 0;
                        Tábla.Rows[sor].Cells[8].Value = "_";
                        Tábla.Rows[sor].Cells[9].Value = 0;
                        Tábla.Rows[sor].Cells[35].Value = 0;
                        Tábla.Rows[sor].Cells[11].Value = "_";
                        Tábla.Rows[sor].Cells[36].Value = 0;
                        Tábla.Rows[sor].Cells[18].Value = 0;
                        Tábla.Rows[sor].Cells[19].Value = "_";
                        Tábla.Rows[sor].Cells[37].Value = "1900.01.01";
                    }
                }
                else
                {
                    Tábla.Rows[sor].Cells[3].Value = "_";
                    Tábla.Rows[sor].Cells[4].Value = 0;
                    Tábla.Rows[sor].Cells[5].Value = "1900.01.01";
                    Tábla.Rows[sor].Cells[34].Value = 0;
                    Tábla.Rows[sor].Cells[7].Value = 0;
                    Tábla.Rows[sor].Cells[8].Value = "_";
                    Tábla.Rows[sor].Cells[9].Value = 0;
                    Tábla.Rows[sor].Cells[35].Value = 0;
                    Tábla.Rows[sor].Cells[11].Value = "_";
                    Tábla.Rows[sor].Cells[36].Value = 0;
                    Tábla.Rows[sor].Cells[18].Value = 0;
                    Tábla.Rows[sor].Cells[19].Value = "_";
                    Tábla.Rows[sor].Cells[37].Value = "1900.01.01";
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

        private void Csatolhatóság_listázása(string azonosító, int sor)
        {
            try
            {
                if (AdatokCsatoló == null) return;
                Adat_Osztály_Adat rekordszer = (from a in AdatokCsatoló
                                                where a.Azonosító == azonosító
                                                select a).FirstOrDefault();
                if (rekordszer != null)
                    Tábla.Rows[sor].Cells[16].Value = KézCsat.Érték(rekordszer, "Csatolhatóság");
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

        private void Futásadat_listázása(string azonosító, int sor)
        {
            try
            {
                if (AdatokFutás == null) return;
                Adat_T5C5_Göngyöl rekordszer = (from a in AdatokFutás
                                                where a.Azonosító == azonosító
                                                select a).FirstOrDefault();
                if (rekordszer != null)
                {
                    Tábla.Rows[sor].Cells[21].Value = rekordszer.Vizsgálatfokozata;
                    Tábla.Rows[sor].Cells[22].Value = rekordszer.Vizsgálatszáma;
                    Tábla.Rows[sor].Cells[23].Value = rekordszer.Futásnap;
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

        private void Előírás_listázás(string azonosító, int sor)
        {
            try
            {
                if (AdatokHBeosztás == null) return;
                Adat_Hétvége_Beosztás rekordszer = (from a in AdatokHBeosztás
                                                    where a.Kocsi1 == azonosító || a.Kocsi2 == azonosító || a.Kocsi3 == azonosító ||
                                                    a.Kocsi4 == azonosító || a.Kocsi5 == azonosító || a.Kocsi6 == azonosító
                                                    select a).FirstOrDefault();
                if (rekordszer != null)
                {
                    Tábla.Rows[sor].Cells[20].Value = rekordszer.Vonal;
                    string ideig = rekordszer.Vissza1 + "-" + rekordszer.Vissza2 + "-" + rekordszer.Vissza3 + "-" + rekordszer.Vissza4 + "-" + rekordszer.Vissza5 + "-" + rekordszer.Vissza6;
                    Tábla.Rows[sor].Cells[31].Value = ideig;
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

        private void Kerékátmérő(string azonosító, int sor)
        {
            try
            {
                if (Mérés_Adatok == null) return;
                List<Adat_Kerék_Mérés> Elem = (from a in Mérés_Adatok
                                               where a.Azonosító == azonosító
                                               orderby a.Mikor descending
                                               select a).ToList();
                if (Elem != null && Elem.Count != 0)
                {
                    int min = Elem.Take(4).Min(b => b.Méret);
                    Tábla.Rows[sor].Cells[17].Value = min;
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

        private void Vezénylés_listázása(string azonosító, int sor)
        {
            try
            {
                if (AdatokVezény != null && AdatokVezény.Count != 0)
                {
                    Adat_Vezénylés rekord = (from a in AdatokVezény
                                             where a.Azonosító == azonosító
                                             select a).FirstOrDefault();
                    if (rekord != null)
                    {
                        // ha egyforma akkor kiírjuk
                        if (rekord.Vizsgálatraütemez == 1)
                        {
                            // előző napi
                            if (rekord.Dátum.ToString("MM-dd-yyyy") == DateTime.Today.AddDays(-1).ToString("MM-dd-yyyy"))
                                Tábla.Rows[sor].Cells[30].Value = rekord.Vizsgálat.Trim() + "-" + rekord.Dátum.ToString("MM.dd") + "-e";
                            // aznapi
                            else if (rekord.Dátum.ToString("MM-dd-yyyy") == DateTime.Today.ToString("MM-dd-yyyy"))
                                Tábla.Rows[sor].Cells[30].Value = rekord.Vizsgálat.Trim() + "-" + rekord.Dátum.ToString("MM.dd") + "-a";
                            else
                                Tábla.Rows[sor].Cells[30].Value = rekord.Vizsgálat.Trim() + "-" + rekord.Dátum.ToString("MM.dd") + "-u";
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

        private void Korrekció_km(string azonosító, int sor)
        {
            try
            {
                Tábla.Rows[sor].Cells[33].Value = 0;
                if (AdatokZSER == null) return;

                List<Adat_Főkönyv_Zser_Km> KorNapikmLista = (from a in AdatokZSER
                                                             where a.Azonosító == azonosító && a.Dátum > Tábla.Rows[sor].Cells[37].Value.ToÉrt_DaTeTime()
                                                             select a).ToList();

                if (KorNapikmLista != null)
                    KorNapikm = KorNapikmLista.Sum(a => a.Napikm);

                Tábla.Rows[sor].Cells[33].Value = KorNapikm;

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

        private void Előírás_listázásFrissít()
        {
            try
            {
                Előíráslistázás();
                if (AdatokHBeosztás == null) return;
                Holtart.Be(AdatokHBeosztás.Count + 1);
                for (int i = 0; i < Tábla.Rows.Count; i++)
                {
                    string pályaszám = Tábla.Rows[i].Cells[1].Value.ToStrTrim();
                    Adat_Hétvége_Beosztás rekordszer = (from a in AdatokHBeosztás
                                                        where a.Kocsi1 == pályaszám || a.Kocsi2 == pályaszám || a.Kocsi3 == pályaszám ||
                                                        a.Kocsi4 == pályaszám || a.Kocsi5 == pályaszám || a.Kocsi6 == pályaszám
                                                        select a).FirstOrDefault();
                    if (rekordszer != null)
                    {
                        Tábla.Rows[i].Cells[20].Value = rekordszer.Vonal;
                        string ideig = rekordszer.Vissza1 + "-" + rekordszer.Vissza2 + "-" + rekordszer.Vissza3 + "-" + rekordszer.Vissza4 + "-" + rekordszer.Vissza5 + "-" + rekordszer.Vissza6;
                        Tábla.Rows[i].Cells[31].Value = ideig;
                    }
                    else
                    {
                        Tábla.Rows[i].Cells[20].Value = "";
                        Tábla.Rows[i].Cells[31].Value = "";
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

            // kiírja a hétvégi előírást
            List<Adat_Hétvége_Beosztás> Adatok = KézHBeosztás.Lista_Adatok(Cmbtelephely.Text.Trim());

            // sorbarendezzük a táblát pályaszám szerint
            Tábla.Sort(Tábla.Columns[1], System.ComponentModel.ListSortDirection.Ascending);

            Holtart.Be(100);

            for (int i = 0; i < Tábla.Rows.Count; i++)
            {
                foreach (Adat_Hétvége_Beosztás rekordszer in Adatok)
                {
                    if (Tábla.Rows[i].Cells[1].Value.ToString().Trim() == rekordszer.Kocsi1.Trim() ||
                        Tábla.Rows[i].Cells[1].Value.ToString().Trim() == rekordszer.Kocsi2.Trim() ||
                        Tábla.Rows[i].Cells[1].Value.ToString().Trim() == rekordszer.Kocsi3.Trim() ||
                        Tábla.Rows[i].Cells[1].Value.ToString().Trim() == rekordszer.Kocsi4.Trim() ||
                        Tábla.Rows[i].Cells[1].Value.ToString().Trim() == rekordszer.Kocsi5.Trim() ||
                        Tábla.Rows[i].Cells[1].Value.ToString().Trim() == rekordszer.Kocsi6.Trim())
                    {
                        Tábla.Rows[i].Cells[20].Value = rekordszer.Vonal.Trim();
                        string ideig = "";
                        if (rekordszer.Vissza1.Trim() == "1")
                            ideig += "1";
                        else
                            ideig += "0";
                        if (rekordszer.Vissza2.Trim() == "1")
                            ideig += "-1";
                        else
                            ideig += "-0";
                        if (rekordszer.Vissza3.Trim() == "1")
                            ideig += "-1";
                        else
                            ideig += "-0";
                        if (rekordszer.Vissza4.Trim() == "1")
                            ideig += "-1";
                        else
                            ideig += "-0";
                        if (rekordszer.Vissza5.Trim() == "1")
                            ideig += "-1";
                        else
                            ideig += "-0";
                        if (rekordszer.Vissza6.Trim() == "1")
                            ideig += "-1";
                        else
                            ideig += "-0";

                        Tábla.Rows[i].Cells[31].Value = ideig;
                        break;
                    }
                }
            }
            Holtart.Lép();
        }

        private void VonalasFrissít()
        {
            AdatokElőírás = KézElőírás.Lista_Adatok(Cmbtelephely.Text.Trim());
        }

        private void TáblaSzínezés()
        {
            // cellák színezése
            if (AlsóPanel1.Trim() == "lista")
            {
                for (int sor = 0; sor < Tábla.Rows.Count; sor++)
                {
                    if (Tábla.Rows[sor].Cells[29].Value != null)
                    {
                        switch (int.Parse(Tábla.Rows[sor].Cells[29].Value.ToString()))
                        {
                            case 3:
                                {
                                    // ha beálló
                                    Tábla.Rows[sor].Cells[1].Style.BackColor = Color.Yellow;
                                    Tábla.Rows[sor].Cells[1].Style.ForeColor = Color.Black;
                                    Tábla.Rows[sor].Cells[1].Style.Font = new Font("ThenArial Narrow", 11f, FontStyle.Italic);

                                    Tábla.Rows[sor].Cells[13].Style.BackColor = Color.Yellow;
                                    Tábla.Rows[sor].Cells[13].Style.ForeColor = Color.Black;
                                    Tábla.Rows[sor].Cells[13].Style.Font = new Font("ThenArial Narrow", 11f, FontStyle.Italic);
                                    break;
                                }
                            case 4:
                                {
                                    // ha BM
                                    Tábla.Rows[sor].Cells[1].Style.BackColor = Color.Red;
                                    Tábla.Rows[sor].Cells[1].Style.ForeColor = Color.White;
                                    Tábla.Rows[sor].Cells[1].Style.Font = new Font("ThenArial Narrow", 11f, FontStyle.Italic);

                                    Tábla.Rows[sor].Cells[13].Style.BackColor = Color.Red;
                                    Tábla.Rows[sor].Cells[13].Style.ForeColor = Color.White;
                                    Tábla.Rows[sor].Cells[13].Style.Font = new Font("ThenArial Narrow", 11f, FontStyle.Italic);
                                    break;
                                }
                        }
                    }
                    if (Tábla.Rows[sor].Cells[20].Value != null)
                    {
                        foreach (Adat_Hétvége_Előírás Elem in AdatokElőírás)
                        {
                            if (Tábla.Rows[sor].Cells[20].Value.ToStrTrim() == Elem.Vonal.Trim())
                            {
                                Tábla.Rows[sor].Cells[0].Style.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                                Tábla.Rows[sor].Cells[2].Style.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                                Tábla.Rows[sor].Cells[15].Style.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                                break;
                            }
                            if (Tábla.Rows[sor].Cells[20].Value.ToStrTrim() == "")
                            {
                                Tábla.Rows[sor].Cells[0].Style.BackColor = default;
                                Tábla.Rows[sor].Cells[2].Style.BackColor = default;
                                Tábla.Rows[sor].Cells[15].Style.BackColor = default;
                                break;
                            }
                        }
                    }
                }
            }
        }
        #endregion


        #region Keresés
        private void Keresés_metódus()
        {
            try
            {
                if (Új_Ablak_Kereső == null)
                {
                    Új_Ablak_Kereső = new Ablak_Kereső();
                    Új_Ablak_Kereső.FormClosed += Új_Ablak_Kereső_Closed;
                    Új_Ablak_Kereső.Top = 50;
                    Új_Ablak_Kereső.Left = 50;
                    Új_Ablak_Kereső.Show();
                    Új_Ablak_Kereső.Ismétlődő_Változás += Szövegkeresés;
                }
                else
                {
                    Új_Ablak_Kereső.Activate();
                    Új_Ablak_Kereső.WindowState = FormWindowState.Normal;
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

        private void Új_Ablak_Kereső_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kereső = null;
        }

        private void Szövegkeresés()
        {
            // megkeressük a szöveget a táblázatban
            if (Új_Ablak_Kereső.Keresendő == null) return;
            if (Új_Ablak_Kereső.Keresendő.Trim() == "") return;
            if (Tábla.Rows.Count < 0) return;

            for (int i = 0; i < Tábla.Rows.Count; i++)
            {
                if (Tábla.Rows[i].Cells[1].Value.ToString().Trim() == Új_Ablak_Kereső.Keresendő.Trim())
                {
                    Tábla.Rows[i].Cells[1].Style.BackColor = Color.Orange;
                    Tábla.FirstDisplayedScrollingRowIndex = i;
                    Tábla.CurrentCell = Tábla.Rows[i].Cells[1];
                    return;
                }
            }
        }
        #endregion


        #region Táblázatban kattint
        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (AlsóPanel1 == "szerelvény") return;
                if (e.RowIndex < 0) return;

                Táblázatba_kattint(e.RowIndex);
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

        private void Táblázatba_kattint(int sor)
        {
            try
            {
                string Tény;

                //Terv szerint vagy Tény szerint listáz
                if (Terv)
                {
                    //ha üres a szerelvény akkor a  pályaszám az egy elem
                    if (Tábla.Rows[sor].Cells[27].Value == null)
                        Tény = Tábla.Rows[sor].Cells[1].Value.ToString();
                    else
                        Tény = Tábla.Rows[sor].Cells[27].Value.ToString();
                }
                else
                {
                    //ha üres a szerelvény akkor a  pályaszám az egy elem
                    if (Tábla.Rows[sor].Cells[25].Value == null)
                        Tény = Tábla.Rows[sor].Cells[1].Value.ToString();
                    else
                        Tény = Tábla.Rows[sor].Cells[25].Value.ToString();
                }

                //Hány kocsiból áll a szerelvény
                string[] darab = Tény.Split('-');
                int[] sorok = new int[darab.Length];


                //Szerelvény járműveinek sorainak megkeresése
                for (int i = 0; i < darab.Length; i++)
                {
                    for (int j = 0; j < Tábla.Rows.Count; j++)
                    {
                        if (Tábla.Rows[j].Cells[1].Value.ToString().Trim() == darab[i].Trim())
                        {
                            sorok[i] = j;
                            break;
                        }

                    }
                }

                Adat_T5C5_Posta Posta;
                Posta_lista.Clear();

                //Összegyűjtük a szerelvény adatait
                for (int i = 0; i < darab.Length; i++)
                {
                    string Azonosító = Tartalom_Vizsgál(sorok[i], 1);
                    string Típus = Tartalom_Vizsgál(sorok[i], 2);
                    string Csatolható = Tartalom_Vizsgál(sorok[i], 16);

                    string V2_következő = Tartalom_Vizsgál(sorok[i], 11);
                    int V2_Futott_Km = Tartalom_Vizs_Int(sorok[i], 12);


                    int V_sorszám = Tartalom_Vizs_Int(sorok[i], 9);
                    string V_Következő = Tartalom_Vizsgál(sorok[i], 8);
                    int V_futott_Km = Tartalom_Vizs_Int(sorok[i], 10);

                    int E3_sorszám = Tartalom_Vizs_Int(sorok[i], 22);
                    int Napszám = Tartalom_Vizs_Int(sorok[i], 23);
                    string Terv_Nap = Tartalom_Vizsgál(sorok[i], 30);
                    string Hiba = Tartalom_Vizsgál(sorok[i], 14);

                    string Előírt_szerelvény = Tartalom_Vizsgál(sorok[i], 27) != "" ? Tartalom_Vizsgál(sorok[i], 27) : Tartalom_Vizsgál(sorok[i], 1);
                    string Tényleges_szerelvény = Tartalom_Vizsgál(sorok[i], 25);
                    string Rendelésszám = "";
                    int Státus = Tartalom_Vizs_Int(sorok[i], 29);
                    long szerelvényszám = Tartalom_Vizs_Long(sorok[i], 24);

                    int Vizsgál = 0;
                    int Marad = 0;

                    string Vissza = Tartalom_Vizsgál(sorok[i], 31);
                    string Kiad = Tartalom_Vizsgál(sorok[i], 32);
                    string Vonal = Tartalom_Vizsgál(sorok[i], 20);



                    Posta = new Adat_T5C5_Posta(
                                 Azonosító,
                                 Típus,
                                 Csatolható,
                                 V_sorszám,
                                 V2_következő,
                                 V2_Futott_Km,
                                 V_Következő,
                                 V_futott_Km,
                                 Napszám,
                                 Terv_Nap,
                                 Hiba,
                                 Előírt_szerelvény,
                                 Tényleges_szerelvény,
                                 Rendelésszám,
                                 szerelvényszám,
                                 Státus,
                                 E3_sorszám,
                                 Vizsgál,
                                 Marad,
                                 Kiad,
                                 Vissza,
                                 Vonal,
                                 Terv
                                   );
                    Posta_lista.Add(Posta);
                }

                Új_Ablak_T5C5_Segéd?.Close();


                Új_Ablak_T5C5_Segéd = new Ablak_T5C5_Segéd(Posta_lista, "Vizsgálat", DateTime.Today, Cmbtelephely.Text.Trim(), Terv);
                Új_Ablak_T5C5_Segéd.FormClosed += Ablak_T5C5_Segéd_Closed;
                Új_Ablak_T5C5_Segéd.Top = 150;
                Új_Ablak_T5C5_Segéd.Left = 500;
                Új_Ablak_T5C5_Segéd.Show();
                Új_Ablak_T5C5_Segéd.Változás += Adat_módosítás;
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

        private int Tartalom_Vizs_Int(int sor, int oszlop)
        {
            int válasz = 0;
            if (Tábla.Rows[sor].Cells[oszlop].Value != null)
                válasz = int.Parse(Tábla.Rows[sor].Cells[oszlop].Value.ToString().Trim());
            return válasz;
        }

        private long Tartalom_Vizs_Long(int sor, int oszlop)
        {
            long válasz = 0;
            if (Tábla.Rows[sor].Cells[oszlop].Value != null)
                válasz = int.Parse(Tábla.Rows[sor].Cells[oszlop].Value.ToString().Trim());
            return válasz;
        }

        private string Tartalom_Vizsgál(int sor, int oszlop)
        {
            string válasz = "";
            if (Tábla.Rows[sor].Cells[oszlop].Value != null)
                válasz = Tábla.Rows[sor].Cells[oszlop].Value.ToString().Trim();
            return válasz;
        }

        private void Ablak_T5C5_Segéd_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_T5C5_Segéd = null;
        }

        private void Adat_módosítás()
        {
            Előírás_listázásFrissít();
            Tábla.Sort(Tábla.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
            TáblaSzínezés();
            Holtart.Ki();
        }
        #endregion


        #region Gombok
        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\V2Vizsgálat.html";
                Module_Excel.Megnyitás(hely);
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

        private void Előírt_Click(object sender, EventArgs e)
        {

            Tábla_kitöltés();
            Terv = true;
        }

        private void AktuálisLista_Click(object sender, EventArgs e)
        {

            Tábla_kitöltés();
            Terv = false;
        }

        private void Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"T5C5_Vizsgálat_{Program.PostásTelephely.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;
                Module_Excel.DataGridViewToExcel(fájlexc, Tábla, true);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Module_Excel.Megnyitás(fájlexc);
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

        private void AktSzerelvény_Click(object sender, EventArgs e)
        {
            try
            {
                AlsóPanel1 = "szerelvény";

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 19;

                // fejléc elkészítése 
                Tábla.Columns[0].HeaderText = "Típus";
                Tábla.Columns[0].Width = 80;
                Tábla.Columns[1].HeaderText = "Psz";
                Tábla.Columns[1].Width = 80;
                Tábla.Columns[2].HeaderText = "V után futott";
                Tábla.Columns[2].Width = 80;
                Tábla.Columns[3].HeaderText = "Csatolhatóság";
                Tábla.Columns[3].Width = 80;
                Tábla.Columns[4].HeaderText = "Psz";
                Tábla.Columns[4].Width = 80;
                Tábla.Columns[5].HeaderText = "V után futott";
                Tábla.Columns[5].Width = 80;
                Tábla.Columns[6].HeaderText = "Csatolhatóság";
                Tábla.Columns[6].Width = 80;
                Tábla.Columns[7].HeaderText = "Psz";
                Tábla.Columns[7].Width = 80;
                Tábla.Columns[8].HeaderText = "V után futott";
                Tábla.Columns[8].Width = 80;
                Tábla.Columns[9].HeaderText = "Csatolhatóság";
                Tábla.Columns[9].Width = 80;
                Tábla.Columns[10].HeaderText = "Psz";
                Tábla.Columns[10].Width = 80;
                Tábla.Columns[11].HeaderText = "V után futott";
                Tábla.Columns[11].Width = 80;
                Tábla.Columns[12].HeaderText = "Csatolhatóság";
                Tábla.Columns[12].Width = 80;
                Tábla.Columns[13].HeaderText = "Psz";
                Tábla.Columns[13].Width = 80;
                Tábla.Columns[14].HeaderText = "V után futott";
                Tábla.Columns[14].Width = 80;
                Tábla.Columns[15].HeaderText = "Csatolhatóság";
                Tábla.Columns[15].Width = 80;
                Tábla.Columns[16].HeaderText = "Psz";
                Tábla.Columns[16].Width = 80;
                Tábla.Columns[17].HeaderText = "V után futott";
                Tábla.Columns[17].Width = 80;
                Tábla.Columns[18].HeaderText = "Csatolhatóság";
                Tábla.Columns[18].Width = 80;

                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adatok = (from a in Adatok
                          where a.Valóstípus.Contains("T5C5")
                          orderby a.Szerelvénykocsik, a.Azonosító
                          select a).ToList();

                AdatokCsatoló = KézCsat.Lista_Adat();
                V_km_adatok_lista();

                long előző = 0;
                int oszlop = 0;
                int i = 0;
                Holtart.Be(Adatok.Count);

                foreach (Adat_Jármű rekord in Adatok)
                {
                    if (előző != rekord.Szerelvénykocsik || rekord.Szerelvénykocsik == 0)
                    {
                        Tábla.RowCount++;
                        i = Tábla.RowCount - 1;
                        előző = rekord.Szerelvénykocsik;
                        oszlop = 1;
                    }
                    Tábla.Rows[i].Cells[0].Value = rekord.Valóstípus.Trim();
                    if (előző == rekord.Szerelvénykocsik)
                    {
                        Tábla.Rows[i].Cells[oszlop].Value = rekord.Azonosító.Trim();

                        Adat_T5C5_Kmadatok rekordkm = (from a in AdatokVkm
                                                       where a.Azonosító == rekord.Azonosító.Trim()
                                                       select a).FirstOrDefault();
                        if (rekordkm != null)
                            Tábla.Rows[i].Cells[oszlop + 1].Value = rekordkm.KMUkm - rekordkm.Vizsgkm;

                        Adat_Osztály_Adat rekordszer = (from a in AdatokCsatoló
                                                        where a.Azonosító == rekord.Azonosító
                                                        select a).FirstOrDefault();
                        if (rekordszer != null)
                            Tábla.Rows[i].Cells[oszlop + 2].Value = KézCsat.Érték(rekordszer, "Csatolhatóság");


                        oszlop += 3;
                    }
                    Holtart.Lép();
                }

                Tábla.Refresh();
                Tábla.Sort(Tábla.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                Tábla.Visible = true;
                Tábla.ClearSelection();
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

        private void Kereső_Click(object sender, EventArgs e)
        {
            Keresés_metódus();
        }

        private void BeosztásTörlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Valóban töröljük az eddigi adatokat?", "Biztonsági kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    KézHBeosztás.Törlés(Cmbtelephely.Text.Trim());
                    Új_Ablak_Utasítás_Generálás?.Close();
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


        Ablak_Utasítás_Generálás Új_Ablak_Utasítás_Generálás;
        private void Utasítás_Click(object sender, EventArgs e)
        {
            Új_Ablak_Utasítás_Generálás?.Close();

            Új_Ablak_Utasítás_Generálás = new Ablak_Utasítás_Generálás(Cmbtelephely.Text.Trim(), UtasításSzövegTervezet());
            Új_Ablak_Utasítás_Generálás.FormClosed += Ablak_Utasítás_Generálás_FormClosed;
            Új_Ablak_Utasítás_Generálás.Show();
        }

        private void Ablak_Utasítás_Generálás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Utasítás_Generálás = null;
        }

        private string UtasításSzövegTervezet()
        {
            string válasz = "";
            try
            {
                string előzővonal = "";

                string szöveg;
                int i = 0;
                List<Adat_Hétvége_Beosztás> Adatok = KézHBeosztás.Lista_Adatok(Cmbtelephely.Text.Trim());

                szöveg = "20 -n forgalomba kell adni:\r\n";

                foreach (Adat_Hétvége_Beosztás rekord in Adatok)
                {
                    if (előzővonal.Trim() == "" || előzővonal.Trim() != rekord.Vonal.Trim())
                    {
                        előzővonal = rekord.Vonal.Trim();
                        szöveg += $"\r\n {rekord.Vonal.Trim()} Vonal\r\n\r\n";
                        i = 0;
                    }
                    i++;
                    szöveg += i.ToString() + "- ";
                    if (rekord.Kocsi1.Trim() != "") szöveg += rekord.Kocsi1.Trim();
                    if (rekord.Kocsi2.Trim() != "") szöveg += "-" + rekord.Kocsi2.Trim();
                    if (rekord.Kocsi3.Trim() != "") szöveg += "-" + rekord.Kocsi3.Trim();
                    if (rekord.Kocsi4.Trim() != "") szöveg += "-" + rekord.Kocsi4.Trim();
                    if (rekord.Kocsi5.Trim() != "") szöveg += "-" + rekord.Kocsi5.Trim();
                    if (rekord.Kocsi6.Trim() != "") szöveg += "-" + rekord.Kocsi6.Trim();

                    if (rekord.Vissza1 == "1") szöveg += " Vissza kell csatolni:" + rekord.Kocsi1.Trim();
                    if (rekord.Vissza2 == "1") szöveg += " Vissza kell csatolni:" + rekord.Kocsi2.Trim();
                    if (rekord.Vissza3 == "1") szöveg += " Vissza kell csatolni:" + rekord.Kocsi3.Trim();
                    if (rekord.Vissza4 == "1") szöveg += " Vissza kell csatolni:" + rekord.Kocsi4.Trim();
                    if (rekord.Vissza5 == "1") szöveg += " Vissza kell csatolni:" + rekord.Kocsi5.Trim();
                    if (rekord.Vissza6 == "1") szöveg += " Vissza kell csatolni:" + rekord.Kocsi6.Trim();
                    szöveg += "\r\n";
                }
                válasz += szöveg + "\r\n";
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

        Ablak_T5C5_Vonalak Új_Ablak_T5C5_Vonalak;
        private void Vonalak_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_T5C5_Vonalak == null)
            {
                Új_Ablak_T5C5_Vonalak = new Ablak_T5C5_Vonalak(Cmbtelephely.Text.Trim());
                Új_Ablak_T5C5_Vonalak.FormClosed += Ablak_T5C5_Vonalak_FormClosed;
                Új_Ablak_T5C5_Vonalak.Változás += VonalasFrissít;
                Új_Ablak_T5C5_Vonalak.Show();
            }
            else
            {
                Új_Ablak_T5C5_Vonalak.Activate();
                Új_Ablak_T5C5_Vonalak.WindowState = FormWindowState.Maximized;
            }
        }

        private void Ablak_T5C5_Vonalak_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_T5C5_Vonalak = null;
        }

        Ablak_T5C5_Felmentés Új_Ablak_T5C5_Felmentés;
        private void Felmentés_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_T5C5_Felmentés == null)
            {
                Új_Ablak_T5C5_Felmentés = new Ablak_T5C5_Felmentés(Cmbtelephely.Text.Trim());
                Új_Ablak_T5C5_Felmentés.FormClosed += Ablak_T5C5_Felmentés_FormClosed;
                Új_Ablak_T5C5_Felmentés.Show();
            }
            else
            {
                Új_Ablak_T5C5_Felmentés.Activate();
                Új_Ablak_T5C5_Felmentés.WindowState = FormWindowState.Maximized;
            }
        }

        private void Ablak_T5C5_Felmentés_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_T5C5_Vonalak = null;
        }

        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
                if (Cmbtelephely.Text.Trim() == "") return;
                if (Program.PostásJogkör.Any(c => c != '0'))
                {

                }
                else
                {
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
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
