using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Szabadságok
    {
        readonly string jelszó = "Mocó";
        string hely;
        readonly string táblanév = "szabadságok";

        private bool FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\segéd\Kiegészítő.mdb";
            return File.Exists(hely);
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }

        public List<Adat_Kiegészítő_Szabadságok> Lista_Adatok(string Telephely)
        {
            List<Adat_Kiegészítő_Szabadságok> Adatok = new List<Adat_Kiegészítő_Szabadságok>();
            if (FájlBeállítás(Telephely))
            {

                Adat_Kiegészítő_Szabadságok Adat;
                string szöveg = $"SELECT * FROM {táblanév}";
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
                                    Adat = new Adat_Kiegészítő_Szabadságok(
                                            rekord["Sorszám"].ToÉrt_Long(),
                                            rekord["Megnevezés"].ToStrTrim()
                                              );
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }
}
