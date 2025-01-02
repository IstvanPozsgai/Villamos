using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Eszköz
    {

        public List<Adat_Eszköz> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Eszköz> Adatok = new List<Adat_Eszköz>();
            Adat_Eszköz Adat;

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
                                Adat = new Adat_Eszköz(
                                       rekord["Eszköz"].ToStrTrim(),
                                       rekord["Alszám"].ToStrTrim(),
                                       rekord["Megnevezés"].ToStrTrim(),
                                       rekord["Megnevezés_folyt"].ToStrTrim(),
                                       rekord["Gyártási_szám"].ToStrTrim(),
                                       rekord["Leltárszám"].ToStrTrim(),
                                       rekord["Leltár_dátuma"].ToÉrt_DaTeTime(),
                                       rekord["Mennyiség"].ToÉrt_Double(),
                                       rekord["Bázis_menny_egység"].ToStrTrim(),
                                       rekord["Aktiválás_dátuma"].ToÉrt_DaTeTime(),
                                       rekord["Telephely"].ToStrTrim(),
                                       rekord["Telephely_megnevezése"].ToStrTrim(),
                                       rekord["Helyiség"].ToStrTrim(),
                                       rekord["Helyiség_megnevezés"].ToStrTrim(),
                                       rekord["Gyár"].ToStrTrim(),
                                       rekord["Leltári_költséghely"].ToStrTrim(),
                                       rekord["Vonalkód"].ToStrTrim(),
                                       rekord["Leltár_forduló_nap"].ToÉrt_DaTeTime(),
                                       rekord["Szemügyi_törzsszám"].ToStrTrim(),
                                       rekord["Dolgozó_neve"].ToStrTrim(),
                                       rekord["Deaktiválás_dátuma"].ToÉrt_DaTeTime(),
                                       rekord["Eszközosztály"].ToStrTrim(),
                                       rekord["Üzletág"].ToStrTrim(),
                                       rekord["Cím"].ToStrTrim(),
                                       rekord["Költséghely"].ToStrTrim(),
                                       rekord["Felelős_költséghely"].ToStrTrim(),
                                       rekord["Régi_leltárszám"].ToStrTrim(),
                                       rekord["Vonalkódozható"].ToÉrt_Bool(),
                                       rekord["Rendszám_pályaszám"].ToStrTrim(),
                                       rekord["Épület_Szerszám"].ToStrTrim(),
                                       rekord["Épület_van"].ToÉrt_Bool(),
                                       rekord["Szerszám_van"].ToÉrt_Bool(),
                                       rekord["Státus"].ToÉrt_Bool()
                                       );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<string> Lista_EszközNév(string hely, string jelszó, string szöveg)
        {
            List<string> Adatok = new List<string>();
            string Adat;

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
                                Adat = rekord["Eszköz"].ToStrTrim();
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Eszköz Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Eszköz Adat = null;

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
                                Adat = new Adat_Eszköz(
                                       rekord["Eszköz"].ToStrTrim(),
                                       rekord["Alszám"].ToStrTrim(),
                                       rekord["Megnevezés"].ToStrTrim(),
                                       rekord["Megnevezés_folyt"].ToStrTrim(),
                                       rekord["Gyártási_szám"].ToStrTrim(),
                                       rekord["Leltárszám"].ToStrTrim(),
                                       rekord["Leltár_dátuma"].ToÉrt_DaTeTime(),
                                       rekord["Mennyiség"].ToÉrt_Double(),
                                       rekord["Bázis_menny_egység"].ToStrTrim(),
                                       rekord["Aktiválás_dátuma"].ToÉrt_DaTeTime(),
                                       rekord["Telephely"].ToStrTrim(),
                                       rekord["Telephely_megnevezése"].ToStrTrim(),
                                       rekord["Helyiség"].ToStrTrim(),
                                       rekord["Helyiség_megnevezés"].ToStrTrim(),
                                       rekord["Gyár"].ToStrTrim(),
                                       rekord["Leltári_költséghely"].ToStrTrim(),
                                       rekord["Vonalkód"].ToStrTrim(),
                                       rekord["Leltár_forduló_nap"].ToÉrt_DaTeTime(),
                                       rekord["Szemügyi_törzsszám"].ToStrTrim(),
                                       rekord["Dolgozó_neve"].ToStrTrim(),
                                       rekord["Deaktiválás_dátuma"].ToÉrt_DaTeTime(),
                                       rekord["Eszközosztály"].ToStrTrim(),
                                       rekord["Üzletág"].ToStrTrim(),
                                       rekord["Cím"].ToStrTrim(),
                                       rekord["Költséghely"].ToStrTrim(),
                                       rekord["Felelős_költséghely"].ToStrTrim(),
                                       rekord["Régi_leltárszám"].ToStrTrim(),
                                       rekord["Vonalkódozható"].ToÉrt_Bool(),
                                       rekord["Rendszám_pályaszám"].ToStrTrim(),
                                       rekord["Épület_Szerszám"].ToStrTrim(),
                                       rekord["Épület_van"].ToÉrt_Bool(),
                                       rekord["Szerszám_van"].ToÉrt_Bool(),
                                       rekord["Státus"].ToÉrt_Bool()
                                       );
                            }
                        }
                    }
                }
            }
            return Adat;
        }
    }
}
