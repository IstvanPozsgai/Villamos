using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos_Kezelők
{
    public  class Kezelő_T5C5_Fűtés
    {
        public List<Adat_T5C5_Fűtés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Fűtés> Adatok = new List<Adat_T5C5_Fűtés>();
            Adat_T5C5_Fűtés Adat;

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
                                Adat = new Adat_T5C5_Fűtés(
                                       rekord["ID"].ToÉrt_Long(),
                                       rekord["Pályaszám"].ToStrTrim(),
                                       rekord["Telephely"].ToStrTrim(),
                                       rekord["Dátum"].ToÉrt_DaTeTime(),
                                       rekord["Dolgozó"].ToStrTrim(),
                                       rekord["I_szakasz"].ToÉrt_Double(),
                                       rekord["II_szakasz"].ToÉrt_Double(),
                                       rekord["Fűtés_típusa"].ToÉrt_Int(),
                                       rekord["Jófűtés"].ToStrTrim(),
                                       rekord["Megjegyzés"].ToStrTrim(),
                                       rekord["Beállítási_értékek"].ToÉrt_Int(),
                                       rekord["Módosító"].ToStrTrim(),
                                       rekord["Mikor"].ToÉrt_DaTeTime()
                                       );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_T5C5_Fűtés Adat)
        {
            string szöveg = "INSERT INTO Fűtés_tábla  (ID, pályaszám, telephely, dátum, dolgozó, I_szakasz, II_szakasz, fűtés_típusa, Jófűtés, Megjegyzés, Beállítási_értékek, Módosító, Mikor) VALUES (";
            szöveg += $"{Adat.ID}, "; // ID,
            szöveg += $"'{Adat.Pályaszám}', "; // pályaszám,
            szöveg += $"'{Adat.Telephely}', "; // telephely,
            szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', "; // dátum,
            szöveg += $"'{Adat.Dolgozó}', "; // dolgozó,
            szöveg += $"{Adat.I_szakasz.ToString().Replace(',', '.')}, "; // I_szakasz,
            szöveg += $"{Adat.II_szakasz.ToString().Replace(',', '.')}, "; // II_szakasz,
            szöveg += $"{Adat.Fűtés_típusa}, "; // fűtés_típusa,
            szöveg += $"'{Adat.Jófűtés}', "; // Jófűtés,
            szöveg += $"'{Adat.Megjegyzés}', "; // Megjegyzés,
            szöveg += $"{Adat.Beállítási_értékek},";// Beállítási_értékek,
            szöveg += $"'{Adat.Módosító}', "; // Módosító,
            szöveg += $"'{Adat.Mikor}') "; // Mikor
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
    }
}
