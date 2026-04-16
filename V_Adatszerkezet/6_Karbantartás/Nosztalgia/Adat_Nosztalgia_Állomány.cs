namespace Villamos.Adatszerkezet
{
    public class Adat_Nosztalgia_Állomány
    {
        public string Azonosító { get; private set; }
        public string Gyártó { get; private set; }
        public string Év { get; private set; }
        public string Ntípus { get; private set; }
        public string Eszközszám { get; private set; }
        public string Leltári_szám { get; private set; }

        public Adat_Nosztalgia_Állomány(string azonosító, string gyártó, string év, string ntípus, string eszközszám, string leltári_szám)
        {
            Azonosító = azonosító;
            Gyártó = gyártó;
            Év = év;
            Ntípus = ntípus;
            Eszközszám = eszközszám;
            Leltári_szám = leltári_szám;
        }
    }

}
