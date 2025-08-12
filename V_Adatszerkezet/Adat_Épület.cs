namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Épület_Adattábla
    {
        public int ID { get; private set; }
        public string Megnevezés { get; private set; }
        public string Osztály { get; private set; }
        public double Méret { get; private set; }
        public string Helységkód { get; private set; }
        public bool Státus { get; private set; }
        public int E1évdb { get; private set; }
        public int E2évdb { get; private set; }
        public int E3évdb { get; private set; }
        public string Kezd { get; private set; }
        public string Végez { get; private set; }
        public string Ellenőremail { get; private set; }
        public string Ellenőrneve { get; private set; }
        public string Ellenőrtelefonszám { get; private set; }
        public bool Szemetes { get; private set; }
        public string Kapcsolthelység { get; private set; }

        public Adat_Épület_Adattábla(int iD, string megnevezés, string osztály, double méret, string helységkód, bool státus, int e1évdb, int e2évdb, int e3évdb, string kezd, string végez, string ellenőremail, string ellenőrneve, string ellenőrtelefonszám, bool szemetes, string kapcsolthelység)
        {
            ID = iD;
            Megnevezés = megnevezés;
            Osztály = osztály;
            Méret = méret;
            Helységkód = helységkód;
            Státus = státus;
            E1évdb = e1évdb;
            E2évdb = e2évdb;
            E3évdb = e3évdb;
            Kezd = kezd;
            Végez = végez;
            Ellenőremail = ellenőremail;
            Ellenőrneve = ellenőrneve;
            Ellenőrtelefonszám = ellenőrtelefonszám;
            Szemetes = szemetes;
            Kapcsolthelység = kapcsolthelység;
        }
    }

    public class Adat_Épület_Takarításosztály
    {
        public int ID { get; private set; }
        public string Osztály { get; private set; }
        public double E1Ft { get; private set; }
        public double E2Ft { get; private set; }
        public double E3Ft { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Épület_Takarításosztály(int iD, string osztály, double e1Ft, double e2Ft, double e3Ft, bool státus)
        {
            ID = iD;
            Osztály = osztály;
            E1Ft = e1Ft;
            E2Ft = e2Ft;
            E3Ft = e3Ft;
            Státus = státus;
        }
    }

    public class Adat_Épület_Naptár
    {
        public bool Előterv { get; private set; }
        public int Hónap { get; private set; }
        public bool Igazolás { get; private set; }
        public string Napok { get; private set; }

        public Adat_Épület_Naptár(bool előterv, int hónap, bool igazolás, string napok)
        {
            Előterv = előterv;
            Hónap = hónap;
            Igazolás = igazolás;
            Napok = napok;
        }
    }


    public class Adat_Épület_Takarításrakijelölt
    {
        public int E1elvégzettdb { get; private set; }
        public int E1kijelöltdb { get; private set; }
        public string E1rekijelölt { get; private set; }
        public int E2elvégzettdb { get; private set; }
        public int E2kijelöltdb { get; private set; }
        public string E2rekijelölt { get; private set; }
        public int E3elvégzettdb { get; private set; }
        public int E3kijelöltdb { get; private set; }
        public string E3rekijelölt { get; private set; }
        public string Helységkód { get; private set; }
        public int Hónap { get; private set; }
        public string Megnevezés { get; private set; }
        public string Osztály { get; private set; }

        public Adat_Épület_Takarításrakijelölt(int e1elvégzettdb, int e1kijelöltdb, string e1rekijelölt, int e2elvégzettdb, int e2kijelöltdb, string e2rekijelölt, int e3elvégzettdb, int e3kijelöltdb, string e3rekijelölt, string helységkód, int hónap, string megnevezés, string osztály)
        {
            E1elvégzettdb = e1elvégzettdb;
            E1kijelöltdb = e1kijelöltdb;
            E1rekijelölt = e1rekijelölt;
            E2elvégzettdb = e2elvégzettdb;
            E2kijelöltdb = e2kijelöltdb;
            E2rekijelölt = e2rekijelölt;
            E3elvégzettdb = e3elvégzettdb;
            E3kijelöltdb = e3kijelöltdb;
            E3rekijelölt = e3rekijelölt;
            Helységkód = helységkód;
            Hónap = hónap;
            Megnevezés = megnevezés;
            Osztály = osztály;
        }

        public Adat_Épület_Takarításrakijelölt(int e1kijelöltdb, int e2kijelöltdb, int e3kijelöltdb, string helységkód, int hónap)
        {
            E1kijelöltdb = e1kijelöltdb;
            E2kijelöltdb = e2kijelöltdb;
            E3kijelöltdb = e3kijelöltdb;
            Helységkód = helységkód;
            Hónap = hónap;
        }

        public Adat_Épület_Takarításrakijelölt(string helységkód, int hónap, int e1elvégzettdb, int e2elvégzettdb, int e3elvégzettdb)
        {
            Helységkód = helységkód;
            Hónap = hónap;
            E1elvégzettdb = e1elvégzettdb;
            E2elvégzettdb = e2elvégzettdb;
            E3elvégzettdb = e3elvégzettdb;
        }

        public Adat_Épület_Takarításrakijelölt(int e1kijelöltdb, int e2kijelöltdb, int e3kijelöltdb, string helységkód, int hónap, string e1rekijelölt, string e2rekijelölt, string e3rekijelölt)
        {
            E1kijelöltdb = e1kijelöltdb;
            E2kijelöltdb = e2kijelöltdb;
            E3kijelöltdb = e3kijelöltdb;
            Helységkód = helységkód;
            Hónap = hónap;
            E1rekijelölt = e1rekijelölt;
            E2rekijelölt = e2rekijelölt;
            E3rekijelölt = e3rekijelölt;
        }
    }
}

