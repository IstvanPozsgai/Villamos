namespace Villamos.V_Ablakok._5_Karbantartás.T5C5
{
    partial class Ablak_T5C5_Vonalak
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_T5C5_Vonalak));
            this.Vonal_blue = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Vonal_tábla = new System.Windows.Forms.DataGridView();
            this.Vonal_fel = new System.Windows.Forms.Button();
            this.Command7_Rögzítés = new System.Windows.Forms.Button();
            this.Command8_Új = new System.Windows.Forms.Button();
            this.Command11_frissít = new System.Windows.Forms.Button();
            this.Command10_Listát_töröl = new System.Windows.Forms.Button();
            this.Command9_színkereső = new System.Windows.Forms.Button();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.Vonal_red = new System.Windows.Forms.TextBox();
            this.Vonal_Id = new System.Windows.Forms.TextBox();
            this.Vonal_Vonal = new System.Windows.Forms.TextBox();
            this.Vonal_Mennyiség = new System.Windows.Forms.TextBox();
            this.Vonal_green = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Vonal_tábla)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Vonal_blue
            // 
            this.Vonal_blue.BackColor = System.Drawing.Color.White;
            this.Vonal_blue.Enabled = false;
            this.Vonal_blue.Location = new System.Drawing.Point(481, 38);
            this.Vonal_blue.Name = "Vonal_blue";
            this.Vonal_blue.Size = new System.Drawing.Size(62, 26);
            this.Vonal_blue.TabIndex = 92;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(250, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(89, 20);
            this.Label4.TabIndex = 89;
            this.Label4.Text = "Mennyiség:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(345, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(44, 20);
            this.Label3.TabIndex = 81;
            this.Label3.Text = "Szín:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(85, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(54, 20);
            this.Label2.TabIndex = 80;
            this.Label2.Text = "Vonal:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(3, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(76, 20);
            this.Label1.TabIndex = 79;
            this.Label1.Text = "Sorszám:";
            // 
            // Vonal_tábla
            // 
            this.Vonal_tábla.AllowUserToAddRows = false;
            this.Vonal_tábla.AllowUserToDeleteRows = false;
            this.Vonal_tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Vonal_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Vonal_tábla.Location = new System.Drawing.Point(10, 85);
            this.Vonal_tábla.Name = "Vonal_tábla";
            this.Vonal_tábla.ReadOnly = true;
            this.Vonal_tábla.RowHeadersVisible = false;
            this.Vonal_tábla.Size = new System.Drawing.Size(1063, 383);
            this.Vonal_tábla.TabIndex = 78;
            this.Vonal_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Vonal_tábla_CellClick);
            // 
            // Vonal_fel
            // 
            this.Vonal_fel.BackgroundImage = global::Villamos.Properties.Resources.Button_Upload_01;
            this.Vonal_fel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Vonal_fel.Location = new System.Drawing.Point(278, 3);
            this.Vonal_fel.Name = "Vonal_fel";
            this.Vonal_fel.Size = new System.Drawing.Size(45, 45);
            this.Vonal_fel.TabIndex = 91;
            this.Vonal_fel.UseVisualStyleBackColor = true;
            this.Vonal_fel.Click += new System.EventHandler(this.Vonal_fel_Click);
            // 
            // Command7_Rögzítés
            // 
            this.Command7_Rögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command7_Rögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command7_Rögzítés.Location = new System.Drawing.Point(113, 3);
            this.Command7_Rögzítés.Name = "Command7_Rögzítés";
            this.Command7_Rögzítés.Size = new System.Drawing.Size(45, 45);
            this.Command7_Rögzítés.TabIndex = 86;
            this.Command7_Rögzítés.UseVisualStyleBackColor = true;
            this.Command7_Rögzítés.Click += new System.EventHandler(this.Command7_Rögzítés_Click);
            // 
            // Command8_Új
            // 
            this.Command8_Új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Command8_Új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command8_Új.Location = new System.Drawing.Point(168, 3);
            this.Command8_Új.Name = "Command8_Új";
            this.Command8_Új.Size = new System.Drawing.Size(45, 45);
            this.Command8_Új.TabIndex = 85;
            this.Command8_Új.UseVisualStyleBackColor = true;
            this.Command8_Új.Click += new System.EventHandler(this.Command8_Új_Click);
            // 
            // Command11_frissít
            // 
            this.Command11_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command11_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command11_frissít.Location = new System.Drawing.Point(223, 3);
            this.Command11_frissít.Name = "Command11_frissít";
            this.Command11_frissít.Size = new System.Drawing.Size(45, 45);
            this.Command11_frissít.TabIndex = 84;
            this.Command11_frissít.UseVisualStyleBackColor = true;
            this.Command11_frissít.Click += new System.EventHandler(this.Command11_frissít_Click);
            // 
            // Command10_Listát_töröl
            // 
            this.Command10_Listát_töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Command10_Listát_töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command10_Listát_töröl.Location = new System.Drawing.Point(333, 3);
            this.Command10_Listát_töröl.Name = "Command10_Listát_töröl";
            this.Command10_Listát_töröl.Size = new System.Drawing.Size(45, 45);
            this.Command10_Listát_töröl.TabIndex = 83;
            this.Command10_Listát_töröl.UseVisualStyleBackColor = true;
            this.Command10_Listát_töröl.Click += new System.EventHandler(this.Command10_Listát_töröl_Click);
            // 
            // Command9_színkereső
            // 
            this.Command9_színkereső.BackgroundImage = global::Villamos.Properties.Resources.Yellow_Glass_Folders_Icon_25;
            this.Command9_színkereső.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command9_színkereső.Location = new System.Drawing.Point(3, 3);
            this.Command9_színkereső.Name = "Command9_színkereső";
            this.Command9_színkereső.Size = new System.Drawing.Size(45, 45);
            this.Command9_színkereső.TabIndex = 82;
            this.Command9_színkereső.UseVisualStyleBackColor = true;
            this.Command9_színkereső.Click += new System.EventHandler(this.Command9_színkereső_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(10, 189);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(1058, 21);
            this.Holtart.TabIndex = 127;
            this.Holtart.Visible = false;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(450, 3);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 126;
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.Command9_színkereső, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Command7_Rögzítés, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.Command8_Új, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.Command11_frissít, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.Vonal_fel, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.Command10_Listát_töröl, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.BtnSúgó, 8, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(575, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(498, 60);
            this.tableLayoutPanel1.TabIndex = 128;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 87F));
            this.tableLayoutPanel2.Controls.Add(this.Label1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.Label2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.Label4, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.Vonal_blue, 5, 1);
            this.tableLayoutPanel2.Controls.Add(this.Vonal_red, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.Vonal_Id, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.Vonal_Vonal, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.Vonal_Mennyiség, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.Label3, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.Vonal_green, 4, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(12, 10);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(557, 69);
            this.tableLayoutPanel2.TabIndex = 129;
            // 
            // Vonal_red
            // 
            this.Vonal_red.BackColor = System.Drawing.Color.White;
            this.Vonal_red.Enabled = false;
            this.Vonal_red.Location = new System.Drawing.Point(345, 38);
            this.Vonal_red.Name = "Vonal_red";
            this.Vonal_red.Size = new System.Drawing.Size(62, 26);
            this.Vonal_red.TabIndex = 94;
            // 
            // Vonal_Id
            // 
            this.Vonal_Id.BackColor = System.Drawing.Color.White;
            this.Vonal_Id.Enabled = false;
            this.Vonal_Id.Location = new System.Drawing.Point(3, 38);
            this.Vonal_Id.Name = "Vonal_Id";
            this.Vonal_Id.Size = new System.Drawing.Size(72, 26);
            this.Vonal_Id.TabIndex = 87;
            // 
            // Vonal_Vonal
            // 
            this.Vonal_Vonal.BackColor = System.Drawing.Color.White;
            this.Vonal_Vonal.Location = new System.Drawing.Point(85, 38);
            this.Vonal_Vonal.Name = "Vonal_Vonal";
            this.Vonal_Vonal.Size = new System.Drawing.Size(159, 26);
            this.Vonal_Vonal.TabIndex = 88;
            // 
            // Vonal_Mennyiség
            // 
            this.Vonal_Mennyiség.BackColor = System.Drawing.Color.White;
            this.Vonal_Mennyiség.Location = new System.Drawing.Point(250, 38);
            this.Vonal_Mennyiség.Name = "Vonal_Mennyiség";
            this.Vonal_Mennyiség.Size = new System.Drawing.Size(85, 26);
            this.Vonal_Mennyiség.TabIndex = 90;
            // 
            // Vonal_green
            // 
            this.Vonal_green.BackColor = System.Drawing.Color.White;
            this.Vonal_green.Enabled = false;
            this.Vonal_green.Location = new System.Drawing.Point(413, 38);
            this.Vonal_green.Name = "Vonal_green";
            this.Vonal_green.Size = new System.Drawing.Size(62, 26);
            this.Vonal_green.TabIndex = 93;
            // 
            // Ablak_T5C5_Vonalak
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(1083, 480);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Vonal_tábla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_T5C5_Vonalak";
            this.Text = "T5C5 Vonalak";
            this.Load += new System.EventHandler(this.Ablak_T5C5_Vonalak_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Vonal_tábla)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.TextBox Vonal_blue;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.DataGridView Vonal_tábla;
        internal System.Windows.Forms.Button Vonal_fel;
        internal System.Windows.Forms.Button Command7_Rögzítés;
        internal System.Windows.Forms.Button Command8_Új;
        internal System.Windows.Forms.Button Command11_frissít;
        internal System.Windows.Forms.Button Command10_Listát_töröl;
        internal System.Windows.Forms.Button Command9_színkereső;
        private V_MindenEgyéb.MyProgressbar Holtart;
        internal System.Windows.Forms.Button BtnSúgó;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        internal System.Windows.Forms.TextBox Vonal_red;
        internal System.Windows.Forms.TextBox Vonal_Id;
        internal System.Windows.Forms.TextBox Vonal_Vonal;
        internal System.Windows.Forms.TextBox Vonal_Mennyiség;
        internal System.Windows.Forms.TextBox Vonal_green;
    }
}