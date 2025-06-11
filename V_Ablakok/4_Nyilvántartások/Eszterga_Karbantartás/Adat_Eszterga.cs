using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Eszterga_Muveletek
    {
        public int ID { get; set; }
        public string Művelet { get; set; }
        public int Egység { get; set; }
        public int Mennyi_Dátum { get; set; }
        public int Mennyi_Óra { get; set; }
        public bool Státus { get; set; }
        public DateTime Utolsó_Dátum { get; set; }
        public long Utolsó_Üzemóra_Állás { get; set; }
        public string Megjegyzés { get; set; }

        public Adat_Eszterga_Muveletek(int iD, string művelet, int egység, int mennyi_Dátum, int mennyi_Óra, bool státus, DateTime utolsó_Dátum, long utolsó_Üzemóra_Állás, string megjegyzés)
        {
            ID = iD;
            Művelet = művelet;
            Egység = egység;
            Mennyi_Dátum = mennyi_Dátum;
            Mennyi_Óra = mennyi_Óra;
            Státus = státus;
            Utolsó_Dátum = utolsó_Dátum;
            Utolsó_Üzemóra_Állás = utolsó_Üzemóra_Állás;
            Megjegyzés = megjegyzés;
        }
        public Adat_Eszterga_Muveletek(int iD, string művelet, int egység, int mennyi_Dátum, int mennyi_Óra, bool státus, DateTime utolsó_Dátum, long utolsó_Üzemóra_Állás)
        {
            ID = iD;
            Művelet = művelet;
            Egység = egység;
            Mennyi_Dátum = mennyi_Dátum;
            Mennyi_Óra = mennyi_Óra;
            Státus = státus;
            Utolsó_Dátum = utolsó_Dátum;
            Utolsó_Üzemóra_Állás = utolsó_Üzemóra_Állás;
        }
        public Adat_Eszterga_Muveletek(int iD)
        {
            ID = iD;
        }
        public Adat_Eszterga_Muveletek(string megjegyzés, int iD)
        {
            Megjegyzés = megjegyzés;
            ID = iD;
        }
        public Adat_Eszterga_Muveletek(DateTime maiDatum, long aktivUzemora, int iD)
        {
            Utolsó_Dátum = maiDatum;
            Utolsó_Üzemóra_Állás = aktivUzemora;
            ID = iD;
        }
        public Adat_Eszterga_Muveletek(int iD, DateTime utolsó_Dátum, long utolsó_Üzemóra_Állás)
        {
            Utolsó_Dátum = utolsó_Dátum;
            Utolsó_Üzemóra_Állás = utolsó_Üzemóra_Állás;
            ID = iD;
        }
    }
    public class Adat_Eszterga_Uzemora
    {
        public int ID { get; set; }
        public long Uzemora { get; set; }
        public DateTime Dátum { get; set; }
        public bool Státus { get; set; }

        public Adat_Eszterga_Uzemora(int iD, long üzemóra, DateTime dátum, bool státus)
        {
            ID = iD;
            Uzemora = üzemóra;
            Dátum = dátum;
            Státus = státus;
        }
        public Adat_Eszterga_Uzemora(int következőId, long uzemOra, DateTime maiDátum)
        {
            ID = következőId;
            Uzemora = uzemOra;
            Dátum = maiDátum;
        }
        public Adat_Eszterga_Uzemora(int iD)
        {
            ID = iD;
        }
    }
    public class Adat_Eszterga_Muveletek_Naplo
    {
        public int ID { get; set; }
        public string Művelet { get; set; }
        public int Mennyi_Dátum { get; set; }
        public int Mennyi_Óra { get; set; }
        public DateTime Utolsó_Dátum { get; set; }
        public long Utolsó_Üzemóra_Állás { get; set; }
        public string Megjegyzés { get; set; }
        public string Rögzítő { get; set; }
        public DateTime Rögzítés_Dátuma { get; set; }

        public Adat_Eszterga_Muveletek_Naplo(int iD, string művelet, int mennyi_Dátum, int mennyi_Óra, DateTime utolsó_Dátum, long utolsó_Üzemóra_Állás, string megjegyzés, string rögzítő, DateTime rögzítés_Dátuma)
        {
            ID = iD;
            Művelet = művelet;
            Mennyi_Dátum = mennyi_Dátum;
            Mennyi_Óra = mennyi_Óra;
            Utolsó_Dátum = utolsó_Dátum;
            Utolsó_Üzemóra_Állás = utolsó_Üzemóra_Állás;
            Megjegyzés = megjegyzés;
            Rögzítő = rögzítő;
            Rögzítés_Dátuma = rögzítés_Dátuma;
        }
    }
}
