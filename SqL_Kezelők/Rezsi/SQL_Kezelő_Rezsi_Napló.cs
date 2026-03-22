using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class SQL_Kezelő_Rezsi_Napló : IKezelőAlap<Adat_Rezsi_Listanapló>
    {
        string hely;

        public string Jelszó { get; } = "CsavarHúzó";

        public string Táblanév { get; } = "Tbl_Rezsi_Napló";

        public string Hely { get; set; }

        public void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\SQL\Rezsi\{Telephely}\Rezsinapló{Év}.db";
            if (!File.Exists(hely.KönyvSzerk())) Tábla_Létrehozás();
            Hely = hely;
        }

        public void Tábla_Létrehozás()
        {
            try
            {
                //A sqlite adatbázisban a következő táblát hoztuk létre
                //
                string szöveg = $@"CREATE TABLE {Táblanév} (
                                Azonosító TEXT,
                            	Honnan TEXT,
                            	Hova  TEXT,
                            	Mennyiség REAL,
                            	Mirehasznál   TEXT,
                            	Módosította   TEXT,
                            	Módosításidátum   TEXT,
                            	Státus    INTEGER);";
                MyA.SqLite_TáblaLétrehozás(hely.KönyvSzerk(), Jelszó, szöveg);
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
                Adatok = MyA.Lista_Adatok(hely, Jelszó, Táblanév, rekord => new Adat_Rezsi_Listanapló(
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
                string szöveg = $"INSERT INTO {Táblanév} (Azonosító, honnan, hova, mennyiség, státus, módosította, mirehasznál, módosításidátum) VALUES ";
                szöveg += $@"(@Azonosító, @honnan, @hova, @mennyiség, @státus, @módosította, @mirehasznál, @módosításidátum)";

                string oszlopok = string.Join(", ", tulajdonsagok.Select(p => p.Name));
                // Pl: @Azonosító, @Honnan, @Hova...
                string parameterek = string.Join(", ", tulajdonsagok.Select(p => "@" + p.Name));

                string sql = $"INSERT INTO {Táblanév} ({oszlopok}) VALUES ({parameterek});";


                using (SqliteCommand cmd = new SqliteCommand(szöveg))
                {
                    // EGYETLEN SOR az összes paraméter helyett:
                    Kisegítő. ParaméterekHozzáadása(cmd, Adat);

                    MyA.SqLite_Módosítás(hely, Jelszó, cmd);
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
