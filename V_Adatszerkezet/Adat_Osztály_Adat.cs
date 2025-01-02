namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Osztály_Adat
    {
        public string Azonosító { get; private set; }
        public string Típus { get; private set; }
        public string AlTípus { get; private set; }
        public string Telephely { get; private set; }

        public string Szolgálat { get; private set; }
        public string Adat1 { get; private set; }
        public string Adat2 { get; private set; }
        public string Adat3 { get; private set; }
        public string Adat4 { get; private set; }
        public string Adat5 { get; private set; }
        public string Adat6 { get; private set; }
        public string Adat7 { get; private set; }
        public string Adat8 { get; private set; }
        public string Adat9 { get; private set; }
        public string Adat10 { get; private set; }
        public string Adat11 { get; private set; }
        public string Adat12 { get; private set; }
        public string Adat13 { get; private set; }
        public string Adat14 { get; private set; }
        public string Adat15 { get; private set; }
        public string Adat16 { get; private set; }
        public string Adat17 { get; private set; }
        public string Adat18 { get; private set; }
        public string Adat19 { get; private set; }
        public string Adat20 { get; private set; }
        public string Adat21 { get; private set; }
        public string Adat22 { get; private set; }
        public string Adat23 { get; private set; }
        public string Adat24 { get; private set; }
        public string Adat25 { get; private set; }
        public string Adat26 { get; private set; }
        public string Adat27 { get; private set; }
        public string Adat28 { get; private set; }
        public string Adat29 { get; private set; }
        public string Adat30 { get; private set; }
        public string Adat31 { get; private set; }
        public string Adat32 { get; private set; }
        public string Adat33 { get; private set; }
        public string Adat34 { get; private set; }
        public string Adat35 { get; private set; }
        public string Adat36 { get; private set; }
        public string Adat37 { get; private set; }
        public string Adat38 { get; private set; }
        public string Adat39 { get; private set; }
        public string Adat40 { get; private set; }

        public Adat_Osztály_Adat(string azonosító, string típus, string alTípus, string telephely, string szolgálat, string adat1, string adat2, string adat3, string adat4, string adat5, string adat6, string adat7, string adat8, string adat9, string adat10, string adat11, string adat12, string adat13, string adat14, string adat15, string adat16, string adat17, string adat18, string adat19, string adat20, string adat21, string adat22, string adat23, string adat24, string adat25, string adat26, string adat27, string adat28, string adat29, string adat30, string adat31, string adat32, string adat33, string adat34, string adat35, string adat36, string adat37, string adat38, string adat39, string adat40)
        {
            Azonosító = azonosító;
            Típus = típus;
            AlTípus = alTípus;
            Telephely = telephely;
            Szolgálat = szolgálat;
            Adat1 = adat1;
            Adat2 = adat2;
            Adat3 = adat3;
            Adat4 = adat4;
            Adat5 = adat5;
            Adat6 = adat6;
            Adat7 = adat7;
            Adat8 = adat8;
            Adat9 = adat9;
            Adat10 = adat10;
            Adat11 = adat11;
            Adat12 = adat12;
            Adat13 = adat13;
            Adat14 = adat14;
            Adat15 = adat15;
            Adat16 = adat16;
            Adat17 = adat17;
            Adat18 = adat18;
            Adat19 = adat19;
            Adat20 = adat20;
            Adat21 = adat21;
            Adat22 = adat22;
            Adat23 = adat23;
            Adat24 = adat24;
            Adat25 = adat25;
            Adat26 = adat26;
            Adat27 = adat27;
            Adat28 = adat28;
            Adat29 = adat29;
            Adat30 = adat30;
            Adat31 = adat31;
            Adat32 = adat32;
            Adat33 = adat33;
            Adat34 = adat34;
            Adat35 = adat35;
            Adat36 = adat36;
            Adat37 = adat37;
            Adat38 = adat38;
            Adat39 = adat39;
            Adat40 = adat40;
        }
    }


    public class Adat_Osztály_Név
    {
        public int Id { get; private set; }
        public string Osztálynév { get; private set; }
        public string Osztálymező { get; private set; }
        public string Használatban { get; private set; }

        public Adat_Osztály_Név(int id, string osztálynév, string osztálymező, string használatban)
        {
            Id = id;
            Osztálynév = osztálynév;
            Osztálymező = osztálymező;
            Használatban = használatban;
        }
    }


    public class Adat_Osztály_Adat_Szum
    {


        public string AlTípus { get; private set; }
        public string Telephely { get; private set; }

        public string Adat { get; private set; }

        public int Összeg { get; private set; }

        public Adat_Osztály_Adat_Szum( string alTípus, string telephely, string adat, int összeg)
        {
      
            AlTípus = alTípus;
            Telephely = telephely;
            Adat = adat;
            Összeg = összeg;
        }

        public Adat_Osztály_Adat_Szum(string adat, int összeg)
        {
            Adat = adat;
            Összeg = összeg;
        }
    }

}
