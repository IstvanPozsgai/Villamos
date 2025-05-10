using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_MindenEgyéb
{
    public class IDM_Dolgozó
    {
        readonly static Kezelő_Alap_Beolvasás KézBeolvasás = new Kezelő_Alap_Beolvasás();


        public static void Behajtási_beolvasás(string Excel_hely)
        {
            try
            {
                //beolvassuk az excel táblát és megnézzük, hogy megegyezik-e a két fejléc
                DataTable Tábla = MyF.Excel_Tábla_Beolvas(Excel_hely);
                if (!MyF.Betöltéshelyes("Behajtás", Tábla)) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");

                ///beolvassuk azt hogy melyik elemekre van szükségünk az excel táblából
                List<Adat_Alap_Beolvasás> Adatok = KézBeolvasás.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Csoport == "Behajtás"
                          && a.Törölt == "0"
                          orderby a.Oszlop
                          select a).ToList();

                if (Adatok == null) return;
                int vége = Adatok.Max(a => a.Oszlop);
                if (vége == 0) return;
                // Beolvasni kívánt oszlopok
                int j = 0;
                string[] oszlopnév = new string[vége + 1];

                for (int i = 1; i < vége; i++)
                {
                    j += 1;
                    bool vane = Adatok.Any(a => a.Oszlop == i && a.Kell != 0);
                    if (vane)
                        oszlopnév[j] = MyE.Oszlopnév(i);
                    else
                        oszlopnév[j] = "";
                }

                Kezelő_Behajtás_Dolgozótábla Kéz_behajt = new Kezelő_Behajtás_Dolgozótábla();
                List<Adat_Behajtás_Dolgozótábla> Adatok_behajt = Kéz_behajt.Lista_Adatok();
                int sor = 2;
                MyE.ExcelMegnyitás(Excel_hely);
                while (MyE.Beolvas($"A{sor}").Trim() != "_")
                {
                    // beolvassuk az adatokat
                    string sztsz = MyE.Beolvas(oszlopnév[2] + sor).Trim();
                    Regex vizsgál = new Regex(@"[0-9]", RegexOptions.Compiled);
                    if (vizsgál.IsMatch(sztsz))
                    {
                        sztsz = MyF.Eleje_kihagy(sztsz, "0");
                        string vezetéknév = MyE.Beolvas(oszlopnév[13] + sor).Trim();
                        string keresztnév = MyE.Beolvas(oszlopnév[12] + sor).Trim();
                        string családnévutónév = vezetéknév + " " + keresztnév;
                        string munkakör = MyE.Beolvas(oszlopnév[11] + sor).Trim();
                        string szervezetiegység = MyE.Beolvas(oszlopnév[16] + sor).Trim();
                        string státussz = MyE.Beolvas(oszlopnév[21] + sor).Trim();
                        int státus = 0;
                        if (státussz.Trim() == "ACTIVE") státus = 1;

                        // meg nézzük, hogy van-e már ilyen adat
                        bool vane = Adatok_behajt.Any(a => a.SZTSZ.Trim() == sztsz.Trim());
                        Adat_Behajtás_Dolgozótábla ADAT = new Adat_Behajtás_Dolgozótábla(
                                                        sztsz.Trim(),
                                                        családnévutónév.Trim(),
                                                        szervezetiegység.Trim(),
                                                        munkakör.Trim(),
                                                        státus);
                        if (vane)
                            Kéz_behajt.Módosítás(ADAT);
                        else
                            Kéz_behajt.Rögzítés(ADAT);
                    }
                    sor++;
                }

                // az excel tábla bezárása
                MyE.ExcelBezárás();

                // kitöröljük a betöltött fájlt
                File.Delete(Excel_hely);
                MessageBox.Show("Az adat konvertálás befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Behajtási_beolvasás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
