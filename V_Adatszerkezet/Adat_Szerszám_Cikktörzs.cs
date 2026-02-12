using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Villamos.Adatszerkezet;

namespace Villamos.Adatszerkezet
{
    public class Adat_Szerszám_Cikktörzs
    {

        public string Azonosító { get; private set; }
        public string Megnevezés { get; private set; }
        public string Méret { get; private set; }
        public string Hely { get; private set; }
        public string Leltáriszám { get; private set; }
        public DateTime Beszerzésidátum { get; private set; }
        public int Státus { get; private set; }
        public string Költséghely { get; private set; }
        public string Gyáriszám { get; private set; }

        public Adat_Szerszám_Cikktörzs(string azonosító, string megnevezés, string méret, string gyáriszám)
        {
            Azonosító = azonosító;
            Megnevezés = megnevezés;
            Méret = méret;
            Gyáriszám = gyáriszám;
        }

        public Adat_Szerszám_Cikktörzs(string azonosító, string megnevezés)
        {
            Azonosító = azonosító;
            Megnevezés = megnevezés;
        }


        /// <summary>
        /// Módosítás és lekérdezés
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="megnevezés"></param>
        /// <param name="méret"></param>
        /// <param name="hely"></param>
        /// <param name="leltáriszám"></param>
        /// <param name="beszerzésidátum"></param>
        /// <param name="státus"></param>
        /// <param name="költséghely"></param>
        /// <param name="gyáriszám"></param>
        public Adat_Szerszám_Cikktörzs(string azonosító, string megnevezés, string méret, string hely, string leltáriszám, DateTime beszerzésidátum, int státus, string költséghely, string gyáriszám)
        {
            Azonosító = azonosító;
            Megnevezés = megnevezés;
            Méret = méret;
            Hely = hely;
            Leltáriszám = leltáriszám;
            Beszerzésidátum = beszerzésidátum;
            Státus = státus;
            Költséghely = költséghely;
            Gyáriszám = gyáriszám;
        }

        public void Szerszám_nyilvántartás(string hely)
        {

            string szöveg;
            string jelszó = "csavarhúzó";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();

            //Létrehozzuk az adatbázist és beállítunk jelszót
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Cikktörzs (";
            szöveg += "[azonosító]  char (20),";
            szöveg += "[Megnevezés]  char (50),";
            szöveg += "[Méret]  char (15),";
            szöveg += "[Hely]  char (50),";
            szöveg += "[leltáriszám]  char (20),";
            szöveg += "[Beszerzésidátum] DATE,";
            szöveg += "[státus]  short,";
            szöveg += "[költséghely]  char (6),";
            szöveg += "[gyáriszám]  char (50))";

            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE könyvtörzs (";
            szöveg += "[szerszámkönyvszám]  char (10),";
            szöveg += "[szerszámkönyvnév]  char (50),";
            szöveg += "[felelős1]  char (50),";
            szöveg += "[felelős2]  char (50),";
            szöveg += "[státus]  yesno)";
            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Könyvelés (";
            szöveg += "[azonosító]  char (20),";
            szöveg += "[szerszámkönyvszám]  char (10),";
            szöveg += "[mennyiség]  short,";
            szöveg += "[dátum] DATE)";

            //Létrehozzuk az adattáblát
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

        }
    }
}
