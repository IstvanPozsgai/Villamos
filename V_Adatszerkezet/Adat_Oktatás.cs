using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_OktatásTábla
    {
        public long IDoktatás { get; private set; }
        public string Téma { get; private set; }
        public string Kategória { get; private set; }
        public string Gyakoriság { get; private set; }
        public string Státus { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Telephely { get; private set; }
        public long Listázásisorrend { get; private set; }
        public long Ismétlődés { get; private set; }
        public string PDFfájl { get; private set; }

        public Adat_OktatásTábla(long iDoktatás, string téma, string kategória, string gyakoriság, string státus, DateTime dátum, string telephely, long listázásisorrend, long ismétlődés, string pDFfájl)
        {
            IDoktatás = iDoktatás;
            Téma = téma;
            Kategória = kategória;
            Gyakoriság = gyakoriság;
            Státus = státus;
            Dátum = dátum;
            Telephely = telephely;
            Listázásisorrend = listázásisorrend;
            Ismétlődés = ismétlődés;
            PDFfájl = pDFfájl;
        }
    }

}
