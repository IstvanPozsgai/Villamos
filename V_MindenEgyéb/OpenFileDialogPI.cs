using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                string FájlKiterjesztés = Path.GetExtension(dialog.FileName).TrimStart('.').ToLower();
                if (string.IsNullOrEmpty(FájlKiterjesztés))
                {
                    MessageBox.Show("Nincs a betölteni kívánt fájlnak fájl kiterjesztése!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                if (!KiterjesztésLista.Contains(FájlKiterjesztés))
                {
                    MessageBox.Show($"Nem megfelelõ a betölteni kívánt fájl kiterjesztés:{FájlKiterjesztés} formátuma!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                isValidFile = true;
            }
            return result;
        }
    }
}