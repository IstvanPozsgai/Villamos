using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;



namespace Villamos.Kezelők
{
    public class Kezelő_Jármű
    {
        readonly string jelszó = "pozsgaii";
        string hely;

        private void FájlBeállítás(string Telephely)
        {

            if (Telephely == "Főmérnökség" || Telephely.Contains("törzs") || Telephely.Contains("osztály"))
            {
                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\villamos.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.KocsikTípusaTelep(hely.KönyvSzerk());
            }
            else
            {
                hely = $@"{Application.StartupPath}\{Telephely}\Adatok\villamos\villamos.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.KocsikTípusa(hely.KönyvSzerk());
            }
        }

        public List<Adat_Jármű> Lista_Adatok(string Telephely)
        {
            string szöveg = "SELECT * FROM állománytábla order by azonosító";
            FájlBeállítás(Telephely);

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

        public void Rögzítés(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "INSERT INTO Állománytábla (azonosító, hibák, státus, típus, üzem, törölt, hibáksorszáma, szerelvény, szerelvénykocsik, miótaáll, valóstípus, valóstípus2, üzembehelyezés) VALUES (";
                szöveg += $"'{Adat.Azonosító.Trim()}', 0, 0, 'Nincs', 'Közös', false, 0, false, 0, '1900.01.01', ";
                szöveg += $"'{Adat.Valóstípus.Trim()}', ";
                szöveg += $"'{Adat.Valóstípus2.Trim()}', '1900.01.01')";
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

        public void Módosítás_Telephely(string Telephely, List<string> Üzemek, List<string> Azonosítók)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < Üzemek.Count; i++)
                {
                    string szöveg = "UPDATE Állománytábla SET ";
                    szöveg += $"üzem='{Üzemek[i].Trim()}' ";
                    szöveg += $" WHERE [azonosító] ='{Azonosítók[i]}'";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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

        public void Módosítás_Hiba_Státus(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE állománytábla SET ";
                szöveg += $" hibák={Adat.Hibák}, ";
                szöveg += $" státus={Adat.Státus} ";
                szöveg += $" WHERE  [azonosító]='{Adat.Azonosító}'";
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

        public void Módosítás_Státus_Dátum(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE állománytábla SET ";
                szöveg += $" hibák={Adat.Hibák}, ";
                szöveg += $" miótaáll='{Adat.Miótaáll}' ";
                szöveg += $" WHERE  [azonosító]='{Adat.Azonosító}'";
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

        public void Módosítás_Státus_Hiba_Dátum(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE állománytábla SET ";
                szöveg += $" hibák={Adat.Hibák}, ";
                szöveg += $" státus={Adat.Státus}, ";
                szöveg += $" miótaáll='{Adat.Miótaáll}' ";
                szöveg += $" WHERE  [azonosító]='{Adat.Azonosító}'";
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

        public void Módosítás_Hiba(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE állománytábla SET ";
                szöveg += $" hibák={Adat.Hibák} ";
                szöveg += $" WHERE  [azonosító]='{Adat.Azonosító}'";
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

        public void Módosítás_ÜzemBe(string Telephely, List<Adat_Jármű> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Jármű Adat in Adatok)
                {
                    string szöveg = "UPDATE állománytábla SET ";
                    szöveg += $" üzembehelyezés='{Adat.Üzembehelyezés:yyyy.MM.dd}' ";
                    szöveg += $"where [azonosító] ='{Adat.Azonosító}'";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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

        public void Módosítás_ÜzemÁtvétel(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE Állománytábla SET ";
                szöveg += $" üzem='{Adat.Üzem}', típus='{Adat.Típus}' WHERE azonosító='{Adat.Azonosító}'";
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

        public void Módosítás_Típus(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE állománytábla SET ";
                szöveg += $"valóstípus='{Adat.Valóstípus}', ";
                szöveg += $"valóstípus2='{Adat.Valóstípus2}', ";
                szöveg += $"üzembehelyezés='{Adat.Üzembehelyezés:yyyy.MM.dd}' ";
                szöveg += $"where [azonosító] ='{Adat.Azonosító}'";
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

        public void Törlés(string Telephely, string Azonosító)
        {
            try
            {
                List<Adat_Jármű> Adatok = Lista_Adatok(Telephely);
                Adat_Jármű Elem = (from a in Adatok
                                   where a.Azonosító == Azonosító
                                   select a).FirstOrDefault();

                string szöveg;
                if (Elem != null)
                {
                    if (Elem.Törölt)
                        szöveg = $"UPDATE Állománytábla SET törölt=false WHERE [azonosító]='{Azonosító}'";
                    else
                        szöveg = $"UPDATE Állománytábla SET törölt=true WHERE [azonosító]='{Azonosító}'";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }

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

        public void Módosítás_Szerelvény(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE  állománytábla SET ";
                szöveg += $" szerelvény={Adat.Szerelvény}, szerelvénykocsik={Adat.Szerelvénykocsik}";
                szöveg += $" WHERE azonosító='{Adat.Azonosító}'";
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

        public void Módosítás_Szerelvény(string Telephely, List<Adat_Jármű> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> szövegGy = new List<string>();
                foreach (Adat_Jármű Adat in Adatok)
                {
                    string szöveg = "UPDATE  állománytábla SET ";
                    szöveg += $" szerelvény={Adat.Szerelvény}, szerelvénykocsik={Adat.Szerelvénykocsik}";
                    szöveg += $" WHERE azonosító='{Adat.Azonosító}'";
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

        public void Módosítás(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE Állománytábla SET ";
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


        //Elkopó
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
