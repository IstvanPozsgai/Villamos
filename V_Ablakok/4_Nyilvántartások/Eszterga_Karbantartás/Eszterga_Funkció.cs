using System.Collections.Generic;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga
{
    static class Eszterga_Funkció
    {
        public static List<Adat_Eszterga_Műveletek> Eszterga_KarbantartasFeltölt()
        {
            Kezelő_Eszterga_Műveletek Kéz = new Kezelő_Eszterga_Műveletek();
            List<Adat_Eszterga_Műveletek> Adatok = Kéz.Lista_Adatok();
            return Adatok;
        }
        public static List<Adat_Eszterga_Üzemóra> Eszterga_ÜzemóraFeltölt()
        {
            Kezelő_Eszterga_Üzemóra Kéz = new Kezelő_Eszterga_Üzemóra();
            List<Adat_Eszterga_Üzemóra> Adatok = Kéz.Lista_Adatok();

            return Adatok;
        }
        public static List<Adat_Eszterga_Műveletek_Napló> Eszterga_KarbantartasNaplóFeltölt()
        {
            Kezelő_Eszterga_Műveletek_Napló Kéz = new Kezelő_Eszterga_Műveletek_Napló();
            List<Adat_Eszterga_Műveletek_Napló> Adatok = Kéz.Lista_Adatok();

            return Adatok;
        }
    }
}
