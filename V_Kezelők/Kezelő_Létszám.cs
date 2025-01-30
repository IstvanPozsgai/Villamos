using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Létszám_Elrendezés_Változatok
    {
        readonly string jelszó = "repülő";
        public List<Adat_Létszám_Elrendezés_Változatok> Lista_Adatok(string hely)
        {
            List<Adat_Létszám_Elrendezés_Változatok> Adatok = new List<Adat_Létszám_Elrendezés_Változatok>();
            Adat_Létszám_Elrendezés_Változatok Adat;
            string szöveg = $"Select * FROM Alaplista";
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
                                Adat = new Adat_Létszám_Elrendezés_Változatok(
                                        rekord["id"].ToÉrt_Int(),
                                        rekord["Változatnév"].ToStrTrim(),
                                        rekord["Csoportnév"].ToStrTrim(),
                                        rekord["Oszlop"].ToStrTrim(),
                                        rekord["Sor"].ToÉrt_Int(),
                                        rekord["Szélesség"].ToÉrt_Int()
                                     );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(string hely, Adat_Létszám_Elrendezés_Változatok Adat)
        {
            try
            {
                string szöveg = "UPDATE Alaplista SET  ";
                szöveg += $" Csoportnév='{Adat.Csoportnév}', ";
                szöveg += $" oszlop='{Adat.Oszlop}', ";
                szöveg += $" sor={Adat.Sor}, ";
                szöveg += $" Változatnév='{Adat.Változatnév}', ";
                szöveg += $" szélesség={Adat.Szélesség} ";
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

        public void Rögzítés(string hely, Adat_Létszám_Elrendezés_Változatok Adat)
        {
            try
            {
                string szöveg = "INSERT INTO Változatok (Azonosító, változatnév) VALUES (";
                szöveg = "INSERT INTO Alaplista ";
                szöveg += "( id, csoportnév, oszlop, sor, szélesség, Változatnév) VALUES (";
                szöveg += $"{Sorszám(hely)},";
                szöveg += $" '{Adat.Csoportnév}',";
                szöveg += $" '{Adat.Oszlop}', ";
                szöveg += $"{Adat.Sor},";
                szöveg += $" {Adat.Szélesség},";
                szöveg += $" '{Adat.Változatnév}' )";
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

        public void Törlés(string hely, Adat_Létszám_Elrendezés_Változatok Adat)
        {
            try
            {
                string szöveg;
                if (Adat.Id == 0)
                    szöveg = $"DELETE FROM Alaplista WHERE Változatnév='{Adat.Változatnév}'";
                else
                    szöveg = $"DELETE FROM Alaplista WHERE sorszám={Adat.Id}";

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

        private int Sorszám(string hely)
        {
            int válasz = 1;
            try
            {
                List<Adat_Létszám_Elrendezés_Változatok> Adatok = Lista_Adatok(hely);
                if (Adatok != null && Adatok.Count > 0) válasz = Adatok.Max(a => a.Id) + 1;
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
}
