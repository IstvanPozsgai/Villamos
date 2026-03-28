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
    public class SQL_Kezelő_Users
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\SQL\Belépés.mdb";
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tbl_Bejelentkezés_Users";

        public SQL_Kezelő_Users()
        {
            if (!File.Exists(hely)) Tábla_Létrehozás();
            if (!MyA.SqLite_ABvanTábla(hely, jelszó, táblanév)) Tábla_Létrehozás();
        }

        public void Tábla_Létrehozás()
        {
            try
            {
                string szöveg = $@"CREATE TABLE {táblanév} (
                                UserId INTEGER PRIMARY KEY AUTOINCREMENT, 
                                UserName TEXT, 
                                WinUserName TEXT, 
                                Dolgozószám TEXT, 
                                Password TEXT, 
                                Dátum TEXT, 
                                Frissít INTEGER, 
                                Törölt INTEGER, 
                                Szervezetek TEXT, 
                                Szervezet TEXT, 
                                GlobalAdmin INTEGER, 
                                TelepAdmin INTEGER
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

        public List<Adat_Users> Lista_Adatok()
        {
            List<Adat_Users> Adatok = new List<Adat_Users>();
            try
            {
                Adatok = MyA.Lista_Adatok(hely, jelszó, táblanév, rekord => new Adat_Users(
                                        rekord["UserId"].ToÉrt_Int(),
                                        rekord["UserName"].ToStrTrim(),
                                        rekord["WinUserName"].ToStrTrim(),
                                        rekord["Dolgozószám"].ToStrTrim(),
                                        rekord["Password"].ToStrTrim(),
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Frissít"].ToÉrt_Bool(),
                                        rekord["Törölt"].ToÉrt_Bool(),
                                        rekord["Szervezetek"].ToStrTrim(),
                                        rekord["Szervezet"].ToStrTrim(),
                                        rekord["GlobalAdmin"].ToÉrt_Bool(),
                                        rekord["TelepAdmin"].ToÉrt_Bool()
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

        public void Rögzítés(Adat_Users Adat)
        {
            try
            {
                string pword = Adat.Password;
                if (Adat.Password.Trim() == "") pword = "123456";
                bool frissít = true; //első rögzítéskor alapból előírjuk a jelszó megváltoztatását

                string szöveg = $"INSERT INTO {táblanév} (UserId, UserName, WinUserName, Dolgozószám, Password, Dátum, Frissít, Törölt, Szervezetek, Szervezet, GlobalAdmin, TelepAdmin) VALUES ";
                szöveg += $@"(@UserId, @UserName, @WinUserName, @Dolgozószám, @Password, @Dátum, @Frissít, @Törölt, @Szervezetek, @Szervezet, @GlobalAdmin, @TelepAdmin)";


                SqliteCommand cmd = new SqliteCommand(szöveg);
                //     cmd.Parameters.AddWithValue("@UserId", Adat.UserId);
                cmd.Parameters.AddWithValue("@UserName", Adat.UserName);
                cmd.Parameters.AddWithValue("@WinUserName", Adat.WinUserName);
                cmd.Parameters.AddWithValue("@Dolgozószám", Adat.Dolgozószám);
                cmd.Parameters.AddWithValue("@Password", Adat.Password);
                cmd.Parameters.AddWithValue("@Dátum", Adat.Dátum);
                cmd.Parameters.AddWithValue("@Frissít", frissít);
                cmd.Parameters.AddWithValue("@Törölt", Adat.Törölt);
                cmd.Parameters.AddWithValue("@Szervezetek", Adat.Szervezetek);
                cmd.Parameters.AddWithValue("@Szervezet", Adat.Szervezet);
                cmd.Parameters.AddWithValue("@GlobalAdmin", Adat.GlobalAdmin);
                cmd.Parameters.AddWithValue("@TelepAdmin", Adat.TelepAdmin);

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

        public void Módosítás(Adat_Users Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $@"UserName=@UserName, ";
                szöveg += $@"WinUserName=@WinUserName, ";
                szöveg += $@"Dolgozószám=@Dolgozószám, ";
                szöveg += $@"Password=@Password, ";
                szöveg += $@"Dátum=@Dátum, ";
                szöveg += $@"Frissít=@Frissít, ";
                szöveg += $@"Törölt=@Törölt, ";
                szöveg += $@"Szervezetek=@Szervezetek, ";
                szöveg += $@"Szervezet=@Szervezet, ";
                szöveg += $@"GlobalAdmin=@GlobalAdmin, ";
                szöveg += $@"TelepAdmin=@TelepAdmin, ";
                szöveg += $@"WHERE UserId=@UserId;";

                SqliteCommand cmd = new SqliteCommand(szöveg);

                cmd.Parameters.AddWithValue("@UserId", Adat.UserId);
                cmd.Parameters.AddWithValue("@UserName", Adat.UserName);
                cmd.Parameters.AddWithValue("@WinUserName", Adat.WinUserName);
                cmd.Parameters.AddWithValue("@Dolgozószám", Adat.Dolgozószám);
                cmd.Parameters.AddWithValue("@Password", Adat.Password);
                cmd.Parameters.AddWithValue("@Dátum", Adat.Dátum);
                cmd.Parameters.AddWithValue("@Frissít", Adat.Frissít);
                cmd.Parameters.AddWithValue("@Törölt", Adat.Törölt);
                cmd.Parameters.AddWithValue("@Szervezetek", Adat.Szervezetek);
                cmd.Parameters.AddWithValue("@Szervezet", Adat.Szervezet);
                cmd.Parameters.AddWithValue("@GlobalAdmin", Adat.GlobalAdmin);
                cmd.Parameters.AddWithValue("@TelepAdmin", Adat.TelepAdmin);

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

        public void Döntés(Adat_Users Adat)
        {
            try
            {
                List<Adat_Users> Adatok = Lista_Adatok();
                if (!Adatok.Any(a => a.UserId == Adat.UserId))
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

        public void MódosításJeszó(Adat_Users Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $@"Password=@Password, ";
                szöveg += $@"Dátum=@Dátum, ";
                szöveg += $@"Frissít=@Frissít, ";
                szöveg += $@"WHERE UserId=@UserId;";

                SqliteCommand cmd = new SqliteCommand(szöveg);

                cmd.Parameters.AddWithValue("@UserId", Adat.UserId);
                cmd.Parameters.AddWithValue("@Password", Adat.Password);
                cmd.Parameters.AddWithValue("@Dátum", Adat.Dátum);
                cmd.Parameters.AddWithValue("@Frissít", Adat.Frissít);

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
