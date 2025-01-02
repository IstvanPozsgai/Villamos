using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;


namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Osztály_Adat
    {
        public List<Adat_Osztály_Adat> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Osztály_Adat> Adatok = new List<Adat_Osztály_Adat>();
            Adat_Osztály_Adat Adat;

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
                                Adat = new Adat_Osztály_Adat(
                                    rekord["Azonosító"].ToStrTrim (),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["altípus"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["szolgálat"].ToStrTrim(),

                                    rekord["Adat1"].ToStrTrim(),
                                    rekord["Adat2"].ToStrTrim(),
                                    rekord["Adat3"].ToStrTrim(),
                                    rekord["Adat4"].ToStrTrim(),
                                    rekord["Adat5"].ToStrTrim(),

                                    rekord["Adat6"].ToStrTrim(),
                                    rekord["Adat7"].ToStrTrim(),
                                    rekord["Adat8"].ToStrTrim(),
                                    rekord["Adat9"].ToStrTrim(),
                                    rekord["Adat10"].ToStrTrim(),

                                    rekord["Adat11"].ToStrTrim(),
                                    rekord["Adat12"].ToStrTrim(),
                                    rekord["Adat13"].ToStrTrim(),
                                    rekord["Adat14"].ToStrTrim(),
                                    rekord["Adat15"].ToStrTrim(),

                                    rekord["Adat16"].ToStrTrim(),
                                    rekord["Adat17"].ToStrTrim(),
                                    rekord["Adat18"].ToStrTrim(),
                                    rekord["Adat19"].ToStrTrim(),
                                    rekord["Adat20"].ToStrTrim(),

                                    rekord["Adat21"].ToStrTrim(),
                                    rekord["Adat22"].ToStrTrim(),
                                    rekord["Adat23"].ToStrTrim(),
                                    rekord["Adat24"].ToStrTrim(),
                                    rekord["Adat25"].ToStrTrim(),

                                    rekord["Adat26"].ToStrTrim(),
                                    rekord["Adat27"].ToStrTrim(),
                                    rekord["Adat28"].ToStrTrim(),
                                    rekord["Adat29"].ToStrTrim(),
                                    rekord["Adat30"].ToStrTrim(),

                                    rekord["Adat31"].ToStrTrim(),
                                    rekord["Adat32"].ToStrTrim(),
                                    rekord["Adat33"].ToStrTrim(),
                                    rekord["Adat34"].ToStrTrim(),
                                    rekord["Adat35"].ToStrTrim(),

                                    rekord["Adat36"].ToStrTrim(),
                                    rekord["Adat37"].ToStrTrim(),
                                    rekord["Adat38"].ToStrTrim(),
                                    rekord["Adat39"].ToStrTrim(),
                                    rekord["Adat40"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Osztály_Adat Egy_Adat(string hely, string jelszó, string szöveg)
        {
          
            Adat_Osztály_Adat Adat=null;

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
                            
                                Adat = new Adat_Osztály_Adat(
                                    rekord["Azonosító"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["altípus"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["szolgálat"].ToStrTrim(),

                                    rekord["Adat1"].ToStrTrim(),
                                    rekord["Adat2"].ToStrTrim(),
                                    rekord["Adat3"].ToStrTrim(),
                                    rekord["Adat4"].ToStrTrim(),
                                    rekord["Adat5"].ToStrTrim(),

                                    rekord["Adat6"].ToStrTrim(),
                                    rekord["Adat7"].ToStrTrim(),
                                    rekord["Adat8"].ToStrTrim(),
                                    rekord["Adat9"].ToStrTrim(),
                                    rekord["Adat10"].ToStrTrim(),

                                    rekord["Adat11"].ToStrTrim(),
                                    rekord["Adat12"].ToStrTrim(),
                                    rekord["Adat13"].ToStrTrim(),
                                    rekord["Adat14"].ToStrTrim(),
                                    rekord["Adat15"].ToStrTrim(),

                                    rekord["Adat16"].ToStrTrim(),
                                    rekord["Adat17"].ToStrTrim(),
                                    rekord["Adat18"].ToStrTrim(),
                                    rekord["Adat19"].ToStrTrim(),
                                    rekord["Adat20"].ToStrTrim(),

                                    rekord["Adat21"].ToStrTrim(),
                                    rekord["Adat22"].ToStrTrim(),
                                    rekord["Adat23"].ToStrTrim(),
                                    rekord["Adat24"].ToStrTrim(),
                                    rekord["Adat25"].ToStrTrim(),

                                    rekord["Adat26"].ToStrTrim(),
                                    rekord["Adat27"].ToStrTrim(),
                                    rekord["Adat28"].ToStrTrim(),
                                    rekord["Adat29"].ToStrTrim(),
                                    rekord["Adat30"].ToStrTrim(),

                                    rekord["Adat31"].ToStrTrim(),
                                    rekord["Adat32"].ToStrTrim(),
                                    rekord["Adat33"].ToStrTrim(),
                                    rekord["Adat34"].ToStrTrim(),
                                    rekord["Adat35"].ToStrTrim(),

                                    rekord["Adat36"].ToStrTrim(),
                                    rekord["Adat37"].ToStrTrim(),
                                    rekord["Adat38"].ToStrTrim(),
                                    rekord["Adat39"].ToStrTrim(),
                                    rekord["Adat40"].ToStrTrim()
                                    );

                        }
                    }
                }
            }
            return Adat;
        }



    }

    public class Kezelő_Osztály_Név
    {
        public List<Adat_Osztály_Név> Lista_Adat(string hely, string jelszó, string szöveg)
        {
            List<Adat_Osztály_Név> Adatok = new List<Adat_Osztály_Név>();
            Adat_Osztály_Név Adat;

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
                                Adat = new Adat_Osztály_Név(
                                     MyF.Érték_INT(rekord["id"].ToStrTrim()),
                                     rekord["Osztálynév"].ToStrTrim(),
                                     rekord["Osztálymező"].ToStrTrim(),
                                     rekord["Használatban"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        /// <summary>
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Osztály_Név Adat)
        {
            try
            {
                string szöveg = "UPDATE  osztálytábla SET";
                szöveg += $" osztálynév='{Adat.Osztálynév}', ";
                szöveg += $" osztálymező='{Adat.Osztálymező}', ";
                szöveg += $" használatban='{Adat.Használatban}' ";
                szöveg += $" where id={Adat.Id} ";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }

    public class Kezelő_Osztály_Adat_Szum
    {
        public List<Adat_Osztály_Adat_Szum> Lista_Adat(string hely, string jelszó, string szöveg, string mező)
        {
            List<Adat_Osztály_Adat_Szum> Adatok = new List<Adat_Osztály_Adat_Szum>();
            Adat_Osztály_Adat_Szum Adat;

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
                                Adat = new Adat_Osztály_Adat_Szum(
                                    rekord["altípus"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),                           
                                    rekord[mező].ToStrTrim(),
                                    rekord["Összeg"].ToÉrt_Int()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Osztály_Adat_Szum> Lista_Adat1(string hely, string jelszó, string szöveg, string mező)
        {
            List<Adat_Osztály_Adat_Szum> Adatok = new List<Adat_Osztály_Adat_Szum>();
            Adat_Osztály_Adat_Szum Adat;

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
                                Adat = new Adat_Osztály_Adat_Szum(
                                    rekord[mező].ToStrTrim(),
                                    rekord["Összeg"].ToÉrt_Int()
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
