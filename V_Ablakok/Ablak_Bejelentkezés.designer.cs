using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    public partial class AblakBejelentkezés : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }

        // Required by the Windows Form Designer
        private System.ComponentModel.IContainer components;

        // NOTE: The following procedure is required by the Windows Form Designer
        // It can be modified using the Windows Form Designer.  
        // Do not modify it using the code editor.
        [DebuggerStepThrough()]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AblakBejelentkezés));
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.Súgó = new System.Windows.Forms.Button();
            this.lblVerzió = new System.Windows.Forms.Label();
            this.CmbTelephely = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Btnlekérdezés = new System.Windows.Forms.Button();
            this.lblProgramnév = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.CmbUserName = new System.Windows.Forms.ComboBox();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.BtnMégse = new System.Windows.Forms.Button();
            this.BtnBelépés = new System.Windows.Forms.Button();
            this.BtnJelszóMódosítás = new System.Windows.Forms.Button();
            this.Timer_kilép = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.BackColor = System.Drawing.Color.Tan;
            this.GroupBox1.Controls.Add(this.Súgó);
            this.GroupBox1.Controls.Add(this.lblVerzió);
            this.GroupBox1.Controls.Add(this.CmbTelephely);
            this.GroupBox1.Controls.Add(this.Label2);
            this.GroupBox1.Controls.Add(this.Btnlekérdezés);
            this.GroupBox1.Controls.Add(this.lblProgramnév);
            this.GroupBox1.Location = new System.Drawing.Point(5, 5);
            this.GroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GroupBox1.Size = new System.Drawing.Size(555, 91);
            this.GroupBox1.TabIndex = 28;
            this.GroupBox1.TabStop = false;
            // 
            // Súgó
            // 
            this.Súgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Súgó.Location = new System.Drawing.Point(512, 8);
            this.Súgó.Name = "Súgó";
            this.Súgó.Size = new System.Drawing.Size(40, 40);
            this.Súgó.TabIndex = 2;
            this.toolTip1.SetToolTip(this.Súgó, "Súgó");
            this.Súgó.UseVisualStyleBackColor = true;
            this.Súgó.Click += new System.EventHandler(this.Súgó_Click);
            // 
            // lblVerzió
            // 
            this.lblVerzió.BackColor = System.Drawing.Color.Bisque;
            this.lblVerzió.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblVerzió.Location = new System.Drawing.Point(220, 12);
            this.lblVerzió.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVerzió.Name = "lblVerzió";
            this.lblVerzió.Size = new System.Drawing.Size(163, 27);
            this.lblVerzió.TabIndex = 38;
            this.lblVerzió.Text = "Verzió: 20.04.19";
            this.lblVerzió.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CmbTelephely
            // 
            this.CmbTelephely.DropDownHeight = 160;
            this.CmbTelephely.DropDownWidth = 215;
            this.CmbTelephely.FormattingEnabled = true;
            this.CmbTelephely.IntegralHeight = false;
            this.CmbTelephely.ItemHeight = 20;
            this.CmbTelephely.Location = new System.Drawing.Point(156, 49);
            this.CmbTelephely.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CmbTelephely.Name = "CmbTelephely";
            this.CmbTelephely.Size = new System.Drawing.Size(213, 28);
            this.CmbTelephely.TabIndex = 0;
            this.CmbTelephely.SelectedIndexChanged += new System.EventHandler(this.CmbTelephely_SelectedIndexChanged);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(56, 49);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(80, 20);
            this.Label2.TabIndex = 32;
            this.Label2.Text = "Telephely:";
            // 
            // Btnlekérdezés
            // 
            this.Btnlekérdezés.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Btnlekérdezés.Location = new System.Drawing.Point(393, 51);
            this.Btnlekérdezés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Btnlekérdezés.Name = "Btnlekérdezés";
            this.Btnlekérdezés.Size = new System.Drawing.Size(154, 30);
            this.Btnlekérdezés.TabIndex = 1;
            this.Btnlekérdezés.Text = "Csak Lekérdezésre";
            this.Btnlekérdezés.UseVisualStyleBackColor = true;
            this.Btnlekérdezés.Visible = false;
            this.Btnlekérdezés.Click += new System.EventHandler(this.Btnlekérdezés_Click);
            // 
            // lblProgramnév
            // 
            this.lblProgramnév.BackColor = System.Drawing.Color.Bisque;
            this.lblProgramnév.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblProgramnév.Location = new System.Drawing.Point(8, 12);
            this.lblProgramnév.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProgramnév.Name = "lblProgramnév";
            this.lblProgramnév.Size = new System.Drawing.Size(208, 27);
            this.lblProgramnév.TabIndex = 31;
            this.lblProgramnév.Text = "Villamos Nyilvántartások";
            this.lblProgramnév.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GroupBox2
            // 
            this.GroupBox2.BackColor = System.Drawing.Color.Tan;
            this.GroupBox2.Controls.Add(this.CmbUserName);
            this.GroupBox2.Controls.Add(this.TxtPassword);
            this.GroupBox2.Controls.Add(this.Label4);
            this.GroupBox2.Controls.Add(this.Label3);
            this.GroupBox2.Controls.Add(this.BtnMégse);
            this.GroupBox2.Controls.Add(this.BtnBelépés);
            this.GroupBox2.Controls.Add(this.BtnJelszóMódosítás);
            this.GroupBox2.Location = new System.Drawing.Point(5, 106);
            this.GroupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.GroupBox2.Size = new System.Drawing.Size(555, 176);
            this.GroupBox2.TabIndex = 40;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Visible = false;
            // 
            // CmbUserName
            // 
            this.CmbUserName.DropDownHeight = 160;
            this.CmbUserName.DropDownWidth = 215;
            this.CmbUserName.FormattingEnabled = true;
            this.CmbUserName.IntegralHeight = false;
            this.CmbUserName.ItemHeight = 20;
            this.CmbUserName.Location = new System.Drawing.Point(156, 21);
            this.CmbUserName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CmbUserName.Name = "CmbUserName";
            this.CmbUserName.Size = new System.Drawing.Size(213, 28);
            this.CmbUserName.Sorted = true;
            this.CmbUserName.TabIndex = 0;
            this.CmbUserName.SelectedIndexChanged += new System.EventHandler(this.CmbUserName_SelectedIndexChanged);
            // 
            // TxtPassword
            // 
            this.TxtPassword.Location = new System.Drawing.Point(156, 64);
            this.TxtPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(215, 26);
            this.TxtPassword.TabIndex = 1;
            this.TxtPassword.UseSystemPasswordChar = true;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(8, 70);
            this.Label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(58, 20);
            this.Label4.TabIndex = 42;
            this.Label4.Text = "Jelszó:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(8, 24);
            this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(128, 20);
            this.Label3.TabIndex = 41;
            this.Label3.Text = "Felhasználó név:";
            // 
            // BtnMégse
            // 
            this.BtnMégse.Location = new System.Drawing.Point(12, 113);
            this.BtnMégse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnMégse.Name = "BtnMégse";
            this.BtnMégse.Size = new System.Drawing.Size(145, 30);
            this.BtnMégse.TabIndex = 3;
            this.BtnMégse.Text = "Mégse";
            this.BtnMégse.UseVisualStyleBackColor = true;
            this.BtnMégse.Click += new System.EventHandler(this.BtnMégse_Click);
            // 
            // BtnBelépés
            // 
            this.BtnBelépés.Location = new System.Drawing.Point(224, 113);
            this.BtnBelépés.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnBelépés.Name = "BtnBelépés";
            this.BtnBelépés.Size = new System.Drawing.Size(145, 30);
            this.BtnBelépés.TabIndex = 2;
            this.BtnBelépés.Text = "Belépés";
            this.BtnBelépés.UseVisualStyleBackColor = true;
            this.BtnBelépés.Click += new System.EventHandler(this.BtnBelépés_Click);
            // 
            // BtnJelszóMódosítás
            // 
            this.BtnJelszóMódosítás.Location = new System.Drawing.Point(393, 19);
            this.BtnJelszóMódosítás.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnJelszóMódosítás.Name = "BtnJelszóMódosítás";
            this.BtnJelszóMódosítás.Size = new System.Drawing.Size(154, 30);
            this.BtnJelszóMódosítás.TabIndex = 4;
            this.BtnJelszóMódosítás.Text = "Jelszó Módosítás";
            this.BtnJelszóMódosítás.UseVisualStyleBackColor = true;
            this.BtnJelszóMódosítás.Click += new System.EventHandler(this.BtnJelszóMódosítás_Click);
            // 
            // Timer_kilép
            // 
            this.Timer_kilép.Interval = 10000;
            this.Timer_kilép.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // AblakBejelentkezés
            // 
            this.AcceptButton = this.BtnBelépés;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 291);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.GroupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AblakBejelentkezés";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bejelentkezés";
            this.Load += new System.EventHandler(this.AblakBejelentkezés_Load);
            this.Shown += new System.EventHandler(this.AblakBejelentkezés_Shown);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.ResumeLayout(false);

        }
        internal GroupBox GroupBox1;
        internal Label lblVerzió;
        internal Label Label2;
        internal Label lblProgramnév;
        internal Button Btnlekérdezés;
        internal GroupBox GroupBox2;
        internal TextBox TxtPassword;
        internal Label Label4;
        internal Label Label3;
        internal Button BtnMégse;
        internal Button BtnJelszóMódosítás;
        private Button BtnBelépés;
        internal ComboBox CmbUserName;
        internal Button Súgó;
        internal Timer Timer_kilép;
        public ComboBox CmbTelephely;
        private ToolTip toolTip1;
    }
}