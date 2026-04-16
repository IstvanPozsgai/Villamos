using System;

namespace Villamos.Adatszerkezet
{
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
