using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Kezelők
{
    public class Kezelő_CAF_KM_Attekintes
    {
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\CAF\CAF.mdb";
        readonly string jelszó = "CzabalayL";

        readonly Kezelő_CAF_Adatok KézAdatok = new Kezelő_CAF_Adatok();
        IEnumerable<Adat_CAF_Adatok> osszes_adat;

        public Kezelő_CAF_KM_Attekintes()
        {
            if (!File.Exists(hely)) Adatbázis_Létrehozás.CAFtábla(hely.KönyvSzerk());
            osszes_adat = ÖsszesCAFAdat();
        }

        public List<Adat_CAF_KM_Attekintes> Lista_Adatok()
        {
            string szöveg;           
            szöveg = $"SELECT * FROM KM_Attekintes ORDER BY azonosito";

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
                                Adat = new Adat_CAF_KM_Attekintes(
                                        rekord["azonosito"].ToStrTrim(),
                                        rekord["kov_p0"].ToÉrt_Long(),
                                        rekord["kov_p1"].ToÉrt_Long(),
                                        rekord["kov_p2"].ToÉrt_Long(),
                                        rekord["utolso_p0_kozott"].ToÉrt_Long(),
                                        rekord["utolso_p1_kozott"].ToÉrt_Long(),
                                        rekord["utolso_p3_es_p2_kozott"].ToÉrt_Long(),
                                        rekord["elso_p2"].ToÉrt_Long(),
                                        rekord["elso_p3"].ToÉrt_Long()
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

        public void Rögzítés_Elso(Adat_CAF_KM_Attekintes Adat)
        {
            try
            {                
                if (Egy_Adat(Adat.azonosito) == null)
                {                    
                    string szöveg = "INSERT INTO KM_Attekintes (azonosito, kov_p0, kov_p1, kov_p2, utolso_p0_kozott, utolso_p1_kozott, utolso_p3_es_p2_kozott, elso_p2, elso_p3) VALUES (";
                    szöveg += $"'{Adat.azonosito}', "; // azonosító
                    szöveg += $"'{Adat.kov_p0}', "; // Megtehető KM a következő vizsgálatig.
                    szöveg += $"'{Adat.kov_p1}', "; // Megtehető KM a következő P1 vizsgálatig.
                    szöveg += $"{Adat.kov_p2}, "; // Megtehető KM a következő P2 vizsgálatig.
                    szöveg += $"'{Adat.utolso_p0_kozott}', "; // Megtett KM az előző P0 vizsgálatok között.
                    szöveg += $"'{Adat.utolso_p1_kozott}', "; // Megtett KM az előző P1 vizsgálatok között.
                    szöveg += Adat.utolso_p3_es_p2_kozott == null ? "null, " : $"'{Adat.utolso_p3_es_p2_kozott}', "; // Megtett KM az előző P3 és P2 vizsgálatok között.
                    szöveg += Adat.elso_p2 == null ? "null, " : $"'{Adat.elso_p2}', "; // Első P2-es vizsgálat KM értéke
                    szöveg += Adat.elso_p3 == null ? "null)" : $"'{Adat.elso_p3}')"; // Első P3-as vizsgálat KM értéke
                    MyA.ABMódosítás(hely, jelszó, szöveg);
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
        public void Módosítás(Adat_CAF_KM_Attekintes Adat)
        {
            try
            {
                string szöveg = "UPDATE KM_Attekintes SET ";
                szöveg += $"kov_p0='{Adat.kov_p0}', "; 
                szöveg += $"kov_p1='{Adat.kov_p1}', "; 
                szöveg += $"kov_p2={Adat.kov_p2}, "; 
                szöveg += $"utolso_p0_kozott='{Adat.utolso_p0_kozott}', "; 
                szöveg += $"utolso_p1_kozott='{Adat.utolso_p1_kozott}', "; 
                szöveg += $"utolso_p3_es_p2_kozott='{Adat.utolso_p3_es_p2_kozott}', "; 
                szöveg += $" WHERE azonosito='{Adat.azonosito}'";
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

        const int Vizsgalatok_Kozott_Megteheto_Km = 14000;

        // Visszaadja az összes ADB-t összefűzve. Muszáj végigmenni rajtuk. Sebességben picit jobb, mintha egyesével beolvassa őket.
        // Ezt kiszervezem a kezelőbe.
        private IEnumerable<Adat_CAF_Adatok> ÖsszesCAFAdat()
        {
            return KézAdatok.Lista_Adatok()
                .Concat(Enumerable.Range(2016, DateTime.Now.Year - 2016 + 1)
                    .SelectMany(ev => KézAdatok.ElőzőÉvek(ev)));
        }

        // Itt a metódusokban lévő utolsó KM kivételeket egységesíteni kell.
        private long Kovetkezo_P0_Vizsgalat_KM_Erteke(string Aktualis_palyaszam)
        {
            // Kiveszi az utolsó teljesített km alapú vizsgálatot.
            Adat_CAF_Adatok Adott_Villamos = KézAdatok.Lista_Adatok()
                                                       .Where(a => a.IDŐvKM == 2 && a.Státus == 6 && a.Azonosító == Aktualis_palyaszam)
                                                       .OrderByDescending(a => a.Dátum)
                                                       .First();
            // Visszaadja a következő P vizsgálat KM várt értékét.

            return ((Adott_Villamos.KM_Sorszám + 1) * Vizsgalatok_Kozott_Megteheto_Km) - Utolso_KM_Vizsgalat_Erteke(Aktualis_palyaszam);

        }

        // Ez már benne van a kezelőben félig meddig, overload-olva beleteszem ezt a verziót is később
        private long Utolso_KM_Vizsgalat_Erteke(string Aktualis_palyaszam)
        {
            // Kiveszi az utolsó teljesített km alapú vizsgálatot.
            Adat_CAF_Adatok Adott_Villamos = KézAdatok.Lista_Adatok()
                                                       .Where(a => a.IDŐvKM == 2 && a.Státus == 6 && a.Azonosító == Aktualis_palyaszam)
                                                       .OrderByDescending(a => a.Dátum)
                                                       .First();
            // Visszaadja a következő sorszámú vizsgálat KM várt értékét.
            return Adott_Villamos.Számláló;

        }

        private long Kovetkezo_P1_Vizsgalat_KM_Erteke(string Aktualis_palyaszam)
        {
            // Kiveszi az utolsó teljesített km alapú vizsgálatot.
            Adat_CAF_Adatok Adott_Villamos = KézAdatok.Lista_Adatok()
                                                       .Where(a => a.IDŐvKM == 2 && a.Státus == 6 && a.Azonosító == Aktualis_palyaszam)
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
            Adat_CAF_Adatok Adott_Villamos = KézAdatok.Lista_Adatok()
                                                       .Where(a => a.IDŐvKM == 2 && a.Státus == 6 && a.Azonosító == Aktualis_palyaszam)
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
                            a.KM_Sorszám % 5 != 0)
                .OrderByDescending(a => a.Dátum)
                .Take(2)
                .ToList();

            if (p0Vizsgalatok.Count < 2)
                return 0;

            return p0Vizsgalatok[0].Számláló - p0Vizsgalatok[1].Számláló;
        }

        private long P1_vizsgalatok_kozott_megtett_KM_Erteke(string Aktualis_palyaszam)
        {
            // P1: osztható 5-tel, de nem 20-szal
            var p1Vizsgalatok = osszes_adat
                .Where(a => a.IDŐvKM == 2 &&
                            a.Státus == 6 &&
                            a.Azonosító == Aktualis_palyaszam &&
                            a.KM_Sorszám % 5 == 0 &&
                            a.KM_Sorszám % 20 != 0)
                .OrderByDescending(a => a.Dátum)
                .Take(2)
                .ToList();

            if (p1Vizsgalatok.Count < 2)
                return 0;

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
            if (P3 == null) return null;

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
            if (P2 == null) return null;

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

            return elsoP3?.Számláló;
        }

        public void Feltolt()
        {
            for (int i = 2101; i < 2117; i++)
            {
                string palya = $"{i}";
                Adat_CAF_KM_Attekintes teszt = new Adat_CAF_KM_Attekintes(palya, Kovetkezo_P0_Vizsgalat_KM_Erteke(palya), Kovetkezo_P1_Vizsgalat_KM_Erteke(palya), Kovetkezo_P2_Vizsgalat_KM_Erteke(palya), P0_vizsgalatok_kozott_megtett_KM_Erteke(palya), P1_vizsgalatok_kozott_megtett_KM_Erteke(palya), Utolso_P3_es_P2_kozotti_futas(palya), Elso_P2_rendben_van_e(palya), Elso_P3_rendben_van_e(palya));
                Rögzítés_Elso(teszt);
            }
        }

    }
}

