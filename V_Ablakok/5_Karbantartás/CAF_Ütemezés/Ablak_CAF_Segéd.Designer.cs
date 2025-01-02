namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    partial class Ablak_CAF_Segéd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_CAF_Segéd));
            this.Segéd_sorszám = new System.Windows.Forms.TextBox();
            this.Segéd_Pót_Rögzít = new System.Windows.Forms.Button();
            this.Segéd_darab = new System.Windows.Forms.TextBox();
            this.Segéd_Vizsg = new System.Windows.Forms.TextBox();
            this.Segéd_ütemez = new System.Windows.Forms.Button();
            this.Segéd_pályaszám = new System.Windows.Forms.TextBox();
            this.Segéd_dátum = new System.Windows.Forms.DateTimePicker();
            this.Segéd_átütemez = new System.Windows.Forms.Button();
            this.Segéd_Töröl = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // Segéd_sorszám
            // 
            this.Segéd_sorszám.Enabled = false;
            this.Segéd_sorszám.Location = new System.Drawing.Point(12, 146);
            this.Segéd_sorszám.Name = "Segéd_sorszám";
            this.Segéd_sorszám.Size = new System.Drawing.Size(69, 26);
            this.Segéd_sorszám.TabIndex = 236;
            // 
            // Segéd_Pót_Rögzít
            // 
            this.Segéd_Pót_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Segéd_Pót_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Segéd_Pót_Rögzít.Location = new System.Drawing.Point(244, 127);
            this.Segéd_Pót_Rögzít.Name = "Segéd_Pót_Rögzít";
            this.Segéd_Pót_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Segéd_Pót_Rögzít.TabIndex = 235;
            this.toolTip1.SetToolTip(this.Segéd_Pót_Rögzít, "Rögzíti az adatokat");
            this.Segéd_Pót_Rögzít.UseVisualStyleBackColor = true;
            this.Segéd_Pót_Rögzít.Click += new System.EventHandler(this.Segéd_Pót_Rögzít_Click);
            // 
            // Segéd_darab
            // 
            this.Segéd_darab.Location = new System.Drawing.Point(188, 146);
            this.Segéd_darab.Name = "Segéd_darab";
            this.Segéd_darab.Size = new System.Drawing.Size(50, 26);
            this.Segéd_darab.TabIndex = 234;
            // 
            // Segéd_Vizsg
            // 
            this.Segéd_Vizsg.Location = new System.Drawing.Point(87, 146);
            this.Segéd_Vizsg.Name = "Segéd_Vizsg";
            this.Segéd_Vizsg.Size = new System.Drawing.Size(95, 26);
            this.Segéd_Vizsg.TabIndex = 233;
            this.Segéd_Vizsg.Text = "P";
            // 
            // Segéd_ütemez
            // 
            this.Segéd_ütemez.BackgroundImage = global::Villamos.Properties.Resources.Document_preferences;
            this.Segéd_ütemez.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Segéd_ütemez.Location = new System.Drawing.Point(193, 76);
            this.Segéd_ütemez.Name = "Segéd_ütemez";
            this.Segéd_ütemez.Size = new System.Drawing.Size(45, 45);
            this.Segéd_ütemez.TabIndex = 232;
            this.toolTip1.SetToolTip(this.Segéd_ütemez, "A listázott elemeket átállítja tervezettről Ütemezettre");
            this.Segéd_ütemez.UseVisualStyleBackColor = true;
            this.Segéd_ütemez.Click += new System.EventHandler(this.Segéd_ütemez_Click);
            // 
            // Segéd_pályaszám
            // 
            this.Segéd_pályaszám.Enabled = false;
            this.Segéd_pályaszám.Location = new System.Drawing.Point(12, 12);
            this.Segéd_pályaszám.Name = "Segéd_pályaszám";
            this.Segéd_pályaszám.Size = new System.Drawing.Size(119, 26);
            this.Segéd_pályaszám.TabIndex = 229;
            // 
            // Segéd_dátum
            // 
            this.Segéd_dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Segéd_dátum.Location = new System.Drawing.Point(12, 44);
            this.Segéd_dátum.Name = "Segéd_dátum";
            this.Segéd_dátum.Size = new System.Drawing.Size(119, 26);
            this.Segéd_dátum.TabIndex = 228;
            // 
            // Segéd_átütemez
            // 
            this.Segéd_átütemez.BackgroundImage = global::Villamos.Properties.Resources.átütemez32;
            this.Segéd_átütemez.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Segéd_átütemez.Location = new System.Drawing.Point(244, 25);
            this.Segéd_átütemez.Name = "Segéd_átütemez";
            this.Segéd_átütemez.Size = new System.Drawing.Size(45, 45);
            this.Segéd_átütemez.TabIndex = 227;
            this.toolTip1.SetToolTip(this.Segéd_átütemez, "Átütemezi a karbantartás");
            this.Segéd_átütemez.UseVisualStyleBackColor = true;
            this.Segéd_átütemez.Click += new System.EventHandler(this.Segéd_átütemez_Click);
            // 
            // Segéd_Töröl
            // 
            this.Segéd_Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Segéd_Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Segéd_Töröl.Location = new System.Drawing.Point(244, 76);
            this.Segéd_Töröl.Name = "Segéd_Töröl";
            this.Segéd_Töröl.Size = new System.Drawing.Size(45, 45);
            this.Segéd_Töröl.TabIndex = 226;
            this.toolTip1.SetToolTip(this.Segéd_Töröl, "Ütemezés törlése");
            this.Segéd_Töröl.UseVisualStyleBackColor = true;
            this.Segéd_Töröl.Click += new System.EventHandler(this.Segéd_Töröl_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Ablak_CAF_Segéd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Peru;
            this.ClientSize = new System.Drawing.Size(296, 183);
            this.Controls.Add(this.Segéd_sorszám);
            this.Controls.Add(this.Segéd_Pót_Rögzít);
            this.Controls.Add(this.Segéd_darab);
            this.Controls.Add(this.Segéd_Vizsg);
            this.Controls.Add(this.Segéd_ütemez);
            this.Controls.Add(this.Segéd_pályaszám);
            this.Controls.Add(this.Segéd_dátum);
            this.Controls.Add(this.Segéd_átütemez);
            this.Controls.Add(this.Segéd_Töröl);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Ablak_CAF_Segéd";
            this.Text = "Ütemező Segédablak";
            this.Load += new System.EventHandler(this.Ablak_CAF_Segéd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox Segéd_sorszám;
        internal System.Windows.Forms.Button Segéd_Pót_Rögzít;
        internal System.Windows.Forms.TextBox Segéd_darab;
        internal System.Windows.Forms.TextBox Segéd_Vizsg;
        internal System.Windows.Forms.Button Segéd_ütemez;
        internal System.Windows.Forms.TextBox Segéd_pályaszám;
        internal System.Windows.Forms.DateTimePicker Segéd_dátum;
        internal System.Windows.Forms.Button Segéd_átütemez;
        internal System.Windows.Forms.Button Segéd_Töröl;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}