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
    public class Kezelő_Rezsi_Könyvelés
    {
        readonly string jelszó = "csavarhúzó";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Rezsi\rezsikönyv.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Rezsilista(hely);
        }

        public List<Adat_Rezsi_Lista> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = "SELECT * FROM könyv ORDER BY Azonosító";
            List<Adat_Rezsi_Lista> Adatok = new List<Adat_Rezsi_Lista>();
            Adat_Rezsi_Lista Adat;

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
                                Adat = new Adat_Rezsi_Lista(
                                       rekord["Azonosító"].ToStrTrim(),
                                       rekord["Mennyiség"].ToÉrt_Double(),
                                       rekord["Dátum"].ToÉrt_DaTeTime(),
                                       rekord["státus"].ToÉrt_Bool());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Rezsi_Lista> Lista_Adatok(List<string> Telephely)
        {
            List<Adat_Rezsi_Lista> Adatok = new List<Adat_Rezsi_Lista>();
            foreach (string telep in Telephely)
            {
                FájlBeállítás(telep);
                string szöveg = "SELECT * FROM könyv ORDER BY Azonosító";
                Adat_Rezsi_Lista Adat;

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
                                    Adat = new Adat_Rezsi_Lista(
                                           rekord["Azonosító"].ToStrTrim(),
                                           rekord["Mennyiség"].ToÉrt_Double(),
                                           rekord["Dátum"].ToÉrt_DaTeTime(),
                                           rekord["státus"].ToÉrt_Bool(),
                                           telep);
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }

            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Rezsi_Lista Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "INSERT INTO könyv (azonosító, Mennyiség, dátum, státus ) VALUES (";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"{Adat.Mennyiség.ToString().Replace(',', '.')}, ";
                szöveg += $"'{Adat.Dátum}', ";
                szöveg += $"{Adat.Státus})";
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

        public void Módosítás(string Telephely, Adat_Rezsi_Lista Adat)
        {
            try
            {
                FájlBeállítás(Telephely);

                string szöveg = "UPDATE könyv  SET ";
                szöveg += $"Mennyiség={Adat.Mennyiség.ToString().Replace(',', '.')}, ";
                szöveg += $"státus={Adat.Státus}, ";
                szöveg += $"dátum ='{Adat.Dátum}' ";
                szöveg += $" WHERE [azonosító]='{Adat.Azonosító}'";
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

        public void Nagybetűs(string Telephely)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<Adat_Rezsi_Lista> Adatok = Lista_Adatok(Telephely);
                foreach (Adat_Rezsi_Lista rekord in Adatok)
                {
                    if (rekord.Azonosító != rekord.Azonosító.ToUpper())
                    {
                        Adat_Rezsi_Lista Adat = new Adat_Rezsi_Lista(
                                                rekord.Azonosító.ToUpper(),
                                                rekord.Mennyiség,
                                                rekord.Dátum,
                                                rekord.Státus);
                        Rögzítés(Telephely, Adat);
                        string szöveg = $"DELETE FROM könyv WHERE Azonosító='{rekord.Azonosító}'";
                        Adatbázis.ABtörlés(hely, jelszó, szöveg);
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
