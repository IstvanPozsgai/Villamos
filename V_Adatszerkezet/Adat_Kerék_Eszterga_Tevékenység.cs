using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kerék_Eszterga_Tevékenység
    {
        public string Tevékenység { get; private set; }
        public double Munkaidő { get; private set; }
        public long Betűszín { get; private set; }
        public long Háttérszín { get; private set; }
        public int Id { get; private set; }

        public bool Marad { get; private set; }

        public Adat_Kerék_Eszterga_Tevékenység(string tevékenység, double munkaidő, long betűszín, long háttérszín, int id, bool marad)
        {
            Tevékenység = tevékenység;
            Munkaidő = munkaidő;
            Betűszín = betűszín;
            Háttérszín = háttérszín;
            Id = id;
            Marad = marad;
        }
    }
}
