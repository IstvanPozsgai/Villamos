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
    public class Kezelő_Dolgozó_Alap
    {
        readonly string jelszó = "forgalmiutasítás";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Dolgozók.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Dolgozói_Adatok(hely.KönyvSzerk());
        }

        public List<Adat_Dolgozó_Alap> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            List<Adat_Dolgozó_Alap> Adatok = new List<Adat_Dolgozó_Alap>();
            Adat_Dolgozó_Alap Adat;
            string szöveg = "SELECT * FROM Dolgozóadatok order by DolgozóNév asc";

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
                                Adat = new Adat_Dolgozó_Alap(
                                          rekord["Sorszám"].ToÉrt_Long(),
                                          rekord["DolgozóNév"].ToStrTrim(),
                                          rekord["Dolgozószám"].ToStrTrim(),
                                          rekord["Leánykori"].ToStrTrim(),
                                          rekord["Anyja"].ToStrTrim(),
                                          rekord["Születésiidő"].ToÉrt_DaTeTime(),
                                          rekord["Születésihely"].ToStrTrim(),
                                          rekord["TAj"].ToStrTrim(),
                                          rekord["ADÓ"].ToStrTrim(),
                                          rekord["Belépésiidő"].ToÉrt_DaTeTime(),
                                          rekord["Lakcím"].ToStrTrim(),
                                          rekord["Ideiglenescím"].ToStrTrim(),
                                          rekord["Telefonszám1"].ToStrTrim(),
                                          rekord["telefonszám2"].ToStrTrim(),
                                          rekord["telefonszám3"].ToStrTrim(),
                                          rekord["Munkakör"].ToStrTrim(),
                                          rekord["Csopvez"].ToÉrt_Bool(),
                                          rekord["Csoport"].ToStrTrim(),
                                          rekord["Munkarend"].ToÉrt_Bool(),
                                          rekord["Orvosiérvényesség"].ToÉrt_DaTeTime(),
                                          rekord["Orvosivizsgálat"].ToÉrt_DaTeTime(),
                                          rekord["Targoncaérvényesség"].ToÉrt_DaTeTime(),
                                          rekord["Emelőérvényesség"].ToÉrt_DaTeTime(),
                                          rekord["Kilépésiidő"].ToÉrt_DaTeTime(),
                                          rekord["emelőgépigazolvány"].ToStrTrim(),
                                          rekord["nehézgépkezelőigazolvány"].ToStrTrim(),
                                          rekord["targoncaigazolvány"].ToStrTrim(),
                                          rekord["képernyősidő"].ToÉrt_DaTeTime(),
                                          rekord["nehézgépidő"].ToÉrt_DaTeTime(),
                                          rekord["feorsz"].ToStrTrim(),
                                          rekord["jogosítványszám"].ToStrTrim(),
                                          rekord["Jogosítványérvényesség"].ToÉrt_DaTeTime(),
                                          rekord["jogtanúsítvány"].ToStrTrim(),
                                          rekord["jogorvosi"].ToÉrt_DaTeTime(),
                                          rekord["tűzvizsgaideje"].ToÉrt_DaTeTime(),
                                          rekord["tűzvizsgaérv"].ToÉrt_DaTeTime(),
                                          rekord["passzív"].ToÉrt_Bool(),
                                          rekord["jogosítványkategória"].ToStrTrim(),
                                          rekord["Bejelentkezésinév"].ToStrTrim(),
                                          rekord["főkönyvtitulus"].ToStrTrim(),
                                          rekord["vezényelt"].ToÉrt_Bool(),
                                          rekord["vezényelve"].ToÉrt_Bool(),
                                          rekord["részmunkaidős"].ToÉrt_Bool(),
                                          rekord["alkalmazott"].ToÉrt_Bool(),
                                          rekord["csoportkód"].ToStrTrim(),
                                          rekord["túlóraeng"].ToÉrt_Bool(),
                                          rekord["részmunkaidőperc"].ToÉrt_Int()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Dolgozó_Alap Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "INSERT INTO dolgozóadatok ";
                szöveg += " ( dolgozónév, dolgozószám, kilépésiidő, belépésiidő)";
                szöveg += " VALUES (";
                szöveg += $"'{Adat.DolgozóNév}', ";
                szöveg += $"'{Adat.Dolgozószám}', ";
                szöveg += $"'{Adat.Kilépésiidő:yyyy.MM.dd}', ";
                szöveg += $"'{Adat.Belépésiidő:yyyy.MM.dd}' )";
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

        public void Módosít_Csoport(string Telephely, Adat_Dolgozó_Alap Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE Dolgozóadatok SET csoport='Nincs' WHERE dolgozószám='{Adat.Dolgozószám}'";
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

        public void Módosít_Kilép(string Telephely, Adat_Dolgozó_Alap Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE  dolgozóadatok SET ";
                szöveg += $" kilépésiidő ='{Adat.Kilépésiidő:yyyy.MM.dd}' ";
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

        public void Módosít_Telep(string Telephely, Adat_Dolgozó_Alap Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE  dolgozóadatok SET ";
                szöveg += $" kilépésiidő='{Adat.Kilépésiidő:yyyy.MM.dd}', ";
                szöveg += $" belépésiidő='{Adat.Belépésiidő:yyyy.MM.dd}', ";
                szöveg += $" lakcím='{Adat.Lakcím}', ";
                szöveg += $" dolgozónév='{Adat.DolgozóNév}' ";
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

        public void Rögzítés_Telep(string Telephely, Adat_Dolgozó_Alap Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "INSERT INTO dolgozóadatok ";
                szöveg += " ( dolgozónév, dolgozószám, kilépésiidő, belépésiidő, lakcím)";
                szöveg += " VALUES (";
                szöveg += $"'{Adat.DolgozóNév}', ";
                szöveg += $"'{Adat.Dolgozószám}', ";
                szöveg += $"'{Adat.Kilépésiidő:yyyy.MM.dd}', ";
                szöveg += $"'{Adat.Belépésiidő:yyyy.MM.dd}',";
                szöveg += $"'{Adat.Lakcím}' )";
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

        public void Módosít_Vezénylés(string Telephely, Adat_Dolgozó_Alap Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE  dolgozóadatok SET ";
                szöveg += $" kilépésiidő='{Adat.Kilépésiidő:yyyy.MM.dd}', ";
                szöveg += $" lakcím='{Adat.Lakcím}', ";
                szöveg += $" Vezényelt={Adat.Vezényelt}, ";
                szöveg += $" Vezényelve={Adat.Vezényelve} ";
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

        public void Módosít_Vezénylés_Saját(string Telephely, Adat_Dolgozó_Alap Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE  dolgozóadatok SET ";
                szöveg += $" lakcím='{Adat.Lakcím}', ";
                szöveg += $" Vezényelt={Adat.Vezényelt}, ";
                szöveg += $" Vezényelve={Adat.Vezényelve} ";
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

        public void Rögzítés_Vezénylés(string Telephely, Adat_Dolgozó_Alap Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "INSERT INTO dolgozóadatok ";
                szöveg += "(dolgozószám, dolgozónév, Vezényelt, Vezényelve, lakcím, kilépésiidő)";
                szöveg += " VALUES (";
                szöveg += $"'{Adat.Dolgozószám}', ";
                szöveg += $"'{Adat.DolgozóNév}', ";
                szöveg += $"{Adat.Vezényelt}, {Adat.Vezényelve}, ";
                szöveg += $"'{Adat.Lakcím}', ";
                szöveg += $"'{Adat.Kilépésiidő:yyyy.MM.dd}') ";
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

        public void Módosít_Alap(string Telephely, Adat_Dolgozó_Alap Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE Dolgozóadatok SET ";
                szöveg += $" csoport='{Adat.Csoport}', ";
                szöveg += $" Főkönyvtitulus='{Adat.Főkönyvtitulus}', ";
                szöveg += $" bejelentkezésinév='{Adat.Bejelentkezésinév}', ";
                szöveg += $" munkarend={Adat.Munkarend}, ";
                szöveg += $" Csopvez={Adat.Csopvez}, ";
                szöveg += $" Passzív={Adat.Passzív}, ";
                szöveg += $" Részmunkaidős={Adat.Részmunkaidős}, ";
                szöveg += $" alkalmazott={Adat.Alkalmazott}, ";
                szöveg += $" TAJ='{Adat.TAj}', ";
                szöveg += $" csoportkód='{Adat.Csoportkód}', ";
                szöveg += $" részmunkaidőperc={Adat.Részmunkaidőperc} ";
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

        public void Módosít_Túl(string Telephely, Adat_Dolgozó_Alap Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE Dolgozóadatok SET túlóraeng={Adat.Túlóraeng} WHERE dolgozószám='{Adat.Dolgozószám}'";
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

        public void Módosít_Munka(string Telephely, Adat_Dolgozó_Alap Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE Dolgozóadatok SET ";
                szöveg += $" feorsz='{Adat.Feorsz}', ";
                szöveg += $" munkakör='{Adat.Munkakör}' ";
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

        public void Módosít_Jog(string Telephely, Adat_Dolgozó_Alap Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE Dolgozóadatok SET ";
                szöveg += $" Jogosítványszám='{Adat.Jogosítványszám}', ";
                szöveg += $" Jogtanúsítvány='{Adat.Jogtanúsítvány}', ";
                szöveg += $" jogosítványérvényesség='{Adat.Jogosítványérvényesség:yyyy.MM.dd}', ";
                szöveg += $" jogorvosi='{Adat.Jogorvosi:yyyy.MM.dd}' ";
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

        public void Módosít_Csoport(string Telephely, List<string> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> SzövegGy = new List<string>();
                foreach (string elem in Adatok)
                {
                    string szöveg = $"UPDATE Dolgozóadatok SET csoport='Nincs' WHERE dolgozószám='{elem}'";
                    SzövegGy.Add(szöveg);
                }

                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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

        public void Módosít_Vissza(string Telephely, Adat_Dolgozó_Alap Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE  dolgozóadatok SET ";
                szöveg += $" kilépésiidő ='{Adat.Kilépésiidő}', ";
                szöveg += $" dolgozónév='{Adat.DolgozóNév}'";
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

        public void Módosít_Ki(string Telephely, Adat_Dolgozó_Alap Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE  dolgozóadatok SET ";
                szöveg += $" kilépésiidő='{Adat.Kilépésiidő:yyyy.MM.dd}', ";
                szöveg += $" lakcím='{Adat.Lakcím}' ";
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




        public List<Adat_Dolgozó_Alap> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Dolgozó_Alap> Adatok = new List<Adat_Dolgozó_Alap>();
            Adat_Dolgozó_Alap Adat;

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
                                Adat = new Adat_Dolgozó_Alap(
                                          rekord["Sorszám"].ToÉrt_Long(),
                                          rekord["DolgozóNév"].ToStrTrim(),
                                          rekord["Dolgozószám"].ToStrTrim(),
                                          rekord["Leánykori"].ToStrTrim(),
                                          rekord["Anyja"].ToStrTrim(),
                                          rekord["Születésiidő"].ToÉrt_DaTeTime(),
                                          rekord["Születésihely"].ToStrTrim(),
                                          rekord["TAj"].ToStrTrim(),
                                          rekord["ADÓ"].ToStrTrim(),
                                          rekord["Belépésiidő"].ToÉrt_DaTeTime(),
                                          rekord["Lakcím"].ToStrTrim(),
                                          rekord["Ideiglenescím"].ToStrTrim(),
                                          rekord["Telefonszám1"].ToStrTrim(),
                                          rekord["telefonszám2"].ToStrTrim(),
                                          rekord["telefonszám3"].ToStrTrim(),
                                          rekord["Munkakör"].ToStrTrim(),
                                          rekord["Csopvez"].ToÉrt_Bool(),
                                          rekord["Csoport"].ToStrTrim(),
                                          rekord["Munkarend"].ToÉrt_Bool(),
                                          rekord["Orvosiérvényesség"].ToÉrt_DaTeTime(),
                                          rekord["Orvosivizsgálat"].ToÉrt_DaTeTime(),
                                          rekord["Targoncaérvényesség"].ToÉrt_DaTeTime(),
                                          rekord["Emelőérvényesség"].ToÉrt_DaTeTime(),
                                          rekord["Kilépésiidő"].ToÉrt_DaTeTime(),
                                          rekord["emelőgépigazolvány"].ToStrTrim(),
                                          rekord["nehézgépkezelőigazolvány"].ToStrTrim(),
                                          rekord["targoncaigazolvány"].ToStrTrim(),
                                          rekord["képernyősidő"].ToÉrt_DaTeTime(),
                                          rekord["nehézgépidő"].ToÉrt_DaTeTime(),
                                          rekord["feorsz"].ToStrTrim(),
                                          rekord["jogosítványszám"].ToStrTrim(),
                                          rekord["Jogosítványérvényesség"].ToÉrt_DaTeTime(),
                                          rekord["jogtanúsítvány"].ToStrTrim(),
                                          rekord["jogorvosi"].ToÉrt_DaTeTime(),
                                          rekord["tűzvizsgaideje"].ToÉrt_DaTeTime(),
                                          rekord["tűzvizsgaérv"].ToÉrt_DaTeTime(),
                                          rekord["passzív"].ToÉrt_Bool(),
                                          rekord["jogosítványkategória"].ToStrTrim(),
                                          rekord["Bejelentkezésinév"].ToStrTrim(),
                                          rekord["főkönyvtitulus"].ToStrTrim(),
                                          rekord["vezényelt"].ToÉrt_Bool(),
                                          rekord["vezényelve"].ToÉrt_Bool(),
                                          rekord["részmunkaidős"].ToÉrt_Bool(),
                                          rekord["alkalmazott"].ToÉrt_Bool(),
                                          rekord["csoportkód"].ToStrTrim(),
                                          rekord["túlóraeng"].ToÉrt_Bool(),
                                          rekord["részmunkaidőperc"].ToÉrt_Int()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Dolgozó_Alap Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Dolgozó_Alap Adat = null;

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
                            rekord.Read();


                            Adat = new Adat_Dolgozó_Alap(
                                      rekord["Sorszám"].ToÉrt_Long(),
                                      rekord["DolgozóNév"].ToStrTrim(),
                                      rekord["Dolgozószám"].ToStrTrim(),
                                      rekord["Leánykori"].ToStrTrim(),
                                      rekord["Anyja"].ToStrTrim(),
                                      rekord["Születésiidő"].ToÉrt_DaTeTime(),
                                      rekord["Születésihely"].ToStrTrim(),
                                      rekord["TAj"].ToStrTrim(),
                                      rekord["ADÓ"].ToStrTrim(),
                                      rekord["Belépésiidő"].ToÉrt_DaTeTime(),
                                      rekord["Lakcím"].ToStrTrim(),
                                      rekord["Ideiglenescím"].ToStrTrim(),
                                      rekord["Telefonszám1"].ToStrTrim(),
                                      rekord["telefonszám2"].ToStrTrim(),
                                      rekord["telefonszám3"].ToStrTrim(),
                                      rekord["Munkakör"].ToStrTrim(),
                                      rekord["Csopvez"].ToÉrt_Bool(),
                                      rekord["Csoport"].ToStrTrim(),
                                      rekord["Munkarend"].ToÉrt_Bool(),
                                      rekord["Orvosiérvényesség"].ToÉrt_DaTeTime(),
                                      rekord["Orvosivizsgálat"].ToÉrt_DaTeTime(),
                                      rekord["Targoncaérvényesség"].ToÉrt_DaTeTime(),
                                      rekord["Emelőérvényesség"].ToÉrt_DaTeTime(),
                                      rekord["Kilépésiidő"].ToÉrt_DaTeTime(),
                                      rekord["emelőgépigazolvány"].ToStrTrim(),
                                      rekord["nehézgépkezelőigazolvány"].ToStrTrim(),
                                      rekord["targoncaigazolvány"].ToStrTrim(),
                                      rekord["képernyősidő"].ToÉrt_DaTeTime(),
                                      rekord["nehézgépidő"].ToÉrt_DaTeTime(),
                                      rekord["feorsz"].ToStrTrim(),
                                      rekord["jogosítványszám"].ToStrTrim(),
                                      rekord["Jogosítványérvényesség"].ToÉrt_DaTeTime(),
                                      rekord["jogtanúsítvány"].ToStrTrim(),
                                      rekord["jogorvosi"].ToÉrt_DaTeTime(),
                                      rekord["tűzvizsgaideje"].ToÉrt_DaTeTime(),
                                      rekord["tűzvizsgaérv"].ToÉrt_DaTeTime(),
                                      rekord["passzív"].ToÉrt_Bool(),
                                      rekord["jogosítványkategória"].ToStrTrim(),
                                      rekord["Bejelentkezésinév"].ToStrTrim(),
                                      rekord["főkönyvtitulus"].ToStrTrim(),
                                      rekord["vezényelt"].ToÉrt_Bool(),
                                      rekord["vezényelve"].ToÉrt_Bool(),
                                      rekord["részmunkaidős"].ToÉrt_Bool(),
                                      rekord["alkalmazott"].ToÉrt_Bool(),
                                      rekord["csoportkód"].ToStrTrim(),
                                      rekord["túlóraeng"].ToÉrt_Bool(),
                                      rekord["részmunkaidőperc"].ToÉrt_Int()
                                      );


                        }
                    }
                }
            }
            return Adat;
        }

        public Adat_Dolgozó_Alap Felhasználó(string hely, string jelszó, string NickNév)
        {
            Adat_Dolgozó_Alap Adat = null;
            try
            {

                if (NickNév.Trim() != "")
                {
                    string szöveg = $"SELECT * FROM Dolgozóadatok WHERE Bejelentkezésinév='{NickNév.Trim()}'";

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
                                        Adat = new Adat_Dolgozó_Alap(
                                           rekord["Sorszám"].ToÉrt_Long(),
                                           rekord["DolgozóNév"].ToStrTrim(),
                                           rekord["Dolgozószám"].ToStrTrim(),
                                           rekord["Leánykori"].ToStrTrim(),
                                           rekord["Anyja"].ToStrTrim(),
                                           rekord["Születésiidő"].ToÉrt_DaTeTime(),
                                           rekord["Születésihely"].ToStrTrim(),
                                           rekord["TAj"].ToStrTrim(),
                                           rekord["ADÓ"].ToStrTrim(),
                                           rekord["Belépésiidő"].ToÉrt_DaTeTime(),
                                           rekord["Lakcím"].ToStrTrim(),
                                           rekord["Ideiglenescím"].ToStrTrim(),
                                           rekord["Telefonszám1"].ToStrTrim(),
                                           rekord["telefonszám2"].ToStrTrim(),
                                           rekord["telefonszám3"].ToStrTrim(),
                                           rekord["Munkakör"].ToStrTrim(),
                                           rekord["Csopvez"].ToÉrt_Bool(),
                                           rekord["Csoport"].ToString(),
                                           rekord["Munkarend"].ToÉrt_Bool(),
                                           rekord["Orvosiérvényesség"].ToÉrt_DaTeTime(),
                                           rekord["Orvosivizsgálat"].ToÉrt_DaTeTime(),
                                           rekord["Targoncaérvényesség"].ToÉrt_DaTeTime(),
                                           rekord["Emelőérvényesség"].ToÉrt_DaTeTime(),
                                           rekord["Kilépésiidő"].ToÉrt_DaTeTime(),
                                           rekord["emelőgépigazolvány"].ToStrTrim(),
                                           rekord["nehézgépkezelőigazolvány"].ToStrTrim(),
                                           rekord["targoncaigazolvány"].ToStrTrim(),
                                           rekord["képernyősidő"].ToÉrt_DaTeTime(),
                                           rekord["nehézgépidő"].ToÉrt_DaTeTime(),
                                           rekord["feorsz"].ToStrTrim(),
                                           rekord["jogosítványszám"].ToStrTrim(),
                                           rekord["Jogosítványérvényesség"].ToÉrt_DaTeTime(),
                                           rekord["jogtanúsítvány"].ToStrTrim(),
                                           rekord["jogorvosi"].ToÉrt_DaTeTime(),
                                           rekord["tűzvizsgaideje"].ToÉrt_DaTeTime(),
                                           rekord["tűzvizsgaérv"].ToÉrt_DaTeTime(),
                                           rekord["passzív"].ToÉrt_Bool(),
                                           rekord["jogosítványkategória"].ToStrTrim(),
                                           rekord["Bejelentkezésinév"].ToStrTrim(),
                                           rekord["főkönyvtitulus"].ToStrTrim(),
                                           rekord["vezényelt"].ToÉrt_Bool(),
                                           rekord["vezényelve"].ToÉrt_Bool(),
                                           rekord["részmunkaidős"].ToÉrt_Bool(),
                                           rekord["alkalmazott"].ToÉrt_Bool(),
                                           rekord["csoportkód"].ToStrTrim(),
                                           rekord["túlóraeng"].ToÉrt_Bool(),
                                           rekord["részmunkaidőperc"].ToÉrt_Int()
                                           );
                                    }
                                }
                            }
                        }
                    }
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

            return Adat;
        }

        public static Adat_Dolgozó_Alap NévTörzsDarabol(string szöveg, char elválasztó)
        {
            Adat_Dolgozó_Alap válasz = null;
            string[] Darabol = szöveg.Split(elválasztó);
            if (Darabol.Length == 2)
                válasz = new Adat_Dolgozó_Alap(
                    Darabol[0].Trim(),
                    Darabol[1].Trim()
                    );
            return válasz;
        }
    }




    public class Kezelő_Dolgozó_Beosztás
    {
        public List<Adat_Dolgozó_Beosztás> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Dolgozó_Beosztás> Adatok = new List<Adat_Dolgozó_Beosztás>();
            Adat_Dolgozó_Beosztás Adat;

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

                                Adat = new Adat_Dolgozó_Beosztás(
                                          rekord["Nap"].ToÉrt_Int(),
                                          rekord["Beosztáskód"].ToStrTrim(),
                                          rekord["Ledolgozott"].ToÉrt_Int(),

                                          rekord["Túlóra"].ToÉrt_Int(),
                                          rekord["Túlórakezd"].ToÉrt_DaTeTime(),
                                          rekord["Túlóravég"].ToÉrt_DaTeTime(),

                                          rekord["Csúszóra"].ToÉrt_Int(),
                                          rekord["CSúszórakezd"].ToÉrt_DaTeTime(),
                                          rekord["Csúszóravég"].ToÉrt_DaTeTime(),

                                          rekord["Megjegyzés"].ToStrTrim(),
                                          rekord["Túlóraok"].ToStrTrim(),
                                          rekord["Szabiok"].ToStrTrim(),

                                          rekord["kért"].ToÉrt_Bool(),
                                          rekord["Csúszok"].ToStrTrim(),
                                          rekord["AFTóra"].ToÉrt_Int(),
                                          rekord["AFTok"].ToStrTrim()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Dolgozó_Beosztás Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Dolgozó_Beosztás Adat = null;

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

                                Adat = new Adat_Dolgozó_Beosztás(
                                          rekord["Nap"].ToÉrt_Int(),
                                          rekord["Beosztáskód"].ToStrTrim(),
                                          rekord["Ledolgozott"].ToÉrt_Int(),

                                          rekord["Túlóra"].ToÉrt_Int(),
                                          rekord["Túlórakezd"].ToÉrt_DaTeTime(),
                                          rekord["Túlóravég"].ToÉrt_DaTeTime(),

                                          rekord["Csúszóra"].ToÉrt_Int(),
                                          rekord["CSúszórakezd"].ToÉrt_DaTeTime(),
                                          rekord["Csúszóravég"].ToÉrt_DaTeTime(),

                                          rekord["Megjegyzés"].ToStrTrim(),
                                          rekord["Túlóraok"].ToStrTrim(),
                                          rekord["Szabiok"].ToStrTrim(),

                                          rekord["kért"].ToÉrt_Bool(),
                                          rekord["Csúszok"].ToStrTrim(),
                                          rekord["AFTóra"].ToÉrt_Int(),
                                          rekord["AFTok"].ToStrTrim()
                                          );
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Dolgozó_Beosztás_lista
    {
        public List<string> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<string> Adatok = new List<string>();
            string Adat;

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
                                Adat = rekord["dolgozólista"].ToStrTrim();
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

    public class Kezelő_Dolgozó_Beosztás_Új
    {
        readonly string jelszó = "kiskakas";
        public List<Adat_Dolgozó_Beosztás_Új> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Dolgozó_Beosztás_Új> Adatok = new List<Adat_Dolgozó_Beosztás_Új>();
            Adat_Dolgozó_Beosztás_Új Adat;

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

                                Adat = new Adat_Dolgozó_Beosztás_Új(
                                          rekord["dolgozószám"].ToStrTrim(),
                                          rekord["Nap"].ToÉrt_DaTeTime(),
                                          rekord["Beosztáskód"].ToStrTrim(),
                                          rekord["Ledolgozott"].ToÉrt_Int(),

                                          rekord["Túlóra"].ToÉrt_Int(),
                                          rekord["Túlórakezd"].ToÉrt_DaTeTime(),
                                          rekord["Túlóravég"].ToÉrt_DaTeTime(),

                                          rekord["Csúszóra"].ToÉrt_Int(),
                                          rekord["CSúszórakezd"].ToÉrt_DaTeTime(),
                                          rekord["Csúszóravég"].ToÉrt_DaTeTime(),

                                          rekord["Megjegyzés"].ToStrTrim(),
                                          rekord["Túlóraok"].ToStrTrim(),
                                          rekord["Szabiok"].ToStrTrim(),

                                          rekord["kért"].ToÉrt_Bool(),
                                          rekord["Csúszok"].ToStrTrim(),
                                          rekord["AFTóra"].ToÉrt_Int(),
                                          rekord["AFTok"].ToStrTrim()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Dolgozó_Beosztás_Új> Lista_Adatok(string hely)
        {
            string szöveg = $"SELECT * FROM Beosztás";
            List<Adat_Dolgozó_Beosztás_Új> Adatok = new List<Adat_Dolgozó_Beosztás_Új>();
            Adat_Dolgozó_Beosztás_Új Adat;

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

                                Adat = new Adat_Dolgozó_Beosztás_Új(
                                          rekord["dolgozószám"].ToStrTrim(),
                                          rekord["Nap"].ToÉrt_DaTeTime(),
                                          rekord["Beosztáskód"].ToStrTrim(),
                                          rekord["Ledolgozott"].ToÉrt_Int(),

                                          rekord["Túlóra"].ToÉrt_Int(),
                                          rekord["Túlórakezd"].ToÉrt_DaTeTime(),
                                          rekord["Túlóravég"].ToÉrt_DaTeTime(),

                                          rekord["Csúszóra"].ToÉrt_Int(),
                                          rekord["CSúszórakezd"].ToÉrt_DaTeTime(),
                                          rekord["Csúszóravég"].ToÉrt_DaTeTime(),

                                          rekord["Megjegyzés"].ToStrTrim(),
                                          rekord["Túlóraok"].ToStrTrim(),
                                          rekord["Szabiok"].ToStrTrim(),

                                          rekord["kért"].ToÉrt_Bool(),
                                          rekord["Csúszok"].ToStrTrim(),
                                          rekord["AFTóra"].ToÉrt_Int(),
                                          rekord["AFTok"].ToStrTrim()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Dolgozó_Beosztás_Új Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Dolgozó_Beosztás_Új Adat = null;

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

                                Adat = new Adat_Dolgozó_Beosztás_Új(
                                          rekord["dolgozószám"].ToStrTrim(),
                                          rekord["Nap"].ToÉrt_DaTeTime(),
                                          rekord["Beosztáskód"].ToStrTrim(),
                                          rekord["Ledolgozott"].ToÉrt_Int(),

                                          rekord["Túlóra"].ToÉrt_Int(),
                                          rekord["Túlórakezd"].ToÉrt_DaTeTime(),
                                          rekord["Túlóravég"].ToÉrt_DaTeTime(),

                                          rekord["Csúszóra"].ToÉrt_Int(),
                                          rekord["CSúszórakezd"].ToÉrt_DaTeTime(),
                                          rekord["Csúszóravég"].ToÉrt_DaTeTime(),

                                          rekord["Megjegyzés"].ToStrTrim(),
                                          rekord["Túlóraok"].ToStrTrim(),
                                          rekord["Szabiok"].ToStrTrim(),

                                          rekord["kért"].ToÉrt_Bool(),
                                          rekord["Csúszok"].ToStrTrim(),
                                          rekord["AFTóra"].ToÉrt_Int(),
                                          rekord["AFTok"].ToStrTrim()
                                          );
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }


    public class Kezelő_Dolgozó_Személyes
    {
        readonly string jelszó = "forgalmiutasítás";

        public List<Adat_Dolgozó_Személyes> Lista_Adatok(string hely)
        {
            List<Adat_Dolgozó_Személyes> Adatok = new List<Adat_Dolgozó_Személyes>();
            Adat_Dolgozó_Személyes Adat;
            string szöveg = $"SELECT * FROM személyes";
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
                                Adat = new Adat_Dolgozó_Személyes(
                                          rekord["Anyja"].ToStrTrim(),
                                          rekord["dolgozószám"].ToStrTrim(),
                                          rekord["Ideiglenescím"].ToStrTrim(),
                                          rekord["Lakcím"].ToStrTrim(),
                                          rekord["Leánykori"].ToStrTrim(),
                                          rekord["Születésihely"].ToStrTrim(),
                                          rekord["Születésiidő"].ToÉrt_DaTeTime(),
                                          rekord["Telefonszám1"].ToStrTrim(),
                                          rekord["Telefonszám2"].ToStrTrim(),
                                          rekord["Telefonszám3"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string hely, Adat_Dolgozó_Személyes Adat)
        {
            try
            {
                string szöveg = "UPDATE személyes SET ";
                szöveg += $" Leánykori='{Adat.Leánykori}', ";
                szöveg += $" Anyja='{Adat.Anyja}', ";
                szöveg += $" Születésiidő='{Adat.Születésiidő:yyyy.MM.dd}', ";
                szöveg += $" Születésihely='{Adat.Születésihely}', ";
                szöveg += $" Lakcím='{Adat.Lakcím}', ";
                szöveg += $" Ideiglenescím='{Adat.Ideiglenescím}', ";
                szöveg += $" Telefonszám1='{Adat.Telefonszám1}', ";
                szöveg += $" Telefonszám2='{Adat.Telefonszám2}', ";
                szöveg += $" Telefonszám3='{Adat.Telefonszám3}' ";
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

        public void Rögzítés(string hely, Adat_Dolgozó_Személyes Adat)
        {
            try
            {
                string szöveg = "INSERT INTO személyes (dolgozószám, leánykori, anyja, születésiidő, születésihely, lakcím, ideiglenescím, telefonszám1, telefonszám2, telefonszám3 )";
                szöveg += " VALUES ";
                szöveg += $"('{Adat.Dolgozószám}', ";
                szöveg += $"'{Adat.Leánykori}', ";
                szöveg += $"'{Adat.Anyja}', ";
                szöveg += $"'{Adat.Születésiidő:yyyy.MM.dd}', ";
                szöveg += $"'{Adat.Születésihely}', ";
                szöveg += $"'{Adat.Lakcím}', ";
                szöveg += $"'{Adat.Ideiglenescím}', ";
                szöveg += $"'{Adat.Telefonszám1}', ";
                szöveg += $"'{Adat.Telefonszám2}', ";
                szöveg += $"'{Adat.Telefonszám3}')";
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


    public class Kezelő_Szatube_Szabadság
    {
        public List<Adat_Szatube_Szabadság> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Szatube_Szabadság> Adatok = new List<Adat_Szatube_Szabadság>();
            Adat_Szatube_Szabadság Adat;

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

                                Adat = new Adat_Szatube_Szabadság(
                                          rekord["sorszám"].ToÉrt_Double(),
                                          rekord["Törzsszám"].ToStrTrim(),
                                          rekord["Dolgozónév"].ToStrTrim(),
                                          rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                          rekord["Befejeződátum"].ToÉrt_DaTeTime(),
                                          rekord["Kivettnap"].ToÉrt_Int(),
                                          rekord["Szabiok"].ToStrTrim(),
                                          rekord["Státus"].ToÉrt_Int(),
                                          rekord["Rögzítette"].ToStrTrim(),
                                          rekord["rögzítésdátum"].ToÉrt_DaTeTime()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public Adat_Szatube_Szabadság Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Szatube_Szabadság Adat = null;

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
                            rekord.Read();

                            Adat = new Adat_Szatube_Szabadság(
                                      rekord["sorszám"].ToÉrt_Double(),
                                      rekord["Törzsszám"].ToStrTrim(),
                                      rekord["Dolgozónév"].ToStrTrim(),
                                      rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                      rekord["Befejeződátum"].ToÉrt_DaTeTime(),
                                      rekord["Kivettnap"].ToÉrt_Int(),
                                      rekord["Szabiok"].ToStrTrim(),
                                      rekord["Státus"].ToÉrt_Int(),
                                      rekord["Rögzítette"].ToStrTrim(),
                                      rekord["rögzítésdátum"].ToÉrt_DaTeTime()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }

    }


    public class Kezelő_Szatube_Csúsztatás
    {
        public List<Adat_Szatube_Csúsztatás> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Szatube_Csúsztatás> Adatok = new List<Adat_Szatube_Csúsztatás>();
            Adat_Szatube_Csúsztatás Adat;

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

                                Adat = new Adat_Szatube_Csúsztatás(
                                     rekord["sorszám"].ToÉrt_Double(),
                                     rekord["Törzsszám"].ToStrTrim(),
                                     rekord["Dolgozónév"].ToStrTrim(),
                                     rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                     rekord["Befejeződátum"].ToÉrt_DaTeTime(),
                                     rekord["Kivettnap"].ToÉrt_Int(),
                                     rekord["Szabiok"].ToStrTrim(),
                                     rekord["Státus"].ToÉrt_Int(),
                                     rekord["Rögzítette"].ToStrTrim(),
                                     rekord["rögzítésdátum"].ToÉrt_DaTeTime(),
                                     rekord["Kezdőidő"].ToÉrt_DaTeTime(),
                                     rekord["Befejezőidő"].ToÉrt_DaTeTime()
                                     );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public Adat_Szatube_Csúsztatás Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Szatube_Csúsztatás Adat = null;

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
                            rekord.Read();
                            Adat = new Adat_Szatube_Csúsztatás(
                                      rekord["sorszám"].ToÉrt_Double(),
                                      rekord["Törzsszám"].ToStrTrim(),
                                      rekord["Dolgozónév"].ToStrTrim(),
                                      rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                      rekord["Befejeződátum"].ToÉrt_DaTeTime(),
                                      rekord["Kivettnap"].ToÉrt_Int(),
                                      rekord["Szabiok"].ToStrTrim(),
                                      rekord["Státus"].ToÉrt_Int(),
                                      rekord["Rögzítette"].ToStrTrim(),
                                      rekord["rögzítésdátum"].ToÉrt_DaTeTime(),
                                      rekord["Kezdőidő"].ToÉrt_DaTeTime(),
                                      rekord["Befejezőidő"].ToÉrt_DaTeTime()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Szatube_Aft
    {
        public List<Adat_Szatube_AFT> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Szatube_AFT> Adatok = new List<Adat_Szatube_AFT>();
            Adat_Szatube_AFT Adat;

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

                                Adat = new Adat_Szatube_AFT(
                                          rekord["sorszám"].ToÉrt_Double(),
                                          rekord["Törzsszám"].ToStrTrim(),
                                          rekord["Dolgozónév"].ToStrTrim(),
                                          rekord["Dátum"].ToÉrt_DaTeTime(),
                                          rekord["Aftóra"].ToÉrt_Int(),
                                          rekord["Aftok"].ToStrTrim(),
                                          rekord["Státus"].ToÉrt_Int(),
                                          rekord["Rögzítette"].ToStrTrim(),
                                          rekord["rögzítésdátum"].ToÉrt_DaTeTime()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Szatube_AFT Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Szatube_AFT Adat = null;

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
                            rekord.Read();

                            Adat = new Adat_Szatube_AFT(
                                      rekord["sorszám"].ToÉrt_Double(),
                                      rekord["Törzsszám"].ToStrTrim(),
                                      rekord["Dolgozónév"].ToStrTrim(),
                                      rekord["Dátum"].ToÉrt_DaTeTime(),
                                      rekord["Aftóra"].ToÉrt_Int(),
                                      rekord["Aftok"].ToStrTrim(),
                                      rekord["Státus"].ToÉrt_Int(),
                                      rekord["Rögzítette"].ToStrTrim(),
                                      rekord["rögzítésdátum"].ToÉrt_DaTeTime()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }
    }

    public class Kezelő_Szatube_Túlóra
    {

        public List<Adat_Szatube_Túlóra> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Szatube_Túlóra> Adatok = new List<Adat_Szatube_Túlóra>();
            Adat_Szatube_Túlóra Adat;

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

                                Adat = new Adat_Szatube_Túlóra(
                                          rekord["sorszám"].ToÉrt_Double(),
                                          rekord["Törzsszám"].ToStrTrim(),
                                          rekord["Dolgozónév"].ToStrTrim(),
                                          rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                          rekord["Befejeződátum"].ToÉrt_DaTeTime(),
                                          rekord["Kivettnap"].ToÉrt_Int(),
                                          rekord["Szabiok"].ToStrTrim(),
                                          rekord["Státus"].ToÉrt_Int(),
                                          rekord["Rögzítette"].ToStrTrim(),
                                          rekord["rögzítésdátum"].ToÉrt_DaTeTime(),
                                          rekord["Kezdőidő"].ToÉrt_DaTeTime(),
                                          rekord["Befejezőidő"].ToÉrt_DaTeTime()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public Adat_Szatube_Túlóra Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Szatube_Túlóra Adat = null;

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
                            rekord.Read();

                            Adat = new Adat_Szatube_Túlóra(
                                      rekord["sorszám"].ToÉrt_Double(),
                                      rekord["Törzsszám"].ToStrTrim(),
                                      rekord["Dolgozónév"].ToStrTrim(),
                                      rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                      rekord["Befejeződátum"].ToÉrt_DaTeTime(),
                                      rekord["Kivettnap"].ToÉrt_Int(),
                                      rekord["Szabiok"].ToStrTrim(),
                                      rekord["Státus"].ToÉrt_Int(),
                                      rekord["Rögzítette"].ToStrTrim(),
                                      rekord["rögzítésdátum"].ToÉrt_DaTeTime(),
                                      rekord["Kezdőidő"].ToÉrt_DaTeTime(),
                                      rekord["Befejezőidő"].ToÉrt_DaTeTime()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }

    }

    public class Kezelő_Szatube_Beteg
    {
        public List<Adat_Szatube_Beteg> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Szatube_Beteg> Adatok = new List<Adat_Szatube_Beteg>();
            Adat_Szatube_Beteg Adat;

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

                                Adat = new Adat_Szatube_Beteg(
                                          rekord["sorszám"].ToÉrt_Double(),
                                          rekord["Törzsszám"].ToStrTrim(),
                                          rekord["Dolgozónév"].ToStrTrim(),
                                          rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                          rekord["Befejeződátum"].ToÉrt_DaTeTime(),
                                          rekord["Kivettnap"].ToÉrt_Int(),
                                          rekord["Szabiok"].ToStrTrim(),
                                          rekord["Státus"].ToÉrt_Int(),
                                          rekord["Rögzítette"].ToStrTrim(),
                                          rekord["rögzítésdátum"].ToÉrt_DaTeTime()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public Adat_Szatube_Beteg Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Szatube_Beteg Adat = null;

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
                            rekord.Read();

                            Adat = new Adat_Szatube_Beteg(
                                      rekord["sorszám"].ToÉrt_Double(),
                                      rekord["Törzsszám"].ToStrTrim(),
                                      rekord["Dolgozónév"].ToStrTrim(),
                                      rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                      rekord["Befejeződátum"].ToÉrt_DaTeTime(),
                                      rekord["Kivettnap"].ToÉrt_Int(),
                                      rekord["Szabiok"].ToStrTrim(),
                                      rekord["Státus"].ToÉrt_Int(),
                                      rekord["Rögzítette"].ToStrTrim(),
                                      rekord["rögzítésdátum"].ToÉrt_DaTeTime()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }

    }

    public class Kezelő_Dolgozó_Beosztás_Napló
    {

        public List<Adat_Dolgozó_Beosztás_Napló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Dolgozó_Beosztás_Napló> Adatok = new List<Adat_Dolgozó_Beosztás_Napló>();
            Adat_Dolgozó_Beosztás_Napló Adat;

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

                                Adat = new Adat_Dolgozó_Beosztás_Napló(
                                          rekord["Sorszám"].ToÉrt_Double(),
                                          rekord["Dátum"].ToÉrt_DaTeTime(),
                                          rekord["Beosztáskód"].ToStrTrim(),
                                          rekord["Túlóra"].ToÉrt_Int(),
                                          rekord["Túlórakezd"].ToÉrt_DaTeTime(),
                                          rekord["Túlóravég"].ToÉrt_DaTeTime(),
                                          rekord["Csúszóra"].ToÉrt_Int(),
                                          rekord["CSúszórakezd"].ToÉrt_DaTeTime(),
                                          rekord["Csúszóravég"].ToÉrt_DaTeTime(),
                                          rekord["Megjegyzés"].ToStrTrim(),
                                          rekord["Túlóraok"].ToStrTrim(),
                                          rekord["Szabiok"].ToStrTrim(),
                                          rekord["kért"].ToÉrt_Bool(),
                                          rekord["Csúszok"].ToStrTrim(),
                                          rekord["Rögzítette"].ToStrTrim(),
                                          rekord["Rögzítésdátum"].ToÉrt_DaTeTime(),
                                          rekord["dolgozónév"].ToStrTrim(),
                                          rekord["Törzsszám"].ToStrTrim(),
                                          rekord["AFTóra"].ToÉrt_Int(),
                                          rekord["AFTok"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Dolgozó_Beosztás_Napló Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Dolgozó_Beosztás_Napló Adat = null;

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
                            rekord.Read();

                            Adat = new Adat_Dolgozó_Beosztás_Napló(
                                      rekord["Sorszám"].ToÉrt_Double(),
                                      rekord["Dátum"].ToÉrt_DaTeTime(),
                                      rekord["Beosztáskód"].ToStrTrim(),
                                      rekord["Túlóra"].ToÉrt_Int(),
                                      rekord["Túlórakezd"].ToÉrt_DaTeTime(),
                                      rekord["Túlóravég"].ToÉrt_DaTeTime(),
                                      rekord["Csúszóra"].ToÉrt_Int(),
                                      rekord["CSúszórakezd"].ToÉrt_DaTeTime(),
                                      rekord["Csúszóravég"].ToÉrt_DaTeTime(),
                                      rekord["Megjegyzés"].ToStrTrim(),
                                      rekord["Túlóraok"].ToStrTrim(),
                                      rekord["Szabiok"].ToStrTrim(),
                                      rekord["kért"].ToÉrt_Bool(),
                                      rekord["Csúszok"].ToStrTrim(),
                                      rekord["Rögzítette"].ToStrTrim(),
                                      rekord["Rögzítésdátum"].ToÉrt_DaTeTime(),
                                      rekord["dolgozónév"].ToStrTrim(),
                                      rekord["Törzsszám"].ToStrTrim(),
                                      rekord["AFTóra"].ToÉrt_Int(),
                                      rekord["AFTok"].ToStrTrim()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }


    }

}
