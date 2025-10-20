using System.Windows.Forms;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Sérülés
{
    partial class Ablak_PDF_Feltöltés
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_PDF_Feltöltés));
            this.FájlLista = new System.Windows.Forms.ListBox();
            this.Pdftöltő = new PdfiumViewer.PdfViewer();
            this.Btn_Másolás = new System.Windows.Forms.Button();
            this.Btn_PDFNyitó = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FájlLista
            // 
            this.FájlLista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FájlLista.FormattingEnabled = true;
            this.FájlLista.ItemHeight = 20;
            this.FájlLista.Location = new System.Drawing.Point(10, 68);
            this.FájlLista.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FájlLista.Name = "FájlLista";
            this.FájlLista.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.FájlLista.Size = new System.Drawing.Size(281, 484);
            this.FájlLista.TabIndex = 243;
            this.FájlLista.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // Pdftöltő
            // 
            this.Pdftöltő.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Pdftöltő.BackColor = System.Drawing.Color.SeaGreen;
            this.Pdftöltő.Location = new System.Drawing.Point(306, 10);
            this.Pdftöltő.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Pdftöltő.Name = "Pdftöltő";
            this.Pdftöltő.Size = new System.Drawing.Size(473, 542);
            this.Pdftöltő.TabIndex = 253;
            this.Pdftöltő.ZoomMode = PdfiumViewer.PdfViewerZoomMode.FitBest;
            this.Pdftöltő.ShowToolbar = true;
            // 
            // Btn_Másolás
            // 
            this.Btn_Másolás.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Másolás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Másolás.Location = new System.Drawing.Point(246, 10);
            this.Btn_Másolás.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Btn_Másolás.Name = "Btn_Másolás";
            this.Btn_Másolás.Size = new System.Drawing.Size(45, 45);
            this.Btn_Másolás.TabIndex = 255;
            this.Btn_Másolás.UseVisualStyleBackColor = true;
            this.Btn_Másolás.Click += new System.EventHandler(this.Btn_PDFVálasztó_Click);
            // 
            // Btn_PDFNyitó
            // 
            this.Btn_PDFNyitó.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.Btn_PDFNyitó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_PDFNyitó.Location = new System.Drawing.Point(10, 10);
            this.Btn_PDFNyitó.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Btn_PDFNyitó.Name = "Btn_PDFNyitó";
            this.Btn_PDFNyitó.Size = new System.Drawing.Size(45, 45);
            this.Btn_PDFNyitó.TabIndex = 254;
            this.Btn_PDFNyitó.UseVisualStyleBackColor = true;
            this.Btn_PDFNyitó.Click += new System.EventHandler(this.Btn_PDFNyitó_Click);

            // 
            // Ablak_PDF_Feltöltés
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.Btn_Másolás);
            this.Controls.Add(this.Btn_PDFNyitó);
            this.Controls.Add(this.FájlLista);
            this.Controls.Add(this.Pdftöltő);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_PDF_Feltöltés";
            this.Text = "Villamos PDF feltöltés";
            this.Load += new System.EventHandler(this.Ablak_Sérülés_PDF_Load);
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.ListBox FájlLista;
        private PdfiumViewer.PdfViewer Pdftöltő;
        internal System.Windows.Forms.Button Btn_Másolás;
        internal System.Windows.Forms.Button Btn_PDFNyitó;
    }
}