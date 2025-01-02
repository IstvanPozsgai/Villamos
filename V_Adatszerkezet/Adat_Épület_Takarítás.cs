namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Épület_Takarítás_Adattábla
    {
        public int Id { get; private set; }
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

        public Adat_Épület_Takarítás_Adattábla(int id, string megnevezés, string osztály, double méret, string helységkód, bool státus, int e1évdb, int e2évdb, int e3évdb, string kezd, string végez, string ellenőremail, string ellenőrneve, string ellenőrtelefonszám, bool szemetes, string kapcsolthelység)
        {
            Id = id;
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


    public class Adat_Épület_Takarítás_Osztály
    {
        public int Id { get; private set; }
        public string Osztály { get; private set; }
        public double E1Ft { get; private set; }
        public double E2Ft { get; private set; }
        public double E3Ft { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Épület_Takarítás_Osztály(int id, string osztály, double e1Ft, double e2Ft, double e3Ft, bool státus)
        {
            Id = id;
            Osztály = osztály;
            E1Ft = e1Ft;
            E2Ft = e2Ft;
            E3Ft = e3Ft;
            Státus = státus;
        }
    }

    public class Adat_ÉpJár_Takarítás_TIG
    {
        public string Telephely { get; private set; }
        public string Tevékenység { get; private set; }
        public double Mennyiség { get; private set; }
        public string ME { get; private set; }
        public double Egységár { get; private set; }
        public double Összesen { get; private set; }

        public Adat_ÉpJár_Takarítás_TIG(string telephely, string tevékenység, double mennyiség, string mE, double egységár, double összesen)
        {
            Telephely = telephely;
            Tevékenység = tevékenység;
            Mennyiség = mennyiség;
            ME = mE;
            Egységár = egységár;
            Összesen = összesen;
        }
    }
}
