using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Munkakör
    {
        readonly string jelszó = "ladányis";
        public List<Adat_Munkakör> Lista_Adatok(string hely)
        {
            List<Adat_Munkakör> Adatok = new List<Adat_Munkakör>();
            Adat_Munkakör Adat;

            string szöveg = "Select * FROM munkakörtábla";
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
                                Adat = new Adat_Munkakör(
                                        rekord["id"].ToÉrt_Long(),
                                        rekord["Megnevezés"].ToStrTrim(),
                                        rekord["PDFfájlnév"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Long(),
                                        rekord["telephely"].ToStrTrim(),
                                        rekord["HRazonosító"].ToStrTrim(),
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["Rögzítő"].ToStrTrim()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, Adat_Munkakör Adat)
        {
            try
            {
                string szöveg = "INSERT INTO munkakörtábla ";
                szöveg += "(ID,  Megnevezés, PDFfájlnév, státus, telephely,  Hrazonosító, dátum,  rögzítő)";
                szöveg += " VALUES (";
                szöveg += $"{Sorszám (hely)}, ";
                szöveg += $"'{Adat.Megnevezés}', ";
                szöveg += $"'{Adat.PDFfájlnév}', ";
                szöveg += $"{Adat.Státus}, ";
                szöveg += $"'{Adat.Telephely}', ";
                szöveg += $"'{Adat.HRazonosító}', ";
                szöveg += $"'{Adat.Dátum}', ";
                szöveg += $"'{Adat.Rögzítő}') ";
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

        public void Törlés(string hely, Adat_Munkakör Adat) 
        {
            try
            {
                string szöveg = $"UPDATE munkakörtábla SET státus=1 WHERE Id={Adat.ID}";
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

        public long Sorszám(string hely)
        {
            long Válasz = 1;
            try
            {
                List<Adat_Munkakör> Adatok = Lista_Adatok(hely);
                if (Adatok.Count > 0) Válasz = Adatok.Max(j => j.ID) + 1;
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
