using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
  
    public partial class Ablak_Fő_Egyesített : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Fő_Egyesített));
            this.Panel2 = new System.Windows.Forms.Panel();
            this.Kimutatás = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.TípusHavi = new System.Windows.Forms.Button();
            this.SzemélyHavi = new System.Windows.Forms.Button();
            this.KiadHavi = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.TípusNapi = new System.Windows.Forms.Button();
            this.Command7 = new System.Windows.Forms.Button();
            this.TípusÁllományDb = new System.Windows.Forms.Button();
            this.SzemélyNapi = new System.Windows.Forms.Button();
            this.KiadaNapi = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Délután = new System.Windows.Forms.RadioButton();
            this.Délelőtt = new System.Windows.Forms.RadioButton();
            this.BtnSúgó = new System.Windows.Forms.Button();
            this.Tábla = new System.Windows.Forms.DataGridView();
            this.Kategórilista = new System.Windows.Forms.CheckedListBox();
            this.CsoportkijelölMind = new System.Windows.Forms.Button();
            this.CsoportVissza = new System.Windows.Forms.Button();
            this.Csuk = new System.Windows.Forms.Button();
            this.Nyit = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.alsóPanels4 = new System.Windows.Forms.TextBox();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Holtartfő = new Villamos.V_MindenEgyéb.MyProgressbar();
            this.Panel2.SuspendLayout();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel2
            // 
            this.Panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Panel2.BackColor = System.Drawing.Color.Goldenrod;
            this.Panel2.Controls.Add(this.Kimutatás);
            this.Panel2.Controls.Add(this.Label2);
            this.Panel2.Controls.Add(this.TípusHavi);
            this.Panel2.Controls.Add(this.SzemélyHavi);
            this.Panel2.Controls.Add(this.KiadHavi);
            this.Panel2.Controls.Add(this.Label1);
            this.Panel2.Controls.Add(this.TípusNapi);
            this.Panel2.Controls.Add(this.Command7);
            this.Panel2.Controls.Add(this.TípusÁllományDb);
            this.Panel2.Controls.Add(this.SzemélyNapi);
            this.Panel2.Controls.Add(this.KiadaNapi);
            this.Panel2.Controls.Add(this.Button3);
            this.Panel2.Controls.Add(this.Dátum);
            this.Panel2.Controls.Add(this.Panel1);
            this.Panel2.Location = new System.Drawing.Point(5, 55);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(174, 530);
            this.Panel2.TabIndex = 208;
            // 
            // Kimutatás
            // 
            this.Kimutatás.BackgroundImage = global::Villamos.Properties.Resources.Aha_Soft_Large_Seo_SEO;
            this.Kimutatás.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kimutatás.Location = new System.Drawing.Point(122, 14);
            this.Kimutatás.Name = "Kimutatás";
            this.Kimutatás.Size = new System.Drawing.Size(40, 40);
            this.Kimutatás.TabIndex = 227;
            this.toolTip1.SetToolTip(this.Kimutatás, "Kimutatást készít az adott táblázatból");
            this.Kimutatás.UseVisualStyleBackColor = true;
            this.Kimutatás.Click += new System.EventHandler(this.Kimutatás_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.OrangeRed;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label2.ForeColor = System.Drawing.Color.White;
            this.Label2.Location = new System.Drawing.Point(11, 266);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(109, 20);
            this.Label2.TabIndex = 226;
            this.Label2.Text = "Havi adatok:";
            // 
            // TípusHavi
            // 
            this.TípusHavi.BackColor = System.Drawing.Color.Yellow;
            this.TípusHavi.Location = new System.Drawing.Point(8, 371);
            this.TípusHavi.Name = "TípusHavi";
            this.TípusHavi.Size = new System.Drawing.Size(156, 35);
            this.TípusHavi.TabIndex = 224;
            this.TípusHavi.Text = "Típus csere";
            this.TípusHavi.UseVisualStyleBackColor = false;
            this.TípusHavi.Click += new System.EventHandler(this.TípusHavi_Click);
            // 
            // SzemélyHavi
            // 
            this.SzemélyHavi.BackColor = System.Drawing.Color.Yellow;
            this.SzemélyHavi.Location = new System.Drawing.Point(8, 330);
            this.SzemélyHavi.Name = "SzemélyHavi";
            this.SzemélyHavi.Size = new System.Drawing.Size(156, 35);
            this.SzemélyHavi.TabIndex = 225;
            this.SzemélyHavi.Text = "Személyzet";
            this.SzemélyHavi.UseVisualStyleBackColor = false;
            this.SzemélyHavi.Click += new System.EventHandler(this.SzemélyHavi_Click);
            // 
            // KiadHavi
            // 
            this.KiadHavi.BackColor = System.Drawing.Color.Yellow;
            this.KiadHavi.Location = new System.Drawing.Point(7, 289);
            this.KiadHavi.Name = "KiadHavi";
            this.KiadHavi.Size = new System.Drawing.Size(156, 35);
            this.KiadHavi.TabIndex = 223;
            this.KiadHavi.Text = "Kiadás";
            this.KiadHavi.UseVisualStyleBackColor = false;
            this.KiadHavi.Click += new System.EventHandler(this.KiadHavi_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.OrangeRed;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(11, 112);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(110, 20);
            this.Label1.TabIndex = 222;
            this.Label1.Text = "Napi adatok:";
            // 
            // TípusNapi
            // 
            this.TípusNapi.BackColor = System.Drawing.Color.Yellow;
            this.TípusNapi.Location = new System.Drawing.Point(8, 217);
            this.TípusNapi.Name = "TípusNapi";
            this.TípusNapi.Size = new System.Drawing.Size(156, 35);
            this.TípusNapi.TabIndex = 218;
            this.TípusNapi.Text = "Típus csere";
            this.TípusNapi.UseVisualStyleBackColor = false;
            this.TípusNapi.Click += new System.EventHandler(this.TípusNapi_Click);
            // 
            // Command7
            // 
            this.Command7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Command7.BackColor = System.Drawing.Color.Yellow;
            this.Command7.Location = new System.Drawing.Point(6, 484);
            this.Command7.Name = "Command7";
            this.Command7.Size = new System.Drawing.Size(156, 33);
            this.Command7.TabIndex = 219;
            this.Command7.Text = "Kocsik ellenőrzése";
            this.Command7.UseVisualStyleBackColor = false;
            this.Command7.Click += new System.EventHandler(this.Command7_Click);
            // 
            // TípusÁllományDb
            // 
            this.TípusÁllományDb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.TípusÁllományDb.BackColor = System.Drawing.Color.Yellow;
            this.TípusÁllományDb.Location = new System.Drawing.Point(6, 427);
            this.TípusÁllományDb.Name = "TípusÁllományDb";
            this.TípusÁllományDb.Size = new System.Drawing.Size(156, 51);
            this.TípusÁllományDb.TabIndex = 220;
            this.TípusÁllományDb.Text = "Típus állományi darabok";
            this.TípusÁllományDb.UseVisualStyleBackColor = false;
            this.TípusÁllományDb.Click += new System.EventHandler(this.TípusÁllományDb_Click);
            // 
            // SzemélyNapi
            // 
            this.SzemélyNapi.BackColor = System.Drawing.Color.Yellow;
            this.SzemélyNapi.Location = new System.Drawing.Point(8, 176);
            this.SzemélyNapi.Name = "SzemélyNapi";
            this.SzemélyNapi.Size = new System.Drawing.Size(156, 35);
            this.SzemélyNapi.TabIndex = 221;
            this.SzemélyNapi.Text = "Személyzet";
            this.SzemélyNapi.UseVisualStyleBackColor = false;
            this.SzemélyNapi.Click += new System.EventHandler(this.SzemélyNapi_Click);
            // 
            // KiadaNapi
            // 
            this.KiadaNapi.BackColor = System.Drawing.Color.Yellow;
            this.KiadaNapi.Location = new System.Drawing.Point(7, 135);
            this.KiadaNapi.Name = "KiadaNapi";
            this.KiadaNapi.Size = new System.Drawing.Size(156, 35);
            this.KiadaNapi.TabIndex = 217;
            this.KiadaNapi.Text = "Kiadás";
            this.KiadaNapi.UseVisualStyleBackColor = false;
            this.KiadaNapi.Click += new System.EventHandler(this.KiadaNapi_Click);
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
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(6, 7);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(110, 26);
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
            this.Panel1.Size = new System.Drawing.Size(109, 61);
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
            // 
            // BtnSúgó
            // 
            this.BtnSúgó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSúgó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.BtnSúgó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BtnSúgó.Location = new System.Drawing.Point(1125, 5);
            this.BtnSúgó.Name = "BtnSúgó";
            this.BtnSúgó.Size = new System.Drawing.Size(45, 45);
            this.BtnSúgó.TabIndex = 206;
            this.toolTip1.SetToolTip(this.BtnSúgó, "Súgó");
            this.BtnSúgó.UseVisualStyleBackColor = true;
            this.BtnSúgó.Click += new System.EventHandler(this.BtnSúgó_Click);
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
            this.Tábla.Location = new System.Drawing.Point(185, 55);
            this.Tábla.Name = "Tábla";
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(985, 530);
            this.Tábla.TabIndex = 210;
            // 
            // Kategórilista
            // 
            this.Kategórilista.CheckOnClick = true;
            this.Kategórilista.FormattingEnabled = true;
            this.Kategórilista.Location = new System.Drawing.Point(11, 16);
            this.Kategórilista.Name = "Kategórilista";
            this.Kategórilista.Size = new System.Drawing.Size(168, 25);
            this.Kategórilista.TabIndex = 211;
            // 
            // CsoportkijelölMind
            // 
            this.CsoportkijelölMind.BackgroundImage = global::Villamos.Properties.Resources.mndent_kijelöl;
            this.CsoportkijelölMind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsoportkijelölMind.Location = new System.Drawing.Point(234, 7);
            this.CsoportkijelölMind.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CsoportkijelölMind.Name = "CsoportkijelölMind";
            this.CsoportkijelölMind.Size = new System.Drawing.Size(40, 40);
            this.CsoportkijelölMind.TabIndex = 212;
            this.toolTip1.SetToolTip(this.CsoportkijelölMind, "Mindent kijelöl");
            this.CsoportkijelölMind.UseVisualStyleBackColor = true;
            this.CsoportkijelölMind.Click += new System.EventHandler(this.CsoportkijelölMind_Click);
            // 
            // CsoportVissza
            // 
            this.CsoportVissza.BackgroundImage = global::Villamos.Properties.Resources.üres_lista;
            this.CsoportVissza.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CsoportVissza.Location = new System.Drawing.Point(282, 7);
            this.CsoportVissza.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CsoportVissza.Name = "CsoportVissza";
            this.CsoportVissza.Size = new System.Drawing.Size(40, 40);
            this.CsoportVissza.TabIndex = 213;
            this.toolTip1.SetToolTip(this.CsoportVissza, "Minden kijelölést töröl");
            this.CsoportVissza.UseVisualStyleBackColor = true;
            this.CsoportVissza.Click += new System.EventHandler(this.CsoportVissza_Click);
            // 
            // Csuk
            // 
            this.Csuk.BackgroundImage = global::Villamos.Properties.Resources.fel;
            this.Csuk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Csuk.Location = new System.Drawing.Point(186, 7);
            this.Csuk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Csuk.Name = "Csuk";
            this.Csuk.Size = new System.Drawing.Size(40, 40);
            this.Csuk.TabIndex = 214;
            this.Csuk.UseVisualStyleBackColor = true;
            this.Csuk.Click += new System.EventHandler(this.Csuk_Click);
            // 
            // Nyit
            // 
            this.Nyit.BackgroundImage = global::Villamos.Properties.Resources.le;
            this.Nyit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Nyit.Location = new System.Drawing.Point(185, 7);
            this.Nyit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Nyit.Name = "Nyit";
            this.Nyit.Size = new System.Drawing.Size(40, 40);
            this.Nyit.TabIndex = 215;
            this.toolTip1.SetToolTip(this.Nyit, "Lenyitja a listát");
            this.Nyit.UseVisualStyleBackColor = true;
            this.Nyit.Click += new System.EventHandler(this.Nyit_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // alsóPanels4
            // 
            this.alsóPanels4.Location = new System.Drawing.Point(324, 161);
            this.alsóPanels4.Name = "alsóPanels4";
            this.alsóPanels4.Size = new System.Drawing.Size(100, 26);
            this.alsóPanels4.TabIndex = 217;
            this.alsóPanels4.Visible = false;
            // 
            // Holtart
            // 
            this.Holtart.Location = new System.Drawing.Point(338, 7);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(752, 18);
            this.Holtart.TabIndex = 218;
            this.Holtart.Visible = false;
            // 
            // Holtartfő
            // 
            this.Holtartfő.Location = new System.Drawing.Point(338, 32);
            this.Holtartfő.Name = "Holtartfő";
            this.Holtartfő.Size = new System.Drawing.Size(752, 18);
            this.Holtartfő.TabIndex = 218;
            this.Holtartfő.Visible = false;
            // 
            // Ablak_Fő_Egyesített
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.ClientSize = new System.Drawing.Size(1172, 591);
            this.Controls.Add(this.Holtartfő);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.alsóPanels4);
            this.Controls.Add(this.Nyit);
            this.Controls.Add(this.Csuk);
            this.Controls.Add(this.CsoportkijelölMind);
            this.Controls.Add(this.CsoportVissza);
            this.Controls.Add(this.Kategórilista);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.Panel2);
            this.Controls.Add(this.BtnSúgó);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Fő_Egyesített";
            this.Text = "Rögzített Főmérnökségi Kiadási adatok";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Fő_Egyesített_Load);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal Panel Panel2;
        internal Button TípusNapi;
        internal Button Command7;
        internal Button TípusÁllományDb;
        internal Button SzemélyNapi;
        internal Button KiadaNapi;
        internal Button Button3;
        internal DateTimePicker Dátum;
        internal Panel Panel1;
        internal RadioButton Délután;
        internal RadioButton Délelőtt;
        internal Button BtnSúgó;
        internal Button Kimutatás;
        internal Label Label2;
        internal Button TípusHavi;
        internal Button SzemélyHavi;
        internal Button KiadHavi;
        internal Label Label1;
        internal DataGridView Tábla;
        internal CheckedListBox Kategórilista;
        internal Button CsoportkijelölMind;
        internal Button CsoportVissza;
        internal Button Csuk;
        internal Button Nyit;
        private ToolTip toolTip1;
        internal TextBox alsóPanels4;
        private V_MindenEgyéb.MyProgressbar Holtart;
        private V_MindenEgyéb.MyProgressbar Holtartfő;
    }
}