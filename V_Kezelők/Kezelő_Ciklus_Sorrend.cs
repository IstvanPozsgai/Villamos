using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;

namespace Villamos.V_Kezelők
{
    public class Kezelő_Ciklus_Sorrend
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\ciklus.mdb";
        readonly string jelszó = "pocsaierzsi";
        readonly string táblanév = "ciklusrendtábla";

        public Kezelő_Ciklus_Sorrend()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Ciklusrendtábla(hely.KönyvSzerk());
        }
    }
}
