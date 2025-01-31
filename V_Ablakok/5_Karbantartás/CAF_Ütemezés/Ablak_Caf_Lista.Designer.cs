namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    partial class Ablak_Caf_Lista
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Caf_Lista));
            this.Lista_Dátumig = new System.Windows.Forms.DateTimePicker();
            this.Lista_Dátumtól = new System.Windows.Forms.DateTimePicker();
            this.Radio_idő = new System.Windows.Forms.RadioButton();
            this.Radio_km = new System.Windows.Forms.RadioButton();
            this.Radio_mind = new System.Windows.Forms.RadioButton();
            this.Lista_Pályaszám = new System.Windows.Forms.ComboBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.Tábla_lista = new System.Windows.Forms.DataGridView();
            this.Átírja_Módosításhoz = new System.Windows.Forms.Button();
            this.Lista_Pályaszám_friss = new System.Windows.Forms.Button();
            this.Lista_excel = new System.Windows.Forms.Button();
            this.Ütem_frissít = new System.Windows.Forms.Button();
            this.Alap_adatok = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_lista)).BeginInit();
            this.SuspendLayout();
            // 
            // Lista_Dátumig
            // 
            this.Lista_Dátumig.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Lista_Dátumig.Location = new System.Drawing.Point(757, 26);
            this.Lista_Dátumig.Name = "Lista_Dátumig";
            this.Lista_Dátumig.Size = new System.Drawing.Size(119, 26);
            this.Lista_Dátumig.TabIndex = 201;
            // 
            // Lista_Dátumtól
            // 
            this.Lista_Dátumtól.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Lista_Dátumtól.Location = new System.Drawing.Point(632, 26);
            this.Lista_Dátumtól.Name = "Lista_Dátumtól";
            this.Lista_Dátumtól.Size = new System.Drawing.Size(119, 26);
            this.Lista_Dátumtól.TabIndex = 200;
            // 
            // Radio_idő
            // 
            this.Radio_idő.AutoSize = true;
            this.Radio_idő.BackColor = System.Drawing.Color.BurlyWood;
            this.Radio_idő.Location = new System.Drawing.Point(520, 28);
            this.Radio_idő.Name = "Radio_idő";
            this.Radio_idő.Size = new System.Drawing.Size(50, 24);
            this.Radio_idő.TabIndex = 199;
            this.Radio_idő.Text = "Idő";
            this.Radio_idő.UseVisualStyleBackColor = false;
            // 
            // Radio_km
            // 
            this.Radio_km.AutoSize = true;
            this.Radio_km.BackColor = System.Drawing.Color.BurlyWood;
            this.Radio_km.Location = new System.Drawing.Point(576, 28);
            this.Radio_km.Name = "Radio_km";
            this.Radio_km.Size = new System.Drawing.Size(50, 24);
            this.Radio_km.TabIndex = 198;
            this.Radio_km.Text = "Km";
            this.Radio_km.UseVisualStyleBackColor = false;
            // 
            // Radio_mind
            // 
            this.Radio_mind.AutoSize = true;
            this.Radio_mind.BackColor = System.Drawing.Color.BurlyWood;
            this.Radio_mind.Checked = true;
            this.Radio_mind.Location = new System.Drawing.Point(453, 28);
            this.Radio_mind.Name = "Radio_mind";
            this.Radio_mind.Size = new System.Drawing.Size(61, 24);
            this.Radio_mind.TabIndex = 197;
            this.Radio_mind.TabStop = true;
            this.Radio_mind.Text = "Mind";
            this.Radio_mind.UseVisualStyleBackColor = false;
            // 
            // Lista_Pályaszám
            // 
            this.Lista_Pályaszám.DropDownHeight = 300;
            this.Lista_Pályaszám.FormattingEnabled = true;
            this.Lista_Pályaszám.IntegralHeight = false;
            this.Lista_Pályaszám.Location = new System.Drawing.Point(326, 24);
            this.Lista_Pályaszám.Name = "Lista_Pályaszám";
            this.Lista_Pályaszám.Size = new System.Drawing.Size(121, 28);
            this.Lista_Pályaszám.TabIndex = 194;
            this.Lista_Pályaszám.SelectedIndexChanged += new System.EventHandler(this.Lista_Pályaszám_SelectedIndexChanged);
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(231, 32);
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
            // Átírja_Módosításhoz
            // 
            this.Átírja_Módosításhoz.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Átírja_Módosításhoz.BackgroundImage = global::Villamos.Properties.Resources.Action_configure;
            this.Átírja_Módosításhoz.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Átírja_Módosításhoz.Location = new System.Drawing.Point(185, 12);
            this.Átírja_Módosításhoz.Name = "Átírja_Módosításhoz";
            this.Átírja_Módosításhoz.Size = new System.Drawing.Size(40, 40);
            this.Átírja_Módosításhoz.TabIndex = 202;
            this.toolTip1.SetToolTip(this.Átírja_Módosításhoz, "Ütemezés módosításba beírja a kijelölt sor adatait");
            this.Átírja_Módosításhoz.UseVisualStyleBackColor = false;
            this.Átírja_Módosításhoz.Click += new System.EventHandler(this.Átírja_Módosításhoz_Click);
            // 
            // Lista_Pályaszám_friss
            // 
            this.Lista_Pályaszám_friss.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Lista_Pályaszám_friss.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lista_Pályaszám_friss.Location = new System.Drawing.Point(882, 12);
            this.Lista_Pályaszám_friss.Name = "Lista_Pályaszám_friss";
            this.Lista_Pályaszám_friss.Size = new System.Drawing.Size(40, 40);
            this.Lista_Pályaszám_friss.TabIndex = 196;
            this.toolTip1.SetToolTip(this.Lista_Pályaszám_friss, "Pályaszámhoz tartozó adatok kiírása");
            this.Lista_Pályaszám_friss.UseVisualStyleBackColor = true;
            this.Lista_Pályaszám_friss.Click += new System.EventHandler(this.Lista_Pályaszám_friss_Click);
            // 
            // Lista_excel
            // 
            this.Lista_excel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Lista_excel.BackgroundImage = global::Villamos.Properties.Resources.Excel_gyűjtő;
            this.Lista_excel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Lista_excel.Location = new System.Drawing.Point(956, 11);
            this.Lista_excel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Lista_excel.Name = "Lista_excel";
            this.Lista_excel.Size = new System.Drawing.Size(40, 40);
            this.Lista_excel.TabIndex = 192;
            this.toolTip1.SetToolTip(this.Lista_excel, "Táblazatban szereplő adatok exportálása Excelbe.");
            this.Lista_excel.UseVisualStyleBackColor = true;
            this.Lista_excel.Click += new System.EventHandler(this.Lista_excel_Click);
            // 
            // Ütem_frissít
            // 
            this.Ütem_frissít.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Ütem_frissít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Ütem_frissít.Location = new System.Drawing.Point(8, 12);
            this.Ütem_frissít.Name = "Ütem_frissít";
            this.Ütem_frissít.Size = new System.Drawing.Size(40, 40);
            this.Ütem_frissít.TabIndex = 191;
            this.toolTip1.SetToolTip(this.Ütem_frissít, "Állományi adatok kiírása");
            this.Ütem_frissít.UseVisualStyleBackColor = true;
            this.Ütem_frissít.Click += new System.EventHandler(this.Ütem_frissít_Click);
            // 
            // Alap_adatok
            // 
            this.Alap_adatok.BackgroundImage = global::Villamos.Properties.Resources.process_accept;
            this.Alap_adatok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_adatok.Location = new System.Drawing.Point(139, 13);
            this.Alap_adatok.Name = "Alap_adatok";
            this.Alap_adatok.Size = new System.Drawing.Size(40, 40);
            this.Alap_adatok.TabIndex = 212;
            this.toolTip1.SetToolTip(this.Alap_adatok, "Kijelölt alapadatokat rögzítési felületen jeleníti meg.");
            this.Alap_adatok.UseVisualStyleBackColor = true;
            this.Alap_adatok.Click += new System.EventHandler(this.Alap_adatok_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(30, 240);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(950, 25);
            this.Holtart.TabIndex = 213;
            this.Holtart.Visible = false;
            // 
            // Ablak_Caf_Lista
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Peru;
            this.ClientSize = new System.Drawing.Size(1003, 428);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Alap_adatok);
            this.Controls.Add(this.Lista_Dátumig);
            this.Controls.Add(this.Lista_Dátumtól);
            this.Controls.Add(this.Radio_idő);
            this.Controls.Add(this.Radio_km);
            this.Controls.Add(this.Radio_mind);
            this.Controls.Add(this.Lista_Pályaszám);
            this.Controls.Add(this.Label19);
            this.Controls.Add(this.Tábla_lista);
            this.Controls.Add(this.Átírja_Módosításhoz);
            this.Controls.Add(this.Lista_Pályaszám_friss);
            this.Controls.Add(this.Lista_excel);
            this.Controls.Add(this.Ütem_frissít);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Ablak_Caf_Lista";
            this.Text = "Caf Listák";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ablak_Caf_Lista_FormClosed);
            this.Load += new System.EventHandler(this.Ablak_Caf_Lista_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Tábla_lista)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.DateTimePicker Lista_Dátumig;
        internal System.Windows.Forms.DateTimePicker Lista_Dátumtól;
        internal System.Windows.Forms.RadioButton Radio_idő;
        internal System.Windows.Forms.RadioButton Radio_km;
        internal System.Windows.Forms.RadioButton Radio_mind;
        internal System.Windows.Forms.ComboBox Lista_Pályaszám;
        internal System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.DataGridView Tábla_lista;
        internal System.Windows.Forms.Button Átírja_Módosításhoz;
        internal System.Windows.Forms.Button Lista_Pályaszám_friss;
        internal System.Windows.Forms.Button Lista_excel;
        internal System.Windows.Forms.Button Ütem_frissít;
        internal System.Windows.Forms.Button Alap_adatok;
        private System.Windows.Forms.ToolTip toolTip1;
        internal V_MindenEgyéb.MyProgressbar Holtart;
    }
}