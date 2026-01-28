namespace Villamos.Adatszerkezet
{
    public class Adat_Raktár
    {
        public string Cikkszám { get; private set; }
        public string Sarzs { get; private set; }
        public string Raktárhely { get; private set; }
        public double Mennyiség { get; private set; }

        public Adat_Raktár(string cikkszám, string sarzs, string raktárhely, double mennyiség)
        {
            Cikkszám = cikkszám;
            Sarzs = sarzs;
            Raktárhely = raktárhely;
            Mennyiség = mennyiség;
        }
    }
}
