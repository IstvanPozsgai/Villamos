using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatbázis_Funkció;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_CAF_KM_Attekintes
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
        readonly string jelszó = "CzabalayL";
        readonly string táblanév = "KM_Attekintes";

        readonly Kezelő_CAF_Adatok KézAdatok = new Kezelő_CAF_Adatok();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Ciklus Kéz_Ciklus = new Kezelő_Ciklus();

        IEnumerable<Adat_CAF_Adatok> osszes_adat;
        static IEnumerable<Adat_CAF_Adatok> cache_osszes_adat = null;
        long Vizsgalatok_Kozott_Megteheto_Km;

        public Kezelő_CAF_KM_Attekintes()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.CAFtábla(hely.KönyvSzerk());

            // Ez később kivehető, ez csak a programrész verziócsere utáni első futtatása miatt került bele, hogy ne kézzel hozzuk létre a táblát.
            if (!Adatbázis.ABvanTábla(hely, jelszó, $"SELECT * FROM {táblanév}"))
            {
                Tabla_Letrehozasa();
            }

            if (cache_osszes_adat == null)
            {
                InitializeCache(KézAdatok); // egyszeri töltés
            }
            osszes_adat = cache_osszes_adat;

            // Lekéri a Ciklusrend adatbázisból a vizsgálatok közötti megtehető km értékét.
            // Elég az elsőt lekérnünk, mivel minden vizsgálatra egységesen van meghatározva.
            Vizsgalatok_Kozott_Megteheto_Km = Kéz_Ciklus.Lista_Adatok().FirstOrDefault(a => a.Típus == "CAF_km").Névleges;
        }

        // JAVÍTANDÓ: kerüljön át  Adatbázis_Létrehozás osztályba a CAF alá
        // Ez később kivehető, ez csak a programrész verziócsere utáni első futtatása miatt került bele, hogy ne kézzel hozzuk létre a táblát.
        // Az Adatbázis_Létrehozás osztályban szerepel a lenti SQL szintaxis.
        private void Tabla_Letrehozasa()
        {
            string szöveg = "CREATE TABLE KM_Attekintes (";
            szöveg += "azonosito CHAR(10), ";
            szöveg += "utolso_vizsgalat_valos_allasa LONG, ";
            szöveg += "kov_p0 LONG, ";
            szöveg += "kov_p1 LONG, ";
            szöveg += "kov_p2 LONG, ";
            szöveg += "utolso_p0_kozott LONG, ";
            szöveg += "utolso_p1_kozott LONG, ";
            szöveg += "utolso_p3_es_p2_kozott LONG, ";
            szöveg += "elso_p2 LONG, ";
            szöveg += "elso_p3 LONG,";
            szöveg += "utolso_p0_sorszam LONG,";
            szöveg += "utolso_p1_sorszam LONG,";
            szöveg += "utolso_p2_sorszam LONG,";
            szöveg += "utolso_p3_sorszam LONG);";

            MyA.ABMódosítás(hely, jelszó, szöveg);
        }

        public List<Adat_CAF_KM_Attekintes> Lista_Adatok()
        {
            string szöveg;
            szöveg = $"SELECT * FROM {táblanév} ORDER BY azonosito";

            List<Adat_CAF_KM_Attekintes> Adatok = new List<Adat_CAF_KM_Attekintes>();
            Adat_CAF_KM_Attekintes Adat;

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
                                // JAVÍTANDÓ: és miért nem jó, ha 0 értéket kap?
                                // Mivel lehet, hogy éppen úgy áll a számláló, hogy 0 Km lenne a következő vizsgálat.
                                // Így tudom, hogyha null, akkor üzenetet kell átadni és nem 0-án áll a számláló.
                                // Itt azért van null vizsgálat, mivel ha nem volt még olyan vizsgálat null értéket kap adatbázisban.
                                Adat = new Adat_CAF_KM_Attekintes(
                                 rekord["azonosito"].ToStrTrim(),
                                  rekord["utolso_vizsgalat_valos_allasa"] != DBNull.Value ? rekord["utolso_vizsgalat_valos_allasa"].ToÉrt_Long() : (long?)null,
                                 rekord["kov_p0"] != DBNull.Value ? rekord["kov_p0"].ToÉrt_Long() : (long?)null,
                                 rekord["kov_p1"] != DBNull.Value ? rekord["kov_p1"].ToÉrt_Long() : (long?)null,
                                 rekord["kov_p2"] != DBNull.Value ? rekord["kov_p2"].ToÉrt_Long() : (long?)null,
                                 rekord["utolso_p0_kozott"] != DBNull.Value ? rekord["utolso_p0_kozott"].ToÉrt_Long() : (long?)null,
                                 rekord["utolso_p1_kozott"] != DBNull.Value ? rekord["utolso_p1_kozott"].ToÉrt_Long() : (long?)null,
                                 rekord["utolso_p3_es_p2_kozott"] != DBNull.Value ? rekord["utolso_p3_es_p2_kozott"].ToÉrt_Long() : (long?)null,
                                 rekord["elso_p2"] != DBNull.Value ? rekord["elso_p2"].ToÉrt_Long() : (long?)null,
                                 rekord["elso_p3"] != DBNull.Value ? rekord["elso_p3"].ToÉrt_Long() : (long?)null,
                                 rekord["utolso_p0_sorszam"] != DBNull.Value ? rekord["utolso_p0_sorszam"].ToÉrt_Long() : (long?)null,
                                 rekord["utolso_p1_sorszam"] != DBNull.Value ? rekord["utolso_p1_sorszam"].ToÉrt_Long() : (long?)null,
                                 rekord["utolso_p2_sorszam"] != DBNull.Value ? rekord["utolso_p2_sorszam"].ToÉrt_Long() : (long?)null,
                                 rekord["utolso_p3_sorszam"] != DBNull.Value ? rekord["utolso_p3_sorszam"].ToÉrt_Long() : (long?)null
                                 );
                                Adatok.Add(Adat);
                            }
                        }
                    }
                }
            }
            return Adatok;
        }

        public Adat_CAF_KM_Attekintes Egy_Adat(string Azonosító)
        {
            Adat_CAF_KM_Attekintes Adat = null;
            try
            {
                List<Adat_CAF_KM_Attekintes> Adatok = Lista_Adatok();
                if (Adatok.Count > 0) Adat = Adatok.FirstOrDefault(a => a.azonosito == Azonosító);
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
            return Adat;
        }

        private void Rögzítés_Elso(Adat_CAF_KM_Attekintes Adat)
        {
            try
            {
                if (Egy_Adat(Adat.azonosito) == null)
                {
                    string szöveg = $"INSERT INTO {táblanév} (azonosito, utolso_vizsgalat_valos_allasa, kov_p0, kov_p1, kov_p2, utolso_p0_kozott, utolso_p1_kozott, utolso_p3_es_p2_kozott, elso_p2, elso_p3, utolso_p0_sorszam, utolso_p1_sorszam, utolso_p2_sorszam, utolso_p3_sorszam) VALUES (";

                    szöveg += $"'{Adat.azonosito}', "; // azonosító

                    szöveg += Adat.utolso_vizsgalat_valos_allasa == null
                        ? "null, "
                        : $"'{Adat.utolso_vizsgalat_valos_allasa}', "; // Utolsó vizsgálat tervezett számláló állásához képest való eltérés.

                    szöveg += Adat.kov_p0 == null
                        ? "null, "
                        : $"'{Adat.kov_p0}', "; // Megtehető KM a következő P0 vizsgálatig

                    szöveg += Adat.kov_p1 == null
                        ? "null, "
                        : $"'{Adat.kov_p1}', "; // Megtehető KM a következő P1 vizsgálatig

                    szöveg += Adat.kov_p2 == null
                        ? "null, "
                        : $"'{Adat.kov_p2}', "; // Megtehető KM a következő P2 vizsgálatig

                    szöveg += Adat.utolso_p0_kozott == null
                        ? "null, "
                        : $"'{Adat.utolso_p0_kozott}', "; // Megtett KM az előző P0 vizsgálatok között

                    szöveg += Adat.utolso_p1_kozott == null
                        ? "null, "
                        : $"'{Adat.utolso_p1_kozott}', "; // Megtett KM az előző P1 vizsgálatok között

                    szöveg += Adat.utolso_p3_es_p2_kozott == null
                        ? "null, "
                        : $"'{Adat.utolso_p3_es_p2_kozott}', "; // Megtett KM az előző P3 és P2 vizsgálatok között

                    szöveg += Adat.elso_p2 == null
                        ? "null, "
                        : $"'{Adat.elso_p2}', "; // Első P2-es vizsgálat KM értéke

                    szöveg += Adat.elso_p3 == null
                        ? "null, "
                        : $"'{Adat.elso_p3}',"; // Első P3-as vizsgálat KM értéke

                    szöveg += Adat.utolso_p0_sorszam == null
                        ? "null, "
                        : $"'{Adat.utolso_p0_sorszam}',"; //Utolso P0 vizsgálat KM sorszáma

                    szöveg += Adat.utolso_p1_sorszam == null
                        ? "null, "
                        : $"'{Adat.utolso_p1_sorszam}',"; //Utolso P1 vizsgálat KM sorszáma

                    szöveg += Adat.utolso_p2_sorszam == null
                        ? "null, "
                        : $"'{Adat.utolso_p2_sorszam}',"; //Utolso P2 vizsgálat KM sorszáma

                    szöveg += Adat.utolso_p3_sorszam == null
                        ? "null)"
                        : $"'{Adat.utolso_p3_sorszam}')"; //Utolso P3 vizsgálat KM sorszáma

                    MyA.ABMódosítás(hely, jelszó, szöveg); // SQL beszúrás futtatása
                }

                // Ezt kell lefuttatni a programrész bevezetésekor a verziócsere után               
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

        // Ez fog lefutni a frissítés gomb hatására / javítás beírásakor.
        private void Erteket_Frissit(Adat_CAF_KM_Attekintes Adat)
        {
            try
            {
                string szoveg = $"UPDATE {táblanév} SET ";

                szoveg += Adat.utolso_vizsgalat_valos_allasa == null
                    ? "utolso_vizsgalat_valos_allasa=null, "
                    : $"utolso_vizsgalat_valos_allasa='{Adat.utolso_vizsgalat_valos_allasa}', ";

                szoveg += Adat.kov_p0 == null
                    ? "kov_p0=null, "
                    : $"kov_p0='{Adat.kov_p0}', ";

                szoveg += Adat.kov_p1 == null
                    ? "kov_p1=null, "
                    : $"kov_p1='{Adat.kov_p1}', ";

                szoveg += Adat.kov_p2 == null
                    ? "kov_p2=null, "
                    : $"kov_p2='{Adat.kov_p2}', ";

                szoveg += Adat.utolso_p0_kozott == null
                    ? "utolso_p0_kozott=null, "
                    : $"utolso_p0_kozott='{Adat.utolso_p0_kozott}', ";

                szoveg += Adat.utolso_p1_kozott == null
                    ? "utolso_p1_kozott=null, "
                    : $"utolso_p1_kozott='{Adat.utolso_p1_kozott}', ";

                szoveg += Adat.utolso_p3_es_p2_kozott == null
                    ? "utolso_p3_es_p2_kozott=null, "
                    : $"utolso_p3_es_p2_kozott='{Adat.utolso_p3_es_p2_kozott}', ";

                szoveg += Adat.utolso_p0_sorszam == null
                   ? "utolso_p0_sorszam=null, "
                   : $"utolso_p0_sorszam='{Adat.utolso_p0_sorszam}', ";

                szoveg += Adat.utolso_p1_sorszam == null
                   ? "utolso_p1_sorszam=null, "
                   : $"utolso_p1_sorszam='{Adat.utolso_p1_sorszam}', ";

                szoveg += Adat.utolso_p2_sorszam == null
                   ? "utolso_p2_sorszam=null, "
                   : $"utolso_p2_sorszam='{Adat.utolso_p2_sorszam}', ";

                szoveg += Adat.utolso_p3_sorszam == null
                   ? "utolso_p3_sorszam=null "
                   : $"utolso_p3_sorszam='{Adat.utolso_p3_sorszam}' ";

                szoveg += $"WHERE azonosito='{Adat.azonosito}'";

                MyA.ABMódosítás(hely, jelszó, szoveg);
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

        // Visszaadja az összes ADB-t összefűzve. Muszáj végigmenni rajtuk. Sebességben picit jobb, mintha egyesével beolvassa őket.
        // Ezt kiszervezem a kezelőbe.
        public static void InitializeCache(Kezelő_CAF_Adatok kézAdatok)
        {
            if (cache_osszes_adat == null)
            {
                cache_osszes_adat = kézAdatok.Lista_Adatok()
                    .Concat(Enumerable.Range(2016, DateTime.Now.Year - 2016 + 1)
                    .SelectMany(ev => kézAdatok.ElőzőÉvek(ev)))
                    .ToList(); // fontos, hogy ténylegesen memóriában tárolja
            }
        }

        /// <summary>
        ///  Itt a metódusokban lévő utolsó KM kivételeket egységesíteni kell.
        /// </summary>
        /// <param name="Aktualis_palyaszam"></param>
        /// <returns></returns>
        private long Kovetkezo_P0_Vizsgalat_KM_Erteke(string Aktualis_palyaszam)
        {
            // Kiveszi az utolsó teljesített km alapú vizsgálatot.
            Adat_CAF_Adatok Adott_Villamos = osszes_adat
                                                       .Where(a => a.IDŐvKM == 2 && a.Státus == 6 && a.Azonosító == Aktualis_palyaszam && a.Megjegyzés != "Ütemezési Segéd")
                                                       .OrderByDescending(a => a.Dátum)
                                                       .FirstOrDefault();
            // Visszaadja a következő P vizsgálat KM várt értékét.
            return ((Adott_Villamos.KM_Sorszám + 1) * Vizsgalatok_Kozott_Megteheto_Km) - Utolso_KM_Vizsgalat_Erteke(Aktualis_palyaszam);
        }

        // Visszaadja, hogy a villamos a legutolsó teljesített vizsgálat során az előírt számlálóhoz képest milyen állással teljesítette azt.
        private long? Utolso_Vizsgalat_Valos_Allasa(string Aktualis_palyaszam)
        {
            Adat_CAF_Adatok Adott_Villamos = osszes_adat
                                                       .Where(a => a.IDŐvKM == 2 && a.Státus == 6 && a.Azonosító == Aktualis_palyaszam && a.Megjegyzés != "Ütemezési Segéd")
                                                       .OrderByDescending(a => a.Dátum)
                                                       .FirstOrDefault();
            return (Adott_Villamos.KM_Sorszám * Vizsgalatok_Kozott_Megteheto_Km - Utolso_KM_Vizsgalat_Erteke(Aktualis_palyaszam));
        }

        // Ez már benne van a kezelőben félig meddig, overload-olva beleteszem ezt a verziót is később
        private long Utolso_KM_Vizsgalat_Erteke(string Aktualis_palyaszam)
        {
            // Kiveszi az utolsó teljesített km alapú vizsgálatot.
            Adat_CAF_Adatok Adott_Villamos = osszes_adat
                                                       .Where(a => a.IDŐvKM == 2 && a.Státus == 6 && a.Azonosító == Aktualis_palyaszam && a.Megjegyzés != "Ütemezési Segéd")
                                                       .OrderByDescending(a => a.Dátum)
                                                       .First();
            // Visszaadja a következő sorszámú vizsgálat KM várt értékét.
            return Adott_Villamos.Számláló;

        }

        private long Kovetkezo_P1_Vizsgalat_KM_Erteke(string Aktualis_palyaszam)
        {
            // Kiveszi az utolsó teljesített km alapú vizsgálatot.
            Adat_CAF_Adatok Adott_Villamos = osszes_adat
                                                       .Where(a => a.IDŐvKM == 2 && a.Státus == 6 && a.Azonosító == Aktualis_palyaszam && a.Megjegyzés != "Ütemezési Segéd")
                                                       .OrderByDescending(a => a.Dátum)
                                                       .First();
            // Ha 5-el osztható, de 20-al nem, akkor P1 vizsgálat.
            for (int i = Adott_Villamos.KM_Sorszám; i < 80; i++)
            {
                if (i % 5 == 0 && i % 20 != 0)
                {
                    return (i * Vizsgalatok_Kozott_Megteheto_Km) - Utolso_KM_Vizsgalat_Erteke(Aktualis_palyaszam);
                }
            }
            return 0;
        }

        private long Kovetkezo_P2_Vizsgalat_KM_Erteke(string Aktualis_palyaszam)
        {
            // Kiveszi az utolsó teljesített km alapú vizsgálatot.
            Adat_CAF_Adatok Adott_Villamos = osszes_adat
                                                       .Where(a => a.IDŐvKM == 2 && a.Státus == 6 && a.Azonosító == Aktualis_palyaszam && a.Megjegyzés != "Ütemezési Segéd")
                                                       .OrderByDescending(a => a.Dátum)
                                                       .First();
            // Ha csak 20-al osztható, akkor P2/P3 vizsgálat.
            for (int i = Adott_Villamos.KM_Sorszám; i < 80; i++)
            {
                if (i % 20 == 0)
                {
                    return (i * Vizsgalatok_Kozott_Megteheto_Km) - Utolso_KM_Vizsgalat_Erteke(Aktualis_palyaszam);
                }
            }
            return 0;
        }

        private long P0_vizsgalatok_kozott_megtett_KM_Erteke(string Aktualis_palyaszam)
        {

            // P0: nem osztható 5-tel
            var p0Vizsgalatok = osszes_adat
                .Where(a => a.IDŐvKM == 2 &&
                            a.Státus == 6 &&
                            a.Azonosító == Aktualis_palyaszam &&
                            a.KM_Sorszám % 5 != 0
                            && a.Megjegyzés != "Ütemezési Segéd")
                .OrderByDescending(a => a.Dátum)
                .Take(2)
                .ToList();

            if (p0Vizsgalatok.Count < 2)
                return 0;

            return p0Vizsgalatok[0].Számláló - p0Vizsgalatok[1].Számláló;
        }

        private long? P1_vizsgalatok_kozott_megtett_KM_Erteke(string Aktualis_palyaszam)
        {
            // P1: osztható 5-tel, de nem 20-szal
            var p1Vizsgalatok = osszes_adat
                .Where(a => a.IDŐvKM == 2 &&
                            a.Státus == 6 &&
                            a.Azonosító == Aktualis_palyaszam &&
                            a.KM_Sorszám % 5 == 0 &&
                            a.KM_Sorszám % 20 != 0
                            && a.Megjegyzés != "Ütemezési Segéd")
                .OrderByDescending(a => a.Dátum)
                .Take(2)
                .ToList();

            if (p1Vizsgalatok.Count < 2 || p1Vizsgalatok[0].KM_Sorszám == p1Vizsgalatok[1].KM_Sorszám)
                return null;

            return p1Vizsgalatok[0].Számláló - p1Vizsgalatok[1].Számláló;
        }

        private long? Utolso_P3_es_P2_kozotti_futas(string Aktualis_palyaszam)
        {
            // Utolsó P3 keresése
            Adat_CAF_Adatok P3 = osszes_adat
                .Where(a => a.IDŐvKM == 2 &&
                            a.Státus == 6 &&
                            a.Azonosító == Aktualis_palyaszam &&
                            (a.Vizsgálat == "P3/2P2" || a.Vizsgálat == "2P3") &&
                            a.Megjegyzés != "Ütemezési Segéd")
                .OrderByDescending(a => a.Dátum)
                .FirstOrDefault();

            // Ha nem találunk P3 vizsgálatot
            if (P3 == null || P3.Számláló == 0) return null;

            // Utolsó P2 keresése
            Adat_CAF_Adatok P2 = osszes_adat
                .Where(a => a.IDŐvKM == 2 &&
                            a.Státus == 6 &&
                            a.Azonosító == Aktualis_palyaszam &&
                            (a.Vizsgálat == "P2" || a.Vizsgálat == "3P2") &&
                            a.Megjegyzés != "Ütemezési Segéd")
                .OrderByDescending(a => a.Dátum)
                .FirstOrDefault();

            // Ha nem találunk P2 vizsgálatot.
            if (P2 == null || P2.Számláló == 0) return null;

            return P3.Számláló - P2.Számláló;
        }

        // A 2 alábbi metódus nem fog minden megnyíláskor lefutni, kapni fog ADB-ben 3 mezőt és verziócserekor lefuttatjuk őket.
        // A jövőben az új villamosok miatt figyelni kell majd, hogy ezek a mezők csak 1x frissülhetnek a villamos élete során, tehát amikor a 20. és 40. vizsgálat megvolt.
        private long? Elso_P2_rendben_van_e(string Aktualis_palyaszam)
        {
            // Megkeresem az első P2 vizsgálatot.
            Adat_CAF_Adatok elsoP2 = osszes_adat
                .Where(a => a.IDŐvKM == 2 &&
                            a.Státus == 6 &&
                            a.Azonosító == Aktualis_palyaszam &&
                            (a.Vizsgálat == "P2" || a.Vizsgálat == "3P2") &&
                            a.Megjegyzés != "Ütemezési Segéd")
                .OrderBy(a => a.Dátum)
                .FirstOrDefault();

            if (elsoP2 == null || elsoP2.Számláló == 0) return null;

            return elsoP2?.Számláló;
        }

        private long? Elso_P3_rendben_van_e(string Aktualis_palyaszam)
        {
            // Megkeresem az első P3 vizsgálatot
            Adat_CAF_Adatok elsoP3 = osszes_adat
                .Where(a => a.IDŐvKM == 2 &&
                            a.Státus == 6 &&
                            a.Azonosító == Aktualis_palyaszam &&
                            (a.Vizsgálat == "P3/2P2" || a.Vizsgálat == "2P3") &&
                            a.Megjegyzés != "Ütemezési Segéd")
                .OrderBy(a => a.Dátum)
                .FirstOrDefault();

            if (elsoP3 == null || elsoP3.Számláló == 0) return null;

            return elsoP3?.Számláló;
        }

        private int? Utolso_P0_Sorszam(string palya)
        {
            return osszes_adat
                .Where(a => a.IDŐvKM == 2
                            && a.Státus == 6
                            && a.Azonosító == palya
                            && a.KM_Sorszám % 5 != 0   // P0: nem osztható 5-tel
                            && a.Megjegyzés != "Ütemezési Segéd")
                .OrderByDescending(a => a.Dátum)
                .Select(a => (int?)a.KM_Sorszám)
                .FirstOrDefault();
        }

        private int? Utolso_P1_Sorszam(string palya)
        {
            return osszes_adat
                .Where(a => a.IDŐvKM == 2
                            && a.Státus == 6
                            && a.Azonosító == palya
                            && a.KM_Sorszám % 5 == 0
                            && a.KM_Sorszám % 20 != 0   // P1: osztható 5-tel, de nem 20-szal
                            && a.Megjegyzés != "Ütemezési Segéd")
                .OrderByDescending(a => a.Dátum)
                .Select(a => (int?)a.KM_Sorszám)
                .FirstOrDefault();
        }

        private int? Utolso_P2_Sorszam(string palya)
        {
            return osszes_adat
                .Where(a => a.IDŐvKM == 2
                            && a.Státus == 6
                            && a.Azonosító == palya
                            && (a.Vizsgálat == "P2" || a.Vizsgálat == "3P2")
                            && a.Megjegyzés != "Ütemezési Segéd")
                .OrderByDescending(a => a.Dátum)
                .Select(a => (int?)a.KM_Sorszám)
                .FirstOrDefault();
        }

        private int? Utolso_P3_Sorszam(string palya)
        {
            return osszes_adat
                .Where(a => a.IDŐvKM == 2
                            && a.Státus == 6
                            && a.Azonosító == palya
                            && (a.Vizsgálat == "P3/2P2" || a.Vizsgálat == "2P3")
                            && a.Megjegyzés != "Ütemezési Segéd")
                .OrderByDescending(a => a.Dátum)
                .Select(a => (int?)a.KM_Sorszám)
                .FirstOrDefault();
        }

        private List<int> NemTortentPvizsgalat()
        {
            List<int> NemTortentPvizsgalat = new List<int>();

            List<int> azonositoLista = OsszesPalyaszam();

            for (int i = 0; i <= azonositoLista.Count() - 1; i++)
            {
                string Palyaszam = $"{azonositoLista[i]}";
                if (KézAdatok.Lista_Adatok().FirstOrDefault(a => a.Azonosító == Palyaszam && a.IDŐvKM == 2) == null)
                {
                    NemTortentPvizsgalat.Add(Palyaszam.ToÉrt_Int());
                }
            }
            return NemTortentPvizsgalat;
        }

        private List<int> OsszesPalyaszam()
        {
            return KézJármű.Lista_Adatok("Főmérnökség")
                   .Where(a => a.Típus.Contains("CAF") && !a.Azonosító.StartsWith("V"))
                   .Select(a => int.Parse(a.Azonosító))
                   .ToList();
        }


        // JAVÍTANDÓ: A pályaszám, helyett a típust használd
        // KÉSZ
        //Amúgy miben különbözik a rövis és a hosszú CAF?
        // Itt azért oldottam meg így, mivel 2117 az utolsó rövid CAF és ugye 2201 az első rövid.
        // Ha 1 db for ciklust használnék a feltöltésre, akkor a 2118 és 2199 között lenne egy "lyuk".
        // De teljesen jogos, most jutott eszembe, hogy 1 LINQ lekérdezés elég lett volna és a StartsWith szerepelhetett volna 2x &&-el, ha nem típust használnánk.
        public void Tabla_Feltoltese()
        {
            List<int> azonositoLista = OsszesPalyaszam();
            List<int> TortentPvizsgalat = NemTortentPvizsgalat();

            for (int i = 0; i <= azonositoLista.Count() - 1; i++)
            {
                string Palyaszam = $"{azonositoLista[i]}";
                if (!TortentPvizsgalat.Contains(Palyaszam.ToÉrt_Int()))
                {
                    Adat_CAF_KM_Attekintes teszt = new Adat_CAF_KM_Attekintes(Palyaszam, Utolso_Vizsgalat_Valos_Allasa(Palyaszam), Kovetkezo_P0_Vizsgalat_KM_Erteke(Palyaszam), Kovetkezo_P1_Vizsgalat_KM_Erteke(Palyaszam), Kovetkezo_P2_Vizsgalat_KM_Erteke(Palyaszam), P0_vizsgalatok_kozott_megtett_KM_Erteke(Palyaszam), P1_vizsgalatok_kozott_megtett_KM_Erteke(Palyaszam), Utolso_P3_es_P2_kozotti_futas(Palyaszam), Elso_P2_rendben_van_e(Palyaszam), Elso_P3_rendben_van_e(Palyaszam), Utolso_P0_Sorszam(Palyaszam), Utolso_P1_Sorszam(Palyaszam), Utolso_P2_Sorszam(Palyaszam), Utolso_P3_Sorszam(Palyaszam));
                    Rögzítés_Elso(teszt);
                }
            }
        }

        public void Erteket_Frissit_Egyeni(string palya)
        {
            List<int> TortentPvizsgalat = NemTortentPvizsgalat();

            if (!TortentPvizsgalat.Contains(palya.ToÉrt_Int()))
            {
                Adat_CAF_KM_Attekintes teszt = new Adat_CAF_KM_Attekintes(palya, Utolso_Vizsgalat_Valos_Allasa(palya), Kovetkezo_P0_Vizsgalat_KM_Erteke(palya), Kovetkezo_P1_Vizsgalat_KM_Erteke(palya), Kovetkezo_P2_Vizsgalat_KM_Erteke(palya), P0_vizsgalatok_kozott_megtett_KM_Erteke(palya), P1_vizsgalatok_kozott_megtett_KM_Erteke(palya), Utolso_P3_es_P2_kozotti_futas(palya), Elso_P2_rendben_van_e(palya), Elso_P3_rendben_van_e(palya), Utolso_P0_Sorszam(palya), Utolso_P1_Sorszam(palya), Utolso_P2_Sorszam(palya), Utolso_P3_Sorszam(palya));
                Erteket_Frissit(teszt);
            }
            else
            {
                MessageBox.Show($"A {palya} pályaszámú villamoshoz még nem került rögzítésre KM alapú vizsgálat!", "Figyelem!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void Erteket_Frissit_Osszes(string palya)
        {
            List<int> TortentPvizsgalat = NemTortentPvizsgalat();

            if (!TortentPvizsgalat.Contains(palya.ToÉrt_Int()))
            {
                Adat_CAF_KM_Attekintes teszt = new Adat_CAF_KM_Attekintes(palya, Utolso_Vizsgalat_Valos_Allasa(palya), Kovetkezo_P0_Vizsgalat_KM_Erteke(palya), Kovetkezo_P1_Vizsgalat_KM_Erteke(palya), Kovetkezo_P2_Vizsgalat_KM_Erteke(palya), P0_vizsgalatok_kozott_megtett_KM_Erteke(palya), P1_vizsgalatok_kozott_megtett_KM_Erteke(palya), Utolso_P3_es_P2_kozotti_futas(palya), Elso_P2_rendben_van_e(palya), Elso_P3_rendben_van_e(palya), Utolso_P0_Sorszam(palya), Utolso_P1_Sorszam(palya), Utolso_P2_Sorszam(palya), Utolso_P3_Sorszam(palya));
                Erteket_Frissit(teszt);
            }
        }
    }
}

