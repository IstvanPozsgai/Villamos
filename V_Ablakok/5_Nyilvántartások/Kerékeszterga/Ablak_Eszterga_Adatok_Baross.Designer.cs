namespace Villamos.Villamos_Ablakok.Kerékeszterga
{
    partial class Ablak_Eszterga_Adatok_Baross
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Eszterga_Adatok_Baross));
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Villamos_programba = new System.Windows.Forms.Button();
            this.Töröl = new System.Windows.Forms.Button();
            this.Ellenőrzések = new System.Windows.Forms.Button();
            this.Súgó = new System.Windows.Forms.Button();
            this.ExcelKimenet = new System.Windows.Forms.Button();
            this.Beolvassa = new System.Windows.Forms.Button();
            this.Tábla_Listázás = new System.Windows.Forms.Button();
            this.Adat_Javítás = new System.Windows.Forms.Button();
            this.Státuscombo = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dátumig = new System.Windows.Forms.DateTimePicker();
            this.Dátumtól = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Pályaszám = new System.Windows.Forms.TextBox();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Khaki;
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.BackgroundColor = System.Drawing.Color.DarkKhaki;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Moccasin;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(13, 69);
            this.Tábla.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Tábla.RowHeadersWidth = 42;
            this.Tábla.RowTemplate.Height = 30;
            this.Tábla.Size = new System.Drawing.Size(993, 321);
            this.Tábla.TabIndex = 65;
            // 
            // Villamos_programba
            // 
            this.Villamos_programba.BackgroundImage = global::Villamos.Properties.Resources.Aha_Soft_Standard_Transport_Tram;
            this.Villamos_programba.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Villamos_programba.Location = new System.Drawing.Point(817, 10);
            this.Villamos_programba.Name = "Villamos_programba";
            this.Villamos_programba.Size = new System.Drawing.Size(45, 45);
            this.Villamos_programba.TabIndex = 252;
            this.toolTip1.SetToolTip(this.Villamos_programba, "Az ellenőrzött adatokat beírja a kerék átmérő adatok közé");
            this.Villamos_programba.UseVisualStyleBackColor = true;
            this.Villamos_programba.Visible = false;
            this.Villamos_programba.Click += new System.EventHandler(this.Villamos_programba_Click);
            // 
            // Töröl
            // 
            this.Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Töröl.Location = new System.Drawing.Point(660, 10);
            this.Töröl.Name = "Töröl";
            this.Töröl.Size = new System.Drawing.Size(45, 45);
            this.Töröl.TabIndex = 251;
            this.toolTip1.SetToolTip(this.Töröl, "Adatot töröl");
            this.Töröl.UseVisualStyleBackColor = true;
            this.Töröl.Click += new System.EventHandler(this.Töröl_Click);
            // 
            // Ellenőrzések
            // 
            this.Ellenőrzések.BackgroundImage = global::Villamos.Properties.Resources.Gear_01;
            this.Ellenőrzések.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Ellenőrzések.Location = new System.Drawing.Point(712, 10);
            this.Ellenőrzések.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Ellenőrzések.Name = "Ellenőrzések";
            this.Ellenőrzések.Size = new System.Drawing.Size(45, 45);
            this.Ellenőrzések.TabIndex = 250;
            this.Ellenőrzések.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.Ellenőrzések, "Adatok ellenőrzések");
            this.Ellenőrzések.UseVisualStyleBackColor = true;
            this.Ellenőrzések.Click += new System.EventHandler(this.Ellenőrzések_Click);
            // 
            // Súgó
            // 
            this.Súgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Súgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Súgó.Location = new System.Drawing.Point(961, 10);
            this.Súgó.Name = "Súgó";
            this.Súgó.Size = new System.Drawing.Size(45, 45);
            this.Súgó.TabIndex = 247;
            this.toolTip1.SetToolTip(this.Súgó, "Súgó");
            this.Súgó.UseVisualStyleBackColor = true;
            this.Súgó.Click += new System.EventHandler(this.Súgó_Click);
            // 
            // ExcelKimenet
            // 
            this.ExcelKimenet.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.ExcelKimenet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ExcelKimenet.Location = new System.Drawing.Point(868, 10);
            this.ExcelKimenet.Name = "ExcelKimenet";
            this.ExcelKimenet.Size = new System.Drawing.Size(45, 45);
            this.ExcelKimenet.TabIndex = 246;
            this.toolTip1.SetToolTip(this.ExcelKimenet, "Excel táblázatot készít a táblázat adataiból");
            this.ExcelKimenet.UseVisualStyleBackColor = true;
            this.ExcelKimenet.Click += new System.EventHandler(this.ExcelKimenet_Click);
            // 
            // Beolvassa
            // 
            this.Beolvassa.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.Beolvassa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Beolvassa.Location = new System.Drawing.Point(13, 10);
            this.Beolvassa.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Beolvassa.Name = "Beolvassa";
            this.Beolvassa.Size = new System.Drawing.Size(45, 45);
            this.Beolvassa.TabIndex = 67;
            this.toolTip1.SetToolTip(this.Beolvassa, "Beolvassa és feldolgozza a .csv fájlt.");
            this.Beolvassa.UseVisualStyleBackColor = true;
            this.Beolvassa.Click += new System.EventHandler(this.Beolvassa_Click);
            // 
            // Tábla_Listázás
            // 
            this.Tábla_Listázás.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Tábla_Listázás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tábla_Listázás.Location = new System.Drawing.Point(557, 12);
            this.Tábla_Listázás.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Tábla_Listázás.Name = "Tábla_Listázás";
            this.Tábla_Listázás.Size = new System.Drawing.Size(45, 45);
            this.Tábla_Listázás.TabIndex = 66;
            this.toolTip1.SetToolTip(this.Tábla_Listázás, "Listázza az adatokat");
            this.Tábla_Listázás.UseVisualStyleBackColor = true;
            this.Tábla_Listázás.Click += new System.EventHandler(this.Tábla_Listázás_Click);
            // 
            // Adat_Javítás
            // 
            this.Adat_Javítás.BackgroundImage = global::Villamos.Properties.Resources.App_network_connection_manager;
            this.Adat_Javítás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Adat_Javítás.Location = new System.Drawing.Point(765, 10);
            this.Adat_Javítás.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Adat_Javítás.Name = "Adat_Javítás";
            this.Adat_Javítás.Size = new System.Drawing.Size(45, 45);
            this.Adat_Javítás.TabIndex = 257;
            this.Adat_Javítás.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.Adat_Javítás, "Adatok javítása");
            this.Adat_Javítás.UseVisualStyleBackColor = true;
            this.Adat_Javítás.Click += new System.EventHandler(this.Adat_Javítás_Click);
            // 
            // Státuscombo
            // 
            this.Státuscombo.FormattingEnabled = true;
            this.Státuscombo.Location = new System.Drawing.Point(164, 29);
            this.Státuscombo.Name = "Státuscombo";
            this.Státuscombo.Size = new System.Drawing.Size(160, 28);
            this.Státuscombo.TabIndex = 248;
            this.Státuscombo.SelectedIndexChanged += new System.EventHandler(this.Státuscombo_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(167, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 249;
            this.label1.Text = "Státus:";
            // 
            // dátumig
            // 
            this.dátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dátumig.Location = new System.Drawing.Point(443, 31);
            this.dátumig.Name = "dátumig";
            this.dátumig.Size = new System.Drawing.Size(107, 26);
            this.dátumig.TabIndex = 254;
            // 
            // Dátumtól
            // 
            this.Dátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumtól.Location = new System.Drawing.Point(330, 31);
            this.Dátumtól.Name = "Dátumtól";
            this.Dátumtól.Size = new System.Drawing.Size(107, 26);
            this.Dátumtól.TabIndex = 253;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(446, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 20);
            this.label2.TabIndex = 255;
            this.label2.Text = "Dátumig:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(333, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 20);
            this.label3.TabIndex = 256;
            this.label3.Text = "Dátumtól:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(65, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 259;
            this.label4.Text = "Pályaszám:";
            // 
            // Pályaszám
            // 
            this.Pályaszám.Location = new System.Drawing.Point(62, 31);
            this.Pályaszám.Name = "Pályaszám";
            this.Pályaszám.Size = new System.Drawing.Size(96, 26);
            this.Pályaszám.TabIndex = 258;
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(45, 200);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(930, 30);
            this.Holtart.TabIndex = 260;
            this.Holtart.Visible = false;
            // 
            // Ablak_Eszterga_Adatok_Baross
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 404);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Pályaszám);
            this.Controls.Add(this.Adat_Javítás);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dátumig);
            this.Controls.Add(this.Dátumtól);
            this.Controls.Add(this.Villamos_programba);
            this.Controls.Add(this.Töröl);
            this.Controls.Add(this.Ellenőrzések);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Státuscombo);
            this.Controls.Add(this.Súgó);
            this.Controls.Add(this.ExcelKimenet);
            this.Controls.Add(this.Beolvassa);
            this.Controls.Add(this.Tábla_Listázás);
            this.Controls.Add(this.Tábla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Eszterga_Adatok_Baross";
            this.Text = "Ablak Eszterga Adatok";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_Eszterga_Adatok_Baross_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Eszterga_Adatok_Baross_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView Tábla;
        internal System.Windows.Forms.Button Tábla_Listázás;
        internal System.Windows.Forms.Button Beolvassa;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Button ExcelKimenet;
        internal System.Windows.Forms.Button Súgó;
        private System.Windows.Forms.ComboBox Státuscombo;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button Ellenőrzések;
        internal System.Windows.Forms.Button Töröl;
        internal System.Windows.Forms.Button Villamos_programba;
        internal System.Windows.Forms.DateTimePicker dátumig;
        internal System.Windows.Forms.DateTimePicker Dátumtól;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button Adat_Javítás;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Pályaszám;
        internal V_MindenEgyéb.MyProgressbar Holtart;
    }
}