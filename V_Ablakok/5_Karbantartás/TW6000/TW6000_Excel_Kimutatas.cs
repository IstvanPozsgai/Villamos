using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.V_Ablakok._5_Karbantartás.TW6000
{
    public class TW6000_Excel_Kimutatas
    {
        List<Adat_TW6000_Ütemezés> AdatokÜtem = new List<Adat_TW6000_Ütemezés>();
        public void Kimutatast_Keszit(string fájlexc, string[] VizsgálatLista, Kezelő_TW6000_Előterv KézElőterv)
        {
            string[] cím = new string[4];
            string[] leírás = new string[4];

            // paraméter tábla feltöltése
            cím[1] = "Adatok";
            leírás[1] = "Előtervezett adatok";
            cím[2] = "Vizsgálatok";
            leírás[2] = "Vizsgálati adatok havonta";
            cím[3] = "Éves_terv";
            leírás[3] = "Vizsgálati adatok éves";

            // ****************************************************
            // elkészítjük a lapokat
            // ****************************************************
            string munkalap = "Tartalom";
            MyX.ExcelLétrehozás(munkalap);

            for (int i = 1; i < 4; i++)
                MyX.Munkalap_Új(cím[i]);

            // ****************************************************
            // Elkészítjük a tartalom jegyzéket
            // ****************************************************
            MyX.Aktív_Cella(munkalap, "A1");
            MyX.Kiir("Munkalapfül", "a1");
            MyX.Kiir("Leírás", "b1");

            for (int i = 1; i <= 3; i++)
            {

                MyX.Link_beillesztés(munkalap, "A" + (i + 1).ToString(), cím[i].Trim());
                MyX.Kiir(leírás[i], "b" + (i + 1).ToString());
            }
            MyX.Oszlopszélesség(munkalap, "A:B");


            //// ****************************************************
            //// Elkészítjük a munkalapokat
            //// ****************************************************

            long sor = Adatoklistázása(VizsgálatLista, KézElőterv);
            if (sor > 2)        //Azért kell mert nem tud csak 2 soros táblából kimutatást készíteni
            {
                Kimutatás();
                Kimutatás1();
            }

            MyX.Munkalap_aktív("Tartalom");
            MyX.Aktív_Cella(munkalap, "A1");
            MyX.ExcelMentés(fájlexc);
            MyX.ExcelBezárás();

            MyF.Megnyitás(fájlexc);
        }

        private long Adatoklistázása(string[] VizsgálatLista, Kezelő_TW6000_Előterv KézElőterv)
        {
            long válasz = 0;
            try
            {
                string munkalap = "Adatok";
                MyX.Munkalap_aktív(munkalap);
                MyX.Aktív_Cella(munkalap, "A1");
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");

                // fejlécet kiírjuk
                MyX.Kiir("Pályaszám", "a3");
                MyX.Kiir("ciklusrend", "b3");
                MyX.Kiir("elkészült", "c3");
                MyX.Kiir("Megjegyzés", "d3");
                MyX.Kiir("státus", "e3");
                MyX.Kiir("elkészülés", "f3");
                MyX.Kiir("esedékesség", "g3");
                MyX.Kiir("vizsgálat", "h3");
                MyX.Kiir("v. sorszám", "i3");
                MyX.Kiir("ütemezés", "j3");
                MyX.Kiir("végezte", "k3");
                MyX.Kiir("Év", "l3");
                MyX.Kiir("Hónap", "m3");

                string hely = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\TW6000adatok.mdb";
                if (VizsgálatLista == null) return válasz;

                AdatokÜtem = KézElőterv.Lista_AdatokÜtem(hely);

                List<Adat_TW6000_Ütemezés> AdatokGy = new List<Adat_TW6000_Ütemezés>();
                for (int i = 0; i < VizsgálatLista.Length; i++)
                {
                    List<Adat_TW6000_Ütemezés> Ideig = (from a in AdatokÜtem
                                                        where a.Vizsgfoka == VizsgálatLista[i].ToStrTrim()
                                                        select a).ToList();
                    AdatokGy.AddRange(Ideig);
                }

                AdatokGy = (from a in AdatokGy
                            orderby a.Azonosító, a.Vütemezés
                            select a).ToList();
                int sor = 4;
                if (AdatokGy.Count > 0) válasz = AdatokGy.Count;
                foreach (Adat_TW6000_Ütemezés rekord in AdatokGy)
                {
                    MyX.Kiir($"#SZÁME#{rekord.Azonosító}", "a" + sor);
                    MyX.Kiir(rekord.Ciklusrend.Trim(), "b" + sor);
                    MyX.Kiir(rekord.Elkészült.ToString(), "c" + sor);
                    MyX.Kiir(rekord.Megjegyzés.Trim(), "d" + sor);
                    MyX.Kiir($"#SZÁME#{rekord.Státus}", "e" + sor);
                    MyX.Kiir(rekord.Velkészülés.ToString("yyyy.MM.dd"), "f" + sor);
                    MyX.Kiir(rekord.Vesedékesség.ToString("yyyy.MM.dd"), "g" + sor);
                    MyX.Kiir(rekord.Vizsgfoka.Trim(), "h" + sor);
                    MyX.Kiir($"#SZÁME#{rekord.Vsorszám}", "i" + sor);
                    MyX.Kiir(rekord.Vütemezés.ToString("yyyy.MM.dd"), "j" + sor);
                    MyX.Kiir(rekord.Vvégezte.Trim(), "k" + sor);
                    MyX.Kiir($"#SZÁME#{rekord.Vütemezés.Year}", "l" + sor);
                    MyX.Kiir($"#SZÁME#{rekord.Vütemezés.Month}", "m" + sor);
                    sor++;
                    //Holtart.Lép();
                }

                // megformázzuk
                MyX.Oszlopszélesség(munkalap, "A:m");
                MyX.Rácsoz(munkalap, $"a3:m{(sor - 1)}");
                MyX.Vastagkeret(munkalap, "a3:m3");

                // szűrő
                MyX.Szűrés(munkalap, $"A", "M", sor, 3);

                // ablaktábla rögzítése
                MyX.Tábla_Rögzítés(munkalap, 3);

                // kiírjuk a tábla méretét
                MyX.Munkalap_aktív("Vizsgálatok");
                MyX.Kiir((sor - 1).ToString(), "aa1");
                MyX.Munkalap_aktív("Éves_terv");
                MyX.Kiir((sor - 1).ToString(), "aa1");
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

        private void Kimutatás()
        {
            try
            {
                string munkalap = "Vizsgálatok";

                MyX.Aktív_Cella(munkalap, "A1");
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");
                // beolvassuk a sor végét
                int sor = int.Parse(MyX.Beolvas(munkalap, "aa1"));


                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "M" + sor;
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

                sorNév.Add("Hónap");


                SzűrőNév.Add("végezte");
                SzűrőNév.Add("év");

                oszlopNév.Add("vizsgálat");

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

        private void Kimutatás1()
        {
            try
            {
                string munkalap = "Éves_terv";

                MyX.Aktív_Cella(munkalap, "A1");
                MyX.Link_beillesztés(munkalap, "A1", "Tartalom");
                // beolvassuk a sor végét
                int sor = int.Parse(MyX.Beolvas(munkalap, "aa1"));


                string munkalap_adat = "Adatok";
                string balfelső = "A3";
                string jobbalsó = "M" + sor;
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

                sorNév.Add("év");

                SzűrőNév.Add("végezte");

                oszlopNév.Add("vizsgálat");

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