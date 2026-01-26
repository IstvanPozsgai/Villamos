using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Jármű
    {
        readonly string jelszó = "pozsgaii";
        string hely;
        readonly string táblanév = "állománytábla";

        private void FájlBeállítás(string Telephely)
        {

            if (Telephely == "Főmérnökség" || Telephely.Contains("törzs") || Telephely.Contains("osztály"))
            {
                hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\villamos.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.KocsikTípusaTelep(hely.KönyvSzerk());
            }
            else
            {
                hely = $@"{Application.StartupPath}\{Telephely}\Adatok\villamos\villamos.mdb";
                if (!File.Exists(hely)) Adatbázis_Létrehozás.KocsikTípusa(hely.KönyvSzerk());
            }
        }

        public List<Adat_Jármű> Lista_Adatok(string Telephely)
        {
            if (Telephely == "Főmérnökség")
                return Lista_AdatokFő(Telephely);
            else
                return Lista_AdatokTelephely(Telephely);
        }

        private List<Adat_Jármű> Lista_AdatokFő(string Telephely)
        {
            string szöveg = $"SELECT * FROM {táblanév} order by azonosító";
            FájlBeállítás(Telephely);

            List<Adat_Jármű> Adatok = new List<Adat_Jármű>();
            Adat_Jármű Adat;
            try
            {
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
                                    Adat = new Adat_Jármű(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["hibák"].ToÉrt_Long(),
                                        rekord["státus"].ToÉrt_Long(),
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Üzem"].ToStrTrim(),
                                        rekord["törölt"].ToÉrt_Bool(),
                                        rekord["hibáksorszáma"].ToÉrt_Long(),
                                        rekord["szerelvény"].ToÉrt_Bool(),
                                        rekord["szerelvénykocsik"].ToÉrt_Long(),
                                        rekord["miótaáll"].ToÉrt_DaTeTime(),
                                        rekord["valóstípus"].ToStrTrim(),
                                        rekord["valóstípus2"].ToStrTrim(),
                                        rekord["Üzembehelyezés"].ToÉrt_DaTeTime()
                                        );
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Lista_Adatok\n" + szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
            return Adatok;
        }

        private List<Adat_Jármű> Lista_AdatokTelephely(string Telephely)
        {
            string szöveg = $"SELECT * FROM {táblanév} order by azonosító";
            FájlBeállítás(Telephely);

            List<Adat_Jármű> Adatok = new List<Adat_Jármű>();
            Adat_Jármű Adat;
            try
            {
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
                                    Adat = new Adat_Jármű(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["hibák"].ToÉrt_Long(),
                                        rekord["státus"].ToÉrt_Long(),
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["Üzem"].ToStrTrim(),
                                        rekord["törölt"].ToÉrt_Bool(),
                                        rekord["hibáksorszáma"].ToÉrt_Long(),
                                        rekord["szerelvény"].ToÉrt_Bool(),
                                        rekord["szerelvénykocsik"].ToÉrt_Long(),
                                        rekord["miótaáll"].ToÉrt_DaTeTime(),
                                        rekord["valóstípus"].ToStrTrim(),
                                        rekord["valóstípus2"].ToStrTrim()
                                        );
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Lista_Adatok\n" + szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                if (Telephely == "Főmérnökség")
                {
                    string szöveg = $"INSERT INTO {táblanév} (azonosító, hibák, státus, típus, üzem, törölt, hibáksorszáma, szerelvény, szerelvénykocsik, miótaáll, valóstípus, valóstípus2, üzembehelyezés) VALUES (";
                    szöveg += $"'{Adat.Azonosító.Trim()}', 0, 0, 'Nincs', 'Közös', false, 0, false, 0, '1900.01.01', ";
                    szöveg += $"'{Adat.Valóstípus.Trim()}', ";
                    szöveg += $"'{Adat.Valóstípus2.Trim()}', '1900.01.01')";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
                }
                else
                {
                    string szöveg = $"INSERT INTO {táblanév} (azonosító, hibák, státus, típus, üzem, törölt, hibáksorszáma, szerelvény, szerelvénykocsik, miótaáll, valóstípus, valóstípus2) VALUES (";
                    szöveg += $"'{Adat.Azonosító.Trim()}', 0, 0, '{Adat.Típus}', '{Adat.Üzem}', false, 0, false, 0, '1900.01.01', ";
                    szöveg += $"'{Adat.Valóstípus.Trim()}', ";
                    szöveg += $"'{Adat.Valóstípus2.Trim()}')";
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

        /// <summary>
        /// Azonosító alapján módosítja a telephelyet
        /// </summary>
        /// <param name="Telephely"></param>
        /// <param name="Üzemek"></param>
        /// <param name="Azonosítók"></param>
        public void Módosítás_Telephely(string Telephely, List<string> Üzemek, List<string> Azonosítók)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < Üzemek.Count; i++)
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $"üzem='{Üzemek[i].Trim()}' ";
                    szöveg += $" WHERE [azonosító] ='{Azonosítók[i]}'";
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

        /// <summary>
        /// Azonosító alapján módosítja a státuszt és a hibát
        /// </summary>
        /// <param name="Telephely"></param>
        /// <param name="Adat"></param>
        public void Módosítás_Hiba_Státus(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $" hibák={Adat.Hibák}, ";
                szöveg += $" státus={Adat.Státus} ";
                szöveg += $" WHERE  [azonosító]='{Adat.Azonosító}'";
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
        /// Azonosító alapján módosítja a státuszt és a mióta állt módosítja
        /// </summary>
        /// <param name="Telephely"></param>
        /// <param name="Adat"></param>
        public void Módosítás_Státus_Dátum(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $" hibák={Adat.Hibák}, ";
                szöveg += $" miótaáll='{Adat.Miótaáll}' ";
                szöveg += $" WHERE  [azonosító]='{Adat.Azonosító}'";
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
        /// Azonosító alapján módosítja a státuszt, hibát és a mióta állt módosítja
        /// </summary>
        /// <param name="Telephely"></param>
        /// <param name="Adat"></param>
        public void Módosítás_Státus_Hiba_Dátum(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $" hibák={Adat.Hibák}, ";
                szöveg += $" státus={Adat.Státus}, ";
                szöveg += $" miótaáll='{Adat.Miótaáll}' ";
                szöveg += $" WHERE  [azonosító]='{Adat.Azonosító}'";
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
        /// Módosítja a hibát az azonosító alapján
        /// </summary>
        /// <param name="Telephely"></param>
        /// <param name="Adat"></param>
        public void Módosítás_Hiba(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE  {táblanév}  SET ";
                szöveg += $" hibák={Adat.Hibák} ";
                szöveg += $" WHERE  [azonosító]='{Adat.Azonosító}'";
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

        public void Módosítás_ÜzemBe(string Telephely, List<Adat_Jármű> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Jármű Adat in Adatok)
                {
                    string szöveg = $"UPDATE  {táblanév}  SET ";
                    szöveg += $" üzembehelyezés='{Adat.Üzembehelyezés:yyyy.MM.dd}' ";
                    szöveg += $"where [azonosító] ='{Adat.Azonosító}'";
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

        public void Módosítás_ÜzemÁtvétel(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE  {táblanév}  SET ";
                szöveg += $" üzem='{Adat.Üzem}', típus='{Adat.Típus}' WHERE azonosító='{Adat.Azonosító}'";
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

        public void Módosítás_Típus(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg;
                if (Telephely == "Főmérnökség")
                {
                    szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $"valóstípus='{Adat.Valóstípus}', ";
                    szöveg += $"valóstípus2='{Adat.Valóstípus2}', ";
                    szöveg += $"üzembehelyezés='{Adat.Üzembehelyezés:yyyy.MM.dd}' ";
                    szöveg += $"where [azonosító] ='{Adat.Azonosító}'";
                }
                else
                {
                    szöveg = $"UPDATE  {táblanév}  SET ";
                    szöveg += $"valóstípus='{Adat.Valóstípus}', ";
                    szöveg += $"valóstípus2='{Adat.Valóstípus2}' ";
                    szöveg += $"where [azonosító] ='{Adat.Azonosító}'";
                }
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

        public void Módosítás_Dátum(string Telephely, string Azonosító, DateTime Dátum)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE  {táblanév}  SET ";
                szöveg += $"miótaáll='{Dátum}' ";
                szöveg += $"where [azonosító] ='{Azonosító}'";
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

        public void Módosítás_Szerelvény(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE  {táblanév}  SET ";
                szöveg += $" szerelvény={Adat.Szerelvény}, szerelvénykocsik={Adat.Szerelvénykocsik}";
                szöveg += $" WHERE azonosító='{Adat.Azonosító}'";
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

        public void Módosítás_Szerelvény(string Telephely, List<Adat_Jármű> Adatok)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<string> szövegGy = new List<string>();
                foreach (Adat_Jármű Adat in Adatok)
                {
                    string szöveg = $"UPDATE  {táblanév}  SET ";
                    szöveg += $" szerelvény={Adat.Szerelvény}, szerelvénykocsik={Adat.Szerelvénykocsik}";
                    szöveg += $" WHERE azonosító='{Adat.Azonosító}'";
                    szövegGy.Add(szöveg);
                }

                MyA.ABMódosítás(hely, jelszó, szövegGy);
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

        public void Módosítás(string Telephely, Adat_Jármű Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE  {táblanév}  SET ";
                szöveg += $"hibák={Adat.Hibák}, ";
                szöveg += $"státus={Adat.Státus}, ";
                szöveg += $"törölt={Adat.Törölt}, ";
                szöveg += $"hibáksorszáma={Adat.Hibáksorszáma}, ";
                szöveg += $"szerelvény={Adat.Szerelvény}, ";
                szöveg += $"valóstípus='{Adat.Valóstípus.Trim()}', ";
                szöveg += $"valóstípus2='{Adat.Valóstípus2.Trim()}', ";
                szöveg += $"szerelvénykocsik={Adat.Szerelvénykocsik}, ";
                szöveg += $"miótaáll='{Adat.Miótaáll}', ";
                szöveg += $"típus='{Adat.Típus.Trim()}', ";
                szöveg += $"üzem='{Adat.Üzem.Trim()}' ";
                szöveg += $" WHERE [azonosító] ='{Adat.Azonosító.Trim()}'";
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

        public void Törlés(string Telephely, string Azonosító)
        {
            try
            {
                List<Adat_Jármű> Adatok = Lista_Adatok(Telephely);
                Adat_Jármű Elem = (from a in Adatok
                                   where a.Azonosító == Azonosító
                                   select a).FirstOrDefault();

                string szöveg;
                if (Elem != null)
                {
                    if (Telephely == "Főmérnökség")
                    {
                        if (Elem.Törölt)
                            szöveg = $"UPDATE {táblanév} SET törölt=false WHERE [azonosító]='{Azonosító}'";
                        else
                            szöveg = $"UPDATE {táblanév} SET törölt=true WHERE [azonosító]='{Azonosító}'";
                        MyA.ABMódosítás(hely, jelszó, szöveg);
                    }
                    else
                    {
                        szöveg = $"DELETE FROM {táblanév} WHERE [azonosító]='{Azonosító}'";
                        MyA.ABtörlés(hely, jelszó, szöveg);
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
        }
    }
}
