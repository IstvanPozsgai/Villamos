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
    public class Kezelő_T5C5_Kmadatok_Napló
    {
        readonly string jelszó = "pocsaierzsi";
        string hely;

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Napló\2021Kmnapló{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.KmfutástáblaNapló(hely.KönyvSzerk());
        }

        public List<Adat_T5C5_Kmadatok_Napló> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = "SELECT * FROM kmtáblaNapló";
            List<Adat_T5C5_Kmadatok_Napló> Adatok = new List<Adat_T5C5_Kmadatok_Napló>();
            Adat_T5C5_Kmadatok_Napló Adat;

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
                                Adat = new Adat_T5C5_Kmadatok_Napló(
                                    rekord["ID"].ToÉrt_Long(),
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["jjavszám"].ToÉrt_Long(),
                                    rekord["KMUkm"].ToÉrt_Long(),
                                    rekord["KMUdátum"].ToÉrt_DaTeTime(),

                                    rekord["vizsgfok"].ToStrTrim(),
                                    rekord["vizsgdátumk"].ToÉrt_DaTeTime(),
                                    rekord["vizsgdátumv"].ToÉrt_DaTeTime(),
                                    rekord["vizsgkm"].ToÉrt_Long(),
                                    rekord["havikm"].ToÉrt_Long(),

                                    rekord["vizsgsorszám"].ToÉrt_Long(),
                                    rekord["fudátum"].ToÉrt_DaTeTime(),
                                    rekord["Teljeskm"].ToÉrt_Long(),
                                    rekord["Ciklusrend"].ToStrTrim(),
                                    rekord["V2végezte"].ToStrTrim(),

                                    rekord["KövV2_sorszám"].ToÉrt_Long(),
                                    rekord["KövV2"].ToStrTrim(),
                                    rekord["KövV_sorszám"].ToÉrt_Long(),
                                    rekord["KövV"].ToStrTrim(),
                                    rekord["törölt"].ToÉrt_Bool(),

                                    rekord["V2V3Számláló"].ToÉrt_Long(),
                                    rekord["Módosító"].ToStrTrim(),
                                    rekord["Mikor"].ToÉrt_DaTeTime()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(int Év, Adat_T5C5_Kmadatok Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = "INSERT INTO kmtáblaNapló  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt, Módosító, Mikor) VALUES (";
                szöveg += $"{Adat.ID}, ";                      // ID
                szöveg += $"'{Adat.Azonosító}',";              // azonosító
                szöveg += $"{Adat.Jjavszám}, ";                // jjavszám
                szöveg += $"{Adat.KMUkm}, ";                   // KMUkm
                szöveg += $"'{Adat.KMUdátum:yyyy.MM.dd}', ";   // KMUdátum

                szöveg += $"'{Adat.Vizsgfok}', ";                 //vizsgfok
                szöveg += $"'{Adat.Vizsgdátumk:yyyy.MM.dd}', ";   //vizsgdátumk
                szöveg += $"'{Adat.Vizsgdátumv:yyyy.MM.dd}', ";   //vizsgdátumv

                szöveg += $"{Adat.Vizsgkm}, ";               //vizsgkm
                szöveg += $"{Adat.Havikm}, ";                //havikm
                szöveg += $"{Adat.Vizsgsorszám}, ";          //vizsgsorszám
                szöveg += $"'{Adat.Fudátum:yyyy.MM.dd}', "; //fudátum

                szöveg += $"{Adat.Teljeskm}, ";         //Teljeskm
                szöveg += $"'{Adat.Ciklusrend}', ";     // Ciklusrend
                szöveg += $"'{Adat.V2végezte}', ";      // V2végezte
                szöveg += $"{Adat.KövV2_sorszám}, ";    // KövV2_Sorszám
                szöveg += $"'{Adat.KövV2}', ";          //KövV2,

                szöveg += $"{Adat.KövV_sorszám}, "; //KövV_Sorszám
                szöveg += $"'{Adat.KövV}', ";       //KövV
                szöveg += $"{Adat.V2V3Számláló}, "; //V2V3Számláló
                szöveg += $"{Adat.Törölt}, ";       //törölt
                szöveg += $"'{Program.PostásNév}', ";   //Módosító
                szöveg += $"'{DateTime.Now}')";       //Mikor

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

        //elkopó
        public List<Adat_T5C5_Kmadatok_Napló> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Kmadatok_Napló> Adatok = new List<Adat_T5C5_Kmadatok_Napló>();
            Adat_T5C5_Kmadatok_Napló Adat;

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
                                Adat = new Adat_T5C5_Kmadatok_Napló(
                                    rekord["ID"].ToÉrt_Long(),
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["jjavszám"].ToÉrt_Long(),
                                    rekord["KMUkm"].ToÉrt_Long(),
                                    rekord["KMUdátum"].ToÉrt_DaTeTime(),

                                    rekord["vizsgfok"].ToStrTrim(),
                                    rekord["vizsgdátumk"].ToÉrt_DaTeTime(),
                                    rekord["vizsgdátumv"].ToÉrt_DaTeTime(),
                                    rekord["vizsgkm"].ToÉrt_Long(),
                                    rekord["havikm"].ToÉrt_Long(),

                                    rekord["vizsgsorszám"].ToÉrt_Long(),
                                    rekord["fudátum"].ToÉrt_DaTeTime(),
                                    rekord["Teljeskm"].ToÉrt_Long(),
                                    rekord["Ciklusrend"].ToStrTrim(),
                                    rekord["V2végezte"].ToStrTrim(),

                                    rekord["KövV2_sorszám"].ToÉrt_Long(),
                                    rekord["KövV2"].ToStrTrim(),
                                    rekord["KövV_sorszám"].ToÉrt_Long(),
                                    rekord["KövV"].ToStrTrim(),
                                    rekord["törölt"].ToÉrt_Bool(),

                                    rekord["V2V3Számláló"].ToÉrt_Long(),
                                    rekord["Módosító"].ToStrTrim(),
                                    rekord["Mikor"].ToÉrt_DaTeTime()
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
