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
            this.Hibanaplo_Tablazat = new Zuby.ADGV.AdvancedDataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmb_valaszthato_evek = new System.Windows.Forms.ComboBox();
            this.btn_frissit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Hibanaplo_Tablazat)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.Hibanaplo_Tablazat.Location = new System.Drawing.Point(3, 57);
            this.Hibanaplo_Tablazat.MaxFilterButtonImageHeight = 23;
            this.Hibanaplo_Tablazat.Name = "Hibanaplo_Tablazat";
            this.Hibanaplo_Tablazat.ReadOnly = true;
            this.Hibanaplo_Tablazat.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Hibanaplo_Tablazat.RowHeadersWidth = 51;
            this.Hibanaplo_Tablazat.RowTemplate.Height = 24;
            this.Hibanaplo_Tablazat.Size = new System.Drawing.Size(794, 404);
            this.Hibanaplo_Tablazat.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Hibanaplo_Tablazat.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Hibanaplo_Tablazat, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmb_valaszthato_evek);
            this.panel1.Controls.Add(this.btn_frissit);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(794, 48);
            this.panel1.TabIndex = 1;
            // 
            // cmb_valaszthato_evek
            // 
            this.cmb_valaszthato_evek.FormattingEnabled = true;
            this.cmb_valaszthato_evek.Location = new System.Drawing.Point(3, 11);
            this.cmb_valaszthato_evek.Name = "cmb_valaszthato_evek";
            this.cmb_valaszthato_evek.Size = new System.Drawing.Size(138, 24);
            this.cmb_valaszthato_evek.TabIndex = 4;
            this.cmb_valaszthato_evek.SelectionChangeCommitted += new System.EventHandler(this.cmb_valaszthato_evek_SelectionChangeCommitted);
            // 
            // btn_frissit
            // 
            this.btn_frissit.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.btn_frissit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_frissit.Location = new System.Drawing.Point(147, -3);
            this.btn_frissit.Name = "btn_frissit";
            this.btn_frissit.Size = new System.Drawing.Size(50, 50);
            this.btn_frissit.TabIndex = 3;
            this.btn_frissit.UseVisualStyleBackColor = true;
            // 
            // Ablak_Hibanaplo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Ablak_Hibanaplo";
            this.Text = "Ablak_Hibanaplo";
            this.Load += new System.EventHandler(this.Ablak_Hibanaplo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Hibanaplo_Tablazat)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Zuby.ADGV.AdvancedDataGridView Hibanaplo_Tablazat;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmb_valaszthato_evek;
        private System.Windows.Forms.Button btn_frissit;

    }
}