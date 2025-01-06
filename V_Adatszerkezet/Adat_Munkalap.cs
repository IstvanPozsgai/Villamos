using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_MunkaRend
    {
        public long ID { get; private set; }
        public string Munkarend { get; private set; }
        public bool Látszódik { get; private set; }

        public Adat_MunkaRend(long iD, string munkarend, bool látszódik)
        {
            ID = iD;
            Munkarend = munkarend;
            Látszódik = látszódik;
        }
    }

    public class Adat_Munka_Folyamat
    {
        public long ID { get; private set; }
        public string Rendelésiszám { get; private set; }
        public string Azonosító { get; private set; }
        public string Munkafolyamat { get; private set; }
        public bool Látszódik { get; private set; }

        public Adat_Munka_Folyamat(long iD, string rendelésiszám, string azonosító, string munkafolyamat, bool látszódik)
        {
            ID = iD;
            Rendelésiszám = rendelésiszám;
            Azonosító = azonosító;
            Munkafolyamat = munkafolyamat;
            Látszódik = látszódik;
        }
    }

    public class Adat_Munka_Szolgálat
    {
        public string Költséghely { get; private set; }
        public string Szolgálat { get; private set; }
        public string Üzem { get; private set; }
        public string A1 { get; private set; }
        public string A2 { get; private set; }
        public string A3 { get; private set; }

        public string A4 { get; private set; }
        public string A5 { get; private set; }

        public string A6 { get; private set; }
        public string A7 { get; private set; }

        public Adat_Munka_Szolgálat(string költséghely, string szolgálat, string üzem, string a1, string a2, string a3, string a4, string a5, string a6, string a7)
        {
            Költséghely = költséghely;
            Szolgálat = szolgálat;
            Üzem = üzem;
            A1 = a1;
            A2 = a2;
            A3 = a3;
            A4 = a4;
            A5 = a5;
            A6 = a6;
            A7 = a7;
        }
    }

    public class Adat_Munkalapelszámoló
    {
        public long ID { get; private set; }
        public long Idő { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Megnevezés { get; private set; }
        public string Művelet { get; private set; }
        public string Pályaszám { get; private set; }
        public string Rendelés { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Munkalapelszámoló(long iD, long idő, DateTime dátum, string megnevezés, string művelet, string pályaszám, string rendelés, bool státus)
        {
            ID = iD;
            Idő = idő;
            Dátum = dátum;
            Megnevezés = megnevezés;
            Művelet = művelet;
            Pályaszám = pályaszám;
            Rendelés = rendelés;
            Státus = státus;
        }
    }

    public class Adat_Munkalapösszesítő
    {
        public long ID { get; private set; }
        public string Megnevezés { get; private set; }
        public string Műveletet { get; private set; }
        public string Pályaszám { get; private set; }
        public string Rendelés { get; private set; }

        public Adat_Munkalapösszesítő(long iD, string megnevezés, string műveletet, string pályaszám, string rendelés)
        {
            ID = iD;
            Megnevezés = megnevezés;
            Műveletet = műveletet;
            Pályaszám = pályaszám;
            Rendelés = rendelés;
        }
    }

    public class Adat_Munka_Idő
    {
        public long ID { get; private set; }
        public long Idő { get; private set; }

        public Adat_Munka_Idő(long iD, long idő)
        {
            ID = iD;
            Idő = idő;
        }
    }

    public class Adat_Munka_Rendelés
    {
        public long ID { get; private set; }

        public string Megnevezés { get; private set; }
        public string Műveletet { get; private set; }
        public string Pályaszám { get; private set; }
        public string Rendelés { get; private set; }

        public Adat_Munka_Rendelés(long iD, string megnevezés, string műveletet, string pályaszám, string rendelés)
        {
            ID = iD;
            Megnevezés = megnevezés;
            Műveletet = műveletet;
            Pályaszám = pályaszám;
            Rendelés = rendelés;
        }
    }

    public class Adat_Munka_Adatok
    {
        public long ID { get; private set; }
        public int Idő { get; private set; }
        public int SUMIdő { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Megnevezés { get; private set; }
        public string Művelet { get; private set; }
        public string Pályaszám { get; private set; }
        public string Rendelés { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Munka_Adatok(long iD, int idő, DateTime dátum, string megnevezés, string művelet, string pályaszám, string rendelés, bool státus)
        {
            ID = iD;
            Idő = idő;
            Dátum = dátum;
            Megnevezés = megnevezés;
            Művelet = művelet;
            Pályaszám = pályaszám;
            Rendelés = rendelés;
            Státus = státus;
        }

        public Adat_Munka_Adatok( string megnevezés, string művelet, string pályaszám, string rendelés)
        {
            Megnevezés = megnevezés;
            Művelet = művelet;
            Pályaszám = pályaszám;
            Rendelés = rendelés;
        }

        public Adat_Munka_Adatok(int sUMIdő, string művelet, string rendelés)
        {
            SUMIdő = sUMIdő;
            Művelet = művelet;
            Rendelés = rendelés;
        }
    }
}
