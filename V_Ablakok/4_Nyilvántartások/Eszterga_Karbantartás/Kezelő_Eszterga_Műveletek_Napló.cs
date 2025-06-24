using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Eszterga_Karbantartás
{
    public class Kezelo_Eszterga_Muveletek_Naplo
    {
        readonly string jelszo = "bozaim";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Eszterga_Karbantartás_{DateTime.Now.Year}_Napló.mdb".KönyvSzerk();
        readonly string tablaNev = "Műveletek_Napló";
        public Kezelo_Eszterga_Muveletek_Naplo()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Eszterga_Karbantartas_Naplo(hely);
        }
        public List<Adat_Eszterga_Muveletek_Naplo> Lista_Adatok()
        {
            string szoveg = "SELECT * FROM Műveletek_Napló ORDER BY ID ";
            List<Adat_Eszterga_Muveletek_Naplo> Adatok = new List<Adat_Eszterga_Muveletek_Naplo>();
            Adat_Eszterga_Muveletek_Naplo Adat;

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
                                Adat = new Adat_Eszterga_Muveletek_Naplo(
                                        rekord["ID"].ToÉrt_Int(),
                                        rekord["Művelet"].ToStrTrim(),
                                        rekord["Mennyi_Dátum"].ToÉrt_Int(),
                                        rekord["Mennyi_Óra"].ToÉrt_Int(),
                                        rekord["Utolsó_Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Utolsó_Üzemóra_Állás"].ToÉrt_Long(),
                                        rekord["Megjegyzés"].ToStrTrim(),
                                        rekord["Rögzítő"].ToStrTrim(),
                                        rekord["Rögzítés_Dátuma"].ToÉrt_DaTeTime());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        // JAVÍTANDÓ:Ez rögzítés
        public void EsztergaNaplozas(List<Adat_Eszterga_Muveletek_Naplo> Adatok)
        {
            try
            {
                List<string> sqlLista = new List<string>();

                foreach (Adat_Eszterga_Muveletek_Naplo rekord in Adatok)
                {
                    string szoveg = $"INSERT INTO {tablaNev} (ID, Művelet, Mennyi_Dátum, Mennyi_Óra, Utolsó_Dátum, Utolsó_Üzemóra_Állás, [Megjegyzés], Rögzítő, Rögzítés_Dátuma) VALUES (";
                    szoveg += $"{rekord.ID}, ";
                    szoveg += $"'{rekord.Művelet}', ";
                    szoveg += $"{rekord.Mennyi_Dátum}, ";
                    szoveg += $"{rekord.Mennyi_Óra}, ";
                    szoveg += $"#{rekord.Utolsó_Dátum:yyyy-MM-dd}#, ";
                    szoveg += $"{rekord.Utolsó_Üzemóra_Állás}, ";
                    szoveg += $"'{rekord.Megjegyzés}', ";
                    szoveg += $"'{rekord.Rögzítő}', ";
                    szoveg += $"#{rekord.Rögzítés_Dátuma:yyyy-MM-dd}#)";

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
        // JAVÍTANDÓ:az módosítás
        public void UtolagModositas(Adat_Eszterga_Muveletek_Naplo újAdat, DateTime eredetiDatum)
        {
            try
            {
                string szoveg = $"UPDATE {tablaNev} SET ";
                szoveg += $"Utolsó_Dátum = #{újAdat.Utolsó_Dátum:yyyy-MM-dd}#, ";
                szoveg += $"Utolsó_Üzemóra_Állás = {újAdat.Utolsó_Üzemóra_Állás}, ";
                szoveg += $"Megjegyzés = '{újAdat.Megjegyzés}', ";
                szoveg += $"Rögzítő = '{újAdat.Rögzítő}', ";
                szoveg += $"Rögzítés_Dátuma = #{újAdat.Rögzítés_Dátuma:yyyy-MM-dd}# ";
                szoveg += $"WHERE ID = {újAdat.ID} AND Utolsó_Dátum = #{eredetiDatum:yyyy-MM-dd}#";

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
    }
}
