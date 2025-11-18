using System;
using System.Diagnostics;
using System.Windows.Forms;
using Villamos;

public static partial class Függvénygyűjtemény
{
    public static string Oszlopnév(int sorszám)
    {
        string oszlopNev = string.Empty;
        int eredetiSorszám = sorszám;
        try
        {
            if (sorszám < 1) throw new ArgumentOutOfRangeException(nameof(sorszám), "Az oszlopszámnak 1 vagy nagyobbnak kell lennie.");
            while (sorszám > 0)
            {
                sorszám--;
                oszlopNev = (char)('A' + (sorszám % 26)) + oszlopNev;
                sorszám /= 26;
            }
        }
        catch (Exception ex)
        {
            StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
            string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
            HibaNapló.Log(ex.Message, $"Oszlopnév(sorszám {eredetiSorszám}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return oszlopNev;
    }
}
