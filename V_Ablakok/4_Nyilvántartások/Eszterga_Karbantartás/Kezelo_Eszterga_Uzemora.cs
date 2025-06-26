using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.V_Ablakok._4_Nyilvántartások.Eszterga_Karbantartás
{
    public class Kezelo_Eszterga_Uzemora
    {
        readonly string jelszo = "bozaim";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Eszterga_Karbantartás.mdb";
        readonly string Tabla_Uzem = "Üzemóra";
        // JAVÍTANDÓ:Hogy jön létre a fájl?

        public List<Adat_Eszterga_Uzemora> Lista_Adatok()
        {
            string szoveg = $"SELECT * FROM {Tabla_Uzem} ORDER BY Dátum, ID  ";
            List<Adat_Eszterga_Uzemora> Adatok = new List<Adat_Eszterga_Uzemora>();
            Adat_Eszterga_Uzemora Adat;

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
                                Adat = new Adat_Eszterga_Uzemora(
                                        rekord["ID"].ToÉrt_Int(),
                                        rekord["Üzemóra"].ToÉrt_Long(),
                                        rekord["Dátum"].ToÉrt_DaTeTime(),
                                        rekord["Státus"].ToÉrt_Bool());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
        public void Rogzites(Adat_Eszterga_Uzemora Adat)
        {
            try
            {
                string szoveg = $"INSERT INTO {Tabla_Uzem} (ID, Üzemóra, Dátum, Státus) VALUES(";
                szoveg += $"'{Sorszam()}', ";
                szoveg += $"{Adat.Uzemora}, ";
                szoveg += $"'{Adat.Dátum:yyyy-MM-dd}', ";
                szoveg += $"{(Adat.Státus ? "TRUE" : "FALSE")})";
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
        public void Torles(Adat_Eszterga_Uzemora Adat)
        {
            try
            {
                string szoveg = $"UPDATE {Tabla_Uzem} SET Státus=True WHERE ID={Adat.ID}";
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
        private int Sorszam()
        {
            int valasz = 1;
            try
            {
                List<Adat_Eszterga_Uzemora> Adatok = Lista_Adatok();
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
    }

}
