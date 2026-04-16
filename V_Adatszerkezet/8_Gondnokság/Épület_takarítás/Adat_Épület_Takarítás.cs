namespace Villamos.Adatszerkezet
{
    public class Adat_Épület_Takarítás_Osztály
    {
        public int Id { get; private set; }
        public string Osztály { get; private set; }
        public double E1Ft { get; private set; }
        public double E2Ft { get; private set; }
        public double E3Ft { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Épület_Takarítás_Osztály(int id, string osztály, double e1Ft, double e2Ft, double e3Ft, bool státus)
        {
            Id = id;
            Osztály = osztály;
            E1Ft = e1Ft;
            E2Ft = e2Ft;
            E3Ft = e3Ft;
            Státus = státus;
        }
    }
}
