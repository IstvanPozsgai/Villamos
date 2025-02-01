using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;

using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_Munkalap_admin
    {
        readonly Kezelő_Munka_Folyamat KézMunkaFoly = new Kezelő_Munka_Folyamat();
        readonly Kezelő_MunkaRend KézMunkaRend = new Kezelő_MunkaRend();
        readonly Kezelő_Munka_Szolgálat KézSzolgálat = new Kezelő_Munka_Szolgálat();

        #region Alap
        public Ablak_Munkalap_admin()
        {
            InitializeComponent();
        }

        private void Ablak_Munkalap_admin_Load(object sender, EventArgs e)
        {
            try
            {
                Telephelyekfeltöltése();
                Dátum.Value = DateTime.Today;
                Jogosultságkiosztás();
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
                Cmbtelephely.Items.AddRange(Listák.TelephelyLista_Jármű());
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

        private void Fülekkitöltése()
        {
            try
            {
                switch (Fülek.SelectedIndex)
                {
                    case 0:
                        {
                            Folyamatlistáz();
                            break;
                        }
                    case 1:
                        {
                            Rendlistáz();
                            break;
                        }
                    case 2:
                        {
                            Szolgálatadatok_listázása();
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

        private void Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Munkalap_admin.html";
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
                // ide kell az összes gombot tenni amit szabályozni akarunk
                RendelésRögzít.Enabled = false;
                ÚjRögzítés.Enabled = false;
                MunkafolyamatTörlés.Enabled = false;
                Visszavon.Enabled = false;
                Cseregomb.Enabled = false;
                Karbantartás.Enabled = false;
                FejlécRögzít.Enabled = false;
                Button1.Enabled = false;
                Button2.Enabled = false;
                Button3.Enabled = false;
                Button4.Enabled = false;

                melyikelem = 80;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    RendelésRögzít.Enabled = true;
                    ÚjRögzítés.Enabled = true;
                    MunkafolyamatTörlés.Enabled = true;
                    Visszavon.Enabled = true;

                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Cseregomb.Enabled = true;
                }
                // módosítás 3
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Karbantartás.Enabled = true;
                }

                melyikelem = 81;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Button1.Enabled = true;
                    Button2.Enabled = true;
                    Button3.Enabled = true;
                    Button4.Enabled = true;

                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                }
                // módosítás 3
                if (MyF.Vanjoga(melyikelem, 3))
                {
                }

                melyikelem = 82;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    FejlécRögzít.Enabled = true;

                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
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

        private void Cmbtelephely_SelectedIndexChanged(object sender, EventArgs e)
        {
            Fülekkitöltése();
            Rendlistáz();
            Szolgálatadatok_listázása();
        }

        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            KézMunkaFoly.AdatbázisLétrehozás(Cmbtelephely.Text, Dátum.Value.Year);
            Fülekkitöltése();
            Rendlistáz();
            Szolgálatadatok_listázása();
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


        #region Folyamatok
        private void Folyamatlistáz()
        {
            try
            {
                List<Adat_Munka_Folyamat> Adatok = KézMunkaFoly.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);

                MunkafolyamatTábla.Rows.Clear();
                MunkafolyamatTábla.Columns.Clear();
                MunkafolyamatTábla.Refresh();
                MunkafolyamatTábla.Visible = false;
                MunkafolyamatTábla.ColumnCount = 5;
                MunkafolyamatTábla.RowCount = 0;

                // fejléc elkészítése
                MunkafolyamatTábla.Columns[0].HeaderText = "Sorszám";
                MunkafolyamatTábla.Columns[0].Width = 80;
                MunkafolyamatTábla.Columns[1].HeaderText = "Rendelésiszám";
                MunkafolyamatTábla.Columns[1].Width = 120;
                MunkafolyamatTábla.Columns[2].HeaderText = "Pályaszám";
                MunkafolyamatTábla.Columns[2].Width = 120;
                MunkafolyamatTábla.Columns[3].HeaderText = "Munkafolyamat";
                MunkafolyamatTábla.Columns[3].Width = 300;
                MunkafolyamatTábla.Columns[4].HeaderText = "Érvényes";
                MunkafolyamatTábla.Columns[4].Width = 100;

                foreach (Adat_Munka_Folyamat rekord in Adatok)
                {
                    MunkafolyamatTábla.RowCount++;
                    int i = MunkafolyamatTábla.RowCount - 1;
                    MunkafolyamatTábla.Rows[i].Cells[0].Value = rekord.ID;
                    MunkafolyamatTábla.Rows[i].Cells[1].Value = rekord.Rendelésiszám.Trim();
                    MunkafolyamatTábla.Rows[i].Cells[2].Value = rekord.Azonosító.Trim();
                    MunkafolyamatTábla.Rows[i].Cells[3].Value = rekord.Munkafolyamat.Trim();
                    if (rekord.Látszódik)
                    {
                        MunkafolyamatTábla.Rows[i].Cells[4].Value = "Érvényes";
                    }
                    else
                    {
                        MunkafolyamatTábla.Rows[i].Cells[4].Value = "Törölt";
                    }
                }

                MunkafolyamatTábla.Visible = true;
                MunkafolyamatTábla.Refresh();
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

        private void RendelésRögzít_Click(object sender, EventArgs e)
        {
            try
            {
                if (RendelésiszámText.Text.Trim() == "") throw new HibásBevittAdat("A rendelési számot ki kell tölteni.");
                if (MunkafolyamatText.Text.Trim() == "") throw new HibásBevittAdat("A munkafolyamat részt ki kell tölteni.");
                if (PályaszámText.Text.Trim() == "") PályaszámText.Text = "_";
                if (!long.TryParse(IDfolyamat.Text, out long sorszám)) sorszám = 0;

                List<Adat_Munka_Folyamat> Adatok = KézMunkaFoly.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);

                Adat_Munka_Folyamat Elem = (from a in Adatok
                                            where a.ID == sorszám
                                            select a).FirstOrDefault();

                if (Elem == null)
                {
                    //Rögzítés
                    Adat_Munka_Folyamat ADAT = new Adat_Munka_Folyamat(0,
                                                                       RendelésiszámText.Text.Trim(),
                                                                       PályaszámText.Text,
                                                                       MunkafolyamatText.Text.Trim(),
                                                                       true);
                    KézMunkaFoly.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value.Year, ADAT);
                }
                else
                {
                    // ha már volt adat akkor módosítjuk
                    Adat_Munka_Folyamat ADAT = new Adat_Munka_Folyamat(sorszám,
                                                                       RendelésiszámText.Text.Trim(),
                                                                       PályaszámText.Text,
                                                                       MunkafolyamatText.Text.Trim(),
                                                                       true);
                    KézMunkaFoly.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value.Year, ADAT);
                }
                Folyamatlistáz();

                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void MunkafolyamatTábla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MunkafolyamatTábla.Rows[e.RowIndex].Selected = true;
        }

        private void MunkafolyamatTábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int sor = e.RowIndex;
                if (sor < 0) return;

                IDfolyamat.Text = MunkafolyamatTábla.Rows[sor].Cells[0].Value.ToString().Trim();
                RendelésiszámText.Text = MunkafolyamatTábla.Rows[sor].Cells[1].Value.ToString().Trim();
                PályaszámText.Text = MunkafolyamatTábla.Rows[sor].Cells[2].Value.ToString().Trim();
                MunkafolyamatText.Text = MunkafolyamatTábla.Rows[sor].Cells[3].Value.ToString().Trim();
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

        private void MunkafolyamatTábla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (MunkafolyamatTábla.Rows[e.RowIndex].Cells[4].Value.ToString().Trim() == "Törölt")
            {
                MunkafolyamatTábla.Rows[e.RowIndex].Cells[4].Style.ForeColor = Color.White;
                MunkafolyamatTábla.Rows[e.RowIndex].Cells[4].Style.BackColor = Color.IndianRed;
                MunkafolyamatTábla.Rows[e.RowIndex].Cells[4].Style.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
            }
        }

        private void Karbantartás_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("A törölt adatsorokat véglegesen töröljük?", "Kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    KézMunkaFoly.Törlés(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                else
                    return;
                Adatok_tisztítása();
                Folyamatlistáz();
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

        private void Adatok_tisztítása()
        {
            //Újra sorszámozza a folyamatokat
            try
            {
                KézMunkaFoly.ÚjraSorszámoz(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
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

        private void Cseregomb_Click(object sender, EventArgs e)
        {
            try
            {
                if (RendelésiszámText.Text.Trim() == "") throw new HibásBevittAdat("A rendelési számot ki kell tölteni.");
                if (RendelésiSzámúj.Text.Trim() == "") throw new HibásBevittAdat("A rendelési számot ki kell tölteni.");

                KézMunkaFoly.MódosításRendelés(Cmbtelephely.Text.Trim(), Dátum.Value.Year, RendelésiszámText.Text.Trim(), RendelésiSzámúj.Text.Trim());
                Folyamatlistáz();
                Bevitelimezőtisztítás();
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

        private void Bevitelimezőtisztítás()
        {
            try
            {
                IDfolyamat.Text = "";
                RendelésiszámText.Text = "";
                PályaszámText.Text = "";
                MunkafolyamatText.Text = "";
                RendelésiSzámúj.Text = "";
                PályaszámTextÚj.Text = "";
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

        private void ÚjRögzítés_Click(object sender, EventArgs e)
        {
            Bevitelimezőtisztítás();
        }

        private void MunkafolyamatTörlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (!long.TryParse(IDfolyamat.Text, out long sorszám)) throw new HibásBevittAdat("A sorszámot meg kell adni.");
                KézMunkaFoly.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value.Year, sorszám, false);
                Folyamatlistáz();
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

        private void Visszavon_Click(object sender, EventArgs e)
        {
            try
            {
                if (!long.TryParse(IDfolyamat.Text, out long sorszám)) throw new HibásBevittAdat("A sorszámot meg kell adni.");
                KézMunkaFoly.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value.Year, sorszám, true);
                Folyamatlistáz();
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

        private void CseregombPsz_Click(object sender, EventArgs e)
        {
            try
            {
                if (PályaszámText.Text.Trim() == "") throw new HibásBevittAdat("A pályaszámot meg kell adni.");
                if (PályaszámTextÚj.Text.Trim() == "") throw new HibásBevittAdat("A pályaszámot ki kell tölteni.");
                KézMunkaFoly.MódosításPálya(Cmbtelephely.Text.Trim(), Dátum.Value.Year, PályaszámText.Text.Trim(), PályaszámTextÚj.Text.Trim());
                Folyamatlistáz();
                Bevitelimezőtisztítás();
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

        private void CsoportFel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MunkafolyamatTábla.SelectedRows.Count == 0) return;
                // az elsőt nem lehet feljebb vinni
                if (MunkafolyamatTábla.SelectedRows[0].Index <= 0) return;
                long SorszámElső = long.Parse(MunkafolyamatTábla.Rows[MunkafolyamatTábla.SelectedRows[0].Index].Cells[0].Value.ToString());
                long SorszámMásodik = long.Parse(MunkafolyamatTábla.Rows[MunkafolyamatTábla.SelectedRows[0].Index - 1].Cells[0].Value.ToString());
                KézMunkaFoly.Csere(Cmbtelephely.Text.Trim(), Dátum.Value.Year, SorszámElső, SorszámMásodik);
                Folyamatlistáz();
                MessageBox.Show("Az adatrögzítése megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        #region Munkarend 
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MunkarendText.Text.Trim() == "") throw new HibásBevittAdat("A munkarendet meg kell adni.");
                if (!long.TryParse(IDrend.Text, out long Sorszám)) Sorszám = 0;
                if (IDrend.Text.Trim() == "")
                {
                    Adat_MunkaRend ADAT = new Adat_MunkaRend(0, MunkarendText.Text.Trim(), true);
                    KézMunkaRend.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value.Year, ADAT);
                }
                else
                {
                    // ha már volt adat akkor módosítjuk
                    Adat_MunkaRend ADAT = new Adat_MunkaRend(Sorszám, MunkarendText.Text.Trim(), true);
                    KézMunkaRend.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value.Year, ADAT);
                }
                Rendlistáz();
                ÜrítMunkaRend();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Rendlistáz()
        {
            try
            {
                List<Adat_MunkaRend> Adatok = KézMunkaRend.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);

                MunkarendTábla.Rows.Clear();
                MunkarendTábla.Columns.Clear();
                MunkarendTábla.Refresh();
                MunkarendTábla.Visible = false;
                MunkarendTábla.ColumnCount = 3;

                // fejléc elkészítése
                MunkarendTábla.Columns[0].HeaderText = "Sorszám";
                MunkarendTábla.Columns[0].Width = 150;
                MunkarendTábla.Columns[1].HeaderText = "Munkarend";
                MunkarendTábla.Columns[1].Width = 400;
                MunkarendTábla.Columns[2].HeaderText = "Státus";
                MunkarendTábla.Columns[2].Width = 200;

                foreach (Adat_MunkaRend rekord in Adatok)
                {
                    MunkarendTábla.RowCount++;
                    int i = MunkarendTábla.RowCount - 1;

                    MunkarendTábla.Rows[i].Cells[0].Value = rekord.ID;
                    MunkarendTábla.Rows[i].Cells[1].Value = rekord.Munkarend.Trim();
                    MunkarendTábla.Rows[i].Cells[2].Value = rekord.Látszódik ? "Érvényes" : "Törölt";
                }

                MunkarendTábla.Visible = true;
                MunkarendTábla.Refresh();
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

        private void Button2_Click(object sender, EventArgs e)
        {
            IDrend.Text = "";
            MunkarendText.Text = "";
        }

        private void MunkarendTábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                IDrend.Text = MunkarendTábla.Rows[e.RowIndex].Cells[0].Value.ToString();
                MunkarendText.Text = MunkarendTábla.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
        }

        private void MunkarendTábla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            MunkarendTábla.Rows[e.RowIndex].Selected = true;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!long.TryParse(IDrend.Text.Trim(), out long Sorszám)) return;
                if (IDrend.Text.Trim() == "") return;

                KézMunkaRend.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value.Year, Sorszám, false);
                Rendlistáz();
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
                if (!long.TryParse(IDrend.Text.Trim(), out long Sorszám)) return;
                if (IDrend.Text.Trim() == "") return;

                KézMunkaRend.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value.Year, Sorszám, true);
                Rendlistáz();
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

        private void MunkarendTábla_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // egész sor színezése ha törölt

            foreach (DataGridViewRow row in MunkarendTábla.Rows)
            {
                if (row.Cells[2].Value.ToString().Trim() == "Törölt")
                {
                    row.DefaultCellStyle.ForeColor = Color.White;
                    row.DefaultCellStyle.BackColor = Color.IndianRed;
                    row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Strikeout);
                }
            }
        }

        private void ÜrítMunkaRend()
        {
            IDrend.Clear();
            MunkarendText.Clear();
        }
        #endregion


        #region Fejléc
        private void FejlécRögzít_Click(object sender, EventArgs e)
        {
            try
            {
                List<Adat_Munka_Szolgálat> Adatok = KézSzolgálat.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                Adat_Munka_Szolgálat ADAT = new Adat_Munka_Szolgálat(Költséghely.Text.Trim(),
                                                                     Szolgálat.Text.Trim(),
                                                                     Üzem.Text.Trim(),
                                                                     "0", "0", "0", "0", "0", "0", "0");
                bool vane = Adatok.Any();
                if (!vane)
                    KézSzolgálat.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value.Year, ADAT);
                else
                    KézSzolgálat.Módosítás(Cmbtelephely.Text.Trim(), Dátum.Value.Year, ADAT);

                Szolgálatadatok_listázása();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void Szolgálatadatok_listázása()
        {
            try
            {
                List<Adat_Munka_Szolgálat> Adatok = KézSzolgálat.Lista_Adatok(Cmbtelephely.Text.Trim(), Dátum.Value.Year);
                Adat_Munka_Szolgálat Adat = (from a in Adatok
                                             orderby a.Üzem
                                             select a).FirstOrDefault();
                if (Adat != null)
                {
                    Költséghely.Text = Adat.Költséghely.Trim();
                    Szolgálat.Text = Adat.Szolgálat.Trim();
                    Üzem.Text = Adat.Üzem.Trim();
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
    }
}