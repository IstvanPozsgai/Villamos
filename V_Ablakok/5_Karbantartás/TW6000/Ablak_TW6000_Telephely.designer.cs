namespace Villamos.Villamos_Ablakok.TW6000
{
    partial class Ablak_TW6000_Telephely
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_TW6000_Telephely));
            this.Telephely_tábla = new System.Windows.Forms.DataGridView();
            this.Üzem_sorszám = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Üzem_rögzít = new System.Windows.Forms.Button();
            this.Üzem_töröl = new System.Windows.Forms.Button();
            this.Command22 = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.Üzemek = new System.Windows.Forms.ComboBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Telephely_tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Telephely_tábla
            // 
            this.Telephely_tábla.AllowUserToAddRows = false;
            this.Telephely_tábla.AllowUserToDeleteRows = false;
            this.Telephely_tábla.AllowUserToResizeColumns = false;
            this.Telephely_tábla.AllowUserToResizeRows = false;
            this.Telephely_tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Telephely_tábla.Location = new System.Drawing.Point(10, 101);
            this.Telephely_tábla.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Telephely_tábla.Name = "Telephely_tábla";
            this.Telephely_tábla.RowHeadersVisible = false;
            this.Telephely_tábla.RowHeadersWidth = 51;
            this.Telephely_tábla.Size = new System.Drawing.Size(305, 263);
            this.Telephely_tábla.TabIndex = 228;
            this.Telephely_tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Telephely_tábla_CellClick);
            // 
            // Üzem_sorszám
            // 
            this.Üzem_sorszám.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Üzem_sorszám.Location = new System.Drawing.Point(10, 65);
            this.Üzem_sorszám.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Üzem_sorszám.Name = "Üzem_sorszám";
            this.Üzem_sorszám.Size = new System.Drawing.Size(72, 26);
            this.Üzem_sorszám.TabIndex = 0;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label5.Location = new System.Drawing.Point(6, 38);
            this.Label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(76, 20);
            this.Label5.TabIndex = 220;
            this.Label5.Text = "Sorszám:";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.Lime;
            this.Label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label6.Location = new System.Drawing.Point(6, 6);
            this.Label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(134, 20);
            this.Label6.TabIndex = 87;
            this.Label6.Text = "Telephely sorrend";
            // 
            // Üzem_rögzít
            // 
            this.Üzem_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Üzem_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Üzem_rögzít.Location = new System.Drawing.Point(275, 51);
            this.Üzem_rögzít.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Üzem_rögzít.Name = "Üzem_rögzít";
            this.Üzem_rögzít.Size = new System.Drawing.Size(40, 40);
            this.Üzem_rögzít.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.Üzem_rögzít, "Rögzít");
            this.Üzem_rögzít.UseVisualStyleBackColor = true;
            this.Üzem_rögzít.Click += new System.EventHandler(this.Üzem_rögzít_Click);
            // 
            // Üzem_töröl
            // 
            this.Üzem_töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Üzem_töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Üzem_töröl.Location = new System.Drawing.Point(227, 18);
            this.Üzem_töröl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Üzem_töröl.Name = "Üzem_töröl";
            this.Üzem_töröl.Size = new System.Drawing.Size(40, 40);
            this.Üzem_töröl.TabIndex = 4;
            this.ToolTip1.SetToolTip(this.Üzem_töröl, "Törlés");
            this.Üzem_töröl.UseVisualStyleBackColor = true;
            this.Üzem_töröl.Click += new System.EventHandler(this.Üzem_töröl_Click);
            // 
            // Command22
            // 
            this.Command22.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command22.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command22.Location = new System.Drawing.Point(178, 18);
            this.Command22.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Command22.Name = "Command22";
            this.Command22.Size = new System.Drawing.Size(40, 40);
            this.Command22.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.Command22, "Listázza a táblázatot");
            this.Command22.UseVisualStyleBackColor = true;
            this.Command22.Click += new System.EventHandler(this.ÜzemTöröl_Click);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label4.Location = new System.Drawing.Point(90, 38);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(80, 20);
            this.Label4.TabIndex = 221;
            this.Label4.Text = "Telephely:";
            // 
            // Üzemek
            // 
            this.Üzemek.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Üzemek.FormattingEnabled = true;
            this.Üzemek.Location = new System.Drawing.Point(90, 63);
            this.Üzemek.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Üzemek.Name = "Üzemek";
            this.Üzemek.Size = new System.Drawing.Size(177, 28);
            this.Üzemek.TabIndex = 1;
            // 
            // Ablak_TW6000_Telephely
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(326, 375);
            this.Controls.Add(this.Üzemek);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Telephely_tábla);
            this.Controls.Add(this.Üzem_rögzít);
            this.Controls.Add(this.Üzem_sorszám);
            this.Controls.Add(this.Üzem_töröl);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Command22);
            this.Controls.Add(this.Label6);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ablak_TW6000_Telephely";
            this.Text = "TW6000 Telephely";
            this.Load += new System.EventHandler(this.Ablak_TW6000_Telephely_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Telephely_tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.DataGridView Telephely_tábla;
        internal System.Windows.Forms.Button Üzem_rögzít;
        internal System.Windows.Forms.Button Üzem_töröl;
        internal System.Windows.Forms.TextBox Üzem_sorszám;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Button Command22;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.ComboBox Üzemek;
        internal System.Windows.Forms.ToolTip ToolTip1;
    }
}