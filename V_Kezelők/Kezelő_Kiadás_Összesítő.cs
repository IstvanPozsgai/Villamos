using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiadás_Összesítő
    {
        string hely;
        readonly string jelszó = "plédke";

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\főkönyv\kiadás{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kiadásiösszesítőtábla(hely.KönyvSzerk());
        }

        public List<Adat_Kiadás_összesítő> Lista_adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = "SELECT * FROM tábla  ";

            List<Adat_Kiadás_összesítő> Adatok = new List<Adat_Kiadás_összesítő>();
            Adat_Kiadás_összesítő Adat;

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
                                Adat = new Adat_Kiadás_összesítő(

                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["forgalomban"].ToÉrt_Int(),
                                    rekord["tartalék"].ToÉrt_Int(),
                                    rekord["kocsiszíni"].ToÉrt_Int(),
                                    rekord["félreállítás"].ToÉrt_Int(),
                                    rekord["főjavítás"].ToÉrt_Int(),
                                    rekord["személyzet"].ToÉrt_Int()
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
        public List<Adat_Kiadás_összesítő> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiadás_összesítő> Adatok = new List<Adat_Kiadás_összesítő>();
            Adat_Kiadás_összesítő Adat;

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
                                Adat = new Adat_Kiadás_összesítő(

                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["forgalomban"].ToÉrt_Int(),
                                    rekord["tartalék"].ToÉrt_Int(),
                                    rekord["kocsiszíni"].ToÉrt_Int(),
                                    rekord["félreállítás"].ToÉrt_Int(),
                                    rekord["főjavítás"].ToÉrt_Int(),
                                    rekord["személyzet"].ToÉrt_Int()
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

