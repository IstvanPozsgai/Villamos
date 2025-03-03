﻿using System;
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
    public class Kezelő_Üzenet_Olvas
    {
        readonly string jelszó = "katalin";
        string hely;

        private void FájlBeállítás(string Telephely, int Év)
        {
            hely = $@"{Application.StartupPath}\{Telephely.Trim()}\adatok\üzenetek\{Év}üzenet.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.ALÜzenetadatok(hely.KönyvSzerk());
        }

        public List<Adat_Üzenet_Olvasás> Lista_Adatok(string Telephely, int Év)
        {
            FájlBeállítás(Telephely, Év);
            string szöveg = "SELECT * FROM olvasás ";
            List<Adat_Üzenet_Olvasás> Adatok = new List<Adat_Üzenet_Olvasás>();
            Adat_Üzenet_Olvasás Adat;

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
                                Adat = new Adat_Üzenet_Olvasás(
                                    rekord["Sorszám"].ToÉrt_Double(),
                                    rekord["ki"].ToStrTrim(),
                                    rekord["üzenetid"].ToÉrt_Double(),
                                    rekord["Mikor"].ToÉrt_DaTeTime(),
                                    rekord["olvasva"].ToÉrt_Bool()
                                    );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public void Rögzítés(string Telephely, int Év, Adat_Üzenet_Olvasás Adat)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<Adat_Üzenet_Olvasás> Adatok = Lista_Adatok(Telephely, Év);

                Adat_Üzenet_Olvasás vane = (from a in Adatok
                                            where a.Üzenetid == Adat.Üzenetid
                                            && a.Ki == Program.PostásNév.Trim()
                                            select a).FirstOrDefault();
                if (vane == null)
                {
                    double i = 1;
                    if (Adatok.Count > 0) i = Adatok.Max(a => a.Sorszám) + 1;

                    string szöveg = "INSERT INTO olvasás ";
                    szöveg += "(sorszám, ki, üzenetid, mikor, olvasva)";
                    szöveg += " VALUES ";
                    szöveg += $"({i}, '{Program.PostásNév.Trim()}', {Adat.Üzenetid}, '{DateTime.Now}', -1)";
                    MyA.ABMódosítás(hely, jelszó, szöveg);
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

        public void Rögzítés(string Telephely, int Év, List<Adat_Üzenet_Olvasás> AdatokOlvas)
        {
            try
            {
                FájlBeállítás(Telephely, Év);
                List<Adat_Üzenet_Olvasás> Adatok = Lista_Adatok(Telephely, Év);

                double i = 0;
                if (Adatok.Count > 0) i = Adatok.Max(a => a.Sorszám);
                List<string> SzövegGy = new List<string>();
                foreach (Adat_Üzenet_Olvasás item in AdatokOlvas)
                {
                    Adat_Üzenet_Olvasás vane = (from a in Adatok
                                                where a.Üzenetid == item.Üzenetid
                                                && a.Ki == Program.PostásNév.Trim()
                                                select a).FirstOrDefault();
                    if (vane == null)
                    {
                        i++;
                        string szöveg = "INSERT INTO olvasás ";
                        szöveg += "(sorszám, ki, üzenetid, mikor, olvasva)";
                        szöveg += " VALUES ";
                        szöveg += $"({i}, '{Program.PostásNév.Trim()}', {item.Üzenetid}, '{DateTime.Now}', -1)";
                        SzövegGy.Add(szöveg);
                    }
                }
                if (SzövegGy != null && SzövegGy.Count > 0) MyA.ABMódosítás(hely, jelszó, SzövegGy);
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
