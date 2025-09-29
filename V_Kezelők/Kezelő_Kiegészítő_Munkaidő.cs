﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Munkaidő
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";
        readonly string jelszó = "Mocó";
        readonly string táblanév = "munkaidő";

        public Kezelő_Kiegészítő_Munkaidő()
        {
            // if (!File.Exists(hely)) Adatbázis_Létrehozás   (hely.KönyvSzerk());
        }


        public List<Adat_Kiegészítő_Munkaidő> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY munkarendelnevezés";
            List<Adat_Kiegészítő_Munkaidő> Adatok = new List<Adat_Kiegészítő_Munkaidő>();
            Adat_Kiegészítő_Munkaidő Adat;

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
                                Adat = new Adat_Kiegészítő_Munkaidő(
                                     rekord["munkarendelnevezés"].ToStrTrim(),
                                     rekord["munkaidő"].ToÉrt_Double()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Kiegészítő_Munkaidő Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (munkaidő, munkarendelnevezés) VALUES (";
                szöveg += $"{Adat.Munkaidő}, ";
                szöveg += $"'{Adat.Munkarendelnevezés}')";
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

        public void Módosítás(Adat_Kiegészítő_Munkaidő Adat)
        {
            try
            {
                string szöveg = $" UPDATE {táblanév} SET ";
                szöveg += $" munkaidő={Adat.Munkaidő}";
                szöveg += $" WHERE munkarendelnevezés='{Adat.Munkarendelnevezés}'";

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

        public void Törlés(string Munkarendelnevezés)
        {
            try
            {
                string szöveg = $"DELETE FROM {táblanév} WHERE munkarendelnevezés='{Munkarendelnevezés}'";
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
