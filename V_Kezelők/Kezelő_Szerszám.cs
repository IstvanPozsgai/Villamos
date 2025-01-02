using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Szerszám_könvyvelés
    {
        public List<Adat_Szerszám_Könyvelés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Szerszám_Könyvelés> Adatok = new List<Adat_Szerszám_Könyvelés>();
            Adat_Szerszám_Könyvelés Adat;

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
                                Adat = new Adat_Szerszám_Könyvelés(
                                       rekord["mennyiség"].ToÉrt_Int(),
                                       rekord["dátum"].ToÉrt_DaTeTime(),
                                       rekord["Azonosító"].ToStrTrim(),
                                       rekord["szerszámkönyvszám"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Szerszám_Könyvelés Adat)
        {
            try
            {
                string szöveg = "INSERT INTO könyvelés  (azonosító, Szerszámkönyvszám, mennyiség, dátum ) VALUES (";
                szöveg += $"'{Adat.AzonosítóMás}', ";
                szöveg += $"'{Adat.SzerszámkönyvszámMás}', ";
                szöveg += $"{Adat.Mennyiség}, ";
                szöveg += $"'{DateTime.Now}') ";
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

        public void Módosítás(string hely, string jelszó, Adat_Szerszám_Könyvelés Adat)
        {
            try
            {
                string szöveg = "UPDATE könyvelés  SET ";
                szöveg += $" mennyiség={Adat.Mennyiség}, ";
                szöveg += $" dátum='{DateTime.Now}' ";
                szöveg += $" WHERE Szerszámkönyvszám='{Adat.SzerszámkönyvszámMás}' And ";
                szöveg += $" azonosító='{Adat.AzonosítóMás}'";
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

        public void Törlés(string hely, string jelszó, Adat_Szerszám_Könyvelés Adat)
        {

            try
            {
                string szöveg = "DELETE FROM  könyvelés ";
                szöveg += $" WHERE Szerszámkönyvszám='{Adat.SzerszámkönyvszámMás}' And ";
                szöveg += $" azonosító='{Adat.AzonosítóMás}'";
                MyA.ABtörlés(hely, jelszó, szöveg);
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

    public class Kezelő_Szerszám_Cikk
    {
        public List<Adat_Szerszám_Cikktörzs> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Szerszám_Cikktörzs Adat;
            List<Adat_Szerszám_Cikktörzs> Adatok = new List<Adat_Szerszám_Cikktörzs>();

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
                                Adat = new Adat_Szerszám_Cikktörzs(
                                           rekord["Azonosító"].ToStrTrim(),
                                           rekord["megnevezés"].ToStrTrim(),
                                           rekord["méret"].ToStrTrim(),
                                           rekord["hely"].ToStrTrim(),
                                           rekord["leltáriszám"].ToStrTrim(),
                                           rekord["Beszerzésidátum"].ToÉrt_DaTeTime(),
                                           rekord["státus"].ToÉrt_Int(),
                                           rekord["költséghely"].ToStrTrim(),
                                           rekord["gyáriszám"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string hely, string jelszó, Adat_Szerszám_Cikktörzs Adat)
        {
            string szöveg = "UPDATE cikktörzs  SET ";
            szöveg += $"megnevezés='{Adat.Megnevezés}', ";
            szöveg += $"méret='{Adat.Méret}', ";
            szöveg += $"leltáriszám='{Adat.Leltáriszám}', ";
            szöveg += $"költséghely='{Adat.Költséghely}', ";
            szöveg += $"hely='{Adat.Hely}', ";
            szöveg += $"státus='{Adat.Státus}', ";
            szöveg += $"gyáriszám='{Adat.Gyáriszám}', ";
            szöveg += $" Beszerzésidátum='{Adat.Beszerzésidátum:yyyy.MM.dd}' ";
            szöveg += $" WHERE azonosító='{Adat.Azonosító}'";
            Adatbázis.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Rögzítés(string hely, string jelszó, Adat_Szerszám_Cikktörzs Adat)
        {
            string szöveg = "INSERT INTO cikktörzs  (azonosító, megnevezés, méret, leltáriszám, Beszerzésidátum, státus, hely, költséghely, gyáriszám) VALUES (";
            szöveg += $"'{Adat.Azonosító}', ";
            szöveg += $"'{Adat.Megnevezés}', ";
            szöveg += $"'{Adat.Méret}', ";
            szöveg += $"'{Adat.Leltáriszám}', ";
            szöveg += $"'{Adat.Beszerzésidátum:yyyy.MM.dd}', ";
            szöveg += $"{Adat.Státus}, ";
            szöveg += $"'{Adat.Hely}', ";
            szöveg += $"'{Adat.Költséghely}', ";
            szöveg += $"'{Adat.Gyáriszám}') ";
            Adatbázis.ABMódosítás(hely, jelszó, szöveg);
        }
    }

    public class Kezelő_Szerszám_Könyv
    {
        public List<Adat_Szerszám_Könyvtörzs> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Szerszám_Könyvtörzs Adat;
            List<Adat_Szerszám_Könyvtörzs> Adatok = new List<Adat_Szerszám_Könyvtörzs>();
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
                                Adat = new Adat_Szerszám_Könyvtörzs(
                                           rekord["szerszámkönyvszám"].ToStrTrim(),
                                           rekord["szerszámkönyvnév"].ToStrTrim(),
                                           rekord["felelős1"].ToStrTrim(),
                                           rekord["felelős2"].ToStrTrim(),
                                           rekord["státus"].ToÉrt_Bool());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Szerszám_Könyvtörzs Adat)
        {
            try
            {
                string szöveg = "INSERT INTO könyvtörzs  (Szerszámkönyvszám, Szerszámkönyvnév, felelős1, felelős2, státus ) VALUES (";
                szöveg += $"'{Adat.Szerszámkönyvszám}', ";
                szöveg += $"'{Adat.Szerszámkönyvnév}', ";
                szöveg += $"'{Adat.Felelős1}', ";
                szöveg += $"'{Adat.Felelős2}', ";
                szöveg += $"{Adat.Státus}) ";
                Adatbázis.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Könyv rögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás(string hely, string jelszó, Adat_Szerszám_Könyvtörzs Adat)
        {
            try
            {
                string szöveg = "UPDATE könyvtörzs  SET ";
                szöveg += $"Szerszámkönyvnév='{Adat.Szerszámkönyvnév.Trim()}', ";
                szöveg += $"felelős1='{Adat.Felelős1.Trim()}', ";
                szöveg += $"felelős2='{Adat.Felelős2.Trim()}', ";
                szöveg += $"státus={Adat.Státus} ";
                szöveg += $" WHERE Szerszámkönyvszám='{Adat.Szerszámkönyvszám.Trim()}'";
                Adatbázis.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Könyv módosítás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

    public class Kezelő_Szerszám_Napló
    {
        public List<Adat_Szerszám_Napló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Szerszám_Napló> Adatok = new List<Adat_Szerszám_Napló>();
            Adat_Szerszám_Napló Adat;

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
                                Adat = new Adat_Szerszám_Napló(
                                rekord["azonosító"].ToStrTrim(),
                                rekord["honnan"].ToStrTrim(),
                                rekord["hova"].ToStrTrim(),
                                rekord["mennyiség"].ToÉrt_Int(),
                                rekord["módosította"].ToStrTrim(),
                                rekord["módosításidátum"].ToÉrt_DaTeTime()
                                );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Szerszám_Napló Adat)
        {
            string szöveg = "INSERT INTO napló  (azonosító, honnan, hova, mennyiség, módosította, módosításidátum ) VALUES (";
            szöveg += $"'{Adat.Azonosító}', ";
            szöveg += $"'{Adat.Honnan}', ";
            szöveg += $"'{Adat.Hova}', ";
            szöveg += $"{Adat.Mennyiség}, ";
            szöveg += $"'{Program.PostásNév}', ";
            szöveg += $"'{DateTime.Now}') ";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

    }
}

