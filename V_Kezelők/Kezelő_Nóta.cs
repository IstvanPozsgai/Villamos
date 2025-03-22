using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.V_Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Nóta
    {
        readonly string jelszó = "TörökKasos";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Nóta\NótaT5C5.mdb";

        public Kezelő_Nóta()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.FődarabNóta(hely.KönyvSzerk());
        }

        public List<Adat_Nóta> Lista_Adat(bool Aktív = true)
        {
            string szöveg;
            if (Aktív == true)
                szöveg = $"SELECT * FROM Nóta_Adatok WHERE Státus<>9 ORDER BY ID";
            else
                szöveg = $"SELECT * FROM Nóta_Adatok ORDER BY ID";

            List<Adat_Nóta> Adatok = new List<Adat_Nóta>();
            Adat_Nóta Adat;

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
                                Adat = new Adat_Nóta(
                                     rekord["Id"].ToÉrt_Long(),
                                     rekord["Berendezés"].ToStrTrim(),
                                     rekord["Készlet_Sarzs"].ToStrTrim(),
                                     rekord["Raktár"].ToStrTrim(),
                                     rekord["Telephely"].ToStrTrim(),
                                     rekord["Forgóváz"].ToStrTrim(),
                                     rekord["Beépíthető"].ToÉrt_Bool(),
                                     rekord["MűszakiM"].ToStrTrim(),
                                     rekord["OsztásiM"].ToStrTrim(),
                                     rekord["Dátum"].ToÉrt_DaTeTime(),
                                     rekord["Státus"].ToÉrt_Int());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Módosítás(Adat_Nóta Adat)
        {
            try
            {
                string szöveg;
                if (Program.Postás_Vezér)
                {
                    szöveg = "UPDATE Nóta_Adatok SET ";
                    szöveg += $"Berendezés='{Adat.Berendezés}', ";
                    szöveg += $"Készlet_Sarzs='{Adat.Készlet_Sarzs}', ";
                    szöveg += $"Raktár='{Adat.Raktár}', ";
                    szöveg += $"Telephely='{Adat.Telephely}', ";
                    szöveg += $"Forgóváz='{Adat.Forgóváz}', ";
                    szöveg += $"Beépíthető={Adat.Beépíthető}, ";
                    szöveg += $"MűszakiM='{Adat.MűszakiM}', ";
                    szöveg += $"OsztásiM='{Adat.OsztásiM}', ";
                    szöveg += $"Dátum='{Adat.Dátum:yyyy.MM.dd}', ";
                    szöveg += $"Státus={Adat.Státus} ";
                    szöveg += $" WHERE [Id] ={Adat.Id}";
                }
                else
                {
                    szöveg = "UPDATE Nóta_Adatok SET ";
                    szöveg += $"Telephely='{Adat.Telephely}', ";
                    szöveg += $"Forgóváz='{Adat.Forgóváz}', ";
                    szöveg += $"Beépíthető={Adat.Beépíthető}, ";
                    szöveg += $"MűszakiM='{Adat.MűszakiM}' ";
                    szöveg += $" WHERE [Id] ={Adat.Id}";
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


        public void Rögzítés(Adat_Nóta Adat)
        {
            try
            {


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
