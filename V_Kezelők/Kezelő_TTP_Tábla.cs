using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_TTP_Tábla
    {
        readonly string hely = $@"{Application.StartupPath}/Főmérnökség/adatok/TTP/TTP_Adatbázis.mdb";
        readonly string jelszó = "rudolfg";

        public Kezelő_TTP_Tábla()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.TTP_Adatbázis(hely.KönyvSzerk());
        }

        public List<Adat_TTP_Tábla> Lista_Adatok()
        {
            List<Adat_TTP_Tábla> Adatok = new List<Adat_TTP_Tábla>();
            Adat_TTP_Tábla Adat;
            string szöveg = $"SELECT * FROM TTP_Tábla";

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
                                Adat = new Adat_TTP_Tábla(
                                       rekord["Azonosító"].ToStrTrim(),
                                       rekord["Lejárat_Dátum"].ToÉrt_DaTeTime(),
                                       rekord["Ütemezés_Dátum"].ToÉrt_DaTeTime(),
                                       rekord["TTP_Dátum"].ToÉrt_DaTeTime(),
                                       rekord["TTP_Javítás"].ToÉrt_Bool(),
                                       rekord["Rendelés"].ToStrTrim(),
                                       rekord["JavBefDát"].ToÉrt_DaTeTime(),
                                       rekord["Együtt"].ToStrTrim(),
                                       rekord["Státus"].ToÉrt_Int(),
                                       rekord["Megjegyzés"].ToStrTrim()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Módosítás(Adat_TTP_Tábla Adat)
        {
            try
            {
                string szöveg = $"UPDATE TTP_Tábla SET ";
                szöveg += $"[Lejárat_Dátum]='{Adat.Lejárat_Dátum:d}', ";
                szöveg += $"[TTP_Dátum]='{Adat.TTP_Dátum:d}', ";
                szöveg += $"[TTP_Javítás]={Adat.TTP_Javítás}, ";
                szöveg += $"[Rendelés] ='{Adat.Rendelés}', ";
                szöveg += $"[JavBefDát] ='{Adat.JavBefDát:d}', ";
                szöveg += $"[Együtt]='{Adat.Együtt}', ";
                szöveg += $"[Státus]={Adat.Státus}, ";
                szöveg += $"[Megjegyzés]='{Adat.Megjegyzés}' ";
                szöveg += $" WHERE [Azonosító]='{Adat.Azonosító}' AND [Ütemezés_Dátum]=#{Adat.Ütemezés_Dátum:M-d-yy}#";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "TTP_AdatTábla_Rögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Rögzítés(Adat_TTP_Tábla Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO TTP_Tábla (";
                szöveg += $"[Azonosító], ";
                szöveg += $"[Lejárat_Dátum], ";
                szöveg += $"[Ütemezés_Dátum], ";
                szöveg += $"[TTP_Dátum], ";
                szöveg += $"[TTP_Javítás], ";
                szöveg += $"[Rendelés] , ";
                szöveg += $"[JavBefDát] , ";
                szöveg += $"[Együtt], ";
                szöveg += $"[Státus], ";
                szöveg += $"[Megjegyzés] ) VALUES (";
                szöveg += $"'{Adat.Azonosító}', ";
                szöveg += $"'{Adat.Lejárat_Dátum}', ";
                szöveg += $"'{Adat.Ütemezés_Dátum}', ";
                szöveg += $"'{Adat.TTP_Dátum}', ";
                szöveg += $"{Adat.TTP_Javítás}, ";
                szöveg += $"'{Adat.Rendelés}', ";
                szöveg += $"'{Adat.JavBefDát}', ";
                szöveg += $"'{Adat.Együtt}', ";
                szöveg += $"{Adat.Státus}, ";
                szöveg += $"'{Adat.Megjegyzés}' )";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "TTP_AdatTábla_Módosítás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Törlés(Adat_TTP_Tábla Adat)
        {
            try
            {
                string szöveg = $"DELETE FROM TTP_Tábla ";
                szöveg += $" WHERE [Azonosító]='{Adat.Azonosító}' AND [Ütemezés_Dátum]=#{Adat.Ütemezés_Dátum:MM-dd-yyyy}#";
                MyA.ABtörlés(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "TTP_AdatTábla_Rögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void TTP_AdatTábla_Vizsgál(Adat_TTP_Tábla Adat, List<Adat_TTP_Tábla> Adatok)
        {
            try
            {
                Adat_TTP_Tábla Elem = (from a in Adatok
                                       where a.Azonosító == Adat.Azonosító
                                       && a.Ütemezés_Dátum == Adat.Ütemezés_Dátum
                                       select a).FirstOrDefault();
                if (Elem != null)
                    Módosítás(Adat);
                else
                    Rögzítés(Adat);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "TTP_AdatTábla_Módosítás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void TörténetbeRögzítés(DateTime ÜtemezésDátuma, string Pályaszám, List<Adat_TTP_Tábla> Adatok, Adat_Tábla_Vezénylés Adat, string szerelvény)
        {
            try
            {
                Adat_TTP_Tábla Eleme = (from a in Adatok
                                        where a.Azonosító == Pályaszám
                                        && a.Ütemezés_Dátum == ÜtemezésDátuma
                                        select a).FirstOrDefault();
                if (Eleme == null)
                {
                    Adat_TTP_Tábla Elem = new Adat_TTP_Tábla(
                                            Adat.Azonosító,
                                            Adat.Le_Dátum,
                                            ÜtemezésDátuma,
                                            new DateTime(1900, 1, 1),
                                            false,
                                            "",
                                            new DateTime(1900, 1, 1),
                                            szerelvény,
                                            1,
                                            Adat.Megjegyzés);
                    Rögzítés(Elem);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "TörténetbeRögzítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

}
