using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Behajtás_Telephelystátusz
    {
        public int ID { get; set; }
        public string Státus { get; set; }
        public int Gondnok { get; set; }
        public int Indoklás { get; set; }

        public Adat_Behajtás_Telephelystátusz(int iD, string státus, int gondnok, int indoklás)
        {
            ID = iD;
            Státus = státus;
            Gondnok = gondnok;
            Indoklás = indoklás;
        }
    }
}
