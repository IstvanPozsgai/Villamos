using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Kerék_Eszterga_Esztergályos
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";
        readonly string jelszó = "RónaiSándor";
        readonly string táblanév = "Esztergályos";


        public Kezelő_Kerék_Eszterga_Esztergályos()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kerék_Törzs(hely.KönyvSzerk());
        }

        public List<Adat_Kerék_Eszterga_Esztergályos> Lista_Adatok()
        {
            List<Adat_Kerék_Eszterga_Esztergályos> Adatok = new List<Adat_Kerék_Eszterga_Esztergályos>();
            Adat_Kerék_Eszterga_Esztergályos Adat;
            string szöveg = $"SELECT * FROM {táblanév}";
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
                                Adat = new Adat_Kerék_Eszterga_Esztergályos(
                                        rekord["Dolgozószám"].ToStrTrim(),
                                        rekord["Dolgozónév"].ToStrTrim(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Státus"].ToÉrt_Int()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }



        //elkopó
        public List<Adat_Kerék_Eszterga_Esztergályos> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kerék_Eszterga_Esztergályos> Adatok = new List<Adat_Kerék_Eszterga_Esztergályos>();
            Adat_Kerék_Eszterga_Esztergályos Adat;

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
                                Adat = new Adat_Kerék_Eszterga_Esztergályos(
                                        rekord["Dolgozószám"].ToStrTrim(),
                                        rekord["Dolgozónév"].ToStrTrim(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Státus"].ToÉrt_Int()
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
