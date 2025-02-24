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
    public class Kezelő_Váltós_Váltóscsopitábla
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Váltóscsoportvezetők.mdb".KönyvSzerk();
        readonly string jelszó = "Gábor";

        public Kezelő_Váltós_Váltóscsopitábla()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Váltóscsopitábla(hely.KönyvSzerk());
        }

        public List<Adat_Váltós_Váltóscsopitábla> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM tábla  order by  csoport,telephely";
            List<Adat_Váltós_Váltóscsopitábla> Adatok = new List<Adat_Váltós_Váltóscsopitábla>();
            Adat_Váltós_Váltóscsopitábla Adat;

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
                                Adat = new Adat_Váltós_Váltóscsopitábla(
                                          rekord["Csoport"].ToStrTrim(),
                                          rekord["Telephely"].ToStrTrim(),
                                          rekord["Név"].ToStrTrim()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Váltós_Váltóscsopitábla Adat)
        {
            try
            {
                string szöveg = "INSERT INTO tábla (csoport, telephely, név) VALUES (";
                szöveg += $"'{Adat.Csoport}', ";
                szöveg += $"'{Adat.Telephely}', ";
                szöveg += $"'{Adat.Név}') ";
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

        public void Módosítás(Adat_Váltós_Váltóscsopitábla Adat)
        {
            try
            {
                string szöveg = " UPDATE  tábla SET ";
                szöveg += $" név='{Adat.Név}' ";
                szöveg += $" WHERE csoport='{Adat.Csoport}' and telephely='{Adat.Telephely}'";
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

        public void Törlés(Adat_Váltós_Váltóscsopitábla Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM tábla WHERE csoport='{Adat.Csoport}' and telephely='{Adat.Telephely}'";
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
