using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;


namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Könyvtár
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\kiegészítő2.mdb";
        readonly string jelszó = "Mocó";
        public List<Adat_Kiegészítő_Könyvtár> Lista_Adatok(string hely, string jelszó, string szöveg)
        {

            Adat_Kiegészítő_Könyvtár Adat;
            List<Adat_Kiegészítő_Könyvtár> Adatok = new List<Adat_Kiegészítő_Könyvtár>();

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
                                Adat = new Adat_Kiegészítő_Könyvtár(
                                           rekord["id"].ToÉrt_Int(),
                                           rekord["név"].ToStrTrim(),
                                           rekord["vezér1"].ToÉrt_Bool(),
                                           rekord["Csoport1"].ToÉrt_Int(),
                                           rekord["Csoport2"].ToÉrt_Int(),
                                           rekord["vezér2"].ToÉrt_Bool(),
                                           rekord["sorrend1"].ToÉrt_Int(),
                                           rekord["sorrend2"].ToÉrt_Int()
                                           );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Kiegészítő_Könyvtár> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM könyvtár ORDER BY név";
            Adat_Kiegészítő_Könyvtár Adat;
            List<Adat_Kiegészítő_Könyvtár> Adatok = new List<Adat_Kiegészítő_Könyvtár>();

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
                                Adat = new Adat_Kiegészítő_Könyvtár(
                                           rekord["id"].ToÉrt_Int(),
                                           rekord["név"].ToStrTrim(),
                                           rekord["vezér1"].ToÉrt_Bool(),
                                           rekord["Csoport1"].ToÉrt_Int(),
                                           rekord["Csoport2"].ToÉrt_Int(),
                                           rekord["vezér2"].ToÉrt_Bool(),
                                           rekord["sorrend1"].ToÉrt_Int(),
                                           rekord["sorrend2"].ToÉrt_Int()
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
