using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Jármű_Takarítás_Mátrix
    {

        public int Id { get; private set; }
        public string Fajta { get; private set; }
        public string Fajtamásik { get; private set; }

        public bool Igazság { get; private set; }
        public Adat_Jármű_Takarítás_Mátrix(int id, string fajta, string fajtamásik, bool igazság)
        {
            Id = id;
            Fajta = fajta;
            Fajtamásik = fajtamásik;
            Igazság = igazság;

        }
    }
}
