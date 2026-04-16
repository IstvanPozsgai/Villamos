using System;
using System.Collections.Generic;

namespace Villamos.Adatszerkezet
{
    public class Adat_Kiegészítő_Szolgálattelepei
    {
        public int Sorszám { get; private set; }
        public string Telephelynév { get; private set; }
        public string Szolgálatnév { get; private set; }
        public string Felelősmunkahely { get; private set; }
        public string Raktár { get; private set; }
        public Adat_Kiegészítő_Szolgálattelepei(int sorszám, string telephelynév, string szolgálatnév, string felelősmunkahely)
        {
            Sorszám = sorszám;
            Telephelynév = telephelynév;
            Szolgálatnév = szolgálatnév;
            Felelősmunkahely = felelősmunkahely;
        }

        public Adat_Kiegészítő_Szolgálattelepei(int sorszám, string telephelynév, string szolgálatnév, string felelősmunkahely, string raktár)
        {
            Sorszám = sorszám;
            Telephelynév = telephelynév;
            Szolgálatnév = szolgálatnév;
            Felelősmunkahely = felelősmunkahely;
            Raktár = raktár;
        }
    }

}

