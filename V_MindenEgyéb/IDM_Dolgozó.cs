using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos.V_MindenEgyéb
{
    public class IDM_Dolgozó
    {

        public static void Behajtási_beolvasás(string Excel_hely)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\behajtási\Behajtási_alap.mdb";
                string jelszó = "egérpad";
                string fájlexc = Excel_hely;

                // hány elemből áll a adatsor
                int vége;
                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\beolvasás.mdb";
                jelszó = "sajátmagam";

                string szöveg = "SELECT * FROM tábla WHERE csoport= 'Behajtás' and törölt='0'";
                Kezelő_Alap_Beolvasás Kéz = new Kezelő_Alap_Beolvasás();
                List<Adat_Alap_Beolvasás> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                if (Adatok == null) return;

                vége = Adatok.Max(a => a.Oszlop);
                if (vége == 0) return;
                MyE.ExcelMegnyitás(fájlexc);

                // beolvassuk a fejlécet
                szöveg = "";

                for (int i = 1; i <= vége; i++)
                    szöveg += MyE.Beolvas(MyE.Oszlopnév(i) + "1");
                if (!MyF.Betöltéshelyes("Behajtás", szöveg))
                {
                    MessageBox.Show("Nem megfelelő a betölteni kívánt adatok formátuma", "Betöltési hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // az excel tábla bezárása
                    MyE.ExcelBezárás();
                    return;
                }
                // Beolvasni kívánt oszlopok
                int j = 0;
                string[] oszlopnév = new string[vége + 1];

                for (int i = 1; i < vége; i++)
                {
                    j += 1;
                    bool vane = Adatok.Any(a => a.Oszlop == i && a.Kell == 1);
                    if (vane)
                        oszlopnév[j] = MyE.Oszlopnév(i);
                    else
                        oszlopnév[j] = "";
                }


                int státus;

                Kezelő_Behajtás_Dolgozótábla Kéz_behajt = new Kezelő_Behajtás_Dolgozótábla();
                List<Adat_Behajtás_Dolgozótábla> Adatok_behajt = Kéz_behajt.Lista_Adatok();
                int sor = 2;

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

                        if (státussz.Trim() == "ACTIVE")
                            státus = 1;
                        else
                            státus = 0;
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
                File.Delete(fájlexc);
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

        public static void Védő_beolvasás(string Excel_hely, string Cmbtelephely)
        {
            try
            {
                string fájlexc = Excel_hely;
                // hány elemből áll a adatsor

                string hely = Application.StartupPath + @"\Főmérnökség\adatok\beolvasás.mdb";
                string jelszó = "sajátmagam";
                string szöveg = "SELECT * FROM tábla where [csoport]= 'Dolgozó'  and [törölt]='0'  order by oszlop desc";

                MyE.ExcelMegnyitás(fájlexc);

                // beolvassuk a fejlécet az Excelből
                string Excel_szöveg = "";
                int oszlop = 1;
                while (MyE.Beolvas(MyE.Oszlopnév(oszlop) + "1") != "_")
                {
                    Excel_szöveg += MyE.Beolvas(MyE.Oszlopnév(oszlop) + "1");
                    oszlop++;
                }

                if (!MyF.Betöltéshelyes("Dolgozó", Excel_szöveg))
                {
                    MyE.ExcelBezárás();
                    throw new HibásBevittAdat("Nem megfelelő a betölteni kívánt adatok formátuma");
                }

                // Beolvasni kívánt oszlopok

                szöveg = "SELECT * FROM tábla where csoport= 'Dolgozó'  and törölt='0'";
                Kezelő_Excel_Beolvasó Kéz = new Kezelő_Excel_Beolvasó();
                List<Adat_Excel_Beolvasó> oszlopnév = Kéz.Lista_Adat(hely, jelszó, szöveg);

                string sztsz;
                string családnévutónév;
                string munkakör;
                string státussz;


                hely = $@"{Application.StartupPath}\" + Cmbtelephely.Trim() + @"\adatok\dolgozók.mdb";
                jelszó = "forgalmiutasítás";
                // Minden dolgozót feltöltünk
                List<Adat_Dolgozó_Alap> Dolgozók = Dolgozók_Lista(Cmbtelephely);

                int sor = 2;
                while (MyE.Beolvas("A" + sor) != "_")
                {
                    // beolvassuk az adatokat
                    sztsz = MyE.Beolvas(MyE.Oszlopnév(1) + sor);
                    //Ha csak számot tartalmaz akkor foglalkozunk tovább vele
                    Regex vizsgál = new Regex(@"[0-9]", RegexOptions.Compiled);
                    if (vizsgál.IsMatch(sztsz))
                    {
                        sztsz = MyF.Szöveg_Tisztítás(MyF.Eleje_kihagy(sztsz, "0"), 0, 8);
                        családnévutónév = MyF.Szöveg_Tisztítás((MyE.Beolvas(MyE.Oszlopnév(7) + sor) + " " + MyE.Beolvas(MyE.Oszlopnév(8) + sor)), 0, 50);
                        munkakör = MyF.Szöveg_Tisztítás(MyE.Beolvas(MyE.Oszlopnév(9) + sor), 0, 50);
                        státussz = MyE.Beolvas(MyE.Oszlopnév(4) + sor);

                        // meg nézzük, hogy van-e már ilyen adat
                        if (!DolgozóVan(Dolgozók, sztsz))
                        {
                            // ha nincs akkor újként rögzíti
                            szöveg = "INSERT INTO dolgozóadatok ( Dolgozószám, Dolgozónév, belépésiidő, kilépésiidő, munkakör )  VALUES ( ";
                            szöveg += "'" + sztsz.Trim() + "', ";   // Dolgozószám
                            szöveg += "'" + családnévutónév.Trim() + "', "; // Dolgozónév
                            szöveg += "'" + DateTime.Today.ToString("yyyy.MM.dd") + "', ";  // belépésiidő
                            szöveg += "'1900.01.01', ";  // kilépésiidő
                            szöveg += "'" + munkakör.Trim() + "') "; // munkakör
                            MyA.ABMódosítás(hely, jelszó, szöveg);
                        }
                        else
                        {
                            // ha van visszaállítja a kilépési időt 1900.01.01-re
                            if (státussz.Trim() == "ACTIVE")
                            {
                                szöveg = "UPDATE dolgozóadatok  SET ";
                                szöveg += "Dolgozónév='" + családnévutónév.Trim() + "', "; // Dolgozónév
                                szöveg += "belépésiidő='" + DateTime.Today.ToString("yyyy.MM.dd") + "', ";  // belépésiidő
                                szöveg += "kilépésiidő='1900.01.01' ";  // kilépésiidő
                                szöveg += " WHERE Dolgozószám='" + sztsz.Trim() + "'";
                                MyA.ABMódosítás(hely, jelszó, szöveg);
                            }
                        }
                    }
                    sor++;
                }
                // az excel tábla bezárása
                MyE.ExcelBezárás();
                // kitöröljük a betöltött fájlt
                File.Delete(fájlexc);
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


        static List<Adat_Dolgozó_Alap> Dolgozók_Lista(string Cmbtelephely)
        {
            string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Trim() + @"\adatok\dolgozók.mdb";
            string jelszó = "forgalmiutasítás";
            string szöveg = "SELECT * FROM dolgozóadatok ";
            Kezelő_Dolgozó_Alap kéz = new Kezelő_Dolgozó_Alap();
            List<Adat_Dolgozó_Alap> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
            return Adatok;
        }


        static bool DolgozóVan(List<Adat_Dolgozó_Alap> Dolgozók, string HRazonosító)
        {
            bool válasz = false;
            foreach (Adat_Dolgozó_Alap Elem in Dolgozók)
            {
                if (Elem.Dolgozószám.Trim() == HRazonosító.Trim())
                {
                    return true;
                }
            }
            return válasz;
        }
    }


}
