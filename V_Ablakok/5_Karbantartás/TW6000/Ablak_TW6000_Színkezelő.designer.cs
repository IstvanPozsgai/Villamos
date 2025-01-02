namespace Villamos.Villamos_Ablakok.TW6000
{
    partial class Ablak_TW6000_Színkezelő
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_TW6000_Színkezelő));
            this.Szín_Tábla = new System.Windows.Forms.DataGridView();
            this.Színe = new System.Windows.Forms.TextBox();
            this.Vonal = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Karb_rögzít = new System.Windows.Forms.Button();
            this.SzínPaletta = new System.Windows.Forms.Button();
            this.Szín_tábla_lista = new System.Windows.Forms.Button();
            this.Karb_töröl = new System.Windows.Forms.Button();
            this.Karb_új = new System.Windows.Forms.Button();
            this.ColorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.Szín_Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Szín_Tábla
            // 
            this.Szín_Tábla.AllowUserToAddRows = false;
            this.Szín_Tábla.AllowUserToDeleteRows = false;
            this.Szín_Tábla.AllowUserToResizeColumns = false;
            this.Szín_Tábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Szín_Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Szín_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Szín_Tábla.DefaultCellStyle = dataGridViewCellStyle2;
            this.Szín_Tábla.Location = new System.Drawing.Point(12, 144);
            this.Szín_Tábla.Name = "Szín_Tábla";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Szín_Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Szín_Tábla.RowHeadersVisible = false;
            this.Szín_Tábla.RowHeadersWidth = 51;
            this.Szín_Tábla.Size = new System.Drawing.Size(331, 171);
            this.Szín_Tábla.TabIndex = 228;
            this.Szín_Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Szín_Tábla_CellClick);
            // 
            // Színe
            // 
            this.Színe.Enabled = false;
            this.Színe.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Színe.Location = new System.Drawing.Point(127, 97);
            this.Színe.Name = "Színe";
            this.Színe.Size = new System.Drawing.Size(100, 26);
            this.Színe.TabIndex = 1;
            // 
            // Vonal
            // 
            this.Vonal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Vonal.Location = new System.Drawing.Point(12, 97);
            this.Vonal.Name = "Vonal";
            this.Vonal.Size = new System.Drawing.Size(100, 26);
            this.Vonal.TabIndex = 0;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label3.Location = new System.Drawing.Point(125, 70);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(44, 20);
            this.Label3.TabIndex = 221;
            this.Label3.Text = "Szín:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label2.Location = new System.Drawing.Point(8, 70);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(74, 20);
            this.Label2.TabIndex = 220;
            this.Label2.Text = "Vizsgálat";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Olive;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label1.Location = new System.Drawing.Point(8, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(86, 20);
            this.Label1.TabIndex = 87;
            this.Label1.Text = "Színkezelő";
            // 
            // Karb_rögzít
            // 
            this.Karb_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Karb_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Karb_rögzít.Location = new System.Drawing.Point(303, 86);
            this.Karb_rögzít.Name = "Karb_rögzít";
            this.Karb_rögzít.Size = new System.Drawing.Size(40, 40);
            this.Karb_rögzít.TabIndex = 2;
            this.Karb_rögzít.UseVisualStyleBackColor = true;
            this.Karb_rögzít.Click += new System.EventHandler(this.Karb_rögzít_Click);
            // 
            // SzínPaletta
            // 
            this.SzínPaletta.BackgroundImage = global::Villamos.Properties.Resources.Dtafalonso_Modern_Xp_ModernXP_12_Workstation_Desktop_Colors;
            this.SzínPaletta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SzínPaletta.Location = new System.Drawing.Point(257, 86);
            this.SzínPaletta.Name = "SzínPaletta";
            this.SzínPaletta.Size = new System.Drawing.Size(40, 40);
            this.SzínPaletta.TabIndex = 3;
            this.SzínPaletta.UseVisualStyleBackColor = true;
            this.SzínPaletta.Click += new System.EventHandler(this.SzínPaletta_Click);
            // 
            // Szín_tábla_lista
            // 
            this.Szín_tábla_lista.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Szín_tábla_lista.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Szín_tábla_lista.Location = new System.Drawing.Point(129, 12);
            this.Szín_tábla_lista.Name = "Szín_tábla_lista";
            this.Szín_tábla_lista.Size = new System.Drawing.Size(40, 40);
            this.Szín_tábla_lista.TabIndex = 4;
            this.Szín_tábla_lista.UseVisualStyleBackColor = true;
            this.Szín_tábla_lista.Click += new System.EventHandler(this.Szín_tábla_lista_Click);
            // 
            // Karb_töröl
            // 
            this.Karb_töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Karb_töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Karb_töröl.Location = new System.Drawing.Point(221, 12);
            this.Karb_töröl.Name = "Karb_töröl";
            this.Karb_töröl.Size = new System.Drawing.Size(40, 40);
            this.Karb_töröl.TabIndex = 6;
            this.Karb_töröl.UseVisualStyleBackColor = true;
            this.Karb_töröl.Click += new System.EventHandler(this.Karb_töröl_Click);
            // 
            // Karb_új
            // 
            this.Karb_új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Karb_új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Karb_új.Location = new System.Drawing.Point(175, 12);
            this.Karb_új.Name = "Karb_új";
            this.Karb_új.Size = new System.Drawing.Size(40, 40);
            this.Karb_új.TabIndex = 5;
            this.Karb_új.UseVisualStyleBackColor = true;
            this.Karb_új.Click += new System.EventHandler(this.Karb_új_Click);
            // 
            // Ablak_TW6000_Színkezelő
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(360, 331);
            this.Controls.Add(this.Szín_Tábla);
            this.Controls.Add(this.Karb_rögzít);
            this.Controls.Add(this.SzínPaletta);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Szín_tábla_lista);
            this.Controls.Add(this.Színe);
            this.Controls.Add(this.Karb_töröl);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Vonal);
            this.Controls.Add(this.Karb_új);
            this.Controls.Add(this.Label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Ablak_TW6000_Színkezelő";
            this.Text = "TW6000 Színkezelő";
            this.Load += new System.EventHandler(this.Ablak_TW6000_Színkezelő_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Szín_Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.DataGridView Szín_Tábla;
        internal System.Windows.Forms.Button SzínPaletta;
        internal System.Windows.Forms.Button Karb_rögzít;
        internal System.Windows.Forms.Button Karb_új;
        internal System.Windows.Forms.Button Karb_töröl;
        internal System.Windows.Forms.TextBox Színe;
        internal System.Windows.Forms.TextBox Vonal;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Button Szín_tábla_lista;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ColorDialog ColorDialog1;
    }
}