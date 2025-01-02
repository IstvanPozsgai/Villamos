namespace Villamos.Villamos_Ablakok.Kerékeszterga
{
    partial class Ablak_Eszterga_Beosztás
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Eszterga_Beosztás));
            this.Terv_Tábla = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.Terv_Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Terv_Tábla
            // 
            this.Terv_Tábla.AllowUserToAddRows = false;
            this.Terv_Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Terv_Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Terv_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Terv_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Terv_Tábla.EnableHeadersVisualStyles = false;
            this.Terv_Tábla.Location = new System.Drawing.Point(12, 12);
            this.Terv_Tábla.Name = "Terv_Tábla";
            this.Terv_Tábla.RowHeadersVisible = false;
            this.Terv_Tábla.Size = new System.Drawing.Size(655, 339);
            this.Terv_Tábla.TabIndex = 188;
            // 
            // Ablak_Eszterga_Beosztás
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 363);
            this.Controls.Add(this.Terv_Tábla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Ablak_Eszterga_Beosztás";
            this.Text = "Ablak_Eszterga_Beosztás";
            this.Load += new System.EventHandler(this.Ablak_Eszterga_Beosztás_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_Eszterga_Beosztás_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Terv_Tábla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.DataGridView Terv_Tábla;
    }
}