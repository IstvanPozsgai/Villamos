using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Típusrendezéstábla
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb".Ellenőrzés();
        readonly string jelszó = "Mocó";

        public List<Adat_Kiegészítő_Típusrendezéstábla> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM típusrendezéstábla order by sorszám";
            List<Adat_Kiegészítő_Típusrendezéstábla> Adatok = new List<Adat_Kiegészítő_Típusrendezéstábla>();
            Adat_Kiegészítő_Típusrendezéstábla Adat;

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
                                Adat = new Adat_Kiegészítő_Típusrendezéstábla(
                                     rekord["sorszám"].ToÉrt_Long(),
                                     rekord["főkategória"].ToStrTrim(),
                                     rekord["típus"].ToStrTrim(),
                                     rekord["alTípus"].ToStrTrim(),
                                     rekord["telephely"].ToStrTrim(),
                                     rekord["telephelyitípus"].ToStrTrim()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Kiegészítő_Típusrendezéstábla Adat)
        {
            try
            {
                string szöveg = "INSERT INTO típusrendezéstábla ( sorszám, főkategória, típus, altípus, telephely, telephelyitípus)";
                szöveg += $" VALUES ({Adat.Sorszám}, ";
                szöveg += $"'{Adat.Főkategória}', ";
                szöveg += $"'{Adat.Típus}', ";
                szöveg += $"'{Adat.AlTípus}', ";
                szöveg += $"'{Adat.Telephely}', ";
                szöveg += $"'{Adat.Telephelyitípus}')";
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

        public void Törlés(Adat_Kiegészítő_Típusrendezéstábla Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM típusrendezéstábla where sorszám={Adat.Sorszám}";
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

        public void Módosítás(Adat_Kiegészítő_Típusrendezéstábla Adat)
        {
            try
            {
                string szöveg = $"UPDATE típusrendezéstábla SET ";
                szöveg += $"sorszám='{Adat.Sorszám}'";
                szöveg += $"WHERE telephely='{Adat.Telephely}' ";
                szöveg += $"and telephelyitípus='{Adat.Telephelyitípus}'";
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
