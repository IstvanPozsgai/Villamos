namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    partial class Ablak_CAF_Eszterga_Beállítás
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_CAF_Eszterga_Beállítás));
            this.Alap_pályaszám = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Alap_rögzít = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Idő_Lépés = new System.Windows.Forms.TextBox();
            this.KM_alapú = new System.Windows.Forms.RadioButton();
            this.Idő_alapú = new System.Windows.Forms.RadioButton();
            this.Km_Lépés = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Alap_pályaszám
            // 
            this.Alap_pályaszám.DropDownHeight = 300;
            this.Alap_pályaszám.FormattingEnabled = true;
            this.Alap_pályaszám.IntegralHeight = false;
            this.Alap_pályaszám.Location = new System.Drawing.Point(210, 3);
            this.Alap_pályaszám.Name = "Alap_pályaszám";
            this.Alap_pályaszám.Size = new System.Drawing.Size(121, 28);
            this.Alap_pályaszám.TabIndex = 75;
            this.Alap_pályaszám.SelectedIndexChanged += new System.EventHandler(this.Alap_pályaszám_SelectedIndexChanged);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(3, 0);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(89, 20);
            this.Label1.TabIndex = 74;
            this.Label1.Text = "Pályaszám:";
            // 
            // Alap_rögzít
            // 
            this.Alap_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Alap_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_rögzít.Location = new System.Drawing.Point(362, 12);
            this.Alap_rögzít.Name = "Alap_rögzít";
            this.Alap_rögzít.Size = new System.Drawing.Size(50, 50);
            this.Alap_rögzít.TabIndex = 107;
            this.toolTip1.SetToolTip(this.Alap_rögzít, "Rögzíti a beállításokat");
            this.Alap_rögzít.UseVisualStyleBackColor = true;
            this.Alap_rögzít.Click += new System.EventHandler(this.Alap_rögzít_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 20);
            this.label3.TabIndex = 109;
            this.label3.Text = "Futott Km alapú ütemezés:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(188, 20);
            this.label4.TabIndex = 110;
            this.label4.Text = "Időalapú ütemezés [nap]:";
            // 
            // Idő_Lépés
            // 
            this.Idő_Lépés.Location = new System.Drawing.Point(210, 83);
            this.Idő_Lépés.Name = "Idő_Lépés";
            this.Idő_Lépés.Size = new System.Drawing.Size(121, 26);
            this.Idő_Lépés.TabIndex = 111;
            // 
            // KM_alapú
            // 
            this.KM_alapú.AutoSize = true;
            this.KM_alapú.Checked = true;
            this.KM_alapú.Location = new System.Drawing.Point(3, 43);
            this.KM_alapú.Name = "KM_alapú";
            this.KM_alapú.Size = new System.Drawing.Size(93, 24);
            this.KM_alapú.TabIndex = 112;
            this.KM_alapú.TabStop = true;
            this.KM_alapú.Text = "Km alapú";
            this.KM_alapú.UseVisualStyleBackColor = true;
            this.KM_alapú.CheckedChanged += new System.EventHandler(this.KM_alapú_CheckedChanged);
            // 
            // Idő_alapú
            // 
            this.Idő_alapú.AutoSize = true;
            this.Idő_alapú.Location = new System.Drawing.Point(210, 43);
            this.Idő_alapú.Name = "Idő_alapú";
            this.Idő_alapú.Size = new System.Drawing.Size(93, 24);
            this.Idő_alapú.TabIndex = 113;
            this.Idő_alapú.Text = "Idő alapú";
            this.Idő_alapú.UseVisualStyleBackColor = true;
            // 
            // Km_Lépés
            // 
            this.Km_Lépés.Location = new System.Drawing.Point(210, 123);
            this.Km_Lépés.Name = "Km_Lépés";
            this.Km_Lépés.Size = new System.Drawing.Size(121, 26);
            this.Km_Lépés.TabIndex = 114;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 207F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.Label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Km_Lépés, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.KM_alapú, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Idő_Lépés, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.Idő_alapú, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.Alap_pályaszám, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.Dátum, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(344, 198);
            this.tableLayoutPanel1.TabIndex = 115;
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(210, 163);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(121, 26);
            this.Dátum.TabIndex = 115;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(147, 20);
            this.label2.TabIndex = 116;
            this.label2.Text = "Ütemezés Dátuma:";
            // 
            // Ablak_CAF_Eszterga_Beállítás
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Peru;
            this.ClientSize = new System.Drawing.Size(420, 218);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Alap_rögzít);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Ablak_CAF_Eszterga_Beállítás";
            this.Text = "CAF esztergálási alapadatok";
            this.Load += new System.EventHandler(this.Ablak_CAF_Eszterga_Beállítás_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_CAF_Eszterga_Beállítás_KeyDown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ComboBox Alap_pályaszám;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.Button Alap_rögzít;
        private System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Idő_Lépés;
        private System.Windows.Forms.RadioButton KM_alapú;
        private System.Windows.Forms.RadioButton Idő_alapú;
        private System.Windows.Forms.TextBox Km_Lépés;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DateTimePicker Dátum;
        internal System.Windows.Forms.Label label2;
    }
}