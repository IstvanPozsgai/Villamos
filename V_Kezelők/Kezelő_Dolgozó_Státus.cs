using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Dolgozó_Státus
    {
        readonly string jelszó = "forgalmiutasítás";

        public List<Adat_Dolgozó_Státus> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * FROM státustábla ORDER BY ID desc";
            List<Adat_Dolgozó_Státus> Adatok = new List<Adat_Dolgozó_Státus>();
            Adat_Dolgozó_Státus Adat;

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

                                Adat = new Adat_Dolgozó_Státus(
                                          rekord["ID"].ToÉrt_Long(),
                                          rekord["Névki"].ToStrTrim(),
                                          rekord["Részmunkaidős"].ToÉrt_Int(),
                                          rekord["Hrazonosítóki"].ToStrTrim(),
                                          rekord["Bérki"].ToÉrt_Double(),
                                          rekord["telephelyki"].ToStrTrim(),
                                          rekord["kilépésoka"].ToStrTrim(),
                                          rekord["kilépésdátum"].ToÉrt_DaTeTime(),
                                          rekord["Névbe"].ToStrTrim(),
                                          rekord["Hrazonosítóbe"].ToStrTrim(),
                                          rekord["Bérbe"].ToÉrt_Double(),
                                          rekord["Honnanjött"].ToStrTrim(),
                                          rekord["telephelybe"].ToStrTrim(),
                                          rekord["belépésidátum"].ToÉrt_DaTeTime(),
                                          rekord["Státusváltozások"].ToStrTrim(),
                                          rekord["Státusváltzásoka"].ToStrTrim(),
                                          rekord["Megjegyzés"].ToStrTrim()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosít_Be(string hely, Adat_Dolgozó_Státus Adat)
        {
            try
            {
                string szöveg = "UPDATE  státustábla SET ";
                szöveg += $" Névbe='{Adat.Névbe}', ";
                szöveg += $" Hrazonosítóbe ='{Adat.Hrazonosítóbe}', ";
                szöveg += $" belépésidátum ='{Adat.Belépésidátum:yyyy.MM.dd}', ";
                szöveg += $" bérbe ={Adat.Bérbe} ";
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

        public void Módosít_Be_Honnan(string hely, Adat_Dolgozó_Státus Adat)
        {
            try
            {
                string szöveg = "UPDATE státustábla  SET  ";
                szöveg += $"Honnanjött='{Adat.Honnanjött}' ";
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

        public void Módosít_Státus(string hely, Adat_Dolgozó_Státus Adat)
        {
            try
            {
                string szöveg = "UPDATE státustábla  SET  ";
                szöveg += $" Státusváltozások='{Adat.Státusváltozások}'";
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

        public void Módosít_Státus_Teljes(string hely, Adat_Dolgozó_Státus Adat)
        {
            try
            {
                string szöveg = "UPDATE státustábla  SET  ";
                szöveg += $"Státusváltzásoka='{Adat.Státusváltozoka}', ";
                szöveg += $"Megjegyzés='{Adat.Megjegyzés}', ";
                szöveg += $"Részmunkaidős={Adat.Részmunkaidős} ";
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

        public void Módosít_Státus_Megjegyzés(string hely, Adat_Dolgozó_Státus Adat)
        {
            try
            {
                string szöveg = "UPDATE státustábla  SET  ";
                szöveg += $"Megjegyzés='{Adat.Megjegyzés}', ";
                szöveg += $"Részmunkaidős={Adat.Részmunkaidős} ";
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

        public void Módosít_Be_Teljes(string hely, Adat_Dolgozó_Státus Adat)
        {
            try
            {
                string szöveg = "UPDATE státustábla  SET  ";
                szöveg += $" Névbe='{Adat.Névbe}', ";
                szöveg += $" Hrazonosítóbe ='{Adat.Hrazonosítóbe}', ";
                szöveg += $" bérbe ={Adat.Bérbe}, ";
                szöveg += $" telephelybe='{Adat.Telephelybe}', ";
                szöveg += $" Honnanjött='{Adat.Honnanjött}', ";
                szöveg += $" belépésidátum ='{Adat.Belépésidátum:yyyy.MM.dd}' ";
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

        public void Módosít_Kilép(string hely, Adat_Dolgozó_Státus Adat)
        {
            try
            {
                string szöveg = "UPDATE  státustábla SET ";
                szöveg += $" kilépésdátum='{Adat.Kilépésdátum:yyyy.MM.dd}' ";
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

        public void Módosít_Kilép_Ok(string hely, Adat_Dolgozó_Státus Adat)
        {
            try
            {
                string szöveg = "UPDATE státustábla  SET  ";
                szöveg += $"kilépésoka='{Adat.Kilépésoka}' ";
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

        public void Módosít_Kilép_Teljes(string hely, Adat_Dolgozó_Státus Adat)
        {
            try
            {
                string szöveg = "UPDATE státustábla  SET  ";
                szöveg += $"névki='{Adat.Névki}', ";
                szöveg += $"Hrazonosítóki='{Adat.Hrazonosítóki}', ";
                szöveg += $"bérki={Adat.Bérki}, ";
                szöveg += $"telephelyki='{Adat.Telephelyki}', ";
                szöveg += $"kilépésoka='{Adat.Kilépésoka}', ";
                szöveg += $"kilépésdátum='{Adat.Kilépésdátum:yyyy.MM.dd}' ";
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

        public long Rögzítés_Új(string hely, Adat_Dolgozó_Státus Adat)
        {
            long Válasz = 1;
            try
            {
                Válasz = Sorszám(hely);
                string szöveg = "INSERT INTO státustábla (id, Státusváltozások, telephelyki, honnanjött, Hrazonosítóbe, névbe) VALUES";
                szöveg += $" ({Válasz},'{Adat.Státusváltozások}', '_', '_', '_', '_')";
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
            return Válasz;
        }
  
        public void Rögzítés_Alap(string hely, Adat_Dolgozó_Státus Adat)
        {
            try
            {
             long   Válasz = Sorszám(hely);
                string szöveg = "INSERT INTO státustábla ";
                szöveg += " (id, Névki, Hrazonosítóki, kilépésdátum, Bérki, telephelyki, Státusváltozások, névbe,  Hrazonosítóbe, honnanjött, belépésidátum )";
                szöveg += " VALUES (";
                szöveg += $"{Válasz}, ";
                szöveg += $" '{Adat.Névki}', ";
                szöveg += $" '{Adat.Hrazonosítóki}', ";
                szöveg += $" '{Adat.Kilépésdátum:yyyy.MM.dd}', ";
                szöveg += $" {Adat.Bérki}, ";
                szöveg += $" '{Adat.Telephelyki}', ";
                szöveg += $" '{Adat.Státusváltozások}', ";
                szöveg += $" '{Adat.Névbe}', ";
                szöveg += $" '{Adat.Hrazonosítóbe}', ";
                szöveg += $" '{Adat.Honnanjött}', ";
                szöveg += $" '{Adat.Belépésidátum:yyyy.MM.dd}')";
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
                List<Adat_Dolgozó_Státus> Adatok = Lista_Adatok(hely);
                if (Adatok != null) Válasz = Adatok.Max(a => a.ID) + 1;
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
