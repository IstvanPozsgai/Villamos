using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;


namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Idő_Kor
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kiegészítő.mdb".KönyvSzerk();
        readonly string jelszó = "Mocó";
        readonly string táblanév = "idő_korrekció";

        public Kezelő_Kiegészítő_Idő_Kor()
        {
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }

        public List<Adat_Kiegészítő_Idő_Kor> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév} ";
            List<Adat_Kiegészítő_Idő_Kor> Adatok = new List<Adat_Kiegészítő_Idő_Kor>();
            Adat_Kiegészítő_Idő_Kor Adat;

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
                                Adat = new Adat_Kiegészítő_Idő_Kor(
                                     rekord["id"].ToÉrt_Long(),
                                     rekord["kiadási"].ToÉrt_Long(),
                                     rekord["érkezési"].ToÉrt_Long()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Kiegészítő_Idő_Kor Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév}  (id, kiadási, érkezési ) ";
                szöveg += $"VALUES ('{Adat.Id}, ";
                szöveg += $"{Adat.Kiadási}, ";
                szöveg += $"{Adat.Érkezési}) ";
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

        public void Módosítás(Adat_Kiegészítő_Idő_Kor Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} Set ";
                szöveg += $"érkezési={Adat.Érkezési}, ";
                szöveg += $"kiadási={Adat.Kiadási} ";
                szöveg += $" where id={Adat.Id} ";
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
