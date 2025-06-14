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
