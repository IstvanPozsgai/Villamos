using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Eszterga_Műveletek
    {
        public int ID { get; set; }
        public string Művelet { get; set; }
        public int Egység { get; set; }
        public int Mennyi_Dátum { get; set; }
        public int Mennyi_Óra { get; set; }
        public bool Státus { get; set; }
        public DateTime Utolsó_Dátum { get; set; }
        public long Utolsó_Üzemóra_Állás { get; set; }

        public Adat_Eszterga_Műveletek(int iD, string művelet, int egység, int mennyi_Dátum, int mennyi_Óra, bool státus, DateTime utolsó_Dátum, long utolsó_Üzemóra_Állás)
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
    }
    public class Adat_Eszterga_Üzemóra
    {
        public int Üzemóra { get; set; }
        public DateTime Dátum { get; set; }
        public bool Státus { get; set; }

        public Adat_Eszterga_Üzemóra(int üzemóra, DateTime dátum, bool státus)
        {
            Üzemóra = üzemóra;
            Dátum = dátum;
            Státus = státus;
        }
    }
}
