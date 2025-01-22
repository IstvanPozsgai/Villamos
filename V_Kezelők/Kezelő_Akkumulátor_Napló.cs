using System;
using System.Collections.Generic;
using System.Data.OleDb;

using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Akkumulátor_Napló
    {
        readonly string hely;
        readonly string jelszó = "kasosmiklós";

        public DateTime Dátum { get; private set; }

        public Kezelő_Akkumulátor_Napló(DateTime dátum)
        {
            Dátum = dátum;
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Akkumulátor\Akkunapló{Dátum.Year}.mdb";
        }

        public List<Adat_Akkumulátor_Napló> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM Akkutábla_Napló";
            List<Adat_Akkumulátor_Napló> Adatok = new List<Adat_Akkumulátor_Napló>();
            Adat_Akkumulátor_Napló Adat;

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
                                Adat = new Adat_Akkumulátor_Napló(
                                        rekord["Beépítve"].ToStrTrim(),
                                        rekord["Fajta"].ToStrTrim(),
                                        rekord["Gyártó"].ToStrTrim(),
                                        rekord["Gyáriszám"].ToStrTrim(),
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Garancia"].ToÉrt_DaTeTime(),
                                        rekord["Gyártásiidő"].ToÉrt_DaTeTime(),
                                        rekord["Státus"].ToÉrt_Int(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["Módosításdátuma"].ToÉrt_DaTeTime(),
                                        rekord["Kapacitás"].ToÉrt_Int(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Rögzítés"].ToÉrt_DaTeTime(),
                                        rekord["Rögzítő"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Akkumulátor_Napló Adat)
        {
            try
            {
                string szöveg = "INSERT INTO Akkutábla_Napló ";
                szöveg += "(beépítve, fajta, gyártó, Gyáriszám, típus, garancia, gyártásiidő, státus, Megjegyzés, Módosításdátuma, kapacitás, Telephely, Rögzítés, Rögzítő)";
                szöveg += " VALUES (";
                szöveg += $"'{Adat.Beépítve}', "; //beépítve       ,
                szöveg += $"'{Adat.Fajta}', "; //fajta,
                szöveg += $"'{Adat.Gyártó}', "; //gyártó,
                szöveg += $"'{Adat.Gyáriszám}', "; //Gyáriszám,
                szöveg += $"'{Adat.Típus}', "; //típus,
                szöveg += $"'{Adat.Garancia:yyyy.MM.dd}', "; //garancia,
                szöveg += $"'{Adat.Gyártásiidő:yyyy.MM.dd}', "; //gyártásiidő,
                szöveg += $"{Adat.Státus}, "; //státus,
                szöveg += $"'{Adat.Megjegyzés}', "; //Megjegyzés,
                szöveg += $"'{Adat.Módosításdátuma}', "; //Módosításdátuma,
                szöveg += $"{Adat.Kapacitás}, "; //kapacitás,
                szöveg += $"'{Adat.Telephely}', "; //Telephely
                szöveg += $"'{Adat.Rögzítés}', "; //Rögzítés,
                szöveg += $"'{Adat.Rögzítő}') "; //Rögzítő

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
