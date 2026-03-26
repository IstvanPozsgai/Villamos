namespace Villamos.Villamos_Ablakok
{
    partial class Ablak_Kidobó_változat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Kidobó_változat));
            this.Változatalaplista = new System.Windows.Forms.ListBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Újváltozat = new System.Windows.Forms.TextBox();
            this.VáltozatTörlés = new System.Windows.Forms.Button();
            this.ÚjváltozatRögzít = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Változatalaplista
            // 
            this.Változatalaplista.FormattingEnabled = true;
            this.Változatalaplista.ItemHeight = 20;
            this.Változatalaplista.Location = new System.Drawing.Point(129, 55);
            this.Változatalaplista.Name = "Változatalaplista";
            this.Változatalaplista.Size = new System.Drawing.Size(208, 184);
            this.Változatalaplista.TabIndex = 1;
            this.Változatalaplista.SelectedIndexChanged += new System.EventHandler(this.Változatalaplista_SelectedIndexChanged);
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(10, 56);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(72, 20);
            this.Label3.TabIndex = 88;
            this.Label3.Text = "Változat:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(10, 29);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(112, 20);
            this.Label2.TabIndex = 87;
            this.Label2.Text = "Új változatnév:";
            // 
            // Újváltozat
            // 
            this.Újváltozat.Location = new System.Drawing.Point(129, 23);
            this.Újváltozat.Name = "Újváltozat";
            this.Újváltozat.Size = new System.Drawing.Size(207, 26);
            this.Újváltozat.TabIndex = 0;
            // 
            // VáltozatTörlés
            // 
            this.VáltozatTörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.VáltozatTörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VáltozatTörlés.Location = new System.Drawing.Point(342, 56);
            this.VáltozatTörlés.Name = "VáltozatTörlés";
            this.VáltozatTörlés.Size = new System.Drawing.Size(40, 40);
            this.VáltozatTörlés.TabIndex = 3;
            this.VáltozatTörlés.UseVisualStyleBackColor = true;
            this.VáltozatTörlés.Click += new System.EventHandler(this.VáltozatTörlés_Click);
            // 
            // ÚjváltozatRögzít
            // 
            this.ÚjváltozatRögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.ÚjváltozatRögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ÚjváltozatRögzít.Location = new System.Drawing.Point(342, 9);
            this.ÚjváltozatRögzít.Margin = new System.Windows.Forms.Padding(4);
            this.ÚjváltozatRögzít.Name = "ÚjváltozatRögzít";
            this.ÚjváltozatRögzít.Size = new System.Drawing.Size(40, 40);
            this.ÚjváltozatRögzít.TabIndex = 2;
            this.ÚjváltozatRögzít.UseVisualStyleBackColor = true;
            this.ÚjváltozatRögzít.Click += new System.EventHandler(this.ÚjváltozatRögzít_Click);
            // 
            // Ablak_Kidobó_változat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(390, 248);
            this.Controls.Add(this.Változatalaplista);
            this.Controls.Add(this.VáltozatTörlés);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Újváltozat);
            this.Controls.Add(this.ÚjváltozatRögzít);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Ablak_Kidobó_változat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Változat nevek Karbantartása";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Ablak_Kidobó_változat_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_Kidobó_változat_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ListBox Változatalaplista;
        internal System.Windows.Forms.Button VáltozatTörlés;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button ÚjváltozatRögzít;
        internal System.Windows.Forms.TextBox Újváltozat;
    }
}