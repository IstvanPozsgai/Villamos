using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.V_Kezelők
{
    public class Kezelő_Takarítás_BMR
    {
        public List<Adat_Takarítás_BMR> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Takarítás_BMR> Adatok = new List<Adat_Takarítás_BMR>();
            Adat_Takarítás_BMR Adat;

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
                                Adat = new Adat_Takarítás_BMR(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["JárműÉpület"].ToStrTrim(),
                                        rekord["BMRszám"].ToStrTrim(),
                                        rekord["Dátum"].ToÉrt_DaTeTime()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzít(string hely, string jelszó, Adat_Takarítás_BMR Adat)
        {
            string szöveg = "INSERT INTO TakarításBMR (Id, Telephely, JárműÉpület, BMRszám, Dátum) VALUES (";
            szöveg += $"{Adat.Id}, '{Adat.Telephely}', '{Adat.JárműÉpület}', '{Adat.BMRszám}', '{Adat.Dátum}')";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Rögzít(string hely, string jelszó, List<Adat_Takarítás_BMR> Adatok)
        {
            List<string> SzövegGy = new List<string>();
            foreach (Adat_Takarítás_BMR Adat in Adatok)
            {
                string szöveg = "INSERT INTO TakarításBMR (Id, Telephely, JárműÉpület, BMRszám, Dátum) VALUES (";
                szöveg += $"{Adat.Id}, '{Adat.Telephely}', '{Adat.JárműÉpület}', '{Adat.BMRszám}', '{Adat.Dátum}')";
                SzövegGy.Add(szöveg);
            }
            MyA.ABMódosítás(hely, jelszó, SzövegGy);
        }

        public void Módosít(string hely, string jelszó, Adat_Takarítás_BMR Adat)
        {
            string szöveg = "UPDATE TakarításBMR  SET ";
            //szöveg += $"Dátum='{Adat.Dátum.ToShortDateString()}', ";
            //szöveg += $"Telephely='{Adat.Telephely}', ";
            //szöveg += $"JárműÉpület='{Adat.JárműÉpület}', ";
            szöveg += $"BMRszám='{Adat.BMRszám}' ";
            szöveg += $" WHERE id={Adat.Id}";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Módosít(string hely, string jelszó, List<Adat_Takarítás_BMR> Adatok)
        {
            List<string> SzövegGy = new List<string>();
            foreach (Adat_Takarítás_BMR Adat in Adatok)
            {
                string szöveg = "UPDATE TakarításBMR  SET ";
                //szöveg += $"Dátum='{Adat.Dátum.ToShortDateString()}', ";
                //szöveg += $"Telephely='{Adat.Telephely}', ";
                //szöveg += $"JárműÉpület='{Adat.JárműÉpület}', ";
                szöveg += $"BMRszám='{Adat.BMRszám}' ";
                szöveg += $" WHERE id={Adat.Id}";
                SzövegGy.Add(szöveg);
            }
            MyA.ABMódosítás(hely, jelszó, SzövegGy);
        }
    }
}
