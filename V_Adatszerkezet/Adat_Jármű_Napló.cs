using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
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
}
