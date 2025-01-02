using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
    public partial class Ablak_T5C5_futás : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_T5C5_futás));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.SAP_adatok = new System.Windows.Forms.Button();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Vissza = new System.Windows.Forms.Button();
            this.Napkinyitása = new System.Windows.Forms.Button();
            this.Göngyölés = new System.Windows.Forms.Button();
            this.Zseradategyeztetés = new System.Windows.Forms.Button();
            this.Zserbeolvasás = new System.Windows.Forms.Button();
            this.Napadatai = new System.Windows.Forms.Button();
            this.Naplezárása = new System.Windows.Forms.Button();
            this.Lista = new System.Windows.Forms.Button();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Bevitelilap = new System.Windows.Forms.Panel();
            this.Label4 = new System.Windows.Forms.Label();
            this.Rögzít = new System.Windows.Forms.Button();
            this.Kategória = new System.Windows.Forms.ComboBox();
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.Bevitelilap.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel1.BackColor = System.Drawing.Color.Tomato;
            this.Panel1.Controls.Add(this.SAP_adatok);
            this.Panel1.Controls.Add(this.Dátum);
            this.Panel1.Controls.Add(this.Vissza);
            this.Panel1.Controls.Add(this.Napkinyitása);
            this.Panel1.Controls.Add(this.Göngyölés);
            this.Panel1.Controls.Add(this.Zseradategyeztetés);
            this.Panel1.Controls.Add(this.Zserbeolvasás);
            this.Panel1.Controls.Add(this.Napadatai);
            this.Panel1.Controls.Add(this.Naplezárása);
            this.Panel1.Controls.Add(this.Lista);
            this.Panel1.Location = new System.Drawing.Point(5, 56);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(224, 399);
            this.Panel1.TabIndex = 0;
            // 
            // SAP_adatok
            // 
            this.SAP_adatok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SAP_adatok.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.SAP_adatok.BackgroundImage = global::Villamos.Properties.Resources.SAP;
            this.SAP_adatok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SAP_adatok.Location = new System.Drawing.Point(168, 3);
            this.SAP_adatok.Name = "SAP_adatok";
            this.SAP_adatok.Size = new System.Drawing.Size(50, 50);
            this.SAP_adatok.TabIndex = 93;
            this.SAP_adatok.UseVisualStyleBackColor = false;
            this.SAP_adatok.Click += new System.EventHandler(this.SAP_adatok_Click);
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(10, 10);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(123, 26);
            this.Dátum.TabIndex = 8;
            this.Dátum.ValueChanged += new System.EventHandler(this.Dátum_ValueChanged);
            // 
            // Vissza
            // 
            this.Vissza.Location = new System.Drawing.Point(12, 331);
            this.Vissza.Name = "Vissza";
            this.Vissza.Size = new System.Drawing.Size(206, 33);
            this.Vissza.TabIndex = 7;
            this.Vissza.Text = "Vissza göngyölés";
            this.Vissza.UseVisualStyleBackColor = true;
            this.Vissza.Click += new System.EventHandler(this.Vissza_Click);
            // 
            // Napkinyitása
            // 
            this.Napkinyitása.Location = new System.Drawing.Point(12, 253);
            this.Napkinyitása.Name = "Napkinyitása";
            this.Napkinyitása.Size = new System.Drawing.Size(206, 33);
            this.Napkinyitása.TabIndex = 6;
            this.Napkinyitása.Text = "Nap kinyitása";
            this.Napkinyitása.UseVisualStyleBackColor = true;
            this.Napkinyitása.Click += new System.EventHandler(this.Napkinyitása_Click);
            // 
            // Göngyölés
            // 
            this.Göngyölés.Location = new System.Drawing.Point(12, 292);
            this.Göngyölés.Name = "Göngyölés";
            this.Göngyölés.Size = new System.Drawing.Size(206, 33);
            this.Göngyölés.TabIndex = 5;
            this.Göngyölés.Text = "Adatok göngyölése";
            this.Göngyölés.UseVisualStyleBackColor = true;
            this.Göngyölés.Click += new System.EventHandler(this.Göngyölés_Click);
            // 
            // Zseradategyeztetés
            // 
            this.Zseradategyeztetés.Location = new System.Drawing.Point(12, 175);
            this.Zseradategyeztetés.Name = "Zseradategyeztetés";
            this.Zseradategyeztetés.Size = new System.Drawing.Size(206, 33);
            this.Zseradategyeztetés.TabIndex = 4;
            this.Zseradategyeztetés.Text = "Zser adatok összevetése";
            this.Zseradategyeztetés.UseVisualStyleBackColor = true;
            this.Zseradategyeztetés.Click += new System.EventHandler(this.Zseradategyeztetés_Click);
            // 
            // Zserbeolvasás
            // 
            this.Zserbeolvasás.Location = new System.Drawing.Point(12, 136);
            this.Zserbeolvasás.Name = "Zserbeolvasás";
            this.Zserbeolvasás.Size = new System.Drawing.Size(206, 33);
            this.Zserbeolvasás.TabIndex = 3;
            this.Zserbeolvasás.Text = "Zser beolvasás";
            this.Zserbeolvasás.UseVisualStyleBackColor = true;
            this.Zserbeolvasás.Click += new System.EventHandler(this.Zserbeolvasás_Click);
            // 
            // Napadatai
            // 
            this.Napadatai.Location = new System.Drawing.Point(12, 97);
            this.Napadatai.Name = "Napadatai";
            this.Napadatai.Size = new System.Drawing.Size(206, 33);
            this.Napadatai.TabIndex = 2;
            this.Napadatai.Text = "Napi alaptábla létrehozása";
            this.Napadatai.UseVisualStyleBackColor = true;
            this.Napadatai.Click += new System.EventHandler(this.Napadatai_Click);
            // 
            // Naplezárása
            // 
            this.Naplezárása.Location = new System.Drawing.Point(12, 214);
            this.Naplezárása.Name = "Naplezárása";
            this.Naplezárása.Size = new System.Drawing.Size(206, 33);
            this.Naplezárása.TabIndex = 1;
            this.Naplezárása.Text = "Nap lezárása";
            this.Naplezárása.UseVisualStyleBackColor = true;
            this.Naplezárása.Click += new System.EventHandler(this.Naplezárása_Click);
            // 
            // Lista
            // 
            this.Lista.Location = new System.Drawing.Point(12, 58);
            this.Lista.Name = "Lista";
            this.Lista.Size = new System.Drawing.Size(206, 33);
            this.Lista.TabIndex = 0;
            this.Lista.Text = "Kocsik listázása";
            this.ToolTip1.SetToolTip(this.Lista, "Listázza a kocsikat");
            this.Lista.UseVisualStyleBackColor = true;
            this.Lista.Click += new System.EventHandler(this.Lista_Click);
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.Cmbtelephely);
            this.Panel2.Controls.Add(this.Label13);
            this.Panel2.Location = new System.Drawing.Point(7, 9);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(335, 38);
            this.Panel2.TabIndex = 55;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(146, 5);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectedIndexChanged += new System.EventHandler(this.Cmbtelephely_SelectedIndexChanged);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(3, 8);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            this.Label13.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Label13_MouseClick);
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1092, 5);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 57;
            this.ToolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
            // 
            // Bevitelilap
            // 
            this.Bevitelilap.BackColor = System.Drawing.Color.Orange;
            this.Bevitelilap.Controls.Add(this.Label4);
            this.Bevitelilap.Controls.Add(this.Rögzít);
            this.Bevitelilap.Controls.Add(this.Kategória);
            this.Bevitelilap.Location = new System.Drawing.Point(351, 44);
            this.Bevitelilap.Name = "Bevitelilap";
            this.Bevitelilap.Size = new System.Drawing.Size(178, 120);
            this.Bevitelilap.TabIndex = 58;
            this.Bevitelilap.Visible = false;
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
            // Rögzít
            // 
            this.Rögzít.BackColor = System.Drawing.Color.Silver;
            this.Rögzít.Location = new System.Drawing.Point(40, 79);
            this.Rögzít.Name = "Rögzít";
            this.Rögzít.Size = new System.Drawing.Size(100, 30);
            this.Rögzít.TabIndex = 4;
            this.Rögzít.Text = "Rögzít";
            this.Rögzít.UseVisualStyleBackColor = false;
            this.Rögzít.Click += new System.EventHandler(this.Rögzít_Click);
            // 
            // Kategória
            // 
            this.Kategória.FormattingEnabled = true;
            this.Kategória.Location = new System.Drawing.Point(11, 45);
            this.Kategória.Name = "Kategória";
            this.Kategória.Size = new System.Drawing.Size(151, 28);
            this.Kategória.TabIndex = 3;
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(351, 10);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(735, 28);
            this.Holtart.TabIndex = 59;
            this.Holtart.Visible = false;
            // 
            // Panel3
            // 
            this.Panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel3.AutoScroll = true;
            this.Panel3.BackColor = System.Drawing.Color.Tomato;
            this.Panel3.Location = new System.Drawing.Point(235, 56);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(902, 399);
            this.Panel3.TabIndex = 60;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Ablak_T5C5_futás
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Salmon;
            this.ClientSize = new System.Drawing.Size(1149, 466);
            this.Controls.Add(this.Bevitelilap);
            this.Controls.Add(this.Panel3);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.BtnSúgó);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_T5C5_futás";
            this.Text = "T5C5 Napi futásadat ellenőrzés";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_T5C5_futás_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_T5C5_futás_KeyDown);
            this.Panel1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Bevitelilap.ResumeLayout(false);
            this.Bevitelilap.PerformLayout();
            this.ResumeLayout(false);

        }

        internal Panel Panel1;
        internal Panel Panel2;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Button BtnSúgó;
        internal DateTimePicker Dátum;
        internal Button Vissza;
        internal Button Napkinyitása;
        internal Button Göngyölés;
        internal Button Zseradategyeztetés;
        internal Button Zserbeolvasás;
        internal Button Napadatai;
        internal Button Naplezárása;
        internal Button Lista;
        internal Panel Bevitelilap;
        internal Button Rögzít;
        internal ComboBox Kategória;
        internal  V_MindenEgyéb.MyProgressbar Holtart;
        internal Panel Panel3;
        internal ToolTip ToolTip1;
        internal Label Label4;
        internal Button SAP_adatok;
        private Timer timer1;
    }
}