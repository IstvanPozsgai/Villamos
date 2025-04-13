using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Takarítás
    {
        readonly string jelszó = "Mocó";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\segéd\Kiegészítő.mdb";
            // if (!File.Exists(hely)) Adatbázis_Létrehozás.  (hely.KönyvSzerk());
        }

        public List<string> Lista_Adatok(string Telephely)
        {
            string szöveg = "Select * FROM takarítástípus order by típus";
            List<string> Adatok = new List<string>();
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
            return Adatok;
        }
    }
}
