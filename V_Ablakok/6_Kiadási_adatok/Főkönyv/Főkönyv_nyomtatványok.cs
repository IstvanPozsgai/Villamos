using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;

namespace Villamos.Villamos_Nyomtatványok
{
    public class Főkönyv_Főkönyv
    {
        int oszlop;
        int eleje;
        int sor;
        int utolsó;
        string kicsinálta;
        int oszlop1;
        string viszonylatelőző;
        int szerelvényhossz;
        int szerelvényhossz1;
        readonly string munkalap = "Munka1";
        int újsor;

        public void Főkönyv_Alap(string Cmbtelephely, string szövegd, string napszak, DateTime Dátum, string fájlexc)
        {
            MyE.ExcelLétrehozás();

            MyE.Munkalap_betű("Arial", 12);

            // oszlop szélességeket beállítjuk az alapot
            MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(1) + ":" + MyE.Oszlopnév(1), 7);
            MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(2) + ":" + MyE.Oszlopnév(11), 9);

            // elkészítjük a fejlécet
            MyE.Betű("A1:l1", 16);

            MyE.Egyesít(munkalap, "A1:d1");
            MyE.Kiir(Cmbtelephely.Trim() + " Üzem", "a1");
            MyE.Egyesít(munkalap, "e1:i1");
            MyE.Kiir("©Főkönyv", "e1");
            MyE.Egyesít(munkalap, "j1:l1");
            MyE.Kiir(szövegd, "j1");
            sor = 2;

            MyE.Kiir("Visz.", "a" + $"{sor}");
            MyE.Egyesít(munkalap, "b" + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
            MyE.Kiir("I. Járműállomány", "b" + $"{sor}");

            oszlop = 12;
            eleje = 12;

            Kezelő_Jármű_Állomány_Típus KJÁT_kéz = new Kezelő_Jármű_Állomány_Típus();
            List<Adat_Jármű_Állomány_Típus> típus = KJÁT_kéz.Lista_Adatok(Cmbtelephely.Trim());

            foreach (Adat_Jármű_Állomány_Típus rekord in típus)
            {
                MyE.Kiir(rekord.Típus.Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");

                MyE.Oszlopszélesség(munkalap, (MyE.Oszlopnév(oszlop) + ":" + MyE.Oszlopnév(oszlop)));
                oszlop += 1;
            }

            utolsó = oszlop;

            MyE.Rácsoz("a" + $"{sor}" + ":" + MyE.Oszlopnév(oszlop - 1) + $"{sor}");
            MyE.Vastagkeret("a" + $"{sor}" + ":" + MyE.Oszlopnév(oszlop - 1) + $"{sor}");

            // napi adatok tábla
            // megnézzük kicsinálta
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\főkönyv\{Dátum.Year}\nap\{Dátum:yyyyMMdd}{napszak.Trim()}nap.mdb";
            string jelszó = "lilaakác";
            string szöveg = "SELECT * FROM segédtábla WHERE id=1 ";
            Kezelő_Főkönyv_SegédTábla KézSegédTábla = new Kezelő_Főkönyv_SegédTábla();
            Adat_Főkönyv_SegédTábla ElemSegéd = KézSegédTábla.Egy_Adat(hely, jelszó, szöveg);

            if (ElemSegéd != null)
                kicsinálta = ElemSegéd.Bejelentkezésinév;
            else
                kicsinálta = "*";

            // ******************************************
            // ***** Forgalomba adott járművek kezdete **
            // ******************************************
            szöveg = "SELECT * FROM adattábla where Adattábla.viszonylat <>'-'";
            if (napszak == "de")
                szöveg += " AND napszak='DE' ";
            else
                szöveg += " AND napszak='DU' ";
            szöveg += " order by Adattábla.viszonylat,adattábla.tényindulás, Adattábla.forgalmiszám, Adattábla.azonosító ";

            sor += 1;
            oszlop1 = 2;
            viszonylatelőző = "";
            szerelvényhossz = 0;
            szerelvényhossz1 = 0;

            // *********************************
            // ide kell írni a forgalmi kocsikat
            // *********************************

            MyE.Kiir("Forgalomba adott járművek", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, true, true);
            MyE.Vastagkeret(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");

            MyE.Háttérszín(MyE.Oszlopnév(1) + $"{sor}", System.Drawing.Color.GreenYellow);

            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");

            sor += 1;

            int[] sordarab = new int[16];
            int[] forgalombanösszesen = new int[16];
            int[] tartalék = new int[16];
            int[] javításon = new int[16];
            int[] félreállítás = new int[16];
            int[] főjavítás = new int[16];
            int[] telepenkívül = new int[16];
            int[] személyzet = new int[16];
            // lenullázuk a darabszámokat
            for (int j = 0; j < 16; j++)
            {
                forgalombanösszesen[j] = 0;
                tartalék[j] = 0;
                javításon[j] = 0;
                félreállítás[j] = 0;
                főjavítás[j] = 0;
                sordarab[j] = 0;
                telepenkívül[j] = 0;
                személyzet[j] = 0;
            }

            Kezelő_Főkönyv_Nap KFN_kép = new Kezelő_Főkönyv_Nap();
            List<Adat_Főkönyv_Nap> Adatok = KFN_kép.Lista_adatok(hely, jelszó, szöveg);

            int előzőkocsihossz;
            int nemelső;

            viszonylatelőző = "-";

            előzőkocsihossz = 0;
            nemelső = 0;

            foreach (Adat_Főkönyv_Nap rekord in Adatok)
            {

                if (rekord.Viszonylat.Trim() != "-")
                {
                    // első adat
                    if (viszonylatelőző == "-")
                        viszonylatelőző = rekord.Viszonylat.Trim();
                    // Ameddig egyforma addig nem választjuk el
                    if (viszonylatelőző.Trim() == rekord.Viszonylat.Trim())
                    {
                        MyE.Kiir(rekord.Viszonylat.Trim(), MyE.Oszlopnév(1) + $"{sor}");
                        // ha rövidebb a szerelvény akkor kiirja a következőt

                        if (rekord.Kocsikszáma == 1)
                            szerelvényhossz = 0;

                        if (előzőkocsihossz < rekord.Kocsikszáma && nemelső == 0)
                        {
                            szerelvényhossz = 0;
                            nemelső = 1;
                        }

                        if (szerelvényhossz != rekord.Kocsikszáma)
                        {
                            MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString());
                            // ha beálló akkor színez
                            if (rekord.Tényérkezés.Hour > 7 && rekord.Tényérkezés.Hour < 12)
                                MyE.Háttérszín(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), System.Drawing.Color.Orange);

                            // ha beállóba kért akkor dőlt betű
                            if (rekord.Státus == 3)
                                MyE.Betű(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), false, true, true);

                            //Ha rossz kocsi van forgalomban
                            if (rekord.Státus == 4)
                            {
                                MyE.Betű(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), false, true, true);
                                MyE.Háttérszín(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), System.Drawing.Color.Red);
                                MyE.Betű(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), System.Drawing.Color.Yellow);
                            }

                            if (rekord.Megjegyzés.Trim().ToUpper().Substring(0, 1) == "T") // ha T betűvel kezdődik többlet kiadás
                            {

                                MyE.Betű(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), true, false, true);
                                MyE.Háttérszín(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), System.Drawing.Color.LightSkyBlue);
                            }
                            // személyzet hiány
                            if (rekord.Megjegyzés.Trim().ToUpper().Substring(0, 1) == "S") // ha s betűvel kezdődik személyzet hiány
                            {
                                MyE.Háttérszín(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), System.Drawing.Color.GreenYellow);
                                MyE.Betű(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), false, false, true);

                                for (int i = 0; i < típus.Count; i++)
                                {
                                    // megkeressük a típust és emeljük a darabszámot
                                    if (rekord.Típus.Trim() == típus[i].Típus.Trim())
                                    {
                                        személyzet[i] = személyzet[i] + 1;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                // megkeressük, hogy melyik típusba tartozik majd emeljük a darabszámot
                                // ha személyzet hiányos akkor nem emeljük a darabszámokat
                                for (int j = 0; j < típus.Count; j++)
                                {
                                    // megkeressük a típust és emeljük a darabszámot
                                    if (rekord.Típus.Trim() == típus[j].Típus.Trim())
                                    {
                                        sordarab[j] = sordarab[j] + 1;
                                        break;
                                    }
                                }

                            }
                            szerelvényhossz += 1;
                        }
                        // ha elértük az utolsó kocsit a szerelvényben akkor emeljük a oszlopszámot
                        if (szerelvényhossz == Convert.ToInt32(rekord.Kocsikszáma))
                        {
                            szerelvényhossz1 = Convert.ToInt32(rekord.Kocsikszáma);
                            előzőkocsihossz = Convert.ToInt32(rekord.Kocsikszáma);
                            nemelső = 0;
                            oszlop1 += 1;
                            if (oszlop1 == 12)
                            {
                                // kiírjuk a sor végén a darabszámokat
                                for (int j = 0; j < típus.Count; j++)
                                {

                                    if (típus[j].Típus.Trim() != "")
                                    {
                                        MyE.Kiir(sordarab[j].ToString(), MyE.Oszlopnév(eleje + j) + $"{sor}");
                                        forgalombanösszesen[j] = forgalombanösszesen[j] + sordarab[j];
                                        MyE.Egyesít(munkalap, MyE.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyE.Oszlopnév(eleje + j) + (sor + szerelvényhossz).ToString());
                                        MyE.Igazít_függőleges(MyE.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyE.Oszlopnév(eleje + j) + (sor + szerelvényhossz).ToString(), "közép");
                                    }

                                }
                                // megformázzuk a sort(sorokat)
                                if (szerelvényhossz > 0)
                                    MyE.Rácsoz(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + (sor + szerelvényhossz).ToString());

                                // viszonylatot egyesít
                                MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(1) + (sor + szerelvényhossz).ToString());
                                MyE.Igazít_függőleges(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(1) + (sor + szerelvényhossz).ToString(), "közép");
                                // lenullázuk a darabszámokat

                                for (int j = 0; j < típus.Count; j++)
                                {
                                    sordarab[j] = 0;
                                }

                                oszlop1 = 2;
                                sor = sor + 1 + szerelvényhossz;
                                szerelvényhossz1 = szerelvényhossz;

                            }
                            szerelvényhossz = 0;
                        }
                    }
                    else
                    {
                        // ha különböző lesz akkor kiírjuk a darabszámokat
                        if (oszlop1 != 2)
                        {
                            for (int j = 0; j < típus.Count; j++)
                            {
                                if (típus[j].Típus.Trim() != "")
                                {
                                    MyE.Kiir(sordarab[j].ToString(), MyE.Oszlopnév(eleje + j) + $"{sor}");
                                    forgalombanösszesen[j] = forgalombanösszesen[j] + sordarab[j];
                                    MyE.Egyesít(munkalap, MyE.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyE.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString());
                                    MyE.Igazít_függőleges(MyE.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyE.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString(), "közép");
                                }

                            }
                            // megformázzuk a sort(sorokat)
                            if (szerelvényhossz1 > 0)
                            {
                                MyE.Rácsoz(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + (sor + szerelvényhossz1).ToString());
                            }
                            // viszonylatot egyesít
                            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(1) + (sor + szerelvényhossz1).ToString());
                            MyE.Igazít_függőleges(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(1) + (sor + szerelvényhossz1).ToString(), "közép");
                            // lenullázuk a darabszámokat
                            for (int j = 0; j < típus.Count; j++)
                            {
                                sordarab[j] = 0;
                            }
                            // ha különböző lesz akkor kihagyunk egy sort és visszamegyünk az első oszlophoz
                            sor = sor + 1 + Convert.ToInt32(szerelvényhossz1);
                        }
                        oszlop1 = 2;
                        MyE.Kiir(rekord.Viszonylat.Trim(), MyE.Oszlopnév(1) + $"{sor}");
                        MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString());
                        if (rekord.Kocsikszáma > 1)
                        {
                            nemelső = 1;
                        }
                        // ha beálló akkor színez
                        if (rekord.Tényérkezés.Hour > 7 && rekord.Tényérkezés.Hour < 12)
                        {
                            MyE.Háttérszín(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), System.Drawing.Color.Orange);
                        }
                        // ha beállóba kért akkor dőlt betű
                        if (rekord.Státus == 3)
                        {
                            MyE.Betű(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), false, true, true);
                        }
                        if (rekord.Megjegyzés.Trim().ToUpper().Substring(0, 1) == "T") // ha T betűvel kezdődik többlet kiadás
                        {

                            MyE.Betű(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), true, false, true);
                            MyE.Háttérszín(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), System.Drawing.Color.LightSkyBlue);
                        }
                        // személyzet hiány
                        if (rekord.Megjegyzés.Trim().ToUpper().Substring(0, 1) == "S") // ha s betűvel kezdődik személyzet hiány
                        {
                            MyE.Háttérszín(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), System.Drawing.Color.GreenYellow);
                            MyE.Betű(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), false, false, true);

                            for (int i = 0; i < típus.Count; i++)
                            {
                                // megkeressük a típust és emeljük a darabszámot
                                if (rekord.Típus.Trim() == típus[i].Típus.Trim())
                                {
                                    személyzet[i] = személyzet[i] + 1;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            // megkeressük, hogy melyik típusba tartozik majd emeljük a darabszámot
                            // ha személyzet hiányos akkor nem emeljük a darabszámokat
                            for (int j = 0; j < típus.Count; j++)
                            {
                                // megkeressük a típust és emeljük a darabszámot
                                if (rekord.Típus.Trim() == típus[j].Típus.Trim())
                                {
                                    sordarab[j] = sordarab[j] + 1;
                                    break;
                                }
                            }

                        }
                        if (rekord.Kocsikszáma == 1)
                            oszlop1 += 1; // ha egy kocsiból áll a szerelvény akkor a következőkocsit felül írja az elsőt
                        szerelvényhossz += 1;
                    }
                }
                viszonylatelőző = rekord.Viszonylat.Trim();

            }



            // kiírjuk a sor végén a darabszámokat

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyE.Kiir(sordarab[j].ToString(), MyE.Oszlopnév(eleje + j) + $"{sor}");
                    forgalombanösszesen[j] = forgalombanösszesen[j] + sordarab[j];
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyE.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString());
                    MyE.Igazít_függőleges(MyE.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyE.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString().Trim(), "közép");
                }
            }
            // megformázzuk a sort(sorokat)
            MyE.Rácsoz(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + (sor + szerelvényhossz1).ToString());
            // viszonylatot egyesít
            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(1) + (sor + szerelvényhossz1).ToString());
            MyE.Igazít_függőleges(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(1) + (sor + szerelvényhossz1).ToString(), "közép");
            sor = sor + szerelvényhossz1 + 1;

            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
            MyE.Kiir("Forgalomban Összesen:", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Igazít_vízszintes(MyE.Oszlopnév(1) + $"{sor}", "jobb");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, true, true);
            // típusonként összeadjuk a forgalomban lévőket
            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyE.Kiir(forgalombanösszesen[j].ToString(), MyE.Oszlopnév(eleje + j) + $"{sor}");
                    MyE.Igazít_vízszintes(MyE.Oszlopnév(eleje + j) + $"{sor}", "közép");
                    MyE.Betű(MyE.Oszlopnév(eleje + j) + $"{sor}", false, true, true);
                }

            }
            MyE.Rácsoz("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Vastagkeret("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            sor += 1;
            // ******************************************
            // ***** Forgalomba adott járművek Vége    **
            // ******************************************

            // '**********************************
            // 'ide kell személyzet hiányt
            // '**********************************
            MyE.Kiir("Személyzet hiány", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, false, true);
            MyE.Vastagkeret(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Háttérszín(MyE.Oszlopnév(1) + $"{sor}", System.Drawing.Color.GreenYellow);

            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            sor += 1;

            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
            MyE.Kiir("Személyzet hiány Összesen:", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, true, true);
            MyE.Igazít_vízszintes(MyE.Oszlopnév(1) + $"{sor}", "jobb");

            // típusonként összeadjuk a forgalomban lévőket

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyE.Kiir(személyzet[j].ToString(), MyE.Oszlopnév(eleje + j) + $"{sor}");
                    MyE.Igazít_vízszintes(MyE.Oszlopnév(eleje + j) + $"{sor}", "közép");
                    MyE.Betű(MyE.Oszlopnév(eleje + j) + $"{sor}", false, true, true);
                }
            }
            MyE.Rácsoz("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Rácsoz("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            sor += 1;

            // **********************************
            // ide kell írni a tartalék kocsikat
            // **********************************
            MyE.Kiir("Üzemképes Tartalék", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, false, true);
            MyE.Vastagkeret(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Háttérszín(MyE.Oszlopnév(1) + $"{sor}", System.Drawing.Color.GreenYellow);

            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");

            // tartalékok kiírása

            szöveg = "SELECT * FROM adattábla where napszak ='_' ";
            szöveg += " order by  típus asc,adattábla.kocsikszáma  desc, adattábla.szerelvény, Adattábla.azonosító ";
            Adatok = KFN_kép.Lista_adatok(hely, jelszó, szöveg);

            string előzőtípus = "-";
            szerelvényhossz = 0;
            szerelvényhossz1 = 0;
            long előzőszerelvény = 0;
            oszlop1 = 1;
            sor += 1;

            foreach (Adat_Főkönyv_Nap rekord in Adatok)
            {

                // első adat
                if (viszonylatelőző == "-")
                    viszonylatelőző = rekord.Viszonylat.Trim();
                if (előzőtípus == "-")
                    előzőtípus = rekord.Típus.Trim();

                // ha a másik típus lesz
                if (előzőtípus.Trim() != rekord.Típus.Trim())
                {
                    if (szerelvényhossz1 < szerelvényhossz)
                        szerelvényhossz1 = szerelvényhossz;
                    // megformázzuk a sort(sorokat)
                    MyE.Rácsoz(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + (sor + szerelvényhossz1).ToString());
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(1) + (sor + szerelvényhossz1).ToString());
                    // ha különböző lesz akkor kiírjuk a darabszámokat
                    for (int j = 0; j < típus.Count; j++)
                    {
                        if (típus[j].Típus.Trim() != "")
                        {
                            MyE.Egyesít(munkalap, MyE.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyE.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString());
                            MyE.Igazít_függőleges(MyE.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyE.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString(), "közép");
                        }
                    }
                    oszlop1 = 1;
                    sor = sor + 1 + szerelvényhossz1;
                    szerelvényhossz1 = szerelvényhossz;
                    előzőtípus = rekord.Típus.Trim();
                }
                // ha a kocsik száma egyenlő a szerelvény számmal akkor új oszlopba írja
                if (rekord.Kocsikszáma <= 1 && rekord.Státus != 4)
                {
                    oszlop1 += 1;
                    if (oszlop1 == 12)
                    {
                        // megformázzuk a sort(sorokat)
                        MyE.Rácsoz(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + (sor + szerelvényhossz1).ToString());
                        MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(1) + (sor + szerelvényhossz1).ToString());
                        // ha különböző lesz akkor kiírjuk a darabszámokat

                        for (int j = 0; j < típus.Count; j++)
                        {
                            if (típus[j].Típus.Trim() != "")
                            {
                                MyE.Egyesít(munkalap, MyE.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyE.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString());
                                MyE.Igazít_függőleges(MyE.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyE.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString(), "közép");
                            }
                        }
                        oszlop1 = 2;
                        sor = sor + 1 + szerelvényhossz1;
                        szerelvényhossz1 = szerelvényhossz;
                    }
                    if (szerelvényhossz1 < szerelvényhossz)
                        szerelvényhossz1 = szerelvényhossz;
                    szerelvényhossz = 0;
                }
                // ha más a szerelényszám akkor új oszlopba írjuk előzőszerelvény = rekord.szerelvény")
                if (előzőszerelvény != rekord.Szerelvény && rekord.Kocsikszáma > 1)
                {
                    oszlop1 += 1;
                    if (oszlop1 == 12)
                    {
                        // megformázzuk a sort(sorokat)
                        MyE.Rácsoz(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + (sor + szerelvényhossz1).ToString());
                        MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(1) + (sor + szerelvényhossz1).ToString());
                        // ha különböző lesz akkor kiírjuk a darabszámokat
                        for (int j = 0; j < típus.Count; j++)
                        {
                            if (típus[j].Típus.Trim() != "")
                            {
                                MyE.Egyesít(munkalap, MyE.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyE.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString());
                                MyE.Igazít_függőleges(MyE.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyE.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString(), "közép");
                            }

                        }
                        oszlop1 = 2;
                        sor = sor + 1 + szerelvényhossz1;
                        szerelvényhossz1 = szerelvényhossz;
                    }
                    if (szerelvényhossz1 < szerelvényhossz)
                        szerelvényhossz1 = szerelvényhossz;
                    szerelvényhossz = 0;
                }
                előzőszerelvény = rekord.Szerelvény;
                if (szerelvényhossz != rekord.Kocsikszáma)
                {
                    if (rekord.Kocsikszáma > 1)
                    {
                        MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString());

                        MyE.Kiir(rekord.Típus.Trim(), "a" + $"{sor}");
                        if (rekord.Státus == 4)
                        {
                            MyE.Betű(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), false, false, true);
                            MyE.Háttérszín(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), System.Drawing.Color.Red);
                            MyE.Betű(MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), System.Drawing.Color.Yellow);
                        }
                        szerelvényhossz += 1;
                    }
                    else if (rekord.Státus != 4)
                    {
                        MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString());

                        MyE.Kiir(rekord.Típus.Trim(), "a" + $"{sor}");

                        szerelvényhossz += 1;
                    }
                    // megkeressük, hogy melyik típusba tartozik majd emeljük a darabszámot
                    for (int j = 0; j < típus.Count; j++)
                    {
                        // megkeressük a típust és emeljük a darabszámot
                        if (rekord.Típus.Trim() == típus[j].Típus.Trim())
                        {
                            if (rekord.Státus != 4)
                            {
                                tartalék[j] = tartalék[j] + 1;
                                break;
                            }
                        }
                    }

                }
                // ha nincs szerelvényben
                if (rekord.Kocsikszáma == 0)
                {
                    if (rekord.Státus != 4)
                    {
                        // ha nem álló akkor kiírja
                        MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString());

                        MyE.Kiir(rekord.Típus.Trim(), "a" + $"{sor}");
                    }
                    else
                    {
                    }
                    // megkeressük, hogy melyik típusba tartozik majd emeljük a darabszámot
                    for (int j = 0; j < típus.Count; j++)
                    {
                        // megkeressük a típust és emeljük a darabszámot
                        if (rekord.Típus.Trim() == típus[j].Típus.Trim())
                        {
                            if (rekord.Státus != 4)
                            {
                                tartalék[j] = tartalék[j] + 1;
                                break;
                            }
                        }
                    }
                }
            }


            // ha végére ért és nem volt formázás.
            if (oszlop1 != 2)
            {
                // megformázzuk a sort(sorokat)
                MyE.Rácsoz(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + (sor + szerelvényhossz1).ToString());
                MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(1) + (sor + szerelvényhossz1).ToString());
                MyE.Igazít_függőleges(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(1) + (sor + szerelvényhossz1).ToString(), "közép");
                // ha különböző lesz akkor kiírjuk a darabszámokat

                for (int j = 0; j < típus.Count; j++)
                {
                    if (típus[j].Típus.Trim() != "")
                    {
                        MyE.Egyesít(munkalap, MyE.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyE.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString());
                        MyE.Igazít_függőleges(MyE.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyE.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString(), "közép");
                    }
                }
                oszlop1 = 2;
                sor = sor + 1 + szerelvényhossz1;
                szerelvényhossz1 = szerelvényhossz;
            } // ha volt adat akkor formáz


            // megformázzuk a sort(sorokat)
            MyE.Rácsoz(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + (sor + szerelvényhossz1).ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(1) + (sor + szerelvényhossz1).ToString());
            // ha különböző lesz akkor kiírjuk a darabszámokat

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyE.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString());
                    MyE.Igazít_függőleges(MyE.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyE.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString(), "közép");
                }
            }



            // tartalék Összesítő
            sor = sor + 1 + szerelvényhossz1;
            // sor = sor + szerelvényhossz1
            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
            MyE.Kiir("Tartalék Összesen:", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Igazít_vízszintes(MyE.Oszlopnév(1) + $"{sor}", "jobb");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, true, true);
            // típusonként összeadjuk a tartalékokat

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyE.Kiir(tartalék[j].ToString(), MyE.Oszlopnév(eleje + j) + $"{sor}");
                    MyE.Igazít_vízszintes(MyE.Oszlopnév(eleje + j) + $"{sor}", "közép");
                    MyE.Betű(MyE.Oszlopnév(eleje + j) + $"{sor}", false, true, true);
                }
            }
            MyE.Rácsoz("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Vastagkeret("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            sor += 1;

            // ******************************************
            // Üzemképes Összesen
            // ******************************************
            MyE.Kiir("Üzemképes Villamosok", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, false, true);
            MyE.Vastagkeret(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Háttérszín(MyE.Oszlopnév(1) + $"{sor}", System.Drawing.Color.GreenYellow);

            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");

            // üzemképes Összesítő
            sor += 1;
            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
            MyE.Kiir("Üzemképes Összesen:", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Igazít_vízszintes(MyE.Oszlopnév(1) + $"{sor}", "jobb");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, true, true);
            // típusonként összeadjuk a tartalékokat

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyE.Kiir((tartalék[j] + forgalombanösszesen[j] + személyzet[j]).ToString(), MyE.Oszlopnév(eleje + j) + $"{sor}");
                    MyE.Igazít_vízszintes(MyE.Oszlopnév(eleje + j) + $"{sor}", "közép");
                    MyE.Betű(MyE.Oszlopnév(eleje + j) + $"{sor}", false, true, true);
                }
            }
            MyE.Rácsoz("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Vastagkeret("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");

            // ******************************************
            // ide kerül a kocsiszíni javítás
            // ******************************************

            sor += 1;
            MyE.Kiir("Kocsiszíni javítás", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, false, true);
            MyE.Vastagkeret(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Háttérszín(MyE.Oszlopnév(1) + $"{sor}", System.Drawing.Color.GreenYellow);

            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");

            sor += 1;

            MyE.Kiir("Psz", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, true, false);
            MyE.Egyesít(munkalap, MyE.Oszlopnév(2) + $"{sor}" + ":" + MyE.Oszlopnév(3) + $"{sor}");
            MyE.Kiir("Dátum", MyE.Oszlopnév(2) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(2) + $"{sor}", false, true, false);
            MyE.Egyesít(munkalap, MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
            MyE.Kiir("Javítás leírása", MyE.Oszlopnév(4) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(4) + $"{sor}", false, true, false);
            MyE.Egyesít(munkalap, MyE.Oszlopnév(eleje) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Rácsoz(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop - 1) + $"{sor}");
            MyE.Vastagkeret(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop - 1) + $"{sor}");

            int soreleje = sor;
            int sorvége;


            szöveg = "SELECT * FROM adattábla where adattábla.státus=4 and  (Adattábla.napszak ='-' or Adattábla.napszak ='_') ";
            szöveg += " order by azonosító";
            Adatok = KFN_kép.Lista_adatok(hely, jelszó, szöveg);

            foreach (Adat_Főkönyv_Nap rekord in Adatok)
            {
                if (!rekord.Hibaleírása.Contains("#") && !rekord.Hibaleírása.Contains("&") && !rekord.Hibaleírása.Contains("§"))
                {
                    sor++;
                    if (soreleje == 0)
                        soreleje = sor;
                    MyE.Kiir(rekord.Azonosító.Trim(), "a" + $"{sor}");

                    MyE.Egyesít(munkalap, MyE.Oszlopnév(2) + $"{sor}" + ":" + MyE.Oszlopnév(3) + $"{sor}");
                    MyE.Kiir(rekord.Miótaáll.ToString("yyyy.MM.dd"), MyE.Oszlopnév(2) + $"{sor}");

                    MyE.Egyesít(munkalap, MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
                    MyE.Kiir(rekord.Hibaleírása.Trim(), MyE.Oszlopnév(4) + $"{sor}");

                    MyE.Igazít_vízszintes(MyE.Oszlopnév(4) + $"{sor}", "bal");
                    if (rekord.Hibaleírása.Trim().Length > 75)
                    {
                        int sor_magasság = ((rekord.Hibaleírása.Length / 75) + 1) * 15;
                        MyE.Sormagasság(sor.ToString() + ":" + $"{sor}", sor_magasság);
                        MyE.Egyesít(munkalap, MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
                        MyE.Sortörésseltöbbsorba(MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}", true);
                    }

                    // kiválasztjuk melyik típus
                    for (int j = 0; j < típus.Count; j++)
                    {
                        if (rekord.Típus.Trim() == típus[j].Típus.Trim())
                        {
                            javításon[j] = javításon[j] + 1;
                            MyE.Kiir("1", MyE.Oszlopnév(12 + j) + $"{sor}");
                            break;
                        }
                    }
                }

            }
            sorvége = sor;
            if (soreleje != 0)
            {
                if (soreleje == sorvége)
                {
                    // ha egy sor
                    MyE.Rácsoz(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(utolsó - 1) + sorvége.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(2) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(3) + sorvége.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(4) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(11) + sorvége.ToString());
                    MyE.Rácsoz(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                }
                if (soreleje + 1 <= sorvége)
                {
                    // ha több sor
                    MyE.Rácsoz(MyE.Oszlopnév(1) + soreleje.ToString() + ":" + MyE.Oszlopnév(utolsó - 1) + sorvége.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(2) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(3) + sorvége.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(4) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(11) + sorvége.ToString());
                    MyE.Rácsoz(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                }
            }

            sor++;
            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
            MyE.Kiir("Kocsiszíni javítás Összesen:", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Igazít_vízszintes(MyE.Oszlopnév(1) + $"{sor}", "jobb");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, true, true);

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyE.Kiir(javításon[j].ToString(), MyE.Oszlopnév(12 + j) + $"{sor}");
                    MyE.Igazít_vízszintes(MyE.Oszlopnév(12 + j) + $"{sor}", "közép");
                    MyE.Betű(MyE.Oszlopnév(12 + j) + $"{sor}", false, true, true);
                }

            }
            MyE.Rácsoz("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Vastagkeret("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            // ******************************************
            // ide kerül a kocsiszíni javítás  vége
            // ******************************************

            // ******************************************
            // ide kerül a telepen kívüli javítás
            // ******************************************
            sor++;
            MyE.Kiir("Telephelyen kívüli javítás", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, false, true);
            MyE.Vastagkeret(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Háttérszín(MyE.Oszlopnév(1) + $"{sor}", System.Drawing.Color.GreenYellow);

            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");

            sor += 1;

            MyE.Kiir("Psz", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, true, false);
            MyE.Egyesít(munkalap, MyE.Oszlopnév(2) + $"{sor}" + ":" + MyE.Oszlopnév(3) + $"{sor}");
            MyE.Kiir("Dátum", MyE.Oszlopnév(2) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(2) + $"{sor}", false, true, false);
            MyE.Egyesít(munkalap, MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
            MyE.Kiir("Javítás leírása", MyE.Oszlopnév(4) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(4) + $"{sor}", false, true, false);
            MyE.Egyesít(munkalap, MyE.Oszlopnév(eleje) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Rácsoz(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop - 1) + $"{sor}");
            MyE.Vastagkeret(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop - 1) + $"{sor}");

            szöveg = "SELECT * FROM adattábla where adattábla.státus=4 and Adattábla.viszonylat ='-'";
            szöveg += " order by azonosító";
            Adatok = KFN_kép.Lista_adatok(hely, jelszó, szöveg);

            soreleje = sor;

            foreach (Adat_Főkönyv_Nap rekord in Adatok)
            {

                if (rekord.Hibaleírása.Contains("§"))
                {
                    sor++;
                    if (soreleje == 0)
                        soreleje = sor;
                    MyE.Kiir(rekord.Azonosító.Trim(), "a" + $"{sor}");

                    MyE.Egyesít(munkalap, MyE.Oszlopnév(2) + $"{sor}" + ":" + MyE.Oszlopnév(3) + $"{sor}");
                    MyE.Kiir(rekord.Miótaáll.ToString("yyyy.MM.dd"), MyE.Oszlopnév(2) + $"{sor}");

                    MyE.Egyesít(munkalap, MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
                    MyE.Kiir(rekord.Hibaleírása.Trim(), MyE.Oszlopnév(4) + $"{sor}");

                    MyE.Igazít_vízszintes(MyE.Oszlopnév(4) + $"{sor}", "bal");

                    if (rekord.Hibaleírása.Trim().Length > 75)
                    {
                        int sor_magasság = ((rekord.Hibaleírása.Length / 75) + 1) * 15;
                        MyE.Sormagasság(sor.ToString() + ":" + $"{sor}", sor_magasság);
                        MyE.Egyesít(munkalap, MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
                        MyE.Sortörésseltöbbsorba(MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}", true);
                    }

                    // kiválasztjuk melyik típus
                    for (int j = 0; j < típus.Count; j++)
                    {
                        if (rekord.Típus.Trim() == típus[j].Típus.Trim())
                        {
                            telepenkívül[j] = telepenkívül[j] + 1;
                            MyE.Kiir("1", MyE.Oszlopnév(12 + j) + $"{sor}");
                            break;
                        }
                    }
                }
            }
            sorvége = sor;
            if (soreleje != 0)
            {
                if (soreleje == sorvége)
                {
                    // ha egy sor
                    MyE.Rácsoz(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(utolsó - 1) + sorvége.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(2) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(3) + sorvége.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(4) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(11) + sorvége.ToString());
                    MyE.Rácsoz(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                }
                if (soreleje + 1 <= sorvége)
                {
                    // ha több sor
                    MyE.Rácsoz(MyE.Oszlopnév(1) + soreleje.ToString() + ":" + MyE.Oszlopnév(utolsó - 1) + sorvége.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(2) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(3) + sorvége.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(4) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(11) + sorvége.ToString());
                    MyE.Rácsoz(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                }
            }


            sor++;
            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
            MyE.Kiir("Telephelyen kívüli javítás Összesen:", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Igazít_vízszintes(MyE.Oszlopnév(1) + $"{sor}", "jobb");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, true, true);

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyE.Kiir(telepenkívül[j].ToString(), MyE.Oszlopnév(12 + j) + $"{sor}");
                    MyE.Igazít_vízszintes(MyE.Oszlopnév(12 + j) + $"{sor}", "közép");
                    MyE.Betű(MyE.Oszlopnév(12 + j) + $"{sor}", false, true, true);
                }

            }
            MyE.Rácsoz("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Vastagkeret("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            // ******************************************
            // ide kerül a telepen kívüli javítás vége
            // ******************************************


            // ***********************************
            // ide a félre áLlítás
            // ***********************************
            // fejléc
            sor++;
            MyE.Kiir("Félreállítás", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, false, true);
            MyE.Vastagkeret(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Háttérszín(MyE.Oszlopnév(1) + $"{sor}", System.Drawing.Color.GreenYellow);

            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");

            sor += 1;

            MyE.Kiir("Psz", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, true, false);
            MyE.Egyesít(munkalap, MyE.Oszlopnév(2) + $"{sor}" + ":" + MyE.Oszlopnév(3) + $"{sor}");
            MyE.Kiir("Dátum", MyE.Oszlopnév(2) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(2) + $"{sor}", false, true, false);
            MyE.Egyesít(munkalap, MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
            MyE.Kiir("Javítás leírása", MyE.Oszlopnév(4) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(4) + $"{sor}", false, true, false);
            MyE.Egyesít(munkalap, MyE.Oszlopnév(eleje) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Rácsoz(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop - 1) + $"{sor}");
            MyE.Vastagkeret(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop - 1) + $"{sor}");

            soreleje = 0;

            foreach (Adat_Főkönyv_Nap rekord in Adatok)
            {
                if (rekord.Hibaleírása.Contains("&"))

                {
                    sor++;
                    if (soreleje == 0)
                        soreleje = sor;
                    MyE.Kiir(rekord.Azonosító.Trim(), "a" + $"{sor}");

                    MyE.Vastagkeret("a" + $"{sor}");
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(2) + $"{sor}" + ":" + MyE.Oszlopnév(3) + $"{sor}");
                    MyE.Vastagkeret(MyE.Oszlopnév(2) + $"{sor}" + ":" + MyE.Oszlopnév(3) + $"{sor}");
                    MyE.Kiir(rekord.Miótaáll.ToString("yyyy.MM.dd"), MyE.Oszlopnév(2) + $"{sor}");

                    MyE.Egyesít(munkalap, MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
                    MyE.Kiir(rekord.Hibaleírása.Trim(), MyE.Oszlopnév(4) + $"{sor}");

                    MyE.Igazít_vízszintes(MyE.Oszlopnév(4) + $"{sor}", "bal");
                    if (rekord.Hibaleírása.Trim().Length > 75)
                    {
                        int sor_magasság = ((rekord.Hibaleírása.Length / 75) + 1) * 15;
                        MyE.Sormagasság(sor.ToString() + ":" + $"{sor}", sor_magasság);
                        MyE.Egyesít(munkalap, MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
                        MyE.Sortörésseltöbbsorba(MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}", true);
                    }

                    // kiválasztjuk melyik típus
                    for (int j = 0; j < típus.Count; j++)
                    {
                        if (rekord.Típus.Trim() == típus[j].Típus.Trim())
                        {
                            félreállítás[j] = félreállítás[j] + 1;
                            MyE.Kiir("1", MyE.Oszlopnév(12 + j) + $"{sor}");
                            break;
                        }
                    }
                }
            }
            sorvége = sor;
            if (soreleje != 0)
            {
                if (soreleje == sorvége)
                {
                    // ha egy sor
                    MyE.Rácsoz(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(utolsó - 1) + sorvége.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(2) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(3) + sorvége.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(4) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(11) + sorvége.ToString());
                    MyE.Rácsoz(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                }
                if (soreleje < sorvége)
                {
                    // ha több sor
                    MyE.Rácsoz(MyE.Oszlopnév(1) + soreleje.ToString() + ":" + MyE.Oszlopnév(utolsó - 1) + sorvége.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(2) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(3) + sorvége.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(4) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(11) + sorvége.ToString());
                    MyE.Rácsoz(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                }
            }


            sor++;
            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
            MyE.Kiir("Félre állítás Összesen:", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Igazít_vízszintes(MyE.Oszlopnév(1) + $"{sor}", "jobb");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, true, true);

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyE.Kiir(félreállítás[j].ToString(), MyE.Oszlopnév(12 + j) + $"{sor}");
                    MyE.Igazít_vízszintes(MyE.Oszlopnév(12 + j) + $"{sor}", "közép");
                    MyE.Betű(MyE.Oszlopnév(12 + j) + $"{sor}", false, true, true);
                }
            }
            MyE.Rácsoz("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Vastagkeret("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");

            // ***********************************
            // ide a félre áLlítás vége
            // ***********************************


            // ********************************************
            // ide a Főjavítás
            // ********************************************
            // fejléc
            sor++;
            MyE.Kiir("Főjavítás", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, false, true);
            MyE.Vastagkeret(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Háttérszín(MyE.Oszlopnév(1) + $"{sor}", System.Drawing.Color.GreenYellow);

            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");

            sor++;


            MyE.Kiir("Psz", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, true, false);
            MyE.Egyesít(munkalap, MyE.Oszlopnév(2) + $"{sor}" + ":" + MyE.Oszlopnév(3) + $"{sor}");
            MyE.Kiir("Dátum", MyE.Oszlopnév(2) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(2) + $"{sor}", false, true, false);
            MyE.Egyesít(munkalap, MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
            MyE.Kiir("Javítás leírása", MyE.Oszlopnév(4) + $"{sor}");
            MyE.Betű(MyE.Oszlopnév(4) + $"{sor}", false, true, false);
            MyE.Egyesít(munkalap, MyE.Oszlopnév(eleje) + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Rácsoz(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop - 1) + $"{sor}");
            MyE.Vastagkeret(MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop - 1) + $"{sor}");


            soreleje = 0;

            foreach (Adat_Főkönyv_Nap rekord in Adatok)
            {

                if (rekord.Hibaleírása.Contains("#"))
                {
                    sor++;
                    if (soreleje == 0)
                        soreleje = sor;
                    MyE.Kiir(rekord.Azonosító.Trim(), "a" + $"{sor}");

                    MyE.Vastagkeret("a" + $"{sor}");
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(2) + $"{sor}" + ":" + MyE.Oszlopnév(3) + $"{sor}");
                    MyE.Vastagkeret(MyE.Oszlopnév(2) + $"{sor}" + ":" + MyE.Oszlopnév(3) + $"{sor}");
                    MyE.Kiir(rekord.Miótaáll.ToString("yyyy.MM.dd"), MyE.Oszlopnév(2) + $"{sor}");

                    MyE.Egyesít(munkalap, MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
                    MyE.Kiir(rekord.Hibaleírása.Trim(), MyE.Oszlopnév(4) + $"{sor}");

                    MyE.Igazít_vízszintes(MyE.Oszlopnév(4) + $"{sor}", "bal");
                    if (rekord.Hibaleírása.Trim().Length > 75)
                    {
                        int sor_magasság = ((rekord.Hibaleírása.Length / 75) + 1) * 15;
                        MyE.Sormagasság(sor.ToString() + ":" + $"{sor}", sor_magasság);
                        MyE.Egyesít(munkalap, MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
                        MyE.Sortörésseltöbbsorba(MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}", true);
                    }

                    // kiválasztjuk melyik típus
                    for (int j = 0; j < típus.Count; j++)
                    {
                        if (rekord.Típus.Trim() == típus[j].Típus.Trim())
                        {
                            főjavítás[j] = főjavítás[j] + 1;
                            MyE.Kiir("1", MyE.Oszlopnév(12 + j) + $"{sor}");
                            break;
                        }
                    }
                }
            }
            sorvége = sor;
            if (soreleje != 0)
            {
                if (soreleje == sorvége)
                {
                    // ha egy sor
                    MyE.Rácsoz(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(utolsó - 1) + sorvége.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(2) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(3) + sorvége.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(4) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(11) + sorvége.ToString());
                    MyE.Rácsoz(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                }
                if (soreleje + 1 <= sorvége)
                {
                    // ha több sor
                    MyE.Rácsoz(MyE.Oszlopnév(1) + soreleje.ToString() + ":" + MyE.Oszlopnév(utolsó - 1) + sorvége.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(2) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(3) + sorvége.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(4) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(11) + sorvége.ToString());
                    MyE.Rácsoz(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyE.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                }
            }

            sor++;
            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
            MyE.Kiir("Főjavítás Összesen:", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Igazít_vízszintes(MyE.Oszlopnév(1) + $"{sor}", "jobb");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, true, true);

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyE.Kiir(főjavítás[j].ToString(), MyE.Oszlopnév(12 + j) + $"{sor}");
                    MyE.Igazít_vízszintes(MyE.Oszlopnév(12 + j) + $"{sor}", "közép");
                    MyE.Betű(MyE.Oszlopnév(12 + j) + $"{sor}", false, true, true);
                }
            }
            MyE.Rácsoz("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Vastagkeret("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");

            // ********************************************
            // ide a Főjavítás vége 
            // ********************************************


            sor++;
            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + $"{sor}" + ":" + MyE.Oszlopnév(11) + $"{sor}");
            MyE.Kiir("Összesen:", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Igazít_vízszintes(MyE.Oszlopnév(1) + $"{sor}", "jobb");
            MyE.Betű(MyE.Oszlopnév(1) + $"{sor}", false, true, true);

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyE.Kiir((forgalombanösszesen[j] + tartalék[j] + javításon[j] + félreállítás[j] + főjavítás[j] + telepenkívül[j] + személyzet[j]).ToString(), MyE.Oszlopnév(12 + j) + $"{sor}");
                    MyE.Igazít_vízszintes(MyE.Oszlopnév(12 + j) + $"{sor}", "közép");
                    MyE.Betű(MyE.Oszlopnév(12 + j) + $"{sor}", false, true, true);
                }
            }

            MyE.Rácsoz("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Vastagkeret("a" + $"{sor}" + ":" + MyE.Oszlopnév(utolsó - 1) + $"{sor}");
            MyE.Oszlopszélesség(munkalap, "A:A");

            // *******************************************
            // **********A táblázat jobb oldala***********
            // *******************************************

            újsor = 1;
            utolsó++;
            MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(utolsó) + ":" + MyE.Oszlopnév(utolsó + 14), 10);
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyE.Kiir("Kocsiállomány Jelentés", MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Betű(MyE.Oszlopnév(utolsó) + újsor.ToString(), 20);
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 9) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Kiir(szövegd, MyE.Oszlopnév(utolsó + 9) + újsor.ToString());
            MyE.Betű(MyE.Oszlopnév(utolsó + 9) + újsor.ToString(), 20);

            újsor += 1;
            Jobb_kategória("II. Események");

            újsor += 1;
            Menet_fejléc();

            újsor += 3;

            // egyesítjük kettesével
            // itt kell majd beolvasni a menetkimaradásokat.
            hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\főkönyv\menet" + Dátum.ToString("yyyy") + ".mdb";

            if (!File.Exists(hely))
                Adatbázis_Létrehozás.Menekimaradás_telephely(hely);
            int napia = 0;
            int napib = 0;
            int napic = 0;
            int napi = 0;
            jelszó = "lilaakác";

            szöveg = "SELECT * FROM menettábla where [bekövetkezés]>=#" + Dátum.ToString("M-d-yy") + " 00:00:0#";
            szöveg += " and [bekövetkezés]<#" + Dátum.ToString("M-d-yy") + " 23:59:0#";
            szöveg += " and [törölt]=0";
            szöveg += " order by eseményjele";
            Kezelő_Menetkimaradás KM_kéz = new Kezelő_Menetkimaradás();
            List<Adat_Menetkimaradás> Madatok = KM_kéz.Lista_Adatok(hely, jelszó, szöveg);


            // van menetkimaradás

            if (Madatok.Count != 0)
            {
                foreach (Adat_Menetkimaradás rekord in Madatok)
                {
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó) + (újsor + 1).ToString());
                    MyE.Kiir(rekord.Eseményjele.ToUpper(), MyE.Oszlopnév(utolsó) + újsor.ToString());
                    switch (rekord.Eseményjele.ToUpper())
                    {
                        case "A":
                            {
                                napia += 1;
                                break;
                            }
                        case "B":
                            {
                                napib += 1;
                                break;
                            }
                        case "C":
                            {
                                napic += 1;
                                break;
                            }
                    }
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 1) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 1) + (újsor + 1).ToString());
                    MyE.Kiir(rekord.Viszonylat.Trim(), MyE.Oszlopnév(utolsó + 1) + újsor.ToString());

                    MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 2) + (újsor + 1).ToString());
                    MyE.Kiir(rekord.Típus.Trim(), MyE.Oszlopnév(utolsó + 2) + újsor.ToString());

                    MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 3) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 3) + (újsor + 1).ToString());
                    MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(utolsó + 3) + újsor.ToString());

                    MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 12) + (újsor + 1).ToString());
                    MyE.Sortörésseltöbbsorba(MyE.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 12) + (újsor + 1).ToString(), true);

                    MyE.Kiir(rekord.Jvbeírás.Trim() + " - " + rekord.Vmbeírás.Trim() + "-" + rekord.Javítás.Trim(), MyE.Oszlopnév(utolsó + 4) + újsor.ToString());

                    MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 13) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 13) + (újsor + 1).ToString());
                    MyE.Kiir(rekord.Bekövetkezés.ToString("hh: mm"), MyE.Oszlopnév(utolsó + 13) + újsor.ToString());

                    MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 14) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + (újsor + 1).ToString());
                    MyE.Kiir(rekord.Kimaradtmenet.ToString(), MyE.Oszlopnév(utolsó + 14) + újsor.ToString());

                    napi += Convert.ToInt32(rekord.Kimaradtmenet);

                    MyE.Rácsoz(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + (újsor + 1).ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + (újsor + 1).ToString());
                    újsor += 2;
                }
            }
            else
            {
                // nincs adat
                MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó) + (újsor + 1).ToString());
                MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 1) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 1) + (újsor + 1).ToString());
                MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 2) + (újsor + 1).ToString());
                MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 3) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 3) + (újsor + 1).ToString());
                MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 12) + (újsor + 1).ToString());
                MyE.Kiir("Nincs adat", MyE.Oszlopnév(utolsó + 4) + újsor.ToString());
                MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 13) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 13) + (újsor + 1).ToString());
                MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 14) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + (újsor + 1).ToString());
                MyE.Rácsoz(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + (újsor + 1).ToString());
                MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + (újsor + 1).ToString());
                újsor += 2;
            }

            // göngyölt ABC és menet
            int göngya = 0;
            int göngyb = 0;
            int göngyc = 0;
            int göngymenet = 0;

            szöveg = "SELECT * FROM menettábla where ";
            szöveg += "[bekövetkezés]>=#" + Dátum.ToString("MM") + "-1-" + Dátum.ToString("yyyy") + " 00:00:0#";
            szöveg += " and [bekövetkezés]<#" + Dátum.ToString("MM-d-yyyy") + " 23:59:0#";
            szöveg += " and [törölt]=0";

            Madatok = KM_kéz.Lista_Adatok(hely, jelszó, szöveg);

            foreach (Adat_Menetkimaradás rekord in Madatok)
            {

                switch (rekord.Eseményjele.ToUpper())
                {
                    case "A":
                        {
                            göngya += 1;
                            break;
                        }
                    case "B":
                        {
                            göngyb += 1;
                            break;
                        }
                    case "C":
                        {
                            göngyc += 1;
                            break;
                        }
                }
                göngymenet += Convert.ToInt32(rekord.Kimaradtmenet);
            }


            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyE.Kiir("Napi \"A\"", MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyE.Kiir(napia.ToString(), MyE.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyE.Kiir("Göngyölt \"A\"", MyE.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 6) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyE.Kiir(göngya.ToString(), MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 8) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 13) + újsor.ToString());
            MyE.Kiir("Napi összes kimaradt menet:", MyE.Oszlopnév(utolsó + 8) + újsor.ToString());
            MyE.Kiir(napi.ToString(), MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Rácsoz(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());

            újsor += 1;
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyE.Kiir("Napi \"B\"", MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyE.Kiir(napib.ToString(), MyE.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyE.Kiir("Göngyölt \"B\"", MyE.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 6) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyE.Kiir(göngyb.ToString(), MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 8) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 13) + újsor.ToString());
            MyE.Kiir("Göngyölt összes kimaradt menet:", MyE.Oszlopnév(utolsó + 8) + újsor.ToString());
            MyE.Kiir(göngymenet.ToString(), MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Rácsoz(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());

            újsor += 1;
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyE.Kiir("Napi \"C\"", MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyE.Kiir(napic.ToString(), MyE.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyE.Kiir("Göngyölt \"C\"", MyE.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 6) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyE.Kiir(göngyc.ToString(), MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 8) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Rácsoz(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());

            újsor += 1;
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyE.Kiir("Összesen:", MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyE.Kiir((napia + napib + napic).ToString(), MyE.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyE.Kiir("Összesen:", MyE.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 6) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyE.Kiir((göngya + göngyb + göngyc).ToString(), MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 8) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Rácsoz(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());

            // ****************************************
            // ******* Napi változások  kezdete 
            // ****************************************

            újsor += 1;
            Jobb_kategória("III. Napi Változások");
            Jobb_Napiváltozások_fejléc();
            Jobb_NapiVáltozások(Cmbtelephely, Dátum);

            // ****************************************
            // ******* Eltérések eleje   **************
            // ****************************************

            újsor += 1;
            Jobb_kategória("IV. Típus cserék");
            újsor += 1;
            Jobb_Típuscsere_fejléc();
            Jobb_Típuscsere(Cmbtelephely, Dátum);

            // ****************************************
            // ******* Személyzet hiány  eleje*********
            // ****************************************

            újsor += 1;
            Jobb_kategória("V. Személyzet hiány");
            Jobb_Személyzet_Fejléc();
            Jobb_Személyzet_Fejléc(Cmbtelephely, Dátum);

            // ****************************************
            // ******* Tervezett karbantartás eleje****
            // ****************************************

            újsor += 1;
            Jobb_kategória("VI. Tervezett Karbantartás");
            Jobb_Tervezet_fejléc();
            Jobb_Tervezet(Cmbtelephely, Dátum);

            // **************************************************
            // ******* Állomány változás eleje*******************
            // **************************************************
            // ******* Ez jól láthatóan nem lett megírva ********
            // **************************************************


            újsor++;
            Jobb_kategória("VII. Telephelyek közötti kocsi cserék");
            újsor++;
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 7) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Igazít_vízszintes(MyE.Oszlopnév(utolsó) + újsor.ToString(), "közép");
            MyE.Kiir("Érkező járművek", MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Igazít_vízszintes(MyE.Oszlopnév(utolsó + 7) + újsor.ToString(), "közép");
            MyE.Kiir("Átadott járművek", MyE.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó + 7) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            újsor++;
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 1) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 8) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 10) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 11) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Kiir("Psz", MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Kiir("Típus", MyE.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyE.Kiir("Telephely", MyE.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyE.Kiir("Psz", MyE.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyE.Kiir("Típus", MyE.Oszlopnév(utolsó + 8) + újsor.ToString());
            MyE.Kiir("Telephely", MyE.Oszlopnév(utolsó + 11) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó + 7) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            // **************************************************
            // ******* Aláírás helyek                    ********
            // **************************************************

            újsor += 2;
            MyE.Kiir("Kiállította:", MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Kiir("Ellenőrizte:", MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyE.Kiir("Látta:", MyE.Oszlopnév(utolsó + 12) + újsor.ToString());
            újsor += 3;
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 6) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 8) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 12) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó) + (újsor + 1).ToString() + ":" + MyE.Oszlopnév(utolsó + 2) + (újsor + 1).ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 6) + (újsor + 1).ToString() + ":" + MyE.Oszlopnév(utolsó + 8) + (újsor + 1).ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 12) + (újsor + 1).ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + (újsor + 1).ToString());
            // MyE.Kiirjuk a készítő nevét és beosztását
            hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\Dolgozók.mdb";
            jelszó = "forgalmiutasítás";
            szöveg = "SELECT * FROM dolgozóadatok where [Bejelentkezésinév]='" + kicsinálta.Trim() + "'";

            Kezelő_Dolgozó_Alap kézDolg = new Kezelő_Dolgozó_Alap();
            Adat_Dolgozó_Alap Adat = kézDolg.Egy_Adat(hely, jelszó, szöveg);
            string dolgozónév = "_";
            string főkönyvtitulus = "_";

            if (Adat != null)
            {
                dolgozónév = Adat.DolgozóNév == null ? "_" : Adat.DolgozóNév.Trim();
                főkönyvtitulus = Adat.Főkönyvtitulus == null ? "_" : Adat.Főkönyvtitulus.Trim();
            }


            MyE.Kiir(dolgozónév, MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Kiir(főkönyvtitulus, MyE.Oszlopnév(utolsó) + (újsor + 1).ToString());

            // MyE.Kiirjuk a személyeket az ellenőrző személyeket
            hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\segéd\kiegészítő.mdb";
            jelszó = "Mocó";

            szöveg = "SELECT * FROM főkönyvtábla order by id";
            int ii = 6;

            Kezelő_Kiegészítő_főkönyvtábla KKF_kéz = new Kezelő_Kiegészítő_főkönyvtábla();
            List<Adat_Kiegészítő_főkönyvtábla> adatok = KKF_kéz.Lista_Adatok(hely, jelszó, szöveg);

            foreach (Adat_Kiegészítő_főkönyvtábla rekord in adatok)
            {
                MyE.Kiir(rekord.Név.Trim(), MyE.Oszlopnév(utolsó + ii) + újsor.ToString());
                MyE.Kiir(rekord.Beosztás.Trim(), MyE.Oszlopnév(utolsó + ii) + (újsor + 1).ToString());
                ii += 6;
            }


            MyE.Betű(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + (újsor + 1).ToString(), 10);
            MyE.Betű(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + (újsor + 1).ToString(), false, false, true);

            újsor += 3;

            MyE.Kiir("Jelölés magyarázat:", MyE.Oszlopnév(utolsó) + újsor.ToString());
            újsor += 1;
            MyE.Kiir("1111", MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Háttérszín(MyE.Oszlopnév(utolsó) + újsor.ToString(), System.Drawing.Color.Orange);
            MyE.Kiir("Beálló", MyE.Oszlopnév(utolsó + 1) + újsor.ToString());

            MyE.Kiir("2222", MyE.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyE.Háttérszín(MyE.Oszlopnév(utolsó + 2) + újsor.ToString(), System.Drawing.Color.Orange);
            MyE.Betű(MyE.Oszlopnév(utolsó + 2) + újsor.ToString(), false, true, true);
            MyE.Kiir("Beálló és a műszak bekérte", MyE.Oszlopnév(utolsó + 3) + újsor.ToString());

            MyE.Kiir("3333", MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyE.Háttérszín(MyE.Oszlopnév(utolsó + 6) + újsor.ToString(), System.Drawing.Color.GreenYellow);
            MyE.Betű(MyE.Oszlopnév(utolsó + 6) + újsor.ToString(), false, false, true);
            MyE.Kiir("Személyzet hiány", MyE.Oszlopnév(utolsó + 7) + újsor.ToString());

            MyE.Kiir("4444", MyE.Oszlopnév(utolsó + 9) + újsor.ToString());
            MyE.Betű(MyE.Oszlopnév(utolsó + 9) + újsor.ToString(), false, false, true);
            MyE.Háttérszín(MyE.Oszlopnév(utolsó + 9) + újsor.ToString(), System.Drawing.Color.Red);
            MyE.Betű(MyE.Oszlopnév(utolsó + 9) + újsor.ToString(), System.Drawing.Color.Yellow);
            MyE.Kiir("Üzemképtelen", MyE.Oszlopnév(utolsó + 10) + újsor.ToString());

            MyE.Kiir("5555", MyE.Oszlopnév(utolsó + 12) + újsor.ToString());
            MyE.Háttérszín(MyE.Oszlopnév(utolsó + 12) + újsor.ToString(), System.Drawing.Color.LightSkyBlue);
            MyE.Betű(MyE.Oszlopnév(utolsó + 12) + újsor.ToString(), true, false, true);
            MyE.Kiir("Többlet kiadás", MyE.Oszlopnév(utolsó + 13) + újsor.ToString());

            //***************************************
            //*Nyomtatási beállítások
            //***************************************
            if (sor > újsor)
                újsor = sor;
            MyE.NyomtatásiTerület_részletes(munkalap, "A1:" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString(),
                0.196850393700787d, 0.196850393700787d, 0.196850393700787d, 0.196850393700787d,
                0.31496062992126d, 0.31496062992126d, "1", "1", false, "A3", true, true);

            MyE.Aktív_Cella(munkalap, "A1");
            MyE.ExcelMentés(fájlexc);
            MyE.ExcelBezárás();
            MyE.Megnyitás(fájlexc);
        }

        private void Jobb_Tervezet(string Cmbtelephely, DateTime Dátum)
        {

            string helykieg = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\segéd\Kiegészítő.mdb";


            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\hibanapló" + @"\" + Dátum.ToString("yyyyMM") + "hibanapló.mdb";
            string jelszó = "pozsgaii";
            int mennyi;
            if (System.IO.File.Exists(hely))
            {

                // csak azokat listázzuk amik be vannak jelölve
                string szöveg = "SELECT * FROM hibaterv WHERE főkönyv = true ORDER BY id";
                Kezelő_jármű_hiba KJH_kéz = new Kezelő_jármű_hiba();
                List<Adat_Jármű_hiba> Adatok;

                Kezelő_kiegészítő_Hibaterv KKH_kéz = new Kezelő_kiegészítő_Hibaterv();
                List<Adat_Kiegészítő_Hibaterv> KiAdatokÖ = KKH_kéz.Lista_Adatok(Cmbtelephely.Trim());
                List<Adat_Kiegészítő_Hibaterv> KiAdatok = (from a in KiAdatokÖ
                                                           where a.Főkönyv == true
                                                           select a).ToList();

                foreach (Adat_Kiegészítő_Hibaterv rekordkieg in KiAdatok)
                {
                    újsor += 1;
                    mennyi = 3;
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 3) + újsor.ToString());
                    MyE.Kiir(rekordkieg.Szöveg.Trim(), MyE.Oszlopnév(utolsó) + újsor.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 3) + újsor.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
                    szöveg = "";
                    szöveg = "SELECT * FROM hibatábla where idő>=#" + Dátum.ToString("MM-dd-yyyy") + " 06:00:0#";
                    szöveg += " and idő<#" + Dátum.AddDays(1).ToString("MM-dd-yyyy") + " 06:00:0#";
                    szöveg += " and javítva=true";
                    szöveg += " order by azonosító";
                    Adatok = KJH_kéz.Lista_adatok(hely, jelszó, szöveg);
                    foreach (Adat_Jármű_hiba rekord in Adatok)
                    {
                        if (rekord.Hibaleírása.Contains(rekordkieg.Szöveg.Trim()))
                        {
                            mennyi += 1;
                            if (mennyi == 15)
                            {
                                MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 3) + újsor.ToString());
                                MyE.Vastagkeret(MyE.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
                                újsor += 1;
                                MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 3) + újsor.ToString());
                                MyE.Kiir(rekordkieg.Szöveg.Trim(), MyE.Oszlopnév(utolsó) + újsor.ToString());
                                mennyi = 4;
                            }
                            MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(utolsó + mennyi) + újsor.ToString());
                        }
                    }

                    MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 3) + újsor.ToString());
                    MyE.Rácsoz(MyE.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
                    mennyi = 4;
                }
            }
        }

        private void Jobb_Tervezet_fejléc()
        {

            újsor += 1;
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Kiir("Karbantartás", MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Igazít_vízszintes(MyE.Oszlopnév(utolsó + 4) + újsor.ToString(), "közép");
            MyE.Kiir("Pályaszám(ok)", MyE.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó + 4) + újsor.ToString());
        }

        private void Jobb_Személyzet_Fejléc(string Cmbtelephely, DateTime Dátum)
        {
            // megnézzük, hogy volt-e személyzet hiány ezen a napon

            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\főkönyv\személyzet" + Dátum.ToString("yyyy") + ".mdb";
            string jelszó = "plédke";
            string szöveg = "SELECT * FROM tábla where [dátum]=#" + Dátum.ToString("MM-dd-yyyy") + "#";
            szöveg += " order by napszak, típus";

            Kezelő_Főkönyv_Személyzet KFS_kéz = new Kezelő_Főkönyv_Személyzet();
            List<Adat_Főkönyv_Személyzet> SzAdatok = KFS_kéz.Lista_adatok(hely, jelszó, szöveg);
            int egyik = 1;
            foreach (Adat_Főkönyv_Személyzet rekord in SzAdatok)
            {

                switch (egyik)
                {
                    case 1:
                        {
                            újsor += 1;
                            MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 4) + újsor.ToString());
                            MyE.Vastagkeret(MyE.Oszlopnév(utolsó + 5) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 9) + újsor.ToString());
                            MyE.Vastagkeret(MyE.Oszlopnév(utolsó + 10) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
                            MyE.Kiir(rekord.Viszonylat.Trim() + "/" + rekord.Forgalmiszám.Trim(), MyE.Oszlopnév(utolsó) + újsor.ToString());
                            MyE.Kiir(rekord.Tervindulás.ToString("hh: mm"), MyE.Oszlopnév(utolsó + 1) + újsor.ToString());
                            MyE.Kiir(rekord.Típus.Trim(), MyE.Oszlopnév(utolsó + 2) + újsor.ToString());
                            MyE.Kiir(rekord.Napszak.Trim(), MyE.Oszlopnév(utolsó + 3) + újsor.ToString());
                            MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(utolsó + 4) + újsor.ToString());
                            break;
                        }
                    case 2:
                        {
                            MyE.Kiir(rekord.Viszonylat.Trim() + "/" + rekord.Forgalmiszám.Trim(), MyE.Oszlopnév(utolsó + 5) + újsor.ToString());
                            MyE.Kiir(rekord.Tervindulás.ToString("hh: mm"), MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
                            MyE.Kiir(rekord.Típus.Trim(), MyE.Oszlopnév(utolsó + 7) + újsor.ToString());
                            MyE.Kiir(rekord.Napszak.Trim(), MyE.Oszlopnév(utolsó + 8) + újsor.ToString());
                            MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(utolsó + 9) + újsor.ToString());
                            break;
                        }
                    case 3:
                        {
                            MyE.Kiir(rekord.Viszonylat.Trim() + "/" + rekord.Forgalmiszám.Trim(), MyE.Oszlopnév(utolsó + 10) + újsor.ToString());
                            MyE.Kiir(rekord.Tervindulás.ToString("hh: mm"), MyE.Oszlopnév(utolsó + 11) + újsor.ToString());
                            MyE.Kiir(rekord.Típus.ToString(), MyE.Oszlopnév(utolsó + 12) + újsor.ToString());
                            MyE.Kiir(rekord.Napszak.Trim(), MyE.Oszlopnév(utolsó + 13) + újsor.ToString());
                            MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
                            break;
                        }
                }
                egyik++;
                if (egyik == 4) egyik = 1;
            }
        }

        private void Jobb_Személyzet_Fejléc()
        {

            újsor += 1;
            MyE.Kiir("Visz./forg.", MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Kiir("Ind. idő", MyE.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyE.Kiir("Típus", MyE.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyE.Kiir("Napszak", MyE.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyE.Kiir("Psz", MyE.Oszlopnév(utolsó + 4) + újsor.ToString());

            MyE.Kiir("Visz./forg.", MyE.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyE.Kiir("Ind. idő", MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyE.Kiir("Típus", MyE.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyE.Kiir("Napszak", MyE.Oszlopnév(utolsó + 8) + újsor.ToString());
            MyE.Kiir("Psz", MyE.Oszlopnév(utolsó + 9) + újsor.ToString());

            MyE.Kiir("Visz./forg.", MyE.Oszlopnév(utolsó + 10) + újsor.ToString());
            MyE.Kiir("Ind. idő", MyE.Oszlopnév(utolsó + 11) + újsor.ToString());
            MyE.Kiir("Típus", MyE.Oszlopnév(utolsó + 12) + újsor.ToString());
            MyE.Kiir("Napszak", MyE.Oszlopnév(utolsó + 13) + újsor.ToString());
            MyE.Kiir("Psz", MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó + 5) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 9) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó + 10) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
        }

        private void Jobb_Típuscsere(string Cmbtelephely, DateTime Dátum)
        {

            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\főkönyv\típuscsere" + Dátum.ToString("yyyy") + ".mdb";
            string jelszó = "plédke";
            string szöveg = "SELECT * FROM típuscseretábla where [dátum]=#" + Dátum.ToString("MM-dd-yyyy") + "# order by napszak, típuselőírt";
            Kezelő_Főkönyv_Típuscsere KFT_kéz = new Kezelő_Főkönyv_Típuscsere();
            List<Adat_FőKönyv_Típuscsere> Adatok = KFT_kéz.Lista_adatok(hely, jelszó, szöveg);

            int ik = 2;
            foreach (Adat_FőKönyv_Típuscsere rekord in Adatok)
            {
                if (ik == 1)
                {
                    // ha páros
                    MyE.Kiir(rekord.Viszonylat.Trim() + "/" + rekord.Forgalmiszám.Trim(), MyE.Oszlopnév(utolsó + 8) + újsor.ToString());
                    MyE.Kiir(rekord.Tervindulás.ToString("hh: mm"), MyE.Oszlopnév(utolsó + 9) + újsor.ToString());
                    MyE.Kiir(rekord.Típuselőírt.Trim(), MyE.Oszlopnév(utolsó + 10) + újsor.ToString());
                    MyE.Kiir(rekord.Típuskiadott.Trim(), MyE.Oszlopnév(utolsó + 12) + újsor.ToString());
                    MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
                    ik = 2;
                }
                else
                {
                    // ha páratlan
                    újsor += 1;
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 3) + újsor.ToString());
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 5) + újsor.ToString());
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 10) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 11) + újsor.ToString());
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 12) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 13) + újsor.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 7) + újsor.ToString());
                    MyE.Vastagkeret(MyE.Oszlopnév(utolsó + 8) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
                    MyE.Kiir(rekord.Viszonylat.Trim() + "/" + rekord.Forgalmiszám.Trim(), MyE.Oszlopnév(utolsó) + újsor.ToString());
                    MyE.Kiir(rekord.Tervindulás.ToString("hh: mm"), MyE.Oszlopnév(utolsó + 1) + újsor.ToString());
                    MyE.Kiir(rekord.Típuselőírt.Trim(), MyE.Oszlopnév(utolsó + 2) + újsor.ToString());
                    MyE.Kiir(rekord.Típuskiadott.Trim(), MyE.Oszlopnév(utolsó + 4) + újsor.ToString());
                    MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
                    ik = 1;
                }

            }

        }

        private void Jobb_Típuscsere_fejléc()
        {
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 10) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 11) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 12) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 13) + újsor.ToString());
            MyE.Kiir("Visz./forg.", MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Kiir("Visz./forg.", MyE.Oszlopnév(utolsó + 8) + újsor.ToString());
            MyE.Kiir("Ind. idő", MyE.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyE.Kiir("Ind. idő", MyE.Oszlopnév(utolsó + 9) + újsor.ToString());
            MyE.Kiir("Előírt típus", MyE.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyE.Kiir("Előírt típus", MyE.Oszlopnév(utolsó + 10) + újsor.ToString());
            MyE.Kiir("Kiadott típus", MyE.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyE.Kiir("kiadott típus", MyE.Oszlopnév(utolsó + 12) + újsor.ToString());
            MyE.Kiir("Psz", MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyE.Kiir("Psz", MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó + 8) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
        }

        private void Jobb_NapiVáltozások(string Cmbtelephely, DateTime Dátum)
        {

            // ha létezik a fájl akkor készít

            int álldb = 0;
            int készdb = 0;
            int darab;

            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\hibanapló\Elkészült{Dátum.Year}.mdb";
            string jelszó = "plédke";
            string szöveg = "SELECT * FROM xnapostábla ";
            szöveg += $" where kezdődátum= #{Dátum:MM-dd-yyyy}#";
            szöveg += " order by azonosító";
            //ELKÉSZÜLT  darabszám meghatározása
            if (File.Exists(hely))
            {
                Kezelő_Jármű_Xnapos Kéz = new Kezelő_Jármű_Xnapos();
                List<Adat_Jármű_Xnapos> AdatokX = Kéz.Lista_Adatok(hely, jelszó, szöveg);
                if (AdatokX != null) álldb = AdatokX.Count;
            }



            hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\hibanapló\napi.mdb";
            if (System.IO.File.Exists(hely))
            {
                szöveg = "SELECT * FROM xnapostábla ";
                szöveg += " where kezdődátum >= #" + Dátum.ToString("MM-dd-yyyy") + " 00:00:0" + "#";
                szöveg += " AND kezdődátum <= #" + Dátum.ToString("MM-dd-yyyy") + " 23:59:59" + "#";
                szöveg += " order by azonosító";
                Kezelő_Jármű_Xnapos Kéz = new Kezelő_Jármű_Xnapos();
                List<Adat_Jármű_Xnapos> AdatokX = Kéz.Lista_Adatok(hely, jelszó, szöveg);
                if (AdatokX != null) álldb = AdatokX.Count;
            }

            hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\hibanapló\Elkészült{Dátum.Year}.mdb";
            if (File.Exists(hely))
            {
                szöveg = "SELECT * FROM xnapostábla ";
                szöveg += " where végdátum >= #" + Dátum.ToString("MM-dd-yyyy") + " 00:00:0" + "#";
                szöveg += " AND végdátum <= #" + Dátum.ToString("MM-dd-yyyy") + " 23:59:59" + "#";
                szöveg += " order by azonosító";
                Kezelő_Jármű_Xnapos Kéz = new Kezelő_Jármű_Xnapos();
                List<Adat_Jármű_Xnapos> AdatokX = Kéz.Lista_Adatok(hely, jelszó, szöveg);
                if (AdatokX != null) készdb = AdatokX.Count;
            }

            if (készdb > álldb)
                darab = készdb;
            else
                darab = álldb;

            // elkészítjük az üres táblázatot
            for (int i = 1; i <= darab; i++)
            {
                MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 1) + (újsor + i).ToString() + ":" + MyE.Oszlopnév(utolsó + 5) + (újsor + i).ToString());
                MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 7) + (újsor + i).ToString() + ":" + MyE.Oszlopnév(utolsó + 8) + (újsor + i).ToString());
                MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 10) + (újsor + i).ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + (újsor + i).ToString());
                MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + (újsor + i).ToString() + ":" + MyE.Oszlopnév(utolsó + 5) + (újsor + i).ToString());
                MyE.Vastagkeret(MyE.Oszlopnév(utolsó + 6) + (újsor + i).ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + (újsor + i).ToString());
            }

            hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\hibanapló\Elkészült{Dátum.Year}.mdb";
            szöveg = "SELECT * FROM xnapostábla ";
            szöveg += " where végdátum >= #" + Dátum.ToString("MM-dd-yyyy") + " 00:00:0" + "#";
            szöveg += " AND végdátum <= #" + Dátum.ToString("MM-dd-yyyy") + " 23:59:59" + "#";
            szöveg += " order by azonosító";


            Kezelő_Jármű_Xnapos KJX_kéz = new Kezelő_Jármű_Xnapos();
            List<Adat_Jármű_Xnapos> Adatok = KJX_kéz.Lista_Adatok(hely, jelszó, szöveg);


            int ji = 1;
            foreach (Adat_Jármű_Xnapos rekord in Adatok)
            {
                MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(utolsó + 6) + (újsor + ji).ToString());
                MyE.Kiir(rekord.Kezdődátum.ToString("yyyy.MM.dd"), MyE.Oszlopnév(utolsó + 7) + (újsor + ji).ToString());
                MyE.Kiir((rekord.Végdátum - rekord.Kezdődátum).Days.ToString(), MyE.Oszlopnév(utolsó + 9) + (újsor + ji).ToString());
                MyE.Kiir(rekord.Hibaleírása.Trim(), MyE.Oszlopnév(utolsó + 10) + (újsor + ji).ToString());
                ji += 1;
            }

            szöveg = "SELECT * FROM xnapostábla ";
            szöveg += " where kezdődátum = #" + Dátum.ToString("MM-dd-yyyy") + "#";
            szöveg += " order by azonosító";

            Adatok = KJX_kéz.Lista_Adatok(hely, jelszó, szöveg);

            ji = 1;

            foreach (Adat_Jármű_Xnapos rekord in Adatok)
            {
                MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(utolsó) + (újsor + ji).ToString());
                MyE.Kiir(rekord.Hibaleírása.Trim(), MyE.Oszlopnév(utolsó + 1) + (újsor + ji).ToString());
                ji += 1;
            }

            hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\hibanapló\napi.mdb";
            jelszó = "plédke";

            if (System.IO.File.Exists(hely))
            {

                szöveg = "SELECT * FROM xnapostábla ";
                szöveg += " where kezdődátum= #" + Dátum.ToString("MM-dd-yyyy") + "#";
                szöveg += " order by azonosító";

                Adatok = KJX_kéz.Lista_Adatok(hely, jelszó, szöveg);


                foreach (Adat_Jármű_Xnapos rekord in Adatok)
                {
                    MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(utolsó) + (újsor + ji).ToString());
                    MyE.Kiir(rekord.Hibaleírása.Trim(), MyE.Oszlopnév(utolsó + 1) + (újsor + ji).ToString());
                    ji += 1;
                }
            }
            újsor += darab;
        }

        private void Jobb_Napiváltozások_fejléc()
        {

            újsor += 1;
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyE.Kiir("Leálló Kocsik", MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 6) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Kiir("Elkészült kocsik", MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó + 6) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());

            újsor += 1;
            MyE.Kiir("Psz", MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Kiir("Psz", MyE.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 1) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyE.Kiir("Oka", MyE.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 7) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 8) + újsor.ToString());
            MyE.Kiir("Mióta", MyE.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyE.Kiir("Állás Nap", MyE.Oszlopnév(utolsó + 9) + újsor.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 10) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Kiir("Oka", MyE.Oszlopnév(utolsó + 10) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó + 6) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
        }

        private void Menet_fejléc()
        {
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó) + (újsor + 2).ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 1) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 1) + (újsor + 2).ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 2) + (újsor + 2).ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 3) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 3) + (újsor + 2).ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 12) + (újsor + 2).ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 13) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 13) + (újsor + 2).ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó + 14) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + (újsor + 2).ToString());
            MyE.Rácsoz(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + (újsor + 2).ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + (újsor + 2).ToString());


            MyE.Kiir("Esemény jele", MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Kiir("Viszonylat", MyE.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyE.Kiir("Típus", MyE.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyE.Sortörésseltöbbsorba(MyE.Oszlopnév(utolsó + 3) + újsor.ToString(), true);
            MyE.Kiir("Meghibásodott jármű pályaszáma", MyE.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyE.Kiir("Forgalmi esemény vagy járműhiba rövid leírása", MyE.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyE.Sortörésseltöbbsorba(MyE.Oszlopnév(utolsó + 13) + újsor.ToString(), true);
            MyE.Kiir("Esemény időpontja ", MyE.Oszlopnév(utolsó + 13) + újsor.ToString());
            MyE.Sortörésseltöbbsorba(MyE.Oszlopnév(utolsó + 14) + újsor.ToString(), true);
            MyE.Kiir("Kimaradt menetek száma", MyE.Oszlopnév(utolsó + 14) + újsor.ToString());

            MyE.Betű(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + (újsor + 2), 8);

        }

        private void Jobb_kategória(string név)
        {
            MyE.Egyesít(munkalap, MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Igazít_vízszintes(MyE.Oszlopnév(utolsó) + újsor.ToString(), "közép");
            MyE.Kiir(név, MyE.Oszlopnév(utolsó) + újsor.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyE.Háttérszín(MyE.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyE.Oszlopnév(utolsó + 14) + újsor.ToString(), System.Drawing.Color.GreenYellow);
        }
    }

    public class Főkönyv_Háromnapos
    {
        public void Három_Nyomtatvány(string fájlneve, string Cmbtelephely, string papírméret, string papírelrendezés)
        {

            MyE.ExcelLétrehozás();

            MyE.Új_munkalap("Munka2");
            MyE.Új_munkalap("Munka3");

            string[] mit = { "Hétfő-Csütörtök", "Kedd-Péntek", "Szerda-Szombat" };

            // kiírjuk a kocsikat

            string helyhiba = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\villamos\hiba.mdb";
            string jelszóhiba = "pozsgaii";
            string szöveg = "SELECT * FROM hibatábla ";
            Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();
            List<Adat_Jármű_hiba> AdatokHiba = KézHiba.Lista_adatok(helyhiba, jelszóhiba, szöveg);

            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\villamos\villamos2.mdb";
            string jelszó = "pozsgaii";
            szöveg = $"SELECT * FROM állománytábla";

            Kezelő_Jármű2 KézJármű = new Kezelő_Jármű2();
            List<Adat_Jármű_2> AdatokHárom = KézJármű.Lista_Adatok(hely, jelszó, szöveg);

            for (int j = 1; j <= 3; j++)
            {
                List<Adat_Jármű_2> AdatokSzűrt = (from a in AdatokHárom
                                                  where a.Haromnapos == j
                                                  orderby a.Azonosító ascending
                                                  select a).ToList();
                string munkalap = "Munka" + j;
                MyE.Munkalap_aktív(munkalap);
                MyE.Munkalap_betű("Arial", 12);

                int sor = 1;
                int oszlop = 1;
                int i = 1;

                foreach (Adat_Jármű_2 rekord in AdatokSzűrt)
                {
                    if (sor == 1)
                    {
                        // elkészítjük a fejlécet
                        MyE.Kiir("psz", MyE.Oszlopnév(oszlop) + $"{sor}");
                        MyE.Kiir("Hiba", MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                        MyE.Kiir("Nappal", MyE.Oszlopnév(oszlop + 2) + $"{sor}");
                        MyE.Kiir("Éjszaka", MyE.Oszlopnév(oszlop + 3) + $"{sor}");
                        sor += 1;
                    }
                    MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");
                    List<Adat_Jármű_hiba> ElemekHiba = (from a in AdatokHiba
                                                        where a.Azonosító == rekord.Azonosító
                                                        orderby a.Korlát descending
                                                        select a).ToList();
                    string üzemképtelen = "";
                    string beálló = "";
                    if (ElemekHiba != null && ElemekHiba.Count > 0)
                    {
                        foreach (Adat_Jármű_hiba Elem in ElemekHiba)
                        {
                            if (Elem.Korlát == 4) üzemképtelen += Elem.Hibaleírása;
                            if (Elem.Korlát == 3) beálló += Elem.Hibaleírása;
                        }

                        if (üzemképtelen.Trim() != "_")
                        {
                            // ha üzemképtelen
                            if (üzemképtelen.Length > 20)
                                MyE.Kiir(üzemképtelen.Substring(0, 20), MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                            else
                                MyE.Kiir(üzemképtelen, MyE.Oszlopnév(oszlop + 1) + $"{sor}");

                            MyE.Betű(MyE.Oszlopnév(oszlop + 1) + $"{sor}", false, false, true);
                        }
                        else if (beálló.Trim() != "_")
                        {
                            // ha beálló


                            if (beálló.Length > 20)
                                MyE.Kiir(beálló.Substring(0, 20), MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                            else
                                MyE.Kiir(beálló, MyE.Oszlopnév(oszlop + 1) + $"{sor}");

                            MyE.Betű(MyE.Oszlopnév(oszlop + 1) + $"{sor}", false, true, false);
                        }
                    }
                    sor += 1;
                    i += 1;

                    if (sor == 27)
                    {
                        sor = 1;
                        oszlop += 4;
                    }
                }

                //Formázzukalapokat

                for (int ii = 1; ii < oszlop + 3; ii += 4)
                {
                    MyE.Rácsoz(MyE.Oszlopnév(ii) + "1:" + MyE.Oszlopnév(ii + 3) + "26");
                    MyE.Betű(MyE.Oszlopnév(ii) + "1:" + MyE.Oszlopnév(ii) + "26", false, true, true);
                    MyE.Vastagkeret(MyE.Oszlopnév(ii) + "1:" + MyE.Oszlopnév(ii) + "26");
                    MyE.Vastagkeret(MyE.Oszlopnév(ii) + "1:" + MyE.Oszlopnév(ii + 3) + "1");
                    MyE.Vastagkeret(MyE.Oszlopnév(ii) + "1");

                }
                for (int ii = 1; ii < oszlop + 3; ii += 4)
                {
                    MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(ii) + ":" + MyE.Oszlopnév(ii + 3), 10);
                    MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(ii + 1) + ":" + MyE.Oszlopnév(ii + 1), 25);
                    MyE.Igazít_vízszintes(MyE.Oszlopnév(ii) + ":" + MyE.Oszlopnév(ii), "közép");
                }
                MyE.Sormagasság("1:26", 25);
                MyE.Betű("1:1", false, false, true);
                // nyomtatási terület
                MyE.NyomtatásiTerület_részletes(munkalap, "A1:" + MyE.Oszlopnév(oszlop + 3) + "26", "", "",
                    "&\"-,Félkövér\"&16" + mit[j - 1],
                    "&\"-,Félkövér\"&16©E2 vizsgálati",
                    "&\"Arial,Félkövér\"&16 " + DateTime.Today.ToString("yyyy.MM.dd"),
                    "&\"Arial,Normál\"&14........................................................" + '\n' + "nappalos aláírás",
                    "",
                    "&\"Arial,Normál\"&14........................................................" + '\n' + "éjszakás aláírás", "",
                    0.708661417322835d, 0.708661417322835d, 0.984251968503937d, 0.590551181102362d, 0.31496062992126d, 0.31496062992126d, true, true, "1", "1",
                    papírelrendezés != "Fekvő", papírméret);
                MyE.Aktív_Cella(munkalap, "A1");
            }
            // átnevezzük a lapokat

            MyE.Munkalap_átnevezés("Munka1", mit[0]);
            MyE.Munkalap_átnevezés("Munka2", mit[1]);
            MyE.Munkalap_átnevezés("Munka3", mit[2]);

            MyE.Munkalap_aktív("Hétfő-Csütörtök");

            MyE.ExcelMentés(fájlneve);
            MyE.ExcelBezárás();
            MyE.Megnyitás(fájlneve);
        }
    }

    public class Főkönyv_Beálló
    {
        public void Beálló_kocsik(string fájlexl, string Telephely, DateTime Dátum, string napszak, string papírméret, string papírelrendezés)
        {

            MyE.ExcelLétrehozás();
            // egész lap betű méret arial 16
            MyE.Munkalap_betű("Arial", 16);
            string munkalap = "Munka1";

            // oszlop szélességeket beállítjuk az alapot
            MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(1) + ":" + MyE.Oszlopnév(13), 8);
            MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(2) + ":" + MyE.Oszlopnév(2), 13);
            MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(9) + ":" + MyE.Oszlopnév(9), 13);
            MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(3) + ":" + MyE.Oszlopnév(3), 30);
            MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(4) + ":" + MyE.Oszlopnév(4), 30);
            MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(10) + ":" + MyE.Oszlopnév(10), 30);
            MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(11) + ":" + MyE.Oszlopnév(11), 30);
            MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(7) + ":" + MyE.Oszlopnév(7), 5);
            // sormagasság
            MyE.Sormagasság("1:100", 25);

            // Fejléc elkészítése
            MyE.Egyesít(munkalap, MyE.Oszlopnév(1) + 1.ToString() + ":" + MyE.Oszlopnév(4) + 1.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(5) + 1.ToString() + ":" + MyE.Oszlopnév(6) + 1.ToString());
            MyE.Kiir("©Beálló villamosok", MyE.Oszlopnév(1) + 1.ToString());
            MyE.Kiir(Dátum.ToString("yyyy.MM.dd"), MyE.Oszlopnév(5) + 1.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(8) + 1.ToString() + ":" + MyE.Oszlopnév(11) + 1.ToString());
            MyE.Egyesít(munkalap, MyE.Oszlopnév(12) + 1.ToString() + ":" + MyE.Oszlopnév(13) + 1.ToString());
            MyE.Kiir("©Beálló villamosok", MyE.Oszlopnév(8) + 1.ToString());
            MyE.Kiir(Dátum.ToString("yyyy.MM.dd"), MyE.Oszlopnév(12) + 1.ToString());
            MyE.Kiir("Idő", "a2");
            MyE.Kiir("Idő", "h2");
            MyE.Kiir("Visz.", "b2");
            MyE.Kiir("Visz.", "i2");
            MyE.Kiir("Milyen javításra kérték", "f2");
            MyE.Kiir("Milyen javításra kérték", "m2");
            MyE.Egyesít(munkalap, MyE.Oszlopnév(4) + 2.ToString() + ":" + MyE.Oszlopnév(6) + 2.ToString());
            MyE.Kiir("Pályaszámok", "C2");
            MyE.Egyesít(munkalap, MyE.Oszlopnév(11) + 2.ToString() + ":" + MyE.Oszlopnév(13) + 2.ToString());
            MyE.Kiir("Pályaszámok", "j2");
            // ********************************
            // tartalom kiírása
            // ********************************+

            string hely = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\főkönyv\{Dátum:yyyy}\nap\{Dátum:yyyyMMdd}" + napszak.Trim() + "nap.mdb";
            string jelszó = "lilaakác";

            string szöveg = "SELECT * FROM adattábla where Adattábla.viszonylat <> '-'  order by tényérkezés,viszonylat, forgalmiszám, azonosító ";
            Kezelő_Főkönyv_Nap FKN_kéz = new Kezelő_Főkönyv_Nap();
            List<Adat_Főkönyv_Nap> Adatok = FKN_kéz.Lista_adatok(hely, jelszó, szöveg);

            int sor = 3;
            int szerelvényhossz = 0;
            string szöveg1 = "";

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
                            MyE.Egyesít(munkalap, MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(6) + $"{sor}");
                            MyE.Egyesít(munkalap, MyE.Oszlopnév(11) + $"{sor}" + ":" + MyE.Oszlopnév(13) + $"{sor}");
                            MyE.Kiir(rekord.Tényérkezés.ToString("HH:mm"), MyE.Oszlopnév(1) + $"{sor}");
                            MyE.Kiir(rekord.Viszonylat.Trim() + "/" + rekord.Forgalmiszám.Trim(), MyE.Oszlopnév(2) + $"{sor}");
                            MyE.Kiir(szöveg, MyE.Oszlopnév(3) + $"{sor}");
                            MyE.Kiir(szöveg1.Trim(), MyE.Oszlopnév(4) + $"{sor}");
                            MyE.Kiir(rekord.Tényérkezés.ToString("HH:mm"), MyE.Oszlopnév(8) + $"{sor}");
                            MyE.Kiir(rekord.Viszonylat.Trim() + "/" + rekord.Forgalmiszám.Trim(), MyE.Oszlopnév(9) + $"{sor}");
                            MyE.Kiir(szöveg, MyE.Oszlopnév(10) + $"{sor}");
                            MyE.Kiir(szöveg1.Trim(), MyE.Oszlopnév(11) + $"{sor}");
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
                MyE.Egyesít(munkalap, MyE.Oszlopnév(4) + $"{sor}" + ":" + MyE.Oszlopnév(6) + $"{sor}");
                MyE.Egyesít(munkalap, MyE.Oszlopnév(11) + $"{sor}" + ":" + MyE.Oszlopnév(13) + $"{sor}");
                sor++;
            }

            MyE.Egyesít(munkalap, MyE.Oszlopnév(2) + $"{sor}" + ":" + MyE.Oszlopnév(3) + $"{sor}");
            MyE.Egyesít(munkalap, MyE.Oszlopnév(9) + $"{sor}" + ":" + MyE.Oszlopnév(10) + $"{sor}");
            MyE.Kiir("Vizsgálatra marad", MyE.Oszlopnév(2) + $"{sor}");
            MyE.Kiir("Vizsgálatra marad", MyE.Oszlopnév(9) + $"{sor}");
            MyE.Kiir("Vág.", MyE.Oszlopnév(1) + $"{sor}");
            MyE.Kiir("Vág.", MyE.Oszlopnév(8) + $"{sor}");
            MyE.Kiir("Vág.", MyE.Oszlopnév(5) + $"{sor}");
            MyE.Kiir("Vág.", MyE.Oszlopnév(12) + $"{sor}");
            MyE.Kiir("Visz.", MyE.Oszlopnév(6) + $"{sor}");
            MyE.Kiir("Visz.", MyE.Oszlopnév(13) + $"{sor}");
            MyE.Kiir("Tartalék", MyE.Oszlopnév(4) + $"{sor}");
            MyE.Kiir("Tartalék", MyE.Oszlopnév(11) + $"{sor}");
            for (int i = 1; i <= 9; i++)
            {
                sor++;
                MyE.Egyesít(munkalap, MyE.Oszlopnév(2) + $"{sor}" + ":" + MyE.Oszlopnév(3) + $"{sor}");
                MyE.Egyesít(munkalap, MyE.Oszlopnév(9) + $"{sor}" + ":" + MyE.Oszlopnév(10) + $"{sor}");

            }
            // idejön a vizsgálatra BM

            // keretezés
            MyE.Rácsoz(MyE.Oszlopnév(1) + 1.ToString() + ":" + MyE.Oszlopnév(6) + $"{sor}");
            MyE.Rácsoz(MyE.Oszlopnév(8) + 1.ToString() + ":" + MyE.Oszlopnév(13) + $"{sor}");
            MyE.Vastagkeret(MyE.Oszlopnév(1) + 1.ToString() + ":" + MyE.Oszlopnév(6) + $"{sor}");
            MyE.Vastagkeret(MyE.Oszlopnév(8) + 1.ToString() + ":" + MyE.Oszlopnév(13) + $"{sor}");
            MyE.Vastagkeret(MyE.Oszlopnév(1) + (sor - 9).ToString() + ":" + MyE.Oszlopnév(6) + (sor - 9).ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(8) + (sor - 9).ToString() + ":" + MyE.Oszlopnév(13) + (sor - 9).ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(1) + 1.ToString() + ":" + MyE.Oszlopnév(6) + 2.ToString());
            MyE.Vastagkeret(MyE.Oszlopnév(8) + 1.ToString() + ":" + MyE.Oszlopnév(13) + 2.ToString());


            // **********************************
            // nyomtatási beállítások
            // **********************************

            bool papírelrendez;
            if (papírelrendezés == "--")
                papírelrendez = false;
            else if (papírelrendezés == "Álló")
                papírelrendez = true;
            else
                papírelrendez = false;
            if (papírméret == "--") papírméret = "A4";

            MyE.NyomtatásiTerület_részletes(munkalap, "A1:" + MyE.Oszlopnév(13) + $"{sor}",
                0.236220472440945d, 0.236220472440945d,
                0.196850393700787d, 0.196850393700787d,
                0.31496062992126d, 0.31496062992126d, "1", "1", papírelrendez, papírméret, true, true);

            MyE.Aktív_Cella(munkalap, "A1");
            // bezárjuk az Excel-t
            MyE.ExcelMentés(fájlexl);
            MyE.ExcelBezárás();


            MyE.Megnyitás(fájlexl);
        }

    }

    public class Főkönyv_Jegykezelő
    {

        public void Jegykezelő(string fájlneve, string Cmbtelephely, List<Adat_Jármű> AdatokJármű, List<Adat_Főkönyv_Nap> AdatokFőkönyvNap, DateTime Dátum, List<string> AdatokTakarításTípus, List<Adat_Jármű_Vendég> AdatokFőVendég)
        {
            MyE.ExcelLétrehozás();
            MyE.Munkalap_betű("Arial", 20);

            // első sor állítva
            MyE.SzövegIrány("Munka1", "5:5", 90);
            MyE.Sormagasság("5:5", 150);

            DateTime napszak = new DateTime(1900, 1, 1);

            int sor = 5;
            int oszlop = 1;
            int i = 1;
            int oszlopismét = 1;

            foreach (Adat_Jármű rekord in AdatokJármű)
            {
                string Típuskell = (from a in AdatokTakarításTípus
                                    where a == rekord.Típus
                                    select a).FirstOrDefault();
                if (Típuskell != null && Típuskell.Trim() != "")
                {
                    if (sor == 5)
                    {
                        //  elkészítjük a fejlécet
                        MyE.Kiir("Psz", MyE.Oszlopnév(oszlop) + $"{sor}");
                        MyE.Kiir("Típus", MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                        MyE.Kiir("Ellenőrizve", MyE.Oszlopnév(oszlop + 2) + $"{sor}");
                        MyE.Kiir("Eltömítve", MyE.Oszlopnév(oszlop + 3) + $"{sor}");
                        MyE.Kiir("Készülék csere", MyE.Oszlopnév(oszlop + 4) + $"{sor}");
                        MyE.Kiir("Futár hiba", MyE.Oszlopnév(oszlop + 5) + $"{sor}");
                        MyE.Kiir("Festékszalag", MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                        sor += 1;
                    }
                    //  kiírjuk a pályaszámot, típust
                    MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");
                    MyE.Kiir(rekord.Típus.Trim(), MyE.Oszlopnév(oszlop + 1) + $"{sor}");

                    Adat_Főkönyv_Nap ElemNap = (from a in AdatokFőkönyvNap
                                                where a.Azonosító == rekord.Azonosító
                                                select a).FirstOrDefault();

                    if (ElemNap != null)
                    {
                        if (ElemNap.Tervindulás.ToShortDateString() == napszak.ToShortDateString())
                        {
                            MyE.Egyesít("Munka1", MyE.Oszlopnév(oszlop + 2) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                            MyE.Kiir("Benn volt", MyE.Oszlopnév(oszlop + 2) + $"{sor}");
                        }
                    }
                    Adat_Jármű_Vendég VendégAdat = (from a in AdatokFőVendég
                                                    where a.Azonosító == rekord.Azonosító
                                                    select a).FirstOrDefault();

                    if (VendégAdat != null)
                    {
                        if (Cmbtelephely.Trim() != VendégAdat.KiadóTelephely)
                        {
                            MyE.Egyesít("Munka1", MyE.Oszlopnév(oszlop + 2) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                            MyE.Kiir(VendégAdat.KiadóTelephely, MyE.Oszlopnév(oszlop + 2) + $"{sor}");
                        }
                    }
                    sor += 1;
                    i += 1;

                    if (sor == 56)
                    {
                        sor = 5;
                        oszlop += 7;
                        oszlopismét += 1;
                    }
                }
            }

            // összes oszlopszélesség 5
            MyE.Oszlopszélesség("Munka1", "a:" + MyE.Oszlopnév(oszlopismét * 7), 5);
            for (int j = 0; j < oszlopismét; j++)
            {
                //  beállítjuk az oszlop psz szélességeket
                MyE.Oszlopszélesség("Munka1", MyE.Oszlopnév(1 + j * 7) + ":" + MyE.Oszlopnév(1 + j * 7), 16);
                MyE.Oszlopszélesség("Munka1", MyE.Oszlopnév(1 + j * 7 + 1) + ":" + MyE.Oszlopnév(1 + j * 7 + 1), 16);
                //  rácsozzuk
                MyE.Rácsoz(MyE.Oszlopnév(1 + j * 7) + "5:" + MyE.Oszlopnév(7 + j * 7) + "55");
                MyE.Vastagkeret(MyE.Oszlopnév(1 + j * 7) + "5:" + MyE.Oszlopnév(7 + j * 7) + "5");
            }
            MyE.NyomtatásiTerület_részletes("Munka1", "$A$1:$" + MyE.Oszlopnév(7 + (oszlopismét - 1) * 7) + "$61", 0.708661417322835d, 0.708661417322835d,
                0.590551181102362d, 0.748031496062992d, 0.31496062992126d, 0.31496062992126d, "1", "1");


            MyE.Kiir("©Éjszakai jegyellenőrzés", "A3");

            MyE.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap " + Dátum.ToString("dddd"), "a1");

            MyE.Egyesít("Munka1", "a60:f60");
            MyE.Aláírásvonal("A60:F60");
            MyE.Kiir("Váltós csoportvezető", "A60");

            // bezárjuk az Excel-t
            MyE.Aktív_Cella("Munka1", "A1");

            MyE.ExcelMentés(fájlneve);
            MyE.ExcelBezárás();

            MyE.Megnyitás(fájlneve);
        }
    }

    public class Főkönyv_Meghagyás
    {
        public void Főkönyv_Meghagyáskészítés(string fájlexc, string Cmbtelephely, DateTime Dátum, string papírméret, string papírelrendezés)
        {
            try
            {
                MyE.ExcelLétrehozás();
                string munkalap = "Munka1";

                // egész tábla betűméret
                MyE.Munkalap_betű("Calibri", 11);

                // oszlop szélesség
                MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(1) + ":" + MyE.Oszlopnév(30), 6);
                MyE.Oszlopszélesség(munkalap, "d:d", 2);
                MyE.Oszlopszélesség(munkalap, "h:h", 2);
                MyE.Oszlopszélesség(munkalap, "l:l", 2);
                MyE.Oszlopszélesség(munkalap, "r:r", 10);
                // vastag keret
                MyE.Vastagkeret("a1:ad1");

                MyE.Betű("e1", 18);
                MyE.Betű("t1", 18);
                MyE.Betű("e1", false, false, true);
                MyE.Betű("t1", false, false, true);
                MyE.Kiir(DateTime.Today.ToString("yyyy.MM.dd dddd"), "e1");
                MyE.Egyesít(munkalap, "e1:k1");
                MyE.Kiir(DateTime.Today.ToString("yyyy.MM.dd dddd"), "t1");
                MyE.Egyesít(munkalap, "t1:z1");
                MyE.Kiir("Csatolások:", "q20");
                MyE.Betű("q20", false, false, true);
                MyE.Kiir("Szétcsatolások:", "y20");
                MyE.Betű("y20", false, false, true);
                //
                // frissítjük a táblát
                // elkészítjük a formanyomtatványt
                Főkönyv_Funkciók.Napiállók(Cmbtelephely.Trim());
                // kiirjuk a V2-t

                string jelszó = "pozsgaii";


                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\villamos\Új_napihiba.mdb";
                string szöveg = "SELECT * FROM hiba order by azonosító asc";

                Kezelő_Nap_Hiba KNH_kéz = new Kezelő_Nap_Hiba();
                List<Adat_Nap_Hiba> Adatok = KNH_kéz.Lista_adatok(hely, jelszó, szöveg);

                int sor = 2;
                int oszlop = 17;
                Kiirja_Karb_("V3", Adatok, sor, oszlop);

                sor += 2;
                Kiirja_Karb_("V2", Adatok, sor, oszlop);

                sor += 2;
                Kiirja_Karb_("V1", Adatok, sor, oszlop);


                sor += 2;
                Kiirja_Karb_("E3", Adatok, sor, oszlop);


                // vizsgálatra maradjon benn


                // csoportosításhoz alaphelyzetbe állítjuk a váltózókat
                List<string> csoportpsz = new List<string>();


                foreach (Adat_Nap_Hiba rekord in Adatok)
                {
                    if (rekord.Üzemképtelen.ToUpper().Contains("E3") || rekord.Üzemképtelen.ToUpper().Contains("V1"))
                    {
                        csoportpsz.Add(rekord.Azonosító.Trim());
                    }
                }

                // benn maradók csoportba kiirása
                oszlop = 20;
                sor += 2;
                MyE.Kiir("Vizsgálatra maradjon:", MyE.Oszlopnév(17) + $"{sor}");
                MyE.Betű(MyE.Oszlopnév(17) + $"{sor}", false, false, true);

                for (int j = 0; j < csoportpsz.Count; j++)
                {

                    if (csoportpsz[j].Trim() != "")
                    {
                        MyE.Kiir(csoportpsz[j].Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");

                        oszlop += 1;
                        if (oszlop > 28)
                        {
                            oszlop = 21;
                            sor += 1;
                        }
                    }

                }
                // mosás bennmarad
                sor += 2;

                // csoportosításhoz alaphelyzetbe állítjuk a váltózókat
                csoportpsz.Clear();


                szöveg = "SELECT * FROM hiba order by azonosító asc";
                Adatok = KNH_kéz.Lista_adatok(hely, jelszó, szöveg);


                foreach (Adat_Nap_Hiba rekord in Adatok)
                {
                    if (rekord.Üzemképtelen.ToUpper().Contains("MOSÓ"))
                    {
                        csoportpsz.Add(rekord.Azonosító.Trim());
                    }
                }


                // a mosók kiirása
                MyE.Kiir("Mosásra maradjon:", MyE.Oszlopnév(17) + $"{sor}");
                MyE.Betű(MyE.Oszlopnév(17) + $"{sor}", false, false, true);

                oszlop = 20;
                for (int j = 0; j < csoportpsz.Count; j++)
                {
                    if (csoportpsz[j].Trim() != "")
                    {
                        MyE.Kiir(csoportpsz[j], MyE.Oszlopnév(oszlop) + $"{sor}");
                        oszlop += 1;
                        if (oszlop > 28)
                        {
                            oszlop = 19;
                            sor += 1;
                        }
                    }

                }
                // mosás beálló
                // csoportosításhoz alaphelyzetbe állítjuk a váltózókat
                csoportpsz.Clear();

                foreach (Adat_Nap_Hiba rekord in Adatok)
                {
                    if (rekord.Beálló.ToUpper().Contains("MOSÓ"))
                    {
                        csoportpsz.Add(rekord.Azonosító.Trim());
                    }
                }

                // a mosók kiirása
                sor += 2;
                MyE.Kiir("Mosás:", MyE.Oszlopnév(17) + $"{sor}");
                MyE.Betű(MyE.Oszlopnév(17) + $"{sor}", false, false, true);

                oszlop = 19;
                for (int j = 0; j < csoportpsz.Count; j++)
                {

                    if (csoportpsz[j].Trim() != "")
                    {
                        MyE.Kiir(csoportpsz[j], MyE.Oszlopnév(oszlop) + $"{sor}");
                        oszlop += 1;
                        if (oszlop > 28)
                        {
                            oszlop = 19;
                            sor += 1;
                        }
                    }

                }

                // hibák
                oszlop = 1;
                int sorúj = 2;
                MyE.Kiir("Hibák:", MyE.Oszlopnév(1) + sorúj.ToString());
                MyE.Betű(MyE.Oszlopnév(1) + sorúj.ToString(), false, false, true);
                sorúj += 1;


                string szöveg1;
                foreach (Adat_Nap_Hiba rekord in Adatok)
                {

                    sorúj += 1;
                    if (rekord.Státus == 3)
                    {
                        MyE.Kiir("*" + rekord.Azonosító.Trim(), MyE.Oszlopnév(1) + sorúj.ToString());
                    }
                    else
                    {
                        MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(1) + sorúj.ToString());
                    }
                    if (rekord.Státus == 4)
                    {
                        MyE.Betű(MyE.Oszlopnév(1) + sorúj.ToString(), true, false, true);
                    }
                    MyE.Kiir(rekord.Típus.Trim(), MyE.Oszlopnév(2) + sorúj.ToString());
                    MyE.Egyesít(munkalap, MyE.Oszlopnév(2) + sorúj.ToString() + ":" + MyE.Oszlopnév(3) + sorúj.ToString());
                    szöveg1 = "";
                    if (rekord.Üzemképtelen.Trim() != "_")
                    {
                        szöveg1 = rekord.Üzemképtelen.Trim();
                    }
                    if (rekord.Beálló.Trim() != "_")
                    {
                        szöveg1 += "+" + rekord.Beálló.Trim();
                    }
                    if (rekord.Üzemképeshiba.Trim() != "_")
                    {
                        szöveg1 += "+" + rekord.Üzemképeshiba.Trim();
                    }
                    szöveg1 = szöveg1.Length > 100 ? szöveg1.Substring(0, 100) : szöveg1;
                    MyE.Kiir(szöveg1, MyE.Oszlopnév(4) + sorúj.ToString());
                }



                // összecsatolások
                // megnézzük, hogy van-e adott szerelvény napló
                hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\hibanapló\" + Dátum.ToString("yyyyMM") + "szerelvény.mdb";

                // ha van akkor kiirjuk
                if (System.IO.File.Exists(hely))
                {
                    oszlop = 17;

                    szöveg = "SELECT * FROM szerelvénytáblanapló where [szerelvényhossz]>0 and [mikor]> # " + Dátum.ToString("MM-dd-yyyy") + " #";
                    szöveg += " order by mikor";

                    long szerelvény = 0;
                    sor = 22;
                    Kezelő_Szerelvény_Napló KSZN_kéz = new Kezelő_Szerelvény_Napló();
                    List<Adat_Szerelvény_Napló> SzAdatok = KSZN_kéz.Lista_Adatok(hely, jelszó, szöveg);
                    if (SzAdatok != null)
                    {
                        foreach (Adat_Szerelvény_Napló rekord in SzAdatok)
                        {
                            if (rekord.Kocsi2.Trim() != "0")
                            {
                                // ha a szerelvény id nem egyezik akkor sort emel
                                if (szerelvény != rekord.ID && szerelvény != 0)
                                    sor++;
                                if (rekord.Kocsi1.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi1, MyE.Oszlopnév(oszlop) + $"{sor}");
                                if (rekord.Kocsi2.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi2, MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                                if (rekord.Kocsi3.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi3, MyE.Oszlopnév(oszlop + 2) + $"{sor}");
                                if (rekord.Kocsi4.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi4, MyE.Oszlopnév(oszlop + 3) + $"{sor}");
                                if (rekord.Kocsi5.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi5, MyE.Oszlopnév(oszlop + 4) + $"{sor}");
                                if (rekord.Kocsi6.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi6, MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                                szerelvény = rekord.ID;
                            }
                        }
                    }

                    // Szétcsatolások
                    oszlop = 25;
                    sor = 22;

                    szöveg = "SELECT * FROM szerelvénytáblanapló where [szerelvényhossz]=0 and [mikor]> # " + Dátum.ToString("MM-dd-yyyy") + " #";
                    szöveg += " order by mikor";

                    SzAdatok = KSZN_kéz.Lista_Adatok(hely, jelszó, szöveg);
                    if (SzAdatok != null)
                    {
                        foreach (Adat_Szerelvény_Napló rekord in SzAdatok)
                        {
                            // ha a második kocsi van akkor kírja a 0-kat
                            if (rekord.Kocsi2.Trim() != "0")
                            {
                                if (rekord.Kocsi1.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi1.Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");
                                if (rekord.Kocsi2.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi2.Trim(), MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                                if (rekord.Kocsi3.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi3.Trim(), MyE.Oszlopnév(oszlop + 2) + $"{sor}");
                                if (rekord.Kocsi4.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi4.Trim(), MyE.Oszlopnév(oszlop + 3) + $"{sor}");
                                if (rekord.Kocsi5.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi5.Trim(), MyE.Oszlopnév(oszlop + 4) + $"{sor}");
                                if (rekord.Kocsi6.Trim() != "0")
                                    MyE.Kiir(rekord.Kocsi6.Trim(), MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                                sor += 1;
                            }
                        }
                    }
                }
                if (sorúj >= sor)
                    sor = sorúj;
                // vastag keret
                MyE.Vastagkeret("a1:ad" + $"{sor}");
                // nyomtatási beállítások
                bool papírelrendez;
                if (papírelrendezés == "--")
                    papírelrendez = false;
                else if (papírelrendezés == "Álló")
                    papírelrendez = true;
                else
                    papírelrendez = false;
                if (papírméret == "--") papírméret = "A3";

                MyE.NyomtatásiTerület_részletes(munkalap, "a1:ad" + $"{sor}", 0.196850393700787d, 0.196850393700787d, 0.196850393700787d, 0.196850393700787d,
                    0.196850393700787d, 0.196850393700787d, "1", "1", papírelrendez, papírméret, true, true);

                // bezárjuk az Excel-t
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                MyE.Megnyitás(fájlexc);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);

            }
        }
        void Kiirja_Karb_(string Karbantartás, List<Adat_Nap_Hiba> Adatok, int sor, int oszlop)
        {
            if (Adatok.Count > 0)
            {
                MyE.Kiir(Karbantartás, MyE.Oszlopnév(oszlop) + $"{sor}");
                MyE.Betű(MyE.Oszlopnév(oszlop) + $"{sor}", false, false, true);
                foreach (Adat_Nap_Hiba rekord in Adatok)
                {
                    // kiirjuk a v3-t
                    if (rekord.Üzemképtelen.ToUpper().Contains(Karbantartás))
                    {
                        oszlop += 1;
                        if (oszlop == 31)
                        {
                            oszlop = 18;
                            sor += 1;
                        }
                        MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");
                        MyE.Betű(MyE.Oszlopnév(oszlop) + $"{sor}", true, false, true);
                    }
                    // kiirjuk a v3-t
                    if (rekord.Beálló.ToUpper().Contains(Karbantartás))
                    {
                        oszlop += 1;
                        if (oszlop == 31)
                        {
                            oszlop = 18;
                            sor += 1;
                        }
                        MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");
                    }
                }
            }
        }
    }

    public class Főkönyv_Takarítás
    {
        public void Takarítás_Excel(string fájlexc, string Cmbtelephely, DateTime Dátum, string napszak, List<string> AdatokTakarításTípus,
                                 List<Adat_Jármű> AdatokJármű,
                                 List<Adat_Főkönyv_Nap> AdatokFőkönyvNap,
                                 List<Adat_Jármű_Vendég> AdatokFőVendég,
                                 List<Adat_Főkönyv_ZSER> AdatokFőkönyvZSER)
        {
            MyE.ExcelLétrehozás();

            // létrehozzunk annyi lapfület amennyi típusm kell
            if (AdatokTakarításTípus != null && AdatokTakarításTípus.Count > 0)
            {
                MyE.Munkalap_átnevezés("Munka1", "Takarítás");
                MyE.Új_munkalap("Nappalos Igazoló");
                MyE.Új_munkalap("Összes_állományi");
                MyE.Új_munkalap("J1_J2_J3");
                MyE.Új_munkalap("J4_J5_J6");

                // munkalapok létrehozása
                foreach (string rekordkieg in AdatokTakarításTípus)
                {
                    MyE.Új_munkalap(rekordkieg.Trim());
                    MyE.Új_munkalap(rekordkieg.Trim() + "_Üres");
                }

            }

            Söpréslapok(napszak, Dátum, Cmbtelephely, AdatokTakarításTípus, AdatokJármű, AdatokFőkönyvNap, AdatokFőVendég);
            Üreslapok(Dátum, napszak, AdatokTakarításTípus, AdatokJármű);
            EstiBeállók(Dátum, napszak, AdatokFőkönyvZSER);
            Összes_takarítás_kocsi(Cmbtelephely, Dátum, napszak, AdatokTakarításTípus, AdatokJármű, AdatokFőkönyvNap, AdatokFőVendég);
            Takarítás_igazoló(Cmbtelephely, Dátum);
            Összevont("J1", Dátum, napszak, AdatokJármű, AdatokFőkönyvNap, AdatokTakarításTípus, AdatokFőVendég);
            Összevont("J4", Dátum, napszak, AdatokJármű, AdatokFőkönyvNap, AdatokTakarításTípus, AdatokFőVendég);

            // bezárjuk az Excel-t
            MyE.Munkalap_aktív("Takarítás");
            MyE.Aktív_Cella("Takarítás", "A1");

            MyE.ExcelMentés(fájlexc);
            MyE.ExcelBezárás();
            MyE.Megnyitás(fájlexc);
        }

        private void Söpréslapok(string napszak, DateTime Dátum, string Cmbtelephely, List<string> AdatokTakarításTípus,
                                 List<Adat_Jármű> AdatokJármű,
                                 List<Adat_Főkönyv_Nap> AdatokFőkönyvNap,
                                 List<Adat_Jármű_Vendég> AdatokFőVendég)
        {

            // ******************************
            // *  Söprés lapok              *
            // ******************************
            int sor, oszlop, oszlopismét;

            foreach (string rekordkieg in AdatokTakarításTípus)
            {
                MyE.Munkalap_aktív(rekordkieg.Trim());
                sor = 5;
                oszlop = 1;
                oszlopismét = 1;

                List<Adat_Jármű> AdatokJárműSzűrt = (from a in AdatokJármű
                                                     where a.Típus == rekordkieg
                                                     orderby a.Azonosító ascending
                                                     select a).ToList();

                foreach (Adat_Jármű rekord in AdatokJárműSzűrt)
                {
                    if (sor == 5)
                    {
                        // elkészítjük a fejlécet
                        MyE.Kiir("Psz", MyE.Oszlopnév(oszlop) + $"{sor}");
                        MyE.Kiir("Kijelölve", MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                        MyE.Kiir("Megfelelő", MyE.Oszlopnév(oszlop + 2) + $"{sor}");
                        MyE.Kiir("Nem Megfelelő", MyE.Oszlopnév(oszlop + 3) + $"{sor}");
                        MyE.Kiir("Graffiti (m2)", MyE.Oszlopnév(oszlop + 4) + $"{sor}");
                        MyE.Kiir("Eseti (m2)", MyE.Oszlopnév(oszlop + 5) + $"{sor}");
                        MyE.Kiir("Fertőtlenítés (m2)", MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                        sor += 1;
                    }
                    MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");

                    DateTime IdeigDátum = new DateTime(Dátum.Year, Dátum.Month, Dátum.Day, 14, 0, 0);
                    DateTime NullaDátum = new DateTime(1900, 1, 1, 0, 0, 0);
                    Adat_Főkönyv_Nap ElemNap;
                    if (napszak.Trim() == "de")
                        ElemNap = (from a in AdatokFőkönyvNap
                                   where a.Azonosító == rekord.Azonosító &&
                                   a.Tervérkezés < IdeigDátum &&
                                   a.Tervérkezés != NullaDátum
                                   select a).FirstOrDefault();
                    else
                        ElemNap = (from a in AdatokFőkönyvNap
                                   where a.Azonosító == rekord.Azonosító &&
                                   a.Tervérkezés >= IdeigDátum
                                   select a).FirstOrDefault();

                    if (ElemNap != null) MyE.Kiir("X", MyE.Oszlopnév(oszlop + 1) + $"{sor}");


                    Adat_Jármű_Vendég VendégAdat = (from a in AdatokFőVendég
                                                    where a.Azonosító == rekord.Azonosító
                                                    select a).FirstOrDefault();

                    if (VendégAdat != null)
                    {
                        if (Cmbtelephely.Trim() != VendégAdat.KiadóTelephely)
                        {
                            MyE.Kiir("", MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                            MyE.Egyesít(rekordkieg.Trim(), MyE.Oszlopnév(oszlop + 2) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                            MyE.Kiir(VendégAdat.KiadóTelephely, MyE.Oszlopnév(oszlop + 2) + $"{sor}");
                        }
                    }

                    sor += 1;
                    if (sor == 46)
                    {
                        sor = 5;
                        oszlop += 8;
                        oszlopismét += 1;
                    }
                }


                // minden cella
                MyE.Munkalap_betű("Arial", 20);

                // első sor állítva
                MyE.SzövegIrány(rekordkieg.Trim(), "5:5", 90);
                MyE.Sormagasság("5:5", 175);

                // összes oszlopszélesség 7
                MyE.Oszlopszélesség(rekordkieg.Trim(), "a:" + MyE.Oszlopnév(oszlopismét * 7), 6);

                for (int j = 0; j < oszlopismét; j++)
                {
                    // beállítjuk az oszlop psz szélességeket
                    MyE.Oszlopszélesség(rekordkieg.Trim(), MyE.Oszlopnév(1 + j * 8) + ":" + MyE.Oszlopnév(1 + j * 8), 10);

                    // rácsozzuk
                    MyE.Rácsoz(MyE.Oszlopnév(1 + j * 8) + "5:" + MyE.Oszlopnév(7 + j * 8) + "46");
                    MyE.Vastagkeret(MyE.Oszlopnév(1 + j * 8) + "5:" + MyE.Oszlopnév(7 + j * 8) + "5");
                    MyE.Vastagkeret(MyE.Oszlopnév(1 + j * 8) + "46:" + MyE.Oszlopnév(7 + j * 8) + "46");
                    MyE.Kiir("=COUNTA(R[-40]C:R[-1]C)", MyE.Oszlopnév(2 + j * 8) + "46");
                }
                MyE.Sormagasság("46:47", 30);
                MyE.Vastagkeret(MyE.Oszlopnév(1 + (oszlopismét - 1) * 8) + "47:" + MyE.Oszlopnév(7 + (oszlopismét - 1) * 8) + "47");
                MyE.Kiir("Össz", "A46");
                MyE.Kiir("Össz", MyE.Oszlopnév(1 + (oszlopismét - 1) * 8) + "47");
                MyE.Kiir("=SUM(R[-1])", MyE.Oszlopnév(2 + (oszlopismét - 1) * 8) + "47");


                if (oszlopismét < 3)
                    oszlopismét = 3;

                MyE.NyomtatásiTerület_részletes(rekordkieg.Trim(), "A1:" + MyE.Oszlopnév(7 + (oszlopismét - 1) * 8) + "65", 0.393700787401575d, 0.393700787401575d,
                    0.590551181102362d, 0.748031496062992d, 0.31496062992126d, 0.31496062992126d, "1", "1", true, "A4", false, false);

                if (napszak == "de")
                    MyE.Kiir(rekordkieg.Trim() + "    ©J1     takarítás Nappal", "a3");
                else
                    MyE.Kiir(rekordkieg.Trim() + "    ©J1     takarítás ÉJSZAKA", "a3");



                MyE.Kiir("Előírt létszám:  …… fő, megjelent :……. Fő", "A49");
                MyE.Kiir("Cégjelzéses munkaruhát nem viselt : …….. Fő", "a51");

                MyE.Egyesít(rekordkieg.Trim(), "a53:p53");
                MyE.Kiir("A megrendelő a napi munkatervet átadta: ……….óra……….perckor. ", "A53");
                MyE.Egyesít(rekordkieg.Trim(), "a58:e58");
                MyE.Egyesít(rekordkieg.Trim(), "g58:k58");
                MyE.Kiir("BKV ZRT.", "a58");
                MyE.Kiir("Vállalkozó", "g58");
                MyE.Pontvonal("a58:e58");
                MyE.Pontvonal("g58:k58");

                MyE.Egyesít(rekordkieg.Trim(), "a59:p59");
                MyE.Kiir("A vállalkozó a napi munkafeladatok elvégzését lejelentette:", "A59");
                MyE.Egyesít(rekordkieg.Trim(), "a61:p61");
                MyE.Kiir("  ……….óra……….perckor.", "a61");


                MyE.Egyesít(rekordkieg.Trim(), "a65:e65");
                MyE.Egyesít(rekordkieg.Trim(), "g65:k65");
                MyE.Kiir("BKV ZRT.", "a65");
                MyE.Kiir("Vállalkozó", "g65");
                MyE.Pontvonal("a65:e65");
                MyE.Pontvonal("g65:k65");

                MyE.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");
                MyE.Aktív_Cella(rekordkieg.Trim(), "A1");
            }
        }

        private void Üreslapok(DateTime Dátum, string napszak, List<string> AdatokTakarításTípus,
                                 List<Adat_Jármű> AdatokJármű)
        {
            // ******************************
            // *  Üres lapok                *
            // ******************************
            int sor, oszlop, oszlopismét;
            foreach (string rekordkieg in AdatokTakarításTípus)
            {
                string munkalap = rekordkieg.Trim() + "_Üres";
                MyE.Munkalap_aktív(rekordkieg.Trim() + "_Üres");

                sor = 5;
                oszlop = 1;
                oszlopismét = 1;

                List<Adat_Jármű> AdatokJárműSzűrt = (from a in AdatokJármű
                                                     where a.Típus == rekordkieg
                                                     orderby a.Azonosító ascending
                                                     select a).ToList();

                foreach (Adat_Jármű rekord in AdatokJárműSzűrt)
                {
                    if (sor == 5)
                    {
                        // elkészítjük a fejlécet
                        MyE.Kiir("Psz", MyE.Oszlopnév(oszlop) + $"{sor}");
                        MyE.Kiir("Kijelölve", MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                        MyE.Kiir("Megfelelő", MyE.Oszlopnév(oszlop + 2) + $"{sor}");
                        MyE.Kiir("Nem Megfelelő", MyE.Oszlopnév(oszlop + 3) + $"{sor}");
                        MyE.Kiir("Graffiti (m2)", MyE.Oszlopnév(oszlop + 4) + $"{sor}");
                        MyE.Kiir("Eseti (m2)", MyE.Oszlopnév(oszlop + 5) + $"{sor}");
                        MyE.Kiir("Fertőtlenítés (m2)", MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                        sor += 1;
                    }
                    MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");
                    sor += 1;

                    if (sor == 46)
                    {
                        sor = 5;
                        oszlop += 8;
                        oszlopismét += 1;
                    }

                }

                // minden cella
                MyE.Munkalap_betű("Arial", 20);

                // első sor állítva
                MyE.SzövegIrány(munkalap, "5:5", 90);
                MyE.Sormagasság("5:5", 175);

                // összes oszlopszélesség 7
                MyE.Oszlopszélesség(munkalap, "a:" + MyE.Oszlopnév(oszlopismét * 7), 6);

                for (int j = 0; j < oszlopismét; j++)
                {
                    // beállítjuk az oszlop psz szélességeket
                    MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(1 + j * 8) + ":" + MyE.Oszlopnév(1 + j * 8), 10);

                    // rácsozzuk
                    MyE.Rácsoz(MyE.Oszlopnév(1 + j * 8) + "5:" + MyE.Oszlopnév(7 + j * 8) + "46");
                    MyE.Vastagkeret(MyE.Oszlopnév(1 + j * 8) + "5:" + MyE.Oszlopnév(7 + j * 8) + "5");
                    MyE.Vastagkeret(MyE.Oszlopnév(1 + j * 8) + "46:" + MyE.Oszlopnév(7 + j * 8) + "46");

                }
                MyE.Sormagasság("46:47", 30);
                MyE.Vastagkeret(MyE.Oszlopnév(1 + (oszlopismét - 1) * 8) + "47:" + MyE.Oszlopnév(7 + (oszlopismét - 1) * 8) + "47");
                MyE.Kiir("Össz", "A46");
                MyE.Kiir("Össz", MyE.Oszlopnév(1 + (oszlopismét - 1) * 8) + "47");

                if (oszlopismét < 3)
                    oszlopismét = 3;
                MyE.NyomtatásiTerület_részletes(munkalap, "A1:" + MyE.Oszlopnév(7 + (oszlopismét - 1) * 8) + "65", 0.393700787401575d, 0.393700787401575d,
                                0.590551181102362d, 0.748031496062992d, 0.31496062992126d, 0.31496062992126d, "1", "1", true, "A4", false, false);

                if (napszak == "de")
                    MyE.Kiir(rekordkieg.Trim() + "    ©J1     takarítás Nappal", "a3");
                else
                    MyE.Kiir(rekordkieg.Trim() + "    ©J1     takarítás ÉJSZAKA", "a3");



                MyE.Kiir("Előírt létszám:  …… fő, megjelent :……. Fő", "A49");
                MyE.Kiir("Cégjelzéses munkaruhát nem viselt : …….. Fő", "a51");

                MyE.Egyesít(munkalap, "A53:P53");
                MyE.Kiir("A megrendelő a napi munkatervet átadta: ……….óra……….perckor. ", "A53");
                MyE.Egyesít(munkalap, "a58:e58");
                MyE.Egyesít(munkalap, "g58:k58");
                MyE.Kiir("BKV ZRT.", "a58");
                MyE.Kiir("Vállalkozó", "g58");
                MyE.Pontvonal("a58:e58");
                MyE.Pontvonal("g58:k58");

                MyE.Egyesít(munkalap, "a59:p59");
                MyE.Kiir("A vállalkozó a napi munkafeladatok elvégzését lejelentette:", "A59");
                MyE.Egyesít(munkalap, "a61:p61");
                MyE.Kiir("  ……….óra……….perckor.", "a61");


                MyE.Egyesít(munkalap, "a65:e65");
                MyE.Egyesít(munkalap, "g65:k65");
                MyE.Kiir("BKV ZRT.", "a65");
                MyE.Kiir("Vállalkozó", "g65");
                MyE.Pontvonal("a65:e65");
                MyE.Pontvonal("g65:k65");

                MyE.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");
                MyE.Aktív_Cella(munkalap, "A1");
            }
        }

        private void EstiBeállók(DateTime Dátum, string napszak, List<Adat_Főkönyv_ZSER> AdatokFőkönyvZSER)
        {

            // ******************************
            // *  ESti beállók              *
            // ******************************

            int sor;
            string munkalap = "Takarítás";
            MyE.Munkalap_aktív(munkalap);
            // minden cella
            MyE.Munkalap_betű("Arial", 14);

            sor = 2;

            if (napszak == "de")
                MyE.Kiir(Dátum.ToString("yyyy.MM.dd") + " Nappali söprés", "a1");
            else
                MyE.Kiir(Dátum.ToString("yyyy.MM.dd") + " Esti söprés", "a1");

            foreach (Adat_Főkönyv_ZSER rekord in AdatokFőkönyvZSER)
            {
                if (napszak == "de")
                {
                    if (rekord.Tervérkezés < new DateTime(Dátum.Year, Dátum.Month, Dátum.Day, 14, 0, 0) && rekord.Tervérkezés > new DateTime(Dátum.Year, Dátum.Month, Dátum.Day, 0, 0, 0))
                    {
                        sor += 1;
                        MyE.Kiir(rekord.Viszonylat.Trim(), MyE.Oszlopnév(1) + $"{sor}");
                        if (rekord.Kocsi1.Trim() != "_")
                            MyE.Kiir(rekord.Kocsi1.Trim(), MyE.Oszlopnév(2) + $"{sor}");
                        if (rekord.Kocsi2.Trim() != "_")
                            MyE.Kiir(rekord.Kocsi2.Trim(), MyE.Oszlopnév(3) + $"{sor}");
                        if (rekord.Kocsi3.Trim() != "_")
                            MyE.Kiir(rekord.Kocsi3.Trim(), MyE.Oszlopnév(4) + $"{sor}");
                        if (rekord.Kocsi4.Trim() != "_")
                            MyE.Kiir(rekord.Kocsi4.Trim(), MyE.Oszlopnév(5) + $"{sor}");
                        if (rekord.Kocsi5.Trim() != "_")
                            MyE.Kiir(rekord.Kocsi5.Trim(), MyE.Oszlopnév(6) + $"{sor}");
                        if (rekord.Kocsi6.Trim() != "_")
                            MyE.Kiir(rekord.Kocsi6.Trim(), MyE.Oszlopnév(7) + $"{sor}");
                        MyE.Kiir(rekord.Tervérkezés.ToString(), MyE.Oszlopnév(8) + $"{sor}");
                    }
                }

                else if (rekord.Tervérkezés > new DateTime(Dátum.Year, Dátum.Month, Dátum.Day, 14, 0, 0))
                {
                    sor += 1;
                    MyE.Kiir(rekord.Viszonylat.Trim(), MyE.Oszlopnév(1) + $"{sor}");
                    if (rekord.Kocsi1.Trim() != "_")
                        MyE.Kiir(rekord.Kocsi1.Trim(), MyE.Oszlopnév(2) + $"{sor}");
                    if (rekord.Kocsi2.Trim() != "_")
                        MyE.Kiir(rekord.Kocsi2.Trim(), MyE.Oszlopnév(3) + $"{sor}");
                    if (rekord.Kocsi3.Trim() != "_")
                        MyE.Kiir(rekord.Kocsi3.Trim(), MyE.Oszlopnév(4) + $"{sor}");
                    if (rekord.Kocsi4.Trim() != "_")
                        MyE.Kiir(rekord.Kocsi4.Trim(), MyE.Oszlopnév(5) + $"{sor}");
                    if (rekord.Kocsi5.Trim() != "_")
                        MyE.Kiir(rekord.Kocsi5.Trim(), MyE.Oszlopnév(6) + $"{sor}");
                    if (rekord.Kocsi6.Trim() != "_")
                        MyE.Kiir(rekord.Kocsi6.Trim(), MyE.Oszlopnév(7) + $"{sor}");
                    MyE.Kiir(rekord.Tervérkezés.ToString(), MyE.Oszlopnév(8) + $"{sor}");
                }

            }


            MyE.Oszlopszélesség(munkalap, "H:H");
            MyE.NyomtatásiTerület_részletes("Takarítás", "A1:H" + $"{sor}", "", "", true);

            MyE.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");
            MyE.Aktív_Cella("Takarítás", "A1");
        }

        private void Takarítás_igazoló(string Cmbtelephely, DateTime Dátum)
        {
            string munkalap = "Nappalos Igazoló";
            MyE.Munkalap_aktív(munkalap);

            // Betűméret
            MyE.Munkalap_betű("Calibri", 12);

            // létszám adatok
            MyE.Oszlopszélesség(munkalap, "a:a", 6);
            MyE.Oszlopszélesség(munkalap, "b:c", 15);
            MyE.Oszlopszélesség(munkalap, "d:i", 10);
            MyE.Oszlopszélesség(munkalap, "i:i", 13);
            MyE.Egyesít(munkalap, "a1:b1");
            MyE.Egyesít(munkalap, "a2:b2");
            MyE.Egyesít(munkalap, "a3:b3");
            MyE.Kiir("Előírt létszám [Fő]: ", "a1");
            MyE.Kiir("Megjelent [Fő]:", "a2");
            MyE.Kiir("Munkaruhát viselt  [Fő]:", "a3");
            MyE.Rácsoz("a1:c3");

            Kezelő_Jármű_Takarítás_Vezénylés KJTV_kéz = new Kezelő_Jármű_Takarítás_Vezénylés();
            List<Adat_Jármű_Takarítás_Vezénylés> Adatok = KJTV_kéz.Lista_Adatok(Cmbtelephely.Trim(), DateTime.Now.Year);
            Adatok = (from a in Adatok
                      where a.Státus != 9
                      && a.Dátum.ToShortDateString() == Dátum.ToShortDateString()
                      orderby a.Takarítási_fajta, a.Szerelvényszám, a.Azonosító
                      select a).ToList();

            string takarítási_fajta = "";
            int sor = 5;
            int eleje = 5;
            int vége = 5;

            foreach (Adat_Jármű_Takarítás_Vezénylés rekord in Adatok)
            {

                if (takarítási_fajta.Trim() == rekord.Takarítási_fajta.Trim())
                {
                    // ha azonos akkor kiírja a pályaszámot
                    sor += 1;
                    MyE.Kiir(rekord.Azonosító.Trim(), "a" + $"{sor}");
                    MyE.Sormagasság(sor.ToString() + ":" + $"{sor}", 25);
                }
                else
                {
                    // fejlécet készít és befejezi az előző táblát
                    if (sor != 5)
                    {
                        vége = sor + 3;
                        MyE.Sormagasság(sor.ToString() + ":" + (sor + 3).ToString(), 25);
                        sor += 4;
                    }
                    if (eleje == 5 & vége == 5)
                    {
                    }
                    // első alkalommal nem fejezi be az előző táblázatot
                    else
                    {
                        // befejezi az előző táblát
                        MyE.Rácsoz("a" + eleje.ToString() + ":i" + vége.ToString());
                        MyE.Vastagkeret("a" + eleje.ToString() + ":i" + vége.ToString());
                        MyE.Vastagkeret("a" + (eleje + 1).ToString() + ":i" + vége.ToString());
                    }

                    takarítási_fajta = rekord.Takarítási_fajta.Trim();
                    MyE.Betű("a" + $"{sor}", 16);

                    MyE.Kiir(takarítási_fajta.Trim(), "a" + $"{sor}");
                    sor += 1;
                    eleje = sor;


                    // fejléc
                    MyE.Sormagasság(sor.ToString() + ":" + $"{sor}", 48);
                    MyE.Kiir("Jármű biztosításának ideje", "b" + $"{sor}");
                    MyE.Kiir("Takarítás befejezésének ideje", "c" + $"{sor}");

                    MyE.Kiir("Megfelelt", "d" + $"{sor}");
                    MyE.Kiir("Nem Megfelelt", "e" + $"{sor}");
                    MyE.Kiir("Pót határidő", "f" + $"{sor}");
                    MyE.Kiir("Megfelelt", "g" + $"{sor}");
                    MyE.Kiir("Nem Megfelelt", "h" + $"{sor}");
                    MyE.Kiir("Igazolta", "i" + $"{sor}");
                    MyE.Sortörésseltöbbsorba(sor.ToString() + ":" + $"{sor}");
                    // első kocsi
                    sor += 1;
                    MyE.Kiir(rekord.Azonosító.Trim(), "a" + $"{sor}");
                    MyE.Sormagasság(sor.ToString() + ":" + $"{sor}", 25);
                }
            }


            // befejezi az előző tábláta  

            vége = sor + 3;
            MyE.Sormagasság(sor.ToString() + ":" + (sor + 3).ToString(), 25);
            MyE.Rácsoz("a" + eleje.ToString() + ":i" + vége.ToString());
            MyE.Vastagkeret("a" + eleje.ToString() + ":i" + vége.ToString());
            MyE.Vastagkeret("a" + (eleje + 1).ToString() + ":i" + vége.ToString());


            // Aláírás lábléc
            sor += 5;
            MyE.Egyesít(munkalap, "a" + $"{sor}" + ":g" + $"{sor}");
            MyE.Kiir("A megrendelő a napi munkatervet átadta: ……….óra……….perckor. ", "A" + $"{sor}");
            sor += 5;
            MyE.Egyesít(munkalap, "a" + $"{sor}" + ":d" + $"{sor}");
            MyE.Egyesít(munkalap, "f" + $"{sor}" + ":i" + $"{sor}");
            MyE.Kiir("BKV ZRT.", "a" + $"{sor}");
            MyE.Kiir("Vállalkozó", "f" + $"{sor}");
            MyE.Pontvonal("a" + $"{sor}");
            MyE.Pontvonal("f" + $"{sor}");

            sor += 2;
            MyE.Egyesít(munkalap, "a" + $"{sor}" + ":g" + $"{sor}");
            MyE.Kiir("A vállalkozó a napi munkafeladatok elvégzését lejelentette:  ……….óra……….perckor.", "a" + $"{sor}");

            sor += 5;
            MyE.Egyesít(munkalap, "a" + $"{sor}" + ":d" + $"{sor}");
            MyE.Egyesít(munkalap, "f" + $"{sor}" + ":i" + $"{sor}");
            MyE.Kiir("BKV ZRT.", "a" + $"{sor}");
            MyE.Kiir("Vállalkozó", "f" + $"{sor}");
            MyE.Pontvonal("a" + $"{sor}");
            MyE.Pontvonal("f" + $"{sor}");

            vége = sor;

            // nyomtatási beállítások
            MyE.NyomtatásiTerület_részletes(munkalap, "A1:I" + vége.ToString(), "$6:$6", "", Cmbtelephely.Trim(), "©Jármű takarítás igazolólap Nappal ",
                Dátum.ToString("yyyy.MM.dd"), "........................................\n                    BKV Zrt", "",
                "........................................\nTakarítást végző    \n", "", 0.708661417322835d, 0.708661417322835d,
                0.748031496062992d, 0.748031496062992d, 0.31496062992126d, 0.31496062992126d, true, false, "1", "false", true, "A4");


            MyE.Aktív_Cella(munkalap, "A1");
        }

        private void Összes_takarítás_kocsi(string Cmbtelephely, DateTime Dátum, string napszak, List<string> AdatokTakarításTípus, List<Adat_Jármű> AdatokJármű,
            List<Adat_Főkönyv_Nap> AdatokFőkönyvNap, List<Adat_Jármű_Vendég> AdatokFőVendég)
        {
            string munkalap = "Összes_állományi";
            MyE.Munkalap_aktív(munkalap);

            int sor;
            int oszlop;
            int oszlopismét;
            int blokkeleje;

            sor = 5;
            oszlop = 1;
            oszlopismét = 1;

            // elkészítjük a fejlécet
            MyE.Kiir("Psz", MyE.Oszlopnév(oszlop) + 5.ToString());
            MyE.Kiir("Kijelölve", MyE.Oszlopnév(oszlop + 1) + 5.ToString());
            MyE.Kiir("Megfelelő", MyE.Oszlopnév(oszlop + 2) + 5.ToString());
            MyE.Kiir("Nem Megfelelő", MyE.Oszlopnév(oszlop + 3) + 5.ToString());
            MyE.Kiir("Graffiti (m2)", MyE.Oszlopnév(oszlop + 4) + 5.ToString());
            MyE.Kiir("Eseti (m2)", MyE.Oszlopnév(oszlop + 5) + 5.ToString());
            MyE.Kiir("Fertőtlenítés (m2)", MyE.Oszlopnév(oszlop + 6) + 5.ToString());
            sor += 1;

            foreach (string rekordkieg in AdatokTakarításTípus)
            {
                // elkészítjük a fejlécet
                MyE.Kiir("Psz", MyE.Oszlopnév(oszlop) + 5.ToString());
                MyE.Kiir("Kijelölve", MyE.Oszlopnév(oszlop + 1) + 5.ToString());
                MyE.Kiir("Megfelelő", MyE.Oszlopnév(oszlop + 2) + 5.ToString());
                MyE.Kiir("Nem Megfelelő", MyE.Oszlopnév(oszlop + 3) + 5.ToString());
                MyE.Kiir("Graffiti (m2)", MyE.Oszlopnév(oszlop + 4) + 5.ToString());
                MyE.Kiir("Eseti (m2)", MyE.Oszlopnév(oszlop + 5) + 5.ToString());
                MyE.Kiir("Fertőtlenítés (m2)", MyE.Oszlopnév(oszlop + 6) + 5.ToString());

                MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop + 1) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                MyE.Kiir(rekordkieg.Trim(), MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                MyE.Vastagkeret(MyE.Oszlopnév(oszlop + 1) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                sor += 1;
                blokkeleje = sor;

                List<Adat_Jármű> AdatokJárműSzűrt = (from a in AdatokJármű
                                                     where a.Típus == rekordkieg
                                                     orderby a.Azonosító ascending
                                                     select a).ToList();

                foreach (Adat_Jármű rekord in AdatokJárműSzűrt)
                {

                    if (sor == 5)
                    {
                        // elkészítjük a fejlécet
                        MyE.Kiir("Psz", MyE.Oszlopnév(oszlop) + $"{sor}");
                        MyE.Kiir("Kijelölve", MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                        MyE.Kiir("Megfelelő", MyE.Oszlopnév(oszlop + 2) + $"{sor}");
                        MyE.Kiir("Nem Megfelelő", MyE.Oszlopnév(oszlop + 3) + $"{sor}");
                        MyE.Kiir("Graffiti (m2)", MyE.Oszlopnév(oszlop + 4) + $"{sor}");
                        MyE.Kiir("Eseti (m2)", MyE.Oszlopnév(oszlop + 5) + $"{sor}");
                        MyE.Kiir("Fertőtlenítés (m2)", MyE.Oszlopnév(oszlop + 6) + $"{sor}");

                        sor += 1;
                    }

                    MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");

                    DateTime IdeigDátum = new DateTime(Dátum.Year, Dátum.Month, Dátum.Day, 14, 0, 0);
                    DateTime NullaDátum = new DateTime(1900, 1, 1, 0, 0, 0);
                    Adat_Főkönyv_Nap ElemNap;
                    if (napszak.Trim() == "de")
                        ElemNap = (from a in AdatokFőkönyvNap
                                   where a.Azonosító == rekord.Azonosító &&
                                   a.Tervérkezés < IdeigDátum &&
                                   a.Tervérkezés != NullaDátum
                                   select a).FirstOrDefault();
                    else
                        ElemNap = (from a in AdatokFőkönyvNap
                                   where a.Azonosító == rekord.Azonosító &&
                                   a.Tervérkezés >= IdeigDátum
                                   select a).FirstOrDefault();
                    if (ElemNap != null) MyE.Kiir("X", MyE.Oszlopnév(oszlop + 1) + $"{sor}");

                    Adat_Jármű_Vendég VendégAdat = (from a in AdatokFőVendég
                                                    where a.Azonosító == rekord.Azonosító
                                                    select a).FirstOrDefault();

                    if (VendégAdat != null)
                    {
                        if (Cmbtelephely.Trim() != VendégAdat.KiadóTelephely)
                        {
                            MyE.Kiir("", MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                            MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop + 2) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                            MyE.Kiir(VendégAdat.KiadóTelephely, MyE.Oszlopnév(oszlop + 2) + $"{sor}");
                        }
                    }

                    sor += 1;
                    if (sor >= 46)
                    {
                        MyE.Kiir("Össz", MyE.Oszlopnév(oszlop) + $"{sor}");
                        MyE.Kiir("=COUNTA(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C)", MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                        MyE.Rácsoz(MyE.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                        MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");

                        sor = 5;
                        blokkeleje = 6;
                        oszlop += 7;
                        oszlopismét += 1;
                    }
                }


                // ha vége a típusnak akkor összesítünk
                if (sor >= 43)
                {
                    sor = 46;
                    MyE.Kiir("Össz", MyE.Oszlopnév(oszlop) + $"{sor}");
                    MyE.Kiir("=COUNTA(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C)", MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                    MyE.Rácsoz(MyE.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                    sor = 6;
                    blokkeleje = 6;
                    oszlop += 7;
                    oszlopismét += 1;
                }
                else
                {
                    sor += 3;
                    MyE.Kiir("Össz", MyE.Oszlopnév(oszlop) + $"{sor}");
                    MyE.Kiir("=COUNTA(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C)", MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                    MyE.Rácsoz(MyE.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                    sor += 1;
                }
            }



            // **************************************************************
            // ha van olyan jármű ami másik telephelyről jött, akkor kiírjuk
            // '**************************************************************

            List<Adat_Jármű_Vendég> VAdatok = (from a in AdatokFőVendég
                                               where a.KiadóTelephely == Cmbtelephely.Trim()
                                               orderby a.Típus, a.Azonosító
                                               select a).ToList();

            if (sor >= 43)
            {
                sor = 6;
                blokkeleje = 6;
                oszlop += 7;
                oszlopismét += 1;
            }
            else
            {
                blokkeleje = sor;
                sor += 1;
            }
            string előzőtípus = "";


            foreach (Adat_Jármű_Vendég rekord in VAdatok)
            {
                if (sor == 5)
                {
                    // elkészítjük a fejlécet
                    MyE.Kiir("Psz", MyE.Oszlopnév(oszlop) + 5.ToString());
                    MyE.Kiir("Kijelölve", MyE.Oszlopnév(oszlop + 1) + 5.ToString());
                    MyE.Kiir("Megfelelő", MyE.Oszlopnév(oszlop + 2) + 5.ToString());
                    MyE.Kiir("Nem Megfelelő", MyE.Oszlopnév(oszlop + 3) + 5.ToString());
                    MyE.Kiir("Graffiti (m2)", MyE.Oszlopnév(oszlop + 4) + 5.ToString());
                    MyE.Kiir("Eseti (m2)", MyE.Oszlopnév(oszlop + 5) + 5.ToString());
                    MyE.Kiir("Fertőtlenítés (m2)", MyE.Oszlopnév(oszlop + 6) + 5.ToString());
                    sor += 1;
                }
                if (előzőtípus.Trim() != rekord.Típus.Trim())
                {

                    MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop + 2) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                    MyE.Kiir(rekord.Típus.Trim(), MyE.Oszlopnév(oszlop + 2) + $"{sor}");
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlop + 1) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                    előzőtípus = rekord.Típus.Trim();
                    blokkeleje = sor;
                    sor += 1;
                }
                MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");

                sor += 1;

                if (sor == 46)
                {
                    MyE.Kiir("Össz", MyE.Oszlopnév(oszlop) + $"{sor}");
                    MyE.Kiir("=COUNTA(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C)", MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                    MyE.Rácsoz(MyE.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                    MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");

                    sor = 6;
                    blokkeleje = 6;
                    oszlop += 7;
                    oszlopismét += 1;
                }
            }

            if (sor > 45)
            {
                MyE.Kiir("Össz", MyE.Oszlopnév(oszlop) + $"{sor}");
                MyE.Kiir("=COUNTA(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C)", MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                MyE.Rácsoz(MyE.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                sor = 5;
                blokkeleje = 6;
                oszlop += 7;
                oszlopismét += 1;
            }
            else
            {
                sor += 3;
            }

            MyE.Kiir("Össz", MyE.Oszlopnév(oszlop) + $"{sor}");
            MyE.Kiir("=COUNTA(R[-" + Math.Abs(sor - blokkeleje).ToString() + "]C:R[-1]C)", MyE.Oszlopnév(oszlop + 1) + $"{sor}");
            MyE.Rácsoz(MyE.Oszlopnév(oszlop) + blokkeleje.ToString() + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
            MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");

            // **************************************************************
            // ha van olyan jármű ami másik telephelyről jött, akkor kiírjuk vége
            // **************************************************************

            // Maradék rácsozás
            if (sor < 46)
            {
                MyE.Rácsoz(MyE.Oszlopnév(oszlop) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + "46");
                MyE.Vastagkeret(MyE.Oszlopnév(oszlop) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + "46");
            }
            MyE.Munkalap_betű("Arial", 20);


            // első sor állítva
            MyE.SzövegIrány(munkalap, "5:5", 90);
            MyE.Sormagasság("5:5", 175);

            // összes oszlopszélesség 6
            MyE.Oszlopszélesség(munkalap, "a:" + MyE.Oszlopnév(oszlopismét * 7), 6);

            for (int j = 0; j < oszlopismét; j++)
            {
                // beállítjuk az oszlop psz szélességeket
                MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(1 + j * 7) + ":" + MyE.Oszlopnév(1 + j * 7), 15);

                // rácsozzuk
                MyE.Rácsoz(MyE.Oszlopnév(1 + j * 7) + "5:" + MyE.Oszlopnév(7 + j * 7) + "5");
                MyE.Vastagkeret(MyE.Oszlopnév(1 + j * 7) + "5:" + MyE.Oszlopnév(7 + j * 7) + "5");
            }

            if (oszlopismét < 3)
                oszlopismét = 3;
            MyE.NyomtatásiTerület_részletes(munkalap, "A1:" + MyE.Oszlopnév(7 + (oszlopismét - 1) * 7) + "65", 0.393700787401575d, 0.393700787401575d,
              0.590551181102362d, 0.31496062992126d, 0.31496062992126d, 0.31496062992126d, "1", "1");

            if (napszak == "de")
                MyE.Kiir("©J1 takarítás NAPPAL", "a3");
            else
                MyE.Kiir("©J1 takarítás ÉJSZAKA", "a3");

            MyE.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");

            MyE.Kiir("Előírt létszám:  …… fő, megjelent :……. Fő", "A49");
            MyE.Kiir("Cégjelzéses munkaruhát nem viselt : …….. Fő", "a51");

            MyE.Egyesít(munkalap, "a53:p53");
            MyE.Kiir("A megrendelő a napi munkatervet átadta: ……….óra……….perckor. ", "A53");
            MyE.Egyesít(munkalap, "a58:e58");
            MyE.Egyesít(munkalap, "g58:k58");
            MyE.Kiir("BKV ZRT.", "a58");
            MyE.Kiir("Vállalkozó", "g58");
            MyE.Pontvonal("A58:E58");
            MyE.Pontvonal("G58:K58");


            MyE.Egyesít(munkalap, "a59:p59");
            MyE.Kiir("A vállalkozó a napi munkafeladatok elvégzését lejelentette:", "A59");
            MyE.Egyesít(munkalap, "a61:p61");
            MyE.Kiir("  ……….óra……….perckor.", "a61");

            MyE.Egyesít(munkalap, "a65:e65");
            MyE.Egyesít(munkalap, "g65:k65");
            MyE.Kiir("BKV ZRT.", "a65");
            MyE.Kiir("Vállalkozó", "g65");
            MyE.Pontvonal("A65:E65");
            MyE.Pontvonal("G65:K65");

            MyE.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");
            MyE.Aktív_Cella(munkalap, "A1");

        }

        private void Összevont(string tétel, DateTime Dátum, string napszak,
            List<Adat_Jármű> AdatokJármű, List<Adat_Főkönyv_Nap> AdatokFőkönyvNap, List<string> AdatokTakarításTípus, List<Adat_Jármű_Vendég> AdatokFőVendég)
        {
            string munkalap;
            if (tétel.Trim() == "J1")
            {
                munkalap = "J1_J2_J3";
                MyE.Munkalap_aktív(munkalap);

                MyE.Kiir("J1 takarítás Nappal", "A4");
                MyE.Kiir("J2 takarítás Nappal", "H4");
                MyE.Kiir("J3 takarítás Nappal", "O4");
            }
            else
            {
                munkalap = "J4_J5_J6";
                MyE.Munkalap_aktív(munkalap);

                MyE.Kiir("J4 takarítás Nappal", "A4");
                MyE.Kiir("J5 takarítás Nappal", "H4");
                MyE.Kiir("J6 takarítás Nappal", "O4");
            }


            // minden cella

            MyE.Sormagasság("5:5", 175);


            // Oszlopszélesség("A:A", 10)
            MyE.Oszlopszélesség(munkalap, "A:A", 10);
            MyE.Oszlopszélesség(munkalap, "H:H", 10);
            MyE.Oszlopszélesség(munkalap, "O:O", 10);
            MyE.Oszlopszélesség(munkalap, "B:G", 6);
            MyE.Oszlopszélesség(munkalap, "I:N", 6);
            MyE.Oszlopszélesség(munkalap, "P:U", 6);


            MyE.Egyesít(munkalap, "a4:g4");
            MyE.Egyesít(munkalap, "h4:n4");
            MyE.Egyesít(munkalap, "o4:u4");
            MyE.Vastagkeret("a4:g4");
            MyE.Vastagkeret("h4:n4");
            MyE.Vastagkeret("o4:u4");
            int sor, oszlop;

            sor = 5;
            oszlop = 1;
            for (int szorzó = 0; szorzó <= 2; szorzó++)
            {
                // elkészítjük a fejlécet
                MyE.Kiir("Psz", MyE.Oszlopnév(oszlop + 7 * szorzó) + $"{sor}");
                MyE.Kiir("Kijelölve", MyE.Oszlopnév(oszlop + 7 * szorzó + 1) + $"{sor}");
                MyE.Kiir("Megfelelő", MyE.Oszlopnév(oszlop + 7 * szorzó + 2) + $"{sor}");
                MyE.Kiir("Nem Megfelelő", MyE.Oszlopnév(oszlop + 7 * szorzó + 3) + $"{sor}");
                MyE.Kiir("Graffiti (m2)", MyE.Oszlopnév(oszlop + 7 * szorzó + 4) + $"{sor}");
                MyE.Kiir("Eseti (m2)", MyE.Oszlopnév(oszlop + 7 * szorzó + 5) + $"{sor}");
                MyE.Kiir("Fertőtlenítés (m2)", MyE.Oszlopnév(oszlop + 7 * szorzó + 6) + $"{sor}");

            }
            string előzőtípus = "";

            foreach (string rekordkieg in AdatokTakarításTípus)
            {

                if (előzőtípus.Trim() != rekordkieg.Trim())
                {
                    // ha vége a típusnak, akkor kiírjuk az összesen sort
                    if (!string.IsNullOrEmpty(előzőtípus.Trim()))
                    {
                        // az utolsó után 3 üres sor
                        sor += 4;

                        MyE.Kiir("Össz", "a" + $"{sor}");
                        MyE.Kiir("Össz", "h" + $"{sor}");
                        MyE.Kiir("Össz", "o" + $"{sor}");
                        MyE.Kiir("1", "AA" + $"{sor}");
                    }

                    sor += 1;
                    MyE.Egyesít(munkalap, "a" + $"{sor}" + ":g" + $"{sor}");
                    MyE.Egyesít(munkalap, "h" + $"{sor}" + ":n" + $"{sor}");
                    MyE.Egyesít(munkalap, "o" + $"{sor}" + ":u" + $"{sor}");

                    MyE.Kiir(rekordkieg.Trim(), "a" + $"{sor}");
                    MyE.Kiir(rekordkieg.Trim(), "h" + $"{sor}");
                    MyE.Kiir(rekordkieg.Trim(), "o" + $"{sor}");

                    MyE.Kiir("1", "AA" + $"{sor}");
                    előzőtípus = rekordkieg.Trim();
                }

                List<Adat_Jármű> AdatokJárműSzűrt = (from a in AdatokJármű
                                                     where a.Típus == rekordkieg
                                                     orderby a.Azonosító ascending
                                                     select a).ToList();

                foreach (Adat_Jármű rekord in AdatokJárműSzűrt)
                {
                    sor += 1;

                    MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop) + $"{sor}");
                    MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop + 7) + $"{sor}");
                    MyE.Kiir(rekord.Azonosító.Trim(), MyE.Oszlopnév(oszlop + 14) + $"{sor}");
                    MyE.Kiir("0", "AA" + $"{sor}");

                    DateTime IdeigDátum = new DateTime(Dátum.Year, Dátum.Month, Dátum.Day, 14, 0, 0);
                    DateTime NullaDátum = new DateTime(1900, 1, 1, 0, 0, 0);
                    Adat_Főkönyv_Nap ElemNap;
                    if (napszak.Trim() == "de")
                        ElemNap = (from a in AdatokFőkönyvNap
                                   where a.Azonosító == rekord.Azonosító &&
                                   a.Tervérkezés < IdeigDátum &&
                                   a.Tervérkezés != NullaDátum
                                   select a).FirstOrDefault();
                    else
                        ElemNap = (from a in AdatokFőkönyvNap
                                   where a.Azonosító == rekord.Azonosító &&
                                   a.Tervérkezés >= IdeigDátum
                                   select a).FirstOrDefault();
                    // Ha volt forgalomban akkor csak a J1-hez írja be
                    if (ElemNap != null && tétel.Trim() == "J1") MyE.Kiir("X", MyE.Oszlopnév(oszlop + 1) + $"{sor}");

                    Adat_Jármű_Vendég VendégAdat = (from a in AdatokFőVendég
                                                    where a.Azonosító == rekord.Azonosító
                                                    select a).FirstOrDefault();

                    if (VendégAdat != null)
                    {
                        MyE.Kiir("", MyE.Oszlopnév(oszlop + 1) + $"{sor}");
                        MyE.Egyesít(munkalap, MyE.Oszlopnév(oszlop + 2) + $"{sor}" + ":" + MyE.Oszlopnév(oszlop + 6) + $"{sor}");
                        MyE.Kiir(VendégAdat.KiadóTelephely, MyE.Oszlopnév(oszlop + 2) + $"{sor}");
                    }
                }
            }

            sor += 1;
            MyE.Kiir("Össz", "a" + $"{sor}");
            MyE.Kiir("Össz", "h" + $"{sor}");
            MyE.Kiir("Össz", "o" + $"{sor}");
            MyE.Kiir("1", "AA" + $"{sor}");
            // ****************************
            // formázás
            // ****************************
            MyE.Munkalap_betű("Arial", 20);

            // első sor állítva
            MyE.SzövegIrány(munkalap, "5:5", 90);

            MyE.Rácsoz("A5:A" + $"{sor}");
            MyE.Vastagkeret("A5:A" + $"{sor}");
            MyE.Rácsoz("A5:G" + $"{sor}");
            MyE.Vastagkeret("A5:G" + $"{sor}");

            MyE.Rácsoz("H5:N" + $"{sor}");
            MyE.Vastagkeret("H5:N" + $"{sor}");

            MyE.Rácsoz("O5:U" + $"{sor}");
            MyE.Vastagkeret("O5:U" + $"{sor}");

            for (int j = 5; j < sor; j++)
            {
                if (MyE.Beolvas("AA" + j.ToString()) == "1")
                {
                    MyE.Vastagkeret("A" + j.ToString() + ":G" + j.ToString());
                    MyE.Vastagkeret("H" + j.ToString() + ":N" + j.ToString());
                    MyE.Vastagkeret("O" + j.ToString() + ":U" + j.ToString());
                }
            }

            sor += 2;
            MyE.Kiir("Előírt létszám:  …… fő, megjelent :……. Fő", "A" + $"{sor}");
            sor += 2;
            MyE.Kiir("Cégjelzéses munkaruhát nem viselt : …….. Fő", "a" + $"{sor}");
            sor += 2;
            MyE.Egyesít(munkalap, "a" + $"{sor}" + ":U" + $"{sor}");
            MyE.Kiir("A megrendelő a napi munkatervet átadta: ……….óra……….perckor. ", "A" + $"{sor}");
            sor += 5;
            MyE.Egyesít(munkalap, "a" + $"{sor}" + ":e" + $"{sor}");
            MyE.Egyesít(munkalap, "g" + $"{sor}" + ":k" + $"{sor}");

            MyE.Kiir("BKV ZRT.", "a" + $"{sor}");
            MyE.Kiir("Vállalkozó", "g" + $"{sor}");
            MyE.Pontvonal("a" + $"{sor}" + ":E" + $"{sor}");
            MyE.Pontvonal("g" + $"{sor}" + ":K" + $"{sor}");
            sor += 2;
            MyE.Egyesít(munkalap, "a" + $"{sor}" + ":u" + $"{sor}");
            MyE.Kiir("A vállalkozó a napi munkafeladatok elvégzését lejelentette:  ……….óra……….perckor.", "A" + $"{sor}");

            sor += 5;
            MyE.Egyesít(munkalap, "a" + $"{sor}" + ":e" + $"{sor}");
            MyE.Egyesít(munkalap, "g" + $"{sor}" + ":k" + $"{sor}");
            MyE.Kiir("BKV ZRT.", "a" + $"{sor}");
            MyE.Kiir("Vállalkozó", "g" + $"{sor}");
            MyE.Pontvonal("a" + $"{sor}" + ":E" + $"{sor}");
            MyE.Pontvonal("g" + $"{sor}" + ":K" + $"{sor}");

            MyE.NyomtatásiTerület_részletes(munkalap,
                                             "A1:U" + $"{sor}",
                                             "$1:$5",
                                             "",
                                             "", "", "", "", "", "", "",
                                             0.393700787401575d, 0.393700787401575d,
                                             0.590551181102362d, 0.748031496062992d,
                                             0.31496062992126d, 0.31496062992126d,
                                             true, false,
                                             "1", "false", true, "A4");

            if (napszak.Trim() == "de")
                MyE.Kiir("©Takarítás NAPPAL", "o1");
            else
                MyE.Kiir("©Takarítás ÉJSZAKA", "O1");

            MyE.Kiir(Dátum.ToString("yyyy.MM") + " hó " + Dátum.ToString("dd") + " nap", "a1");

            MyE.Aktív_Cella(munkalap, "A1");
        }
    }
}
