namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    partial class Ablak_CAF_km_mod_seged
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_CAF_km_mod_seged));
            this.Segéd_KM_allas = new System.Windows.Forms.TextBox();
            this.Segéd_Pót_Rögzít = new System.Windows.Forms.Button();
            this.Segéd_pályaszám = new System.Windows.Forms.TextBox();
            this.Segéd_dátum = new System.Windows.Forms.DateTimePicker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.seged_kov_stat = new System.Windows.Forms.TextBox();
            this.label_palyaszam = new System.Windows.Forms.Label();
            this.label_datum = new System.Windows.Forms.Label();
            this.label_statusz = new System.Windows.Forms.Label();
            this.label_km = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Segéd_KM_allas
            // 
            this.Segéd_KM_allas.Location = new System.Drawing.Point(111, 108);
            this.Segéd_KM_allas.Name = "Segéd_KM_allas";
            this.Segéd_KM_allas.Size = new System.Drawing.Size(120, 26);
            this.Segéd_KM_allas.TabIndex = 236;
            // 
            // Segéd_Pót_Rögzít
            // 
            this.Segéd_Pót_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Segéd_Pót_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Segéd_Pót_Rögzít.Location = new System.Drawing.Point(185, 140);
            this.Segéd_Pót_Rögzít.Name = "Segéd_Pót_Rögzít";
            this.Segéd_Pót_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Segéd_Pót_Rögzít.TabIndex = 235;
            this.toolTip1.SetToolTip(this.Segéd_Pót_Rögzít, "Rögzíti az adatokat");
            this.Segéd_Pót_Rögzít.UseVisualStyleBackColor = true;
            this.Segéd_Pót_Rögzít.Click += new System.EventHandler(this.Segéd_Pót_Rögzít_Click);
            // 
            // Segéd_pályaszám
            // 
            this.Segéd_pályaszám.Enabled = false;
            this.Segéd_pályaszám.Location = new System.Drawing.Point(111, 12);
            this.Segéd_pályaszám.Name = "Segéd_pályaszám";
            this.Segéd_pályaszám.Size = new System.Drawing.Size(120, 26);
            this.Segéd_pályaszám.TabIndex = 229;
            this.Segéd_pályaszám.TextChanged += new System.EventHandler(this.Segéd_pályaszám_TextChanged);
            // 
            // Segéd_dátum
            // 
            this.Segéd_dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Segéd_dátum.Location = new System.Drawing.Point(111, 44);
            this.Segéd_dátum.Name = "Segéd_dátum";
            this.Segéd_dátum.Size = new System.Drawing.Size(120, 26);
            this.Segéd_dátum.TabIndex = 228;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // seged_kov_stat
            // 
            this.seged_kov_stat.Enabled = false;
            this.seged_kov_stat.Location = new System.Drawing.Point(111, 76);
            this.seged_kov_stat.Name = "seged_kov_stat";
            this.seged_kov_stat.Size = new System.Drawing.Size(120, 26);
            this.seged_kov_stat.TabIndex = 237;
            // 
            // label_palyaszam
            // 
            this.label_palyaszam.AutoSize = true;
            this.label_palyaszam.Location = new System.Drawing.Point(6, 18);
            this.label_palyaszam.Name = "label_palyaszam";
            this.label_palyaszam.Size = new System.Drawing.Size(89, 20);
            this.label_palyaszam.TabIndex = 238;
            this.label_palyaszam.Text = "Pályaszám:";
            this.label_palyaszam.Click += new System.EventHandler(this.label1_Click);
            // 
            // label_datum
            // 
            this.label_datum.AutoSize = true;
            this.label_datum.Location = new System.Drawing.Point(6, 49);
            this.label_datum.Name = "label_datum";
            this.label_datum.Size = new System.Drawing.Size(61, 20);
            this.label_datum.TabIndex = 239;
            this.label_datum.Text = "Dátum:";
            // 
            // label_statusz
            // 
            this.label_statusz.AutoSize = true;
            this.label_statusz.Location = new System.Drawing.Point(6, 79);
            this.label_statusz.Name = "label_statusz";
            this.label_statusz.Size = new System.Drawing.Size(60, 20);
            this.label_statusz.TabIndex = 240;
            this.label_statusz.Text = "Státus:";
            this.label_statusz.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // label_km
            // 
            this.label_km.AutoSize = true;
            this.label_km.Location = new System.Drawing.Point(6, 114);
            this.label_km.Name = "label_km";
            this.label_km.Size = new System.Drawing.Size(99, 20);
            this.label_km.TabIndex = 241;
            this.label_km.Text = "KM óra állás:";
            // 
            // Ablak_CAF_km_mod_seged
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Peru;
            this.ClientSize = new System.Drawing.Size(258, 200);
            this.Controls.Add(this.label_km);
            this.Controls.Add(this.label_statusz);
            this.Controls.Add(this.label_datum);
            this.Controls.Add(this.label_palyaszam);
            this.Controls.Add(this.seged_kov_stat);
            this.Controls.Add(this.Segéd_KM_allas);
            this.Controls.Add(this.Segéd_Pót_Rögzít);
            this.Controls.Add(this.Segéd_pályaszám);
            this.Controls.Add(this.Segéd_dátum);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Ablak_CAF_km_mod_seged";
            this.Text = "KM Bejegyzés";
            this.Load += new System.EventHandler(this.Ablak_CAF_km_mod_seged_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox Segéd_KM_allas;
        internal System.Windows.Forms.Button Segéd_Pót_Rögzít;
        internal System.Windows.Forms.TextBox Segéd_pályaszám;
        internal System.Windows.Forms.DateTimePicker Segéd_dátum;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox seged_kov_stat;
        private System.Windows.Forms.Label label_palyaszam;
        private System.Windows.Forms.Label label_datum;
        private System.Windows.Forms.Label label_statusz;
        private System.Windows.Forms.Label label_km;
    }
}