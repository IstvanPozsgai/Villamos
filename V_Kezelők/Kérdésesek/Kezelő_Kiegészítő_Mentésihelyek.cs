using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Mentésihelyek
    {
        readonly string jelszó = "Mocó";
        string hely;
        readonly string táblanév = "Mentésihelyek";

        private bool FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\segéd\Kiegészítő1.mdb";
            return File.Exists(hely);
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }

        public List<Adat_Kiegészítő_Mentésihelyek> Lista_Adatok(string Telephely)
        {
            List<Adat_Kiegészítő_Mentésihelyek> Adatok = new List<Adat_Kiegészítő_Mentésihelyek>();
            if (FájlBeállítás(Telephely))
            {
                string szöveg = $"SELECT * FROM {táblanév}  order by  sorszám";

                Adat_Kiegészítő_Mentésihelyek Adat;

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
                                    Adat = new Adat_Kiegészítő_Mentésihelyek(
                                         rekord["sorszám"].ToÉrt_Long(),
                                         rekord["alprogram"].ToStrTrim(),
                                         rekord["Elérésiút"].ToStrTrim());
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Kiegészítő_Mentésihelyek Adat)
        {
            if (FájlBeállítás(Telephely))
            {
                string szöveg = $"INSERT INTO {táblanév} ( sorszám, alprogram, elérésiút )";
                szöveg += $" VALUES ({Adat.Sorszám}, ";
                szöveg += $"'{Adat.Alprogram}',";
                szöveg += $"'{Adat.Elérésiút}' )";

                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
        }

        public void Módosítás(string Telephely, Adat_Kiegészítő_Mentésihelyek Adat)
        {
            try
            {
                if (FájlBeállítás(Telephely))
                {
                    string szöveg = $"UPDATE {táblanév} SET ";
                    szöveg += $" alprogram='{Adat.Alprogram}',";
                    szöveg += $" elérésiút='{Adat.Elérésiút}' ";
                    szöveg += $" WHERE sorszám={Adat.Sorszám} ";

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

        public void Törlés(string Telephely, Adat_Kiegészítő_Mentésihelyek Adat)
        {
            try
            {
                if (FájlBeállítás(Telephely))
                {
                    string szöveg = $"DELETE FROM {táblanév} WHERE sorszám={Adat.Sorszám}";
                    MyA.ABtörlés(hely, jelszó, szöveg);
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
