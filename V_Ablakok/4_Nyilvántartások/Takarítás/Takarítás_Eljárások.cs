using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Takarítás
{
    public class Takarítás_Eljárások
    {
        public Kezelő_Jármű_Takarítás_Vezénylés KézVezény = new Kezelő_Jármű_Takarítás_Vezénylés();
        public List<Adat_Jármű_Takarítás_Vezénylés> AdatokVezény = new List<Adat_Jármű_Takarítás_Vezénylés>();

        public void TakVezénylésLista(string hely)
        {
            try
            {
                AdatokVezény.Clear();
                string jelszó = "seprűéslapát";
                string szöveg = "SELECT * FROM vezénylés";
                AdatokVezény = KézVezény.Lista_Adatok(hely, jelszó, szöveg);
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
        }

        public void Ütem_Rögzít(string hely, string jelszó, DateTime Dátum, string pályaszám, string takarításfajta, string szerelvényszám)
        {
            try
            {
                TakVezénylésLista(hely);
                Adat_Jármű_Takarítás_Vezénylés Elem = (from a in AdatokVezény
                                                       where a.Azonosító == pályaszám 
                                                       && a.Takarítási_fajta == takarításfajta 
                                                       && a.Státus != 9
                                                       && a.Dátum .ToShortDateString ()==Dátum.ToShortDateString ()
                                                       select a).FirstOrDefault();
                string szöveg;
                if (Elem != null)
                {
                    // Módosítjuk
                    szöveg = "UPDATE Vezénylés  SET  státus=0 ";
                    szöveg += $" WHERE dátum=#{Dátum:MM-dd-yyyy}# And azonosító='{pályaszám.Trim()}' AND takarítási_fajta='{takarításfajta}'";
                }
                else
                {
                    long Utolsó_elem = 1;
                    if (AdatokVezény.Count > 0) Utolsó_elem = AdatokVezény.Max(a => a.Id) + 1;

                    szöveg = "INSERT INTO Vezénylés (id, azonosító, dátum, takarítási_fajta, szerelvényszám,  státus ) VALUES (";
                    szöveg += $"{Utolsó_elem}, "; // id
                    szöveg += $" '{pályaszám.Trim()}', "; // azonosító
                    szöveg += $" '{Dátum:yyyy.MM.dd}', ";  // dátum
                    szöveg += $"'{takarításfajta}', "; // takarítási_fajta
                    szöveg += $"{szerelvényszám}, "; // szerelvényszám
                    szöveg += "0)";      // státus
                }

                MyA.ABMódosítás(hely, jelszó, szöveg);
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
        }

        public void Ütem_Töröl(string hely, string jelszó, DateTime Dátum, string pályaszám, string takarításfajta)
        {
            try
            {
                TakVezénylésLista(hely);
                Adat_Jármű_Takarítás_Vezénylés Elem = (from a in AdatokVezény
                                                       where a.Azonosító == pályaszám
                                                       && a.Takarítási_fajta == takarításfajta
                                                       && a.Státus != 9
                                                       && a.Dátum.ToShortDateString() == Dátum.ToShortDateString()
                                                       select a).FirstOrDefault();
                if (Elem != null)
                {
                    string szöveg = "UPDATE Vezénylés  SET  státus=9 ";
                    szöveg += $" WHERE dátum=#{Dátum:MM-dd-yyyy}# And azonosító='{pályaszám.Trim()}' AND takarítási_fajta='{takarításfajta}'";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
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
        }

    }
}
