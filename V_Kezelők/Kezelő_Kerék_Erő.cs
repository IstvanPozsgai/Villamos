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
    public class Kezelő_Kerék_Erő
    {
        readonly string jelszó = "szabólászló";
        string hely;
        readonly string táblanév = "erőtábla";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Év}\telepikerék.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Méréstáblakerék(hely.KönyvSzerk());
        }

        public List<Adat_Kerék_Erő> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Kerék_Erő> Adatok = new List<Adat_Kerék_Erő>();

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
                                Adat_Kerék_Erő Adat = new Adat_Kerék_Erő(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Van"].ToStrTrim(),
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

        public void Módosítás(int Év, Adat_Kerék_Erő Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $" van='{Adat.Van}', ";
                szöveg += $" mikor='{Adat.Mikor}', ";
                szöveg += $" módosító='{Adat.Módosító}' ";
                szöveg += $" WHERE azonosító = '{Adat.Azonosító}'";
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

        public void Rögzítés(int Év, Adat_Kerék_Erő Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"INSERT INTO {táblanév} (van, mikor, módosító, azonosító)  VALUES (";
                szöveg += $"'{Adat.Van}', ";
                szöveg += $"'{Adat.Mikor}', ";
                szöveg += $"'{Adat.Módosító}', ";
                szöveg += $"'{Adat.Azonosító}')";
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
        public List<Adat_Kerék_Erő> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Erő> Adatok = new List<Adat_Kerék_Erő>();
            Adat_Kerék_Erő Adat;

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
                                Adat = new Adat_Kerék_Erő(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Van"].ToStrTrim(),
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
