using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos_Kezelők
{
    public class Kezelő_Eszterga_Műveletek
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Eszterga_Karbantartás.mdb";
        readonly string jelszó = "bozaim";
        readonly string Tabla_Muvelet = "Műveletek";
        public List<Adat_Eszterga_Műveletek> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {Tabla_Muvelet} ORDER BY ID  ";
            List<Adat_Eszterga_Műveletek> Adatok = new List<Adat_Eszterga_Műveletek>();
            Adat_Eszterga_Műveletek Adat;

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
                                Adat = new Adat_Eszterga_Műveletek(
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
        public void Rögzítés(Adat_Eszterga_Műveletek Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {Tabla_Muvelet} (ID, Művelet, Egység, Mennyi_Dátum, Mennyi_Óra, Státus, Utolsó_Dátum, Utolsó_Üzemóra_Állás) VALUES(";
                szöveg += $"'{Sorszám()}', ";
                szöveg += $"'{Adat.Művelet}', ";
                szöveg += $"{Adat.Egység}, ";
                szöveg += $"{Adat.Mennyi_Dátum}, ";
                szöveg += $"{Adat.Mennyi_Óra}, ";
                szöveg += $"{(Adat.Státus ? "True" : "False")}, ";
                szöveg += $"#{Adat.Utolsó_Dátum:yyyy-MM-dd}#, ";
                szöveg += $"{Adat.Utolsó_Üzemóra_Állás})";
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
        public void Törlés(Adat_Eszterga_Műveletek Adat, bool törlés)
        {
            try
            {
                string oszlop = törlés ? "Státus=True" : "Megjegyzés=NULL";
                string szöveg = $"UPDATE {Tabla_Muvelet} SET {oszlop} WHERE ID={Adat.ID}";
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
        private int Sorszám()
        {
            int válasz = 1;
            try
            {
                List<Adat_Eszterga_Műveletek> Adatok = Lista_Adatok();
                if (Adatok.Count > 0) válasz = Adatok.Max(a => a.ID) + 1;
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
            return válasz;
        }
        public void Módosítás(Adat_Eszterga_Műveletek Adat)
        {
            try
            {
                string szöveg = $"UPDATE {Tabla_Muvelet} SET ";
                szöveg += $"Utolsó_Dátum=#{Adat.Utolsó_Dátum:yyyy-MM-dd}#, ";
                szöveg += $"Utolsó_Üzemóra_Állás={Adat.Utolsó_Üzemóra_Állás} ";
                szöveg += $"WHERE ID = {Adat.ID}";
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
        public void Megjegyzés_Módosítás(Adat_Eszterga_Műveletek Adat)
        {
            try
            {
                string szöveg = $"UPDATE {Tabla_Muvelet} SET Megjegyzés='{Adat.Megjegyzés}' WHERE ID={Adat.ID}";
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
        public void Rendezés(Adat_Eszterga_Műveletek Adat, int KovetkezoID)
        {
            try
            {
                string szöveg = $"UPDATE {Tabla_Muvelet} SET ID = {KovetkezoID} WHERE ID = {Adat.ID}";
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
        public void MeglévőMűvelet_Módosítás(Adat_Eszterga_Műveletek Adat)
        {
            try
            {
                string szöveg = $"UPDATE {Tabla_Muvelet} SET ";
                szöveg += $"Művelet='{Adat.Művelet}', ";
                szöveg += $"Egység={Adat.Egység}, ";
                szöveg += $"Mennyi_Dátum={Adat.Mennyi_Dátum}, ";
                szöveg += $"Mennyi_Óra={Adat.Mennyi_Óra}, ";
                szöveg += $"Státus={(Adat.Státus ? "True" : "False")}, ";
                szöveg += $"Utolsó_Dátum=#{Adat.Utolsó_Dátum:yyyy-MM-dd}#, ";
                szöveg += $"Utolsó_Üzemóra_állás={Adat.Utolsó_Üzemóra_Állás} ";
                szöveg += $"WHERE ID = {Adat.ID} ";
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
        public void MűveletCsere(Adat_Eszterga_Műveletek rekord1, Adat_Eszterga_Műveletek rekord2)
        {
            try
            {
                string szöveg1 = $"UPDATE {Tabla_Muvelet} SET Művelet='{rekord2.Művelet}', ";
                szöveg1 += $"Egység={rekord2.Egység}, ";
                szöveg1 += $"Mennyi_Dátum={rekord2.Mennyi_Dátum}, ";
                szöveg1 += $"Mennyi_Óra={rekord2.Mennyi_Óra}, ";
                szöveg1 += $"Státus={(rekord2.Státus ? "True" : "False")},";
                szöveg1 += $"Utolsó_Dátum=#{rekord2.Utolsó_Dátum:yyyy-MM-dd}#,";
                szöveg1 += $"Utolsó_Üzemóra_állás={rekord2.Utolsó_Üzemóra_Állás} ";
                szöveg1 += $"WHERE ID={rekord1.ID}";

                string szöveg2 = $"UPDATE {Tabla_Muvelet} SET Művelet='{rekord1.Művelet}', ";
                szöveg2 += $"Egység={rekord1.Egység}, ";
                szöveg2 += $"Mennyi_Dátum={rekord1.Mennyi_Dátum}, ";
                szöveg2 += $"Mennyi_Óra={rekord1.Mennyi_Óra}, ";
                szöveg2 += $"Státus={(rekord1.Státus ? "True" : "False")},";
                szöveg2 += $"Utolsó_Dátum=#{rekord1.Utolsó_Dátum:yyyy-MM-dd}#,";
                szöveg2 += $"Utolsó_Üzemóra_állás={rekord1.Utolsó_Üzemóra_Állás} ";
                szöveg2 += $"WHERE ID={rekord2.ID}";

                List<string> SQL = new List<string> { szöveg1, szöveg2 };

                MyA.ABMódosítás(hely, jelszó, SQL);
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
        public void MűveletSorrend(int ElsoID, int MasodikID)
        {
            try
            {
                string szöveg, szövegMozog;

                if (ElsoID < MasodikID)
                {
                    szöveg = $"UPDATE {Tabla_Muvelet} SET ID = ID + 1 WHERE ID >= {MasodikID}";
                    MyA.ABMódosítás(hely, jelszó, szöveg);

                    szövegMozog = $"UPDATE {Tabla_Muvelet} SET ID = {MasodikID} WHERE ID = {ElsoID}";
                    MyA.ABMódosítás(hely, jelszó, szövegMozog);
                }
                else
                {
                    szöveg = $"UPDATE {Tabla_Muvelet} SET ID = ID + 1 WHERE ID >= {MasodikID}";
                    MyA.ABMódosítás(hely, jelszó, szöveg);

                    szövegMozog = $"UPDATE {Tabla_Muvelet} SET ID = {MasodikID} WHERE ID = {ElsoID + 1}";
                    MyA.ABMódosítás(hely, jelszó, szövegMozog);
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
        readonly string jelszó = "bozaim";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Eszterga_Karbantartás.mdb";
        readonly string Tabla_Uzem = "Üzemóra";
        public List<Adat_Eszterga_Üzemóra> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {Tabla_Uzem} ORDER BY Dátum, ID  ";
            List<Adat_Eszterga_Üzemóra> Adatok = new List<Adat_Eszterga_Üzemóra>();
            Adat_Eszterga_Üzemóra Adat;

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
                                Adat = new Adat_Eszterga_Üzemóra(
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
        public void Rögzítés(Adat_Eszterga_Üzemóra Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {Tabla_Uzem} (ID, Üzemóra, Dátum, Státus) VALUES(";
                szöveg += $"'{Sorszám()}', ";
                szöveg += $"{Adat.Üzemóra}, ";
                szöveg += $"'{Adat.Dátum:yyyy-MM-dd}', ";
                szöveg += $"{(Adat.Státus ? "TRUE" : "FALSE")})";
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
        public void Törlés(Adat_Eszterga_Üzemóra Adat)
        {
            try
            {
                string szöveg = $"UPDATE {Tabla_Uzem} SET Státus=True WHERE ID={Adat.ID}";
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
        private int Sorszám()
        {
            int válasz = 1;
            try
            {
                List<Adat_Eszterga_Üzemóra> Adatok = Lista_Adatok();
                if (Adatok.Count > 0) válasz = Adatok.Max(a => a.ID) + 1;
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
            return válasz;
        }
    }
    public class Kezelő_Eszterga_Műveletek_Napló
    {
        readonly string jelszó = "bozaim";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Eszterga_Karbantartás_{DateTime.Now.Year}_Napló.mdb";
        readonly string Tabla_Napló = "Műveletek_Napló";
        public List<Adat_Eszterga_Műveletek_Napló> Lista_Adatok()
        {
           string szöveg = "SELECT * FROM Műveletek_Napló ORDER BY ID ";
            List<Adat_Eszterga_Műveletek_Napló> Adatok = new List<Adat_Eszterga_Műveletek_Napló>();
            Adat_Eszterga_Műveletek_Napló Adat;

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
                                Adat = new Adat_Eszterga_Műveletek_Napló(
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
        public void EsztergaNaplózás(Adat_Eszterga_Műveletek_Napló Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {Tabla_Napló} (ID, Művelet, Mennyi_Dátum, Mennyi_Óra, Utolsó_Dátum, Utolsó_Üzemóra_Állás, [Megjegyzés], Rögzítő, Rögzítés_Dátuma) VALUES (";
                szöveg += $"{Adat.ID}, ";
                szöveg += $"'{Adat.Művelet}', ";
                szöveg += $"{Adat.Mennyi_Dátum}, ";
                szöveg += $"{Adat.Mennyi_Óra}, ";
                szöveg += $"#{Adat.Utolsó_Dátum:yyyy-MM-dd}#, ";
                szöveg += $"{Adat.Utolsó_Üzemóra_Állás}, ";
                szöveg += $"'{Adat.Megjegyzés}', ";
                szöveg += $"'{Adat.Rögzítő}', ";
                szöveg += $"#{Adat.Rögzítés_Dátuma:yyyy-MM-dd}#)";
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
        public void Update(Adat_Eszterga_Műveletek_Napló újAdat, DateTime eredetiDatum)
        {
            try
            {
                string szöveg = $"UPDATE {Tabla_Napló} SET ";
                szöveg += $"Utolsó_Dátum = #{újAdat.Utolsó_Dátum:yyyy-MM-dd}#, ";
                szöveg += $"Utolsó_Üzemóra_Állás = {újAdat.Utolsó_Üzemóra_Állás}, ";
                szöveg += $"Megjegyzés = '{újAdat.Megjegyzés}', ";
                szöveg += $"Rögzítő = '{újAdat.Rögzítő}', ";
                szöveg += $"Rögzítés_Dátuma = #{újAdat.Rögzítés_Dátuma:yyyy-MM-dd}# ";
                szöveg += $"WHERE ID = {újAdat.ID} AND Utolsó_Dátum = #{eredetiDatum:yyyy-MM-dd}#";

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
