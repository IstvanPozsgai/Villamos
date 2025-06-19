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
    public class Kezelő_Rezsi_Napló
    {
        readonly string jelszó = "csavarhúzó";
        readonly string Táblanév = "napló";
        string hely;

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Rezsi\rezsinapló{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Rezsilistanapló(hely.KönyvSzerk());
        }

        public List<Adat_Rezsi_Listanapló> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = $"SELECT * FROM {Táblanév} ORDER BY Azonosító";
            List<Adat_Rezsi_Listanapló> Adatok = new List<Adat_Rezsi_Listanapló>();
            Adat_Rezsi_Listanapló Adat;

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
                                Adat = new Adat_Rezsi_Listanapló(
                                       rekord["Azonosító"].ToStrTrim(),
                                       rekord["Honnan"].ToStrTrim(),
                                       rekord["Hova"].ToStrTrim(),
                                       rekord["Mennyiség"].ToÉrt_Double(),
                                       rekord["Mirehasznál"].ToStrTrim(),
                                       rekord["Módosította"].ToStrTrim(),
                                       rekord["módosításidátum"].ToÉrt_DaTeTime(),
                                       rekord["Státus"].ToÉrt_Bool());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, int Év, Adat_Rezsi_Listanapló Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"INSERT INTO {Táblanév} (Azonosító, honnan, hova, mennyiség, státus, módosította, mirehasznál, módosításidátum) VALUES (";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"'{Adat.Honnan}', ";
                szöveg += $"'{Adat.Hova}', ";
                szöveg += $"{Adat.Mennyiség}, ";
                szöveg += $"{Adat.Státus},";
                szöveg += $"'{Adat.Módosította}', ";
                szöveg += $"'{Adat.Mirehasznál}', ";
                szöveg += $"'{Adat.Módosításidátum}')";
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

        public void Nagybetűs(string Telephely, int Év)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<Adat_Rezsi_Listanapló> Adatok = Lista_Adatok(Telephely, Év);
                foreach (Adat_Rezsi_Listanapló rekord in Adatok)
                {
                    if (rekord.Azonosító != rekord.Azonosító.ToUpper())
                    {
                        Adat_Rezsi_Listanapló Adat = new Adat_Rezsi_Listanapló(
                                                rekord.Azonosító.ToUpper(),
                                                rekord.Honnan,
                                                rekord.Hova,
                                                rekord.Mennyiség,
                                                rekord.Mirehasznál == null || rekord.Mirehasznál == "" ? "_" : rekord.Mirehasznál,
                                                rekord.Módosította,
                                                rekord.Módosításidátum,
                                                rekord.Státus);
                        Rögzítés(Telephely, Év, Adat);
                    }
                }

                List<string> Lista = Adatok.Select(a => a.Azonosító).Distinct().ToList();
                foreach (string rekord in Lista)
                {
                    if (rekord != rekord.ToUpper())
                    {
                        string szöveg = $"DELETE FROM {Táblanév} WHERE Azonosító='{rekord}'";
                        MyA.ABtörlés(hely, jelszó, szöveg);
                    }
                }

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
