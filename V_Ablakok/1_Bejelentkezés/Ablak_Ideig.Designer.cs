namespace Villamos.V_Ablakok._1_Bejelentkezés
{
    partial class Ablak_Ideig
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
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.CmbNevekOld = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtJogkör = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(114, 3);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(277, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 0);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(72, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephely:";
            // 
            // CmbNevekOld
            // 
            this.CmbNevekOld.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbNevekOld.FormattingEnabled = true;
            this.CmbNevekOld.Location = new System.Drawing.Point(114, 37);
            this.CmbNevekOld.Name = "CmbNevekOld";
            this.CmbNevekOld.Size = new System.Drawing.Size(277, 28);
            this.CmbNevekOld.TabIndex = 19;
            this.CmbNevekOld.SelectionChangeCommitted += new System.EventHandler(this.CmbNevekOld_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 20);
            this.label1.TabIndex = 20;
            this.label1.Text = "Felhasználó old";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "Jogosultság old";
            // 
            // TxtJogkör
            // 
            this.TxtJogkör.Location = new System.Drawing.Point(114, 71);
            this.TxtJogkör.Multiline = true;
            this.TxtJogkör.Name = "TxtJogkör";
            this.TxtJogkör.Size = new System.Drawing.Size(490, 113);
            this.TxtJogkör.TabIndex = 22;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.Label13, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Cmbtelephely, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.CmbNevekOld, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.TxtJogkör, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(609, 227);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // Ablak_Ideig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 692);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Ideig";
            this.Text = "Ablak_Ideig";
            this.Load += new System.EventHandler(this.Ablak_Ideig_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.ComboBox Cmbtelephely;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.ComboBox CmbNevekOld;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox TxtJogkör;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}