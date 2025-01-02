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
        public static void Kerékmérésektábla(string hely)
        {
            string szöveg;
            string jelszó = "rudolfg";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE tábla (";
            szöveg += "[azonosító]  char (10), ";
            szöveg += "[Bekövetkezés] DATE,";
            szöveg += "[üzem]  char (30), ";
            szöveg += "[törölt]  yesno, ";
            szöveg += "[mikor] DATE,";
            szöveg += "[Ki]  char (15), ";
            szöveg += "[típus]  char (50)) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Kerékmérésekjogtábla(string hely)
        {
            string szöveg;
            string jelszó = "rudolfg";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE tábla (";
            szöveg += "[név]  char (15), ";
            szöveg += "[típus]  char (50)) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE naptábla (";
            szöveg += "[id] short) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }
    }
}
