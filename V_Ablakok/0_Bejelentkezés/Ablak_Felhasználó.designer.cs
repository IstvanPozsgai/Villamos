using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{

    public partial class Ablak_Felhasználó : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components != null)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Felhasználó));
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnRögzít = new System.Windows.Forms.Button();
            this.BtnSugó = new System.Windows.Forms.Button();
            this.BtnÚj = new System.Windows.Forms.Button();
            this.BtnFrissít = new System.Windows.Forms.Button();
            this.BtnDolgozóilsta = new System.Windows.Forms.Button();
            this.TextWinUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextUserNév = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.CmbDolgozószám = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.UserId = new System.Windows.Forms.TextBox();
            this.Törölt = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.CmbDolgozónév = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.Frissít = new System.Windows.Forms.CheckBox();
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.lblSzervezet = new System.Windows.Forms.Label();
            this.ChkSzervezet = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.CmbSzervezet = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Főadmin = new System.Windows.Forms.CheckBox();
            this.TelephelyAdmin = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolTip1
            // 
            this.ToolTip1.IsBalloon = true;
            // 
            // BtnRögzít
            // 
            this.BtnRögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnRögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnRögzít.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnRögzít.Location = new System.Drawing.Point(3, 299);
            this.BtnRögzít.Name = "BtnRögzít";
            this.BtnRögzít.Size = new System.Drawing.Size(44, 45);
            this.BtnRögzít.TabIndex = 97;
            this.ToolTip1.SetToolTip(this.BtnRögzít, "Hozzákötjük a felhasználónévhez  a Windows profilt");
            this.BtnRögzít.UseVisualStyleBackColor = true;
            this.BtnRögzít.Click += new System.EventHandler(this.BtnRögzít_Click);
            // 
            // BtnSugó
            // 
            this.BtnSugó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSugó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSugó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSugó.Location = new System.Drawing.Point(164, 3);
            this.BtnSugó.Name = "BtnSugó";
            this.BtnSugó.Size = new System.Drawing.Size(45, 44);
            this.BtnSugó.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.BtnSugó, "Online sugó megjelenítése");
            this.BtnSugó.UseVisualStyleBackColor = true;
            this.BtnSugó.Click += new System.EventHandler(this.BtnSugó_Click);
            // 
            // BtnÚj
            // 
            this.BtnÚj.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.BtnÚj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnÚj.Location = new System.Drawing.Point(3, 3);
            this.BtnÚj.Name = "BtnÚj";
            this.BtnÚj.Size = new System.Drawing.Size(44, 44);
            this.BtnÚj.TabIndex = 222;
            this.ToolTip1.SetToolTip(this.BtnÚj, "Hozzákötjük a felhasználónévhez  a Windows profilt");
            this.BtnÚj.UseVisualStyleBackColor = true;
            this.BtnÚj.Click += new System.EventHandler(this.BtnÚj_Click);
            // 
            // BtnFrissít
            // 
            this.BtnFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BtnFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnFrissít.Location = new System.Drawing.Point(3, 103);
            this.BtnFrissít.Name = "BtnFrissít";
            this.BtnFrissít.Size = new System.Drawing.Size(44, 44);
            this.BtnFrissít.TabIndex = 223;
            this.ToolTip1.SetToolTip(this.BtnFrissít, "Hozzákötjük a felhasználónévhez  a Windows profilt");
            this.BtnFrissít.UseVisualStyleBackColor = true;
            this.BtnFrissít.Click += new System.EventHandler(this.BtnFrissít_Click);
            // 
            // BtnDolgozóilsta
            // 
            this.BtnDolgozóilsta.BackgroundImage = global::Villamos.Properties.Resources.felhasználók32;
            this.BtnDolgozóilsta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnDolgozóilsta.Location = new System.Drawing.Point(53, 3);
            this.BtnDolgozóilsta.Name = "BtnDolgozóilsta";
            this.BtnDolgozóilsta.Size = new System.Drawing.Size(44, 44);
            this.BtnDolgozóilsta.TabIndex = 224;
            this.ToolTip1.SetToolTip(this.BtnDolgozóilsta, "Frissíti a dolgozói listát IDM adataival");
            this.BtnDolgozóilsta.UseVisualStyleBackColor = true;
            this.BtnDolgozóilsta.Click += new System.EventHandler(this.BtnDolgozóilsta_Click);
            // 
            // TextWinUser
            // 
            this.TextWinUser.Location = new System.Drawing.Point(201, 73);
            this.TextWinUser.MaxLength = 25;
            this.TextWinUser.Name = "TextWinUser";
            this.TextWinUser.Size = new System.Drawing.Size(192, 26);
            this.TextWinUser.TabIndex = 96;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 20);
            this.label2.TabIndex = 95;
            this.label2.Text = "Windows Felhasználónév:";
            // 
            // TextUserNév
            // 
            this.TextUserNév.Location = new System.Drawing.Point(201, 38);
            this.TextUserNév.MaxLength = 25;
            this.TextUserNév.Name = "TextUserNév";
            this.TextUserNév.Size = new System.Drawing.Size(192, 26);
            this.TextUserNév.TabIndex = 88;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(3, 35);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(128, 20);
            this.Label1.TabIndex = 87;
            this.Label1.Text = "Felhasználó név:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CmbDolgozószám, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.TextWinUser, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.Label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.TextUserNév, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.UserId, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Törölt, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.CmbDolgozónév, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.TxtPassword, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.Frissít, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.Főadmin, 1, 8);
            this.tableLayoutPanel1.Controls.Add(this.TelephelyAdmin, 1, 9);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 10;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(670, 347);
            this.tableLayoutPanel1.TabIndex = 99;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 20);
            this.label5.TabIndex = 106;
            this.label5.Text = "Id";
            // 
            // CmbDolgozószám
            // 
            this.CmbDolgozószám.FormattingEnabled = true;
            this.CmbDolgozószám.Location = new System.Drawing.Point(201, 108);
            this.CmbDolgozószám.MaxLength = 8;
            this.CmbDolgozószám.Name = "CmbDolgozószám";
            this.CmbDolgozószám.Size = new System.Drawing.Size(165, 28);
            this.CmbDolgozószám.Sorted = true;
            this.CmbDolgozószám.TabIndex = 99;
            this.CmbDolgozószám.SelectionChangeCommitted += new System.EventHandler(this.CmbDolgozószám_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 97;
            this.label3.Text = "Dolgozó szám";
            // 
            // UserId
            // 
            this.UserId.Location = new System.Drawing.Point(201, 3);
            this.UserId.MaxLength = 25;
            this.UserId.Name = "UserId";
            this.UserId.Size = new System.Drawing.Size(94, 26);
            this.UserId.TabIndex = 105;
            // 
            // Törölt
            // 
            this.Törölt.AutoSize = true;
            this.Törölt.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Törölt.Location = new System.Drawing.Point(201, 248);
            this.Törölt.Name = "Törölt";
            this.Törölt.Size = new System.Drawing.Size(68, 24);
            this.Törölt.TabIndex = 101;
            this.Törölt.Text = "Törölt";
            this.Törölt.UseVisualStyleBackColor = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 20);
            this.label4.TabIndex = 98;
            this.label4.Text = "Dolgozó név:";
            // 
            // CmbDolgozónév
            // 
            this.CmbDolgozónév.FormattingEnabled = true;
            this.CmbDolgozónév.Location = new System.Drawing.Point(201, 143);
            this.CmbDolgozónév.Name = "CmbDolgozónév";
            this.CmbDolgozónév.Size = new System.Drawing.Size(455, 28);
            this.CmbDolgozónév.Sorted = true;
            this.CmbDolgozónév.TabIndex = 104;
            this.CmbDolgozónév.SelectionChangeCommitted += new System.EventHandler(this.CmbDolgozónév_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 175);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 20);
            this.label6.TabIndex = 107;
            this.label6.Text = "Jelszó:";
            // 
            // TxtPassword
            // 
            this.TxtPassword.Location = new System.Drawing.Point(201, 178);
            this.TxtPassword.MaxLength = 50;
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(192, 26);
            this.TxtPassword.TabIndex = 108;
            this.TxtPassword.TextChanged += new System.EventHandler(this.TxtPassword_TextChanged);
            // 
            // Frissít
            // 
            this.Frissít.AutoSize = true;
            this.Frissít.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Frissít.Location = new System.Drawing.Point(201, 213);
            this.Frissít.Name = "Frissít";
            this.Frissít.Size = new System.Drawing.Size(229, 24);
            this.Frissít.TabIndex = 103;
            this.Frissít.Text = "Jelszó változtatási kötelezés";
            this.Frissít.UseVisualStyleBackColor = false;
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.FilterAndSortEnabled = true;
            this.Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.Location = new System.Drawing.Point(12, 365);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.Size = new System.Drawing.Size(1165, 260);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 221;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // lblSzervezet
            // 
            this.lblSzervezet.AutoSize = true;
            this.lblSzervezet.Location = new System.Drawing.Point(3, 0);
            this.lblSzervezet.Name = "lblSzervezet";
            this.lblSzervezet.Size = new System.Drawing.Size(117, 20);
            this.lblSzervezet.TabIndex = 225;
            this.lblSzervezet.Text = "Alap szervezet:";
            // 
            // ChkSzervezet
            // 
            this.ChkSzervezet.CheckOnClick = true;
            this.ChkSzervezet.FormattingEnabled = true;
            this.ChkSzervezet.Location = new System.Drawing.Point(3, 103);
            this.ChkSzervezet.Name = "ChkSzervezet";
            this.ChkSzervezet.Size = new System.Drawing.Size(265, 235);
            this.ChkSzervezet.TabIndex = 226;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel2.Controls.Add(this.BtnSugó, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnFrissít, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.BtnDolgozóilsta, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnÚj, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.BtnRögzít, 0, 4);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(965, 12);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 5;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 79F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(212, 347);
            this.tableLayoutPanel2.TabIndex = 227;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.CmbSzervezet, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.lblSzervezet, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.ChkSzervezet, 0, 3);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(688, 11);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 4;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(271, 348);
            this.tableLayoutPanel3.TabIndex = 228;
            // 
            // CmbSzervezet
            // 
            this.CmbSzervezet.FormattingEnabled = true;
            this.CmbSzervezet.Location = new System.Drawing.Point(3, 33);
            this.CmbSzervezet.MaxLength = 8;
            this.CmbSzervezet.Name = "CmbSzervezet";
            this.CmbSzervezet.Size = new System.Drawing.Size(265, 28);
            this.CmbSzervezet.Sorted = true;
            this.CmbSzervezet.TabIndex = 109;
            this.CmbSzervezet.SelectionChangeCommitted += new System.EventHandler(this.CmbSzervezet_SelectionChangeCommitted);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 20);
            this.label7.TabIndex = 227;
            this.label7.Text = "További szervezetek:";
            // 
            // Főadmin
            // 
            this.Főadmin.AutoSize = true;
            this.Főadmin.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Főadmin.Location = new System.Drawing.Point(201, 283);
            this.Főadmin.Name = "Főadmin";
            this.Főadmin.Size = new System.Drawing.Size(194, 24);
            this.Főadmin.TabIndex = 109;
            this.Főadmin.Text = "Program Adminisztrátor";
            this.Főadmin.UseVisualStyleBackColor = false;
            // 
            // TelephelyAdmin
            // 
            this.TelephelyAdmin.AutoSize = true;
            this.TelephelyAdmin.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.TelephelyAdmin.Location = new System.Drawing.Point(201, 318);
            this.TelephelyAdmin.Name = "TelephelyAdmin";
            this.TelephelyAdmin.Size = new System.Drawing.Size(204, 24);
            this.TelephelyAdmin.TabIndex = 110;
            this.TelephelyAdmin.Text = "Telephelyi Adminisztrátor";
            this.TelephelyAdmin.UseVisualStyleBackColor = false;
            // 
            // Ablak_Felhasználó
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1189, 637);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Felhasználó";
            this.Text = "Felhasználók karbantartása";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AblakFelhasználó_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }
        internal ToolTip ToolTip1;
        internal Button BtnSugó;
        internal Button BtnRögzít;
        internal TextBox TextWinUser;
        internal Label label2;
        internal TextBox TextUserNév;
        internal Label Label1;
        private TableLayoutPanel tableLayoutPanel1;
        internal Label label3;
        internal Label label4;
        private ComboBox CmbDolgozószám;
        private CheckBox Törölt;
        private Zuby.ADGV.AdvancedDataGridView Tábla;
        private CheckBox Frissít;
        private ComboBox CmbDolgozónév;
        internal Label label5;
        internal TextBox UserId;
        internal Button BtnÚj;
        internal Button BtnFrissít;
        internal Label label6;
        internal TextBox TxtPassword;
        internal Button BtnDolgozóilsta;
        internal Label lblSzervezet;
        internal CheckedListBox ChkSzervezet;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private ComboBox CmbSzervezet;
        internal Label label7;
        private CheckBox Főadmin;
        private CheckBox TelephelyAdmin;
    }
}