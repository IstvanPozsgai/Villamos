namespace Villamos.Villamos_Ablakok
{
    partial class Ablak_Eszterga_Dolgozók
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Eszterga_Dolgozók));
            this.Esztergályos_törlés = new System.Windows.Forms.Button();
            this.Esztergályos_Rögzítés = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Dolgozó_nevek = new System.Windows.Forms.ComboBox();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Frissít = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.Telephely = new System.Windows.Forms.ComboBox();
            this.Munkajelleg = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Esztergályos_törlés
            // 
            this.Esztergályos_törlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Esztergályos_törlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Esztergályos_törlés.Location = new System.Drawing.Point(558, 75);
            this.Esztergályos_törlés.Name = "Esztergályos_törlés";
            this.Esztergályos_törlés.Size = new System.Drawing.Size(45, 45);
            this.Esztergályos_törlés.TabIndex = 186;
            this.Esztergályos_törlés.UseVisualStyleBackColor = true;
            this.Esztergályos_törlés.Click += new System.EventHandler(this.Esztergályos_törlés_Click);
            // 
            // Esztergályos_Rögzítés
            // 
            this.Esztergályos_Rögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Esztergályos_Rögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Esztergályos_Rögzítés.Location = new System.Drawing.Point(611, 11);
            this.Esztergályos_Rögzítés.Name = "Esztergályos_Rögzítés";
            this.Esztergályos_Rögzítés.Size = new System.Drawing.Size(45, 45);
            this.Esztergályos_Rögzítés.TabIndex = 185;
            this.Esztergályos_Rögzítés.UseVisualStyleBackColor = true;
            this.Esztergályos_Rögzítés.Click += new System.EventHandler(this.Esztergályos_Rögzítés_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dolgozók";
            // 
            // Dolgozó_nevek
            // 
            this.Dolgozó_nevek.DropDownHeight = 300;
            this.Dolgozó_nevek.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Dolgozó_nevek.FormattingEnabled = true;
            this.Dolgozó_nevek.IntegralHeight = false;
            this.Dolgozó_nevek.Location = new System.Drawing.Point(118, 12);
            this.Dolgozó_nevek.Name = "Dolgozó_nevek";
            this.Dolgozó_nevek.Size = new System.Drawing.Size(382, 28);
            this.Dolgozó_nevek.TabIndex = 1;
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Location = new System.Drawing.Point(4, 132);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(660, 228);
            this.Tábla.TabIndex = 187;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // Frissít
            // 
            this.Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Frissít.Location = new System.Drawing.Point(609, 75);
            this.Frissít.Name = "Frissít";
            this.Frissít.Size = new System.Drawing.Size(45, 45);
            this.Frissít.TabIndex = 189;
            this.Frissít.UseVisualStyleBackColor = true;
            this.Frissít.Click += new System.EventHandler(this.Frissít_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 190;
            this.label2.Text = "Telephely:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 191;
            this.label3.Text = "Munka jellege:";
            // 
            // Telephely
            // 
            this.Telephely.DropDownHeight = 300;
            this.Telephely.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Telephely.FormattingEnabled = true;
            this.Telephely.IntegralHeight = false;
            this.Telephely.Location = new System.Drawing.Point(118, 54);
            this.Telephely.Name = "Telephely";
            this.Telephely.Size = new System.Drawing.Size(262, 28);
            this.Telephely.TabIndex = 192;
            this.Telephely.SelectedIndexChanged += new System.EventHandler(this.Telephely_SelectedIndexChanged);
            // 
            // Munkajelleg
            // 
            this.Munkajelleg.DropDownHeight = 300;
            this.Munkajelleg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Munkajelleg.FormattingEnabled = true;
            this.Munkajelleg.IntegralHeight = false;
            this.Munkajelleg.Location = new System.Drawing.Point(118, 98);
            this.Munkajelleg.Name = "Munkajelleg";
            this.Munkajelleg.Size = new System.Drawing.Size(262, 28);
            this.Munkajelleg.TabIndex = 193;
            // 
            // Ablak_Eszterga_Dolgozók
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Olive;
            this.ClientSize = new System.Drawing.Size(668, 372);
            this.Controls.Add(this.Munkajelleg);
            this.Controls.Add(this.Telephely);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Frissít);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.Esztergályos_Rögzítés);
            this.Controls.Add(this.Esztergályos_törlés);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Dolgozó_nevek);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Ablak_Eszterga_Dolgozók";
            this.Text = "Kerékesztergára beosztható dolgozók";
            this.Load += new System.EventHandler(this.Ablak_Eszterga_Dolgozók_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_Eszterga_Dolgozók_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Esztergályos_törlés;
        internal System.Windows.Forms.Button Esztergályos_Rögzítés;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Dolgozó_nevek;
        private System.Windows.Forms.DataGridView Tábla;
        internal System.Windows.Forms.Button Frissít;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Telephely;
        private System.Windows.Forms.ComboBox Munkajelleg;
    }
}