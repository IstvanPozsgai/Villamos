﻿using System;
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
    public class Kezelő_TW6000_Ütemezés
    {
        readonly string hely = $@"{Application.StartupPath}\főmérnökség\adatok\villamos4TW.mdb";
        readonly string jelszó = "czapmiklós";
        readonly Kezelő_TW600_ÜtemNapló KézNapló = new Kezelő_TW600_ÜtemNapló();

        public Kezelő_TW6000_Ütemezés()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.TW6000tábla(hely.KönyvSzerk());
        }

        public List<Adat_TW6000_Ütemezés> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM ütemezés";
            List<Adat_TW6000_Ütemezés> Adatok = new List<Adat_TW6000_Ütemezés>();
            Adat_TW6000_Ütemezés Adat;

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
                                Adat = new Adat_TW6000_Ütemezés(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Ciklusrend"].ToStrTrim(),
                                        rekord["Elkészült"].ToÉrt_Bool(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Long(),
                                        rekord["velkészülés"].ToÉrt_DaTeTime(),
                                        rekord["vesedékesség"].ToÉrt_DaTeTime(),
                                        rekord["vizsgfoka"].ToStrTrim(),
                                        rekord["vsorszám"].ToÉrt_Long(),
                                        rekord["vütemezés"].ToÉrt_DaTeTime(),
                                        rekord["Vvégezte"].ToStrTrim()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(List<Adat_TW6000_Ütemezés> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_TW6000_Ütemezés Adat in Adatok)
                {
                    string szöveg = "INSERT INTO ütemezés (azonosító, ciklusrend, elkészült, megjegyzés, ";
                    szöveg += " státus, velkészülés, vesedékesség, vizsgfoka, ";
                    szöveg += " vsorszám, vütemezés, vvégezte) VALUES (";
                    szöveg += $"'{Adat.Azonosító}', ";
                    szöveg += $"'{Adat.Ciklusrend}', ";
                    szöveg += $"{Adat.Elkészült},";
                    szöveg += $" '{Adat.Megjegyzés}',";
                    szöveg += $" {Adat.Státus},";
                    szöveg += $" '{Adat.Velkészülés:yyyy.MM.dd}', ";
                    szöveg += $"'{Adat.Vesedékesség:yyyy.MM.dd}', ";
                    szöveg += $"'{Adat.Vizsgfoka}', ";
                    szöveg += $"{Adat.Vsorszám}, ";
                    szöveg += $"'{Adat.Vütemezés:yyyy.MM.dd}', ";
                    szöveg += $"'{Adat.Vvégezte}' )";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
                //Naplózunk
                KézNapló.Rögzítés(DateTime.Now.Year, Adatok);
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

        public void Rögzítés(Adat_TW6000_Ütemezés Adat)
        {
            try
            {
                string szöveg = "INSERT INTO ütemezés (azonosító, ciklusrend, elkészült, megjegyzés, ";
                szöveg += " státus, velkészülés, vesedékesség, vizsgfoka, ";
                szöveg += " vsorszám, vütemezés, vvégezte) VALUES (";
                szöveg += $"'{Adat.Azonosító}', "; // azonosító
                szöveg += $"'{Adat.Ciklusrend}', "; // ciklusrend
                szöveg += $"{Adat.Elkészült}, ";
                szöveg += $" '{Adat.Megjegyzés}', "; // megjegyzés
                szöveg += $"{Adat.Státus}, "; // státus 
                szöveg += $" '{Adat.Velkészülés:yyyy.MM.dd}', "; // velkészülés
                szöveg += $"'{Adat.Vesedékesség:yyyy.MM.dd}', "; // vesedékesség
                szöveg += $"'{Adat.Vizsgfoka}', "; // vizsgfoka
                szöveg += $"{Adat.Vsorszám}, "; // vsorszám
                szöveg += $"'{Adat.Vütemezés:yyyy.MM.dd}', ";  // vütemezés
                szöveg += $"'{Adat.Vvégezte}') "; // vvégezte
                MyA.ABMódosítás(hely, jelszó, szöveg);
                KézNapló.Rögzítés(DateTime.Now.Year, Adat);
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


        public void Módosítás(Adat_TW6000_Ütemezés Adat)
        {
            try
            {
                string szöveg = $"UPDATE ütemezés SET ciklusrend='{Adat.Ciklusrend}', ";
                szöveg += $"elkészült={Adat.Elkészült}, ";
                szöveg += $"megjegyzés='{Adat.Megjegyzés}', ";
                szöveg += $"státus={Adat.Státus}, ";
                szöveg += $"velkészülés='{Adat.Velkészülés:yyyy.MM.dd}', ";
                szöveg += $"vizsgfoka='{Adat.Vizsgfoka}', ";
                szöveg += $"vsorszám={Adat.Vsorszám}, ";
                szöveg += $"vütemezés='{Adat.Vütemezés:yyyy.MM.dd}', ";
                szöveg += $"vvégezte='{Adat.Vvégezte}'";
                szöveg += $"WHERE azonosító='{Adat.Azonosító}'";
                szöveg += $" and vesedékesség=#{Adat.Vesedékesség:MM-dd-yyyy}#";
                MyA.ABMódosítás(hely, jelszó, szöveg);

                KézNapló.Rögzítés(DateTime.Now.Year, Adat);
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

        public void Módosítás_ütem(Adat_TW6000_Ütemezés Adat, long OldStátus)
        {
            try
            {
                string szöveg = "UPDATE ütemezés SET ";
                szöveg += $" megjegyzés='Előjegyezve: {Adat.Megjegyzés}',";
                szöveg += $" státus={Adat.Státus} ";
                szöveg += $" WHERE  vütemezés=#{Adat.Vütemezés:MM-dd-yyyy}# ";
                szöveg += $" And státus={OldStátus} ";
                szöveg += $" AND azonosító='{Adat.Azonosító}'";
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

        public void Módosítás(List<Adat_TW6000_Ütemezés> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                List<Adat_TW6000_Ütemezés> AdatokNaplóhoz = new List<Adat_TW6000_Ütemezés>();
                List<Adat_TW6000_Ütemezés> AdatokTárolt = Lista_Adatok();
                foreach (Adat_TW6000_Ütemezés Adat in Adatok)
                {
                    string szöveg = "UPDATE ütemezés SET ";
                    szöveg += $" státus={Adat.Státus},";
                    szöveg += $" megjegyzés ='{Adat.Megjegyzés}' ";
                    szöveg += $" WHERE azonosító='{Adat.Azonosító}'";
                    szöveg += $" AND vütemezés=#{Adat.Vütemezés:MM-dd-yyyy}#";
                    SzövegGy.Add(szöveg);

                    //Naplófájlhoz megkeressü az adatokat
                    Adat_TW6000_Ütemezés Elem = (from a in AdatokTárolt
                                                 where a.Azonosító == Adat.Azonosító && a.Vütemezés == Adat.Vütemezés
                                                 select a).FirstOrDefault();
                    if (Elem != null)
                    {
                        Adat_TW6000_Ütemezés AdatNapló = new Adat_TW6000_Ütemezés(
                            Elem.Azonosító,
                            Elem.Ciklusrend,
                            Elem.Elkészült,
                            Adat.Megjegyzés,
                            Adat.Státus,
                            Elem.Velkészülés,
                            Elem.Vesedékesség,
                            Elem.Vizsgfoka,
                            Elem.Vsorszám,
                            Adat.Vütemezés,
                            Adat.Vvégezte
                        );
                        AdatokNaplóhoz.Add(AdatNapló);
                    }
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
                //Naplózunk
                KézNapló.Rögzítés(DateTime.Now.Year, AdatokNaplóhoz);
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

        public void Törlés(List<Adat_TW6000_Ütemezés> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_TW6000_Ütemezés Adat in Adatok)
                {
                    string szöveg = $"DELETE FROM  ütemezés WHERE azonosító='{Adat.Azonosító}'";
                    szöveg += $" AND vütemezés=#{Adat.Vütemezés:MM-dd-yyyy}#";
                    szöveg += $" AND státus={Adat.Státus}";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABtörlés(hely, jelszó, SzövegGy);
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
