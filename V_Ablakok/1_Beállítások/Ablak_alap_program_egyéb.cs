using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_MindenEgyéb;
using Villamos.Villamos_Adatszerkezet;
using static System.IO.File;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_alap_program_egyéb
    {
        #region Kezelők
        readonly Kezelő_Alap_Beolvasás KézBeolv = new Kezelő_Alap_Beolvasás();
        readonly Kezelő_Osztály_Név KézOsztály = new Kezelő_Osztály_Név();
        readonly Kezelő_Jármű_Takarítás_Kötbér KézJárműtakKöt = new Kezelő_Jármű_Takarítás_Kötbér();
        readonly Kezelő_Jármű_Takarítás_Ár KézTakÁr = new Kezelő_Jármű_Takarítás_Ár();
        readonly Kezelő_Jármű_Állomány_Típus KézÁllományTípus = new Kezelő_Jármű_Állomány_Típus();
        readonly Kezelő_Jármű_Takarítás_Mátrix KétJárműtakMátr = new Kezelő_Jármű_Takarítás_Mátrix();
        readonly Kezelő_Kiegészítő_Sérülés KézSérülés = new Kezelő_Kiegészítő_Sérülés();
        #endregion


        private string directoryTargetLocation; // Selected file path
        private string Destinydirectory; // Selected dest directory path

        readonly DataTable AdatÁRTábla = new DataTable();

        public Ablak_alap_program_egyéb()
        {
            InitializeComponent();
            Start();
        }

        private void AblakProgramegyéb_Load(object sender, EventArgs e)
        {
        }

        #region Alap

        private void Start()
        {
            try
            {
                //Ha van 0-tól különböző akkor a régi jogosultságkiosztást használjuk
                //ha mind 0 akkor a GombLathatosagKezelo-t használjuk
                if (Program.PostásJogkör.Any(c => c != '0'))
                {
                    Telephelyekfeltöltése();
                    Jogosultságkiosztás();
                }
                else
                {
                    TelephelyekFeltöltéseÚj();
                    Cmbtelephely.Text = "Főmérnökség";
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
                }

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

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                List<Adat_Kiegészítő_Sérülés> Adatok = KézSérülés.Lista_Adatok();
                foreach (Adat_Kiegészítő_Sérülés rekord in Adatok)
                    Cmbtelephely.Items.Add(rekord.Név);

                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
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

        private void TelephelyekFeltöltéseÚj()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Adat in GombLathatosagKezelo.Telephelyek(this.Name))
                    Cmbtelephely.Items.Add(Adat.Trim());
                //Alapkönyvtárat beállítjuk 
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

        private void Jogosultságkiosztás()
        {
            int melyikelem;
            try
            {
                SAPRögzít.Enabled = false;
                SAPTöröl.Enabled = false;
                OsztályRögzít.Enabled = false;
                BeolvásBeállítás.Enabled = false;

                Tak_Ár_rögzítés.Enabled = false;
                Button4.Enabled = false;
                Mátrix_rögzít.Enabled = false;
                Adatok_beolvasása.Enabled = false;

                if (Program.PostásTelephely.Trim() != "Főmérnökség")
                {
                    SAPRögzít.Visible = false;
                    SAPTöröl.Visible = false;
                    OsztályRögzít.Visible = false;
                    BeolvásBeállítás.Visible = false;

                    Tak_Ár_rögzítés.Visible = true;
                    Button4.Visible = true;
                    Mátrix_rögzít.Visible = true;
                    Adatok_beolvasása.Visible = true;
                }
                else
                {
                    SAPRögzít.Visible = true;
                    SAPTöröl.Visible = true;
                    OsztályRögzít.Visible = true;
                    BeolvásBeállítás.Visible = true;

                    Tak_Ár_rögzítés.Visible = false;
                    Button4.Visible = false;
                    Mátrix_rögzít.Visible = false;
                    Adatok_beolvasása.Visible = false;
                }



                melyikelem = 15;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    SAPRögzít.Enabled = true;
                    SAPTöröl.Enabled = true;
                    BeolvásBeállítás.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    OsztályRögzít.Enabled = true;
                }
                // módosítás 3
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Tak_Ár_rögzítés.Enabled = true;
                    Button4.Enabled = true;
                    Mátrix_rögzít.Enabled = true;
                    Adatok_beolvasása.Enabled = true;
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

        private void Fülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Fülekkitöltése()
        {
            try
            {
                switch (Fülek.SelectedIndex)
                {
                    case 0:
                        {
                            // SAP-FORTE beolvasasás
                            CiklusTípusfeltöltés();
                            break;
                        }
                    case 1:
                        {
                            // Osztály beolvasás
                            Osztálytáblaíró();
                            break;
                        }
                    case 2:
                        {
                            // Biztonsági másolat készítés
                            Dátumig.Value = DateTime.Today;
                            Dátumtól.Value = DateTime.Today;
                            break;
                        }
                    case 3:
                        {
                            Takarítási_combok_feltöltése();
                            Kötbér_listázás();
                            Pót_ürítés();
                            Kocsitípusok_feltöltése();
                            Ár_tábla_listázás();
                            Ár_beviteli_törlés();

                            Mátrix_tábla_kiírás();
                            Mátrix_ürítés();

                            Mátrix_combo();
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

        private void Button13_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\alapegyéb.html";
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

        private void Fülek_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
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


        #region SAP-Forte beolvasás

        #endregion


        #region Osztály
        private void OsztályExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (TáblaOsztály.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Osztály_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMdd"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, TáblaOsztály);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(fájlexc);
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

        private void OsztályRögzít_Click(object sender, EventArgs e)
        {
            try
            {
                Osztálynév.Text = MyF.Szöveg_Tisztítás(Osztálynév.Text);
                Osztálymező.Text = MyF.Szöveg_Tisztítás(Osztálymező.Text);

                // leellenőrizzük, hogy minden adat ki van-e töltve
                if ((Osztálynév.Text.Trim() == "")) return;
                if ((Osztálymező.Text.Trim() == "")) return;
                if ((ID.Text.Trim() == "")) return;


                Adat_Osztály_Név ADAT = new Adat_Osztály_Név(ID.Text.ToÉrt_Int(),
                                                             Osztálynév.Text,
                                                             Osztálymező.Text,
                                                             Használatban.Checked);

                KézOsztály.Módosítás(ADAT);
                Osztálytáblaíró();
                MessageBox.Show("Az adat rögzítése megtörtént. ", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Osztályfrissít_Click(object sender, EventArgs e)
        {
            try
            {
                Osztálytáblaíró();
                ID.Text = "";
                Osztálynév.Text = "";
                Osztálymező.Text = "";
                Használatban.Checked = false;
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

        private void Osztálytáblaíró()
        {
            try
            {
                List<Adat_Osztály_Név> AdatokOsztNév = KézOsztály.Lista_Adat();
                TáblaOsztály.Rows.Clear();
                TáblaOsztály.Columns.Clear();
                TáblaOsztály.Refresh();
                TáblaOsztály.Visible = false;
                TáblaOsztály.ColumnCount = 4;

                // ' fejléc elkészítése

                TáblaOsztály.Columns[0].HeaderText = "Id";
                TáblaOsztály.Columns[0].Width = 60;
                TáblaOsztály.Columns[1].HeaderText = "Osztálynév";
                TáblaOsztály.Columns[1].Width = 400;
                TáblaOsztály.Columns[2].HeaderText = "Osztálymező";
                TáblaOsztály.Columns[2].Width = 400;
                TáblaOsztály.Columns[3].HeaderText = "Használatban";
                TáblaOsztály.Columns[3].Width = 150;

                foreach (Adat_Osztály_Név rekord in AdatokOsztNév)
                {
                    TáblaOsztály.RowCount++;
                    int i = TáblaOsztály.RowCount - 1;

                    TáblaOsztály.Rows[i].Cells[0].Value = rekord.Id;
                    TáblaOsztály.Rows[i].Cells[1].Value = rekord.Osztálynév;
                    TáblaOsztály.Rows[i].Cells[2].Value = rekord.Osztálymező;

                    if (rekord.Használatban)
                        TáblaOsztály.Rows[i].Cells[3].Value = "Igen";
                    else
                        TáblaOsztály.Rows[i].Cells[3].Value = "Nem";
                }
                TáblaOsztály.Refresh();
                TáblaOsztály.Visible = true;
                TáblaOsztály.ClearSelection();

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

        private void TáblaOsztály_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (TáblaOsztály.SelectedRows.Count != 0)
                {
                    ID.Text = TáblaOsztály.Rows[TáblaOsztály.SelectedRows[0].Index].Cells[0].Value.ToString();
                    Osztálynév.Text = TáblaOsztály.Rows[TáblaOsztály.SelectedRows[0].Index].Cells[1].Value.ToString();
                    Osztálymező.Text = TáblaOsztály.Rows[TáblaOsztály.SelectedRows[0].Index].Cells[2].Value.ToString();
                    Használatban.Checked = TáblaOsztály.Rows[TáblaOsztály.SelectedRows[0].Index].Cells[3].Value.ToString() == "Igen";
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

        private void TáblaOsztály_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                TáblaOsztály.Rows[e.RowIndex].Selected = true;
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

        private void Osztály_Új_Click(object sender, EventArgs e)
        {
            try
            {
                KézOsztály.ÚjMező();
                Osztálytáblaíró();
                MessageBox.Show("Az új mező létrejött.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Járműtakarítás alapadatok
        private void Takarítási_combok_feltöltése()
        {
            try
            {
                Kötbér_takarítási_fajta.Items.Clear();
                Kötbér_takarítási_fajta.Items.Add("J1");
                Kötbér_takarítási_fajta.Items.Add("J2");
                Kötbér_takarítási_fajta.Items.Add("J3");
                Kötbér_takarítási_fajta.Items.Add("J4");
                Kötbér_takarítási_fajta.Items.Add("J5");
                Kötbér_takarítási_fajta.Items.Add("J6");
                Kötbér_takarítási_fajta.Items.Add("Graffiti");
                Kötbér_takarítási_fajta.Items.Add("Eseti");
                Kötbér_takarítási_fajta.Items.Add("Fertőtlenítés");

                Szűr_Fajta.Items.Clear();
                Szűr_Fajta.Items.Add("J1");
                Szűr_Fajta.Items.Add("J2");
                Szűr_Fajta.Items.Add("J3");
                Szűr_Fajta.Items.Add("J4");
                Szűr_Fajta.Items.Add("J5");
                Szűr_Fajta.Items.Add("J6");
                Szűr_Fajta.Items.Add("Graffiti");
                Szűr_Fajta.Items.Add("Eseti");
                Szűr_Fajta.Items.Add("Fertőtlenítés");

                Tak_Napszak.Items.Clear();
                Tak_Napszak.Items.Add("Nappal");
                Tak_Napszak.Items.Add("Éjszaka");

                Szűr_Napszak.Items.Clear();
                Szűr_Napszak.Items.Add("Nappal");
                Szűr_Napszak.Items.Add("Éjszaka");

                Tak_J_takarítási_fajta.Items.Clear();
                Tak_J_takarítási_fajta.Items.Add("J1");
                Tak_J_takarítási_fajta.Items.Add("J2");
                Tak_J_takarítási_fajta.Items.Add("J3");
                Tak_J_takarítási_fajta.Items.Add("J4");
                Tak_J_takarítási_fajta.Items.Add("J5");
                Tak_J_takarítási_fajta.Items.Add("J6");
                Tak_J_takarítási_fajta.Items.Add("Graffiti");
                Tak_J_takarítási_fajta.Items.Add("Eseti");
                Tak_J_takarítási_fajta.Items.Add("Fertőtlenítés");
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

        private void Kocsitípusok_feltöltése()
        {
            try
            {
                List<string> Adatok = KézTakÁr.Lista_Adatok().Select(a => a.JárműTípus).Distinct().ToList();

                Tak_J_típus.Items.Clear();
                Szűr_Típus.Items.Clear();

                foreach (string elem in Adatok)
                {
                    Tak_J_típus.Items.Add(elem);
                    Szűr_Típus.Items.Add(elem);
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


        #region Takarítás Kötbér
        private void Kötbér_listázás()
        {
            try
            {
                List<Adat_Jármű_Takarítás_Kötbér> ADatokJárműtakKöt = KézJárműtakKöt.Lista_Adat();

                Kötbér_tábla.Rows.Clear();
                Kötbér_tábla.Columns.Clear();
                Kötbér_tábla.Refresh();
                Kötbér_tábla.Visible = false;
                Kötbér_tábla.ColumnCount = 3;

                // fejléc elkészítése
                Kötbér_tábla.Columns[0].HeaderText = "Takarítási fajta";
                Kötbér_tábla.Columns[0].Width = 100;
                Kötbér_tábla.Columns[1].HeaderText = "Nem megfelelő";
                Kötbér_tábla.Columns[1].Width = 100;
                Kötbér_tábla.Columns[2].HeaderText = "Póthatáridő";
                Kötbér_tábla.Columns[2].Width = 100;

                foreach (Adat_Jármű_Takarítás_Kötbér rekord in ADatokJárműtakKöt)
                {
                    Kötbér_tábla.RowCount++;
                    int i = Kötbér_tábla.RowCount - 1;
                    Kötbér_tábla.Rows[i].Cells[0].Value = rekord.Takarítási_fajta;
                    Kötbér_tábla.Rows[i].Cells[1].Value = rekord.NemMegfelel;
                    Kötbér_tábla.Rows[i].Cells[2].Value = rekord.Póthatáridő;
                }

                Kötbér_tábla.Visible = true;
                Kötbér_tábla.Refresh();

                Kötbér_tábla.ClearSelection();
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

        private void Kötbér_Frissít_Click(object sender, EventArgs e)
        {
            try
            {
                Kötbér_listázás();
                Pót_ürítés();
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

        private void Kötbér_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                int i = e.RowIndex;
                Kötbér_takarítási_fajta.Text = Kötbér_tábla.Rows[i].Cells[0].Value.ToStrTrim();
                Kötbér_Nem.Text = Kötbér_tábla.Rows[i].Cells[1].Value.ToStrTrim();
                Kötbér_pót.Text = Kötbér_tábla.Rows[i].Cells[2].Value.ToStrTrim();
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

        private void Pót_rögzítés()
        {
            try
            {
                if (Kötbér_takarítási_fajta.Text.Trim() == "") throw new HibásBevittAdat("Takarítási fajtát meg kell adni.");
                if (Kötbér_Nem.Text.Trim() == "") Kötbér_Nem.Text = "0";
                if (Kötbér_pót.Text.Trim() == "") Kötbér_pót.Text = "0";
                if (!int.TryParse(Kötbér_Nem.Text, out int Kötbérnem)) throw new HibásBevittAdat("A nem megfelelési szorzónak számnak kell lennie.");
                if (!int.TryParse(Kötbér_pót.Text, out int Kötbérpót)) throw new HibásBevittAdat("A póthatáridő szorzónak számnak kell lennie.");

                List<Adat_Jármű_Takarítás_Kötbér> ADatokJárműtakKöt = KézJárműtakKöt.Lista_Adat();

                Adat_Jármű_Takarítás_Kötbér Elem = (from a in ADatokJárműtakKöt
                                                    where a.Takarítási_fajta == Kötbér_takarítási_fajta.Text.Trim()
                                                    select a).FirstOrDefault()
                                                    ;
                Adat_Jármű_Takarítás_Kötbér ADAT = new Adat_Jármű_Takarítás_Kötbér(Kötbér_takarítási_fajta.Text.Trim(),
                                                                                   Kötbér_Nem.Text.Replace(",", "."),
                                                                                   Kötbér_pót.Text.Replace(",", "."));

                if (Elem != null)
                    KézJárműtakKöt.Módosítás(ADAT);
                else
                    KézJárműtakKöt.Rögzítés(ADAT);


                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Kötbér_listázás();
                Pót_ürítés();
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

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                Pót_rögzítés();
                Pót_ürítés();
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

        private void Pót_ürítés()
        {
            try
            {
                Kötbér_takarítási_fajta.Text = "";
                Kötbér_Nem.Text = "";
                Kötbér_pót.Text = "";
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


        #region Takarítás Ár
        private void Tak_Ár_frissít_Click(object sender, EventArgs e)
        {
            try
            {
                Ár_tábla_listázás();
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

        private void Ár_tábla_listázás()
        {
            try
            {
                List<Adat_Jármű_Takarítás_Árak> AdatokÖ = KézTakÁr.Lista_Adatok();

                if (Szűr_Fajta.Text.Trim() != "")
                    AdatokÖ = (from a in AdatokÖ
                               where a.Takarítási_fajta == Szűr_Fajta.Text.Trim()
                               select a).ToList();

                if (Szűr_Típus.Text.Trim() != "")
                    AdatokÖ = (from a in AdatokÖ
                               where a.JárműTípus == Szűr_Típus.Text.Trim()
                               select a).ToList();

                if (Szűr_Napszak.Text.Trim() == "Nappal")
                    AdatokÖ = (from a in AdatokÖ
                               where a.Napszak == 1
                               select a).ToList();
                else if (Szűr_Napszak.Text.Trim() == "Éjszaka")
                    AdatokÖ = (from a in AdatokÖ
                               where a.Napszak == 2
                               select a).ToList();

                if (Szűr_Érvényes.Checked)
                    AdatokÖ = (from a in AdatokÖ
                               where a.Érv_kezdet <= DateTime.Today && a.Érv_vég >= DateTime.Today
                               select a).ToList();

                AdatokÖ = (from a in AdatokÖ
                           orderby a.Érv_kezdet, a.JárműTípus, a.Takarítási_fajta
                           select a).ToList();

                Tak_Ár_Tábla.Visible = false;
                Tak_Ár_Tábla.CleanFilterAndSort();

                Tak_Ár_Tábla_Fejléc();
                AdatÁRTábla.Clear();

                Tak_Ár_tábla_Feltöltés(AdatokÖ);
                Tak_Ár_Tábla.CleanFilterAndSort();
                Tak_Ár_Tábla.DataSource = AdatÁRTábla;
                Tak_Ár_Tábla_Szélesség();

                Tak_Ár_Tábla.Visible = true;
                Tak_Ár_Tábla.Refresh();
                Tak_Ár_Tábla.ClearSelection();
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

        private void Tak_Ár_Tábla_Fejléc()
        {
            AdatÁRTábla.Columns.Clear();
            AdatÁRTábla.Columns.Add("Sorszám");
            AdatÁRTábla.Columns.Add("Jármű típus");
            AdatÁRTábla.Columns.Add("Takarítási fajta");
            AdatÁRTábla.Columns.Add("Napszak");
            AdatÁRTábla.Columns.Add("Ár");
            AdatÁRTábla.Columns.Add("Kezdő dátum");
            AdatÁRTábla.Columns.Add("Vég dátum");
        }

        private void Tak_Ár_Tábla_Szélesség()
        {
            Tak_Ár_Tábla.Columns["Sorszám"].Width = 100;
            Tak_Ár_Tábla.Columns["Jármű típus"].Width = 100;
            Tak_Ár_Tábla.Columns["Takarítási fajta"].Width = 100;
            Tak_Ár_Tábla.Columns["Napszak"].Width = 100;
            Tak_Ár_Tábla.Columns["Ár"].Width = 100;
            Tak_Ár_Tábla.Columns["Kezdő dátum"].Width = 100;
            Tak_Ár_Tábla.Columns["Vég dátum"].Width = 100;
        }

        private void Tak_Ár_tábla_Feltöltés(List<Adat_Jármű_Takarítás_Árak> AdatokÖ)
        {
            foreach (Adat_Jármű_Takarítás_Árak rekord in AdatokÖ)
            {
                DataRow Soradat = AdatÁRTábla.NewRow();

                Soradat["Sorszám"] = rekord.Id;
                Soradat["Jármű típus"] = rekord.JárműTípus.Trim();
                Soradat["Takarítási fajta"] = rekord.Takarítási_fajta.Trim();
                if (rekord.Napszak == 1)
                    Soradat["Napszak"] = "Nappal";
                else
                    Soradat["Napszak"] = "Éjszaka";
                Soradat["Ár"] = rekord.Ár;
                Soradat["Kezdő dátum"] = rekord.Érv_kezdet.ToString("yyyy.MM.dd");
                Soradat["Vég dátum"] = rekord.Érv_vég.ToString("yyyy.MM.dd");

                AdatÁRTábla.Rows.Add(Soradat);
            }

        }

        private void Ár_beviteli_törlés()
        {
            try
            {
                Tak_id.Text = "";
                Tak_J_típus.Text = "";
                Tak_J_takarítási_fajta.Text = "";
                Tak_Napszak.Text = "";
                Tak_Ár.Text = "";
                Tak_Érv_k.Value = new DateTime(1900, 1, 1);
                Tak_érv_V.Value = new DateTime(1900, 1, 1);
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

        private void Tak_Ár_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                int i = e.RowIndex;

                Tak_id.Text = Tak_Ár_Tábla.Rows[i].Cells[0].Value.ToString();
                Tak_J_típus.Text = Tak_Ár_Tábla.Rows[i].Cells[1].Value.ToString();
                Tak_J_takarítási_fajta.Text = Tak_Ár_Tábla.Rows[i].Cells[2].Value.ToString();
                Tak_Napszak.Text = Tak_Ár_Tábla.Rows[i].Cells[3].Value.ToString();
                Tak_Ár.Text = Tak_Ár_Tábla.Rows[i].Cells[4].Value.ToString();
                Tak_Érv_k.Value = DateTime.Parse(Tak_Ár_Tábla.Rows[i].Cells[5].Value.ToString());
                Tak_érv_V.Value = DateTime.Parse(Tak_Ár_Tábla.Rows[i].Cells[6].Value.ToString());
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

        private void Tak_Ár_rögzítés_Click(object sender, EventArgs e)
        {
            try
            {

                if (Tak_J_típus.Text.Trim() == "") throw new HibásBevittAdat("A járműtípus kötelező mező nem lehet üres.");
                if (Tak_J_takarítási_fajta.Text.Trim() == "") throw new HibásBevittAdat("A takarítási fajta kötelező mező nem lehet üres.");
                if (Tak_Napszak.Text.Trim() == "") throw new HibásBevittAdat("A napszak kötelező mező nem lehet üres.");
                if (Tak_Ár.Text.Trim() == "") throw new HibásBevittAdat("A takarítási ár mező kötelező mező.");
                if (!double.TryParse(Tak_Ár.Text, out double result)) throw new HibásBevittAdat("A takarítási árnak számnak kell lennie.");
                if (Tak_Érv_k.Value >= Tak_érv_V.Value) throw new HibásBevittAdat("Az érvényesség kezdetének a végénél kisebbnek kell lennie.");

                List<Adat_Jármű_Takarítás_Árak> AdatokÁr = KézTakÁr.Lista_Adatok();

                // lellenőrizzük, hogy van-e olyan id
                double ID = 1;
                if (Tak_id.Text.Trim() == "")
                {
                    // következő id meghatározása
                    if (AdatokÁr.Count > 0) ID = AdatokÁr.Max(a => a.Id) + 1;
                    Tak_id.Text = $"{ID}";

                    Adat_Jármű_Takarítás_Árak ADAT = new Adat_Jármű_Takarítás_Árak(ID,
                                                                Tak_J_típus.Text.Trim(),
                                                                Tak_J_takarítási_fajta.Text.Trim(),
                                                                Tak_Napszak.Text.Trim() == "Nappal" ? 1 : 2,
                                                                double.Parse(Tak_Ár.Text.Replace(",", ".")),
                                                                Tak_Érv_k.Value,
                                                                Tak_érv_V.Value);
                    KézTakÁr.Rögzítés(ADAT);
                    MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // ha nem üres, akkor leellörizzük, hogy van-e ilyen elem
                    if (!double.TryParse(Tak_id.Text, out ID)) ID = 0;
                    Adat_Jármű_Takarítás_Árak Elem = (from a in AdatokÁr
                                                      where a.Id == ID
                                                      select a).FirstOrDefault();

                    if (Elem != null)
                    {
                        Adat_Jármű_Takarítás_Árak ADAT = new Adat_Jármű_Takarítás_Árak(ID,
                                                               Tak_J_típus.Text.Trim(),
                                                               Tak_J_takarítási_fajta.Text.Trim(),
                                                               Tak_Napszak.Text.Trim() == "Nappal" ? 1 : 2,
                                                               double.Parse(Tak_Ár.Text.Replace(",", ".")),
                                                               Tak_Érv_k.Value,
                                                               Tak_érv_V.Value);
                        KézTakÁr.Módosítás(ADAT);
                        MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                Ár_tábla_listázás();
                Kocsitípusok_feltöltése();
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

        private void Excel_tak_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tak_Ár_Tábla.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Takarírás_ár_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMdd"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, Tak_Ár_Tábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás(fájlexc);
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

        private void Tak_Új_Click(object sender, EventArgs e)
        {
            try
            {
                Ár_beviteli_törlés();
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

        private void Beviteli_táblakészítés_Click(object sender, EventArgs e)
        {
            try
            {
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Excel tábla készítés adatok beolvasásához",
                    FileName = "Beolvasó_Jármű_Takarítás_" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.ExcelLétrehozás();

                MyE.Kiir("Járműtípus", "A1");
                MyE.Kiir("Takarítási fokozat", "B1");
                MyE.Kiir("Napszak", "C1");
                MyE.Kiir("Egységár", "D1");
                MyE.Kiir("Kezdete", "E1");
                MyE.Kiir("Vége", "F1");
                string[] Tak_fajta = { "J1", "J2", "J3", "J4", "J5", "J6", "Graffiti", "Eseti", "Fertőtlenítés" };
                string[] Napszak = { "Nappal", "Éjszaka" };

                List<Adat_Jármű_Állomány_Típus> Adatok = KézÁllományTípus.Lista_Adatok(Cmbtelephely.Text.Trim());

                int sor = 1;
                foreach (Adat_Jármű_Állomány_Típus rekord in Adatok)
                {
                    foreach (string fajta in Tak_fajta)
                    {
                        foreach (string nap in Napszak)
                        {
                            sor++;
                            MyE.Kiir(rekord.Típus.ToString().Trim(), "A" + sor);
                            MyE.Kiir(fajta.Trim(), "B" + sor);
                            MyE.Kiir(nap.ToString().Trim(), "C" + sor);

                            MyE.Kiir(Tak_Érv_k.Value.ToString("yyyy.MM.dd"), "E" + sor);
                            MyE.Kiir(Tak_érv_V.Value.ToString("yyyy.MM.dd"), "F" + sor);
                        }
                    }
                }


                MyE.Oszlopszélesség("Munka1", "A:F");
                MyE.Rácsoz("A1:F" + sor);
                MyE.NyomtatásiTerület_részletes("Munka1", "A1:F" + sor, "", "", true);
                MyE.ExcelMentés(fájlexc);
                MyE.ExcelBezárás();

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

        private void Adatok_beolvasása_Click(object sender, EventArgs e)
        {
            string fájlexc = "";
            try
            {
                // megpróbáljuk megnyitni az excel táblát.
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Jármű takarítási árak betöltése",
                    FileName = "",
                    Filter = "Excel |*.xlsx"
                };

                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (OpenFileDialogPI.ShowDialogEllenőr(OpenFileDialog1) == DialogResult.OK)
                    fájlexc = OpenFileDialog1.FileName;
                else
                    return;

                DataTable Tábla = MyF.Excel_Tábla_Beolvas(fájlexc);

                List<Adat_Jármű_Takarítás_Árak> AdatokÁrak = KézTakÁr.Lista_Adatok();
                double id = 1;
                if (AdatokÁrak.Count > 0) id = AdatokÁrak.Max(a => a.Id);

                Holtart.Be();
                List<Adat_Jármű_Takarítás_Árak> MódosítGy = new List<Adat_Jármű_Takarítás_Árak>();
                List<Adat_Jármű_Takarítás_Árak> RögzítGy = new List<Adat_Jármű_Takarítás_Árak>();
                foreach (DataRow Sor in Tábla.Rows)
                {
                    int ára = Sor["Egységár"].ToString().ToÉrt_Int();
                    string Járműtípus = Sor["Járműtípus"].ToStrTrim();
                    string Takarítási_fajta = Sor["Takarítási fokozat"].ToStrTrim();
                    int napszak = Sor["Napszak"].ToStrTrim() == "Nappal" ? 1 : 2;
                    DateTime kezdet = Sor["Kezdete"].ToStrTrim().ToÉrt_DaTeTime();
                    DateTime vége = Sor["Vége"].ToStrTrim().ToÉrt_DaTeTime();


                    Adat_Jármű_Takarítás_Árak Egy = (from a in AdatokÁrak
                                                     where a.Érv_vég >= kezdet
                                                     && a.JárműTípus == Járműtípus
                                                     && a.Takarítási_fajta == Takarítási_fajta
                                                     && a.Napszak == napszak
                                                     select a).FirstOrDefault();
                    //Megkeressük, hogy létezik-e már hasonló, ha igen akkor az érvényyeségi időt bezárjuk
                    if (Egy != null)
                    {
                        Adat_Jármű_Takarítás_Árak ADATM = new Adat_Jármű_Takarítás_Árak(Egy.Id,
                                                                                        kezdet.AddDays(-1));
                        MódosítGy.Add(ADATM);
                    }

                    // következő id meghatározása
                    id++;
                    //Rögzítjük az új elemet
                    Adat_Jármű_Takarítás_Árak ADAT = new Adat_Jármű_Takarítás_Árak(id,
                                                                                   Járműtípus,
                                                                                   Takarítási_fajta,
                                                                                   napszak,
                                                                                   ára,
                                                                                   kezdet,
                                                                                   vége);
                    RögzítGy.Add(ADAT);

                    Holtart.Lép();
                }
                if (MódosítGy.Count > 0) KézTakÁr.Módosítás_Vég(MódosítGy);
                if (RögzítGy.Count > 0) KézTakÁr.Rögzítés(RögzítGy);

                Ár_tábla_listázás();
                Holtart.Ki();
                File.Delete(fájlexc);
                MessageBox.Show("Az Excel tábla feldolgozása megtörtént. !", "Tájékoztató", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                if (ex.StackTrace.Contains("System.IO.File.InternalDelete"))
                    MessageBox.Show($"A programnak a beolvasott adatokat tartalmazó fájlt nem sikerült törölni.\n Valószínüleg a {fájlexc} nyitva van.\n\nAz adat konvertálás befejeződött!", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                    MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void VégeÁrRögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tak_Ár_Tábla.SelectedRows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy módosítani kívánt tétel sem.");

                List<Adat_Jármű_Takarítás_Árak> Adatok = new List<Adat_Jármű_Takarítás_Árak>();
                Holtart.Be(Tak_Ár_Tábla.SelectedRows.Count + 1);
                for (int i = 0; i < Tak_Ár_Tábla.SelectedRows.Count; i++)
                {
                    Adat_Jármű_Takarítás_Árak ADAT = new Adat_Jármű_Takarítás_Árak(Tak_Ár_Tábla.SelectedRows[i].Cells[0].Value.ToÉrt_Double(),
                                                                                   Tak_érv_V.Value);
                    Adatok.Add(ADAT);
                    Holtart.Lép();
                }
                KézTakÁr.Módosítás_Vég(Adatok);
                Holtart.Ki();
                Ár_tábla_listázás();
                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Takarítás mátrix
        private void Mátrix_tábla_kiírás()
        {
            try
            {
                List<Adat_Jármű_Takarítás_Mátrix> AdatokJárműtakMátr = KétJárműtakMátr.Lista_Adat();

                Mátrix_tábla.Rows.Clear();
                Mátrix_tábla.Columns.Clear();
                Mátrix_tábla.Refresh();
                Mátrix_tábla.Visible = false;
                Mátrix_tábla.ColumnCount = 4;

                // fejléc elkészítése
                Mátrix_tábla.Columns[0].HeaderText = "Sor- szám";
                Mátrix_tábla.Columns[0].Width = 80;
                Mátrix_tábla.Columns[1].HeaderText = "Takarítás fajta ";
                Mátrix_tábla.Columns[1].Width = 80;
                Mátrix_tábla.Columns[2].HeaderText = "Takarítás fajta másik";
                Mátrix_tábla.Columns[2].Width = 80;
                Mátrix_tábla.Columns[3].HeaderText = "átrögzítés";
                Mátrix_tábla.Columns[3].Width = 80;

                foreach (Adat_Jármű_Takarítás_Mátrix rekord in AdatokJárműtakMátr)
                {
                    Mátrix_tábla.RowCount++;
                    int i = Mátrix_tábla.RowCount - 1;
                    Mátrix_tábla.Rows[i].Cells[0].Value = rekord.Id;
                    Mátrix_tábla.Rows[i].Cells[1].Value = rekord.Fajta;
                    Mátrix_tábla.Rows[i].Cells[2].Value = rekord.Fajtamásik;
                    if (rekord.Igazság)
                        Mátrix_tábla.Rows[i].Cells[3].Value = "Igen";
                    else
                        Mátrix_tábla.Rows[i].Cells[3].Value = "Nem";
                }
                Mátrix_tábla.Visible = true;
                Mátrix_tábla.Refresh();
                Mátrix_tábla.ClearSelection();
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

        private void Mátrix_tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                int i = e.RowIndex;

                Mátrix_fajta.Text = Mátrix_tábla.Rows[i].Cells[1].Value.ToStrTrim();
                Mátrix_fajtamásik.Text = Mátrix_tábla.Rows[i].Cells[2].Value.ToStrTrim();
                Mátrix_igazság.Text = Mátrix_tábla.Rows[i].Cells[3].Value.ToStrTrim();
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

        private void Mátrix_frissít_Click(object sender, EventArgs e)
        {
            try
            {
                Mátrix_tábla_kiírás();
                Mátrix_ürítés();
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

        private void Mátrix_ürítés()
        {
            try
            {
                Mátrix_fajta.Text = "";
                Mátrix_fajtamásik.Text = "";
                Mátrix_igazság.Text = "";
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

        private void Mátrix_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if ((Mátrix_fajta.Text.Trim() == "") || (Mátrix_fajtamásik.Text.Trim() == "")) return;

                List<Adat_Jármű_Takarítás_Mátrix> Adatok = KétJárműtakMátr.Lista_Adat();

                Adat_Jármű_Takarítás_Mátrix Elem = (from a in Adatok
                                                    where a.Fajta == Mátrix_fajta.Text.Trim() && a.Fajtamásik == Mátrix_fajtamásik.Text.Trim()
                                                    select a).FirstOrDefault();

                if (Elem != null)
                {
                    Adat_Jármű_Takarítás_Mátrix ADAT = new Adat_Jármű_Takarítás_Mátrix(0,
                                                                      Mátrix_fajta.Text.Trim(),
                                                                      Mátrix_fajtamásik.Text.Trim(),
                                                                      Mátrix_igazság.Text.Trim() == "Igen");
                    KétJárműtakMátr.Módosítás(ADAT);
                }
                else
                {
                    int sorszám = 1;
                    // következő id meghatározása

                    if (Adatok.Count > 0) sorszám = Adatok.Max(a => a.Id) + 1;
                    Adat_Jármű_Takarítás_Mátrix ADAT = new Adat_Jármű_Takarítás_Mátrix(sorszám,
                                                                                       Mátrix_fajta.Text.Trim(),
                                                                                       Mátrix_fajtamásik.Text.Trim(),
                                                                                       Mátrix_igazság.Text.Trim() == "Igen");
                    KétJárműtakMátr.Rögzítés(ADAT);
                }
                Mátrix_tábla_kiírás();
                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Mátrix_combo()
        {
            try
            {
                List<Adat_Jármű_Takarítás_Mátrix> Adatok = KétJárműtakMátr.Lista_Adat();
                List<string> Fajta = (from a in Adatok select a.Fajta).Distinct().ToList();

                Mátrix_fajta.Items.Clear();
                foreach (string elem in Fajta)
                    Mátrix_fajta.Items.Add(elem);


                Fajta = (from a in Adatok select a.Fajtamásik).Distinct().ToList();
                Mátrix_fajtamásik.Items.Clear();
                foreach (string elem in Fajta)
                    Mátrix_fajtamásik.Items.Add(elem);

                Mátrix_igazság.Items.Clear();
                Mátrix_igazság.Items.Add("Igen");
                Mátrix_igazság.Items.Add("Nem");

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


        #region Biztonsági másolat lapfül
        private void Honnan_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog FolderBrowserDialog1 = new FolderBrowserDialog())
                {
                    FolderBrowserDialog1.Description = "Válassz másolandó könyvtárat";
                    {
                        if (FolderBrowserDialog1.ShowDialog() == DialogResult.OK)
                        {
                            directoryTargetLocation = FolderBrowserDialog1.SelectedPath;
                            Honnan.Text = directoryTargetLocation.ToString();
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

        private void Hova_rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                using (FolderBrowserDialog FolderBrowserDialog1 = new FolderBrowserDialog())
                {
                    FolderBrowserDialog1.Description = "Másolás helyének kiválasztása";
                    {
                        if (FolderBrowserDialog1.ShowDialog() == DialogResult.OK)
                        {
                            Destinydirectory = FolderBrowserDialog1.SelectedPath;
                            Hova.Text = Destinydirectory.ToString();
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

        private void Bizt_frissít_Click(object sender, EventArgs e)
        {
            try
            {

                if (Honnan.Text.Trim() == "") return;
                if (Hova.Text.Trim() == "") return;
                Bizt_frissít.Visible = false;
                Holtart.Be(30);
                ReCreateDirectoryStructure(Honnan.Text.Trim(), Hova.Text.Trim());
                Holtart.Ki();
                Bizt_frissít.Visible = true;
                MessageBox.Show("Az adat másolás befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void ReCreateDirectoryStructure(string sourceDir, string targetDir, string rootDir = "")
        {
            try
            {
                if (rootDir.Trim() == "") rootDir = sourceDir;

                string[] folders = Directory.GetDirectories(sourceDir);
                foreach (string folder in folders)
                {
                    // létrehozzuk a könyvtárat
                    Directory.CreateDirectory(folder.Replace(rootDir, targetDir));
                    ReCreateDirectoryStructure(folder, targetDir, rootDir);
                    // be másoljuk a fájlokat
                    string[] fájlnevek = Directory.GetFiles(folder);
                    foreach (string newPath in fájlnevek)
                    {
                        string fájlnév = newPath;
                        DateTime fileSystemInfo = GetLastWriteTime(fájlnév);
                        if (Exists(fájlnév))
                        {
                            if (fileSystemInfo >= Dátumtól.Value & fileSystemInfo <= Dátumig.Value)
                            {
                                Copy(newPath, newPath.Replace(rootDir, targetDir), true);
                            }
                        }
                    }
                    Holtart.Lép();
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

        #region Csempékhez

        Ablak_Beolvasás Új_Ablak_Beolvasás;
        private void BeolvásBeállítás_Click(object sender, EventArgs e)
        {
            if (Új_Ablak_Beolvasás == null)
            {
                Új_Ablak_Beolvasás = new Ablak_Beolvasás();
                Új_Ablak_Beolvasás.FormClosed += Új_Ablak_Beolvasás_FormClosed;
                Új_Ablak_Beolvasás.Show();
            }
            else
            {
                Új_Ablak_Beolvasás.Activate();
                Új_Ablak_Beolvasás.WindowState = FormWindowState.Maximized;
            }

        }

        private void Új_Ablak_Beolvasás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Beolvasás = null;
        }

        #endregion

        private void Cmbtelephely_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                Cmbtelephely.Text = Cmbtelephely.Items[Cmbtelephely.SelectedIndex].ToStrTrim();
                if (Cmbtelephely.Text.Trim() == "") return;
                if (Program.PostásJogkör.Any(c => c != '0'))
                {

                }
                else
                {
                    GombLathatosagKezelo.Beallit(this, Cmbtelephely.Text.Trim());
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