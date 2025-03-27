namespace Villamos.V_MindenEgyéb
{
    public static class Enumok
    {
        public enum TTP_Státus
        {
            Nincs_beállítva = 0,
            Ütemezett = 1,
            Javítandó = 5,
            Lezárt = 8,
            Törölt = 9
        };

        public enum Jármű_Státus
        {
            Szabad = 1,
            Beálló = 3,
            Üzemképtelen = 4
        };

        public enum Akku_Státus
        {
            Új = 1,
            Beépített = 2,
            Használt = 3,
            Selejtezésre_javasolt = 4,
            Leselejtezett = 5,
            Törölt = 9
        };

        public enum Eszterga_Állapot_Státus
        {
            Beolvasott = 1,
            Hibás = 2,
            Ellenőrzött = 4,
            Villamos_Áttöltött = 7,
            Törölt = 9
        };

        public enum Eszt_Adat_Állapot_Státus
        {
            Beolvasott = 1,
            Hibás = 2,
            Ellenőrzött = 4,
            Villamos_Áttöltött = 7,
            SAP_Áttöltött = 8,
            Törölt = 9
        };

        public enum Takfajtaadat
        {
            J2 = 1,
            J3 = 2,
            J4 = 3,
            J5 = 4,
            J6 = 5,
        };

        public enum TakfajtaadatÖ
        {
            J2 = 1,
            J3 = 2,
            J4 = 3,
            J5 = 4,
            J6 = 5,
            Gépi = 6
        };

        public enum TW6000_Státusz
        {
            tervezési = 0,
            ütemezett = 2,
            előjegyezve = 4,
            elvégzett = 6,
            törölt = 9
        }

        public enum T5C5_Nap_Státusz
        {
            Forgalomban = 0,
            Hibás = 1,
            E3 = 2,
            V1 = 3,
            V2 = 4,
            V3 = 5,
            J1 = 6,
        }

        public enum Dolgozó_Státusz
        {
            Státus_létrehozása = 0,
            Státus_megszüntetése = 1,
            Személy_csere = 2,
        }

        public enum Váltós_Naptár_Státusz
        {
            Munkanap_1 = 1,
            Ünnepnap_Ü = 2,
            Pihenőnap_P = 3,
            Vasárnap_V = 4
        }

        public enum Váltós_Naptár_Státusz_Váltó
        {
            Üres_ = 1,
            Nappal_7 = 2,
            Éjszaka_8 = 3,
            ElvontSzabadnap_E = 4,
            Pihenő_P = 5,
            KiadottSzabadnap_Z = 6
        }

        public enum Kerék_Állapot
        {
            Frissen_esztergált = 1,
            Üzemszerűen_kopott_forgalomban = 2,
            Forgalomképes_esztergálandó = 3,
            Forgalomképtelen_azonnali_esztergálást_igényel = 4
        }

        public enum Nóta_Státus
        {
            Feldolgozandó = 1,
            Telephelyi_Javítás = 3,
            Esztergálandó = 4,
            VJSZ_Javítás = 5,
            Felhasználható = 7,
            Lezárt_Selejt = 9
        }
    }
}
