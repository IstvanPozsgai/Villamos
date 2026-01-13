using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_SérülésSzöveg
    {
        string hely;
        readonly string jelszó = "kismalac";
        readonly string táblanév = "tábla";

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\segéd\sérülés.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Sérüléstábla(hely);
        }

        public List<Adat_Kiegészítő_SérülésSzöveg> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = $"SELECT * FROM {táblanév}";
            Adat_Kiegészítő_SérülésSzöveg Adat;
            List<Adat_Kiegészítő_SérülésSzöveg> Adatok = new List<Adat_Kiegészítő_SérülésSzöveg>();

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
                                Adat = new Adat_Kiegészítő_SérülésSzöveg(
                                           rekord["Id"].ToÉrt_Int(),
                                           rekord["Szöveg1"].ToStrTrim(),
                                           rekord["Szöveg2"].ToStrTrim(),
                                           rekord["Szöveg3"].ToStrTrim(),
                                           rekord["Szöveg4"].ToStrTrim(),
                                           rekord["Szöveg5"].ToStrTrim(),
                                           rekord["Szöveg6"].ToStrTrim(),
                                           rekord["Szöveg7"].ToStrTrim(),
                                           rekord["Szöveg8"].ToStrTrim(),
                                           rekord["Szöveg9"].ToStrTrim(),
                                           rekord["Szöveg10"].ToStrTrim(),
                                           rekord["Szöveg11"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Kiegészítő_SérülésSzöveg Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"INSERT INTO {táblanév} (id, szöveg1, szöveg2, szöveg3, szöveg4, szöveg5, szöveg6, szöveg7, szöveg8, szöveg9, szöveg10, szöveg11) VALUES (";
                szöveg += $"{Adat.Id}, ";
                szöveg += $"'{Adat.Szöveg1}', ";
                szöveg += $"'{Adat.Szöveg2}', ";
                szöveg += $"'{Adat.Szöveg3}', ";
                szöveg += $"'{Adat.Szöveg4}', ";
                szöveg += $"'{Adat.Szöveg5}', ";
                szöveg += $"'{Adat.Szöveg6}', ";
                szöveg += $"'{Adat.Szöveg7}', ";
                szöveg += $"'{Adat.Szöveg8}', ";
                szöveg += $"'{Adat.Szöveg9}', ";
                szöveg += $"'{Adat.Szöveg10}', ";
                szöveg += $"'{Adat.Szöveg11}') ";
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

        public void Módosítás(string Telephely, Adat_Kiegészítő_SérülésSzöveg Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"szöveg1='{Adat.Szöveg1}', ";
                szöveg += $"szöveg2='{Adat.Szöveg2}', ";
                szöveg += $"szöveg3='{Adat.Szöveg3}', ";
                szöveg += $"szöveg4='{Adat.Szöveg4}', ";
                szöveg += $"szöveg5='{Adat.Szöveg5}', ";
                szöveg += $"szöveg6='{Adat.Szöveg6}', ";
                szöveg += $"szöveg7='{Adat.Szöveg7}', ";
                szöveg += $"szöveg8='{Adat.Szöveg8}', ";
                szöveg += $"szöveg9='{Adat.Szöveg9}', ";
                szöveg += $"szöveg10='{Adat.Szöveg10}', ";
                szöveg += $"szöveg11='{Adat.Szöveg11}' ";
                szöveg += $" WHERE [id] ={Adat.Id}";
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
