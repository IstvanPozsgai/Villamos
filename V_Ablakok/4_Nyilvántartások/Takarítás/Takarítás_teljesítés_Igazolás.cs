using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Takarítás
{
    public class Takarítás_teljesítés_Igazolás
    {
        public DateTime Dátum { get; private set; }
        public bool Jármű { get; private set; }
        public string Telephely { get; private set; }

        private string AláíróBeosztás = "";
        private string AláíróNév = "";

        readonly Kezelő_Épület_Takarításosztály KézTakarításOsztály = new Kezelő_Épület_Takarításosztály();
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
        List<Adat_Épület_Takarításosztály> AdatokOsztály = new List<Adat_Épület_Takarításosztály>();
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

        public Takarítás_teljesítés_Igazolás(DateTime dátum, bool jármű, string telephely)
        {
            Dátum = dátum;
            Jármű = jármű;
            Telephely = telephely;
        }

        #region Épület
        public void ExcelÉpületTábla(string fájlexc)
        {

            string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Épület\épülettörzs.mdb";
            string jelszó = "seprűéslapát";
            string szöveg = "SELECT * FROM takarításosztály where státus=0 ORDER BY id";
            AdatokOsztály = KézTakarításOsztály.Lista_Adatok(hely, jelszó, szöveg);
            string helyép = $@"{Application.StartupPath}\{Telephely}\Adatok\Épület\{Dátum.Year}épülettakarítás.mdb";

            szöveg = "SELECT * FROM Adattábla where státus=0  ORDER BY id";
            AdatokRészletes = KézAdatTábla.Lista_Adatok(hely, jelszó, szöveg);

            szöveg = "SELECT * FROM takarításrakijelölt";
            AdatokKijelöltek = KézTakarításrakijelölt.Lista_Adatok(helyép, jelszó, szöveg);
            AdatokTIG.Clear();

            string helyop = $@"{Application.StartupPath}\Főmérnökség\Adatok\Takarítás\Opcionális.mdb";
            szöveg = "SELECT * FROM TakarításOpcionális ORDER BY ID";
            AdatokTakOpció = KézOpció.Lista_Adatok(helyop, jelszó, szöveg);

            AdatokTakTelepOpció.Clear();
            string helytelop = $@"{Application.StartupPath}\{Telephely}\Adatok\Épület\Opcionális{Dátum.Year}.mdb";
            szöveg = "SELECT * FROM TakarításOpcTelepAdatok";
            AdatokTakTelepOpció = KézTelep.Lista_Adatok(helytelop, jelszó, szöveg);

            GondnokListaFeltöltés();
            BMRListaFeltöltés();

            // megnyitjuk az excelt
            MyE.ExcelLétrehozás();

            ÁrMunkalap();
            RészletesMunkalap();

            if (AdatokTIG.Count != 0) TIGElkészítés("TIG", "Épület takarítási");
            OpcióslapAdata();
            if (AdatokTIG.Count != 0) TIGElkészítés("TIG_opció", "opcionális");

            MyE.ExcelMentés(fájlexc);
            MyE.ExcelBezárás();
            MyE.Megnyitás(fájlexc);
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
                // *********************************************
                // ********* Osztály tábla *********************
                // *********************************************
                // fejléc elkészítése
                MyE.Kiir("Megnevezés", "a1");
                MyE.Kiir("E1 Egységár [db]", "c1");
                MyE.Kiir("E2 Egységár [Ft/m2]", "d1");
                MyE.Kiir("E3 Egységár [Ft/m2]", "e1");

                int sor = 2;

                if (AdatokOsztály != null)
                {
                    foreach (Adat_Épület_Takarításosztály rekord in AdatokOsztály)
                    {
                        MyE.Kiir(rekord.Osztály.Trim(), $"a{sor}");
                        MyE.Kiir(rekord.E1Ft.ToString().Replace(",", "."), $"c{sor}");
                        MyE.Kiir(rekord.E2Ft.ToString().Replace(",", "."), $"d{sor}");
                        MyE.Kiir(rekord.E3Ft.ToString().Replace(",", "."), $"e{sor}");

                        sor += 1;
                    }
                }
                string munkalap = "Munka1";
                MyE.Oszlopszélesség(munkalap, "A:A");
                MyE.Oszlopszélesség(munkalap, "B:B");
                MyE.OszlopRejtés(munkalap, "B:B");
                MyE.Oszlopszélesség(munkalap, "C:E");

                MyE.Rácsoz($"a1:e{sor - 1}");
                MyE.Munkalap_átnevezés(munkalap, "Összesítő");
                MyE.NyomtatásiTerület_részletes("Összesítő", $"a1:e{sor - 1}", "", "", true);

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
                MyE.Új_munkalap(Telephely);
                MyE.Munkalap_betű("Calibri", 10);

                // ************************************************
                // ************ fejléc elkészítése  ***************
                // ************************************************

                MyE.Egyesít(munkalap, "b1:b2");
                MyE.Kiir("Helyiség", "b1");
                MyE.Egyesít(munkalap, "c1:c2");
                MyE.Kiir("Alapterület [m2]", "c1");
                MyE.Egyesít(munkalap, "d1:i1");
                MyE.Kiir("Teljesítési mennyiségek", "d1");
                MyE.Kiir("Szolgálatási jegyzék kódja", "d2");
                MyE.Kiir("Szolgálatási jegyzék kódja", "f2");
                MyE.Kiir("Szolgálatási jegyzék kódja", "h2");
                MyE.Kiir("Teljesített mennyiség", "e2");
                MyE.Kiir("Teljesített mennyiség", "g2");
                MyE.Kiir("Teljesített mennyiség", "i2");
                MyE.Egyesít(munkalap, "j1:j2");
                MyE.Kiir("E1 Egységár [Ft/alkalom]", "j1");
                MyE.Egyesít(munkalap, "k1:k2");
                MyE.Kiir("E2 Egységár [Ft/alkalom]", "k1");
                MyE.Egyesít(munkalap, "l1:l2");
                MyE.Kiir("E3 Egységár [Ft/alkalom]", "l1");
                MyE.Egyesít(munkalap, "m1:m2");
                MyE.Kiir("E1 \nTeljesített\n összeg\n [Ft/hó]", "m1");
                MyE.Egyesít(munkalap, "n1:n2");
                MyE.Kiir("E2 \nTeljesített\n összeg\n [Ft/hó]", "n1");
                MyE.Egyesít(munkalap, "o1:o2");
                MyE.Kiir("E3 \nTeljesített\n összeg\n [Ft/hó]", "o1");
                MyE.Egyesít(munkalap, "p1:p2");
                MyE.Kiir("Összesen: [Ft/hó]", "p1");
                MyE.Sormagasság("1:1", 47);
                MyE.Sormagasság("2:2", 39);
                MyE.Oszlopszélesség(munkalap, "B:B", 46);
                MyE.Oszlopszélesség(munkalap, "c:i", 11);
                MyE.Oszlopszélesség(munkalap, "j:p", 15);
                MyE.Sortörésseltöbbsorba("c1:c2", true);
                MyE.Sortörésseltöbbsorba("d2:d2", true);
                MyE.Sortörésseltöbbsorba("e2:e2", true);
                MyE.Sortörésseltöbbsorba("f2:f2", true);
                MyE.Sortörésseltöbbsorba("g2:g2", true);
                MyE.Sortörésseltöbbsorba("h2:h2", true);
                MyE.Sortörésseltöbbsorba("i2:i2", true);

                MyE.Sortörésseltöbbsorba("j1:j2", true);
                MyE.Sortörésseltöbbsorba("k1:k2", true);
                MyE.Sortörésseltöbbsorba("l1:l2", true);
                MyE.Sortörésseltöbbsorba("m1:m2", true);
                MyE.Sortörésseltöbbsorba("n1:n2", true);
                MyE.Sortörésseltöbbsorba("o1:o2", true);
                MyE.Sortörésseltöbbsorba("p1:p2", true);

                // a táblázat érdemi része
                int sor = 2;
                double NagySum = 0;
                if (AdatokOsztály != null)
                {
                    foreach (Adat_Épület_Takarításosztály rekord in AdatokOsztály)
                    {

                        sor += 1;
                        MyE.Egyesít(munkalap, $"b{sor}:p{sor}");
                        MyE.Igazít_vízszintes($"b{sor}:p{sor}", "bal");
                        MyE.Háttérszín($"b{sor}:p{sor}", 13434828L);
                        MyE.Kiir(rekord.Osztály.Trim(), $"b{sor}");
                        MyE.Sormagasság($"{sor}:{sor}", 20);

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
                                MyE.Kiir(rekord1.Osztály.Trim(), $"A{sor}");
                                MyE.Kiir(rekord1.Megnevezés.Trim(), $"b{sor}");
                                MyE.Kiir(rekord1.Méret.ToString().Replace(",", "."), $"c{sor}");
                                MyE.Kiir("E1", $"d{sor}");
                                MyE.Kiir("E2", $"f{sor}");
                                MyE.Kiir("E3", $"h{sor}");
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
                                MyE.Kiir(idE1db.ToString(), $"e{sor}");
                                MyE.Kiir(idE2db.ToString(), $"g{sor}");
                                MyE.Kiir(idE3db.ToString(), $"i{sor}");

                                MyE.Kiir(rekord.E1Ft.ToString().Replace(",", "."), $"j{sor}"); MyE.Betű($"j{sor}", "", "#,###.## $");
                                MyE.Kiir((rekord.E2Ft * rekord1.Méret).ToString().Replace(",", "."), $"k{sor}"); MyE.Betű($"k{sor}", "", "#,###.## $");
                                MyE.Kiir((rekord.E3Ft * rekord1.Méret).ToString().Replace(",", "."), $"l{sor}"); MyE.Betű($"l{sor}", "", "#,###.## $");

                                MyE.Kiir("=RC[-3]*RC[-8]", $"m{sor}"); MyE.Betű($"M{sor}", "", "#,###.## $");
                                MyE.Kiir("=RC[-3]*RC[-7]", $"n{sor}"); MyE.Betű($"N{sor}", "", "#,###.## $");
                                MyE.Kiir("=RC[-3]*RC[-6]", $"o{sor}"); MyE.Betű($"O{sor}", "", "#,###.## $");
                                MyE.Kiir("=SUM(RC[-3]:RC[-1])", $"p{sor}"); MyE.Betű($"P{sor}", "", "#,###.## $");

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
                            MyE.Kiir($"{rekord.Osztály} Összesen", $"B{sor}");
                            MyE.Betű($"B{sor}", false, false, true);

                            MyE.Kiir(E1SzumMennyi.ToString().Replace(",", "."), $"E{sor}");
                            MyE.Betű($"E{sor}", false, false, true);

                            MyE.Kiir(E2SzumMennyi.ToString().Replace(",", "."), $"G{sor}");
                            MyE.Betű($"G{sor}", false, false, true);

                            MyE.Kiir(E3SzumMennyi.ToString().Replace(",", "."), $"I{sor}");
                            MyE.Betű($"I{sor}", false, false, true);

                            MyE.Kiir(E1Szum.ToString().Replace(",", "."), $"m{sor}");
                            MyE.Betű($"M{sor}", "", "#,###.## $");
                            MyE.Betű($"M{sor}", false, false, true);

                            MyE.Kiir(E2Szum.ToString().Replace(",", "."), $"n{sor}");
                            MyE.Betű($"N{sor}", "", "#,###.## $");
                            MyE.Betű($"N{sor}", false, false, true);

                            MyE.Kiir(E3Szum.ToString().Replace(",", "."), $"o{sor}");
                            MyE.Betű($"O{sor}", "", "#,###.## $");
                            MyE.Betű($"O{sor}", false, false, true);

                            MyE.Kiir((E1Szum + E2Szum + E3Szum).ToString().Replace(",", "."), $"p{sor}");
                            MyE.Betű($"P{sor}", "", "#,###.## $");
                            MyE.Betű($"P{sor}", false, false, true);

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
                MyE.Igazít_vízszintes($"b{sor}:p{sor}", "bal");
                MyE.Háttérszín($"b{sor}:p{sor}", 13434828L);
                MyE.Egyesít(munkalap, $"b{sor}:o{sor}");
                MyE.Kiir(Telephely + " Összesen/hó", $"b{sor}:o{sor}");

                MyE.Betű($"b{sor}:o{sor}", false, false, true);
                MyE.Egyesít(munkalap, $"b{sor}:o{sor}");
                MyE.Kiir(NagySum.ToString().Replace(",", "."), $"p{sor}");
                MyE.Betű($"P{sor}", "", "#,###.## $");
                MyE.Betű($"P{sor}", false, false, true);

                MyE.Rácsoz($"b1:p{sor}");
                MyE.Sormagasság($"{sor}:{sor}", 25);

                MyE.OszlopRejtés(munkalap, "A:A");
                // bezárjuk az Excel-t
                MyE.Aktív_Cella(munkalap, "A1");

                MyE.NyomtatásiTerület_részletes(munkalap, $"A1:P{sor}", "$1:$1", "", false);

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
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Takarítás\Opcionális.mdb";
                string jelszó = "seprűéslapát";
                string szöveg = "SELECT * FROM TakarításOpcionális ORDER BY ID";
                AdatokTakOpció = KézOpció.Lista_Adatok(hely, jelszó, szöveg);
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
                string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Épület\Opcionális{Dátum.Year}.mdb";
                string jelszó = "seprűéslapát";
                string szöveg = "SELECT * FROM TakarításOpcTelepAdatok";
                AdatokTakTelepOpció = KézTelep.Lista_Adatok(hely, jelszó, szöveg);
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


            MyE.ExcelLétrehozás();
            //Meglévőt átnevezzük 
            MyE.Munkalap_átnevezés("Munka1", "Melléklet");


            AdatokTIG.Clear();
            MellékletElkészítés();
            TIGElkészítés("TIG", "Jármű takarítási");

            // az excel tábla bezárása
            MyE.ExcelMentés(fájlexc);
            MyE.ExcelBezárás();
            MyE.Megnyitás(fájlexc);
        }

        private void AláíróListaFeltöltés()
        {
            Kezelő_Kiegészítő_főkönyvtábla KézFőkönyv = new Kezelő_Kiegészítő_főkönyvtábla();
            string hely = $@"{Application.StartupPath}\{Telephely}\adatok\segéd\kiegészítő.mdb";
            string jelszó = "Mocó";
            string szöveg = "SELECT * FROM főkönyvtábla ";
            List<Adat_Kiegészítő_főkönyvtábla> AdatokFőkönyv = KézFőkönyv.Lista_Adatok(hely, jelszó, szöveg);
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
                MyE.Munkalap_aktív(munkalap);
                MyE.Egyesít(munkalap, "a1:d1");

                int sor = 1;
                MyE.Oszlopszélesség(munkalap, "A:A", 25);
                MyE.Oszlopszélesség(munkalap, "B:B", 35);
                MyE.Oszlopszélesség(munkalap, "C:C", 25);
                MyE.Oszlopszélesség(munkalap, "D:D", 25);
                MyE.Oszlopszélesség(munkalap, "E:E", 25);
                MyE.Oszlopszélesség(munkalap, "F:F", 25);

                int eleje = sor;
                DateTime hónapelsőnapja = MyF.Hónap_elsőnapja(Dátum);
                DateTime hónaputolsónapja = MyF.Hónap_utolsónapja(Dátum);
                Típusok_Gyűjtése(hónapelsőnapja, hónaputolsónapja);

                foreach (string Típus in TIGTípusok)
                {

                    TigMellékletFejléc(sor);
                    J1_Adatok_Melléklet(++sor, 1, "Nappal", Típus, hónapelsőnapja, hónaputolsónapja);
                    J1_Adatok_Melléklet(++sor, 2, "Éjszaka", Típus, hónapelsőnapja, hónaputolsónapja);

                    for (int ii = 0; ii <= Lekérdezés_Kategória.Count - 1; ii++)
                    {
                        MyE.Kiir(Telephely, $"A{++sor}");
                        Többi_Adatok_Melléklet(Lekérdezés_Kategória[ii], sor, 1, "Nappal", Típus, hónapelsőnapja, hónaputolsónapja);
                        MyE.Kiir(Telephely, $"A{++sor}");
                        Többi_Adatok_Melléklet(Lekérdezés_Kategória[ii], sor, 2, "Éjszaka", Típus, hónapelsőnapja, hónaputolsónapja);
                    }
                    MyE.Rácsoz($"A{eleje}:F{sor}");
                    sor++;
                    MyE.Kiir("=SUM(R[-18]C[-1]:R[-1]C[-1])", $"F{sor}");
                    MyE.Kiir($"{Típus} Összesen", $"E{sor}");
                    MyE.Rácsoz($"A{sor}:F{sor}");

                    sor += 2;
                    eleje = sor;
                }
                MyE.Kiir($"=SUM(R[-{sor - 1}]C:R[-2]C)", $"F{sor}");
                MyE.Betű($"F{sor}", "", "#,##0 $");
                MyE.Kiir($"Végösszesen", $"E{sor}");
                MyE.Rácsoz($"A{sor}:F{sor}");

                MyE.NyomtatásiTerület_részletes(munkalap, $"A1:F{sor}", "", "",
                    $"{Telephely} Járműtakarítás", $"{Dátum.Year}.{Dátum.Month}. Hónap", "&P/&N",
                    "", "", "", "",
                    0.393700787401575d, 0.393700787401575,
                    0.984251968503937, 0.984251968503937,
                    0.511811023622047d, 0.511811023622047d,
                    false, false,
                    "1", "", false, "A4");


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

        private void J1_Adatok_Melléklet(int sor, int napszak, string ÉjNAP, string típus, DateTime hónapelsőnapja, DateTime hónaputolsónapja)
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

            MyE.Kiir(Telephely, $"A{sor}");
            MyE.Kiir($"{típus} J1 {ÉjNAP}", $"B{sor}");
            MyE.Kiir(Mennyiség.ToString(), $"C{sor}");
            double Ár = 0;
            if (RészAdatokÁr != null)
            {
                MyE.Kiir(RészAdatokÁr.Ár.ToString(), $"D{sor}");
                Ár = RészAdatokÁr.Ár;
            }
            MyE.Kiir("=RC[-2]*RC[-1]", $"E{sor}");
            MyE.Betű($"E{sor}", "", "#,##0 $");
            Adat_ÉpJár_Takarítás_TIG Elemek = new Adat_ÉpJár_Takarítás_TIG(Telephely, $"{típus} J1 {ÉjNAP}", Mennyiség, ME, Ár, Mennyiség * Ár);
            if (Mennyiség * Ár != 0) AdatokTIG.Add(Elemek);
        }

        private void Többi_Adatok_Melléklet(string takarításfajta, int sor, int napszak, string ÉjNAP, string típus, DateTime hónapelsőnapja, DateTime hónaputolsónapja)
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

            MyE.Kiir(Telephely, $"A{sor}");
            MyE.Kiir($"{típus} {takarításfajta} {ÉjNAP}", $"B{sor}");
            MyE.Kiir(Mennyiség.ToString().Replace(',', '.'), $"C{sor}");
            double Ár = 0;
            if (RészAdatokÁr != null)
            {
                MyE.Kiir(RészAdatokÁr.Ár.ToString(), $"D{sor}");
                Ár = RészAdatokÁr.Ár;
            }
            else
            {
                MyE.Kiir("0", $"D{sor}");
            }
            MyE.Kiir("=RC[-2]*RC[-1]", $"E{sor}");
            MyE.Betű($"E{sor}", "", "#,##0 $");
            Adat_ÉpJár_Takarítás_TIG Elemek = new Adat_ÉpJár_Takarítás_TIG(Telephely, $"{típus} {takarításfajta} {ÉjNAP}", Mennyiség, ME, Ár, Mennyiség * Ár);
            if (Mennyiség * Ár != 0) AdatokTIG.Add(Elemek);
        }

        private void TigMellékletFejléc(int sor)
        {
            MyE.Sortörésseltöbbsorba($"A{sor}:F{sor}");
            MyE.Kiir("Telephely", $"A{sor}");
            MyE.Kiir("Tevékenység\nJárműtípus, takarítási fokozat", $"B{sor}");
            MyE.Kiir("Tarakítási mennyiségek\n(db/hó)", $"C{sor}");
            MyE.Kiir("Egységár\nFt/db", $"D{sor}");
            MyE.Kiir("Összesen\n(Ft/hó)", $"E{sor}");
            MyE.Kiir("Típus Összesen", $"F{sor}");
            MyE.Rácsoz($"A{sor}:F{sor}");
            MyE.Vastagkeret($"A{sor}:F{sor}");
        }
        #endregion


        #region Közös
        private void TIGElkészítés(string munkalap, string munka)
        {
            try
            {
                MyE.Új_munkalap(munkalap);
                MyE.Munkalap_aktív(munkalap);
                MyE.Munkalap_betű("Garamond", 12);
                MyE.Egyesít(munkalap, "a1:d1");

                MyE.Oszlopszélesség(munkalap, "B:B", 10);
                MyE.Oszlopszélesség(munkalap, "C:C", 10);
                MyE.Oszlopszélesség(munkalap, "D:D", 15);
                MyE.Oszlopszélesség(munkalap, "E:E", 13);
                MyE.Oszlopszélesség(munkalap, "F:F", 8);
                MyE.Oszlopszélesség(munkalap, "G:G", 13);
                MyE.Oszlopszélesség(munkalap, "H:H", 13);
                int sor = 1;
                MyE.Egyesít(munkalap, $"D{sor}:H{sor}");
                MyE.Kiir("B+N Referencia  Szerződés száma: BKV Zrt. T-50/20", $"D{sor}");
                MyE.Igazít_vízszintes($"D{sor}", "jobb");

                sor += 2;
                MyE.Egyesít(munkalap, $"A{sor}:H{sor}");
                MyE.Kiir("TELJESÍTÉSIGAZOLÁS", $"A{sor}");
                MyE.Betű($"A{sor}", 14);
                MyE.Betű($"A{sor}", false, false, true);

                sor += 2;
                MyE.Egyesít(munkalap, $"A{sor}:H{sor}");
                MyE.Kiir($"A Villamos Üzemeltetési Igazgatóság, {Telephely} Járműfenntartó Üzemben végzett {munka} munkákról.", $"A{sor}");
                MyE.Sormagasság($"A{sor}", 35);
                MyE.Sortörésseltöbbsorba_egyesített($"A{sor}");
                MyE.Betű($"A{sor}", 14);
                MyE.Betű($"A{sor}", false, false, true);

                sor = Megrendelő(munkalap, sor);
                sor = Vállalkozó(munkalap, sor);

                sor += 2;
                MyE.Egyesít(munkalap, $"A{sor}:H{sor}");
                string BMRszám = KeresBMR();
                string irateleje = "Felek rögzítik, hogy 2022.04.29. napján T-50/20. számon „BKV Zrt. járműveinek, telephelyeinek és létesítményeinek takarítása” " +
                    "tárgyban vállalkozási megbízási szerződést (a továbbiakban: Szerződés) kötöttek. \r\nFelek rögzítik, hogy Szerződéshez kapcsolódó,  BMR ";
                string iratvége = " számú megrendelésben(a továbbiakban: Megrendelés) foglaltakat a Vállalkozó a következők szerint végezte el:";
                MyE.Kiir($"{irateleje}-{BMRszám}-{iratvége}", $"A{sor}");
                MyE.Cella_Betű($"A{sor}", false, false, true, irateleje.Length, BMRszám.Length + 2);
                MyE.Sormagasság($"{sor}:{sor}", 70);
                MyE.Igazít_vízszintes($"A{sor}", "bal");

                sor += 2;
                sor = TáblázatTIGhez(sor, out double Nettó);

                sor += 2;
                MyE.Egyesít(munkalap, $"A{sor}:H{sor}");
                MyE.Kiir($"A teljesítés ideje: {Dátum:yyyy. MMMM} hó", $"A{sor}");
                MyE.Betű($"A{sor}", false, false, true);
                MyE.Igazít_vízszintes($"A{sor}", "bal");

                sor += 2;
                MyE.Egyesít(munkalap, $"A{sor}:H{sor}");
                irateleje = "A Megrendelésben foglalt feladatok ellenértéke összesen nettó ";
                string iratközepe = Math.Round(Nettó, 0).ToStrTrim();
                iratvége = " Ft.+ ÁFA, azaz ";
                string irateleje1 = MyF.Számszóban((long)Math.Round(Nettó, 0));
                string iratvége1 = "+Áfa";
                string irat = irateleje + iratközepe + iratvége + irateleje1 + iratvége1;

                MyE.Kiir($"{irat}", $"A{sor}");
                MyE.Cella_Betű($"A{sor}", false, false, true, irateleje.Length, iratközepe.Length + 2);
                MyE.Cella_Betű($"A{sor}", false, false, true, irateleje.Length + iratközepe.Length + iratvége.Length, irateleje1.Length + 2);
                MyE.Igazít_vízszintes($"A{sor}", "bal");
                MyE.Sormagasság($"{sor}:{sor}", 32);


                if (munkalap == "TIG")
                {
                    sor++;
                    MyE.Egyesít(munkalap, $"A{sor}:H{sor}");
                    MyE.Kiir($"Jelen Teljesítési Igazolás 1. számú mellékletét képezi a Vállalkozó jelen Teljesítés Igazolás kiállítását megalapozó lejelentése.", $"A{sor}");
                    MyE.Igazít_vízszintes($"A{sor}", "bal");
                }

                sor++;
                MyE.Egyesít(munkalap, $"A{sor}:H{sor}");
                MyE.Kiir($"A teljesítés a Szerződésben és a Megrendelésben meghatározottak szerint történt.", $"A{sor}");
                MyE.Igazít_vízszintes($"A{sor}", "bal");


                sor = Aláírások(munkalap, sor);
                MyE.Oszlopszélesség(munkalap, "A:A");

                MyE.NyomtatásiTerület_részletes(munkalap, $"A1:H{sor}", "", "",
                          $"", $"", "",
                          "", "", "", "",
                          0.393700787401575d, 0.393700787401575,
                          0.984251968503937, 0.393700787401575,
                          0.511811023622047d, 0.511811023622047d,
                          false, false,
                          "1", "", true, "A4");

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

        private int Aláírások(string munkalap, int sor)
        {
            sor += 2;
            MyE.Kiir($"Budapest,{DateTime.Today:yyyy. MMMM dd}", $"A{sor}");
            MyE.Egyesít(munkalap, $"F{sor}:H{sor}");
            MyE.Kiir($"Budapest,{DateTime.Today:yyyy. MMMM dd}", $"F{sor}");

            sor += 3;
            MyE.Egyesít(munkalap, $"A{sor}:B{sor}");
            MyE.Egyesít(munkalap, $"F{sor}:H{sor}");


            sor++;
            MyE.Egyesít(munkalap, $"A{sor}:B{sor}");
            MyE.Egyesít(munkalap, $"F{sor}:H{sor}");
            MyE.Kiir(AláíróNév, $"A{sor}");
            MyE.Kiir($"Mong Péter", $"F{sor}");
            MyE.Aláírásvonal($"A{sor}:B{sor}");
            MyE.Aláírásvonal($"F{sor}:H{sor}");

            sor++;
            MyE.Egyesít(munkalap, $"A{sor}:B{sor}");
            MyE.Egyesít(munkalap, $"F{sor}:H{sor}");
            MyE.Kiir(AláíróBeosztás, $"A{sor}");
            MyE.Kiir($"B+N Referencia Zrt.", $"F{sor}");

            sor++;
            MyE.Egyesít(munkalap, $"A{sor}:B{sor}");
            MyE.Egyesít(munkalap, $"F{sor}:H{sor}");
            MyE.Kiir($"BKV ZRT.", $"A{sor}");
            MyE.Kiir($"3644 Tardona, Katus domb 1", $"F{sor}");

            sor++;
            MyE.Egyesít(munkalap, $"A{sor}:B{sor}");
            MyE.Egyesít(munkalap, $"F{sor}:H{sor}");
            MyE.Kiir($"", $"A{sor}");
            MyE.Kiir($"Adószám: 23480874-2-05", $"F{sor}");

            sor += 4;
            MyE.Kiir($"Szállítóiminősítés:", $"A{sor}");
            MyE.Betű($"A{sor}", true, false, false);

            sor++;
            MyE.Kiir($"Szolgáltatás minősége: ", $"A{sor}");
            MyE.Kiir($"%", $"B{sor}");
            MyE.Igazít_vízszintes($"B{sor}", "jobb");
            MyE.Aláírásvonal($"B{++sor}");


            sor++;
            MyE.Kiir($"Szolgáltatás határideje:", $"A{sor}");
            MyE.Kiir($"%", $"B{sor}");
            MyE.Igazít_vízszintes($"B{sor}", "jobb");
            MyE.Aláírásvonal($"B{++sor}");
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

        private string KeresBMR()
        {
            string válasz = "-Nincs megadva BMR szám-";
            try
            {
                Adat_Takarítás_BMR Elem = (from a in AdatokBMR
                                           where a.JárműÉpület == "Épület" &&
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
            MyE.Kiir("Felek adatai:", $"A{sor}");

            sor++;
            MyE.Egyesít(munkalap, $"A{sor}:H{sor}");
            MyE.Kiir("Budapesti Közlekedési Zártkörűen Működő Részvénytársaság", $"A{sor}");
            MyE.Betű($"B{sor}", 14);
            MyE.Betű($"B{sor}", false, false, true);
            MyE.Igazít_vízszintes($"B{sor}", "bal");

            sor++;
            MyE.Egyesít(munkalap, $"B{sor}:C{sor}");
            MyE.Egyesít(munkalap, $"D{sor}:H{sor}");
            MyE.Kiir("Székhely:", $"B{sor}");
            MyE.Kiir("1980 Budapest, Akácfa utca 15.", $"D{sor}");
            MyE.Igazít_vízszintes($"B{sor}", "bal");
            MyE.Igazít_vízszintes($"D{sor}", "bal");

            sor++;
            MyE.Egyesít(munkalap, $"B{sor}:C{sor}");
            MyE.Egyesít(munkalap, $"D{sor}:H{sor}");
            MyE.Kiir("Cégjegyzékszám:", $"B{sor}");
            MyE.Kiir("01-10-043037", $"D{sor}");
            MyE.Igazít_vízszintes($"B{sor}", "bal");
            MyE.Igazít_vízszintes($"D{sor}", "bal");

            sor++;
            MyE.Egyesít(munkalap, $"B{sor}:C{sor}");
            MyE.Egyesít(munkalap, $"D{sor}:H{sor}");
            MyE.Kiir("Adószám:", $"B{sor}");
            MyE.Kiir("12154481-4-44", $"D{sor}");
            MyE.Igazít_vízszintes($"B{sor}", "bal");
            MyE.Igazít_vízszintes($"D{sor}", "bal");

            sor++;
            MyE.Egyesít(munkalap, $"B{sor}:C{sor}");
            MyE.Egyesít(munkalap, $"D{sor}:H{sor}");
            MyE.Kiir("Csoport azonosító:", $"B{sor}");
            MyE.Kiir("17781372-5-44", $"D{sor}");
            MyE.Igazít_vízszintes($"B{sor}", "bal");
            MyE.Igazít_vízszintes($"D{sor}", "bal");

            sor++;
            MyE.Egyesít(munkalap, $"B{sor}:H{sor}");
            MyE.Kiir("Mint megrendelő (a továbbiakban: BKV Zrt.)", $"B{sor}");
            MyE.Igazít_vízszintes($"B{sor}", "bal");
            return sor;
        }

        private int Vállalkozó(string munkalap, int sor)
        {
            sor += 2;
            MyE.Kiir("Valamint", $"A{sor}");

            sor++;
            MyE.Egyesít(munkalap, $"A{sor}:H{sor}");
            MyE.Kiir("B+N Referencia Ipari, Kereskedelmi és Szolgáltató Zártkörűen Működő Részvénytársaság", $"A{sor}");
            MyE.Betű($"B{sor}", 14);
            MyE.Betű($"B{sor}", false, false, true);
            MyE.Igazít_vízszintes($"B{sor}", "bal");

            sor++;
            MyE.Egyesít(munkalap, $"B{sor}:C{sor}");
            MyE.Egyesít(munkalap, $"D{sor}:H{sor}");
            MyE.Kiir("Székhely:", $"B{sor}");
            MyE.Kiir("3644 Tardona, Katus domb 1.", $"D{sor}");
            MyE.Igazít_vízszintes($"B{sor}", "bal");
            MyE.Igazít_vízszintes($"D{sor}", "bal");

            sor++;
            MyE.Egyesít(munkalap, $"B{sor}:C{sor}");
            MyE.Egyesít(munkalap, $"D{sor}:H{sor}");
            MyE.Kiir("Cégjegyzékszám:", $"B{sor}");
            MyE.Kiir("05-10-000479 ", $"D{sor}");
            MyE.Igazít_vízszintes($"B{sor}", "bal");
            MyE.Igazít_vízszintes($"D{sor}", "bal");

            sor++;
            MyE.Egyesít(munkalap, $"B{sor}:C{sor}");
            MyE.Egyesít(munkalap, $"D{sor}:H{sor}");
            MyE.Kiir("Adószám:", $"B{sor}");
            MyE.Kiir("23480874-2-05", $"D{sor}");
            MyE.Igazít_vízszintes($"B{sor}", "bal");
            MyE.Igazít_vízszintes($"D{sor}", "bal");

            sor++;
            MyE.Egyesít(munkalap, $"B{sor}:C{sor}");
            MyE.Egyesít(munkalap, $"D{sor}:H{sor}");
            MyE.Kiir("Csoport azonosító:", $"B{sor}");
            MyE.Kiir("12001008-01705737-01700004", $"D{sor}");
            MyE.Igazít_vízszintes($"B{sor}", "bal");
            MyE.Igazít_vízszintes($"D{sor}", "bal");

            sor++;
            MyE.Egyesít(munkalap, $"B{sor}:H{sor}");
            MyE.Kiir("Mint vállalkozó (a továbbiakban: Vállalkozó)", $"B{sor}");
            MyE.Igazít_vízszintes($"B{sor}", "bal");
            return sor;
        }

        private int TáblázatTIGhez(int sor, out double Összesen)
        {
            int eleje = sor;
            MyE.Kiir("Leírás", $"A{sor}");
            MyE.Kiir("Mennyiség", $"B{sor}");
            MyE.Kiir("Mennyiség\r\nEgység", $"C{sor}");
            MyE.Kiir("Egység ár [Ft]", $"D{sor}");
            MyE.Kiir("Nettó ár [Ft]", $"E{sor}");
            MyE.Kiir("Áfa [%]", $"F{sor}");
            MyE.Kiir("Áfa [Ft]", $"G{sor}");
            MyE.Kiir("Bruttó Ár [Ft]", $"H{sor}");

            Összesen = 0;

            foreach (Adat_ÉpJár_Takarítás_TIG Elem in AdatokTIG)
            {
                sor++;
                MyE.Kiir($"{Elem.Telephely} {Elem.Tevékenység}", $"A{sor}");
                MyE.Kiir(Elem.Mennyiség.ToString().Replace(",", "."), $"B{sor}");
                MyE.Kiir(Elem.ME, $"C{sor}");
                MyE.Kiir(Elem.Egységár.ToString().Replace(",", "."), $"D{sor}");
                MyE.Betű($"D{sor}", "", "#,###.##");
                MyE.Kiir(Elem.Összesen.ToString().Replace(",", "."), $"E{sor}");
                MyE.Betű($"E{sor}", "", "#,###.##");
                Összesen += Elem.Összesen;

                MyE.Betű($"E{sor}", "", "#,##0");
                MyE.Kiir("27", $"F{sor}");
                MyE.Kiir("=RC[-1]*RC[-2]/100", $"G{sor}");
                MyE.Betű($"G{sor}", "", "#,###.##");
                MyE.Kiir("=RC[-1]+RC[-3]", $"H{sor}");
                MyE.Betű($"H{sor}", "", "#,###.##");
            }
            MyE.Rácsoz($"A{eleje}:H{sor}");
            MyE.Vastagkeret($"A{eleje}:H{sor}");
            MyE.Vastagkeret($"A{eleje}:H{eleje}");
            sor++;
            MyE.Kiir("Összesen:", $"A{sor}");
            MyE.Betű($"A{sor}", false, false, true);
            MyE.Kiir($"=SUM(R[-{sor - eleje}]C:R[-1]C)", $"E{sor}");
            MyE.Betű($"E{sor}", "", "#,###");
            MyE.Betű($"E{sor}", false, false, true);
            MyE.Kiir($"=SUM(R[-{sor - eleje}]C:R[-1]C)", $"G{sor}");
            MyE.Betű($"G{sor}", "", "#,###.##");
            MyE.Betű($"G{sor}", false, false, true);
            MyE.Kiir($"=SUM(R[-{sor - eleje}]C:R[-1]C)", $"H{sor}");
            MyE.Betű($"H{sor}", "", "#,###.##");
            MyE.Betű($"H{sor}", false, false, true);
            MyE.Rácsoz($"A{sor}:H{sor}");
            MyE.Vastagkeret($"A{sor}:H{sor}");

            return sor;
        }
        #endregion

    }
}
