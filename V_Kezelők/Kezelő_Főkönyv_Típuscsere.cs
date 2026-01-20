using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Főkönyv_Típuscsere
    {
        readonly string jelszó = "plédke";
        string hely;
        readonly string táblanév = "típuscseretábla";

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely.Trim()}\Adatok\főkönyv\típuscsere{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Tipuscsereösszesítőtábla(hely.KönyvSzerk());
        }

        public List<Adat_FőKönyv_Típuscsere> Lista_Adatok(string Telephely, int Év)
        {
            string szöveg = $"SELECT * FROM {táblanév}";
            FájlBeállítás(Telephely, Év);
            List<Adat_FőKönyv_Típuscsere> Adatok = new List<Adat_FőKönyv_Típuscsere>();
            Adat_FőKönyv_Típuscsere Adat;

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
                                Adat = new Adat_FőKönyv_Típuscsere(
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["típuselőírt"].ToStrTrim(),
                                    rekord["típuskiadott"].ToStrTrim(),
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["tervindulás"].ToÉrt_DaTeTime(),
                                    rekord["azonosító"].ToStrTrim(),
                                    rekord["kocsi"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Törlés(string Telephely, int Év, string Napszak, DateTime Dátum)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"DELETE FROM {táblanév} where dátum=#{Dátum:MM-dd-yyyy}#";
                szöveg += $" and napszak='{Napszak}'";
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

        public void Rögzítés(string Telephely, int Év, Adat_FőKönyv_Típuscsere Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"INSERT INTO {táblanév} (dátum, napszak, típuselőírt, típuskiadott, viszonylat, forgalmiszám, tervindulás, azonosító, kocsi) VALUES (";
                szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', ";
                szöveg += $"'{Adat.Napszak}', ";
                szöveg += $"'{Adat.Típuselőírt}', ";
                szöveg += $"'{Adat.Típuskiadott}', ";
                szöveg += $"'{Adat.Viszonylat}', ";
                szöveg += $"'{Adat.Forgalmiszám}', ";
                szöveg += $"'{Adat.Tervindulás}', ";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"'{Adat.Kocsi}') ";
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
