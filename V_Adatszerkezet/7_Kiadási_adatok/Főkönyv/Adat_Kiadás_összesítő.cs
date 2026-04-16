using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiadás_összesítő
    {
        public DateTime Dátum { get; private set; }
        public string Napszak { get; private set; }
        public string Típus { get; private set; }
        public int Forgalomban { get; private set; }
        public int Tartalék { get; private set; }
        public int Kocsiszíni { get; private set; }
        public int Félreállítás { get; private set; }
        public int Főjavítás { get; private set; }
        public int Személyzet { get; private set; }

        public Adat_Kiadás_összesítő(DateTime dátum, string napszak, string típus, int forgalomban, int tartalék, int kocsiszíni, int félreállítás, int főjavítás, int személyzet)
        {
            Dátum = dátum;
            Napszak = napszak;
            Típus = típus;
            Forgalomban = forgalomban;
            Tartalék = tartalék;
            Kocsiszíni = kocsiszíni;
            Félreállítás = félreállítás;
            Főjavítás = főjavítás;
            Személyzet = személyzet;
        }
    }
}


