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
    public class Kezelő_Vezénylés
    {
        readonly string jelszó = "tápijános";
        string hely;

        private void FájlBeállítás(string Telephely, DateTime Dátum)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\főkönyv\futás\{Dátum.Year}\vezénylés{Dátum.Year}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Vezényléstábla(hely.KönyvSzerk());
        }

        public List<Adat_Vezénylés> Lista_Adatok(string Telephely, DateTime Dátum)
        {
            FájlBeállítás(Telephely, Dátum);
            string szöveg = "SELECT * FROM vezényléstábla";
            List<Adat_Vezénylés> Adatok = new List<Adat_Vezénylés>();


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
                                Adat_Vezénylés Adat = new Adat_Vezénylés(
                                        rekord["azonosító"].ToStrTrim(),
                                        DateTime.Parse(rekord["dátum"].ToString()),
                                        rekord["státus"].ToÉrt_Int(),
                                        rekord["vizsgálatraütemez"].ToÉrt_Int(),
                                        rekord["takarításraütemez"].ToÉrt_Int(),
                                        rekord["vizsgálat"].ToStrTrim(),
                                        rekord["vizsgálatszám"].ToÉrt_Int(),
                                        rekord["rendelésiszám"].ToStrTrim(),
                                        rekord["álljon"].ToÉrt_Int(),
                                        rekord["fusson"].ToÉrt_Int(),
                                        rekord["törlés"].ToÉrt_Int(),
                                        rekord["szerelvényszám"].ToÉrt_Long(),
                                        rekord["típus"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, DateTime Dátum, Adat_Vezénylés Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                string szöveg = "INSERT INTO vezényléstábla ";
                szöveg += "(azonosító, Dátum, Státus, vizsgálatraütemez, takarításraütemez, vizsgálat, vizsgálatszám, rendelésiszám, törlés, szerelvényszám, fusson, álljon, típus) VALUES (";
                szöveg += $"'{Adat.Azonosító}',";
                szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', ";
                szöveg += $"{Adat.Státus}, ";
                szöveg += $"{Adat.Vizsgálatraütemez}, ";
                szöveg += $"{Adat.Takarításraütemez}, ";
                szöveg += $"'{Adat.Vizsgálat}', ";
                szöveg += $"{Adat.Vizsgálatszám}, ";
                szöveg += $"'{Adat.Rendelésiszám}', ";
                szöveg += $"{Adat.Törlés}, ";
                szöveg += $"{Adat.Szerelvényszám}, ";
                szöveg += $"{Adat.Fusson}, ";
                szöveg += $"{Adat.Álljon}, ";
                szöveg += $"'{Adat.Típus}')";

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

        public void Módosítás(string Telephely, DateTime Dátum, Adat_Vezénylés Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                string szöveg = "UPDATE vezényléstábla SET ";
                szöveg += $" Státus={Adat.Státus}, ";
                szöveg += $" vizsgálatraütemez={Adat.Vizsgálatraütemez}, ";
                szöveg += $" takarításraütemez={Adat.Takarításraütemez}, ";
                szöveg += $" vizsgálat ='{Adat.Vizsgálat}', ";
                szöveg += $" vizsgálatszám={Adat.Vizsgálatszám}, ";
                szöveg += $" rendelésiszám='{Adat.Rendelésiszám}', ";
                szöveg += $" szerelvényszám={Adat.Szerelvényszám} ";
                szöveg += $" WHERE [azonosító] ='{Adat.Azonosító}' AND [dátum]=#{Adat.Dátum:M-d-yy}#";
                szöveg += " AND [törlés]=0";
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

        public void Módosítás(string Telephely, DateTime Dátum, string azonosító, DateTime dátum2)
        {
            try
            {
                FájlBeállítás(Telephely, Dátum);
                string szöveg = "UPDATE vezényléstábla SET törlés=1 ";
                szöveg += $" WHERE [azonosító] ='{azonosító.Trim()}' AND [dátum]=#{dátum2:M-d-yy}#";
                szöveg += " AND [törlés]=0";
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




        // Elkopó
        public List<Adat_Vezénylés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Vezénylés> Adatok = new List<Adat_Vezénylés>();

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
                                Adat_Vezénylés Adat = new Adat_Vezénylés(
                                        rekord["azonosító"].ToStrTrim(),
                                        DateTime.Parse(rekord["dátum"].ToString()),
                                        rekord["státus"].ToÉrt_Int(),
                                        rekord["vizsgálatraütemez"].ToÉrt_Int(),
                                        rekord["takarításraütemez"].ToÉrt_Int(),
                                        rekord["vizsgálat"].ToStrTrim(),
                                        rekord["vizsgálatszám"].ToÉrt_Int(),
                                        rekord["rendelésiszám"].ToStrTrim(),
                                        rekord["álljon"].ToÉrt_Int(),
                                        rekord["fusson"].ToÉrt_Int(),
                                        rekord["törlés"].ToÉrt_Int(),
                                        rekord["szerelvényszám"].ToÉrt_Long(),
                                        rekord["típus"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


    }
}
