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
    public class Kezelő_Kiegészítő_Beosztásciklus
    {
        public List<Adat_Kiegészítő_Beosztásciklus> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Beosztásciklus> Adatok = new List<Adat_Kiegészítő_Beosztásciklus>();
            Adat_Kiegészítő_Beosztásciklus Adat;

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
                                Adat = new Adat_Kiegészítő_Beosztásciklus(
                                       rekord["Id"].ToÉrt_Int(),
                                       rekord["Beosztáskód"].ToStrTrim(),
                                       rekord["Hétnapja"].ToStrTrim(),
                                       rekord["Beosztásszöveg"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Beosztásciklus Adat)
        {
            string szöveg = "INSERT INTO beosztásciklus (hétnapja, beosztáskód, beosztásszöveg) VALUES (";
            szöveg += $"'{Adat.Hétnapja}', ";
            szöveg += $"'{Adat.Beosztáskód}";
            szöveg += $"{Adat.Beosztásszöveg} )";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        /// <summary>
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Beosztásciklus Adat)
        {
            string szöveg = " UPDATE  beosztásciklus SET ";
            szöveg += $" hétnapja='{Adat.Hétnapja}', ";
            szöveg += $" beosztáskód='{Adat.Beosztáskód}', ";
            szöveg += $" beosztásszöveg='{Adat.Beosztásszöveg}' ";
            szöveg += $" WHERE   id={Adat.Id}";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        /// <summary>
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás1(string hely, string jelszó, Adat_Kiegészítő_Beosztásciklus Adat1)
        {
            string szöveg = " UPDATE  beosztásciklus SET ";
            szöveg += $" hétnapja='{Adat1.Hétnapja}', ";
            szöveg += $" beosztáskód='{Adat1.Beosztáskód}', ";
            szöveg += $" beosztásszöveg='{Adat1.Beosztásszöveg}' ";
            szöveg += $" WHERE   id={Adat1.Id}";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public Adat_Kiegészítő_Beosztásciklus Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kiegészítő_Beosztásciklus Adat = null;

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

                            Adat = new Adat_Kiegészítő_Beosztásciklus(
                                   rekord["Id"].ToÉrt_Int(),
                                   rekord["Beosztáskód"].ToStrTrim(),
                                   rekord["Hétnapja"].ToStrTrim(),
                                   rekord["Beosztásszöveg"].ToStrTrim());
                        }
                    }
                }
            }
            return Adat;
        }
    }
}
