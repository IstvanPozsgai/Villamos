using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Főkönyv_Személyzet
    {
        public DateTime Dátum { get; private set; }
        public string Napszak { get; private set; }
        public string Típus { get; private set; }
        public string Viszonylat { get; private set; }
        public string Forgalmiszám { get; private set; }
        public DateTime Tervindulás { get; private set; }
        public string Azonosító { get; private set; }

        public Adat_Főkönyv_Személyzet(DateTime dátum, string napszak, string típus, string viszonylat, string forgalmiszám, DateTime tervindulás, string azonosító)
        {
            Dátum = dátum;
            Napszak = napszak;
            Típus = típus;
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Tervindulás = tervindulás;
            Azonosító = azonosító;
        }
    }
}
