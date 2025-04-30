using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_T5C5_Kmadatok
    {
        readonly Kezelő_T5C5_Kmadatok_Napló KézT5C5Napló = new Kezelő_T5C5_Kmadatok_Napló();
        public string Típus { get; private set; }
        private string hely;
        readonly string jelszó = "pocsaierzsi";

        public Kezelő_T5C5_Kmadatok(string típus)
        {
            Típus = típus;
            if (Típus == "T5C5") hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\T5C5\Villamos4T5C5.mdb";
            if (Típus == "ICS") hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\ICSKCSV\Villamos4ICS.mdb";
            if (Típus == "Fogas") hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\villamos4Fogas.mdb";

            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kmfutástábla(hely.KönyvSzerk());
        }

        public List<Adat_T5C5_Kmadatok> Lista_Adatok()
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

        public void Rögzítés(Adat_T5C5_Kmadatok Adat)
        {
            try
            {
                string szöveg = "INSERT INTO kmtábla  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt) VALUES (";
                szöveg += $"{Sorszám()}, ";                        //ID
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
                KézT5C5Napló.Rögzítés(DateTime.Today.Year, Adat);
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

        public void Módosítás(Adat_T5C5_Kmadatok Adat)
        {
            string szöveg = " UPDATE kmtábla SET ";
            szöveg += $" Jjavszám={Adat.Jjavszám}, ";
            szöveg += $" KMUkm={Adat.KMUkm}, ";
            szöveg += $" KMUdátum='{Adat.KMUdátum:yyyy.MM.dd}', ";
            szöveg += $" Vizsgfok='{Adat.Vizsgfok.Trim()}', ";
            szöveg += $" Vizsgdátumk='{Adat.Vizsgdátumk:yyyy.MM.dd}', ";
            szöveg += $" Vizsgdátumv='{Adat.Vizsgdátumv:yyyy.MM.dd}', ";
            szöveg += $" VizsgKm={Adat.Vizsgkm}, ";
            szöveg += $" HaviKm={Adat.Havikm}, ";
            szöveg += $" VizsgSorszám={Adat.Vizsgsorszám}, ";
            szöveg += $" fudátum='{Adat.Fudátum:yyyy.MM.dd}', ";
            szöveg += $" Teljeskm={Adat.Teljeskm}, ";
            szöveg += $" Ciklusrend='{Adat.Ciklusrend.Trim()}', ";
            szöveg += $" V2végezte='{Adat.V2végezte.Trim()}', ";
            szöveg += $" KövV2_Sorszám={Adat.KövV2_sorszám},  ";
            szöveg += $" KövV2='{Adat.KövV2.Trim()}', ";
            szöveg += $" KövV_Sorszám={Adat.KövV_sorszám}, ";
            szöveg += $" KövV='{Adat.KövV.Trim()}', ";
            szöveg += $" törölt={Adat.Törölt}, ";
            szöveg += $" V2V3Számláló={Adat.V2V3Számláló} ";
            szöveg += $" WHERE id={Adat.ID}";
            MyA.ABMódosítás(hely, jelszó, szöveg);
            KézT5C5Napló.Rögzítés(DateTime.Today.Year, Adat);
        }

        public void MódosításKm(List<Adat_T5C5_Kmadatok> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_T5C5_Kmadatok Adat in Adatok)
                {
                    string szöveg = " UPDATE kmtábla SET ";
                    szöveg += $" Jjavszám={Adat.Jjavszám}, ";
                    szöveg += $" KMUkm={Adat.KMUkm}, ";
                    szöveg += $" KMUdátum='{Adat.KMUdátum:yyyy.MM.dd}', ";
                    szöveg += $" HaviKm={Adat.Havikm}, ";
                    szöveg += $" fudátum='{Adat.Fudátum:yyyy.MM.dd}', ";
                    szöveg += $" Teljeskm={Adat.Teljeskm}, ";
                    szöveg += $" WHERE id={Adat.ID}";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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

        public void Törlés(long Sorszám)
        {
            try
            {
                List<Adat_T5C5_Kmadatok> Adatok = Lista_Adatok();
                Adat_T5C5_Kmadatok Adat = Adatok.FirstOrDefault(x => x.ID == Sorszám);
                if (Adat == null)
                {
                    MessageBox.Show("A kiválasztott adat nem található!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                string szöveg = $"UPDATE kmtábla SET törölt=true WHERE id={Sorszám}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                KézT5C5Napló.Rögzítés(DateTime.Today.Year, Adat);
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

        private long Sorszám()
        {
            long válasz = 1;
            try
            {
                List<Adat_T5C5_Kmadatok> Adatok = Lista_Adatok();
                if (Adatok.Count > 0) válasz = Adatok.Max(x => x.ID) + 1;
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
            return válasz;
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
