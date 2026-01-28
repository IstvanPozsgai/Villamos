using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Külső_Cégek
    {
        public double Cégid { get; private set; }
        public string Cég { get; private set; }
        public string Címe { get; private set; }
        public string Cég_email { get; private set; }
        public string Felelős_személy { get; private set; }
        public string Felelős_telefonszám { get; private set; }
        public string Munkaleírás { get; private set; }
        public string Mikor { get; private set; }
        public DateTime Érv_kezdet { get; private set; }
        public DateTime Érv_vég { get; private set; }
        public DateTime Engedélyezés_dátuma { get; private set; }
        public string Engedélyező { get; private set; }
        public int Engedély { get; private set; }
        public bool Státus { get; private set; }

        public string Terület { get; private set; }

        public Adat_Külső_Cégek(double cégid, string cég, string címe, string cég_email, string felelős_személy, string felelős_telefonszám, string munkaleírás, string mikor, DateTime érv_kezdet, DateTime érv_vég, DateTime engedélyezés_dátuma, string engedélyező, int engedély, bool státus, string terület)
        {
            Cégid = cégid;
            Cég = cég;
            Címe = címe;
            Cég_email = cég_email;
            Felelős_személy = felelős_személy;
            Felelős_telefonszám = felelős_telefonszám;
            Munkaleírás = munkaleírás;
            Mikor = mikor;
            Érv_kezdet = érv_kezdet;
            Érv_vég = érv_vég;
            Engedélyezés_dátuma = engedélyezés_dátuma;
            Engedélyező = engedélyező;
            Engedély = engedély;
            Státus = státus;
            Terület = terület;
        }

        public Adat_Külső_Cégek(double cégid, int engedély)
        {
            Cégid = cégid;
            Engedély = engedély;
        }

        public Adat_Külső_Cégek(double cégid, DateTime engedélyezés_dátuma, string engedélyező, int engedély)
        {
            Cégid = cégid;
            Engedélyezés_dátuma = engedélyezés_dátuma;
            Engedélyező = engedélyező;
            Engedély = engedély;
        }
    }

    public class Adat_Külső_Gépjárművek
    {
        public double Id { get; private set; }
        public string Frsz { get; private set; }
        public double Cégid { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Külső_Gépjárművek(double id, string frsz, double cégid, bool státus)
        {
            Id = id;
            Frsz = frsz;
            Cégid = cégid;
            Státus = státus;
        }

        public Adat_Külső_Gépjárművek(double id, bool státus)
        {
            Id = id;
            Státus = státus;
        }
    }

    public class Adat_Külső_Dolgozók
    {
        public double Id { get; private set; }
        public string Név { get; private set; }
        public string Okmányszám { get; private set; }
        public string Anyjaneve { get; private set; }
        public string Születésihely { get; private set; }
        public DateTime Születésiidő { get; private set; }
        public double Cégid { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Külső_Dolgozók(double id, string név, string okmányszám, string anyjaneve, string születésihely, DateTime születésiidő, double cégid, bool státus)
        {
            Id = id;
            Név = név;
            Okmányszám = okmányszám;
            Anyjaneve = anyjaneve;
            Születésihely = születésihely;
            Születésiidő = születésiidő;
            Cégid = cégid;
            Státus = státus;
        }

        public Adat_Külső_Dolgozók(double id, string név, string okmányszám, double cégid, bool státus)
        {
            Id = id;
            Név = név;
            Okmányszám = okmányszám;
            Cégid = cégid;
            Státus = státus;
        }

        public Adat_Külső_Dolgozók(string név, string okmányszám, double cégid, bool státus)
        {
            Név = név;
            Okmányszám = okmányszám;
            Cégid = cégid;
            Státus = státus;
        }

        public Adat_Külső_Dolgozók(double id, bool státus)
        {
            Id = id;
            Státus = státus;
        }
    }

    public class Adat_Külső_Telephelyek
    {
        public double Id { get; private set; }
        public string Telephely { get; private set; }
        public double Cégid { get; private set; }
        public bool Státus { get; private set; }

        public Adat_Külső_Telephelyek(double id, string telephely, double cégid, bool státus)
        {
            Id = id;
            Telephely = telephely;
            Cégid = cégid;
            Státus = státus;
        }
    }

    public class Adat_Külső_Email
    {
        public double Id { get; private set; }
        public string Másolat { get; private set; }
        public string Aláírás { get; private set; }

        public Adat_Külső_Email(double id, string másolat, string aláírás)
        {
            Id = id;
            Másolat = másolat;
            Aláírás = aláírás;
        }
    }

    public class Adat_Külső_Lekérdezés_Autó
    {
        public string Frsz { get; private set; }
        public string Cég { get; private set; }
        public string Telephely { get; private set; }
        public string Munkaleírás { get; private set; }

        public Adat_Külső_Lekérdezés_Autó(string frsz, string cég, string telephely, string munkaleírás)
        {
            Frsz = frsz;
            Cég = cég;
            Telephely = telephely;
            Munkaleírás = munkaleírás;
        }
    }

    public class Adat_Külső_Lekérdezés_Személy
    {
        public string Név { get; private set; }
        public string Okmányszám { get; private set; }
        public string Cég { get; private set; }
        public string Munkaleírás { get; private set; }

        public Adat_Külső_Lekérdezés_Személy(string név, string okmányszám, string cég, string munkaleírás)
        {
            Név = név;
            Okmányszám = okmányszám;
            Cég = cég;
            Munkaleírás = munkaleírás;
        }
    }
}
