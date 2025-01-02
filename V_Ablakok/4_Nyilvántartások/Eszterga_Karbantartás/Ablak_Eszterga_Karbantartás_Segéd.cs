using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga
{
    public partial class Ablak_Eszterga_Karbantartás_Segéd : Form
    {
        #region osztalyszintű elemek
        readonly string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Kerékeszterga\Eszterga_Karbantartás.mdb";
        readonly string jelszó = "bozaim";
        readonly string MaiDátum = DateTime.Today.ToShortDateString();
        private List<Adat_Eszterga_Üzemóra> AdatokÜzemóra;
        public int Üzemóra { get; private set; }
        #endregion

        #region Alap
        public Ablak_Eszterga_Karbantartás_Segéd()
        {
            InitializeComponent();
        }
        private void Ablak_Eszterga_Karbantartás_Segéd_Load(object sender, EventArgs e)
        {
            LblSzöveg.Text = $"Írja be mai napi Üzemóra állását.";

            AdatokÜzemóra = Eszterga_Funkció.Eszterga_ÜzemóraFeltölt();
            Adat_Eszterga_Üzemóra rekord = (from a in AdatokÜzemóra
                                            where a.Státus != true
                                            orderby a.Dátum descending
                                            select a).FirstOrDefault();
            if (rekord == null)
            {
                LblElözö.Text = "Még nem volt üzemóra rögzítés\n az adatbázisban.";
                return;
            }
            else if (rekord != null && rekord.Dátum.ToShortDateString() == DateTime.Today.ToShortDateString())
            {
                MessageBox.Show("A mai napon már rögzítettek üzemóra adatot.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }

            LblElözö.Text = $"Előző Üzemóra ami rögzítésre került:\nÜzemóra: {rekord.Üzemóra}\nDátum: {rekord.Dátum.ToShortDateString()}";
        }
        private void Ablak_Eszterga_Karbantartás_Segéd_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (this.DialogResult != DialogResult.OK)
                    this.DialogResult = DialogResult.Cancel;
            }
        }
        #endregion

        #region Gombok
        private void BtnRogzit_Click(object sender, EventArgs e)
        {
            if (int.TryParse(TxtBxUzemOra.Text, out int uzemOra))
            {
                AdatokÜzemóra = Eszterga_Funkció.Eszterga_ÜzemóraFeltölt();

                Adat_Eszterga_Üzemóra rekord = AdatokÜzemóra.OrderByDescending(a => a.Üzemóra).FirstOrDefault();

                if (rekord != null && uzemOra < rekord.Üzemóra)
                {
                    MessageBox.Show($"Az új üzemóra érték nem lehet kisebb, mint az előző: {rekord.Üzemóra}.", "Hiba.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TxtBxUzemOra.Focus();
                    return;
                }
                Üzemóra = uzemOra;

                string szöveg = $"INSERT INTO Üzemóra (Üzemóra, Dátum) VALUES(";
                szöveg += $"'{uzemOra}', ";
                szöveg += $"'{DateTime.Today.ToShortDateString()}')";
                MyA.ABMódosítás(hely, jelszó, szöveg);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                MessageBox.Show("Kérem adjon meg egy érvényes számot!", "Hiba.", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion
    }
}
