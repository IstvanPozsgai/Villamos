using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Behajtás_Kérelemstátus
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\behajtási\Behajtási_alap.mdb".Ellenőrzés();
        readonly string jelszó = "egérpad";


        public List<Adat_Behajtás_Kérelemsátus> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM Kérelemstátus ORDER BY id";
            List<Adat_Behajtás_Kérelemsátus> Adatok = new List<Adat_Behajtás_Kérelemsátus>();
            Adat_Behajtás_Kérelemsátus Adat;

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
                                Adat = new Adat_Behajtás_Kérelemsátus(
                                        rekord["ID"].ToÉrt_Int(),
                                        rekord["Státus"].ToStrTrim());
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
