namespace Villamos.Adatszerkezet
{
    public class Adat_Belépés_Bejelentkezés
    {
        public long Sorszám { get; private set; }
        public string Név { get; private set; }
        public string Jelszó { get; private set; }
        public string Jogkör { get; private set; }

        public Adat_Belépés_Bejelentkezés(long sorszám, string név, string jelszó, string jogkör)
        {
            Sorszám = sorszám;
            Név = név;
            Jelszó = jelszó;
            Jogkör = jogkör;
        }
    }

}
