using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Technológia_Új
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

        public Adat_Technológia_Új(long iD, string részegység, string munka_utasítás_szám, string utasítás_Cím, string utasítás_leírás, string paraméter,
            int karb_ciklus_eleje, int karb_ciklus_vége, DateTime érv_kezdete, DateTime érv_vége, string szakmai_bontás, string munkaterületi_bontás, string altípus, bool kenés)
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
        }
    }


    public class Adat_Technológia
    {
        public long ID { get; private set; }
        public string Részegység { get; private set; }
        public string Munka_utasítás_szám { get; private set; }
        public string Utasítás_Cím { get; private set; }
        public string Utasítás_leírás { get; private set; }
        public string Paraméter { get; private set; }
        public Adat_technológia_Ciklus Karb_ciklus_eleje { get; private set; }
        public Adat_technológia_Ciklus Karb_ciklus_vége { get; private set; }
        public DateTime Érv_kezdete { get; private set; }
        public DateTime Érv_vége { get; private set; }
        public string Szakmai_bontás { get; private set; }
        public string Munkaterületi_bontás { get; private set; }
        public string Altípus { get; private set; }
        public bool Kenés { get; private set; }

        public Adat_Technológia(long iD, string részegység, string munka_utasítás_szám, string utasítás_Cím, string utasítás_leírás, string paraméter, Adat_technológia_Ciklus karb_ciklus_eleje, Adat_technológia_Ciklus karb_ciklus_vége, DateTime érv_kezdete, DateTime érv_vége, string szakmai_bontás, string munkaterületi_bontás, string altípus, bool kenés)
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
        }
    }


    public class Adat_Technológia_Alap
    {
        public long Id { get; private set; }
        public string Típus { get; private set; }


        public Adat_Technológia_Alap(long id, string típus)
        {
            Id = id;
            Típus = típus;
        }
    }

    public class Adat_technológia_Ciklus
    {

        public int Sorszám { get; private set; }
        public string Fokozat { get; private set; }
        public int Csoportos { get; private set; }
        public string Elérés { get; private set; }
        public string Verzió { get; private set; }

        public Adat_technológia_Ciklus(int sorszám, string fokozat, int csoportos, string elérés, string verzió)
        {
            Sorszám = sorszám;
            Fokozat = fokozat;
            Csoportos = csoportos;
            Elérés = elérés;
            Verzió = verzió;
        }

        public Adat_technológia_Ciklus(int sorszám, string fokozat)
        {
            Sorszám = sorszám;
            Fokozat = fokozat;
        }
    }

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


    public class Adat_Technológia_Változat
    {
        public long Technológia_Id { get; private set; }
        public string Változatnév { get; private set; }
        public string Végzi { get; private set; }
        public string Karbantartási_fokozat { get; private set; }

        public Adat_Technológia_Változat(long technológia_Id, string változatnév, string végzi, string karbantartási_fokozat)
        {
            Technológia_Id = technológia_Id;
            Változatnév = változatnév;
            Végzi = végzi;
            Karbantartási_fokozat = karbantartási_fokozat;
        }

        public Adat_Technológia_Változat(long technológia_Id)
        {
            Technológia_Id = technológia_Id;
        }
    }


    public class Adat_Technológia_Kivételek
    {
        public long Id { get; private set; }
        public string Azonosító { get; private set; }
        public string Altípus { get; private set; }

        public Adat_Technológia_Kivételek(long id, string azonosító, string altípus)
        {
            Id = id;
            Azonosító = azonosító;
            Altípus = altípus;
        }
    }

    public class Adat_Technológia_Rendelés
    {
        public long Év { get; private set; }
        public string Karbantartási_fokozat { get; private set; }
        public string Technológia_típus { get; private set; }
        public string Rendelésiszám { get; private set; }

        public Adat_Technológia_Rendelés(long év, string karbantartási_fokozat, string technológia_típus, string rendelésiszám)
        {
            Év = év;
            Karbantartási_fokozat = karbantartási_fokozat;
            Technológia_típus = technológia_típus;
            Rendelésiszám = rendelésiszám;
        }
    }


}
