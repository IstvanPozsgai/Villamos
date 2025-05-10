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
    public class Kezelő_Dolgozó_Alap
    {
        readonly string jelszó = "forgalmiutasítás";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Dolgozók.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Dolgozói_Adatok(hely.KönyvSzerk());
        }

        public List<Adat_Dolgozó_Alap> Lista_Adatok(string Telephely, bool Aktív = false)
        {
            FájlBeállítás(Telephely);
            List<Adat_Dolgozó_Alap> Adatok = new List<Adat_Dolgozó_Alap>();
            Adat_Dolgozó_Alap Adat;
            string szöveg;
            if (Aktív)
                szöveg = "SELECT * FROM Dolgozóadatok WHERE Kilépésiidő=#1/1/1900# order by DolgozóNév ";
            else
                szöveg = "SELECT * FROM Dolgozóadatok order by DolgozóNév ";

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

        public List<Adat_Dolgozó_Alap> MunkaVégzőLista(string Telephely, DateTime Dátum, List<Adat_Dolgozó_Alap> AdatokNévsor)
        {
            List<Adat_Dolgozó_Alap> Válasz = new List<Adat_Dolgozó_Alap>();
            Kezelő_Kiegészítő_Beosztáskódok KézBeoKód = new Kezelő_Kiegészítő_Beosztáskódok();
            Kezelő_Dolgozó_Beosztás_Új KézBeosztás = new Kezelő_Dolgozó_Beosztás_Új();
            try
            {
                List<Adat_Kiegészítő_Beosztáskódok> BeosztáskódÖ = KézBeoKód.Lista_Adatok(Telephely);
                BeosztáskódÖ = (from a in BeosztáskódÖ
                                where a.Számoló == true
                                orderby a.Beosztáskód
                                select a).ToList();

                List<Adat_Dolgozó_Beosztás_Új> DolgbeosztÖ = KézBeosztás.Lista_Adatok(Telephely, Dátum);
                DolgbeosztÖ = (from a in DolgbeosztÖ
                               where a.Nap == Dátum
                               orderby a.Dolgozószám
                               select a).ToList();

                foreach (Adat_Dolgozó_Alap Elem in AdatokNévsor)
                {
                    string dolgozik = (from a in DolgbeosztÖ
                                       where a.Dolgozószám.Trim() == Elem.Dolgozószám
                                       select a.Beosztáskód).FirstOrDefault();
                    //Van beosztása, akkor megnézzük, hogy az olyan amit be akarunk jelölni.
                    if (dolgozik != null)
                    {
                        string biztosdolgozik = (from a in BeosztáskódÖ
                                                 where dolgozik.Trim() == a.Beosztáskód.Trim()
                                                 select a.Beosztáskód).FirstOrDefault();
                        if (biztosdolgozik != null)
                            Válasz.Add(Elem);
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
            return Válasz;
        }

        /// <summary>
        /// IDM adatok beolvasása során használatos
        /// Dolgozószám, Dolgozónév, belépésiidő, kilépésiidő, munkakör 
        /// </summary>
        /// <param name="Telephely"></param>
        /// <param name="Adat"></param>
        public void Rögzítés_IDM(string Telephely, Adat_Dolgozó_Alap Adat)
        {

            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "INSERT INTO dolgozóadatok ( Dolgozószám, Dolgozónév, belépésiidő, kilépésiidő, munkakör )  VALUES ( ";
                szöveg += $"'{Adat.Dolgozószám}', ";   // Dolgozószám
                szöveg += $"'{Adat.DolgozóNév}', "; // Dolgozónév
                szöveg += $"'{Adat.Belépésiidő:yyyy.MM.dd}', ";  // belépésiidő
                szöveg += $"'{Adat.Kilépésiidő:yyyy.MM.dd}', ";  // kilépésiidő
                szöveg += $"'{Adat.Munkakör}') "; // munkakör
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

        /// <summary>
        /// IDM adatok beolvasása során használatos
        /// Dolgozószám, Dolgozónév, belépésiidő, kilépésiidő, munkakör 
        /// </summary>
        /// <param name="Telephely"></param>
        /// <param name="Adat"></param>
        public void Módosítás_IDM(string Telephely, Adat_Dolgozó_Alap Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE dolgozóadatok  SET ";
                szöveg += $"Dolgozónév='{Adat.DolgozóNév}', "; // Dolgozónév
                szöveg += $"belépésiidő='{Adat.Belépésiidő:yyyy.MM.dd}', ";  // belépésiidő
                szöveg += $"kilépésiidő='{Adat.Kilépésiidő:yyyy.MM.dd}' ";  // kilépésiidő
                szöveg += $" WHERE Dolgozószám='{Adat.Dolgozószám}'";
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

        //Elkopó
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



}
