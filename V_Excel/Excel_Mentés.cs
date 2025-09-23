using System;
using System.Windows.Forms;

namespace Villamos
{
    public static partial class Module_Excel
    {
        /// <summary>
        /// DatagridView tartalmát elmenti Excel fájlba
        /// </summary>
        /// <param name="InitialDirectory">Fájl elérési út</param>
        /// <param name="Title">Ablak felirat</param>
        /// <param name="FileName">Fájlnév</param>
        /// <param name="Filter">Szűrő</param>
        /// <param name="Tábla">DatagridView táblanév</param>
        public static void Mentés(string InitialDirectory, string Title, string FileName, string Filter, DataGridView Tábla)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = InitialDirectory,
                    Title = Title,
                    FileName = FileName,
                    Filter = Filter
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                DataGridViewToExcel(fájlexc, Tábla);
                MessageBox.Show($"Elkészült az Excel tábla:\n{fájlexc}", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Megnyitás(fájlexc);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, $"Mentés {InitialDirectory},{Title},{FileName},{Filter}", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
