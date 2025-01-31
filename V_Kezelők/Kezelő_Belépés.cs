using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Belépés_Bejelentkezés
    {
        readonly string jelszó = "forgalmiutasítás";

        public List<Adat_Belépés_Bejelentkezés> Lista_Adatok(string Telephely)
        {
            string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Belépés.mdb".KönyvSzerk();
            string szöveg = $"SELECT * FROM bejelentkezés";
            List<Adat_Belépés_Bejelentkezés> Adatok = new List<Adat_Belépés_Bejelentkezés>();
            Adat_Belépés_Bejelentkezés Adat;

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
                                Adat = new Adat_Belépés_Bejelentkezés(
                                      rekord["sorszám"].ToÉrt_Long(),
                                      rekord["név"].ToStrTrim(),
                                      rekord["jelszó"].ToStrTrim(),
                                      rekord["jogkör"].ToStrTrim()
                                      );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;

        }


        public void Rögzítés(string Telephely, Adat_Belépés_Bejelentkezés Adat)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Belépés.mdb".KönyvSzerk();
                string szöveg = $"INSERT INTO Bejelentkezés (Név, Jelszó, Jogkör)";
                szöveg += $"Values('{Adat.Név}', ";
                szöveg += $"'{Adat.Jelszó}', ";
                szöveg += $"'{Adat.Jogkör}' )";
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


        public void Módosítás(string Telephely, Adat_Belépés_Bejelentkezés Adat)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Belépés.mdb".KönyvSzerk();
                string szöveg = $"UPDATE Bejelentkezés SET ";
                szöveg += $"jelszó='{Adat.Jelszó}' ";
                szöveg += $"WHERE név='{Adat.Név}'";
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


        public void Törlés(string Telephely, Adat_Belépés_Bejelentkezés Adat)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Belépés.mdb".KönyvSzerk();
                string szöveg = $"DELETE * From Bejelentkezés where név='{Adat.Név}'";
                MyA.ABtörlés(hely, jelszó, szöveg);
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
