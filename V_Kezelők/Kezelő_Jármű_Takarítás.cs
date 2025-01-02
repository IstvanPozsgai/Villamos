using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Jármű_Takarítás_Vezénylés
    {
        public List<Adat_Jármű_Takarítás_Vezénylés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Vezénylés> Adatok = new List<Adat_Jármű_Takarítás_Vezénylés>();
            Adat_Jármű_Takarítás_Vezénylés Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Vezénylés(
                                        rekord["id"].ToÉrt_Long(),
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["takarítási_fajta"].ToStrTrim(),
                                        rekord["szerelvényszám"].ToÉrt_Long(),
                                        rekord["státus"].ToÉrt_Int()
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

    public class Kezelő_Jármű_Takarítás_Ár
    {
        public List<Adat_Jármű_Takarítás_Árak> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
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

        /// <summary>
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Jármű_Takarítás_Árak Adat)
        {
            string szöveg = "UPDATE árak  SET ";
            szöveg += $"JárműTípus='{Adat.JárműTípus}', "; // JárműTípus
            szöveg += $"Takarítási_fajta='{Adat.Takarítási_fajta}', "; // Takarítási_fajta
            szöveg += $"napszak={Adat.Napszak}, ";
            szöveg += $"ár={Adat.Ár.ToString().Replace(",", ".")}, "; // ár
            szöveg += $"Érv_kezdet='{Adat.Érv_kezdet:yyyy.MM.dd}', ";
            szöveg += $"Érv_vég='{Adat.Érv_vég:yyyy.MM.dd}' ";
            szöveg += $" WHERE id={Adat.Id}";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Módosítás(string hely, string jelszó, List<Adat_Jármű_Takarítás_Árak> Adatok)
        {
            List<string> szövegGy = new List<string>();
            foreach (Adat_Jármű_Takarítás_Árak Adat in Adatok)
            {
                string szöveg = "UPDATE árak  SET ";
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
        /// <summary>
        /// Érv vége módosítás
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adatok"></param>
        public void Módosítás_Vég(string hely, string jelszó, List<Adat_Jármű_Takarítás_Árak> Adatok)
        {
            List<string> szövegGy = new List<string>();
            foreach (Adat_Jármű_Takarítás_Árak Adat in Adatok)
            {
                string szöveg = "UPDATE árak  SET ";
                szöveg += $"Érv_vég='{Adat.Érv_vég:yyyy.MM.dd}' ";
                szöveg += $" WHERE id={Adat.Id}";
                szövegGy.Add(szöveg);
            }
            MyA.ABMódosítás(hely, jelszó, szövegGy);
        }

        public void Rögzítés(string hely, string jelszó, Adat_Jármű_Takarítás_Árak Adat)
        {
            string szöveg = "INSERT INTO árak (id, JárműTípus, Takarítási_fajta, napszak, ár, Érv_kezdet, Érv_vég ) VALUES (";
            szöveg += $"{Adat.Id}, "; // id 
            szöveg += $"'{Adat.JárműTípus}', "; // JárműTípus
            szöveg += $"'{Adat.Takarítási_fajta}', "; // Takarítási_fajta
            szöveg += $"{Adat.Napszak}, ";
            szöveg += $"{Adat.Ár.ToString().Replace(",", ".")}, "; // ár
            szöveg += $"'{Adat.Érv_kezdet:yyyy.MM.dd}', ";
            szöveg += $"'{Adat.Érv_vég:yyyy.MM.dd}') ";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Rögzítés(string hely, string jelszó, List<Adat_Jármű_Takarítás_Árak> Adatok)
        {
            List<string> szövegGy = new List<string>();
            foreach (Adat_Jármű_Takarítás_Árak Adat in Adatok)
            {
                string szöveg = "INSERT INTO árak (id, JárműTípus, Takarítási_fajta, napszak, ár, Érv_kezdet, Érv_vég ) VALUES (";
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

    public class Kezelő_Jármű_Takarítás
    {
        public List<Adat_Jármű_Takarítás_Takarítások> Takarítások_Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Takarítások> Adatok = new List<Adat_Jármű_Takarítás_Takarítások>();
            Adat_Jármű_Takarítás_Takarítások Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Takarítások(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["takarítási_fajta"].ToStrTrim(),
                                        rekord["telephely"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Int()
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

    public class Kezelő_Jármű_Takarítás_típus
    {
        public List<Adat_Jármű> Állomány_Lista_típus(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű> Adatok = new List<Adat_Jármű>();
            Adat_Jármű Adat;

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
                                Adat = new Adat_Jármű(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["típus"].ToStrTrim()
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

    public class Kezelő_Jármű_Takarítás_Napló
    {
        public List<Adat_Jármű_Takarítás_Napló> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Napló> Adatok = new List<Adat_Jármű_Takarítás_Napló>();
            Adat_Jármű_Takarítás_Napló Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Napló(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["Takarítási_fajta"].ToStrTrim(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Mikor"].ToÉrt_DaTeTime(),
                                        rekord["Módosító"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Int()
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

    public class Kezelő_Jármű_Takarítás_Kötbér
    {
        public List<Adat_Jármű_Takarítás_Kötbér> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Kötbér> Adatok = new List<Adat_Jármű_Takarítás_Kötbér>();
            Adat_Jármű_Takarítás_Kötbér Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Kötbér(
                                        rekord["takarítási_fajta"].ToStrTrim(),
                                        rekord["nemMegfelel"].ToStrTrim(),
                                        rekord["póthatáridő"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Jármű_Takarítás_Kötbér Adat)
        {
            string szöveg = "INSERT INTO kötbér (Takarítási_fajta, NemMegfelel, Póthatáridő ) VALUES (";
            szöveg += $"'{Adat.Takarítási_fajta}', "; // Takarítási_fajta
            szöveg += $"{Adat.NemMegfelel}, "; // NemMegfelel
            szöveg += $"{Adat.Póthatáridő}) ";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
        /// <summary>
        /// takarítási_fajta
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Jármű_Takarítás_Kötbér Adat)
        {
            string szöveg = "UPDATE kötbér  SET ";
            szöveg += $" NemMegfelel={Adat.NemMegfelel}, "; // NemMegfelel
            szöveg += $" Póthatáridő={Adat.Póthatáridő}"; // Póthatáridő
            szöveg += $" WHERE  takarítási_fajta='{Adat.Takarítási_fajta}'";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }


    }

    public class Kezelő_Jármű_Takarítás_Mátrix
    {
        public List<Adat_Jármű_Takarítás_Mátrix> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Mátrix> Adatok = new List<Adat_Jármű_Takarítás_Mátrix>();
            Adat_Jármű_Takarítás_Mátrix Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Mátrix(
                                        rekord["id"].ToÉrt_Int(),
                                        rekord["fajta"].ToStrTrim(),
                                        rekord["fajtamásik"].ToStrTrim(),
                                        rekord["igazság"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Jármű_Takarítás_Mátrix Adat)
        {
            string szöveg = "INSERT INTO mátrix (id, fajta, fajtamásik, igazság ) VALUES (";
            szöveg += $"{Adat.Id},";
            szöveg += $"'{Adat.Fajta}', ";
            szöveg += $"'{Adat.Fajtamásik}', ";
            szöveg += $"{Adat.Igazság})";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
        /// <summary>
        /// fajta, fajtamásik
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Jármű_Takarítás_Mátrix Adat)
        {
            try
            {
                string szöveg = "UPDATE mátrix  SET ";
                szöveg += $" igazság={Adat.Igazság} ";
                szöveg += $" WHERE fajta='{Adat.Fajta}' AND fajtamásik='{Adat.Fajtamásik}'";
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
    }

    public class Kezelő_Jármű_Takarítás_Teljesítés
    {
        public List<Adat_Jármű_Takarítás_Teljesítés> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Teljesítés> Adatok = new List<Adat_Jármű_Takarítás_Teljesítés>();
            Adat_Jármű_Takarítás_Teljesítés Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Teljesítés(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["takarítási_fajta"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["megfelelt1"].ToÉrt_Int(),
                                        rekord["státus"].ToÉrt_Int(),
                                        rekord["megfelelt2"].ToÉrt_Int(),
                                        rekord["pótdátum"].ToÉrt_Bool(),
                                        rekord["napszak"].ToÉrt_Int(),
                                        rekord["mérték"].ToÉrt_Double(),
                                        rekord["típus"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }


    public class Kezelő_Jármű_Takarítás_J1
    {
        public List<Adat_Jármű_Takarítás_J1> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_J1> Adatok = new List<Adat_Jármű_Takarítás_J1>();
            Adat_Jármű_Takarítás_J1 Adat;

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
                                Adat = new Adat_Jármű_Takarítás_J1(
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["j1megfelelő"].ToÉrt_Int(),
                                        rekord["j1nemmegfelelő"].ToÉrt_Int(),
                                        rekord["napszak"].ToÉrt_Int(),
                                        rekord["típus"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }


    public class Kezelő_Jármű_Takarítás_Ütemező
    {
        public List<Adat_Jármű_Takarítás_Ütemező> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Ütemező> Adatok = new List<Adat_Jármű_Takarítás_Ütemező>();
            Adat_Jármű_Takarítás_Ütemező Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Ütemező(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Kezdő_dátum"].ToÉrt_DaTeTime(),
                                        rekord["növekmény"].ToÉrt_Int(),
                                        rekord["Mérték"].ToStrTrim(),
                                        rekord["Takarítási_fajta"].ToStrTrim(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Int()
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

    public class Kezelő_Jármű_Takarítás_Létszám
    {
        public List<Adat_Jármű_Takarítás_Létszám> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Létszám> Adatok = new List<Adat_Jármű_Takarítás_Létszám>();
            Adat_Jármű_Takarítás_Létszám Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Létszám(
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["előírt"].ToÉrt_Int(),
                                        rekord["megjelent"].ToÉrt_Int(),
                                        rekord["napszak"].ToÉrt_Int(),
                                        rekord["ruhátlan"].ToÉrt_Int());
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
