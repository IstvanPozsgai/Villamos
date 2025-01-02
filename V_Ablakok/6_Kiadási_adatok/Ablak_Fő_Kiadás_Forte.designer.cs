using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    public partial class Ablak_Fő_Kiadás_Forte : Form
    {

        // Form overrides dispose to clean up the component list.
        [DebuggerNonUserCode()]
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (disposing && components!= null)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Fő_Kiadás_Forte));
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Töröl = new System.Windows.Forms.Button();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Dátumról = new System.Windows.Forms.DateTimePicker();
            this.AdatMásol = new System.Windows.Forms.Button();
            this.Munkanap = new System.Windows.Forms.CheckBox();
            this.Command1 = new System.Windows.Forms.Button();
            this.MunkaHétvége = new System.Windows.Forms.Button();
            this.Lista = new System.Windows.Forms.Button();
            this.Fortebeolvasás = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Délután = new System.Windows.Forms.RadioButton();
            this.Délelőtt = new System.Windows.Forms.RadioButton();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Panel2.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(5, 9);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(1142, 28);
            this.Holtart.TabIndex = 174;
            this.Holtart.Visible = false;
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel2.BackColor = System.Drawing.Color.Goldenrod;
            this.Panel2.Controls.Add(this.Töröl);
            this.Panel2.Controls.Add(this.Panel3);
            this.Panel2.Controls.Add(this.Munkanap);
            this.Panel2.Controls.Add(this.Command1);
            this.Panel2.Controls.Add(this.MunkaHétvége);
            this.Panel2.Controls.Add(this.Lista);
            this.Panel2.Controls.Add(this.Fortebeolvasás);
            this.Panel2.Controls.Add(this.Button3);
            this.Panel2.Controls.Add(this.Dátum);
            this.Panel2.Controls.Add(this.Panel1);
            this.Panel2.Location = new System.Drawing.Point(5, 53);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(174, 438);
            this.Panel2.TabIndex = 206;
            // 
            // Töröl
            // 
            this.Töröl.BackgroundImage = global::Villamos.Properties.Resources.Kuka;
            this.Töröl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Töröl.Location = new System.Drawing.Point(118, 92);
            this.Töröl.Name = "Töröl";
            this.Töröl.Size = new System.Drawing.Size(40, 40);
            this.Töröl.TabIndex = 224;
            this.toolTip1.SetToolTip(this.Töröl, "Törli az adatokat");
            this.Töröl.UseVisualStyleBackColor = true;
            this.Töröl.Click += new System.EventHandler(this.Töröl_Click);
            // 
            // Panel3
            // 
            this.Panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel3.BackColor = System.Drawing.Color.Green;
            this.Panel3.Controls.Add(this.Label2);
            this.Panel3.Controls.Add(this.Label1);
            this.Panel3.Controls.Add(this.Dátumról);
            this.Panel3.Controls.Add(this.AdatMásol);
            this.Panel3.Location = new System.Drawing.Point(7, 286);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(162, 100);
            this.Panel3.TabIndex = 223;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(125, 31);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(31, 20);
            this.Label2.TabIndex = 222;
            this.Label2.Text = "-ról";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(5, 5);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(136, 20);
            this.Label1.TabIndex = 221;
            this.Label1.Text = "Adatok másolása:";
            // 
            // Dátumról
            // 
            this.Dátumról.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumról.Location = new System.Drawing.Point(5, 25);
            this.Dátumról.Name = "Dátumról";
            this.Dátumról.Size = new System.Drawing.Size(113, 26);
            this.Dátumról.TabIndex = 77;
            // 
            // AdatMásol
            // 
            this.AdatMásol.BackColor = System.Drawing.Color.Yellow;
            this.AdatMásol.Location = new System.Drawing.Point(11, 57);
            this.AdatMásol.Name = "AdatMásol";
            this.AdatMásol.Size = new System.Drawing.Size(130, 33);
            this.AdatMásol.TabIndex = 220;
            this.AdatMásol.Text = "Adat másolás";
            this.AdatMásol.UseVisualStyleBackColor = false;
            this.AdatMásol.Click += new System.EventHandler(this.AdatMásol_Click);
            // 
            // Munkanap
            // 
            this.Munkanap.AutoSize = true;
            this.Munkanap.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.Munkanap.Checked = true;
            this.Munkanap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Munkanap.Location = new System.Drawing.Point(9, 108);
            this.Munkanap.Name = "Munkanap";
            this.Munkanap.Size = new System.Drawing.Size(103, 24);
            this.Munkanap.TabIndex = 222;
            this.Munkanap.Text = "Munkanap";
            this.Munkanap.UseVisualStyleBackColor = false;
            // 
            // Command1
            // 
            this.Command1.BackColor = System.Drawing.Color.Yellow;
            this.Command1.Location = new System.Drawing.Point(7, 236);
            this.Command1.Name = "Command1";
            this.Command1.Size = new System.Drawing.Size(156, 35);
            this.Command1.TabIndex = 218;
            this.Command1.Text = "Havi adatok";
            this.Command1.UseVisualStyleBackColor = false;
            this.Command1.Click += new System.EventHandler(this.Command1_Click);
            // 
            // MunkaHétvége
            // 
            this.MunkaHétvége.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MunkaHétvége.BackColor = System.Drawing.Color.Yellow;
            this.MunkaHétvége.Location = new System.Drawing.Point(7, 392);
            this.MunkaHétvége.Name = "MunkaHétvége";
            this.MunkaHétvége.Size = new System.Drawing.Size(156, 33);
            this.MunkaHétvége.TabIndex = 219;
            this.MunkaHétvége.Text = "Munkanap/Hétvége";
            this.MunkaHétvége.UseVisualStyleBackColor = false;
            this.MunkaHétvége.Click += new System.EventHandler(this.MunkaHétvége_Click);
            // 
            // Lista
            // 
            this.Lista.BackColor = System.Drawing.Color.Yellow;
            this.Lista.Location = new System.Drawing.Point(7, 195);
            this.Lista.Name = "Lista";
            this.Lista.Size = new System.Drawing.Size(156, 35);
            this.Lista.TabIndex = 221;
            this.Lista.Text = "Napi adatok";
            this.Lista.UseVisualStyleBackColor = false;
            this.Lista.Click += new System.EventHandler(this.Lista_Click);
            // 
            // Fortebeolvasás
            // 
            this.Fortebeolvasás.BackColor = System.Drawing.Color.Yellow;
            this.Fortebeolvasás.Location = new System.Drawing.Point(7, 138);
            this.Fortebeolvasás.Name = "Fortebeolvasás";
            this.Fortebeolvasás.Size = new System.Drawing.Size(156, 35);
            this.Fortebeolvasás.TabIndex = 217;
            this.Fortebeolvasás.Text = "Forte beolvasás";
            this.Fortebeolvasás.UseVisualStyleBackColor = false;
            this.Fortebeolvasás.Click += new System.EventHandler(this.Fortebeolvasás_Click);
            // 
            // Button3
            // 
            this.Button3.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button3.Location = new System.Drawing.Point(118, 39);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(40, 40);
            this.Button3.TabIndex = 216;
            this.toolTip1.SetToolTip(this.Button3, "Excel táblázatot készít a táblázat adataiból");
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(6, 7);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(125, 26);
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
            this.Panel1.Size = new System.Drawing.Size(102, 61);
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
            // Tábla
            // 
            this.Tábla.AllowUserToAddRows = false;
            this.Tábla.AllowUserToDeleteRows = false;
            this.Tábla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla.BackgroundColor = System.Drawing.Color.Lime;
            this.Tábla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla.Location = new System.Drawing.Point(185, 53);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(1003, 438);
            this.Tábla.TabIndex = 209;
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1153, 2);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 173;
            this.toolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Ablak_Fő_Kiadás_Forte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1200, 503);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Fő_Kiadás_Forte";
            this.Text = "FORTE kiadási adatok mentése";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Fő_Kiadás_Forte_Load);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);

        }

        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal Button BtnSúgó;
        internal Panel Panel2;
        internal Button Command1;
        internal Button MunkaHétvége;
        internal Button AdatMásol;
        internal Button Lista;
        internal Button Fortebeolvasás;
        internal Button Button3;
        internal DateTimePicker Dátum;
        internal Panel Panel1;
        internal RadioButton Délután;
        internal RadioButton Délelőtt;
        internal DataGridView Tábla;
        internal Panel Panel3;
        internal Label Label2;
        internal Label Label1;
        internal DateTimePicker Dátumról;
        internal CheckBox Munkanap;
        internal Button Töröl;
        private ToolTip toolTip1;
    }
}