namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Excel_Beolvasó
    {
        public string Csoport { get; private set; }
        public int Oszlop { get; private set; }
        public string Fejléc { get; private set; }
        public string  Törölt { get; private set; }
        public int Kell { get; private set; }

        public Adat_Excel_Beolvasó(string csoport, int oszlop, string fejléc, string törölt, int kell)
        {
            Csoport = csoport;
            Oszlop = oszlop;
            Fejléc = fejléc;
            Törölt = törölt;
            Kell = kell;
        }
    }
}
