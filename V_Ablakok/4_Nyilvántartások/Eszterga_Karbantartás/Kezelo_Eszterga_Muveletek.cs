using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos_Kezelők
{
    // JAVÍTANDÓ:
    //sok módosítás
    //meg nincs kesz
    public class Kezelo_Eszterga_Muveletek
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Eszterga_Karbantartás.mdb".KönyvSzerk();
        readonly string jelszo = "bozaim";
        readonly string tablaNev = "Műveletek";

        public Kezelo_Eszterga_Muveletek()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Eszterga_Karbantartás(hely);
        }

        public List<Adat_Eszterga_Muveletek> Lista_Adatok()
        {
            string szoveg = $"SELECT * FROM {tablaNev} ORDER BY ID  ";
            List<Adat_Eszterga_Muveletek> Adatok = new List<Adat_Eszterga_Muveletek>();
            Adat_Eszterga_Muveletek Adat;

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszo}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                using (OleDbCommand Parancs = new OleDbCommand(szoveg, Kapcsolat))
                {
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {
                        if (rekord.HasRows)
                        {
                            while (rekord.Read())
                            {
                                Adat = new Adat_Eszterga_Muveletek(
                                        rekord["ID"].ToÉrt_Int(),
                                        rekord["Művelet"].ToStrTrim(),
                                        rekord["Egység"].ToÉrt_Int(),
                                        rekord["Mennyi_Dátum"].ToÉrt_Int(),
                                        rekord["Mennyi_Óra"].ToÉrt_Int(),
                                        rekord["Státus"].ToÉrt_Bool(),
                                        rekord["Utolsó_Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Utolsó_Üzemóra_Állás"].ToÉrt_Long(),
                                        rekord["Megjegyzés"].ToStrTrim());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
        public void Rogzites(Adat_Eszterga_Muveletek Adat)
        {
            try
            {
                string szoveg = $"INSERT INTO {tablaNev} (ID, Művelet, Egység, Mennyi_Dátum, Mennyi_Óra, Státus, Utolsó_Dátum, Utolsó_Üzemóra_Állás) VALUES(";
                szoveg += $"'{Sorszam()}', ";
                szoveg += $"'{Adat.Művelet}', ";
                szoveg += $"{Adat.Egység}, ";
                szoveg += $"{Adat.Mennyi_Dátum}, ";
                szoveg += $"{Adat.Mennyi_Óra}, ";
                szoveg += $"{(Adat.Státus ? "True" : "False")}, ";
                szoveg += $"#{Adat.Utolsó_Dátum:yyyy-MM-dd}#, ";
                szoveg += $"{Adat.Utolsó_Üzemóra_Állás})";
                MyA.ABMódosítás(hely, jelszo, szoveg);
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
        public void Torles(List<Adat_Eszterga_Muveletek> Adatok, bool torles)
        {
            try
            {
                List<string> sqlLista = new List<string>();

                foreach (Adat_Eszterga_Muveletek rekord in Adatok)
                {
                    string oszlop = torles ? "Státus=True" : "Megjegyzés=NULL";
                    string szoveg = $"UPDATE {tablaNev} SET {oszlop} WHERE ID={rekord.ID}";
                    sqlLista.Add(szoveg);
                }

                MyA.ABMódosítás(hely, jelszo, sqlLista);
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
        public int Sorszam()
        {
            int valasz = 1;
            try
            {
                List<Adat_Eszterga_Muveletek> Adatok = Lista_Adatok();
                if (Adatok.Count > 0) valasz = Adatok.Max(a => a.ID) + 1;
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
            return valasz;
        }

        public void Modositas(List<Adat_Eszterga_Muveletek> Adatok)
        {
            try
            {
                List<string> sqlLista = new List<string>();

                foreach (Adat_Eszterga_Muveletek rekord in Adatok)
                {
                    string szoveg = $"UPDATE {tablaNev} SET ";
                    szoveg += $"Utolsó_Dátum=#{rekord.Utolsó_Dátum:yyyy-MM-dd}#, ";
                    szoveg += $"Utolsó_Üzemóra_Állás={rekord.Utolsó_Üzemóra_Állás} ";
                    szoveg += $"WHERE ID = {rekord.ID}";

                    sqlLista.Add(szoveg);
                }

                MyA.ABMódosítás(hely, jelszo, sqlLista);
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
        public void Megjegyzes_Modositas(Adat_Eszterga_Muveletek Adat)
        {
            try
            {
                string szoveg = $"UPDATE {tablaNev} SET Megjegyzés='{Adat.Megjegyzés}' WHERE ID={Adat.ID}";
                MyA.ABMódosítás(hely, jelszo, szoveg);
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
        public void Rendezes(Adat_Eszterga_Muveletek Adat, int KovetkezoID)
        {
            try
            {
                string szoveg = $"UPDATE {tablaNev} SET ID = {KovetkezoID} WHERE ID = {Adat.ID}";
                MyA.ABMódosítás(hely, jelszo, szoveg);
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
        private string UpdateSzoveg(Adat_Eszterga_Muveletek Adat)
        {
            return $"UPDATE {tablaNev} SET " +
                   $"Művelet='{Adat.Művelet}', " +
                   $"Egység={Adat.Egység}, " +
                   $"Mennyi_Dátum={Adat.Mennyi_Dátum}, " +
                   $"Mennyi_Óra={Adat.Mennyi_Óra}, " +
                   $"Státus={(Adat.Státus ? "True" : "False")}, " +
                   $"Utolsó_Dátum=#{Adat.Utolsó_Dátum:yyyy-MM-dd}#, " +
                   $"Utolsó_Üzemóra_állás={Adat.Utolsó_Üzemóra_Állás} " +
                   $"WHERE ID={Adat.ID}";
        }

        public void MeglevoMuvelet_Modositas(Adat_Eszterga_Muveletek Adat)
        {
            try
            {
                string sql = UpdateSzoveg(Adat);
                MyA.ABMódosítás(hely, jelszo, sql);
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
        public void MuveletCsere(Adat_Eszterga_Muveletek rekord1, Adat_Eszterga_Muveletek rekord2)
        {
            try
            {
                Adat_Eszterga_Muveletek masolat1 = new Adat_Eszterga_Muveletek
                    (
                        rekord1.ID,
                        rekord2.Művelet,
                        rekord2.Egység,
                        rekord2.Mennyi_Dátum,
                        rekord2.Mennyi_Óra,
                        rekord2.Státus,
                        rekord2.Utolsó_Dátum,
                        rekord2.Utolsó_Üzemóra_Állás,
                        rekord2.Megjegyzés
                    );

                Adat_Eszterga_Muveletek masolat2 = new Adat_Eszterga_Muveletek
                    (
                        rekord2.ID,
                        rekord1.Művelet,
                        rekord1.Egység,
                        rekord1.Mennyi_Dátum,
                        rekord1.Mennyi_Óra,
                        rekord1.Státus,
                        rekord1.Utolsó_Dátum,
                        rekord1.Utolsó_Üzemóra_Állás,
                        rekord1.Megjegyzés
                    );

                List<string> sqlLista = new List<string>
                {
                    UpdateSzoveg(masolat1),
                    UpdateSzoveg(masolat2)
                };

                MyA.ABMódosítás(hely, jelszo, sqlLista);
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
        public void MuveletSorrend(int ElsoID, int MasodikID)
        {
            try
            {
                List<string> sqlLista = new List<string>();

                sqlLista.Add($"UPDATE {tablaNev} SET ID = ID + 1 WHERE ID >= {MasodikID}");
                if (ElsoID < MasodikID)
                    sqlLista.Add($"UPDATE {tablaNev} SET ID = {MasodikID} WHERE ID = {ElsoID}");
                else
                    sqlLista.Add($"UPDATE {tablaNev} SET ID = {MasodikID} WHERE ID = {ElsoID + 1}");

                MyA.ABMódosítás(hely, jelszo, sqlLista);
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

        public void Rendezes()
        {
            List<Adat_Eszterga_Muveletek> rekordok = Lista_Adatok().OrderBy(a => a.ID).ToList();

            int ujID = 1;
            foreach (Adat_Eszterga_Muveletek rekord in rekordok)
            {
                if (rekord.ID != ujID)
                {
                    Adat_Eszterga_Muveletek adat = new Adat_Eszterga_Muveletek(rekord.ID);
                    Rendezes(adat, ujID);
                    rekord.ID = ujID;
                }
                ujID++;
            }
        }
    }
}
