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
    public class Kezelő_Védő_Könyv
    {
        readonly string jelszó = "csavarhúzó";
        string hely;

        private void FájlBeállítás(string Telephely)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\Adatok\Védő\védőkönyv.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Védőkönyvtörzs(hely.KönyvSzerk());
        }

        public List<Adat_Védő_Könyv> Lista_Adatok(string Telephely)
        {
            string szöveg = $"SELECT * FROM lista  ORDER BY szerszámkönyvszám";
            FájlBeállítás(Telephely);
            List<Adat_Védő_Könyv> Adatok = new List<Adat_Védő_Könyv>();
            Adat_Védő_Könyv Adat;

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
                                Adat = new Adat_Védő_Könyv(
                                        rekord["szerszámkönyvszám"].ToStrTrim(),
                                        rekord["szerszámkönyvnév"].ToStrTrim(),
                                        rekord["felelős1"].ToStrTrim(),
                                        rekord["státus"].ToÉrt_Bool()
                                        );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, Adat_Védő_Könyv Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"INSERT INTO lista  (Szerszámkönyvszám, Szerszámkönyvnév, felelős1, státus ) VALUES (";
                szöveg += $"'{Adat.Szerszámkönyvszám}', ";
                szöveg += $"'{Adat.Szerszámkönyvnév}', ";
                szöveg += $"'{Adat.Felelős1}', ";
                szöveg += $"{Adat.Státus} )";
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

        public void Módosítás(string Telephely, Adat_Védő_Könyv Adat)
        {
            try
            {
                FájlBeállítás(Telephely);
                string szöveg = $"UPDATE lista  SET ";
                szöveg += $"Szerszámkönyvnév='{Adat.Szerszámkönyvnév}', ";
                szöveg += $"felelős1='{Adat.Felelős1}', ";
                szöveg += $"státus={Adat.Státus} ";
                szöveg += $" WHERE Szerszámkönyvszám='{Adat.Szerszámkönyvszám}'";
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

        public void AlapAdatok(string Telephely)
        {
            try
            {
                FájlBeállítás(Telephely);
                List<Adat_Védő_Könyv> Adatok = new List<Adat_Védő_Könyv>();
                Adat_Védő_Könyv Adat = new Adat_Védő_Könyv("Érkezett", "Új védőeszköz beérkeztetése", "_", false);
                Adatok.Add(Adat);
                Adat = new Adat_Védő_Könyv("Raktár", "Védő Raktár", "_", false);
                Adatok.Add(Adat);
                Adat = new Adat_Védő_Könyv("Selejt", "Leselejtezett", "_", false);
                Adatok.Add(Adat);
                Adat = new Adat_Védő_Könyv("Selejtre", "Selejtezésre előkészítés", "_", false);
                Adatok.Add(Adat);
                Adat = new Adat_Védő_Könyv("Átvétel", "Átvétel másik telephelyről", "_", false);
                Adatok.Add(Adat);
                Adat = new Adat_Védő_Könyv("Átadás", "Átadás másik telephelyre", "_", false);
                Adatok.Add(Adat);

                List<Adat_Védő_Könyv> AdatokÖ = Lista_Adatok(Telephely);

                foreach (Adat_Védő_Könyv Elem in Adatok)
                {
                    Adat = AdatokÖ.Where(a => a.Szerszámkönyvszám == Elem.Szerszámkönyvszám).FirstOrDefault();
                    if (Adat == null) Rögzítés(Telephely, Elem);
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
    }
}

