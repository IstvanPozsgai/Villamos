using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_kiegészítő_Hibaterv
    {
        readonly string jelszó = "Mocó";

        public List<Adat_Kiegészítő_Hibaterv> Lista_Adatok(string hely)
        {
            string szöveg = "SELECT * FROM hibaterv order by id";
            List<Adat_Kiegészítő_Hibaterv> Adatok = new List<Adat_Kiegészítő_Hibaterv>();
            Adat_Kiegészítő_Hibaterv Adat;

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
                                Adat = new Adat_Kiegészítő_Hibaterv(
                                    rekord["id"].ToÉrt_Long(),
                                    rekord["szöveg"].ToStrTrim(),
                                    rekord["főkönyv"].ToÉrt_Bool()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, Adat_Kiegészítő_Hibaterv Adat)
        {
            string szöveg = $"INSERT INTO hibaterv (id , szöveg, főkönyv ) ";
            szöveg += $" VALUES ({Adat.Id}, ";
            szöveg += $"'{Adat.Szöveg}', ";
            szöveg += $"{Adat.Főkönyv})";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        /// <summary>
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Mósosítás(string hely, Adat_Kiegészítő_Hibaterv Adat)
        {
            try
            {
                string szöveg = $"UPDATE hibaterv SET ";
                szöveg += $"főkönyv={Adat.Főkönyv}, ";
                szöveg += $"szöveg='{Adat.Szöveg}' ";
                szöveg += $"WHERE id={Adat.Id}";
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
        /// id
        /// </summary>
        /// <param name="hely"></param>
        /// <param name="jelszó"></param>
        /// <param name="Adat"></param>
        public void Törlés(string hely, Adat_Kiegészítő_Hibaterv Adat)
        {
            try
            {
                string szöveg = $"DELETE * FROM hibaterv where id={Adat.Id}";
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

    }

}
