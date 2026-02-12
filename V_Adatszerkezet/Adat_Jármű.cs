using System;

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
        /// Hiba rögzítéshez
        /// </summary>
        /// <param name="azonosító"></param>
        /// <param name="hibák"></param>
        /// <param name="státus"></param>
        public Adat_Jármű(string azonosító, long hibák, long státus)
        {
            Azonosító = azonosító;
            Hibák = hibák;
            Státus = státus;
        }

        public Adat_Jármű(string azonosító, long státus, DateTime miótaáll)
        {
            Azonosító = azonosító;
            Státus = státus;
            Miótaáll = miótaáll;
        }

        public Adat_Jármű(string azonosító, long hibák, long státus, DateTime miótaáll)
        {
            Azonosító = azonosító;
            Státus = státus;
            Miótaáll = miótaáll;
            Hibák = hibák;
        }

        public Adat_Jármű(DateTime üzembehelyezés, string azonosító)
        {
            Azonosító = azonosító;
            Üzembehelyezés = üzembehelyezés;
        }

        public Adat_Jármű(string azonosító, string valóstípus, string valóstípus2, DateTime üzembehelyezés)
        {
            Azonosító = azonosító;
            Valóstípus2 = valóstípus2;
            Valóstípus = valóstípus;
            Üzembehelyezés = üzembehelyezés;
        }

        public Adat_Jármű(string azonosító, string típus, string üzem)
        {
            Azonosító = azonosító;
            Típus = típus;
            Üzem = üzem;
        }
    }

}
