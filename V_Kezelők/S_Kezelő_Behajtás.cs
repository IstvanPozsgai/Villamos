using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class S_Kezelő_Behajtás_Alap
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\SQL_Adatok\Behajtási_alap.db";
        readonly string jelszó = "egérpad";
        readonly string táblanév = "alapadatok";

        public S_Kezelő_Behajtás_Alap()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Behajtási_Alap(hely.KönyvSzerk());
        }

        public List<Adat_Behajtás_Alap> Lista_Adatok()
        {
            List<Adat_Behajtás_Alap> Adatok = new List<Adat_Behajtás_Alap>();
            try
            {

                Adatok = MyA.Lista_Adatok(hely, jelszó, táblanév, rekord => new Adat_Behajtás_Alap(
                          rekord["Id"].ToÉrt_Int(),
                          rekord["Adatbázisnév"].ToStrTrim(),
                          rekord["Sorszámbetűjele"].ToString(),
                          rekord["Sorszámkezdete"].ToÉrt_Int(),
                          rekord["Engedélyérvényes"].ToÉrt_DaTeTime(),
                          rekord["Státus"].ToÉrt_Int(),
                          rekord["Adatbáziskönyvtár"].ToStrTrim()));

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Adatok;
        }

        public void Rögzítés(Adat_Behajtás_Alap Adat)
        {
            try
            {
                string szöveg = $"INSERT  INTO {táblanév} (id, adatbázisnév, Sorszámbetűjele, Sorszámkezdete, Engedélyérvényes, Státus, Adatbáziskönyvtár) ";
                szöveg += " VALUES ";
                szöveg += $" ({Sorszám()}, '{Adat.Adatbázisnév}', '{Adat.Sorszámbetűjele}', {Adat.Sorszámkezdete},";
                szöveg += $" '{Adat.Engedélyérvényes:yyyy.MM.dd}', {Adat.Státus}, '{Adat.Adatbáziskönyvtár}')";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Módosítás(Adat_Behajtás_Alap Adat)
        {
            try
            {
                string szöveg = $"UPDATE {táblanév} SET ";
                szöveg += $"Adatbázisnév='{Adat.Adatbázisnév}', ";
                szöveg += $"Sorszámbetűjele='{Adat.Sorszámbetűjele}', ";
                szöveg += $"Sorszámkezdete={Adat.Sorszámkezdete}, ";
                szöveg += $"Engedélyérvényes='{Adat.Engedélyérvényes:yyyy.MM.dd}', ";
                szöveg += $"Státus={Adat.Státus}, ";
                szöveg += $"Adatbáziskönyvtár='{Adat.Adatbáziskönyvtár}' ";
                szöveg += $"WHERE id={Adat.Id};";
                MyA.ABMódosítás(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int Sorszám()
        {
            int válasz = 1;
            try
            {
                List<Adat_Behajtás_Alap> Adatok = Lista_Adatok();
                if (Adatok != null) válasz = Adatok.Max(a => a.Id) + 1;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return válasz;
        }
    }
}
