namespace Villamos.Villamos_Ablakok
{
    partial class Ablak_munkalap_dekádoló_oszlopot_beilleszt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_munkalap_dekádoló_oszlopot_beilleszt));
            this.Tábla4 = new System.Windows.Forms.DataGridView();
            this.Text6 = new System.Windows.Forms.TextBox();
            this.Button1 = new System.Windows.Forms.Button();
            this.Command17 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla4)).BeginInit();
            this.SuspendLayout();
            // 
            // Tábla4
            // 
            this.Tábla4.AllowUserToAddRows = false;
            this.Tábla4.AllowUserToDeleteRows = false;
            this.Tábla4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla4.Location = new System.Drawing.Point(12, 12);
            this.Tábla4.Name = "Tábla4";
            this.Tábla4.RowHeadersVisible = false;
            this.Tábla4.Size = new System.Drawing.Size(452, 277);
            this.Tábla4.TabIndex = 72;
            this.Tábla4.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla4_CellClick);
            this.Tábla4.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla4_CellDoubleClick);
            // 
            // Text6
            // 
            this.Text6.Location = new System.Drawing.Point(468, 33);
            this.Text6.Name = "Text6";
            this.Text6.Size = new System.Drawing.Size(59, 26);
            this.Text6.TabIndex = 4;
            this.Text6.Text = "5";
            // 
            // Button1
            // 
            this.Button1.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button1.Location = new System.Drawing.Point(476, 65);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(45, 45);
            this.Button1.TabIndex = 87;
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Command17
            // 
            this.Command17.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command17.Location = new System.Drawing.Point(474, 238);
            this.Command17.Name = "Command17";
            this.Command17.Size = new System.Drawing.Size(45, 45);
            this.Command17.TabIndex = 84;
            this.Command17.UseVisualStyleBackColor = true;
            this.Command17.Click += new System.EventHandler(this.Command17_Click);
            // 
            // Ablak_munkalap_dekádoló_oszlopot_beilleszt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.ClientSize = new System.Drawing.Size(540, 297);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Command17);
            this.Controls.Add(this.Tábla4);
            this.Controls.Add(this.Text6);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Ablak_munkalap_dekádoló_oszlopot_beilleszt";
            this.Text = "Új oszlopot beilleszt";
            this.Load += new System.EventHandler(this.Ablak_munkalap_dekádoló_oszlopot_beilleszt_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_munkalap_dekádoló_oszlopot_beilleszt_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Button Command17;
        internal System.Windows.Forms.DataGridView Tábla4;
        internal System.Windows.Forms.TextBox Text6;
    }
}