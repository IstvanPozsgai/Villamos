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
    public class Kezelő_Épület_Naptár
    {
        string hely;
        readonly string jelszó = "seprűéslapát";
        readonly string táblanév = "naptár";

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely.Trim()}\Adatok\Épület\{Év}épülettakarítás.mdb".KönyvSzerk();
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Épülettakarítótábla(hely);
        }

        public List<Adat_Épület_Naptár> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = $"SELECT * FROM {táblanév}";

            List<Adat_Épület_Naptár> Adatok = new List<Adat_Épület_Naptár>();

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
                                Adat_Épület_Naptár Adat = new Adat_Épület_Naptár(
                                          rekord["Előterv"].ToÉrt_Bool(),
                                          rekord["Hónap"].ToÉrt_Int(),
                                          rekord["Igazolás"].ToÉrt_Bool(),
                                          rekord["Napok"].ToStrTrim());

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, int Év, Adat_Épület_Naptár Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"INSERT INTO {táblanév}  (előterv, hónap, igazolás, napok ) VALUES (";
                szöveg += $"false, {Adat.Hónap}, false,'{Adat.Napok}')";
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

        public void Módosítás(string Telephely, int Év, string Napok, int Hónap)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"UPDATE {táblanév}  SET ";
                szöveg += $"napok='{Napok}'";
                szöveg += $"  WHERE hónap={Hónap}";
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

        public void Módosítás_igazolás(string Telephely, int Év, bool igazolás, int Hónap)
        {
            try
            {
                FájlBeállítás(Telephely, Év);

                string szöveg = $"UPDATE {táblanév}  SET ";
                szöveg += $" igazolás={igazolás} ";
                szöveg += $" WHERE hónap={Hónap}";
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

        public void Módosítás_előterv(string Telephely, int Év, bool előterv, int Hónap)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"UPDATE {táblanév}  SET ";
                szöveg += $"előterv={előterv} ";
                szöveg += $"  WHERE hónap={Hónap}";
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
    }
}
