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
    public class Kezelő_T5C5_Kmadatok
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\T5C5\Villamos4T5C5.mdb";
        readonly string jelszó = "pocsaierzsi";

        public Kezelő_T5C5_Kmadatok()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kmfutástábla(hely.KönyvSzerk());
        }

        public List<Adat_T5C5_Kmadatok> Lista_Adat()
        {
            string szöveg = "SELECT * FROM kmtábla";
            List<Adat_T5C5_Kmadatok> Adatok = new List<Adat_T5C5_Kmadatok>();
            Adat_T5C5_Kmadatok Adat;

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
                                Adat = new Adat_T5C5_Kmadatok(
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

                                    rekord["V2V3Számláló"].ToÉrt_Long()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzít(Adat_T5C5_Kmadatok Adat)
        {
            try
            {
                string szöveg = "INSERT INTO kmtábla  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt) VALUES (";
                szöveg += $"{Adat.ID}, ";                        //ID
                szöveg += $"'{Adat.Azonosító}', ";               // azonosító
                szöveg += $"{Adat.Jjavszám}, ";                  // jjavszám
                szöveg += $"{Adat.KMUkm}, ";                     // KMUkm
                szöveg += $"'{Adat.KMUdátum:yyyy.MM.dd}', ";     //KMUdátum
                szöveg += $"'{Adat.Vizsgfok.Trim()}', ";         //vizsgfok
                szöveg += $"'{Adat.Vizsgdátumk:yyyy.MM.dd}', ";  //vizsgdátumk
                szöveg += $"'{Adat.Vizsgdátumv:yyyy.MM.dd}', ";  //vizsgdátumv
                szöveg += $"{Adat.Vizsgkm}, ";                //vizsgkm
                szöveg += $"{Adat.Havikm}, ";                 // havikm
                szöveg += $"{Adat.Vizsgsorszám}, ";           //vizsgsorszám
                szöveg += $"'{Adat.Fudátum:yyyy.MM.dd}', ";   //fudátum
                szöveg += $"{Adat.Teljeskm}, ";               //Teljeskm
                szöveg += $"'{Adat.Ciklusrend.Trim()}', ";    // Ciklusrend
                szöveg += $"'{Adat.V2végezte.Trim()}', ";     // V2végezte
                szöveg += $"{Adat.KövV2_sorszám}, ";          // KövV2_Sorszám
                szöveg += $"'{Adat.KövV2.Trim()}', ";         // KövV2
                szöveg += $"{Adat.KövV_sorszám}, ";           //KövV_Sorszám
                szöveg += $"'{Adat.KövV.Trim()}', ";          //KövV
                szöveg += $"{Adat.V2V3Számláló}, ";           //V2V3Számláló
                szöveg += $"{Adat.Törölt} )";                 //törölt

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

        public void Módosítás(long Id, long korrekció)
        {
            try
            {
                string szöveg = " UPDATE kmtábla SET ";
                szöveg += $" KMUkm={korrekció}";
                szöveg += $" WHERE id={Id}";
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


        //Elkopó

        public List<Adat_T5C5_Kmadatok> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Kmadatok> Adatok = new List<Adat_T5C5_Kmadatok>();
            Adat_T5C5_Kmadatok Adat;

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
                                Adat = new Adat_T5C5_Kmadatok(
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

                                    rekord["V2V3Számláló"].ToÉrt_Long()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_T5C5_Kmadatok Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_T5C5_Kmadatok Adat = null;

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
                            rekord.Read();

                            Adat = new Adat_T5C5_Kmadatok(
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

                                rekord["V2V3Számláló"].ToÉrt_Long()
                                ); ;
                        }
                    }
                }
            }
            return Adat;
        }

        public void Rögzít(string hely, string jelszó, Adat_T5C5_Kmadatok Rekord)
        {

            string szöveg = "INSERT INTO kmtábla  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
            szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
            szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
            szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
            szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt) VALUES (";
            szöveg += Rekord.ID + ", '" + Rekord.Azonosító + "', " + Rekord.Jjavszám + ", " + Rekord.KMUkm + ", '" + Rekord.KMUdátum.ToString("yyyy.MM.dd") + "', ";
            szöveg += "'" + Rekord.Vizsgfok.Trim() + "', '" + Rekord.Vizsgdátumk.ToString("yyyy.MM.dd") + "', '" + Rekord.Vizsgdátumv.ToString("yyyy.MM.dd") + "', ";
            szöveg += Rekord.Vizsgkm + ", " + Rekord.Havikm + ", " + Rekord.Vizsgsorszám + ", '" + Rekord.Fudátum.ToString("yyyy.MM.dd") + "', ";
            szöveg += Rekord.Teljeskm + ", '" + Rekord.Ciklusrend.Trim() + "', '" + Rekord.V2végezte.Trim() + "', " + Rekord.KövV2_sorszám + ", '" + Rekord.KövV2.Trim() + "', ";
            szöveg += Rekord.KövV_sorszám + ", '" + Rekord.KövV.Trim() + "', " + Rekord.V2V3Számláló + ", " + Rekord.Törölt + " )";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Módosít(string hely, string jelszó, Adat_T5C5_Kmadatok Rekord)
        {
            string szöveg = " UPDATE kmtábla SET ";
            szöveg += " Jjavszám=" + Rekord.Jjavszám + ", ";
            szöveg += " KMUkm=" + Rekord.KMUkm + ", ";
            szöveg += " KMUdátum='" + Rekord.KMUdátum.ToString("yyyy.MM.dd") + "', ";
            szöveg += " Vizsgfok='" + Rekord.Vizsgfok.Trim() + "', ";
            szöveg += " Vizsgdátumk='" + Rekord.Vizsgdátumk.ToString("yyyy.MM.dd") + "', ";
            szöveg += " Vizsgdátumv='" + Rekord.Vizsgdátumv.ToString("yyyy.MM.dd") + "', ";
            szöveg += " VizsgKm=" + Rekord.Vizsgkm + ", ";
            szöveg += " HaviKm=" + Rekord.Havikm + ", ";
            szöveg += " VizsgSorszám=" + Rekord.Vizsgsorszám + ", ";
            szöveg += " fudátum='" + Rekord.Fudátum.ToString("yyyy.MM.dd") + "', ";
            szöveg += " Teljeskm=" + Rekord.Teljeskm + ", ";
            szöveg += " Ciklusrend='" + Rekord.Ciklusrend.Trim() + "', ";
            szöveg += " V2végezte='" + Rekord.V2végezte.Trim() + "', ";
            szöveg += " KövV2_Sorszám=" + Rekord.KövV2_sorszám + ",  ";
            szöveg += " KövV2='" + Rekord.KövV2.Trim() + "', ";
            szöveg += " KövV_Sorszám=" + Rekord.KövV_sorszám + ", ";
            szöveg += " KövV='" + Rekord.KövV.Trim() + "', ";
            szöveg += " törölt=false, ";
            szöveg += " V2V3Számláló=" + Rekord.V2V3Számláló;
            szöveg += " WHERE id=" + Rekord.ID;
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public List<Adat_T5C5_Kmadatok> Lista_Szűrt_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Kmadatok> Adatok = new List<Adat_T5C5_Kmadatok>();
            Adat_T5C5_Kmadatok Adat;

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
                                Adat = new Adat_T5C5_Kmadatok(
                                    rekord["ID"].ToÉrt_Long(),
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["vizsgdátumk"].ToÉrt_DaTeTime()
                                    ); ;
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
