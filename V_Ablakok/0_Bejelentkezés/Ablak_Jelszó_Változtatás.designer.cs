using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace Villamos
{
    public partial class Ablak_Jelszó_Változtatás : Form
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

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Jelszó_Változtatás));
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Első = new System.Windows.Forms.TextBox();
            this.Második = new System.Windows.Forms.TextBox();
            this.Btnok = new System.Windows.Forms.Button();
            this.BtnMégse = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TxtPassword
            // 
            this.TxtPassword.Location = new System.Drawing.Point(163, 6);
            this.TxtPassword.Margin = new System.Windows.Forms.Padding(6);
            this.TxtPassword.MaxLength = 15;
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(195, 26);
            this.TxtPassword.TabIndex = 43;
            this.TxtPassword.UseSystemPasswordChar = true;
            this.TxtPassword.MouseLeave += new System.EventHandler(this.TxtPassword_MouseLeave);
            this.TxtPassword.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TxtPassword_MouseMove);
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(6, 6);
            this.Label4.Margin = new System.Windows.Forms.Padding(6);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(95, 20);
            this.Label4.TabIndex = 44;
            this.Label4.Text = "Régi Jelszó:";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(6, 82);
            this.Label1.Margin = new System.Windows.Forms.Padding(6);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(119, 20);
            this.Label1.TabIndex = 45;
            this.Label1.Text = "Új Jelszó ismét:";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(6, 44);
            this.Label2.Margin = new System.Windows.Forms.Padding(6);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(72, 20);
            this.Label2.TabIndex = 46;
            this.Label2.Text = "Új jelszó:";
            // 
            // Első
            // 
            this.Első.Location = new System.Drawing.Point(163, 44);
            this.Első.Margin = new System.Windows.Forms.Padding(6);
            this.Első.MaxLength = 15;
            this.Első.Name = "Első";
            this.Első.Size = new System.Drawing.Size(195, 26);
            this.Első.TabIndex = 47;
            this.Első.UseSystemPasswordChar = true;
            this.Első.MouseLeave += new System.EventHandler(this.Első_MouseLeave);
            this.Első.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Első_MouseMove);
            // 
            // Második
            // 
            this.Második.Location = new System.Drawing.Point(163, 82);
            this.Második.Margin = new System.Windows.Forms.Padding(6);
            this.Második.MaxLength = 15;
            this.Második.Name = "Második";
            this.Második.Size = new System.Drawing.Size(195, 26);
            this.Második.TabIndex = 48;
            this.Második.UseSystemPasswordChar = true;
            this.Második.MouseLeave += new System.EventHandler(this.Második_MouseLeave);
            this.Második.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Második_MouseMove);
            // 
            // Btnok
            // 
            this.Btnok.Location = new System.Drawing.Point(163, 120);
            this.Btnok.Margin = new System.Windows.Forms.Padding(6);
            this.Btnok.Name = "Btnok";
            this.Btnok.Size = new System.Drawing.Size(145, 52);
            this.Btnok.TabIndex = 49;
            this.Btnok.Text = "OK";
            this.Btnok.UseVisualStyleBackColor = true;
            this.Btnok.Click += new System.EventHandler(this.Btnok_Click);
            // 
            // BtnMégse
            // 
            this.BtnMégse.Location = new System.Drawing.Point(6, 120);
            this.BtnMégse.Margin = new System.Windows.Forms.Padding(6);
            this.BtnMégse.Name = "BtnMégse";
            this.BtnMégse.Size = new System.Drawing.Size(145, 52);
            this.BtnMégse.TabIndex = 50;
            this.BtnMégse.Text = "Mégse";
            this.BtnMégse.UseVisualStyleBackColor = true;
            this.BtnMégse.Click += new System.EventHandler(this.BtnMégse_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.BtnMégse, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.Második, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.Első, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.TxtPassword, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Label1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.Label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.Btnok, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(368, 183);
            this.tableLayoutPanel1.TabIndex = 53;
            // 
            // AblakJelszóváltoztatás
            // 
            this.AcceptButton = this.Btnok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 188);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AblakJelszóváltoztatás";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jelszóváltoztatás";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AblakJelszóváltoztatás_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        internal TextBox TxtPassword;
        internal Label Label4;
        internal Label Label1;
        internal Label Label2;
        internal TextBox Első;
        internal TextBox Második;
        internal Button Btnok;
        internal Button BtnMégse;
        private TableLayoutPanel tableLayoutPanel1;
    }
}