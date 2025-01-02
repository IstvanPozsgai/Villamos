namespace Villamos.Villamos_Ablakok.CAF_Ütemezés
{
    partial class Ablak_CAF_Alapadat
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_CAF_Alapadat));
            this.Kalkulál = new System.Windows.Forms.Button();
            this.Utolsó_vizsgóta = new System.Windows.Forms.TextBox();
            this.Label22 = new System.Windows.Forms.Label();
            this.Alap_KM_számláló = new System.Windows.Forms.TextBox();
            this.Label20 = new System.Windows.Forms.Label();
            this.Alap_lekérdezés = new System.Windows.Forms.Button();
            this.Alap_Típus = new System.Windows.Forms.TextBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.Alap_rögzít = new System.Windows.Forms.Button();
            this.Alap_Havi_km = new System.Windows.Forms.TextBox();
            this.Alap_KMU = new System.Windows.Forms.TextBox();
            this.Alap_Össz_km = new System.Windows.Forms.TextBox();
            this.Alap_Dátum_frissítés = new System.Windows.Forms.DateTimePicker();
            this.Alap_felújítás = new System.Windows.Forms.DateTimePicker();
            this.Alap_Garancia = new System.Windows.Forms.CheckBox();
            this.Alap_Státus = new System.Windows.Forms.CheckBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Alap_ciklus_km = new System.Windows.Forms.ComboBox();
            this.Alap_ciklus_idő = new System.Windows.Forms.ComboBox();
            this.Alap_vizsg_km = new System.Windows.Forms.TextBox();
            this.Alap_vizsg_idő = new System.Windows.Forms.TextBox();
            this.Alap_vizsg_sorszám_km = new System.Windows.Forms.ComboBox();
            this.Alap_vizsg_sorszám_idő = new System.Windows.Forms.ComboBox();
            this.ALAP_Üzemek_km = new System.Windows.Forms.ComboBox();
            this.ALAP_Üzemek_nap = new System.Windows.Forms.ComboBox();
            this.Alap_dátum_idő = new System.Windows.Forms.DateTimePicker();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Alap_dátum_km = new System.Windows.Forms.DateTimePicker();
            this.Alap_pályaszám = new System.Windows.Forms.ComboBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.Holtart = new V_MindenEgyéb.MyProgressbar();
            this.SuspendLayout();
            // 
            // Kalkulál
            // 
            this.Kalkulál.BackgroundImage = global::Villamos.Properties.Resources.CALC1;
            this.Kalkulál.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Kalkulál.Location = new System.Drawing.Point(846, 168);
            this.Kalkulál.Name = "Kalkulál";
            this.Kalkulál.Size = new System.Drawing.Size(50, 52);
            this.Kalkulál.TabIndex = 114;
            this.toolTip1.SetToolTip(this.Kalkulál, "SAP km frissítési adatok betöltése");
            this.Kalkulál.UseVisualStyleBackColor = true;
            this.Kalkulál.Click += new System.EventHandler(this.Kalkulál_Click);
            // 
            // Utolsó_vizsgóta
            // 
            this.Utolsó_vizsgóta.Location = new System.Drawing.Point(624, 253);
            this.Utolsó_vizsgóta.Name = "Utolsó_vizsgóta";
            this.Utolsó_vizsgóta.Size = new System.Drawing.Size(146, 26);
            this.Utolsó_vizsgóta.TabIndex = 113;
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.Location = new System.Drawing.Point(391, 259);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(231, 20);
            this.Label22.TabIndex = 112;
            this.Label22.Text = "Utolsó vizsgálat óta becsült km:";
            // 
            // Alap_KM_számláló
            // 
            this.Alap_KM_számláló.Location = new System.Drawing.Point(624, 221);
            this.Alap_KM_számláló.Name = "Alap_KM_számláló";
            this.Alap_KM_számláló.Size = new System.Drawing.Size(146, 26);
            this.Alap_KM_számláló.TabIndex = 111;
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Location = new System.Drawing.Point(391, 227);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(201, 20);
            this.Label20.TabIndex = 110;
            this.Label20.Text = "Számláló állás vizsgálatkor:";
            // 
            // Alap_lekérdezés
            // 
            this.Alap_lekérdezés.BackgroundImage = global::Villamos.Properties.Resources.frissít_gyűjtemény;
            this.Alap_lekérdezés.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_lekérdezés.Location = new System.Drawing.Point(846, 112);
            this.Alap_lekérdezés.Name = "Alap_lekérdezés";
            this.Alap_lekérdezés.Size = new System.Drawing.Size(50, 50);
            this.Alap_lekérdezés.TabIndex = 109;
            this.toolTip1.SetToolTip(this.Alap_lekérdezés, "Pályaszámhoz tartozó adatok kiírása");
            this.Alap_lekérdezés.UseVisualStyleBackColor = true;
            this.Alap_lekérdezés.Click += new System.EventHandler(this.Lekérdezés_lekérdezés_Click);
            // 
            // Alap_Típus
            // 
            this.Alap_Típus.Location = new System.Drawing.Point(624, 385);
            this.Alap_Típus.Name = "Alap_Típus";
            this.Alap_Típus.Size = new System.Drawing.Size(146, 26);
            this.Alap_Típus.TabIndex = 108;
            // 
            // Label18
            // 
            this.Label18.AutoSize = true;
            this.Label18.Location = new System.Drawing.Point(391, 391);
            this.Label18.Name = "Label18";
            this.Label18.Size = new System.Drawing.Size(51, 20);
            this.Label18.TabIndex = 107;
            this.Label18.Text = "Típus:";
            // 
            // Alap_rögzít
            // 
            this.Alap_rögzít.BackgroundImage = global::Villamos.Properties.Resources.Ok_gyűjtemény;
            this.Alap_rögzít.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Alap_rögzít.Location = new System.Drawing.Point(846, 55);
            this.Alap_rögzít.Name = "Alap_rögzít";
            this.Alap_rögzít.Size = new System.Drawing.Size(50, 50);
            this.Alap_rögzít.TabIndex = 106;
            this.toolTip1.SetToolTip(this.Alap_rögzít, "Rögzíti az adatokat");
            this.Alap_rögzít.UseVisualStyleBackColor = true;
            this.Alap_rögzít.Click += new System.EventHandler(this.E_rögzít_Click);
            // 
            // Alap_Havi_km
            // 
            this.Alap_Havi_km.Location = new System.Drawing.Point(176, 420);
            this.Alap_Havi_km.Name = "Alap_Havi_km";
            this.Alap_Havi_km.Size = new System.Drawing.Size(146, 26);
            this.Alap_Havi_km.TabIndex = 105;
            // 
            // Alap_KMU
            // 
            this.Alap_KMU.Location = new System.Drawing.Point(624, 285);
            this.Alap_KMU.Name = "Alap_KMU";
            this.Alap_KMU.Size = new System.Drawing.Size(146, 26);
            this.Alap_KMU.TabIndex = 104;
            // 
            // Alap_Össz_km
            // 
            this.Alap_Össz_km.Location = new System.Drawing.Point(176, 388);
            this.Alap_Össz_km.Name = "Alap_Össz_km";
            this.Alap_Össz_km.Size = new System.Drawing.Size(146, 26);
            this.Alap_Össz_km.TabIndex = 103;
            // 
            // Alap_Dátum_frissítés
            // 
            this.Alap_Dátum_frissítés.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Alap_Dátum_frissítés.Location = new System.Drawing.Point(176, 346);
            this.Alap_Dátum_frissítés.Name = "Alap_Dátum_frissítés";
            this.Alap_Dátum_frissítés.Size = new System.Drawing.Size(119, 26);
            this.Alap_Dátum_frissítés.TabIndex = 102;
            // 
            // Alap_felújítás
            // 
            this.Alap_felújítás.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Alap_felújítás.Location = new System.Drawing.Point(625, 419);
            this.Alap_felújítás.Name = "Alap_felújítás";
            this.Alap_felújítás.Size = new System.Drawing.Size(119, 26);
            this.Alap_felújítás.TabIndex = 101;
            // 
            // Alap_Garancia
            // 
            this.Alap_Garancia.AutoSize = true;
            this.Alap_Garancia.Location = new System.Drawing.Point(624, 352);
            this.Alap_Garancia.Name = "Alap_Garancia";
            this.Alap_Garancia.Size = new System.Drawing.Size(107, 24);
            this.Alap_Garancia.TabIndex = 100;
            this.Alap_Garancia.Text = "Garanciális";
            this.Alap_Garancia.UseVisualStyleBackColor = true;
            // 
            // Alap_Státus
            // 
            this.Alap_Státus.AutoSize = true;
            this.Alap_Státus.Location = new System.Drawing.Point(391, 346);
            this.Alap_Státus.Name = "Alap_Státus";
            this.Alap_Státus.Size = new System.Drawing.Size(68, 24);
            this.Alap_Státus.TabIndex = 99;
            this.Alap_Státus.Text = "Törölt";
            this.Alap_Státus.UseVisualStyleBackColor = true;
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(391, 423);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(130, 20);
            this.Label11.TabIndex = 98;
            this.Label11.Text = "Felújítás dátuma:";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(15, 352);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(130, 20);
            this.Label10.TabIndex = 97;
            this.Label10.Text = "Frissítés dátuma:";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(15, 394);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(106, 20);
            this.Label9.TabIndex = 96;
            this.Label9.Text = "Összes futás:";
            // 
            // Label8
            // 
            this.Label8.AutoSize = true;
            this.Label8.Location = new System.Drawing.Point(391, 291);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(208, 20);
            this.Label8.TabIndex = 95;
            this.Label8.Text = "Jármű kmóra becsültl állása:";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(15, 426);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(69, 20);
            this.Label7.TabIndex = 94;
            this.Label7.Text = "Havi km:";
            // 
            // Alap_ciklus_km
            // 
            this.Alap_ciklus_km.DropDownHeight = 300;
            this.Alap_ciklus_km.FormattingEnabled = true;
            this.Alap_ciklus_km.IntegralHeight = false;
            this.Alap_ciklus_km.Location = new System.Drawing.Point(624, 55);
            this.Alap_ciklus_km.Name = "Alap_ciklus_km";
            this.Alap_ciklus_km.Size = new System.Drawing.Size(146, 28);
            this.Alap_ciklus_km.TabIndex = 93;
            this.Alap_ciklus_km.SelectedIndexChanged += new System.EventHandler(this.Alap_ciklus_km_SelectedIndexChanged);
            // 
            // Alap_ciklus_idő
            // 
            this.Alap_ciklus_idő.DropDownHeight = 300;
            this.Alap_ciklus_idő.FormattingEnabled = true;
            this.Alap_ciklus_idő.IntegralHeight = false;
            this.Alap_ciklus_idő.Location = new System.Drawing.Point(176, 55);
            this.Alap_ciklus_idő.Name = "Alap_ciklus_idő";
            this.Alap_ciklus_idő.Size = new System.Drawing.Size(146, 28);
            this.Alap_ciklus_idő.TabIndex = 92;
            this.Alap_ciklus_idő.SelectedIndexChanged += new System.EventHandler(this.Alap_ciklus_idő_SelectedIndexChanged);
            // 
            // Alap_vizsg_km
            // 
            this.Alap_vizsg_km.Location = new System.Drawing.Point(624, 89);
            this.Alap_vizsg_km.Name = "Alap_vizsg_km";
            this.Alap_vizsg_km.Size = new System.Drawing.Size(146, 26);
            this.Alap_vizsg_km.TabIndex = 91;
            // 
            // Alap_vizsg_idő
            // 
            this.Alap_vizsg_idő.Location = new System.Drawing.Point(176, 89);
            this.Alap_vizsg_idő.Name = "Alap_vizsg_idő";
            this.Alap_vizsg_idő.Size = new System.Drawing.Size(146, 26);
            this.Alap_vizsg_idő.TabIndex = 90;
            // 
            // Alap_vizsg_sorszám_km
            // 
            this.Alap_vizsg_sorszám_km.DropDownHeight = 300;
            this.Alap_vizsg_sorszám_km.FormattingEnabled = true;
            this.Alap_vizsg_sorszám_km.IntegralHeight = false;
            this.Alap_vizsg_sorszám_km.Location = new System.Drawing.Point(624, 121);
            this.Alap_vizsg_sorszám_km.Name = "Alap_vizsg_sorszám_km";
            this.Alap_vizsg_sorszám_km.Size = new System.Drawing.Size(121, 28);
            this.Alap_vizsg_sorszám_km.TabIndex = 89;
            this.Alap_vizsg_sorszám_km.SelectedIndexChanged += new System.EventHandler(this.Alap_vizsg_sorszám_km_SelectedIndexChanged);
            // 
            // Alap_vizsg_sorszám_idő
            // 
            this.Alap_vizsg_sorszám_idő.DropDownHeight = 300;
            this.Alap_vizsg_sorszám_idő.FormattingEnabled = true;
            this.Alap_vizsg_sorszám_idő.IntegralHeight = false;
            this.Alap_vizsg_sorszám_idő.Location = new System.Drawing.Point(176, 121);
            this.Alap_vizsg_sorszám_idő.Name = "Alap_vizsg_sorszám_idő";
            this.Alap_vizsg_sorszám_idő.Size = new System.Drawing.Size(121, 28);
            this.Alap_vizsg_sorszám_idő.TabIndex = 88;
            this.Alap_vizsg_sorszám_idő.SelectedIndexChanged += new System.EventHandler(this.Alap_vizsg_sorszám_idő_SelectedIndexChanged);
            // 
            // ALAP_Üzemek_km
            // 
            this.ALAP_Üzemek_km.DropDownHeight = 300;
            this.ALAP_Üzemek_km.FormattingEnabled = true;
            this.ALAP_Üzemek_km.IntegralHeight = false;
            this.ALAP_Üzemek_km.Location = new System.Drawing.Point(624, 155);
            this.ALAP_Üzemek_km.Name = "ALAP_Üzemek_km";
            this.ALAP_Üzemek_km.Size = new System.Drawing.Size(198, 28);
            this.ALAP_Üzemek_km.TabIndex = 87;
            // 
            // ALAP_Üzemek_nap
            // 
            this.ALAP_Üzemek_nap.DropDownHeight = 300;
            this.ALAP_Üzemek_nap.FormattingEnabled = true;
            this.ALAP_Üzemek_nap.IntegralHeight = false;
            this.ALAP_Üzemek_nap.Location = new System.Drawing.Point(176, 155);
            this.ALAP_Üzemek_nap.Name = "ALAP_Üzemek_nap";
            this.ALAP_Üzemek_nap.Size = new System.Drawing.Size(198, 28);
            this.ALAP_Üzemek_nap.TabIndex = 86;
            // 
            // Alap_dátum_idő
            // 
            this.Alap_dátum_idő.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Alap_dátum_idő.Location = new System.Drawing.Point(176, 189);
            this.Alap_dátum_idő.Name = "Alap_dátum_idő";
            this.Alap_dátum_idő.Size = new System.Drawing.Size(119, 26);
            this.Alap_dátum_idő.TabIndex = 85;
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(391, 195);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(70, 20);
            this.Label12.TabIndex = 84;
            this.Label12.Text = "Dátuma:";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(391, 163);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(73, 20);
            this.Label14.TabIndex = 83;
            this.Label14.Text = "Végezte:";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(391, 129);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(151, 20);
            this.Label15.TabIndex = 82;
            this.Label15.Text = "Vizsgálat sorszáma:";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(391, 95);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(128, 20);
            this.Label16.TabIndex = 81;
            this.Label16.Text = "Utolsó Vizsgálat:";
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(391, 63);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(117, 20);
            this.Label17.TabIndex = 80;
            this.Label17.Text = "Kmalapú Ciklus";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(15, 190);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(70, 20);
            this.Label6.TabIndex = 79;
            this.Label6.Text = "Dátuma:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(15, 158);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(73, 20);
            this.Label5.TabIndex = 78;
            this.Label5.Text = "Végezte:";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(15, 124);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(151, 20);
            this.Label4.TabIndex = 77;
            this.Label4.Text = "Vizsgálat sorszáma:";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(15, 90);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(128, 20);
            this.Label3.TabIndex = 76;
            this.Label3.Text = "Utolsó Vizsgálat:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(15, 58);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(117, 20);
            this.Label2.TabIndex = 75;
            this.Label2.Text = "Időalapú Ciklus";
            // 
            // Alap_dátum_km
            // 
            this.Alap_dátum_km.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Alap_dátum_km.Location = new System.Drawing.Point(624, 189);
            this.Alap_dátum_km.Name = "Alap_dátum_km";
            this.Alap_dátum_km.Size = new System.Drawing.Size(119, 26);
            this.Alap_dátum_km.TabIndex = 74;
            // 
            // Alap_pályaszám
            // 
            this.Alap_pályaszám.DropDownHeight = 300;
            this.Alap_pályaszám.FormattingEnabled = true;
            this.Alap_pályaszám.IntegralHeight = false;
            this.Alap_pályaszám.Location = new System.Drawing.Point(176, 7);
            this.Alap_pályaszám.Name = "Alap_pályaszám";
            this.Alap_pályaszám.Size = new System.Drawing.Size(121, 28);
            this.Alap_pályaszám.TabIndex = 73;
            this.Alap_pályaszám.SelectedIndexChanged += new System.EventHandler(this.Alap_pályaszám_SelectedIndexChanged);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(15, 15);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(89, 20);
            this.Label1.TabIndex = 72;
            this.Label1.Text = "Pályaszám:";
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            // 
            // Holtart
            // 
            this.Holtart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Holtart.Location = new System.Drawing.Point(10, 320);
            this.Holtart.Name = "Holtart";
            this.Holtart.Size = new System.Drawing.Size(880, 25);
            this.Holtart.TabIndex = 115;
            this.Holtart.Visible = false;
            // 
            // Ablak_CAF_Alapadat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Peru;
            this.ClientSize = new System.Drawing.Size(903, 452);
            this.Controls.Add(this.Holtart);
            this.Controls.Add(this.Kalkulál);
            this.Controls.Add(this.Utolsó_vizsgóta);
            this.Controls.Add(this.Label22);
            this.Controls.Add(this.Alap_KM_számláló);
            this.Controls.Add(this.Label20);
            this.Controls.Add(this.Alap_lekérdezés);
            this.Controls.Add(this.Alap_Típus);
            this.Controls.Add(this.Label18);
            this.Controls.Add(this.Alap_rögzít);
            this.Controls.Add(this.Alap_Havi_km);
            this.Controls.Add(this.Alap_KMU);
            this.Controls.Add(this.Alap_Össz_km);
            this.Controls.Add(this.Alap_Dátum_frissítés);
            this.Controls.Add(this.Alap_felújítás);
            this.Controls.Add(this.Alap_Garancia);
            this.Controls.Add(this.Alap_Státus);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Alap_ciklus_km);
            this.Controls.Add(this.Alap_ciklus_idő);
            this.Controls.Add(this.Alap_vizsg_km);
            this.Controls.Add(this.Alap_vizsg_idő);
            this.Controls.Add(this.Alap_vizsg_sorszám_km);
            this.Controls.Add(this.Alap_vizsg_sorszám_idő);
            this.Controls.Add(this.ALAP_Üzemek_km);
            this.Controls.Add(this.ALAP_Üzemek_nap);
            this.Controls.Add(this.Alap_dátum_idő);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.Label16);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Alap_dátum_km);
            this.Controls.Add(this.Alap_pályaszám);
            this.Controls.Add(this.Label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "Ablak_CAF_Alapadat";
            this.Text = "CAF Alapadatok";
            this.Load += new System.EventHandler(this.Ablak_CAF_Alapadat_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ablak_CAF_Alapadat_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button Kalkulál;
        internal System.Windows.Forms.TextBox Utolsó_vizsgóta;
        internal System.Windows.Forms.Label Label22;
        internal System.Windows.Forms.TextBox Alap_KM_számláló;
        internal System.Windows.Forms.Label Label20;
        internal System.Windows.Forms.Button Alap_lekérdezés;
        internal System.Windows.Forms.TextBox Alap_Típus;
        internal System.Windows.Forms.Label Label18;
        internal System.Windows.Forms.Button Alap_rögzít;
        internal System.Windows.Forms.TextBox Alap_Havi_km;
        internal System.Windows.Forms.TextBox Alap_KMU;
        internal System.Windows.Forms.TextBox Alap_Össz_km;
        internal System.Windows.Forms.DateTimePicker Alap_Dátum_frissítés;
        internal System.Windows.Forms.DateTimePicker Alap_felújítás;
        internal System.Windows.Forms.CheckBox Alap_Garancia;
        internal System.Windows.Forms.CheckBox Alap_Státus;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label8;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.ComboBox Alap_ciklus_km;
        internal System.Windows.Forms.ComboBox Alap_ciklus_idő;
        internal System.Windows.Forms.TextBox Alap_vizsg_km;
        internal System.Windows.Forms.TextBox Alap_vizsg_idő;
        internal System.Windows.Forms.ComboBox Alap_vizsg_sorszám_km;
        internal System.Windows.Forms.ComboBox Alap_vizsg_sorszám_idő;
        internal System.Windows.Forms.ComboBox ALAP_Üzemek_km;
        internal System.Windows.Forms.ComboBox ALAP_Üzemek_nap;
        internal System.Windows.Forms.DateTimePicker Alap_dátum_idő;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.Label Label17;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.DateTimePicker Alap_dátum_km;
        internal System.Windows.Forms.ComboBox Alap_pályaszám;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private V_MindenEgyéb.MyProgressbar Holtart;
    }
}