namespace Villamos.V_Ablakok._4_Nyilvántartások.Eszterga_Karbantartás
{
    partial class Ablak_Eszterga_Karbantartás_Üzemóra
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Eszterga_Karbantartás_Üzemóra));
            this.Tabla = new Zuby.ADGV.AdvancedDataGridView();
            this.Btn_Excel = new System.Windows.Forms.Button();
            this.Btn_UjFelvetel = new System.Windows.Forms.Button();
            this.TxtBxUzem = new System.Windows.Forms.TextBox();
            this.Btn_Modosit = new System.Windows.Forms.Button();
            this.LblStatusz = new System.Windows.Forms.Label();
            this.ChckBxStatus = new System.Windows.Forms.CheckBox();
            this.LblUzem = new System.Windows.Forms.Label();
            this.LblDatum = new System.Windows.Forms.Label();
            this.DtmPckr = new System.Windows.Forms.DateTimePicker();
            this.Btn_Pdf = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Tabla)).BeginInit();
            this.SuspendLayout();
            // 
            // Tabla
            // 
            this.Tabla.AllowUserToAddRows = false;
            this.Tabla.AllowUserToDeleteRows = false;
            this.Tabla.AllowUserToResizeRows = false;
            this.Tabla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tabla.FilterAndSortEnabled = true;
            this.Tabla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tabla.Location = new System.Drawing.Point(24, 14);
            this.Tabla.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Tabla.MaxFilterButtonImageHeight = 23;
            this.Tabla.Name = "Tabla";
            this.Tabla.ReadOnly = true;
            this.Tabla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tabla.RowHeadersVisible = false;
            this.Tabla.RowHeadersWidth = 30;
            this.Tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Tabla.Size = new System.Drawing.Size(472, 430);
            this.Tabla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tabla.TabIndex = 261;
            this.Tabla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tabla_CellClick);
            this.Tabla.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.Tabla_DataBindingComplete);
            // 
            // Btn_Excel
            // 
            this.Btn_Excel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_Excel.BackColor = System.Drawing.Color.Transparent;
            this.Btn_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Excel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Excel.ForeColor = System.Drawing.Color.Transparent;
            this.Btn_Excel.Image = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Btn_Excel.Location = new System.Drawing.Point(726, 382);
            this.Btn_Excel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Excel.Name = "Btn_Excel";
            this.Btn_Excel.Size = new System.Drawing.Size(60, 62);
            this.Btn_Excel.TabIndex = 260;
            this.toolTip1.SetToolTip(this.Btn_Excel, "Excel táblázatot készít a táblázat adataiból");
            this.Btn_Excel.UseVisualStyleBackColor = false;
            this.Btn_Excel.Click += new System.EventHandler(this.Btn_Excel_Click);
            // 
            // Btn_UjFelvetel
            // 
            this.Btn_UjFelvetel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_UjFelvetel.BackColor = System.Drawing.Color.Transparent;
            this.Btn_UjFelvetel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_UjFelvetel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_UjFelvetel.ForeColor = System.Drawing.Color.Transparent;
            this.Btn_UjFelvetel.Image = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Btn_UjFelvetel.Location = new System.Drawing.Point(522, 382);
            this.Btn_UjFelvetel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_UjFelvetel.Name = "Btn_UjFelvetel";
            this.Btn_UjFelvetel.Size = new System.Drawing.Size(60, 62);
            this.Btn_UjFelvetel.TabIndex = 259;
            this.toolTip1.SetToolTip(this.Btn_UjFelvetel, "Új üzemóra hozzáadása");
            this.Btn_UjFelvetel.UseVisualStyleBackColor = false;
            this.Btn_UjFelvetel.Click += new System.EventHandler(this.Btn_UjFelvetel_Click);
            // 
            // TxtBxUzem
            // 
            this.TxtBxUzem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TxtBxUzem.Location = new System.Drawing.Point(600, 212);
            this.TxtBxUzem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtBxUzem.Name = "TxtBxUzem";
            this.TxtBxUzem.Size = new System.Drawing.Size(170, 26);
            this.TxtBxUzem.TabIndex = 253;
            // 
            // Btn_Modosit
            // 
            this.Btn_Modosit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_Modosit.BackColor = System.Drawing.Color.Transparent;
            this.Btn_Modosit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Modosit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Modosit.ForeColor = System.Drawing.Color.Transparent;
            this.Btn_Modosit.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Modosit.Location = new System.Drawing.Point(590, 382);
            this.Btn_Modosit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Modosit.Name = "Btn_Modosit";
            this.Btn_Modosit.Size = new System.Drawing.Size(60, 62);
            this.Btn_Modosit.TabIndex = 252;
            this.toolTip1.SetToolTip(this.Btn_Modosit, "Üzemóra rögzítése");
            this.Btn_Modosit.UseVisualStyleBackColor = false;
            this.Btn_Modosit.Click += new System.EventHandler(this.Btn_Modosit_Click);
            // 
            // LblStatusz
            // 
            this.LblStatusz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblStatusz.AutoSize = true;
            this.LblStatusz.Location = new System.Drawing.Point(504, 328);
            this.LblStatusz.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblStatusz.Name = "LblStatusz";
            this.LblStatusz.Size = new System.Drawing.Size(68, 20);
            this.LblStatusz.TabIndex = 257;
            this.LblStatusz.Text = "Státusz:";
            // 
            // ChckBxStatus
            // 
            this.ChckBxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ChckBxStatus.AutoSize = true;
            this.ChckBxStatus.Location = new System.Drawing.Point(600, 328);
            this.ChckBxStatus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChckBxStatus.Name = "ChckBxStatus";
            this.ChckBxStatus.Size = new System.Drawing.Size(79, 24);
            this.ChckBxStatus.TabIndex = 258;
            this.ChckBxStatus.Text = "Törölve";
            this.ChckBxStatus.UseVisualStyleBackColor = true;
            // 
            // LblUzem
            // 
            this.LblUzem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblUzem.AutoSize = true;
            this.LblUzem.Location = new System.Drawing.Point(504, 212);
            this.LblUzem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblUzem.Name = "LblUzem";
            this.LblUzem.Size = new System.Drawing.Size(78, 20);
            this.LblUzem.TabIndex = 254;
            this.LblUzem.Text = "Üzemóra:";
            // 
            // LblDatum
            // 
            this.LblDatum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblDatum.AutoSize = true;
            this.LblDatum.Location = new System.Drawing.Point(504, 275);
            this.LblDatum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblDatum.Name = "LblDatum";
            this.LblDatum.Size = new System.Drawing.Size(61, 20);
            this.LblDatum.TabIndex = 255;
            this.LblDatum.Text = "Dátum:";
            // 
            // DtmPckr
            // 
            this.DtmPckr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DtmPckr.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtmPckr.Location = new System.Drawing.Point(600, 269);
            this.DtmPckr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DtmPckr.Name = "DtmPckr";
            this.DtmPckr.Size = new System.Drawing.Size(170, 26);
            this.DtmPckr.TabIndex = 256;
            // 
            // Btn_Pdf
            // 
            this.Btn_Pdf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_Pdf.BackColor = System.Drawing.Color.Transparent;
            this.Btn_Pdf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Pdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Pdf.ForeColor = System.Drawing.Color.Transparent;
            this.Btn_Pdf.Image = global::Villamos.Properties.Resources.pdf_32;
            this.Btn_Pdf.Location = new System.Drawing.Point(658, 382);
            this.Btn_Pdf.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Pdf.Name = "Btn_Pdf";
            this.Btn_Pdf.Size = new System.Drawing.Size(60, 62);
            this.Btn_Pdf.TabIndex = 262;
            this.toolTip1.SetToolTip(this.Btn_Pdf, "PDF készítés a táblázat adataiból");
            this.Btn_Pdf.UseVisualStyleBackColor = false;
            this.Btn_Pdf.Click += new System.EventHandler(this.Btn_Pdf_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Ablak_Eszterga_Karbantartás_Üzemóra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(797, 458);
            this.Controls.Add(this.Btn_Pdf);
            this.Controls.Add(this.Tabla);
            this.Controls.Add(this.Btn_Excel);
            this.Controls.Add(this.Btn_UjFelvetel);
            this.Controls.Add(this.TxtBxUzem);
            this.Controls.Add(this.Btn_Modosit);
            this.Controls.Add(this.LblStatusz);
            this.Controls.Add(this.ChckBxStatus);
            this.Controls.Add(this.LblUzem);
            this.Controls.Add(this.LblDatum);
            this.Controls.Add(this.DtmPckr);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimizeBox = false;
            this.Name = "Ablak_Eszterga_Karbantartás_Üzemóra";
            this.Text = "Ablak_Eszterga_Karbantartás_Üzemóra";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Eszterga_Karbantartás_Üzemóra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tabla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Zuby.ADGV.AdvancedDataGridView Tabla;
        internal System.Windows.Forms.Button Btn_Excel;
        internal System.Windows.Forms.Button Btn_UjFelvetel;
        internal System.Windows.Forms.TextBox TxtBxUzem;
        internal System.Windows.Forms.Button Btn_Modosit;
        internal System.Windows.Forms.Label LblStatusz;
        internal System.Windows.Forms.CheckBox ChckBxStatus;
        internal System.Windows.Forms.Label LblUzem;
        internal System.Windows.Forms.Label LblDatum;
        internal System.Windows.Forms.DateTimePicker DtmPckr;
        internal System.Windows.Forms.Button Btn_Pdf;
        internal System.Windows.Forms.ToolTip toolTip1;
    }
}