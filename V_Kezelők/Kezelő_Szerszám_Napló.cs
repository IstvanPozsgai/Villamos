using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Szerszám_Napló
    {
        public List<Adat_Szerszám_Napló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Szerszám_Napló> Adatok = new List<Adat_Szerszám_Napló>();
            Adat_Szerszám_Napló Adat;

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
                                Adat = new Adat_Szerszám_Napló(
                                rekord["azonosító"].ToStrTrim(),
                                rekord["honnan"].ToStrTrim(),
                                rekord["hova"].ToStrTrim(),
                                rekord["mennyiség"].ToÉrt_Int(),
                                rekord["módosította"].ToStrTrim(),
                                rekord["módosításidátum"].ToÉrt_DaTeTime()
                                );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Szerszám_Napló Adat)
        {
            string szöveg = "INSERT INTO napló  (azonosító, honnan, hova, mennyiség, módosította, módosításidátum ) VALUES (";
            szöveg += $"'{Adat.Azonosító}', ";
            szöveg += $"'{Adat.Honnan}', ";
            szöveg += $"'{Adat.Hova}', ";
            szöveg += $"{Adat.Mennyiség}, ";
            szöveg += $"'{Program.PostásNév}', ";
            szöveg += $"'{DateTime.Now}') ";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

    }
}
