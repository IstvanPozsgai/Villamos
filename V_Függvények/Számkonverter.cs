public static partial class Függvénygyűjtemény
{
    public static string Számszóban(long szám)
    {
        string ki = "";
        string Számszövegként = szám.ToString();

        if (Számszövegként.Length > 9)
            ki = "Túl hosszú a szám!";
        else
        {
            int i = 1; // szamjegyszámláló
            int elozo = 0; // előző számjegy
            while (Számszövegként.Length > 0)
            {
                int e = int.Parse(Számszövegként.Substring(Számszövegként.Length - 1, 1));
                if (i == 7) ki = "millió-" + ki;
                if (i == 4) ki = "ezer-" + ki;
                if (i % 3 == 1) ki = Egyes(e) + ki;
                if (i % 3 == 2)
                {
                    if (elozo == 0) ki = Tizes2(e) + ki;
                    else ki = Tizes(e) + ki;
                }
                if (i % 3 == 0)
                    ki = Szazas(e) + ki;
                i++;
                elozo = e;
                Számszövegként = Számszövegként.Substring(0, Számszövegként.Length - 1);
            }
            if (szám % 1000000 == 0) ki = ki.Substring(0, ki.Length - 5);  // ezer- le
            if (ki.IndexOf("-ezer-") > -1)
            {
                ki = ki.Substring(0, ki.IndexOf("-ezer-") + 1) + ki.Substring(ki.IndexOf("-ezer-") + 6, ki.Length);
            }
            if (ki.Substring (ki.Length - 1) == "-") ki = ki.Substring(0, ki.Length - 1); // milliós - le

        }
        return ki;
    }
    private static string Szazas(int x)
    {
        switch (x)
        {
            case 0: return "";
            case 1: return "egyszáz";
            case 2: return "kettőszáz";
            case 3: return "háromszáz";
            case 4: return "négyszáz";
            case 5: return "ötszáz";
            case 6: return "hatszáz";
            case 7: return "hétszáz";
            case 8: return "nyolcszáz";
            case 9: return "kilencszáz";
            default: return "";
        }
    }

    private static string Tizes(int x)
    {
        switch (x)
        {
            case 0: return "";
            case 1: return "tizen";
            case 2: return "huszon";
            case 3: return "harminc";
            case 4: return "negyven";
            case 5: return "ötven";
            case 6: return "hatvan";
            case 7: return "hetven";
            case 8: return "nyolcvan";
            case 9: return "kilencven";
            default: return "";
        }
    }

    private static string Tizes2(int x)
    {
        switch (x)
        {
            case 0: return "";
            case 1: return "tíz";
            case 2: return "húsz";
            case 3: return "harminc";
            case 4: return "negyven";
            case 5: return "ötven";
            case 6: return "hatvan";
            case 7: return "hetven";
            case 8: return "nyolcvan";
            case 9: return "kilencven";
            default: return "";
        }
    }

    private static string Egyes(int x)
    {
        switch (x)
        {
            case 0: return "";
            case 1: return "egy";
            case 2: return "kettő";
            case 3: return "három";
            case 4: return "négy";
            case 5: return "öt";
            case 6: return "hat";
            case 7: return "hét";
            case 8: return "nyolc";
            case 9: return "kilenc";
            default: return "";
        }
    }

}

