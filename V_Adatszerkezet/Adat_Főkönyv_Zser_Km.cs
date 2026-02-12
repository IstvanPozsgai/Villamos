using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Főkönyv_Zser_Km
    {
        public string Azonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public int Napikm { get; private set; }
        public string Telephely { get; private set; }

        public Adat_Főkönyv_Zser_Km(string azonosító, DateTime dátum, int napikm, string telephely)
        {
            Azonosító = azonosító;
            Dátum = dátum;
            Napikm = napikm;
            Telephely = telephely;
        }

    }
}
