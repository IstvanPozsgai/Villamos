using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kerék_Eszterga_Igény
    {
        public List<Adat_Kerék_Eszterga_Igény> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Eszterga_Igény> Adatok = new List<Adat_Kerék_Eszterga_Igény>();
            Adat_Kerék_Eszterga_Igény Adat;

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
                                Adat = new Adat_Kerék_Eszterga_Igény(
                                        rekord["Pályaszám"].ToStrTrim(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["Rögzítés_dátum"].ToÉrt_DaTeTime(),
                                        rekord["Igényelte"].ToStrTrim(),
                                        rekord["Tengelyszám"].ToÉrt_Int(),
                                        rekord["Szerelvény"].ToÉrt_Int(),
                                        rekord["prioritás"].ToÉrt_Int(),
                                        rekord["Ütemezés_dátum"].ToÉrt_DaTeTime(),
                                        rekord["státus"].ToÉrt_Int(),
                                        rekord["telephely"].ToStrTrim(),
                                        rekord["típus"].ToStrTrim(),
                                        rekord["Norma"].ToÉrt_Int()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kerék_Eszterga_Igény Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kerék_Eszterga_Igény Adat = null;

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

                            Adat = new Adat_Kerék_Eszterga_Igény(
                                    rekord["Pályaszám"].ToStrTrim(),
                                    rekord["Megjegyzés"].ToStrTrim(),
                                    rekord["Rögzítés_dátum"].ToÉrt_DaTeTime(),
                                    rekord["Igényelte"].ToStrTrim(),
                                    rekord["Tengelyszám"].ToÉrt_Int(),
                                    rekord["Szerelvény"].ToÉrt_Int(),
                                    rekord["prioritás"].ToÉrt_Int(),
                                    rekord["Ütemezés_dátum"].ToÉrt_DaTeTime(),
                                    rekord["státus"].ToÉrt_Int(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["Norma"].ToÉrt_Int()
                                    );
                        }
                    }
                }
            }
            return Adat;
        }
    }


    public class Kezelő_Kerék_Eszterga_Esztergályos
    {
        public List<Adat_Kerék_Eszterga_Esztergályos> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Eszterga_Esztergályos> Adatok = new List<Adat_Kerék_Eszterga_Esztergályos>();
            Adat_Kerék_Eszterga_Esztergályos Adat;

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
                                Adat = new Adat_Kerék_Eszterga_Esztergályos(
                                        rekord["Dolgozószám"].ToStrTrim(),
                                        rekord["Dolgozónév"].ToStrTrim(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Státus"].ToÉrt_Int()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public Adat_Kerék_Eszterga_Esztergályos Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kerék_Eszterga_Esztergályos Adat = null;

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

                            Adat = new Adat_Kerék_Eszterga_Esztergályos(
                                        rekord["Dolgozószám"].ToStrTrim(),
                                        rekord["Dolgozónév"].ToStrTrim(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Státus"].ToÉrt_Int()
                                    );
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Kerék_Eszterga_Naptár
    {
        public List<Adat_Kerék_Eszterga_Naptár> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Eszterga_Naptár> Adatok = new List<Adat_Kerék_Eszterga_Naptár>();
            Adat_Kerék_Eszterga_Naptár Adat;

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
                                Adat = new Adat_Kerék_Eszterga_Naptár(
                                        rekord["Idő"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidő"].ToÉrt_Bool(),
                                        rekord["Foglalt"].ToÉrt_Bool(),
                                        rekord["Pályaszám"].ToStrTrim(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["betűszín"].ToÉrt_Long(),
                                        rekord["háttérszín"].ToÉrt_Long(),
                                        rekord["Marad"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<DateTime> Lista_Adatok_Idő(string hely, string jelszó, string szöveg)
        {
            List<DateTime> Adatok = new List<DateTime>();
            DateTime Adat;

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
                                Adat = rekord["Idő"].ToÉrt_DaTeTime();
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kerék_Eszterga_Naptár Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kerék_Eszterga_Naptár Adat = null;

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
                            Adat = new Adat_Kerék_Eszterga_Naptár(
                                          rekord["Idő"].ToÉrt_DaTeTime(),
                                          rekord["Munkaidő"].ToÉrt_Bool(),
                                          rekord["Foglalt"].ToÉrt_Bool(),
                                          rekord["Pályaszám"].ToStrTrim(),
                                          rekord["Megjegyzés"].ToStrTrim(),
                                          rekord["betűszín"].ToÉrt_Long(),
                                          rekord["háttérszín"].ToÉrt_Long(),
                                          rekord["Marad"].ToÉrt_Bool()
                                          );
                        }
                    }
                }
            }
            return Adat;
        }

        public void Adat_RögzítésIdő(string hely, string jelszó, Adat_Kerék_Eszterga_Naptár Adat)
        {
            string szöveg = $"UPDATE naptár SET pályaszám='_', foglalt=false, Megjegyzés='', ";
            szöveg += $" betűszín=0, háttérszín=12632256, marad=false ";
            szöveg += $"WHERE idő=#{Adat.Idő.ToString("MM-dd-yyyy H:m:s")}#";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Adat_Rögzítés(string hely, string jelszó, Adat_Kerék_Eszterga_Naptár Adat)
        {
            string szöveg = $"UPDATE naptár SET pályaszám='{Adat.Pályaszám.Trim()}', foglalt=true, Megjegyzés='{Adat.Megjegyzés.Trim()}', ";
            szöveg += $" betűszín={Adat.BetűSzín}, háttérszín={Adat.HáttérSzín}, marad={Adat.Marad} ";
            szöveg += $"WHERE idő=#{Adat.Idő.ToString("MM-dd-yyyy HH:mm")}#";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public List<string> Lista_MindenbőlEgy(string hely, string jelszó, string szöveg)
        {
            List<string> Adatok = new List<string>();
            string Adat;

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
                                Adat = rekord["Pályaszám"].ToStrTrim();
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }


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

    public class Kezelő_Kerék_Eszterga_Terjesztés
    {
        public List<Adat_Kerék_Eszterga_Terjesztés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Eszterga_Terjesztés> Adatok = new List<Adat_Kerék_Eszterga_Terjesztés>();
            Adat_Kerék_Eszterga_Terjesztés Adat;

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
                                Adat = new Adat_Kerék_Eszterga_Terjesztés(
                                        rekord["Név"].ToStrTrim(),
                                        rekord["Email"].ToStrTrim(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Változat"].ToÉrt_Int()
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

    public class Kezelő_Baross_Mérési_Adatok
    {
        public List<Adat_Baross_Mérési_Adatok> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Baross_Mérési_Adatok> Adatok = new List<Adat_Baross_Mérési_Adatok>();
            Adat_Baross_Mérési_Adatok Adat;

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
                                Adat = new Adat_Baross_Mérési_Adatok(
                                        rekord["Dátum_1"].ToÉrt_DaTeTime(),
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Tulajdonos"].ToStrTrim(),
                                        rekord["kezelő"].ToStrTrim(),
                                        rekord["Profil"].ToStrTrim(),
                                        rekord["Profil_szám"].ToÉrt_Long(),
                                        rekord["Kerékpár_szám"].ToStrTrim(),
                                        rekord["Adat_1"].ToStrTrim(),
                                        rekord["Adat_2"].ToStrTrim(),
                                        rekord["Adat_3"].ToStrTrim(),
                                        rekord["Típus_Eszt"].ToStrTrim(),
                                        rekord["KMU"].ToÉrt_Long(),
                                        rekord["Pozíció_Eszt"].ToÉrt_Int(),
                                        rekord["Tengely_Aznosító"].ToStrTrim(),
                                        rekord["Adat_4"].ToStrTrim(),
                                        rekord["Dátum_2"].ToÉrt_DaTeTime(),
                                        rekord["Táv_Belső_Futó_K"].ToÉrt_Double(),
                                        rekord["Táv_Nyom_K"].ToÉrt_Double(),
                                        rekord["Delta_K"].ToÉrt_Double(),
                                        rekord["B_Átmérő_K"].ToÉrt_Double(),
                                        rekord["J_Átmérő_K"].ToÉrt_Double(),
                                        rekord["B_Axiális_K"].ToÉrt_Double(),
                                        rekord["J_Axiális_K"].ToÉrt_Double(),
                                        rekord["B_Radiális_K"].ToÉrt_Double(),
                                        rekord["J_Radiális_K"].ToÉrt_Double(),
                                        rekord["B_Nyom_Mag_K"].ToÉrt_Double(),
                                        rekord["J_Nyom_Mag_K"].ToÉrt_Double(),
                                        rekord["B_Nyom_Vast_K"].ToÉrt_Double(),
                                        rekord["J_nyom_Vast_K"].ToÉrt_Double(),
                                        rekord["B_Nyom_Vast_B_K"].ToÉrt_Double(),
                                        rekord["J_nyom_Vast_B_K"].ToÉrt_Double(),
                                        rekord["B_QR_K"].ToÉrt_Double(),
                                        rekord["J_QR_K"].ToÉrt_Double(),
                                        rekord["B_Profilhossz_K"].ToÉrt_Double(),
                                        rekord["J_Profilhossz_K"].ToÉrt_Double(),
                                        rekord["Dátum_3"].ToÉrt_DaTeTime(),
                                        rekord["Táv_Belső_Futó_Ú"].ToÉrt_Double(),
                                        rekord["Táv_Nyom_Ú"].ToÉrt_Double(),
                                        rekord["Delta_Ú"].ToÉrt_Double(),
                                        rekord["B_Átmérő_Ú"].ToÉrt_Double(),
                                        rekord["J_Átmérő_Ú"].ToÉrt_Double(),
                                        rekord["B_Axiális_Ú"].ToÉrt_Double(),
                                        rekord["J_Axiális_Ú"].ToÉrt_Double(),
                                        rekord["B_Radiális_Ú"].ToÉrt_Double(),
                                        rekord["J_Radiális_Ú"].ToÉrt_Double(),
                                        rekord["B_Nyom_Mag_Ú"].ToÉrt_Double(),
                                        rekord["J_Nyom_Mag_Ú"].ToÉrt_Double(),
                                        rekord["B_Nyom_Vast_Ú"].ToÉrt_Double(),
                                        rekord["J_nyom_Vast_Ú"].ToÉrt_Double(),
                                        rekord["B_Nyom_Vast_B_Ú"].ToÉrt_Double(),
                                        rekord["J_nyom_Vast_B_Ú"].ToÉrt_Double(),
                                        rekord["B_QR_Ú"].ToÉrt_Double(),
                                        rekord["J_QR_Ú"].ToÉrt_Double(),
                                        rekord["B_Szög_Ú"].ToÉrt_Double(),
                                        rekord["J_Szög_Ú"].ToÉrt_Double(),
                                        rekord["B_Profilhossz_Ú"].ToÉrt_Double(),
                                        rekord["J_Profilhossz_Ú"].ToÉrt_Double(),
                                        rekord["Eszterga_Id"].ToÉrt_Long(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["Státus"].ToÉrt_Int()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Baross_Mérési_Adatok Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Baross_Mérési_Adatok Adat = null;

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
                            Adat = new Adat_Baross_Mérési_Adatok(
                                    rekord["Dátum_1"].ToÉrt_DaTeTime(),
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Tulajdonos"].ToStrTrim(),
                                    rekord["kezelő"].ToStrTrim(),
                                    rekord["Profil"].ToStrTrim(),
                                    rekord["Profil_szám"].ToÉrt_Long(),
                                    rekord["Kerékpár_szám"].ToStrTrim(),
                                    rekord["Adat_1"].ToStrTrim(),
                                    rekord["Adat_2"].ToStrTrim(),
                                    rekord["Adat_3"].ToStrTrim(),
                                    rekord["Típus_Eszt"].ToStrTrim(),
                                    rekord["KMU"].ToÉrt_Long(),
                                    rekord["Pozíció_Eszt"].ToÉrt_Int(),
                                    rekord["Tengely_Aznosító"].ToStrTrim(),
                                    rekord["Adat_4"].ToStrTrim(),
                                    rekord["Dátum_2"].ToÉrt_DaTeTime(),
                                    rekord["Táv_Belső_Futó_K"].ToÉrt_Double(),
                                    rekord["Táv_Nyom_K"].ToÉrt_Double(),
                                    rekord["Delta_K"].ToÉrt_Double(),
                                    rekord["B_Átmérő_K"].ToÉrt_Double(),
                                    rekord["J_Átmérő_K"].ToÉrt_Double(),
                                    rekord["B_Axiális_K"].ToÉrt_Double(),
                                    rekord["J_Axiális_K"].ToÉrt_Double(),
                                    rekord["B_Radiális_K"].ToÉrt_Double(),
                                    rekord["J_Radiális_K"].ToÉrt_Double(),
                                    rekord["B_Nyom_Mag_K"].ToÉrt_Double(),
                                    rekord["J_Nyom_Mag_K"].ToÉrt_Double(),
                                    rekord["B_Nyom_Vast_K"].ToÉrt_Double(),
                                    rekord["J_nyom_Vast_K"].ToÉrt_Double(),
                                    rekord["B_Nyom_Vast_B_K"].ToÉrt_Double(),
                                    rekord["J_nyom_Vast_B_K"].ToÉrt_Double(),
                                    rekord["B_QR_K"].ToÉrt_Double(),
                                    rekord["J_QR_K"].ToÉrt_Double(),
                                    rekord["B_Profilhossz_K"].ToÉrt_Double(),
                                    rekord["J_Profilhossz_K"].ToÉrt_Double(),
                                    rekord["Dátum_3"].ToÉrt_DaTeTime(),
                                    rekord["Táv_Belső_Futó_Ú"].ToÉrt_Double(),
                                    rekord["Táv_Nyom_Ú"].ToÉrt_Double(),
                                    rekord["Delta_Ú"].ToÉrt_Double(),
                                    rekord["B_Átmérő_Ú"].ToÉrt_Double(),
                                    rekord["J_Átmérő_Ú"].ToÉrt_Double(),
                                    rekord["B_Axiális_Ú"].ToÉrt_Double(),
                                    rekord["J_Axiális_Ú"].ToÉrt_Double(),
                                    rekord["B_Radiális_Ú"].ToÉrt_Double(),
                                    rekord["J_Radiális_Ú"].ToÉrt_Double(),
                                    rekord["B_Nyom_Mag_Ú"].ToÉrt_Double(),
                                    rekord["J_Nyom_Mag_Ú"].ToÉrt_Double(),
                                    rekord["B_Nyom_Vast_Ú"].ToÉrt_Double(),
                                    rekord["J_nyom_Vast_Ú"].ToÉrt_Double(),
                                    rekord["B_Nyom_Vast_B_Ú"].ToÉrt_Double(),
                                    rekord["J_nyom_Vast_B_Ú"].ToÉrt_Double(),
                                    rekord["B_QR_Ú"].ToÉrt_Double(),
                                    rekord["J_QR_Ú"].ToÉrt_Double(),
                                    rekord["B_Szög_Ú"].ToÉrt_Double(),
                                    rekord["J_Szög_Ú"].ToÉrt_Double(),
                                    rekord["B_Profilhossz_Ú"].ToÉrt_Double(),
                                    rekord["J_Profilhossz_Ú"].ToÉrt_Double(),
                                    rekord["Eszterga_Id"].ToÉrt_Long(),
                                    rekord["Megjegyzés"].ToStrTrim(),
                                    rekord["Státus"].ToÉrt_Int()
                                    );
                        }
                    }
                }
            }
            return Adat;
        }
    }
}
