using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Rezsi_Hely
    {
        readonly string jelszó = "csavarhúzó";
        readonly string Táblanév = "tábla";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Rezsi\rezsihely.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Rezsihely(hely.KönyvSzerk());
        }

        public List<Adat_Rezsi_Hely> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = $"SELECT * FROM {Táblanév} Order by azonosító";
            List<Adat_Rezsi_Hely> Adatok = new List<Adat_Rezsi_Hely>();
            Adat_Rezsi_Hely Adat;

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
                                Adat = new Adat_Rezsi_Hely(
                                       rekord["Azonosító"].ToStrTrim(),
                                       rekord["Állvány"].ToStrTrim(),
                                       rekord["Polc"].ToStrTrim(),
                                       rekord["Helyiség"].ToStrTrim(),
                                       rekord["Megjegyzés"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Rezsi_Hely Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"INSERT INTO {Táblanév} (azonosító, helyiség, állvány, polc, megjegyzés) VALUES (";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"'{Adat.Helyiség}', ";
                szöveg += $"'{Adat.Állvány}', ";
                szöveg += $"'{Adat.Polc}', ";
                szöveg += $"'{Adat.Megjegyzés}') ";

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

        public void Módosítás(string Telephely, Adat_Rezsi_Hely Adat)
        {
            try
            {
                FájlBeállítás(Telephely);

                string szöveg = $"UPDATE {Táblanév}  SET ";
                szöveg += $"Helyiség='{Adat.Helyiség}', ";
                szöveg += $"Állvány='{Adat.Állvány}', ";
                szöveg += $"polc='{Adat.Polc}', ";
                szöveg += $"Megjegyzés='{Adat.Megjegyzés}' ";
                szöveg += $" WHERE azonosító='{Adat.Azonosító}'";

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
