using System;
using System.IO;
using System.Windows.Forms;

namespace Villamos
{
    public static class Hely_Ellenőr
    {
        public static string KönyvSzerk(this string fájl)
        {
            string Válasz = fájl;
            try
            {
                if (File.Exists(fájl)) return Válasz;
                string[] Könyvtár = fájl.Split('\\');
                string alap = Könyvtár[0];
                for (int i = 1; i < Könyvtár.Length; i++)
                {
                    if (!Könyvtár[i].Contains(".mdb"))
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

        public static void Könyvtárszerkezet(string Telephely)
        {
            try
            {
                string hely;
                if (Telephely == "Főmérnökség")
                {
                    //Minden könyvtár
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Főkönyv".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Üzenetek".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Képek".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Napló".KönyvSzerk();
                    //Főmérnökség
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Hibanapló".KönyvSzerk();
                }
                if (Telephely != "")
                {
                    //Minden könyvtár
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Főkönyv".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Üzenetek".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Képek".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Napló".KönyvSzerk();
                    //telephelyi
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Főkönyv\Futás".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Főkönyv\{DateTime.Today.Year}\Nap".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Főkönyv\{DateTime.Today.Year}\Zser".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Beosztás".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Naplózás".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Villamos".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\adatok\Segéd".KönyvSzerk();
                    hely = $@"{Application.StartupPath}\{Telephely}\Szerszám".KönyvSzerk();
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
