namespace Villamos
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.cmb_korabbi_evek = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.Hibanaplo_Tablazat)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.Hibanaplo_Tablazat.Location = new System.Drawing.Point(3, 33);
            this.Hibanaplo_Tablazat.MaxFilterButtonImageHeight = 23;
            this.Hibanaplo_Tablazat.Name = "Hibanaplo_Tablazat";
            this.Hibanaplo_Tablazat.ReadOnly = true;
            this.Hibanaplo_Tablazat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Hibanaplo_Tablazat.RowHeadersWidth = 51;
            this.Hibanaplo_Tablazat.RowTemplate.Height = 24;
            this.Hibanaplo_Tablazat.Size = new System.Drawing.Size(794, 414);
            this.Hibanaplo_Tablazat.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Hibanaplo_Tablazat.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Hibanaplo_Tablazat, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cmb_korabbi_evek, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // cmb_korabbi_evek
            // 
            this.cmb_korabbi_evek.FormattingEnabled = true;
            this.cmb_korabbi_evek.Location = new System.Drawing.Point(3, 3);
            this.cmb_korabbi_evek.Name = "cmb_korabbi_evek";
            this.cmb_korabbi_evek.Size = new System.Drawing.Size(140, 24);
            this.cmb_korabbi_evek.TabIndex = 1;
            // 
            // Ablak_Hibanaplo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Ablak_Hibanaplo";
            this.Text = "Hibanapló";
            this.Load += new System.EventHandler(this.Ablak_Hibanaplo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Hibanaplo_Tablazat)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView Hibanaplo_Tablazat;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox cmb_korabbi_evek;
    }
}