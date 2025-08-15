using System;
using System.Collections.Generic;
using System.Linq;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;

namespace Villamos.Kezelők
{
    public class Kezelő_Belépés_MindenMásol
    {
        private readonly Kezelő_Belépés_Jogosultságtábla kezRegi = new Kezelő_Belépés_Jogosultságtábla();
        private readonly Kezelő_Users kezUsers = new Kezelő_Users();
        private readonly Kezelő_Gombok kezGombok = new Kezelő_Gombok();
        private readonly Kezelő_Oldalok kezOldalak = new Kezelő_Oldalok();
        private readonly Kezelő_Kiegészítő_Könyvtár kezSzervezetek = new Kezelő_Kiegészítő_Könyvtár();
        private readonly Kezelő_Jogosultságok kezJog = new Kezelő_Jogosultságok();

        public void Másolás(string telephely, string felhasznaloNev)
        {
            // 1. Betöltés listába
            List<Adat_Belépés_Jogosultságtábla> regiJogokLista = kezRegi.Lista_Adatok(telephely);
            List<Adat_Users> usersLista = kezUsers.Lista_Adatok();
            List<Adat_Gombok> gombokLista = kezGombok.Lista_Adatok();
            List<Adat_Oldalak> oldalakLista = kezOldalak.Lista_Adatok();
            List<Adat_Kiegészítő_Könyvtár> szervezetekLista = kezSzervezetek.Lista_Adatok();

            // 2. Felhasználó keresése új rendszerben
            Adat_Users user = (from u in usersLista
                               where u.UserName.Equals(felhasznaloNev, StringComparison.OrdinalIgnoreCase)
                               select u).FirstOrDefault() ?? throw new Exception("A felhasználó nincs az új rendszerben!");

            // 3. Régi jogosultság keresése
            Adat_Belépés_Jogosultságtábla regiJog = (from r in regiJogokLista
                                                     where r.Név.Equals(felhasznaloNev, StringComparison.OrdinalIgnoreCase)
                                                     select r).FirstOrDefault();
            if (regiJog == null || string.IsNullOrWhiteSpace(regiJog.Jogkörúj1))
                throw new Exception("Nincs régi jogosultság!");

            // 4. Jogosultságok építése LINQ-val
            List<Adat_Jogosultságok> ujJogLista =
                (from karakter in regiJog.Jogkörúj1.Select((kod, index) => new { Kod = kod, Index = index })
                 where karakter.Kod != '0'
                 let gomb = (from g in gombokLista
                             orderby g.GombokId
                             select g).ElementAtOrDefault(karakter.Index)
                 where gomb != null
                 let oldalId = (from o in oldalakLista
                                where o.FromName == gomb.FromName
                                select o.OldalId).FirstOrDefault()
                 where oldalId > 0
                 let allowedOrgNames = (gomb.Szervezet ?? "")
                     .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(s => s.Trim())
                     .ToList()
                 let userOrgNames = (user.Szervezetek ?? "")
                     .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                     .Select(s => s.Trim())
                     .ToList()
                 let targetOrgNames = userOrgNames.Count > 0
                     ? allowedOrgNames.Where(a => userOrgNames.Contains(a)).ToList()
                     : allowedOrgNames
                 from orgName in targetOrgNames
                 let szervezetId = (from s in szervezetekLista
                                    where s.Név.Equals(orgName, StringComparison.OrdinalIgnoreCase)
                                    select s.ID).FirstOrDefault()
                 where szervezetId > 0
                 select new Adat_Jogosultságok(
                     user.UserId,
                     oldalId,
                     gomb.GombokId,
                     szervezetId,
                     false
                 )).ToList();

            // 5. Mentés
            if (ujJogLista.Count > 0)
                kezJog.Rögzítés(ujJogLista);
        }
    }
}