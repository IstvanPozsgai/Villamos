using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Turnusok
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\kiegészítő1.mdb";
        readonly string jelszó = "Mocó";
        readonly string táblanév = "turnusok";

        public Kezelő_Kiegészítő_Turnusok()
        {
            //Nincs kidolgozva
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.Jogosítványtáblalétrehozás(hely);
        }

        public List<Adat_Kiegészítő_Turnusok> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév} order by csoport";
            Adat_Kiegészítő_Turnusok Adat;
            List<Adat_Kiegészítő_Turnusok> Adatok = new List<Adat_Kiegészítő_Turnusok>();

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
                                Adat = new Adat_Kiegészítő_Turnusok(
                                           rekord["csoport"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Kiegészítő_Turnusok Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (csoport)";
                szöveg += $" VALUES ('{Adat.Csoport}')";
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

        public void Törlés(Adat_Kiegészítő_Turnusok Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM {táblanév} WHERE csoport='{Adat.Csoport}'";
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
