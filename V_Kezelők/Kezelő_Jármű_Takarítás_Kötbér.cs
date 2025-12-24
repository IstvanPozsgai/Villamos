using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;


namespace Villamos.Kezelők
{
    public class Kezelő_Jármű_Takarítás_Kötbér
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Takarítás\Jármű_Takarítás.mdb";
        readonly string jelszó = "seprűéslapát";

        public Kezelő_Jármű_Takarítás_Kötbér()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Járműtakarító_Főmérnök_tábla(hely.KönyvSzerk());
        }

        public List<Adat_Jármű_Takarítás_Kötbér> Lista_Adat()
        {
            string szöveg = "SELECT * FROM kötbér order by takarítási_fajta";
            List<Adat_Jármű_Takarítás_Kötbér> Adatok = new List<Adat_Jármű_Takarítás_Kötbér>();
            Adat_Jármű_Takarítás_Kötbér Adat;

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
                                Adat = new Adat_Jármű_Takarítás_Kötbér(
                                        rekord["takarítási_fajta"].ToStrTrim(),
                                        rekord["nemMegfelel"].ToStrTrim(),
                                        rekord["póthatáridő"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Jármű_Takarítás_Kötbér Adat)
        {
            string szöveg = "INSERT INTO kötbér (Takarítási_fajta, NemMegfelel, Póthatáridő ) VALUES (";
            szöveg += $"'{Adat.Takarítási_fajta}', "; // Takarítási_fajta
            szöveg += $"{Adat.NemMegfelel}, "; // NemMegfelel
            szöveg += $"{Adat.Póthatáridő}) ";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Módosítás(Adat_Jármű_Takarítás_Kötbér Adat)
        {
            string szöveg = "UPDATE kötbér  SET ";
            szöveg += $" NemMegfelel={Adat.NemMegfelel}, "; // NemMegfelel
            szöveg += $" Póthatáridő={Adat.Póthatáridő}"; // Póthatáridő
            szöveg += $" WHERE  takarítási_fajta='{Adat.Takarítási_fajta}'";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
    }

}
