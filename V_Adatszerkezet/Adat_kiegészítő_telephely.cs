using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_kiegészítő_telephely
    {

        public long Sorszám { get; private set; }
        public string Telephelynév { get; private set; }
        public string Telephelykönyvtár { get; private set; }
        public string Fortekód { get; private set; }

        public Adat_kiegészítő_telephely(long sorszám, string telephelynév, string telephelykönyvtár, string fortekód)
        {
            Sorszám = sorszám;
            Telephelynév = telephelynév;
            Telephelykönyvtár = telephelykönyvtár;
            Fortekód = fortekód;
        }
    }

    public class Adat_Kiegészítő_Hibaterv
    {
        public long Id { get; private set; }
        public string Szöveg { get; private set; }
        public bool Főkönyv { get; private set; }

        public Adat_Kiegészítő_Hibaterv(long id, string szöveg, bool főkönyv)
        {
            Id = id;
            Szöveg = szöveg;
            Főkönyv = főkönyv;
        }
    }

    public class Adat_Kiegészítő_főkönyvtábla
    {
        public long Id { get; private set; }
        public string Név { get; private set; }
        public string Beosztás { get; private set; }
        public string Email { get; private set; }

        public Adat_Kiegészítő_főkönyvtábla(long id, string név, string beosztás)
        {
            Id = id;
            Név = név;
            Beosztás = beosztás;
        }

        public Adat_Kiegészítő_főkönyvtábla(long id, string név, string beosztás, string email)
        {
            Id = id;
            Név = név;
            Beosztás = beosztás;
            Email = email;
        }
    }


    public class Adat_Kiegészítő_Feortipus
    {
        public string Típus { get; private set; }
        public string Ftípus { get; private set; }

        public Adat_Kiegészítő_Feortipus(string típus, string ftípus)
        {
            Típus = típus;
            Ftípus = ftípus;
        }
    }

    public class Adat_Kiegészítő_Felmentés
    {
        public int Id { get; private set; }
        public string Címzett { get; private set; }
        public string Másolat { get; private set; }
        public string Tárgy { get; private set; }
        public string Kértvizsgálat { get; private set; }
        public string Bevezetés { get; private set; }
        public string Tárgyalás { get; private set; }
        public string Befejezés { get; private set; }
        public string CiklusTípus { get; private set; }

        public Adat_Kiegészítő_Felmentés(int id, string címzett, string másolat, string tárgy, string kértvizsgálat,
            string bevezetés, string tárgyalás, string befejezés, string ciklusTípus)
        {
            Id = id;
            Címzett = címzett;
            Másolat = másolat;
            Tárgy = tárgy;
            Kértvizsgálat = kértvizsgálat;
            Bevezetés = bevezetés;
            Tárgyalás = tárgyalás;
            Befejezés = befejezés;
            CiklusTípus = ciklusTípus;
        }
    }

    public class Adat_Kiegészítő_Csoportbeosztás
    {
        public long Sorszám { get; private set; }
        public string Csoportbeosztás { get; private set; }
        public string Típus { get; private set; }

        public Adat_Kiegészítő_Csoportbeosztás(long sorszám, string csoportbeosztás, string típus)
        {
            Sorszám = sorszám;
            Csoportbeosztás = csoportbeosztás;
            Típus = típus;
        }
    }

    public class Adat_Kiegészítő_Beosztáskódok
    {
        public long Sorszám { get; private set; }
        public string Beosztáskód { get; private set; }
        public DateTime Munkaidőkezdet { get; private set; }
        public DateTime Munkaidővége { get; private set; }
        public int Munkaidő { get; private set; }
        public int Munkarend { get; private set; }
        public string Napszak { get; private set; }
        public bool Éjszakás { get; private set; }
        public bool Számoló { get; private set; }
        public int Óra0 { get; private set; }
        public int Óra1 { get; private set; }
        public int Óra2 { get; private set; }
        public int Óra3 { get; private set; }
        public int Óra4 { get; private set; }
        public int Óra5 { get; private set; }
        public int Óra6 { get; private set; }
        public int Óra7 { get; private set; }
        public int Óra8 { get; private set; }
        public int Óra9 { get; private set; }
        public int Óra10 { get; private set; }
        public int Óra11 { get; private set; }
        public int Óra12 { get; private set; }
        public int Óra13 { get; private set; }
        public int Óra14 { get; private set; }
        public int Óra15 { get; private set; }
        public int Óra16 { get; private set; }
        public int Óra17 { get; private set; }
        public int Óra18 { get; private set; }
        public int Óra19 { get; private set; }
        public int Óra20 { get; private set; }
        public int Óra21 { get; private set; }
        public int Óra22 { get; private set; }
        public int Óra23 { get; private set; }
        public string Magyarázat { get; private set; }

        public Adat_Kiegészítő_Beosztáskódok(long sorszám, string beosztáskód, DateTime munkaidőkezdet, DateTime munkaidővége, int munkaidő, int munkarend, string napszak, bool éjszakás, bool számoló, int óra0, int óra1, int óra2, int óra3, int óra4, int óra5, int óra6, int óra7, int óra8, int óra9, int óra10, int óra11, int óra12, int óra13, int óra14, int óra15, int óra16, int óra17, int óra18, int óra19, int óra20, int óra21, int óra22, int óra23, string magyarázat)
        {
            Sorszám = sorszám;
            Beosztáskód = beosztáskód;
            Munkaidőkezdet = munkaidőkezdet;
            Munkaidővége = munkaidővége;
            Munkaidő = munkaidő;
            Munkarend = munkarend;
            Napszak = napszak;
            Éjszakás = éjszakás;
            Számoló = számoló;
            Óra0 = óra0;
            Óra1 = óra1;
            Óra2 = óra2;
            Óra3 = óra3;
            Óra4 = óra4;
            Óra5 = óra5;
            Óra6 = óra6;
            Óra7 = óra7;
            Óra8 = óra8;
            Óra9 = óra9;
            Óra10 = óra10;
            Óra11 = óra11;
            Óra12 = óra12;
            Óra13 = óra13;
            Óra14 = óra14;
            Óra15 = óra15;
            Óra16 = óra16;
            Óra17 = óra17;
            Óra18 = óra18;
            Óra19 = óra19;
            Óra20 = óra20;
            Óra21 = óra21;
            Óra22 = óra22;
            Óra23 = óra23;
            Magyarázat = magyarázat;
        }

        public Adat_Kiegészítő_Beosztáskódok(long sorszám, string beosztáskód, DateTime munkaidőkezdet, DateTime munkaidővége, int munkaidő, int munkarend, bool éjszakás, bool számoló, string magyarázat)
        {
            Sorszám = sorszám;
            Beosztáskód = beosztáskód;
            Munkaidőkezdet = munkaidőkezdet;
            Munkaidővége = munkaidővége;
            Munkaidő = munkaidő;
            Munkarend = munkarend;
            Éjszakás = éjszakás;
            Számoló = számoló;
            Magyarázat = magyarázat;
        }
    }

    public class Adat_Kiegészítő_Szabadságok
    {

        public long Sorszám { get; private set; }
        public string Megnevezés { get; private set; }

        public Adat_Kiegészítő_Szabadságok(long sorszám, string megnevezés)
        {
            Sorszám = sorszám;
            Megnevezés = megnevezés;
        }
    }


    public class Adat_Kiegészítő_Jelenlétiív
    {
        public long Id { get; private set; }
        public string Szervezet { get; private set; }

        public Adat_Kiegészítő_Jelenlétiív(long id, string szervezet)
        {
            Id = id;
            Szervezet = szervezet;
        }
    }

    public class Adat_Kiegészítő_Igen_Nem
    {
        public long Id { get; private set; }
        public bool Válasz { get; private set; }
        public string Megjegyzés { get; private set; }

        public Adat_Kiegészítő_Igen_Nem(long id, bool válasz, string megjegyzés)
        {
            Id = id;
            Válasz = válasz;
            Megjegyzés = megjegyzés;
        }
    }

    public class Adat_Telep_Kieg_Fortetípus
    {
        public string Típus { get; private set; }

        public string Ftípus { get; private set; }

        public Adat_Telep_Kieg_Fortetípus(string típus, string ftípus)
        {
            Típus = típus;
            Ftípus = ftípus;
        }
    }

    public class Adat_Telep_Kiegészítő_SérülésCaf
    {
        public int Id { get; set; }
        public string Cég { get; set; }
        public string Név { get; set; }
        public string Beosztás { get; set; }

        public Adat_Telep_Kiegészítő_SérülésCaf(int id, string cég, string név, string beosztás)
        {
            Id = id;
            Cég = cég;
            Név = név;
            Beosztás = beosztás;
        }
    }


    public class Adat_Telep_Kiegészítő_Kidobó
    {
        public long Id { get; private set; }
        public string Telephely { get; private set; }

        public Adat_Telep_Kiegészítő_Kidobó(long id, string telephely)
        {
            Id = id;
            Telephely = telephely;
        }
    }

    public class Adat_Telep_Kiegészítő_SAP
    {
        public long Id { get; private set; }
        public string Felelősmunkahely { get; private set; }

        public Adat_Telep_Kiegészítő_SAP(long id, string felelősmunkahely)
        {
            Id = id;
            Felelősmunkahely = felelősmunkahely;
        }
    }


    public class Adat_Telep_Kiegészítő_E3típus
    {
        public string Típus { get; private set; }

        public Adat_Telep_Kiegészítő_E3típus(string típus)
        {
            Típus = típus;
        }
    }

    public class Adat_Telep_Kiegészítő_Takarítástípus
    {
        public string Típus { get; private set; }

        public Adat_Telep_Kiegészítő_Takarítástípus(string típus)
        {
            Típus = típus;
        }

    }

}
