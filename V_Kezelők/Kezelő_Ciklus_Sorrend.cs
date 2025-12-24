using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.V_Kezelők
{
    public class Kezelő_Ciklus_Sorrend
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\ciklus.mdb";
        readonly string jelszó = "pocsaierzsi";
        readonly string táblanév = "CiklusSorrendtábla";

        public Kezelő_Ciklus_Sorrend()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Ciklusrendtábla(hely.KönyvSzerk());
        }

        public List<Adat_Ciklus_Sorrend> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY JárműTípus,sorszám";
            List<Adat_Ciklus_Sorrend> Adatok = new List<Adat_Ciklus_Sorrend>();

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat))
                {
                    Kapcsolat.Open();
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {
                        if (rekord.HasRows)
                        {
                            while (rekord.Read())
                            {
                                Adat_Ciklus_Sorrend Adat = new Adat_Ciklus_Sorrend(
                                        rekord["Sorszám"].ToÉrt_Int(),
                                        rekord["JárműTípus"].ToStrTrim(),
                                        rekord["CiklusNév"].ToStrTrim()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Döntés(Adat_Ciklus_Sorrend Adat)
        {
            try
            {
                List<Adat_Ciklus_Sorrend> Adatok = Lista_Adatok();
                Adat_Ciklus_Sorrend ADAT = Adatok.Where(a => a.JárműTípus == Adat.JárműTípus && a.CiklusNév == Adat.CiklusNév).FirstOrDefault();
                if (ADAT == null)
                {
                    List<Adat_Ciklus_Sorrend> Szűrt = (from a in Adatok
                                                       where a.JárműTípus == Adat.JárműTípus
                                                       select a).ToList();
                    //Ha minusz -1-el érkezik akkor az utolsó sorszámot kell adni neki
                    int Id = Adat.Sorszám;
                    if (Adat.Sorszám < 0)
                        Id = Szűrt.Count > 0 ? Adatok.Max(a => a.Sorszám) + 1 : 1;
                    Rögzítés(Adat, Id);
                }
                else
                    Módosítás(Adat);
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

        public void Rögzítés(Adat_Ciklus_Sorrend Adat, int Id)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} ( Sorszám, JárműTípus, CiklusNév) VALUES (";
                szöveg += $"{Id}, ";
                szöveg += $"'{Adat.JárműTípus}', ";
                szöveg += $"'{Adat.CiklusNév}') ";
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


        public void Módosítás(Adat_Ciklus_Sorrend Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET  ";
                szöveg += $" Sorszám={Adat.Sorszám} ";
                szöveg += $" WHERE [JárműTípus]='{Adat.JárműTípus}' AND [CiklusNév]='{Adat.CiklusNév}'";
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


        public void Törlés(Adat_Ciklus_Sorrend Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM {táblanév} WHERE [JárműTípus]='{Adat.JárműTípus}' AND [CiklusNév]='{Adat.CiklusNév}'";
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
