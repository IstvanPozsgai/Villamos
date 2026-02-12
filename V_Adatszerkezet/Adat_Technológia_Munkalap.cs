using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
    public class Adat_Technológia_Munkalap : IEquatable<Adat_Technológia_Munkalap>
    {
        public long ID { get; private set; }
        public string Részegység { get; private set; }
        public string Munka_utasítás_szám { get; private set; }
        public string Utasítás_Cím { get; private set; }
        public string Utasítás_leírás { get; private set; }
        public string Paraméter { get; private set; }
        public int Karb_ciklus_eleje { get; private set; }
        public int Karb_ciklus_vége { get; private set; }

        public DateTime Érv_kezdete { get; private set; }
        public DateTime Érv_vége { get; private set; }

        public string Szakmai_bontás { get; private set; }
        public string Munkaterületi_bontás { get; private set; }
        public string Altípus { get; private set; }
        public bool Kenés { get; private set; }


        public string Karbantartási_fokozat { get; private set; }
        public string Változatnév { get; private set; }
        public string Végzi { get; private set; }

        public Adat_Technológia_Munkalap(long iD, string részegység, string munka_utasítás_szám, string utasítás_Cím, string utasítás_leírás, string paraméter, int karb_ciklus_eleje, int karb_ciklus_vége, DateTime érv_kezdete, DateTime érv_vége, string szakmai_bontás, string munkaterületi_bontás, string altípus, bool kenés, string karbantartási_fokozat, string változatnév, string végzi)
        {
            ID = iD;
            Részegység = részegység;
            Munka_utasítás_szám = munka_utasítás_szám;
            Utasítás_Cím = utasítás_Cím;
            Utasítás_leírás = utasítás_leírás;
            Paraméter = paraméter;
            Karb_ciklus_eleje = karb_ciklus_eleje;
            Karb_ciklus_vége = karb_ciklus_vége;
            Érv_kezdete = érv_kezdete;
            Érv_vége = érv_vége;
            Szakmai_bontás = szakmai_bontás;
            Munkaterületi_bontás = munkaterületi_bontás;
            Altípus = altípus;
            Kenés = kenés;
            Karbantartási_fokozat = karbantartási_fokozat;
            Változatnév = változatnév;
            Végzi = végzi;
        }

        public Adat_Technológia_Munkalap(long iD, string részegység, string munka_utasítás_szám, string utasítás_Cím, string utasítás_leírás, string paraméter, int karb_ciklus_eleje, int karb_ciklus_vége, DateTime érv_kezdete, DateTime érv_vége, string szakmai_bontás, string munkaterületi_bontás, string altípus, bool kenés, string változatnév, string végzi)
        {
            ID = iD;
            Részegység = részegység;
            Munka_utasítás_szám = munka_utasítás_szám;
            Utasítás_Cím = utasítás_Cím;
            Utasítás_leírás = utasítás_leírás;
            Paraméter = paraméter;
            Karb_ciklus_eleje = karb_ciklus_eleje;
            Karb_ciklus_vége = karb_ciklus_vége;
            Érv_kezdete = érv_kezdete;
            Érv_vége = érv_vége;
            Szakmai_bontás = szakmai_bontás;
            Munkaterületi_bontás = munkaterületi_bontás;
            Altípus = altípus;
            Kenés = kenés;
            Változatnév = változatnév;
            Végzi = végzi;
        }

        public bool Equals(Adat_Technológia_Munkalap other)
        {
            return this.ID.Equals(other.ID);
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}
