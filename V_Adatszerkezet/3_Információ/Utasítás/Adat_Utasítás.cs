using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Utasítás
    {
        public double Sorszám { get; private set; }
        public string Szöveg { get; private set; }
        public string Írta { get; private set; }
        public DateTime Mikor { get; private set; }
        public double Érvényes { get; private set; }

        //Lekérdezés
        public Adat_Utasítás(double sorszám, string szöveg, string írta, DateTime mikor, double érvényes)
        {
            Sorszám = sorszám;
            Szöveg = szöveg;
            Írta = írta;
            Mikor = mikor;
            Érvényes = érvényes;
        }

        public Adat_Utasítás(double sorszám, string szöveg, double érvényes)
        {
            Sorszám = sorszám;
            Szöveg = szöveg;
            Érvényes = érvényes;
        }
    }



}
