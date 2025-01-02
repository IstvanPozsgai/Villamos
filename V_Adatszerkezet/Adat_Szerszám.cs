using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Szerszám_Napló
    {
        public string Azonosító { get; private set; }
        public string Honnan { get; private set; }
        public string Hova { get; private set; }
        public int Mennyiség { get; private set; }
        public string Módosította { get; private set; }
        public DateTime Módosításidátum { get; private set; }


        /// <summary>
        /// Rögzítés
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="honnan"></param>
        /// <param name="hova"></param>
        /// <param name="mennyiség"></param>
        public Adat_Szerszám_Napló(string azonosító, string honnan, string hova, int mennyiség)
        {
            Azonosító = azonosító;
            Honnan = honnan;
            Hova = hova;
            Mennyiség = mennyiség;
        }


        /// <summary>
        /// Lekérdezés
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="honnan"></param>
        /// <param name="hova"></param>
        /// <param name="mennyiség"></param>
        /// <param name="módosította"></param>
        /// <param name="módosításidátum"></param>
        public Adat_Szerszám_Napló(string azonosító, string honnan, string hova, int mennyiség, string módosította, DateTime módosításidátum)
        {
            Azonosító = azonosító;
            Honnan = honnan;
            Hova = hova;
            Mennyiség = mennyiség;
            Módosította = módosította;
            Módosításidátum = módosításidátum;
        }



    }
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

    public class Adat_Szerszám_Könyvtörzs
    {
        public string Szerszámkönyvszám { get; private set; }
        public string Szerszámkönyvnév { get; private set; }
        public string Felelős1 { get; private set; }
        public string Felelős2 { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Szerszám_Könyvtörzs(string szerszámkönyvszám, string szerszámkönyvnév)
        {
            Szerszámkönyvszám = szerszámkönyvszám;
            Szerszámkönyvnév = szerszámkönyvnév;
        }


        /// <summary>
        /// Új rögzítése
        /// </summary>
        /// <param name="szerszámkönyvszám"></param>
        /// <param name="szerszámkönyvnév"></param>
        /// <param name="felelős1"></param>
        /// <param name="felelős2"></param>
        public Adat_Szerszám_Könyvtörzs(string szerszámkönyvszám, string szerszámkönyvnév, string felelős1, string felelős2)
        {
            Szerszámkönyvszám = szerszámkönyvszám;
            Szerszámkönyvnév = szerszámkönyvnév;
            Felelős1 = felelős1;
            Felelős2 = felelős2;
        }


        /// <summary>
        /// Lekérdezés és módosítás
        /// </summary>
        /// <param name="szerszámkönyvszám"></param>
        /// <param name="szerszámkönyvnév"></param>
        /// <param name="felelős1"></param>
        /// <param name="felelős2"></param>
        /// <param name="státus"></param>
        public Adat_Szerszám_Könyvtörzs(string szerszámkönyvszám, string szerszámkönyvnév, string felelős1, string felelős2, bool státus)
        {
            Szerszámkönyvszám = szerszámkönyvszám;
            Szerszámkönyvnév = szerszámkönyvnév;
            Felelős1 = felelős1;
            Felelős2 = felelős2;
            Státus = státus;
        }
    }

    public class Adat_Szerszám_Könyvelés
    {
        public Adat_Szerszám_Cikktörzs  Azonosító { get; private set; }
        public Adat_Szerszám_Könyvtörzs Szerszámkönyvszám { get; private set; }
        public int Mennyiség { get; private set; }
        public DateTime Dátum { get; private set; }

        public string AzonosítóMás { get; private set; }

        public string SzerszámkönyvszámMás { get; private set; }

        /// <summary>
        /// Új rögzítése
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="szerszámkönyvszám"></param>
        /// <param name="mennyiség"></param>
        public Adat_Szerszám_Könyvelés(Adat_Szerszám_Cikktörzs azonosító, Adat_Szerszám_Könyvtörzs szerszámkönyvszám, int mennyiség)
        {
            Azonosító = azonosító;
            Szerszámkönyvszám = szerszámkönyvszám;
            Mennyiség = mennyiség;
        }

        public Adat_Szerszám_Könyvelés(int mennyiség, DateTime dátum, string azonosítóMás, string szerszámkönyvszámMás)
        {
            Mennyiség = mennyiség;
            Dátum = dátum;
            AzonosítóMás = azonosítóMás;
            SzerszámkönyvszámMás = szerszámkönyvszámMás;
        }




        /// <summary>
        /// Lekérdedezés
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="szerszámkönyvszám"></param>
        /// <param name="mennyiség"></param>
        /// <param name="dátum"></param>
        public Adat_Szerszám_Könyvelés(Adat_Szerszám_Cikktörzs azonosító, Adat_Szerszám_Könyvtörzs szerszámkönyvszám, int mennyiség, DateTime dátum)
        {
            Azonosító = azonosító;
            Szerszámkönyvszám = szerszámkönyvszám;
            Mennyiség = mennyiség;
            Dátum = dátum;
        }
    }
}
