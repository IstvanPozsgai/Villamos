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

        public void Módosítás(string Telephely, int Év, List<Adat_Menetkimaradás> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Menetkimaradás Elem in Adatok)
                {
                    string szöveg = $"UPDATE menettábla SET viszonylat='{Elem.Viszonylat}'";
                    szöveg += $", azonosító='{Elem.Azonosító}'";
                    szöveg += $", típus='{Elem.Típus}'";
                    szöveg += $", Eseményjele='{Elem.Eseményjele}'";
                    szöveg += $", Bekövetkezés='{Elem.Bekövetkezés}'";
                    szöveg += $", kimaradtmenet={Elem.Kimaradtmenet}";
                    szöveg += $", jvbeírás='{Elem.Jvbeírás}'";
                    szöveg += $", vmbeírás='{Elem.Vmbeírás}'";
                    szöveg += $", javítás='{Elem.Javítás}'";
                    szöveg += $", törölt={Elem.Törölt} ";
                    szöveg += $" WHERE tétel={Elem.Tétel} and jelentés='{Elem.Jelentés}'";
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

        public void Rögzítés(string Telephely, int Év, List<Adat_Menetkimaradás> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                long i = Sorszám(Telephely, Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Menetkimaradás Adat in Adatok)
                {
                    string szöveg = "INSERT INTO menettábla ";
                    szöveg += " ([viszonylat], [azonosító], [típus], [Eseményjele], [Bekövetkezés],";
                    szöveg += " [kimaradtmenet], [jvbeírás], [vmbeírás], [javítás], [id], [törölt], [tétel], [jelentés]) ";
                    szöveg += " VALUES (";
                    szöveg += $"'{Adat.Viszonylat}','{Adat.Azonosító}','{Adat.Típus}','{Adat.Eseményjele}','{Adat.Bekövetkezés}',";
                    szöveg += $"{Adat.Kimaradtmenet},'{Adat.Jvbeírás}','{Adat.Vmbeírás}','{Adat.Javítás}', {i}, {Adat.Törölt}, {Adat.Tétel},'{Adat.Jelentés}')";
                    i++;
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
                szöveg += $" {Sorszám(Telephely, Év)}, ";
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

        public void Döntés(string Telephely, int Év, List<Adat_Menetkimaradás> Adatok)
        {
            try
            {
                List<Adat_Menetkimaradás> AdatokRögzítés = new List<Adat_Menetkimaradás>();
                List<Adat_Menetkimaradás> AdatokMódosítás = new List<Adat_Menetkimaradás>();
                List<Adat_Menetkimaradás> AdatokBázis = Lista_Adatok(Telephely, Év);
                foreach (Adat_Menetkimaradás Elem in Adatok)
                {
                    Adat_Menetkimaradás ADAT = (from a in AdatokBázis
                                                where a.Tétel == Elem.Tétel
                                                select a).FirstOrDefault();
                    if (ADAT == null)
                        AdatokRögzítés.Add(Elem);
                    else
                        AdatokMódosítás.Add(Elem);
                }
                if (AdatokRögzítés.Count > 0) Rögzítés(Telephely, Év, AdatokRögzítés);
                if (AdatokMódosítás.Count > 0) Módosítás(Telephely, Év, AdatokMódosítás);


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

        public long Sorszám(string Telephely, int Év)
        {
            long válasz = 1;
            try
            {
                List<Adat_Menetkimaradás> Adatok = Lista_Adatok(Telephely, Év);
                if (Adatok.Count > 0) válasz = Adatok.Max(x => x.Id) + 1;
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
            return válasz;
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
