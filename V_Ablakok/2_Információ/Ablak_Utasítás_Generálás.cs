using System;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;

namespace Villamos.V_Ablakok.Közös
{
    public partial class Ablak_Utasítás_Generálás : Form
    {
        readonly Kezelő_Hétvége_Beosztás KézHBeosztás = new Kezelő_Hétvége_Beosztás();
        readonly Kezelő_Utasítás KézUtasítás = new Kezelő_Utasítás();

        public string Telephely { get; private set; }
        public string Előterv { get; private set; }

        public Ablak_Utasítás_Generálás(string telephely)
        {
            Telephely = telephely;
            InitializeComponent();
        }


        public Ablak_Utasítás_Generálás(string telephely, string előterv)
        {
            InitializeComponent();
            Telephely = telephely;
            Előterv = előterv;
        }

        private void Ablak_Utasítás_Generálás_Load(object sender, EventArgs e)
        {
            Txtírásimező.Text = Előterv;
        }


        private void Btnrögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Txtírásimező.Text.Trim() == "") return;
                // megtisztítjuk a szöveget

                Txtírásimező.Text = Txtírásimező.Text.Replace('"', '°').Replace('\'', '°');

                Adat_Utasítás ADAT = new Adat_Utasítás(
                              0,
                              Txtírásimező.Text.Trim(),
                              Program.PostásNév.Trim(),
                              DateTime.Now,
                              0);
                KézUtasítás.Rögzítés(Cmbtelephely.Text.Trim(), DateTime.Today.Year, ADAT);

                MessageBox.Show($"Az utasítás rögzítése megtörtént!", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
