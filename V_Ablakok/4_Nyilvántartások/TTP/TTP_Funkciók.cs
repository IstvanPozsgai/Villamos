using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using Villamos.Kezelők;
using MyEn = Villamos.V_MindenEgyéb.Enumok;
using MyF = Függvénygyűjtemény;


public static partial class Függvénygyűjtemény
{


    readonly static string hely = $@"{Application.StartupPath}/Főmérnökség/adatok/TTP/TTP_Adatbázis.mdb";
    readonly static string jelszó = "rudolfg";

    public static List<Adat_TTP_Alapadat> TTP_AlapadatFeltölt()
    {
        List<Adat_TTP_Alapadat> AdatokAlap = new List<Adat_TTP_Alapadat>();
        try
        {
            Kezelő_TTP_Alapadat KézAlap = new Kezelő_TTP_Alapadat();

            string szöveg = "SELECT * FROM TTP_Alapadat";
            AdatokAlap = KézAlap.Lista_Adatok(hely, jelszó, szöveg);
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "TTP_AlapadatFeltölt", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return AdatokAlap;
    }

    public static List<Adat_TTP_Év> TTP_ÉvFeltölt()
    {
        Kezelő_TTP_Év KézÉv = new Kezelő_TTP_Év();
        List<Adat_TTP_Év> AdatokÉv = new List<Adat_TTP_Év>();
        try
        {
            string szöveg = "SELECT * FROM TTP_Év  ORDER BY Életkor";
            AdatokÉv = KézÉv.Lista_Adatok(hely, jelszó, szöveg);
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "TTP_ÉvFeltölt", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return AdatokÉv;
    }

    public static List<Adat_TTP_Tábla> TTP_TáblaFeltölt()
    {
        Kezelő_TTP_Tábla KézTábla = new Kezelő_TTP_Tábla();
        List<Adat_TTP_Tábla> AdatokTábla = new List<Adat_TTP_Tábla>();
        try
        {
            string szöveg = "SELECT * FROM TTP_Tábla";
            AdatokTábla = KézTábla.Lista_Adatok(hely, jelszó, szöveg);
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, " TTP_TáblaFeltölt", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return AdatokTábla;
    }

    public static List<Adat_TTP_Naptár> TTP_NaptárFeltölt(DateTime Dátum)
    {
        Kezelő_TTP_Naptár KézNaptár = new Kezelő_TTP_Naptár();
        List<Adat_TTP_Naptár> AdatokNaptár = new List<Adat_TTP_Naptár>();
        try
        {
            string szöveg = "SELECT * FROM TTP_Naptár";
            szöveg += $" WHERE dátum>=#{MyF.Év_elsőnapja(Dátum):M-d-yy}# ";
            szöveg += $" AND dátum<=#{MyF.Év_utolsónapja(Dátum):M-d-yy}# ORDER BY dátum";

            AdatokNaptár = KézNaptár.Lista_Adatok(hely, jelszó, szöveg);
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, " TTP_NaptárFeltölt", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return AdatokNaptár;
    }

    public static DataTable TTP_VezénylésFeltölt(List<Adat_Kiegészítő_Sérülés> AdatokTelep, List<Adat_Jármű_hiba> AdatokHiba, DateTime Dátum, bool Kötelező)
    {
        DataTable AdatTábla = new DataTable();
        try
        {
            List<Adat_Tábla_Vezénylés> Vezénylés = TTP_VezénylésLista(AdatokTelep, AdatokHiba, Dátum, Kötelező);

            //Tábla mezőnevek
            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Pályaszám");
            AdatTábla.Columns.Add("Lejárat dátum", typeof(DateTime));
            AdatTábla.Columns.Add("Ütemezés dátum", typeof(DateTime));
            AdatTábla.Columns.Add("Típus");
            AdatTábla.Columns.Add("Telephely");
            AdatTábla.Columns.Add("TTP Kötelezés");
            AdatTábla.Columns.Add("Megjegyzés");
            AdatTábla.Columns.Add("Utolsó TTP dátum", typeof(DateTime));
            AdatTábla.Columns.Add("Státus");
            AdatTábla.Columns.Add("Jármű hiba");
            AdatTábla.Columns.Add("Jármű státusz");

            foreach (Adat_Tábla_Vezénylés rekord in Vezénylés)
            {
                DataRow Soradat = AdatTábla.NewRow();
                Soradat["Pályaszám"] = rekord.Azonosító;
                Soradat["Lejárat dátum"] = rekord.Le_Dátum;
                Soradat["Ütemezés dátum"] = rekord.Ütem_Dátum;
                Soradat["Jármű hiba"] = rekord.Hiba;
                Soradat["Jármű státusz"] = Enum.GetName(typeof(MyEn.Jármű_Státus), rekord.Kocsistátus);
                Soradat["Típus"] = rekord.Típus;
                Soradat["Telephely"] = rekord.Telephely;
                Soradat["TTP Kötelezés"] = rekord.TTP_Kötelezett;
                Soradat["Megjegyzés"] = rekord.Megjegyzés;
                Soradat["Utolsó TTP dátum"] = rekord.Utolsó_Dátum;
                Soradat["Státus"] = Enum.GetName(typeof(MyEn.TTP_Státus), rekord.Státus);
                AdatTábla.Rows.Add(Soradat);
            }
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "TTP_VezénylésFeltölt", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return AdatTábla;
    }

    public static List<Adat_Tábla_Vezénylés> TTP_VezénylésLista(List<Adat_Kiegészítő_Sérülés> AdatokTelep, List<Adat_Jármű_hiba> AdatokHiba, DateTime Dátum, bool Kötelező)
    {
        List<Adat_Tábla_Vezénylés> Adatok = new List<Adat_Tábla_Vezénylés>();

        List<Adat_TTP_Alapadat> AlapAdat = TTP_AlapadatFeltölt();
        List<Adat_TTP_Tábla> TáblaAdat = TTP_TáblaFeltölt();
        List<Adat_TTP_Év> TáblaÉv = TTP_ÉvFeltölt();
        List<Adat_TTP_Naptár> TáblaNaptár = TTP_NaptárFeltölt(Dátum);
        List<Adat_TTP_Tábla> AdatokTeljes = TTP_Tábla_Lista_Feltöltés();
        List<Adat_Jármű> AdatokJármű = TeljesJárműadatok(AdatokTelep);

        foreach (Adat_Jármű rekord in AdatokJármű)
        {
            DateTime lejár = new DateTime(1900, 1, 1);
            DateTime utolsó = new DateTime(1900, 1, 1);
            DateTime ütemezés = new DateTime(1900, 1, 1);
            int státus = 0;

            List<Adat_Jármű_hiba> elemek = (from a in AdatokHiba
                                            where a.Azonosító == rekord.Azonosító
                                            orderby a.Korlát descending
                                            select a).ToList();
            string hiba = "";
            foreach (Adat_Jármű_hiba rek in elemek)
            {
                hiba += rek.Hibaleírása;
            }

            Adat_TTP_Alapadat Elemek2 = (from a in AlapAdat
                                         where a.Azonosító == rekord.Azonosító
                                         select a).FirstOrDefault();

            string TTP_Kötelezett = "Nem";
            if (Elemek2 != null)
                if (Elemek2.TTP)
                    TTP_Kötelezett = "Igen";

            string Megjegyzés = "";
            if (Elemek2 != null) Megjegyzés = Elemek2.Megjegyzés;

            Adat_TTP_Tábla Elemek5 = (from a in TáblaAdat
                                      where a.Azonosító == rekord.Azonosító
                                      orderby a.Ütemezés_Dátum descending
                                      select a).FirstOrDefault();

            if (Elemek5 != null)
            {
                státus = Elemek5.Státus;
                if (Elemek5.Megjegyzés.Trim() != "") Megjegyzés += $" - {Elemek5.Megjegyzés.Trim()}";
            }

            Adat_TTP_Tábla Elemek3 = (from a in TáblaAdat
                                      where a.Azonosító == rekord.Azonosító
                                      orderby a.TTP_Dátum descending
                                      select a).FirstOrDefault();

            if (Elemek3 != null)
            {
                utolsó = Elemek3.TTP_Dátum;


                Adat_TTP_Alapadat Gyártása = (from a in AlapAdat
                                              where a.Azonosító == rekord.Azonosító && a.Gyártási_Év != new DateTime(1900, 1, 1, 0, 0, 0)
                                              select a).FirstOrDefault();
                if (Gyártása != null)
                {

                    int kor = utolsó.Year - Gyártása.Gyártási_Év.Year;

                    Adat_TTP_Év növekszik = (from a in TáblaÉv
                                             where a.Életkor == kor
                                             select a).FirstOrDefault();

                    if (növekszik != null) lejár = Elemek3.TTP_Dátum.AddYears(növekszik.Év);
                }
            }
            Adat_TTP_Tábla Elem4 = (from a in AdatokTeljes
                                    where a.Ütemezés_Dátum.Year == Dátum.Year && a.Együtt.Contains(rekord.Azonosító)
                                    select a).FirstOrDefault();
            if (Elem4 != null) ütemezés = Elem4.Ütemezés_Dátum;


            Adat_Tábla_Vezénylés Adat = new Adat_Tábla_Vezénylés(
                                                rekord.Azonosító,                           //azonosító
                                                lejár,                                      //lejárat dátuma
                                                ütemezés,                                   //ütemezés dátum
                                                Szöveg_Tisztítás(hiba, 0, 255, true),       // Kocsi hibái
                                                rekord.Státus,                              //kocsi státus
                                                rekord.Típus,                               //kocsi típusa
                                                rekord.Üzem,                                //telephely
                                                TTP_Kötelezett,                             // TTP kötelezett
                                                Szöveg_Tisztítás(Megjegyzés, 0, 255, true), //Megjegyzés
                                                utolsó,                                     //utolsó ttp dátuma
                                                státus                                      //ttp státusa 
                                                );
            //Ha kötelező akkor csak azokat tesszük bele akik kötelezettek
            if (Kötelező)
            {
                if (TTP_Kötelezett == "Igen")
                    Adatok.Add(Adat);
            }
            else
                Adatok.Add(Adat);
        }
        Adatok.OrderBy(a => a.Azonosító);
        return Adatok;
    }

    private static List<Adat_Jármű> TeljesJárműadatok(List<Adat_Kiegészítő_Sérülés> AdatokTelep)
    {
        List<Adat_Jármű> Adatok = new List<Adat_Jármű>(); ;
        try
        {
            string jelszó = "pozsgaii";
            string szöveg = "SELECT * FROM állománytábla ORDER BY Azonosító ";
            Kezelő_Jármű KézJármű = new Kezelő_Jármű();
            foreach (Adat_Kiegészítő_Sérülés rekord in AdatokTelep)
            {
                string hely = $@"{Application.StartupPath}\{rekord.Név}\Adatok\villamos\Villamos.mdb";
                if (File.Exists(hely))
                {
                    List<Adat_Jármű> Ideig = KézJármű.Lista_Adatok(hely, jelszó, szöveg);
                    Adatok.AddRange(Ideig);
                }
            }
            Adatok = (from a in Adatok
                      orderby a.Azonosító
                      select a).ToList();
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "TeljesJárműadatok", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return Adatok;
    }

    public static List<Adat_TTP_Tábla> TTP_Tábla_Lista_Feltöltés()
    {
        List<Adat_TTP_Tábla> Adatok = new List<Adat_TTP_Tábla>();
        try
        {
            Kezelő_TTP_Tábla KézTábla = new Kezelő_TTP_Tábla();
            string szöveg = "SELECT * FROM TTP_tábla";
            Adatok = KézTábla.Lista_Adatok(hely, jelszó, szöveg);

        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "TTP_Tábla_Feltöltés", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return Adatok;
    }

    public static bool VanPDF(string Pályaszám, DateTime Dátum)
    {
        bool válasz = false;
        try
        {
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\TTP\PDF\";
            DirectoryInfo Directories = new DirectoryInfo(hely);
            string mialapján = $@"{Pályaszám}_{Dátum:yyyy}*.pdf";
            FileInfo[] fileInfo = Directories.GetFiles(mialapján, SearchOption.TopDirectoryOnly);
            if (fileInfo.Length > 0) válasz = true;
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "VanPDF", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return válasz;
    }

    /// <summary>
    /// Visszaadja, hogy a könyvtárban melyik az utolsó képnek a száma
    /// </summary>
    /// <param name="Pályaszám"></param>
    /// <param name="Dátum"></param>
    /// <returns></returns>
    public static int VanPDFdb(string Pályaszám, DateTime Dátum)
    {
        int válasz = 0;
        try
        {
            string hely = $@"{Application.StartupPath}\Főmérnökség\adatok\TTP\PDF\";
            DirectoryInfo Directories = new DirectoryInfo(hely);
            string mialapján = $@"{Pályaszám}_{Dátum:yyyy}*.pdf";

            FileInfo[] fileInfo = Directories.GetFiles(mialapján, SearchOption.TopDirectoryOnly);
            int ideig = 0;
            for (int i = 0; i < fileInfo.Length; i++)
            {
                string[] darabol = fileInfo[i].Name.Split('_');
                if (darabol.Length == 3)
                {
                    string[] tovább = darabol[darabol.Length - 1].Split('.');
                    if (!int.TryParse(tovább[0], out ideig)) ideig = 0;
                }
                if (válasz < ideig) válasz = ideig;
            }
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "VanPDFdb", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return válasz;
    }
}
