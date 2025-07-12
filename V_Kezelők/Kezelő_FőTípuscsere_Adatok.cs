using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_FőTípuscsere_Adatok
    {
        string hely;
        readonly string jelszó = "pozsi";
        readonly string táblanév = "típuscseretábla";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Év}\{Év}_típuscsere_adatok.mdb".KönyvSzerk();
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kiadásitípuscserefőmérnöktábla(hely);
        }


        public List<Adat_Típuscsere_Adatok> Lista_adatok(int Év)
        {
            FájlBeállítás(Év);
            List<Adat_Típuscsere_Adatok> Adatok = new List<Adat_Típuscsere_Adatok>();
            string szöveg = $"SELECT * FROM {táblanév} ";

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
                                Adat_Típuscsere_Adatok Adat = new Adat_Típuscsere_Adatok(
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["szolgálat"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["típuselőírt"].ToStrTrim(),
                                    rekord["típuskiadott"].ToStrTrim(),
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["tervindulás"].ToÉrt_DaTeTime(),
                                    rekord["azonosító"].ToStrTrim()
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
        public List<Adat_Típuscsere_Adatok> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Típuscsere_Adatok> Adatok = new List<Adat_Típuscsere_Adatok>();
            Adat_Típuscsere_Adatok Adat;

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
                                Adat = new Adat_Típuscsere_Adatok(
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["szolgálat"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["típuselőírt"].ToStrTrim(),
                                    rekord["típuskiadott"].ToStrTrim(),
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["tervindulás"].ToÉrt_DaTeTime(),
                                    rekord["azonosító"].ToStrTrim()
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
