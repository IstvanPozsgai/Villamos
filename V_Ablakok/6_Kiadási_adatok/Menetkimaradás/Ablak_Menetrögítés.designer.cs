using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Villamos
{
     public partial class Ablak_Menetrögítés : Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ablak_Menetrögítés));
            this.Label1 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label14 = new System.Windows.Forms.Label();
            this.Dátum = new System.Windows.Forms.DateTimePicker();
            this.idő = new System.Windows.Forms.DateTimePicker();
            this.txtsorszám = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label8 = new System.Windows.Forms.Label();
            this.chktörlés = new System.Windows.Forms.CheckBox();
            this.txteseményjele = new System.Windows.Forms.TextBox();
            this.txtviszonylat = new System.Windows.Forms.TextBox();
            this.txtpályaszám = new System.Windows.Forms.TextBox();
            this.txttípus = new System.Windows.Forms.TextBox();
            this.txtmenet = new System.Windows.Forms.TextBox();
            this.txtjelentés = new System.Windows.Forms.TextBox();
            this.txttétel = new System.Windows.Forms.TextBox();
            this.txtjvbeírás = new System.Windows.Forms.TextBox();
            this.txtvmbeírás = new System.Windows.Forms.TextBox();
            this.txthibajavítás = new System.Windows.Forms.TextBox();
            this.txthely = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Snow;
            this.Label1.Location = new System.Drawing.Point(199, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(76, 24);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Dátum:";
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Snow;
            this.Label2.Location = new System.Drawing.Point(12, 9);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(76, 24);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Sorszám:";
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.Snow;
            this.Label3.Location = new System.Drawing.Point(417, 9);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(97, 24);
            this.Label3.TabIndex = 2;
            this.Label3.Text = "Időpont:";
            // 
            // Label10
            // 
            this.Label10.BackColor = System.Drawing.Color.Snow;
            this.Label10.Location = new System.Drawing.Point(12, 173);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(169, 24);
            this.Label10.TabIndex = 9;
            this.Label10.Text = "Járművezetői beírás:";
            // 
            // Label11
            // 
            this.Label11.BackColor = System.Drawing.Color.Snow;
            this.Label11.Location = new System.Drawing.Point(12, 141);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(169, 24);
            this.Label11.TabIndex = 10;
            this.Label11.Text = "Típus:";
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.Snow;
            this.Label12.Location = new System.Drawing.Point(12, 109);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(169, 24);
            this.Label12.TabIndex = 11;
            this.Label12.Text = "Pályaszám:";
            // 
            // Label13
            // 
            this.Label13.BackColor = System.Drawing.Color.Snow;
            this.Label13.Location = new System.Drawing.Point(12, 77);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(169, 24);
            this.Label13.TabIndex = 12;
            this.Label13.Text = "Viszonylat: ";
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.Color.Snow;
            this.Label14.Location = new System.Drawing.Point(12, 45);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(169, 24);
            this.Label14.TabIndex = 13;
            this.Label14.Text = "Esemény jele:";
            // 
            // Dátum
            // 
            this.Dátum.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Dátum.Location = new System.Drawing.Point(281, 9);
            this.Dátum.Name = "Dátum";
            this.Dátum.Size = new System.Drawing.Size(118, 24);
            this.Dátum.TabIndex = 14;
            // 
            // idő
            // 
            this.idő.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.idő.Location = new System.Drawing.Point(520, 9);
            this.idő.Name = "idő";
            this.idő.Size = new System.Drawing.Size(118, 24);
            this.idő.TabIndex = 15;
            // 
            // txtsorszám
            // 
            this.txtsorszám.Enabled = false;
            this.txtsorszám.Location = new System.Drawing.Point(94, 9);
            this.txtsorszám.Name = "txtsorszám";
            this.txtsorszám.Size = new System.Drawing.Size(87, 24);
            this.txtsorszám.TabIndex = 16;
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.Snow;
            this.Label4.Location = new System.Drawing.Point(12, 238);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(169, 24);
            this.Label4.TabIndex = 17;
            this.Label4.Text = "Vonalműszaki javítás:";
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.Snow;
            this.Label5.Location = new System.Drawing.Point(12, 303);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(169, 24);
            this.Label5.TabIndex = 18;
            this.Label5.Text = "Hiba javítása:";
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.Snow;
            this.Label6.Location = new System.Drawing.Point(12, 368);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(169, 24);
            this.Label6.TabIndex = 19;
            this.Label6.Text = "Kimaradt menetek:";
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.Snow;
            this.Label7.Location = new System.Drawing.Point(12, 432);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(169, 24);
            this.Label7.TabIndex = 20;
            this.Label7.Text = "SAP jelentésszám tétel:";
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.Snow;
            this.Label8.Location = new System.Drawing.Point(12, 400);
            this.Label8.Name = "Label8";
            this.Label8.Size = new System.Drawing.Size(169, 24);
            this.Label8.TabIndex = 21;
            this.Label8.Text = "SAP jelentésszám:";
            // 
            // chktörlés
            // 
            this.chktörlés.AutoSize = true;
            this.chktörlés.BackColor = System.Drawing.Color.Snow;
            this.chktörlés.Location = new System.Drawing.Point(199, 464);
            this.chktörlés.Name = "chktörlés";
            this.chktörlés.Size = new System.Drawing.Size(66, 22);
            this.chktörlés.TabIndex = 22;
            this.chktörlés.Text = "Törölt";
            this.chktörlés.UseVisualStyleBackColor = false;
            // 
            // txteseményjele
            // 
            this.txteseményjele.Location = new System.Drawing.Point(199, 45);
            this.txteseményjele.Name = "txteseményjele";
            this.txteseményjele.Size = new System.Drawing.Size(114, 24);
            this.txteseményjele.TabIndex = 23;
            // 
            // txtviszonylat
            // 
            this.txtviszonylat.Location = new System.Drawing.Point(199, 77);
            this.txtviszonylat.Name = "txtviszonylat";
            this.txtviszonylat.Size = new System.Drawing.Size(114, 24);
            this.txtviszonylat.TabIndex = 24;
            // 
            // txtpályaszám
            // 
            this.txtpályaszám.Location = new System.Drawing.Point(199, 109);
            this.txtpályaszám.Name = "txtpályaszám";
            this.txtpályaszám.Size = new System.Drawing.Size(114, 24);
            this.txtpályaszám.TabIndex = 25;
            // 
            // txttípus
            // 
            this.txttípus.Location = new System.Drawing.Point(199, 141);
            this.txttípus.Name = "txttípus";
            this.txttípus.Size = new System.Drawing.Size(114, 24);
            this.txttípus.TabIndex = 26;
            // 
            // txtmenet
            // 
            this.txtmenet.Location = new System.Drawing.Point(199, 368);
            this.txtmenet.Name = "txtmenet";
            this.txtmenet.Size = new System.Drawing.Size(114, 24);
            this.txtmenet.TabIndex = 27;
            // 
            // txtjelentés
            // 
            this.txtjelentés.Location = new System.Drawing.Point(199, 400);
            this.txtjelentés.Name = "txtjelentés";
            this.txtjelentés.Size = new System.Drawing.Size(114, 24);
            this.txtjelentés.TabIndex = 28;
            // 
            // txttétel
            // 
            this.txttétel.Location = new System.Drawing.Point(199, 432);
            this.txttétel.Name = "txttétel";
            this.txttétel.Size = new System.Drawing.Size(114, 24);
            this.txttétel.TabIndex = 29;
            // 
            // txtjvbeírás
            // 
            this.txtjvbeírás.Location = new System.Drawing.Point(199, 173);
            this.txtjvbeírás.Multiline = true;
            this.txtjvbeírás.Name = "txtjvbeírás";
            this.txtjvbeírás.Size = new System.Drawing.Size(564, 57);
            this.txtjvbeírás.TabIndex = 30;
            // 
            // txtvmbeírás
            // 
            this.txtvmbeírás.Enabled = false;
            this.txtvmbeírás.Location = new System.Drawing.Point(199, 238);
            this.txtvmbeírás.Multiline = true;
            this.txtvmbeírás.Name = "txtvmbeírás";
            this.txtvmbeírás.Size = new System.Drawing.Size(564, 57);
            this.txtvmbeírás.TabIndex = 31;
            // 
            // txthibajavítás
            // 
            this.txthibajavítás.Location = new System.Drawing.Point(199, 303);
            this.txthibajavítás.Multiline = true;
            this.txthibajavítás.Name = "txthibajavítás";
            this.txthibajavítás.Size = new System.Drawing.Size(564, 57);
            this.txthibajavítás.TabIndex = 32;
            // 
            // txthely
            // 
            this.txthely.Location = new System.Drawing.Point(368, 45);
            this.txthely.Name = "txthely";
            this.txthely.Size = new System.Drawing.Size(100, 24);
            this.txthely.TabIndex = 34;
            this.txthely.Visible = false;
            // 
            // Ablak_Menetrögítés
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Tomato;
            this.ClientSize = new System.Drawing.Size(774, 498);
            this.Controls.Add(this.txthely);
            this.Controls.Add(this.txthibajavítás);
            this.Controls.Add(this.txtvmbeírás);
            this.Controls.Add(this.txtjvbeírás);
            this.Controls.Add(this.txttétel);
            this.Controls.Add(this.txtjelentés);
            this.Controls.Add(this.txtmenet);
            this.Controls.Add(this.txttípus);
            this.Controls.Add(this.txtpályaszám);
            this.Controls.Add(this.txtviszonylat);
            this.Controls.Add(this.txteseményjele);
            this.Controls.Add(this.chktörlés);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.txtsorszám);
            this.Controls.Add(this.idő);
            this.Controls.Add(this.Dátum);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.Label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Ablak_Menetrögítés";
            this.Text = "Menetkimaradás megjelenítése";
            this.Load += new System.EventHandler(this.Ablak_Menetrögítés_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal Label Label1;
        internal Label Label2;
        internal Label Label3;
        internal Label Label10;
        internal Label Label11;
        internal Label Label12;
        internal Label Label13;
        internal Label Label14;
        internal DateTimePicker Dátum;
        internal DateTimePicker idő;
        internal TextBox txtsorszám;
        internal Label Label4;
        internal Label Label5;
        internal Label Label6;
        internal Label Label7;
        internal Label Label8;
        internal CheckBox chktörlés;
        internal TextBox txteseményjele;
        internal TextBox txtviszonylat;
        internal TextBox txtpályaszám;
        internal TextBox txttípus;
        internal TextBox txtmenet;
        internal TextBox txtjelentés;
        internal TextBox txttétel;
        internal TextBox txtjvbeírás;
        internal TextBox txtvmbeírás;
        internal TextBox txthibajavítás;
        internal TextBox txthely;
    }
}