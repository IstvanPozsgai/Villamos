namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Alap_Beolvasás
    {

        public string Csoport { get; private set; }
        public int Oszlop { get; private set; }
        public string Fejléc { get; private set; }
        public string Törölt { get; private set; }
        public long Kell { get; private set; }

        public Adat_Alap_Beolvasás(string csoport, int oszlop, string fejléc, string törölt, long kell)
        {
            Csoport = csoport;
            Oszlop = oszlop;
            Fejléc = fejléc;
            Törölt = törölt;
            Kell = kell;
        }
    }
}
