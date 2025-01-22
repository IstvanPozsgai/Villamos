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
    public class Kezelő_Kiegészítő_Munkaidő
    {
        public List<Adat_Kiegészítő_Munkaidő> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Munkaidő> Adatok = new List<Adat_Kiegészítő_Munkaidő>();
            Adat_Kiegészítő_Munkaidő Adat;

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
                                Adat = new Adat_Kiegészítő_Munkaidő(
                                     rekord["munkarendelnevezés"].ToStrTrim(),
                                     rekord["munkaidő"].ToÉrt_Double()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Munkaidő Adat)
        {
            string szöveg = "INSERT INTO munkaidő (munkaidő, munkarendelnevezés) VALUES (";
            szöveg += $"'{Adat.Munkaidő}";
            szöveg += $"{Adat.Munkarendelnevezés} )";
            MyA.ABMódosítás(hely, jelszó, szöveg);            
        }

        /// <summary>
        /// munkarendelnevezés
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Munkaidő Adat)
        {
            string szöveg = " UPDATE  munkaidő SET ";
            szöveg += $" munkaidő={Adat.Munkaidő}";
            szöveg += $" WHERE munkarendelnevezés='{Adat.Munkarendelnevezés}'";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public Adat_Kiegészítő_Munkaidő Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Kiegészítő_Munkaidő Adat = null;

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
                            Adat = new Adat_Kiegészítő_Munkaidő(
                                  rekord["munkarendelnevezés"].ToStrTrim(),
                                  rekord["munkaidő"].ToÉrt_Double()
                                  );
                        }
                    }
                }
            }
            return Adat;
        }
    }
}
