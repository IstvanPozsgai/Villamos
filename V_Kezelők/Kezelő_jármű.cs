using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;



namespace Villamos.Kezelők
{
    public class Kezelő_Jármű
    {
        readonly string jelszó = "pozsgaii";
        public List<Adat_Jármű> Lista_Jármű_állomány(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű> Adatok = new List<Adat_Jármű>();
            try
            {
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
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["típus"].ToStrTrim()
                                        );
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Lista_Jármű_állomány\n" + szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
            return Adatok;
        }

        public List<string> List_Jármű_típusok(string hely, string jelszó, string szöveg)
        {
            List<string> list = new List<string>();
            string elem;
            try
            {

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
                                    elem = rekord["valóstípus"].ToStrTrim();
                                    list.Add(elem);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "List_Jármű_típusok\n" + szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
            return list;
        }

        public List<string> List_Jármű_Telephely(string hely, string jelszó, string szöveg)
        {
            List<string> list = new List<string>();
            string elem;
            try
            {
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
                                    elem = rekord["üzem"].ToStrTrim();
                                    list.Add(elem);
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "List_Jármű_Telephely\n" + szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
            return list;
        }


        public void Rögzítés(string hely, string jelszó, Adat_Jármű Adat)
        {
            try
            {
                string szöveg = "INSERT INTO Állománytábla (azonosító, hibák, státus, típus, üzem, törölt, hibáksorszáma, szerelvény, szerelvénykocsik, miótaáll, valóstípus, valóstípus2, üzembehelyezés) VALUES (";
                szöveg += $"'{Adat.Azonosító.Trim()}', 0, 0, 'Nincs', 'Közös', false, 0, false, 0, '1900.01.01', ";
                szöveg += $"'{Adat.Valóstípus.Trim()}', ";
                szöveg += $"'{Adat.Valóstípus2.Trim()}', '1900.01.01')";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Rögzítés\n", ex.StackTrace, ex.Source, ex.HResult);
            }
        }


        public void Áthelyezés_új(string hely, string jelszó, Adat_Jármű Adat)
        {
            try
            {
                string szöveg = "INSERT INTO Állománytábla (azonosító, hibák, státus, típus, üzem, törölt, hibáksorszáma, szerelvény, szerelvénykocsik, miótaáll, valóstípus, valóstípus2) VALUES (";
                szöveg += $"'{Adat.Azonosító}', {Adat.Hibák}, {Adat.Státus}, '{Adat.Típus.Trim()}', '{Adat.Üzem.Trim()}'," +
                    $" {Adat.Törölt}, {Adat.Hibáksorszáma}, {Adat.Szerelvény}, {Adat.Szerelvénykocsik}, '{Adat.Miótaáll}', ";
                szöveg += $"'{Adat.Valóstípus.Trim()}', ";
                szöveg += $"'{Adat.Valóstípus2.Trim()} ')";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Rögzítés\n", ex.StackTrace, ex.Source, ex.HResult);
            }
        }


        public void Módosítás(string hely, string jelszó, Adat_Jármű Adat)
        {
            string szöveg = "";
            try
            {
                szöveg = "UPDATE Állománytábla SET ";
                szöveg += $"hibák={Adat.Hibák}, ";
                szöveg += $"státus={Adat.Státus}, ";
                szöveg += $"törölt={Adat.Törölt}, ";
                szöveg += $"hibáksorszáma={Adat.Hibáksorszáma}, ";
                szöveg += $"szerelvény={Adat.Szerelvény}, ";
                szöveg += $"valóstípus='{Adat.Valóstípus.Trim()}', ";
                szöveg += $"valóstípus2='{Adat.Valóstípus2.Trim()}', ";
                szöveg += $"szerelvénykocsik={Adat.Szerelvénykocsik}, ";
                szöveg += $"miótaáll='{Adat.Miótaáll}', ";
                szöveg += $"típus='{Adat.Típus.Trim()}', ";
                szöveg += $"üzem='{Adat.Üzem.Trim()}' ";
                szöveg += $" WHERE [azonosító] ='{Adat.Azonosító.Trim()}'";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Módosítás\n" + szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
        }


        public Adat_Jármű Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Jármű Adat = null;
            try
            {
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
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["hibák"].ToÉrt_Long(),
                                        rekord["státus"].ToÉrt_Long(),
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Üzem"].ToStrTrim(),
                                        rekord["törölt"].ToÉrt_Bool(),
                                        rekord["hibáksorszáma"].ToÉrt_Long(),
                                        rekord["szerelvény"].ToÉrt_Bool(),
                                        rekord["szerelvénykocsik"].ToÉrt_Long(),
                                        rekord["miótaáll"].ToÉrt_DaTeTime(),
                                        rekord["valóstípus"].ToStrTrim(),
                                        rekord["valóstípus2"].ToStrTrim()
                                        );
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Egy_Adat\n" + szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
            return Adat;
        }


        public Adat_Jármű Egy_Adat_fő(string hely, string jelszó, string szöveg)
        {
            Adat_Jármű Adat = null;
            try
            {
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
                                         rekord["Azonosító"].ToStrTrim(),
                                         rekord["hibák"].ToÉrt_Long(),
                                         rekord["státus"].ToÉrt_Long(),
                                         rekord["Típus"].ToStrTrim(),
                                         rekord["Üzem"].ToStrTrim(),
                                         rekord["törölt"].ToÉrt_Bool(),
                                         rekord["hibáksorszáma"].ToÉrt_Long(),
                                         rekord["szerelvény"].ToÉrt_Bool(),
                                         rekord["szerelvénykocsik"].ToÉrt_Long(),
                                         rekord["miótaáll"].ToÉrt_DaTeTime(),
                                         rekord["valóstípus"].ToStrTrim(),
                                         rekord["valóstípus2"].ToStrTrim(),
                                         rekord["üzembehelyezés"].ToÉrt_DaTeTime()
                                         );
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Egy_Adat_fő\n" + szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
            return Adat;
        }


        public List<Adat_Jármű> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű> Adatok = new List<Adat_Jármű>();
            Adat_Jármű Adat;
            try
            {
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
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["hibák"].ToÉrt_Long(),
                                        rekord["státus"].ToÉrt_Long(),
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Üzem"].ToStrTrim(),
                                        rekord["törölt"].ToÉrt_Bool(),
                                        rekord["hibáksorszáma"].ToÉrt_Long(),
                                        rekord["szerelvény"].ToÉrt_Bool(),
                                        rekord["szerelvénykocsik"].ToÉrt_Long(),
                                        rekord["miótaáll"].ToÉrt_DaTeTime(),
                                        rekord["valóstípus"].ToStrTrim(),
                                        rekord["valóstípus2"].ToStrTrim()
                                        );
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Lista_Adatok\n" + szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
            return Adatok;
        }

        public List<Adat_Jármű> Lista_Adatok(string telephely)
        {
            string szöveg = "SELECT * FROM állománytábla order by  azonosító";
            string hely = $@"{Application.StartupPath}\{telephely}\Adatok\villamos\villamos.mdb".Ellenőrzés();
            if (telephely == "Főmérnökség") hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\villamos.mdb".Ellenőrzés();

            List<Adat_Jármű> Adatok = new List<Adat_Jármű>();
            Adat_Jármű Adat;
            try
            {
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
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["hibák"].ToÉrt_Long(),
                                        rekord["státus"].ToÉrt_Long(),
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Üzem"].ToStrTrim(),
                                        rekord["törölt"].ToÉrt_Bool(),
                                        rekord["hibáksorszáma"].ToÉrt_Long(),
                                        rekord["szerelvény"].ToÉrt_Bool(),
                                        rekord["szerelvénykocsik"].ToÉrt_Long(),
                                        rekord["miótaáll"].ToÉrt_DaTeTime(),
                                        rekord["valóstípus"].ToStrTrim(),
                                        rekord["valóstípus2"].ToStrTrim()
                                        );
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Lista_Adatok\n" + szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
            return Adatok;
        }

        public List<string> Lista_Pályaszámok(string hely, string jelszó, string szöveg)
        {
            List<string> Adatok = new List<string>();
            string Adat;
            try
            {
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
                                    Adat = rekord["Azonosító"].ToStrTrim();
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Lista_Pályaszámok\n" + szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
            return Adatok;
        }

        public List<string> Lista_Pályaszámok(string hely, int Melyiknap, string Típus = "ICS")
        {
            string szöveg = $"SELECT * FROM állománytábla where E2={Melyiknap}  ORDER BY  azonosító";
            if (Típus == "T5C5") szöveg = $"SELECT * FROM állománytábla where haromnapos={Melyiknap}  ORDER BY  azonosító";

            List<string> Adatok = new List<string>();
            string Adat;
            try
            {
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
                                    Adat = rekord["Azonosító"].ToStrTrim();
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Lista_Pályaszámok\n" + szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
            return Adatok;
        }

        public List<string> Lista_Pályaszámok(string hely, string Típus)
        {
            string szöveg = $"SELECT * FROM állománytábla WHERE  típus='{Típus}' ORDER BY azonosító";
            List<string> Adatok = new List<string>();
            string Adat;
            try
            {
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
                                    Adat = rekord["Azonosító"].ToStrTrim();
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Lista_Pályaszámok\n" + szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
            return Adatok;
        }
    }

    public class Kezelő_Jármű_Vendég
    {
        public Dictionary<string, string> Szótár(string hely, string jelszó, string szöveg)
        {
            Dictionary<string, string> SzAdat = new Dictionary<string, string>();

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
                                SzAdat.Add(
                                      rekord["Azonosító"].ToStrTrim(),
                                      rekord["kiadótelephely"].ToStrTrim()
                                      );
                            }
                        }
                    }
                }
            }
            return SzAdat;
        }


        public void Rögzítés_Vendég(string hely, string jelszó, Adat_Jármű_Vendég Adat)
        {

            string szöveg = $"SELECT * FROM vendégtábla WHERE azonosító='{Adat.Azonosító.Trim()}'";
            Adat_Jármű_Vendég EgyAdat = Egy_Adat(hely, jelszó, szöveg);
            // rögzítjük az adatot

            if (EgyAdat != null)
            {
                // Ha már létezik, akkor módosítjuk
                szöveg = "UPDATE vendégtábla  SET ";
                szöveg += $"típus='{Adat.Típus.Trim()}', "; // típus
                szöveg += $"BázisTelephely='{Adat.BázisTelephely.Trim()}', "; // BázisTelephely
                szöveg += $"KiadóTelephely='{Adat.KiadóTelephely.Trim()}' "; // KiadóTelephely
                szöveg += $" WHERE azonosító='{Adat.Azonosító.Trim()}'";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                throw new HibásBevittAdat("Az adat módosítása megtörtént.");
            }
            else
            {
                // ha nem létezik 
                szöveg = "INSERT INTO vendégtábla  (  azonosító, típus, BázisTelephely, KiadóTelephely ) VALUES (";
                szöveg += $"'{Adat.Azonosító.Trim()}', "; // azonosító
                szöveg += $"'{Adat.Típus.Trim()}', "; // típus
                szöveg += $"'{Adat.BázisTelephely.Trim()}', "; // BázisTelephely
                szöveg += $"'{Adat.KiadóTelephely.Trim()}')";

                MyA.ABMódosítás(hely, jelszó, szöveg);
                throw new HibásBevittAdat("Az adat rögzítése megtörtént.");
            }
        }

        public void Törlés_Vendég(string hely, string jelszó, Adat_Jármű_Vendég Adat)
        {

            string szöveg = $"SELECT * FROM vendégtábla WHERE azonosító='{Adat.Azonosító.Trim()}'";
            Adat_Jármű_Vendég EgyAdat = Egy_Adat(hely, jelszó, szöveg);

            if (EgyAdat != null)
            {
                szöveg = $"DELETE FROM vendégtábla WHERE azonosító='{Adat.Azonosító.Trim()}'";
                MyA.ABtörlés(hely, jelszó, szöveg);
                throw new HibásBevittAdat("Az adat törlése megtörtént.");
            }

        }

        public List<Adat_Jármű_Vendég> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Vendég> Adatok = new List<Adat_Jármű_Vendég>();
            Adat_Jármű_Vendég Adat;
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
                                Adat = new Adat_Jármű_Vendég(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Típus"].ToStrTrim(),
                                    rekord["Bázistelephely"].ToStrTrim(),
                                    rekord["Kiadótelephely"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Jármű_Vendég Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Jármű_Vendég Adat = null;
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

                            Adat = new Adat_Jármű_Vendég(
                                rekord["Azonosító"].ToStrTrim(),
                                rekord["Típus"].ToStrTrim(),
                                rekord["Bázistelephely"].ToStrTrim(),
                                rekord["Kiadótelephely"].ToStrTrim()
                                );
                        }
                    }
                }
            }
            return Adat;
        }
    }



    public class Kezelő_Jármű_Javításiátfutástábla
    {
        public List<Adat_Jármű_Javításiátfutástábla> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Javításiátfutástábla> Adatok = new List<Adat_Jármű_Javításiátfutástábla>();
            Adat_Jármű_Javításiátfutástábla Adat;
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
                                Adat = new Adat_Jármű_Javításiátfutástábla(
                                        rekord["kezdődátum"].ToÉrt_DaTeTime(),
                                        rekord["végdátum"].ToÉrt_DaTeTime(),
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["hibaleírása"].ToStrTrim()
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
