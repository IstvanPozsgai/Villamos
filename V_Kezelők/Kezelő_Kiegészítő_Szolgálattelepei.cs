using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;


namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Szolgálattelepei
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb".KönyvSzerk();
        readonly string jelszó = "Mocó";

        public Kezelő_Kiegészítő_Szolgálattelepei()
        {
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }


        public List<Adat_Kiegészítő_Szolgálattelepei> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM szolgálattelepeitábla order by sorszám";
            Adat_Kiegészítő_Szolgálattelepei Adat;
            List<Adat_Kiegészítő_Szolgálattelepei> Adatok = new List<Adat_Kiegészítő_Szolgálattelepei>();

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
                                Adat = new Adat_Kiegészítő_Szolgálattelepei(
                                           rekord["sorszám"].ToÉrt_Int(),
                                           rekord["telephelynév"].ToStrTrim(),
                                           rekord["szolgálatnév"].ToStrTrim(),
                                           rekord["felelősmunkahely"].ToStrTrim(),
                                           rekord["raktár"].ToStrTrim()
                                           );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Kiegészítő_Szolgálattelepei Adat)
        {
            try
            {
                string szöveg = "INSERT INTO szolgálattelepeitábla ( sorszám, szolgálatnév, telephelynév, felelősmunkahely, raktár )";
                szöveg += $" VALUES ({Adat.Sorszám}, ";
                szöveg += $"'{Adat.Szolgálatnév}', ";
                szöveg += $"'{Adat.Telephelynév}', ";
                szöveg += $"'{Adat.Felelősmunkahely}',";
                szöveg += $"'{Adat.Raktár}')";
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

        public void Módosítás(Adat_Kiegészítő_Szolgálattelepei Adat)
        {
            try
            {
                string szöveg = "UPDATE szolgálattelepeitábla SET ";
                szöveg += $" sorszám={Adat.Sorszám},";
                szöveg += $" szolgálatnév='{Adat.Szolgálatnév}', ";
                szöveg += $" felelősmunkahely='{Adat.Felelősmunkahely}', ";
                szöveg += $" raktár='{Adat.Raktár}' ";
                szöveg += $" WHERE telephelynév='{Adat.Telephelynév}'";
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

        public void Törlés(Adat_Kiegészítő_Szolgálattelepei Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM szolgálattelepeitábla ";
                szöveg += $" where telephelynév='{Adat.Telephelynév}' and sorszám={Adat.Sorszám}";
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
