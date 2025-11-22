using System;
using System.Diagnostics;
using System.Windows.Forms;
using Villamos;
using static System.IO.File;

public static partial class Függvénygyűjtemény
{
    public static void Megnyitás(string Fájlhelye)
    {
        try
        {
            if (!Exists(Fájlhelye)) return;
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = Fájlhelye,
                UseShellExecute = true, // Fontos: ezzel a rendszer alapértelmezett alkalmazását használja
                Verb = "open"           // Explicit "megnyitás" parancs
            };
            Process.Start(psi);
        }
        catch (Exception ex)
        {
            StackFrame hívó = new System.Diagnostics.StackTrace().GetFrame(1);
            string hívóInfo = hívó?.GetMethod()?.DeclaringType?.FullName + "-" + hívó?.GetMethod()?.Name;
            HibaNapló.Log(ex.Message, $"Megnyitás(Fájlhelye {Fájlhelye}) \n Hívó: {hívóInfo}", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}

