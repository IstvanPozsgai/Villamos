using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű_Takarítás_Ár
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Takarítás\Jármű_Takarítás.mdb";
        readonly string jelszó = "seprűéslapát";

        public Kezelő_Jármű_Takarítás_Ár()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Járműtakarító_Főmérnök_tábla(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_Takarítás_Árak> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM árak";
            List<Adat_Jármű_Takarítás_Árak> Adatok = new List<Adat_Jármű_Takarítás_Árak>();
            Adat_Jármű_Takarítás_Árak Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Árak(
                                        rekord["id"].ToÉrt_Double(),
                                        rekord["JárműTípus"].ToStrTrim(),
                                        rekord["Takarítási_fajta"].ToStrTrim(),
                                        rekord["napszak"].ToÉrt_Int(),
                                        rekord["ár"].ToÉrt_Double(),
                                        rekord["Érv_kezdet"].ToÉrt_DaTeTime(),
                                        rekord["Érv_vég"].ToÉrt_DaTeTime()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(Adat_Jármű_Takarítás_Árak Adat)
        {
            string szöveg = $"UPDATE árak  SET ";
            szöveg += $"JárműTípus='{Adat.JárműTípus}', "; // JárműTípus
            szöveg += $"Takarítási_fajta='{Adat.Takarítási_fajta}', "; // Takarítási_fajta
            szöveg += $"napszak={Adat.Napszak}, ";
            szöveg += $"ár={Adat.Ár.ToString().Replace(",", ".")}, "; // ár
            szöveg += $"Érv_kezdet='{Adat.Érv_kezdet:yyyy.MM.dd}', ";
            szöveg += $"Érv_vég='{Adat.Érv_vég:yyyy.MM.dd}' ";
            szöveg += $" WHERE id={Adat.Id}";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Módosítás(List<Adat_Jármű_Takarítás_Árak> Adatok)
        {
            List<string> szövegGy = new List<string>();
            foreach (Adat_Jármű_Takarítás_Árak Adat in Adatok)
            {
                string szöveg = $"UPDATE árak  SET ";
                szöveg += $"JárműTípus='{Adat.JárműTípus}', "; // JárműTípus
                szöveg += $"Takarítási_fajta='{Adat.Takarítási_fajta}', "; // Takarítási_fajta
                szöveg += $"napszak={Adat.Napszak}, ";
                szöveg += $"ár={Adat.Ár.ToString().Replace(",", ".")}, "; // ár
                szöveg += $"Érv_kezdet='{Adat.Érv_kezdet:yyyy.MM.dd}', ";
                szöveg += $"Érv_vég='{Adat.Érv_vég:yyyy.MM.dd}' ";
                szöveg += $" WHERE id={Adat.Id}";
                szövegGy.Add(szöveg);
            }
            MyA.ABMódosítás(hely, jelszó, szövegGy);
        }

        public void Módosítás_Vég(List<Adat_Jármű_Takarítás_Árak> Adatok)
        {
            List<string> szövegGy = new List<string>();
            foreach (Adat_Jármű_Takarítás_Árak Adat in Adatok)
            {
                string szöveg = $"UPDATE árak  SET ";
                szöveg += $"Érv_vég='{Adat.Érv_vég:yyyy.MM.dd}' ";
                szöveg += $" WHERE id={Adat.Id}";
                szövegGy.Add(szöveg);
            }
            MyA.ABMódosítás(hely, jelszó, szövegGy);
        }

        public void Rögzítés(Adat_Jármű_Takarítás_Árak Adat)
        {
            string szöveg = $"INSERT INTO árak (id, JárműTípus, Takarítási_fajta, napszak, ár, Érv_kezdet, Érv_vég ) VALUES (";
            szöveg += $"{Adat.Id}, "; // id 
            szöveg += $"'{Adat.JárműTípus}', "; // JárműTípus
            szöveg += $"'{Adat.Takarítási_fajta}', "; // Takarítási_fajta
            szöveg += $"{Adat.Napszak}, ";
            szöveg += $"{Adat.Ár.ToString().Replace(",", ".")}, "; // ár
            szöveg += $"'{Adat.Érv_kezdet:yyyy.MM.dd}', ";
            szöveg += $"'{Adat.Érv_vég:yyyy.MM.dd}') ";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Rögzítés(List<Adat_Jármű_Takarítás_Árak> Adatok)
        {
            List<string> szövegGy = new List<string>();
            foreach (Adat_Jármű_Takarítás_Árak Adat in Adatok)
            {
                string szöveg = $"INSERT INTO árak (id, JárműTípus, Takarítási_fajta, napszak, ár, Érv_kezdet, Érv_vég ) VALUES (";
                szöveg += $"{Adat.Id}, "; // id 
                szöveg += $"'{Adat.JárműTípus}', "; // JárműTípus
                szöveg += $"'{Adat.Takarítási_fajta}', "; // Takarítási_fajta
                szöveg += $"{Adat.Napszak}, ";
                szöveg += $"{Adat.Ár.ToString().Replace(",", ".")}, "; // ár
                szöveg += $"'{Adat.Érv_kezdet:yyyy.MM.dd}', ";
                szöveg += $"'{Adat.Érv_vég:yyyy.MM.dd}') ";
                szövegGy.Add(szöveg);
            }
            MyA.ABMódosítás(hely, jelszó, szövegGy);
        }
    }

}
