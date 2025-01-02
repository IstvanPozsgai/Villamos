using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok._6_Kiadási_adatok.Menetkimaradás
{
    public class Menetkimaradás
    {
        readonly static Kezelő_Jármű KézJármű = new Kezelő_Jármű();

        static List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();

        public static List<Adat_Menetkimaradás> Excel_Lista(DataTable Tábla, string felelősmunkahely)
        {
            TípusLista();
            List<Adat_Menetkimaradás> Adatok = new List<Adat_Menetkimaradás>();

            // beolvassuk a felelős munkahelyet


            for (int i = 0; i < Tábla.Rows.Count; i++)
            {
                if (felelősmunkahely.Trim ().ToUpper () ==Tábla.Rows[i]["Felel.munkahely"].ToStrTrim().ToUpper ())
                {
                    string viszonylat = MyF.Szöveg_Tisztítás(Tábla.Rows[i]["Vonalszer."].ToStrTrim(), 0, 6);
                    string azonosító = MyF.Szöveg_Tisztítás(Tábla.Rows[i]["Berendezés"].ToStrTrim(), 1, 4);
                    string Típus = Milyen_típus(azonosító.Trim());
                    string Eseményjele = MyF.Szöveg_Tisztítás(Tábla.Rows[i]["Meghib.jele"].ToStrTrim(), 0, 1);
                    DateTime didő = Tábla.Rows[i]["Jelentés időp."].ToÉrt_DaTeTime();
                    DateTime ddátum = Tábla.Rows[i]["Jelentés dátuma"].ToÉrt_DaTeTime();
                    DateTime bekövetkezés = new DateTime(ddátum.Year, ddátum.Month, ddátum.Day, didő.Hour, didő.Minute, didő.Second);
                    int kimaradtmenet = Tábla.Rows[i]["Kimaradt menet"].ToÉrt_Int();
                    string jvbeírás = MyF.Szöveg_Tisztítás(Tábla.Rows[i]["Szöveg"].ToStrTrim(), 0, 150);
                    string vmbeírás = "*";
                    string javítás = MyF.Szöveg_Tisztítás(Tábla.Rows[i]["Ok szövege"].ToStrTrim(), 0, 150);
                    long Id = 0;
                    bool törölt = false;
                    string Jelentés = MyF.Szöveg_Tisztítás(Tábla.Rows[i]["Jelentés"].ToStrTrim(), 0, 20);
                    int tétel = Tábla.Rows[i]["Tétel"].ToÉrt_Int();

                    Adat_Menetkimaradás Adat = new Adat_Menetkimaradás(
                                                viszonylat,
                                                azonosító,
                                                Típus,
                                                Eseményjele,
                                                bekövetkezés,
                                                kimaradtmenet,
                                                jvbeírás,
                                                vmbeírás,
                                                javítás,
                                                Id,
                                                törölt,
                                                Jelentés,
                                                tétel);
                    Adatok.Add(Adat);
                }
            }
            return Adatok;
        }

        public static bool Adategyezzés(DataTable Tábla)
        {
            bool válasz;
 
            válasz = MyF.Betöltéshelyes("Menet", Tábla);
            return válasz;
        }

        private static string Milyen_típus(string azonosító)
        {
            string típus = "?";
            Adat_Jármű Elem = (from a in AdatokJármű
                               where a.Azonosító == azonosító.Trim()
                               select a).FirstOrDefault();
            if (Elem != null) típus = Elem.Valóstípus;
            return típus;
        }

        private static void TípusLista()
        {
            try
            {
                AdatokJármű.Clear();
                string hely = Application.StartupPath + @"\Főmérnökség\adatok\villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytábla";

                AdatokJármű = KézJármű.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Menetkimaradás.cs - TípusLista", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string Módosít(Adat_Menetkimaradás Elem)
        {
            string szöveg = $"UPDATE menettábla SET viszonylat='{Elem.Viszonylat}'";
            szöveg += $", azonosító='{Elem.Azonosító}'";
            szöveg += $", típus='{Elem.Típus}'";
            szöveg += $", Eseményjele='{Elem.Eseményjele}'";
            szöveg += $", Bekövetkezés='{Elem.Bekövetkezés}'";
            szöveg += $", kimaradtmenet={Elem.Kimaradtmenet}";
            szöveg += $", jvbeírás='{Elem.Jvbeírás}'";
            szöveg += $", vmbeírás='{Elem.Vmbeírás}'";
            szöveg += $", javítás='{Elem.Javítás}'";
            szöveg += $", törölt={Elem.Törölt} ";
            szöveg += $" WHERE tétel={Elem.Tétel} and jelentés='{Elem.Jelentés}'";

            return szöveg;
        }

        public  static string Rögzít(Adat_Menetkimaradás Elem, long Id)
        {
            string szöveg = "INSERT INTO menettábla ";
            szöveg += " ([viszonylat], [azonosító], [típus], [Eseményjele], [Bekövetkezés],";
            szöveg += " [kimaradtmenet], [jvbeírás], [vmbeírás], [javítás], [id], [törölt], [tétel], [jelentés]) ";
            szöveg += " VALUES (";
            szöveg += $"'{Elem.Viszonylat}','{Elem.Azonosító}','{Elem.Típus}','{Elem.Eseményjele}','{Elem.Bekövetkezés}',";
            szöveg += $"{Elem.Kimaradtmenet},'{Elem.Jvbeírás}','{Elem.Vmbeírás}','{Elem.Javítás}', {Id}, {Elem.Törölt}, {Elem.Tétel},'{Elem.Jelentés}')";
            return szöveg;
        }

    }
}
