namespace Villamos.Villamos_Ablakok
{
    partial class Ablak_munkalap_dekádoló_csoport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_munkalap_dekádoló_csoport));
            this.CsoportTábla = new System.Windows.Forms.DataGridView();
            this.Command21 = new System.Windows.Forms.Button();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            ((System.ComponentModel.ISupportInitialize)(this.CsoportTábla)).BeginInit();
            this.SuspendLayout();
            // 
            // CsoportTábla
            // 
            this.CsoportTábla.AllowUserToAddRows = false;
            this.CsoportTábla.AllowUserToDeleteRows = false;
            this.CsoportTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CsoportTábla.Location = new System.Drawing.Point(6, 43);
            this.CsoportTábla.Name = "CsoportTábla";
            this.CsoportTábla.RowHeadersVisible = false;
            this.CsoportTábla.RowHeadersWidth = 51;
            this.CsoportTábla.Size = new System.Drawing.Size(401, 322);
            this.CsoportTábla.TabIndex = 71;
            // 
            // Command21
            // 
            this.Command21.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command21.Location = new System.Drawing.Point(414, 45);
            this.Command21.Name = "Command21";
            this.Command21.Size = new System.Drawing.Size(45, 45);
            this.Command21.TabIndex = 70;
            this.Command21.UseVisualStyleBackColor = true;
            this.Command21.Click += new System.EventHandler(this.Command21_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(5, 7);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(455, 30);
            this.Holtart.TabIndex = 72;
            this.Holtart.Visible = false;
            // 
            // Ablak_munkalap_dekádoló_csoport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.ClientSize = new System.Drawing.Size(464, 372);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.CsoportTábla);
            this.Controls.Add(this.Command21);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Ablak_munkalap_dekádoló_csoport";
            this.Text = "Csoportok munkaidő ellenőrzése";
            this.Load += new System.EventHandler(this.Ablak_munkalap_dekádoló_csoport_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_munkalap_dekádoló_csoport_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.CsoportTábla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.DataGridView CsoportTábla;
        internal System.Windows.Forms.Button Command21;
        private V_MindenEgyéb.MyProgressbar Holtart;
    }
}