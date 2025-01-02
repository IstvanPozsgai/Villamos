using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Üzenet
    {
        public double Sorszám { get; private set; }
        public string Szöveg { get; private set; }
        public string Írta { get; private set; }
        public DateTime Mikor { get; private set; }
        public double Válaszsorszám { get; private set; }

        public Adat_Üzenet(double sorszám, string szöveg, string írta, DateTime mikor, double válaszsorszám)
        {
            Sorszám = sorszám;
            Szöveg = szöveg;
            Írta = írta;
            Mikor = mikor;
            Válaszsorszám = válaszsorszám;
        }
    }

    public class Adat_Üzenet_Olvasás
    {
        public double Sorszám { get; private set; }
        public string Ki { get; private set; }
        public double Üzenetid { get; private set; }
        public DateTime Mikor { get; private set; }
        public bool Olvasva { get; private set; }

        public Adat_Üzenet_Olvasás(double sorszám, string ki, double üzenetid, DateTime mikor, bool olvasva)
        {
            Sorszám = sorszám;
            Ki = ki;
            Üzenetid = üzenetid;
            Mikor = mikor;
            Olvasva = olvasva;
        }

        public Adat_Üzenet_Olvasás(double sorszám, string ki, double üzenetid, DateTime mikor)
        {
            Sorszám = sorszám;
            Ki = ki;
            Üzenetid = üzenetid;
            Mikor = mikor;
        }
    }

}
