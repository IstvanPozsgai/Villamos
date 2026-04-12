using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{

    public class SQL_Kezelő_Belépés_Verzió
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\SQL\Belépés.db";
        readonly string jelszó = "ForgalmiUtasítás";
        readonly string táblanév = "Tbl_Bejelentkezés_Verzió";

        public SQL_Kezelő_Belépés_Verzió()
        {
            if (!File.Exists(hely)) Tábla_Létrehozás();
            if (!MyA.SqLite_ABvanTábla(hely, jelszó, táblanév)) Tábla_Létrehozás();
        }


        public void Tábla_Létrehozás()
        {
            try
            {
                string szöveg = $@"CREATE TABLE {táblanév} (
                                Id INTEGER, 
                                Verzió REAL
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


        public List<Adat_Belépés_Verzió> Lista_Adatok()
        {
            List<Adat_Belépés_Verzió> Adatok = new List<Adat_Belépés_Verzió>();
            try
            {
                Adatok = MyA.Lista_Adatok(hely, jelszó, táblanév, rekord => new Adat_Belépés_Verzió(
                                  rekord["id"].ToÉrt_Long(),
                                  rekord["Verzió"].ToÉrt_Double()));
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

        public void Rögzítés(Adat_Belépés_Verzió Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (Id, Verzió) VALUES ";
                szöveg += $@"(@Id, @Verzió)";


                SqliteCommand cmd = new SqliteCommand(szöveg);
                cmd.Parameters.AddWithValue("@Id", Adat.Id);
                cmd.Parameters.AddWithValue("@Verzió", Adat.Verzió);

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

        public void Módosítás(Adat_Belépés_Verzió Adat)
        {
            try
            {

                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $@"Verzió=@Verzió ";
                szöveg += $@"WHERE id=@Id;";

                SqliteCommand cmd = new SqliteCommand(szöveg);

                cmd.Parameters.AddWithValue("@Id", Adat.Id);
                cmd.Parameters.AddWithValue("@Verzió", Adat.Verzió);

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
