using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.Villamos_Nyomtatványok
{
    public class Főkönyv_Főkönyv
    {
        #region Kezelők
        readonly Kezelő_Jármű_Állomány_Típus KézJárműÁllTípus = new Kezelő_Jármű_Állomány_Típus();
        readonly Kezelő_Főkönyv_SegédTábla KézSegédTábla = new Kezelő_Főkönyv_SegédTábla();
        readonly Kezelő_jármű_hiba KézJárműHiba = new Kezelő_jármű_hiba();
        readonly Kezelő_Menetkimaradás Kéz_Menet = new Kezelő_Menetkimaradás();
        readonly Kezelő_Jármű_Xnapos KJX_kéz = new Kezelő_Jármű_Xnapos();
        readonly Kezelő_Főkönyv_Nap KFN_kép = new Kezelő_Főkönyv_Nap();
        readonly Kezelő_kiegészítő_Hibaterv KKH_kéz = new Kezelő_kiegészítő_Hibaterv();
        readonly Kezelő_Dolgozó_Alap kézDolg = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_Kiegészítő_főkönyvtábla KKF_kéz = new Kezelő_Kiegészítő_főkönyvtábla();
        #endregion

        readonly Beállítás_Betű BeBetű = new Beállítás_Betű();
        readonly Beállítás_Betű BeBetű8 = new Beállítás_Betű { Méret = 8 };
        readonly Beállítás_Betű BeBetű10 = new Beállítás_Betű { Méret = 10 };
        readonly Beállítás_Betű BeBetű16 = new Beállítás_Betű { Méret = 16 };
        readonly Beállítás_Betű BeBetű20 = new Beállítás_Betű { Méret = 20 };
        readonly Beállítás_Betű BeBetűV = new Beállítás_Betű { Vastag = true };
        readonly Beállítás_Betű BeBetűD = new Beállítás_Betű { Dőlt = true };
        readonly Beállítás_Betű BeBetűVD = new Beállítás_Betű { Dőlt = true, Vastag = true };
        readonly Beállítás_Betű BeBetűAD = new Beállítás_Betű { Aláhúzott = true, Dőlt = true };
        readonly Beállítás_Betű BeBetűAV = new Beállítás_Betű { Aláhúzott = true, Vastag = true };
        readonly Beállítás_Betű BeBetűS = new Beállítás_Betű { Szín = Color.Yellow };

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
            MyX.ExcelLétrehozás(munkalap);

            MyX.Munkalap_betű(munkalap, BeBetű);

            // oszlop szélességeket beállítjuk az alapot
            MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(1) + ":" + MyF.Oszlopnév(1), 7);
            MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(2) + ":" + MyF.Oszlopnév(11), 9);

            // elkészítjük a fejlécet
            MyX.Betű(munkalap, "A1:l1", BeBetű16);

            MyX.Egyesít(munkalap, "A1:d1");
            MyX.Kiir(Cmbtelephely.Trim() + " Üzem", "a1");
            MyX.Egyesít(munkalap, "e1:i1");
            MyX.Kiir("Főkönyv", "e1");
            MyX.Egyesít(munkalap, "j1:l1");
            MyX.Kiir(szövegd, "j1");
            sor = 2;

            MyX.Kiir("Visz.", "a" + $"{sor}");
            MyX.Egyesít(munkalap, "b" + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
            MyX.Kiir("I. Járműállomány", "b" + $"{sor}");

            oszlop = 12;
            eleje = 12;


            List<Adat_Jármű_Állomány_Típus> típus = KézJárműÁllTípus.Lista_Adatok(Cmbtelephely.Trim());

            foreach (Adat_Jármű_Állomány_Típus rekord in típus)
            {
                MyX.Kiir(rekord.Típus.Trim(), MyF.Oszlopnév(oszlop) + $"{sor}");

                MyX.Oszlopszélesség(munkalap, (MyF.Oszlopnév(oszlop) + ":" + MyF.Oszlopnév(oszlop)));
                oszlop += 1;
            }

            utolsó = oszlop;

            MyX.Rácsoz(munkalap, "a" + $"{sor}" + ":" + MyF.Oszlopnév(oszlop - 1) + $"{sor}");


            // napi adatok tábla
            // megnézzük kicsinálta
            List<Adat_Főkönyv_SegédTábla> Adatoksegéd = KézSegédTábla.Lista_adatok(Cmbtelephely.Trim(), Dátum, napszak.Trim());
            Adat_Főkönyv_SegédTábla ElemSegéd = Adatoksegéd.Where(y => y.Id == 1).FirstOrDefault();

            if (ElemSegéd != null)
                kicsinálta = ElemSegéd.Bejelentkezésinév;
            else
                kicsinálta = "*";

            // ******************************************
            // ***** Forgalomba adott járművek kezdete **
            // ******************************************
            List<Adat_Főkönyv_Nap> Adatok = KFN_kép.Lista_Adatok(Cmbtelephely.Trim(), Dátum, napszak.Trim());
            Adatok = (from a in Adatok
                      where a.Viszonylat.Trim() != "-"
                      orderby a.Viszonylat.Trim(), a.Tényindulás, a.Forgalmiszám, a.Azonosító.Trim()
                      select a).ToList();
            if (napszak == "de")
                Adatok = Adatok.Where(a => a.Napszak.Trim() == "DE").ToList();
            else
                Adatok = Adatok.Where(a => a.Napszak.Trim() == "DU").ToList();
            sor += 1;
            oszlop1 = 2;
            viszonylatelőző = "";
            szerelvényhossz = 0;
            szerelvényhossz1 = 0;

            // *********************************
            // ide kell írni a forgalmi kocsikat
            // *********************************

            MyX.Kiir("Forgalomba adott járművek", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűVD);
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            MyX.Háttérszín(munkalap, MyF.Oszlopnév(1) + $"{sor}", Color.GreenYellow);
            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");

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
                        MyX.Kiir(rekord.Viszonylat.Trim(), MyF.Oszlopnév(1) + $"{sor}");
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
                            MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString());
                            // ha beálló akkor színez
                            if (rekord.Tényérkezés.Hour > 7 && rekord.Tényérkezés.Hour < 12)
                                MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), Color.Orange);

                            // ha beállóba kért akkor dőlt betű
                            if (rekord.Státus == 3)
                                MyX.Betű(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), BeBetűVD);

                            //Ha rossz kocsi van forgalomban
                            if (rekord.Státus == 4)
                            {
                                MyX.Betű(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), BeBetűVD);
                                MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), Color.Red);
                                MyX.Betű(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), BeBetűS);
                            }

                            if (rekord.Megjegyzés.Trim().ToUpper().Substring(0, 1) == "T") // ha T betűvel kezdődik többlet kiadás
                            {

                                MyX.Betű(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), BeBetűD);
                                MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), Color.LightSkyBlue);
                            }
                            // személyzet hiány
                            if (rekord.Megjegyzés.Trim().ToUpper().Substring(0, 1) == "S") // ha s betűvel kezdődik személyzet hiány
                            {
                                MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), Color.GreenYellow);
                                MyX.Betű(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), BeBetűV);

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
                                        MyX.Kiir(sordarab[j].ToString(), MyF.Oszlopnév(eleje + j) + $"{sor}");
                                        forgalombanösszesen[j] = forgalombanösszesen[j] + sordarab[j];
                                        MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyF.Oszlopnév(eleje + j) + (sor + szerelvényhossz).ToString());
                                        MyX.Igazít_függőleges(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyF.Oszlopnév(eleje + j) + (sor + szerelvényhossz).ToString(), "közép");
                                    }

                                }
                                // megformázzuk a sort(sorokat)
                                if (szerelvényhossz > 0)
                                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + (sor + szerelvényhossz).ToString());

                                // viszonylatot egyesít
                                MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(1) + (sor + szerelvényhossz).ToString());
                                MyX.Igazít_függőleges(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(1) + (sor + szerelvényhossz).ToString(), "közép");
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
                                    MyX.Kiir(sordarab[j].ToString(), MyF.Oszlopnév(eleje + j) + $"{sor}");
                                    forgalombanösszesen[j] = forgalombanösszesen[j] + sordarab[j];
                                    MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyF.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString());
                                    MyX.Igazít_függőleges(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyF.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString(), "közép");
                                }

                            }
                            // megformázzuk a sort(sorokat)
                            if (szerelvényhossz1 > 0)
                            {
                                MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + (sor + szerelvényhossz1).ToString());
                            }
                            // viszonylatot egyesít
                            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(1) + (sor + szerelvényhossz1).ToString());
                            MyX.Igazít_függőleges(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(1) + (sor + szerelvényhossz1).ToString(), "közép");
                            // lenullázuk a darabszámokat
                            for (int j = 0; j < típus.Count; j++)
                            {
                                sordarab[j] = 0;
                            }
                            // ha különböző lesz akkor kihagyunk egy sort és visszamegyünk az első oszlophoz
                            sor = sor + 1 + Convert.ToInt32(szerelvényhossz1);
                        }
                        oszlop1 = 2;
                        MyX.Kiir(rekord.Viszonylat.Trim(), MyF.Oszlopnév(1) + $"{sor}");
                        MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString());
                        if (rekord.Kocsikszáma > 1)
                        {
                            nemelső = 1;
                        }
                        // ha beálló akkor színez
                        if (rekord.Tényérkezés.Hour > 7 && rekord.Tényérkezés.Hour < 12)
                        {
                            MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), Color.Orange);
                        }
                        // ha beállóba kért akkor dőlt betű
                        if (rekord.Státus == 3)
                        {
                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), BeBetűVD);
                        }
                        if (rekord.Megjegyzés.Trim().ToUpper().Substring(0, 1) == "T") // ha T betűvel kezdődik többlet kiadás
                        {

                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), BeBetűAD);
                            MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), Color.LightSkyBlue);
                        }
                        // személyzet hiány
                        if (rekord.Megjegyzés.Trim().ToUpper().Substring(0, 1) == "S") // ha s betűvel kezdődik személyzet hiány
                        {
                            MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), Color.GreenYellow);
                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), BeBetűV);

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
                    MyX.Kiir(sordarab[j].ToString(), MyF.Oszlopnév(eleje + j) + $"{sor}");
                    forgalombanösszesen[j] = forgalombanösszesen[j] + sordarab[j];
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyF.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString());
                    MyX.Igazít_függőleges(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyF.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString().Trim(), "közép");
                }
            }
            // megformázzuk a sort(sorokat)
            MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + (sor + szerelvényhossz1).ToString());
            // viszonylatot egyesít
            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(1) + (sor + szerelvényhossz1).ToString());
            MyX.Igazít_függőleges(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(1) + (sor + szerelvényhossz1).ToString(), "közép");
            sor = sor + szerelvényhossz1 + 1;

            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
            MyX.Kiir("Forgalomban Összesen:", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(1) + $"{sor}", "jobb");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűVD);
            // típusonként összeadjuk a forgalomban lévőket
            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyX.Kiir(forgalombanösszesen[j].ToString(), MyF.Oszlopnév(eleje + j) + $"{sor}");
                    MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}", "közép");
                    MyX.Betű(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}", BeBetűVD);
                }

            }
            MyX.Rácsoz(munkalap, "a" + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");

            sor += 1;
            // ******************************************
            // ***** Forgalomba adott járművek Vége    **
            // ******************************************

            // '**********************************
            // 'ide kell személyzet hiányt
            // '**********************************
            MyX.Kiir("Személyzet hiány", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűV);
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            MyX.Háttérszín(munkalap, MyF.Oszlopnév(1) + $"{sor}", Color.GreenYellow);

            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            sor += 1;

            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
            MyX.Kiir("Személyzet hiány Összesen:", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűVD);
            MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(1) + $"{sor}", "jobb");

            // típusonként összeadjuk a forgalomban lévőket

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyX.Kiir(személyzet[j].ToString(), MyF.Oszlopnév(eleje + j) + $"{sor}");
                    MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}", "közép");
                    MyX.Betű(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}", BeBetűVD);
                }
            }
            MyX.Rácsoz(munkalap, "a" + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            MyX.Rácsoz(munkalap, "a" + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            sor += 1;

            // **********************************
            // ide kell írni a tartalék kocsikat
            // **********************************
            MyX.Kiir("Üzemképes Tartalék", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűV);
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            MyX.Háttérszín(munkalap, MyF.Oszlopnév(1) + $"{sor}", Color.GreenYellow);

            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");

            // tartalékok kiírása
            Adatok = KFN_kép.Lista_Adatok(Cmbtelephely.Trim(), Dátum, napszak.Trim());
            Adatok = (from a in Adatok
                      where a.Napszak.Trim() == "_"
                      orderby a.Típus, a.Kocsikszáma descending, a.Szerelvény, a.Azonosító
                      select a).ToList();


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
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + (sor + szerelvényhossz1).ToString());
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(1) + (sor + szerelvényhossz1).ToString());
                    // ha különböző lesz akkor kiírjuk a darabszámokat
                    for (int j = 0; j < típus.Count; j++)
                    {
                        if (típus[j].Típus.Trim() != "")
                        {
                            MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyF.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString());
                            MyX.Igazít_függőleges(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyF.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString(), "közép");
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
                        MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + (sor + szerelvényhossz1).ToString());
                        MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(1) + (sor + szerelvényhossz1).ToString());
                        // ha különböző lesz akkor kiírjuk a darabszámokat

                        for (int j = 0; j < típus.Count; j++)
                        {
                            if (típus[j].Típus.Trim() != "")
                            {
                                MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyF.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString());
                                MyX.Igazít_függőleges(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyF.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString(), "közép");
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
                        MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + (sor + szerelvényhossz1).ToString());
                        MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(1) + (sor + szerelvényhossz1).ToString());
                        // ha különböző lesz akkor kiírjuk a darabszámokat
                        for (int j = 0; j < típus.Count; j++)
                        {
                            if (típus[j].Típus.Trim() != "")
                            {
                                MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyF.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString());
                                MyX.Igazít_függőleges(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyF.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString(), "közép");
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
                        MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString());

                        MyX.Kiir(rekord.Típus.Trim(), "a" + $"{sor}");
                        if (rekord.Státus == 4)
                        {
                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), BeBetűV);
                            MyX.Háttérszín(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), Color.Red);
                            MyX.Betű(munkalap, MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString(), BeBetűS);
                        }
                        szerelvényhossz += 1;
                    }
                    else if (rekord.Státus != 4)
                    {
                        MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString());

                        MyX.Kiir(rekord.Típus.Trim(), "a" + $"{sor}");

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
                        MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(oszlop1) + (sor + szerelvényhossz).ToString());

                        MyX.Kiir(rekord.Típus.Trim(), "a" + $"{sor}");
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
                MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + (sor + szerelvényhossz1).ToString());
                MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(1) + (sor + szerelvényhossz1).ToString());
                MyX.Igazít_függőleges(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(1) + (sor + szerelvényhossz1).ToString(), "közép");
                // ha különböző lesz akkor kiírjuk a darabszámokat

                for (int j = 0; j < típus.Count; j++)
                {
                    if (típus[j].Típus.Trim() != "")
                    {
                        MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyF.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString());
                        MyX.Igazít_függőleges(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyF.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString(), "közép");
                    }
                }
                oszlop1 = 2;
                sor = sor + 1 + szerelvényhossz1;
                szerelvényhossz1 = szerelvényhossz;
            } // ha volt adat akkor formáz


            // megformázzuk a sort(sorokat)
            MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + (sor + szerelvényhossz1).ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(1) + (sor + szerelvényhossz1).ToString());
            // ha különböző lesz akkor kiírjuk a darabszámokat

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyF.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString());
                    MyX.Igazít_függőleges(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}" + ":" + MyF.Oszlopnév(eleje + j) + (sor + szerelvényhossz1).ToString(), "közép");
                }
            }



            // tartalék Összesítő
            sor = sor + 1 + szerelvényhossz1;
            // sor = sor + szerelvényhossz1
            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
            MyX.Kiir("Tartalék Összesen:", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(1) + $"{sor}", "jobb");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűVD);
            // típusonként összeadjuk a tartalékokat

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyX.Kiir(tartalék[j].ToString(), MyF.Oszlopnév(eleje + j) + $"{sor}");
                    MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}", "közép");
                    MyX.Betű(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}", BeBetűVD);
                }
            }
            MyX.Rácsoz(munkalap, "a" + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");

            sor += 1;

            // ******************************************
            // Üzemképes Összesen
            // ******************************************
            MyX.Kiir("Üzemképes Villamosok", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűV);
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            MyX.Háttérszín(munkalap, MyF.Oszlopnév(1) + $"{sor}", Color.GreenYellow);

            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");

            // üzemképes Összesítő
            sor += 1;
            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
            MyX.Kiir("Üzemképes Összesen:", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(1) + $"{sor}", "jobb");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűVD);
            // típusonként összeadjuk a tartalékokat

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyX.Kiir((tartalék[j] + forgalombanösszesen[j] + személyzet[j]).ToString(), MyF.Oszlopnév(eleje + j) + $"{sor}");
                    MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}", "közép");
                    MyX.Betű(munkalap, MyF.Oszlopnév(eleje + j) + $"{sor}", BeBetűVD);
                }
            }
            MyX.Rácsoz(munkalap, "a" + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");


            // ******************************************
            // ide kerül a kocsiszíni javítás
            // ******************************************

            sor += 1;
            MyX.Kiir("Kocsiszíni javítás", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűV);
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            MyX.Háttérszín(munkalap, MyF.Oszlopnév(1) + $"{sor}", Color.GreenYellow);

            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");

            sor += 1;

            MyX.Kiir("Psz", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűD);
            MyX.Egyesít(munkalap, MyF.Oszlopnév(2) + $"{sor}" + ":" + MyF.Oszlopnév(3) + $"{sor}");
            MyX.Kiir("Dátum", MyF.Oszlopnév(2) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(2) + $"{sor}", BeBetűD);
            MyX.Egyesít(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
            MyX.Kiir("Javítás leírása", MyF.Oszlopnév(4) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(4) + $"{sor}", BeBetűD);
            MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop - 1) + $"{sor}");


            int soreleje = sor;
            int sorvége;
            Adatok = KFN_kép.Lista_Adatok(Cmbtelephely.Trim(), Dátum, napszak.Trim());
            Adatok = (from a in Adatok
                      where a.Státus == 4 && (a.Napszak.Trim() == "-" || a.Napszak.Trim() == "_")
                      orderby a.Azonosító
                      select a).ToList();

            foreach (Adat_Főkönyv_Nap rekord in Adatok)
            {
                if (!rekord.Hibaleírása.Contains("#") && !rekord.Hibaleírása.Contains("&") && !rekord.Hibaleírása.Contains("§"))
                {
                    sor++;
                    if (soreleje == 0)
                        soreleje = sor;
                    MyX.Kiir(rekord.Azonosító.Trim(), "a" + $"{sor}");

                    MyX.Egyesít(munkalap, MyF.Oszlopnév(2) + $"{sor}" + ":" + MyF.Oszlopnév(3) + $"{sor}");
                    MyX.Kiir(rekord.Miótaáll.ToString("yyyy.MM.dd"), MyF.Oszlopnév(2) + $"{sor}");

                    MyX.Egyesít(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
                    MyX.Kiir(rekord.Hibaleírása.Trim(), MyF.Oszlopnév(4) + $"{sor}");

                    MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(4) + $"{sor}", "bal");
                    if (rekord.Hibaleírása.Trim().Length > 75)
                    {
                        int sor_magasság = ((rekord.Hibaleírása.Length / 75) + 1) * 15;
                        MyX.Sormagasság(munkalap, sor.ToString() + ":" + $"{sor}", sor_magasság);
                        MyX.Egyesít(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
                        MyX.Sortörésseltöbbsorba(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}", true);
                    }

                    // kiválasztjuk melyik típus
                    for (int j = 0; j < típus.Count; j++)
                    {
                        if (rekord.Típus.Trim() == típus[j].Típus.Trim())
                        {
                            javításon[j] = javításon[j] + 1;
                            MyX.Kiir("1", MyF.Oszlopnév(12 + j) + $"{sor}");
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
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyF.Oszlopnév(utolsó - 1) + sorvége.ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyF.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                }
                if (soreleje + 1 <= sorvége)
                {
                    // ha több sor
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + soreleje.ToString() + ":" + MyF.Oszlopnév(utolsó - 1) + sorvége.ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyF.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                }
            }

            sor++;
            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
            MyX.Kiir("Kocsiszíni javítás Összesen:", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(1) + $"{sor}", "jobb");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűVD);

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyX.Kiir(javításon[j].ToString(), MyF.Oszlopnév(12 + j) + $"{sor}");
                    MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(12 + j) + $"{sor}", "közép");
                    MyX.Betű(munkalap, MyF.Oszlopnév(12 + j) + $"{sor}", BeBetűVD);
                }

            }
            MyX.Rácsoz(munkalap, "a" + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            // MyX.Vastagkeret(munkalap, "a" + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            // ******************************************
            // ide kerül a kocsiszíni javítás  vége
            // ******************************************

            // ******************************************
            // ide kerül a telepen kívüli javítás
            // ******************************************
            sor++;
            MyX.Kiir("Telephelyen kívüli javítás", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűV);
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            MyX.Háttérszín(munkalap, MyF.Oszlopnév(1) + $"{sor}", Color.GreenYellow);

            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");

            sor += 1;

            MyX.Kiir("Psz", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűD);
            MyX.Egyesít(munkalap, MyF.Oszlopnév(2) + $"{sor}" + ":" + MyF.Oszlopnév(3) + $"{sor}");
            MyX.Kiir("Dátum", MyF.Oszlopnév(2) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(2) + $"{sor}", BeBetűD);
            MyX.Egyesít(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
            MyX.Kiir("Javítás leírása", MyF.Oszlopnév(4) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(4) + $"{sor}", BeBetűD);
            MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop - 1) + $"{sor}");

            Adatok = KFN_kép.Lista_Adatok(Cmbtelephely.Trim(), Dátum, napszak.Trim());
            Adatok = (from a in Adatok
                      where a.Státus == 4 && a.Viszonylat == "-"
                      orderby a.Azonosító
                      select a).ToList();
            soreleje = sor;

            foreach (Adat_Főkönyv_Nap rekord in Adatok)
            {

                if (rekord.Hibaleírása.Contains("§"))
                {
                    sor++;
                    if (soreleje == 0)
                        soreleje = sor;
                    MyX.Kiir(rekord.Azonosító.Trim(), "a" + $"{sor}");

                    MyX.Egyesít(munkalap, MyF.Oszlopnév(2) + $"{sor}" + ":" + MyF.Oszlopnév(3) + $"{sor}");
                    MyX.Kiir(rekord.Miótaáll.ToString("yyyy.MM.dd"), MyF.Oszlopnév(2) + $"{sor}");

                    MyX.Egyesít(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
                    MyX.Kiir(rekord.Hibaleírása.Trim(), MyF.Oszlopnév(4) + $"{sor}");

                    MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(4) + $"{sor}", "bal");

                    if (rekord.Hibaleírása.Trim().Length > 75)
                    {
                        int sor_magasság = ((rekord.Hibaleírása.Length / 75) + 1) * 15;
                        MyX.Sormagasság(munkalap, sor.ToString() + ":" + $"{sor}", sor_magasság);
                        MyX.Egyesít(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
                        MyX.Sortörésseltöbbsorba(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}", true);
                    }

                    // kiválasztjuk melyik típus
                    for (int j = 0; j < típus.Count; j++)
                    {
                        if (rekord.Típus.Trim() == típus[j].Típus.Trim())
                        {
                            telepenkívül[j] = telepenkívül[j] + 1;
                            MyX.Kiir("1", MyF.Oszlopnév(12 + j) + $"{sor}");
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
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyF.Oszlopnév(utolsó - 1) + sorvége.ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyF.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                }
                if (soreleje + 1 <= sorvége)
                {
                    // ha több sor
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + soreleje.ToString() + ":" + MyF.Oszlopnév(utolsó - 1) + sorvége.ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyF.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                }
            }


            sor++;
            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
            MyX.Kiir("Telephelyen kívüli javítás Összesen:", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(1) + $"{sor}", "jobb");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűVD);

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyX.Kiir(telepenkívül[j].ToString(), MyF.Oszlopnév(12 + j) + $"{sor}");
                    MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(12 + j) + $"{sor}", "közép");
                    MyX.Betű(munkalap, MyF.Oszlopnév(12 + j) + $"{sor}", BeBetűVD);
                }

            }
            MyX.Rácsoz(munkalap, "a" + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            // MyX.Vastagkeret(munkalap, "a" + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            // ******************************************
            // ide kerül a telepen kívüli javítás vége
            // ******************************************


            // ***********************************
            // ide a félre áLlítás
            // ***********************************
            // fejléc
            sor++;
            MyX.Kiir("Félreállítás", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűV);
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            MyX.Háttérszín(munkalap, MyF.Oszlopnév(1) + $"{sor}", Color.GreenYellow);

            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");

            sor += 1;

            MyX.Kiir("Psz", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűD);
            MyX.Egyesít(munkalap, MyF.Oszlopnév(2) + $"{sor}" + ":" + MyF.Oszlopnév(3) + $"{sor}");
            MyX.Kiir("Dátum", MyF.Oszlopnév(2) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(2) + $"{sor}", BeBetűD);
            MyX.Egyesít(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
            MyX.Kiir("Javítás leírása", MyF.Oszlopnév(4) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(4) + $"{sor}", BeBetűD);
            MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop - 1) + $"{sor}");

            soreleje = 0;

            foreach (Adat_Főkönyv_Nap rekord in Adatok)
            {
                if (rekord.Hibaleírása.Contains("&"))

                {
                    sor++;
                    if (soreleje == 0)
                        soreleje = sor;
                    MyX.Kiir(rekord.Azonosító.Trim(), "a" + $"{sor}");

                    MyX.Egyesít(munkalap, MyF.Oszlopnév(2) + $"{sor}" + ":" + MyF.Oszlopnév(3) + $"{sor}");
                    MyX.Kiir(rekord.Miótaáll.ToString("yyyy.MM.dd"), MyF.Oszlopnév(2) + $"{sor}");

                    MyX.Egyesít(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
                    MyX.Kiir(rekord.Hibaleírása.Trim(), MyF.Oszlopnév(4) + $"{sor}");

                    MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(4) + $"{sor}", "bal");
                    if (rekord.Hibaleírása.Trim().Length > 75)
                    {
                        int sor_magasság = ((rekord.Hibaleírása.Length / 75) + 1) * 15;
                        MyX.Sormagasság(munkalap, sor.ToString() + ":" + $"{sor}", sor_magasság);
                        MyX.Egyesít(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
                        MyX.Sortörésseltöbbsorba(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}", true);
                    }

                    // kiválasztjuk melyik típus
                    for (int j = 0; j < típus.Count; j++)
                    {
                        if (rekord.Típus.Trim() == típus[j].Típus.Trim())
                        {
                            félreállítás[j] = félreállítás[j] + 1;
                            MyX.Kiir("1", MyF.Oszlopnév(12 + j) + $"{sor}");
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
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyF.Oszlopnév(utolsó - 1) + sorvége.ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyF.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                }
                if (soreleje < sorvége)
                {
                    // ha több sor
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + soreleje.ToString() + ":" + MyF.Oszlopnév(utolsó - 1) + sorvége.ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyF.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                }
            }


            sor++;
            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
            MyX.Kiir("Félre állítás Összesen:", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(1) + $"{sor}", "jobb");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűVD);

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyX.Kiir(félreállítás[j].ToString(), MyF.Oszlopnév(12 + j) + $"{sor}");
                    MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(12 + j) + $"{sor}", "közép");
                    MyX.Betű(munkalap, MyF.Oszlopnév(12 + j) + $"{sor}", BeBetűVD);
                }
            }
            MyX.Rácsoz(munkalap, "a" + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");

            // ***********************************
            // ide a félre áLlítás vége
            // ***********************************


            // ********************************************
            // ide a Főjavítás
            // ********************************************
            // fejléc
            sor++;
            MyX.Kiir("Főjavítás", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűV);
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            MyX.Háttérszín(munkalap, MyF.Oszlopnév(1) + $"{sor}", Color.GreenYellow);

            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");

            sor++;


            MyX.Kiir("Psz", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűD);
            MyX.Egyesít(munkalap, MyF.Oszlopnév(2) + $"{sor}" + ":" + MyF.Oszlopnév(3) + $"{sor}");
            MyX.Kiir("Dátum", MyF.Oszlopnév(2) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(2) + $"{sor}", BeBetűD);
            MyX.Egyesít(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
            MyX.Kiir("Javítás leírása", MyF.Oszlopnév(4) + $"{sor}");
            MyX.Betű(munkalap, MyF.Oszlopnév(4) + $"{sor}", BeBetűD);
            MyX.Egyesít(munkalap, MyF.Oszlopnév(eleje) + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(oszlop - 1) + $"{sor}");
            soreleje = 0;

            foreach (Adat_Főkönyv_Nap rekord in Adatok)
            {

                if (rekord.Hibaleírása.Contains("#"))
                {
                    sor++;
                    if (soreleje == 0)
                        soreleje = sor;
                    MyX.Kiir(rekord.Azonosító.Trim(), "a" + $"{sor}");

                    MyX.Egyesít(munkalap, MyF.Oszlopnév(2) + $"{sor}" + ":" + MyF.Oszlopnév(3) + $"{sor}");
                    MyX.Kiir(rekord.Miótaáll.ToString("yyyy.MM.dd"), MyF.Oszlopnév(2) + $"{sor}");

                    MyX.Egyesít(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
                    MyX.Kiir(rekord.Hibaleírása.Trim(), MyF.Oszlopnév(4) + $"{sor}");

                    MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(4) + $"{sor}", "bal");
                    if (rekord.Hibaleírása.Trim().Length > 75)
                    {
                        int sor_magasság = ((rekord.Hibaleírása.Length / 75) + 1) * 15;
                        MyX.Sormagasság(munkalap, sor.ToString() + ":" + $"{sor}", sor_magasság);
                        MyX.Egyesít(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
                        MyX.Sortörésseltöbbsorba(munkalap, MyF.Oszlopnév(4) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}", true);
                    }

                    // kiválasztjuk melyik típus
                    for (int j = 0; j < típus.Count; j++)
                    {
                        if (rekord.Típus.Trim() == típus[j].Típus.Trim())
                        {
                            főjavítás[j] = főjavítás[j] + 1;
                            MyX.Kiir("1", MyF.Oszlopnév(12 + j) + $"{sor}");
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
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyF.Oszlopnév(utolsó - 1) + sorvége.ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyF.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                }
                if (soreleje + 1 <= sorvége)
                {
                    // ha több sor
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + soreleje.ToString() + ":" + MyF.Oszlopnév(utolsó - 1) + sorvége.ToString());
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(1) + (soreleje - 1).ToString() + ":" + MyF.Oszlopnév(oszlop - 1) + (soreleje - 1).ToString());
                }
            }

            sor++;
            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
            MyX.Kiir("Főjavítás Összesen:", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(1) + $"{sor}", "jobb");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűVD);

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyX.Kiir(főjavítás[j].ToString(), MyF.Oszlopnév(12 + j) + $"{sor}");
                    MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(12 + j) + $"{sor}", "közép");
                    MyX.Betű(munkalap, MyF.Oszlopnév(12 + j) + $"{sor}", BeBetűVD);
                }
            }
            MyX.Rácsoz(munkalap, "a" + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");

            // ********************************************
            // ide a Főjavítás vége 
            // ********************************************


            sor++;
            MyX.Egyesít(munkalap, MyF.Oszlopnév(1) + $"{sor}" + ":" + MyF.Oszlopnév(11) + $"{sor}");
            MyX.Kiir("Összesen:", MyF.Oszlopnév(1) + $"{sor}");
            MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(1) + $"{sor}", "jobb");
            MyX.Betű(munkalap, MyF.Oszlopnév(1) + $"{sor}", BeBetűVD);

            for (int j = 0; j < típus.Count; j++)
            {
                if (típus[j].Típus.Trim() != "")
                {
                    MyX.Kiir((forgalombanösszesen[j] + tartalék[j] + javításon[j] + félreállítás[j] + főjavítás[j] + telepenkívül[j] + személyzet[j]).ToString(), MyF.Oszlopnév(12 + j) + $"{sor}");
                    MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(12 + j) + $"{sor}", "közép");
                    MyX.Betű(munkalap, MyF.Oszlopnév(12 + j) + $"{sor}", BeBetűVD);
                }
            }

            MyX.Rácsoz(munkalap, "a" + $"{sor}" + ":" + MyF.Oszlopnév(utolsó - 1) + $"{sor}");
            MyX.Oszlopszélesség(munkalap, "A:A");

            // *******************************************
            // **********A táblázat jobb oldala***********
            // *******************************************

            újsor = 1;
            utolsó++;
            MyX.Oszlopszélesség(munkalap, MyF.Oszlopnév(utolsó) + ":" + MyF.Oszlopnév(utolsó + 14), 10);
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyX.Kiir("Kocsiállomány Jelentés", MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Betű(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString(), BeBetű20);
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 9) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyX.Kiir(szövegd, MyF.Oszlopnév(utolsó + 9) + újsor.ToString());
            MyX.Betű(munkalap, MyF.Oszlopnév(utolsó + 9) + újsor.ToString(), BeBetű20);

            újsor += 1;
            Jobb_kategória("II. Események");

            újsor += 1;
            Menet_fejléc();

            újsor += 3;

            // egyesítjük kettesével
            // itt kell majd beolvasni a menetkimaradásokat.
            int napia = 0;
            int napib = 0;
            int napic = 0;
            int napi = 0;

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
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó) + (újsor + 1).ToString());
                    MyX.Kiir(rekord.Eseményjele.ToUpper(), MyF.Oszlopnév(utolsó) + újsor.ToString());
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
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 1) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 1) + (újsor + 1).ToString());
                    MyX.Kiir(rekord.Viszonylat.Trim(), MyF.Oszlopnév(utolsó + 1) + újsor.ToString());

                    MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 2) + (újsor + 1).ToString());
                    MyX.Kiir(rekord.Típus.Trim(), MyF.Oszlopnév(utolsó + 2) + újsor.ToString());

                    MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 3) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 3) + (újsor + 1).ToString());
                    MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(utolsó + 3) + újsor.ToString());

                    MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 12) + (újsor + 1).ToString());
                    MyX.Sortörésseltöbbsorba(munkalap, MyF.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 12) + (újsor + 1).ToString(), true);

                    MyX.Kiir(rekord.Jvbeírás.Trim() + " - " + rekord.Vmbeírás.Trim() + "-" + rekord.Javítás.Trim(), MyF.Oszlopnév(utolsó + 4) + újsor.ToString());

                    MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 13) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 13) + (újsor + 1).ToString());
                    MyX.Kiir(rekord.Bekövetkezés.ToString("hh: mm"), MyF.Oszlopnév(utolsó + 13) + újsor.ToString());

                    MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 14) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + (újsor + 1).ToString());
                    MyX.Kiir(rekord.Kimaradtmenet.ToString(), MyF.Oszlopnév(utolsó + 14) + újsor.ToString());

                    napi += Convert.ToInt32(rekord.Kimaradtmenet);

                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + (újsor + 1).ToString());
                    újsor += 2;
                }
            }
            else
            {
                // nincs adat
                MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó) + (újsor + 1).ToString());
                MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 1) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 1) + (újsor + 1).ToString());
                MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 2) + (újsor + 1).ToString());
                MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 3) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 3) + (újsor + 1).ToString());
                MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 12) + (újsor + 1).ToString());
                MyX.Kiir("Nincs adat", MyF.Oszlopnév(utolsó + 4) + újsor.ToString());
                MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 13) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 13) + (újsor + 1).ToString());
                MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 14) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + (újsor + 1).ToString());
                MyX.Rácsoz(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + (újsor + 1).ToString());
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


            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyX.Kiir("Napi \"A\"", MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyX.Kiir(napia.ToString(), MyF.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyX.Kiir("Göngyölt \"A\"", MyF.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 6) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyX.Kiir(göngya.ToString(), MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 8) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 13) + újsor.ToString());
            MyX.Kiir("Napi összes kimaradt menet:", MyF.Oszlopnév(utolsó + 8) + újsor.ToString());
            MyX.Kiir(napi.ToString(), MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyX.Rácsoz(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());

            újsor += 1;
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyX.Kiir("Napi \"B\"", MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyX.Kiir(napib.ToString(), MyF.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyX.Kiir("Göngyölt \"B\"", MyF.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 6) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyX.Kiir(göngyb.ToString(), MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 8) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 13) + újsor.ToString());
            MyX.Kiir("Göngyölt összes kimaradt menet:", MyF.Oszlopnév(utolsó + 8) + újsor.ToString());
            MyX.Kiir(göngymenet.ToString(), MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyX.Rácsoz(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());

            újsor += 1;
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyX.Kiir("Napi \"C\"", MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyX.Kiir(napic.ToString(), MyF.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyX.Kiir("Göngyölt \"C\"", MyF.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 6) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyX.Kiir(göngyc.ToString(), MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 8) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyX.Rácsoz(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());

            újsor += 1;
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyX.Kiir("Összesen:", MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyX.Kiir((napia + napib + napic).ToString(), MyF.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyX.Kiir("Összesen:", MyF.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 6) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyX.Kiir((göngya + göngyb + göngyc).ToString(), MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 8) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyX.Rácsoz(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());

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
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 7) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString(), "közép");
            MyX.Kiir("Érkező járművek", MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(utolsó + 7) + újsor.ToString(), "közép");
            MyX.Kiir("Átadott járművek", MyF.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó + 7) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            újsor++;
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 1) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 8) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 10) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 11) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyX.Kiir("Psz", MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Kiir("Típus", MyF.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyX.Kiir("Telephely", MyF.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyX.Kiir("Psz", MyF.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyX.Kiir("Típus", MyF.Oszlopnév(utolsó + 8) + újsor.ToString());
            MyX.Kiir("Telephely", MyF.Oszlopnév(utolsó + 11) + újsor.ToString());
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó + 7) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            // **************************************************
            // ******* Aláírás helyek                    ********
            // **************************************************

            újsor += 2;
            MyX.Kiir("Kiállította:", MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Kiir("Ellenőrizte:", MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyX.Kiir("Látta:", MyF.Oszlopnév(utolsó + 12) + újsor.ToString());
            újsor += 3;
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 6) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 8) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 12) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó) + (újsor + 1).ToString() + ":" + MyF.Oszlopnév(utolsó + 2) + (újsor + 1).ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 6) + (újsor + 1).ToString() + ":" + MyF.Oszlopnév(utolsó + 8) + (újsor + 1).ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 12) + (újsor + 1).ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + (újsor + 1).ToString());
            // Kiirjuk a készítő nevét és beosztását
            List<Adat_Dolgozó_Alap> AdatokDolg = kézDolg.Lista_Adatok(Cmbtelephely.Trim());
            Adat_Dolgozó_Alap Adat = AdatokDolg.Where(a => a.Bejelentkezésinév == kicsinálta.Trim()).FirstOrDefault();
            string dolgozónév = "_";
            string főkönyvtitulus = "_";

            if (Adat != null)
            {
                dolgozónév = Adat.DolgozóNév == null ? "_" : Adat.DolgozóNév.Trim();
                főkönyvtitulus = Adat.Főkönyvtitulus == null ? "_" : Adat.Főkönyvtitulus.Trim();
            }


            MyX.Kiir(dolgozónév, MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Kiir(főkönyvtitulus, MyF.Oszlopnév(utolsó) + (újsor + 1).ToString());

            // MyX.Kiirjuk a személyeket az ellenőrző személyeket
            int ii = 6;

            List<Adat_Kiegészítő_főkönyvtábla> adatok = KKF_kéz.Lista_Adatok(Cmbtelephely.Trim());

            foreach (Adat_Kiegészítő_főkönyvtábla rekord in adatok)
            {
                MyX.Kiir(rekord.Név.Trim(), MyF.Oszlopnév(utolsó + ii) + újsor.ToString());
                MyX.Kiir(rekord.Beosztás.Trim(), MyF.Oszlopnév(utolsó + ii) + (újsor + 1).ToString());
                ii += 6;
            }


            MyX.Betű(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + (újsor + 1).ToString(), BeBetű10);
            MyX.Betű(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + (újsor + 1).ToString(), BeBetűV);

            újsor += 3;

            MyX.Kiir("Jelölés magyarázat:", MyF.Oszlopnév(utolsó) + újsor.ToString());
            újsor += 1;
            MyX.Kiir("1111", MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Háttérszín(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString(), Color.Orange);
            MyX.Kiir("Beálló", MyF.Oszlopnév(utolsó + 1) + újsor.ToString());

            MyX.Kiir("2222", MyF.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyX.Háttérszín(munkalap, MyF.Oszlopnév(utolsó + 2) + újsor.ToString(), Color.Orange);
            MyX.Betű(munkalap, MyF.Oszlopnév(utolsó + 2) + újsor.ToString(), BeBetűVD);
            MyX.Kiir("Beálló és a műszak bekérte", MyF.Oszlopnév(utolsó + 3) + újsor.ToString());

            MyX.Kiir("3333", MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyX.Háttérszín(munkalap, MyF.Oszlopnév(utolsó + 6) + újsor.ToString(), Color.GreenYellow);
            MyX.Betű(munkalap, MyF.Oszlopnév(utolsó + 6) + újsor.ToString(), BeBetűV);
            MyX.Kiir("Személyzet hiány", MyF.Oszlopnév(utolsó + 7) + újsor.ToString());

            MyX.Kiir("4444", MyF.Oszlopnév(utolsó + 9) + újsor.ToString());
            MyX.Betű(munkalap, MyF.Oszlopnév(utolsó + 9) + újsor.ToString(), BeBetűV);
            MyX.Háttérszín(munkalap, MyF.Oszlopnév(utolsó + 9) + újsor.ToString(), Color.Red);
            MyX.Betű(munkalap, MyF.Oszlopnév(utolsó + 9) + újsor.ToString(), BeBetűS);
            MyX.Kiir("Üzemképtelen", MyF.Oszlopnév(utolsó + 10) + újsor.ToString());

            MyX.Kiir("5555", MyF.Oszlopnév(utolsó + 12) + újsor.ToString());
            MyX.Háttérszín(munkalap, MyF.Oszlopnév(utolsó + 12) + újsor.ToString(), Color.LightSkyBlue);
            MyX.Betű(munkalap, MyF.Oszlopnév(utolsó + 12) + újsor.ToString(), BeBetűAV);
            MyX.Kiir("Többlet kiadás", MyF.Oszlopnév(utolsó + 13) + újsor.ToString());

            //***************************************
            //*Nyomtatási beállítások
            //***************************************
            if (sor > újsor) újsor = sor;
            Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
            {
                Munkalap = munkalap,
                NyomtatásiTerület = $"A1:{MyF.Oszlopnév(utolsó + 14)}{újsor}",
                BalMargó = 5,
                JobbMargó = 5,
                FelsőMargó = 5,
                AlsóMargó = 5,
                FejlécMéret = 8,
                LáblécMéret = 8,
                LapMagas = 1,
                LapSzéles = 1,
                Papírméret = "A3",
                Álló = false,
                VízKözép = true,
                FüggKözép = true
            };
            MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);
            MyX.ExcelMentés(fájlexc);
            MyX.ExcelBezárás();
            MyF.Megnyitás(fájlexc);
        }

        private void Jobb_Tervezet(string Cmbtelephely, DateTime Dátum)
        {

            string helykieg = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\segéd\Kiegészítő.mdb";


            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Trim()}\Adatok\hibanapló" + @"\" + Dátum.ToString("yyyyMM") + "hibanapló.mdb";

            int mennyi;
            if (System.IO.File.Exists(hely))
            {

                // csak azokat listázzuk amik be vannak jelölve

                List<Adat_Jármű_hiba> Adatok;


                List<Adat_Kiegészítő_Hibaterv> KiAdatokÖ = KKH_kéz.Lista_Adatok(Cmbtelephely.Trim());
                List<Adat_Kiegészítő_Hibaterv> KiAdatok = (from a in KiAdatokÖ
                                                           where a.Főkönyv == true
                                                           select a).ToList();

                foreach (Adat_Kiegészítő_Hibaterv rekordkieg in KiAdatok)
                {
                    újsor += 1;
                    mennyi = 3;
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 3) + újsor.ToString());
                    MyX.Kiir(rekordkieg.Szöveg.Trim(), MyF.Oszlopnév(utolsó) + újsor.ToString());
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
                                újsor += 1;
                                MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 3) + újsor.ToString());
                                MyX.Kiir(rekordkieg.Szöveg.Trim(), MyF.Oszlopnév(utolsó) + újsor.ToString());
                                mennyi = 4;
                            }
                            MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(utolsó + mennyi) + újsor.ToString());
                        }
                    }
                    MyX.Rácsoz(munkalap, MyF.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
                    mennyi = 4;
                }
            }
        }

        private void Jobb_Tervezet_fejléc()
        {

            újsor += 1;
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyX.Kiir("Karbantartás", MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(utolsó + 4) + újsor.ToString(), "közép");
            MyX.Kiir("Pályaszám(ok)", MyF.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó + 4) + újsor.ToString());
        }

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
                            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 4) + újsor.ToString());
                            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó + 5) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 9) + újsor.ToString());
                            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó + 10) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
                            MyX.Kiir(rekord.Viszonylat.Trim() + "/" + rekord.Forgalmiszám.Trim(), MyF.Oszlopnév(utolsó) + újsor.ToString());
                            MyX.Kiir(rekord.Tervindulás.ToString("hh: mm"), MyF.Oszlopnév(utolsó + 1) + újsor.ToString());
                            MyX.Kiir(rekord.Típus.Trim(), MyF.Oszlopnév(utolsó + 2) + újsor.ToString());
                            MyX.Kiir(rekord.Napszak.Trim(), MyF.Oszlopnév(utolsó + 3) + újsor.ToString());
                            MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(utolsó + 4) + újsor.ToString());
                            break;
                        }
                    case 2:
                        {
                            MyX.Kiir(rekord.Viszonylat.Trim() + "/" + rekord.Forgalmiszám.Trim(), MyF.Oszlopnév(utolsó + 5) + újsor.ToString());
                            MyX.Kiir(rekord.Tervindulás.ToString("hh: mm"), MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
                            MyX.Kiir(rekord.Típus.Trim(), MyF.Oszlopnév(utolsó + 7) + újsor.ToString());
                            MyX.Kiir(rekord.Napszak.Trim(), MyF.Oszlopnév(utolsó + 8) + újsor.ToString());
                            MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(utolsó + 9) + újsor.ToString());
                            break;
                        }
                    case 3:
                        {
                            MyX.Kiir(rekord.Viszonylat.Trim() + "/" + rekord.Forgalmiszám.Trim(), MyF.Oszlopnév(utolsó + 10) + újsor.ToString());
                            MyX.Kiir(rekord.Tervindulás.ToString("hh: mm"), MyF.Oszlopnév(utolsó + 11) + újsor.ToString());
                            MyX.Kiir(rekord.Típus.ToString(), MyF.Oszlopnév(utolsó + 12) + újsor.ToString());
                            MyX.Kiir(rekord.Napszak.Trim(), MyF.Oszlopnév(utolsó + 13) + újsor.ToString());
                            MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
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
            MyX.Kiir("Visz./forg.", MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Kiir("Ind. idő", MyF.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyX.Kiir("Típus", MyF.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyX.Kiir("Napszak", MyF.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyX.Kiir("Psz", MyF.Oszlopnév(utolsó + 4) + újsor.ToString());

            MyX.Kiir("Visz./forg.", MyF.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyX.Kiir("Ind. idő", MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyX.Kiir("Típus", MyF.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyX.Kiir("Napszak", MyF.Oszlopnév(utolsó + 8) + újsor.ToString());
            MyX.Kiir("Psz", MyF.Oszlopnév(utolsó + 9) + újsor.ToString());

            MyX.Kiir("Visz./forg.", MyF.Oszlopnév(utolsó + 10) + újsor.ToString());
            MyX.Kiir("Ind. idő", MyF.Oszlopnév(utolsó + 11) + újsor.ToString());
            MyX.Kiir("Típus", MyF.Oszlopnév(utolsó + 12) + újsor.ToString());
            MyX.Kiir("Napszak", MyF.Oszlopnév(utolsó + 13) + újsor.ToString());
            MyX.Kiir("Psz", MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó + 5) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 9) + újsor.ToString());
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó + 10) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
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
                    MyX.Kiir(rekord.Viszonylat.Trim() + "/" + rekord.Forgalmiszám.Trim(), MyF.Oszlopnév(utolsó + 8) + újsor.ToString());
                    MyX.Kiir(rekord.Tervindulás.ToString("hh: mm"), MyF.Oszlopnév(utolsó + 9) + újsor.ToString());
                    MyX.Kiir(rekord.Típuselőírt.Trim(), MyF.Oszlopnév(utolsó + 10) + újsor.ToString());
                    MyX.Kiir(rekord.Típuskiadott.Trim(), MyF.Oszlopnév(utolsó + 12) + újsor.ToString());
                    MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
                    ik = 2;
                }
                else
                {
                    // ha páratlan
                    újsor += 1;
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 3) + újsor.ToString());
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 5) + újsor.ToString());
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 10) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 11) + újsor.ToString());
                    MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 12) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 13) + újsor.ToString());
                    MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 7) + újsor.ToString());
                    MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó + 8) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
                    MyX.Kiir(rekord.Viszonylat.Trim() + "/" + rekord.Forgalmiszám.Trim(), MyF.Oszlopnév(utolsó) + újsor.ToString());
                    MyX.Kiir(rekord.Tervindulás.ToString("hh: mm"), MyF.Oszlopnév(utolsó + 1) + újsor.ToString());
                    MyX.Kiir(rekord.Típuselőírt.Trim(), MyF.Oszlopnév(utolsó + 2) + újsor.ToString());
                    MyX.Kiir(rekord.Típuskiadott.Trim(), MyF.Oszlopnév(utolsó + 4) + újsor.ToString());
                    MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
                    ik = 1;
                }

            }

        }

        private void Jobb_Típuscsere_fejléc()
        {
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 10) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 11) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 12) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 13) + újsor.ToString());
            MyX.Kiir("Visz./forg.", MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Kiir("Visz./forg.", MyF.Oszlopnév(utolsó + 8) + újsor.ToString());
            MyX.Kiir("Ind. idő", MyF.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyX.Kiir("Ind. idő", MyF.Oszlopnév(utolsó + 9) + újsor.ToString());
            MyX.Kiir("Előírt típus", MyF.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyX.Kiir("Előírt típus", MyF.Oszlopnév(utolsó + 10) + újsor.ToString());
            MyX.Kiir("Kiadott típus", MyF.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyX.Kiir("kiadott típus", MyF.Oszlopnév(utolsó + 12) + újsor.ToString());
            MyX.Kiir("Psz", MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyX.Kiir("Psz", MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó + 8) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
        }

        private void Jobb_NapiVáltozások(string Cmbtelephely, DateTime Dátum)
        {

            // ha létezik a fájl akkor készít

            int álldb = 0;
            int készdb = 0;
            int darab;

            List<Adat_Jármű_Xnapos> AdatokX = KJX_kéz.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
            AdatokX = (from a in AdatokX
                       where a.Kezdődátum.ToShortDateString() == Dátum.ToShortDateString()
                       orderby a.Azonosító
                       select a).ToList();
            if (AdatokX != null) álldb = AdatokX.Count;

            AdatokX = KJX_kéz.Lista_Adatok(Cmbtelephely.Trim());
            AdatokX = (from a in AdatokX
                       where a.Kezdődátum >= MyF.Nap0000(Dátum) &&
                       a.Kezdődátum <= MyF.Nap2359(Dátum)
                       orderby a.Azonosító
                       select a).ToList();
            if (AdatokX != null) álldb = AdatokX.Count;

            AdatokX = KJX_kéz.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
            AdatokX = (from a in AdatokX
                       where a.Kezdődátum >= MyF.Nap0000(Dátum) &&
                       a.Kezdődátum <= MyF.Nap2359(Dátum)
                       orderby a.Azonosító
                       select a).ToList();
            if (AdatokX != null) készdb = AdatokX.Count;


            if (készdb > álldb)
                darab = készdb;
            else
                darab = álldb;

            // elkészítjük az üres táblázatot
            for (int i = 1; i <= darab; i++)
            {
                MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 1) + (újsor + i).ToString() + ":" + MyF.Oszlopnév(utolsó + 5) + (újsor + i).ToString());
                MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 7) + (újsor + i).ToString() + ":" + MyF.Oszlopnév(utolsó + 8) + (újsor + i).ToString());
                MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 10) + (újsor + i).ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + (újsor + i).ToString());
            }

            List<Adat_Jármű_Xnapos> Adatok = KJX_kéz.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
            Adatok = (from a in Adatok
                      where a.Végdátum >= MyF.Nap0000(Dátum) &&
                      a.Végdátum <= MyF.Nap2359(Dátum)
                      orderby a.Azonosító
                      select a).ToList();

            int ji = 1;
            foreach (Adat_Jármű_Xnapos rekord in Adatok)
            {
                MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(utolsó + 6) + (újsor + ji).ToString());
                MyX.Kiir(rekord.Kezdődátum.ToString("yyyy.MM.dd"), MyF.Oszlopnév(utolsó + 7) + (újsor + ji).ToString());
                MyX.Kiir((rekord.Végdátum - rekord.Kezdődátum).Days.ToString(), MyF.Oszlopnév(utolsó + 9) + (újsor + ji).ToString());
                MyX.Kiir(rekord.Hibaleírása.Trim(), MyF.Oszlopnév(utolsó + 10) + (újsor + ji).ToString());
                ji += 1;
            }

            Adatok = KJX_kéz.Lista_Adatok(Cmbtelephely.Trim(), Dátum.Year);
            Adatok = (from a in Adatok
                      where a.Kezdődátum.ToShortDateString() == Dátum.ToShortDateString()
                      orderby a.Azonosító
                      select a).ToList();

            ji = 1;

            foreach (Adat_Jármű_Xnapos rekord in Adatok)
            {
                MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(utolsó) + (újsor + ji).ToString());
                MyX.Kiir(rekord.Hibaleírása.Trim(), MyF.Oszlopnév(utolsó + 1) + (újsor + ji).ToString());
                ji += 1;
            }

            Adatok = KJX_kéz.Lista_Adatok(Cmbtelephely.Trim());
            Adatok = (from a in Adatok
                      where a.Kezdődátum.ToShortDateString() == Dátum.ToShortDateString()
                      orderby a.Azonosító
                      select a).ToList();

            foreach (Adat_Jármű_Xnapos rekord in Adatok)
            {
                MyX.Kiir(rekord.Azonosító.Trim(), MyF.Oszlopnév(utolsó) + (újsor + ji).ToString());
                MyX.Kiir(rekord.Hibaleírása.Trim(), MyF.Oszlopnév(utolsó + 1) + (újsor + ji).ToString());
                ji += 1;
            }

            újsor += darab;
        }

        private void Jobb_Napiváltozások_fejléc()
        {

            újsor += 1;
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyX.Kiir("Leálló Kocsik", MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 6) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyX.Kiir("Elkészült kocsik", MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó + 6) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());

            újsor += 1;
            MyX.Kiir("Psz", MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Kiir("Psz", MyF.Oszlopnév(utolsó + 6) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 1) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyX.Kiir("Oka", MyF.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 7) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 8) + újsor.ToString());
            MyX.Kiir("Mióta", MyF.Oszlopnév(utolsó + 7) + újsor.ToString());
            MyX.Kiir("Állás Nap", MyF.Oszlopnév(utolsó + 9) + újsor.ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 10) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyX.Kiir("Oka", MyF.Oszlopnév(utolsó + 10) + újsor.ToString());
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 5) + újsor.ToString());
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó + 6) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
        }

        private void Menet_fejléc()
        {
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó) + (újsor + 2).ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 1) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 1) + (újsor + 2).ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 2) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 2) + (újsor + 2).ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 3) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 3) + (újsor + 2).ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 4) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 12) + (újsor + 2).ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 13) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 13) + (újsor + 2).ToString());
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó + 14) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + (újsor + 2).ToString());
            MyX.Rácsoz(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + (újsor + 2).ToString());


            MyX.Kiir("Esemény jele", MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Kiir("Viszonylat", MyF.Oszlopnév(utolsó + 1) + újsor.ToString());
            MyX.Kiir("Típus", MyF.Oszlopnév(utolsó + 2) + újsor.ToString());
            MyX.Sortörésseltöbbsorba(munkalap, MyF.Oszlopnév(utolsó + 3) + újsor.ToString(), true);
            MyX.Kiir("Meghibásodott jármű pályaszáma", MyF.Oszlopnév(utolsó + 3) + újsor.ToString());
            MyX.Kiir("Forgalmi esemény vagy járműhiba rövid leírása", MyF.Oszlopnév(utolsó + 4) + újsor.ToString());
            MyX.Sortörésseltöbbsorba(munkalap, MyF.Oszlopnév(utolsó + 13) + újsor.ToString(), true);
            MyX.Kiir("Esemény időpontja ", MyF.Oszlopnév(utolsó + 13) + újsor.ToString());
            MyX.Sortörésseltöbbsorba(munkalap, MyF.Oszlopnév(utolsó + 14) + újsor.ToString(), true);
            MyX.Kiir("Kimaradt menetek száma", MyF.Oszlopnév(utolsó + 14) + újsor.ToString());

            MyX.Betű(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + (újsor + 2), BeBetű8);

        }

        private void Jobb_kategória(string név)
        {
            MyX.Egyesít(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyX.Igazít_vízszintes(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString(), "közép");
            MyX.Kiir(név, MyF.Oszlopnév(utolsó) + újsor.ToString());
            MyX.Vastagkeret(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString());
            MyX.Háttérszín(munkalap, MyF.Oszlopnév(utolsó) + újsor.ToString() + ":" + MyF.Oszlopnév(utolsó + 14) + újsor.ToString(), Color.GreenYellow);
        }
    }


}
