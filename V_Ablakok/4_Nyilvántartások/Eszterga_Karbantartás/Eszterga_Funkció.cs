using System.Collections.Generic;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Villamos_Kezelők;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga
{
    // JAVÍTANDÓ:Ez miért kell, hogy pihenjen itt a kezelőből az adat, majd tovább tudjon repülni?
    static class Eszterga_Funkció
    {
        public static List<Adat_Eszterga_Muveletek> Eszterga_KarbantartasFeltolt()
        {
            Kezelo_Eszterga_Muveletek Kez = new Kezelo_Eszterga_Muveletek();
            List<Adat_Eszterga_Muveletek> Adatok = Kez.Lista_Adatok();
            return Adatok;
        }
        public static List<Adat_Eszterga_Uzemora> Eszterga_UzemoraFeltolt()
        {
            Kezelő_Eszterga_Üzemóra Kez = new Kezelő_Eszterga_Üzemóra();
            List<Adat_Eszterga_Uzemora> Adatok = Kez.Lista_Adatok();

            return Adatok;
        }
        public static List<Adat_Eszterga_Muveletek_Naplo> Eszterga_KarbantartasNaplóFeltölt()
        {
            Kezelo_Eszterga_Muveletek_Naplo Kez = new Kezelo_Eszterga_Muveletek_Naplo();
            List<Adat_Eszterga_Muveletek_Naplo> Adatok = Kez.Lista_Adatok();

            return Adatok;
        }
    }
}
