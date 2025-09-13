using System.Windows.Forms;

namespace Villamos.V_MindenEgyéb
{
    public static class OpenFileDialogPI
    {
        private readonly static OpenFileDialog dialog = new OpenFileDialog();
        public static string FileName => dialog.FileName;
        public static string Filter { get => dialog.Filter; set => dialog.Filter = value; }
        public static string Title { get => dialog.Title; set => dialog.Title = value; }


        public static DialogResult ShowDialogWithXlsCheck()
        {
            while (true)
            {
                DialogResult result = dialog.ShowDialog();
                if (result != DialogResult.OK)
                    return result;

                string fileName = dialog.FileName.ToLower();
                string[] parts = fileName.Split('.');
                if (parts.Length < 2)
                {
                    MessageBox.Show("Nem megfelelő a betölteni kívánt fájl formátuma!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                if (!parts[parts.Length - 1].Contains("xls"))
                {
                    MessageBox.Show("Nem megfelelő a betölteni kívánt fájl kiterjesztés formátuma!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                return result;
            }
        }


    }
}
