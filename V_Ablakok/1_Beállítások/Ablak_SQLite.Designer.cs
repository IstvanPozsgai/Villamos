namespace Villamos.V_Ablakok._1_Beállítások
{
    partial class Ablak_SQLite
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.dgv_ShowData = new System.Windows.Forms.DataGridView();
            this.tblP_ShowUserData = new System.Windows.Forms.TableLayoutPanel();
            this.lb_username = new System.Windows.Forms.Label();
            this.lb_datetime = new System.Windows.Forms.Label();
            this.lb_tf = new System.Windows.Forms.Label();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.tb_datetime = new System.Windows.Forms.TextBox();
            this.tb_tf = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_create = new System.Windows.Forms.Button();
            this.btn_AddData = new System.Windows.Forms.Button();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ShowData)).BeginInit();
            this.tblP_ShowUserData.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.5F));
            this.tableLayoutPanel1.Controls.Add(this.dgv_ShowData, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tblP_ShowUserData, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 29.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70.66666F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // dgv_ShowData
            // 
            this.dgv_ShowData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_ShowData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableLayoutPanel1.SetColumnSpan(this.dgv_ShowData, 2);
            this.dgv_ShowData.Location = new System.Drawing.Point(3, 135);
            this.dgv_ShowData.Name = "dgv_ShowData";
            this.dgv_ShowData.Size = new System.Drawing.Size(794, 312);
            this.dgv_ShowData.TabIndex = 0;
            this.dgv_ShowData.SelectionChanged += new System.EventHandler(this.dgv_ShowData_SelectionChanged);
            // 
            // tblP_ShowUserData
            // 
            this.tblP_ShowUserData.ColumnCount = 2;
            this.tblP_ShowUserData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.95699F));
            this.tblP_ShowUserData.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.04301F));
            this.tblP_ShowUserData.Controls.Add(this.lb_username, 0, 0);
            this.tblP_ShowUserData.Controls.Add(this.lb_datetime, 0, 1);
            this.tblP_ShowUserData.Controls.Add(this.lb_tf, 0, 2);
            this.tblP_ShowUserData.Controls.Add(this.tb_username, 1, 0);
            this.tblP_ShowUserData.Controls.Add(this.tb_datetime, 1, 1);
            this.tblP_ShowUserData.Controls.Add(this.tb_tf, 1, 2);
            this.tblP_ShowUserData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblP_ShowUserData.Location = new System.Drawing.Point(3, 3);
            this.tblP_ShowUserData.Name = "tblP_ShowUserData";
            this.tblP_ShowUserData.RowCount = 3;
            this.tblP_ShowUserData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblP_ShowUserData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblP_ShowUserData.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 76F));
            this.tblP_ShowUserData.Size = new System.Drawing.Size(558, 126);
            this.tblP_ShowUserData.TabIndex = 2;
            // 
            // lb_username
            // 
            this.lb_username.AutoSize = true;
            this.lb_username.Dock = System.Windows.Forms.DockStyle.Left;
            this.lb_username.Location = new System.Drawing.Point(3, 0);
            this.lb_username.Name = "lb_username";
            this.lb_username.Size = new System.Drawing.Size(27, 25);
            this.lb_username.TabIndex = 0;
            this.lb_username.Text = "Név";
            // 
            // lb_datetime
            // 
            this.lb_datetime.AutoSize = true;
            this.lb_datetime.Location = new System.Drawing.Point(3, 25);
            this.lb_datetime.Name = "lb_datetime";
            this.lb_datetime.Size = new System.Drawing.Size(38, 13);
            this.lb_datetime.TabIndex = 1;
            this.lb_datetime.Text = "Dátum";
            // 
            // lb_tf
            // 
            this.lb_tf.AutoSize = true;
            this.lb_tf.Location = new System.Drawing.Point(3, 50);
            this.lb_tf.Name = "lb_tf";
            this.lb_tf.Size = new System.Drawing.Size(33, 13);
            this.lb_tf.TabIndex = 2;
            this.lb_tf.Text = "Igaz?";
            // 
            // tb_username
            // 
            this.tb_username.Location = new System.Drawing.Point(159, 3);
            this.tb_username.Name = "tb_username";
            this.tb_username.ReadOnly = true;
            this.tb_username.Size = new System.Drawing.Size(100, 20);
            this.tb_username.TabIndex = 3;
            // 
            // tb_datetime
            // 
            this.tb_datetime.Location = new System.Drawing.Point(159, 28);
            this.tb_datetime.Name = "tb_datetime";
            this.tb_datetime.ReadOnly = true;
            this.tb_datetime.Size = new System.Drawing.Size(100, 20);
            this.tb_datetime.TabIndex = 4;
            // 
            // tb_tf
            // 
            this.tb_tf.Location = new System.Drawing.Point(159, 53);
            this.tb_tf.Name = "tb_tf";
            this.tb_tf.ReadOnly = true;
            this.tb_tf.Size = new System.Drawing.Size(100, 20);
            this.tb_tf.TabIndex = 5;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btn_create, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btn_AddData, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_Update, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_Delete, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(567, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 52.54237F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47.45763F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 66F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(230, 126);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // btn_create
            // 
            this.btn_create.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_create.Location = new System.Drawing.Point(3, 34);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(101, 22);
            this.btn_create.TabIndex = 3;
            this.btn_create.Text = "ADB Létrehoz";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // btn_AddData
            // 
            this.btn_AddData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_AddData.Location = new System.Drawing.Point(3, 3);
            this.btn_AddData.Name = "btn_AddData";
            this.btn_AddData.Size = new System.Drawing.Size(101, 25);
            this.btn_AddData.TabIndex = 2;
            this.btn_AddData.Text = "Rögzít";
            this.btn_AddData.UseVisualStyleBackColor = true;
            this.btn_AddData.Click += new System.EventHandler(this.btn_AddData_Click);
            // 
            // btn_Update
            // 
            this.btn_Update.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Update.Location = new System.Drawing.Point(110, 3);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(117, 25);
            this.btn_Update.TabIndex = 4;
            this.btn_Update.Text = "Frissít";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Delete.Location = new System.Drawing.Point(110, 34);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(117, 22);
            this.btn_Delete.TabIndex = 5;
            this.btn_Delete.Text = "Töröl";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // Ablak_SQLite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Ablak_SQLite";
            this.Text = "SQLite teszt";
            this.Load += new System.EventHandler(this.Ablak_SQLite_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ShowData)).EndInit();
            this.tblP_ShowUserData.ResumeLayout(false);
            this.tblP_ShowUserData.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgv_ShowData;
        private System.Windows.Forms.TableLayoutPanel tblP_ShowUserData;
        private System.Windows.Forms.Label lb_username;
        private System.Windows.Forms.Label lb_datetime;
        private System.Windows.Forms.Label lb_tf;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.TextBox tb_datetime;
        private System.Windows.Forms.TextBox tb_tf;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btn_create;
        private System.Windows.Forms.Button btn_AddData;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Button btn_Delete;
    }
}