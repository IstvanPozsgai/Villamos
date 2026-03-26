namespace Villamos.Villamos_Ablakok._3_Dolgozó.Karbantartási_Munkalapok
{
    partial class Ablak_Karbantartási_Rendelés
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Karbantartási_Rendelés));
            this.Rendelés_Töröl = new System.Windows.Forms.Button();
            this.Rendelés_Rendelés = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.Rendelés_Frissít = new System.Windows.Forms.Button();
            this.Rendelés_Tábla = new System.Windows.Forms.DataGridView();
            this.Rendelés_Ok = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.Rendelés_Dátum = new System.Windows.Forms.DateTimePicker();
            this.Rendelés_Típus = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.Rendelés_Ciklus = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Rendelés_Tábla)).BeginInit();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Rendelés_Töröl
            // 
            this.Rendelés_Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Rendelés_Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rendelés_Töröl.Location = new System.Drawing.Point(421, 150);
            this.Rendelés_Töröl.Name = "Rendelés_Töröl";
            this.Rendelés_Töröl.Size = new System.Drawing.Size(45, 45);
            this.Rendelés_Töröl.TabIndex = 248;
            this.toolTip1.SetToolTip(this.Rendelés_Töröl, "Törli az Adatokat");
            this.Rendelés_Töröl.UseVisualStyleBackColor = true;
            this.Rendelés_Töröl.Click += new System.EventHandler(this.Rendelés_Töröl_Click);
            // 
            // Rendelés_Rendelés
            // 
            this.Rendelés_Rendelés.Location = new System.Drawing.Point(167, 169);
            this.Rendelés_Rendelés.Name = "Rendelés_Rendelés";
            this.Rendelés_Rendelés.Size = new System.Drawing.Size(239, 26);
            this.Rendelés_Rendelés.TabIndex = 247;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Silver;
            this.label17.Location = new System.Drawing.Point(12, 169);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(126, 20);
            this.label17.TabIndex = 246;
            this.label17.Text = "Rendelési szám:";
            // 
            // Rendelés_Frissít
            // 
            this.Rendelés_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Rendelés_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rendelés_Frissít.Location = new System.Drawing.Point(472, 150);
            this.Rendelés_Frissít.Name = "Rendelés_Frissít";
            this.Rendelés_Frissít.Size = new System.Drawing.Size(45, 45);
            this.Rendelés_Frissít.TabIndex = 245;
            this.toolTip1.SetToolTip(this.Rendelés_Frissít, "Frissíti a táblázat adatait");
            this.Rendelés_Frissít.UseVisualStyleBackColor = true;
            this.Rendelés_Frissít.Click += new System.EventHandler(this.Rendelés_Frissít_Click);
            // 
            // Rendelés_Tábla
            // 
            this.Rendelés_Tábla.AllowUserToAddRows = false;
            this.Rendelés_Tábla.AllowUserToDeleteRows = false;
            this.Rendelés_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ButtonShadow;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Rendelés_Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Rendelés_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Rendelés_Tábla.EnableHeadersVisualStyles = false;
            this.Rendelés_Tábla.Location = new System.Drawing.Point(7, 206);
            this.Rendelés_Tábla.Name = "Rendelés_Tábla";
            this.Rendelés_Tábla.RowHeadersVisible = false;
            this.Rendelés_Tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.Rendelés_Tábla.Size = new System.Drawing.Size(619, 270);
            this.Rendelés_Tábla.TabIndex = 244;
            this.Rendelés_Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Rendelés_Tábla_CellClick);
            // 
            // Rendelés_Ok
            // 
            this.Rendelés_Ok.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rendelés_Ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rendelés_Ok.Location = new System.Drawing.Point(421, 66);
            this.Rendelés_Ok.Name = "Rendelés_Ok";
            this.Rendelés_Ok.Size = new System.Drawing.Size(45, 45);
            this.Rendelés_Ok.TabIndex = 243;
            this.toolTip1.SetToolTip(this.Rendelés_Ok, "Rögzíti/Módosítja az adatokat");
            this.Rendelés_Ok.UseVisualStyleBackColor = true;
            this.Rendelés_Ok.Click += new System.EventHandler(this.Rendelés_Ok_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Silver;
            this.label16.Location = new System.Drawing.Point(12, 71);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 20);
            this.label16.TabIndex = 242;
            this.label16.Text = "Dátum:";
            // 
            // Rendelés_Dátum
            // 
            this.Rendelés_Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Rendelés_Dátum.Location = new System.Drawing.Point(167, 66);
            this.Rendelés_Dátum.Name = "Rendelés_Dátum";
            this.Rendelés_Dátum.Size = new System.Drawing.Size(105, 26);
            this.Rendelés_Dátum.TabIndex = 241;
            // 
            // Rendelés_Típus
            // 
            this.Rendelés_Típus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Rendelés_Típus.FormattingEnabled = true;
            this.Rendelés_Típus.Location = new System.Drawing.Point(167, 99);
            this.Rendelés_Típus.Name = "Rendelés_Típus";
            this.Rendelés_Típus.Size = new System.Drawing.Size(239, 28);
            this.Rendelés_Típus.TabIndex = 237;
            this.Rendelés_Típus.SelectedIndexChanged += new System.EventHandler(this.Rendelés_Típus_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Silver;
            this.label14.Location = new System.Drawing.Point(12, 102);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(95, 20);
            this.label14.TabIndex = 238;
            this.label14.Text = "Jármű típus:";
            // 
            // Rendelés_Ciklus
            // 
            this.Rendelés_Ciklus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Rendelés_Ciklus.FormattingEnabled = true;
            this.Rendelés_Ciklus.Location = new System.Drawing.Point(167, 133);
            this.Rendelés_Ciklus.Name = "Rendelés_Ciklus";
            this.Rendelés_Ciklus.Size = new System.Drawing.Size(121, 28);
            this.Rendelés_Ciklus.TabIndex = 239;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Silver;
            this.label15.Location = new System.Drawing.Point(12, 139);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(147, 20);
            this.label15.TabIndex = 240;
            this.label15.Text = "Karbantartási ciklus";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(12, 12);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(373, 38);
            this.Panel1.TabIndex = 260;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(173, 7);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(9, 10);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Ablak_Karbantartási_Rendelés
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 488);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Rendelés_Töröl);
            this.Controls.Add(this.Rendelés_Rendelés);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.Rendelés_Frissít);
            this.Controls.Add(this.Rendelés_Tábla);
            this.Controls.Add(this.Rendelés_Ok);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.Rendelés_Dátum);
            this.Controls.Add(this.Rendelés_Típus);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.Rendelés_Ciklus);
            this.Controls.Add(this.label15);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ablak_Karbantartási_Rendelés";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Karbantartási Rendelés számok";
            this.Load += new System.EventHandler(this.Ablak_Karbantartási_Rendelés_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Rendelés_Tábla)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Rendelés_Töröl;
        private System.Windows.Forms.TextBox Rendelés_Rendelés;
        private System.Windows.Forms.Label label17;
        internal System.Windows.Forms.Button Rendelés_Frissít;
        private System.Windows.Forms.DataGridView Rendelés_Tábla;
        internal System.Windows.Forms.Button Rendelés_Ok;
        internal System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker Rendelés_Dátum;
        private System.Windows.Forms.ComboBox Rendelés_Típus;
        internal System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox Rendelés_Ciklus;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.ComboBox Cmbtelephely;
        internal System.Windows.Forms.Label Label13;
    }
}