using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Szatube_Túlóra
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
        public DateTime Kezdőidő { get; private set; }
        public DateTime Befejezőidő { get; private set; }

        public Adat_Szatube_Túlóra(double sorszám, string törzsszám, string dolgozónév, DateTime kezdődátum, DateTime befejeződátum, int kivettnap, string szabiok, int státus, string rögzítette, DateTime rögzítésdátum, DateTime kezdőidő, DateTime befejezőidő)
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
            Kezdőidő = kezdőidő;
            Befejezőidő = befejezőidő;
        }

        public Adat_Szatube_Túlóra(double sorszám, string törzsszám, string dolgozónév, DateTime kezdődátum, DateTime befejeződátum, int kivettnap, string szabiok, string rögzítette, DateTime rögzítésdátum, DateTime kezdőidő, DateTime befejezőidő)
        {
            Sorszám = sorszám;
            Törzsszám = törzsszám;
            Dolgozónév = dolgozónév;
            Kezdődátum = kezdődátum;
            Befejeződátum = befejeződátum;
            Kivettnap = kivettnap;
            Szabiok = szabiok;
            Rögzítette = rögzítette;
            Rögzítésdátum = rögzítésdátum;
            Kezdőidő = kezdőidő;
            Befejezőidő = befejezőidő;
        }

        public Adat_Szatube_Túlóra(string törzsszám, DateTime kezdődátum, int státus)
        {
            Törzsszám = törzsszám;
            Kezdődátum = kezdődátum;
            Státus = státus;
        }
    }

}
