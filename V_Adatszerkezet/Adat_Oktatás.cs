using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Villamos_Adatszerkezet
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


    public class Adat_OktatásiSegéd
    {
        public long IDoktatás { get; private set; }
        public string Telephely { get; private set; }
        public string Oktatásoka { get; private set; }
        public string Oktatástárgya { get; private set; }
        public string Oktatáshelye { get; private set; }
        public long Oktatásidőtartama { get; private set; }
        public string Oktató { get; private set; }
        public string Oktatóbeosztása { get; private set; }
        public string Egyébszöveg { get; private set; }
        public string Email { get; private set; }
        public long Oktatás { get; private set; }

        public Adat_OktatásiSegéd(long iDoktatás, string telephely, string oktatásoka, string oktatástárgya, string oktatáshelye, long oktatásidőtartama, string oktató, string oktatóbeosztása, string egyébszöveg, string email, long oktatás)
        {
            IDoktatás = iDoktatás;
            Telephely = telephely;
            Oktatásoka = oktatásoka;
            Oktatástárgya = oktatástárgya;
            Oktatáshelye = oktatáshelye;
            Oktatásidőtartama = oktatásidőtartama;
            Oktató = oktató;
            Oktatóbeosztása = oktatóbeosztása;
            Egyébszöveg = egyébszöveg;
            Email = email;
            Oktatás = oktatás;
        }
    }


    public class Adat_Oktatásrajelöltek
    {
        public string HRazonosító { get; private set; }
        public long IDoktatás { get; private set; }
        public DateTime Mikortól { get; private set; }
        public long Státus { get; private set; }
        public string Telephely { get; private set; }

        public Adat_Oktatásrajelöltek(string hRazonosító, long iDoktatás, DateTime mikortól, long státus, string telephely)
        {
            HRazonosító = hRazonosító;
            IDoktatás = iDoktatás;
            Mikortól = mikortól;
            Státus = státus;
            Telephely = telephely;
        }
    }

    public class Adat_Oktatás_Napló
    {
        public long ID { get; private set; }
        public string HRazonosító { get; private set; }
        public long IDoktatás { get; private set; }
        public DateTime Oktatásdátuma { get; private set; }
        public string Kioktatta { get; private set; }
        public DateTime Rögzítésdátuma { get; private set; }
        public string Telephely { get; private set; }
        public string PDFFájlneve { get; private set; }
        public long Számonkérés { get; private set; }
        public long Státus { get; private set; }
        public string Rögzítő { get; private set; }
        public string Megjegyzés { get; private set; }

        public Adat_Oktatás_Napló(long iD, string hRazonosító, long iDoktatás, DateTime oktatásdátuma, string kioktatta, DateTime rögzítésdátuma, string telephely, string pDFFájlneve, long számonkérés, long státus, string rögzítő, string megjegyzés)
        {
            ID = iD;
            HRazonosító = hRazonosító;
            IDoktatás = iDoktatás;
            Oktatásdátuma = oktatásdátuma;
            Kioktatta = kioktatta;
            Rögzítésdátuma = rögzítésdátuma;
            Telephely = telephely;
            PDFFájlneve = pDFFájlneve;
            Számonkérés = számonkérés;
            Státus = státus;
            Rögzítő = rögzítő;
            Megjegyzés = megjegyzés;
        }
    }
}
