using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kidobó_Változat
    {
        public long Id { get; private set; }
        public string Változatnév { get; private set; }

        public Adat_Kidobó_Változat(long id, string változatnév)
        {
            Id = id;
            Változatnév = változatnév;
        }
    }
}
