namespace Villamos.Villamos_Ablakok.MEO
{
    partial class Ablak_Kerék_Konverter
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
            this.Végrehajt = new System.Windows.Forms.Button();
            this.Könyvtár = new System.Windows.Forms.Button();
            this.FileList = new System.Windows.Forms.ListBox();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.SuspendLayout();
            // 
            // Végrehajt
            // 
            this.Végrehajt.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Végrehajt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Végrehajt.Location = new System.Drawing.Point(726, 6);
            this.Végrehajt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Végrehajt.Name = "Végrehajt";
            this.Végrehajt.Size = new System.Drawing.Size(45, 45);
            this.Végrehajt.TabIndex = 243;
            this.Végrehajt.UseVisualStyleBackColor = true;
            this.Végrehajt.Click += new System.EventHandler(this.Végrehajt_Click);
            // 
            // Könyvtár
            // 
            this.Könyvtár.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.Könyvtár.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Könyvtár.Location = new System.Drawing.Point(13, 6);
            this.Könyvtár.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Könyvtár.Name = "Könyvtár";
            this.Könyvtár.Size = new System.Drawing.Size(45, 45);
            this.Könyvtár.TabIndex = 242;
            this.Könyvtár.UseVisualStyleBackColor = true;
            this.Könyvtár.Click += new System.EventHandler(this.Könyvtár_Click);
            // 
            // FileList
            // 
            this.FileList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.FileList.FormattingEnabled = true;
            this.FileList.ItemHeight = 20;
            this.FileList.Location = new System.Drawing.Point(12, 59);
            this.FileList.Name = "FileList";
            this.FileList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.FileList.Size = new System.Drawing.Size(758, 604);
            this.FileList.TabIndex = 241;
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(65, 15);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(655, 30);
            this.Holtart.TabIndex = 244;
            this.Holtart.Visible = false;
            // 
            // Ablak_Kerék_Konverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 677);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Végrehajt);
            this.Controls.Add(this.Könyvtár);
            this.Controls.Add(this.FileList);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Kerék_Konverter";
            this.Text = "Kerékmérési adatok konvertálása";
            this.Load += new System.EventHandler(this.Ablak_Kerék_Konverter_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button Végrehajt;
        internal System.Windows.Forms.Button Könyvtár;
        internal System.Windows.Forms.ListBox FileList;
        private V_MindenEgyéb.MyProgressbar Holtart;
    }
}