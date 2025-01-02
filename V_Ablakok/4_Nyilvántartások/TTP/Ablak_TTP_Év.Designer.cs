namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.TTP
{
    partial class Ablak_TTP_Év
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
            this.Tábla_Év = new System.Windows.Forms.DataGridView();
            this.Btn_TTP_Rögz = new System.Windows.Forms.Button();
            this.TxtBxÉletkor = new System.Windows.Forms.TextBox();
            this.TxtBxÉv = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnTöröl = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Év)).BeginInit();
            this.SuspendLayout();
            // 
            // Tábla_Év
            // 
            this.Tábla_Év.AllowUserToAddRows = false;
            this.Tábla_Év.AllowUserToDeleteRows = false;
            this.Tábla_Év.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Tábla_Év.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_Év.Location = new System.Drawing.Point(17, 130);
            this.Tábla_Év.Margin = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.Tábla_Év.Name = "Tábla_Év";
            this.Tábla_Év.RowHeadersVisible = false;
            this.Tábla_Év.RowHeadersWidth = 62;
            this.Tábla_Év.RowTemplate.Height = 28;
            this.Tábla_Év.Size = new System.Drawing.Size(333, 458);
            this.Tábla_Év.TabIndex = 1;
            this.Tábla_Év.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_Év_CellClick);
            // 
            // Btn_TTP_Rögz
            // 
            this.Btn_TTP_Rögz.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_TTP_Rögz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Btn_TTP_Rögz.Location = new System.Drawing.Point(261, 14);
            this.Btn_TTP_Rögz.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btn_TTP_Rögz.Name = "Btn_TTP_Rögz";
            this.Btn_TTP_Rögz.Size = new System.Drawing.Size(45, 45);
            this.Btn_TTP_Rögz.TabIndex = 73;
            this.toolTip1.SetToolTip(this.Btn_TTP_Rögz, "Rögzít");
            this.Btn_TTP_Rögz.UseVisualStyleBackColor = true;
            this.Btn_TTP_Rögz.Click += new System.EventHandler(this.Btn_TTP_Rögz_Click);
            // 
            // TxtBxÉletkor
            // 
            this.TxtBxÉletkor.Location = new System.Drawing.Point(17, 37);
            this.TxtBxÉletkor.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtBxÉletkor.Name = "TxtBxÉletkor";
            this.TxtBxÉletkor.Size = new System.Drawing.Size(106, 26);
            this.TxtBxÉletkor.TabIndex = 74;
            // 
            // TxtBxÉv
            // 
            this.TxtBxÉv.Location = new System.Drawing.Point(17, 93);
            this.TxtBxÉv.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtBxÉv.Name = "TxtBxÉv";
            this.TxtBxÉv.Size = new System.Drawing.Size(106, 26);
            this.TxtBxÉv.TabIndex = 75;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 76;
            this.label1.Text = "Életkor";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.label2.Location = new System.Drawing.Point(13, 68);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(346, 20);
            this.label2.TabIndex = 77;
            this.label2.Text = "Hasznos élettartam növelésének átlagos értéke";
            // 
            // BtnTöröl
            // 
            this.BtnTöröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.BtnTöröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnTöröl.Location = new System.Drawing.Point(314, 14);
            this.BtnTöröl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnTöröl.Name = "BtnTöröl";
            this.BtnTöröl.Size = new System.Drawing.Size(45, 45);
            this.BtnTöröl.TabIndex = 78;
            this.toolTip1.SetToolTip(this.BtnTöröl, "Törlés");
            this.BtnTöröl.UseVisualStyleBackColor = true;
            this.BtnTöröl.Click += new System.EventHandler(this.BtnTöröl_Click);
            // 
            // Ablak_TTP_Év
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 603);
            this.Controls.Add(this.BtnTöröl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtBxÉv);
            this.Controls.Add(this.TxtBxÉletkor);
            this.Controls.Add(this.Btn_TTP_Rögz);
            this.Controls.Add(this.Tábla_Év);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ablak_TTP_Év";
            this.Text = "Beállítja a  jármű életkorához a vizsgálat gyakoriságát";
            this.Load += new System.EventHandler(this.Ablak_TTP_Év_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Év)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.DataGridView Tábla_Év;
        internal System.Windows.Forms.Button Btn_TTP_Rögz;
        internal System.Windows.Forms.TextBox TxtBxÉletkor;
        internal System.Windows.Forms.TextBox TxtBxÉv;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Button BtnTöröl;
        internal System.Windows.Forms.ToolTip toolTip1;
    }
}