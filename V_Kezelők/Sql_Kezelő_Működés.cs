using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Sql_Kezelő_Működés
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\SQL\MűködésiTáblák.db";
        readonly string jelszó = "VivaTv";
        readonly string táblanév = "Tbl_Muk";

        public Sql_Kezelő_Működés()
        {

        }

        public List<Sql_Működés> Lista_Adatok()
        {
            List<Sql_Működés> Adatok = new List<Sql_Működés>();
            try
            {
                Adatok = MyA.Lista_Adatok(hely, jelszó, táblanév, rekord => new Sql_Működés(
                          rekord["Id"].ToÉrt_Int(),
                          rekord["Fájl"].ToString(),
                          rekord["Jelszó"].ToString(),
                          rekord["Tábla"].ToString()));
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


        public void Módosítás(Sql_Működés Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $@"Fájl=@Fájl, ";
                szöveg += $@"Jelszó=@Jelszó, ";
                szöveg += $@"Tábla=@Tábla ";
                szöveg += $@"WHERE id=@Id;";

                SqliteCommand cmd = new SqliteCommand(szöveg);

                cmd.Parameters.AddWithValue("@id", Adat.Id);
                cmd.Parameters.AddWithValue("@Fájl", Adat.Fájl);
                cmd.Parameters.AddWithValue("@Jelszó", Adat.Jelszó);
                cmd.Parameters.AddWithValue("@Tábla", Adat.Tábla);
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


        public void Rögzítés(Sql_Működés Adat)
        {
            try
            {
                string szöveg = $"INSERT  INTO {táblanév} (id, Fájl, Jelszó, Tábla) VALUES ";
                szöveg += @"(@id, @Fájl, @Jelszó, @Tábla)";

                SqliteCommand cmd = new SqliteCommand(szöveg);

                // cmd.Parameters.AddWithValue("@id", Adat.Id); PRIMARY KEY("Id" AUTOINCREMENT) miatt nem kell megadni az id értékét, azt a sqlite automatikusan kezeli
                cmd.Parameters.AddWithValue("@Fájl", Adat.Fájl);
                cmd.Parameters.AddWithValue("@Jelszó", Adat.Jelszó);
                cmd.Parameters.AddWithValue("@Tábla", Adat.Tábla);
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
