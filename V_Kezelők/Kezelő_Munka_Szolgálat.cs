using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Munka_Szolgálat
    {
        readonly string jelszó = "kismalac";
        public List<Adat_Munka_Szolgálat> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * FROM szolgálattábla ";
            List<Adat_Munka_Szolgálat> Adatok = new List<Adat_Munka_Szolgálat>();
            Adat_Munka_Szolgálat Adat;

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
                                Adat = new Adat_Munka_Szolgálat(
                                          rekord["költséghely"].ToStrTrim(),
                                          rekord["szolgálat"].ToStrTrim(),
                                          rekord["üzem"].ToStrTrim(),
                                          rekord["A1"].ToStrTrim(),
                                          rekord["A2"].ToStrTrim(),
                                          rekord["A3"].ToStrTrim(),
                                          rekord["A4"].ToStrTrim(),
                                          rekord["A5"].ToStrTrim(),
                                          rekord["A6"].ToStrTrim(),
                                          rekord["A7"].ToStrTrim()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string hely, Adat_Munka_Szolgálat Adat)
        {
            try
            {
                string szöveg = " UPDATE  szolgálattábla SET ";
                szöveg += $" költséghely='{Adat.Költséghely}', "; 
                szöveg += $" szolgálat='{Adat.Szolgálat}', "; 
                szöveg += $" üzem='{Adat.Üzem}' "; 
                szöveg += " WHERE A7='0'";
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

        public void Rögzítés(string hely, Adat_Munka_Szolgálat Adat)
        {
            try
            {
                string szöveg = "INSERT INTO szolgálattábla (költséghely, szolgálat, üzem, A1, A2, A3, A4, A5, A6, A7)  VALUES (";
                szöveg += $"'{Adat.Költséghely}', "; 
                szöveg += $"'{Adat.Szolgálat}', ";
                szöveg += $"'{Adat.Üzem}', ";
                szöveg += " '0', '0', '0', '0', '0', '0', '0' )";
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
