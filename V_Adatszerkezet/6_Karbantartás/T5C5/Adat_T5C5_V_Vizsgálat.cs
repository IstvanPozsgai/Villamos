using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_T5C5_V_Vizsgálat
    {
        public Adat_T5C5_V_Vizsgálat(long ssz, string psz, string típus, string vizsgfoka, long vizsgSsz, DateTime vizsgVége, long vutánfutottkorr, long havikm, string kövV, long kövVSsz, long előzőVkmkorr, string kövV2V3, long előzőV2V3tőlkmkorr, string járműstátusz, string hibaleírása, string előírtSzerelvény, string csatolhatóság, long kerékátmérőMin, long kMU, string ciklus, string vonal, string naposutolsó, long naposszám, long e3nap, long ténySzerSz, string ténySzerelvény, long előírtSzerSz, string előírtSzerelvény1, long eÍSzerhossz, long státus, string e3vezénylés, string vissza, string kiad, long korrigáltkm, long vutánfutott, long előzőVtőlkm, long előzőV2V3tőlkm, DateTime frissdátum)
        {
            Ssz = ssz;
            Psz = psz;
            Típus = típus;
            Vizsgfoka = vizsgfoka;
            VizsgSsz = vizsgSsz;
            VizsgVége = vizsgVége;
            Vutánfutottkorr = vutánfutottkorr;
            Havikm = havikm;
            KövV = kövV;
            KövVSsz = kövVSsz;
            ElőzőVkmkorr = előzőVkmkorr;
            KövV2V3 = kövV2V3;
            ElőzőV2V3tőlkmkorr = előzőV2V3tőlkmkorr;
            Járműstátusz = járműstátusz;
            Hibaleírása = hibaleírása;
            ElőírtSzerelvény = előírtSzerelvény;
            Csatolhatóság = csatolhatóság;
            KerékátmérőMin = kerékátmérőMin;
            KMU = kMU;
            Ciklus = ciklus;
            Vonal = vonal;
            Naposutolsó = naposutolsó;
            Naposszám = naposszám;
            E3nap = e3nap;
            TénySzerSz = ténySzerSz;
            TénySzerelvény = ténySzerelvény;
            ElőírtSzerSz = előírtSzerSz;
            EÍSzerhossz = eÍSzerhossz;
            Státus = státus;
            E3vezénylés = e3vezénylés;
            Vissza = vissza;
            Kiad = kiad;
            Korrigáltkm = korrigáltkm;
            Vutánfutott = vutánfutott;
            ElőzőVtőlkm = előzőVtőlkm;
            ElőzőV2V3tőlkm = előzőV2V3tőlkm;
            Frissdátum = frissdátum;
        }

        public long Ssz { get; private set; }
        public string Psz { get; private set; }
        public string Típus { get; private set; }
        public string Vizsgfoka { get; private set; } = "-";
        public long VizsgSsz { get; private set; } = 0;
        public DateTime VizsgVége { get; private set; } = new DateTime(1900, 1, 1);
        public long Vutánfutottkorr { get; private set; } = 0;
        public long Havikm { get; private set; } = 0;
        public string KövV { get; private set; } = "-";
        public long KövVSsz { get; private set; } = 0;
        public long ElőzőVkmkorr { get; private set; } = 0;
        public string KövV2V3 { get; private set; } = "-";
        public long ElőzőV2V3tőlkmkorr { get; private set; } = 0;
        public string Járműstátusz { get; private set; } = "-";
        public string Hibaleírása { get; private set; } = "-";
        public string ElőírtSzerelvény { get; private set; } = "-";
        public string Csatolhatóság { get; private set; } = "-";
        public long KerékátmérőMin { get; private set; } = 0;
        public long KMU { get; private set; } = 0;
        public string Ciklus { get; private set; } = "-";
        public string Vonal { get; private set; } = "-";
        public string Naposutolsó { get; private set; } = "-";
        public long Naposszám { get; private set; } = 0;
        public long E3nap { get; private set; } = 0;
        public long TénySzerSz { get; private set; } = 0;
        public string TénySzerelvény { get; private set; } = "-";
        public long ElőírtSzerSz { get; private set; } = 0;
        public string ElőírtSzerelvény1 { get; private set; } = "-";
        public long EÍSzerhossz { get; private set; } = 0;
        public long Státus { get; private set; } = 0;
        public string E3vezénylés { get; private set; } = "-";
        public string Vissza { get; private set; } = "_";
        public string Kiad { get; private set; } = "_";
        public long Korrigáltkm { get; private set; } = 0;
        public long Vutánfutott { get; private set; } = 0;
        public long ElőzőVtőlkm { get; private set; } = 0;
        public long ElőzőV2V3tőlkm { get; private set; } = 0;
        public DateTime Frissdátum { get; private set; } = new DateTime(1900, 1, 1);




    }
}
