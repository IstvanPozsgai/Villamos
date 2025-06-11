using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Kerék_Eszterga_Tevékenység
    {
        readonly string jelszó = "RónaiSándor";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";
        readonly string táblanév = "Tevékenység";

        public List<Adat_Kerék_Eszterga_Tevékenység> Lista_Adatok()
        {
            List<Adat_Kerék_Eszterga_Tevékenység> Adatok = new List<Adat_Kerék_Eszterga_Tevékenység>();
            Adat_Kerék_Eszterga_Tevékenység Adat;
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY id";
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
                                Adat = new Adat_Kerék_Eszterga_Tevékenység(
                                        rekord["Tevékenység"].ToStrTrim(),
                                        rekord["Munkaidő"].ToÉrt_Double(),
                                        rekord["betűszín"].ToÉrt_Long(),
                                        rekord["háttérszín"].ToÉrt_Long(),
                                        rekord["id"].ToÉrt_Int(),
                                        rekord["Marad"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }




        //Elkopó
        public List<Adat_Kerék_Eszterga_Tevékenység> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Eszterga_Tevékenység> Adatok = new List<Adat_Kerék_Eszterga_Tevékenység>();
            Adat_Kerék_Eszterga_Tevékenység Adat;

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
                                Adat = new Adat_Kerék_Eszterga_Tevékenység(
                                        rekord["Tevékenység"].ToStrTrim(),
                                        rekord["Munkaidő"].ToÉrt_Double(),
                                        rekord["betűszín"].ToÉrt_Long(),
                                        rekord["háttérszín"].ToÉrt_Long(),
                                        rekord["id"].ToÉrt_Int(),
                                        rekord["Marad"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public Adat_Kerék_Eszterga_Tevékenység Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kerék_Eszterga_Tevékenység Adat = null;

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
                            Adat = new Adat_Kerék_Eszterga_Tevékenység(
                                    rekord["Tevékenység"].ToStrTrim(),
                                    rekord["Munkaidő"].ToÉrt_Double(),
                                    rekord["betűszín"].ToÉrt_Long(),
                                    rekord["háttérszín"].ToÉrt_Long(),
                                    rekord["id"].ToÉrt_Int(),
                                    rekord["Marad"].ToÉrt_Bool()
                                    );
                        }
                    }
                }
            }
            return Adat;
        }
    }

}
