using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                string F�jlKiterjeszt�s = Path.GetExtension(dialog.FileName).TrimStart('.').ToLower();
                if (string.IsNullOrEmpty(F�jlKiterjeszt�s))
                {
                    MessageBox.Show("Nincs a bet�lteni k�v�nt f�jlnak f�jl kiterjeszt�se!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                if (!Kiterjeszt�sLista.Contains(F�jlKiterjeszt�s))
                {
                    MessageBox.Show($"Nem megfelel� a bet�lteni k�v�nt f�jl kiterjeszt�s:{F�jlKiterjeszt�s} form�tuma!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                isValidFile = true;
            }
            return result;
        }
    }
}