﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
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

        //Elkopó
        public List<Adat_TW6000_Ütemezés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
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

        public Adat_TW6000_Ütemezés Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_TW6000_Ütemezés Adat = null;

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
                        }
                    }
                }
            }
            return Adat;
        }
    }

}
