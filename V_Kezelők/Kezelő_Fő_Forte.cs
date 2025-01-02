using System;
using System.Collections.Generic;
using System.Data.OleDb;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Villamos_Kezelők
{
    public class Kezelő_Fő_Forte
    {

        private void Hibaellenőr(Adat_Fő_Forte Adat)
        {
            string hibák = "";

            if (hibák.Length > 0)
                throw new Exception(hibák);
        }



        public List<Adat_Fő_Forte> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            Adat_Fő_Forte Adat = null;
            List<Adat_Fő_Forte> Adatok = new List<Adat_Fő_Forte>();

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
                                Adat = new Adat_Fő_Forte(
                                    rekord["Dátum"].ToÉrt_DaTeTime(),
                                    rekord["Napszak"].ToStrTrim(),
                                    rekord["TelephelyForte"].ToStrTrim(),
                                    rekord["Típusforte"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["kiadás"].ToÉrt_Int(),
                                    rekord["munkanap"].ToÉrt_Int()
                                     );

                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public Adat_Fő_Forte Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Fő_Forte Adat = null;


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
                            Adat = new Adat_Fő_Forte(
                               rekord["Dátum"].ToÉrt_DaTeTime(),
                               rekord["Napszak"].ToStrTrim(),
                               rekord["TelephelyForte"].ToStrTrim(),
                               rekord["Típusforte"].ToStrTrim(),
                               rekord["telephely"].ToStrTrim(),
                               rekord["típus"].ToStrTrim(),
                               rekord["kiadás"].ToÉrt_Int(),
                               rekord["munkanap"].ToÉrt_Int()
                                );
                        }
                    }
                }
            }
            return Adat;
        }


        public void Rögzít_Fő_forte(string hely, string jelszó, Adat_Fő_Forte Adat)
        {
            Hibaellenőr(Adat);

            string szöveg = "INSERT INTO fortekiadástábla  (dátum, napszak, telephelyforte, típusforte, telephely, típus, kiadás, munkanap  ) VALUES (";
            szöveg += "'" + Adat.Dátum.ToString("yyyy.MM.dd") + "', ";
            szöveg += "'" + Adat.Napszak.ToString() + "', ";
            szöveg += "'" + Adat.Telephelyforte.ToString() + "', ";
            szöveg += "'" + Adat.Típusforte.ToString() + "', ";
            szöveg += "'" + Adat.Telephely.ToString() + "', ";
            szöveg += "'" + Adat.Típus.ToString() + "', ";
            szöveg += Adat.Kiadás.ToString() + ", ";
            szöveg += Adat.Munkanap.ToString() + ") ";

            Adatbázis.ABMódosítás(hely, jelszó, szöveg);
        }
    }
}
