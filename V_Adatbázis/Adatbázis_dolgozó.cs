using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {

        public static void Dolgozói_Státus(string hely)
        {
            string szöveg = "";
            string jelszó = "forgalmiutasítás";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Státustábla (";
            szöveg += "[ID] long,";
            szöveg += "[Névki] CHAR(255),";
            szöveg += "[Részmunkaidős] Decimal,";
            szöveg += "[Hrazonosítóki] CHAR(255),";
            szöveg += "[Bérki] Double,";
            szöveg += "[telephelyki] CHAR(255),";
            szöveg += "[kilépésoka] CHAR(255),";
            szöveg += "[kilépésdátum] DATE,";
            szöveg += "[Névbe] CHAR(255),";
            szöveg += "[Hrazonosítóbe] CHAR(255),";
            szöveg += "[Bérbe] Double,";
            szöveg += "[Honnanjött] CHAR(255),";
            szöveg += "[telephelybe] CHAR(255),";
            szöveg += "[belépésidátum] DATE,";
            szöveg += "[Státusváltozások] CHAR(255),";
            szöveg += "[Státusváltozoka] CHAR(255),";
            szöveg += "[Megjegyzés] CHAR(255) )";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }


        public static void Dolgozói_Adatok(string hely)
        {
            string szöveg = "";
            string jelszó = "forgalmiutasítás";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Dolgozóalapadatok (";
            szöveg += "[Sorszám] long,";
            szöveg += "[DolgozóNév] CHAR(50),";
            szöveg += "[Dolgozószám] CHAR(8),";
            szöveg += "[Leánykori] CHAR(50),";
            szöveg += "[Anyja] CHAR(50),";
            szöveg += "[Születésiidő] DATE,";
            szöveg += "[Születésihely] CHAR(20),";
            szöveg += "[TAj] CHAR(9),";
            szöveg += "[ADÓ] CHAR(10),";
            szöveg += "[Belépésiidő] DATE,";
            szöveg += "[Lakcím] CHAR(50),";
            szöveg += "[Ideiglenescím] CHAR(50),";
            szöveg += "[Telefonszám1] CHAR(13),";
            szöveg += "[telefonszám2] CHAR(13),";
            szöveg += "[telefonszám3] CHAR(13),";
            szöveg += "[Munkakör] CHAR(50),";
            szöveg += "[Csopvez] YESNO,";
            szöveg += "[Csoport] CHAR(30),";
            szöveg += "[Munkarend] YESNO,";
            szöveg += "[Orvosiérvényesség] DATE,";
            szöveg += "[Orvosivizsgálat] DATE,";
            szöveg += "[Targoncaérvényesség] DATE,";
            szöveg += "[Emelőérvényesség] DATE,";
            szöveg += "[Kilépésiidő] DATE,";
            szöveg += "[emelőgépigazolvány] CHAR(15),";
            szöveg += "[nehézgépkezelőigazolvány] CHAR(20),";
            szöveg += "[targoncaigazolvány] CHAR(20),";
            szöveg += "[képernyősidő] DATE,";
            szöveg += "[nehézgépidő] DATE,";
            szöveg += "[feorsz] CHAR(6),";
            szöveg += "[jogosítványszám] CHAR(20),";
            szöveg += "[Jogosítványérvényesség] DATE,";
            szöveg += "[jogtanúsítvány] CHAR(20),";
            szöveg += "[jogorvosi] DATE,";
            szöveg += "[tűzvizsgaideje] DATE,";
            szöveg += "[tűzvizsgaérv] DATE,";
            szöveg += "[passzív] YESNO,";
            szöveg += "[jogosítványkategória] CHAR(15),";
            szöveg += "[Bejelentkezésinév] CHAR(15),";
            szöveg += "[főkönyvtitulus] CHAR(150),";
            szöveg += "[vezényelt] YESNO,";
            szöveg += "[vezényelve] YESNO,";
            szöveg += "[részmunkaidős] YESNO,";
            szöveg += "[alkalmazott] YESNO,";
            szöveg += "[csoportkód] CHAR(5),";
            szöveg += "[túlóraeng] YESNO,";
            szöveg += "[részmunkaidőperc] Decimal )";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }


        public static void Dolgozói_Beosztás_Adatok(string hely, string HRazonosító)
        {
            string szöveg = "";
            string jelszó = "kiskakas";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE " + HRazonosító + " (";
            szöveg += "[Nap] Single,";
            szöveg += "[Beosztáskód] CHAR(3),";
            szöveg += "[Ledolgozott] Single, ";
            szöveg += "[Túlóra] Single, ";
            szöveg += "[Túlórakezd] Date, ";
            szöveg += "[Túlóravég] Date, ";
            szöveg += "[Csúszóra] Single, ";
            szöveg += "[CSúszórakezd] Date, ";
            szöveg += "[Csúszóravég] Date, ";
            szöveg += "[Megjegyzés] CHAR(250), ";
            szöveg += "[Túlóraok] CHAR(250), ";
            szöveg += "[Szabiok] CHAR(250), ";
            szöveg += "[kért] YesNo, ";
            szöveg += "[Csúszok] CHAR(250), ";
            szöveg += "[AFTóra] Single, ";
            szöveg += "[AFTok] CHAR(250) ";
            szöveg += ")";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }


        public static void Dolgozói_Beosztás_Adatok_Új(string hely)
        {
            string szöveg = "";
            string jelszó = "kiskakas";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Beosztás (";
            szöveg += "[Dolgozószám] CHAR(8),";
            szöveg += "[Nap] Date,";
            szöveg += "[Beosztáskód] CHAR(3),";
            szöveg += "[Ledolgozott] Single, ";
            szöveg += "[Túlóra] Single, ";
            szöveg += "[Túlórakezd] Date, ";
            szöveg += "[Túlóravég] Date, ";
            szöveg += "[Csúszóra] Single, ";
            szöveg += "[CSúszórakezd] Date, ";
            szöveg += "[Csúszóravég] Date, ";
            szöveg += "[Megjegyzés] CHAR(250), ";
            szöveg += "[Túlóraok] CHAR(250), ";
            szöveg += "[Szabiok] CHAR(250), ";
            szöveg += "[kért] YesNo, ";
            szöveg += "[Csúszok] CHAR(250), ";
            szöveg += "[AFTóra] Single, ";
            szöveg += "[AFTok] CHAR(250) ";
            szöveg += ")";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Dolgozói_Bér_Adatok(string hely)
        {
            string jelszó = "fütyülősbarack";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            // létrehozzuk a táblát
            string szöveg = "CREATE TABLE ADATTábla (";
            szöveg += "[ADAT1] CHAR(20),";
            szöveg += "[Adat2] CHAR(20) )";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }


        public static void SzaTuBe_tábla(string hely)
        {

            string jelszó = "kertitörpe";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            // szabadság tábla
            string szöveg = "CREATE TABLE Szabadság (";
            szöveg += "[sorszám] DOUBLE,";
            szöveg += "[Törzsszám] CHAR(8),";
            szöveg += "[Dolgozónév] CHAR(50),";
            szöveg += "[Kezdődátum] DATE,";
            szöveg += "[Befejeződátum] DATE,";
            szöveg += "[Kivettnap] Single,";
            szöveg += "[Szabiok] CHAR(250),";
            szöveg += "[Státus] Single,";
            szöveg += "[Rögzítette] CHAR(50),";
            szöveg += "[rögzítésdátum] DATE";
            szöveg += ")";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Beteg (";
            szöveg += "[sorszám] DOUBLE,";
            szöveg += "[Törzsszám] CHAR(8),";
            szöveg += "[Dolgozónév] CHAR(50),";
            szöveg += "[Kezdődátum] DATE,";
            szöveg += "[Befejeződátum] DATE,";
            szöveg += "[Kivettnap] Single,";
            szöveg += "[Szabiok] CHAR(250),";
            szöveg += "[Státus] Single,";
            szöveg += "[Rögzítette] CHAR(50),";
            szöveg += "[rögzítésdátum] DATE";
            szöveg += ")";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Túlóra (";
            szöveg += "[sorszám] DOUBLE,";
            szöveg += "[Törzsszám] CHAR(8),";
            szöveg += "[Dolgozónév] CHAR(50),";
            szöveg += "[Kezdődátum] DATE,";
            szöveg += "[Befejeződátum] DATE,";
            szöveg += "[Kivettnap] Single,";
            szöveg += "[Szabiok] CHAR(250),";
            szöveg += "[Státus] Single,";
            szöveg += "[Rögzítette] CHAR(50),";
            szöveg += "[rögzítésdátum] DATE,";
            szöveg += "[Kezdőidő] DATE,";
            szöveg += "[Befejezőidő] DATE";
            szöveg += ")";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Csúsztatás (";
            szöveg += "[sorszám] DOUBLE,";
            szöveg += "[Törzsszám] CHAR(8),";
            szöveg += "[Dolgozónév] CHAR(50),";
            szöveg += "[Kezdődátum] DATE,";
            szöveg += "[Befejeződátum] DATE,";
            szöveg += "[Kivettnap] Single,";
            szöveg += "[Szabiok] CHAR(250),";
            szöveg += "[Státus] Single,";
            szöveg += "[Rögzítette] CHAR(50),";
            szöveg += "[rögzítésdátum] DATE,";
            szöveg += "[Kezdőidő] DATE,";
            szöveg += "[Befejezőidő] DATE";
            szöveg += ")";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE AFT (";
            szöveg += "[sorszám] DOUBLE,";
            szöveg += "[Törzsszám] CHAR(8),";
            szöveg += "[Dolgozónév] CHAR(50),";
            szöveg += "[dátum] DATE,";
            szöveg += "[AFTóra] Single,";
            szöveg += "[AFTok] CHAR(250),";
            szöveg += "[Státus] Single,";
            szöveg += "[Rögzítette] CHAR(50),";
            szöveg += "[rögzítésdátum] DATE";
            szöveg += ")";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Beosztás_Naplózása(string hely)
        {

            string jelszó = "kerekeskút";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            // létrehozzuk a táblát
            string szöveg = "CREATE TABLE Adatok (";
            szöveg += "[Sorszám] double,";
            szöveg += "[Dátum] date,";
            szöveg += "[Beosztáskód] CHAR(3),";
            szöveg += "[Túlóra] Single, ";
            szöveg += "[Túlórakezd] Date, ";
            szöveg += "[Túlóravég] Date, ";
            szöveg += "[Csúszóra] Single, ";
            szöveg += "[CSúszórakezd] Date, ";
            szöveg += "[Csúszóravég] Date, ";
            szöveg += "[Megjegyzés] CHAR(250), ";
            szöveg += "[Túlóraok] CHAR(250), ";
            szöveg += "[Szabiok] CHAR(250), ";
            szöveg += "[kért] YesNo, ";
            szöveg += "[Csúszok] CHAR(250), ";
            szöveg += "[Rögzítette] CHAR(50), ";
            szöveg += "[rögzítésdátum] Date, ";
            szöveg += "[dolgozónév] CHAR(50), ";
            szöveg += "[Törzsszám] CHAR(8), ";
            szöveg += "[AFTóra] Single, ";
            szöveg += "[AFTok] CHAR(250) ";
            szöveg += ")";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Beosztás_Tábla_Létrehozás(string hely)
        {

            string jelszó = "kiskakas";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            string szöveg = "CREATE TABLE Dolgozólista ( [Dolgozólista]  CHAR(8) )";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Nappalosmunkarendlétrehozás(string hely)
        {

            string jelszó = "katalin";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            // Naptár tábla
            string szöveg = "CREATE TABLE Naptár (";
            szöveg += "[Nap] CHAR(1),";
            szöveg += "[dátum] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Naptár1 (";
            szöveg += "[Nap] CHAR(1),";
            szöveg += "[dátum] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Naptár2 (";
            szöveg += "[Nap] CHAR(1),";
            szöveg += "[dátum] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Naptár3 (";
            szöveg += "[Nap] CHAR(1),";
            szöveg += "[dátum] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Naptár4 (";
            szöveg += "[Nap] CHAR(1),";
            szöveg += "[dátum] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Naptár5 (";
            szöveg += "[Nap] CHAR(1),";
            szöveg += "[dátum] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Naptár6 (";
            szöveg += "[Nap] CHAR(1),";
            szöveg += "[dátum] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Összesítő (";
            szöveg += "[perc] Long,";
            szöveg += "[dátum] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Összesítő1 (";
            szöveg += "[perc] Long,";
            szöveg += "[dátum] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Összesítő2 (";
            szöveg += "[perc] Long,";
            szöveg += "[dátum] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Összesítő3 (";
            szöveg += "[perc] Long,";
            szöveg += "[dátum] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Összesítő4 (";
            szöveg += "[perc] Long,";
            szöveg += "[dátum] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Összesítő5 (";
            szöveg += "[perc] Long,";
            szöveg += "[dátum] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Összesítő6 (";
            szöveg += "[perc] Long,";
            szöveg += "[dátum] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE kijelöltnapok (";
            szöveg += "[telephely] CHAR(50),";
            szöveg += "[csoport] CHAR(10),";
            szöveg += "[dátum] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Váltóstábla (";
            szöveg += "[telephely] CHAR(50),";
            szöveg += "[csoport] CHAR(10),";
            szöveg += "[év] Short,";
            szöveg += "[félév] Short,";
            szöveg += "[ZKnap] Double,";
            szöveg += "[EPnap] Double,";
            szöveg += "[Tperc] Double)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void Váltóscsopitábla(string hely)
        {

            string jelszó = "Gábor";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            string szöveg = "CREATE TABLE tábla (";
            szöveg += "[csoport] CHAR(10),";
            szöveg += "[telephely] CHAR(50),";
            szöveg += "[név] CHAR(50))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }





    }
}
