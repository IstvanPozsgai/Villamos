using System.Diagnostics;
using System.Windows.Forms;

namespace Villamos
{
    
    public partial class Ablak_Beosztás_Napló : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Beosztás_Napló));
            this.Panel1 = new System.Windows.Forms.Panel();
            this.Cmbtelephely = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.Excel = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Dátumtól = new System.Windows.Forms.DateTimePicker();
            this.Dátumig = new System.Windows.Forms.DateTimePicker();
            this.VizsgDátum = new System.Windows.Forms.DateTimePicker();
            this.Dolgozónév = new System.Windows.Forms.ComboBox();
            this.Egy_Nap = new System.Windows.Forms.CheckBox();
            this.Kilépettjel = new System.Windows.Forms.CheckBox();
            this.Tábla = new Zuby.ADGV.AdvancedDataGridView();
            this.Listáz = new System.Windows.Forms.Button();
            this.Sugó = new System.Windows.Forms.Button();
            this.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.Cmbtelephely);
            this.Panel1.Controls.Add(this.Label13);
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(373, 33);
            this.Panel1.TabIndex = 56;
            // 
            // Cmbtelephely
            // 
            this.Cmbtelephely.FormattingEnabled = true;
            this.Cmbtelephely.Location = new System.Drawing.Point(175, 2);
            this.Cmbtelephely.Name = "Cmbtelephely";
            this.Cmbtelephely.Size = new System.Drawing.Size(186, 28);
            this.Cmbtelephely.TabIndex = 18;
            this.Cmbtelephely.SelectionChangeCommitted += new System.EventHandler(this.Cmbtelephely_SelectionChangeCommitted);
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(12, 5);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(145, 20);
            this.Label13.TabIndex = 17;
            this.Label13.Text = "Telephelyi beállítás:";
            // 
            // Excel
            // 
            this.Excel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Excel.Location = new System.Drawing.Point(1089, 2);
            this.Excel.Name = "Excel";
            this.Excel.Size = new System.Drawing.Size(45, 45);
            this.Excel.TabIndex = 94;
            this.Excel.UseVisualStyleBackColor = true;
            this.Excel.Click += new System.EventHandler(this.Excel_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.LawnGreen;
            this.Label1.Location = new System.Drawing.Point(12, 49);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(78, 20);
            this.Label1.TabIndex = 95;
            this.Label1.Text = "Dátumtól:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.LawnGreen;
            this.Label2.Location = new System.Drawing.Point(128, 49);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(73, 20);
            this.Label2.TabIndex = 96;
            this.Label2.Text = "Dátumig:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.LawnGreen;
            this.Label3.Location = new System.Drawing.Point(334, 49);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(100, 20);
            this.Label3.TabIndex = 97;
            this.Label3.Text = "Vizsgált nap:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.LawnGreen;
            this.Label4.Location = new System.Drawing.Point(450, 46);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(72, 20);
            this.Label4.TabIndex = 98;
            this.Label4.Text = "Dolgozó:";
            // 
            // Dátumtól
            // 
            this.Dátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumtól.Location = new System.Drawing.Point(12, 72);
            this.Dátumtól.Name = "Dátumtól";
            this.Dátumtól.Size = new System.Drawing.Size(110, 26);
            this.Dátumtól.TabIndex = 99;
            // 
            // Dátumig
            // 
            this.Dátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátumig.Location = new System.Drawing.Point(128, 72);
            this.Dátumig.Name = "Dátumig";
            this.Dátumig.Size = new System.Drawing.Size(110, 26);
            this.Dátumig.TabIndex = 100;
            // 
            // VizsgDátum
            // 
            this.VizsgDátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.VizsgDátum.Location = new System.Drawing.Point(334, 72);
            this.VizsgDátum.Name = "VizsgDátum";
            this.VizsgDátum.Size = new System.Drawing.Size(110, 26);
            this.VizsgDátum.TabIndex = 101;
            // 
            // Dolgozónév
            // 
            this.Dolgozónév.FormattingEnabled = true;
            this.Dolgozónév.Location = new System.Drawing.Point(450, 69);
            this.Dolgozónév.Name = "Dolgozónév";
            this.Dolgozónév.Size = new System.Drawing.Size(429, 28);
            this.Dolgozónév.TabIndex = 102;
            // 
            // Egy_Nap
            // 
            this.Egy_Nap.AutoSize = true;
            this.Egy_Nap.BackColor = System.Drawing.Color.LawnGreen;
            this.Egy_Nap.Location = new System.Drawing.Point(242, 46);
            this.Egy_Nap.Name = "Egy_Nap";
            this.Egy_Nap.Size = new System.Drawing.Size(86, 24);
            this.Egy_Nap.TabIndex = 103;
            this.Egy_Nap.Text = "Egy nap";
            this.Egy_Nap.UseVisualStyleBackColor = false;
            // 
            // Kilépettjel
            // 
            this.Kilépettjel.AutoSize = true;
            this.Kilépettjel.BackColor = System.Drawing.Color.LawnGreen;
            this.Kilépettjel.Location = new System.Drawing.Point(710, 39);
            this.Kilépettjel.Name = "Kilépettjel";
            this.Kilépettjel.Size = new System.Drawing.Size(169, 24);
            this.Kilépettjel.TabIndex = 104;
            this.Kilépettjel.Text = "Kilépett dolgozókkal";
            this.Kilépettjel.UseVisualStyleBackColor = false;
            this.Kilépettjel.CheckedChanged += new System.EventHandler(this.Kilépettjel_CheckedChanged);
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
            this.Tábla.Location = new System.Drawing.Point(3, 104);
            this.Tábla.MaxFilterButtonImageHeight = 23;
            this.Tábla.Name = "Tábla";
            this.Tábla.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Tábla.RowHeadersVisible = false;
            this.Tábla.Size = new System.Drawing.Size(1194, 343);
            this.Tábla.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            this.Tábla.TabIndex = 106;
            // 
            // Listáz
            // 
            this.Listáz.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Listáz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Listáz.Location = new System.Drawing.Point(905, 52);
            this.Listáz.Name = "Listáz";
            this.Listáz.Size = new System.Drawing.Size(45, 45);
            this.Listáz.TabIndex = 105;
            this.Listáz.UseVisualStyleBackColor = true;
            this.Listáz.Click += new System.EventHandler(this.Listáz_Click);
            // 
            // Sugó
            // 
            this.Sugó.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Sugó.BackgroundImage = global::Villamos.Properties.Resources.Help_Support;
            this.Sugó.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Sugó.Location = new System.Drawing.Point(1152, 2);
            this.Sugó.Name = "Sugó";
            this.Sugó.Size = new System.Drawing.Size(45, 45);
            this.Sugó.TabIndex = 60;
            this.Sugó.UseVisualStyleBackColor = true;
            this.Sugó.Click += new System.EventHandler(this.Súgó_Click);
            // 
            // Ablak_Beosztás_Napló
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(1200, 459);
            this.Controls.Add(this.Tábla);
            this.Controls.Add(this.Listáz);
            this.Controls.Add(this.Kilépettjel);
            this.Controls.Add(this.Egy_Nap);
            this.Controls.Add(this.Dolgozónév);
            this.Controls.Add(this.VizsgDátum);
            this.Controls.Add(this.Dátumig);
            this.Controls.Add(this.Dátumtól);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Excel);
            this.Controls.Add(this.Sugó);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Beosztás_Napló";
            this.Text = "Beosztás Napló";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Beosztás_Napló_Load);
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal Panel Panel1;
        internal ComboBox Cmbtelephely;
        internal Label Label13;
        internal Button Sugó;
        internal Button Excel;
        internal Label Label1;
        internal Label Label2;
        internal Label Label3;
        internal Label Label4;
        internal DateTimePicker Dátumtól;
        internal DateTimePicker Dátumig;
        internal DateTimePicker VizsgDátum;
        internal ComboBox Dolgozónév;
        internal CheckBox Egy_Nap;
        internal CheckBox Kilépettjel;
        internal Button Listáz;
        internal  Zuby.ADGV.AdvancedDataGridView Tábla;
    }
}