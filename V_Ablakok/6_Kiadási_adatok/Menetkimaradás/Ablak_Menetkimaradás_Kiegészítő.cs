using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok
{
    public partial class Ablak_Menetkimaradás_Kiegészítő : Form
    {
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
            Telephelyek_Feltöltése_lista();
        }

        private void Ablak_Menetkimaradás_Kiegészítő_Load(object sender, EventArgs e)
        {

        }

        void Telephelyek_Feltöltése_lista()
        {
            string hely = Application.StartupPath + @"\Főmérnökség\Adatok\kiegészítő.mdb";
            string jelszó = "Mocó";
            string szöveg = "SELECT * FROM Szolgálattelepeitábla ORDER BY telephelynév";

            Kezelő_Kiegészítő_Szolgálattelepei kéz = new Kezelő_Kiegészítő_Szolgálattelepei();
            Lstüzemek = kéz.Lista_Adatok(hely, jelszó, szöveg);
        }


        private void Adat_Törlés_Click(object sender, EventArgs e)
        {
            try
            {
                // törli az időszak főmérnökségi adatait

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\" + dátumtól.Value.ToString("yyyy") + @"\" + dátumtól.Value.ToString("yyyy") + "_menet_adatok.mdb";
                string jelszó = "lilaakác";
                string szöveg = "DELETE * FROM menettábla WHERE bekövetkezés>=#" + dátumtól.Value.ToString("MM-dd-yyyy") + " 00:00:0#";
                szöveg = szöveg + " and bekövetkezés<=#" + dátumig.Value.ToString("MM-dd-yyyy") + " 23:59:59#";
                MyA.ABtörlés(hely, jelszó, szöveg);
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

        private void SAP_Click(object sender, EventArgs e)
        {
            try
            {

                // ellenőrizzük, hogy létezik-e  a főmérnökségi tábla a választott évben
                // leellenőrizzük a főmérnökségi tábla létezik-e ha nem akkor másoljuk
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{dátumtól.Value.Year}\{dátumtól.Value.Year}_menet_adatok.mdb";
                string jelszó = "lilaakác";
                string szöveg = "SELECT * FROM menettábla";

                Kezelő_Menetkimaradás KézMenet = new Kezelő_Menetkimaradás();
                List<Adat_Menetkimaradás> AdatokMenet = KézMenet.Lista_Adatok(hely, jelszó, szöveg);

                // ha nem létezik akkor létrehozzuk
                if (!Exists(hely)) Adatbázis_Létrehozás.Menekimaradás_Főmérnökség(hely);

                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "SAP-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;


                string helykieg = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
                string jelszókieg = "Mocó";
                string szövegkieg = "SELECT * FROM szolgálattelepeitábla";
                Kezelő_Kiegészítő_Szolgálattelepei KézSzolg = new Kezelő_Kiegészítő_Szolgálattelepei();
                List<Adat_Kiegészítő_Szolgálattelepei> AdatokSzolg = KézSzolg.Lista_Adatok(helykieg, jelszókieg, szövegkieg);

                string helytip = Application.StartupPath + @"\Főmérnökség\adatok\villamos.mdb";
                string jelszótip = "pozsgaii";
                string szövegtip = "SELECT * FROM állománytábla ";
                Kezelő_Jármű KézJármű = new Kezelő_Jármű();
                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok(helytip, jelszótip, szövegtip);


                // megnyitjuk a beolvasandó táblát
                MyE.ExcelMegnyitás(fájlexc);
                string munkalap = "Munka1";
                // megnézzük, hogy hány sorból áll a tábla
                int utolsó = MyE.Utolsósor(munkalap);

                főholtart.Be(utolsó + 1);
                if (utolsó > 1)
                {
                    // megnyitjuk a táblát
                    bool törölt = false;

                    List<string> SzövegGy = new List<string>();
                    for (int i = 2; i <= utolsó; i++)
                    {
                        // beolvassuk az adatokat
                        DateTime didő = MyE.Beolvasidő("c" + i.ToString());
                        DateTime ddátum = MyE.BeolvasDátum("b" + i.ToString());
                        DateTime bekövetkezés = new DateTime(ddátum.Year, ddátum.Month, ddátum.Day, didő.Hour, didő.Minute, didő.Second);
                        string jelentés = MyE.Beolvas("a" + i.ToString());
                        string viszonylat = MyF.Szöveg_Tisztítás(MyE.Beolvas("j" + i.ToString()), 0, 6);
                        string azonosító = MyF.Szöveg_Tisztítás(MyE.Beolvas("d" + i.ToString()), 1, 4);
                        string Eseményjele = MyF.Szöveg_Tisztítás(MyE.Beolvas("g" + i.ToString()), 0, 1);
                        int kimaradtmenet = int.Parse(MyE.Beolvas("h" + i.ToString()));
                        int tétel = int.Parse(MyE.Beolvas("m" + i.ToString()));
                        string jvbeírás = MyF.Szöveg_Tisztítás(MyE.Beolvas("e" + i.ToString()), 0, 150);
                        string vmbeírás = "*";
                        string javítás = MyF.Szöveg_Tisztítás(MyE.Beolvas("F" + i.ToString()), 0, 150);


                        string típus = (from a in AdatokJármű
                                        where a.Azonosító.Trim() == azonosító.Trim()
                                        select a.Valóstípus).FirstOrDefault() ?? "?";

                        string felelősmunkahely = MyE.Beolvas("l" + i.ToString()).Trim();

                        string telephely = "_";
                        string szolgálat = "_";
                        Adat_Kiegészítő_Szolgálattelepei Lekérdezés = (from a in AdatokSzolg
                                                                       where a.Felelősmunkahely.Trim() == felelősmunkahely.Trim()
                                                                       select a).FirstOrDefault();
                        if (Lekérdezés != null)
                        {
                            telephely = Lekérdezés.Telephelynév;
                            szolgálat = Lekérdezés.Szolgálatnév;
                        }

                        List<Adat_Menetkimaradás> Ellenorzes = (from a in AdatokMenet
                                                                where a.Tétel == tétel && a.Jelentés == jelentés
                                                                select a).ToList();

                        if ( Ellenorzes.Any ())
                        {
                            // ha van ilyen akkor módosítjuk
                            szöveg = "UPDATE menettábla SET viszonylat= '" + viszonylat.Trim() + "'";
                            szöveg += ", azonosító= '" + azonosító.Trim() + "'";
                            szöveg += ", típus= '" + típus.Trim() + "'";
                            szöveg += ", Eseményjele= '" + Eseményjele.Trim() + "'";
                            szöveg += ", Bekövetkezés= '" + bekövetkezés.ToString() + "'";
                            szöveg += ", kimaradtmenet= " + kimaradtmenet;
                            szöveg += ", jvbeírás= '" + jvbeírás.Trim() + "'";
                            szöveg += ", vmbeírás= '" + vmbeírás.Trim() + "'";
                            szöveg += ", javítás= '" + javítás.Trim() + "'";
                            szöveg += ", törölt= " + törölt;
                            szöveg += ", telephely= '" + telephely.Trim() + "'";
                            szöveg += ", szolgálat= '" + szolgálat.Trim() + "'";
                            szöveg += " WHERE tétel=" + tétel + " and jelentés='" + jelentés.Trim() + "'";
                        }
                        else
                        {
                            // ha nincs a főmérnökségi táblába akkor rögzítjük
                            szöveg = "INSERT INTO menettábla ";
                            // rekord nevek
                            szöveg += "(viszonylat, azonosító, típus, Eseményjele, Bekövetkezés, kimaradtmenet, jvbeírás, javítás, jelentés, tétel, ";
                            szöveg += " vmbeírás, id, telephely, szolgálat, törölt )";
                            szöveg += " VALUES  ( ";
                            // értékek
                            szöveg += $"'{viszonylat}', ";
                            szöveg += $"'{azonosító}', ";
                            szöveg += $"'{típus}', ";
                            szöveg += $"'{Eseményjele}', ";
                            szöveg += $"'{bekövetkezés}', ";
                            szöveg += $"{kimaradtmenet}, ";
                            szöveg += $"'{jvbeírás}', ";
                            szöveg += $"'{javítás}', ";
                            szöveg += $"'{jelentés}', ";
                            szöveg += $"{tétel}, ";
                            szöveg += $"'{vmbeírás}' , ";
                            szöveg += $"{0}, ";
                            szöveg += $"'{telephely}', ";
                            szöveg += $"'{szolgálat}', ";
                            szöveg += $"{törölt} )";
                        }
                        SzövegGy.Add(szöveg);
                        főholtart.Lép();
                    }
                    MyA.ABMódosítás(hely, jelszó, SzövegGy);
                }
                MyE.ExcelBezárás();

                főholtart.Ki();
                // kitöröljük a betöltött fájlt
                Delete(fájlexc);
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

        private void Telephely_gomb_Click(object sender, EventArgs e)
        {
            // telephelyek adatait összemásoljuk
            // leellenőrizzük, hogy létezik-e a fájl
            string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{dátumtól.Value.Year}\{dátumtól.Value.Year}_menet_adatok.mdb";

            if (!Exists(hely)) Adatbázis_Létrehozás.Menekimaradás_Főmérnökség(hely);

            string jelszó = "lilaakác";
            string helytelep;

            // a telepek adataival frisíti a főmérnökségi adatbázis adatait.
            főholtart.Be(Lstüzemek.Count + 1);
            alholtart.Be(100);
            List<string> szövegGy = new List<string>();
            foreach (Adat_Kiegészítő_Szolgálattelepei Elem in Lstüzemek)
            {
                főholtart.Lép();
                // telephelyenként végigmegyünk az adatokon
                helytelep = $@"{Application.StartupPath}\{Elem.Telephelynév.Trim()}\Adatok\főkönyv\menet{dátumtól.Value.Year}.mdb";
                if (Exists(helytelep))
                {

                    string szöveg = $"SELECT * FROM menettábla WHERE Bekövetkezés>=#{dátumtól.Value:yyyy-MM-dd}# AND Bekövetkezés<=#{dátumig.Value:yyyy-MM-dd}# ORDER BY id";
                    Kezelő_Menetkimaradás kéz = new Kezelő_Menetkimaradás();
                    List<Adat_Menetkimaradás> Adatok = kéz.Lista_Adatok(helytelep, jelszó, szöveg);
                    // Végig nézzük az adatokat

                    foreach (Adat_Menetkimaradás rekord in Adatok)
                    {

                        string jvbeírás = (from a in Adatok
                                           where a.Tétel == rekord.Tétel && a.Jelentés == rekord.Jelentés
                                           select a.Jvbeírás).FirstOrDefault();
                        if (jvbeírás!=null && jvbeírás == "_")
                        {
                            // ha nincs a főmérnökségi táblába akkor rögzítjük
                            szöveg = "INSERT INTO menettábla ";
                            // rekord nevek
                            szöveg += "(viszonylat, azonosító, típus, Eseményjele, Bekövetkezés, kimaradtmenet, jvbeírás, javítás, jelentés, tétel, ";
                            szöveg += " vmbeírás, id, telephely, szolgálat, törölt )";
                            szöveg += " VALUES  ( ";
                            // értékek
                            szöveg += "'" + rekord.Viszonylat + "', ";
                            szöveg += "'" + rekord.Azonosító + "', ";
                            szöveg += "'" + rekord.Típus + "', ";
                            szöveg += "'" + rekord.Eseményjele + "', ";
                            szöveg += "'" + rekord.Bekövetkezés.ToString() + "', ";
                            szöveg += rekord.Kimaradtmenet + ", ";
                            szöveg += "'" + rekord.Jvbeírás.Replace('"', '°').Replace('\'', '°') + "', ";
                            szöveg += "'" + rekord.Javítás.Replace('"', '°').Replace('\'', '°') + "', ";
                            szöveg += "'" + rekord.Jelentés + "', ";
                            szöveg += rekord.Tétel + ", ";
                            szöveg += "'" + rekord.Vmbeírás + "' , ";
                            szöveg += "0, ";
                            szöveg += "'" + Elem.Telephelynév.Trim() + "', ";
                            szöveg += "'" + Elem.Szolgálatnév.Trim() + "', ";
                            szöveg += rekord.Törölt;
                            szöveg += " )";
                            szövegGy.Add(szöveg);
                        }
                        alholtart.Lép();
                    }
                }
            }
            MyA.ABMódosítás(hely, jelszó, szövegGy);
            főholtart.Ki();
            alholtart.Ki();
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
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
                SaveFileDialog1.InitialDirectory = "MyDocuments";
                SaveFileDialog1.Title = "Listázott tartalom mentése Excel fájlba";
                SaveFileDialog1.FileName = "Menetkimaradás_" + Program.PostásNév.Trim() + "_" + dátumtól.Value.ToString("yyyy-MMMM") + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                SaveFileDialog1.Filter = "Excel |*.xlsx";
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

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{dátumtól.Value.Year}\{dátumtól.Value.Year}_menet_adatok.mdb";
                string jelszó = "lilaakác";
                string szöveg = "SELECT * FROM menettábla where [bekövetkezés]>=#" + dátumtól.Value.ToString("M-d-yy") + " 00:00:0#";
                szöveg += " and [bekövetkezés]<#" + dátumig.Value.ToString("M-d-yy") + " 23:59:0#";
                szöveg += " order by bekövetkezés";
                Kezelő_MenetKimaradás_Főmérnökség kéz = new Kezelő_MenetKimaradás_Főmérnökség();
                List<Adat_Menetkimaradás_Főmérnökség> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

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
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\" + dátumtól.Value.ToString("yyyy") + @"\" + dátumtól.Value.ToString("yyyy") + "_menet_adatok.mdb";
                string jelszó = "lilaakác";
                string szöveg = "SELECT * FROM menettábla where [bekövetkezés]>=#" + dátumtól.Value.ToString("M-d-yy") + " 00:00:0#";
                szöveg += " and [bekövetkezés]<#" + dátumig.Value.ToString("M-d-yy") + " 23:59:0#";
                szöveg += " order by telephely, típus, viszonylat,bekövetkezés";
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
                Kezelő_MenetKimaradás_Főmérnökség kéz = new Kezelő_MenetKimaradás_Főmérnökség();
                List<Adat_Menetkimaradás_Főmérnökség> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);


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
