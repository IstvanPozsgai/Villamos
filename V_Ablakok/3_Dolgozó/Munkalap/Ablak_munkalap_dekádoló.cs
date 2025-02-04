using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Ablakok;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyF = Függvénygyűjtemény;

namespace Villamos
{

    public partial class Ablak_munkalap_dekádoló
    {
        readonly Kezelő_Munka_Idő KézMunkaIdő = new Kezelő_Munka_Idő();
        readonly Kezelő_Munka_Rendelés KézRendelés = new Kezelő_Munka_Rendelés();
        readonly Kezelő_Munka_Adatok KézMunkaAdatok = new Kezelő_Munka_Adatok();


        #region Alap
        public Ablak_munkalap_dekádoló()
        {
            InitializeComponent();
            Start();
        }

        void Start()
        {
            Telephelyekfeltöltése();
            Dátum.Value = DateTime.Today.AddDays(-1);
            DekádDátum.Value = DateTime.Today.AddDays(-1);
            Jogosultságkiosztás();

            Fülekkitöltése();
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        private void Ablak_munkalap_dekádoló_Load(object sender, EventArgs e)
        {
        }

        private void Ablak_munkalap_dekádoló_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_munkalap_dekádoló_oszlopot_beilleszt?.Close();
            Új_Ablak_munkalap_dekádoló_oszlopot_készít?.Close();
            Új_Ablak_munkalap_dekádoló_csoport?.Close();
        }

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;
                // ide kell az összes gombot tenni amit szabályozni akarunk
                Rögzítés.Enabled = false;
                Button2.Enabled = false;
                Command14.Enabled = false;

                Command4.Enabled = false;
                Változattörlés.Enabled = false;
                Feljebb.Enabled = false;

                Command2.Enabled = false;
                Command.Enabled = false;
                Command3.Enabled = false;

                melyikelem = 86;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Rögzítés.Enabled = true;

                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Button2.Enabled = true;
                }
                // módosítás 3
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Command14.Enabled = true;
                }

                melyikelem = 87;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Command4.Enabled = true;
                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Változattörlés.Enabled = true;
                }
                // módosítás 3
                if (MyF.Vanjoga(melyikelem, 3))
                {
                    Feljebb.Enabled = true;
                }

                melyikelem = 88;
                // módosítás 1
                if (MyF.Vanjoga(melyikelem, 1))
                {
                    Command2.Enabled = true;

                }
                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                {
                    Command.Enabled = true;
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

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                Cmbtelephely.Items.AddRange(Listák.TelephelyLista_Személy(true));
                if (Program.PostásTelephely == "Főmérnökség")
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

        private void BtnSúgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\Munkalap_dekádoló.html";
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
                            Táblakitöltés0();
                            Táblakitöltés1();
                            break;
                        }
                    case 1:
                        {
                            break;
                        }

                    case 2:
                        {
                            Napiidőkbetöltése();
                            break;
                        }
                    case 3:
                        {
                            Táblakitöltés2();
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


        #region Új oszlop Bevitel
        Ablak_munkalap_dekádoló_oszlopot_készít Új_Ablak_munkalap_dekádoló_oszlopot_készít;
        private void Command7_Click(object sender, EventArgs e)
        {
            Új_Ablak_munkalap_dekádoló_oszlopot_készít?.Close();

            Új_Ablak_munkalap_dekádoló_oszlopot_készít = new Ablak_munkalap_dekádoló_oszlopot_készít();
            Új_Ablak_munkalap_dekádoló_oszlopot_készít.FormClosed += Ablak_munkalap_dekádoló_oszlopot_készít_Closed;
            Új_Ablak_munkalap_dekádoló_oszlopot_készít.Top = 150;
            Új_Ablak_munkalap_dekádoló_oszlopot_készít.Left = 500;
            Új_Ablak_munkalap_dekádoló_oszlopot_készít.Show();
            Új_Ablak_munkalap_dekádoló_oszlopot_készít.Változás += Újoszlopot_készít_ÚjElemek;
        }

        private void Ablak_munkalap_dekádoló_oszlopot_készít_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_munkalap_dekádoló_oszlopot_készít = null;
        }

        private void Újoszlopot_készít_ÚjElemek()
        {
            if (Új_Ablak_munkalap_dekádoló_oszlopot_készít != null)
            {
                Tábla1.Refresh();
                Tábla1.ColumnCount += 1;
                int i = Tábla1.ColumnCount - 1;
                Tábla1.Columns[i].Width = 100;

                Tábla1.Columns[i].HeaderText = Új_Ablak_munkalap_dekádoló_oszlopot_készít.Választott.Trim();
                Tábla1.Refresh();
            }
        }
        #endregion


        #region Új oszlopot beilleszt
        Ablak_munkalap_dekádoló_oszlopot_beilleszt Új_Ablak_munkalap_dekádoló_oszlopot_beilleszt;
        private void Command18_Click(object sender, EventArgs e)
        {
            Új_Ablak_munkalap_dekádoló_oszlopot_beilleszt?.Close();

            Új_Ablak_munkalap_dekádoló_oszlopot_beilleszt = new Ablak_munkalap_dekádoló_oszlopot_beilleszt(Dátum.Value, Cmbtelephely.Text.Trim());
            Új_Ablak_munkalap_dekádoló_oszlopot_beilleszt.FormClosed += Ablak_munkalap_dekádoló_oszlopot_beilleszt_Closed;
            Új_Ablak_munkalap_dekádoló_oszlopot_beilleszt.Top = 150;
            Új_Ablak_munkalap_dekádoló_oszlopot_beilleszt.Left = 500;
            Új_Ablak_munkalap_dekádoló_oszlopot_beilleszt.Show();
            Új_Ablak_munkalap_dekádoló_oszlopot_beilleszt.Változás += Újoszlopot_készít_előzményekből;
        }

        private void Ablak_munkalap_dekádoló_oszlopot_beilleszt_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_munkalap_dekádoló_oszlopot_beilleszt = null;
        }

        private void Újoszlopot_készít_előzményekből()
        {
            if (Új_Ablak_munkalap_dekádoló_oszlopot_beilleszt != null)
            {
                Tábla1.Refresh();
                Tábla1.ColumnCount += 1;
                int i = Tábla1.ColumnCount - 1;
                Tábla1.Columns[i].Width = 100;

                Tábla1.Columns[i].HeaderText = Új_Ablak_munkalap_dekádoló_oszlopot_beilleszt.Választott.Trim();
                Tábla1.Refresh();
            }
        }
        #endregion


        #region Csoport munkaidő ellenőrzés
        Ablak_munkalap_dekádoló_csoport Új_Ablak_munkalap_dekádoló_csoport;
        private void Benn_Lévő_Click(object sender, EventArgs e)
        {
            Új_Ablak_munkalap_dekádoló_csoport?.Close();

            Új_Ablak_munkalap_dekádoló_csoport = new Ablak_munkalap_dekádoló_csoport(Dátum.Value, Cmbtelephely.Text.Trim());
            Új_Ablak_munkalap_dekádoló_csoport.FormClosed += Ablak_munkalap_dekádoló_csoport_Closed;
            Új_Ablak_munkalap_dekádoló_csoport.Top = 150;
            Új_Ablak_munkalap_dekádoló_csoport.Left = 500;
            Új_Ablak_munkalap_dekádoló_csoport.Show();
        }

        private void Ablak_munkalap_dekádoló_csoport_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_munkalap_dekádoló_csoport = null;
        }
        #endregion


        #region Napi Összesítő
        private void Táblakitöltés0()
        {
            try
            {
                Tábla.Rows.Clear();
                Tábla.Columns.Clear();
                Tábla.Refresh();
                Tábla.Visible = false;
                Tábla.ColumnCount = 3;
                // fejléc elkészítése
                Tábla.Columns[0].HeaderText = "Napi idő";
                Tábla.Columns[0].Width = 90;
                Tábla.Columns[0].ReadOnly = true;
                Tábla.Columns[1].HeaderText = "Fő";
                Tábla.Columns[1].Width = 55;
                Tábla.Columns[2].HeaderText = "Összesen";
                Tábla.Columns[2].Width = 85;
                Tábla.Columns[2].ReadOnly = true;

                List<Adat_Munka_Idő> Adatok = KézMunkaIdő.Lista_Adatok(Cmbtelephely.Text.Trim());
                int i;
                foreach (Adat_Munka_Idő rekord in Adatok)
                {
                    Tábla.RowCount++;
                    i = Tábla.RowCount - 1;
                    Tábla.Rows[i].Cells[0].Value = rekord.Idő;
                }

                Tábla.RowCount++;
                i = Tábla.RowCount - 1;
                Tábla.Rows[i].Cells[0].Value = "Összesen";
                Tábla.Rows[i].Cells[1].ReadOnly = true;
                Tábla.Visible = true;
                Tábla.Refresh();
                Tábla.ClearSelection();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Dátum_ValueChanged(object sender, EventArgs e)
        {
            Táblakitöltés0();
            Táblakitöltés1();
            Táblakitöltés2();
            Napiidőkbetöltése();
            Rögzítés.Enabled = true;
        }

        private void Command6_Click(object sender, EventArgs e)
        {
            RendelkezésText.Text = "";
            FelhasználtText.Text = "";
            RendelkezésText.BackColor = Color.MediumOrchid;
            FelhasználtText.BackColor = Color.MediumOrchid;
            Rögzítés.Visible = false;

            Táblakitöltés0();
            Táblakitöltés1();
            Táblakitöltés2();
            Napiidőkbetöltése();

            Rögzítés.Enabled = true;
        }

        private void Command11_Click(object sender, EventArgs e)
        {
            Táblaszámoló();
            Táblaszámoló1();
            Ellenörzés();
        }

        private void Táblaszámoló()
        {
            try
            {
                // kiszámoljuk a tábla értékeit
                int Összeg = 0;
                int szorzó = 0;
                int szorzandó = 0;
                int fő = 0;
                {
                    for (int i = 0; i < Tábla.Rows.Count - 1; i++)
                    {

                        if (Tábla.Rows[i].Cells[0].Value == null || !int.TryParse(Tábla.Rows[i].Cells[0].Value.ToString(), out int result))
                        {
                            szorzó = 0;
                        }
                        else
                        {
                            szorzó = result;
                        }

                        if (Tábla.Rows[i].Cells[1].Value == null || !int.TryParse(Tábla.Rows[i].Cells[1].Value.ToString(), out int result1))
                        {
                            szorzandó = 0;
                        }
                        else
                        {
                            szorzandó = result1;
                            fő += szorzandó;
                        }
                        Összeg += szorzó * szorzandó;
                        Tábla.Rows[i].Cells[2].Value = szorzó * szorzandó;
                    }
                    Tábla.Rows[Tábla.Rows.Count - 1].Cells[2].Value = Összeg;
                    Tábla.Rows[Tábla.Rows.Count - 1].Cells[1].Value = fő;
                }
                RendelkezésText.Text = Összeg.ToString();
                Ellenörzés();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Ellenörzés()
        {
            try
            {
                if (FelhasználtText.Text.Trim() == "" || RendelkezésText.Text.Trim() == "") return;
                if (FelhasználtText.Text.Trim() == "0" | RendelkezésText.Text.Trim() == "0") return;
                RendelkezésText.ForeColor = Color.White;
                FelhasználtText.ForeColor = Color.White;
                if (RendelkezésText.Text.Trim() == FelhasználtText.Text.Trim())
                {
                    RendelkezésText.BackColor = Color.Green;
                    FelhasználtText.BackColor = Color.Green;

                    Rögzítés.Visible = true;
                }
                else
                {
                    RendelkezésText.BackColor = Color.Red;
                    FelhasználtText.BackColor = Color.Red;
                    Rögzítés.Visible = false;
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

        private void Táblakitöltés1()
        {
            try
            {
                string szöveg1;

                Tábla1.Rows.Clear();
                Tábla1.Columns.Clear();
                Tábla1.Refresh();
                Tábla1.Visible = false;
                Tábla1.ColumnCount = 1;


                List<Adat_Munka_Rendelés> Adatok = KézRendelés.Lista_Adatok(Cmbtelephely.Text.Trim());
                int i = 0;
                foreach (Adat_Munka_Rendelés rekord in Adatok)
                {
                    if (i != 0)
                    {
                        Tábla1.ColumnCount += 1;
                    }
                    szöveg1 = rekord.Rendelés.Trim() + "\r\n";
                    szöveg1 += rekord.Műveletet.Trim() + "\r\n";
                    szöveg1 += rekord.Megnevezés.Trim() + "\r\n";
                    szöveg1 += rekord.Pályaszám.Trim() + "\r\n";
                    Tábla1.Columns[i].HeaderText = szöveg1;
                    Tábla1.Columns[i].Width = 100;
                    i += 1;
                }

                Tábla1.RowCount = 20;
                Tábla1.Rows[19].ReadOnly = true;
                Tábla1.Visible = true;
                Tábla1.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Táblaszámoló1()
        {
            try
            {
                // kiszámoljuk a tábla értékeit
                int összesen = 0;
                int összeg = 0;
                {
                    for (int j = 0; j < Tábla1.ColumnCount; j++)
                    {
                        összeg = 0;
                        for (int i = 0; i < Tábla1.RowCount - 1; i++)
                        {

                            if (Tábla1.Rows[i].Cells[j].Value != null && int.TryParse(Tábla1.Rows[i].Cells[j].Value.ToStrTrim(), out int Cellaérték))
                                összeg += Cellaérték;

                        }
                        Tábla1.Rows[Tábla1.RowCount - 1].Cells[j].Value = összeg;
                        összesen += összeg;
                    }
                    FelhasználtText.Text = összesen.ToString();
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

        private void Tábla1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in Tábla1.Rows)
                {
                    if (row.ReadOnly == true)
                    {
                        row.DefaultCellStyle.ForeColor = Color.White;
                        row.DefaultCellStyle.BackColor = Color.IndianRed;
                        row.DefaultCellStyle.Font = new Font("Arial Narrow", 12f, FontStyle.Bold);
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

        private void Command24_Click(object sender, EventArgs e)
        {
            // új sor beszúrása
            {
                Tábla1.Rows[Tábla1.RowCount - 1].ReadOnly = false;
                Tábla1.RowCount += 1;
                Tábla1.Rows[Tábla1.RowCount - 1].ReadOnly = true;
            }
        }

        private void Rögzítés_Click(object sender, EventArgs e)
        {
            try
            {
                Rögzítés.Enabled = false;

                List<Adat_Munka_Adatok> Adatok = new List<Adat_Munka_Adatok>();
                for (int i = 0; i < Tábla1.ColumnCount; i++)
                {
                    if (int.TryParse(Tábla1.Rows[Tábla1.RowCount - 1].Cells[i].Value.ToString(), out int idő) && idő != 0)
                    {
                        string fejléc = Tábla1.Columns[i].HeaderText;
                        string[] daraboló = fejléc.Replace("\r", "").Split('\n');
                        // szétdaraboljuk a fejlécet 
                        string rendelés = daraboló[0].Trim();
                        string művelet = daraboló[1].Trim();
                        string megnevezés = daraboló[2].Trim();
                        string pályaszám = daraboló[3].Trim();

                        Adat_Munka_Adatok ADAT = new Adat_Munka_Adatok(0, idő, Dátum.Value, megnevezés, művelet, pályaszám, rendelés, true);
                        Adatok.Add(ADAT);
                    }
                }
                KézMunkaAdatok.Rögzítés(Cmbtelephely.Text.Trim(), Dátum.Value.Year, Adatok);
                Rögzítés.Enabled = true;
                Rögzítés.Visible = false;
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


        #region Dekád 
        private void Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla3.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog1.InitialDirectory = "MyDocuments";
                SaveFileDialog1.Title = "Listázott tartalom mentése Excel fájlba";
                SaveFileDialog1.FileName = $"Idők_{Program.PostásTelephely.Trim()}_{DateTime.Now:yyyyMMddHHmmss}";
                SaveFileDialog1.Filter = "Excel |*.xlsx";
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                Module_Excel.EXCELtábla(fájlexc, Tábla3, true);
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

        private void Command10_Click(object sender, EventArgs e)
        {
            Napilista();
        }

        private void Napilista()
        {
            List<Adat_Munka_Adatok> AdatokÖ = KézMunkaAdatok.Lista_Adatok(Cmbtelephely.Text.Trim(), DekádDátum.Value.Year);
            List<Adat_Munka_Adatok> Adatok = (from a in AdatokÖ
                                              where a.Státus == true
                                              && a.Dátum.ToShortDateString() == DekádDátum.Value.ToShortDateString()
                                              select a).ToList();
            Tábla_Napilista(Adatok);
            Command14.Visible = true;
            Button2.Visible = true;
        }

        private void Tábla_Napilista(List<Adat_Munka_Adatok> Adatok)
        {
            try
            {

                Tábla3.Rows.Clear();
                Tábla3.Columns.Clear();
                Tábla3.Refresh();
                Tábla3.Visible = false;
                Tábla3.ColumnCount = 8;

                // fejléc elkészítése
                Tábla3.Columns[0].HeaderText = "Sorszám";
                Tábla3.Columns[0].Width = 90;
                Tábla3.Columns[1].HeaderText = "Rendelés";
                Tábla3.Columns[1].Width = 110;
                Tábla3.Columns[2].HeaderText = "Művelet";
                Tábla3.Columns[2].Width = 90;
                Tábla3.Columns[3].HeaderText = "";
                Tábla3.Columns[3].Width = 90;
                Tábla3.Columns[4].HeaderText = "Munkaidő";
                Tábla3.Columns[4].Width = 150;
                Tábla3.Columns[5].HeaderText = "Típus";
                Tábla3.Columns[5].Width = 150;
                Tábla3.Columns[7].HeaderText = "Dátum";
                Tábla3.Columns[6].Width = 150;
                Tábla3.Columns[6].HeaderText = "Munka";
                Tábla3.Columns[7].Width = 110;

                foreach (Adat_Munka_Adatok rekord in Adatok)
                {
                    Tábla3.RowCount++;
                    int i = Tábla3.RowCount - 1;
                    Tábla3.Rows[i].Cells[0].Value = rekord.ID != 0 ? rekord.ID.ToString() : "";
                    Tábla3.Rows[i].Cells[0].ReadOnly = true;
                    Tábla3.Rows[i].Cells[1].Value = rekord.Rendelés.Trim();
                    Tábla3.Rows[i].Cells[2].Value = rekord.Művelet.Trim();
                    Tábla3.Rows[i].Cells[4].Value = rekord.Idő;
                    Tábla3.Rows[i].Cells[5].Value = rekord.Megnevezés;
                    Tábla3.Rows[i].Cells[6].Value = rekord.Pályaszám;
                    Tábla3.Rows[i].Cells[7].Value = rekord.Dátum != new DateTime(1900, 1, 1) ? rekord.Dátum.ToString("yyyy.MM.dd") : "";
                }

                Tábla3.Visible = true;
                Tábla3.Refresh();

                Command14.Visible = false;
                Button2.Visible = false;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapÖsszeg_Click(object sender, EventArgs e)
        {
            try
            {

                List<Adat_Munka_Adatok> AdatokÖ = KézMunkaAdatok.Lista_Adatok(Cmbtelephely.Text.Trim(), DekádDátum.Value.Year);
                List<Adat_Munka_Adatok> AdatokS = (from a in AdatokÖ
                                                   where a.Dátum.ToShortDateString() == DekádDátum.Value.ToShortDateString()
                                                   && a.Státus == true
                                                   select a).ToList();

                List<Adat_Munka_Adatok> Adatok = (from a in AdatokS
                                                  group a by new { a.Rendelés, a.Művelet } into csoport
                                                  select new Adat_Munka_Adatok(
                                                      0,
                                                      csoport.Sum(x => x.Idő),
                                                      DekádDátum.Value,
                                                      csoport.First().Megnevezés,
                                                      csoport.Key.Művelet,
                                                      csoport.First().Pályaszám,
                                                      csoport.Key.Rendelés,
                                                      true
                                                  )).ToList();

                Tábla_Napilista(Adatok);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Command13_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime elsőnap = DekádDátum.Value;
                DateTime utolsónap = DekádDátum.Value;

                if (Option1.Checked)
                {
                    elsőnap = MyF.Hónap_elsőnapja(DekádDátum.Value);
                    utolsónap = new DateTime(DekádDátum.Value.Year, DekádDátum.Value.Month, 10);
                }
                if (Option2.Checked)
                {
                    elsőnap = new DateTime(DekádDátum.Value.Year, DekádDátum.Value.Month, 11);
                    utolsónap = new DateTime(DekádDátum.Value.Year, DekádDátum.Value.Month, 20);
                }
                if (Option3.Checked)
                {
                    elsőnap = new DateTime(DekádDátum.Value.Year, DekádDátum.Value.Month, 21);
                    utolsónap = MyF.Hónap_utolsónapja(DekádDátum.Value);
                }
                if (Option4.Checked)
                {
                    elsőnap = MyF.Hónap_elsőnapja(DekádDátum.Value);
                    utolsónap = MyF.Hónap_utolsónapja(DekádDátum.Value);
                }
                List<Adat_Munka_Adatok> AdatokÖ = KézMunkaAdatok.Lista_Adatok(Cmbtelephely.Text.Trim(), DekádDátum.Value.Year);

                List<Adat_Munka_Adatok> AdatokS = (from a in AdatokÖ
                                                   where a.Státus == true
                                                   && a.Dátum >= elsőnap
                                                   && a.Dátum <= utolsónap
                                                   select a).ToList();

                List<Adat_Munka_Adatok> Adatok = (from a in AdatokS
                                                  group a by new { a.Rendelés, a.Művelet } into csoport
                                                  select new Adat_Munka_Adatok(
                                                      0,
                                                      csoport.Sum(x => x.Idő),
                                                      new DateTime(1900, 1, 1),
                                                      "",
                                                      csoport.Key.Művelet,
                                                      "",
                                                      csoport.Key.Rendelés,
                                                      true
                                                  )).ToList();
                Tábla_Napilista(Adatok);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Command14_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla3.Rows.Count < 1) return;

                if (MessageBox.Show(DekádDátum.Value.ToShortDateString() + "-i adatok törlésére készülsz, biztos törlöd?", "Figyelmeztetés", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    List<long> IdK = new List<long>();
                    for (int i = 0; i < Tábla3.Rows.Count; i++)
                        if (long.TryParse(Tábla3.Rows[i].Cells[0].Value.ToString(), out long id)) IdK.Add(id);

                    KézMunkaAdatok.Módosítás(Cmbtelephely.Text.Trim(), DekádDátum.Value.Year, IdK);
                }

                Napilista();
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

        private void Command25_Click(object sender, EventArgs e)
        {
            Havilista();
        }

        private void Havilista()
        {
            try
            {
                DateTime elsőnap = MyF.Hónap_elsőnapja(DekádDátum.Value);
                DateTime utolsónap = MyF.Hónap_utolsónapja(DekádDátum.Value);

                List<Adat_Munka_Adatok> AdatokÖ = KézMunkaAdatok.Lista_Adatok(Cmbtelephely.Text.Trim(), DekádDátum.Value.Year);
                List<Adat_Munka_Adatok> Adatok = (from a in AdatokÖ
                                                  where a.Státus == true
                                                  && a.Dátum >= elsőnap
                                                  && a.Dátum <= utolsónap
                                                  orderby a.Rendelés, a.Dátum
                                                  select a).ToList();
                Tábla_Napilista(Adatok);
                Command14.Visible = true;
                Button2.Visible = true;
            }
            catch (HibásBevittAdat ex)
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
            try
            {
                // rendelési szám módosítás
                if (Tábla3.Rows.Count < 1) return;
                if (Tábla3.SelectedRows.Count < 0) throw new HibásBevittAdat("Nincs Kijelölve a táblázatban módosítandó elem.");
                if (Tábla3.Rows[Tábla3.SelectedRows[0].Index].Cells[1].Value.ToStrTrim() == "") throw new HibásBevittAdat("A kijelöléshez tartozó rendelésiszám érvénytelen.");
                if (!int.TryParse(Tábla3.Rows[Tábla3.SelectedRows[0].Index].Cells[0].Value.ToStrTrim(), out int sorszám)) throw new HibásBevittAdat("A kijelöléshez tartozó sorszám érvénytelen.");

                KézMunkaAdatok.Módosítás(Cmbtelephely.Text.Trim(), DekádDátum.Value.Year, Tábla3.Rows[Tábla3.SelectedRows[0].Index].Cells[1].Value.ToStrTrim(), sorszám);
                Napilista();
                MessageBox.Show("A rendelési szám értéke megváltoztatva. ", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tábla3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Tábla3.Rows[e.RowIndex].Selected = true;
        }

        private void Tábla3_SelectionChanged(object sender, EventArgs e)
        {
            if (Tábla3.Columns[3].HeaderText == "Napi")
            {
                Command14.Visible = true;
                Button2.Visible = true;
            }
        }
        #endregion


        #region Napi munkaidő adatok
        private void Napiidőkbetöltése()
        {
            try
            {
                List<Adat_Munka_Idő> Adatok = KézMunkaIdő.Lista_Adatok(Cmbtelephely.Text.Trim());
                MunkaIdő.Items.Clear();
                MunkaIdő.BeginUpdate();
                foreach (Adat_Munka_Idő elem in Adatok)
                    MunkaIdő.Items.Add(elem.Idő);
                MunkaIdő.EndUpdate();
            }
            catch (HibásBevittAdat ex)
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
                if (Text1.Text.Trim() == "") throw new HibásBevittAdat("Nincs kitöltve a beviteli mező, így nem lehet rögzíteni.");
                if (!int.TryParse(Text1.Text, out int Idő)) throw new HibásBevittAdat("A beviteli mezőbe csak egész számot lehet írni.");

                Adat_Munka_Idő ADAT = new Adat_Munka_Idő(0, Idő);
                KézMunkaIdő.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);

                Napiidőkbetöltése();
                Text1.Text = "";
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

        private void Változattörlés_Click(object sender, EventArgs e)
        {
            try
            {
                if (MunkaIdő.SelectedIndex < 0) throw new HibásBevittAdat("Nincs kiválasztva törlendő mennyiség.");
                if (!int.TryParse(MunkaIdő.Items[MunkaIdő.SelectedIndex].ToString(), out int Idő)) throw new HibásBevittAdat("A beviteli mezőbe csak egész számot lehet írni.");

                Adat_Munka_Idő ADAT = new Adat_Munka_Idő(0, Idő);
                KézMunkaIdő.Törlés(Cmbtelephely.Text.Trim(), ADAT);

                Napiidőkbetöltése();
                MessageBox.Show("Az adattörlése megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Feljebb_Click(object sender, EventArgs e)
        {
            try
            {
                if (MunkaIdő.SelectedIndex <= 0) throw new HibásBevittAdat("Az első elemet nem lehet előrébb tenni.");
                if (!long.TryParse(MunkaIdő.Items[MunkaIdő.SelectedIndex - 1].ToStrTrim(), out long sorelőző)) throw new HibásBevittAdat("Hibás adat.");
                if (!long.TryParse(MunkaIdő.Items[MunkaIdő.SelectedIndex].ToStrTrim(), out long sor)) throw new HibásBevittAdat("Hibás adat.");

                KézMunkaIdő.Csere(Cmbtelephely.Text.Trim(), sorelőző, sor);
                Napiidőkbetöltése();
            }
            catch (HibásBevittAdat ex)
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


        #region Napi Munkaidő lista
        private void Táblakitöltés2()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\{Cmbtelephely.Text.Trim()}\adatok\Munkalap\munkalapösszesítő.mdb";
                if (!System.IO.File.Exists(hely)) return;

                Tábla2.Rows.Clear();
                Tábla2.Columns.Clear();
                Tábla2.Refresh();
                Tábla2.Visible = false;
                Tábla2.ColumnCount = 5;

                // fejléc elkészítése
                Tábla2.Columns[0].HeaderText = "Sorszám";
                Tábla2.Columns[0].Width = 150;
                Tábla2.Columns[1].HeaderText = "Rendelés";
                Tábla2.Columns[1].Width = 150;
                Tábla2.Columns[2].HeaderText = "Művelet";
                Tábla2.Columns[2].Width = 150;
                Tábla2.Columns[3].HeaderText = "Típus";
                Tábla2.Columns[3].Width = 150;
                Tábla2.Columns[4].HeaderText = "Munka";
                Tábla2.Columns[4].Width = 150;

                List<Adat_Munka_Rendelés> Adatok = KézRendelés.Lista_Adatok(Cmbtelephely.Text.Trim());

                foreach (Adat_Munka_Rendelés rekord in Adatok)
                {

                    Tábla2.RowCount++;
                    int i = Tábla2.RowCount - 1;

                    Tábla2.Rows[i].Cells[0].Value = rekord.ID;
                    Tábla2.Rows[i].Cells[1].Value = rekord.Rendelés;
                    Tábla2.Rows[i].Cells[2].Value = rekord.Műveletet;
                    Tábla2.Rows[i].Cells[3].Value = rekord.Megnevezés;
                    Tábla2.Rows[i].Cells[4].Value = rekord.Pályaszám;
                }

                Tábla2.Visible = true;
                Tábla2.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tábla2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) Tábla2.Rows[e.RowIndex].Selected = true;
        }

        private void Tábla2_SelectionChanged(object sender, EventArgs e)
        {
            if (Tábla2.SelectedRows.Count != 0)
            {
                Napi_id.Text = Tábla2.Rows[Tábla2.SelectedRows[0].Index].Cells[0].Value.ToString();
                TextRendelés.Text = Tábla2.Rows[Tábla2.SelectedRows[0].Index].Cells[1].Value.ToString();
                TextMűvelet.Text = Tábla2.Rows[Tábla2.SelectedRows[0].Index].Cells[2].Value.ToString();
                TextMegnevezés.Text = Tábla2.Rows[Tábla2.SelectedRows[0].Index].Cells[3].Value.ToString();
                TextPályaszám.Text = Tábla2.Rows[Tábla2.SelectedRows[0].Index].Cells[4].Value.ToString();
            }
        }

        private void Command2_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextRendelés.Text.Trim() == "") throw new HibásBevittAdat("A rendelési szám mező nem lehet üres.");
                if (TextMűvelet.Text.Trim() == "") TextMűvelet.Text = "_";
                if (TextMegnevezés.Text.Trim() == "") TextMegnevezés.Text = "_";
                if (TextMűvelet.Text.Trim() == "") TextMűvelet.Text = "_";
                if (!long.TryParse(Napi_id.Text.Trim(), out long napi)) napi = 0;

                List<Adat_Munka_Rendelés> Adatok = KézRendelés.Lista_Adatok(Cmbtelephely.Text.Trim());

                Adat_Munka_Rendelés vane = (from a in Adatok
                                            where a.ID == napi
                                            select a).FirstOrDefault();
                Adat_Munka_Rendelés ADAT = new Adat_Munka_Rendelés(napi,
                                                                   TextMegnevezés.Text.Trim(),
                                                                   TextMűvelet.Text.Trim(),
                                                                   TextPályaszám.Text.Trim(),
                                                                   TextRendelés.Text.Trim());

                if (vane == null)
                    KézRendelés.Rögzítés(Cmbtelephely.Text.Trim(), ADAT);
                else
                    KézRendelés.Módosítás(Cmbtelephely.Text.Trim(), ADAT);

                Táblakitöltés2();
                TextRendelés.Text = "";
                TextMűvelet.Text = "";
                TextMegnevezés.Text = "";
                TextPályaszám.Text = "";
                Napi_id.Text = "";
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

        private void Command_Click(object sender, EventArgs e)
        {
            try
            {
                if (TextRendelés.Text.Trim() == "") return;
                if (!long.TryParse(Napi_id.Text, out long ID)) ID = 0;
                KézRendelés.Törlés(Cmbtelephely.Text.Trim(), ID);

                Táblakitöltés2();
                TextRendelés.Text = "";
                TextMűvelet.Text = "";
                TextMegnevezés.Text = "";
                TextPályaszám.Text = "";
                Napi_id.Text = "";
                MessageBox.Show("Az adattörlése megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
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
                //az első elemet nem lehet előrébb vinni
                if (Tábla2.SelectedRows[0].Index < 1) throw new HibásBevittAdat("Az első elemet nem lehet előrébb vinni.");

                long előző = long.Parse(Tábla2.Rows[Tábla2.SelectedRows[0].Index - 1].Cells[0].Value.ToString());
                long következő = long.Parse(Tábla2.Rows[Tábla2.SelectedRows[0].Index].Cells[0].Value.ToString());
                KézRendelés.Csere(Cmbtelephely.Text.Trim(), előző, következő);

                Táblakitöltés2();
                TextRendelés.Text = "";
                TextMűvelet.Text = "";
                TextMegnevezés.Text = "";
                TextPályaszám.Text = "";
                Napi_id.Text = "";
            }
            catch (HibásBevittAdat ex)
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