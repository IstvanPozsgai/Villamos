using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_FőKiadási_adatok
    {
        public DateTime Dátum { get; private set; }
        public string Napszak { get; private set; }
        public long Forgalomban { get; private set; }
        public long Tartalék { get; private set; }
        public long Kocsiszíni { get; private set; }
        public long Félreállítás { get; private set; }
        public long Főjavítás { get; private set; }
        public long Személyzet { get; private set; }
        public long Kiadás { get; private set; }
        public string Főkategória { get; private set; }
        public string Típus { get; private set; }
        public string Altípus { get; private set; }
        public string Telephely { get; private set; }
        public string Szolgálat { get; private set; }
        public string Telephelyitípus { get; private set; }
        public long Munkanap { get; private set; }


        public Adat_FőKiadási_adatok(DateTime dátum, string napszak, long forgalomban, long tartalék, long kocsiszíni, long félreállítás, long főjavítás,
        long személyzet, long kiadás, string főkategória, string típus, string altípus, string telephely, string szolgálat, string telephelyitípus, long munkanap)
        {
            Dátum = dátum;
            Napszak = napszak;
            Forgalomban = forgalomban;
            Tartalék = tartalék;
            Kocsiszíni = kocsiszíni;
            Félreállítás = félreállítás;
            Főjavítás = főjavítás;
            Személyzet = személyzet;
            Kiadás = kiadás;
            Főkategória = főkategória;
            Típus = típus;
            Altípus = altípus;
            Telephely = telephely;
            Szolgálat = szolgálat;
            Telephelyitípus = telephelyitípus;
            Munkanap = munkanap;
        }

        public Adat_FőKiadási_adatok(DateTime dátum, string napszak, string telephely)
        {
            Dátum = dátum;
            Napszak = napszak;
            Telephely = telephely;
        }
    }
}
