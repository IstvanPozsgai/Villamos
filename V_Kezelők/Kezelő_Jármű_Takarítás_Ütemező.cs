using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű_Takarítás_Ütemező
    {
        readonly string jelszó = "seprűéslapát";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Takarítás\Jármű_Takarítás.mdb";

        public Kezelő_Jármű_Takarítás_Ütemező()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Járműtakarító_Főmérnök_tábla(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_Takarítás_Ütemező> Lista_Adat()
        {
            string szöveg = "SELECT * FROM ütemező ";
            List<Adat_Jármű_Takarítás_Ütemező> Adatok = new List<Adat_Jármű_Takarítás_Ütemező>();
            Adat_Jármű_Takarítás_Ütemező Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Ütemező(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Kezdő_dátum"].ToÉrt_DaTeTime(),
                                        rekord["növekmény"].ToÉrt_Int(),
                                        rekord["Mérték"].ToStrTrim(),
                                        rekord["Takarítási_fajta"].ToStrTrim(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Int()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(Adat_Jármű_Takarítás_Ütemező Adat)
        {
            try
            {
                string szöveg = "UPDATE ütemező  SET ";
                szöveg += $"Kezdő_dátum='{Adat.Dátum:yyyy.MM.dd}', ";   // Kezdő_dátum
                szöveg += $"növekmény={Adat.Növekmény}, ";// növekmény
                szöveg += $"Mérték='{Adat.Mérték}', "; // Mérték
                szöveg += $"státus={Adat.Státus} ";
                szöveg += $" WHERE  azonosító='{Adat.Azonosító}' AND ";
                szöveg += $" takarítási_fajta='{Adat.Takarítási_fajta}' AND ";
                szöveg += $" telephely='{Adat.Telephely}'";
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

        public void Rögzítés(Adat_Jármű_Takarítás_Ütemező Adat)
        {
            try
            {
                string szöveg = "INSERT INTO ütemező (azonosító, Kezdő_dátum, növekmény, Mérték, Takarítási_fajta, Telephely, státus) VALUES (";
                szöveg += $"'{Adat.Azonosító}', ";// azonosító
                szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', ";// Kezdő_dátum
                szöveg += $"{Adat.Növekmény}, ";// növekmény
                szöveg += $"'{Adat.Mérték}', "; // Mérték
                szöveg += $"'{Adat.Takarítási_fajta}', ";// Takarítási_fajta
                szöveg += $"'{Adat.Telephely}', ";// Telephely
                szöveg += $"{Adat.Státus})";
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
