using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;

namespace Villamos.Kezelők
{
    public class Kezelő_Sérülés_Jelentés
    {
        string hely;
        readonly string jelszó = "tükör";
        readonly string táblanév = "költség";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\{Év}\sérülés{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Tükörtáblák(hely.KönyvSzerk());
        }

        public List<Adat_Sérülés_Jelentés> Lista_Adatok(int Év)
        {
            FájlBeállítás(Év);
            List<Adat_Sérülés_Jelentés> Adatok = new List<Adat_Sérülés_Jelentés>();
            Adat_Sérülés_Jelentés Adat;

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
                                Adat = new Adat_Sérülés_Jelentés(
                                           rekord["Sorszám"].ToÉrt_Int(),
                                           rekord["Telephely"].ToStrTrim(),
                                           rekord["Dátum"].ToÉrt_DaTeTime(),
                                           rekord["Balesethelyszín"].ToStrTrim(),
                                           rekord["Viszonylat"].ToStrTrim(),
                                           rekord["Rendszám"].ToStrTrim(),
                                           rekord["Járművezető"].ToStrTrim(),
                                           rekord["Rendelésszám"].ToÉrt_Int(),
                                           rekord["Kimenetel"].ToÉrt_Int(),
                                           rekord["Státus"].ToÉrt_Int(),
                                           rekord["Iktatószám"].ToStrTrim(),
                                           rekord["Típus"].ToStrTrim(),
                                           rekord["Szerelvény"].ToStrTrim(),
                                           rekord["Forgalmiakadály"].ToÉrt_Int(),
                                           rekord["Műszaki"].ToÉrt_Bool(),
                                           rekord["Anyagikár"].ToÉrt_Bool(),
                                           rekord["Biztosító"].ToStrTrim(),
                                           rekord["Személyisérülés"].ToÉrt_Bool(),
                                           rekord["Személyisérülés1"].ToÉrt_Bool(),
                                           rekord["Biztosítóidő"].ToÉrt_Int(),
                                           rekord["Mivelütközött"].ToStrTrim(),
                                           rekord["Anyagikárft"].ToÉrt_Int(),
                                           rekord["Leírás"].ToStrTrim(),
                                           rekord["Leírás1"].ToStrTrim(),
                                           rekord["Balesethelyszín1"].ToStrTrim(),
                                           rekord["Esemény"].ToStrTrim(),
                                           rekord["Anyagikárft1"].ToÉrt_Int(),
                                           rekord["Státus1"].ToÉrt_Int(),
                                           rekord["Kmóraállás"].ToStrTrim());
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
