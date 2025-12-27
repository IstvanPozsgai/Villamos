using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;

namespace Villamos.Kezelők
{
    public class Kezelő_Sérülés_Anyag
    {
        string hely;
        readonly string jelszó = "tükör";
        readonly string táblanév = "Anyag";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Év}\sérülés{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely.KönyvSzerk());
        }

        public List<Adat_Sérülés_Anyag> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Sérülés_Anyag> Adatok = new List<Adat_Sérülés_Anyag>();
            Adat_Sérülés_Anyag Adat;

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
                                Adat = new Adat_Sérülés_Anyag(
                                           rekord["cikkszám"].ToStrTrim(),
                                           rekord["anyagnév"].ToStrTrim(),
                                           rekord["mennyiség"].ToÉrt_Double(),
                                           rekord["me"].ToStrTrim(),
                                           rekord["ár"].ToÉrt_Double(),
                                           rekord["állapot"].ToStrTrim(),
                                           rekord["Rendelés"].ToÉrt_Double(),
                                           rekord["mozgásnem"].ToStrTrim()
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
