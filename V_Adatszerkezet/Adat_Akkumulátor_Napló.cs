using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Akkumulátor_Napló
    {
        public string Beépítve { get; private set; }
        public string Fajta { get; private set; }
        public string Gyártó { get; private set; }
        public string Gyáriszám { get; private set; }
        public string Típus { get; private set; }
        public DateTime Garancia { get; private set; }
        public DateTime Gyártásiidő { get; private set; }
        public int Státus { get; private set; }
        public string Megjegyzés { get; private set; }
        public DateTime Módosításdátuma { get; private set; }
        public int Kapacitás { get; private set; }
        public string Telephely { get; private set; }
        public DateTime Rögzítés { get; private set; }
        public string Rögzítő { get; private set; }


        public Adat_Akkumulátor_Napló(string beépítve, string fajta, string gyártó, string gyáriszám, string típus, DateTime garancia, DateTime gyártásiidő,
            int státus, string megjegyzés, DateTime módosításdátuma, int kapacitás, string telephely, DateTime rögzítés, string rögzítő)
        {
            Beépítve = beépítve;
            Fajta = fajta;
            Gyártó = gyártó;
            Gyáriszám = gyáriszám;
            Típus = típus;
            Garancia = garancia;
            Gyártásiidő = gyártásiidő;
            Státus = státus;
            Megjegyzés = megjegyzés;
            Módosításdátuma = módosításdátuma;
            Kapacitás = kapacitás;
            Telephely = telephely;
            Rögzítés = rögzítés;
            Rögzítő = rögzítő;
        }
    }
}
