namespace Villamos.Villamos_Ablakok
{
    partial class Ablak_Eszterga_Segéd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Eszterga_Segéd));
            this.label1 = new System.Windows.Forms.Label();
            this.Text_Dátum = new System.Windows.Forms.TextBox();
            this.Tevékenység = new System.Windows.Forms.ComboBox();
            this.Text_Idő = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Terv_Rögzít = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.Norma_Idő = new System.Windows.Forms.TextBox();
            this.Megjegyzés = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.Töröl = new System.Windows.Forms.Button();
            this.Egy_adat = new System.Windows.Forms.CheckBox();
            this.Igény_Típus = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Marad = new System.Windows.Forms.CheckBox();
            this.Tevékenység_Vál = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Kezdési Dátum:";
            // 
            // Text_Dátum
            // 
            this.Text_Dátum.Enabled = false;
            this.Text_Dátum.Location = new System.Drawing.Point(139, 3);
            this.Text_Dátum.Name = "Text_Dátum";
            this.Text_Dátum.Size = new System.Drawing.Size(149, 26);
            this.Text_Dátum.TabIndex = 1;
            // 
            // Tevékenység
            // 
            this.Tevékenység.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Tevékenység.FormattingEnabled = true;
            this.Tevékenység.Location = new System.Drawing.Point(139, 67);
            this.Tevékenység.Name = "Tevékenység";
            this.Tevékenység.Size = new System.Drawing.Size(373, 28);
            this.Tevékenység.TabIndex = 2;
            this.Tevékenység.SelectedIndexChanged += new System.EventHandler(this.Tevékenység_SelectedIndexChanged);
            // 
            // Text_Idő
            // 
            this.Text_Idő.Enabled = false;
            this.Text_Idő.Location = new System.Drawing.Point(139, 35);
            this.Text_Idő.Name = "Text_Idő";
            this.Text_Idő.Size = new System.Drawing.Size(109, 26);
            this.Text_Idő.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Kezdési Idő:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Kategória:";
            // 
            // Terv_Rögzít
            // 
            this.Terv_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Terv_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Terv_Rögzít.Location = new System.Drawing.Point(696, 12);
            this.Terv_Rögzít.Name = "Terv_Rögzít";
            this.Terv_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Terv_Rögzít.TabIndex = 189;
            this.Terv_Rögzít.UseVisualStyleBackColor = true;
            this.Terv_Rögzít.Click += new System.EventHandler(this.Terv_Rögzít_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 141);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 20);
            this.label4.TabIndex = 190;
            this.label4.Text = "Idő szükséglet:";
            // 
            // Norma_Idő
            // 
            this.Norma_Idő.Location = new System.Drawing.Point(139, 135);
            this.Norma_Idő.Name = "Norma_Idő";
            this.Norma_Idő.Size = new System.Drawing.Size(109, 26);
            this.Norma_Idő.TabIndex = 191;
            // 
            // Megjegyzés
            // 
            this.Megjegyzés.Location = new System.Drawing.Point(139, 167);
            this.Megjegyzés.Multiline = true;
            this.Megjegyzés.Name = "Megjegyzés";
            this.Megjegyzés.Size = new System.Drawing.Size(606, 121);
            this.Megjegyzés.TabIndex = 192;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 20);
            this.label5.TabIndex = 193;
            this.label5.Text = "Megjegyzés:";
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Location = new System.Drawing.Point(12, 294);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersWidth = 51;
            this.Tábla.Size = new System.Drawing.Size(733, 178);
            this.Tábla.TabIndex = 194;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(254, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 20);
            this.label6.TabIndex = 195;
            this.label6.Text = "perc";
            // 
            // Töröl
            // 
            this.Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Töröl.Location = new System.Drawing.Point(645, 12);
            this.Töröl.Name = "Töröl";
            this.Töröl.Size = new System.Drawing.Size(45, 45);
            this.Töröl.TabIndex = 196;
            this.Töröl.UseVisualStyleBackColor = true;
            this.Töröl.Click += new System.EventHandler(this.Töröl_Click);
            // 
            // Egy_adat
            // 
            this.Egy_adat.AutoSize = true;
            this.Egy_adat.Location = new System.Drawing.Point(645, 63);
            this.Egy_adat.Name = "Egy_adat";
            this.Egy_adat.Size = new System.Drawing.Size(91, 24);
            this.Egy_adat.TabIndex = 197;
            this.Egy_adat.Text = "Egy adat";
            this.Egy_adat.UseVisualStyleBackColor = true;
            // 
            // Igény_Típus
            // 
            this.Igény_Típus.FormattingEnabled = true;
            this.Igény_Típus.Location = new System.Drawing.Point(13, 260);
            this.Igény_Típus.Name = "Igény_Típus";
            this.Igény_Típus.Size = new System.Drawing.Size(120, 28);
            this.Igény_Típus.TabIndex = 198;
            this.Igény_Típus.SelectedIndexChanged += new System.EventHandler(this.Igény_Típus_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 237);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 20);
            this.label7.TabIndex = 199;
            this.label7.Text = "Típus:";
            // 
            // Marad
            // 
            this.Marad.AutoSize = true;
            this.Marad.Location = new System.Drawing.Point(377, 140);
            this.Marad.Name = "Marad";
            this.Marad.Size = new System.Drawing.Size(135, 24);
            this.Marad.TabIndex = 200;
            this.Marad.Text = "Helyben marad";
            this.Marad.UseVisualStyleBackColor = true;
            // 
            // Tevékenység_Vál
            // 
            this.Tevékenység_Vál.Location = new System.Drawing.Point(139, 101);
            this.Tevékenység_Vál.Name = "Tevékenység_Vál";
            this.Tevékenység_Vál.Size = new System.Drawing.Size(606, 26);
            this.Tevékenység_Vál.TabIndex = 202;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 20);
            this.label8.TabIndex = 203;
            this.label8.Text = "Tevékenység:";
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(15, 210);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(730, 25);
            this.Holtart.TabIndex = 204;
            this.Holtart.Visible = false;
            // 
            // Ablak_Eszterga_Segéd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SpringGreen;
            this.ClientSize = new System.Drawing.Size(753, 483);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.Tevékenység_Vál);
            this.Controls.Add(this.Marad);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.Igény_Típus);
            this.Controls.Add(this.Egy_adat);
            this.Controls.Add(this.Töröl);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.Megjegyzés);
            this.Controls.Add(this.Norma_Idő);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Terv_Rögzít);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Text_Idő);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Tevékenység);
            this.Controls.Add(this.Text_Dátum);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Ablak_Eszterga_Segéd";
            this.Text = "Kerékeszterga Rögzítési Segéd Ablak";
            this.Load += new System.EventHandler(this.Ablak_Eszterga_Segéd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_Eszterga_Segéd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox Text_Dátum;
        internal System.Windows.Forms.ComboBox Tevékenység;
        internal System.Windows.Forms.TextBox Text_Idő;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button Terv_Rögzít;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox Norma_Idő;
        internal System.Windows.Forms.TextBox Megjegyzés;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.DataGridView Tábla;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Button Töröl;
        internal System.Windows.Forms.CheckBox Egy_adat;
        internal System.Windows.Forms.ComboBox Igény_Típus;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.CheckBox Marad;
        internal System.Windows.Forms.TextBox Tevékenység_Vál;
        internal System.Windows.Forms.Label label8;
        internal V_MindenEgyéb.MyProgressbar Holtart;
    }
}