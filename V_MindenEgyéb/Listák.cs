using System.Collections.Generic;
using System.Linq;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos
{
    public class Listák
    {
        public static List<string> TelephelyLista_Jármű()
        {
            Kezelő_Kiegészítő_Sérülés Kéz = new Kezelő_Kiegészítő_Sérülés();
            List<Adat_Kiegészítő_Sérülés> Adatok = Kéz.Lista_Adatok().OrderBy(a => a.Név).ToList();

            Adat_Kiegészítő_Sérülés Elem = (from a in Adatok
                                            where a.Név.Trim() == Program.PostásTelephely.Trim()
                                            select a).FirstOrDefault();

            List<Adat_Kiegészítő_Sérülés> Eredmény = new List<Adat_Kiegészítő_Sérülés>();

            if (Elem != null)
            {
                // Szakszolgálat
                if (Elem.Vezér1)
                {
                    Eredmény = (from a in Adatok
                                where a.Vezér1 == false && a.Csoport1 == Elem.Csoport1
                                select a).ToList();
                    Program.Postás_Vezér = true;
                }
                //telephely
                else
                {
                    Eredmény.Add(Elem);
                    Program.Postás_Vezér = false;
                }
            }
            else
            if (Program.PostásTelephely.Trim() == "Főmérnökség" || Program.PostásTelephely.Trim() == "Műszaki osztály")
            {
                Eredmény = (from a in Adatok
                            where a.Vezér1 == false
                            select a).ToList();
                Program.Postás_Vezér = true;
            }

            List<string> Válasz = new List<string>();
            foreach (Adat_Kiegészítő_Sérülés rekord in Eredmény)
            {
                Válasz.Add(rekord.Név);
            }
            return Válasz;
        }

        public static List<Adat_Kiegészítő_Sérülés> TelephelyJármű()
        {
            Kezelő_Kiegészítő_Sérülés Kéz = new Kezelő_Kiegészítő_Sérülés();
            List<Adat_Kiegészítő_Sérülés> Adatok = Kéz.Lista_Adatok().OrderBy(a => a.Név).ToList();

            Adat_Kiegészítő_Sérülés Elem = (from a in Adatok
                                            where a.Név.Trim() == Program.PostásTelephely.Trim()
                                            select a).FirstOrDefault();

            List<Adat_Kiegészítő_Sérülés> Eredmény = new List<Adat_Kiegészítő_Sérülés>();

            if (Elem != null)
            {
                // Szakszolgálat
                if (Elem.Vezér1)
                {
                    Eredmény = (from a in Adatok
                                where a.Vezér1 == false && a.Csoport1 == Elem.Csoport1
                                select a).ToList();
                    Program.Postás_Vezér = true;
                }
                //telephely
                else
                {
                    Eredmény.Add(Elem);
                    Program.Postás_Vezér = false;
                }
            }
            else
            if (Program.PostásTelephely.Trim() == "Főmérnökség" || Program.PostásTelephely.Trim() == "Műszaki osztály")
            {
                Eredmény = (from a in Adatok
                            where a.Vezér1 == false
                            select a).ToList();
                Program.Postás_Vezér = true;
            }

            return Eredmény;
        }

        /// <summary>
        /// Telephelyek listát adja vissza
        /// Főmérnökség esetén minden telephelyet
        /// Szakszolgálat esetén saját magát és a telephelyeit
        /// Telephely esetén saját magát
        /// </summary>
        /// <param name="Főmérnök">igen benne van , ha nem Főmérnökség nincs benne</param>
        /// <returns></returns>
        public static List<string> TelephelyLista_Személy(bool Főmérnök)
        {

            Kezelő_Kiegészítő_Könyvtár Kéz = new Kezelő_Kiegészítő_Könyvtár();
            List<Adat_Kiegészítő_Könyvtár> AdatokÖ;
            //Főmérnökséget töröl a listából
            if (Főmérnök)
                AdatokÖ = Kéz.Lista_Adatok().OrderBy(a => a.Név).ToList();
            else
                AdatokÖ = Kéz.Lista_Adatok().Where(a => a.Név != "Főmérnökség").OrderBy(a => a.Név).ToList();

            Adat_Kiegészítő_Könyvtár Elem = (from a in AdatokÖ
                                             where a.Név == Program.PostásTelephely
                                             select a).FirstOrDefault();
            List<Adat_Kiegészítő_Könyvtár> Adatok = new List<Adat_Kiegészítő_Könyvtár>();

            Adatok = AdatokÖ;
            if (Program.PostásTelephely.Trim() == "Főmérnökség")
            {
                Program.Postás_Vezér = true;
                Program.Postás_telephely = false;
            }
            else
            {
                if (Elem != null)
                {
                    // Szakszolgálat
                    if (Elem.Vezér1)
                    {
                        Adatok = (from a in AdatokÖ
                                  where a.Csoport1 == Elem.Csoport1
                                  select a).ToList();
                        Program.Postás_Vezér = true;
                        Program.Postás_telephely = false;
                    }
                    //telephely
                    else
                    {
                        Adatok = (from a in AdatokÖ
                                  where a.Név == Program.PostásTelephely
                                  select a).ToList();
                        Program.Postás_Vezér = false;
                        Program.Postás_telephely = true;
                    }
                }
            }

            List<string> Válasz = new List<string>();
            foreach (Adat_Kiegészítő_Könyvtár rekord in Adatok)
            {
                Válasz.Add(rekord.Név);
            }
            return Válasz;

        }
    }
}
