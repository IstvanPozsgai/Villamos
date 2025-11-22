using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Villamos;
using static System.IO.File;
using MyE = Microsoft.Office.Interop.Excel;

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



    public static void ExcelNyomtatás(List<string> excelFiles, bool törlés = false)
    {
        MyE.Application excelApp = null;
        Workbook workbook = null;

        foreach (string filePath in excelFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Fájl nem található: {filePath}");
                continue;
            }

            try
            {
                // Excel alkalmazás példányosítása (láthatatlan módban)
                excelApp = new MyE.Application
                {
                    Visible = false,
                    DisplayAlerts = false
                };

                // Munkafüzet megnyitása
                workbook = excelApp.Workbooks.Open(filePath);

                // Nyomtatás az alapértelmezett nyomtatóra (minden munkalap)
                workbook.PrintOut(); // Esetleg megadhatsz From, To paramétereket is

                // Munkafüzet bezárása mentés nélkül
                workbook.Close(SaveChanges: false);
                Marshal.ReleaseComObject(workbook);
                workbook = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a(z) {filePath} fájl nyomtatása közben: {ex.Message}");
            }
            finally
            {
                // Excel alkalmazás bezárása
                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                    excelApp = null;
                }

                // Fájl törlése (akár sikeres, akár sikertelen volt a nyomtatás – ízlés szerint)
                try
                {
                    if (törlés)
                    {
                        File.Delete(filePath);
                        Console.WriteLine($"Törölve: {filePath}");
                    }
                }
                catch (Exception delEx)
                {
                    Console.WriteLine($"Nem sikerült törölni: {filePath} – {delEx.Message}");
                }
            }
        }
    }
}

