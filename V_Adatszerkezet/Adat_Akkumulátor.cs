using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Akkumulátor
    {
        public string Beépítve { get; private set; }
        public string Fajta { get; private set; }
        public string Gyártó { get; private set; }
        public string Gyáriszám { get; private set; }
        public string Típus { get; private set; }
        public DateTime Garancia { get; private set; }
        public DateTime Gyártásiidő { get; private set; }
        public int Státus { get; private set; }
        public string Megjegyzés { get; private set; }
        public DateTime Módosításdátuma { get; private set; }
        public int Kapacitás { get; private set; }
        public string Telephely { get; private set; }

        public Adat_Akkumulátor(string beépítve, string fajta, string gyártó, string gyáriszám, string típus, DateTime garancia, DateTime gyártásiidő, int státus, string megjegyzés, DateTime módosításdátuma, int kapacitás, string telephely)
        {
            Beépítve = beépítve;
            Fajta = fajta;
            Gyártó = gyártó;
            Gyáriszám = gyáriszám;
            Típus = típus;
            Garancia = garancia;
            Gyártásiidő = gyártásiidő;
            Státus = státus;
            Megjegyzés = megjegyzés;
            Módosításdátuma = módosításdátuma;
            Kapacitás = kapacitás;
            Telephely = telephely;
        }

        public Adat_Akkumulátor(string beépítve, string gyáriszám, int státus, DateTime módosításdátuma)
        {
            Beépítve = beépítve;
            Gyáriszám = gyáriszám;
            Státus = státus;
            Módosításdátuma = módosításdátuma;
        }
    }

    public class Adat_Akkumulátor_Napló
    {
        public string Beépítve { get; private set; }
        public string Fajta { get; private set; }
        public string Gyártó { get; private set; }
        public string Gyáriszám { get; private set; }
        public string Típus { get; private set; }
        public DateTime Garancia { get; private set; }
        public DateTime Gyártásiidő { get; private set; }
        public int Státus { get; private set; }
        public string Megjegyzés { get; private set; }
        public DateTime Módosításdátuma { get; private set; }
        public int Kapacitás { get; private set; }
        public string Telephely { get; private set; }
        public DateTime Rögzítés { get; private set; }
        public string Rögzítő { get; private set; }


        public Adat_Akkumulátor_Napló(string beépítve, string fajta, string gyártó, string gyáriszám, string típus, DateTime garancia, DateTime gyártásiidő,
            int státus, string megjegyzés, DateTime módosításdátuma, int kapacitás, string telephely, DateTime rögzítés, string rögzítő)
        {
            Beépítve = beépítve;
            Fajta = fajta;
            Gyártó = gyártó;
            Gyáriszám = gyáriszám;
            Típus = típus;
            Garancia = garancia;
            Gyártásiidő = gyártásiidő;
            Státus = státus;
            Megjegyzés = megjegyzés;
            Módosításdátuma = módosításdátuma;
            Kapacitás = kapacitás;
            Telephely = telephely;
            Rögzítés = rögzítés;
            Rögzítő = rögzítő;
        }
    }

    public class Adat_Akkumulátor_Mérés
    {
        public string Gyáriszám { get; private set; }
        public long Kisütésiáram { get; private set; }
        public double Kezdetifesz { get; private set; }
        public double Végfesz { get; private set; }
        public DateTime Kisütésiidő { get; private set; }
        public double Kapacitás { get; private set; }
        public string Megjegyzés { get; private set; }
        public string Van { get; private set; }
        public DateTime Mérésdátuma { get; private set; }
        public DateTime Rögzítés { get; private set; }
        public string Rögzítő { get; private set; }

        public long Id { get; private set; }

        public Adat_Akkumulátor_Mérés(string gyáriszám, long kisütésiáram, double kezdetifesz, double végfesz, DateTime kisütésiidő, double kapacitás, string megjegyzés, string van, DateTime mérésdátuma, DateTime rögzítés, string rögzítő, long id)
        {
            Gyáriszám = gyáriszám;
            Kisütésiáram = kisütésiáram;
            Kezdetifesz = kezdetifesz;
            Végfesz = végfesz;
            Kisütésiidő = kisütésiidő;
            Kapacitás = kapacitás;
            Megjegyzés = megjegyzés;
            Van = van;
            Mérésdátuma = mérésdátuma;
            Rögzítés = rögzítés;
            Rögzítő = rögzítő;
            Id = id;
        }
    }


}
