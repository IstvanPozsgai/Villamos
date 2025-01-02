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
            this.Btn_Módosítás = new System.Windows.Forms.Button();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Btn_Súgó = new System.Windows.Forms.Button();
            this.Btn_Frissít = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Btn_Rögzít = new System.Windows.Forms.Button();
            this.Btn_Excel = new System.Windows.Forms.Button();
            this.GrpBx = new System.Windows.Forms.GroupBox();
            this.DtmPckrElőTerv = new System.Windows.Forms.DateTimePicker();
            this.LblElőterv = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.GrpBx.SuspendLayout();
            this.SuspendLayout();
            // 
            // Btn_Módosítás
            // 
            this.Btn_Módosítás.BackgroundImage = global::Villamos.Properties.Resources.Gear_01;
            this.Btn_Módosítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Módosítás.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Btn_Módosítás.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Módosítás.Location = new System.Drawing.Point(181, 17);
            this.Btn_Módosítás.Name = "Btn_Módosítás";
            this.Btn_Módosítás.Size = new System.Drawing.Size(40, 40);
            this.Btn_Módosítás.TabIndex = 2;
            this.toolTip1.SetToolTip(this.Btn_Módosítás, "Módosítás");
            this.Btn_Módosítás.UseVisualStyleBackColor = true;
            this.Btn_Módosítás.Click += new System.EventHandler(this.Btn_Módosítás_Click);
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Location = new System.Drawing.Point(14, 85);
            this.Tábla.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.RowHeadersWidth = 62;
            this.Tábla.RowTemplate.Height = 28;
            this.Tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Tábla.Size = new System.Drawing.Size(1174, 594);
            this.Tábla.TabIndex = 3;
            // 
            // Btn_Súgó
            // 
            this.Btn_Súgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Súgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Btn_Súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Súgó.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Btn_Súgó.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Súgó.Location = new System.Drawing.Point(1111, 9);
            this.Btn_Súgó.Name = "Btn_Súgó";
            this.Btn_Súgó.Size = new System.Drawing.Size(45, 45);
            this.Btn_Súgó.TabIndex = 66;
            this.toolTip1.SetToolTip(this.Btn_Súgó, "Súgó");
            this.Btn_Súgó.UseVisualStyleBackColor = true;
            this.Btn_Súgó.Click += new System.EventHandler(this.Btn_Súgó_Click);
            // 
            // Btn_Frissít
            // 
            this.Btn_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Btn_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Frissít.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Btn_Frissít.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Frissít.Location = new System.Drawing.Point(6, 17);
            this.Btn_Frissít.Name = "Btn_Frissít";
            this.Btn_Frissít.Size = new System.Drawing.Size(40, 40);
            this.Btn_Frissít.TabIndex = 193;
            this.toolTip1.SetToolTip(this.Btn_Frissít, "Táblázat frissítése");
            this.Btn_Frissít.UseVisualStyleBackColor = true;
            this.Btn_Frissít.Click += new System.EventHandler(this.Btn_Frissít_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Btn_Rögzít
            // 
            this.Btn_Rögzít.BackColor = System.Drawing.Color.Tan;
            this.Btn_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Rögzít.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Btn_Rögzít.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Rögzít.Location = new System.Drawing.Point(61, 17);
            this.Btn_Rögzít.Name = "Btn_Rögzít";
            this.Btn_Rögzít.Size = new System.Drawing.Size(40, 40);
            this.Btn_Rögzít.TabIndex = 194;
            this.toolTip1.SetToolTip(this.Btn_Rögzít, "Művelet Rügzítése");
            this.Btn_Rögzít.UseVisualStyleBackColor = false;
            this.Btn_Rögzít.Click += new System.EventHandler(this.Btn_Rögzít_Click);
            // 
            // Btn_Excel
            // 
            this.Btn_Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Btn_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Excel.FlatAppearance.BorderColor = System.Drawing.Color.Tan;
            this.Btn_Excel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Excel.Location = new System.Drawing.Point(117, 17);
            this.Btn_Excel.Name = "Btn_Excel";
            this.Btn_Excel.Size = new System.Drawing.Size(40, 40);
            this.Btn_Excel.TabIndex = 247;
            this.toolTip1.SetToolTip(this.Btn_Excel, "Excel táblázatot készít a táblázat adataiból");
            this.Btn_Excel.UseVisualStyleBackColor = true;
            this.Btn_Excel.Click += new System.EventHandler(this.Btn_Excel_Click);
            // 
            // GrpBx
            // 
            this.GrpBx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpBx.BackColor = System.Drawing.Color.Tan;
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
            this.GrpBx.Size = new System.Drawing.Size(1174, 63);
            this.GrpBx.TabIndex = 195;
            this.GrpBx.TabStop = false;
            // 
            // DtmPckrElőTerv
            // 
            this.DtmPckrElőTerv.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.DtmPckrElőTerv.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtmPckrElőTerv.Location = new System.Drawing.Point(548, 22);
            this.DtmPckrElőTerv.Name = "DtmPckrElőTerv";
            this.DtmPckrElőTerv.Size = new System.Drawing.Size(139, 26);
            this.DtmPckrElőTerv.TabIndex = 198;
            this.DtmPckrElőTerv.ValueChanged += new System.EventHandler(this.DtmPckrElőTerv_ValueChanged);
            // 
            // LblElőterv
            // 
            this.LblElőterv.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.LblElőterv.AutoSize = true;
            this.LblElőterv.Location = new System.Drawing.Point(406, 27);
            this.LblElőterv.Name = "LblElőterv";
            this.LblElőterv.Size = new System.Drawing.Size(110, 20);
            this.LblElőterv.TabIndex = 197;
            this.LblElőterv.Text = "Előre tervezés";
            // 
            // Ablak_Eszterga_Karbantartás
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(200)))), ((int)(((byte)(184)))));
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.GrpBx);
            this.Controls.Add(this.Tábla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Eszterga_Karbantartás";
            this.Text = "Ablak_Eszterga_Karbantartás";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_Eszterga_Karbantartás_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Eszterga_Karbantartás_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.GrpBx.ResumeLayout(false);
            this.GrpBx.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView Tábla;
        internal System.Windows.Forms.Button Btn_Súgó;
        internal System.Windows.Forms.Button Btn_Frissít;
        internal System.Windows.Forms.Button Btn_Módosítás;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button Btn_Rögzít;
        private System.Windows.Forms.GroupBox GrpBx;
        internal System.Windows.Forms.Button Btn_Excel;
        private System.Windows.Forms.Label LblElőterv;
        private System.Windows.Forms.DateTimePicker DtmPckrElőTerv;
    }
}