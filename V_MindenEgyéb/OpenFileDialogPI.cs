using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Villamos.V_MindenEgy�b
{
    public static class OpenFileDialogPI
    {
        public static DialogResult ShowDialogEllen�r(OpenFileDialog dialog)
        {
            // Enged�lyezett kiterjeszt�sek kigy�jt�se a filterb�l
            List<string> Kiterjeszt�sLista = dialog.Filter
                .Split('|')
                .Where((item, index) => index % 2 == 1) // csak a mint�k
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

                string f�jlKiterjeszt�s = Path.GetExtension(dialog.FileName).TrimStart('.').ToLower();
                if (string.IsNullOrEmpty(f�jlKiterjeszt�s))
                {
                    MessageBox.Show("Nincs a bet�lteni k�v�nt f�jlnak kiterjeszt�se!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                if (!Kiterjeszt�sLista.Contains(f�jlKiterjeszt�s))
                {
                    MessageBox.Show($"Nem megfelel� a bet�lteni k�v�nt f�jl kiterjeszt�s: {f�jlKiterjeszt�s}!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }

                // Ellen�rz�s + bez�r�s el�r�si �t alapj�n Excel eset�n
                // Mint ut�lag r�j�ttem, a FileName nem a f�jlnevet, hanem a teljes el�r�si utat adja vissza.
                if (Kiterjeszt�sLista.Contains(f�jlKiterjeszt�s))
                {
                    IsFileOpened(dialog.FileName);
                }
               
                isValidFile = true;
            }
            return result;
        }

        // Bez�rja a tall�zott Excel t�bl�t abban az esetben, ha az nyitva van.
        static void IsFileOpened(string filePath)
        {
            try
            {
                // Kapcsol�d�s a fut� Excel p�ld�nyhoz
                Microsoft.Office.Interop.Excel.Application excelApp =
                    (Microsoft.Office.Interop.Excel.Application)Marshal.GetActiveObject("Excel.Application");

                // Megkeress�k a keresett f�jlt teljes el�r�si �t alapj�n
                var workbook = excelApp.Workbooks
                    .Cast<Microsoft.Office.Interop.Excel.Workbook>()
                    .FirstOrDefault(wb => wb.FullName.Equals(filePath, StringComparison.OrdinalIgnoreCase));

                if (workbook != null)
                {
                    workbook.Close(true); // true = ment�s bez�r�s el�tt, nem k�rdez
                    Marshal.ReleaseComObject(workbook);
                    // siker�lt bez�rni
                }
            }
            catch (COMException)
            {
                // Ha nincs fut� Excel p�ld�ny nem akad fent.
            }
        }
    }
}
