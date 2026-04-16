using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Beosztásciklus
    {
        public int Id { get; private set; }
        public string Beosztáskód { get; private set; }
        public string Hétnapja { get; private set; }
        public string Beosztásszöveg { get; private set; }

        public Adat_Kiegészítő_Beosztásciklus(int id, string beosztáskód, string hétnapja, string beosztásszöveg)
        {
            Id = id;
            Beosztáskód = beosztáskód;
            Hétnapja = hétnapja;
            Beosztásszöveg = beosztásszöveg;
        }
    }
}
