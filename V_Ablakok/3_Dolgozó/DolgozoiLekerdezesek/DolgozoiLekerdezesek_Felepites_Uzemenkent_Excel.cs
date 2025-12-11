using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.V_Ablakok._3_Dolgozó.DolgozoiLekerdezesek
{
    public class DolgozoiLekerdezesek_Felepites_Uzemenkent_Excel
    {
        public void ExcelKimenetetKeszit(string fájlexc, string[] CmbTelephely, Kezelő_Dolgozó_Személyes KézSzemélyes, Kezelő_Kulcs KézKulcs, Kezelő_Kiegészítő_Csoportbeosztás KézCsop, Kezelő_Dolgozó_Alap KézDolg, Kezelő_Kulcs_Kettő KézKulcs2)
        {
            List<Adat_Kulcs> Adatok_Kulcs = new List<Adat_Kulcs>();
            string helykulcs = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Villamos\Kulcs.mdb";
            bool kulcsfájlvan = false;
            List<Adat_Kulcs> AdatokKulcs = null;
            if (File.Exists(helykulcs))
            {
                kulcsfájlvan = true;
                AdatokKulcs = KézKulcs.Lista_Adatok();
            }

            // létrehozzuk az excel táblát
            MyX.ExcelLétrehozás();

            Beállítás_Betű BeBetű = new Beállítás_Betű() { Név="Arial", Méret = 12 };
            Beállítás_Betű BeBetűVastag = new Beállítás_Betű() { Vastag = true };

            string munkalap = "Összesítő";
            MyX.Munkalap_átnevezés("Munka1", munkalap);
            MyX.Munkalap_betű(munkalap, BeBetű);

            // ****************************************************
            // elkészítjük a lapokat
            // ****************************************************

            for (int i = 0; i < CmbTelephely.Length; i++)
                MyX.Munkalap_Új(CmbTelephely[i]);

            int öoszlop = 2;
            List<Adat_Dolgozó_Személyes> AdatokSzemélyes = KézSzemélyes.Lista_Adatok();

            for (int ii = 0; ii < CmbTelephely.Length; ii++)
            {
                bool személyeseng = false;
                bool béreng = false;
                string Telephely = CmbTelephely[ii];
                if (kulcsfájlvan)
                {

                    string adat1 = Program.PostásNév.Trim().ToUpper();
                    string adat2 = Program.PostásTelephely.Trim().ToUpper();
                    string adat3 = "A";
                    személyeseng = KézKulcs.ABKULCSvan(adat1, adat2, adat3);

                    adat3 = "B";
                    béreng = KézKulcs.ABKULCSvan(adat1, adat2, adat3);
                }
                //Főholtart.Lép();
                munkalap = CmbTelephely[ii];
                MyX.Munkalap_aktív(munkalap);
                MyX.Munkalap_betű(munkalap, BeBetű);

                // elkészítjük a fejlécet
                MyX.Kiir("Sorszám", "a1");
                MyX.Kiir("Név", "b1");
                MyX.Kiir("Munkakör", "c1");
                MyX.Kiir("HR törzsszám", "d1");
                MyX.Kiir("Születési idő", "e1");
                MyX.Kiir("Belépési idő", "f1");
                MyX.Kiir("Bér", "g1");
                MyX.Kiir("Csoport", "h1");
                MyX.Kiir("Passzív", "i1");
                MyX.Kiir("Alkalmazott/fizikai", "j1");
                MyX.Kiir("Ide vezényelt", "k1");
                MyX.Kiir("Elvezényelve", "l1");
                MyX.Kiir("Részmunkaidős", "m1");

                // lenullázzuk
                int fizikai = 0;
                int alkalmazott = 0;
                int Vezényelt = 0;
                int Vezényelve = 0;
                int Részmunkaidős = 0;
                int passzív = 0;
                // leellenőrizzük, hogy minden munkahely ki van-e töltve.
                Munkahelyellenőrzés(CmbTelephely[ii]);

                List<Adat_Kiegészítő_Csoportbeosztás> AdatokCsop = KézCsop.Lista_Adatok(CmbTelephely[ii]);
                List<Adat_Dolgozó_Alap> AdatokDolg = KézDolg.Lista_Adatok(CmbTelephely[ii]);
                AdatokDolg = AdatokDolg.Where(a => a.Kilépésiidő == new DateTime(1900, 1, 1)).ToList();

                int i = 2;
                if (AdatokCsop.Count > 0 && AdatokDolg.Count > 0)
                {
                    //Alholtart.Be(AdatokCsop.Count + 1);

                    foreach (Adat_Kiegészítő_Csoportbeosztás Csoport in AdatokCsop)
                    {
                        //Alholtart.Lép();
                        List<Adat_Dolgozó_Alap> CsoportTagok = AdatokDolg.Where(Elem => Elem.Csoport.Trim() == Csoport.Csoportbeosztás.Trim()).ToList();

                        foreach (Adat_Dolgozó_Alap rekord in CsoportTagok)
                        {
                            MyX.Kiir($"#SZÁME#{(i - 1)}", $"a{i}");
                            MyX.Kiir(rekord.DolgozóNév.Trim(), $"b{i}");
                            MyX.Kiir(rekord.Munkakör.Trim(), $"c{i}");
                            MyX.Kiir($"#SZÁME#{rekord.Dolgozószám}", $"d{i}");

                            if (személyeseng)
                            {
                                Adat_Dolgozó_Személyes Elem = (from a in AdatokSzemélyes
                                                               where a.Dolgozószám == rekord.Dolgozószám
                                                               select a).FirstOrDefault();
                                if (Elem != null) MyX.Kiir(Elem.Születésiidő.ToString("yyyy.MM.dd"), $"e{i}");
                            }

                            MyX.Kiir(rekord.Belépésiidő.ToString("yyyy.MM.dd"), $"f{i}");

                            if (béreng)
                            {
                                string ideig = MyF.Rövidkód(rekord.Dolgozószám);

                                Adatok_Kulcs = KézKulcs2.Lista_Adatok();

                                Adat_Kulcs vane = Adatok_Kulcs.FirstOrDefault(a => a.Adat1.Contains(ideig));

                                if (vane != null)
                                {
                                    ideig = vane.Adat2;

                                    if (ideig != "_" && ideig != null)
                                    {
                                        string bére = MyF.Dekódolja(ideig);
                                        MyX.Kiir($"#SZÁME#{bére}", $"g{i}");
                                    }
                                }
                            }
                            MyX.Kiir(rekord.Csoport, $"h{i}");
                            if (rekord.Passzív)
                            {
                                MyX.Kiir("passzív", $"i{i}");
                                passzív++;
                            }
                            if (rekord.Alkalmazott)
                            {
                                MyX.Kiir("Alkalmazott", $"j{i}");
                                alkalmazott++;
                            }
                            else
                            {
                                MyX.Kiir("Fizikai", $"j{i}");
                                fizikai++;
                            }
                            if (rekord.Vezényelt)
                            {
                                MyX.Kiir("vezényelt", $"k{i}");
                                Vezényelt++;
                            }
                            if (rekord.Vezényelve)
                            {
                                MyX.Kiir("vezényelve", $"l{i}");
                                Vezényelve++;
                            }
                            if (rekord.Részmunkaidős)
                            {
                                MyX.Kiir("részmunkaidős", $"m{i}");
                                Részmunkaidős++;
                            }
                            i += 1;
                        }
                    }

                    //Nincs csoportban
                    List<Adat_Dolgozó_Alap> NincsTagok = AdatokDolg.Where(Elem => Elem.Csoport.Trim() == "Nincs").ToList();

                    foreach (Adat_Dolgozó_Alap rekord in NincsTagok)
                    {
                        MyX.Kiir((i - 1).ToString(), $"a{i}");
                        MyX.Kiir(rekord.DolgozóNév.Trim(), $"b{i}");
                        MyX.Kiir(rekord.Munkakör.Trim(), $"c{i}");
                        MyX.Kiir(rekord.Dolgozószám.Trim(), $"d{i}");

                        if (személyeseng)
                        {
                            Adat_Dolgozó_Személyes Elem = (from a in AdatokSzemélyes
                                                           where a.Dolgozószám == rekord.Dolgozószám
                                                           select a).FirstOrDefault();
                            if (Elem != null)
                                MyX.Kiir(Elem.Születésiidő.ToString("yyyy.MM.dd"), $"e{i}");
                        }

                        MyX.Kiir(rekord.Belépésiidő.ToString("yyyy.MM.dd"), $"f{i}");

                        if (béreng)
                        {
                            string ideig = MyF.Rövidkód(rekord.Dolgozószám);

                            Adatok_Kulcs = KézKulcs2.Lista_Adatok();
                            Adat_Kulcs vane = Adatok_Kulcs.FirstOrDefault(a => a.Adat1.Contains(ideig));
                            ideig = vane.Adat2;

                            if (ideig != "_")
                            {
                                MyX.Kiir(MyF.Dekódolja(ideig), $"g{i}");
                            }
                        }
                        MyX.Kiir(rekord.Csoport, $"h{i}");
                        if (rekord.Passzív)
                        {
                            MyX.Kiir("passzív", $"i{i}");
                            passzív++;
                        }
                        if (rekord.Alkalmazott)
                        {
                            MyX.Kiir("Alkalmazott", $"j{i}");
                            alkalmazott++;
                        }
                        else
                        {
                            MyX.Kiir("Fizikai", $"j{i}");
                            fizikai++;
                        }
                        if (rekord.Vezényelt)
                        {
                            MyX.Kiir("vezényelt", $"k{i}");
                            Vezényelt++;
                        }
                        if (rekord.Vezényelve)
                        {
                            MyX.Kiir("vezényelve", $"l{i}");
                            Vezényelve++;
                        }
                        if (rekord.Részmunkaidős)
                        {
                            MyX.Kiir("részmunkaidős", $"m{i}");
                            Részmunkaidős++;
                        }
                        i += 1;
                    }
                }
                MyX.Oszlopszélesség(munkalap, "A:M");
                MyX.Szűrés(munkalap, "A", "M", i);

                MyX.Vastagkeret(munkalap, $"A1:m{i}");
                MyX.Rácsoz(munkalap,$"A1:m{i}");
               
                i += 1;
                MyX.Kiir("Szellemi", $"b{i}");
                MyX.Kiir($"#SZÁME#{alkalmazott}", $"c{i}");

                MyX.Kiir("Fizikai", $"b{i + 1}");
                MyX.Kiir($"#SZÁME#{fizikai}", $"c{i + 1}");

                MyX.Kiir("Összesen", $"b{i + 2}");
                MyX.Kiir($"#SZÁME#{(fizikai + alkalmazott)}", $"c{i + 2}");

                MyX.Kiir("Vezényelve", $"b{i + 3}");
                MyX.Kiir($"#SZÁME#{Vezényelve}", $"c{i + 3}");

                MyX.Kiir("Vezényelt", $"b{i + 4}");
                MyX.Kiir($"#SZÁME#{Vezényelt}", $"c{i + 4}");

                MyX.Kiir("Részmunkaidős", $"b{i + 5}");
                MyX.Kiir($"#SZÁME#{Részmunkaidős}", $"c{i + 5}");

                MyX.Kiir("Passzív", $"b{i + 6}");
                MyX.Kiir($"#SZÁME#{passzív}", $"c{i + 6}");

                MyX.Vastagkeret(munkalap, $"b{i}:c{i + 6}");
                MyX.Rácsoz(munkalap, $"b{i}:c{i + 6}");
                MyX.Tábla_Rögzítés(munkalap, 1);

                // összesítő lapra kiírjuk telephelyenként
                munkalap = "Összesítő";
                MyX.Munkalap_aktív(munkalap);
                
                MyX.Kiir(Telephely, MyF.Oszlopnév(öoszlop) + "1");
                MyX.Kiir($"#SZÁME#{alkalmazott}", MyF.Oszlopnév(öoszlop) + "2");
                MyX.Kiir($"#SZÁME#{fizikai}", MyF.Oszlopnév(öoszlop) + "3");
                MyX.Kiir($"#SZÁME#{(fizikai + alkalmazott)}", MyF.Oszlopnév(öoszlop) + "4");               
                MyX.Betű(munkalap, MyF.Oszlopnév(öoszlop) + "4", BeBetűVastag);
                MyX.Kiir($"#SZÁME#{Vezényelve}", MyF.Oszlopnév(öoszlop) + "5");
                MyX.Kiir($"#SZÁME#{Vezényelt}", MyF.Oszlopnév(öoszlop) + "6");
                MyX.Kiir($"#SZÁME#{Részmunkaidős}", MyF.Oszlopnév(öoszlop) + "7");
                MyX.Kiir($"#SZÁME#{passzív}", MyF.Oszlopnév(öoszlop) + "8");

                MyX.Vastagkeret(munkalap, MyF.Oszlopnév(öoszlop) + "1:" + MyF.Oszlopnév(öoszlop) + "8");
                MyX.Rácsoz(munkalap, MyF.Oszlopnév(öoszlop) + "1:" + MyF.Oszlopnév(öoszlop) + "8");
                MyX.Oszlopszélesség("Összesítő", MyF.Oszlopnév(öoszlop) + ":" + MyF.Oszlopnév(öoszlop));

                öoszlop += 1;
            }
            munkalap = "Összesítő";
            MyX.Vastagkeret(munkalap, "a1:a8");
            MyX.Rácsoz(munkalap, "a1:a8");                      
            MyX.Kiir("Szellemi", "a2");
            MyX.Kiir("Fizikai", "a3");
            MyX.Kiir("Összesen", "a4");
            MyX.Betű(munkalap,"a4", BeBetűVastag);
            MyX.Kiir("Ide vezényelve", "a5");
            MyX.Kiir("Elvezényelt", "a6");
            MyX.Kiir("Részmunkaidős", "a7");
            MyX.Kiir("Passzív", "a8");
            MyX.Oszlopszélesség("Összesítő", "A:A");

            // összesítő oszlop
            MyX.Kiir("Összesen:", MyF.Oszlopnév(öoszlop) + "1");
            for (int i = 2; i < 9; i++)
                MyX.Kiir("#KÉPLET#=SUM(RC[-" + (öoszlop - 2).ToString() + "]:RC[-1])", MyF.Oszlopnév(öoszlop) + i);

            MyX.Betű(munkalap, MyF.Oszlopnév(öoszlop) + "4", BeBetű);
            MyX.Rácsoz(munkalap,MyF.Oszlopnév(öoszlop) + "1:" + MyF.Oszlopnév(öoszlop) + "8");
            MyX.Oszlopszélesség("Összesítő", MyF.Oszlopnév(öoszlop) + ":" + MyF.Oszlopnév(öoszlop));


            MyX.Munkalap_aktív("Összesítő");
            MyX.Aktív_Cella("Összesítő", "A1");
            MyX.ExcelMentés(fájlexc + ".xlsx");
            MyX.ExcelBezárás();

            MessageBox.Show("A fájl elkészült.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MyF.Megnyitás(fájlexc);
        }

        private void Munkahelyellenőrzés(string Telephely)
        {
            try
            {
                Kezelő_Dolgozó_Alap KézDolg = new Kezelő_Dolgozó_Alap();
                List<Adat_Dolgozó_Alap> AdatokDolgÖ = KézDolg.Lista_Adatok(Telephely);
                List<Adat_Dolgozó_Alap> AdatokDolg = (from a in AdatokDolgÖ
                                                      where a.Kilépésiidő.ToShortDateString() == "1900.01.01"
                                                      orderby a.DolgozóNév
                                                      select a).ToList();

                foreach (Adat_Dolgozó_Alap rekord in AdatokDolg)
                {
                    if (rekord.Csoport == null || rekord.Csoport.Trim() == "")
                    {
                        Adat_Dolgozó_Alap ADAT = new Adat_Dolgozó_Alap(rekord.Dolgozószám.Trim(),
                                                                       "Nincs",
                                                                       new DateTime(1900, 1, 1));
                        KézDolg.Módosít_Csoport(Telephely, ADAT);
                    }
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
    }
}
