using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Kiadás_Összesítő
    {
        string hely;
        readonly string jelszó = "plédke";

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely}\adatok\főkönyv\kiadás{Év}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kiadásiösszesítőtábla(hely.KönyvSzerk());
        }

        public List<Adat_Kiadás_összesítő> Lista_adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = "SELECT * FROM tábla  ";

            List<Adat_Kiadás_összesítő> Adatok = new List<Adat_Kiadás_összesítő>();
            Adat_Kiadás_összesítő Adat;

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
                                Adat = new Adat_Kiadás_összesítő(

                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["forgalomban"].ToÉrt_Int(),
                                    rekord["tartalék"].ToÉrt_Int(),
                                    rekord["kocsiszíni"].ToÉrt_Int(),
                                    rekord["félreállítás"].ToÉrt_Int(),
                                    rekord["főjavítás"].ToÉrt_Int(),
                                    rekord["személyzet"].ToÉrt_Int()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Törlés(string Telephely, int Év, DateTime Dátum, string Napszak)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = $@"DELETE FROM tábla where dátum=#{Dátum:MM-dd-yyyy}# and napszak='{Napszak}'";
                MyA.ABtörlés(hely, jelszó, szöveg);
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


        public void Rögzítés(string Telephely, int Év, Adat_Kiadás_összesítő Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                string szöveg = "INSERT INTO tábla (dátum, napszak, típus, forgalomban, tartalék, kocsiszíni, félreállítás, főjavítás, személyzet) VALUES (";
                szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', ";
                szöveg += $"'{Adat.Napszak}', ";
                szöveg += $"'{Adat.Típus}', ";
                szöveg += $"{Adat.Forgalomban}, ";
                szöveg += $"{Adat.Tartalék}, ";
                szöveg += $"{Adat.Kocsiszíni}, ";
                szöveg += $"{Adat.Félreállítás}, ";
                szöveg += $"{Adat.Főjavítás}, ";
                szöveg += $"{Adat.Személyzet}) ";
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


        //elkopó
        public List<Adat_Kiadás_összesítő> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Kiadás_összesítő> Adatok = new List<Adat_Kiadás_összesítő>();
            Adat_Kiadás_összesítő Adat;

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
                                Adat = new Adat_Kiadás_összesítő(

                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["forgalomban"].ToÉrt_Int(),
                                    rekord["tartalék"].ToÉrt_Int(),
                                    rekord["kocsiszíni"].ToÉrt_Int(),
                                    rekord["félreállítás"].ToÉrt_Int(),
                                    rekord["főjavítás"].ToÉrt_Int(),
                                    rekord["személyzet"].ToÉrt_Int()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }
    }
}

