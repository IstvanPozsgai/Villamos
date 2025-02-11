using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_Technológia
    {
        readonly string jelszó = "Bezzegh";
        string hely;

        #region Kezelők és Lista
        readonly Kezelő_Technológia_Ciklus KézCiklus = new Kezelő_Technológia_Ciklus();
        List<Adat_technológia_Ciklus> AdatokCiklus = new List<Adat_technológia_Ciklus>();
        #endregion

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
                    szöveg += $"{Adat.Kenés.ToString()}) ";//   kenés
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



        public List<Adat_Technológia_Alap> List_Tech_típus(string hely, string jelszó, string szöveg)
        {
            List<Adat_Technológia_Alap> Adatok = new List<Adat_Technológia_Alap>();
            Adat_Technológia_Alap Adat;
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
                                Adat = new Adat_Technológia_Alap(
                                   rekord["id"].ToÉrt_Long(),
                                    rekord["típus"].ToStrTrim()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_Technológia Egy_Adat(string hely, string jelszó, long id)
        {
            Adat_Technológia Adat = null;
            string szöveg = "SELECT Karbantartás_1.fokozat, Karbantartás.fokozat, technológia.Id, technológia.* ";
            szöveg += " FROM (Karbantartás RIGHT JOIN technológia ON Karbantartás.sorszám = technológia.Karb_ciklus_eleje) ";
            szöveg += " LEFT JOIN Karbantartás AS Karbantartás_1 ON technológia.Karb_ciklus_vége = Karbantartás_1.sorszám ";
            szöveg += $" WHERE technológia.Id= {id}";

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
                            rekord.Read();

                            Adat_technológia_Ciklus AdatCikluse = new Adat_technológia_Ciklus(int.Parse(rekord["Karb_ciklus_eleje"].ToString()), rekord["Karbantartás.fokozat"].ToString());
                            Adat_technológia_Ciklus AdatCiklusv = new Adat_technológia_Ciklus(int.Parse(rekord["Karb_ciklus_vége"].ToString()), rekord["Karbantartás_1.fokozat"].ToString());

                            Adat = new Adat_Technológia(
                                id,
                                rekord["részegység"].ToStrTrim(),
                                rekord["munka_utasítás_szám"].ToStrTrim(),
                                rekord["utasítás_cím"].ToStrTrim(),
                                rekord["utasítás_leírás"].ToStrTrim(),
                                rekord["paraméter"].ToStrTrim(),
                                AdatCikluse,
                                AdatCiklusv,
                                rekord["érv_kezdete"].ToÉrt_DaTeTime(),
                                rekord["érv_vége"].ToÉrt_DaTeTime(),
                                rekord["szakmai_bontás"].ToStrTrim(),
                                rekord["munkaterületi_bontás"].ToStrTrim(),
                                rekord["altípus"].ToStrTrim(),
                                rekord["kenés"].ToÉrt_Bool()
                                );
                        }
                    }
                }
            }
            return Adat;
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



        #region Ciklus-Karbantartás

        public List<Adat_technológia_Ciklus> CiklusListaFeltöltés(string hely, string jelszó)
        {
            List<Adat_technológia_Ciklus> Válasz = new List<Adat_technológia_Ciklus>();
            try
            {

                string szöveg = $"SELECT * FROM Karbantartás";
                Válasz = KézCiklus.Lista_Adatok(hely, jelszó, szöveg);

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
            return Válasz;
        }








        #endregion



        public void Egy_Beszúrás(string hely, string jelszó, long sorszám, List<Adat_Technológia_Új> Adatok)
        {

            //kitöröljük az adatokat a sorszámtól
            string szöveg = $"DELETE FROM technológia WHERE id>={sorszám}";
            MyA.ABtörlés(hely, jelszó, szöveg);

            foreach (Adat_Technológia_Új Adat in Adatok)
            {
                // Eggyel hátrébb rögzítjük az adatokat
                szöveg = "INSERT INTO technológia ( iD,  részegység,  munka_utasítás_szám,  utasítás_Cím,  utasítás_leírás,  paraméter, " +
            " karb_ciklus_eleje,  karb_ciklus_vége,  érv_kezdete,  érv_vége,  szakmai_bontás,  munkaterületi_bontás,  altípus,  kenés ) VALUES (";
                szöveg += (Adat.ID + 1).ToString() + ", "; //id
                szöveg += "'" + Adat.Részegység.Trim() + "', "; // részegység
                szöveg += "'" + Adat.Munka_utasítás_szám.Trim() + "', ";//  munka_utasítás_szám
                szöveg += "'" + Adat.Utasítás_Cím.Trim() + "', ";//   utasítás_Cím
                szöveg += "'" + Adat.Utasítás_leírás.Trim() + "', ";//   utasítás_leírás
                szöveg += "'" + Adat.Paraméter.Trim() + "', ";//   paraméter
                szöveg += "'" + Adat.Karb_ciklus_eleje + "', ";//  karb_ciklus_eleje
                szöveg += "'" + Adat.Karb_ciklus_vége + "', ";//  karb_ciklus_vége
                szöveg += "'" + Adat.Érv_kezdete.ToString("yyyy.MM.dd") + "', ";//   érv_kezdete
                szöveg += "'" + Adat.Érv_vége.ToString("yyyy.MM.dd") + "', ";//    érv_vége
                szöveg += "'" + Adat.Szakmai_bontás.Trim() + "', ";//     szakmai_bontás
                szöveg += "'" + Adat.Munkaterületi_bontás.Trim() + "',";//     munkaterületi_bontás
                szöveg += "'" + Adat.Altípus.Trim() + "', ";//    altípus
                szöveg += Adat.Kenés.ToString() + ") ";//   kenés
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            //beszúrjuk az új sort
            szöveg = "INSERT INTO technológia ( iD,  részegység,  munka_utasítás_szám,  utasítás_Cím,  utasítás_leírás,  paraméter, " +
            " karb_ciklus_eleje,  karb_ciklus_vége,  érv_kezdete,  érv_vége,  szakmai_bontás,  munkaterületi_bontás,  altípus,  kenés ) VALUES (";
            szöveg += sorszám.ToString() + ", "; //id
            szöveg += "'', "; // részegység
            szöveg += "'', ";//  munka_utasítás_szám
            szöveg += "'', ";//   utasítás_Cím
            szöveg += "'', ";//   utasítás_leírás
            szöveg += "'', ";//   paraméter
            szöveg += "'1', ";//  karb_ciklus_eleje
            szöveg += "'1', ";//  karb_ciklus_vége
            szöveg += "'1900.01.01', ";//   érv_kezdete
            szöveg += "'1900.01.01', ";//    érv_vége
            szöveg += "'', ";//     szakmai_bontás
            szöveg += "'',";//     munkaterületi_bontás
            szöveg += "'', ";//    altípus
            szöveg += false + ") ";//   kenés
            MyA.ABMódosítás(hely, jelszó, szöveg);

        }


        public void Egy_Törlése(string hely, string jelszó, long sorszám, List<Adat_Technológia_Új> Adatok)
        {

            //kitöröljük a sorszám adatait
            string szöveg = $"DELETE FROM technológia WHERE id>={sorszám}";
            MyA.ABtörlés(hely, jelszó, szöveg);

            foreach (Adat_Technológia_Új Adat in Adatok)
            {
                // Eggyel előrébb rögzítjük az adatokat
                szöveg = "INSERT INTO technológia ( iD,  részegység,  munka_utasítás_szám,  utasítás_Cím,  utasítás_leírás,  paraméter, " +
            " karb_ciklus_eleje,  karb_ciklus_vége,  érv_kezdete,  érv_vége,  szakmai_bontás,  munkaterületi_bontás,  altípus,  kenés ) VALUES (";
                szöveg += (Adat.ID - 1).ToString() + ", "; //id
                szöveg += "'" + Adat.Részegység.Trim() + "', "; // részegység
                szöveg += "'" + Adat.Munka_utasítás_szám.Trim() + "', ";//  munka_utasítás_szám
                szöveg += "'" + Adat.Utasítás_Cím.Trim() + "', ";//   utasítás_Cím
                szöveg += "'" + Adat.Utasítás_leírás.Trim() + "', ";//   utasítás_leírás
                szöveg += "'" + Adat.Paraméter.Trim() + "', ";//   paraméter
                szöveg += "'" + Adat.Karb_ciklus_eleje + "', ";//  karb_ciklus_eleje
                szöveg += "'" + Adat.Karb_ciklus_vége + "', ";//  karb_ciklus_vége
                szöveg += "'" + Adat.Érv_kezdete.ToString("yyyy.MM.dd") + "', ";//   érv_kezdete
                szöveg += "'" + Adat.Érv_vége.ToString("yyyy.MM.dd") + "', ";//    érv_vége
                szöveg += "'" + Adat.Szakmai_bontás.Trim() + "', ";//     szakmai_bontás
                szöveg += "'" + Adat.Munkaterületi_bontás.Trim() + "',";//     munkaterületi_bontás
                szöveg += "'" + Adat.Altípus.Trim() + "', ";//    altípus
                szöveg += Adat.Kenés.ToString() + ") ";//   kenés
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
        }

        public List<string> Lista_Altípus(string hely, string jelszó, string szöveg)
        {
            List<string> Adatok = new List<string>();
            string Adat;
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
                                Adat = rekord["altípus"].ToStrTrim();
                                Adatok.Add(Adat);

                            }
                        }

                    }
                }
            }
            return Adatok;
        }
    }

    public class Kezelő_Technológia_Munkalap
    {
        readonly Kezelő_Technológia MyTech = new Kezelő_Technológia();

        public List<Adat_Technológia_Munkalap> Lista_Technológia(string hely, string jelszó, string szöveg)
        {
            List<Adat_Technológia_Munkalap> Adatok = new List<Adat_Technológia_Munkalap>();
            Adat_Technológia_Munkalap Adat;

            Kezelő_Technológia MyTech = new Kezelő_Technológia();
            List<Adat_technológia_Ciklus> AdatokCiklus = MyTech.CiklusListaFeltöltés(hely, jelszó);

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
                                string eleje = "";
                                string vége = "";
                                Adat_technológia_Ciklus CiklusElem = AdatokCiklus.FirstOrDefault(a => a.Sorszám == rekord["Karb_ciklus_eleje"].ToÉrt_Int());
                                if (CiklusElem != null) eleje = CiklusElem.Fokozat;
                                CiklusElem = AdatokCiklus.FirstOrDefault(a => a.Sorszám == rekord["Karb_ciklus_vége"].ToÉrt_Int());
                                if (CiklusElem != null) vége = CiklusElem.Fokozat;

                                Adat_technológia_Ciklus AdatCikluse = new Adat_technológia_Ciklus(int.Parse(rekord["Karb_ciklus_eleje"].ToString()), eleje);
                                Adat_technológia_Ciklus AdatCiklusv = new Adat_technológia_Ciklus(int.Parse(rekord["Karb_ciklus_vége"].ToString()), vége);

                                Adat = new Adat_Technológia_Munkalap(
                                    rekord["id"].ToÉrt_Long(),
                                    rekord["Részegység"].ToStrTrim(),
                                    rekord["Munka_utasítás_szám"].ToStrTrim(),
                                    rekord["Utasítás_Cím"].ToStrTrim(),
                                    rekord["Utasítás_leírás"].ToStrTrim(),
                                    rekord["Paraméter"].ToStrTrim(),
                                    AdatCikluse.Sorszám,
                                    AdatCiklusv.Sorszám,
                                    rekord["Érv_kezdete"].ToÉrt_DaTeTime(),
                                    rekord["Érv_vége"].ToÉrt_DaTeTime(),
                                    rekord["Szakmai_bontás"].ToStrTrim(),
                                    rekord["Munkaterületi_bontás"].ToStrTrim(),
                                    rekord["Altípus"].ToStrTrim(),
                                    rekord["Kenés"].ToÉrt_Bool(),
                                    rekord["Változatnév"].ToStrTrim(),
                                    rekord["végzi"].ToStrTrim()
                                    );

                                Adatok.Add(Adat);

                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public List<Adat_Technológia_Munkalap> Lista_Adatok(string hely, string jelszó, string szöveg)
        {
            List<Adat_Technológia_Munkalap> Adatok = new List<Adat_Technológia_Munkalap>();
            Adat_Technológia_Munkalap Adat;

            Kezelő_Technológia MyTech = new Kezelő_Technológia();
            List<Adat_technológia_Ciklus> AdatokCiklus = MyTech.CiklusListaFeltöltés(hely, jelszó);

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
                                string eleje = "";
                                string vége = "";
                                Adat_technológia_Ciklus CiklusElem = AdatokCiklus.FirstOrDefault(a => a.Sorszám == rekord["Karb_ciklus_eleje"].ToÉrt_Int());
                                if (CiklusElem != null) eleje = CiklusElem.Fokozat;
                                CiklusElem = AdatokCiklus.FirstOrDefault(a => a.Sorszám == rekord["Karb_ciklus_vége"].ToÉrt_Int());
                                if (CiklusElem != null) vége = CiklusElem.Fokozat;

                                Adat_technológia_Ciklus AdatCikluse = new Adat_technológia_Ciklus(int.Parse(rekord["Karb_ciklus_eleje"].ToString()), eleje);
                                Adat_technológia_Ciklus AdatCiklusv = new Adat_technológia_Ciklus(int.Parse(rekord["Karb_ciklus_vége"].ToString()), vége);

                                Adat = new Adat_Technológia_Munkalap(
                                    rekord["id"].ToÉrt_Long(),
                                    rekord["Részegység"].ToStrTrim(),
                                    rekord["Munka_utasítás_szám"].ToStrTrim(),
                                    rekord["Utasítás_Cím"].ToStrTrim(),
                                    rekord["Utasítás_leírás"].ToStrTrim(),
                                    rekord["Paraméter"].ToStrTrim(),
                                    AdatCikluse.Sorszám,
                                    AdatCiklusv.Sorszám,
                                    rekord["Érv_kezdete"].ToÉrt_DaTeTime(),
                                    rekord["Érv_vége"].ToÉrt_DaTeTime(),
                                    rekord["Szakmai_bontás"].ToStrTrim(),
                                    rekord["Munkaterületi_bontás"].ToStrTrim(),
                                    rekord["Altípus"].ToStrTrim(),
                                    rekord["Kenés"].ToÉrt_Bool(),
                                    rekord["Karbantartási_fokozat"].ToStrTrim(),
                                    rekord["Változatnév"].ToStrTrim(),
                                    rekord["végzi"].ToStrTrim()
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
