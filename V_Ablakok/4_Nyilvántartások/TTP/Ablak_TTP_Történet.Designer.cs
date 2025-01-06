namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.TTP
{
    partial class Ablak_TTP_Történet
    {

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
        internal void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.ChkTTPJavítás = new System.Windows.Forms.CheckBox();
            this.DtLejárat = new System.Windows.Forms.DateTimePicker();
            this.CmbStátus = new System.Windows.Forms.ComboBox();
            this.TxtEgyütt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnPDFFel = new System.Windows.Forms.Button();
            this.PDFNéz = new System.Windows.Forms.Button();
            this.BtnFrissít = new System.Windows.Forms.Button();
            this.Btn_TTP_Rögz = new System.Windows.Forms.Button();
            this.BtnExcel = new System.Windows.Forms.Button();
            this.CmbAzonosító = new System.Windows.Forms.ComboBox();
            this.DtJavBefDát = new System.Windows.Forms.DateTimePicker();
            this.DtÜtemezés = new System.Windows.Forms.DateTimePicker();
            this.DtTTPDátum = new System.Windows.Forms.DateTimePicker();
            this.TxtRendelés = new System.Windows.Forms.TextBox();
            this.TxtMegjegyzés = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.FilterAndSortEnabled = true;
            this.Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.Location = new System.Drawing.Point(12, 364);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.Size = new System.Drawing.Size(1011, 316);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 0;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(3, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Azonosító:";
            // 
            // ChkTTPJavítás
            // 
            this.ChkTTPJavítás.AutoSize = true;
            this.ChkTTPJavítás.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ChkTTPJavítás.Location = new System.Drawing.Point(209, 163);
            this.ChkTTPJavítás.Name = "ChkTTPJavítás";
            this.ChkTTPJavítás.Size = new System.Drawing.Size(187, 24);
            this.ChkTTPJavítás.TabIndex = 2;
            this.ChkTTPJavítás.Text = "Javítás szükséges";
            this.ChkTTPJavítás.UseVisualStyleBackColor = true;
            // 
            // DtLejárat
            // 
            this.DtLejárat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DtLejárat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtLejárat.Location = new System.Drawing.Point(209, 56);
            this.DtLejárat.Name = "DtLejárat";
            this.DtLejárat.Size = new System.Drawing.Size(187, 26);
            this.DtLejárat.TabIndex = 3;
            // 
            // CmbStátus
            // 
            this.CmbStátus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CmbStátus.FormattingEnabled = true;
            this.CmbStátus.Location = new System.Drawing.Point(209, 244);
            this.CmbStátus.Name = "CmbStátus";
            this.CmbStátus.Size = new System.Drawing.Size(187, 28);
            this.CmbStátus.TabIndex = 4;
            // 
            // TxtEgyütt
            // 
            this.TxtEgyütt.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TxtEgyütt.Location = new System.Drawing.Point(636, 91);
            this.TxtEgyütt.Name = "TxtEgyütt";
            this.TxtEgyütt.Size = new System.Drawing.Size(316, 26);
            this.TxtEgyütt.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Location = new System.Drawing.Point(3, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Lejárat dátuma:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(3, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Ütemezés dátuma:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label4.Location = new System.Drawing.Point(3, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "TTP elvégzésének dátuma:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label5.Location = new System.Drawing.Point(3, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(200, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Rendelés szám:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(402, 205);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(228, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Javítás befejezésének dátuma:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label7.Location = new System.Drawing.Point(402, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(228, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "Szerelvényben vizsgál";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label8.Location = new System.Drawing.Point(3, 248);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(200, 20);
            this.label8.TabIndex = 12;
            this.label8.Text = "Státus";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 286);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 20);
            this.label9.TabIndex = 13;
            this.label9.Text = "Megjegyzés:";
            // 
            // BtnPDFFel
            // 
            this.BtnPDFFel.BackgroundImage = global::Villamos.Properties.Resources.pdf_32;
            this.BtnPDFFel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnPDFFel.Location = new System.Drawing.Point(1030, 235);
            this.BtnPDFFel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnPDFFel.Name = "BtnPDFFel";
            this.BtnPDFFel.Size = new System.Drawing.Size(45, 45);
            this.BtnPDFFel.TabIndex = 76;
            this.toolTip1.SetToolTip(this.BtnPDFFel, "Pdf feltöltése");
            this.BtnPDFFel.UseVisualStyleBackColor = true;
            this.BtnPDFFel.Click += new System.EventHandler(this.BtnPDFFel_Click);
            // 
            // PDFNéz
            // 
            this.PDFNéz.BackgroundImage = global::Villamos.Properties.Resources.App_dict;
            this.PDFNéz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PDFNéz.Location = new System.Drawing.Point(637, 5);
            this.PDFNéz.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PDFNéz.Name = "PDFNéz";
            this.PDFNéz.Size = new System.Drawing.Size(45, 40);
            this.PDFNéz.TabIndex = 77;
            this.toolTip1.SetToolTip(this.PDFNéz, "Pdf megjelenítés");
            this.PDFNéz.UseVisualStyleBackColor = true;
            this.PDFNéz.Visible = false;
            this.PDFNéz.Click += new System.EventHandler(this.PDFNéz_Click);
            // 
            // BtnFrissít
            // 
            this.BtnFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnFrissít.Location = new System.Drawing.Point(403, 5);
            this.BtnFrissít.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnFrissít.Name = "BtnFrissít";
            this.BtnFrissít.Size = new System.Drawing.Size(45, 40);
            this.BtnFrissít.TabIndex = 76;
            this.toolTip1.SetToolTip(this.BtnFrissít, "Frissíti a táblázat adatait");
            this.BtnFrissít.UseVisualStyleBackColor = true;
            this.BtnFrissít.Click += new System.EventHandler(this.BtnFrissít_Click);
            // 
            // Btn_TTP_Rögz
            // 
            this.Btn_TTP_Rögz.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_TTP_Rögz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Btn_TTP_Rögz.Location = new System.Drawing.Point(1030, 311);
            this.Btn_TTP_Rögz.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_TTP_Rögz.Name = "Btn_TTP_Rögz";
            this.Btn_TTP_Rögz.Size = new System.Drawing.Size(45, 45);
            this.Btn_TTP_Rögz.TabIndex = 74;
            this.toolTip1.SetToolTip(this.Btn_TTP_Rögz, "Rögzíti/Módosítja az adatokat.");
            this.Btn_TTP_Rögz.UseVisualStyleBackColor = true;
            this.Btn_TTP_Rögz.Click += new System.EventHandler(this.Btn_TTP_Rögz_Click);
            // 
            // BtnExcel
            // 
            this.BtnExcel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.BtnExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnExcel.Location = new System.Drawing.Point(958, 3);
            this.BtnExcel.Name = "BtnExcel";
            this.BtnExcel.Size = new System.Drawing.Size(45, 44);
            this.BtnExcel.TabIndex = 192;
            this.toolTip1.SetToolTip(this.BtnExcel, "Adatbázis adatit exportálja Excelbe");
            this.BtnExcel.UseVisualStyleBackColor = true;
            this.BtnExcel.Click += new System.EventHandler(this.BtnExcel_Click);
            // 
            // CmbAzonosító
            // 
            this.CmbAzonosító.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.CmbAzonosító.FormattingEnabled = true;
            this.CmbAzonosító.Location = new System.Drawing.Point(209, 26);
            this.CmbAzonosító.Name = "CmbAzonosító";
            this.CmbAzonosító.Size = new System.Drawing.Size(187, 28);
            this.CmbAzonosító.TabIndex = 14;
            this.CmbAzonosító.SelectedIndexChanged += new System.EventHandler(this.CmbAzonosító_SelectedIndexChanged);
            // 
            // DtJavBefDát
            // 
            this.DtJavBefDát.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtJavBefDát.Location = new System.Drawing.Point(636, 193);
            this.DtJavBefDát.Name = "DtJavBefDát";
            this.DtJavBefDát.Size = new System.Drawing.Size(111, 26);
            this.DtJavBefDát.TabIndex = 15;
            // 
            // DtÜtemezés
            // 
            this.DtÜtemezés.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DtÜtemezés.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtÜtemezés.Location = new System.Drawing.Point(209, 91);
            this.DtÜtemezés.Name = "DtÜtemezés";
            this.DtÜtemezés.Size = new System.Drawing.Size(187, 26);
            this.DtÜtemezés.TabIndex = 16;
            // 
            // DtTTPDátum
            // 
            this.DtTTPDátum.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.DtTTPDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtTTPDátum.Location = new System.Drawing.Point(209, 126);
            this.DtTTPDátum.Name = "DtTTPDátum";
            this.DtTTPDátum.Size = new System.Drawing.Size(187, 26);
            this.DtTTPDátum.TabIndex = 18;
            // 
            // TxtRendelés
            // 
            this.TxtRendelés.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TxtRendelés.Location = new System.Drawing.Point(209, 196);
            this.TxtRendelés.Name = "TxtRendelés";
            this.TxtRendelés.Size = new System.Drawing.Size(187, 26);
            this.TxtRendelés.TabIndex = 19;
            // 
            // TxtMegjegyzés
            // 
            this.TxtMegjegyzés.Location = new System.Drawing.Point(221, 286);
            this.TxtMegjegyzés.MaxLength = 255;
            this.TxtMegjegyzés.Multiline = true;
            this.TxtMegjegyzés.Name = "TxtMegjegyzés";
            this.TxtMegjegyzés.Size = new System.Drawing.Size(802, 70);
            this.TxtMegjegyzés.TabIndex = 20;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 215F));
            this.tableLayoutPanel1.Controls.Add(this.PDFNéz, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.BtnFrissít, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.CmbStátus, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.CmbAzonosító, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.DtÜtemezés, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.TxtRendelés, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.DtLejárat, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.TxtEgyütt, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.DtJavBefDát, 3, 5);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.DtTTPDátum, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.ChkTTPJavítás, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.BtnExcel, 4, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1011, 268);
            this.tableLayoutPanel1.TabIndex = 75;
            // 
            // Ablak_TTP_Történet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1088, 692);
            this.Controls.Add(this.BtnPDFFel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Btn_TTP_Rögz);
            this.Controls.Add(this.TxtMegjegyzés);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.Tábla);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_TTP_Történet";
            this.Text = "TTP jármű adatok";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_TTP_Történet_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_TTP_Történet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Zuby.ADGV.AdvancedDataGridView Tábla;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.CheckBox ChkTTPJavítás;
        internal System.Windows.Forms.DateTimePicker DtLejárat;
        internal System.Windows.Forms.ComboBox CmbStátus;
        internal System.Windows.Forms.TextBox TxtEgyütt;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.ComboBox CmbAzonosító;
        internal System.Windows.Forms.DateTimePicker DtJavBefDát;
        internal System.Windows.Forms.DateTimePicker DtÜtemezés;
        internal System.Windows.Forms.DateTimePicker DtTTPDátum;
        internal System.Windows.Forms.TextBox TxtRendelés;
        internal System.Windows.Forms.TextBox TxtMegjegyzés;
        internal System.Windows.Forms.Button Btn_TTP_Rögz;
        internal System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal System.Windows.Forms.Button BtnFrissít;
        internal System.Windows.Forms.Button BtnPDFFel;
        internal System.Windows.Forms.Button PDFNéz;
        internal System.Windows.Forms.Button BtnExcel;
        private System.ComponentModel.IContainer components;
    }
}