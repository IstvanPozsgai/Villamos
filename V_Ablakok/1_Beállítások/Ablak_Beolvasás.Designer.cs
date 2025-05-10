namespace Villamos
{
    partial class Ablak_Beolvasás
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.SAPTábla = new System.Windows.Forms.DataGridView();
            this.Label67 = new System.Windows.Forms.Label();
            this.Label68 = new System.Windows.Forms.Label();
            this.Változónév = new System.Windows.Forms.TextBox();
            this.Label60 = new System.Windows.Forms.Label();
            this.SAPFejléc = new System.Windows.Forms.TextBox();
            this.SAPOSzlopszám = new System.Windows.Forms.TextBox();
            this.SAPCsoport = new System.Windows.Forms.ComboBox();
            this.Label69 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.FejlécBeolvasása = new System.Windows.Forms.Button();
            this.SAPRögzít = new System.Windows.Forms.Button();
            this.SAPTöröl = new System.Windows.Forms.Button();
            this.SAPExcel = new System.Windows.Forms.Button();
            this.SAPFrissít = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SAPTábla)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SAPTábla
            // 
            this.SAPTábla.AllowUserToAddRows = false;
            this.SAPTábla.AllowUserToDeleteRows = false;
            this.SAPTábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.SAPTábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.SAPTábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SAPTábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.SAPTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SAPTábla.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.SAPTábla.EnableHeadersVisualStyles = false;
            this.SAPTábla.Location = new System.Drawing.Point(12, 229);
            this.SAPTábla.Name = "SAPTábla";
            this.SAPTábla.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SAPTábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.SAPTábla.RowHeadersWidth = 20;
            this.SAPTábla.Size = new System.Drawing.Size(822, 254);
            this.SAPTábla.TabIndex = 111;
            this.SAPTábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.SAPTábla_CellClick);
            this.SAPTábla.SelectionChanged += new System.EventHandler(this.Tábla_SelectionChanged);
            // 
            // Label67
            // 
            this.Label67.AutoSize = true;
            this.Label67.Location = new System.Drawing.Point(3, 105);
            this.Label67.Name = "Label67";
            this.Label67.Size = new System.Drawing.Size(92, 20);
            this.Label67.TabIndex = 55;
            this.Label67.Text = "Változónév:";
            // 
            // Label68
            // 
            this.Label68.AutoSize = true;
            this.Label68.Location = new System.Drawing.Point(3, 35);
            this.Label68.Name = "Label68";
            this.Label68.Size = new System.Drawing.Size(113, 20);
            this.Label68.TabIndex = 53;
            this.Label68.Text = "Oszlop száma:";
            // 
            // Változónév
            // 
            this.Változónév.Location = new System.Drawing.Point(155, 108);
            this.Változónév.MaxLength = 50;
            this.Változónév.Name = "Változónév";
            this.Változónév.Size = new System.Drawing.Size(187, 26);
            this.Változónév.TabIndex = 3;
            // 
            // Label60
            // 
            this.Label60.AutoSize = true;
            this.Label60.Location = new System.Drawing.Point(3, 70);
            this.Label60.Name = "Label60";
            this.Label60.Size = new System.Drawing.Size(118, 20);
            this.Label60.TabIndex = 54;
            this.Label60.Text = "Fejléc szövege:";
            // 
            // SAPFejléc
            // 
            this.SAPFejléc.Location = new System.Drawing.Point(155, 73);
            this.SAPFejléc.MaxLength = 255;
            this.SAPFejléc.Name = "SAPFejléc";
            this.SAPFejléc.Size = new System.Drawing.Size(459, 26);
            this.SAPFejléc.TabIndex = 2;
            // 
            // SAPOSzlopszám
            // 
            this.SAPOSzlopszám.Location = new System.Drawing.Point(155, 38);
            this.SAPOSzlopszám.Name = "SAPOSzlopszám";
            this.SAPOSzlopszám.Size = new System.Drawing.Size(187, 26);
            this.SAPOSzlopszám.TabIndex = 1;
            // 
            // SAPCsoport
            // 
            this.SAPCsoport.FormattingEnabled = true;
            this.SAPCsoport.Location = new System.Drawing.Point(155, 3);
            this.SAPCsoport.MaxLength = 10;
            this.SAPCsoport.Name = "SAPCsoport";
            this.SAPCsoport.Size = new System.Drawing.Size(187, 28);
            this.SAPCsoport.TabIndex = 0;
            this.SAPCsoport.SelectedIndexChanged += new System.EventHandler(this.Csoport_SelectedIndexChanged);
            // 
            // Label69
            // 
            this.Label69.AutoSize = true;
            this.Label69.Location = new System.Drawing.Point(3, 0);
            this.Label69.Name = "Label69";
            this.Label69.Size = new System.Drawing.Size(146, 20);
            this.Label69.TabIndex = 48;
            this.Label69.Text = "Beolvasási csoport:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.FejlécBeolvasása, 5, 4);
            this.tableLayoutPanel1.Controls.Add(this.Label69, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.SAPCsoport, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Label68, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Label67, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.SAPOSzlopszám, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Label60, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.SAPFejléc, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.Változónév, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.SAPRögzít, 4, 3);
            this.tableLayoutPanel1.Controls.Add(this.SAPTöröl, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.SAPExcel, 3, 4);
            this.tableLayoutPanel1.Controls.Add(this.SAPFrissít, 4, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(823, 211);
            this.tableLayoutPanel1.TabIndex = 112;
            // 
            // FejlécBeolvasása
            // 
            this.FejlécBeolvasása.BackColor = System.Drawing.SystemColors.Control;
            this.FejlécBeolvasása.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FejlécBeolvasása.Image = global::Villamos.Properties.Resources.Document_Microsoft_Excel_01;
            this.FejlécBeolvasása.Location = new System.Drawing.Point(773, 159);
            this.FejlécBeolvasása.Name = "FejlécBeolvasása";
            this.FejlécBeolvasása.Size = new System.Drawing.Size(45, 45);
            this.FejlécBeolvasása.TabIndex = 56;
            this.toolTip1.SetToolTip(this.FejlécBeolvasása, "Excel tábla alapján beolvassa a fejlécet");
            this.FejlécBeolvasása.UseVisualStyleBackColor = false;
            this.FejlécBeolvasása.Click += new System.EventHandler(this.FejlécBeolvasása_Click);
            // 
            // SAPRögzít
            // 
            this.SAPRögzít.BackColor = System.Drawing.SystemColors.Control;
            this.SAPRögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SAPRögzít.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.SAPRögzít.Location = new System.Drawing.Point(722, 108);
            this.SAPRögzít.Name = "SAPRögzít";
            this.SAPRögzít.Size = new System.Drawing.Size(45, 45);
            this.SAPRögzít.TabIndex = 4;
            this.toolTip1.SetToolTip(this.SAPRögzít, "Rögzíti az adatokat");
            this.SAPRögzít.UseVisualStyleBackColor = false;
            this.SAPRögzít.Click += new System.EventHandler(this.Rögzít_Click);
            // 
            // SAPTöröl
            // 
            this.SAPTöröl.BackColor = System.Drawing.SystemColors.Control;
            this.SAPTöröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SAPTöröl.Image = global::Villamos.Properties.Resources.Kuka;
            this.SAPTöröl.Location = new System.Drawing.Point(620, 159);
            this.SAPTöröl.Name = "SAPTöröl";
            this.SAPTöröl.Size = new System.Drawing.Size(45, 45);
            this.SAPTöröl.TabIndex = 5;
            this.toolTip1.SetToolTip(this.SAPTöröl, "Törli a megjelenített értékeket");
            this.SAPTöröl.UseVisualStyleBackColor = false;
            this.SAPTöröl.Click += new System.EventHandler(this.Töröl_Click);
            // 
            // SAPExcel
            // 
            this.SAPExcel.BackColor = System.Drawing.SystemColors.Control;
            this.SAPExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SAPExcel.Image = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.SAPExcel.Location = new System.Drawing.Point(671, 159);
            this.SAPExcel.Name = "SAPExcel";
            this.SAPExcel.Size = new System.Drawing.Size(45, 45);
            this.SAPExcel.TabIndex = 7;
            this.toolTip1.SetToolTip(this.SAPExcel, "Excel kimetetet készít");
            this.SAPExcel.UseVisualStyleBackColor = false;
            this.SAPExcel.Click += new System.EventHandler(this.Excel_Click);
            // 
            // SAPFrissít
            // 
            this.SAPFrissít.BackColor = System.Drawing.SystemColors.Control;
            this.SAPFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SAPFrissít.Image = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.SAPFrissít.Location = new System.Drawing.Point(722, 159);
            this.SAPFrissít.Name = "SAPFrissít";
            this.SAPFrissít.Size = new System.Drawing.Size(45, 45);
            this.SAPFrissít.TabIndex = 6;
            this.toolTip1.SetToolTip(this.SAPFrissít, "Táblázar adatait frissíti");
            this.SAPFrissít.UseVisualStyleBackColor = false;
            this.SAPFrissít.Click += new System.EventHandler(this.Command1_Click);
            // 
            // Ablak_Beolvasás
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.ClientSize = new System.Drawing.Size(846, 495);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.SAPTábla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Beolvasás";
            this.Text = "Beolvasás beállítása";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Beállítások_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SAPTábla)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.DataGridView SAPTábla;
        internal System.Windows.Forms.Button SAPFrissít;
        internal System.Windows.Forms.Label Label67;
        internal System.Windows.Forms.Label Label68;
        internal System.Windows.Forms.TextBox Változónév;
        internal System.Windows.Forms.Label Label60;
        internal System.Windows.Forms.TextBox SAPFejléc;
        internal System.Windows.Forms.TextBox SAPOSzlopszám;
        internal System.Windows.Forms.Button SAPExcel;
        internal System.Windows.Forms.ComboBox SAPCsoport;
        internal System.Windows.Forms.Label Label69;
        internal System.Windows.Forms.Button SAPTöröl;
        internal System.Windows.Forms.Button SAPRögzít;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal System.Windows.Forms.Button FejlécBeolvasása;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}