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
        public static void Tükörtáblák(string hely)
        {
            string szöveg;
            string jelszó = "tükör";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Anyag (";
            szöveg += "[cikkszám]  char (20),";
            szöveg += "[anyagnév]  char (50),";
            szöveg += "[mennyiség]  Double,";
            szöveg += "[me]  char (10),";
            szöveg += "[ár]  Double,";
            szöveg += "[állapot]  char (3),";
            szöveg += "[Rendelés]  Double,";
            szöveg += "[mozgásnem]  char (5))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // tábla létrehozása
            szöveg = "CREATE TABLE Jelentés (";
            szöveg += "[sorszám]  Double,";
            szöveg += "[Telephely]  char (15),";
            szöveg += "[Dátum] DATE,";
            szöveg += "[Balesethelyszín]  char (150),";
            szöveg += "[Viszonylat]  char (20),";
            szöveg += "[Rendszám]  char (10),";
            szöveg += "[járművezető]  char (50),";
            szöveg += "[Rendelésszám]  Double,";
            szöveg += "[kimenetel]  short,";
            szöveg += "[Státus]  short,";
            szöveg += "[iktatószám]  char (50),";
            szöveg += "[Típus]  char (50),";
            szöveg += "[Szerelvény]  char (50),";
            szöveg += "[forgalmiakadály]  short,";
            szöveg += "[műszaki]  yesno,";
            szöveg += "[anyagikár]  yesno,";
            szöveg += "[biztosító]  char (20),";
            szöveg += "[személyisérülés]  yesno,";
            szöveg += "[személyisérülés1]  yesno,";
            szöveg += "[biztosítóidő]  short,";
            szöveg += "[mivelütközött]  char (150),";
            szöveg += "[anyagikárft]  Double,";
            szöveg += "[Leírás]  memo,";
            szöveg += "[Leírás1]  memo,";
            szöveg += "[Balesethelyszín1]  char (150),";
            szöveg += "[esemény]  char (150),";
            szöveg += "[anyagikárft1]  Double,";
            szöveg += "[Státus1]  short,";
            szöveg += "[kmóraállás]  char (20))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // tábla létrehozása
            szöveg = "CREATE TABLE Visszajelentés (";
            szöveg += "[Visszaszám]  char (10),";
            szöveg += "[munkaidő]  Double,";
            szöveg += "[storno]  char (1),";
            szöveg += "[Rendelés]  Double,";
            szöveg += "[Teljesítményfajta]  char (3))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // tábla létrehozása
            szöveg = "CREATE TABLE Művelet (";
            szöveg += "[Teljesítményfajta]  char (3),";
            szöveg += "[Rendelés]  Double,";
            szöveg += "[Visszaszám]  char (10),";
            szöveg += "[Műveletszöveg]  char (100))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // tábla létrehozása
            szöveg = "CREATE TABLE Költség (";
            szöveg += "[Rendelés]  Double,";
            szöveg += "[Anyagköltség]  Double,";
            szöveg += "[munkaköltség]  Double,";
            szöveg += "[Gépköltség]  Double,";
            szöveg += "[Szolgáltatás]  Double,";
            szöveg += "[Státus]  short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // tábla létrehozása
            szöveg = "CREATE TABLE ideig (";
            szöveg += "[Rendelés]  Double,";
            szöveg += "[Anyagköltség]  Double,";
            szöveg += "[munkaköltség]  Double,";
            szöveg += "[Gépköltség]  Double,";
            szöveg += "[Szolgáltatás]  Double,";
            szöveg += "[Státus]  short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // tábla létrehozása
            szöveg = "CREATE TABLE tarifa (";
            szöveg += "[id]  Double,";
            szöveg += "[d60tarifa]  Double,";
            szöveg += "[d03tarifa]  Double)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            // tábla létrehozása
            szöveg = "CREATE TABLE előkalkuláció (";
            szöveg += "[sorszám]  Double,";
            szöveg += "[Dátum] DATE,";
            szöveg += "[Rendszám]  char (10),";
            szöveg += "[sérülésleírás]  char (255),";
            szöveg += "[fénykép]  char (30),";
            szöveg += "[lakidő]  Double,";
            szöveg += "[villidő]  Double,";
            szöveg += "[asztidő]  Double,";
            szöveg += "[kárpidő]  Double,";
            szöveg += "[e1]  char (30),";
            szöveg += "[e1idő]  Double,";
            szöveg += "[e2]  char (30),";
            szöveg += "[e2idő]  Double,";
            szöveg += "[e3]  char (30),";
            szöveg += "[e3idő]  Double,";
            szöveg += "[óradíj]  Double,";
            szöveg += "[Anyagszükséglet]  char (30),";
            szöveg += "[Összköltség]  Double,";
            szöveg += "[Szolgáltatás]  Double,";
            szöveg += "[Rendelés]  Double,";
            szöveg += "[Megjegyzés]  char (255),";
            szöveg += "[fényidő]  Double,";
            szöveg += "[négypéldányos]  yesno,";
            szöveg += "[sorszám3]  Double)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Sérüléstábla(string hely)
        {
            string szöveg;
            string jelszó = "kismalac";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE tábla (";
            szöveg += "[id]  Double, ";
            szöveg += "[szöveg1]  char(250), ";
            szöveg += "[szöveg2]  char(250), ";
            szöveg += "[szöveg3]  char(250), ";
            szöveg += "[szöveg4]  char(250), ";
            szöveg += "[szöveg5]  char(250), ";
            szöveg += "[szöveg6]  char(250), ";
            szöveg += "[szöveg7]  char(250)) ";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = " ALTER TABLE tábla ADD ";
            szöveg += "[szöveg8] char(255) ";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = " ALTER TABLE tábla ADD ";
            szöveg += "szöveg9  char(250) ";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = " ALTER TABLE tábla ADD ";
            szöveg += "[szöveg10]  char(250) ";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = " ALTER TABLE tábla ADD ";
            szöveg += "[szöveg11]  char(250) ";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }

        public static void CAFtáblakészít(string hely)
        {
            string szöveg;
            string jelszó = "kismalac";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            // tábla létrehozása
            szöveg = "CREATE TABLE tábla (";
            szöveg += "[id]  long,";
            szöveg += "[cég]  char (255),";
            szöveg += "[név]  char (255),";
            szöveg += "[beosztás]  char (255))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
