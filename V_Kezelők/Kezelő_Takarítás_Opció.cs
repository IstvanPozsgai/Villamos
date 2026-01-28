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
    public class Kezelő_Takarítás_Opció
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Takarítás\Opcionális.mdb";
        readonly string jelszó = "seprűéslapát";
        readonly string táblanév = "TakarításOpcionális";

        public Kezelő_Takarítás_Opció()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.ÉpülettakarításOpcionálislétrehozás(hely.KönyvSzerk());
        }

        public List<Adat_Takarítás_Opció> Lista_Adatok()
        {
            List<Adat_Takarítás_Opció> Adatok = new List<Adat_Takarítás_Opció>();
            Adat_Takarítás_Opció Adat;
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY ID";
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
                                Adat = new Adat_Takarítás_Opció(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Megnevezés"].ToStrTrim(),
                                        rekord["Mennyisége"].ToString(),
                                        rekord["Ár"].ToÉrt_Double(),
                                        rekord["Kezdet"].ToÉrt_DaTeTime(),
                                        rekord["Vég"].ToÉrt_DaTeTime()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzít(Adat_Takarítás_Opció Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (Id, Megnevezés, Mennyisége, Ár, Kezdet, Vég) VALUES (";
                szöveg += $"{Adat.Id}, '{Adat.Megnevezés}', '{Adat.Mennyisége}', {Adat.Ár}, '{Adat.Kezdet.ToShortDateString()}', '{Adat.Vég.ToShortDateString()}')";
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

        public void Rögzít(List<Adat_Takarítás_Opció> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Takarítás_Opció Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (Id, Megnevezés, Mennyisége, Ár, Kezdet, Vég) VALUES (";
                    szöveg += $"{Adat.Id}, '{Adat.Megnevezés}', '{Adat.Mennyisége}', {Adat.Ár}, '{Adat.Kezdet.ToShortDateString()}', '{Adat.Vég.ToShortDateString()}')";
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

        public void Módosít(Adat_Takarítás_Opció Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév}  SET ";
                szöveg += $"Megnevezés='{Adat.Megnevezés}', ";
                szöveg += $"Mennyisége='{Adat.Mennyisége}', ";
                szöveg += $"Ár={Adat.Ár}, ";
                szöveg += $"Kezdet='{Adat.Kezdet.ToShortDateString()}', ";
                szöveg += $"Vég='{Adat.Vég.ToShortDateString()}' ";
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

        public void Módosít(List<Adat_Takarítás_Opció> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Takarítás_Opció Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév}  SET ";
                    szöveg += $"Vég='{Adat.Vég.ToShortDateString()}' ";
                    szöveg += $" WHERE id={Adat.Id}";
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
    }
}
