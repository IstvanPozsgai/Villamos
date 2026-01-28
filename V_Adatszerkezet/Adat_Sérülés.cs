using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Sérülés_Tarifa
    {
        public int Id { get; set; }
        public int D60tarifa { get; set; }
        public int D03tarifa { get; set; }

        public Adat_Sérülés_Tarifa(int id, int d60tarifa, int d03tarifa)
        {
            Id = id;
            D60tarifa = d60tarifa;
            D03tarifa = d03tarifa;
        }
    }

    public class Adat_Sérülés_Előkalkuláció
    {
        public int Sorszám { get; set; }
        public DateTime Dátum { get; set; }
        public string Rendszám { get; set; }
        public string Sérülésleírás { get; set; }
        public string Fénykép { get; set; }
        public int Lakidő { get; set; }
        public int Villidő { get; set; }
        public int Asztidő { get; set; }
        public int Kárpidő { get; set; }
        public string E1 { get; set; }
        public int E1idő { get; set; }
        public string E2 { get; set; }
        public int E2idő { get; set; }
        public string E3 { get; set; }
        public int E3idő { get; set; }
        public int Óradíj { get; set; }
        public string Anyagszükséglet { get; set; }
        public int Összköltség { get; set; }
        public int Szolgáltatás { get; set; }
        public int Rendelés { get; set; }
        public string Megjegyzés { get; set; }
        public int Fényidő { get; set; }
        public bool Négypéldányos { get; set; }
        public int Sorszám3 { get; set; }

        public Adat_Sérülés_Előkalkuláció(int sorszám, DateTime dátum, string rendszám, string sérülésleírás, string fénykép, int lakidő, int villidő, int asztidő, int kárpidő, string e1, int e1idő, string e2, int e2idő, string e3, int e3idő, int óradíj, string anyagszükséglet, int összköltség, int szolgáltatás, int rendelés, string megjegyzés, int fényidő, bool négypéldányos, int sorszám3)
        {
            Sorszám = sorszám;
            Dátum = dátum;
            Rendszám = rendszám;
            Sérülésleírás = sérülésleírás;
            Fénykép = fénykép;
            Lakidő = lakidő;
            Villidő = villidő;
            Asztidő = asztidő;
            Kárpidő = kárpidő;
            E1 = e1;
            E1idő = e1idő;
            E2 = e2;
            E2idő = e2idő;
            E3 = e3;
            E3idő = e3idő;
            Óradíj = óradíj;
            Anyagszükséglet = anyagszükséglet;
            Összköltség = összköltség;
            Szolgáltatás = szolgáltatás;
            Rendelés = rendelés;
            Megjegyzés = megjegyzés;
            Fényidő = fényidő;
            Négypéldányos = négypéldányos;
            Sorszám3 = sorszám3;
        }
    }

    public class Adat_Sérülés_Ideig
    {
        public int Rendelés { get; set; }
        public int Anyagköltség { get; set; }
        public int Munkaköltség { get; set; }
        public int Gépköltség { get; set; }
        public int Szolgáltatás { get; set; }
        public int Státus { get; set; }

        public Adat_Sérülés_Ideig(int rendelés, int anyagköltség, int munkaköltség, int gépköltség, int szolgáltatás, int státus)
        {
            Rendelés = rendelés;
            Anyagköltség = anyagköltség;
            Munkaköltség = munkaköltség;
            Gépköltség = gépköltség;
            Szolgáltatás = szolgáltatás;
            Státus = státus;
        }
    }

    public class Adat_Sérülés_Költség
    {
        public int Rendelés { get; set; }
        public int Anyagköltség { get; set; }
        public int Munkaköltség { get; set; }
        public int Gépköltség { get; set; }
        public int Szolgáltatás { get; set; }
        public int Státus { get; set; }

        public Adat_Sérülés_Költség(int rendelés, int anyagköltség, int munkaköltség, int gépköltség, int szolgáltatás, int státus)
        {
            Rendelés = rendelés;
            Anyagköltség = anyagköltség;
            Munkaköltség = munkaköltség;
            Gépköltség = gépköltség;
            Szolgáltatás = szolgáltatás;
            Státus = státus;
        }
    }

    public class Adat_Sérülés_Művelet
    {
        public string Teljesítményfajta { get; set; }
        public int Rendelés { get; set; }
        public string Visszaszám { get; set; }
        public string Műveletszöveg { get; set; }

        public Adat_Sérülés_Művelet(string teljesítményfajta, int rendelés, string visszaszám, string műveletszöveg)
        {
            Teljesítményfajta = teljesítményfajta;
            Rendelés = rendelés;
            Visszaszám = visszaszám;
            Műveletszöveg = műveletszöveg;
        }
    }

    public class Adat_Sérülés_Visszajelentés
    {
        public string Visszaszám { get; set; }
        public int Munkaidő { get; set; }
        public string Storno { get; set; }
        public int Rendelés { get; set; }
        public string Teljesítményfajta { get; set; }

        public Adat_Sérülés_Visszajelentés(string visszaszám, int munkaidő, string storno, int rendelés, string teljesítményfajta)
        {
            Visszaszám = visszaszám;
            Munkaidő = munkaidő;
            Storno = storno;
            Rendelés = rendelés;
            Teljesítményfajta = teljesítményfajta;
        }
    }


    public class Adat_Sérülés_Jelentés
    {
        public int Sorszám { get; set; }
        public string Telephely { get; set; }
        public DateTime Dátum { get; set; }
        public string Balesethelyszín { get; set; }
        public string Viszonylat { get; set; }
        public string Rendszám { get; set; }
        public string Járművezető { get; set; }
        public int Rendelésszám { get; set; }
        public int Kimenetel { get; set; }
        public int Státus { get; set; }
        public string Iktatószám { get; set; }
        public string Típus { get; set; }
        public string Szerelvény { get; set; }
        public int Forgalmiakadály { get; set; }
        public bool Műszaki { get; set; }
        public bool Anyagikár { get; set; }
        public string Biztosító { get; set; }
        public bool Személyisérülés { get; set; }
        public bool Személyisérülés1 { get; set; }
        public int Biztosítóidő { get; set; }
        public string Mivelütközött { get; set; }
        public int Anyagikárft { get; set; }
        public string Leírás { get; set; }
        public string Leírás1 { get; set; }
        public string Balesethelyszín1 { get; set; }
        public string Esemény { get; set; }
        public int Anyagikárft1 { get; set; }
        public int Státus1 { get; set; }
        public string Kmóraállás { get; set; }

        public Adat_Sérülés_Jelentés(int sorszám, string telephely, DateTime dátum, string balesethelyszín, string viszonylat, string rendszám, string járművezető, int rendelésszám, int kimenetel,
                int státus, string iktatószám, string típus, string szerelvény, int forgalmiakadály, bool műszaki, bool anyagikár, string biztosító, bool személyisérülés, bool személyisérülés1, int biztosítóidő,
                string mivelütközött, int anyagikárft, string leírás, string leírás1, string balesethelyszín1, string esemény, int anyagikárft1, int státus1, string kmóraállás)
        {
            Sorszám = sorszám;
            Telephely = telephely;
            Dátum = dátum;
            Balesethelyszín = balesethelyszín;
            Viszonylat = viszonylat;
            Rendszám = rendszám;
            Járművezető = járművezető;
            Rendelésszám = rendelésszám;
            Kimenetel = kimenetel;
            Státus = státus;
            Iktatószám = iktatószám;
            Típus = típus;
            Szerelvény = szerelvény;
            Forgalmiakadály = forgalmiakadály;
            Műszaki = műszaki;
            Anyagikár = anyagikár;
            Biztosító = biztosító;
            Személyisérülés = személyisérülés;
            Személyisérülés1 = személyisérülés1;
            Biztosítóidő = biztosítóidő;
            Mivelütközött = mivelütközött;
            Anyagikárft = anyagikárft;
            Leírás = leírás;
            Leírás1 = leírás1;
            Balesethelyszín1 = balesethelyszín1;
            Esemény = esemény;
            Anyagikárft1 = anyagikárft1;
            Státus1 = státus1;
            Kmóraállás = kmóraállás;
        }

        public Adat_Sérülés_Jelentés(int sorszám, int státus1)
        {
            Sorszám = sorszám;
            Státus1 = státus1;
        }
    }

    public class Adat_Sérülés_Anyag
    {
        public string Cikkszám { get; private set; }
        public string Anyagnév { get; private set; }
        public double Mennyiség { get; private set; }
        public string Me { get; private set; }
        public double Ár { get; private set; }
        public string Állapot { get; private set; }
        public double Rendelés { get; private set; }
        public string Mozgásnem { get; private set; }

        public Adat_Sérülés_Anyag(string cikkszám, string anyagnév, double mennyiség, string me, double ár, string állapot, double rendelés, string mozgásnem)
        {
            Cikkszám = cikkszám;
            Anyagnév = anyagnév;
            Mennyiség = mennyiség;
            Me = me;
            Ár = ár;
            Állapot = állapot;
            Rendelés = rendelés;
            Mozgásnem = mozgásnem;
        }
    }
}
