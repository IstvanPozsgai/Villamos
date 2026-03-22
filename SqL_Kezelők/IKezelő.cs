using System.Collections.Generic;

namespace Villamos.Kezelők
{
    public interface IKezelőAlap<TAdat>
    {
        string Jelszó { get; }
        string Táblanév { get; }
        string Hely { get; set; }

        void Tábla_Létrehozás();

        void FájlBeállítás(string telephely, int év);

        List<TAdat> Lista_Adatok(string telephely, int év);

        void Rögzítés(string telephely, int év, TAdat adat);


    }
}
