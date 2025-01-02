using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
   
    public partial class Ablak_SAP_osztály : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_SAP_osztály));
            this.Lapfülek = new System.Windows.Forms.TabControl();
            this.TabPage1 = new System.Windows.Forms.TabPage();
            this.Frissít = new System.Windows.Forms.Button();
            this.SAP_Betölt = new System.Windows.Forms.Button();
            this.Telepadatok = new System.Windows.Forms.Button();
            this.PályaszámCombo1 = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.TabPage2 = new System.Windows.Forms.TabPage();
            this.LekérdezFajta = new System.Windows.Forms.Button();
            this.Excel = new System.Windows.Forms.Button();
            this.LekérdezRészletes = new System.Windows.Forms.Button();
            this.LekérdezTelep = new System.Windows.Forms.Button();
            this.Tábla1 = new System.Windows.Forms.DataGridView();
            this.Osztálylista = new System.Windows.Forms.ListBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Súgó = new System.Windows.Forms.Button();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.Lapfülek.SuspendLayout();
            this.TabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.TabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).BeginInit();
            this.SuspendLayout();
            // 
            // Lapfülek
            // 
            this.Lapfülek.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Lapfülek.Controls.Add(this.TabPage1);
            this.Lapfülek.Controls.Add(this.TabPage2);
            this.Lapfülek.Location = new System.Drawing.Point(0, 56);
            this.Lapfülek.Name = "Lapfülek";
            this.Lapfülek.Padding = new System.Drawing.Point(16, 3);
            this.Lapfülek.SelectedIndex = 0;
            this.Lapfülek.Size = new System.Drawing.Size(869, 393);
            this.Lapfülek.TabIndex = 70;
            this.Lapfülek.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.Lapfülek_DrawItem);
            this.Lapfülek.SelectedIndexChanged += new System.EventHandler(this.Fülek_SelectedIndexChanged);
            // 
            // TabPage1
            // 
            this.TabPage1.BackColor = System.Drawing.Color.Salmon;
            this.TabPage1.Controls.Add(this.Frissít);
            this.TabPage1.Controls.Add(this.SAP_Betölt);
            this.TabPage1.Controls.Add(this.Telepadatok);
            this.TabPage1.Controls.Add(this.PályaszámCombo1);
            this.TabPage1.Controls.Add(this.Label1);
            this.TabPage1.Controls.Add(this.Tábla);
            this.TabPage1.Location = new System.Drawing.Point(4, 29);
            this.TabPage1.Name = "TabPage1";
            this.TabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage1.Size = new System.Drawing.Size(861, 360);
            this.TabPage1.TabIndex = 0;
            this.TabPage1.Text = "SAP osztály adatok";
            // 
            // Frissít
            // 
            this.Frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Frissít.Location = new System.Drawing.Point(316, 8);
            this.Frissít.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Frissít.Name = "Frissít";
            this.Frissít.Size = new System.Drawing.Size(50, 50);
            this.Frissít.TabIndex = 117;
            this.ToolTip1.SetToolTip(this.Frissít, "Pályaszámra listázza az összes osztály adatot");
            this.Frissít.UseVisualStyleBackColor = true;
            this.Frissít.Click += new System.EventHandler(this.Frissít_Click);
            // 
            // SAP_Betölt
            // 
            this.SAP_Betölt.BackgroundImage = global::Villamos.Properties.Resources.SAP;
            this.SAP_Betölt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SAP_Betölt.Location = new System.Drawing.Point(369, 8);
            this.SAP_Betölt.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SAP_Betölt.Name = "SAP_Betölt";
            this.SAP_Betölt.Size = new System.Drawing.Size(50, 50);
            this.SAP_Betölt.TabIndex = 116;
            this.ToolTip1.SetToolTip(this.SAP_Betölt, "SAP adatokat betölti");
            this.SAP_Betölt.UseVisualStyleBackColor = true;
            this.SAP_Betölt.Click += new System.EventHandler(this.SAP_Betölt_Click);
            // 
            // Telepadatok
            // 
            this.Telepadatok.BackgroundImage = global::Villamos.Properties.Resources.Action_configure;
            this.Telepadatok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Telepadatok.Location = new System.Drawing.Point(422, 8);
            this.Telepadatok.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Telepadatok.Name = "Telepadatok";
            this.Telepadatok.Size = new System.Drawing.Size(50, 50);
            this.Telepadatok.TabIndex = 115;
            this.ToolTip1.SetToolTip(this.Telepadatok, "Frissíti a jármű telephelyi adatait");
            this.Telepadatok.UseVisualStyleBackColor = true;
            this.Telepadatok.Click += new System.EventHandler(this.Telepadatok_Click);
            // 
            // PályaszámCombo1
            // 
            this.PályaszámCombo1.DropDownHeight = 150;
            this.PályaszámCombo1.FormattingEnabled = true;
            this.PályaszámCombo1.IntegralHeight = false;
            this.PályaszámCombo1.Location = new System.Drawing.Point(136, 14);
            this.PályaszámCombo1.Name = "PályaszámCombo1";
            this.PályaszámCombo1.Size = new System.Drawing.Size(159, 28);
            this.PályaszámCombo1.TabIndex = 114;
            this.PályaszámCombo1.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.PályaszámCombo1_PreviewKeyDown);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(20, 20);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(89, 20);
            this.Label1.TabIndex = 113;
            this.Label1.Text = "Pályaszám:";
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Tábla.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Tábla.EnableHeadersVisualStyles = false;
            this.Tábla.Location = new System.Drawing.Point(3, 61);
            this.Tábla.Name = "Tábla";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.Tábla.RowHeadersWidth = 20;
            this.Tábla.Size = new System.Drawing.Size(855, 286);
            this.Tábla.TabIndex = 112;
            // 
            // TabPage2
            // 
            this.TabPage2.BackColor = System.Drawing.Color.Coral;
            this.TabPage2.Controls.Add(this.LekérdezFajta);
            this.TabPage2.Controls.Add(this.Excel);
            this.TabPage2.Controls.Add(this.LekérdezRészletes);
            this.TabPage2.Controls.Add(this.LekérdezTelep);
            this.TabPage2.Controls.Add(this.Tábla1);
            this.TabPage2.Controls.Add(this.Osztálylista);
            this.TabPage2.Location = new System.Drawing.Point(4, 29);
            this.TabPage2.Name = "TabPage2";
            this.TabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.TabPage2.Size = new System.Drawing.Size(861, 360);
            this.TabPage2.TabIndex = 1;
            this.TabPage2.Text = "Lekérdezések";
            // 
            // LekérdezFajta
            // 
            this.LekérdezFajta.BackColor = System.Drawing.Color.DarkCyan;
            this.LekérdezFajta.BackgroundImage = global::Villamos.Properties.Resources.CARDFIL3;
            this.LekérdezFajta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LekérdezFajta.Location = new System.Drawing.Point(216, 70);
            this.LekérdezFajta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LekérdezFajta.Name = "LekérdezFajta";
            this.LekérdezFajta.Size = new System.Drawing.Size(50, 50);
            this.LekérdezFajta.TabIndex = 121;
            this.ToolTip1.SetToolTip(this.LekérdezFajta, "Lekérdezés egyszerű kategóriánként összesített");
            this.LekérdezFajta.UseVisualStyleBackColor = false;
            this.LekérdezFajta.Click += new System.EventHandler(this.LekérdezFajta_Click);
            // 
            // Excel
            // 
            this.Excel.BackColor = System.Drawing.Color.DarkCyan;
            this.Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel.Location = new System.Drawing.Point(216, 190);
            this.Excel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Excel.Name = "Excel";
            this.Excel.Size = new System.Drawing.Size(50, 50);
            this.Excel.TabIndex = 120;
            this.ToolTip1.SetToolTip(this.Excel, "Excel táblázatot készít ");
            this.Excel.UseVisualStyleBackColor = false;
            this.Excel.Click += new System.EventHandler(this.Excel_Click);
            // 
            // LekérdezRészletes
            // 
            this.LekérdezRészletes.BackColor = System.Drawing.Color.DarkCyan;
            this.LekérdezRészletes.BackgroundImage = global::Villamos.Properties.Resources.Treetog_Junior_Document_scroll;
            this.LekérdezRészletes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LekérdezRészletes.Location = new System.Drawing.Point(216, 130);
            this.LekérdezRészletes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LekérdezRészletes.Name = "LekérdezRészletes";
            this.LekérdezRészletes.Size = new System.Drawing.Size(50, 50);
            this.LekérdezRészletes.TabIndex = 119;
            this.ToolTip1.SetToolTip(this.LekérdezRészletes, "Összes adat listázása pályaszám bontásban");
            this.LekérdezRészletes.UseVisualStyleBackColor = false;
            this.LekérdezRészletes.Click += new System.EventHandler(this.LekérdezRészletes_Click);
            // 
            // LekérdezTelep
            // 
            this.LekérdezTelep.BackColor = System.Drawing.Color.DarkCyan;
            this.LekérdezTelep.BackgroundImage = global::Villamos.Properties.Resources.BeCardStack;
            this.LekérdezTelep.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LekérdezTelep.Location = new System.Drawing.Point(216, 10);
            this.LekérdezTelep.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.LekérdezTelep.Name = "LekérdezTelep";
            this.LekérdezTelep.Size = new System.Drawing.Size(50, 50);
            this.LekérdezTelep.TabIndex = 118;
            this.ToolTip1.SetToolTip(this.LekérdezTelep, "Lekérdezés telephely típus bontásban összesítve");
            this.LekérdezTelep.UseVisualStyleBackColor = false;
            this.LekérdezTelep.Click += new System.EventHandler(this.LekérdezTelep_Click);
            // 
            // Tábla1
            // 
            this.Tábla1.AllowUserToAddRows = false;
            this.Tábla1.AllowUserToDeleteRows = false;
            this.Tábla1.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.Tábla1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.Tábla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.Tábla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Tábla1.EnableHeadersVisualStyles = false;
            this.Tábla1.Location = new System.Drawing.Point(273, 10);
            this.Tábla1.Name = "Tábla1";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Tábla1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.Tábla1.RowHeadersWidth = 20;
            this.Tábla1.Size = new System.Drawing.Size(585, 347);
            this.Tábla1.TabIndex = 113;
            // 
            // Osztálylista
            // 
            this.Osztálylista.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Osztálylista.FormattingEnabled = true;
            this.Osztálylista.ItemHeight = 20;
            this.Osztálylista.Location = new System.Drawing.Point(5, 10);
            this.Osztálylista.Name = "Osztálylista";
            this.Osztálylista.Size = new System.Drawing.Size(204, 324);
            this.Osztálylista.TabIndex = 0;
            // 
            // Súgó
            // 
            this.Súgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Súgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Súgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Súgó.Location = new System.Drawing.Point(818, 3);
            this.Súgó.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Súgó.Name = "Súgó";
            this.Súgó.Size = new System.Drawing.Size(45, 45);
            this.Súgó.TabIndex = 68;
            this.ToolTip1.SetToolTip(this.Súgó, "Súgó");
            this.Súgó.UseVisualStyleBackColor = true;
            this.Súgó.Click += new System.EventHandler(this.Súgó_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(15, 15);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(795, 30);
            this.Holtart.TabIndex = 118;
            this.Holtart.Visible = false;
            // 
            // Ablak_SAP_osztály
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 446);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Lapfülek);
            this.Controls.Add(this.Súgó);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_SAP_osztály";
            this.Text = "SAP Osztályadatok";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_SAP_osztály_Load);
            this.Lapfülek.ResumeLayout(false);
            this.TabPage1.ResumeLayout(false);
            this.TabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.TabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).EndInit();
            this.ResumeLayout(false);

        }

        internal Button Súgó;
        internal TabControl Lapfülek;
        internal TabPage TabPage1;
        internal TabPage TabPage2;
        internal ComboBox PályaszámCombo1;
        internal Label Label1;
        internal DataGridView Tábla;
        internal Button Frissít;
        internal Button SAP_Betölt;
        internal Button Telepadatok;
        internal ListBox Osztálylista;
        internal Button LekérdezFajta;
        internal Button Excel;
        internal Button LekérdezRészletes;
        internal Button LekérdezTelep;
        internal DataGridView Tábla1;
        internal ToolTip ToolTip1;
        private V_MindenEgyéb.MyProgressbar Holtart;
    }
}