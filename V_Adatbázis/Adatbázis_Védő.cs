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
        public static void Védőkönyvtörzs(string hely)
        {
            string szöveg;
            string jelszó = "csavarhúzó";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE lista (";
            szöveg += "[szerszámkönyvszám]  char (10),";
            szöveg += "[szerszámkönyvnév]  char (50),";
            szöveg += "[felelős1]  char (60),";
            szöveg += "[státus]  yesno)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Védőtörzs_készítés(string hely)
        {
            string szöveg;
            string jelszó = "csavarhúzó";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Lista (";
            szöveg += "[azonosító]  char (20),";
            szöveg += "[Megnevezés]  char (50),";
            szöveg += "[Méret]  char (15),";
            szöveg += "[státus]  short,";
            szöveg += "[költséghely]  char (6),";
            szöveg += "[Védelem]  char (20), ";
            szöveg += "[Kockázat]  char (100), ";
            szöveg += "[Szabvány]  char (50), ";
            szöveg += "[Szint]  char (50), ";
            szöveg += "[Munk_megnevezés]  char (150))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Védőlista(string hely)
        {
            // védőKönyvelés
            string szöveg;
            string jelszó = "csavarhúzó";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE lista (";
            szöveg += "[azonosító]  char (20), ";
            szöveg += "[szerszámkönyvszám]  char (50), ";
            szöveg += "[mennyiség]  double, ";
            szöveg += "[Gyáriszám]  char (50),";
            szöveg += "[dátum] DATE, ";
            szöveg += "[státus] YESNO )";

            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }


        public static void Védőlistanapló(string hely)
        {

            string szöveg;
            string jelszó = "csavarhúzó";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE lista (";
            szöveg += "[Azonosító]  char (20),";
            szöveg += "[Honnan]  char (50),";
            szöveg += "[Hova]  char (50),";
            szöveg += "[Mennyiség]  double,";
            szöveg += "[Gyáriszám]  char (50),";
            szöveg += "[Módosította]  char (50),";
            szöveg += "[Módosításidátum] DATE, ";
            szöveg += "[státus] YESNO )";

            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
