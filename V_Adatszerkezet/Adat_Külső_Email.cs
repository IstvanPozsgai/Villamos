using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Külső_Email
    {
        public double Id { get; private set; }
        public string Másolat { get; private set; }
        public string Aláírás { get; private set; }

        public Adat_Külső_Email(double id, string másolat, string aláírás)
        {
            Id = id;
            Másolat = másolat;
            Aláírás = aláírás;
        }
    }
}
