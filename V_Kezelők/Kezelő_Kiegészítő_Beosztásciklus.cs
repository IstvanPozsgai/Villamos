using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Beosztásciklus
    {
        readonly string jelszó = "Mocó";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő2.mdb";

        public Kezelő_Kiegészítő_Beosztásciklus()
        {
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.  (hely.KönyvSzerk());
        }


        public List<Adat_Kiegészítő_Beosztásciklus> Lista_Adatok(string Tábla)
        {
            string szöveg = $"SELECT * FROM {Tábla} ORDER BY id";
            List<Adat_Kiegészítő_Beosztásciklus> Adatok = new List<Adat_Kiegészítő_Beosztásciklus>();
            Adat_Kiegészítő_Beosztásciklus Adat;

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
                                Adat = new Adat_Kiegészítő_Beosztásciklus(
                                       rekord["Id"].ToÉrt_Int(),
                                       rekord["Beosztáskód"].ToStrTrim(),
                                       rekord["Hétnapja"].ToStrTrim(),
                                       rekord["Beosztásszöveg"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Tábla, Adat_Kiegészítő_Beosztásciklus Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {Tábla} (hétnapja, beosztáskód, beosztásszöveg) VALUES (";
                szöveg += $"'{Adat.Hétnapja}', ";
                szöveg += $"'{Adat.Beosztáskód}";
                szöveg += $"{Adat.Beosztásszöveg} )";
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

        public void Módosítás(string Tábla, Adat_Kiegészítő_Beosztásciklus Adat)
        {
            try
            {
                string szöveg = $"UPDATE {Tábla} SET ";
                szöveg += $" hétnapja='{Adat.Hétnapja}', ";
                szöveg += $" beosztáskód='{Adat.Beosztáskód}', ";
                szöveg += $" beosztásszöveg='{Adat.Beosztásszöveg}' ";
                szöveg += $" WHERE id={Adat.Id}";
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

        public void Törlés(string Tábla, Adat_Kiegészítő_Beosztásciklus Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM {Tábla} WHERE  id={Adat.Id}";
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
