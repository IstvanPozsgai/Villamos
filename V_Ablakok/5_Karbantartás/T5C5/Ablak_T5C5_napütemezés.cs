using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Kezelők;
using Villamos.Villamos_Ablakok;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyA = Adatbázis;
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

        readonly Kezelő_T5C5_Állomány KézÁllomány = new Kezelő_T5C5_Állomány();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Szerelvény KézSzerelvény = new Kezelő_Szerelvény();
        readonly Kezelő_Nap_Hiba KézHiba = new Kezelő_Nap_Hiba();
        readonly Kezelő_Vezénylés KézVezénylés = new Kezelő_Vezénylés();
        readonly Kezelő_Szerelvény KézSzerElő = new Kezelő_Szerelvény();
        readonly Kezelő_Osztály_Adat KézOszt = new Kezelő_Osztály_Adat();
        readonly Kezelő_Jármű_Vendég KézIdegen = new Kezelő_Jármű_Vendég();
        readonly Kezelő_T5C5_Kmadatok KézKM = new Kezelő_T5C5_Kmadatok();
        readonly Kezelő_Főkönyv_Zser_Km KézKorr = new Kezelő_Főkönyv_Zser_Km();

        List<Adat_T5C5_Állomány> Adatok = new List<Adat_T5C5_Állomány>();
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
        public Ablak_T5C5_napütemezés()
        {
            InitializeComponent();
            Start();

        }


        #region Alap
        private void Start()
        {
            Telephelyekfeltöltése();
            // megnézzük, hogy van-e tábla
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\futás\" + DateTime.Today.ToString("yyyy");
            if (!Exists(hely))
                System.IO.Directory.CreateDirectory(hely);
            hely += @"\vezénylés" + DateTime.Today.ToString("yyyy") + ".mdb";

            if (!Exists(hely))
                Adatbázis_Létrehozás.Vezényléstábla(hely);

            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos6.mdb";
            if (!Exists(hely))
                Adatbázis_Létrehozás.Járműtulajdonságoktábla(hely);

            hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\villamos\szerelvényelőírt.mdb";
            if (!Exists(hely))
                Adatbázis_Létrehozás.Szerelvénytáblalap(hely);

            Jogosultságkiosztás();
            Dátum.Value = DateTime.Today;
        }



        private void Ablak_T5C5_napütemezés_Load(object sender, EventArgs e)
        {

        }


        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                Cmbtelephely.Items.AddRange(Listák.TelephelyLista_Jármű());
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


        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\T5C5_futás_ütemez.html";
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

                Főkönyv_Funkciók.Napiállók(Cmbtelephely.Text.Trim());

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
                Tábla.Columns[19].HeaderText = "";
                Tábla.Columns[19].Width = 80;
                Tábla.Columns[20].HeaderText = "";
                Tábla.Columns[20].Width = 80;
                Tábla.Columns[21].HeaderText = "";
                Tábla.Columns[21].Width = 80;
                Tábla.Columns[22].HeaderText = "V napi";
                Tábla.Columns[22].Width = 80;
                Tábla.Columns[23].HeaderText = "státus";
                Tábla.Columns[23].Width = 80;
                Tábla.Columns[24].HeaderText = "marad";
                Tábla.Columns[24].Width = 80;
                Tábla.Columns[25].HeaderText = "V";
                Tábla.Columns[25].Width = 80;
                Tábla.Columns[26].HeaderText = "státus";
                Tábla.Columns[26].Width = 80;
                Tábla.Columns[26].Visible = false;
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

                FutásAdatok_Feltöltése();
                JárműAdatok_Feltöltése();
                SzerelvényListaFeltöltése();    // Szerelvény


                //    Hiba
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\Új_napihiba.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM hiba  ORDER BY azonosító";
                AdatokHiba = KézHiba.Lista_adatok(hely, jelszó, szöveg);


                Vezénylés_Lista_feltöltés();  //Vezénylés
                VezénylésN_Lista_feltöltés(); //Napi


                // Szerelvény
                hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\villamos\szerelvényelőírt.mdb";
                szöveg = "Select * FROM szerelvénytábla ORDER BY id";
                jelszó = "pozsgaii";
                AdatokSzerElő = KézSzerElő.Lista_Adatok(hely, jelszó, szöveg);

                //Csatolhat
                hely = Application.StartupPath + @"\Főmérnökség\adatok\osztály.mdb";
                jelszó = "kéménybe";
                szöveg = "SELECT * FROM osztályadatok ORDER BY azonosító";
                AdatokOszt = KézOszt.Lista_Adat(hely, jelszó, szöveg);

                //    Idegen 
                hely = Application.StartupPath + @"\Főmérnökség\adatok\villamos.mdb";
                jelszó = "pozsgaii";
                szöveg = "SELECT * FROM vendégtábla order by azonosító";
                AdatokIdegen = KézIdegen.Lista_adatok(hely, jelszó, szöveg);

                //           KM tábla
                hely = Application.StartupPath + @"\Főmérnökség\Adatok\T5C5\Villamos4T5C5.mdb";
                jelszó = "pocsaierzsi";
                szöveg = "SELECT KMtábla.*";
                szöveg += " FROM  (SELECT KMtábla.azonosító, Max(KMtábla.vizsgdátumk) AS MaxOfvizsgdátumk FROM KMtábla WHERE törölt=False GROUP BY KMtábla.azonosító ORDER BY azonosító) AS Rész ";
                szöveg += " INNER JOIN KMtábla ON (Rész.MaxOfvizsgdátumk = KMtábla.vizsgdátumk) AND (Rész.azonosító = KMtábla.azonosító) ";
                szöveg += " WHERE törölt=False ORDER BY KMtábla.azonosító";
                AdatokKM = KézKM.Lista_Adat(hely, jelszó, szöveg);

                int i;
                Holtart.Be(Adatok.Count + 2);

                // kiírjuk az alapot
                foreach (Adat_T5C5_Állomány rekord in Adatok)
                {
                    Holtart.Lép();
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Azonosító;
                    Tábla.Rows[i].Cells[2].Value = rekord.Vizsgálatdátuma.ToString("yyyy.MM.dd");
                    Tábla.Rows[i].Cells[3].Value = rekord.Vizsgálatfokozata;
                    Tábla.Rows[i].Cells[4].Value = rekord.Vizsgálatszáma;
                    Tábla.Rows[i].Cells[5].Value = rekord.Futásnap;
                    Tábla.Rows[i].Cells[6].Value = "_";
                    Tábla.Rows[i].Cells[9].Value = "_";
                    Tábla.Rows[i].Cells[10].Value = "_";
                    Tábla.Rows[i].Cells[11].Value = 0;
                    Tábla.Rows[i].Cells[12].Value = rekord.Utolsóforgalminap.ToString("yyyy.MM.dd");
                    Tábla.Rows[i].Cells[14].Value = "_";
                    Tábla.Rows[i].Cells[15].Value = "0";
                    Tábla.Rows[i].Cells[16].Value = "_";
                    Tábla.Rows[i].Cells[24].Value = 0;
                    Tábla.Rows[i].Cells[25].Value = 0;
                    Tábla.Rows[i].Cells[26].Value = 0;
                    Tábla.Rows[i].Cells[27].Value = "_";
                    Tábla.Rows[i].Cells[31].Value = 0;

                    Adat_Jármű EgyJármű = (from a in AdatokJármű
                                           where a.Azonosító == rekord.Azonosító
                                           select a).FirstOrDefault();
                    if (EgyJármű != null)
                    {
                        Tábla.Rows[i].Cells[1].Value = EgyJármű.Valóstípus;
                        Tábla.Rows[i].Cells[15].Value = EgyJármű.Szerelvénykocsik;
                        Tábla.Rows[i].Cells[23].Value = EgyJármű.Státus;

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
                    }

                    Adat_Nap_Hiba EgyHiba = (from a in AdatokHiba
                                             where a.Azonosító == rekord.Azonosító
                                             select a).FirstOrDefault();
                    if (EgyHiba != null)
                    {
                        Tábla.Rows[i].Cells[6].Value = EgyHiba.Üzemképtelen.Trim() + "-" + EgyHiba.Beálló.Trim() + "-" + EgyHiba.Üzemképeshiba.Trim();
                    }
                    Adat_Vezénylés EgyVezénylés = (from a in AdatokVezénylés
                                                   where a.Azonosító == rekord.Azonosító
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
                                                    where a.Azonosító == rekord.Azonosító
                                                    select a).FirstOrDefault();
                    if (EgyVezénylésN != null)
                    {
                        Tábla.Rows[i].Cells[22].Value = EgyVezénylésN.Vizsgálat;
                        Tábla.Rows[i].Cells[24].Value = EgyVezénylésN.Státus;
                        Tábla.Rows[i].Cells[25].Value = EgyVezénylésN.Vizsgálatraütemez;
                        Tábla.Rows[i].Cells[27].Value = EgyVezénylésN.Rendelésiszám;
                    }

                    Adat_Szerelvény EgySzerElő = (from a in AdatokSzerElő
                                                  where a.Kocsi1 == rekord.Azonosító || a.Kocsi2 == rekord.Azonosító || a.Kocsi3 == rekord.Azonosító ||
                                                        a.Kocsi4 == rekord.Azonosító || a.Kocsi5 == rekord.Azonosító || a.Kocsi6 == rekord.Azonosító
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
                                                 where a.Azonosító == rekord.Azonosító
                                                 select a).FirstOrDefault();
                    if (EgyOszt != null)
                        Tábla.Rows[i].Cells[29].Value = EgyOszt.Adat5.Trim();

                    Adat_Jármű_Vendég EgyIdegen = (from a in AdatokIdegen
                                                   where a.Azonosító == rekord.Azonosító
                                                   select a).FirstOrDefault();
                    if (EgyIdegen != null)
                        Tábla.Rows[i].Cells[7].Value = EgyIdegen.KiadóTelephely;

                    Adat_T5C5_Kmadatok EgyKm = (from a in AdatokKM
                                                where a.Azonosító == rekord.Azonosító
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
                        Adat_Általános_String_Dátum ideig = new Adat_Általános_String_Dátum(
                                                          EgyKm.KMUdátum,
                                                          rekord.Azonosító
                                                         );
                        Frissítés.Add(ideig);
                    }
                }

                KorrekcióListaFeltöltés();
                Korrekció_km();

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

        private void JárműAdatok_Feltöltése()
        {
            try
            {
                AdatokJármű.Clear();
                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\adatok\villamos\villamos.mdb";
                if (!File.Exists(hely)) return;
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytábla ORDER BY azonosító";
                AdatokJármű = KézJármű.Lista_Adatok(hely, jelszó, szöveg);

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

        private void FutásAdatok_Feltöltése()
        {
            try
            {
                Adatok.Clear();
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\T5C5\villamos3.mdb";
                if (!File.Exists(hely)) return;
                string jelszó = "pozsgaii";
                string szöveg = $"SELECT * FROM Állománytábla where telephely='{Cmbtelephely.Text.Trim()}' ORDER BY azonosító";

                Adatok = KézÁllomány.Lista_Adat(hely, jelszó, szöveg);
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


        private void SzerelvényListaFeltöltése()
        {
            AdatokSzerelvény.Clear();
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\villamos\szerelvény.mdb";
            if (!File.Exists(hely)) return;
            string szöveg = "Select * FROM szerelvénytábla ORDER BY id";
            string jelszó = "pozsgaii";
            AdatokSzerelvény = KézSzerelvény.Lista_Adatok(hely, jelszó, szöveg);
        }

        private void KorrekcióListaFeltöltés()
        {
            try
            {
                AdatokZserKm.Clear();
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Dátum.Value.Year}\Napi_km_Zser_{Dátum.Value.Year}.mdb";
                if (!Exists(hely)) return;

                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM Tábla";
                AdatokZserKm = KézKorr.Lista_adatok(hely, jelszó, szöveg);
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

        private void Korrekció_km()
        {
            try
            {
                Holtart.Be(Frissítés.Count + 1);

                foreach (Adat_Általános_String_Dátum elem in Frissítés)
                {
                    List<Adat_Főkönyv_Zser_Km> SzűrtAdat = (from a in AdatokZserKm
                                                            where a.Dátum >= elem.Dátum &&
                                                            a.Azonosító == elem.Szöveg.Trim()
                                                            select a).ToList();
                    int Napikm = SzűrtAdat.Sum(a => a.Napikm);
                    Adat_Főkönyv_Zser_Km ideig = new Adat_Főkönyv_Zser_Km(
                                  elem.Szöveg.Trim(),
                                  elem.Dátum,
                                  Napikm,
                                  "");
                    AdatokKorr.Add(ideig);

                    Holtart.Lép();
                }

                Holtart.Be(Tábla.Rows.Count + 1);

                for (int sor = 0; sor < Tábla.Rows.Count; sor++)
                {
                    string pályaszám = Tábla.Rows[sor].Cells[0].Value.ToStrTrim();

                    Adat_T5C5_Kmadatok EgyKm = (from a in AdatokKM
                                                where a.Azonosító == pályaszám
                                                select a).FirstOrDefault();
                    if (EgyKm != null)
                    {
                        int Napikm = (from a in AdatokKorr
                                      where a.Azonosító == pályaszám
                                      select a.Napikm).FirstOrDefault();
                        Tábla.Rows[sor].Cells[31].Value = Napikm;
                        Tábla.Rows[sor].Cells[11].Value = (EgyKm.KMUkm - EgyKm.Vizsgkm) + Napikm;
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

        private void Vezénylés_Lista_feltöltés()
        {
            try
            {
                AdatokVezénylés.Clear();
                //    Vezénylés
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\futás\{Dátum.Value.Year}\vezénylés{Dátum.Value.Year}.mdb";
                string jelszó = "tápijános";
                string szöveg = $"SELECT * FROM vezényléstábla where   [dátum]>=#{DateTime.Today.AddDays(-1):MM-dd-yyyy}# and [törlés]=0 ORDER BY azonosító";
                AdatokVezénylés = KézVezénylés.Lista_Adatok(hely, jelszó, szöveg);

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
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\futás\{Dátum.Value.Year}\vezénylés{Dátum.Value.Year}.mdb";
                string jelszó = "tápijános";
                string szöveg = $"SELECT * FROM vezényléstábla where   [dátum]>=#{Dátum.Value:MM-dd-yyyy}# and [törlés]=0 ORDER BY szerelvényszám, azonosító";

                AdatokVezénylésN = KézVezénylés.Lista_Adatok(hely, jelszó, szöveg);

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
                    Tábla.Columns[(ii - 1) * 2 + 1].HeaderText = "Psz";
                    Tábla.Columns[(ii - 1) * 2 + 1].Width = 85;
                    Tábla.Columns[1 + (ii - 1) * 2 + 1].HeaderText = "Futásnap";
                    Tábla.Columns[1 + (ii - 1) * 2 + 1].Width = 85;
                }

                SzerelvényListaFeltöltése();
                FutásAdatok_Feltöltése();
                JárműAdatok_Feltöltése();


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
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\T5C5\" + Dátum.Value.ToString("yyyy") + @"\havi" + Dátum.Value.ToString("yyyyMM") + ".mdb";
                string jelszó = "pozsgaii";

                string szöveg = "SELECT * FROM állománytábla WHERE telephely='" + Cmbtelephely.Text.Trim() + "' order by azonosító";
                if (!Exists(hely))
                    return;


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

                Kezelő_T5C5_Havi_Nap KézNapok = new Kezelő_T5C5_Havi_Nap();
                List<Adat_T5C5_Havi_Nap> AdatokNapok = KézNapok.Lista_Adat(hely, jelszó, szöveg);
                Holtart.Be(AdatokNapok.Count + 1);
                int i;
                foreach (Adat_T5C5_Havi_Nap rekord in AdatokNapok)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
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

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\futás\{Dátum.Value.Year}";
                if (!Exists(hely)) System.IO.Directory.CreateDirectory(hely);

                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\futás\{Dátum.Value.Year}\vezénylés{Dátum.Value.Year}.mdb";
                if (!Exists(hely)) Adatbázis_Létrehozás.Vezényléstábla(hely);

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
            if (Új_Ablak_Kereső.Keresendő == null)
                return;
            if (Új_Ablak_Kereső.Keresendő.Trim() == "")
                return;

            if (Tábla.Rows.Count < 0)
                return;

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
        private void BtnExcelkimenet_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",

                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "T5C5_Nap_futás_" + Program.PostásTelephely.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, Tábla, true);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Module_Excel.Megnyitás(fájlexc + ".xlsx");
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
                    FileName = "Vezénylés-" + Program.PostásNév.Trim() + "-" + Dátum.Value.ToString("yyyyMMdd"),
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
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\képek\Villamos.png";
                MyE.Kép_beillesztés(munkalap, "A1", hely, 40, 30, 200, 450);
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
                hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\futás\" + Dátum.Value.ToString("yyyy") + @"\vezénylés" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "tápijános";
                string szöveg = "SELECT * FROM vezényléstábla WHERE [törlés]=0 and [dátum]=#" + Dátum.Value.ToString("M-d-yy") + "#";
                szöveg += " ORDER BY szerelvényszám, azonosító";

                string szöveg1 = "";
                long szerelvény = 0;

                Kezelő_Vezénylés kéz = new Kezelő_Vezénylés();
                List<Adat_Vezénylés> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
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

                string helyhiba = "";
                string helyhibaelőző = "";
                string szöveghiba;
                long szerelvény = 0;
                string jelszóhiba = "lilaakác";

                helyhibaelőző = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\menet" + Dátum.Value.AddYears(-1).Year + ".mdb";
                helyhiba = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\menet" + Dátum.Value.Year + ".mdb";

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\futás\" + Dátum.Value.Year + @"\vezénylés" + Dátum.Value.Year + ".mdb";
                string jelszó = "tápijános";
                string szöveg = "SELECT * FROM vezényléstábla where [törlés]=0 AND vizsgálatraütemez = 1 AND [dátum]=#" + Dátum.Value.ToString("M-d-yy") + "#";
                szöveg += " ORDER BY szerelvényszám, azonosító";

                int sor = 1;
                MyE.Kiir(Dátum.Value.ToString("yyyy.MM.dd") + "-i tervezett karbantartásokhoz járműveinek 1 hónapos hibalistája", "A" + sor.ToString());
                sor += 2;

                Kezelő_Vezénylés kézvez = new Kezelő_Vezénylés();
                List<Adat_Vezénylés> AdatVez = kézvez.Lista_Adatok(hely, jelszó, szöveg); //rekord

                Kezelő_Menetkimaradás kézhiba = new Kezelő_Menetkimaradás();
                List<Adat_Menetkimaradás> Adathiba;

                foreach (Adat_Vezénylés rekord in AdatVez)
                {

                    if (szerelvény == 0)
                        szerelvény = rekord.Szerelvényszám;

                    MyE.Kiir(rekord.Azonosító.Trim(), "A" + sor.ToString());
                    MyE.Kiir(rekord.Vizsgálat.Trim(), "B" + sor.ToString());
                    sor += 1;

                    // hibák felsorolása az aktuális évben

                    szöveghiba = "SELECT * FROM menettábla where ";
                    szöveghiba += " azonosító='" + rekord.Azonosító.Trim() + "'";
                    szöveghiba += " and [bekövetkezés]>=#" + Dátum.Value.AddMonths(-1).ToString("MM-dd-yyyy") + " 00:00:0#";
                    szöveghiba += " and [bekövetkezés]<=#" + Dátum.Value.ToString("MM-dd-yyyy") + " 23:59:0#";
                    szöveghiba += " order by bekövetkezés desc";
                    if (Exists(helyhiba))
                    {
                        Adathiba = kézhiba.Lista_Adatok(helyhiba, jelszóhiba, szöveghiba);
                        foreach (Adat_Menetkimaradás rekordhiba in Adathiba)
                        {
                            MyE.Kiir(rekordhiba.Bekövetkezés.ToString(), "c" + sor.ToString());
                            MyE.Kiir(rekordhiba.Jvbeírás.Trim(), "d" + sor.ToString());
                            MyE.Kiir(rekordhiba.Javítás.Trim(), "e" + sor.ToString());
                            sor += 1;
                        }
                    }


                    if (Exists(helyhibaelőző))
                    {
                        // hibák felsorolása az előző évben
                        if (Dátum.Value.Year != Dátum.Value.AddYears(-1).Year)
                        {
                            Adathiba = kézhiba.Lista_Adatok(helyhibaelőző, jelszóhiba, szöveghiba);
                            foreach (Adat_Menetkimaradás rekordhiba in Adathiba)
                            {
                                MyE.Kiir(rekordhiba.Bekövetkezés.ToString(), "c" + sor.ToString());
                                MyE.Kiir(rekordhiba.Jvbeírás.Trim(), "d" + sor.ToString());
                                MyE.Kiir(rekordhiba.Javítás.Trim(), "e" + sor.ToString());
                                sor += 1;
                            }
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
                if (holnap.ToString("yyyy.MM.dd") != Dátum.Value.ToString("yyyy.MM.dd"))
                {
                    if (MessageBox.Show("Biztos, hogy akarunk ütemezni " + Dátum.Value.ToString("yyyy.MM.dd") + " napra ?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        // Nemet választottuk
                        return;
                    }
                }

                string helyütemez = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\főkönyv\futás\{Dátum.Value.Year}\vezénylés{Dátum.Value.Year}.mdb";
                string jelszóütemez = "tápijános";
                string szöveg = $"SELECT * FROM vezényléstábla where [törlés]=0 and [dátum]=#{Dátum.Value:M-d-yy}# order by  azonosító";
                Kezelő_Vezénylés KézVezénylés = new Kezelő_Vezénylés();
                List<Adat_Vezénylés> Adatok = KézVezénylés.Lista_Adatok(helyütemez, jelszóütemez, szöveg);


                // Módosítjuk a jármű státuszát
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\villamos\villamos.mdb";
                string jelszó = "pozsgaii";
                szöveg = "SELECT * FROM állománytábla ";
                Kezelő_Jármű KézJármű = new Kezelő_Jármű();
                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok(hely, jelszó, szöveg);

                // megnyitjuk a hibákat
                string helyhiba = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\villamos\hiba.mdb";
                szöveg = "SELECT * FROM hibatábla";
                Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();
                List<Adat_Jármű_hiba> AdatokHiba = KézHiba.Lista_adatok(helyhiba, jelszó, szöveg);

                // naplózás
                string helynapló = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\hibanapló";
                helynapló += @"\" + DateTime.Now.ToString("yyyyMM") + "hibanapló.mdb";
                if (!Exists(helynapló)) Adatbázis_Létrehozás.Hibatáblalap(helynapló);
                Holtart.Be();

                string szöveg1;
                string szöveg3;
                int talált;
                int szín;
                long státus;
                int újstátus = 0;
                string típusa = "";
                long hibáksorszáma;
                long hiba;
                DateTime mikor;

                // ha van ütemezett kocsi
                foreach (Adat_Vezénylés rekordütemez in Adatok)
                {

                    if (rekordütemez.Vizsgálatraütemez == 1)
                    {
                        // hiba leírása
                        szöveg1 = "";
                        szöveg3 = "KARÓRARUGÓ";
                        if (rekordütemez.Vizsgálatraütemez == 1)
                        {
                            if (rekordütemez.Vizsgálat.Contains("V1"))
                            {
                                for (int j = 0; j < Tábla.Rows.Count; j++)
                                {
                                    if (Tábla.Rows[j].Cells[0].Value.ToStrTrim() == rekordütemez.Azonosító.Trim())
                                    {
                                        szöveg1 += Tábla.Rows[j].Cells[10].Value.ToStrTrim() + "-" + Tábla.Rows[j].Cells[28].Value.ToStrTrim();
                                        szöveg3 = szöveg1;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                szöveg1 += rekordütemez.Vizsgálat.Trim() + " ";
                            }
                        }

                        if (rekordütemez.Státus == 4)
                        {
                            szöveg1 += "-" + rekordütemez.Dátum.ToString("yyyy.MM.dd.") + " Maradjon benn ";
                        }
                        else
                        {
                            szöveg1 += "-" + rekordütemez.Dátum.ToString("yyyy.MM.dd.") + " Beálló ";
                        }
                        // Megnézzük, hogy volt-e már rögzítve ilyen szöveg
                        talált = 0;
                        Adat_Jármű_hiba HibaElem = (from a in AdatokHiba
                                                    where a.Azonosító == rekordütemez.Azonosító
                                                    && (a.Hibaleírása.Contains(szöveg3.Trim()) || a.Hibaleírása.Contains(szöveg1.Trim()))
                                                    select a).FirstOrDefault();
                        if (HibaElem != null) talált = 1;

                        szín = 0;
                        // ha már volt ilyen szöveg rögzítve a pályaszámhoz akkor nem rögzítjük mégegyszer
                        if (talált == 0)
                        {
                            // hibák számát emeljük és státus állítjuk ha kell
                            Adat_Jármű ElemJármű = (from a in AdatokJármű
                                                    where a.Azonosító == rekordütemez.Azonosító
                                                    select a).FirstOrDefault();
                            if (ElemJármű != null)
                            {
                                szín = 1;
                                hibáksorszáma = ElemJármű.Hibák;
                                hiba = ElemJármű.Hibák + 1;
                                típusa = ElemJármű.Típus;
                                státus = ElemJármű.Státus;
                                újstátus = 0;
                                if (státus != 4) // ha 4 státusa akkor nem kell módosítani.
                                {
                                    // ha a következő napra ütemez
                                    if (DateTime.Today.AddDays(1).ToString("yyyy.MM.dd") == Dátum.Value.ToString("yyyy.MM.dd"))
                                    {
                                        if (rekordütemez.Státus == 4)
                                        {
                                            státus = 4;
                                            mikor = DateTime.Now;
                                        }
                                        else
                                        {
                                            státus = 3;
                                        }
                                    }
                                    else if (státus < 4)
                                        státus = 3;
                                }
                                else
                                {
                                    újstátus = 1;
                                }

                                // rögzítjük a villamos.mdb-be
                                szöveg = $"UPDATE állománytábla SET hibák={hiba}, ";
                                // csak akkor módosítkjuk a dátumot, ha nem áll
                                if (státus == 4 && újstátus == 0)
                                    szöveg += $" miótaáll='{DateTime.Now:yyyy.MM.dd}', ";
                                szöveg += $" státus={státus} ";
                                szöveg += $" WHERE  [azonosító]='{rekordütemez.Azonosító.Trim()}'";
                                MyA.ABMódosítás(hely, jelszó, szöveg);


                                // beírjuk a hibákat
                                // ha 7-nál kevesebb hibája van akkor rögzítjük
                                if (szín == 1)
                                {
                                    szöveg = "INSERT INTO hibatábla (létrehozta, korlát, hibaleírása, idő, javítva, típus, azonosító, hibáksorszáma ) VALUES (";
                                    szöveg += $"'{Program.PostásNév.Trim()}', ";
                                    // ha a következő napra ütemez
                                    if (DateTime.Today.AddDays(1).ToString("yyyy.MM.dd") == Dátum.Value.ToString("yyyy.MM.dd"))
                                    {
                                        if (rekordütemez.Státus == 4)
                                            szöveg += " 4, ";
                                        else
                                            szöveg += " 3, ";

                                    }
                                    else
                                    {
                                        szöveg += " 3, ";
                                    }
                                    szöveg += $"'{szöveg1.Trim()}', ";
                                    szöveg += $"'{DateTime.Now}', false, ";
                                    szöveg += $"'{típusa.Trim()}', ";
                                    szöveg += $"'{rekordütemez.Azonosító.Trim()}', {hibáksorszáma})";
                                    MyA.ABMódosítás(helyhiba, jelszó, szöveg);
                                    // naplózzuk a hibákat
                                    MyA.ABMódosítás(helynapló, jelszó, szöveg);
                                }
                            }
                        }
                    }
                    Holtart.Lép();
                }

                Holtart.Ki();
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


        private void SAP_adatok_Click(object sender, EventArgs e)
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
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                {
                    SAP_adatok.Visible = true;
                    return;
                }

                Holtart.Be();
                timer1.Enabled = true;
                FájlExcel_ = fájlexc;

                SZál_KM_Beolvasás(() =>
                { //leállítjuk a számlálót és kikapcsoljuk a holtartot.
                    timer1.Enabled = false;
                    Holtart.Ki();
                    MessageBox.Show("Az adatok beolvasása megtörtént !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });


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


        private void SZál_KM_Beolvasás(Action callback)
        {
            Thread proc = new Thread(() =>
            {
                //beolvassuk az adatokat
                SAP_Adatokbeolvasása_km.Km_beolvasó(FájlExcel_);
                this.Invoke(callback, new object[] { });
            });
            proc.Start();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            Holtart.Lép();
        }

        #endregion



        private void Ütemezés_lista_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (Ütemezés_lista.SelectedIndex == -1)
                    return;
                if (Ütemezés_lista.Items[Ütemezés_lista.SelectedIndex].ToStrTrim() == "")
                    return;
                if (Ütemezés_lista.Items[Ütemezés_lista.SelectedIndex].ToString().Contains("E3"))
                    return;
                if (Ütemezés_lista.Items[Ütemezés_lista.SelectedIndex].ToString().Contains("V1"))
                    return;

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

    }
}