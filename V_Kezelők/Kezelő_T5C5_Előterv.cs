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
    public class Kezelő_T5C5_Előterv
    {
        readonly string jelszó = "pocsaierzsi";

        private void FájlBeállítás(string hely)
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Előtervkmfutástábla(hely.KönyvSzerk());
        }

        public List<Adat_T5C5_Előterv> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * FROM KMtábla order by vizsgdátumv desc";
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

        public void Rögzítés(string hely, List<Adat_T5C5_Előterv> Adatok)
        {
            try
            {
                FájlBeállítás(hely);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_T5C5_Előterv Adat in Adatok)
                {
                    string szöveg = "INSERT INTO kmtábla  (ID, azonosító, jjavszám, KMUkm, KMUdátum, ";
                    szöveg += " vizsgfok,  vizsgdátumk, vizsgdátumv,";
                    szöveg += " vizsgkm, havikm, vizsgsorszám, fudátum, ";
                    szöveg += " Teljeskm, Ciklusrend, V2végezte, KövV2_Sorszám, KövV2, ";
                    szöveg += " KövV_Sorszám, KövV, V2V3Számláló, törölt, Honostelephely, tervsorszám, Kerék_K11, Kerék_K12, Kerék_K21, Kerék_K22, Kerék_min)";
                    szöveg += " VALUES (";
                    szöveg += $"{Adat.ID}, ";                                // id
                    szöveg += $"'{Adat.Azonosító}', ";                       // azonosító
                    szöveg += $"{Adat.Jjavszám}, ";                          // jjavszám
                    szöveg += $"{Adat.KMUkm}, ";                             // KMUkm
                    szöveg += $"'{Adat.KMUdátum:yyyy.MM.dd}', ";             // KMUdátum
                    szöveg += $"'{Adat.Vizsgfok.Trim()}', ";                 // vizsgfok
                    szöveg += $"'{Adat.Vizsgdátumk:yyyy.MM.dd}', ";          // vizsgdátumk
                    szöveg += $"'{Adat.Vizsgdátumv:yyyy.MM.dd}', ";          // vizsgdátumv
                    szöveg += $"{Adat.Vizsgkm}, ";                           // vizsgkm
                    szöveg += $"{Adat.Havikm}, ";                            // havikm
                    szöveg += $"{Adat.Vizsgsorszám}, ";                      // vizsgsorszám
                    szöveg += $"'{Adat.Fudátum:yyyy.MM.dd}', ";              // fudátum
                    szöveg += $"{Adat.Teljeskm}, ";                          // Teljeskm
                    szöveg += $"'{Adat.Ciklusrend}', ";                      // Ciklusrend
                    szöveg += $"'{Adat.V2végezte}', ";                       // V2végezte
                    szöveg += $"{Adat.KövV2_sorszám}, ";                     // KövV2_Sorszám
                    szöveg += $"'{Adat.KövV2}', ";                           // KövV2
                    szöveg += $"{Adat.KövV_sorszám}, ";                      // KövV_Sorszám
                    szöveg += $"'{Adat.KövV.Trim()}', ";                     // KövV
                    szöveg += $"{Adat.V2V3Számláló}, ";                      // V2V3Számláló
                    szöveg += $"{Adat.Törölt}, ";                            // törölt
                    szöveg += $"'{Adat.Honostelephely}', ";                  // Honostelephely
                    szöveg += $"{Adat.Tervsorszám}, ";                       // tervsorszám
                    szöveg += $"{Adat.Kerék_K11.ToString().Replace(",", ".")}, "; // Kerék_K11
                    szöveg += $"{Adat.Kerék_K12.ToString().Replace(",", ".")}, "; // Kerék_K12
                    szöveg += $"{Adat.Kerék_K21.ToString().Replace(",", ".")}, "; // Kerék_K21
                    szöveg += $"{Adat.Kerék_K22.ToString().Replace(",", ".")}, "; // Kerék_K22
                    szöveg += $"{Adat.Kerék_min.ToString().Replace(",", ".")} )"; // Kerék_min
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
    }
}
