using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Behajtás_Kérelemoka
    {
        public int Id { get; set; }
        public string Ok { get; set; }

        public Adat_Behajtás_Kérelemoka(int id, string ok)
        {
            Id = id;
            Ok = ok;
        }
    }
}
