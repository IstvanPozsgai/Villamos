using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_ICS_előterv
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
        public long V2V3Számláló { get; private set; }
        public bool Törölt { get; private set; }
        public string Honostelephely { get; private set; }
        public long Tervsorszám { get; private set; }
        public double Kerék_K1 { get; private set; }
        public double Kerék_K2 { get; private set; }
        public double Kerék_K3 { get; private set; }
        public double Kerék_K4 { get; private set; }
        public double Kerék_K5 { get; private set; }
        public double Kerék_K6 { get; private set; }
        public double Kerék_K7 { get; private set; }
        public double Kerék_K8 { get; private set; }
        public double Kerék_min { get; private set; }

        public Adat_ICS_előterv(long iD, string azonosító, long jjavszám, long kMUkm, DateTime kMUdátum, string vizsgfok, DateTime vizsgdátumk, DateTime vizsgdátumv, long vizsgkm, long havikm, long vizsgsorszám, DateTime fudátum, long teljeskm, string ciklusrend, string v2végezte, long kövV2_sorszám, string kövV2, long kövV_sorszám, string kövV, long v2V3Számláló, bool törölt, string honostelephely, long tervsorszám, double kerék_K1, double kerék_K2, double kerék_K3, double kerék_K4, double kerék_K5, double kerék_K6, double kerék_K7, double kerék_K8, double kerék_min)
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
            V2V3Számláló = v2V3Számláló;
            Törölt = törölt;
            Honostelephely = honostelephely;
            Tervsorszám = tervsorszám;
            Kerék_K1 = kerék_K1;
            Kerék_K2 = kerék_K2;
            Kerék_K3 = kerék_K3;
            Kerék_K4 = kerék_K4;
            Kerék_K5 = kerék_K5;
            Kerék_K6 = kerék_K6;
            Kerék_K7 = kerék_K7;
            Kerék_K8 = kerék_K8;
            Kerék_min = kerék_min;
        }
    }

}

