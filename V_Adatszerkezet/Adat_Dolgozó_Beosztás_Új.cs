using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Dolgozó_Beosztás_Új
    {
        public string Dolgozószám { get; private set; }
        public DateTime Nap { get; private set; }
        public string Beosztáskód { get; private set; }
        public int Ledolgozott { get; private set; }
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
        public int AFTóra { get; private set; }
        public string AFTok { get; private set; }

        public Adat_Dolgozó_Beosztás_Új(string dolgozószám, DateTime nap, string beosztáskód, int ledolgozott,
            int túlóra, DateTime túlórakezd, DateTime túlóravég,
            int csúszóra, DateTime cSúszórakezd, DateTime csúszóravég,
            string megjegyzés, string túlóraok, string szabiok, bool kért, string csúszok, int aFTóra, string aFTok)
        {
            Dolgozószám = dolgozószám;
            Nap = nap;
            Beosztáskód = beosztáskód;
            Ledolgozott = ledolgozott;
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
            AFTóra = aFTóra;
            AFTok = aFTok;
        }


        public Adat_Dolgozó_Beosztás_Új(string dolgozószám, DateTime nap, string beosztáskód, int ledolgozott,
                                        int csúszóra, DateTime cSúszórakezd, DateTime csúszóravég, string csúszok)
        {
            Dolgozószám = dolgozószám;
            Nap = nap;
            Beosztáskód = beosztáskód;
            Ledolgozott = ledolgozott;
            Csúszóra = csúszóra;
            CSúszórakezd = cSúszórakezd;
            Csúszóravég = csúszóravég;
            Csúszok = csúszok;
        }

        public Adat_Dolgozó_Beosztás_Új(string dolgozószám, DateTime nap, string beosztáskód, int ledolgozott,
                                        int aFTóra, string aFTok)
        {
            Dolgozószám = dolgozószám;
            Nap = nap;
            Beosztáskód = beosztáskód;
            Ledolgozott = ledolgozott;
            AFTóra = aFTóra;
            AFTok = aFTok;
        }


    }
}
