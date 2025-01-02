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

        public static void Technológia_Adat(string hely)
        {
            string szöveg;
            string jelszó = "Bezzegh";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE technológia (";
            szöveg += " [Id] Long PRIMARY KEY,";
            szöveg += " [Részegység] CHAR(10),";
            szöveg += " [Munka_utasítás_szám] CHAR(10),";
            szöveg += " [Utasítás_Cím] CHAR(250),";
            szöveg += " [Utasítás_leírás] memo,";
            szöveg += " [Paraméter] memo,";
            szöveg += " [Karb_ciklus_eleje] short,";
            szöveg += " [Karb_ciklus_vége] short,";
            szöveg += " [Érv_kezdete] dateTime,";
            szöveg += " [Érv_vége] dateTime,";
            szöveg += " [Szakmai_bontás] CHAR(50),";
            szöveg += " [Munkaterületi_bontás]  CHAR(50),";
            szöveg += " [Altípus] CHAR(50),";
            szöveg += " [Kenés] yesno)";
            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Karbantartás (";
            szöveg += " [fokozat] char(20), ";
            szöveg += " [sorszám] short PRIMARY KEY, ";
            szöveg += " [csoportos] short, ";
            szöveg += " [elérés] char(20)," ;
            szöveg += " [verzió] char(20),)";
            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE típus_tábla (";
            szöveg += " [id] Long , ";
            szöveg += " [típus] char(20))";
            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE kivételek (";
            szöveg += " [Id] Long , ";
            szöveg += " [Azonosító] char(20),";
            szöveg += " [Altípus] char(50))";
            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // másodlagos kulcsok
            szöveg = "ALTER TABLE technológia ";
            szöveg += "ADD FOREIGN KEY(Karb_ciklus_eleje) REFERENCES Karbantartás(sorszám)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "ALTER TABLE technológia ";
            szöveg += "ADD FOREIGN KEY(Karb_ciklus_vége) REFERENCES Karbantartás(sorszám)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Technológia_ALAPAdat(string hely)
        {
            string szöveg;
            string jelszó = "Bezzegh";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Típus_tábla (";
            szöveg += " [Id] short,";
            szöveg += " [Típus] CHAR(20))";

            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Technológia_Telep(string hely, string telephely)
        {
            string szöveg;
            string jelszó = "Bezzegh";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = $"CREATE TABLE {telephely} (";
            szöveg += " [technológia_Id] Long ,";
            szöveg += " [Karbantartási_fokozat] char(20), ";
            szöveg += " [Változatnév] CHAR(50),";
            szöveg += " [Végzi] CHAR(50))";

            //Létrehozzuk az adattáblát

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);


        }

        public static void Technológia_Rendelés(string hely, string telephely)
        {
            string szöveg;
            string jelszó = "Bezzegh";
            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = $"CREATE TABLE {telephely} (";
            szöveg += " [Év] Long ,";
            szöveg += " [Karbantartási_fokozat] char(20), ";
            szöveg += " [Technológia_típus] CHAR(50),";
            szöveg += " [Rendelésiszám] CHAR(20))";

            //Létrehozzuk az adattáblát

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }
    }
}
