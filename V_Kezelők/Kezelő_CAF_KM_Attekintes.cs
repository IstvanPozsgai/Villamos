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
    public class Kezelő_CAF_KM_Attekintes
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
        readonly string jelszó = "CzabalayL";

        public Kezelő_CAF_KM_Attekintes()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.CAFtábla(hely.KönyvSzerk());
        }

        public List<Adat_CAF_KM_Attekintes> Lista_Adatok()
        {
            string szöveg;           
            szöveg = $"SELECT * FROM KM_Attekintes ORDER BY azonosító";

            List<Adat_CAF_KM_Attekintes> Adatok = new List<Adat_CAF_KM_Attekintes>();
            Adat_CAF_KM_Attekintes Adat;

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
                                Adat = new Adat_CAF_KM_Attekintes(
                                        rekord["azonosito"].ToStrTrim(),
                                        rekord["kov_p0"].ToÉrt_Long(),
                                        rekord["kov_p1"].ToÉrt_Long(),
                                        rekord["kov_p2"].ToÉrt_Long(),
                                        rekord["utolso_p0_kozott"].ToÉrt_Long(),
                                        rekord["utolso_p1_kozott"].ToÉrt_Long(),
                                        rekord["utolso_p3_es_p2_kozott"].ToÉrt_Long(),
                                        rekord["elso_p2"].ToÉrt_Long(),
                                        rekord["elso_p3"].ToÉrt_Long()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_CAF_KM_Attekintes Egy_Adat(string Azonosító)
        {
            Adat_CAF_KM_Attekintes Adat = null;
            try
            {
                List<Adat_CAF_KM_Attekintes> Adatok = Lista_Adatok();
                if (Adatok.Count > 0) Adat = Adatok.FirstOrDefault(a => a.azonosito == Azonosító);
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
            return Adat;
        }

        public void Rögzítés_Elso(Adat_CAF_KM_Attekintes Adat)
        {
            try
            {
                // Ezt kell lefuttatni a programrész bevezetésekor a verziócsere után
                string szöveg = "INSERT INTO KM_Attekintes (azonosító, kov_p0, kov_p1, kov_p2, utolso_p0_kozott, utolso_p1_kozott, utolso_p3_es_p2_kozott, elso_p2, elso_p3) VALUES (";
                szöveg += $"'{Adat.azonosito}', "; // azonosító
                szöveg += $"'{Adat.kov_p0}', "; // Megtehető KM a következő vizsgálatig.
                szöveg += $"'{Adat.kov_p1}', "; // Megtehető KM a következő P1 vizsgálatig.
                szöveg += $"{Adat.kov_p2}, "; // Megtehető KM a következő P2 vizsgálatig.
                szöveg += $"'{Adat.utolso_p0_kozott}', "; // Megtett KM az előző P0 vizsgálatok között.
                szöveg += $"'{Adat.utolso_p1_kozott}', "; // Megtett KM az előző P1 vizsgálatok között.
                szöveg += $"'{Adat.utolso_p3_es_p2_kozott}', "; // Megtett KM az előző P3 és P2 vizsgálatok között.
                szöveg += $"'{Adat.elso_p2}', "; // Első P2-es vizsgálat KM értéke
                szöveg += $"'{Adat.elso_p3}')"; // Első P3-as vizsgálat KM értéke
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

        // Ez fog lefutni a frissítés gomb hatására / javítás beírásakor.
        public void Módosítás(Adat_CAF_KM_Attekintes Adat)
        {
            try
            {
                string szöveg = "UPDATE KM_Attekintes  SET ";
                szöveg += $"kov_p0='{Adat.kov_p0}', "; 
                szöveg += $"kov_p1='{Adat.kov_p1}', "; 
                szöveg += $"kov_p2={Adat.kov_p2}, "; 
                szöveg += $"utolso_p0_kozott='{Adat.utolso_p0_kozott}', "; 
                szöveg += $"utolso_p1_kozott='{Adat.utolso_p1_kozott}', "; 
                szöveg += $"utolso_p3_es_p2_kozott='{Adat.utolso_p3_es_p2_kozott}', "; 
                szöveg += $" WHERE azonosító='{Adat.azonosito}'";
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

