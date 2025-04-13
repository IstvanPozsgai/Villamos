using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Reklám
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\villamos5.mdb";
        readonly string jelszó = "morecs";

        public Kezelő_Reklám()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Villamostábla5reklám(hely.KönyvSzerk());
        }

        public List<Adat_Reklám> Lista_Adatok()
        {
            List<Adat_Reklám> Adatok = new List<Adat_Reklám>();
            try
            {
                Adat_Reklám Adat;
                string szöveg = $"SELECT * FROM reklámtábla";

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
                                    Adat = new Adat_Reklám(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                        rekord["Befejeződátum"].ToÉrt_DaTeTime(),
                                        rekord["Reklámneve"].ToStrTrim(),
                                        rekord["Viszonylat"].ToStrTrim(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Reklámmérete"].ToStrTrim(),
                                        rekord["Szerelvényben"].ToÉrt_Int(),
                                        rekord["Szerelvény"].ToStrTrim(),
                                        rekord["Ragasztásitilalom"].ToÉrt_DaTeTime(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["Típus"].ToStrTrim()
                                        );
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Lista_Reklám_állomány", ex.StackTrace, ex.Source, ex.HResult);
            }
            return Adatok;
        }

        public List<Adat_Reklám> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Reklám> Adatok = new List<Adat_Reklám>();
            try
            {
                Adat_Reklám Adat;

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
                                    Adat = new Adat_Reklám(
                                        rekord["Azonosító"].ToStrTrim(),
                                        rekord["Kezdődátum"].ToÉrt_DaTeTime(),
                                        rekord["Befejeződátum"].ToÉrt_DaTeTime(),
                                        rekord["Reklámneve"].ToStrTrim(),
                                        rekord["Viszonylat"].ToStrTrim(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Reklámmérete"].ToStrTrim(),
                                        rekord["Szerelvényben"].ToÉrt_Int(),
                                        rekord["Szerelvény"].ToStrTrim(),
                                        rekord["Ragasztásitilalom"].ToÉrt_DaTeTime(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["Típus"].ToStrTrim()
                                        );
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Lista_Reklám_állomány\n" + szöveg, ex.StackTrace, ex.Source, ex.HResult);
            }
            return Adatok;
        }

    }

}
