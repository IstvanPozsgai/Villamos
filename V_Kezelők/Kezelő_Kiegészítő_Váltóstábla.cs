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
    public class Kezelő_Kiegészítő_Váltóstábla
    {
        public List<Adat_Kiegészítő_Váltóstábla> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Váltóstábla> Adatok = new List<Adat_Kiegészítő_Váltóstábla>();
            Adat_Kiegészítő_Váltóstábla Adat;

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
                                Adat = new Adat_Kiegészítő_Váltóstábla(
                                       rekord["Id"].ToÉrt_Int(),
                                       rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                       rekord["Ciklusnap"].ToÉrt_Int(),
                                       rekord["Megnevezés"].ToStrTrim(),
                                       rekord["Csoport"].ToString());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Váltóstábla Adat)
        {
            string szöveg = "INSERT INTO váltósbeosztás (kezdődátum, ciklusnap, megnevezés,  csoport) VALUES (";
            szöveg += $"' + {Adat.Kezdődátum} + ', ";
            szöveg += $"{Adat.Ciklusnap}, ";
            szöveg += $"' + {Adat.Megnevezés} + ', ";
            szöveg += $"' + {Adat.Csoport} + ' ) ";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        /// <summary>
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Váltóstábla Adat)
        {
            string szöveg = " UPDATE  váltósbeosztás SET ";
            szöveg += $" kezdődátum=' {Adat.Kezdődátum} ', ";
            szöveg += $" ciklusnap={Adat.Ciklusnap}, ";
            szöveg += $" megnevezés= ' {Adat.Megnevezés} ', ";
            szöveg += $" csoport=' {Adat.Csoport} ' ";
            szöveg += $" WHERE id={Adat.Id}" + Adat.Id;

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
    }
}
