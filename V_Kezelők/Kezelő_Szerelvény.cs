using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Szerelvény
    {

        /// <summary>
        /// A feltételknek megfelelő listát adja vissza
        /// </summary>
        /// <param name="hely">fájl elérhetősége</param>
        /// <param name="jelszó">Jelszó</param>
        /// <param name="szöveg">SQL</param>
        /// <returns></returns>
        public List<Adat_Szerelvény> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Szerelvény> AdatKocsik = new List<Adat_Szerelvény>();

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
                                Adat_Szerelvény Adat = new Adat_Szerelvény(
                                                rekord["id"].ToÉrt_Long(),
                                                rekord["szerelvényhossz"].ToÉrt_Long(),
                                                rekord["Kocsi1"].ToStrTrim(),
                                                rekord["Kocsi2"].ToStrTrim(),
                                                rekord["Kocsi3"].ToStrTrim(),
                                                rekord["Kocsi4"].ToStrTrim(),
                                                rekord["Kocsi5"].ToStrTrim(),
                                                rekord["Kocsi6"].ToStrTrim());
                                AdatKocsik.Add(Adat);
                            }
                        }
                    }
                }
            }
            return AdatKocsik;
        }

        /// <summary>
        /// A szerelvény Id alapján visszaadja a teljes rekordot
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="szerelvényId"></param>
        /// <returns></returns>
        public Adat_Szerelvény SzerelvényEgy(string hely, string jelszó, long szerelvényId)
        {
            Adat_Szerelvény Adat = null;

            string szöveg = "SELECT * FROM szerelvénytábla WHERE id=" + szerelvényId.ToString();

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

                            Adat = new Adat_Szerelvény(
                                       rekord["id"].ToÉrt_Long(),
                                       rekord["szerelvényhossz"].ToÉrt_Long(),
                                       rekord["Kocsi1"].ToStrTrim(),
                                       rekord["Kocsi2"].ToStrTrim(),
                                       rekord["Kocsi3"].ToStrTrim(),
                                       rekord["Kocsi4"].ToStrTrim(),
                                       rekord["Kocsi5"].ToStrTrim(),
                                       rekord["Kocsi6"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }

        public Adat_Szerelvény Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Szerelvény Adat = null;

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
                            Adat = new Adat_Szerelvény(
                                       rekord["id"].ToÉrt_Long(),
                                       rekord["szerelvényhossz"].ToÉrt_Long(),
                                       rekord["Kocsi1"].ToStrTrim(),
                                       rekord["Kocsi2"].ToStrTrim(),
                                       rekord["Kocsi3"].ToStrTrim(),
                                       rekord["Kocsi4"].ToStrTrim(),
                                       rekord["Kocsi5"].ToStrTrim(),
                                       rekord["Kocsi6"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }

        public void Szerelvény_módosítás(string hely, string jelszó, Adat_Szerelvény Adat)
        {
            string szöveg;
            szöveg = "UPDATE szerelvénytábla SET ";
            szöveg += "kocsi1='" + Adat.Kocsi1 + "', ";
            szöveg += "kocsi2='" + Adat.Kocsi2 + "', ";
            szöveg += "kocsi3='" + Adat.Kocsi3 + "', ";
            szöveg += "kocsi4='" + Adat.Kocsi4 + "', ";
            szöveg += "kocsi5='" + Adat.Kocsi5 + "', ";
            szöveg += "kocsi6='" + Adat.Kocsi6 + "', ";
            szöveg += "szerelvényhossz=" + Adat.Szerelvényhossz.ToString() + " ";
            szöveg += " WHERE id=" + Adat.Szerelvény_ID.ToString();

            Adatbázis.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Új_Szerelvény(string hely, string jelszó, Adat_Szerelvény Adat)
        {
            string szöveg;
            szöveg = "INSERT INTO szerelvénytábla (id, kocsi1, kocsi2, kocsi3, kocsi4, kocsi5, kocsi6, szerelvényhossz) VALUES (";

            szöveg += Adat.Szerelvény_ID.ToString() + ", '"
                    + Adat.Kocsi1 + "', '" + Adat.Kocsi2 + "', '"
                    + Adat.Kocsi3 + "', '" + Adat.Kocsi4 + "', '"
                    + Adat.Kocsi5 + "', '" + Adat.Kocsi6 + "', "
                    + Adat.Szerelvényhossz.ToString() + "  )";

            Adatbázis.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Naplózás_Szerelvény(string hely, string jelszó, Adat_Szerelvény Adat)
        {
            string szöveg;
            szöveg = "INSERT INTO szerelvénytáblanapló (id, kocsi1, kocsi2, kocsi3, kocsi4, kocsi5, kocsi6, szerelvényhossz, módosító, mikor) VALUES (";
            szöveg += Adat.Szerelvény_ID.ToString() + ", ";
            szöveg += "'" + Adat.Kocsi1 + "', ";
            szöveg += "'" + Adat.Kocsi2 + "', ";
            szöveg += "'" + Adat.Kocsi3 + "', ";
            szöveg += "'" + Adat.Kocsi4 + "', ";
            szöveg += "'" + Adat.Kocsi5 + "', ";
            szöveg += "'" + Adat.Kocsi6 + "', ";
            szöveg += Adat.Szerelvényhossz.ToString() + ", ";
            szöveg += "'" + Program.PostásNév.Trim() + "', ";
            szöveg += "'" + DateTime.Now.ToString() + "') ";
            Adatbázis.ABMódosítás(hely, jelszó, szöveg);
        }
    }

    public class Kezelő_Szerelvény_Napló
    {
        public List<Adat_Szerelvény_Napló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Szerelvény_Napló> Adatok = new List<Adat_Szerelvény_Napló>();

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
                                Adat_Szerelvény_Napló Adat = new Adat_Szerelvény_Napló(
                                                rekord["id"].ToÉrt_Long(),
                                                rekord["szerelvényhossz"].ToÉrt_Long(),
                                                rekord["Kocsi1"].ToStrTrim(),
                                                rekord["Kocsi2"].ToStrTrim(),
                                                rekord["Kocsi3"].ToStrTrim(),
                                                rekord["Kocsi4"].ToStrTrim(),
                                                rekord["Kocsi5"].ToStrTrim(),
                                                rekord["Kocsi6"].ToStrTrim(),
                                                rekord["Módosító"].ToStrTrim(),
                                                rekord["mikor"].ToÉrt_DaTeTime()
                                                );
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
