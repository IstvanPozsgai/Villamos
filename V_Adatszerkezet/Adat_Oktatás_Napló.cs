using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
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

        public Adat_Oktatás_Napló(long iD, DateTime rögzítésdátuma, long státus, string rögzítő)
        {
            ID = iD;
            Rögzítésdátuma = rögzítésdátuma;
            Státus = státus;
            Rögzítő = rögzítő;
        }
    }
}
