using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_SQLite
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\SQLite\Test.db";
        readonly string Password = "CzabalayL";
        readonly string TableName = "TestTable";


        public Kezelő_SQLite()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.CAFtábla(hely.KönyvSzerk());

            // Ez később kivehető, ez csak a programrész verziócsere utáni első futtatása miatt került bele, hogy ne kézzel hozzuk létre a táblát.
            if (!Adatbázis.ABvanTábla(hely, jelszó, $"SELECT * FROM {táblanév}"))
            {
                Tabla_Letrehozasa();
            }

            if (cache_osszes_adat == null)
            {
                InitializeCache(KézAdatok); // egyszeri töltés
            }
            osszes_adat = cache_osszes_adat;

            // Lekéri a Ciklusrend adatbázisból a vizsgálatok közötti megtehető km értékét.
            // Elég az elsőt lekérnünk, mivel minden vizsgálatra egységesen van meghatározva.
            Vizsgalatok_Kozott_Megteheto_Km = Kéz_Ciklus.Lista_Adatok().FirstOrDefault(a => a.Típus == "CAF_km").Névleges;
        }

        // Create

        // Read

        // Update

        // Delete

    }
}

