using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű2
    {
        readonly string jelszó = "pozsgaii";
        string hely;
        readonly string táblanév = "állománytábla";

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\villamos\villamos2.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Villamostábla(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_2> Lista_Adatok(string Telephely)
        {
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY Azonosító ";
            FájlBeállítás(Telephely);
            List<Adat_Jármű_2> Adatok = new List<Adat_Jármű_2>();
            Adat_Jármű_2 adat;

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
                                adat = new Adat_Jármű_2(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["takarítás"].ToÉrt_DaTeTime(),
                                    rekord["haromnapos"].ToÉrt_Int()
                                    );
                                Adatok.Add(adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string Telephely, Adat_Jármű_2 Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                // Ha már létezik, akkor módosítjuk
                string szöveg = $"UPDATE {táblanév}  SET ";
                szöveg += $"takarítás='{Adat.Takarítás}', "; // takarítás
                szöveg += $"haromnapos='{Adat.Haromnapos}' "; // haromnapos
                szöveg += $" WHERE azonosító='{Adat.Azonosító.Trim()}'";
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

        public void Módosítás(string Telephely, List<Adat_Jármű_2> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Jármű_2 Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET haromnapos={Adat.Haromnapos} WHERE azonosító='{Adat.Azonosító}'";
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

        public void Módosítás99(string Telephely)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE {táblanév} SET {táblanév}.haromnapos = 99";
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

        public void Rögzítés(string Telephely, Adat_Jármű_2 Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"INSERT INTO {táblanév} (azonosító, takarítás, haromnapos) VALUES (";
                szöveg += $"'{Adat.Azonosító.Trim()}', "; // azonosító
                szöveg += $"'{Adat.Takarítás}', "; // takarítás
                szöveg += $"{Adat.Haromnapos}) "; // haromnapos

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

        public void Rögzítés(string Telephely, List<Adat_Jármű_2> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> SzövegGY = new List<string>();
                foreach (Adat_Jármű_2 Adat in Adatok)
                {
                    // ha nem létezik 
                    string szöveg = $"INSERT INTO {táblanév} (azonosító, takarítás, haromnapos) VALUES (";
                    szöveg += $"'{Adat.Azonosító.Trim()}', "; // azonosító
                    szöveg += $"'{Adat.Takarítás}', "; // takarítás
                    szöveg += $"{Adat.Haromnapos}) "; // haromnapos
                    SzövegGY.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGY);
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
                FájlBeállítás(Telephely);
                string szöveg = $"DELETE FROM {táblanév} WHERE [azonosító]='{Azonosító}'";
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



        //Elkopó
        public List<Adat_Jármű_2> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_2> Adatok = new List<Adat_Jármű_2>();
            Adat_Jármű_2 adat;

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
                                if (!DateTime.TryParse(rekord["takarítás"].ToString(), out DateTime takarítás))
                                    takarítás = new DateTime(1900, 1, 1);

                                adat = new Adat_Jármű_2(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["takarítás"].ToÉrt_DaTeTime(),
                                    rekord["haromnapos"].ToÉrt_Int()
                                    );
                                Adatok.Add(adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }
}
