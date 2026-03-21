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
    public class Sql_Kezelő_Átöltés
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\SQL\MűködésiTáblák.db";
        readonly string jelszó = "VivaTv";
        readonly string táblanév = "Tbl_MdbKész";

        public Sql_Kezelő_Átöltés()
        {
            if (!File.Exists(hely)) Tábla_Létrehozás();
        }

        public void Tábla_Létrehozás()
        {
            try
            {
                //A sqlite adatbázisban a következő táblát hoztuk létre, hogy a mdb fájlok adatait tároljuk:
                string szöveg = $"CREATE TABLE IF NOT EXISTS {táblanév} (Id INTEGER PRIMARY KEY AUTOINCREMENT, Fájl TEXT, Jelszó TEXT, Tábla TEXT, Törölt INTEGER);";
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

        public List<Sql_Működés> Lista_Adatok()
        {
            List<Sql_Működés> Adatok = new List<Sql_Működés>();
            try
            {
                Adatok = MyA.Lista_Adatok(hely, jelszó, táblanév, rekord => new Sql_Működés(
                          rekord["Id"].ToÉrt_Int(),
                          rekord["Fájl"].ToString(),
                          rekord["Jelszó"].ToString(),
                          rekord["Tábla"].ToString(),
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


        public void Módosítás(Sql_Működés Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $@"Fájl=@Fájl, ";
                szöveg += $@"Jelszó=@Jelszó, ";
                szöveg += $@"Tábla=@Tábla ";
                szöveg += $@"Törölt=@Törölt ";
                szöveg += $@"WHERE id=@Id;";

                SqliteCommand cmd = new SqliteCommand(szöveg);

                cmd.Parameters.AddWithValue("@id", Adat.Id);
                cmd.Parameters.AddWithValue("@Fájl", Adat.Fájl);
                cmd.Parameters.AddWithValue("@Jelszó", Adat.Jelszó);
                cmd.Parameters.AddWithValue("@Tábla", Adat.Tábla);
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


        public void Rögzítés(Sql_Működés Adat)
        {
            try
            {
                string szöveg = $"INSERT  INTO {táblanév} ( Fájl, Jelszó, Tábla, Törölt) VALUES ";
                szöveg += @"( @Fájl, @Jelszó, @Tábla,@Törölt )";

                SqliteCommand cmd = new SqliteCommand(szöveg);

                // cmd.Parameters.AddWithValue("@id", Adat.Id); PRIMARY KEY("Id" AUTOINCREMENT) miatt nem kell megadni az id értékét, azt a sqlite automatikusan kezeli
                cmd.Parameters.AddWithValue("@Fájl", Adat.Fájl);
                cmd.Parameters.AddWithValue("@Jelszó", Adat.Jelszó);
                cmd.Parameters.AddWithValue("@Tábla", Adat.Tábla);
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

        public void Döntés(Sql_Működés Adat)
        {
            try
            {
                List<Sql_Működés> Adatok = Lista_Adatok();
                Sql_Működés Elem = (from a in Adatok
                                    where a.Fájl == Adat.Fájl
                                    && a.Tábla == Adat.Fájl
                                    select a).FirstOrDefault();
                if (Elem == null)
                {
                    Rögzítés(Adat);
                }
                else
                {
                    //Nincs értelme a módosításnak, hiszen a fájl és a tábla értékek alapján már létezik egy ilyen rekord, de a jelszó értékét meg akarjuk
                    //megváltoztatni.
                    //  Módosítás(Adat);
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
    }
}
