using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos_Kezelők
{
    public class Kezelő_Behajtás_Alap
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\behajtási\Behajtási_alap.mdb";
        readonly string jelszó = "egérpad";

        public List<Adat_Behajtás_Alap> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM alapadatok ORDER BY id";
            List<Adat_Behajtás_Alap> Adatok = new List<Adat_Behajtás_Alap>();
            Adat_Behajtás_Alap Adat;

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
                                Adat = new Adat_Behajtás_Alap(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Adatbázisnév"].ToStrTrim(),
                                        rekord["Sorszámbetűjele"].ToString(),
                                        rekord["Sorszámkezdete"].ToÉrt_Int(),
                                        rekord["Engedélyérvényes"].ToÉrt_DaTeTime(),
                                        rekord["Státus"].ToÉrt_Int(),
                                        rekord["Adatbáziskönyvtár"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Behajtás_Alap Adat)
        {
            try
            {
                string szöveg = "INSERT  INTO alapadatok (id, adatbázisnév, Sorszámbetűjele, Sorszámkezdete, Engedélyérvényes, Státus, Adatbáziskönyvtár) ";
                szöveg += " VALUES ";
                szöveg += $" ({Sorszám()}, '{Adat.Adatbázisnév}', '{Adat.Sorszámbetűjele}', {Adat.Sorszámkezdete},";
                szöveg += $" '{Adat.Engedélyérvényes:yyyy.MM.dd}', {Adat.Státus}, '{Adat.Adatbáziskönyvtár}')";
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

        public void Módosítás(Adat_Behajtás_Alap Adat)
        {
            try
            {
                string szöveg = $"Adatbázisnév='{Adat.Adatbázisnév}', ";
                szöveg += $"Sorszámbetűjele='{Adat.Sorszámbetűjele}', ";
                szöveg += $"Sorszámkezdete={Adat.Sorszámkezdete}, ";
                szöveg += $"Engedélyérvényes='{Adat.Engedélyérvényes:yyyy.MM.dd}', ";
                szöveg += $"Státus={Adat.Státus}, ";
                szöveg += $"Adatbáziskönyvtár='{Adat.Adatbáziskönyvtár}' ";
                szöveg += $"WHERE id={Adat.Id};";
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

        private int Sorszám()
        {
            int válasz = 1;
            try
            {
                List<Adat_Behajtás_Alap> Adatok = Lista_Adatok();
                if (Adatok != null) válasz = Adatok.Max(a => a.Id) + 1;
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






    public class Kezelő_Behajtás_Szolgálati
    {
        public List<Adat_Behajtás_Szolgálati> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Behajtás_Szolgálati> Adatok = new List<Adat_Behajtás_Szolgálati>();
            Adat_Behajtás_Szolgálati Adat;

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
                                Adat = new Adat_Behajtás_Szolgálati(
                                        rekord["ID"].ToÉrt_Int(),
                                        rekord["Szolgálatihely"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }

}
