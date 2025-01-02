using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Kerék_Eszterga_Igény
    {
        public string Pályaszám { get; private set; }
        public string Megjegyzés { get; private set; }
        public DateTime Rögzítés_dátum { get; private set; }
        public string Igényelte { get; private set; }
        public int Tengelyszám { get; private set; }
        public int Szerelvény { get; private set; }
        public int Prioritás { get; private set; }
        public DateTime Ütemezés_dátum { get; private set; }
        public int Státus { get; private set; }
        public string Telephely { get; private set; }

        public string Típus { get; private set; }

        public int Norma { get; private set; }

        public Adat_Kerék_Eszterga_Igény(string pályaszám, string megjegyzés, DateTime rögzítés_dátum, string igényelte, int tengelyszám, int szerelvény, int prioritás, DateTime ütemezés_dátum, int státus, string telephely, string típus, int norma)
        {
            Pályaszám = pályaszám;
            Megjegyzés = megjegyzés;
            Rögzítés_dátum = rögzítés_dátum;
            Igényelte = igényelte;
            Tengelyszám = tengelyszám;
            Szerelvény = szerelvény;
            Prioritás = prioritás;
            Ütemezés_dátum = ütemezés_dátum;
            Státus = státus;
            Telephely = telephely;
            Típus = típus;
            Norma = norma;
        }
    }

    public class Adat_Kerék_Eszterga_Esztergályos
    {
        public string Dolgozószám { get; private set; }
        public string Dolgozónév { get; private set; }

        public string Telephely { get; private set; }
        public int Státus { get; private set; }

        public Adat_Kerék_Eszterga_Esztergályos(string dolgozószám, string dolgozónév, string telephely, int státus)
        {
            Dolgozószám = dolgozószám;
            Dolgozónév = dolgozónév;
            Telephely = telephely;
            Státus = státus;
        }
    }

    public class Adat_Kerék_Eszterga_Naptár
    {
        public DateTime Idő { get; private set; }
        public bool Munkaidő { get; private set; }
        public bool Foglalt { get; private set; }
        public string Pályaszám { get; private set; }
        public string Megjegyzés { get; private set; }
        public long BetűSzín { get; private set; }
        public long HáttérSzín { get; private set; }

        public bool Marad { get; set; }

        public Adat_Kerék_Eszterga_Naptár(DateTime idő, bool munkaidő, bool foglalt, string pályaszám, string megjegyzés, long betűSzín, long háttérSzín, bool marad)
        {
            Idő = idő;
            Munkaidő = munkaidő;
            Foglalt = foglalt;
            Pályaszám = pályaszám;
            Megjegyzés = megjegyzés;
            BetűSzín = betűSzín;
            HáttérSzín = háttérSzín;
            Marad = marad;
        }

        public Adat_Kerék_Eszterga_Naptár(DateTime idő)
        {
            Idő = idő;
        }

    }

    public class Adat_Kerék_Eszterga_Tevékenység
    {
        public string Tevékenység { get; private set; }
        public double Munkaidő { get; private set; }
        public long Betűszín { get; private set; }
        public long Háttérszín { get; private set; }
        public int Id { get; private set; }

        public bool Marad { get; private set; }

        public Adat_Kerék_Eszterga_Tevékenység(string tevékenység, double munkaidő, long betűszín, long háttérszín, int id, bool marad)
        {
            Tevékenység = tevékenység;
            Munkaidő = munkaidő;
            Betűszín = betűszín;
            Háttérszín = háttérszín;
            Id = id;
            Marad = marad;
        }
    }

    public class Adat_Kerék_Eszterga_Tengely
    {
        public string Típus { get; private set; }

        public int Munkaidő { get; private set; }

        public int Állapot { get; private set; }

        public Adat_Kerék_Eszterga_Tengely(string típus, int munkaidő, int állapot)
        {
            Típus = típus;
            Munkaidő = munkaidő;
            Állapot = állapot;
        }
    }


    public class Adat_Kerék_Eszterga_Igény_Napló
    {
        public string Pályaszám { get; private set; }
        public string Megjegyzés { get; private set; }
        public DateTime Rögzítés_dátum { get; private set; }
        public string Igényelte { get; private set; }
        public int Tengelyszám { get; private set; }
        public int Szerelvény { get; private set; }
        public int Prioritás { get; private set; }
        public DateTime Ütemezés_dátum { get; private set; }
        public int Státus { get; private set; }
        public string Telephely { get; private set; }
        public string Típus { get; private set; }
        public string Ki { get; private set; }

        public DateTime Mikor { get; private set; }

        public Adat_Kerék_Eszterga_Igény_Napló(string pályaszám, string megjegyzés, DateTime rögzítés_dátum, string igényelte, int tengelyszám, int szerelvény, int prioritás, DateTime ütemezés_dátum, int státus, string telephely, string típus, string ki, DateTime mikor)
        {
            Pályaszám = pályaszám;
            Megjegyzés = megjegyzés;
            Rögzítés_dátum = rögzítés_dátum;
            Igényelte = igényelte;
            Tengelyszám = tengelyszám;
            Szerelvény = szerelvény;
            Prioritás = prioritás;
            Ütemezés_dátum = ütemezés_dátum;
            Státus = státus;
            Telephely = telephely;
            Típus = típus;
            Ki = ki;
            Mikor = mikor;
        }
    }


    public class Adat_Kerék_Eszterga_Terjesztés
    {
        public string Név { get; private set; }
        public string Email { get; private set; }
        public string Telephely { get; private set; }
        public int Változat { get; private set; }

        public Adat_Kerék_Eszterga_Terjesztés(string név, string email, string telephely, int változat)
        {
            Név = név;
            Email = email;
            Telephely = telephely;
            Változat = változat;
        }
    }

    public class Adat_Kerék_Eszterga_Automata
    {
        public string FelhasználóiNév { get; private set; }
        public DateTime UtolsóÜzenet { get; private set; }

        public Adat_Kerék_Eszterga_Automata(string felhasználóiNév, DateTime utolsóÜzenet)
        {
            FelhasználóiNév = felhasználóiNév;
            UtolsóÜzenet = utolsóÜzenet;
        }
    }


    public class Adat_Baross_Mérési_Adatok
    {

        public DateTime Dátum_1 { get; private set; }
        public string Azonosító { get; private set; }
        public string Tulajdonos { get; private set; }
        public string Kezelő { get; private set; }
        public string Profil { get; private set; }
        public long Profil_szám { get; private set; }
        public string Kerékpár_szám { get; private set; }
        public string Adat_1 { get; private set; }
        public string Adat_2 { get; private set; }
        public string Adat_3 { get; private set; }
        public string Típus_Eszt { get; private set; }
        public long KMU { get; private set; }
        public int Pozíció_Eszt { get; private set; }
        public string Tengely_Aznosító { get; private set; }
        public string Adat_4 { get; private set; }
        public DateTime Dátum_2 { get; private set; }
        public double Táv_Belső_Futó_K { get; private set; }
        public double Táv_Nyom_K { get; private set; }
        public double Delta_K { get; private set; }
        public double B_Átmérő_K { get; private set; }
        public double J_Átmérő_K { get; private set; }
        public double B_Axiális_K { get; private set; }
        public double J_Axiális_K { get; private set; }
        public double B_Radiális_K { get; private set; }
        public double J_Radiális_K { get; private set; }
        public double B_Nyom_Mag_K { get; private set; }
        public double J_Nyom_Mag_K { get; private set; }
        public double B_Nyom_Vast_K { get; private set; }
        public double J_nyom_Vast_K { get; private set; }
        public double B_Nyom_Vast_B_K { get; private set; }
        public double J_nyom_Vast_B_K { get; private set; }
        public double B_QR_K { get; private set; }
        public double J_QR_K { get; private set; }
        public double B_Profilhossz_K { get; private set; }
        public double J_Profilhossz_K { get; private set; }
        public DateTime Dátum_3 { get; private set; }
        public double Táv_Belső_Futó_Ú { get; private set; }
        public double Táv_Nyom_Ú { get; private set; }
        public double Delta_Ú { get; private set; }
        public double B_Átmérő_Ú { get; private set; }
        public double J_Átmérő_Ú { get; private set; }
        public double B_Axiális_Ú { get; private set; }
        public double J_Axiális_Ú { get; private set; }
        public double B_Radiális_Ú { get; private set; }
        public double J_Radiális_Ú { get; private set; }
        public double B_Nyom_Mag_Ú { get; private set; }
        public double J_Nyom_Mag_Ú { get; private set; }
        public double B_Nyom_Vast_Ú { get; private set; }
        public double J_nyom_Vast_Ú { get; private set; }
        public double B_Nyom_Vast_B_Ú { get; private set; }
        public double J_nyom_Vast_B_Ú { get; private set; }
        public double B_QR_Ú { get; private set; }
        public double J_QR_Ú { get; private set; }
        public double B_Szög_Ú { get; private set; }
        public double J_Szög_Ú { get; private set; }
        public double B_Profilhossz_Ú { get; private set; }
        public double J_Profilhossz_Ú { get; private set; }
        public long Eszterga_Id { get; private set; }
        public string Megjegyzés { get; private set; }
        public int Státus { get; private set; }

        public Adat_Baross_Mérési_Adatok(DateTime dátum_1, string azonosító, string tulajdonos, string kezelő, string profil, long profil_szám, string kerékpár_szám, 
            string adat_1, string adat_2, string adat_3, string típus_Eszt, long kMU, int pozíció_Eszt, string tengely_Aznosító, string adat_4, DateTime dátum_2, 
            double táv_Belső_Futó_K, double táv_Nyom_K, double delta_K, double b_Átmérő_K, double j_Átmérő_K, double b_Axiális_K, double j_Axiális_K, double b_Radiális_K, 
            double j_Radiális_K, double b_Nyom_Mag_K, double j_Nyom_Mag_K, double b_Nyom_Vast_K, double j_nyom_Vast_K, double b_Nyom_Vast_B_K, double j_nyom_Vast_B_K, double b_QR_K,
            double j_QR_K, double b_Profilhossz_K, double j_Profilhossz_K, DateTime dátum_3, double táv_Belső_Futó_Ú, double táv_Nyom_Ú, double delta_Ú, double b_Átmérő_Ú,
            double j_Átmérő_Ú, double b_Axiális_Ú, double j_Axiális_Ú, double b_Radiális_Ú, double j_Radiális_Ú, double b_Nyom_Mag_Ú, double j_Nyom_Mag_Ú, double b_Nyom_Vast_Ú,
            double j_nyom_Vast_Ú, double b_Nyom_Vast_B_Ú, double j_nyom_Vast_B_Ú, double b_QR_Ú, double j_QR_Ú, double b_Szög_Ú, double j_Szög_Ú, double b_Profilhossz_Ú,
            double j_Profilhossz_Ú, long eszterga_Id, string megjegyzés, int státus)
        {
            Dátum_1 = dátum_1;
            Azonosító = azonosító;
            Tulajdonos = tulajdonos;
            Kezelő = kezelő;
            Profil = profil;
            Profil_szám = profil_szám;
            Kerékpár_szám = kerékpár_szám;
            Adat_1 = adat_1;
            Adat_2 = adat_2;
            Adat_3 = adat_3;
            Típus_Eszt = típus_Eszt;
            KMU = kMU;
            Pozíció_Eszt = pozíció_Eszt;
            Tengely_Aznosító = tengely_Aznosító;
            Adat_4 = adat_4;
            Dátum_2 = dátum_2;
            Táv_Belső_Futó_K = táv_Belső_Futó_K;
            Táv_Nyom_K = táv_Nyom_K;
            Delta_K = delta_K;
            B_Átmérő_K = b_Átmérő_K;
            J_Átmérő_K = j_Átmérő_K;
            B_Axiális_K = b_Axiális_K;
            J_Axiális_K = j_Axiális_K;
            B_Radiális_K = b_Radiális_K;
            J_Radiális_K = j_Radiális_K;
            B_Nyom_Mag_K = b_Nyom_Mag_K;
            J_Nyom_Mag_K = j_Nyom_Mag_K;
            B_Nyom_Vast_K = b_Nyom_Vast_K;
            J_nyom_Vast_K = j_nyom_Vast_K;
            B_Nyom_Vast_B_K = b_Nyom_Vast_B_K;
            J_nyom_Vast_B_K = j_nyom_Vast_B_K;
            B_QR_K = b_QR_K;
            J_QR_K = j_QR_K;
            B_Profilhossz_K = b_Profilhossz_K;
            J_Profilhossz_K = j_Profilhossz_K;
            Dátum_3 = dátum_3;
            Táv_Belső_Futó_Ú = táv_Belső_Futó_Ú;
            Táv_Nyom_Ú = táv_Nyom_Ú;
            Delta_Ú = delta_Ú;
            B_Átmérő_Ú = b_Átmérő_Ú;
            J_Átmérő_Ú = j_Átmérő_Ú;
            B_Axiális_Ú = b_Axiális_Ú;
            J_Axiális_Ú = j_Axiális_Ú;
            B_Radiális_Ú = b_Radiális_Ú;
            J_Radiális_Ú = j_Radiális_Ú;
            B_Nyom_Mag_Ú = b_Nyom_Mag_Ú;
            J_Nyom_Mag_Ú = j_Nyom_Mag_Ú;
            B_Nyom_Vast_Ú = b_Nyom_Vast_Ú;
            J_nyom_Vast_Ú = j_nyom_Vast_Ú;
            B_Nyom_Vast_B_Ú = b_Nyom_Vast_B_Ú;
            J_nyom_Vast_B_Ú = j_nyom_Vast_B_Ú;
            B_QR_Ú = b_QR_Ú;
            J_QR_Ú = j_QR_Ú;
            B_Szög_Ú = b_Szög_Ú;
            J_Szög_Ú = j_Szög_Ú;
            B_Profilhossz_Ú = b_Profilhossz_Ú;
            J_Profilhossz_Ú = j_Profilhossz_Ú;
            Eszterga_Id = eszterga_Id;
            Megjegyzés = megjegyzés;
            Státus = státus;
        }
    }
}
