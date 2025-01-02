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
        public static void Szerszám_nyilvántartás(string hely)
        {
            string szöveg;
            string jelszó = "csavarhúzó";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Cikktörzs (";
            szöveg += "[azonosító]  char (20) PRIMARY KEY,";
            szöveg += "[Megnevezés]  char (100),";
            szöveg += "[Méret]  char (15),";
            szöveg += "[Hely]  char (50),";
            szöveg += "[leltáriszám]  char (100),";
            szöveg += "[Beszerzésidátum] DATE,";
            szöveg += "[státus]  short,";
            szöveg += "[költséghely]  char (10),";
            szöveg += "[gyáriszám]  char (50))";


            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE könyvtörzs (";
            szöveg += "[szerszámkönyvszám]  char (50)  PRIMARY KEY,";
            szöveg += "[szerszámkönyvnév]  char (50),";
            szöveg += "[felelős1]  char (70),";
            szöveg += "[felelős2]  char (70),";
            szöveg += "[státus]  yesno ";
            szöveg += " )";
            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Könyvelés (";
            szöveg += "[azonosító]  char (20), ";
            szöveg += "[szerszámkönyvszám]  char (50), ";
            szöveg += "[mennyiség]  double, ";
            szöveg += "[dátum] DATE ) ";

            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // másodlagos kulcsok
            szöveg = "ALTER TABLE Könyvelés ";
            szöveg += "ADD FOREIGN KEY(azonosító) REFERENCES Cikktörzs(azonosító)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "ALTER TABLE Könyvelés ";
            szöveg += "ADD FOREIGN KEY(szerszámkönyvszám) REFERENCES könyvtörzs(szerszámkönyvszám)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Szerszámlistanapló(string hely)
        {

            string szöveg;
            string jelszó = "csavarhúzó";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Napló (";
            szöveg += "[azonosító]  char (20),";
            szöveg += "[Honnan]  char (50),";
            szöveg += "[Hova]  char (50),";
            szöveg += "[mennyiség]  double,";
            szöveg += "[Módosította]  char (50),";
            szöveg += "[módosításidátum] DATE)";

            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }
    }
}
