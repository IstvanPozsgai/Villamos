using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;

namespace Villamos.Kezelők
{
    public class Kezelő_Sérülés_Visszajelentés
    {
        string hely;
        readonly string jelszó = "tükör";
        readonly string táblanév = "visszajelentés";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Év}\sérülés{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely.KönyvSzerk());
        }

        public List<Adat_Sérülés_Visszajelentés> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Sérülés_Visszajelentés> Adatok = new List<Adat_Sérülés_Visszajelentés>();
            Adat_Sérülés_Visszajelentés Adat;

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
                                Adat = new Adat_Sérülés_Visszajelentés(
                                           rekord["Visszaszám"].ToStrTrim(),
                                           rekord["Munkaidő"].ToÉrt_Int(),
                                           rekord["Storno"].ToStrTrim(),
                                           rekord["Rendelés"].ToÉrt_Int(),
                                           rekord["Teljesítményfajta"].ToStrTrim());
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
