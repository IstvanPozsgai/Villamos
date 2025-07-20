namespace Villamos.V_Ablakok._1_Beállítások
{
    partial class Ablak_Ciklus_Sorrend
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Ciklus_Sorrend));
            this.label1 = new System.Windows.Forms.Label();
            this.Sorszám = new System.Windows.Forms.TextBox();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Rögzítés = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CiklusTípus = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.JárműTípus = new System.Windows.Forms.ComboBox();
            this.BeoFrissít = new System.Windows.Forms.Button();
            this.BeoÚj = new System.Windows.Forms.Button();
            this.Töröl = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sorszám:";
            // 
            // Sorszám
            // 
            this.Sorszám.Location = new System.Drawing.Point(104, 3);
            this.Sorszám.Name = "Sorszám";
            this.Sorszám.Size = new System.Drawing.Size(117, 26);
            this.Sorszám.TabIndex = 1;
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Location = new System.Drawing.Point(12, 119);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Tábla.Size = new System.Drawing.Size(666, 351);
            this.Tábla.TabIndex = 3;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // Rögzítés
            // 
            this.Rögzítés.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Rögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzítés.Location = new System.Drawing.Point(353, 6);
            this.Rögzítés.Name = "Rögzítés";
            this.Rögzítés.Size = new System.Drawing.Size(45, 45);
            this.Rögzítés.TabIndex = 191;
            this.Rögzítés.UseVisualStyleBackColor = true;
            this.Rögzítés.Click += new System.EventHandler(this.Rögzítés_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Jármű típus:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ciklus név:";
            // 
            // CiklusTípus
            // 
            this.CiklusTípus.FormattingEnabled = true;
            this.CiklusTípus.Location = new System.Drawing.Point(104, 73);
            this.CiklusTípus.MaxLength = 15;
            this.CiklusTípus.Name = "CiklusTípus";
            this.CiklusTípus.Size = new System.Drawing.Size(173, 28);
            this.CiklusTípus.TabIndex = 192;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Sorszám, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.CiklusTípus, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.JárműTípus, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(298, 108);
            this.tableLayoutPanel1.TabIndex = 194;
            // 
            // JárműTípus
            // 
            this.JárműTípus.FormattingEnabled = true;
            this.JárműTípus.Location = new System.Drawing.Point(104, 38);
            this.JárműTípus.MaxLength = 15;
            this.JárműTípus.Name = "JárműTípus";
            this.JárműTípus.Size = new System.Drawing.Size(173, 28);
            this.JárműTípus.TabIndex = 192;
            // 
            // BeoFrissít
            // 
            this.BeoFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BeoFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BeoFrissít.Location = new System.Drawing.Point(455, 69);
            this.BeoFrissít.Name = "BeoFrissít";
            this.BeoFrissít.Size = new System.Drawing.Size(45, 45);
            this.BeoFrissít.TabIndex = 197;
            this.BeoFrissít.UseVisualStyleBackColor = true;
            this.BeoFrissít.Click += new System.EventHandler(this.BeoFrissít_Click);
            // 
            // BeoÚj
            // 
            this.BeoÚj.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.BeoÚj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BeoÚj.Location = new System.Drawing.Point(404, 69);
            this.BeoÚj.Name = "BeoÚj";
            this.BeoÚj.Size = new System.Drawing.Size(45, 45);
            this.BeoÚj.TabIndex = 196;
            this.BeoÚj.UseVisualStyleBackColor = true;
            this.BeoÚj.Click += new System.EventHandler(this.BeoÚj_Click);
            // 
            // Töröl
            // 
            this.Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Töröl.Location = new System.Drawing.Point(353, 69);
            this.Töröl.Name = "Töröl";
            this.Töröl.Size = new System.Drawing.Size(45, 45);
            this.Töröl.TabIndex = 195;
            this.Töröl.UseVisualStyleBackColor = true;
            this.Töröl.Click += new System.EventHandler(this.Töröl_Click);
            // 
            // Ablak_Ciklus_Sorrend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Peru;
            this.ClientSize = new System.Drawing.Size(688, 480);
            this.Controls.Add(this.BeoFrissít);
            this.Controls.Add(this.BeoÚj);
            this.Controls.Add(this.Töröl);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Rögzítés);
            this.Controls.Add(this.Tábla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Ciklus_Sorrend";
            this.Text = "Ciklus Sorrend";
            this.Load += new System.EventHandler(this.Ablak_Ciklus_Sorrend_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Sorszám;
        private System.Windows.Forms.DataGridView Tábla;
        internal System.Windows.Forms.Button Rögzítés;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CiklusTípus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal System.Windows.Forms.Button BeoFrissít;
        internal System.Windows.Forms.Button BeoÚj;
        internal System.Windows.Forms.Button Töröl;
        private System.Windows.Forms.ComboBox JárműTípus;
    }
}