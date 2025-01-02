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
        //Ellenőrizendő a típusa a változónak osztály létrehozását megelőzendő az ellenőrzött előtt van jelölés
        public static void Kieg_Telephely(string hely)
        {
            string szöveg;
            string jelszó = "Mocó";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Alapszabadság (";
            szöveg += "[sorszám] long,";
            szöveg += "[életkor] egész,";
            szöveg += "[8órás] Long,";
            szöveg += "[12órás] long)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Bennlét (";
            szöveg += "[sorszám] long,";
            szöveg += "[kezdőidő] date,";
            szöveg += "[végzőidő] date)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Beosztáskódok (";
            szöveg += "[sorszám] long,";
            szöveg += "[beosztáskód] CHAR(3),";
            szöveg += "[munkaidőkezdet] date,";
            szöveg += "[munkaidővége] date,";
            szöveg += "[munkaidő] short,";
            szöveg += "[munkarend] short,";
            szöveg += "[napszak] CHAR(2),";
            szöveg += "[éjszakás] YESNO,";
            szöveg += "[számoló] YESNO,";
            szöveg += "[0] short,";
            szöveg += "[1] short,";
            szöveg += "[2] short,";
            szöveg += "[3] short,";
            szöveg += "[4] short,";
            szöveg += "[5] short,";
            szöveg += "[6] short,";
            szöveg += "[7] short,";
            szöveg += "[8] short,";
            szöveg += "[9] short,";
            szöveg += "[10] short,";
            szöveg += "[11] short,";
            szöveg += "[12] short,";
            szöveg += "[13] short,";
            szöveg += "[14] short,";
            szöveg += "[15] short,";
            szöveg += "[16] short,";
            szöveg += "[17] short,";
            szöveg += "[18] short,";
            szöveg += "[19] short,";
            szöveg += "[20] short,";
            szöveg += "[21] short,";
            szöveg += "[22] short,";
            szöveg += "[23] short,";
            szöveg += "[magyarázat] CHAR(255))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Csoportbeosztás (";
            szöveg += "[sorszám] long,";
            szöveg += "[csoportbeosztás] CHAR(50),";
            szöveg += "[típus] CHAR(1))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE E3típus (";
            szöveg += "[típus] CHAR(20))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Feorszámok (";
            szöveg += "[sorszám] számláló,";
            szöveg += "[feorszám] CHAR(10),";
            szöveg += "[feormegnevezés] CHAR(50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Feortipus (";
            szöveg += "[típus] CHAR(20),";
            szöveg += "[ftípus] CHAR(20))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Főkönyvtábla (";
            szöveg += "[id] short,";
            szöveg += "[név] CHAR(255),";
            szöveg += "[beosztás] CHAR(255))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Hétnapja (";
            szöveg += "[sorszám] számláló,";
            szöveg += "[hétnapja] CHAR(10),";
            szöveg += "[hétnapjarövid] CHAR(3))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Hibaterv (";
            szöveg += "[id] short,";
            szöveg += "[szöveg] CHAR(50),";
            szöveg += "[főkönyv] YESNO)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Hónapév (";
            szöveg += "[sorszám] számláló,";
            szöveg += "[hónapév] CHAR(20))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE időtábla (";
            szöveg += "[reggel] date,";
            szöveg += "[délután] date,";
            szöveg += "[este] date,";
            szöveg += "[sorszám] short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Jelenlétiív (";
            szöveg += "[ID] short,";
            szöveg += "[szervezet] CHAR(255))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Jogosítvány (";
            szöveg += "[sorszám] számláló,";
            szöveg += "[kategória] CHAR(1))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Kidobó (";
            szöveg += "[id] short,";
            szöveg += "[telephely] CHAR(255))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Kockázatok (";
            szöveg += "[sorszám] számláló,";
            szöveg += "[megnevezés] CHAR(100))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Munkakör (";
            szöveg += "[sorszám] számláló,";
            szöveg += "[munkakör] CHAR(50),";
            szöveg += "[1] short,";
            szöveg += "[2] short,";
            szöveg += "[3] short,";
            szöveg += "[4] short,";
            szöveg += "[5] short,";
            szöveg += "[6] short,";
            szöveg += "[7] short,";
            szöveg += "[8] short,";
            szöveg += "[9] short,";
            szöveg += "[10] short,";
            szöveg += "[11] short,";
            szöveg += "[12] short,";
            szöveg += "[13] short,";
            szöveg += "[14] short,";
            szöveg += "[15] short,";
            szöveg += "[16] short,";
            szöveg += "[17] short,";
            szöveg += "[18] short,";
            szöveg += "[19] short,";
            szöveg += "[20] short,";
            szöveg += "[21] short,";
            szöveg += "[22] short,";
            szöveg += "[23] short,";
            szöveg += "[24] short,";
            szöveg += "[25] short,";
            szöveg += "[26] short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE ok (";
            szöveg += "[okok] CHAR(100))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Sapmunkahely (";
            szöveg += "[ID] short,";
            szöveg += "[felelősmunkahely] CHAR(255))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Szabadságok (";
            szöveg += "[sorszám] számláló,";
            szöveg += "[megnevezés] CHAR(50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Takarítástípus (";
            szöveg += "[típus] CHAR(20))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Túlóra (";
            szöveg += "[sorszám] stámláló,";
            szöveg += "[megnevezés] CHAR(50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }


        public static void Kieg1_Telephely(string hely)
        {
            string szöveg;
            string jelszó = "Mocó";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Felmentés ("; szöveg += "[] date,";
            szöveg += "[id] short,";
            szöveg += "[címzett] CHAR(255),";
            szöveg += "[másolat] CHAR(255),";
            szöveg += "[tárgy] CHAR(255),";
            szöveg += "[kértvizsgálat] CHAR(20),";
            szöveg += "[bevezetés] long,";
            szöveg += "[tárgyalás] long,";
            szöveg += "[befejezés] long)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Igen_Nem (";
            szöveg += "[id] short,";
            szöveg += "[válasz] YESNO,";
            szöveg += "[megjegyzés] CHAR(255)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE mentésihelyek (";
            szöveg += "[sorszám] short,";
            szöveg += "[alprogram] CHAR(255),";
            szöveg += "[elérésiút] CHAR(255))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Típuszínektábla (";
            szöveg += "[típus] CHAR(50),";
            szöveg += "[színszám] Short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Kieg_Főmérnökség(string hely)
        {
            string szöveg;
            string jelszó = "Mocó";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Adatok (";
            szöveg += "[ID] short,";
            szöveg += "[szöveg] CHAR(255),";
            szöveg += "[email] CHAR(255))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Fortetípus (";
            szöveg += "[sorszám] short,";
            szöveg += "[ftípus] CHAR(20),";
            szöveg += "[telephely] CHAR(50),";
            szöveg += "[telephelytípus] CHAR(50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Fortevonal (";
            szöveg += "[fortavonal] CHAR(20))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Főkategóriatábla (";
            szöveg += "[sorszám] short,";
            szöveg += "[főkategória] CHAR(50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);


            szöveg = "CREATE TABLE Főkönyvtábla (";
            szöveg += "[id] short,";
            szöveg += "[név] CHAR(255),";
            szöveg += "[beosztás] CHAR(255))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE idő_korrekció (";
            szöveg += "[id] short,";
            szöveg += "[kiadási] short,";
            szöveg += "[érkezési] short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE időtábla (";
            szöveg += "[sorszám] short,";
            szöveg += "[reggel] date,";
            szöveg += "[este] date,";
            szöveg += "[délután] date)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE reklámtábla (";
            szöveg += "[méret] CHAR(30))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Sérülés (";
            szöveg += "[id] számláló,";
            szöveg += "[név] CHAR(30),";
            szöveg += "[vezér1] YESNO,";
            szöveg += "[csoport1] short,";
            szöveg += "[csport2] short,";
            szöveg += "[vezér2] YESNO,";
            szöveg += "[sorrend1] short,";
            szöveg += "[sorrend2] short,";
            szöveg += "[költséghely] CHAR(10))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Szolgálattábla (";
            szöveg += "[sorszám] short,";
            szöveg += "[szolgálatnév] CHAR(50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);


            szöveg = "CREATE TABLE Szolgálattelepeitábla (";
            szöveg += "[sorszám] short,";
            szöveg += "[telephelynév] CHAR(50),";
            szöveg += "[szolgálatnév] CHAR(50),";
            szöveg += "[felelősmunkahely] CHAR(50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Telephelytábla (";
            szöveg += "[sorszám] short,";
            szöveg += "[telephelynév] CHAR(50),";
            szöveg += "[telephelykönyvtár] CHAR(50),";
            szöveg += "[fortekód] CHAR(50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Típusaltípustábla (";
            szöveg += "[sorszám] short,";
            szöveg += "[típus] CHAR(50),";
            szöveg += "[altípus] CHAR(50),";
            szöveg += "[főkategória] CHAR(50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Típusrendezéstábla (";
            szöveg += "[sorszám] short,";
            szöveg += "[főkategória] CHAR(50),";
            szöveg += "[típus] CHAR(50),";
            szöveg += "[altípus] CHAR(50),";
            szöveg += "[telephely] CHAR(50),";
            szöveg += "[telephelyitípus] CHAR(255))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Végrehajtótábla (";
            szöveg += "[sorszám] short,";
            szöveg += "[telephelynév] CHAR(50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Kieg1_Főmérnökség(string hely)
        {
            string szöveg;
            string jelszó = "Mocó";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE beosegéd (";
            szöveg += "[beosztáskód] CHAR(4),";
            szöveg += "[túlóra] short,";
            szöveg += "[kezdőidő] date,";
            szöveg += "[végeidő] date,";
            szöveg += "[túlóraoka] CHAR(255),";
            szöveg += "[telephely] Char(50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Kijelöltnapok (";
            szöveg += "[telephely] CHAR(50),";
            szöveg += "[csoport] CHAR(5),";
            szöveg += "[dátum] date)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE túlórakeret (";
            szöveg += "[határ] short,";
            szöveg += "[parancs] short,";
            szöveg += "[telephely] CHAR(50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE túlórakeret (";
            szöveg += "[csoport] CHAR(5))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Váltóstábla (";
            szöveg += "[év] short,";
            szöveg += "[félév] short,";
            szöveg += "[csoport] CHAR(10),";
            szöveg += "[ZKnap] short,";
            szöveg += "[EPnap] short,";
            szöveg += "[Tperc] short,";
            szöveg += "[telephely] CHAR(50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);


        }

        public static void Kieg2_Főmérnökség(string hely)
        {
            string szöveg;
            string jelszó = "Mocó";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Beosztásciklus (";
            szöveg += "[beosztáskód] CHAR(5),";
            szöveg += "[hétnapja] CHAR(25),";
            szöveg += "[beosztásszöveg] CHAR(15),";
            szöveg += "[Id] számláló)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Doksi (";
            szöveg += "[kategória] CHAR(50),";
            szöveg += "[kód] CHAR(5),";
            szöveg += "[éves] CHAR(1))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE éjszakásciklus (";
            szöveg += "[beosztáskód] CHAR(5),";
            szöveg += "[hétnapja] CHAR(25),";
            szöveg += "[beosztásszöveg] CHAR(15),";
            szöveg += "[id] számláló)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Feorszámok (";
            szöveg += "[sorszám] számláló,";
            szöveg += "[feorszám] CHAR(10),";
            szöveg += "[feormegnevezés] CHAR(50),";
            szöveg += "[státus] short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE jogtípus (";
            szöveg += "[sorszám] számláló,";
            szöveg += "[típus] CHAR(50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE jogvonal (";
            szöveg += "[sorszám] számláló,";
            szöveg += "[szám] CHAR(10),";
            szöveg += "[megnevezés] CHAR(255))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Kiegmunkakör (";
            szöveg += "[id] short,";
            szöveg += "[megnevezés] CHAR(50),";
            szöveg += "[státus] short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Könyvtár (";
            szöveg += "[ID] számláló,";
            szöveg += "[Név] CHAR(30),";
            szöveg += "[vezér1] YESNO,";
            szöveg += "[csoport1] short,";
            szöveg += "[csoport2] short,";
            szöveg += "[vezér2] YESNO,";
            szöveg += "[sorrend1] short,";
            szöveg += "[sorrend2] short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Munkaidő (";
            szöveg += "[munkarendelnevezés] CHAR(10),";
            szöveg += "[munkaidő] short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Részmunkakör (";
            szöveg += "[id] short,";
            szöveg += "[megnevezés] CHAR(50),";
            szöveg += "[Státus] short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Valósbeosztás(";
            szöveg += "[id] számláló,";
            szöveg += "[kezdődátum] date,";
            szöveg += "[ciklusnap] short,";
            szöveg += "[megnevezés] CHAR(50),";
            szöveg += "[csoport] CHAR(5))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Védelem(";
            szöveg += "[sorszám] short,";
            szöveg += "[megnevezés] CHAR(20))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);


        }
    }
}
