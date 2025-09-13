using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Menetkimaradás_Kiegészítő : Form
    {
        readonly Kezelő_MenetKimaradás_Főmérnökség KézFőmérnök = new Kezelő_MenetKimaradás_Főmérnökség();
        readonly Kezelő_Kiegészítő_Szolgálattelepei KézSzolgTelep = new Kezelő_Kiegészítő_Szolgálattelepei();
        readonly Kezelő_Menetkimaradás KézMenet = new Kezelő_Menetkimaradás();

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
                MyE.ExcelLétrehozás();

                // formázáshoz
                MyE.Munkalap_átnevezés("Munka1", "Tartalom");
                // ****************************************************
                // elkészítjük a lapokat
                // ****************************************************
                for (int i = 1; i <= 5; i++)
                {
                    MyE.Új_munkalap(cím[i].Trim());
                }

                // ****************************************************
                // Elkészítjük a tartalom jegyzéket
                // ****************************************************
                string munkalap = "Tartalom";
                MyE.Munkalap_aktív("Tartalom");

                MyE.Kiir("Munkalapfül", "a1");
                MyE.Kiir("Leírás", "b1");
                for (int i = 1; i <= 5; i++)
                {
                    MyE.Link_beillesztés(munkalap, "A" + (i + 1).ToString(), cím[i].Trim());
                    MyE.Kiir(leírás[i].Trim(), "b" + (i + 1).ToString());
                }
                MyE.Oszlopszélesség(munkalap, "A:B");
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
                MyE.Munkalap_aktív("Tartalom");
                MyE.Aktív_Cella("Tartalom", "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                főholtart.Ki();
                alholtart.Ki();
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Adatokkiírásaexcelbe()
        {
            try
            {
                alsópanels6 = "";
                string munkalap = "Adatok_1";
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

                // fejléc elkészítése
                MyE.Kiir("viszonylat", "A5");
                MyE.Kiir("azonosító", "B5");
                MyE.Kiir("típus", "C5");
                MyE.Kiir("Eseményjele", "d5");
                MyE.Kiir("Bekövetkezés", "e5");
                MyE.Kiir("kimaradtmenet", "F5");
                MyE.Kiir("jvbeírás", "g5");
                MyE.Kiir("vmbeírás", "h5");
                MyE.Kiir("javítás", "i5");
                MyE.Kiir("törölt", "j5");
                MyE.Kiir("jelentés", "k5");
                MyE.Kiir("tétel", "l5");
                MyE.Kiir("telephely", "m5");
                MyE.Kiir("szolgálat", "n5");

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
                    MyE.Kiir(rekord.Viszonylat, "A" + i.ToString());
                    MyE.Kiir(rekord.Azonosító, "B" + i.ToString());
                    MyE.Kiir(rekord.Típus, "C" + i.ToString());
                    MyE.Kiir(rekord.Eseményjele, "d" + i.ToString());
                    MyE.Kiir(rekord.Bekövetkezés.ToString(), "e" + i.ToString());
                    MyE.Kiir(rekord.Kimaradtmenet.ToString(), "F" + i.ToString());
                    MyE.Kiir(rekord.Jvbeírás, "g" + i.ToString());
                    MyE.Kiir(rekord.Vmbeírás, "h" + i.ToString());
                    MyE.Kiir(rekord.Javítás, "i" + i.ToString());
                    MyE.Kiir(rekord.Kimaradtmenet.ToString(), "j" + i.ToString());
                    MyE.Kiir(rekord.Jelentés, "k" + i.ToString());
                    MyE.Kiir(rekord.Tétel.ToString(), "l" + i.ToString());
                    MyE.Kiir(rekord.Telephely, "m" + i.ToString());
                    MyE.Kiir(rekord.Szolgálat, "n" + i.ToString());

                    alholtart.Lép();
                    i++;
                }

                alsópanels6 = (i - 1).ToString();
                MyE.Oszlopszélesség(munkalap, "A:N");
                MyE.Rácsoz("A5:N" + alsópanels6);
                MyE.Aktív_Cella(munkalap, "A1");
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
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

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

                MyE.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);
                MyE.Aktív_Cella(munkalap, "A1");
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
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

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

                MyE.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);
                MyE.Aktív_Cella(munkalap, "A1");
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
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

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

                MyE.Kimutatás_Fő(munkalap_adat, balfelső, jobbalsó, kimutatás_Munkalap, Kimutatás_cella, Kimutatás_név
                                , összesítNév, Összesít_módja, sorNév, oszlopNév, SzűrőNév);
                MyE.Aktív_Cella(munkalap, "A1");
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
                MyE.Munkalap_aktív(munkalap);
                MyE.Link_beillesztés(munkalap, "A1", "Tartalom");

                // elkészítjük a fejlécet
                MyE.Munkalap_betű("Arial", 10);
                int sor = 6;
                int oszlop = 1;

                MyE.Kiir("Telephely", "A" + sor.ToString());
                MyE.Oszlopszélesség(munkalap, "A:A", 20);
                MyE.Kiir("Típus", "B" + sor.ToString());
                MyE.Oszlopszélesség(munkalap, "B:B", 15);
                MyE.Kiir("Viszonylat", "C" + sor.ToString());
                MyE.Oszlopszélesség(munkalap, "C:C", 15);
                MyE.Rácsoz("A6:C6");
                MyE.Egyesít(munkalap, "A5:C5");
                MyE.Vastagkeret("A5:C5");

                MyE.Kiir("Villamos Járműműszaki Főmérnökség " + dátumtól.Value.ToString(), "A5:C5");
                oszlop += 3;
                sor--;

                MyE.Kiir("A", MyE.Oszlopnév(oszlop) + (sor + 1).ToString());
                MyE.Kiir("Menet", MyE.Oszlopnév(oszlop + 1) + (sor + 1).ToString());
                MyE.Kiir("B", MyE.Oszlopnév(oszlop + 2) + (sor + 1).ToString());
                MyE.Kiir("Menet", MyE.Oszlopnév(oszlop + 3) + (sor + 1).ToString());
                MyE.Kiir("C", MyE.Oszlopnév(oszlop + 4) + (sor + 1).ToString());
                MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(oszlop) + ":" + MyE.Oszlopnév(oszlop + 4), 5);
                MyE.Rácsoz(MyE.Oszlopnév(oszlop) + (sor + 1).ToString() + ":" + MyE.Oszlopnév(oszlop + 4) + (sor + 1).ToString());

                MyE.Egyesít(munkalap, "d5:h5");
                MyE.Vastagkeret("d5:h5");
                MyE.Kiir("Összesen", "d5:h5");
                // kiirjuk a naptári napokat
                oszlop += 5;
                alholtart.Be(33);
                for (int I = 1; I <= 31; I++)

                {
                    MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(oszlop) + ":" + MyE.Oszlopnév(oszlop + 4), 5);
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop) + sor.ToString() + ":" + MyE.Oszlopnév(oszlop + 4) + sor.ToString());
                    MyE.Kiir(I.ToString(), MyE.Oszlopnév(oszlop) + sor.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + sor.ToString() + ":" + MyE.Oszlopnév(oszlop + 4) + sor.ToString());

                    MyE.Kiir("A", MyE.Oszlopnév(oszlop) + (sor + 1).ToString());
                    MyE.Kiir("Menet", MyE.Oszlopnév(oszlop + 1) + (sor + 1).ToString());
                    MyE.Kiir("B", MyE.Oszlopnév(oszlop + 2) + (sor + 1).ToString());
                    MyE.Kiir("Menet", MyE.Oszlopnév(oszlop + 3) + (sor + 1).ToString());
                    MyE.Kiir("C", MyE.Oszlopnév(oszlop + 4) + (sor + 1).ToString());

                    MyE.Rácsoz(MyE.Oszlopnév(oszlop) + (sor + 1).ToString() + ":" + MyE.Oszlopnév(oszlop + 4) + (sor + 1).ToString());

                    alholtart.Lép();
                    oszlop += 5;
                }
                oszlop -= 5;
                MyE.Háttérszín("A5:" + MyE.Oszlopnév(oszlop + 4) + "6", Color.Yellow);

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
                    MyE.Kiir(aö.ToString(), MyE.Oszlopnév(napja * 5 + 9) + sor.ToString());
                    MyE.Kiir(amö.ToString(), MyE.Oszlopnév(napja * 5 + 10) + sor.ToString());
                    MyE.Kiir(bö.ToString(), MyE.Oszlopnév(napja * 5 + 11) + sor.ToString());
                    MyE.Kiir(bmö.ToString(), MyE.Oszlopnév(napja * 5 + 12) + sor.ToString());
                    MyE.Kiir(cö.ToString(), MyE.Oszlopnév(napja * 5 + 13) + sor.ToString());
                    nap = rekord.Bekövetkezés.Day;
                    if (etelephely.Trim() != rekord.Telephely.Trim())
                    {
                        MyE.Kiir(rekord.Telephely.Trim(), "a" + sor.ToString());
                        MyE.Kiir(rekord.Típus.Trim(), "b" + sor.ToString());
                        MyE.Kiir(rekord.Viszonylat.Trim(), "c" + sor.ToString());
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
                        MyE.Kiir(képlet, "d" + sor.ToString());
                        MyE.Kiir(képlet, "e" + sor.ToString());
                        MyE.Kiir(képlet, "f" + sor.ToString());
                        MyE.Kiir(képlet, "g" + sor.ToString());
                        MyE.Kiir(képlet, "h" + sor.ToString());

                    }
                    if (etípus.Trim() != rekord.Típus.Trim())
                    {
                        MyE.Kiir(rekord.Típus.Trim(), "b" + sor.ToString());
                        MyE.Kiir(rekord.Viszonylat.Trim(), "c" + sor.ToString());
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
                        MyE.Kiir(képlet, "d" + sor.ToString());
                        MyE.Kiir(képlet, "e" + sor.ToString());
                        MyE.Kiir(képlet, "f" + sor.ToString());
                        MyE.Kiir(képlet, "g" + sor.ToString());
                        MyE.Kiir(képlet, "h" + sor.ToString());
                    }

                    if (eviszonylat.Trim() != rekord.Viszonylat.Trim())
                    {
                        MyE.Kiir(rekord.Viszonylat.Trim(), "c" + sor.ToString());
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
                        MyE.Kiir(képlet, "d" + sor.ToString());
                        MyE.Kiir(képlet, "e" + sor.ToString());
                        MyE.Kiir(képlet, "f" + sor.ToString());
                        MyE.Kiir(képlet, "g" + sor.ToString());
                        MyE.Kiir(képlet, "h" + sor.ToString());
                    }

                    alholtart.Lép();
                    i++;
                }
                sor++;
                MyE.Kiir("Összesen:", "a" + sor.ToString());
                MyE.Rácsoz("c7:c" + sor.ToString());
                MyE.Egyesít(munkalap, "a" + sor.ToString() + ":" + "c" + sor.ToString());
                MyE.Vastagkeret("a" + sor.ToString() + ":" + "c" + sor.ToString());
                // utolsó sor után összesítjük
                képlet = "=SUM(R[-" + (sor - 7).ToString() + "]C:R[-1]C)";
                for (j = 4; j <= 163; j++)
                    MyE.Kiir(képlet, MyE.Oszlopnév(j) + sor.ToString());


                // formázás folytatása
                alholtart.Be(33);
                // típus formázása
                int utolsó = 7;

                for (int kk = 8; kk <= sor; kk++)
                {
                    if (MyE.Beolvas("b" + k.ToString()) != "")
                    {
                        MyE.Vastagkeret("b" + utolsó.ToString() + ":b" + (kk - 1).ToString());
                        utolsó = kk;
                    }
                }
                // naponta formáz
                utolsó = 7;

                for (k = 8; k <= sor; k++)
                {
                    if (MyE.Beolvas("A" + k.ToString()) != "")
                    {
                        MyE.Vastagkeret("A" + utolsó.ToString() + ":c" + (k - 1).ToString());
                        MyE.Vastagkeret("A" + utolsó.ToString() + ":A" + (k - 1).ToString());
                        MyE.Vastagkeret("c" + utolsó.ToString() + ":c" + (k - 1).ToString());
                        pótoszlop = 4;
                        for (j = 0; j <= 31; j++)
                        {
                            MyE.Rácsoz(MyE.Oszlopnév(pótoszlop) + utolsó.ToString() + ":" + MyE.Oszlopnév(pótoszlop + 4) + (k - 1).ToString());
                            MyE.Háttérszín(MyE.Oszlopnév(pótoszlop) + utolsó.ToString() + ":" + MyE.Oszlopnév(pótoszlop) + (k - 1).ToString(), Color.LightSkyBlue);
                            MyE.Háttérszín(MyE.Oszlopnév(pótoszlop + 2) + utolsó.ToString() + ":" + MyE.Oszlopnév(pótoszlop + 2) + (k - 1).ToString(), Color.LightSkyBlue);
                            MyE.Háttérszín(MyE.Oszlopnév(pótoszlop + 4) + utolsó.ToString() + ":" + MyE.Oszlopnév(pótoszlop + 4) + (k - 1).ToString(), Color.LightSkyBlue);

                            pótoszlop += 5;
                            alholtart.Lép();
                        }
                        utolsó = k;
                    }

                }
                // UTOLSÓ SOROK
                MyE.Vastagkeret("A" + utolsó.ToString() + ":A" + (sor - 1).ToString());
                MyE.Vastagkeret("c" + utolsó.ToString() + ":c" + (sor - 1).ToString());
                pótoszlop = 4;
                for (j = 0; j <= 31; j++)
                {
                    MyE.Rácsoz(MyE.Oszlopnév(pótoszlop) + utolsó.ToString() + ":" + MyE.Oszlopnév(pótoszlop + 4) + (sor - 1).ToString());
                    // összesítő sor
                    MyE.Rácsoz(MyE.Oszlopnév(pótoszlop) + sor.ToString() + ":" + MyE.Oszlopnév(pótoszlop + 4) + sor.ToString());
                    MyE.Háttérszín(MyE.Oszlopnév(pótoszlop) + sor.ToString() + ":" + MyE.Oszlopnév(pótoszlop) + sor.ToString(), Color.LightSkyBlue);
                    MyE.Háttérszín(MyE.Oszlopnév(pótoszlop + 2) + sor.ToString() + ":" + MyE.Oszlopnév(pótoszlop + 2) + sor.ToString(), Color.LightSkyBlue);
                    MyE.Háttérszín(MyE.Oszlopnév(pótoszlop + 4) + sor.ToString() + ":" + MyE.Oszlopnév(pótoszlop + 4) + sor.ToString(), Color.LightSkyBlue);


                    pótoszlop += 5;
                    alholtart.Lép();
                }
                // típus formázás
                MyE.Aktív_Cella(munkalap, "A1");
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
