using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Villamos
{
    
    public partial class Ablak_kidobó : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_kidobó));
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.ForteBetöltés = new System.Windows.Forms.Button();
            this.Command8 = new System.Windows.Forms.Button();
            this.Command1 = new System.Windows.Forms.Button();
            this.Command2 = new System.Windows.Forms.Button();
            this.Command11 = new System.Windows.Forms.Button();
            this.Command12 = new System.Windows.Forms.Button();
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Tábla1 = new System.Windows.Forms.DataGridView();
            this.VáltozatCombo = new System.Windows.Forms.ComboBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.Forte_Beolvasás = new System.Windows.Forms.Button();
            this.Keresés = new System.Windows.Forms.Button();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Btn_Ittasági = new System.Windows.Forms.Button();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).BeginInit();
            this.SuspendLayout();
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(344, 12);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(668, 28);
            this.Holtart.TabIndex = 178;
            this.Holtart.Visible = false;
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(3, 12);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(335, 33);
            this.Panel2.TabIndex = 176;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(143, 0);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(5, 5);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(12, 93);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(123, 26);
            this.Dátum.TabIndex = 0;
            this.Dátum.ValueChanged += new System.EventHandler(this.Dátum_ValueChanged);
            // 
            // ForteBetöltés
            // 
            this.ForteBetöltés.Location = new System.Drawing.Point(12, 125);
            this.ForteBetöltés.Name = "ForteBetöltés";
            this.ForteBetöltés.Size = new System.Drawing.Size(208, 32);
            this.ForteBetöltés.TabIndex = 1;
            this.ForteBetöltés.Text = "Forte adatok beolvasása";
            this.ForteBetöltés.UseVisualStyleBackColor = true;
            this.ForteBetöltés.Click += new System.EventHandler(this.ForteBetöltés_Click);
            // 
            // Command8
            // 
            this.Command8.Location = new System.Drawing.Point(12, 481);
            this.Command8.Name = "Command8";
            this.Command8.Size = new System.Drawing.Size(208, 32);
            this.Command8.TabIndex = 7;
            this.Command8.Text = "Változatok karbantartása";
            this.Command8.UseVisualStyleBackColor = true;
            this.Command8.Click += new System.EventHandler(this.Command8_Click);
            // 
            // Command1
            // 
            this.Command1.Location = new System.Drawing.Point(12, 387);
            this.Command1.Name = "Command1";
            this.Command1.Size = new System.Drawing.Size(208, 32);
            this.Command1.TabIndex = 5;
            this.Command1.Text = "Kidobó készítés";
            this.Command1.UseVisualStyleBackColor = true;
            this.Command1.Click += new System.EventHandler(this.Command1_Click);
            // 
            // Command2
            // 
            this.Command2.Location = new System.Drawing.Point(12, 349);
            this.Command2.Name = "Command2";
            this.Command2.Size = new System.Drawing.Size(208, 32);
            this.Command2.TabIndex = 4;
            this.Command2.Text = "Adatok listázása";
            this.Command2.UseVisualStyleBackColor = true;
            this.Command2.Click += new System.EventHandler(this.Command2_Click);
            // 
            // Command11
            // 
            this.Command11.Location = new System.Drawing.Point(12, 311);
            this.Command11.Name = "Command11";
            this.Command11.Size = new System.Drawing.Size(208, 32);
            this.Command11.TabIndex = 3;
            this.Command11.Text = "Változattal Módosít";
            this.Command11.UseVisualStyleBackColor = true;
            this.Command11.Click += new System.EventHandler(this.Command11_Click);
            // 
            // Command12
            // 
            this.Command12.Location = new System.Drawing.Point(12, 519);
            this.Command12.Name = "Command12";
            this.Command12.Size = new System.Drawing.Size(208, 32);
            this.Command12.TabIndex = 8;
            this.Command12.Text = "Változatok listázása";
            this.Command12.UseVisualStyleBackColor = true;
            this.Command12.Click += new System.EventHandler(this.Command12_Click);
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.BackgroundColor = System.Drawing.Color.Orange;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.FilterAndSortEnabled = true;
            this.Tábla.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.Location = new System.Drawing.Point(226, 62);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(837, 489);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 204;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // Tábla1
            // 
            this.Tábla1.AllowUserToAddRows = false;
            this.Tábla1.AllowUserToDeleteRows = false;
            this.Tábla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla1.BackgroundColor = System.Drawing.Color.LightGreen;
            this.Tábla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla1.Location = new System.Drawing.Point(226, 63);
            this.Tábla1.Name = "Tábla1";
            this.Tábla1.RowHeadersVisible = false;
            this.Tábla1.Size = new System.Drawing.Size(837, 489);
            this.Tábla1.TabIndex = 205;
            this.Tábla1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla1_CellClick);
            // 
            // VáltozatCombo
            // 
            this.VáltozatCombo.FormattingEnabled = true;
            this.VáltozatCombo.Location = new System.Drawing.Point(12, 277);
            this.VáltozatCombo.Name = "VáltozatCombo";
            this.VáltozatCombo.Size = new System.Drawing.Size(208, 28);
            this.VáltozatCombo.TabIndex = 2;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(12, 63);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(66, 20);
            this.Label18.TabIndex = 212;
            this.Label18.Text = "Label18";
            // 
            // Forte_Beolvasás
            // 
            this.Forte_Beolvasás.Location = new System.Drawing.Point(12, 163);
            this.Forte_Beolvasás.Name = "Forte_Beolvasás";
            this.Forte_Beolvasás.Size = new System.Drawing.Size(208, 52);
            this.Forte_Beolvasás.TabIndex = 213;
            this.Forte_Beolvasás.Text = "Forte adatok további telephely(ek)";
            this.Forte_Beolvasás.UseVisualStyleBackColor = true;
            this.Forte_Beolvasás.Click += new System.EventHandler(this.Forte_Beolvasás_Click);
            // 
            // Keresés
            // 
            this.Keresés.BackgroundImage = global::Villamos.Properties.Resources.Nagyító;
            this.Keresés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Keresés.Location = new System.Drawing.Point(12, 427);
            this.Keresés.Name = "Keresés";
            this.Keresés.Size = new System.Drawing.Size(45, 45);
            this.Keresés.TabIndex = 6;
            this.Keresés.UseVisualStyleBackColor = true;
            this.Keresés.Click += new System.EventHandler(this.Keresés_Click);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1018, 12);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 9;
            this.toolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Btn_Ittasági
            // 
            this.Btn_Ittasági.BackgroundImage = global::Villamos.Properties.Resources.felhasználók32;
            this.Btn_Ittasági.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Btn_Ittasági.Location = new System.Drawing.Point(175, 427);
            this.Btn_Ittasági.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Btn_Ittasági.Name = "Btn_Ittasági";
            this.Btn_Ittasági.Size = new System.Drawing.Size(45, 45);
            this.Btn_Ittasági.TabIndex = 214;
            this.toolTip1.SetToolTip(this.Btn_Ittasági, "Ittassági nyomtatványt generál");
            this.Btn_Ittasági.UseVisualStyleBackColor = true;
            this.Btn_Ittasági.Click += new System.EventHandler(this.Btn_Ittasági_Click);
            // 
            // Ablak_kidobó
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Orange;
            this.ClientSize = new System.Drawing.Size(1074, 563);
            this.Controls.Add(this.Btn_Ittasági);
            this.Controls.Add(this.Forte_Beolvasás);
            this.Controls.Add(this.Keresés);
            this.Controls.Add(this.Label18);
            this.Controls.Add(this.VáltozatCombo);
            this.Controls.Add(this.Tábla1);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.Command12);
            this.Controls.Add(this.Command11);
            this.Controls.Add(this.Command2);
            this.Controls.Add(this.Command1);
            this.Controls.Add(this.Command8);
            this.Controls.Add(this.ForteBetöltés);
            this.Controls.Add(this.Dátum);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_kidobó";
            this.Text = "Kidobó készítés";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_kidobó_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_kidobó_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_kidobó_KeyDown);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button BtnSúgó;
        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal DateTimePicker Dátum;
        internal Button ForteBetöltés;
        internal Button Command8;
        internal Button Command1;
        internal Button Command2;
        internal Button Command11;
        internal Button Command12;
        internal  Zuby.ADGV.AdvancedDataGridView Tábla;
        internal DataGridView Tábla1;
        internal ComboBox VáltozatCombo;
        internal Label Label18;
        internal Button Keresés;
        internal Button Forte_Beolvasás;
        private ToolTip toolTip1;
        internal Button Btn_Ittasági;
    }
}