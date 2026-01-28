using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
namespace Villamos.Kezelők
{
    public class Kezelő_Védő_Könyvelés
    {
        readonly string jelszó = "csavarhúzó";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Védő\védőkönyvelés.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Védőlista(hely);
        }

        public List<Adat_Védő_Könyvelés> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = $"SELECT * FROM lista WHERE státus=0 ORDER BY azonosító";
            List<Adat_Védő_Könyvelés> Adatok = new List<Adat_Védő_Könyvelés>();
            Adat_Védő_Könyvelés Adat;

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat))
                {
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {
                        if (rekord.HasRows)
                        {
                            while (rekord.Read())
                            {
                                Adat = new Adat_Védő_Könyvelés(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["szerszámkönyvszám"].ToStrTrim(),
                                        rekord["mennyiség"].ToÉrt_Double(),
                                        rekord["gyáriszám"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["státus"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }

                    }
                }
            }
            return Adatok;
        }


        public void Rögzítés(string Telephely, Adat_Védő_Könyvelés Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"INSERT INTO lista (Azonosító, Szerszámkönyvszám, Mennyiség, gyáriszám, dátum, státus) VALUES (";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"'{Adat.Szerszámkönyvszám}', ";
                szöveg += $"{Adat.Mennyiség}, ";
                szöveg += $"'{Adat.Gyáriszám}', ";
                szöveg += $"'{Adat.Dátum}', ";
                szöveg += $" {Adat.Státus})";
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


        public void Módosítás(string Telephely, Adat_Védő_Könyvelés Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE lista SET ";
                szöveg += $"Mennyiség={Adat.Mennyiség}, ";
                szöveg += $"gyáriszám='{Adat.Gyáriszám}', ";
                szöveg += $"dátum ='{Adat.Dátum}' ";
                szöveg += $" WHERE Azonosító='{Adat.Azonosító}' ";
                szöveg += $" AND Szerszámkönyvszám='{Adat.Szerszámkönyvszám}'";
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

        public void Törlés(string Telephely, Adat_Védő_Könyvelés Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg =$"DELETE FROM lista ";
                szöveg += $" WHERE Azonosító='{Adat.Azonosító}'";
                szöveg += $" AND Szerszámkönyvszám='{Adat.Szerszámkönyvszám}'";
                MyA.ABtörlés(hely, jelszó, szöveg);
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
