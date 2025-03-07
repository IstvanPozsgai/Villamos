using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_CAF_Adatok
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
        readonly string jelszó = "CzabalayL";

        public Kezelő_CAF_Adatok()
        {
            //Ellenőrzés
        }


        public List<Adat_CAF_Adatok> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM adatok ORDER BY azonosító";
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

        public double Sorszám()
        {
            double válasz = 1;
            try
            {
                List<Adat_CAF_Adatok> Adatok = Lista_Adatok();
                if (Adatok.Count > 0) válasz = Adatok.Max(a => a.Id) + 1;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Sorsszám", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return válasz;
        }

        public void Döntés(Adat_CAF_Adatok Adat)
        {
            try
            {
                double sorszám;
                // ha nincs kitöltve az id, megkeressük a következő számot
                if (Adat.Id == 0)
                    sorszám = Sorszám();
                else
                    sorszám = Adat.Id;

                List<Adat_CAF_Adatok> Adatok = Lista_Adatok();

                Adat_CAF_Adatok Elem = (from a in Adatok
                                        where a.Id == sorszám
                                        select a).FirstOrDefault();

                if (Elem != null)
                    Módosítás(Adat);
                else
                    Rögzítés(Adat, sorszám);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "RögzítiMódosít", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Rögzítés(Adat_CAF_Adatok Adat, double Sorszám)
        {
            try
            {
                string szöveg = "INSERT INTO adatok (id, azonosító, vizsgálat, Dátum, számláló, státus, km_sorszám, idő_sorszám, idővKM, megjegyzés, Dátum_program) VALUES (";
                szöveg += $"{Sorszám}, "; // id 
                szöveg += $"'{Adat.Azonosító}', "; // azonosító
                szöveg += $"'{Adat.Vizsgálat.Trim()}', "; // vizsgálat
                szöveg += $" '{Adat.Dátum:yyyy.MM.dd}', "; // Dátum
                szöveg += $"{Adat.Számláló}, "; // számláló
                szöveg += $"{Adat.Státus}, "; // státus 
                szöveg += $"{Adat.KM_Sorszám}, "; // km_sorszám
                szöveg += $"{Adat.IDŐ_Sorszám}, "; // idő_sorszám
                szöveg += $"{Adat.IDŐvKM}, ";// idővKM
                szöveg += "'{Adat.Megjegyzés}', "; // megjegyzés
                szöveg += " '{Adat.Dátum_program:yyyy.MM.dd}') ";
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

        public void Módosítás(Adat_CAF_Adatok Adat)
        {
            try
            {
                string szöveg = "UPDATE adatok  SET ";
                szöveg += $"vizsgálat='{Adat.Vizsgálat}', "; // vizsgálat
                szöveg += $"Dátum='{Adat.Dátum:yyyy.MM.dd}', "; // Dátum
                szöveg += $"számláló={Adat.Számláló}, "; // számláló
                szöveg += $"státus={Adat.Státus}, "; // státus 
                szöveg += $"km_sorszám={Adat.KM_Sorszám}, "; // km_sorszám
                szöveg += $"idő_sorszám={Adat.IDŐ_Sorszám}, "; // idő_sorszám
                szöveg += $"megjegyzés='{Adat.Megjegyzés}', ";// megjegyzés
                szöveg += $"idővKM={Adat.IDŐvKM} ";
                szöveg += $" WHERE id={Adat.Id}";
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

        public void Módosítás_Státus(double Sorszám, int státus)
        {
            try
            {
                string szöveg = $"UPDATE adatok  SET státus={státus} WHERE id={Sorszám}";
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

}
