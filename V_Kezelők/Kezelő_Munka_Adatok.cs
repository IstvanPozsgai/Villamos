using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Munka_Adatok
    {
        readonly string jelszó = "dekádoló";

        public List<Adat_Munka_Adatok> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * FROM Adatoktábla";
            List<Adat_Munka_Adatok> Adatok = new List<Adat_Munka_Adatok>();
            Adat_Munka_Adatok Adat;

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
                                Adat = new Adat_Munka_Adatok(
                                          rekord["ID"].ToÉrt_Long(),
                                          rekord["idő"].ToÉrt_Int(),
                                          rekord["dátum"].ToÉrt_DaTeTime(),
                                          rekord["megnevezés"].ToString(),
                                          rekord["művelet"].ToString(),
                                          rekord["pályaszám"].ToString(),
                                          rekord["rendelés"].ToString(),
                                          rekord["státus"].ToÉrt_Bool()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Munka_Adatok> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Munka_Adatok> Adatok = new List<Adat_Munka_Adatok>();
            Adat_Munka_Adatok Adat;

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
                                Adat = new Adat_Munka_Adatok(
                                          rekord["ID"].ToÉrt_Long(),
                                          rekord["idő"].ToÉrt_Int(),
                                          rekord["dátum"].ToÉrt_DaTeTime(),
                                          rekord["megnevezés"].ToString(),
                                          rekord["művelet"].ToString(),
                                          rekord["pályaszám"].ToString(),
                                          rekord["rendelés"].ToString(),
                                          rekord["státus"].ToÉrt_Bool()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Munka_Adatok> Lista_Adatok_Szűk(string hely, string jelszó, string szöveg)
        {
            List<Adat_Munka_Adatok> Adatok = new List<Adat_Munka_Adatok>();
            Adat_Munka_Adatok Adat;

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
                                Adat = new Adat_Munka_Adatok(
                                          rekord["megnevezés"].ToString(),
                                          rekord["művelet"].ToString(),
                                          rekord["pályaszám"].ToString(),
                                          rekord["rendelés"].ToString()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Munka_Adatok> Lista_Adat_SUM_List(string hely, string jelszó, string szöveg)
        {
            List<Adat_Munka_Adatok> Adatok = new List<Adat_Munka_Adatok>();
            Adat_Munka_Adatok Adat;

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
                                Adat = new Adat_Munka_Adatok(
                                          rekord["SUMIdő"].ToÉrt_Int(),
                                          rekord["dátum"].ToÉrt_DaTeTime(),
                                          rekord["megnevezés"].ToString(),
                                          rekord["művelet"].ToString(),
                                          rekord["pályaszám"].ToString(),
                                          rekord["rendelés"].ToString(),
                                          rekord["státus"].ToÉrt_Bool()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Munka_Adatok> Lista_AdatokSUM(string hely, string jelszó, string szöveg)
        {
            List<Adat_Munka_Adatok> Adatok = new List<Adat_Munka_Adatok>();
            Adat_Munka_Adatok Adat;

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
                                Adat = new Adat_Munka_Adatok(
                                          rekord["SUMidő"].ToÉrt_Int(),
                                          rekord["művelet"].ToString(),
                                          rekord["rendelés"].ToString()
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
