using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Ablakok;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_T5C5_napütemezés
    {
        private string FájlExcel_;
        string AlsóPanel1 = "";
        Ablak_Kereső Új_Ablak_Kereső;
        Ablak_T5C5_Segéd Új_Ablak_T5C5_Segéd;
        #region Kezelők Listák


        readonly Kezelő_T5C5_Göngyöl KézÁllomány = new Kezelő_T5C5_Göngyöl();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Szerelvény KézSzerelvény = new Kezelő_Szerelvény();
        readonly Kezelő_Nap_Hiba KézHiba = new Kezelő_Nap_Hiba();
        readonly Kezelő_Vezénylés KézVezénylés = new Kezelő_Vezénylés();
        readonly Kezelő_Szerelvény KézSzerElő = new Kezelő_Szerelvény();
        readonly Kezelő_Osztály_Adat KézOszt = new Kezelő_Osztály_Adat();
        readonly Kezelő_Jármű_Vendég KézIdegen = new Kezelő_Jármű_Vendég();
        readonly Kezelő_T5C5_Kmadatok KézKM = new Kezelő_T5C5_Kmadatok("T5C5");
        readonly Kezelő_Főkönyv_Zser_Km KézKorr = new Kezelő_Főkönyv_Zser_Km();
        readonly Kezelő_T5C5_Havi_Nap KézNapok = new Kezelő_T5C5_Havi_Nap();
        readonly Kezelő_Menetkimaradás KézMenet = new Kezelő_Menetkimaradás();

        List<Adat_T5C5_Göngyöl> Adatok = new List<Adat_T5C5_Göngyöl>();
        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        List<Adat_Szerelvény> AdatokSzerelvény = new List<Adat_Szerelvény>();
        List<Adat_Nap_Hiba> AdatokHiba = new List<Adat_Nap_Hiba>();
        List<Adat_Vezénylés> AdatokVezénylés = new List<Adat_Vezénylés>();
        List<Adat_Vezénylés> AdatokVezénylésN = new List<Adat_Vezénylés>();
        List<Adat_Szerelvény> AdatokSzerElő = new List<Adat_Szerelvény>();
        List<Adat_Osztály_Adat> AdatokOszt = new List<Adat_Osztály_Adat>();
        List<Adat_Jármű_Vendég> AdatokIdegen = new List<Adat_Jármű_Vendég>();
        List<Adat_T5C5_Kmadatok> AdatokKM = new List<Adat_T5C5_Kmadatok>();
#pragma warning disable IDE0044 // Add readonly modifier
        List<Adat_Főkönyv_Zser_Km> AdatokKorr = new List<Adat_Főkönyv_Zser_Km>();
        List<Adat_Főkönyv_Zser_Km> AdatokZserKm = new List<Adat_Főkönyv_Zser_Km>();
        List<Adat_Általános_String_Dátum> Frissítés = new List<Adat_Általános_String_Dátum>();
        List<Adat_T5C5_Posta> Posta_lista = new List<Adat_T5C5_Posta>();
#pragma warning restore IDE0044 // Add readonly modifier
        #endregion


        #region Alap
        public Ablak_T5C5_napütemezés()
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
                Dátum.Value = DateTime.Today;
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

        private void Ablak_T5C5_napütemezés_Load(object sender, EventArgs e)
        {

        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.PostásTelephely.Contains("törzs"))
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

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\T5C5_futás_ütemez.html";
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

        private void Ablak_T5C5_napütemezés_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27)
            {
                Új_Ablak_Kereső?.Close();

                Új_Ablak_T5C5_Segéd?.Close();
            }

            //Ctrl+F
            if (e.Control && e.KeyCode == Keys.F)
            {
                Keresés_metódus();
            }
        }

        private void Jogosultságkiosztás()
        {

            int melyikelem;

            Btn_Vezénylésbeírás.Enabled = false;
            SAP_adatok.Enabled = false;

            melyikelem = 102;
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
                Btn_Vezénylésbeírás.Enabled = true;
                SAP_adatok.Enabled = true;
            }
        }

        private void Ablak_T5C5_napütemezés_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kereső?.Close();
            Új_Ablak_T5C5_Segéd?.Close();
        }
        #endregion


        #region Listázások
        private void Btn_Lista_Click(object sender, EventArgs e)
        {
            Kocsikkirása();
        }

        private void Kocsikkirása()
        {
            try
            {
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 33;
                AlsóPanel1 = "lista";
                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Psz";
                Tábla.Columns[0].Width = 65;
                Tábla.Columns[1].HeaderText = "Típus";
                Tábla.Columns[1].Width = 80;
                Tábla.Columns[1].Frozen = true;
                Tábla.Columns[2].HeaderText = "Utolsó vizsgálat";
                Tábla.Columns[2].Width = 100;
                Tábla.Columns[3].HeaderText = "Vizsgálat fokozata";
                Tábla.Columns[3].Width = 80;
                Tábla.Columns[4].HeaderText = "Vizsgálat száma";
                Tábla.Columns[4].Width = 80;
                Tábla.Columns[5].HeaderText = "Futásnap";
                Tábla.Columns[5].Width = 80;
                Tábla.Columns[6].HeaderText = "Hiba";
                Tábla.Columns[6].Width = 300;
                Tábla.Columns[7].HeaderText = "Telephely";
                Tábla.Columns[7].Width = 80;
                Tábla.Columns[8].HeaderText = "V. előző";
                Tábla.Columns[8].Width = 80;
                Tábla.Columns[9].HeaderText = "Előterv";
                Tábla.Columns[9].Width = 100;
                Tábla.Columns[10].HeaderText = "Következő V";
                Tábla.Columns[10].Width = 80;
                Tábla.Columns[11].HeaderText = "Előző V-től km korrigált";
                Tábla.Columns[11].Width = 80;
                Tábla.Columns[12].HeaderText = "Utolsó forgalminap";
                Tábla.Columns[12].Width = 120;
                Tábla.Columns[13].HeaderText = "Előírt Szerelvény szám";
                Tábla.Columns[13].Width = 100; // .Columns(13).Visible = False
                Tábla.Columns[14].HeaderText = "Előírt Szerelvény öá.";
                Tábla.Columns[14].Width = 150;
                Tábla.Columns[15].HeaderText = "Tény Szerelvény szám";
                Tábla.Columns[15].Width = 80;
                Tábla.Columns[16].HeaderText = "Tény Szerelvény öá-";
                Tábla.Columns[16].Width = 150;
                Tábla.Columns[17].HeaderText = "Követező V2";
                Tábla.Columns[17].Width = 80;
                Tábla.Columns[18].HeaderText = "Előző V2-től futott";
                Tábla.Columns[18].Width = 80;
                Tábla.Columns[19].HeaderText = "A_";
                Tábla.Columns[19].Width = 80;
                Tábla.Columns[20].HeaderText = "B_";
                Tábla.Columns[20].Width = 80;
                Tábla.Columns[21].HeaderText = "C_";
                Tábla.Columns[21].Width = 80;
                Tábla.Columns[22].HeaderText = "V napi";
                Tábla.Columns[22].Width = 80;
                Tábla.Columns[23].HeaderText = "státus";
                Tábla.Columns[23].Width = 80;
                Tábla.Columns[24].HeaderText = "marad";
                Tábla.Columns[24].Width = 80;
                Tábla.Columns[25].HeaderText = "V";
                Tábla.Columns[25].Width = 80;
                Tábla.Columns[26].HeaderText = "státus_";
                Tábla.Columns[26].Width = 80;
                Tábla.Columns[26].Visible = true;
                Tábla.Columns[27].HeaderText = "Rendelés";
                Tábla.Columns[27].Width = 80;
                Tábla.Columns[28].HeaderText = "Következő V Ssz";
                Tábla.Columns[28].Width = 80;
                Tábla.Columns[29].HeaderText = "Csatolhatóság";
                Tábla.Columns[29].Width = 80;
                Tábla.Columns[30].HeaderText = "Előző V-től km";
                Tábla.Columns[30].Width = 80;
                Tábla.Columns[31].HeaderText = "km korr";
                Tábla.Columns[31].Width = 80;
                Tábla.Columns[32].HeaderText = "Frissítés dátum";
                Tábla.Columns[32].Width = 100;

                Főkönyv_Funkciók.Napiállók(Cmbtelephely.Text.Trim());
                Adatok = KézÁllomány.Lista_Adatok("Főmérnökség", DateTime.Today);
                AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim()).Where(a => a.Valóstípus.Contains("T5C5")).ToList();
                AdatokSzerelvény = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim());   // Szerelvény
                AdatokZserKm = KézKorr.Lista_adatok(Dátum.Value.Year);      //Zser adatok
                AdatokHiba = KézHiba.Lista_Adatok(Cmbtelephely.Text.Trim());    //    Hiba
                Vezénylés_Lista_feltöltés();  //Vezénylés
                VezénylésN_Lista_feltöltés(); //Napi
                AdatokSzerElő = KézSzerElő.Lista_Adatok(Cmbtelephely.Text.Trim(), true);     //Előírt szerelvény
                AdatokOszt = KézOszt.Lista_Adat();   //Csatolhat
                AdatokIdegen = KézIdegen.Lista_Adatok();        //Idegen 
                AdatokKM = KézKM.Lista_Adatok();      //           KM tábla

                Holtart.Be(Adatok.Count + 2);

                // kiírjuk az alapot
                foreach (Adat_Jármű EgyJármű in AdatokJármű)
                {
                    Holtart.Lép();
                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = EgyJármű.Azonosító;
                    Tábla.Rows[i].Cells[1].Value = EgyJármű.Valóstípus;
                    Tábla.Rows[i].Cells[6].Value = "_";
                    Tábla.Rows[i].Cells[9].Value = "_";
                    Tábla.Rows[i].Cells[10].Value = "_";
                    Tábla.Rows[i].Cells[11].Value = 0;
                    Tábla.Rows[i].Cells[14].Value = "_";
                    Tábla.Rows[i].Cells[15].Value = "0";
                    Tábla.Rows[i].Cells[15].Value = EgyJármű.Szerelvénykocsik;
                    Tábla.Rows[i].Cells[16].Value = "_";
                    Tábla.Rows[i].Cells[23].Value = EgyJármű.Státus;
                    Tábla.Rows[i].Cells[24].Value = 0;
                    Tábla.Rows[i].Cells[25].Value = 0;
                    Tábla.Rows[i].Cells[26].Value = 0;
                    Tábla.Rows[i].Cells[27].Value = "_";
                    Tábla.Rows[i].Cells[31].Value = 0;

                    Adat_T5C5_Göngyöl rekord = Adatok.Where(a => a.Azonosító == EgyJármű.Azonosító).FirstOrDefault();
                    if (rekord != null)
                    {
                        Tábla.Rows[i].Cells[2].Value = rekord.Vizsgálatdátuma.ToString("yyyy.MM.dd");
                        Tábla.Rows[i].Cells[3].Value = rekord.Vizsgálatfokozata;
                        Tábla.Rows[i].Cells[4].Value = rekord.Vizsgálatszáma;
                        Tábla.Rows[i].Cells[5].Value = rekord.Futásnap;
                        Tábla.Rows[i].Cells[12].Value = rekord.Utolsóforgalminap.ToString("yyyy.MM.dd");
                    }

                    Adat_Szerelvény EgySzerelvény = (from a in AdatokSzerelvény
                                                     where a.Szerelvény_ID == EgyJármű.Szerelvénykocsik
                                                     select a).FirstOrDefault();
                    if (EgySzerelvény != null)
                    {
                        string ideig = EgySzerelvény.Kocsi1.Trim();
                        ideig += EgySzerelvény.Kocsi2.Trim() == "0" ? "" : "-" + EgySzerelvény.Kocsi2.Trim();
                        ideig += EgySzerelvény.Kocsi3.Trim() == "0" ? "" : "-" + EgySzerelvény.Kocsi3.Trim();
                        ideig += EgySzerelvény.Kocsi4.Trim() == "0" ? "" : "-" + EgySzerelvény.Kocsi4.Trim();
                        ideig += EgySzerelvény.Kocsi5.Trim() == "0" ? "" : "-" + EgySzerelvény.Kocsi5.Trim();
                        ideig += EgySzerelvény.Kocsi6.Trim() == "0" ? "" : "-" + EgySzerelvény.Kocsi6.Trim();
                        Tábla.Rows[i].Cells[16].Value = ideig;
                    }

                    Adat_Nap_Hiba EgyHiba = (from a in AdatokHiba
                                             where a.Azonosító == EgyJármű.Azonosító
                                             select a).FirstOrDefault();
                    if (EgyHiba != null)
                    {
                        Tábla.Rows[i].Cells[6].Value = EgyHiba.Üzemképtelen.Trim() + "-" + EgyHiba.Beálló.Trim() + "-" + EgyHiba.Üzemképeshiba.Trim();
                    }
                    Adat_Vezénylés EgyVezénylés = (from a in AdatokVezénylés
                                                   where a.Azonosító == EgyJármű.Azonosító
                                                   select a).FirstOrDefault();
                    if (EgyVezénylés != null)
                    {
                        // előző napi
                        if (EgyVezénylés.Dátum.ToString("MM-dd-yyyy") == Dátum.Value.AddDays(-1).ToString("MM-dd-yyyy"))
                        {
                            Tábla.Rows[i].Cells[8].Value = EgyVezénylés.Vizsgálat.Trim();
                            Tábla.Rows[i].Cells[9].Value = EgyVezénylés.Vizsgálat.Trim() + "-" + EgyVezénylés.Dátum.ToString("MM.dd") + "-e";
                        }
                        // aznapi
                        else if (EgyVezénylés.Dátum.ToString("MM-dd-yyyy") == Dátum.Value.ToString("MM-dd-yyyy"))
                            Tábla.Rows[i].Cells[9].Value = EgyVezénylés.Vizsgálat.Trim() + "-" + EgyVezénylés.Dátum.ToString("MM.dd") + "-a";
                        else
                            Tábla.Rows[i].Cells[9].Value = EgyVezénylés.Vizsgálat.Trim() + "-" + EgyVezénylés.Dátum.ToString("MM.dd") + "-u";

                    }

                    Adat_Vezénylés EgyVezénylésN = (from a in AdatokVezénylésN
                                                    where a.Azonosító == EgyJármű.Azonosító
                                                    select a).FirstOrDefault();
                    if (EgyVezénylésN != null)
                    {
                        Tábla.Rows[i].Cells[22].Value = EgyVezénylésN.Vizsgálat;
                        Tábla.Rows[i].Cells[24].Value = EgyVezénylésN.Státus;
                        Tábla.Rows[i].Cells[25].Value = EgyVezénylésN.Vizsgálatraütemez;
                        Tábla.Rows[i].Cells[27].Value = EgyVezénylésN.Rendelésiszám;
                    }

                    Adat_Szerelvény EgySzerElő = (from a in AdatokSzerElő
                                                  where a.Kocsi1 == EgyJármű.Azonosító || a.Kocsi2 == EgyJármű.Azonosító || a.Kocsi3 == EgyJármű.Azonosító ||
                                                        a.Kocsi4 == EgyJármű.Azonosító || a.Kocsi5 == EgyJármű.Azonosító || a.Kocsi6 == EgyJármű.Azonosító
                                                  select a).FirstOrDefault();
                    if (EgySzerElő != null)
                    {
                        string ideig = EgySzerElő.Kocsi1.Trim();
                        ideig += EgySzerElő.Kocsi2.Trim() == "0" ? "" : "-" + EgySzerElő.Kocsi2.Trim();
                        ideig += EgySzerElő.Kocsi3.Trim() == "0" ? "" : "-" + EgySzerElő.Kocsi3.Trim();
                        ideig += EgySzerElő.Kocsi4.Trim() == "0" ? "" : "-" + EgySzerElő.Kocsi4.Trim();
                        ideig += EgySzerElő.Kocsi5.Trim() == "0" ? "" : "-" + EgySzerElő.Kocsi5.Trim();
                        ideig += EgySzerElő.Kocsi6.Trim() == "0" ? "" : "-" + EgySzerElő.Kocsi6.Trim();

                        Tábla.Rows[i].Cells[13].Value = EgySzerElő.Szerelvény_ID;
                        Tábla.Rows[i].Cells[14].Value = ideig;
                    }

                    Adat_Osztály_Adat EgyOszt = (from a in AdatokOszt
                                                 where a.Azonosító == EgyJármű.Azonosító
                                                 select a).FirstOrDefault();
                    if (EgyOszt != null)
                        Tábla.Rows[i].Cells[29].Value = KézOszt.Érték(EgyOszt, "Csatolhatóság");

                    Adat_Jármű_Vendég EgyIdegen = (from a in AdatokIdegen
                                                   where a.Azonosító == EgyJármű.Azonosító
                                                   select a).FirstOrDefault();
                    if (EgyIdegen != null)
                        Tábla.Rows[i].Cells[7].Value = EgyIdegen.KiadóTelephely;

                    Adat_T5C5_Kmadatok EgyKm = (from a in AdatokKM
                                                where a.Azonosító == EgyJármű.Azonosító
                                                && a.Törölt == false
                                                orderby a.Vizsgdátumk descending
                                                select a).FirstOrDefault();
                    if (EgyKm != null)
                    {
                        if (EgyKm.KövV_sorszám != 0)
                            Tábla.Rows[i].Cells[28].Value = EgyKm.KövV_sorszám;
                        else
                            Tábla.Rows[i].Cells[28].Value = 0;

                        if (EgyKm.KövV.Trim() != "")
                            Tábla.Rows[i].Cells[10].Value = EgyKm.KövV.Trim();
                        else
                            Tábla.Rows[i].Cells[10].Value = "_";

                        Tábla.Rows[i].Cells[17].Value = EgyKm.KövV2;
                        Tábla.Rows[i].Cells[18].Value = EgyKm.KMUkm - EgyKm.V2V3Számláló;
                        Tábla.Rows[i].Cells[30].Value = EgyKm.KMUkm - EgyKm.Vizsgkm;
                        Tábla.Rows[i].Cells[32].Value = EgyKm.KMUdátum.ToString("yyyy.MM.dd");

                        List<Adat_Főkönyv_Zser_Km> SzűrtAdat = (from a in AdatokZserKm
                                                                where a.Dátum > EgyKm.KMUdátum &&
                                                                a.Azonosító == EgyJármű.Azonosító.Trim()
                                                                select a).ToList();
                        int Napikm = SzűrtAdat.Sum(a => a.Napikm);
                        Tábla.Rows[i].Cells[31].Value = Napikm;
                        Tábla.Rows[i].Cells[11].Value = (EgyKm.KMUkm - EgyKm.Vizsgkm) + Napikm;
                    }
                }

                // futásnap emelkedő
                Tábla.Sort(Tábla.Columns[5], System.ComponentModel.ListSortDirection.Descending);
                Tábla_Színezés();

                Tábla.Refresh();
                Tábla.ClearSelection();
                Tábla.Visible = true;
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

        private void Vezénylés_Lista_feltöltés()
        {
            try
            {
                AdatokVezénylés.Clear();
                AdatokVezénylés = KézVezénylés.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);
                AdatokVezénylés = (from a in AdatokVezénylés
                                   where a.Dátum >= DateTime.Today
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

        private void Vezénylés_listázása()
        {
            try
            {
                Vezénylés_Lista_feltöltés();
                Holtart.Be();

                for (int sor = 0; sor < Tábla.Rows.Count; sor++)
                {
                    string pályaszám = Tábla.Rows[sor].Cells[0].Value.ToStrTrim();

                    Adat_Vezénylés EgyVezénylés = (from a in AdatokVezénylés
                                                   where a.Azonosító == pályaszám
                                                   select a).FirstOrDefault();
                    if (EgyVezénylés != null)
                    {
                        // előző napi
                        if (EgyVezénylés.Dátum.ToString("MM-dd-yyyy") == Dátum.Value.AddDays(-1).ToString("MM-dd-yyyy"))
                        {
                            Tábla.Rows[sor].Cells[8].Value = EgyVezénylés.Vizsgálat.Trim();
                            Tábla.Rows[sor].Cells[9].Value = EgyVezénylés.Vizsgálat.Trim() + "-" + EgyVezénylés.Dátum.ToString("MM.dd") + "-e";
                        }
                        // aznapi
                        else if (EgyVezénylés.Dátum.ToString("MM-dd-yyyy") == Dátum.Value.ToString("MM-dd-yyyy"))
                            Tábla.Rows[sor].Cells[9].Value = EgyVezénylés.Vizsgálat.Trim() + "-" + EgyVezénylés.Dátum.ToString("MM.dd") + "-a";
                        else
                            Tábla.Rows[sor].Cells[9].Value = EgyVezénylés.Vizsgálat.Trim() + "-" + EgyVezénylés.Dátum.ToString("MM.dd") + "-u";

                    }
                    Holtart.Lép();
                }
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

        private void VezénylésN_Lista_feltöltés()
        {
            try
            {
                AdatokVezénylésN.Clear();
                //    Vezénylés
                AdatokVezénylésN = KézVezénylés.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);
                AdatokVezénylésN = (from a in AdatokVezénylésN
                                    where a.Dátum >= Dátum.Value
                                    && a.Törlés == 0
                                    orderby a.Szerelvényszám, a.Azonosító
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

        private void Vezénylés_listázása_napi()
        {
            try
            {
                Holtart.Be();
                VezénylésN_Lista_feltöltés();
                for (int sor = 0; sor < Tábla.Rows.Count; sor++)
                {
                    string pályaszám = Tábla.Rows[sor].Cells[0].Value.ToStrTrim();

                    Adat_Vezénylés EgyVezénylésN = (from a in AdatokVezénylésN
                                                    where a.Azonosító == pályaszám
                                                    select a).FirstOrDefault();
                    if (EgyVezénylésN != null)
                    {
                        Tábla.Rows[sor].Cells[22].Value = EgyVezénylésN.Vizsgálat.Trim();
                        Tábla.Rows[sor].Cells[24].Value = EgyVezénylésN.Státus;
                        Tábla.Rows[sor].Cells[25].Value = EgyVezénylésN.Vizsgálatraütemez;
                        Tábla.Rows[sor].Cells[27].Value = EgyVezénylésN.Rendelésiszám.Trim();
                    }
                    Holtart.Lép();
                }
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

        private void Btn_Szerelvénylista_Click(object sender, EventArgs e)
        {
            try
            {
                AlsóPanel1 = "szerelvény";

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 13;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Típus";
                Tábla.Columns[0].Width = 85;
                for (int ii = 1; ii <= 6; ii++)
                {
                    Tábla.Columns[(ii - 1) * 2 + 1].HeaderText = $"Psz{ii}";
                    Tábla.Columns[(ii - 1) * 2 + 1].Width = 85;
                    Tábla.Columns[1 + (ii - 1) * 2 + 1].HeaderText = $"Futásnap{ii}";
                    Tábla.Columns[1 + (ii - 1) * 2 + 1].Width = 85;
                }

                AdatokSzerelvény = KézSzerelvény.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adatok = KézÁllomány.Lista_Adatok("Főmérnökség", DateTime.Today).Where(a => a.Telephely == Cmbtelephely.Text.Trim()).ToList();
                AdatokJármű = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                Holtart.Be(AdatokSzerelvény.Count + 1);

                int i;
                foreach (Adat_Szerelvény rekord in AdatokSzerelvény)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    if (rekord.Kocsi1 != "0")
                    {
                        Tábla.Rows[i].Cells[0].Value = PszTípus(rekord.Kocsi1);
                        Tábla.Rows[i].Cells[1].Value = rekord.Kocsi1;
                        Tábla.Rows[i].Cells[2].Value = PszFutás(rekord.Kocsi1);
                    }
                    if (rekord.Kocsi2 != "0")
                    {
                        Tábla.Rows[i].Cells[3].Value = rekord.Kocsi2.Trim();
                        Tábla.Rows[i].Cells[4].Value = PszFutás(rekord.Kocsi2);
                    }
                    if (rekord.Kocsi3 != "0")
                    {
                        Tábla.Rows[i].Cells[5].Value = rekord.Kocsi3.Trim();
                        Tábla.Rows[i].Cells[6].Value = PszFutás(rekord.Kocsi3);
                    }
                    if (rekord.Kocsi4 != "0")
                    {
                        Tábla.Rows[i].Cells[7].Value = rekord.Kocsi4.Trim();
                        Tábla.Rows[i].Cells[8].Value = PszFutás(rekord.Kocsi4);
                    }
                    if (rekord.Kocsi5 != "0")
                    {
                        Tábla.Rows[i].Cells[9].Value = rekord.Kocsi5.Trim();
                        Tábla.Rows[i].Cells[10].Value = PszFutás(rekord.Kocsi5);
                    }
                    if (rekord.Kocsi6 != "0")
                    {
                        Tábla.Rows[i].Cells[11].Value = rekord.Kocsi6.Trim();
                        Tábla.Rows[i].Cells[12].Value = PszFutás(rekord.Kocsi6);
                    }
                    Holtart.Lép();
                }

                Holtart.Ki();
                Tábla.Refresh();
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

        private string PszTípus(string pályaszám)
        {
            string válasz = "";
            válasz = (from a in AdatokJármű
                      where a.Azonosító == pályaszám
                      select a.Típus).FirstOrDefault();

            return válasz;
        }

        private int PszFutás(string pályaszám)
        {
            int válasz = 0;
            válasz = (from a in Adatok
                      where a.Azonosító == pályaszám
                      select a.Futásnap).FirstOrDefault();

            return válasz;
        }

        private void Btn_hónaplistázás_Click(object sender, EventArgs e)
        {
            try
            {
                AlsóPanel1 = "havi";
                List<Adat_T5C5_Havi_Nap> AdatokNapok = KézNapok.Lista_Adatok(Dátum.Value).Where(a => a.Telephely == Cmbtelephely.Text.Trim()).ToList();

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 33;

                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Pályaszám";
                Tábla.Columns[0].Width = 85;
                Tábla.Columns[1].HeaderText = "Futásnap";
                Tábla.Columns[1].Width = 80;
                for (int ii = 1; ii <= 31; ii++)
                {
                    Tábla.Columns[ii + 1].HeaderText = ii.ToString();
                    Tábla.Columns[ii + 1].Width = 40;
                }

                Holtart.Be(AdatokNapok.Count + 1);

                foreach (Adat_T5C5_Havi_Nap rekord in AdatokNapok)
                {
                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Azonosító;
                    Tábla.Rows[i].Cells[1].Value = rekord.Futásnap;

                    Tábla.Rows[i].Cells[2].Value = rekord.N1;
                    Tábla.Rows[i].Cells[3].Value = rekord.N2;
                    Tábla.Rows[i].Cells[4].Value = rekord.N3;
                    Tábla.Rows[i].Cells[5].Value = rekord.N4;
                    Tábla.Rows[i].Cells[6].Value = rekord.N5;
                    Tábla.Rows[i].Cells[7].Value = rekord.N6;
                    Tábla.Rows[i].Cells[8].Value = rekord.N7;
                    Tábla.Rows[i].Cells[9].Value = rekord.N8;
                    Tábla.Rows[i].Cells[10].Value = rekord.N9;
                    Tábla.Rows[i].Cells[11].Value = rekord.N10;

                    Tábla.Rows[i].Cells[12].Value = rekord.N11;
                    Tábla.Rows[i].Cells[13].Value = rekord.N12;
                    Tábla.Rows[i].Cells[14].Value = rekord.N13;
                    Tábla.Rows[i].Cells[15].Value = rekord.N14;
                    Tábla.Rows[i].Cells[16].Value = rekord.N15;
                    Tábla.Rows[i].Cells[17].Value = rekord.N16;
                    Tábla.Rows[i].Cells[18].Value = rekord.N17;
                    Tábla.Rows[i].Cells[19].Value = rekord.N18;
                    Tábla.Rows[i].Cells[20].Value = rekord.N19;
                    Tábla.Rows[i].Cells[21].Value = rekord.N20;

                    Tábla.Rows[i].Cells[22].Value = rekord.N21;
                    Tábla.Rows[i].Cells[23].Value = rekord.N22;
                    Tábla.Rows[i].Cells[24].Value = rekord.N23;
                    Tábla.Rows[i].Cells[25].Value = rekord.N24;
                    Tábla.Rows[i].Cells[26].Value = rekord.N25;
                    Tábla.Rows[i].Cells[27].Value = rekord.N26;
                    Tábla.Rows[i].Cells[28].Value = rekord.N27;
                    Tábla.Rows[i].Cells[29].Value = rekord.N28;
                    Tábla.Rows[i].Cells[30].Value = rekord.N29;
                    Tábla.Rows[i].Cells[31].Value = rekord.N30;

                    Tábla.Rows[i].Cells[32].Value = rekord.N31;
                    Holtart.Lép();
                }
                Tábla.Refresh();
                Tábla.ClearSelection();
                Tábla.Visible = true;
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

        private void Tábla_Színezés()
        {
            try
            {
                if (Tábla.Rows.Count < 1) return;

                for (int sor = 0; sor < Tábla.Rows.Count; sor++)
                {
                    // cellák színezése

                    if (Tábla.Rows[sor].Cells[4].Value.ToÉrt_Int() >= 5)
                    {
                        Tábla.Rows[sor].Cells[4].Style.BackColor = Color.Orange;
                        Tábla.Rows[sor].Cells[4].Style.ForeColor = Color.White;
                        Tábla.Rows[sor].Cells[4].Style.Font = new Font("Arial Narrow", 11f, FontStyle.Italic);
                    }
                    if (Tábla.Rows[sor].Cells[8].Value != null && Tábla.Rows[sor].Cells[8].Value.ToStrTrim() == "E3")
                    {
                        Tábla.Rows[sor].Cells[8].Style.BackColor = Color.Green;
                        Tábla.Rows[sor].Cells[8].Style.ForeColor = Color.White;
                        Tábla.Rows[sor].Cells[8].Style.Font = new Font("Arial Narrow", 11f, FontStyle.Italic);
                    }
                    if (Tábla.Rows[sor].Cells[8].Value != null && Tábla.Rows[sor].Cells[8].Value.ToStrTrim() == "V1")
                    {
                        Tábla.Rows[sor].Cells[8].Style.BackColor = Color.Red;
                        Tábla.Rows[sor].Cells[8].Style.ForeColor = Color.White;
                        Tábla.Rows[sor].Cells[8].Style.Font = new Font("Arial Narrow", 11f, FontStyle.Italic);
                    }
                    if (Tábla.Rows[sor].Cells[9].Value != null)
                    {
                        string[] darab = Tábla.Rows[sor].Cells[9].Value.ToStrTrim().Split('-');
                        switch (darab[darab.Length - 1])
                        {
                            case "e":
                                {
                                    Tábla.Rows[sor].Cells[9].Style.BackColor = Color.Olive;
                                    Tábla.Rows[sor].Cells[9].Style.ForeColor = Color.White;
                                    Tábla.Rows[sor].Cells[9].Style.Font = new Font("Arial Narrow", 11f, FontStyle.Italic);
                                    break;
                                }
                            case "a":
                                {
                                    Tábla.Rows[sor].Cells[9].Style.BackColor = Color.BlueViolet;
                                    Tábla.Rows[sor].Cells[9].Style.ForeColor = Color.White;
                                    Tábla.Rows[sor].Cells[9].Style.Font = new Font("Arial Narrow", 11f, FontStyle.Italic);
                                    break;
                                }
                            case "u":
                                {
                                    Tábla.Rows[sor].Cells[9].Style.BackColor = Color.Gray;
                                    Tábla.Rows[sor].Cells[9].Style.ForeColor = Color.White;
                                    Tábla.Rows[sor].Cells[9].Style.Font = new Font("Arial Narrow", 11f, FontStyle.Italic);
                                    break;
                                }
                        }
                    }

                    // ha E3
                    if (Tábla.Rows[sor].Cells[25].Value.ToString() == "1" && Tábla.Rows[sor].Cells[27].Value.ToStrTrim() == "_")
                    {
                        Tábla.Rows[sor].Cells[5].Style.BackColor = Color.Blue;
                        Tábla.Rows[sor].Cells[5].Style.ForeColor = Color.White;
                        Tábla.Rows[sor].Cells[5].Style.Font = new Font("Arial Narrow", 11f, FontStyle.Italic);
                    }
                    // ha V1
                    if (Tábla.Rows[sor].Cells[25].Value.ToString() == "1" && Tábla.Rows[sor].Cells[27].Value.ToStrTrim() != "_")
                    {
                        Tábla.Rows[sor].Cells[5].Style.BackColor = Color.Yellow;
                        Tábla.Rows[sor].Cells[5].Style.ForeColor = Color.Black;
                        Tábla.Rows[sor].Cells[5].Style.Font = new Font("ThenArial Narrow", 11f, FontStyle.Italic);
                    }
                    if (Tábla.Rows[sor].Cells[23].Value != null)
                    {
                        switch (Tábla.Rows[sor].Cells[23].Value.ToÉrt_Int())
                        {
                            case 3:
                                {
                                    // ha beálló
                                    Tábla.Rows[sor].Cells[6].Style.BackColor = Color.Yellow;
                                    Tábla.Rows[sor].Cells[6].Style.ForeColor = Color.Black;
                                    Tábla.Rows[sor].Cells[6].Style.Font = new Font("ThenArial Narrow", 11f, FontStyle.Italic);
                                    break;
                                }
                            case 4:
                                {
                                    // ha BM
                                    Tábla.Rows[sor].Cells[6].Style.BackColor = Color.Red;
                                    Tábla.Rows[sor].Cells[6].Style.ForeColor = Color.White;
                                    Tábla.Rows[sor].Cells[6].Style.Font = new Font("ThenArial Narrow", 11f, FontStyle.Italic);
                                    break;
                                }
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

        private void Ütemezettkocsik()
        {
            try
            {
                Ütemezés_lista.Items.Clear();
                VezénylésN_Lista_feltöltés();

                List<Adat_Vezénylés> Adatok = (from a in AdatokVezénylésN
                                               where a.Dátum == Dátum.Value && a.Törlés == 0
                                               select a).ToList();
                long szerelvény = 0;
                string szöveg1 = "";

                Ütemezés_lista.Items.Add("E3");

                foreach (Adat_Vezénylés rekord in Adatok)
                {
                    if (szerelvény == 0)
                        szerelvény = rekord.Szerelvényszám;
                    if (rekord.Vizsgálatraütemez == 1 && rekord.Vizsgálat.Trim() == "E3")
                    {
                        if (szerelvény == rekord.Szerelvényszám)
                        {
                            szöveg1 += rekord.Azonosító.Trim() + "-";
                        }
                        else
                        {
                            if (szöveg1.Trim() != "")
                            {
                                Ütemezés_lista.Items.Add(szöveg1);
                                szöveg1 = "";
                            }
                            szöveg1 = rekord.Azonosító.Trim() + "-";
                            szerelvény = rekord.Szerelvényszám;
                        }
                    }
                }
                if (szöveg1.Trim() != "")
                {
                    Ütemezés_lista.Items.Add(szöveg1);
                    szöveg1 = "";
                }

                Ütemezés_lista.Items.Add("");
                szöveg1 = "";
                Ütemezés_lista.Items.Add("V1");
                szerelvény = 0;
                foreach (Adat_Vezénylés rekord in Adatok)
                {
                    if (szerelvény == 0)
                        szerelvény = rekord.Szerelvényszám;
                    if (rekord.Vizsgálatraütemez == 1 && rekord.Vizsgálat.Trim() == "V1")
                    {
                        Ütemezés_lista.Items.Add(rekord.Azonosító.Trim() + " - " + rekord.Rendelésiszám.Trim());
                    }
                }

                Ütemezés_lista.Items.Add("");
                szöveg1 = "";

                if (szöveg1.Trim() != "")
                {
                    Ütemezés_lista.Items.Add(szöveg1);
                    szöveg1 = "";
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

        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            Ütemezettkocsik();
        }
        #endregion


        #region Keresés
        private void Keresés_Click(object sender, EventArgs e)
        {
            Keresés_metódus();
        }

        void Keresés_metódus()
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
                if (Tábla.Rows[i].Cells[0].Value.ToStrTrim() == Új_Ablak_Kereső.Keresendő.Trim())
                {
                    Tábla.Rows[i].Cells[0].Style.BackColor = Color.Orange;
                    Tábla.FirstDisplayedScrollingRowIndex = i;
                    Tábla.CurrentCell = Tábla.Rows[i].Cells[0];
                    return;
                }
            }
        }
        #endregion


        #region excel kimenetek
        private async void BtnExcelkimenet_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) return;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"T5C5_Nap_futás_{Program.PostásTelephely.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    FájlExcel_ = SaveFileDialog1.FileName;
                else
                    return;

                Holtart.Be();
                timer1.Enabled = true;
                await Task.Run(() => MyE.DataGridViewToExcel(FájlExcel_, Tábla));
                timer1.Enabled = false;
                Holtart.Ki();

                MessageBox.Show("Elkészült az Excel tábla: " + FájlExcel_, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(FájlExcel_);
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

        private void Btn_vezénylésexcel_Click(object sender, EventArgs e)
        {
            try
            {
                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Vezénylés készítés mentése Excel fájlba",
                    FileName = $"Vezénylés-{Program.PostásNév.Trim()}-{Dátum.Value:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else

                    return;

                Holtart.Be();
                // megnyitjuk
                MyE.ExcelLétrehozás();

                MyE.Munkalap_betű("Times New Roman CE", 24);
                string munkalap = "Munka1";

                // oszlop szélességek beállítása
                MyE.Oszlopszélesség(munkalap, "A:A", 3);
                MyE.Oszlopszélesség(munkalap, "C:C", 3);
                MyE.Oszlopszélesség(munkalap, "B:B", 90);

                // az első sor színezése
                MyE.Háttérszín("A1:C1", Color.Yellow);

                // Két széle színez
                for (int i = 2; i <= 8; i++)
                {
                    MyE.Háttérszín("A" + i.ToString(), Color.Yellow);
                    MyE.Háttérszín("C" + i.ToString(), Color.Yellow);
                }

                // képet beilleszt
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Ábrák\Villamos_T5C5.png";
                if (File.Exists(hely)) MyE.Kép_beillesztés(munkalap, "A1", hely, 40, 30, 200, 450);
                Holtart.Lép();
                int sor = 8;
                MyE.Kiir("Feladatterv", "b" + sor.ToString());
                MyE.Igazít_vízszintes("B" + sor, "közép");
                sor += 1;
                // kiírjuk a dátumot
                MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 45);

                // két széle sárga
                MyE.Háttérszín("A" + sor.ToString(), Color.Yellow);
                MyE.Háttérszín("C" + sor.ToString(), Color.Yellow);
                MyE.Kiir(Dátum.Value.ToString("yyyy.MMMM.dd."), "B" + sor.ToString());
                MyE.Igazít_vízszintes("B" + sor, "közép");
                MyE.Betű("b" + sor.ToString(), 36);

                Holtart.Lép();
                sor += 1;
                // kiírjuk az E3
                MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 45);

                // két széle sárga
                MyE.Háttérszín("A" + sor.ToString(), Color.Yellow);
                MyE.Háttérszín("C" + sor.ToString(), Color.Yellow);

                MyE.Kiir("E3", "b" + sor.ToString());
                MyE.Betű("B" + sor.ToString(), 36);
                MyE.Igazít_vízszintes("B" + sor, "közép");

                // megnyitjuk a táblázatot
                string szöveg1 = "";
                long szerelvény = 0;

                List<Adat_Vezénylés> Adatok = KézVezénylés.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value);
                Adatok = (from a in Adatok
                          where a.Törlés == 0
                          && a.Dátum == Dátum.Value
                          orderby a.Szerelvényszám, a.Azonosító
                          select a).ToList();
                Holtart.Lép();
                foreach (Adat_Vezénylés rekord in Adatok)
                {
                    if (szerelvény == 0)
                        szerelvény = rekord.Szerelvényszám;
                    if (rekord.Vizsgálatraütemez == 1 & rekord.Vizsgálat.Trim() == "E3")
                    {
                        if (szerelvény == rekord.Szerelvényszám)
                        {
                            szöveg1 += rekord.Azonosító.Trim() + "-";
                        }
                        else
                        {
                            if (szöveg1.Trim() != "")
                            {
                                sor += 1;
                                MyE.Kiir(szöveg1, "B" + sor.ToString());
                                MyE.Igazít_vízszintes("B" + sor, "közép");
                                MyE.Háttérszín("A" + sor.ToString(), Color.Yellow);
                                MyE.Háttérszín("C" + sor.ToString(), Color.Yellow);
                            }
                            szöveg1 = rekord.Azonosító.Trim() + "-";
                            szerelvény = rekord.Szerelvényszám;
                        }
                    }
                }

                sor += 1;
                MyE.Kiir(szöveg1, "b" + sor.ToString());
                MyE.Igazít_vízszintes("B" + sor, "közép");
                MyE.Háttérszín("A" + sor.ToString(), Color.Yellow);
                MyE.Háttérszín("C" + sor.ToString(), Color.Yellow);

                // üres sor
                sor += 1;
                MyE.Háttérszín("A" + sor.ToString(), Color.Yellow);
                MyE.Háttérszín("C" + sor.ToString(), Color.Yellow);

                // kiírjuk az V1
                sor += 1;
                MyE.Sormagasság(sor.ToString() + ":" + sor.ToString(), 45);

                // két széle sárga
                MyE.Háttérszín("A" + sor.ToString(), Color.Yellow);
                MyE.Háttérszín("C" + sor.ToString(), Color.Yellow);
                MyE.Kiir("V1", "b" + sor.ToString());
                MyE.Igazít_vízszintes("B" + sor, "közép");
                MyE.Betű("B" + sor.ToString(), 36);

                Holtart.Lép();
                szöveg1 = "";

                foreach (Adat_Vezénylés rekord in Adatok)
                {
                    if (rekord.Vizsgálatraütemez == 1 & rekord.Vizsgálat.Trim() == "V1")
                    {
                        szöveg1 = rekord.Azonosító.Trim() + " - " + rekord.Rendelésiszám.Trim();
                        sor += 1;
                        MyE.Kiir(szöveg1, "b" + sor.ToString());
                        MyE.Igazít_vízszintes("B" + sor, "közép");
                        MyE.Háttérszín("A" + sor.ToString(), Color.Yellow);
                        MyE.Háttérszín("C" + sor.ToString(), Color.Yellow);
                    }

                }

                sor += 1;
                MyE.Háttérszín("a" + sor.ToString() + ":c" + sor.ToString(), Color.Yellow);

                // nyomtatási beállítások
                MyE.NyomtatásiTerület_részletes(munkalap, "A1:C" + sor);
                Holtart.Ki();

                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                MyE.Megnyitás(fájlexc);
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

        private void Btn_Command3_Click(object sender, EventArgs e)
        {
            try
            {
                string fájlexc;
                string munkalap = "Munka1";
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Hibalista-" + Program.PostásNév.Trim() + "-" + Dátum.Value.Year,
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                Holtart.Be();
                // megnyitjuk
                MyE.ExcelLétrehozás();

                long szerelvény = 0;

                int sor = 1;
                MyE.Kiir(Dátum.Value.ToString("yyyy.MM.dd") + "-i tervezett karbantartásokhoz járműveinek 1 hónapos hibalistája", "A" + sor.ToString());
                sor += 2;

                List<Adat_Vezénylés> AdatVez = KézVezénylés.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value); //rekord
                AdatVez = (from a in AdatVez
                           where a.Törlés == 0
                           && a.Vizsgálatraütemez == 1
                           && a.Dátum == Dátum.Value
                           orderby a.Szerelvényszám, a.Azonosító
                           select a).ToList();

                List<Adat_Menetkimaradás> AdatokhibaÖ = KézMenet.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.AddYears(-1).Year);
                List<Adat_Menetkimaradás> IdeigAdatokhiba = KézMenet.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                AdatokhibaÖ.AddRange(IdeigAdatokhiba);

                foreach (Adat_Vezénylés rekord in AdatVez)
                {

                    if (szerelvény == 0)
                        szerelvény = rekord.Szerelvényszám;

                    MyE.Kiir(rekord.Azonosító.Trim(), "A" + sor.ToString());
                    MyE.Kiir(rekord.Vizsgálat.Trim(), "B" + sor.ToString());
                    sor += 1;

                    // hibák felsorolása az aktuális évben
                    List<Adat_Menetkimaradás> Adatokhiba = (from a in AdatokhibaÖ
                                                            where a.Azonosító == rekord.Azonosító.Trim()
                                                            && a.Bekövetkezés >= Dátum.Value.AddMonths(-1)
                                                            && a.Bekövetkezés <= Dátum.Value.AddDays(1)
                                                            orderby a.Bekövetkezés descending
                                                            select a).ToList();

                    if (Adatokhiba != null && Adatokhiba.Count > 0)
                    {
                        foreach (Adat_Menetkimaradás rekordhiba in Adatokhiba)
                        {
                            MyE.Kiir(rekordhiba.Bekövetkezés.ToString(), $"c{sor}");
                            MyE.Kiir(rekordhiba.Jvbeírás.Trim(), $"d{sor}");
                            MyE.Kiir(rekordhiba.Javítás.Trim(), $"e{sor}");
                            sor += 1;
                        }
                    }

                    sor += 2;
                    Holtart.Lép();
                }

                MyE.Oszlopszélesség(munkalap, "C:C");
                MyE.Oszlopszélesség(munkalap, "D:D");
                MyE.Oszlopszélesség(munkalap, "E:E");

                // nyomtatási beállítások
                MyE.NyomtatásiTerület_részletes(munkalap, "A1:E" + sor, "", "", true);

                Holtart.Ki();
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                MyE.Megnyitás(fájlexc);
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


        #region Panel
        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) Táblázatba_kattint(e.RowIndex);
        }

        private void Táblázatba_kattint(int sor)
        {
            try
            {
                if (AlsóPanel1 == "szerelvény" || AlsóPanel1 == "havi") return;

                //Hány kocsiból áll a szerelvény
                string Tény = Tábla.Rows[sor].Cells[16].Value.ToString();
                string[] darab;
                int[] sorok;

                if (Tény != "_")
                {
                    darab = Tény.Split('-');
                    sorok = new int[darab.Length];
                    //Szerelvény járműveinek sorainak megkeresése
                    for (int i = 0; i < darab.Length; i++)
                    {
                        for (int j = 0; j < Tábla.Rows.Count; j++)
                        {
                            if (Tábla.Rows[j].Cells[0].Value.ToStrTrim() == darab[i].Trim())
                            {
                                sorok[i] = j;
                                break;
                            }

                        }
                    }
                }
                else
                {
                    darab = new string[1];
                    sorok = new int[1];
                    darab[0] = Tábla.Rows[sor].Cells[0].Value.ToString();
                    sorok[0] = sor;
                }


                Adat_T5C5_Posta Posta;
                Posta_lista.Clear();

                //Összegyűjtük a szerelvény adatait
                for (int i = 0; i < darab.Length; i++)
                {
                    Posta = new Adat_T5C5_Posta(
                                  Tábla.Rows[sorok[i]].Cells[0].Value.ToStrTrim(),//Azonosító,
                                  Tábla.Rows[sorok[i]].Cells[1].Value.ToStrTrim(),//Típus,
                                  Tábla.Rows[sorok[i]].Cells[29].Value.ToStrTrim(),//Csatolható,
                                  Tábla.Rows[sorok[i]].Cells[28].Value.ToÉrt_Int(),//V_sorszám,
                                  Tábla.Rows[sorok[i]].Cells[17].Value.ToStrTrim(),//V2_következő,
                                  Tábla.Rows[sorok[i]].Cells[18].Value.ToÉrt_Int(),//V2_Futott_Km,
                                  Tábla.Rows[sorok[i]].Cells[10].Value.ToStrTrim(),//V_Következő,
                                  Tábla.Rows[sorok[i]].Cells[11].Value.ToÉrt_Int(),// V_futott_Km,
                                  Tábla.Rows[sorok[i]].Cells[5].Value.ToÉrt_Int(),//Napszám,
                                  Tábla.Rows[sorok[i]].Cells[9].Value.ToStrTrim(),//Terv_Nap,
                                  Tábla.Rows[sorok[i]].Cells[6].Value.ToStrTrim(),//Hiba,
                                  Tábla.Rows[sorok[i]].Cells[14].Value.ToStrTrim(),//Előírt_szerelvény,
                                  Tábla.Rows[sorok[i]].Cells[16].Value.ToStrTrim(),//Tényleges_szerelvény,
                                  Tábla.Rows[sorok[i]].Cells[27].Value.ToStrTrim(),//Rendelésszám,
                                  Tábla.Rows[sorok[i]].Cells[15].Value.ToÉrt_Long(),//szerelvényszám,
                                  Tábla.Rows[sorok[i]].Cells[23].Value.ToÉrt_Int(),//Státus,
                                  Tábla.Rows[sorok[i]].Cells[4].Value.ToÉrt_Int(),//E3_sorszám,
                                  Tábla.Rows[sorok[i]].Cells[25].Value.ToÉrt_Int(),   //vizsgál
                                  Tábla.Rows[sorok[i]].Cells[24].Value.ToÉrt_Int(),
                                  "",//Kiad
                                  "", //Vissza
                                  "",//Vonal
                                  false //Terv
                                  );
                    Posta_lista.Add(Posta);
                }

                Új_Ablak_T5C5_Segéd?.Close();


                Új_Ablak_T5C5_Segéd = new Ablak_T5C5_Segéd(Posta_lista, "Nap", Dátum.Value, Cmbtelephely.Text.Trim(), false);
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

        private void Adat_módosítás()
        {
            Rész_Ürítés();
            Ütemezettkocsik();
            Vezénylés_listázása();
            Vezénylés_listázása_napi();
            Tábla.Sort(Tábla.Columns[5], System.ComponentModel.ListSortDirection.Descending);
            Tábla_Színezés();
        }

        private void Rész_Ürítés()
        {
            for (int i = 0; i < Tábla.Rows.Count; i++)
            {
                Tábla.Rows[i].Cells[9].Value = "";
                Tábla.Rows[i].Cells[22].Value = "";
                Tábla.Rows[i].Cells[24].Value = "";
                Tábla.Rows[i].Cells[25].Value = "";
                Tábla.Rows[i].Cells[27].Value = "";
            }
        }

        private void Ablak_T5C5_Segéd_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_T5C5_Segéd = null;
        }
        #endregion


        #region Gombok
        private void Btn_Vezénylésbeírás_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime holnap = DateTime.Today.AddDays(1);
                if (holnap != Dátum.Value)
                {
                    if (MessageBox.Show($"Biztos, hogy akarunk ütemezni {Dátum.Value:yyyy.MM.dd} napra ?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                        return;   // Nemet választottuk
                }
                Vezénylés.T5C5(Cmbtelephely.Text.Trim(), Dátum.Value);

                MessageBox.Show("Az adatok rögzítése befejeződött!", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                SAP_adatok.Visible = false;
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
                    FájlExcel_ = OpenFileDialog1.FileName;
                else
                {
                    SAP_adatok.Visible = true;
                    return;
                }

                Holtart.Be();
                timer1.Enabled = true;
                await Task.Run(() => SAP_Adatokbeolvasása.Km_beolvasó(FájlExcel_, "T5C5"));
                timer1.Enabled = false;
                Holtart.Ki();
                MessageBox.Show("Az adatok beolvasása megtörtént !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SAP_adatok.Visible = true;
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


        private void Ütemezés_lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Ütemezés_lista.SelectedIndex == -1)
                    return;
                if (Ütemezés_lista.Items[Ütemezés_lista.SelectedIndex].ToStrTrim() == "") return;
                if (Ütemezés_lista.Items[Ütemezés_lista.SelectedIndex].ToString().Contains("E3")) return;
                if (Ütemezés_lista.Items[Ütemezés_lista.SelectedIndex].ToString().Contains("V1")) return;

                for (int i = 0; i < Tábla.Rows.Count; i++)
                {
                    //Megkeressük a kocsihoz tartozó szerelvényt
                    if (Ütemezés_lista.Items[Ütemezés_lista.SelectedIndex].ToString().Substring(0, 4).Trim() == Tábla.Rows[i].Cells[0].Value.ToStrTrim())
                    {
                        Táblázatba_kattint(i);
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

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
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