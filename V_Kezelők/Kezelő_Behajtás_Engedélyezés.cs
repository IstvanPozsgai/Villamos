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
    public class Kezelő_Behajtás_Engedélyezés
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\behajtási\Behajtási_alap.mdb";
        readonly string jelszó = "egérpad";

        public Kezelő_Behajtás_Engedélyezés()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Alap(hely.KönyvSzerk());
        }

        public List<Adat_Behajtás_Engedélyezés> Lista_Adatok()
        {
            string szöveg = "SELECT * FROM engedélyezés ORDER BY id";
            List<Adat_Behajtás_Engedélyezés> Adatok = new List<Adat_Behajtás_Engedélyezés>();
            Adat_Behajtás_Engedélyezés Adat;

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
                                Adat = new Adat_Behajtás_Engedélyezés(
                                        rekord["Id"].ToÉrt_Int(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Emailcím"].ToStrTrim(),
                                        rekord["Gondnok"].ToÉrt_Bool(),
                                        rekord["Szakszolgálat"].ToÉrt_Bool(),
                                        rekord["Telefonszám"].ToStrTrim(),
                                        rekord["Szakszolgálatszöveg"].ToStrTrim(),
                                        rekord["Beosztás"].ToStrTrim(),
                                        rekord["Név"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Behajtás_Engedélyezés Adat)
        {
            try
            {
                string szöveg = "INSERT INTO engedélyezés (id, telephely, emailcím, gondnok, szakszolgálat, telefonszám, szakszolgálatszöveg, beosztás, név) VALUES (";
                szöveg += $"{Sorszám()}, "; // id 
                szöveg += $"'{Adat.Telephely}', "; // telephely
                szöveg += $"'{Adat.Emailcím}', "; // emailcím
                szöveg += $" {Adat.Gondnok}, ";       // gondnok
                szöveg += $" {Adat.Szakszolgálat}, "; // szakszolgálat
                szöveg += $"'{Adat.Telefonszám}', "; // telefonszám
                szöveg += $"'{Adat.Szakszolgálatszöveg}', "; // szakszolgálatszöveg
                szöveg += $"'{Adat.Beosztás}', "; // beosztás
                szöveg += $"'{Adat.Név}') "; // név
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


        public void Döntés(Adat_Behajtás_Engedélyezés Adat)
        {
            try
            {
                if (Adat.Id == 0)
                    Rögzítés(Adat);
                else
                {
                    List<Adat_Behajtás_Engedélyezés> Adatok = Lista_Adatok();
                    Adat_Behajtás_Engedélyezés Elem = (from a in Adatok
                                                       where a.Id == Adat.Id // feltételezve, hogy az Id az egyedi azonosító
                                                       select a).FirstOrDefault();
                    if (Elem != null)
                        Módosítás(Adat);
                    else
                        Rögzítés(Adat);
                }
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

        public void Módosítás(Adat_Behajtás_Engedélyezés Adat)
        {
            try
            {
                string szöveg = "UPDATE engedélyezés SET ";
                szöveg += $" telephely='{Adat.Telephely}', "; // telephely
                szöveg += $" emailcím='{Adat.Emailcím}', "; // emailcím
                szöveg += $" gondnok={Adat.Gondnok}, ";
                szöveg += $" szakszolgálat={Adat.Szakszolgálat}, ";       // szakszolgálat
                szöveg += $" telefonszám='{Adat.Telefonszám}', "; // telefonszám
                szöveg += $" szakszolgálatszöveg='{Adat.Szakszolgálatszöveg}', "; // szakszolgálatszöveg
                szöveg += $" beosztás='{Adat.Beosztás}', "; // beosztás
                szöveg += $" név='{Adat.Név}'"; // név
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

        public long Sorszám()
        {
            long Válasz = 1;
            try
            {
                List<Adat_Behajtás_Engedélyezés> Adatok = Lista_Adatok();
                if (Adatok != null && Adatok.Count > 0) Válasz = Adatok.Max(x => x.Id) + 1;
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

        public void Törlés(int sorszám)
        {
            try
            {
                string szöveg = $"DELETE FROM engedélyezés WHERE id={sorszám}";
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

        public void Csere(int sor)
        {
            try
            {
                List<Adat_Behajtás_Engedélyezés> Adatok = Lista_Adatok().OrderBy(a => a.Id).ToList();
                Adat_Behajtás_Engedélyezés Elem = (from a in Adatok
                                                   where a.Id == sor
                                                   select a).FirstOrDefault();
                Adat_Behajtás_Engedélyezés Előző = (from a in Adatok
                                                    where a.Id == sor - 1
                                                    orderby a.Id descending
                                                    select a).FirstOrDefault();
                if (Elem == null || Előző == null) throw new HibásBevittAdat("Az első elemet nem lehet előrébb helyezni.");

                List<string> SzövegGy = new List<string>();
                string szöveg = $"UPDATE engedélyezés SET id=0 WHERE id={Elem.Id}";
                SzövegGy.Add(szöveg);
                szöveg = $"UPDATE engedélyezés SET id={Elem.Id} WHERE id={Előző.Id}";
                SzövegGy.Add(szöveg);
                szöveg = $"UPDATE engedélyezés SET id={Előző.Id} WHERE id=0";
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
