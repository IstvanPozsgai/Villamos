using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Behajtás_Telephelystátusz
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\behajtási\Behajtási_alap.mdb";
        readonly string jelszó = "egérpad";

        public Kezelő_Behajtás_Telephelystátusz()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Alap(hely.KönyvSzerk());
        }

        public List<Adat_Behajtás_Telephelystátusz> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM telephelystátus";
            List<Adat_Behajtás_Telephelystátusz> Adatok = new List<Adat_Behajtás_Telephelystátusz>();
            Adat_Behajtás_Telephelystátusz Adat;

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
                                Adat = new Adat_Behajtás_Telephelystátusz(
                                        rekord["ID"].ToÉrt_Int(),
                                        rekord["Státus"].ToStrTrim(),
                                        rekord["Gondnok"].ToÉrt_Int(),
                                        rekord["Indoklás"].ToÉrt_Int());
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
