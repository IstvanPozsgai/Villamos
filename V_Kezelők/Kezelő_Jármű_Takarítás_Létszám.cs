using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű_Takarítás_Létszám
    {
        readonly string jelszó = "seprűéslapát";
        string hely;

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Takarítás\Takarítás_{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Járműtakarító_Telephely_tábla(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_Takarítás_Létszám> Lista_Adat(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = $"SELECT * FROM létszám";
            List<Adat_Jármű_Takarítás_Létszám> Adatok = new List<Adat_Jármű_Takarítás_Létszám>();
            Adat_Jármű_Takarítás_Létszám Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Létszám(
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["előírt"].ToÉrt_Int(),
                                        rekord["megjelent"].ToÉrt_Int(),
                                        rekord["napszak"].ToÉrt_Int(),
                                        rekord["ruhátlan"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Rögzítés(string Telephely, int Év, Adat_Jármű_Takarítás_Létszám Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"INSERT INTO létszám (dátum, napszak, előírt, megjelent, ruhátlan) VALUES (";
                szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', ";     //dátum
                szöveg += $"{Adat.Napszak}, ";     //napszak,
                szöveg += $"{Adat.Előírt}, ";     //előírt,
                szöveg += $"{Adat.Megjelent}, ";     //megjelent,
                szöveg += $"{Adat.Ruhátlan}) ";     //ruhátlan
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


        public void Módosítás(string Telephely, int Év, Adat_Jármű_Takarítás_Létszám Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"UPDATE létszám  SET ";
                szöveg += $"előírt={Adat.Előírt}, ";
                szöveg += $"megjelent={Adat.Megjelent}, ";
                szöveg += $"ruhátlan={Adat.Ruhátlan} ";
                szöveg += $" WHERE Dátum =#{Adat.Dátum:M-d-yy}# ";
                szöveg += $" And napszak={Adat.Napszak}";
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
