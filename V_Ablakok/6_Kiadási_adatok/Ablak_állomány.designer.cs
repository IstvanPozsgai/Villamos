using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
     public partial class Ablak_állomány : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_állomány));
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.PanelKocsik = new System.Windows.Forms.Panel();
            this.Bevitelilap = new System.Windows.Forms.Panel();
            this.Rögzít = new System.Windows.Forms.Button();
            this.Label4 = new System.Windows.Forms.Label();
            this.Telephely = new System.Windows.Forms.ComboBox();
            this.AlsóPanels1 = new System.Windows.Forms.TextBox();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Alap_excel = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Panel2.SuspendLayout();
            this.Bevitelilap.SuspendLayout();
            this.SuspendLayout();
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(451, 10);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(591, 28);
            this.Holtart.TabIndex = 175;
            this.Holtart.Visible = false;
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(5, 5);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(335, 33);
            this.Panel2.TabIndex = 173;
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
            // PanelKocsik
            // 
            this.PanelKocsik.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelKocsik.AutoScroll = true;
            this.PanelKocsik.BackColor = System.Drawing.Color.Gray;
            this.PanelKocsik.Location = new System.Drawing.Point(5, 56);
            this.PanelKocsik.Name = "PanelKocsik";
            this.PanelKocsik.Size = new System.Drawing.Size(1088, 290);
            this.PanelKocsik.TabIndex = 208;
            // 
            // Bevitelilap
            // 
            this.Bevitelilap.BackColor = System.Drawing.Color.Orange;
            this.Bevitelilap.Controls.Add(this.Rögzít);
            this.Bevitelilap.Controls.Add(this.Label4);
            this.Bevitelilap.Controls.Add(this.Telephely);
            this.Bevitelilap.Location = new System.Drawing.Point(132, 44);
            this.Bevitelilap.Name = "Bevitelilap";
            this.Bevitelilap.Size = new System.Drawing.Size(178, 120);
            this.Bevitelilap.TabIndex = 169;
            this.Bevitelilap.Visible = false;
            // 
            // Rögzít
            // 
            this.Rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Rögzít.Location = new System.Drawing.Point(125, 68);
            this.Rögzít.Name = "Rögzít";
            this.Rögzít.Size = new System.Drawing.Size(40, 40);
            this.Rögzít.TabIndex = 79;
            this.Rögzít.UseVisualStyleBackColor = true;
            this.Rögzít.Click += new System.EventHandler(this.Rögzít_Click);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(54, 11);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(57, 20);
            this.Label4.TabIndex = 5;
            this.Label4.Text = "Label4";
            // 
            // Telephely
            // 
            this.Telephely.FormattingEnabled = true;
            this.Telephely.Location = new System.Drawing.Point(14, 34);
            this.Telephely.Name = "Telephely";
            this.Telephely.Size = new System.Drawing.Size(151, 28);
            this.Telephely.TabIndex = 3;
            // 
            // AlsóPanels1
            // 
            this.AlsóPanels1.Location = new System.Drawing.Point(346, 5);
            this.AlsóPanels1.Name = "AlsóPanels1";
            this.AlsóPanels1.Size = new System.Drawing.Size(27, 26);
            this.AlsóPanels1.TabIndex = 209;
            this.AlsóPanels1.Visible = false;
            // 
            // ToolTip1
            // 
            this.ToolTip1.IsBalloon = true;
            // 
            // Alap_excel
            // 
            this.Alap_excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Alap_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_excel.Location = new System.Drawing.Point(346, 5);
            this.Alap_excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Alap_excel.Name = "Alap_excel";
            this.Alap_excel.Size = new System.Drawing.Size(45, 45);
            this.Alap_excel.TabIndex = 210;
            this.ToolTip1.SetToolTip(this.Alap_excel, "Táblázat adatait excelbe menti");
            this.Alap_excel.UseVisualStyleBackColor = true;
            this.Alap_excel.Click += new System.EventHandler(this.Alap_excel_Click);
            // 
            // Button1
            // 
            this.Button1.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button1.Location = new System.Drawing.Point(397, 5);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(45, 45);
            this.Button1.TabIndex = 211;
            this.ToolTip1.SetToolTip(this.Button1, "Frissíti a táblázatot");
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1048, 5);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 174;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Ablak_állomány
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 358);
            this.Controls.Add(this.Bevitelilap);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Alap_excel);
            this.Controls.Add(this.AlsóPanels1);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.PanelKocsik);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_állomány";
            this.Text = "Állomány tábla";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_állomány_Load);
            this.Shown += new System.EventHandler(this.Ablak_állomány_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_állomány_KeyDown);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Bevitelilap.ResumeLayout(false);
            this.Bevitelilap.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button BtnSúgó;
        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Panel PanelKocsik;
        internal TextBox AlsóPanels1;
        internal ToolTip ToolTip1;
        internal Button Alap_excel;
        internal Panel Bevitelilap;
        internal Label Label4;
        internal ComboBox Telephely;
        internal Button Rögzít;
        internal Button Button1;
    }
}