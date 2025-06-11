using System.Collections.Generic;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga
{
    static class Eszterga_Funkció
    {
        public static List<Adat_Eszterga_Muveletek> Eszterga_KarbantartasFeltolt()
        {
            Kezelő_Eszterga_Műveletek Kéz = new Kezelő_Eszterga_Műveletek();
            List<Adat_Eszterga_Muveletek> Adatok = Kéz.Lista_Adatok();
            return Adatok;
        }
        public static List<Adat_Eszterga_Uzemora> Eszterga_UzemoraFeltolt()
        {
            Kezelő_Eszterga_Üzemóra Kéz = new Kezelő_Eszterga_Üzemóra();
            List<Adat_Eszterga_Uzemora> Adatok = Kéz.Lista_Adatok();

            return Adatok;
        }
        public static List<Adat_Eszterga_Muveletek_Naplo> Eszterga_KarbantartasNaplóFeltölt()
        {
            Kezelő_Eszterga_Műveletek_Napló Kéz = new Kezelő_Eszterga_Műveletek_Napló();
            List<Adat_Eszterga_Muveletek_Naplo> Adatok = Kéz.Lista_Adatok();

            return Adatok;
        }
    }
}
