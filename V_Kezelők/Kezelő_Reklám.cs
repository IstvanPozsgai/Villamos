using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

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

        public void Módosítás(Adat_Reklám Adat)
        {
            try
            {
                string szöveg = "UPDATE reklámtábla  SET ";
                szöveg += $"kezdődátum='{Adat.Kezdődátum:yyyy.MM.dd}', ";
                szöveg += $"befejeződátum='{Adat.Befejeződátum:yyyy.MM.dd}', ";
                szöveg += $"reklámneve='{MyF.Szöveg_Tisztítás(Adat.Reklámneve)}', ";
                szöveg += $"viszonylat='{Adat.Viszonylat}', ";
                szöveg += $"telephely='{Adat.Telephely}', ";
                szöveg += $"reklámmérete='{Adat.Reklámmérete}', ";
                szöveg += $"ragasztásitilalom='{Adat.Ragasztásitilalom:yyyy.MM.dd}', ";
                szöveg += $"szerelvényben={Adat.Szerelvényben}, ";
                szöveg += $"szerelvény='{Adat.Szerelvény}', ";
                szöveg += $"megjegyzés=' {MyF.Szöveg_Tisztítás(Adat.Megjegyzés)}', ";
                szöveg += $"típus='{Adat.Típus}' ";
                szöveg += $" WHERE [azonosító]='{Adat.Azonosító}'";
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

        public void RögzítésTilalom(Adat_Reklám Adat)
        {
            try
            {
                string szöveg = "INSERT INTO reklámtábla  (azonosító, kezdődátum, befejeződátum, reklámneve, viszonylat, telephely, reklámmérete,";
                szöveg += " ragasztásitilalom, szerelvény, szerelvényben, megjegyzés, típus) VALUES (";
                szöveg += $"'{Adat.Azonosító}', '2000.01.01', '2000.01.01', '*', '*',";
                szöveg += $"'{Adat.Telephely}', '*',";
                szöveg += $"'{Adat.Ragasztásitilalom:yyyy.MM.dd}'', '*', 0,  '*', ";
                szöveg += $"'{Adat.Típus}')";
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

        public void MódosításTilalom(Adat_Reklám Adat)
        {
            try
            {
                string szöveg = "UPDATE reklámtábla  SET ";
                szöveg += $"ragasztásitilalom='{Adat.Ragasztásitilalom:yyyy.MM.dd}' ";
                szöveg += $" WHERE [azonosító]='{Adat.Azonosító}'";
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
}
