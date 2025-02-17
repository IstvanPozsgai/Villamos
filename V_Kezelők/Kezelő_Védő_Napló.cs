using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Védő_Napló
    {
        readonly string jelszó = "csavarhúzó";
        string hely;

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Védő\védőnapló{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Védőlistanapló(hely.KönyvSzerk());
        }

        public List<Adat_Védő_Napló> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = "SELECT * FROM lista order by azonosító";
            List<Adat_Védő_Napló> Adatok = new List<Adat_Védő_Napló>();
            Adat_Védő_Napló Adat;

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
                                Adat = new Adat_Védő_Napló(
                                        rekord["azonosító"].ToStrTrim(),
                                        rekord["Honnan"].ToStrTrim(),
                                        rekord["Hova"].ToStrTrim(),
                                        rekord["mennyiség"].ToÉrt_Double(),
                                        rekord["gyáriszám"].ToStrTrim(),
                                        rekord["Módosította"].ToStrTrim(),
                                        rekord["Módosításidátum"].ToÉrt_DaTeTime(),
                                        rekord["státus"].ToÉrt_Bool()
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
