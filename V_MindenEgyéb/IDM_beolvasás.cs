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

namespace Villamos.MindenEgyéb
{
    public static class IDM_beolvasás
    {
        public static void Behajtási_beolvasás(string Excel_hely)
        {
            try
            {

                //beolvassuk az excel táblát és megnézzük, hogy megegyezik-e a két fejléc
                DataTable Tábla = MyF.Excel_Tábla_Beolvas(Excel_hely);
                if (!MyF.Betöltéshelyes("Behajtás", Tábla)) throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma ! ");

                ///beolvassuk azt hogy melyik elemekre van szükségünk az excel táblából
                Kezelő_Excel_Beolvasás KézBeolvasás = new Kezelő_Excel_Beolvasás();
                List<Adat_Excel_Beolvasás> Adatok = KézBeolvasás.Lista_Adatok();
                if (Adatok == null) return;
                string oszlopHR = (from a in Adatok where a.Csoport == "Behajtás" && a.Státusz == false && a.Változónév == "Dolgozószám" select a.Fejléc).FirstOrDefault();
                string oszlopMunka = (from a in Adatok where a.Csoport == "Behajtás" && a.Státusz == false && a.Változónév == "Munkakör" select a.Fejléc).FirstOrDefault();
                string oszlopNév = (from a in Adatok where a.Csoport == "Behajtás" && a.Státusz == false && a.Változónév == "Dolgozónév" select a.Fejléc).FirstOrDefault();
                string oszlopStátus = (from a in Adatok where a.Csoport == "Behajtás" && a.Státusz == false && a.Változónév == "Státusz" select a.Fejléc).FirstOrDefault();
                string oszlopSzerv = (from a in Adatok where a.Csoport == "Behajtás" && a.Státusz == false && a.Változónév == "Szervezet" select a.Fejléc).FirstOrDefault();

                Kezelő_Behajtás_Dolgozótábla KézDolgozó = new Kezelő_Behajtás_Dolgozótábla();
                List<Adat_Behajtás_Dolgozótábla> Adatok_behajt = KézDolgozó.Lista_Adatok();
                MyE.ExcelMegnyitás(Excel_hely);

                foreach (DataRow sor in Tábla.Rows)
                {
                    // beolvassuk az adatokat
                    string sztsz = sor[oszlopHR].ToString();
                    Regex vizsgál = new Regex(@"[0-9]", RegexOptions.Compiled);
                    if (vizsgál.IsMatch(sztsz))
                    {
                        sztsz = MyF.Eleje_kihagy(sztsz, "0");
                        string családnévutónév = sor[oszlopNév].ToString();
                        string munkakör = sor[oszlopMunka].ToString();
                        string szervezetiegység = sor[oszlopSzerv].ToString();
                        string státussz = sor[oszlopStátus].ToString();
                        bool státus = false;
                        if (státussz.Trim() == "ACTIVE") státus = true;

                        // meg nézzük, hogy van-e már ilyen adat
                        bool vane = Adatok_behajt.Any(a => a.Dolgozószám.Trim() == sztsz.Trim());
                        Adat_Behajtás_Dolgozótábla ADAT = new Adat_Behajtás_Dolgozótábla(
                                                        sztsz.Trim(),
                                                        családnévutónév.Trim(),
                                                        szervezetiegység.Trim(),
                                                        munkakör.Trim(),
                                                        státus);
                        if (vane)
                            KézDolgozó.Módosítás(ADAT);
                        else
                            KézDolgozó.Rögzítés(ADAT);
                    }

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
