using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;

namespace Villamos.Villamos_Nyomtatványok
{
    public class Főkönyv_Beálló
    {
        public void Beálló_kocsik(string fájlexl, string Telephely, DateTime Dátum, string napszak, string papírméret, string papírelrendezés)
        {
            try
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
                    6, 6,
                    5, 5,
                    8, 8, "1", "1", papírelrendez, papírméret, true, true);

                MyE.Aktív_Cella(munkalap, "A1");
                // bezárjuk az Excel-t
                MyE.ExcelMentés(fájlexl);
                MyE.ExcelBezárás();


                MyE.Megnyitás(fájlexl);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2146777998)
                {
                    // Lekeri COM platformrol a hatterben futo nyitott Excel tablakat.
                    Microsoft.Office.Interop.Excel.Application excelApp = (Microsoft.Office.Interop.Excel.Application)Marshal.GetActiveObject("Excel.Application");
                    // Osszgyujti a fenti folyamatbol a nyitott tablak eleresi helyet es nevet
                    List<string> futoExcelek = excelApp.Workbooks.Cast<Microsoft.Office.Interop.Excel.Workbook>()
                                                     .Select(wb => wb.FullName)
                                                     .ToList();

                    string HibaSzöveg2 = $"{this.ToString()}\n" +
                       $"Telephely:{Telephely} \n" +
                       $"fájlexl:{fájlexl} \n" +
                       $"Dátum:{Dátum}\n" +
                       $"napszak:{napszak}\n" +
                       $"papírméret:{papírméret}\n" +
                       $"papírelrendezés:{papírelrendezés}\n" +
                       $"Futo excelek szama a hatterben: {excelApp.Workbooks.Count}\n" +
                       $"Futo excelek a hatterben: {string.Join(", ", futoExcelek)}";

                    HibaNapló.Log(ex.Message, HibaSzöveg2, ex.StackTrace, ex.Source, ex.HResult);

                    // Ha nem fut EXCEL.EXE, akkor is probalja meg kiloni, mivel betud ragadni ugy, hogy nem latjuk a COM interfeszen keresztul.
                    if (futoExcelek.Count == 0)
                    {
                        // Lekeri a hatterben futo folyamatokat, es az osszes olyat, amelynek a neve tartalmazza az EXCEL-t kilovi.
                        foreach (var proc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                        {
                            try
                            {
                                proc.Kill();
                                proc.WaitForExit();
                            }
                            catch
                            {
                                // Erre azert van szukseg, hogyha nem fut semmilyen EXCEL a hatterben, ne akadjon meg a program.
                                HibaNapló.Log(ex.Message, HibaSzöveg2 + "\nHatterben levo Excel kilove!", ex.StackTrace, ex.Source, ex.HResult);
                            }
                        }
                    }
                    else
                    {
                        DialogResult ExcelBezarE = MessageBox.Show($"Úgy tűnik, hogy a következő Excel fájlok nyitva vannak, emiatt a program hibára futott:\nBezárja őket?(Mentésre kerülnek)\n{string.Join(",\n", futoExcelek)}"
                        , "Figyelem", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                        if (ExcelBezarE == DialogResult.Yes)
                        {
                            // Ha van nyitott Excel
                            while (excelApp.Workbooks.Count > 0)
                            {
                                excelApp.Workbooks[1].Close(true); // true = Menti oket
                            }
                            // Excel bezarasa
                            excelApp.Quit();
                            // COM eldobasa
                            Marshal.ReleaseComObject(excelApp);
                            excelApp = null;
                            MessageBox.Show("A futó Excel fájlok sikeresen belettek zárva.\nPróbálja meg a generálást újból!", "Figyelem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            MessageBox.Show("A folyamat megszakítva, nem történt Excel fájl generálás.");
                        }
                    }
                }
                else
                {
                    string HibaSzöveg = $"{this.ToString()}\n" +
                   $"Telephely:{Telephely} \n" +
                   $"fájlexl:{fájlexl} \n" +
                   $"Dátum:{Dátum}\n" +
                   $"napszak:{napszak}\n" +
                   $"papírméret:{papírméret}\n" +
                   $"papírelrendezés:{papírelrendezés}";

                    HibaNapló.Log(ex.Message, HibaSzöveg, ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
