using System;
using System.Data.OleDb;
using System.Windows.Forms;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Szerszám_FejLáb
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Kiegészítő.mdb".KönyvSzerk();
        readonly string jelszó = "Mocó";

        public Kezelő_Szerszám_FejLáb()
        {
            string szöveg = $"SELECT * FROM Szerszám_FejLáb";
            if (!Adatbázis.ABvanTábla(hely, jelszó, szöveg)) Adatbázis_Létrehozás.Szerszám_FejLáb(hely);
        }

        public Adat_Szerszám_FejLáb Egy_Adat()
        {
            Adat_Szerszám_FejLáb Adat = null;
            string szöveg = $"SELECT * FROM Szerszám_FejLáb WHERE Id=1";
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

                            Adat = new Adat_Szerszám_FejLáb(
                                      rekord["Id"].ToÉrt_Int(),
                                      rekord["Fejléc_Bal"].ToStrTrim(),
                                      rekord["Fejléc_Közép"].ToStrTrim(),
                                      rekord["Fejléc_Jobb"].ToStrTrim(),
                                      rekord["Lábléc_Bal"].ToStrTrim(),
                                      rekord["Lábléc_Közép"].ToStrTrim(),
                                      rekord["Lábléc_Jobb"].ToStrTrim()
                                      );
                        }
                    }
                }
            }
            return Adat;
        }

        public void Rögzítés(Adat_Szerszám_FejLáb Adat)
        {
            try
            {
                string szöveg;
                if (Egy_Adat() == null)
                {
                    szöveg = $"INSERT INTO Szerszám_FejLáb (Id, Fejléc_Bal, Fejléc_Közép, Fejléc_Jobb, Lábléc_Bal, Lábléc_Közép, Lábléc_Jobb) VALUES (1, ";
                    szöveg += $"'{Adat.Fejléc_Bal}', ";
                    szöveg += $"'{Adat.Fejléc_Közép}', ";
                    szöveg += $"'{Adat.Fejléc_Jobb}', ";
                    szöveg += $"'{Adat.Lábléc_Bal}', ";
                    szöveg += $"'{Adat.Lábléc_Közép}', ";
                    szöveg += $"'{Adat.Lábléc_Jobb}' )";
                }
                else
                {
                    szöveg = $"UPDATE Szerszám_FejLáb SET ";
                    szöveg += $"Fejléc_Bal='{Adat.Fejléc_Bal}', ";
                    szöveg += $"Fejléc_Közép='{Adat.Fejléc_Közép}', ";
                    szöveg += $"Fejléc_Jobb='{Adat.Fejléc_Jobb}', ";
                    szöveg += $"Lábléc_Bal='{Adat.Lábléc_Bal}', ";
                    szöveg += $"Lábléc_Közép='{Adat.Lábléc_Közép}', ";
                    szöveg += $"Lábléc_Jobb='{Adat.Lábléc_Jobb}' ";
                    szöveg += $"WHERE Id={Adat.Id} ";
                }
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
