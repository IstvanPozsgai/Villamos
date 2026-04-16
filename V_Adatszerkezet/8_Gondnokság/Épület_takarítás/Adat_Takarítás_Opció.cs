using System;

namespace Villamos.Adatszerkezet
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

}
