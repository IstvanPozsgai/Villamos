namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    partial class Ablak_Caf_km_mod
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Caf_km_mod));
            this.Lista_Pályaszám = new System.Windows.Forms.ComboBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.Tábla_lista = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Lista_Pályaszám_friss = new System.Windows.Forms.Button();
            this.button_km_modosit = new System.Windows.Forms.Button();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_lista)).BeginInit();
            this.SuspendLayout();
            // 
            // Lista_Pályaszám
            // 
            this.Lista_Pályaszám.DropDownHeight = 300;
            this.Lista_Pályaszám.FormattingEnabled = true;
            this.Lista_Pályaszám.IntegralHeight = false;
            this.Lista_Pályaszám.Location = new System.Drawing.Point(107, 19);
            this.Lista_Pályaszám.Name = "Lista_Pályaszám";
            this.Lista_Pályaszám.Size = new System.Drawing.Size(121, 28);
            this.Lista_Pályaszám.TabIndex = 194;
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(12, 22);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(89, 20);
            this.Label19.TabIndex = 193;
            this.Label19.Text = "Pályaszám:";
            // 
            // Tábla_lista
            // 
            this.Tábla_lista.AllowUserToAddRows = false;
            this.Tábla_lista.AllowUserToDeleteRows = false;
            this.Tábla_lista.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Tábla_lista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Tábla_lista.Location = new System.Drawing.Point(8, 58);
            this.Tábla_lista.Name = "Tábla_lista";
            this.Tábla_lista.RowHeadersVisible = false;
            this.Tábla_lista.RowHeadersWidth = 51;
            this.Tábla_lista.Size = new System.Drawing.Size(988, 367);
            this.Tábla_lista.TabIndex = 190;
            this.Tábla_lista.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Tábla_lista_CellClick);
            this.Tábla_lista.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Tábla_lista_CellFormatting);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Lista_Pályaszám_friss
            // 
            this.Lista_Pályaszám_friss.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Lista_Pályaszám_friss.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lista_Pályaszám_friss.Location = new System.Drawing.Point(940, 12);
            this.Lista_Pályaszám_friss.Name = "Lista_Pályaszám_friss";
            this.Lista_Pályaszám_friss.Size = new System.Drawing.Size(40, 40);
            this.Lista_Pályaszám_friss.TabIndex = 196;
            this.toolTip1.SetToolTip(this.Lista_Pályaszám_friss, "Pályaszámhoz tartozó adatok kiírása");
            this.Lista_Pályaszám_friss.UseVisualStyleBackColor = true;
            this.Lista_Pályaszám_friss.Click += new System.EventHandler(this.Lista_Pályaszám_friss_Click);
            // 
            // button_km_modosit
            // 
            this.button_km_modosit.BackgroundImage = global::Villamos.Properties.Resources.Gear_01;
            this.button_km_modosit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_km_modosit.Location = new System.Drawing.Point(894, 12);
            this.button_km_modosit.Name = "button_km_modosit";
            this.button_km_modosit.Size = new System.Drawing.Size(40, 40);
            this.button_km_modosit.TabIndex = 214;
            this.toolTip1.SetToolTip(this.button_km_modosit, "Pályaszámhoz tartozó adatok kiírása");
            this.button_km_modosit.UseVisualStyleBackColor = true;
            this.button_km_modosit.Click += new System.EventHandler(this.button_km_modosit_Click);
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(243, 19);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(593, 28);
            this.Holtart.TabIndex = 213;
            this.Holtart.Visible = false;
            // 
            // Ablak_Caf_km_mod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Peru;
            this.ClientSize = new System.Drawing.Size(1003, 428);
            this.Controls.Add(this.button_km_modosit);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Lista_Pályaszám);
            this.Controls.Add(this.Label19);
            this.Controls.Add(this.Tábla_lista);
            this.Controls.Add(this.Lista_Pályaszám_friss);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Caf_km_mod";
            this.Text = "Caf KM módosítás";
            this.Load += new System.EventHandler(this.Ablak_Caf_km_mod_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_lista)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.ComboBox Lista_Pályaszám;
        internal System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.DataGridView Tábla_lista;
        internal System.Windows.Forms.Button Lista_Pályaszám_friss;
        private System.Windows.Forms.ToolTip toolTip1;
        internal V_MindenEgyéb.MyProgressbar Holtart;
        internal System.Windows.Forms.Button button_km_modosit;
    }
}