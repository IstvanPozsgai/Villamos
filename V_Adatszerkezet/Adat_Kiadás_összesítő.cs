using System;

namespace Villamos.Villamos_Adatszerkezet
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
    }

    public class Adat_Személyzet_Adatok
    {
        public DateTime Dátum { get; private set; }
        public string Napszak { get; private set; }
        public string Szolgálat { get; private set; }
        public string Telephely { get; private set; }
        public string Típus { get; private set; }
        public string Viszonylat { get; private set; }
        public string Forgalmiszám { get; private set; }
        public DateTime Tervindulás { get; private set; }
        public string Azonosító { get; private set; }

        public Adat_Személyzet_Adatok(DateTime dátum, string napszak, string szolgálat, string telephely, string típus,
            string viszonylat, string forgalmiszám, DateTime tervindulás, string azonosító)
        {
            Dátum = dátum;
            Napszak = napszak;
            Szolgálat = szolgálat;
            Telephely = telephely;
            Típus = típus;
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Tervindulás = tervindulás;
            Azonosító = azonosító;
        }
    }

    public class Adat_Típuscsere_Adatok
    {
        public DateTime Dátum { get; private set; }
        public string Napszak { get; private set; }
        public string Szolgálat { get; private set; }
        public string Telephely { get; private set; }
        public string Típuselőírt { get; private set; }
        public string Típuskiadott { get; private set; }
        public string Viszonylat { get; private set; }
        public string Forgalmiszám { get; private set; }
        public DateTime Tervindulás { get; private set; }
        public string Azonosító { get; private set; }

        public Adat_Típuscsere_Adatok(DateTime dátum, string napszak, string szolgálat, string telephely, string típuselőírt,
        string típuskiadott, string viszonylat, string forgalmiszám, DateTime tervindulás, string azonosító)
        {
            Dátum = dátum;
            Napszak = napszak;
            Szolgálat = szolgálat;
            Telephely = telephely;
            Típuselőírt = típuselőírt;
            Típuskiadott = típuskiadott;
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Tervindulás = tervindulás;
            Azonosító = azonosító;
        }
    }

    public class Adat_Forte_Kiadási_Adatok
    {
        public DateTime Dátum { get; private set; }
        public string Napszak { get; private set; }
        public string Telephelyforte { get; private set; }
        public string Típusforte { get; private set; }
        public string Telephely { get; private set; }
        public string Típus { get; private set; }
        public long Kiadás { get; private set; }
        public long Munkanap { get; private set; }

        public Adat_Forte_Kiadási_Adatok(DateTime dátum, string napszak, string telephelyforte, 
            string típusforte, string telephely, string típus, long kiadás, long munkanap)
        {
            Dátum = dátum;
            Napszak = napszak;
            Telephelyforte = telephelyforte;
            Típusforte = típusforte;
            Telephely = telephely;
            Típus = típus;
            Kiadás = kiadás;
            Munkanap = munkanap;
        }
    }








}


