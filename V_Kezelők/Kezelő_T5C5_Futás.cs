using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_T5C5_Futás
    {
        readonly string jelszó = "lilaakác";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\T5C5\villamos3.mdb";

        public Kezelő_T5C5_Futás()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Futásnaptábla_Létrehozás(hely.KönyvSzerk());
        }

        public List<Adat_T5C5_Futás> Lista_Adat()
        {
            string szöveg = "SELECT * FROM futástábla order by azonosító";
            List<Adat_T5C5_Futás> Adatok = new List<Adat_T5C5_Futás>();
            Adat_T5C5_Futás Adat;

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
                                Adat = new Adat_T5C5_Futás(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Dátum"].ToÉrt_DaTeTime(),
                                    rekord["Futásstátus"].ToStrTrim(),
                                    rekord["Státus"].ToÉrt_Long()
                                    ); ;
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        //Elkopó
        public List<Adat_T5C5_Futás> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Futás> Adatok = new List<Adat_T5C5_Futás>();
            Adat_T5C5_Futás Adat;

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
                                Adat = new Adat_T5C5_Futás(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Dátum"].ToÉrt_DaTeTime(),
                                    rekord["Futásstátus"].ToStrTrim(),
                                    rekord["Státus"].ToÉrt_Long()
                                    ); ;
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
