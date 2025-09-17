using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Felmentés
    {
        readonly string jelszó = "Mocó";
        string hely;
        readonly string táblanév = "Felmentés";

        private bool FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Segéd\kiegészítő1.mdb";
            return File.Exists(hely);
        }

        public List<Adat_Kiegészítő_Felmentés> Lista_Adatok(string Telephely)
        {
            List<Adat_Kiegészítő_Felmentés> Adatok = new List<Adat_Kiegészítő_Felmentés>();
            if (FájlBeállítás(Telephely))
            {
                string szöveg = $"SELECT * FROM {táblanév}";

                Adat_Kiegészítő_Felmentés Adat;

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
                                    Adat = new Adat_Kiegészítő_Felmentés(
                                            rekord["id"].ToÉrt_Int(),
                                            rekord["Címzett"].ToStrTrim(),
                                            rekord["Másolat"].ToStrTrim(),
                                            rekord["Tárgy"].ToStrTrim(),
                                            rekord["Kértvizsgálat"].ToStrTrim(),
                                            rekord["Bevezetés"].ToStrTrim(),
                                            rekord["Tárgyalás"].ToStrTrim(),
                                            rekord["Befejezés"].ToStrTrim(),
                                            rekord["CiklusTípus"].ToStrTrim()
                                              );
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string Telephely, Adat_Kiegészítő_Felmentés Adat)
        {
            try
            {
                if (FájlBeállítás(Telephely))
                {
                    // Módosítjuk
                    string szöveg = $"UPDATE {táblanév}  SET ";
                    szöveg += $"Címzett='{Adat.Címzett}', ";
                    szöveg += $"Másolat='{Adat.Másolat}', ";
                    szöveg += $"Tárgy='{Adat.Tárgy}', ";
                    szöveg += $"Kértvizsgálat='{Adat.Kértvizsgálat}', ";
                    szöveg += $"Bevezetés='{Adat.Bevezetés}', ";
                    szöveg += $"Tárgyalás='{Adat.Tárgyalás}', ";
                    szöveg += $"Befejezés='{Adat.Befejezés}' ";
                    szöveg += $"CiklusTípus='{Adat.CiklusTípus}' ";
                    szöveg += $" WHERE Id={Adat.Id}";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
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

        public void Rögzítés(string Telephely, Adat_Kiegészítő_Felmentés Adat)
        {
            try
            {
                if (FájlBeállítás(Telephely))
                {
                    string szöveg = $"INSERT INTO {táblanév} (id, Címzett, Másolat,Tárgy,Kértvizsgálat, Bevezetés, Tárgyalás,Befejezés, CiklusTípus ) VALUES (";
                    szöveg += $"{Sorszám(Telephely)}, ";
                    szöveg += $"'{Adat.Címzett}', ";
                    szöveg += $"'{Adat.Másolat}', ";
                    szöveg += $"'{Adat.Tárgy}', ";
                    szöveg += $"'{Adat.Kértvizsgálat}', ";
                    szöveg += $"'{Adat.Bevezetés}', ";
                    szöveg += $"'{Adat.Tárgyalás}', ";
                    szöveg += $"'{Adat.Befejezés}', ";
                    szöveg += $"'{Adat.CiklusTípus}') ";

                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
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

        private int Sorszám(string Telephely)
        {
            int Válasz = 1;
            try
            {
                List<Adat_Kiegészítő_Felmentés> Adatok = Lista_Adatok(Telephely);
                if (Adatok.Count > 0) Válasz = Adatok.Max(a => a.Id) + 1;

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
