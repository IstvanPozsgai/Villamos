using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villamos.Villamos_Adatszerkezet;
using MyX = Villamos.MyClosedXML_Excel;
using MyF = Függvénygyűjtemény;
using System.Drawing;
using Villamos.V_Adatszerkezet;

namespace Villamos.V_Ablakok._7_Gondnokság.Épület_takarítás
{
    public class Epulettakaritas_Takaritasi_Megrendelo_Excel
    {
        public void ExcelKeszit(string fájlexc, string Cmbtelephely, DateTime Dátum, List<Adat_Épület_Takarításrakijelölt> AdatokKijelöltek, List<Adat_Épület_Takarítás_Osztály> AdatokTakOsztály, List<Adat_Épület_Takarítás_Osztály> AdatokAdatTábla )
        {
            Beállítás_Betű BeBetű = new Beállítás_Betű() { Vastag = true };
            Beállítás_Betű BeBetűC = new Beállítás_Betű() { Név="Calibri", Méret=10 };
            string munkalap = "Munka1";
            MyX.ExcelLétrehozás();
            // megnyitjuk az excelt

            //Holtart.Be();
            // *********************************************
            // ********* Osztály tábla *********************
            // *********************************************
            // fejléc elkészítése
            MyX.Kiir("Megnevezés", "a1");
            MyX.Kiir("E1 Egységár [Ft/m2]", "c1");
            MyX.Kiir("E2 Egységár [Ft/m2]", "d1");
            MyX.Kiir("E3 Egységár [Ft/m2]", "e1");

            AdatokTakOsztály = (from a in AdatokTakOsztály
                                where a.Státus == false
                                orderby a.Id
                                select a).ToList();
            //Holtart.Be(20);

            int sor = 2;
            foreach (Adat_Épület_Takarítás_Osztály rekord in AdatokTakOsztály)
            {
                MyX.Kiir(rekord.Osztály.Trim(), "a" + sor.ToString());
                MyX.Kiir(rekord.E1Ft.ToString().Replace(",", "."), "c" + sor.ToString());
                MyX.Kiir(rekord.E2Ft.ToString().Replace(",", "."), "d" + sor.ToString());
                MyX.Kiir(rekord.E3Ft.ToString().Replace(",", "."), "e" + sor.ToString());
                //Holtart.Lép();
                sor += 1;
            }
            MyX.Oszlopszélesség(munkalap, "A:A");
            MyX.Oszlopszélesség(munkalap, "B:B");
            MyX.Oszlopszélesség(munkalap, "C:E");
            MyX.Rácsoz(munkalap,"a1:e" + (sor - 1).ToString());

            MyX.Munkalap_átnevezés("Munka1", "Összesítő");
            MyX.OszlopRejtés("Összesítő", "B:B");

            MyX.Munkalap_Új(Cmbtelephely);
            MyX.Munkalap_betű(munkalap, BeBetűC);
            munkalap = Cmbtelephely;
            // ************************************************
            // ************ fejléc elkészítése  ***************
            // ************************************************
            MyX.Egyesít(munkalap, "b1:b2");
            MyX.Kiir("Helyiség", "b1");
            MyX.Egyesít(munkalap, "c1:c2");
            MyX.Kiir("Alapterület [m2]", "c1");
            MyX.Egyesít(munkalap, "d1:k1");
            MyX.Kiir("Gyakoriság", "d1");
            MyX.Kiir("Szolgálatási jegyzék kódja", "d2");
            MyX.Kiir("Szolgálatási jegyzék kódja", "g2");
            MyX.Kiir("Szolgálatási jegyzék kódja", "j2");
            MyX.Kiir("Gyakoriság alkalom /év", "e2");
            MyX.Kiir("Gyakoriság alkalom /év", "h2");
            MyX.Kiir("Gyakoriság alkalom /hó", "f2");
            MyX.Kiir("Gyakoriság alkalom /hó", "i2");
            MyX.Kiir("Gyakoriság alkalom /hó", "k2");
            MyX.Egyesít(munkalap, "l1:l2");
            MyX.Kiir("E1 Egységár [Ft/alkalom]", "l1");
            MyX.Egyesít(munkalap, "m1:m2");
            MyX.Kiir("E2 Egységár [Ft/alkalom]", "m1");
            MyX.Egyesít(munkalap, "n1:n2");
            MyX.Kiir("E3 Egységár [Ft/alkalom]", "n1");
            MyX.Egyesít(munkalap, "o1:o2");
            MyX.Kiir("E1 Egységár [Ft/hó]", "o1");
            MyX.Egyesít(munkalap, "p1:p2");
            MyX.Kiir("E2 Egységár [Ft/hó]", "p1");
            MyX.Egyesít(munkalap, "q1:q2");
            MyX.Kiir("E3 Egységár [Ft/hó]", "q1");
            MyX.Egyesít(munkalap, "r1:r2");
            MyX.Kiir("Összesen: [Ft/hó]", "r1");
            MyX.Egyesít(munkalap, "s1:t2");
            MyX.Kiir("Feladatellátás tervezett időpontja", "s1");
            MyX.Egyesít(munkalap, "u1:w1");
            MyX.Kiir("Minőségellenőrzésért, teljesítési igazolásért felelős személy", "u1");
            MyX.Kiir("Neve", "u2");
            MyX.Kiir("Telefonszám", "v2");
            MyX.Kiir("E-mail cím", "w2");
            MyX.Sormagasság(munkalap,"1:1", 47);
            MyX.Sormagasság(munkalap,"2:2", 39);
            MyX.Oszlopszélesség(munkalap, "B:B", 46);
            MyX.Oszlopszélesség(munkalap, "c:k", 11);
            MyX.Oszlopszélesség(munkalap, "l:n", 13);
            MyX.Oszlopszélesség(munkalap, "o:v", 15);
            MyX.Oszlopszélesség(munkalap, "w:W", 20);
            MyX.Sortörésseltöbbsorba(munkalap,"c1",true);
            MyX.Sortörésseltöbbsorba(munkalap,"d2:k2");
            MyX.Sortörésseltöbbsorba(munkalap,"l1",true);
            MyX.Sortörésseltöbbsorba(munkalap,"m1", true);
            MyX.Sortörésseltöbbsorba(munkalap,"n1", true);
            MyX.Sortörésseltöbbsorba(munkalap,"o1", true);
            MyX.Sortörésseltöbbsorba(munkalap,"p1", true);
            MyX.Sortörésseltöbbsorba(munkalap,"r1", true);
            MyX.OszlopRejtés(munkalap, "A:A");

            // a táblázat érdemi része

            sor = 2;
            Adat_Épület_Takarításrakijelölt rekordép;

            foreach (Adat_Épület_Takarítás_Osztály rekord in AdatokTakOsztály)
            {
                sor += 1;
                MyX.Egyesít(munkalap, "b" + sor.ToString() + ":W" + sor.ToString());
                MyX.Igazít_vízszintes(munkalap,"b" + sor.ToString() + ":W" + sor.ToString(), "bal");
                MyX.Háttérszín(munkalap,"b" + sor.ToString() + ":W" + sor.ToString(), Color.FromArgb(13434828));
                MyX.Kiir(rekord.Osztály.Trim(), "b" + sor.ToString());
                MyX.Sormagasság(munkalap,sor.ToString() + ":" + sor.ToString(), 20);

                List<Adat_Épület_Adattábla> AdatokA = (from a in AdatokAdatTábla
                                                       where a.Státus == false
                                                       && a.Osztály == rekord.Osztály.Trim()
                                                       orderby a.ID
                                                       select a).ToList();

                foreach (Adat_Épület_Adattábla rekord1 in AdatokA)
                {
                    sor++;
                    MyX.Kiir(rekord1.Osztály.Trim(), "A" + sor.ToString());
                    MyX.Kiir(rekord1.Megnevezés.Trim(), "b" + sor.ToString());
                    MyX.Kiir(rekord1.Méret.ToString(), "c" + sor.ToString());
                    MyX.Kiir("E1", "d" + sor.ToString());
                    MyX.Kiir(rekord1.E1évdb.ToString(), "e" + sor.ToString());
                    MyX.Kiir("E2", "g" + sor.ToString());
                    MyX.Kiir(rekord1.E2évdb.ToString(), "h" + sor.ToString());
                    MyX.Kiir("E3", "j" + sor.ToString());
                    int idE1db = 0;
                    int idE2db = 0;
                    int idE3db = 0;

                    rekordép = (from a in AdatokKijelöltek
                                where a.Hónap == Dátum.Month
                                && a.Helységkód == rekord1.Helységkód.Trim()
                                select a).FirstOrDefault();

                    if (rekordép != null)
                    {
                        idE1db = rekordép.E1kijelöltdb;
                        idE2db = rekordép.E2kijelöltdb;
                        idE3db = rekordép.E3kijelöltdb;
                    }

                    MyX.Kiir(idE1db.ToString(), "f" + sor.ToString());
                    MyX.Kiir(idE2db.ToString(), "i" + sor.ToString());
                    MyX.Kiir(idE3db.ToString(), "k" + sor.ToString());
                    MyX.Kiir(rekord.E1Ft.ToString().Replace(",", "."), "l" + sor.ToString()); //Ez darabra megy
                    MyX.Kiir((rekord.E2Ft * rekord1.Méret).ToString().Replace(",", "."), "m" + sor.ToString());
                    MyX.Kiir((rekord.E3Ft * rekord1.Méret).ToString().Replace(",", "."), "n" + sor.ToString());
                    MyX.Kiir("#KÉPLET#=RC[-3]*RC[-9]", "o" + sor.ToString());
                    MyX.Kiir("#KÉPLET#=RC[-3]*RC[-7]", "p" + sor.ToString());
                    MyX.Kiir("#KÉPLET#=RC[-3]*RC[-6]", "q" + sor.ToString());
                    MyX.Kiir("#KÉPLET#=SUM(RC[-3]:RC[-1])", "r" + sor.ToString());
                    MyX.Kiir(rekord1.Kezd.Trim(), "s" + sor.ToString());
                    MyX.Kiir(rekord1.Végez.Trim(), "t" + sor.ToString());
                    MyX.Kiir(rekord1.Ellenőrneve.Trim(), "u" + sor.ToString());
                    MyX.Kiir(rekord1.Ellenőrtelefonszám.Trim(), "v" + sor.ToString());
                    MyX.Kiir(rekord1.Ellenőremail.Trim(), "w" + sor.ToString());
                }
                //Holtart.Lép();
            }


            // összesítő sor
            sor += 1;
            MyX.Igazít_vízszintes(munkalap, "b" + sor.ToString() + ":W" + sor.ToString(), "bal");
            MyX.Háttérszín(munkalap,"b" + sor.ToString() + ":W" + sor.ToString(), Color.FromArgb(13434828));
            MyX.Egyesít(munkalap, "b" + sor.ToString() + ":n" + sor.ToString());
            MyX.Kiir(Cmbtelephely + " Összesen/hó", "b" + sor.ToString() + ":n" + sor.ToString());
            MyX.Betű(munkalap,"b" + sor.ToString() + ":n" + sor.ToString(), BeBetű);
            MyX.Egyesít(munkalap, "b" + sor.ToString() + ":n" + sor.ToString());
            MyX.Egyesít(munkalap, "o" + sor.ToString() + ":r" + sor.ToString());
            MyX.Kiir("#KÉPLET#=SUM(R[-" + (sor - 3).ToString() + "]C[3]:R[-1]C[3])", "o" + sor.ToString() + ":r" + sor.ToString());
            MyX.Rácsoz(munkalap,"b1:W" + sor.ToString());
            MyX.Sormagasság(munkalap,sor.ToString() + ":" + sor.ToString(), 25);

            // bezárjuk az Excel-t
            MyX.Aktív_Cella(munkalap, "A1");
            MyX.ExcelMentés(fájlexc);
            MyX.ExcelBezárás();
        }
    }
}
