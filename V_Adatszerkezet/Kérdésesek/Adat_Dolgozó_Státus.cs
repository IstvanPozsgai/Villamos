using System;

namespace Villamos.Adatszerkezet
{
    public class Adat_Dolgozó_Státus
    {
        public long ID { get; private set; }
        public string Névki { get; private set; }
        public decimal Részmunkaidős { get; private set; }
        public string Hrazonosítóki { get; private set; }
        public double Bérki { get; private set; }
        public string Telephelyki { get; private set; }
        public string Kilépésoka { get; private set; }
        public DateTime Kilépésdátum { get; private set; }
        public string Névbe { get; private set; }
        public string Hrazonosítóbe { get; private set; }
        public double Bérbe { get; private set; }
        public string Honnanjött { get; private set; }
        public string Telephelybe { get; private set; }
        public DateTime Belépésidátum { get; private set; }
        public string Státusváltozások { get; private set; }
        public string Státusváltozoka { get; private set; }
        public string Megjegyzés { get; private set; }
        public bool Előzetes { get; private set; }

        public Adat_Dolgozó_Státus(long iD, string névki, decimal részmunkaidős, string hrazonosítóki, double bérki, string telephelyki, string kilépésoka, DateTime kilépésdátum,
            string névbe, string hrazonosítóbe, double bérbe, string honnanjött, string telephelybe, DateTime belépésidátum, string státusváltozások, string státusváltozoka,
            string megjegyzés, bool előzetes)
        {
            ID = iD;
            Névki = névki;
            Részmunkaidős = részmunkaidős;
            Hrazonosítóki = hrazonosítóki;
            Bérki = bérki;
            Telephelyki = telephelyki;
            Kilépésoka = kilépésoka;
            Kilépésdátum = kilépésdátum;
            Névbe = névbe;
            Hrazonosítóbe = hrazonosítóbe;
            Bérbe = bérbe;
            Honnanjött = honnanjött;
            Telephelybe = telephelybe;
            Belépésidátum = belépésidátum;
            Státusváltozások = státusváltozások;
            Státusváltozoka = státusváltozoka;
            Megjegyzés = megjegyzés;
            Előzetes = előzetes;
        }

        public Adat_Dolgozó_Státus(long iD, string névbe, string hrazonosítóbe, double bérbe, DateTime belépésidátum)
        {
            ID = iD;
            Névbe = névbe;
            Hrazonosítóbe = hrazonosítóbe;
            Bérbe = bérbe;
            Belépésidátum = belépésidátum;
        }

        public Adat_Dolgozó_Státus(long iD, string névki, string hrazonosítóki, double bérki, string telephelyki, DateTime kilépésdátum, string névbe, string hrazonosítóbe,
            string honnanjött, DateTime belépésidátum, string státusváltozások, bool előzetes)
        {
            ID = iD;
            Névki = névki;
            Hrazonosítóki = hrazonosítóki;
            Bérki = bérki;
            Telephelyki = telephelyki;
            Kilépésdátum = kilépésdátum;
            Névbe = névbe;
            Hrazonosítóbe = hrazonosítóbe;
            Honnanjött = honnanjött;
            Belépésidátum = belépésidátum;
            Státusváltozások = státusváltozások;
            Előzetes = előzetes;
        }

        public Adat_Dolgozó_Státus(long iD, DateTime kilépésdátum, bool előzetes)
        {
            ID = iD;
            Kilépésdátum = kilépésdátum;
            Előzetes = előzetes;
        }

        public Adat_Dolgozó_Státus(long iD, string kilépésoka)
        {
            ID = iD;
            Kilépésoka = kilépésoka;
        }

        public Adat_Dolgozó_Státus(long iD, string névki, string hrazonosítóki, double bérki, string telephelyki, string kilépésoka, DateTime kilépésdátum)
        {
            ID = iD;
            Névki = névki;
            Hrazonosítóki = hrazonosítóki;
            Bérki = bérki;
            Telephelyki = telephelyki;
            Kilépésoka = kilépésoka;
            Kilépésdátum = kilépésdátum;
        }

        public Adat_Dolgozó_Státus(long iD, double bérbe, DateTime belépésidátum, string névbe, string hrazonosítóbe, string honnanjött, string telephelybe)
        {
            ID = iD;
            Névbe = névbe;
            Hrazonosítóbe = hrazonosítóbe;
            Bérbe = bérbe;
            Honnanjött = honnanjött;
            Telephelybe = telephelybe;
            Belépésidátum = belépésidátum;
        }

        public Adat_Dolgozó_Státus(long iD, decimal részmunkaidős, string státusváltozoka, string megjegyzés)
        {
            ID = iD;
            Részmunkaidős = részmunkaidős;
            Státusváltozoka = státusváltozoka;
            Megjegyzés = megjegyzés;
        }

    }
}
