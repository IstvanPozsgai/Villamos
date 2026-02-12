using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Villamos.Adatszerkezet
{
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
