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
            this.Tabla = new Zuby.ADGV.AdvancedDataGridView();
            this.Btn_Excel = new System.Windows.Forms.Button();
            this.Btn_ÚjFelvétel = new System.Windows.Forms.Button();
            this.TxtBxÜzem = new System.Windows.Forms.TextBox();
            this.Btn_Módosít = new System.Windows.Forms.Button();
            this.LblStátuszÜzem = new System.Windows.Forms.Label();
            this.ChckBxStátus = new System.Windows.Forms.CheckBox();
            this.LblÜzem = new System.Windows.Forms.Label();
            this.LblDátum = new System.Windows.Forms.Label();
            this.DtmPckrDátum = new System.Windows.Forms.DateTimePicker();
            this.Btn_Pdf = new System.Windows.Forms.Button();
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
            this.Tabla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            this.Tabla.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Tábla_CellFormatting);
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
            this.Btn_Excel.UseVisualStyleBackColor = false;
            this.Btn_Excel.Click += new System.EventHandler(this.Btn_Excel_Click);
            // 
            // Btn_ÚjFelvétel
            // 
            this.Btn_ÚjFelvétel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_ÚjFelvétel.BackColor = System.Drawing.Color.Transparent;
            this.Btn_ÚjFelvétel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_ÚjFelvétel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_ÚjFelvétel.ForeColor = System.Drawing.Color.Transparent;
            this.Btn_ÚjFelvétel.Image = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Btn_ÚjFelvétel.Location = new System.Drawing.Point(522, 382);
            this.Btn_ÚjFelvétel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_ÚjFelvétel.Name = "Btn_ÚjFelvétel";
            this.Btn_ÚjFelvétel.Size = new System.Drawing.Size(60, 62);
            this.Btn_ÚjFelvétel.TabIndex = 259;
            this.Btn_ÚjFelvétel.UseVisualStyleBackColor = false;
            this.Btn_ÚjFelvétel.Click += new System.EventHandler(this.Btn_ÚjFelvétel_Click);
            // 
            // TxtBxÜzem
            // 
            this.TxtBxÜzem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TxtBxÜzem.Location = new System.Drawing.Point(600, 212);
            this.TxtBxÜzem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtBxÜzem.Name = "TxtBxÜzem";
            this.TxtBxÜzem.Size = new System.Drawing.Size(170, 26);
            this.TxtBxÜzem.TabIndex = 253;
            // 
            // Btn_Módosít
            // 
            this.Btn_Módosít.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_Módosít.BackColor = System.Drawing.Color.Transparent;
            this.Btn_Módosít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Módosít.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Módosít.ForeColor = System.Drawing.Color.Transparent;
            this.Btn_Módosít.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Módosít.Location = new System.Drawing.Point(590, 382);
            this.Btn_Módosít.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Módosít.Name = "Btn_Módosít";
            this.Btn_Módosít.Size = new System.Drawing.Size(60, 62);
            this.Btn_Módosít.TabIndex = 252;
            this.Btn_Módosít.UseVisualStyleBackColor = false;
            this.Btn_Módosít.Click += new System.EventHandler(this.Btn_Módosít_Click);
            // 
            // LblStátuszÜzem
            // 
            this.LblStátuszÜzem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblStátuszÜzem.AutoSize = true;
            this.LblStátuszÜzem.Location = new System.Drawing.Point(504, 328);
            this.LblStátuszÜzem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblStátuszÜzem.Name = "LblStátuszÜzem";
            this.LblStátuszÜzem.Size = new System.Drawing.Size(68, 20);
            this.LblStátuszÜzem.TabIndex = 257;
            this.LblStátuszÜzem.Text = "Státusz:";
            // 
            // ChckBxStátus
            // 
            this.ChckBxStátus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ChckBxStátus.AutoSize = true;
            this.ChckBxStátus.Location = new System.Drawing.Point(600, 328);
            this.ChckBxStátus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChckBxStátus.Name = "ChckBxStátus";
            this.ChckBxStátus.Size = new System.Drawing.Size(79, 24);
            this.ChckBxStátus.TabIndex = 258;
            this.ChckBxStátus.Text = "Törölve";
            this.ChckBxStátus.UseVisualStyleBackColor = true;
            // 
            // LblÜzem
            // 
            this.LblÜzem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblÜzem.AutoSize = true;
            this.LblÜzem.Location = new System.Drawing.Point(504, 212);
            this.LblÜzem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblÜzem.Name = "LblÜzem";
            this.LblÜzem.Size = new System.Drawing.Size(78, 20);
            this.LblÜzem.TabIndex = 254;
            this.LblÜzem.Text = "Üzemóra:";
            // 
            // LblDátum
            // 
            this.LblDátum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblDátum.AutoSize = true;
            this.LblDátum.Location = new System.Drawing.Point(504, 275);
            this.LblDátum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblDátum.Name = "LblDátum";
            this.LblDátum.Size = new System.Drawing.Size(61, 20);
            this.LblDátum.TabIndex = 255;
            this.LblDátum.Text = "Dátum:";
            // 
            // DtmPckrDátum
            // 
            this.DtmPckrDátum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DtmPckrDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtmPckrDátum.Location = new System.Drawing.Point(600, 269);
            this.DtmPckrDátum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DtmPckrDátum.Name = "DtmPckrDátum";
            this.DtmPckrDátum.Size = new System.Drawing.Size(170, 26);
            this.DtmPckrDátum.TabIndex = 256;
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
            this.Btn_Pdf.UseVisualStyleBackColor = false;
            this.Btn_Pdf.Click += new System.EventHandler(this.Btn_Pdf_Click);
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
            this.Controls.Add(this.Btn_ÚjFelvétel);
            this.Controls.Add(this.TxtBxÜzem);
            this.Controls.Add(this.Btn_Módosít);
            this.Controls.Add(this.LblStátuszÜzem);
            this.Controls.Add(this.ChckBxStátus);
            this.Controls.Add(this.LblÜzem);
            this.Controls.Add(this.LblDátum);
            this.Controls.Add(this.DtmPckrDátum);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
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
        internal System.Windows.Forms.Button Btn_ÚjFelvétel;
        internal System.Windows.Forms.TextBox TxtBxÜzem;
        internal System.Windows.Forms.Button Btn_Módosít;
        internal System.Windows.Forms.Label LblStátuszÜzem;
        internal System.Windows.Forms.CheckBox ChckBxStátus;
        internal System.Windows.Forms.Label LblÜzem;
        internal System.Windows.Forms.Label LblDátum;
        internal System.Windows.Forms.DateTimePicker DtmPckrDátum;
        internal System.Windows.Forms.Button Btn_Pdf;
    }
}