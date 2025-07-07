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
    public class Kezelő_Kerék_Eszterga_Naptár
    {
        string hely;
        readonly string jelszó = "RónaiSándor";
        readonly string táblanév = "Naptár";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{Év}_Esztergálás.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kerék_Éves(hely);
        }

        public List<Adat_Kerék_Eszterga_Naptár> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            List<Adat_Kerék_Eszterga_Naptár> Adatok = new List<Adat_Kerék_Eszterga_Naptár>();
            Adat_Kerék_Eszterga_Naptár Adat;
            string szöveg = $"SELECT * FROM {táblanév}";

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
                                Adat = new Adat_Kerék_Eszterga_Naptár(
                                        rekord["Idő"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidő"].ToÉrt_Bool(),
                                        rekord["Foglalt"].ToÉrt_Bool(),
                                        rekord["Pályaszám"].ToStrTrim(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["betűszín"].ToÉrt_Long(),
                                        rekord["háttérszín"].ToÉrt_Long(),
                                        rekord["Marad"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás_Idő(int Év, List<DateTime> Idők)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (DateTime Idő in Idők)
                {
                    string szöveg = $"UPDATE naptár SET pályaszám='_', " +
                        $"foglalt=false, " +
                        $"Megjegyzés='', " +
                        $" betűszín=0, " +
                        $"háttérszín=12632256, " +
                        $"marad=false " +
                        $"WHERE idő=#{Idő:MM-dd-yyyy H:m:s}#";
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

        public void Módosítás(int Év, List<Adat_Kerék_Eszterga_Naptár> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kerék_Eszterga_Naptár Adat in Adatok)
                {
                    string szöveg = $"UPDATE naptár SET " +
                        $"pályaszám='{Adat.Pályaszám.Trim()}', " +
                        $"foglalt={Adat.Foglalt}, " +
                        $"Megjegyzés='{Adat.Megjegyzés.Trim()}', " +
                        $"betűszín={Adat.BetűSzín}, " +
                        $"háttérszín={Adat.HáttérSzín}, " +
                        $"marad={Adat.Marad} " +
                        $"WHERE idő=#{Adat.Idő:MM-dd-yyyy HH:mm}#";
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

        public void Módosítás_Státus(int Év, DateTime Idő, string Pályaszám, bool EgyElem)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg;
                if (EgyElem)
                {
                    szöveg = $"UPDATE naptár SET  foglalt=false, Megjegyzés='', betűszín=0, háttérszín=12632256, pályaszám='', marad=false ";
                    szöveg += $" WHERE idő=#{Idő:MM-dd-yyyy H:m:s}# AND pályaszám='{Pályaszám}'";
                }
                else
                {
                    szöveg = $"UPDATE naptár SET  foglalt=false, Megjegyzés='', betűszín=0, háttérszín=12632256, pályaszám='', marad=false ";
                    szöveg += $" WHERE idő>=#{Idő:MM-dd-yyyy H:m:s}# AND pályaszám='{Pályaszám}'";
                }
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


        //elkopó
        public List<Adat_Kerék_Eszterga_Naptár> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Eszterga_Naptár> Adatok = new List<Adat_Kerék_Eszterga_Naptár>();
            Adat_Kerék_Eszterga_Naptár Adat;

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
                                Adat = new Adat_Kerék_Eszterga_Naptár(
                                        rekord["Idő"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidő"].ToÉrt_Bool(),
                                        rekord["Foglalt"].ToÉrt_Bool(),
                                        rekord["Pályaszám"].ToStrTrim(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["betűszín"].ToÉrt_Long(),
                                        rekord["háttérszín"].ToÉrt_Long(),
                                        rekord["Marad"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<DateTime> Lista_Adatok_Idő(string hely, string jelszó, string szöveg)
        {
            List<DateTime> Adatok = new List<DateTime>();
            DateTime Adat;

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
                                Adat = rekord["Idő"].ToÉrt_DaTeTime();
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
