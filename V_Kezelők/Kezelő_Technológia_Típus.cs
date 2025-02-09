﻿using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Technológia_Típus
    {
        readonly string jelszó = "Bezzegh";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\technológia\technológia.mdb";

        public Kezelő_Technológia_Típus()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Technológia_ALAPAdat(hely.KönyvSzerk());
        }

        public List<Adat_Technológia_TípusT> Lista_Adatok()
        {
            string szöveg = "SELECT *  FROM Típus_tábla ORDER BY típus";
            List<Adat_Technológia_TípusT> Adatok = new List<Adat_Technológia_TípusT>();
            Adat_Technológia_TípusT Adat;
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
                                Adat = new Adat_Technológia_TípusT(
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

    }
}
