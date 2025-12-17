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
    public class Kezelő_Kerék_Eszterga_Terjesztés
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";
        readonly string jelszó = "RónaiSándor";
        readonly string táblanév = "terjesztés";

        public Kezelő_Kerék_Eszterga_Terjesztés()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kerék_Törzs(hely.KönyvSzerk());
        }

        public List<Adat_Kerék_Eszterga_Terjesztés> Lista_Adatok()
        {
            List<Adat_Kerék_Eszterga_Terjesztés> Adatok = new List<Adat_Kerék_Eszterga_Terjesztés>();

            string szöveg = $"SELECT * FROM {táblanév} ORDER BY  név";

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
                                Adat_Kerék_Eszterga_Terjesztés Adat = new Adat_Kerék_Eszterga_Terjesztés(
                                        rekord["Név"].ToStrTrim(),
                                        rekord["Email"].ToStrTrim(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Változat"].ToÉrt_Int()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás (Adat_Kerék_Eszterga_Terjesztés Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"név='{Adat.Név}', ";  //Név
                szöveg += $"Telephely='{Adat.Telephely}', ";    //Telephely
                szöveg += $"Változat={Adat.Változat} ";    //Változat
                szöveg += $" WHERE email='{Adat.Email}'";
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

        public void Rögzítés(Adat_Kerék_Eszterga_Terjesztés Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (Név, Email, Telephely, Változat ) VALUES (";
                szöveg += $"'{Adat.Név}', ";  //Név
                szöveg += $"'{Adat.Email}', ";  // Email
                szöveg += $"'{Adat.Telephely}', ";    //Telephely
                szöveg += $"{Adat.Változat} )";    //Változat
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

        public void Döntés(Adat_Kerék_Eszterga_Terjesztés Adat)
        {
            try
            {
                List<Adat_Kerék_Eszterga_Terjesztés> Adatok = Lista_Adatok();
                Adat_Kerék_Eszterga_Terjesztés Elem = (from a in Adatok
                                                       where a.Email == Adat.Email
                                                       select a).FirstOrDefault();
                if (Elem != null)
                    Módosítás(Adat);
                else
                    Rögzítés(Adat);
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

        public void Törlés(string Email)
        {
            try
            {
                string szöveg = $"DELETE FROM {táblanév} WHERE email='{Email}'";
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
