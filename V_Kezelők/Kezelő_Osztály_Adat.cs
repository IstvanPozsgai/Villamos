using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Osztály_Adat
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\osztály.mdb";
        readonly string jelszó = "kéménybe";

        public Kezelő_Osztály_Adat()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Osztálytábla(hely.KönyvSzerk());
        }

        public List<Adat_Osztály_Adat> Lista_Adat()
        {
            List<Adat_Osztály_Adat> Adatok = new List<Adat_Osztály_Adat>();
            try
            {
                string szöveg = "select * from osztályadatok ORDER BY azonosító";
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
                                    List<string> AdatokGy = new List<string>();
                                    List<string> Mezőnevek = new List<string>();
                                    for (int i = 0; i < rekord.FieldCount; i++)
                                    {
                                        string Mezőnév = rekord.GetName(i).ToStrTrim();
                                        string Érték = "?";
                                        if (rekord.GetValue(i).GetType() != null) Érték = rekord.GetValue(i).ToStrTrim();
                                        if (Mezőnév.Contains("Adat"))
                                        {
                                            AdatokGy.Add(Érték);
                                            Mezőnevek.Add(Mezőnév);
                                        }
                                    }

                                    Adat_Osztály_Adat Adat = new Adat_Osztály_Adat(
                                        rekord["Azonosító"].ToStrTrim(),
                                        AdatokGy,
                                        Mezőnevek
                                        );
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
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
            return Adatok;
        }

        public string Érték(Adat_Osztály_Adat rekord, string Mezőnév)
        {
            string Válasz = "?";
            try
            {
                for (int i = 0; i < rekord.Mezőnév.Count; i++)
                {
                    if (rekord.Mezőnév[i].ToStrTrim() == Mezőnév)
                        Válasz = rekord.Adatok[i].ToStrTrim();
                }
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
            return Válasz;
        }


        public void Módosítás(List<Adat_Osztály_Adat> Adatok)
        {
            try
            {
                //
                //for (int ki = 1; ki < AdatokNév.Count; ki++)
                //{
                //    if (Ideig[ki] != null)
                //    {
                //        if (Ideig[ki].Trim() != "")
                //            szöveg += $"adat{ki}='{Ideig[ki].Trim()}', ";
                //    }
                //}
                //szöveg = szöveg.Substring(0, szöveg.Length - 2); //az utolsó vesszőt eldobjuk
                //szöveg += $" WHERE azonosító='{pályaszám.Trim()}'";
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Osztály_Adat rekord in Adatok)
                {
                    string szöveg = "UPDATE osztályadatok SET ";
                    foreach (string név in rekord.Mezőnév)
                        szöveg += $", {név}";

                    szöveg += ") VALUES (";
                    szöveg += $"'{rekord.Azonosító}'";

                    foreach (string érték in rekord.Adatok)
                        szöveg += $", '{érték}'";
                    szöveg += ")";
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


        public void Rögzítés(List<Adat_Osztály_Adat> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Osztály_Adat rekord in Adatok)
                {
                    string szöveg = "INSERT INTO osztályadatok ( azonosító";
                    foreach (string név in rekord.Mezőnév)
                        szöveg += $", {név}";

                    szöveg += ") VALUES (";
                    szöveg += $"'{rekord.Azonosító}'";

                    foreach (string érték in rekord.Adatok)
                        szöveg += $", '{érték}'";
                    szöveg += ")";
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
