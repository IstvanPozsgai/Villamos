using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;


namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Kiegészítő_Csoportbeosztás
    {
        readonly string jelszó = "Mocó";
        public List<Adat_Kiegészítő_Csoportbeosztás> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiegészítő_Csoportbeosztás> Adatok = new List<Adat_Kiegészítő_Csoportbeosztás>();
            Adat_Kiegészítő_Csoportbeosztás Adat;

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
                                Adat = new Adat_Kiegészítő_Csoportbeosztás(
                                        rekord["Sorszám"].ToÉrt_Long(),
                                        rekord["Csoportbeosztás"].ToStrTrim(),
                                        rekord["Típus"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Kiegészítő_Csoportbeosztás> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * FROM csoportbeosztás order by sorszám";
            List<Adat_Kiegészítő_Csoportbeosztás> Adatok = new List<Adat_Kiegészítő_Csoportbeosztás>();
            Adat_Kiegészítő_Csoportbeosztás Adat;

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
                                Adat = new Adat_Kiegészítő_Csoportbeosztás(
                                        rekord["Sorszám"].ToÉrt_Long(),
                                        rekord["Csoportbeosztás"].ToStrTrim(),
                                        rekord["Típus"].ToStrTrim()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, string jelszó, Adat_Kiegészítő_Csoportbeosztás Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO csoportbeosztás (sorszám, csoportbeosztás, típus) ";
                szöveg += $"VALUES ({Adat.Sorszám}, ";
                szöveg += $"'{Adat.Csoportbeosztás}', ";
                szöveg += $"'{Adat.Típus}' )";
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
        /// <summary>
        /// csoportbeosztás
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Módosítás(string hely, string jelszó, Adat_Kiegészítő_Csoportbeosztás Adat)
        {
            try
            {
                string szöveg = " UPDATE csoportbeosztás SET ";
                szöveg += $" típus='{Adat.Típus}'";
                szöveg += $" WHERE csoportbeosztás='{Adat.Csoportbeosztás}'";
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


        public void Módosítás(string hely, string jelszó, List<Adat_Kiegészítő_Csoportbeosztás> Adat)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kiegészítő_Csoportbeosztás rekord in Adat)
                {
                    string szöveg = " UPDATE csoportbeosztás SET ";
                    szöveg += $" típus='{rekord.Típus}'";
                    szöveg += $" WHERE csoportbeosztás='{rekord.Csoportbeosztás}'";
                    SzövegGy.Add(szöveg);

                }
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
