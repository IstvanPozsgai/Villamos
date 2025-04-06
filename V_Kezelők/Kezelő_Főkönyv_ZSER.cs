using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Főkönyv_ZSER
    {
        public List<Adat_Főkönyv_ZSER> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Főkönyv_ZSER> Adatok = new List<Adat_Főkönyv_ZSER>();
            Adat_Főkönyv_ZSER Adat;

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
                                Adat = new Adat_Főkönyv_ZSER(
                                    rekord["viszonylat"].ToStrTrim(),
                                    rekord["forgalmiszám"].ToStrTrim(),
                                    rekord["tervindulás"].ToÉrt_DaTeTime(),
                                    rekord["tényindulás"].ToÉrt_DaTeTime(),
                                    rekord["tervérkezés"].ToÉrt_DaTeTime(),
                                    rekord["tényérkezés"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["szerelvénytípus"].ToStrTrim(),
                                    rekord["kocsikszáma"].ToÉrt_Long(),
                                    rekord["megjegyzés"].ToStrTrim(),
                                    rekord["kocsi1"].ToStrTrim(),
                                    rekord["kocsi2"].ToStrTrim(),
                                    rekord["kocsi3"].ToStrTrim(),
                                    rekord["kocsi4"].ToStrTrim(),
                                    rekord["kocsi5"].ToStrTrim(),
                                    rekord["kocsi6"].ToStrTrim(),
                                    rekord["ellenőrző"].ToStrTrim(),
                                    rekord["Státus"].ToStrTrim()
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
