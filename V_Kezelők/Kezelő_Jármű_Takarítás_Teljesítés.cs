using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű_Takarítás_Teljesítés
    {
        readonly string jelszó = "seprűéslapát";
        string hely;

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Takarítás\Takarítás_{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Járműtakarító_Telephely_tábla(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_Takarítás_Teljesítés> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = $"SELECT * FROM teljesítés";

            List<Adat_Jármű_Takarítás_Teljesítés> Adatok = new List<Adat_Jármű_Takarítás_Teljesítés>();
            Adat_Jármű_Takarítás_Teljesítés Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Teljesítés(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["takarítási_fajta"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["megfelelt1"].ToÉrt_Int(),
                                        rekord["státus"].ToÉrt_Int(),
                                        rekord["megfelelt2"].ToÉrt_Int(),
                                        rekord["pótdátum"].ToÉrt_Bool(),
                                        rekord["napszak"].ToÉrt_Int(),
                                        rekord["mérték"].ToÉrt_Double(),
                                        rekord["típus"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string Telephely, int Év, Adat_Jármű_Takarítás_Teljesítés Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"UPDATE Teljesítés SET ";
                szöveg += $"megfelelt1={Adat.Megfelelt1}, ";
                szöveg += $"státus={Adat.Státus}, ";
                szöveg += $"megfelelt2={Adat.Megfelelt2}, ";
                szöveg += $"pótdátum={Adat.Pótdátum}, ";
                szöveg += $"mérték={Adat.Mérték.ToString().Replace(",", ".")} ";
                szöveg += $" WHERE dátum=#{Adat.Dátum:MM-dd-yyyy}#";
                szöveg += $" and napszak={Adat.Napszak} ";
                szöveg += $" and azonosító='{Adat.Azonosító}'";
                szöveg += $" and takarítási_fajta='{Adat.Takarítási_fajta}'";
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

        public void Rögzítés(string Telephely, int Év, Adat_Jármű_Takarítás_Teljesítés Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"INSERT INTO Teljesítés (azonosító, takarítási_fajta, dátum, megfelelt1, státus, megfelelt2, pótdátum, napszak, típus,  mérték ) VALUES (";
                szöveg += $"'{Adat.Azonosító}', ";  // azonosító
                szöveg += $"'{Adat.Takarítási_fajta}', ";  // takarítási_fajta
                szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', "; // dátum
                szöveg += $"{Adat.Megfelelt1}, ";            // megfelelt1,
                szöveg += $"{Adat.Státus}, ";                             // státus,
                szöveg += $"{Adat.Megfelelt2}, ";                         // megfelelt2,
                szöveg += $"{Adat.Pótdátum}, ";                           // pótdátum,
                szöveg += $"{Adat.Napszak}, ";                            // napszak,
                szöveg += $"'{Adat.Típus}', ";                            // típus,
                szöveg += $"{Adat.Mérték.ToString().Replace(",", ".")})"; // mérték
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
