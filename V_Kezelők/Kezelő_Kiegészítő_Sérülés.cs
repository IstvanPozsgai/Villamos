using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;


namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Sérülés
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\kiegészítő.mdb".KönyvSzerk();
        readonly string jelszó = "Mocó";
        readonly string táblanév = "sérülés";

        public Kezelő_Kiegészítő_Sérülés()
        {
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }

        public List<Adat_Kiegészítő_Sérülés> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY id";
            Adat_Kiegészítő_Sérülés Adat;
            List<Adat_Kiegészítő_Sérülés> Adatok = new List<Adat_Kiegészítő_Sérülés>();

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
                                Adat = new Adat_Kiegészítő_Sérülés(
                                           rekord["id"].ToÉrt_Int(),
                                           rekord["név"].ToStrTrim(),
                                           rekord["vezér1"].ToÉrt_Bool(),
                                           rekord["Csoport1"].ToÉrt_Int(),
                                           rekord["Csoport2"].ToÉrt_Int(),
                                           rekord["vezér2"].ToÉrt_Bool(),
                                           rekord["sorrend1"].ToÉrt_Int(),
                                           rekord["sorrend2"].ToÉrt_Int(),
                                           rekord["költséghely"].ToStrTrim()
                                           );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Kiegészítő_Sérülés Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} ";
                szöveg += " (id, név, csoport1, csoport2, sorrend1, sorrend2, vezér1, vezér2, költséghely ) VALUES ";
                szöveg += $"({Adat.ID}, ";
                szöveg += $"'{Adat.Név}', ";
                szöveg += $"{Adat.Csoport1}, ";
                szöveg += $"{Adat.Csoport2}, ";
                szöveg += $"{Adat.Sorrend1}, ";
                szöveg += $"{Adat.Sorrend2}, ";
                szöveg += $"{Adat.Vezér1}, ";
                szöveg += $"{Adat.Vezér2}, ";
                szöveg += $"'{Adat.Költséghely}')";

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

        public void Módosítás(Adat_Kiegészítő_Sérülés Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"név='{Adat.Név}', ";
                szöveg += $"csoport1={Adat.Csoport1}, ";
                szöveg += $"csoport2={Adat.Csoport2}, ";
                szöveg += $"sorrend1={Adat.Sorrend1}, ";
                szöveg += $"sorrend2={Adat.Sorrend2}, ";
                szöveg += $"vezér1={Adat.Vezér1}, ";
                szöveg += $"vezér2={Adat.Vezér2}, ";
                szöveg += $"költséghely='{Adat.Költséghely}' ";
                szöveg += $" WHERE id={Adat.ID}";

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

        public void Törlés(Adat_Kiegészítő_Sérülés Adat)
        {
            try
            {
                string szöveg = $"Delete FROM {táblanév} where id={Adat.ID}";
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
