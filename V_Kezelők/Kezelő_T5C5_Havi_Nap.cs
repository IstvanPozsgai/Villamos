using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Kezelő_T5C5_Havi_Nap
    {
        readonly string jelszó = "pozsgaii";
        string hely;

        private void FájlBeállítás(DateTime Dátum)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\T5C5\{Dátum.Year}\havi{Dátum:yyyyMM}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Havifutástábla_Létrehozás(hely.KönyvSzerk());
        }

        public List<Adat_T5C5_Havi_Nap> Lista_Adatok(DateTime Dátum)
        {
            FájlBeállítás(Dátum);
            string szöveg = "SELECT * FROM állománytábla ORDER BY azonosító";
            List<Adat_T5C5_Havi_Nap> Adatok = new List<Adat_T5C5_Havi_Nap>();
            Adat_T5C5_Havi_Nap Adat;

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
                                Adat = new Adat_T5C5_Havi_Nap(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["N1"].ToStrTrim(),
                                    rekord["N2"].ToStrTrim(),
                                    rekord["N3"].ToStrTrim(),
                                    rekord["N4"].ToStrTrim(),
                                    rekord["N5"].ToStrTrim(),
                                    rekord["N6"].ToStrTrim(),
                                    rekord["N7"].ToStrTrim(),
                                    rekord["N8"].ToStrTrim(),
                                    rekord["N9"].ToStrTrim(),
                                    rekord["N10"].ToStrTrim(),
                                    rekord["N11"].ToStrTrim(),
                                    rekord["N12"].ToStrTrim(),
                                    rekord["N13"].ToStrTrim(),
                                    rekord["N14"].ToStrTrim(),
                                    rekord["N15"].ToStrTrim(),
                                    rekord["N16"].ToStrTrim(),
                                    rekord["N17"].ToStrTrim(),
                                    rekord["N18"].ToStrTrim(),
                                    rekord["N19"].ToStrTrim(),
                                    rekord["N20"].ToStrTrim(),
                                    rekord["N21"].ToStrTrim(),
                                    rekord["N22"].ToStrTrim(),
                                    rekord["N23"].ToStrTrim(),
                                    rekord["N24"].ToStrTrim(),
                                    rekord["N25"].ToStrTrim(),
                                    rekord["N26"].ToStrTrim(),
                                    rekord["N27"].ToStrTrim(),
                                    rekord["N28"].ToStrTrim(),
                                    rekord["N29"].ToStrTrim(),
                                    rekord["N30"].ToStrTrim(),
                                    rekord["N31"].ToStrTrim(),
                                    rekord["Futásnap"].ToÉrt_Int(),
                                    rekord["Telephely"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(DateTime Dátum, List<Adat_T5C5_Havi_Nap> Adatok)
        {
            try
            {
                FájlBeállítás(Dátum);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_T5C5_Havi_Nap Adat in Adatok)
                {
                    string szöveg = "INSERT INTO állománytábla (Azonosító, N1, N2, N3, N4, N5, N6, N7, N8, N9, N10,";
                    szöveg += "N11,N12,N13,N14,N15,N16,N17,N18,N19,N20,";
                    szöveg += "N21,N22,N23,N24,N25,N26,N27,N28,N29,N30,N31,Futásnap,Telephely ) VALUES (";
                    szöveg += $"'{Adat.Azonosító}',";
                    szöveg += $"'{Adat.N1}',";
                    szöveg += $"'{Adat.N2}',";
                    szöveg += $"'{Adat.N3}',";
                    szöveg += $"'{Adat.N4}',";
                    szöveg += $"'{Adat.N5}',";
                    szöveg += $"'{Adat.N6}',";
                    szöveg += $"'{Adat.N7}',";
                    szöveg += $"'{Adat.N8}',";
                    szöveg += $"'{Adat.N9}',";
                    szöveg += $"'{Adat.N10}',";
                    szöveg += $"'{Adat.N11}',";
                    szöveg += $"'{Adat.N12}',";
                    szöveg += $"'{Adat.N13}',";
                    szöveg += $"'{Adat.N14}',";
                    szöveg += $"'{Adat.N15}',";
                    szöveg += $"'{Adat.N16}',";
                    szöveg += $"'{Adat.N17}',";
                    szöveg += $"'{Adat.N18}',";
                    szöveg += $"'{Adat.N19}',";
                    szöveg += $"'{Adat.N20}',";
                    szöveg += $"'{Adat.N21}',";
                    szöveg += $"'{Adat.N22}',";
                    szöveg += $"'{Adat.N23}',";
                    szöveg += $"'{Adat.N24}',";
                    szöveg += $"'{Adat.N25}',";
                    szöveg += $"'{Adat.N26}',";
                    szöveg += $"'{Adat.N27}',";
                    szöveg += $"'{Adat.N28}',";
                    szöveg += $"'{Adat.N29}',";
                    szöveg += $"'{Adat.N30}',";
                    szöveg += $"'{Adat.N31}',";
                    szöveg += $"{Adat.Futásnap},";
                    szöveg += $"'{Adat.Telephely}')";
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


        //elkopó
        public List<Adat_T5C5_Havi_Nap> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Havi_Nap> Adatok = new List<Adat_T5C5_Havi_Nap>();
            Adat_T5C5_Havi_Nap Adat;

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
                                Adat = new Adat_T5C5_Havi_Nap(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["N1"].ToStrTrim(),
                                    rekord["N2"].ToStrTrim(),
                                    rekord["N3"].ToStrTrim(),
                                    rekord["N4"].ToStrTrim(),
                                    rekord["N5"].ToStrTrim(),
                                    rekord["N6"].ToStrTrim(),
                                    rekord["N7"].ToStrTrim(),
                                    rekord["N8"].ToStrTrim(),
                                    rekord["N9"].ToStrTrim(),
                                    rekord["N10"].ToStrTrim(),
                                    rekord["N11"].ToStrTrim(),
                                    rekord["N12"].ToStrTrim(),
                                    rekord["N13"].ToStrTrim(),
                                    rekord["N14"].ToStrTrim(),
                                    rekord["N15"].ToStrTrim(),
                                    rekord["N16"].ToStrTrim(),
                                    rekord["N17"].ToStrTrim(),
                                    rekord["N18"].ToStrTrim(),
                                    rekord["N19"].ToStrTrim(),
                                    rekord["N20"].ToStrTrim(),
                                    rekord["N21"].ToStrTrim(),
                                    rekord["N22"].ToStrTrim(),
                                    rekord["N23"].ToStrTrim(),
                                    rekord["N24"].ToStrTrim(),
                                    rekord["N25"].ToStrTrim(),
                                    rekord["N26"].ToStrTrim(),
                                    rekord["N27"].ToStrTrim(),
                                    rekord["N28"].ToStrTrim(),
                                    rekord["N29"].ToStrTrim(),
                                    rekord["N30"].ToStrTrim(),
                                    rekord["N31"].ToStrTrim(),
                                    rekord["Futásnap"].ToÉrt_Int(),
                                    rekord["Telephely"].ToStrTrim()
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



    public class Kezelő_T5C5_Előterv
    {

        public List<Adat_T5C5_Előterv> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Előterv> Adatok = new List<Adat_T5C5_Előterv>();
            Adat_T5C5_Előterv Adat;

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

                                Adat = new Adat_T5C5_Előterv(
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

                                    rekord["Honostelephely"].ToStrTrim(),
                                    rekord["tervsorszám"].ToÉrt_Long(),
                                    rekord["Kerék_K11"].ToÉrt_Double(),
                                    rekord["Kerék_K12"].ToÉrt_Double(),
                                    rekord["Kerék_K21"].ToÉrt_Double(),
                                    rekord["Kerék_K22"].ToÉrt_Double(),
                                    rekord["Kerék_min"].ToÉrt_Double(),

                                    rekord["V2V3Számláló"].ToÉrt_Long()
                                    ); ;
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_T5C5_Előterv Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_T5C5_Előterv Adat = null;

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

                            Adat = new Adat_T5C5_Előterv(
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

                                rekord["Honostelephely"].ToStrTrim(),
                                rekord["tervsorszám"].ToÉrt_Long(),
                                rekord["Kerék_K11"].ToÉrt_Double(),
                                rekord["Kerék_K12"].ToÉrt_Double(),
                                rekord["Kerék_K21"].ToÉrt_Double(),
                                rekord["Kerék_K22"].ToÉrt_Double(),
                                rekord["Kerék_min"].ToÉrt_Double(),

                                rekord["V2V3Számláló"].ToÉrt_Long()
                                );
                        }
                    }
                }
            }
            return Adat;
        }
    }

}
