﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_JogosítványTípus
    {
        public int Sorszám { get; private set; }
        public string Törzsszám { get; private set; }
        public string Jogtípus { get; private set; }
        public DateTime Jogtípusérvényes { get; private set; }
        public DateTime Jogtípusmegszerzés { get; private set; }
        public bool Státus { get; private set; }

        public Adat_JogosítványTípus(int sorszám, string törzsszám, string jogtípus, DateTime jogtípusérvényes, DateTime jogtípusmegszerzés, bool státus)
        {
            Sorszám = sorszám;
            Törzsszám = törzsszám;
            Jogtípus = jogtípus;
            Jogtípusérvényes = jogtípusérvényes;
            Jogtípusmegszerzés = jogtípusmegszerzés;
            Státus = státus;
        }
    }

    public class Adat_JogosítványVonal
    {
        public int Sorszám { get; private set; }
        public string Törzsszám { get; private set; }
        public DateTime Jogvonalérv { get; private set; }
        public DateTime  Jogvonalmegszerzés { get; private set; }
        public string Vonalmegnevezés { get; private set; }
        public string Vonalszám { get; private set; }
        public bool Státus { get; private set; }

        public Adat_JogosítványVonal(int sorszám, string törzsszám, DateTime jogvonalérv, DateTime jogvonalmegszerzés, string vonalmegnevezés, string vonalszám, bool státus)
        {
            Sorszám = sorszám;
            Törzsszám = törzsszám;
            Jogvonalérv = jogvonalérv;
            Jogvonalmegszerzés = jogvonalmegszerzés;
            Vonalmegnevezés = vonalmegnevezés;
            Vonalszám = vonalszám;
            Státus = státus;
        }
    }
}
