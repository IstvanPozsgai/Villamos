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
    public class Kezelő_OktatásTábla
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Főmérnökség_oktatás.mdb";
        readonly string jelszó = "pázmányt";

        public Kezelő_OktatásTábla()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Oktatás_ALAP(hely.KönyvSzerk());
        }

        public List<Adat_OktatásTábla> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM Oktatástábla ";
            List<Adat_OktatásTábla> Adatok = new List<Adat_OktatásTábla>();
            Adat_OktatásTábla Adat;

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
                                Adat = new Adat_OktatásTábla(
                                    rekord["IDoktatás"].ToÉrt_Long(),
                                    rekord["Téma"].ToStrTrim(),
                                    rekord["Kategória"].ToStrTrim(),
                                    rekord["gyakoriság"].ToStrTrim(),
                                    rekord["státus"].ToStrTrim(),
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["listázásisorrend"].ToÉrt_Long(),
                                    rekord["ismétlődés"].ToÉrt_Long(),
                                    rekord["PDFfájl"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_OktatásTábla Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO Oktatástábla ( IDoktatás, Téma, Kategória, gyakoriság, ismétlődés, státus, dátum, telephely,listázásisorrend, pdffájl )";
                szöveg += $" VALUES ( {Sorszám()}, ";       //IDoktatás
                szöveg += $"'{Adat.Téma}', ";                   //    Téma
                szöveg += $"'{Adat.Kategória}', ";    //    Kategória
                szöveg += $"'{Adat.Gyakoriság}', ";   //    gyakoriság 
                szöveg += $"{Adat.Ismétlődés}, ";        //    ismétlődés
                szöveg += $"'{Adat.Státus}', ";       //    státus
                szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', ";   //    dátum
                szöveg += $"'{Adat.Telephely}', ";    //    telephely
                szöveg += $"{Adat.Listázásisorrend}, ";                             //    listázásisorrend
                szöveg += $"'{Adat.PDFfájl}' )";      //    pdffájl

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

        public void Módosítás(Adat_OktatásTábla Adat)
        {
            try
            {
                string szöveg = $"UPDATE Oktatástábla SET ";
                szöveg += $" téma='{Adat.Téma}', ";
                szöveg += $" kategória='{Adat.Kategória}', ";
                szöveg += $" gyakoriság='{Adat.Gyakoriság}', ";
                szöveg += $" ismétlődés={Adat.Ismétlődés}, ";
                szöveg += $" státus='{Adat.Státus}', ";
                szöveg += $" dátum='{Adat.Dátum:yyyy.MM.dd}', ";
                szöveg += $" telephely='{Adat.Telephely}', ";
                szöveg += $" pdffájl='{Adat.PDFfájl}' ";
                szöveg += $" WHERE IDoktatás={Adat.IDoktatás}";
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

        public long Sorszám()
        {
            long Válasz = 1;
            try
            {
                List<Adat_OktatásTábla> Adatok = Lista_Adatok();
                if (Adatok != null && Adatok.Count > 0) Válasz = Adatok.Max(x => x.IDoktatás) + 1;
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

        public void Csere(long Sorszám1, long Sorszám2)
        {
            try
            {
                Adat_OktatásTábla Adat1 = Lista_Adatok().Find(x => x.IDoktatás == Sorszám1);
                Adat_OktatásTábla Adat2 = Lista_Adatok().Find(x => x.IDoktatás == Sorszám2);

                List<string> SzövegGy = new List<string>();

                // előrébb visszük
                string szöveg = $"UPDATE Oktatástábla SET listázásisorrend={Adat2.Listázásisorrend} where idoktatás={Sorszám1}";
                SzövegGy.Add(szöveg);

                // hátrább visszük
                szöveg = $"UPDATE Oktatástábla SET listázásisorrend={Adat1.Listázásisorrend} where idoktatás={Sorszám2}";
                SzövegGy.Add(szöveg);

                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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
