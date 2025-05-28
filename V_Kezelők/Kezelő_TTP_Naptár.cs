using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_TTP_Naptár
    {
        public List<Adat_TTP_Naptár> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_TTP_Naptár> Adatok = new List<Adat_TTP_Naptár>();
            Adat_TTP_Naptár Adat;

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
                                Adat = new Adat_TTP_Naptár(
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Munkanap"].ToÉrt_Bool());
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
