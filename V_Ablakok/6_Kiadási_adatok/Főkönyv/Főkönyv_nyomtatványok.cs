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
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Nyomtatványok
{
    public class Főkönyv_Főkönyv
    {
        #region Kezelők
        readonly Kezelő_Jármű_Állomány_Típus KézJárműÁllTípus = new Kezelő_Jármű_Állomány_Típus();
        readonly Kezelő_Főkönyv_SegédTábla KézSegédTábla = new Kezelő_Főkönyv_SegédTábla();
        readonly Kezelő_jármű_hiba KézJárműHiba = new Kezelő_jármű_hiba();
        readonly Kezelő_Menetkimaradás Kéz_Menet = new Kezelő_Menetkimaradás();
        #endregion


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


            List<Adat_Jármű_Állomány_Típus> típus = KézJárműÁllTípus.Lista_Adatok(Cmbtelephely.Trim());

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

            if (!File.Exists(hely)) Adatbázis_Létrehozás.Menekimaradás_telephely(hely);
            int napia = 0;
            int napib = 0;
            int napic = 0;
            int napi = 0;
            jelszó = "lilaakác";

            List<Adat_Menetkimaradás> Madatok = Kéz_Menet.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
            Madatok = (from a in Madatok
                       where a.Bekövetkezés >= MyF.Nap0000(Dátum)
                       && a.Bekövetkezés <= MyF.Nap2359(Dátum)
                       && a.Törölt == false
                       orderby a.Eseményjele
                       select a).ToList();

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

            Madatok = Kéz_Menet.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
            Madatok = (from a in Madatok
                       where a.Bekövetkezés >= MyF.Nap0000(MyF.Hónap_elsőnapja(Dátum))
                       && a.Bekövetkezés <= MyF.Nap2359(Dátum)
                       && a.Törölt == false
                       orderby a.Eseményjele
                       select a).ToList();

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
                5, 5, 5, 5,
                8, 8, "1", "1", false, "A3", true, true);

            MyE.Aktív_Cella(munkalap, "A1");
            MyE.ExcelMentés(fájlexc);
            MyE.ExcelBezárás();
            MyE.Megnyitás(fájlexc);
        }

        private void Jobb_Tervezet(string Cmbtelephely, DateTime Dátum)
        {

            string helykieg = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\segéd\Kiegészítő.mdb";


            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\adatok\hibanapló" + @"\" + Dátum.ToString("yyyyMM") + "hibanapló.mdb";

            int mennyi;
            if (System.IO.File.Exists(hely))
            {

                // csak azokat listázzuk amik be vannak jelölve

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
                    //szöveg = "";
                    //szöveg = "SELECT * FROM hibatábla where idő>=#" + Dátum.ToString("MM-dd-yyyy") + " 06:00:0#";
                    //szöveg += " and idő<#" + Dátum.AddDays(1).ToString("MM-dd-yyyy") + " 06:00:0#";
                    //szöveg += " and javítva=true";
                    //szöveg += " order by azonosító";
                    //Adatok = KJH_kéz.Lista_adatok(hely, jelszó, szöveg);

                    Adatok = KézJárműHiba.Lista_Adatok(Cmbtelephely.Trim(), Dátum);
                    Adatok = (from a in Adatok
                              where a.Idő >= MyF.Nap0600(Dátum)
                              && a.Idő < MyF.Nap0600(Dátum.AddDays(1))
                              && a.Javítva == true
                              orderby a.Azonosító
                              select a).ToList();
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
        //
        private void Jobb_Személyzet_Fejléc(string Cmbtelephely, DateTime Dátum)
        {
            // megnézzük, hogy volt-e személyzet hiány ezen a napon
            Kezelő_Főkönyv_Személyzet KFS_kéz = new Kezelő_Főkönyv_Személyzet();
            List<Adat_Főkönyv_Személyzet> SzAdatok = KFS_kéz.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
            SzAdatok = (from a in SzAdatok
                        where a.Dátum.ToShortDateString() == Dátum.ToShortDateString()
                        orderby a.Napszak, a.Típus
                        select a).ToList();
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
            Kezelő_Főkönyv_Típuscsere KFT_kéz = new Kezelő_Főkönyv_Típuscsere();
            List<Adat_FőKönyv_Típuscsere> Adatok = KFT_kéz.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
            Adatok = (from a in Adatok
                      where a.Dátum.ToShortDateString() == Dátum.ToShortDateString()
                      orderby a.Napszak, a.Típuselőírt
                      select a).ToList();

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


}
