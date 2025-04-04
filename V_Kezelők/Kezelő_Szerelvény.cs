using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;

namespace Villamos.Kezelők
{
    public class Kezelő_Szerelvény
    {
        readonly string jelszó = "pozsgaii";
        string hely;

        private void FájlBeállítás(string Telephely, bool előírt = false)
        {
            if (előírt)
                hely = $@"{Application.StartupPath}\{Telephely}\Adatok\villamos\szerelvényelőírt.mdb";
            else
                hely = $@"{Application.StartupPath}\{Telephely}\Adatok\villamos\szerelvény.mdb";

            if (!File.Exists(hely)) Adatbázis_Létrehozás.Szerelvénytáblalap(hely.KönyvSzerk());
        }

        public List<Adat_Szerelvény> Lista_Adatok(string Telephely, bool előírt = false)
        {
            FájlBeállítás(Telephely, előírt);
            string szöveg = "Select * FROM szerelvénytábla ORDER BY kocsi1";
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


    }
}
