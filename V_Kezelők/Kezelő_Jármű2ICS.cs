using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű2ICS
    {
        readonly string jelszó = "pozsgaii";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\villamos\villamos2ICS.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.VillamostáblaICS(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_2ICS> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_2ICS> Adatok = new List<Adat_Jármű_2ICS>();
            Adat_Jármű_2ICS adat;

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
                                adat = new Adat_Jármű_2ICS(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["takarítás"].ToÉrt_DaTeTime(),
                                    rekord["E2"].ToÉrt_Int(),
                                    rekord["E3"].ToÉrt_Int()
                                    );
                                Adatok.Add(adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public List<Adat_Jármű_2ICS> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            List<Adat_Jármű_2ICS> Adatok = new List<Adat_Jármű_2ICS>();
            Adat_Jármű_2ICS adat;
            string szöveg = $"SELECT * FROM állománytábla";
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
                                adat = new Adat_Jármű_2ICS(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["takarítás"].ToÉrt_DaTeTime(),
                                    rekord["E2"].ToÉrt_Int(),
                                    rekord["E3"].ToÉrt_Int()
                                    );
                                Adatok.Add(adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

    }

}
