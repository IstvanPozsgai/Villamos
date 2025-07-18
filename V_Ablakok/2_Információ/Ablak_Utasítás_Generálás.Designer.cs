namespace Villamos.V_Ablakok.Közös
{
    partial class Ablak_Utasítás_Generálás
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Utasítás_Generálás));
            this.Txtírásimező = new System.Windows.Forms.RichTextBox();
            this.Btnrögzítés = new System.Windows.Forms.Button();
            this.Label13 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // Txtírásimező
            // 
            this.Txtírásimező.AcceptsTab = true;
            this.Txtírásimező.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Txtírásimező.Location = new System.Drawing.Point(12, 12);
            this.Txtírásimező.Name = "Txtírásimező";
            this.Txtírásimező.Size = new System.Drawing.Size(995, 485);
            this.Txtírásimező.TabIndex = 82;
            this.Txtírásimező.Text = "";
            // 
            // Btnrögzítés
            // 
            this.Btnrögzítés.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btnrögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btnrögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnrögzítés.Location = new System.Drawing.Point(1014, 14);
            this.Btnrögzítés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btnrögzítés.Name = "Btnrögzítés";
            this.Btnrögzítés.Size = new System.Drawing.Size(48, 48);
            this.Btnrögzítés.TabIndex = 83;
            this.toolTip1.SetToolTip(this.Btnrögzítés, "Rögzíti az utasítást");
            this.Btnrögzítés.UseVisualStyleBackColor = true;
            this.Btnrögzítés.Click += new System.EventHandler(this.Btnrögzítés_Click);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 7);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Ablak_Utasítás_Generálás
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tan;
            this.ClientSize = new System.Drawing.Size(1073, 509);
            this.Controls.Add(this.Txtírásimező);
            this.Controls.Add(this.Btnrögzítés);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Utasítás_Generálás";
            this.Text = "Utasítás  Írás";
            this.Load += new System.EventHandler(this.Ablak_Utasítás_Generálás_Load);
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.RichTextBox Txtírásimező;
        internal System.Windows.Forms.Button Btnrögzítés;
        internal System.Windows.Forms.Label Label13;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}