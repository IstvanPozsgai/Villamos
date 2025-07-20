using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    public partial class Ablak_MEO_kerék : Form
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_MEO_kerék));
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Lapfülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Töröl = new System.Windows.Forms.Button();
            this.Btn_Mérés_Rögz_Frissit = new System.Windows.Forms.Button();
            this.Rögzít = new System.Windows.Forms.Button();
            this.Telephely = new System.Windows.Forms.ComboBox();
            this.Pályaszám = new System.Windows.Forms.ComboBox();
            this.Típus2 = new System.Windows.Forms.TextBox();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.BtnkijelölTelephelytörlés = new System.Windows.Forms.Button();
            this.BtnKijelölTelephely = new System.Windows.Forms.Button();
            this.BtnkijelölTípustörlés = new System.Windows.Forms.Button();
            this.BtnKijelölTípus = new System.Windows.Forms.Button();
            this.TelephelyList = new System.Windows.Forms.CheckedListBox();
            this.Típuslista = new System.Windows.Forms.CheckedListBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.ListaTábla = new System.Windows.Forms.DataGridView();
            this.Btn_Mérés_Lista_Frissit = new System.Windows.Forms.Button();
            this.Rögzítő1 = new System.Windows.Forms.ComboBox();
            this.Dátumtól = new System.Windows.Forms.DateTimePicker();
            this.Dátumig = new System.Windows.Forms.DateTimePicker();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Excellekérdezés = new System.Windows.Forms.Button();
            this.TabPage3 = new System.Windows.Forms.TabPage();
            this.BtnKijelölTelephely1Törlés = new System.Windows.Forms.Button();
            this.BtnKijelölTelephely1 = new System.Windows.Forms.Button();
            this.BtnKijelölTípus1Törlés = new System.Windows.Forms.Button();
            this.BtnKijelölTípus1 = new System.Windows.Forms.Button();
            this.TelephelyList1 = new System.Windows.Forms.CheckedListBox();
            this.Típuslista1 = new System.Windows.Forms.CheckedListBox();
            this.LekérdTábla = new System.Windows.Forms.DataGridView();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Btn_Mérés_Frissit = new System.Windows.Forms.Button();
            this.Btn_Mérés_Excel = new System.Windows.Forms.Button();
            this.TabPage4 = new System.Windows.Forms.TabPage();
            this.Konvertálás = new System.Windows.Forms.Button();
            this.FelhasználóTábla = new System.Windows.Forms.DataGridView();
            this.Határnap = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.Típus = new System.Windows.Forms.ComboBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.Rögzítő = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Btn_Jog_Torles = new System.Windows.Forms.Button();
            this.Btn_Jog_Frissit = new System.Windows.Forms.Button();
            this.Btn_Jog_Hatarnap_Rogzit = new System.Windows.Forms.Button();
            this.Btn_Jog_Tipus_Rogzit = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Lapfülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListaTábla)).BeginInit();
            this.TabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LekérdTábla)).BeginInit();
            this.TabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FelhasználóTábla)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1246, 5);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 174;
            this.toolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Lapfülek
            // 
            this.Lapfülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Lapfülek.Controls.Add(this.TabPage1);
            this.Lapfülek.Controls.Add(this.TabPage2);
            this.Lapfülek.Controls.Add(this.TabPage3);
            this.Lapfülek.Controls.Add(this.TabPage4);
            this.Lapfülek.Location = new System.Drawing.Point(5, 56);
            this.Lapfülek.Name = "Lapfülek";
            this.Lapfülek.Padding = new System.Drawing.Point(16, 3);
            this.Lapfülek.SelectedIndex = 0;
            this.Lapfülek.Size = new System.Drawing.Size(1286, 331);
            this.Lapfülek.TabIndex = 176;
            this.Lapfülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Lapfülek_DrawItem);
            this.Lapfülek.SelectedIndexChanged += new System.EventHandler(this.LapFülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.DarkKhaki;
            this.TabPage1.Controls.Add(this.Töröl);
            this.TabPage1.Controls.Add(this.Btn_Mérés_Rögz_Frissit);
            this.TabPage1.Controls.Add(this.Rögzít);
            this.TabPage1.Controls.Add(this.Telephely);
            this.TabPage1.Controls.Add(this.Pályaszám);
            this.TabPage1.Controls.Add(this.Típus2);
            this.TabPage1.Controls.Add(this.Dátum);
            this.TabPage1.Controls.Add(this.Label15);
            this.TabPage1.Controls.Add(this.Label14);
            this.TabPage1.Controls.Add(this.Label12);
            this.TabPage1.Controls.Add(this.Label11);
            this.TabPage1.Controls.Add(this.Tábla);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(1278, 298);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "Mérés rögzítés";
            // 
            // Töröl
            // 
            this.Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Töröl.Location = new System.Drawing.Point(216, 24);
            this.Töröl.Name = "Töröl";
            this.Töröl.Size = new System.Drawing.Size(45, 45);
            this.Töröl.TabIndex = 157;
            this.toolTip1.SetToolTip(this.Töröl, "Törli az adatokat");
            this.Töröl.UseVisualStyleBackColor = true;
            this.Töröl.Click += new System.EventHandler(this.Töröl_Click);
            // 
            // Btn_Mérés_Rögz_Frissit
            // 
            this.Btn_Mérés_Rögz_Frissit.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Btn_Mérés_Rögz_Frissit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Mérés_Rögz_Frissit.Location = new System.Drawing.Point(216, 165);
            this.Btn_Mérés_Rögz_Frissit.Name = "Btn_Mérés_Rögz_Frissit";
            this.Btn_Mérés_Rögz_Frissit.Size = new System.Drawing.Size(45, 45);
            this.Btn_Mérés_Rögz_Frissit.TabIndex = 156;
            this.toolTip1.SetToolTip(this.Btn_Mérés_Rögz_Frissit, "Frissíti a táblázatot");
            this.Btn_Mérés_Rögz_Frissit.UseVisualStyleBackColor = true;
            this.Btn_Mérés_Rögz_Frissit.Click += new System.EventHandler(this.Btn_Mérés_Rögz_Frissit_Click);
            // 
            // Rögzít
            // 
            this.Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzít.Location = new System.Drawing.Point(216, 227);
            this.Rögzít.Name = "Rögzít";
            this.Rögzít.Size = new System.Drawing.Size(45, 45);
            this.Rögzít.TabIndex = 155;
            this.toolTip1.SetToolTip(this.Rögzít, "Rögzíti/módosítja az adatokat");
            this.Rögzít.UseVisualStyleBackColor = true;
            this.Rögzít.Click += new System.EventHandler(this.Rögzít_Click);
            // 
            // Telephely
            // 
            this.Telephely.FormattingEnabled = true;
            this.Telephely.Location = new System.Drawing.Point(10, 110);
            this.Telephely.Name = "Telephely";
            this.Telephely.Size = new System.Drawing.Size(179, 28);
            this.Telephely.TabIndex = 154;
            this.Telephely.SelectedIndexChanged += new System.EventHandler(this.Telephely_SelectedIndexChanged);
            // 
            // Pályaszám
            // 
            this.Pályaszám.DropDownHeight = 350;
            this.Pályaszám.FormattingEnabled = true;
            this.Pályaszám.IntegralHeight = false;
            this.Pályaszám.Location = new System.Drawing.Point(10, 41);
            this.Pályaszám.Name = "Pályaszám";
            this.Pályaszám.Size = new System.Drawing.Size(179, 28);
            this.Pályaszám.Sorted = true;
            this.Pályaszám.TabIndex = 153;
            this.Pályaszám.SelectedIndexChanged += new System.EventHandler(this.Pályaszám_SelectedIndexChanged);
            this.Pályaszám.Leave += new System.EventHandler(this.Pályaszám_LostFocus);
            // 
            // Típus2
            // 
            this.Típus2.Location = new System.Drawing.Point(10, 184);
            this.Típus2.Name = "Típus2";
            this.Típus2.Size = new System.Drawing.Size(152, 26);
            this.Típus2.TabIndex = 152;
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(10, 246);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(105, 26);
            this.Dátum.TabIndex = 151;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(6, 213);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(61, 20);
            this.Label15.TabIndex = 87;
            this.Label15.Text = "Dátum:";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(6, 149);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(51, 20);
            this.Label14.TabIndex = 86;
            this.Label14.Text = "Típus:";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(6, 87);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(80, 20);
            this.Label12.TabIndex = 85;
            this.Label12.Text = "Telephely:";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(6, 18);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(89, 20);
            this.Label11.TabIndex = 84;
            this.Label11.Text = "Pályaszám:";
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(267, 6);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.RowHeadersWidth = 51;
            this.Tábla.Size = new System.Drawing.Size(1005, 286);
            this.Tábla.TabIndex = 83;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.LightSkyBlue;
            this.TabPage2.Controls.Add(this.BtnkijelölTelephelytörlés);
            this.TabPage2.Controls.Add(this.BtnKijelölTelephely);
            this.TabPage2.Controls.Add(this.BtnkijelölTípustörlés);
            this.TabPage2.Controls.Add(this.BtnKijelölTípus);
            this.TabPage2.Controls.Add(this.TelephelyList);
            this.TabPage2.Controls.Add(this.Típuslista);
            this.TabPage2.Controls.Add(this.Label8);
            this.TabPage2.Controls.Add(this.Label7);
            this.TabPage2.Controls.Add(this.ListaTábla);
            this.TabPage2.Controls.Add(this.Btn_Mérés_Lista_Frissit);
            this.TabPage2.Controls.Add(this.Rögzítő1);
            this.TabPage2.Controls.Add(this.Dátumtól);
            this.TabPage2.Controls.Add(this.Dátumig);
            this.TabPage2.Controls.Add(this.Label6);
            this.TabPage2.Controls.Add(this.Label5);
            this.TabPage2.Controls.Add(this.Label4);
            this.TabPage2.Controls.Add(this.Excellekérdezés);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(1278, 298);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Rögzítési listák";
            // 
            // BtnkijelölTelephelytörlés
            // 
            this.BtnkijelölTelephelytörlés.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.BtnkijelölTelephelytörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnkijelölTelephelytörlés.Location = new System.Drawing.Point(573, 17);
            this.BtnkijelölTelephelytörlés.Name = "BtnkijelölTelephelytörlés";
            this.BtnkijelölTelephelytörlés.Size = new System.Drawing.Size(45, 45);
            this.BtnkijelölTelephelytörlés.TabIndex = 160;
            this.toolTip1.SetToolTip(this.BtnkijelölTelephelytörlés, "Kijelölések törlése");
            this.BtnkijelölTelephelytörlés.UseVisualStyleBackColor = true;
            this.BtnkijelölTelephelytörlés.Click += new System.EventHandler(this.BtnkijelölTelephelytörlés_Click);
            // 
            // BtnKijelölTelephely
            // 
            this.BtnKijelölTelephely.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.BtnKijelölTelephely.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKijelölTelephely.Location = new System.Drawing.Point(522, 17);
            this.BtnKijelölTelephely.Name = "BtnKijelölTelephely";
            this.BtnKijelölTelephely.Size = new System.Drawing.Size(45, 45);
            this.BtnKijelölTelephely.TabIndex = 159;
            this.toolTip1.SetToolTip(this.BtnKijelölTelephely, "Mindent kijelöl");
            this.BtnKijelölTelephely.UseVisualStyleBackColor = true;
            this.BtnKijelölTelephely.Click += new System.EventHandler(this.BtnKijelölTelephely_Click);
            // 
            // BtnkijelölTípustörlés
            // 
            this.BtnkijelölTípustörlés.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.BtnkijelölTípustörlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnkijelölTípustörlés.Location = new System.Drawing.Point(215, 15);
            this.BtnkijelölTípustörlés.Name = "BtnkijelölTípustörlés";
            this.BtnkijelölTípustörlés.Size = new System.Drawing.Size(45, 45);
            this.BtnkijelölTípustörlés.TabIndex = 158;
            this.toolTip1.SetToolTip(this.BtnkijelölTípustörlés, "Kijelölések törlése");
            this.BtnkijelölTípustörlés.UseVisualStyleBackColor = true;
            this.BtnkijelölTípustörlés.Click += new System.EventHandler(this.BtnkijelölTípustörlés_Click);
            // 
            // BtnKijelölTípus
            // 
            this.BtnKijelölTípus.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.BtnKijelölTípus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKijelölTípus.Location = new System.Drawing.Point(164, 15);
            this.BtnKijelölTípus.Name = "BtnKijelölTípus";
            this.BtnKijelölTípus.Size = new System.Drawing.Size(45, 45);
            this.BtnKijelölTípus.TabIndex = 157;
            this.toolTip1.SetToolTip(this.BtnKijelölTípus, "Mindent kijelöl");
            this.BtnKijelölTípus.UseVisualStyleBackColor = true;
            this.BtnKijelölTípus.Click += new System.EventHandler(this.BtnKijelölTípus_Click);
            // 
            // TelephelyList
            // 
            this.TelephelyList.CheckOnClick = true;
            this.TelephelyList.FormattingEnabled = true;
            this.TelephelyList.Location = new System.Drawing.Point(266, 33);
            this.TelephelyList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TelephelyList.Name = "TelephelyList";
            this.TelephelyList.Size = new System.Drawing.Size(250, 25);
            this.TelephelyList.TabIndex = 142;
            this.TelephelyList.MouseLeave += new System.EventHandler(this.TelephelyList_MouseLeave);
            this.TelephelyList.MouseHover += new System.EventHandler(this.TelephelyList_MouseHover);
            // 
            // Típuslista
            // 
            this.Típuslista.CheckOnClick = true;
            this.Típuslista.FormattingEnabled = true;
            this.Típuslista.Location = new System.Drawing.Point(10, 33);
            this.Típuslista.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Típuslista.Name = "Típuslista";
            this.Típuslista.Size = new System.Drawing.Size(148, 25);
            this.Típuslista.Sorted = true;
            this.Típuslista.TabIndex = 140;
            this.Típuslista.MouseLeave += new System.EventHandler(this.Típuslista_MouseLeave);
            this.Típuslista.MouseHover += new System.EventHandler(this.Típuslista_MouseHover);
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(6, 3);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(51, 20);
            this.Label8.TabIndex = 156;
            this.Label8.Text = "Típus:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(262, 3);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(80, 20);
            this.Label7.TabIndex = 155;
            this.Label7.Text = "Telephely:";
            // 
            // ListaTábla
            // 
            this.ListaTábla.AllowUserToAddRows = false;
            this.ListaTábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ListaTábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.ListaTábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ListaTábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.ListaTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListaTábla.EnableHeadersVisualStyles = false;
            this.ListaTábla.Location = new System.Drawing.Point(6, 66);
            this.ListaTábla.Name = "ListaTábla";
            this.ListaTábla.RowHeadersVisible = false;
            this.ListaTábla.RowHeadersWidth = 51;
            this.ListaTábla.Size = new System.Drawing.Size(1266, 226);
            this.ListaTábla.TabIndex = 154;
            // 
            // Btn_Mérés_Lista_Frissit
            // 
            this.Btn_Mérés_Lista_Frissit.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Btn_Mérés_Lista_Frissit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Mérés_Lista_Frissit.Location = new System.Drawing.Point(1119, 14);
            this.Btn_Mérés_Lista_Frissit.Name = "Btn_Mérés_Lista_Frissit";
            this.Btn_Mérés_Lista_Frissit.Size = new System.Drawing.Size(45, 45);
            this.Btn_Mérés_Lista_Frissit.TabIndex = 152;
            this.toolTip1.SetToolTip(this.Btn_Mérés_Lista_Frissit, "Frissíti a táblázatot");
            this.Btn_Mérés_Lista_Frissit.UseVisualStyleBackColor = true;
            this.Btn_Mérés_Lista_Frissit.Click += new System.EventHandler(this.Btn_Mérés_Lista_Frissit_Click);
            // 
            // Rögzítő1
            // 
            this.Rögzítő1.FormattingEnabled = true;
            this.Rögzítő1.Location = new System.Drawing.Point(845, 30);
            this.Rögzítő1.Name = "Rögzítő1";
            this.Rögzítő1.Size = new System.Drawing.Size(179, 28);
            this.Rögzítő1.TabIndex = 151;
            // 
            // Dátumtól
            // 
            this.Dátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumtól.Location = new System.Drawing.Point(624, 32);
            this.Dátumtól.Name = "Dátumtól";
            this.Dátumtól.Size = new System.Drawing.Size(105, 26);
            this.Dátumtól.TabIndex = 150;
            // 
            // Dátumig
            // 
            this.Dátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumig.Location = new System.Drawing.Point(734, 32);
            this.Dátumig.Name = "Dátumig";
            this.Dátumig.Size = new System.Drawing.Size(105, 26);
            this.Dátumig.TabIndex = 149;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(730, 2);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(73, 20);
            this.Label6.TabIndex = 148;
            this.Label6.Text = "Dátumig:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(843, 2);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(68, 20);
            this.Label5.TabIndex = 147;
            this.Label5.Text = "Rögzítő:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(620, 3);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(78, 20);
            this.Label4.TabIndex = 146;
            this.Label4.Text = "Dátumtól:";
            // 
            // Excellekérdezés
            // 
            this.Excellekérdezés.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excellekérdezés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excellekérdezés.Location = new System.Drawing.Point(1170, 15);
            this.Excellekérdezés.Name = "Excellekérdezés";
            this.Excellekérdezés.Size = new System.Drawing.Size(45, 45);
            this.Excellekérdezés.TabIndex = 153;
            this.toolTip1.SetToolTip(this.Excellekérdezés, "Excel táblázatot készít a táblázat adataiból");
            this.Excellekérdezés.UseVisualStyleBackColor = true;
            this.Excellekérdezés.Click += new System.EventHandler(this.Excellekérdezés_Click);
            // 
            // TabPage3
            // 
            this.TabPage3.BackColor = System.Drawing.Color.LightSkyBlue;
            this.TabPage3.Controls.Add(this.BtnKijelölTelephely1Törlés);
            this.TabPage3.Controls.Add(this.BtnKijelölTelephely1);
            this.TabPage3.Controls.Add(this.BtnKijelölTípus1Törlés);
            this.TabPage3.Controls.Add(this.BtnKijelölTípus1);
            this.TabPage3.Controls.Add(this.TelephelyList1);
            this.TabPage3.Controls.Add(this.Típuslista1);
            this.TabPage3.Controls.Add(this.LekérdTábla);
            this.TabPage3.Controls.Add(this.Label9);
            this.TabPage3.Controls.Add(this.Label10);
            this.TabPage3.Controls.Add(this.Btn_Mérés_Frissit);
            this.TabPage3.Controls.Add(this.Btn_Mérés_Excel);
            this.TabPage3.Location = new System.Drawing.Point(4, 29);
            this.TabPage3.Name = "TabPage3";
            this.TabPage3.Size = new System.Drawing.Size(1278, 298);
            this.TabPage3.TabIndex = 2;
            this.TabPage3.Text = "Mérések listázása";
            // 
            // BtnKijelölTelephely1Törlés
            // 
            this.BtnKijelölTelephely1Törlés.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.BtnKijelölTelephely1Törlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKijelölTelephely1Törlés.Location = new System.Drawing.Point(645, 13);
            this.BtnKijelölTelephely1Törlés.Name = "BtnKijelölTelephely1Törlés";
            this.BtnKijelölTelephely1Törlés.Size = new System.Drawing.Size(45, 45);
            this.BtnKijelölTelephely1Törlés.TabIndex = 171;
            this.toolTip1.SetToolTip(this.BtnKijelölTelephely1Törlés, "Kijelölések törlése");
            this.BtnKijelölTelephely1Törlés.UseVisualStyleBackColor = true;
            this.BtnKijelölTelephely1Törlés.Click += new System.EventHandler(this.BtnKijelölTelephely1Törlés_Click);
            // 
            // BtnKijelölTelephely1
            // 
            this.BtnKijelölTelephely1.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.BtnKijelölTelephely1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKijelölTelephely1.Location = new System.Drawing.Point(594, 13);
            this.BtnKijelölTelephely1.Name = "BtnKijelölTelephely1";
            this.BtnKijelölTelephely1.Size = new System.Drawing.Size(45, 45);
            this.BtnKijelölTelephely1.TabIndex = 170;
            this.toolTip1.SetToolTip(this.BtnKijelölTelephely1, "Mindent kijelöl");
            this.BtnKijelölTelephely1.UseVisualStyleBackColor = true;
            this.BtnKijelölTelephely1.Click += new System.EventHandler(this.BtnKijelölTelephely1_Click);
            // 
            // BtnKijelölTípus1Törlés
            // 
            this.BtnKijelölTípus1Törlés.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.BtnKijelölTípus1Törlés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKijelölTípus1Törlés.Location = new System.Drawing.Point(218, 15);
            this.BtnKijelölTípus1Törlés.Name = "BtnKijelölTípus1Törlés";
            this.BtnKijelölTípus1Törlés.Size = new System.Drawing.Size(45, 45);
            this.BtnKijelölTípus1Törlés.TabIndex = 169;
            this.toolTip1.SetToolTip(this.BtnKijelölTípus1Törlés, "Kijelölések törlése");
            this.BtnKijelölTípus1Törlés.UseVisualStyleBackColor = true;
            this.BtnKijelölTípus1Törlés.Click += new System.EventHandler(this.BtnKijelölTípus1Törlés_Click);
            // 
            // BtnKijelölTípus1
            // 
            this.BtnKijelölTípus1.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.BtnKijelölTípus1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnKijelölTípus1.Location = new System.Drawing.Point(167, 15);
            this.BtnKijelölTípus1.Name = "BtnKijelölTípus1";
            this.BtnKijelölTípus1.Size = new System.Drawing.Size(45, 45);
            this.BtnKijelölTípus1.TabIndex = 168;
            this.toolTip1.SetToolTip(this.BtnKijelölTípus1, "Mindent kijelöl");
            this.BtnKijelölTípus1.UseVisualStyleBackColor = true;
            this.BtnKijelölTípus1.Click += new System.EventHandler(this.BtnKijelölTípus1_Click);
            // 
            // TelephelyList1
            // 
            this.TelephelyList1.CheckOnClick = true;
            this.TelephelyList1.FormattingEnabled = true;
            this.TelephelyList1.Location = new System.Drawing.Point(338, 33);
            this.TelephelyList1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TelephelyList1.Name = "TelephelyList1";
            this.TelephelyList1.Size = new System.Drawing.Size(250, 25);
            this.TelephelyList1.TabIndex = 159;
            this.TelephelyList1.MouseLeave += new System.EventHandler(this.TelephelyList1_MouseLeave);
            this.TelephelyList1.MouseHover += new System.EventHandler(this.TelephelyList1_MouseHover);
            // 
            // Típuslista1
            // 
            this.Típuslista1.CheckOnClick = true;
            this.Típuslista1.FormattingEnabled = true;
            this.Típuslista1.Location = new System.Drawing.Point(10, 33);
            this.Típuslista1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Típuslista1.Name = "Típuslista1";
            this.Típuslista1.Size = new System.Drawing.Size(148, 25);
            this.Típuslista1.Sorted = true;
            this.Típuslista1.TabIndex = 157;
            this.Típuslista1.MouseLeave += new System.EventHandler(this.Típuslista1_MouseLeave);
            this.Típuslista1.MouseHover += new System.EventHandler(this.Típuslista1_MouseHover);
            // 
            // LekérdTábla
            // 
            this.LekérdTábla.AllowUserToAddRows = false;
            this.LekérdTábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.LekérdTábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.LekérdTábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.LekérdTábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.LekérdTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.LekérdTábla.DefaultCellStyle = dataGridViewCellStyle7;
            this.LekérdTábla.EnableHeadersVisualStyles = false;
            this.LekérdTábla.Location = new System.Drawing.Point(5, 63);
            this.LekérdTábla.Name = "LekérdTábla";
            this.LekérdTábla.RowHeadersVisible = false;
            this.LekérdTábla.RowHeadersWidth = 51;
            this.LekérdTábla.Size = new System.Drawing.Size(1270, 232);
            this.LekérdTábla.TabIndex = 167;
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(6, 3);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(51, 20);
            this.Label9.TabIndex = 166;
            this.Label9.Text = "Típus:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(334, 3);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(80, 20);
            this.Label10.TabIndex = 165;
            this.Label10.Text = "Telephely:";
            // 
            // Btn_Mérés_Frissit
            // 
            this.Btn_Mérés_Frissit.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Btn_Mérés_Frissit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Mérés_Frissit.Location = new System.Drawing.Point(798, 15);
            this.Btn_Mérés_Frissit.Name = "Btn_Mérés_Frissit";
            this.Btn_Mérés_Frissit.Size = new System.Drawing.Size(45, 45);
            this.Btn_Mérés_Frissit.TabIndex = 163;
            this.toolTip1.SetToolTip(this.Btn_Mérés_Frissit, "Frissíti a táblázatot");
            this.Btn_Mérés_Frissit.UseVisualStyleBackColor = true;
            this.Btn_Mérés_Frissit.Click += new System.EventHandler(this.Btn_Mérés_Frissit_Click);
            // 
            // Btn_Mérés_Excel
            // 
            this.Btn_Mérés_Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Btn_Mérés_Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Mérés_Excel.Location = new System.Drawing.Point(848, 15);
            this.Btn_Mérés_Excel.Name = "Btn_Mérés_Excel";
            this.Btn_Mérés_Excel.Size = new System.Drawing.Size(45, 45);
            this.Btn_Mérés_Excel.TabIndex = 164;
            this.toolTip1.SetToolTip(this.Btn_Mérés_Excel, "Excel táblázatot készít a táblázat adataiból");
            this.Btn_Mérés_Excel.UseVisualStyleBackColor = true;
            this.Btn_Mérés_Excel.Click += new System.EventHandler(this.Btn_Mérés_Excel_Click);
            // 
            // TabPage4
            // 
            this.TabPage4.BackColor = System.Drawing.Color.Olive;
            this.TabPage4.Controls.Add(this.Konvertálás);
            this.TabPage4.Controls.Add(this.FelhasználóTábla);
            this.TabPage4.Controls.Add(this.Határnap);
            this.TabPage4.Controls.Add(this.Label3);
            this.TabPage4.Controls.Add(this.Típus);
            this.TabPage4.Controls.Add(this.Label2);
            this.TabPage4.Controls.Add(this.Rögzítő);
            this.TabPage4.Controls.Add(this.Label1);
            this.TabPage4.Controls.Add(this.Btn_Jog_Torles);
            this.TabPage4.Controls.Add(this.Btn_Jog_Frissit);
            this.TabPage4.Controls.Add(this.Btn_Jog_Hatarnap_Rogzit);
            this.TabPage4.Controls.Add(this.Btn_Jog_Tipus_Rogzit);
            this.TabPage4.Location = new System.Drawing.Point(4, 29);
            this.TabPage4.Name = "TabPage4";
            this.TabPage4.Size = new System.Drawing.Size(1278, 298);
            this.TabPage4.TabIndex = 3;
            this.TabPage4.Text = "Jogosultság kiosztás";
            // 
            // Konvertálás
            // 
            this.Konvertálás.BackgroundImage = global::Villamos.Properties.Resources.Document_preferences;
            this.Konvertálás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Konvertálás.Location = new System.Drawing.Point(11, 214);
            this.Konvertálás.Name = "Konvertálás";
            this.Konvertálás.Size = new System.Drawing.Size(45, 45);
            this.Konvertálás.TabIndex = 83;
            this.toolTip1.SetToolTip(this.Konvertálás, "Kerékmérési adatok konvertálása *.csv  fájl(ok)ból *.xlsx fájlba.");
            this.Konvertálás.UseVisualStyleBackColor = true;
            this.Konvertálás.Click += new System.EventHandler(this.Konvertálás_Click);
            // 
            // FelhasználóTábla
            // 
            this.FelhasználóTábla.AllowUserToAddRows = false;
            this.FelhasználóTábla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.FelhasználóTábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle8;
            this.FelhasználóTábla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.FelhasználóTábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.FelhasználóTábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FelhasználóTábla.EnableHeadersVisualStyles = false;
            this.FelhasználóTábla.Location = new System.Drawing.Point(281, 6);
            this.FelhasználóTábla.Name = "FelhasználóTábla";
            this.FelhasználóTábla.RowHeadersVisible = false;
            this.FelhasználóTábla.RowHeadersWidth = 51;
            this.FelhasználóTábla.Size = new System.Drawing.Size(994, 289);
            this.FelhasználóTábla.TabIndex = 82;
            this.FelhasználóTábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.FelhasználóTábla_CellClick);
            // 
            // Határnap
            // 
            this.Határnap.Location = new System.Drawing.Point(102, 170);
            this.Határnap.Name = "Határnap";
            this.Határnap.Size = new System.Drawing.Size(152, 26);
            this.Határnap.TabIndex = 77;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(4, 176);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(80, 20);
            this.Label3.TabIndex = 76;
            this.Label3.Text = "Határnap:";
            // 
            // Típus
            // 
            this.Típus.FormattingEnabled = true;
            this.Típus.Location = new System.Drawing.Point(96, 47);
            this.Típus.Name = "Típus";
            this.Típus.Size = new System.Drawing.Size(179, 28);
            this.Típus.Sorted = true;
            this.Típus.TabIndex = 75;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(7, 55);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(51, 20);
            this.Label2.TabIndex = 74;
            this.Label2.Text = "Típus:";
            // 
            // Rögzítő
            // 
            this.Rögzítő.FormattingEnabled = true;
            this.Rögzítő.Location = new System.Drawing.Point(96, 13);
            this.Rögzítő.Name = "Rögzítő";
            this.Rögzítő.Size = new System.Drawing.Size(179, 28);
            this.Rögzítő.Sorted = true;
            this.Rögzítő.TabIndex = 73;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(7, 21);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(68, 20);
            this.Label1.TabIndex = 72;
            this.Label1.Text = "Rögzítő:";
            // 
            // Btn_Jog_Torles
            // 
            this.Btn_Jog_Torles.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Btn_Jog_Torles.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Jog_Torles.Location = new System.Drawing.Point(11, 78);
            this.Btn_Jog_Torles.Name = "Btn_Jog_Torles";
            this.Btn_Jog_Torles.Size = new System.Drawing.Size(45, 45);
            this.Btn_Jog_Torles.TabIndex = 81;
            this.toolTip1.SetToolTip(this.Btn_Jog_Torles, "Törli az adatokat");
            this.Btn_Jog_Torles.UseVisualStyleBackColor = true;
            this.Btn_Jog_Torles.Click += new System.EventHandler(this.Btn_Jog_Torles_Click);
            // 
            // Btn_Jog_Frissit
            // 
            this.Btn_Jog_Frissit.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Btn_Jog_Frissit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Jog_Frissit.Location = new System.Drawing.Point(102, 81);
            this.Btn_Jog_Frissit.Name = "Btn_Jog_Frissit";
            this.Btn_Jog_Frissit.Size = new System.Drawing.Size(45, 45);
            this.Btn_Jog_Frissit.TabIndex = 80;
            this.toolTip1.SetToolTip(this.Btn_Jog_Frissit, "Frissíti a táblázatot");
            this.Btn_Jog_Frissit.UseVisualStyleBackColor = true;
            this.Btn_Jog_Frissit.Click += new System.EventHandler(this.Btn_Jog_Frissit_Click);
            // 
            // Btn_Jog_Hatarnap_Rogzit
            // 
            this.Btn_Jog_Hatarnap_Rogzit.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Jog_Hatarnap_Rogzit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Jog_Hatarnap_Rogzit.Location = new System.Drawing.Point(230, 214);
            this.Btn_Jog_Hatarnap_Rogzit.Name = "Btn_Jog_Hatarnap_Rogzit";
            this.Btn_Jog_Hatarnap_Rogzit.Size = new System.Drawing.Size(45, 45);
            this.Btn_Jog_Hatarnap_Rogzit.TabIndex = 79;
            this.toolTip1.SetToolTip(this.Btn_Jog_Hatarnap_Rogzit, "Rögzíti/módosítja az adatokat");
            this.Btn_Jog_Hatarnap_Rogzit.UseVisualStyleBackColor = true;
            this.Btn_Jog_Hatarnap_Rogzit.Click += new System.EventHandler(this.Btn_Jog_Hatarnap_Rogzit_Click);
            // 
            // Btn_Jog_Tipus_Rogzit
            // 
            this.Btn_Jog_Tipus_Rogzit.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Btn_Jog_Tipus_Rogzit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Jog_Tipus_Rogzit.Location = new System.Drawing.Point(230, 81);
            this.Btn_Jog_Tipus_Rogzit.Name = "Btn_Jog_Tipus_Rogzit";
            this.Btn_Jog_Tipus_Rogzit.Size = new System.Drawing.Size(45, 45);
            this.Btn_Jog_Tipus_Rogzit.TabIndex = 78;
            this.toolTip1.SetToolTip(this.Btn_Jog_Tipus_Rogzit, "Rögzíti/módosítja az adatokat");
            this.Btn_Jog_Tipus_Rogzit.UseVisualStyleBackColor = true;
            this.Btn_Jog_Tipus_Rogzit.Click += new System.EventHandler(this.Btn_Jog_Tipus_Rogzit_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(40, 15);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(1200, 30);
            this.Holtart.TabIndex = 158;
            this.Holtart.Visible = false;
            // 
            // Ablak_MEO_kerék
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.OliveDrab;
            this.ClientSize = new System.Drawing.Size(1298, 392);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Lapfülek);
            this.Controls.Add(this.BtnSúgó);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_MEO_kerék";
            this.Text = "MEO Kerékmérések adminisztrációja";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_MEO_kerék_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_MEO_kerék_Load);
            this.Lapfülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.TabPage2.ResumeLayout(false);
            this.TabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListaTábla)).EndInit();
            this.TabPage3.ResumeLayout(false);
            this.TabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LekérdTábla)).EndInit();
            this.TabPage4.ResumeLayout(false);
            this.TabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FelhasználóTábla)).EndInit();
            this.ResumeLayout(false);

        }
        internal Button BtnSúgó;
        internal TabControl Lapfülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal TabPage TabPage3;
        internal TabPage TabPage4;
        internal CheckedListBox TelephelyList;
        internal CheckedListBox Típuslista;
        internal Label Label6;
        internal Label Label5;
        internal Label Label4;
        internal ComboBox Rögzítő1;
        internal DateTimePicker Dátumtól;
        internal DateTimePicker Dátumig;
        internal Button Excellekérdezés;
        internal Button Btn_Mérés_Lista_Frissit;
        internal DataGridView ListaTábla;
        internal Label Label8;
        internal Label Label7;
        internal DataGridView LekérdTábla;
        internal Label Label9;
        internal Label Label10;
        internal Button Btn_Mérés_Excel;
        internal Button Btn_Mérés_Frissit;
        internal CheckedListBox TelephelyList1;
        internal CheckedListBox Típuslista1;
        internal TextBox Típus2;
        internal DateTimePicker Dátum;
        internal Label Label15;
        internal Label Label14;
        internal Label Label12;
        internal Label Label11;
        internal DataGridView Tábla;
        internal DataGridView FelhasználóTábla;
        internal Button Btn_Jog_Torles;
        internal Button Btn_Jog_Frissit;
        internal Button Btn_Jog_Hatarnap_Rogzit;
        internal Button Btn_Jog_Tipus_Rogzit;
        internal TextBox Határnap;
        internal Label Label3;
        internal ComboBox Típus;
        internal Label Label2;
        internal ComboBox Rögzítő;
        internal Label Label1;
        internal Button Töröl;
        internal Button Btn_Mérés_Rögz_Frissit;
        internal Button Rögzít;
        internal ComboBox Telephely;
        internal ComboBox Pályaszám;
        private ToolTip toolTip1;
        internal Button Konvertálás;
        internal Button BtnkijelölTípustörlés;
        internal Button BtnKijelölTípus;
        internal Button BtnkijelölTelephelytörlés;
        internal Button BtnKijelölTelephely;
        internal Button BtnKijelölTelephely1Törlés;
        internal Button BtnKijelölTelephely1;
        internal Button BtnKijelölTípus1Törlés;
        internal Button BtnKijelölTípus1;
        internal V_MindenEgyéb.MyProgressbar Holtart;
    }
}