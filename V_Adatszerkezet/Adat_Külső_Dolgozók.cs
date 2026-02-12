using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Külső_Dolgozók
    {
        public double Id { get; private set; }
        public string Név { get; private set; }
        public string Okmányszám { get; private set; }
        public string Anyjaneve { get; private set; }
        public string Születésihely { get; private set; }
        public DateTime Születésiidő { get; private set; }
        public double Cégid { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Külső_Dolgozók(double id, string név, string okmányszám, string anyjaneve, string születésihely, DateTime születésiidő, double cégid, bool státus)
        {
            Id = id;
            Név = név;
            Okmányszám = okmányszám;
            Anyjaneve = anyjaneve;
            Születésihely = születésihely;
            Születésiidő = születésiidő;
            Cégid = cégid;
            Státus = státus;
        }

        public Adat_Külső_Dolgozók(double id, string név, string okmányszám, double cégid, bool státus)
        {
            Id = id;
            Név = név;
            Okmányszám = okmányszám;
            Cégid = cégid;
            Státus = státus;
        }

        public Adat_Külső_Dolgozók(string név, string okmányszám, double cégid, bool státus)
        {
            Név = név;
            Okmányszám = okmányszám;
            Cégid = cégid;
            Státus = státus;
        }

        public Adat_Külső_Dolgozók(double id, bool státus)
        {
            Id = id;
            Státus = státus;
        }
    }
}
