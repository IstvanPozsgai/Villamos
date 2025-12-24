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
    public class Kezelő_Kerék_Eszterga_Igény
    {
        readonly string jelszó = "RónaiSándor";
        readonly string táblanév = "Igény";
        string hely;

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\{Év}_Igény.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kerék_Igény(hely.KönyvSzerk());
        }

        public List<Adat_Kerék_Eszterga_Igény> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Kerék_Eszterga_Igény> Adatok = new List<Adat_Kerék_Eszterga_Igény>();

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
                                Adat_Kerék_Eszterga_Igény Adat = new Adat_Kerék_Eszterga_Igény(
                                        rekord["Pályaszám"].ToStrTrim(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["Rögzítés_dátum"].ToÉrt_DaTeTime(),
                                        rekord["Igényelte"].ToStrTrim(),
                                        rekord["Tengelyszám"].ToÉrt_Int(),
                                        rekord["Szerelvény"].ToÉrt_Int(),
                                        rekord["prioritás"].ToÉrt_Int(),
                                        rekord["Ütemezés_dátum"].ToÉrt_DaTeTime(),
                                        rekord["státus"].ToÉrt_Int(),
                                        rekord["telephely"].ToStrTrim(),
                                        rekord["típus"].ToStrTrim(),
                                        rekord["Norma"].ToÉrt_Int()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Rögzítés(int Év, Adat_Kerék_Eszterga_Igény Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"INSERT INTO {táblanév} (Pályaszám, Rögzítés_dátum,  Igényelte, Tengelyszám, Szerelvény,  prioritás, Ütemezés_dátum,  státus, telephely, megjegyzés, típus, norma) VALUES (";
                szöveg += $"'{Adat.Pályaszám}', '{Adat.Rögzítés_dátum}', '{Adat.Igényelte}', {Adat.Tengelyszám}, {Adat.Szerelvény},";
                szöveg += $" {Adat.Prioritás}, '{Adat.Ütemezés_dátum}', {Adat.Státus}, '{Adat.Telephely}','{Adat.Megjegyzés}', '{Adat.Típus}', {Adat.Norma})";
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


        public void Módosítás(int Év, Adat_Kerék_Eszterga_Igény Adat, bool Törlés = false)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg;
                if (Törlés)
                    szöveg = $"UPDATE {táblanév} SET " +
                        $"státus={Adat.Státus}, " +
                        $"ütemezés_dátum='{Adat.Ütemezés_dátum:yyyy.MM.dd}' " +
                        $"WHERE státus=2 " +
                        $"AND telephely='{Adat.Telephely}' " +
                        $"AND pályaszám='{Adat.Pályaszám}'";
                else
                    szöveg = $"UPDATE {táblanév} SET " +
                        $"státus={Adat.Státus}, " +
                        $"ütemezés_dátum='{Adat.Ütemezés_dátum:yyyy.MM.dd}' " +
                        $"WHERE státus<=2 " +
                        $"AND telephely='{Adat.Telephely}' " +
                        $"AND pályaszám='{Adat.Pályaszám}'";
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

        public void Módosítás_Státus(int Év, string Pályaszám, int volt, int lesz)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"UPDATE {táblanév} SET státus={lesz}";
                szöveg += $" WHERE státus={volt} ";
                szöveg += $"AND pályaszám='{Pályaszám}'";

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
        public List<Adat_Kerék_Eszterga_Igény> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Eszterga_Igény> Adatok = new List<Adat_Kerék_Eszterga_Igény>();
            Adat_Kerék_Eszterga_Igény Adat;

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
                                Adat = new Adat_Kerék_Eszterga_Igény(
                                        rekord["Pályaszám"].ToStrTrim(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["Rögzítés_dátum"].ToÉrt_DaTeTime(),
                                        rekord["Igényelte"].ToStrTrim(),
                                        rekord["Tengelyszám"].ToÉrt_Int(),
                                        rekord["Szerelvény"].ToÉrt_Int(),
                                        rekord["prioritás"].ToÉrt_Int(),
                                        rekord["Ütemezés_dátum"].ToÉrt_DaTeTime(),
                                        rekord["státus"].ToÉrt_Int(),
                                        rekord["telephely"].ToStrTrim(),
                                        rekord["típus"].ToStrTrim(),
                                        rekord["Norma"].ToÉrt_Int()
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
