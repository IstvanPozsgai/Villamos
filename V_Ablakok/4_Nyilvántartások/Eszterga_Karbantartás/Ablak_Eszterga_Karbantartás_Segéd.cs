using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Villamos.V_Ablakok._4_Nyilvántartások.Eszterga_Karbantartás;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.Kerékeszterga
{
    public partial class Ablak_Eszterga_Karbantartás_Segéd : Form
    {
        #region osztalyszintű elemek
        List<Adat_Eszterga_Uzemora> AdatokUzemora = new List<Adat_Eszterga_Uzemora>();
        readonly Kezelő_Eszterga_Üzemóra Kez_Uzemora = new Kezelő_Eszterga_Üzemóra();
        // JAVÍTANDÓ:Biztos?
        readonly bool Baross = Program.PostásTelephely.Trim() == "Angyalföld";
        public int Uzemora { get; private set; }
        #endregion

        #region Alap

        /// <summary>
        /// Inicializálja a segédablak komponenseit, majd meghívja a jogosultságkezelést.
        /// </summary>
        public Ablak_Eszterga_Karbantartás_Segéd()
        {
            InitializeComponent();
            Jogosultsagkiosztas();
        }

        /// <summary>
        /// Beállítja a jogosultságok alapján az űrlap vezérlőinek láthatóságát és engedélyezettségét.
        /// Jogosultság alapján engedélyezi vagy tiltja az üzemóra mezőt és a rögzítő gombot.
        /// </summary>
        private void Jogosultsagkiosztas()
        {
            try
            {
                int melyikelem;
                melyikelem = 160;
                BtnRogzit.Visible = Baross;
                TxtBxUzemOra.Enabled = Baross;

                //módosítás 1
                //Ablak_Eszterga_Karbantartás oldalon is felhasználva.

                //módosítás 2
                BtnRogzit.Enabled = MyF.Vanjoga(melyikelem, 2);

                //módosítás 3
                //Ablak_Eszterga_Karbantartás_Módosít oldalon felhasználva.
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


        /// <summary>
        /// Az ablak betöltésekor eldönti, hogy a felhasználó rögzíthet-e új üzemóra adatot.  
        /// Ha nincs jogosultsága, megjeleníti az utolsó rögzített üzemórát, majd bezárja az ablakot.  
        /// Ha van jogosultsága, előkészíti a mezőket a rögzítéshez, és ellenőrzi, történt-e már mai napi rögzítés.
        /// </summary>
        private void Ablak_Eszterga_Karbantartás_Segéd_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Baross || !MyF.Vanjoga(160, 1))
                {
                    AdatokUzemora = Kez_Uzemora.Lista_Adatok();
                    Adat_Eszterga_Uzemora Uzemora = (from a in AdatokUzemora
                                                     where !a.Státus
                                                     orderby a.Dátum descending
                                                     select a).FirstOrDefault();

                    if (Uzemora != null)
                        LblElözö.Text = $"Előző napi Üzemóra:\nÜzemóra: {Uzemora.Uzemora}\nDátum: {Uzemora.Dátum.ToShortDateString()}";
                    else
                        LblElözö.Text = "Nincs előző napi üzemóra rögzítve.";

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    return;
                }

                LblSzöveg.Text = $"Írja be mai napi Üzemóra állását.";

                AdatokUzemora = Kez_Uzemora.Lista_Adatok();
                Adat_Eszterga_Uzemora rekord = (from a in AdatokUzemora
                                                where !a.Státus
                                                orderby a.Dátum descending
                                                select a).FirstOrDefault();
                if (rekord == null)
                {
                    LblElözö.Text = "Még nem volt üzemóra rögzítés\n az adatbázisban.";
                    return;
                }
                else if (rekord != null && rekord.Dátum == DateTime.Today)
                {
                    MessageBox.Show("A mai napon már rögzítettek üzemóra adatot.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }

                LblElözö.Text = $"Előző Üzemóra ami rögzítésre került:\nÜzemóra: {rekord.Uzemora}\nDátum: {rekord.Dátum.ToShortDateString()}";
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

        /// <summary>
        /// Az ablak bezárásakor beállítja a visszatérési értéket a dialógushoz.
        /// Ha a felhasználó bezárta az ablakot, és nincs rögzítés, a kilépés „Cancel” eredménnyel történik.
        /// Jogosultság nélküli felhasználó esetén automatikusan „OK” lesz az eredmény.
        /// </summary>
        private void Ablak_Eszterga_Karbantartás_Segéd_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Baross && e.CloseReason == CloseReason.UserClosing)
                this.DialogResult = DialogResult.OK;

            else if (e.CloseReason == CloseReason.UserClosing)
            {
                if (this.DialogResult != DialogResult.OK)
                    this.DialogResult = DialogResult.Cancel;
            }
        }
        #endregion

        #region Gombok

        /// <summary>
        /// Ellenőrzi az üzemóra mező értékét, majd ha az érvényes és nem kisebb az utolsó rögzített értéknél,
        /// elmenti új üzemóra rekordként az aktuális napra. 
        /// </summary>
        private void BtnRogzit_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(TxtBxUzemOra.Text, out int uzemOra) && uzemOra >= 0)
                {
                    AdatokUzemora = Kez_Uzemora.Lista_Adatok();

                    Adat_Eszterga_Uzemora rekord = (from a in AdatokUzemora
                                                    where !a.Státus
                                                    orderby a.Uzemora descending
                                                    select a).FirstOrDefault();

                    if (rekord != null && uzemOra < rekord.Uzemora)
                    {
                        MessageBox.Show($"Az új üzemóra érték nem lehet kisebb, mint az előző: {rekord.Uzemora}.", "Hiba.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        TxtBxUzemOra.Focus();
                        return;
                    }
                    Uzemora = uzemOra;

                    Adat_Eszterga_Uzemora ADAT = new Adat_Eszterga_Uzemora(0,
                                                                  uzemOra,
                                                                  DateTime.Today,
                                                                  false);
                    Kez_Uzemora.Rogzites(ADAT);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                    MessageBox.Show("Kérem adjon meg egy érvényes számot!", "Hiba.", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        #endregion

        private void TxtBxUzemOra_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BtnRogzit.PerformClick();
            }
        }
    }
}
