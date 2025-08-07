namespace Villamos.V_Ablakok._4_Nyilvántartások.Eszterga_Karbantartás
{
    partial class Ablak_Eszterga_Karbantartás_Napló
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Eszterga_Karbantartás_Napló));
            this.TxtBxMegjegyzes = new System.Windows.Forms.TextBox();
            this.LblMegjegyzes = new System.Windows.Forms.Label();
            this.LblAznapiUzemora = new System.Windows.Forms.Label();
            this.TxtBxUtolagUzemora = new System.Windows.Forms.TextBox();
            this.TablaMuvelet = new Zuby.ADGV.AdvancedDataGridView();
            this.TablaNaplo = new Zuby.ADGV.AdvancedDataGridView();
            this.Btn_Modosit = new System.Windows.Forms.Button();
            this.LblUtolagMuvelet = new System.Windows.Forms.Label();
            this.LblUtolagNaplozasTabla = new System.Windows.Forms.Label();
            this.lblUzemora = new System.Windows.Forms.Label();
            this.txtBxUzemora = new System.Windows.Forms.TextBox();
            this.lblDatum = new System.Windows.Forms.Label();
            this.DtmPckr = new System.Windows.Forms.DateTimePicker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.TablaMuvelet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaNaplo)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtBxMegjegyzes
            // 
            this.TxtBxMegjegyzes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtBxMegjegyzes.Location = new System.Drawing.Point(732, 104);
            this.TxtBxMegjegyzes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtBxMegjegyzes.Multiline = true;
            this.TxtBxMegjegyzes.Name = "TxtBxMegjegyzes";
            this.TxtBxMegjegyzes.Size = new System.Drawing.Size(410, 170);
            this.TxtBxMegjegyzes.TabIndex = 53;
            // 
            // LblMegjegyzes
            // 
            this.LblMegjegyzes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblMegjegyzes.AutoSize = true;
            this.LblMegjegyzes.Location = new System.Drawing.Point(728, 79);
            this.LblMegjegyzes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblMegjegyzes.Name = "LblMegjegyzes";
            this.LblMegjegyzes.Size = new System.Drawing.Size(97, 20);
            this.LblMegjegyzes.TabIndex = 52;
            this.LblMegjegyzes.Text = "Megjegyzés:";
            // 
            // LblAznapiUzemora
            // 
            this.LblAznapiUzemora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LblAznapiUzemora.AutoSize = true;
            this.LblAznapiUzemora.Location = new System.Drawing.Point(1215, 337);
            this.LblAznapiUzemora.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.LblAznapiUzemora.Name = "LblAznapiUzemora";
            this.LblAznapiUzemora.Size = new System.Drawing.Size(164, 20);
            this.LblAznapiUzemora.TabIndex = 49;
            this.LblAznapiUzemora.Text = "Aznapi üzemóra állás:";
            // 
            // TxtBxUtolagUzemora
            // 
            this.TxtBxUtolagUzemora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtBxUtolagUzemora.Location = new System.Drawing.Point(1218, 372);
            this.TxtBxUtolagUzemora.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtBxUtolagUzemora.Name = "TxtBxUtolagUzemora";
            this.TxtBxUtolagUzemora.Size = new System.Drawing.Size(266, 26);
            this.TxtBxUtolagUzemora.TabIndex = 48;
            // 
            // TablaMuvelet
            // 
            this.TablaMuvelet.AllowUserToAddRows = false;
            this.TablaMuvelet.AllowUserToDeleteRows = false;
            this.TablaMuvelet.AllowUserToResizeRows = false;
            this.TablaMuvelet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TablaMuvelet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaMuvelet.FilterAndSortEnabled = true;
            this.TablaMuvelet.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.TablaMuvelet.Location = new System.Drawing.Point(16, 44);
            this.TablaMuvelet.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.TablaMuvelet.MaxFilterButtonImageHeight = 23;
            this.TablaMuvelet.Name = "TablaMuvelet";
            this.TablaMuvelet.ReadOnly = true;
            this.TablaMuvelet.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TablaMuvelet.RowHeadersVisible = false;
            this.TablaMuvelet.RowHeadersWidth = 62;
            this.TablaMuvelet.RowTemplate.Height = 28;
            this.TablaMuvelet.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TablaMuvelet.Size = new System.Drawing.Size(700, 314);
            this.TablaMuvelet.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.TablaMuvelet.TabIndex = 47;
            this.TablaMuvelet.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.TablaMuvelet_DataBindingComplete);
            this.TablaMuvelet.SelectionChanged += new System.EventHandler(this.TablaMuvelet_SelectionChanged);
            // 
            // TablaNaplo
            // 
            this.TablaNaplo.AllowUserToAddRows = false;
            this.TablaNaplo.AllowUserToDeleteRows = false;
            this.TablaNaplo.AllowUserToResizeRows = false;
            this.TablaNaplo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TablaNaplo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaNaplo.FilterAndSortEnabled = true;
            this.TablaNaplo.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.TablaNaplo.Location = new System.Drawing.Point(16, 399);
            this.TablaNaplo.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.TablaNaplo.MaxFilterButtonImageHeight = 23;
            this.TablaNaplo.Name = "TablaNaplo";
            this.TablaNaplo.ReadOnly = true;
            this.TablaNaplo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TablaNaplo.RowHeadersVisible = false;
            this.TablaNaplo.RowHeadersWidth = 62;
            this.TablaNaplo.RowTemplate.Height = 28;
            this.TablaNaplo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TablaNaplo.Size = new System.Drawing.Size(700, 326);
            this.TablaNaplo.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.TablaNaplo.TabIndex = 29;
            this.TablaNaplo.SelectionChanged += new System.EventHandler(this.TablaNaplo_SelectionChanged);
            // 
            // Btn_Modosit
            // 
            this.Btn_Modosit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Modosit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Modosit.FlatAppearance.BorderColor = System.Drawing.Color.Wheat;
            this.Btn_Modosit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Btn_Modosit.Image = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Modosit.Location = new System.Drawing.Point(1082, 663);
            this.Btn_Modosit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_Modosit.Name = "Btn_Modosit";
            this.Btn_Modosit.Size = new System.Drawing.Size(60, 62);
            this.Btn_Modosit.TabIndex = 46;
            this.toolTip1.SetToolTip(this.Btn_Modosit, "Napló rögzítése");
            this.Btn_Modosit.UseVisualStyleBackColor = true;
            this.Btn_Modosit.Click += new System.EventHandler(this.Btn_Modosit_Click);
            // 
            // LblUtolagMuvelet
            // 
            this.LblUtolagMuvelet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblUtolagMuvelet.AutoSize = true;
            this.LblUtolagMuvelet.Location = new System.Drawing.Point(12, 18);
            this.LblUtolagMuvelet.Name = "LblUtolagMuvelet";
            this.LblUtolagMuvelet.Size = new System.Drawing.Size(107, 20);
            this.LblUtolagMuvelet.TabIndex = 55;
            this.LblUtolagMuvelet.Text = "Művelet tábla:";
            // 
            // LblUtolagNaplozasTabla
            // 
            this.LblUtolagNaplozasTabla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LblUtolagNaplozasTabla.AutoSize = true;
            this.LblUtolagNaplozasTabla.Location = new System.Drawing.Point(12, 373);
            this.LblUtolagNaplozasTabla.Name = "LblUtolagNaplozasTabla";
            this.LblUtolagNaplozasTabla.Size = new System.Drawing.Size(121, 20);
            this.LblUtolagNaplozasTabla.TabIndex = 54;
            this.LblUtolagNaplozasTabla.Text = "Naplózási tábla:";
            // 
            // lblUzemora
            // 
            this.lblUzemora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUzemora.AutoSize = true;
            this.lblUzemora.Location = new System.Drawing.Point(959, 18);
            this.lblUzemora.Name = "lblUzemora";
            this.lblUzemora.Size = new System.Drawing.Size(164, 20);
            this.lblUzemora.TabIndex = 57;
            this.lblUzemora.Text = "Aznapi üzemóra állás:";
            // 
            // txtBxUzemora
            // 
            this.txtBxUzemora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBxUzemora.Enabled = false;
            this.txtBxUzemora.Location = new System.Drawing.Point(963, 44);
            this.txtBxUzemora.Name = "txtBxUzemora";
            this.txtBxUzemora.Size = new System.Drawing.Size(179, 26);
            this.txtBxUzemora.TabIndex = 56;
            // 
            // lblDatum
            // 
            this.lblDatum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDatum.AutoSize = true;
            this.lblDatum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblDatum.Location = new System.Drawing.Point(728, 18);
            this.lblDatum.Name = "lblDatum";
            this.lblDatum.Size = new System.Drawing.Size(61, 20);
            this.lblDatum.TabIndex = 59;
            this.lblDatum.Text = "Dátum:";
            // 
            // DtmPckr
            // 
            this.DtmPckr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DtmPckr.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DtmPckr.Location = new System.Drawing.Point(732, 41);
            this.DtmPckr.Name = "DtmPckr";
            this.DtmPckr.Size = new System.Drawing.Size(107, 26);
            this.DtmPckr.TabIndex = 58;
            this.DtmPckr.ValueChanged += new System.EventHandler(this.DtmPckr_ValueChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Ablak_Eszterga_Karbantartás_Napló
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Wheat;
            this.ClientSize = new System.Drawing.Size(1155, 736);
            this.Controls.Add(this.lblDatum);
            this.Controls.Add(this.DtmPckr);
            this.Controls.Add(this.lblUzemora);
            this.Controls.Add(this.txtBxUzemora);
            this.Controls.Add(this.LblUtolagMuvelet);
            this.Controls.Add(this.LblUtolagNaplozasTabla);
            this.Controls.Add(this.Btn_Modosit);
            this.Controls.Add(this.LblMegjegyzes);
            this.Controls.Add(this.TxtBxMegjegyzes);
            this.Controls.Add(this.LblAznapiUzemora);
            this.Controls.Add(this.TablaNaplo);
            this.Controls.Add(this.TxtBxUtolagUzemora);
            this.Controls.Add(this.TablaMuvelet);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Eszterga_Karbantartás_Napló";
            this.Text = "Ablak_Eszterga_Karbantartás_Napló";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Eszterga_Karbantartás_Napló_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TablaMuvelet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TablaNaplo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Button Btn_Modosit;
        internal System.Windows.Forms.TextBox TxtBxMegjegyzes;
        internal System.Windows.Forms.Label LblMegjegyzes;
        internal System.Windows.Forms.Label LblAznapiUzemora;
        internal System.Windows.Forms.TextBox TxtBxUtolagUzemora;
        internal Zuby.ADGV.AdvancedDataGridView TablaMuvelet;
        internal Zuby.ADGV.AdvancedDataGridView TablaNaplo;
        internal System.Windows.Forms.Label LblUtolagMuvelet;
        internal System.Windows.Forms.Label LblUtolagNaplozasTabla;
        internal System.Windows.Forms.Label lblUzemora;
        internal System.Windows.Forms.TextBox txtBxUzemora;
        internal System.Windows.Forms.Label lblDatum;
        internal System.Windows.Forms.DateTimePicker DtmPckr;
        internal System.Windows.Forms.ToolTip toolTip1;
    }
}