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
    public class Kezelő_JogosítványTípus
    {
        readonly string jelszó = "egycsészekávé";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Főmérnökség1.mdb";

        public Kezelő_JogosítványTípus()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Jogosítványtáblalétrehozás(hely.KönyvSzerk());
        }

        public List<Adat_JogosítványTípus> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM jogosítványtípus";
            List<Adat_JogosítványTípus> Adatok = new List<Adat_JogosítványTípus>();
            Adat_JogosítványTípus Adat;

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
                                Adat = new Adat_JogosítványTípus(
                                    rekord["Sorszám"].ToÉrt_Int(),
                                    rekord["Törzsszám"].ToStrTrim(),
                                    rekord["jogtípus"].ToStrTrim(),
                                    rekord["jogtípusérvényes"].ToÉrt_DaTeTime(),
                                    rekord["jogtípusmegszerzés"].ToÉrt_DaTeTime(),
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

        public void Rögzítés(Adat_JogosítványTípus Adat)
        {
            try
            {
                string szöveg = "INSERT INTO jogosítványtípus (Sorszám, Törzsszám, jogtípus, jogtípusérvényes, jogtípusmegszerzés, státus)";
                szöveg += $" VALUES ({Sorszám()}, ";
                szöveg += $"'{Adat.Törzsszám}', ";
                szöveg += $"'{Adat.Jogtípus}', ";
                szöveg += $"'{Adat.Jogtípusérvényes:yyyy.MM.dd}', ";
                szöveg += $"'{Adat.Jogtípusmegszerzés:yyyy.MM.dd}', false )";
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

        public void Módosítás(Adat_JogosítványTípus Adat)
        {
            try
            {
                string szöveg = "UPDATE jogosítványtípus SET ";
                szöveg += $" jogtípusmegszerzés='{Adat.Jogtípusmegszerzés:yyyy.MM.dd}', ";
                szöveg += $" jogtípusérvényes='{Adat.Jogtípusérvényes:yyyy.MM.dd}' ";
                szöveg += $" WHERE Törzsszám='{Adat.Törzsszám}' AND jogtípus='{Adat.Jogtípus}' AND státus=false";
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

        public void Törlés(Adat_JogosítványTípus Adat)
        {
            try
            {
                string szöveg = "UPDATE jogosítványtípus SET státus=true ";
                szöveg += $" WHERE Törzsszám='{Adat.Törzsszám}' AND jogtípus='{Adat.Jogtípus}' AND státus=false";
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
                List<Adat_JogosítványTípus> Adatok = Lista_Adatok();
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
