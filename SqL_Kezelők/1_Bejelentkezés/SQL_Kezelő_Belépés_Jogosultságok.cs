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
    public class SQL_Kezelő_Belépés_Jogosultságok
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\SQL\Belépés.db";
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tbl_Bejelentkezés_Jogosultság";

        public SQL_Kezelő_Belépés_Jogosultságok()
        {
            if (!File.Exists(hely)) Tábla_Létrehozás();
            if (!MyA.SqLite_ABvanTábla(hely, jelszó, táblanév)) Tábla_Létrehozás();
        }

        public void Tábla_Létrehozás()
        {
            try
            {
                string szöveg = $@"CREATE TABLE {táblanév} (
                                UserId INTEGER, 
                                OldalId INTEGER, 
                                GombokId INTEGER, 
                                SzervezetId INTEGER, 
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

        public List<Adat_Bejelentkezés_Jogosultságok> Lista_Adatok()
        {
            List<Adat_Bejelentkezés_Jogosultságok> Adatok = new List<Adat_Bejelentkezés_Jogosultságok>();
            try
            {
                Adatok = MyA.Lista_Adatok(hely, jelszó, táblanév, rekord => new Adat_Bejelentkezés_Jogosultságok(
                                        rekord["UserId"].ToÉrt_Int(),
                                        rekord["OldalId"].ToÉrt_Int(),
                                        rekord["GombokId"].ToÉrt_Int(),
                                        rekord["SzervezetId"].ToÉrt_Int(),
                                        rekord["Törölt"].ToÉrt_Bool()));
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

        /// <summary>
        /// A teljes memóriában lévő lista szinkronizálása az adatbázissal egyetlen szupergyors tranzakcióban.
        /// </summary>
        public void Teljes_Szinkronizáció(List<Adat_Bejelentkezés_Jogosultságok> MemóriaAdatok)
        {
            try
            {
                // Lekérjük az adatbázis jelenlegi állapotát
                List<Adat_Bejelentkezés_Jogosultságok> DbAdatok = Lista_Adatok();

                using (var connection = new SqliteConnection($"Data Source={hely};Password={jelszó};"))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        string insertSzöveg = $"INSERT INTO {táblanév} (UserId, OldalId, GombokId, SzervezetId, Törölt) VALUES (@UserId, @OldalId, @GombokId, @SzervezetId, @Törölt)";
                        string updateSzöveg = $"UPDATE {táblanév} SET Törölt=@Törölt WHERE UserId=@UserId AND OldalId=@OldalId AND GombokId=@GombokId AND SzervezetId=@SzervezetId";

                        foreach (var adat in MemóriaAdatok)
                        {
                            var regiAdat = DbAdatok.FirstOrDefault(a => a.SzervezetId == adat.SzervezetId && a.UserId == adat.UserId && a.OldalId == adat.OldalId && a.GombokId == adat.GombokId);

                            using (var cmd = connection.CreateCommand())
                            {
                                cmd.Transaction = transaction;

                                if (regiAdat == null)
                                {
                                    // Ha nincs még az adatbázisban, és nem is törölt, beszúrjuk
                                    if (!adat.Törölt)
                                    {
                                        cmd.CommandText = insertSzöveg;
                                        cmd.Parameters.AddWithValue("@UserId", adat.UserId);
                                        cmd.Parameters.AddWithValue("@OldalId", adat.OldalId);
                                        cmd.Parameters.AddWithValue("@GombokId", adat.GombokId);
                                        cmd.Parameters.AddWithValue("@SzervezetId", adat.SzervezetId);
                                        cmd.Parameters.AddWithValue("@Törölt", adat.Törölt);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    // Ha már létezik, de változott a törölt státusza, frissítjük
                                    if (regiAdat.Törölt != adat.Törölt)
                                    {
                                        cmd.CommandText = updateSzöveg;
                                        cmd.Parameters.AddWithValue("@UserId", adat.UserId);
                                        cmd.Parameters.AddWithValue("@OldalId", adat.OldalId);
                                        cmd.Parameters.AddWithValue("@GombokId", adat.GombokId);
                                        cmd.Parameters.AddWithValue("@SzervezetId", adat.SzervezetId);
                                        cmd.Parameters.AddWithValue("@Törölt", adat.Törölt);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                        }

                        // Kitöröljük a véglegesen töröltre állított sorokat a tisztaság kedvéért
                        using (var cmd = connection.CreateCommand())
                        {
                            cmd.Transaction = transaction;
                            cmd.CommandText = $"DELETE FROM {táblanév} WHERE Törölt = true";
                            cmd.ExecuteNonQuery();
                        }

                        // Véglegesítjük a tranzakciót (Fizikai írás)
                        transaction.Commit();
                    }
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
        public void Döntés(List<Adat_Bejelentkezés_Jogosultságok> Adatok)
        {
            try
            {
                List<Adat_Bejelentkezés_Jogosultságok> AdatokRégi = Lista_Adatok();
                List<Adat_Bejelentkezés_Jogosultságok> AdatokR = new List<Adat_Bejelentkezés_Jogosultságok>();
                List<Adat_Bejelentkezés_Jogosultságok> AdatokM = new List<Adat_Bejelentkezés_Jogosultságok>();
                foreach (Adat_Bejelentkezés_Jogosultságok Adat in Adatok)
                {
                    if (!AdatokRégi.Any(a => a.SzervezetId == Adat.SzervezetId && a.UserId == Adat.UserId && a.OldalId == Adat.OldalId && a.GombokId == Adat.GombokId))
                    {
                        AdatokR.Add(Adat);
                    }
                    else
                    {
                        AdatokM.Add(Adat);
                    }
                }
                if (AdatokR.Count > 0) Rögzítés(AdatokR);
                if (AdatokM.Count > 0) Módosítás(AdatokM);
                Törlés();
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
            }
        }

        private void Rögzítés(List<Adat_Bejelentkezés_Jogosultságok> Adatok)
        {
            try
            {
                List<SqliteCommand> parancsLista = new List<SqliteCommand>();
                string szöveg = $"INSERT INTO {táblanév} (UserId, OldalId, GombokId, SzervezetId, Törölt) VALUES (@UserId, @OldalId, @GombokId, @SzervezetId, @Törölt)";

                foreach (var adat in Adatok)
                {
                    SqliteCommand cmd = new SqliteCommand(szöveg);
                    cmd.Parameters.AddWithValue("@UserId", adat.UserId);
                    cmd.Parameters.AddWithValue("@OldalId", adat.OldalId);
                    cmd.Parameters.AddWithValue("@GombokId", adat.GombokId);
                    cmd.Parameters.AddWithValue("@SzervezetId", adat.SzervezetId);
                    cmd.Parameters.AddWithValue("@Törölt", adat.Törölt);
                    parancsLista.Add(cmd);
                }
                MyA.SqLite_Módosítások(hely, jelszó, parancsLista);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
            }
        }

        private void Módosítás(List<Adat_Bejelentkezés_Jogosultságok> Adatok)
        {
            try
            {
                List<SqliteCommand> parancsLista = new List<SqliteCommand>();
                string szöveg = $"UPDATE {táblanév} SET Törölt=@Törölt WHERE UserId=@UserId AND OldalId=@OldalId AND GombokId=@GombokId AND SzervezetId=@SzervezetId ";

                foreach (var adat in Adatok)
                {
                    SqliteCommand cmd = new SqliteCommand(szöveg);
                    cmd.Parameters.AddWithValue("@UserId", adat.UserId);
                    cmd.Parameters.AddWithValue("@OldalId", adat.OldalId);
                    cmd.Parameters.AddWithValue("@GombokId", adat.GombokId);
                    cmd.Parameters.AddWithValue("@SzervezetId", adat.SzervezetId);
                    cmd.Parameters.AddWithValue("@Törölt", adat.Törölt);
                    parancsLista.Add(cmd);
                }
                MyA.SqLite_Módosítások(hely, jelszó, parancsLista);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
            }
        }

        public void Törlés()
        {
            try
            {
                string szöveg = $"DELETE FROM {táblanév} WHERE Törölt=true";
                SqliteCommand cmd = new SqliteCommand(szöveg);
                MyA.SqLite_Módosítás(hely, jelszó, cmd);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
            }
        }

        public void Törlés(Adat_Bejelentkezés_Jogosultságok Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM {táblanév} WHERE UserId=@UserId AND OldalId=@OldalId";
                SqliteCommand cmd = new SqliteCommand(szöveg);
                cmd.Parameters.AddWithValue("@UserId", Adat.UserId);
                cmd.Parameters.AddWithValue("@OldalId", Adat.OldalId);
                MyA.SqLite_Módosítás(hely, jelszó, cmd);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
            }
        }
    }
}