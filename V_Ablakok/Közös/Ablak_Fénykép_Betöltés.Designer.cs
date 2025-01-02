namespace Villamos.Villamos_Ablakok.Közös
{
    partial class Ablak_Fénykép_Betöltés
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Fénykép_Betöltés));
            this.Fényképek = new System.Windows.Forms.ListBox();
            this.Képnyitó = new System.Windows.Forms.Button();
            this.Képválasztó = new System.Windows.Forms.Button();
            this.Képtöltő = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Képtöltő)).BeginInit();
            this.SuspendLayout();
            // 
            // Fényképek
            // 
            this.Fényképek.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Fényképek.FormattingEnabled = true;
            this.Fényképek.ItemHeight = 20;
            this.Fényképek.Location = new System.Drawing.Point(13, 67);
            this.Fényképek.Name = "Fényképek";
            this.Fényképek.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.Fényképek.Size = new System.Drawing.Size(243, 444);
            this.Fényképek.TabIndex = 2;
            this.Fényképek.SelectedIndexChanged += new System.EventHandler(this.Fényképek_SelectedIndexChanged);
            // 
            // Képnyitó
            // 
            this.Képnyitó.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.Képnyitó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Képnyitó.Location = new System.Drawing.Point(13, 14);
            this.Képnyitó.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Képnyitó.Name = "Képnyitó";
            this.Képnyitó.Size = new System.Drawing.Size(45, 45);
            this.Képnyitó.TabIndex = 237;
            this.Képnyitó.UseVisualStyleBackColor = true;
            this.Képnyitó.Click += new System.EventHandler(this.Képnyitó_Click);
            // 
            // Képválasztó
            // 
            this.Képválasztó.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Képválasztó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Képválasztó.Location = new System.Drawing.Point(210, 15);
            this.Képválasztó.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Képválasztó.Name = "Képválasztó";
            this.Képválasztó.Size = new System.Drawing.Size(45, 45);
            this.Képválasztó.TabIndex = 234;
            this.Képválasztó.UseVisualStyleBackColor = true;
            this.Képválasztó.Click += new System.EventHandler(this.Képválasztó_Click);
            // 
            // Képtöltő
            // 
            this.Képtöltő.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Képtöltő.Location = new System.Drawing.Point(262, 12);
            this.Képtöltő.Name = "Képtöltő";
            this.Képtöltő.Size = new System.Drawing.Size(775, 499);
            this.Képtöltő.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Képtöltő.TabIndex = 0;
            this.Képtöltő.TabStop = false;
            // 
            // Ablak_Fénykép_Betöltés
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaGreen;
            this.ClientSize = new System.Drawing.Size(1049, 519);
            this.Controls.Add(this.Képnyitó);
            this.Controls.Add(this.Képválasztó);
            this.Controls.Add(this.Fényképek);
            this.Controls.Add(this.Képtöltő);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Fénykép_Betöltés";
            this.Text = "Ablak_Fénykép_Betöltés";
            this.Load += new System.EventHandler(this.Ablak_Fénykép_Betöltés_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Képtöltő)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Button Képnyitó;
        internal System.Windows.Forms.Button Képválasztó;
        internal System.Windows.Forms.ListBox Fényképek;
        internal System.Windows.Forms.PictureBox Képtöltő;
    }
}