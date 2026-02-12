using System;

namespace Villamos.Adatszerkezet
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

}
