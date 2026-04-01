using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using MyF = Függvénygyűjtemény;

namespace Villamos.Ablakok
{
    public partial class Ablak_Ideig : Form
    {
        readonly Kezelő_Kiegészítő_Sérülés KézSérülés = new Kezelő_Kiegészítő_Sérülés();
        readonly Kezelő_Belépés_Jogosultságtábla KézJogOld = new Kezelő_Belépés_Jogosultságtábla();
        readonly SQL_Kezelő_Belépés_Users KézUsers = new SQL_Kezelő_Belépés_Users();
        readonly SQL_Kezelő_Bejelentkezés_Fordító KézFordító = new SQL_Kezelő_Bejelentkezés_Fordító();
        readonly SQL_Kezelő_Belépés_Gombok KézGomb = new SQL_Kezelő_Belépés_Gombok();

        List<Adat_Bejelentkezés_Users> ÚjFelhasználók = new List<Adat_Bejelentkezés_Users>();
        public Ablak_Ideig()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_Ideig_Load(object sender, EventArgs e)
        {

        }

        private void Start()
        {
            Telephelyekfeltöltése();
            Újfelhasználóklistája();
        }


        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                Cmbtelephely.Items.Add("");
                Cmbtelephely.Items.Add("Főmérnökség");
                List<Adat_Kiegészítő_Sérülés> Adatok = KézSérülés.Lista_Adatok();
                foreach (Adat_Kiegészítő_Sérülés rekord in Adatok)
                    Cmbtelephely.Items.Add(rekord.Név);
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


        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
            Neveklistája();

        }

        private void Neveklistája()
        {
            try
            {
                if (Cmbtelephely.Text.Trim() == "") return;
                List<Adat_Belépés_Jogosultságtábla> AdatokLista = KézJogOld.Lista_Adatok(Cmbtelephely.Text.Trim());


                if (AdatokLista != null)
                {
                    CmbNevekOld.Items.Clear();
                    CmbNevekOld.BeginUpdate();
                    foreach (Adat_Belépés_Jogosultságtábla Elem in AdatokLista)
                        CmbNevekOld.Items.Add(Elem.Név);

                    CmbNevekOld.EndUpdate();
                    CmbNevekOld.Refresh();
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

        private void CmbNevekOld_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CmbNevekOld.Text = CmbNevekOld.Items[CmbNevekOld.SelectedIndex].ToStrTrim();
            if (CmbNevekOld.Text.Trim() == "") return;
            // Megkeressük a dolgozót és kiíjuk a jogosultságait
            List<Adat_Belépés_Jogosultságtábla> Adatok = KézJogOld.Lista_Adatok(Cmbtelephely.Text.Trim());
            Adat_Belépés_Jogosultságtábla rekord = (from a in Adatok
                                                    where a.Név == CmbNevekOld.Text.Trim()
                                                    select a).FirstOrDefault();
            TxtJogkör.Text = rekord.Jogkörúj1;
            Program.PostásJogkör = rekord.Jogkörúj1;
        }

        private void Újfelhasználóklistája()
        {
            ÚjFelhasználók = KézUsers.Lista_Adatok().OrderBy(a => a.UserName).ToList();

            if (ÚjFelhasználók != null)
            {
                CmbFelhasználóNew.Items.Clear();
                CmbFelhasználóNew.BeginUpdate();
                foreach (Adat_Bejelentkezés_Users Elem in ÚjFelhasználók)
                    CmbFelhasználóNew.Items.Add($"{Elem.UserName}-{Elem.UserId}");

                CmbFelhasználóNew.EndUpdate();
                CmbFelhasználóNew.Refresh();
            }
        }

        private void CmbFelhasználóNew_SelectionChangeCommitted(object sender, EventArgs e)
        {
            CmbFelhasználóNew.Text = CmbFelhasználóNew.Items[CmbFelhasználóNew.SelectedIndex].ToStrTrim();
            if (CmbFelhasználóNew.Text.Trim() == "") return;
            string[] darabol = CmbFelhasználóNew.Text.Trim().Split('-');
            FelhasználóId.Value = darabol[1].ToÉrt_Int();
        }



        public static DataTable JogosultsagDataTableLekerese()
        {
            // DataTable inicializálása az oszlopokkal
            DataTable dt = new DataTable();
            dt.Columns.Add("Ablak Neve", typeof(string));
            dt.Columns.Add("Gomb Felirata", typeof(string));
            dt.Columns.Add("Gomb Kódneve", typeof(string));

            // Csak ezeket az ablakokat fogja vizsgálni a program, mert ezekben van csak jogosultság beállítás
            string[] vizsgalandoAblakok = {
                 "Ablak_alap_program_egyéb", "Ablak_alap_program_kiadás", "Ablak_alap_program_személy", "Ablak_DolgozóiLekérdezések", "Ablak_Oktatások", "Ablak_Beosztás",
                 };
            //string[] vizsgalandoAblakok = {
            //   "Ablak_reklám", "Ablak_keréknyilvántartás", "Ablak_MEO_kerék", "Ablak_sérülés", "Ablak_Jármű_takarítás_új", "Ablak_Tulajdonságok_CAF", "Ablak_IcsKcsv", "Ablak_Karbantartási_adatok", "Ablak_T5C5_fűtés", "Ablak_T5C5_napütemezés", "Ablak_T5C5_Tulajdonság", "Ablak_T5C5_Vizsgálat_ütemező", "Ablak_T5C5_futás", "Ablak_TW6000_Tulajdonság", "Ablak_Fő_Egyesített", "Ablak_Fő_Kiadás_Forte", "Ablak_Fő_Napiadatok", "Ablak_állomány", "Ablak_Főkönyv", "Ablak_kidobó", "Ablak_Behajtási", "Ablak_Szatube", "Ablak_külső", "Ablak_Rezsi", "Ablak_Épülettakarítás", "Ablak_Dolgozóialapadatok", "Ablak_Felvétel", "Ablak_Fogaskerekű_Tulajdonságok", "Ablak_Akkumulátor", "Ablak_Ciklus", "Ablak_Jármű", "Ablak_Munkalap_admin", "Ablak_munkalap_dekádoló", "Ablak_Munkalap_készítés", "Ablak_Napiadatok", "Ablak_SAP_osztály", "Ablak_Túlóra_Figyelés", "Ablak_Utasítás", "Ablak_Váltós", "Ablak_üzenet", "Ablak_technológia", "Ablak_Karbantartási_Munkalapok", "Ablak_KerékEszterga_Ütemezés", "Ablak_Nóta_Részletes", "Ablak_Nosztalgia", "Ablak_Eszterga_Segéd", "Ablak_Beosztás_kieg", "Ablak_Eszköz", "Ablak_CAF_Alapadat", "Ablak_Caf_Lista", "Ablak_CAF_Részletes", "Ablak_CAF_Segéd", "Ablak_CAF_Szín", "Ablak_ICS_KCSV_segéd", "Ablak_Főkönyv_Napi_Adatok", "Ablak_Eszterga_Adatok_Baross", "Ablak_TTP", "Ablak_TTP_Történet", "Jármű_Takarítás_Ütemezés_Segéd1", "Ablak_Karbantartási_Rendelés", "Ablak_Karbantartás_Csoport", "Karbantartás_Rögzítés", "Ablak_Fődarab", "Ablak_Vételezés", "Ablak_Üzenet_Generálás", "Ablak_Utasítás_Generálás", "Ablak_szerelvény"
            //     };

            var formTipusok = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Form)) && !t.IsAbstract)
                .Where(t => vizsgalandoAblakok.Contains(t.Name)); // Csak ami a listában van


            foreach (var tipus in formTipusok)
            {
                try
                {
                    using (Form ablak = (Form)Activator.CreateInstance(tipus))
                    {
                        // 1. ELŐKÉSZÍTÉS A REJTETT MEGJELENÍTÉSHEZ
                        ablak.StartPosition = FormStartPosition.CenterScreen;
                        //  ablak.Location = new Point(-10000, -10000); // Képernyőn kívülre tesszük
                        ablak.Opacity = 1.0; // Teljesen átlátszóvá tesszük
                        //       ablak.ShowInTaskbar = false; // Ne jelenjen meg a tálcán

                        // 2. MEGJELENÍTÉS (Ez kényszeríti a WinForms-t az állapotok frissítésére)
                        ablak.Show();

                        // Hagyunk időt a Windows-nak és a Form-nak a kirajzolásra
                        Application.DoEvents();

                        // 3. JOGOSULTSÁGOK LEFUTTATÁSA
                        MethodInfo metodus = tipus.GetMethod("Jogosultságkiosztás",
                            BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

                        if (metodus != null)
                        {
                            metodus.Invoke(ablak, null);

                            // Itt hagyunk egy pillanatot a Windows-nak az üzenetsor feldolgozására
                            Application.DoEvents();

                            var minden = MindenGombLekerese(ablak);

                            var aktivGombok = MindenGombLekerese(ablak).Where(g => g.Visible && g.Enabled);

                            foreach (var gomb in aktivGombok)
                            {
                                // Reflectionnel lekérjük a gomb SAJÁT láthatósági beállítását
                                // Ez akkor is True-t ad, ha az ablak maga rejtve van!
                                PropertyInfo pi = typeof(Control).GetProperty("Visible",
                                    BindingFlags.Instance | BindingFlags.Public);
                                bool beallitottLathatosag = (bool)pi.GetValue(gomb, null);


                                // Ablak címe (ha üres, akkor az osztály neve)
                                string ablakMegnevezes = string.IsNullOrEmpty(ablak.Text) ? ablak.Name : ablak.Text;

                                // Új sor hozzáadása a táblázathoz
                                dt.Rows.Add(
                                    ablakMegnevezes,
                                    gomb.Text.Replace("&", ""),
                                    gomb.Name
                                );
                            }
                        }
                        ablak.Hide(); // Munka végeztével elrejtjük
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Hiba a(z) {tipus.Name} vizsgálatakor: {ex.Message}");
                }
            }

            return dt;
        }

        // Ez a függvény megmondja, hogy a gomb Visible-re lett-e állítva, 
        // függetlenül attól, hogy az ablak épp látszik-e.
        private static bool IsControlVisible(Control c)
        {
            PropertyInfo prop = typeof(Control).GetProperty("Visible",
                BindingFlags.Instance | BindingFlags.Public);
            return (bool)prop.GetValue(c);
        }

        private static IEnumerable<Button> MindenGombLekerese(Control szulo)
        {
            List<Button> talaltGombok = new List<Button>();
            talaltGombok.AddRange(szulo.Controls.OfType<Button>());
            foreach (Control gyerek in szulo.Controls)
                talaltGombok.AddRange(MindenGombLekerese(gyerek));
            return talaltGombok;
        }


        private void BtnRögzít_Click(object sender, EventArgs e)
        {
            // Lekérjük az adatokat
            DataTable jogokTable = JogosultsagDataTableLekerese();

            // Összekötjük a DataGridView-val
            Tábla.DataSource = jogokTable;

            // Opcionális: Oszlopok automatikus méretezése, hogy minden látszódjon
            Tábla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }

        private void FordítóTáblaKészítő_Click(object sender, EventArgs e)
        {
            try
            {
                string fájl = $@"{Application.StartupPath}\Temp\AblakokGombok.csv";
                //Megnyitjuk a fájlt és feldolgozzuk
                List<GombAdatok> TáblaJogok = MyF.CsvToList<GombAdatok>(fájl);

                fájl = $@"{Application.StartupPath}\Temp\Telephely.csv";
                //Megnyitjuk a fájlt és feldolgozzuk
                List<LáthatóságAdatok> TáblaTulaj = MyF.CsvToList<LáthatóságAdatok>(fájl);
                List<Adat_Bejelentkezés_Gombok> AdatokGomb = KézGomb.Lista_Adatok();


                List<Adat_Bejelentkezés_Fordító> Adatok = new List<Adat_Bejelentkezés_Fordító>();

                foreach (Adat_Bejelentkezés_Gombok adat in AdatokGomb)
                {
                    GombAdatok AdatGombOld = (from a in TáblaJogok
                                              where a.AblakNev == adat.FromName
                                              && a.GombNev == adat.GombName
                                              select a).FirstOrDefault();

                    LáthatóságAdatok AdatLáthat = (from a in TáblaTulaj
                                                   where a.AblakNev == adat.FromName
                                                   && a.GombNev == adat.GombName
                                                   select a).FirstOrDefault();

                    Adat_Bejelentkezés_Fordító ADAT = new Adat_Bejelentkezés_Fordító(
                        adat.GombokId,
                        adat.FromName,
                        adat.GombName,
                        AdatLáthat.Ertek,
                        AdatGombOld == null ? 0 : AdatGombOld.MelyikElem.ToÉrt_Int(),
                        AdatGombOld == null ? 0 : AdatGombOld.EgyKettőHárom.ToÉrt_Int()
                        );
                    Adatok.Add(ADAT);
                }

                KézFordító.Rögzítés(Adatok);

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

        private void BtnRégitábla_Click(object sender, EventArgs e)
        {
            RégiAdatok.TelephelyJogosultsaga();
            RégiAdatok.GombokJogosultsaga();
        }
    }

    class GombAdatok
    {
        public string AblakNev { get; set; }
        public string GombNev { get; set; }
        public string Tulajdonsag { get; set; }
        public string Ertek { get; set; }
        public string MelyikElem { get; set; }
        public string EgyKettőHárom { get; set; }
    }

    class LáthatóságAdatok
    {
        public string AblakNev { get; set; }
        public string GombNev { get; set; }
        public string Tulajdonsag { get; set; }
        public string Ertek { get; set; }
        public string Reláció { get; set; }
    }
}