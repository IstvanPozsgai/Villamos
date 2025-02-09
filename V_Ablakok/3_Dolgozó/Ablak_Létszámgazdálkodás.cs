using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyEn = Villamos.V_MindenEgyéb.Enumok;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class AblakLétszámgazdálkodás
    {
        #region Kezelők és Listák
        readonly Kezelő_Kulcs KézKulcs = new Kezelő_Kulcs();
        readonly Kezelő_Kulcs_Kettő KézKulcsPénz = new Kezelő_Kulcs_Kettő();
        readonly Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_Kiegészítő_Csoportbeosztás KézSegéd = new Kezelő_Kiegészítő_Csoportbeosztás();
        readonly Kezelő_Dolgozó_Státus Kéz_Státus = new Kezelő_Dolgozó_Státus();
        readonly Kezelő_Dolgozó_Személyes Kezelő_Személyes = new Kezelő_Dolgozó_Személyes();

        List<Adat_Dolgozó_Státus> AdatokStátus = new List<Adat_Dolgozó_Státus>();
        List<Adat_Dolgozó_Személyes> Adatok_Személyes = new List<Adat_Dolgozó_Személyes>();
        List<Adat_Dolgozó_Telephely> AdatokDolgozók = new List<Adat_Dolgozó_Telephely>();
        List<Adat_Kulcs> AdatokPénz = new List<Adat_Kulcs>();
        readonly List<Adat_Kiegészítő_Csoportbeosztás> AdatokSegéd = new List<Adat_Kiegészítő_Csoportbeosztás>();
        #endregion

        int öoszlop = 2;

        #region Alap
        public AblakLétszámgazdálkodás()
        {
            InitializeComponent();
        }

        private void AblakLétszámgazdálkodás_Load(object sender, EventArgs e)
        {
            try
            {
                Jogosultságkiosztás();
                Telephelyekfeltöltése();
                Fülek.SelectedIndex = 0;
                Fülekkitöltése();
                Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
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

        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    Cmbtelephely.Items.Add(Elem);

                Telephelybe.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    Telephelybe.Items.Add(Elem);

                Telephelyki.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    Telephelybe.Items.Add(Elem);


                if (Program.PostásTelephely == "Főmérnökség")
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim(); }
                else
                { Cmbtelephely.Text = Program.PostásTelephely; }

                Cmbtelephely.Enabled = Program.Postás_Vezér;
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

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Kilépőfull.Enabled = false;
                BelépőFull.Enabled = false;
                StatusFull.Enabled = false;
                Kilépő.Enabled = false;
                Belépő.Enabled = false;
                Status.Enabled = false;
                StátusMódosítás.Enabled = false;
                Új_Státus.Enabled = false;
                Command4.Enabled = false;
                Áthelyez.Enabled = false;

                melyikelem = 75;
                // módosítás 1 Részleges rögztítés
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Kilépő.Enabled = true;
                    Belépő.Enabled = true;
                    Status.Enabled = true;
                    Kilépőfull.Visible = false;
                    BelépőFull.Visible = false;
                    StatusFull.Visible = false;
                    Áthelyez.Enabled = true;
                }
                // módosítás 2 teljes rögzítés
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Kilépő.Visible = false;
                    Belépő.Visible = false;
                    Status.Visible = false;
                    Kilépőfull.Enabled = true;
                    BelépőFull.Enabled = true;
                    StatusFull.Enabled = true;
                    Kilépőfull.Visible = true;
                    BelépőFull.Visible = true;
                    StatusFull.Visible = true;
                    StátusMódosítás.Enabled = true;
                    Áthelyez.Enabled = true;
                }
                // módosítás 3 létrehozás/törlés
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Új_Státus.Enabled = true;
                    Command4.Enabled = true;
                }
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

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Dolgozógazdálkodás.html";
                MyE.Megnyitás(hely);
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

        private void Fülekkitöltése()
        {
            try
            {
                switch (Fülek.SelectedIndex)
                {
                    case 0:
                        {
                            // státus listázás
                            Táblaíró();
                            break;
                        }
                    case 1:
                        {
                            // státus módosítás
                            Státusváltozásokfeltöltése();
                            break;
                        }
                    case 2:
                        {
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                }
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

        private void Fülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Határozza meg, hogy melyik lap van jelenleg kiválasztva
            TabPage SelectedTab = Fülek.TabPages[e.Index];

            // Szerezze be a lap fejlécének területét
            Rectangle HeaderRect = Fülek.GetTabRect(e.Index);

            // Hozzon létreecsetet a szöveg megfestéséhez
            SolidBrush BlackTextBrush = new SolidBrush(Color.Black);

            // Állítsa be a szöveg igazítását
            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Festse meg a szöveget a megfelelő félkövér és szín beállítással
            if ((e.State & DrawItemState.Selected) != 0)
            {
                Font BoldFont = new Font(Fülek.Font.Name, Fülek.Font.Size, FontStyle.Bold);
                // háttér szín beállítása
                e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), e.Bounds);
                Rectangle paddedBounds = e.Bounds;
                paddedBounds.Inflate(0, 0);
                e.Graphics.DrawString(SelectedTab.Text, BoldFont, BlackTextBrush, paddedBounds, sf);
            }
            else
            {
                e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);
            }
            // Munka kész – dobja ki a keféket
            BlackTextBrush.Dispose();
        }
        #endregion


        #region Státus listázás
        private void Command6_Click(object sender, EventArgs e)
        {
            Táblaíró();
        }

        private void Táblaíró()
        {
            try
            {
                List<Adat_Dolgozó_Státus> AdatokÖ = Kéz_Státus.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Dolgozó_Státus> Adatok = new List<Adat_Dolgozó_Státus>();

                if (SzűrtLista.Checked)
                {
                    if (NyitottÜres.Checked)
                        Adatok = (from a in AdatokÖ
                                  where a.Státusváltozások != "Státus megszüntetése"
                                  && (a.Hrazonosítóbe == null || a.Hrazonosítóbe == "" || a.Hrazonosítóbe == "_")
                                  && (a.Honnanjött == null || a.Honnanjött.Trim() == "_")
                                  orderby a.ID descending
                                  select a).ToList();

                    if (NyitottFolyamat.Checked)
                        Adatok = (from a in AdatokÖ
                                  where a.Státusváltozások != "Státus megszüntetése"
                                  && (a.Hrazonosítóbe == null || a.Hrazonosítóbe == "" || a.Hrazonosítóbe == "_")
                                  && (a.Honnanjött == null || a.Honnanjött.Trim() != "_")
                                  orderby a.ID descending
                                  select a).ToList();

                    if (MindAKettő.Checked)
                        Adatok = (from a in AdatokÖ
                                  where a.Státusváltozások != "Státus megszüntetése"
                                  && (a.Hrazonosítóbe == null || a.Hrazonosítóbe == "" || a.Hrazonosítóbe == "_")
                                  orderby a.ID descending
                                  select a).ToList();
                }
                else
                    Adatok = AdatokÖ;

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 17;
                Tábla.RowCount = 0;
                // ' fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Sorszám";
                Tábla.Columns[0].Width = 40;
                Tábla.Columns[1].HeaderText = "Kilépő Név";
                Tábla.Columns[1].Width = 190;
                Tábla.Columns[2].HeaderText = "Hr azonosító";
                Tábla.Columns[2].Width = 90;
                Tábla.Columns[3].HeaderText = "Bér";
                Tábla.Columns[3].Width = 75;
                Tábla.Columns[4].HeaderText = "Telephelyről";
                Tábla.Columns[4].Width = 120;
                Tábla.Columns[5].HeaderText = "Kilépés oka";
                Tábla.Columns[5].Width = 250;
                Tábla.Columns[6].HeaderText = "Kilépés Dátuma:";
                Tábla.Columns[6].Width = 100;
                Tábla.Columns[7].HeaderText = "Belépő Név";
                Tábla.Columns[7].Width = 190;
                Tábla.Columns[8].HeaderText = "Hr azonosító";
                Tábla.Columns[8].Width = 90;
                Tábla.Columns[9].HeaderText = "Bér";
                Tábla.Columns[9].Width = 75;
                Tábla.Columns[10].HeaderText = "Régi munkahelye";
                Tábla.Columns[10].Width = 120;
                Tábla.Columns[11].HeaderText = "Telephelyre";
                Tábla.Columns[11].Width = 120;
                Tábla.Columns[12].HeaderText = "Belépési dátum:";
                Tábla.Columns[12].Width = 100;
                Tábla.Columns[13].HeaderText = "Státusváltozás";
                Tábla.Columns[13].Width = 150;
                Tábla.Columns[14].HeaderText = "Státusváltozás oka:";
                Tábla.Columns[14].Width = 250;
                Tábla.Columns[15].HeaderText = "Megjegyzés";
                Tábla.Columns[15].Width = 250;
                Tábla.Columns[16].HeaderText = "RészMunkaidős";
                Tábla.Columns[16].Width = 150;

                foreach (Adat_Dolgozó_Státus rekord in Adatok)
                {
                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.ID;
                    Tábla.Rows[i].Cells[1].Value = rekord.Névki.Trim();
                    Tábla.Rows[i].Cells[2].Value = rekord.Hrazonosítóki.Trim();
                    Tábla.Rows[i].Cells[3].Value = rekord.Bérki;
                    Tábla.Rows[i].Cells[4].Value = rekord.Telephelyki.Trim();
                    Tábla.Rows[i].Cells[5].Value = rekord.Kilépésoka.Trim();
                    Tábla.Rows[i].Cells[6].Value = rekord.Kilépésdátum.ToString("yyyy.MM.dd");
                    Tábla.Rows[i].Cells[7].Value = rekord.Névbe.Trim();
                    Tábla.Rows[i].Cells[8].Value = rekord.Hrazonosítóbe.Trim();
                    Tábla.Rows[i].Cells[9].Value = rekord.Bérbe;
                    Tábla.Rows[i].Cells[10].Value = rekord.Honnanjött.Trim();
                    Tábla.Rows[i].Cells[11].Value = rekord.Telephelybe.Trim();
                    Tábla.Rows[i].Cells[12].Value = rekord.Belépésidátum.ToString("yyyy.MM.dd");
                    Tábla.Rows[i].Cells[13].Value = rekord.Státusváltozások;
                    Tábla.Rows[i].Cells[14].Value = rekord.Státusváltozoka.Trim();
                    Tábla.Rows[i].Cells[15].Value = rekord.Megjegyzés.Trim();
                    if (rekord.Részmunkaidős == -1)
                        Tábla.Rows[i].Cells[16].Value = "Részmunkaidős";
                    else
                        Tábla.Rows[i].Cells[16].Value = "";
                }
                Tábla.Visible = true;
                Tábla.Refresh();
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

        private void SzűrtLista_CheckedChanged(object sender, EventArgs e)
        {
            Panel3.Visible = SzűrtLista.Checked;
            Táblaíró();
        }

        private void NyitottÜres_Click(object sender, EventArgs e)
        {
            Táblaíró();
        }

        private void NyitottFolyamat_Click(object sender, EventArgs e)
        {
            Táblaíró();
        }

        private void MindAKettő_Click(object sender, EventArgs e)
        {
            Táblaíró();
        }

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (!long.TryParse(Tábla.Rows[e.RowIndex].Cells[0].Value.ToString(), out long sorszám)) return;
            Kiirjaid(sorszám);
            Fülek.SelectedIndex = 1;
        }

        private void Tábla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // egész sor színezése ha törölt
            foreach (DataGridViewRow row in Tábla.Rows)
            {
                if (row.Cells[13].Value.ToString().Trim() == "Státus megszüntetése")
                {
                    row.DefaultCellStyle.ForeColor = Color.White;
                    row.DefaultCellStyle.BackColor = Color.IndianRed;
                    row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
                }
                if (row.Cells[13].Value.ToString().Trim() == "Státus létrehozása")
                {
                    row.DefaultCellStyle.ForeColor = Color.White;
                    row.DefaultCellStyle.BackColor = Color.ForestGreen;
                    row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Bold);
                }
            }
        }
        #endregion


        #region Státus módosítás
        private void Kiirjaid(long sorszám)
        {
            try
            {
                Kiürítiamezőket();
                List<Adat_Dolgozó_Státus> Adatok = Kéz_Státus.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Dolgozó_Státus rekord = (from a in Adatok
                                              where a.ID == sorszám
                                              select a).FirstOrDefault();
                if (rekord != null)
                {
                    Id.Text = rekord.ID.ToString();
                    Névki.Text = rekord.Névki.Trim();
                    Hrazonosítóki.Text = rekord.Hrazonosítóki.Trim();
                    Bérki.Text = rekord.Bérki.ToString();
                    Telephelyki.Text = rekord.Telephelyki.Trim();
                    KilépésOka.Text = rekord.Kilépésoka.Trim();
                    KilépésDátum.Value = rekord.Kilépésdátum;
                    Névbe.Text = rekord.Névbe.Trim();
                    Hrazonosítóbe.Text = rekord.Hrazonosítóbe.Trim();
                    Bérbe.Text = rekord.Bérbe.ToString();
                    Honnanjött.Text = rekord.Honnanjött.Trim();
                    Telephelybe.Text = rekord.Telephelybe.Trim();
                    Belépésidátum.Value = rekord.Belépésidátum;
                    Státusváltozásoka.Text = rekord.Státusváltozoka.Trim();
                    Label22.Text = rekord.Státusváltozások.Trim();
                    Megjegyzés.Text = rekord.Megjegyzés.Trim();
                    if (Label22.Text.Trim() == "Státus megszüntetése")
                    {
                        Panel6.BackColor = Color.Red;
                        Belépő.Enabled = false;
                        BelépőFull.Enabled = false;
                    }
                    else
                    {
                        Panel6.BackColor = Color.Turquoise;
                        Belépő.Enabled = true;
                        BelépőFull.Enabled = true;
                    }
                    if (Label22.Text.Trim() == "Státus létrehozása")
                    {
                        Panel5.BackColor = Color.Red;
                        Kilépő.Enabled = false;
                        Kilépőfull.Enabled = false;
                    }
                    else
                    {
                        Panel5.BackColor = Color.MediumSpringGreen;
                        Kilépő.Enabled = true;
                        Kilépőfull.Enabled = true;
                    }
                    if (rekord.Megjegyzés.Trim() == "")
                        Megjegyzés.Text = rekord.Megjegyzés.Trim();

                    if (rekord.Részmunkaidős == -1)
                        Check1.Checked = true;
                    else
                        Check1.Checked = false;
                }
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

        private void Kiürítiamezőket()
        {
            Id.Text = "";
            Névki.Text = "";
            Hrazonosítóki.Text = "";
            Bérki.Text = "";
            Telephelyki.Text = "";
            KilépésOka.Text = "";
            KilépésDátum.Value = new DateTime(1900, 1, 1);
            Névbe.Text = "";
            Hrazonosítóbe.Text = "";
            Bérbe.Text = "";
            Honnanjött.Text = "";
            Telephelybe.Text = "";
            Belépésidátum.Value = new DateTime(1900, 1, 1);
            Státusváltozásoka.Text = "";
            Label22.Text = "";
            Megjegyzés.Text = "";
            Check1.Checked = false;
        }

        private void Kilépőfull_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Id.Text, out int sorszám)) throw new HibásBevittAdat("Nincs kitöltve a sorszám, így nem kerül rögzítésre.");
                if (!double.TryParse(Bérki.Text, out double BérKi))
                {
                    BérKi = 0;
                    Bérki.Text = BérKi.ToString();
                }
                if (KilépésOka.Text.Trim() == "") KilépésOka.Text = "_";

                AdatokStátus = Kéz_Státus.Lista_Adatok(Cmbtelephely.Text.Trim());
                bool vane = AdatokStátus.Any(adat => adat.ID == sorszám);

                if (vane)
                {
                    Adat_Dolgozó_Státus ADAT = new Adat_Dolgozó_Státus(sorszám,
                                                                       Névki.Text.Trim(),
                                                                       Hrazonosítóki.Text.Trim(),
                                                                       BérKi,
                                                                       Telephelyki.Text.Trim(),
                                                                       KilépésOka.Text.Trim(),
                                                                       KilépésDátum.Value);
                    Kéz_Státus.Módosít_Kilép_Teljes(Cmbtelephely.Text.Trim(), ADAT);
                }
                Kiirjaid(sorszám);
                Táblaíró();
                MessageBox.Show("Az adatok rögzítésre kerültek.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Kilépő_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Id.Text, out int sorszám)) throw new HibásBevittAdat("Nincs kitöltve a sorszám, így nem kerül rögzítésre.");
                if (KilépésOka.Text.Trim() == "") KilépésOka.Text = "_";

                AdatokStátus = Kéz_Státus.Lista_Adatok(Cmbtelephely.Text.Trim());
                bool vane = AdatokStátus.Any(adat => adat.ID == sorszám);
                if (vane)
                {
                    Adat_Dolgozó_Státus ADAT = new Adat_Dolgozó_Státus(sorszám, KilépésOka.Text.Trim());
                    Kéz_Státus.Módosít_Kilép_Ok(Cmbtelephely.Text.Trim(), ADAT);

                }
                Kiirjaid(sorszám);
                Táblaíró();

                MessageBox.Show("Az adatok rögzítésre kerültek.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BelépőFull_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Id.Text, out int sorszám)) throw new HibásBevittAdat("Nincs kitöltve a sorszám, így nem kerül rögzítésre.");
                if (!double.TryParse(Bérbe.Text, out double BérBe))
                {
                    BérBe = 0;
                    Bérbe.Text = BérBe.ToString();
                }
                if (Honnanjött.Text.Trim() == "") Honnanjött.Text = "_";
                if (Hrazonosítóbe.Text.Trim() == "") Hrazonosítóbe.Text = "_";
                if (Névbe.Text.Trim() == "") Névbe.Text = "_";
                AdatokStátus = Kéz_Státus.Lista_Adatok(Cmbtelephely.Text.Trim());
                bool vane = AdatokStátus.Any(adat => adat.ID == sorszám);

                if (vane)
                {
                    Adat_Dolgozó_Státus ADAT = new Adat_Dolgozó_Státus(sorszám,
                                                                       BérBe,
                                                                       Belépésidátum.Value,
                                                                       Névbe.Text.Trim(),
                                                                       Hrazonosítóbe.Text.Trim(),
                                                                       Honnanjött.Text.Trim(),
                                                                       Telephelybe.Text.Trim()
                                                                       );
                    Kéz_Státus.Módosít_Be_Teljes(Cmbtelephely.Text.Trim(), ADAT);
                }

                Kiirjaid(sorszám);
                Táblaíró();
                MessageBox.Show("Az adatok rögzítésre kerültek.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Belépő_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Id.Text, out int sorszám)) throw new HibásBevittAdat("Nincs kitöltve a sorszám, így nem kerül rögzítésre.");
                if (Honnanjött.Text.Trim() == "") Honnanjött.Text = "_";

                AdatokStátus = Kéz_Státus.Lista_Adatok(Cmbtelephely.Text.Trim());
                bool vane = AdatokStátus.Any(adat => adat.ID == sorszám);
                if (vane)
                {
                    Adat_Dolgozó_Státus ADAT = new Adat_Dolgozó_Státus(sorszám,
                                                   0,
                                                   new DateTime(1900, 1, 1),
                                                   "",
                                                   "",
                                                   Honnanjött.Text.Trim(),
                                                   ""
                                                   );
                    Kéz_Státus.Módosít_Be_Honnan(Cmbtelephely.Text.Trim(), ADAT);
                }
                Kiirjaid(sorszám);
                Táblaíró();
                MessageBox.Show("Az adatok rögzítésre kerültek.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void StátusMódosítás_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Id.Text, out int sorszám)) throw new HibásBevittAdat("Nincs kitöltve a sorszám, így nem kerül rögzítésre.");
                AdatokStátus = Kéz_Státus.Lista_Adatok(Cmbtelephely.Text.Trim());
                bool vane = AdatokStátus.Any(adat => adat.ID == sorszám);

                if (vane)
                {
                    Adat_Dolgozó_Státus ADAT = new Adat_Dolgozó_Státus(sorszám,
                                                                       "",
                                                                       "",
                                                                       0,
                                                                       "",
                                                                       new DateTime(1900, 1, 1),
                                                                       "",
                                                                       "",
                                                                       "",
                                                                       new DateTime(1900, 1, 1),
                                                                       Státusváltozások.Text);
                    Kéz_Státus.Módosít_Státus(Cmbtelephely.Text.Trim(), ADAT);
                }
                Kiirjaid(sorszám);
                Táblaíró();
                MessageBox.Show("Az adatok rögzítésre kerültek.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void StatusFull_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Id.Text, out int sorszám)) throw new HibásBevittAdat("Nincs kitöltve a sorszám, így nem kerül rögzítésre.");
                AdatokStátus = Kéz_Státus.Lista_Adatok(Cmbtelephely.Text.Trim());
                bool vane = AdatokStátus.Any(adat => adat.ID == sorszám);
                if (vane)
                {
                    Adat_Dolgozó_Státus ADAT = new Adat_Dolgozó_Státus(sorszám,
                                                                       Check1.Checked ? -1 : 0,
                                                                       Státusváltozásoka.Text.Trim(),
                                                                       Megjegyzés.Text.Trim());
                    Kéz_Státus.Módosít_Státus_Teljes(Cmbtelephely.Text.Trim(), ADAT);
                }
                Kiirjaid(sorszám);
                Táblaíró();
                MessageBox.Show("Az adatok rögzítésre kerültek.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Status_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Id.Text, out int sorszám)) throw new HibásBevittAdat("Nincs kitöltve a sorszám, így nem kerül rögzítésre.");
                AdatokStátus = Kéz_Státus.Lista_Adatok(Cmbtelephely.Text.Trim());
                bool vane = AdatokStátus.Any(adat => adat.ID == sorszám);
                if (vane)
                {
                    Adat_Dolgozó_Státus ADAT = new Adat_Dolgozó_Státus(sorszám,
                                                   Check1.Checked ? -1 : 0,
                                                   "",
                                                   Megjegyzés.Text.Trim());
                    Kéz_Státus.Módosít_Státus_Megjegyzés(Cmbtelephely.Text.Trim(), ADAT);

                }
                Kiirjaid(sorszám);
                Táblaíró();
                MessageBox.Show("Az adatok rögzítésre kerültek.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Új_Státus_Click(object sender, EventArgs e)
        {
            try
            {
                Adat_Dolgozó_Státus ADAT = new Adat_Dolgozó_Státus(0,
                                                                   "", "", 0,
                                                                   "_",
                                                                   new DateTime(1900, 1, 1),
                                                                   "_",
                                                                   "_",
                                                                   "_",
                                                                   new DateTime(1900, 1, 1),
                                                                   "Státus létrehozása"
                                                                   );
                long utolsó = Kéz_Státus.Rögzítés_Új(Cmbtelephely.Text.Trim(), ADAT);
                Id.Text = utolsó.ToString();
                Kiirjaid(utolsó);
                Táblaíró();

                MessageBox.Show("Az Új státus létre lett hozva.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Command4_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Id.Text, out int sorszám)) throw new HibásBevittAdat("Nincs kitöltve a sorszám, így nem kerül rögzítésre.");
                AdatokStátus = Kéz_Státus.Lista_Adatok(Cmbtelephely.Text.Trim());
                string eredmény = AdatokStátus
                    .Where(adat => adat.ID == sorszám)
                    .Select(adat => adat.Hrazonosítóbe.Trim())
                    .FirstOrDefault();
                if (eredmény.Any())
                {
                    if (eredmény.Trim() == "" || eredmény.Trim() == "_")
                    {
                        Adat_Dolgozó_Státus ADAT = new Adat_Dolgozó_Státus(sorszám,
                                                   "",
                                                   "",
                                                   0,
                                                   "",
                                                   new DateTime(1900, 1, 1),
                                                   "",
                                                   "",
                                                   "",
                                                   new DateTime(1900, 1, 1),
                                                   "Státus megszüntetése");
                        Kéz_Státus.Módosít_Státus(Cmbtelephely.Text.Trim(), ADAT);
                        MessageBox.Show("A státus megszüntetésre került.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("A státust nem lehet megszüntetni miután fel lett töltve.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                Kiirjaid(sorszám);
                Táblaíró();
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

        private void Státusváltozásokfeltöltése()
        {
            Státusváltozások.Items.Clear();
            foreach (MyEn.Dolgozó_Státusz elem in Enum.GetValues(typeof(MyEn.Dolgozó_Státusz)))
                Státusváltozások.Items.Add(elem.ToString().Replace('_', ' '));
        }

        private void Áthelyez_Click(object sender, EventArgs e)
        {
            try
            {
                if (!long.TryParse(Id.Text, out long sorszám)) throw new HibásBevittAdat("Nincs kitöltve a sorszám, így nem lehet áthelyezni az adatokat.");
                if (!long.TryParse(Új_Sorszám.Text, out long Új_sorszám)) throw new HibásBevittAdat("Nincs kitöltve az új sorszám, így nem lehet áthelyezni az adatokat.");
                List<Adat_Dolgozó_Státus> Adatok = Kéz_Státus.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Dolgozó_Státus Adat = (from a in Adatok
                                            where a.ID == Új_sorszám
                                            select a).FirstOrDefault();

                if (Adat != null)
                {
                    if ((Adat.Hrazonosítóbe.Trim() == "" || Adat.Hrazonosítóbe.Trim() == "_") && (Adat.Honnanjött != "" || Adat.Honnanjött != "_"))
                    {
                        //Beolvassuk a másolandót
                        Adat = (from a in Adatok
                                where a.ID == sorszám
                                select a).FirstOrDefault();
                        Adat_Dolgozó_Státus ADAT = new Adat_Dolgozó_Státus(Új_sorszám,
                                                                           Adat.Bérbe,
                                                                           Adat.Belépésidátum,
                                                                           Adat.Névbe,
                                                                           Adat.Hrazonosítóbe,
                                                                           Adat.Honnanjött,
                                                                           Adat.Telephelybe);
                        Kéz_Státus.Módosít_Be_Teljes(Cmbtelephely.Text.Trim(), ADAT);
                        ADAT = new Adat_Dolgozó_Státus(sorszám,
                                                       0,
                                                       new DateTime(1900, 1, 1),
                                                       "_",
                                                       "_",
                                                       "_",
                                                       "_");
                        Kéz_Státus.Módosít_Be_Teljes(Cmbtelephely.Text.Trim(), ADAT);
                        MessageBox.Show("Az adatok rögzítésre kerültek.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Kiirjaid(sorszám);
                        Táblaíró();
                    }
                    else
                        throw new HibásBevittAdat("A megadott sorszám már tartalmaz adatokat, így nem lehet áthelyezni az adatokat.");
                }
                else
                    throw new HibásBevittAdat("Nincs ilyen sorszám, így nem lehet áthelyezni az adatokat.");
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


        #region Bérfrissítés
        private void Command9_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Text2.Text.Trim() == "") || (Text4.Text.Trim() == "") || (Text5.Text.Trim() == "")) throw new HibásBevittAdat("A beolvasási elrendezés nincs megadva.");
                if (!int.TryParse(Text2.Text, out int hrsor)) throw new HibásBevittAdat("A kezdősorszám nem szám.");

                string hroszlop = Text4.Text.Trim();
                string béroszlop = Text5.Text.Trim();

                string fájlexc;
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Bér adatok betölétése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;


                // megnyitjuk az excel táblát
                MyE.ExcelMegnyitás(fájlexc);

                //Új

                List<Adat_Kulcs> AdatokKulcs = KézKulcsPénz.Lista_Adatok();
                List<Adat_Kulcs> AdatokKulcsMód = new List<Adat_Kulcs>();
                List<Adat_Kulcs> AdatokKulcsRögz = new List<Adat_Kulcs>();
                while (MyE.Beolvas(hroszlop + hrsor.ToString()).Trim() != "_")
                {
                    string rekord = $"{MyF.Rövidkód(MyE.Beolvas(hroszlop + hrsor.ToString()))}";

                    bool vane = (from a in AdatokKulcs
                                 where a.Adat1.Contains(rekord)
                                 select a).Any();

                    Adat_Kulcs ADAT = new Adat_Kulcs(MyF.Kódol(MyE.Beolvas($"{hroszlop}{hrsor}")), MyF.Kódol(MyE.Beolvas($"{béroszlop}{hrsor}")));
                    if (vane)
                        AdatokKulcsMód.Add(ADAT);
                    else
                        AdatokKulcsRögz.Add(ADAT);

                    hrsor += 1;
                }
                KézKulcsPénz.Rögzít(AdatokKulcsRögz);
                KézKulcsPénz.Módosít(AdatokKulcsMód);

                // az excel tábla bezárása
                MyE.ExcelBezárás();
                File.Delete(fájlexc);
                MessageBox.Show("Az adatok rögzítésre kerültek.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Exceltábla
        private void Command5_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();
                // kimeneti fájl helye és neve
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Felépítés Státus lekérdezés",
                    FileName = "Felépítés_Státus_" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = MyF.Szöveg_Tisztítás(fájlexc, 0, -1);

                // létrehozzuk az excel táblát
                MyE.ExcelLétrehozás();
                MyE.Munkalap_betű("arial", 12);

                // ****************************************************
                // elkészítjük a lapokat
                // ****************************************************
                MyE.Munkalap_átnevezés("Munka1", "Státus");

                for (int i = 0; i < Cmbtelephely.Items.Count; i++)
                    MyE.Új_munkalap(Cmbtelephely.Items[i].ToString());

                MyE.Új_munkalap("Összesítő");
                MyE.Új_munkalap("Adatok");

                Adatok_Személyes = Kezelő_Személyes.Lista_Adatok();
                AdatokPénz = KézKulcsPénz.Lista_Adatok();

                for (int ii = 0; ii < Cmbtelephely.Items.Count; ii++)
                {
                    string helytelep = $@"{Application.StartupPath}\{Cmbtelephely.Items[ii]}\Adatok\Dolgozók.mdb";
                    string helyvált = $@"{Application.StartupPath}\{Cmbtelephely.Items[ii]}\adatok\segéd\kiegészítő.mdb";
                    List<Adat_Kiegészítő_Csoportbeosztás> Segéd = KézSegéd.Lista_Adatok(Cmbtelephely.Items[ii].ToStrTrim());
                    if (File.Exists(helyvált) && File.Exists(helytelep))
                    {
                        // leellenőrizzük, hogy minden munkahely ki van-e töltve.
                        Munkahelyellenőrzés(Cmbtelephely.Items[ii].ToStrTrim());
                        List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Items[ii].ToStrTrim());
                        List<Adat_Dolgozó_Telephely> AdatokTelep = new List<Adat_Dolgozó_Telephely>();
                        Cmbtelephely.Text = Cmbtelephely.Items[ii].ToString();
                        foreach (Adat_Dolgozó_Alap Elem in Adatok)
                        {
                            Adat_Dolgozó_Telephely EgyAdat = new Adat_Dolgozó_Telephely(Elem, Cmbtelephely.Items[ii].ToString());
                            AdatokTelep.Add(EgyAdat);
                        }

                        AdatokListázásaMunkalapra(Cmbtelephely.Items[ii].ToString(), AdatokTelep, Segéd);
                        AdatokDolgozók.AddRange(AdatokTelep);
                        AdatokSegéd.AddRange(Segéd);
                    }
                }

                AdatokDolgozók = AdatokDolgozók.OrderBy(a => a.Dolgozó.DolgozóNév).ToList();
                AdatokListázásaMunkalapra("Adatok", AdatokDolgozók, AdatokSegéd);
                ÖsszesítőMunkalap();
                // számoljuk a státus tábla adatait
                // ++++++++++++++++++++++++++++++
                // Üres státusok
                // ++++++++++++++++++++++++++++++
                SzűrtLista.Checked = false;
                Cmbtelephely.Text = Program.PostásTelephely;
                List<Adat_Dolgozó_Státus> AdatokStátus = Kéz_Státus.Lista_Adatok(Cmbtelephely.Text.Trim());

                int Összeg = 0;
                int oszlop = 2;
                while (MyE.Beolvas(MyE.Oszlopnév(oszlop) + "11") != "Összesen:")
                {
                    List<Adat_Dolgozó_Státus> Eredmény = (from a in AdatokStátus
                                                          where a.Telephelyki == MyE.Beolvas(MyE.Oszlopnév(oszlop) + "11").Trim()
                                                          && a.Hrazonosítóbe == "_"
                                                          && a.Névbe == "_"
                                                          && a.Honnanjött == "_"
                                                          && a.Státusváltozások == "Személy csere"
                                                          select a).ToList();
                    if (Eredmény != null)
                        Összeg = Eredmény.Count;

                    Eredmény.Clear();
                    Eredmény = (from a in AdatokStátus
                                where a.Telephelybe == MyE.Beolvas(MyE.Oszlopnév(oszlop) + "11").Trim()
                                && a.Hrazonosítóbe == "_"
                                && a.Honnanjött == "_"
                                && a.Státusváltozások == "Státus létrehozása"
                                select a).ToList();
                    if (Eredmény != null)
                        Összeg += Eredmény.Count;

                    MyE.Kiir(Összeg.ToString(), MyE.Oszlopnév(oszlop) + "16");
                    oszlop += 1;
                }

                //'++++++++++++++++++++++++++++++
                // ' felvétel folyamatban
                // '++++++++++++++++++++++++++++++
                oszlop = 2;
                Összeg = 0;
                while (MyE.Beolvas(MyE.Oszlopnév(oszlop) + "11") != "Összesen:")
                {
                    List<Adat_Dolgozó_Státus> Eredmény = (from a in AdatokStátus
                                                          where a.Telephelybe == MyE.Beolvas(MyE.Oszlopnév(oszlop) + "11").Trim()
                                                          && a.Hrazonosítóbe == "_"
                                                          && a.Névbe != "_"
                                                          && a.Honnanjött != "_"
                                                          select a).ToList();
                    if (Eredmény != null)
                        Összeg = Eredmény.Count;

                    Eredmény.Clear();
                    Eredmény = (from a in AdatokStátus
                                where a.Belépésidátum > DateTime.Today
                                && a.Telephelybe == MyE.Beolvas(MyE.Oszlopnév(oszlop) + "11").Trim()
                                select a).ToList();

                    if (Eredmény != null)
                        Összeg += Eredmény.Count;

                    MyE.Kiir(Összeg.ToString(), MyE.Oszlopnév(oszlop) + "17");
                    oszlop += 1;
                }

                // ++++++++++++++++++++++++++++++
                // ' Előzetesen kilépett
                // '++++++++++++++++++++++++++++++
                oszlop = 2;
                Összeg = 0;
                while (MyE.Beolvas(MyE.Oszlopnév(oszlop) + "11") != "Összesen:")
                {
                    List<Adat_Dolgozó_Státus> Eredmény = (from a in AdatokStátus
                                                          where a.Telephelyki == MyE.Beolvas(MyE.Oszlopnév(oszlop) + "11").Trim()
                                                          && a.Kilépésdátum > DateTime.Today
                                                          select a).ToList();
                    if (Eredmény != null)
                        Összeg = Eredmény.Count;
                    MyE.Kiir(Összeg.ToString(), MyE.Oszlopnév(oszlop) + "15");
                    oszlop += 1;
                }
                string munkalap = "Státus";
                MyE.Aktív_Cella(munkalap, "A1");

                // ***************************
                // Státus tábla
                // ***************************
                StátusMunkaLap();

                Holtart.Ki();
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();
                MyE.Megnyitás(fájlexc);
                Cmbtelephely.Text = Program.PostásTelephely;
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

        private void ÖsszesítőMunkalap()
        {
            try
            {
                string munkalap = "Összesítő";
                MyE.Munkalap_aktív(munkalap);
                MyE.Rácsoz("a1:a8");
                MyE.Vastagkeret("a1:a8");
                MyE.Rácsoz("a11:a18");
                MyE.Vastagkeret("a11:a18");
                MyE.Kiir("Szellemi", "a2");
                MyE.Kiir("Szellemi", "a12");
                MyE.Kiir("Fizikai", "a3");
                MyE.Kiir("Fizikai", "a13");
                MyE.Kiir("Összesen", "a4");
                MyE.Kiir("Összesen", "a18");
                MyE.Betű("a4", false, false, true);
                MyE.Betű("a18", false, false, true);
                MyE.Kiir("Vezényelve", "a5");
                MyE.Kiir("Vezényelt", "a6");
                MyE.Kiir("részmunkaidős", "a7");
                MyE.Kiir("Passzív", "a8");
                MyE.Kiir("Passzív", "a14");
                MyE.Kiir("Előzetesen kilépetett", "a15");
                MyE.Kiir("Üres Státus", "a16");
                MyE.Kiir("Felvétel Folyamatban", "a17");
                MyE.Oszlopszélesség(munkalap, "A:A");

                // összesítő oszlop
                MyE.Kiir("Összesen:", MyE.Oszlopnév(öoszlop) + "1");
                MyE.Kiir("Összesen:", MyE.Oszlopnév(öoszlop) + "11");
                for (int isor = 2; isor <= 8; isor++)
                    MyE.Kiir("=SUM(RC[-" + (öoszlop - 2).ToString() + "]:RC[-1])", MyE.Oszlopnév(öoszlop) + isor.ToString());
                for (int isor = 12; isor <= 18; isor++)
                    MyE.Kiir("=SUM(RC[-" + (öoszlop - 2).ToString() + "]:RC[-1])", MyE.Oszlopnév(öoszlop) + isor.ToString());
                int oszlop = 2;
                while (MyE.Beolvas(MyE.Oszlopnév(oszlop) + "11") != "Összesen:")
                {
                    MyE.Kiir("=SUM(R[-6]C:R[-1]C)", MyE.Oszlopnév(oszlop) + "18");
                    oszlop += 1;
                }
                MyE.Betű(MyE.Oszlopnév(öoszlop) + "4", false, false, true);
                MyE.Betű(MyE.Oszlopnév(öoszlop) + "18", false, false, true);
                MyE.Rácsoz(MyE.Oszlopnév(öoszlop) + "1:" + MyE.Oszlopnév(öoszlop) + "8");
                MyE.Vastagkeret(MyE.Oszlopnév(öoszlop) + "1:" + MyE.Oszlopnév(öoszlop) + "8");
                MyE.Rácsoz(MyE.Oszlopnév(öoszlop) + "11:" + MyE.Oszlopnév(öoszlop) + "18");
                MyE.Vastagkeret(MyE.Oszlopnév(öoszlop) + "11:" + MyE.Oszlopnév(öoszlop) + "18");
                MyE.Oszlopszélesség(munkalap, MyE.Oszlopnév(öoszlop) + ":" + MyE.Oszlopnév(öoszlop));

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

        private void StátusMunkaLap()
        {
            try
            {
                string munkalap = "Státus";
                MyE.Munkalap_aktív(munkalap);
                MyE.EXCELtábla(munkalap, "", Tábla, true);

                // oszlopszélességek
                MyE.Oszlopszélesség(munkalap, "a:a", 5);
                MyE.Oszlopszélesség(munkalap, "b:b", 17);
                MyE.Oszlopszélesség(munkalap, "c:c", 8);
                MyE.Oszlopszélesség(munkalap, "d:d", 8);
                MyE.Oszlopszélesség(munkalap, "e:e", 14);
                MyE.Oszlopszélesség(munkalap, "f:f", 59);
                MyE.Oszlopszélesség(munkalap, "g:g", 13);
                MyE.Oszlopszélesség(munkalap, "h:h", 17);
                MyE.Oszlopszélesség(munkalap, "i:i", 8);
                MyE.Oszlopszélesség(munkalap, "j:j", 8);
                MyE.Oszlopszélesség(munkalap, "k:k", 18);
                MyE.Oszlopszélesség(munkalap, "l:l", 14);
                MyE.Oszlopszélesség(munkalap, "m:m", 13);
                MyE.Oszlopszélesség(munkalap, "n:n", 22);
                MyE.Oszlopszélesség(munkalap, "o:o", 36);
                MyE.Oszlopszélesség(munkalap, "p:p", 36);


                MyE.Háttérszín("a:a", 11851260);
                MyE.Háttérszín("b:g", 13421823);
                MyE.Háttérszín("h:m", 10092441);
                MyE.Háttérszín("n:r", 13434879);

                // megszűnő státus szürkítése
                for (int sor = 0; sor < Tábla.Rows.Count; sor++)
                {
                    if (MyE.Beolvas($"N{sor + 1}").Trim() == "Státus megszüntetése")
                        MyE.Háttérszín($"H{sor + 1}:N{sor + 1}", 9868950);
                }
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

        private void AdatokListázásaMunkalapra(string munkalap, List<Adat_Dolgozó_Telephely> AdatokKapott, List<Adat_Kiegészítő_Csoportbeosztás> Segéd)
        {
            try
            {
                List<Adat_Kulcs> AdatokKulcs = KézKulcs.Lista_Adatok();
                bool kulcsfájlvan = AdatokKulcs.Count > 0;

                DateTime kilépésidátum;
                int fizikai = 0;
                int alkalmazott = 0;
                int Vezényelt = 0;
                int Vezényelve = 0;
                int Részmunkaidős = 0;
                int passzív = 0;
                bool személyeseng = false;
                bool béreng = false;
                if (kulcsfájlvan)
                {
                    string adat1 = Program.PostásNév.Trim().ToUpper();
                    string adat2 = Program.PostásTelephely.Trim().ToUpper();
                    string adat3 = "A";
                    személyeseng = KézKulcs.ABKULCSvan(adat1, adat2, adat3);

                    adat3 = "B";
                    béreng = KézKulcs.ABKULCSvan(adat1, adat2, adat3);
                }

                MyE.Munkalap_aktív(munkalap);

                // elkészítjük a fejlécet
                MyE.Kiir("Sorszám", "a1");
                MyE.Kiir("Név", "b1");
                MyE.Kiir("Munkakör", "c1");
                MyE.Kiir("HR törzsszám", "d1");
                MyE.Kiir("Születési idő", "e1");
                MyE.Kiir("Belépési idő", "f1");
                MyE.Kiir("Bér", "g1");
                MyE.Kiir("Csoport", "h1");
                MyE.Kiir("Passzív", "i1");
                MyE.Kiir("Alkalmazott/fizikai", "j1");
                MyE.Kiir("Ide vezényelt", "k1");
                MyE.Kiir("Elvezényelve", "l1");
                MyE.Kiir("Részmunkaidős", "m1");
                MyE.Kiir("Szervezetiegység", "n1");
                // lenullázzuk
                fizikai = 0;
                alkalmazott = 0;
                Vezényelt = 0;
                Vezényelve = 0;
                Részmunkaidős = 0;
                passzív = 0;

                int i = 2;
                Holtart.Lép();
                foreach (Adat_Kiegészítő_Csoportbeosztás csoportnév in Segéd)
                {
                    Holtart.Lép();


                    List<Adat_Dolgozó_Telephely> Dolgozók = (from a in AdatokKapott
                                                             where a.Dolgozó.Csoport == csoportnév.Csoportbeosztás
                                                             orderby a.Dolgozó.DolgozóNév
                                                             select a).ToList();

                    foreach (Adat_Dolgozó_Telephely rekord in Dolgozók)
                    {

                        if (rekord.Dolgozó.Kilépésiidő != null)
                            kilépésidátum = rekord.Dolgozó.Kilépésiidő;
                        else
                            kilépésidátum = new DateTime(1900, 1, 1);

                        if (kilépésidátum < new DateTime(2000, 1, 1))
                        {
                            MyE.Kiir((i - 1d).ToString(), $"a{i}");
                            MyE.Kiir(rekord.Dolgozó.DolgozóNév.Trim(), $"b{i}");
                            if (rekord.Dolgozó.Munkakör.Trim() != null)
                                MyE.Kiir(rekord.Dolgozó.Munkakör.Trim(), $"c{i}");
                            MyE.Kiir(rekord.Dolgozó.Dolgozószám.Trim(), $"d{i}");

                            if (személyeseng)
                            {
                                DateTime ideigdátum = (from a in Adatok_Személyes
                                                       where a.Dolgozószám == rekord.Dolgozó.Dolgozószám
                                                       select a.Születésiidő).FirstOrDefault();
                                if (ideigdátum != null) MyE.Kiir(ideigdátum.ToString("yyyy.MM.dd"), "e" + i);
                            }


                            if (rekord.Dolgozó.Belépésiidő != null)
                                MyE.Kiir(rekord.Dolgozó.Belépésiidő.ToString(), $"F{i}");

                            if (béreng)
                            {
                                string ideig = MyF.Rövidkód(rekord.Dolgozó.Dolgozószám.Trim());
                                ideig = (from adat in AdatokPénz
                                         where adat.Adat1.Contains(ideig)
                                         select adat.Adat2).FirstOrDefault();
                                if (ideig != null && ideig != "_")
                                    MyE.Kiir(MyF.Dekódolja(ideig), $"G{i}");
                            }

                            if (rekord.Dolgozó.Csoport.Trim() != null)
                                MyE.Kiir(rekord.Dolgozó.Csoport.Trim(), $"H{i}");
                            if (rekord.Dolgozó.Passzív)
                            {
                                MyE.Kiir("passzív", $"I{i}");
                                passzív += 1;
                            }

                            if (rekord.Dolgozó.Alkalmazott)
                            {
                                MyE.Kiir("Alkalmazott", $"J{i}");
                                alkalmazott += 1;
                            }
                            else
                            {
                                MyE.Kiir("Fizikai", $"J{i}");
                                fizikai += 1;
                            }

                            if (rekord.Dolgozó.Vezényelt)
                            {
                                MyE.Kiir("vezényelt", $"K{i}");
                                Vezényelt += 1;
                            }
                            if (rekord.Dolgozó.Vezényelve)
                            {
                                MyE.Kiir("vezényelve", $"L{i}");
                                Vezényelve += 1;
                            }
                            if (rekord.Dolgozó.Részmunkaidős)
                            {
                                MyE.Kiir("részmunkaidős", $"M{i}");
                                Részmunkaidős += 1;
                            }
                            MyE.Kiir(rekord.Telephely, $"n{i}");
                            i += 1;
                        }
                    }
                }

                MyE.Szűrés(munkalap, "A:N", 1);
                MyE.Oszlopszélesség(munkalap, "A:N");

                MyE.Rácsoz($"A1:N{i}");
                MyE.Vastagkeret($"A1:N{i}");

                i += 1;
                MyE.Kiir("Szellemi", $"b{i}");
                MyE.Kiir(alkalmazott.ToString(), $"c{i}");

                MyE.Kiir("Fizikai", $"b{(i + 1)}");
                MyE.Kiir(fizikai.ToString(), $"c{i + 1}");

                MyE.Kiir("Összesen", $"b{i + 2}");
                MyE.Kiir((fizikai + alkalmazott).ToString(), $"c{i + 2}");

                MyE.Kiir("Vezényelve", $"b{i + 3} ");
                MyE.Kiir(Vezényelve.ToString(), $"c{i + 3}");

                MyE.Kiir("vezényelt", $"b{i + 4}");
                MyE.Kiir(Vezényelt.ToString(), $"c{i + 4}");

                MyE.Kiir("részmunkaidős", $"b{i + 5}");
                MyE.Kiir(Részmunkaidős.ToString(), $"c{i + 5}");

                MyE.Kiir("Passzív", $"b{i + 6}");
                MyE.Kiir(passzív.ToString(), $"c{i + 6}");

                MyE.Rácsoz($"b{i}:c{i + 6}");
                MyE.Vastagkeret($"b{i}:c{i + 6}");

                // ------------------------------------------
                // összesítő lapra kiírjuk telephelyenként
                // ------------------------------------------
                if (munkalap != "Adatok")
                {
                    MyE.Munkalap_aktív("Összesítő");

                    MyE.Kiir(munkalap, MyE.Oszlopnév(öoszlop) + "1");
                    MyE.Kiir(munkalap, MyE.Oszlopnév(öoszlop) + "11");
                    MyE.Kiir(alkalmazott.ToString(), MyE.Oszlopnév(öoszlop) + "2");
                    MyE.Kiir(alkalmazott.ToString(), MyE.Oszlopnév(öoszlop) + "12");
                    MyE.Kiir(fizikai.ToString(), MyE.Oszlopnév(öoszlop) + "3");
                    MyE.Kiir(fizikai.ToString(), MyE.Oszlopnév(öoszlop) + "13");
                    MyE.Kiir((fizikai + alkalmazott).ToString(), MyE.Oszlopnév(öoszlop) + "4");
                    MyE.Betű(MyE.Oszlopnév(öoszlop) + "4", false, false, true);
                    MyE.Kiir(Vezényelve.ToString(), MyE.Oszlopnév(öoszlop) + "5");
                    MyE.Kiir(Vezényelt.ToString(), MyE.Oszlopnév(öoszlop) + "6");
                    MyE.Kiir(Részmunkaidős.ToString(), MyE.Oszlopnév(öoszlop) + "7");
                    MyE.Kiir(passzív.ToString(), MyE.Oszlopnév(öoszlop) + "8");
                    MyE.Kiir(passzív.ToString(), MyE.Oszlopnév(öoszlop) + "14");
                    MyE.Rácsoz(MyE.Oszlopnév(öoszlop) + "1:" + MyE.Oszlopnév(öoszlop) + "8");
                    MyE.Vastagkeret(MyE.Oszlopnév(öoszlop) + "1:" + MyE.Oszlopnév(öoszlop) + "8");
                    MyE.Rácsoz(MyE.Oszlopnév(öoszlop) + "11:" + MyE.Oszlopnév(öoszlop) + "18");
                    MyE.Vastagkeret(MyE.Oszlopnév(öoszlop) + "11:" + MyE.Oszlopnév(öoszlop) + "18");
                    MyE.Oszlopszélesség("Összesítő", MyE.Oszlopnév(öoszlop) + ":" + MyE.Oszlopnév(öoszlop));
                    öoszlop += 1;
                    MyE.Aktív_Cella("Összesítő", "A1");
                }
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

        private void Munkahelyellenőrzés(string Telephely)
        {
            List<Adat_Dolgozó_Alap> Dolgozók = KézDolgozó.Lista_Adatok(Telephely);
            if (Dolgozók != null)
            {
                List<string> Adatok = new List<string>();
                foreach (Adat_Dolgozó_Alap rekord in Dolgozók)
                {
                    if (rekord.Csoport == null)
                        Adatok.Add(rekord.Dolgozószám);
                }
                if (Adatok != null && Adatok.Count > 0) KézDolgozó.Módosít_Csoport(Telephely, Adatok);
            }
        }
        #endregion
    }
}