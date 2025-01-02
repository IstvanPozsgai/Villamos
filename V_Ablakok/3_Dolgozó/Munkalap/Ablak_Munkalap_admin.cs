using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyA = Adatbázis;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_Munkalap_admin
    {
        readonly Kezelő_Munka_Folyamat KézMunkaFoly = new Kezelő_Munka_Folyamat();
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

                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Munkalap";
                if (!Directory.Exists(hely)) System.IO.Directory.CreateDirectory(hely);

                // ha nincs olyan évi adatbázis, akkor létrehozzuk az előző évi alapján ha van.
                hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Munkalap\munkalap" + Dátum.Value.ToString("yyyy") + ".mdb";

                if (!File.Exists(hely)) KézMunkaFoly.AdatbázisLétrehozás(Cmbtelephely.Text, Dátum.Value);

                Fülek.SelectedIndex = 0;

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


        #region Alap
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


        private void Button13_Click(object sender, EventArgs e)
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
            KézMunkaFoly.AdatbázisLétrehozás(Cmbtelephely.Text, Dátum.Value);
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
                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Munkalap\munkalap" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "kismalac";
                if (!File.Exists(hely))
                    return;
                string szöveg = "SELECT * FROM folyamattábla ORDER BY id";

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

                Kezelő_Munka_Folyamat kéz = new Kezelő_Munka_Folyamat();
                List<Adat_Munka_Folyamat> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);
                int i;

                foreach (Adat_Munka_Folyamat rekord in Adatok)
                {
                    MunkafolyamatTábla.RowCount++;
                    i = MunkafolyamatTábla.RowCount - 1;
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
                // megnézzük, hogy melyik az utolós sorszám
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text}\Adatok\Munkalap\munkalap{Dátum.Value:yyyy}.mdb";
                string jelszó = "kismalac";

                //Új
                string szöveg = "SELECT * FROM folyamattábla";
                Kezelő_Munka_Folyamat Kéz = new Kezelő_Munka_Folyamat();
                List<Adat_Munka_Folyamat> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);

                if (IDfolyamat.Text.Trim() == "")
                {
                    double i = Adatok.Any() ? (Adatok.Max(a => a.ID) + 1) : 1;
                    IDfolyamat.Text = i.ToString();
                    szöveg = "INSERT INTO folyamattábla (id, Rendelésiszám, azonosító, munkafolyamat, látszódik)  VALUES (";
                    szöveg += IDfolyamat.Text + ", ";
                    szöveg += "'" + RendelésiszámText.Text.Trim() + "', ";
                    szöveg += "'" + PályaszámText.Text + "', ";
                    szöveg += "'" + MunkafolyamatText.Text.Trim() + "', ";
                    szöveg += " true ) ";
                }
                else
                {
                    // ha már volt adat akkor módosítjuk
                    szöveg = " UPDATE  folyamattábla SET ";
                    szöveg += " Rendelésiszám='" + RendelésiszámText.Text.Trim() + "', ";
                    szöveg += " azonosító='" + PályaszámText.Text + "', ";
                    szöveg += " munkafolyamat='" + MunkafolyamatText.Text.Trim() + "' ";
                    szöveg += " WHERE id=" + IDfolyamat.Text;
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);

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
                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Munkalap\munkalap" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "kismalac";
                if (!File.Exists(hely))
                    return;
                if (MessageBox.Show("A törölt adatsorokat véglegesen töröljük?", "Kérdés", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string szöveg = "DELETE FROM  folyamattábla WHERE látszódik=false";
                    MyA.ABtörlés(hely, jelszó, szöveg);
                }
                else
                {
                    return;
                }
                Folyamatlistáz();
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
            try
            {
                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Munkalap\munkalap" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "kismalac";
                int j = 1;

                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < MunkafolyamatTábla.Rows.Count; i++)
                {
                    if (int.Parse(MunkafolyamatTábla.Rows[i].Cells[0].Value.ToString()) != j)
                    {
                        string szöveg = " UPDATE folyamattábla SET id=" + j.ToString() + " WHERE munkafolyamat='" + MunkafolyamatTábla.Rows[i].Cells[3].Value.ToString().Trim() + "'";
                        SzövegGy.Add(szöveg);
                    }
                    j += 1;
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);
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
                if (RendelésiszámText.Text.Trim() == "")
                    throw new HibásBevittAdat("A rendelési számot ki kell tölteni.");
                if (RendelésiSzámúj.Text.Trim() == "")
                    throw new HibásBevittAdat("A rendelési számot ki kell tölteni.");

                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Munkalap\munkalap" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "kismalac";
                int j = 1;

                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < MunkafolyamatTábla.Rows.Count; i++)
                {
                    if (MunkafolyamatTábla.Rows[i].Cells[1].Value.ToString().Trim() == RendelésiszámText.Text.Trim())
                    {
                        string szöveg = " UPDATE folyamattábla SET Rendelésiszám='" + RendelésiSzámúj.Text.Trim() + "' WHERE id=" + MunkafolyamatTábla.Rows[i].Cells[0].Value.ToString().Trim();
                        SzövegGy.Add(szöveg);
                    }
                    j += 1;
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

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
                if (IDfolyamat.Text.Trim() == "")
                    throw new HibásBevittAdat("A sorszámot meg kell adni.");
                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Munkalap\munkalap" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "kismalac";
                string szöveg = " UPDATE folyamattábla SET látszódik=false WHERE id=" + IDfolyamat.Text.Trim();
                MyA.ABMódosítás(hely, jelszó, szöveg);
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
                if (IDfolyamat.Text.Trim() == "")
                    throw new HibásBevittAdat("A sorszámot meg kell adni.");
                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Munkalap\munkalap" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "kismalac";
                string szöveg = " UPDATE folyamattábla SET látszódik=true WHERE id=" + IDfolyamat.Text.Trim();
                MyA.ABMódosítás(hely, jelszó, szöveg);
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
                if (PályaszámText.Text.Trim() == "")
                    throw new HibásBevittAdat("A pályaszámot meg kell adni.");
                if (PályaszámTextÚj.Text.Trim() == "")
                    throw new HibásBevittAdat("A pályaszámot ki kell tölteni.");
                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Munkalap\munkalap" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "kismalac";
                int j = 1;

                List<string> SzövegGy = new List<string>();
                for (int i = 0; i < MunkafolyamatTábla.Rows.Count; i++)
                {
                    if (MunkafolyamatTábla.Rows[i].Cells[2].Value.ToString().Trim() == PályaszámText.Text.Trim())
                    {
                        string szöveg = " UPDATE folyamattábla SET azonosító='" + PályaszámTextÚj.Text.Trim() + "' WHERE id=" + MunkafolyamatTábla.Rows[i].Cells[0].Value.ToString().Trim();
                        SzövegGy.Add(szöveg);
                    }
                    j += 1;
                }
                MyA.ABMódosítás(hely, jelszó, SzövegGy);

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
                if (MunkafolyamatTábla.SelectedRows.Count == 0)
                    return;

                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Munkalap\munkalap" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "kismalac";
                // az elsőt nem lehet feljebb vinni
                int sor = MunkafolyamatTábla.SelectedRows[0].Index;
                if (sor <= 0) return;

                // a kiválasztott sor elé mentjük
                string szöveg = " UPDATE folyamattábla SET ";
                szöveg += " id='" + (double.Parse(MunkafolyamatTábla.Rows[sor].Cells[0].Value.ToString()) - 1).ToString() + "'";
                szöveg += " WHERE munkafolyamat='" + MunkafolyamatTábla.Rows[sor].Cells[3].Value.ToString().Trim() + "'";
                MyA.ABMódosítás(hely, jelszó, szöveg);
                // az előzőt hátrébb rakjuk
                szöveg = " UPDATE folyamattábla SET ";
                szöveg += " id='" + double.Parse(MunkafolyamatTábla.Rows[sor].Cells[0].Value.ToString()).ToString() + "'";
                szöveg += " WHERE munkafolyamat='" + MunkafolyamatTábla.Rows[MunkafolyamatTábla.SelectedRows[0].Index - 1].Cells[3].Value.ToString().Trim() + "'";
                MyA.ABMódosítás(hely, jelszó, szöveg);

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

                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text}\Adatok\Munkalap\munkalap{Dátum.Value:yyyy}.mdb";
                string jelszó = "kismalac";
                if (!File.Exists(hely)) return;
                // megnézzük, hogy van-e sorszám

                //Új
                string szöveg = "SELECT * FROM munkarendtábla";
                Kezelő_MunkaRend Kéz = new Kezelő_MunkaRend();
                List<Adat_MunkaRend> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);


                if (IDrend.Text.Trim() == "")
                {
                    double i = Adatok.Any() ? Adatok.Max(a => a.ID) + 1 : 1;

                    IDrend.Text = i.ToString();
                    szöveg = "INSERT INTO munkarendtábla (id, munkarend, látszódik)  VALUES (";
                    szöveg += IDrend.Text + ", ";
                    szöveg += "'" + MunkarendText.Text.Trim() + "', ";
                    szöveg += " true ) ";
                }
                else
                {
                    // ha már volt adat akkor módosítjuk
                    szöveg = " UPDATE  munkarendtábla SET ";
                    szöveg += " munkarend='" + MunkarendText.Text.Trim() + "' ";
                    szöveg += " WHERE id=" + IDrend.Text;
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
                Rendlistáz();

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
                string hely = Application.StartupPath + $@"\{Cmbtelephely.Text}\Adatok\Munkalap\munkalap{Dátum.Value.Year}.mdb";
                string jelszó = "kismalac";
                if (!File.Exists(hely))
                    return;
                string szöveg = "SELECT * FROM munkarendtábla ORDER BY id";

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

                Kezelő_MunkaRend kéz = new Kezelő_MunkaRend();
                List<Adat_MunkaRend> Adatok = kéz.Lista_Adatok(hely, jelszó, szöveg);

                int i;
                foreach (Adat_MunkaRend rekord in Adatok)
                {
                    MunkarendTábla.RowCount++;
                    i = MunkarendTábla.RowCount - 1;

                    MunkarendTábla.Rows[i].Cells[0].Value = rekord.ID;
                    MunkarendTábla.Rows[i].Cells[1].Value = rekord.Munkarend.Trim();
                    if (rekord.Látszódik)
                    {
                        MunkarendTábla.Rows[i].Cells[2].Value = "Érvényes";
                    }
                    else
                    {
                        MunkarendTábla.Rows[i].Cells[2].Value = "Törölt";
                    }
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
                if (IDrend.Text.Trim() == "")
                    return;
                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Munkalap\munkalap" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "kismalac";
                string szöveg = " UPDATE munkarendtábla SET látszódik=false WHERE id=" + IDrend.Text.Trim();
                MyA.ABMódosítás(hely, jelszó, szöveg);
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
                if (IDrend.Text.Trim() == "")
                    return;
                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Munkalap\munkalap" + Dátum.Value.ToString("yyyy") + ".mdb";
                string jelszó = "kismalac";
                string szöveg = " UPDATE munkarendtábla SET látszódik=true WHERE id=" + IDrend.Text.Trim();
                MyA.ABMódosítás(hely, jelszó, szöveg);
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
        #endregion


        #region Fejléc
        private void FejlécRögzít_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text}\Adatok\Munkalap\munkalap{Dátum.Value:yyyy}.mdb";
                string jelszó = "kismalac";
                string szöveg = "SELECT * FROM szolgálattábla ";


                //Új
                Kezelő_Munka_Szolgálat Kéz = new Kezelő_Munka_Szolgálat();
                List<Adat_Munka_Szolgálat> Adatok = Kéz.Lista_Adatok(hely, jelszó, szöveg);
                bool vane = Adatok.Any();
                if (!vane)
                {
                    szöveg = "INSERT INTO szolgálattábla (költséghely, szolgálat, üzem, A1, A2, A3, A4, A5, A6, A7)  VALUES (";
                    szöveg += "'" + Költséghely.Text.Trim() + "', ";
                    szöveg += "'" + Szolgálat.Text.Trim() + "', ";
                    szöveg += "'" + Üzem.Text.Trim() + "', ";
                    szöveg += " '0', '0', '0', '0', '0', '0', '0' )";
                }
                else
                {
                    // ha már volt adat akkor módosítjuk
                    szöveg = " UPDATE  szolgálattábla SET ";
                    szöveg += " költséghely='" + Költséghely.Text.Trim() + "', ";
                    szöveg += " szolgálat='" + Szolgálat.Text.Trim() + "', ";
                    szöveg += " üzem='" + Üzem.Text.Trim() + "' ";
                    szöveg += " WHERE A7='0'";
                }
                MyA.ABMódosítás(hely, jelszó, szöveg);
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
                string hely = $@"{Application.StartupPath}\" + Cmbtelephely.Text + @"\Adatok\Munkalap\munkalap" + Dátum.Value.ToString("yyyy") + ".mdb";
                if (!File.Exists(hely)) return;
                string jelszó = "kismalac";
                string szöveg = "SELECT * FROM szolgálattábla ";

                Kezelő_Munka_Szolgálat Kéz = new Kezelő_Munka_Szolgálat();
                Adat_Munka_Szolgálat Adat = Kéz.Egy_Adat(hely, jelszó, szöveg);
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