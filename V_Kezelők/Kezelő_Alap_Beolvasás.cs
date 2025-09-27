using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;


namespace Villamos.Kezelők
{
    public class Kezelő_Alap_Beolvasás
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\beolvasás.mdb";
        readonly string jelszó = "sajátmagam";

        public List<Adat_Alap_Beolvasás> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM tábla";
            List<Adat_Alap_Beolvasás> Adatok = new List<Adat_Alap_Beolvasás>();
            Adat_Alap_Beolvasás Adat;

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
                                Adat = new Adat_Alap_Beolvasás(
                                        rekord["csoport"].ToStrTrim(),
                                        rekord["oszlop"].ToÉrt_Int(),
                                        rekord["fejléc"].ToStrTrim(),
                                        rekord["törölt"].ToStrTrim(),
                                        rekord["kell"].ToÉrt_Long()
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
