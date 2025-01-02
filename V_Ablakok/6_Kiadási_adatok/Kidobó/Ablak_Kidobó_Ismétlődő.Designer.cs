namespace Villamos.Villamos_Ablakok
{
    partial class Ablak_Kidobó_Ismétlődő
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Kidobó_Ismétlődő));
            this.Frame3KezdésiIdő = new System.Windows.Forms.DateTimePicker();
            this.Frame3VégzésiIdő = new System.Windows.Forms.DateTimePicker();
            this.Frame3Megjegyzés = new System.Windows.Forms.TextBox();
            this.Frame3VégzésiHely = new System.Windows.Forms.TextBox();
            this.Frame3KezdésiHely = new System.Windows.Forms.TextBox();
            this.Frame3ForgalmiSzám = new System.Windows.Forms.TextBox();
            this.Változatkarb = new System.Windows.Forms.Button();
            this.ComboVáltozat = new System.Windows.Forms.ComboBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Frame3Szolgálatiszám = new System.Windows.Forms.TextBox();
            this.Command4 = new System.Windows.Forms.Button();
            this.Command5 = new System.Windows.Forms.Button();
            this.Törlés = new System.Windows.Forms.Button();
            this.Rögzítés = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Frame3KezdésiIdő
            // 
            this.Frame3KezdésiIdő.CustomFormat = "HH:mm:ss";
            this.Frame3KezdésiIdő.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.Frame3KezdésiIdő.Location = new System.Drawing.Point(139, 176);
            this.Frame3KezdésiIdő.Name = "Frame3KezdésiIdő";
            this.Frame3KezdésiIdő.Size = new System.Drawing.Size(91, 26);
            this.Frame3KezdésiIdő.TabIndex = 216;
            // 
            // Frame3VégzésiIdő
            // 
            this.Frame3VégzésiIdő.CustomFormat = "HH:mm:ss";
            this.Frame3VégzésiIdő.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.Frame3VégzésiIdő.Location = new System.Drawing.Point(139, 240);
            this.Frame3VégzésiIdő.Name = "Frame3VégzésiIdő";
            this.Frame3VégzésiIdő.Size = new System.Drawing.Size(91, 26);
            this.Frame3VégzésiIdő.TabIndex = 219;
            // 
            // Frame3Megjegyzés
            // 
            this.Frame3Megjegyzés.Location = new System.Drawing.Point(139, 272);
            this.Frame3Megjegyzés.Name = "Frame3Megjegyzés";
            this.Frame3Megjegyzés.Size = new System.Drawing.Size(280, 26);
            this.Frame3Megjegyzés.TabIndex = 220;
            // 
            // Frame3VégzésiHely
            // 
            this.Frame3VégzésiHely.Location = new System.Drawing.Point(139, 208);
            this.Frame3VégzésiHely.Name = "Frame3VégzésiHely";
            this.Frame3VégzésiHely.Size = new System.Drawing.Size(207, 26);
            this.Frame3VégzésiHely.TabIndex = 217;
            // 
            // Frame3KezdésiHely
            // 
            this.Frame3KezdésiHely.Location = new System.Drawing.Point(139, 144);
            this.Frame3KezdésiHely.Name = "Frame3KezdésiHely";
            this.Frame3KezdésiHely.Size = new System.Drawing.Size(207, 26);
            this.Frame3KezdésiHely.TabIndex = 214;
            // 
            // Frame3ForgalmiSzám
            // 
            this.Frame3ForgalmiSzám.Location = new System.Drawing.Point(139, 112);
            this.Frame3ForgalmiSzám.Name = "Frame3ForgalmiSzám";
            this.Frame3ForgalmiSzám.Size = new System.Drawing.Size(207, 26);
            this.Frame3ForgalmiSzám.TabIndex = 213;
            // 
            // Változatkarb
            // 
            this.Változatkarb.Location = new System.Drawing.Point(139, 42);
            this.Változatkarb.Name = "Változatkarb";
            this.Változatkarb.Size = new System.Drawing.Size(208, 32);
            this.Változatkarb.TabIndex = 211;
            this.Változatkarb.Text = "Lista karbantartás";
            this.Változatkarb.UseVisualStyleBackColor = true;
            this.Változatkarb.Click += new System.EventHandler(this.Változatkarb_Click);
            // 
            // ComboVáltozat
            // 
            this.ComboVáltozat.FormattingEnabled = true;
            this.ComboVáltozat.Location = new System.Drawing.Point(139, 10);
            this.ComboVáltozat.Name = "ComboVáltozat";
            this.ComboVáltozat.Size = new System.Drawing.Size(249, 28);
            this.ComboVáltozat.TabIndex = 210;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(8, 246);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(95, 20);
            this.Label12.TabIndex = 230;
            this.Label12.Text = "Végzési idő:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(8, 214);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(102, 20);
            this.Label11.TabIndex = 229;
            this.Label11.Text = "Végzési hely:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(8, 182);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(94, 20);
            this.Label10.TabIndex = 228;
            this.Label10.Text = "Kezdési idő:";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(8, 150);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(101, 20);
            this.Label9.TabIndex = 227;
            this.Label9.Text = "Kezdési hely:";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(8, 118);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(116, 20);
            this.Label8.TabIndex = 226;
            this.Label8.Text = "Forgalmi szám:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(8, 86);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(124, 20);
            this.Label7.TabIndex = 225;
            this.Label7.Text = "Szolgálati szám:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(8, 21);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(72, 20);
            this.Label4.TabIndex = 224;
            this.Label4.Text = "Változat:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(8, 278);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(97, 20);
            this.Label5.TabIndex = 223;
            this.Label5.Text = "Megjegyzés:";
            // 
            // Frame3Szolgálatiszám
            // 
            this.Frame3Szolgálatiszám.Location = new System.Drawing.Point(139, 80);
            this.Frame3Szolgálatiszám.Name = "Frame3Szolgálatiszám";
            this.Frame3Szolgálatiszám.Size = new System.Drawing.Size(207, 26);
            this.Frame3Szolgálatiszám.TabIndex = 212;
            // 
            // Command4
            // 
            this.Command4.BackgroundImage = global::Villamos.Properties.Resources.BLOONS5;
            this.Command4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command4.Location = new System.Drawing.Point(352, 194);
            this.Command4.Name = "Command4";
            this.Command4.Size = new System.Drawing.Size(40, 40);
            this.Command4.TabIndex = 218;
            this.Command4.UseVisualStyleBackColor = true;
            this.Command4.Click += new System.EventHandler(this.Command4_Click);
            // 
            // Command5
            // 
            this.Command5.BackgroundImage = global::Villamos.Properties.Resources.BLOONS5;
            this.Command5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command5.Location = new System.Drawing.Point(352, 132);
            this.Command5.Name = "Command5";
            this.Command5.Size = new System.Drawing.Size(40, 40);
            this.Command5.TabIndex = 215;
            this.Command5.UseVisualStyleBackColor = true;
            this.Command5.Click += new System.EventHandler(this.Command5_Click);
            // 
            // Törlés
            // 
            this.Törlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Törlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Törlés.Location = new System.Drawing.Point(426, 90);
            this.Törlés.Name = "Törlés";
            this.Törlés.Size = new System.Drawing.Size(40, 40);
            this.Törlés.TabIndex = 222;
            this.Törlés.UseVisualStyleBackColor = true;
            this.Törlés.Click += new System.EventHandler(this.Törlés_Click);
            // 
            // Rögzítés
            // 
            this.Rögzítés.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzítés.Location = new System.Drawing.Point(426, 258);
            this.Rögzítés.Margin = new System.Windows.Forms.Padding(4);
            this.Rögzítés.Name = "Rögzítés";
            this.Rögzítés.Size = new System.Drawing.Size(40, 40);
            this.Rögzítés.TabIndex = 221;
            this.Rögzítés.UseVisualStyleBackColor = true;
            this.Rögzítés.Click += new System.EventHandler(this.Rögzítés_Click);
            // 
            // Ablak_Kidobó_Ismétlődő
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tomato;
            this.ClientSize = new System.Drawing.Size(482, 307);
            this.Controls.Add(this.Command4);
            this.Controls.Add(this.Command5);
            this.Controls.Add(this.Frame3KezdésiIdő);
            this.Controls.Add(this.Frame3VégzésiIdő);
            this.Controls.Add(this.Frame3Megjegyzés);
            this.Controls.Add(this.Frame3VégzésiHely);
            this.Controls.Add(this.Frame3KezdésiHely);
            this.Controls.Add(this.Frame3ForgalmiSzám);
            this.Controls.Add(this.Változatkarb);
            this.Controls.Add(this.ComboVáltozat);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Törlés);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Rögzítés);
            this.Controls.Add(this.Frame3Szolgálatiszám);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Ablak_Kidobó_Ismétlődő";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Ismétlődő adatok rögzítése";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_Kidobó_Ismétlődő_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Kidobó_Ismétlődő_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_Kidobó_Ismétlődő_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Command4;
        internal System.Windows.Forms.Button Command5;
        internal System.Windows.Forms.DateTimePicker Frame3VégzésiIdő;
        internal System.Windows.Forms.TextBox Frame3Megjegyzés;
        internal System.Windows.Forms.TextBox Frame3VégzésiHely;
        internal System.Windows.Forms.TextBox Frame3KezdésiHely;
        internal System.Windows.Forms.TextBox Frame3ForgalmiSzám;
        internal System.Windows.Forms.Button Változatkarb;
        internal System.Windows.Forms.ComboBox ComboVáltozat;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Button Törlés;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Button Rögzítés;
        internal System.Windows.Forms.TextBox Frame3Szolgálatiszám;
        internal System.Windows.Forms.DateTimePicker Frame3KezdésiIdő;
    }
}