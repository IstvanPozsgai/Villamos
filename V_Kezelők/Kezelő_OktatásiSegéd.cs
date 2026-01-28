using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_OktatásiSegéd
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Főmérnökség_oktatás.mdb";
        readonly string jelszó = "pázmányt";

        public Kezelő_OktatásiSegéd()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Oktatás_ALAP(hely.KönyvSzerk());
        }

        public List<Adat_OktatásiSegéd> Lista_Adatok()
        {
            List<Adat_OktatásiSegéd> Adatok = new List<Adat_OktatásiSegéd>();
            Adat_OktatásiSegéd Adat;
            string szöveg = $"SELECT * FROM Oktatásisegéd";
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
                                Adat = new Adat_OktatásiSegéd(
                                    rekord["IDoktatás"].ToÉrt_Long(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["oktatásoka"].ToStrTrim(),
                                    rekord["Oktatástárgya"].ToStrTrim(),
                                    rekord["Oktatáshelye"].ToStrTrim(),
                                    rekord["oktatásidőtartama"].ToÉrt_Long(),
                                    rekord["Oktató"].ToStrTrim(),
                                    rekord["Oktatóbeosztása"].ToStrTrim(),
                                    rekord["Egyébszöveg"].ToStrTrim(),
                                    rekord["email"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Módosítás(Adat_OktatásiSegéd Adat)
        {
            try
            {
                string szöveg = $"UPDATE oktatásisegéd SET ";
                szöveg += $"oktatásoka='{Adat.Oktatásoka}', ";
                szöveg += $"oktatástárgya='{Adat.Oktatástárgya}', ";
                szöveg += $"oktatáshelye='{Adat.Oktatáshelye}', ";
                szöveg += $"oktatásidőtartama={Adat.Oktatásidőtartama}, ";
                szöveg += $"oktató='{Adat.Oktató}', ";
                szöveg += $"oktatóbeosztása='{Adat.Oktatóbeosztása}', ";
                szöveg += $"egyébszöveg='{Adat.Egyébszöveg}', ";
                szöveg += $"email='{Adat.Email}' ";
                szöveg += $" WHERE Idoktatás={Adat.IDoktatás}";
                szöveg += $" and telephely='{Adat.Telephely}'";
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


        public void Rögzítés(Adat_OktatásiSegéd Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO oktatásisegéd (IDoktatás,  telephely, oktatásoka, oktatástárgya, oktatáshelye, oktatásidőtartama, oktató, oktatóbeosztása, egyébszöveg, email )";
                szöveg += $" VALUES ({Adat.IDoktatás}, ";
                szöveg += $"'{Adat.Telephely}', ";
                szöveg += $"'{Adat.Oktatásoka}', ";
                szöveg += $"'{Adat.Oktatástárgya}', ";
                szöveg += $"'{Adat.Oktatáshelye}', ";
                szöveg += $"{Adat.Oktatásidőtartama}, ";
                szöveg += $"'{Adat.Oktató}', ";
                szöveg += $"'{Adat.Oktatóbeosztása}', ";
                szöveg += $"'{Adat.Egyébszöveg}', ";
                szöveg += $"'{Adat.Email}') ";
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
