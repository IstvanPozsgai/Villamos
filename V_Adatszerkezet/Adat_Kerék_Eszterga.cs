using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Kerék_Eszterga_Igény
    {
        public string Pályaszám { get; private set; }
        public string Megjegyzés { get; private set; }
        public DateTime Rögzítés_dátum { get; private set; }
        public string Igényelte { get; private set; }
        public int Tengelyszám { get; private set; }
        public int Szerelvény { get; private set; }
        public int Prioritás { get; private set; }
        public DateTime Ütemezés_dátum { get; private set; }
        public int Státus { get; private set; }
        public string Telephely { get; private set; }

        public string Típus { get; private set; }

        public int Norma { get; private set; }

        public Adat_Kerék_Eszterga_Igény(string pályaszám, string megjegyzés, DateTime rögzítés_dátum, string igényelte, int tengelyszám, int szerelvény, int prioritás, DateTime ütemezés_dátum, int státus, string telephely, string típus, int norma)
        {
            Pályaszám = pályaszám;
            Megjegyzés = megjegyzés;
            Rögzítés_dátum = rögzítés_dátum;
            Igényelte = igényelte;
            Tengelyszám = tengelyszám;
            Szerelvény = szerelvény;
            Prioritás = prioritás;
            Ütemezés_dátum = ütemezés_dátum;
            Státus = státus;
            Telephely = telephely;
            Típus = típus;
            Norma = norma;
        }

        /// <summary>
        ///   Módosításhoz konstruktor, csak a pályaszám, ütemezés dátum és telephely szükséges.
        /// </summary>
        /// <param name="pályaszám"></param>
        /// <param name="ütemezés_dátum"></param>
        /// <param name="státus"></param>
        /// <param name="telephely"></param>
        public Adat_Kerék_Eszterga_Igény(string pályaszám, DateTime ütemezés_dátum, int státus, string telephely)
        {
            Pályaszám = pályaszám;
            Ütemezés_dátum = ütemezés_dátum;
            Státus = státus;
            Telephely = telephely;
        }
    }

    public class Adat_Kerék_Eszterga_Esztergályos
    {
        public string Dolgozószám { get; private set; }
        public string Dolgozónév { get; private set; }

        public string Telephely { get; private set; }
        public int Státus { get; private set; }

        public Adat_Kerék_Eszterga_Esztergályos(string dolgozószám, string dolgozónév, string telephely, int státus)
        {
            Dolgozószám = dolgozószám;
            Dolgozónév = dolgozónév;
            Telephely = telephely;
            Státus = státus;
        }
    }

    public class Adat_Kerék_Eszterga_Naptár
    {
        public DateTime Idő { get; private set; }
        public bool Munkaidő { get; private set; }
        public bool Foglalt { get; private set; }
        public string Pályaszám { get; private set; }
        public string Megjegyzés { get; private set; }
        public long BetűSzín { get; private set; }
        public long HáttérSzín { get; private set; }

        public bool Marad { get; set; }

        public Adat_Kerék_Eszterga_Naptár(DateTime idő, bool munkaidő, bool foglalt, string pályaszám, string megjegyzés, long betűSzín, long háttérSzín, bool marad)
        {
            Idő = idő;
            Munkaidő = munkaidő;
            Foglalt = foglalt;
            Pályaszám = pályaszám;
            Megjegyzés = megjegyzés;
            BetűSzín = betűSzín;
            HáttérSzín = háttérSzín;
            Marad = marad;
        }

        public Adat_Kerék_Eszterga_Naptár(DateTime idő, bool foglalt, string pályaszám, string megjegyzés, long betűSzín, long háttérSzín, bool marad)
        {
            Idő = idő;
            Foglalt = foglalt;
            Pályaszám = pályaszám;
            Megjegyzés = megjegyzés;
            BetűSzín = betűSzín;
            HáttérSzín = háttérSzín;
            Marad = marad;
        }

        public Adat_Kerék_Eszterga_Naptár(DateTime idő)
        {
            Idő = idő;
        }

    }


    public class Adat_Kerék_Eszterga_Tevékenység
    {
        public string Tevékenység { get; private set; }
        public double Munkaidő { get; private set; }
        public long Betűszín { get; private set; }
        public long Háttérszín { get; private set; }
        public int Id { get; private set; }

        public bool Marad { get; private set; }

        public Adat_Kerék_Eszterga_Tevékenység(string tevékenység, double munkaidő, long betűszín, long háttérszín, int id, bool marad)
        {
            Tevékenység = tevékenység;
            Munkaidő = munkaidő;
            Betűszín = betűszín;
            Háttérszín = háttérszín;
            Id = id;
            Marad = marad;
        }
    }

    public class Adat_Kerék_Eszterga_Tengely
    {
        public string Típus { get; private set; }

        public int Munkaidő { get; private set; }

        public int Állapot { get; private set; }

        public Adat_Kerék_Eszterga_Tengely(string típus, int munkaidő, int állapot)
        {
            Típus = típus;
            Munkaidő = munkaidő;
            Állapot = állapot;
        }
    }


    public class Adat_Kerék_Eszterga_Igény_Napló
    {
        public string Pályaszám { get; private set; }
        public string Megjegyzés { get; private set; }
        public DateTime Rögzítés_dátum { get; private set; }
        public string Igényelte { get; private set; }
        public int Tengelyszám { get; private set; }
        public int Szerelvény { get; private set; }
        public int Prioritás { get; private set; }
        public DateTime Ütemezés_dátum { get; private set; }
        public int Státus { get; private set; }
        public string Telephely { get; private set; }
        public string Típus { get; private set; }
        public string Ki { get; private set; }

        public DateTime Mikor { get; private set; }

        public Adat_Kerék_Eszterga_Igény_Napló(string pályaszám, string megjegyzés, DateTime rögzítés_dátum, string igényelte, int tengelyszám, int szerelvény, int prioritás, DateTime ütemezés_dátum, int státus, string telephely, string típus, string ki, DateTime mikor)
        {
            Pályaszám = pályaszám;
            Megjegyzés = megjegyzés;
            Rögzítés_dátum = rögzítés_dátum;
            Igényelte = igényelte;
            Tengelyszám = tengelyszám;
            Szerelvény = szerelvény;
            Prioritás = prioritás;
            Ütemezés_dátum = ütemezés_dátum;
            Státus = státus;
            Telephely = telephely;
            Típus = típus;
            Ki = ki;
            Mikor = mikor;
        }
    }


    public class Adat_Kerék_Eszterga_Terjesztés
    {
        public string Név { get; private set; }
        public string Email { get; private set; }
        public string Telephely { get; private set; }
        public int Változat { get; private set; }

        public Adat_Kerék_Eszterga_Terjesztés(string név, string email, string telephely, int változat)
        {
            Név = név;
            Email = email;
            Telephely = telephely;
            Változat = változat;
        }
    }

    public class Adat_Kerék_Eszterga_Automata
    {
        public string FelhasználóiNév { get; private set; }
        public DateTime UtolsóÜzenet { get; private set; }

        public Adat_Kerék_Eszterga_Automata(string felhasználóiNév, DateTime utolsóÜzenet)
        {
            FelhasználóiNév = felhasználóiNév;
            UtolsóÜzenet = utolsóÜzenet;
        }
    }



}
