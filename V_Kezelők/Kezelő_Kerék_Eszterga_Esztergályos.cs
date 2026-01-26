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
    public class Kezelő_Kerék_Eszterga_Esztergályos
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";
        readonly string jelszó = "RónaiSándor";
        readonly string táblanév = "Esztergályos";


        public Kezelő_Kerék_Eszterga_Esztergályos()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kerék_Törzs(hely.KönyvSzerk());
        }

        public List<Adat_Kerék_Eszterga_Esztergályos> Lista_Adatok()
        {
            List<Adat_Kerék_Eszterga_Esztergályos> Adatok = new List<Adat_Kerék_Eszterga_Esztergályos>();
            Adat_Kerék_Eszterga_Esztergályos Adat;
            string szöveg = $"SELECT * FROM {táblanév}";
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
                                Adat = new Adat_Kerék_Eszterga_Esztergályos(
                                        rekord["Dolgozószám"].ToStrTrim(),
                                        rekord["Dolgozónév"].ToStrTrim(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Státus"].ToÉrt_Int()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Döntés(Adat_Kerék_Eszterga_Esztergályos Adat)
        {
            try
            {
                List<Adat_Kerék_Eszterga_Esztergályos> Adatok = Lista_Adatok();
                Adat_Kerék_Eszterga_Esztergályos Elem = (from a in Adatok
                                                         where a.Dolgozószám == Adat.Dolgozószám
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

        public void Módosítás(Adat_Kerék_Eszterga_Esztergályos Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"telephely='{Adat.Telephely}', ";
                szöveg += $"státus={Adat.Státus} ";
                szöveg += $" WHERE dolgozószám='{Adat.Dolgozószám}'";
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

        public void Rögzítés(Adat_Kerék_Eszterga_Esztergályos Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} (Dolgozószám, dolgozónév, telephely, státus) VALUES (";
                szöveg += $"'{Adat.Dolgozószám}','{Adat.Dolgozónév}','{Adat.Telephely}', {Adat.Státus} )";
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

        public void Törlés(string Dolgozószám)
        {
            try
            {
                string szöveg = $"DELETE FROM {táblanév}  WHERE dolgozószám='{Dolgozószám}'";
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
