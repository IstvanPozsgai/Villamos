namespace Villamos.V_Ablakok._4_Nyilvántartások.Vételezés
{
    partial class Ablak_Elfekvő
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Btn_AdatFeldolgozás = new System.Windows.Forms.Button();
            this.Btn_Frissit = new System.Windows.Forms.Button();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Btn_ExcelExport = new System.Windows.Forms.Button();
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 16;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel1.Controls.Add(this.Btn_AdatFeldolgozás, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.BtnSúgó, 15, 0);
            this.tableLayoutPanel1.Controls.Add(this.Btn_Frissit, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Btn_ExcelExport, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1176, 55);
            this.tableLayoutPanel1.TabIndex = 199;
            // 
            // Btn_AdatFeldolgozás
            // 
            this.Btn_AdatFeldolgozás.BackgroundImage = global::Villamos.Properties.Resources.Button_Upload_01;
            this.Btn_AdatFeldolgozás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_AdatFeldolgozás.Location = new System.Drawing.Point(3, 3);
            this.Btn_AdatFeldolgozás.Name = "Btn_AdatFeldolgozás";
            this.Btn_AdatFeldolgozás.Size = new System.Drawing.Size(44, 45);
            this.Btn_AdatFeldolgozás.TabIndex = 195;
            this.toolTip1.SetToolTip(this.Btn_AdatFeldolgozás, "Adatok feltöltése");
            this.Btn_AdatFeldolgozás.UseVisualStyleBackColor = true;
            this.Btn_AdatFeldolgozás.Click += new System.EventHandler(this.Btn_AdatFeldolgozás_Click);
            // 
            // Btn_Frissit
            // 
            this.Btn_Frissit.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Btn_Frissit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Frissit.Location = new System.Drawing.Point(53, 3);
            this.Btn_Frissit.Name = "Btn_Frissit";
            this.Btn_Frissit.Size = new System.Drawing.Size(44, 45);
            this.Btn_Frissit.TabIndex = 203;
            this.toolTip1.SetToolTip(this.Btn_Frissit, "Táblázat frissítése");
            this.Btn_Frissit.UseVisualStyleBackColor = true;
            this.Btn_Frissit.Click += new System.EventHandler(this.Btn_Frissit_Click);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1128, 3);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 188;
            this.toolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            // 
            // Btn_ExcelExport
            // 
            this.Btn_ExcelExport.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Btn_ExcelExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_ExcelExport.Location = new System.Drawing.Point(103, 2);
            this.Btn_ExcelExport.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_ExcelExport.Name = "Btn_ExcelExport";
            this.Btn_ExcelExport.Size = new System.Drawing.Size(43, 45);
            this.Btn_ExcelExport.TabIndex = 204;
            this.toolTip1.SetToolTip(this.Btn_ExcelExport, "Excel kimenetet készít a Felső \r\ntáblázat adatai alapján");
            this.Btn_ExcelExport.UseVisualStyleBackColor = true;
            this.Btn_ExcelExport.Click += new System.EventHandler(this.Btn_ExcelExport_Click);
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
            this.Tábla.Location = new System.Drawing.Point(12, 73);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.Size = new System.Drawing.Size(1176, 607);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 201;
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(12, 112);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(1176, 28);
            this.Holtart.TabIndex = 202;
            this.Holtart.Visible = false;
            // 
            // Ablak_Elfekvő
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSalmon;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Elfekvő";
            this.Text = "Elfekvő készlet";
            this.Load += new System.EventHandler(this.Ablak_Elfekvő_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        internal System.Windows.Forms.Button Btn_AdatFeldolgozás;
        internal System.Windows.Forms.Button BtnSúgó;
        internal System.Windows.Forms.Button Btn_Frissit;
        private Zuby.ADGV.AdvancedDataGridView Tábla;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal System.Windows.Forms.Button Btn_ExcelExport;
    }
}