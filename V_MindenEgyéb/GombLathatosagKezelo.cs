using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos;
using Villamos.Adatszerkezet;
using Villamos.Villamos_Adatszerkezet;


public static class GombLathatosagKezelo
{
    /// <summary>
    /// Be�ll�tja az ablakon tal�lhat� gombok l�that�s�g�t az adatb�zis alapj�n.
    /// </summary>
    /// <param name="form">Az ablak (Form), amin a gombok tal�lhat�k.</param>
    public static void Beallit(Form form, string Telephely = "")
    {
        // Lek�rj�k az adott ablakhoz tartoz� gombokat az adatb�zisb�l
        List<Adat_Gombok> gombok = Program.Post�sGombok.Where(g => g.FromName == form.Name && !g.T�r�lt).ToList();

        Gombok�ltal�nos(form, gombok);
        GombokSzem�lyes(form, gombok, Telephely);
    }

    /// <summary>
    /// Kikapcsoljuk az ablakon tal�lhat� gombokat, ha a gombok adatb�zisban megtal�lhat�ak, �gy nem lesznek l�that�ak.
    /// </summary>
    /// <param name="form"></param>
    /// <param name="gombok"></param>
    private static void Gombok�ltal�nos(Form form, List<Adat_Gombok> gombok)
    {
        try
        {
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

    /// <summary>
    /// Visszakapcsoljuk a gombokat, ha a felhaszn�l�nak van jogosults�ga az adott gombhoz, az adott telephelyen.
    /// </summary>
    /// <param name="form"></param>
    /// <param name=""></param>
    /// <param name="gombok"></param>
    /// <param name="Telephely"></param>
    private static void GombokSzem�lyes(Form form, List<Adat_Gombok> gombok, string Telephely = "")
    {
        try
        {
            int TelephelyID = 0;
            Adat_Kieg�sz�t�_K�nyvt�r TelephelyAdat = (from a in Program.Post�sK�nyvt�r
                                                      where a.N�v == Telephely
                                                      select a).FirstOrDefault();
            if (TelephelyAdat != null) TelephelyID = TelephelyAdat.ID;

            // Lek�rj�k az aktu�lis oldal ID-j�t
            Adat_Oldalak AdatOldal = Program.Post�sOldalak.Where(o => o.FromName == form.Name).FirstOrDefault();
            if (AdatOldal == null) return;

            // Lek�rj�k az adott felhaszn�l�hoz tartoz� gombokat az adatb�zisb�l
            List<Adat_Jogosults�gok> jogosults�gok = Program.Post�sJogosults�gok;
            jogosults�gok = (from j in jogosults�gok
                             where j.UserId == Program.Post�sN�vId
                             && !j.T�r�lt
                             && j.SzervezetId == TelephelyID
                             && j.OldalId == AdatOldal.OldalId
                             select j).ToList();

            // ha a jogosult�s�g t�bl�ban van akkor van hozz� joga �gy l�that�v� tessz�k a gombokat
            foreach (Adat_Jogosults�gok adatGomb in jogosults�gok)
            {
                Adat_Gombok Egygomb = (from a in gombok
                                       where a.GombokId == adatGomb.GombokId
                                       select a).FirstOrDefault();
                if (Egygomb != null)
                {
                    // Megkeress�k a gombot az ablak Controls gy�jtem�ny�ben
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

    public static List<string> Telephelyek(string AblakN�v)
    {
        List<string> V�lasz = new List<string>();
        try
        {
            //Mi az oldal Id-je
            Adat_Oldalak Oldal = (from a in Program.Post�sOldalak
                                  where a.FromName == AblakN�v
                                  select a).FirstOrDefault();
            int OldalId = 0;
            if (Oldal != null) OldalId = Oldal.OldalId;
            //Azok a jogosults�gok amik az adott oldalhoz tartoznak
            List<Adat_Jogosults�gok> AdatokA = (from a in Program.Post�sJogosults�gok
                                                where a.OldalId == OldalId
                                                select a).ToList();

            List<Adat_Kieg�sz�t�_K�nyvt�r> Ideig = new List<Adat_Kieg�sz�t�_K�nyvt�r>();
            foreach (Adat_Jogosults�gok Adat in AdatokA)
            {
                Adat_Kieg�sz�t�_K�nyvt�r AdatK�nyv = (from a in Program.Post�sK�nyvt�r
                                                      where a.ID == Adat.SzervezetId
                                                      select a).FirstOrDefault();
                Ideig.Add(AdatK�nyv);
            }

            if (Ideig.Count > 0) V�lasz = Ideig.OrderBy(a => a.N�v).Select(a => a.N�v).Distinct().ToList();
        }
        catch (Hib�sBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Inform�ci�", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapl�.Log(ex.Message, "GombLathatosagKezelo - Telephelyek List�z�sa", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba napl�z�sra ker�lt.", "A program hib�ra futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return V�lasz;
    }

}