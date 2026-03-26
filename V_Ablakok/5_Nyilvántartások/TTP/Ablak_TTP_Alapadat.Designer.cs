namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.TTP
{
    partial class Ablak_TTP_Alapadat
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
            this.BtnRögzít = new System.Windows.Forms.Button();
            this.CmbPályaszám = new System.Windows.Forms.ComboBox();
            this.DátumGyártás = new System.Windows.Forms.DateTimePicker();
            this.ChbTTP = new System.Windows.Forms.CheckBox();
            this.TxtbxMegjegyz = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // BtnRögzít
            // 
            this.BtnRögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnRögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.BtnRögzít.Location = new System.Drawing.Point(430, 215);
            this.BtnRögzít.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnRögzít.Name = "BtnRögzít";
            this.BtnRögzít.Size = new System.Drawing.Size(45, 45);
            this.BtnRögzít.TabIndex = 0;
            this.toolTip1.SetToolTip(this.BtnRögzít, "Rögzítés");
            this.BtnRögzít.UseVisualStyleBackColor = true;
            this.BtnRögzít.Click += new System.EventHandler(this.BtnRögzít_Click);
            // 
            // CmbPályaszám
            // 
            this.CmbPályaszám.FormattingEnabled = true;
            this.CmbPályaszám.Location = new System.Drawing.Point(12, 38);
            this.CmbPályaszám.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CmbPályaszám.Name = "CmbPályaszám";
            this.CmbPályaszám.Size = new System.Drawing.Size(180, 28);
            this.CmbPályaszám.Sorted = true;
            this.CmbPályaszám.TabIndex = 1;
            this.CmbPályaszám.SelectionChangeCommitted += new System.EventHandler(this.CmbPályaszám_SelectionChangeCommitted);
            this.CmbPályaszám.TextUpdate += new System.EventHandler(this.CmbPályaszám_TextUpdate);
            // 
            // DátumGyártás
            // 
            this.DátumGyártás.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DátumGyártás.Location = new System.Drawing.Point(205, 40);
            this.DátumGyártás.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DátumGyártás.Name = "DátumGyártás";
            this.DátumGyártás.Size = new System.Drawing.Size(122, 26);
            this.DátumGyártás.TabIndex = 2;
            // 
            // ChbTTP
            // 
            this.ChbTTP.AutoSize = true;
            this.ChbTTP.Location = new System.Drawing.Point(339, 14);
            this.ChbTTP.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ChbTTP.Name = "ChbTTP";
            this.ChbTTP.Size = new System.Drawing.Size(130, 24);
            this.ChbTTP.TabIndex = 3;
            this.ChbTTP.Text = "TTP kötelezett";
            this.ChbTTP.UseVisualStyleBackColor = true;
            // 
            // TxtbxMegjegyz
            // 
            this.TxtbxMegjegyz.Location = new System.Drawing.Point(12, 96);
            this.TxtbxMegjegyz.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtbxMegjegyz.Multiline = true;
            this.TxtbxMegjegyz.Name = "TxtbxMegjegyz";
            this.TxtbxMegjegyz.Size = new System.Drawing.Size(464, 109);
            this.TxtbxMegjegyz.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Pályaszám";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(201, 14);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Gyártási dátum";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 71);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Megjegyzés";
            // 
            // Ablak_TTP_Alapadat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 270);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TxtbxMegjegyz);
            this.Controls.Add(this.ChbTTP);
            this.Controls.Add(this.DátumGyártás);
            this.Controls.Add(this.CmbPályaszám);
            this.Controls.Add(this.BtnRögzít);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Ablak_TTP_Alapadat";
            this.Text = "Jármű alapadatainak beállítása";
            this.Load += new System.EventHandler(this.Ablak_TTP_Alapadat_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button BtnRögzít;
        internal System.Windows.Forms.ComboBox CmbPályaszám;
        internal System.Windows.Forms.DateTimePicker DátumGyártás;
        internal System.Windows.Forms.CheckBox ChbTTP;
        internal System.Windows.Forms.TextBox TxtbxMegjegyz;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.ToolTip toolTip1;
        private System.ComponentModel.IContainer components;
    }
}