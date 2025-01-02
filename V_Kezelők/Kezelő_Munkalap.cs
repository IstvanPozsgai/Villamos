using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Munka_Folyamat
    {
        public List<Adat_Munka_Folyamat> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Munka_Folyamat> Adatok = new List<Adat_Munka_Folyamat>();
            Adat_Munka_Folyamat Adat;

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
                                Adat = new Adat_Munka_Folyamat(
                                          rekord["ID"].ToÉrt_Long(),
                                          rekord["rendelésiszám"].ToStrTrim(),
                                          rekord["azonosító"].ToStrTrim(),
                                          rekord["munkafolyamat"].ToStrTrim(),
                                          rekord["látszódik"].ToÉrt_Bool()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }



        public void AdatbázisLétrehozás(string Cmbtelephely, DateTime Dátum)
        {
            try
            {
                // ha nincs olyan évi adatbázis, akkor létrehozzuk az előző évi alapján ha van.
                string hely = $@"{Application.StartupPath}\{Cmbtelephely}\Adatok\Munkalap\munkalap{Dátum.Year}.mdb";
                if (!File.Exists(hely))
                {
                    Adatbázis_Létrehozás.Munkalap_tábla(hely);
                    //HA Van előző évi akkor az adatokat átmásoljuk
                    hely = $@"{Application.StartupPath}\{Cmbtelephely}\Adatok\Munkalap\munkalap{Dátum.AddYears(-1).Year}.mdb";
                    if (File.Exists(hely))
                    {
                        Folyamat_Átír(Cmbtelephely, Dátum);
                        Munkarend_Átír(Cmbtelephely, Dátum);
                        Szolgálat_Átír(Cmbtelephely, Dátum);
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
        }

        private void Munkarend_Átír(string Cmbtelephely, DateTime Dátum)
        {
            string hely = Application.StartupPath + $@"\{Cmbtelephely.Trim()}\Adatok\Munkalap\munkalap{Dátum.AddYears(-1).Year}.mdb";
            string szöveg = "SELECT * FROM munkarendtábla WHERE Látszódik=true  ORDER BY id";
            string jelszó = "kismalac";

            Kezelő_MunkaRend kéz = new Kezelő_MunkaRend();
            List<Adat_MunkaRend> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

            hely = Application.StartupPath + $@"\{Cmbtelephely.Trim()}\Adatok\Munkalap\munkalap{Dátum.Year}.mdb";
            int id = 0;

            List<string> SzövegGy = new List<string>();
            foreach (Adat_MunkaRend rekord in Adatok)
            {
                // új adat rögzítése
                id++;
                szöveg = "INSERT INTO munkarendtábla (id, munkarend, látszódik)  VALUES (";
                szöveg += id + ", ";
                szöveg += "'" + rekord.Munkarend.Trim() + "', ";
                szöveg += " true ) ";
                SzövegGy.Add(szöveg);
            }
            MyA.ABMódosítás(hely, jelszó, SzövegGy);
        }

        private void Folyamat_Átír(string Cmbtelephely, DateTime Dátum)
        {
            try
            {
                string hely = Application.StartupPath + $@"\{Cmbtelephely.Trim()}\Adatok\Munkalap\munkalap{Dátum.AddYears(-1).Year}.mdb";
                string szöveg = "SELECT * FROM folyamattábla WHERE Látszódik=true  ORDER BY id";
                string jelszó = "kismalac";

                Kezelő_Munka_Folyamat kéz = new Kezelő_Munka_Folyamat();
                List<Adat_Munka_Folyamat> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                int id = 0;

                hely = Application.StartupPath + $@"\{Cmbtelephely.Trim()}\Adatok\Munkalap\munkalap{Dátum.Year}.mdb";

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Munka_Folyamat rekord in Adatok)
                {
                    // új adat rögzítése
                    id++;
                    szöveg = "INSERT INTO folyamattábla (id, Rendelésiszám, azonosító, munkafolyamat, látszódik)  VALUES (";
                    szöveg += id + ", ";
                    szöveg += "'" + rekord.Rendelésiszám.Trim() + "', ";
                    szöveg += "'" + rekord.Azonosító.Trim() + "', ";
                    szöveg += "'" + rekord.Munkafolyamat.Trim() + "', ";
                    szöveg += " true ) ";
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

        private void Szolgálat_Átír(string Cmbtelephely, DateTime Dátum)
        {
            try
            {
                string hely = Application.StartupPath + $@"\{Cmbtelephely.Trim()}\Adatok\Munkalap\munkalap{Dátum.AddYears(-1).Year}.mdb";
                string szöveg = "SELECT * FROM szolgálattábla";
                string jelszó = "kismalac";

                Kezelő_Munka_Szolgálat Kéz = new Kezelő_Munka_Szolgálat();
                Adat_Munka_Szolgálat Adat = Kéz.Egy_Adat(hely, jelszó, szöveg);

                hely = Application.StartupPath + $@"\{Cmbtelephely.Trim()}\Adatok\Munkalap\munkalap{Dátum.Year}.mdb";
                if (Adat != null)
                {
                    szöveg = "INSERT INTO szolgálattábla (költséghely, szolgálat, üzem, A1, A2, A3, A4, A5, A6, A7)  VALUES (";
                    szöveg += "'" + Adat.Költséghely.Trim() + "', ";
                    szöveg += "'" + Adat.Szolgálat.Trim() + "', ";
                    szöveg += "'" + Adat.Üzem.Trim() + "', ";
                    szöveg += " '0', '0', '0', '0', '0', '0', '0' )";

                    MyA.ABMódosítás(hely, jelszó, szöveg);
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




    public class Kezelő_MunkaRend
    {
        public List<Adat_MunkaRend> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_MunkaRend> Adatok = new List<Adat_MunkaRend>();
            Adat_MunkaRend Adat;

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
                                Adat = new Adat_MunkaRend(
                                          rekord["ID"].ToÉrt_Long(),
                                          rekord["munkarend"].ToStrTrim(),
                                          rekord["látszódik"].ToÉrt_Bool()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

    public class Kezelő_Munka_Szolgálat
    {
        public List<Adat_Munka_Szolgálat> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Munka_Szolgálat> Adatok = new List<Adat_Munka_Szolgálat>();
            Adat_Munka_Szolgálat Adat;

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
                                Adat = new Adat_Munka_Szolgálat(
                                          rekord["költséghely"].ToStrTrim(),
                                          rekord["szolgálat"].ToStrTrim(),
                                          rekord["üzem"].ToStrTrim(),
                                          rekord["A1"].ToStrTrim(),
                                          rekord["A2"].ToStrTrim(),
                                          rekord["A3"].ToStrTrim(),
                                          rekord["A4"].ToStrTrim(),
                                          rekord["A5"].ToStrTrim(),
                                          rekord["A6"].ToStrTrim(),
                                          rekord["A7"].ToStrTrim()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Munka_Szolgálat Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Munka_Szolgálat Adat = null;

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
                                Adat = new Adat_Munka_Szolgálat(
                                          rekord["költséghely"].ToStrTrim(),
                                          rekord["szolgálat"].ToStrTrim(),
                                          rekord["üzem"].ToStrTrim(),
                                          rekord["A1"].ToStrTrim(),
                                          rekord["A2"].ToStrTrim(),
                                          rekord["A3"].ToStrTrim(),
                                          rekord["A4"].ToStrTrim(),
                                          rekord["A5"].ToStrTrim(),
                                          rekord["A6"].ToStrTrim(),
                                          rekord["A7"].ToStrTrim()
                                          );
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }
    public class Kezelő_Munkalapösszesítő
    {
        public List<Adat_Munkalapösszesítő> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Munkalapösszesítő> Adatok = new List<Adat_Munkalapösszesítő>();
            Adat_Munkalapösszesítő Adat;

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
                                Adat = new Adat_Munkalapösszesítő(
                                          rekord["ID"].ToÉrt_Long(),
                                          rekord["megnevezés"].ToString(),
                                          rekord["művelet"].ToString(),
                                          rekord["pályaszám"].ToString(),
                                          rekord["rendelés"].ToString()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

    public class Kezelő_Munka_Idő
    {
        public List<Adat_Munka_Idő> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Munka_Idő> Adatok = new List<Adat_Munka_Idő>();
            Adat_Munka_Idő Adat;

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
                                Adat = new Adat_Munka_Idő(
                                          rekord["ID"].ToÉrt_Long(),
                                          rekord["Idő"].ToÉrt_Long()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

    public class Kezelő_Munkalapelszámoló
    {
        public List<Adat_Munkalapelszámoló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Munkalapelszámoló> Adatok = new List<Adat_Munkalapelszámoló>();
            Adat_Munkalapelszámoló Adat;

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
                                Adat = new Adat_Munkalapelszámoló(
                                          rekord["ID"].ToÉrt_Long(),
                                          rekord["idő"].ToÉrt_Long(),
                                          rekord["dátum"].ToÉrt_DaTeTime(),
                                          rekord["megnevezés"].ToString(),
                                          rekord["művelet"].ToString(),
                                          rekord["pályaszám"].ToString(),
                                          rekord["rendelés"].ToString(),
                                          rekord["státus"].ToÉrt_Bool()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }


    public class Kezelő_Munka_Rendelés
    {
        public List<Adat_Munka_Rendelés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Munka_Rendelés> Adatok = new List<Adat_Munka_Rendelés>();
            Adat_Munka_Rendelés Adat;

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
                                Adat = new Adat_Munka_Rendelés(
                                          rekord["ID"].ToÉrt_Long(),
                                          rekord["megnevezés"].ToString(),
                                          rekord["művelet"].ToString(),
                                          rekord["pályaszám"].ToString(),
                                          rekord["rendelés"].ToString()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }
    public class Kezelő_Munka_Adatok
    {
        public List<Adat_Munka_Adatok> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Munka_Adatok> Adatok = new List<Adat_Munka_Adatok>();
            Adat_Munka_Adatok Adat;

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
                                Adat = new Adat_Munka_Adatok(
                                          rekord["ID"].ToÉrt_Long(),
                                          rekord["idő"].ToÉrt_Int(),
                                          rekord["dátum"].ToÉrt_DaTeTime(),
                                          rekord["megnevezés"].ToString(),
                                          rekord["művelet"].ToString(),
                                          rekord["pályaszám"].ToString(),
                                          rekord["rendelés"].ToString(),
                                          rekord["státus"].ToÉrt_Bool()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Munka_Adatok> Lista_Adatok_Szűk(string hely, string jelszó, string szöveg)
        {
            List<Adat_Munka_Adatok> Adatok = new List<Adat_Munka_Adatok>();
            Adat_Munka_Adatok Adat;

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
                                Adat = new Adat_Munka_Adatok(
                                          rekord["megnevezés"].ToString(),
                                          rekord["művelet"].ToString(),
                                          rekord["pályaszám"].ToString(),
                                          rekord["rendelés"].ToString()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Munka_Adatok> Lista_Adat_SUM_List(string hely, string jelszó, string szöveg)
        {
            List<Adat_Munka_Adatok> Adatok = new List<Adat_Munka_Adatok>();
            Adat_Munka_Adatok Adat;

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
                                Adat = new Adat_Munka_Adatok(
                                          rekord["SUMIdő"].ToÉrt_Int(),
                                          rekord["dátum"].ToÉrt_DaTeTime(),
                                          rekord["megnevezés"].ToString(),
                                          rekord["művelet"].ToString(),
                                          rekord["pályaszám"].ToString(),
                                          rekord["rendelés"].ToString(),
                                          rekord["státus"].ToÉrt_Bool()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Munka_Adatok> Lista_AdatokSUM(string hely, string jelszó, string szöveg)
        {
            List<Adat_Munka_Adatok> Adatok = new List<Adat_Munka_Adatok>();
            Adat_Munka_Adatok Adat;

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
                                Adat = new Adat_Munka_Adatok(
                                          rekord["SUMidő"].ToÉrt_Int(),
                                          rekord["művelet"].ToString(),
                                          rekord["rendelés"].ToString()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }
}
