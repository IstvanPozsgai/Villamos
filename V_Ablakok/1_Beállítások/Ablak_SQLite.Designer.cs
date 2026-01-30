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
            this.btn_AddData = new System.Windows.Forms.Button();
            this.tblP_ShowUserData = new System.Windows.Forms.TableLayoutPanel();
            this.lb_username = new System.Windows.Forms.Label();
            this.lb_datetime = new System.Windows.Forms.Label();
            this.lb_tf = new System.Windows.Forms.Label();
            this.tb_username = new System.Windows.Forms.TextBox();
            this.tb_datetime = new System.Windows.Forms.TextBox();
            this.tb_tf = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ShowData)).BeginInit();
            this.tblP_ShowUserData.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 29.5F));
            this.tableLayoutPanel1.Controls.Add(this.dgv_ShowData, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btn_AddData, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tblP_ShowUserData, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
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
            this.dgv_ShowData.Location = new System.Drawing.Point(3, 228);
            this.dgv_ShowData.Name = "dgv_ShowData";
            this.dgv_ShowData.Size = new System.Drawing.Size(794, 219);
            this.dgv_ShowData.TabIndex = 0;
            // 
            // btn_AddData
            // 
            this.btn_AddData.Location = new System.Drawing.Point(567, 3);
            this.btn_AddData.Name = "btn_AddData";
            this.btn_AddData.Size = new System.Drawing.Size(75, 23);
            this.btn_AddData.TabIndex = 1;
            this.btn_AddData.Text = "Rögzít";
            this.btn_AddData.UseVisualStyleBackColor = true;
            this.btn_AddData.Click += new System.EventHandler(this.btn_AddData_Click);
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
            this.tblP_ShowUserData.Size = new System.Drawing.Size(558, 219);
            this.tblP_ShowUserData.TabIndex = 2;
            // 
            // lb_username
            // 
            this.lb_username.AutoSize = true;
            this.lb_username.Dock = System.Windows.Forms.DockStyle.Left;
            this.lb_username.Location = new System.Drawing.Point(3, 0);
            this.lb_username.Name = "lb_username";
            this.lb_username.Size = new System.Drawing.Size(27, 71);
            this.lb_username.TabIndex = 0;
            this.lb_username.Text = "Név";
            // 
            // lb_datetime
            // 
            this.lb_datetime.AutoSize = true;
            this.lb_datetime.Location = new System.Drawing.Point(3, 71);
            this.lb_datetime.Name = "lb_datetime";
            this.lb_datetime.Size = new System.Drawing.Size(38, 13);
            this.lb_datetime.TabIndex = 1;
            this.lb_datetime.Text = "Dátum";
            // 
            // lb_tf
            // 
            this.lb_tf.AutoSize = true;
            this.lb_tf.Location = new System.Drawing.Point(3, 142);
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
            this.tb_datetime.Location = new System.Drawing.Point(159, 74);
            this.tb_datetime.Name = "tb_datetime";
            this.tb_datetime.ReadOnly = true;
            this.tb_datetime.Size = new System.Drawing.Size(100, 20);
            this.tb_datetime.TabIndex = 4;
            // 
            // tb_tf
            // 
            this.tb_tf.Location = new System.Drawing.Point(159, 145);
            this.tb_tf.Name = "tb_tf";
            this.tb_tf.ReadOnly = true;
            this.tb_tf.Size = new System.Drawing.Size(100, 20);
            this.tb_tf.TabIndex = 5;
            // 
            // Ablak_SQLite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Ablak_SQLite";
            this.Text = "Ablak_SQLite";
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_ShowData)).EndInit();
            this.tblP_ShowUserData.ResumeLayout(false);
            this.tblP_ShowUserData.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView dgv_ShowData;
        private System.Windows.Forms.Button btn_AddData;
        private System.Windows.Forms.TableLayoutPanel tblP_ShowUserData;
        private System.Windows.Forms.Label lb_username;
        private System.Windows.Forms.Label lb_datetime;
        private System.Windows.Forms.Label lb_tf;
        private System.Windows.Forms.TextBox tb_username;
        private System.Windows.Forms.TextBox tb_datetime;
        private System.Windows.Forms.TextBox tb_tf;
    }
}