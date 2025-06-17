using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{


    public class Kezelő_Kerék_Eszterga_Tengely
    {
        public List<Adat_Kerék_Eszterga_Tengely> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Eszterga_Tengely> Adatok = new List<Adat_Kerék_Eszterga_Tengely>();
            Adat_Kerék_Eszterga_Tengely Adat;

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
                                Adat = new Adat_Kerék_Eszterga_Tengely(
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Munkaidő"].ToÉrt_Int(),
                                        rekord["Állapot"].ToÉrt_Int()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Egy_Rögzítés(string hely, string jelszó, Adat_Kerék_Eszterga_Tengely Adat)
        {
            string szöveg = $"INSERT INTO tengely ( Típus, munkaidő, állapot) VALUES ('{Adat.Típus.Trim()}', {Adat.Munkaidő}, {Adat.Állapot})";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Egy_Módosítás(string hely, string jelszó, Adat_Kerék_Eszterga_Tengely Adat)
        {
            string szöveg = "UPDATE tengely SET ";
            szöveg += $" munkaidő={Adat.Munkaidő} ";
            szöveg += $" WHERE típus='{Adat.Típus.Trim()}' AND Állapot={Adat.Állapot}";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
    }


    public class Kezelő_Kerék_Eszterga_Automata
    {
        public List<Adat_Kerék_Eszterga_Automata> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Eszterga_Automata> Adatok = new List<Adat_Kerék_Eszterga_Automata>();
            Adat_Kerék_Eszterga_Automata Adat;

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
                                Adat = new Adat_Kerék_Eszterga_Automata(
                                        rekord["FelhasználóiNév"].ToStrTrim(),
                                        rekord["UtolsóÜzenet"].ToÉrt_DaTeTime()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kerék_Eszterga_Automata Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kerék_Eszterga_Automata Adat = null;

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
                            Adat = new Adat_Kerék_Eszterga_Automata(
                                    rekord["FelhasználóiNév"].ToStrTrim(),
                                    rekord["UtolsóÜzenet"].ToÉrt_DaTeTime()
                                    );
                        }
                    }
                }
            }
            return Adat;
        }
    }


}
