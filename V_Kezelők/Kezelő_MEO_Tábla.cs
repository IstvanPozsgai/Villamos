using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_MEO_Tábla
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\kerékmérés.mdb";
        readonly string jelszó = "rudolfg";
        readonly string táblanév = "tábla";

        public List<Adat_MEO_Tábla> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY név, típus";
            List<Adat_MEO_Tábla> Adatok = new List<Adat_MEO_Tábla>();
            Adat_MEO_Tábla Adat;

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
                                Adat = new Adat_MEO_Tábla(
                                        rekord["Név"].ToStrTrim(),
                                        rekord["Típus"].ToStrTrim()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_MEO_Tábla Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév}  (név, típus ) VALUES (";
                szöveg += $"'{Adat.Név}', ";
                szöveg += $"'{Adat.Típus}') ";
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

        public void Törlés(Adat_MEO_Tábla Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM {táblanév} WHERE név='{Adat.Név}'";
                szöveg += $" AND típus='{Adat.Típus}'";
                MyA.ABtörlés(hely, jelszó, szöveg);
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
