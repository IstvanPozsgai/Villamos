namespace Villamos.V_Ablakok._7_Gondnokság.Épület_takarítás
{
    partial class Ablak_Opció
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Opció));
            this.Rögzítés = new System.Windows.Forms.Button();
            this.Frissítés = new System.Windows.Forms.Button();
            this.DátumMező = new System.Windows.Forms.Label();
            this.Opció_Tábla = new Zuby.ADGV.AdvancedDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Opció_Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Rögzítés
            // 
            this.Rögzítés.BackgroundImage = global::Villamos.Properties.Resources.mentés32;
            this.Rögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzítés.Location = new System.Drawing.Point(958, 12);
            this.Rögzítés.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Rögzítés.Name = "Rögzítés";
            this.Rögzítés.Size = new System.Drawing.Size(45, 45);
            this.Rögzítés.TabIndex = 189;
            this.Rögzítés.UseVisualStyleBackColor = true;
            this.Rögzítés.Click += new System.EventHandler(this.Rögzítés_Click);
            // 
            // Frissítés
            // 
            this.Frissítés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Frissítés.Location = new System.Drawing.Point(905, 13);
            this.Frissítés.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Frissítés.Name = "Frissítés";
            this.Frissítés.Size = new System.Drawing.Size(45, 45);
            this.Frissítés.TabIndex = 191;
            this.Frissítés.UseVisualStyleBackColor = true;
            this.Frissítés.Click += new System.EventHandler(this.Frissítés_Click);
            // 
            // DátumMező
            // 
            this.DátumMező.AutoSize = true;
            this.DátumMező.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DátumMező.Location = new System.Drawing.Point(14, 22);
            this.DátumMező.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DátumMező.Name = "DátumMező";
            this.DátumMező.Size = new System.Drawing.Size(66, 24);
            this.DátumMező.TabIndex = 192;
            this.DátumMező.Text = "label1";
            // 
            // Opció_Tábla
            // 
            this.Opció_Tábla.AllowUserToAddRows = false;
            this.Opció_Tábla.AllowUserToDeleteRows = false;
            this.Opció_Tábla.AllowUserToResizeRows = false;
            this.Opció_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Opció_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Opció_Tábla.FilterAndSortEnabled = true;
            this.Opció_Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Opció_Tábla.Location = new System.Drawing.Point(18, 66);
            this.Opció_Tábla.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Opció_Tábla.MaxFilterButtonImageHeight = 23;
            this.Opció_Tábla.Name = "Opció_Tábla";
            this.Opció_Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Opció_Tábla.RowHeadersVisible = false;
            this.Opció_Tábla.Size = new System.Drawing.Size(985, 252);
            this.Opció_Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Opció_Tábla.TabIndex = 193;
            // 
            // Ablak_Opció
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 332);
            this.Controls.Add(this.Opció_Tábla);
            this.Controls.Add(this.DátumMező);
            this.Controls.Add(this.Frissítés);
            this.Controls.Add(this.Rögzítés);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Opció";
            this.Text = "Ablak_Opció";
            this.Load += new System.EventHandler(this.Ablak_Opció_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Opció_Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Rögzítés;
        internal System.Windows.Forms.Button Frissítés;
        private System.Windows.Forms.Label DátumMező;
        private Zuby.ADGV.AdvancedDataGridView Opció_Tábla;
    }
}