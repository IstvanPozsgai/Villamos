using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos_Kezelők
{
    public class Kezelo_Eszterga_Muveletek
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Eszterga_Karbantartás.mdb";
        readonly string jelszo = "bozaim";
        readonly string Tabla_Muvelet = "Műveletek";
        public List<Adat_Eszterga_Muveletek> Lista_Adatok()
        {
            string szoveg = $"SELECT * FROM {Tabla_Muvelet} ORDER BY ID  ";
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
                string szoveg = $"INSERT INTO {Tabla_Muvelet} (ID, Művelet, Egység, Mennyi_Dátum, Mennyi_Óra, Státus, Utolsó_Dátum, Utolsó_Üzemóra_Állás) VALUES(";
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
                    string szoveg = $"UPDATE {Tabla_Muvelet} SET {oszlop} WHERE ID={rekord.ID}";
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
        private int Sorszam()
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
                    string szoveg = $"UPDATE {Tabla_Muvelet} SET ";
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
                string szoveg = $"UPDATE {Tabla_Muvelet} SET Megjegyzés='{Adat.Megjegyzés}' WHERE ID={Adat.ID}";
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
                string szoveg = $"UPDATE {Tabla_Muvelet} SET ID = {KovetkezoID} WHERE ID = {Adat.ID}";
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
        public void MeglevoMuvelet_Modositas(Adat_Eszterga_Muveletek Adat)
        {
            try
            {
                string szoveg = $"UPDATE {Tabla_Muvelet} SET ";
                szoveg += $"Művelet='{Adat.Művelet}', ";
                szoveg += $"Egység={Adat.Egység}, ";
                szoveg += $"Mennyi_Dátum={Adat.Mennyi_Dátum}, ";
                szoveg += $"Mennyi_Óra={Adat.Mennyi_Óra}, ";
                szoveg += $"Státus={(Adat.Státus ? "True" : "False")}, ";
                szoveg += $"Utolsó_Dátum=#{Adat.Utolsó_Dátum:yyyy-MM-dd}#, ";
                szoveg += $"Utolsó_Üzemóra_állás={Adat.Utolsó_Üzemóra_Állás} ";
                szoveg += $"WHERE ID = {Adat.ID} ";
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
        public void MuveletCsere(Adat_Eszterga_Muveletek rekord1, Adat_Eszterga_Muveletek rekord2)
        {
            try
            {
                string szoveg1 = $"UPDATE {Tabla_Muvelet} SET Művelet='{rekord2.Művelet}', ";
                szoveg1 += $"Egység={rekord2.Egység}, ";
                szoveg1 += $"Mennyi_Dátum={rekord2.Mennyi_Dátum}, ";
                szoveg1 += $"Mennyi_Óra={rekord2.Mennyi_Óra}, ";
                szoveg1 += $"Státus={(rekord2.Státus ? "True" : "False")},";
                szoveg1 += $"Utolsó_Dátum=#{rekord2.Utolsó_Dátum:yyyy-MM-dd}#,";
                szoveg1 += $"Utolsó_Üzemóra_állás={rekord2.Utolsó_Üzemóra_Állás} ";
                szoveg1 += $"WHERE ID={rekord1.ID}";

                string szoveg2 = $"UPDATE {Tabla_Muvelet} SET Művelet='{rekord1.Művelet}', ";
                szoveg2 += $"Egység={rekord1.Egység}, ";
                szoveg2 += $"Mennyi_Dátum={rekord1.Mennyi_Dátum}, ";
                szoveg2 += $"Mennyi_Óra={rekord1.Mennyi_Óra}, ";
                szoveg2 += $"Státus={(rekord1.Státus ? "True" : "False")},";
                szoveg2 += $"Utolsó_Dátum=#{rekord1.Utolsó_Dátum:yyyy-MM-dd}#,";
                szoveg2 += $"Utolsó_Üzemóra_állás={rekord1.Utolsó_Üzemóra_Állás} ";
                szoveg2 += $"WHERE ID={rekord2.ID}";

                List<string> SQL = new List<string> { szoveg1, szoveg2 };

                MyA.ABMódosítás(hely, jelszo, SQL);
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
                string szoveg, szovegMozog;

                if (ElsoID < MasodikID)
                {
                    szoveg = $"UPDATE {Tabla_Muvelet} SET ID = ID + 1 WHERE ID >= {MasodikID}";
                    MyA.ABMódosítás(hely, jelszo, szoveg);

                    szovegMozog = $"UPDATE {Tabla_Muvelet} SET ID = {MasodikID} WHERE ID = {ElsoID}";
                    MyA.ABMódosítás(hely, jelszo, szovegMozog);
                }
                else
                {
                    szoveg = $"UPDATE {Tabla_Muvelet} SET ID = ID + 1 WHERE ID >= {MasodikID}";
                    MyA.ABMódosítás(hely, jelszo, szoveg);

                    szovegMozog = $"UPDATE {Tabla_Muvelet} SET ID = {MasodikID} WHERE ID = {ElsoID + 1}";
                    MyA.ABMódosítás(hely, jelszo, szovegMozog);
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
    public class Kezelő_Eszterga_Üzemóra
    {
        readonly string jelszo = "bozaim";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Eszterga_Karbantartás.mdb";
        readonly string Tabla_Uzem = "Üzemóra";
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
    public class Kezelo_Eszterga_Muveletek_Naplo
    {
        readonly string jelszo = "bozaim";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Eszterga_Karbantartás_{DateTime.Now.Year}_Napló.mdb";
        readonly string Tabla_Naplo = "Műveletek_Napló";
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
        public void EsztergaNaplozas(List<Adat_Eszterga_Muveletek_Naplo> Adatok)
        {
            try
            {
                List<string> sqlLista = new List<string>();

                foreach (Adat_Eszterga_Muveletek_Naplo rekord in Adatok)
                {
                    string szoveg = $"INSERT INTO {Tabla_Naplo} (ID, Művelet, Mennyi_Dátum, Mennyi_Óra, Utolsó_Dátum, Utolsó_Üzemóra_Állás, [Megjegyzés], Rögzítő, Rögzítés_Dátuma) VALUES (";
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
        public void UtolagUpdate(Adat_Eszterga_Muveletek_Naplo újAdat, DateTime eredetiDatum)
        {
            try
            {
                string szoveg = $"UPDATE {Tabla_Naplo} SET ";
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
