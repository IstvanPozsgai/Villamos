namespace Villamos.Villamos_Ablakok
{
    partial class Ablak_munkalap_dekádoló_oszlopot_készít
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_munkalap_dekádoló_oszlopot_készít));
            this.Text3 = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Text4 = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Text2 = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Text5 = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Command8 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Text3
            // 
            this.Text3.Location = new System.Drawing.Point(144, 105);
            this.Text3.MaxLength = 20;
            this.Text3.Name = "Text3";
            this.Text3.Size = new System.Drawing.Size(145, 26);
            this.Text3.TabIndex = 3;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(12, 111);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(111, 20);
            this.Label5.TabIndex = 88;
            this.Label5.Text = "Munka leírása:";
            // 
            // Text4
            // 
            this.Text4.Location = new System.Drawing.Point(144, 71);
            this.Text4.MaxLength = 20;
            this.Text4.Name = "Text4";
            this.Text4.Size = new System.Drawing.Size(145, 26);
            this.Text4.TabIndex = 2;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(12, 77);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(117, 20);
            this.Label4.TabIndex = 86;
            this.Label4.Text = "Psz vagy Típus:";
            // 
            // Text2
            // 
            this.Text2.Location = new System.Drawing.Point(144, 37);
            this.Text2.MaxLength = 20;
            this.Text2.Name = "Text2";
            this.Text2.Size = new System.Drawing.Size(145, 26);
            this.Text2.TabIndex = 1;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(12, 43);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(106, 20);
            this.Label3.TabIndex = 84;
            this.Label3.Text = "Műveletszám:";
            // 
            // Text5
            // 
            this.Text5.Location = new System.Drawing.Point(144, 3);
            this.Text5.MaxLength = 20;
            this.Text5.Name = "Text5";
            this.Text5.Size = new System.Drawing.Size(145, 26);
            this.Text5.TabIndex = 0;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(12, 9);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(126, 20);
            this.Label2.TabIndex = 2;
            this.Label2.Text = "Rendelési szám:";
            // 
            // Command8
            // 
            this.Command8.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command8.Location = new System.Drawing.Point(295, 86);
            this.Command8.Name = "Command8";
            this.Command8.Size = new System.Drawing.Size(45, 45);
            this.Command8.TabIndex = 4;
            this.Command8.UseVisualStyleBackColor = true;
            this.Command8.Click += new System.EventHandler(this.Command8_Click);
            // 
            // Ablak_munkalap_dekádoló_oszlopot_készít
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.ClientSize = new System.Drawing.Size(350, 138);
            this.Controls.Add(this.Text3);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Text4);
            this.Controls.Add(this.Text5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Command8);
            this.Controls.Add(this.Text2);
            this.Controls.Add(this.Label3);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Ablak_munkalap_dekádoló_oszlopot_készít";
            this.Text = "Új oszlopot készít";
            this.Load += new System.EventHandler(this.Ablak_munkalap_dekádoló_oszlopot_készít_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_munkalap_dekádoló_oszlopot_készít_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.TextBox Text3;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox Text4;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.TextBox Text2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Button Command8;
        internal System.Windows.Forms.TextBox Text5;
        internal System.Windows.Forms.Label Label2;
    }
}