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
    public class SQL_Kezelő_Rezsi_Napló
    {
        readonly public string jelszó = "CsavarHúzó";
        readonly public string táblanév = "Tbl_Rezsi_Napló";
        public string hely;

        public void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\SQL\Rezsi\{Telephely}\Rezsinapló{Év}.db";
            if (!File.Exists(hely.KönyvSzerk())) Tábla_Létrehozás();
        }

        private void Tábla_Létrehozás()
        {
            try
            {
                //A sqlite adatbázisban a következő táblát hoztuk létre
                //
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

        public List<Adat_Rezsi_Listanapló> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            List<Adat_Rezsi_Listanapló> Adatok = new List<Adat_Rezsi_Listanapló>();
            try
            {
                Adatok = MyA.Lista_Adatok(hely, jelszó, táblanév, rekord => new Adat_Rezsi_Listanapló(
                         rekord["Azonosító"].ToStrTrim(),
                         rekord["Honnan"].ToStrTrim(),
                         rekord["Hova"].ToStrTrim(),
                         rekord["Mennyiség"].ToÉrt_Double(),
                         rekord["Mirehasznál"].ToStrTrim(),
                         rekord["Módosította"].ToStrTrim(),
                         rekord["módosításidátum"].ToÉrt_DaTeTime(),
                         rekord["Státus"].ToÉrt_Bool()
                         )).OrderBy(a => a.Azonosító).ToList();
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

        public void Rögzítés(string Telephely, int Év, Adat_Rezsi_Listanapló Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"INSERT INTO {táblanév} (Azonosító, honnan, hova, mennyiség, státus, módosította, mirehasznál, módosításidátum) VALUES ";
                szöveg += $@"(@Azonosító, @honnan, @hova, @mennyiség, @státus, @módosította, @mirehasznál, @módosításidátum)";

                SqliteCommand cmd = new SqliteCommand(szöveg);

                cmd.Parameters.AddWithValue("@Azonosító", Adat.Azonosító);
                cmd.Parameters.AddWithValue("@honnan", Adat.Honnan);
                cmd.Parameters.AddWithValue("@hova", Adat.Hova);
                cmd.Parameters.AddWithValue("@mennyiség", Adat.Mennyiség);
                cmd.Parameters.AddWithValue("@státus", Adat.Státus);
                cmd.Parameters.AddWithValue("@módosította", Adat.Módosította);
                cmd.Parameters.AddWithValue("@mirehasznál", Adat.Mirehasznál);
                cmd.Parameters.AddWithValue("@módosításidátum", Adat.Módosításidátum);

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
