using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Főkategóriatábla
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb";
        readonly string jelszó = "Mocó";

        public List<Adat_Kiegészítő_Főkategóriatábla> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM főkategóriatábla  order by  sorszám";
            List<Adat_Kiegészítő_Főkategóriatábla> Adatok = new List<Adat_Kiegészítő_Főkategóriatábla>();
            Adat_Kiegészítő_Főkategóriatábla Adat;

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
                                Adat = new Adat_Kiegészítő_Főkategóriatábla(
                                     rekord["sorszám"].ToÉrt_Long(),
                                     rekord["főkategória"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Kiegészítő_Főkategóriatábla Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO főkategóriatábla (sorszám, főkategória) ";
                szöveg += $"VALUES ({Adat.Sorszám}, ";
                szöveg += $"'{Adat.Főkategória}')";
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

        public void Törlés(Adat_Kiegészítő_Főkategóriatábla Adat)
        {
            try
            {
                string szöveg = $"DELETE  FROM főkategóriatábla WHERE főkategória='{Adat.Főkategória}'";
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

        public void Módosítás(Adat_Kiegészítő_Főkategóriatábla Adat)
        {
            try
            {
                string szöveg = $"UPDATE főkategóriatábla SET ";
                szöveg += $"főkategória='{Adat.Főkategória}' ";
                szöveg += $"WHERE sorszám={Adat.Sorszám}";
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
