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
    public class Kezelő_Osztály_Adat
    {
        Kezelő_Osztály_Név KézNév = new Kezelő_Osztály_Név();
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\osztály.mdb";
        readonly string jelszó = "kéménybe";
        readonly string táblanév = "osztályadatok";

        public Kezelő_Osztály_Adat()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Osztálytábla(hely.KönyvSzerk());
        }

        public List<Adat_Osztály_Adat> Lista_Adat()
        {
            List<Adat_Osztály_Adat> Adatok = new List<Adat_Osztály_Adat>();
            try
            {
                string szöveg = $"select * from {táblanév} ORDER BY azonosító";
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

        public string Érték(Adat_Osztály_Adat rekord, string Leírás)
        {
            string Válasz = "?";
            try
            {
                string mezőnév = KézNév.Mezőnév(Leírás);
                for (int i = 0; i < rekord.Mezőnév.Count; i++)
                {
                    if (rekord.Mezőnév[i].ToStrTrim() == mezőnév)
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
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Osztály_Adat rekord in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    string szöveg1 = "   ";
                    for (int i = 0; i < rekord.Mezőnév.Count; i++)
                    {
                        if (rekord.Mezőnév[i].ToStrTrim() != "")
                            szöveg1 += $"{rekord.Mezőnév[i].ToStrTrim()}='{rekord.Adatok[i].ToStrTrim()}', ";
                    }
                    szöveg1 = szöveg1.Substring(0, szöveg1.Length - 2); //az utolsó vesszőt eldobjuk
                    szöveg += $"{szöveg1} WHERE azonosító='{rekord.Azonosító}'";
                    if (szöveg1.Trim() != "") SzövegGy.Add(szöveg);
                }
                if (SzövegGy.Count > 0) MyA.ABMódosítás(hely, jelszó, SzövegGy);
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
                    string szöveg = $"INSERT INTO {táblanév} ( azonosító";
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
