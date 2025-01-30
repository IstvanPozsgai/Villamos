using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{

    public class Kezelő_Behajtás_Behajtási_Napló
    {
        readonly string jelszó = "forgalmirendszám";
        public List<Adat_Behajtás_Behajtási_Napló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Behajtás_Behajtási_Napló> Adatok = new List<Adat_Behajtás_Behajtási_Napló>();
            Adat_Behajtás_Behajtási_Napló Adat;

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
                                Adat = new Adat_Behajtás_Behajtási_Napló(
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
                                        rekord["ID"].ToÉrt_Int(),
                                        rekord["Rögzítette"].ToStrTrim(),
                                        rekord["Rögzítésdátuma"].ToÉrt_DaTeTime(),
                                        rekord["Érvényes"].ToÉrt_DaTeTime());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Behajtás_Behajtási_Napló> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * FROM alapadatok";
            List<Adat_Behajtás_Behajtási_Napló> Adatok = new List<Adat_Behajtás_Behajtási_Napló>();
            Adat_Behajtás_Behajtási_Napló Adat;

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
                                Adat = new Adat_Behajtás_Behajtási_Napló(
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
                                        rekord["ID"].ToÉrt_Int(),
                                        rekord["Rögzítette"].ToStrTrim(),
                                        rekord["Rögzítésdátuma"].ToÉrt_DaTeTime(),
                                        rekord["Érvényes"].ToÉrt_DaTeTime());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        /// <summary>
        /// Utolsó rögzített id szám
        /// </summary>
        /// <param name="hely"></param>
        /// <returns></returns>
        public double Napló_Id(string hely)
        {
            double válasz = 0;
            try
            {
                List<Adat_Behajtás_Behajtási_Napló> Adatok = Lista_Adatok(hely);
                if (Adatok == null) return válasz;
                válasz = Adatok.Max(a => a.ID);
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

        private double Sorszám(string hely)
        {
            double válasz = 1;
            try
            {
                List<Adat_Behajtás_Behajtási_Napló> Adatok = Lista_Adatok(hely);
                if (Adatok == null) return válasz;
                válasz = Adatok.Max(a => a.ID) + 1;
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

        public void Rögzítés(string hely, Adat_Behajtás_Behajtási_Napló Adat)
        {
            try
            {
                string szöveg = "INSERT INTO alapadatok ( Sorszám, Szolgálatihely, Hrazonosító, Név, Rendszám, ";
                szöveg += "Angyalföld_engedély, Baross_engedély, Budafok_engedély, Ferencváros_engedély, Fogaskerekű_engedély, Hungária_engedély, Kelenföld_engedély, ";
                szöveg += "Száva_engedély, Szépilona_engedély, Zugló_engedély, Státus, Dátum, PDF, oka, ";
                szöveg += "Angyalföld_megjegyzés, Baross_megjegyzés, Budafok_megjegyzés, Ferencváros_megjegyzés, Fogaskerekű_megjegyzés, Hungária_megjegyzés, Kelenföld_megjegyzés, ";
                szöveg += "Száva_megjegyzés, Szépilona_megjegyzés, Zugló_megjegyzés, ";
                szöveg += "Korlátlan, Autók_száma,  Megjegyzés, I_engedély, II_engedély, III_engedély,ID, Rögzítette, rögzítésdátuma, érvényes )";
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
                szöveg += $"{Sorszám(hely)}, ";    //ID
                szöveg += $"'{Adat.Rögzítette}', ";  //Rögzítette
                szöveg += $"'{Adat.Rögzítésdátuma}', "; //  rögzítésdátuma
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

        public void Rögzítés_Gondnok(string hely, string Telephely, int Gondnok, string Megjegyzés, string sorszám)
        {
            try
            {
                string szöveg = $"INSERT INTO alapadatok ( Sorszám, {Telephely}_engedély, {Telephely}_megjegyzés, ID, Rögzítette, rögzítésdátuma )";
                szöveg += $" VALUES ( '{sorszám}', {Gondnok}, '{Megjegyzés}', {Sorszám(hely)}, '{Program.PostásNév.Trim()}', '{DateTime.Now}') ";
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

        public void Rögzítés_Státus(string hely, Adat_Behajtás_Behajtási_Napló Adat)
        {
            try
            {
                string szöveg = "INSERT INTO alapadatok ( Sorszám, státus, ID, Rögzítette, rögzítésdátuma )";
                szöveg += $" VALUES ('{Adat.Sorszám}', {Adat.Státus}, {Sorszám(hely)},'{Adat.Rögzítette}', '{Adat.Rögzítésdátuma}') ";
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

        public void Rögzítés_Szakszolgálat(string hely, string Szakszolg, int SzakszolgEng, string sorSzám)
        {
            try
            {
                string szöveg = $"INSERT INTO alapadatok ( Sorszám, {Szakszolg}, ID, Rögzítette, rögzítésdátuma )";
                szöveg += $" VALUES ( '{sorSzám}', {SzakszolgEng}, {Sorszám(hely)}, '{Program.PostásNév.Trim()}', '{DateTime.Now}') ";
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
