namespace Villamos.V_Ablakok._4_Nyilvántartások.Nóta
{
    partial class Ablak_Nóta_Összesítés
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
            this.Frissíti_táblalistát = new System.Windows.Forms.Button();
            this.FődarabTípusok = new System.Windows.Forms.ComboBox();
            this.Tábla = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Frissíti_táblalistát
            // 
            this.Frissíti_táblalistát.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissíti_táblalistát.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Frissíti_táblalistát.Location = new System.Drawing.Point(311, 12);
            this.Frissíti_táblalistát.Name = "Frissíti_táblalistát";
            this.Frissíti_táblalistát.Size = new System.Drawing.Size(45, 45);
            this.Frissíti_táblalistát.TabIndex = 187;
            this.Frissíti_táblalistát.UseVisualStyleBackColor = true;
            this.Frissíti_táblalistát.Click += new System.EventHandler(this.Frissíti_táblalistát_Click);
            // 
            // FődarabTípusok
            // 
            this.FődarabTípusok.FormattingEnabled = true;
            this.FődarabTípusok.Location = new System.Drawing.Point(5, 29);
            this.FődarabTípusok.Name = "FődarabTípusok";
            this.FődarabTípusok.Size = new System.Drawing.Size(300, 28);
            this.FődarabTípusok.TabIndex = 188;
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Location = new System.Drawing.Point(5, 63);
            this.Tábla.Name = "Tábla";
            this.Tábla.Size = new System.Drawing.Size(832, 219);
            this.Tábla.TabIndex = 189;
            // 
            // Ablak_Nóta_Összesítés
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 294);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.FődarabTípusok);
            this.Controls.Add(this.Frissíti_táblalistát);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Nóta_Összesítés";
            this.Text = "Fődarab adatok Összesítése";
            this.Load += new System.EventHandler(this.Ablak_Nóta_Összesítés_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Button Frissíti_táblalistát;
        private System.Windows.Forms.ComboBox FődarabTípusok;
        private System.Windows.Forms.DataGridView Tábla;
    }
}