using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos.Kezelők
{
    public class Kezelő_Technológia
    {
        readonly string jelszó = "Bezzegh";
        string hely;


        private void FájlBeállítás(string Típus)
        {
            hely = $@"{Application.StartupPath}\Főmérnökség\adatok\Technológia\{Típus}.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Technológia_Adat(hely.KönyvSzerk());
        }

        public List<Adat_Technológia_Új> Lista_Adatok(string Típus)
        {
            FájlBeállítás(Típus);
            string szöveg = $"SELECT * FROM Technológia ";
            List<Adat_Technológia_Új> Adatok = new List<Adat_Technológia_Új>();
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
                                Adat_Technológia_Új Adat = new Adat_Technológia_Új(
                                    rekord["id"].ToÉrt_Long(),
                                    rekord["Részegység"].ToStrTrim(),
                                    rekord["Munka_utasítás_szám"].ToStrTrim(),
                                    rekord["Utasítás_Cím"].ToStrTrim(),
                                    rekord["Utasítás_leírás"].ToStrTrim(),
                                    rekord["Paraméter"].ToStrTrim(),
                                    rekord["Karb_ciklus_eleje"].ToÉrt_Int(),
                                    rekord["Karb_ciklus_vége"].ToÉrt_Int(),
                                    rekord["Érv_kezdete"].ToÉrt_DaTeTime(),
                                    rekord["Érv_vége"].ToÉrt_DaTeTime(),
                                    rekord["Szakmai_bontás"].ToStrTrim(),
                                    rekord["Munkaterületi_bontás"].ToStrTrim(),
                                    rekord["Altípus"].ToStrTrim(),
                                    rekord["Kenés"].ToÉrt_Bool());
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Típus, List<Adat_Technológia_Új> BeAdatok)
        {
            try
            {
                List<Adat_Technológia_Új> Adatok = Lista_Adatok(Típus);
                long id = 1;
                if (Adatok != null && Adatok.Count > 0) id = Adatok.Max(a => a.ID) + 1;

                List<string> SzövegGy = new List<string>();
                foreach (Adat_Technológia_Új Adat in BeAdatok)
                {
                    string szöveg = "INSERT INTO technológia ( iD,  részegység,  munka_utasítás_szám,  utasítás_Cím,  utasítás_leírás,  paraméter, " +
                                " karb_ciklus_eleje,  karb_ciklus_vége,  érv_kezdete,  érv_vége,  szakmai_bontás,  munkaterületi_bontás,  altípus,  kenés ) VALUES (";
                    szöveg += $"{id}, "; //id
                    szöveg += $"'{Adat.Részegység.Trim()}', "; // részegység
                    szöveg += $"'{Adat.Munka_utasítás_szám.Trim()}', ";//  munka_utasítás_szám
                    szöveg += $"'{Adat.Utasítás_Cím.Trim()}', ";//   utasítás_Cím
                    szöveg += $"'{Adat.Utasítás_leírás.Trim()}', ";//   utasítás_leírás
                    szöveg += $"'{Adat.Paraméter.Trim()}', ";//   paraméter
                    szöveg += $"{Adat.Karb_ciklus_eleje}, ";//  karb_ciklus_eleje
                    szöveg += $"{Adat.Karb_ciklus_vége}, ";//  karb_ciklus_vége
                    szöveg += $"'{Adat.Érv_kezdete:yyyy.MM.dd}', ";//   érv_kezdete
                    szöveg += $"'{Adat.Érv_vége:yyyy.MM.dd}', ";//    érv_vége
                    szöveg += $"'{Adat.Szakmai_bontás.Trim()}', ";//     szakmai_bontás
                    szöveg += $"'{Adat.Munkaterületi_bontás.Trim()}',";//     munkaterületi_bontás
                    szöveg += $"'{Adat.Altípus.Trim()}', ";//    altípus
                    szöveg += $"{Adat.Kenés}) ";//   kenés
                    SzövegGy.Add(szöveg);
                    id++;

                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw new HibásBevittAdat("Az adatok nem kerültek rögzítésre.");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new HibásBevittAdat("Az adatok nem kerültek rögzítésre.");
            }

        }

        public void Rögzítés(string Típus, Adat_Technológia_Új Adat)
        {
            try
            {
                List<Adat_Technológia_Új> Adatok = Lista_Adatok(Típus);
                long id = 1;
                string szöveg;
                if (Adatok != null && Adatok.Count > 0)
                    if (Adatok.Where(a => a.ID == Adat.ID).FirstOrDefault() != null)
                        id = Adat.ID;
                    else
                        id = Adatok.Max(a => a.ID) + 1;

                if (Adat.ID == 0 || Adatok.Where(a => a.ID == Adat.ID).FirstOrDefault() == null)
                {

                    szöveg = "INSERT INTO technológia ( iD,  részegység,  munka_utasítás_szám,  utasítás_Cím,  utasítás_leírás,  paraméter, " +
                             " karb_ciklus_eleje,  karb_ciklus_vége,  érv_kezdete,  érv_vége,  szakmai_bontás,  munkaterületi_bontás,  altípus,  kenés ) VALUES (";
                    szöveg += $"{id}, "; //id
                    szöveg += $"'{Adat.Részegység.Trim()}', "; // részegység
                    szöveg += $"'{Adat.Munka_utasítás_szám.Trim()}', ";//  munka_utasítás_szám
                    szöveg += $"'{Adat.Utasítás_Cím.Trim()}', ";//   utasítás_Cím
                    szöveg += $"'{Adat.Utasítás_leírás.Trim()}', ";//   utasítás_leírás
                    szöveg += $"'{Adat.Paraméter.Trim()}', ";//   paraméter
                    szöveg += $"{Adat.Karb_ciklus_eleje}, ";//  karb_ciklus_eleje
                    szöveg += $"{Adat.Karb_ciklus_vége}, ";//  karb_ciklus_vége
                    szöveg += $"'{Adat.Érv_kezdete:yyyy.MM.dd}', ";//   érv_kezdete
                    szöveg += $"'{Adat.Érv_vége:yyyy.MM.dd}', ";//    érv_vége
                    szöveg += $"'{Adat.Szakmai_bontás.Trim()}', ";//     szakmai_bontás
                    szöveg += $"'{Adat.Munkaterületi_bontás.Trim()}',";//     munkaterületi_bontás
                    szöveg += $"'{Adat.Altípus.Trim()}', ";//    altípus
                    szöveg += $"{Adat.Kenés}) ";//   kenés
                }
                else
                {
                    szöveg = "UPDATE technológia SET ";
                    szöveg += $"részegység='{Adat.Részegység.Trim()}', ";
                    szöveg += $"munka_utasítás_szám='{Adat.Munka_utasítás_szám.Trim()}', ";
                    szöveg += $"utasítás_Cím='{Adat.Utasítás_Cím.Trim()}', ";
                    szöveg += $"utasítás_leírás='{Adat.Utasítás_leírás.Trim()}', ";
                    szöveg += $"paraméter='{Adat.Paraméter.Trim()}', ";
                    szöveg += $"karb_ciklus_eleje={Adat.Karb_ciklus_eleje}, ";
                    szöveg += $"karb_ciklus_vége={Adat.Karb_ciklus_vége}, ";
                    szöveg += $"érv_kezdete='{Adat.Érv_kezdete:yyyy.MM.dd}', ";
                    szöveg += $"érv_vége='{Adat.Érv_vége:yyyy.MM.dd}', ";
                    szöveg += $"szakmai_bontás='{Adat.Szakmai_bontás.Trim()}', ";
                    szöveg += $"munkaterületi_bontás='{Adat.Munkaterületi_bontás.Trim()}', ";
                    szöveg += $"altípus='{Adat.Altípus.Trim()}', ";
                    szöveg += $"kenés={Adat.Kenés} ";
                    szöveg += $" WHERE id={Adat.ID}";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw new HibásBevittAdat("Az adatok nem kerültek rögzítésre.");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new HibásBevittAdat("Az adatok nem kerültek rögzítésre.");
            }
        }

        public void Törlés(string Típus, long sorszám, bool végig)
        {
            try
            {
                FájlBeállítás(Típus);
                string szöveg;
                if (végig)
                    szöveg = $"DELETE FROM technológia WHERE id>={sorszám}";
                else
                    szöveg = $"DELETE FROM technológia WHERE id={sorszám}";
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

        public void Egy_Beszúrás(string Típus, long sorszám, List<Adat_Technológia_Új> Adatok)
        {
            Törlés(Típus, sorszám, true);
            Adat_Technológia_Új Adat = new Adat_Technológia_Új(sorszám, "", "", "", "", "", 0, 0, MyF.ElsőNap(), MyF.ElsőNap(), "", "", "", true);
            List<Adat_Technológia_Új> AdatokÚj = new List<Adat_Technológia_Új>
            {
                Adat
            };

            foreach (Adat_Technológia_Új ELem in Adatok)
            {
                sorszám++;
                Adat = new Adat_Technológia_Új(sorszám, ELem.Részegység, ELem.Munka_utasítás_szám, ELem.Utasítás_Cím, ELem.Utasítás_leírás, ELem.Paraméter, ELem.Karb_ciklus_eleje,
                    ELem.Karb_ciklus_vége, ELem.Érv_kezdete, ELem.Érv_vége, ELem.Szakmai_bontás, ELem.Munkaterületi_bontás, ELem.Altípus, ELem.Kenés);
                AdatokÚj.Add(Adat);

            }
            Rögzítés(Típus, AdatokÚj);
        }

        public void Egy_Törlése(string Típus, long sorszám, List<Adat_Technológia_Új> Adatok)
        {
            Törlés(Típus, sorszám, true);
            sorszám--;
            List<Adat_Technológia_Új> AdatokÚj = new List<Adat_Technológia_Új>();

            foreach (Adat_Technológia_Új ELem in Adatok)
            {
                sorszám++;
                Adat_Technológia_Új Adat = new Adat_Technológia_Új(sorszám, ELem.Részegység, ELem.Munka_utasítás_szám, ELem.Utasítás_Cím, ELem.Utasítás_leírás, ELem.Paraméter, ELem.Karb_ciklus_eleje,
                    ELem.Karb_ciklus_vége, ELem.Érv_kezdete, ELem.Érv_vége, ELem.Szakmai_bontás, ELem.Munkaterületi_bontás, ELem.Altípus, ELem.Kenés);
                AdatokÚj.Add(Adat);
            }
            Rögzítés(Típus, AdatokÚj);
        }




        public List<Adat_Technológia> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Technológia> Adatok = new List<Adat_Technológia>();
            Adat_Technológia Adat;
            Kezelő_Technológia_Ciklus Kéz = new Kezelő_Technológia_Ciklus();
            string másikszöveg = "SELECT * FROM karbantartás";
            List<Adat_technológia_Ciklus> AdatokCiklus = Kéz.Lista_Adatok(hely, jelszó, másikszöveg);


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
                                Adat_technológia_Ciklus AdatCikluse = (from a in AdatokCiklus
                                                                       where a.Sorszám == rekord["Karb_ciklus_eleje"].ToÉrt_Int()
                                                                       select a).FirstOrDefault();
                                Adat_technológia_Ciklus AdatCiklusv = (from a in AdatokCiklus
                                                                       where a.Sorszám == rekord["Karb_ciklus_vége"].ToÉrt_Int()
                                                                       select a).FirstOrDefault();

                                Adat = new Adat_Technológia(
                                    rekord["id"].ToÉrt_Long(),
                                    rekord["Részegység"].ToStrTrim(),
                                    rekord["Munka_utasítás_szám"].ToStrTrim(),
                                    rekord["Utasítás_Cím"].ToStrTrim(),
                                    rekord["Utasítás_leírás"].ToStrTrim(),
                                    rekord["Paraméter"].ToStrTrim(),
                                    AdatCikluse,
                                    AdatCiklusv,
                                    rekord["Érv_kezdete"].ToÉrt_DaTeTime(),
                                    rekord["Érv_vége"].ToÉrt_DaTeTime(),
                                    rekord["Szakmai_bontás"].ToStrTrim(),
                                    rekord["Munkaterületi_bontás"].ToStrTrim(),
                                    rekord["Altípus"].ToStrTrim(),
                                    rekord["Kenés"].ToÉrt_Bool());

                                Adatok.Add(Adat);

                            }
                        }

                    }
                }
            }
            return Adatok;
        }

        public void Rögzít_adat(string hely, string jelszó, Adat_Technológia Adat)
        {

            try
            {
                if (Adat.Részegység.Length > 10) throw new HibásBevittAdat("Részegység maximum 10 karakter hosszú lehet!");
                if (Adat.Munka_utasítás_szám.Length > 10) throw new HibásBevittAdat("Munka_utasítás száma maximum 10 karakter hosszú lehet!");
                if (Adat.Utasítás_Cím.Length > 250) throw new HibásBevittAdat("Utasítás címe maximum 250 karakter hosszú lehet!");
                if (Adat.Szakmai_bontás.Length > 50) throw new HibásBevittAdat("Szakmai Bontás maximum 50 karakter hosszú lehet!");
                if (Adat.Munkaterületi_bontás.Length > 50) throw new HibásBevittAdat("Munkaterületi bontás maximum 50 karakter hosszú lehet!");
                if (Adat.Altípus.Length > 50) throw new HibásBevittAdat("Altípus maximum 50 karakter hosszú lehet!");
                if (Adat.Érv_kezdete >= Adat.Érv_vége) throw new HibásBevittAdat("Az érvényesség kezdetének kisebbnek kell lennie az érvényesség végénél!");
                if (Adat.Részegység.Length == 0) throw new HibásBevittAdat("Részegység  nem lehet 0 karakter hosszú lehet!");
                if (Adat.Munka_utasítás_szám.Length == 0) throw new HibásBevittAdat("Munka_utasítás száma  nem lehet 0 karakter hosszú lehet!");
                if (Adat.Utasítás_Cím.Length == 0) throw new HibásBevittAdat("Utasítás címe  nem lehet 0 karakter hosszú lehet!");

                string szöveg = "SELECT * FROM technológia";
                Kezelő_Technológia KézTechnológia = new Kezelő_Technológia();
                List<Adat_Technológia> AdatokTechnológia = KézTechnológia.Lista_Adatok(hely, jelszó, szöveg);

                Adat_Technológia Elem = AdatokTechnológia.FirstOrDefault(a => a.ID == Adat.ID);

                long id = Adat.ID;

                if (Elem == null)
                {

                    szöveg = "INSERT INTO technológia ( iD,  részegység,  munka_utasítás_szám,  utasítás_Cím,  utasítás_leírás,  paraméter, " +
                        " karb_ciklus_eleje,  karb_ciklus_vége,  érv_kezdete,  érv_vége,  szakmai_bontás,  munkaterületi_bontás,  altípus,  kenés ) VALUES (";
                    szöveg += $"{id}, "; //id
                    szöveg += "'" + Adat.Részegység.Trim() + "', "; // részegység
                    szöveg += "'" + Adat.Munka_utasítás_szám.Trim() + "', ";//  munka_utasítás_szám
                    szöveg += "'" + Adat.Utasítás_Cím.Trim() + "', ";//   utasítás_Cím
                    szöveg += "'" + Adat.Utasítás_leírás.Trim() + "', ";//   utasítás_leírás
                    szöveg += "'" + Adat.Paraméter.Trim() + "', ";//   paraméter
                    szöveg += "'" + Adat.Karb_ciklus_eleje.Sorszám.ToString() + "', ";//  karb_ciklus_eleje
                    szöveg += "'" + Adat.Karb_ciklus_vége.Sorszám.ToString() + "', ";//  karb_ciklus_vége
                    szöveg += "'" + Adat.Érv_kezdete.ToString("yyyy.MM.dd") + "', ";//   érv_kezdete
                    szöveg += "'" + Adat.Érv_vége.ToString("yyyy.MM.dd") + "', ";//    érv_vége
                    szöveg += "'" + Adat.Szakmai_bontás.Trim() + "', ";//     szakmai_bontás
                    szöveg += "'" + Adat.Munkaterületi_bontás.Trim() + "',";//     munkaterületi_bontás
                    szöveg += "'" + Adat.Altípus.Trim() + "', ";//    altípus
                    szöveg += Adat.Kenés.ToString() + ") ";//   kenés

                }
                else
                {
                    szöveg = "UPDATE technológia  SET ";
                    szöveg += "részegység='" + Adat.Részegység.Trim() + "', "; // részegység
                    szöveg += "munka_utasítás_szám='" + Adat.Munka_utasítás_szám.Trim() + "', ";//  munka_utasítás_szám
                    szöveg += "utasítás_Cím='" + Adat.Utasítás_Cím.Trim() + "', ";//   utasítás_Cím
                    szöveg += "utasítás_leírás='" + Adat.Utasítás_leírás.Trim() + "', ";//   utasítás_leírás
                    szöveg += "paraméter='" + Adat.Paraméter.Trim() + "', ";//   paraméter
                    szöveg += "karb_ciklus_eleje='" + Adat.Karb_ciklus_eleje.Sorszám.ToString() + "', ";//  karb_ciklus_eleje
                    szöveg += "karb_ciklus_vége='" + Adat.Karb_ciklus_vége.Sorszám.ToString() + "', ";//  karb_ciklus_vége
                    szöveg += "érv_kezdete='" + Adat.Érv_kezdete.ToString("yyyy.MM.dd") + "', ";//   érv_kezdete
                    szöveg += "érv_vége='" + Adat.Érv_vége.ToString("yyyy.MM.dd") + "', ";//    érv_vége
                    szöveg += "szakmai_bontás='" + Adat.Szakmai_bontás.Trim() + "', ";//     szakmai_bontás
                    szöveg += "munkaterületi_bontás='" + Adat.Munkaterületi_bontás.Trim() + "',";//     munkaterületi_bontás
                    szöveg += "altípus='" + Adat.Altípus.Trim() + "', ";//    altípus
                    szöveg += "kenés=" + Adat.Kenés.ToString();//   kenés
                    szöveg += " WHERE id=" + Adat.ID.ToString();
                }

                MyA.ABMódosítás(hely, jelszó, szöveg);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                throw new HibásBevittAdat("Az adatok nem kerültek rögzítésre.");
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new HibásBevittAdat("Az adatok nem kerültek rögzítésre.");
            }

        }
    }
}
