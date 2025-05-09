﻿namespace Villamos.V_Ablakok._4_Nyilvántartások.Eszterga_Karbantartás
{
    partial class Ablak_Eszterga_Karbantartás_Üzemóra
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
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Btn_Excel = new System.Windows.Forms.Button();
            this.Btn_ÚjFelvétel = new System.Windows.Forms.Button();
            this.TxtBxÜzem = new System.Windows.Forms.TextBox();
            this.Btn_Módosít = new System.Windows.Forms.Button();
            this.LblStátuszÜzem = new System.Windows.Forms.Label();
            this.ChckBxStátus = new System.Windows.Forms.CheckBox();
            this.LblÜzem = new System.Windows.Forms.Label();
            this.LblDátum = new System.Windows.Forms.Label();
            this.DtmPckrDátum = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.AllowUserToResizeRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.FilterAndSortEnabled = true;
            this.Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.Location = new System.Drawing.Point(24, 14);
            this.Tábla.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.RowHeadersWidth = 30;
            this.Tábla.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Tábla.Size = new System.Drawing.Size(558, 430);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 261;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            this.Tábla.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Tábla_CellFormatting);
            // 
            // Btn_Excel
            // 
            this.Btn_Excel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_Excel.BackColor = System.Drawing.Color.Transparent;
            this.Btn_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Excel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Excel.ForeColor = System.Drawing.Color.Transparent;
            this.Btn_Excel.Image = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Btn_Excel.Location = new System.Drawing.Point(798, 382);
            this.Btn_Excel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Excel.Name = "Btn_Excel";
            this.Btn_Excel.Size = new System.Drawing.Size(60, 62);
            this.Btn_Excel.TabIndex = 260;
            this.Btn_Excel.UseVisualStyleBackColor = false;
            this.Btn_Excel.Click += new System.EventHandler(this.Btn_Excel_Click);
            // 
            // Btn_ÚjFelvétel
            // 
            this.Btn_ÚjFelvétel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_ÚjFelvétel.BackColor = System.Drawing.Color.Transparent;
            this.Btn_ÚjFelvétel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_ÚjFelvétel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_ÚjFelvétel.ForeColor = System.Drawing.Color.Transparent;
            this.Btn_ÚjFelvétel.Image = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Btn_ÚjFelvétel.Location = new System.Drawing.Point(660, 382);
            this.Btn_ÚjFelvétel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_ÚjFelvétel.Name = "Btn_ÚjFelvétel";
            this.Btn_ÚjFelvétel.Size = new System.Drawing.Size(60, 62);
            this.Btn_ÚjFelvétel.TabIndex = 259;
            this.Btn_ÚjFelvétel.UseVisualStyleBackColor = false;
            this.Btn_ÚjFelvétel.Click += new System.EventHandler(this.Btn_ÚjFelvétel_Click);
            // 
            // TxtBxÜzem
            // 
            this.TxtBxÜzem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TxtBxÜzem.Location = new System.Drawing.Point(686, 212);
            this.TxtBxÜzem.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtBxÜzem.Name = "TxtBxÜzem";
            this.TxtBxÜzem.Size = new System.Drawing.Size(170, 26);
            this.TxtBxÜzem.TabIndex = 253;
            // 
            // Btn_Módosít
            // 
            this.Btn_Módosít.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Btn_Módosít.BackColor = System.Drawing.Color.Transparent;
            this.Btn_Módosít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Módosít.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Módosít.ForeColor = System.Drawing.Color.Transparent;
            this.Btn_Módosít.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Módosít.Location = new System.Drawing.Point(728, 382);
            this.Btn_Módosít.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Módosít.Name = "Btn_Módosít";
            this.Btn_Módosít.Size = new System.Drawing.Size(60, 62);
            this.Btn_Módosít.TabIndex = 252;
            this.Btn_Módosít.UseVisualStyleBackColor = false;
            this.Btn_Módosít.Click += new System.EventHandler(this.Btn_Módosít_Click);
            // 
            // LblStátuszÜzem
            // 
            this.LblStátuszÜzem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblStátuszÜzem.AutoSize = true;
            this.LblStátuszÜzem.Location = new System.Drawing.Point(590, 328);
            this.LblStátuszÜzem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblStátuszÜzem.Name = "LblStátuszÜzem";
            this.LblStátuszÜzem.Size = new System.Drawing.Size(68, 20);
            this.LblStátuszÜzem.TabIndex = 257;
            this.LblStátuszÜzem.Text = "Státusz:";
            // 
            // ChckBxStátus
            // 
            this.ChckBxStátus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ChckBxStátus.AutoSize = true;
            this.ChckBxStátus.Location = new System.Drawing.Point(686, 328);
            this.ChckBxStátus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChckBxStátus.Name = "ChckBxStátus";
            this.ChckBxStátus.Size = new System.Drawing.Size(79, 24);
            this.ChckBxStátus.TabIndex = 258;
            this.ChckBxStátus.Text = "Törölve";
            this.ChckBxStátus.UseVisualStyleBackColor = true;
            // 
            // LblÜzem
            // 
            this.LblÜzem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblÜzem.AutoSize = true;
            this.LblÜzem.Location = new System.Drawing.Point(590, 212);
            this.LblÜzem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblÜzem.Name = "LblÜzem";
            this.LblÜzem.Size = new System.Drawing.Size(78, 20);
            this.LblÜzem.TabIndex = 254;
            this.LblÜzem.Text = "Üzemóra:";
            // 
            // LblDátum
            // 
            this.LblDátum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LblDátum.AutoSize = true;
            this.LblDátum.Location = new System.Drawing.Point(590, 275);
            this.LblDátum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblDátum.Name = "LblDátum";
            this.LblDátum.Size = new System.Drawing.Size(61, 20);
            this.LblDátum.TabIndex = 255;
            this.LblDátum.Text = "Dátum:";
            // 
            // DtmPckrDátum
            // 
            this.DtmPckrDátum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DtmPckrDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtmPckrDátum.Location = new System.Drawing.Point(686, 269);
            this.DtmPckrDátum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DtmPckrDátum.Name = "DtmPckrDátum";
            this.DtmPckrDátum.Size = new System.Drawing.Size(170, 26);
            this.DtmPckrDátum.TabIndex = 256;
            // 
            // Ablak_Eszterga_Karbantartás_Üzemóra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(868, 458);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.Btn_Excel);
            this.Controls.Add(this.Btn_ÚjFelvétel);
            this.Controls.Add(this.TxtBxÜzem);
            this.Controls.Add(this.Btn_Módosít);
            this.Controls.Add(this.LblStátuszÜzem);
            this.Controls.Add(this.ChckBxStátus);
            this.Controls.Add(this.LblÜzem);
            this.Controls.Add(this.LblDátum);
            this.Controls.Add(this.DtmPckrDátum);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimizeBox = false;
            this.Name = "Ablak_Eszterga_Karbantartás_Üzemóra";
            this.Text = "Ablak_Eszterga_Karbantartás_Üzemóra";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Eszterga_Karbantartás_Üzemóra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Zuby.ADGV.AdvancedDataGridView Tábla;
        internal System.Windows.Forms.Button Btn_Excel;
        internal System.Windows.Forms.Button Btn_ÚjFelvétel;
        internal System.Windows.Forms.TextBox TxtBxÜzem;
        internal System.Windows.Forms.Button Btn_Módosít;
        internal System.Windows.Forms.Label LblStátuszÜzem;
        internal System.Windows.Forms.CheckBox ChckBxStátus;
        internal System.Windows.Forms.Label LblÜzem;
        internal System.Windows.Forms.Label LblDátum;
        internal System.Windows.Forms.DateTimePicker DtmPckrDátum;
    }
}