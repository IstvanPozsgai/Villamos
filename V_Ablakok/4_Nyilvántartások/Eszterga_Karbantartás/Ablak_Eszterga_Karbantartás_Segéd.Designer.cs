namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga
{
    partial class Ablak_Eszterga_Karbantartás_Segéd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Eszterga_Karbantartás_Segéd));
            this.TxtBxUzemOra = new System.Windows.Forms.TextBox();
            this.Lbl_Uzemora = new System.Windows.Forms.Label();
            this.LblElözö = new System.Windows.Forms.Label();
            this.LblSzöveg = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Btn_Rogzit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TxtBxUzemOra
            // 
            this.TxtBxUzemOra.Location = new System.Drawing.Point(31, 83);
            this.TxtBxUzemOra.Name = "TxtBxUzemOra";
            this.TxtBxUzemOra.Size = new System.Drawing.Size(120, 26);
            this.TxtBxUzemOra.TabIndex = 1;
            this.TxtBxUzemOra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBxUzemOra_KeyDown);
            // 
            // Lbl_Uzemora
            // 
            this.Lbl_Uzemora.AutoSize = true;
            this.Lbl_Uzemora.Location = new System.Drawing.Point(27, 56);
            this.Lbl_Uzemora.Name = "Lbl_Uzemora";
            this.Lbl_Uzemora.Size = new System.Drawing.Size(78, 20);
            this.Lbl_Uzemora.TabIndex = 2;
            this.Lbl_Uzemora.Text = "Üzemóra:";
            // 
            // LblElözö
            // 
            this.LblElözö.AutoSize = true;
            this.LblElözö.Location = new System.Drawing.Point(27, 119);
            this.LblElözö.Name = "LblElözö";
            this.LblElözö.Size = new System.Drawing.Size(0, 20);
            this.LblElözö.TabIndex = 3;
            // 
            // LblSzöveg
            // 
            this.LblSzöveg.AutoSize = true;
            this.LblSzöveg.Location = new System.Drawing.Point(27, 23);
            this.LblSzöveg.Name = "LblSzöveg";
            this.LblSzöveg.Size = new System.Drawing.Size(0, 20);
            this.LblSzöveg.TabIndex = 4;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Btn_Rogzit
            // 
            this.Btn_Rogzit.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Rogzit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Rogzit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(234)))), ((int)(((byte)(214)))));
            this.Btn_Rogzit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Rogzit.Location = new System.Drawing.Point(157, 76);
            this.Btn_Rogzit.Name = "Btn_Rogzit";
            this.Btn_Rogzit.Size = new System.Drawing.Size(40, 40);
            this.Btn_Rogzit.TabIndex = 46;
            this.toolTip1.SetToolTip(this.Btn_Rogzit, "Művelet módosítása");
            this.Btn_Rogzit.UseVisualStyleBackColor = true;
            this.Btn_Rogzit.Click += new System.EventHandler(this.BtnRogzit_Click);
            // 
            // Ablak_Eszterga_Karbantartás_Segéd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(234)))), ((int)(((byte)(214)))));
            this.ClientSize = new System.Drawing.Size(365, 189);
            this.Controls.Add(this.Btn_Rogzit);
            this.Controls.Add(this.LblSzöveg);
            this.Controls.Add(this.LblElözö);
            this.Controls.Add(this.Lbl_Uzemora);
            this.Controls.Add(this.TxtBxUzemOra);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Eszterga_Karbantartás_Segéd";
            this.Text = "Kerékeszterga üzemóra rögzítése";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ablak_Eszterga_Karbantartás_Segéd_FormClosing);
            this.Load += new System.EventHandler(this.Ablak_Eszterga_Karbantartás_Segéd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox TxtBxUzemOra;
        private System.Windows.Forms.Label Lbl_Uzemora;
        private System.Windows.Forms.Label LblElözö;
        private System.Windows.Forms.Label LblSzöveg;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Button Btn_Rogzit;
    }
}