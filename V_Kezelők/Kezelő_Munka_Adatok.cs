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
    public class Kezelő_Munka_Adatok
    {
        readonly string jelszó = "dekádoló";
        string hely;

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\Munkalap\munkalapelszámoló_{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Munkalapévestábla(hely.KönyvSzerk());
        }

        public List<Adat_Munka_Adatok> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = $"SELECT * FROM Adatoktábla";
            List<Adat_Munka_Adatok> Adatok = new List<Adat_Munka_Adatok>();
            Adat_Munka_Adatok Adat;

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
                                Adat = new Adat_Munka_Adatok(
                                          rekord["ID"].ToÉrt_Long(),
                                          rekord["idő"].ToÉrt_Int(),
                                          rekord["dátum"].ToÉrt_DaTeTime(),
                                          rekord["megnevezés"].ToString(),
                                          rekord["művelet"].ToString(),
                                          rekord["pályaszám"].ToString(),
                                          rekord["rendelés"].ToString(),
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

        public void Rögzítés(string Telephely, int Év, List<Adat_Munka_Adatok> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<string> szövegGy = new List<string>();
                foreach (Adat_Munka_Adatok adat in Adatok)
                {
                    string szöveg = $"INSERT INTO Adatoktábla (rendelés, művelet, megnevezés, pályaszám, idő, dátum, státus) VALUES (";
                    szöveg += $"'{adat.Rendelés}', ";
                    szöveg += $"'{adat.Művelet}', ";
                    szöveg += $"'{adat.Megnevezés}', ";
                    szöveg += $"'{adat.Pályaszám}', ";
                    szöveg += $"{adat.Idő}, ";
                    szöveg += $"'{adat.Dátum:yyyy.MM.dd}', ";
                    szöveg += $"{adat.Státus}) ";
                    szövegGy.Add(szöveg);
                }

                MyA.ABMódosítás(hely, jelszó, szövegGy);

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

        public void Módosítás(string Telephely, int Év, string rendelés, int Id)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"UPDATE Adatoktábla SET rendelés='{rendelés}' WHERE id={Id}";
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

        public void Módosítás(string Telephely, int Év, List<long> idk)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<string> szövegGy = new List<string>();
                foreach (long elem in idk)
                {
                    string szöveg = $"UPDATE Adatoktábla SET státus=false WHERE id={elem}";
                    szövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, szövegGy);
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
