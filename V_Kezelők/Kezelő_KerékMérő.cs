using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Ablakok.MEO;


namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_KerékMérő
    {
        public List<Adat_KerékMérő> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_KerékMérő> Adatok = new List<Adat_KerékMérő>();
            Adat_KerékMérő Adat;

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
                                Adat = new Adat_KerékMérő(
                                        rekord["Pályaszám"].ToStrTrim(),
                                        rekord["Tengely"].ToStrTrim(),
                                        rekord["DátumIdő"].ToÉrt_DaTeTime(),
                                        rekord["A_KKOPJ"].ToÉrt_Double(),
                                        rekord["A_h"].ToÉrt_Double(),
                                        rekord["A_ATM_J"].ToÉrt_Double(),
                                        rekord["A_BETAJ"].ToÉrt_Double(),
                                        rekord["A_NYKMJ"].ToÉrt_Double(),
                                        rekord["A_n"].ToÉrt_Double(),
                                        rekord["A_n2"].ToÉrt_Double(),
                                        rekord["A_KIFUTJ"].ToÉrt_Double(),
                                        rekord["A_V_J"].ToÉrt_Double(),
                                        rekord["A_a"].ToÉrt_Double(),
                                        rekord["A_NYKVJ"].ToÉrt_Double(),
                                        rekord["A_QR_J"].ToÉrt_Double(),
                                        rekord["A_BKOPB"].ToÉrt_Double(),
                                        rekord["A_KKOPB"].ToÉrt_Double(),
                                        rekord["A_hb"].ToÉrt_Double(),
                                        rekord["A_ATM_B"].ToÉrt_Double(),
                                        rekord["A_BETAB"].ToÉrt_Double(),
                                        rekord["A_NYKMB"].ToÉrt_Double(),
                                        rekord["A_nb"].ToÉrt_Double(),
                                        rekord["A_n2b"].ToÉrt_Double(),
                                        rekord["A_KIFUTB"].ToÉrt_Double(),
                                        rekord["A_V_B"].ToÉrt_Double(),
                                        rekord["A_ab"].ToÉrt_Double(),
                                        rekord["A_NYKVB"].ToÉrt_Double(),
                                        rekord["A_QR_B"].ToÉrt_Double(),
                                        rekord["A_HATL_T"].ToÉrt_Double(),
                                        rekord["A_Vt1"].ToÉrt_Double(),
                                        rekord["A_Vt2"].ToÉrt_Double(),
                                        rekord["A_t"].ToÉrt_Double(),
                                        rekord["A_apb_J"].ToÉrt_Double(),
                                        rekord["A_apb_B"].ToÉrt_Double(),
                                        rekord["A_Vt1BKV"].ToÉrt_Double(),
                                        rekord["A_Vt2BKV"].ToÉrt_Double(),
                                        rekord["A_ATM_K"].ToÉrt_Double(),
                                        rekord["A_BKOPJ"].ToÉrt_Double(),
                                        rekord["A_Rf_J"].ToÉrt_Double(),
                                        rekord["A_Rf_B"].ToÉrt_Double(),
                                        rekord["Hiba"].ToStrTrim()
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

    public class Kezelő_KerékmérőTengely
    {
        public List<Adat_KerékmérőTengely> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_KerékmérőTengely> Adatok = new List<Adat_KerékmérőTengely>();
            Adat_KerékmérőTengely Adat;

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
                                Adat = new Adat_KerékmérőTengely(
                                        rekord["Név"].ToStrTrim(),
                                        rekord["SAP"].ToStrTrim()
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
