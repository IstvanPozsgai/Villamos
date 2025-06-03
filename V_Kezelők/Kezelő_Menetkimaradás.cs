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
    public class Kezelő_Menetkimaradás
    {
        readonly string jelszó = "lilaakác";
        string hely;

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\főkönyv\menet{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Menekimaradás_telephely(hely.KönyvSzerk());
        }

        public List<Adat_Menetkimaradás> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = "SELECT * FROM menettábla";
            List<Adat_Menetkimaradás> Adatok = new List<Adat_Menetkimaradás>();
            Adat_Menetkimaradás Adat;

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
                                Adat = new Adat_Menetkimaradás(
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["eseményjele"].ToStrTrim(),
                                    rekord["bekövetkezés"].ToÉrt_DaTeTime(),
                                    rekord["kimaradtmenet"].ToÉrt_Long(),
                                    rekord["jvbeírás"].ToStrTrim(),
                                    rekord["vmbeírás"].ToStrTrim(),
                                    rekord["javítás"].ToStrTrim(),
                                    rekord["id"].ToÉrt_Long(),
                                    rekord["törölt"].ToÉrt_Bool(),
                                    rekord["jelentés"].ToStrTrim(),
                                    rekord["tétel"].ToÉrt_Long()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Módosítás(string Telephely, int Év, Adat_Menetkimaradás Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $"UPDATE menettábla SET viszonylat='{Adat.Viszonylat}'";
                szöveg += $", azonosító='{Adat.Azonosító}'";
                szöveg += $", típus='{Adat.Típus}'";
                szöveg += $", Eseményjele='{Adat.Eseményjele}'";
                szöveg += $", Bekövetkezés='{Adat.Bekövetkezés}'";
                szöveg += $", kimaradtmenet={Adat.Kimaradtmenet}";
                szöveg += $", jvbeírás='{Adat.Jvbeírás}'";
                szöveg += $", vmbeírás='{Adat.Vmbeírás}'";
                szöveg += $", javítás='{Adat.Javítás}'";
                szöveg += $", törölt={Adat.Törölt} ";
                szöveg += $" WHERE tétel={Adat.Tétel} and jelentés='{Adat.Jelentés}'";
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


        public void Rögzítés(string Telephely, int Év, Adat_Menetkimaradás Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = "INSERT INTO menettábla ";
                szöveg += " ([viszonylat], [azonosító], [típus], [Eseményjele], [Bekövetkezés],";
                szöveg += " [kimaradtmenet], [jvbeírás], [vmbeírás], [javítás], [id], [törölt], [tétel], [jelentés]) ";
                szöveg += " VALUES (";
                szöveg += $"'{Adat.Viszonylat}', ";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"'{Adat.Típus}', ";
                szöveg += $"'{Adat.Eseményjele}', ";
                szöveg += $"'{Adat.Bekövetkezés}', ";
                szöveg += $"{Adat.Kimaradtmenet}, ";
                szöveg += $"'{Adat.Jvbeírás}', ";
                szöveg += $"'{Adat.Vmbeírás}', ";
                szöveg += $"'{Adat.Javítás}', ";
                szöveg += $" {Adat.Id}, ";
                szöveg += $" {Adat.Törölt}, ";
                szöveg += $" {Adat.Tétel}, ";
                szöveg += $"'{Adat.Jelentés}')";
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
        public List<Adat_Menetkimaradás> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Menetkimaradás> Adatok = new List<Adat_Menetkimaradás>();
            Adat_Menetkimaradás Adat;

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
                                Adat = new Adat_Menetkimaradás(
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["eseményjele"].ToStrTrim(),
                                    rekord["bekövetkezés"].ToÉrt_DaTeTime(),
                                    rekord["kimaradtmenet"].ToÉrt_Long(),
                                    rekord["jvbeírás"].ToStrTrim(),
                                    rekord["vmbeírás"].ToStrTrim(),
                                    rekord["javítás"].ToStrTrim(),
                                    rekord["id"].ToÉrt_Long(),
                                    rekord["törölt"].ToÉrt_Bool(),
                                    rekord["jelentés"].ToStrTrim(),
                                    rekord["tétel"].ToÉrt_Long()
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
