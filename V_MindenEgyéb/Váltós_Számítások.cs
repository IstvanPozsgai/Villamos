using System;


public partial class Függvénygyűjtemény
{

    /// <summary>
    /// Bekéri a váltós beosztás kezdő dátumát, kivánt napot, és a ciklus hosszát
    /// Visszaadja, hog melyik napnál tart a ciklusban 
    /// </summary>
    /// <param name="Kezdődátum"></param>
    /// <param name="AktuálisDátum"></param>
    /// <returns></returns>
    public static int Váltónap(DateTime Kezdődátum, DateTime AktuálisDátum, int CiklusHossz)
    {
        double ciklus = (double)CiklusHossz;
        double hanyadik = (AktuálisDátum - Kezdődátum).Days;

        double maradék = hanyadik % ciklus;
        int Érték = (int)(maradék) + 1;

        return Érték;

    }
}

