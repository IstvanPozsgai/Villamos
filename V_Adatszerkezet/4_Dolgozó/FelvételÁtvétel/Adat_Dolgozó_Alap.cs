using System;

namespace Villamos.Adatszerkezet
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

    }

}
