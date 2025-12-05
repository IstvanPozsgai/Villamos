using PdfiumViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos
{
    public partial class Ablak_Oktatások
    {
        string VálasztottOktatás = "";
        string ListaNév = "";
        readonly Beállítás_Betű Bebetű = new Beállítás_Betű { Név = "Calibri", Méret = 14 };
        #region Kezelők Listák
        readonly Kezelő_OktatásTábla KézOktatás = new Kezelő_OktatásTábla();
        readonly Kezelő_Oktatás_Napló Kéz_Okt_Nap = new Kezelő_Oktatás_Napló();
        readonly Kezelő_Oktatásrajelöltek Kéz_OktJelölt = new Kezelő_Oktatásrajelöltek();
        readonly Kezelő_Dolgozó_Alap KézDolgozó = new Kezelő_Dolgozó_Alap();
        readonly Kezelő_Kiegészítő_Csoportbeosztás KézCsopBeo = new Kezelő_Kiegészítő_Csoportbeosztás();
        readonly Kezelő_OktatásiSegéd KézSegéd = new Kezelő_OktatásiSegéd();
        readonly Kezelő_Kiegészítő_Jelenlétiív KézJelenléti = new Kezelő_Kiegészítő_Jelenlétiív();

        List<Adat_Oktatásrajelöltek> Adatok_OktJelölt = new List<Adat_Oktatásrajelöltek>();

        #endregion


        #region Alap
        public Ablak_Oktatások()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            try
            {
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                {
                    TelephelyekFeltöltéseÚj();
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }
                else
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }

                Csoportfeltöltés();
                Névfeltöltés();
                Oktatásistátusok();
                TáblaOktatáslistázás();
                PDF_néző.Width = TPOktatásRögz.Width;
                Okatatófeltöltés();
                Tárgyfeltöltés();

                BizDátum.Value = DateTime.Now;
                OktDátum.Value = DateTime.Now;
                Dátumig.Value = DateTime.Now;
                Átütemezés.Value = DateTime.Now;
                Dátumtól.Value = new DateTime(DateTime.Now.Year, 1, 1);
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


        private void AblakOktatások_Load(object sender, EventArgs e)
        {

        }

        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Fülekkitöltése()
        {
            Lecsukja();

            switch (Fülek.SelectedIndex)
            {
                case 0:
                    {
                        TáblaOktatáslistázás();
                        Oktatásistátusok();
                        break;
                    }
                case 1:
                    {
                        Tárgyfeltöltés();
                        Oktataandó_Választó.Checked = true;
                        break;
                    }
                case 2:
                    {
                        Okatatófeltöltés();
                        break;
                    }
                case 3:
                    {
                        Okatatófeltöltés();
                        break;
                    }
                case 4:
                    {
                        Tárgyfeltöltés();
                        break;
                    }
                case 5:
                    {

                        Felcsukja();
                        break;
                    }
            }
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Személy(true))
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség")
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim();
                else
                    Cmbtelephely.Text = Program.PostásTelephely;

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

        private void TelephelyekFeltöltéseÚj()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Adat in GombLathatosagKezelo.Telephelyek(this.Name))
                    Cmbtelephely.Items.Add(Adat.Trim());
                //Alapkönyvtárat beállítjuk 
                if (Cmbtelephely.Items.Cast<string>().Contains(Program.PostásTelephely))
                    Cmbtelephely.Text = Program.PostásTelephely;
                else
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToStrTrim();
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
            int melyikelem;

            // ide kell az összes gombot tenni amit szabályozni akarunk false
            BtnElrendelés.Enabled = false;
            Kötelezésmód.Enabled = false;
            TörölKötelezés.Enabled = false;
            BtnJelenléti.Enabled = false;
            BtnAdminMentés.Enabled = false;

            BtnPDFsave.Enabled = false;
            BtnNaplózásEredményTöröl.Enabled = false;

            melyikelem = 64;
            // módosítás 1 Dolgozók oktatásainak elrendelése
            if (MyF.Vanjoga(melyikelem, 1))
                BtnElrendelés.Enabled = true;
            // módosítás 2 dolgozó oktatás elrendelésének törlése átütemezése
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Kötelezésmód.Enabled = true;
                TörölKötelezés.Enabled = true;
            }
            // módosítás 3 adminisztráció mentés, jelenléti ív készítés, e-mail küldés
            if (MyF.Vanjoga(melyikelem, 3))
            {
                BtnJelenléti.Enabled = true;
                BtnAdminMentés.Enabled = true;

            }

            melyikelem = 65;
            // módosítás 1 Oktatás tényének rögzítése és módosítása
            if (MyF.Vanjoga(melyikelem, 1))
                BtnPDFsave.Enabled = true;
            // módosítás 2 Oktatás tényének törlése
            if (MyF.Vanjoga(melyikelem, 2))
                BtnNaplózásEredményTöröl.Enabled = true;
            // módosítás 3 
            if (MyF.Vanjoga(melyikelem, 3))
            {
                if (Program.Postás_Vezér)
                {
                    // Rögzítteljes.Visible = true;
                }
                else
                {
                    // Rögzítteljes.Visible = false;
                }
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
                e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);
            // Munka kész – dobja ki a keféket
            BlackTextBrush.Dispose();
        }

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\oktatás.html";
                MyF.Megnyitás(hely);
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
            Csoportfeltöltés();
            Névfeltöltés();
            TáblaOktatáslistázás();
            Fülek.SelectedIndex = 0;
        }
        #endregion


        #region Feltöltések
        private void BtnKijelölcsop_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ChkCsoport.Items.Count; i++)
                ChkCsoport.SetItemChecked(i, true);
            Jelöltcsoport();
        }

        private void Btnkijelöltörlés_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ChkCsoport.Items.Count; i++)

                ChkCsoport.SetItemChecked(i, false);
            Jelöltcsoport();
        }

        private void Btnmindkijelöl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ChkDolgozónév.Items.Count; i++)
                ChkDolgozónév.SetItemChecked(i, true);
        }

        private void Btnkijelöléstöröl_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ChkDolgozónév.Items.Count; i++)
                ChkDolgozónév.SetItemChecked(i, false);
        }

        private void BtnKijelölésátjelöl_Click(object sender, EventArgs e)
        {
            Jelöltcsoport();
        }

        private void Jelöltcsoport()
        {
            try
            {
                ChkDolgozónév.Items.Clear();
                ChkDolgozónév.BeginUpdate();

                List<Adat_Dolgozó_Alap> AdatokÖ = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());

                for (int j = 0; j < ChkCsoport.CheckedItems.Count; j++)
                {
                    List<Adat_Dolgozó_Alap> Adatok = AdatokÖ.Where(a => a.Kilépésiidő == new DateTime(1900, 1, 1)).ToList();
                    //csoporttagokat kiválogatja
                    if (ChkCsoport.CheckedItems[j].ToStrTrim() != "Összes")
                        Adatok = Adatok.Where(a => a.Csoport == ChkCsoport.CheckedItems[j].ToStrTrim()).ToList();

                    foreach (Adat_Dolgozó_Alap rekord in Adatok)
                        ChkDolgozónév.Items.Add($"{rekord.DolgozóNév.Trim()} = {rekord.Dolgozószám.Trim()}");
                }
                ChkDolgozónév.EndUpdate();
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

        private void Csoportfeltöltés()
        {
            ChkCsoport.Items.Clear();
            ChkCsoport.BeginUpdate();
            List<Adat_Kiegészítő_Csoportbeosztás> Adatok = KézCsopBeo.Lista_Adatok(Cmbtelephely.Text.Trim());
            foreach (Adat_Kiegészítő_Csoportbeosztás rekord in Adatok)
                ChkCsoport.Items.Add(rekord.Csoportbeosztás);
            ChkCsoport.EndUpdate();
        }

        private void Névfeltöltés()
        {
            try
            {
                ChkDolgozónév.Items.Clear();
                ChkDolgozónév.BeginUpdate();
                List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim(), true);
                foreach (Adat_Dolgozó_Alap rekord in Adatok)
                    ChkDolgozónév.Items.Add($"{rekord.DolgozóNév.Trim()}={rekord.Dolgozószám.Trim()}");

                ChkDolgozónév.EndUpdate();
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

        private void Felcsukja()
        {
            Fülek.Top = 10;
            Fülek.Height = Size.Height - 60;
            BtnPdfNyit.Visible = false;
        }

        private void Lecsukja()
        {
            Fülek.Top = TáblaOktatás.Height + TáblaOktatás.Top + 10;
            Fülek.Height = 220;
            BtnPdfNyit.Visible = true;
            AdminFel.Visible = true;
            AdminLe.Visible = false;
        }

        private void Tárgyfeltöltés()
        {
            try
            {
                Holtart.Be();
                Cmboktatásrögz.Items.Clear();
                Cmboktatásrögz.Items.Add("");
                CMBoktatástárgya.Items.Clear();
                CMBoktatástárgya.Items.Add("");
                Adminoktatástárgya.Items.Clear();
                Adminoktatástárgya.Items.Add("");

                List<Adat_OktatásTábla> Adatok = KézOktatás.Lista_Adatok().Where(a => a.Telephely == Cmbtelephely.Text.Trim()).OrderBy(a => a.Listázásisorrend).ToList();
                foreach (Adat_OktatásTábla rekord in Adatok)
                {

                    Cmboktatásrögz.Items.Add($"{rekord.IDoktatás} - {rekord.Téma.Trim()}");
                    Adminoktatástárgya.Items.Add($"{rekord.IDoktatás} - {rekord.Téma.Trim()}");
                    Holtart.Lép();
                }
                List<Adat_OktatásTábla> SzűrtAdatok = (from a in Adatok
                                                       where a.Státus == "Érvényes"
                                                       select a).ToList();

                foreach (Adat_OktatásTábla rekord in SzűrtAdatok)
                    CMBoktatástárgya.Items.Add($"{rekord.IDoktatás} - {rekord.Téma.Trim()}");

                Cmboktatásrögz.Refresh();
                CMBoktatástárgya.Refresh();
                Adminoktatástárgya.Refresh();
                if (VálasztottOktatás.Trim() != "") CMBoktatástárgya.Text = VálasztottOktatás;
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

        private void BtnExcelkimenet_Click(object sender, EventArgs e)
        {
            try
            {
                if (TáblaOktatás.Rows.Count <= 0) return;
                string fájlexc;
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Oktatás_{Program.PostásNév.Trim()} - {DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.DataGridViewToXML(fájlexc, TáblaOktatás);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyF.Megnyitás(fájlexc);
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


        #region Tábla
        private void TáblaOktatás_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (TáblaOktatás.RowCount > 0)
                {
                    // egész sor színezése ha törölt
                    if (Chkelrendelés.Checked)
                    {
                        foreach (DataGridViewRow row in TáblaOktatás.Rows)
                        {
                            if (row.Cells[5].Value.ToString().Trim() == "Törölt")
                            {
                                row.DefaultCellStyle.ForeColor = Color.White;
                                row.DefaultCellStyle.BackColor = Color.IndianRed;
                                row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
                            }
                        }
                    }
                    // egész sor színezése ha törölt
                    if (CHkNapló.Checked)
                    {
                        if (TáblaOktatás.ColumnCount - 1 > 12)
                        {
                            foreach (DataGridViewRow row in TáblaOktatás.Rows)
                            {
                                if ((row.Cells[12].Value.ToString().Trim() == "Törölt").ToÉrt_Bool())
                                {
                                    row.DefaultCellStyle.ForeColor = Color.White;
                                    row.DefaultCellStyle.BackColor = Color.IndianRed;
                                    row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
                                }
                            }
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

        private void TáblaOktatás_SelectionChanged(object sender, EventArgs e)
        {
            if (!CHKpdfvan.Checked)
            {
                if (TáblaOktatás.SelectedRows.Count != 0 && Chkoktat.Checked)
                {
                    // ha oktatandóra kattint akkor összeállítja a fájl nevét
                    if (TáblaOktatás.SelectedRows.Count == 1)
                    {
                        // ha egy van kijelölve
                        Txtmentett.Text = $"{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[0].Value}_";
                        Txtmentett.Text += $"{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[2].Value}_";
                        Txtmentett.Text += $"{Cmbtelephely.Text}_";
                        Txtmentett.Text += $"{BizDátum.Value:yyyyMMdd}.pdf";
                    }
                    else
                    {
                        // ha több sor van kijelölve
                        int volt = 0;
                        int i = 1;
                        while (volt == 0)
                        {
                            Txtmentett.Text = $"Csop{i}_";
                            Txtmentett.Text += $"{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[2].Value}_";
                            Txtmentett.Text += $"{Cmbtelephely.Text}_";
                            Txtmentett.Text += $"{BizDátum.Value:yyyyMMdd}.pdf";
                            string hely = $@"{Application.StartupPath}\Főmérnökség\Oktatás\{Cmbtelephely.Text}\{Txtmentett.Text.Trim()}";
                            if (File.Exists(hely))
                                i++;
                            else
                                volt = 1;
                        }
                    }
                }
            }
            if (TáblaOktatás.SelectedRows.Count == 1 && CHkNapló.Checked)
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\Oktatás\{Cmbtelephely.Text}\{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[8].Value}";
                Pdf_Megjelenítés(hely);
            }
        }
        #endregion


        #region Pdflapfül
        private void BtnPdfMegnyitás_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
                Txtmegnyitott.Text = "";
                Txtmentett.Text = "";
                OpenFileDialog1.Filter = "PDF Files |*.pdf";
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                {
                    Kezelő_Pdf.PdfMegnyitás(PDF_néző, OpenFileDialog1.FileName);

                    Txtmegnyitott.Text = OpenFileDialog1.FileName;
                    CHKpdfvan.Checked = false;
                    Fülek.SelectedIndex = 5;
                    Felcsukja();
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

        private void Pdf_Megjelenítés(string hely)
        {
            try
            {
                if (!File.Exists(hely)) return;
                Kezelő_Pdf.PdfMegnyitás(PDF_néző, hely);
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

        private void BtnPdfNyit_Click(object sender, EventArgs e)
        {
            Felcsukja();
        }

        private void BtnPdfCsuk_Click(object sender, EventArgs e)
        {
            Lecsukja();
        }
        #endregion


        #region Elrendelés
        private void Oktatásistátusok()
        {
            try
            {
                // töröljük az előzményeket
                CMBStátus.Items.Clear();
                CmbKategória.Items.Clear();
                CmbGyakoriság.Items.Clear();

                // üres 
                CMBStátus.Items.Add("");
                CmbKategória.Items.Add("");
                CmbGyakoriság.Items.Add("");
                // feltöltjök amit kell
                // kategória
                List<Adat_OktatásTábla> Adatok = KézOktatás.Lista_Adatok().Where(a => a.Státus == "Érvényes").OrderBy(a => a.Kategória).ToList();

                List<string> Adatokszűrt = Adatok.Select(a => a.Kategória).Distinct().ToList();
                foreach (string rekord in Adatokszűrt)
                    CmbKategória.Items.Add(rekord);

                // gyakoriság
                Adatok = Adatok.OrderBy(a => a.Gyakoriság).ToList();
                Adatokszűrt = Adatok.Select(a => a.Gyakoriság).Distinct().ToList();
                foreach (string rekord in Adatokszűrt)
                    CmbGyakoriság.Items.Add(rekord);

                // státusok 
                CMBStátus.Items.Add("Érvényes");
                CMBStátus.Items.Add("Törölt");
                CMBStátus.Text = "Érvényes";
                // számonkérés
                CMBszámon.Items.Clear();
                CMBszámon.Items.Add("0 -nem volt");
                CMBszámon.Items.Add("1 -megfelelt");
                CMBszámon.Items.Add("2 -nem felelt meg");
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

        private void Btnfrissít_Click_1(object sender, EventArgs e)
        {
            CHkNapló.Checked = false;
            Chkoktat.Checked = false;
            Chkelrendelés.Checked = false;
            TáblaOktatáslistázás();
        }

        private void TáblaOktatáslistázás()
        {
            try
            {
                List<Adat_OktatásTábla> Adatok = KézOktatás.Lista_Adatok().Where(a => a.Telephely == Cmbtelephely.Text.Trim()).OrderBy(a => a.Listázásisorrend).ToList();

                if (CmbGyakoriság.Text.Trim() != "") Adatok = Adatok.Where(a => a.Gyakoriság == CmbGyakoriság.Text.Trim()).ToList();
                if (CMBStátus.Text.Trim() != "") Adatok = Adatok.Where(a => a.Státus == CMBStátus.Text.Trim()).ToList();
                if (CmbKategória.Text.Trim() != "") Adatok = Adatok.Where(a => a.Kategória == CmbKategória.Text.Trim()).ToList();

                Chkoktat.Checked = false;
                Holtart.Be();

                TáblaOktatás.Rows.Clear();
                TáblaOktatás.Columns.Clear();
                ListaNév = "Elrendelés";
                TáblaOktatás.Refresh();
                TáblaOktatás.Visible = false;
                TáblaOktatás.ColumnCount = 9;
                TáblaOktatás.RowCount = 0;
                // ' fejléc elkészítése
                TáblaOktatás.Columns[0].HeaderText = "Sor- szám";
                TáblaOktatás.Columns[0].Width = 50;
                TáblaOktatás.Columns[1].HeaderText = "Oktatás témája";
                TáblaOktatás.Columns[1].Width = 520;
                TáblaOktatás.Columns[2].HeaderText = "Kategória";
                TáblaOktatás.Columns[2].Width = 120;
                TáblaOktatás.Columns[3].HeaderText = "Gyakoriság";
                TáblaOktatás.Columns[3].Width = 110;
                TáblaOktatás.Columns[4].HeaderText = "Gyakoriság hónap";
                TáblaOktatás.Columns[4].Width = 100;
                TáblaOktatás.Columns[5].HeaderText = "Státus";
                TáblaOktatás.Columns[5].Width = 100;
                TáblaOktatás.Columns[6].HeaderText = "Dátum";
                TáblaOktatás.Columns[6].Width = 110;
                TáblaOktatás.Columns[7].HeaderText = "Telephely";
                TáblaOktatás.Columns[7].Width = 120;
                TáblaOktatás.Columns[8].HeaderText = "Listázási sorrend";
                TáblaOktatás.Columns[8].Width = 70;

                foreach (Adat_OktatásTábla rekord in Adatok)
                {
                    TáblaOktatás.RowCount++;
                    int i = TáblaOktatás.RowCount - 1;
                    TáblaOktatás.Rows[i].Cells[0].Value = rekord.IDoktatás;
                    TáblaOktatás.Rows[i].Cells[1].Value = rekord.Téma.Trim();
                    TáblaOktatás.Rows[i].Cells[2].Value = rekord.Kategória.Trim();
                    TáblaOktatás.Rows[i].Cells[3].Value = rekord.Gyakoriság.Trim();
                    TáblaOktatás.Rows[i].Cells[4].Value = rekord.Ismétlődés;
                    TáblaOktatás.Rows[i].Cells[5].Value = rekord.Státus.Trim();
                    TáblaOktatás.Rows[i].Cells[6].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                    TáblaOktatás.Rows[i].Cells[7].Value = rekord.Telephely.Trim();
                    TáblaOktatás.Rows[i].Cells[8].Value = rekord.Listázásisorrend;
                    Holtart.Lép();
                }
                Chkelrendelés.Checked = true;
                TáblaOktatás.Visible = true;
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

        private void BtnElrendelés_Click(object sender, EventArgs e)
        {
            try
            {
                Holtart.Be();

                if (ListaNév != "Elrendelés") throw new HibásBevittAdat("Nem megfelelő listázott tartalom.");    //Ha nem az van kilistázva, akkor kilép
                if (TáblaOktatás.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve a táblázatban sor!");        // ha nincs kijelölve egy sor sem akkor kilép
                if (ChkDolgozónév.CheckedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve dolgozó!");       // ha nincs kijelölve egy dolgozó sem akkor kilép 

                // dolgozóneveket pörgetjük végig
                Adatok_OktJelölt = Kéz_OktJelölt.Lista_Adatok();

                List<Adat_Oktatásrajelöltek> AdatokR = new List<Adat_Oktatásrajelöltek>();
                for (int i = 0; i < ChkDolgozónév.CheckedItems.Count; i++)
                {
                    // a tábla adatait pörgetjük végig
                    for (int j = 0; j < TáblaOktatás.SelectedRows.Count; j++)
                    {
                        if (long.TryParse(TáblaOktatás.SelectedRows[j].Cells[0].Value.ToString(), out long Idoktatás))
                        {
                            string[] darabol = ChkDolgozónév.CheckedItems[i].ToString().Split('=');
                            // megnézzük, hogy van-e már rögzítve
                            Adat_Oktatásrajelöltek rekord = (from r in Adatok_OktJelölt
                                                             where r.HRazonosító == darabol[1].Trim() &&
                                                                   r.Telephely == Cmbtelephely.Text.Trim() &&
                                                                   r.IDoktatás == Idoktatás &&
                                                                   r.Státus == 0
                                                             select r).FirstOrDefault();
                            if (rekord == null)
                            {
                                // rögzítjük az adatokat
                                Adat_Oktatásrajelöltek ADAT = new Adat_Oktatásrajelöltek(
                                                            darabol[1].Trim(),
                                                            Idoktatás,
                                                            OktDátum.Value,
                                                            0,
                                                            Cmbtelephely.Text.Trim());
                                AdatokR.Add(ADAT);
                            }
                        }
                        Holtart.Lép();
                    }
                }
                Holtart.Ki();
                if (AdatokR.Count > 0)
                {
                    Kéz_OktJelölt.Rögzítés(AdatokR);
                    MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Oktatandó
        private void BtnOktatásFrissít_Click(object sender, EventArgs e)
        {
            FrissítOktatás();
        }

        private void FrissítOktatás()
        {
            CHkNapló.Checked = false;
            Chkoktat.Checked = false;
            Chkelrendelés.Checked = false;
            if (Oktataandó_Választó.Checked)
                Oktatáslistázáskötelezés(1);
            else
                Oktatáslistázáskötelezés(2);

        }

        private void Oktatáslistázáskötelezés(int Melyik)
        {
            try
            {


                // ha nincs kijelölve egy dolgozó sem akkor kilép
                if (ChkDolgozónév.CheckedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve dolgozó!");

                List<Adat_Dolgozó_Alap> AdatokDolgozó = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_OktatásTábla> AdatokOktatás = KézOktatás.Lista_Adatok();
                List<Adat_Oktatásrajelöltek> AdatokJelölt = Kéz_OktJelölt.Lista_Adatok();

                Chkoktat.Checked = true;

                TáblaOktatás.Rows.Clear();
                TáblaOktatás.Columns.Clear();

                ListaNév = "Kötelezés";
                TáblaOktatás.Refresh();
                TáblaOktatás.Visible = false;
                TáblaOktatás.ColumnCount = 8;
                TáblaOktatás.RowCount = 0;
                // ' fejléc elkészítése
                TáblaOktatás.Columns[0].HeaderText = "HR azonosító";
                TáblaOktatás.Columns[0].Width = 115;
                TáblaOktatás.Columns[1].HeaderText = "Név";
                TáblaOktatás.Columns[1].Width = 300;
                TáblaOktatás.Columns[2].HeaderText = "IDoktatás";
                TáblaOktatás.Columns[2].Width = 80;
                TáblaOktatás.Columns[3].HeaderText = "Oktatás Témája";
                TáblaOktatás.Columns[3].Width = 300;
                TáblaOktatás.Columns[4].HeaderText = "Elrendelés dátuma";
                TáblaOktatás.Columns[4].Width = 110;
                TáblaOktatás.Columns[5].HeaderText = "Telephely";
                TáblaOktatás.Columns[5].Width = 120;
                TáblaOktatás.Columns[6].HeaderText = "Státus";
                TáblaOktatás.Columns[6].Width = 100;
                TáblaOktatás.Columns[7].HeaderText = "ismétlődés";
                TáblaOktatás.Columns[7].Width = 100;

                List<Adat_Oktatásrajelöltek> Adatok;
                long oktatásid = 0;
                if (CMBoktatástárgya.Text.Trim() != "") oktatásid = CMBoktatástárgya.Text.Substring(0, CMBoktatástárgya.Text.IndexOf("-")).ToÉrt_Long();
                switch (Melyik)
                {
                    case 1:

                        if (oktatásid != 0)
                            Adatok = (from a in AdatokJelölt
                                      where a.Mikortól < Lejáródátum.Value && a.Telephely == Cmbtelephely.Text.Trim()
                                      && a.IDoktatás == oktatásid
                                      && a.Státus == 0
                                      orderby a.HRazonosító
                                      select a).ToList();
                        else
                            Adatok = (from a in AdatokJelölt
                                      where a.Mikortól < Lejáródátum.Value && a.Telephely == Cmbtelephely.Text.Trim()
                                      && a.Státus == 0
                                      orderby a.HRazonosító
                                      select a).ToList();

                        break;
                    case 2:
                        if (oktatásid != 0)
                            Adatok = (from a in AdatokJelölt
                                      where a.Telephely == Cmbtelephely.Text.Trim()
                                      && a.IDoktatás == oktatásid && a.Státus < 2
                                      orderby a.HRazonosító
                                      select a).ToList();
                        else
                            Adatok = (from a in AdatokJelölt
                                      where a.Telephely == Cmbtelephely.Text.Trim() && a.Státus < 2
                                      orderby a.HRazonosító
                                      select a).ToList();
                        break;
                    default:
                        Adatok = AdatokJelölt;
                        break;
                }

                Holtart.Be(ChkDolgozónév.CheckedItems.Count + 1);
                for (int i = 0; i < ChkDolgozónév.CheckedItems.Count; i++)
                {
                    string[] tomb = ChkDolgozónév.CheckedItems[i].ToString().Split('=');
                    List<Adat_Oktatásrajelöltek> Rekordok = (from ab in Adatok
                                                             where ab.HRazonosító.Trim() == tomb[1].Trim()
                                                             select ab).ToList();
                    if (Rekordok != null)
                    {
                        foreach (Adat_Oktatásrajelöltek Elem in Rekordok)
                        {
                            int ii = TáblaOktatás.Rows.Add();
                            TáblaOktatás.Rows[ii].Cells[0].Value = Elem.HRazonosító;
                            TáblaOktatás.Rows[ii].Cells[2].Value = Elem.IDoktatás;
                            TáblaOktatás.Rows[ii].Cells[4].Value = Elem.Mikortól.ToString("yyyy.MM.dd");
                            TáblaOktatás.Rows[ii].Cells[5].Value = Elem.Telephely;
                            TáblaOktatás.Rows[ii].Cells[6].Value = Elem.Státus;

                            string DolgozóNév = (from a in AdatokDolgozó
                                                 where a.Dolgozószám == Elem.HRazonosító
                                                 select a.DolgozóNév).FirstOrDefault() ?? "";
                            TáblaOktatás.Rows[ii].Cells[1].Value = DolgozóNév;

                            Adat_OktatásTábla rekordszer = (from a in AdatokOktatás
                                                            where a.IDoktatás == Elem.IDoktatás
                                                            select a).FirstOrDefault();
                            if (rekordszer != null)
                            {
                                TáblaOktatás.Rows[ii].Cells[3].Value = rekordszer.Téma;
                                TáblaOktatás.Rows[ii].Cells[7].Value = rekordszer.Ismétlődés;
                            }
                            Holtart.Lép();
                        }
                    }
                }
                TáblaOktatás.Visible = true;
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

        private void Kötelezésmód_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListaNév != "Kötelezés") throw new HibásBevittAdat("Hibás listázott tartalom.");
                if (TáblaOktatás.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve érvényes sor a táblázatban.");

                if (TáblaOktatás.SelectedRows.Count != 0 && Chkoktat.Checked)
                {
                    List<Adat_Oktatásrajelöltek> AdatokM = new List<Adat_Oktatásrajelöltek>();
                    foreach (DataGridViewRow row in TáblaOktatás.SelectedRows)
                    {
                        Adat_Oktatásrajelöltek ADAT = new Adat_Oktatásrajelöltek(
                                               row.Cells[0].Value.ToStrTrim(),
                                               row.Cells[2].Value.ToÉrt_Long(),
                                               Átütemezés.Value,
                                               0,
                                               Cmbtelephely.Text.Trim());
                        AdatokM.Add(ADAT);
                    }
                    if (AdatokM.Count > 0)
                    {
                        Kéz_OktJelölt.Módosítás_Ütem(AdatokM);
                        MessageBox.Show("Az adatok módosítása megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                Oktatáslistázáskötelezés(1);

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

        private void BtnOktatásEredményTöröl_Click(object sender, EventArgs e)
        {
            TáblaOktatás.ClearSelection();
            Txtmentett.Text = "";
        }

        private void TörölKötelezés_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListaNév != "Kötelezés") throw new HibásBevittAdat("Hibás listázott tartalom.");
                if (TáblaOktatás.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve érvényes sor a táblázatban.");

                if (TáblaOktatás.SelectedRows.Count != 0 && Chkoktat.Checked)
                {
                    List<Adat_Oktatásrajelöltek> AdatokM = new List<Adat_Oktatásrajelöltek>();
                    foreach (DataGridViewRow row in TáblaOktatás.SelectedRows)
                    {
                        Adat_Oktatásrajelöltek ADAT = new Adat_Oktatásrajelöltek(
                                             row.Cells[0].Value.ToStrTrim(),
                                             row.Cells[2].Value.ToÉrt_Long(),
                                             new DateTime(1900, 1, 1),
                                             2,
                                             Cmbtelephely.Text.Trim());
                        AdatokM.Add(ADAT);
                    }
                    if (AdatokM.Count > 0)
                    {
                        Kéz_OktJelölt.Módosítás_Ütem(AdatokM);
                        MessageBox.Show("Az adatok törlése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                Oktatáslistázáskötelezés(1);
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

        private void CMBoktatástárgya_SelectedIndexChanged(object sender, EventArgs e)
        {
            Adminoktatástárgya.Text = CMBoktatástárgya.Text;
            VálasztottOktatás = CMBoktatástárgya.Text;
        }

        private void Oktataandó_Választó_CheckedChanged(object sender, EventArgs e)
        {
            Oktatás_Panel.Visible = Oktataandó_Választó.Checked;
            FrissítOktatás();
        }
        #endregion


        #region adminisztráció
        private void Button10_Click(object sender, EventArgs e)
        {
            Lecsukja();
            AdminFel.Visible = true;
            AdminLe.Visible = false;
        }

        private void BtnLapFül_Click(object sender, EventArgs e)
        {
            Felcsukja();
            AdminLe.Visible = true;
            AdminFel.Visible = false;
        }

        private void ADMINürítés()
        {
            Adminoktatásdátuma.Value = DateTime.Now;
            AdminOktatásoka.Text = "";
            AdminOktató.Text = "";
            AdminOktatómunkaköre.Text = "";
            Adminhelyszín.Text = "";
            Admintartam.Text = "";
            Admintematika.Text = "";
            Egyébszöveg.Text = "";
            Txtemail.Text = "";
        }

        private void Adminoktatástárgya_SelectedIndexChanged(object sender, EventArgs e)
        {
            Kiírjaadmin();
        }

        private void Kiírjaadmin()
        {
            try
            {
                if (Adminoktatástárgya.Text.Trim() == "") return;
                ADMINürítés();

                List<Adat_OktatásiSegéd> Adatok = KézSegéd.Lista_Adatok();
                long IdOktatás = Adminoktatástárgya.Text.Substring(0, Adminoktatástárgya.Text.IndexOf("-")).ToÉrt_Long();
                Adat_OktatásiSegéd rekord = (from a in Adatok
                                             where a.Telephely == Cmbtelephely.Text
                                             && a.IDoktatás == IdOktatás
                                             select a).FirstOrDefault();
                if (rekord != null)
                {
                    AdminOktatásoka.Text = rekord.Oktatásoka.Trim();
                    AdminOktató.Text = rekord.Oktató.Trim();
                    AdminOktatómunkaköre.Text = rekord.Oktatóbeosztása.Trim();
                    Adminhelyszín.Text = rekord.Oktatáshelye.Trim();
                    Admintartam.Text = rekord.Oktatásidőtartama.ToString().Trim();
                    Admintematika.Text = rekord.Oktatástárgya.Replace('°', '"'); ;  // visszacseréli a °-t "-ra
                    Egyébszöveg.Text = rekord.Egyébszöveg.Replace('~', '"'); // visszacseréli a ~-t "-ra
                    Txtemail.Text = rekord.Email.Trim();
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

        private void JelenlétiÍVKészítés()
        {
            try
            {
                Holtart.Be();
                if (Chkelrendelés.Checked) return;
                // E-mail küldés előkészítése
                TextBox1.Text = "";
                TextBox2.Text = "";

                // ha nincs kijelölve senki akkor kilép
                if (TáblaOktatás.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy dolgozó sem.");

                int hányember = TáblaOktatás.SelectedRows.Count;

                string fájlexc = $@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\Jelenléti_Oktatáshoz-{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddhhmmss}.xlsx";

                // formázáshoz
                string munkalap = "Munka1";
                MyX.ExcelLétrehozás(munkalap);

                MyX.Munkalap_betű(munkalap, Bebetű);
                MyX.Oszlopszélesség(munkalap, "a:a", 6);
                MyX.Oszlopszélesség(munkalap, "b:b", 15);
                MyX.Oszlopszélesség(munkalap, "c:c", 26);
                MyX.Oszlopszélesség(munkalap, "d:d", 36);
                MyX.Oszlopszélesség(munkalap, "e:e", 13);
                MyX.Oszlopszélesség(munkalap, "f:f", 20);
                MyX.Oszlopszélesség(munkalap, "g:g", 15);

                for (int j = 1; j <= 10; j++)
                {
                    MyX.Egyesít(munkalap, "A" + j.ToString() + ":b" + j.ToString());
                    MyX.Egyesít(munkalap, "c" + j.ToString() + ":g" + j.ToString());
                    Holtart.Lép();
                }

                MyX.Kiir("Szervezet:", "a1");
                string eredmény = "";

                List<Adat_Kiegészítő_Jelenlétiív> Adatok = KézJelenléti.Lista_Adatok(Cmbtelephely.Text.Trim());
                if (Adatok != null)
                {
                    string Szervezet = (from a in Adatok
                                        where a.Id == 2
                                        select a.Szervezet).FirstOrDefault();
                    if (Szervezet != null)
                        eredmény += $"{Szervezet.Trim()}\n";
                    Szervezet = (from a in Adatok
                                 where a.Id == 3
                                 select a.Szervezet).FirstOrDefault();
                    if (Szervezet != null)
                        eredmény += $"{Szervezet.Trim()}\n";
                    Szervezet = (from a in Adatok
                                 where a.Id == 4
                                 select a.Szervezet).FirstOrDefault();
                    if (Szervezet != null)
                        eredmény += $"{Szervezet.Trim()}";
                }
                Holtart.Lép();
                MyX.Kiir(eredmény, "c1");
                MyX.Sormagasság(munkalap, "c1", 55);
                MyX.Sortörésseltöbbsorba(munkalap, "c1", true);

                MyX.Kiir("Hely, helyszíne:", "a2");
                MyX.Kiir(Adminhelyszín.Text, "c2");
                MyX.Sormagasság(munkalap, "2:2", 20);

                MyX.Kiir("Tárgy:", "a3");
                MyX.Kiir(Adminoktatástárgya.Text.Substring(Adminoktatástárgya.Text.IndexOf("-") + 1), "c3");
                int sormagasság = Hánysor(Adminoktatástárgya.Text) * 20;
                if (sormagasság > 408) sormagasság = 408;
                MyX.Sormagasság(munkalap, "3:3", sormagasság);

                MyX.Kiir("Időpont:", "a4");
                MyX.Kiir(Adminoktatásdátuma.Value.ToString("yyyy.MM.dd"), "c4");
                MyX.Sormagasság(munkalap, "4:4", 20);

                MyX.Kiir("Időtartam:", "a5");
                MyX.Kiir(Admintartam.Text + " óra", "c5");
                MyX.Sormagasság(munkalap, "5:5", 20);

                MyX.Kiir("Ok:", "a6");
                MyX.Kiir(AdminOktatásoka.Text, "c6");
                sormagasság = Hánysor(AdminOktatásoka.Text) * 20;
                if (sormagasság > 408) sormagasság = 408;
                MyX.Sormagasság(munkalap, "6:6", sormagasság);

                MyX.Kiir("Leírás:", "a7");
                MyX.Kiir(Admintematika.Text, "c7");
                MyX.Sortörésseltöbbsorba(munkalap, "C7", true);
                sormagasság = Hánysor(Admintematika.Text) * 19;
                if (sormagasság > 408) sormagasság = 408;
                MyX.Sormagasság(munkalap, "c7", sormagasság);

                MyX.Kiir("Előadó:", "a8");
                MyX.Kiir(AdminOktató.Text, "c8");
                MyX.Sormagasság(munkalap, "8:8", 20);

                MyX.Kiir("Munkaköre:", "a9");
                MyX.Kiir(AdminOktatómunkaköre.Text, "c9");
                MyX.Sormagasság(munkalap, "9:9", 20);

                MyX.Kiir("Aláírása:", "a10");
                MyX.Sormagasság(munkalap, "10:10", 40);


                // E-MAIL SZÖVEGÉT előkészítjük
                TextBox1.Text = $"A felnőttképzés adatszolgáltatási rendszeréhez (FAR) szükséges adatok\n\r";
                TextBox1.Text += $"Képzés megnevezése: {Adminoktatástárgya.Text.Substring(Adminoktatástárgya.Text.IndexOf("-") + 1)}\n\r";
                TextBox1.Text += $"Képzés jellege: {AdminOktatásoka.Text.Trim()}\n\r";
                TextBox1.Text += $"Képzés helye: {eredmény}\n\r";
                TextBox1.Text += $"Képzés helyének címe: {Adminhelyszín.Text.Trim()}\n\r";
                TextBox1.Text += $"Képzés óraszáma: {Admintartam.Text.Trim()}\n\r";
                TextBox1.Text += $"Első Képzési nap: {Adminoktatásdátuma.Value:yyyy.MM.dd}\n\r";
                TextBox1.Text += $"Befejezés tervezett időpontja: {Adminoktatásdátuma.Value:yyyy.MM.dd}\n\r\n\r";
                Holtart.Lép();
                // aláírás vonal

                MyX.Egyesít(munkalap, "A11:g11");
                MyX.Aláírásvonal(munkalap, "C11:G11");
                MyX.Kiir(Egyébszöveg.Text, "a11");
                MyX.Sortörésseltöbbsorba(munkalap, "A11", true);
                MyX.Sormagasság(munkalap, "a11", 60);

                for (int ik = 1; ik < 12; ik++)
                {
                    MyX.Igazít_függőleges(munkalap, $"A{ik}", "közép");
                    MyX.Igazít_vízszintes(munkalap, $"A{ik}", "bal");
                    MyX.Igazít_függőleges(munkalap, $"C{ik}", "közép");
                    MyX.Igazít_vízszintes(munkalap, $"C{ik}", "bal");
                }

                // táblázat fejléc
                MyX.Sormagasság(munkalap, "12:12", 55);
                MyX.Kiir("Sor- \nszám", "a12");
                MyX.Sortörésseltöbbsorba(munkalap, "A12");
                MyX.Kiir("HR azonosító", "b12");
                MyX.Kiir("Dolgozó neve", "c12");
                MyX.Kiir("Munkaköre", "d12");
                MyX.Kiir("Dátum", "e12");
                MyX.Kiir("Aláírás", "f12");
                MyX.Kiir($"Megfelelt\n(igen/nem/-)", "g12");
                MyX.Sortörésseltöbbsorba(munkalap, "G12");
                // E-mail
                TextBox1.Text += $"HR azonosító - Dolgozó neve\n\r";

                int sor = 13;
                int i = 1;

                List<Adat_Dolgozó_Alap> AdatokDolg = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());

                for (int j = 0; j < TáblaOktatás.Rows.Count; j++)
                {
                    if (TáblaOktatás.Rows[j].Selected)
                    {
                        MyX.Sormagasság(munkalap, $"{sor}:{sor}", 35);
                        MyX.Kiir($"{i}", $"a{sor}");
                        string HR = TáblaOktatás.Rows[j].Cells[0].Value.ToStrTrim();
                        MyX.Kiir(HR, $"b{sor}");
                        // E-mail
                        TextBox1.Text += $"{TáblaOktatás.Rows[j].Cells[0].Value} - ";
                        MyX.Kiir($"{TáblaOktatás.Rows[j].Cells[1].Value}", $"c{sor}");
                        // E-mail
                        TextBox1.Text += $"{TáblaOktatás.Rows[j].Cells[1].Value}\n\r";
                        if (AdatokDolg != null)
                        {
                            string munkakör = (from a in AdatokDolg
                                               where a.Dolgozószám == HR
                                               select a.Munkakör).FirstOrDefault();
                            if (munkakör != null)
                            {
                                MyX.Kiir(munkakör, $"d{sor}");
                                if (munkakör.Length > 30) MyX.Sortörésseltöbbsorba(munkalap, $"d{sor}");
                            }
                        }
                        i++;
                        sor++;
                    }
                    Holtart.Lép();
                }
                i--;
                sor--;
                MyX.Rácsoz(munkalap, "a12:g12");
                MyX.Rácsoz(munkalap, $"a13:g{sor}");

                string fénykép = $@"{Application.StartupPath}\Főmérnökség\adatok\Ábrák\BKV.png";
         
                // '**********************************************
                // '**Nyomtatási beállítások                    **
                // '**********************************************
                Beállítás_Nyomtatás BeNyom = new Beállítás_Nyomtatás
                {
                    Munkalap = munkalap,
                    NyomtatásiTerület = $"a1:g{sor}",
                    FejlécBal = "&G",
                    FejlécJobb = $"Budapesti Közlekedési Zártkörűen Működő Részvénytársaság\nJELENLÉTI ÍV",
                    LáblécBal = "Hatálybalépés dátuma: 2020.10.15",
                    LáblécKözép = "&P/&N",
                    BalMargó = 15,
                    JobbMargó = 15,
                    FelsőMargó = 19,
                    AlsóMargó = 19,
                    FejlécMéret = 10,
                    LáblécMéret = 13,
                    VízKözép = true,
                    Álló = true,
                    LapSzéles = 1,
                    Képútvonal= fénykép
                };
                MyX.NyomtatásiTerület_részletes(munkalap, BeNyom);

                MyX.ExcelMentés(fájlexc);
                MyX.ExcelBezárás();
                MyF.Megnyitás(fájlexc);

                Holtart.Ki();
                MessageBox.Show("A nyomtatvány elkészült. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private int Hánysor(string szöveg)
        {
            int válasz = 0;
            string[] darabol = szöveg.Split('\n');
            for (int i = 0; i < darabol.Length; i++)
            {
                //97 karakter fér el mezőben
                válasz += darabol[i].Length / 97;
                if (darabol[i].Length / 97 != darabol[i].Length % 97)
                    válasz++;
            }
            return válasz;
        }

        private void BtnAdminMentés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Adminoktatástárgya.Text?.Trim() == "") return;
                Adminoktatástárgya.Text = Adminoktatástárgya.Text.Replace("\"", "°").Replace("'", "°");
                Egyébszöveg.Text = Egyébszöveg.Text.Replace("\"", "°").Replace("'", "°");
                if (Admintartam.Text?.Trim() == "") Admintartam.Text = "0";
                if (!long.TryParse(Admintartam.Text, out long tartam)) tartam = 0;
                if (Txtemail.Text?.Trim() == "") Txtemail.Text = "_";

                List<Adat_OktatásiSegéd> Adatok = KézSegéd.Lista_Adatok();
                Adat_OktatásiSegéd rekord = (from a in Adatok
                                             where a.Telephely == Cmbtelephely.Text &&
                                                   a.IDoktatás == long.Parse(Adminoktatástárgya.Text.Substring(0, Adminoktatástárgya.Text.IndexOf("-")))
                                             select a).FirstOrDefault();
                Adat_OktatásiSegéd ADAT = new Adat_OktatásiSegéd(
                                        Adminoktatástárgya.Text.Substring(0, Adminoktatástárgya.Text.IndexOf("-")).Trim().ToÉrt_Long(),
                                        Cmbtelephely.Text.Trim(),
                                        AdminOktatásoka.Text.Trim(),
                                        Admintematika.Text.Trim(),
                                        Adminhelyszín.Text.Trim(),
                                        tartam,
                                        AdminOktató.Text.Trim(),
                                        AdminOktatómunkaköre.Text.Trim(),
                                        Egyébszöveg.Text.Trim(),
                                        Txtemail.Text.Trim());

                if (rekord != null)
                    KézSegéd.Módosítás(ADAT);
                else
                    KézSegéd.Rögzítés(ADAT);

                Kiírjaadmin();
                MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnJelenléti_Click(object sender, EventArgs e)
        {
            JelenlétiÍVKészítés();
        }

        private void TáblaOktatás_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (CHkNapló.Checked && e.RowIndex >= 0)
                {
                    string hely = $@"{Application.StartupPath}\Főmérnökség\Oktatás\{Cmbtelephely.Text}\{TáblaOktatás.Rows[e.RowIndex].Cells[8].Value}";
                    if (File.Exists(hely)) Pdf_Megjelenítés(hely);
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


        #region Oktatás Rögzítés
        private void BtnPdfÚjHasználClick(object sender, EventArgs e)
        {
            try
            {
                string Könyvtár = $@"{Application.StartupPath}\Főmérnökség\Oktatás\{Cmbtelephely.Text}";

                // feltöltött fájlok ismételt felhasználása
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    Filter = "PDF Files |*.pdf",
                    InitialDirectory = Könyvtár
                };

                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                {
                    if (!OpenFileDialog1.FileName.Contains(Könyvtár)) throw new HibásBevittAdat("A program által beállított könyvtárból lehet csak fájlt választani.");
                    PDF_néző.Document = PdfDocument.Load(OpenFileDialog1.FileName);
                    Txtmegnyitott.Text = OpenFileDialog1.FileName;
                    Txtmentett.Text = OpenFileDialog1.SafeFileName;
                    CHKpdfvan.Checked = true;
                    PDF_néző.Visible = true;
                    Fülek.SelectedIndex = 5;
                    Felcsukja();
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

        private void BizDátum_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!CHKpdfvan.Checked)
                {
                    if (TáblaOktatás.SelectedRows.Count != 0 && Chkoktat.Checked)
                    {
                        // ha oktatandóra kattint akkor összeállítja a fájl nevét
                        if (TáblaOktatás.SelectedRows.Count == 1)
                        {
                            // ha egy van kijelölve
                            Txtmentett.Text = $"{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[0].Value}_";
                            Txtmentett.Text += $"{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[2].Value}_";
                            Txtmentett.Text += $"{Cmbtelephely.Text}_";
                            Txtmentett.Text += $"{BizDátum.Value:yyyyMMdd}.pdf";
                        }
                        else
                        {
                            // ha több sor van kijelölve
                            int volt = 0;
                            int i = 1;
                            while (volt != 1)
                            {
                                Txtmentett.Text = $"Csop{i}_";
                                Txtmentett.Text += $"{TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[2].Value}_";
                                Txtmentett.Text += $"{Cmbtelephely.Text}_";
                                Txtmentett.Text += $"{BizDátum.Value:yyyyMMdd}.pdf";
                                string hely = $@"{Application.StartupPath}\Főmérnökség\Oktatás\{Cmbtelephely.Text}\{Txtmentett.Text.Trim()}";
                                if (!File.Exists(hely))
                                    i++;
                                else
                                    volt = 1;
                            }
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

        private void BtnPDFsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Txtmegnyitott.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva a feltölteni kívánt fájl.");
                if (Txtmentett.Text.Trim() == "") throw new HibásBevittAdat("Nincs kiválasztva a feltölteni kívánt fájl átnevezve, ellenőrizze le hogy van-e kiválasztva dolgozó.");
                if (CMBszámon.Text.Trim() == "")
                {
                    CMBszámon.Focus();
                    CMBszámon.BackColor = Color.Yellow;
                    return;
                }
                if (LSToktató.Text.Trim() == "")
                {
                    LSToktató.Focus();
                    LSToktató.BackColor = Color.Yellow;
                    return;
                }

                if (Megjegyzés.Text.Trim() == "") Megjegyzés.Text = "_";
                string hely = $@"{Application.StartupPath}\Főmérnökség\Oktatás\{Cmbtelephely.Text}\{Txtmentett.Text.Trim()}";
                // ha fel kell tölteni
                if (!CHKpdfvan.Checked)
                {
                    // PDF fájl-t feltöljük.
                    if (File.Exists(hely))
                    {
                        if (MessageBox.Show("Ezen a néven már létezik fájl, felülírjuk?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                            return;
                        else
                            File.Delete(hely);
                    }
                    // ha nem létezik akkor odamásoljuk
                    File.Copy(Txtmegnyitott.Text, hely);
                }

                // adatbázis adatokat rögzítünk.
                if (TáblaOktatás.SelectedRows.Count == 1)
                {
                    // ha egy dolgozót töltünk fel.
                    // módosítjuk a tábla adatait
                    Adat_Oktatásrajelöltek ADATJelölt = new Adat_Oktatásrajelöltek(
                                              TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[0].Value.ToString(),
                                              TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[2].Value.ToÉrt_Long(),
                                              TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[4].Value.ToÉrt_DaTeTime().AddMonths(TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[7].Value.ToÉrt_Int()),
                                              TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[7].Value.ToÉrt_Int() == 0 ? 1 : 0,
                                              Cmbtelephely.Text.Trim());

                    if (TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[7].Value.ToÉrt_Int() == 0)
                    {
                        // ha csak egyszer kell feltölteni, akkor a státust átállítjuk 1-re
                        Kéz_OktJelölt.Módosítás_Státus(ADATJelölt);
                    }
                    else
                    {
                        // ha valamilyen rendszereséggel kell oktatni, akkor hozzáadjuk a dátumhoz
                        Kéz_OktJelölt.Módosítás_Státus_Dátum(ADATJelölt);
                    }
                    // Naplózás
                    Adat_Oktatás_Napló ADATNAPLÓ = new Adat_Oktatás_Napló(
                                                0,
                                                TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[0].Value.ToStrTrim(),
                                                TáblaOktatás.Rows[TáblaOktatás.SelectedRows[0].Index].Cells[2].Value.ToÉrt_Long(),
                                                BizDátum.Value,
                                                LSToktató.Text.Trim(),
                                                DateTime.Now,
                                                Cmbtelephely.Text.Trim(),
                                                Txtmentett.Text.Trim(),
                                                CMBszámon.Text.Substring(0, 1).ToÉrt_Long(),
                                                0,
                                                Program.PostásNév.Trim(),
                                                Megjegyzés.Text.Trim());
                    Kéz_Okt_Nap.Rögzítés(Cmbtelephely.Text.Trim(), DateTime.Now.Year, ADATNAPLÓ);
                }
                else
                {
                    // ha csoportosan töljük fel az adatokat
                    List<Adat_Oktatásrajelöltek> AdatokMS = new List<Adat_Oktatásrajelöltek>();
                    List<Adat_Oktatásrajelöltek> AdatokMD = new List<Adat_Oktatásrajelöltek>();
                    List<Adat_Oktatás_Napló> AdatokNapló = new List<Adat_Oktatás_Napló>();

                    for (int i = 0; i < TáblaOktatás.SelectedRows.Count; i++)
                    {
                        int ideigHónap = TáblaOktatás.Rows[TáblaOktatás.SelectedRows[i].Index].Cells[7].Value.ToÉrt_Int();
                        DateTime ideigDátum = TáblaOktatás.Rows[TáblaOktatás.SelectedRows[i].Index].Cells[4].Value.ToÉrt_DaTeTime();
                        Adat_Oktatásrajelöltek ADATJelölt = new Adat_Oktatásrajelöltek(
                                                  TáblaOktatás.Rows[TáblaOktatás.SelectedRows[i].Index].Cells[0].Value.ToString(),
                                                  TáblaOktatás.Rows[TáblaOktatás.SelectedRows[i].Index].Cells[2].Value.ToÉrt_Long(),
                                                  ideigDátum.AddMonths(ideigHónap),
                                                  ideigHónap == 0 ? 1 : 0,
                                                  Cmbtelephely.Text.Trim());


                        if (ideigHónap == 0)
                        {
                            // ha csak egyszer kell feltölteni, akkor a státust átállítjuk 1-re
                            AdatokMS.Add(ADATJelölt);
                        }
                        else
                        {
                            // ha valamilyen rendszereséggel kell oktatni, akkor hozzáadjuk a dátumhoz
                            AdatokMD.Add(ADATJelölt);
                        }

                        // Naplózás
                        Adat_Oktatás_Napló ADATNAPLÓ = new Adat_Oktatás_Napló(
                                                    0,
                                                    TáblaOktatás.Rows[TáblaOktatás.SelectedRows[i].Index].Cells[0].Value.ToStrTrim(),
                                                    TáblaOktatás.Rows[TáblaOktatás.SelectedRows[i].Index].Cells[2].Value.ToÉrt_Long(),
                                                    BizDátum.Value,
                                                    LSToktató.Text.Trim(),
                                                    DateTime.Now,
                                                    Cmbtelephely.Text.Trim(),
                                                    Txtmentett.Text.Trim(),
                                                    CMBszámon.Text.Substring(0, 1).ToÉrt_Long(),
                                                    0,
                                                    Program.PostásNév.Trim(),
                                                    Megjegyzés.Text.Trim());
                        AdatokNapló.Add(ADATNAPLÓ);
                    }

                    if (AdatokMS.Count > 0) Kéz_OktJelölt.Módosítás_Státus(AdatokMS);
                    if (AdatokMD.Count > 0) Kéz_OktJelölt.Módosítás_Státus_Dátum(AdatokMD);
                    if (AdatokNapló.Count > 0) Kéz_Okt_Nap.Rögzítés(Cmbtelephely.Text.Trim(), DateTime.Now.Year, AdatokNapló);

                }


                MessageBox.Show("Az adatok rögzítése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CMBszámon.BackColor = Color.White;
                LSToktató.BackColor = Color.White;
                Txtmegnyitott.Text = "";
                Txtmentett.Text = "";
                PDF_néző.Visible = false;
                CHKpdfvan.Checked = false;
                Oktatáslistázáskötelezés(1);
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

        private void Okatatófeltöltés()
        {
            Holtart.Be();
            LSToktató.Items.Clear();
            LSToktató.Items.Add("");
            AdminOktató.Items.Clear();
            AdminOktató.Items.Add("");
            List<Adat_Dolgozó_Alap> Adatok = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim(), true);
            foreach (Adat_Dolgozó_Alap rekord in Adatok)
            {
                LSToktató.Items.Add(rekord.DolgozóNév.Trim());
                AdminOktató.Items.Add(rekord.DolgozóNév.Trim());
                Holtart.Lép();
            }
            AdminOktató.Refresh();
            LSToktató.Refresh();
            Holtart.Ki();
        }
        #endregion


        #region Rögzítés Napló
        private void BtnNaplózásEredményTöröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Chkelrendelés.Checked) return;
                if (TáblaOktatás.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kiválasztva Törölni kívűnt oktatás.");
                // ha nincs kijelölve sor akkor kilép

                DateTime dátumoktatás = new DateTime(1900, 1, 1);
                if (TáblaOktatás.SelectedRows.Count == 0) return;

                List<Adat_OktatásTábla> AdatokOkt = new List<Adat_OktatásTábla>();
                List<Adat_Oktatásrajelöltek> AdatokJelölt = new List<Adat_Oktatásrajelöltek>();
                List<Adat_Oktatásrajelöltek> AdatokJelöltDát = new List<Adat_Oktatásrajelöltek>();
                List<Adat_Oktatás_Napló> AdatokNapló = new List<Adat_Oktatás_Napló>();
                List<Adat_OktatásTábla> Adatok = KézOktatás.Lista_Adatok();
                Adatok_OktJelölt = Kéz_OktJelölt.Lista_Adatok();

                foreach (DataGridViewRow row in TáblaOktatás.SelectedRows)
                {
                    // a naplófájl módosítása törlésre 
                    Adat_Oktatás_Napló AdatNapló = new Adat_Oktatás_Napló(
                                                 row.Cells[0].Value.ToÉrt_Long(),
                                                 DateTime.Now,
                                                 1,
                                                 Program.PostásNév.Trim());
                    AdatokNapló.Add(AdatNapló);

                    int oktatásism = 0;
                    int idoktatás = row.Cells[3].Value.ToÉrt_Int();

                    Adat_OktatásTábla ElemOkt = (from a in Adatok
                                                 where a.IDoktatás == idoktatás
                                                 select a).FirstOrDefault();
                    if (ElemOkt != null) oktatásism = (int)ElemOkt.Ismétlődés;

                    Adat_Oktatásrajelöltek ElemOktJel = (from a in Adatok_OktJelölt
                                                         where a.IDoktatás == (long)row.Cells[3].Value
                                                         && a.HRazonosító == row.Cells[1].Value.ToString()
                                                         && a.Telephely == Cmbtelephely.Text
                                                         select a).FirstOrDefault();
                    if (ElemOktJel != null) dátumoktatás = ElemOktJel.Mikortól;

                    Adat_Oktatásrajelöltek ADATJelölt = new Adat_Oktatásrajelöltek(
                                                    row.Cells[1].Value.ToStrTrim(),
                                                    row.Cells[3].Value.ToÉrt_Long(),
                                                    dátumoktatás.AddMonths(-1 * oktatásism),
                                                    oktatásism != 0 ? 0 : 1,
                                                    Cmbtelephely.Text.Trim());

                    if (oktatásism == 0)
                        AdatokJelölt.Add(ADATJelölt);       // ha csak egyszer kell feltölteni, akkor a státust átállítjuk 0-re, hisz töröltük az oktatást
                    else
                        AdatokJelöltDát.Add(ADATJelölt);    // ha valamilyen rendszereséggel kell oktatni, akkor levonjuk a dátumból hisz töröljük

                }
                if (AdatokNapló.Count > 0) Kéz_Okt_Nap.Törlés(Cmbtelephely.Text.Trim(), Dátumtól.Value.Year, AdatokNapló);
                if (AdatokJelölt.Count > 0) Kéz_OktJelölt.Módosítás_Státus(AdatokJelölt);
                if (AdatokJelöltDát.Count > 0) Kéz_OktJelölt.Módosítás_Státus_Dátum(AdatokJelöltDát);
                Listanapló();

                MessageBox.Show("Az adatok törlése megtörtént. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnRögzítFrissít_Click(object sender, EventArgs e)
        {
            if (Dátumtól.Value.Year != Dátumig.Value.Year) throw new HibásBevittAdat("A két dátum azonos évben kell, hogy legyen.");
            Listanapló();
        }

        private void Listanapló()
        {
            try
            {
                if (ChkDolgozónév.CheckedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölove egy dolgozó sem.");


                CHkNapló.Checked = false;
                Chkoktat.Checked = false;
                Chkelrendelés.Checked = false;

                List<Adat_Oktatás_Napló> Adatok = Kéz_Okt_Nap.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátumtól.Value.Year);
                Adatok = (from a in Adatok
                          where a.Telephely == Cmbtelephely.Text.Trim()
                          && a.Rögzítésdátuma > Dátumtól.Value
                          && a.Rögzítésdátuma < Dátumig.Value.AddDays(1)
                          orderby a.HRazonosító
                          select a).ToList();
                if (Cmboktatásrögz.Text.Trim() != "") Adatok = Adatok.Where(a => a.IDoktatás == Cmboktatásrögz.Text.Substring(0, Cmboktatásrögz.Text.IndexOf("-")).ToÉrt_Long()).ToList();

                List<Adat_Dolgozó_Alap> AdatokDolg = KézDolgozó.Lista_Adatok(Cmbtelephely.Text.Trim());
                List<Adat_OktatásTábla> AdatokOkt = KézOktatás.Lista_Adatok();

                if (Adatok == null || Adatok.Count < 1) throw new HibásBevittAdat("Nincs az időszakban naplózott események adatbázisa.");

                Holtart.Be();
                TáblaOktatás.Rows.Clear();
                TáblaOktatás.Columns.Clear();
                ListaNév = "Napló";

                TáblaOktatás.Refresh();
                TáblaOktatás.Visible = true;
                TáblaOktatás.ColumnCount = 14;
                TáblaOktatás.RowCount = 0;
                // ' fejléc elkészítése
                TáblaOktatás.Columns[0].HeaderText = "Sorszám";
                TáblaOktatás.Columns[0].Width = 80;
                TáblaOktatás.Columns[1].HeaderText = "HR azonosító";
                TáblaOktatás.Columns[1].Width = 115;
                TáblaOktatás.Columns[2].HeaderText = "Név";
                TáblaOktatás.Columns[2].Width = 300;
                TáblaOktatás.Columns[3].HeaderText = "IDoktatás";
                TáblaOktatás.Columns[3].Width = 80;
                TáblaOktatás.Columns[4].HeaderText = "Oktatás Témája";
                TáblaOktatás.Columns[4].Width = 300;
                TáblaOktatás.Columns[5].HeaderText = "Oktatás dátuma";
                TáblaOktatás.Columns[5].Width = 110;
                TáblaOktatás.Columns[6].HeaderText = "Telephely";
                TáblaOktatás.Columns[6].Width = 120;
                TáblaOktatás.Columns[7].HeaderText = "Oktató";
                TáblaOktatás.Columns[7].Width = 150;
                TáblaOktatás.Columns[8].HeaderText = "PDF név";
                TáblaOktatás.Columns[8].Width = 300;
                TáblaOktatás.Columns[9].HeaderText = "Számonkérés";
                TáblaOktatás.Columns[9].Width = 100;
                TáblaOktatás.Columns[10].HeaderText = "Rögzítő";
                TáblaOktatás.Columns[10].Width = 100;
                TáblaOktatás.Columns[11].HeaderText = "Rögzítés ideje";
                TáblaOktatás.Columns[11].Width = 170;
                TáblaOktatás.Columns[12].HeaderText = "Státus";
                TáblaOktatás.Columns[12].Width = 100;
                TáblaOktatás.Columns[13].HeaderText = "Megjegyzés/ Tárolási hely";
                TáblaOktatás.Columns[13].Width = 300;
                CHkNapló.Checked = true;


                for (int j = 0; j < ChkDolgozónév.CheckedItems.Count; j++)
                {
                    string[] darabol = ChkDolgozónév.CheckedItems[j].ToString().Split('=');
                    List<Adat_Oktatás_Napló> AdatokSzűrt = (from ab in Adatok
                                                            where ab.HRazonosító.Trim() == darabol[1].Trim()
                                                            select ab).ToList();
                    if (AdatokSzűrt != null)
                    {
                        foreach (Adat_Oktatás_Napló rekord in AdatokSzűrt)
                        {
                            int i = TáblaOktatás.Rows.Add();
                            TáblaOktatás.Rows[i].Cells[0].Value = rekord.ID;
                            TáblaOktatás.Rows[i].Cells[1].Value = rekord.HRazonosító;
                            Adat_Dolgozó_Alap Elem = (from a in AdatokDolg
                                                      where a.Dolgozószám == rekord.HRazonosító
                                                      select a).FirstOrDefault();

                            if (Elem != null) TáblaOktatás.Rows[i].Cells[2].Value = Elem.DolgozóNév;

                            TáblaOktatás.Rows[i].Cells[3].Value = rekord.IDoktatás;

                            Adat_OktatásTábla ElemT = (from a in AdatokOkt
                                                       where a.IDoktatás == rekord.IDoktatás
                                                       select a).FirstOrDefault();

                            if (Elem != null) TáblaOktatás.Rows[i].Cells[4].Value = ElemT.Téma;
                            TáblaOktatás.Rows[i].Cells[5].Value = rekord.Oktatásdátuma.ToString("yyyy.MM.dd").Trim();
                            TáblaOktatás.Rows[i].Cells[6].Value = rekord.Telephely.Trim();
                            TáblaOktatás.Rows[i].Cells[7].Value = rekord.Kioktatta.Trim();
                            TáblaOktatás.Rows[i].Cells[8].Value = rekord.PDFFájlneve.Trim();
                            string válasz = "";
                            switch (rekord.Számonkérés)
                            {
                                case 0:
                                    {
                                        válasz = "nem volt";
                                        break;
                                    }
                                case 1:
                                    {
                                        válasz = "megfelelt";
                                        break;
                                    }
                                case 2:
                                    {
                                        válasz = "nem felelt meg";
                                        break;
                                    }
                            }

                            TáblaOktatás.Rows[i].Cells[9].Value = válasz.Trim();
                            TáblaOktatás.Rows[i].Cells[10].Value = rekord.Rögzítő.Trim();
                            TáblaOktatás.Rows[i].Cells[11].Value = rekord.Rögzítésdátuma;
                            switch (rekord.Státus)
                            {
                                case 0:
                                    {
                                        válasz = "Érvényes";
                                        break;
                                    }
                                case 1:
                                    {
                                        válasz = "Törölt";
                                        break;
                                    }
                            }
                            TáblaOktatás.Rows[i].Cells[12].Value = válasz.Trim();
                            TáblaOktatás.Rows[i].Cells[13].Value = rekord.Megjegyzés.Trim();
                        }
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
        #endregion

        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
                if (Cmbtelephely.Text.Trim() == "") return;
                //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
                //ha nem akkor a régit használjuk
                if (Program.PostásJogkör.Substring(0, 1) == "R")
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                else
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
    }
}