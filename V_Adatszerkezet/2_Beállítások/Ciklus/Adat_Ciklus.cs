namespace Villamos.Adatszerkezet
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
}
