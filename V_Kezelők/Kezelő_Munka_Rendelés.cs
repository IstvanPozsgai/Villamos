using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos.Kezelők
{
    public class Kezelő_Munka_Rendelés
    {
        readonly string jelszó = "felépítés";
        public List<Adat_Munka_Rendelés> Lista_Adatok(string hely)
        {

            string szöveg = "SELECT * FROM rendeléstábla  order by  id";
            List<Adat_Munka_Rendelés> Adatok = new List<Adat_Munka_Rendelés>();
            Adat_Munka_Rendelés Adat;

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
                                Adat = new Adat_Munka_Rendelés(
                                          rekord["ID"].ToÉrt_Long(),
                                          rekord["megnevezés"].ToString(),
                                          rekord["művelet"].ToString(),
                                          rekord["pályaszám"].ToString(),
                                          rekord["rendelés"].ToString()
                                          );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string hely, Adat_Munka_Rendelés Adat)
        {
            try
            {
                string szöveg = "INSERT INTO  rendeléstábla (rendelés, művelet, megnevezés, pályaszám) VALUES (";
                szöveg += $"'{Adat.Rendelés}', ";
                szöveg += $"'{Adat.Műveletet}', ";
                szöveg += $"'{Adat.Megnevezés}', ";
                szöveg += $"'{Adat.Pályaszám}') ";
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

        public void Módosítás(string hely, Adat_Munka_Rendelés Adat)
        {
            try
            {
                string szöveg = "UPDATE rendeléstábla  SET ";
                szöveg += $" megnevezés='{Adat.Megnevezés}', ";
                szöveg += $" pályaszám='{Adat.Pályaszám}', ";
                szöveg += $" rendelés='{Adat.Rendelés}', ";
                szöveg += $" művelet='{Adat.Műveletet}' ";
                szöveg += $" WHERE id={Adat.ID}";
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

        public void Törlés(string hely, long ID)
        {
            try
            {
                string szöveg = $"DELETE FROM rendeléstábla WHERE id={ID}";
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

        public void Csere(string hely, long előző, long következő)
        {
            try
            {
                List<Adat_Munka_Rendelés> Adatok = Lista_Adatok(hely);
                Adat_Munka_Rendelés ElőzőAdat = (from a in Adatok
                                                 where a.ID == előző
                                                 select a).FirstOrDefault();
                Adat_Munka_Rendelés KövetkezőAdat = (from a in Adatok
                                                     where a.ID == következő
                                                     select a).FirstOrDefault();

                Adat_Munka_Rendelés ÚjElőző=new Adat_Munka_Rendelés(következő,
                                                                    ElőzőAdat.Megnevezés,
                                                                    ElőzőAdat.Műveletet,
                                                                    ElőzőAdat.Pályaszám,
                                                                    ElőzőAdat.Rendelés);        
                Adat_Munka_Rendelés ÚjKövetkező = new Adat_Munka_Rendelés(előző,
                                                                          KövetkezőAdat.Megnevezés,
                                                                          KövetkezőAdat.Műveletet,
                                                                          KövetkezőAdat.Pályaszám,
                                                                          KövetkezőAdat.Rendelés);
                Módosítás(hely, ÚjElőző);
                Módosítás(hely, ÚjKövetkező);
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
