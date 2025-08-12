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
    public class Kezelő_Eszterga_Műveletek
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Eszterga_Karbantartás.mdb".KönyvSzerk();
        readonly string jelszo = "bozaim";
        readonly string tablaNev = "Műveletek";

        public Kezelő_Eszterga_Műveletek()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Eszterga_Karbantartás(hely);
        }
        /// <summary>
        /// Lekéri az esztergaműveletek listáját az adatbázisból.
        /// </summary>
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

        /// <summary>
        /// Új műveleti rekordot rögzít az adatbázisban.
        /// </summary>
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

        /// <summary>
        /// A megadott rekordokat logikailag törli (Státusz beállítása vagy Megjegyzés nullázása).
        /// </summary>
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

        /// <summary>
        /// Meghatározza a következő elérhető azonosítót az új rekordhoz.
        /// </summary>
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

        /// <summary>
        /// Több rekord "Utolsó_Dátum" és "Utolsó_Üzemóra_Állás" mezőit frissíti.
        /// </summary>
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

        /// <summary>
        /// Ez a megjegyzés módosítására való
        /// </summary>
        /// <param name="Adat"></param>
        public void Modositas_Megjegyzes(string Megjegyzés, int ID)
        {
            try
            {
                string szoveg = $"UPDATE {tablaNev} SET Megjegyzés='{Megjegyzés}' WHERE ID={ID}";
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


        /// <summary>
        /// Egy meglévő műveleti rekord összes mezőjét módosítja.
        /// </summary>
        public void Modositas_MeglevoMuvelet(Adat_Eszterga_Muveletek Adat)
        {
            try
            {
                string sql = $"UPDATE {tablaNev} SET " +
                   $"Művelet='{Adat.Művelet}', " +
                   $"Egység={Adat.Egység}, " +
                   $"Mennyi_Dátum={Adat.Mennyi_Dátum}, " +
                   $"Mennyi_Óra={Adat.Mennyi_Óra}, " +
                   $"Státus={(Adat.Státus ? "True" : "False")}, " +
                   $"Utolsó_Dátum=#{Adat.Utolsó_Dátum:yyyy-MM-dd}#, " +
                   $"Utolsó_Üzemóra_állás={Adat.Utolsó_Üzemóra_Állás} " +
                   $"WHERE ID={Adat.ID}";
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

        /// <summary>
        /// Két rekord ID-jét felcseréli az adatbázisban.
        /// </summary>
        public void Csere(int Id1, int Id2)
        {
            try
            {
                List<string> sqlLista = new List<string>
                {
                    $"UPDATE {tablaNev} SET ID = 0 WHERE ID = {Id1}",
                    $"UPDATE {tablaNev} SET ID = {Id1} WHERE ID = {Id2}",
                    $"UPDATE {tablaNev} SET ID = {Id2} WHERE ID = 0"
                };

                MyA.ABMódosítás(hely, jelszo, sqlLista);

                string torles = $"DELETE FROM {tablaNev} WHERE ID = 0";
                MyA.ABtörlés(hely, jelszo, torles);
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

        /// <summary>
        /// A rekordok sorrendjét módosítja a megadott új sorrend szerint.
        /// </summary>
        public void Sorrendezes(int elsoId, int masodikId)
        {
            try
            {
                if (elsoId < masodikId)
                    for (int i = elsoId; i < masodikId - 1; i++)
                        Csere(i, i + 1);
                else
                    for (int i = elsoId; i > masodikId + 1; i--)
                        Csere(i, i - 1);
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
