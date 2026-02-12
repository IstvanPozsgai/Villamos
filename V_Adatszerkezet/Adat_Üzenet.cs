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


}
