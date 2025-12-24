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
    public class Kezelő_Eszköz
    {
        string hely;
        readonly string jelszó = "TóthKatalin";
        readonly string táblanév = "Adatok";

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Eszköz\Eszköz.mdb".KönyvSzerk();
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Eszköztábla(hely);
        }

        public List<Adat_Eszköz> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            List<Adat_Eszköz> Adatok = new List<Adat_Eszköz>();
            string szöveg = $"SELECT * FROM {táblanév} ";

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
                                Adat_Eszköz Adat = new Adat_Eszköz(
                                       rekord["Eszköz"].ToStrTrim(),
                                       rekord["Alszám"].ToStrTrim(),
                                       rekord["Megnevezés"].ToStrTrim(),
                                       rekord["Megnevezés_folyt"].ToStrTrim(),
                                       rekord["Gyártási_szám"].ToStrTrim(),
                                       rekord["Leltárszám"].ToStrTrim(),
                                       rekord["Leltár_dátuma"].ToÉrt_DaTeTime(),
                                       rekord["Mennyiség"].ToÉrt_Double(),
                                       rekord["Bázis_menny_egység"].ToStrTrim(),
                                       rekord["Aktiválás_dátuma"].ToÉrt_DaTeTime(),
                                       rekord["Telephely"].ToStrTrim(),
                                       rekord["Telephely_megnevezése"].ToStrTrim(),
                                       rekord["Helyiség"].ToStrTrim(),
                                       rekord["Helyiség_megnevezés"].ToStrTrim(),
                                       rekord["Gyár"].ToStrTrim(),
                                       rekord["Leltári_költséghely"].ToStrTrim(),
                                       rekord["Vonalkód"].ToStrTrim(),
                                       rekord["Leltár_forduló_nap"].ToÉrt_DaTeTime(),
                                       rekord["Szemügyi_törzsszám"].ToStrTrim(),
                                       rekord["Dolgozó_neve"].ToStrTrim(),
                                       rekord["Deaktiválás_dátuma"].ToÉrt_DaTeTime(),
                                       rekord["Eszközosztály"].ToStrTrim(),
                                       rekord["Üzletág"].ToStrTrim(),
                                       rekord["Cím"].ToStrTrim(),
                                       rekord["Költséghely"].ToStrTrim(),
                                       rekord["Felelős_költséghely"].ToStrTrim(),
                                       rekord["Régi_leltárszám"].ToStrTrim(),
                                       rekord["Vonalkódozható"].ToÉrt_Bool(),
                                       rekord["Rendszám_pályaszám"].ToStrTrim(),
                                       rekord["Épület_Szerszám"].ToStrTrim(),
                                       rekord["Épület_van"].ToÉrt_Bool(),
                                       rekord["Szerszám_van"].ToÉrt_Bool(),
                                       rekord["Státus"].ToÉrt_Bool()
                                       );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string Telephely, List<Adat_Eszköz> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Eszköz Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $" Helyiség='{Adat.Helyiség}', ";
                    szöveg += $" Helyiség_megnevezés='{Adat.Helyiség_megnevezés}', ";
                    szöveg += $" Szemügyi_törzsszám='{Adat.Szemügyi_törzsszám}', ";
                    szöveg += $" Dolgozó_neve='{Adat.Dolgozó_neve}', ";
                    szöveg += $" Költséghely='{Adat.Költséghely}', ";
                    szöveg += $" Leltárszám='{Adat.Leltárszám}', ";
                    szöveg += $" Felelős_költséghely='{Adat.Felelős_költséghely}' ";
                    szöveg += $"WHERE Eszköz='{Adat.Eszköz}'";
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

        public void Rögzítés(string Telephely, List<Adat_Eszköz> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Eszköz Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (";
                    szöveg += "Eszköz, Alszám, Megnevezés, Megnevezés_folyt, Gyártási_szám,";
                    szöveg += "Leltárszám, Leltár_dátuma, Mennyiség, Bázis_menny_egység, Aktiválás_dátuma,";
                    szöveg += " Telephely, Telephely_megnevezése, Helyiség, Helyiség_megnevezés,  Gyár,";
                    szöveg += " Leltári_költséghely, Vonalkód, Leltár_forduló_nap, Szemügyi_törzsszám, Dolgozó_neve,";
                    szöveg += " Deaktiválás_dátuma, Eszközosztály, Üzletág, Cím, Költséghely, ";
                    szöveg += "Felelős_költséghely, Régi_leltárszám, Vonalkódozható, Rendszám_pályaszám,";
                    szöveg += " Épület_Szerszám, Épület_van, Szerszám_van, státus) VALUES ( ";
                    szöveg += $"'{Adat.Eszköz}', '{Adat.Alszám}', '{Adat.Megnevezés}', '{Adat.Megnevezés_folyt}', '{Adat.Gyártási_szám}', ";
                    szöveg += $"'{Adat.Leltárszám}', '{Adat.Leltár_dátuma}', {Adat.Mennyiség.ToString().Replace(',', '.')}, '{Adat.Bázis_menny_egység}', '{Adat.Aktiválás_dátuma}', ";
                    szöveg += $"'{Adat.Telephely}', '{Adat.Telephely_megnevezése}', '{Adat.Helyiség}', '{Adat.Helyiség_megnevezés}', '{Adat.Gyár}', ";
                    szöveg += $"'{Adat.Leltári_költséghely}', '{Adat.Vonalkód}', '{Adat.Leltár_forduló_nap}', '{Adat.Szemügyi_törzsszám}', '{Adat.Dolgozó_neve}', ";
                    szöveg += $"'{Adat.Deaktiválás_dátuma}', '{Adat.Eszközosztály}', '{Adat.Üzletág}', '{Adat.Cím}', '{Adat.Költséghely}', ";
                    szöveg += $"'{Adat.Felelős_költséghely}', '{Adat.Régi_leltárszám}', {Adat.Vonalkódozható}, '{Adat.Rendszám_pályaszám}', ";
                    szöveg += $"'{Adat.Épület_Szerszám}', {Adat.Épület_van}, {Adat.Szerszám_van}, {Adat.Státus} )";
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


        //Elkopó

        public List<Adat_Eszköz> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Eszköz> Adatok = new List<Adat_Eszköz>();
            Adat_Eszköz Adat;

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
                                Adat = new Adat_Eszköz(
                                       rekord["Eszköz"].ToStrTrim(),
                                       rekord["Alszám"].ToStrTrim(),
                                       rekord["Megnevezés"].ToStrTrim(),
                                       rekord["Megnevezés_folyt"].ToStrTrim(),
                                       rekord["Gyártási_szám"].ToStrTrim(),
                                       rekord["Leltárszám"].ToStrTrim(),
                                       rekord["Leltár_dátuma"].ToÉrt_DaTeTime(),
                                       rekord["Mennyiség"].ToÉrt_Double(),
                                       rekord["Bázis_menny_egység"].ToStrTrim(),
                                       rekord["Aktiválás_dátuma"].ToÉrt_DaTeTime(),
                                       rekord["Telephely"].ToStrTrim(),
                                       rekord["Telephely_megnevezése"].ToStrTrim(),
                                       rekord["Helyiség"].ToStrTrim(),
                                       rekord["Helyiség_megnevezés"].ToStrTrim(),
                                       rekord["Gyár"].ToStrTrim(),
                                       rekord["Leltári_költséghely"].ToStrTrim(),
                                       rekord["Vonalkód"].ToStrTrim(),
                                       rekord["Leltár_forduló_nap"].ToÉrt_DaTeTime(),
                                       rekord["Szemügyi_törzsszám"].ToStrTrim(),
                                       rekord["Dolgozó_neve"].ToStrTrim(),
                                       rekord["Deaktiválás_dátuma"].ToÉrt_DaTeTime(),
                                       rekord["Eszközosztály"].ToStrTrim(),
                                       rekord["Üzletág"].ToStrTrim(),
                                       rekord["Cím"].ToStrTrim(),
                                       rekord["Költséghely"].ToStrTrim(),
                                       rekord["Felelős_költséghely"].ToStrTrim(),
                                       rekord["Régi_leltárszám"].ToStrTrim(),
                                       rekord["Vonalkódozható"].ToÉrt_Bool(),
                                       rekord["Rendszám_pályaszám"].ToStrTrim(),
                                       rekord["Épület_Szerszám"].ToStrTrim(),
                                       rekord["Épület_van"].ToÉrt_Bool(),
                                       rekord["Szerszám_van"].ToÉrt_Bool(),
                                       rekord["Státus"].ToÉrt_Bool()
                                       );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<string> Lista_EszközNév(string hely, string jelszó, string szöveg)
        {
            List<string> Adatok = new List<string>();
            string Adat;

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
                                Adat = rekord["Eszköz"].ToStrTrim();
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Eszköz Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Eszköz Adat = null;

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
                                Adat = new Adat_Eszköz(
                                       rekord["Eszköz"].ToStrTrim(),
                                       rekord["Alszám"].ToStrTrim(),
                                       rekord["Megnevezés"].ToStrTrim(),
                                       rekord["Megnevezés_folyt"].ToStrTrim(),
                                       rekord["Gyártási_szám"].ToStrTrim(),
                                       rekord["Leltárszám"].ToStrTrim(),
                                       rekord["Leltár_dátuma"].ToÉrt_DaTeTime(),
                                       rekord["Mennyiség"].ToÉrt_Double(),
                                       rekord["Bázis_menny_egység"].ToStrTrim(),
                                       rekord["Aktiválás_dátuma"].ToÉrt_DaTeTime(),
                                       rekord["Telephely"].ToStrTrim(),
                                       rekord["Telephely_megnevezése"].ToStrTrim(),
                                       rekord["Helyiség"].ToStrTrim(),
                                       rekord["Helyiség_megnevezés"].ToStrTrim(),
                                       rekord["Gyár"].ToStrTrim(),
                                       rekord["Leltári_költséghely"].ToStrTrim(),
                                       rekord["Vonalkód"].ToStrTrim(),
                                       rekord["Leltár_forduló_nap"].ToÉrt_DaTeTime(),
                                       rekord["Szemügyi_törzsszám"].ToStrTrim(),
                                       rekord["Dolgozó_neve"].ToStrTrim(),
                                       rekord["Deaktiválás_dátuma"].ToÉrt_DaTeTime(),
                                       rekord["Eszközosztály"].ToStrTrim(),
                                       rekord["Üzletág"].ToStrTrim(),
                                       rekord["Cím"].ToStrTrim(),
                                       rekord["Költséghely"].ToStrTrim(),
                                       rekord["Felelős_költséghely"].ToStrTrim(),
                                       rekord["Régi_leltárszám"].ToStrTrim(),
                                       rekord["Vonalkódozható"].ToÉrt_Bool(),
                                       rekord["Rendszám_pályaszám"].ToStrTrim(),
                                       rekord["Épület_Szerszám"].ToStrTrim(),
                                       rekord["Épület_van"].ToÉrt_Bool(),
                                       rekord["Szerszám_van"].ToÉrt_Bool(),
                                       rekord["Státus"].ToÉrt_Bool()
                                       );
                            }
                        }
                    }
                }
            }
            return Adat;
        }


    }
}
