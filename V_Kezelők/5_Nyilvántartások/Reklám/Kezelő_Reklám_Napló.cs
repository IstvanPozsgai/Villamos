using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos.Kezelők
{
    public class Kezelő_Reklám_Napló
    {
        readonly string jelszó = "morecs";
        readonly string táblanév = "reklámtábla";
        string hely;

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Napló\Reklámnapló{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Villamostábla5reklámnapló(hely);
        }

        public List<Adat_Reklám_Napló> Lista_Adatok(int Év)
        {
            List<Adat_Reklám_Napló> Adatok = new List<Adat_Reklám_Napló>();
            FájlBeállítás(Év);
            Adat_Reklám_Napló Adat;
            string szöveg = $"SELECT * FROM {táblanév}";
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
            return Adatok;
        }

        public void Rögzítés(int Év, Adat_Reklám_Napló Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"INSERT INTO {táblanév}  (azonosító, kezdődátum, befejeződátum, reklámneve, viszonylat, telephely, reklámmérete,";
                szöveg += " ragasztásitilalom, szerelvény, szerelvényben, megjegyzés, típus, id, módosító, mikor   ) VALUES (";
                szöveg += $"'{Adat.Azonosító}',"; //azonosító
                szöveg += $" '{Adat.Kezdődátum:yyyy.MM.dd}',";               //       kezdődátum
                szöveg += $" '{Adat.Befejeződátum:yyyy.MM.dd}',";               //        befejeződátum
                szöveg += $" '{MyF.Szöveg_Tisztítás(Adat.Reklámneve)}',"; //        reklámneve
                szöveg += $" '{Adat.Viszonylat}',";                        //        viszonylat
                szöveg += $"'{Adat.Telephely}',";   //        telephely
                szöveg += $" '{Adat.Reklámmérete}',";                        //        reklámmérete
                szöveg += $"'{Adat.Ragasztásitilalom:yyyy.MM.dd}',";//        ragasztásitilalom
                szöveg += $" '{Adat.Szerelvény}',"; //        szerelvény
                szöveg += $" {Adat.Szerelvényben},";                          //        szerelvényben
                szöveg += $"  '{MyF.Szöveg_Tisztítás(Adat.Megjegyzés)}', ";       //        megjegyzés
                szöveg += $"'{Adat.Típus}', ";      //        típus
                szöveg += $"{Sorszám(Év)},";                    //        id
                szöveg += $" '{Adat.Módosító}',";   //    módosító
                szöveg += $" '{Adat.Mikor}')";               //    mikor
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

        private long Sorszám(int Év)
        {
            long válasz = 1;
            try
            {
                List<Adat_Reklám_Napló> Adatok = Lista_Adatok(Év);
                if (Adatok.Count > 0) válasz = Adatok.Max(a => a.Id) + 1;
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
            return válasz;
        }
    }
}
