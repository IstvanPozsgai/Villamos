using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{



    public class Kezelő_Jármű_Takarítás_Létszám
    {
        public List<Adat_Jármű_Takarítás_Létszám> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Jármű_Takarítás_Létszám> Adatok = new List<Adat_Jármű_Takarítás_Létszám>();
            Adat_Jármű_Takarítás_Létszám Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Létszám(
                                        rekord["dátum"].ToÉrt_DaTeTime(),
                                        rekord["előírt"].ToÉrt_Int(),
                                        rekord["megjelent"].ToÉrt_Int(),
                                        rekord["napszak"].ToÉrt_Int(),
                                        rekord["ruhátlan"].ToÉrt_Int());
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
