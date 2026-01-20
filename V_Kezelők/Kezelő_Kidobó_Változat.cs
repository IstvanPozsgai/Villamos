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
    public class Kezelő_Kidobó_Változat
    {
        readonly string jelszó = "erzsébet";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Főkönyv\Kidobó\kidobósegéd.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kidobósegédadattábla(hely.KönyvSzerk());
        }
        public List<Adat_Kidobó_Változat> Lista_Adat(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = $"SELECT * FROM Változattábla  order by id";
            List<Adat_Kidobó_Változat> Adatok = new List<Adat_Kidobó_Változat>();
            Adat_Kidobó_Változat Adat;

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
                                Adat = new Adat_Kidobó_Változat(
                                      rekord["id"].ToÉrt_Long(),
                                      rekord["Változatnév"].ToStrTrim()
                                      );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Kidobó_Változat Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"INSERT INTO Változattábla (id, változatnév) VALUES (";
                szöveg += $"{Sorszám(hely)}, '{Adat.Változatnév}') ";
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

        private long Sorszám(string Telephely)
        {
            long Válasz = 1;
            try
            {
                List<Adat_Kidobó_Változat> Adatok = Lista_Adat(Telephely);
                if (Adatok != null && Adatok.Count > 0) Válasz = Adatok.Max(a => a.Id) + 1;
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
            return Válasz;
        }

        public void Törlés(string Telephely, Adat_Kidobó_Változat Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"DELETE FROM Változattábla WHERE Változatnév='{Adat.Változatnév}'";
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
