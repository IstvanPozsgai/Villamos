using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Dolgozó_Beosztás_Napló
    {
        public double Sorszám { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Beosztáskód { get; private set; }
        public int Túlóra { get; private set; }
        public DateTime Túlórakezd { get; private set; }
        public DateTime Túlóravég { get; private set; }
        public int Csúszóra { get; private set; }
        public DateTime CSúszórakezd { get; private set; }
        public DateTime Csúszóravég { get; private set; }
        public string Megjegyzés { get; private set; }
        public string Túlóraok { get; private set; }
        public string Szabiok { get; private set; }
        public bool Kért { get; private set; }
        public string Csúszok { get; private set; }
        public string Rögzítette { get; private set; }
        public DateTime Rögzítésdátum { get; private set; }
        public string Dolgozónév { get; private set; }
        public string Törzsszám { get; private set; }
        public int AFTóra { get; private set; }
        public string AFTok { get; private set; }

        public Adat_Dolgozó_Beosztás_Napló(double sorszám, DateTime dátum, string beosztáskód, int túlóra, DateTime túlórakezd, DateTime túlóravég, int csúszóra, DateTime cSúszórakezd, DateTime csúszóravég, string megjegyzés, string túlóraok, string szabiok, bool kért, string csúszok, string rögzítette, DateTime rögzítésdátum, string dolgozónév, string törzsszám, int aFTóra, string aFTok)
        {
            Sorszám = sorszám;
            Dátum = dátum;
            Beosztáskód = beosztáskód;
            Túlóra = túlóra;
            Túlórakezd = túlórakezd;
            Túlóravég = túlóravég;
            Csúszóra = csúszóra;
            CSúszórakezd = cSúszórakezd;
            Csúszóravég = csúszóravég;
            Megjegyzés = megjegyzés;
            Túlóraok = túlóraok;
            Szabiok = szabiok;
            Kért = kért;
            Csúszok = csúszok;
            Rögzítette = rögzítette;
            Rögzítésdátum = rögzítésdátum;
            Dolgozónév = dolgozónév;
            Törzsszám = törzsszám;
            AFTóra = aFTóra;
            AFTok = aFTok;
        }
    }
}
