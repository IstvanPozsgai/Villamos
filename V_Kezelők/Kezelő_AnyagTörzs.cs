using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.V_Adatbázis;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.V_Kezelők
{
    public class Kezelő_AnyagTörzs
    {
        readonly string hely;
        readonly string jelszó = "kasosmiklós";
        readonly string táblanév = "AnyagTábla";

        public Kezelő_AnyagTörzs()
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\rezsi\AnyagTörzs.mdb".KönyvSzerk();
            if (!File.Exists(hely)) Adatbázis_Létrehozás.AnyagTörzs(hely);
        }

        public List<Adat_Anyagok> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Anyagok> Adatok = new List<Adat_Anyagok>();


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
                                Adat_Anyagok Adat = new Adat_Anyagok(
                                        rekord["Cikkszám"].ToStrTrim(),
                                        rekord["Megnevezés"].ToStrTrim(),
                                        rekord["KeresőFogalom"].ToStrTrim(),
                                        rekord["Sarzs"].ToStrTrim(),
                                        rekord["Ár"].ToÉrt_Double()
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
