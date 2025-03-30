using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Forte_Kiadási_Adatok
    {
        readonly string jelszó = "gémkapocs";
        string hely;

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Év}\{Év}_fortekiadási_adatok.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Fortekiadásifőmtábla(hely.KönyvSzerk());
        }

        public List<Adat_Forte_Kiadási_Adatok> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = "SELECT * FROM fortekiadástábla";
            List<Adat_Forte_Kiadási_Adatok> Adatok = new List<Adat_Forte_Kiadási_Adatok>();
            Adat_Forte_Kiadási_Adatok Adat;

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
                                Adat = new Adat_Forte_Kiadási_Adatok(
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["telephelyforte"].ToStrTrim(),
                                    rekord["típusforte"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["kiadás"].ToÉrt_Long(),
                                    rekord["munkanap"].ToÉrt_Long()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        //Elkopó
        public List<Adat_Forte_Kiadási_Adatok> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Forte_Kiadási_Adatok> Adatok = new List<Adat_Forte_Kiadási_Adatok>();
            Adat_Forte_Kiadási_Adatok Adat;

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
                                Adat = new Adat_Forte_Kiadási_Adatok(
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["telephelyforte"].ToStrTrim(),
                                    rekord["típusforte"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["kiadás"].ToÉrt_Long(),
                                    rekord["munkanap"].ToÉrt_Long()
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
