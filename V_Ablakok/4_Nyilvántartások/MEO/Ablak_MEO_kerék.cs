using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Ablakok.MEO;
using Villamos.Villamos_Adatszerkezet;
using static System.Windows.Forms.CheckedListBox;
using MyF = Függvénygyűjtemény;
using MyE = Villamos.Module_Excel;
using MyX = Villamos.MyClosedXML_Excel;

namespace Villamos
{
    public partial class Ablak_MEO_kerék
    {
        #region Kezelők
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_MEO_KerékMérés KézKerékMérés = new Kezelő_MEO_KerékMérés();
        readonly Kezelő_MEO_Tábla KézKerékMérés2 = new Kezelő_MEO_Tábla();
        readonly Kezelő_kiegészítő_telephely KézKiegTelephely = new Kezelő_kiegészítő_telephely();
        readonly Kezelő_MEO_Naptábla KézNaptábla = new Kezelő_MEO_Naptábla();
        readonly Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();
        readonly Kezelő_Belépés_Jogosultságtábla KézJogTábla = new Kezelő_Belépés_Jogosultságtábla();
        #endregion


        #region Listák
        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        List<Adat_MEO_KerékMérés> AdatokKerékMérés = new List<Adat_MEO_KerékMérés>();
        List<Adat_MEO_Tábla> AdatokKerékMérés2 = new List<Adat_MEO_Tábla>();
        List<Adat_kiegészítő_telephely> AdatokKiegTelephely = new List<Adat_kiegészítő_telephely>();
        readonly List<Adat_Jármű_hiba> AdatokHIBA = new List<Adat_Jármű_hiba>();
        #endregion


        Adat_Jármű AdatJármű;
        Adat_MEO_Naptábla AdatNaptábla;

        #region Alap
        public Ablak_MEO_kerék()
        {
            InitializeComponent();
            Start();
        }

        private void Start()
        {
            Dátum.Value = DateTime.Today;
            Dátumig.Value = DateTime.Today;
            Dátumtól.Value = new DateTime(DateTime.Today.Year, 1, 1);

            //Ha az első karakter "R" akkor az új jogosultságkiosztást használjuk
            //ha nem akkor a régit használjuk
            if (Program.PostásJogkör.Substring(0, 1) == "R")
                GombLathatosagKezelo.Beallit(this, "Főmérnökség");
            else
                Jogosultságkiosztás();

            AdatokKerékMérés_Feltöltés();
            Fülekkitöltése();
            Lapfülek.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        private void Ablak_MEO_kerék_Load(object sender, EventArgs e)
        {

        }

        private void Ablak_MEO_kerék_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kerék_Konverter?.Close();
        }

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Töröl.Enabled = false;
                Rögzít.Enabled = false;

                Btn_Jog_Torles.Enabled = false;
                Btn_Jog_Tipus_Rogzit.Enabled = false;
                Btn_Jog_Hatarnap_Rogzit.Enabled = false;

                // csak főmérnökségi belépéssel törölhető
                if (Program.PostásTelephely == "Főmérnökség")
                {
                    Btn_Jog_Torles.Visible = true;
                    Btn_Jog_Tipus_Rogzit.Visible = true;
                    Btn_Jog_Hatarnap_Rogzit.Visible = true;
                    Töröl.Visible = true;
                    Rögzít.Visible = true;
                }
                else
                {
                    Btn_Jog_Torles.Visible = false;
                    Btn_Jog_Tipus_Rogzit.Visible = false;
                    Btn_Jog_Hatarnap_Rogzit.Visible = false;
                    Töröl.Visible = false;
                    Rögzít.Visible = false;
                }

                melyikelem = 180;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Töröl.Enabled = true;
                    Rögzít.Enabled = true;
                }
                // módosítás 2 
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Btn_Jog_Torles.Enabled = true;
                    Btn_Jog_Tipus_Rogzit.Enabled = true;
                    Btn_Jog_Hatarnap_Rogzit.Enabled = true;
                }
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
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

        private void Fülekkitöltése()
        {
            switch (Lapfülek.SelectedIndex)
            {
                case 0:
                    {
                        Pályaszámfeltöltés();
                        Telephelyfeltöltés();
                        break;
                    }
                case 1:
                    {
                        Telephelyfeltöltéslist();
                        Típuslistafeltöltés();
                        Rögzítőfeltöltés();
                        break;
                    }
                case 2:
                    {
                        Típuslista1feltöltés();
                        Telephelyfeltöltéslist1();
                        Napkiirás();
                        break;
                    }
                case 3:
                    {
                        Névfeltöltés();
                        Típusfeltöltés();
                        Napkiirás();
                        break;
                    }
            }
        }

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\MEO_kerék.html";
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

        private void LapFülek_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
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
                e.Graphics.DrawString(SelectedTab.Text, e.Font, BlackTextBrush, HeaderRect, sf);
            // Munka kész – dobja ki a keféket
            BlackTextBrush.Dispose();
        }
        #endregion


        #region Jogosultság kiosztás
        private void Btn_Jog_Frissit_Click(object sender, EventArgs e)
        {
            Felhasználólistáz();
        }

        private void Felhasználólistáz()
        {
            try
            {
                List<Adat_MEO_Tábla> AdatokÖ = KézKerékMérés2.Lista_Adatok();
                List<Adat_MEO_Tábla> Adatok;
                if (Rögzítő.Text.Trim() != "")
                    Adatok = (from a in AdatokÖ
                              where a.Név == Rögzítő.Text.Trim()
                              select a).ToList();
                else
                    Adatok = AdatokÖ;

                FelhasználóTábla.Rows.Clear();
                FelhasználóTábla.Columns.Clear();
                FelhasználóTábla.Refresh();
                FelhasználóTábla.Visible = false;
                FelhasználóTábla.ColumnCount = 2;

                // fejléc elkészítése
                FelhasználóTábla.Columns[0].HeaderText = "Név";
                FelhasználóTábla.Columns[0].Width = 150;
                FelhasználóTábla.Columns[1].HeaderText = "Típus";
                FelhasználóTábla.Columns[1].Width = 150;

                int i;

                foreach (Adat_MEO_Tábla rekord in Adatok)
                {
                    FelhasználóTábla.RowCount++;
                    i = FelhasználóTábla.RowCount - 1;
                    FelhasználóTábla.Rows[i].Cells[0].Value = rekord.Név;
                    FelhasználóTábla.Rows[i].Cells[1].Value = rekord.Típus;
                }

                FelhasználóTábla.Visible = true;
                FelhasználóTábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Névfeltöltés()
        {
            try
            {
                Rögzítő.Items.Clear();
                Rögzítő.BeginUpdate();

                List<Adat_Belépés_Jogosultságtábla> Adatok = KézJogTábla.Lista_Adatok("Főmérnökség");
                foreach (Adat_Belépés_Jogosultságtábla rekord in Adatok)
                {
                    string ideig = rekord.Jogkörúj1.ToStrTrim();
                    int melyikelem = 180;

                    if (ideig.Substring(melyikelem - 1, 1) == "3" ||
                         ideig.Substring(melyikelem - 1, 1) == "7" ||
                         ideig.Substring(melyikelem - 1, 1) == "b" ||
                         ideig.Substring(melyikelem - 1, 1) == "f")

                        Rögzítő.Items.Add(rekord.Név.ToStrTrim());
                }

                Rögzítő.EndUpdate();
                Rögzítő.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Típusfeltöltés()
        {
            try
            {
                AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");

                List<string> RészAdat = (from a in AdatokJármű
                                         select a.Valóstípus).Distinct().ToList();

                Típus.Items.Clear();
                Típus.Items.Add("");
                Típus.BeginUpdate();
                foreach (string rekord in RészAdat)
                    Típus.Items.Add(rekord);
                Típus.EndUpdate();
                Típus.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Jog_Hatarnap_Rogzit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Határnap.Text.ToStrTrim() == "") throw new HibásBevittAdat("Nincs kitöltve a határnap!");
                if (!int.TryParse(Határnap.Text, out int HatárNap)) throw new HibásBevittAdat("Nem megfelelő formátum a határnap!");

                AdatNaptábla = KézNaptábla.Egy_Adat();
                if (AdatNaptábla != null)
                {
                    int előző = AdatNaptábla.Id;
                    KézNaptábla.Módosítás(HatárNap, előző);
                }
                else
                    KézNaptábla.Rögzítés(HatárNap);
                MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Jog_Torles_Click(object sender, EventArgs e)
        {
            try
            {
                if (Rögzítő.Text.ToStrTrim() == "") throw new HibásBevittAdat("Nincs kitöltve a rögzítő!");
                if (Típus.Text.ToStrTrim() == "") throw new HibásBevittAdat("Nincs kitöltve a típus!");

                AdatokKerékMérés2 = KézKerékMérés2.Lista_Adatok();
                AdatokKerékMérés2 = (from a in AdatokKerékMérés2
                                     where a.Név == Rögzítő.Text.ToStrTrim()
                                     && a.Típus == Típus.Text.ToStrTrim()
                                     select a).ToList();
                if (AdatokKerékMérés2.Count != 0)
                {
                    Adat_MEO_Tábla Adat = new Adat_MEO_Tábla(Rögzítő.Text.ToStrTrim(), Típus.Text.ToStrTrim());
                    KézKerékMérés2.Törlés(Adat);
                    MessageBox.Show("Az adatok törlése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Nincs ilyen adat amit törölni lehet!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Felhasználólistáz();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Jog_Tipus_Rogzit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Rögzítő.Text.ToStrTrim() == "") throw new HibásBevittAdat("Nincs kitöltve a rögzítő!");
                if (Típus.Text.ToStrTrim() == "") throw new HibásBevittAdat("Nincs kitöltve a típus!");

                List<Adat_MEO_Tábla> AdatokÖ = KézKerékMérés2.Lista_Adatok();
                AdatokKerékMérés2 = (from a in AdatokÖ
                                     where a.Név == Rögzítő.Text.ToStrTrim()
                                     && a.Típus == Típus.Text.ToStrTrim()
                                     select a).ToList();
                if (AdatokKerékMérés2.Count != 0)
                    MessageBox.Show("Az adatok már egyszer rögzítésre kerültek!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    Adat_MEO_Tábla ADAT = new Adat_MEO_Tábla(Rögzítő.Text.ToStrTrim(), Típus.Text.ToStrTrim());
                    KézKerékMérés2.Rögzítés(ADAT);
                    MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                Felhasználólistáz();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FelhasználóTábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (FelhasználóTábla.Rows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve cella!");
                Rögzítő.Text = FelhasználóTábla.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
                Típus.Text = FelhasználóTábla.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
            }
        }

        private void Napkiirás()
        {
            try
            {
                AdatNaptábla = KézNaptábla.Egy_Adat();
                if (AdatNaptábla != null) Határnap.Text = AdatNaptábla.Id.ToStrTrim();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion


        #region Mérés rögzítzés
        private void Btn_Mérés_Rögz_Frissit_Click(object sender, EventArgs e)
        {
            Listáz();
        }

        private void Listáz()
        {
            try
            {
                DateTime előzőév = Dátum.Value.AddYears(-1);

                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 6;
                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Pályaszám";
                Tábla.Columns[0].Width = 100;
                Tábla.Columns[1].HeaderText = "Típus";
                Tábla.Columns[1].Width = 150;
                Tábla.Columns[2].HeaderText = "Telephely";
                Tábla.Columns[2].Width = 150;
                Tábla.Columns[3].HeaderText = "Dátum";
                Tábla.Columns[3].Width = 200;
                Tábla.Columns[4].HeaderText = "Rögzítette";
                Tábla.Columns[4].Width = 200;
                Tábla.Columns[5].HeaderText = "Mikor";
                Tábla.Columns[5].Width = 200;

                ListaTartalma();
                Tábla.Sort(Tábla.Columns[3], System.ComponentModel.ListSortDirection.Ascending);
                Tábla.Refresh();
                Tábla.ClearSelection();
                Tábla.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ListaTartalma()
        {
            try
            {
                if (AdatokKerékMérés.Count != 0)
                {
                    int i;
                    List<Adat_MEO_KerékMérés> adat = (from a in AdatokKerékMérés
                                                      where a.Azonosító == Pályaszám.Text.Trim() && a.Törölt != true
                                                      select a).ToList();

                    foreach (Adat_MEO_KerékMérés rekord in adat)
                    {
                        Tábla.RowCount++;
                        i = Tábla.RowCount - 1;
                        Tábla.Rows[i].Cells[0].Value = rekord.Azonosító;
                        Tábla.Rows[i].Cells[1].Value = rekord.Típus;
                        Tábla.Rows[i].Cells[2].Value = rekord.Üzem;
                        Tábla.Rows[i].Cells[3].Value = rekord.Bekövetkezés.ToString("yyyy.MM.dd");
                        Tábla.Rows[i].Cells[4].Value = rekord.Ki;
                        Tábla.Rows[i].Cells[5].Value = rekord.Mikor;
                    }
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pályaszámfeltöltés()
        {
            try
            {
                Rögzítő.Text = Program.PostásNév.ToStrTrim();
                Felhasználólistáz();
                Pályaszám.Items.Clear();
                //ha olyan személy lép be akinek van kiosztva típus, akkor csak azt listázza
                if (FelhasználóTábla.Rows.Count > 0)
                {
                    for (int i = 0; i < FelhasználóTábla.Rows.Count; i++)
                    {
                        string FelhasználóTípusa = FelhasználóTábla.Rows[i].Cells[1].Value.ToStrTrim();
                        AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                        AdatokJármű = (from a in AdatokJármű
                                       where a.Valóstípus == FelhasználóTípusa && a.Törölt == false
                                       orderby a.Azonosító
                                       select a).ToList();
                        if (AdatokJármű != null)
                            foreach (Adat_Jármű rekord in AdatokJármű)
                                Pályaszám.Items.Add(rekord.Azonosító);
                    }
                }
                else
                {
                    //Különben minden kocsit listáz, de a módodítási gombok nem aktívak
                    Pályaszám.Items.Clear();
                    Pályaszám.BeginUpdate();
                    AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség").Where(a => a.Törölt == false).OrderBy(a => a.Azonosító).ToList();
                    foreach (Adat_Jármű rekord in AdatokJármű)
                        Pályaszám.Items.Add(rekord.Azonosító);
                    Pályaszám.EndUpdate();
                    Pályaszám.Refresh();
                    Töröl.Visible = false;
                    Rögzít.Visible = false;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Rögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pályaszám.Text.ToStrTrim() == "") throw new HibásBevittAdat("Nincs kitöltve a pályaszám!");
                if (Telephely.Text.ToStrTrim() == "") throw new HibásBevittAdat("Nincs kitöltve a telephely!");

                Listáz();
                // leellenőrizzük, hogy lehet-e rögzíteni.

                if (!Pályaszám.Items.Contains(Pályaszám.Text.Trim())) throw new HibásBevittAdat("A pályaszám nem tartozik a kiosztott villamosok körébe, így nem kerül rögzítésre!");

                AdatokKerékMérés = KézKerékMérés.Lista_Adatok(Dátum.Value.Year);
                AdatokKerékMérés = (from a in AdatokKerékMérés
                                    where a.Azonosító == Pályaszám.Text.ToStrTrim()
                                    && a.Üzem == Telephely.Text.ToStrTrim()
                                    && a.Bekövetkezés == Dátum.Value
                                    && a.Törölt == false
                                    select a).ToList();

                if (AdatokKerékMérés.Count == 0)
                {
                    Adat_MEO_KerékMérés ADAT = new Adat_MEO_KerékMérés(
                             Pályaszám.Text.ToStrTrim(),
                             Dátum.Value,
                             Telephely.Text.ToStrTrim(),
                             false,
                             DateTime.Now,
                             Program.PostásNév.ToStrTrim(),
                             Típus2.Text.ToStrTrim());
                    KézKerékMérés.Rögzítés(Dátum.Value.Year, ADAT);
                    AdatokKerékMérés_Feltöltés();
                    MessageBox.Show("Az adatok rögzítése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Az adatok már egyszer rögzítésre kerültek!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Listáz();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Telephelyfeltöltés()
        {
            try
            {
                AdatokKiegTelephely = KézKiegTelephely.Lista_Adatok();

                Telephely.Items.Clear();
                Telephely.Items.Add("");
                Telephely.BeginUpdate();
                foreach (Adat_kiegészítő_telephely rekord in AdatokKiegTelephely)
                    TelephelyList.Items.Add(rekord.Telephelykönyvtár.ToStrTrim());
                Telephely.EndUpdate();
                Telephely.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Pályaszám_LostFocus(object sender, EventArgs e)
        {
            Pályaszám_bezonosítás();
        }

        private void Pályaszám_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pályaszám_bezonosítás();
        }

        private void Pályaszám_bezonosítás()
        {
            try
            {
                if (Pályaszám.Text.ToStrTrim() == "") throw new HibásBevittAdat("Nincs kitöltve a pályaszám!");
                AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                AdatJármű = (from a in AdatokJármű
                             where a.Azonosító == Pályaszám.Text.ToStrTrim()
                             select a).FirstOrDefault();
                if (AdatJármű != null)
                {
                    Telephely.Text = AdatJármű.Üzem.Trim();
                    Típus2.Text = AdatJármű.Valóstípus.Trim();
                    Listáz();
                }
                else
                {
                    Pályaszám.Focus();
                    MessageBox.Show("Nincs ilyen pályaszámú villamos!", "Hiba", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Tábla.Rows.Count < 1) throw new HibásBevittAdat("Nincs kijelölve cella!");
            if (e.RowIndex < 0)
                return;

            Pályaszám.Text = Tábla.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
            Telephely.Text = Tábla.Rows[e.RowIndex].Cells[2].Value.ToStrTrim();
            Dátum.Value = DateTime.Parse(Tábla.Rows[e.RowIndex].Cells[3].Value.ToStrTrim());
            Típus2.Text = Tábla.Rows[e.RowIndex].Cells[1].Value.ToStrTrim();
        }

        private void Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Pályaszám.Text.ToStrTrim() == "") throw new HibásBevittAdat("Nincs kitöltve a pályaszám!");
                if (Telephely.Text.ToStrTrim() == "") throw new HibásBevittAdat("Nincs kitöltve a telephely.");

                List<Adat_MEO_KerékMérés> Adatok = KézKerékMérés.Lista_Adatok(Dátum.Value.Year);
                Adatok = (from a in Adatok
                          where a.Azonosító == Pályaszám.Text.ToStrTrim()
                          && a.Üzem == Telephely.Text.ToStrTrim()
                          && a.Bekövetkezés == Dátum.Value
                          && a.Törölt == false
                          select a).ToList();
                if (Adatok != null)
                {
                    Adat_MEO_KerékMérés Adat = new Adat_MEO_KerékMérés(
                        Pályaszám.Text.ToStrTrim(),
                        Dátum.Value,
                        Telephely.Text.ToStrTrim(),
                        true,
                        DateTime.Now,
                        Program.PostásNév.ToStrTrim(),
                        Típus2.Text.ToStrTrim());
                    KézKerékMérés.Módosítás(Dátum.Value.Year, Adat);
                    MessageBox.Show("Az adat törlése befejeződött!", "Figyelmeztetés", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AdatokKerékMérés_Feltöltés();
                }
                Listáz();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Telephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pályaszámfeltöltés();
        }
        #endregion


        #region Rögzítés listázása
        private void Telephelyfeltöltéslist()
        {
            try
            {
                AdatokKiegTelephely = KézKiegTelephely.Lista_Adatok();

                TelephelyList.Items.Clear();
                TelephelyList.BeginUpdate();
                foreach (Adat_kiegészítő_telephely rekord in AdatokKiegTelephely)
                    TelephelyList.Items.Add(rekord.Telephelykönyvtár.ToStrTrim());
                TelephelyList.EndUpdate();
                MindenElemKijelölve(TelephelyList);
                TelephelyList.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Típuslistafeltöltés()
        {
            try
            {
                AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");

                List<string> RészAdat = (from a in AdatokJármű
                                         select a.Valóstípus).Distinct().ToList();

                Típuslista.Items.Clear();
                Típuslista.BeginUpdate();
                foreach (string rekord in RészAdat)
                    Típuslista.Items.Add(rekord);
                Típuslista.EndUpdate();
                Típuslista.Refresh();
                MindenElemKijelölve(Típuslista);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Rögzítőfeltöltés()
        {
            try
            {
                AdatokKerékMérés = KézKerékMérés.Lista_Adatok(Dátumtól.Value.Year);
                List<string> adatok = (from a in AdatokKerékMérés
                                       select a.Ki).Distinct().ToList();

                Rögzítő1.Items.Clear();
                Rögzítő1.BeginUpdate();
                foreach (string rekord in adatok)
                    Rögzítő1.Items.Add(rekord);
                Rögzítő1.EndUpdate();
                Rögzítő1.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Excellekérdezés_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListaTábla.Rows.Count <= 0) throw new HibásBevittAdat("Nincs kijelölve adat a listában!");
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Kerékmérés{Program.PostásNév.ToStrTrim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.DataGridViewToXML(fájlexc, ListaTábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MyE.Megnyitás(fájlexc);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Mérés_Lista_Frissit_Click(object sender, EventArgs e)
        {
            Listalistáz();
        }

        private void Listalistáz()
        {
            try
            {
                if (!int.TryParse(Határnap.Text, out int HatárNap)) HatárNap = 0;
                AdatokKerékMérés_Feltöltés();
                List<Adat_MEO_KerékMérés> AdatokÖ = (from a in AdatokKerékMérés
                                                     where a.Mikor >= MyF.Nap0000(Dátumtól.Value)
                                                     && a.Mikor <= MyF.Nap2359(Dátumig.Value)
                                                     && a.Törölt == false
                                                     orderby a.Bekövetkezés
                                                     select a).ToList();
                // típus
                if (Típuslista.CheckedItems.Count != 0) TípusraSzűrés(ref AdatokÖ, Típuslista.CheckedItems);

                // telephely
                if (TelephelyList.CheckedItems.Count != 0) TelephelyreSzűrés(ref AdatokÖ, TelephelyList.CheckedItems);


                if (Rögzítő1.Text.ToStrTrim() != "")
                    AdatokÖ = AdatokÖ.Where(a => a.Ki == Rögzítő1.Text.ToStrTrim()).ToList();

                ListaTábla.Rows.Clear();
                ListaTábla.Columns.Clear();
                ListaTábla.Refresh();
                ListaTábla.Visible = false;
                ListaTábla.ColumnCount = 6;

                ListaTábla.Columns[0].HeaderText = "Pályaszám";
                ListaTábla.Columns[0].Width = 100;
                ListaTábla.Columns[1].HeaderText = "Típus";
                ListaTábla.Columns[1].Width = 100;
                ListaTábla.Columns[2].HeaderText = "Telephely";
                ListaTábla.Columns[2].Width = 100;
                ListaTábla.Columns[3].HeaderText = "Dátum";
                ListaTábla.Columns[3].Width = 200;
                ListaTábla.Columns[4].HeaderText = "Rögzítette";
                ListaTábla.Columns[4].Width = 200;
                ListaTábla.Columns[5].HeaderText = "Mikor";
                ListaTábla.Columns[5].Width = 200;

                foreach (Adat_MEO_KerékMérés rekord in AdatokÖ)
                {
                    ListaTábla.RowCount++; ;
                    int i = ListaTábla.RowCount - 1;
                    ListaTábla.Rows[i].Cells[0].Value = rekord.Azonosító;
                    ListaTábla.Rows[i].Cells[1].Value = rekord.Típus;
                    ListaTábla.Rows[i].Cells[2].Value = rekord.Üzem;
                    ListaTábla.Rows[i].Cells[3].Value = rekord.Bekövetkezés.ToString("yyyy.MM.dd");
                    ListaTábla.Rows[i].Cells[4].Value = rekord.Ki;
                    ListaTábla.Rows[i].Cells[5].Value = rekord.Mikor;
                }

                ListaTábla.Visible = true;
                ListaTábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TelephelyreSzűrés(ref List<Adat_MEO_KerékMérés> Adatok, CheckedItemCollection Lista)
        {
            List<Adat_MEO_KerékMérés> Adatokbel = new List<Adat_MEO_KerékMérés>();
            foreach (string adat2 in Lista)
            {
                List<Adat_MEO_KerékMérés> Ideig = (from a in Adatok
                                                   where a.Üzem == adat2.ToStrTrim()
                                                   select a).ToList();
                Adatokbel.AddRange(Ideig);
            }
            Adatok = Adatokbel;
        }

        private void TípusraSzűrés(ref List<Adat_MEO_KerékMérés> Adatok, CheckedItemCollection Lista)
        {
            List<Adat_MEO_KerékMérés> Adatokbel = new List<Adat_MEO_KerékMérés>();
            foreach (string adat in Lista)
            {
                List<Adat_MEO_KerékMérés> Ideig = (from a in Adatok
                                                   where a.Típus == adat.ToStrTrim()
                                                   select a).ToList();
                Adatokbel.AddRange(Ideig);
            }
            Adatok = Adatokbel;
        }
        #endregion


        #region Mérések listázása
        private void Btn_Mérés_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (LekérdTábla.Rows.Count <= 0) throw new HibásBevittAdat("Nincs kijelölve sor!");
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Kerékmérés{Program.PostásNév.ToStrTrim()}-{DateTime.Now:yyyyMMddHHmmss}",
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyX.DataGridViewToXML(fájlexc, LekérdTábla);
                MessageBox.Show("Elkészült az Excel tábla: " + fájlexc, "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

                MyE.Megnyitás(fájlexc);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Mérés_Frissit_Click(object sender, EventArgs e)
        {
            Listalistáz1();
        }

        private void Típuslista1feltöltés()
        {
            try
            {
                AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                List<string> RészAdat = (from a in AdatokJármű
                                         select a.Valóstípus).Distinct().ToList();
                Típuslista1.Items.Clear();
                Típuslista1.BeginUpdate();
                foreach (string rekord in RészAdat)
                    Típuslista1.Items.Add(rekord);
                Típuslista1.EndUpdate();
                MindenElemKijelölve(Típuslista1);
                Típuslista1.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Telephelyfeltöltéslist1()
        {
            try
            {
                AdatokKiegTelephely = KézKiegTelephely.Lista_Adatok();

                TelephelyList1.Items.Clear();
                TelephelyList1.BeginUpdate();
                foreach (Adat_kiegészítő_telephely rekord in AdatokKiegTelephely)
                    TelephelyList1.Items.Add(rekord.Telephelykönyvtár.ToStrTrim());
                TelephelyList1.EndUpdate();
                MindenElemKijelölve(TelephelyList1);
                TelephelyList1.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Listalistáz1()
        {
            try
            {
                if (!int.TryParse(Határnap.Text, out int HatárNap)) HatárNap = 0;

                Holtart.Be();

                LekérdTábla.Rows.Clear();
                LekérdTábla.Columns.Clear();
                LekérdTábla.Refresh();
                LekérdTábla.Visible = false;
                LekérdTábla.ColumnCount = 7;

                // fejléc elkészítése
                LekérdTábla.Columns[0].HeaderText = "Pályaszám";
                LekérdTábla.Columns[0].Width = 100;
                LekérdTábla.Columns[1].HeaderText = "Típus";
                LekérdTábla.Columns[1].Width = 100;
                LekérdTábla.Columns[2].HeaderText = "Telephely";
                LekérdTábla.Columns[2].Width = 200;
                LekérdTábla.Columns[3].HeaderText = "Utolsó mérés dátuma";
                LekérdTábla.Columns[3].Width = 100;
                LekérdTábla.Columns[4].HeaderText = "Üzemképesség";
                LekérdTábla.Columns[4].Width = 150;
                LekérdTábla.Columns[5].HeaderText = "Mióta áll";
                LekérdTábla.Columns[5].Width = 100;
                LekérdTábla.Columns[6].HeaderText = "Hiba";
                LekérdTábla.Columns[6].Width = 500;

                AdatokJármű = KézJármű.Lista_Adatok("Főmérnökség");
                AdatokJármű = (from a in AdatokJármű
                               where a.Törölt == false
                               orderby a.Azonosító
                               select a).ToList();
                // kiírjuk a típushoz tartozó kocsikat telephelyenként
                // típus
                if (Típuslista1.CheckedItems.Count != 0) TípusraSzűrés(ref AdatokJármű, Típuslista1.CheckedItems);

                // telephely
                if (TelephelyList1.CheckedItems.Count != 0) TelephelyreSzűrés(ref AdatokJármű, TelephelyList1.CheckedItems);
                AdatokJármű = AdatokJármű.OrderBy(a => a.Azonosító).ToList();

                AdatokKerékMérés_Feltöltés();
                int i;
                Hiba_Lista();
                foreach (Adat_Jármű rekord in AdatokJármű)
                {
                    LekérdTábla.RowCount++;
                    i = LekérdTábla.RowCount - 1;
                    LekérdTábla.Rows[i].Cells[0].Value = rekord.Azonosító;
                    LekérdTábla.Rows[i].Cells[1].Value = rekord.Valóstípus;
                    LekérdTábla.Rows[i].Cells[2].Value = rekord.Üzem;

                    Lista_Mérés1(rekord, LekérdTábla, i, AdatokKerékMérés, HatárNap);
                    Lista_Mérés2(rekord, LekérdTábla, i);
                    Holtart.Lép();
                }

                LekérdTábla.Refresh();
                LekérdTábla.Visible = true;

                Holtart.Ki();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TelephelyreSzűrés(ref List<Adat_Jármű> Adatokjármű, CheckedItemCollection Lista)
        {
            List<Adat_Jármű> Adatok = new List<Adat_Jármű>();
            foreach (string adat2 in Lista)
            {
                List<Adat_Jármű> Ideig = (from a in Adatokjármű
                                          where a.Üzem == adat2.ToStrTrim()
                                          select a).ToList();
                Adatok.AddRange(Ideig);
            }
            Adatokjármű = Adatok;
        }

        private void TípusraSzűrés(ref List<Adat_Jármű> Adatokjármű, CheckedItemCollection Lista)
        {
            List<Adat_Jármű> Adatok = new List<Adat_Jármű>();
            foreach (string adat in Lista)
            {
                List<Adat_Jármű> Ideig = (from a in Adatokjármű
                                          where a.Valóstípus == adat.ToStrTrim()
                                          select a).ToList();
                Adatok.AddRange(Ideig);
            }
            Adatokjármű = Adatok;
        }

        #endregion


        #region Konverter
        Ablak_Kerék_Konverter Új_Ablak_Kerék_Konverter;
        private void Konvertálás_Click(object sender, EventArgs e)
        {
            Új_Ablak_Kerék_Konverter?.Close();

            Új_Ablak_Kerék_Konverter = new Ablak_Kerék_Konverter();
            Új_Ablak_Kerék_Konverter.FormClosed += Ablak_Kerék_Konverter_Closed;

            Új_Ablak_Kerék_Konverter.StartPosition = FormStartPosition.CenterScreen;
            Új_Ablak_Kerék_Konverter.Show();
        }

        private void Ablak_Kerék_Konverter_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_Kerék_Konverter = null;
        }
        #endregion


        #region Listák_Feltöltése
        private void AdatokKerékMérés_Feltöltés()
        {
            try
            {
                AdatokKerékMérés.Clear();
                DateTime előzőév = DateTime.Today.AddYears(-1);
                AdatokKerékMérés = KézKerékMérés.Lista_Adatok(előzőév.Year, true);
                List<Adat_MEO_KerékMérés> AdatokKerékMérésIDEIG = KézKerékMérés.Lista_Adatok(DateTime.Today.Year, true);
                AdatokKerékMérés.AddRange(AdatokKerékMérésIDEIG);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Lista_Mérés1(Adat_Jármű rekord, DataGridView LekérdTábla, int i, List<Adat_MEO_KerékMérés> AdatokKerékMérés, int HatárNap)
        {
            try
            {
                Adat_MEO_KerékMérés Méresegy = (from a in AdatokKerékMérés
                                                where a.Azonosító == rekord.Azonosító
                                                orderby a.Bekövetkezés descending
                                                select a).FirstOrDefault();
                if (Méresegy != null)
                {
                    LekérdTábla.Rows[i].Cells[3].Value = Méresegy.Bekövetkezés.ToString("yyyy.MM.dd");
                    if (Méresegy.Bekövetkezés.AddDays(HatárNap) < DateTime.Today)
                        LekérdTábla.Rows[i].Cells[3].Style.BackColor = Color.Red;
                    else
                        LekérdTábla.Rows[i].Cells[3].Style.BackColor = Color.White;
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Lista_Mérés2(Adat_Jármű rekord, DataGridView LekérdTábla, int i)
        {
            try
            {
                Adat_Jármű Mérésketto = (from a in AdatokJármű
                                         where a.Azonosító == rekord.Azonosító
                                         select a).FirstOrDefault();
                if (Mérésketto != null)
                {
                    switch (Mérésketto.Státus)
                    {
                        case 3:
                            LekérdTábla.Rows[i].Cells[4].Value = "Beállóba adott";
                            LekérdTábla.Rows[i].Cells[4].Style.BackColor = Color.Yellow;
                            break;

                        case 4:
                            LekérdTábla.Rows[i].Cells[4].Value = "Üzemképtelen";
                            LekérdTábla.Rows[i].Cells[4].Style.BackColor = Color.Red;
                            LekérdTábla.Rows[i].Cells[5].Value = Mérésketto.Miótaáll.ToString("yyyy.MM.dd");
                            break;
                    }

                    // Hibaadatok lekérése és beállítása

                    List<Adat_Jármű_hiba> Méréshárom = (from a in AdatokHIBA
                                                        where a.Azonosító == rekord.Azonosító
                                                        select a).ToList();

                    if (Méréshárom != null)
                    {
                        LekérdTábla.Rows[i].Cells[6].Value = "";
                        foreach (Adat_Jármű_hiba Elem in Méréshárom)
                            LekérdTábla.Rows[i].Cells[6].Value += $"{Elem.Hibaleírása} ";
                    }
                }
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToStrTrim(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Hiba_Lista()
        {
            try
            {
                foreach (Adat_kiegészítő_telephely rekord in AdatokKiegTelephely)
                {
                    List<Adat_Jármű_hiba> Adatok = KézHiba.Lista_Adatok(rekord.Telephelynév);
                    AdatokHIBA.AddRange(Adatok);
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


        #region Checkbox kijelölés/törlések
        private void BtnKijelölTípus_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Típuslista.Items.Count; i++)
                Típuslista.SetItemChecked(i, true);
        }

        private void BtnkijelölTípustörlés_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Típuslista.Items.Count; i++)
                Típuslista.SetItemChecked(i, false);
        }

        private void BtnKijelölTelephely_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < TelephelyList.Items.Count; i++)
                TelephelyList.SetItemChecked(i, true);
        }

        private void BtnkijelölTelephelytörlés_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < TelephelyList.Items.Count; i++)
                TelephelyList.SetItemChecked(i, false);
        }

        private void BtnKijelölTípus1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Típuslista1.Items.Count; i++)
                Típuslista1.SetItemChecked(i, true);
        }

        private void BtnKijelölTípus1Törlés_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Típuslista1.Items.Count; i++)
                Típuslista1.SetItemChecked(i, false);
        }

        private void BtnKijelölTelephely1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < TelephelyList1.Items.Count; i++)
                TelephelyList1.SetItemChecked(i, true);
        }

        private void BtnKijelölTelephely1Törlés_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < TelephelyList1.Items.Count; i++)
                TelephelyList1.SetItemChecked(i, false);
        }

        private void MindenElemKijelölve(CheckedListBox CheckListBox)
        {
            for (int i = 0; i < CheckListBox.Items.Count; i++)
                CheckListBox.SetItemChecked(i, true);
        }
        #endregion


        #region CheckBox Lenyilasa
        private void TelephelyList_MouseLeave(object sender, EventArgs e)
        {
            TelephelyList.Height = 25;
        }

        private void TelephelyList_MouseHover(object sender, EventArgs e)
        {
            TelephelyList.Height = 300;
        }

        private void Típuslista_MouseLeave(object sender, EventArgs e)
        {
            Típuslista.Height = 25;
        }

        private void Típuslista_MouseHover(object sender, EventArgs e)
        {
            Típuslista.Height = 300;
        }

        private void Típuslista1_MouseLeave(object sender, EventArgs e)
        {
            Típuslista1.Height = 25;
        }

        private void Típuslista1_MouseHover(object sender, EventArgs e)
        {
            Típuslista1.Height = 300;
        }

        private void TelephelyList1_MouseLeave(object sender, EventArgs e)
        {
            TelephelyList1.Height = 25;
        }

        private void TelephelyList1_MouseHover(object sender, EventArgs e)
        {
            TelephelyList1.Height = 300;
        }
        #endregion


    }
}