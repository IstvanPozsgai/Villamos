using System.Windows.Forms;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Sérülés
{
    partial class Ablak_Sérülés_Kép
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
            this.Képnyitó = new System.Windows.Forms.Button();
            this.Képválasztó = new System.Windows.Forms.Button();
            this.ListBox1 = new System.Windows.Forms.ListBox();
            this.Képtöltő = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Képtöltő)).BeginInit();
            this.SuspendLayout();
            // 
            // Képnyitó
            // 
            this.Képnyitó.BackgroundImage = global::Villamos.Properties.Resources.Folder_;
            this.Képnyitó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Képnyitó.Location = new System.Drawing.Point(10, 10);
            this.Képnyitó.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Képnyitó.Name = "Képnyitó";
            this.Képnyitó.Size = new System.Drawing.Size(45, 45);
            this.Képnyitó.TabIndex = 241;
            this.Képnyitó.UseVisualStyleBackColor = true;
            this.Képnyitó.Click += new System.EventHandler(this.Képnyitó_Click);
            // 
            // Képválasztó
            // 
            this.Képválasztó.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Képválasztó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Képválasztó.Location = new System.Drawing.Point(227, 10);
            this.Képválasztó.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.Képválasztó.Name = "Képválasztó";
            this.Képválasztó.Size = new System.Drawing.Size(45, 45);
            this.Képválasztó.TabIndex = 240;
            this.Képválasztó.UseVisualStyleBackColor = true;
            this.Képválasztó.Click += new System.EventHandler(this.Képválasztó_Click);
            // 
            // ListBox1
            // 
            this.ListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ListBox1.FormattingEnabled = true;
            this.ListBox1.ItemHeight = 20;
            this.ListBox1.Location = new System.Drawing.Point(10, 68);
            this.ListBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ListBox1.Name = "ListBox1";
            this.ListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ListBox1.Size = new System.Drawing.Size(262, 484);
            this.ListBox1.TabIndex = 239;
            this.ListBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox1_SelectedIndexChanged);
            // 
            // Képtöltő
            // 
            this.Képtöltő.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Képtöltő.BackColor = System.Drawing.Color.SeaGreen;
            this.Képtöltő.Location = new System.Drawing.Point(280, 10);
            this.Képtöltő.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Képtöltő.Name = "Képtöltő";
            this.Képtöltő.Size = new System.Drawing.Size(491, 542);
            this.Képtöltő.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Képtöltő.TabIndex = 238;
            this.Képtöltő.TabStop = false;
            // 
            // Ablak_Sérülés_Kép
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.Képnyitó);
            this.Controls.Add(this.Képválasztó);
            this.Controls.Add(this.ListBox1);
            this.Controls.Add(this.Képtöltő);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Sérülés_Kép";
            this.Text = "Ablak_Sérülés_Kép";
            this.Load += new System.EventHandler(this.Ablak_Sérülés_Kép_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Képtöltő)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button Képnyitó;
        internal System.Windows.Forms.Button Képválasztó;
        internal System.Windows.Forms.ListBox ListBox1;
        internal System.Windows.Forms.PictureBox Képtöltő;
        private ToolTip toolTip1;
    }
}