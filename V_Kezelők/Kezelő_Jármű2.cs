﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű2
    {
        readonly string jelszó = "pozsgaii";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\villamos\villamos2.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_2> Lista_Adatok(string Telephely)
        {
            string szöveg = $"SELECT * FROM állománytábla ";
            FájlBeállítás(Telephely);
            List<Adat_Jármű_2> Adatok = new List<Adat_Jármű_2>();
            Adat_Jármű_2 adat;

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
                                if (!DateTime.TryParse(rekord["takarítás"].ToString(), out DateTime takarítás))
                                    takarítás = new DateTime(1900, 1, 1);

                                adat = new Adat_Jármű_2(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["takarítás"].ToÉrt_DaTeTime(),
                                    rekord["haromnapos"].ToÉrt_Int()
                                    );
                                Adatok.Add(adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Jármű_2> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_2> Adatok = new List<Adat_Jármű_2>();
            Adat_Jármű_2 adat;

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
                                if (!DateTime.TryParse(rekord["takarítás"].ToString(), out DateTime takarítás))
                                    takarítás = new DateTime(1900, 1, 1);

                                adat = new Adat_Jármű_2(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["takarítás"].ToÉrt_DaTeTime(),
                                    rekord["haromnapos"].ToÉrt_Int()
                                    );
                                Adatok.Add(adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Jármű_2 Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Jármű_2 adat = null;

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
                            adat = new Adat_Jármű_2(
                                rekord["Azonosító"].ToStrTrim(),
                                rekord["takarítás"].ToÉrt_DaTeTime(),
                                rekord["haromnapos"].ToÉrt_Int()
                                );
                        }
                    }
                }
            }
            return adat;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Jármű_2 Adat)
        {
            // ha nem létezik 
            string szöveg = "INSERT INTO állománytábla  (  azonosító, takarítás, haromnapos ) VALUES (";
            szöveg += $"'{Adat.Azonosító.Trim()}', "; // azonosító
            szöveg += $"'{Adat.Takarítás}', "; // takarítás
            szöveg += $"{Adat.Haromnapos}) "; // haromnapos

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Módosít(string hely, string jelszó, Adat_Jármű_2 Adat)
        {

            // Ha már létezik, akkor módosítjuk
            string szöveg = "UPDATE állománytábla  SET ";
            szöveg += $"takarítás='{Adat.Takarítás}', "; // takarítás
            szöveg += $"haromnapos='{Adat.Haromnapos}' "; // haromnapos
            szöveg += $" WHERE azonosító='{Adat.Azonosító.Trim()}'";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void MódosítHárom(string hely, string jelszó, List<Adat_Jármű_2> Adatok)
        {
            List<string> SzövegGY = new List<string>();
            foreach (Adat_Jármű_2 Adat in Adatok)
            {
                string szöveg = "UPDATE állománytábla  SET ";
                szöveg += $"haromnapos='{Adat.Haromnapos}' "; // haromnapos
                szöveg += $" WHERE azonosító='{Adat.Azonosító.Trim()}'";
                SzövegGY.Add(szöveg);
            }
            MyA.ABMódosítás(hely, jelszó, SzövegGY);
        }

        public void Rögzítés(string hely, string jelszó, List<Adat_Jármű_2> Adatok)
        {
            List<string> SzövegGY = new List<string>();
            foreach (Adat_Jármű_2 Adat in Adatok)
            {
                // ha nem létezik 
                string szöveg = "INSERT INTO állománytábla  (  azonosító, takarítás, haromnapos ) VALUES (";
                szöveg += $"'{Adat.Azonosító.Trim()}', "; // azonosító
                szöveg += $"'{Adat.Takarítás}', "; // takarítás
                szöveg += $"{Adat.Haromnapos}) "; // haromnapos
                SzövegGY.Add(szöveg);
            }
            MyA.ABMódosítás(hely, jelszó, SzövegGY);
        }
    }


}
