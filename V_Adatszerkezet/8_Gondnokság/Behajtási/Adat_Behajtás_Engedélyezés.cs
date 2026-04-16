using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Behajtás_Engedélyezés
    {
        public int Id { get; set; }
        public string Telephely { get; set; }
        public string Emailcím { get; set; }
        public Boolean Gondnok { get; set; }
        public Boolean Szakszolgálat { get; set; }
        public string Telefonszám { get; set; }
        public string Szakszolgálatszöveg { get; set; }
        public string Beosztás { get; set; }
        public string Név { get; set; }

        public Adat_Behajtás_Engedélyezés(int id, string telephely, string emailcím, bool gondnok, bool szakszolgálat, string telefonszám, string szakszolgálatszöveg, string beosztás, string név)
        {
            Id = id;
            Telephely = telephely;
            Emailcím = emailcím;
            Gondnok = gondnok;
            Szakszolgálat = szakszolgálat;
            Telefonszám = telefonszám;
            Szakszolgálatszöveg = szakszolgálatszöveg;
            Beosztás = beosztás;
            Név = név;
        }
    }
}
