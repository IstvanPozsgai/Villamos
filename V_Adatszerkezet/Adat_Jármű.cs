using System;
using System.Security.Policy;

namespace Villamos.Adatszerkezet
{
    public class Adat_Jármű
    {
        public string Azonosító { get; private set; }
        public long Hibák { get; private set; }
        public long Státus { get; private set; }
        public string Típus { get; set; }
        public string Üzem { get; set; }
        public bool Törölt { get; private set; }
        public long Hibáksorszáma { get; private set; }
        public bool Szerelvény { get; private set; }
        public long Szerelvénykocsik { get; private set; }
        public DateTime Miótaáll { get; private set; }
        public string Valóstípus { get; private set; }
        public string Valóstípus2 { get; private set; }
        public DateTime Üzembehelyezés { get; private set; }

        //Létrehozás
        public Adat_Jármű(string azonosító, long hibák, long státus, string típus, string üzem, bool törölt, long hibáksorszáma, bool szerelvény, long szerelvénykocsik, DateTime miótaáll, string valóstípus, string valóstípus2, DateTime üzembehelyezés)
        {
            Azonosító = azonosító;
            Hibák = hibák;
            Státus = státus;
            Típus = típus;
            Üzem = üzem;
            Törölt = törölt;
            Hibáksorszáma = hibáksorszáma;
            Szerelvény = szerelvény;
            Szerelvénykocsik = szerelvénykocsik;
            Miótaáll = miótaáll;
            Valóstípus = valóstípus;
            Valóstípus2 = valóstípus2;
            Üzembehelyezés = üzembehelyezés;
        }

        public Adat_Jármű(string azonosító, long hibák, long státus, string típus, string üzem, bool törölt, long hibáksorszáma, bool szerelvény, long szerelvénykocsik, DateTime miótaáll, string valóstípus, string valóstípus2)
        {
            Azonosító = azonosító;
            Hibák = hibák;
            Státus = státus;
            Típus = típus;
            Üzem = üzem;
            Törölt = törölt;
            Hibáksorszáma = hibáksorszáma;
            Szerelvény = szerelvény;
            Szerelvénykocsik = szerelvénykocsik;
            Miótaáll = miótaáll;
            Valóstípus = valóstípus;
            Valóstípus2 = valóstípus2;
        }
        /// <summary>
        /// Azonosító és a jármű státusa
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="státus"></param>
        public Adat_Jármű(string azonosító, long státus)
        {
            Azonosító = azonosító;
            Státus = státus;

        }

        /// <summary>
        /// Szerelvényhez
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="szerelvény"></param>
        /// <param name="szerelvénykocsik"></param>
        public Adat_Jármű(string azonosító, bool szerelvény, long szerelvénykocsik)
        {
            Azonosító = azonosító;
            Szerelvény = szerelvény;
            Szerelvénykocsik = szerelvénykocsik;
        }

        /// <summary>
        /// Állomány tábla
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="típus"></param>
        public Adat_Jármű(string azonosító, string típus)
        {
            Azonosító = azonosító;
            Típus = típus;
        }


    }

    /// <summary>
    /// Állomány táblánál külön beállítási lehetőség, hogy hol is van a kocsi
    /// </summary>
    public class Adat_Jármű_Vendég
    {
        public string Azonosító { get; private set; }
        public string Típus { get; private set; }
        public string BázisTelephely { get; private set; }
        public string KiadóTelephely { get; private set; }

        /// <summary>
        /// A szótárhoz készített 
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="kiadóTelephely"></param>
        public Adat_Jármű_Vendég(string azonosító, string kiadóTelephely)
        {
            Azonosító = azonosító;
            KiadóTelephely = kiadóTelephely;
        }
        /// <summary>
        /// Rögzítéshez, módosításhoz
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="típus"></param>
        /// <param name="bázisTelephely"></param>
        /// <param name="kiadóTelephely"></param>
        public Adat_Jármű_Vendég(string azonosító, string típus, string bázisTelephely, string kiadóTelephely)
        {
            Azonosító = azonosító;
            Típus = típus;
            BázisTelephely = bázisTelephely;
            KiadóTelephely = kiadóTelephely;
        }
    }

    public class Adat_Jármű_Napló
    {

        public string Azonosító { get; private set; }
        public string Típus { get; private set; }
        public string Honnan { get; private set; }
        public string Hova { get; private set; }
        public bool Törölt { get; private set; }
        public string Módosító { get; private set; }
        public DateTime Mikor { get; private set; }
        public string Céltelep { get; private set; }
        public int Üzenet { get; private set; }

        public Adat_Jármű_Napló(string azonosító, string típus, string honnan, string hova, bool törölt, string módosító, DateTime mikor, string céltelep, int üzenet)
        {
            Azonosító = azonosító;
            Típus = típus;
            Honnan = honnan;
            Hova = hova;
            Törölt = törölt;
            Módosító = módosító;
            Mikor = mikor;
            Céltelep = céltelep;
            Üzenet = üzenet;
        }
    }

    public class Adat_Jármű_hiba
    {

        public string Létrehozta { get; private set; }
        public long Korlát { get; private set; }
        public string Hibaleírása { get; private set; }
        public DateTime Idő { get; private set; }
        public bool Javítva { get; private set; }
        public string Típus { get; private set; }
        public string Azonosító { get; private set; }
        public long Hibáksorszáma { get; private set; }

        public Adat_Jármű_hiba(string létrehozta, long korlát, string hibaleírása, DateTime idő, bool javítva, string típus, string azonosító, long hibáksorszáma)
        {
            Létrehozta = létrehozta;
            Korlát = korlát;
            Hibaleírása = hibaleírása;
            Idő = idő;
            Javítva = javítva;
            Típus = típus;
            Azonosító = azonosító;
            Hibáksorszáma = hibáksorszáma;
        }
    }


    public class Adat_Jármű_Javításiátfutástábla
    {
        public DateTime Kezdődátum { get; private set; }
        public DateTime Végdátum { get; private set; }
        public string Azonosító { get; private set; }
        public string Hibaleírása { get; private set; }

        public Adat_Jármű_Javításiátfutástábla(DateTime kezdődátum, DateTime végdátum, string azonosító, string hibaleírása)
        {
            Kezdődátum = kezdődátum;
            Végdátum = végdátum;
            Azonosító = azonosító;
            Hibaleírása = hibaleírása;
        }
    }

    public class Adat_Jármű_Állomány_Típus
    {
        public long Id { get; private set; }
        public long Állomány { get; private set; }
        public string Típus { get; private set; }

        public Adat_Jármű_Állomány_Típus(long id, long állomány, string típus)
        {
            Id = id;
            Állomány = állomány;
            Típus = típus;
        }
    }

    public class Adat_Jármű_Xnapos
    {
        public DateTime Kezdődátum { get; private set; }
        public DateTime Végdátum { get; private set; }
        public string Azonosító { get; private set; }

        public string Hibaleírása { get; private set; }

        public Adat_Jármű_Xnapos(DateTime kezdődátum, DateTime végdátum, string azonosító, string hibaleírása)
        {
            Kezdődátum = kezdődátum;
            Végdátum = végdátum;
            Azonosító = azonosító;
            Hibaleírása = hibaleírása;
        }
    }
}
