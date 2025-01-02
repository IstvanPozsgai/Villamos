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

        public static void Jogosítványtáblalétrehozás(string hely)
        {
            string szöveg;
            string jelszó = "egycsészekávé";


            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE jogosítványtípus (";
            szöveg += "[Sorszám] SHORT,";
            szöveg += "[Törzsszám] CHAR(8),";
            szöveg += "[jogtípus] CHAR(50),";
            szöveg += "[jogtípusérvényes] DATE,";
            szöveg += "[jogtípusmegszerzés] DATE,";
            szöveg += "[státus] YESNO)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE jogosítványvonal (";
            szöveg += "[Sorszám] SHORT,";
            szöveg += "[Törzsszám] CHAR(8),";
            szöveg += "[jogvonalérv] DATE,";
            szöveg += "[jogvonalmegszerzés] DATE,";
            szöveg += "[vonalmegnevezés] CHAR(255),";
            szöveg += "[vonalszám] CHAR(10),";
            szöveg += "[státus] YESNO)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Munkakör_segédadattábla(string hely)
        {
            string szöveg;
            string jelszó = "ladányis";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Munkakörtábla (";
            szöveg += "[ID]  long, ";
            szöveg += "[Megnevezés]  char (255), ";
            szöveg += "[PDFfájlnév] char (255),";
            szöveg += "[státus] long,";
            szöveg += "[telephely]  char (50), ";
            szöveg += "[HRazonosító]  char (8), ";
            szöveg += "[dátum]  DATE, ";
            szöveg += "[Rögzítő]  char (15)) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Személyes_AdatTábla(string hely) 
        {

            string szöveg;
            string jelszó = "forgalmiutasítás";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE személyes (";
            szöveg += "[dolgozószám]  char (8), ";
            szöveg += "[leánykori]  char (50), ";
            szöveg += "[anyja]  char (50), ";
            szöveg += "[születésiidő]  DATE, ";
            szöveg += "[születésihely]  char (20), ";
            szöveg += "[lakcím]  char (50), ";
            szöveg += "[ideiglenescím]  char (50), ";
            szöveg += "[telefonszám1]  char (13), ";
            szöveg += "[telefonszám2]  char (13), ";
            szöveg += "[telefonszám3]  char (13)) ";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
