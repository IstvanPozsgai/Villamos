using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

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

        public void Rögzítés(string hely, List<Adat_Munka_Adatok> Adatok)
        {
            try
            {
                List<string> szövegGy = new List<string>();
                foreach (Adat_Munka_Adatok adat in Adatok)
                {
                    string szöveg = "INSERT INTO Adatoktábla (rendelés, művelet, megnevezés, pályaszám, idő, dátum, státus) VALUES (";
                    szöveg += $"'{adat.Rendelés}', ";
                    szöveg += $"'{adat.Művelet}', ";
                    szöveg += $"'{adat.Megnevezés}', ";
                    szöveg += $"'{adat.Pályaszám}', ";
                    szöveg += $"{adat.Idő}, ";
                    szöveg += $"'{adat.Dátum:yyyy.MM.dd}', ";
                    szöveg += $"'{adat.Státus}') ";
                    szövegGy.Add(szöveg);
                }

                MyA.ABMódosítás(hely, jelszó, szövegGy);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás(string hely, string rendelés, int Id)
        {
            try
            {
                string szöveg = $"UPDATE Adatoktábla SET rendelés='{rendelés}' WHERE id={Id}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás(string hely, List<long> idk)
        {
            try
            {
                List<string> szövegGy = new List<string>();
                foreach (long elem in idk)
                {
                    string szöveg = $"UPDATE Adatoktábla SET státus=false WHERE id={elem}";
                    szövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
