using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Főkönyv_Nap
    {
        public long Státus { get; private set; }
        public string Hibaleírása { get; private set; }
        public string Típus { get; private set; }
        public string Azonosító { get; private set; }
        public long Szerelvény { get; private set; }
        public string Viszonylat { get; private set; }
        public string Forgalmiszám { get; private set; }
        public long Kocsikszáma { get; private set; }
        public DateTime Tervindulás { get; private set; }
        public DateTime Tényindulás { get; private set; }
        public DateTime Tervérkezés { get; private set; }
        public DateTime Tényérkezés { get; private set; }
        public DateTime Miótaáll { get; private set; }
        public string Napszak { get; private set; }
        public string Megjegyzés { get; private set; }

        public string Telephely { get; private set; }

        public Adat_Főkönyv_Nap(long státus, string hibaleírása, string típus, string azonosító, long szerelvény, string viszonylat, string forgalmiszám, long kocsikszáma, DateTime tervindulás, DateTime tényindulás, DateTime tervérkezés, DateTime tényérkezés, DateTime miótaáll, string napszak, string megjegyzés)
        {
            Státus = státus;
            Hibaleírása = hibaleírása;
            Típus = típus;
            Azonosító = azonosító;
            Szerelvény = szerelvény;
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Kocsikszáma = kocsikszáma;
            Tervindulás = tervindulás;
            Tényindulás = tényindulás;
            Tervérkezés = tervérkezés;
            Tényérkezés = tényérkezés;
            Miótaáll = miótaáll;
            Napszak = napszak;
            Megjegyzés = megjegyzés;
        }

        public Adat_Főkönyv_Nap(string viszonylat, string forgalmiszám, long kocsikszáma, DateTime tervindulás, DateTime tényindulás, DateTime tervérkezés, DateTime tényérkezés, string napszak, string megjegyzés)
        {
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Kocsikszáma = kocsikszáma;
            Tervindulás = tervindulás;
            Tényindulás = tényindulás;
            Tervérkezés = tervérkezés;
            Tényérkezés = tényérkezés;
            Napszak = napszak;
            Megjegyzés = megjegyzés;
        }

        public Adat_Főkönyv_Nap(string viszonylat, string forgalmiszám, long kocsikszáma, DateTime tervindulás, DateTime tényindulás, DateTime tervérkezés, DateTime tényérkezés, string napszak,
            string megjegyzés, string azonosító)
        {
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Kocsikszáma = kocsikszáma;
            Tervindulás = tervindulás;
            Tényindulás = tényindulás;
            Tervérkezés = tervérkezés;
            Tényérkezés = tényérkezés;
            Napszak = napszak;
            Megjegyzés = megjegyzés;
            Azonosító = azonosító;
        }

        public Adat_Főkönyv_Nap(string viszonylat, string forgalmiszám, long kocsikszáma, DateTime tervindulás, DateTime tényindulás, DateTime tervérkezés, DateTime tényérkezés, string azonosító)
        {
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Kocsikszáma = kocsikszáma;
            Tervindulás = tervindulás;
            Tényindulás = tényindulás;
            Tervérkezés = tervérkezés;
            Tényérkezés = tényérkezés;
            Azonosító = azonosító;
        }

        public Adat_Főkönyv_Nap(long státus, string hibaleírása, string típus, string azonosító, long szerelvény, string viszonylat, string forgalmiszám, long kocsikszáma, DateTime tervindulás, DateTime tényindulás, DateTime tervérkezés, DateTime tényérkezés, DateTime miótaáll, string napszak, string megjegyzés, string telephely) : this(státus, hibaleírása, típus, azonosító, szerelvény, viszonylat, forgalmiszám, kocsikszáma, tervindulás, tényindulás, tervérkezés, tényérkezés, miótaáll, napszak, megjegyzés)
        {
            Telephely = telephely;
        }
    }


    public class Adat_Főkönyv_Személyzet
    {
        public DateTime Dátum { get; private set; }
        public string Napszak { get; private set; }
        public string Típus { get; private set; }
        public string Viszonylat { get; private set; }
        public string Forgalmiszám { get; private set; }
        public DateTime Tervindulás { get; private set; }
        public string Azonosító { get; private set; }

        public Adat_Főkönyv_Személyzet(DateTime dátum, string napszak, string típus, string viszonylat, string forgalmiszám, DateTime tervindulás, string azonosító)
        {
            Dátum = dátum;
            Napszak = napszak;
            Típus = típus;
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Tervindulás = tervindulás;
            Azonosító = azonosító;
        }
    }

    public class Adat_FőKönyv_Típuscsere
    {

        public DateTime Dátum { get; private set; }
        public string Napszak { get; private set; }
        public string Típuselőírt { get; private set; }
        public string Típuskiadott { get; private set; }
        public string Viszonylat { get; private set; }
        public string Forgalmiszám { get; private set; }
        public DateTime Tervindulás { get; private set; }
        public string Azonosító { get; private set; }
        public string Kocsi { get; private set; }

        public Adat_FőKönyv_Típuscsere(DateTime dátum, string napszak, string típuselőírt, string típuskiadott, string viszonylat, string forgalmiszám, DateTime tervindulás, string azonosító, string kocsi)
        {
            Dátum = dátum;
            Napszak = napszak;
            Típuselőírt = típuselőírt;
            Típuskiadott = típuskiadott;
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Tervindulás = tervindulás;
            Azonosító = azonosító;
            Kocsi = kocsi;
        }
    }

    public class Adat_Főkönyv_ZSER
    {
        public string Viszonylat { get; private set; }
        public string Forgalmiszám { get; private set; }
        public DateTime Tervindulás { get; private set; }
        public DateTime Tényindulás { get; private set; }
        public DateTime Tervérkezés { get; private set; }
        public DateTime Tényérkezés { get; private set; }
        public string Napszak { get; private set; }
        public string Szerelvénytípus { get; private set; }
        public long Kocsikszáma { get; private set; }
        public string Megjegyzés { get; private set; }
        public string Kocsi1 { get; private set; }
        public string Kocsi2 { get; private set; }
        public string Kocsi3 { get; private set; }
        public string Kocsi4 { get; private set; }
        public string Kocsi5 { get; private set; }
        public string Kocsi6 { get; private set; }
        public string Ellenőrző { get; private set; }
        public string Státus { get; private set; }

        public Adat_Főkönyv_ZSER(string viszonylat, string forgalmiszám, DateTime tervindulás, DateTime tényindulás, DateTime tervérkezés, DateTime tényérkezés, string napszak, string szerelvénytípus, long kocsikszáma, string megjegyzés, string kocsi1, string kocsi2, string kocsi3, string kocsi4, string kocsi5, string kocsi6, string ellenőrző, string státus)
        {
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Tervindulás = tervindulás;
            Tényindulás = tényindulás;
            Tervérkezés = tervérkezés;
            Tényérkezés = tényérkezés;
            Napszak = napszak;
            Szerelvénytípus = szerelvénytípus;
            Kocsikszáma = kocsikszáma;
            Megjegyzés = megjegyzés;
            Kocsi1 = kocsi1;
            Kocsi2 = kocsi2;
            Kocsi3 = kocsi3;
            Kocsi4 = kocsi4;
            Kocsi5 = kocsi5;
            Kocsi6 = kocsi6;
            Ellenőrző = ellenőrző;
            Státus = státus;
        }

        public Adat_Főkönyv_ZSER(string viszonylat, string forgalmiszám, DateTime tervindulás)
        {
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Tervindulás = tervindulás;
        }

        public Adat_Főkönyv_ZSER(string viszonylat, string forgalmiszám, DateTime tervindulás, string ellenőrző)
        {
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Tervindulás = tervindulás;
            Ellenőrző = ellenőrző;
        }

        public Adat_Főkönyv_ZSER(string napszak, string viszonylat, string forgalmiszám, DateTime tervindulás)
        {
            Viszonylat = viszonylat;
            Forgalmiszám = forgalmiszám;
            Tervindulás = tervindulás;
            Napszak = napszak;
        }
    }

    public class Adat_Főkönyv_Zser_Km
    {
        public string Azonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public int Napikm { get; private set; }
        public string Telephely { get; private set; }

        public Adat_Főkönyv_Zser_Km(string azonosító, DateTime dátum, int napikm, string telephely)
        {
            Azonosító = azonosító;
            Dátum = dátum;
            Napikm = napikm;
            Telephely = telephely;
        }

        public Adat_Főkönyv_Zser_Km(string azonosító, int napikm)
        {
            Azonosító = azonosító;
            Napikm = napikm;
        }
    }

    public class Adat_Főkönyv_SegédTábla
    {
        public long Id { get; private set; }
        public string Bejelentkezésinév { get; private set; }

        public Adat_Főkönyv_SegédTábla(long id, string bejelentkezésinév)
        {
            Id = id;
            Bejelentkezésinév = bejelentkezésinév;
        }
    }
}
