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
    public class Kezelő_Szerelvény_Napló
    {
        readonly string jelszó = "pozsgaii";
        string hely;
        readonly string táblanév = "szerelvénytáblanapló";

        private void FájlBeállítás(string Telephely, DateTime Dátum)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\hibanapló\{Dátum:yyyyMM}szerelvény.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Szerelvénytáblalapnapló(hely.KönyvSzerk());
        }

        public List<Adat_Szerelvény_Napló> Lista_Adatok(string Telephely, DateTime Dátum)
        {
            FájlBeállítás(Telephely, Dátum);
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY mikor desc";
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

        public void Rögzítés(string Telephely, DateTime Dátum, Adat_Szerelvény Adat)
        {
            FájlBeállítás(Telephely, Dátum);
            string szöveg = $"INSERT INTO {táblanév} (id, kocsi1, kocsi2, kocsi3, kocsi4, kocsi5, kocsi6, szerelvényhossz, módosító, mikor) VALUES (";
            szöveg += $"{Adat.Szerelvény_ID}, ";
            szöveg += $"'{Adat.Kocsi1}', ";
            szöveg += $"'{Adat.Kocsi2}', ";
            szöveg += $"'{Adat.Kocsi3}', ";
            szöveg += $"'{Adat.Kocsi4}', ";
            szöveg += $"'{Adat.Kocsi5}', ";
            szöveg += $"'{Adat.Kocsi6}', ";
            szöveg += $"{Adat.Szerelvényhossz}, ";
            szöveg += $"'{Program.PostásNév.Trim()}', ";
            szöveg += $"'{DateTime.Now}') ";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
    }
}
