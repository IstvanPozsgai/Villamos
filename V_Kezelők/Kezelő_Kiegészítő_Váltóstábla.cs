using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;


namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Váltóstábla
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
        readonly string jelszó = "Mocó";

        public Kezelő_Kiegészítő_Váltóstábla()
        {
            // if (!File.Exists(hely)) Adatbázis_Létrehozás   (hely.KönyvSzerk());
        }

        public List<Adat_Kiegészítő_Váltóstábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Váltóstábla> Adatok = new List<Adat_Kiegészítő_Váltóstábla>();
            Adat_Kiegészítő_Váltóstábla Adat;

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
                                Adat = new Adat_Kiegészítő_Váltóstábla(
                                       rekord["Id"].ToÉrt_Int(),
                                       rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                       rekord["Ciklusnap"].ToÉrt_Int(),
                                       rekord["Megnevezés"].ToStrTrim(),
                                       rekord["Csoport"].ToString());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Kiegészítő_Váltóstábla> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM váltósbeosztás ORDER BY id";
            List<Adat_Kiegészítő_Váltóstábla> Adatok = new List<Adat_Kiegészítő_Váltóstábla>();
            Adat_Kiegészítő_Váltóstábla Adat;

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
                                Adat = new Adat_Kiegészítő_Váltóstábla(
                                       rekord["Id"].ToÉrt_Int(),
                                       rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                       rekord["Ciklusnap"].ToÉrt_Int(),
                                       rekord["Megnevezés"].ToStrTrim(),
                                       rekord["Csoport"].ToString());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Kiegészítő_Váltóstábla Adat)
        {
            try
            {
                string szöveg = "INSERT INTO váltósbeosztás (kezdődátum, ciklusnap, megnevezés,  csoport) VALUES (";
                szöveg += $"'{Adat.Kezdődátum:yyyy.MM.dd}', ";
                szöveg += $"{Adat.Ciklusnap}, ";
                szöveg += $"'{Adat.Megnevezés}', ";
                szöveg += $"'{Adat.Csoport}' ) ";
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

        public void Módosítás(Adat_Kiegészítő_Váltóstábla Adat)
        {
            try
            {
                string szöveg = " UPDATE  váltósbeosztás SET ";
                szöveg += $" kezdődátum='{Adat.Kezdődátum:yyyy.MM.dd}', ";
                szöveg += $" ciklusnap={Adat.Ciklusnap}, ";
                szöveg += $" megnevezés='{Adat.Megnevezés}', ";
                szöveg += $" csoport='{Adat.Csoport}' ";
                szöveg += $" WHERE id={Adat.Id}";

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

        public void Törlés(int Id)
        {
            try
            {
                string szöveg = $"DELETE FROM váltósbeosztás WHERE id={Id}";
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
