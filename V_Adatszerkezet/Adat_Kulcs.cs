namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Kulcs
    {
        public string Adat1 { get; private set; }
        public string Adat2 { get; private set; }
        public string Adat3 { get; private set; }

        public Adat_Kulcs(string adat1, string adat2, string adat3)
        {
            Adat1 = adat1;
            Adat2 = adat2;
            Adat3 = adat3;
        }

        public Adat_Kulcs(string adat1, string adat2)
        {
            Adat1 = adat1;
            Adat2 = adat2;
        }
    }
}
