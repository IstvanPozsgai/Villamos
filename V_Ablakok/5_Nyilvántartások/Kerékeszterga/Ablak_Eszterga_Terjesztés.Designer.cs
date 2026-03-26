namespace Villamos.Villamos_Ablakok.Kerékeszterga
{
    partial class Ablak_Eszterga_Terjesztés
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Eszterga_Terjesztés));
            this.Email = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Rögzít = new System.Windows.Forms.Button();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CmbVáltozat = new System.Windows.Forms.ComboBox();
            this.Töröl = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.Név = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Email
            // 
            this.Email.Location = new System.Drawing.Point(165, 44);
            this.Email.MaxLength = 50;
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(303, 26);
            this.Email.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "E-mail cím:";
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Location = new System.Drawing.Point(12, 144);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(681, 191);
            this.Tábla.TabIndex = 2;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // Rögzít
            // 
            this.Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzít.Location = new System.Drawing.Point(645, 12);
            this.Rögzít.Name = "Rögzít";
            this.Rögzít.Size = new System.Drawing.Size(48, 48);
            this.Rögzít.TabIndex = 3;
            this.toolTip1.SetToolTip(this.Rögzít, "Rögzít/ Módosít");
            this.Rögzít.UseVisualStyleBackColor = true;
            this.Rögzít.Click += new System.EventHandler(this.Rögzít_Click);
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.DropDownHeight = 300;
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.IntegralHeight = false;
            this.Cmbtelephely.Location = new System.Drawing.Point(165, 76);
            this.Cmbtelephely.MaxLength = 50;
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(188, 28);
            this.Cmbtelephely.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Telephely:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Terjesztési változat:";
            // 
            // CmbVáltozat
            // 
            this.CmbVáltozat.DropDownHeight = 300;
            this.CmbVáltozat.FormattingEnabled = true;
            this.CmbVáltozat.IntegralHeight = false;
            this.CmbVáltozat.Location = new System.Drawing.Point(165, 110);
            this.CmbVáltozat.Name = "CmbVáltozat";
            this.CmbVáltozat.Size = new System.Drawing.Size(188, 28);
            this.CmbVáltozat.TabIndex = 7;
            // 
            // Töröl
            // 
            this.Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Töröl.Location = new System.Drawing.Point(645, 76);
            this.Töröl.Name = "Töröl";
            this.Töröl.Size = new System.Drawing.Size(48, 48);
            this.Töröl.TabIndex = 8;
            this.toolTip1.SetToolTip(this.Töröl, "Törli az adatokat");
            this.Töröl.UseVisualStyleBackColor = true;
            this.Töröl.Click += new System.EventHandler(this.Töröl_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Név:";
            // 
            // Név
            // 
            this.Név.Location = new System.Drawing.Point(165, 12);
            this.Név.MaxLength = 50;
            this.Név.Name = "Név";
            this.Név.Size = new System.Drawing.Size(303, 26);
            this.Név.TabIndex = 9;
            // 
            // Ablak_Eszterga_Terjesztés
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(701, 341);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Név);
            this.Controls.Add(this.Töröl);
            this.Controls.Add(this.CmbVáltozat);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Cmbtelephely);
            this.Controls.Add(this.Rögzít);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Email);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Ablak_Eszterga_Terjesztés";
            this.Text = "Terjesztési Lista";
            this.Load += new System.EventHandler(this.Ablak_Eszterga_Terjesztés_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Email;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView Tábla;
        internal System.Windows.Forms.Button Rögzít;
        private System.Windows.Forms.ComboBox Cmbtelephely;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CmbVáltozat;
        internal System.Windows.Forms.Button Töröl;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Név;
    }
}