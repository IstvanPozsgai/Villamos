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
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(12, 12);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(404, 33);
            this.Panel1.TabIndex = 48;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(175, 2);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(215, 28);
            this.Cmbtelephely.TabIndex = 18;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 4);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(128, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Ablak_Ideig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 692);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Ideig";
            this.Text = "Ablak_Ideig";
            this.Load += new System.EventHandler(this.Ablak_Ideig_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.ComboBox Cmbtelephely;
        internal System.Windows.Forms.Label Label13;
    }
}