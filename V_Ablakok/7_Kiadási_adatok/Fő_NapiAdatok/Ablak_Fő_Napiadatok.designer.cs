using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{

    public partial class Ablak_Fő_Napiadatok : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Fő_Napiadatok));
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Délután = new System.Windows.Forms.RadioButton();
            this.Délelőtt = new System.Windows.Forms.RadioButton();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Command4 = new System.Windows.Forms.Button();
            this.HiányzóRögz = new System.Windows.Forms.Button();
            this.HiányzóAlap = new System.Windows.Forms.Button();
            this.Lista = new System.Windows.Forms.Button();
            this.Command1 = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Tábla2 = new System.Windows.Forms.DataGridView();
            this.Tábla1 = new System.Windows.Forms.DataGridView();
            this.LabelTelephely = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).BeginInit();
            this.SuspendLayout();
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(5, 12);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(1088, 28);
            this.Holtart.TabIndex = 172;
            this.Holtart.Visible = false;
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(6, 7);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(115, 26);
            this.Dátum.TabIndex = 76;
            this.Dátum.ValueChanged += new System.EventHandler(this.Dátum_ValueChanged);
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.Panel1.Controls.Add(this.Délután);
            this.Panel1.Controls.Add(this.Délelőtt);
            this.Panel1.Location = new System.Drawing.Point(7, 39);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(114, 61);
            this.Panel1.TabIndex = 204;
            // 
            // Délután
            // 
            this.Délután.AutoSize = true;
            this.Délután.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Délután.Location = new System.Drawing.Point(8, 33);
            this.Délután.Name = "Délután";
            this.Délután.Size = new System.Drawing.Size(83, 24);
            this.Délután.TabIndex = 1;
            this.Délután.TabStop = true;
            this.Délután.Text = "Délután";
            this.Délután.UseVisualStyleBackColor = false;
            this.Délután.Click += new System.EventHandler(this.Délután_Click);
            // 
            // Délelőtt
            // 
            this.Délelőtt.AutoSize = true;
            this.Délelőtt.BackColor = System.Drawing.Color.LightSteelBlue;
            this.Délelőtt.Checked = true;
            this.Délelőtt.Location = new System.Drawing.Point(8, 3);
            this.Délelőtt.Name = "Délelőtt";
            this.Délelőtt.Size = new System.Drawing.Size(82, 24);
            this.Délelőtt.TabIndex = 0;
            this.Délelőtt.TabStop = true;
            this.Délelőtt.Text = "Délelőtt";
            this.Délelőtt.UseVisualStyleBackColor = false;
            this.Délelőtt.Click += new System.EventHandler(this.Délelőtt_Click);
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel2.BackColor = System.Drawing.Color.Goldenrod;
            this.Panel2.Controls.Add(this.Command4);
            this.Panel2.Controls.Add(this.HiányzóRögz);
            this.Panel2.Controls.Add(this.HiányzóAlap);
            this.Panel2.Controls.Add(this.Lista);
            this.Panel2.Controls.Add(this.Command1);
            this.Panel2.Controls.Add(this.Button3);
            this.Panel2.Controls.Add(this.Dátum);
            this.Panel2.Controls.Add(this.Panel1);
            this.Panel2.Location = new System.Drawing.Point(5, 46);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(174, 416);
            this.Panel2.TabIndex = 205;
            // 
            // Command4
            // 
            this.Command4.BackColor = System.Drawing.Color.Yellow;
            this.Command4.Location = new System.Drawing.Point(7, 188);
            this.Command4.Name = "Command4";
            this.Command4.Size = new System.Drawing.Size(156, 35);
            this.Command4.TabIndex = 218;
            this.Command4.Text = "Töröl";
            this.Command4.UseVisualStyleBackColor = false;
            this.Command4.Click += new System.EventHandler(this.Command4_Click);
            // 
            // HiányzóRögz
            // 
            this.HiányzóRögz.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.HiányzóRögz.BackColor = System.Drawing.Color.Yellow;
            this.HiányzóRögz.Location = new System.Drawing.Point(7, 351);
            this.HiányzóRögz.Name = "HiányzóRögz";
            this.HiányzóRögz.Size = new System.Drawing.Size(156, 52);
            this.HiányzóRögz.TabIndex = 219;
            this.HiányzóRögz.Text = "Hiányzó Rögzítések";
            this.HiányzóRögz.UseVisualStyleBackColor = false;
            this.HiányzóRögz.Click += new System.EventHandler(this.HiányzóRögz_Click);
            // 
            // HiányzóAlap
            // 
            this.HiányzóAlap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.HiányzóAlap.BackColor = System.Drawing.Color.Yellow;
            this.HiányzóAlap.Location = new System.Drawing.Point(7, 294);
            this.HiányzóAlap.Name = "HiányzóAlap";
            this.HiányzóAlap.Size = new System.Drawing.Size(156, 51);
            this.HiányzóAlap.TabIndex = 220;
            this.HiányzóAlap.Text = "Hiányzó alapadatok";
            this.HiányzóAlap.UseVisualStyleBackColor = false;
            this.HiányzóAlap.Click += new System.EventHandler(this.HiányzóAlap_Click);
            // 
            // Lista
            // 
            this.Lista.BackColor = System.Drawing.Color.Yellow;
            this.Lista.Location = new System.Drawing.Point(6, 147);
            this.Lista.Name = "Lista";
            this.Lista.Size = new System.Drawing.Size(156, 35);
            this.Lista.TabIndex = 221;
            this.Lista.Text = "Napi adatok";
            this.Lista.UseVisualStyleBackColor = false;
            this.Lista.Click += new System.EventHandler(this.Lista_Click);
            // 
            // Command1
            // 
            this.Command1.BackColor = System.Drawing.Color.Yellow;
            this.Command1.Location = new System.Drawing.Point(6, 106);
            this.Command1.Name = "Command1";
            this.Command1.Size = new System.Drawing.Size(156, 35);
            this.Command1.TabIndex = 217;
            this.Command1.Text = "Rögzít";
            this.Command1.UseVisualStyleBackColor = false;
            this.Command1.Click += new System.EventHandler(this.Command1_Click);
            // 
            // Button3
            // 
            this.Button3.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button3.Location = new System.Drawing.Point(122, 60);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(40, 40);
            this.Button3.TabIndex = 216;
            this.toolTip1.SetToolTip(this.Button3, "Excel táblázatot készít a táblázat adataiból");
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Panel3
            // 
            this.Panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel3.BackColor = System.Drawing.Color.Goldenrod;
            this.Panel3.Location = new System.Drawing.Point(185, 46);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(174, 416);
            this.Panel3.TabIndex = 206;
            // 
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.BackgroundColor = System.Drawing.Color.Blue;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Location = new System.Drawing.Point(365, 80);
            this.Tábla.Name = "Tábla";
            this.Tábla.ReadOnly = true;
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(779, 384);
            this.Tábla.TabIndex = 207;
            this.Tábla.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_CellClick);
            // 
            // Tábla2
            // 
            this.Tábla2.AllowUserToAddRows = false;
            this.Tábla2.AllowUserToDeleteRows = false;
            this.Tábla2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla2.BackgroundColor = System.Drawing.Color.Lime;
            this.Tábla2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla2.Location = new System.Drawing.Point(365, 78);
            this.Tábla2.Name = "Tábla2";
            this.Tábla2.ReadOnly = true;
            this.Tábla2.RowHeadersVisible = false;
            this.Tábla2.Size = new System.Drawing.Size(779, 386);
            this.Tábla2.TabIndex = 208;
            // 
            // Tábla1
            // 
            this.Tábla1.AllowUserToAddRows = false;
            this.Tábla1.AllowUserToDeleteRows = false;
            this.Tábla1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla1.BackgroundColor = System.Drawing.Color.HotPink;
            this.Tábla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla1.Location = new System.Drawing.Point(365, 80);
            this.Tábla1.Name = "Tábla1";
            this.Tábla1.ReadOnly = true;
            this.Tábla1.RowHeadersVisible = false;
            this.Tábla1.Size = new System.Drawing.Size(779, 384);
            this.Tábla1.TabIndex = 209;
            // 
            // LabelTelephely
            // 
            this.LabelTelephely.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.LabelTelephely.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LabelTelephely.Location = new System.Drawing.Point(365, 50);
            this.LabelTelephely.Name = "LabelTelephely";
            this.LabelTelephely.Size = new System.Drawing.Size(200, 25);
            this.LabelTelephely.TabIndex = 216;
            this.LabelTelephely.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.Black;
            this.Label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label6.ForeColor = System.Drawing.Color.White;
            this.Label6.Location = new System.Drawing.Point(576, 50);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(125, 25);
            this.Label6.TabIndex = 217;
            this.Label6.Text = "Napi adatok:";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label6.Click += new System.EventHandler(this.Label6_Click);
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.Black;
            this.Label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label7.ForeColor = System.Drawing.Color.White;
            this.Label7.Location = new System.Drawing.Point(707, 50);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(155, 25);
            this.Label7.TabIndex = 218;
            this.Label7.Text = "Személyzet hiány:";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label7.Click += new System.EventHandler(this.Label7_Click);
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.Black;
            this.Label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label8.ForeColor = System.Drawing.Color.White;
            this.Label8.Location = new System.Drawing.Point(868, 50);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(128, 25);
            this.Label8.TabIndex = 219;
            this.Label8.Text = "Típus cserék:";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label8.Click += new System.EventHandler(this.Label8_Click);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1099, 5);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 171;
            this.toolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Ablak_Fő_Napiadatok
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Olive;
            this.ClientSize = new System.Drawing.Size(1148, 474);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.LabelTelephely);
            this.Controls.Add(this.Tábla2);
            this.Controls.Add(this.Tábla1);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.Panel3);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Fő_Napiadatok";
            this.Text = "Napi telephelyi adatok összesítése";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Fő_Napiadatok_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla1)).EndInit();
            this.ResumeLayout(false);

        }

        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button BtnSúgó;
        internal DateTimePicker Dátum;
        internal Panel Panel1;
        internal RadioButton Délután;
        internal RadioButton Délelőtt;
        internal Panel Panel2;
        internal Button Button3;
        internal Button Command1;
        internal Button Command4;
        internal Button HiányzóRögz;
        internal Button HiányzóAlap;
        internal Button Lista;
        internal Panel Panel3;
        internal DataGridView Tábla;
        internal DataGridView Tábla2;
        internal DataGridView Tábla1;
        internal Label LabelTelephely;
        internal Label Label6;
        internal Label Label7;
        internal Label Label8;
        private ToolTip toolTip1;
    }
}