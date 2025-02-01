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

    public class Kezelő_JogosítványVonal
    {
        readonly string jelszó = "egycsészekávé";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Főmérnökség1.mdb";

        public Kezelő_JogosítványVonal()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Jogosítványtáblalétrehozás(hely.KönyvSzerk());
        }


        public List<Adat_JogosítványVonal> Lista_Adatok()
        {
            List<Adat_JogosítványVonal> Adatok = new List<Adat_JogosítványVonal>();
            Adat_JogosítványVonal Adat;

            string szöveg = $"SELECT * FROM jogosítványvonal";
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
                                Adat = new Adat_JogosítványVonal(
                                    rekord["Sorszám"].ToÉrt_Int(),
                                    rekord["Törzsszám"].ToStrTrim(),
                                    rekord["jogvonalérv"].ToÉrt_DaTeTime(),
                                    rekord["jogvonalmegszerzés"].ToÉrt_DaTeTime(),
                                    rekord["vonalmegnevezés"].ToStrTrim(),
                                    rekord["vonalszám"].ToString(),
                                    rekord["státus"].ToÉrt_Bool()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_JogosítványVonal Adat)
        {
            try
            {
                string szöveg = "INSERT INTO jogosítványvonal ( Sorszám, Törzsszám, Jogvonalérv, Jogvonalmegszerzés, Vonalmegnevezés, Vonalszám, státus)";
                szöveg += $" VALUES ({Sorszám()},";
                szöveg += $"'{Adat.Törzsszám}', ";
                szöveg += $"'{Adat.Jogvonalérv:yyyy.MM.dd}', ";
                szöveg += $"'{Adat.Jogvonalmegszerzés:yyyy.MM.dd}', ";
                szöveg += $"'{Adat.Vonalmegnevezés}', ";
                szöveg += $"'{Adat.Vonalszám}', false )";
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

        public void Módosítás(Adat_JogosítványVonal Adat)
        {
            try
            {
                string szöveg = "UPDATE jogosítványvonal SET ";
                szöveg += $" jogvonalmegszerzés='{Adat.Jogvonalmegszerzés:yyyy.MM.dd}', ";
                szöveg += $" jogvonalérv='{Adat.Jogvonalérv:yyyy.MM.dd}' ";
                szöveg += $" WHERE Törzsszám='{Adat.Törzsszám}'  AND vonalszám='{Adat.Vonalszám}'  AND státus=false";
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

        public void Törlés(Adat_JogosítványVonal Adat)
        {
            try
            {
                string szöveg = "UPDATE jogosítványvonal SET státus=true ";
                szöveg += $" WHERE Törzsszám='{Adat.Törzsszám}'  AND vonalszám='{Adat.Vonalszám}'  AND státus=false";
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

        public int Sorszám()
        {
            int Válasz = 1;
            try
            {
                List<Adat_JogosítványVonal> Adatok = Lista_Adatok();
                if (Adatok.Count > 0) Válasz = Adatok.Max(j => j.Sorszám) + 1;
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
    }

}
