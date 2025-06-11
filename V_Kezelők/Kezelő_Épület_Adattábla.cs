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
    public class Kezelő_Épület_Adattábla
    {
        readonly string jelszó = "seprűéslapát";
        readonly string táblanév = "Adattábla";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Épület\épülettörzs.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Épülettakarításlétrehozás(hely);
        }

        public List<Adat_Épület_Adattábla> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            List<Adat_Épület_Adattábla> Adatok = new List<Adat_Épület_Adattábla>();
            Adat_Épület_Adattábla Adat;
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
                                Adat = new Adat_Épület_Adattábla(
                                          rekord["ID"].ToÉrt_Int(),
                                          rekord["Megnevezés"].ToStrTrim(),
                                          rekord["Osztály"].ToStrTrim(),
                                          rekord["Méret"].ToÉrt_Double(),
                                          rekord["Helységkód"].ToStrTrim(),
                                          rekord["Státus"].ToÉrt_Bool(),
                                          rekord["E1évdb"].ToÉrt_Int(),
                                          rekord["E2évdb"].ToÉrt_Int(),
                                          rekord["E3évdb"].ToÉrt_Int(),
                                          rekord["Kezd"].ToStrTrim(),
                                          rekord["Végez"].ToStrTrim(),
                                          rekord["Ellenőremail"].ToStrTrim(),
                                          rekord["Ellenőrneve"].ToStrTrim(),
                                          rekord["Ellenőrtelefonszám"].ToStrTrim(),
                                          rekord["Szemetes"].ToÉrt_Bool(),
                                          rekord["Kapcsolthelység"].ToStrTrim()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string Telephely, Adat_Épület_Adattábla Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"megnevezés='{Adat.Megnevezés}', ";
                szöveg += $"Osztály='{Adat.Osztály}', ";
                szöveg += $"Méret={Adat.Méret}, ";
                szöveg += $"helységkód='E{Adat.Helységkód}', ";
                szöveg += $"E1évdb='{Adat.E1évdb}', ";
                szöveg += $"E2évdb='{Adat.E2évdb}', ";
                szöveg += $"E3évdb='{Adat.E3évdb}', ";
                szöveg += $"kezd='{Adat.Kezd}', ";
                szöveg += $"végez='{Adat.Végez}', ";
                szöveg += $"ellenőremail='{Adat.Ellenőremail}', ";
                szöveg += $"ellenőrneve='{Adat.Ellenőrneve}', ";
                szöveg += $"ellenőrtelefonszám='{Adat.Ellenőrtelefonszám}', ";
                szöveg += $" szemetes={Adat.Szemetes}, ";
                szöveg += $"kapcsolthelység='{Adat.Kapcsolthelység}'";
                szöveg += $" WHERE id ={Adat.ID}";
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

        public void Módosítás(string Telephely, int id)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $" státus=true ";
                szöveg += $" WHERE id ={id}";
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

        public void Módosítás(string Telephely, int első, int második)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $" id={első} ";
                szöveg += $" WHERE id ={második}";
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

        public void Rögzítés(string Telephely, Adat_Épület_Adattábla Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"INSERT INTO {táblanév} (id, Megnevezés, Osztály, Méret, helységkód, státus, E1évdb, E2évdb, E3évdb," +
                        " kezd, végez, ellenőremail, ellenőrneve, ellenőrtelefonszám, szemetes, kapcsolthelység ) VALUES (";
                szöveg += $"{Sorszám(Telephely)}, ";
                szöveg += $"'{Adat.Megnevezés}', ";
                szöveg += $"'{Adat.Osztály}', ";
                szöveg += Adat.Méret.ToString().Replace(',', '.') + ", ";
                szöveg += $"'E{Adat.Helységkód}', ";
                szöveg += "false, ";
                szöveg += $"{Adat.E1évdb}, ";
                szöveg += $"{Adat.E2évdb}, ";
                szöveg += $"{Adat.E3évdb}, ";
                szöveg += $"'{Adat.Kezd}', ";
                szöveg += $"'{Adat.Végez}', ";
                szöveg += $"'{Adat.Ellenőremail}', ";
                szöveg += $"'{Adat.Ellenőrneve}', ";
                szöveg += $"'{Adat.Ellenőrtelefonszám}', ";
                szöveg += $" {Adat.Szemetes}, ";
                szöveg += $"'{Adat.Kapcsolthelység}')";
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

        private int Sorszám(string Telephely)
        {
            int válasz = 1;
            try
            {
                List<Adat_Épület_Adattábla> Adatok = Lista_Adatok(Telephely);
                if (Adatok != null) válasz = Adatok.Max(a => a.ID) + 1;
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
            return válasz;
        }

        public void Csere(string Telephely, int Id)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<Adat_Épület_Adattábla> Adatok = Lista_Adatok(Telephely).OrderBy(a => a.ID).ToList();
                Adat_Épület_Adattábla Adat = Adatok.FirstOrDefault(a => a.ID == Id);
                Adat_Épület_Adattábla Előző = Adatok.LastOrDefault(a => a.ID < Id);
                Módosítás(Telephely, Előző.ID, 0);
                Módosítás(Telephely, Adat.ID, Előző.ID);
                Módosítás(Telephely, 0, Adat.ID);
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


        //elkopó
        public List<Adat_Épület_Adattábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Épület_Adattábla> Adatok = new List<Adat_Épület_Adattábla>();
            Adat_Épület_Adattábla Adat;

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
                                Adat = new Adat_Épület_Adattábla(
                                          rekord["ID"].ToÉrt_Int(),
                                          rekord["Megnevezés"].ToStrTrim(),
                                          rekord["Osztály"].ToStrTrim(),
                                          rekord["Méret"].ToÉrt_Double(),
                                          rekord["Helységkód"].ToStrTrim(),
                                          rekord["Státus"].ToÉrt_Bool(),
                                          rekord["E1évdb"].ToÉrt_Int(),
                                          rekord["E2évdb"].ToÉrt_Int(),
                                          rekord["E3évdb"].ToÉrt_Int(),
                                          rekord["Kezd"].ToStrTrim(),
                                          rekord["Végez"].ToStrTrim(),
                                          rekord["Ellenőremail"].ToStrTrim(),
                                          rekord["Ellenőrneve"].ToStrTrim(),
                                          rekord["Ellenőrtelefonszám"].ToStrTrim(),
                                          rekord["Szemetes"].ToÉrt_Bool(),
                                          rekord["Kapcsolthelység"].ToStrTrim()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

    }
}
