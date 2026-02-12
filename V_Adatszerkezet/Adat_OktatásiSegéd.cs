using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_OktatásiSegéd
    {
        public long IDoktatás { get; private set; }
        public string Telephely { get; private set; }
        public string Oktatásoka { get; private set; }
        public string Oktatástárgya { get; private set; }
        public string Oktatáshelye { get; private set; }
        public long Oktatásidőtartama { get; private set; }
        public string Oktató { get; private set; }
        public string Oktatóbeosztása { get; private set; }
        public string Egyébszöveg { get; private set; }
        public string Email { get; private set; }


        public Adat_OktatásiSegéd(long iDoktatás, string telephely, string oktatásoka, string oktatástárgya, string oktatáshelye,
            long oktatásidőtartama, string oktató, string oktatóbeosztása, string egyébszöveg, string email)
        {
            IDoktatás = iDoktatás;
            Telephely = telephely;
            Oktatásoka = oktatásoka;
            Oktatástárgya = oktatástárgya;
            Oktatáshelye = oktatáshelye;
            Oktatásidőtartama = oktatásidőtartama;
            Oktató = oktató;
            Oktatóbeosztása = oktatóbeosztása;
            Egyébszöveg = egyébszöveg;
            Email = email;
        }
    }
}
