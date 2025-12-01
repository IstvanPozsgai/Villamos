using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.V_Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Takarítás
{
    public class Takarítás_teljesítés_Igazolás
    {
        public DateTime Dátum { get; private set; }
        public bool Jármű { get; private set; }
        public string Telephely { get; private set; }

        private string AláíróBeosztás = "";
        private string AláíróNév = "";

        readonly Kezelő_Épület_Takarítás_Osztály KézTakarításOsztály = new Kezelő_Épület_Takarítás_Osztály();
        readonly Kezelő_Épület_Adattábla KézAdatTábla = new Kezelő_Épület_Adattábla();
        readonly Kezelő_Épület_Takarításrakijelölt KézTakarításrakijelölt = new Kezelő_Épület_Takarításrakijelölt();
        readonly Kezelő_Takarítás_Opció KézOpció = new Kezelő_Takarítás_Opció();
        readonly Kezelő_Takarítás_Telep_Opció KézTelep = new Kezelő_Takarítás_Telep_Opció();
        readonly Kezelő_Takarítás_BMR KézBMR = new Kezelő_Takarítás_BMR();
        readonly Kezelő_Jármű_Takarítás_Ár KézTakÁr = new Kezelő_Jármű_Takarítás_Ár();
        readonly Kezelő_Jármű_Takarítás_J1 KézJ1 = new Kezelő_Jármű_Takarítás_J1();
        readonly Kezelő_Jármű_Takarítás_Teljesítés KézTelj = new Kezelő_Jármű_Takarítás_Teljesítés();
        readonly Kezelő_Behajtás_Engedélyezés KézBehEng = new Kezelő_Behajtás_Engedélyezés();


        List<Adat_Jármű_Takarítás_Teljesítés> AdatokTelj = new List<Adat_Jármű_Takarítás_Teljesítés>();
        List<Adat_Jármű_Takarítás_J1> AdatokJ1 = new List<Adat_Jármű_Takarítás_J1>();
        List<Adat_Jármű_Takarítás_Árak> AdatokÁr = new List<Adat_Jármű_Takarítás_Árak>();
        List<Adat_Épület_Takarítás_Osztály> AdatokOsztály = new List<Adat_Épület_Takarítás_Osztály>();
        List<Adat_Épület_Adattábla> AdatokRészletes = new List<Adat_Épület_Adattábla>();
        List<Adat_Épület_Takarításrakijelölt> AdatokKijelöltek = new List<Adat_Épület_Takarításrakijelölt>();
        List<Adat_Takarítás_Opció> AdatokTakOpció = new List<Adat_Takarítás_Opció>();
        List<Adat_Takarítás_Telep_Opció> AdatokTakTelepOpció = new List<Adat_Takarítás_Telep_Opció>();
        List<Adat_Takarítás_BMR> AdatokBMR = new List<Adat_Takarítás_BMR>();

        readonly List<string> Lekérdezés_Kategória = new List<string>() { "J2", "J3", "J4", "J5", "J6", "Graffiti", "Eseti", "Fertőtlenítés" };

#pragma warning disable IDE0044 // Add readonly modifier
        List<Adat_ÉpJár_Takarítás_TIG> AdatokTIG = new List<Adat_ÉpJár_Takarítás_TIG>();
        List<string> TIGTípusok = new List<string>();
#pragma warning restore IDE0044 // Add readonly modifie

        readonly Beállítás_Betű BeBetűG14V = new Beállítás_Betű { Név = "Garamond", Méret = 14, Vastag = true };
        readonly Beállítás_Betű BeBetűGFt = new Beállítás_Betű { Név = "Garamond", Méret = 12, Formátum = "#,###.## Ft" };
        readonly Beállítás_Betű BeBetűGS = new Beállítás_Betű { Név = "Garamond", Méret = 12, Formátum = "#,###.##" };
        readonly Beállítás_Betű BeBetűGSV = new Beállítás_Betű { Név = "Garamond", Méret = 12, Formátum = "#,###.##", Vastag = true };
        readonly Beállítás_Betű BeBetűGFt0 = new Beállítás_Betű { Név = "Garamond", Méret = 12, Formátum = "#,##0 Ft" };
        readonly Beállítás_Betű BeBetűG0 = new Beállítás_Betű { Név = "Garamond", Méret = 12, Formátum = "#,##0" };
        readonly Beállítás_Betű BeBetűGSP = new Beállítás_Betű { Név = "Garamond", Méret = 12, Formátum = "#,###" };
        readonly Beállítás_Betű BeBetűGFtV = new Beállítás_Betű { Név = "Garamond", Méret = 12, Formátum = "#,###.## Ft", Vastag = true };
        readonly Beállítás_Betű BeBetűGV = new Beállítás_Betű { Név = "Garamond", Méret = 12, Vastag = true };
        readonly Beállítás_Betű BeBetűGA = new Beállítás_Betű { Név = "Garamond", Méret = 12, Aláhúzott = true };
        readonly Beállítás_Betű BeBetűG12 = new Beállítás_Betű { Név = "Garamond", Méret = 12 };
        readonly Beállítás_Betű BeBetűG10 = new Beállítás_Betű { Név = "Garamond", Méret = 10 };
        readonly Beállítás_Betű BeBetűG10V = new Beállítás_Betű { Név = "Garamond", Méret = 10, Vastag = true };
        readonly Beállítás_Betű BeBetűG10Ft = new Beállítás_Betű { Név = "Garamond", Méret = 10, Formátum = "#,###.## Ft" };
        readonly Beállítás_Betű BeBetűG10VFt = new Beállítás_Betű { Név = "Garamond", Méret = 10, Formátum = "#,###.## Ft", Vastag = true };

        public Takarítás_teljesítés_Igazolás(DateTime dátum, bool jármű, string telephely)
        {
            Dátum = dátum;
            Jármű = jármű;
            Telephely = telephely;
        }

        #region Épület
        public void ExcelÉpületTábla(string fájlexc)
        {
            AdatokOsztály = KézTakarításOsztály.Lista_Adatok(Telephely);
            AdatokOsztály = (from a in AdatokOsztály
                             where a.Státus == false
                             orderby a.Id
                             select a).ToList();

            AdatokRészletes = KézAdatTábla.Lista_Adatok(Telephely);
            AdatokRészletes = (from a in AdatokRészletes
                               where a.Státus == false
                               orderby a.ID
                               select a).ToList();

            AdatokKijelöltek = KézTakarításrakijelölt.Lista_Adatok(Telephely, Dátum.Year);
            AdatokTIG.Clear();

            AdatokTakOpció = KézOpció.Lista_Adatok();

            AdatokTakTelepOpció.Clear();
            AdatokTakTelepOpció = KézTelep.Lista_Adatok(Telephely, Dátum.Year);

            GondnokListaFeltöltés();
            BMRListaFeltöltés();

            string munkalap = "Munka1";

            // megnyitjuk az excelt
            MyX.ExcelLétrehozás(munkalap);

            ÁrMunkalap();
            RészletesMunkalap();

            if (AdatokTIG.Count != 0) TIGElkészítés("TIG", "Épület takarítási");
            OpcióslapAdata();
            if (AdatokTIG.Count != 0) TIGElkészítés("TIG_opció", "opcionális");

            MyX.ExcelMentés(fájlexc);
            MyX.ExcelBezárás();
            MyF.Megnyitás(fájlexc);
        }

        private void GondnokListaFeltöltés()
        {
            try
            {

                List<Adat_Behajtás_Engedélyezés> AdatokBehEng = new List<Adat_Behajtás_Engedélyezés>();

                AdatokBehEng = KézBehEng.Lista_Adatok();
                Adat_Behajtás_Engedélyezés Elem = AdatokBehEng.Where(a => a.Telephely == Telephely).FirstOrDefault();
                if (Elem != null)
                {
                    AláíróBeosztás = Elem.Beosztás;
                    AláíróNév = Elem.Név;
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

        private void ÁrMunkalap()
        {
            try
            {
                string munkalap = "Összesítő";
                MyX.Munkalap_átnevezés("Munka1", munkalap);
                // *********************************************
                // ********* Osztály tábla *********************
                // *********************************************
                // fejléc elkészítése
                MyX.Kiir("Megnevezés", "a1");
                MyX.Kiir("E1 Egységár [db]", "c1");
                MyX.Kiir("E2 Egységár [Ft/m2]", "d1");
                MyX.Kiir("E3 Egységár [Ft/m2]", "e1");

                int sor = 2;

                if (AdatokOsztály != null)
                {
                    foreach (Adat_Épület_Takarítás_Osztály rekord in AdatokOsztály)
                    {
                        MyX.Kiir(rekord.Osztály.Trim(), $"a{sor}");
                        MyX.Kiir($"#SZÁMD#{rekord.E1Ft}", $"c{sor}");
                        MyX.Kiir($"#SZÁMD#{rekord.E2Ft}", $"d{sor}");
                        MyX.Kiir($"#SZÁMD#{rekord.E3Ft}", $"e{sor}");

                        sor += 1;
                    }
                }

                MyX.Oszlopszélesség(munkalap, "A:A");
                MyX.Oszlopszélesség(munkalap, "B:B");
                MyX.OszlopRejtés(munkalap, "B:B");
                MyX.Oszlopszélesség(munkalap, "C:E");

                MyX.Rácsoz(munkalap, $"a1:e{sor - 1}");

                Beállítás_Nyomtatás benyom = new Beállítás_Nyomtatás
                {
                    Munkalap = "Összesítő",
                    NyomtatásiTerület = $"a1:e{sor - 1}"
                };
                MyX.NyomtatásiTerület_részletes(munkalap, benyom);

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

        private void RészletesMunkalap()
        {
            try
            {

                string munkalap = Telephely;
                MyX.Munkalap_Új(Telephely);
                MyX.Munkalap_aktív(munkalap);
                Beállítás_Betű BeB = new Beállítás_Betű { Név = "Calibri", Méret = 10 };
                MyX.Munkalap_betű(munkalap, BeB);

                // ************************************************
                // ************ fejléc elkészítése  ***************
                // ************************************************

                MyX.Egyesít(munkalap, "b1:b2");
                MyX.Kiir("Helyiség", "b1");
                MyX.Egyesít(munkalap, "c1:c2");
                MyX.Kiir("Alapterület [m2]", "c1");
                MyX.Egyesít(munkalap, "d1:i1");
                MyX.Kiir("Teljesítési mennyiségek", "d1");
                MyX.Kiir("Szolgálatási jegyzék kódja", "d2");
                MyX.Kiir("Szolgálatási jegyzék kódja", "f2");
                MyX.Kiir("Szolgálatási jegyzék kódja", "h2");
                MyX.Kiir("Teljesített mennyiség", "e2");
                MyX.Kiir("Teljesített mennyiség", "g2");
                MyX.Kiir("Teljesített mennyiség", "i2");
                MyX.Egyesít(munkalap, "j1:j2");
                MyX.Kiir("E1 Egységár [Ft/alkalom]", "j1");
                MyX.Egyesít(munkalap, "k1:k2");
                MyX.Kiir("E2 Egységár [Ft/alkalom]", "k1");
                MyX.Egyesít(munkalap, "l1:l2");
                MyX.Kiir("E3 Egységár [Ft/alkalom]", "l1");
                MyX.Egyesít(munkalap, "m1:m2");
                MyX.Kiir("E1 \nTeljesített\n összeg\n [Ft/hó]", "m1");
                MyX.Egyesít(munkalap, "n1:n2");
                MyX.Kiir("E2 \nTeljesített\n összeg\n [Ft/hó]", "n1");
                MyX.Egyesít(munkalap, "o1:o2");
                MyX.Kiir("E3 \nTeljesített\n összeg\n [Ft/hó]", "o1");
                MyX.Egyesít(munkalap, "p1:p2");
                MyX.Kiir("Összesen: [Ft/hó]", "p1");
                MyX.Sormagasság(munkalap, "1:1", 47);
                MyX.Sormagasság(munkalap, "2:2", 39);
                MyX.Oszlopszélesség(munkalap, "B:B", 46);
                MyX.Oszlopszélesség(munkalap, "c:i", 11);
                MyX.Oszlopszélesség(munkalap, "j:p", 15);
                MyX.Sortörésseltöbbsorba(munkalap, "c1:c2", true);
                MyX.Sortörésseltöbbsorba(munkalap, "d2:d2", true);
                MyX.Sortörésseltöbbsorba(munkalap, "e2:e2", true);
                MyX.Sortörésseltöbbsorba(munkalap, "f2:f2", true);
                MyX.Sortörésseltöbbsorba(munkalap, "g2:g2", true);
                MyX.Sortörésseltöbbsorba(munkalap, "h2:h2", true);
                MyX.Sortörésseltöbbsorba(munkalap, "i2:i2", true);

                MyX.Sortörésseltöbbsorba(munkalap, "j1:j2", true);
                MyX.Sortörésseltöbbsorba(munkalap, "k1:k2", true);
                MyX.Sortörésseltöbbsorba(munkalap, "l1:l2", true);
                MyX.Sortörésseltöbbsorba(munkalap, "m1:m2", true);
                MyX.Sortörésseltöbbsorba(munkalap, "n1:n2", true);
                MyX.Sortörésseltöbbsorba(munkalap, "o1:o2", true);
                MyX.Sortörésseltöbbsorba(munkalap, "p1:p2", true);

                // a táblázat érdemi része
                int sor = 2;
                double NagySum = 0;
                if (AdatokOsztály != null)
                {
                    foreach (Adat_Épület_Takarítás_Osztály rekord in AdatokOsztály)
                    {

                        sor += 1;
                        MyX.Egyesít(munkalap, $"b{sor}:p{sor}");
                        MyX.Igazít_vízszintes(munkalap, $"b{sor}:p{sor}", "bal");
                        MyX.Háttérszín(munkalap, $"b{sor}:p{sor}", Color.GreenYellow);
                        MyX.Kiir(rekord.Osztály.Trim(), $"b{sor}");
                        MyX.Sormagasság(munkalap, $"{sor}:{sor}", 20);

                        double E1SzumMennyi = 0;
                        double E2SzumMennyi = 0;
                        double E3SzumMennyi = 0;

                        double E1Szum = 0;
                        double E2Szum = 0;
                        double E3Szum = 0;

                        int idE1Ö = 0;
                        int idE2Ö = 0;
                        int idE3Ö = 0;

                        List<Adat_Épület_Adattábla> Épületkategória = AdatokRészletes.Where(a => a.Osztály == rekord.Osztály).ToList();
                        if (Épületkategória != null)
                        {
                            foreach (Adat_Épület_Adattábla rekord1 in Épületkategória)
                            {

                                sor++;
                                MyX.Kiir(rekord1.Osztály.Trim(), $"A{sor}");
                                MyX.Kiir(rekord1.Megnevezés.Trim(), $"b{sor}");
                                MyX.Kiir($"#SZÁMD#{rekord1.Méret}", $"c{sor}");
                                MyX.Kiir("E1", $"d{sor}");
                                MyX.Kiir("E2", $"f{sor}");
                                MyX.Kiir("E3", $"h{sor}");
                                int idE1db = 0;
                                int idE2db = 0;
                                int idE3db = 0;

                                Adat_Épület_Takarításrakijelölt Elem = AdatokKijelöltek.FirstOrDefault(a => a.Hónap == Dátum.Month && a.Helységkód == rekord1.Helységkód.Trim());

                                if (Elem != null)
                                {
                                    idE1db = Elem.E1elvégzettdb;
                                    idE2db = Elem.E2elvégzettdb;
                                    idE3db = Elem.E3elvégzettdb;
                                }
                                MyX.Kiir($"#SZÁMD#{idE1db}", $"e{sor}");
                                MyX.Kiir($"#SZÁMD#{idE2db}", $"g{sor}");
                                MyX.Kiir($"#SZÁMD#{idE3db}", $"i{sor}");

                                MyX.Kiir($"#SZÁMD#{rekord.E1Ft}", $"j{sor}");
                                MyX.Betű(munkalap, $"j{sor}", BeBetűG10Ft);
                                MyX.Kiir($"#SZÁMD#{(rekord.E2Ft * rekord1.Méret)}", $"k{sor}");
                                MyX.Betű(munkalap, $"k{sor}", BeBetűG10Ft);
                                MyX.Kiir($"#SZÁMD#{(rekord.E3Ft * rekord1.Méret)}", $"l{sor}");
                                MyX.Betű(munkalap, $"l{sor}", BeBetűG10Ft);

                                MyX.Kiir("#KÉPLET#=RC[-3]*RC[-8]", $"m{sor}");
                                MyX.Betű(munkalap, $"M{sor}", BeBetűG10Ft);
                                MyX.Kiir("#KÉPLET#=RC[-3]*RC[-7]", $"n{sor}");
                                MyX.Betű(munkalap, $"N{sor}", BeBetűG10Ft);
                                MyX.Kiir("#KÉPLET#=RC[-3]*RC[-6]", $"o{sor}");
                                MyX.Betű(munkalap, $"O{sor}", BeBetűG10Ft);
                                MyX.Kiir("#KÉPLET#=SUM(RC[-3]:RC[-1])", $"p{sor}");
                                MyX.Betű(munkalap, $"P{sor}", BeBetűG10Ft);

                                E1SzumMennyi += (idE1db);
                                E2SzumMennyi += (idE2db * rekord1.Méret);
                                E3SzumMennyi += (idE3db * rekord1.Méret);

                                E1Szum += (idE1db * rekord.E1Ft);
                                E2Szum += (idE2db * rekord.E2Ft * rekord1.Méret);
                                E3Szum += (idE3db * rekord.E3Ft * rekord1.Méret);

                                idE1Ö += idE1db;
                                idE2Ö += idE2db;
                                idE3Ö += idE3db;
                            }
                            sor++;
                            MyX.Kiir($"{rekord.Osztály} Összesen", $"B{sor}");
                            MyX.Betű(munkalap, $"B{sor}", BeBetűG10V);

                            MyX.Kiir($"#SZÁMD#{E1SzumMennyi}", $"E{sor}");
                            MyX.Betű(munkalap, $"E{sor}", BeBetűG10V);

                            MyX.Kiir($"#SZÁMD#{E2SzumMennyi}", $"G{sor}");
                            MyX.Betű(munkalap, $"G{sor}", BeBetűG10V);

                            MyX.Kiir($"#SZÁMD#{E3SzumMennyi}", $"I{sor}");
                            MyX.Betű(munkalap, $"I{sor}", BeBetűG10V);

                            MyX.Kiir($"#SZÁMD#{E1Szum.ToString()}", $"m{sor}");
                            MyX.Betű(munkalap, $"M{sor}", BeBetűG10VFt);

                            MyX.Kiir($"#SZÁMD#{E2Szum.ToString()}", $"n{sor}");
                            MyX.Betű(munkalap, $"N{sor}", BeBetűG10VFt);

                            MyX.Kiir($"#SZÁMD#{E3Szum.ToString()}", $"o{sor}");
                            MyX.Betű(munkalap, $"O{sor}", BeBetűG10VFt);

                            MyX.Kiir($"#SZÁMD#{E1Szum + E2Szum + E3Szum}", $"p{sor}");
                            MyX.Betű(munkalap, $"P{sor}", BeBetűG10VFt);

                            Adat_ÉpJár_Takarítás_TIG TigElem;
                            if (idE1Ö != 0)
                            {
                                TigElem = new Adat_ÉpJár_Takarítás_TIG(rekord.Osztály, "E1", E1SzumMennyi, "db", rekord.E1Ft, E1Szum);
                                AdatokTIG.Add(TigElem);
                            }
                            if (idE2Ö != 0)
                            {
                                TigElem = new Adat_ÉpJár_Takarítás_TIG(rekord.Osztály, "E2", E2SzumMennyi, "m2", rekord.E2Ft, E2Szum);
                                AdatokTIG.Add(TigElem);
                            }
                            if (idE3Ö != 0)
                            {
                                TigElem = new Adat_ÉpJár_Takarítás_TIG(rekord.Osztály, "E3", E3SzumMennyi, "m2", rekord.E3Ft, E3Szum);
                                AdatokTIG.Add(TigElem);
                            }
                            NagySum += E1Szum + E2Szum + E3Szum;
                        }
                    }
                }

                // összesítő sor
                sor += 1;
                MyX.Igazít_vízszintes(munkalap, $"b{sor}:p{sor}", "bal");
                MyX.Háttérszín(munkalap, $"b{sor}:p{sor}", Color.GreenYellow);
                MyX.Egyesít(munkalap, $"b{sor}:o{sor}");
                MyX.Kiir(Telephely + " Összesen/hó", $"b{sor}:o{sor}");

                MyX.Betű(munkalap, $"b{sor}:o{sor}", BeBetűGV);
                MyX.Egyesít(munkalap, $"b{sor}:o{sor}");
                MyX.Kiir($"#SZÁMD#{NagySum}", $"p{sor}");
                MyX.Betű(munkalap, $"P{sor}", BeBetűGFtV);

                MyX.Rácsoz(munkalap, $"b1:p{sor}");
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 25);

                MyX.OszlopRejtés(munkalap, "A:A");
                // bezárjuk az Excel-t
                Beállítás_Nyomtatás benyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:P{sor}",
                    IsmétlődőSorok = "$1:$1",
                    Álló = false

                };
                MyX.NyomtatásiTerület_részletes(munkalap, benyom);

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

        private void OpcióslapAdata()
        {
            OpcióListaFeltöltés();
            OpcióListaTelepFeltöltés();
            AdatokTIG.Clear();
            Adat_ÉpJár_Takarítás_TIG TigElem;
            foreach (Adat_Takarítás_Opció rekord in AdatokTakOpció)
            {
                Adat_Takarítás_Telep_Opció Elem = AdatokTakTelepOpció.Where(a => a.Id == rekord.Id && a.Dátum == new DateTime(Dátum.Year, Dátum.Month, 1)).FirstOrDefault();
                if (Elem != null)
                {
                    double Szum = rekord.Ár * Elem.Teljesített;
                    if (Szum != 0)
                    {
                        TigElem = new Adat_ÉpJár_Takarítás_TIG(rekord.Megnevezés, "", Elem.Teljesített, rekord.Mennyisége, rekord.Ár, Szum);
                        AdatokTIG.Add(TigElem);
                    }
                }
            }
        }

        private void OpcióListaFeltöltés()
        {
            try
            {
                AdatokTakOpció.Clear();
                AdatokTakOpció = KézOpció.Lista_Adatok();
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

        private void OpcióListaTelepFeltöltés()
        {
            try
            {
                AdatokTakTelepOpció.Clear();
                AdatokTakTelepOpció = KézTelep.Lista_Adatok(Telephely, Dátum.Year);
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

        #region Jármű
        public void ExcelJárműTábla(string fájlexc)
        {
            // kimeneti fájl helye és neve


            // megnyitjuk az excelt
            AláíróListaFeltöltés();
            BMRListaFeltöltés();
            TeljesítésListaFeltöltés();
            J1ListaFeltöltés();
            ÁrListaFeltöltés();

            string munkalap = "Melléklet";
            MyX.ExcelLétrehozás(munkalap);



            AdatokTIG.Clear();
            MellékletElkészítés();
            TIGElkészítés("TIG", "Jármű takarítási");

            // az excel tábla bezárása
            MyX.ExcelMentés(fájlexc);
            MyX.ExcelBezárás();
            MyF.Megnyitás(fájlexc);
        }

        private void AláíróListaFeltöltés()
        {
            Kezelő_Kiegészítő_főkönyvtábla KézFőkönyv = new Kezelő_Kiegészítő_főkönyvtábla();
            List<Adat_Kiegészítő_főkönyvtábla> AdatokFőkönyv = KézFőkönyv.Lista_Adatok(Telephely);
            Adat_Kiegészítő_főkönyvtábla Elem = (from a in AdatokFőkönyv
                                                 where a.Id == 2
                                                 select a).FirstOrDefault();

            if (Elem != null)
            {
                AláíróBeosztás = Elem.Beosztás;
                AláíróNév = Elem.Név;
            }
        }

        public void J1ListaFeltöltés()
        {
            try
            {
                AdatokJ1.Clear();
                AdatokJ1 = KézJ1.Lista_Adat(Telephely, Dátum.Year);
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

        public void TeljesítésListaFeltöltés()
        {
            try
            {
                AdatokTelj.Clear();
                AdatokTelj = KézTelj.Lista_Adatok(Telephely, Dátum.Year);
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

        public void ÁrListaFeltöltés()
        {
            try
            {
                AdatokÁr.Clear();
                AdatokÁr = KézTakÁr.Lista_Adatok();
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

        private void MellékletElkészítés()
        {
            try
            {
                string munkalap = "Melléklet";
                MyX.Munkalap_aktív(munkalap);
                MyX.Munkalap_betű(munkalap, BeBetűG12);

                int sor = 1;
                MyX.Oszlopszélesség(munkalap, "A:A", 25);
                MyX.Oszlopszélesség(munkalap, "B:B", 35);
                MyX.Oszlopszélesség(munkalap, "C:C", 25);
                MyX.Oszlopszélesség(munkalap, "D:D", 25);
                MyX.Oszlopszélesség(munkalap, "E:E", 25);
                MyX.Oszlopszélesség(munkalap, "F:F", 25);

                int eleje = sor;
                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum);
                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum);
                Típusok_Gyűjtése(hónapelsőnapja, hónaputolsónapja);

                foreach (string Típus in TIGTípusok)
                {

                    TigMellékletFejléc(munkalap, sor);
                    J1_Adatok_Melléklet(munkalap, ++sor, 1, "Nappal", Típus, hónapelsőnapja, hónaputolsónapja);
                    J1_Adatok_Melléklet(munkalap, ++sor, 2, "Éjszaka", Típus, hónapelsőnapja, hónaputolsónapja);

                    for (int ii = 0; ii <= Lekérdezés_Kategória.Count - 1; ii++)
                    {
                        MyX.Kiir(Telephely, $"A{++sor}");
                        Többi_Adatok_Melléklet(munkalap, Lekérdezés_Kategória[ii], sor, 1, "Nappal", Típus, hónapelsőnapja, hónaputolsónapja);
                        MyX.Kiir(Telephely, $"A{++sor}");
                        Többi_Adatok_Melléklet(munkalap, Lekérdezés_Kategória[ii], sor, 2, "Éjszaka", Típus, hónapelsőnapja, hónaputolsónapja);
                    }
                    MyX.Rácsoz(munkalap, $"A{eleje}:F{sor}");
                    sor++;
                    MyX.Kiir("#KÉPLET#=SUM(R[-18]C[-1]:R[-1]C[-1])", $"F{sor}");
                    MyX.Betű(munkalap, $"F{sor}", BeBetűGFt);
                    MyX.Kiir($"#SZÁMD#{Típus} Összesen", $"E{sor}");
                    MyX.Rácsoz(munkalap, $"A{sor}:F{sor}");

                    sor += 2;
                    eleje = sor;
                }
                MyX.Kiir($"#KÉPLET#=SUM(R[-{sor - 1}]C:R[-2]C)", $"F{sor}");
                MyX.Betű(munkalap, $"F{sor}", BeBetűGFt);
                MyX.Kiir($"Végösszesen", $"E{sor}");
                MyX.Rácsoz(munkalap, $"A{sor}:F{sor}");
                Beállítás_Nyomtatás benyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:F{sor}",
                    IsmétlődőSorok = "$1:$1",
                    Álló = false,
                    LapSzéles = 1,
                    FejlécBal = $"{Telephely} Járműtakarítás",
                    FejlécKözép = $"{Dátum.Year}.{Dátum.Month}. Hónap",
                    FejlécJobb = "&P/&N"
                };
                MyX.NyomtatásiTerület_részletes(munkalap, benyom);
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

        private void Típusok_Gyűjtése(DateTime hónapelsőnapja, DateTime hónaputolsónapja)
        {
            List<string> Ideig = (from a in AdatokJ1
                                  where a.Dátum >= hónapelsőnapja
                                  && a.Dátum <= hónaputolsónapja
                                  orderby a.Típus
                                  select a.Típus).ToList().Distinct().ToList();
            TIGTípusok.AddRange(Ideig);
            Ideig = (from a in AdatokTelj
                     where a.Dátum >= hónapelsőnapja
                         && a.Dátum <= hónaputolsónapja
                     orderby a.Típus
                     select a.Típus).ToList().Distinct().ToList();
            TIGTípusok.AddRange(Ideig);

            Ideig = (from a in TIGTípusok
                     orderby a
                     select a).ToList().Distinct().ToList();
            TIGTípusok.Clear();
            TIGTípusok.AddRange(Ideig);
        }

        private void J1_Adatok_Melléklet(string munkalap, int sor, int napszak, string ÉjNAP, string típus, DateTime hónapelsőnapja, DateTime hónaputolsónapja)
        {

            List<Adat_Jármű_Takarítás_J1> RészAdatokJ1 = (from a in AdatokJ1
                                                          where a.Dátum >= hónapelsőnapja
                                                          && a.Dátum <= hónaputolsónapja
                                                          && a.Típus == típus
                                                          && a.Napszak == napszak
                                                          select a).ToList();
            double Mennyiség = RészAdatokJ1.Sum(a => a.J1megfelelő);
            string ME = "db";

            Adat_Jármű_Takarítás_Árak RészAdatokÁr = (from a in AdatokÁr
                                                      where a.JárműTípus == típus
                                                      && a.Takarítási_fajta == "J1"
                                                      && a.Napszak == napszak
                                                      && a.Érv_kezdet <= hónapelsőnapja
                                                      && a.Érv_vég >= hónaputolsónapja
                                                      select a).FirstOrDefault();

            MyX.Kiir(Telephely, $"A{sor}");
            MyX.Kiir($"{típus} J1 {ÉjNAP}", $"B{sor}");
            MyX.Kiir($"#SZÁMD#{Mennyiség}", $"C{sor}");
            double Ár = 0;
            if (RészAdatokÁr != null)
            {
                MyX.Kiir($"#SZÁMD#{RészAdatokÁr.Ár}", $"D{sor}");
                Ár = RészAdatokÁr.Ár;
            }
            MyX.Kiir("#KÉPLET#=RC[-2]*RC[-1]", $"E{sor}");

            MyX.Betű(munkalap, $"E{sor}", BeBetűGFt0);
            Adat_ÉpJár_Takarítás_TIG Elemek = new Adat_ÉpJár_Takarítás_TIG(Telephely, $"{típus} J1 {ÉjNAP}", Mennyiség, ME, Ár, Mennyiség * Ár);
            if (Mennyiség * Ár != 0) AdatokTIG.Add(Elemek);
        }

        private void Többi_Adatok_Melléklet(string munkalap, string takarításfajta, int sor, int napszak, string ÉjNAP, string típus, DateTime hónapelsőnapja, DateTime hónaputolsónapja)
        {

            List<Adat_Jármű_Takarítás_Teljesítés> RészAdatok = (from a in AdatokTelj
                                                                where a.Dátum >= hónapelsőnapja
                                                                && a.Dátum <= hónaputolsónapja
                                                                && a.Típus == típus
                                                                && a.Napszak == napszak
                                                                && a.Takarítási_fajta == takarításfajta
                                                                && a.Státus == 1
                                                                select a).ToList();
            double Mennyiség;
            string ME;
            if (takarításfajta == "Graffiti" || takarításfajta == "Eseti" || takarításfajta == "Fertőtlenítés")
            {
                Mennyiség = RészAdatok.Sum(a => a.Mérték);
                ME = "m2";
            }
            else
            {
                Mennyiség = RészAdatok.Sum(a => a.Megfelelt1);
                ME = "db";
            }

            Adat_Jármű_Takarítás_Árak RészAdatokÁr = (from a in AdatokÁr
                                                      where a.JárműTípus == típus
                                                      && a.Takarítási_fajta == takarításfajta
                                                      && a.Napszak == napszak
                                                      && a.Érv_kezdet <= hónapelsőnapja
                                                      && a.Érv_vég >= hónaputolsónapja
                                                      select a).FirstOrDefault();

            MyX.Kiir(Telephely, $"A{sor}");
            MyX.Kiir($"{típus} {takarításfajta} {ÉjNAP}", $"B{sor}");
            MyX.Kiir($"#SZÁMD#{Mennyiség}", $"C{sor}");
            double Ár = 0;
            if (RészAdatokÁr != null)
            {
                MyX.Kiir($"#SZÁMD#{RészAdatokÁr.Ár}", $"D{sor}");
                Ár = RészAdatokÁr.Ár;
            }
            else
            {
                MyX.Kiir("#SZÁMD#0", $"D{sor}");
            }
            MyX.Kiir("#KÉPLET#=RC[-2]*RC[-1]", $"E{sor}");
            MyX.Betű(munkalap, $"E{sor}", BeBetűGFt0);
            Adat_ÉpJár_Takarítás_TIG Elemek = new Adat_ÉpJár_Takarítás_TIG(Telephely, $"{típus} {takarításfajta} {ÉjNAP}", Mennyiség, ME, Ár, Mennyiség * Ár);
            if (Mennyiség * Ár != 0) AdatokTIG.Add(Elemek);
        }

        private void TigMellékletFejléc(string munkalap, int sor)
        {
            MyX.Sortörésseltöbbsorba(munkalap, $"A{sor}:F{sor}");
            MyX.Kiir("Telephely", $"A{sor}");
            MyX.Kiir("Tevékenység\nJárműtípus, takarítási fokozat", $"B{sor}");
            MyX.Kiir("Tarakítási mennyiségek\n(db/hó)", $"C{sor}");
            MyX.Kiir("Egységár\nFt/db", $"D{sor}");
            MyX.Kiir("Összesen\n(Ft/hó)", $"E{sor}");
            MyX.Kiir("Típus Összesen", $"F{sor}");
            MyX.Rácsoz(munkalap, $"A{sor}:F{sor}");
        }
        #endregion


        #region Közös
        private void TIGElkészítés(string munkalap, string munka)
        {
            try
            {
                MyX.Munkalap_Új(munkalap);
                MyX.Munkalap_aktív(munkalap);
                MyX.Munkalap_betű(munkalap, BeBetűG12);
                MyX.Egyesít(munkalap, "a1:d1");

                MyX.Oszlopszélesség(munkalap, "B:B", 10);
                MyX.Oszlopszélesség(munkalap, "C:C", 10);
                MyX.Oszlopszélesség(munkalap, "D:D", 15);
                MyX.Oszlopszélesség(munkalap, "E:E", 13);
                MyX.Oszlopszélesség(munkalap, "F:F", 8);
                MyX.Oszlopszélesség(munkalap, "G:G", 13);
                MyX.Oszlopszélesség(munkalap, "H:H", 13);
                int sor = 1;
                MyX.Egyesít(munkalap, $"D{sor}:H{sor}");
                MyX.Kiir("B+N Referencia  Szerződés száma: BKV Zrt. T-50/20", $"D{sor}");
                MyX.Igazít_vízszintes(munkalap, $"D{sor}", "jobb");

                sor += 2;
                MyX.Egyesít(munkalap, $"A{sor}:H{sor}");
                MyX.Kiir("TELJESÍTÉSIGAZOLÁS", $"A{sor}");
                MyX.Betű(munkalap, $"A{sor}", BeBetűG14V);


                sor += 2;
                MyX.Egyesít(munkalap, $"A{sor}:H{sor}");
                MyX.Kiir($"A Villamos Üzemeltetési Igazgatóság, {Telephely} Járműfenntartó Üzemben végzett {munka} munkákról.", $"A{sor}");
                MyX.Sormagasság(munkalap, $"A{sor}", 35);
                MyX.Sortörésseltöbbsorba(munkalap, $"A{sor}", true);
                MyX.Betű(munkalap, $"A{sor}", BeBetűG14V);

                sor = Megrendelő(munkalap, sor);
                sor = Vállalkozó(munkalap, sor);

                sor += 2;
                MyX.Egyesít(munkalap, $"A{sor}:H{sor}");
                string BMRszám = KeresBMR(munka);
                string irateleje = "Felek rögzítik, hogy 2022.04.29. napján T-50/20. számon „BKV Zrt. járműveinek, telephelyeinek és létesítményeinek takarítása” " +
                    "tárgyban vállalkozási megbízási szerződést (a továbbiakban: Szerződés) kötöttek. \r\nFelek rögzítik, hogy Szerződéshez kapcsolódó,  BMR ";
                string iratvége = " számú megrendelésben(a továbbiakban: Megrendelés) foglaltakat a Vállalkozó a következők szerint végezte el:";
                MyX.Kiir($"{irateleje}-{BMRszám}-{iratvége}", $"A{sor}");

                RichTextRun BeállításCella = new RichTextRun
                {
                    Vastag = true,
                    Start = irateleje.Length,
                    Hossz = BMRszám.Length + 2
                };
                List<RichTextRun> Beállítások = new List<RichTextRun> { BeállításCella };

                Beállítás_CellaSzöveg BeSzöv = new Beállítás_CellaSzöveg
                {
                    Cella = $"A{sor}",
                    MunkalapNév = munkalap,
                    FullText = $"{irateleje}-{BMRszám}-{iratvége}",
                    Beállítások = Beállítások,
                    Betű = BeBetűG12
                };
                MyX.Cella_Betű(BeSzöv);

                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 70);
                MyX.Igazít_vízszintes(munkalap, $"A{sor}", "bal");

                sor += 2;
                sor = TáblázatTIGhez(munkalap, sor, out double Nettó);

                sor += 2;
                MyX.Egyesít(munkalap, $"A{sor}:H{sor}");
                MyX.Kiir($"A teljesítés ideje: {Dátum:yyyy. MMMM} hó", $"A{sor}");
                MyX.Betű(munkalap, $"A{sor}", BeBetűGV);
                MyX.Igazít_vízszintes(munkalap, $"A{sor}", "bal");

                sor += 2;
                MyX.Egyesít(munkalap, $"A{sor}:H{sor}");
                MyX.Sortörésseltöbbsorba(munkalap, $"A{sor}:H{sor}");
                irateleje = "A Megrendelésben foglalt feladatok ellenértéke összesen nettó ";
                string iratközepe = Math.Round(Nettó, 0).ToStrTrim();
                iratvége = " Ft.+ ÁFA,\n azaz ";
                string irateleje1 = MyF.Számszóban((long)Math.Round(Nettó, 0));
                string iratvége1 = "+Áfa";
                string irat = irateleje + iratközepe + iratvége + irateleje1 + iratvége1;

                MyX.Kiir($"{irat}", $"A{sor}");
                RichTextRun BeállításCella1 = new RichTextRun
                {
                    Vastag = true,
                    Start = irateleje.Length,
                    Hossz = iratközepe.Length + 2
                };
                RichTextRun BeállításCella2 = new RichTextRun
                {
                    Vastag = true,
                    Start = irateleje.Length + iratközepe.Length + iratvége.Length,
                    Hossz = irateleje1.Length + 2
                };
                Beállítások = new List<RichTextRun> { BeállításCella1, BeállításCella2 };

                Beállítás_CellaSzöveg BeSzöv1 = new Beállítás_CellaSzöveg
                {
                    Cella = $"A{sor}",
                    MunkalapNév = munkalap,
                    FullText = irat,
                    Beállítások = Beállítások,
                    Betű = BeBetűG12
                };
                MyX.Cella_Betű(BeSzöv1);

                MyX.Igazít_vízszintes(munkalap, $"A{sor}", "bal");
                MyX.Sormagasság(munkalap, $"{sor}:{sor}", 32);


                if (munkalap == "TIG")
                {
                    sor++;
                    MyX.Egyesít(munkalap, $"A{sor}:H{sor}");
                    MyX.Kiir($"Jelen Teljesítési Igazolás 1. számú mellékletét képezi a Vállalkozó jelen Teljesítés Igazolás kiállítását megalapozó lejelentése.", $"A{sor}");
                    MyX.Igazít_vízszintes(munkalap, $"A{sor}", "bal");
                }

                sor++;
                MyX.Egyesít(munkalap, $"A{sor}:H{sor}");
                MyX.Kiir($"A teljesítés a Szerződésben és a Megrendelésben meghatározottak szerint történt.", $"A{sor}");
                MyX.Igazít_vízszintes(munkalap, $"A{sor}", "bal");


                sor = Aláírások(munkalap, sor);
                MyX.Oszlopszélesség(munkalap, "A:A");
                Beállítás_Nyomtatás benyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:H{sor}",
                    IsmétlődőSorok = "$1:$1",
                    Álló = true,
                    LapSzéles = 1,
                    FelsőMargó = 10,
                    AlsóMargó = 25,
                    BalMargó = 10,
                    JobbMargó = 10,
                    FejlécMéret = 13,
                    LáblécMéret = 13

                };
                MyX.NyomtatásiTerület_részletes(munkalap, benyom);
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

        private int Aláírások(string munkalap, int sor)
        {
            sor += 2;
            MyX.Kiir($"Budapest,{DateTime.Today:yyyy. MMMM dd}", $"A{sor}");
            MyX.Egyesít(munkalap, $"F{sor}:H{sor}");
            MyX.Kiir($"Budapest,{DateTime.Today:yyyy. MMMM dd}", $"F{sor}");

            sor += 3;
            MyX.Egyesít(munkalap, $"A{sor}:B{sor}");
            MyX.Egyesít(munkalap, $"F{sor}:H{sor}");


            sor++;
            MyX.Egyesít(munkalap, $"A{sor}:B{sor}");
            MyX.Egyesít(munkalap, $"F{sor}:H{sor}");
            MyX.Kiir(AláíróNév, $"A{sor}");
            MyX.Kiir($"Mong Péter", $"F{sor}");
            MyX.Aláírásvonal(munkalap, $"A{sor}:B{sor}");
            MyX.Aláírásvonal(munkalap, $"F{sor}:H{sor}");

            sor++;
            MyX.Egyesít(munkalap, $"A{sor}:B{sor}");
            MyX.Egyesít(munkalap, $"F{sor}:H{sor}");
            MyX.Kiir(AláíróBeosztás, $"A{sor}");
            MyX.Kiir($"B+N Referencia Zrt.", $"F{sor}");

            sor++;
            MyX.Egyesít(munkalap, $"A{sor}:B{sor}");
            MyX.Egyesít(munkalap, $"F{sor}:H{sor}");
            MyX.Kiir($"BKV ZRT.", $"A{sor}");
            MyX.Kiir($"3644 Tardona, Katus domb 1", $"F{sor}");

            sor++;
            MyX.Egyesít(munkalap, $"A{sor}:B{sor}");
            MyX.Egyesít(munkalap, $"F{sor}:H{sor}");
            MyX.Kiir($"", $"A{sor}");
            MyX.Kiir($"Adószám: 23480874-2-05", $"F{sor}");

            sor += 4;
            MyX.Kiir($"Szállítóiminősítés:", $"A{sor}");
            MyX.Betű(munkalap, $"A{sor}", BeBetűGA);

            sor++;
            MyX.Kiir($"Szolgáltatás minősége: ", $"A{sor}");
            MyX.Kiir($"%", $"B{sor}");
            MyX.Igazít_vízszintes(munkalap, $"B{sor}", "jobb");
            MyX.Aláírásvonal(munkalap, $"B{++sor}");


            sor++;
            MyX.Kiir($"Szolgáltatás határideje:", $"A{sor}");
            MyX.Kiir($"%", $"B{sor}");
            MyX.Igazít_vízszintes(munkalap, $"B{sor}", "jobb");
            MyX.Aláírásvonal(munkalap, $"B{++sor}");
            return sor;
        }

        private void BMRListaFeltöltés()
        {
            try
            {
                AdatokBMR.Clear();
                AdatokBMR = KézBMR.Lista_Adatok();
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

        private string KeresBMR(string munka)
        {
            string válasz = "-Nincs megadva BMR szám-";
            try
            {
                if (munka.Trim() == "opcionális") munka = "Épület takarítási";
                string[] darabol = munka.Split(' ');

                Adat_Takarítás_BMR Elem = (from a in AdatokBMR
                                           where a.JárműÉpület == darabol[0].Trim() &&
                                           a.Dátum == new DateTime(Dátum.Year, Dátum.Month, 1) &&
                                           a.Telephely == Telephely
                                           select a).FirstOrDefault();
                if (Elem != null) válasz = Elem.BMRszám;
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

        private int Megrendelő(string munkalap, int sor)
        {
            sor += 2;
            MyX.Kiir("Felek adatai:", $"A{sor}");

            sor++;
            MyX.Egyesít(munkalap, $"A{sor}:H{sor}");
            MyX.Kiir("Budapesti Közlekedési Zártkörűen Működő Részvénytársaság", $"A{sor}");
            MyX.Betű(munkalap, $"B{sor}", BeBetűG14V);
            MyX.Igazít_vízszintes(munkalap, $"B{sor}", "bal");

            sor++;
            MyX.Egyesít(munkalap, $"B{sor}:C{sor}");
            MyX.Egyesít(munkalap, $"D{sor}:H{sor}");
            MyX.Kiir("Székhely:", $"B{sor}");
            MyX.Kiir("1980 Budapest, Akácfa utca 15.", $"D{sor}");
            MyX.Igazít_vízszintes(munkalap, $"B{sor}", "bal");
            MyX.Igazít_vízszintes(munkalap, $"D{sor}", "bal");

            sor++;
            MyX.Egyesít(munkalap, $"B{sor}:C{sor}");
            MyX.Egyesít(munkalap, $"D{sor}:H{sor}");
            MyX.Kiir("Cégjegyzékszám:", $"B{sor}");
            MyX.Kiir("01-10-043037", $"D{sor}");
            MyX.Igazít_vízszintes(munkalap, $"B{sor}", "bal");
            MyX.Igazít_vízszintes(munkalap, $"D{sor}", "bal");

            sor++;
            MyX.Egyesít(munkalap, $"B{sor}:C{sor}");
            MyX.Egyesít(munkalap, $"D{sor}:H{sor}");
            MyX.Kiir("Adószám:", $"B{sor}");
            MyX.Kiir("12154481-4-44", $"D{sor}");
            MyX.Igazít_vízszintes(munkalap, $"B{sor}", "bal");
            MyX.Igazít_vízszintes(munkalap, $"D{sor}", "bal");

            sor++;
            MyX.Egyesít(munkalap, $"B{sor}:C{sor}");
            MyX.Egyesít(munkalap, $"D{sor}:H{sor}");
            MyX.Kiir("Csoport azonosító:", $"B{sor}");
            MyX.Kiir("17781372-5-44", $"D{sor}");
            MyX.Igazít_vízszintes(munkalap, $"B{sor}", "bal");
            MyX.Igazít_vízszintes(munkalap, $"D{sor}", "bal");

            sor++;
            MyX.Egyesít(munkalap, $"B{sor}:H{sor}");
            MyX.Kiir("Mint megrendelő (a továbbiakban: BKV Zrt.)", $"B{sor}");
            MyX.Igazít_vízszintes(munkalap, $"B{sor}", "bal");
            return sor;
        }

        private int Vállalkozó(string munkalap, int sor)
        {
            sor += 2;
            MyX.Kiir("Valamint", $"A{sor}");

            sor++;
            MyX.Egyesít(munkalap, $"A{sor}:H{sor}");
            MyX.Kiir("B+N Referencia Ipari, Kereskedelmi és Szolgáltató Zártkörűen Működő Részvénytársaság", $"A{sor}");
            MyX.Betű(munkalap, $"B{sor}", BeBetűG14V);

            MyX.Igazít_vízszintes(munkalap, $"B{sor}", "bal");

            sor++;
            MyX.Egyesít(munkalap, $"B{sor}:C{sor}");
            MyX.Egyesít(munkalap, $"D{sor}:H{sor}");
            MyX.Kiir("Székhely:", $"B{sor}");
            MyX.Kiir("3644 Tardona, Katus domb 1.", $"D{sor}");
            MyX.Igazít_vízszintes(munkalap, $"B{sor}", "bal");
            MyX.Igazít_vízszintes(munkalap, $"D{sor}", "bal");

            sor++;
            MyX.Egyesít(munkalap, $"B{sor}:C{sor}");
            MyX.Egyesít(munkalap, $"D{sor}:H{sor}");
            MyX.Kiir("Cégjegyzékszám:", $"B{sor}");
            MyX.Kiir("05-10-000479 ", $"D{sor}");
            MyX.Igazít_vízszintes(munkalap, $"B{sor}", "bal");
            MyX.Igazít_vízszintes(munkalap, $"D{sor}", "bal");

            sor++;
            MyX.Egyesít(munkalap, $"B{sor}:C{sor}");
            MyX.Egyesít(munkalap, $"D{sor}:H{sor}");
            MyX.Kiir("Adószám:", $"B{sor}");
            MyX.Kiir("23480874-2-05", $"D{sor}");
            MyX.Igazít_vízszintes(munkalap, $"B{sor}", "bal");
            MyX.Igazít_vízszintes(munkalap, $"D{sor}", "bal");

            sor++;
            MyX.Egyesít(munkalap, $"B{sor}:C{sor}");
            MyX.Egyesít(munkalap, $"D{sor}:H{sor}");
            MyX.Kiir("Csoport azonosító:", $"B{sor}");
            MyX.Kiir("12001008-01705737-01700004", $"D{sor}");
            MyX.Igazít_vízszintes(munkalap, $"B{sor}", "bal");
            MyX.Igazít_vízszintes(munkalap, $"D{sor}", "bal");

            sor++;
            MyX.Egyesít(munkalap, $"B{sor}:H{sor}");
            MyX.Kiir("Mint vállalkozó (a továbbiakban: Vállalkozó)", $"B{sor}");
            MyX.Igazít_vízszintes(munkalap, $"B{sor}", "bal");
            return sor;
        }

        private int TáblázatTIGhez(string munkalap, int sor, out double Összesen)
        {
            int eleje = sor;
            MyX.Kiir("Leírás", $"A{sor}");
            MyX.Kiir("Mennyiség", $"B{sor}");
            MyX.Kiir("Mennyiség\r\nEgység", $"C{sor}");
            MyX.Kiir("Egység ár [Ft]", $"D{sor}");
            MyX.Kiir("Nettó ár [Ft]", $"E{sor}");
            MyX.Kiir("Áfa [%]", $"F{sor}");
            MyX.Kiir("Áfa [Ft]", $"G{sor}");
            MyX.Kiir("Bruttó Ár [Ft]", $"H{sor}");

            Összesen = 0;

            foreach (Adat_ÉpJár_Takarítás_TIG Elem in AdatokTIG)
            {
                sor++;
                MyX.Kiir($"{Elem.Telephely} {Elem.Tevékenység}", $"A{sor}");
                MyX.Kiir($"#SZÁMD#{Elem.Mennyiség}", $"B{sor}");
                MyX.Kiir(Elem.ME, $"C{sor}");
                MyX.Kiir($"#SZÁMD#{Elem.Egységár}", $"D{sor}");
                MyX.Betű(munkalap, $"D{sor}", BeBetűGS);
                MyX.Kiir($"#SZÁMD#{Math.Round(Elem.Összesen)}", $"E{sor}");
                MyX.Betű(munkalap, $"E{sor}", BeBetűGS);
                Összesen += Math.Round(Elem.Összesen);

                MyX.Betű(munkalap, $"E{sor}", BeBetűG0);
                MyX.Kiir("#SZÁME#27", $"F{sor}");
                MyX.Kiir("#KÉPLET#=RC[-1]*RC[-2]/100", $"G{sor}");
                MyX.Betű(munkalap, $"G{sor}", BeBetűGS);
                MyX.Kiir("#KÉPLET#=RC[-1]+RC[-3]", $"H{sor}");
                MyX.Betű(munkalap, $"H{sor}", BeBetűGS);
            }
            MyX.Rácsoz(munkalap, $"A{eleje}:A{sor}");
            MyX.Rácsoz(munkalap, $"B{eleje}:H{sor}");
            MyX.Rácsoz(munkalap, $"A{eleje}:H{eleje}");
            sor++;
            MyX.Kiir("Összesen:", $"A{sor}");
            MyX.Betű(munkalap, $"A{sor}", BeBetűGV);
            MyX.Kiir($"#KÉPLET#=SUM(R[-{sor - eleje}]C:R[-1]C)", $"E{sor}");
            MyX.Betű(munkalap, $"E{sor}", BeBetűGSP);

            MyX.Kiir($"#KÉPLET#=SUM(R[-{sor - eleje}]C:R[-1]C)", $"G{sor}");
            MyX.Betű(munkalap, $"G{sor}", BeBetűGSV);

            MyX.Kiir($"#KÉPLET#=SUM(R[-{sor - eleje}]C:R[-1]C)", $"H{sor}");
            MyX.Betű(munkalap, $"H{sor}", BeBetűGSV);

            MyX.Rácsoz(munkalap, $"A{sor}:H{sor}");


            return sor;
        }
        #endregion
    }
}
