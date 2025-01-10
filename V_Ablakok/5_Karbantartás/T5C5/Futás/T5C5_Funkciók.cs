using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos
{
    public static class T5C5_Funkciók
    {
        public static List<Adat_T5C5_Göngyöl_DátumTábla> AdatokGöngyöl_feltöltése()
        {
            List<Adat_T5C5_Göngyöl_DátumTábla> Adatok = new List<Adat_T5C5_Göngyöl_DátumTábla>();
            try
            {
                Kezelő_T5C5_Göngyöl_DátumTábla KézGöngyöl = new Kezelő_T5C5_Göngyöl_DátumTábla();
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\T5C5\villamos3.mdb";
                string szöveg = "SELECT * FROM dátumtábla";
                string jelszó = "pozsgaii";
                Adatok = KézGöngyöl.Lista_Adat(hely, jelszó, szöveg);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "AdatokGöngyöl_feltöltése()", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Adatok;
        }

        public static List<Adat_T5C5_Göngyöl_DátumTábla> AdatokGöngyöl_feltöltése(string hely)
        {
            List<Adat_T5C5_Göngyöl_DátumTábla> Adatok = new List<Adat_T5C5_Göngyöl_DátumTábla>();
            try
            {
                Kezelő_T5C5_Göngyöl_DátumTábla KézGöngyöl = new Kezelő_T5C5_Göngyöl_DátumTábla();
                string szöveg = "SELECT * FROM dátumtábla";
                string jelszó = "pozsgaii";
                Adatok = KézGöngyöl.Lista_Adat(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "AdatokGöngyöl_feltöltése()", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Adatok;
        }

        public static List<string> Pályaszám_feltöltés(string telephely)
        {
            List<string> Adatok = new List<string>();
            try
            {
                if (telephely.Trim() == "") return Adatok;

                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = $"Select * FROM Állománytábla WHERE Üzem='{telephely.Trim()}' AND ";
                szöveg += $" törölt=0 AND valóstípus Like  '%T5C5%' ORDER BY azonosító";

                Kezelő_Jármű kéz = new Kezelő_Jármű();
                Adatok = kéz.Lista_Pályaszámok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Pályaszám_feltöltés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Adatok;
        }


        public static void Zároljuk(List<Adat_T5C5_Göngyöl_DátumTábla> Adatok, string telephely)
        {
            try
            {
                Adat_T5C5_Göngyöl_DátumTábla Elem = (from a in Adatok
                                                     where a.Zárol == true
                                                     select a).FirstOrDefault();

                if (Elem != null) throw new HibásBevittAdat($"Az adatok göngyölése nem lehetséges, mert {Elem.Telephely} dolgozza fel az adatokat.");

                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\T5C5\villamos3.mdb";
                string szöveg = $"UPDATE dátumtábla SET Zárol=true WHERE telephely='{telephely.Trim()}'";
                string jelszó = "pozsgaii";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Zároljuk", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Kinyitjuk(string telephely)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\T5C5\villamos3.mdb";
                string szöveg = $"UPDATE dátumtábla SET Zárol=false WHERE telephely='{telephely}'";
                string jelszó = "pozsgaii";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                MessageBox.Show($"A(z) {telephely} telephelyi zárolás feloldásra került!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Kinyitjuk", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
