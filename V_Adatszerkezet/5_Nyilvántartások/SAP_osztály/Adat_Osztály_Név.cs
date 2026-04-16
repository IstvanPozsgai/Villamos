using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Osztály_Név
    {
        public int Id { get; private set; }
        public string Osztálynév { get; private set; }
        public string Osztálymező { get; private set; }
        public bool Használatban { get; private set; }

        public Adat_Osztály_Név(int id, string osztálynév, string osztálymező, bool használatban)
        {
            Id = id;
            Osztálynév = osztálynév;
            Osztálymező = osztálymező;
            Használatban = használatban;
        }
    }
}
