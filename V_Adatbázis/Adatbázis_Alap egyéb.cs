using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void Egyéb_beolvasás(string hely)
        {
            string szöveg;
            string jelszó = "sajátmagam";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE tábla (";
            szöveg += " [csoport] CHAR(10), ";
            szöveg += " [oszlop] short,";
            szöveg += " [fejléc] CHAR(255),";
            szöveg += " [törölt] CHAR(1),";
            szöveg += " [kell] long)";

            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

    }
}
