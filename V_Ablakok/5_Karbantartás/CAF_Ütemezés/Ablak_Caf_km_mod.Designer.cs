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
            this.Tábla_lista = new System.Windows.Forms.DataGridView();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Lista_Pályaszám_friss = new System.Windows.Forms.Button();
            this.Holtart = new Villamos.V_MindenEgyéb.MyProgressbar();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_lista)).BeginInit();
            this.SuspendLayout();
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
            //this.Tábla_lista.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Tábla_lista_CellFormatting);
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
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(12, 19);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(922, 28);
            this.Holtart.TabIndex = 213;
            this.Holtart.Visible = false;
            // 
            // Ablak_Caf_km_mod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Peru;
            this.ClientSize = new System.Drawing.Size(1003, 428);
            this.Controls.Add(this.Holtart);
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

        }

        #endregion
        internal System.Windows.Forms.DataGridView Tábla_lista;
        internal System.Windows.Forms.Button Lista_Pályaszám_friss;
        private System.Windows.Forms.ToolTip toolTip1;
        internal V_MindenEgyéb.MyProgressbar Holtart;
    }
}