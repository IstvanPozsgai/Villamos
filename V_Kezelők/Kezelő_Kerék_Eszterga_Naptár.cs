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
    public class Kezelő_Kerék_Eszterga_Naptár
    {
        string hely;
        readonly string jelszó = "RónaiSándor";
        readonly string táblanév = "Naptár";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{Év}_Esztergálás.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kerék_Éves(hely);
        }

        public List<Adat_Kerék_Eszterga_Naptár> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            List<Adat_Kerék_Eszterga_Naptár> Adatok = new List<Adat_Kerék_Eszterga_Naptár>();
            Adat_Kerék_Eszterga_Naptár Adat;
            string szöveg = $"SELECT * FROM {táblanév}";

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
                                Adat = new Adat_Kerék_Eszterga_Naptár(
                                        rekord["Idő"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidő"].ToÉrt_Bool(),
                                        rekord["Foglalt"].ToÉrt_Bool(),
                                        rekord["Pályaszám"].ToStrTrim(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["betűszín"].ToÉrt_Long(),
                                        rekord["háttérszín"].ToÉrt_Long(),
                                        rekord["Marad"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        //elkopó
        public List<Adat_Kerék_Eszterga_Naptár> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Eszterga_Naptár> Adatok = new List<Adat_Kerék_Eszterga_Naptár>();
            Adat_Kerék_Eszterga_Naptár Adat;

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
                                Adat = new Adat_Kerék_Eszterga_Naptár(
                                        rekord["Idő"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidő"].ToÉrt_Bool(),
                                        rekord["Foglalt"].ToÉrt_Bool(),
                                        rekord["Pályaszám"].ToStrTrim(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["betűszín"].ToÉrt_Long(),
                                        rekord["háttérszín"].ToÉrt_Long(),
                                        rekord["Marad"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<DateTime> Lista_Adatok_Idő(string hely, string jelszó, string szöveg)
        {
            List<DateTime> Adatok = new List<DateTime>();
            DateTime Adat;

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
                                Adat = rekord["Idő"].ToÉrt_DaTeTime();
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kerék_Eszterga_Naptár Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kerék_Eszterga_Naptár Adat = null;

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
                            Adat = new Adat_Kerék_Eszterga_Naptár(
                                          rekord["Idő"].ToÉrt_DaTeTime(),
                                          rekord["Munkaidő"].ToÉrt_Bool(),
                                          rekord["Foglalt"].ToÉrt_Bool(),
                                          rekord["Pályaszám"].ToStrTrim(),
                                          rekord["Megjegyzés"].ToStrTrim(),
                                          rekord["betűszín"].ToÉrt_Long(),
                                          rekord["háttérszín"].ToÉrt_Long(),
                                          rekord["Marad"].ToÉrt_Bool()
                                          );
                        }
                    }
                }
            }
            return Adat;
        }

        public void Adat_RögzítésIdő(string hely, string jelszó, Adat_Kerék_Eszterga_Naptár Adat)
        {
            string szöveg = $"UPDATE naptár SET pályaszám='_', foglalt=false, Megjegyzés='', ";
            szöveg += $" betűszín=0, háttérszín=12632256, marad=false ";
            szöveg += $"WHERE idő=#{Adat.Idő:MM-dd-yyyy H:m:s}#";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Adat_Rögzítés(string hely, string jelszó, Adat_Kerék_Eszterga_Naptár Adat)
        {
            string szöveg = $"UPDATE naptár SET pályaszám='{Adat.Pályaszám.Trim()}', foglalt=true, Megjegyzés='{Adat.Megjegyzés.Trim()}', ";
            szöveg += $" betűszín={Adat.BetűSzín}, háttérszín={Adat.HáttérSzín}, marad={Adat.Marad} ";
            szöveg += $"WHERE idő=#{Adat.Idő:MM-dd-yyyy HH:mm}#";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public List<string> Lista_MindenbőlEgy(string hely, string jelszó, string szöveg)
        {
            List<string> Adatok = new List<string>();
            string Adat;

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
                                Adat = rekord["Pályaszám"].ToStrTrim();
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

}
