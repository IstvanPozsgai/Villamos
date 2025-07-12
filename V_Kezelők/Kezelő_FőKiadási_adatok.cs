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
    public class Kezelő_FőKiadási_adatok
    {
        string hely;
        readonly string jelszó = "pozsi";
        readonly string táblanév = "kiadástábla";

        private void FájlBeállítás(int Év)
        {
            hely = $@"{Application.StartupPath}\főmérnökség\adatok\{Év}\{Év}_kiadási_adatok.mdb".KönyvSzerk();
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kiadásiösszesítőfőmérnöktábla(hely);
        }

        public List<Adat_FőKiadási_adatok> Lista_adatok(int Év)
        {
            FájlBeállítás(Év);
            List<Adat_FőKiadási_adatok> Adatok = new List<Adat_FőKiadási_adatok>();
            string szöveg = $"SELECT * FROM  {táblanév}";

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
                                Adat_FőKiadási_adatok Adat = new Adat_FőKiadási_adatok(
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["forgalomban"].ToÉrt_Long(),
                                    rekord["tartalék"].ToÉrt_Long(),
                                    rekord["kocsiszíni"].ToÉrt_Long(),
                                    rekord["félreállítás"].ToÉrt_Long(),
                                    rekord["főjavítás"].ToÉrt_Long(),
                                    rekord["személyzet"].ToÉrt_Long(),
                                    rekord["kiadás"].ToÉrt_Long(),
                                    rekord["főkategória"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["altípus"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["szolgálat"].ToStrTrim(),
                                    rekord["telephelyitípus"].ToStrTrim(),
                                    rekord["munkanap"].ToÉrt_Long()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Módosítás(int Év, List<Adat_FőKiadási_adatok> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_FőKiadási_adatok Adat in Adatok)
                {
                    string szöveg = $"UPDATE {táblanév}  SET ";
                    szöveg += $"Főkategória='{Adat.Főkategória}', ";
                    szöveg += $"Típus='{Adat.Típus}', ";
                    szöveg += $"Altípus='{Adat.Altípus}', ";
                    szöveg += $"szolgálat='{Adat.Szolgálat}', ";
                    szöveg += $"forgalomban={Adat.Forgalomban}, ";
                    szöveg += $"tartalék={Adat.Tartalék}, ";
                    szöveg += $"kocsiszíni={Adat.Forgalomban}, ";
                    szöveg += $"félreállítás={Adat.Félreállítás}, ";
                    szöveg += $"főjavítás={Adat.Főjavítás}, ";
                    szöveg += $"munkanap={Adat.Munkanap}, ";
                    szöveg += $"kiadás={Adat.Kiadás}, ";
                    szöveg += $"személyzet={Adat.Személyzet}";
                    szöveg += $" WHERE [dátum]=#{Adat.Dátum:M-d-yy}# ";
                    szöveg += $" and napszak='{Adat.Napszak}'";
                    szöveg += $" and telephely='{Adat.Telephely}'";
                    szöveg += $" and telephelyitípus='{Adat.Telephelyitípus}'";
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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

        public void Rögzítés(int Év, List<Adat_FőKiadási_adatok> Adatok)
        {
            try
            {
                FájlBeállítás(Év);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_FőKiadási_adatok Adat in Adatok)
                {
                    string szöveg = $"INSERT INTO {táblanév}  (dátum, napszak, főkategória, típus, ";
                    szöveg += "altípus, telephely, szolgálat, telephelyitípus, ";
                    szöveg += "forgalomban, tartalék, kocsiszíni, félreállítás, ";
                    szöveg += "főjavítás, munkanap, kiadás, személyzet ) VALUES (";
                    szöveg += $"'{Adat.Dátum:yyyy.MM.dd}', ";     // dátum
                    szöveg += $"'{Adat.Napszak}', ";              // napszak
                    szöveg += $"'{Adat.Főkategória}', ";          // főkategória
                    szöveg += $"'{Adat.Típus}', ";                // típus

                    szöveg += $"'{Adat.Altípus}', ";         // altípus
                    szöveg += $"'{Adat.Telephely}', ";       // telephely
                    szöveg += $"'{Adat.Szolgálat}', ";       // szolgálat
                    szöveg += $"'{Adat.Telephelyitípus}', "; // telephelyitípus

                    szöveg += $"{Adat.Forgalomban}, ";    //  forgalomban
                    szöveg += $"{Adat.Tartalék}, ";       //  tartalék
                    szöveg += $"{Adat.Kocsiszíni}, ";     //  kocsiszíni
                    szöveg += $"{Adat.Félreállítás}, ";   //  félreállítás

                    szöveg += $"{Adat.Főjavítás}, ";    //  főjavítás
                    szöveg += $"{Adat.Munkanap}, ";     //  munkanap
                    szöveg += $"{Adat.Kiadás}, ";       //  kiadás
                    szöveg += $"{Adat.Személyzet}) ";   //  személyzet
                    SzövegGy.Add(szöveg);
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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

        public void Törlés(int Év, Adat_FőKiadási_adatok Adat)
        {
            try
            {
                FájlBeállítás(Év);
                string szöveg = $"DELETE FROM kiadástábla WHERE [dátum]=#{Adat.Dátum:M-d-yy}#";
                if (Adat.Napszak.Trim() != "")
                    szöveg += $" and napszak='{Adat.Napszak}' ";
                szöveg += $" and telephely='{Adat.Telephely.Trim()}'";
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

        //Elkopó
        public List<Adat_FőKiadási_adatok> Lista_adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_FőKiadási_adatok> Adatok = new List<Adat_FőKiadási_adatok>();

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
                                Adat_FőKiadási_adatok Adat = new Adat_FőKiadási_adatok(
                                    rekord["dátum"].ToÉrt_DaTeTime(),
                                    rekord["napszak"].ToStrTrim(),
                                    rekord["forgalomban"].ToÉrt_Long(),
                                    rekord["tartalék"].ToÉrt_Long(),
                                    rekord["kocsiszíni"].ToÉrt_Long(),
                                    rekord["félreállítás"].ToÉrt_Long(),
                                    rekord["főjavítás"].ToÉrt_Long(),
                                    rekord["személyzet"].ToÉrt_Long(),
                                    rekord["kiadás"].ToÉrt_Long(),
                                    rekord["főkategória"].ToStrTrim(),
                                    rekord["típus"].ToStrTrim(),
                                    rekord["altípus"].ToStrTrim(),
                                    rekord["telephely"].ToStrTrim(),
                                    rekord["szolgálat"].ToStrTrim(),
                                    rekord["telephelyitípus"].ToStrTrim(),
                                    rekord["munkanap"].ToÉrt_Long()
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
