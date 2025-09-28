using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Szatube_Szabadság
    {
        readonly string jelszó = "kertitörpe";
        string hely;
        readonly string táblanév = "Szabadság";

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\Szatubecs\{Év}SzaTuBeCs.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.SzaTuBe_tábla(hely);
        }

        public List<Adat_Szatube_Szabadság> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = $"Select * FROM {táblanév}";
            List<Adat_Szatube_Szabadság> Adatok = new List<Adat_Szatube_Szabadság>();


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

                                Adat_Szatube_Szabadság Adat = new Adat_Szatube_Szabadság(
                                          rekord["sorszám"].ToÉrt_Double(),
                                          rekord["Törzsszám"].ToStrTrim(),
                                          rekord["Dolgozónév"].ToStrTrim(),
                                          rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                          rekord["Befejeződátum"].ToÉrt_DaTeTime(),
                                          rekord["Kivettnap"].ToÉrt_Int(),
                                          rekord["Szabiok"].ToStrTrim(),
                                          rekord["Státus"].ToÉrt_Int(),
                                          rekord["Rögzítette"].ToStrTrim(),
                                          rekord["rögzítésdátum"].ToÉrt_DaTeTime()
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
        public List<Adat_Szatube_Szabadság> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.SzaTuBe_tábla(hely);
            List<Adat_Szatube_Szabadság> Adatok = new List<Adat_Szatube_Szabadság>();
            Adat_Szatube_Szabadság Adat;

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

                                Adat = new Adat_Szatube_Szabadság(
                                          rekord["sorszám"].ToÉrt_Double(),
                                          rekord["Törzsszám"].ToStrTrim(),
                                          rekord["Dolgozónév"].ToStrTrim(),
                                          rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                          rekord["Befejeződátum"].ToÉrt_DaTeTime(),
                                          rekord["Kivettnap"].ToÉrt_Int(),
                                          rekord["Szabiok"].ToStrTrim(),
                                          rekord["Státus"].ToÉrt_Int(),
                                          rekord["Rögzítette"].ToStrTrim(),
                                          rekord["rögzítésdátum"].ToÉrt_DaTeTime()
                                          );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public Adat_Szatube_Szabadság Egy_Adat(string hely, string jelszó, string szöveg)
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.SzaTuBe_tábla(hely);
            Adat_Szatube_Szabadság Adat = null;

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

                            Adat = new Adat_Szatube_Szabadság(
                                      rekord["sorszám"].ToÉrt_Double(),
                                      rekord["Törzsszám"].ToStrTrim(),
                                      rekord["Dolgozónév"].ToStrTrim(),
                                      rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                      rekord["Befejeződátum"].ToÉrt_DaTeTime(),
                                      rekord["Kivettnap"].ToÉrt_Int(),
                                      rekord["Szabiok"].ToStrTrim(),
                                      rekord["Státus"].ToÉrt_Int(),
                                      rekord["Rögzítette"].ToStrTrim(),
                                      rekord["rögzítésdátum"].ToÉrt_DaTeTime()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }

    }
}
