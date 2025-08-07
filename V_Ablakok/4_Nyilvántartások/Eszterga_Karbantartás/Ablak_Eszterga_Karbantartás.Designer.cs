namespace Villamos.Villamos_Ablakok._5_Karbantartás.Eszterga_Karbantartás
{
    partial class Ablak_Eszterga_Karbantartás
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Eszterga_Karbantartás));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Btn_Modositas = new System.Windows.Forms.Button();
            this.Btn_Excel = new System.Windows.Forms.Button();
            this.Btn_Frissit = new System.Windows.Forms.Button();
            this.Btn_Sugo = new System.Windows.Forms.Button();
            this.Btn_Rogzit = new System.Windows.Forms.Button();
            this.Btn_Naplo_Listaz = new System.Windows.Forms.Button();
            this.Btn_Pdf = new System.Windows.Forms.Button();
            this.GrpBx = new System.Windows.Forms.GroupBox();
            this.LblÜzemÁtlag = new System.Windows.Forms.Label();
            this.TxtBxNapiUzemoraAtlag = new System.Windows.Forms.TextBox();
            this.LblÁtlagÜzemóraSzám = new System.Windows.Forms.Label();
            this.TxtBxUzem = new System.Windows.Forms.TextBox();
            this.TxtBxNapi = new System.Windows.Forms.TextBox();
            this.LblNap = new System.Windows.Forms.Label();
            this.LblÜzemóra = new System.Windows.Forms.Label();
            this.DtmPckrEloTerv = new System.Windows.Forms.DateTimePicker();
            this.LblElőterv = new System.Windows.Forms.Label();
            this.Tabla = new Zuby.ADGV.AdvancedDataGridView();
            this.GrpBx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tabla)).BeginInit();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Btn_Modositas
            // 
            this.Btn_Modositas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Modositas.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Btn_Modositas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Modositas.Image = global::Villamos.Properties.Resources.Gear_01;
            this.Btn_Modositas.Location = new System.Drawing.Point(236, 22);
            this.Btn_Modositas.Name = "Btn_Modositas";
            this.Btn_Modositas.Size = new System.Drawing.Size(40, 40);
            this.Btn_Modositas.TabIndex = 2;
            this.toolTip1.SetToolTip(this.Btn_Modositas, "Módosítás");
            this.Btn_Modositas.UseVisualStyleBackColor = true;
            this.Btn_Modositas.Click += new System.EventHandler(this.Btn_Modositas_Click);
            // 
            // Btn_Excel
            // 
            this.Btn_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Btn_Excel.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Btn_Excel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Excel.Image = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Btn_Excel.Location = new System.Drawing.Point(98, 22);
            this.Btn_Excel.Name = "Btn_Excel";
            this.Btn_Excel.Size = new System.Drawing.Size(40, 40);
            this.Btn_Excel.TabIndex = 247;
            this.toolTip1.SetToolTip(this.Btn_Excel, "Excel táblázatot készít a táblázat adataiból");
            this.Btn_Excel.UseVisualStyleBackColor = true;
            this.Btn_Excel.Click += new System.EventHandler(this.Btn_Excel_Click);
            // 
            // Btn_Frissit
            // 
            this.Btn_Frissit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Frissit.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Btn_Frissit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Frissit.Image = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Btn_Frissit.Location = new System.Drawing.Point(6, 22);
            this.Btn_Frissit.Name = "Btn_Frissit";
            this.Btn_Frissit.Size = new System.Drawing.Size(40, 40);
            this.Btn_Frissit.TabIndex = 193;
            this.toolTip1.SetToolTip(this.Btn_Frissit, "Táblázat frissítése");
            this.Btn_Frissit.UseVisualStyleBackColor = true;
            this.Btn_Frissit.Click += new System.EventHandler(this.Btn_Frissit_Click);
            // 
            // Btn_Sugo
            // 
            this.Btn_Sugo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Sugo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Sugo.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Btn_Sugo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Sugo.Image = global::Villamos.Properties.Resources.Help_Support;
            this.Btn_Sugo.Location = new System.Drawing.Point(1123, 20);
            this.Btn_Sugo.Name = "Btn_Sugo";
            this.Btn_Sugo.Size = new System.Drawing.Size(45, 45);
            this.Btn_Sugo.TabIndex = 66;
            this.toolTip1.SetToolTip(this.Btn_Sugo, "Súgó");
            this.Btn_Sugo.UseVisualStyleBackColor = true;
            this.Btn_Sugo.Click += new System.EventHandler(this.Btn_Sugo_Click);
            // 
            // Btn_Rogzit
            // 
            this.Btn_Rogzit.BackColor = System.Drawing.Color.Tan;
            this.Btn_Rogzit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Rogzit.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Btn_Rogzit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Rogzit.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Rogzit.Location = new System.Drawing.Point(52, 22);
            this.Btn_Rogzit.Name = "Btn_Rogzit";
            this.Btn_Rogzit.Size = new System.Drawing.Size(40, 40);
            this.Btn_Rogzit.TabIndex = 194;
            this.toolTip1.SetToolTip(this.Btn_Rogzit, "Művelet Rügzítése");
            this.Btn_Rogzit.UseVisualStyleBackColor = false;
            this.Btn_Rogzit.Click += new System.EventHandler(this.Btn_Rogzit_Click);
            // 
            // Btn_Naplo_Listaz
            // 
            this.Btn_Naplo_Listaz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Naplo_Listaz.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Btn_Naplo_Listaz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Naplo_Listaz.Image = global::Villamos.Properties.Resources.Treetog_Junior_Document_scroll;
            this.Btn_Naplo_Listaz.Location = new System.Drawing.Point(190, 22);
            this.Btn_Naplo_Listaz.Name = "Btn_Naplo_Listaz";
            this.Btn_Naplo_Listaz.Size = new System.Drawing.Size(40, 40);
            this.Btn_Naplo_Listaz.TabIndex = 254;
            this.toolTip1.SetToolTip(this.Btn_Naplo_Listaz, "Napló listázása");
            this.Btn_Naplo_Listaz.UseVisualStyleBackColor = true;
            this.Btn_Naplo_Listaz.Click += new System.EventHandler(this.Btn_Naplo_Listaz_Click);
            // 
            // Btn_Pdf
            // 
            this.Btn_Pdf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Btn_Pdf.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Btn_Pdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Pdf.Image = global::Villamos.Properties.Resources.pdf_32;
            this.Btn_Pdf.Location = new System.Drawing.Point(144, 22);
            this.Btn_Pdf.Name = "Btn_Pdf";
            this.Btn_Pdf.Size = new System.Drawing.Size(40, 40);
            this.Btn_Pdf.TabIndex = 257;
            this.toolTip1.SetToolTip(this.Btn_Pdf, "PDF készítés a táblázat adataiból");
            this.Btn_Pdf.UseVisualStyleBackColor = true;
            this.Btn_Pdf.Click += new System.EventHandler(this.Btn_Pdf_Click);
            // 
            // GrpBx
            // 
            this.GrpBx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpBx.BackColor = System.Drawing.Color.Tan;
            this.GrpBx.Controls.Add(this.Btn_Pdf);
            this.GrpBx.Controls.Add(this.LblÜzemÁtlag);
            this.GrpBx.Controls.Add(this.TxtBxNapiUzemoraAtlag);
            this.GrpBx.Controls.Add(this.Btn_Naplo_Listaz);
            this.GrpBx.Controls.Add(this.LblÁtlagÜzemóraSzám);
            this.GrpBx.Controls.Add(this.TxtBxUzem);
            this.GrpBx.Controls.Add(this.TxtBxNapi);
            this.GrpBx.Controls.Add(this.LblNap);
            this.GrpBx.Controls.Add(this.LblÜzemóra);
            this.GrpBx.Controls.Add(this.DtmPckrEloTerv);
            this.GrpBx.Controls.Add(this.Btn_Modositas);
            this.GrpBx.Controls.Add(this.Btn_Excel);
            this.GrpBx.Controls.Add(this.LblElőterv);
            this.GrpBx.Controls.Add(this.Btn_Frissit);
            this.GrpBx.Controls.Add(this.Btn_Rogzit);
            this.GrpBx.Controls.Add(this.Btn_Sugo);
            this.GrpBx.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GrpBx.Location = new System.Drawing.Point(14, 12);
            this.GrpBx.Name = "GrpBx";
            this.GrpBx.Size = new System.Drawing.Size(1174, 78);
            this.GrpBx.TabIndex = 195;
            this.GrpBx.TabStop = false;
            // 
            // LblÜzemÁtlag
            // 
            this.LblÜzemÁtlag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LblÜzemÁtlag.AutoSize = true;
            this.LblÜzemÁtlag.Location = new System.Drawing.Point(299, 49);
            this.LblÜzemÁtlag.Name = "LblÜzemÁtlag";
            this.LblÜzemÁtlag.Size = new System.Drawing.Size(163, 20);
            this.LblÜzemÁtlag.TabIndex = 256;
            this.LblÜzemÁtlag.Text = "Üzemóra átlag napjai:";
            // 
            // TxtBxNapiUzemoraAtlag
            // 
            this.TxtBxNapiUzemoraAtlag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TxtBxNapiUzemoraAtlag.Location = new System.Drawing.Point(468, 49);
            this.TxtBxNapiUzemoraAtlag.Name = "TxtBxNapiUzemoraAtlag";
            this.TxtBxNapiUzemoraAtlag.Size = new System.Drawing.Size(43, 26);
            this.TxtBxNapiUzemoraAtlag.TabIndex = 255;
            this.TxtBxNapiUzemoraAtlag.Text = "30";
            this.TxtBxNapiUzemoraAtlag.TextChanged += new System.EventHandler(this.TxtBxNapiUzemoraAtlag_TextChanged);
            // 
            // LblÁtlagÜzemóraSzám
            // 
            this.LblÁtlagÜzemóraSzám.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LblÁtlagÜzemóraSzám.AutoSize = true;
            this.LblÁtlagÜzemóraSzám.Location = new System.Drawing.Point(531, 52);
            this.LblÁtlagÜzemóraSzám.Name = "LblÁtlagÜzemóraSzám";
            this.LblÁtlagÜzemóraSzám.Size = new System.Drawing.Size(0, 20);
            this.LblÁtlagÜzemóraSzám.TabIndex = 253;
            // 
            // TxtBxUzem
            // 
            this.TxtBxUzem.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.TxtBxUzem.Location = new System.Drawing.Point(988, 46);
            this.TxtBxUzem.Name = "TxtBxUzem";
            this.TxtBxUzem.Size = new System.Drawing.Size(100, 26);
            this.TxtBxUzem.TabIndex = 252;
            this.TxtBxUzem.Text = "8";
            this.TxtBxUzem.TextChanged += new System.EventHandler(this.TxtBxUzem_TextChanged);
            // 
            // TxtBxNapi
            // 
            this.TxtBxNapi.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.TxtBxNapi.Location = new System.Drawing.Point(988, 17);
            this.TxtBxNapi.Name = "TxtBxNapi";
            this.TxtBxNapi.Size = new System.Drawing.Size(100, 26);
            this.TxtBxNapi.TabIndex = 251;
            this.TxtBxNapi.Text = "5";
            this.TxtBxNapi.TextChanged += new System.EventHandler(this.TxtBxNapi_TextChanged);
            // 
            // LblNap
            // 
            this.LblNap.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LblNap.AutoSize = true;
            this.LblNap.Location = new System.Drawing.Point(835, 20);
            this.LblNap.Name = "LblNap";
            this.LblNap.Size = new System.Drawing.Size(147, 20);
            this.LblNap.TabIndex = 249;
            this.LblNap.Text = "Napi figyelmeztetés";
            // 
            // LblÜzemóra
            // 
            this.LblÜzemóra.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.LblÜzemóra.AutoSize = true;
            this.LblÜzemóra.Location = new System.Drawing.Point(802, 45);
            this.LblÜzemóra.Name = "LblÜzemóra";
            this.LblÜzemóra.Size = new System.Drawing.Size(180, 20);
            this.LblÜzemóra.TabIndex = 248;
            this.LblÜzemóra.Text = "Üzemóra figyelmeztetés";
            // 
            // DtmPckrEloTerv
            // 
            this.DtmPckrEloTerv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DtmPckrEloTerv.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtmPckrEloTerv.Location = new System.Drawing.Point(415, 20);
            this.DtmPckrEloTerv.Name = "DtmPckrEloTerv";
            this.DtmPckrEloTerv.Size = new System.Drawing.Size(139, 26);
            this.DtmPckrEloTerv.TabIndex = 198;
            this.DtmPckrEloTerv.ValueChanged += new System.EventHandler(this.DtmPckrEloTerv_ValueChanged);
            // 
            // LblElőterv
            // 
            this.LblElőterv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LblElőterv.AutoSize = true;
            this.LblElőterv.Location = new System.Drawing.Point(299, 22);
            this.LblElőterv.Name = "LblElőterv";
            this.LblElőterv.Size = new System.Drawing.Size(110, 20);
            this.LblElőterv.TabIndex = 197;
            this.LblElőterv.Text = "Előre tervezés";
            // 
            // Tabla
            // 
            this.Tabla.AllowUserToAddRows = false;
            this.Tabla.AllowUserToDeleteRows = false;
            this.Tabla.AllowUserToResizeRows = false;
            this.Tabla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tabla.FilterAndSortEnabled = true;
            this.Tabla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tabla.Location = new System.Drawing.Point(14, 97);
            this.Tabla.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Tabla.MaxFilterButtonImageHeight = 23;
            this.Tabla.Name = "Tabla";
            this.Tabla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tabla.RowHeadersVisible = false;
            this.Tabla.RowHeadersWidth = 62;
            this.Tabla.RowTemplate.Height = 28;
            this.Tabla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Tabla.Size = new System.Drawing.Size(1174, 596);
            this.Tabla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tabla.TabIndex = 196;
            this.Tabla.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tabla_CellEndEdit);
            this.Tabla.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.Tabla_DataBindingComplete);
            // 
            // Ablak_Eszterga_Karbantartás
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.ClientSize = new System.Drawing.Size(1200, 706);
            this.Controls.Add(this.Tabla);
            this.Controls.Add(this.GrpBx);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Eszterga_Karbantartás";
            this.Text = "Kerékeszterga Műveletek";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_Eszterga_Karbantartas_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Eszterga_Karbantartás_Load);
            this.GrpBx.ResumeLayout(false);
            this.GrpBx.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tabla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Button Btn_Sugo;
        internal System.Windows.Forms.Button Btn_Frissit;
        internal System.Windows.Forms.Button Btn_Modositas;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox GrpBx;
        internal System.Windows.Forms.Button Btn_Excel;
        private System.Windows.Forms.Label LblElőterv;
        private System.Windows.Forms.DateTimePicker DtmPckrEloTerv;
        private System.Windows.Forms.Button Btn_Rogzit;
        private System.Windows.Forms.Label LblNap;
        private System.Windows.Forms.Label LblÜzemóra;
        private System.Windows.Forms.Label LblÁtlagÜzemóraSzám;
        private System.Windows.Forms.TextBox TxtBxUzem;
        private System.Windows.Forms.TextBox TxtBxNapi;
        internal System.Windows.Forms.Button Btn_Naplo_Listaz;
        internal Zuby.ADGV.AdvancedDataGridView Tabla;
        private System.Windows.Forms.TextBox TxtBxNapiUzemoraAtlag;
        private System.Windows.Forms.Label LblÜzemÁtlag;
        internal System.Windows.Forms.Button Btn_Pdf;
    }
}