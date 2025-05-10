using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Forms;
using Villamos;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;


public static partial class Függvénygyűjtemény
{
    readonly static Kezelő_Alap_Beolvasás KézBeolvasás = new Kezelő_Alap_Beolvasás();
    readonly static Kezelő_Excel_Beolvasás Kéz_Beolvasás = new Kezelő_Excel_Beolvasás();
    /// <summary>
    /// Az Excel tábla fejlécét hasonlítja össze a táblázatban letárolt értékekkel.
    /// </summary>
    /// <param name="Melyik"></param>
    /// <param name="Fejlécsor"></param>
    /// <returns></returns>
    public static bool Betöltéshelyes(string Melyik, string Fejlécsor)
    {
        bool válasz = false;
        try
        {
            List<Adat_Alap_Beolvasás> Adatok = KézBeolvasás.Lista_Adatok();
            Adatok = (from a in Adatok
                      where a.Csoport == Melyik.Trim()
                      && a.Törölt == "0"
                      orderby a.Oszlop
                      select a).ToList();

            string szöveg = "";
            foreach (Adat_Alap_Beolvasás rekord in Adatok)
                szöveg += rekord.Fejléc;

            if (szöveg.Trim() == Fejlécsor.Trim()) válasz = true;
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "Függvénygyűjtemény - Betöltéshelyes", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return válasz;
    }

    //Elkopó
    /// <summary>
    /// Az Excel táblát kapja és a fejlécét hasonlítja össze a táblázatban letárolt értékekkel.
    /// </summary>
    /// <param name="Melyik"></param>
    /// <param name="Fejlécsor"></param>
    /// <returns></returns>
    public static bool Betöltéshelyes(string Melyik, DataTable Tábla)
    {
        bool válasz = false;
        try
        {
            //  beolvassuk a fejlécet ha eltér a megadotttól, akkor kiírja és bezárja
            string fejlécbeolvas = "";
            for (int i = 0; i < Tábla.Columns.Count; i++)
                fejlécbeolvas += Tábla.Columns[i].ColumnName.ToStrTrim();


            List<Adat_Alap_Beolvasás> Adatok = KézBeolvasás.Lista_Adatok();
            Adatok = (from a in Adatok
                      where a.Csoport == Melyik.Trim()
                      && a.Törölt == "0"
                      orderby a.Oszlop
                      select a).ToList();

            string szöveg = "";
            foreach (Adat_Alap_Beolvasás rekord in Adatok)
                szöveg += rekord.Fejléc;

            if (szöveg.Trim() == fejlécbeolvas.Trim()) válasz = true;
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "Függvénygyűjtemény - Betöltéshelyes", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return válasz;
    }

    public static bool BetöltésHelyes(string Melyik, DataTable Tábla)
    {
        bool válasz = false;
        try
        {
            //  beolvassuk a fejlécet ha eltér a megadotttól, akkor kiírja és bezárja
            string fejlécbeolvas = "";
            for (int i = 0; i < Tábla.Columns.Count; i++)
                fejlécbeolvas += Tábla.Columns[i].ColumnName.ToStrTrim();


            List<Adat_Excel_Beolvasás> Adatok = Kéz_Beolvasás.Lista_Adatok();
            Adatok = (from a in Adatok
                      where a.Csoport == Melyik.Trim()
                      && a.Státusz == false
                      orderby a.Oszlop
                      select a).ToList();

            string szöveg = "";
            foreach (Adat_Excel_Beolvasás rekord in Adatok)
                szöveg += rekord.Fejléc;

            if (szöveg.Trim() == fejlécbeolvas.Trim()) válasz = true;
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "Függvénygyűjtemény - Betöltéshelyes", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return válasz;
    }

    #region Jelszó kódolások

    public static string Rövidkód(string jelszó)
    {
        int hossz = jelszó.Length;
        string újszó;
        újszó = ((char)(122 - hossz)).ToString();
        for (int i = 1, loopTo = hossz; i <= loopTo; i++)
            újszó += ((char)(65 + Convert.ToInt32(jelszó.Substring(i - 1, 1)) - i)).ToString();
        string ford = "";
        for (int i = újszó.Length; i >= 1; i -= 1)
            ford += újszó.Substring(i - 1, 1);
        return ford;
    }

    public static string Kódol(string jelszó)
    {
        Random rnd = new Random(100);
        int hossz = jelszó.Length;
        string újszó = "";
        string eredmény;
        int érték;
        újszó += ((char)(Convert.ToInt32(rnd.Next(1, 11)) + 97)).ToString();
        újszó += ((char)(Convert.ToInt32(rnd.Next(1, 11)) + 97)).ToString();
        újszó += ((char)(122 - hossz)).ToString();
        for (int i = 1; i <= hossz; i++)
        {
            eredmény = jelszó.Substring(i - 1, 1);
            érték = Convert.ToInt32(eredmény);
            újszó += ((char)(65 + érték - i)).ToString();
        }
        // kitölti 20 karakterig
        for (int i = újszó.Length; i <= 19; i++)
            újszó += ((char)(Convert.ToInt32(rnd.Next(1, 11)) + 97)).ToString();
        string ford = "";
        for (int i = 20; i >= 1; i -= 1)
            ford += újszó.Substring(i - 1, 1);
        return ford;
    }

    public static string Dekódolja(string áljelszó)
    {
        string ford = "";
        int hossz;
        string újjelszó = "";
        if (áljelszó.Length > 0)
        {
            for (int i = 20; i >= 1; i -= 1)
                ford += áljelszó.Substring(i - 1, 1);

            hossz = 122 - ASC(ford.Substring(2, 1));

            for (int i = 1; i <= hossz; i++)
            {
                int ideig = ASC(ford.Substring(i + 2, 1)) + i - 65;

                újjelszó += ideig.ToString();

            }
        }
        return újjelszó.Replace(@"\u000", "");
    }

    public static int ASC(string Betű)
    {
        //Visszaadjuk a betű ascii kódját miután karakterré alakítottuk
        char valami = Convert.ToChar(Betű);
        return (int)valami;
    }


    public static string MÁSKódol(string jelszó)
    {
        Random rnd = new Random();
        int hossz = jelszó.Length;
        jelszó = jelszó.ToUpper();
        string újszó = "";
        újszó += ((char)(Convert.ToInt32(rnd.Next(100) * 10) + 47)).ToString(); //kamu
        újszó += ((char)(Convert.ToInt32(rnd.Next(100) * 10) + 97)).ToString(); //kamu
        újszó += ((char)(122 - hossz)).ToString();
        for (int i = 1; i <= hossz; i++)
        {
            string betű = jelszó.Substring(i - 1, 1);
            int ss = char.Parse(betű);
            string újbetű = ((char)(ss + i - 15)).ToString();
            int újss = char.Parse(újbetű);
            újszó += újbetű;
        }
        // kitölti 25 karakterig
        for (int i = újszó.Length; i <= 24; i++)
            újszó += ((char)(int)Convert.ToInt32(rnd.Next(100) * 10) + 97).ToString();
        string ford = "";
        for (int i = 25; i >= 1; i -= 1)
            ford += újszó.Substring(i - 1, 1);
        return ford;
    }

    public static string MÁSDekódolja(string áljelszó)
    {

        string ford = "";
        int hossz;
        string újjelszó = "";
        string betű;

        if (áljelszó.Length > 0)
        {
            for (int i = 25; i >= 1; i -= 1)
                ford += áljelszó.Substring(i - 1, 1);
            hossz = 122 - Convert.ToChar(ford.Substring(2, 1));

            for (int i = 1; i <= hossz; i++)
            {
                betű = ford.Substring(i + 2, 1);
                char Betűkar = char.Parse(betű);
                újjelszó += ((char)(char.Parse(betű) - i + 15)).ToString();
            }
        }

        return újjelszó;
    }

    public static string MÁSRövidkód(string jelszó)
    {
        Random rnd = new Random();
        int hossz = jelszó.Length;
        jelszó = jelszó.ToUpper();
        string újszó = "";

        újszó += ((char)(Convert.ToInt32(rnd.Next(100) * 10) + 97)).ToString();
        újszó += ((char)(Convert.ToInt32(rnd.Next(100) * 10) + 97)).ToString();
        újszó += ((char)(122 - hossz)).ToString();

        for (int i = 1, loopTo = hossz; i <= loopTo; i++)
        {
            char egybetű = Convert.ToChar(jelszó.Substring(i - 1, 1));
            int Betű = Convert.ToInt32(egybetű);
            újszó += ((char)(Betű + i - 15)).ToString();
        }
        string ford = "";
        for (int i = újszó.Length; i >= 1; i -= 1)
        {
            ford += újszó.Substring(i - 1, 1);
        }
        return ford;
    }

    #endregion


    #region Üres rekord Vizsgálatok
    public static string Vizsgálat(OleDbDataReader ReKORD, string cím)
    {
        string iszöveg = "_";
        if (!(ReKORD[cím] is DBNull))
            iszöveg = Convert.ToString(ReKORD[cím]);
        return iszöveg;
    }

    public static DateTime Vizsgálatdátum(OleDbDataReader ReKORD, string cím)
    {
        var idátum = Convert.ToDateTime("1900.01.01");
        if (!(ReKORD[cím] is DBNull))
            idátum = Convert.ToDateTime(ReKORD[cím]);
        return idátum;
    }

    public static bool Vizsgálatigaz(OleDbDataReader ReKORD, string cím)
    {
        bool iigaz = false;
        if (!(ReKORD[cím] is DBNull))
            iigaz = Convert.ToBoolean(ReKORD[cím]);
        return iigaz;
    }

    public static double Vizsgálatszám(OleDbDataReader ReKORD, string cím)
    {
        double iszám = 0d;
        if (!(ReKORD[cím] is DBNull))
            iszám = Convert.ToDouble(ReKORD[cím]);
        return iszám;
    }

    #endregion

    public static AdatCombohoz[] ComboFeltöltés(string sqlhely, string sqljelszó, string sqlszöveg, string rekordoszlop)
    {

        string kapcsolatiszöveg = "Provider=Microsoft.Jet.OleDb.4.0;Data Source= '" + sqlhely + "'; Jet Oledb:Database Password=" + sqljelszó;
        OleDbConnection Kapcsolat = new OleDbConnection(kapcsolatiszöveg);
        OleDbCommand Parancs = new OleDbCommand(sqlszöveg, Kapcsolat);

        int sorszám = 0;

        // elemek száma
        Kapcsolat.Open();
        OleDbDataReader rekord = Parancs.ExecuteReader();
        if (rekord.HasRows)
        {
            while (rekord.Read())
                sorszám += 1;
        }
        Kapcsolat.Close();

        // 0 elemmel kezdődik
        var Combo_lista = new AdatCombohoz[sorszám];
        Kapcsolat.Open();
        rekord = Parancs.ExecuteReader();
        sorszám = 0;
        if (rekord.HasRows)
        {
            while (rekord.Read())
            {
                Combo_lista[sorszám] = new AdatCombohoz(rekord[rekordoszlop].ToString().Trim(), sorszám);
                sorszám += 1;
            }
        }

        rekord.Close();
        Parancs.Dispose();
        Kapcsolat.Close();
        return Combo_lista;
    }

    public static long Futás_km(string azonosító, DateTime dátum_érték)
    {
        long mennyi = 0;
        Kezelő_Főkönyv_Zser_Km KézZser = new Kezelő_Főkönyv_Zser_Km();
        List<Adat_Főkönyv_Zser_Km> AdatokZser = new List<Adat_Főkönyv_Zser_Km>();
        // ha volt előző év is
        if (dátum_érték.Year != DateTime.Today.Year)
        {
            AdatokZser = KézZser.Lista_adatok(dátum_érték.Year);
            // ha volt előző évben
            if (AdatokZser != null && AdatokZser.Count > 0)
            {
                List<Adat_Főkönyv_Zser_Km> SzűrtAdatok = (from a in AdatokZser
                                                          where a.Azonosító == azonosító.Trim() &&
                                                          a.Dátum > dátum_érték
                                                          select a).ToList();
                if (SzűrtAdatok != null) mennyi = SzűrtAdatok.Sum(a => a.Napikm);
            }
        }
        AdatokZser.Clear();

        // aktuális év
        AdatokZser = KézZser.Lista_adatok(DateTime.Today.Year);
        if (AdatokZser != null && AdatokZser.Count > 0)
        {
            List<Adat_Főkönyv_Zser_Km> SzűrtAdatok = (from a in AdatokZser
                                                      where a.Azonosító == azonosító.Trim() &&
                                                      a.Dátum >= dátum_érték
                                                      select a).ToList();
            if (SzűrtAdatok != null) mennyi += SzűrtAdatok.Sum(a => a.Napikm);
        }
        return mennyi;
    }



    #region Jogosultság

    public static bool Vanjoga(int melyikelem, int csoport)
    {
        bool válasz;
        switch (csoport)
        {
            case 1: //1 -es csoport
                {
                    if (Program.PostásJogkör.Substring(melyikelem - 1, 1) == "3" ||
                        Program.PostásJogkör.Substring(melyikelem - 1, 1) == "7" ||
                        Program.PostásJogkör.Substring(melyikelem - 1, 1) == "b" ||
                        Program.PostásJogkör.Substring(melyikelem - 1, 1) == "f")
                    { válasz = true; }
                    else
                    { válasz = false; }
                    break;
                }
            case 2: // 2-es csoport
                {
                    if (Program.PostásJogkör.Substring(melyikelem - 1, 1) == "5" ||
                              Program.PostásJogkör.Substring(melyikelem - 1, 1) == "7" ||
                              Program.PostásJogkör.Substring(melyikelem - 1, 1) == "d" ||
                              Program.PostásJogkör.Substring(melyikelem - 1, 1) == "f")
                    { válasz = true; }
                    else
                    { válasz = false; }
                    break;
                }
            default: //3-as csoport
                {
                    if (Program.PostásJogkör.Substring(melyikelem - 1, 1) == "9" ||
                          Program.PostásJogkör.Substring(melyikelem - 1, 1) == "b" ||
                          Program.PostásJogkör.Substring(melyikelem - 1, 1) == "d" ||
                          Program.PostásJogkör.Substring(melyikelem - 1, 1) == "f")

                    { válasz = true; }
                    else
                    { válasz = false; }
                    break;
                }
        }
        return válasz;

    }



    #endregion
}



public partial class AdatCombohoz
{
    private int Data;
    private string Name;
    public AdatCombohoz(string NameArgument, int Value)
    {
        Name = NameArgument;
        Data = Value;
    }

    public override string ToString()
    {
        return Convert.ToString(Name);
    }

    public int GetData()
    {
        return Data;
    }
}

