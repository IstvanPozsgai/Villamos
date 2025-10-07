namespace Villamos.V_MindenEgyéb
{
    partial class Ablak_Hibanaplo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Hibanaplo));
            this.Hibanaplo_Tablazat = new Zuby.ADGV.AdvancedDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Hibanaplo_Tablazat)).BeginInit();
            this.SuspendLayout();
            // 
            // Hibanaplo_Tablazat
            // 
            this.Hibanaplo_Tablazat.AllowUserToAddRows = false;
            this.Hibanaplo_Tablazat.AllowUserToDeleteRows = false;
            this.Hibanaplo_Tablazat.AllowUserToOrderColumns = true;
            this.Hibanaplo_Tablazat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Hibanaplo_Tablazat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Hibanaplo_Tablazat.FilterAndSortEnabled = true;
            this.Hibanaplo_Tablazat.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Hibanaplo_Tablazat.Location = new System.Drawing.Point(0, 0);
            this.Hibanaplo_Tablazat.MaxFilterButtonImageHeight = 23;
            this.Hibanaplo_Tablazat.Name = "Hibanaplo_Tablazat";
            this.Hibanaplo_Tablazat.ReadOnly = true;
            this.Hibanaplo_Tablazat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Hibanaplo_Tablazat.RowHeadersWidth = 51;
            this.Hibanaplo_Tablazat.RowTemplate.Height = 24;
            this.Hibanaplo_Tablazat.Size = new System.Drawing.Size(800, 450);
            this.Hibanaplo_Tablazat.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Hibanaplo_Tablazat.TabIndex = 0;
            // 
            // Ablak_Hibanaplo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Hibanaplo_Tablazat);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Ablak_Hibanaplo";
            this.Text = "Ablak_Hibanaplo";
            this.Load += new System.EventHandler(this.Ablak_Hibanaplo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Hibanaplo_Tablazat)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView Hibanaplo_Tablazat;
    }
}