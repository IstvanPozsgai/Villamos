using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{


    public class Kezelő_Reklám_Napló
    {
        public List<Adat_Reklám_Napló> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Reklám_Napló> Adatok = new List<Adat_Reklám_Napló>();
            try
            {
                Adat_Reklám_Napló Adat;

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
                                    Adat = new Adat_Reklám_Napló(
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
                                        rekord["Típus"].ToStrTrim(),
                                        rekord["ID"].ToÉrt_Long(),
                                        rekord["Mikor"].ToÉrt_DaTeTime(),
                                        rekord["Módosító"].ToStrTrim()
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
