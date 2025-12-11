using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.V_Ablakok._3_Dolgozó.DolgozoiLekerdezesek
{
    public class DolgozoiLekerdezesek_Felepites_Aktualis
    {
        public void ExceltKeszit(string fájlexc, string[] Cmbtelephely, Kezelő_Dolgozó_Alap KézDolg, Kezelő_Kiegészítő_Csoportbeosztás KézCsop)
        {
            Beállítás_Betű BeBetű = new Beállítás_Betű() { Név="Arial", Méret=12};

            MyX.ExcelLétrehozás();
            string munkalap = "Összesítő";
            MyX.Munkalap_átnevezés("Munka1", munkalap);

            MyX.Munkalap_betű(munkalap, BeBetű);

            // ****************************************************
            // elkészítjük a lapokat
            // ****************************************************

            for (int i = 0; i < Cmbtelephely.Length; i++)
                MyX.Munkalap_Új(Cmbtelephely[i]);

            int[] feorössz = new int[10];
            int sor;
            int oszlop;
            var utolsósor = default(int);
            int darab;

            // elkészítjük az egyes telephelyeket
            for (int i = 0; i < Cmbtelephely.Length; i++)
            {
                string telep = Cmbtelephely[i];
                utolsósor = 0;
                for (int j = 1; j < 10; j++)
                    feorössz[j] = 0;
                //Főholtart.Lép();
                munkalap = telep;
                MyX.Munkalap_aktív(munkalap);

                List<Adat_Kiegészítő_Csoportbeosztás> Csoport = KézCsop.Lista_Adatok(telep);
                sor = 1;
                oszlop = -3;

                //Alholtart.Be(Csoport.Count + 2);

                foreach (Adat_Kiegészítő_Csoportbeosztás rekordvált in Csoport)
                {
                    //Alholtart.Lép();
                    oszlop += 4;

                    List<Adat_Dolgozó_Alap> AdatDolg = KézDolg.Lista_Adatok(telep);
                    AdatDolg = AdatDolg.Where(a => a.Csoport == rekordvált.Csoportbeosztás.Trim() && a.Kilépésiidő == new DateTime(1900, 1, 1)).ToList();
                    // elkészítjük a fejlécet
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(oszlop) + sor + ":" + MyF.Oszlopnév(oszlop + 3) + sor);
                    MyX.Kiir(rekordvált.Csoportbeosztás.Trim(), MyF.Oszlopnév(oszlop) + sor);

                    sor += 1;
                    MyX.Kiir("Ssz.", MyF.Oszlopnév(oszlop) + sor);
                    MyX.Kiir("Név", MyF.Oszlopnév(oszlop + 1) + sor);
                    MyX.Kiir("Feor", MyF.Oszlopnév(oszlop + 2) + sor);
                    MyX.Kiir("Munkakör", MyF.Oszlopnév(oszlop + 3) + sor);


                    foreach (Adat_Dolgozó_Alap rekord in AdatDolg)
                    {
                        sor += 1;
                        MyX.Kiir($"#SZÁME#{(sor - 2)}", MyF.Oszlopnév(oszlop) + sor);
                        MyX.Kiir(rekord.DolgozóNév.Trim(), MyF.Oszlopnév(oszlop + 1) + sor);
                        MyX.Kiir($"#SZÁME#{rekord.Feorsz}", MyF.Oszlopnév(oszlop + 2) + sor);
                        MyX.Kiir(rekord.Munkakör.Trim(), MyF.Oszlopnév(oszlop + 3) + sor);

                        if (rekord.Feorsz.Length > 1)
                        {
                            if (int.TryParse(rekord.Feorsz.Substring(0, 1), out int Szám))
                            {
                                if (Szám < 5)
                                    MyX.Háttérszín(munkalap,MyF.Oszlopnév(oszlop) + sor + ":" + MyF.Oszlopnév(oszlop + 3) + sor, Color.FromArgb(10053375));

                                feorössz[Szám]++;
                            }
                        }

                        // passzív színez
                        if (rekord.Passzív)
                            MyX.Háttérszín(munkalap,MyF.Oszlopnév(oszlop) + sor + ":" + MyF.Oszlopnév(oszlop + 3) + sor, Color.FromArgb(9868950));
                    }

                    if (utolsósor < sor)
                        utolsósor = sor;
                    if (sor > 1)
                    {
                        MyX.Vastagkeret(munkalap, MyF.Oszlopnév(oszlop) + "1:" + MyF.Oszlopnév(oszlop + 3) + sor);
                        MyX.Rácsoz(munkalap,MyF.Oszlopnév(oszlop) + "1:" + MyF.Oszlopnév(oszlop + 3) + sor);
                    }
                    MyX.Vastagkeret(munkalap,MyF.Oszlopnév(oszlop) + "1:" + MyF.Oszlopnév(oszlop + 3) + "2");
                    MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(oszlop) + ":" + MyF.Oszlopnév(oszlop + 3));
                    sor = 1;
                }

                // kiírjuk az öszesítő táblákat
                sor = utolsósor + 2;

                MyX.Kiir("Létszám összetétel", "b" + sor);
                MyX.Egyesít(munkalap, "a" + sor + ":a" + sor);
                sor += 2;
                MyX.Kiir("Feor", "b" + sor);
                MyX.Egyesít(munkalap, "a" + sor + ":a" + sor);
                darab = 0;
                for (int j = 1; j < 10; j++)
                {
                    sor += 1;
                    MyX.Kiir("F." + j, "b" + sor);
                    MyX.Kiir($"#SZÁME#{feorössz[j]}", "c" + sor);
                    darab += feorössz[j];
                }
                sor += 1;
                MyX.Kiir("Összesen:", "b" + sor);
                MyX.Kiir($"#SZÁME#{darab}", "c" + sor);
                MyX.Vastagkeret(munkalap, "b" + (sor - 10).ToString() + ":" + "c" + sor);
                MyX.Rácsoz(munkalap,"b" + (sor - 10).ToString() + ":" + "c" + sor);
            }

            //Főholtart.Ki();
            //Alholtart.Ki();
            MyX.Munkalap_aktív(munkalap);
            MyX.Aktív_Cella(munkalap, "A1");
            MyX.ExcelMentés(fájlexc + ".xlsx");
            MyX.ExcelBezárás();
        }
    }
}
