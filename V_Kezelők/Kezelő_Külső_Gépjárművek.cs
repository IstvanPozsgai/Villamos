using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Külső_Gépjárművek
    {

        readonly string táblanév = "gépjárművek";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Behajtási\Külső_adatok.mdb";
        readonly string jelszó = "Janda";

        public Kezelő_Külső_Gépjárművek()
        {
            FájlBeállítás();
        }

        private void FájlBeállítás()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Külsős_Táblák(hely);
        }

        public List<Adat_Külső_Gépjárművek> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Külső_Gépjárművek> Adatok = new List<Adat_Külső_Gépjárművek>();
            Adat_Külső_Gépjárművek Adat;

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
                                Adat = new Adat_Külső_Gépjárművek(
                                        rekord["Id"].ToÉrt_Double(),
                                        rekord["Frsz"].ToStrTrim(),
                                        rekord["Cégid"].ToÉrt_Double(),
                                        rekord["Státus"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Rögzítés(List<Adat_Külső_Gépjárművek> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Külső_Gépjárművek Adat in Adatok)
                {
                    List<Adat_Külső_Gépjárművek> ListaAdatok = Lista_Adatok();
                    double id = ListaAdatok.Any() ? ListaAdatok.Max(a => a.Id) + 1 : 1;

                    string szöveg = "INSERT INTO Gépjárművek (id, frsz, cégid, státus) VALUES (";
                    szöveg += $"{id}, "; // id
                    szöveg += $"'{Adat.Frsz}', "; // frsz
                    szöveg += $"'{Adat.Cégid}', "; // cégid
                    szöveg += $" {Adat.Státus}) ";
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

        public void Rögzítés(Adat_Külső_Gépjárművek Adat)
        {
            try
            {
                string szöveg = "INSERT INTO Gépjárművek (id, frsz, cégid, státus) VALUES (";
                szöveg += $"{Adat.Id}, "; // id
                szöveg += $"'{Adat.Frsz}', "; // frsz
                szöveg += $"'{Adat.Cégid}', "; // cégid
                szöveg += $" {Adat.Státus}) ";
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

        public void Módosítás(List<Adat_Külső_Gépjárművek> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Külső_Gépjárművek Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $" státus={Adat.Státus} ";
                    szöveg += $" WHERE Cégid={Adat.Cégid} AND frsz='{Adat.Frsz}'";
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

        public void Módosítás(Adat_Külső_Gépjárművek Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $" státus={Adat.Státus} ";
                szöveg += $" WHERE Cégid={Adat.Cégid} AND frsz='{Adat.Frsz}'";
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

        public void Törlés(List<Adat_Külső_Gépjárművek> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Külső_Gépjárművek Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $" státus={Adat.Státus} ";
                    szöveg += $" WHERE Cégid={Adat.Id}";
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

        public void Döntés(List<Adat_Külső_Gépjárművek> Adatok)
        {
            try
            {
                List<Adat_Külső_Gépjárművek> ListaAdatok = Lista_Adatok();
                List<Adat_Külső_Gépjárművek> AdatokR = new List<Adat_Külső_Gépjárművek>();
                List<Adat_Külső_Gépjárművek> AdatokM = new List<Adat_Külső_Gépjárművek>();
                foreach (Adat_Külső_Gépjárművek ADAT in Adatok)
                {
                    bool vane = Adatok.Any(a => a.Cégid == ADAT.Cégid && a.Frsz.Trim() == ADAT.Frsz.Trim());
                    if (vane)
                        AdatokM.Add(ADAT);
                    else
                        AdatokR.Add(ADAT);

                    if (AdatokM.Count > 0) Módosítás(AdatokM);
                    if (AdatokR.Count > 0) Rögzítés(AdatokR);
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
