using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_MindenEgyéb
{
    public static class SAP_Adatokbeolvasása_km
    {
        /// <summary>
        /// Fogaskerekűre alakítva
        /// </summary>
        /// <param name="fájlexcel"></param>
        /// <param name="hely"></param>

        public static void Km_beolvasó(string fájlexcel, string helye)
        {
            try
            {
                DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexcel);


                //Ellenőrzés
                if (!MyF.Betöltéshelyes("KM adatok", Tábla)) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");

                MyE.ExcelMegnyitás(fájlexcel);
                string jelszó = "pocsaierzsi";
                string szöveg = $"SELECT * FROM kmtábla";
                string beopályaszám;

                Kezelő_T5C5_Kmadatok Kéz = new Kezelő_T5C5_Kmadatok();
                List<Adat_T5C5_Kmadatok> Adatok = Kéz.Lista_Adat(helye, jelszó, szöveg);

                // Első adattól végig pörgetjük a beolvasást addig amíg nem lesz üres

                int sor = 2;
                while (MyE.Beolvas($"a{sor}") != "_")
                {
                    string beolvasott = MyE.Beolvas($"a{sor}");
                    beopályaszám = MyF.Szöveg_Tisztítás(beolvasott, 1, 4);

                    if (beopályaszám.Trim() == "") break;
                    Adat_T5C5_Kmadatok Elem = (from a in Adatok
                                               where a.Azonosító == beopályaszám.Trim()
                                               && a.Törölt == false
                                               orderby a.Vizsgdátumk descending
                                               select a).FirstOrDefault();

                    if (Elem != null)
                    {
                        szöveg = "UPDATE kmtábla SET ";
                        szöveg += $" KMUdátum='{MyE.BeolvasDátum($"c{sor}"):yyyy.MM.dd}', ";
                        szöveg += $" KMUkm={MyE.Beolvas($"d{sor}")}, ";
                        if (MyE.Beolvas($"b{sor}") == "_")
                            szöveg += " havikm=0, ";
                        else
                            szöveg += $" havikm={MyE.Beolvas($"b{sor}")}, ";

                        szöveg += $" Jjavszám={MyE.Beolvas($"f{sor}")}, ";
                        szöveg += $" fudátum='{MyE.BeolvasDátum($"g{sor}")}', ";
                        szöveg += $" teljeskm={MyE.Beolvas($"e{sor}").Trim()} ";
                        szöveg += $"WHERE [id]={Elem.ID} ";
                        MyA.ABMódosítás(helye, jelszó, szöveg);
                    }
                    sor++;
                }
                // az excel tábla bezárása
                MyE.ExcelBezárás();
                // kitöröljük a betöltött fájlt
                File.Delete(fájlexcel);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Km_beolvasó", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public static void Km_beolvasó(string fájlexcel)
        {
            try
            {
                DateTime Eleje = DateTime.Now;
                DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexcel);
                //Ellenőrzés
                if (!MyF.Betöltéshelyes("KM adatok", Tábla)) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");

                string hely = Application.StartupPath + @"\Főmérnökség\Adatok\T5C5\Villamos4T5C5.mdb";
                string jelszó = "pocsaierzsi";
                // Első adattól végig pörgetjük a beolvasást addig amíg nem lesz üres

                string szöveg = "SELECT KMtábla.azonosító, KMtábla.vizsgdátumk, KMtábla.ID ";
                szöveg += " FROM  (SELECT KMtábla.azonosító, Max(KMtábla.vizsgdátumk) AS MaxOfvizsgdátumk FROM KMtábla WHERE törölt=False GROUP BY KMtábla.azonosító ORDER BY azonosító) AS Rész ";
                szöveg += " INNER JOIN KMtábla ON (Rész.MaxOfvizsgdátumk = KMtábla.vizsgdátumk) AND (Rész.azonosító = KMtábla.azonosító) ";
                szöveg += " WHERE törölt=False ORDER BY KMtábla.azonosító";

                Kezelő_T5C5_Kmadatok KézT5 = new Kezelő_T5C5_Kmadatok();
                List<Adat_T5C5_Kmadatok> AdatokT5 = KézT5.Lista_Szűrt_Adat(hely, jelszó, szöveg);
                List<string> SzövegGy = new List<string>();

                foreach (Adat_BEOLVAS_KM rekord in Excel_Km_Beolvas(Tábla))
                {
                    long utolsórögzítés = (from a in AdatokT5
                                           where a.Azonosító.Trim() == rekord.Azonosító
                                           select a.ID).FirstOrDefault();
                    if (utolsórögzítés != 0)
                    {
                        szöveg = "UPDATE kmtábla SET ";
                        szöveg += $" KMUdátum='{rekord.KMUdátum:yyyy.MM.dd}', ";
                        szöveg += $" KMUkm={rekord.KMUkm}, ";
                        szöveg += $" havikm={rekord.Havikm}, ";
                        szöveg += $" Jjavszám={rekord.Jjavszám}, ";
                        szöveg += $" fudátum='{rekord.Fudátum:yyyy.MM.dd}', ";
                        szöveg += $" teljeskm={rekord.Teljeskm} ";
                        szöveg += $" WHERE [id]={utolsórögzítés}";
                        SzövegGy.Add(szöveg);
                    }
                }
                if (SzövegGy.Count > 0) MyA.ABMódosítás(hely, jelszó, SzövegGy);

                DateTime Vége = DateTime.Now;
                MessageBox.Show($"Az adatok beolvasása {Vége - Eleje} idő alatt megtörtént.", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // kitöröljük a betöltött fájlt
                File.Delete(fájlexcel);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Km_beolvasó", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public static List<Adat_BEOLVAS_KM> Excel_Km_Beolvas(DataTable EgyTábla)
        {
            List<Adat_BEOLVAS_KM> Adatok = new List<Adat_BEOLVAS_KM>();
            if (EgyTábla != null)
            {
                for (int i = 0; i < EgyTábla.Rows.Count; i++)
                {
                    Adat_BEOLVAS_KM Adat = new Adat_BEOLVAS_KM(
                                    MyF.Szöveg_Tisztítás(EgyTábla.Rows[i]["Berendezés"].ToStrTrim(), 1, 4),
                                    EgyTábla.Rows[i]["Megtett KM (eltérés)"].ToÉrt_Long(),
                                    EgyTábla.Rows[i]["Intervallum KM dátum"].ToÉrt_DaTeTime(),
                                    EgyTábla.Rows[i]["KMU"].ToÉrt_Long(),
                                    EgyTábla.Rows[i]["KM"].ToÉrt_Long(),
                                    EgyTábla.Rows[i]["FÚ"].ToÉrt_Long(),
                                    EgyTábla.Rows[i]["FÚ dátuma"].ToÉrt_DaTeTime()
                                    );
                    Adatok.Add(Adat);
                }
            }
            return Adatok;
        }

    }


}
