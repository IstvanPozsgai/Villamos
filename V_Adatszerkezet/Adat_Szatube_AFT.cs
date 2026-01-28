using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Szatube_AFT
    {
        public double Sorszám { get; private set; }
        public string Törzsszám { get; private set; }
        public string Dolgozónév { get; private set; }
        public DateTime Dátum { get; private set; }
        public int AFTóra { get; private set; }
        public string AFTok { get; private set; }
        public int Státus { get; private set; }
        public string Rögzítette { get; private set; }
        public DateTime Rögzítésdátum { get; private set; }

        public Adat_Szatube_AFT(double sorszám, string törzsszám, string dolgozónév, DateTime dátum, int aFTóra, string aFTok, int státus, string rögzítette, DateTime rögzítésdátum)
        {
            Sorszám = sorszám;
            Törzsszám = törzsszám;
            Dolgozónév = dolgozónév;
            Dátum = dátum;
            AFTóra = aFTóra;
            AFTok = aFTok;
            Státus = státus;
            Rögzítette = rögzítette;
            Rögzítésdátum = rögzítésdátum;
        }
    }
}
