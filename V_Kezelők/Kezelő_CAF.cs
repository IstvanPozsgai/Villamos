using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_CAF_alap
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
        readonly string jelszó = "CzabalayL";

        public List<Adat_CAF_alap> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM alap ORDER BY azonosító";
            List<Adat_CAF_alap> Adatok = new List<Adat_CAF_alap>();
            Adat_CAF_alap Adat;

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
                                Adat = new Adat_CAF_alap(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Ciklusnap"].ToStrTrim(),
                                        rekord["Utolsó_Nap"].ToStrTrim(),
                                        rekord["Utolsó_Nap_sorszám"].ToÉrt_Long(),
                                        rekord["Végezte_nap"].ToStrTrim(),
                                        rekord["Vizsgdátum_nap"].ToÉrt_DaTeTime(),
                                        rekord["Cikluskm"].ToStrTrim(),
                                        rekord["Utolsó_Km"].ToStrTrim(),
                                        rekord["Utolsó_Km_sorszám"].ToÉrt_Long(),
                                        rekord["Végezte_km"].ToStrTrim(),
                                        rekord["Vizsgdátum_km"].ToÉrt_DaTeTime(),
                                        rekord["Számláló"].ToÉrt_Long(),
                                        rekord["havikm"].ToÉrt_Long(),
                                        rekord["KMUkm"].ToÉrt_Long(),
                                        rekord["KMUdátum"].ToÉrt_DaTeTime(),
                                        rekord["fudátum"].ToÉrt_DaTeTime(),
                                        rekord["Teljeskm"].ToÉrt_Long(),
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Garancia"].ToÉrt_Bool(),
                                        rekord["törölt"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_CAF_alap> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_CAF_alap> Adatok = new List<Adat_CAF_alap>();
            Adat_CAF_alap Adat;

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
                                Adat = new Adat_CAF_alap(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Ciklusnap"].ToStrTrim(),
                                        rekord["Utolsó_Nap"].ToStrTrim(),
                                        rekord["Utolsó_Nap_sorszám"].ToÉrt_Long(),
                                        rekord["Végezte_nap"].ToStrTrim(),
                                        rekord["Vizsgdátum_nap"].ToÉrt_DaTeTime(),
                                        rekord["Cikluskm"].ToStrTrim(),
                                        rekord["Utolsó_Km"].ToStrTrim(),
                                        rekord["Utolsó_Km_sorszám"].ToÉrt_Long(),
                                        rekord["Végezte_km"].ToStrTrim(),
                                        rekord["Vizsgdátum_km"].ToÉrt_DaTeTime(),
                                        rekord["Számláló"].ToÉrt_Long(),
                                        rekord["havikm"].ToÉrt_Long(),
                                        rekord["KMUkm"].ToÉrt_Long(),
                                        rekord["KMUdátum"].ToÉrt_DaTeTime(),
                                        rekord["fudátum"].ToÉrt_DaTeTime(),
                                        rekord["Teljeskm"].ToÉrt_Long(),
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Garancia"].ToÉrt_Bool(),
                                        rekord["törölt"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_CAF_alap Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_CAF_alap Adat = null;

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

                            Adat = new Adat_CAF_alap(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Ciklusnap"].ToStrTrim(),
                                    rekord["Utolsó_Nap"].ToStrTrim(),
                                    rekord["Utolsó_Nap_sorszám"].ToÉrt_Long(),
                                    rekord["Végezte_nap"].ToStrTrim(),
                                    rekord["Vizsgdátum_nap"].ToÉrt_DaTeTime(),
                                    rekord["Cikluskm"].ToStrTrim(),
                                    rekord["Utolsó_Km"].ToStrTrim(),
                                    rekord["Utolsó_Km_sorszám"].ToÉrt_Long(),
                                    rekord["Végezte_km"].ToStrTrim(),
                                    rekord["Vizsgdátum_km"].ToÉrt_DaTeTime(),
                                    rekord["Számláló"].ToÉrt_Long(),
                                    rekord["havikm"].ToÉrt_Long(),
                                    rekord["KMUkm"].ToÉrt_Long(),
                                    rekord["KMUdátum"].ToÉrt_DaTeTime(),
                                    rekord["fudátum"].ToÉrt_DaTeTime(),
                                    rekord["Teljeskm"].ToÉrt_Long(),
                                    rekord["Típus"].ToStrTrim(),
                                    rekord["Garancia"].ToÉrt_Bool(),
                                    rekord["törölt"].ToÉrt_Bool()
                                    );
                        }
                    }
                }
            }
            return Adat;
        }


    }

    public class Kezelő_CAF_Adatok
    {
        public List<Adat_CAF_Adatok> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_CAF_Adatok> Adatok = new List<Adat_CAF_Adatok>();
            Adat_CAF_Adatok Adat;

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
                                //DateTime dátum = DateTime.TryParse(rekord["dátum"].ToString(), out dátum) ? dátum : new DateTime(1900, 1, 1);
                                //DateTime Dátum_program = DateTime.TryParse(rekord["Dátum_program"].ToString(), out Dátum_program) ? Dátum_program : new DateTime(1900, 1, 1);
                                Adat = new Adat_CAF_Adatok(
                                        rekord["id"].ToÉrt_Double(),
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Vizsgálat"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["Dátum_program"].ToÉrt_DaTeTime(),
                                        rekord["Számláló"].ToÉrt_Long(),
                                        rekord["Státus"].ToÉrt_Int(),
                                        rekord["KM_Sorszám"].ToÉrt_Int(),
                                        rekord["IDŐ_Sorszám"].ToÉrt_Int(),
                                        rekord["IDŐvKM"].ToÉrt_Int(),
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

        public Adat_CAF_Adatok Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_CAF_Adatok Adat = null;

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

                            DateTime dátum = DateTime.TryParse(rekord["dátum"].ToString(), out dátum) ? dátum : new DateTime(1900, 1, 1);
                            DateTime Dátum_program = DateTime.TryParse(rekord["Dátum_program"].ToString(), out Dátum_program) ? Dátum_program : new DateTime(1900, 1, 1);
                            Adat = new Adat_CAF_Adatok(
                                    rekord["id"].ToÉrt_Double(),
                                    rekord["azonosító"].ToStrTrim(),
                                    rekord["Vizsgálat"].ToStrTrim(),
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["Dátum_program"].ToÉrt_DaTeTime(),
                                    rekord["Számláló"].ToÉrt_Long(),
                                    rekord["Státus"].ToÉrt_Int(),
                                    rekord["KM_Sorszám"].ToÉrt_Int(),
                                    rekord["IDŐ_Sorszám"].ToÉrt_Int(),
                                    rekord["IDŐvKM"].ToÉrt_Int(),
                                    rekord["Megjegyzés"].ToStrTrim()
                                    );
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_CAF_Telephely
    {
        public List<Adat_CAF_Telephely> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_CAF_Telephely> Adatok = new List<Adat_CAF_Telephely>();
            Adat_CAF_Telephely Adat;

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
                                Adat = new Adat_CAF_Telephely(
                                        rekord["sorrend"].ToÉrt_Long(),
                                        rekord["telephely"].ToStrTrim()
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


    public class Kezelő_CAF_Ütemezés
    {
        public List<Adat_CAF_Ütemezés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_CAF_Ütemezés> Adatok = new List<Adat_CAF_Ütemezés>();
            Adat_CAF_Ütemezés Adat;

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
                                Adat = new Adat_CAF_Ütemezés(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Ciklusrend"].ToStrTrim(),
                                        rekord["Elkészült"].ToÉrt_Bool(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Long(),
                                        rekord["velkészülés"].ToÉrt_DaTeTime(),
                                        rekord["vesedékesség"].ToÉrt_DaTeTime(),
                                        rekord["vizsgfoka"].ToStrTrim(),
                                        rekord["vsorszám"].ToÉrt_Long(),
                                        rekord["vütemezés"].ToÉrt_DaTeTime(),
                                        rekord["Vvégezte"].ToStrTrim()
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


    public class Kezelő_CAF_Alapnapló
    {
        public List<Adat_CAF_Alapnapló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_CAF_Alapnapló> Adatok = new List<Adat_CAF_Alapnapló>();
            Adat_CAF_Alapnapló Adat;

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
                                Adat = new Adat_CAF_Alapnapló(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Ciklusrend"].ToStrTrim(),
                                        rekord["kötöttstart"].ToÉrt_Bool(),
                                        rekord["megállítás"].ToÉrt_Bool(),
                                        rekord["Oka"].ToStrTrim(),
                                        rekord["rögzítésiidő"].ToÉrt_DaTeTime(),
                                        rekord["rögzítő"].ToStrTrim(),
                                        rekord["start"].ToÉrt_DaTeTime(),
                                        rekord["vizsgdátum"].ToÉrt_DaTeTime(),
                                        rekord["vizsgnév"].ToStrTrim(),
                                        rekord["vizsgsorszám"].ToÉrt_Long()
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


    public class Kezelő_CAF_Ütemezésnapló
    {
        public List<Adat_CAF_Ütemezésnapló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_CAF_Ütemezésnapló> Adatok = new List<Adat_CAF_Ütemezésnapló>();
            Adat_CAF_Ütemezésnapló Adat;

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
                                Adat = new Adat_CAF_Ütemezésnapló(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Ciklusrend"].ToStrTrim(),
                                        rekord["Elkészült"].ToÉrt_Bool(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["rögzítésideje"].ToÉrt_DaTeTime(),
                                        rekord["rögzítő"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Long(),
                                        rekord["velkészülés"].ToÉrt_DaTeTime(),
                                        rekord["vesedékesség"].ToÉrt_DaTeTime(),
                                        rekord["vizsgfoka"].ToStrTrim(),
                                        rekord["vsorszám"].ToÉrt_Long(),
                                        rekord["vütemezés"].ToÉrt_DaTeTime(),
                                        rekord["Vvégezte"].ToStrTrim()
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

