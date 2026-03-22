using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class SQL_Kezelő_Rezsi_Törzs
    {
        readonly string jelszó = "csavarhúzó";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\SQL\Rezsi\rezsitörzs.db";
        readonly string Táblanév = "törzs";

        public SQL_Kezelő_Rezsi_Törzs()
        {
            if (!File.Exists(hely.KönyvSzerk())) Tábla_Létrehozás();
        }

        private void Tábla_Létrehozás()
        {
            try
            {
                //A sqlite adatbázisban a következő táblát hoztuk létre
                // JAVÍTANDÓ:
                string szöveg = $@"CREATE TABLE {táblanév} (
                                Azonosító TEXT,
                            	Honnan TEXT,
                            	Hova  TEXT,
                            	Mennyiség REAL,
                            	Mirehasznál   TEXT,
                            	Módosította   TEXT,
                            	Módosításidátum   TEXT,
                            	Státus    INTEGER);";
                MyA.SqLite_TáblaLétrehozás(hely.KönyvSzerk(), jelszó, szöveg);
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

        public List<Adat_Rezsi_Törzs> Lista_Adatok()
        {
            List<Adat_Rezsi_Törzs> Adatok = new List<Adat_Rezsi_Törzs>();
            try
            {
                Adatok = MyA.Lista_Adatok(hely, jelszó, táblanév, rekord => new Adat_Rezsi_Törzs(
                           rekord["Azonosító"].ToStrTrim(),
                           rekord["Megnevezés"].ToStrTrim(),
                           rekord["Méret"].ToStrTrim(),
                           rekord["Státus"].ToÉrt_Int(),
                           rekord["Csoport"].ToStrTrim()
                          ));
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
            return Adatok;
        }

        public void Rögzítés(Adat_Rezsi_Törzs Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {Táblanév} (azonosító, megnevezés, Méret, státus, csoport ) VALUES ";
                szöveg += "(@Azonosító, @Megnevezés, @Méret, @Státusz, @Csoport)";

                SqliteCommand cmd = new SqliteCommand(szöveg);
                cmd.Parameters.AddWithValue("@Azonosító ", Adat.Azonosító);
                cmd.Parameters.AddWithValue("@Megnevezés ", Adat.Megnevezés);
                cmd.Parameters.AddWithValue("@Méret ", Adat.Méret);
                cmd.Parameters.AddWithValue("@Státusz ", Adat.Státusz);
                cmd.Parameters.AddWithValue("@Csoport ", Adat.Csoport);

                MyA.SqLite_Módosítás(hely, jelszó, cmd);

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

        public void Módosítás(Adat_Rezsi_Törzs Adat)
        {
            try
            {
                string szöveg = $"UPDATE {Táblanév} SET ";
                szöveg += $@"Megnevezés =@Megnevezés, ";
                szöveg += $@"Méret =@Méret, ";
                szöveg += $@"Státusz =@Státusz, ";
                szöveg += $@"Csoport =@Csoport  ";
                szöveg += $" WHERE azonosító=@Azonosító";

                SqliteCommand cmd = new SqliteCommand(szöveg);

                cmd.Parameters.AddWithValue("@Azonosító ", Adat.Azonosító);
                cmd.Parameters.AddWithValue("@Megnevezés ", Adat.Megnevezés);
                cmd.Parameters.AddWithValue("@Méret ", Adat.Méret);
                cmd.Parameters.AddWithValue("@Státusz ", Adat.Státusz);
                cmd.Parameters.AddWithValue("@Csoport ", Adat.Csoport);

                MyA.SqLite_Módosítás(hely, jelszó, cmd);
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
