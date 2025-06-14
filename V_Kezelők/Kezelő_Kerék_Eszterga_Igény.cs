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
            string szöveg = "SELECT * FROM Igény";
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
                string szöveg = "INSERT INTO igény (Pályaszám, Rögzítés_dátum,  Igényelte, Tengelyszám, Szerelvény,  prioritás, Ütemezés_dátum,  státus, telephely, megjegyzés, típus, norma) VALUES (";
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

        public Adat_Kerék_Eszterga_Igény Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kerék_Eszterga_Igény Adat = null;

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
                            rekord.Read();

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
                        }
                    }
                }
            }
            return Adat;
        }
    }

}
