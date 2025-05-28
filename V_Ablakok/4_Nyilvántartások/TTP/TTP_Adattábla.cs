using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Villamos;
using Villamos.Adatszerkezet;
using MyA = Adatbázis;
using MyEn = Villamos.V_MindenEgyéb.Enumok;

public static partial class Függvénygyűjtemény
{
    public static DataTable AdatTábla_TTP_TáblaFeltölt(List<Adat_TTP_Tábla> Adatok)
    {
        DataTable AdatTábla = new DataTable();
        try
        {
            //Tábla mezőnevek
            AdatTábla.Columns.Clear();
            AdatTábla.Columns.Add("Pályaszám");
            AdatTábla.Columns.Add("Lejárat dátum", typeof(DateTime));
            AdatTábla.Columns.Add("Ütemezés dátum", typeof(DateTime));
            AdatTábla.Columns.Add("TTP dátum", typeof(DateTime));
            AdatTábla.Columns.Add("TTP Javítás");
            AdatTábla.Columns.Add("Rendelés");
            AdatTábla.Columns.Add("Javítás befejező dátum", typeof(DateTime));
            AdatTábla.Columns.Add("Szerelvény");
            AdatTábla.Columns.Add("Státus");
            AdatTábla.Columns.Add("Megjegyzés");


            foreach (Adat_TTP_Tábla rekord in Adatok)
            {
                DataRow Soradat = AdatTábla.NewRow();
                Soradat["Pályaszám"] = rekord.Azonosító;
                Soradat["Lejárat dátum"] = rekord.Lejárat_Dátum;
                Soradat["Ütemezés dátum"] = rekord.Ütemezés_Dátum;
                Soradat["TTP dátum"] = rekord.TTP_Dátum;
                Soradat["TTP Javítás"] = rekord.TTP_Javítás == true ? "Igen" : "Nem";
                Soradat["Rendelés"] = rekord.Rendelés;
                if (rekord.TTP_Javítás) Soradat["Javítás befejező dátum"] = rekord.JavBefDát;

                Soradat["Szerelvény"] = rekord.Együtt;
                Soradat["Státus"] = Enum.GetName(typeof(MyEn.TTP_Státus), rekord.Státus);
                Soradat["Megjegyzés"] = rekord.Megjegyzés;
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


    public static void TTP_AdatTábla_Módosítás(Adat_TTP_Tábla rekord)
    {
        try
        {
            string szöveg = $"UPDATE TTP_Tábla SET ";
            szöveg += $"[Lejárat_Dátum]='{rekord.Lejárat_Dátum:d}', ";
            szöveg += $"[TTP_Dátum]='{rekord.TTP_Dátum:d}', ";
            szöveg += $"[TTP_Javítás]={rekord.TTP_Javítás}, ";
            szöveg += $"[Rendelés] ='{rekord.Rendelés}', ";
            szöveg += $"[JavBefDát] ='{rekord.JavBefDát:d}', ";
            szöveg += $"[Együtt]='{rekord.Együtt}', ";
            szöveg += $"[Státus]={rekord.Státus}, ";
            szöveg += $"[Megjegyzés]='{rekord.Megjegyzés}' ";
            szöveg += $" WHERE [Azonosító]='{rekord.Azonosító}' AND [Ütemezés_Dátum]=#{rekord.Ütemezés_Dátum:M-d-yy}#";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "TTP_AdatTábla_Rögzítés", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static void TTP_AdatTábla_Rögzítés(Adat_TTP_Tábla rekord)
    {
        try
        {
            string szöveg = "INSERT INTO TTP_Tábla (";
            szöveg += $"[Azonosító], ";
            szöveg += $"[Lejárat_Dátum], ";
            szöveg += $"[Ütemezés_Dátum], ";
            szöveg += $"[TTP_Dátum], ";
            szöveg += $"[TTP_Javítás], ";
            szöveg += $"[Rendelés] , ";
            szöveg += $"[JavBefDát] , ";
            szöveg += $"[Együtt], ";
            szöveg += $"[Státus], ";
            szöveg += $"[Megjegyzés] ) VALUES (";
            szöveg += $"'{rekord.Azonosító}', ";
            szöveg += $"'{rekord.Lejárat_Dátum}', ";
            szöveg += $"'{rekord.Ütemezés_Dátum}', ";
            szöveg += $"'{rekord.TTP_Dátum}', ";
            szöveg += $"{rekord.TTP_Javítás}, ";
            szöveg += $"'{rekord.Rendelés}', ";
            szöveg += $"'{rekord.JavBefDát}', ";
            szöveg += $"'{rekord.Együtt}', ";
            szöveg += $"{rekord.Státus}, ";
            szöveg += $"'{rekord.Megjegyzés}' )";
            MyA.ABMódosítás(hely, jelszó, szöveg);
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "TTP_AdatTábla_Módosítás", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static void TTP_AdatTábla_Törlés(Adat_TTP_Tábla rekord)
    {
        try
        {
            string szöveg = $"DELETE FROM TTP_Tábla ";
            szöveg += $" WHERE [Azonosító]='{rekord.Azonosító}' AND [Ütemezés_Dátum]=#{rekord.Ütemezés_Dátum:MM-dd-yyyy}#";
            MyA.ABtörlés(hely, jelszó, szöveg);
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "TTP_AdatTábla_Rögzítés", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static void TTP_AdatTábla_Vizsgál(Adat_TTP_Tábla rekord, List<Adat_TTP_Tábla> Adatok)
    {
        try
        {
            Adat_TTP_Tábla Elem = (from a in Adatok
                                   where a.Azonosító == rekord.Azonosító
                                   && a.Ütemezés_Dátum == rekord.Ütemezés_Dátum
                                   select a).FirstOrDefault();
            if (Elem != null)
                TTP_AdatTábla_Módosítás(rekord);
            else
                TTP_AdatTábla_Rögzítés(rekord);
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "TTP_AdatTábla_Módosítás", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public static void TörténetbeRögzítés(DateTime ÜtemezésDátuma, string Pályaszám, List<Adat_TTP_Tábla> Adatok, Adat_Tábla_Vezénylés Adat, string szerelvény)
    {
        try
        {
            Adat_TTP_Tábla Eleme = (from a in Adatok
                                    where a.Azonosító == Pályaszám
                                    && a.Ütemezés_Dátum == ÜtemezésDátuma
                                    select a).FirstOrDefault();
            if (Eleme == null)
            {
                Adat_TTP_Tábla Elem = new Adat_TTP_Tábla(
                                        Adat.Azonosító,
                                        Adat.Le_Dátum,
                                        ÜtemezésDátuma,
                                        new DateTime(1900, 1, 1),
                                        false,
                                        "",
                                        new DateTime(1900, 1, 1),
                                        szerelvény,
                                        1,
                                        Adat.Megjegyzés);
                TTP_AdatTábla_Rögzítés(Elem);
            }
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "TörténetbeRögzítés", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


}

