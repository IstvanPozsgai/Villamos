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
            this.Btn_Módosítás = new System.Windows.Forms.Button();
            this.Btn_Excel = new System.Windows.Forms.Button();
            this.Btn_Frissít = new System.Windows.Forms.Button();
            this.Btn_Súgó = new System.Windows.Forms.Button();
            this.Btn_Rögzít = new System.Windows.Forms.Button();
            this.Bttn_Napló_Listáz = new System.Windows.Forms.Button();
            this.Btn_Pdf = new System.Windows.Forms.Button();
            this.GrpBx = new System.Windows.Forms.GroupBox();
            this.LblÜzemÁtlag = new System.Windows.Forms.Label();
            this.TxtBxNapiUzemoraAtlag = new System.Windows.Forms.TextBox();
            this.LblÁtlagÜzemóraSzám = new System.Windows.Forms.Label();
            this.TxtBxÜzem = new System.Windows.Forms.TextBox();
            this.TxtBxNapi = new System.Windows.Forms.TextBox();
            this.LblNap = new System.Windows.Forms.Label();
            this.LblÜzemóra = new System.Windows.Forms.Label();
            this.DtmPckrElőTerv = new System.Windows.Forms.DateTimePicker();
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
            // Btn_Módosítás
            // 
            this.Btn_Módosítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Módosítás.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Btn_Módosítás.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Módosítás.Image = global::Villamos.Properties.Resources.Gear_01;
            this.Btn_Módosítás.Location = new System.Drawing.Point(236, 22);
            this.Btn_Módosítás.Name = "Btn_Módosítás";
            this.Btn_Módosítás.Size = new System.Drawing.Size(40, 40);
            this.Btn_Módosítás.TabIndex = 2;
            this.toolTip1.SetToolTip(this.Btn_Módosítás, "Módosítás");
            this.Btn_Módosítás.UseVisualStyleBackColor = true;
            this.Btn_Módosítás.Click += new System.EventHandler(this.Btn_Módosítás_Click);
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
            // Btn_Frissít
            // 
            this.Btn_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Frissít.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Btn_Frissít.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Frissít.Image = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Btn_Frissít.Location = new System.Drawing.Point(6, 22);
            this.Btn_Frissít.Name = "Btn_Frissít";
            this.Btn_Frissít.Size = new System.Drawing.Size(40, 40);
            this.Btn_Frissít.TabIndex = 193;
            this.toolTip1.SetToolTip(this.Btn_Frissít, "Táblázat frissítése");
            this.Btn_Frissít.UseVisualStyleBackColor = true;
            this.Btn_Frissít.Click += new System.EventHandler(this.Btn_Frissít_Click);
            // 
            // Btn_Súgó
            // 
            this.Btn_Súgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Súgó.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Btn_Súgó.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Súgó.Image = global::Villamos.Properties.Resources.Help_Support;
            this.Btn_Súgó.Location = new System.Drawing.Point(1123, 20);
            this.Btn_Súgó.Name = "Btn_Súgó";
            this.Btn_Súgó.Size = new System.Drawing.Size(45, 45);
            this.Btn_Súgó.TabIndex = 66;
            this.toolTip1.SetToolTip(this.Btn_Súgó, "Súgó");
            this.Btn_Súgó.UseVisualStyleBackColor = true;
            this.Btn_Súgó.Click += new System.EventHandler(this.Btn_Súgó_Click);
            // 
            // Btn_Rögzít
            // 
            this.Btn_Rögzít.BackColor = System.Drawing.Color.Tan;
            this.Btn_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Rögzít.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Btn_Rögzít.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Rögzít.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Rögzít.Location = new System.Drawing.Point(52, 22);
            this.Btn_Rögzít.Name = "Btn_Rögzít";
            this.Btn_Rögzít.Size = new System.Drawing.Size(40, 40);
            this.Btn_Rögzít.TabIndex = 194;
            this.toolTip1.SetToolTip(this.Btn_Rögzít, "Művelet Rügzítése");
            this.Btn_Rögzít.UseVisualStyleBackColor = false;
            this.Btn_Rögzít.Click += new System.EventHandler(this.Btn_Rögzít_Click);
            // 
            // Bttn_Napló_Listáz
            // 
            this.Bttn_Napló_Listáz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Bttn_Napló_Listáz.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Bttn_Napló_Listáz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Bttn_Napló_Listáz.Image = global::Villamos.Properties.Resources.Treetog_Junior_Document_scroll;
            this.Bttn_Napló_Listáz.Location = new System.Drawing.Point(190, 22);
            this.Bttn_Napló_Listáz.Name = "Bttn_Napló_Listáz";
            this.Bttn_Napló_Listáz.Size = new System.Drawing.Size(40, 40);
            this.Bttn_Napló_Listáz.TabIndex = 254;
            this.toolTip1.SetToolTip(this.Bttn_Napló_Listáz, "Napló listázása");
            this.Bttn_Napló_Listáz.UseVisualStyleBackColor = true;
            this.Bttn_Napló_Listáz.Click += new System.EventHandler(this.Btn_Napló_Listáz_Click);
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
            this.toolTip1.SetToolTip(this.Btn_Pdf, "Excel táblázatot készít a táblázat adataiból");
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
            this.GrpBx.Controls.Add(this.Bttn_Napló_Listáz);
            this.GrpBx.Controls.Add(this.LblÁtlagÜzemóraSzám);
            this.GrpBx.Controls.Add(this.TxtBxÜzem);
            this.GrpBx.Controls.Add(this.TxtBxNapi);
            this.GrpBx.Controls.Add(this.LblNap);
            this.GrpBx.Controls.Add(this.LblÜzemóra);
            this.GrpBx.Controls.Add(this.DtmPckrElőTerv);
            this.GrpBx.Controls.Add(this.Btn_Módosítás);
            this.GrpBx.Controls.Add(this.Btn_Excel);
            this.GrpBx.Controls.Add(this.LblElőterv);
            this.GrpBx.Controls.Add(this.Btn_Frissít);
            this.GrpBx.Controls.Add(this.Btn_Rögzít);
            this.GrpBx.Controls.Add(this.Btn_Súgó);
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
            // TxtBxÜzem
            // 
            this.TxtBxÜzem.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.TxtBxÜzem.Location = new System.Drawing.Point(988, 46);
            this.TxtBxÜzem.Name = "TxtBxÜzem";
            this.TxtBxÜzem.Size = new System.Drawing.Size(100, 26);
            this.TxtBxÜzem.TabIndex = 252;
            this.TxtBxÜzem.Text = "8";
            this.TxtBxÜzem.TextChanged += new System.EventHandler(this.TxtBxÜzem_TextChanged);
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
            // DtmPckrElőTerv
            // 
            this.DtmPckrElőTerv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DtmPckrElőTerv.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtmPckrElőTerv.Location = new System.Drawing.Point(415, 20);
            this.DtmPckrElőTerv.Name = "DtmPckrElőTerv";
            this.DtmPckrElőTerv.Size = new System.Drawing.Size(139, 26);
            this.DtmPckrElőTerv.TabIndex = 198;
            this.DtmPckrElőTerv.ValueChanged += new System.EventHandler(this.DtmPckrElőTerv_ValueChanged);
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
            this.Tabla.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellEndEdit);
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
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_Eszterga_Karbantartás_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Eszterga_Karbantartás_Load);
            this.GrpBx.ResumeLayout(false);
            this.GrpBx.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tabla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Button Btn_Súgó;
        internal System.Windows.Forms.Button Btn_Frissít;
        internal System.Windows.Forms.Button Btn_Módosítás;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox GrpBx;
        internal System.Windows.Forms.Button Btn_Excel;
        private System.Windows.Forms.Label LblElőterv;
        private System.Windows.Forms.DateTimePicker DtmPckrElőTerv;
        private System.Windows.Forms.Button Btn_Rögzít;
        private System.Windows.Forms.Label LblNap;
        private System.Windows.Forms.Label LblÜzemóra;
        private System.Windows.Forms.Label LblÁtlagÜzemóraSzám;
        private System.Windows.Forms.TextBox TxtBxÜzem;
        private System.Windows.Forms.TextBox TxtBxNapi;
        internal System.Windows.Forms.Button Bttn_Napló_Listáz;
        internal Zuby.ADGV.AdvancedDataGridView Tabla;
        private System.Windows.Forms.TextBox TxtBxNapiUzemoraAtlag;
        private System.Windows.Forms.Label LblÜzemÁtlag;
        internal System.Windows.Forms.Button Btn_Pdf;
    }
}