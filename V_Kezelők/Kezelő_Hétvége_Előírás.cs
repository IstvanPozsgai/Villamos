using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Hétvége_Előírás
    {
        readonly string jelszó = "pozsgaii";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\villamos\előírásgyűjteményúj.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kiadáshétvége(hely.KönyvSzerk());
        }

        public List<Adat_Hétvége_Előírás> Lista_Adatok(string Telephely)
        {
            FájlBeállítás(Telephely);
            string szöveg = "Select * FROM előírás ORDER BY id";
            List<Adat_Hétvége_Előírás> Adatok = new List<Adat_Hétvége_Előírás>();
            Adat_Hétvége_Előírás Adat;

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
                                Adat = new Adat_Hétvége_Előírás(
                                        rekord["id"].ToÉrt_Long(),
                                        rekord["vonal"].ToStrTrim(),
                                        rekord["Mennyiség"].ToÉrt_Long(),
                                        rekord["red"].ToÉrt_Int(),
                                        rekord["green"].ToÉrt_Int(),
                                        rekord["blue"].ToÉrt_Int()
                                       );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string Telephely, Adat_Hétvége_Előírás Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = "UPDATE előírás SET ";
                szöveg += $" vonal='{Adat.Vonal}', ";
                szöveg += $" mennyiség={Adat.Mennyiség},  ";
                szöveg += $" red={Adat.Red},  ";
                szöveg += $" green={Adat.Green},  ";
                szöveg += $" blue={Adat.Blue}";
                szöveg += $" WHERE id={Adat.Id}";
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
                                Adat = new Adat_Hétvége_Előírás(
                                        rekord["id"].ToÉrt_Long(),
                                        rekord["vonal"].ToStrTrim(),
                                        rekord["Mennyiség"].ToÉrt_Long(),
                                        rekord["red"].ToÉrt_Int(),
                                        rekord["green"].ToÉrt_Int(),
                                        rekord["blue"].ToÉrt_Int()
                                       );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
        }
    }

}
