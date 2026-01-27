using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Feorszámok
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kiegészítő2.mdb";
        readonly string jelszó = "Mocó";
        readonly string táblanév = "feorszámok";

        public Kezelő_Kiegészítő_Feorszámok()
        {
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }

        public List<Adat_Kiegészítő_Feorszámok> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY sorszám";
            Adat_Kiegészítő_Feorszámok Adat;
            List<Adat_Kiegészítő_Feorszámok> Adatok = new List<Adat_Kiegészítő_Feorszámok>();

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
                                Adat = new Adat_Kiegészítő_Feorszámok(
                                           rekord["sorszám"].ToÉrt_Long(),
                                           rekord["Feorszám"].ToStrTrim(),
                                           rekord["feormegnevezés"].ToStrTrim(),
                                           rekord["Státus"].ToÉrt_Long()
                                           );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Kiegészítő_Feorszámok Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (Feorszám, feormegnevezés, státus) VALUES";
                szöveg += $"('{Adat.Feorszám}', '{Adat.Feormegnevezés}', {Adat.Státus})";
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

        public void Módosítás(Adat_Kiegészítő_Feorszámok Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév}  SET";
                szöveg += $" feorszám='{Adat.Feorszám}', ";
                szöveg += $" feormegnevezés='{Adat.Feormegnevezés}', ";
                szöveg += $" státus={Adat.Státus} ";
                szöveg += $"WHERE sorszám={Adat.Sorszám}";
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

        public void Csere(long Sorszám1)
        {
            try
            {
                List<Adat_Kiegészítő_Feorszámok> Adatok = Lista_Adatok();
                Adat_Kiegészítő_Feorszámok Adat1 = Adatok.Find(x => x.Sorszám == Sorszám1);
                Adat_Kiegészítő_Feorszámok Adat2 = Adatok.Find(x => x.Sorszám == Sorszám1 - 1);

                string szöveg = $"UPDATE {táblanév}  SET";
                szöveg += $" feorszám='{Adat2.Feorszám}', ";
                szöveg += $" feormegnevezés='{Adat2.Feormegnevezés}', ";
                szöveg += $" státus={Adat2.Státus} ";
                szöveg += $"WHERE sorszám={Adat1.Sorszám}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                szöveg = $"UPDATE {táblanév}  SET";
                szöveg += $" feorszám='{Adat1.Feorszám}', ";
                szöveg += $" feormegnevezés='{Adat1.Feormegnevezés}', ";
                szöveg += $" státus={Adat1.Státus} ";
                szöveg += $"WHERE sorszám={Adat2.Sorszám}";
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
