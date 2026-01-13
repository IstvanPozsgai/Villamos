using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Osztály_Adat
    {
        Kezelő_Osztály_Név KézNév = new Kezelő_Osztály_Név();
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\osztály.mdb";
        readonly string jelszó = "kéménybe";
        readonly string táblanév = "osztályadatok";

        public Kezelő_Osztály_Adat()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Osztálytábla(hely.KönyvSzerk());
            // a mezők átalakuljanak MEMO-ra.
            TeljesTablacsereMemoTipusra();
        }

        private void TeljesTablacsereMemoTipusra()
        {
            try
            {
                //Megnézzük, milyen oszlopaink vannak most
                List<string> jelenlegiOszlopok = GetFizikaiOszlopok();
                if (jelenlegiOszlopok.Count == 0) return;

                // Ha már van temp tábla szemétként, töröljük
                if (MyA.ABvanTábla(hely, jelszó, "SELECT * FROM osztályadatok_new"))
                    MyA.ABtörlés(hely, jelszó, "DROP TABLE osztályadatok_new");

                // SQL parancs generálása az ÚJ tábla létrehozásához
                // expliciten MEMO-t hozunk létre
                string createSql = "CREATE TABLE osztályadatok_new ( [azonosító] char(10) PRIMARY KEY";

                foreach (string oszlop in jelenlegiOszlopok)
                {
                    if (oszlop.ToLower() == "azonosító") continue;

                    // Ha "Adat"-tal kezdődik, akkor KÖTELEZŐEN MEMO legyen
                    if (oszlop.StartsWith("Adat", StringComparison.InvariantCultureIgnoreCase))
                        createSql += $", [{oszlop}] MEMO";
                    else
                        // Minden más marad szöveg
                        createSql += $", [{oszlop}] TEXT(255)";
                }
                createSql += " )";

                // Létrehozzuk az üres, de TÖKÉLETES szerkezetű új táblát
                MyA.ABMódosítás(hely, jelszó, createSql);

                // ADATOK ÁTMÁSOLÁSA
                // Az Access automatikusan konvertálja a Rövid szöveget MEMO-ra másoláskor
                // Összeállítjuk a mezőlistát a másoláshoz
                string mezok = "[azonosító]";
                foreach (string oszlop in jelenlegiOszlopok)
                    if (oszlop.ToLower() != "azonosító") mezok += $", [{oszlop}]";

                string copySql = $"INSERT INTO osztályadatok_new ({mezok}) SELECT {mezok} FROM {táblanév}";
                MyA.ABMódosítás(hely, jelszó, copySql);

                //CSERE
                MyA.ABtörlés(hely, jelszó, $"DROP TABLE {táblanév}");

                string createOriginalSql = createSql.Replace("osztályadatok_new", táblanév);
                MyA.ABMódosítás(hely, jelszó, createOriginalSql);

                string copyBackSql = $"INSERT INTO {táblanév} SELECT * FROM osztályadatok_new";
                MyA.ABMódosítás(hely, jelszó, copyBackSql);

                // Temp törlése
                MyA.ABtörlés(hely, jelszó, "DROP TABLE osztályadatok_new");

                // TÖMÖRÍTÉS (Hogy a méret lecsökkenjen)
                MyA.AdatbazisTomorites(hely, jelszó);

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

        private List<string> GetFizikaiOszlopok()
        {
            List<string> oszlopok = new List<string>();
            string connStr = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='{hely}';Jet OLEDB:Database Password={jelszó}";
            try { using (OleDbConnection c = new OleDbConnection(connStr)) { c.Open(); } }
            catch { connStr = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}';Jet Oledb:Database Password={jelszó}"; }

            try
            {
                using (OleDbConnection Kapcsolat = new OleDbConnection(connStr))
                {
                    Kapcsolat.Open();
                    DataTable schemaTable = Kapcsolat.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, táblanév, null });

                    //Az ORDINAL_POSITION szerint rendezzük, hogy az adatok ne keveredjenek
                    DataRow[] rows = schemaTable.Select("", "ORDINAL_POSITION ASC");

                    foreach (DataRow row in rows)
                        oszlopok.Add(row["COLUMN_NAME"].ToString());
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
            return oszlopok;
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