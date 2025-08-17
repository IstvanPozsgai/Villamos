using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos;
using Villamos.Adatszerkezet;
using Villamos.Kezelõk;
using Villamos.Villamos_Adatszerkezet;


public static class GombLathatosagKezelo
{
    private static Kezelõ_Gombok KézGombok = new Kezelõ_Gombok();
    /// <summary>
    /// Beállítja az ablakon található gombok láthatóságát az adatbázis alapján.
    /// </summary>
    /// <param name="form">Az ablak (Form), amin a gombok találhatók.</param>
    public static void Beallit(Form form, string Telephely = "")
    {
        // Lekérjük az adott ablakhoz tartozó gombokat az adatbázisból
        List<Adat_Gombok> gombok = KézGombok.Lista_Adatok()
            .Where(g => g.FromName == form.Name && !g.Törölt)
            .ToList();

        GombokÁltalános(form, gombok);
        GombokSzemélyes(form, gombok, Telephely);
    }

    /// <summary>
    /// Kikapcsoljuk az ablakon található gombokat, ha a gombok adatbázisban megtalálhatóak, így nem lesznek láthatóak.
    /// </summary>
    /// <param name="form"></param>
    /// <param name="gombok"></param>
    private static void GombokÁltalános(Form form, List<Adat_Gombok> gombok)
    {
        try
        {
            foreach (Adat_Gombok adatGomb in gombok)
            {
                // Megkeressük a gombot az ablak Controls gyûjteményében
                Control control = form.Controls.Find(adatGomb.GombName, true).FirstOrDefault();
                if (control is Button button)
                {
                    button.Visible = adatGomb.Látható;
                }
            }
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "GombokÁltalános", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Visszakapcsoljuk a gombokat, ha a felhasználónak van jogosultsága az adott gombhoz, az adott telephelyen.
    /// </summary>
    /// <param name="form"></param>
    /// <param name=""></param>
    /// <param name="gombok"></param>
    /// <param name="Telephely"></param>
    private static void GombokSzemélyes(Form form, List<Adat_Gombok> gombok, string Telephely = "")
    {
        try
        {

            Kezelõ_Kiegészítõ_Könyvtár kézKönyvtár = new Kezelõ_Kiegészítõ_Könyvtár();
            List<Adat_Kiegészítõ_Könyvtár> AdatokKönyvtár = kézKönyvtár.Lista_Adatok();
            int TelephelyID = 0;
            Adat_Kiegészítõ_Könyvtár TelephelyAdat = (from a in AdatokKönyvtár
                                                      where a.Név == Telephely
                                                      select a).FirstOrDefault();
            if (TelephelyAdat != null) TelephelyID = TelephelyAdat.ID;

            // Lekérjük az aktuális oldal ID-ját
            Kezelõ_Oldalok KézOldal = new Kezelõ_Oldalok();
            Adat_Oldalak AdatOldal = KézOldal.Lista_Adatok().Where(o => o.FromName == form.Name).FirstOrDefault();
            if (AdatOldal == null) return;

            // Lekérjük az adott felhasználóhoz tartozó gombokat az adatbázisból
            List<Adat_Jogosultságok> jogosultságok = Program.PostásJogosultságok;
            jogosultságok = (from j in jogosultságok
                             where j.UserId == Program.PostásNévId
                             && !j.Törölt
                             && j.SzervezetId == TelephelyID
                             && j.OldalId == AdatOldal.OldalId
                             select j).ToList();

            // ha a jogosultáság táblában van akkor van hozzá joga így láthatóvá tesszük a gombokat
            foreach (Adat_Jogosultságok adatGomb in jogosultságok)
            {
                Adat_Gombok Egygomb = (from a in gombok
                                       where a.GombokId == adatGomb.GombokId
                                       select a).FirstOrDefault();
                if (Egygomb != null)
                {
                    // Megkeressük a gombot az ablak Controls gyûjteményében
                    Control control = form.Controls.Find(Egygomb.GombName, true).FirstOrDefault();
                    if (control is Button button) button.Visible = true;
                }
            }
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "GombokSzemélyes", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}