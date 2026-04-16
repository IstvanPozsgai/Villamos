using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Épület_Takarításrakijelölt
    {
        public int E1elvégzettdb { get; private set; }
        public int E1kijelöltdb { get; private set; }
        public string E1rekijelölt { get; private set; }
        public int E2elvégzettdb { get; private set; }
        public int E2kijelöltdb { get; private set; }
        public string E2rekijelölt { get; private set; }
        public int E3elvégzettdb { get; private set; }
        public int E3kijelöltdb { get; private set; }
        public string E3rekijelölt { get; private set; }
        public string Helységkód { get; private set; }
        public int Hónap { get; private set; }
        public string Megnevezés { get; private set; }
        public string Osztály { get; private set; }

        public Adat_Épület_Takarításrakijelölt(int e1elvégzettdb, int e1kijelöltdb, string e1rekijelölt, int e2elvégzettdb, int e2kijelöltdb, string e2rekijelölt, int e3elvégzettdb, int e3kijelöltdb, string e3rekijelölt, string helységkód, int hónap, string megnevezés, string osztály)
        {
            E1elvégzettdb = e1elvégzettdb;
            E1kijelöltdb = e1kijelöltdb;
            E1rekijelölt = e1rekijelölt;
            E2elvégzettdb = e2elvégzettdb;
            E2kijelöltdb = e2kijelöltdb;
            E2rekijelölt = e2rekijelölt;
            E3elvégzettdb = e3elvégzettdb;
            E3kijelöltdb = e3kijelöltdb;
            E3rekijelölt = e3rekijelölt;
            Helységkód = helységkód;
            Hónap = hónap;
            Megnevezés = megnevezés;
            Osztály = osztály;
        }

        public Adat_Épület_Takarításrakijelölt(int e1kijelöltdb, int e2kijelöltdb, int e3kijelöltdb, string helységkód, int hónap)
        {
            E1kijelöltdb = e1kijelöltdb;
            E2kijelöltdb = e2kijelöltdb;
            E3kijelöltdb = e3kijelöltdb;
            Helységkód = helységkód;
            Hónap = hónap;
        }

        public Adat_Épület_Takarításrakijelölt(string helységkód, int hónap, int e1elvégzettdb, int e2elvégzettdb, int e3elvégzettdb)
        {
            Helységkód = helységkód;
            Hónap = hónap;
            E1elvégzettdb = e1elvégzettdb;
            E2elvégzettdb = e2elvégzettdb;
            E3elvégzettdb = e3elvégzettdb;
        }

        public Adat_Épület_Takarításrakijelölt(int e1kijelöltdb, int e2kijelöltdb, int e3kijelöltdb, string helységkód, int hónap, string e1rekijelölt, string e2rekijelölt, string e3rekijelölt)
        {
            E1kijelöltdb = e1kijelöltdb;
            E2kijelöltdb = e2kijelöltdb;
            E3kijelöltdb = e3kijelöltdb;
            Helységkód = helységkód;
            Hónap = hónap;
            E1rekijelölt = e1rekijelölt;
            E2rekijelölt = e2rekijelölt;
            E3rekijelölt = e3rekijelölt;
        }
    }
}
