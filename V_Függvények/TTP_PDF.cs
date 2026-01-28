using System;
using System.IO;
using System.Windows.Forms;
using Villamos;


public static partial class Függvénygyűjtemény
{
    public static bool VanPDF(string Pályaszám, DateTime Dátum)
    {
        bool válasz = false;
        try
        {
            string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\TTP\PDF\";
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
            string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\TTP\PDF\";
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
