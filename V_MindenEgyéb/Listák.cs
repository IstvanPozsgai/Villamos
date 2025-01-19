using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos
{
    public class Listák
    {
        public static AdatCombohoz[] TelephelyLista_Jármű()
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

            AdatCombohoz[] Combo_lista = new AdatCombohoz[Eredmény.Count];

            int i = 0;
            foreach (Adat_Kiegészítő_Sérülés rekord in Eredmény)
            {
                Combo_lista[i] = new AdatCombohoz(rekord.Név.Trim(), i);
                i += 1;
            }

            return Combo_lista;
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
        public static AdatCombohoz[] TelephelyLista_Személy(bool Főmérnök)
        {
            // false
            //List<Adat_Kiegészítő_Könyvtár> AdatokÖ = kézKönyvtár.Lista_Adatok();
            //List<Adat_Kiegészítő_Könyvtár> Adatok = (from a in AdatokÖ
            //                                         where a.Név != "Főmérnökség"
            //                                         select a).ToList();

            //foreach (Adat_Kiegészítő_Könyvtár Elem in Adatok)
            //    Cmbtelephely.Items.Add(Elem.Név);
            string hely = Application.StartupPath + @"\Főmérnökség\Adatok\kiegészítő2.mdb";
            string jelszó = "Mocó";
            string szöveg;

            //Főmérnökséget töröl a listából
            if (Főmérnök)
                szöveg = "SELECT * FROM könyvtár ORDER BY név";
            else
                szöveg = "SELECT * FROM könyvtár WHERE Név<>'Főmérnökség' ORDER BY név";


            Kezelő_Kiegészítő_Könyvtár Kéz = new Kezelő_Kiegészítő_Könyvtár();
            List<Adat_Kiegészítő_Könyvtár> AdatokÖ = Kéz.Lista_Adatok(hely, jelszó, szöveg);

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

            AdatCombohoz[] Combo_lista = new AdatCombohoz[Adatok.Count];
            int i = 0;
            foreach (Adat_Kiegészítő_Könyvtár rekord in Adatok)
            {
                Combo_lista[i] = new AdatCombohoz(rekord.Név, i);
                i += 1;
            }
            return Combo_lista;

        }
    }
}
