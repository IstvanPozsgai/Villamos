using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Kezelők
{
    public class Kezelő_Általános_Int_String
    {
        public List<Adat_Általános_Int_String> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Általános_Int_String> Adatok = new List<Adat_Általános_Int_String>();
            Adat_Általános_Int_String Adat;

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
                                Adat = new Adat_Általános_Int_String(
                                        rekord["id"].ToÉrt_Int (),
                                        rekord["szöveg"].ToStrTrim()
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


    public class Kezelő_Általános_String
    {
        public List<string> Lista_Adatok(string hely, string jelszó, string szöveg, string Mező)
        {
            List<string> Adatok = new List<string>();
            string Adat;

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
                                Adat = rekord[Mező].ToStrTrim();
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
