using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Főkönyv_SegédTábla
    {
        public long Id { get; private set; }
        public string Bejelentkezésinév { get; private set; }

        public Adat_Főkönyv_SegédTábla(long id, string bejelentkezésinév)
        {
            Id = id;
            Bejelentkezésinév = bejelentkezésinév;
        }
    }
}
