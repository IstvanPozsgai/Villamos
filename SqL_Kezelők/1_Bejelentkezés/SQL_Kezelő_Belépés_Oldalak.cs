using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class SQL_Kezelő_Belépés_Oldalak
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\SQL\Belépés.db";
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tbl_Bejelentkezés_Oldalak";

        public SQL_Kezelő_Belépés_Oldalak()
        {
            if (!File.Exists(hely)) Tábla_Létrehozás();
            if (!MyA.SqLite_ABvanTábla(hely, jelszó, táblanév)) Tábla_Létrehozás();
        }

        public void Tábla_Létrehozás()
        {
            try
            {
                string szöveg = $@"CREATE TABLE {táblanév} (
                                OldalId INTEGER PRIMARY KEY AUTOINCREMENT,
                                FromName TEXT, 
                                MenuName TEXT, 
                                MenuFelirat TEXT, 
                                Látható INTEGER, 
                                Törölt INTEGER
                                );";
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

        public List<Adat_Belépés_Oldalak> Lista_Adatok()
        {
            List<Adat_Belépés_Oldalak> Adatok = new List<Adat_Belépés_Oldalak>();
            try
            {
                Adatok = MyA.Lista_Adatok(hely, jelszó, táblanév, rekord => new Adat_Belépés_Oldalak(
                                        rekord["OldalId"].ToÉrt_Int(),
                                        rekord["FromName"].ToStrTrim(),
                                        rekord["MenuName"].ToStrTrim(),
                                        rekord["MenuFelirat"].ToStrTrim(),
                                        rekord["Látható"].ToÉrt_Bool(),
                                        rekord["Törölt"].ToÉrt_Bool()
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

        public void Döntés(Adat_Belépés_Oldalak Adat)
        {
            try
            {
                List<Adat_Belépés_Oldalak> Adatok = Lista_Adatok();
                if (!Adatok.Any(a => a.OldalId == Adat.OldalId))
                    Rögzítés(Adat);
                else
                    Módosítás(Adat);

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

        public void Rögzítés(Adat_Belépés_Oldalak Adat)
        {
            try
            {

                string szöveg = $"INSERT INTO {táblanév} ( FromName, MenuName, MenuFelirat, Látható, Törölt) VALUES ";
                szöveg += $@"( @FromName, @MenuName, @MenuFelirat, @Látható, @Törölt)";


                SqliteCommand cmd = new SqliteCommand(szöveg);
                cmd.Parameters.AddWithValue("@FromName", Adat.FromName);
                cmd.Parameters.AddWithValue("@MenuName", Adat.MenuName);
                cmd.Parameters.AddWithValue("@MenuFelirat", Adat.MenuFelirat);
                cmd.Parameters.AddWithValue("@Látható", Adat.Látható);
                cmd.Parameters.AddWithValue("@Törölt", Adat.Törölt);

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

        public void Módosítás(Adat_Belépés_Oldalak Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $@"FromName=@FromName, ";
                szöveg += $@"MenuName=@MenuName, ";
                szöveg += $@"MenuFelirat=@MenuFelirat, ";
                szöveg += $@"Látható=@Látható, ";
                szöveg += $@"Törölt=@Törölt, ";

                szöveg += $@"WHERE OldalId=@OldalId;";

                SqliteCommand cmd = new SqliteCommand(szöveg);

                cmd.Parameters.AddWithValue("@OldalId", Adat.OldalId);
                cmd.Parameters.AddWithValue("@FromName", Adat.FromName);
                cmd.Parameters.AddWithValue("@MenuName", Adat.MenuName);
                cmd.Parameters.AddWithValue("@MenuFelirat", Adat.MenuFelirat);
                cmd.Parameters.AddWithValue("@Látható", Adat.Látható);
                cmd.Parameters.AddWithValue("@Törölt", Adat.Törölt);

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
