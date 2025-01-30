using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerék_nyilvántartás
{
    public static class KerékNyilvántartás_funkciók
    {
        public static bool Erőtámkiolvasás(string pályaszám)
        {
            bool Válasz = false;
            try
            {
                // betöljük az utolsó erőtám adatot
                Kezelő_Kerék_Erő Kéz = new Kezelő_Kerék_Erő();
                List<Adat_Kerék_Erő> Adatok = new List<Adat_Kerék_Erő>();
                for (int év = 0; év < 1; év++)
                {
                    string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{DateTime.Now.AddYears(-1 * év).Year}\telepikerék.mdb";
                    string jelszó = "szabólászló";
                    string szöveg = "SELECT * FROM erőtábla";
                    if (Exists(hely))
                    {
                        List<Adat_Kerék_Erő> AdatokIdeig = Kéz.Lista_Adatok(hely, jelszó, szöveg);
                        Adatok.AddRange(AdatokIdeig);
                    }
                }
                if (Adatok.Count != 0)
                {
                    Adat_Kerék_Erő Elem = (from a in Adatok
                                           where a.Azonosító == pályaszám
                                           orderby a.Mikor descending
                                           select a).FirstOrDefault();
                    if (Elem != null)
                    {
                        if (Elem.Van == "van") Válasz = true;
                    }
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "KerékNyilvántartás_funkciók", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Válasz;
        }


        public static Adat_Kerék_Eszterga Esztergakiolvasás(string pályaszám)
        {
            Adat_Kerék_Eszterga Válasz = null;
            try
            {
                // betöljük az utolsó erőtám adatot
                Kezelő_Kerék_Eszterga Kéz = new Kezelő_Kerék_Eszterga();
                List<Adat_Kerék_Eszterga> Adatok = new List<Adat_Kerék_Eszterga>();
                for (int év = 0; év < 1; év++)
                {
                    string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{DateTime.Now.AddYears(-1 * év).Year}\telepikerék.mdb";
                    string jelszó = "szabólászló";
                    string szöveg = "SELECT * FROM esztergatábla";
                    if (Exists(hely))
                    {
                        List<Adat_Kerék_Eszterga> AdatokIdeig = Kéz.Lista_Adatok(hely, jelszó, szöveg);
                        Adatok.AddRange(AdatokIdeig);
                    }
                }
                if (Adatok.Count != 0)
                {
                    Válasz = (from a in Adatok
                              where a.Azonosító == pályaszám
                              orderby a.Mikor descending
                              select a).FirstOrDefault();
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "KerékNyilvántartás_funkciók", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Válasz;
        }

    }
}
