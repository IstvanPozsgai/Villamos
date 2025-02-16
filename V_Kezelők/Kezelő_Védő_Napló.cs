using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Védő_Napló
    {
        public List<Adat_Védő_Napló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Védő_Napló> Adatok = new List<Adat_Védő_Napló>();
            Adat_Védő_Napló Adat;

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
                                Adat = new Adat_Védő_Napló(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Honnan"].ToStrTrim(),
                                        rekord["Hova"].ToStrTrim(),
                                        rekord["mennyiség"].ToÉrt_Double(),
                                        rekord["gyáriszám"].ToStrTrim(),
                                        rekord["Módosította"].ToStrTrim(),
                                        rekord["Módosításidátum"].ToÉrt_DaTeTime(),
                                        rekord["státus"].ToÉrt_Bool()
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
