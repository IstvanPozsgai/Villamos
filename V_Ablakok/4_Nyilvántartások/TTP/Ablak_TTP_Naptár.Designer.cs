namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.TTP
{
    partial class Ablak_TTP_Naptár
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        internal System.ComponentModel.IContainer components = null;

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
            this.TáblaNaptár = new System.Windows.Forms.DataGridView();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.ChkMunkanap = new System.Windows.Forms.CheckBox();
            this.BtnRögzít = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TáblaNaptár)).BeginInit();
            this.SuspendLayout();
            // 
            // TáblaNaptár
            // 
            this.TáblaNaptár.AllowUserToAddRows = false;
            this.TáblaNaptár.AllowUserToDeleteRows = false;
            this.TáblaNaptár.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TáblaNaptár.Location = new System.Drawing.Point(12, 68);
            this.TáblaNaptár.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.TáblaNaptár.Name = "TáblaNaptár";
            this.TáblaNaptár.RowHeadersVisible = false;
            this.TáblaNaptár.RowHeadersWidth = 62;
            this.TáblaNaptár.RowTemplate.Height = 28;
            this.TáblaNaptár.Size = new System.Drawing.Size(423, 602);
            this.TáblaNaptár.TabIndex = 1;
            this.TáblaNaptár.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TáblaNaptár_CellClick);
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(13, 34);
            this.Dátum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(144, 26);
            this.Dátum.TabIndex = 3;
            this.Dátum.ValueChanged += new System.EventHandler(this.Dátum_ValueChanged);
            // 
            // ChkMunkanap
            // 
            this.ChkMunkanap.AutoSize = true;
            this.ChkMunkanap.Location = new System.Drawing.Point(165, 36);
            this.ChkMunkanap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChkMunkanap.Name = "ChkMunkanap";
            this.ChkMunkanap.Size = new System.Drawing.Size(103, 24);
            this.ChkMunkanap.TabIndex = 4;
            this.ChkMunkanap.Text = "Munkanap";
            this.ChkMunkanap.UseVisualStyleBackColor = true;
            // 
            // BtnRögzít
            // 
            this.BtnRögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnRögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnRögzít.Location = new System.Drawing.Point(280, 15);
            this.BtnRögzít.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.BtnRögzít.Name = "BtnRögzít";
            this.BtnRögzít.Size = new System.Drawing.Size(45, 45);
            this.BtnRögzít.TabIndex = 5;
            this.toolTip1.SetToolTip(this.BtnRögzít, "Rögzítés");
            this.BtnRögzít.UseVisualStyleBackColor = true;
            this.BtnRögzít.Click += new System.EventHandler(this.BtnRögzít_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::Villamos.Properties.Resources.CALENDR1;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button1.Location = new System.Drawing.Point(390, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 45);
            this.button1.TabIndex = 6;
            this.toolTip1.SetToolTip(this.button1, "A naptár adatok áttöltése");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Ablak_TTP_Naptár
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 685);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.BtnRögzít);
            this.Controls.Add(this.ChkMunkanap);
            this.Controls.Add(this.Dátum);
            this.Controls.Add(this.TáblaNaptár);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ablak_TTP_Naptár";
            this.Text = "Naptár beállítása a TTP vizsgálat végzéséhez";
            this.Load += new System.EventHandler(this.Ablak_TTP_Naptár_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TáblaNaptár)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView TáblaNaptár;
        internal System.Windows.Forms.DateTimePicker Dátum;
        internal System.Windows.Forms.CheckBox ChkMunkanap;
        internal System.Windows.Forms.Button BtnRögzít;
        internal System.Windows.Forms.ToolTip toolTip1;
        internal System.Windows.Forms.Button button1;
    }
}