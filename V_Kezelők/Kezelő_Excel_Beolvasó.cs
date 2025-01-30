using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Kezelők
{
    public class Kezelő_Excel_Beolvasó
    {
        public List<Adat_Excel_Beolvasó> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Excel_Beolvasó> Adatok = new List<Adat_Excel_Beolvasó>();
            Adat_Excel_Beolvasó Adat;

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
                                Adat = new Adat_Excel_Beolvasó(
                                    rekord["csoport"].ToStrTrim(),
                                    rekord["oszlop"].ToÉrt_Int(),
                                    rekord["fejléc"].ToStrTrim(),
                                    rekord["törölt"].ToStrTrim(),
                                    rekord["kell"].ToÉrt_Int()
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
