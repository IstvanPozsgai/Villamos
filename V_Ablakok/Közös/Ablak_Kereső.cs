using System;
using System.Windows.Forms;
using Villamos.Villamos_Ablakok;

namespace Villamos
{
    public partial class Ablak_Kereső : Form
    {
        public string Keresendő { get; set; }
        public event Event_Kidobó Ismétlődő_Változás;


        public Ablak_Kereső()
        {
            InitializeComponent();

        }

        private void Ablak_Kereső_Load(object sender, EventArgs e)
        {
            AcceptButton = Keresés_OK;
        }

        public void Keresés_OK_Click(object sender, EventArgs e)
        {
            Keresés();
        }

        public void Keresés()
        {
            Keresendő = Keresett.Text.Trim();
            if (Ismétlődő_Változás != null) Ismétlődő_Változás();
        }

        private void Ablak_Kereső_KeyDown(object sender, KeyEventArgs e)
        {
            //Esc
            if ((int)e.KeyCode == 27)
                      this.Close ();    
            
        }
    }
}
