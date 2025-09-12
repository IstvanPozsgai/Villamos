namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Anyagok
    {
        public string Cikkszám { get; private set; }
        public string Megnevezés { get; private set; }
        public string KeresőFogalom { get; private set; }
        public string Sarzs { get; private set; }
        public double Ár { get; private set; }

        public Adat_Anyagok(string cikkszám, string megnevezés, string keresőFogalom, string sarzs, double ár)
        {
            Cikkszám = cikkszám;
            Megnevezés = megnevezés;
            KeresőFogalom = keresőFogalom;
            Sarzs = sarzs;
            Ár = ár;
        }
    }
}
