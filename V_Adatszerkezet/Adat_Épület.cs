namespace Villamos.Adatszerkezet
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

}

