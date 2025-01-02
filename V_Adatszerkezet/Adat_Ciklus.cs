using System.Collections.Generic;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Ciklus
    {
        public string Típus { get; private set; }
        public long Sorszám { get; private set; }
        public string Vizsgálatfok { get; private set; }
        public string Törölt { get; private set; }
        public long Névleges { get; private set; }
        public long Alsóérték { get; private set; }
        public long Felsőérték { get; private set; }

        public Adat_Ciklus(string típus, long sorszám, string vizsgálatfok, string törölt, long névleges, long alsóérték, long felsőérték)
        {
            Típus = típus;
            Sorszám = sorszám;
            Vizsgálatfok = vizsgálatfok;
            Törölt = törölt;
            Névleges = névleges;
            Alsóérték = alsóérték;
            Felsőérték = felsőérték;
        }
    }

    public class ÖHasonlít_Adat_Ciklus_Típus : IEqualityComparer<Adat_Ciklus>
    {
        public bool Equals(Adat_Ciklus X, Adat_Ciklus Y)
        {
            return X.Típus.Equals(Y.Típus);
        }

        public int GetHashCode(Adat_Ciklus obj)
        {
            return obj.Típus.GetHashCode();
        }
    }
}
