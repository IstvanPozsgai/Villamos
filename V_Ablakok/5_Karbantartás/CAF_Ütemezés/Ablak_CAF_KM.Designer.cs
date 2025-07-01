namespace Villamos.V_Ablakok._5_Karbantartás.CAF_Ütemezés
{
    partial class Ablak_CAF_KM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_CAF_KM));
            this.Tablalista = new Zuby.ADGV.AdvancedDataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.Tablalista)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tablalista
            // 
            this.Tablalista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tablalista.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tablalista.FilterAndSortEnabled = true;
            this.Tablalista.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tablalista.Location = new System.Drawing.Point(3, 3);
            this.Tablalista.MaxFilterButtonImageHeight = 23;
            this.Tablalista.Name = "Tablalista";
            this.Tablalista.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tablalista.Size = new System.Drawing.Size(1036, 564);
            this.Tablalista.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tablalista.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Tablalista, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 8.450705F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.54929F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1042, 570);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // Ablak_CAF_KM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BurlyWood;
            this.ClientSize = new System.Drawing.Size(1060, 591);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Ablak_CAF_KM";
            this.Text = "CAF Km Módosítás";
            this.Load += new System.EventHandler(this.Ablak_CAF_KM_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tablalista)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView Tablalista;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}