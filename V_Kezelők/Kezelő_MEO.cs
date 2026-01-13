using System;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_MEO_Naptábla
    {
        readonly string jelszó = "rudolfg";
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\kerékmérés.mdb";
        readonly string táblanév = "naptábla";

        public Kezelő_MEO_Naptábla()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.Kerékmérésekjogtábla(hely.KönyvSzerk());
        }

        public Adat_MEO_Naptábla Egy_Adat()
        {
            Adat_MEO_Naptábla Adat = null;
            string szöveg = $"SELECT * FROM {táblanév} ";
            string kapcsolatiszöveg = $"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='{hely}'; Jet Oledb:Database Password={jelszó}";
            using (OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg))
            {
                Kapcsolat.Open();
                using (OleDbCommand Parancs = new OleDbCommand(szöveg, Kapcsolat))
                {
                    using (OleDbDataReader rekord = Parancs.ExecuteReader())
                    {
                        if (rekord.HasRows)
                        {
                            while (rekord.Read())
                            {
                                Adat = new Adat_MEO_Naptábla(
                                        rekord["Id"].ToÉrt_Int());
                            }
                        }
                    }
                }
            }
            return Adat;
        }

        public void Rögzítés(int HatárNap)
        {
            try
            {
                string szöveg = $"INSERT INTO naptábla (id) VALUES ({HatárNap})";
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

        public void Módosítás(int HatárNap, int előző)
        {
            try
            {
                string szöveg = $"UPDATE naptábla SET id={HatárNap} WHERE id={előző}";
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
    }
}
