using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Telep_Kiegészítő_Kidobó
    {
        readonly string jelszó = "Mocó";

        public List<Adat_Telep_Kiegészítő_Kidobó> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * FROM kidobó  WHERE  id=1";
            List<Adat_Telep_Kiegészítő_Kidobó> Adatok = new List<Adat_Telep_Kiegészítő_Kidobó>();
            Adat_Telep_Kiegészítő_Kidobó Adat;

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
                                Adat = new Adat_Telep_Kiegészítő_Kidobó(
                                                    rekord["Id"].ToÉrt_Long(),
                                                    rekord["Telephely"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, Adat_Telep_Kiegészítő_Kidobó Adat)
        {
            string szöveg = $"INSERT INTO kidobó (id, telephely)";
            szöveg += $"VALUES ({Adat.Id},";
            szöveg += $"'{Adat.Telephely})'";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        /// <summary>
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, Adat_Telep_Kiegészítő_Kidobó Adat)
        {
            string szöveg = $"UPDATE kidobó SET ";
            szöveg += $"telephely='{Adat.Telephely}'";
            szöveg += $"WHERE id={Adat.Id}";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
    }
}
