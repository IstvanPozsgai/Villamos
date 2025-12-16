using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;
using Villamos.Adatszerkezet;

namespace Villamos.V_Ablakok._7_Gondnokság.Épület_takarítás
{
    public class Epulettakaritas_Takaritasi_Naplo_Excel
    {
        public void ExceltKeszit(string fájlexc, DateTime Dátum, List<Adat_Épület_Takarításrakijelölt> AdatokKijelöltek, List<Adat_Épület_Adattábla> AdatokAdatTábla, List<Adat_Épület_Naptár> AdatokÉNaptár, string helységkód, int hónapnap)
        {
            Beállítás_Betű BeBetű = new Beállítás_Betű() { Név= "Arial", Méret = 12};
            Beállítás_Betű BeBetűVastag = new Beállítás_Betű() { Név = "Arial", Méret = 12, Vastag=true };
            Beállítás_Betű BeBetűDőlt = new Beállítás_Betű() { Név = "Arial", Méret = 12, Dőlt = true };
            string munkalap = "Munka1";
            // megnyitjuk az excelt
            MyX.ExcelLétrehozás();
            MyX.Munkalap_betű(munkalap, BeBetű);
            MyX.Sormagasság(munkalap,"1:50", 18);

            // oszlop széleségek beállítása
            MyX.Oszlopszélesség(munkalap, "a:n", 5);
            MyX.Oszlopszélesség(munkalap, "a:a", 7);
            MyX.Oszlopszélesség(munkalap, "e:f", 8);
            MyX.Oszlopszélesség(munkalap, "g:g", 10);
            MyX.Oszlopszélesség(munkalap, "j:k", 10);
            MyX.Oszlopszélesség(munkalap, "n:n", 10);
            // '**********************************************
            // '**          fejléc          ******************
            // '**********************************************
            MyX.Egyesít(munkalap, "a1:N1");
            MyX.Kiir(Dátum.ToString("yyyy MMMM"), "a1");
            MyX.Betű(munkalap,"a1", BeBetűVastag);
            MyX.Egyesít(munkalap, "a2:n4");
            MyX.Kiir("helyiség", "a2");

            MyX.Egyesít(munkalap, "a5:n5");
            MyX.Kiir("Takarítási napló", "a5");
            MyX.Betű(munkalap,"a5", BeBetűVastag);
            MyX.Vastagkeret(munkalap,"a7");
            MyX.Egyesít(munkalap, "b7:g7");
            MyX.Vastagkeret(munkalap,"b7:g7");
            MyX.Egyesít(munkalap, "h7:n7");
            MyX.Vastagkeret(munkalap,"h7:n7");
            MyX.Kiir("Szolgáltató tölti ki", "b7");
            MyX.Betű(munkalap,"b7", BeBetűDőlt);
            MyX.Kiir("BKV szervezeti igazolója tölti ki", "h7");
            MyX.Betű(munkalap,"h7", BeBetűDőlt);
            MyX.Sormagasság(munkalap,"8:8", 51);
            MyX.Egyesít(munkalap, "a8:a9");
            MyX.Kiir("Dátum", "a8");
            MyX.Egyesít(munkalap, "b8:d8");
            MyX.Kiir("Szolg. jegyzék kódja", "b8");
            MyX.Sortörésseltöbbsorba(munkalap,"B8", true);

            MyX.Kiir("E1", "b9");
            MyX.Kiir("E2", "c9");
            MyX.Kiir("E3", "d9");
            MyX.Egyesít(munkalap, "e8:f8");
            MyX.Kiir("Takarítás ideje", "e8");
            MyX.Kiir("-tól", "e9");
            MyX.Kiir("-ig", "f9");
            MyX.Egyesít(munkalap, "g8:g9");
            MyX.Kiir("Aláírás", "g8");
            MyX.Egyesít(munkalap, "h8:i8");
            MyX.Kiir("Megfelelő", "h8");
            MyX.Kiir("I", "h9");
            MyX.Kiir("N", "i9");
            MyX.Egyesít(munkalap, "j8:j9");
            MyX.Kiir("Igazolta", "j8");
            MyX.Egyesít(munkalap, "k8:k9");
            MyX.Kiir("Pót. Határ- idő", "k8");
            MyX.Sortörésseltöbbsorba(munkalap, "K8", true);
            MyX.Egyesít(munkalap, "l8:m8");
            MyX.Kiir("Megfelelő", "l8");
            MyX.Kiir("I", "l9");
            MyX.Kiir("N", "m9");
            MyX.Egyesít(munkalap, "n8:n9");
            MyX.Kiir("Igazolta", "n8");
            MyX.Rácsoz(munkalap,"a7:n9");
            MyX.Vastagkeret(munkalap,"a8");
            MyX.Vastagkeret(munkalap,"b8:g9");
            MyX.Vastagkeret(munkalap,"h8:n9");
            int sor = 1;

            Adat_Épület_Takarításrakijelölt rekord = (from a in AdatokKijelöltek
                                                      where a.Hónap == Dátum.Month
                                                      && a.Helységkód == helységkód.Trim()
                                                      select a).FirstOrDefault();

            if (rekord != null)
            {
                // kiirjuk a helység nevét
                string szöveg1 = rekord.Helységkód.Trim() + " - " + rekord.Megnevezés.Trim();

                List<Adat_Épület_Adattábla> AdatokÉ = (from a in AdatokAdatTábla
                                                       where a.Státus == false
                                                       && a.Kapcsolthelység == helységkód.Trim()
                                                       select a).ToList();

                if (AdatokÉ != null)
                {
                    foreach (Adat_Épület_Adattábla rekordép in AdatokÉ)
                        szöveg1 += "; " + rekordép.Helységkód.Trim() + " - " + rekordép.Megnevezés.Trim();
                }

                MyX.Kiir(szöveg1, "a2");
                MyX.Sortörésseltöbbsorba(munkalap,"a2", true);
                MyX.Igazít_vízszintes(munkalap,"a2", "közép");

                sor = 10;

                for (int i = 0; i < hónapnap; i++)
                {
                    if (MyF.Szöveg_Tisztítás(rekord.E1rekijelölt, i, 1) == "0")
                        MyX.Háttérszín(munkalap,"b" + sor.ToString(), Color.FromArgb(12632256));
                    if (MyF.Szöveg_Tisztítás(rekord.E2rekijelölt, i, 1) == "0")
                        MyX.Háttérszín(munkalap,"c" + sor.ToString(), Color.FromArgb(12632256));
                    if (MyF.Szöveg_Tisztítás(rekord.E3rekijelölt, i, 1) == "0")
                        MyX.Háttérszín(munkalap,"d" + sor.ToString(), Color.FromArgb(12632256));
                    sor += 1;
                }
            }

            sor = 10;

            for (int i = 0; i < hónapnap; i++)
            {
                MyX.Kiir((i + 1).ToString(), "a" + sor.ToString());
                sor += 1;
            }
            MyX.Kiir("Össz", "a" + sor.ToString());
            MyX.Betű(munkalap,"a" + sor.ToString(), BeBetűVastag);
            MyX.Rácsoz(munkalap,"a10:n" + sor.ToString());
            MyX.Vastagkeret(munkalap,"b10:g" + sor.ToString());
            MyX.Rácsoz(munkalap, $"B10:G{sor}");
            MyX.Vastagkeret(munkalap,"h10:n" + sor.ToString());
            MyX.Rácsoz(munkalap, $"H10:N{sor}");
            MyX.Vastagkeret(munkalap,"a" + sor.ToString() + ":n" + sor.ToString());
            MyX.Rácsoz(munkalap, $"A{sor}:N{sor}");
            // Szombat vasárnap
            Adat_Épület_Naptár Naptár = (from a in AdatokÉNaptár
                                         where a.Hónap == Dátum.Month
                                         select a).FirstOrDefault();

            if (Naptár != null)
            {
                sor = 10;
                
                for (int i = 0; i < hónapnap; i++)
                {                    
                    if (MyF.Szöveg_Tisztítás(Naptár.Napok, i, 1) == "0")
                    {
                        // ferde vonal
                        Beállítás_Ferde BeállítFerde = new Beállítás_Ferde() { Munkalap = munkalap, Terület = $"B{sor}:N{sor}" };
                        MyX.FerdeVonal(BeállítFerde);
                    }
                    sor += 1;
                }
            }

            sor += 2;
            // jelmagyarázat
            MyX.Kiir("Jelmagyarázat", "a" + sor.ToString());
            sor += 1;
            MyX.Vékonykeret(munkalap,"a" + sor.ToString());
            MyX.Kiir("Megrendelt takarítás", "b" + sor.ToString());
            sor += 1;
            MyX.Vékonykeret(munkalap,"a" + sor.ToString());
            MyX.Háttérszín(munkalap,"a" + sor.ToString(), Color.FromArgb(12632256));
            MyX.Kiir("Nincs megrendelve a takarítás", "b" + sor.ToString());
            sor += 1;
            MyX.Vékonykeret(munkalap,"a" + sor.ToString());
            Beállítás_Ferde BeállítFerde_ = new Beállítás_Ferde() { Munkalap = munkalap, Terület = $"A{sor}" };
            MyX.FerdeVonal(BeállítFerde_);
            MyX.Háttérszín(munkalap,"a" + sor.ToString(), Color.FromArgb(12632256));
            MyX.Kiir("Munkaszüneti nap", "b" + sor.ToString());

            // **********************************************
            // **Nyomtatási beállítások                    **
            // **********************************************
            Beállítás_Nyomtatás NyomtatásBeállít = new Beállítás_Nyomtatás() { Munkalap = munkalap, NyomtatásiTerület = "a1:n" + sor, BalMargó = 10, JobbMargó = 10, AlsóMargó = 15, FelsőMargó = 15, FejlécMéret = 13, LáblécMéret = 13, LapSzéles = 1, LapMagas = 1 };
            MyX.NyomtatásiTerület_részletes(munkalap, NyomtatásBeállít);

            // **********************************************
            // **Nyomtatás                                 **
            // **********************************************
            //if (Option9.Checked) MyX.Nyomtatás(munkalap, 1, 1);

            // bezárjuk az Excel-t
            MyX.Aktív_Cella(munkalap, "A1");
            MyX.ExcelMentés(fájlexc);
            MyX.ExcelBezárás();
        }
    }
}
