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
    public class Kezelő_Kiegészítő_Beosegéd
    {
        public List<Adat_Kiegészítő_Beosegéd> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Beosegéd> Adatok = new List<Adat_Kiegészítő_Beosegéd>();
            Adat_Kiegészítő_Beosegéd Adat;

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
                                Adat = new Adat_Kiegészítő_Beosegéd(
                                     rekord["Beosztáskód"].ToStrTrim(),
                                     rekord["Túlóra"].ToÉrt_Int(),
                                     rekord["Kezdőidő"].ToÉrt_DaTeTime(),
                                     rekord["Végeidő"].ToÉrt_DaTeTime(),
                                     rekord["túlóraoka"].ToStrTrim(),
                                     rekord["telephely"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Beosegéd Adat)
        {
            string szöveg = "INSERT INTO beosegéd (beosztáskód, túlóra, kezdőidő, végeidő, túlóraoka, telephely) VALUES (";
            szöveg += $"' + {Adat.Beosztáskód} + ', ";
            szöveg += $"{Adat.Túlóra}, ";
            szöveg += $"'{Adat.Kezdőidő}', ";
            szöveg += $"'{Adat.Végeidő}', ";
            szöveg += $"'{Adat.Túlóraoka}', ";
            szöveg += $"'{Adat.Telephely}' ) ";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        /// <summary>
        /// beosztáskód, telephely
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Beosegéd Adat)
        {
            string szöveg = " UPDATE  beosegéd SET ";
            szöveg += $" túlóra={Adat.Túlóra}, ";
            szöveg += $" túlóraoka='{Adat.Túlóraoka}', ";
            szöveg += $" kezdőidő='{Adat.Kezdőidő}', ";
            szöveg += $" végeidő='{Adat.Végeidő}' ";
            szöveg += $" WHERE beosztáskód='{Adat.Beosztáskód}' AND telephely='{Adat.Telephely}'";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public Adat_Kiegészítő_Beosegéd Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Beosegéd Adat = null;

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
                            rekord.Read();

                            Adat = new Adat_Kiegészítő_Beosegéd(
                                 rekord["Beosztáskód"].ToStrTrim(),
                                 rekord["Túlóra"].ToÉrt_Int(),
                                 rekord["Kezdőidő"].ToÉrt_DaTeTime(),
                                 rekord["Végeidő"].ToÉrt_DaTeTime(),
                                 rekord["túlóraoka"].ToStrTrim(),
                                 rekord["telephely"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }
    }

}
