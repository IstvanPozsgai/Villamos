using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Szatube_Szabadság
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

        /// <summary>
        /// Teljes
        /// </summary>
        /// <param name="sorszám"></param>
        /// <param name="törzsszám"></param>
        /// <param name="dolgozónév"></param>
        /// <param name="kezdődátum"></param>
        /// <param name="befejeződátum"></param>
        /// <param name="kivettnap"></param>
        /// <param name="szabiok"></param>
        /// <param name="státus"></param>
        /// <param name="rögzítette"></param>
        /// <param name="rögzítésdátum"></param>
        public Adat_Szatube_Szabadság(double sorszám, string törzsszám, string dolgozónév, DateTime kezdődátum, DateTime befejeződátum, int kivettnap, string szabiok, int státus, string rögzítette, DateTime rögzítésdátum)
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

        public Adat_Szatube_Szabadság(string törzsszám, DateTime kezdődátum, string szabiok)
        {
            Törzsszám = törzsszám;
            Kezdődátum = kezdődátum;
            Szabiok = szabiok;
        }
    }

}
