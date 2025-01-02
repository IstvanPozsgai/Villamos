using Villamos.Adatszerkezet;

namespace Villamos.Villamos_Adatbázis_Funkció
{
    public static partial class Adatbázis_Létrehozás
    {

        public static void Kerék_Törzs(string hely)
        {
            string szöveg;
            string jelszó = "RónaiSándor";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Esztergályos (";
            szöveg += "[Dolgozószám] CHAR(8),";
            szöveg += "[dolgozónév]  char (50),";
            szöveg += "[Telephely] CHAR(50),";
            szöveg += "[Státus]  short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Tevékenység (";
            szöveg += "[Id] short,";
            szöveg += "[Tevékenység] CHAR(50),";
            szöveg += "[Munkaidő]  double,";
            szöveg += "[HáttérSzín] long,";
            szöveg += "[BetűSzín] long,";
            szöveg += "[Marad] yesno)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Tengely (";
            szöveg += "[Típus] CHAR(50),";
            szöveg += "[Munkaidő]  short,";
            szöveg += "[Állapot]  short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Terjesztés (";
            szöveg += "[Név] CHAR(50),";
            szöveg += "[Email] CHAR(50),";
            szöveg += "[Telephely] CHAR(50),";
            szöveg += "[Változat]  short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);

            szöveg = "CREATE TABLE Automata (";
            szöveg += "[FelhasználóiNév] CHAR(50),";
            szöveg += "[UtolsóÜzenet] date)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Kerék_Éves(string hely)
        {
            string szöveg;
            string jelszó = "RónaiSándor";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Naptár (";
            szöveg += "[Idő]  Date,";
            szöveg += "[Munkaidő] yesno,";
            szöveg += "[Foglalt] yesno, ";
            szöveg += "[Pályaszám]  char (70),";
            szöveg += "[Megjegyzés] LONGTEXT,";
            szöveg += "[HáttérSzín] long,";
            szöveg += "[BetűSzín] long,";
            szöveg += "[Marad] yesno)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Kerék_Igény(string hely)
        {
            string szöveg;
            string jelszó = "RónaiSándor";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Igény (";
            szöveg += "[Pályaszám]  char (70),";
            szöveg += "[Megjegyzés] LONGTEXT,";
            szöveg += "[Rögzítés_dátum] DATE, ";
            szöveg += "[Igényelte]  char (50),";
            szöveg += "[Tengelyszám] short,";
            szöveg += "[Szerelvény] short,";
            szöveg += "[prioritás] short,";
            szöveg += "[Ütemezés_dátum] DATE, ";
            szöveg += "[telephely]  char (50),";
            szöveg += "[típus]  char (50),";
            szöveg += "[státus]  short,";
            szöveg += "[Norma]  short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Kerék_Igény_napló(string hely)
        {
            string szöveg;
            string jelszó = "RónaiSándor";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Igény (";
            szöveg += "[Pályaszám]  char (70),";
            szöveg += "[Megjegyzés] LONGTEXT,";
            szöveg += "[Rögzítés_dátum] DATE, ";
            szöveg += "[Igényelte]  char (50),";
            szöveg += "[Tengelyszám] short,";
            szöveg += "[Szerelvény] short,";
            szöveg += "[prioritás] short,";
            szöveg += "[Ütemezés_dátum] DATE, ";
            szöveg += "[telephely]  char (50),";
            szöveg += "[típus]  char (50),";
            szöveg += "[státus]  short,";
            szöveg += "[Mikor] DATE, ";
            szöveg += "[Ki]  char (50),";
            szöveg += "[Norma]  short)";
            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }

        public static void Kerék_Baross_Mérési_Adatok(string hely)
        {
            string szöveg;
            string jelszó = "RónaiSándor";

            AdatBázis_kezelés ADAT = new AdatBázis_kezelés();
            ADAT.AB_Adat_Bázis_Létrehozás(hely, jelszó);

            szöveg = "CREATE TABLE Mérés (";
            szöveg += "[Dátum_1]  DATE,";
            szöveg += "[Azonosító]  char (70),";
            szöveg += "[Tulajdonos]  char (70),";
            szöveg += "[kezelő]  char (70),";
            szöveg += "[Profil]  char (70),";
            szöveg += "[Profil_szám]  long,";
            szöveg += "[Kerékpár_szám]  char (70),";
            szöveg += "[Adat_1]  char (70),";
            szöveg += "[Adat_2]  char (70),";
            szöveg += "[Adat_3]  char (70),";
            szöveg += "[Típus_Eszt]  char (70),";
            szöveg += "[KMU]  long,";
            szöveg += "[Pozíció_Eszt]  short,";
            szöveg += "[Tengely_Aznosító]  char(70),";
            szöveg += "[Adat_4]  char(70),";
            szöveg += "[Dátum_2]  DATE,";
            szöveg += "[Táv_Belső_Futó_K]  double,";
            szöveg += "[Táv_Nyom_K]  double,";
            szöveg += "[Delta_K]  double,";
            szöveg += "[B_Átmérő_K] double,";
            szöveg += "[J_Átmérő_K]  double,";
            szöveg += "[B_Axiális_K]  double,";
            szöveg += "[J_Axiális_K]  double,";
            szöveg += "[B_Radiális_K]  double,";
            szöveg += "[J_Radiális_K]  double,";
            szöveg += "[B_Nyom_Mag_K]  double,";
            szöveg += "[J_Nyom_Mag_K]  double,";
            szöveg += "[B_Nyom_Vast_K]  double,";
            szöveg += "[J_nyom_Vast_K]  double,";
            szöveg += "[B_Nyom_Vast_B_K]  double,";
            szöveg += "[J_nyom_Vast_B_K]  double,";
            szöveg += "[B_QR_K]  double,";
            szöveg += "[J_QR_K]  double,";
            szöveg += "[B_Profilhossz_K]  double,";
            szöveg += "[J_Profilhossz_K]  double,";
            szöveg += "[Dátum_3]  Date,";
            szöveg += "[Táv_Belső_Futó_Ú]  double,";
            szöveg += "[Táv_Nyom_Ú]  double,";
            szöveg += "[Delta_Ú]  double,";
            szöveg += "[B_Átmérő_Ú]  double,";
            szöveg += "[J_Átmérő_Ú]  double,";
            szöveg += "[B_Axiális_Ú]  double,";
            szöveg += "[J_Axiális_Ú]  double,";
            szöveg += "[B_Radiális_Ú]  double,";
            szöveg += "[J_Radiális_Ú]  double,";
            szöveg += "[B_Nyom_Mag_Ú]  double,";
            szöveg += "[J_Nyom_Mag_Ú]  double,";
            szöveg += "[B_Nyom_Vast_Ú]  double,";
            szöveg += "[J_nyom_Vast_Ú]  double,";
            szöveg += "[B_Nyom_Vast_B_Ú]  double,";
            szöveg += "[J_nyom_Vast_B_Ú]  double,";
            szöveg += "[B_QR_Ú]  double,";
            szöveg += "[J_QR_Ú]  double,";
            szöveg += "[B_Szög_Ú]  double,";
            szöveg += "[J_Szög_Ú]  double,";
            szöveg += "[B_Profilhossz_Ú]  double,";
            szöveg += "[J_Profilhossz_Ú]  double,";
            szöveg += "[Eszterga_Id]  long,";
            szöveg += "[Megjegyzés]  LONGTEXT,";
            szöveg += "[Státus]  int)";

            ADAT.AB_Adat_Tábla_Létrehozás(hely, jelszó, szöveg);
        }
    }
}
