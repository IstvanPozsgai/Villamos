namespace Villamos.Adatszerkezet
{
    public class Adat_Dolgozó
    {
        public string Dolgozószám { get; private set; }
        public string Dolgozónév { get; private set; }
        public string Munkakör { get; private set; }
        public string Szervezet { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Dolgozó(string dolgozószám, string dolgozónév, string munkakör, string szervezet, bool státus)
        {
            Dolgozószám = dolgozószám;
            Dolgozónév = dolgozónév;
            Munkakör = munkakör;
            Szervezet = szervezet;
            Státus = státus;
        }
    }
}
