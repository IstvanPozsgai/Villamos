using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos.Kezelők
{
    public class Kezelő_Kulcs
    {
        readonly string hely = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Villamos\Kulcs.mdb".Ellenőrzés();
        readonly string jelszó = "Tóth_Katalin";

        public List<Adat_Kulcs> Lista_Adatok()
        {
            string szöveg = "Select * From Adat";
            List<Adat_Kulcs> Adatok = new List<Adat_Kulcs>();
            Adat_Kulcs Adat;

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
                                Adat = new Adat_Kulcs(
                                    rekord["adat1"].ToStrTrim(),
                                    rekord["adat2"].ToStrTrim(),
                                    rekord["adat3"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public bool ABKULCSvan(string adat1, string adat2, string adat3)
        {
            bool válasz = false;
            List<Adat_Kulcs> AdatokKulcs = Lista_Adatok();
            foreach (Adat_Kulcs rekord in AdatokKulcs)
            {
                string adat1Kód = MyF.MÁSDekódolja(rekord.Adat1.ToString());
                string adat2Kód = MyF.MÁSDekódolja(rekord.Adat2.ToString());
                string adat3Kód = MyF.MÁSDekódolja(rekord.Adat3.ToString());
                if ((adat1.Trim() == adat1Kód.Trim()) && (adat2.Trim() == adat2Kód.Trim()) && (adat3.Trim() == adat3Kód.Trim()))
                {
                    válasz = true;
                    break;
                }
            }
            return válasz;
        }

        public void Rögzít(Adat_Kulcs Adat)
        {
            try
            {

                string szöveg = "INSERT INTO adat ";
                szöveg += "(adat1, adat2, adat3 ) VALUES ";
                szöveg += $"('{Adat.Adat1}', ";
                szöveg += $" '{Adat.Adat2}', ";
                szöveg += $" '{Adat.Adat3}' )";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Kulcs rögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }


    public class Kezelő_Kulcs_Kettő
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Villamos10.mdb";
        readonly string jelszó = "fütyülősbarack";
        public List<Adat_Kulcs> Lista_Adatok()
        {
            List<Adat_Kulcs> Adatok = new List<Adat_Kulcs>();
            Adat_Kulcs Adat;

            string szöveg = $"SELECT * FROM Adattábla";

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
                                Adat = new Adat_Kulcs(
                                    rekord["adat1"].ToStrTrim(),
                                    rekord["adat2"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzít(List<Adat_Kulcs> Adatok)
        {
            try
            {
                if (Adatok == null) return;
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kulcs Adat in Adatok)
                {
                    string szöveg = "INSERT INTO adattábla ";
                    szöveg += "(adat1, adat2) VALUES ";
                    szöveg += $"('{Adat.Adat1}', ";
                    szöveg += $" '{Adat.Adat2}')";
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Kulcs rögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Rögzít(Adat_Kulcs Adat)
        {
            try
            {
                string szöveg = "INSERT INTO adattábla ";
                szöveg += "(adat1, adat2) VALUES ";
                szöveg += $"('{Adat.Adat1}', ";
                szöveg += $" '{Adat.Adat2}')";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Kulcs rögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosít(List<Adat_Kulcs> Adatok)
        {
            try
            {
                if (Adatok == null) return;
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kulcs Adat in Adatok)
                {
                    string szöveg = "UPDATE Adattábla SET ";
                    szöveg += $" Adat2='{Adat.Adat2}'";
                    szöveg += $" WHERE Adat1 like '%{Adat.Adat1}%'";

                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Kulcs rögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Adat1
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosít(Adat_Kulcs Adat)
        {
            try
            {
                string szöveg = "UPDATE Adattábla SET ";
                szöveg += $" Adat2='{Adat.Adat2}'";
                szöveg += $" WHERE Adat1 like '%{Adat.Adat1}%'";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Kulcs rögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    public class Kezelő_Kulcs_Három
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Villamos9.mdb";
        readonly string jelszó = "Tóth_Katalin";


        public List<Adat_Kulcs> Lista_Adatok()
        {

            List<Adat_Kulcs> Adatok = new List<Adat_Kulcs>();
            Adat_Kulcs Adat;
            string szöveg = $"SELECT * FROM Adattábla";
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
                                Adat = new Adat_Kulcs(
                                    rekord["adat1"].ToStrTrim(),
                                    rekord["adat2"].ToStrTrim(),
                                    rekord["adat3"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }



        public bool ABKULCSvan(List<Adat_Kulcs> AdatokKulcs, string adat1, string adat2, string adat3)
        {
            bool válasz = false;
            foreach (Adat_Kulcs rekord in AdatokKulcs)
            {
                string adat1Kód = MyF.MÁSDekódolja(rekord.Adat1.ToString());
                string adat2Kód = MyF.MÁSDekódolja(rekord.Adat2.ToString());
                string adat3Kód = MyF.MÁSDekódolja(rekord.Adat3.ToString());
                if ((adat1.Trim() == adat1Kód.Trim()) && (adat2.Trim() == adat2Kód.Trim()) && (adat3.Trim() == adat3Kód.Trim()))
                {
                    válasz = true;
                    break;
                }
            }
            return válasz;
        }

        public void Rögzít(string hely, Adat_Kulcs Adat)
        {
            try
            {

                string szöveg = "INSERT INTO adat ";
                szöveg += "(adat1, adat2, adat3 ) VALUES ";
                szöveg += $"('{Adat.Adat1}', ";
                szöveg += $" '{Adat.Adat2}', ";
                szöveg += $" '{Adat.Adat3}' )";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Kulcs rögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
