using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Fogas_Km
    {
        public long ID { get; private set; }
        public string Azonosító { get; private set; }
        public long Jjavszám { get; private set; }
        public long KMUkm { get; private set; }
        public DateTime KMUdátum { get; private set; }
        public string Vizsgfok { get; private set; }
        public DateTime Vizsgdátumk { get; private set; }
        public DateTime Vizsgdátumv { get; private set; }
        public long Vizsgkm { get; private set; }
        public long Havikm { get; private set; }
        public long Vizsgsorszám { get; private set; }
        public DateTime Fudátum { get; private set; }
        public long Teljeskm { get; private set; }
        public string Ciklusrend { get; private set; }
        public string V2végezte { get; private set; }
        public long KövV2_sorszám { get; private set; }
        public string KövV2 { get; private set; }
        public long KövV_sorszám { get; private set; }
        public string KövV { get; private set; }
        public bool Törölt { get; private set; }
        public long V2V3Számláló { get; private set; }

        public Adat_Fogas_Km(long iD, string azonosító, long jjavszám, long kMUkm, DateTime kMUdátum, string vizsgfok, DateTime vizsgdátumk, DateTime vizsgdátumv, long vizsgkm, long havikm, long vizsgsorszám, DateTime fudátum, long teljeskm, string ciklusrend, string v2végezte, long kövV2_sorszám, string kövV2, long kövV_sorszám, string kövV, bool törölt, long v2V3Számláló)
        {
            ID = iD;
            Azonosító = azonosító;
            Jjavszám = jjavszám;
            KMUkm = kMUkm;
            KMUdátum = kMUdátum;
            Vizsgfok = vizsgfok;
            Vizsgdátumk = vizsgdátumk;
            Vizsgdátumv = vizsgdátumv;
            Vizsgkm = vizsgkm;
            Havikm = havikm;
            Vizsgsorszám = vizsgsorszám;
            Fudátum = fudátum;
            Teljeskm = teljeskm;
            Ciklusrend = ciklusrend;
            V2végezte = v2végezte;
            KövV2_sorszám = kövV2_sorszám;
            KövV2 = kövV2;
            KövV_sorszám = kövV_sorszám;
            KövV = kövV;
            Törölt = törölt;
            V2V3Számláló = v2V3Számláló;
        }
    }
}
