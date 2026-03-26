namespace Villamos.Villamos_Ablakok.Kerék_nyilvántartás
{
    partial class Ablak_Kerék_Gyűjtő
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Kerék_Gyűjtő));
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Label1 = new System.Windows.Forms.Label();
            this.PályaszámTxt = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Rögzít = new System.Windows.Forms.Button();
            this.Frissít = new System.Windows.Forms.Button();
            this.RögzítOka = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(12, 68);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(1092, 362);
            this.Tábla.TabIndex = 114;
            this.Tábla.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Tábla_EditingControlShowing);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.DarkOrange;
            this.Label1.Location = new System.Drawing.Point(15, 37);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(89, 20);
            this.Label1.TabIndex = 115;
            this.Label1.Text = "Pályaszám:";
            // 
            // PályaszámTxt
            // 
            this.PályaszámTxt.Enabled = false;
            this.PályaszámTxt.Location = new System.Drawing.Point(110, 31);
            this.PályaszámTxt.Name = "PályaszámTxt";
            this.PályaszámTxt.Size = new System.Drawing.Size(89, 26);
            this.PályaszámTxt.TabIndex = 116;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Rögzít
            // 
            this.Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzít.Location = new System.Drawing.Point(1054, 12);
            this.Rögzít.Name = "Rögzít";
            this.Rögzít.Size = new System.Drawing.Size(50, 50);
            this.Rögzít.TabIndex = 119;
            this.toolTip1.SetToolTip(this.Rögzít, "Rögzíti az adatokat");
            this.Rögzít.UseVisualStyleBackColor = true;
            this.Rögzít.Click += new System.EventHandler(this.Rögzít_Click);
            // 
            // Frissít
            // 
            this.Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Frissít.Location = new System.Drawing.Point(205, 12);
            this.Frissít.Name = "Frissít";
            this.Frissít.Size = new System.Drawing.Size(45, 45);
            this.Frissít.TabIndex = 120;
            this.toolTip1.SetToolTip(this.Frissít, "Frissíti a kiválasztott pályaszámnak megfelelően az adatokat");
            this.Frissít.UseVisualStyleBackColor = true;
            this.Frissít.Click += new System.EventHandler(this.Frissít_Click);
            // 
            // RögzítOka
            // 
            this.RögzítOka.Location = new System.Drawing.Point(421, 31);
            this.RögzítOka.MaxLength = 20;
            this.RögzítOka.Name = "RögzítOka";
            this.RögzítOka.Size = new System.Drawing.Size(220, 26);
            this.RögzítOka.TabIndex = 122;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.DarkOrange;
            this.Label6.Location = new System.Drawing.Point(314, 37);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(87, 20);
            this.Label6.TabIndex = 121;
            this.Label6.Text = "Mérés oka:";
            // 
            // Ablak_Kerék_Gyűjtő
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.ClientSize = new System.Drawing.Size(1116, 442);
            this.Controls.Add(this.RögzítOka);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Frissít);
            this.Controls.Add(this.Rögzít);
            this.Controls.Add(this.PályaszámTxt);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Tábla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ablak_Kerék_Gyűjtő";
            this.Text = "Mérési adatok Gyűjtő Rögzítése";
            this.Load += new System.EventHandler(this.Ablak_Kerék_Gyűjtő_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView Tábla;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.TextBox PályaszámTxt;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Button Rögzít;
        internal System.Windows.Forms.Button Frissít;
        internal System.Windows.Forms.TextBox RögzítOka;
        internal System.Windows.Forms.Label Label6;
    }
}