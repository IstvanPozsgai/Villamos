using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos;
using Villamos.Adatszerkezet;
using Villamos.Kezelõk;


public static class GombLathatosagKezelo
{

    /// <summary>
    /// Beállítja az ablakon található gombok láthatóságát az adatbázis alapján.
    /// </summary>
    /// <param name="form">Az ablak (Form), amin a gombok találhatók.</param>
    public static void Beallit(Form form)
    {
        GombokÁltalános(form);
        GombokSzemélyes(form);
    }

    private static void GombokÁltalános(Form form)
    {
        try
        {
            Kezelõk_Gombok kezeloGombok = new Kezelõk_Gombok();

            // Lekérjük az adott ablakhoz tartozó gombokat az adatbázisból
            List<Adat_Gombok> gombok = kezeloGombok.Lista_Adatok()
                .Where(g => g.FromName == form.Name && !g.Törölt)
                .ToList();

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

    private static void GombokSzemélyes(Form form)
    {
        try
        {
            Kezelõk_Jogosultságok Kéz = new Kezelõk_Jogosultságok();

            Kezelõk_Gombok kezeloGombok = new Kezelõk_Gombok();
            // Lekérjük az adott ablakhoz tartozó gombokat az adatbázisból
            List<Adat_Gombok> AdatokGombok = kezeloGombok.Lista_Adatok()
                .Where(g => g.FromName == form.Name && !g.Törölt)
                .ToList();

            // Lekérjük az aktuális oldal ID-ját
            Kezelõk_Oldalok KézOldal = new Kezelõk_Oldalok();
            Adat_Oldalak AdatOldal = KézOldal.Lista_Adatok()
                .Where(o => o.FromName == form.Name).FirstOrDefault();
            if (AdatOldal == null) return;

            // Lekérjük az adott felhasználóhoz tartozó gombokat az adatbázisból
            List<Adat_Jogosultságok> jogosultságok = Kéz.Lista_Adatok()
                .Where(j => j.UserId == Program.PostásNévId && !j.Törölt && j.OldalId == AdatOldal.OldalId)
                .ToList();
            // ha a jogosultáság táblában van akkor van hozzá joga így láthatóvá tesszük a gombokat
            foreach (Adat_Jogosultságok adatGomb in jogosultságok)
            {
                Adat_Gombok Egygomb = (from a in AdatokGombok
                                       where a.GombokId == adatGomb.GombokId
                                       select a).FirstOrDefault();
                if (Egygomb != null)
                {                // Megkeressük a gombot az ablak Controls gyûjteményében
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