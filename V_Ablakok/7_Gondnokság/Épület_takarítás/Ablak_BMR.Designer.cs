namespace Villamos.V_Ablakok._7_Gondnokság.Épület_takarítás
{
    partial class Ablak_BMR
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
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.DátumMező = new System.Windows.Forms.Label();
            this.Frissítés = new System.Windows.Forms.Button();
            this.Rögzítés = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.FilterAndSortEnabled = true;
            this.Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.Location = new System.Drawing.Point(12, 61);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(409, 325);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 0;
            // 
            // DátumMező
            // 
            this.DátumMező.AutoSize = true;
            this.DátumMező.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DátumMező.Location = new System.Drawing.Point(13, 9);
            this.DátumMező.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.DátumMező.Name = "DátumMező";
            this.DátumMező.Size = new System.Drawing.Size(66, 24);
            this.DátumMező.TabIndex = 194;
            this.DátumMező.Text = "label1";
            // 
            // Frissítés
            // 
            this.Frissítés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Frissítés.Location = new System.Drawing.Point(323, 9);
            this.Frissítés.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Frissítés.Name = "Frissítés";
            this.Frissítés.Size = new System.Drawing.Size(45, 45);
            this.Frissítés.TabIndex = 193;
            this.Frissítés.UseVisualStyleBackColor = true;
            this.Frissítés.Click += new System.EventHandler(this.Frissítés_Click);
            // 
            // Rögzítés
            // 
            this.Rögzítés.BackgroundImage = global::Villamos.Properties.Resources.mentés32;
            this.Rögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzítés.Location = new System.Drawing.Point(376, 9);
            this.Rögzítés.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Rögzítés.Name = "Rögzítés";
            this.Rögzítés.Size = new System.Drawing.Size(45, 45);
            this.Rögzítés.TabIndex = 192;
            this.Rögzítés.UseVisualStyleBackColor = true;
            this.Rögzítés.Click += new System.EventHandler(this.Rögzítés_Click);
            // 
            // Ablak_BMR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 395);
            this.Controls.Add(this.DátumMező);
            this.Controls.Add(this.Frissítés);
            this.Controls.Add(this.Rögzítés);
            this.Controls.Add(this.Tábla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ablak_BMR";
            this.Text = "Ablak_BMR";
            this.Load += new System.EventHandler(this.Ablak_BMR_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView Tábla;
        internal System.Windows.Forms.Button Frissítés;
        internal System.Windows.Forms.Button Rögzítés;
        private System.Windows.Forms.Label DátumMező;
    }
}