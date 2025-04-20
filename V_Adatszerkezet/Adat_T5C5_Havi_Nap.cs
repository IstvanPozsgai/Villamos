using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_T5C5_Havi_Nap
    {
        public string Azonosító { get; private set; }
        public string N1 { get; private set; }
        public string N2 { get; private set; }
        public string N3 { get; private set; }
        public string N4 { get; private set; }
        public string N5 { get; private set; }
        public string N6 { get; private set; }
        public string N7 { get; private set; }
        public string N8 { get; private set; }
        public string N9 { get; private set; }
        public string N10 { get; private set; }
        public string N11 { get; private set; }
        public string N12 { get; private set; }
        public string N13 { get; private set; }
        public string N14 { get; private set; }
        public string N15 { get; private set; }
        public string N16 { get; private set; }
        public string N17 { get; private set; }
        public string N18 { get; private set; }
        public string N19 { get; private set; }
        public string N20 { get; private set; }
        public string N21 { get; private set; }
        public string N22 { get; private set; }
        public string N23 { get; private set; }
        public string N24 { get; private set; }
        public string N25 { get; private set; }
        public string N26 { get; private set; }
        public string N27 { get; private set; }
        public string N28 { get; private set; }
        public string N29 { get; private set; }
        public string N30 { get; private set; }
        public string N31 { get; private set; }
        public int Futásnap { get; private set; }
        public string Telephely { get; private set; }

        public Adat_T5C5_Havi_Nap(string azonosító, string n1, string n2, string n3, string n4, string n5, string n6, string n7, string n8, string n9, string n10, string n11, string n12, string n13, string n14, string n15, string n16, string n17, string n18, string n19, string n20, string n21, string n22, string n23, string n24, string n25, string n26, string n27, string n28, string n29, string n30, string n31, int futásnap, string telephely)
        {
            Azonosító = azonosító;
            N1 = n1;
            N2 = n2;
            N3 = n3;
            N4 = n4;
            N5 = n5;
            N6 = n6;
            N7 = n7;
            N8 = n8;
            N9 = n9;
            N10 = n10;
            N11 = n11;
            N12 = n12;
            N13 = n13;
            N14 = n14;
            N15 = n15;
            N16 = n16;
            N17 = n17;
            N18 = n18;
            N19 = n19;
            N20 = n20;
            N21 = n21;
            N22 = n22;
            N23 = n23;
            N24 = n24;
            N25 = n25;
            N26 = n26;
            N27 = n27;
            N28 = n28;
            N29 = n29;
            N30 = n30;
            N31 = n31;
            Futásnap = futásnap;
            Telephely = telephely;
        }

        public Adat_T5C5_Havi_Nap(string azonosító, string n1, int futásnap, string telephely)
        {
            Azonosító = azonosító;
            N1 = n1;
            Futásnap = futásnap;
            Telephely = telephely;
        }
    }

    public class Adat_T5C5_Futás
    {
        public string Azonosító { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Futásstátus { get; private set; }
        public long Státus { get; private set; }

        public Adat_T5C5_Futás(string azonosító, DateTime dátum, string futásstátus, long státus)
        {
            Azonosító = azonosító;
            Dátum = dátum;
            Futásstátus = futásstátus;
            Státus = státus;
        }
    }

    public class Adat_T5C5_Futás1
    {
        public long Státus { get; private set; }

        public Adat_T5C5_Futás1(long státus)
        {
            Státus = státus;
        }
    }

    public class Adat_T5C5_Göngyöl
    {
        public string Azonosító { get; private set; }
        public DateTime Utolsórögzítés { get; private set; }
        public DateTime Vizsgálatdátuma { get; private set; }
        public DateTime Utolsóforgalminap { get; private set; }
        public string Vizsgálatfokozata { get; private set; }
        public int Vizsgálatszáma { get; private set; }
        public int Futásnap { get; private set; }
        public string Telephely { get; private set; }

        public Adat_T5C5_Göngyöl(string azonosító, DateTime utolsórögzítés, DateTime vizsgálatdátuma, DateTime utolsóforgalminap, string vizsgálatfokozata, int vizsgálatszáma, int futásnap, string telephely)
        {
            Azonosító = azonosító;
            Utolsórögzítés = utolsórögzítés;
            Vizsgálatdátuma = vizsgálatdátuma;
            Utolsóforgalminap = utolsóforgalminap;
            Vizsgálatfokozata = vizsgálatfokozata;
            Vizsgálatszáma = vizsgálatszáma;
            Futásnap = futásnap;
            Telephely = telephely;
        }
    }


    public class Adat_T5C5_Kmadatok
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

        public Adat_T5C5_Kmadatok(long iD, string azonosító, long jjavszám, long kMUkm, DateTime kMUdátum, string vizsgfok, DateTime vizsgdátumk, DateTime vizsgdátumv, long vizsgkm, long havikm, long vizsgsorszám, DateTime fudátum, long teljeskm, string ciklusrend, string v2végezte, long kövV2_sorszám, string kövV2, long kövV_sorszám, string kövV, bool törölt, long v2V3Számláló)
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


        public Adat_T5C5_Kmadatok(long iD, string azonosító, DateTime vizsgdátumk)
        {
            ID = iD;
            Azonosító = azonosító;
            Vizsgdátumk = vizsgdátumk;
        }
    }

    public class Adat_T5C5_Kmadatok_Napló
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

        public string Módosító { get; private set; }
        public DateTime Mikor { get; private set; }

        public Adat_T5C5_Kmadatok_Napló(long iD, string azonosító, long jjavszám, long kMUkm, DateTime kMUdátum, string vizsgfok, DateTime vizsgdátumk, DateTime vizsgdátumv, long vizsgkm, long havikm, long vizsgsorszám, DateTime fudátum, long teljeskm, string ciklusrend, string v2végezte, long kövV2_sorszám, string kövV2, long kövV_sorszám, string kövV, bool törölt, long v2V3Számláló, string módosító, DateTime mikor)
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
            Módosító = módosító;
            Mikor = mikor;
        }
    }
    public class Adat_T5C5_Előterv
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

        public string Honostelephely { get; private set; }
        public long Tervsorszám { get; private set; }
        public double Kerék_K11 { get; private set; }
        public double Kerék_K12 { get; private set; }
        public double Kerék_K21 { get; private set; }
        public double Kerék_K22 { get; private set; }
        public double Kerék_min { get; private set; }
        public long V2V3Számláló { get; private set; }

        public Adat_T5C5_Előterv(long iD, string azonosító, long jjavszám, long kMUkm, DateTime kMUdátum, string vizsgfok, DateTime vizsgdátumk, DateTime vizsgdátumv, long vizsgkm, long havikm, long vizsgsorszám, DateTime fudátum, long teljeskm, string ciklusrend, string v2végezte, long kövV2_sorszám, string kövV2, long kövV_sorszám, string kövV, bool törölt, string honostelephely, long tervsorszám, double kerék_K11, double kerék_K12, double kerék_K21, double kerék_K22, double kerék_min, long v2V3Számláló)
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
            Honostelephely = honostelephely;
            Tervsorszám = tervsorszám;
            Kerék_K11 = kerék_K11;
            Kerék_K12 = kerék_K12;
            Kerék_K21 = kerék_K21;
            Kerék_K22 = kerék_K22;
            Kerék_min = kerék_min;
            V2V3Számláló = v2V3Számláló;
        }
    }


    public class Adat_T5C5_Posta
    {
        public string Azonosító { get; private set; }
        public string Típus { get; private set; }
        public string Csatolható { get; private set; }
        public int V_Sorszám { get; private set; }
        public string V2_következő { get; private set; }
        public int V2_Futott_Km { get; private set; }
        public string V_Következő { get; private set; }
        public int V_futott_Km { get; private set; }
        public int Napszám { get; private set; }
        public string Terv_Nap { get; private set; }
        public string Hiba { get; private set; }
        public string Előírt_szerelvény { get; private set; }
        public string Tényleges_szerelvény { get; private set; }
        public string Rendelésszám { get; private set; }
        public long Szerelvényszám { get; private set; }

        public int Státus { get; private set; }
        public int E3_sorszám { get; private set; }
        public int Vizsgál { get; private set; }
        public int Marad { get; private set; }

        public string Kiad { get; private set; }
        public string Vissza { get; private set; }
        public string Vonal { get; private set; }


        public Adat_T5C5_Posta(string azonosító, string típus, string csatolható, int v_Sorszám, string v2_következő, int v2_Futott_Km,
            string v_Következő, int v_futott_Km, int napszám, string terv_Nap, string hiba, string előírt_szerelvény, string tényleges_szerelvény,
            string rendelésszám, long szerelvényszám, int státus, int e3_sorszám, int vizsgál, int marad, string kiad, string vissza, string vonal, bool terv)
        {
            Azonosító = azonosító;
            Típus = típus;
            Csatolható = csatolható;
            V_Sorszám = v_Sorszám;
            V2_következő = v2_következő;
            V2_Futott_Km = v2_Futott_Km;
            V_Következő = v_Következő;
            V_futott_Km = v_futott_Km;
            Napszám = napszám;
            Terv_Nap = terv_Nap;
            Hiba = hiba;
            Előírt_szerelvény = előírt_szerelvény;
            Tényleges_szerelvény = tényleges_szerelvény;
            Rendelésszám = rendelésszám;
            Szerelvényszám = szerelvényszám;
            Státus = státus;
            E3_sorszám = e3_sorszám;
            Vizsgál = vizsgál;
            Marad = marad;
            Kiad = kiad;
            Vissza = vissza;
            Vonal = vonal;

        }
    }

    public class Adat_BEOLVAS_KM
    {
        public string Azonosító { get; set; }
        public long Havikm { get; set; }
        public DateTime KMUdátum { get; set; }
        public long KMUkm { get; set; }
        public long Teljeskm { get; set; }
        public long Jjavszám { get; set; }
        public DateTime Fudátum { get; set; }

        public Adat_BEOLVAS_KM(string azonosító, long havikm, DateTime kMUdátum, long kMUkm, long teljeskm, long jjavszám, DateTime fudátum)
        {
            Azonosító = azonosító;
            Havikm = havikm;
            KMUdátum = kMUdátum;
            KMUkm = kMUkm;
            Teljeskm = teljeskm;
            Jjavszám = jjavszám;
            Fudátum = fudátum;
        }
    }

    public class Adat_T5C5_Göngyöl_DátumTábla
    {

        public string Telephely { get; private set; }
        public DateTime Utolsórögzítés { get; set; }

        public bool Zárol { get; private set; }

        public Adat_T5C5_Göngyöl_DátumTábla(string telephely, DateTime utolsórögzítés, bool zárol)
        {
            Telephely = telephely;
            Utolsórögzítés = utolsórögzítés;
            Zárol = zárol;
        }
    }

}
