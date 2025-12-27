using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;

namespace Villamos.Kezelők
{
    public class Kezelő_Sérülés_Művelet
    {
        string hely;
        readonly string jelszó = "tükör";
        readonly string táblanév = "művelet";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Év}\sérülés{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely.KönyvSzerk());
        }

        public List<Adat_Sérülés_Művelet> Lista_Adatok(int Év)
        {
            string szöveg = $"SELECT * FROM {táblanév} ";
            FájlBeállítás(Év);
            List<Adat_Sérülés_Művelet> Adatok = new List<Adat_Sérülés_Művelet>();
            Adat_Sérülés_Művelet Adat;

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
                                Adat = new Adat_Sérülés_Művelet(
                                           rekord["Teljesítményfajta"].ToStrTrim(),
                                           rekord["Rendelés"].ToÉrt_Int(),
                                           rekord["Visszaszám"].ToStrTrim(),
                                           rekord["Műveletszöveg"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Sérülés_Művelet Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Sérülés_Művelet Adat = null;

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
                                Adat = new Adat_Sérülés_Művelet(
                                           rekord["Teljesítményfajta"].ToStrTrim(),
                                           rekord["Rendelés"].ToÉrt_Int(),
                                           rekord["Visszaszám"].ToStrTrim(),
                                           rekord["Műveletszöveg"].ToStrTrim());
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }

}
