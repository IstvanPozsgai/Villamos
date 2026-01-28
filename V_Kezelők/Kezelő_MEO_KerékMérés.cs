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
    public class Kezelő_MEO_KerékMérés
    {
        readonly string jelszó = "rudolfg";
        string hely;
        readonly string táblanév = "tábla";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Év}\{Év}_kerékmérések.mdb".KönyvSzerk();
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kerékmérésektábla(hely);
        }

        public List<Adat_MEO_KerékMérés> Lista_Adatok(int Év, bool törölt = false)
        {
            FájlBeállítás(Év);
            List<Adat_MEO_KerékMérés> Adatok = new List<Adat_MEO_KerékMérés>();
            string szöveg = $"SELECT * FROM {táblanév} ";
            if (törölt) szöveg += " WHERE törölt=false ORDER BY azonosító, Bekövetkezés DESC";

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
                                Adat_MEO_KerékMérés Adat = new Adat_MEO_KerékMérés(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Bekövetkezés"].ToÉrt_DaTeTime(),
                                        rekord["Üzem"].ToStrTrim(),
                                        rekord["Törölt"].ToÉrt_Bool(),
                                        rekord["Mikor"].ToÉrt_DaTeTime(),
                                        rekord["Ki"].ToStrTrim(),
                                        rekord["Típus"].ToStrTrim()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Rögzítés(int Év, Adat_MEO_KerékMérés Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"INSERT INTO {táblanév} (azonosító, üzem, típus, bekövetkezés, mikor, ki, törölt ) VALUES (";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"'{Adat.Üzem}', ";
                szöveg += $"'{Adat.Típus}', ";
                szöveg += $"'{Adat.Bekövetkezés:yyyy.MM.dd}', ";
                szöveg += $"'{Adat.Mikor}', ";
                szöveg += $"'{Adat.Ki}', {Adat.Törölt} )";
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


        public void Módosítás(int Év, Adat_MEO_KerékMérés Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"UPDATE tábla  SET ";
                szöveg += " törölt=true,";
                szöveg += $" mikor='{Adat.Mikor}', ";
                szöveg += $" ki='{Adat.Ki}' ";
                szöveg += $" WHERE azonosító ='{Adat.Azonosító} '";
                szöveg += $" AND üzem='{Adat.Üzem}'";
                szöveg += $" AND Bekövetkezés=#{Adat.Bekövetkezés:M-d-yy}#";
                szöveg += " AND törölt=false";
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
