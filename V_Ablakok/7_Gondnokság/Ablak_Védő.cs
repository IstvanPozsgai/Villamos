using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;


namespace Villamos
{

    public partial class Ablak_védő
    {
        string Alap_hely = "";
        string Könyv_hely = "";
        string Szervezet1, Szervezet2, Szervezet3;


        readonly Kezelő_Védő_Cikktörzs KézCikk = new Kezelő_Védő_Cikktörzs();
        readonly Kezelő_Védő_Könyv KézKönyv = new Kezelő_Védő_Könyv();
        readonly Kezelő_Védő_Könyvelés KézKönyvelés = new Kezelő_Védő_Könyvelés();
        readonly Kezelő_Kiegészítő_Védelem KézVédelem = new Kezelő_Kiegészítő_Védelem();
        readonly Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_Védő_Napló KézNapló = new Kezelő_Védő_Napló();
        readonly Kezelő_Kiegészítő_Jelenlétiív KézJelenléti = new Kezelő_Kiegészítő_Jelenlétiív();

        List<Adat_Védő_Cikktörzs> AdatokCikk = new List<Adat_Védő_Cikktörzs>();
        List<Adat_Védő_Könyv> AdatokKönyv = new List<Adat_Védő_Könyv>();
        List<Adat_Védő_Könyvelés> AdatokKönyvelés = new List<Adat_Védő_Könyvelés>();

#pragma warning disable
        DataTable AdatTáblaALap = new DataTable();
        DataTable AdatTáblaKönyv = new DataTable();
        DataTable AdatTáblaNapló = new DataTable();
        DataTable AdatTáblaLekérd = new DataTable();
        DataTable AdatTáblaTábla = new DataTable();

#pragma warning restore


        #region Alap
        public Ablak_védő()
        {
            InitializeComponent();
            Start();
        }

        void Start()
        {
            try
            {
                Telephelyekfeltöltése();

                Lapfülek.SelectedIndex = 0;
                Fülekkitöltése();
                Jogosultságkiosztás();

                Lapfülek.DrawMode = TabDrawMode.OwnerDrawFixed;

                Napló_Dátumtól.Value = DateTime.Today;
                Napló_Dátumig.Value = DateTime.Today;
                Könyvelési_dátum.Value = DateTime.Today;
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

        private void Ablak_védő_Load(object sender, EventArgs e)
        {
        }

        private void AlapAdatok_Rögzítés()
        {
            try
            {
                AdatokCikk = KézCikk.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());

                KézKönyv.AlapAdatok(Cmbtelephely.Text.Trim());
                // létrehozzuk a PDF könyvtárat
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\PDF".KönyvSzerk();
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

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                { Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim(); }
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

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {

            Könyv_hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\védőkönyv.mdb";
            Alap_hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\védőtörzs.mdb";
            AlapAdatok_Rögzítés();

            Lapfülek.SelectedIndex = 0;
            Fülekkitöltése();
        }

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Alap_Rögzít.Enabled = false;
                Könyv_Rögzít.Enabled = false;
                Rögzít.Enabled = false;

                // csak főmérnökségi belépéssel törölhető
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                }
                else
                {
                }

                melyikelem = 237;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Alap_Rögzít.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Könyv_Rögzít.Enabled = true;
                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Rögzít.Enabled = true;
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
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\védőfelszerelés.html";
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

        private void LapFülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Fülekkitöltése()
        {
            try
            {
                switch (Lapfülek.SelectedIndex)
                {
                    case 0:
                        {
                            // rögzítés
                            Honnan_feltöltések();
                            AcceptButton = Rögzít;
                            break;
                        }
                    case 1:
                        {
                            // törzs lap
                            Azonosítók();
                            VédeleM_feltöltés();
                            Ürít();
                            Alap_tábla_író();
                            AcceptButton = Alap_Rögzít;
                            break;
                        }
                    case 2:
                        {
                            // könyvlap
                            Szeszámkönyvfeltöltés();
                            Névfeltöltés1();
                            Könyv_tábla_író();
                            AcceptButton = Könyv_Rögzít;
                            break;
                        }
                    case 3:
                        {
                            // Lekérdezés
                            Lekérd_Szeszámkönyvfeltöltés();
                            Lekérd_névfeltöltés();
                            Lekérd_Azonosítók();
                            Lekérd_megnevezések();
                            AcceptButton = Lekérd_Jelöltszersz;
                            break;
                        }
                    case 4:
                        {
                            // Naplózás
                            Napló_könyv_feltöltés();
                            AcceptButton = Napló_Listáz;
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

        private void LapFülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Határozza meg, hogy melyik lap van jelenleg kiválasztva
            TabPage SelectedTab = Lapfülek.TabPages[e.Index];

            // Szerezze be a lap fejlécének területét
            Rectangle HeaderRect = Lapfülek.GetTabRect(e.Index);

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
                Font BoldFont = new Font(Lapfülek.Font.Name, Lapfülek.Font.Size, FontStyle.Bold);
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


        #region CikkTörzslap
        private void Azonosítók()
        {
            try
            {
                AdatokCikk = KézCikk.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Védő_Cikktörzs> Adatok;
                if (!Alap_Töröltek.Checked)
                    Adatok = (from a in AdatokCikk
                              where a.Státus == 0
                              select a).ToList();
                else
                    Adatok = (from a in AdatokCikk
                              where a.Státus == 1
                              select a).ToList();

                Alap_Azonosító.Items.Clear();
                Alap_Azonosító.BeginUpdate();

                foreach (Adat_Védő_Cikktörzs Elem in Adatok)
                    Alap_Azonosító.Items.Add(Elem.Azonosító);

                Alap_Azonosító.EndUpdate();
                Alap_Azonosító.Refresh();
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

        private void VédeleM_feltöltés()
        {
            try
            {
                List<Adat_Kiegészítő_Védelem> Adatok = KézVédelem.Lista_Adatok();

                Alap_védelem.Items.Clear();
                foreach (Adat_Kiegészítő_Védelem Elem in Adatok)
                    Alap_védelem.Items.Add(Elem.Megnevezés);
                Alap_védelem.Refresh();
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

        private void Ürít()
        {
            Alap_Megnevezés.Text = "";
            Alap_Méret.Text = "";
            Alap_Aktív.Checked = false;
            Alap_Azonosító.Text = "";
            Alap_Költséghely.Text = "";

            Alap_védelem.Text = "";
            Alap_kockázat.Text = "";
            Alap_szabvány.Text = "";
            Alap_Szint.Text = "";
            Alap_Munk_Megnevezés.Text = "";
        }

        private void Ürít_kis()
        {
            Alap_Megnevezés.Text = "";
            Alap_Méret.Text = "";

            Alap_Aktív.Checked = false;
            Alap_Költséghely.Text = "";
            Alap_védelem.Text = "";
            Alap_kockázat.Text = "";
            Alap_szabvány.Text = "";
            Alap_Szint.Text = "";
            Alap_Munk_Megnevezés.Text = "";
        }

        private void Töröltek_CheckedChanged(object sender, EventArgs e)
        {
            Azonosítók();
            Ürít();
            Alap_tábla_író();
        }

        private void Frissít_Click(object sender, EventArgs e)
        {
            Azonosítók();
            Ürít();
            Alap_tábla_író();
        }

        private void Azonosító_SelectedIndexChanged(object sender, EventArgs e)
        {
            Alap_azonosító_választó();
        }

        private void Alap_azonosító_választó()
        {
            try
            {
                if (Alap_Azonosító.Text.Trim() == "") return;
                Ürít_kis();
                AdatokCikk = KézCikk.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Védő_Cikktörzs rekord = (from a in AdatokCikk
                                              where a.Azonosító == Alap_Azonosító.Text.Trim()
                                              select a).FirstOrDefault();
                if (rekord != null)
                {
                    Alap_Megnevezés.Text = rekord.Megnevezés.Trim();
                    Alap_Méret.Text = rekord.Méret.Trim();
                    Alap_Költséghely.Text = rekord.Költséghely.Trim();
                    if (rekord.Státus == 1)
                        Alap_Aktív.Checked = true;
                    else
                        Alap_Aktív.Checked = false;

                    Alap_védelem.Text = rekord.Védelem.Trim();
                    Alap_kockázat.Text = rekord.Kockázat.Trim();
                    Alap_szabvány.Text = rekord.Szabvány.Trim();
                    Alap_Szint.Text = rekord.Szint.Trim();
                    Alap_Munk_Megnevezés.Text = rekord.Munk_megnevezés.Trim();
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

        private void Új_adat_Click(object sender, EventArgs e)
        {
            Ürít();
        }

        private void Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Alap_Azonosító.Text.Trim() == "") throw new HibásBevittAdat("Azonosító mező kitöltése kötelező");
                if (Alap_Megnevezés.Text.Trim() == "") throw new HibásBevittAdat("Megnevezés mező kitöltése kötelező");

                Alap_Azonosító.Text = MyF.Szöveg_Tisztítás(Alap_Azonosító.Text, 0, 20);
                Alap_Megnevezés.Text = MyF.Szöveg_Tisztítás(Alap_Megnevezés.Text, 0, 50);
                Alap_Méret.Text = MyF.Szöveg_Tisztítás(Alap_Méret.Text, 0, 15);

                if (Alap_Méret.Text.Trim() == "") Alap_Méret.Text = "-";
                if (Alap_Költséghely.Text.Trim() == "") Alap_Költséghely.Text = "-";
                if (Alap_védelem.Text.Trim() == "") Alap_védelem.Text = "-";
                if (Alap_kockázat.Text.Trim() == "") Alap_kockázat.Text = "-";
                if (Alap_szabvány.Text.Trim() == "") Alap_szabvány.Text = "-";
                if (Alap_Szint.Text.Trim() == "") Alap_Szint.Text = "-";
                if (Alap_Munk_Megnevezés.Text.Trim() == "") Alap_Munk_Megnevezés.Text = "-";

                AdatokCikk = KézCikk.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Védő_Cikktörzs rekord = (from a in AdatokCikk
                                              where a.Azonosító == Alap_Azonosító.Text.Trim()
                                              select a).FirstOrDefault();

                Adat_Védő_Cikktörzs ADAT = new Adat_Védő_Cikktörzs(
                                    Alap_Azonosító.Text.Trim(),
                                    Alap_Megnevezés.Text.Trim(),
                                    Alap_Méret.Text.Trim(),
                                    Alap_Aktív.Checked == true ? 1 : 0,
                                    MyF.Szöveg_Tisztítás(Alap_Költséghely.Text.Trim(), 0, 6, true),
                                    MyF.Szöveg_Tisztítás(Alap_védelem.Text.Trim(), 0, 20, true),
                                    MyF.Szöveg_Tisztítás(Alap_kockázat.Text.Trim(), 0, 100, true),
                                    MyF.Szöveg_Tisztítás(Alap_szabvány.Text.Trim(), 0, 50, true),
                                    MyF.Szöveg_Tisztítás(Alap_Szint.Text.Trim(), 0, 50, true),
                                    MyF.Szöveg_Tisztítás(Alap_Munk_Megnevezés.Text.Trim(), 0, 150, true));

                if (rekord == null)
                    KézCikk.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);
                else
                    KézCikk.Módosítás(Cmbtelephely.Text.Trim(), ADAT);

                Azonosítók();
                Ürít();
                Alap_tábla_író();
                MessageBox.Show("Az adatok rögzítése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Alap_tábla_író()
        {
            try
            {
                Alap_tábla.Visible = false;
                Alap_tábla.CleanFilterAndSort();
                AlapTáblaFejléc();
                AlapTáblaTartalom();
                Alap_tábla.DataSource = AdatTáblaALap;
                AlapTáblaOszlopSzélesség();
                Alap_tábla.Visible = true;
                Alap_tábla.Refresh();
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

        private void AlapTáblaTartalom()
        {
            AdatTáblaALap.Clear();
            AdatokCikk = KézCikk.Lista_Adatok(Cmbtelephely.Text.Trim());

            List<Adat_Védő_Cikktörzs> Adatok;
            if (!Alap_Töröltek.Checked)
                Adatok = (from a in AdatokCikk
                          where a.Státus == 0
                          select a).ToList();
            else
                Adatok = (from a in AdatokCikk
                          where a.Státus == 1
                          select a).ToList();

            foreach (Adat_Védő_Cikktörzs rekord in Adatok)
            {
                DataRow Soradat = AdatTáblaALap.NewRow();

                Soradat["Azonosító"] = rekord.Azonosító;
                Soradat["Megnevezés"] = rekord.Megnevezés;
                Soradat["Méret"] = rekord.Méret;
                Soradat["Költséghely"] = rekord.Költséghely;
                Soradat["Aktív"] = rekord.Státus == 1 ? "Törölt" : "Élő";
                Soradat["Védelem"] = rekord.Védelem;
                Soradat["Kockázat"] = rekord.Kockázat;
                Soradat["Szabvány"] = rekord.Szabvány;
                Soradat["Szint"] = rekord.Szint;
                Soradat["Munkavédelmi elnevezés"] = rekord.Munk_megnevezés;
                AdatTáblaALap.Rows.Add(Soradat);
            }


        }

        private void AlapTáblaFejléc()
        {
            try
            {
                AdatTáblaALap.Columns.Clear();
                AdatTáblaALap.Columns.Add("Azonosító");
                AdatTáblaALap.Columns.Add("Megnevezés");
                AdatTáblaALap.Columns.Add("Méret");
                AdatTáblaALap.Columns.Add("Költséghely");
                AdatTáblaALap.Columns.Add("Aktív");
                AdatTáblaALap.Columns.Add("Védelem");
                AdatTáblaALap.Columns.Add("Kockázat");
                AdatTáblaALap.Columns.Add("Szabvány");
                AdatTáblaALap.Columns.Add("Szint");
                AdatTáblaALap.Columns.Add("Munkavédelmi elnevezés");
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

        private void AlapTáblaOszlopSzélesség()
        {
            Alap_tábla.Columns["Azonosító"].Width = 130;
            Alap_tábla.Columns["Megnevezés"].Width = 500;
            Alap_tábla.Columns["Méret"].Width = 120;
            Alap_tábla.Columns["Költséghely"].Width = 100;
            Alap_tábla.Columns["Aktív"].Width = 70;
            Alap_tábla.Columns["Védelem"].Width = 120;
            Alap_tábla.Columns["Kockázat"].Width = 250;
            Alap_tábla.Columns["Szabvány"].Width = 250;
            Alap_tábla.Columns["Szint"].Width = 250;
            Alap_tábla.Columns["Munkavédelmi elnevezés"].Width = 500;
        }

        private void Alap_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Alap_Azonosító.Text = Alap_tábla.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
            Alap_azonosító_választó();
        }

        private void Alap_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Alap_tábla.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Védőtörzsadatok_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Alap_tábla, false);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(fájlexc + ".xlsx");
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


        #region Könyv lap
        private void Szeszámkönyvfeltöltés()
        {
            try
            {
                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Védő_Könyv> Adatok;
                if (!Könyv_Töröltek.Checked)
                    Adatok = (from a in AdatokKönyv
                              where a.Státus == false
                              select a).ToList();
                else
                    Adatok = (from a in AdatokKönyv
                              where a.Státus == true
                              select a).ToList();

                Könyv_szám.Items.Clear();
                Könyv_szám.Items.Add("");
                Könyv_szám.BeginUpdate();
                foreach (Adat_Védő_Könyv Elem in Adatok)
                    Könyv_szám.Items.Add(Elem.Szerszámkönyvszám);

                Könyv_szám.EndUpdate();
                Könyv_szám.Refresh();
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

        private void Névfeltöltés1()
        {
            try
            {
                Könyv_Felelős1.Items.Clear();
                Könyv_Felelős1.BeginUpdate();

                DateTime kilépett = new DateTime(1900, 1, 1);
                List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim()).Where(a => a.Kilépésiidő <= kilépett).ToList();

                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                {
                    Könyv_Felelős1.Items.Add(rekord.DolgozóNév.Trim() + " = " + rekord.Dolgozószám.Trim());
                }
                Könyv_Felelős1.EndUpdate();
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

        private void Frissít_Click1(object sender, EventArgs e)
        {
            Szeszámkönyvfeltöltés();
            Névfeltöltés1();
            Könyv_tábla_író();
        }

        private void Töröltek_CheckedChanged_1(object sender, EventArgs e)
        {
            Könyv_szám.Text = "";
            Szeszámkönyvfeltöltés();
            Könyv_tábla_író();
        }

        private void Szerszámkönyvszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Kírja_könyvet();
        }

        private void Kírja_könyvet()
        {
            try
            {
                if (Könyv_szám.Text.Trim() == "") return;
                Könyv_ürít();
                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Védő_Könyv rekord = (from a in AdatokKönyv
                                          where a.Szerszámkönyvszám == Könyv_szám.Text.Trim()
                                          select a).FirstOrDefault();
                if (rekord != null)
                {
                    Könyv_szám.Text = rekord.Szerszámkönyvszám.Trim();
                    Könyv_megnevezés.Text = rekord.Szerszámkönyvnév.Trim();
                    Könyv_Felelős1.Text = rekord.Felelős1.Trim();
                    Könyv_Törlés.Checked = rekord.Státus;
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

        private void Könyv_ürít()
        {
            Könyv_megnevezés.Text = "";
            Könyv_Felelős1.Text = "";
            Könyv_Törlés.Checked = false;
        }

        private void Könyv_tábla_író()
        {
            try
            {
                Könyv_tábla.Visible = false;
                Könyv_tábla.CleanFilterAndSort();
                KönyvTáblaFejléc();
                KönyvTáblaTartalom();
                Könyv_tábla.DataSource = AdatTáblaKönyv;
                KönyvTáblaOszlopSzélesség();
                Könyv_tábla.Visible = true;
                Könyv_tábla.Refresh();
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

        private void KönyvTáblaFejléc()
        {
            AdatTáblaKönyv.Columns.Clear();
            AdatTáblaKönyv.Columns.Add("Könyvszám");
            AdatTáblaKönyv.Columns.Add("Könyvmegnevezés");
            AdatTáblaKönyv.Columns.Add("Felelős személy");
            AdatTáblaKönyv.Columns.Add("Aktív");
        }

        private void KönyvTáblaOszlopSzélesség()
        {
            Könyv_tábla.Columns["Könyvszám"].Width = 150;
            Könyv_tábla.Columns["Könyvmegnevezés"].Width = 400;
            Könyv_tábla.Columns["Felelős személy"].Width = 400;
            Könyv_tábla.Columns["Aktív"].Width = 100;
        }

        private void KönyvTáblaTartalom()
        {
            try
            {
                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Védő_Könyv> Adatok;
                if (Könyv_Töröltek.Checked)
                    Adatok = (from a in AdatokKönyv
                              where a.Státus == true
                              select a).ToList();
                else
                    Adatok = (from a in AdatokKönyv
                              where a.Státus == false
                              select a).ToList();
                AdatTáblaKönyv.Clear();
                foreach (Adat_Védő_Könyv rekord in Adatok)
                {
                    DataRow Soradat = AdatTáblaKönyv.NewRow();
                    Soradat["Könyvszám"] = rekord.Szerszámkönyvszám;
                    Soradat["Könyvmegnevezés"] = rekord.Szerszámkönyvnév;
                    Soradat["Felelős személy"] = rekord.Felelős1;
                    Soradat["Aktív"] = rekord.Státus ? "Törölt" : "Élő";
                    AdatTáblaKönyv.Rows.Add(Soradat);
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

        private void Könyv_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Könyv_szám.Text = Könyv_tábla.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
            Kírja_könyvet();
        }

        private void Könyv_új_Click(object sender, EventArgs e)
        {
            Könyv_ürít();
            Könyv_szám.Text = "";
        }

        private void Rögzít_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (Könyv_szám.Text.Trim() == "") throw new HibásBevittAdat("A védőkönyvszáma mező nem lehet üres.");
                if (Könyv_megnevezés.Text.Trim() == "") throw new HibásBevittAdat("A védőkönyv megnevezés mező nem lehet üres.");

                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Védő_Könyv Rekord = (from a in AdatokKönyv
                                          where a.Szerszámkönyvszám == Könyv_szám.Text.Trim()
                                          select a).FirstOrDefault();
                Adat_Védő_Könyv ADAT = new Adat_Védő_Könyv(
                                    MyF.Szöveg_Tisztítás(Könyv_szám.Text.Trim(), 0, 10, true),
                                    MyF.Szöveg_Tisztítás(Könyv_megnevezés.Text.Trim(), 0, 50, true),
                                    MyF.Szöveg_Tisztítás(Könyv_Felelős1.Text.Trim(), 0, 60, true),
                                    Könyv_Törlés.Checked);
                if (Rekord == null)
                    KézKönyv.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);
                else
                    KézKönyv.Módosítás(Cmbtelephely.Text.Trim(), ADAT);

                Könyv_szám.Text = "";
                Könyv_ürít();
                Szeszámkönyvfeltöltés();
                Könyv_tábla_író();
                MessageBox.Show("Az adatok rögzítése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Könyv_excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Könyv_tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Védőkönyv_Adatok_" + Program.PostásNév + "_" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Könyv_tábla, false);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(fájlexc + ".xlsx");
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

        private void IDM_dolgozó_Click(object sender, EventArgs e)
        {
            try
            {
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "IDM-s Adatok betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };
                string fájlexc;
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                IDM_Dolgozó.Védő_beolvasás(fájlexc, Cmbtelephely.Text.Trim());
                Névfeltöltés1();

                MessageBox.Show("Az adat konvertálás befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

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


        #region Napló lapfül
        private void Napló_könyv_feltöltés()
        {
            try
            {
                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Védő_Könyv> Adatok = AdatokKönyv.Where(a => a.Státus == false).ToList();

                Napló_Honnan.Items.Clear();
                Napló_Hova.Items.Clear();
                Napló_Honnannév.Items.Clear();
                Napló_Hovánév.Items.Clear();

                Napló_Honnan.Items.Add("");
                Napló_Hova.Items.Add("");
                Napló_Honnannév.Items.Add("");
                Napló_Hovánév.Items.Add("");

                foreach (Adat_Védő_Könyv rekord in Adatok)
                {
                    Napló_Honnan.Items.Add(rekord.Szerszámkönyvszám);
                    Napló_Hova.Items.Add(rekord.Szerszámkönyvszám);
                    Napló_Honnannév.Items.Add(rekord.Szerszámkönyvnév);
                    Napló_Hovánév.Items.Add(rekord.Szerszámkönyvnév);
                }

                Napló_Honnan.Refresh();
                Napló_Hova.Refresh();
                Napló_Honnannév.Refresh();
                Napló_Hovánév.Refresh();
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

        private void Napló_táblaíró()
        {
            try
            {
                Napló_Tábla.Visible = false;
                Napló_Tábla.CleanFilterAndSort();
                NaplóTáblaFejléc();
                NaplóTáblaTartalom();
                Napló_Tábla.DataSource = AdatTáblaNapló;
                NaplóTáblaOszlopSzélesség();
                Napló_Tábla.Visible = true;
                Napló_Tábla.Refresh();
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

        private void NaplóTáblaOszlopSzélesség()
        {
            Napló_Tábla.Columns["Azonosító"].Width = 120;
            Napló_Tábla.Columns["Megnevezés"].Width = 300;
            Napló_Tábla.Columns["Méret"].Width = 100;
            Napló_Tábla.Columns["Mennyiség"].Width = 100;
            Napló_Tábla.Columns["Bizonylatszám"].Width = 130;
            Napló_Tábla.Columns["Honnan"].Width = 100;
            Napló_Tábla.Columns["Hova"].Width = 100;
            Napló_Tábla.Columns["Módosította"].Width = 120;
            Napló_Tábla.Columns["Mód. dátum"].Width = 180;
        }

        private void NaplóTáblaTartalom()
        {
            AdatokCikk = KézCikk.Lista_Adatok(Cmbtelephely.Text.Trim());

            AdatTáblaNapló.Clear();
            List<Adat_Védő_Napló> Adatok = KézNapló.Lista_Adatok(Cmbtelephely.Text.Trim(), Napló_Dátumtól.Value.Year);
            Adatok = (from a in Adatok
                      where a.Módosításidátum > Napló_Dátumtól.Value
                      && a.Módosításidátum < Napló_Dátumig.Value.AddDays(1)
                      select a).ToList();

            if (!(Napló_Honnan.Text.Trim() == ""))
                Adatok = Adatok.Where(a => a.Honnan == Napló_Honnan.Text.Trim()).ToList();

            if (!(Napló_Hova.Text.Trim() == ""))
                Adatok = Adatok.Where(a => a.Hova == Napló_Hova.Text.Trim()).ToList();

            Holtart.Be(Adatok.Count + 1);
            foreach (Adat_Védő_Napló rekord in Adatok)
            {
                Adat_Védő_Cikktörzs Elem = (from a in AdatokCikk
                                            where a.Azonosító == rekord.Azonosító
                                            select a).FirstOrDefault();
                DataRow Soradat = AdatTáblaNapló.NewRow();
                Soradat["Azonosító"] = rekord.Azonosító.Trim();
                Soradat["Mennyiség"] = rekord.Mennyiség;
                Soradat["Bizonylatszám"] = rekord.Gyáriszám.Trim();
                Soradat["Honnan"] = rekord.Honnan.Trim();
                Soradat["Hova"] = rekord.Hova.Trim();
                Soradat["Módosította"] = rekord.Módosította.Trim();
                Soradat["Mód. dátum"] = rekord.Módosításidátum.ToString("yyyy.MM.dd");
                if (Elem != null)
                {
                    Soradat["Megnevezés"] = Elem.Megnevezés.Trim();
                    Soradat["Méret"] = Elem.Méret.Trim();
                }
                else
                {
                    Soradat["Megnevezés"] = "";
                    Soradat["Méret"] = "";
                }
                AdatTáblaNapló.Rows.Add(Soradat);
                Holtart.Lép();
            }
            Holtart.Ki();
        }

        private void NaplóTáblaFejléc()
        {
            AdatTáblaNapló.Columns.Clear();
            AdatTáblaNapló.Columns.Add("Azonosító");
            AdatTáblaNapló.Columns.Add("Megnevezés");
            AdatTáblaNapló.Columns.Add("Méret");
            AdatTáblaNapló.Columns.Add("Mennyiség");
            AdatTáblaNapló.Columns.Add("Bizonylatszám");
            AdatTáblaNapló.Columns.Add("Honnan");
            AdatTáblaNapló.Columns.Add("Hova");
            AdatTáblaNapló.Columns.Add("Módosította");
            AdatTáblaNapló.Columns.Add("Mód. dátum");
        }

        private void Listáz_Click(object sender, EventArgs e)
        {
            Napló_táblaíró();
        }

        private void Dátumtól_ValueChanged(object sender, EventArgs e)
        {
            if (Napló_Dátumtól.Value > Napló_Dátumig.Value)
                Napló_Dátumig.Value = Napló_Dátumtól.Value;
        }

        private void Dátumig_ValueChanged(object sender, EventArgs e)
        {
            if (Napló_Dátumtól.Value > Napló_Dátumig.Value)
                Napló_Dátumtól.Value = Napló_Dátumig.Value;
        }

        private void Excel_gomb_Click(object sender, EventArgs e)
        {
            try
            {
                if (Napló_Tábla.Rows.Count <= 0)
                    return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Védőfelszerelés_Naplózás_" + Program.PostásNév + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Napló_Tábla, true);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(fájlexc + ".xlsx");
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

        private void Honnannév_SelectedIndexChanged(object sender, EventArgs e)
        {
            Napló_Honnan.Text = Könyvszám(Napló_Honnannév.Text.Trim());
        }

        private void Hovánév_SelectedIndexChanged(object sender, EventArgs e)
        {
            Napló_Hova.Text = Könyvszám(Napló_Hovánév.Text.Trim());
        }

        private string Könyvszám(string könyvnév)
        {
            string válasz = "";
            AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());
            Adat_Védő_Könyv Elem = (from a in AdatokKönyv
                                    where a.Szerszámkönyvnév == könyvnév
                                    select a).FirstOrDefault();
            if (Elem != null) válasz = Elem.Szerszámkönyvnév;
            return válasz;
        }

        private string Könyvnév(string könyvszám)
        {
            string válasz = "";
            AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());
            Adat_Védő_Könyv Elem = (from a in AdatokKönyv
                                    where a.Szerszámkönyvszám == könyvszám
                                    select a).FirstOrDefault();
            if (Elem != null) válasz = Elem.Szerszámkönyvnév;
            return válasz;
        }

        private void Honnan_SelectedIndexChanged(object sender, EventArgs e)
        {
            Napló_Honnannév.Text = Könyvnév(Napló_Honnan.Text.Trim());
            Napló_táblaíró();
        }

        private void Hova_SelectedIndexChanged(object sender, EventArgs e)
        {
            Napló_Hovánév.Text = Könyvnév(Napló_Hova.Text.Trim());
            Napló_táblaíró();
        }

        private void Nyomtatvány_Click(object sender, EventArgs e)
        {
            try
            {
                string munkalap = "Munka1";
                // megvizsgáljuk, hogy a feltételeknek megfelel
                if (((Napló_Honnan.Text.Trim() == "")) || (Napló_Hova.Text.Trim() == ""))
                {
                    throw new HibásBevittAdat("A Honnan, vagy a Hova mező nincs kitöltve,ezért nem készül nyomtatványt!");
                }
                // ha van kijelölve sor akkor tovább megy
                if (Napló_Tábla.SelectedRows.Count < 1)
                {
                    throw new HibásBevittAdat("Nincs kijelölve egy sor sem,ezért nem készül nyomtatványt!");
                }

                // melyik eset áll fenn?
                int eset = 0;
                string milyenkönyv = "";
                if (Napló_Honnan.Text.Trim() == "Raktár")
                {
                    eset = 1;
                    milyenkönyv = Napló_Hova.Text.Trim();
                }
                if (Napló_Hova.Text.Trim() == "Raktár")
                {
                    eset = 2;
                    milyenkönyv = Napló_Honnan.Text.Trim();
                }
                if (Napló_Hova.Text.Trim() == "Selejtre")
                {
                    eset = 3;
                    milyenkönyv = Napló_Honnan.Text.Trim();
                }
                if (eset == 0)
                {
                    throw new HibásBevittAdat("Program használati hiba miatt nem készül nyomtatványt!");
                }
                // létrehozzuk az excel táblát
                string fájlexc;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Védőfelszerelés felvételi nyomtatvány készítés",
                    FileName = "Védőfelvétel_" + Program.PostásTelephely.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;


                // megnyitjuk az excelt
                MyE.ExcelLétrehozás();

                Holtart.Be(20);

                // beolvassuk a három szervezeti egységet, és a beosztásokat
                Szervezet_Feltöltés();

                // Szervezeti kiírások
                MyE.Oszlopszélesség(munkalap, "a:a", 23);
                MyE.Oszlopszélesség(munkalap, "b:b", 54);
                MyE.Oszlopszélesség(munkalap, "c:d", 17);
                MyE.Oszlopszélesség(munkalap, "e:e", 14);
                MyE.Kiir(Szervezet1.Trim(), "a1");
                MyE.Kiir(Szervezet2.Trim(), "a2");
                MyE.Kiir(Szervezet3.Trim(), "a3");
                MyE.Betű("a1:a3", false, false, true);
                MyE.Egyesít(munkalap, "a5:E5");
                MyE.Betű("a5", 16);
                MyE.Betű("a5", false, false, true);
                switch (eset)
                {
                    case 1:
                        {
                            MyE.Kiir("Bizonylat a Védőeszköz felvételről", "a5");
                            break;
                        }
                    case 2:
                        {
                            MyE.Kiir("Bizonylat a Védőeszköz leadásáról", "a5");
                            break;
                        }
                    case 3:
                        {
                            MyE.Kiir("Bizonylat a selejtessévált Védőeszköz leadásáról", "a5");
                            break;
                        }
                }
                MyE.Egyesít(munkalap, "b7:E7");
                MyE.Egyesít(munkalap, "b9:E9");
                MyE.Egyesít(munkalap, "b11:E11");
                MyE.Kiir("Könyvszám:", "a7");
                MyE.Kiir("Könyv megnevezése:", "a9");
                MyE.Kiir("Könyvért felelős", "a11");

                // beírjuk a védőkönyv adatokat
                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Védő_Könyv Elem = (from a in AdatokKönyv
                                        where a.Szerszámkönyvszám == milyenkönyv.Trim()
                                        select a).FirstOrDefault();

                if (Elem != null)
                {
                    MyE.Kiir(Elem.Szerszámkönyvszám, "b7");
                    MyE.Kiir(Elem.Szerszámkönyvnév, "b9");
                    MyE.Kiir(Elem.Felelős1, "b11");

                }

                Holtart.Lép();

                // elkészítjük a fejlécet
                MyE.Kiir("Nyilvántartásiszám:", "a15");
                MyE.Kiir("Védőeszköz megnevezése:", "b15");
                MyE.Kiir("Méret:", "c15");
                MyE.Kiir("Bizonylatszám:", "d15");
                MyE.Kiir("Mennyiség:", "e15");
                // beírjuk a felvett szerszámokat
                int sor = 16;
                int hanyadik = 0;

                for (int j = 0; j < Napló_Tábla.Rows.Count; j++)
                {

                    if (Napló_Tábla.Rows[j].Selected == true)
                    {
                        // ha ki van jelölve
                        MyE.Kiir(Napló_Tábla.Rows[j].Cells[0].Value.ToString(), "A" + sor.ToString());
                        MyE.Kiir(Napló_Tábla.Rows[j].Cells[1].Value.ToString(), "b" + sor.ToString());
                        MyE.Kiir(Napló_Tábla.Rows[j].Cells[3].Value.ToString(), "e" + sor.ToString());
                        if (Napló_Tábla.Rows[j].Cells[2].Value.ToStrTrim() != "0")
                        {
                            MyE.Kiir(Napló_Tábla.Rows[j].Cells[2].Value.ToString(), "c" + sor.ToString());
                        }
                        else
                        {
                            MyE.Kiir("-", "c" + sor.ToString());
                        }
                        if (Napló_Tábla.Rows[j].Cells[4].Value.ToStrTrim() != "0")
                        {
                            MyE.Kiir(Napló_Tábla.Rows[j].Cells[4].Value.ToString(), "d" + sor.ToString());
                        }
                        else
                        {
                            MyE.Kiir("-", "d" + sor.ToString());
                        }
                        sor += 1;
                        hanyadik += 1;
                    }
                    Holtart.Lép();
                }

                Holtart.Lép();
                // keretezünk
                MyE.Rácsoz("a15:e" + sor.ToString());
                MyE.Vastagkeret("a15:e15");
                MyE.Vastagkeret("a15:e" + sor.ToString());
                sor += 2;
                MyE.Kiir("Kelt:" + DateTime.Now.ToString(), "a" + sor.ToString());
                sor += 2;
                switch (eset)
                {
                    case 1:
                        {
                            MyE.Kiir("A felsorolt Védőeszköz(ök)et használatra felvettem.", "a" + sor.ToString());
                            break;
                        }
                    case 2:
                        {
                            MyE.Kiir("A felsorolt Védőeszköz(ök)et tovább használatra leadtam.", "a" + sor.ToString());
                            break;
                        }
                    case 3:
                        {
                            MyE.Kiir("A felsorolt Védőeszköz(ök)et selejtezés / javítás céljából leadtam.", "a" + sor.ToString());
                            break;
                        }
                }
                sor += 2;
                MyE.Egyesít(munkalap, "c" + sor.ToString() + ":e" + sor.ToString());
                MyE.Kiir("Dolgozó aláírása", "c" + sor.ToString());

                // pontozás az aláírásnak
                MyE.Egyesít(munkalap, "c" + sor + ":e" + sor);
                MyE.Pontvonal("c" + sor + ":e" + sor);

                sor += 2;
                switch (eset)
                {
                    case 1:
                        {
                            MyE.Kiir("A dolgozónak kiadtam  a felsorolt védőeszköz(ök)et.", "a" + sor.ToString());
                            break;
                        }
                    case 2:
                        {
                            MyE.Kiir("A dolgozótól visszavettem a fenn felsorolt védőeszköz(ök)et.", "a" + sor.ToString());
                            break;
                        }
                    case 3:
                        {
                            MyE.Kiir("A dolgozótól visszavettem a fenn felsorolt védőeszköz(ök)et.", "a" + sor.ToString());
                            break;
                        }
                }
                Holtart.Lép();
                sor += 2;
                MyE.Egyesít(munkalap, "c" + sor.ToString() + ":e" + sor.ToString());
                MyE.Kiir("Raktáros", "c" + sor.ToString());

                // pontozás az aláírásnak
                MyE.Egyesít(munkalap, "c" + sor + ":e" + sor);
                MyE.Pontvonal("c" + sor + ":e" + sor);

                if (eset == 3)
                {
                    sor += 2;
                    MyE.Egyesít(munkalap, "a" + sor.ToString() + ":e" + sor.ToString());
                    MyE.Kiir("A leadott Védőeszköz(ök)et megvizsgáltam és megállapítottam ,hogy a", "a" + sor.ToString());
                    sor += 2;
                    MyE.Egyesít(munkalap, "a" + sor.ToString() + ":e" + sor.ToString());
                    MyE.Kiir("kártérítési felelősség fenn áll.         /      kártérítési felelősséggel a dolgozó nem tartozik.", "a" + sor.ToString());
                    sor += 2;
                    MyE.Egyesít(munkalap, "c" + sor.ToString() + ":e" + sor.ToString());
                    MyE.Kiir("Munkahelyivezető", "c" + sor.ToString());

                    // pontozás az aláírásnak
                    MyE.Egyesít(munkalap, "c" + sor + ":e" + sor);
                    MyE.Pontvonal("c" + sor + ":e" + sor);
                }
                // nyomtatási beállítások
                MyE.NyomtatásiTerület_részletes(munkalap, "a1:e" + sor,
                    balMargó: 0.393700787401575, jobbMargó: 0.393700787401575, felsőMargó: 0.393700787401575, alsóMargó: 0.393700787401575,
                    oldalmagas: "false");

                if (Napló_Nyomtat.Checked == true)
                {
                    MyE.Nyomtatás(munkalap, 1, 2);
                    MessageBox.Show("A Védőeszköz bizonylatok nyomtatása elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                // bezárjuk az Excel-t
                MyE.Aktív_Cella(munkalap, "A1");
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

                if (Napló_Fájltöröl.Checked)
                {
                    if (Napló_Nyomtat.Checked == true)
                        File.Delete(fájlexc + ".xlsx");
                }
                else
                {
                    MyE.Megnyitás(fájlexc);
                    MessageBox.Show("A Védőeszköz bizonylat elkészült.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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

        private void Napló_Tábla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\PDF\{Napló_Tábla.Rows[e.RowIndex].Cells[4].Value.ToStrTrim()}.pdf";
            if (File.Exists(hely))
            {
                Kezelő_Pdf.PdfMegnyitás(PDF_néző, hely);
                Lapfülek.SelectedIndex = 5;
            }
            else
            {
                PDF_néző.Visible = false;
            }
        }
        #endregion


        #region Lekérdezés
        private void Lekérd_Szeszámkönyvfeltöltés()
        {
            try
            {
                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Védő_Könyv> Adatok;
                if (!Lekérd_Töröltek.Checked)
                    Adatok = (from a in AdatokKönyv
                              where a.Státus == false
                              select a).ToList();
                else
                    Adatok = (from a in AdatokKönyv
                              where a.Státus == true
                              select a).ToList();

                Lekérd_Szerszámkönyvszám.Items.Clear();
                Lekérd_Szerszámkönyvszám.BeginUpdate();

                foreach (Adat_Védő_Könyv rekord in Adatok)
                    Lekérd_Szerszámkönyvszám.Items.Add(rekord.Szerszámkönyvszám.Trim() + " = " + rekord.Szerszámkönyvnév.Trim());

                Lekérd_Szerszámkönyvszám.EndUpdate();
                Lekérd_Szerszámkönyvszám.Refresh();
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

        private void Lekérd_Azonosítók()
        {
            try
            {
                AdatokCikk = KézCikk.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Védő_Cikktörzs> Adatok;
                if (!Lekérd_Töröltek.Checked)
                    Adatok = (from a in AdatokCikk
                              where a.Státus == 0
                              select a).ToList();
                else
                    Adatok = (from a in AdatokCikk
                              where a.Státus == 1
                              select a).ToList();
                string hely = Alap_hely.Trim();

                Lekérd_Szerszámazonosító.Items.Clear();
                Lekérd_Szerszámazonosító.BeginUpdate();
                foreach (Adat_Védő_Cikktörzs Elem in Adatok)
                    Lekérd_Szerszámazonosító.Items.Add(Elem.Azonosító);

                Lekérd_Szerszámazonosító.EndUpdate();
                Lekérd_Szerszámazonosító.Refresh();
                Lekérd_Megnevezés.Text = "";
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

        private void Lekérd_névfeltöltés()
        {
            try
            {
                Lekérd_Felelős1.Items.Clear();
                Lekérd_Felelős1.BeginUpdate();

                DateTime kilépett = new DateTime(1900, 1, 1);
                List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim()).Where(a => a.Kilépésiidő <= kilépett).ToList();

                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    Lekérd_Felelős1.Items.Add(rekord.DolgozóNév.Trim() + " = " + rekord.Dolgozószám.Trim());

                Lekérd_Felelős1.EndUpdate();
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

        private void Lekérd_megnevezések()
        {
            try
            {
                AdatokCikk = KézCikk.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Védő_Cikktörzs> Adatok;
                if (!Lekérd_Töröltek.Checked)
                    Adatok = (from a in AdatokCikk
                              where a.Státus == 0
                              select a).ToList();
                else
                    Adatok = (from a in AdatokCikk
                              where a.Státus == 1
                              select a).ToList();

                Lekérd_Megnevezés.Items.Clear();
                Lekérd_Megnevezés.Items.Add("");
                Lekérd_Megnevezés.BeginUpdate();

                foreach (Adat_Védő_Cikktörzs rekord in Adatok)
                    Lekérd_Megnevezés.Items.Add(rekord.Megnevezés.Trim());

                Lekérd_Megnevezés.EndUpdate();
                Lekérd_Megnevezés.Refresh();

                Lekérd_Megnevezés.Text = "";
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

        private void Lenyit_Click(object sender, EventArgs e)
        {
            Lekérd_Szerszámkönyvszám.Height = 500;
        }

        private void Visszacsuk_Click(object sender, EventArgs e)
        {
            Lekérd_Szerszámkönyvszám.Height = 25;
        }

        private void Összeskijelöl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Lekérd_Szerszámkönyvszám.Items.Count; i++)
                Lekérd_Szerszámkönyvszám.SetItemChecked(i, true);
            Lekérd_táblaíró();
            Lekérd_Szerszámkönyvszám.Height = 25;
        }

        private void Mindtöröl_Click(object sender, EventArgs e)
        {
            Lekérd_mindtöröl_esemény();
            Lekérd_Tábla.Rows.Clear();
            Lekérd_Tábla.Columns.Clear();
        }

        private void Lekérd_mindtöröl_esemény()
        {
            for (int i = 0; i < Lekérd_Szerszámkönyvszám.Items.Count; i++)
                Lekérd_Szerszámkönyvszám.SetItemChecked(i, false);
            Lekérd_táblaíró();
            Lekérd_Szerszámkönyvszám.Height = 25;
        }

        private void Lekérd_táblaíró()
        {
            try
            {

                Lekérd_Tábla.Visible = false;
                Lekérd_Tábla.CleanFilterAndSort();
                AdatTáblaLekérd.Clear();
                LekérdTáblaFejléc();
                LekérdTáblaTartalom();
                Lekérd_Tábla.DataSource = AdatTáblaLekérd;
                LekérdTáblaOszlopSzélesség();
                Lekérd_Tábla_Színez();
                Lekérd_Tábla.Visible = true;
                Lekérd_Tábla.Refresh();
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

        private void LekérdTáblaOszlopSzélesség()
        {
            Lekérd_Tábla.Columns["Azonosító"].Width = 120;
            Lekérd_Tábla.Columns["Megnevezés"].Width = 350;
            Lekérd_Tábla.Columns["Méret"].Width = 100;
            Lekérd_Tábla.Columns["Mennyiség"].Width = 100;
            Lekérd_Tábla.Columns["Bizonylatszám"].Width = 130;
            Lekérd_Tábla.Columns["Dátum"].Width = 100;
            Lekérd_Tábla.Columns["Könyvszám"].Width = 100;
            Lekérd_Tábla.Columns["Könyv megnevezés"].Width = 300;
            Lekérd_Tábla.Columns["Státus"].Width = 100;
        }

        private void LekérdTáblaTartalom()
        {
            try
            {
                if (Lekérd_Szerszámkönyvszám.CheckedItems.Count < 1) return;
                AdatTáblaLekérd.Clear();

                AdatokKönyvelés = KézKönyvelés.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());

                foreach (string Elem in Lekérd_Szerszámkönyvszám.CheckedItems)
                {
                    string[] darabol = Elem.Split('=');
                    List<Adat_Védő_Könyvelés> Adatok = (from a in AdatokKönyvelés
                                                        where a.Szerszámkönyvszám == darabol[0].Trim()
                                                        select a).ToList();

                    Holtart.Be(Adatok.Count + 1);
                    foreach (Adat_Védő_Könyvelés rekord in Adatok)
                    {
                        DataRow Soradat = AdatTáblaLekérd.NewRow();

                        Adat_Védő_Cikktörzs CikkElem = (from a in AdatokCikk
                                                        where a.Azonosító == rekord.Azonosító
                                                        select a).FirstOrDefault();

                        Soradat["Azonosító"] = rekord.Azonosító;
                        if (CikkElem != null)
                        {
                            Soradat["Megnevezés"] = CikkElem.Megnevezés;
                            Soradat["Méret"] = CikkElem.Méret;
                        }
                        else
                        {
                            Soradat["Megnevezés"] = "_";
                            Soradat["Méret"] = "_";
                        }
                        Soradat["Mennyiség"] = rekord.Mennyiség;
                        Soradat["Bizonylatszám"] = rekord.Gyáriszám.Trim();
                        Soradat["Dátum"] = rekord.Dátum.ToString("yyyy.MM.dd");
                        Soradat["Könyvszám"] = rekord.Szerszámkönyvszám.Trim();
                        Soradat["Könyv megnevezés"] = darabol[1].Trim();
                        Soradat["Státus"] = !rekord.Státus ? "Aktív" : "Törölt";

                        AdatTáblaLekérd.Rows.Add(Soradat);
                        Holtart.Lép();
                    }
                }
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

        private void LekérdTáblaFejléc()
        {
            AdatTáblaLekérd.Columns.Clear();
            AdatTáblaLekérd.Columns.Add("Azonosító");
            AdatTáblaLekérd.Columns.Add("Megnevezés");
            AdatTáblaLekérd.Columns.Add("Méret");
            AdatTáblaLekérd.Columns.Add("Mennyiség");
            AdatTáblaLekérd.Columns.Add("Bizonylatszám");
            AdatTáblaLekérd.Columns.Add("Dátum");
            AdatTáblaLekérd.Columns.Add("Könyvszám");
            AdatTáblaLekérd.Columns.Add("Könyv megnevezés");
            AdatTáblaLekérd.Columns.Add("Státus");
        }

        private void Lekérd_Tábla_Színez()
        {
            try
            {
                if (Lekérd_Tábla.Columns.Count > 7)
                {
                    foreach (DataGridViewRow row in Lekérd_Tábla.Rows)
                    {
                        if (row.Cells[6].Value != null && row.Cells[6].Value.ToStrTrim() == "Selejt")
                        {
                            row.DefaultCellStyle.ForeColor = Color.White;
                            row.DefaultCellStyle.BackColor = Color.Red;

                        }
                        if (row.Cells[6].Value != null && row.Cells[6].Value.ToStrTrim() == "Érkezett")
                        {
                            row.DefaultCellStyle.ForeColor = Color.White;
                            row.DefaultCellStyle.BackColor = Color.Green;

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

        private void Jelöltszersz_Click(object sender, EventArgs e)
        {
            Lekérd_táblaíró();
            Lekérd_Szerszámkönyvszám.Height = 25;
        }

        private void Töröltek_CheckedChanged_2(object sender, EventArgs e)
        {
            Lekérd_Szeszámkönyvfeltöltés();
        }

        private void Excelclick_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lekérd_Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Védő_Lekérdezés_{Program.PostásNév}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Lekérd_Tábla, false);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(fájlexc + ".xlsx");
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

        private void Nevekkiválasztása_Click(object sender, EventArgs e)
        {
            try
            {
                if (Lekérd_Felelős1.Text.Trim() == "") return;
                Lekérd_mindtöröl_esemény();

                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Védő_Könyv> Adatok = (from a in AdatokKönyv
                                                where a.Felelős1 == Lekérd_Felelős1.Text.Trim()
                                                select a).ToList();

                foreach (Adat_Védő_Könyv rekord in Adatok)
                {
                    for (int j = 0; j < Lekérd_Szerszámkönyvszám.Items.Count; j++)
                    {
                        string[] darab = Lekérd_Szerszámkönyvszám.Items[j].ToString().Split('=');
                        if (rekord.Szerszámkönyvszám.Trim() == darab[0].Trim())
                            Lekérd_Szerszámkönyvszám.SetItemChecked(j, true);

                    }
                }
                Lekérd_táblaíró();
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

        private void Szerszámazonosító_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((Lekérd_Szerszámazonosító.Text.Trim()) == "") return;
                Adat_Védő_Cikktörzs Elem = (from a in AdatokCikk
                                            where a.Azonosító == MyF.Szöveg_Tisztítás(Lekérd_Szerszámazonosító.Text, 0, 20, true)
                                            select a).FirstOrDefault();
                if (Elem != null) Lekérd_Megnevezés.Text = Elem.Megnevezés;
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

        private void Megnevezés_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((Lekérd_Megnevezés.Text.Trim()) == "") return;

                Adat_Védő_Cikktörzs Elem = (from a in AdatokCikk
                                            where a.Megnevezés == Lekérd_Megnevezés.Text.Trim()
                                            select a).FirstOrDefault();
                if (Elem != null) Lekérd_Szerszámazonosító.Text = Elem.Azonosító;
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

        private void Anyagkiíró_Click(object sender, EventArgs e)
        {
            Lekérd_táblaíróanyagra();
        }

        private void Lekérd_táblaíróanyagra()
        {
            try
            {
                Lekérd_Tábla.Visible = false;
                Lekérd_Tábla.CleanFilterAndSort();
                LekérdAnyagTáblaFejléc();
                LekérdAnyagTáblaTartalom();
                Lekérd_Tábla.DataSource = AdatTáblaLekérd;
                LekérdAnyagTáblaOszlopSzélesség();
                Lekérd_Tábla.Visible = true;
                Lekérd_Tábla.Refresh();
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

        private void LekérdAnyagTáblaOszlopSzélesség()
        {
            try
            {
                Lekérd_Tábla.Columns["Azonosító"].Width = 120;
                Lekérd_Tábla.Columns["Megnevezés"].Width = 350;
                Lekérd_Tábla.Columns["Méret"].Width = 100;
                Lekérd_Tábla.Columns["Mennyiség"].Width = 100;
                Lekérd_Tábla.Columns["Bizonylatszám"].Width = 130;
                Lekérd_Tábla.Columns["Dátum"].Width = 100;
                Lekérd_Tábla.Columns["Könyvszám"].Width = 100;
                Lekérd_Tábla.Columns["Könyv megnevezés"].Width = 200;
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

        private void LekérdAnyagTáblaTartalom()
        {
            try
            {
                AdatTáblaLekérd.Clear();
                double Összeg = 0;
                AdatokKönyvelés = KézKönyvelés.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokCikk = KézCikk.Lista_Adatok(Cmbtelephely.Text.Trim());

                List<Adat_Védő_Könyvelés> Adatok;
                if (Lekérd_Szerszámazonosító.Text.Trim() != "")
                    Adatok = (from a in AdatokKönyvelés
                              where a.Azonosító == Lekérd_Szerszámazonosító.Text.Trim() && a.Státus == false
                              select a).ToList();
                else
                    Adatok = AdatokKönyvelés;

                List<Adat_Védő_Könyvelés> AdatokA;
                if (Adatok != null)
                {
                    AdatokA = (from a in Adatok
                               join cikk in AdatokCikk on a.Azonosító equals cikk.Azonosító
                               where cikk.Megnevezés.ToUpper().Contains(Lekérd_Megnevezés.Text.ToUpper().Trim())
                               select a).ToList();
                }
                else
                    AdatokA = Adatok;

                Holtart.Be(Adatok.Count + 1);
                foreach (Adat_Védő_Könyvelés rekord in AdatokA)
                {
                    DataRow Soradat = AdatTáblaLekérd.NewRow();

                    Soradat["Azonosító"] = rekord.Azonosító.Trim();
                    Soradat["Mennyiség"] = rekord.Mennyiség;
                    if (rekord.Szerszámkönyvszám.Trim() != "Raktár")
                        Soradat["Bizonylatszám"] = rekord.Gyáriszám.Trim();
                    else
                        Soradat["Bizonylatszám"] = "";

                    Soradat["Dátum"] = rekord.Dátum.ToString("yyyy.MM.dd");
                    Soradat["Könyvszám"] = rekord.Szerszámkönyvszám;

                    Adat_Védő_Cikktörzs CikkElem = (from a in AdatokCikk
                                                    where a.Azonosító == rekord.Azonosító
                                                    select a).FirstOrDefault();
                    if (CikkElem != null)
                    {
                        Soradat["Megnevezés"] = CikkElem.Megnevezés;
                        Soradat["Méret"] = CikkElem.Méret;
                    }
                    else
                    {
                        Soradat["Megnevezés"] = "_";
                        Soradat["Méret"] = "_";
                    }
                    Adat_Védő_Könyv ElemKönyv = (from a in AdatokKönyv
                                                 where a.Szerszámkönyvszám == rekord.Szerszámkönyvszám
                                                 select a).FirstOrDefault();
                    if (ElemKönyv != null)
                        Soradat["Könyv megnevezés"] = ElemKönyv.Szerszámkönyvnév;
                    else
                        Soradat["Könyv megnevezés"] = "_";

                    if (rekord.Szerszámkönyvszám.Trim() != "Selejt" && rekord.Szerszámkönyvszám.Trim() != "Érkezett")
                        Összeg += rekord.Mennyiség;

                    AdatTáblaLekérd.Rows.Add(Soradat);
                    Holtart.Lép();
                }

                if (AdatTáblaLekérd.Rows.Count > 0)
                {
                    DataRow Soradat = AdatTáblaLekérd.NewRow();
                    Soradat["Azonosító"] = "Összesen:";
                    Soradat["Mennyiség"] = Összeg;
                    AdatTáblaLekérd.Rows.Add(Soradat);

                }
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

        private void LekérdAnyagTáblaFejléc()
        {
            try
            {
                AdatTáblaLekérd.Columns.Clear();
                AdatTáblaLekérd.Columns.Add("Azonosító");
                AdatTáblaLekérd.Columns.Add("Megnevezés");
                AdatTáblaLekérd.Columns.Add("Méret");
                AdatTáblaLekérd.Columns.Add("Mennyiség");
                AdatTáblaLekérd.Columns.Add("Bizonylatszám");
                AdatTáblaLekérd.Columns.Add("Dátum");
                AdatTáblaLekérd.Columns.Add("Könyvszám");
                AdatTáblaLekérd.Columns.Add("Könyv megnevezés");
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

        private void Lekérd_Command1_Click(object sender, EventArgs e)
        {
            // beolvassuk a három szervezeti egységet, és a beosztásokat
            Szervezet_Feltöltés();

            Lekérd_Szerszámkönyvszám.Height = 25;

            Lekérd_táblaíró_más();
        }

        private void Lekérd_táblaíró_más()
        {
            try
            {
                string munkalap = "Munka1";
                AdatokKönyvelés = KézKönyvelés.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());

                string fájlexc;
                string könyvtár = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // táblázatba kilistázzuk a könyv tartalmát
                foreach (string Elem in Lekérd_Szerszámkönyvszám.CheckedItems)
                {
                    string[] darabol = Elem.Split('=');
                    //    string szöveg = $"SELECT * FROM lista WHERE szerszámkönyvszám ='{darabol[0].Trim()}'  ORDER BY azonosító";
                    List<Adat_Védő_Könyvelés> Adatok = (from a in AdatokKönyvelés
                                                        where a.Szerszámkönyvszám == darabol[0].Trim()
                                                        select a).ToList();
                    Lekérd_Tábla.Visible = false;
                    Lekérd_Tábla.CleanFilterAndSort();
                    LekérdTáblaFejléc();
                    LekérdTáblaTartalom();
                    Lekérd_Tábla.DataSource = AdatTáblaLekérd;
                    LekérdTáblaOszlopSzélesség();
                    Lekérd_Tábla_Színez();
                    Lekérd_Tábla.Visible = true;
                    Lekérd_Tábla.Refresh();
                    // kiirt táblából készítünk excel táblát ha a címsoron kívül van tétel
                    if (Lekérd_Tábla.Rows.Count > 0)
                    {
                        // a fájlnév előkészítése
                        fájlexc = $@"{könyvtár}\Védőkönyv_{darabol[0].Trim()}_{Program.PostásTelephely.Trim()}.xlsx";
                        if (File.Exists(fájlexc)) File.Delete(fájlexc);

                        // megnyitjuk az excelt
                        MyE.ExcelLétrehozás();

                        MyE.Oszlopszélesség(munkalap, "A:a", 23);
                        MyE.Oszlopszélesség(munkalap, "B:b", 54);
                        MyE.Oszlopszélesség(munkalap, "c:d", 17);
                        MyE.Oszlopszélesség(munkalap, "E:e", 14);
                        MyE.Oszlopszélesség(munkalap, "F:f", 16);
                        MyE.Kiir(Szervezet1.Trim(), "a1");
                        MyE.Kiir(Szervezet2.Trim(), "a2");
                        MyE.Kiir(Szervezet3.Trim(), "a3");
                        MyE.Betű("a1:a3", false, false, true);
                        MyE.Egyesít(munkalap, "a5:E5");
                        MyE.Betű("a5", 16);
                        MyE.Betű("a5", false, false, true);
                        MyE.Kiir("Egyéni védőeszköz nyilvántartó lap", "a5");

                        MyE.Egyesít(munkalap, "b7:E7");
                        MyE.Egyesít(munkalap, "b9:E9");
                        MyE.Egyesít(munkalap, "b11:E11");
                        MyE.Kiir("Könyvszám:", "a7");
                        MyE.Kiir("Könyv megnevezése:", "a9");
                        MyE.Kiir("Könyvért felelős", "a11");

                        // beírjuk a szerszámkönyv adatokat
                        Adat_Védő_Könyv Könyv = (from a in AdatokKönyv
                                                 where a.Szerszámkönyvszám == darabol[0].Trim()
                                                 select a).FirstOrDefault();
                        if (Könyv != null)
                        {
                            MyE.Kiir(Könyv.Szerszámkönyvszám.Trim(), "b7");
                            MyE.Kiir(Könyv.Szerszámkönyvnév.Trim(), "b9");
                            MyE.Kiir(Könyv.Felelős1.Trim(), "b11");
                        }
                        // elkészítjük a fejlécet
                        MyE.Kiir("Nyilvántartásiszám:", "a15");
                        MyE.Kiir("Védőeszköz megnevezése:", "b15");
                        MyE.Kiir("Méret:", "c15");
                        MyE.Kiir("Bizonylatszám:", "e15");
                        MyE.Kiir("Mennyiség:", "d15");
                        MyE.Kiir("Felvétel dátuma:", "f15");
                        // beírjuk a felvett védőeszközöket

                        for (int sorT = 0; sorT < Lekérd_Tábla.RowCount; sorT++)
                        {
                            for (int oszlop = 0; oszlop <= 5; oszlop++)
                                MyE.Kiir(Lekérd_Tábla.Rows[sorT].Cells[oszlop].Value.ToString(), MyE.Oszlopnév(oszlop + 1) + (sorT + 16).ToString());
                        }

                        int sor = Lekérd_Tábla.Rows.Count + 15;

                        // keretezünk
                        MyE.Rácsoz("a15:f" + sor.ToString());
                        MyE.Vastagkeret("a15:f15");
                        MyE.Vastagkeret("a15:f" + sor.ToString());
                        sor += 2;
                        MyE.Kiir("Kelt:" + DateTime.Today.ToString("yyyy.MM.dd"), "a" + sor.ToString());
                        sor += 2;
                        MyE.Kiir("A felsorolt védőeszköz(öke)t használatra felvettem.", "a" + sor.ToString());
                        sor += 2;
                        MyE.Egyesít(munkalap, "c" + sor.ToString() + ":f" + sor.ToString());
                        MyE.Kiir("Dolgozó aláírása", "c" + sor.ToString());

                        // pontozás az aláírásnak
                        MyE.Egyesít(munkalap, "c" + sor + ":f" + sor);
                        MyE.Pontvonal("c" + sor + ":f" + sor);


                        sor += 5;
                        MyE.Egyesít(munkalap, "c" + sor.ToString() + ":f" + sor.ToString());
                        MyE.Kiir("Raktáros", "c" + sor.ToString());
                        // pontozás az aláírásnak
                        MyE.Egyesít(munkalap, "c" + sor + ":f" + sor);
                        MyE.Pontvonal("c" + sor + ":f" + sor);

                        // nyomtatási beállítások
                        MyE.NyomtatásiTerület_részletes(munkalap, "a1:f" + sor,
                            0.393700787401575, 0.393700787401575,
                            0.393700787401575, 0.393700787401575);

                        // bezárjuk az Excel-t
                        MyE.Aktív_Cella(munkalap, "A1");
                        MyE.ExcelMentés(fájlexc);
                        MyE.ExcelBezárás();
                    }
                }
                Holtart.Ki();
                MessageBox.Show("A kívánt nyilvántartások kiírása megtörtént Excelbe!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Lekérd_Tábla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\PDF\{Lekérd_Tábla.Rows[e.RowIndex].Cells[4].Value.ToStrTrim()}.pdf";
            if (File.Exists(hely))
            {
                Kezelő_Pdf.PdfMegnyitás(PDF_néző, hely);
                Lapfülek.SelectedIndex = 5;
            }
            else
            {
                PDF_néző.Visible = false;
            }
        }
        #endregion


        #region Rögzítés lapfül
        private void Más_dátum_CheckedChanged(object sender, EventArgs e)
        {
            Könyvelési_dátum.Enabled = Más_dátum.Checked;
        }

        private void Honnan_feltöltések()
        {
            try
            {
                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Védő_Könyv> Adatok = (from a in AdatokKönyv
                                                where a.Státus == false
                                                select a).ToList();
                Honnan.Items.Clear();
                Honnan.BeginUpdate();
                HonnanNév.Items.Clear();
                HonnanNév.BeginUpdate();

                foreach (Adat_Védő_Könyv Elem in Adatok)
                {
                    Honnan.Items.Add(Elem.Szerszámkönyvszám);
                    HonnanNév.Items.Add(Elem.Szerszámkönyvnév);
                }

                Honnan.EndUpdate();
                Honnan.Refresh();
                HonnanNév.EndUpdate();
                HonnanNév.Refresh();
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

        private void Hova_feltöltések()
        {
            try
            {
                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Védő_Könyv> Adatok = (from a in AdatokKönyv
                                                where a.Státus == false
                                                select a).ToList();

                Hova.Items.Clear();
                Hova.BeginUpdate();
                HováNév.Items.Clear();
                HováNév.BeginUpdate();

                foreach (Adat_Védő_Könyv Elem in Adatok)
                {
                    Hova.Items.Add(Elem.Szerszámkönyvszám);
                    HováNév.Items.Add(Elem.Szerszámkönyvnév);
                }

                Hova.EndUpdate();
                Hova.Refresh();
                HováNév.EndUpdate();
                HováNév.Refresh();
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

        private void HonnanNév_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                Honnan_kiíró_kszám();
                Hova.Enabled = true;
                HováNév.Enabled = true;
                Hova.Items.Clear();
                HováNév.Items.Clear();
                HonnanMennyiség.Text = 0.ToString();
                HováMennyiség.Text = 0.ToString();
                switch (HonnanNév.Text.Trim() ?? "")
                {
                    case "Új védőeszköz beérkeztetése":
                        {
                            // betölti a teljes választék listát
                            Rögzítés_azonosítók();

                            Hova.Text = "Raktár";
                            HováNév.Text = "Védő Raktár";
                            Hova.Enabled = false;
                            HováNév.Enabled = false;
                            break;
                        }
                    case "Védő Raktár":
                        {
                            Azonosítóhelyen();
                            Hova.Text = "";
                            HováNév.Text = "";
                            Hova_feltöltések();
                            // ide nem lehet könyvelni
                            Hova.Items.Remove("Selejt");
                            HováNév.Items.Remove("Leselejtezett");
                            break;
                        }
                    case "Leselejtezett":
                        {
                            Azonosítóhelyen();
                            Hova.Text = "Selejtre";
                            HováNév.Text = "Selejtezésre előkészítés";
                            Hova.Enabled = false;
                            HováNév.Enabled = false;
                            break;
                        }

                    case "Selejtezésre előkészítés":
                        {
                            Azonosítóhelyen();
                            Hova.Items.Add("Raktár");
                            Hova.Items.Add("Selejt");
                            HováNév.Items.Add("Védő Raktár");
                            HováNév.Items.Add("Leselejtezett");
                            break;
                        }
                    case "Átadás-Átvétel másik telephelyről":
                        {
                            Rögzítés_azonosítók();
                            Hova.Text = "Raktár";
                            HováNév.Text = "Védő Raktár";
                            Hova.Enabled = false;
                            HováNév.Enabled = false;
                            break;
                        }

                    default:
                        {
                            Azonosítóhelyen();
                            Hova.Text = "Raktár";
                            HováNév.Text = "Védő Raktár";
                            Hova.Enabled = false;
                            HováNév.Enabled = false;
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

        private void Rögzítés_azonosítók()
        {
            try
            {
                AdatokCikk = KézCikk.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_Védő_Cikktörzs> Adatok = (from a in AdatokCikk
                                                    where a.Státus == 0
                                                    select a).ToList();

                SzerszámAzonosító.Items.Clear();
                SzerszámAzonosító.BeginUpdate();

                foreach (Adat_Védő_Cikktörzs Elem in Adatok)
                    SzerszámAzonosító.Items.Add(Elem.Azonosító);

                SzerszámAzonosító.EndUpdate();
                SzerszámAzonosító.Refresh();

                SzerszámAzonosító.Text = "";
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

        private void Honnan_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                Honnan_kiíró_név();
                Hova.Enabled = true;
                HováNév.Enabled = true;
                Hova.Items.Clear();
                HováNév.Items.Clear();
                HonnanMennyiség.Text = 0.ToString();
                HováMennyiség.Text = 0.ToString();
                Megnevezés.Text = "";
                if (Honnan.Text.Trim() == "") return;
                switch (Honnan.Text.Trim())
                {
                    case "Érkezett":
                        {
                            // betölti a teljes választék listát
                            Rögzítés_azonosítók();
                            Hova.Text = "Raktár";
                            HováNév.Text = "Védő Raktár";
                            Hova.Enabled = false;
                            HováNév.Enabled = false;
                            SzerszámAzonosító.Focus();
                            break;
                        }
                    case "Raktár":
                        {
                            Azonosítóhelyen();
                            Hova.Text = "";
                            HováNév.Text = "";
                            Hova_feltöltések();
                            // ide nem lehet könyvelni
                            Hova.Items.Remove("Selejt");
                            HováNév.Items.Remove("Leselejtezett");
                            Hova.Refresh();
                            HováNév.Refresh();
                            Hova.Focus();
                            break;
                        }
                    case "Selejt":
                        {
                            Azonosítóhelyen();
                            Hova.Text = "Selejtre";
                            HováNév.Text = "Selejtezésre előkészítés";
                            Hova.Enabled = false;
                            HováNév.Enabled = false;
                            SzerszámAzonosító.Focus();
                            break;
                        }
                    case "Selejtre":
                        {
                            Azonosítóhelyen();
                            Hova.Items.Add("Raktár");
                            Hova.Items.Add("Selejt");
                            HováNév.Items.Add("Védő Raktár");
                            HováNév.Items.Add("Leselejtezett");
                            Hova.Focus();
                            break;
                        }
                    case "Átadás":
                        {
                            Azonosítóhelyen();
                            Hova.Text = "Raktár";
                            HováNév.Text = "Védő Raktár";
                            Hova.Enabled = false;
                            HováNév.Enabled = false;
                            SzerszámAzonosító.Focus();
                            break;
                        }
                    case "Átvétel":
                        {
                            Rögzítés_azonosítók();
                            Hova.Text = "Raktár";
                            HováNév.Text = "Védő Raktár";
                            Hova.Enabled = false;
                            HováNév.Enabled = false;
                            SzerszámAzonosító.Focus();
                            break;
                        }
                    default:
                        {
                            Azonosítóhelyen();
                            Hova.Text = "Raktár";
                            HováNév.Text = "Védő Raktár";
                            Hova.Enabled = false;
                            HováNév.Enabled = false;
                            break;
                        }
                }
                Tábla_Könyv_írás();
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

        private void Hova_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Hova_kiíró_név();
            Darabszámok_kiírása();
        }

        private void Hova_kiíró_név()
        {
            try
            {
                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Védő_Könyv Ideig = (from a in AdatokKönyv
                                         where a.Szerszámkönyvszám == Hova.Text.Trim()
                                         select a).FirstOrDefault();

                if (Ideig != null)
                    HováNév.Text = Ideig.Szerszámkönyvnév;
                else
                    HováNév.Text = "";

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

        private void Hova_kiíró_kszám()
        {
            try
            {
                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Védő_Könyv Ideig = (from a in AdatokKönyv
                                         where a.Szerszámkönyvnév == HováNév.Text.Trim()
                                         select a).FirstOrDefault();
                if (Ideig != null)
                    Hova.Text = Ideig.Szerszámkönyvszám;
                else
                    Hova.Text = "";
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

        private void Honnan_kiíró_név()
        {
            try
            {
                AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Védő_Könyv Ideig = (from a in AdatokKönyv
                                         where a.Szerszámkönyvszám == Honnan.Text.Trim()
                                         select a).FirstOrDefault();
                if (Ideig != null)
                    HonnanNév.Text = Ideig.Szerszámkönyvnév;
                else
                    HonnanNév.Text = "";
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

        private void Honnan_kiíró_kszám()
        {
            try
            {
                if (HonnanNév.Text.Trim() != "Érkezett")
                {
                    AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());

                    Adat_Védő_Könyv Ideig = (from a in AdatokKönyv
                                             where a.Szerszámkönyvnév == HonnanNév.Text.Trim()
                                             select a).FirstOrDefault();
                    if (Ideig != null)
                        Honnan.Text = Ideig.Szerszámkönyvszám;
                    else
                        Honnan.Text = "";
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

        private void HováNév_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            Hova_kiíró_kszám();
            Darabszámok_kiírása();
        }

        private void SzerszámAzonosító_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SzAzonosító_kiíró();
            Darabszámok_kiírása();
            Mennyiség.Focus();
        }

        private void SzerszámAzonosító_DropDownClosed(object sender, EventArgs e)
        {
            SzAzonosító_kiíró();
            Darabszámok_kiírása();
            Mennyiség.Focus();
        }

        private void SzAzonosító_kiíró()
        {
            try
            {
                Mennyiség.Text = "";
                Gyáriszám.Text = "";
                Mennyiség.Enabled = true;
                if (SzerszámAzonosító.Text.Trim() == "") return;

                AdatokCikk = KézCikk.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Védő_Cikktörzs Elem = (from a in AdatokCikk
                                            where a.Azonosító == SzerszámAzonosító.Text.Trim()
                                            select a).FirstOrDefault();

                if (Elem != null) Megnevezés.Text = Elem.Megnevezés;

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

        private void Darabszámok_kiírása()
        {
            try
            {
                AdatokKönyvelés = KézKönyvelés.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Védő_Könyvelés IdeigMennyi = (from a in AdatokKönyvelés
                                                   where a.Azonosító == SzerszámAzonosító.Text.Trim() && a.Szerszámkönyvszám == Honnan.Text.Trim() && a.Státus == false
                                                   select a).FirstOrDefault();
                if (IdeigMennyi != null)
                    HonnanMennyiség.Text = IdeigMennyi.Mennyiség.ToString();
                else
                    HonnanMennyiség.Text = "0";

                IdeigMennyi = (from a in AdatokKönyvelés
                               where a.Azonosító == SzerszámAzonosító.Text.Trim() && a.Szerszámkönyvszám == Hova.Text.Trim() && a.Státus == false
                               select a).FirstOrDefault();

                if (IdeigMennyi != null)
                    HováMennyiség.Text = IdeigMennyi.Mennyiség.ToString();
                else
                    HováMennyiség.Text = "0";

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

        private void Azonosítóhelyen()
        {
            try
            {
                AdatokKönyvelés = KézKönyvelés.Lista_Adatok(Cmbtelephely.Text.Trim());
                SzerszámAzonosító.Text = "";
                SzerszámAzonosító.Items.Clear();
                SzerszámAzonosító.BeginUpdate();

                List<Adat_Védő_Könyvelés> Idegig = (from a in AdatokKönyvelés
                                                    where a.Szerszámkönyvszám == Honnan.Text.Trim() && a.Státus == false
                                                    select a).ToList();
                foreach (Adat_Védő_Könyvelés Elem in Idegig)
                    SzerszámAzonosító.Items.Add(Elem.Azonosító);

                SzerszámAzonosító.EndUpdate();
                SzerszámAzonosító.Refresh();
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

        private void Tábla_Könyv_fejléc()
        {
            try
            {
                AdatTáblaTábla.Columns.Clear();
                AdatTáblaTábla.Columns.Add("Azonosító");
                AdatTáblaTábla.Columns.Add("Megnevezés");
                AdatTáblaTábla.Columns.Add("Méret");
                AdatTáblaTábla.Columns.Add("Költséghely");
                AdatTáblaTábla.Columns.Add("Mennyiség");
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

        private void Tábla_Könyv_írás()
        {
            try
            {
                Tábla_Könyv.Visible = false;
                Tábla_Könyv.CleanFilterAndSort();
                Tábla_Könyv_fejléc();
                TáblaTáblaTartalom();
                Tábla_Könyv.DataSource = AdatTáblaTábla;
                TáblaTáblaOszlopSzélesség();
                Tábla_Könyv.Visible = true;
                Tábla_Könyv.Refresh();
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

        private void TáblaTáblaOszlopSzélesség()
        {
            Tábla_Könyv.Columns["Azonosító"].Width = 150;
            Tábla_Könyv.Columns["Megnevezés"].Width = 500;
            Tábla_Könyv.Columns["Méret"].Width = 150;
            Tábla_Könyv.Columns["Költséghely"].Width = 150;
            Tábla_Könyv.Columns["Mennyiség"].Width = 150;
        }

        private void TáblaTáblaTartalom()
        {

            if (Honnan.Text.Trim() == "Érkezett" || Honnan.Text.Trim() == "Átvétel")
                Tábla_Könyv_Cikk_Érkezett();
            else
                Tábla_Könyv_Cikk_Könyv();
        }

        private void Tábla_Könyv_Cikk_Érkezett()
        {
            try
            {
                string hely = Alap_hely;
                AdatokCikk = KézCikk.Lista_Adatok(Cmbtelephely.Text.Trim());

                List<Adat_Védő_Cikktörzs> Adatok = (from a in AdatokCikk
                                                    where a.Státus == 0 && a.Megnevezés.ToUpper().Contains(Könyv_SzűrőTXT.Text.Trim().ToUpper())
                                                    select a).ToList();
                AdatTáblaTábla.Clear();
                foreach (Adat_Védő_Cikktörzs rekord in Adatok)
                {
                    DataRow Soradat = AdatTáblaTábla.NewRow();
                    Soradat["Azonosító"] = rekord.Azonosító.Trim();
                    Soradat["Megnevezés"] = rekord.Megnevezés.Trim();
                    Soradat["Méret"] = rekord.Méret.Trim();
                    Soradat["Költséghely"] = rekord.Költséghely.Trim();
                    Soradat["Mennyiség"] = 0;
                    AdatTáblaTábla.Rows.Add(Soradat);
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

        private void Tábla_Könyv_Cikk_Könyv()
        {
            try
            {
                AdatokKönyvelés = KézKönyvelés.Lista_Adatok(Cmbtelephely.Text.Trim());
                AdatokCikk = KézCikk.Lista_Adatok(Cmbtelephely.Text.Trim());

                List<Adat_Védő_Könyvelés> Adatok = (from a in AdatokKönyvelés
                                                    where a.Szerszámkönyvszám == Honnan.Text.Trim() && a.Státus == false
                                                    orderby a.Azonosító
                                                    select a).ToList();
                if (Könyv_SzűrőTXT.Text.Trim() != "")
                    Adatok = (from a in Adatok
                              join b in AdatokCikk on a.Azonosító equals b.Azonosító
                              where b.Megnevezés.ToUpper().Contains(Könyv_SzűrőTXT.Text.Trim().ToUpper())
                              select a).ToList();


                AdatTáblaTábla.Clear();
                foreach (Adat_Védő_Könyvelés rekord in Adatok)
                {
                    Adat_Védő_Cikktörzs Elem = (from a in AdatokCikk
                                                where a.Azonosító == rekord.Azonosító
                                                select a).FirstOrDefault();
                    DataRow Soradat = AdatTáblaTábla.NewRow();
                    Soradat["Azonosító"] = rekord.Azonosító.Trim();
                    Soradat["Mennyiség"] = rekord.Mennyiség;
                    if (Elem != null)
                    {
                        Soradat["Megnevezés"] = Elem.Megnevezés;
                        Soradat["Méret"] = Elem.Méret.Trim();
                        Soradat["Költséghely"] = Elem.Költséghely;
                    }
                    else
                    {
                        Soradat["Megnevezés"] = "";
                        Soradat["Méret"] = "";
                        Soradat["Költséghely"] = "";
                    }
                    AdatTáblaTábla.Rows.Add(Soradat);
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

        private void Rögzít_Click_2(object sender, EventArgs e)
        {
            try
            {

                if (Honnan.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva, hogy honnan könyvelünk.");
                if (Hova.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva, hogy hova könyvelünk.");
                if (Honnan.Text.Trim() == Hova.Text.Trim()) throw new HibásBevittAdat("Önmagába könyvelés nem megengedett.");
                if (Megnevezés.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva könyvelendő elem.");
                if (Mennyiség.Text.Trim() == "") throw new HibásBevittAdat("A mennyiséget meg kell adni.");
                if (!int.TryParse(Mennyiség.Text.Trim(), out int result)) throw new HibásBevittAdat("A mennyiségnek egész számnak kell lennie.");
                if (HováMennyiség.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva, hogy hova könyvelünk.");
                if (HonnanMennyiség.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva, hogy honnankönyvelünk.");
                if (!int.TryParse(HonnanMennyiség.Text.Trim(), out int result1)) throw new HibásBevittAdat("Nincs kiválasztva, hogy honnankönyvelünk.");
                if (Gyáriszám.Text == "") Gyáriszám.Text = "0";
                if (Gyáriszám.Text.Contains(@"/") || Gyáriszám.Text.Contains(@"\"))
                {
                    Gyáriszám.Text = Gyáriszám.Text.Replace(@"\", "-").Replace(@"/", "-");
                    MessageBox.Show(@"A '\', vagy '/' karakterek kicserélsre kerültek '-' karakterre.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (!(Honnan.Text.Trim() == "Érkezett" || Honnan.Text.Trim() == "Átvétel") && int.Parse(HonnanMennyiség.Text) < int.Parse(Mennyiség.Text))
                    throw new HibásBevittAdat("Nem lehet a meglévőnél többet kivenni!");

                // Beraktározás
                if (Honnan.Text.Trim() == "Érkezett" && Hova.Text.Trim() == "Raktár")
                {
                    // nincs raktáron és beérkezett
                    if (Gyáriszám.Text.Trim() != "")
                    {
                        // ha van bizonylatszám akkor könyvel
                        // feltöljük a pdf-t
                        PDF_feltöltés();
                        string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\PDF\{Gyáriszám.Text.Trim()}.pdf";
                        if (!File.Exists(hely)) throw new HibásBevittAdat("Nem lett feltöltve az alapbizonylatról a PDF fájl, ezért nem lehet könyvelni.");

                        Rögzítés_érkezettről();
                        Rögzítés();
                        Naplózás(false);
                        Darabszámok_kiírása();
                        Mennyiség.Text = "";
                        Tábla_Könyv_írás();
                        return;
                    }
                    else
                        throw new HibásBevittAdat("Ennél a mozgásnál a bizonylatszámot meg kell adni !");
                }

                // beraktározás storno
                if (Hova.Text.Trim() == "Érkezett" && Honnan.Text.Trim() == "Raktár")
                {
                    if (Gyáriszám.Text == "0") throw new HibásBevittAdat("Bizonylatszám hiányában nem lehet stronózni a beraktározást!");
                    else
                    {
                        if (MessageBox.Show("Töröljük a bizonylatról készített PDF fájlt? ", "Kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string hova = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\PDF\Törölt_{Gyáriszám.Text.Trim()}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\PDF\{Gyáriszám.Text.Trim()}.pdf";
                            File.Copy(hely, hova);
                            File.Delete(hely);
                        }
                        Érkezettről_storno();
                        Mennyiség.Text = "";
                        Tábla_Könyv_írás();
                        return;
                    }
                }

                // dolgozónak kiadás
                if (Honnan.Text.Trim() == "Raktár" && Hova.Text.Trim() != "Érkezett" &&
                    Hova.Text.Trim() != "Átadás" && Hova.Text.Trim() != "Átvétel" &&
                    Hova.Text.Trim() != "Selejt" && Hova.Text.Trim() != "Selejtre")
                {
                    Rögzítés();
                    Naplózás(false);
                    Készletcsökkentés();
                    Darabszámok_kiírása();
                    Mennyiség.Text = "";
                    Tábla_Könyv_írás();
                    return;
                }

                // dolgozó visszaraktár
                if (Hova.Text.Trim() == "Raktár" && Honnan.Text.Trim() != "Átadás" && Honnan.Text.Trim() != "Átvétel" &&
                    Honnan.Text.Trim() != "Érkezett" &&
                    Honnan.Text.Trim() != "Selejt" && Honnan.Text.Trim() != "Selejtre")
                {
                    Rögzítés();
                    Készletcsökkentés();
                    Naplózás(false);
                    Darabszámok_kiírása();
                    Mennyiség.Text = "";
                    Tábla_Könyv_írás();
                    return;
                }

                // selejt előkészítés
                if (Honnan.Text.Trim() == "Raktár" && Hova.Text.Trim() == "Selejtre")
                {
                    Rögzítés();
                    Készletcsökkentés();
                    Naplózás(false);
                    Darabszámok_kiírása();
                    Mennyiség.Text = "";
                    Tábla_Könyv_írás();
                    return;
                }

                // selejt előkészítés storno
                if (Hova.Text.Trim() == "Raktár" && Honnan.Text.Trim() == "Selejtre")
                {
                    Rögzítés();
                    Készletcsökkentés();
                    Naplózás(false);
                    Darabszámok_kiírása();
                    Mennyiség.Text = "";
                    Tábla_Könyv_írás();
                    return;
                }

                // selejtezés
                if (Honnan.Text.Trim() == "Selejtre" && Hova.Text.Trim() == "Selejt")
                {
                    if (Gyáriszám.Text != "0")
                    {
                        // ha van bizonylatszám akkor könyvel
                        // feltöljük a pdf-t
                        PDF_feltöltés();
                        string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\PDF\{Gyáriszám.Text.Trim()}.pdf";
                        if (!File.Exists(hely)) throw new HibásBevittAdat("Nem lett feltöltve az alapbizonylatról a PDF fájl, ezért nem lehet könyvelni.");

                        Rögzítés_selejtre();
                        Készletcsökkentés();
                        Naplózás(false);
                        Darabszámok_kiírása();
                        Mennyiség.Text = "";
                        Tábla_Könyv_írás();
                        return;
                    }
                    else
                        throw new HibásBevittAdat("Ennél a mozgásnál a bizonylatszámot meg kell adni !");
                }

                // selejtezés storno
                if (Hova.Text.Trim() == "Selejtre" && Honnan.Text.Trim() == "Selejt")
                {
                    if (Gyáriszám.Text == "0")
                        throw new HibásBevittAdat("Bizonylatszám hiányában nem lehet stronózni a selejtezést!");
                    else
                    {
                        if (MessageBox.Show("Töröljük a bizonylatról készített PDF fájlt? ", "Kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string hova = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\PDF\Törölt_{Gyáriszám.Text.Trim()}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\PDF\{Gyáriszám.Text.Trim()}.pdf";
                            File.Copy(hely, hova);
                            File.Delete(hely);
                        }
                        Selejt_storno();
                        Mennyiség.Text = "";
                        Tábla_Könyv_írás();
                        return;
                    }
                }

                // Másik telephelyről érkezik
                if (Honnan.Text.Trim() == "Átvétel" && Hova.Text.Trim() == "Raktár")
                {
                    // nincs raktáron és beérkezett
                    if (Gyáriszám.Text != "0")
                    {
                        // ha van bizonylatszám akkor könyvel
                        // feltöljük a pdf-t
                        PDF_feltöltés();
                        string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\PDF\{Gyáriszám.Text.Trim()}.pdf";
                        if (!File.Exists(hely)) throw new HibásBevittAdat("Nem lett feltöltve az alapbizonylatról a PDF fájl, ezért nem lehet könyvelni.");

                        Rögzítés_érkezettről();
                        Rögzítés();
                        Naplózás(false);
                        Darabszámok_kiírása();
                        Mennyiség.Text = "";
                        Tábla_Könyv_írás();
                        return;
                    }
                    else
                        throw new HibásBevittAdat("Ennél a mozgásnál a bizonylatszámot meg kell adni !");
                }

                // Másik telephelyre könyvelés storno
                if (Hova.Text.Trim() == "Átvétel" && Honnan.Text.Trim() == "Raktár")
                {
                    if (Gyáriszám.Text == "0")
                        throw new HibásBevittAdat("Bizonylatszám hiányában nem lehet stronózni a beraktározást!");
                    else
                    {
                        if (MessageBox.Show("Töröljük a bizonylatról készített PDF fájlt? ", "Kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string hova = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\PDF\Törölt_{Gyáriszám.Text.Trim()}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\PDF\{Gyáriszám.Text.Trim()}.pdf";
                            File.Copy(hely, hova);
                            File.Delete(hely);
                        }
                        Érkezettről_storno();
                        Mennyiség.Text = "";
                        Tábla_Könyv_írás();
                        return;
                    }
                }

                // Másik telephelyre adjuk
                if (Honnan.Text.Trim() == "Raktár" && Hova.Text.Trim() == "Átadás")
                {
                    // nincs raktáron és beérkezett
                    if (Gyáriszám.Text != "0")
                    {
                        // ha van bizonylatszám akkor könyvel
                        // feltöljük a pdf-t
                        PDF_feltöltés();
                        string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\PDF\{Gyáriszám.Text.Trim()}.pdf";
                        if (!File.Exists(hely)) throw new HibásBevittAdat("Nem lett feltöltve az alapbizonylatról a PDF fájl, ezért nem lehet könyvelni.");

                        Rögzítés_átadás();
                        Készletcsökkentés();
                        Naplózás(false);
                        Darabszámok_kiírása();
                        Mennyiség.Text = "";
                        Tábla_Könyv_írás();
                        return;
                    }
                    else
                        throw new HibásBevittAdat("Ennél a mozgásnál a bizonylatszámot meg kell adni !");
                }

                //Másik telephelyre adjuk storno
                if (Hova.Text.Trim() == "Raktár" && Honnan.Text.Trim() == "Átadás")
                {
                    if (Gyáriszám.Text == "0")
                        throw new HibásBevittAdat("Bizonylatszám hiányában nem lehet stronózni a selejtezést!");
                    else
                    {
                        if (MessageBox.Show("Töröljük a bizonylatról készített PDF fájlt? ", "Kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string hova = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\PDF\Törölt_{Gyáriszám.Text.Trim()}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
                            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\PDF\{Gyáriszám.Text.Trim()}.pdf";
                            File.Copy(hely, hova);
                            File.Delete(hely);
                        }
                        Selejt_storno();
                        Mennyiség.Text = "";
                        Tábla_Könyv_írás();
                        return;
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

        private void Könyvelés_Szűrés_Click(object sender, EventArgs e)
        {
            Tábla_Könyv_írás();
        }

        private void Tábla_Könyv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            SzerszámAzonosító.Text = Tábla_Könyv.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
            SzAzonosító_kiíró();
            Darabszámok_kiírása();
            Mennyiség.Focus();
        }

        private void Naplózás(bool állapot)
        {
            try
            {
                Adat_Védő_Napló ADAT = new Adat_Védő_Napló(
                                MyF.Szöveg_Tisztítás(SzerszámAzonosító.Text, 0, 20),
                                MyF.Szöveg_Tisztítás(Honnan.Text.Trim(), 0, 10),
                                MyF.Szöveg_Tisztítás(Hova.Text.Trim(), 0, 10),
                                Mennyiség.Text.ToÉrt_Double(),
                                Gyáriszám.Text.Trim(),
                                Program.PostásNév.Trim(),
                                DateTime.Now,
                                állapot);
                KézNapló.Rögzítés(Cmbtelephely.Text.Trim(), DateTime.Today.Year, ADAT);
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

        private void Rögzítés()
        {
            try
            {
                AdatokKönyvelés = KézKönyvelés.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Védő_Könyvelés ideig = (from a in AdatokKönyvelés
                                             where a.Azonosító == SzerszámAzonosító.Text.Trim() && a.Szerszámkönyvszám == Hova.Text.Trim()
                                             select a).FirstOrDefault();

                Adat_Védő_Könyvelés ADAT = new Adat_Védő_Könyvelés(
                                MyF.Szöveg_Tisztítás(SzerszámAzonosító.Text, 0, 20),
                                MyF.Szöveg_Tisztítás(Hova.Text.Trim(), 0, 10),
                                Mennyiség.Text.ToÉrt_Int() + HováMennyiség.Text.ToÉrt_Int(),
                                MyF.Szöveg_Tisztítás(Gyáriszám.Text.Trim(), 0, 50),
                                DateTime.Now,
                                false);

                if (ideig != null)
                    KézKönyvelés.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                else
                    KézKönyvelés.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);

                MessageBox.Show("Rögzítés megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Rögzítés_érkezettről()
        {
            try
            {
                DateTime dátum = DateTime.Now;
                if (Más_dátum.Checked) dátum = Könyvelési_dátum.Value;

                Adat_Védő_Könyvelés ADAT = new Adat_Védő_Könyvelés(
                                    MyF.Szöveg_Tisztítás(SzerszámAzonosító.Text, 0, 20),
                                    MyF.Szöveg_Tisztítás(Honnan.Text.Trim(), 0, 10),
                                    Mennyiség.Text.ToÉrt_Int(),
                                    Gyáriszám.Text.Trim(),
                                    dátum,
                                    false);
                KézKönyvelés.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);

                MessageBox.Show("Beérkezés Rögzítés megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Készletcsökkentés()
        {
            try
            {
                AdatokKönyvelés = KézKönyvelés.Lista_Adatok(Cmbtelephely.Text.Trim());
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\védőkönyvelés.mdb";

                Adat_Védő_Könyvelés ideig = (from a in AdatokKönyvelés
                                             where a.Azonosító == SzerszámAzonosító.Text.Trim() && a.Szerszámkönyvszám == Honnan.Text.Trim()
                                             select a).FirstOrDefault();

                Adat_Védő_Könyvelés ADAT = new Adat_Védő_Könyvelés(
                                    MyF.Szöveg_Tisztítás(SzerszámAzonosító.Text, 0, 20),
                                    MyF.Szöveg_Tisztítás(Honnan.Text.Trim(), 0, 10),
                                    HonnanMennyiség.Text.ToÉrt_Int() - Mennyiség.Text.ToÉrt_Int(),
                                    "_",
                                    DateTime.Now,
                                    false);

                if (ideig != null)
                {
                    if (HonnanMennyiség.Text.ToÉrt_Int() - Mennyiség.Text.ToÉrt_Int() != 0)
                        KézKönyvelés.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                    else
                        KézKönyvelés.Törlés(Cmbtelephely.Text.Trim(), ADAT);
                }
                MessageBox.Show("Rögzítés megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Érkezettről_storno()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\védőkönyvelés.mdb";
                AdatokKönyvelés = KézKönyvelés.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Védő_Könyvelés ideig = (from a in AdatokKönyvelés
                                             where a.Azonosító == SzerszámAzonosító.Text.Trim() && a.Szerszámkönyvszám == Hova.Text.Trim()
                                             && a.Gyáriszám == Gyáriszám.Text.Trim() && a.Státus == false
                                             select a).FirstOrDefault();

                if (ideig != null)
                {
                    Adat_Védő_Könyvelés ADAT = new Adat_Védő_Könyvelés(
                      MyF.Szöveg_Tisztítás(SzerszámAzonosító.Text, 0, 20),
                      MyF.Szöveg_Tisztítás(Hova.Text.Trim(), 0, 10),
                      ideig.Mennyiség,
                      Gyáriszám.Text.Trim(),
                      DateTime.Today,
                      true);
                    KézKönyvelés.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                    // csökkentjük a készletet
                    Készletcsökkentés();
                    Naplózás(true);
                    Darabszámok_kiírása();
                }
                else
                {
                    MessageBox.Show("Ez a Bizonylatszám hibás, így nem lehet stronózni a beraktározást!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Rögzítés_selejtre()
        {
            try
            {
                DateTime dátum = DateTime.Now;
                if (Más_dátum.Checked) dátum = Könyvelési_dátum.Value;
                Adat_Védő_Könyvelés ADAT = new Adat_Védő_Könyvelés(
                                MyF.Szöveg_Tisztítás(SzerszámAzonosító.Text, 0, 20),
                                MyF.Szöveg_Tisztítás(Hova.Text.Trim(), 0, 10),
                                Mennyiség.Text.ToÉrt_Int(),
                                Gyáriszám.Text.Trim(),
                                dátum,
                                false);
                KézKönyvelés.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);

                MessageBox.Show("Selejtezés Rögzítés megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

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

        private void Rögzítés_átadás()
        {
            try
            {
                DateTime dátum = DateTime.Now;
                if (Más_dátum.Checked) dátum = Könyvelési_dátum.Value;
                Adat_Védő_Könyvelés ADAT = new Adat_Védő_Könyvelés(
                                    MyF.Szöveg_Tisztítás(SzerszámAzonosító.Text, 0, 20),
                                    MyF.Szöveg_Tisztítás(Hova.Text.Trim(), 0, 10),
                                    Mennyiség.Text.ToÉrt_Int(),
                                    Gyáriszám.Text.Trim(),
                                    dátum,
                                    false);
                KézKönyvelés.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);
                MessageBox.Show("Az átadás Rögzítése megtörtént !", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Selejt_storno()
        {
            try
            {
                AdatokKönyvelés = KézKönyvelés.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Védő_Könyvelés ideig = (from a in AdatokKönyvelés
                                             where a.Azonosító == SzerszámAzonosító.Text.Trim() && a.Szerszámkönyvszám == Honnan.Text.Trim() &&
                                             a.Gyáriszám == Gyáriszám.Text.Trim() && a.Státus == false
                                             select a).FirstOrDefault();
                if (ideig != null)
                {
                    Mennyiség.Text = ideig.Mennyiség.ToString();
                    Adat_Védő_Könyvelés ADAT = new Adat_Védő_Könyvelés(
                                        MyF.Szöveg_Tisztítás(SzerszámAzonosító.Text, 0, 20),
                                        MyF.Szöveg_Tisztítás(Honnan.Text.Trim(), 0, 10),
                                        ideig.Mennyiség,
                                        Gyáriszám.Text.Trim(),
                                        DateTime.Today,
                                        true);
                    KézKönyvelés.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                    // vissza kell rakni a selejtre előkészítésbe
                    Rögzítés();
                    Naplózás(true);
                    Darabszámok_kiírása();
                }
                else
                {
                    MessageBox.Show("Ez a Bizonylatszám hibás, így nem lehet stronózni a beraktározást!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Átadás_storno()
        {
            try
            {
                AdatokKönyvelés = KézKönyvelés.Lista_Adatok(Cmbtelephely.Text.Trim());
                Adat_Védő_Könyvelés ideig = (from a in AdatokKönyvelés
                                             where a.Azonosító == SzerszámAzonosító.Text.Trim() && a.Szerszámkönyvszám == Honnan.Text.Trim() &&
                                             a.Gyáriszám == Gyáriszám.Text.Trim() && a.Státus == false
                                             select a).FirstOrDefault();
                if (ideig != null)
                {
                    Adat_Védő_Könyvelés ADAT = new Adat_Védő_Könyvelés(
                                            MyF.Szöveg_Tisztítás(SzerszámAzonosító.Text, 0, 20),
                                            MyF.Szöveg_Tisztítás(Honnan.Text.Trim(), 0, 10),
                                            ideig.Mennyiség,
                                            Gyáriszám.Text.Trim(),
                                            DateTime.Today,
                                            true);
                    KézKönyvelés.Módosítás(Cmbtelephely.Text.Trim(), ADAT);
                    // vissza kell rakni a selejtre előkészítésbe
                    Mennyiség.Text = ideig.Mennyiség.ToString();
                    Rögzítés();
                    Naplózás(true);
                    Darabszámok_kiírása();
                }
                else
                {
                    MessageBox.Show("Ez a Bizonylatszám hibás, így nem lehet stronózni a beraktározást!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        #endregion


        #region PDF feltöltés megjelenítés
        private void PDF_feltöltés()
        {

            if (Gyáriszám.Text.Trim() == "") return;
            string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\Adatok\Védő\PDF\{Gyáriszám.Text.Trim()}.pdf";
            if (!File.Exists(hely))
            {
                // ha nincs akkor feltöltjük
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    Filter = "PDF Files |*.pdf"
                };
                if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Kezelő_Pdf.PdfMegnyitás(PDF_néző, OpenFileDialog1.FileName);

                    File.Copy(OpenFileDialog1.FileName, hely);
                }
            }
        }
        #endregion


        #region Munkáltatói
        private void Lekérd_Munkáltatói_Click(object sender, EventArgs e)
        {
            try
            {
                // beolvassuk a három szervezeti egységet, és a beosztásokat
                Szervezet_Feltöltés();
                Lekérd_Szerszámkönyvszám.Height = 25;
                Lekérd_Munkáltatói_jegyzék();
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


        private void Lekérd_Munkáltatói_jegyzék()
        {
            try
            {
                string munkalap = "Munka1";
                Holtart.Be(20);
                string fájlexc;
                string könyvtár = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                Adat_Védő_Könyv Könyv;
                List<Adat_Védő_Könyvelés> AdatKönyvelés;
                int i;

                foreach (string Elem in Lekérd_Szerszámkönyvszám.CheckedItems)
                {
                    string[] darabol = Elem.Split('=');
                    Lekérd_Tábla.Rows.Clear();
                    Lekérd_Tábla.Columns.Clear();
                    Lekérd_Tábla.Refresh();
                    Lekérd_Tábla.Visible = false;
                    Lekérd_Tábla.ColumnCount = 6;

                    // fejléc elkészítése
                    Lekérd_Tábla.Columns[0].HeaderText = "Azonosító";
                    Lekérd_Tábla.Columns[0].Width = 120;
                    Lekérd_Tábla.Columns[1].HeaderText = "Védelem";
                    Lekérd_Tábla.Columns[1].Width = 150;
                    Lekérd_Tábla.Columns[2].HeaderText = "Kockázat";
                    Lekérd_Tábla.Columns[2].Width = 200;
                    Lekérd_Tábla.Columns[3].HeaderText = "Szabvány";
                    Lekérd_Tábla.Columns[3].Width = 200;
                    Lekérd_Tábla.Columns[4].HeaderText = "Szint";
                    Lekérd_Tábla.Columns[4].Width = 200;
                    Lekérd_Tábla.Columns[5].HeaderText = "Munk_megnevezés";
                    Lekérd_Tábla.Columns[5].Width = 400;

                    AdatKönyvelés = KézKönyvelés.Lista_Adatok(Cmbtelephely.Text.Trim());
                    AdatKönyvelés = AdatKönyvelés.Where(a => a.Szerszámkönyvszám == darabol[0].Trim()).OrderBy(a => a.Azonosító).ToList();

                    foreach (Adat_Védő_Könyvelés rekord in AdatKönyvelés)
                    {
                        Lekérd_Tábla.RowCount++;
                        i = Lekérd_Tábla.RowCount - 1;
                        Lekérd_Tábla.Rows[i].Cells[0].Value = rekord.Azonosító.Trim();

                        Holtart.Lép();
                    }

                    if (Lekérd_Tábla.Rows.Count > 0)
                    {
                        Lekérd_Munkáltatói_jegyzék_folyt();

                    }
                    Lekérd_Tábla.Visible = true;
                    Lekérd_Tábla.Refresh();


                    // kiirt táblából készítünk excel táblát ha a címsoron kívül van tétel
                    if (Lekérd_Tábla.Rows.Count > 0)
                    {
                        // a fájlnév előkészítése
                        fájlexc = könyvtár + $@"\Védő_meghatározás_{darabol[1].Trim()}_{Program.PostásTelephely.Trim()}.xlsx";

                        if (File.Exists(fájlexc))
                            File.Delete(fájlexc);

                        // megnyitjuk az excelt
                        MyE.ExcelLétrehozás();
                        MyE.Oszlopszélesség(munkalap, "a:a", 20);
                        MyE.Oszlopszélesség(munkalap, "b:b", 19);
                        MyE.Oszlopszélesség(munkalap, "c:c", 19);
                        MyE.Oszlopszélesség(munkalap, "D:d", 22);
                        MyE.Oszlopszélesség(munkalap, "E:e", 30);
                        MyE.Kiir(Szervezet1.Trim(), "a1");
                        MyE.Kiir(Szervezet2.Trim(), "a2");
                        MyE.Kiir(Szervezet3.Trim(), "a3");

                        MyE.Kiir("31/VU/2020. 3. számú melléklete", "e1");
                        MyE.Betű("a1:a3", false, false, true);
                        MyE.Egyesít(munkalap, "a5:E5");
                        MyE.Betű("a5", 16);
                        MyE.Betű("a5", false, false, true);
                        MyE.Kiir("Egyéni védőeszközök személyenkénti meghatározása", "a5");

                        MyE.Egyesít(munkalap, "b7:E7");
                        MyE.Egyesít(munkalap, "b9:E9");
                        MyE.Kiir("Munkavállaló neve:", "a7");
                        MyE.Kiir("HR azonosító:", "a9");


                        // beírjuk a szerszámkönyv adatokat
                        AdatokKönyv = KézKönyv.Lista_Adatok(Cmbtelephely.Text.Trim());
                        Könyv = AdatokKönyv.Where(s => s.Szerszámkönyvszám == darabol[0].Trim()).FirstOrDefault();
                        if (Könyv != null && Könyv.Felelős1.Contains("="))
                        {
                            string[] dara = Könyv.Felelős1.Split('=');
                            MyE.Kiir(dara[1].Trim(), "b9");
                            MyE.Kiir(dara[0].Trim(), "b7");
                        }

                        // elkészítjük a fejlécet
                        MyE.Kiir("A védelem iránya", "a11");
                        MyE.Kiir("Kockázatok jellegének megnevezése", "b11");
                        MyE.Kiir("A szükséges egyéni védőeszköz védelmi szintje, szabványszáma", "c11");
                        MyE.Kiir("Védelmi szint meghatározása", "D11");
                        MyE.Kiir("Egyéni védőeszköz megnevezése (minimális követelménye)", "E11");
                        MyE.Sormagasság((11).ToString() + ":" + (11).ToString(), 80);
                        MyE.Sortörésseltöbbsorba((11).ToString() + ":" + (11).ToString());

                        MyE.Sormagasság("11:11", 80);
                        MyE.Igazít_vízszintes("11:11", "közép");

                        // tartalom kiírása
                        for (int sorT = 0; sorT < Lekérd_Tábla.RowCount; sorT++)
                        {
                            for (int oszlop = 1; oszlop <= 5; oszlop++)
                            {
                                MyE.Kiir(Lekérd_Tábla.Rows[sorT].Cells[oszlop].Value.ToString(), MyE.Oszlopnév(oszlop) + (sorT + 12).ToString());
                                MyE.Sormagasság((sorT + 12).ToString() + ":" + (sorT + 12).ToString(), 45);
                                MyE.Sortörésseltöbbsorba((sorT + 12).ToString() + ":" + (sorT + 12).ToString());
                            }
                            Holtart.Lép();
                        }
                        int sor = Lekérd_Tábla.Rows.Count + 11;

                        // keretezünk
                        MyE.Rácsoz("a11:e" + sor.ToString());
                        MyE.Vastagkeret("a11:e11");
                        MyE.Vastagkeret("a11:e" + sor.ToString());
                        sor += 2;
                        MyE.Kiir("Kelt:" + DateTime.Today.ToString("yyyy.MM.dd"), "a" + sor.ToString());
                        sor += 2;

                        MyE.Egyesít(munkalap, "c" + sor.ToString() + ":e" + sor.ToString());
                        MyE.Kiir("Munkáltató aláírása", "c" + sor.ToString());

                        // pontozás az aláírásnak
                        MyE.Egyesít(munkalap, "c" + sor + ":e" + sor);
                        MyE.Pontvonal("c" + sor + ":e" + sor);

                        // nyomtatási beállítások
                        MyE.NyomtatásiTerület_részletes(munkalap, "a1:e" + sor,
                            0.393700787401575, 0.393700787401575,
                            0.393700787401575, 0.393700787401575, oldalmagas: "false");

                        // bezárjuk az Excel-t
                        MyE.Aktív_Cella(munkalap, "A1");
                        MyE.ExcelMentés(fájlexc);
                        MyE.ExcelBezárás();
                    }
                }
                Holtart.Ki();
                MessageBox.Show("A kívánt személyes védőeszköz meghatározás kiírása megtörtént Excelbe!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void Lekérd_Munkáltatói_jegyzék_folyt()
        {
            try
            {

                string hely = Alap_hely;


                // sorbarendezzük a táblát pályaszám szerint

                Lekérd_Tábla.Sort(Lekérd_Tábla.Columns[0], System.ComponentModel.ListSortDirection.Ascending);
                Lekérd_Tábla.Visible = false;

                Kezelő_Védő_Cikktörzs kéz = new Kezelő_Védő_Cikktörzs();
                List<Adat_Védő_Cikktörzs> Adatok = kéz.Lista_Adatok(Cmbtelephely.Text.Trim());

                int hiba = 0;
                int i = 0;
                foreach (Adat_Védő_Cikktörzs rekord in Adatok)
                {

                    if (String.Compare(Lekérd_Tábla.Rows[i].Cells[0].Value.ToStrTrim(), rekord.Azonosító.Trim()) <= 0)
                    {
                        // ha kisebb a táblázatban lévő szám akkor addig növeljük amíg egyenlő nem lesz
                        while (String.Compare(Lekérd_Tábla.Rows[i].Cells[0].Value.ToStrTrim(), rekord.Azonosító.Trim()) < 0)
                        {
                            i += 1;
                            if (i == Lekérd_Tábla.Rows.Count)
                            {
                                hiba = 1;
                                break;
                            }
                        }

                        if (hiba == 1)
                            break;
                        while (String.Compare(Lekérd_Tábla.Rows[i].Cells[0].Value.ToStrTrim(), rekord.Azonosító.Trim()) <= 0)
                        {
                            if (Lekérd_Tábla.Rows[i].Cells[0].Value.ToStrTrim() == rekord.Azonosító.Trim())
                            {
                                // ha egyforma akkor kiírjuk
                                Lekérd_Tábla.Rows[i].Cells[1].Value = rekord.Védelem;
                                Lekérd_Tábla.Rows[i].Cells[2].Value = rekord.Kockázat;
                                Lekérd_Tábla.Rows[i].Cells[3].Value = rekord.Szabvány;
                                Lekérd_Tábla.Rows[i].Cells[4].Value = rekord.Szint;
                                Lekérd_Tábla.Rows[i].Cells[5].Value = rekord.Munk_megnevezés;
                            }
                            i += 1;
                            if (i == Lekérd_Tábla.Rows.Count)
                            {
                                hiba = 1;
                                break;
                            }
                        }
                        if (hiba == 1)
                            break;
                    }
                    Holtart.Lép();
                }
                Lekérd_Tábla.Refresh();
                Lekérd_Tábla.Sort(Lekérd_Tábla.Columns[1], System.ComponentModel.ListSortDirection.Descending);

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


        #region listák feltöltése
        private void Szervezet_Feltöltés()
        {
            try
            {
                List<Adat_Kiegészítő_Jelenlétiív> Adatok = KézJelenléti.Lista_Adatok(Cmbtelephely.Text.Trim());

                Szervezet1 = (from a in Adatok
                              where a.Id == 2
                              select a.Szervezet).FirstOrDefault();
                Szervezet2 = (from a in Adatok
                              where a.Id == 3
                              select a.Szervezet).FirstOrDefault();
                Szervezet3 = (from a in Adatok
                              where a.Id == 4
                              select a.Szervezet).FirstOrDefault();
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
    }
}