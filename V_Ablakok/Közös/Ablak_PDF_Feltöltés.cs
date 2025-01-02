using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static System.IO.File;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Sérülés
{
    public partial class Ablak_PDF_Feltöltés : Form
    {
        public event Event_Kidobó Változás;

        private string BeolvasásiHely;
        private DateTime Dátum { get; set; }
        private int DoksikValue { get; set; }
        private int Sorszam { get; set; }
        private string Pályaszám { get; set; }
        private string Hova { get; set; }
        private string MelyikAblak { get; set; }
        private List<string> PDFek { get; set; }
        private bool Megjelenítés { get; set; }



        public Ablak_PDF_Feltöltés(string hova, DateTime dátum, int doksikValue, int sorszam, string pályaszám, List<string> pDFek, string melyikAblak, bool megjelenítés)
        {
            Dátum = dátum;
            DoksikValue = doksikValue;
            Sorszam = sorszam;
            Pályaszám = pályaszám;
            PDFek = pDFek;
            Hova = hova;
            MelyikAblak = melyikAblak;
            Megjelenítés = megjelenítés;
            InitializeComponent();

        }

        private void Ablak_Sérülés_PDF_Load(object sender, EventArgs e)
        {
            if (Megjelenítés)
            {
                Btn_PDFNyitó.Visible = false;
                Btn_Másolás.Visible = false;
                ElemekListája();
                BeolvasásiHely = Hova;
                this.Text = "Villamos PDF megjelenítés";
            }
        }

        private void ElemekListája()
        {
            try
            {       // A tervezett fájlnévnek megfelelően szűrjük a könyvtár tartalmát
                FájlLista.Items.Clear();
                DirectoryInfo Directories = new DirectoryInfo(Hova);
                string mialapján = $@"{Pályaszám}_{Dátum:yyyy}*.pdf";


                FileInfo[] fileInfo = Directories.GetFiles(mialapján, SearchOption.TopDirectoryOnly);

                foreach (FileInfo file in fileInfo)
                    FájlLista.Items.Add(file.Name);

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

        private void Btn_PDFNyitó_Click(object sender, EventArgs e)
        {
            Pdfekkel();
        }

        private void Pdfekkel()
        {
            try
            {
                FájlLista.Items.Clear();
                using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
                {
                    if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        DirectoryInfo di = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
                        BeolvasásiHely = folderBrowserDialog1.SelectedPath;
                        FileInfo[] aryFi = di.GetFiles("*.pdf");
                        foreach (FileInfo fi in aryFi)
                            FájlLista.Items.Add(fi.Name);
                    }
                }
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


        private void Btn_PDFVálasztó_Click(object sender, EventArgs e)
        {
            switch (MelyikAblak)
            {
                case "Sérülés":
                    Sérüléshez();
                    break;
                case "TTP":
                    TTP();
                    break;
            }
        }





        private void Sérüléshez()
        {
            try
            {
                if (FájlLista.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kiválasztva dokumentum!");

                int sorszám;
                if (DoksikValue == 0) sorszám = 0;
                else
                {
                    string szöveg = PDFek[PDFek.Count - 1].Trim().Substring(0, PDFek[PDFek.Count - 1].Trim().Length - 4);
                    string[] darabok = szöveg.Split('_');
                    sorszám = int.Parse(darabok[darabok.Length - 1]);
                }
                // kijelölt elemeket másolja a kijelölt könyvtárba
                for (int i = 0; i < FájlLista.SelectedItems.Count; i++)
                {
                    sorszám++;
                    string hely = $@"{$"{Hova}{Dátum.Year}"}_{Sorszam.ToStrTrim()}_{Pályaszám.ToStrTrim()}_{sorszám}.pdf";
                    string honnan = $@"{BeolvasásiHely}\{FájlLista.SelectedItems[i].ToStrTrim()}";
                    Copy(honnan, hely);
                    PDFek.Add($@"{$"{Dátum.Year}"}_{Sorszam.ToStrTrim()}_{Pályaszám.ToStrTrim()}_{sorszám}.pdf");
                }
                DoksikValue += FájlLista.SelectedItems.Count;
                MessageBox.Show("A PDF feltöltés sikeres volt.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Változás?.Invoke();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void TTP()
        {
            try
            {
                if (FájlLista.SelectedItems.Count < 1) throw new HibásBevittAdat("Nincs kiválasztva dokumentum!");
                int elem = MyF.VanPDFdb(Pályaszám, Dátum);
                // kijelölt elemeket másolja a kijelölt könyvtárba
                for (int i = 0; i < FájlLista.SelectedItems.Count; i++)
                {
                    string hely = $@"{Hova}\{Pályaszám}_{Dátum:yyyyMMdd}_{++elem}.pdf";
                    string honnan = $@"{BeolvasásiHely}\{FájlLista.SelectedItems[i].ToStrTrim()}";
                    Copy(honnan, hely);
                }
                DoksikValue += FájlLista.SelectedItems.Count;
                MessageBox.Show("A PDF feltöltés sikeres volt.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Változás?.Invoke();

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

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (FájlLista.SelectedItems.Count == 0) return;
                string hely = $@"{BeolvasásiHely}\{FájlLista.SelectedItems[0]}";
                if (!Exists(hely)) throw new HibásBevittAdat("Nem létezik a betölteni kívánt pdf.");

                Byte[] bytes = ReadAllBytes(hely);
                MemoryStream stream = new MemoryStream(bytes);
                PdfDocument pdfDocument = PdfDocument.Load(stream);
                Pdftöltő.Document = pdfDocument;
                Pdftöltő.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
