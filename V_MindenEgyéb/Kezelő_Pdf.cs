using PdfiumViewer;
using System;
using System.IO;
using System.Windows.Forms;

namespace Villamos.V_MindenEgyéb
{
    public static class Kezelő_Pdf
    {
        public static void PdfMegnyitás(PdfViewer PDF_néző, string hely)
        {
            try
            {
                PdfÜrítés(PDF_néző);
                Byte[] bytes = File.ReadAllBytes(hely);
                MemoryStream stream = new MemoryStream(bytes);
                PdfDocument pdfDocument = PdfDocument.Load(stream);
                PDF_néző.Document = pdfDocument;
                PDF_néző.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "Kezelő_Pdf/PdfMegnyitás", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void PdfÜrítés(PdfViewer PDF_néző)
        {
            try
            {
                PDF_néző.Document?.Dispose();
                PDF_néző.Document = null;
                GC.Collect();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, "PdfÜrítés", ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
