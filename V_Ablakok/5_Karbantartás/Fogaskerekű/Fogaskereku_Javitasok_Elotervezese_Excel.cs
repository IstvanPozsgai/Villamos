using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;
namespace Villamos.V_Ablakok._5_Karbantartás.Fogaskereku
{
    public class Fogaskereku_Javitasok_Elotervezese_Excel
    {
        long utolsósor;

        readonly Kezelő_T5C5_Előterv KézElőterv = new Kezelő_T5C5_Előterv();

        readonly Beállítás_Betű BeBetű = new Beállítás_Betű();

        public void ExceltGeneral(string fájlexc)
        {
            string[] cím = new string[5];
            string[] Leírás = new string[5];

            // paraméter tábla feltöltése
            cím[1] = "Adatok";
            Leírás[1] = "Előtervezett adatok";
            cím[2] = "Vizsgálatok";
            Leírás[2] = "Vizsgálati adatok havonta";
            cím[3] = "Éves_terv";
            Leírás[3] = "Vizsgálati adatok éves";
            cím[4] = "Éves_havi_terv";
            Leírás[4] = "Vizsgálati adatok éves/havi";

            // megnyitjuk
            MyX.ExcelLétrehozás();
            string munkalap = "Tartalom";

            // ****************************************************
            // elkészítjük a lapokat
            // ****************************************************
            MyX.Munkalap_átnevezés("Munka1", munkalap);

            for (int i = 1; i < 5; i++)
                MyX.Munkalap_Új(cím[i]);

            // ****************************************************
            // Elkészítjük a tartalom jegyzéket
            // ****************************************************
            MyX.Munkalap_aktív(munkalap);
            MyX.Kiir("Munkalapfül", "a1");
            MyX.Kiir("Leírás", "b1");

            for (int i = 1; i < 5; i++)
            {
                MyX.Kiir(cím[i], "A" + (i + 1).ToString());
                MyX.Link_beillesztés(munkalap, "B" + (i + 1).ToString(), cím[i].Trim());
                MyX.Kiir(Leírás[i], "B" + (i + 1).ToString());
            }
            MyX.Oszlopszélesség(munkalap, "A:B");

            // ****************************************************
            // Elkészítjük a munkalapokat
            // ****************************************************
            //FőHoltart.Maximum = 4;
            //FőHoltart.Value = 1;
            Adatoklistázása();
            //FőHoltart.Value = 2;
            Kimutatás();
            //FőHoltart.Value = 3;
            Kimutatás1();
            //FőHoltart.Value = 4;
            Kimutatás2();

            MyX.Munkalap_aktív(munkalap);
            MyX.Aktív_Cella(munkalap, "A1");

            MyX.ExcelMentés(fájlexc);
            MyX.ExcelBezárás();
        }
        private void Adatoklistázása()
        {
            try
            {
                string munkalap = "Adatok";
                MyX.Munkalap_aktív(munkalap);

                // megnyitjuk az adatbázist
                string hely = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Kmadatok.mdb";
                DataTable dataTable = MyF.ToDataTable(KézElőterv.Lista_Adatok(hely));
                utolsósor = dataTable.Rows.Count;

                munkalap = "Adatok";
                MyX.Munkalap_betű(munkalap, BeBetű);
                MyX.Munkalap_Adattábla(munkalap, dataTable);

                MyX.SorBeszúrás(munkalap, 1, 2);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");
                MyX.Munkalap_aktív(munkalap);

                // fejlécet kiírjuk
                MyX.Kiir("ID", "a3");
                MyX.Kiir("Pályaszám", "b3");
                MyX.Kiir("Jjavszám", "c3");
                MyX.Kiir("KMUkm", "d3");
                MyX.Kiir("KMUdátum", "e3");
                MyX.Kiir("vizsgfok", "f3");
                MyX.Kiir("vizsgdátumkezdő", "g3");
                MyX.Kiir("vizsgdátumvég", "h3");
                MyX.Kiir("vizsgkmszámláló", "i3");
                MyX.Kiir("havikm", "j3");
                MyX.Kiir("vizsgsorszám", "k3");
                MyX.Kiir("Jdátum", "l3");
                MyX.Kiir("Teljeskm", "m3");
                MyX.Kiir("Ciklusrend", "n3");
                MyX.Kiir("V2végezte", "o3");
                MyX.Kiir("Köv V2 sorszám", "p3");
                MyX.Kiir("Köv V2", "q3");
                MyX.Kiir("Köv V sorszám", "r3");
                MyX.Kiir("köv V", "s3");
                MyX.Kiir("Törölt", "t3");
                MyX.Kiir("Módosító", "u3");
                MyX.Kiir("Módosítás dátuma", "v3");
                MyX.Kiir("Honostelephely", "w3");
                MyX.Kiir("tervsorszám", "x3");
                MyX.Kiir("Kerék_11", "y3");
                MyX.Kiir("Kerék_12", "z3");
                MyX.Kiir("Kerék_21", "aa3");
                MyX.Kiir("Kerék_22", "ab3");
                MyX.Kiir("Kerék_min", "ac3");
                MyX.Kiir("V2V3 számláló", "ad3");
                MyX.Kiir("Év", "ae3");
                MyX.Kiir("fokozat", "af3");
                MyX.Kiir("Hónap", "ag3");


                MyX.Kiir("#KÉPLET#=YEAR(RC[-23])", "AE4");
                MyX.Kiir("#KÉPLET#=LEFT(RC[-26],2)", "AF4");
                MyX.Kiir("#KÉPLET#=MONTH(RC[-25])", "AG4");

                MyX.Képlet_másol(munkalap, "AE4:AG4", "AE5:AG" + (utolsósor + 3));

                // megformázzuk
                MyX.Oszlopszélesség(munkalap, "A:AG");

                // Azonosítók számmá alakulása
                for (global::System.Int32 i = 4; i < utolsósor + 4; i++)
                {
                    string atalakitando_szam = MyX.Beolvas(munkalap, $"B{i}");
                    MyX.Kiir($"#SZÁME#{atalakitando_szam}", $"B{i}");
                }

                // Töröl oszlopszélessége, nem működik automatikusan
                MyX.Oszlopszélesség(munkalap, "T:T", 7.86);


                MyX.Vastagkeret(munkalap, "a3:AG3");
                MyX.Vastagkeret(munkalap, "a3:AG" + (utolsósor + 3));
                MyX.Vastagkeret(munkalap, "a3:AG3");
                MyX.Rácsoz(munkalap, "a3:AG" + (utolsósor + 3));
                // szűrő
                MyX.Szűrés(munkalap, "A", "AG", (int)(utolsósor + 3), 3);

                // ablaktábla rögzítése

                MyX.Tábla_Rögzítés(munkalap, 3);


                // kiírjuk a tábla méretét
                MyX.Munkalap_aktív("Vizsgálatok");
                MyX.Kiir((utolsósor + 2).ToString(), "aa1");
                MyX.Munkalap_aktív("Éves_terv");
                MyX.Kiir((utolsósor + 2).ToString(), "aa1");
                MyX.Munkalap_aktív("Éves_havi_terv");
                MyX.Kiir((utolsósor + 2).ToString(), "aa1");


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

        private void Kimutatás()
        {
            try
            {
                string munkalap = "Vizsgálatok";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "AG" + utolsósor;
                string kimutatás_Munkalap = munkalap;
                string Kimutatás_cella = "A6";
                string Kimutatás_név = "Kimutatás";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("Pályaszám");

                Összesít_módja.Add("xlCount");

                sorNév.Add("vizsgdátumkezdő");


                SzűrőNév.Add("Honostelephely");
                SzűrőNév.Add("tervsorszám");

                oszlopNév.Add("vizsgfok");

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
                MyX.Aktív_Cella(munkalap, "A1");
                MyX.Oszlopszélesség(munkalap, "A:A", 16.14);
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

        private void Kimutatás1()
        {
            try
            {
                string munkalap = "Éves_terv";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "AG" + utolsósor;
                string kimutatás_Munkalap = munkalap;
                string Kimutatás_cella = "A6";
                string Kimutatás_név = "Kimutatás1";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("Pályaszám");

                Összesít_módja.Add("xlCount");

                sorNév.Add("Év");


                SzűrőNév.Add("Honostelephely");
                SzűrőNév.Add("tervsorszám");

                oszlopNév.Add("Fokozat");

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

        private void Kimutatás2()
        {
            try
            {
                string munkalap = "Éves_havi_terv";
                MyX.Munkalap_aktív(munkalap);
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "AG" + utolsósor;
                string kimutatás_Munkalap = munkalap;
                string Kimutatás_cella = "A6";
                string Kimutatás_név = "Kimutatás2";

                List<string> összesítNév = new List<string>();
                List<string> Összesít_módja = new List<string>();
                List<string> sorNév = new List<string>();
                List<string> oszlopNév = new List<string>();
                List<string> SzűrőNév = new List<string>();

                összesítNév.Add("ID");

                Összesít_módja.Add("xlCount");

                sorNév.Add("Pályaszám");

                oszlopNév.Add("Hónap");

                SzűrőNév.Add("Honostelephely");
                SzűrőNév.Add("Év");
                SzűrőNév.Add("Fokozat");

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
    }
}
