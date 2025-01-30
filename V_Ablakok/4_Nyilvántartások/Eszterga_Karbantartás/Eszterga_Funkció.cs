using System.Collections.Generic;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Kezelők;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga
{
    static class Eszterga_Funkció
    {
        readonly static string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Eszterga_Karbantartás.mdb";
        readonly static string jelszó = "bozaim";
        public static List<Adat_Eszterga_Műveletek> Eszterga_KarbantartasFeltölt()
        {
            Kezelő_Eszterga_Műveletek KézAlap = new Kezelő_Eszterga_Műveletek();
            List<Adat_Eszterga_Műveletek> AdatokAlap = new List<Adat_Eszterga_Műveletek>();

            string szöveg = "SELECT * FROM Műveletek ORDER BY ID";
            AdatokAlap = KézAlap.Lista_Adatok(hely, jelszó, szöveg);
            return AdatokAlap;
        }
        public static List<Adat_Eszterga_Üzemóra> Eszterga_ÜzemóraFeltölt()
        {
            Kezelő_Eszterga_Üzemóra KézAlap = new Kezelő_Eszterga_Üzemóra();
            List<Adat_Eszterga_Üzemóra> AdatokAlap = new List<Adat_Eszterga_Üzemóra>();

            string szöveg = "SELECT * FROM Üzemóra";
            AdatokAlap = KézAlap.Lista_Adatok(hely, jelszó, szöveg);
            return AdatokAlap;
        }
    }
}
