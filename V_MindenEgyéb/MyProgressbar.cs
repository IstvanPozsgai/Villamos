using System.Drawing;
using System.Windows.Forms;
using MyColor = Villamos.V_MindenEgyéb.Kezelő_Szín;

namespace Villamos.V_MindenEgyéb
{
    public class MyProgressbar : ProgressBar
    {
        Brush _ForeColor = new SolidBrush(Color.DarkOliveGreen);
        public MyProgressbar()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        /// <summary>
        ///  Ezt a változatot kódban kell megadni.
        /// </summary>
        /// <param name="Ablak">Amelyik formon el akarjuk helyezni</param>
        /// <param name="maximum">Mennyire legyen felosztva</param>
        /// <param name="X">Bal felső sarok X mérete, ha -1 a szélétől 20 egységre</param>
        /// <param name="Y">Bal felső sarok Y mérete, ha -1, akkor képernyő közepe</param>  
        /// <param name="SzínHex">Színszám Hex-ban alapértelmezett szín a Color.DarkOliveGreen </param>
        /// <param name="Hossz">Milyen hosszú legyen a csík, ha -1 akkor ablak.width-60 </param>
        /// <param name="Magasság">A csík magassága, ha -1 akkor 25 </param>
        public void Be(Form Ablak, int Maximum = 20, int X = -1, int Y = -1, string SzínHex = "#556B2F", int Hossz = -1, int Magasság = -1)
        {
            this.Name = "Holtart";
            this.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));

            if (X == -1) X = 20;
            if (Y == -1) Y = (Ablak.Height / 2) - 25;
            this.Location = new System.Drawing.Point(X, Y);

            if (Hossz == -1) Hossz = Ablak.Width - 60;
            if (Magasság == -1) Magasság = 25;
            this.Size = new System.Drawing.Size(Hossz, Magasság);

            this.Maximum = Maximum;
            this.Value = 0;

            this.Visible = true;

            if (SzínHex != "#556B2F") _ForeColor = new SolidBrush(MyColor.HexToColor(SzínHex));

            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;

            Ablak.Controls.Add(this);
            this.BringToFront();
        }

        public void Be(int Maximum = 20, string SzínHex = "#556B2F")
        {
            this.Maximum = Maximum;
            this.Value = 0;
            this.Visible = true;
            if (SzínHex != "#556B2F") _ForeColor = new SolidBrush(MyColor.HexToColor(SzínHex));
        }

        public void Lép()
        {
            if (this.Maximum <= ++this.Value) this.Value = 0;
        }

        public void Ki()
        {
            this.Visible = false;
            this.Value = 0;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;

            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            rec.Height = rec.Height - 4;

            e.Graphics.FillRectangle(_ForeColor, 2, 2, rec.Width, rec.Height);
        }
    }
}
