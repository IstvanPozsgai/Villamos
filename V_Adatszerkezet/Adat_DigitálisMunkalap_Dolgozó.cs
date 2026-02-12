using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_DigitálisMunkalap_Dolgozó
    {
        public string DolgozóNév { get; private set; }
        public string Dolgozószám { get; private set; }

        public long Fej_Id { get; private set; }
        public long Technológia_Id { get; private set; }

        public Adat_DigitálisMunkalap_Dolgozó(string dolgozóNév, string dolgozószám, long fej_Id, long technológia_Id)
        {
            DolgozóNév = dolgozóNév;
            Dolgozószám = dolgozószám;
            Fej_Id = fej_Id;
            Technológia_Id = technológia_Id;
        }
    }
}
