using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    
    public partial class Ablak_T5C5_napütemezés : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_T5C5_napütemezés));
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Label13 = new System.Windows.Forms.Label();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Btn_Lista = new System.Windows.Forms.Button();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.Btn_hónaplistázás = new System.Windows.Forms.Button();
            this.Btn_Szerelvénylista = new System.Windows.Forms.Button();
            this.Ütemezés_lista = new System.Windows.Forms.ListBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Btn_Command3 = new System.Windows.Forms.Button();
            this.Btn_Vezénylésbeírás = new System.Windows.Forms.Button();
            this.Btn_vezénylésexcel = new System.Windows.Forms.Button();
            this.SAP_adatok = new System.Windows.Forms.Button();
            this.Kereső_hívó = new System.Windows.Forms.Button();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.BtnExcelkimenet = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Dátum
            // 
            this.Dátum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(1019, 76);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(123, 26);
            this.Dátum.TabIndex = 9;
            this.Dátum.ValueChanged += new System.EventHandler(this.Dátum_ValueChanged);
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Location = new System.Drawing.Point(5, 4);
            this.Tábla.Name = "Tábla";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla.RowHeadersWidth = 25;
            this.Tábla.Size = new System.Drawing.Size(972, 417);
            this.Tábla.TabIndex = 63;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel2.BackColor = System.Drawing.Color.Peru;
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Location = new System.Drawing.Point(983, 4);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(200, 66);
            this.Panel2.TabIndex = 71;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(5, 4);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(3, 27);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(180, 28);
            this.Cmbtelephely.TabIndex = 18;
            // 
            // Btn_Lista
            // 
            this.Btn_Lista.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Lista.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Btn_Lista.Location = new System.Drawing.Point(980, 159);
            this.Btn_Lista.Name = "Btn_Lista";
            this.Btn_Lista.Size = new System.Drawing.Size(203, 28);
            this.Btn_Lista.TabIndex = 72;
            this.Btn_Lista.Text = "Kocsik listázása";
            this.Btn_Lista.UseVisualStyleBackColor = false;
            this.Btn_Lista.Click += new System.EventHandler(this.Btn_Lista_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.Holtart.ForeColor = System.Drawing.Color.Green;
            this.Holtart.Location = new System.Drawing.Point(35, 184);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(887, 25);
            this.Holtart.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.Holtart.TabIndex = 74;
            this.Holtart.Visible = false;
            // 
            // Btn_hónaplistázás
            // 
            this.Btn_hónaplistázás.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_hónaplistázás.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Btn_hónaplistázás.Location = new System.Drawing.Point(980, 229);
            this.Btn_hónaplistázás.Name = "Btn_hónaplistázás";
            this.Btn_hónaplistázás.Size = new System.Drawing.Size(203, 30);
            this.Btn_hónaplistázás.TabIndex = 75;
            this.Btn_hónaplistázás.Text = "Hónap listázása";
            this.Btn_hónaplistázás.UseVisualStyleBackColor = false;
            this.Btn_hónaplistázás.Click += new System.EventHandler(this.Btn_hónaplistázás_Click);
            // 
            // Btn_Szerelvénylista
            // 
            this.Btn_Szerelvénylista.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Szerelvénylista.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Btn_Szerelvénylista.Location = new System.Drawing.Point(980, 193);
            this.Btn_Szerelvénylista.Name = "Btn_Szerelvénylista";
            this.Btn_Szerelvénylista.Size = new System.Drawing.Size(203, 30);
            this.Btn_Szerelvénylista.TabIndex = 76;
            this.Btn_Szerelvénylista.Text = "Szerelvény listázása";
            this.Btn_Szerelvénylista.UseVisualStyleBackColor = false;
            this.Btn_Szerelvénylista.Click += new System.EventHandler(this.Btn_Szerelvénylista_Click);
            // 
            // Ütemezés_lista
            // 
            this.Ütemezés_lista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Ütemezés_lista.FormattingEnabled = true;
            this.Ütemezés_lista.ItemHeight = 20;
            this.Ütemezés_lista.Location = new System.Drawing.Point(980, 317);
            this.Ütemezés_lista.Name = "Ütemezés_lista";
            this.Ütemezés_lista.Size = new System.Drawing.Size(203, 104);
            this.Ütemezés_lista.TabIndex = 82;
            this.Ütemezés_lista.SelectedIndexChanged += new System.EventHandler(this.Ütemezés_lista_SelectedIndexChanged);
            // 
            // Btn_Command3
            // 
            this.Btn_Command3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Command3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Btn_Command3.BackgroundImage = global::Villamos.Properties.Resources.Document_write;
            this.Btn_Command3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Command3.Location = new System.Drawing.Point(1031, 265);
            this.Btn_Command3.Name = "Btn_Command3";
            this.Btn_Command3.Size = new System.Drawing.Size(50, 50);
            this.Btn_Command3.TabIndex = 79;
            this.ToolTip1.SetToolTip(this.Btn_Command3, "Hibalista a tervezett karbantartáshoz");
            this.Btn_Command3.UseVisualStyleBackColor = false;
            this.Btn_Command3.Click += new System.EventHandler(this.Btn_Command3_Click);
            // 
            // Btn_Vezénylésbeírás
            // 
            this.Btn_Vezénylésbeírás.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_Vezénylésbeírás.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Btn_Vezénylésbeírás.BackgroundImage = global::Villamos.Properties.Resources.leadott;
            this.Btn_Vezénylésbeírás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Vezénylésbeírás.Location = new System.Drawing.Point(1082, 265);
            this.Btn_Vezénylésbeírás.Name = "Btn_Vezénylésbeírás";
            this.Btn_Vezénylésbeírás.Size = new System.Drawing.Size(50, 50);
            this.Btn_Vezénylésbeírás.TabIndex = 78;
            this.ToolTip1.SetToolTip(this.Btn_Vezénylésbeírás, "Járműkarbantartási adatokba beírja");
            this.Btn_Vezénylésbeírás.UseVisualStyleBackColor = false;
            this.Btn_Vezénylésbeírás.Click += new System.EventHandler(this.Btn_Vezénylésbeírás_Click);
            // 
            // Btn_vezénylésexcel
            // 
            this.Btn_vezénylésexcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Btn_vezénylésexcel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Btn_vezénylésexcel.BackgroundImage = global::Villamos.Properties.Resources.CALC1;
            this.Btn_vezénylésexcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_vezénylésexcel.Location = new System.Drawing.Point(980, 265);
            this.Btn_vezénylésexcel.Name = "Btn_vezénylésexcel";
            this.Btn_vezénylésexcel.Size = new System.Drawing.Size(50, 50);
            this.Btn_vezénylésexcel.TabIndex = 77;
            this.ToolTip1.SetToolTip(this.Btn_vezénylésexcel, "Feladatterv készítés");
            this.Btn_vezénylésexcel.UseVisualStyleBackColor = false;
            this.Btn_vezénylésexcel.Click += new System.EventHandler(this.Btn_vezénylésexcel_Click);
            // 
            // SAP_adatok
            // 
            this.SAP_adatok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SAP_adatok.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SAP_adatok.BackgroundImage = global::Villamos.Properties.Resources.SAP;
            this.SAP_adatok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SAP_adatok.Location = new System.Drawing.Point(1133, 265);
            this.SAP_adatok.Name = "SAP_adatok";
            this.SAP_adatok.Size = new System.Drawing.Size(50, 50);
            this.SAP_adatok.TabIndex = 92;
            this.SAP_adatok.UseVisualStyleBackColor = false;
            this.SAP_adatok.Click += new System.EventHandler(this.SAP_adatok_Click);
            // 
            // Kereső_hívó
            // 
            this.Kereső_hívó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Kereső_hívó.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Kereső_hívó.BackgroundImage = global::Villamos.Properties.Resources.Nagyító;
            this.Kereső_hívó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kereső_hívó.Location = new System.Drawing.Point(1056, 105);
            this.Kereső_hívó.Name = "Kereső_hívó";
            this.Kereső_hívó.Size = new System.Drawing.Size(50, 50);
            this.Kereső_hívó.TabIndex = 86;
            this.Kereső_hívó.UseVisualStyleBackColor = false;
            this.Kereső_hívó.Click += new System.EventHandler(this.Keresés_Click);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1133, 105);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(50, 50);
            this.BtnSúgó.TabIndex = 73;
            this.BtnSúgó.UseVisualStyleBackColor = false;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // BtnExcelkimenet
            // 
            this.BtnExcelkimenet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExcelkimenet.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnExcelkimenet.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.BtnExcelkimenet.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnExcelkimenet.Location = new System.Drawing.Point(980, 105);
            this.BtnExcelkimenet.Name = "BtnExcelkimenet";
            this.BtnExcelkimenet.Size = new System.Drawing.Size(50, 50);
            this.BtnExcelkimenet.TabIndex = 70;
            this.BtnExcelkimenet.UseVisualStyleBackColor = false;
            this.BtnExcelkimenet.Click += new System.EventHandler(this.BtnExcelkimenet_Click);
            // 
            // Ablak_T5C5_napütemezés
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SaddleBrown;
            this.ClientSize = new System.Drawing.Size(1189, 431);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.SAP_adatok);
            this.Controls.Add(this.Ütemezés_lista);
            this.Controls.Add(this.Btn_Command3);
            this.Controls.Add(this.Btn_Vezénylésbeírás);
            this.Controls.Add(this.Btn_vezénylésexcel);
            this.Controls.Add(this.Btn_Szerelvénylista);
            this.Controls.Add(this.Btn_hónaplistázás);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Btn_Lista);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.BtnExcelkimenet);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.Dátum);
            this.Controls.Add(this.Kereső_hívó);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Ablak_T5C5_napütemezés";
            this.Text = "Ablak_T5C5_napütemezés";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_T5C5_napütemezés_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_T5C5_napütemezés_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_T5C5_napütemezés_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        internal DateTimePicker Dátum;
        internal DataGridView Tábla;
        internal Button BtnExcelkimenet;
        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Button Btn_Lista;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button BtnSúgó;
        internal Button Btn_hónaplistázás;
        internal Button Btn_Szerelvénylista;
        internal Button Btn_vezénylésexcel;
        internal Button Btn_Vezénylésbeírás;
        internal Button Btn_Command3;
        internal ListBox Ütemezés_lista;
        internal Button Kereső_hívó;
        internal ToolTip ToolTip1;
        internal Button SAP_adatok;
        private Timer timer1;
    }
}