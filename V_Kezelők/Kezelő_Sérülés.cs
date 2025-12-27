using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Sérülés_Ideig
    {
        public List<Adat_Sérülés_Ideig> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Sérülés_Ideig> Adatok = new List<Adat_Sérülés_Ideig>();
            Adat_Sérülés_Ideig Adat;

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
                                Adat = new Adat_Sérülés_Ideig(
                                           rekord["Rendelés"].ToÉrt_Int(),
                                           rekord["Anyagköltség"].ToÉrt_Int(),
                                           rekord["Munkaköltség"].ToÉrt_Int(),
                                           rekord["Gépköltség"].ToÉrt_Int(),
                                           rekord["Szolgáltatás"].ToÉrt_Int(),
                                           rekord["Státus"].ToÉrt_Int());
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
