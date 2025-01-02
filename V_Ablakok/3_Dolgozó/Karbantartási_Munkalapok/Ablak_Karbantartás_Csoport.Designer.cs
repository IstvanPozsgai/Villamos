namespace Villamos.Villamos_Ablakok._3_Dolgozó.Karbantartási_Munkalapok
{
    partial class Ablak_Karbantartás_Csoport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Karbantartás_Csoport));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Csoport_Töröl = new System.Windows.Forms.Button();
            this.Csoport_frissít = new System.Windows.Forms.Button();
            this.Csoport_rögzít = new System.Windows.Forms.Button();
            this.BtnÜres = new System.Windows.Forms.Button();
            this.Label111 = new System.Windows.Forms.Label();
            this.Csoport_típus = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Csoport_Ciklus = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Csoport_tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Csoport_változat = new System.Windows.Forms.ComboBox();
            this.Csoport_Végző = new System.Windows.Forms.ComboBox();
            this.Excel = new System.Windows.Forms.Button();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.Sorszám = new System.Windows.Forms.TextBox();
            this.CHKÉrvényes = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.Csoport_tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Csoport_Töröl
            // 
            this.Csoport_Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Csoport_Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csoport_Töröl.Location = new System.Drawing.Point(1143, 58);
            this.Csoport_Töröl.Name = "Csoport_Töröl";
            this.Csoport_Töröl.Size = new System.Drawing.Size(45, 45);
            this.Csoport_Töröl.TabIndex = 252;
            this.toolTip1.SetToolTip(this.Csoport_Töröl, "Kiválasztott pályaszám hozzáadása a táblázathoz");
            this.Csoport_Töröl.UseVisualStyleBackColor = true;
            this.Csoport_Töröl.Click += new System.EventHandler(this.Csoport_Töröl_Click);
            // 
            // Csoport_frissít
            // 
            this.Csoport_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Csoport_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csoport_frissít.Location = new System.Drawing.Point(1092, 58);
            this.Csoport_frissít.Name = "Csoport_frissít";
            this.Csoport_frissít.Size = new System.Drawing.Size(45, 45);
            this.Csoport_frissít.TabIndex = 245;
            this.toolTip1.SetToolTip(this.Csoport_frissít, "Kiválasztott pályaszám hozzáadása a táblázathoz");
            this.Csoport_frissít.UseVisualStyleBackColor = true;
            this.Csoport_frissít.Click += new System.EventHandler(this.Csoport_frissít_Click);
            // 
            // Csoport_rögzít
            // 
            this.Csoport_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Csoport_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csoport_rögzít.Location = new System.Drawing.Point(1009, 58);
            this.Csoport_rögzít.Name = "Csoport_rögzít";
            this.Csoport_rögzít.Size = new System.Drawing.Size(45, 45);
            this.Csoport_rögzít.TabIndex = 240;
            this.toolTip1.SetToolTip(this.Csoport_rögzít, "Kiválasztott pályaszám hozzáadása a táblázathoz");
            this.Csoport_rögzít.UseVisualStyleBackColor = true;
            this.Csoport_rögzít.Click += new System.EventHandler(this.Csoport_rögzít_Click);
            // 
            // BtnÜres
            // 
            this.BtnÜres.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.BtnÜres.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnÜres.Location = new System.Drawing.Point(1143, 7);
            this.BtnÜres.Name = "BtnÜres";
            this.BtnÜres.Size = new System.Drawing.Size(45, 45);
            this.BtnÜres.TabIndex = 257;
            this.toolTip1.SetToolTip(this.BtnÜres, "Beviteli mezők ürítése");
            this.BtnÜres.UseVisualStyleBackColor = true;
            this.BtnÜres.Click += new System.EventHandler(this.BtnÜres_Click);
            // 
            // Label111
            // 
            this.Label111.AutoSize = true;
            this.Label111.BackColor = System.Drawing.Color.Transparent;
            this.Label111.Location = new System.Drawing.Point(8, 53);
            this.Label111.Name = "Label111";
            this.Label111.Size = new System.Drawing.Size(76, 20);
            this.Label111.TabIndex = 251;
            this.Label111.Text = "Sorszám:";
            // 
            // Csoport_típus
            // 
            this.Csoport_típus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Csoport_típus.FormattingEnabled = true;
            this.Csoport_típus.Location = new System.Drawing.Point(121, 12);
            this.Csoport_típus.Name = "Csoport_típus";
            this.Csoport_típus.Size = new System.Drawing.Size(239, 28);
            this.Csoport_típus.TabIndex = 247;
            this.Csoport_típus.SelectedIndexChanged += new System.EventHandler(this.Csoport_típus_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(7, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 20);
            this.label8.TabIndex = 248;
            this.label8.Text = "Jármű típus:";
            // 
            // Csoport_Ciklus
            // 
            this.Csoport_Ciklus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Csoport_Ciklus.FormattingEnabled = true;
            this.Csoport_Ciklus.Location = new System.Drawing.Point(638, 12);
            this.Csoport_Ciklus.Name = "Csoport_Ciklus";
            this.Csoport_Ciklus.Size = new System.Drawing.Size(121, 28);
            this.Csoport_Ciklus.TabIndex = 249;
            this.Csoport_Ciklus.SelectedIndexChanged += new System.EventHandler(this.Csoport_Ciklus_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(445, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(147, 20);
            this.label9.TabIndex = 250;
            this.label9.Text = "Karbantartási ciklus";
            // 
            // Csoport_tábla
            // 
            this.Csoport_tábla.AllowUserToAddRows = false;
            this.Csoport_tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Csoport_tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Csoport_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Csoport_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Csoport_tábla.Location = new System.Drawing.Point(11, 114);
            this.Csoport_tábla.Name = "Csoport_tábla";
            this.Csoport_tábla.Size = new System.Drawing.Size(1177, 320);
            this.Csoport_tábla.TabIndex = 246;
            this.Csoport_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Csoport_tábla_CellClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(445, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(185, 20);
            this.label7.TabIndex = 244;
            this.label7.Text = "Csoportosítási elnevezés";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 20);
            this.label6.TabIndex = 243;
            this.label6.Text = "Változatnév:";
            // 
            // Csoport_változat
            // 
            this.Csoport_változat.FormattingEnabled = true;
            this.Csoport_változat.Location = new System.Drawing.Point(121, 78);
            this.Csoport_változat.Name = "Csoport_változat";
            this.Csoport_változat.Size = new System.Drawing.Size(302, 28);
            this.Csoport_változat.TabIndex = 242;
            this.Csoport_változat.SelectedIndexChanged += new System.EventHandler(this.Csoport_változat_SelectedIndexChanged);
            // 
            // Csoport_Végző
            // 
            this.Csoport_Végző.FormattingEnabled = true;
            this.Csoport_Végző.Location = new System.Drawing.Point(638, 78);
            this.Csoport_Végző.Name = "Csoport_Végző";
            this.Csoport_Végző.Size = new System.Drawing.Size(302, 28);
            this.Csoport_Végző.TabIndex = 241;
            // 
            // Excel
            // 
            this.Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel.Location = new System.Drawing.Point(1092, 7);
            this.Excel.Name = "Excel";
            this.Excel.Size = new System.Drawing.Size(45, 45);
            this.Excel.TabIndex = 253;
            this.Excel.UseVisualStyleBackColor = true;
            this.Excel.Click += new System.EventHandler(this.Excel_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(29, 173);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(1140, 28);
            this.Holtart.TabIndex = 255;
            this.Holtart.Visible = false;
            // 
            // Sorszám
            // 
            this.Sorszám.Location = new System.Drawing.Point(121, 47);
            this.Sorszám.Name = "Sorszám";
            this.Sorszám.Size = new System.Drawing.Size(100, 26);
            this.Sorszám.TabIndex = 256;
            // 
            // CHKÉrvényes
            // 
            this.CHKÉrvényes.AutoSize = true;
            this.CHKÉrvényes.Checked = true;
            this.CHKÉrvényes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CHKÉrvényes.Location = new System.Drawing.Point(786, 14);
            this.CHKÉrvényes.Name = "CHKÉrvényes";
            this.CHKÉrvényes.Size = new System.Drawing.Size(186, 24);
            this.CHKÉrvényes.TabIndex = 258;
            this.CHKÉrvényes.Text = "Csak érvényes elemek";
            this.CHKÉrvényes.UseVisualStyleBackColor = true;
            // 
            // Ablak_Karbantartás_Csoport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 443);
            this.Controls.Add(this.CHKÉrvényes);
            this.Controls.Add(this.BtnÜres);
            this.Controls.Add(this.Sorszám);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Label111);
            this.Controls.Add(this.Csoport_típus);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Csoport_Ciklus);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Csoport_tábla);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Csoport_változat);
            this.Controls.Add(this.Csoport_Végző);
            this.Controls.Add(this.Excel);
            this.Controls.Add(this.Csoport_Töröl);
            this.Controls.Add(this.Csoport_frissít);
            this.Controls.Add(this.Csoport_rögzít);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Karbantartás_Csoport";
            this.Text = "Karbantartás Csoportosítás";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Karbantartás_Csoport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Csoport_tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Label Label111;
        private System.Windows.Forms.ComboBox Csoport_típus;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox Csoport_Ciklus;
        private System.Windows.Forms.Label label9;
        private  Zuby.ADGV.AdvancedDataGridView Csoport_tábla;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox Csoport_változat;
        private System.Windows.Forms.ComboBox Csoport_Végző;
        internal System.Windows.Forms.Button Excel;
        internal System.Windows.Forms.Button Csoport_Töröl;
        internal System.Windows.Forms.Button Csoport_frissít;
        internal System.Windows.Forms.Button Csoport_rögzít;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        private System.Windows.Forms.TextBox Sorszám;
        internal System.Windows.Forms.Button BtnÜres;
        private System.Windows.Forms.CheckBox CHKÉrvényes;
    }
}