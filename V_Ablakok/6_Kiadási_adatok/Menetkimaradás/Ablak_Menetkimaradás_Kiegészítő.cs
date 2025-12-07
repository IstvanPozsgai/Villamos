using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Adatszerkezet;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Menetkimaradás_Kiegészítő : Form
    {
        readonly Kezelő_MenetKimaradás_Főmérnökség KézFőmérnök = new Kezelő_MenetKimaradás_Főmérnökség();
        readonly Kezelő_Kiegészítő_Szolgálattelepei KézSzolgTelep = new Kezelő_Kiegészítő_Szolgálattelepei();
        readonly Kezelő_Menetkimaradás KézMenet = new Kezelő_Menetkimaradás();
         readonly Beállítás_Betű BeBetű10 = new Beállítás_Betű {Méret = 10 };
        //Különszálas beolvasás
        string Felelősmunkahely = "";
        string Telephely = "";
        DateTime DátumKüld = DateTime.Now;
        string Fájlexc = "";

        List<Adat_Kiegészítő_Szolgálattelepei> Lstüzemek;

        string alsópanels6;
        int Elemek_száma = 0;
        public Ablak_Menetkimaradás_Kiegészítő()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            dátumig.Value = DateTime.Now.AddDays(-1);
            dátumtól.Value = DateTime.Now.AddDays(-1);
            Lstüzemek = KézSzolgTelep.Lista_Adatok().OrderBy(a => a.Telephelynév).ToList();
        }

        private void Ablak_Menetkimaradás_Kiegészítő_Load(object sender, EventArgs e)
        {

        }

        private void Adat_Törlés_Click(object sender, EventArgs e)
        {
            try
            {
                // törli az időszak főmérnökségi adatait
                KézFőmérnök.Törlés(dátumtól.Value.Year, dátumtól.Value, dátumig.Value);
                MessageBox.Show("Az adatok törlése befejeződött!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private async void SAP_Click(object sender, EventArgs e)
        {
            try
            {
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                    Fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                timer1.Enabled = true;
                főholtart.Be();
                Felelősmunkahely = "";   // beolvassuk a felelős munkahelyet
                DátumKüld = dátumtól.Value; // beolvassuk a dátumot
                await Task.Run(() => SAP_Adatokbeolvasása.Menet_beolvasó(Telephely, DátumKüld.Year, Fájlexc, Felelősmunkahely, false));
                timer1.Enabled = false;
                főholtart.Ki();

                // kitöröljük a betöltött fájlt
                MessageBox.Show("Az adat konvertálás befejeződött!", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Beolvasott adatok feldolgozása közben lépteti a Holtartot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            főholtart.Lép();
        }

        private void Telephely_gomb_Click(object sender, EventArgs e)
        {
            try
            {  // telephelyek adatait összemásoljuk

                // a telepek adataival frisíti a főmérnökségi adatbázis adatait.
                főholtart.Be(Lstüzemek.Count + 1);
                alholtart.Be(100);
                List<Adat_Menetkimaradás_Főmérnökség> AdatokGy = new List<Adat_Menetkimaradás_Főmérnökség>();
                foreach (Adat_Kiegészítő_Szolgálattelepei Elem in Lstüzemek)
                {
                    főholtart.Lép();
                    // telephelyenként végigmegyünk az adatokon
                    List<Adat_Menetkimaradás> Adatok = KézMenet.Lista_Adatok(Elem.Telephelynév.Trim(), dátumtól.Value.Year);
                    Adatok = (from a in Adatok
                              where a.Bekövetkezés >= MyF.Nap0000(dátumtól.Value)
                              && a.Bekövetkezés <= MyF.Nap2359(dátumig.Value)
                              orderby a.Id
                              select a).ToList();
                    // Végig nézzük az adatokat

                    foreach (Adat_Menetkimaradás rekord in Adatok)
                    {
                        Adat_Menetkimaradás_Főmérnökség ADAT = new Adat_Menetkimaradás_Főmérnökség(
                                         rekord.Viszonylat,
                                         rekord.Azonosító,
                                         rekord.Típus,
                                         rekord.Eseményjele,
                                         rekord.Bekövetkezés,
                                         rekord.Kimaradtmenet,
                                         rekord.Jvbeírás.Replace('"', '°').Replace('\'', '°'),
                                         rekord.Vmbeírás,
                                         rekord.Javítás.Replace('"', '°').Replace('\'', '°'),
                                         0,// ID-t nem használjuk itt
                                         rekord.Törölt,
                                         rekord.Jelentés,
                                         rekord.Tétel,
                                         Elem.Telephelynév.Trim(),
                                         Elem.Szolgálatnév.Trim());
                        AdatokGy.Add(ADAT);
                        alholtart.Lép();
                    }
                }
                KézFőmérnök.Döntés(dátumtól.Value.Year, AdatokGy);
                főholtart.Ki();
                alholtart.Ki();
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

        private void Kimutatás_Gomb_Click(object sender, EventArgs e)
        {
            Kimutatás();
        }

        private void Kimutatás()
        {
            try
            {
                főholtart.Be(15);
                alholtart.Be();
                string[] cím = new string[6];
                string[] leírás = new string[6];
                // paraméter tábla feltöltése
                cím[1] = "Adatok_1";
                leírás[1] = "Adatbázis adatai a kiválasztott időszakban";
                cím[2] = "ABC_összesítő";
                leírás[2] = "Kiválasztott időszakban Szolgálat-telephely-ABC kimutatás";
                cím[3] = "Típus_összesítő";
                leírás[3] = "Kiválasztott időszakban Szolgálat-telephely-típus kimutatás";
                cím[4] = "Vonalankénti_összesítő";
                leírás[4] = "Kiválasztott időszakban Szolgálat-telephely-Vonal kimutatás";
                cím[5] = "Napi_bontású_adattábla";
                leírás[5] = "Kiválasztott időszakban Bada Gábor által vezetett formátum";
                string fájlexc;
                // létrehozzuk az excel táblát
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Menetkimaradás_{Program.PostásNév.Trim()}_{dátumtól.Value:yyyy-MMMM}_{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;
                //      fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyX.ExcelLétrehozás();

                // formázáshoz
                MyX.Munkalap_átnevezés("Munka1", "Tartalom");
                // ****************************************************
                // elkészítjük a lapokat
                // ****************************************************
                for (int i = 1; i <= 5; i++)
                {
                    MyX.Munkalap_Új(cím[i].Trim());
                }

                // ****************************************************
                // Elkészítjük a tartalom jegyzéket
                // ****************************************************
                string munkalap = "Tartalom";
                MyX.Munkalap_aktív("Tartalom");

                MyX.Kiir("Munkalapfül", "a1");
                MyX.Kiir("Leírás", "b1");
                for (int i = 1; i <= 5; i++)
                {
                    MyX.Link_beillesztés(munkalap, "A" + (i + 1).ToString(), cím[i].Trim());
                    MyX.Kiir(leírás[i].Trim(), "b" + (i + 1).ToString());
                }
                MyX.Oszlopszélesség(munkalap, "A:B");
                // ****************************************************
                // Elkészítjük a munkalapokat
                // ****************************************************
                főholtart.Lép();
                Adatokkiírásaexcelbe();
                if (Elemek_száma > 0)
                {
                    főholtart.Lép();
                    Abckimutatás();
                    főholtart.Lép();
                    Típuskimutatás();
                    főholtart.Lép();
                    Vonalkimutatás();
                }
                főholtart.Lép();
                Öszesítőtábla();
                MyX.Munkalap_aktív("Tartalom");
                MyX.Aktív_Cella("Tartalom", "A1");
                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();

                főholtart.Ki();
                alholtart.Ki();
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.Megnyitás(fájlexc);
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

        private void Adatokkiírásaexcelbe()
        {
            try
            {
                alsópanels6 = "";
                string munkalap = "Adatok_1";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                // fejléc elkészítése
                MyX.Kiir("viszonylat", "A5");
                MyX.Kiir("azonosító", "B5");
                MyX.Kiir("típus", "C5");
                MyX.Kiir("Eseményjele", "d5");
                MyX.Kiir("Bekövetkezés", "e5");
                MyX.Kiir("kimaradtmenet", "F5");
                MyX.Kiir("jvbeírás", "g5");
                MyX.Kiir("vmbeírás", "h5");
                MyX.Kiir("javítás", "i5");
                MyX.Kiir("törölt", "j5");
                MyX.Kiir("jelentés", "k5");
                MyX.Kiir("tétel", "l5");
                MyX.Kiir("telephely", "m5");
                MyX.Kiir("szolgálat", "n5");

                List<Adat_Menetkimaradás_Főmérnökség> Adatok = KézFőmérnök.Lista_Adatok(dátumtól.Value.Year);
                Adatok = (from a in Adatok
                          where a.Bekövetkezés >= MyF.Nap0000(dátumtól.Value)
                          && a.Bekövetkezés <= MyF.Nap2359(dátumig.Value)
                          orderby a.Bekövetkezés
                          select a).ToList();

                alholtart.Be(100);
                int i = 6;

                foreach (Adat_Menetkimaradás_Főmérnökség rekord in Adatok)
                {
                    MyX.Kiir(rekord.Viszonylat, "A" + i.ToString());
                    MyX.Kiir(rekord.Azonosító, "B" + i.ToString());
                    MyX.Kiir(rekord.Típus, "C" + i.ToString());
                    MyX.Kiir(rekord.Eseményjele, "d" + i.ToString());
                    MyX.Kiir(rekord.Bekövetkezés.ToString(), "e" + i.ToString());
                    MyX.Kiir(rekord.Kimaradtmenet.ToString(), "F" + i.ToString());
                    MyX.Kiir(rekord.Jvbeírás, "g" + i.ToString());
                    MyX.Kiir(rekord.Vmbeírás, "h" + i.ToString());
                    MyX.Kiir(rekord.Javítás, "i" + i.ToString());
                    MyX.Kiir(rekord.Kimaradtmenet.ToString(), "j" + i.ToString());
                    MyX.Kiir(rekord.Jelentés, "k" + i.ToString());
                    MyX.Kiir(rekord.Tétel.ToString(), "l" + i.ToString());
                    MyX.Kiir(rekord.Telephely, "m" + i.ToString());
                    MyX.Kiir(rekord.Szolgálat, "n" + i.ToString());

                    alholtart.Lép();
                    i++;
                }

                alsópanels6 = (i - 1).ToString();
                MyX.Oszlopszélesség(munkalap, "A:N");
                MyX.Rácsoz(munkalap, "A5:N" + alsópanels6);
                MyX.Aktív_Cella(munkalap, "A1");
                Elemek_száma = Adatok.Count;
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

        private void Abckimutatás()
        {
            try
            {
                string munkalap = "ABC_összesítő";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok_1";
                string balfelső = "A5";
                string jobbalsó = "N" + alsópanels6.Trim();
                string kimutatás_Munkalap = "ABC_összesítő";
                string Kimutatás_cella = "A8";
                string Kimutatás_név = "Kimutatás1";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("kimaradtmenet");
                összesítNév.Add("Bekövetkezés");

                Összesít_módja.Add("xlSum");
                Összesít_módja.Add("xlCount");

                sorNév.Add("Eseményjele");

                SzűrőNév.Add("szolgálat");
                SzűrőNév.Add("telephely");
                Beállítás_Kimutatás Bekimutat = new Beállítás_Kimutatás
                {
                    Munkalapnév = munkalap_adat,
                    Balfelső = balfelső,
                    Jobbalsó = jobbalsó,
                    Kimutatás_Munkalapnév = kimutatás_Munkalap,
                    Kimutatás_cella = Kimutatás_cella,
                    Kimutatás_név = Kimutatás_név,
                    ÖsszesítNév = összesítNév,
                    Összesítés_módja = Összesít_módja,
                    SorNév = sorNév,
                    OszlopNév = oszlopNév,
                    SzűrőNév = SzűrőNév
                };
                MyX.Kimutatás_Fő(Bekimutat);
                //MyX.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                //                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);
                MyX.Aktív_Cella(munkalap, "A1");
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

        private void Típuskimutatás()
        {
            try
            {

                string munkalap = "Típus_összesítő";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok_1";
                string balfelső = "A5";
                string jobbalsó = "N" + alsópanels6.Trim();
                string kimutatás_Munkalap = "Típus_összesítő";
                string Kimutatás_cella = "A8";
                string Kimutatás_név = "Kimutatás1";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("Bekövetkezés");
                összesítNév.Add("kimaradtmenet");

                Összesít_módja.Add("xlCount");
                Összesít_módja.Add("xlSum");

                sorNév.Add("típus");

                SzűrőNév.Add("szolgálat");
                SzűrőNév.Add("telephely");

                oszlopNév.Add("Eseményjele");

                Beállítás_Kimutatás Bekimutat = new Beállítás_Kimutatás
                {
                    Munkalapnév = munkalap_adat,
                    Balfelső = balfelső,
                    Jobbalsó = jobbalsó,
                    Kimutatás_Munkalapnév = kimutatás_Munkalap,
                    Kimutatás_cella = Kimutatás_cella,
                    Kimutatás_név = Kimutatás_név,
                    ÖsszesítNév = összesítNév,
                    Összesítés_módja = Összesít_módja,
                    SorNév = sorNév,
                    OszlopNév = oszlopNév,
                    SzűrőNév = SzűrőNév
                };
                MyX.Kimutatás_Fő(Bekimutat);
                //MyX.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                //                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);
                MyX.Aktív_Cella(munkalap, "A1");
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

        private void Vonalkimutatás()
        {
            try
            {
                string munkalap = "Vonalankénti_összesítő";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok_1";
                string balfelső = "A5";
                string jobbalsó = "N" + alsópanels6.Trim();
                string kimutatás_Munkalap = "Vonalankénti_összesítő";
                string Kimutatás_cella = "A8";
                string Kimutatás_név = "Kimutatás1";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("Bekövetkezés");
                összesítNév.Add("kimaradtmenet");

                Összesít_módja.Add("xlCount");
                Összesít_módja.Add("xlSum");

                sorNév.Add("viszonylat");

                SzűrőNév.Add("szolgálat");
                SzűrőNév.Add("telephely");

                oszlopNév.Add("Eseményjele");
                Beállítás_Kimutatás Bekimutat = new Beállítás_Kimutatás
                {
                    Munkalapnév = munkalap_adat,
                    Balfelső = balfelső,
                    Jobbalsó = jobbalsó,
                    Kimutatás_Munkalapnév = kimutatás_Munkalap,
                    Kimutatás_cella = Kimutatás_cella,
                    Kimutatás_név = Kimutatás_név,
                    ÖsszesítNév = összesítNév,
                    Összesítés_módja = Összesít_módja,
                    SorNév = sorNév,
                    OszlopNév = oszlopNév,
                    SzűrőNév = SzűrőNév
                };
                MyX.Kimutatás_Fő(Bekimutat);

                //MyX.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                //                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);
                MyX.Aktív_Cella(munkalap, "A1");
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

        private void Öszesítőtábla()
        {
            try
            {
                string munkalap = "Napi_bontású_adattábla";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                // elkészítjük a fejlécet
                MyX.Munkalap_betű(munkalap, BeBetű10);
                int sor = 6;
                int oszlop = 1;

                MyX.Kiir("Telephely", "A" + sor.ToString());
                MyX.Oszlopszélesség(munkalap, "A:A", 20);
                MyX.Kiir("Típus", "B" + sor.ToString());
                MyX.Oszlopszélesség(munkalap, "B:B", 15);
                MyX.Kiir("Viszonylat", "C" + sor.ToString());
                MyX.Oszlopszélesség(munkalap, "C:C", 15);
                MyX.Rácsoz(munkalap, "A6:C6");
                MyX.Egyesít(munkalap, "A5:C5");
                MyX.Vastagkeret(munkalap, "A5:C5");

                MyX.Kiir("Villamos Járműműszaki Főmérnökség " + dátumtól.Value.ToString(), "A5:C5");
                oszlop += 3;
                sor--;

                MyX.Kiir("A", MyF.Oszlopnév(oszlop) + (sor + 1).ToString());
                MyX.Kiir("Menet", MyF.Oszlopnév(oszlop + 1) + (sor + 1).ToString());
                MyX.Kiir("B", MyF.Oszlopnév(oszlop + 2) + (sor + 1).ToString());
                MyX.Kiir("Menet", MyF.Oszlopnév(oszlop + 3) + (sor + 1).ToString());
                MyX.Kiir("C", MyF.Oszlopnév(oszlop + 4) + (sor + 1).ToString());
                MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(oszlop) + ":" + MyF.Oszlopnév(oszlop + 4), 5);
                MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlop) + (sor + 1).ToString() + ":" + MyF.Oszlopnév(oszlop + 4) + (sor + 1).ToString());

                MyX.Egyesít(munkalap, "d5:h5");
                MyX.Vastagkeret(munkalap, "d5:h5");
                MyX.Kiir("Összesen", "d5:h5");
                // kiirjuk a naptári napokat
                oszlop += 5;
                alholtart.Be(33);
                for (int I = 1; I <= 31; I++)

                {
                    MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(oszlop) + ":" + MyF.Oszlopnév(oszlop + 4), 5);
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(oszlop) + sor.ToString() + ":" + MyF.Oszlopnév(oszlop + 4) + sor.ToString());
                    MyX.Kiir(I.ToString(), MyF.Oszlopnév(oszlop) + sor.ToString());
                    MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlop) + sor.ToString() + ":" + MyF.Oszlopnév(oszlop + 4) + sor.ToString());

                    MyX.Kiir("A", MyF.Oszlopnév(oszlop) + (sor + 1).ToString());
                    MyX.Kiir("Menet", MyF.Oszlopnév(oszlop + 1) + (sor + 1).ToString());
                    MyX.Kiir("B", MyF.Oszlopnév(oszlop + 2) + (sor + 1).ToString());
                    MyX.Kiir("Menet", MyF.Oszlopnév(oszlop + 3) + (sor + 1).ToString());
                    MyX.Kiir("C", MyF.Oszlopnév(oszlop + 4) + (sor + 1).ToString());

                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(oszlop) + (sor + 1).ToString() + ":" + MyF.Oszlopnév(oszlop + 4) + (sor + 1).ToString());

                    alholtart.Lép();
                    oszlop += 5;
                }
                oszlop -= 5;
                MyX.Háttérszín(munkalap, "A5:" + MyF.Oszlopnév(oszlop + 4) + "6", Color.Yellow);

                List<Adat_Menetkimaradás_Főmérnökség> Adatok = KézFőmérnök.Lista_Adatok(dátumtól.Value.Year);
                Adatok = (from a in Adatok
                          where a.Bekövetkezés >= MyF.Nap0000(dátumtól.Value)
                          && a.Bekövetkezés <= MyF.Nap2359(dátumig.Value)
                          orderby a.Telephely, a.Típus, a.Viszonylat, a.Bekövetkezés
                          select a).ToList();

                int aö = 0;
                int bö = 0;
                int cö = 0;
                long amö = 0;
                long bmö = 0;
                int j;
                int k;
                int pótoszlop;
                int nap = 1;
                string képlet;
                int napja;

                alholtart.Be(100);
                int i = 6;
                k = 1;

                string etelephely = "";
                string etípus = "";
                string eviszonylat = "";
                sor++;

                foreach (Adat_Menetkimaradás_Főmérnökség rekord in Adatok)
                {

                    if (etelephely.Trim() != rekord.Telephely.Trim() || etípus.Trim() != rekord.Típus.Trim() ||
                        eviszonylat.Trim() != rekord.Viszonylat.Trim())
                    {
                        sor++;
                        nap = 1;
                        aö = 0;
                        bö = 0;
                        cö = 0;
                        amö = 0;
                        bmö = 0;
                    }
                    napja = rekord.Bekövetkezés.Day;
                    if (nap != napja)
                    {
                        aö = 0;
                        bö = 0;
                        cö = 0;
                        amö = 0;
                        bmö = 0;
                    }
                    switch (rekord.Eseményjele.ToUpper())
                    {
                        case "A":
                            {
                                aö++;
                                amö += rekord.Kimaradtmenet;
                                break;
                            }
                        case "B":
                            {
                                bö++;
                                bmö += rekord.Kimaradtmenet;
                                break;
                            }
                        case "C":
                            {
                                cö++;
                                break;
                            }
                    }
                    napja = rekord.Bekövetkezés.Day - 1;
                    MyX.Kiir(aö.ToString(), MyF.Oszlopnév(napja * 5 + 9) + sor.ToString());
                    MyX.Kiir(amö.ToString(), MyF.Oszlopnév(napja * 5 + 10) + sor.ToString());
                    MyX.Kiir(bö.ToString(), MyF.Oszlopnév(napja * 5 + 11) + sor.ToString());
                    MyX.Kiir(bmö.ToString(), MyF.Oszlopnév(napja * 5 + 12) + sor.ToString());
                    MyX.Kiir(cö.ToString(), MyF.Oszlopnév(napja * 5 + 13) + sor.ToString());
                    nap = rekord.Bekövetkezés.Day;
                    if (etelephely.Trim() != rekord.Telephely.Trim())
                    {
                        MyX.Kiir(rekord.Telephely.Trim(), "a" + sor.ToString());
                        MyX.Kiir(rekord.Típus.Trim(), "b" + sor.ToString());
                        MyX.Kiir(rekord.Viszonylat.Trim(), "c" + sor.ToString());
                        etelephely = rekord.Telephely.Trim();
                        etípus = rekord.Típus.Trim();
                        eviszonylat = rekord.Viszonylat.Trim();
                        // összesen mezőt kitöljük
                        pótoszlop = 0;
                        képlet = "=SUM(";
                        for (j = 1; j <= 31; j++)
                        {
                            pótoszlop += 5;
                            képlet = képlet + "RC[" + pótoszlop.ToString() + "],";
                        }
                        képlet = képlet.Substring(0, képlet.Length - 1) + ")";
                        MyX.Kiir(képlet, "d" + sor.ToString());
                        MyX.Kiir(képlet, "e" + sor.ToString());
                        MyX.Kiir(képlet, "f" + sor.ToString());
                        MyX.Kiir(képlet, "g" + sor.ToString());
                        MyX.Kiir(képlet, "h" + sor.ToString());

                    }
                    if (etípus.Trim() != rekord.Típus.Trim())
                    {
                        MyX.Kiir(rekord.Típus.Trim(), "b" + sor.ToString());
                        MyX.Kiir(rekord.Viszonylat.Trim(), "c" + sor.ToString());
                        etípus = rekord.Típus.Trim();
                        eviszonylat = rekord.Viszonylat.Trim();
                        // összesen mezőt kitöljük
                        pótoszlop = 0;
                        képlet = "=SUM(";
                        for (j = 1; j <= 31; j++)
                        {
                            pótoszlop += 5;
                            képlet = képlet + "RC[" + pótoszlop.ToString() + "],";
                        }
                        képlet = képlet.Substring(0, képlet.Length - 1) + ")";
                        MyX.Kiir(képlet, "d" + sor.ToString());
                        MyX.Kiir(képlet, "e" + sor.ToString());
                        MyX.Kiir(képlet, "f" + sor.ToString());
                        MyX.Kiir(képlet, "g" + sor.ToString());
                        MyX.Kiir(képlet, "h" + sor.ToString());
                    }

                    if (eviszonylat.Trim() != rekord.Viszonylat.Trim())
                    {
                        MyX.Kiir(rekord.Viszonylat.Trim(), "c" + sor.ToString());
                        eviszonylat = rekord.Viszonylat.Trim();
                        // összesen mezőt kitöljük
                        pótoszlop = 0;
                        képlet = "=SUM(";
                        for (j = 1; j <= 31; j++)
                        {
                            pótoszlop += 5;
                            képlet = képlet + "RC[" + pótoszlop.ToString() + "],";
                        }
                        képlet = képlet.Substring(0, képlet.Length - 1) + ")";
                        MyX.Kiir(képlet, "d" + sor.ToString());
                        MyX.Kiir(képlet, "e" + sor.ToString());
                        MyX.Kiir(képlet, "f" + sor.ToString());
                        MyX.Kiir(képlet, "g" + sor.ToString());
                        MyX.Kiir(képlet, "h" + sor.ToString());
                    }

                    alholtart.Lép();
                    i++;
                }
                sor++;
                MyX.Kiir("Összesen:", "a" + sor.ToString());
                MyX.Rácsoz(munkalap, "c7:c" + sor.ToString());
                MyX.Egyesít(munkalap, "a" + sor.ToString() + ":" + "c" + sor.ToString());
                MyX.Vastagkeret(munkalap, "a" + sor.ToString() + ":" + "c" + sor.ToString());
                // utolsó sor után összesítjük
                képlet = "=SUM(R[-" + (sor - 7).ToString() + "]C:R[-1]C)";
                for (j = 4; j <= 163; j++)
                    MyX.Kiir(képlet, MyF.Oszlopnév(j) + sor.ToString());


                // formázás folytatása
                alholtart.Be(33);
                // típus formázása
                int utolsó = 7;

                for (int kk = 8; kk <= sor; kk++)
                {
                    if (MyX.Beolvas(munkalap, "b" + k.ToString()) != "")
                    {
                        MyX.Vastagkeret(munkalap, "b" + utolsó.ToString() + ":b" + (kk - 1).ToString());
                        utolsó = kk;
                    }
                }
                // naponta formáz
                utolsó = 7;

                for (k = 8; k <= sor; k++)
                {
                    if (MyX.Beolvas(munkalap, "A" + k.ToString()) != "")
                    {
                        MyX.Vastagkeret(munkalap, "A" + utolsó.ToString() + ":c" + (k - 1).ToString());
                        MyX.Vastagkeret(munkalap, "A" + utolsó.ToString() + ":A" + (k - 1).ToString());
                        MyX.Vastagkeret(munkalap, "c" + utolsó.ToString() + ":c" + (k - 1).ToString());
                        pótoszlop = 4;
                        for (j = 0; j <= 31; j++)
                        {
                            MyX.Rácsoz(munkalap, MyF.Oszlopnév(pótoszlop) + utolsó.ToString() + ":" + MyF.Oszlopnév(pótoszlop + 4) + (k - 1).ToString());
                            MyX.Háttérszín(munkalap, MyF.Oszlopnév(pótoszlop) + utolsó.ToString() + ":" + MyF.Oszlopnév(pótoszlop) + (k - 1).ToString(), Color.LightSkyBlue);
                            MyX.Háttérszín(munkalap, MyF.Oszlopnév(pótoszlop + 2) + utolsó.ToString() + ":" + MyF.Oszlopnév(pótoszlop + 2) + (k - 1).ToString(), Color.LightSkyBlue);
                            MyX.Háttérszín(munkalap, MyF.Oszlopnév(pótoszlop + 4) + utolsó.ToString() + ":" + MyF.Oszlopnév(pótoszlop + 4) + (k - 1).ToString(), Color.LightSkyBlue);

                            pótoszlop += 5;
                            alholtart.Lép();
                        }
                        utolsó = k;
                    }

                }
                // UTOLSÓ SOROK
                MyX.Vastagkeret(munkalap, "A" + utolsó.ToString() + ":A" + (sor - 1).ToString());
                MyX.Vastagkeret(munkalap, "c" + utolsó.ToString() + ":c" + (sor - 1).ToString());
                pótoszlop = 4;
                for (j = 0; j <= 31; j++)
                {
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(pótoszlop) + utolsó.ToString() + ":" + MyF.Oszlopnév(pótoszlop + 4) + (sor - 1).ToString());
                    // összesítő sor
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(pótoszlop) + sor.ToString() + ":" + MyF.Oszlopnév(pótoszlop + 4) + sor.ToString());
                    MyX.Háttérszín(munkalap, MyF.Oszlopnév(pótoszlop) + sor.ToString() + ":" + MyF.Oszlopnév(pótoszlop) + sor.ToString(), Color.LightSkyBlue);
                    MyX.Háttérszín(munkalap, MyF.Oszlopnév(pótoszlop + 2) + sor.ToString() + ":" + MyF.Oszlopnév(pótoszlop + 2) + sor.ToString(), Color.LightSkyBlue);
                    MyX.Háttérszín(munkalap, MyF.Oszlopnév(pótoszlop + 4) + sor.ToString() + ":" + MyF.Oszlopnév(pótoszlop + 4) + sor.ToString(), Color.LightSkyBlue);


                    pótoszlop += 5;
                    alholtart.Lép();
                }
                // típus formázás
                MyX.Aktív_Cella(munkalap, "A1");
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

        private void Ablak_Menetkimaradás_Kiegészítő_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 27)
            {
                this.Close();
            }
        }
    }
}
