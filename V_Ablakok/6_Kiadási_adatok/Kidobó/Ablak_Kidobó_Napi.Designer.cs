namespace Villamos.Villamos_Ablakok
{
    partial class Ablak_Kidobó_Napi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Kidobó_Napi));
            this.KezdésiIdő = new System.Windows.Forms.DateTimePicker();
            this.VégzésiIdő = new System.Windows.Forms.DateTimePicker();
            this.Megjegyzés = new System.Windows.Forms.TextBox();
            this.VégzésiHely = new System.Windows.Forms.TextBox();
            this.KezdésiHely = new System.Windows.Forms.TextBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.Command3 = new System.Windows.Forms.Button();
            this.Plusz = new System.Windows.Forms.Button();
            this.Rögzít = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // KezdésiIdő
            // 
            this.KezdésiIdő.CustomFormat = "HH:mm:ss";
            this.KezdésiIdő.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.KezdésiIdő.Location = new System.Drawing.Point(142, 57);
            this.KezdésiIdő.Name = "KezdésiIdő";
            this.KezdésiIdő.Size = new System.Drawing.Size(91, 26);
            this.KezdésiIdő.TabIndex = 2;
            // 
            // VégzésiIdő
            // 
            this.VégzésiIdő.CustomFormat = "HH:mm:ss";
            this.VégzésiIdő.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.VégzésiIdő.Location = new System.Drawing.Point(142, 140);
            this.VégzésiIdő.Name = "VégzésiIdő";
            this.VégzésiIdő.Size = new System.Drawing.Size(91, 26);
            this.VégzésiIdő.TabIndex = 5;
            // 
            // Megjegyzés
            // 
            this.Megjegyzés.Location = new System.Drawing.Point(142, 199);
            this.Megjegyzés.Name = "Megjegyzés";
            this.Megjegyzés.Size = new System.Drawing.Size(280, 26);
            this.Megjegyzés.TabIndex = 6;
            // 
            // VégzésiHely
            // 
            this.VégzésiHely.Location = new System.Drawing.Point(142, 108);
            this.VégzésiHely.Name = "VégzésiHely";
            this.VégzésiHely.Size = new System.Drawing.Size(207, 26);
            this.VégzésiHely.TabIndex = 3;
            // 
            // KezdésiHely
            // 
            this.KezdésiHely.Location = new System.Drawing.Point(142, 25);
            this.KezdésiHely.Name = "KezdésiHely";
            this.KezdésiHely.Size = new System.Drawing.Size(207, 26);
            this.KezdésiHely.TabIndex = 0;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(11, 146);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(95, 20);
            this.Label14.TabIndex = 196;
            this.Label14.Text = "Végzési idő:";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(11, 114);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(102, 20);
            this.Label15.TabIndex = 195;
            this.Label15.Text = "Végzési hely:";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(11, 63);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(94, 20);
            this.Label16.TabIndex = 194;
            this.Label16.Text = "Kezdési idő:";
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(11, 31);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(101, 20);
            this.Label17.TabIndex = 193;
            this.Label17.Text = "Kezdési hely:";
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Location = new System.Drawing.Point(11, 205);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(97, 20);
            this.Label21.TabIndex = 87;
            this.Label21.Text = "Megjegyzés:";
            // 
            // Command3
            // 
            this.Command3.BackgroundImage = global::Villamos.Properties.Resources.BLOONS5;
            this.Command3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command3.Location = new System.Drawing.Point(355, 94);
            this.Command3.Name = "Command3";
            this.Command3.Size = new System.Drawing.Size(40, 40);
            this.Command3.TabIndex = 4;
            this.Command3.UseVisualStyleBackColor = true;
            this.Command3.Click += new System.EventHandler(this.Command3_Click);
            // 
            // Plusz
            // 
            this.Plusz.BackgroundImage = global::Villamos.Properties.Resources.BLOONS5;
            this.Plusz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Plusz.Location = new System.Drawing.Point(355, 11);
            this.Plusz.Name = "Plusz";
            this.Plusz.Size = new System.Drawing.Size(40, 40);
            this.Plusz.TabIndex = 1;
            this.Plusz.UseVisualStyleBackColor = true;
            this.Plusz.Click += new System.EventHandler(this.Plusz_Click);
            // 
            // Rögzít
            // 
            this.Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzít.Location = new System.Drawing.Point(428, 186);
            this.Rögzít.Margin = new System.Windows.Forms.Padding(4);
            this.Rögzít.Name = "Rögzít";
            this.Rögzít.Size = new System.Drawing.Size(40, 40);
            this.Rögzít.TabIndex = 7;
            this.Rögzít.UseVisualStyleBackColor = true;
            this.Rögzít.Click += new System.EventHandler(this.Rögzít_Click);
            // 
            // Ablak_Kidobó_Napi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Yellow;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(476, 238);
            this.Controls.Add(this.Command3);
            this.Controls.Add(this.Plusz);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.KezdésiIdő);
            this.Controls.Add(this.Rögzít);
            this.Controls.Add(this.VégzésiIdő);
            this.Controls.Add(this.Megjegyzés);
            this.Controls.Add(this.Label21);
            this.Controls.Add(this.VégzésiHely);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.KezdésiHely);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.Label14);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Ablak_Kidobó_Napi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Ablak_Kidobó_Napi";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Ablak_Kidobó_Napi_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_Kidobó_Napi_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Command3;
        internal System.Windows.Forms.Button Plusz;
        internal System.Windows.Forms.DateTimePicker KezdésiIdő;
        internal System.Windows.Forms.DateTimePicker VégzésiIdő;
        internal System.Windows.Forms.TextBox Megjegyzés;
        internal System.Windows.Forms.TextBox VégzésiHely;
        internal System.Windows.Forms.TextBox KezdésiHely;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.Label Label17;
        internal System.Windows.Forms.Label Label21;
        internal System.Windows.Forms.Button Rögzít;
    }
}