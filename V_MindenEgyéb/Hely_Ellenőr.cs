using System;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;

namespace Villamos
{
    public static class Hely_Ellenőr
    {
        public static string Ellenőrzés(this string fájl)
        {
            string Válasz = fájl;
            try
            {
                if (File.Exists(fájl)) return Válasz;
                string[] Könyvtár = fájl.Split('\\');
                string alap = Könyvtár[0];
                for (int i = 1; i < Könyvtár.Length; i++)
                {

                    if (Könyvtár[i].Contains(".mdb"))
                        Tábla(alap, Könyvtár[i]);
                    else
                    {
                        alap += $@"\{Könyvtár[i]}";
                        if (!Directory.Exists(alap)) Directory.CreateDirectory(alap);
                    }
                }

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"Hely_Ellenőr : Ellenőrzés :{fájl}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Válasz;
        }

        private static void Tábla(string Hely, string fájl)
        {
            string hely = $@"{Hely}\{fájl}";
            try
            {

                if (fájl.Contains("napló") && fájl.Contains(".mdb")) Adatbázis_Létrehozás.Kocsitípusanapló(hely);
                if (fájl.Contains("Váltóscsoportvezetők.mdb")) Adatbázis_Létrehozás.Váltóscsopitábla(hely);
                if (fájl.Contains("munkaidőnaptár.mdb")) Adatbázis_Létrehozás.Nappalosmunkarendlétrehozás(hely);
                if (fájl.Contains("üzenet.mdb")) Adatbázis_Létrehozás.ALÜzenetadatok(hely);
                if (fájl.Contains("utasítás.mdb")) Adatbázis_Létrehozás.UtasításadatokTábla(hely);
                if (fájl.Contains("Akkunapló")) Adatbázis_Létrehozás.Akku_Mérés(hely);
                if (fájl.Contains("akku.mdb")) Adatbázis_Létrehozás.Akku_adatok(hely);
                if (fájl.Contains("ciklus.mdb")) Adatbázis_Létrehozás.Ciklusrendtábla(hely);
                if (fájl.Contains("munkalapelszámoló")) Adatbázis_Létrehozás.Ciklusrendtábla(hely);



            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"Hely_Ellenőr : Tábla :\n{hely}\n{fájl}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Könyvtárszerkezet(string Telephely)
        {
            try
            {
                string hely;
                //Minden könyvtár
                hely = $@"{Application.StartupPath}\{Telephely}\adatok\Főkönyv".Ellenőrzés();
                hely = $@"{Application.StartupPath}\{Telephely}\adatok\Üzenetek".Ellenőrzés();
                hely = $@"{Application.StartupPath}\{Telephely}\Képek".Ellenőrzés();
                hely = $@"{Application.StartupPath}\{Telephely}\Napló".Ellenőrzés();

                if (Telephely == "Főmérnökség")
                {
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Főkönyv".Ellenőrzés();
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Hibanapló".Ellenőrzés();
                    hely = $@"{Application.StartupPath}\{Telephely}\napló\napló{DateTime.Now.Year}.mdb".Ellenőrzés();
                }
                else
                {
                    //telephelyi
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Főkönyv\Futás".Ellenőrzés();
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Főkönyv\{DateTime.Today.Year}\Nap".Ellenőrzés();
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Főkönyv\{DateTime.Today.Year}\Zser".Ellenőrzés();
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Beosztás".Ellenőrzés();
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Naplózás".Ellenőrzés();
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Villamos".Ellenőrzés();
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Segéd".Ellenőrzés();
                    hely = $@"{Application.StartupPath}\{Telephely}\Szerszám".Ellenőrzés();
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"Hely_Ellenőr : Könyvtárszerkezet ", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
