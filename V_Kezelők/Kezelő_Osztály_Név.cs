using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;


namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Osztály_Név
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\osztály.mdb";
        readonly string jelszó = "kéménybe";
        public List<Adat_Osztály_Név> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Osztály_Név> Adatok = new List<Adat_Osztály_Név>();
            Adat_Osztály_Név Adat;

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
                                Adat = new Adat_Osztály_Név(
                                     MyF.Érték_INT(rekord["id"].ToStrTrim()),
                                     rekord["Osztálynév"].ToStrTrim(),
                                     rekord["Osztálymező"].ToStrTrim(),
                                     rekord["Használatban"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public List<Adat_Osztály_Név> Lista_Adat()
        {
            string szöveg = "SELECT * FROM osztálytábla order by id";
            List<Adat_Osztály_Név> Adatok = new List<Adat_Osztály_Név>();
            Adat_Osztály_Név Adat;

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
                                Adat = new Adat_Osztály_Név(
                                     MyF.Érték_INT(rekord["id"].ToStrTrim()),
                                     rekord["Osztálynév"].ToStrTrim(),
                                     rekord["Osztálymező"].ToStrTrim(),
                                     rekord["Használatban"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
        /// <summary>
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(Adat_Osztály_Név Adat)
        {
            try
            {
                string szöveg = "UPDATE  osztálytábla SET";
                szöveg += $" osztálynév='{Adat.Osztálynév}', ";
                szöveg += $" osztálymező='{Adat.Osztálymező}', ";
                szöveg += $" használatban='{Adat.Használatban}' ";
                szöveg += $" where id={Adat.Id} ";
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
