using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Behajtás_Jogosultság
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\behajtási\Behajtási_alap.mdb".KönyvSzerk();
        readonly string jelszó = "egérpad";

        public Kezelő_Behajtás_Jogosultság()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Alap(hely.KönyvSzerk());
        }

        public List<Adat_Behajtás_Jogosultság> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM jogosultságtípus ORDER BY id";
            List<Adat_Behajtás_Jogosultság> Adatok = new List<Adat_Behajtás_Jogosultság>();
            Adat_Behajtás_Jogosultság Adat;

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
                                Adat = new Adat_Behajtás_Jogosultság(
                                        rekord["ID"].ToÉrt_Int(),
                                        rekord["Státustípus"].ToStrTrim());
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
