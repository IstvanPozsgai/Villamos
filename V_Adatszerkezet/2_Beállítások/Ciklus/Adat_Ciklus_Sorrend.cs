namespace Villamos.Adatszerkezet
{
    public class Adat_Ciklus_Sorrend
    {
        public int Sorszám { get; private set; }
        public string JárműTípus { get; private set; }
        public string CiklusNév { get; private set; }

        public Adat_Ciklus_Sorrend(int sorszám, string járműTípus, string ciklusNév)
        {
            Sorszám = sorszám;
            JárműTípus = járműTípus;
            CiklusNév = ciklusNév;
        }
    }
}
