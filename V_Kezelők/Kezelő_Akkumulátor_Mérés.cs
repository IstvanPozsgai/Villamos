using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Kezelők
{
    public class Kezelő_Akkumulátor_Mérés
    {
        string hely;
        readonly string jelszó = "kasosmiklós";

        public DateTime Dátum { get; private set; }

        public Kezelő_Akkumulátor_Mérés(DateTime dátum)
        {
            Dátum = dátum;
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Akkumulátor\Akkunapló{Dátum.Year}.mdb";
        }

        public List<Adat_Akkumulátor_Mérés> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Akkumulátor_Mérés> Adatok = new List<Adat_Akkumulátor_Mérés>();
            Adat_Akkumulátor_Mérés Adat;

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
                                Adat = new Adat_Akkumulátor_Mérés(
                                        rekord["Gyáriszám"].ToStrTrim(),
                                        rekord["kisütésiáram"].ToÉrt_Long(),
                                        rekord["kezdetifesz"].ToÉrt_Double(),
                                        rekord["végfesz"].ToÉrt_Double(),
                                        rekord["kisütésiidő"].ToÉrt_DaTeTime(),
                                        rekord["kapacitás"].ToÉrt_Double(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["van"].ToStrTrim(),
                                        rekord["Mérésdátuma"].ToÉrt_DaTeTime(),
                                        rekord["Rögzítés"].ToÉrt_DaTeTime(),
                                        rekord["Rögzítő"].ToStrTrim(),
                                        rekord["id"].ToÉrt_Long()
                                         );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Akkumulátor_Mérés> Lista_Adatok(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Akkumulátor\Akkunapló{Dátum.Year - Év}.mdb";
            string szöveg = "SELECT * FROM méréstábla ORDER BY gyáriszám, Mérésdátuma asc";
            List<Adat_Akkumulátor_Mérés> Adatok = new List<Adat_Akkumulátor_Mérés>();
            Adat_Akkumulátor_Mérés Adat;

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
                                Adat = new Adat_Akkumulátor_Mérés(
                                        rekord["Gyáriszám"].ToStrTrim(),
                                        rekord["kisütésiáram"].ToÉrt_Long(),
                                        rekord["kezdetifesz"].ToÉrt_Double(),
                                        rekord["végfesz"].ToÉrt_Double(),
                                        rekord["kisütésiidő"].ToÉrt_DaTeTime(),
                                        rekord["kapacitás"].ToÉrt_Double(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["van"].ToStrTrim(),
                                        rekord["Mérésdátuma"].ToÉrt_DaTeTime(),
                                        rekord["Rögzítés"].ToÉrt_DaTeTime(),
                                        rekord["Rögzítő"].ToStrTrim(),
                                        rekord["id"].ToÉrt_Long()
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
