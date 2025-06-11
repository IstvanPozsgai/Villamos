using Villamos.Adatszerkezet;
namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {
        public static void Eszterga_Karbantartás(string hely)
        {
            string szöveg;
            string jelszó = "bozaim";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Műveletek (";
            szöveg += "[ID]  short,";
            szöveg += "[Művelet] CHAR(254),";
            szöveg += "[Egység] short, ";
            szöveg += "[Mennyi_Dátum] short,";
            szöveg += "[Mennyi_Óra] short,";
            szöveg += "[Státus] yesno,";
            szöveg += "[Utolsó_Dátum] DATE,";
            szöveg += "[Utolsó_Üzemóra_Állás] long,";
            szöveg += "[Megjegyzés] CHAR(254))";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Üzemóra (";
            szöveg += "[ID] short, ";
            szöveg += "[Üzemóra] long, ";
            szöveg += "[Dátum] DATE, ";
            szöveg += "[Státus] yesno)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
        public static void Eszterga_Karbantartas_Naplo(string hely)
        {
            string szöveg;
            string jelszó = "bozaim";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);
            szöveg = "CREATE TABLE Műveletek_Napló (";
            szöveg += "[ID]  short,";
            szöveg += "[Művelet] CHAR(254),";
            szöveg += "[Mennyi_Dátum] short,";
            szöveg += "[Mennyi_Óra] short,";
            szöveg += "[Utolsó_Dátum] DATE,";
            szöveg += "[Utolsó_Üzemóra_Állás] long,";
            szöveg += "[Megjegyzés] CHAR(254),";
            szöveg += "[Rögzítő] CHAR(200),";
            szöveg += "[Rögzítés_Dátuma] DATE)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
