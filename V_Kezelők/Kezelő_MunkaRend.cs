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
    public class Kezelő_MunkaRend
    {
        readonly string jelszó = "kismalac";
        string hely;

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Munkalap\munkalap{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Munkalap_tábla(hely.KönyvSzerk());
        }


        public List<Adat_MunkaRend> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = "SELECT * FROM munkarendtábla ORDER BY id";
            List<Adat_MunkaRend> Adatok = new List<Adat_MunkaRend>();
            Adat_MunkaRend Adat;

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
                                Adat = new Adat_MunkaRend(
                                          rekord["ID"].ToÉrt_Long(),
                                          rekord["munkarend"].ToStrTrim(),
                                          rekord["látszódik"].ToÉrt_Bool()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, int Év, Adat_MunkaRend Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = "INSERT INTO munkarendtábla (id, munkarend, látszódik)  VALUES (";
                szöveg += $"{Sorszám(hely)}, ";
                szöveg += $"'{Adat.Munkarend}', ";
                szöveg += $" {Adat.Látszódik} ) ";
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

        public void Módosítás(string Telephely, int Év, Adat_MunkaRend Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = " UPDATE  munkarendtábla SET ";
                szöveg += $" munkarend='{Adat.Munkarend}' ";
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

        public void Módosítás(string Telephely, int Év, long sorszám, bool Látszódik)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string jelszó = "kismalac";
                string szöveg = $"UPDATE munkarendtábla SET látszódik={Látszódik} WHERE id={sorszám}";
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

        private long Sorszám(string Telephely, int Év)
        {
            long Válasz = 1;
            try
            {
                FájlBeállítás(Telephely, Év);
                List<Adat_MunkaRend> Adatok = Lista_Adatok(Telephely, Év);
                if (Adatok != null && Adatok.Count > 0) Válasz = Adatok.Max(x => x.ID) + 1;
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
    }
}
