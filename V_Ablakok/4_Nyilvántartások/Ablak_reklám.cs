using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Ablakok.Közös;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_reklám
    {
        //Másoláshoz
        DateTime Mrekezd;
        DateTime Mrevég;
        string Mreklám;
        string Mszerelvény;
        string Mméret;
        string Mvonal;
        bool MCheckBox1;
        string Mmegjegyzés;

        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_Reklám KézReklám = new Kezelő_Reklám();
        readonly Kezelő_Reklám_Napló KézReklámNapló = new Kezelő_Reklám_Napló();
        readonly Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();
        readonly Kezelő_Utasítás KézUtasítás = new Kezelő_Utasítás();
        readonly Kezelő_kiegészítő_telephely KézTelep = new Kezelő_kiegészítő_telephely();
        readonly Kezelő_Kiegészítő_Reklám KézKiegReklám = new Kezelő_Kiegészítő_Reklám();

        List<Adat_Jármű_hiba> AdatokHiba = new List<Adat_Jármű_hiba>();
        List<Adat_Jármű> AdatokJármű_Teljes = new List<Adat_Jármű>();
        List<Adat_Jármű> AdatokJármű_Telep = new List<Adat_Jármű>();
        List<Adat_Reklám> AdatokReklám = new List<Adat_Reklám>();
        List<Adat_Reklám_Napló> AdatokReklámNapló = new List<Adat_Reklám_Napló>();
        List<string> Adatok_ReklámNév = new List<string>();

        #region Alap
        public Ablak_reklám()
        {
            InitializeComponent();
            Start();
        }

        private void Ablak_reklám_Load(object sender, EventArgs e)
        {

        }
        private void Ablak_reklám_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Utasítás_Generálás?.Close();
        }
        private void Start()
        {
            Telephelyekfeltöltése();
            Naplótól.Value = DateTime.Today;
            Naplóig.Value = DateTime.Today;
            Rekezd.Value = DateTime.Today;
            Revég.Value = DateTime.Today;
            Ragaszt.Value = new DateTime(2000, 1, 1);

            Méretbetöltés();

            GombLathatosagKezelo.Beallit(this);
            Jogosultságkiosztás();
            Telephely.Text = Cmbtelephely.Text;
            Lapfülek.DrawMode = TabDrawMode.OwnerDrawFixed;
            AdatokJármű_Teljes = KézJármű.Lista_Adatok("Főmérnökség").Where(a => a.Törölt == false).OrderBy(a => a.Azonosító).ToList();
            AdatokReklám = KézReklám.Lista_Adatok();
        }

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
                if (Program.PostásTelephely == "Főmérnökség" || Program.Postás_Vezér)
                    Cmbtelephely.Text = Cmbtelephely.Items[0].ToString().Trim();
                else
                    Cmbtelephely.Text = Program.PostásTelephely;

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

        private void Button13_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\Reklám.html";
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

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Törlés.Enabled = false;
                Rögzít.Enabled = false;
                Command3.Enabled = false;
                Utasítás.Enabled = false;


                melyikelem = 89;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Rögzít.Enabled = true;
                    Utasítás.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Törlés.Enabled = true;
                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Command3.Enabled = true;
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

        private void LAPFülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
        }

        private void Fülekkitöltése()
        {
            switch (Lapfülek.SelectedIndex)
            {
                case 0:
                    {
                        Méretbetöltés();
                        break;
                    }
                case 1:
                    {
                        // Áttekintés
                        if (!Cmbtelephely.Enabled)
                        {
                            AdatokJármű_Telep = KézJármű.Lista_Adatok(Cmbtelephely.Text.Trim()).Where(a => a.Törölt == false).OrderBy(a => a.Azonosító).ToList();
                            AdatokHiba = KézHiba.Lista_Adatok(Cmbtelephely.Text.Trim());
                        }
                        Reklámnevelistázása();
                        Típusfeltöltés();
                        Telephelyfeltöltés();
                        break;
                    }

                case 2:
                    {
                        // Naplózás
                        Pályaszámfeltöltés();
                        break;
                    }

            }
        }

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
            Telephely.Text = Cmbtelephely.Text;
            AdatokJármű_Teljes = KézJármű.Lista_Adatok("Főmérnökség").Where(a => a.Törölt == false).OrderBy(a => a.Azonosító).ToList();
            AdatokReklám = KézReklám.Lista_Adatok();
        }

        private void Lapfülek_DrawItem(object sender, DrawItemEventArgs e)
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


        #region Utasítás lap
        private string UtasításSzövegTervezet()
        {
            string Válasz = "";
            try
            {
                Válasz = "A következő járműveknek az alábbi vonalakon kell futnia reklám miatt:\r\n";
                List<Adat_Jármű> AdatokJármű = (from a in AdatokJármű_Teljes
                                                where a.Üzem == Cmbtelephely.Text.Trim()
                                                select a).ToList();

                foreach (Adat_Jármű rekord in AdatokJármű)
                {
                    Adat_Reklám Elem = (from a in AdatokReklám
                                        where a.Azonosító == rekord.Azonosító
                                        select a).FirstOrDefault();
                    if (Elem != null)
                    {

                        if (Elem.Reklámneve != "")
                        {
                            if (Elem.Reklámneve != "*")
                            {
                                string szöveg = $"{rekord.Azonosító}-nek a {Elem.Viszonylat}-on kell közlednie {Elem.Kezdődátum:yyyy.MM.dd}-tól ";
                                szöveg += $"{Elem.Befejeződátum:yyyy.MM.dd}-ig a reklám szövege: {Elem.Reklámneve}";
                                Válasz += szöveg + "\r\n";
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
            return Válasz;
        }


        Ablak_Utasítás_Generálás Új_Ablak_Utasítás_Generálás;
        private void Utasítás_Click(object sender, EventArgs e)
        {
            Új_Ablak_Utasítás_Generálás?.Close();

            Új_Ablak_Utasítás_Generálás = new Ablak_Utasítás_Generálás(Cmbtelephely.Text.Trim(), UtasításSzövegTervezet());
            Új_Ablak_Utasítás_Generálás.FormClosed += Ablak_Utasítás_Generálás_FormClosed;
            Új_Ablak_Utasítás_Generálás.Show();
        }

        private void Ablak_Utasítás_Generálás_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Utasítás_Generálás = null;
        }
        #endregion


        #region Napló
        private void Pályaszámfeltöltés()
        {
            try
            {
                List<Adat_Jármű> AdatokJármű = (from a in AdatokJármű_Teljes
                                                where a.Üzem == Cmbtelephely.Text.Trim()
                                                select a).ToList();
                ListPályaszám.Items.Clear();

                foreach (Adat_Jármű Elem in AdatokJármű)
                    ListPályaszám.Items.Add(Elem.Azonosító);
                ListPályaszám.Refresh();
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

        private void Command5_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\Napló\Reklámnapló{Naplótól.Value.Year}.mdb";
                AdatokReklámNapló = KézReklámNapló.Lista_Adatok(Naplótól.Value.Year);

                TáblaNapló.Rows.Clear();
                TáblaNapló.Columns.Clear();
                TáblaNapló.Refresh();
                TáblaNapló.Visible = false;
                TáblaNapló.ColumnCount = 13;

                // fejléc elkészítése
                TáblaNapló.Columns[0].HeaderText = "Psz";
                TáblaNapló.Columns[0].Width = 70;
                TáblaNapló.Columns[1].HeaderText = "Reklám neve";
                TáblaNapló.Columns[1].Width = 250;
                TáblaNapló.Columns[2].HeaderText = "Kezdete";
                TáblaNapló.Columns[2].Width = 120;
                TáblaNapló.Columns[3].HeaderText = "Vége";
                TáblaNapló.Columns[3].Width = 120;
                TáblaNapló.Columns[4].HeaderText = "Mérete";
                TáblaNapló.Columns[4].Width = 150;
                TáblaNapló.Columns[5].HeaderText = "Szerelvény";
                TáblaNapló.Columns[5].Width = 150;
                TáblaNapló.Columns[6].HeaderText = "Viszonylat";
                TáblaNapló.Columns[6].Width = 100;
                TáblaNapló.Columns[7].HeaderText = "Ragasztási tilalom";
                TáblaNapló.Columns[7].Width = 100;
                TáblaNapló.Columns[8].HeaderText = "Megjegyzés";
                TáblaNapló.Columns[8].Width = 200;
                TáblaNapló.Columns[9].HeaderText = "Típus";
                TáblaNapló.Columns[9].Width = 80;
                TáblaNapló.Columns[10].HeaderText = "Telephely";
                TáblaNapló.Columns[10].Width = 120;
                TáblaNapló.Columns[11].HeaderText = "Módosító";
                TáblaNapló.Columns[11].Width = 120;
                TáblaNapló.Columns[12].HeaderText = "Mód. dátum";
                TáblaNapló.Columns[12].Width = 170;

                List<Adat_Reklám_Napló> Adatok;
                if (ListPályaszám.Text.Trim() == "")
                    Adatok = (from a in AdatokReklámNapló
                              where a.Mikor >= Naplótól.Value && a.Mikor <= Naplóig.Value.AddDays(1)
                              orderby a.Id, a.Azonosító
                              select a).ToList();
                else
                    Adatok = (from a in AdatokReklámNapló
                              where a.Mikor >= Naplótól.Value
                              && a.Mikor <= Naplóig.Value.AddDays(1)
                              && a.Azonosító == ListPályaszám.Text.Trim()
                              orderby a.Id, a.Azonosító
                              select a).ToList();
                foreach (Adat_Reklám_Napló rekord in Adatok)
                {
                    TáblaNapló.RowCount++;
                    int i = TáblaNapló.RowCount - 1;
                    TáblaNapló.Rows[i].Cells[0].Value = rekord.Azonosító;
                    TáblaNapló.Rows[i].Cells[1].Value = rekord.Reklámneve;
                    TáblaNapló.Rows[i].Cells[2].Value = rekord.Kezdődátum.ToShortDateString();
                    TáblaNapló.Rows[i].Cells[3].Value = rekord.Befejeződátum.ToShortDateString();
                    TáblaNapló.Rows[i].Cells[4].Value = rekord.Reklámmérete;
                    TáblaNapló.Rows[i].Cells[5].Value = rekord.Szerelvény;
                    TáblaNapló.Rows[i].Cells[6].Value = rekord.Viszonylat;
                    TáblaNapló.Rows[i].Cells[7].Value = rekord.Ragasztásitilalom.ToShortDateString();
                    if (rekord.Ragasztásitilalom > DateTime.Today) TáblaNapló.Rows[i].Cells[7].Style.BackColor = Color.Red;
                    TáblaNapló.Rows[i].Cells[8].Value = rekord.Megjegyzés;
                    TáblaNapló.Rows[i].Cells[9].Value = rekord.Típus;
                    TáblaNapló.Rows[i].Cells[10].Value = rekord.Telephely;
                    TáblaNapló.Rows[i].Cells[11].Value = rekord.Módosító;
                    TáblaNapló.Rows[i].Cells[12].Value = rekord.Mikor;
                }
                TáblaNapló.Visible = true;
                TáblaNapló.Refresh();
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

        private void Command6_Click(object sender, EventArgs e)
        {
            try
            {
                if (TáblaNapló.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Reklámnapló_export_{Program.PostásNév.Trim()}-{DateTime.Today:yyyyMMddhhmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, TáblaNapló);
                MyE.Megnyitás(fájlexc);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Áttekintés
        private void Reklámnevelistázása()
        {
            Reklámnevelista.Items.Clear();

            Adatok_ReklámNév = (List<string>)(from a in AdatokReklám
                                              orderby a.Reklámneve
                                              select a.Reklámneve).Distinct().ToList();
            Reklámnevelista.BeginUpdate();
            foreach (string Elem in Adatok_ReklámNév)
                Reklámnevelista.Items.Add(Elem);
            Reklámnevelista.EndUpdate();
            Reklámnevelista.Refresh();
        }

        private void Típusfeltöltés()
        {
            Típuslista.Items.Clear();
            Típuslista.BeginUpdate();
            List<string> Adatok = (List<string>)AdatokJármű_Teljes.Select(x => x.Valóstípus).Distinct().ToList();
            foreach (string Elem in Adatok)
                Típuslista.Items.Add(Elem);
            Típuslista.EndUpdate();
            Típuslista.Refresh();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Reklám_lekérdezés();
        }

        private void Reklám_lekérdezés()
        {
            try
            {
                if (TelephelyList.CheckedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy telephely sem.");
                if (Típuslista.CheckedItems.Count < 1) throw new HibásBevittAdat("Nincs kijelölve egy típus sem.");

                Holtart.Be();
                //Telephelyek tartozó pályaszámok
                List<Adat_Jármű> JárművekTelep = new List<Adat_Jármű>();
                for (int i = 0; i < TelephelyList.CheckedItems.Count; i++)
                {
                    List<Adat_Jármű> Jármű = (from a in AdatokJármű_Teljes
                                              where a.Üzem == TelephelyList.CheckedItems[i].ToString()
                                              select a).ToList();
                    if (Jármű != null) JárművekTelep.AddRange(Jármű);
                }
                JárművekTelep.OrderBy(a => a.Azonosító);

                //Típusra szűkítünk
                List<Adat_Jármű> Járművek = new List<Adat_Jármű>();
                for (int i = 0; i < Típuslista.CheckedItems.Count; i++)
                {
                    List<Adat_Jármű> Jármű = (from a in JárművekTelep
                                              where a.Valóstípus == Típuslista.CheckedItems[i].ToString()
                                              select a).ToList();
                    if (Jármű != null) Járművek.AddRange(Jármű);
                }
                Járművek.OrderBy(a => a.Azonosító);

                TáblaFejléc();
                foreach (Adat_Jármű Elem in Járművek)
                {
                    Adat_Reklám rekord = (from a in AdatokReklám
                                          where a.Azonosító == Elem.Azonosító
                                          select a).FirstOrDefault();
                    int i;
                    if (Reklámnevelista.CheckedItems.Count == 0)
                    {
                        //Ha nincs kijelölve akkor mindent kiír
                        Tábla.RowCount++;
                        i = Tábla.RowCount - 1;
                        Tábla.Rows[i].Cells[0].Value = Elem.Azonosító;
                        Tábla.Rows[i].Cells[9].Value = Elem.Valóstípus;
                        Tábla.Rows[i].Cells[10].Value = Elem.Üzem;
                        Reklám_Adatok(i, rekord);
                        //Ha egy telephelyről kérdezzük, akkor kiírjuk a jármű státusát és hibáit
                        if (!Cmbtelephely.Enabled) Telephelyi_Kiírás(i, Elem.Azonosító);
                    }
                    else
                    {
                        if (rekord != null)
                        {
                            foreach (string item in Reklámnevelista.CheckedItems)
                            {
                                if (item == rekord.Reklámneve)
                                {
                                    Tábla.RowCount++;
                                    i = Tábla.RowCount - 1;
                                    Tábla.Rows[i].Cells[0].Value = Elem.Azonosító;
                                    Tábla.Rows[i].Cells[9].Value = Elem.Valóstípus;
                                    Tábla.Rows[i].Cells[10].Value = Elem.Üzem;
                                    Reklám_Adatok(i, rekord);
                                    //Ha egy telephelyről kérdezzük, akkor kiírjuk a jármű státusát és hibáit
                                    if (!Cmbtelephely.Enabled) Telephelyi_Kiírás(i, Elem.Azonosító);
                                }
                            }
                        }
                    }
                    Holtart.Lép();
                }
                Tábla.Refresh();
                Tábla.ClearSelection();
                Tábla.Visible = true;
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

        private void Telephelyi_Kiírás(int i, string Azonosító)
        {
            Adat_Jármű EgyKocsi = (from a in AdatokJármű_Telep
                                   where a.Azonosító == Azonosító
                                   select a).FirstOrDefault();
            Jármű_Státusa(i, EgyKocsi);



            List<Adat_Jármű_hiba> EgyHiba = (from a in AdatokHiba
                                             where a.Azonosító == Azonosító
                                             select a).ToList();
            Jármű_Hibái(i, EgyHiba);
        }

        private void Jármű_Hibái(int i, List<Adat_Jármű_hiba> EgyHiba)
        {
            Tábla.Rows[i].Cells[11].Value = "";
            if (EgyHiba != null)
            {
                foreach (Adat_Jármű_hiba Hiba in EgyHiba)
                {
                    Tábla.Rows[i].Cells[11].Value += Hiba.Hibaleírása;
                }
            }
        }

        private void Jármű_Státusa(int i, Adat_Jármű EgyKocsi)
        {
            if (EgyKocsi != null)
            {
                Tábla.Rows[i].Cells[12].Value = EgyKocsi.Státus;
                switch (EgyKocsi.Státus)
                {
                    case 0:
                        {
                            // zöld
                            Tábla.Rows[i].Cells[0].Style.BackColor = Color.Green;
                            break;
                        }
                    case 1:
                        {
                            // szürke
                            Tábla.Rows[i].Cells[0].Style.BackColor = Color.Gray;
                            break;
                        }
                    case 2:
                        {
                            // kék
                            Tábla.Rows[i].Cells[0].Style.BackColor = Color.Blue;
                            break;
                        }
                    case 3:
                        {
                            // sárga
                            Tábla.Rows[i].Cells[0].Style.BackColor = Color.Yellow;
                            break;
                        }
                    case 4:
                        {
                            // piros
                            Tábla.Rows[i].Cells[0].Style.BackColor = Color.Red;
                            break;
                        }
                }
            }
        }

        private void Reklám_Adatok(int i, Adat_Reklám rekord)
        {
            if (rekord != null)
            {
                Tábla.Rows[i].Cells[1].Value = rekord.Reklámneve;
                Tábla.Rows[i].Cells[2].Value = rekord.Kezdődátum.ToString("yyyy.MM.dd");
                Tábla.Rows[i].Cells[3].Value = rekord.Befejeződátum.ToString("yyyy.MM.dd");
                Tábla.Rows[i].Cells[4].Value = rekord.Reklámmérete;
                Tábla.Rows[i].Cells[5].Value = rekord.Szerelvény;
                Tábla.Rows[i].Cells[6].Value = rekord.Viszonylat;
                Tábla.Rows[i].Cells[7].Value = rekord.Ragasztásitilalom.ToString("yyyy.MM.dd");
                if (rekord.Ragasztásitilalom > DateTime.Today) Tábla.Rows[i].Cells[7].Style.BackColor = Color.Red;
                Tábla.Rows[i].Cells[8].Value = rekord.Megjegyzés;
                Tábla.Rows[i].Cells[13].Value = rekord.Szerelvényben;
            }
        }

        private void TáblaFejléc()
        {
            Tábla.Rows.Clear();
            Tábla.Columns.Clear();
            Tábla.Refresh();
            Tábla.Visible = false;
            Tábla.ColumnCount = 14;
            // fejléc elkészítése
            Tábla.Columns[0].HeaderText = "Psz";
            Tábla.Columns[0].Width = 70;
            Tábla.Columns[1].HeaderText = "Reklám neve";
            Tábla.Columns[1].Width = 250;
            Tábla.Columns[2].HeaderText = "Kezdete";
            Tábla.Columns[2].Width = 100;
            Tábla.Columns[3].HeaderText = "Vége";
            Tábla.Columns[3].Width = 100;
            Tábla.Columns[4].HeaderText = "Mérete";
            Tábla.Columns[4].Width = 150;
            Tábla.Columns[5].HeaderText = "Szerelvény";
            Tábla.Columns[5].Width = 150;
            Tábla.Columns[6].HeaderText = "Viszonylat";
            Tábla.Columns[6].Width = 100;
            Tábla.Columns[7].HeaderText = "Ragasztási tilalom";
            Tábla.Columns[7].Width = 100;
            Tábla.Columns[8].HeaderText = "Megjegyzés";
            Tábla.Columns[8].Width = 200;
            Tábla.Columns[9].HeaderText = "Típus";
            Tábla.Columns[9].Width = 80;
            Tábla.Columns[10].HeaderText = "Telephely";
            Tábla.Columns[10].Width = 120;
            Tábla.Columns[11].HeaderText = "Hiba";
            Tábla.Columns[11].Width = 200;
            Tábla.Columns[12].HeaderText = "Státus";
            Tábla.Columns[12].Width = 80;
            Tábla.Columns[13].HeaderText = "Szerelvényben";
            Tábla.Columns[13].Width = 80;
        }

        private void Telephelyfeltöltés()
        {
            try
            {
                TelephelyList.Items.Clear();
                List<Adat_kiegészítő_telephely> Telephelyek = KézTelep.Lista_Adatok();

                TelephelyList.BeginUpdate();
                foreach (Adat_kiegészítő_telephely Elem in Telephelyek)
                {
                    TelephelyList.Items.Add(Elem.Telephelykönyvtár);
                    if (Cmbtelephely.Text == Elem.Telephelykönyvtár) TelephelyList.SetItemChecked(TelephelyList.Items.Count - 1, true);
                }
                TelephelyList.EndUpdate();
                TelephelyList.Refresh();
                //telephelyi lekérdezés, akkor másik telephelyet nem lát
                if (Cmbtelephely.Enabled)
                {
                    TelephelyList.Visible = true;
                    Telephely_Mind.Visible = true;
                    Telephely_Semmi.Visible = true;
                }
                else
                {
                    TelephelyList.Visible = false;
                    Telephely_Mind.Visible = false;
                    Telephely_Semmi.Visible = false;
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

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                Üresmezők();
                {
                    Pályaszám.Text = Tábla.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                    if (Tábla.Rows[e.RowIndex].Cells[1].Value == null)
                        Reklám.Text = "";
                    else
                        Reklám.Text = Tábla.Rows[e.RowIndex].Cells[1].Value.ToString().Trim();


                    if (Tábla.Rows[e.RowIndex].Cells[2].Value != null)
                        Rekezd.Value = Convert.ToDateTime(Tábla.Rows[e.RowIndex].Cells[2].Value);
                    else
                        Rekezd.Value = DateTime.Today;

                    if (Tábla.Rows[e.RowIndex].Cells[3].Value != null)
                        Revég.Value = Convert.ToDateTime(Tábla.Rows[e.RowIndex].Cells[3].Value);
                    else
                        Revég.Value = DateTime.Today;


                    if (Tábla.Rows[e.RowIndex].Cells[4].Value == null)
                        Méret.Text = "";
                    else
                        Méret.Text = Tábla.Rows[e.RowIndex].Cells[4].Value.ToString().Trim();


                    if (Tábla.Rows[e.RowIndex].Cells[5].Value == null)
                        Szerelvény.Text = "";
                    else
                        Szerelvény.Text = Tábla.Rows[e.RowIndex].Cells[5].Value.ToString().Trim();


                    if (Tábla.Rows[e.RowIndex].Cells[6].Value == null)
                        Vonal.Text = "";
                    else
                        Vonal.Text = Tábla.Rows[e.RowIndex].Cells[6].Value.ToString().Trim();


                    if (Tábla.Rows[e.RowIndex].Cells[7].Value != null)
                        Ragaszt.Value = Convert.ToDateTime(Tábla.Rows[e.RowIndex].Cells[7].Value);
                    else
                        Ragaszt.Value = Convert.ToDateTime("2000.01.01");

                    if (Ragaszt.Value > DateTime.Today)
                    {
                        Panel2.BackColor = Color.Red;
                        Törlés.Visible = false;
                        Rögzít.Visible = false;
                    }
                    else
                    {
                        Panel2.BackColor = Color.Green;
                        Törlés.Visible = true;
                        Rögzít.Visible = true;
                    }

                    if (Tábla.Rows[e.RowIndex].Cells[8].Value == null)
                        Megjegyzés.Text = "";
                    else
                        Megjegyzés.Text = Tábla.Rows[e.RowIndex].Cells[8].Value.ToString().Trim();


                    if (Tábla.Rows[e.RowIndex].Cells[13].Value != null)
                        CheckBox1.Checked = true;
                    else
                        CheckBox1.Checked = false;


                    if (Tábla.Rows[e.RowIndex].Cells[9].Value == null)
                        Típus.Text = "";
                    else
                        Típus.Text = Tábla.Rows[e.RowIndex].Cells[9].Value.ToString().Trim();

                }
                Lapfülek.SelectedIndex = 0;
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


        #region Rögzítés
        private void Másol_Click(object sender, EventArgs e)
        {
            Mrekezd = Rekezd.Value;
            Mrevég = Revég.Value;
            Mreklám = Reklám.Text.Trim();
            Mszerelvény = Szerelvény.Text.Trim();
            Mméret = Méret.Text.Trim();
            Mvonal = Vonal.Text.Trim();
            MCheckBox1 = CheckBox1.Checked;
            Mmegjegyzés = Megjegyzés.Text.Trim();
        }

        private void Beilleszt_Click(object sender, EventArgs e)
        {
            Rekezd.Value = Mrekezd;
            Revég.Value = Mrevég;
            Reklám.Text = Mreklám;
            Szerelvény.Text = Mszerelvény;
            Méret.Text = Mméret;
            Vonal.Text = Mvonal;
            CheckBox1.Checked = MCheckBox1;
            Megjegyzés.Text = Mmegjegyzés;
        }

        private void Üresmezők()
        {
            Ragaszt.Value = new DateTime(2000, 1, 1);
            Rekezd.Value = DateTime.Today;
            Revég.Value = DateTime.Today;
            Reklám.Text = "";
            Vonal.Text = "";
            Méret.Text = "";
            Szerelvény.Text = "";
            CheckBox1.Checked = false;
            Megjegyzés.Text = "";
        }

        private void ÜresmezőkTörlés()
        {
            Ragaszt.Value = new DateTime(2000, 1, 1);
            Rekezd.Value = DateTime.Today;
            Revég.Value = DateTime.Today;
            Reklám.Text = "*";
            Vonal.Text = "*";
            Méret.Text = "*";
            Szerelvény.Text = "*";
            CheckBox1.Checked = false;
            Megjegyzés.Text = "*";
            Típus.Text = "*";
        }

        private void Listáz_Click(object sender, EventArgs e)
        {
            Listázza_pályaszámadatait();
        }

        private void Listázza_pályaszámadatait()
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("Nincs kijelölve pályaszám");

                // Leellenőrizzük, hogy van-e ilyen kocsi a telephelyen
                Adat_Jármű EgyJármű = (from a in AdatokJármű_Teljes
                                       where a.Azonosító == Pályaszám.Text.Trim()
                                       && a.Üzem == Cmbtelephely.Text.Trim()
                                       select a).FirstOrDefault();

                Üresmezők();
                if (EgyJármű == null)
                {

                    throw new HibásBevittAdat("A telephelyen nincs ilyen jármű!");
                }
                else
                    Típus.Text = EgyJármű.Valóstípus;

                Adat_Reklám EgyReklám = (from a in AdatokReklám
                                         where a.Azonosító == Pályaszám.Text.Trim()
                                         select a).FirstOrDefault();

                if (EgyReklám != null)
                {
                    Ragaszt.Value = EgyReklám.Ragasztásitilalom;
                    Rekezd.Value = EgyReklám.Kezdődátum;
                    Revég.Value = EgyReklám.Befejeződátum;
                    Reklám.Text = EgyReklám.Reklámneve;
                    Vonal.Text = EgyReklám.Viszonylat;
                    Méret.Text = EgyReklám.Reklámmérete;
                    Szerelvény.Text = EgyReklám.Szerelvény;
                    if (EgyReklám.Szerelvényben == 0)
                        CheckBox1.Checked = false;
                    else
                        CheckBox1.Checked = true;
                    Megjegyzés.Text = EgyReklám.Megjegyzés;
                }
                else
                {

                    if (Ragaszt.Value > DateTime.Today)
                    {
                        Panel2.BackColor = Color.Red;
                        Törlés.Visible = false;
                        Rögzít.Visible = false;
                    }
                    else
                    {
                        Panel2.BackColor = Color.Green;
                        Törlés.Visible = true;
                        Rögzít.Visible = true;
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

        private void Command3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("A pályaszám mező nem lehet üres.");
                // pályaszám ellenőrzése

                Adat_Jármű EgyJármű = (from a in AdatokJármű_Teljes
                                       where a.Azonosító == Pályaszám.Text.Trim()
                                       select a).FirstOrDefault() ?? throw new HibásBevittAdat("Nincs ilyen pályaszámú jármű a nyilvántartásban.");

                Adat_Reklám EgyReklám = (from a in AdatokReklám
                                         where a.Azonosító == Pályaszám.Text.Trim()
                                         select a).FirstOrDefault();
                if (EgyReklám == null)
                {
                    // ha nincs akkor hozzáad egy sort
                    Adat_Reklám ADAT = new Adat_Reklám(
                        Pályaszám.Text.Trim(),
                        Telephely.Text.Trim(),
                        Ragaszt.Value,
                        Típus.Text.Trim());
                    KézReklám.RögzítésTilalom(ADAT);
                }
                else
                {
                    string reklámneve = EgyReklám.Reklámneve;
                    if (reklámneve == "*" || reklámneve.Trim() == "")
                    {
                        Adat_Reklám ADAT = new Adat_Reklám(
                            Pályaszám.Text.Trim(),
                            Ragaszt.Value);
                        KézReklám.MódosításTilalom(ADAT);
                    }
                    else
                        throw new HibásBevittAdat("A ragasztási tilalmat csak leszedett reklámmal lehet rögzíteni.");

                    NaplózzukRögzítést();
                }
                AdatokReklám = KézReklám.Lista_Adatok();
                Listázza_pályaszámadatait();
                MessageBox.Show("A ragasztási tilalom rögzítése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void NaplózzukRögzítést()
        {
            try
            {
                // naplózás
                Adat_Reklám_Napló ADAT = new Adat_Reklám_Napló(
                      Pályaszám.Text.Trim(),
                      Rekezd.Value,
                      Revég.Value,
                      MyF.Szöveg_Tisztítás(Reklám.Text.Trim()),
                      Vonal.Text.Trim(),
                      Telephely.Text.Trim(),
                      Méret.Text.Trim(),
                      CheckBox1.Checked ? 1 : 0,
                      CheckBox1.Checked ? Szerelvény.Text.Trim() : "*",
                      Ragaszt.Value,
                      MyF.Szöveg_Tisztítás(Megjegyzés.Text.Trim()),
                      Típus.Text.Trim(),
                      0, // id, itt 0 mert új rekord
                      DateTime.Now,
                      Program.PostásNév.Trim());
                KézReklámNapló.Rögzítés(DateTime.Now.Year, ADAT);
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

        private void Méretbetöltés()
        {
            try
            {
                Méret.Items.Clear();
                List<Adat_Kiegészítő_Reklám> Adatok = KézKiegReklám.Lista_Adatok();
                foreach (Adat_Kiegészítő_Reklám Elem in Adatok)
                    Méret.Items.Add(Elem.Méret);
                Méret.Refresh();
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

        private void Törlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("A pályaszám mező nem lehet üres.");

                // pályaszám ellenőrzése
                Adat_Jármű EgyJármű = (from a in AdatokJármű_Teljes
                                       where a.Azonosító == Pályaszám.Text.Trim()
                                       select a).FirstOrDefault() ?? throw new HibásBevittAdat("Nincs ilyen pályaszámú jármű a nyilvántartásban.");

                Adat_Reklám EgyReklám = (from a in AdatokReklám
                                         where a.Azonosító == Pályaszám.Text.Trim()
                                         select a).FirstOrDefault();

                if (EgyReklám != null)
                {
                    ÜresmezőkTörlés();
                    Reklám_Módosítás();
                    NaplózzukRögzítést();
                    AdatokReklám = KézReklám.Lista_Adatok();
                    Üresmezők();
                    Listázza_pályaszámadatait();
                    AdatokReklám = KézReklám.Lista_Adatok();
                    MessageBox.Show("A reklám törlése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    throw new HibásBevittAdat("Nincs ilyen pályaszámú járművön reklám, így nem lehet törölni.");
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

        private void Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pályaszám.Text.Trim() == "") throw new HibásBevittAdat("A pályaszám mező nem lehet üres.");

                // pályaszám ellenőrzése
                Adat_Jármű EgyJármű = (from a in AdatokJármű_Teljes
                                       where a.Azonosító == Pályaszám.Text.Trim()
                                       select a).FirstOrDefault() ?? throw new HibásBevittAdat("Nincs ilyen pályaszámú jármű a nyilvántartásban.");

                if (Reklám.Text.Trim() == "") throw new HibásBevittAdat("Reklám neve nem lehet üres");
                if (Vonal.Text.Trim() == "") throw new HibásBevittAdat("A vonalat meg kell adni.");
                if (Méret.Text.Trim() == "") throw new HibásBevittAdat("A reklám méretét meg kell adni.");
                if (Típus.Text.Trim() == "") throw new HibásBevittAdat("A jármű típusát meg kell adni.");
                if (Megjegyzés.Text.Trim() == "") Megjegyzés.Text = "*";
                if (CheckBox1.Checked == true && Szerelvény.Text.Trim() == "") throw new HibásBevittAdat("Ha szerelvényben közlekedik akkor meg kell adni a szerelvény járműveit.");
                if (Rekezd.Value > Revég.Value) throw new HibásBevittAdat("A reklám kihelyezés kezdetének kisebbnek kell lennie a befejezési dátumnál.");

                Adat_Reklám EgyReklám = (from a in AdatokReklám
                                         where a.Azonosító == Pályaszám.Text.Trim()
                                         select a).FirstOrDefault();
                if (EgyReklám == null)
                {
                    // ha nincs akkor hozzáad egy sort
                    Adat_Reklám ADAT = new Adat_Reklám(
                         Pályaszám.Text.Trim(),
                         Telephely.Text.Trim(),
                         Ragaszt.Value,
                         Típus.Text.Trim());
                    KézReklám.RögzítésTilalom(ADAT);
                }
                else
                {
                    if (EgyReklám.Ragasztásitilalom > Rekezd.Value) throw new HibásBevittAdat("A járművön ragasztási tilalom van, ezért nem lehet plakátot tenni rá.");
                }
                Reklám_Módosítás();
                NaplózzukRögzítést();                // naplózás
                AdatokReklám = KézReklám.Lista_Adatok();
                Üresmezők();
                Listázza_pályaszámadatait();
                AdatokReklám = KézReklám.Lista_Adatok();
                MessageBox.Show("A reklám rögzítése megtörtént!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Reklám_Módosítás()
        {
            try
            {
                Adat_Reklám ADAT = new Adat_Reklám(
                    Pályaszám.Text.Trim(),
                    Rekezd.Value,
                    Revég.Value,
                    MyF.Szöveg_Tisztítás(Reklám.Text.Trim()),
                    Vonal.Text.Trim(),
                    Telephely.Text.Trim(),
                    Méret.Text.Trim(),
                    CheckBox1.Checked ? 1 : 0,
                    CheckBox1.Checked ? Szerelvény.Text.Trim() : "*",
                    Ragaszt.Value,
                    MyF.Szöveg_Tisztítás(Megjegyzés.Text.Trim()),
                    Típus.Text.Trim());
                KézReklám.Módosítás(ADAT);
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

        private void Excellekérdezés_Click(object sender, EventArgs e)
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
                    FileName = $"Reklámnapló_export_{Program.PostásNév.Trim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, Tábla);
                MyE.Megnyitás(fájlexc);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Mind és Semmi gombok
        private void Reklám_Mind_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Reklámnevelista.Items.Count; i++)
                Reklámnevelista.SetItemChecked(i, true);
        }

        private void Reklám_Semmi_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Reklámnevelista.Items.Count; i++)
                Reklámnevelista.SetItemChecked(i, false);
        }

        private void Típus_Mind_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Típuslista.Items.Count; i++)
                Típuslista.SetItemChecked(i, true);
        }

        private void Típus_Semmi_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Típuslista.Items.Count; i++)
                Típuslista.SetItemChecked(i, false);
        }

        private void Telephely_Mind_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < TelephelyList.Items.Count; i++)
                TelephelyList.SetItemChecked(i, true);
        }

        private void Telephely_Semmi_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < TelephelyList.Items.Count; i++)
                TelephelyList.SetItemChecked(i, false);
        }
        #endregion


        #region Egér mozgás és listák
        private void Reklámnevelista_MouseHover(object sender, EventArgs e)
        {
            Reklámnevelista.Height = 500;
        }

        private void Reklámnevelista_MouseEnter(object sender, EventArgs e)
        {
            Reklámnevelista.Height = 25;
        }

        private void Reklámnevelista_MouseLeave(object sender, EventArgs e)
        {
            Reklámnevelista.Height = 25;
        }

        private void Típuslista_MouseHover(object sender, EventArgs e)
        {
            Típuslista.Height = 500;
        }

        private void Típuslista_MouseEnter(object sender, EventArgs e)
        {
            Típuslista.Height = 25;
        }

        private void Típuslista_MouseLeave(object sender, EventArgs e)
        {
            Típuslista.Height = 25;
        }

        private void TelephelyList_MouseEnter(object sender, EventArgs e)
        {
            TelephelyList.Height = 25;
        }

        private void TelephelyList_MouseHover(object sender, EventArgs e)
        {
            TelephelyList.Height = 500;
        }

        private void TelephelyList_MouseLeave(object sender, EventArgs e)
        {
            TelephelyList.Height = 25;
        }

        #endregion


    }
}