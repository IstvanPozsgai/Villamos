using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű_Vendég
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos.mdb";
        readonly string jelszó = "pozsgaii";

        public Kezelő_Jármű_Vendég()
        {
            //  if (!File.Exists(hely)) Adatbázis_Létrehozás   (hely.KönyvSzerk());
        }

        public List<Adat_Jármű_Vendég> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM vendégtábla order by azonosító";
            List<Adat_Jármű_Vendég> Adatok = new List<Adat_Jármű_Vendég>();
            Adat_Jármű_Vendég Adat;
            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat))
                {
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {
                        if (rekord.HasRows)
                        {
                            while (rekord.Read())
                            {
                                Adat = new Adat_Jármű_Vendég(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Típus"].ToStrTrim(),
                                    rekord["Bázistelephely"].ToStrTrim(),
                                    rekord["Kiadótelephely"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Törlés(Adat_Jármű_Vendég Adat)
        {
            try
            {
                List<Adat_Jármű_Vendég> Adatok = Lista_Adatok();
                Adat_Jármű_Vendég EgyAdat = Adatok.FirstOrDefault(a => a.Azonosító == Adat.Azonosító.Trim());

                if (EgyAdat != null)
                {
                    string szöveg = $"DELETE FROM vendégtábla WHERE azonosító='{Adat.Azonosító.Trim()}'";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                    throw new HibásBevittAdat("Az adat törlése megtörtént.");
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

        public void Rögzítés(Adat_Jármű_Vendég Adat)
        {
            try
            {
                List<Adat_Jármű_Vendég> Adatok = Lista_Adatok();
                Adat_Jármű_Vendég EgyAdat = Adatok.FirstOrDefault(a => a.Azonosító == Adat.Azonosító.Trim());
                // rögzítjük az adatot

                if (EgyAdat != null)
                {
                    // Ha már létezik, akkor módosítjuk
                    string szöveg = "UPDATE vendégtábla  SET ";
                    szöveg += $"típus='{Adat.Típus.Trim()}', "; // típus
                    szöveg += $"BázisTelephely='{Adat.BázisTelephely.Trim()}', "; // BázisTelephely
                    szöveg += $"KiadóTelephely='{Adat.KiadóTelephely.Trim()}' "; // KiadóTelephely
                    szöveg += $" WHERE azonosító='{Adat.Azonosító.Trim()}'";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                    throw new HibásBevittAdat("Az adat módosítása megtörtént.");
                }
                else
                {
                    // ha nem létezik 
                    string szöveg = "INSERT INTO vendégtábla  (  azonosító, típus, BázisTelephely, KiadóTelephely ) VALUES (";
                    szöveg += $"'{Adat.Azonosító.Trim()}', "; // azonosító
                    szöveg += $"'{Adat.Típus.Trim()}', "; // típus
                    szöveg += $"'{Adat.BázisTelephely.Trim()}', "; // BázisTelephely
                    szöveg += $"'{Adat.KiadóTelephely.Trim()}')";

                    MyA.ABMódosítás(hely, jelszó, szöveg);
                    throw new HibásBevittAdat("Az adat rögzítése megtörtént.");
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
