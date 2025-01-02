namespace Villamos
{
    partial class Ablak_Kereső
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Kereső));
            this.Keresés_OK = new System.Windows.Forms.Button();
            this.Keresett = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Keresés_OK
            // 
            this.Keresés_OK.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Keresés_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Keresés_OK.Location = new System.Drawing.Point(258, 5);
            this.Keresés_OK.Margin = new System.Windows.Forms.Padding(4);
            this.Keresés_OK.Name = "Keresés_OK";
            this.Keresés_OK.Size = new System.Drawing.Size(40, 40);
            this.Keresés_OK.TabIndex = 95;
            this.Keresés_OK.UseVisualStyleBackColor = true;
            this.Keresés_OK.Click += new System.EventHandler(this.Keresés_OK_Click);
            // 
            // Keresett
            // 
            this.Keresett.Location = new System.Drawing.Point(12, 12);
            this.Keresett.Name = "Keresett";
            this.Keresett.Size = new System.Drawing.Size(219, 26);
            this.Keresett.TabIndex = 94;
            // 
            // Ablak_Kereső
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 49);
            this.Controls.Add(this.Keresés_OK);
            this.Controls.Add(this.Keresett);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ablak_Kereső";
            this.Text = "Keresés";
            this.Load += new System.EventHandler(this.Ablak_Kereső_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_Kereső_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Button Keresés_OK;
        internal System.Windows.Forms.TextBox Keresett;
    }
}