using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Összevont
    {

        public string Azonosító { get; private set; }
        public long Státus { get; private set; }
        public string Üzem { get; private set; }
        public DateTime Miótaáll { get; private set; }
        public string Valóstípus { get; private set; }
        public DateTime Üzembehelyezés { get; private set; }
        public string Hibaleírása { get; private set; }

        public Adat_Összevont(string azonosító, long státus, string üzem, DateTime miótaáll, string valóstípus, DateTime üzembehelyezés, string hibaleírása)
        {
            Azonosító = azonosító;
            Státus = státus;
            Üzem = üzem;
            Miótaáll = miótaáll;
            Valóstípus = valóstípus;
            Üzembehelyezés = üzembehelyezés;
            Hibaleírása = hibaleírása;
        }
    }



}
