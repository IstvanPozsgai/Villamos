using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Telep_Kiegészítő_SérülésCaf
    {
        string hely;
        readonly string jelszó = "kismalac";
        readonly string táblanév = "tábla";

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\segéd\sérüléscaf.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.CAFtáblakészít(hely.KönyvSzerk());
        }

        public List<Adat_Telep_Kiegészítő_SérülésCaf> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY id";
            List<Adat_Telep_Kiegészítő_SérülésCaf> Adatok = new List<Adat_Telep_Kiegészítő_SérülésCaf>();
            Adat_Telep_Kiegészítő_SérülésCaf Adat;

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
                                Adat = new Adat_Telep_Kiegészítő_SérülésCaf(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Cég"].ToStrTrim(),
                                        rekord["Név"].ToStrTrim(),
                                        rekord["Beosztás"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Telep_Kiegészítő_SérülésCaf Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"INSERT INTO {táblanév} (id, cég, név, beosztás) VALUES (";
                szöveg += $"{Adat.Id}, ";
                szöveg += $"'{Adat.Cég}', ";
                szöveg += $"'{Adat.Név}', ";
                szöveg += $"'{Adat.Beosztás}')";
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

        public void Módosítás(string Telephely, Adat_Telep_Kiegészítő_SérülésCaf Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"cég='{Adat.Cég}', ";
                szöveg += $"név='{Adat.Név}', ";
                szöveg += $"beosztás='{Adat.Beosztás}' ";
                szöveg += $" WHERE [id] ={Adat.Id}";
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

        public void Törlés(string Telephely, int AdatID)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"DELETE FROM {táblanév} WHERE id={AdatID}";
                MyA.ABtörlés(hely, jelszó, szöveg);
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

        public void Újraszámolás(string Telephely)
        {
            try
            {
                FájlBeállítás(Telephely);

                List<Adat_Telep_Kiegészítő_SérülésCaf> AdatokSérülésCaf = Lista_Adatok(Telephely);

                List<string> szövegGy = new List<string>();
                for (int index = 0; index < AdatokSérülésCaf.Count; index++)
                {
                    int újId = index + 1;
                    string szöveg = $"UPDATE {táblanév} SET id={újId} WHERE id={AdatokSérülésCaf[index].Id}";
                    szövegGy.Add(szöveg);
                    AdatokSérülésCaf[index].Id = újId;
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
