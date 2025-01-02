using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{

    public partial class AblakFelhasználó : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components  != null)
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AblakFelhasználó));
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Felhasználómásolása = new System.Windows.Forms.Button();
            this.Win_Rögzít = new System.Windows.Forms.Button();
            this.WinUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Panel_titok = new System.Windows.Forms.Panel();
            this.CMBMireSzemélyes = new System.Windows.Forms.CheckedListBox();
            this.Button1 = new System.Windows.Forms.Button();
            this.Btn_Bezár = new System.Windows.Forms.Button();
            this.Chk_Insert = new System.Windows.Forms.CheckBox();
            this.Chk_PageUp = new System.Windows.Forms.CheckBox();
            this.Chk_Shift = new System.Windows.Forms.CheckBox();
            this.Chk_Enter = new System.Windows.Forms.CheckBox();
            this.Chk_CTRL = new System.Windows.Forms.CheckBox();
            this.Btnalapjogosultság = new System.Windows.Forms.Button();
            this.BtnVendég = new System.Windows.Forms.Button();
            this.BtnÚjjelszó = new System.Windows.Forms.Button();
            this.BtnDolgozótörlés = new System.Windows.Forms.Button();
            this.BtnÚjdolgozó = new System.Windows.Forms.Button();
            this.Listtételek = new System.Windows.Forms.ListBox();
            this.TextNév = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.Kereső = new System.Windows.Forms.Button();
            this.lblnév = new System.Windows.Forms.Label();
            this.BtnJogosultság = new System.Windows.Forms.Button();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BtnSugó = new System.Windows.Forms.Button();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            this.Panel_titok.SuspendLayout();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Location = new System.Drawing.Point(11, 41);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1038, 448);
            this.Fülek.TabIndex = 0;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Lapfülek_DrawItem);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.LimeGreen;
            this.TabPage1.Controls.Add(this.Felhasználómásolása);
            this.TabPage1.Controls.Add(this.Win_Rögzít);
            this.TabPage1.Controls.Add(this.WinUser);
            this.TabPage1.Controls.Add(this.label2);
            this.TabPage1.Controls.Add(this.Panel_titok);
            this.TabPage1.Controls.Add(this.Btnalapjogosultság);
            this.TabPage1.Controls.Add(this.BtnVendég);
            this.TabPage1.Controls.Add(this.BtnÚjjelszó);
            this.TabPage1.Controls.Add(this.BtnDolgozótörlés);
            this.TabPage1.Controls.Add(this.BtnÚjdolgozó);
            this.TabPage1.Controls.Add(this.Listtételek);
            this.TabPage1.Controls.Add(this.TextNév);
            this.TabPage1.Controls.Add(this.Label1);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1030, 415);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Felhasználók";
            // 
            // Felhasználómásolása
            // 
            this.Felhasználómásolása.BackgroundImage = global::Villamos.Properties.Resources.Document_Copy_01;
            this.Felhasználómásolása.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Felhasználómásolása.Location = new System.Drawing.Point(501, 289);
            this.Felhasználómásolása.Name = "Felhasználómásolása";
            this.Felhasználómásolása.Size = new System.Drawing.Size(50, 50);
            this.Felhasználómásolása.TabIndex = 86;
            this.ToolTip1.SetToolTip(this.Felhasználómásolása, "Kiválaszott felhasználó  jogosultságainak másolása");
            this.Felhasználómásolása.UseVisualStyleBackColor = true;
            this.Felhasználómásolása.Click += new System.EventHandler(this.Felhasználómásolása_Click);
            // 
            // Win_Rögzít
            // 
            this.Win_Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Win_Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Win_Rögzít.Location = new System.Drawing.Point(718, 3);
            this.Win_Rögzít.Name = "Win_Rögzít";
            this.Win_Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Win_Rögzít.TabIndex = 85;
            this.ToolTip1.SetToolTip(this.Win_Rögzít, "Hozzákötjük a felhasználónévhez  a Windows profilt");
            this.Win_Rögzít.UseVisualStyleBackColor = true;
            this.Win_Rögzít.Click += new System.EventHandler(this.Win_Rögzít_Click);
            // 
            // WinUser
            // 
            this.WinUser.Location = new System.Drawing.Point(533, 8);
            this.WinUser.MaxLength = 15;
            this.WinUser.Name = "WinUser";
            this.WinUser.Size = new System.Drawing.Size(165, 26);
            this.WinUser.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(335, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Windows Felhasználónév:";
            // 
            // Panel_titok
            // 
            this.Panel_titok.BackColor = System.Drawing.Color.ForestGreen;
            this.Panel_titok.Controls.Add(this.CMBMireSzemélyes);
            this.Panel_titok.Controls.Add(this.Button1);
            this.Panel_titok.Controls.Add(this.Btn_Bezár);
            this.Panel_titok.Controls.Add(this.Chk_Insert);
            this.Panel_titok.Controls.Add(this.Chk_PageUp);
            this.Panel_titok.Controls.Add(this.Chk_Shift);
            this.Panel_titok.Controls.Add(this.Chk_Enter);
            this.Panel_titok.Controls.Add(this.Chk_CTRL);
            this.Panel_titok.Location = new System.Drawing.Point(533, 70);
            this.Panel_titok.Name = "Panel_titok";
            this.Panel_titok.Size = new System.Drawing.Size(469, 178);
            this.Panel_titok.TabIndex = 9;
            this.Panel_titok.Visible = false;
            // 
            // CMBMireSzemélyes
            // 
            this.CMBMireSzemélyes.CheckOnClick = true;
            this.CMBMireSzemélyes.FormattingEnabled = true;
            this.CMBMireSzemélyes.Location = new System.Drawing.Point(122, 15);
            this.CMBMireSzemélyes.Name = "CMBMireSzemélyes";
            this.CMBMireSzemélyes.Size = new System.Drawing.Size(218, 151);
            this.CMBMireSzemélyes.TabIndex = 84;
            // 
            // Button1
            // 
            this.Button1.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Button1.Location = new System.Drawing.Point(363, 15);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(45, 45);
            this.Button1.TabIndex = 26;
            this.ToolTip1.SetToolTip(this.Button1, "Jogosultságok rögzítése");
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Btn_Bezár
            // 
            this.Btn_Bezár.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Bezár.BackgroundImage = global::Villamos.Properties.Resources.bezár;
            this.Btn_Bezár.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btn_Bezár.Location = new System.Drawing.Point(421, 0);
            this.Btn_Bezár.Name = "Btn_Bezár";
            this.Btn_Bezár.Size = new System.Drawing.Size(45, 45);
            this.Btn_Bezár.TabIndex = 24;
            this.ToolTip1.SetToolTip(this.Btn_Bezár, "Bezárja az ablakot");
            this.Btn_Bezár.UseVisualStyleBackColor = true;
            this.Btn_Bezár.Click += new System.EventHandler(this.Btn_Bezár_Click);
            // 
            // Chk_Insert
            // 
            this.Chk_Insert.AutoSize = true;
            this.Chk_Insert.Location = new System.Drawing.Point(15, 137);
            this.Chk_Insert.Name = "Chk_Insert";
            this.Chk_Insert.Size = new System.Drawing.Size(69, 24);
            this.Chk_Insert.TabIndex = 23;
            this.Chk_Insert.Text = "Insert";
            this.Chk_Insert.UseVisualStyleBackColor = true;
            // 
            // Chk_PageUp
            // 
            this.Chk_PageUp.AutoSize = true;
            this.Chk_PageUp.Location = new System.Drawing.Point(15, 104);
            this.Chk_PageUp.Name = "Chk_PageUp";
            this.Chk_PageUp.Size = new System.Drawing.Size(90, 24);
            this.Chk_PageUp.TabIndex = 22;
            this.Chk_PageUp.Text = "Page Up";
            this.Chk_PageUp.UseVisualStyleBackColor = true;
            // 
            // Chk_Shift
            // 
            this.Chk_Shift.AutoSize = true;
            this.Chk_Shift.Location = new System.Drawing.Point(16, 44);
            this.Chk_Shift.Name = "Chk_Shift";
            this.Chk_Shift.Size = new System.Drawing.Size(61, 24);
            this.Chk_Shift.TabIndex = 21;
            this.Chk_Shift.Text = "Shift";
            this.Chk_Shift.UseVisualStyleBackColor = true;
            // 
            // Chk_Enter
            // 
            this.Chk_Enter.AutoSize = true;
            this.Chk_Enter.Location = new System.Drawing.Point(16, 74);
            this.Chk_Enter.Name = "Chk_Enter";
            this.Chk_Enter.Size = new System.Drawing.Size(57, 24);
            this.Chk_Enter.TabIndex = 20;
            this.Chk_Enter.Text = "End";
            this.Chk_Enter.UseVisualStyleBackColor = true;
            // 
            // Chk_CTRL
            // 
            this.Chk_CTRL.AutoSize = true;
            this.Chk_CTRL.Location = new System.Drawing.Point(16, 14);
            this.Chk_CTRL.Name = "Chk_CTRL";
            this.Chk_CTRL.Size = new System.Drawing.Size(69, 24);
            this.Chk_CTRL.TabIndex = 19;
            this.Chk_CTRL.Text = "CTRL";
            this.Chk_CTRL.UseVisualStyleBackColor = true;
            // 
            // Btnalapjogosultság
            // 
            this.Btnalapjogosultság.BackgroundImage = global::Villamos.Properties.Resources.user_mindenjogtöröl;
            this.Btnalapjogosultság.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Btnalapjogosultság.Location = new System.Drawing.Point(420, 289);
            this.Btnalapjogosultság.Name = "Btnalapjogosultság";
            this.Btnalapjogosultság.Size = new System.Drawing.Size(50, 50);
            this.Btnalapjogosultság.TabIndex = 7;
            this.ToolTip1.SetToolTip(this.Btnalapjogosultság, "Minden jogosultság törlése");
            this.Btnalapjogosultság.UseVisualStyleBackColor = true;
            this.Btnalapjogosultság.Click += new System.EventHandler(this.Btnalapjogosultság_Click);
            // 
            // BtnVendég
            // 
            this.BtnVendég.BackgroundImage = global::Villamos.Properties.Resources.user_vendég1;
            this.BtnVendég.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnVendég.Location = new System.Drawing.Point(339, 289);
            this.BtnVendég.Name = "BtnVendég";
            this.BtnVendég.Size = new System.Drawing.Size(50, 50);
            this.BtnVendég.TabIndex = 6;
            this.ToolTip1.SetToolTip(this.BtnVendég, "Vendég jogosultságainak másolása");
            this.BtnVendég.UseVisualStyleBackColor = true;
            this.BtnVendég.Click += new System.EventHandler(this.BtnVendég_Click);
            // 
            // BtnÚjjelszó
            // 
            this.BtnÚjjelszó.BackgroundImage = global::Villamos.Properties.Resources.user_accept_256;
            this.BtnÚjjelszó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnÚjjelszó.Location = new System.Drawing.Point(339, 158);
            this.BtnÚjjelszó.Name = "BtnÚjjelszó";
            this.BtnÚjjelszó.Size = new System.Drawing.Size(50, 50);
            this.BtnÚjjelszó.TabIndex = 5;
            this.ToolTip1.SetToolTip(this.BtnÚjjelszó, "A Jelszó beállítása INIT- re");
            this.BtnÚjjelszó.UseVisualStyleBackColor = true;
            this.BtnÚjjelszó.Click += new System.EventHandler(this.BtnÚjjelszó_Click);
            // 
            // BtnDolgozótörlés
            // 
            this.BtnDolgozótörlés.BackgroundImage = global::Villamos.Properties.Resources.user_remove_256_64;
            this.BtnDolgozótörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnDolgozótörlés.Location = new System.Drawing.Point(420, 49);
            this.BtnDolgozótörlés.Name = "BtnDolgozótörlés";
            this.BtnDolgozótörlés.Size = new System.Drawing.Size(50, 50);
            this.BtnDolgozótörlés.TabIndex = 4;
            this.ToolTip1.SetToolTip(this.BtnDolgozótörlés, "Felhasználó törlés");
            this.BtnDolgozótörlés.UseVisualStyleBackColor = true;
            this.BtnDolgozótörlés.Click += new System.EventHandler(this.BtnDolgozótörlés_Click);
            // 
            // BtnÚjdolgozó
            // 
            this.BtnÚjdolgozó.BackgroundImage = global::Villamos.Properties.Resources.user_add_256;
            this.BtnÚjdolgozó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnÚjdolgozó.Location = new System.Drawing.Point(339, 49);
            this.BtnÚjdolgozó.Name = "BtnÚjdolgozó";
            this.BtnÚjdolgozó.Size = new System.Drawing.Size(50, 50);
            this.BtnÚjdolgozó.TabIndex = 3;
            this.ToolTip1.SetToolTip(this.BtnÚjdolgozó, "Új Felhasználó létrehozása");
            this.BtnÚjdolgozó.UseVisualStyleBackColor = true;
            this.BtnÚjdolgozó.Click += new System.EventHandler(this.BtnÚjdolgozó_Click);
            // 
            // Listtételek
            // 
            this.Listtételek.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Listtételek.FormattingEnabled = true;
            this.Listtételek.ItemHeight = 20;
            this.Listtételek.Location = new System.Drawing.Point(138, 49);
            this.Listtételek.Name = "Listtételek";
            this.Listtételek.Size = new System.Drawing.Size(165, 344);
            this.Listtételek.TabIndex = 2;
            this.Listtételek.SelectedIndexChanged += new System.EventHandler(this.Listtételek_SelectedIndexChanged);
            // 
            // TextNév
            // 
            this.TextNév.Location = new System.Drawing.Point(138, 8);
            this.TextNév.MaxLength = 15;
            this.TextNév.Name = "TextNév";
            this.TextNév.Size = new System.Drawing.Size(165, 26);
            this.TextNév.TabIndex = 1;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(8, 14);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(124, 20);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Felhasználónév:";
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.LimeGreen;
            this.TabPage2.Controls.Add(this.Kereső);
            this.TabPage2.Controls.Add(this.lblnév);
            this.TabPage2.Controls.Add(this.BtnJogosultság);
            this.TabPage2.Controls.Add(this.Tábla);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1030, 415);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Jogosultságok";
            // 
            // Kereső
            // 
            this.Kereső.BackgroundImage = global::Villamos.Properties.Resources.Nagyító;
            this.Kereső.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Kereső.Location = new System.Drawing.Point(738, 6);
            this.Kereső.Name = "Kereső";
            this.Kereső.Size = new System.Drawing.Size(45, 45);
            this.Kereső.TabIndex = 4;
            this.ToolTip1.SetToolTip(this.Kereső, "Megkeressük a szöveget a táblázatban.");
            this.Kereső.UseVisualStyleBackColor = true;
            this.Kereső.Click += new System.EventHandler(this.Kereső_Click);
            // 
            // lblnév
            // 
            this.lblnév.AutoSize = true;
            this.lblnév.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblnév.Location = new System.Drawing.Point(125, 19);
            this.lblnév.Name = "lblnév";
            this.lblnév.Size = new System.Drawing.Size(0, 20);
            this.lblnév.TabIndex = 3;
            // 
            // BtnJogosultság
            // 
            this.BtnJogosultság.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.BtnJogosultság.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnJogosultság.Location = new System.Drawing.Point(687, 7);
            this.BtnJogosultság.Name = "BtnJogosultság";
            this.BtnJogosultság.Size = new System.Drawing.Size(45, 45);
            this.BtnJogosultság.TabIndex = 1;
            this.ToolTip1.SetToolTip(this.BtnJogosultság, "Jogosultságok rögzítése");
            this.BtnJogosultság.UseVisualStyleBackColor = true;
            this.BtnJogosultság.Click += new System.EventHandler(this.BtnJogosultság_Click);
            // 
            // Tábla
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(6, 55);
            this.Tábla.Name = "Tábla";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGreen;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(1018, 354);
            this.Tábla.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Column7";
            this.Column7.Name = "Column7";
            // 
            // ToolTip1
            // 
            this.ToolTip1.IsBalloon = true;
            // 
            // BtnSugó
            // 
            this.BtnSugó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSugó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSugó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.BtnSugó.Location = new System.Drawing.Point(1004, 3);
            this.BtnSugó.Name = "BtnSugó";
            this.BtnSugó.Size = new System.Drawing.Size(45, 45);
            this.BtnSugó.TabIndex = 2;
            this.ToolTip1.SetToolTip(this.BtnSugó, "Online sugó megjelenítése");
            this.BtnSugó.UseVisualStyleBackColor = true;
            this.BtnSugó.Click += new System.EventHandler(this.BtnSugó_Click);
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(4, 1);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(380, 36);
            this.Panel1.TabIndex = 46;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(149, 5);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(228, 28);
            this.Cmbtelephely.TabIndex = 19;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 4);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // AblakFelhasználó
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 495);
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.BtnSugó);
            this.Controls.Add(this.Fülek);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AblakFelhasználó";
            this.Text = "Felhasználók karbantartása";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AblakFelhasználó_FormClosed);
            this.Load += new System.EventHandler(this.AblakFelhasználó_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AblakFelhasználó_KeyDown);
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            this.Panel_titok.ResumeLayout(false);
            this.Panel_titok.PerformLayout();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        internal TabControl Fülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal Label Label1;
        internal TextBox TextNév;
        internal ListBox Listtételek;
        internal Button Btnalapjogosultság;
        internal Button BtnVendég;
        internal Button BtnÚjjelszó;
        internal Button BtnDolgozótörlés;
        internal Button BtnÚjdolgozó;
        internal Button BtnJogosultság;
        internal DataGridView Tábla;
        internal DataGridViewTextBoxColumn Column1;
        internal DataGridViewTextBoxColumn Column2;
        internal DataGridViewTextBoxColumn Column3;
        internal DataGridViewCheckBoxColumn Column4;
        internal DataGridViewCheckBoxColumn Column5;
        internal DataGridViewCheckBoxColumn Column6;
        internal DataGridViewCheckBoxColumn Column7;
        internal ToolTip ToolTip1;
        internal Button BtnSugó;
        internal Label lblnév;
        internal Panel Panel1;
        internal Label Label13;
        internal Panel Panel_titok;
        internal CheckBox Chk_Enter;
        internal CheckBox Chk_CTRL;
        internal CheckBox Chk_Insert;
        internal CheckBox Chk_PageUp;
        internal CheckBox Chk_Shift;
        internal Button Btn_Bezár;
        internal Button Button1;
        internal CheckedListBox CMBMireSzemélyes;
        internal ComboBox Cmbtelephely;
        internal Button Win_Rögzít;
        internal TextBox WinUser;
        internal Label label2;
        internal Button Kereső;
        internal Button Felhasználómásolása;
    }
}