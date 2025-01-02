using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Reklám
    {
        public string Azonosító { get; private set; }
        public DateTime Kezdődátum { get; private set; }
        public DateTime Befejeződátum { get; private set; }
        public string Reklámneve { get; private set; }
        public string Viszonylat { get; private set; }
        public string Telephely { get; private set; }
        public string Reklámmérete { get; private set; }
        public int Szerelvényben { get; private set; }
        public string Szerelvény { get; private set; }
        public DateTime Ragasztásitilalom { get; private set; }
        public string Megjegyzés { get; private set; }
        public string Típus { get; private set; }

        public Adat_Reklám(string azonosító, DateTime kezdődátum, DateTime befejeződátum, string reklámneve, string viszonylat, string telephely, string reklámmérete, int szerelvényben, string szerelvény, DateTime ragasztásitilalom, string megjegyzés, string típus)
        {
            Azonosító = azonosító;
            Kezdődátum = kezdődátum;
            Befejeződátum = befejeződátum;
            Reklámneve = reklámneve;
            Viszonylat = viszonylat;
            Telephely = telephely;
            Reklámmérete = reklámmérete;
            Szerelvényben = szerelvényben;
            Szerelvény = szerelvény;
            Ragasztásitilalom = ragasztásitilalom;
            Megjegyzés = megjegyzés;
            Típus = típus;
        }
    }


    public class Adat_Reklám_Napló
    {
        public string Azonosító { get; private set; }
        public DateTime Kezdődátum { get; private set; }
        public DateTime Befejeződátum { get; private set; }
        public string Reklámneve { get; private set; }
        public string Viszonylat { get; private set; }
        public string Telephely { get; private set; }
        public string Reklámmérete { get; private set; }
        public int Szerelvényben { get; private set; }
        public string Szerelvény { get; private set; }
        public DateTime Ragasztásitilalom { get; private set; }
        public string Megjegyzés { get; private set; }
        public string Típus { get; private set; }
         public long Id { get; private set; }
        public DateTime Mikor { get; private set; }
        public string Módosító { get; private set; }

        public Adat_Reklám_Napló(string azonosító, DateTime kezdődátum, DateTime befejeződátum, string reklámneve, string viszonylat, string telephely, string reklámmérete, int szerelvényben, string szerelvény, DateTime ragasztásitilalom, string megjegyzés, string típus, long id, DateTime mikor, string módosító)
        {
            Azonosító = azonosító;
            Kezdődátum = kezdődátum;
            Befejeződátum = befejeződátum;
            Reklámneve = reklámneve;
            Viszonylat = viszonylat;
            Telephely = telephely;
            Reklámmérete = reklámmérete;
            Szerelvényben = szerelvényben;
            Szerelvény = szerelvény;
            Ragasztásitilalom = ragasztásitilalom;
            Megjegyzés = megjegyzés;
            Típus = típus;
            Id = id;
            Mikor = mikor;
            Módosító = módosító;
        }
    }
}
