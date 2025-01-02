using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    public partial class Ablak_Munkalap_admin : Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Munkalap_admin));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.MunkafolyamatTábla = new System.Windows.Forms.DataGridView();
            this.PályaszámTextÚj = new System.Windows.Forms.TextBox();
            this.CseregombPsz = new System.Windows.Forms.Button();
            this.CsoportFel = new System.Windows.Forms.Button();
            this.Visszavon = new System.Windows.Forms.Button();
            this.RendelésiSzámúj = new System.Windows.Forms.TextBox();
            this.Cseregomb = new System.Windows.Forms.Button();
            this.Karbantartás = new System.Windows.Forms.Button();
            this.ÚjRögzítés = new System.Windows.Forms.Button();
            this.MunkafolyamatTörlés = new System.Windows.Forms.Button();
            this.RendelésRögzít = new System.Windows.Forms.Button();
            this.MunkafolyamatText = new System.Windows.Forms.TextBox();
            this.PályaszámText = new System.Windows.Forms.TextBox();
            this.RendelésiszámText = new System.Windows.Forms.TextBox();
            this.IDfolyamat = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.MunkarendTábla = new System.Windows.Forms.DataGridView();
            this.Button4 = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.MunkarendText = new System.Windows.Forms.TextBox();
            this.IDrend = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Üzem = new System.Windows.Forms.TextBox();
            this.Szolgálat = new System.Windows.Forms.TextBox();
            this.Költséghely = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.FejlécRögzít = new System.Windows.Forms.Button();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Button13 = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MunkafolyamatTábla)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MunkarendTábla)).BeginInit();
            this.TabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(12, 12);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(373, 33);
            this.Panel1.TabIndex = 53;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(175, 2);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(12, 5);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Controls.Add(this.TabPage3);
            this.Fülek.Location = new System.Drawing.Point(3, 51);
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(940, 439);
            this.Fülek.TabIndex = 55;
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.SandyBrown;
            this.TabPage1.Controls.Add(this.MunkafolyamatTábla);
            this.TabPage1.Controls.Add(this.PályaszámTextÚj);
            this.TabPage1.Controls.Add(this.CseregombPsz);
            this.TabPage1.Controls.Add(this.CsoportFel);
            this.TabPage1.Controls.Add(this.Visszavon);
            this.TabPage1.Controls.Add(this.RendelésiSzámúj);
            this.TabPage1.Controls.Add(this.Cseregomb);
            this.TabPage1.Controls.Add(this.Karbantartás);
            this.TabPage1.Controls.Add(this.ÚjRögzítés);
            this.TabPage1.Controls.Add(this.MunkafolyamatTörlés);
            this.TabPage1.Controls.Add(this.RendelésRögzít);
            this.TabPage1.Controls.Add(this.MunkafolyamatText);
            this.TabPage1.Controls.Add(this.PályaszámText);
            this.TabPage1.Controls.Add(this.RendelésiszámText);
            this.TabPage1.Controls.Add(this.IDfolyamat);
            this.TabPage1.Controls.Add(this.Label4);
            this.TabPage1.Controls.Add(this.Label3);
            this.TabPage1.Controls.Add(this.Label2);
            this.TabPage1.Controls.Add(this.Label1);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(932, 406);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Munkafolyamatok";
            // 
            // MunkafolyamatTábla
            // 
            this.MunkafolyamatTábla.AllowUserToAddRows = false;
            this.MunkafolyamatTábla.AllowUserToDeleteRows = false;
            this.MunkafolyamatTábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.MunkafolyamatTábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.MunkafolyamatTábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MunkafolyamatTábla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.MunkafolyamatTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MunkafolyamatTábla.EnableHeadersVisualStyles = false;
            this.MunkafolyamatTábla.Location = new System.Drawing.Point(5, 163);
            this.MunkafolyamatTábla.Name = "MunkafolyamatTábla";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MunkafolyamatTábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.MunkafolyamatTábla.RowHeadersVisible = false;
            this.MunkafolyamatTábla.RowHeadersWidth = 25;
            this.MunkafolyamatTábla.Size = new System.Drawing.Size(920, 237);
            this.MunkafolyamatTábla.TabIndex = 193;
            this.MunkafolyamatTábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MunkafolyamatTábla_CellClick);
            this.MunkafolyamatTábla.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MunkafolyamatTábla_CellDoubleClick);
            this.MunkafolyamatTábla.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.MunkafolyamatTábla_CellFormatting);
            // 
            // PályaszámTextÚj
            // 
            this.PályaszámTextÚj.Location = new System.Drawing.Point(399, 93);
            this.PályaszámTextÚj.MaxLength = 6;
            this.PályaszámTextÚj.Name = "PályaszámTextÚj";
            this.PályaszámTextÚj.Size = new System.Drawing.Size(195, 26);
            this.PályaszámTextÚj.TabIndex = 98;
            // 
            // CseregombPsz
            // 
            this.CseregombPsz.BackgroundImage = global::Villamos.Properties.Resources.page_swap_32;
            this.CseregombPsz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CseregombPsz.Location = new System.Drawing.Point(348, 84);
            this.CseregombPsz.Name = "CseregombPsz";
            this.CseregombPsz.Size = new System.Drawing.Size(45, 45);
            this.CseregombPsz.TabIndex = 97;
            this.ToolTip1.SetToolTip(this.CseregombPsz, "Pályaszám tömeges cseréjét végzi el.\r\nFeltétel, hogy mind a kettő mező tartalmazz" +
        "on értéket.");
            this.CseregombPsz.UseVisualStyleBackColor = true;
            this.CseregombPsz.Click += new System.EventHandler(this.CseregombPsz_Click);
            // 
            // CsoportFel
            // 
            this.CsoportFel.BackgroundImage = global::Villamos.Properties.Resources.Button_Upload_01;
            this.CsoportFel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsoportFel.Location = new System.Drawing.Point(816, 74);
            this.CsoportFel.Name = "CsoportFel";
            this.CsoportFel.Size = new System.Drawing.Size(45, 45);
            this.CsoportFel.TabIndex = 96;
            this.ToolTip1.SetToolTip(this.CsoportFel, "Sorrendben eggyel előrébb teszi");
            this.CsoportFel.UseVisualStyleBackColor = true;
            this.CsoportFel.Click += new System.EventHandler(this.CsoportFel_Click);
            // 
            // Visszavon
            // 
            this.Visszavon.BackgroundImage = global::Villamos.Properties.Resources.Mimetype_recycled;
            this.Visszavon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Visszavon.Location = new System.Drawing.Point(714, 74);
            this.Visszavon.Name = "Visszavon";
            this.Visszavon.Size = new System.Drawing.Size(45, 45);
            this.Visszavon.TabIndex = 95;
            this.ToolTip1.SetToolTip(this.Visszavon, "A törölt elemet visszaállítja érvényes státuszra.");
            this.Visszavon.UseVisualStyleBackColor = true;
            this.Visszavon.Click += new System.EventHandler(this.Visszavon_Click);
            // 
            // RendelésiSzámúj
            // 
            this.RendelésiSzámúj.Location = new System.Drawing.Point(399, 47);
            this.RendelésiSzámúj.MaxLength = 20;
            this.RendelésiSzámúj.Name = "RendelésiSzámúj";
            this.RendelésiSzámúj.Size = new System.Drawing.Size(195, 26);
            this.RendelésiSzámúj.TabIndex = 94;
            // 
            // Cseregomb
            // 
            this.Cseregomb.BackgroundImage = global::Villamos.Properties.Resources.page_swap_32;
            this.Cseregomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Cseregomb.Location = new System.Drawing.Point(348, 32);
            this.Cseregomb.Name = "Cseregomb";
            this.Cseregomb.Size = new System.Drawing.Size(45, 45);
            this.Cseregomb.TabIndex = 93;
            this.ToolTip1.SetToolTip(this.Cseregomb, "Rendelési szám tömeges cseréjét végzi el.\r\nFeltétel, hogy mind a kettő mező tarta" +
        "lmazzon értéket.");
            this.Cseregomb.UseVisualStyleBackColor = true;
            this.Cseregomb.Click += new System.EventHandler(this.Cseregomb_Click);
            // 
            // Karbantartás
            // 
            this.Karbantartás.BackgroundImage = global::Villamos.Properties.Resources.clear32;
            this.Karbantartás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Karbantartás.Location = new System.Drawing.Point(765, 74);
            this.Karbantartás.Name = "Karbantartás";
            this.Karbantartás.Size = new System.Drawing.Size(45, 45);
            this.Karbantartás.TabIndex = 92;
            this.ToolTip1.SetToolTip(this.Karbantartás, "Törölt tételeket kitörli az adatbázisból véglegesen.");
            this.Karbantartás.UseVisualStyleBackColor = true;
            this.Karbantartás.Click += new System.EventHandler(this.Karbantartás_Click);
            // 
            // ÚjRögzítés
            // 
            this.ÚjRögzítés.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.ÚjRögzítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ÚjRögzítés.Location = new System.Drawing.Point(612, 74);
            this.ÚjRögzítés.Name = "ÚjRögzítés";
            this.ÚjRögzítés.Size = new System.Drawing.Size(45, 45);
            this.ÚjRögzítés.TabIndex = 91;
            this.ToolTip1.SetToolTip(this.ÚjRögzítés, "Új adatoknak készíti elő a beviteli mezőket");
            this.ÚjRögzítés.UseVisualStyleBackColor = true;
            this.ÚjRögzítés.Click += new System.EventHandler(this.ÚjRögzítés_Click);
            // 
            // MunkafolyamatTörlés
            // 
            this.MunkafolyamatTörlés.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.MunkafolyamatTörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MunkafolyamatTörlés.Location = new System.Drawing.Point(663, 74);
            this.MunkafolyamatTörlés.Name = "MunkafolyamatTörlés";
            this.MunkafolyamatTörlés.Size = new System.Drawing.Size(45, 45);
            this.MunkafolyamatTörlés.TabIndex = 83;
            this.ToolTip1.SetToolTip(this.MunkafolyamatTörlés, "Törölt státusra állítja át a kijelölt elemet.");
            this.MunkafolyamatTörlés.UseVisualStyleBackColor = true;
            this.MunkafolyamatTörlés.Click += new System.EventHandler(this.MunkafolyamatTörlés_Click);
            // 
            // RendelésRögzít
            // 
            this.RendelésRögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.RendelésRögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.RendelésRögzít.Location = new System.Drawing.Point(881, 9);
            this.RendelésRögzít.Name = "RendelésRögzít";
            this.RendelésRögzít.Size = new System.Drawing.Size(45, 45);
            this.RendelésRögzít.TabIndex = 82;
            this.ToolTip1.SetToolTip(this.RendelésRögzít, "Rögzíti az adatokat.");
            this.RendelésRögzít.UseVisualStyleBackColor = true;
            this.RendelésRögzít.Click += new System.EventHandler(this.RendelésRögzít_Click);
            // 
            // MunkafolyamatText
            // 
            this.MunkafolyamatText.Location = new System.Drawing.Point(147, 131);
            this.MunkafolyamatText.MaxLength = 150;
            this.MunkafolyamatText.Name = "MunkafolyamatText";
            this.MunkafolyamatText.Size = new System.Drawing.Size(779, 26);
            this.MunkafolyamatText.TabIndex = 9;
            // 
            // PályaszámText
            // 
            this.PályaszámText.Location = new System.Drawing.Point(147, 93);
            this.PályaszámText.MaxLength = 6;
            this.PályaszámText.Name = "PályaszámText";
            this.PályaszámText.Size = new System.Drawing.Size(195, 26);
            this.PályaszámText.TabIndex = 8;
            // 
            // RendelésiszámText
            // 
            this.RendelésiszámText.Location = new System.Drawing.Point(147, 47);
            this.RendelésiszámText.MaxLength = 20;
            this.RendelésiszámText.Name = "RendelésiszámText";
            this.RendelésiszámText.Size = new System.Drawing.Size(195, 26);
            this.RendelésiszámText.TabIndex = 7;
            // 
            // IDfolyamat
            // 
            this.IDfolyamat.Enabled = false;
            this.IDfolyamat.Location = new System.Drawing.Point(147, 9);
            this.IDfolyamat.Name = "IDfolyamat";
            this.IDfolyamat.Size = new System.Drawing.Size(129, 26);
            this.IDfolyamat.TabIndex = 6;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(17, 137);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(121, 20);
            this.Label4.TabIndex = 5;
            this.Label4.Text = "Munkafolyamat:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(17, 99);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(89, 20);
            this.Label3.TabIndex = 4;
            this.Label3.Text = "Pályaszám:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(17, 53);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(126, 20);
            this.Label2.TabIndex = 3;
            this.Label2.Text = "Rendelési szám:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(17, 12);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(76, 20);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Sorszám:";
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.Peru;
            this.TabPage2.Controls.Add(this.MunkarendTábla);
            this.TabPage2.Controls.Add(this.Button4);
            this.TabPage2.Controls.Add(this.Button3);
            this.TabPage2.Controls.Add(this.MunkarendText);
            this.TabPage2.Controls.Add(this.IDrend);
            this.TabPage2.Controls.Add(this.Label6);
            this.TabPage2.Controls.Add(this.Label5);
            this.TabPage2.Controls.Add(this.Button2);
            this.TabPage2.Controls.Add(this.Button1);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(932, 406);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Munkarend adatok";
            // 
            // MunkarendTábla
            // 
            this.MunkarendTábla.AllowUserToAddRows = false;
            this.MunkarendTábla.AllowUserToDeleteRows = false;
            this.MunkarendTábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.MunkarendTábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.MunkarendTábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MunkarendTábla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.MunkarendTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MunkarendTábla.Location = new System.Drawing.Point(6, 108);
            this.MunkarendTábla.Name = "MunkarendTábla";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.MunkarendTábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.MunkarendTábla.RowHeadersVisible = false;
            this.MunkarendTábla.RowHeadersWidth = 25;
            this.MunkarendTábla.Size = new System.Drawing.Size(920, 292);
            this.MunkarendTábla.TabIndex = 192;
            this.MunkarendTábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MunkarendTábla_CellClick);
            this.MunkarendTábla.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.MunkarendTábla_CellDoubleClick);
            this.MunkarendTábla.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.MunkarendTábla_CellFormatting);
            // 
            // Button4
            // 
            this.Button4.BackgroundImage = global::Villamos.Properties.Resources.Mimetype_recycled;
            this.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button4.Location = new System.Drawing.Point(567, 57);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(45, 45);
            this.Button4.TabIndex = 96;
            this.ToolTip1.SetToolTip(this.Button4, "A törölt elemet visszaállítja érvényes státuszra.");
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button3
            // 
            this.Button3.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button3.Location = new System.Drawing.Point(516, 57);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(45, 45);
            this.Button3.TabIndex = 94;
            this.ToolTip1.SetToolTip(this.Button3, "Törölt státusra állítja át a kijelölt elemet.");
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // MunkarendText
            // 
            this.MunkarendText.Location = new System.Drawing.Point(130, 66);
            this.MunkarendText.MaxLength = 20;
            this.MunkarendText.Name = "MunkarendText";
            this.MunkarendText.Size = new System.Drawing.Size(222, 26);
            this.MunkarendText.TabIndex = 3;
            // 
            // IDrend
            // 
            this.IDrend.Enabled = false;
            this.IDrend.Location = new System.Drawing.Point(130, 15);
            this.IDrend.Name = "IDrend";
            this.IDrend.Size = new System.Drawing.Size(118, 26);
            this.IDrend.TabIndex = 2;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(17, 72);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(93, 20);
            this.Label6.TabIndex = 1;
            this.Label6.Text = "Munkarend:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(17, 21);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(76, 20);
            this.Label5.TabIndex = 0;
            this.Label5.Text = "Sorszám:";
            // 
            // Button2
            // 
            this.Button2.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button2.Location = new System.Drawing.Point(465, 57);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(45, 45);
            this.Button2.TabIndex = 92;
            this.ToolTip1.SetToolTip(this.Button2, "Új adatoknak készíti elő a beviteli mezőket");
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button1.Location = new System.Drawing.Point(465, 6);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(45, 45);
            this.Button1.TabIndex = 83;
            this.ToolTip1.SetToolTip(this.Button1, "Rögzíti az adatokat.");
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.Khaki;
            this.TabPage3.Controls.Add(this.Üzem);
            this.TabPage3.Controls.Add(this.Szolgálat);
            this.TabPage3.Controls.Add(this.Költséghely);
            this.TabPage3.Controls.Add(this.Label9);
            this.TabPage3.Controls.Add(this.Label8);
            this.TabPage3.Controls.Add(this.Label7);
            this.TabPage3.Controls.Add(this.FejlécRögzít);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(932, 406);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Szolgálat adatok";
            // 
            // Üzem
            // 
            this.Üzem.Location = new System.Drawing.Point(144, 111);
            this.Üzem.MaxLength = 30;
            this.Üzem.Name = "Üzem";
            this.Üzem.Size = new System.Drawing.Size(222, 26);
            this.Üzem.TabIndex = 87;
            // 
            // Szolgálat
            // 
            this.Szolgálat.Location = new System.Drawing.Point(144, 69);
            this.Szolgálat.MaxLength = 30;
            this.Szolgálat.Name = "Szolgálat";
            this.Szolgálat.Size = new System.Drawing.Size(222, 26);
            this.Szolgálat.TabIndex = 86;
            // 
            // Költséghely
            // 
            this.Költséghely.Location = new System.Drawing.Point(144, 27);
            this.Költséghely.MaxLength = 30;
            this.Költséghely.Name = "Költséghely";
            this.Költséghely.Size = new System.Drawing.Size(222, 26);
            this.Költséghely.TabIndex = 85;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(17, 117);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(55, 20);
            this.Label9.TabIndex = 2;
            this.Label9.Text = "Üzem:";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(17, 75);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(112, 20);
            this.Label8.TabIndex = 1;
            this.Label8.Text = "Szakszolgálat:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(17, 33);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(94, 20);
            this.Label7.TabIndex = 0;
            this.Label7.Text = "Költséghely:";
            // 
            // FejlécRögzít
            // 
            this.FejlécRögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.FejlécRögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FejlécRögzít.Location = new System.Drawing.Point(424, 27);
            this.FejlécRögzít.Name = "FejlécRögzít";
            this.FejlécRögzít.Size = new System.Drawing.Size(45, 45);
            this.FejlécRögzít.TabIndex = 84;
            this.ToolTip1.SetToolTip(this.FejlécRögzít, "Rögzíti az adatokat.");
            this.FejlécRögzít.UseVisualStyleBackColor = true;
            this.FejlécRögzít.Click += new System.EventHandler(this.FejlécRögzít_Click);
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(421, 15);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(130, 26);
            this.Dátum.TabIndex = 56;
            this.Dátum.ValueChanged += new System.EventHandler(this.Dátum_ValueChanged);
            // 
            // Button13
            // 
            this.Button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button13.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Button13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button13.Location = new System.Drawing.Point(890, 5);
            this.Button13.Name = "Button13";
            this.Button13.Size = new System.Drawing.Size(45, 45);
            this.Button13.TabIndex = 54;
            this.Button13.UseVisualStyleBackColor = true;
            this.Button13.Click += new System.EventHandler(this.Button13_Click);
            // 
            // Ablak_Munkalap_admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(947, 487);
            this.Controls.Add(this.Dátum);
            this.Controls.Add(this.Fülek);
            this.Controls.Add(this.Button13);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Munkalap_admin";
            this.Text = "Munkalap adatok karbantartása";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Munkalap_admin_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MunkafolyamatTábla)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MunkarendTábla)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        internal Button Button13;
        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal TabControl Fülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TextBox MunkafolyamatText;
        internal TextBox PályaszámText;
        internal TextBox RendelésiszámText;
        internal TextBox IDfolyamat;
        internal Label Label4;
        internal Label Label3;
        internal Label Label2;
        internal Label Label1;
        internal TabPage TabPage3;
        internal DateTimePicker Dátum;
        internal Button MunkafolyamatTörlés;
        internal Button RendelésRögzít;
        internal Button ÚjRögzítés;
        internal Button Karbantartás;
        internal TextBox RendelésiSzámúj;
        internal Button Cseregomb;
        internal Button Visszavon;
        internal ToolTip ToolTip1;
        internal TextBox PályaszámTextÚj;
        internal Button CseregombPsz;
        internal Button CsoportFel;
        internal Button Button2;
        internal Button Button1;
        internal TextBox MunkarendText;
        internal TextBox IDrend;
        internal Label Label6;
        internal Label Label5;
        internal TextBox Üzem;
        internal TextBox Szolgálat;
        internal TextBox Költséghely;
        internal Button FejlécRögzít;
        internal Label Label9;
        internal Label Label8;
        internal Label Label7;
        internal Button Button4;
        internal Button Button3;
        internal DataGridView MunkarendTábla;
        internal DataGridView MunkafolyamatTábla;
    }
}