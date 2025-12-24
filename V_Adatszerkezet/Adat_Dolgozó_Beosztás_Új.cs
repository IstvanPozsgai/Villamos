using System;

namespace Villamos.Adatszerkezet
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

        /// <summary>
        /// Teljes
        /// </summary>
        /// <param name="dolgozószám"></param>
        /// <param name="nap"></param>
        /// <param name="beosztáskód"></param>
        /// <param name="ledolgozott"></param>
        /// <param name="túlóra"></param>
        /// <param name="túlórakezd"></param>
        /// <param name="túlóravég"></param>
        /// <param name="csúszóra"></param>
        /// <param name="cSúszórakezd"></param>
        /// <param name="csúszóravég"></param>
        /// <param name="megjegyzés"></param>
        /// <param name="túlóraok"></param>
        /// <param name="szabiok"></param>
        /// <param name="kért"></param>
        /// <param name="csúszok"></param>
        /// <param name="aFTóra"></param>
        /// <param name="aFTok"></param>
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

        /// <summary>
        /// Csúsztatás
        /// </summary>
        /// <param name="dolgozószám"></param>
        /// <param name="nap"></param>
        /// <param name="beosztáskód"></param>
        /// <param name="ledolgozott"></param>
        /// <param name="csúszóra"></param>
        /// <param name="cSúszórakezd"></param>
        /// <param name="csúszóravég"></param>
        /// <param name="csúszok"></param>
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

        /// <summary>
        /// AFT
        /// </summary>
        /// <param name="dolgozószám"></param>
        /// <param name="nap"></param>
        /// <param name="beosztáskód"></param>
        /// <param name="ledolgozott"></param>
        /// <param name="aFTóra"></param>
        /// <param name="aFTok"></param>
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

        /// <summary>
        /// Túlóra
        /// </summary>
        /// <param name="dolgozószám"></param>
        /// <param name="nap"></param>
        /// <param name="túlóra"></param>
        /// <param name="túlórakezd"></param>
        /// <param name="túlóravég"></param>
        /// <param name="túlóraok"></param>
        public Adat_Dolgozó_Beosztás_Új(string dolgozószám, DateTime nap, int túlóra, DateTime túlórakezd, DateTime túlóravég, string túlóraok)
        {
            Dolgozószám = dolgozószám;
            Nap = nap;
            Túlóra = túlóra;
            Túlórakezd = túlórakezd;
            Túlóravég = túlóravég;
            Túlóraok = túlóraok;
        }

        /// <summary>
        /// Megjegyzés
        /// </summary>
        /// <param name="dolgozószám"></param>
        /// <param name="nap"></param>
        /// <param name="megjegyzés"></param>
        /// <param name="kért"></param>
        public Adat_Dolgozó_Beosztás_Új(string dolgozószám, DateTime nap, string megjegyzés, bool kért)
        {
            Dolgozószám = dolgozószám;
            Nap = nap;
            Megjegyzés = megjegyzés;
            Kért = kért;
        }

        public Adat_Dolgozó_Beosztás_Új(string dolgozószám, DateTime nap, string szabiok)
        {
            Dolgozószám = dolgozószám;
            Nap = nap;
            Szabiok = szabiok;
        }
    }
}
