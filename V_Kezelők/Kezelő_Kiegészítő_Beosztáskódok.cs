using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Beosztáskódok
    {
        readonly string jelszó = "Mocó";

        public List<Adat_Kiegészítő_Beosztáskódok> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * FROM beosztáskódok Order By  sorszám";
            List<Adat_Kiegészítő_Beosztáskódok> Adatok = new List<Adat_Kiegészítő_Beosztáskódok>();
            Adat_Kiegészítő_Beosztáskódok Adat;

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
                                Adat = new Adat_Kiegészítő_Beosztáskódok(
                                        rekord["Sorszám"].ToÉrt_Long(),
                                        rekord["Beosztáskód"].ToStrTrim(),
                                        rekord["Munkaidőkezdet"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidővége"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidő"].ToÉrt_Int(),
                                        rekord["Munkarend"].ToÉrt_Int(),
                                        rekord["Napszak"].ToStrTrim(),
                                        rekord["Éjszakás"].ToÉrt_Bool(),
                                        rekord["Számoló"].ToÉrt_Bool(),
                                        rekord["0"].ToÉrt_Int(),
                                        rekord["1"].ToÉrt_Int(),
                                        rekord["2"].ToÉrt_Int(),
                                        rekord["3"].ToÉrt_Int(),
                                        rekord["4"].ToÉrt_Int(),
                                        rekord["5"].ToÉrt_Int(),
                                        rekord["6"].ToÉrt_Int(),
                                        rekord["7"].ToÉrt_Int(),
                                        rekord["8"].ToÉrt_Int(),
                                        rekord["9"].ToÉrt_Int(),
                                        rekord["10"].ToÉrt_Int(),
                                        rekord["11"].ToÉrt_Int(),
                                        rekord["12"].ToÉrt_Int(),
                                        rekord["13"].ToÉrt_Int(),
                                        rekord["14"].ToÉrt_Int(),
                                        rekord["15"].ToÉrt_Int(),
                                        rekord["16"].ToÉrt_Int(),
                                        rekord["17"].ToÉrt_Int(),
                                        rekord["18"].ToÉrt_Int(),
                                        rekord["19"].ToÉrt_Int(),
                                        rekord["20"].ToÉrt_Int(),
                                        rekord["21"].ToÉrt_Int(),
                                        rekord["22"].ToÉrt_Int(),
                                        rekord["23"].ToÉrt_Int(),
                                        rekord["Magyarázat"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Kiegészítő_Beosztáskódok> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Beosztáskódok> Adatok = new List<Adat_Kiegészítő_Beosztáskódok>();
            Adat_Kiegészítő_Beosztáskódok Adat;

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
                                Adat = new Adat_Kiegészítő_Beosztáskódok(
                                        rekord["Sorszám"].ToÉrt_Long(),
                                        rekord["Beosztáskód"].ToStrTrim(),
                                        rekord["Munkaidőkezdet"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidővége"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidő"].ToÉrt_Int(),
                                        rekord["Munkarend"].ToÉrt_Int(),
                                        rekord["Napszak"].ToStrTrim(),
                                        rekord["Éjszakás"].ToÉrt_Bool(),
                                        rekord["Számoló"].ToÉrt_Bool(),
                                        rekord["0"].ToÉrt_Int(),
                                        rekord["1"].ToÉrt_Int(),
                                        rekord["2"].ToÉrt_Int(),
                                        rekord["3"].ToÉrt_Int(),
                                        rekord["4"].ToÉrt_Int(),
                                        rekord["5"].ToÉrt_Int(),
                                        rekord["6"].ToÉrt_Int(),
                                        rekord["7"].ToÉrt_Int(),
                                        rekord["8"].ToÉrt_Int(),
                                        rekord["9"].ToÉrt_Int(),
                                        rekord["10"].ToÉrt_Int(),
                                        rekord["11"].ToÉrt_Int(),
                                        rekord["12"].ToÉrt_Int(),
                                        rekord["13"].ToÉrt_Int(),
                                        rekord["14"].ToÉrt_Int(),
                                        rekord["15"].ToÉrt_Int(),
                                        rekord["16"].ToÉrt_Int(),
                                        rekord["17"].ToÉrt_Int(),
                                        rekord["18"].ToÉrt_Int(),
                                        rekord["19"].ToÉrt_Int(),
                                        rekord["20"].ToÉrt_Int(),
                                        rekord["21"].ToÉrt_Int(),
                                        rekord["22"].ToÉrt_Int(),
                                        rekord["23"].ToÉrt_Int(),
                                        rekord["Magyarázat"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Kiegészítő_Beosztáskódok Egy_Adat(string hely, string jelszó, string szöveg)
        {

            Adat_Kiegészítő_Beosztáskódok Adat = null;

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

                            Adat = new Adat_Kiegészítő_Beosztáskódok(
                                        rekord["Sorszám"].ToÉrt_Long(),
                                        rekord["Beosztáskód"].ToStrTrim(),
                                        rekord["Munkaidőkezdet"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidővége"].ToÉrt_DaTeTime(),
                                        rekord["Munkaidő"].ToÉrt_Int(),
                                        rekord["Munkarend"].ToÉrt_Int(),
                                        rekord["Napszak"].ToStrTrim(),
                                        rekord["Éjszakás"].ToÉrt_Bool(),
                                        rekord["Számoló"].ToÉrt_Bool(),
                                        rekord["0"].ToÉrt_Int(),
                                        rekord["1"].ToÉrt_Int(),
                                        rekord["2"].ToÉrt_Int(),
                                        rekord["3"].ToÉrt_Int(),
                                        rekord["4"].ToÉrt_Int(),
                                        rekord["5"].ToÉrt_Int(),
                                        rekord["6"].ToÉrt_Int(),
                                        rekord["7"].ToÉrt_Int(),
                                        rekord["8"].ToÉrt_Int(),
                                        rekord["9"].ToÉrt_Int(),
                                        rekord["10"].ToÉrt_Int(),
                                        rekord["11"].ToÉrt_Int(),
                                        rekord["12"].ToÉrt_Int(),
                                        rekord["13"].ToÉrt_Int(),
                                        rekord["14"].ToÉrt_Int(),
                                        rekord["15"].ToÉrt_Int(),
                                        rekord["16"].ToÉrt_Int(),
                                        rekord["17"].ToÉrt_Int(),
                                        rekord["18"].ToÉrt_Int(),
                                        rekord["19"].ToÉrt_Int(),
                                        rekord["20"].ToÉrt_Int(),
                                        rekord["21"].ToÉrt_Int(),
                                        rekord["22"].ToÉrt_Int(),
                                        rekord["23"].ToÉrt_Int(),
                                        rekord["Magyarázat"].ToStrTrim()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }

        public void Rögzítés(string hely, Adat_Kiegészítő_Beosztáskódok Adat)
        {
            try
            {
                string szöveg = "INSERT INTO beosztáskódok (sorszám, beosztáskód, munkaidőkezdet, munkaidővége,  munkaidő, munkarend, napszak, éjszakás, számoló, 0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23, Magyarázat)";
                szöveg += " VALUES (";
                szöveg += $" {Adat.Sorszám}, ";                            //  sorszám
                szöveg += $"'{Adat.Beosztáskód}', ";                       //  beosztáskód
                szöveg += $"'{Adat.Munkaidőkezdet:HH:mm:ss}', ";           //  munkaidőkezdet
                szöveg += $"'{Adat.Munkaidővége:HH:mm:ss}', ";             //  munkaidővége
                szöveg += $" {Adat.Munkaidő}, ";                           //  munkaidő
                szöveg += $" {Adat.Munkarend},";                           //  munkarend
                szöveg += $"'_', ";                                      //  napszak
                szöveg += $" {Adat.Éjszakás}, ";                           //  éjszakás
                szöveg += $" {Adat.Számoló}, ";                            //  számoló
                szöveg += " 0,0,0,0,0,0,0,0,0,0, 0,0,0,0,0,0,0,0,0,0, 0,0,0,0, ";
                szöveg += $" '{Adat.Magyarázat}') ";                       //   Magyarázat
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

        public void Módosítás(string hely, Adat_Kiegészítő_Beosztáskódok Adat)
        {
            try
            {
                string szöveg = "UPDATE beosztáskódok SET ";
                szöveg += $" beosztáskód='{Adat.Beosztáskód}', ";
                szöveg += $" munkaidőkezdet='{Adat.Munkaidőkezdet:HH:mm:ss}', ";
                szöveg += $" munkaidővége='{Adat.Munkaidővége:HH:mm:ss}', ";
                szöveg += $" munkaidő={Adat.Munkaidő}, ";
                szöveg += $" munkarend={Adat.Munkarend}, ";
                szöveg += $" éjszakás={Adat.Éjszakás}, ";
                szöveg += $" számoló={Adat.Számoló}, ";
                szöveg += $" Magyarázat='{Adat.Magyarázat}' ";
                szöveg += $" WHERE  sorszám={Adat.Sorszám} ";
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

        public void Törlés(string hely, string BeoKód)
        {
            try
            {
                string szöveg = $"DELETE FROM beosztáskódok where beosztáskód='{BeoKód}'";
                MyA.ABtörlés(hely, jelszó, szöveg);
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

}
