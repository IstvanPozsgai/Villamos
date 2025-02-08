using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{

    public class Kezelő_Technológia_Ciklus
    {
        readonly string jelszó = "Bezzegh";
        string hely;

        private void FájlBeállítás(string Típus)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Típus}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Technológia_Adat(hely.KönyvSzerk());
        }

        public List<Adat_technológia_Ciklus> Lista_Adatok(string Típus)
        {
            FájlBeállítás(Típus);
            string szöveg = "SELECT * FROM karbantartás ORDER BY sorszám";
            List<Adat_technológia_Ciklus> Adatok = new List<Adat_technológia_Ciklus>();
            Adat_technológia_Ciklus Adat;

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
                                Adat = new Adat_technológia_Ciklus(
                                    rekord["sorszám"].ToÉrt_Int(),
                                    rekord["fokozat"].ToStrTrim(),
                                    rekord["csoportos"].ToÉrt_Int(),
                                    rekord["elérés"].ToStrTrim(),
                                    rekord["verzió"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }

                    }
                }
            }
            return Adatok;
        }

        public List<Adat_technológia_Ciklus> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_technológia_Ciklus> Adatok = new List<Adat_technológia_Ciklus>();
            Adat_technológia_Ciklus Adat;

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
                                Adat = new Adat_technológia_Ciklus(
                                    rekord["sorszám"].ToÉrt_Int(),
                                    rekord["fokozat"].ToStrTrim(),
                                    rekord["csoportos"].ToÉrt_Int(),
                                    rekord["elérés"].ToStrTrim(),
                                    rekord["verzió"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }

                    }
                }
            }
            return Adatok;
        }

        public Adat_technológia_Ciklus Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_technológia_Ciklus Adat = null;

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
                                Adat = new Adat_technológia_Ciklus(
                                    rekord["sorszám"].ToÉrt_Int(),
                                    rekord["fokozat"].ToStrTrim(),
                                    rekord["csoportos"].ToÉrt_Int(),
                                    rekord["elérés"].ToStrTrim(),
                                     rekord["verzió"].ToStrTrim()
                                    );
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }

}
