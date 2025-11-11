using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Szatube_Beteg
    {
        public double Sorszám { get; private set; }
        public string Törzsszám { get; private set; }
        public string Dolgozónév { get; private set; }
        public DateTime Kezdődátum { get; private set; }
        public DateTime Befejeződátum { get; private set; }
        public int Kivettnap { get; private set; }
        public string Szabiok { get; private set; }
        public int Státus { get; private set; }
        public string Rögzítette { get; private set; }
        public DateTime Rögzítésdátum { get; private set; }

        public Adat_Szatube_Beteg(double sorszám, string törzsszám, string dolgozónév, DateTime kezdődátum, DateTime befejeződátum, int kivettnap, string szabiok, int státus, string rögzítette, DateTime rögzítésdátum)
        {
            Sorszám = sorszám;
            Törzsszám = törzsszám;
            Dolgozónév = dolgozónév;
            Kezdődátum = kezdődátum;
            Befejeződátum = befejeződátum;
            Kivettnap = kivettnap;
            Szabiok = szabiok;
            Státus = státus;
            Rögzítette = rögzítette;
            Rögzítésdátum = rögzítésdátum;
        }

        public Adat_Szatube_Beteg(string törzsszám, DateTime kezdődátum)
        {
            Törzsszám = törzsszám;
            Kezdődátum = kezdődátum;
        }
    }

}
