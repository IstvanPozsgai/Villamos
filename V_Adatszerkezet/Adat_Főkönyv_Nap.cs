using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Főkönyv_Nap
    {
        public long Státus { get; private set; }
        public string Hibaleírása { get; private set; }
        public string Típus { get; private set; }
        public string Azonosító { get; private set; }
        public long Szerelvény { get; private set; }
        public string Viszonylat { get; private set; }
        public string Forgalmiszám { get; private set; }
        public long Kocsikszáma { get; private set; }
        public DateTime Tervindulás { get; private set; }
        public DateTime Tényindulás { get; private set; }
        public DateTime Tervérkezés { get; private set; }
        public DateTime Tényérkezés { get; private set; }
        public DateTime Miótaáll { get; private set; }
        public string Napszak { get; private set; }
        public string Megjegyzés { get; private set; }

        public string Telephely { get; private set; }

        public Adat_Főkönyv_Nap(long státus, string hibaleírása, string típus, string azonosító, long szerelvény, string viszonylat, string forgalmiszám, long kocsikszáma, DateTime tervindulás, DateTime tényindulás, DateTime tervérkezés, DateTime tényérkezés, DateTime miótaáll, string napszak, string megjegyzés)
        {
            Státus = státus;
            Hibaleírása = hibaleírása;
            Típus = típus;
            Azonosító = azonosító;
            Szerelvény = szerelvény;
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Kocsikszáma = kocsikszáma;
            Tervindulás = tervindulás;
            Tényindulás = tényindulás;
            Tervérkezés = tervérkezés;
            Tényérkezés = tényérkezés;
            Miótaáll = miótaáll;
            Napszak = napszak;
            Megjegyzés = megjegyzés;
        }

        public Adat_Főkönyv_Nap(string viszonylat, string forgalmiszám, long kocsikszáma, DateTime tervindulás, DateTime tényindulás, DateTime tervérkezés, DateTime tényérkezés, string napszak, string megjegyzés)
        {
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Kocsikszáma = kocsikszáma;
            Tervindulás = tervindulás;
            Tényindulás = tényindulás;
            Tervérkezés = tervérkezés;
            Tényérkezés = tényérkezés;
            Napszak = napszak;
            Megjegyzés = megjegyzés;
        }

        public Adat_Főkönyv_Nap(string viszonylat, string forgalmiszám, long kocsikszáma, DateTime tervindulás, DateTime tényindulás, DateTime tervérkezés, DateTime tényérkezés, string napszak,
            string megjegyzés, string azonosító)
        {
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Kocsikszáma = kocsikszáma;
            Tervindulás = tervindulás;
            Tényindulás = tényindulás;
            Tervérkezés = tervérkezés;
            Tényérkezés = tényérkezés;
            Napszak = napszak;
            Megjegyzés = megjegyzés;
            Azonosító = azonosító;
        }

        public Adat_Főkönyv_Nap(string viszonylat, string forgalmiszám, long kocsikszáma, DateTime tervindulás, DateTime tényindulás, DateTime tervérkezés, DateTime tényérkezés, string azonosító)
        {
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Kocsikszáma = kocsikszáma;
            Tervindulás = tervindulás;
            Tényindulás = tényindulás;
            Tervérkezés = tervérkezés;
            Tényérkezés = tényérkezés;
            Azonosító = azonosító;
        }

        public Adat_Főkönyv_Nap(long státus, string hibaleírása, string típus, string azonosító, long szerelvény, string viszonylat, string forgalmiszám, long kocsikszáma, DateTime tervindulás, DateTime tényindulás, DateTime tervérkezés, DateTime tényérkezés, DateTime miótaáll, string napszak, string megjegyzés, string telephely) : this(státus, hibaleírása, típus, azonosító, szerelvény, viszonylat, forgalmiszám, kocsikszáma, tervindulás, tényindulás, tervérkezés, tényérkezés, miótaáll, napszak, megjegyzés)
        {
            Telephely = telephely;
        }
    }

}
