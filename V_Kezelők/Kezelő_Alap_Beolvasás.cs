using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;


namespace Villamos.Kezelők
{
    public class Kezelő_Alap_Beolvasás
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\beolvasás.mdb";
        readonly string jelszó = "sajátmagam";

        public Kezelő_Alap_Beolvasás()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Egyéb_beolvasás(hely.KönyvSzerk());
        }
        //elkopó
        public List<Adat_Alap_Beolvasás> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Alap_Beolvasás> Adatok = new List<Adat_Alap_Beolvasás>();
            Adat_Alap_Beolvasás Adat;

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
                                Adat = new Adat_Alap_Beolvasás(
                                        rekord["csoport"].ToStrTrim(),
                                        rekord["oszlop"].ToÉrt_Int(),
                                        rekord["fejléc"].ToStrTrim(),
                                        rekord["törölt"].ToStrTrim(),
                                        rekord["kell"].ToÉrt_Long()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Alap_Beolvasás> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM tábla";
            List<Adat_Alap_Beolvasás> Adatok = new List<Adat_Alap_Beolvasás>();
            Adat_Alap_Beolvasás Adat;

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
                                Adat = new Adat_Alap_Beolvasás(
                                        rekord["csoport"].ToStrTrim(),
                                        rekord["oszlop"].ToÉrt_Int(),
                                        rekord["fejléc"].ToStrTrim(),
                                        rekord["törölt"].ToStrTrim(),
                                        rekord["kell"].ToÉrt_Long()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Alap_Beolvasás Adat)
        {
            string szöveg = "INSERT INTO tábla ";
            szöveg += " ( csoport, oszlop, fejléc, törölt, kell)";
            szöveg += " VALUES ";
            szöveg += $" ('{Adat.Csoport}', {Adat.Oszlop}, '{Adat.Fejléc}', '{Adat.Törölt}', {Adat.Kell})";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public void Módosítás(Adat_Alap_Beolvasás Adat)
        {
            try
            {
                string szöveg = "UPDATE  tábla SET ";
                szöveg += $" fejléc='{Adat.Fejléc}', ";
                szöveg += $" kell={Adat.Kell}";
                szöveg += $" WHERE [csoport]= '{Adat.Csoport}' and [oszlop]={Adat.Oszlop}";
                szöveg += $" and [törölt]='{Adat.Törölt}'";
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

        public void Törlés(Adat_Alap_Beolvasás Adat)
        {
            try
            {
                string szöveg = "UPDATE  tábla SET ";
                szöveg += $" törölt='1'";
                szöveg += $" WHERE [csoport]= '{Adat.Csoport}'  and [oszlop]={Adat.Oszlop}";
                szöveg += $" and [törölt]='{Adat.Törölt}'";
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
