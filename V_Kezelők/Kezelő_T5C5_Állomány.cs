using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_T5C5_Állomány
    {
        readonly string jelszó = "pozsgaii";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\T5C5\villamos3.mdb";

        public Kezelő_T5C5_Állomány()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Futásnaptábla_Létrehozás(hely.KönyvSzerk());
        }


        public List<Adat_T5C5_Állomány> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM Állománytábla ORDER BY azonosító";
            List<Adat_T5C5_Állomány> Adatok = new List<Adat_T5C5_Állomány>();
            Adat_T5C5_Állomány Adat;

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
                                Adat = new Adat_T5C5_Állomány(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Utolsórögzítés"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatdátuma"].ToÉrt_DaTeTime(),
                                    rekord["Utolsóforgalminap"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatfokozata"].ToStrTrim(),
                                    rekord["Vizsgálatszáma"].ToÉrt_Int(),
                                    rekord["Futásnap"].ToÉrt_Int(),
                                    rekord["Telephely"].ToStrTrim()
                                    ); ;
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        //Elkopó
        public List<Adat_T5C5_Állomány> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_T5C5_Állomány> Adatok = new List<Adat_T5C5_Állomány>();
            Adat_T5C5_Állomány Adat;

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
                                Adat = new Adat_T5C5_Állomány(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Utolsórögzítés"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatdátuma"].ToÉrt_DaTeTime(),
                                    rekord["Utolsóforgalminap"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatfokozata"].ToStrTrim(),
                                    rekord["Vizsgálatszáma"].ToÉrt_Int(),
                                    rekord["Futásnap"].ToÉrt_Int(),
                                    rekord["Telephely"].ToStrTrim()
                                    ); ;
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_T5C5_Állomány Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_T5C5_Állomány Adat = null;

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
                                Adat = new Adat_T5C5_Állomány(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["Utolsórögzítés"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatdátuma"].ToÉrt_DaTeTime(),
                                    rekord["Utolsóforgalminap"].ToÉrt_DaTeTime(),
                                    rekord["Vizsgálatfokozata"].ToStrTrim(),
                                    rekord["Vizsgálatszáma"].ToÉrt_Int(),
                                    rekord["Futásnap"].ToÉrt_Int(),
                                    rekord["Telephely"].ToStrTrim()
                                    ); ;
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }


}
