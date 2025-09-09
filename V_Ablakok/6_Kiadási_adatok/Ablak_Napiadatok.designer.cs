using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
 
    public partial class Ablak_Napiadatok : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Napiadatok));
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Label13 = new System.Windows.Forms.Label();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Excel_gomb = new System.Windows.Forms.Button();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Állókocsik = new System.Windows.Forms.Button();
            this.Havitípuscsere = new System.Windows.Forms.Button();
            this.Haviszemélyzethiány = new System.Windows.Forms.Button();
            this.Havielkészültkocsik = new System.Windows.Forms.Button();
            this.Havilista = new System.Windows.Forms.Button();
            this.Napikarbantartás = new System.Windows.Forms.Button();
            this.Napielkészültek = new System.Windows.Forms.Button();
            this.Napiállókocsik = new System.Windows.Forms.Button();
            this.Napiadatok_Frissítése = new System.Windows.Forms.Button();
            this.Lista = new System.Windows.Forms.Button();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Holtart = new System.Windows.Forms.ProgressBar();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Tábla1 = new Zuby.ADGV.AdvancedDataGridView();
            this.Tábla2 = new Zuby.ADGV.AdvancedDataGridView();
            this.KötésiOsztály = new System.Windows.Forms.BindingSource(this.components);
            this.KötésiOsztály1 = new System.Windows.Forms.BindingSource(this.components);
            this.KötésiOsztály2 = new System.Windows.Forms.BindingSource(this.components);
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KötésiOsztály)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KötésiOsztály1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.KötésiOsztály2)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Location = new System.Drawing.Point(1, 3);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(215, 66);
            this.Panel2.TabIndex = 73;
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(10, 4);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(14, 27);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(148, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
            // 
            // Excel_gomb
            // 
            this.Excel_gomb.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Excel_gomb.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel_gomb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel_gomb.Location = new System.Drawing.Point(57, 75);
            this.Excel_gomb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Excel_gomb.Name = "Excel_gomb";
            this.Excel_gomb.Size = new System.Drawing.Size(40, 40);
            this.Excel_gomb.TabIndex = 165;
            this.toolTip1.SetToolTip(this.Excel_gomb, "Excel táblázatot készít a táblázat adataiból");
            this.Excel_gomb.UseVisualStyleBackColor = false;
            this.Excel_gomb.Click += new System.EventHandler(this.Excel_gomb_Click);
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(103, 75);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(112, 26);
            this.Dátum.TabIndex = 166;
            // 
            // Állókocsik
            // 
            this.Állókocsik.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Állókocsik.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Állókocsik.Location = new System.Drawing.Point(7, 125);
            this.Állókocsik.Name = "Állókocsik";
            this.Állókocsik.Size = new System.Drawing.Size(208, 35);
            this.Állókocsik.TabIndex = 167;
            this.Állókocsik.Text = "Álló kocsik";
            this.Állókocsik.UseVisualStyleBackColor = false;
            this.Állókocsik.Click += new System.EventHandler(this.Állókocsik_Click);
            // 
            // Havitípuscsere
            // 
            this.Havitípuscsere.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Havitípuscsere.Location = new System.Drawing.Point(7, 512);
            this.Havitípuscsere.Name = "Havitípuscsere";
            this.Havitípuscsere.Size = new System.Drawing.Size(208, 35);
            this.Havitípuscsere.TabIndex = 168;
            this.Havitípuscsere.Text = "Havi típus csere";
            this.Havitípuscsere.UseVisualStyleBackColor = false;
            this.Havitípuscsere.Click += new System.EventHandler(this.Havitípuscsere_Click);
            // 
            // Haviszemélyzethiány
            // 
            this.Haviszemélyzethiány.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Haviszemélyzethiány.Location = new System.Drawing.Point(8, 471);
            this.Haviszemélyzethiány.Name = "Haviszemélyzethiány";
            this.Haviszemélyzethiány.Size = new System.Drawing.Size(208, 35);
            this.Haviszemélyzethiány.TabIndex = 169;
            this.Haviszemélyzethiány.Text = "Havi személyzet hiány";
            this.Haviszemélyzethiány.UseVisualStyleBackColor = false;
            this.Haviszemélyzethiány.Click += new System.EventHandler(this.Haviszemélyzethiány_Click);
            // 
            // Havielkészültkocsik
            // 
            this.Havielkészültkocsik.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Havielkészültkocsik.Location = new System.Drawing.Point(8, 430);
            this.Havielkészültkocsik.Name = "Havielkészültkocsik";
            this.Havielkészültkocsik.Size = new System.Drawing.Size(208, 35);
            this.Havielkészültkocsik.TabIndex = 170;
            this.Havielkészültkocsik.Text = "Havi elkészült kocsik";
            this.Havielkészültkocsik.UseVisualStyleBackColor = false;
            this.Havielkészültkocsik.Click += new System.EventHandler(this.Havielkészültkocsik_Click);
            // 
            // Havilista
            // 
            this.Havilista.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Havilista.Location = new System.Drawing.Point(8, 389);
            this.Havilista.Name = "Havilista";
            this.Havilista.Size = new System.Drawing.Size(208, 35);
            this.Havilista.TabIndex = 171;
            this.Havilista.Text = "Havi kiadás lista";
            this.Havilista.UseVisualStyleBackColor = false;
            this.Havilista.Click += new System.EventHandler(this.Havilista_Click);
            // 
            // Napikarbantartás
            // 
            this.Napikarbantartás.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Napikarbantartás.Location = new System.Drawing.Point(8, 330);
            this.Napikarbantartás.Name = "Napikarbantartás";
            this.Napikarbantartás.Size = new System.Drawing.Size(208, 53);
            this.Napikarbantartás.TabIndex = 172;
            this.Napikarbantartás.Text = "Napi elkészült karbantartások";
            this.Napikarbantartás.UseVisualStyleBackColor = false;
            this.Napikarbantartás.Click += new System.EventHandler(this.Napikarbantartás_Click);
            // 
            // Napielkészültek
            // 
            this.Napielkészültek.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Napielkészültek.Location = new System.Drawing.Point(7, 289);
            this.Napielkészültek.Name = "Napielkészültek";
            this.Napielkészültek.Size = new System.Drawing.Size(208, 35);
            this.Napielkészültek.TabIndex = 173;
            this.Napielkészültek.Text = "Napi elkészült kocsik";
            this.Napielkészültek.UseVisualStyleBackColor = false;
            this.Napielkészültek.Click += new System.EventHandler(this.Napielkészültek_Click);
            // 
            // Napiállókocsik
            // 
            this.Napiállókocsik.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Napiállókocsik.Location = new System.Drawing.Point(8, 248);
            this.Napiállókocsik.Name = "Napiállókocsik";
            this.Napiállókocsik.Size = new System.Drawing.Size(208, 35);
            this.Napiállókocsik.TabIndex = 174;
            this.Napiállókocsik.Text = "Napi álló kocsik";
            this.Napiállókocsik.UseVisualStyleBackColor = false;
            this.Napiállókocsik.Click += new System.EventHandler(this.Napiállókocsik_Click);
            // 
            // Napiadatok_Frissítése
            // 
            this.Napiadatok_Frissítése.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Napiadatok_Frissítése.Location = new System.Drawing.Point(8, 207);
            this.Napiadatok_Frissítése.Name = "Napiadatok_Frissítése";
            this.Napiadatok_Frissítése.Size = new System.Drawing.Size(208, 35);
            this.Napiadatok_Frissítése.TabIndex = 175;
            this.Napiadatok_Frissítése.Text = "Napi adatok frissítése";
            this.Napiadatok_Frissítése.UseVisualStyleBackColor = false;
            this.Napiadatok_Frissítése.Click += new System.EventHandler(this.Napiadatok_Frissítése_Click);
            // 
            // Lista
            // 
            this.Lista.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.Lista.Location = new System.Drawing.Point(8, 166);
            this.Lista.Name = "Lista";
            this.Lista.Size = new System.Drawing.Size(208, 35);
            this.Lista.TabIndex = 176;
            this.Lista.Text = "Napi kiadási adatok";
            this.Lista.UseVisualStyleBackColor = false;
            this.Lista.Click += new System.EventHandler(this.Lista_Click);
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.BackColor = System.Drawing.Color.LightGreen;
            this.Label6.Location = new System.Drawing.Point(228, 6);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(98, 20);
            this.Label6.TabIndex = 177;
            this.Label6.Text = "Napi adatok:";
            this.Label6.Click += new System.EventHandler(this.Label6_Click);
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.BackColor = System.Drawing.Color.LightGreen;
            this.Label8.Location = new System.Drawing.Point(501, 7);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(102, 20);
            this.Label8.TabIndex = 178;
            this.Label8.Text = "Típus cserék:";
            this.Label8.Click += new System.EventHandler(this.Label8_Click);
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.BackColor = System.Drawing.Color.LightGreen;
            this.Label7.Location = new System.Drawing.Point(349, 7);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(136, 20);
            this.Label7.TabIndex = 179;
            this.Label7.Text = "Személyzet hiány:";
            this.Label7.Click += new System.EventHandler(this.Label7_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(15, 319);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(874, 15);
            this.Holtart.TabIndex = 184;
            this.Holtart.Visible = false;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(8, 75);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(40, 40);
            this.BtnSúgó.TabIndex = 75;
            this.toolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = false;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
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
            this.Tábla.Location = new System.Drawing.Point(230, 50);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(690, 500);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 185;
            // 
            // Tábla1
            // 
            this.Tábla1.AllowUserToAddRows = false;
            this.Tábla1.AllowUserToDeleteRows = false;
            this.Tábla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla1.FilterAndSortEnabled = true;
            this.Tábla1.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla1.Location = new System.Drawing.Point(230, 50);
            this.Tábla1.MaxFilterButtonImageHeight = 23;
            this.Tábla1.Name = "Tábla1";
            this.Tábla1.ReadOnly = true;
            this.Tábla1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla1.RowHeadersVisible = false;
            this.Tábla1.Size = new System.Drawing.Size(690, 500);
            this.Tábla1.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla1.TabIndex = 186;
            // 
            // Tábla2
            // 
            this.Tábla2.AllowUserToAddRows = false;
            this.Tábla2.AllowUserToDeleteRows = false;
            this.Tábla2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla2.FilterAndSortEnabled = true;
            this.Tábla2.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla2.Location = new System.Drawing.Point(230, 50);
            this.Tábla2.MaxFilterButtonImageHeight = 23;
            this.Tábla2.Name = "Tábla2";
            this.Tábla2.ReadOnly = true;
            this.Tábla2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla2.RowHeadersVisible = false;
            this.Tábla2.Size = new System.Drawing.Size(690, 500);
            this.Tábla2.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla2.TabIndex = 187;
            // 
            // Ablak_Napiadatok
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(925, 561);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Tábla1);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Lista);
            this.Controls.Add(this.Napiadatok_Frissítése);
            this.Controls.Add(this.Napiállókocsik);
            this.Controls.Add(this.Napielkészültek);
            this.Controls.Add(this.Napikarbantartás);
            this.Controls.Add(this.Havilista);
            this.Controls.Add(this.Havielkészültkocsik);
            this.Controls.Add(this.Haviszemélyzethiány);
            this.Controls.Add(this.Havitípuscsere);
            this.Controls.Add(this.Állókocsik);
            this.Controls.Add(this.Dátum);
            this.Controls.Add(this.Excel_gomb);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Tábla2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Napiadatok";
            this.Text = "Kiadási és Javítási adatok";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Napiadatok_Load);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KötésiOsztály)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KötésiOsztály1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.KötésiOsztály2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal Panel Panel2;
        internal Label Label13;
        internal ComboBox Cmbtelephely;
        internal Button BtnSúgó;
        internal Button Excel_gomb;
        internal DateTimePicker Dátum;
        internal Button Állókocsik;
        internal Button Havitípuscsere;
        internal Button Haviszemélyzethiány;
        internal Button Havielkészültkocsik;
        internal Button Havilista;
        internal Button Napikarbantartás;
        internal Button Napielkészültek;
        internal Button Napiállókocsik;
        internal Button Napiadatok_Frissítése;
        internal Button Lista;
        internal Label Label6;
        internal Label Label8;
        internal Label Label7;
        internal ProgressBar Holtart;
        private ToolTip toolTip1;
        private Zuby.ADGV.AdvancedDataGridView Tábla;
        private Zuby.ADGV.AdvancedDataGridView Tábla1;
        private Zuby.ADGV.AdvancedDataGridView Tábla2;
        internal BindingSource KötésiOsztály;
        internal BindingSource KötésiOsztály1;
        internal BindingSource KötésiOsztály2;
    }
}