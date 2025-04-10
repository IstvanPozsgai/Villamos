using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Nóta
    {
        readonly string jelszó = "TörökKasos";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Nóta\NótaT5C5.mdb";

        readonly Kezelő_Kiegészítő_Szolgálattelepei KézSzolgálattelepei = new Kezelő_Kiegészítő_Szolgálattelepei();
        readonly Kezelő_Kerék_Tábla KézBerendezés = new Kezelő_Kerék_Tábla();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();

        public Kezelő_Nóta()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.FődarabNóta(hely.KönyvSzerk());
        }

        public List<Adat_Nóta> Lista_Adat(bool Aktív = true)
        {
            string szöveg;
            if (Aktív == true)
                szöveg = $"SELECT * FROM Nóta_Adatok WHERE Státus<>9 ORDER BY ID";
            else
                szöveg = $"SELECT * FROM Nóta_Adatok ORDER BY ID";

            List<Adat_Nóta> Adatok = new List<Adat_Nóta>();
            Adat_Nóta Adat;

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
                                Adat = new Adat_Nóta(
                                     rekord["Id"].ToÉrt_Long(),
                                     rekord["Berendezés"].ToStrTrim(),
                                     rekord["Készlet_Sarzs"].ToStrTrim(),
                                     rekord["Raktár"].ToStrTrim(),
                                     rekord["Telephely"].ToStrTrim(),
                                     rekord["Forgóváz"].ToStrTrim(),
                                     rekord["Beépíthető"].ToÉrt_Bool(),
                                     rekord["MűszakiM"].ToStrTrim(),
                                     rekord["OsztásiM"].ToStrTrim(),
                                     rekord["Dátum"].ToÉrt_DaTeTime(),
                                     rekord["Státus"].ToÉrt_Int(),
                                     rekord["Cikkszám"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Módosítás(Adat_Nóta Adat)
        {
            try
            {
                string szöveg;
                if (Program.Postás_Vezér || Program.PostásTelephely == "Főmérnökség")
                {
                    szöveg = "UPDATE Nóta_Adatok SET ";
                    szöveg += $"Berendezés='{Adat.Berendezés}', ";
                    szöveg += $"Készlet_Sarzs='{Adat.Készlet_Sarzs}', ";
                    szöveg += $"Raktár='{Adat.Raktár}', ";
                    szöveg += $"Telephely='{Adat.Telephely}', ";
                    szöveg += $"Forgóváz='{Adat.Forgóváz}', ";
                    szöveg += $"Beépíthető={Adat.Beépíthető}, ";
                    szöveg += $"MűszakiM='{Adat.MűszakiM}', ";
                    szöveg += $"OsztásiM='{Adat.OsztásiM}', ";
                    szöveg += $"Dátum='{Adat.Dátum:yyyy.MM.dd}', ";
                    szöveg += $"Státus={Adat.Státus}, ";
                    szöveg += $"Cikkszám='{Adat.Cikkszám}' ";
                    szöveg += $" WHERE [Id] ={Adat.Id}";
                }
                else
                {
                    szöveg = "UPDATE Nóta_Adatok SET ";
                    szöveg += $"Telephely='{Adat.Telephely}', ";
                    szöveg += $"Forgóváz='{Adat.Forgóváz}', ";
                    szöveg += $"Beépíthető={Adat.Beépíthető}, ";
                    szöveg += $"MűszakiM='{Adat.MűszakiM}' ";
                    szöveg += $" WHERE [Id] ={Adat.Id}";
                }
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

        public void Rögzítés(Adat_Nóta Adat)
        {
            try
            {
                string szöveg = "INSERT  INTO Nóta_Adatok ";
                szöveg += "(Berendezés, Készlet_Sarzs, Raktár, Telephely, Forgóváz, Beépíthető, MűszakiM, OsztásiM, Dátum, Státus, Id, Cikkszám) VALUES (";
                szöveg += $"'{Adat.Berendezés}', ";      // Berendezés
                szöveg += $"'{Adat.Készlet_Sarzs}', ";   // Készlet_Sarzs
                szöveg += $"'{Adat.Raktár}', ";          // Raktár
                szöveg += $"'{Adat.Telephely}', ";           // Telephely
                szöveg += $"'{Adat.Forgóváz}', ";             // Forgóváz
                szöveg += $"{Adat.Beépíthető}, ";           // Beépíthető
                szöveg += $"'{Adat.MűszakiM}', ";             // MűszakiM
                szöveg += $"'{Adat.OsztásiM}', ";             // OsztásiM
                szöveg += $"'{DateTime.Today:yyyy.MM.dd}', ";        // Dátum
                szöveg += $"{Adat.Státus}, ";                    // Státus
                szöveg += $"{Sorszám()}, ";
                szöveg += $"'{Adat.Cikkszám}') ";
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

        public void Módosítás(List<Adat_Nóta> Adatok)
        {
            try
            {
                List<Adat_Kiegészítő_Szolgálattelepei> AdatokRaktár = KézSzolgálattelepei.Lista_Adatok();
                List<Adat_Kerék_Tábla> AdatokKocsi = KézBerendezés.Lista_Adatok();
                List<Adat_Jármű> AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");

                List<string> SzövegGY = new List<string>();
                foreach (Adat_Nóta Adat in Adatok)
                {
                    string Telephely = "";

                    if (Adat.Raktár != "")
                    {
                        Adat_Kiegészítő_Szolgálattelepei AdatRaktár = AdatokRaktár.FirstOrDefault(x => x.Raktár == Adat.Raktár);
                        if (AdatRaktár != null) Telephely = AdatRaktár.Telephelynév;
                    }

                    if (Telephely == "")
                    {
                        Adat_Kerék_Tábla AdatBerendezés = AdatokKocsi.FirstOrDefault(a => a.Kerékberendezés == Adat.Berendezés);
                        if (AdatBerendezés != null)
                        {
                            Adat_Jármű EgyKocsi = AdatokJármű.FirstOrDefault(a => a.Azonosító == AdatBerendezés.Azonosító);
                            if (EgyKocsi != null) Telephely = EgyKocsi.Üzem;
                        }
                    }

                    if (Telephely == "")
                    {
                        if (Adat.Készlet_Sarzs.Trim() == "02")
                        {
                            string szöveg = "UPDATE Nóta_Adatok SET ";
                            szöveg += $"Készlet_Sarzs='{Adat.Készlet_Sarzs}', ";
                            szöveg += $"Raktár='{Adat.Raktár}', ";
                            szöveg += $"Státus=7 ";
                            szöveg += $" WHERE [Id] ={Adat.Id}";
                            SzövegGY.Add(szöveg);
                        }
                        else
                        {
                            string szöveg = "UPDATE Nóta_Adatok SET ";
                            szöveg += $"Készlet_Sarzs='{Adat.Készlet_Sarzs}', ";
                            szöveg += $"Raktár='{Adat.Raktár}' ";
                            szöveg += $" WHERE [Id] ={Adat.Id}";
                            SzövegGY.Add(szöveg);
                        }
                    }
                    else
                    {
                        if (Adat.Készlet_Sarzs.Trim() == "02")
                        {
                            string szöveg = "UPDATE Nóta_Adatok SET ";
                            szöveg += $"Készlet_Sarzs='{Adat.Készlet_Sarzs}', ";
                            szöveg += $"Raktár='{Adat.Raktár}', ";
                            szöveg += $"Telephely='{Telephely}', ";
                            szöveg += $"Státus=7 ";
                            szöveg += $" WHERE [Id] ={Adat.Id}";
                            SzövegGY.Add(szöveg);
                        }
                        else
                        {
                            string szöveg = "UPDATE Nóta_Adatok SET ";
                            szöveg += $"Készlet_Sarzs='{Adat.Készlet_Sarzs}', ";
                            szöveg += $"Raktár='{Adat.Raktár}', ";
                            szöveg += $"Telephely='{Telephely}' ";
                            szöveg += $" WHERE [Id] ={Adat.Id}";
                            SzövegGY.Add(szöveg);
                        }
                    }
                }

                MyA.ABMódosítás(hely, jelszó, SzövegGY);
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

        public void Rögzítés(List<Adat_Nóta> Adatok)
        {
            try
            {
                List<Adat_Kiegészítő_Szolgálattelepei> AdatokRaktár = KézSzolgálattelepei.Lista_Adatok();

                List<string> SzövegGY = new List<string>();
                long id = Sorszám();
                foreach (Adat_Nóta Adat in Adatok)
                {
                    string Telephely = "";
                    if (Adat.Raktár != "")
                    {
                        Adat_Kiegészítő_Szolgálattelepei AdatRaktár = AdatokRaktár.FirstOrDefault(x => x.Raktár == Adat.Raktár);
                        if (AdatRaktár != null) Telephely = AdatRaktár.Telephelynév;
                    }

                    string szöveg = "INSERT  INTO Nóta_Adatok ";
                    szöveg += "(Berendezés, Készlet_Sarzs, Raktár, Telephely, Forgóváz, Beépíthető, MűszakiM, OsztásiM, Dátum, Státus, Id, cikkszám) VALUES (";
                    szöveg += $"'{Adat.Berendezés}', ";      // Berendezés
                    szöveg += $"'{Adat.Készlet_Sarzs}', ";   // Készlet_Sarzs
                    szöveg += $"'{Adat.Raktár}', ";          // Raktár
                    szöveg += $"'{Telephely}', ";           // Telephely
                    szöveg += $"'', ";             // Forgóváz
                    szöveg += $"false, ";           // Beépíthető
                    szöveg += $"'', ";             // MűszakiM
                    szöveg += $"'', ";             // OsztásiM
                    szöveg += $"'{DateTime.Today:yyyy.MM.dd}', ";        // Dátum
                    szöveg += $"1, ";                    // Státus
                    szöveg += $"{id}, ";                  // Id
                    szöveg += $"'{Adat.Cikkszám}') ";
                    SzövegGY.Add(szöveg);
                    id++;

                }
                MyA.ABMódosítás(hely, jelszó, SzövegGY);
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

        private long Sorszám()
        {
            long Válasz = 1;
            try
            {
                List<Adat_Nóta> Adatok = Lista_Adat();
                if (Adatok != null && Adatok.Count > 0) Válasz = Adatok.Max(x => x.Id) + 1;
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

        public void Módosítás(List<long> IDk)
        {
            try
            {
                List<string> SzövegGY = new List<string>();
                foreach (long Id in IDk)
                {
                    string szöveg = "UPDATE Nóta_Adatok SET Státus=9 ";
                    szöveg += $" WHERE [Id] ={Id}";
                    SzövegGY.Add(szöveg);
                }

                MyA.ABMódosítás(hely, jelszó, SzövegGY);

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
