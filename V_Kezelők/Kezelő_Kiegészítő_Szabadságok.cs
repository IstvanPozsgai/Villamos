using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Szabadságok
    {
        readonly string jelszó = "Mocó";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Segéd\kiegészítő.mdb";
            //   if (!File.Exists(hely)) Adatbázis_Létrehozás.Beosztás_Naplózása(hely.KönyvSzerk());
        }

        public List<Adat_Kiegészítő_Szabadságok> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            List<Adat_Kiegészítő_Szabadságok> Adatok = new List<Adat_Kiegészítő_Szabadságok>();
            Adat_Kiegészítő_Szabadságok Adat;
            string szöveg = "SELECT * FROM szabadságok";
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
            return Adatok;
        }

        //Elkopó
        public List<Adat_Kiegészítő_Szabadságok> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Szabadságok> Adatok = new List<Adat_Kiegészítő_Szabadságok>();
            Adat_Kiegészítő_Szabadságok Adat;

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
            return Adatok;
        }
    }
}
