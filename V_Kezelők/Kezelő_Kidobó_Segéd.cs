using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kidobó_Segéd
    {
        readonly string jelszó = "erzsébet";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Főkönyv\Kidobó\kidobósegéd.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kidobósegédadattábla(hely.KönyvSzerk());
        }
        public List<Adat_Kidobó_Segéd> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = "SELECT * FROM Kidobósegédtábla  order by változatnév, szolgálatiszám";
            List<Adat_Kidobó_Segéd> Adatok = new List<Adat_Kidobó_Segéd>();
            Adat_Kidobó_Segéd Adat;

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
                                Adat = new Adat_Kidobó_Segéd(
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["szolgálatiszám"].ToStrTrim(),
                                    rekord["kezdés"].ToÉrt_DaTeTime(),
                                    rekord["végzés"].ToÉrt_DaTeTime(),
                                    rekord["Kezdéshely"].ToStrTrim(),
                                    rekord["Végzéshely"].ToStrTrim(),
                                    rekord["Változatnév"].ToStrTrim(),
                                    rekord["megjegyzés"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Törlés(string Telephely, string változatnév)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<Adat_Kidobó_Segéd> AdatokÖ = Lista_Adatok(Telephely);
                List<Adat_Kidobó_Segéd> Adatok = (from a in AdatokÖ
                                                  where a.Változatnév == változatnév
                                                  select a).ToList();
                if (Adatok != null && Adatok.Count > 0)
                {
                    string szöveg = $"DELETE FROM Kidobósegédtábla WHERE Változatnév='{változatnév}'";
                    MyA.ABtörlés(hely, jelszó, szöveg);
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

        public void Törlés(string Telephely, string változatnév, string szolgálatiszám)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<Adat_Kidobó_Segéd> AdatokÖ = Lista_Adatok(Telephely);
                List<Adat_Kidobó_Segéd> Adatok = (from a in AdatokÖ
                                                  where a.Változatnév == változatnév
                                                  && a.Szolgálatiszám == szolgálatiszám
                                                  select a).ToList();
                if (Adatok != null && Adatok.Count > 0)
                {
                    string szöveg = $"DELETE FROM Kidobósegédtábla WHERE Változatnév='{változatnév}' AND szolgálatiszám='{szolgálatiszám}'";
                    MyA.ABtörlés(hely, jelszó, szöveg);
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

        public void Rögzítés(string Telephely, Adat_Kidobó_Segéd Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "INSERT INTO Kidobósegédtábla (változatnév, forgalmiszám, szolgálatiszám, Kezdéshely, Végzéshely, megjegyzés, Kezdés, Végzés) VALUES (";
                szöveg += $"'{Adat.Változatnév}', ";      //változatnév
                szöveg += $"'{Adat.Forgalmiszám}', ";      //forgalmiszám
                szöveg += $"'{Adat.Szolgálatiszám}', ";    //szolgálatiszám
                szöveg += $"'{Adat.Kezdéshely}', ";          //Kezdéshely
                szöveg += $"'{Adat.Végzéshely}', ";          //Végzéshely
                szöveg += $"'{Adat.Megjegyzés}', ";          //megjegyzés
                szöveg += $"'{Adat.Kezdés:HH:mm}', ";      //Kezdés
                szöveg += $"'{Adat.Végzés:HH:mm}' )";       //Végzés
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

        public void Módosítás(string Telephely, Adat_Kidobó_Segéd Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE Kidobósegédtábla  SET ";
                szöveg += $"Kezdéshely='{Adat.Kezdéshely}', ";
                szöveg += $"Végzéshely='{Adat.Végzéshely}', ";
                szöveg += $"megjegyzés='{Adat.Megjegyzés}', ";
                szöveg += $" Kezdés='{Adat.Kezdés:HH:mm}', ";
                szöveg += $" végzés='{Adat.Végzés}' ";
                szöveg += $" WHERE  szolgálatiszám='{Adat.Szolgálatiszám}' AND változatnév='{Adat.Változatnév}'";
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
    }

}
