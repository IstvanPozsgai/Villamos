using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Villamos.V_MindenEgyéb
{
    public static class OpenFileDialogPI
    {
        public static DialogResult ShowDialogEllenõr(OpenFileDialog dialog)
        {
            // Engedélyezett kiterjesztések kigyûjtése a filterbõl
            List<string> KiterjesztésLista = dialog.Filter
                .Split('|')
                .Where((item, index) => index % 2 == 1) // csak a minták
                .SelectMany(patterns => patterns.Split(';'))
                .Select(pattern => pattern.Trim().TrimStart('*').TrimStart('.').ToLower())
                .Where(ext => !string.IsNullOrWhiteSpace(ext))
                .ToList();

            bool isValidFile = false;
            DialogResult result = DialogResult.Cancel;

            while (!isValidFile)
            {
                result = dialog.ShowDialog();
                if (result != DialogResult.OK) return result;

                string fájlKiterjesztés = Path.GetExtension(dialog.FileName).TrimStart('.').ToLower();
                if (string.IsNullOrEmpty(fájlKiterjesztés))
                {
                    MessageBox.Show("Nincs a betölteni kívánt fájlnak kiterjesztése!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                if (!KiterjesztésLista.Contains(fájlKiterjesztés))
                {
                    MessageBox.Show($"Nem megfelelõ a betölteni kívánt fájl kiterjesztés: {fájlKiterjesztés}!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                // Ellenõrzés + bezárás elérési út alapján Excel esetén
                // Mint utólag rájöttem, a FileName nem a fájlnevet, hanem a teljes elérési utat adja vissza.
                if (KiterjesztésLista.Contains(fájlKiterjesztés))
                {
                    IsFileOpened(dialog.FileName);
                }
               
                isValidFile = true;
            }
            return result;
        }

        // Bezárja a tallózott Excel táblát abban az esetben, ha az nyitva van.
        static void IsFileOpened(string filePath)
        {
            try
            {
                // Kapcsolódás a futó Excel példányhoz
                Microsoft.Office.Interop.Excel.Application excelApp =
                    (Microsoft.Office.Interop.Excel.Application)Marshal.GetActiveObject("Excel.Application");

                // Megkeressük a keresett fájlt teljes elérési út alapján
                var workbook = excelApp.Workbooks
                    .Cast<Microsoft.Office.Interop.Excel.Workbook>()
                    .FirstOrDefault(wb => wb.FullName.Equals(filePath, StringComparison.OrdinalIgnoreCase));

                if (workbook != null)
                {
                    workbook.Close(true); // true = mentés bezárás elõtt, nem kérdez
                    Marshal.ReleaseComObject(workbook);
                    // sikerült bezárni
                }
            }
            catch (COMException)
            {
                // Ha nincs futó Excel példány nem akad fent.
            }
        }
    }
}
