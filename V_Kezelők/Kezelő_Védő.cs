using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Védő_Könyv
    {
        public List<Adat_Védő_Könyv> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Védő_Könyv> Adatok = new List<Adat_Védő_Könyv>();
            Adat_Védő_Könyv Adat;

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
                                Adat = new Adat_Védő_Könyv(
                                        rekord["szerszámkönyvszám"].ToStrTrim(),
                                        rekord["szerszámkönyvnév"].ToStrTrim(),
                                        rekord["felelős1"].ToStrTrim(),
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

        public Adat_Védő_Könyv Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Védő_Könyv Adat = null;

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
                                Adat = new Adat_Védő_Könyv(
                                        rekord["szerszámkönyvszám"].ToStrTrim(),
                                        rekord["szerszámkönyvnév"].ToStrTrim(),
                                        rekord["felelős1"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Bool()
                                        );
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }




}

