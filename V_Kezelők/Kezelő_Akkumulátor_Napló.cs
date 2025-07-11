﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Akkumulátor_Napló
    {
        string hely;
        readonly string jelszó = "kasosmiklós";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Akkumulátor\Akkunapló{Év}.mdb".KönyvSzerk();
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Akku_Mérés(hely);
        }

        public List<Adat_Akkumulátor_Napló> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
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

        public void Rögzítés(int Év, Adat_Akkumulátor_Napló Adat)
        {
            try
            {
                FájlBeállítás(Év);
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
                szöveg += $"'{Adat.Megjegyzés}', "; //MegjegyzésVáltozó,
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
