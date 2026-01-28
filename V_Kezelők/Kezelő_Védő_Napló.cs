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
    public class Kezelő_Védő_Napló
    {
        readonly string jelszó = "csavarhúzó";
        string hely;

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Védő\védőnapló{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Védőlistanapló(hely.KönyvSzerk());
        }

        public List<Adat_Védő_Napló> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = $"SELECT * FROM lista order by azonosító";
            List<Adat_Védő_Napló> Adatok = new List<Adat_Védő_Napló>();
            Adat_Védő_Napló Adat;

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
                                Adat = new Adat_Védő_Napló(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Honnan"].ToStrTrim(),
                                        rekord["Hova"].ToStrTrim(),
                                        rekord["mennyiség"].ToÉrt_Double(),
                                        rekord["gyáriszám"].ToStrTrim(),
                                        rekord["Módosította"].ToStrTrim(),
                                        rekord["Módosításidátum"].ToÉrt_DaTeTime(),
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

        public void Rögzítés(string Telephely, int Év, Adat_Védő_Napló Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"INSERT INTO lista (Azonosító, honnan, hova, Mennyiség, gyáriszám, státus, módosította, módosításidátum ) VALUES (";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"'{Adat.Honnan}', ";
                szöveg += $"'{Adat.Hova}', ";
                szöveg += $"{Adat.Mennyiség}, ";
                szöveg += $"'{Adat.Gyáriszám}', ";
                szöveg += $"{Adat.Státus} , ";
                szöveg += $"'{Adat.Módosította}', ";
                szöveg += $"'{Adat.Módosításidátum}') ";

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
