using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.V_Adatbázis;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.V_Kezelők
{
    public class Kezelő_AnyagTörzs
    {
        readonly string hely;
        readonly string jelszó = "SzőkeLászló";
        readonly string táblanév = "AnyagTábla";

        public Kezelő_AnyagTörzs()
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\rezsi\AnyagTörzs.mdb".KönyvSzerk();
            if (!File.Exists(hely)) Adatbázis_Létrehozás.AnyagTörzs(hely);
        }

        public List<Adat_Anyagok> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Anyagok> Adatok = new List<Adat_Anyagok>();
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
                                Adat_Anyagok Adat = new Adat_Anyagok(
                                        rekord["Cikkszám"].ToStrTrim(),
                                        rekord["Megnevezés"].ToStrTrim(),
                                        rekord["KeresőFogalom"].ToStrTrim(),
                                        rekord["Sarzs"].ToStrTrim(),
                                        rekord["Ár"].ToÉrt_Double()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Osztályoz(List<Adat_Anyagok> AdatokKap)
        {
            try
            {
                List<Adat_Anyagok> AdatokGyMód = new List<Adat_Anyagok>();
                List<Adat_Anyagok> AdatokGyRögzítés = new List<Adat_Anyagok>();
                List<Adat_Anyagok> Adatok = Lista_Adatok();
                // Kiválogatjuk a betöltendő adatokat
                foreach (Adat_Anyagok adat in AdatokKap)
                {
                    Adat_Anyagok VanAnyag = (from a in Adatok
                                             where a.Cikkszám.Trim() == adat.Cikkszám.Trim()
                                             && a.Sarzs.Trim() == adat.Sarzs.Trim()
                                             select a).FirstOrDefault();
                    //ha eddig nem volt ilyen anyag akkor felvesszük
                    if (VanAnyag == null)
                        AdatokGyRögzítés.Add(adat);
                    else
                    {
                        //Csak azokat vesszük fel a módosítandók közé, ahol van változás
                        if (!Egyezes(adat, VanAnyag)) AdatokGyMód.Add(adat);
                    }
                }
                if (AdatokGyMód.Count > 0) Módosítás(AdatokGyMód);
                if (AdatokGyRögzítés.Count > 0) Rögzítés(AdatokGyRögzítés);
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

        /// <summary>
        /// SAP adatok alapján módosítja a cikkszámokat és árakat
        /// </summary>
        /// <param name="Adatok"></param>
        private void Módosítás(List<Adat_Anyagok> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Anyagok Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $"Megnevezés='{Adat.Megnevezés}', ";
                    szöveg += $"Ár={Adat.Ár.ToString().Replace(",", ".")} ";
                    szöveg += $"WHERE Cikkszám='{Adat.Cikkszám}' ";
                    szöveg += $"AND Sarzs='{Adat.Sarzs}'";
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

        /// <summary>
        /// Módosítás csak a KeresőFogalom
        /// </summary>
        /// <param name="Adatok"></param>
        private void Módosítás(Adat_Anyagok Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"KeresőFogalom='{Adat.KeresőFogalom}' ";
                szöveg += $"WHERE Cikkszám='{Adat.Cikkszám}' ";
                szöveg += $"AND Sarzs='{Adat.Sarzs}'";
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

        private void Rögzítés(List<Adat_Anyagok> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Anyagok Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} (Cikkszám, Megnevezés, KeresőFogalom, Sarzs, Ár) VALUES (";
                    szöveg += $"'{Adat.Cikkszám}', ";
                    szöveg += $"'{Adat.Megnevezés}', ";
                    szöveg += $"'{Adat.KeresőFogalom}', ";
                    szöveg += $"'{Adat.Sarzs}', ";
                    szöveg += $"{Adat.Ár.ToString().Replace(",", ".")})";
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

        private void Rögzítés(Adat_Anyagok Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (Cikkszám, Megnevezés, KeresőFogalom, Sarzs, Ár) VALUES (";
                szöveg += $"'{Adat.Cikkszám}', ";
                szöveg += $"'{Adat.Megnevezés}', ";
                szöveg += $"'{Adat.KeresőFogalom}', ";
                szöveg += $"'{Adat.Sarzs}', ";
                szöveg += $"{Adat.Ár.ToString().Replace(",", ".")})";
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

        public bool TeljesEgyezes(Adat_Anyagok egyik, Adat_Anyagok masik)
        {
            return egyik.Cikkszám == masik.Cikkszám &&
                   egyik.Megnevezés == masik.Megnevezés &&
                   egyik.KeresőFogalom == masik.KeresőFogalom &&
                   egyik.Sarzs == masik.Sarzs &&
                   egyik.Ár == masik.Ár;
        }

        public bool Egyezes(Adat_Anyagok egyik, Adat_Anyagok masik)
        {
            return egyik.Cikkszám.Trim() == masik.Cikkszám.Trim() &&
                   egyik.Megnevezés.Trim() == masik.Megnevezés.Trim() &&
                   egyik.Sarzs.Trim() == masik.Sarzs.Trim() &&
                   egyik.Ár == masik.Ár;
        }

        public void Döntés(Adat_Anyagok Adat)
        {
            try
            {
                List<Adat_Anyagok> Adatok = Lista_Adatok();
                Adat_Anyagok VanAnyag = (from a in Adatok
                                         where a.Cikkszám == Adat.Cikkszám
                                         && a.Sarzs == Adat.Sarzs
                                         select a).FirstOrDefault();
                if (VanAnyag == null)
                    Rögzítés(Adat);
                else
                {
                    if (!Egyezes(Adat, VanAnyag)) Módosítás(Adat);
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

        }
    }
}
