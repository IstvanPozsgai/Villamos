namespace Villamos.V_Ablakok._0_Bejelentkezés
{
    partial class Ablak_Szoftver_Kulcs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Szoftver_Kulcs));
            this.DolgozóNév = new System.Windows.Forms.Label();
            this.TextNév = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.CMBMireSzemélyes = new System.Windows.Forms.CheckedListBox();
            this.Alap_Rögzít = new System.Windows.Forms.Button();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // DolgozóNév
            // 
            this.DolgozóNév.AutoSize = true;
            this.DolgozóNév.Location = new System.Drawing.Point(3, 45);
            this.DolgozóNév.Name = "DolgozóNév";
            this.DolgozóNév.Size = new System.Drawing.Size(49, 20);
            this.DolgozóNév.TabIndex = 225;
            this.DolgozóNév.Text = "<< >>";
            // 
            // TextNév
            // 
            this.TextNév.FormattingEnabled = true;
            this.TextNév.Location = new System.Drawing.Point(305, 3);
            this.TextNév.Name = "TextNév";
            this.TextNév.Size = new System.Drawing.Size(228, 28);
            this.TextNév.TabIndex = 224;
            this.TextNév.SelectionChangeCommitted += new System.EventHandler(this.TextNév_SelectionChangeCommitted);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(3, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(124, 20);
            this.Label1.TabIndex = 223;
            this.Label1.Text = "Felhasználónév:";
            // 
            // CMBMireSzemélyes
            // 
            this.CMBMireSzemélyes.CheckOnClick = true;
            this.CMBMireSzemélyes.FormattingEnabled = true;
            this.CMBMireSzemélyes.Location = new System.Drawing.Point(305, 138);
            this.CMBMireSzemélyes.Name = "CMBMireSzemélyes";
            this.CMBMireSzemélyes.Size = new System.Drawing.Size(228, 151);
            this.CMBMireSzemélyes.TabIndex = 227;
            // 
            // Alap_Rögzít
            // 
            this.Alap_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Alap_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Alap_Rögzít.Location = new System.Drawing.Point(623, 12);
            this.Alap_Rögzít.Name = "Alap_Rögzít";
            this.Alap_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Alap_Rögzít.TabIndex = 226;
            this.Alap_Rögzít.UseVisualStyleBackColor = true;
            this.Alap_Rögzít.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(305, 93);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(228, 28);
            this.Cmbtelephely.TabIndex = 229;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 90);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 228;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CMBMireSzemélyes, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.Cmbtelephely, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.TextNév, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Label13, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.DolgozóNév, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(605, 292);
            this.tableLayoutPanel1.TabIndex = 230;
            // 
            // Ablak_Szoftver_Kulcs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Coral;
            this.ClientSize = new System.Drawing.Size(679, 316);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Alap_Rögzít);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Szoftver_Kulcs";
            this.Text = "Szoftverkulcs létrehozása";
            this.Load += new System.EventHandler(this.Ablak_Szoftver_Kulcs_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label DolgozóNév;
        private System.Windows.Forms.ComboBox TextNév;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.CheckedListBox CMBMireSzemélyes;
        internal System.Windows.Forms.Button Alap_Rögzít;
        internal System.Windows.Forms.ComboBox Cmbtelephely;
        internal System.Windows.Forms.Label Label13;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}