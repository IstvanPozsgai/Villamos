using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
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
            string szöveg = $"SELECT * FROM előírás ORDER BY id";
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
                string szöveg = $"UPDATE előírás SET ";
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

        public void Rögzítés(string Telephely, Adat_Hétvége_Előírás Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"INSERT INTO előírás (id, vonal, mennyiség, red, green, blue ) VALUES (";
                szöveg += $"{Sorszám(Telephely)}, ";
                szöveg += $"'{Adat.Vonal}', ";
                szöveg += $"{Adat.Mennyiség}, ";
                szöveg += $"{Adat.Red}, ";
                szöveg += $"{Adat.Green}, ";
                szöveg += $"{Adat.Blue})";
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

        public void Törlés(string Telephely, long Id)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"DELETE FROM előírás where id={Id}";
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

        public long Sorszám(string Telephely)
        {
            long Válasz = 1;
            try
            {
                FájlBeállítás(Telephely);
                List<Adat_Hétvége_Előírás> Adatok = Lista_Adatok(Telephely);
                if (Adatok.Count > 0) Válasz = Adatok.Max(a => a.Id) + 1;

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
            return Válasz;
        }

        public void Csere(string Telephely, long Id)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<Adat_Hétvége_Előírás> Adatok = Lista_Adatok(Telephely);
                long ElőzőId = 0;
                foreach (Adat_Hétvége_Előírás Elem in Adatok)
                {
                    if (Elem.Id == Id) break;
                    ElőzőId = Elem.Id;
                }

                string szöveg = $"UPDATE előírás SET id={0} WHERE id={Id}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                szöveg = $"UPDATE előírás SET id={Id} WHERE id={ElőzőId}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                szöveg = $"UPDATE előírás SET id={ElőzőId} WHERE id={0}";
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
