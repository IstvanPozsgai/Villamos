using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Takarítás_Opció
    {
        public int Id { get; private set; }
        public string Megnevezés { get; private set; }
        public string Mennyisége { get; private set; }
        public double Ár { get; private set; }
        public DateTime Kezdet { get; private set; }
        public DateTime Vég { get; private set; }

        public Adat_Takarítás_Opció(int id, string megnevezés, string mennyisége, double ár, DateTime kezdet, DateTime vég)
        {
            Id = id;
            Megnevezés = megnevezés;
            Mennyisége = mennyisége;
            Ár = ár;
            Kezdet = kezdet;
            Vég = vég;
        }

        public Adat_Takarítás_Opció(int id, DateTime vég)
        {
            Id = id;
            Vég = vég;
        }
    }

    public class Adat_Takarítás_Telep_Opció
    {
        public int Id { get; private set; }
        public DateTime Dátum { get; private set; }
        public double Megrendelt { get; private set; }
        public double Teljesített { get; private set; }

        public Adat_Takarítás_Telep_Opció(int id, DateTime dátum, double megrendelt, double teljesített)
        {
            Id = id;
            Dátum = dátum;
            Megrendelt = megrendelt;
            Teljesített = teljesített;
        }
    }
}
