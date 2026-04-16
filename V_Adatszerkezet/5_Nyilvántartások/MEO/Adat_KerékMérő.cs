using System;

namespace Villamos.Villamos_Ablakok.MEO
{
    public class Adat_KerékMérő
    {
        public string Pályaszám { get; set; }
        public string Tengely { get; set; }
        public DateTime DátumIdő { get; set; }
        public double A_KKOPJ { get; set; }
        public double A_h { get; set; }
        public double A_ATM_J { get; set; }
        public double A_BETAJ { get; set; }
        public double A_NYKMJ { get; set; }
        public double A_n { get; set; }
        public double A_n2 { get; set; }
        public double A_KIFUTJ { get; set; }
        public double A_V_J { get; set; }
        public double A_a { get; set; }
        public double A_NYKVJ { get; set; }
        public double A_QR_J { get; set; }
        public double A_BKOPB { get; set; }
        public double A_KKOPB { get; set; }
        public double A_hb { get; set; }
        public double A_ATM_B { get; set; }
        public double A_BETAB { get; set; }
        public double A_NYKMB { get; set; }
        public double A_nb { get; set; }
        public double A_n2b { get; set; }
        public double A_KIFUTB { get; set; }
        public double A_V_B { get; set; }
        public double A_ab { get; set; }
        public double A_NYKVB { get; set; }
        public double A_QR_B { get; set; }
        public double A_HATL_T { get; set; }
        public double A_Vt1 { get; set; }
        public double A_Vt2 { get; set; }
        public double A_t { get; set; }
        public double A_apb_J { get; set; }
        public double A_apb_B { get; set; }
        public double A_Vt1BKV { get; set; }
        public double A_Vt2BKV { get; set; }
        public double A_ATM_K { get; set; }
        public double A_BKOPJ { get; set; }
        public double A_Rf_J { get; set; }
        public double A_Rf_B { get; set; }
        public string Hiba { get; set; }

        //CAF
        public long Km { get; set; }
        public string AGY_J { get; set; }
        public string AGY_B { get; set; }


        public Adat_KerékMérő(string pályaszám, string tengely, DateTime dátumIdő, double a_KKOPJ, double a_h, double a_ATM_J, double a_BETAJ, double a_NYKMJ, double a_n, double a_n2, double a_KIFUTJ, double a_V_J, double a_a, double a_NYKVJ, double a_QR_J, double a_BKOPB, double a_KKOPB, double a_hb, double a_ATM_B, double a_BETAB, double a_NYKMB, double a_nb, double a_n2b, double a_KIFUTB, double a_V_B, double a_ab, double a_NYKVB, double a_QR_B, double a_HATL_T, double a_Vt1, double a_Vt2, double a_t, double a_apb_J, double a_apb_B, double a_Vt1BKV, double a_Vt2BKV, double a_ATM_K, double a_BKOPJ, double a_Rf_J, double a_Rf_B, string hiba)
        {
            Pályaszám = pályaszám;
            Tengely = tengely;
            DátumIdő = dátumIdő;
            A_KKOPJ = a_KKOPJ;
            A_h = a_h;
            A_ATM_J = a_ATM_J;
            A_BETAJ = a_BETAJ;
            A_NYKMJ = a_NYKMJ;
            A_n = a_n;
            A_n2 = a_n2;
            A_KIFUTJ = a_KIFUTJ;
            A_V_J = a_V_J;
            A_a = a_a;
            A_NYKVJ = a_NYKVJ;
            A_QR_J = a_QR_J;
            A_BKOPB = a_BKOPB;
            A_KKOPB = a_KKOPB;
            A_hb = a_hb;
            A_ATM_B = a_ATM_B;
            A_BETAB = a_BETAB;
            A_NYKMB = a_NYKMB;
            A_nb = a_nb;
            A_n2b = a_n2b;
            A_KIFUTB = a_KIFUTB;
            A_V_B = a_V_B;
            A_ab = a_ab;
            A_NYKVB = a_NYKVB;
            A_QR_B = a_QR_B;
            A_HATL_T = a_HATL_T;
            A_Vt1 = a_Vt1;
            A_Vt2 = a_Vt2;
            A_t = a_t;
            A_apb_J = a_apb_J;
            A_apb_B = a_apb_B;
            A_Vt1BKV = a_Vt1BKV;
            A_Vt2BKV = a_Vt2BKV;
            A_ATM_K = a_ATM_K;
            A_BKOPJ = a_BKOPJ;
            A_Rf_J = a_Rf_J;
            A_Rf_B = a_Rf_B;
            Hiba = hiba;
        }

        public Adat_KerékMérő(string pályaszám, string tengely, DateTime dátumIdő, double a_KKOPJ, double a_h, double a_ATM_J, double a_BETAJ, double a_NYKMJ, double a_n, double a_n2, double a_KIFUTJ, double a_V_J, double a_a, double a_NYKVJ, double a_QR_J, double a_BKOPB, double a_KKOPB, double a_hb, double a_ATM_B, double a_BETAB, double a_NYKMB, double a_nb, double a_n2b, double a_KIFUTB, double a_V_B, double a_ab, double a_NYKVB, double a_QR_B, double a_HATL_T, double a_Vt1, double a_Vt2, double a_t, double a_apb_J, double a_apb_B, double a_Vt1BKV, double a_Vt2BKV, double a_ATM_K, double a_BKOPJ, double a_Rf_J, double a_Rf_B, string hiba, long km, string aGY_J, string aGY_B) : this(pályaszám, tengely, dátumIdő, a_KKOPJ, a_h, a_ATM_J, a_BETAJ, a_NYKMJ, a_n, a_n2, a_KIFUTJ, a_V_J, a_a, a_NYKVJ, a_QR_J, a_BKOPB, a_KKOPB, a_hb, a_ATM_B, a_BETAB, a_NYKMB, a_nb, a_n2b, a_KIFUTB, a_V_B, a_ab, a_NYKVB, a_QR_B, a_HATL_T, a_Vt1, a_Vt2, a_t, a_apb_J, a_apb_B, a_Vt1BKV, a_Vt2BKV, a_ATM_K, a_BKOPJ, a_Rf_J, a_Rf_B, hiba)
        {
            Km = km;
            AGY_J = aGY_J;
            AGY_B = aGY_B;
        }
    }

}
