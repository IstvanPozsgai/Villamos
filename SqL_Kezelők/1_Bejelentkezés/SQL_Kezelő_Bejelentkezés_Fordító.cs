using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class SQL_Kezelő_Bejelentkezés_Fordító
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\SQL\Belépés.db";
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tbl_Bejelentkezés_Fordító";

        public SQL_Kezelő_Bejelentkezés_Fordító()
        {
            if (!File.Exists(hely)) Tábla_Létrehozás();
            if (!MyA.SqLite_ABvanTábla(hely, jelszó, táblanév)) Tábla_Létrehozás();
        }

        public void Tábla_Létrehozás()
        {
            try
            {
                string szöveg = $@"CREATE TABLE {táblanév} (
                                GombokId INTEGER PRIMARY KEY AUTOINCREMENT,
                                FromName TEXT, 
                                GombName TEXT, 
                                Szervezet TEXT, 
                                MelyikBetű INTEGER, 
                                MelyikOszlop INTEGER
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

        public List<Adat_Bejelentkezés_Fordító> Lista_Adatok()
        {
            List<Adat_Bejelentkezés_Fordító> Adatok = new List<Adat_Bejelentkezés_Fordító>();
            try
            {
                Adatok = MyA.Lista_Adatok(hely, jelszó, táblanév, rekord => new Adat_Bejelentkezés_Fordító(
                                rekord["GombokId"].ToÉrt_Int(),
                                rekord["FromName"].ToStrTrim(),
                                rekord["GombName"].ToStrTrim(),
                                rekord["Szervezet"].ToStrTrim(),
                                rekord["MelyikBetű"].ToÉrt_Int(),
                                rekord["MelyikOszlop"].ToÉrt_Int(),
                                rekord["UserId"].ToÉrt_Int(),
                                rekord["OldalId"].ToÉrt_Int(),
                                rekord["SzervezetID"].ToÉrt_Int()
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



        public void Rögzítés(Adat_Bejelentkezés_Fordító Adat)
        {
            try
            {

                string szöveg = $"INSERT INTO {táblanév} (GombokId, FromName, GombName, Szervezet, MelyikBetű, MelyikOszlop, UserId, OldalId, SzervezetId) VALUES ";
                szöveg += $@"(@GombokId, @FromName, @GombName, @Szervezet, @MelyikBetű, @MelyikOszlop, @UserId, @OldalId, @SzervezetId))";


                SqliteCommand cmd = new SqliteCommand(szöveg);
                cmd.Parameters.AddWithValue("@GombokId", Adat.GombokId);
                cmd.Parameters.AddWithValue("@FromName", Adat.FromName);
                cmd.Parameters.AddWithValue("@GombName", Adat.GombName);
                cmd.Parameters.AddWithValue("@Szervezet", Adat.Szervezet);
                cmd.Parameters.AddWithValue("@MelyikBetű", Adat.MelyikBetű);
                cmd.Parameters.AddWithValue("@MelyikOszlop", Adat.MelyikOszlop);
                cmd.Parameters.AddWithValue("@UserId", Adat.UserId);
                cmd.Parameters.AddWithValue("@OldalId", Adat.OldalId);
                cmd.Parameters.AddWithValue("@SzervezetId", Adat.SzervezetId);

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

        public void Rögzítés(List<Adat_Bejelentkezés_Fordító> Adatok)
        {
            try
            {
                List<SqliteCommand> parancsLista = new List<SqliteCommand>();
                string szöveg = $"INSERT INTO {táblanév} (GombokId, FromName, GombName, Szervezet, MelyikBetű, MelyikOszlop, UserId, OldalId, SzervezetId) VALUES ";
                szöveg += $@"(@GombokId, @FromName, @GombName, @Szervezet, @MelyikBetű, @MelyikOszlop, @UserId, @OldalId, @SzervezetId))";

                foreach (var Adat in Adatok)
                {
                    SqliteCommand cmd = new SqliteCommand(szöveg);
                    cmd.Parameters.AddWithValue("@GombokId", Adat.GombokId);
                    cmd.Parameters.AddWithValue("@FromName", Adat.FromName);
                    cmd.Parameters.AddWithValue("@GombName", Adat.GombName);
                    cmd.Parameters.AddWithValue("@Szervezet", Adat.Szervezet);
                    cmd.Parameters.AddWithValue("@MelyikBetű", Adat.MelyikBetű);
                    cmd.Parameters.AddWithValue("@MelyikOszlop", Adat.MelyikOszlop);
                    cmd.Parameters.AddWithValue("@UserId", Adat.UserId);
                    cmd.Parameters.AddWithValue("@OldalId", Adat.OldalId);
                    cmd.Parameters.AddWithValue("@SzervezetId", Adat.SzervezetId);
                    parancsLista.Add(cmd);
                }
                MyA.SqLite_Módosítások(hely, jelszó, parancsLista);
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

        public void Módosítás(Adat_Bejelentkezés_Fordító Adat)
        {
            try
            {

                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $@"FromName=@FromName, ";
                szöveg += $@"GombName=@GombName, ";
                szöveg += $@"Szervezet=@Szervezet, ";
                szöveg += $@"MelyikBetű=@MelyikBetű, ";
                szöveg += $@"MelyikOszlop=@MelyikOszlop, ";
                szöveg += $@"UserId=@UserId, ";
                szöveg += $@"OldalId=@OldalId, ";
                szöveg += $@"SzervezetId=@SzervezetId ";
                szöveg += $@"WHERE GombokId=@GombokId;";

                SqliteCommand cmd = new SqliteCommand(szöveg);

                cmd.Parameters.AddWithValue("@GombokId", Adat.GombokId);
                cmd.Parameters.AddWithValue("@FromName", Adat.FromName);
                cmd.Parameters.AddWithValue("@GombName", Adat.GombName);
                cmd.Parameters.AddWithValue("@Szervezet", Adat.Szervezet);
                cmd.Parameters.AddWithValue("@MelyikBetű", Adat.MelyikBetű);
                cmd.Parameters.AddWithValue("@MelyikOszlop", Adat.MelyikOszlop);
                cmd.Parameters.AddWithValue("@UserId", Adat.UserId);
                cmd.Parameters.AddWithValue("@OldalId", Adat.OldalId);
                cmd.Parameters.AddWithValue("@SzervezetId", Adat.SzervezetId);

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
