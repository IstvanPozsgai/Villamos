using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{

    public class Kezelő_Oktatás_Napló
    {
        string hely;
        readonly string jelszó = "pázmányt";

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\{Év}\Oktatásnapló_{Telephely}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Oktatás_Napló(hely.KönyvSzerk());
        }


        public List<Adat_Oktatás_Napló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Oktatás_Napló> Adatok = new List<Adat_Oktatás_Napló>();
            Adat_Oktatás_Napló Adat;

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
                                Adat = new Adat_Oktatás_Napló(
                                    rekord["ID"].ToÉrt_Long(),
                                    rekord["HRazonosító"].ToStrTrim(),
                                    rekord["IDoktatás"].ToÉrt_Long(),
                                    rekord["Oktatásdátuma"].ToÉrt_DaTeTime(),
                                    rekord["Kioktatta"].ToStrTrim(),
                                    rekord["Rögzítésdátuma"].ToÉrt_DaTeTime(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["PDFFájlneve"].ToStrTrim(),
                                    rekord["Számonkérés"].ToÉrt_Long(),
                                    rekord["státus"].ToÉrt_Long(),
                                    rekord["Rögzítő"].ToStrTrim(),
                                    rekord["Megjegyzés"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Oktatás_Napló> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = "SELECT * FROM oktatásnapló ";
            List<Adat_Oktatás_Napló> Adatok = new List<Adat_Oktatás_Napló>();
            Adat_Oktatás_Napló Adat;

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
                                Adat = new Adat_Oktatás_Napló(
                                    rekord["ID"].ToÉrt_Long(),
                                    rekord["HRazonosító"].ToStrTrim(),
                                    rekord["IDoktatás"].ToÉrt_Long(),
                                    rekord["Oktatásdátuma"].ToÉrt_DaTeTime(),
                                    rekord["Kioktatta"].ToStrTrim(),
                                    rekord["Rögzítésdátuma"].ToÉrt_DaTeTime(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["PDFFájlneve"].ToStrTrim(),
                                    rekord["Számonkérés"].ToÉrt_Long(),
                                    rekord["státus"].ToÉrt_Long(),
                                    rekord["Rögzítő"].ToStrTrim(),
                                    rekord["Megjegyzés"].ToStrTrim()
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
