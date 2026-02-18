using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Belépés_Oldalok
    {
        readonly string hely =
            $@"{Application.StartupPath}\Főmérnökség\SqLite\Belépés.db".KönyvSzerk();

        string ConnString = SqLite_Adatbázis.BuildConnectionString(hely, jelszó);

        readonly string jelszó = "ForgalmiUtasítás";

        public Kezelő_Belépés_Oldalok()
        {
            // Ha a fájl nem létezik: SQLite kapcsolat létrehozása
            if (!File.Exists(hely))
               SqLite_Adatbázis.BuildConnectionString(hely, jelszó);

            // EF inicializálás (tábla létrehozása)
            using (var db = new Context_Bejelentkezés_Oldalak(ConnString))
            {
                db.Database.Initialize(false);
            }
        }

        // LISTA LEKÉRÉS
        public List<SAdat_Belépés_Oldalak> Lista_Adatok()
        {
            using (var db = new Context_Bejelentkezés_Oldalak(ConnString))
            {
                return db.Oldalak.ToList();
            }
        }

        // ÚJ vagy MÓDOSÍTÁS automatikusan
        public void Döntés(SAdat_Belépés_Oldalak adat)
        {
            using (var db = new Context_Bejelentkezés_Oldalak(ConnString))
            {
                var létező = db.Oldalak.FirstOrDefault(a => a.OldalId == adat.OldalId);

                if (létező == null)
                {
                    // Rögzítés
                    db.Oldalak.Add(adat);
                }
                else
                {
                    // Módosítás
                    db.Entry(létező).CurrentValues.SetValues(adat);
                }

                db.SaveChanges();
            }
        }

        // KÜLÖN MÓDOSÍTÁS (ha szükséges)
        public void Módosítás(SAdat_Belépés_Oldalak adat)
        {
            using (var db = new Context_Bejelentkezés_Oldalak(ConnString))
            {
                db.Entry(adat).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        // KÜLÖN RÖGZÍTÉS (ha szükséges)
        public void Rögzítés(SAdat_Belépés_Oldalak adat)
        {
            using (var db = new Context_Bejelentkezés_Oldalak(ConnString))
            {
                db.Oldalak.Add(adat);
                db.SaveChanges();
            }
        }
    }
}