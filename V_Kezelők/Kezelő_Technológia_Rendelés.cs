using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Technológia_Rendelés
    {
        readonly string jelszó = "Bezzegh";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\\Munkalap\Rendelés.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Technológia_Rendelés(hely.KönyvSzerk(), Telephely);
        }

        public List<Adat_Technológia_Rendelés> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = $"SELECT * FROM {Telephely}";
            List<Adat_Technológia_Rendelés> Adatok = new List<Adat_Technológia_Rendelés>();
            Adat_Technológia_Rendelés Adat;

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

                                Adat = new Adat_Technológia_Rendelés(
                                    rekord["év"].ToÉrt_Long(),
                                    rekord["Karbantartási_fokozat"].ToStrTrim(),
                                    rekord["Technológia_típus"].ToStrTrim(),
                                    rekord["Rendelésiszám"].ToStrTrim()
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
