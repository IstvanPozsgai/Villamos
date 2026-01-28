using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_főkönyvtábla
    {
        readonly string jelszó = "Mocó";
        string hely;
        readonly string táblanév = "Főkönyvtábla";

        private bool FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\segéd\Kiegészítő.mdb";
            return File.Exists(hely);
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }


        public List<Adat_Kiegészítő_főkönyvtábla> Lista_Adatok(string Telephely)
        {
            List<Adat_Kiegészítő_főkönyvtábla> Adatok = new List<Adat_Kiegészítő_főkönyvtábla>();
            try
            {
                if (FájlBeállítás(Telephely))
                {
                    string szöveg = $"SELECT * FROM {táblanév}";

                    Adat_Kiegészítő_főkönyvtábla Adat;

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
                                        Adat = new Adat_Kiegészítő_főkönyvtábla(
                                                  rekord["id"].ToÉrt_Long(),
                                                  rekord["név"].ToStrTrim(),
                                                  rekord["beosztás"].ToStrTrim(),
                                                  rekord["email"].ToStrTrim());
                                        Adatok.Add(Adat);
                                    }
                                }
                            }
                        }
                    }
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
            return Adatok;
        }

        public void Módosítás(string Telephely, Adat_Kiegészítő_főkönyvtábla Adat)
        {
            try
            {
                if (FájlBeállítás(Telephely))
                {
                    string szöveg = $"UPDATE {táblanév} SET név='{Adat.Név}',";
                    szöveg += $" beosztás='{Adat.Beosztás}', ";
                    szöveg += $" email='{Adat.Email}'";
                    szöveg += $" WHERE id={Adat.Id} ";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
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
