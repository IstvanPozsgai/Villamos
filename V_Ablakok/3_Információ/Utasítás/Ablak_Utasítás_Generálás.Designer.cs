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
            this.IIISzak = new System.Windows.Forms.Button();
            this.IISzak = new System.Windows.Forms.Button();
            this.ISzak = new System.Windows.Forms.Button();
            this.MindKijelöl = new System.Windows.Forms.Button();
            this.MindVissza = new System.Windows.Forms.Button();
            this.Üzemek = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.Panel = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.Panel.SuspendLayout();
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
            this.Txtírásimező.Size = new System.Drawing.Size(538, 587);
            this.Txtírásimező.TabIndex = 82;
            this.Txtírásimező.Text = "";
            // 
            // Btnrögzítés
            // 
            this.Btnrögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btnrögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btnrögzítés.Location = new System.Drawing.Point(4, 5);
            this.Btnrögzítés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btnrögzítés.Name = "Btnrögzítés";
            this.Btnrögzítés.Size = new System.Drawing.Size(45, 45);
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
            // IIISzak
            // 
            this.IIISzak.BackgroundImage = global::Villamos.Properties.Resources._3B;
            this.IIISzak.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.IIISzak.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.IIISzak.Location = new System.Drawing.Point(113, 63);
            this.IIISzak.Name = "IIISzak";
            this.IIISzak.Size = new System.Drawing.Size(45, 45);
            this.IIISzak.TabIndex = 88;
            this.toolTip1.SetToolTip(this.IIISzak, "III Vontatási üzemeinek kijelölése");
            this.IIISzak.UseVisualStyleBackColor = true;
            this.IIISzak.Click += new System.EventHandler(this.IIISzak_Click);
            // 
            // IISzak
            // 
            this.IISzak.BackgroundImage = global::Villamos.Properties.Resources._2B;
            this.IISzak.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.IISzak.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.IISzak.Location = new System.Drawing.Point(58, 63);
            this.IISzak.Name = "IISzak";
            this.IISzak.Size = new System.Drawing.Size(45, 45);
            this.IISzak.TabIndex = 87;
            this.toolTip1.SetToolTip(this.IISzak, "II Vontatási üzemeinek kijelölése");
            this.IISzak.UseVisualStyleBackColor = true;
            this.IISzak.Click += new System.EventHandler(this.IISzak_Click);
            // 
            // ISzak
            // 
            this.ISzak.BackgroundImage = global::Villamos.Properties.Resources._1B;
            this.ISzak.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ISzak.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ISzak.Location = new System.Drawing.Point(3, 63);
            this.ISzak.Name = "ISzak";
            this.ISzak.Size = new System.Drawing.Size(45, 45);
            this.ISzak.TabIndex = 86;
            this.toolTip1.SetToolTip(this.ISzak, "I Vontatási üzemeinek kijelölése");
            this.ISzak.UseVisualStyleBackColor = true;
            this.ISzak.Click += new System.EventHandler(this.ISzak_Click);
            // 
            // MindKijelöl
            // 
            this.MindKijelöl.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.MindKijelöl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MindKijelöl.Location = new System.Drawing.Point(114, 5);
            this.MindKijelöl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MindKijelöl.Name = "MindKijelöl";
            this.MindKijelöl.Size = new System.Drawing.Size(45, 45);
            this.MindKijelöl.TabIndex = 173;
            this.toolTip1.SetToolTip(this.MindKijelöl, "Mindent kijelöl");
            this.MindKijelöl.UseVisualStyleBackColor = true;
            this.MindKijelöl.Click += new System.EventHandler(this.MindKijelöl_Click);
            // 
            // MindVissza
            // 
            this.MindVissza.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.MindVissza.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MindVissza.Location = new System.Drawing.Point(169, 5);
            this.MindVissza.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MindVissza.Name = "MindVissza";
            this.MindVissza.Size = new System.Drawing.Size(45, 45);
            this.MindVissza.TabIndex = 174;
            this.toolTip1.SetToolTip(this.MindVissza, "Minden kijelölést töröl");
            this.MindVissza.UseVisualStyleBackColor = true;
            this.MindVissza.Click += new System.EventHandler(this.MindVissza_Click);
            // 
            // Üzemek
            // 
            this.Üzemek.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Üzemek.Enabled = false;
            this.Üzemek.FormattingEnabled = true;
            this.Üzemek.Location = new System.Drawing.Point(560, 196);
            this.Üzemek.Name = "Üzemek";
            this.Üzemek.Size = new System.Drawing.Size(231, 403);
            this.Üzemek.TabIndex = 84;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.Controls.Add(this.Btnrögzítés, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(560, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(229, 62);
            this.tableLayoutPanel1.TabIndex = 88;
            // 
            // Panel
            // 
            this.Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel.ColumnCount = 5;
            this.Panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.Panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.Panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.Panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.Panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.Panel.Controls.Add(this.IIISzak, 2, 1);
            this.Panel.Controls.Add(this.IISzak, 1, 1);
            this.Panel.Controls.Add(this.ISzak, 0, 1);
            this.Panel.Controls.Add(this.MindVissza, 3, 0);
            this.Panel.Controls.Add(this.MindKijelöl, 2, 0);
            this.Panel.Location = new System.Drawing.Point(560, 73);
            this.Panel.Name = "Panel";
            this.Panel.RowCount = 2;
            this.Panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.Panel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.Panel.Size = new System.Drawing.Size(231, 117);
            this.Panel.TabIndex = 89;
            // 
            // Ablak_Utasítás_Generálás
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LimeGreen;
            this.ClientSize = new System.Drawing.Size(799, 611);
            this.Controls.Add(this.Panel);
            this.Controls.Add(this.Üzemek);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.Txtírásimező);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Utasítás_Generálás";
            this.Text = "Utasítás  Írás";
            this.Load += new System.EventHandler(this.Ablak_Utasítás_Generálás_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.RichTextBox Txtírásimező;
        internal System.Windows.Forms.Button Btnrögzítés;
        internal System.Windows.Forms.Label Label13;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckedListBox Üzemek;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel Panel;
        internal System.Windows.Forms.Button IIISzak;
        internal System.Windows.Forms.Button IISzak;
        internal System.Windows.Forms.Button ISzak;
        internal System.Windows.Forms.Button MindVissza;
        internal System.Windows.Forms.Button MindKijelöl;
    }
}