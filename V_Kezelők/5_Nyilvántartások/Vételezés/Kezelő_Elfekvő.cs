using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Elfekvő
    {
        readonly string hely;
        readonly string jelszó = "bozaim";
        readonly string táblanév = "Tbl_Elfekvő";

        public Kezelő_Elfekvő()
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Elfekvő\Elfekvő.mdb".KönyvSzerk();
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Elfekvőtábla(hely);
        }

        public List<Adat_Elfekvő> Lista_Adatok()
        {
            string szöveg = $"SELECT * FROM {táblanév}";
            List<Adat_Elfekvő> Adatok = new List<Adat_Elfekvő>();

            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";
            try
            {
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
                                    Adat_Elfekvő Adat = new Adat_Elfekvő(
                                        rekord["Id"].ToÉrt_Long(),
                                        rekord["Anyag"].ToStrTrim(),
                                        rekord["Anyag rövid szövege"].ToStrTrim(),
                                        rekord["Raktárhely"].ToStrTrim(),
                                        rekord["Szabadon használható"].ToÉrt_Double(),
                                        rekord["Szab_felh_érték"].ToÉrt_Double(), // Módosított mezőnév olvasása
                                        rekord["Sarzs"].ToStrTrim(),
                                        rekord["Utolsó mozgás"].ToÉrt_DaTeTime()
                                    );
                                    Adatok.Add(Adat);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Elfekvő Adat)
        {
            try
            {
                string szöveg = $"INSERT INTO {táblanév} ([Anyag], [Anyag rövid szövege], [Raktárhely], [Szabadon használható], [Szab_felh_érték], [Sarzs], [Utolsó mozgás]) VALUES (";
                szöveg += $"'{Adat.Anyag}', ";
                szöveg += $"'{Adat.Anyag_rövid_szövege?.Replace("'", "''")}', ";
                szöveg += $"'{Adat.Raktárhely}', ";
                szöveg += $"{Adat.Szabadon_használható.ToString().Replace(",", ".")}, ";
                szöveg += $"{Adat.Szab_felh_érték.ToString().Replace(",", ".")}, ";
                szöveg += $"'{Adat.Sarzs}', ";
                szöveg += $"'{Adat.Utolsó_mozgás:yyyy.MM.dd HH:mm:ss}')";

                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Tábla_Kiürítés()
        {
            try
            {
                string szöveg = $"DELETE FROM {táblanév}";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
            }
        }

        public void Tömeges_Rögzítés(List<Adat_Elfekvő> adatok)
        {
            try
            {
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Elfekvő Adat in adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév} ([Anyag], [Anyag rövid szövege], [Raktárhely], [Szabadon használható], [Szab_felh_érték], [Sarzs], [Utolsó mozgás]) VALUES (";
                    szöveg += $"'{Adat.Anyag}', ";
                    szöveg += $"'{Adat.Anyag_rövid_szövege?.Replace("'", "''")}', ";
                    szöveg += $"'{Adat.Raktárhely}', ";
                    szöveg += $"{Adat.Szabadon_használható.ToString().Replace(",", ".")}, ";
                    szöveg += $"{Adat.Szab_felh_érték.ToString().Replace(",", ".")}, ";
                    szöveg += $"'{Adat.Sarzs}', ";
                    szöveg += $"'{Adat.Utolsó_mozgás:yyyy.MM.dd HH:mm:ss}')";
                    SzövegGy.Add(szöveg);
                }

                MyA.ABMódosítás(hely, jelszó, SzövegGy);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került a tömeges rögzítés közben.", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}