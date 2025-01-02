using PdfiumViewer;
using System;
using System.IO;
using System.Windows.Forms;

namespace Villamos.Villamos_Ablakok.T5C5
{
    public partial class Ablak_PDF_Tallózó : Form
    {
        public string Hely { get; private set; }
        public string Fájlnévrész { get; private set; }

        public Ablak_PDF_Tallózó(string hely, string fájlnévrész)
        {
            Hely = hely;
            Fájlnévrész = fájlnévrész;
            InitializeComponent();
            Start();

        }
        void Start()
        {
            Lista_Feltöltés();


        }

        private void Ablak_PDF_Tallózó_Load(object sender, EventArgs e)
        {

        }

        void Lista_Feltöltés()
        {
            try
            {
                if (Hely.Trim() == "") return;

                FileList.Items.Clear();

                DirectoryInfo di = new DirectoryInfo(Hely.Trim());
                var aryFi = di.GetFiles($"*{Fájlnévrész.Trim()}*.pdf");

                foreach (var fi in aryFi)
                    FileList.Items.Add(fi.Name);

            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void FileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string    hely =Hely.Trim ()+@"\" + FileList.SelectedItems[0].ToString();
                if (!File.Exists(hely) )
                    return;

                Byte[] bytes = System.IO.File.ReadAllBytes(hely);
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
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
