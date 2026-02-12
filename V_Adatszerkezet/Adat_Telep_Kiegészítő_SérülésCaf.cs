using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Telep_Kiegészítő_SérülésCaf
    {
        public int Id { get; set; }
        public string Cég { get; set; }
        public string Név { get; set; }
        public string Beosztás { get; set; }

        public Adat_Telep_Kiegészítő_SérülésCaf(int id, string cég, string név, string beosztás)
        {
            Id = id;
            Cég = cég;
            Név = név;
            Beosztás = beosztás;
        }
    }
}
