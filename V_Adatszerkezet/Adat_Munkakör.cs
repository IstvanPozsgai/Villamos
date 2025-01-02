using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Munkakör
    {
        public long ID { get; private set; }
        public string Megnevezés { get; private set; }
        public string PDFfájlnév { get; private set; }
        public long Státus { get; private set; }
        public string Telephely { get; private set; }
        public string HRazonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Rögzítő { get; private set; }

        public Adat_Munkakör(long iD, string megnevezés, string pDFfájlnév, long státus, string telephely, string hRazonosító, DateTime dátum, string rögzítő)
        {
            ID = iD;
            Megnevezés = megnevezés;
            PDFfájlnév = pDFfájlnév;
            Státus = státus;
            Telephely = telephely;
            HRazonosító = hRazonosító;
            Dátum = dátum;
            Rögzítő = rögzítő;
        }
    }
}
