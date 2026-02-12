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

}
