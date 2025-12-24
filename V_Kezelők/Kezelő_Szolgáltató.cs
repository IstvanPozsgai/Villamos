using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Szolgáltató
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
        readonly string jelszó = "Mocó";
        readonly string táblanév = "TakarításSzolgáltató";

        public List<Adat_Szolgáltató> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév}";
            Adat_Szolgáltató Adat;
            List<Adat_Szolgáltató> Adatok = new List<Adat_Szolgáltató>();

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
                                Adat = new Adat_Szolgáltató(
                                           rekord["ID"].ToÉrt_Int (),
                                           rekord["SzerződésSzám"].ToStrTrim(),
                                           rekord["IratEleje"].ToStrTrim(),
                                           rekord["IratVége"].ToStrTrim(),
                                           rekord["Aláíró"].ToStrTrim(),
                                           rekord["CégNévAlá"].ToStrTrim(),
                                           rekord["CégCím"].ToStrTrim(),
                                           rekord["CégAdó"].ToStrTrim(),
                                           rekord["CégHosszúNév"].ToStrTrim(),
                                           rekord["Cégjegyzékszám"].ToStrTrim(),
                                           rekord["CsoportAzonosító"].ToStrTrim()
                                           );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(Adat_Szolgáltató Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET  ";
                szöveg += $"SzerződésSzám='{Adat.SzerződésSzám}',";
                szöveg += $"IratEleje='{Adat.IratEleje}',";
                szöveg += $"IratVége='{Adat.IratVége}',";
                szöveg += $"Aláíró='{Adat.Aláíró}',";
                szöveg += $"CégNévAlá='{Adat.CégNévAlá}',";
                szöveg += $"CégCím='{Adat.CégCím}',";
                szöveg += $"CégAdó='{Adat.CégAdó}',";
                szöveg += $"CégHosszúNév='{Adat.CégHosszúNév}',";
                szöveg += $"Cégjegyzékszám='{Adat.Cégjegyzékszám}',";
                szöveg += $"CsoportAzonosító='{Adat.CsoportAzonosító}'";
                szöveg += $" WHERE [ID]={Adat.ID}";
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
