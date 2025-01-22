using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos_Kezelők
{
    public class Kezelő_Kiegészítő_Beosegéd
    {
        readonly string jelszó = "Mocó";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő1.mdb";

        public List<Adat_Kiegészítő_Beosegéd> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM beosegéd  ORDER BY beosztáskód ";
            List<Adat_Kiegészítő_Beosegéd> Adatok = new List<Adat_Kiegészítő_Beosegéd>();
            Adat_Kiegészítő_Beosegéd Adat;

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
                                Adat = new Adat_Kiegészítő_Beosegéd(
                                     rekord["Beosztáskód"].ToStrTrim(),
                                     rekord["Túlóra"].ToÉrt_Int(),
                                     rekord["Kezdőidő"].ToÉrt_DaTeTime(),
                                     rekord["Végeidő"].ToÉrt_DaTeTime(),
                                     rekord["túlóraoka"].ToStrTrim(),
                                     rekord["telephely"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Kiegészítő_Beosegéd> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Beosegéd> Adatok = new List<Adat_Kiegészítő_Beosegéd>();
            Adat_Kiegészítő_Beosegéd Adat;

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
                                Adat = new Adat_Kiegészítő_Beosegéd(
                                     rekord["Beosztáskód"].ToStrTrim(),
                                     rekord["Túlóra"].ToÉrt_Int(),
                                     rekord["Kezdőidő"].ToÉrt_DaTeTime(),
                                     rekord["Végeidő"].ToÉrt_DaTeTime(),
                                     rekord["túlóraoka"].ToStrTrim(),
                                     rekord["telephely"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Kiegészítő_Beosegéd Adat)
        {
            try
            {
                string szöveg = "INSERT INTO beosegéd (beosztáskód, túlóra, kezdőidő, végeidő, túlóraoka, telephely) VALUES (";
                szöveg += $"'{Adat.Beosztáskód}', ";
                szöveg += $"{Adat.Túlóra}, ";
                szöveg += $"'{Adat.Kezdőidő:HH:mm:ss}', ";
                szöveg += $"'{Adat.Végeidő:HH:mm:ss}', ";
                szöveg += $"'{Adat.Túlóraoka}', ";
                szöveg += $"'{Adat.Telephely}' ) ";
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

        public void Módosítás(Adat_Kiegészítő_Beosegéd Adat)
        {
            try
            {
                string szöveg = " UPDATE  beosegéd SET ";
                szöveg += $" túlóra={Adat.Túlóra}, ";
                szöveg += $" túlóraoka='{Adat.Túlóraoka}', ";
                szöveg += $" kezdőidő='{Adat.Kezdőidő:HH:mm:ss}', ";
                szöveg += $" végeidő='{Adat.Végeidő:HH:mm:ss}' ";
                szöveg += $" WHERE beosztáskód='{Adat.Beosztáskód}' AND telephely='{Adat.Telephely}'";
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

        public void Törlés(string Beosztáskód, string Telephely)
        {
            try
            {
                string szöveg = $"DELETE FROM beosegéd where beosztáskód='{Beosztáskód.Trim()}' AND telephely='{Telephely.Trim()}'";
                MyA.ABtörlés(hely, jelszó, szöveg);
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
