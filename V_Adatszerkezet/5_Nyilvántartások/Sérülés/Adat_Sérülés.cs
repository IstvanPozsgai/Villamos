using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Sérülés_Tarifa
    {
        public int Id { get; set; }
        public int D60tarifa { get; set; }
        public int D03tarifa { get; set; }

        public Adat_Sérülés_Tarifa(int id, int d60tarifa, int d03tarifa)
        {
            Id = id;
            D60tarifa = d60tarifa;
            D03tarifa = d03tarifa;
        }
    }

}
