namespace Villamos
{
    partial class Ablak_Hibanaplo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Hibanaplo));
            this.Részletek = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // Részletek
            // 
            this.Részletek.BackgroundImage = global::Villamos.Properties.Resources.App_dict;
            this.Részletek.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Részletek.Location = new System.Drawing.Point(2, 2);
            this.Részletek.Margin = new System.Windows.Forms.Padding(4);
            this.Részletek.Name = "Részletek";
            this.Részletek.Size = new System.Drawing.Size(45, 43);
            this.Részletek.TabIndex = 191;
            this.toolTip1.SetToolTip(this.Részletek, "Hiba részletei");
            this.Részletek.UseVisualStyleBackColor = true;
            this.Részletek.Click += new System.EventHandler(this.Részletek_Click);
            // 
            // Ablak_Hibanaplo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 261);
            this.Controls.Add(this.Részletek);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Ablak_Hibanaplo";
            this.Text = "Hibanapló";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Ablak_Hibanaplo_Load);
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Button Részletek;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}