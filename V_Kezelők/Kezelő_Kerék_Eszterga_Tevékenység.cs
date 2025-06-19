using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kerék_Eszterga_Tevékenység
    {
        readonly string jelszó = "RónaiSándor";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Törzs.mdb";
        readonly string táblanév = "Tevékenység";

        public List<Adat_Kerék_Eszterga_Tevékenység> Lista_Adatok()
        {
            List<Adat_Kerék_Eszterga_Tevékenység> Adatok = new List<Adat_Kerék_Eszterga_Tevékenység>();
            Adat_Kerék_Eszterga_Tevékenység Adat;
            string szöveg = $"SELECT * FROM {táblanév} ORDER BY id";
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
                                Adat = new Adat_Kerék_Eszterga_Tevékenység(
                                        rekord["Tevékenység"].ToStrTrim(),
                                        rekord["Munkaidő"].ToÉrt_Double(),
                                        rekord["betűszín"].ToÉrt_Long(),
                                        rekord["háttérszín"].ToÉrt_Long(),
                                        rekord["id"].ToÉrt_Int(),
                                        rekord["Marad"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Döntés(Adat_Kerék_Eszterga_Tevékenység Adat)
        {
            try
            {
                if (Adat.Id == 0)
                    Rögzítés(Adat);
                else
                    Módosítás(Adat);
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

        public void Rögzítés(Adat_Kerék_Eszterga_Tevékenység Adat)
        {
            try
            {
                int ID = 1;
                List<Adat_Kerék_Eszterga_Tevékenység> Adatok = Lista_Adatok();
                if (Adatok.Count > 0) // van már adat
                {
                    ID = Adatok.Max(x => x.Id) + 1; // az utolsó id + 1
                }

                string szöveg = $"INSERT INTO {táblanév} (Id, Tevékenység, Munkaidő, HáttérSzín, BetűSzín, marad) VALUES (";
                szöveg += $" {ID}, '{Adat.Tevékenység}', {Adat.Munkaidő}, {Adat.Háttérszín}, {Adat.Betűszín}, {Adat.Marad})";
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

        public void Módosítás(Adat_Kerék_Eszterga_Tevékenység Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $" Tevékenység='{Adat.Tevékenység}', ";
                szöveg += $" HáttérSzín={Adat.Háttérszín}, ";
                szöveg += $" BetűSzín={Adat.Betűszín}, ";
                szöveg += $" munkaidő={Adat.Munkaidő}, ";
                szöveg += $" marad={Adat.Marad} ";
                szöveg += $" WHERE id={Adat.Id}";
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

        public void Módosítás(int honnan, int hova)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $" id={hova} ";
                szöveg += $" WHERE id={honnan}";
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

        public void Törlés(int Id)
        {
            try
            {
                string szöveg = $"DELETE FROM {táblanév} WHERE id={Id}";
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

        public void Rendezés()
        {
            try
            {
                List<Adat_Kerék_Eszterga_Tevékenység> Adatok = Lista_Adatok();
                string szöveg = $"DELETE FROM {táblanév}";
                MyA.ABtörlés(hely, jelszó, szöveg);
                int i = 1;
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Kerék_Eszterga_Tevékenység rekord in Adatok)
                {
                    szöveg = $"INSERT INTO {táblanév}  (Id, Tevékenység, Munkaidő, HáttérSzín, BetűSzín, marad) VALUES (";
                    szöveg += $"{i}, '{rekord.Tevékenység.Trim()}', {rekord.Munkaidő}, {rekord.Háttérszín}, {rekord.Betűszín}, {rekord.Marad})";
                    i++;
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

        public void Feljebb(int ID)
        {
            try
            {
                List<Adat_Kerék_Eszterga_Tevékenység> Adatok = Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Id < ID
                          orderby a.Id descending
                          select a).ToList();

                int előző = Adatok[0].Id;
                Módosítás(ID, 0);
                Módosítás(előző, ID);
                Módosítás(0, előző);
                Törlés(0);
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


        //Elkopó
        public Adat_Kerék_Eszterga_Tevékenység Egy_Adat(string hely, string jelszó, string szöveg)
        {
            Adat_Kerék_Eszterga_Tevékenység Adat = null;

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
                            rekord.Read();
                            Adat = new Adat_Kerék_Eszterga_Tevékenység(
                                    rekord["Tevékenység"].ToStrTrim(),
                                    rekord["Munkaidő"].ToÉrt_Double(),
                                    rekord["betűszín"].ToÉrt_Long(),
                                    rekord["háttérszín"].ToÉrt_Long(),
                                    rekord["id"].ToÉrt_Int(),
                                    rekord["Marad"].ToÉrt_Bool()
                                    );
                        }
                    }
                }
            }
            return Adat;
        }


    }

}
