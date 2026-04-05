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
        /// Eldöntük az új adatok alapján, hogy melyeket kell rögzíteni és melyeket módosítani, majd ezeket külön-külön továbbítjuk a megfelelő metódusoknak
        /// </summary>
        /// <param name="Adatok"></param>
        public void Döntés(List<Adat_Bejelentkezés_Jogosultságok> Adatok)
        {
            try
            {
                List<Adat_Bejelentkezés_Jogosultságok> AdatokRégi = Lista_Adatok();
                List<Adat_Bejelentkezés_Jogosultságok> AdatokR = new List<Adat_Bejelentkezés_Jogosultságok>();
                List<Adat_Bejelentkezés_Jogosultságok> AdatokM = new List<Adat_Bejelentkezés_Jogosultságok>();
                foreach (Adat_Bejelentkezés_Jogosultságok Adat in Adatok)
                {
                    // Ha a régi adatok között nincs benne akkor rögzítjük az újakat.
                    if (!AdatokRégi.Any(a => a.SzervezetId == Adat.SzervezetId && a.UserId == Adat.UserId && a.OldalId == Adat.OldalId && a.GombokId == Adat.GombokId))
                    {
                        AdatokR.Add(Adat);
                    }
                    else
                    {
                        string szöveg = $"UPDATE {táblanév} SET ";
                        szöveg += $"Törölt ={Adat.Törölt} ";
                        szöveg += $"WHERE SzervezetId = {Adat.SzervezetId} AND ";
                        szöveg += $"UserId ={Adat.UserId} AND ";
                        szöveg += $"OldalId ={Adat.OldalId} AND ";
                        szöveg += $"GombokId ={Adat.GombokId}";
                        AdatokM.Add(Adat);
                    }
                }
                if (AdatokR.Count > 0) Rögzítés(AdatokR);
                if (AdatokM.Count > 0) Módosítás(AdatokM);
                //Miután átállítottuk kitöröljük a törölt elemeket
                Törlés();
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



        /// <summary>
        /// Rögzítéshez előkészítjük a parancsokat és egyetlen hívással elküldjük az összeset
        /// </summary>
        /// <param name="Adatok"></param>
        private void Rögzítés(List<Adat_Bejelentkezés_Jogosultságok> Adatok)
        {
            try
            {
                List<SqliteCommand> parancsLista = new List<SqliteCommand>();
                string szöveg = $"INSERT INTO {táblanév} (UserId, OldalId, GombokId, SzervezetId, Törölt) VALUES ";
                szöveg += $@"(@UserId, @OldalId, @GombokId, @SzervezetId, @Törölt)";

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

                // Egyetlen hívással elküldjük az összeset
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


        /// <summary>
        /// Módosításhoz előkészítjük a parancsokat és egyetlen hívással elküldjük az összeset
        /// </summary>
        /// <param name="Adatok"></param>
        private void Módosítás(List<Adat_Bejelentkezés_Jogosultságok> Adatok)
        {
            try
            {
                List<SqliteCommand> parancsLista = new List<SqliteCommand>();

                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $@"Törölt=@Törölt ";
                szöveg += $@"WHERE UserId=@UserId AND ";
                szöveg += $@"OldalId=@OldalId AND ";
                szöveg += $@"GombokId=@GombokId AND ";
                szöveg += $@"SzervezetId=@SzervezetId ";

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

                // Egyetlen hívással elküldjük az összeset
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


        public void Törlés()
        {
            try
            {
                // SQL DELETE parancs az azonosító alapján
                string szöveg = $"DELETE FROM {táblanév} WHERE Törölt=true";

                SqliteCommand cmd = new SqliteCommand(szöveg);

                // Meghívjuk a saját segédmetódusodat a végrehajtáshoz
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
