using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Szerszám_Cikk
    {
        public List<Adat_Szerszám_Cikktörzs> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Szerszám_Cikktörzs Adat;
            List<Adat_Szerszám_Cikktörzs> Adatok = new List<Adat_Szerszám_Cikktörzs>();

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
                                Adat = new Adat_Szerszám_Cikktörzs(
                                           rekord["Azonosító"].ToStrTrim(),
                                           rekord["megnevezés"].ToStrTrim(),
                                           rekord["méret"].ToStrTrim(),
                                           rekord["hely"].ToStrTrim(),
                                           rekord["leltáriszám"].ToStrTrim(),
                                           rekord["Beszerzésidátum"].ToÉrt_DaTeTime(),
                                           rekord["státus"].ToÉrt_Int(),
                                           rekord["költséghely"].ToStrTrim(),
                                           rekord["gyáriszám"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string hely, string jelszó, Adat_Szerszám_Cikktörzs Adat)
        {
            string szöveg = "UPDATE cikktörzs  SET ";
            szöveg += $"megnevezés='{Adat.Megnevezés}', ";
            szöveg += $"méret='{Adat.Méret}', ";
            szöveg += $"leltáriszám='{Adat.Leltáriszám}', ";
            szöveg += $"költséghely='{Adat.Költséghely}', ";
            szöveg += $"hely='{Adat.Hely}', ";
            szöveg += $"státus='{Adat.Státus}', ";
            szöveg += $"gyáriszám='{Adat.Gyáriszám}', ";
            szöveg += $" Beszerzésidátum='{Adat.Beszerzésidátum:yyyy.MM.dd}' ";
            szöveg += $" WHERE azonosító='{Adat.Azonosító}'";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Rögzítés(string hely, string jelszó, Adat_Szerszám_Cikktörzs Adat)
        {
            string szöveg = "INSERT INTO cikktörzs  (azonosító, megnevezés, méret, leltáriszám, Beszerzésidátum, státus, hely, költséghely, gyáriszám) VALUES (";
            szöveg += $"'{Adat.Azonosító}', ";
            szöveg += $"'{Adat.Megnevezés}', ";
            szöveg += $"'{Adat.Méret}', ";
            szöveg += $"'{Adat.Leltáriszám}', ";
            szöveg += $"'{Adat.Beszerzésidátum:yyyy.MM.dd}', ";
            szöveg += $"{Adat.Státus}, ";
            szöveg += $"'{Adat.Hely}', ";
            szöveg += $"'{Adat.Költséghely}', ";
            szöveg += $"'{Adat.Gyáriszám}') ";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
    }
}
