using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos_Kezelők
{
    public class Kezelő_Akkumulátor_Mérés
    {
        string hely;
        readonly string jelszó = "kasosmiklós";

        public List<Adat_Akkumulátor_Mérés> Lista_Adatok(DateTime Dátum)
        {
            List<Adat_Akkumulátor_Mérés> Adatok = new List<Adat_Akkumulátor_Mérés>();
            Adat_Akkumulátor_Mérés Adat;

            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Akkumulátor\Akkunapló{Dátum.Year}.mdb".Ellenőrzés();
            string szöveg = "SELECT * FROM méréstábla ORDER BY gyáriszám, Mérésdátuma asc";

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
                                Adat = new Adat_Akkumulátor_Mérés(
                                        rekord["Gyáriszám"].ToStrTrim(),
                                        rekord["kisütésiáram"].ToÉrt_Long(),
                                        rekord["kezdetifesz"].ToÉrt_Double(),
                                        rekord["végfesz"].ToÉrt_Double(),
                                        rekord["kisütésiidő"].ToÉrt_DaTeTime(),
                                        rekord["kapacitás"].ToÉrt_Double(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["van"].ToStrTrim(),
                                        rekord["Mérésdátuma"].ToÉrt_DaTeTime(),
                                        rekord["Rögzítés"].ToÉrt_DaTeTime(),
                                        rekord["Rögzítő"].ToStrTrim(),
                                        rekord["id"].ToÉrt_Long()
                                         );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Törlés(DateTime Dátum, List<int> Számok)
        {
            try
            {
                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Akkumulátor\Akkunapló{Dátum.Year}.mdb".Ellenőrzés();
                List<string> SzövegGy = new List<string>();
                foreach (int ID in Számok)
                {
                    string szöveg = $"UPDATE méréstábla SET Rögzítő='TÖRÖLT' WHERE ID={ID}";
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

        public void Rögzítés(Adat_Akkumulátor_Mérés Adat, DateTime Dátum)
        {
            try
            {
                hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Akkumulátor\Akkunapló{Dátum.Year}.mdb".Ellenőrzés();

                string szöveg = "INSERT INTO méréstábla ";
                szöveg += "(Gyáriszám, Kisütésiáram, Kezdetifesz, Végfesz, Kisütésiidő, Kapacitás, Megjegyzés, Van, Mérésdátuma, Rögzítés, Rögzítő, id)";
                szöveg += " VALUES (";
                szöveg += $"'{Adat.Gyáriszám}', ";//Gyáriszám
                szöveg += $"{Adat.Kisütésiáram.ToString().Replace(',', '.')}, ";//kisütésiáram
                szöveg += $"{Adat.Kezdetifesz.ToString().Replace(',', '.')}, ";//kezdetifesz
                szöveg += $"{Adat.Végfesz.ToString().Replace(',', '.')}, ";//végfesz]
                szöveg += $"'{Adat.Kisütésiidő:HH:mm}', ";//kisütésiidő
                szöveg += $"{Adat.Kapacitás.ToString().Replace(',', '.')}, ";//kapacitás
                szöveg += $"'{Adat.Megjegyzés}', ";//Megjegyzés
                szöveg += $"'{Adat.Van}', ";      //Van
                szöveg += $"'{Adat.Mérésdátuma:yyyy.MM.dd}', ";//Mérésdátuma
                szöveg += $"'{Adat.Rögzítés}', ";//Rögzítés
                szöveg += $"'{Adat.Rögzítő}', ";//Rögzítő
                szöveg += $"{Sorszám(Dátum)})";//id
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

        public long Sorszám(DateTime Dátum)
        {
            long Válasz = 1;
            try
            {
                List<Adat_Akkumulátor_Mérés> Adatok = Lista_Adatok(Dátum);
                if (Adatok != null && Adatok.Count > 0) Válasz = Adatok.Max(x => x.Id) + 1;
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
            return Válasz;
        }

    }
}
