using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Takarítás
    {
        readonly string jelszó = "Mocó";
        string hely;
        readonly string táblanév = "takarítástípus";

        private bool FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\segéd\Kiegészítő.mdb";
            return File.Exists(hely);
            //nincs elkészítve
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Adatok_Napló(hely.KönyvSzerk());
        }

        public List<string> Lista_Adatok(string Telephely)
        {
            List<string> Adatok = new List<string>();
            if (FájlBeállítás(Telephely))
            {
                string szöveg = $"Select * FROM {táblanév} order by típus";
               
                FájlBeállítás(Telephely);
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
                                    Adatok.Add(rekord["típus"].ToString());
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
