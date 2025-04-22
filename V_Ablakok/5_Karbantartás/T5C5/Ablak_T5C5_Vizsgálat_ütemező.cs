using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Ablakok;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyO = Microsoft.Office.Interop.Outlook;

namespace Villamos
{

    public partial class Ablak_T5C5_Vizsgálat_ütemező
    {
        string AlsóPanel1;
        Ablak_Kereső Új_Ablak_Kereső;
        Ablak_T5C5_Segéd Új_Ablak_T5C5_Segéd;
#pragma warning disable IDE0044 // Add readonly modifier
        //      List<Adat_Általános_String_Dátum> Frissítés = new List<Adat_Általános_String_Dátum>();
        List<Adat_T5C5_Posta> Posta_lista = new List<Adat_T5C5_Posta>();
#pragma warning restore IDE0044 // Add readonly modifier

        readonly Kezelő_Szerelvény KézSzer = new Kezelő_Szerelvény();
        readonly Kezelő_Nap_Hiba KézHiba = new Kezelő_Nap_Hiba();
        readonly Kezelő_Főkönyv_Zser_Km KézZser = new Kezelő_Főkönyv_Zser_Km();
        readonly Kezelő_T5C5_Kmadatok KézVkm = new Kezelő_T5C5_Kmadatok("T5C5");
        readonly Kezelő_Osztály_Adat KézCsat = new Kezelő_Osztály_Adat();
        readonly Kezelő_T5C5_Göngyöl KézFutás = new Kezelő_T5C5_Göngyöl();
        readonly Kezelő_Hétvége_Előírás KézElőírás = new Kezelő_Hétvége_Előírás();
        readonly Kezelő_Kerék_Tábla KézKerék = new Kezelő_Kerék_Tábla();
        readonly Kezelő_Kerék_Mérés Mérés_kéz = new Kezelő_Kerék_Mérés();
        readonly Kezelő_Vezénylés KézVezény = new Kezelő_Vezénylés();
        readonly Kezelő_Hétvége_Beosztás KézHBeosztás = new Kezelő_Hétvége_Beosztás();
        readonly Kezelő_Kiegészítő_Felmentés KézFelmentés = new Kezelő_Kiegészítő_Felmentés();
        readonly Kezelő_Utasítás KézUtasítás = new Kezelő_Utasítás();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();

        List<Adat_Szerelvény> AdatokSzer = new List<Adat_Szerelvény>();
        List<Adat_Szerelvény> AdatokSzerelvényElő = new List<Adat_Szerelvény>();
        List<Adat_Nap_Hiba> AdatokHiba = new List<Adat_Nap_Hiba>();
        List<Adat_Főkönyv_Zser_Km> AdatokZSER = new List<Adat_Főkönyv_Zser_Km>();
        List<Adat_T5C5_Kmadatok> AdatokVkm = new List<Adat_T5C5_Kmadatok>();
        List<Adat_Osztály_Adat> AdatokCsatoló = new List<Adat_Osztály_Adat>();
        List<Adat_T5C5_Göngyöl> AdatokFutás = new List<Adat_T5C5_Göngyöl>();
        List<Adat_Hétvége_Beosztás> AdatokHBeosztás = new List<Adat_Hétvége_Beosztás>();
        List<Adat_Hétvége_Előírás> AdatokElőírás = new List<Adat_Hétvége_Előírás>();
        List<Adat_Kerék_Mérés> Mérés_Adatok = new List<Adat_Kerék_Mérés>();
        List<Adat_Vezénylés> AdatokVezény = new List<Adat_Vezénylés>();

        // alapkijelölés
        int Alap_red;
        int Alap_green;
        int Alap_blue;
        int Vál_red;
        int Vál_green;
        int Vál_blue;
        bool Terv = false;
        // Számításhoz
        long KorNapikm = 0;
        long VUtánFutot = 0;
        long ElőzőVtől = 0;
        long ElőzőV2V3 = 0;

        #region Alap
        public Ablak_T5C5_Vizsgálat_ütemező()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Telephelyekfeltöltése();
            Jogosultságkiosztás();
        }

        private void Ablak_Vizsgálat_ütemező_Load(object sender, EventArgs e)
        {
            Fülek.SelectedIndex = 0;
            Fülekkitöltése();
            Vonal_tábla_író();
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        private void Ablak_T5C5_Vizsgálat_ütemező_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kereső?.Close();
            Új_Ablak_T5C5_Segéd?.Close();
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.PostásTelephely.Contains("törzs"))
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

                Command10_Listát_töröl.Enabled = false;
                Vonal_fel.Enabled = false;
                Command7_Rögzítés.Enabled = false;

                Btnrögzítés.Enabled = false;

                Email.Enabled = false;
                Ciklus_Mentés.Enabled = false;

                melyikelem = 103;
                // módosítás 1 Dolgozók ki és beléptetése

                if (MyF.Vanjoga(melyikelem, 1))
                {
                }
                // módosítás 2 Állományba vétel

                if (MyF.Vanjoga(melyikelem, 1))
                {
                }
                // módosítás 3 Vezénylés

                if (MyF.Vanjoga(melyikelem, 1))
                {
                }
                melyikelem = 104;
                // módosítás 1

                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Command10_Listát_töröl.Enabled = true;
                    Vonal_fel.Enabled = true;
                    Command7_Rögzítés.Enabled = true;
                }
                // módosítás 2 

                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Email.Enabled = true;
                    Ciklus_Mentés.Enabled = true;
                }

                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                }

                melyikelem = 105;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Btnrögzítés.Enabled = true;
                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                }
                // módosítás 3

                if (MyF.Vanjoga(melyikelem, 1))
                {
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
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\V2Vizsgálat.html";
                Module_Excel.Megnyitás(hely);
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

        private void Fülekkitöltése()
        {
            switch (Fülek.SelectedIndex)
            {
                case 0:
                    {
                        break;
                    }

                case 1:
                    {
                        Vonal_tábla_író();
                        break;
                    }

                case 2:
                    {
                        Utasítás_Írás();
                        break;
                    }
                case 3:
                    {
                        Felmentés_kiírás();
                        break;
                    }
            }
        }

        private void Ablak_Vizsgálat_ütemező_KeyDown(object sender, KeyEventArgs e)
        {

            // ESC
            if ((int)e.KeyCode == 27)
            {
                Új_Ablak_Kereső?.Close();

                Új_Ablak_T5C5_Segéd?.Close();
            }
            //ctrl+F
            if (e.Control && e.KeyCode == Keys.F)
            {
                Keresés_metódus();
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


        #region Vonalak lapfül
        private void Command9_színkereső_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog ColorDialog1 = new ColorDialog();
                if (ColorDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    Vonal_red.BackColor = ColorDialog1.Color;
                    Vonal_green.BackColor = ColorDialog1.Color;
                    Vonal_blue.BackColor = ColorDialog1.Color;

                    Vonal_red.Text = ColorDialog1.Color.R.ToString();
                    Vonal_green.Text = ColorDialog1.Color.G.ToString();
                    Vonal_blue.Text = ColorDialog1.Color.B.ToString();
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

        private void Command8_Új_Click(object sender, EventArgs e)
        {
            Vonal_kiürít();
        }

        private void Command7_Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Vonal_Vonal.Text.Trim() == "") throw new HibásBevittAdat("A Vonal beviteli mező nem lehet üres.");
                if (Vonal_Mennyiség.Text.Trim() == "") throw new HibásBevittAdat("A Mennyiség beviteli mező nem lehet üres.");
                if (Vonal_red.Text.Trim() == "") throw new HibásBevittAdat("A Szín beviteli mező nem lehet üres.");
                if (Vonal_green.Text.Trim() == "") throw new HibásBevittAdat("A Szín beviteli mező nem lehet üres.");
                if (Vonal_blue.Text.Trim() == "") throw new HibásBevittAdat("A Szín beviteli mező nem lehet üres.");
                if (!int.TryParse(Vonal_red.Text.Trim(), out int Red)) Red = 0;
                if (!int.TryParse(Vonal_green.Text.Trim(), out int Green)) Green = 0;
                if (!int.TryParse(Vonal_blue.Text.Trim(), out int Blue)) Blue = 0;
                if (!long.TryParse(Vonal_Mennyiség.Text.Trim(), out long Mennyiség)) Mennyiség = 0;
                if (!long.TryParse(Vonal_Id.Text.Trim(), out long Id)) Id = 0;


                Vonal_Vonal.Text = MyF.Szöveg_Tisztítás(Vonal_Vonal.Text, 0, 20);

                AdatokElőírás = KézElőírás.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Hétvége_Előírás ElőírásElem = (from a in AdatokElőírás
                                                    where a.Id == Id
                                                    select a).FirstOrDefault();

                Adat_Hétvége_Előírás ADAT = new Adat_Hétvége_Előírás(
                    Id,
                    Vonal_Vonal.Text.Trim(),
                    Mennyiség,
                    Red,
                    Green,
                    Blue);

                if (ElőírásElem != null)
                    KézElőírás.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                else
                    KézElőírás.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);

                Vonal_tábla_író();
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

        private void Command11_frissít_Click(object sender, EventArgs e)
        {
            Vonal_tábla_író();
        }

        private void Vonal_tábla_író()
        {
            try
            {
                AdatokElőírás = KézElőírás.Lista_Adatok(Cmbtelephely.Text.Trim());

                Vonal_tábla.Rows.Clear();
                Vonal_tábla.Columns.Clear();
                Vonal_tábla.Refresh();
                Vonal_tábla.Visible = false;
                Vonal_tábla.ColumnCount = 6;

                // fejléc elkészítése
                Vonal_tábla.Columns[0].HeaderText = "Sorszám";
                Vonal_tábla.Columns[0].Width = 100;
                Vonal_tábla.Columns[1].HeaderText = "Vonal";
                Vonal_tábla.Columns[1].Width = 200;
                Vonal_tábla.Columns[2].HeaderText = "Mennyiség";
                Vonal_tábla.Columns[2].Width = 200;
                Vonal_tábla.Columns[3].HeaderText = "Piros";
                Vonal_tábla.Columns[3].Width = 100;
                Vonal_tábla.Columns[4].HeaderText = "Zöld";
                Vonal_tábla.Columns[4].Width = 100;
                Vonal_tábla.Columns[5].HeaderText = "Kék";
                Vonal_tábla.Columns[5].Width = 100;

                foreach (Adat_Hétvége_Előírás rekord in AdatokElőírás)
                {
                    Vonal_tábla.RowCount++;
                    int i = Vonal_tábla.RowCount - 1;

                    Vonal_tábla.Rows[i].Cells[0].Value = rekord.Id;
                    Vonal_tábla.Rows[i].Cells[1].Value = rekord.Vonal;
                    Vonal_tábla.Rows[i].Cells[2].Value = rekord.Mennyiség;
                    Vonal_tábla.Rows[i].Cells[3].Value = rekord.Red;
                    Vonal_tábla.Rows[i].Cells[4].Value = rekord.Green;
                    Vonal_tábla.Rows[i].Cells[5].Value = rekord.Blue;
                    Vonal_tábla.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(rekord.Red, rekord.Green, rekord.Blue);
                    Vonal_tábla.Rows[i].Cells[4].Style.BackColor = Color.FromArgb(rekord.Red, rekord.Green, rekord.Blue);
                    Vonal_tábla.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(rekord.Red, rekord.Green, rekord.Blue);
                }

                Vonal_tábla.Visible = true;
                Vonal_tábla.Refresh();

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

        private void Command10_Listát_töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Vonal_Id.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelöve a törlendő tétel");
                if (!long.TryParse(Vonal_Id.Text, out long Id)) throw new HibásBevittAdat("Nincs kijelöve a törlendő tétel");
                AdatokElőírás = KézElőírás.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Hétvége_Előírás ElőírásElem = (from a in AdatokElőírás
                                                    where a.Id == Id
                                                    select a).FirstOrDefault();

                if (ElőírásElem != null)
                    KézElőírás.Törlés(Cmbtelephely.Text.Trim(), Id);

                Vonal_tábla_író();
                Vonal_kiürít();
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

        private void Vonal_kiürít()
        {
            Vonal_Id.Text = "";
            Vonal_Vonal.Text = "";
            Vonal_Mennyiség.Text = "";
            Vonal_red.Text = "";
            Vonal_green.Text = "";
            Vonal_blue.Text = "";
            Vonal_red.BackColor = Color.White;
            Vonal_green.BackColor = Color.White;
            Vonal_blue.BackColor = Color.White;
        }

        private void Vonal_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;

                Vonal_Id.Text = Vonal_tábla.Rows[e.RowIndex].Cells[0].Value.ToString();
                Vonal_Vonal.Text = Vonal_tábla.Rows[e.RowIndex].Cells[1].Value.ToString();
                Vonal_Mennyiség.Text = Vonal_tábla.Rows[e.RowIndex].Cells[2].Value.ToString();

                Vonal_red.Text = Vonal_tábla.Rows[e.RowIndex].Cells[3].Value.ToString();
                Vonal_green.Text = Vonal_tábla.Rows[e.RowIndex].Cells[4].Value.ToString();
                Vonal_blue.Text = Vonal_tábla.Rows[e.RowIndex].Cells[5].Value.ToString();

                Vonal_red.BackColor = Color.FromArgb(int.Parse(Vonal_red.Text), int.Parse(Vonal_green.Text), int.Parse(Vonal_blue.Text));
                Vonal_green.BackColor = Color.FromArgb(int.Parse(Vonal_red.Text), int.Parse(Vonal_green.Text), int.Parse(Vonal_blue.Text));
                Vonal_blue.BackColor = Color.FromArgb(int.Parse(Vonal_red.Text), int.Parse(Vonal_green.Text), int.Parse(Vonal_blue.Text));
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

        private void Vonal_fel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Vonal_Id.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve Vonal.");
                if (!long.TryParse(Vonal_Id.Text.Trim(), out long ID)) ID = 0;
                if (ID <= 1) throw new HibásBevittAdat("Az első elemet nem lehet előrébb tenni.");
                KézElőírás.Csere(Cmbtelephely.Text.Trim(), ID);
                Vonal_tábla_író();
                Vonal_kiürít();
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


        #region V ütemező
        private void AktuálisSzerelvénySzerintiListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fülek.SelectedIndex = 0;
            Tábla_kitöltés();
            Terv = false;
        }

        private void ElőírtSzerelvénySzerintiListaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fülek.SelectedIndex = 0;
            Tábla_kitöltés();
            Terv = true;
        }

        private void Tábla_kitöltés()
        {
            try
            {
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 38;

                Listák_Feltöltése();

                // fejléc elkészítése 
                Tábla.Columns[0].HeaderText = "Ssz";
                Tábla.Columns[0].Width = 60;
                Tábla.Columns[1].HeaderText = "Psz";
                Tábla.Columns[1].Width = 70;
                Tábla.Columns[2].HeaderText = "Típus";
                Tábla.Columns[2].Width = 70;
                Tábla.Columns[2].Frozen = true;
                Tábla.Columns[3].HeaderText = "Vizsg. foka";
                Tábla.Columns[3].Width = 70;
                Tábla.Columns[4].HeaderText = "Vizsg. Ssz.";
                Tábla.Columns[4].Width = 70;
                Tábla.Columns[5].HeaderText = "Vizsg. Vége";
                Tábla.Columns[5].Width = 110;
                Tábla.Columns[6].HeaderText = "V után futott korr";
                Tábla.Columns[6].Width = 70;
                Tábla.Columns[7].HeaderText = "Havi km";
                Tábla.Columns[7].Width = 70;
                Tábla.Columns[8].HeaderText = "Köv. V";
                Tábla.Columns[8].Width = 80;
                Tábla.Columns[9].HeaderText = "Köv. V Ssz";
                Tábla.Columns[9].Width = 80;
                Tábla.Columns[10].HeaderText = "Előző V-től km korr";
                Tábla.Columns[10].Width = 80;

                Tábla.Columns[11].HeaderText = "Köv. V2/V3";
                Tábla.Columns[11].Width = 80;
                Tábla.Columns[12].HeaderText = "Előző V2/V3-től km korr";
                Tábla.Columns[12].Width = 80;
                Tábla.Columns[13].HeaderText = "Jármű státusz";
                Tábla.Columns[13].Width = 120;
                Tábla.Columns[14].HeaderText = "Hiba leírása";
                Tábla.Columns[14].Width = 300;

                Tábla.Columns[15].HeaderText = "Előírt Szerelvény";
                Tábla.Columns[15].Width = 160;
                Tábla.Columns[16].HeaderText = "Csatolhatóság";
                Tábla.Columns[16].Width = 70;
                Tábla.Columns[17].HeaderText = "Kerék átmérő Min";
                Tábla.Columns[17].Width = 80;
                Tábla.Columns[18].HeaderText = "KMU";
                Tábla.Columns[18].Width = 80;
                Tábla.Columns[19].HeaderText = "Ciklus";
                Tábla.Columns[19].Width = 120;

                Tábla.Columns[20].HeaderText = "Vonal";
                Tábla.Columns[20].Width = 70;
                Tábla.Columns[21].HeaderText = "Napos utolsó";
                Tábla.Columns[21].Width = 70;
                Tábla.Columns[22].HeaderText = "Napos szám";
                Tábla.Columns[22].Width = 70;
                Tábla.Columns[23].HeaderText = "E3 nap";
                Tábla.Columns[23].Width = 70;
                Tábla.Columns[24].HeaderText = "Tény Szer.sz";
                Tábla.Columns[24].Width = 70;
                Tábla.Columns[25].HeaderText = "Tény Szerelvény";
                Tábla.Columns[25].Width = 70;
                Tábla.Columns[26].HeaderText = "Előírt Szer Sz";
                Tábla.Columns[26].Width = 70;
                Tábla.Columns[27].HeaderText = "Előírt Szerelvény";
                Tábla.Columns[27].Width = 70;
                Tábla.Columns[28].HeaderText = "EÍ Szer hossz";
                Tábla.Columns[28].Width = 70;
                Tábla.Columns[29].HeaderText = "Státus";
                Tábla.Columns[29].Width = 70;
                Tábla.Columns[30].HeaderText = "E3 vezénylés";
                Tábla.Columns[30].Width = 70;
                Tábla.Columns[31].HeaderText = "Vissza";
                Tábla.Columns[31].Width = 70;
                Tábla.Columns[32].HeaderText = "Kiad";
                Tábla.Columns[32].Width = 70;
                Tábla.Columns[33].HeaderText = "Korrigált km";
                Tábla.Columns[33].Width = 70;
                Tábla.Columns[34].HeaderText = "V után futott";
                Tábla.Columns[34].Width = 70;
                Tábla.Columns[35].HeaderText = "Előző V-től km ";
                Tábla.Columns[35].Width = 80;
                Tábla.Columns[36].HeaderText = "Előző V2/V3-től km ";
                Tábla.Columns[36].Width = 70;
                Tábla.Columns[37].HeaderText = "Friss dátum";
                Tábla.Columns[37].Width = 70;

                // kilistázzuk a adatbázis adatait
                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adatok = (from a in Adatok
                          where a.Törölt == false
                          && a.Valóstípus.Contains("T5C5")
                          orderby a.Azonosító
                          select a).ToList();
                Holtart.Be();

                foreach (Adat_Jármű rekord in Adatok)
                {
                    KorNapikm = 0;
                    VUtánFutot = 0;
                    ElőzőVtől = 0;
                    ElőzőV2V3 = 0;

                    Tábla.RowCount++;
                    int i = Tábla.RowCount - 1;

                    Tábla.Rows[i].Cells[1].Value = rekord.Azonosító;
                    Tábla.Rows[i].Cells[2].Value = rekord.Típus;
                    Tábla.Rows[i].Cells[17].Value = 0;
                    Tábla.Rows[i].Cells[19].Value = "";
                    Tábla.Rows[i].Cells[23].Value = 0;
                    Tábla.Rows[i].Cells[31].Value = "_";
                    Tábla.Rows[i].Cells[32].Value = "_";
                    Tábla.Rows[i].Cells[24].Value = "_";
                    Tábla.Rows[i].Cells[33].Value = 0;
                    Tábla.Rows[i].Cells[34].Value = 0;
                    Tábla.Rows[i].Cells[35].Value = 0;
                    Tábla.Rows[i].Cells[36].Value = 0;
                    switch (rekord.Státus)
                    {
                        case 0:
                            {
                                Tábla.Rows[i].Cells[13].Value = "Üzemképes";
                                break;
                            }
                        case 1:
                            {
                                Tábla.Rows[i].Cells[13].Value = "Szabad";
                                break;
                            }
                        case 2:
                            {
                                Tábla.Rows[i].Cells[13].Value = "Beálló";
                                break;
                            }
                        case 3:
                            {
                                Tábla.Rows[i].Cells[13].Value = "Beállóba adott";
                                break;
                            }
                        case 4:
                            {
                                // üzemképtelennél a pályaszám piros és a státus
                                Tábla.Rows[i].Cells[13].Value = "Üzemképtelen";
                                break;
                            }
                    }
                    Tábla.Rows[i].Cells[29].Value = rekord.Státus;
                    Tábla.Rows[i].Cells[24].Value = rekord.Szerelvénykocsik;
                    Szerelvények_listázása(rekord.Szerelvénykocsik, i);
                    Szerelvények_listázása_előírt(rekord.Azonosító, i);
                    Hiba_listázása(rekord.Azonosító, i);
                    V_km_adatok(rekord.Azonosító, i);
                    Csatolhatóság_listázása(rekord.Azonosító, i);
                    Futásadat_listázása(rekord.Azonosító, i);
                    Előírás_listázás(rekord.Azonosító, i);
                    Kerékátmérő(rekord.Azonosító, i);
                    Vezénylés_listázása(rekord.Azonosító, i);
                    Tábla.Rows[i].Cells[33].Value = 0;
                    Korrekció_km(rekord.Azonosító, i);

                    Tábla.Rows[i].Cells[6].Value = KorNapikm + VUtánFutot;
                    Tábla.Rows[i].Cells[10].Value = KorNapikm + ElőzőVtől;
                    Tábla.Rows[i].Cells[12].Value = KorNapikm + ElőzőV2V3;
                    Holtart.Lép();
                }
                Tábla.Refresh();
                AlsóPanel1 = "lista";

                Tábla.Sort(Tábla.Columns[12], System.ComponentModel.ListSortDirection.Descending);
                for (int ii = 0; ii < Tábla.Rows.Count; ii++)
                {
                    Tábla.Rows[ii].Cells[0].Value = ii + 1;
                }
                Tábla.Sort(Tábla.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                Holtart.Ki();
                Tábla.ClearSelection();
                Tábla.Visible = true;
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

        private void Listák_Feltöltése()
        {
            Szerelvény();
            ElőSzerelvény();
            HibaLista();
            ZSERlista();
            V_km_adatok_lista();
            CsatolLista();
            Futásadatlistázása();
            Előíráslistázás();
            KerékátmérőLista();
            Vezényléslistázása();
        }

        private void Szerelvény()
        {
            try
            {
                AdatokSzer.Clear();
                AdatokSzer = KézSzer.Lista_Adatok(Cmbtelephely.Text.Trim());
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

        private void ElőSzerelvény()
        {
            try
            {
                AdatokSzerelvényElő.Clear();
                AdatokSzerelvényElő = KézSzer.Lista_Adatok(Cmbtelephely.Text.Trim(), true);
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

        private void HibaLista()
        {
            try
            {
                Főkönyv_Funkciók.Napiállók(Cmbtelephely.Text.Trim());
                AdatokHiba.Clear();
                AdatokHiba = KézHiba.Lista_Adatok(Cmbtelephely.Text.Trim());
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

        private void ZSERlista()
        {
            try
            {
                AdatokZSER.Clear();
                AdatokZSER = KézZser.Lista_adatok(DateTime.Today.Year);
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

        private void V_km_adatok_lista()
        {
            try
            {
                AdatokVkm.Clear();
                AdatokVkm = KézVkm.Lista_Adatok();
                AdatokVkm = (from a in AdatokVkm
                             where a.Törölt == false
                             orderby a.Azonosító ascending, a.Vizsgdátumk descending
                             select a).ToList();
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

        private void CsatolLista()
        {
            try
            {
                AdatokCsatoló.Clear();
                AdatokCsatoló = KézCsat.Lista_Adat();
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

        private void Futásadatlistázása()
        {
            try
            {
                AdatokFutás.Clear();
                AdatokFutás = KézFutás.Lista_Adatok("Főmérnökség", DateTime.Today);
                AdatokFutás = (from a in AdatokFutás
                               where a.Telephely == Cmbtelephely.Text.Trim()
                               orderby a.Azonosító
                               select a).ToList();
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

        private void Előíráslistázás()
        {
            try
            {
                AdatokHBeosztás.Clear();
                AdatokHBeosztás = KézHBeosztás.Lista_Adatok(Cmbtelephely.Text.Trim());
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

        private void KerékátmérőLista()
        {
            try
            {
                Mérés_Adatok.Clear();
                Mérés_Adatok = Mérés_kéz.Lista_Adatok(DateTime.Today.Year);
                List<Adat_Kerék_Mérés> Mérés_AdatokE = Mérés_kéz.Lista_Adatok(DateTime.Today.Year - 1);
                if (Mérés_AdatokE != null)
                    Mérés_Adatok.AddRange(Mérés_AdatokE);
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

        private void Vezényléslistázása()
        {
            try
            {
                AdatokVezény.Clear();
                AdatokVezény = KézVezény.Lista_Adatok(Cmbtelephely.Text.Trim(), DateTime.Today);
                AdatokVezény = (from a in AdatokVezény
                                where a.Dátum >= DateTime.Today.AddDays(-1)
                                && a.Törlés == 0
                                orderby a.Azonosító
                                select a).ToList();
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

        private void Szerelvények_listázása(long Szerelvény_ID, int sor)
        {
            try
            {
                if (AdatokSzer == null) return;
                Adat_Szerelvény rekordszer = (from a in AdatokSzer
                                              where a.Szerelvény_ID == Szerelvény_ID
                                              select a).FirstOrDefault();
                if (rekordszer != null)
                {
                    string ideig = "";
                    // ha egyforma akkor kiírjuk
                    if (rekordszer.Kocsi1.Trim() != "_" && rekordszer.Kocsi1.Trim() != "0") ideig += rekordszer.Kocsi1.Trim();
                    if (rekordszer.Kocsi2.Trim() != "_" && rekordszer.Kocsi2.Trim() != "0") ideig += "-" + rekordszer.Kocsi2.Trim();
                    if (rekordszer.Kocsi3.Trim() != "_" && rekordszer.Kocsi3.Trim() != "0") ideig += "-" + rekordszer.Kocsi3.Trim();
                    if (rekordszer.Kocsi4.Trim() != "_" && rekordszer.Kocsi4.Trim() != "0") ideig += "-" + rekordszer.Kocsi4.Trim();
                    if (rekordszer.Kocsi5.Trim() != "_" && rekordszer.Kocsi5.Trim() != "0") ideig += "-" + rekordszer.Kocsi5.Trim();
                    if (rekordszer.Kocsi6.Trim() != "_" && rekordszer.Kocsi6.Trim() != "0") ideig += "-" + rekordszer.Kocsi6.Trim();
                    Tábla.Rows[sor].Cells[25].Value = ideig;
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

        private void Szerelvények_listázása_előírt(string azonosító, int sor)
        {
            try
            {
                if (AdatokSzerelvényElő == null) return;
                Adat_Szerelvény Elem = (from a in AdatokSzerelvényElő
                                        where a.Kocsi1 == azonosító || a.Kocsi2 == azonosító || a.Kocsi3 == azonosító ||
                                              a.Kocsi4 == azonosító || a.Kocsi5 == azonosító || a.Kocsi6 == azonosító
                                        select a).FirstOrDefault();
                if (Elem != null)
                {
                    string ideig = Elem.Kocsi1.Trim();
                    ideig += Elem.Kocsi2.Trim() == "_" ? "" : "-" + Elem.Kocsi2.Trim();
                    ideig += Elem.Kocsi3.Trim() == "_" ? "" : "-" + Elem.Kocsi3.Trim();
                    ideig += Elem.Kocsi4.Trim() == "_" ? "" : "-" + Elem.Kocsi4.Trim();
                    ideig += Elem.Kocsi5.Trim() == "_" ? "" : "-" + Elem.Kocsi5.Trim();
                    ideig += Elem.Kocsi6.Trim() == "_" ? "" : "-" + Elem.Kocsi6.Trim();

                    Tábla.Rows[sor].Cells[26].Value = Elem.Szerelvény_ID;
                    Tábla.Rows[sor].Cells[15].Value = ideig;
                    Tábla.Rows[sor].Cells[27].Value = ideig;
                    Tábla.Rows[sor].Cells[28].Value = Elem.Szerelvényhossz;
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

        private void Hiba_listázása(string azonosító, int sor)
        {
            try
            {
                if (AdatokHiba == null) return;
                Adat_Nap_Hiba rekordszer = (from a in AdatokHiba
                                            where a.Azonosító == azonosító
                                            select a).FirstOrDefault();
                if (rekordszer != null)
                    Tábla.Rows[sor].Cells[14].Value = rekordszer.Üzemképtelen + "-" + rekordszer.Beálló + "-" + rekordszer.Üzemképeshiba;
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

        private void V_km_adatok(string azonosító, int sor)
        {
            try
            {
                if (AdatokVkm != null)
                {
                    Adat_T5C5_Kmadatok rekordszer = (from a in AdatokVkm
                                                     where a.Azonosító == azonosító
                                                     select a).FirstOrDefault();
                    if (rekordszer != null)
                    {
                        Tábla.Rows[sor].Cells[3].Value = rekordszer.Vizsgfok;
                        Tábla.Rows[sor].Cells[4].Value = rekordszer.Vizsgsorszám;
                        Tábla.Rows[sor].Cells[5].Value = rekordszer.Vizsgdátumv.ToString("yyyy.MM.dd");
                        if (rekordszer.Vizsgsorszám == 0)
                        {
                            // ha J akkor nem kell különbséget képezni
                            Tábla.Rows[sor].Cells[34].Value = rekordszer.KMUkm;
                            VUtánFutot = rekordszer.KMUkm;
                        }
                        else
                        {
                            Tábla.Rows[sor].Cells[34].Value = rekordszer.KMUkm - rekordszer.Vizsgkm;
                            VUtánFutot = rekordszer.KMUkm - rekordszer.Vizsgkm;
                        }
                        Tábla.Rows[sor].Cells[7].Value = rekordszer.Havikm;
                        Tábla.Rows[sor].Cells[8].Value = rekordszer.KövV;
                        Tábla.Rows[sor].Cells[9].Value = rekordszer.KövV_sorszám;
                        Tábla.Rows[sor].Cells[35].Value = rekordszer.KMUkm - rekordszer.Vizsgkm;
                        ElőzőVtől = rekordszer.KMUkm - rekordszer.Vizsgkm;
                        Tábla.Rows[sor].Cells[11].Value = rekordszer.KövV2;
                        Tábla.Rows[sor].Cells[36].Value = rekordszer.KMUkm - rekordszer.V2V3Számláló;
                        ElőzőV2V3 = rekordszer.KMUkm - rekordszer.V2V3Számláló;
                        Tábla.Rows[sor].Cells[18].Value = rekordszer.KMUkm;
                        Tábla.Rows[sor].Cells[19].Value = rekordszer.Ciklusrend.Trim();
                        Tábla.Rows[sor].Cells[37].Value = rekordszer.KMUdátum.ToString("yyyy.MM.dd");
                    }
                    else
                    {
                        Tábla.Rows[sor].Cells[3].Value = "_";
                        Tábla.Rows[sor].Cells[4].Value = 0;
                        Tábla.Rows[sor].Cells[5].Value = "1900.01.01";
                        Tábla.Rows[sor].Cells[34].Value = 0;
                        Tábla.Rows[sor].Cells[7].Value = 0;
                        Tábla.Rows[sor].Cells[8].Value = "_";
                        Tábla.Rows[sor].Cells[9].Value = 0;
                        Tábla.Rows[sor].Cells[35].Value = 0;
                        Tábla.Rows[sor].Cells[11].Value = "_";
                        Tábla.Rows[sor].Cells[36].Value = 0;
                        Tábla.Rows[sor].Cells[18].Value = 0;
                        Tábla.Rows[sor].Cells[19].Value = "_";
                        Tábla.Rows[sor].Cells[37].Value = "1900.01.01";
                    }
                }
                else
                {
                    Tábla.Rows[sor].Cells[3].Value = "_";
                    Tábla.Rows[sor].Cells[4].Value = 0;
                    Tábla.Rows[sor].Cells[5].Value = "1900.01.01";
                    Tábla.Rows[sor].Cells[34].Value = 0;
                    Tábla.Rows[sor].Cells[7].Value = 0;
                    Tábla.Rows[sor].Cells[8].Value = "_";
                    Tábla.Rows[sor].Cells[9].Value = 0;
                    Tábla.Rows[sor].Cells[35].Value = 0;
                    Tábla.Rows[sor].Cells[11].Value = "_";
                    Tábla.Rows[sor].Cells[36].Value = 0;
                    Tábla.Rows[sor].Cells[18].Value = 0;
                    Tábla.Rows[sor].Cells[19].Value = "_";
                    Tábla.Rows[sor].Cells[37].Value = "1900.01.01";
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

        private void Csatolhatóság_listázása(string azonosító, int sor)
        {
            try
            {
                if (AdatokCsatoló == null) return;
                Adat_Osztály_Adat rekordszer = (from a in AdatokCsatoló
                                                where a.Azonosító == azonosító
                                                select a).FirstOrDefault();
                if (rekordszer != null)
                    Tábla.Rows[sor].Cells[16].Value = KézCsat.Érték(rekordszer, "Csatolhatóság");
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

        private void Futásadat_listázása(string azonosító, int sor)
        {
            try
            {
                if (AdatokFutás == null) return;
                Adat_T5C5_Göngyöl rekordszer = (from a in AdatokFutás
                                                where a.Azonosító == azonosító
                                                select a).FirstOrDefault();
                if (rekordszer != null)
                {
                    Tábla.Rows[sor].Cells[21].Value = rekordszer.Vizsgálatfokozata;
                    Tábla.Rows[sor].Cells[22].Value = rekordszer.Vizsgálatszáma;
                    Tábla.Rows[sor].Cells[23].Value = rekordszer.Futásnap;
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

        private void Előírás_listázás(string azonosító, int sor)
        {
            try
            {
                if (AdatokHBeosztás == null) return;
                Adat_Hétvége_Beosztás rekordszer = (from a in AdatokHBeosztás
                                                    where a.Kocsi1 == azonosító || a.Kocsi2 == azonosító || a.Kocsi3 == azonosító ||
                                                    a.Kocsi4 == azonosító || a.Kocsi5 == azonosító || a.Kocsi6 == azonosító
                                                    select a).FirstOrDefault();
                if (rekordszer != null)
                {
                    Tábla.Rows[sor].Cells[20].Value = rekordszer.Vonal;
                    string ideig = rekordszer.Vissza1 + "-" + rekordszer.Vissza2 + "-" + rekordszer.Vissza3 + "-" + rekordszer.Vissza4 + "-" + rekordszer.Vissza5 + "-" + rekordszer.Vissza6;
                    Tábla.Rows[sor].Cells[31].Value = ideig;
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

        private void Kerékátmérő(string azonosító, int sor)
        {
            try
            {
                if (Mérés_Adatok == null) return;
                List<Adat_Kerék_Mérés> Elem = (from a in Mérés_Adatok
                                               where a.Azonosító == azonosító
                                               orderby a.Mikor descending
                                               select a).ToList();
                if (Elem != null && Elem.Count != 0)
                {
                    int min = Elem.Take(4).Min(b => b.Méret);
                    Tábla.Rows[sor].Cells[17].Value = min;
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

        private void Vezénylés_listázása(string azonosító, int sor)
        {
            try
            {
                if (AdatokVezény != null && AdatokVezény.Count != 0)
                {
                    Adat_Vezénylés rekord = (from a in AdatokVezény
                                             where a.Azonosító == azonosító
                                             select a).FirstOrDefault();
                    if (rekord != null)
                    {
                        // ha egyforma akkor kiírjuk
                        if (rekord.Vizsgálatraütemez == 1)
                        {
                            // előző napi
                            if (rekord.Dátum.ToString("MM-dd-yyyy") == DateTime.Today.AddDays(-1).ToString("MM-dd-yyyy"))
                                Tábla.Rows[sor].Cells[30].Value = rekord.Vizsgálat.Trim() + "-" + rekord.Dátum.ToString("MM.dd") + "-e";
                            // aznapi
                            else if (rekord.Dátum.ToString("MM-dd-yyyy") == DateTime.Today.ToString("MM-dd-yyyy"))
                                Tábla.Rows[sor].Cells[30].Value = rekord.Vizsgálat.Trim() + "-" + rekord.Dátum.ToString("MM.dd") + "-a";
                            else
                                Tábla.Rows[sor].Cells[30].Value = rekord.Vizsgálat.Trim() + "-" + rekord.Dátum.ToString("MM.dd") + "-u";
                        }
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

        private void Korrekció_km(string azonosító, int sor)
        {
            try
            {
                Tábla.Rows[sor].Cells[33].Value = 0;
                if (AdatokZSER == null) return;

                List<Adat_Főkönyv_Zser_Km> KorNapikmLista = (from a in AdatokZSER
                                                             where a.Azonosító == azonosító && a.Dátum > Tábla.Rows[sor].Cells[37].Value.ToÉrt_DaTeTime()
                                                             select a).ToList();

                if (KorNapikmLista != null)
                    KorNapikm = KorNapikmLista.Sum(a => a.Napikm);

                Tábla.Rows[sor].Cells[33].Value = KorNapikm;

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

        private void Előírás_listázásFrissít()
        {
            try
            {
                Előíráslistázás();
                if (AdatokHBeosztás == null) return;
                Holtart.Be(AdatokHBeosztás.Count + 1);
                for (int i = 0; i < Tábla.Rows.Count; i++)
                {
                    string pályaszám = Tábla.Rows[i].Cells[1].Value.ToStrTrim();
                    Adat_Hétvége_Beosztás rekordszer = (from a in AdatokHBeosztás
                                                        where a.Kocsi1 == pályaszám || a.Kocsi2 == pályaszám || a.Kocsi3 == pályaszám ||
                                                        a.Kocsi4 == pályaszám || a.Kocsi5 == pályaszám || a.Kocsi6 == pályaszám
                                                        select a).FirstOrDefault();
                    if (rekordszer != null)
                    {
                        Tábla.Rows[i].Cells[20].Value = rekordszer.Vonal;
                        string ideig = rekordszer.Vissza1 + "-" + rekordszer.Vissza2 + "-" + rekordszer.Vissza3 + "-" + rekordszer.Vissza4 + "-" + rekordszer.Vissza5 + "-" + rekordszer.Vissza6;
                        Tábla.Rows[i].Cells[31].Value = ideig;
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

            // kiírja a hétvégi előírást
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\villamos\előírásgyűjteményúj.mdb";
            string szöveg = "Select * FROM beosztás ORDER BY id";
            string jelszó = "pozsgaii";
            // sorbarendezzük a táblát pályaszám szerint

            Tábla.Sort(Tábla.Columns[1], System.ComponentModel.ListSortDirection.Ascending);


            List<Adat_Hétvége_Beosztás> Adatok = KézHBeosztás.Lista_Adatok(hely, jelszó, szöveg);

            Holtart.Be(100);

            for (int i = 0; i < Tábla.Rows.Count; i++)
            {
                foreach (Adat_Hétvége_Beosztás rekordszer in Adatok)
                {
                    if (Tábla.Rows[i].Cells[1].Value.ToString().Trim() == rekordszer.Kocsi1.Trim() ||
                        Tábla.Rows[i].Cells[1].Value.ToString().Trim() == rekordszer.Kocsi2.Trim() ||
                        Tábla.Rows[i].Cells[1].Value.ToString().Trim() == rekordszer.Kocsi3.Trim() ||
                        Tábla.Rows[i].Cells[1].Value.ToString().Trim() == rekordszer.Kocsi4.Trim() ||
                        Tábla.Rows[i].Cells[1].Value.ToString().Trim() == rekordszer.Kocsi5.Trim() ||
                        Tábla.Rows[i].Cells[1].Value.ToString().Trim() == rekordszer.Kocsi6.Trim())
                    {
                        Tábla.Rows[i].Cells[20].Value = rekordszer.Vonal.Trim();
                        string ideig = "";
                        if (rekordszer.Vissza1.Trim() == "1")
                            ideig += "1";
                        else
                            ideig += "0";
                        if (rekordszer.Vissza2.Trim() == "1")
                            ideig += "-1";
                        else
                            ideig += "-0";
                        if (rekordszer.Vissza3.Trim() == "1")
                            ideig += "-1";
                        else
                            ideig += "-0";
                        if (rekordszer.Vissza4.Trim() == "1")
                            ideig += "-1";
                        else
                            ideig += "-0";
                        if (rekordszer.Vissza5.Trim() == "1")
                            ideig += "-1";
                        else
                            ideig += "-0";
                        if (rekordszer.Vissza6.Trim() == "1")
                            ideig += "-1";
                        else
                            ideig += "-0";

                        Tábla.Rows[i].Cells[31].Value = ideig;
                        break;
                    }
                }
            }
            Holtart.Lép();
        }

        private void Tábla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // cellák színezése
            if (AlsóPanel1.Trim() == "lista")
            {
                if (Tábla.Rows[e.RowIndex].Cells[29].Value != null)
                {
                    switch (int.Parse(Tábla.Rows[e.RowIndex].Cells[29].Value.ToString()))
                    {
                        case 3:
                            {
                                // ha beálló
                                Tábla.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.Yellow;
                                Tábla.Rows[e.RowIndex].Cells[1].Style.ForeColor = Color.Black;
                                Tábla.Rows[e.RowIndex].Cells[1].Style.Font = new Font("ThenArial Narrow", 11f, FontStyle.Italic);

                                Tábla.Rows[e.RowIndex].Cells[13].Style.BackColor = Color.Yellow;
                                Tábla.Rows[e.RowIndex].Cells[13].Style.ForeColor = Color.Black;
                                Tábla.Rows[e.RowIndex].Cells[13].Style.Font = new Font("ThenArial Narrow", 11f, FontStyle.Italic);
                                break;
                            }
                        case 4:
                            {
                                // ha BM
                                Tábla.Rows[e.RowIndex].Cells[1].Style.BackColor = Color.Red;
                                Tábla.Rows[e.RowIndex].Cells[1].Style.ForeColor = Color.White;
                                Tábla.Rows[e.RowIndex].Cells[1].Style.Font = new Font("ThenArial Narrow", 11f, FontStyle.Italic);

                                Tábla.Rows[e.RowIndex].Cells[13].Style.BackColor = Color.Red;
                                Tábla.Rows[e.RowIndex].Cells[13].Style.ForeColor = Color.White;
                                Tábla.Rows[e.RowIndex].Cells[13].Style.Font = new Font("ThenArial Narrow", 11f, FontStyle.Italic);
                                break;
                            }
                    }
                }
                if (Tábla.Rows[e.RowIndex].Cells[20].Value != null)
                {
                    foreach (Adat_Hétvége_Előírás Elem in AdatokElőírás)
                    {
                        if (Tábla.Rows[e.RowIndex].Cells[20].Value.ToString().Trim() == Elem.Vonal.Trim())
                        {
                            Tábla.Rows[e.RowIndex].Cells[0].Style.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                            Tábla.Rows[e.RowIndex].Cells[2].Style.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);
                            Tábla.Rows[e.RowIndex].Cells[15].Style.BackColor = Color.FromArgb(Elem.Red, Elem.Green, Elem.Blue);

                            break;
                        }
                    }
                }
            }
        }
        #endregion


        #region Keresés
        private void Keresés_metódus()
        {
            try
            {
                if (Új_Ablak_Kereső == null)
                {
                    Új_Ablak_Kereső = new Ablak_Kereső();
                    Új_Ablak_Kereső.FormClosed += Új_Ablak_Kereső_Closed;
                    Új_Ablak_Kereső.Top = 50;
                    Új_Ablak_Kereső.Left = 50;
                    Új_Ablak_Kereső.Show();
                    Új_Ablak_Kereső.Ismétlődő_Változás += Szövegkeresés;
                }
                else
                {
                    Új_Ablak_Kereső.Activate();
                    Új_Ablak_Kereső.WindowState = FormWindowState.Normal;
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

        private void Új_Ablak_Kereső_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kereső = null;
        }

        private void Szövegkeresés()
        {
            // megkeressük a szöveget a táblázatban
            if (Új_Ablak_Kereső.Keresendő == null) return;
            if (Új_Ablak_Kereső.Keresendő.Trim() == "") return;
            if (Tábla.Rows.Count < 0) return;

            for (int i = 0; i < Tábla.Rows.Count; i++)
            {
                if (Tábla.Rows[i].Cells[1].Value.ToString().Trim() == Új_Ablak_Kereső.Keresendő.Trim())
                {
                    Tábla.Rows[i].Cells[1].Style.BackColor = Color.Orange;
                    Tábla.FirstDisplayedScrollingRowIndex = i;
                    Tábla.CurrentCell = Tábla.Rows[i].Cells[1];
                    return;
                }
            }
        }
        #endregion


        #region Táblázatban kattint
        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (AlsóPanel1 == "szerelvény") return;
                if (e.RowIndex < 0) return;

                Táblázatba_kattint(e.RowIndex);
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

        private void Táblázatba_kattint(int sor)
        {
            try
            {
                string Tény;

                //Terv szerint vagy Tény szerint listáz
                if (Terv)
                {
                    //ha üres a szerelvény akkor a  pályaszám az egy elem
                    if (Tábla.Rows[sor].Cells[27].Value == null)
                        Tény = Tábla.Rows[sor].Cells[1].Value.ToString();
                    else
                        Tény = Tábla.Rows[sor].Cells[27].Value.ToString();
                }
                else
                {
                    //ha üres a szerelvény akkor a  pályaszám az egy elem
                    if (Tábla.Rows[sor].Cells[25].Value == null)
                        Tény = Tábla.Rows[sor].Cells[1].Value.ToString();
                    else
                        Tény = Tábla.Rows[sor].Cells[25].Value.ToString();
                }
                //Ciklusba átírjuk a pályaszámot és a többi adatot
                Ciklus_Pályaszám.Text = Tábla.Rows[sor].Cells[1].Value.ToString();
                CiklusTípus.Text = Tábla.Rows[sor].Cells[19].Value.ToString();
                J_tőlFutott.Text = Tábla.Rows[sor].Cells[18].Value.ToString();
                Következő_vizsgálat.Text = Tábla.Rows[sor].Cells[8].Value.ToString();

                //Hány kocsiból áll a szerelvény
                string[] darab = Tény.Split('-');
                int[] sorok = new int[darab.Length];


                //Szerelvény járműveinek sorainak megkeresése
                for (int i = 0; i < darab.Length; i++)
                {
                    for (int j = 0; j < Tábla.Rows.Count; j++)
                    {
                        if (Tábla.Rows[j].Cells[1].Value.ToString().Trim() == darab[i].Trim())
                        {
                            sorok[i] = j;
                            break;
                        }

                    }
                }

                Adat_T5C5_Posta Posta;
                Posta_lista.Clear();

                //Összegyűjtük a szerelvény adatait
                for (int i = 0; i < darab.Length; i++)
                {
                    string Azonosító = Tartalom_Vizsgál(sorok[i], 1);
                    string Típus = Tartalom_Vizsgál(sorok[i], 2);
                    string Csatolható = Tartalom_Vizsgál(sorok[i], 16);

                    string V2_következő = Tartalom_Vizsgál(sorok[i], 11);
                    int V2_Futott_Km = Tartalom_Vizs_Int(sorok[i], 12);


                    int V_sorszám = Tartalom_Vizs_Int(sorok[i], 9);
                    string V_Következő = Tartalom_Vizsgál(sorok[i], 8);
                    int V_futott_Km = Tartalom_Vizs_Int(sorok[i], 10);

                    int E3_sorszám = Tartalom_Vizs_Int(sorok[i], 22);
                    int Napszám = Tartalom_Vizs_Int(sorok[i], 23);
                    string Terv_Nap = Tartalom_Vizsgál(sorok[i], 30);
                    string Hiba = Tartalom_Vizsgál(sorok[i], 14);

                    string Előírt_szerelvény = Tartalom_Vizsgál(sorok[i], 27) != "" ? Tartalom_Vizsgál(sorok[i], 27) : Tartalom_Vizsgál(sorok[i], 1);
                    string Tényleges_szerelvény = Tartalom_Vizsgál(sorok[i], 25);
                    string Rendelésszám = "";
                    int Státus = Tartalom_Vizs_Int(sorok[i], 29);
                    long szerelvényszám = Tartalom_Vizs_Long(sorok[i], 24);

                    int Vizsgál = 0;
                    int Marad = 0;

                    string Vissza = Tartalom_Vizsgál(sorok[i], 31);
                    string Kiad = Tartalom_Vizsgál(sorok[i], 32);
                    string Vonal = Tartalom_Vizsgál(sorok[i], 20);



                    Posta = new Adat_T5C5_Posta(
                                 Azonosító,
                                 Típus,
                                 Csatolható,
                                 V_sorszám,
                                 V2_következő,
                                 V2_Futott_Km,
                                 V_Következő,
                                 V_futott_Km,
                                 Napszám,
                                 Terv_Nap,
                                 Hiba,
                                 Előírt_szerelvény,
                                 Tényleges_szerelvény,
                                 Rendelésszám,
                                 szerelvényszám,
                                 Státus,
                                 E3_sorszám,
                                 Vizsgál,
                                 Marad,
                                 Kiad,
                                 Vissza,
                                 Vonal,
                                 Terv
                                   );
                    Posta_lista.Add(Posta);
                }

                Új_Ablak_T5C5_Segéd?.Close();


                Új_Ablak_T5C5_Segéd = new Ablak_T5C5_Segéd(Posta_lista, "Vizsgálat", DateTime.Today, Cmbtelephely.Text.Trim(), Terv);
                Új_Ablak_T5C5_Segéd.FormClosed += Ablak_T5C5_Segéd_Closed;
                Új_Ablak_T5C5_Segéd.Top = 150;
                Új_Ablak_T5C5_Segéd.Left = 500;
                Új_Ablak_T5C5_Segéd.Show();
                Új_Ablak_T5C5_Segéd.Változás += Adat_módosítás;
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

        private int Tartalom_Vizs_Int(int sor, int oszlop)
        {
            int válasz = 0;
            if (Tábla.Rows[sor].Cells[oszlop].Value != null)
                válasz = int.Parse(Tábla.Rows[sor].Cells[oszlop].Value.ToString().Trim());
            return válasz;
        }

        private long Tartalom_Vizs_Long(int sor, int oszlop)
        {
            long válasz = 0;
            if (Tábla.Rows[sor].Cells[oszlop].Value != null)
                válasz = int.Parse(Tábla.Rows[sor].Cells[oszlop].Value.ToString().Trim());
            return válasz;
        }

        private string Tartalom_Vizsgál(int sor, int oszlop)
        {
            string válasz = "";
            if (Tábla.Rows[sor].Cells[oszlop].Value != null)
                válasz = Tábla.Rows[sor].Cells[oszlop].Value.ToString().Trim();
            return válasz;
        }

        private void Ablak_T5C5_Segéd_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_T5C5_Segéd = null;
        }

        private void Adat_módosítás()
        {
            Előírás_listázásFrissít();
            Tábla.Sort(Tábla.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
            Holtart.Ki();
        }
        #endregion


        #region Menü
        private void SzínválasztóToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ColorDialog ColorDialog1 = new ColorDialog();
                if (ColorDialog1.ShowDialog() != DialogResult.Cancel)
                {
                    Alap_red = ColorDialog1.Color.R;
                    Alap_green = ColorDialog1.Color.G;
                    Alap_blue = ColorDialog1.Color.B;

                    Vál_red = ColorDialog1.Color.R;
                    Vál_green = ColorDialog1.Color.G;
                    Vál_blue = ColorDialog1.Color.B;
                }
                else
                {
                    // visszaírjuk az eredetit
                    Vál_red = 148;
                    Vál_green = 148;
                    Vál_blue = 148;
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

        private void SzínezToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SzínezToolStripMenuItem.Checked)
                SzínezToolStripMenuItem.Checked = true;
            else
                SzínezToolStripMenuItem.Checked = false;
        }

        private void KeresésCtrlFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Keresés_metódus();
        }

        private void BeosztásAdatokTörléseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Valóban töröljük az eddigi adatokat?", "Biztonsági kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    KézHBeosztás.Törlés(Cmbtelephely.Text.Trim());
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

        private void AktuálisSzerelvénySzerintVizsgálatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                AlsóPanel1 = "szerelvény";

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 19;

                // fejléc elkészítése 
                Tábla.Columns[0].HeaderText = "Típus";
                Tábla.Columns[0].Width = 80;
                Tábla.Columns[1].HeaderText = "Psz";
                Tábla.Columns[1].Width = 80;
                Tábla.Columns[2].HeaderText = "V után futott";
                Tábla.Columns[2].Width = 80;
                Tábla.Columns[3].HeaderText = "Csatolhatóság";
                Tábla.Columns[3].Width = 80;
                Tábla.Columns[4].HeaderText = "Psz";
                Tábla.Columns[4].Width = 80;
                Tábla.Columns[5].HeaderText = "V után futott";
                Tábla.Columns[5].Width = 80;
                Tábla.Columns[6].HeaderText = "Csatolhatóság";
                Tábla.Columns[6].Width = 80;
                Tábla.Columns[7].HeaderText = "Psz";
                Tábla.Columns[7].Width = 80;
                Tábla.Columns[8].HeaderText = "V után futott";
                Tábla.Columns[8].Width = 80;
                Tábla.Columns[9].HeaderText = "Csatolhatóság";
                Tábla.Columns[9].Width = 80;
                Tábla.Columns[10].HeaderText = "Psz";
                Tábla.Columns[10].Width = 80;
                Tábla.Columns[11].HeaderText = "V után futott";
                Tábla.Columns[11].Width = 80;
                Tábla.Columns[12].HeaderText = "Csatolhatóság";
                Tábla.Columns[12].Width = 80;
                Tábla.Columns[13].HeaderText = "Psz";
                Tábla.Columns[13].Width = 80;
                Tábla.Columns[14].HeaderText = "V után futott";
                Tábla.Columns[14].Width = 80;
                Tábla.Columns[15].HeaderText = "Csatolhatóság";
                Tábla.Columns[15].Width = 80;
                Tábla.Columns[16].HeaderText = "Psz";
                Tábla.Columns[16].Width = 80;
                Tábla.Columns[17].HeaderText = "V után futott";
                Tábla.Columns[17].Width = 80;
                Tábla.Columns[18].HeaderText = "Csatolhatóság";
                Tábla.Columns[18].Width = 80;

                List<Adat_Jármű> Adatok = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adatok = (from a in Adatok
                          where a.Valóstípus.Contains("T5C5")
                          orderby a.Szerelvénykocsik, a.Azonosító
                          select a).ToList();

                AdatokCsatoló = KézCsat.Lista_Adat();
                V_km_adatok_lista();

                long előző = 0;
                int oszlop = 0;
                int i = 0;
                Holtart.Be(Adatok.Count);

                foreach (Adat_Jármű rekord in Adatok)
                {
                    if (előző != rekord.Szerelvénykocsik || rekord.Szerelvénykocsik == 0)
                    {
                        Tábla.RowCount++;
                        i = Tábla.RowCount - 1;
                        előző = rekord.Szerelvénykocsik;
                        oszlop = 1;
                    }
                    Tábla.Rows[i].Cells[0].Value = rekord.Valóstípus.Trim();
                    if (előző == rekord.Szerelvénykocsik)
                    {
                        Tábla.Rows[i].Cells[oszlop].Value = rekord.Azonosító.Trim();

                        Adat_T5C5_Kmadatok rekordkm = (from a in AdatokVkm
                                                       where a.Azonosító == rekord.Azonosító.Trim()
                                                       select a).FirstOrDefault();
                        if (rekordkm != null)
                            Tábla.Rows[i].Cells[oszlop + 1].Value = rekordkm.KMUkm - rekordkm.Vizsgkm;

                        Adat_Osztály_Adat rekordszer = (from a in AdatokCsatoló
                                                        where a.Azonosító == rekord.Azonosító
                                                        select a).FirstOrDefault();
                        if (rekordszer != null)
                            Tábla.Rows[i].Cells[oszlop + 2].Value = KézCsat.Érték(rekordszer, "Csatolhatóság");


                        oszlop += 3;
                    }
                    Holtart.Lép();
                }

                Tábla.Refresh();
                Tábla.Sort(Tábla.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                Tábla.Visible = true;
                Tábla.ClearSelection();
                Holtart.Ki();
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

        private void ExcelKimenetKészítéseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"T5C5_Vizsgálat_{Program.PostásTelephely.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, Tábla, true);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Module_Excel.Megnyitás(fájlexc + ".xlsx");
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


        #region Utasítás
        private void Utasítás_tervezet_Click(object sender, EventArgs e)
        {
            Utasítás_Írás();
        }

        private void Utasítás_Írás()
        {
            try
            {
                Txtírásimező.Text = "";
                // kiírja a hétvégi előírást

                string előzővonal = "";
                Txtírásimező.Text = "";
                string szöveg0;
                int i = 0;
                List<Adat_Hétvége_Beosztás> Adatok = KézHBeosztás.Lista_Adatok(Cmbtelephely.Text.Trim());

                szöveg0 = "20 -n forgalomba kell adni:\r\n";

                foreach (Adat_Hétvége_Beosztás rekord in Adatok)
                {
                    if (előzővonal.Trim() == "" || előzővonal.Trim() != rekord.Vonal.Trim())
                    {
                        előzővonal = rekord.Vonal.Trim();
                        szöveg0 += $"\r\n {rekord.Vonal.Trim()} Vonal\r\n\r\n";
                        i = 0;
                    }
                    i++;
                    szöveg0 += i.ToString() + "- ";
                    if (rekord.Kocsi1.Trim() != "") szöveg0 += rekord.Kocsi1.Trim();
                    if (rekord.Kocsi2.Trim() != "") szöveg0 += "-" + rekord.Kocsi2.Trim();
                    if (rekord.Kocsi3.Trim() != "") szöveg0 += "-" + rekord.Kocsi3.Trim();
                    if (rekord.Kocsi4.Trim() != "") szöveg0 += "-" + rekord.Kocsi4.Trim();
                    if (rekord.Kocsi5.Trim() != "") szöveg0 += "-" + rekord.Kocsi5.Trim();
                    if (rekord.Kocsi6.Trim() != "") szöveg0 += "-" + rekord.Kocsi6.Trim();

                    if (rekord.Vissza1 == "1") szöveg0 += " Vissza kell csatolni:" + rekord.Kocsi1.Trim();
                    if (rekord.Vissza2 == "1") szöveg0 += " Vissza kell csatolni:" + rekord.Kocsi2.Trim();
                    if (rekord.Vissza3 == "1") szöveg0 += " Vissza kell csatolni:" + rekord.Kocsi3.Trim();
                    if (rekord.Vissza4 == "1") szöveg0 += " Vissza kell csatolni:" + rekord.Kocsi4.Trim();
                    if (rekord.Vissza5 == "1") szöveg0 += " Vissza kell csatolni:" + rekord.Kocsi5.Trim();
                    if (rekord.Vissza6 == "1") szöveg0 += " Vissza kell csatolni:" + rekord.Kocsi6.Trim();
                    szöveg0 += "\r\n";
                }
                Txtírásimező.Text += szöveg0 + "\r\n";
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

        private void Utasítás_törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Valóban töröljük az eddigi adatokat?", "Biztonsági kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    KézHBeosztás.Törlés(Cmbtelephely.Text.Trim());
                Utasítás_Írás();
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


        #region Ciklus eltolás
        private void Email_Click(object sender, EventArgs e)
        {
            try
            {
                MyO._Application _app = new MyO.Application();
                MyO.MailItem mail = (MyO.MailItem)_app.CreateItem(MyO.OlItemType.olMailItem);
                string Tábla_html;

                // címzett
                mail.To = Címzett.Text.Trim();

                mail.CC = Másolat.Text.Trim(); // másolatot kap

                string szöveg = Tárgy.Text.Trim();
                szöveg = szöveg.Replace("$$", Ciklus_Pályaszám.Text.Trim()).Replace("ßß", J_tőlFutott.Text).Replace("ŁŁ", Következő_vizsgálat.Text).Replace("łł", Kért_vizsgálat.Text).Replace("\r\n", "<br>");
                mail.Subject = szöveg.Trim(); // üzenet tárgya

                // üzent szövege
                mail.HTMLBody = "<html><body> <p> ";
                // üzent szövege
                szöveg = Bevezetés.Text.Trim() + "<br>";
                szöveg = szöveg.Replace("$$", Ciklus_Pályaszám.Text.Trim()).Replace("ßß", J_tőlFutott.Text).Replace("ŁŁ", Következő_vizsgálat.Text).Replace("łł", Kért_vizsgálat.Text).Replace("\r\n", "<br>");
                mail.HTMLBody += szöveg + "</p>";

                // Table start.
                // Adding fejléc.
                Tábla_html = "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 12pt'><tr>";
                foreach (DataGridViewColumn column in Vizs_tábla.Columns)
                    Tábla_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.HeaderText + "</th>";
                Tábla_html += "</tr>";
                // Adding adatsorok.
                foreach (DataGridViewRow row in Vizs_tábla.Rows)
                {
                    Tábla_html += "<tr>";

                    foreach (DataGridViewCell cell in row.Cells)
                        Tábla_html += "<td style='width:120px;border: 1px solid #ccc'>" + cell.Value.ToString() + "</td>";

                    Tábla_html += "</tr>";
                }
                Tábla_html += "</table>";
                // Table end.
                mail.HTMLBody += Tábla_html;

                szöveg = "<p>" + Tárgyalás.Text.Trim() + "<br>";
                szöveg = szöveg.Replace("$$", Ciklus_Pályaszám.Text.Trim()).Replace("ßß", J_tőlFutott.Text).Replace("ŁŁ", Következő_vizsgálat.Text).Replace("łł", Kért_vizsgálat.Text).Replace("\r\n", "<br>");
                mail.HTMLBody += szöveg + "</p>";


                // Table start.
                // Adding fejléc.
                Tábla_html = "<table cellpadding='5' cellspacing='0' style='border: 1px solid #ccc;font-size: 12pt'><tr>";
                foreach (DataGridViewColumn column in Keréktábla.Columns)
                    Tábla_html += "<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.HeaderText + "</th>";
                Tábla_html += "</tr>";
                // Adding adatsorok.
                foreach (DataGridViewRow row in Keréktábla.Rows)
                {
                    Tábla_html += "<tr>";

                    foreach (DataGridViewCell cell in row.Cells)
                        Tábla_html += "<td style='width:120px;border: 1px solid #ccc'>" + cell.Value.ToString() + "</td>";

                    Tábla_html += "</tr>";
                }
                Tábla_html += "</table>";
                // Table end.
                mail.HTMLBody += Tábla_html;
                szöveg = "<p>" + Befejezés.Text.Trim() + "<br>";
                szöveg = szöveg.Replace("$$", Ciklus_Pályaszám.Text.Trim()).Replace("ßß", J_tőlFutott.Text).Replace("ŁŁ", Következő_vizsgálat.Text).Replace("łł", Kért_vizsgálat.Text).Replace("\r\n", "<br>");
                mail.HTMLBody += szöveg;

                mail.HTMLBody += "</p></body></html>  ";

                // outlook
                mail.Importance = MyO.OlImportance.olImportanceNormal;
                ((MyO._MailItem)mail).Send();

                MessageBox.Show("Üzenet el lett küldve", "Üzenet küldés sikeres", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Berendezés_adatok()
        {
            try
            {
                if (Ciklus_Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("A Pályaszám beviteli mező nem lehet üres");

                List<Adat_Kerék_Tábla> Adatok = KézKerék.Lista_Adatok();
                Adatok = (from a in Adatok
                          where a.Azonosító == Ciklus_Pályaszám.Text.Trim()
                          && a.Objektumfajta == "V.KERÉKPÁR"
                          orderby a.Pozíció
                          select a).ToList();

                List<Adat_Kerék_Mérés> AdatokMérés = Mérés_kéz.Lista_Adatok(DateTime.Today.Year);
                List<Adat_Kerék_Mérés> Ideig = Mérés_kéz.Lista_Adatok(DateTime.Today.Year - 1);
                AdatokMérés.AddRange(Ideig);
                AdatokMérés = (from a in AdatokMérés
                               orderby a.Kerékberendezés ascending, a.Mikor descending
                               select a).ToList();

                Keréktábla.Rows.Clear();
                Keréktábla.Columns.Clear();
                Keréktábla.Refresh();
                Keréktábla.Visible = false;
                Keréktábla.ColumnCount = 8;

                // fejléc elkészítése
                Keréktábla.Columns[0].HeaderText = "Psz";
                Keréktábla.Columns[0].Width = 50;
                Keréktábla.Columns[1].HeaderText = "Berendezésszám";
                Keréktábla.Columns[1].Width = 150;
                Keréktábla.Columns[2].HeaderText = "Gyári szám";
                Keréktábla.Columns[2].Width = 100;
                Keréktábla.Columns[3].HeaderText = "Pozíció";
                Keréktábla.Columns[3].Width = 100;
                Keréktábla.Columns[4].HeaderText = "Mérés Dátuma";
                Keréktábla.Columns[4].Width = 170;
                Keréktábla.Columns[5].HeaderText = "Állapot";
                Keréktábla.Columns[5].Width = 100;
                Keréktábla.Columns[6].HeaderText = "Méret";
                Keréktábla.Columns[6].Width = 100;
                Keréktábla.Columns[7].HeaderText = "Megnevezés";
                Keréktábla.Columns[7].Width = 300;

                foreach (Adat_Kerék_Tábla rekord in Adatok)
                {
                    Keréktábla.RowCount++;
                    int i = Keréktábla.RowCount - 1;
                    Keréktábla.Rows[i].Cells[0].Value = rekord.Azonosító;
                    Keréktábla.Rows[i].Cells[1].Value = rekord.Kerékberendezés;
                    Keréktábla.Rows[i].Cells[2].Value = rekord.Kerékgyártásiszám;
                    Keréktábla.Rows[i].Cells[3].Value = rekord.Pozíció;
                    Keréktábla.Rows[i].Cells[7].Value = rekord.Kerékmegnevezés;
                    Adat_Kerék_Mérés Mérés = (from a in AdatokMérés
                                              where a.Kerékberendezés == rekord.Kerékberendezés
                                              select a).FirstOrDefault();
                    if (Mérés != null)
                    {
                        Keréktábla.Rows[i].Cells[4].Value = Mérés.Mikor;
                        Keréktábla.Rows[i].Cells[5].Value = Mérés.Állapot.Trim();
                        Keréktábla.Rows[i].Cells[6].Value = Mérés.Méret;
                    }
                }

                Keréktábla.Visible = true;
                Keréktábla.Refresh();
                Keréktábla.Sort(Keréktábla.Columns[3], System.ComponentModel.ListSortDirection.Ascending);
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

        private void CiklusFrissít_Click(object sender, EventArgs e)
        {
            KMU_kiírása();
            Berendezés_adatok();
            Kiirjaatörténelmet();
        }

        private void KMU_kiírása()
        {
            try
            {
                if (Ciklus_Pályaszám.Text.Trim() == "") return;
                V_km_adatok_lista();
                Adat_T5C5_Kmadatok ElemKm = (from a in AdatokVkm
                                             where a.Azonosító == Ciklus_Pályaszám.Text.Trim()
                                             && a.Törölt == false
                                             orderby a.Vizsgdátumk descending
                                             select a).FirstOrDefault();
                J_tőlFutott.Text = ElemKm.KMUkm.ToString();
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

        private void Kiirjaatörténelmet()
        {
            try
            {
                V_km_adatok_lista();
                List<Adat_T5C5_Kmadatok> Adatok = (from a in AdatokVkm
                                                   where a.Azonosító == Ciklus_Pályaszám.Text.Trim()
                                                   && a.Törölt == false
                                                   orderby a.Vizsgdátumv descending
                                                   select a).ToList();
                Vizs_tábla.Rows.Clear();
                Vizs_tábla.Columns.Clear();
                Vizs_tábla.Refresh();
                Vizs_tábla.Visible = false;
                Vizs_tábla.ColumnCount = 5;

                // fejléc elkészítése
                Vizs_tábla.Columns[0].HeaderText = "Ssz.";
                Vizs_tábla.Columns[0].Width = 80;
                Vizs_tábla.Columns[1].HeaderText = "Psz";
                Vizs_tábla.Columns[1].Width = 80;
                Vizs_tábla.Columns[2].HeaderText = "Vizsg. foka";
                Vizs_tábla.Columns[2].Width = 80;
                Vizs_tábla.Columns[3].HeaderText = "Vizsg. Ssz.";
                Vizs_tábla.Columns[3].Width = 80;
                Vizs_tábla.Columns[4].HeaderText = "Vizsg. Vége";
                Vizs_tábla.Columns[4].Width = 110;

                int i;

                foreach (Adat_T5C5_Kmadatok rekord in Adatok)
                {
                    if (rekord.Vizsgfok.Contains("V2") || rekord.Vizsgfok.Contains("V3") || rekord.Vizsgfok.Contains("J"))
                    {
                        Vizs_tábla.RowCount++;
                        i = Vizs_tábla.RowCount - 1;
                        Vizs_tábla.Rows[i].Cells[0].Value = rekord.ID;
                        Vizs_tábla.Rows[i].Cells[1].Value = rekord.Azonosító;
                        Vizs_tábla.Rows[i].Cells[2].Value = rekord.Vizsgfok;
                        Vizs_tábla.Rows[i].Cells[3].Value = rekord.Vizsgsorszám;
                        Vizs_tábla.Rows[i].Cells[4].Value = rekord.Vizsgdátumv.ToString("yyyy.MM.dd");
                    }
                    if (rekord.Vizsgsorszám == 0)
                        break;
                }
                Vizs_tábla.Visible = true;
                Vizs_tábla.Sort(Vizs_tábla.Columns[4], System.ComponentModel.ListSortDirection.Ascending);
                Vizs_tábla.Refresh();
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

        private void Ciklus_Mentés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Címzett.Text.Trim() == "") throw new HibásBevittAdat("A Címzett mező nem lehet üres.");
                if (CiklusTípus.Text.Trim() == "") throw new HibásBevittAdat("A Ciklus típus mező nem lehet üres.");
                if (Másolat.Text.Trim() == "") Másolat.Text = "_";
                if (Tárgy.Text.Trim() == "") throw new HibásBevittAdat("A Tárgy mező nem lehet üres.");
                if (Kért_vizsgálat.Text.Trim() == "") throw new HibásBevittAdat("A Kért vizsgálat mező nem lehet üres.");
                if (Bevezetés.Text.Trim() == "") throw new HibásBevittAdat("A Bevezetés mező nem lehet üres.");
                if (Tárgyalás.Text.Trim() == "") throw new HibásBevittAdat("A Tárgyalás mező nem lehet üres.");
                if (Befejezés.Text.Trim() == "") throw new HibásBevittAdat("A Befejezés mező nem lehet üres.");
                if (!int.TryParse(Felmentés_Id.Text, out int Id)) Id = 0;

                List<Adat_Kiegészítő_Felmentés> Adatok = KézFelmentés.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Kiegészítő_Felmentés Elem = (from a in Adatok
                                                  where a.CiklusTípus == CiklusTípus.Text.Trim()
                                                  select a).FirstOrDefault();
                Adat_Kiegészítő_Felmentés ADAT = new Adat_Kiegészítő_Felmentés(
                                          Id,
                                          Címzett.Text.Trim(),
                                          Másolat.Text.Trim(),
                                          Tárgy.Text.Trim(),
                                          Kért_vizsgálat.Text.Trim(),
                                          Bevezetés.Text.Trim(),
                                          Tárgyalás.Text.Trim(),
                                          Befejezés.Text.Trim(),
                                          CiklusTípus.Text.Trim());
                if (Elem != null)
                    KézFelmentés.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                else
                    KézFelmentés.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);

                MessageBox.Show("Az adatok Mentése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Felmentés_kiírás()
        {
            List<Adat_Kiegészítő_Felmentés> Adatok = KézFelmentés.Lista_Adatok(Cmbtelephely.Text.Trim());

            Adat_Kiegészítő_Felmentés rekord = (from a in Adatok
                                                where a.CiklusTípus == CiklusTípus.Text.Trim()
                                                select a).FirstOrDefault();

            if (rekord != null)
            {
                Címzett.Text = rekord.Címzett;
                Másolat.Text = rekord.Másolat;
                Tárgy.Text = rekord.Tárgy;
                Kért_vizsgálat.Text = rekord.Kértvizsgálat;
                Bevezetés.Text = rekord.Bevezetés;
                Tárgyalás.Text = rekord.Tárgyalás;
                Befejezés.Text = rekord.Befejezés;
                CiklusTípus.Text = rekord.CiklusTípus;
                Felmentés_Id.Text = rekord.Id.ToString();
            }
            else
            {
                MessageBox.Show("Ehhez a Ciklus típushoz nincsenek még beállítva adatok!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion
    }
}