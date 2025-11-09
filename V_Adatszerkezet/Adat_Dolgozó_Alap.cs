using System;

namespace Villamos.Villamos_Adatszerkezet
{
    public class Adat_Dolgozó_Alap
    {
        public long Sorszám { get; private set; }
        public string DolgozóNév { get; private set; }
        public string Dolgozószám { get; private set; }
        public string Leánykori { get; private set; }
        public string Anyja { get; private set; }
        public DateTime Születésiidő { get; private set; }
        public string Születésihely { get; private set; }
        public string TAj { get; private set; }
        public string ADÓ { get; private set; }
        public DateTime Belépésiidő { get; private set; }
        public string Lakcím { get; private set; }
        public string Ideiglenescím { get; private set; }
        public string Telefonszám1 { get; private set; }
        public string Telefonszám2 { get; private set; }
        public string Telefonszám3 { get; private set; }
        public string Munkakör { get; private set; }
        public bool Csopvez { get; private set; }
        public string Csoport { get; private set; }
        public bool Munkarend { get; private set; }
        public DateTime Orvosiérvényesség { get; private set; }
        public DateTime Orvosivizsgálat { get; private set; }
        public DateTime Targoncaérvényesség { get; private set; }
        public DateTime Emelőérvényesség { get; private set; }
        public DateTime Kilépésiidő { get; private set; }
        public string Emelőgépigazolvány { get; private set; }
        public string Nehézgépkezelőigazolvány { get; private set; }
        public string Targoncaigazolvány { get; private set; }
        public DateTime Képernyősidő { get; private set; }
        public DateTime Nehézgépidő { get; private set; }
        public string Feorsz { get; private set; }
        public string Jogosítványszám { get; private set; }
        public DateTime Jogosítványérvényesség { get; private set; }
        public string Jogtanúsítvány { get; private set; }
        public DateTime Jogorvosi { get; private set; }
        public DateTime Tűzvizsgaideje { get; private set; }
        public DateTime Tűzvizsgaérv { get; private set; }
        public bool Passzív { get; private set; }
        public string Jogosítványkategória { get; private set; }
        public string Bejelentkezésinév { get; private set; }
        public string Főkönyvtitulus { get; private set; }
        public bool Vezényelt { get; private set; }
        public bool Vezényelve { get; private set; }
        public bool Részmunkaidős { get; private set; }
        public bool Alkalmazott { get; private set; }
        public string Csoportkód { get; private set; }
        public bool Túlóraeng { get; private set; }
        public decimal Részmunkaidőperc { get; private set; }


        public Adat_Dolgozó_Alap(long sorszám, string dolgozóNév, string dolgozószám, string leánykori, string anyja, DateTime születésiidő, string születésihely, string tAj,
            string aDÓ, DateTime belépésiidő, string lakcím, string ideiglenescím, string telefonszám1, string telefonszám2, string telefonszám3, string munkakör, bool csopvez,
            string csoport, bool munkarend, DateTime orvosiérvényesség, DateTime orvosivizsgálat, DateTime targoncaérvényesség, DateTime emelőérvényesség, DateTime kilépésiidő,
            string emelőgépigazolvány, string nehézgépkezelőigazolvány, string targoncaigazolvány, DateTime képernyősidő, DateTime nehézgépidő, string feorsz, string jogosítványszám,
            DateTime jogosítványérvényesség, string jogtanúsítvány, DateTime jogorvosi, DateTime tűzvizsgaideje, DateTime tűzvizsgaérv, bool passzív, string jogosítványkategória,
            string bejelentkezésinév, string főkönyvtitulus, bool vezényelt, bool vezényelve, bool részmunkaidős, bool alkalmazott, string csoportkód, bool túlóraeng,
            decimal részmunkaidőperc)
        {
            Sorszám = sorszám;
            DolgozóNév = dolgozóNév;
            Dolgozószám = dolgozószám;
            Leánykori = leánykori;
            Anyja = anyja;
            Születésiidő = születésiidő;
            Születésihely = születésihely;
            TAj = tAj;
            ADÓ = aDÓ;
            Belépésiidő = belépésiidő;
            Lakcím = lakcím;
            Ideiglenescím = ideiglenescím;
            Telefonszám1 = telefonszám1;
            Telefonszám2 = telefonszám2;
            Telefonszám3 = telefonszám3;
            Munkakör = munkakör;
            Csopvez = csopvez;
            Csoport = csoport;
            Munkarend = munkarend;
            Orvosiérvényesség = orvosiérvényesség;
            Orvosivizsgálat = orvosivizsgálat;
            Targoncaérvényesség = targoncaérvényesség;
            Emelőérvényesség = emelőérvényesség;
            Kilépésiidő = kilépésiidő;
            Emelőgépigazolvány = emelőgépigazolvány;
            Nehézgépkezelőigazolvány = nehézgépkezelőigazolvány;
            Targoncaigazolvány = targoncaigazolvány;
            Képernyősidő = képernyősidő;
            Nehézgépidő = nehézgépidő;
            Feorsz = feorsz;
            Jogosítványszám = jogosítványszám;
            Jogosítványérvényesség = jogosítványérvényesség;
            Jogtanúsítvány = jogtanúsítvány;
            Jogorvosi = jogorvosi;
            Tűzvizsgaideje = tűzvizsgaideje;
            Tűzvizsgaérv = tűzvizsgaérv;
            Passzív = passzív;
            Jogosítványkategória = jogosítványkategória;
            Bejelentkezésinév = bejelentkezésinév;
            Főkönyvtitulus = főkönyvtitulus;
            Vezényelt = vezényelt;
            Vezényelve = vezényelve;
            Részmunkaidős = részmunkaidős;
            Alkalmazott = alkalmazott;
            Csoportkód = csoportkód;
            Túlóraeng = túlóraeng;
            Részmunkaidőperc = részmunkaidőperc;
        }

        public Adat_Dolgozó_Alap(string dolgozóNév, string dolgozószám)
        {
            DolgozóNév = dolgozóNév;
            Dolgozószám = dolgozószám;
        }

        public Adat_Dolgozó_Alap(string dolgozószám, bool túlóraeng)
        {
            Dolgozószám = dolgozószám;
            Túlóraeng = túlóraeng;
        }

        public Adat_Dolgozó_Alap(string dolgozószám, string feorsz, string munkakör)
        {
            Dolgozószám = dolgozószám;
            Feorsz = feorsz;
            Munkakör = munkakör;
        }

        public Adat_Dolgozó_Alap(string dolgozószám, string csoport, string főkönyvtitulus, string bejelentkezésinév, bool csopvez, bool munkarend, bool passzív, bool részmunkaidős,
                                 bool alkalmazott, string tAj, string csoportkód, decimal részmunkaidőperc)
        {
            Dolgozószám = dolgozószám;
            Csoport = csoport;
            Főkönyvtitulus = főkönyvtitulus;
            Bejelentkezésinév = bejelentkezésinév;
            Csopvez = csopvez;
            Munkarend = munkarend;
            Passzív = passzív;
            Részmunkaidős = részmunkaidős;
            Alkalmazott = alkalmazott;
            TAj = tAj;
            Csoportkód = csoportkód;
            Részmunkaidőperc = részmunkaidőperc;
        }

        public Adat_Dolgozó_Alap(string dolgozószám, string jogosítványszám, string jogtanúsítvány, DateTime jogosítványérvényesség, DateTime jogorvosi)
        {
            Dolgozószám = dolgozószám;
            Jogosítványszám = jogosítványszám;
            Jogtanúsítvány = jogtanúsítvány;
            Jogosítványérvényesség = jogosítványérvényesség;
            Jogorvosi = jogorvosi;
        }

        public Adat_Dolgozó_Alap(string dolgozószám, string csoport, DateTime kilépésiidő)
        {
            Dolgozószám = dolgozószám;
            Csoport = csoport;
            Kilépésiidő = kilépésiidő;
        }

        public Adat_Dolgozó_Alap(string dolgozószám, DateTime kilépésiidő)
        {
            Dolgozószám = dolgozószám;
            Kilépésiidő = kilépésiidő;
        }

        public Adat_Dolgozó_Alap(string dolgozószám, DateTime kilépésiidő, string lakcím)
        {
            Dolgozószám = dolgozószám;
            Kilépésiidő = kilépésiidő;
            Lakcím = lakcím;
        }

        public Adat_Dolgozó_Alap(string dolgozószám, string dolgozóNév, DateTime kilépésiidő, DateTime belépésiidő)
        {
            DolgozóNév = dolgozóNév;
            Dolgozószám = dolgozószám;
            Kilépésiidő = kilépésiidő;
            Belépésiidő = belépésiidő;
        }

        public Adat_Dolgozó_Alap(string dolgozószám, string dolgozóNév, DateTime kilépésiidő, DateTime belépésiidő, string lakcím)
        {
            DolgozóNév = dolgozóNév;
            Dolgozószám = dolgozószám;
            Kilépésiidő = kilépésiidő;
            Belépésiidő = belépésiidő;
            Lakcím = lakcím;
        }

        public Adat_Dolgozó_Alap(string dolgozószám, string dolgozóNév, DateTime kilépésiidő, DateTime belépésiidő, string lakcím, string munkakör)
        {
            DolgozóNév = dolgozóNév;
            Dolgozószám = dolgozószám;
            Kilépésiidő = kilépésiidő;
            Belépésiidő = belépésiidő;
            Lakcím = lakcím;
            Munkakör = munkakör;
        }

        public Adat_Dolgozó_Alap(string dolgozószám, string dolgozóNév, DateTime kilépésiidő, bool vezényelt, bool vezényelve, string lakcím)
        {
            Dolgozószám = dolgozószám;
            DolgozóNév = dolgozóNév;
            Kilépésiidő = kilépésiidő;
            Vezényelt = vezényelt;
            Vezényelve = vezényelve;
            Lakcím = lakcím;
        }

        /// <summary>
        /// IDM adatok betöltéséhez kell
        /// </summary>
        /// <param name="dolgozóNév"></param>
        /// <param name="dolgozószám"></param>
        /// <param name="belépésiidő"></param>
        /// <param name="munkakör"></param>
        /// <param name="kilépésiidő"></param>
        public Adat_Dolgozó_Alap(string dolgozóNév, string dolgozószám, DateTime belépésiidő, string munkakör, DateTime kilépésiidő)
        {
            Munkakör = munkakör;
            Kilépésiidő = kilépésiidő;
            DolgozóNév = dolgozóNév;
            Dolgozószám = dolgozószám;
            Belépésiidő = belépésiidő;
        }
    }

    public class Adat_Dolgozó_Telephely
    {
        public Adat_Dolgozó_Alap Dolgozó { get; private set; }
        public string Telephely { get; private set; }

        public Adat_Dolgozó_Telephely(Adat_Dolgozó_Alap dolgozó, string telephely)
        {
            Dolgozó = dolgozó;
            Telephely = telephely;
        }
    }



    public class Adat_Dolgozó_Beosztás
    {
        public int Nap { get; private set; }
        public string Beosztáskód { get; private set; }
        public int Ledolgozott { get; private set; }
        public int Túlóra { get; private set; }
        public DateTime Túlórakezd { get; private set; }
        public DateTime Túlóravég { get; private set; }
        public int Csúszóra { get; private set; }
        public DateTime CSúszórakezd { get; private set; }
        public DateTime Csúszóravég { get; private set; }
        public string Megjegyzés { get; private set; }
        public string Túlóraok { get; private set; }
        public string Szabiok { get; private set; }
        public bool Kért { get; private set; }
        public string Csúszok { get; private set; }
        public int AFTóra { get; private set; }
        public string AFTok { get; private set; }

        public Adat_Dolgozó_Beosztás(int nap, string beosztáskód, int ledolgozott, int túlóra, DateTime túlórakezd, DateTime túlóravég, int csúszóra, DateTime cSúszórakezd, DateTime csúszóravég, string megjegyzés, string túlóraok, string szabiok, bool kért, string csúszok, int aFTóra, string aFTok)
        {
            Nap = nap;
            Beosztáskód = beosztáskód;
            Ledolgozott = ledolgozott;
            Túlóra = túlóra;
            Túlórakezd = túlórakezd;
            Túlóravég = túlóravég;
            Csúszóra = csúszóra;
            CSúszórakezd = cSúszórakezd;
            Csúszóravég = csúszóravég;
            Megjegyzés = megjegyzés;
            Túlóraok = túlóraok;
            Szabiok = szabiok;
            Kért = kért;
            Csúszok = csúszok;
            AFTóra = aFTóra;
            AFTok = aFTok;
        }
    }




    public class Adat_Dolgozó_Személyes
    {

        public string Anyja { get; private set; }
        public string Dolgozószám { get; private set; }
        public string Ideiglenescím { get; private set; }
        public string Lakcím { get; private set; }
        public string Leánykori { get; private set; }
        public string Születésihely { get; private set; }

        public DateTime Születésiidő { get; private set; }
        public string Telefonszám1 { get; private set; }
        public string Telefonszám2 { get; private set; }
        public string Telefonszám3 { get; private set; }

        public Adat_Dolgozó_Személyes(string anyja, string dolgozószám, string ideiglenescím, string lakcím, string leánykori, string születésihely, DateTime születésiidő, string telefonszám1, string telefonszám2, string telefonszám3)
        {
            Anyja = anyja;
            Dolgozószám = dolgozószám;
            Ideiglenescím = ideiglenescím;
            Lakcím = lakcím;
            Leánykori = leánykori;
            Születésihely = születésihely;
            Születésiidő = születésiidő;
            Telefonszám1 = telefonszám1;
            Telefonszám2 = telefonszám2;
            Telefonszám3 = telefonszám3;
        }
    }


    public class Adat_Szatube_Szabadság
    {
        public double Sorszám { get; private set; }
        public string Törzsszám { get; private set; }
        public string Dolgozónév { get; private set; }
        public DateTime Kezdődátum { get; private set; }
        public DateTime Befejeződátum { get; private set; }
        public int Kivettnap { get; private set; }
        public string Szabiok { get; private set; }
        public int Státus { get; private set; }
        public string Rögzítette { get; private set; }
        public DateTime Rögzítésdátum { get; private set; }

        public Adat_Szatube_Szabadság(double sorszám, string törzsszám, string dolgozónév, DateTime kezdődátum, DateTime befejeződátum, int kivettnap, string szabiok, int státus, string rögzítette, DateTime rögzítésdátum)
        {
            Sorszám = sorszám;
            Törzsszám = törzsszám;
            Dolgozónév = dolgozónév;
            Kezdődátum = kezdődátum;
            Befejeződátum = befejeződátum;
            Kivettnap = kivettnap;
            Szabiok = szabiok;
            Státus = státus;
            Rögzítette = rögzítette;
            Rögzítésdátum = rögzítésdátum;
        }
    }

    public class Adat_Szatube_Beteg
    {
        public double Sorszám { get; private set; }
        public string Törzsszám { get; private set; }
        public string Dolgozónév { get; private set; }
        public DateTime Kezdődátum { get; private set; }
        public DateTime Befejeződátum { get; private set; }
        public int Kivettnap { get; private set; }
        public string Szabiok { get; private set; }
        public int Státus { get; private set; }
        public string Rögzítette { get; private set; }
        public DateTime Rögzítésdátum { get; private set; }

        public Adat_Szatube_Beteg(double sorszám, string törzsszám, string dolgozónév, DateTime kezdődátum, DateTime befejeződátum, int kivettnap, string szabiok, int státus, string rögzítette, DateTime rögzítésdátum)
        {
            Sorszám = sorszám;
            Törzsszám = törzsszám;
            Dolgozónév = dolgozónév;
            Kezdődátum = kezdődátum;
            Befejeződátum = befejeződátum;
            Kivettnap = kivettnap;
            Szabiok = szabiok;
            Státus = státus;
            Rögzítette = rögzítette;
            Rögzítésdátum = rögzítésdátum;
        }
    }


    public class Adat_Dolgozó_Beosztás_Napló
    {

        public double Sorszám { get; private set; }
        public DateTime Dátum { get; private set; }
        public string Beosztáskód { get; private set; }
        public int Túlóra { get; private set; }
        public DateTime Túlórakezd { get; private set; }
        public DateTime Túlóravég { get; private set; }
        public int Csúszóra { get; private set; }
        public DateTime CSúszórakezd { get; private set; }
        public DateTime Csúszóravég { get; private set; }
        public string Megjegyzés { get; private set; }
        public string Túlóraok { get; private set; }
        public string Szabiok { get; private set; }
        public bool Kért { get; private set; }
        public string Csúszok { get; private set; }
        public string Rögzítette { get; private set; }
        public DateTime Rögzítésdátum { get; private set; }
        public string Dolgozónév { get; private set; }
        public string Törzsszám { get; private set; }
        public int AFTóra { get; private set; }
        public string AFTok { get; private set; }

        public Adat_Dolgozó_Beosztás_Napló(double sorszám, DateTime dátum, string beosztáskód, int túlóra, DateTime túlórakezd, DateTime túlóravég, int csúszóra, DateTime cSúszórakezd, DateTime csúszóravég, string megjegyzés, string túlóraok, string szabiok, bool kért, string csúszok, string rögzítette, DateTime rögzítésdátum, string dolgozónév, string törzsszám, int aFTóra, string aFTok)
        {
            Sorszám = sorszám;
            Dátum = dátum;
            Beosztáskód = beosztáskód;
            Túlóra = túlóra;
            Túlórakezd = túlórakezd;
            Túlóravég = túlóravég;
            Csúszóra = csúszóra;
            CSúszórakezd = cSúszórakezd;
            Csúszóravég = csúszóravég;
            Megjegyzés = megjegyzés;
            Túlóraok = túlóraok;
            Szabiok = szabiok;
            Kért = kért;
            Csúszok = csúszok;
            Rögzítette = rögzítette;
            Rögzítésdátum = rögzítésdátum;
            Dolgozónév = dolgozónév;
            Törzsszám = törzsszám;
            AFTóra = aFTóra;
            AFTok = aFTok;
        }
    }


}
