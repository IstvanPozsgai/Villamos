using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Külső_Telephelyek
    {
        readonly string táblanév = "telephelyek";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Behajtási\Külső_adatok.mdb";
        readonly string jelszó = "Janda";

        public Kezelő_Külső_Telephelyek()
        {
            FájlBeállítás();
        }

        private void FájlBeállítás()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Külsős_Táblák(hely);
        }


        public List<Adat_Külső_Telephelyek> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Külső_Telephelyek> Adatok = new List<Adat_Külső_Telephelyek>();
            Adat_Külső_Telephelyek Adat;

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
                                Adat = new Adat_Külső_Telephelyek(
                                        rekord["Id"].ToÉrt_Double(),
                                        rekord["Telephely"].ToStrTrim(),
                                        rekord["Cégid"].ToÉrt_Double(),
                                        rekord["Státus"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }


        public void Rögzítés(List<Adat_Külső_Telephelyek> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Külső_Telephelyek Adat in Adatok)
                {
                    List<Adat_Külső_Telephelyek> Adatok_Külső_Telephelyek = Lista_Adatok();
                    double id = Adatok_Külső_Telephelyek.Any() ? Adatok_Külső_Telephelyek.Max(a => a.Id) + 1 : 1;

                    string szöveg = $"INSERT INTO {táblanév} (id, telephely, cégid, státus ) VALUES (";
                    szöveg += $"{id}, ";
                    szöveg += $"'{Adat.Telephely}', ";
                    szöveg += $"{Adat.Cégid}, ";
                    szöveg += $"{Adat.Státus} )";
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


        public void Módosítás(List<Adat_Külső_Telephelyek> Adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Külső_Telephelyek Adat in Adatok)
                {


                    string szöveg = "UPDATE telephelyek  SET ";
                    if (!bool.Parse(Telephely_Tábla.Rows[i].Cells[0].Value.ToString()))
                        szöveg += "státus=false ";
                    else
                        szöveg += "státus=true ";

                    szöveg += " WHERE  cégid=" + Telephely_Cégid.Text.Trim() + " AND telephely='" + Telephely_Tábla.Rows[i].Cells[1].Value.ToString().Trim() + "'";
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
