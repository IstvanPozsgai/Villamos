using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Takarítás_Opció
    {
        public List<Adat_Takarítás_Opció> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Takarítás_Opció> Adatok = new List<Adat_Takarítás_Opció>();
            Adat_Takarítás_Opció Adat;

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
                                Adat = new Adat_Takarítás_Opció(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Megnevezés"].ToStrTrim(),
                                        rekord["Mennyisége"].ToString(),
                                        rekord["Ár"].ToÉrt_Double(),
                                        rekord["Kezdet"].ToÉrt_DaTeTime(),
                                        rekord["Vég"].ToÉrt_DaTeTime()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzít(string hely, string jelszó, Adat_Takarítás_Opció Adat)
        {
            string szöveg = "INSERT INTO TakarításOpcionális (Id, Megnevezés, Mennyisége, Ár, Kezdet, Vég) VALUES (";
            szöveg += $"{Adat.Id}, '{Adat.Megnevezés}', '{Adat.Mennyisége}', {Adat.Ár}, '{Adat.Kezdet.ToShortDateString()}', '{Adat.Vég.ToShortDateString()}')";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Módosít(string hely, string jelszó, Adat_Takarítás_Opció Adat)
        {
            string szöveg = "UPDATE TakarításOpcionális  SET ";
            szöveg += $"Megnevezés='{Adat.Megnevezés}', ";
            szöveg += $"Mennyisége='{Adat.Mennyisége}', ";
            szöveg += $"Ár={Adat.Ár}, ";
            szöveg += $"Kezdet='{Adat.Kezdet.ToShortDateString()}', ";
            szöveg += $"Vég='{Adat.Vég.ToShortDateString()}' ";
            szöveg += $" WHERE id={Adat.Id}";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
    }


    public class Kezelő_Takarítás_Telep_Opció
    {
        public List<Adat_Takarítás_Telep_Opció> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Takarítás_Telep_Opció> Adatok = new List<Adat_Takarítás_Telep_Opció>();
            Adat_Takarítás_Telep_Opció Adat;

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
                                Adat = new Adat_Takarítás_Telep_Opció(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Megrendelt"].ToÉrt_Double(),
                                        rekord["Teljesített"].ToÉrt_Double()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzít(string hely, string jelszó, Adat_Takarítás_Telep_Opció Adat)
        {
            string szöveg = "INSERT INTO TakarításOpcTelepAdatok (Id, Dátum, Megrendelt, Teljesített) VALUES (";
            szöveg += $"{Adat.Id}, '{Adat.Dátum}', {Adat.Megrendelt}, {Adat.Teljesített}))";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }


        public void Rögzít(string hely, string jelszó, List<Adat_Takarítás_Telep_Opció> Adatok)
        {
            List<string> SzövegGy = new List<string>();
            foreach (Adat_Takarítás_Telep_Opció Adat in Adatok)
            {
                string szöveg = "INSERT INTO TakarításOpcTelepAdatok (Id, Dátum, Megrendelt, Teljesített) VALUES (";
                szöveg += $"{Adat.Id}, '{Adat.Dátum.ToShortDateString()}', {Adat.Megrendelt}, {Adat.Teljesített})";
                SzövegGy.Add(szöveg);
            }
            MyA.ABMódosítás(hely, jelszó, SzövegGy);
        }
  
        public void Módosít(string hely, string jelszó, Adat_Takarítás_Telep_Opció Adat)
        {
            string szöveg = "UPDATE TakarításOpcTelepAdatok  SET ";
            szöveg += $"Dátum='{Adat.Dátum.ToShortDateString()}', ";
            szöveg += $"Megrendelt={Adat.Megrendelt}, ";
            szöveg += $"Teljesített={Adat.Teljesített} ";
            szöveg += $" WHERE id={Adat.Id}";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Módosít(string hely, string jelszó, List<Adat_Takarítás_Telep_Opció> Adatok)
        {
            List<string> SzövegGy = new List<string>();
            foreach (Adat_Takarítás_Telep_Opció Adat in Adatok)
            {
                string szöveg = "UPDATE TakarításOpcTelepAdatok  SET ";
                szöveg += $"Dátum='{Adat.Dátum.ToShortDateString()}', ";
                szöveg += $"Megrendelt={Adat.Megrendelt}, ";
                szöveg += $"Teljesített={Adat.Teljesített} ";
                szöveg += $" WHERE id={Adat.Id}";
                SzövegGy.Add(szöveg);
            }
            MyA.ABMódosítás(hely, jelszó, SzövegGy);
        }
    }
}
