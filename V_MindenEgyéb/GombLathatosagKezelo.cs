using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos;
using Villamos.Adatszerkezet;
using Villamos.Kezel�k;


public static class GombLathatosagKezelo
{

    /// <summary>
    /// Be�ll�tja az ablakon tal�lhat� gombok l�that�s�g�t az adatb�zis alapj�n.
    /// </summary>
    /// <param name="form">Az ablak (Form), amin a gombok tal�lhat�k.</param>
    public static void Beallit(Form form)
    {
        Gombok�ltal�nos(form);
        GombokSzem�lyes(form);
    }

    private static void Gombok�ltal�nos(Form form)
    {
        try
        {
            Kezel�k_Gombok kezeloGombok = new Kezel�k_Gombok();

            // Lek�rj�k az adott ablakhoz tartoz� gombokat az adatb�zisb�l
            List<Adat_Gombok> gombok = kezeloGombok.Lista_Adatok()
                .Where(g => g.FromName == form.Name && !g.T�r�lt)
                .ToList();

            foreach (Adat_Gombok adatGomb in gombok)
            {
                // Megkeress�k a gombot az ablak Controls gy�jtem�ny�ben
                Control control = form.Controls.Find(adatGomb.GombName, true).FirstOrDefault();
                if (control is Button button)
                {
                    button.Visible = adatGomb.L�that�;
                }
            }

        }
        catch (Hib�sBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Inform�ci�", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapl�.Log(ex.Message, "Gombok�ltal�nos", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba napl�z�sra ker�lt.", "A program hib�ra futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private static void GombokSzem�lyes(Form form)
    {
        try
        {
            Kezel�k_Jogosults�gok K�z = new Kezel�k_Jogosults�gok();

            Kezel�k_Gombok kezeloGombok = new Kezel�k_Gombok();
            // Lek�rj�k az adott ablakhoz tartoz� gombokat az adatb�zisb�l
            List<Adat_Gombok> AdatokGombok = kezeloGombok.Lista_Adatok()
                .Where(g => g.FromName == form.Name && !g.T�r�lt)
                .ToList();

            // Lek�rj�k az aktu�lis oldal ID-j�t
            Kezel�k_Oldalok K�zOldal = new Kezel�k_Oldalok();
            Adat_Oldalak AdatOldal = K�zOldal.Lista_Adatok()
                .Where(o => o.FromName == form.Name).FirstOrDefault();
            if (AdatOldal == null) return;

            // Lek�rj�k az adott felhaszn�l�hoz tartoz� gombokat az adatb�zisb�l
            List<Adat_Jogosults�gok> jogosults�gok = K�z.Lista_Adatok()
                .Where(j => j.UserId == Program.Post�sN�vId && !j.T�r�lt && j.OldalId == AdatOldal.OldalId)
                .ToList();
            // ha a jogosult�s�g t�bl�ban van akkor van hozz� joga �gy l�that�v� tessz�k a gombokat
            foreach (Adat_Jogosults�gok adatGomb in jogosults�gok)
            {
                Adat_Gombok Egygomb = (from a in AdatokGombok
                                       where a.GombokId == adatGomb.GombokId
                                       select a).FirstOrDefault();
                if (Egygomb != null)
                {                // Megkeress�k a gombot az ablak Controls gy�jtem�ny�ben
                    Control control = form.Controls.Find(Egygomb.GombName, true).FirstOrDefault();
                    if (control is Button button) button.Visible = true;

                }
            }
        }
        catch (Hib�sBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Inform�ci�", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapl�.Log(ex.Message, "GombokSzem�lyes", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba napl�z�sra ker�lt.", "A program hib�ra futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}