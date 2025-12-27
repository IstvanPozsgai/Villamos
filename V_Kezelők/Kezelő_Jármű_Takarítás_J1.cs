using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű_Takarítás_J1
    {
        readonly string jelszó = "seprűéslapát";
        string hely;

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Takarítás\Takarítás_{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Járműtakarító_Telephely_tábla(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_Takarítás_J1> Lista_Adat(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = "SELECT * FROM J1adatok";
            List<Adat_Jármű_Takarítás_J1> Adatok = new List<Adat_Jármű_Takarítás_J1>();
            Adat_Jármű_Takarítás_J1 Adat;

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
                                Adat = new Adat_Jármű_Takarítás_J1(
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["j1megfelelő"].ToÉrt_Int(),
                                        rekord["j1nemmegfelelő"].ToÉrt_Int(),
                                        rekord["napszak"].ToÉrt_Int(),
                                        rekord["típus"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, int Év, Adat_Jármű_Takarítás_J1 Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = "INSERT INTO j1adatok  (dátum, napszak, típus, J1megfelelő, J1nemmegfelelő) VALUES (";
                szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', ";
                szöveg += $"{Adat.Napszak}, ";
                szöveg += $"'{Adat.Típus}', ";
                szöveg += $"{Adat.J1megfelelő}, ";
                szöveg += $"{Adat.J1nemmegfelelő}) ";
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

        public void Módosítás(string Telephely, int Év, Adat_Jármű_Takarítás_J1 Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = "UPDATE j1adatok SET ";
                szöveg += $"J1megfelelő={Adat.J1megfelelő}, ";
                szöveg += $"J1nemmegfelelő={Adat.J1nemmegfelelő} ";
                szöveg += $" WHERE Dátum =#{Adat.Dátum:M-d-yyyy}# And napszak={Adat.Napszak} And típus='{Adat.Típus}'";
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
