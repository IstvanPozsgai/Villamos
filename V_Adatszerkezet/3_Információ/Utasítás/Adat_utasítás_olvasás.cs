using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_utasítás_olvasás
    {
        public double Sorszám { get; private set; }
        public string Ki { get; private set; }
        public double Üzenetid { get; private set; }
        public DateTime Mikor { get; private set; }
        public bool Olvasva { get; private set; }

        public Adat_utasítás_olvasás(double sorszám, string ki, double üzenetid, DateTime mikor, bool olvasva)
        {
            Sorszám = sorszám;
            Ki = ki;
            Üzenetid = üzenetid;
            Mikor = mikor;
            Olvasva = olvasva;
        }


    }
}
