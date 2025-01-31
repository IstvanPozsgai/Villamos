using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    public partial class Ablak_Váltós : Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Váltós));
            this.Fülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.BEOkódFriss = new System.Windows.Forms.Button();
            this.Tábla_BeoKód = new System.Windows.Forms.DataGridView();
            this.Végeidő = new System.Windows.Forms.DateTimePicker();
            this.Kezdőidő = new System.Windows.Forms.DateTimePicker();
            this.Túlóra = new System.Windows.Forms.TextBox();
            this.Beosztáskód = new System.Windows.Forms.TextBox();
            this.Túlóraoka = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Telephely = new System.Windows.Forms.ComboBox();
            this.Label57 = new System.Windows.Forms.Label();
            this.Tábla_BeoKód_Új = new System.Windows.Forms.Button();
            this.Tábla_BeoKód_Töröl = new System.Windows.Forms.Button();
            this.Tábla_BeoKód_OK = new System.Windows.Forms.Button();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.TúlóraFrissít = new System.Windows.Forms.Button();
            this.Label9 = new System.Windows.Forms.Label();
            this.Tábla_Keret = new System.Windows.Forms.DataGridView();
            this.Túlparancs = new System.Windows.Forms.TextBox();
            this.Túlhatár = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Túltelephely = new System.Windows.Forms.ComboBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Tábla_Keret_Új = new System.Windows.Forms.Button();
            this.Tábla_Keret_Töröl = new System.Windows.Forms.Button();
            this.Tábla_Keret_OK = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.Éves_Frissít = new System.Windows.Forms.Button();
            this.ÉvesTperc = new System.Windows.Forms.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
            this.ÉvesEPnap = new System.Windows.Forms.TextBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.ÉvesFélév = new System.Windows.Forms.TextBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.ÉvesCsoport = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.ÉvesZKnap = new System.Windows.Forms.TextBox();
            this.ÉvesÉv = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.ÉvesTelephely = new System.Windows.Forms.ComboBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.Éves_Tábla = new System.Windows.Forms.DataGridView();
            this.Éves_Generál = new System.Windows.Forms.Button();
            this.Éves_Új = new System.Windows.Forms.Button();
            this.Éves_Töröl = new System.Windows.Forms.Button();
            this.Éves_Ok = new System.Windows.Forms.Button();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Tábla_Munkarend = new System.Windows.Forms.DataGridView();
            this.MunkaRend_OK = new System.Windows.Forms.Button();
            this.MunkaRend_Töröl = new System.Windows.Forms.Button();
            this.Munkarendelnevezés = new System.Windows.Forms.TextBox();
            this.Munkaidő = new System.Windows.Forms.TextBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label24 = new System.Windows.Forms.Label();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Csoport_Töröl = new System.Windows.Forms.Button();
            this.Csoport_Tábla = new System.Windows.Forms.DataGridView();
            this.Kezdődátum = new System.Windows.Forms.DateTimePicker();
            this.CsoportCombo = new System.Windows.Forms.ComboBox();
            this.Label22 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label19 = new System.Windows.Forms.Label();
            this.Ciklusnap = new System.Windows.Forms.TextBox();
            this.MegnevezésText = new System.Windows.Forms.TextBox();
            this.Id = new System.Windows.Forms.TextBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.Csoport_OK = new System.Windows.Forms.Button();
            this.Label17 = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.TurnusokLista = new System.Windows.Forms.ListBox();
            this.TurnusText = new System.Windows.Forms.TextBox();
            this.Turnus_Ok = new System.Windows.Forms.Button();
            this.Turnus_Töröl = new System.Windows.Forms.Button();
            this.TabPage5 = new System.Windows.Forms.TabPage();
            this.Nappaloslenyíló = new System.Windows.Forms.ComboBox();
            this.Panel4 = new System.Windows.Forms.Panel();
            this.Nappal_Ok = new System.Windows.Forms.Button();
            this.Nappalos_Excel = new System.Windows.Forms.Button();
            this.Tábla_Nappalos = new System.Windows.Forms.DataGridView();
            this.Választott = new System.Windows.Forms.TextBox();
            this.Nappal_Számol = new System.Windows.Forms.Button();
            this.Nappal_Alap = new System.Windows.Forms.Button();
            this.NappaloS_Tábla_Friss = new System.Windows.Forms.Button();
            this.Dátumnappal = new System.Windows.Forms.DateTimePicker();
            this.Label25 = new System.Windows.Forms.Label();
            this.TabPage6 = new System.Windows.Forms.TabPage();
            this.VáltósLenyíló = new System.Windows.Forms.ComboBox();
            this.Panel5 = new System.Windows.Forms.Panel();
            this.Command30 = new System.Windows.Forms.Button();
            this.Excelkészítés = new System.Windows.Forms.Button();
            this.VváltósCsoport = new System.Windows.Forms.ComboBox();
            this.Label27 = new System.Windows.Forms.Label();
            this.Tábla9 = new System.Windows.Forms.DataGridView();
            this.VálasztottVáltó = new System.Windows.Forms.TextBox();
            this.Command33 = new System.Windows.Forms.Button();
            this.Command36 = new System.Windows.Forms.Button();
            this.Command34 = new System.Windows.Forms.Button();
            this.VáltósNaptár = new System.Windows.Forms.DateTimePicker();
            this.Label26 = new System.Windows.Forms.Label();
            this.TabPage7 = new System.Windows.Forms.TabPage();
            this.ElvontCsoport = new System.Windows.Forms.ComboBox();
            this.ElvontDátum = new System.Windows.Forms.DateTimePicker();
            this.ElvontTelephely = new System.Windows.Forms.ComboBox();
            this.Elvont_Frissít = new System.Windows.Forms.Button();
            this.ElvontÉv = new System.Windows.Forms.TextBox();
            this.Elvont_Generált = new System.Windows.Forms.Button();
            this.Elvont_Új = new System.Windows.Forms.Button();
            this.Elvont_Töröl = new System.Windows.Forms.Button();
            this.Elvont_OK = new System.Windows.Forms.Button();
            this.Label33 = new System.Windows.Forms.Label();
            this.Label32 = new System.Windows.Forms.Label();
            this.Label31 = new System.Windows.Forms.Label();
            this.Label30 = new System.Windows.Forms.Label();
            this.Label29 = new System.Windows.Forms.Label();
            this.Label28 = new System.Windows.Forms.Label();
            this.SzűrtTelephely = new System.Windows.Forms.ComboBox();
            this.Tábla_Elvont = new System.Windows.Forms.DataGridView();
            this.TabPage8 = new System.Windows.Forms.TabPage();
            this.Tábla_VáltMunka = new System.Windows.Forms.DataGridView();
            this.VáltMunka_Feljebb = new System.Windows.Forms.Button();
            this.VáltMunka_Új = new System.Windows.Forms.Button();
            this.VáltMunka_Töröl = new System.Windows.Forms.Button();
            this.VáltMunka_OK = new System.Windows.Forms.Button();
            this.BeosztásSzöveg = new System.Windows.Forms.TextBox();
            this.VáltMunkBeoKód = new System.Windows.Forms.TextBox();
            this.Label36 = new System.Windows.Forms.Label();
            this.Hétnapja = new System.Windows.Forms.TextBox();
            this.Label34 = new System.Windows.Forms.Label();
            this.Label35 = new System.Windows.Forms.Label();
            this.TabPage10 = new System.Windows.Forms.TabPage();
            this.Tábla_Éjszaka = new System.Windows.Forms.DataGridView();
            this.Éjszaka_Feljebb = new System.Windows.Forms.Button();
            this.Éjszaka_ÚJ = new System.Windows.Forms.Button();
            this.Éjszaka_Töröl = new System.Windows.Forms.Button();
            this.Éjszaka_Ok = new System.Windows.Forms.Button();
            this.ÉBeosztásSzöveg = new System.Windows.Forms.TextBox();
            this.ÉBeoKód = new System.Windows.Forms.TextBox();
            this.Label40 = new System.Windows.Forms.Label();
            this.ÉhétNapja = new System.Windows.Forms.TextBox();
            this.Label41 = new System.Windows.Forms.Label();
            this.Label42 = new System.Windows.Forms.Label();
            this.TabPage9 = new System.Windows.Forms.TabPage();
            this.CsoportVáltóCsop = new System.Windows.Forms.ComboBox();
            this.TelephelyVáltóCsop = new System.Windows.Forms.ComboBox();
            this.Tábla_CsopVez = new System.Windows.Forms.DataGridView();
            this.CsopVez_Töröl = new System.Windows.Forms.Button();
            this.CsopVez_Ok = new System.Windows.Forms.Button();
            this.CsopVezNév = new System.Windows.Forms.TextBox();
            this.Label37 = new System.Windows.Forms.Label();
            this.Label38 = new System.Windows.Forms.Label();
            this.Label39 = new System.Windows.Forms.Label();
            this.Chk_CTRL = new System.Windows.Forms.CheckBox();
            this.Button13 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.Fülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_BeoKód)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Keret)).BeginInit();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Éves_Tábla)).BeginInit();
            this.TabPage4.SuspendLayout();
            this.Panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Munkarend)).BeginInit();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Csoport_Tábla)).BeginInit();
            this.Panel1.SuspendLayout();
            this.TabPage5.SuspendLayout();
            this.Panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Nappalos)).BeginInit();
            this.TabPage6.SuspendLayout();
            this.Panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla9)).BeginInit();
            this.TabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Elvont)).BeginInit();
            this.TabPage8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_VáltMunka)).BeginInit();
            this.TabPage10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Éjszaka)).BeginInit();
            this.TabPage9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_CsopVez)).BeginInit();
            this.SuspendLayout();
            // 
            // Fülek
            // 
            this.Fülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Fülek.Controls.Add(this.TabPage1);
            this.Fülek.Controls.Add(this.TabPage2);
            this.Fülek.Controls.Add(this.TabPage3);
            this.Fülek.Controls.Add(this.TabPage4);
            this.Fülek.Controls.Add(this.TabPage5);
            this.Fülek.Controls.Add(this.TabPage6);
            this.Fülek.Controls.Add(this.TabPage7);
            this.Fülek.Controls.Add(this.TabPage8);
            this.Fülek.Controls.Add(this.TabPage10);
            this.Fülek.Controls.Add(this.TabPage9);
            this.Fülek.Location = new System.Drawing.Point(1, 56);
            this.Fülek.Multiline = true;
            this.Fülek.Name = "Fülek";
            this.Fülek.Padding = new System.Drawing.Point(16, 3);
            this.Fülek.SelectedIndex = 0;
            this.Fülek.Size = new System.Drawing.Size(1140, 410);
            this.Fülek.TabIndex = 63;
            this.toolTip1.SetToolTip(this.Fülek, "Éves naptárat készít");
            this.Fülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Fülek_DrawItem);
            this.Fülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.TabPage1.Controls.Add(this.BEOkódFriss);
            this.TabPage1.Controls.Add(this.Tábla_BeoKód);
            this.TabPage1.Controls.Add(this.Végeidő);
            this.TabPage1.Controls.Add(this.Kezdőidő);
            this.TabPage1.Controls.Add(this.Túlóra);
            this.TabPage1.Controls.Add(this.Beosztáskód);
            this.TabPage1.Controls.Add(this.Túlóraoka);
            this.TabPage1.Controls.Add(this.Label5);
            this.TabPage1.Controls.Add(this.Label4);
            this.TabPage1.Controls.Add(this.Label3);
            this.TabPage1.Controls.Add(this.Label2);
            this.TabPage1.Controls.Add(this.Label1);
            this.TabPage1.Controls.Add(this.Telephely);
            this.TabPage1.Controls.Add(this.Label57);
            this.TabPage1.Controls.Add(this.Tábla_BeoKód_Új);
            this.TabPage1.Controls.Add(this.Tábla_BeoKód_Töröl);
            this.TabPage1.Controls.Add(this.Tábla_BeoKód_OK);
            this.TabPage1.Location = new System.Drawing.Point(4, 54);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1132, 352);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Beosztás kód";
            // 
            // BEOkódFriss
            // 
            this.BEOkódFriss.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.BEOkódFriss.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BEOkódFriss.Location = new System.Drawing.Point(616, 91);
            this.BEOkódFriss.Name = "BEOkódFriss";
            this.BEOkódFriss.Size = new System.Drawing.Size(45, 45);
            this.BEOkódFriss.TabIndex = 76;
            this.toolTip1.SetToolTip(this.BEOkódFriss, "Frissíti a táblázatot");
            this.BEOkódFriss.UseVisualStyleBackColor = true;
            this.BEOkódFriss.Click += new System.EventHandler(this.BEOkódFriss_Click);
            // 
            // Tábla_BeoKód
            // 
            this.Tábla_BeoKód.AllowUserToAddRows = false;
            this.Tábla_BeoKód.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Tábla_BeoKód.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla_BeoKód.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_BeoKód.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla_BeoKód.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_BeoKód.EnableHeadersVisualStyles = false;
            this.Tábla_BeoKód.Location = new System.Drawing.Point(3, 215);
            this.Tábla_BeoKód.Name = "Tábla_BeoKód";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_BeoKód.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Tábla_BeoKód.RowHeadersWidth = 51;
            this.Tábla_BeoKód.Size = new System.Drawing.Size(1126, 134);
            this.Tábla_BeoKód.TabIndex = 45;
            this.Tábla_BeoKód.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_BeoKód_CellClick);
            this.Tábla_BeoKód.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_BeoKód_CellDoubleClick);
            this.Tábla_BeoKód.SelectionChanged += new System.EventHandler(this.Tábla_BeoKód_SelectionChanged);
            // 
            // Végeidő
            // 
            this.Végeidő.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Végeidő.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.Végeidő.Location = new System.Drawing.Point(150, 110);
            this.Végeidő.Name = "Végeidő";
            this.Végeidő.Size = new System.Drawing.Size(112, 26);
            this.Végeidő.TabIndex = 3;
            this.Végeidő.Value = new System.DateTime(2021, 3, 30, 7, 0, 0, 0);
            // 
            // Kezdőidő
            // 
            this.Kezdőidő.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Kezdőidő.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.Kezdőidő.Location = new System.Drawing.Point(150, 74);
            this.Kezdőidő.Name = "Kezdőidő";
            this.Kezdőidő.Size = new System.Drawing.Size(112, 26);
            this.Kezdőidő.TabIndex = 2;
            this.Kezdőidő.Value = new System.DateTime(2021, 3, 30, 7, 0, 0, 0);
            // 
            // Túlóra
            // 
            this.Túlóra.Location = new System.Drawing.Point(150, 42);
            this.Túlóra.Name = "Túlóra";
            this.Túlóra.Size = new System.Drawing.Size(141, 26);
            this.Túlóra.TabIndex = 1;
            // 
            // Beosztáskód
            // 
            this.Beosztáskód.Location = new System.Drawing.Point(150, 6);
            this.Beosztáskód.Name = "Beosztáskód";
            this.Beosztáskód.Size = new System.Drawing.Size(141, 26);
            this.Beosztáskód.TabIndex = 0;
            // 
            // Túlóraoka
            // 
            this.Túlóraoka.Location = new System.Drawing.Point(150, 147);
            this.Túlóraoka.Name = "Túlóraoka";
            this.Túlóraoka.Size = new System.Drawing.Size(511, 26);
            this.Túlóraoka.TabIndex = 4;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(16, 189);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(80, 20);
            this.Label5.TabIndex = 39;
            this.Label5.Text = "Telephely:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(16, 153);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(87, 20);
            this.Label4.TabIndex = 38;
            this.Label4.Text = "Túlóra oka:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(16, 117);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(95, 20);
            this.Label3.TabIndex = 37;
            this.Label3.Text = "Túlóra vége:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(16, 81);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(118, 20);
            this.Label2.TabIndex = 36;
            this.Label2.Text = "Túlóra kezdete:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(16, 45);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(57, 20);
            this.Label1.TabIndex = 35;
            this.Label1.Text = "Túlóra:";
            // 
            // Telephely
            // 
            this.Telephely.FormattingEnabled = true;
            this.Telephely.Location = new System.Drawing.Point(150, 181);
            this.Telephely.Name = "Telephely";
            this.Telephely.Size = new System.Drawing.Size(187, 28);
            this.Telephely.TabIndex = 5;
            // 
            // Label57
            // 
            this.Label57.AutoSize = true;
            this.Label57.Location = new System.Drawing.Point(16, 9);
            this.Label57.Name = "Label57";
            this.Label57.Size = new System.Drawing.Size(106, 20);
            this.Label57.TabIndex = 34;
            this.Label57.Text = "Beosztáskód:";
            // 
            // Tábla_BeoKód_Új
            // 
            this.Tábla_BeoKód_Új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Tábla_BeoKód_Új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tábla_BeoKód_Új.Location = new System.Drawing.Point(358, 92);
            this.Tábla_BeoKód_Új.Name = "Tábla_BeoKód_Új";
            this.Tábla_BeoKód_Új.Size = new System.Drawing.Size(45, 45);
            this.Tábla_BeoKód_Új.TabIndex = 7;
            this.toolTip1.SetToolTip(this.Tábla_BeoKód_Új, "Új adatnak előkészíti a beviteli mezőt");
            this.Tábla_BeoKód_Új.UseVisualStyleBackColor = true;
            this.Tábla_BeoKód_Új.Click += new System.EventHandler(this.Tábla_BeoKód_Új_Click);
            // 
            // Tábla_BeoKód_Töröl
            // 
            this.Tábla_BeoKód_Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Tábla_BeoKód_Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tábla_BeoKód_Töröl.Location = new System.Drawing.Point(409, 92);
            this.Tábla_BeoKód_Töröl.Name = "Tábla_BeoKód_Töröl";
            this.Tábla_BeoKód_Töröl.Size = new System.Drawing.Size(45, 45);
            this.Tábla_BeoKód_Töröl.TabIndex = 8;
            this.toolTip1.SetToolTip(this.Tábla_BeoKód_Töröl, "Törli az adatokat");
            this.Tábla_BeoKód_Töröl.UseVisualStyleBackColor = true;
            this.Tábla_BeoKód_Töröl.Click += new System.EventHandler(this.Tábla_BeoKód_Töröl_Click);
            // 
            // Tábla_BeoKód_OK
            // 
            this.Tábla_BeoKód_OK.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Tábla_BeoKód_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tábla_BeoKód_OK.Location = new System.Drawing.Point(358, 6);
            this.Tábla_BeoKód_OK.Name = "Tábla_BeoKód_OK";
            this.Tábla_BeoKód_OK.Size = new System.Drawing.Size(45, 45);
            this.Tábla_BeoKód_OK.TabIndex = 6;
            this.toolTip1.SetToolTip(this.Tábla_BeoKód_OK, "Rögzít / Módosít");
            this.Tábla_BeoKód_OK.UseVisualStyleBackColor = true;
            this.Tábla_BeoKód_OK.Click += new System.EventHandler(this.Tábla_BeoKód_Click);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.Teal;
            this.TabPage2.Controls.Add(this.TúlóraFrissít);
            this.TabPage2.Controls.Add(this.Label9);
            this.TabPage2.Controls.Add(this.Tábla_Keret);
            this.TabPage2.Controls.Add(this.Túlparancs);
            this.TabPage2.Controls.Add(this.Túlhatár);
            this.TabPage2.Controls.Add(this.Label6);
            this.TabPage2.Controls.Add(this.Label7);
            this.TabPage2.Controls.Add(this.Túltelephely);
            this.TabPage2.Controls.Add(this.Label8);
            this.TabPage2.Controls.Add(this.Tábla_Keret_Új);
            this.TabPage2.Controls.Add(this.Tábla_Keret_Töröl);
            this.TabPage2.Controls.Add(this.Tábla_Keret_OK);
            this.TabPage2.Location = new System.Drawing.Point(4, 54);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1132, 352);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Túlóra keret";
            // 
            // TúlóraFrissít
            // 
            this.TúlóraFrissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.TúlóraFrissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TúlóraFrissít.Location = new System.Drawing.Point(312, 57);
            this.TúlóraFrissít.Name = "TúlóraFrissít";
            this.TúlóraFrissít.Size = new System.Drawing.Size(45, 45);
            this.TúlóraFrissít.TabIndex = 77;
            this.toolTip1.SetToolTip(this.TúlóraFrissít, "Frissíti a táblázatot");
            this.TúlóraFrissít.UseVisualStyleBackColor = true;
            this.TúlóraFrissít.Click += new System.EventHandler(this.TúlóraFrissít_Click);
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(481, 7);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(57, 20);
            this.Label9.TabIndex = 56;
            this.Label9.Text = "Label9";
            // 
            // Tábla_Keret
            // 
            this.Tábla_Keret.AllowUserToAddRows = false;
            this.Tábla_Keret.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Tábla_Keret.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Tábla_Keret.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_Keret.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.Tábla_Keret.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_Keret.EnableHeadersVisualStyles = false;
            this.Tábla_Keret.Location = new System.Drawing.Point(3, 108);
            this.Tábla_Keret.Name = "Tábla_Keret";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Green;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_Keret.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.Tábla_Keret.RowHeadersWidth = 51;
            this.Tábla_Keret.Size = new System.Drawing.Size(1126, 241);
            this.Tábla_Keret.TabIndex = 55;
            this.Tábla_Keret.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_Keret_CellClick);
            this.Tábla_Keret.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_Keret_CellDoubleClick);
            this.Tábla_Keret.SelectionChanged += new System.EventHandler(this.Tábla_Keret_SelectionChanged);
            // 
            // Túlparancs
            // 
            this.Túlparancs.Location = new System.Drawing.Point(119, 44);
            this.Túlparancs.Name = "Túlparancs";
            this.Túlparancs.Size = new System.Drawing.Size(141, 26);
            this.Túlparancs.TabIndex = 1;
            // 
            // Túlhatár
            // 
            this.Túlhatár.Location = new System.Drawing.Point(119, 12);
            this.Túlhatár.Name = "Túlhatár";
            this.Túlhatár.Size = new System.Drawing.Size(141, 26);
            this.Túlhatár.TabIndex = 0;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(7, 80);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(80, 20);
            this.Label6.TabIndex = 54;
            this.Label6.Text = "Telephely:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(7, 45);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(103, 20);
            this.Label7.TabIndex = 53;
            this.Label7.Text = "Követelmény:";
            // 
            // Túltelephely
            // 
            this.Túltelephely.FormattingEnabled = true;
            this.Túltelephely.Location = new System.Drawing.Point(119, 76);
            this.Túltelephely.Name = "Túltelephely";
            this.Túltelephely.Size = new System.Drawing.Size(187, 28);
            this.Túltelephely.TabIndex = 2;
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(7, 18);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(76, 20);
            this.Label8.TabIndex = 52;
            this.Label8.Text = "Határóra:";
            // 
            // Tábla_Keret_Új
            // 
            this.Tábla_Keret_Új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Tábla_Keret_Új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tábla_Keret_Új.Location = new System.Drawing.Point(361, 57);
            this.Tábla_Keret_Új.Name = "Tábla_Keret_Új";
            this.Tábla_Keret_Új.Size = new System.Drawing.Size(45, 45);
            this.Tábla_Keret_Új.TabIndex = 4;
            this.toolTip1.SetToolTip(this.Tábla_Keret_Új, "Új adatnak előkészíti a beviteli mezőt");
            this.Tábla_Keret_Új.UseVisualStyleBackColor = true;
            this.Tábla_Keret_Új.Click += new System.EventHandler(this.Tábla_Keret_Új_Click);
            // 
            // Tábla_Keret_Töröl
            // 
            this.Tábla_Keret_Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Tábla_Keret_Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tábla_Keret_Töröl.Location = new System.Drawing.Point(412, 57);
            this.Tábla_Keret_Töröl.Name = "Tábla_Keret_Töröl";
            this.Tábla_Keret_Töröl.Size = new System.Drawing.Size(45, 45);
            this.Tábla_Keret_Töröl.TabIndex = 5;
            this.toolTip1.SetToolTip(this.Tábla_Keret_Töröl, "Törli az adatokat");
            this.Tábla_Keret_Töröl.UseVisualStyleBackColor = true;
            this.Tábla_Keret_Töröl.Click += new System.EventHandler(this.Tábla_Keret_Töröl_Click);
            // 
            // Tábla_Keret_OK
            // 
            this.Tábla_Keret_OK.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Tábla_Keret_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Tábla_Keret_OK.Location = new System.Drawing.Point(361, 6);
            this.Tábla_Keret_OK.Name = "Tábla_Keret_OK";
            this.Tábla_Keret_OK.Size = new System.Drawing.Size(45, 45);
            this.Tábla_Keret_OK.TabIndex = 3;
            this.toolTip1.SetToolTip(this.Tábla_Keret_OK, "Rögzít / Módosít");
            this.Tábla_Keret_OK.UseVisualStyleBackColor = true;
            this.Tábla_Keret_OK.Click += new System.EventHandler(this.Tábla_Keret_OK_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.TabPage3.Controls.Add(this.Éves_Frissít);
            this.TabPage3.Controls.Add(this.ÉvesTperc);
            this.TabPage3.Controls.Add(this.Label16);
            this.TabPage3.Controls.Add(this.ÉvesEPnap);
            this.TabPage3.Controls.Add(this.Label15);
            this.TabPage3.Controls.Add(this.ÉvesFélév);
            this.TabPage3.Controls.Add(this.Label14);
            this.TabPage3.Controls.Add(this.ÉvesCsoport);
            this.TabPage3.Controls.Add(this.Label13);
            this.TabPage3.Controls.Add(this.ÉvesZKnap);
            this.TabPage3.Controls.Add(this.ÉvesÉv);
            this.TabPage3.Controls.Add(this.Label10);
            this.TabPage3.Controls.Add(this.Label11);
            this.TabPage3.Controls.Add(this.ÉvesTelephely);
            this.TabPage3.Controls.Add(this.Label12);
            this.TabPage3.Controls.Add(this.Éves_Tábla);
            this.TabPage3.Controls.Add(this.Éves_Generál);
            this.TabPage3.Controls.Add(this.Éves_Új);
            this.TabPage3.Controls.Add(this.Éves_Töröl);
            this.TabPage3.Controls.Add(this.Éves_Ok);
            this.TabPage3.Location = new System.Drawing.Point(4, 54);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1132, 352);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Éves összesítő";
            // 
            // Éves_Frissít
            // 
            this.Éves_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Éves_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Éves_Frissít.Location = new System.Drawing.Point(882, 66);
            this.Éves_Frissít.Name = "Éves_Frissít";
            this.Éves_Frissít.Size = new System.Drawing.Size(45, 45);
            this.Éves_Frissít.TabIndex = 75;
            this.toolTip1.SetToolTip(this.Éves_Frissít, "Frissíti a táblázatot");
            this.Éves_Frissít.UseVisualStyleBackColor = true;
            this.Éves_Frissít.Click += new System.EventHandler(this.Éves_Frissít_Click);
            // 
            // ÉvesTperc
            // 
            this.ÉvesTperc.Location = new System.Drawing.Point(733, 78);
            this.ÉvesTperc.Name = "ÉvesTperc";
            this.ÉvesTperc.Size = new System.Drawing.Size(141, 26);
            this.ÉvesTperc.TabIndex = 5;
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(635, 84);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(92, 20);
            this.Label16.TabIndex = 74;
            this.Label16.Text = "Túlóra perc:";
            // 
            // ÉvesEPnap
            // 
            this.ÉvesEPnap.Location = new System.Drawing.Point(490, 78);
            this.ÉvesEPnap.Name = "ÉvesEPnap";
            this.ÉvesEPnap.Size = new System.Drawing.Size(141, 26);
            this.ÉvesEPnap.TabIndex = 4;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(353, 84);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(136, 20);
            this.Label15.TabIndex = 72;
            this.Label15.Text = "Elvont pihenőnap:";
            // 
            // ÉvesFélév
            // 
            this.ÉvesFélév.Location = new System.Drawing.Point(490, 10);
            this.ÉvesFélév.Name = "ÉvesFélév";
            this.ÉvesFélév.Size = new System.Drawing.Size(141, 26);
            this.ÉvesFélév.TabIndex = 1;
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(353, 16);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(51, 20);
            this.Label14.TabIndex = 70;
            this.Label14.Text = "Félév:";
            // 
            // ÉvesCsoport
            // 
            this.ÉvesCsoport.FormattingEnabled = true;
            this.ÉvesCsoport.Location = new System.Drawing.Point(159, 43);
            this.ÉvesCsoport.Name = "ÉvesCsoport";
            this.ÉvesCsoport.Size = new System.Drawing.Size(187, 28);
            this.ÉvesCsoport.TabIndex = 2;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(7, 120);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(80, 20);
            this.Label13.TabIndex = 67;
            this.Label13.Text = "Telephely:";
            // 
            // ÉvesZKnap
            // 
            this.ÉvesZKnap.Location = new System.Drawing.Point(159, 78);
            this.ÉvesZKnap.Name = "ÉvesZKnap";
            this.ÉvesZKnap.Size = new System.Drawing.Size(141, 26);
            this.ÉvesZKnap.TabIndex = 3;
            // 
            // ÉvesÉv
            // 
            this.ÉvesÉv.Location = new System.Drawing.Point(159, 10);
            this.ÉvesÉv.Name = "ÉvesÉv";
            this.ÉvesÉv.Size = new System.Drawing.Size(141, 26);
            this.ÉvesÉv.TabIndex = 0;
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(7, 84);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(146, 20);
            this.Label10.TabIndex = 65;
            this.Label10.Text = "Kiadott szabadnap:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(7, 51);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(69, 20);
            this.Label11.TabIndex = 64;
            this.Label11.Text = "Csoport:";
            // 
            // ÉvesTelephely
            // 
            this.ÉvesTelephely.FormattingEnabled = true;
            this.ÉvesTelephely.Location = new System.Drawing.Point(159, 112);
            this.ÉvesTelephely.Name = "ÉvesTelephely";
            this.ÉvesTelephely.Size = new System.Drawing.Size(187, 28);
            this.ÉvesTelephely.TabIndex = 6;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(7, 16);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(31, 20);
            this.Label12.TabIndex = 63;
            this.Label12.Text = "Év:";
            // 
            // Éves_Tábla
            // 
            this.Éves_Tábla.AllowUserToAddRows = false;
            this.Éves_Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Lime;
            this.Éves_Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.Éves_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Éves_Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.Éves_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Éves_Tábla.EnableHeadersVisualStyles = false;
            this.Éves_Tábla.Location = new System.Drawing.Point(1, 146);
            this.Éves_Tábla.Name = "Éves_Tábla";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Éves_Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.Éves_Tábla.RowHeadersWidth = 51;
            this.Éves_Tábla.Size = new System.Drawing.Size(1129, 201);
            this.Éves_Tábla.TabIndex = 56;
            this.Éves_Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Éves_Tábla_CellClick);
            this.Éves_Tábla.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Éves_Tábla_CellDoubleClick);
            this.Éves_Tábla.SelectionChanged += new System.EventHandler(this.Éves_Tábla_SelectionChanged);
            // 
            // Éves_Generál
            // 
            this.Éves_Generál.BackgroundImage = global::Villamos.Properties.Resources.Calendar;
            this.Éves_Generál.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Éves_Generál.Location = new System.Drawing.Point(994, 66);
            this.Éves_Generál.Name = "Éves_Generál";
            this.Éves_Generál.Size = new System.Drawing.Size(45, 45);
            this.Éves_Generál.TabIndex = 10;
            this.toolTip1.SetToolTip(this.Éves_Generál, "Generálja az adatokat a beosztásból");
            this.Éves_Generál.UseVisualStyleBackColor = true;
            this.Éves_Generál.Click += new System.EventHandler(this.Éves_Generál_Click);
            // 
            // Éves_Új
            // 
            this.Éves_Új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Éves_Új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Éves_Új.Location = new System.Drawing.Point(943, 10);
            this.Éves_Új.Name = "Éves_Új";
            this.Éves_Új.Size = new System.Drawing.Size(45, 45);
            this.Éves_Új.TabIndex = 8;
            this.toolTip1.SetToolTip(this.Éves_Új, "Új adatnak előkészíti a beviteli mezőt");
            this.Éves_Új.UseVisualStyleBackColor = true;
            this.Éves_Új.Click += new System.EventHandler(this.Éves_Új_Click);
            // 
            // Éves_Töröl
            // 
            this.Éves_Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Éves_Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Éves_Töröl.Location = new System.Drawing.Point(994, 10);
            this.Éves_Töröl.Name = "Éves_Töröl";
            this.Éves_Töröl.Size = new System.Drawing.Size(45, 45);
            this.Éves_Töröl.TabIndex = 9;
            this.toolTip1.SetToolTip(this.Éves_Töröl, "Törli az adatokat");
            this.Éves_Töröl.UseVisualStyleBackColor = true;
            this.Éves_Töröl.Click += new System.EventHandler(this.Éves_Töröl_Click);
            // 
            // Éves_Ok
            // 
            this.Éves_Ok.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Éves_Ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Éves_Ok.Location = new System.Drawing.Point(882, 10);
            this.Éves_Ok.Name = "Éves_Ok";
            this.Éves_Ok.Size = new System.Drawing.Size(45, 45);
            this.Éves_Ok.TabIndex = 7;
            this.toolTip1.SetToolTip(this.Éves_Ok, "Rögzít / Módosít");
            this.Éves_Ok.UseVisualStyleBackColor = true;
            this.Éves_Ok.Click += new System.EventHandler(this.Éves_Ok_Click);
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.Teal;
            this.TabPage4.Controls.Add(this.Panel3);
            this.TabPage4.Controls.Add(this.Panel2);
            this.TabPage4.Controls.Add(this.Label17);
            this.TabPage4.Controls.Add(this.Panel1);
            this.TabPage4.Location = new System.Drawing.Point(4, 54);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1132, 352);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Csoport turnusok";
            // 
            // Panel3
            // 
            this.Panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel3.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Panel3.Controls.Add(this.Tábla_Munkarend);
            this.Panel3.Controls.Add(this.MunkaRend_OK);
            this.Panel3.Controls.Add(this.MunkaRend_Töröl);
            this.Panel3.Controls.Add(this.Munkarendelnevezés);
            this.Panel3.Controls.Add(this.Munkaidő);
            this.Panel3.Controls.Add(this.Label23);
            this.Panel3.Controls.Add(this.Label24);
            this.Panel3.Location = new System.Drawing.Point(805, 10);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(318, 337);
            this.Panel3.TabIndex = 69;
            // 
            // Tábla_Munkarend
            // 
            this.Tábla_Munkarend.AllowUserToAddRows = false;
            this.Tábla_Munkarend.AllowUserToDeleteRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Tábla_Munkarend.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.Tábla_Munkarend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.Cyan;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_Munkarend.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.Tábla_Munkarend.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_Munkarend.EnableHeadersVisualStyles = false;
            this.Tábla_Munkarend.Location = new System.Drawing.Point(3, 147);
            this.Tábla_Munkarend.Name = "Tábla_Munkarend";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.Cyan;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_Munkarend.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.Tábla_Munkarend.RowHeadersWidth = 51;
            this.Tábla_Munkarend.Size = new System.Drawing.Size(312, 187);
            this.Tábla_Munkarend.TabIndex = 82;
            this.Tábla_Munkarend.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_Munkarend_CellClick);
            this.Tábla_Munkarend.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_Munkarend_CellDoubleClick);
            this.Tábla_Munkarend.SelectionChanged += new System.EventHandler(this.Tábla_Munkarend_SelectionChanged);
            // 
            // MunkaRend_OK
            // 
            this.MunkaRend_OK.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.MunkaRend_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MunkaRend_OK.Location = new System.Drawing.Point(259, 29);
            this.MunkaRend_OK.Name = "MunkaRend_OK";
            this.MunkaRend_OK.Size = new System.Drawing.Size(45, 45);
            this.MunkaRend_OK.TabIndex = 2;
            this.toolTip1.SetToolTip(this.MunkaRend_OK, "Rögzít / Módosít");
            this.MunkaRend_OK.UseVisualStyleBackColor = true;
            this.MunkaRend_OK.Click += new System.EventHandler(this.MunkaRend_OK_Click);
            // 
            // MunkaRend_Töröl
            // 
            this.MunkaRend_Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.MunkaRend_Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.MunkaRend_Töröl.Location = new System.Drawing.Point(259, 94);
            this.MunkaRend_Töröl.Name = "MunkaRend_Töröl";
            this.MunkaRend_Töröl.Size = new System.Drawing.Size(45, 45);
            this.MunkaRend_Töröl.TabIndex = 3;
            this.toolTip1.SetToolTip(this.MunkaRend_Töröl, "Törli az adatokat");
            this.MunkaRend_Töröl.UseVisualStyleBackColor = true;
            this.MunkaRend_Töröl.Click += new System.EventHandler(this.MunkaRend_Töröl_Click);
            // 
            // Munkarendelnevezés
            // 
            this.Munkarendelnevezés.Location = new System.Drawing.Point(20, 45);
            this.Munkarendelnevezés.Name = "Munkarendelnevezés";
            this.Munkarendelnevezés.Size = new System.Drawing.Size(164, 26);
            this.Munkarendelnevezés.TabIndex = 0;
            // 
            // Munkaidő
            // 
            this.Munkaidő.Location = new System.Drawing.Point(20, 110);
            this.Munkaidő.Name = "Munkaidő";
            this.Munkaidő.Size = new System.Drawing.Size(164, 26);
            this.Munkaidő.TabIndex = 1;
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.Location = new System.Drawing.Point(16, 81);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(82, 20);
            this.Label23.TabIndex = 76;
            this.Label23.Text = "Munkaidő:";
            // 
            // Label24
            // 
            this.Label24.AutoSize = true;
            this.Label24.Location = new System.Drawing.Point(16, 22);
            this.Label24.Name = "Label24";
            this.Label24.Size = new System.Drawing.Size(168, 20);
            this.Label24.TabIndex = 75;
            this.Label24.Text = "Munkarend elnevezés:";
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Panel2.Controls.Add(this.Csoport_Töröl);
            this.Panel2.Controls.Add(this.Csoport_Tábla);
            this.Panel2.Controls.Add(this.Kezdődátum);
            this.Panel2.Controls.Add(this.CsoportCombo);
            this.Panel2.Controls.Add(this.Label22);
            this.Panel2.Controls.Add(this.Label21);
            this.Panel2.Controls.Add(this.Label20);
            this.Panel2.Controls.Add(this.Label19);
            this.Panel2.Controls.Add(this.Ciklusnap);
            this.Panel2.Controls.Add(this.MegnevezésText);
            this.Panel2.Controls.Add(this.Id);
            this.Panel2.Controls.Add(this.Label18);
            this.Panel2.Controls.Add(this.Csoport_OK);
            this.Panel2.Location = new System.Drawing.Point(225, 11);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(574, 336);
            this.Panel2.TabIndex = 68;
            // 
            // Csoport_Töröl
            // 
            this.Csoport_Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Csoport_Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csoport_Töröl.Location = new System.Drawing.Point(521, 83);
            this.Csoport_Töröl.Name = "Csoport_Töröl";
            this.Csoport_Töröl.Size = new System.Drawing.Size(45, 45);
            this.Csoport_Töröl.TabIndex = 79;
            this.toolTip1.SetToolTip(this.Csoport_Töröl, "Törli az adatokat");
            this.Csoport_Töröl.UseVisualStyleBackColor = true;
            this.Csoport_Töröl.Click += new System.EventHandler(this.Csoport_Töröl_Click);
            // 
            // Csoport_Tábla
            // 
            this.Csoport_Tábla.AllowUserToAddRows = false;
            this.Csoport_Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Csoport_Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.Csoport_Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.Cyan;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Csoport_Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.Csoport_Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Csoport_Tábla.EnableHeadersVisualStyles = false;
            this.Csoport_Tábla.Location = new System.Drawing.Point(3, 182);
            this.Csoport_Tábla.Name = "Csoport_Tábla";
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.Cyan;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Csoport_Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.Csoport_Tábla.RowHeadersWidth = 51;
            this.Csoport_Tábla.Size = new System.Drawing.Size(568, 151);
            this.Csoport_Tábla.TabIndex = 78;
            this.Csoport_Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Csoport_Tábla_CellClick);
            this.Csoport_Tábla.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Csoport_Tábla_CellDoubleClick);
            this.Csoport_Tábla.SelectionChanged += new System.EventHandler(this.Csoport_Tábla_SelectionChanged);
            // 
            // Kezdődátum
            // 
            this.Kezdődátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Kezdődátum.Location = new System.Drawing.Point(174, 47);
            this.Kezdődátum.Name = "Kezdődátum";
            this.Kezdődátum.Size = new System.Drawing.Size(137, 26);
            this.Kezdődátum.TabIndex = 1;
            // 
            // CsoportCombo
            // 
            this.CsoportCombo.FormattingEnabled = true;
            this.CsoportCombo.Location = new System.Drawing.Point(174, 141);
            this.CsoportCombo.Name = "CsoportCombo";
            this.CsoportCombo.Size = new System.Drawing.Size(187, 28);
            this.CsoportCombo.TabIndex = 4;
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.Location = new System.Drawing.Point(12, 53);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(107, 20);
            this.Label22.TabIndex = 75;
            this.Label22.Text = "Kezdő dátum:";
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Location = new System.Drawing.Point(12, 83);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(82, 20);
            this.Label21.TabIndex = 74;
            this.Label21.Text = "Ciklusnap:";
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Location = new System.Drawing.Point(12, 115);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(103, 20);
            this.Label20.TabIndex = 73;
            this.Label20.Text = "Megnevezés:";
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(12, 149);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(144, 20);
            this.Label19.TabIndex = 72;
            this.Label19.Text = "Csoport elnevezés:";
            // 
            // Ciklusnap
            // 
            this.Ciklusnap.Location = new System.Drawing.Point(174, 77);
            this.Ciklusnap.Name = "Ciklusnap";
            this.Ciklusnap.Size = new System.Drawing.Size(140, 26);
            this.Ciklusnap.TabIndex = 2;
            // 
            // MegnevezésText
            // 
            this.MegnevezésText.Location = new System.Drawing.Point(174, 109);
            this.MegnevezésText.Name = "MegnevezésText";
            this.MegnevezésText.Size = new System.Drawing.Size(277, 26);
            this.MegnevezésText.TabIndex = 3;
            // 
            // Id
            // 
            this.Id.Location = new System.Drawing.Point(174, 16);
            this.Id.Name = "Id";
            this.Id.Size = new System.Drawing.Size(140, 26);
            this.Id.TabIndex = 0;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(12, 22);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(30, 20);
            this.Label18.TabIndex = 69;
            this.Label18.Text = "ID:";
            // 
            // Csoport_OK
            // 
            this.Csoport_OK.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Csoport_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csoport_OK.Location = new System.Drawing.Point(521, 26);
            this.Csoport_OK.Name = "Csoport_OK";
            this.Csoport_OK.Size = new System.Drawing.Size(45, 45);
            this.Csoport_OK.TabIndex = 5;
            this.toolTip1.SetToolTip(this.Csoport_OK, "Rögzít / Módosít");
            this.Csoport_OK.UseVisualStyleBackColor = true;
            this.Csoport_OK.Click += new System.EventHandler(this.Csoport_OK_Click);
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(15, 30);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(144, 20);
            this.Label17.TabIndex = 67;
            this.Label17.Text = "Csoport elnevezés:";
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.Panel1.Controls.Add(this.TurnusokLista);
            this.Panel1.Controls.Add(this.TurnusText);
            this.Panel1.Controls.Add(this.Turnus_Ok);
            this.Panel1.Controls.Add(this.Turnus_Töröl);
            this.Panel1.Location = new System.Drawing.Point(5, 10);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(211, 339);
            this.Panel1.TabIndex = 0;
            // 
            // TurnusokLista
            // 
            this.TurnusokLista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TurnusokLista.FormattingEnabled = true;
            this.TurnusokLista.ItemHeight = 20;
            this.TurnusokLista.Location = new System.Drawing.Point(14, 78);
            this.TurnusokLista.Name = "TurnusokLista";
            this.TurnusokLista.Size = new System.Drawing.Size(140, 244);
            this.TurnusokLista.TabIndex = 1;
            this.TurnusokLista.SelectedIndexChanged += new System.EventHandler(this.TurnusokLista_SelectedIndexChanged);
            // 
            // TurnusText
            // 
            this.TurnusText.Location = new System.Drawing.Point(14, 46);
            this.TurnusText.Name = "TurnusText";
            this.TurnusText.Size = new System.Drawing.Size(140, 26);
            this.TurnusText.TabIndex = 0;
            // 
            // Turnus_Ok
            // 
            this.Turnus_Ok.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Turnus_Ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Turnus_Ok.Location = new System.Drawing.Point(160, 27);
            this.Turnus_Ok.Name = "Turnus_Ok";
            this.Turnus_Ok.Size = new System.Drawing.Size(45, 45);
            this.Turnus_Ok.TabIndex = 2;
            this.toolTip1.SetToolTip(this.Turnus_Ok, "Rögzít / Módosít");
            this.Turnus_Ok.UseVisualStyleBackColor = true;
            this.Turnus_Ok.Click += new System.EventHandler(this.Turnus_Ok_Click);
            // 
            // Turnus_Töröl
            // 
            this.Turnus_Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Turnus_Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Turnus_Töröl.Location = new System.Drawing.Point(160, 78);
            this.Turnus_Töröl.Name = "Turnus_Töröl";
            this.Turnus_Töröl.Size = new System.Drawing.Size(45, 45);
            this.Turnus_Töröl.TabIndex = 3;
            this.toolTip1.SetToolTip(this.Turnus_Töröl, "Törli az adatokat");
            this.Turnus_Töröl.UseVisualStyleBackColor = true;
            this.Turnus_Töröl.Click += new System.EventHandler(this.Turnus_Töröl_Click);
            // 
            // TabPage5
            // 
            this.TabPage5.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.TabPage5.Controls.Add(this.Nappaloslenyíló);
            this.TabPage5.Controls.Add(this.Panel4);
            this.TabPage5.Controls.Add(this.Nappalos_Excel);
            this.TabPage5.Controls.Add(this.Tábla_Nappalos);
            this.TabPage5.Controls.Add(this.Választott);
            this.TabPage5.Controls.Add(this.Nappal_Számol);
            this.TabPage5.Controls.Add(this.Nappal_Alap);
            this.TabPage5.Controls.Add(this.NappaloS_Tábla_Friss);
            this.TabPage5.Controls.Add(this.Dátumnappal);
            this.TabPage5.Controls.Add(this.Label25);
            this.TabPage5.Location = new System.Drawing.Point(4, 54);
            this.TabPage5.Name = "TabPage5";
            this.TabPage5.Size = new System.Drawing.Size(1132, 352);
            this.TabPage5.TabIndex = 4;
            this.TabPage5.Text = "Munkaidő naptár";
            // 
            // Nappaloslenyíló
            // 
            this.Nappaloslenyíló.FormattingEnabled = true;
            this.Nappaloslenyíló.Location = new System.Drawing.Point(554, 8);
            this.Nappaloslenyíló.Name = "Nappaloslenyíló";
            this.Nappaloslenyíló.Size = new System.Drawing.Size(161, 28);
            this.Nappaloslenyíló.TabIndex = 82;
            this.Nappaloslenyíló.Visible = false;
            this.Nappaloslenyíló.SelectedIndexChanged += new System.EventHandler(this.Nappaloslenyíló_SelectedIndexChanged);
            this.Nappaloslenyíló.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Nappaloslenyíló_KeyDown);
            // 
            // Panel4
            // 
            this.Panel4.BackColor = System.Drawing.Color.Red;
            this.Panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel4.Controls.Add(this.Nappal_Ok);
            this.Panel4.Location = new System.Drawing.Point(365, 5);
            this.Panel4.Name = "Panel4";
            this.Panel4.Size = new System.Drawing.Size(50, 50);
            this.Panel4.TabIndex = 89;
            this.Panel4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Panel4_MouseClick);
            // 
            // Nappal_Ok
            // 
            this.Nappal_Ok.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.Nappal_Ok.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Nappal_Ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Nappal_Ok.Enabled = false;
            this.Nappal_Ok.Location = new System.Drawing.Point(5, 5);
            this.Nappal_Ok.Name = "Nappal_Ok";
            this.Nappal_Ok.Size = new System.Drawing.Size(40, 40);
            this.Nappal_Ok.TabIndex = 4;
            this.toolTip1.SetToolTip(this.Nappal_Ok, "Rögzít / Módosít");
            this.Nappal_Ok.UseVisualStyleBackColor = false;
            this.Nappal_Ok.Click += new System.EventHandler(this.Nappal_Ok_Click);
            // 
            // Nappalos_Excel
            // 
            this.Nappalos_Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Nappalos_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Nappalos_Excel.Location = new System.Drawing.Point(435, 8);
            this.Nappalos_Excel.Name = "Nappalos_Excel";
            this.Nappalos_Excel.Size = new System.Drawing.Size(45, 45);
            this.Nappalos_Excel.TabIndex = 88;
            this.toolTip1.SetToolTip(this.Nappalos_Excel, "Excel táblázatot készít a táblázatból");
            this.Nappalos_Excel.UseVisualStyleBackColor = true;
            this.Nappalos_Excel.Click += new System.EventHandler(this.Nappalos_Excel_Click);
            // 
            // Tábla_Nappalos
            // 
            this.Tábla_Nappalos.AllowUserToAddRows = false;
            this.Tábla_Nappalos.AllowUserToDeleteRows = false;
            this.Tábla_Nappalos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla_Nappalos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_Nappalos.Location = new System.Drawing.Point(3, 59);
            this.Tábla_Nappalos.Name = "Tábla_Nappalos";
            this.Tábla_Nappalos.RowHeadersVisible = false;
            this.Tábla_Nappalos.RowHeadersWidth = 51;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.Turquoise;
            this.Tábla_Nappalos.RowsDefaultCellStyle = dataGridViewCellStyle16;
            this.Tábla_Nappalos.Size = new System.Drawing.Size(1126, 288);
            this.Tábla_Nappalos.TabIndex = 87;
            this.Tábla_Nappalos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_Nappalos_CellClick);
            this.Tábla_Nappalos.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Tábla_Nappalos_Scroll);
            // 
            // Választott
            // 
            this.Választott.BackColor = System.Drawing.Color.Turquoise;
            this.Választott.Location = new System.Drawing.Point(866, 11);
            this.Választott.Name = "Választott";
            this.Választott.Size = new System.Drawing.Size(47, 26);
            this.Választott.TabIndex = 85;
            this.Választott.Visible = false;
            // 
            // Nappal_Számol
            // 
            this.Nappal_Számol.BackgroundImage = global::Villamos.Properties.Resources.Calc;
            this.Nappal_Számol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Nappal_Számol.Location = new System.Drawing.Point(253, 8);
            this.Nappal_Számol.Name = "Nappal_Számol";
            this.Nappal_Számol.Size = new System.Drawing.Size(45, 45);
            this.Nappal_Számol.TabIndex = 2;
            this.toolTip1.SetToolTip(this.Nappal_Számol, "Összesíti a munkidőket");
            this.Nappal_Számol.UseVisualStyleBackColor = true;
            this.Nappal_Számol.Click += new System.EventHandler(this.Nappal_Számol_Click);
            // 
            // Nappal_Alap
            // 
            this.Nappal_Alap.BackgroundImage = global::Villamos.Properties.Resources.CALENDR1;
            this.Nappal_Alap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Nappal_Alap.Location = new System.Drawing.Point(202, 8);
            this.Nappal_Alap.Name = "Nappal_Alap";
            this.Nappal_Alap.Size = new System.Drawing.Size(45, 45);
            this.Nappal_Alap.TabIndex = 1;
            this.toolTip1.SetToolTip(this.Nappal_Alap, "Éves naptárat készít");
            this.Nappal_Alap.UseVisualStyleBackColor = true;
            this.Nappal_Alap.Click += new System.EventHandler(this.Nappal_Alap_Click);
            // 
            // NappaloS_Tábla_Friss
            // 
            this.NappaloS_Tábla_Friss.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.NappaloS_Tábla_Friss.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.NappaloS_Tábla_Friss.Location = new System.Drawing.Point(304, 8);
            this.NappaloS_Tábla_Friss.Name = "NappaloS_Tábla_Friss";
            this.NappaloS_Tábla_Friss.Size = new System.Drawing.Size(45, 45);
            this.NappaloS_Tábla_Friss.TabIndex = 3;
            this.toolTip1.SetToolTip(this.NappaloS_Tábla_Friss, "Rögzített adatokat listázza \r\n");
            this.NappaloS_Tábla_Friss.UseVisualStyleBackColor = true;
            this.NappaloS_Tábla_Friss.Click += new System.EventHandler(this.NappaloS_Tábla_Friss_Click);
            // 
            // Dátumnappal
            // 
            this.Dátumnappal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumnappal.Location = new System.Drawing.Point(59, 8);
            this.Dátumnappal.Name = "Dátumnappal";
            this.Dátumnappal.Size = new System.Drawing.Size(137, 26);
            this.Dátumnappal.TabIndex = 0;
            this.Dátumnappal.ValueChanged += new System.EventHandler(this.Dátumnappal_ValueChanged);
            // 
            // Label25
            // 
            this.Label25.AutoSize = true;
            this.Label25.Location = new System.Drawing.Point(7, 14);
            this.Label25.Name = "Label25";
            this.Label25.Size = new System.Drawing.Size(31, 20);
            this.Label25.TabIndex = 77;
            this.Label25.Text = "Év:";
            // 
            // TabPage6
            // 
            this.TabPage6.BackColor = System.Drawing.Color.Teal;
            this.TabPage6.Controls.Add(this.VáltósLenyíló);
            this.TabPage6.Controls.Add(this.Panel5);
            this.TabPage6.Controls.Add(this.Excelkészítés);
            this.TabPage6.Controls.Add(this.VváltósCsoport);
            this.TabPage6.Controls.Add(this.Label27);
            this.TabPage6.Controls.Add(this.Tábla9);
            this.TabPage6.Controls.Add(this.VálasztottVáltó);
            this.TabPage6.Controls.Add(this.Command33);
            this.TabPage6.Controls.Add(this.Command36);
            this.TabPage6.Controls.Add(this.Command34);
            this.TabPage6.Controls.Add(this.VáltósNaptár);
            this.TabPage6.Controls.Add(this.Label26);
            this.TabPage6.Location = new System.Drawing.Point(4, 54);
            this.TabPage6.Name = "TabPage6";
            this.TabPage6.Size = new System.Drawing.Size(1132, 352);
            this.TabPage6.TabIndex = 5;
            this.TabPage6.Text = "Váltós naptár";
            // 
            // VáltósLenyíló
            // 
            this.VáltósLenyíló.FormattingEnabled = true;
            this.VáltósLenyíló.Location = new System.Drawing.Point(685, 9);
            this.VáltósLenyíló.Name = "VáltósLenyíló";
            this.VáltósLenyíló.Size = new System.Drawing.Size(168, 28);
            this.VáltósLenyíló.TabIndex = 94;
            this.VáltósLenyíló.Visible = false;
            this.VáltósLenyíló.SelectedIndexChanged += new System.EventHandler(this.VáltósLenyíló_SelectedIndexChanged);
            this.VáltósLenyíló.KeyDown += new System.Windows.Forms.KeyEventHandler(this.VáltósLenyíló_KeyDown);
            // 
            // Panel5
            // 
            this.Panel5.BackColor = System.Drawing.Color.Red;
            this.Panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Panel5.Controls.Add(this.Command30);
            this.Panel5.Location = new System.Drawing.Point(482, 7);
            this.Panel5.Name = "Panel5";
            this.Panel5.Size = new System.Drawing.Size(50, 50);
            this.Panel5.TabIndex = 108;
            this.Panel5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Panel5_MouseClick);
            this.Panel5.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel5_MouseMove);
            // 
            // Command30
            // 
            this.Command30.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Command30.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command30.Location = new System.Drawing.Point(5, 5);
            this.Command30.Name = "Command30";
            this.Command30.Size = new System.Drawing.Size(40, 40);
            this.Command30.TabIndex = 92;
            this.toolTip1.SetToolTip(this.Command30, "Rögzít / Módosít");
            this.Command30.UseVisualStyleBackColor = true;
            this.Command30.Visible = false;
            this.Command30.Click += new System.EventHandler(this.Command30_Click);
            // 
            // Excelkészítés
            // 
            this.Excelkészítés.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excelkészítés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excelkészítés.Location = new System.Drawing.Point(538, 8);
            this.Excelkészítés.Name = "Excelkészítés";
            this.Excelkészítés.Size = new System.Drawing.Size(45, 45);
            this.Excelkészítés.TabIndex = 102;
            this.toolTip1.SetToolTip(this.Excelkészítés, "Excel táblázatot készít a táblázatból");
            this.Excelkészítés.UseVisualStyleBackColor = true;
            this.Excelkészítés.Click += new System.EventHandler(this.Excelkészítés_Click);
            // 
            // VváltósCsoport
            // 
            this.VváltósCsoport.FormattingEnabled = true;
            this.VváltósCsoport.Location = new System.Drawing.Point(156, 39);
            this.VváltósCsoport.Name = "VváltósCsoport";
            this.VváltósCsoport.Size = new System.Drawing.Size(137, 28);
            this.VváltósCsoport.TabIndex = 101;
            this.VváltósCsoport.SelectedIndexChanged += new System.EventHandler(this.VváltósCsoport_SelectedIndexChanged);
            // 
            // Label27
            // 
            this.Label27.AutoSize = true;
            this.Label27.Location = new System.Drawing.Point(7, 47);
            this.Label27.Name = "Label27";
            this.Label27.Size = new System.Drawing.Size(144, 20);
            this.Label27.TabIndex = 100;
            this.Label27.Text = "Csoport elnevezés:";
            // 
            // Tábla9
            // 
            this.Tábla9.AllowUserToAddRows = false;
            this.Tábla9.AllowUserToDeleteRows = false;
            this.Tábla9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla9.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla9.Location = new System.Drawing.Point(7, 75);
            this.Tábla9.Name = "Tábla9";
            this.Tábla9.RowHeadersVisible = false;
            this.Tábla9.RowHeadersWidth = 51;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.Tábla9.RowsDefaultCellStyle = dataGridViewCellStyle17;
            this.Tábla9.Size = new System.Drawing.Size(1122, 274);
            this.Tábla9.TabIndex = 99;
            this.Tábla9.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla9_CellClick);
            this.Tábla9.Scroll += new System.Windows.Forms.ScrollEventHandler(this.Tábla9_Scroll);
            this.Tábla9.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Tábla9_KeyDown);
            // 
            // VálasztottVáltó
            // 
            this.VálasztottVáltó.Location = new System.Drawing.Point(936, 11);
            this.VálasztottVáltó.Name = "VálasztottVáltó";
            this.VálasztottVáltó.Size = new System.Drawing.Size(47, 26);
            this.VálasztottVáltó.TabIndex = 97;
            this.VálasztottVáltó.Visible = false;
            // 
            // Command33
            // 
            this.Command33.BackgroundImage = global::Villamos.Properties.Resources.Calc;
            this.Command33.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command33.Location = new System.Drawing.Point(380, 8);
            this.Command33.Name = "Command33";
            this.Command33.Size = new System.Drawing.Size(45, 45);
            this.Command33.TabIndex = 90;
            this.toolTip1.SetToolTip(this.Command33, "Összesíti a munkaidőket");
            this.Command33.UseVisualStyleBackColor = true;
            this.Command33.Click += new System.EventHandler(this.Command33_Click);
            // 
            // Command36
            // 
            this.Command36.BackgroundImage = global::Villamos.Properties.Resources.CALENDR1;
            this.Command36.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command36.Location = new System.Drawing.Point(329, 8);
            this.Command36.Name = "Command36";
            this.Command36.Size = new System.Drawing.Size(45, 45);
            this.Command36.TabIndex = 89;
            this.toolTip1.SetToolTip(this.Command36, "Elkészíti a váltós naptárnak megfelelő naptárat");
            this.Command36.UseVisualStyleBackColor = true;
            this.Command36.Click += new System.EventHandler(this.Command36_Click);
            // 
            // Command34
            // 
            this.Command34.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Command34.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Command34.Location = new System.Drawing.Point(431, 8);
            this.Command34.Name = "Command34";
            this.Command34.Size = new System.Drawing.Size(45, 45);
            this.Command34.TabIndex = 91;
            this.toolTip1.SetToolTip(this.Command34, "Generálja az adatokat a beosztásból");
            this.Command34.UseVisualStyleBackColor = true;
            this.Command34.Click += new System.EventHandler(this.Command34_Click);
            // 
            // VáltósNaptár
            // 
            this.VáltósNaptár.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.VáltósNaptár.Location = new System.Drawing.Point(156, 7);
            this.VáltósNaptár.Name = "VáltósNaptár";
            this.VáltósNaptár.Size = new System.Drawing.Size(137, 26);
            this.VáltósNaptár.TabIndex = 88;
            this.VáltósNaptár.ValueChanged += new System.EventHandler(this.VáltósNaptár_ValueChanged);
            // 
            // Label26
            // 
            this.Label26.AutoSize = true;
            this.Label26.Location = new System.Drawing.Point(9, 12);
            this.Label26.Name = "Label26";
            this.Label26.Size = new System.Drawing.Size(31, 20);
            this.Label26.TabIndex = 93;
            this.Label26.Text = "Év:";
            // 
            // TabPage7
            // 
            this.TabPage7.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.TabPage7.Controls.Add(this.ElvontCsoport);
            this.TabPage7.Controls.Add(this.ElvontDátum);
            this.TabPage7.Controls.Add(this.ElvontTelephely);
            this.TabPage7.Controls.Add(this.Elvont_Frissít);
            this.TabPage7.Controls.Add(this.ElvontÉv);
            this.TabPage7.Controls.Add(this.Elvont_Generált);
            this.TabPage7.Controls.Add(this.Elvont_Új);
            this.TabPage7.Controls.Add(this.Elvont_Töröl);
            this.TabPage7.Controls.Add(this.Elvont_OK);
            this.TabPage7.Controls.Add(this.Label33);
            this.TabPage7.Controls.Add(this.Label32);
            this.TabPage7.Controls.Add(this.Label31);
            this.TabPage7.Controls.Add(this.Label30);
            this.TabPage7.Controls.Add(this.Label29);
            this.TabPage7.Controls.Add(this.Label28);
            this.TabPage7.Controls.Add(this.SzűrtTelephely);
            this.TabPage7.Controls.Add(this.Tábla_Elvont);
            this.TabPage7.Location = new System.Drawing.Point(4, 54);
            this.TabPage7.Name = "TabPage7";
            this.TabPage7.Size = new System.Drawing.Size(1132, 352);
            this.TabPage7.TabIndex = 6;
            this.TabPage7.Text = "Elvont napok";
            // 
            // ElvontCsoport
            // 
            this.ElvontCsoport.FormattingEnabled = true;
            this.ElvontCsoport.Location = new System.Drawing.Point(710, 20);
            this.ElvontCsoport.Name = "ElvontCsoport";
            this.ElvontCsoport.Size = new System.Drawing.Size(137, 28);
            this.ElvontCsoport.TabIndex = 2;
            // 
            // ElvontDátum
            // 
            this.ElvontDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ElvontDátum.Location = new System.Drawing.Point(710, 55);
            this.ElvontDátum.Name = "ElvontDátum";
            this.ElvontDátum.Size = new System.Drawing.Size(137, 26);
            this.ElvontDátum.TabIndex = 3;
            // 
            // ElvontTelephely
            // 
            this.ElvontTelephely.FormattingEnabled = true;
            this.ElvontTelephely.Location = new System.Drawing.Point(710, 86);
            this.ElvontTelephely.Name = "ElvontTelephely";
            this.ElvontTelephely.Size = new System.Drawing.Size(187, 28);
            this.ElvontTelephely.TabIndex = 4;
            // 
            // Elvont_Frissít
            // 
            this.Elvont_Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Elvont_Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elvont_Frissít.Location = new System.Drawing.Point(145, 3);
            this.Elvont_Frissít.Name = "Elvont_Frissít";
            this.Elvont_Frissít.Size = new System.Drawing.Size(45, 45);
            this.Elvont_Frissít.TabIndex = 9;
            this.toolTip1.SetToolTip(this.Elvont_Frissít, "Frissíti a táblázatot");
            this.Elvont_Frissít.UseVisualStyleBackColor = true;
            this.Elvont_Frissít.Click += new System.EventHandler(this.Elvont_Frissít_Click);
            // 
            // ElvontÉv
            // 
            this.ElvontÉv.Location = new System.Drawing.Point(145, 54);
            this.ElvontÉv.Name = "ElvontÉv";
            this.ElvontÉv.Size = new System.Drawing.Size(140, 26);
            this.ElvontÉv.TabIndex = 0;
            // 
            // Elvont_Generált
            // 
            this.Elvont_Generált.BackgroundImage = global::Villamos.Properties.Resources.Calendar;
            this.Elvont_Generált.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elvont_Generált.Location = new System.Drawing.Point(1001, 68);
            this.Elvont_Generált.Name = "Elvont_Generált";
            this.Elvont_Generált.Size = new System.Drawing.Size(45, 45);
            this.Elvont_Generált.TabIndex = 8;
            this.toolTip1.SetToolTip(this.Elvont_Generált, "Generálja az adatokat a beosztásból");
            this.Elvont_Generált.UseVisualStyleBackColor = true;
            this.Elvont_Generált.Click += new System.EventHandler(this.Elvont_Generált_Click);
            // 
            // Elvont_Új
            // 
            this.Elvont_Új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Elvont_Új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elvont_Új.Location = new System.Drawing.Point(950, 12);
            this.Elvont_Új.Name = "Elvont_Új";
            this.Elvont_Új.Size = new System.Drawing.Size(45, 45);
            this.Elvont_Új.TabIndex = 6;
            this.toolTip1.SetToolTip(this.Elvont_Új, "Új adatnak előkészíti a beviteli mezőt");
            this.Elvont_Új.UseVisualStyleBackColor = true;
            this.Elvont_Új.Click += new System.EventHandler(this.Elvont_Új_Click);
            // 
            // Elvont_Töröl
            // 
            this.Elvont_Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Elvont_Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elvont_Töröl.Location = new System.Drawing.Point(1001, 12);
            this.Elvont_Töröl.Name = "Elvont_Töröl";
            this.Elvont_Töröl.Size = new System.Drawing.Size(45, 45);
            this.Elvont_Töröl.TabIndex = 7;
            this.toolTip1.SetToolTip(this.Elvont_Töröl, "Törli az adatokat");
            this.Elvont_Töröl.UseVisualStyleBackColor = true;
            this.Elvont_Töröl.Click += new System.EventHandler(this.Elvont_Töröl_Click);
            // 
            // Elvont_OK
            // 
            this.Elvont_OK.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Elvont_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Elvont_OK.Location = new System.Drawing.Point(889, 12);
            this.Elvont_OK.Name = "Elvont_OK";
            this.Elvont_OK.Size = new System.Drawing.Size(45, 45);
            this.Elvont_OK.TabIndex = 5;
            this.toolTip1.SetToolTip(this.Elvont_OK, "Rögzít / Módosít");
            this.Elvont_OK.UseVisualStyleBackColor = true;
            this.Elvont_OK.Click += new System.EventHandler(this.Elvont_OK_Click);
            // 
            // Label33
            // 
            this.Label33.AutoSize = true;
            this.Label33.Location = new System.Drawing.Point(7, 12);
            this.Label33.Name = "Label33";
            this.Label33.Size = new System.Drawing.Size(94, 20);
            this.Label33.TabIndex = 107;
            this.Label33.Text = "Lista szűrés";
            // 
            // Label32
            // 
            this.Label32.AutoSize = true;
            this.Label32.Location = new System.Drawing.Point(559, 94);
            this.Label32.Name = "Label32";
            this.Label32.Size = new System.Drawing.Size(80, 20);
            this.Label32.TabIndex = 106;
            this.Label32.Text = "Telephely:";
            // 
            // Label31
            // 
            this.Label31.AutoSize = true;
            this.Label31.Location = new System.Drawing.Point(559, 60);
            this.Label31.Name = "Label31";
            this.Label31.Size = new System.Drawing.Size(140, 20);
            this.Label31.TabIndex = 105;
            this.Label31.Text = "Elvont szabadnap:";
            // 
            // Label30
            // 
            this.Label30.AutoSize = true;
            this.Label30.Location = new System.Drawing.Point(559, 24);
            this.Label30.Name = "Label30";
            this.Label30.Size = new System.Drawing.Size(69, 20);
            this.Label30.TabIndex = 104;
            this.Label30.Text = "Csoport:";
            // 
            // Label29
            // 
            this.Label29.AutoSize = true;
            this.Label29.Location = new System.Drawing.Point(7, 60);
            this.Label29.Name = "Label29";
            this.Label29.Size = new System.Drawing.Size(31, 20);
            this.Label29.TabIndex = 103;
            this.Label29.Text = "Év:";
            // 
            // Label28
            // 
            this.Label28.AutoSize = true;
            this.Label28.Location = new System.Drawing.Point(7, 94);
            this.Label28.Name = "Label28";
            this.Label28.Size = new System.Drawing.Size(80, 20);
            this.Label28.TabIndex = 102;
            this.Label28.Text = "Telephely:";
            // 
            // SzűrtTelephely
            // 
            this.SzűrtTelephely.FormattingEnabled = true;
            this.SzűrtTelephely.Location = new System.Drawing.Point(145, 86);
            this.SzűrtTelephely.Name = "SzűrtTelephely";
            this.SzűrtTelephely.Size = new System.Drawing.Size(187, 28);
            this.SzűrtTelephely.TabIndex = 1;
            // 
            // Tábla_Elvont
            // 
            this.Tábla_Elvont.AllowUserToAddRows = false;
            this.Tábla_Elvont.AllowUserToDeleteRows = false;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Tábla_Elvont.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle18;
            this.Tábla_Elvont.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_Elvont.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.Tábla_Elvont.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_Elvont.EnableHeadersVisualStyles = false;
            this.Tábla_Elvont.Location = new System.Drawing.Point(3, 128);
            this.Tábla_Elvont.Name = "Tábla_Elvont";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.Lime;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_Elvont.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.Tábla_Elvont.RowHeadersWidth = 51;
            this.Tábla_Elvont.Size = new System.Drawing.Size(1126, 221);
            this.Tábla_Elvont.TabIndex = 100;
            this.Tábla_Elvont.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_Elvont_CellClick);
            this.Tábla_Elvont.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_Elvont_CellDoubleClick);
            this.Tábla_Elvont.SelectionChanged += new System.EventHandler(this.Tábla_Elvont_SelectionChanged);
            // 
            // TabPage8
            // 
            this.TabPage8.BackColor = System.Drawing.Color.Teal;
            this.TabPage8.Controls.Add(this.Tábla_VáltMunka);
            this.TabPage8.Controls.Add(this.VáltMunka_Feljebb);
            this.TabPage8.Controls.Add(this.VáltMunka_Új);
            this.TabPage8.Controls.Add(this.VáltMunka_Töröl);
            this.TabPage8.Controls.Add(this.VáltMunka_OK);
            this.TabPage8.Controls.Add(this.BeosztásSzöveg);
            this.TabPage8.Controls.Add(this.VáltMunkBeoKód);
            this.TabPage8.Controls.Add(this.Label36);
            this.TabPage8.Controls.Add(this.Hétnapja);
            this.TabPage8.Controls.Add(this.Label34);
            this.TabPage8.Controls.Add(this.Label35);
            this.TabPage8.Location = new System.Drawing.Point(4, 54);
            this.TabPage8.Name = "TabPage8";
            this.TabPage8.Size = new System.Drawing.Size(1132, 352);
            this.TabPage8.TabIndex = 7;
            this.TabPage8.Text = "Váltós munkarend";
            // 
            // Tábla_VáltMunka
            // 
            this.Tábla_VáltMunka.AllowUserToAddRows = false;
            this.Tábla_VáltMunka.AllowUserToDeleteRows = false;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Tábla_VáltMunka.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle21;
            this.Tábla_VáltMunka.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_VáltMunka.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.Tábla_VáltMunka.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_VáltMunka.EnableHeadersVisualStyles = false;
            this.Tábla_VáltMunka.Location = new System.Drawing.Point(3, 109);
            this.Tábla_VáltMunka.Name = "Tábla_VáltMunka";
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_VáltMunka.RowHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.Tábla_VáltMunka.RowHeadersWidth = 51;
            this.Tábla_VáltMunka.Size = new System.Drawing.Size(1126, 238);
            this.Tábla_VáltMunka.TabIndex = 114;
            this.Tábla_VáltMunka.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_VáltMunka_CellClick);
            this.Tábla_VáltMunka.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_VáltMunka_CellDoubleClick);
            this.Tábla_VáltMunka.SelectionChanged += new System.EventHandler(this.Tábla_VáltMunka_SelectionChanged);
            // 
            // VáltMunka_Feljebb
            // 
            this.VáltMunka_Feljebb.BackgroundImage = global::Villamos.Properties.Resources.Up_gyűjtemény;
            this.VáltMunka_Feljebb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VáltMunka_Feljebb.Location = new System.Drawing.Point(347, 58);
            this.VáltMunka_Feljebb.Name = "VáltMunka_Feljebb";
            this.VáltMunka_Feljebb.Size = new System.Drawing.Size(45, 45);
            this.VáltMunka_Feljebb.TabIndex = 4;
            this.toolTip1.SetToolTip(this.VáltMunka_Feljebb, "Feljebb viszi a sorban az adatot");
            this.VáltMunka_Feljebb.UseVisualStyleBackColor = true;
            this.VáltMunka_Feljebb.Click += new System.EventHandler(this.VáltMunka_Feljebb_Click);
            // 
            // VáltMunka_Új
            // 
            this.VáltMunka_Új.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.VáltMunka_Új.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VáltMunka_Új.Location = new System.Drawing.Point(449, 58);
            this.VáltMunka_Új.Name = "VáltMunka_Új";
            this.VáltMunka_Új.Size = new System.Drawing.Size(45, 45);
            this.VáltMunka_Új.TabIndex = 6;
            this.toolTip1.SetToolTip(this.VáltMunka_Új, "Új adatnak előkészíti a beviteli mezőt");
            this.VáltMunka_Új.UseVisualStyleBackColor = true;
            this.VáltMunka_Új.Click += new System.EventHandler(this.VáltMunka_Új_Click);
            // 
            // VáltMunka_Töröl
            // 
            this.VáltMunka_Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.VáltMunka_Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VáltMunka_Töröl.Location = new System.Drawing.Point(398, 58);
            this.VáltMunka_Töröl.Name = "VáltMunka_Töröl";
            this.VáltMunka_Töröl.Size = new System.Drawing.Size(45, 45);
            this.VáltMunka_Töröl.TabIndex = 5;
            this.toolTip1.SetToolTip(this.VáltMunka_Töröl, "Törli az adatokat");
            this.VáltMunka_Töröl.UseVisualStyleBackColor = true;
            this.VáltMunka_Töröl.Click += new System.EventHandler(this.VáltMunka_Töröl_Click);
            // 
            // VáltMunka_OK
            // 
            this.VáltMunka_OK.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.VáltMunka_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.VáltMunka_OK.Location = new System.Drawing.Point(347, 7);
            this.VáltMunka_OK.Name = "VáltMunka_OK";
            this.VáltMunka_OK.Size = new System.Drawing.Size(45, 45);
            this.VáltMunka_OK.TabIndex = 3;
            this.toolTip1.SetToolTip(this.VáltMunka_OK, "Rögzít / Módosít");
            this.VáltMunka_OK.UseVisualStyleBackColor = true;
            this.VáltMunka_OK.Click += new System.EventHandler(this.VáltMunka_OK_Click);
            // 
            // BeosztásSzöveg
            // 
            this.BeosztásSzöveg.Location = new System.Drawing.Point(148, 75);
            this.BeosztásSzöveg.Name = "BeosztásSzöveg";
            this.BeosztásSzöveg.Size = new System.Drawing.Size(140, 26);
            this.BeosztásSzöveg.TabIndex = 2;
            // 
            // VáltMunkBeoKód
            // 
            this.VáltMunkBeoKód.Location = new System.Drawing.Point(148, 41);
            this.VáltMunkBeoKód.Name = "VáltMunkBeoKód";
            this.VáltMunkBeoKód.Size = new System.Drawing.Size(140, 26);
            this.VáltMunkBeoKód.TabIndex = 1;
            // 
            // Label36
            // 
            this.Label36.AutoSize = true;
            this.Label36.Location = new System.Drawing.Point(7, 13);
            this.Label36.Name = "Label36";
            this.Label36.Size = new System.Drawing.Size(78, 20);
            this.Label36.TabIndex = 107;
            this.Label36.Text = "Hétnapja:";
            // 
            // Hétnapja
            // 
            this.Hétnapja.Location = new System.Drawing.Point(148, 7);
            this.Hétnapja.Name = "Hétnapja";
            this.Hétnapja.Size = new System.Drawing.Size(140, 26);
            this.Hétnapja.TabIndex = 0;
            // 
            // Label34
            // 
            this.Label34.AutoSize = true;
            this.Label34.Location = new System.Drawing.Point(7, 47);
            this.Label34.Name = "Label34";
            this.Label34.Size = new System.Drawing.Size(110, 20);
            this.Label34.TabIndex = 106;
            this.Label34.Text = "Beosztás kód:";
            // 
            // Label35
            // 
            this.Label35.AutoSize = true;
            this.Label35.Location = new System.Drawing.Point(7, 81);
            this.Label35.Name = "Label35";
            this.Label35.Size = new System.Drawing.Size(134, 20);
            this.Label35.TabIndex = 105;
            this.Label35.Text = "Beosztás szöveg:";
            // 
            // TabPage10
            // 
            this.TabPage10.BackColor = System.Drawing.Color.Teal;
            this.TabPage10.Controls.Add(this.Tábla_Éjszaka);
            this.TabPage10.Controls.Add(this.Éjszaka_Feljebb);
            this.TabPage10.Controls.Add(this.Éjszaka_ÚJ);
            this.TabPage10.Controls.Add(this.Éjszaka_Töröl);
            this.TabPage10.Controls.Add(this.Éjszaka_Ok);
            this.TabPage10.Controls.Add(this.ÉBeosztásSzöveg);
            this.TabPage10.Controls.Add(this.ÉBeoKód);
            this.TabPage10.Controls.Add(this.Label40);
            this.TabPage10.Controls.Add(this.ÉhétNapja);
            this.TabPage10.Controls.Add(this.Label41);
            this.TabPage10.Controls.Add(this.Label42);
            this.TabPage10.Location = new System.Drawing.Point(4, 54);
            this.TabPage10.Name = "TabPage10";
            this.TabPage10.Size = new System.Drawing.Size(1132, 352);
            this.TabPage10.TabIndex = 9;
            this.TabPage10.Text = "Éjszakás munkarend";
            // 
            // Tábla_Éjszaka
            // 
            this.Tábla_Éjszaka.AllowUserToAddRows = false;
            this.Tábla_Éjszaka.AllowUserToDeleteRows = false;
            dataGridViewCellStyle24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Tábla_Éjszaka.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle24;
            this.Tábla_Éjszaka.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_Éjszaka.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
            this.Tábla_Éjszaka.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_Éjszaka.EnableHeadersVisualStyles = false;
            this.Tábla_Éjszaka.Location = new System.Drawing.Point(2, 108);
            this.Tábla_Éjszaka.Name = "Tábla_Éjszaka";
            dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle26.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_Éjszaka.RowHeadersDefaultCellStyle = dataGridViewCellStyle26;
            this.Tábla_Éjszaka.RowHeadersWidth = 51;
            this.Tábla_Éjszaka.Size = new System.Drawing.Size(1127, 238);
            this.Tábla_Éjszaka.TabIndex = 125;
            this.Tábla_Éjszaka.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_Éjszaka_CellClick);
            this.Tábla_Éjszaka.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_Éjszaka_CellDoubleClick);
            this.Tábla_Éjszaka.SelectionChanged += new System.EventHandler(this.Tábla_Éjszaka_SelectionChanged);
            // 
            // Éjszaka_Feljebb
            // 
            this.Éjszaka_Feljebb.BackgroundImage = global::Villamos.Properties.Resources.Up_gyűjtemény;
            this.Éjszaka_Feljebb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Éjszaka_Feljebb.Location = new System.Drawing.Point(346, 57);
            this.Éjszaka_Feljebb.Name = "Éjszaka_Feljebb";
            this.Éjszaka_Feljebb.Size = new System.Drawing.Size(45, 45);
            this.Éjszaka_Feljebb.TabIndex = 119;
            this.toolTip1.SetToolTip(this.Éjszaka_Feljebb, "Feljebb viszi a sorban az adatot");
            this.Éjszaka_Feljebb.UseVisualStyleBackColor = true;
            this.Éjszaka_Feljebb.Click += new System.EventHandler(this.Éjszaka_Feljebb_Click);
            // 
            // Éjszaka_ÚJ
            // 
            this.Éjszaka_ÚJ.BackgroundImage = global::Villamos.Properties.Resources.New_gyűjtemény;
            this.Éjszaka_ÚJ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Éjszaka_ÚJ.Location = new System.Drawing.Point(448, 57);
            this.Éjszaka_ÚJ.Name = "Éjszaka_ÚJ";
            this.Éjszaka_ÚJ.Size = new System.Drawing.Size(45, 45);
            this.Éjszaka_ÚJ.TabIndex = 121;
            this.toolTip1.SetToolTip(this.Éjszaka_ÚJ, "Új adatnak előkészíti a beviteli mezőt");
            this.Éjszaka_ÚJ.UseVisualStyleBackColor = true;
            this.Éjszaka_ÚJ.Click += new System.EventHandler(this.Éjszaka_ÚJ_Click);
            // 
            // Éjszaka_Töröl
            // 
            this.Éjszaka_Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Éjszaka_Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Éjszaka_Töröl.Location = new System.Drawing.Point(397, 57);
            this.Éjszaka_Töröl.Name = "Éjszaka_Töröl";
            this.Éjszaka_Töröl.Size = new System.Drawing.Size(45, 45);
            this.Éjszaka_Töröl.TabIndex = 120;
            this.toolTip1.SetToolTip(this.Éjszaka_Töröl, "Törli az adatokat");
            this.Éjszaka_Töröl.UseVisualStyleBackColor = true;
            this.Éjszaka_Töröl.Click += new System.EventHandler(this.Éjszaka_Töröl_Click);
            // 
            // Éjszaka_Ok
            // 
            this.Éjszaka_Ok.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Éjszaka_Ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Éjszaka_Ok.Location = new System.Drawing.Point(346, 6);
            this.Éjszaka_Ok.Name = "Éjszaka_Ok";
            this.Éjszaka_Ok.Size = new System.Drawing.Size(45, 45);
            this.Éjszaka_Ok.TabIndex = 118;
            this.toolTip1.SetToolTip(this.Éjszaka_Ok, "Rögzít / Módosít");
            this.Éjszaka_Ok.UseVisualStyleBackColor = true;
            this.Éjszaka_Ok.Click += new System.EventHandler(this.Éjszaka_Ok_Click);
            // 
            // ÉBeosztásSzöveg
            // 
            this.ÉBeosztásSzöveg.Location = new System.Drawing.Point(147, 74);
            this.ÉBeosztásSzöveg.Name = "ÉBeosztásSzöveg";
            this.ÉBeosztásSzöveg.Size = new System.Drawing.Size(140, 26);
            this.ÉBeosztásSzöveg.TabIndex = 117;
            // 
            // ÉBeoKód
            // 
            this.ÉBeoKód.Location = new System.Drawing.Point(147, 40);
            this.ÉBeoKód.Name = "ÉBeoKód";
            this.ÉBeoKód.Size = new System.Drawing.Size(140, 26);
            this.ÉBeoKód.TabIndex = 116;
            // 
            // Label40
            // 
            this.Label40.AutoSize = true;
            this.Label40.Location = new System.Drawing.Point(6, 12);
            this.Label40.Name = "Label40";
            this.Label40.Size = new System.Drawing.Size(78, 20);
            this.Label40.TabIndex = 124;
            this.Label40.Text = "Hétnapja:";
            // 
            // ÉhétNapja
            // 
            this.ÉhétNapja.Location = new System.Drawing.Point(147, 6);
            this.ÉhétNapja.Name = "ÉhétNapja";
            this.ÉhétNapja.Size = new System.Drawing.Size(140, 26);
            this.ÉhétNapja.TabIndex = 115;
            // 
            // Label41
            // 
            this.Label41.AutoSize = true;
            this.Label41.Location = new System.Drawing.Point(6, 46);
            this.Label41.Name = "Label41";
            this.Label41.Size = new System.Drawing.Size(110, 20);
            this.Label41.TabIndex = 123;
            this.Label41.Text = "Beosztás kód:";
            // 
            // Label42
            // 
            this.Label42.AutoSize = true;
            this.Label42.Location = new System.Drawing.Point(6, 80);
            this.Label42.Name = "Label42";
            this.Label42.Size = new System.Drawing.Size(134, 20);
            this.Label42.TabIndex = 122;
            this.Label42.Text = "Beosztás szöveg:";
            // 
            // TabPage9
            // 
            this.TabPage9.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.TabPage9.Controls.Add(this.CsoportVáltóCsop);
            this.TabPage9.Controls.Add(this.TelephelyVáltóCsop);
            this.TabPage9.Controls.Add(this.Tábla_CsopVez);
            this.TabPage9.Controls.Add(this.CsopVez_Töröl);
            this.TabPage9.Controls.Add(this.CsopVez_Ok);
            this.TabPage9.Controls.Add(this.CsopVezNév);
            this.TabPage9.Controls.Add(this.Label37);
            this.TabPage9.Controls.Add(this.Label38);
            this.TabPage9.Controls.Add(this.Label39);
            this.TabPage9.Location = new System.Drawing.Point(4, 54);
            this.TabPage9.Name = "TabPage9";
            this.TabPage9.Size = new System.Drawing.Size(1132, 352);
            this.TabPage9.TabIndex = 8;
            this.TabPage9.Text = "Csop.vez nevek";
            // 
            // CsoportVáltóCsop
            // 
            this.CsoportVáltóCsop.FormattingEnabled = true;
            this.CsoportVáltóCsop.Location = new System.Drawing.Point(156, 4);
            this.CsoportVáltóCsop.Name = "CsoportVáltóCsop";
            this.CsoportVáltóCsop.Size = new System.Drawing.Size(187, 28);
            this.CsoportVáltóCsop.TabIndex = 0;
            // 
            // TelephelyVáltóCsop
            // 
            this.TelephelyVáltóCsop.FormattingEnabled = true;
            this.TelephelyVáltóCsop.Location = new System.Drawing.Point(156, 74);
            this.TelephelyVáltóCsop.Name = "TelephelyVáltóCsop";
            this.TelephelyVáltóCsop.Size = new System.Drawing.Size(268, 28);
            this.TelephelyVáltóCsop.TabIndex = 2;
            // 
            // Tábla_CsopVez
            // 
            this.Tábla_CsopVez.AllowUserToAddRows = false;
            this.Tábla_CsopVez.AllowUserToDeleteRows = false;
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Tábla_CsopVez.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle27;
            this.Tábla_CsopVez.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_CsopVez.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle28;
            this.Tábla_CsopVez.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_CsopVez.EnableHeadersVisualStyles = false;
            this.Tábla_CsopVez.Location = new System.Drawing.Point(2, 108);
            this.Tábla_CsopVez.Name = "Tábla_CsopVez";
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle29.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            dataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla_CsopVez.RowHeadersDefaultCellStyle = dataGridViewCellStyle29;
            this.Tábla_CsopVez.RowHeadersWidth = 51;
            this.Tábla_CsopVez.Size = new System.Drawing.Size(1127, 238);
            this.Tábla_CsopVez.TabIndex = 121;
            this.Tábla_CsopVez.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CsopVez_CellClick);
            this.Tábla_CsopVez.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CsopVez_CellDoubleClick);
            this.Tábla_CsopVez.SelectionChanged += new System.EventHandler(this.Tábla_CsopVez_SelectionChanged);
            // 
            // CsopVez_Töröl
            // 
            this.CsopVez_Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.CsopVez_Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsopVez_Töröl.Location = new System.Drawing.Point(468, 55);
            this.CsopVez_Töröl.Name = "CsopVez_Töröl";
            this.CsopVez_Töröl.Size = new System.Drawing.Size(45, 45);
            this.CsopVez_Töröl.TabIndex = 4;
            this.toolTip1.SetToolTip(this.CsopVez_Töröl, "Törli az adatokat");
            this.CsopVez_Töröl.UseVisualStyleBackColor = true;
            this.CsopVez_Töröl.Click += new System.EventHandler(this.CsopVez_Töröl_Click);
            // 
            // CsopVez_Ok
            // 
            this.CsopVez_Ok.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.CsopVez_Ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsopVez_Ok.Location = new System.Drawing.Point(468, 4);
            this.CsopVez_Ok.Name = "CsopVez_Ok";
            this.CsopVez_Ok.Size = new System.Drawing.Size(45, 45);
            this.CsopVez_Ok.TabIndex = 3;
            this.toolTip1.SetToolTip(this.CsopVez_Ok, "Rögzít / Módosít");
            this.CsopVez_Ok.UseVisualStyleBackColor = true;
            this.CsopVez_Ok.Click += new System.EventHandler(this.CsopVez_Ok_Click);
            // 
            // CsopVezNév
            // 
            this.CsopVezNév.Location = new System.Drawing.Point(156, 40);
            this.CsopVezNév.Name = "CsopVezNév";
            this.CsopVezNév.Size = new System.Drawing.Size(294, 26);
            this.CsopVezNév.TabIndex = 1;
            // 
            // Label37
            // 
            this.Label37.AutoSize = true;
            this.Label37.Location = new System.Drawing.Point(6, 12);
            this.Label37.Name = "Label37";
            this.Label37.Size = new System.Drawing.Size(144, 20);
            this.Label37.TabIndex = 120;
            this.Label37.Text = "Csoport elnevezés:";
            // 
            // Label38
            // 
            this.Label38.AutoSize = true;
            this.Label38.Location = new System.Drawing.Point(6, 46);
            this.Label38.Name = "Label38";
            this.Label38.Size = new System.Drawing.Size(145, 20);
            this.Label38.TabIndex = 119;
            this.Label38.Text = "Csoportvezető név:";
            // 
            // Label39
            // 
            this.Label39.AutoSize = true;
            this.Label39.Location = new System.Drawing.Point(6, 80);
            this.Label39.Name = "Label39";
            this.Label39.Size = new System.Drawing.Size(80, 20);
            this.Label39.TabIndex = 118;
            this.Label39.Text = "Telephely:";
            // 
            // Chk_CTRL
            // 
            this.Chk_CTRL.AutoSize = true;
            this.Chk_CTRL.Location = new System.Drawing.Point(960, 56);
            this.Chk_CTRL.Name = "Chk_CTRL";
            this.Chk_CTRL.Size = new System.Drawing.Size(127, 24);
            this.Chk_CTRL.TabIndex = 65;
            this.Chk_CTRL.Text = "CTRL nyomva";
            this.Chk_CTRL.UseVisualStyleBackColor = true;
            this.Chk_CTRL.Visible = false;
            // 
            // Button13
            // 
            this.Button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button13.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Button13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button13.Location = new System.Drawing.Point(1088, 11);
            this.Button13.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Button13.Name = "Button13";
            this.Button13.Size = new System.Drawing.Size(40, 40);
            this.Button13.TabIndex = 62;
            this.toolTip1.SetToolTip(this.Button13, "Súgó");
            this.Button13.UseVisualStyleBackColor = true;
            this.Button13.Click += new System.EventHandler(this.Súgó_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(15, 25);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(930, 25);
            this.Holtart.TabIndex = 66;
            this.Holtart.Visible = false;
            // 
            // Ablak_Váltós
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SeaGreen;
            this.ClientSize = new System.Drawing.Size(1140, 469);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Chk_CTRL);
            this.Controls.Add(this.Fülek);
            this.Controls.Add(this.Button13);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Váltós";
            this.Text = "Váltós munkarend és Túlóra";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Váltós_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_Váltós_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Ablak_Váltós_KeyUp);
            this.Fülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_BeoKód)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Keret)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Éves_Tábla)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.TabPage4.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Munkarend)).EndInit();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Csoport_Tábla)).EndInit();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.TabPage5.ResumeLayout(false);
            this.TabPage5.PerformLayout();
            this.Panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Nappalos)).EndInit();
            this.TabPage6.ResumeLayout(false);
            this.TabPage6.PerformLayout();
            this.Panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla9)).EndInit();
            this.TabPage7.ResumeLayout(false);
            this.TabPage7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Elvont)).EndInit();
            this.TabPage8.ResumeLayout(false);
            this.TabPage8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_VáltMunka)).EndInit();
            this.TabPage10.ResumeLayout(false);
            this.TabPage10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_Éjszaka)).EndInit();
            this.TabPage9.ResumeLayout(false);
            this.TabPage9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_CsopVez)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal Button Button13;
        internal TabControl Fülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal TabPage TabPage4;
        internal TabPage TabPage5;
        internal TabPage TabPage6;
        internal TabPage TabPage7;
        internal TabPage TabPage8;
        internal TabPage TabPage9;
        internal TabPage TabPage10;
        internal Button Tábla_BeoKód_Új;
        internal Button Tábla_BeoKód_Töröl;
        internal Button Tábla_BeoKód_OK;
        internal TextBox Túlóra;
        internal TextBox Beosztáskód;
        internal TextBox Túlóraoka;
        internal Label Label5;
        internal Label Label4;
        internal Label Label3;
        internal Label Label2;
        internal Label Label1;
        internal ComboBox Telephely;
        internal Label Label57;
        internal DateTimePicker Végeidő;
        internal DateTimePicker Kezdőidő;
        internal DataGridView Tábla_BeoKód;
        internal DataGridView Tábla_Keret;
        internal TextBox Túlparancs;
        internal TextBox Túlhatár;
        internal Label Label6;
        internal Label Label7;
        internal ComboBox Túltelephely;
        internal Label Label8;
        internal Button Tábla_Keret_Új;
        internal Button Tábla_Keret_Töröl;
        internal Button Tábla_Keret_OK;
        internal Button Éves_Generál;
        internal TextBox ÉvesZKnap;
        internal TextBox ÉvesÉv;
        internal Label Label10;
        internal Label Label11;
        internal ComboBox ÉvesTelephely;
        internal Label Label12;
        internal Button Éves_Új;
        internal Button Éves_Töröl;
        internal Button Éves_Ok;
        internal DataGridView Éves_Tábla;
        internal TextBox ÉvesTperc;
        internal Label Label16;
        internal TextBox ÉvesEPnap;
        internal Label Label15;
        internal TextBox ÉvesFélév;
        internal Label Label14;
        internal ComboBox ÉvesCsoport;
        internal Label Label13;
        internal Panel Panel2;
        internal ComboBox CsoportCombo;
        internal Label Label22;
        internal Label Label21;
        internal Label Label20;
        internal Label Label19;
        internal TextBox Ciklusnap;
        internal TextBox MegnevezésText;
        internal TextBox Id;
        internal Label Label18;
        internal Button Csoport_OK;
        internal Label Label17;
        internal Panel Panel1;
        internal ListBox TurnusokLista;
        internal TextBox TurnusText;
        internal Button Turnus_Ok;
        internal Button Turnus_Töröl;
        internal DataGridView Csoport_Tábla;
        internal DateTimePicker Kezdődátum;
        internal Panel Panel3;
        internal DataGridView Tábla_Munkarend;
        internal Button MunkaRend_OK;
        internal Button MunkaRend_Töröl;
        internal TextBox Munkarendelnevezés;
        internal TextBox Munkaidő;
        internal Label Label23;
        internal Label Label24;
        internal Button Nappal_Számol;
        internal Button Nappal_Alap;
        internal Button NappaloS_Tábla_Friss;
        internal Button Nappal_Ok;
        internal DateTimePicker Dátumnappal;
        internal Label Label25;
        internal DataGridView Tábla_Nappalos;
        internal TextBox Választott;
        internal ComboBox Nappaloslenyíló;
        internal Button Nappalos_Excel;
        internal Button Excelkészítés;
        internal ComboBox VváltósCsoport;
        internal Label Label27;
        internal DataGridView Tábla9;
        internal TextBox VálasztottVáltó;
        internal ComboBox VáltósLenyíló;
        internal Button Command33;
        internal Button Command36;
        internal Button Command34;
        internal Button Command30;
        internal DateTimePicker VáltósNaptár;
        internal Label Label26;
        internal ComboBox ElvontCsoport;
        internal DateTimePicker ElvontDátum;
        internal ComboBox ElvontTelephely;
        internal Button Elvont_Frissít;
        internal TextBox ElvontÉv;
        internal Button Elvont_Generált;
        internal Button Elvont_Új;
        internal Button Elvont_Töröl;
        internal Button Elvont_OK;
        internal Label Label33;
        internal Label Label32;
        internal Label Label31;
        internal Label Label30;
        internal Label Label29;
        internal Label Label28;
        internal ComboBox SzűrtTelephely;
        internal DataGridView Tábla_Elvont;
        internal DataGridView Tábla_VáltMunka;
        internal Button VáltMunka_Feljebb;
        internal Button VáltMunka_Új;
        internal Button VáltMunka_Töröl;
        internal Button VáltMunka_OK;
        internal TextBox BeosztásSzöveg;
        internal TextBox VáltMunkBeoKód;
        internal Label Label36;
        internal TextBox Hétnapja;
        internal Label Label34;
        internal Label Label35;
        internal ComboBox CsoportVáltóCsop;
        internal ComboBox TelephelyVáltóCsop;
        internal DataGridView Tábla_CsopVez;
        internal Button CsopVez_Töröl;
        internal Button CsopVez_Ok;
        internal TextBox CsopVezNév;
        internal Label Label37;
        internal Label Label38;
        internal Label Label39;
        internal DataGridView Tábla_Éjszaka;
        internal Button Éjszaka_Feljebb;
        internal Button Éjszaka_ÚJ;
        internal Button Éjszaka_Töröl;
        internal Button Éjszaka_Ok;
        internal TextBox ÉBeosztásSzöveg;
        internal TextBox ÉBeoKód;
        internal Label Label40;
        internal TextBox ÉhétNapja;
        internal Label Label41;
        internal Label Label42;
        internal Button Csoport_Töröl;
        internal Panel Panel4;
        internal CheckBox Chk_CTRL;
        internal Panel Panel5;
        internal Button Éves_Frissít;
        private ToolTip toolTip1;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button BEOkódFriss;
        internal Button TúlóraFrissít;
        internal Label Label9;
    }
}