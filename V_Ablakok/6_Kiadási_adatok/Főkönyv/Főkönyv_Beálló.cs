using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.Villamos_Nyomtatványok
{
    public class Főkönyv_Beálló
    {
        readonly Kezelő_Főkönyv_Nap Kéz_Főkönyv = new Kezelő_Főkönyv_Nap();

        public void Beálló_kocsik(string fájlexl, string Telephely, DateTime Dátum, string napszak, string papírméret, string papírelrendezés)
        {
            try
            {
                string munkalap = "Munka1";
                MyX.ExcelLétrehozás(munkalap);
                // egész lap betű méret arial 16
                Beállítás_Betű Bebetű = new Beállítás_Betű { Méret = 16 };
                MyX.Munkalap_betű(munkalap, Bebetű);


                // oszlop szélességeket beállítjuk az alapot
                MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(1) + ":" + MyF.Oszlopnév(13), 8);
                MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(2) + ":" + MyF.Oszlopnév(2), 13);
                MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(9) + ":" + MyF.Oszlopnév(9), 13);
                MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(3) + ":" + MyF.Oszlopnév(3), 30);
                MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(4) + ":" + MyF.Oszlopnév(4), 30);
                MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(10) + ":" + MyF.Oszlopnév(10), 30);
                MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(11) + ":" + MyF.Oszlopnév(11), 30);
                MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(7) + ":" + MyF.Oszlopnév(7), 5);
                // sormagasság
                MyX.Sormagasság(munkalap, "1:100", 25);

                // Fejléc elkészítése
                MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + 1.ToString() + ":" + MyF.Oszlopnév(4) + 1.ToString());
                MyX.Egyesít(munkalap, MyF.Oszlopnév(5) + 1.ToString() + ":" + MyF.Oszlopnév(6) + 1.ToString());
                MyX.Kiir("Beálló villamosok", MyF.Oszlopnév(1) + 1.ToString());
                MyX.Kiir(Dátum.ToString("yyyy.MM.dd"), MyF.Oszlopnév(5) + 1.ToString());
                MyX.Egyesít(munkalap, MyF.Oszlopnév(8) + 1.ToString() + ":" + MyF.Oszlopnév(11) + 1.ToString());
                MyX.Egyesít(munkalap, MyF.Oszlopnév(12) + 1.ToString() + ":" + MyF.Oszlopnév(13) + 1.ToString());
                MyX.Kiir("Beálló villamosok", MyF.Oszlopnév(8) + 1.ToString());
                MyX.Kiir(Dátum.ToString("yyyy.MM.dd"), MyF.Oszlopnév(12) + 1.ToString());
                MyX.Kiir("Idő", "a2");
                MyX.Kiir("Idő", "h2");
                MyX.Kiir("Visz.", "b2");
                MyX.Kiir("Visz.", "i2");
                MyX.Kiir("Milyen javításra kérték", "f2");
                MyX.Kiir("Milyen javításra kérték", "m2");
                MyX.Egyesít(munkalap, MyF.Oszlopnév(4) + 2.ToString() + ":" + MyF.Oszlopnév(6) + 2.ToString());
                MyX.Kiir("Pályaszámok", "C2");
                MyX.Egyesít(munkalap, MyF.Oszlopnév(11) + 2.ToString() + ":" + MyF.Oszlopnév(13) + 2.ToString());
                MyX.Kiir("Pályaszámok", "j2");
                // ********************************
                // tartalom kiírása
                // ********************************+

                List<Adat_Főkönyv_Nap> Adatok = Kéz_Főkönyv.Lista_Adatok(Telephely.Trim(), Dátum, napszak.Trim());
                Adatok = (from a in Adatok
                          where a.Viszonylat != "-"
                          orderby a.Tényérkezés, a.Viszonylat, a.Forgalmiszám, a.Azonosító
                          select a).ToList();

                int sor = 3;
                int szerelvényhossz = 0;
                string szöveg1 = "";
                string szöveg = "";

                foreach (Adat_Főkönyv_Nap rekord in Adatok)
                {
                    // ha délelőtt bejön
                    //if (rekord.Tényérkezés.Hour > 7 && rekord.Tényérkezés.Hour < 12 && napszak.Trim() == "de" || rekord.Tényérkezés.Hour > 12 && napszak == "du")
                    if (rekord.Napszak.Trim() == "DE" || rekord.Napszak.Trim() == "DU")
                    {
                        if (rekord.Tényérkezés.Hour > 7 && rekord.Tényérkezés.Hour < 12 && napszak.Trim() == "de" ||
                            ((rekord.Tényérkezés.Hour > 12 && napszak == "du") || (rekord.Tényérkezés.Hour < 6 && napszak == "du")))
                        {

                            // összefűzzük az egy szerelvénybe tartozó kocsikat
                            if (szerelvényhossz == 0)
                            {
                                szöveg = rekord.Azonosító.Trim();
                                if (rekord.Hibaleírása.Trim() != "_" && rekord.Státus == 3)
                                    szöveg1 = rekord.Hibaleírása.Trim();

                                szerelvényhossz = 1;
                            }
                            else
                            {
                                szöveg = szöveg + "-" + rekord.Azonosító.Trim();
                                if (rekord.Hibaleírása.Trim() != "_" && rekord.Státus == 3)
                                    szöveg1 = rekord.Hibaleírása.Trim();


                                szerelvényhossz += 1;
                            }
                            szöveg1 = szöveg1.Trim().Length > 30 ? szöveg1.Substring(0, 30) : szöveg1.Trim();
                            // ha a szerelvény összes kocsija megvan akkor kiírja a tételeket.
                            if (szerelvényhossz == rekord.Kocsikszáma)
                            {
                                MyX.Egyesít(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(6) + $"{sor}");
                                MyX.Egyesít(munkalap, MyF.Oszlopnév(11) + $"{sor}" + ":" + MyF.Oszlopnév(13) + $"{sor}");
                                MyX.Kiir(rekord.Tényérkezés.ToString("HH:mm"), MyF.Oszlopnév(1) + $"{sor}");
                                MyX.Kiir(rekord.Viszonylat.Trim() + "/" + rekord.Forgalmiszám.Trim(), MyF.Oszlopnév(2) + $"{sor}");
                                MyX.Kiir(szöveg, MyF.Oszlopnév(3) + $"{sor}");
                                MyX.Kiir(szöveg1.Trim(), MyF.Oszlopnév(4) + $"{sor}");
                                MyX.Kiir(rekord.Tényérkezés.ToString("HH:mm"), MyF.Oszlopnév(8) + $"{sor}");
                                MyX.Kiir(rekord.Viszonylat.Trim() + "/" + rekord.Forgalmiszám.Trim(), MyF.Oszlopnév(9) + $"{sor}");
                                MyX.Kiir(szöveg, MyF.Oszlopnév(10) + $"{sor}");
                                MyX.Kiir(szöveg1.Trim(), MyF.Oszlopnév(11) + $"{sor}");
                                sor += 1;
                                szerelvényhossz = 0;
                                szöveg = "";
                                szöveg1 = "";
                            }
                        }
                    }
                }


                // közép fejléc
                for (int i = 1; i <= 3; i++)
                {
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(6) + $"{sor}");
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(11) + $"{sor}" + ":" + MyF.Oszlopnév(13) + $"{sor}");
                    sor++;
                }

                MyX.Egyesít(munkalap, MyF.Oszlopnév(2) + $"{sor}" + ":" + MyF.Oszlopnév(3) + $"{sor}");
                MyX.Egyesít(munkalap, MyF.Oszlopnév(9) + $"{sor}" + ":" + MyF.Oszlopnév(10) + $"{sor}");
                MyX.Kiir("Vizsgálatra marad", MyF.Oszlopnév(2) + $"{sor}");
                MyX.Kiir("Vizsgálatra marad", MyF.Oszlopnév(9) + $"{sor}");
                MyX.Kiir("Vág.", MyF.Oszlopnév(1) + $"{sor}");
                MyX.Kiir("Vág.", MyF.Oszlopnév(8) + $"{sor}");
                MyX.Kiir("Vág.", MyF.Oszlopnév(5) + $"{sor}");
                MyX.Kiir("Vág.", MyF.Oszlopnév(12) + $"{sor}");
                MyX.Kiir("Visz.", MyF.Oszlopnév(6) + $"{sor}");
                MyX.Kiir("Visz.", MyF.Oszlopnév(13) + $"{sor}");
                MyX.Kiir("Tartalék", MyF.Oszlopnév(4) + $"{sor}");
                MyX.Kiir("Tartalék", MyF.Oszlopnév(11) + $"{sor}");
                int sorfej = sor;
                for (int i = 1; i <= 9; i++)
                {
                    sor++;
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(2) + $"{sor}" + ":" + MyF.Oszlopnév(3) + $"{sor}");
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(9) + $"{sor}" + ":" + MyF.Oszlopnév(10) + $"{sor}");

                }
                // idejön a vizsgálatra BM

                // keretezés
                MyX.Rácsoz(munkalap, $"{MyF.Oszlopnév(1)}1:{MyF.Oszlopnév(6)}{2}");
                MyX.Rácsoz(munkalap, $"{MyF.Oszlopnév(1)}3:{MyF.Oszlopnév(6)}{sor}");
                MyX.Rácsoz(munkalap, $"{MyF.Oszlopnév(8)}1:{MyF.Oszlopnév(13)}{2}");
                MyX.Rácsoz(munkalap, $"{MyF.Oszlopnév(8)}3:{MyF.Oszlopnév(13)}{sor}");
                MyX.Rácsoz(munkalap, $"{MyF.Oszlopnév(1)}{sorfej}:{MyF.Oszlopnév(6)}{sorfej}");
                MyX.Rácsoz(munkalap, $"{MyF.Oszlopnév(8)}{sorfej}:{MyF.Oszlopnév(13)}{sorfej}");

                // **********************************
                // nyomtatási beállítások
                // **********************************
                bool papírelrendez = false;
                if (papírelrendezés == "Álló") papírelrendez = true;
                if (papírméret == "--") papírméret = "A4";

                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"A1:{MyF.Oszlopnév(13)}{sor}",
                    Álló = papírelrendez,
                    LapMagas = 1,
                    LapSzéles = 1,
                    AlsóMargó = 5,
                    FelsőMargó = 5,
                    BalMargó = 6,
                    JobbMargó = 6,
                    LáblécMéret = 8,
                    FejlécMéret = 8,
                    Papírméret= papírméret  ,
                    VízKözép=true ,
                    FüggKözép=true 
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);

                // bezárjuk az Excel-t
                MyX.ExcelMentés(fájlexl);
                MyX.ExcelBezárás();
                MyF.Megnyitás(fájlexl);
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

