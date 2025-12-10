using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos.V_Ablakok._3_Dolgozó.Szatube
{
    public class Szatube_NyomtatasSzabi
    {

        List<string> NyomtatásiFájlok = new List<string>();

        // Jelenlegi állapotot tekintve az előbb megbeszéltek (külön fájlba való mentés) még nem készültek el
        public Szatube_NyomtatasSzabi(Kezelő_Szatube_Szabadság kézSzabadság, string cmbTelephely, int adat_Évek)
        {
            KézSzabadság = kézSzabadság;
            CmbTelephely = cmbTelephely;
            Adat_Évek = adat_Évek;
        }

        public Kezelő_Szatube_Szabadság KézSzabadság { get; set; }
        public string CmbTelephely { get; set; }
        public int Adat_Évek { get; set; }

        public void Kiir(string fájlexcel, DataGridView Tábla, List<double> SzűrtLista, List<Adat_Szatube_Szabadság> Adatok)
        {
            NyomtatásiFájlok.Clear();
            string könyvtár = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string munkalap = "Munka1";
            DateTime IdeigDátum;
            string utolsókivettnap;
            // excel tábla megnyitása
            MyX.ExcelMegnyitás(fájlexcel);
            MyX.Munkalap_aktív(munkalap);
            int elem = 0;
         
            for (int i = 0; i < SzűrtLista.Count; i++)
            {
                elem++;
                List<Adat_Szatube_Szabadság> Szabadság = (from a in Adatok
                                                          where a.Sorszám == SzűrtLista[i]
                                                          select a).ToList();
                double mostKivesz = Szabadság.Sum(szám => szám.Kivettnap);
                Adat_Szatube_Szabadság Kezdet = Szabadság.First(a => a.Kezdődátum == Szabadság.Min(b => b.Kezdődátum));
                Adat_Szatube_Szabadság Vége = Szabadság.First(a => a.Kezdődátum == Szabadság.Max(b => b.Kezdődátum));

                utolsókivettnap = Tábla.SelectedRows[i].Cells[5].Value.ToString();
                switch (elem)
                {
                    case 1:
                        {
                            // Amit itt nem sikerült még tesztelnem, az az, hogy az Adat_Évek megfelelő értéket helyeznek-e be.
                            IdeigDátum = Kezdet.Kezdődátum;
                            MyX.Kiir(Kezdet.Sorszám + " /_" + Adat_Évek, "m1");
                            MyX.Kiir(Kezdet.Szabiok.Trim(), "f4");
                            MyX.Kiir(DateTime.Now.Year.ToString(), "i5");
                            MyX.Kiir(DateTime.Now.Month.ToString(), "k5");
                            MyX.Kiir(DateTime.Now.Day.ToString(), "m5");
                            MyX.Kiir(Kezdet.Dolgozónév.Trim(), "B9");
                            MyX.Kiir(Kezdet.Törzsszám.Trim(), "i9");
                            MyX.Kiir(Kezdet.Kezdődátum.ToString("yyyy.MM.dd"), "b27");
                            MyX.Kiir(Vége.Befejeződátum.ToString("yyyy.MM.dd"), "B30");
                            MyX.Kiir(mostKivesz.ToString(), "g27");
                            MyX.Kiir(IdeigDátum.Year.ToString(), "d17");
                            MyX.Kiir(Összesnapja(Kezdet.Törzsszám.Trim()).ToString(), "g17");
                            MyX.Kiir(IdeigDátum.Year.ToString(), "d21");
                            MyX.Kiir(Kivettnapja(Kezdet.Törzsszám.Trim(), IdeigDátum).ToString(), "g21");
                            MyX.Kiir(CmbTelephely, "B13");
                            break;
                        }
                    case 2:
                        {
                            IdeigDátum = Kezdet.Kezdődátum;
                            MyX.Kiir(Kezdet.Sorszám + " /_" + Adat_Évek, "ab1");
                            MyX.Kiir(Kezdet.Szabiok.Trim(), "u4");
                            MyX.Kiir(DateTime.Now.Year.ToString(), "x5");
                            MyX.Kiir(DateTime.Now.Month.ToString(), "z5");
                            MyX.Kiir(DateTime.Now.Day.ToString(), "ab5");
                            MyX.Kiir(Kezdet.Dolgozónév.Trim(), "q9");
                            MyX.Kiir(Kezdet.Törzsszám.Trim(), "x9");
                            MyX.Kiir(Kezdet.Kezdődátum.ToString("yyyy.MM.dd"), "q27");
                            MyX.Kiir(Vége.Befejeződátum.ToString("yyyy.MM.dd"), "q30");
                            MyX.Kiir(mostKivesz.ToString(), "v27");
                            MyX.Kiir(IdeigDátum.Year.ToString(), "s17");
                            MyX.Kiir(Összesnapja(Kezdet.Törzsszám.Trim()).ToString(), "v17");
                            MyX.Kiir(IdeigDátum.Year.ToString(), "s21");
                            MyX.Kiir(Kivettnapja(Kezdet.Törzsszám.Trim(), IdeigDátum).ToString(), "v21");
                            MyX.Kiir(CmbTelephely, "q13");
                            break;
                        }
                    case 3:
                        {
                            IdeigDátum = Kezdet.Kezdődátum;
                            MyX.Kiir(Kezdet.Sorszám + " /_" + Adat_Évek, "m33");
                            MyX.Kiir(Kezdet.Szabiok.Trim(), "f36");
                            MyX.Kiir(DateTime.Now.Year.ToString(), "i37");
                            MyX.Kiir(DateTime.Now.Month.ToString(), "k37");
                            MyX.Kiir(DateTime.Now.Day.ToString(), "m37");
                            MyX.Kiir(Kezdet.Dolgozónév.Trim(), "B41");
                            MyX.Kiir(Kezdet.Törzsszám.Trim(), "i41");
                            MyX.Kiir(Kezdet.Kezdődátum.ToString("yyyy.MM.dd"), "b59");
                            MyX.Kiir(Vége.Befejeződátum.ToString("yyyy.MM.dd"), "B62");
                            MyX.Kiir(mostKivesz.ToString(), "g59");
                            MyX.Kiir(IdeigDátum.Year.ToString(), "d49");
                            MyX.Kiir(Összesnapja(Kezdet.Törzsszám.Trim()).ToString(), "g49");
                            MyX.Kiir(IdeigDátum.Year.ToString(), "d53");
                            MyX.Kiir(Kivettnapja(Kezdet.Törzsszám.Trim(), IdeigDátum).ToString(), "g53");
                            MyX.Kiir(CmbTelephely, "B45");
                            break;
                        }
                    case 4:
                        {
                            IdeigDátum = Kezdet.Kezdődátum;
                            MyX.Kiir(Kezdet.Sorszám + " /_" + Adat_Évek, "ab33");
                            MyX.Kiir(Kezdet.Szabiok.Trim(), "u36");
                            MyX.Kiir(DateTime.Now.Year.ToString(), "x37");
                            MyX.Kiir(DateTime.Now.Month.ToString(), "z37");
                            MyX.Kiir(DateTime.Now.Day.ToString(), "ab37");
                            MyX.Kiir(Kezdet.Dolgozónév.Trim(), "q41");
                            MyX.Kiir(Kezdet.Törzsszám.Trim(), "x41");
                            MyX.Kiir(Kezdet.Kezdődátum.ToString("yyyy.MM.dd"), "q59");
                            MyX.Kiir(Vége.Befejeződátum.ToString("yyyy.MM.dd"), "q62");
                            MyX.Kiir(mostKivesz.ToString(), "v59");
                            MyX.Kiir(IdeigDátum.Year.ToString(), "s49");
                            MyX.Kiir(Összesnapja(Kezdet.Törzsszám.Trim()).ToString(), "v49");
                            MyX.Kiir(IdeigDátum.Year.ToString(), "s53");
                            MyX.Kiir(Kivettnapja(Kezdet.Törzsszám.Trim(), IdeigDátum).ToString(), "v53");
                            MyX.Kiir(CmbTelephely, "q45");
                            break;
                        }
                }
                // ha négy név van vagy ha a jelöltek számát elértük, akkor nyomtat majd a beírt adatokat törli
                if (elem == 4)
                {
                    string fájlnév = $"Szabadság_{Program.PostásNév}_{elem}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                    string MentésiFájl = $@"{könyvtár}\{fájlnév}";
                    MyX.ExcelMentés(MentésiFájl);
                    NyomtatásiFájlok.Add(MentésiFájl);
                    MyX.ExcelBezárás();
                    MyX.ExcelMegnyitás(fájlexcel);
                    MyX.Munkalap_aktív(munkalap);
                    elem = 0;
                }
                
            }
            if (elem != 0)
            {
                string fájlnév = $"Szabadság_{Program.PostásNév}_{elem}_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
                string MentésiFájl = $@"{könyvtár}\{fájlnév}";
                MyX.ExcelMentés(MentésiFájl);
                NyomtatásiFájlok.Add(MentésiFájl);
                MyX.ExcelBezárás();
            }
        
            MyF.ExcelNyomtatás(NyomtatásiFájlok,true);
        }

        private int Összesnapja(string törzsszám)
        {
            int válasz = 0;
            try
            {
                List<Adat_Szatube_Szabadság> Adatok = KézSzabadság.Lista_Adatok(CmbTelephely, Adat_Évek);
                Adatok = (from a in Adatok
                          where a.Törzsszám == törzsszám.Trim()
                          orderby a.Kezdődátum
                          select a).ToList();

                foreach (Adat_Szatube_Szabadság rekord in Adatok)
                {
                    if (rekord.Szabiok.Trim() == "Alap")
                        válasz += rekord.Kivettnap;
                    // 3 a törölt szabadság
                    if (rekord.Szabiok.ToUpper().Contains("PÓT") && rekord.Státus != 3)
                        válasz += rekord.Kivettnap;
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
            return válasz;
        }

        private int Kivettnapja(string törzsszám, DateTime dátum)
        {
            int válasz = 0;
            List<Adat_Szatube_Szabadság> Adatok = KézSzabadság.Lista_Adatok(CmbTelephely, dátum.Year);
            Adatok = (from a in Adatok
                      where a.Törzsszám == törzsszám.Trim() &&
                      a.Kezdődátum < dátum
                      orderby a.Kezdődátum
                      select a).ToList();

            foreach (Adat_Szatube_Szabadság rekord in Adatok)
            {
                if (rekord.Szabiok.ToUpper().Contains("KIVÉTEL") && rekord.Státus != 3)
                    válasz += rekord.Kivettnap;
            }
            return válasz;
        }
    }
}
