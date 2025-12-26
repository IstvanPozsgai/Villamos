using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Adatszerkezet;


namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Részmunkakör
    {
        public List<Adat_Kiegészítő_Részmunkakör> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Részmunkakör Adat;
            List<Adat_Kiegészítő_Részmunkakör> Adatok = new List<Adat_Kiegészítő_Részmunkakör>();

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
                                Adat = new Adat_Kiegészítő_Részmunkakör(
                                           rekord["Id"].ToÉrt_Long(),
                                           rekord["Megnevezés"].ToStrTrim(),
                                           rekord["Id"].ToÉrt_Long()
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


    public class Kezelő_Kiegészítő_Doksi
    {
        public List<Adat_Kiegészítő_Doksi> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Doksi Adat;
            List<Adat_Kiegészítő_Doksi> Adatok = new List<Adat_Kiegészítő_Doksi>();

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
                                Adat = new Adat_Kiegészítő_Doksi(
                                           rekord["Kategória"].ToStrTrim(),
                                           rekord["Kód"].ToStrTrim(),
                                           rekord["Éves"].ToStrTrim()
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