using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Behajtás_Behajtási
    {
        readonly string jelszó = "forgalmirendszám";

        public List<Adat_Behajtás_Behajtási> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Behajtás_Behajtási> Adatok = new List<Adat_Behajtás_Behajtási>();
            Adat_Behajtás_Behajtási Adat;

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
                                Adat = new Adat_Behajtás_Behajtási(
                                        rekord["Sorszám"].ToStrTrim(),
                                        rekord["Szolgálatihely"].ToStrTrim(),
                                        rekord["HRazonosító"].ToStrTrim(),
                                        rekord["Név"].ToStrTrim(),
                                        rekord["Rendszám"].ToStrTrim(),
                                        rekord["Angyalföld_engedély"].ToÉrt_Int(),
                                        rekord["Angyalföld_megjegyzés"].ToStrTrim(),
                                        rekord["Baross_engedély"].ToÉrt_Int(),
                                        rekord["Baross_megjegyzés"].ToStrTrim(),
                                        rekord["Budafok_engedély"].ToÉrt_Int(),
                                        rekord["Budafok_megjegyzés"].ToStrTrim(),
                                        rekord["Ferencváros_engedély"].ToÉrt_Int(),
                                        rekord["Ferencváros_megjegyzés"].ToStrTrim(),
                                        rekord["Fogaskerekű_engedély"].ToÉrt_Int(),
                                        rekord["Fogaskerekű_megjegyzés"].ToStrTrim(),
                                        rekord["Hungária_engedély"].ToÉrt_Int(),
                                        rekord["Hungária_megjegyzés"].ToStrTrim(),
                                        rekord["Kelenföld_engedély"].ToÉrt_Int(),
                                        rekord["Kelenföld_megjegyzés"].ToStrTrim(),
                                        rekord["Száva_engedély"].ToÉrt_Int(),
                                        rekord["Száva_megjegyzés"].ToStrTrim(),
                                        rekord["Szépilona_engedély"].ToÉrt_Int(),
                                        rekord["Szépilona_megjegyzés"].ToStrTrim(),
                                        rekord["Zugló_engedély"].ToÉrt_Int(),
                                        rekord["Zugló_megjegyzés"].ToStrTrim(),
                                        rekord["Korlátlan"].ToStrTrim(),
                                        rekord["Autók_száma"].ToÉrt_Int(),
                                        rekord["I_engedély"].ToÉrt_Int(),
                                        rekord["II_engedély"].ToÉrt_Int(),
                                        rekord["III_engedély"].ToÉrt_Int(),
                                        rekord["Státus"].ToÉrt_Int(),
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["PDF"].ToStrTrim(),
                                        rekord["OKA"].ToStrTrim(),
                                        rekord["Érvényes"].ToÉrt_DaTeTime());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Behajtás_Behajtási> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * FROM alapadatok ";
            List<Adat_Behajtás_Behajtási> Adatok = new List<Adat_Behajtás_Behajtási>();
            Adat_Behajtás_Behajtási Adat;

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
                                Adat = new Adat_Behajtás_Behajtási(
                                        rekord["Sorszám"].ToStrTrim(),
                                        rekord["Szolgálatihely"].ToStrTrim(),
                                        rekord["HRazonosító"].ToStrTrim(),
                                        rekord["Név"].ToStrTrim(),
                                        rekord["Rendszám"].ToStrTrim(),
                                        rekord["Angyalföld_engedély"].ToÉrt_Int(),
                                        rekord["Angyalföld_megjegyzés"].ToStrTrim(),
                                        rekord["Baross_engedély"].ToÉrt_Int(),
                                        rekord["Baross_megjegyzés"].ToStrTrim(),
                                        rekord["Budafok_engedély"].ToÉrt_Int(),
                                        rekord["Budafok_megjegyzés"].ToStrTrim(),
                                        rekord["Ferencváros_engedély"].ToÉrt_Int(),
                                        rekord["Ferencváros_megjegyzés"].ToStrTrim(),
                                        rekord["Fogaskerekű_engedély"].ToÉrt_Int(),
                                        rekord["Fogaskerekű_megjegyzés"].ToStrTrim(),
                                        rekord["Hungária_engedély"].ToÉrt_Int(),
                                        rekord["Hungária_megjegyzés"].ToStrTrim(),
                                        rekord["Kelenföld_engedély"].ToÉrt_Int(),
                                        rekord["Kelenföld_megjegyzés"].ToStrTrim(),
                                        rekord["Száva_engedély"].ToÉrt_Int(),
                                        rekord["Száva_megjegyzés"].ToStrTrim(),
                                        rekord["Szépilona_engedély"].ToÉrt_Int(),
                                        rekord["Szépilona_megjegyzés"].ToStrTrim(),
                                        rekord["Zugló_engedély"].ToÉrt_Int(),
                                        rekord["Zugló_megjegyzés"].ToStrTrim(),
                                        rekord["Korlátlan"].ToStrTrim(),
                                        rekord["Autók_száma"].ToÉrt_Int(),
                                        rekord["I_engedély"].ToÉrt_Int(),
                                        rekord["II_engedély"].ToÉrt_Int(),
                                        rekord["III_engedély"].ToÉrt_Int(),
                                        rekord["Státus"].ToÉrt_Int(),
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["PDF"].ToStrTrim(),
                                        rekord["OKA"].ToStrTrim(),
                                        rekord["Érvényes"].ToÉrt_DaTeTime());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, Adat_Behajtás_Behajtási Adat)
        {
            try
            {
                string szöveg = "INSERT INTO alapadatok ( Sorszám, Szolgálatihely, Hrazonosító, Név, Rendszám, ";
                szöveg += "Angyalföld_engedély, Baross_engedély, Budafok_engedély, Ferencváros_engedély, Fogaskerekű_engedély, Hungária_engedély, Kelenföld_engedély, ";
                szöveg += "Száva_engedély, Szépilona_engedély, Zugló_engedély, Státus, Dátum, PDF, oka, ";
                szöveg += "Angyalföld_megjegyzés, Baross_megjegyzés, Budafok_megjegyzés, Ferencváros_megjegyzés, Fogaskerekű_megjegyzés, Hungária_megjegyzés, Kelenföld_megjegyzés, ";
                szöveg += "Száva_megjegyzés, Szépilona_megjegyzés, Zugló_megjegyzés, ";
                szöveg += "Korlátlan, Autók_száma,  Megjegyzés, I_engedély, II_engedély, III_engedély, érvényes )";
                szöveg += $" VALUES ( '{Adat.Sorszám}', ";//Sorszám
                szöveg += $" '{Adat.Szolgálatihely}', ";//Szolgálatihely
                szöveg += $" '{Adat.HRazonosító}', ";//Hrazonosító
                szöveg += $" '{Adat.Név}', ";//Név
                szöveg += $" '{Adat.Rendszám}', ";//Rendszám

                szöveg += $" {Adat.Angyalföld_engedély}, ";//Angyalföld_engedély,
                szöveg += $" {Adat.Baross_engedély}, ";      //Baross_engedély,
                szöveg += $" {Adat.Budafok_engedély}, ";      //Budafok_engedély,
                szöveg += $" {Adat.Ferencváros_engedély}, ";      //Ferencváros_engedély,
                szöveg += $" {Adat.Fogaskerekű_engedély}, ";      //Fogaskerekű_engedély,
                szöveg += $" {Adat.Hungária_engedély}, ";      //Hungária_engedély,
                szöveg += $" {Adat.Kelenföld_engedély}, ";      //Kelenföld_engedély,
                szöveg += $" {Adat.Száva_engedély}, ";      //"Száva_engedély,
                szöveg += $" {Adat.Szépilona_engedély}, ";      //Szépilona_engedély,
                szöveg += $" {Adat.Zugló_engedély}, ";      //Zugló_engedély

                szöveg += $" {Adat.Státus}, ";   //Státus
                szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', ";//Dátum
                szöveg += $"'{Adat.PDF}', ";// PDF
                szöveg += $"'{Adat.OKA}', "; //Oka

                szöveg += $" '{Adat.Angyalföld_megjegyzés}', "; //Angyalföld_megjegyzés,
                szöveg += $" '{Adat.Baross_megjegyzés}', "; //Baross_megjegyzés,
                szöveg += $" '{Adat.Budafok_megjegyzés}', "; //Budafok_megjegyzés,
                szöveg += $" '{Adat.Ferencváros_megjegyzés}', "; //Ferencváros_megjegyzés,
                szöveg += $" '{Adat.Fogaskerekű_megjegyzés}', "; //Fogaskerekű_megjegyzés,
                szöveg += $" '{Adat.Hungária_megjegyzés}', "; //Hungária_megjegyzés,
                szöveg += $" '{Adat.Kelenföld_megjegyzés}', "; //Kelenföld_megjegyzés,
                szöveg += $" '{Adat.Száva_megjegyzés}', "; //Száva_megjegyzés,
                szöveg += $" '{Adat.Szépilona_megjegyzés}', "; //Szépilona_megjegyzés,
                szöveg += $" '{Adat.Zugló_megjegyzés}', "; //Zugló_megjegyzés,

                szöveg += $"'{Adat.Korlátlan}', ";    //Korlátlan,
                szöveg += $"{Adat.Autók_száma}, ";    //Autók_száma,
                szöveg += $"'{Adat.Megjegyzés}', ";    //Megjegyzés,
                szöveg += $"{Adat.I_engedély}, ";     //I_engedély
                szöveg += $"{Adat.II_engedély}, ";     //II_engedély
                szöveg += $"{Adat.III_engedély}, ";     //III_engedély
                szöveg += $"'{Adat.Érvényes:yyyy.MM.dd}') ";    //érvényes
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

        public void Módosítás(string hely, Adat_Behajtás_Behajtási Adat)
        {
            try
            {
                string szöveg = "UPDATE alapadatok Set ";
                szöveg += $" Hrazonosító='{Adat.HRazonosító}', ";
                szöveg += $" Név='{Adat.Név}', ";
                szöveg += $" Szolgálatihely='{Adat.Szolgálatihely}', ";
                szöveg += $" Dátum='{Adat.Dátum:yyyy.MM.dd}', ";
                szöveg += $" PDF='{Adat.PDF}', ";
                szöveg += $" oka='{Adat.OKA}', ";
                szöveg += $" Korlátlan='{Adat.Korlátlan}', ";
                szöveg += $" Autók_száma={Adat.Autók_száma}, ";
                szöveg += $" Megjegyzés='{Adat.Megjegyzés}', ";
                szöveg += $" Angyalföld_engedély={Adat.Angyalföld_engedély}, ";
                szöveg += $" Baross_engedély={Adat.Baross_engedély}, ";
                szöveg += $" Budafok_engedély={Adat.Budafok_engedély}, ";
                szöveg += $" Ferencváros_engedély={Adat.Ferencváros_engedély}, ";
                szöveg += $" Fogaskerekű_engedély={Adat.Fogaskerekű_engedély}, ";
                szöveg += $" Hungária_engedély={Adat.Hungária_engedély}, ";
                szöveg += $" Kelenföld_engedély={Adat.Kelenföld_engedély}, ";
                szöveg += $" Száva_engedély={Adat.Száva_engedély}, ";
                szöveg += $" Szépilona_engedély={Adat.Szépilona_engedély}, ";
                szöveg += $" Zugló_engedély={Adat.Zugló_engedély}, ";

                szöveg += $" Angyalföld_megjegyzés='{Adat.Angyalföld_megjegyzés}', ";
                szöveg += $" Baross_megjegyzés='{Adat.Baross_megjegyzés}', ";
                szöveg += $" Budafok_megjegyzés='{Adat.Budafok_megjegyzés}', ";
                szöveg += $" Ferencváros_megjegyzés='{Adat.Ferencváros_megjegyzés}', ";
                szöveg += $" Fogaskerekű_megjegyzés='{Adat.Fogaskerekű_megjegyzés}', ";
                szöveg += $" Hungária_megjegyzés='{Adat.Hungária_megjegyzés}', ";
                szöveg += $" Kelenföld_megjegyzés='{Adat.Kelenföld_megjegyzés}', ";
                szöveg += $" Száva_megjegyzés='{Adat.Száva_megjegyzés}', ";
                szöveg += $" Szépilona_megjegyzés='{Adat.Szépilona_megjegyzés}', ";
                szöveg += $" Zugló_megjegyzés='{Adat.Zugló_megjegyzés}', ";

                szöveg += $" Státus={Adat.Státus}, ";
                szöveg += $" Rendszám='{Adat.Rendszám}', ";
                szöveg += $" érvényes='{Adat.Érvényes}' ";
                szöveg += $" WHERE sorszám='{Adat.Sorszám}'";
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

        public void Módosítás_Gondnok(string hely, string Telephely, int Gondnok, string Megjegyzés, string Sorszám)
        {
            try
            {
                string szöveg = "UPDATE alapadatok SET ";
                szöveg += $"{Telephely}_engedély={Gondnok}, ";
                szöveg += $"{Telephely}_megjegyzés='{Megjegyzés}'";
                szöveg += $" WHERE sorszám='{Sorszám}'";
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

        public void Módosítás_Státus(string hely, Adat_Behajtás_Behajtási Adat)
        {
            try
            {
                string szöveg = $"UPDATE alapadatok Set Státus={Adat.Státus}";
                szöveg += $" WHERE sorszám='{Adat.Sorszám}'";
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

        public void Módosítás_Szakszolgálat(string hely, string Szakszolg, int SzakszolgEng, string sorSzám)
        {
            try
            {
                string szöveg = $"UPDATE alapadatok SET {Szakszolg}={SzakszolgEng} WHERE sorszám='{sorSzám}'";
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
