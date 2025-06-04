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
    public class Kezelők_Users
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ÚJ_Belépés.mdb";
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tábla_Users";

        public Kezelők_Users()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Adatbázis_Users(hely.KönyvSzerk());
            if (!AdatBázis_kezelés.TáblaEllenőrzés(hely, jelszó, táblanév)) Adatbázis_Létrehozás.Adatbázis_Users(hely);
        }

        public List<Adat_Users> Lista_Adatok()
        {
            List<Adat_Users> Adatok = new List<Adat_Users>();
            string szöveg = $"SELECT * FROM {táblanév}";
            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";

            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat))
                {
                    Kapcsolat.Open();
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {
                        if (rekord.HasRows)
                        {
                            while (rekord.Read())
                            {
                                Adat_Users Adat = new Adat_Users(
                                        rekord["UserId"].ToÉrt_Int(),
                                        rekord["UserName"].ToStrTrim(),
                                        rekord["WinUserName"].ToStrTrim(),
                                        rekord["Dolgozószám"].ToStrTrim(),
                                        rekord["Password"].ToStrTrim(),
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Frissít"].ToÉrt_Bool(),
                                        rekord["Törölt"].ToÉrt_Bool(),
                                        rekord["Szervezetek"].ToStrTrim()
                                );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
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

        public void Rögzítés(Adat_Users Adat)
        {
            try
            {
                string pword = Adat.Password;
                if (Adat.Password.Trim() == "") pword = "123456";
                bool frissít = true;
                string szöveg = $"INSERT INTO {táblanév} (UserName, WinUserName, Dolgozószám, [Password], Dátum, frissít, Törölt, Szervezetek) VALUES (";
                szöveg += $"'{Adat.UserName}', '{Adat.WinUserName}', '{Adat.Dolgozószám}', '{pword}', '{Adat.Dátum:yyyy.MM.dd}', {frissít}, {Adat.Törölt}, '{Adat.Szervezetek}')";
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

        public void Módosítás(Adat_Users Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"WinUserName ='{Adat.WinUserName}', ";
                szöveg += $"[Password] ='{Adat.Password}', ";
                szöveg += $"Dátum ='{Adat.Dátum:yyyy.MM.dd}', ";
                szöveg += $"Frissít ={Adat.Frissít}, ";
                szöveg += $"Törölt ={Adat.Törölt}, ";
                szöveg += $"Szervezetek ='{Adat.Szervezetek}' ";
                szöveg += $"WHERE UserId = {Adat.UserId}";
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
    }
}
