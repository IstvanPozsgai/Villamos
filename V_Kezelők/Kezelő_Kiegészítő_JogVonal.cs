using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_JogVonal
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
        readonly string jelszó = "Mocó";

        public List<Adat_Kiegészítő_Jogvonal> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Jogvonal Adat;
            List<Adat_Kiegészítő_Jogvonal> Adatok = new List<Adat_Kiegészítő_Jogvonal>();

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
                                Adat = new Adat_Kiegészítő_Jogvonal(
                                           rekord["sorszám"].ToÉrt_Long(),
                                           rekord["Szám"].ToStrTrim(),
                                           rekord["Megnevezés"].ToStrTrim()
                                           );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Kiegészítő_Jogvonal> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM jogvonal  order by  sorszám";
            Adat_Kiegészítő_Jogvonal Adat;
            List<Adat_Kiegészítő_Jogvonal> Adatok = new List<Adat_Kiegészítő_Jogvonal>();

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
                                Adat = new Adat_Kiegészítő_Jogvonal(
                                           rekord["sorszám"].ToÉrt_Long(),
                                           rekord["Szám"].ToStrTrim(),
                                           rekord["Megnevezés"].ToStrTrim()
                                           );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Jogvonal Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Jogvonal Adat = null;

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
                            rekord.Read();
                            Adat = new Adat_Kiegészítő_Jogvonal(
                                   rekord["sorszám"].ToÉrt_Long(),
                                   rekord["Szám"].ToString(),
                                   rekord["Megnevezés"].ToStrTrim()
                                   );
                        }
                    }
                }
            }
            return Adat;
        }

        public void Rögzítés(Adat_Kiegészítő_Jogvonal Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO jogvonal ( Szám, megnevezés  ) VALUES ( '{Adat.Szám}', '{Adat.Megnevezés}' )";
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

        public void Módosítás(Adat_Kiegészítő_Jogvonal Adat)
        {
            try
            {
                string szöveg = "UPDATE jogvonal  SET ";
                szöveg += $" Szám='{Adat.Szám}', ";
                szöveg += $" megnevezés='{Adat.Megnevezés}' ";
                szöveg += $"WHERE sorszám={Adat.Sorszám}";
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


    }

}
