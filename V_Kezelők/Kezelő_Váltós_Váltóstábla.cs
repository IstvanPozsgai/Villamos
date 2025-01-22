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
    public class Kezelő_Váltós_Váltóstábla
    {
        public List<Adat_Váltós_Váltóstábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Váltós_Váltóstábla> Adatok = new List<Adat_Váltós_Váltóstábla>();
            Adat_Váltós_Váltóstábla Adat;

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
                                Adat = new Adat_Váltós_Váltóstábla(
                                          rekord["Telephely"].ToStrTrim(),
                                          rekord["Csoport"].ToStrTrim(),
                                          rekord["Év"].ToÉrt_Int(),
                                          rekord["Félév"].ToÉrt_Int(),
                                          rekord["ZKnap"].ToÉrt_Double(),
                                          rekord["Epnap"].ToÉrt_Double(),
                                          rekord["Tperc"].ToÉrt_Double()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Váltós_Váltóstábla Adat)
        {
            string szöveg = "INSERT INTO váltóstábla (év, félév, csoport, ZKnap, EPnap, Tperc, telephely ) VALUES (";
            szöveg += $" VALUES ( {Adat.Év},";
            szöveg += $"'{Adat.Félév}', ";
            szöveg += $"'{Adat.Csoport}', ";
            szöveg += $"'{Adat.Zknap}', ";
            szöveg += $"'{Adat.Epnap}";
            szöveg += $"'{Adat.Tperc}";
            szöveg += $"{Adat.Telephely} )";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        /// <summary>
        /// év, félév, csoport, telephely
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Váltós_Váltóstábla Adat)
        {
            string szöveg = " UPDATE  váltóstábla SET ";
            szöveg += $" ZKnap={Adat.Zknap}, ";
            szöveg += $" EPnap={Adat.Epnap}, ";
            szöveg += $" Tperc={Adat.Tperc} ";
            szöveg += $" WHERE  év={Adat.Év}";
            szöveg += $" and félév={Adat.Félév}";
            szöveg += $" and csoport='{Adat.Csoport}'";
            szöveg += $" and telephely='{Adat.Telephely}'";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
    }
}
