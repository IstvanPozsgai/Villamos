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
    public class Kezelő_Külső_Cégek
    {
        readonly string táblanév = "Cégek";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Behajtási\Külső_adatok.mdb";
        readonly string jelszó = "Janda";

        public Kezelő_Külső_Cégek()
        {
            FájlBeállítás();
        }


        private void FájlBeállítás()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Külsős_Táblák(hely);
        }


        public List<Adat_Külső_Cégek> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév}";

            List<Adat_Külső_Cégek> Adatok = new List<Adat_Külső_Cégek>();
            Adat_Külső_Cégek Adat;

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
                                Adat = new Adat_Külső_Cégek(
                                        rekord["Cégid"].ToÉrt_Double(),
                                        rekord["Cég"].ToStrTrim(),
                                        rekord["Címe"].ToStrTrim(),
                                        rekord["Cég_email"].ToStrTrim(),
                                        rekord["Felelős_személy"].ToStrTrim(),
                                        rekord["Felelős_telefonszám"].ToStrTrim(),
                                        rekord["Munkaleírás"].ToStrTrim(),
                                        rekord["Mikor"].ToStrTrim(),
                                        rekord["Érv_kezdet"].ToÉrt_DaTeTime(),
                                        rekord["Érv_vég"].ToÉrt_DaTeTime(),
                                        rekord["Engedélyezés_dátuma"].ToÉrt_DaTeTime(),
                                        rekord["Engedélyező"].ToStrTrim(),
                                        rekord["Engedély"].ToÉrt_Int(),
                                        rekord["Státus"].ToÉrt_Bool(),
                                        rekord["Terület"].ToStrTrim()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Rögzítés(Adat_Külső_Cégek Adat)
        {
            try
            {

                string szöveg = "INSERT INTO Cégek (cégid, cég, címe, cég_email, felelős_személy, Felelős_telefonszám, munkaleírás,";
                szöveg += " mikor, érv_kezdet, érv_vég, Engedélyezés_dátuma, engedélyező, engedély, státus, terület)  VALUES (";
                szöveg += $"{Adat.Cégid}, "; // cégid
                szöveg += $"'{Adat.Cég}', "; // cég
                szöveg += $"'{Adat.Címe}', "; // címe
                szöveg += $"'{Adat.Cég_email}', "; // cég_email
                szöveg += $"'{Adat.Felelős_személy}', "; // felelős_személy
                szöveg += $"'{Adat.Felelős_telefonszám}', "; // Felelős_telefonszám
                szöveg += $"'{Adat.Munkaleírás}', "; // munkaleírás
                szöveg += $"'{Adat.Mikor}', "; // Mikor
                szöveg += $"'{Adat.Érv_kezdet:yyyy.MM.dd}', "; // érv_kezdet
                szöveg += $"'{Adat.Érv_vég:yyyy.MM.dd}', "; // érv_vég
                szöveg += $"'{Adat.Engedélyezés_dátuma:yyyy.MM.dd}', ";  // endegélyezés_dátuma
                szöveg += $"'{Adat.Engedélyező}', "; // engedélyező
                szöveg += $" {Adat.Engedély}, "; // engedély új rögzítés
                szöveg += $" {Adat.Státus}, ";  // státus
                szöveg += $"'{Adat.Terület}')";
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


        public void Módosítás(Adat_Külső_Cégek Adat)
        {
            try
            {

                string szöveg = $"UPDATE {táblanév}  Set ";
                szöveg += $" cég='{Adat.Cég}', ";  // cég
                szöveg += $" címe='{Adat.Címe}', "; // címe
                szöveg += $" cég_email='{Adat.Cég_email}', ";// cég_email
                szöveg += $" felelős_személy='{Adat.Felelős_személy}', ";// felelős_személy
                szöveg += $" Felelős_telefonszám='{Adat.Felelős_telefonszám}', ";  // Felelős_telefonszám
                szöveg += $" munkaleírás='{Adat.Munkaleírás}', ";  // munkaleírás
                szöveg += $" Mikor='{Adat.Mikor}', "; 
                szöveg += $" érv_kezdet='{Adat.Érv_kezdet:yyyy.MM.dd}', "; // érv_kezdet
                szöveg += $" érv_vég='{Adat.Érv_vég:yyyy.MM.dd}', "; // érv_vég
                szöveg += $" engedély={Adat.Engedély}, ";  // engedély
                szöveg += $" státus={Adat.Státus} ";
                szöveg += $" WHERE [Cégid]={Adat.Cégid}";
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


        public void Engedélyezésre(List<Adat_Külső_Cégek> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Külső_Cégek Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév}  Set ";
                    szöveg += $" engedély={Adat.Engedély} ";  // engedély
                    szöveg += $" WHERE [Cégid]={Adat.Cégid}";
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

        public void Engedélyezés(List<Adat_Külső_Cégek> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Külső_Cégek Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév}  SET ";
                    szöveg += $" engedély={Adat.Engedély}, "; // engedély
                    szöveg += $" Engedélyezés_dátuma='{Adat.Engedélyezés_dátuma:yyyy.MM.dd HH:mm}', ";
                    szöveg += $" Engedélyező='{Adat.Engedélyező}'";
                    szöveg += $" WHERE [Cégid]={Adat.Cégid}";
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
