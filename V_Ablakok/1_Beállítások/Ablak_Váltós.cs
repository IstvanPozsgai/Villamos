using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyEn = Villamos.V_MindenEgyéb.Enumok;
using MyF = Függvénygyűjtemény;



namespace Villamos
{
    public partial class Ablak_Váltós
    {
        #region Változók
        double Tizenkétóra = 0;
        int Munkarend8 = 0;
        int Munkarend12 = 0;
        int sor = 0;
        int oszlop = 0;
        int SorVáltó = 0;
        int OszlopVáltó = 0;
        int ScrollX = 0;
        int ScrollY = 0;
        int VScrollX = 0;
        int VScrollY = 0;
        int TextElső = 0;
        int TextMásodik = 0;

        double Epihenő = 0;
        double Mpihenő = 0;
        double Évpihenő = 0;
        double EfélévNappal = 0;
        double MfélévNappal = 0;
        double Egészév = 0;
        #endregion


        #region Kezelők
        readonly Kezelő_Kiegészítő_Beosegéd KézBeoSegéd = new Kezelő_Kiegészítő_Beosegéd();
        readonly Kezelő_Kiegészítő_Túlórakeret KézTúlórakeret = new Kezelő_Kiegészítő_Túlórakeret();
        readonly Kezelő_Váltós_Váltóstábla KézVáltóstábla = new Kezelő_Váltós_Váltóstábla();
        readonly Kezelő_Váltós_Naptár KézVNaptár = new Kezelő_Váltós_Naptár();
        readonly Kezelő_Kiegészítő_Turnusok KézTurnusok = new Kezelő_Kiegészítő_Turnusok();
        readonly Kezelő_Kiegészítő_Váltóstábla KézKiegVáltóstábla = new Kezelő_Kiegészítő_Váltóstábla();
        readonly Kezelő_Kiegészítő_Munkaidő KézMunkaidő = new Kezelő_Kiegészítő_Munkaidő();
        readonly Kezelő_Váltós_Összesítő KézVáltÖsszesítő = new Kezelő_Váltós_Összesítő();
        readonly Kezelő_Kiegészítő_Beosztásciklus KézBeosztásciklus = new Kezelő_Kiegészítő_Beosztásciklus();
        readonly Kezelő_Váltós_Váltóscsopitábla KézVáltóscsopitábla = new Kezelő_Váltós_Váltóscsopitábla();
        readonly Kezelő_Váltós_Kijelöltnapok KézKijelöltnapok = new Kezelő_Váltós_Kijelöltnapok();
        readonly Kezelő_Kiegészítő_Könyvtár KézKiegKönyvtár = new Kezelő_Kiegészítő_Könyvtár();
        #endregion


        #region Listák
        List<Adat_Kiegészítő_Beosegéd> AdatokBeoSegéd = new List<Adat_Kiegészítő_Beosegéd>();
        List<Adat_Kiegészítő_Túlórakeret> AdatokTúlórakeret = new List<Adat_Kiegészítő_Túlórakeret>();
        List<Adat_Váltós_Váltóstábla> AdatokVáltóstábla = new List<Adat_Váltós_Váltóstábla>();
        List<Adat_Kiegészítő_Turnusok> AdatokTurnusok = new List<Adat_Kiegészítő_Turnusok>();
        List<Adat_Kiegészítő_Váltóstábla> AdatokKiegVáltóstábla = new List<Adat_Kiegészítő_Váltóstábla>();
        List<Adat_Kiegészítő_Munkaidő> AdatokMunkaidő = new List<Adat_Kiegészítő_Munkaidő>();
        List<Adat_Kiegészítő_Beosztásciklus> AdatokBeosztásciklusVáltó = new List<Adat_Kiegészítő_Beosztásciklus>();
        List<Adat_Kiegészítő_Beosztásciklus> AdatokÉjszakásBeoCiklus = new List<Adat_Kiegészítő_Beosztásciklus>();
        List<Adat_Váltós_Váltóscsopitábla> AdatokVáltóscsopitábla = new List<Adat_Váltós_Váltóscsopitábla>();
        List<Adat_Váltós_Kijelöltnapok> AdatokKijelöltnapok = new List<Adat_Váltós_Kijelöltnapok>();
        #endregion


        #region alap
        public Ablak_Váltós()
        {
            InitializeComponent();
        }

        private void Ablak_Váltós_Load(object sender, EventArgs e)
        {
            GombLathatosagKezelo.Beallit(this);
            Jogosultságkiosztás();
            Fülek.SelectedIndex = 0;
            Fülekkitöltése();
            Munkarend8és12kiirás();
            Fülek.DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        private void Jogosultságkiosztás()
        {
            int melyikelem;
            // Főmérnöség esetén lehet módosítani
            if (Program.PostásTelephely.Trim() == "Főmérnökség")
            {
                Tábla_BeoKód_Új.Visible = true;
                Tábla_BeoKód_OK.Visible = true;
                Tábla_BeoKód_Töröl.Visible = true;

                Tábla_Keret_Új.Visible = true;
                Tábla_Keret_OK.Visible = true;
                Tábla_Keret_Töröl.Visible = true;

                Éves_Töröl.Visible = true;
                Éves_Új.Visible = true;
                Éves_Ok.Visible = true;
                Éves_Generál.Visible = true;

                CsopVez_Töröl.Visible = true;
                CsopVez_Ok.Visible = true;

                VáltMunka_Töröl.Visible = true;
                VáltMunka_Feljebb.Visible = true;
                VáltMunka_OK.Visible = true;
                VáltMunka_Új.Visible = true;

                Éjszaka_ÚJ.Visible = true;
                Éjszaka_Ok.Visible = true;
                Éjszaka_Feljebb.Visible = true;
                Éjszaka_Töröl.Visible = true;

                Turnus_Ok.Visible = true;
                Turnus_Töröl.Visible = true;
                Csoport_OK.Visible = true;
                Csoport_Töröl.Visible = true;
                MunkaRend_OK.Visible = true;
                MunkaRend_Töröl.Visible = true;

                Elvont_OK.Visible = true;
                Elvont_Új.Visible = true;
                Elvont_Töröl.Visible = true;
                Elvont_Generált.Visible = true;

                Nappal_Ok.Visible = true;

                Panel5.Enabled = true;
                Panel4.Enabled = true;
            }
            else
            {
                Tábla_BeoKód_Új.Visible = false;
                Tábla_BeoKód_OK.Visible = false;
                Tábla_BeoKód_Töröl.Visible = false;

                Tábla_Keret_Új.Visible = false;
                Tábla_Keret_OK.Visible = false;
                Tábla_Keret_Töröl.Visible = false;

                Éves_Töröl.Visible = false;
                Éves_Új.Visible = false;
                Éves_Ok.Visible = false;
                Éves_Generál.Visible = false;

                CsopVez_Töröl.Visible = false;
                CsopVez_Ok.Visible = false;

                VáltMunka_Töröl.Visible = false;
                VáltMunka_Feljebb.Visible = false;
                VáltMunka_OK.Visible = false;
                VáltMunka_Új.Visible = false;

                Éjszaka_ÚJ.Visible = false;
                Éjszaka_Ok.Visible = false;
                Éjszaka_Feljebb.Visible = false;
                Éjszaka_Töröl.Visible = false;

                Turnus_Ok.Visible = false;
                Turnus_Töröl.Visible = false;
                Csoport_OK.Visible = false;
                Csoport_Töröl.Visible = false;
                MunkaRend_OK.Visible = false;
                MunkaRend_Töröl.Visible = false;

                Elvont_OK.Visible = false;
                Elvont_Új.Visible = false;
                Elvont_Töröl.Visible = false;
                Elvont_Generált.Visible = false;

                Nappal_Ok.Visible = false;

                Command30.Visible = false;
                Panel5.Enabled = false;
                Panel4.Enabled = false;
            }
            // ide kell az összes gombot tenni amit szabályozni akarunk false
            Tábla_BeoKód_Új.Enabled = false;
            Tábla_BeoKód_OK.Enabled = false;
            Tábla_BeoKód_Töröl.Enabled = false;

            Tábla_Keret_Új.Enabled = false;
            Tábla_Keret_OK.Enabled = false;
            Tábla_Keret_Töröl.Enabled = false;

            CsopVez_Töröl.Enabled = false;
            CsopVez_Ok.Enabled = false;

            Éves_Töröl.Enabled = false;
            Éves_Új.Enabled = false;
            Éves_Ok.Enabled = false;
            Éves_Generál.Enabled = false;

            VáltMunka_Töröl.Enabled = false;
            VáltMunka_Feljebb.Enabled = false;
            VáltMunka_OK.Enabled = false;
            VáltMunka_Új.Enabled = false;

            Éjszaka_ÚJ.Enabled = false;
            Éjszaka_Ok.Enabled = false;
            Éjszaka_Feljebb.Enabled = false;
            Éjszaka_Töröl.Enabled = false;

            Turnus_Ok.Enabled = false;
            Turnus_Töröl.Enabled = false;
            Csoport_OK.Enabled = false;
            Csoport_Töröl.Enabled = false;
            MunkaRend_OK.Enabled = false;
            MunkaRend_Töröl.Enabled = false;

            Elvont_OK.Enabled = false;
            Elvont_Új.Enabled = false;
            Elvont_Töröl.Enabled = false;
            Elvont_Generált.Enabled = false;

            Nappal_Ok.Enabled = false;

            Command30.Enabled = false;
            melyikelem = 11;
            // módosítás 1 beosztáskód
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Tábla_BeoKód_Új.Enabled = true;
                Tábla_BeoKód_OK.Enabled = true;
                Tábla_BeoKód_Töröl.Enabled = true;
            }
            // módosítás 2 túlóra keret
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Tábla_Keret_Új.Enabled = true;
                Tábla_Keret_OK.Enabled = true;
                Tábla_Keret_Töröl.Enabled = true;
            }
            // módosítás 3 éves összesítő
            if (MyF.Vanjoga(melyikelem, 3))
            {
                Éves_Töröl.Enabled = true;
                Éves_Új.Enabled = true;
                Éves_Ok.Enabled = true;
                Éves_Generál.Enabled = true;
            }

            melyikelem = 12;
            // módosítás 4 csoport turnusok
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Turnus_Ok.Enabled = true;
                Turnus_Töröl.Enabled = true;
                Csoport_OK.Enabled = true;
                Csoport_Töröl.Enabled = true;
                MunkaRend_OK.Enabled = true;
                MunkaRend_Töröl.Enabled = true;
            }
            // módosítás 5 Munkaidő naptár
            if (MyF.Vanjoga(melyikelem, 2))
            {
                Panel4.Enabled = true;
                Nappal_Ok.Enabled = false;
            }
            // módosítás 6 Váltós naptár
            if (MyF.Vanjoga(melyikelem, 3))
            {
                Panel5.Enabled = true;
                Command30.Enabled = false;

            }

            melyikelem = 13;
            // módosítás 7 elvont napok
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Elvont_OK.Enabled = true;
                Elvont_Új.Enabled = true;
                Elvont_Töröl.Enabled = true;
                Elvont_Generált.Enabled = true;
            }
            // módosítás 8 váltós munkarend
            if (MyF.Vanjoga(melyikelem, 2))
            {
                VáltMunka_Töröl.Enabled = true;
                VáltMunka_Feljebb.Enabled = true;
                VáltMunka_OK.Enabled = true;
                VáltMunka_Új.Enabled = true;
            }
            // módosítás 9 csopveznevek
            if (MyF.Vanjoga(melyikelem, 3))
            {
                CsopVez_Töröl.Enabled = true;
                CsopVez_Ok.Enabled = true;
            }

            melyikelem = 14;
            // módosítás 10 éjszakás munkarend
            if (MyF.Vanjoga(melyikelem, 1))
            {
                Éjszaka_ÚJ.Enabled = true;
                Éjszaka_Ok.Enabled = true;
                Éjszaka_Feljebb.Enabled = true;
                Éjszaka_Töröl.Enabled = true;
            }
            // módosítás 11
            if (MyF.Vanjoga(melyikelem, 2))
            {

            }
            // módosítás 12
            if (MyF.Vanjoga(melyikelem, 3))
            {

            }
        }

        private void Fülekkitöltése()
        {
            switch (Fülek.SelectedIndex)
            {
                case 0:
                    {
                        // Beosztáskód
                        Tábla_BeoKód_kiirás();
                        Telephelyfeltöltés();
                        break;
                    }
                case 1:
                    {
                        // túlóra keret
                        Tábla_Keret_kiirás();
                        TÚLTelephelyfeltöltés();
                        Label9.Text = "0 - Nincs tennivaló\r\n";
                        Label9.Text += "1 - Dolgozói hozzájárulás ellenőrzése\r\n";
                        Label9.Text += "5 - Szakszolgálati engedély\r\n";
                        Label9.Text += "9 - Több túlóra rögzítés nem lehetséges";
                        break;
                    }
                case 2:
                    {
                        // éves összesítő

                        ÉvesÉv.Text = DateTime.Today.Year.ToString();
                        Éves_Tábla_kiirás();
                        ÉvesTelephelyfeltöltés();
                        ÉvesCsoportfeltöltés();
                        VváltósCsoportfeltöltés();
                        break;
                    }
                case 3:
                    {
                        // Csoport turnusok
                        TurnusokFormListafeltöltés();
                        Csoport_Tábla_kiirás();
                        Tábla_Munkarend_kiirás();
                        Kezdődátum.Value = DateTime.Today;
                        break;
                    }

                case 4:
                    {
                        // munkaidő naptár
                        Dátumnappal.Value = DateTime.Today;
                        Nappaloslenyílófeltöltés();
                        break;
                    }
                case 5:
                    {
                        // váltós keret
                        VáltósNaptár.Value = DateTime.Today;
                        Váltóslenyílófeltöltés();
                        VváltósCsoportfeltöltés();
                        break;
                    }
                case 6:
                    {
                        // elvont napok
                        ElvontTelephelyfeltöltés();
                        ElvontÉv.Text = DateTime.Today.Year.ToString();
                        ElvontDátum.Value = DateTime.Today;
                        ElvontCsoportfeltöltés();
                        break;
                    }
                case 7:
                    {
                        // váltós munkarend
                        Tábla_VáltMunka_kiirás();
                        break;
                    }
                case 8:
                    {
                        // éjszakás munkarend
                        Tábla_Éjszaka_kiirás();
                        break;
                    }
                case 9:
                    {
                        // csopvez nevek
                        CSOPORTCsoportfeltöltés();
                        Tábla_CsopVez_kiirás();
                        CsoportvezTelephelyfeltöltés();
                        break;
                    }
            }
        }

        private void Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = Application.StartupPath + @"\Súgó\VillamosLapok\SzemélyVáltó.html";
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

        private void Ablak_Váltós_KeyDown(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 17)
                Chk_CTRL.Checked = true;
        }

        private void Ablak_Váltós_KeyUp(object sender, KeyEventArgs e)
        {
            if ((int)e.KeyCode == 17) Chk_CTRL.Checked = false;
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


        #region Beosztás lapfül
        private void Telephelyfeltöltés()
        {
            try
            {
                Telephely.Items.Clear();
                List<Adat_Kiegészítő_Könyvtár> Adatok = KézKiegKönyvtár.Lista_Adatok();

                Telephely.Items.Add("_");
                foreach (Adat_Kiegészítő_Könyvtár rekord in Adatok)
                    Telephely.Items.Add(rekord.Név);
                Telephely.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BEOkódFriss_Click(object sender, EventArgs e)
        {
            Tábla_BeoKód_kiirás();
        }

        private void Tábla_BeoKód_kiirás()
        {
            try
            {
                // **************************************
                // *****Beosztás kódok*******************
                // **************************************
                AdatokBeoSegéd = KézBeoSegéd.Lista_Adatok();


                Tábla_BeoKód.Rows.Clear();
                Tábla_BeoKód.Columns.Clear();
                Tábla_BeoKód.Refresh();
                Tábla_BeoKód.Visible = false;
                Tábla_BeoKód.ColumnCount = 6;

                // fejléc elkészítése
                Tábla_BeoKód.Columns[0].HeaderText = "Beosztáskód";
                Tábla_BeoKód.Columns[0].Width = 120;
                Tábla_BeoKód.Columns[1].HeaderText = "Túlóra";
                Tábla_BeoKód.Columns[1].Width = 100;
                Tábla_BeoKód.Columns[2].HeaderText = "Túlóra kezdete";
                Tábla_BeoKód.Columns[2].Width = 100;
                Tábla_BeoKód.Columns[3].HeaderText = "Túlóra vége";
                Tábla_BeoKód.Columns[3].Width = 100;
                Tábla_BeoKód.Columns[4].HeaderText = "Túlóra oka";
                Tábla_BeoKód.Columns[4].Width = 500;
                Tábla_BeoKód.Columns[5].HeaderText = "Telephely";
                Tábla_BeoKód.Columns[5].Width = 200;

                foreach (Adat_Kiegészítő_Beosegéd rekord in AdatokBeoSegéd)
                {
                    Tábla_BeoKód.RowCount++;
                    int i = Tábla_BeoKód.RowCount - 1;

                    Tábla_BeoKód.Rows[i].Cells[0].Value = rekord.Beosztáskód;
                    Tábla_BeoKód.Rows[i].Cells[1].Value = rekord.Túlóra;
                    Tábla_BeoKód.Rows[i].Cells[2].Value = rekord.Kezdőidő.ToString("HH:mm:ss");
                    Tábla_BeoKód.Rows[i].Cells[3].Value = rekord.Végeidő.ToString("HH:mm:ss");
                    Tábla_BeoKód.Rows[i].Cells[4].Value = rekord.Túlóraoka;
                    Tábla_BeoKód.Rows[i].Cells[5].Value = rekord.Telephely;
                }

                Tábla_BeoKód.Refresh();
                Tábla_BeoKód.Visible = true;
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tábla_BeoKód_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Tábla_BeoKód.Rows[e.RowIndex].Selected = true;
        }

        private void Tábla_BeoKód_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Tábla_BeoKód.Rows[e.RowIndex].Selected = true;
        }

        private void Tábla_BeoKód_SelectionChanged(object sender, EventArgs e)
        {
            if (Tábla_BeoKód.SelectedRows.Count != 0)
            {
                Beosztáskód.Text = Tábla_BeoKód.Rows[Tábla_BeoKód.SelectedRows[0].Index].Cells[0].Value.ToString();
                Túlóra.Text = Tábla_BeoKód.Rows[Tábla_BeoKód.SelectedRows[0].Index].Cells[1].Value.ToString();
                DateTime ideigkez = Tábla_BeoKód.Rows[Tábla_BeoKód.SelectedRows[0].Index].Cells[2].Value.ToÉrt_DaTeTime();
                Kezdőidő.Value = new DateTime(1900, 1, 1, ideigkez.Hour, ideigkez.Minute, ideigkez.Second);
                ideigkez = Tábla_BeoKód.Rows[Tábla_BeoKód.SelectedRows[0].Index].Cells[3].Value.ToÉrt_DaTeTime();
                Végeidő.Value = new DateTime(1900, 1, 1, ideigkez.Hour, ideigkez.Minute, ideigkez.Second);
                Túlóraoka.Text = Tábla_BeoKód.Rows[Tábla_BeoKód.SelectedRows[0].Index].Cells[4].Value.ToString();
                Telephely.Text = Tábla_BeoKód.Rows[Tábla_BeoKód.SelectedRows[0].Index].Cells[5].Value.ToString();
            }
        }

        private void Tábla_BeoKód_Új_Click(object sender, EventArgs e)
        {
            Beosztáskód.Text = "";
            Túlóra.Text = "";
            Kezdőidő.Value = new DateTime(1900, 1, 1, 0, 0, 0);
            Végeidő.Value = new DateTime(1900, 1, 1, 0, 0, 0);
            Túlóraoka.Text = "";
            Telephely.Text = "";
            Tábla_BeoKód.ClearSelection();
        }

        private void Tábla_BeoKód_Click(object sender, EventArgs e)
        {
            try
            {
                if (Beosztáskód.Text.Trim() == "") throw new HibásBevittAdat("A beosztáskódot ki kell tölteni.");
                if (Telephely.Text.Trim() == "") throw new HibásBevittAdat("A telephely mező értékenek tartalmazni kell valamit.");
                if (!int.TryParse(Túlóra.Text, out int TúlÓra)) Túlóra.Text = "0";
                if (Túlóraoka.Text.Trim() == "") Túlóraoka.Text = "_";
                AdatokBeoSegéd = KézBeoSegéd.Lista_Adatok();
                Adat_Kiegészítő_Beosegéd Elem = (from a in AdatokBeoSegéd
                                                 where a.Beosztáskód == Beosztáskód.Text.Trim()
                                                 && a.Telephely == Telephely.Text.Trim()
                                                 select a).FirstOrDefault();

                Adat_Kiegészítő_Beosegéd ADAT = new Adat_Kiegészítő_Beosegéd(Beosztáskód.Text.Trim(),
                                                                             TúlÓra,
                                                                             Kezdőidő.Value,
                                                                             Végeidő.Value,
                                                                             Túlóraoka.Text.Trim(),
                                                                             Telephely.Text.Trim());
                if (Elem != null)
                    KézBeoSegéd.Módosítás(ADAT);
                else
                    KézBeoSegéd.Rögzítés(ADAT);

                Tábla_BeoKód_kiirás();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tábla_BeoKód_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Beosztáskód.Text.Trim() == "") throw new HibásBevittAdat("A beosztáskód mezőt ki kell tölteni.");
                if (Telephely.Text.Trim() == "") throw new HibásBevittAdat("A telephely mező nem lehet üres.");

                AdatokBeoSegéd = KézBeoSegéd.Lista_Adatok();
                Adat_Kiegészítő_Beosegéd Elem = (from a in AdatokBeoSegéd
                                                 where a.Beosztáskód == Beosztáskód.Text.Trim()
                                                 && a.Telephely == Telephely.Text.Trim()
                                                 select a).FirstOrDefault();

                if (Elem != null)
                {
                    KézBeoSegéd.Törlés(Beosztáskód.Text.Trim(), Telephely.Text.Trim());
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Tábla_BeoKód_kiirás();
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


        #region Túlóra keret
        private void TúlóraFrissít_Click(object sender, EventArgs e)
        {
            Tábla_Keret_kiirás();
        }

        private void Tábla_Keret_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Tábla_Keret.Rows[e.RowIndex].Selected = true;
        }

        private void Tábla_Keret_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Tábla_Keret.Rows[e.RowIndex].Selected = true;
        }

        private void Tábla_Keret_SelectionChanged(object sender, EventArgs e)
        {
            if (Tábla_Keret.SelectedRows.Count != 0)
            {
                Túlhatár.Text = Tábla_Keret.Rows[Tábla_Keret.SelectedRows[0].Index].Cells[0].Value.ToString();
                Túltelephely.Text = Tábla_Keret.Rows[Tábla_Keret.SelectedRows[0].Index].Cells[1].Value.ToString();
                Túlparancs.Text = Tábla_Keret.Rows[Tábla_Keret.SelectedRows[0].Index].Cells[2].Value.ToString();
            }
        }

        private void Tábla_Keret_kiirás()
        {
            try
            {
                AdatokTúlórakeret = KézTúlórakeret.Lista_Adatok();

                Tábla_Keret.Rows.Clear();
                Tábla_Keret.Columns.Clear();
                Tábla_Keret.Refresh();
                Tábla_Keret.Visible = false;
                Tábla_Keret.ColumnCount = 3;

                // fejléc elkészítése
                Tábla_Keret.Columns[0].HeaderText = "Határóra";
                Tábla_Keret.Columns[0].Width = 100;
                Tábla_Keret.Columns[1].HeaderText = "Telephely";
                Tábla_Keret.Columns[1].Width = 200;
                Tábla_Keret.Columns[2].HeaderText = "Következmény";
                Tábla_Keret.Columns[2].Width = 200;

                foreach (Adat_Kiegészítő_Túlórakeret rekord in AdatokTúlórakeret)
                {
                    Tábla_Keret.RowCount++;
                    int i = Tábla_Keret.RowCount - 1;
                    Tábla_Keret.Rows[i].Cells[0].Value = rekord.Határ;
                    Tábla_Keret.Rows[i].Cells[1].Value = rekord.Telephely;
                    Tábla_Keret.Rows[i].Cells[2].Value = rekord.Parancs;
                }
                Tábla_Keret.Visible = true;
                Tábla_Keret.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TÚLTelephelyfeltöltés()
        {
            try
            {
                Túltelephely.Items.Clear();
                List<Adat_Kiegészítő_Könyvtár> Adatok = KézKiegKönyvtár.Lista_Adatok();
                Túltelephely.Items.Add("_");
                foreach (Adat_Kiegészítő_Könyvtár rekord in Adatok)
                    Túltelephely.Items.Add(rekord.Név);
                Túltelephely.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tábla_Keret_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (Túlhatár.Text.Trim() == "") throw new HibásBevittAdat("A túlóra határt ki kell tölteni.");
                if (!int.TryParse(Túlhatár.Text, out int Határ)) throw new HibásBevittAdat("A túlóra határ mezőnek egész számnak kell lennie.");
                if (!int.TryParse(Túlparancs.Text, out int Parancs)) throw new HibásBevittAdat("Követelmény mezőnek egész számnak kell lennie.");
                if (Túltelephely.Text.Trim() == "") throw new HibásBevittAdat("A telephely mezőt ki kell tölteni.");

                Adat_Kiegészítő_Túlórakeret Elem = (from a in AdatokTúlórakeret
                                                    where a.Telephely == Túltelephely.Text.Trim()
                                                    && a.Határ == Határ
                                                    select a).FirstOrDefault();

                Adat_Kiegészítő_Túlórakeret ADAT = new Adat_Kiegészítő_Túlórakeret(Határ,
                                                                                   Parancs,
                                                                                   Túltelephely.Text.Trim());

                if (Elem != null)
                    KézTúlórakeret.Módosítás(ADAT);
                else
                    KézTúlórakeret.Rögzítés(ADAT);

                Tábla_Keret_kiirás();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tábla_Keret_Új_Click(object sender, EventArgs e)
        {
            Túlhatár.Text = "";
            Túltelephely.Text = "";
            Túlparancs.Text = "";
        }

        private void Tábla_Keret_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Túlhatár.Text, out int Határ)) throw new HibásBevittAdat("Határnak egész számnak kell lennie.");
                if (Túltelephely.Text.Trim() == "") throw new HibásBevittAdat("Telephely mezőt ki kell tölteni.");

                Adat_Kiegészítő_Túlórakeret Elem = (from a in AdatokTúlórakeret
                                                    where a.Telephely == Túltelephely.Text.Trim()
                                                    && a.Határ == Határ
                                                    select a).FirstOrDefault();
                if (Elem != null)
                {
                    Adat_Kiegészítő_Túlórakeret ADAT = new Adat_Kiegészítő_Túlórakeret(
                                                                   Határ,
                                                                   0,
                                                                   Túltelephely.Text.Trim());
                    KézTúlórakeret.Törlés(ADAT);
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Tábla_Keret_kiirás();
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


        #region Éves összesítő
        private void Éves_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Éves_Tábla.Rows[e.RowIndex].Selected = true;
        }

        private void Éves_Tábla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Éves_Tábla.Rows[e.RowIndex].Selected = true;
        }

        private void Éves_Tábla_SelectionChanged(object sender, EventArgs e)
        {
            if (Éves_Tábla.SelectedRows.Count != 0)
            {
                ÉvesÉv.Text = Éves_Tábla.Rows[Éves_Tábla.SelectedRows[0].Index].Cells[0].Value.ToString();
                ÉvesFélév.Text = Éves_Tábla.Rows[Éves_Tábla.SelectedRows[0].Index].Cells[1].Value.ToString();
                ÉvesCsoport.Text = Éves_Tábla.Rows[Éves_Tábla.SelectedRows[0].Index].Cells[2].Value.ToString();
                ÉvesZKnap.Text = Éves_Tábla.Rows[Éves_Tábla.SelectedRows[0].Index].Cells[3].Value.ToString();
                ÉvesEPnap.Text = Éves_Tábla.Rows[Éves_Tábla.SelectedRows[0].Index].Cells[4].Value.ToString();
                ÉvesTperc.Text = Éves_Tábla.Rows[Éves_Tábla.SelectedRows[0].Index].Cells[5].Value.ToString();
                ÉvesTelephely.Text = Éves_Tábla.Rows[Éves_Tábla.SelectedRows[0].Index].Cells[6].Value.ToString();
            }
        }

        private void Éves_Tábla_kiirás()
        {
            try
            {
                if (!int.TryParse(ÉvesÉv.Text, out int Év)) return;
                AdatokVáltóstábla = KézVáltóstábla.Lista_Adatok(Év);

                Éves_Tábla.Rows.Clear();
                Éves_Tábla.Columns.Clear();
                Éves_Tábla.Refresh();
                Éves_Tábla.Visible = false;
                Éves_Tábla.ColumnCount = 7;

                // fejléc elkészítése
                Éves_Tábla.Columns[0].HeaderText = "Év";
                Éves_Tábla.Columns[0].Width = 100;
                Éves_Tábla.Columns[1].HeaderText = "Félév";
                Éves_Tábla.Columns[1].Width = 100;
                Éves_Tábla.Columns[2].HeaderText = "Csoport";
                Éves_Tábla.Columns[2].Width = 100;
                Éves_Tábla.Columns[3].HeaderText = "Kiadott pihenőnap";
                Éves_Tábla.Columns[3].Width = 100;
                Éves_Tábla.Columns[4].HeaderText = "Elvont pihenőnap";
                Éves_Tábla.Columns[4].Width = 100;
                Éves_Tábla.Columns[5].HeaderText = "Túlóra perc";
                Éves_Tábla.Columns[5].Width = 100;
                Éves_Tábla.Columns[6].HeaderText = "Telephely";
                Éves_Tábla.Columns[6].Width = 100;

                foreach (Adat_Váltós_Váltóstábla rekord in AdatokVáltóstábla)
                {
                    Éves_Tábla.RowCount++;
                    int i = Éves_Tábla.RowCount - 1;

                    Éves_Tábla.Rows[i].Cells[0].Value = rekord.Év;
                    Éves_Tábla.Rows[i].Cells[1].Value = rekord.Félév;
                    Éves_Tábla.Rows[i].Cells[2].Value = rekord.Csoport;
                    Éves_Tábla.Rows[i].Cells[3].Value = rekord.Zknap;
                    Éves_Tábla.Rows[i].Cells[4].Value = rekord.Epnap;
                    Éves_Tábla.Rows[i].Cells[5].Value = rekord.Tperc;
                    Éves_Tábla.Rows[i].Cells[6].Value = rekord.Telephely;
                }

                Éves_Tábla.Visible = true;
                Éves_Tábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Éves_Új_Click(object sender, EventArgs e)
        {
            ÉvesÉv.Text = "";
            ÉvesFélév.Text = "";
            ÉvesCsoport.Text = "";
            ÉvesZKnap.Text = "";
            ÉvesEPnap.Text = "";
            ÉvesTperc.Text = "";
            ÉvesTelephely.Text = "";
            Éves_Tábla.ClearSelection();
        }

        private void ÉvesTelephelyfeltöltés()
        {
            ÉvesTelephely.Items.Clear();
            List<Adat_Kiegészítő_Könyvtár> Adatok = KézKiegKönyvtár.Lista_Adatok();
            ÉvesTelephely.Items.Add("_");
            foreach (Adat_Kiegészítő_Könyvtár rekord in Adatok)
                ÉvesTelephely.Items.Add(rekord.Név);
            ÉvesTelephely.Refresh();
            ÉvesTelephely.Items.Clear();
        }

        private void ÉvesCsoportfeltöltés()
        {
            try
            {
                ÉvesCsoport.Items.Clear();

                List<Adat_Kiegészítő_Turnusok> Adatok = KézTurnusok.Lista_Adatok();
                foreach (Adat_Kiegészítő_Turnusok rekord in Adatok)
                    ÉvesCsoport.Items.Add(rekord.Csoport);

                ÉvesCsoport.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Éves_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (ÉvesCsoport.Text.Trim() == "") throw new HibásBevittAdat("A csoport mezőnek tartalmaznia kell adatot.");
                if (!int.TryParse(ÉvesÉv.Text, out int Év)) throw new HibásBevittAdat("Az Évnek egész számnak kell lennie.");
                if (!int.TryParse(ÉvesFélév.Text, out int Félév)) throw new HibásBevittAdat("Az Félévnek egész számnak kell lennie.");
                if (!int.TryParse(ÉvesZKnap.Text, out int ZKnap)) throw new HibásBevittAdat("A kiadott szabadnapnak egész számnak kell lennie.");
                if (!int.TryParse(ÉvesEPnap.Text, out int EPnap)) throw new HibásBevittAdat("Az elvontnapnak egész számnak kell lennie.");
                if (!int.TryParse(ÉvesTperc.Text, out int Tperc)) throw new HibásBevittAdat("A túlóra percnek egész számnak kell lennie.");
                if (ÉvesTelephely.Text.Trim() == "") throw new HibásBevittAdat("A telephely mezőt ki kell tölteni.");

                AdatokVáltóstábla = KézVáltóstábla.Lista_Adatok(Év);

                Adat_Váltós_Váltóstábla Elem = (from a in AdatokVáltóstábla
                                                where a.Év == Év && a.Félév == Félév
                                                && a.Csoport == ÉvesCsoport.Text.Trim()
                                                && a.Telephely == ÉvesTelephely.Text.Trim()
                                                select a).FirstOrDefault();

                Adat_Váltós_Váltóstábla ADAT = new Adat_Váltós_Váltóstábla(ÉvesTelephely.Text.Trim(),
                                                                           ÉvesCsoport.Text.Trim(),
                                                                           Év,
                                                                           Félév,
                                                                           ZKnap,
                                                                           EPnap,
                                                                           Tperc);
                if (Elem != null)
                    KézVáltóstábla.Módosítás(Év, ADAT);
                else
                    KézVáltóstábla.Rögzítés(Év, ADAT);


                Éves_Tábla_kiirás();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Éves_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (ÉvesCsoport.Text.Trim() == "") throw new HibásBevittAdat("A csoport mezőnek tartalmaznia kell adatot.");
                if (!int.TryParse(ÉvesÉv.Text, out int Év)) throw new HibásBevittAdat("Az Évnek egész számnak kell lennie.");
                if (!int.TryParse(ÉvesFélév.Text, out int Félév)) throw new HibásBevittAdat("Az Félévnek egész számnak kell lennie.");
                if (ÉvesTelephely.Text.Trim() == "") throw new HibásBevittAdat("A telephely mezőt ki kell tölteni.");

                Adat_Váltós_Váltóstábla Elem = (from a in AdatokVáltóstábla
                                                where a.Év == Év && a.Félév == Félév
                                                && a.Csoport == ÉvesCsoport.Text.Trim()
                                                && a.Telephely == ÉvesTelephely.Text.Trim()
                                                select a).FirstOrDefault();

                if (Elem != null)
                {
                    Adat_Váltós_Váltóstábla ADAT = new Adat_Váltós_Váltóstábla(ÉvesTelephely.Text.Trim(),
                                                           ÉvesCsoport.Text.Trim(),
                                                           Év,
                                                           Félév,
                                                           0,
                                                           0,
                                                           0);
                    KézVáltóstábla.Törlés(Év, ADAT);
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                Éves_Tábla_kiirás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Éves_Frissít_Click(object sender, EventArgs e)
        {
            Éves_Tábla_kiirás();
        }

        private void Éves_Generál_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(ÉvesÉv.Text, out int Év)) throw new HibásBevittAdat("Az Évnek egész számnak kell lennie.");

                if (ÉvesTelephely.Text.Trim() == "") ÉvesTelephely.Text = "_";

                for (int l = 1; l <= ÉvesCsoport.Items.Count; l++)
                {
                    ÉvesCsoport.SelectedIndex = l - 1;
                    int ZKnap = 0;
                    int EPnap = 0;
                    int Tperc = 0;
                    int Félév = 1;

                    List<Adat_Váltós_Naptár> AdatokVNaptár = KézVNaptár.Lista_Adatok(Év, l.ToString());
                    AdatokVNaptár = AdatokVNaptár.Where(a => a.Nap == "E" || a.Nap == "Z").ToList();

                    VáltósNaptár.Value = new DateTime(Év, 7, 1);
                    VváltósCsoport.SelectedIndex = l - 1;
                    ÉvesCsoport.SelectedIndex = l - 1;
                    ÉvesFélév.Text = "1";
                    Váltós_Tábla_listázása();

                    Tperc = TextElső;

                    //Elsőfélév
                    EPnap = (from a in AdatokVNaptár
                             where a.Nap == "E" && a.Dátum < new DateTime(Év, 7, 1)
                             select a).Count();
                    ZKnap = (from a in AdatokVNaptár
                             where a.Nap == "Z" && a.Dátum < new DateTime(Év, 7, 1)
                             select a).Count();
                    Adat_Váltós_Váltóstábla FélÉvAdat = new Adat_Váltós_Váltóstábla(ÉvesTelephely.Text.Trim(),
                                                                                    ÉvesCsoport.Text.Trim(),
                                                                                    Év,
                                                                                    Félév,
                                                                                    ZKnap,
                                                                                    EPnap,
                                                                                    Tperc);
                    Rögzít_VáltósTábla(FélÉvAdat);

                    //Második félév
                    ZKnap = 0;
                    EPnap = 0;
                    Félév = 2;
                    Tperc = TextMásodik;
                    EPnap = (from a in AdatokVNaptár
                             where a.Nap == "E" && a.Dátum >= new DateTime(Év, 7, 1)
                             select a).Count();
                    ZKnap = (from a in AdatokVNaptár
                             where a.Nap == "Z" && a.Dátum >= new DateTime(Év, 7, 1)
                             select a).Count();
                    FélÉvAdat = new Adat_Váltós_Váltóstábla(ÉvesTelephely.Text.Trim(),
                                                            ÉvesCsoport.Text.Trim(),
                                                            Év,
                                                            Félév,
                                                            ZKnap,
                                                            EPnap,
                                                            Tperc);
                    Rögzít_VáltósTábla(FélÉvAdat);
                }

                Éves_Tábla_kiirás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Rögzít_VáltósTábla(Adat_Váltós_Váltóstábla Adat)
        {
            try
            {
                if (!int.TryParse(ÉvesÉv.Text, out int Év)) throw new HibásBevittAdat("Az Évnek egész számnak kell lennie.");
                List<Adat_Váltós_Váltóstábla> Adatok = KézVáltóstábla.Lista_Adatok(Év);

                Adat_Váltós_Váltóstábla Elem = (from a in Adatok
                                                where a.Év == Adat.Év && a.Félév == Adat.Félév
                                                && a.Csoport == Adat.Csoport
                                                && a.Telephely == Adat.Telephely
                                                select a).FirstOrDefault();
                Adat_Váltós_Váltóstábla ADAT = new Adat_Váltós_Váltóstábla(Adat.Telephely,
                                                                           Adat.Csoport,
                                                                           Adat.Év,
                                                                           Adat.Félév,
                                                                           Adat.Zknap,
                                                                           Adat.Epnap,
                                                                           Adat.Tperc);

                if (Elem != null)
                    KézVáltóstábla.Módosítás(Év, ADAT);
                else
                    KézVáltóstábla.Rögzítés(Év, ADAT);
            }
            catch (HibásBevittAdat ex)
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


        #region Turnuskok kész
        private void TurnusokFormListafeltöltés()
        {
            try
            {
                AdatokTurnusok = KézTurnusok.Lista_Adatok();
                CsoportCombo.Items.Clear();
                TurnusokLista.Items.Clear();

                foreach (Adat_Kiegészítő_Turnusok item in AdatokTurnusok)
                {
                    TurnusokLista.Items.Add(item.Csoport);
                    CsoportCombo.Items.Add(item.Csoport);
                }
                TurnusokLista.Refresh();
                CsoportCombo.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Turnus_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (TurnusText.Text.Trim() == "") throw new HibásBevittAdat("A csoport elnevezésnek tartalmaznia kell adatot.");

                AdatokTurnusok = KézTurnusok.Lista_Adatok();

                Adat_Kiegészítő_Turnusok Elem = (from a in AdatokTurnusok
                                                 where a.Csoport == TurnusText.Text.Trim()
                                                 select a).FirstOrDefault();
                Adat_Kiegészítő_Turnusok ADAT = new Adat_Kiegészítő_Turnusok(TurnusText.Text.Trim());

                if (Elem == null)
                {
                    KézTurnusok.Rögzítés(ADAT);
                }
                TurnusText.Text = "";
                TurnusokFormListafeltöltés();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Turnus_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (TurnusText.Text.Trim() == "") throw new HibásBevittAdat("A csoport elnevezésnek tartalmaznia kell adatot.");

                AdatokTurnusok = KézTurnusok.Lista_Adatok();
                Adat_Kiegészítő_Turnusok Elem = (from a in AdatokTurnusok
                                                 where a.Csoport == TurnusText.Text.Trim()
                                                 select a).FirstOrDefault();

                if (Elem != null)
                {
                    Adat_Kiegészítő_Turnusok ADAT = new Adat_Kiegészítő_Turnusok(TurnusText.Text.Trim());
                    KézTurnusok.Törlés(ADAT);
                }
                TurnusText.Text = "";
                TurnusokFormListafeltöltés();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TurnusokLista_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TurnusokLista.SelectedItems.Count < 1) return;

            TurnusText.Text = TurnusokLista.Items[TurnusokLista.SelectedIndex].ToString();
        }
        #endregion


        #region Csoportelnevezés
        private void Csoport_Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Csoport_Tábla.Rows[e.RowIndex].Selected = true;
        }

        private void Csoport_Tábla_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Csoport_Tábla.Rows[e.RowIndex].Selected = true;
        }

        private void Csoport_Tábla_SelectionChanged(object sender, EventArgs e)
        {
            if (Csoport_Tábla.SelectedRows.Count != 0)
            {
                Id.Text = Csoport_Tábla.Rows[Csoport_Tábla.SelectedRows[0].Index].Cells[0].Value.ToString();
                Kezdődátum.Value = Csoport_Tábla.Rows[Csoport_Tábla.SelectedRows[0].Index].Cells[1].Value.ToÉrt_DaTeTime();
                Ciklusnap.Text = Csoport_Tábla.Rows[Csoport_Tábla.SelectedRows[0].Index].Cells[2].Value.ToString();
                MegnevezésText.Text = Csoport_Tábla.Rows[Csoport_Tábla.SelectedRows[0].Index].Cells[3].Value.ToString();
                CsoportCombo.Text = Csoport_Tábla.Rows[Csoport_Tábla.SelectedRows[0].Index].Cells[4].Value.ToString();
            }
        }

        private void Csoport_Tábla_kiirás()
        {
            try
            {
                AdatokKiegVáltóstábla = KézKiegVáltóstábla.Lista_Adatok();

                Csoport_Tábla.Rows.Clear();
                Csoport_Tábla.Columns.Clear();
                Csoport_Tábla.Refresh();
                Csoport_Tábla.Visible = false;
                Csoport_Tábla.ColumnCount = 5;

                // fejléc elkészítése
                Csoport_Tábla.Columns[0].HeaderText = "ID";
                Csoport_Tábla.Columns[0].Width = 150;
                Csoport_Tábla.Columns[1].HeaderText = "Kezdő dátum";
                Csoport_Tábla.Columns[1].Width = 150;
                Csoport_Tábla.Columns[2].HeaderText = "Ciklusnap";
                Csoport_Tábla.Columns[2].Width = 150;
                Csoport_Tábla.Columns[3].HeaderText = "Megnevezés";
                Csoport_Tábla.Columns[3].Width = 150;
                Csoport_Tábla.Columns[4].HeaderText = "Csoport";
                Csoport_Tábla.Columns[4].Width = 150;

                foreach (Adat_Kiegészítő_Váltóstábla rekord in AdatokKiegVáltóstábla)
                {
                    Csoport_Tábla.RowCount++;
                    int i = Csoport_Tábla.RowCount - 1;
                    Csoport_Tábla.Rows[i].Cells[0].Value = rekord.Id;
                    Csoport_Tábla.Rows[i].Cells[1].Value = rekord.Kezdődátum.ToString("yyyy.MM.dd");
                    Csoport_Tábla.Rows[i].Cells[2].Value = rekord.Ciklusnap;
                    Csoport_Tábla.Rows[i].Cells[3].Value = rekord.Megnevezés;
                    Csoport_Tábla.Rows[i].Cells[4].Value = rekord.Csoport;
                }

                Csoport_Tábla.Visible = true;
                Csoport_Tábla.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Csoport_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Id.Text, out int Id_)) throw new HibásBevittAdat("Id mezőnek számnak kell lennie.");
                if (MegnevezésText.Text.Trim() == "") throw new HibásBevittAdat("A megnevezés mezőnek tartalmaznia kell adatot.");
                if (CsoportCombo.Text.Trim() == "") throw new HibásBevittAdat("A csoport elnevezésnek tartalmaznia kell adatot.");
                if (!int.TryParse(Ciklusnap.Text, out int Napciklus)) throw new HibásBevittAdat("A ciklusnapnak egész számnak kell lennie.");

                Adat_Kiegészítő_Váltóstábla Elem = (from a in AdatokKiegVáltóstábla
                                                    where a.Id == Id_
                                                    select a).FirstOrDefault();
                Adat_Kiegészítő_Váltóstábla ADAT = new Adat_Kiegészítő_Váltóstábla(Id_,
                                                                                   Kezdőidő.Value,
                                                                                   Napciklus,
                                                                                   MegnevezésText.Text.Trim(),
                                                                                   CsoportCombo.Text.Trim());

                if (Elem != null)
                    KézKiegVáltóstábla.Módosítás(ADAT);
                else
                    KézKiegVáltóstábla.Rögzítés(ADAT);

                Csoport_Tábla_kiirás();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Csoport_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(Id.Text, out int Id_)) throw new HibásBevittAdat("Id mezőnek számnak kell lennie.");
                Adat_Kiegészítő_Váltóstábla Elem = (from a in AdatokKiegVáltóstábla
                                                    where a.Id == Id_
                                                    select a).FirstOrDefault();
                if (Elem != null)
                    KézKiegVáltóstábla.Törlés(Id_);

                Id.Text = "";
                Csoport_Tábla_kiirás();
            }
            catch (HibásBevittAdat ex)
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
        private void Tábla_Munkarend_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Tábla_Munkarend.Rows[e.RowIndex].Selected = true;
        }

        private void Tábla_Munkarend_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Tábla_Munkarend.Rows[e.RowIndex].Selected = true;
        }

        private void Tábla_Munkarend_SelectionChanged(object sender, EventArgs e)
        {
            if (Tábla_Munkarend.SelectedRows.Count != 0)
            {
                Munkarendelnevezés.Text = Tábla_Munkarend.Rows[Tábla_Munkarend.SelectedRows[0].Index].Cells[0].Value.ToString();
                Munkaidő.Text = Tábla_Munkarend.Rows[Tábla_Munkarend.SelectedRows[0].Index].Cells[1].Value.ToString();
            }
        }

        private void Tábla_Munkarend_kiirás()
        {
            try
            {
                AdatokMunkaidő = KézMunkaidő.Lista_Adatok();

                Tábla_Munkarend.Rows.Clear();
                Tábla_Munkarend.Columns.Clear();
                Tábla_Munkarend.Refresh();
                Tábla_Munkarend.Visible = false;
                Tábla_Munkarend.ColumnCount = 2;
                // fejléc elkészítése
                Tábla_Munkarend.Columns[0].HeaderText = "munkarend";
                Tábla_Munkarend.Columns[0].Width = 100;
                Tábla_Munkarend.Columns[1].HeaderText = "munkaidő";
                Tábla_Munkarend.Columns[1].Width = 80;

                foreach (Adat_Kiegészítő_Munkaidő rekord in AdatokMunkaidő)
                {
                    Tábla_Munkarend.RowCount++;
                    int i = Tábla_Munkarend.RowCount - 1;

                    Tábla_Munkarend.Rows[i].Cells[0].Value = rekord.Munkarendelnevezés;
                    Tábla_Munkarend.Rows[i].Cells[1].Value = rekord.Munkaidő;
                    if (rekord.Munkarendelnevezés == "12")
                        Tizenkétóra = rekord.Munkaidő;
                }

                Tábla_Munkarend.Visible = true;
                Tábla_Munkarend.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MunkaRend_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (Munkarendelnevezés.Text.Trim() == "") throw new HibásBevittAdat("Munkarend elnevezése mezőnek tartalmaznia kell valamit.");
                if (!int.TryParse(Munkaidő.Text, out int IdőMunkna)) throw new HibásBevittAdat("A munkaidőnek egész számnak kell lennie.");

                AdatokMunkaidő = KézMunkaidő.Lista_Adatok();
                Adat_Kiegészítő_Munkaidő Elem = (from a in AdatokMunkaidő
                                                 where a.Munkarendelnevezés == Munkarendelnevezés.Text.Trim()
                                                 select a).FirstOrDefault();
                Adat_Kiegészítő_Munkaidő ADAT = new Adat_Kiegészítő_Munkaidő(Munkarendelnevezés.Text.Trim(),
                                                                             IdőMunkna);

                if (Elem != null)
                    KézMunkaidő.Módosítás(ADAT);
                else
                    KézMunkaidő.Rögzítés(ADAT);

                Tábla_Munkarend_kiirás();
                Munkarendelnevezés.Text = "";
                Munkaidő.Text = "";
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MunkaRend_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Munkarendelnevezés.Text.Trim() == "") throw new HibásBevittAdat("Munkarend elnevezése mezőnek tartalmaznia kell valamit.");
                if (!int.TryParse(Munkaidő.Text, out int IdőMunkna)) throw new HibásBevittAdat("A munkaidőnek egész számnak kell lennie.");
                Adat_Kiegészítő_Munkaidő Elem = (from a in AdatokMunkaidő
                                                 where a.Munkarendelnevezés == Munkarendelnevezés.Text.Trim()
                                                 select a).FirstOrDefault();
                if (Elem != null) KézMunkaidő.Törlés(Munkarendelnevezés.Text.Trim());
                Munkarendelnevezés.Text = "";
                Munkaidő.Text = "";
                Tábla_Munkarend_kiirás();
            }
            catch (HibásBevittAdat ex)
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


        #region Munkaidő naptár
        private void Nappal_Alap_Click(object sender, EventArgs e)
        {
            Alapnaptár();
        }

        private void Alapnaptár()
        {
            try
            {
                DateTime hónaputolsónapja;
                DateTime próbanap;

                Tábla_Nappalos.Rows.Clear();
                Tábla_Nappalos.Columns.Clear();
                Tábla_Nappalos.Refresh();
                Tábla_Nappalos.Visible = false;
                Tábla_Nappalos.ColumnCount = 36;
                Tábla_Nappalos.RowCount = 15;

                // fejléc elkészítése
                Tábla_Nappalos.Columns[0].HeaderText = "Hónap";
                Tábla_Nappalos.Columns[0].Width = 100;
                Tábla_Nappalos.Columns[1].HeaderText = "Munkanapok";
                Tábla_Nappalos.Columns[1].Width = 105;
                Tábla_Nappalos.Columns[2].HeaderText = "idő [perc]";
                Tábla_Nappalos.Columns[2].Width = 100;
                Tábla_Nappalos.Columns[3].HeaderText = "idő [óra]";
                Tábla_Nappalos.Columns[3].Width = 100;
                Tábla_Nappalos.Columns[4].HeaderText = "Pihenőnapok száma";
                Tábla_Nappalos.Columns[4].Width = 110;

                for (int i = 1; i <= 31; i++)
                {
                    Tábla_Nappalos.Columns[i + 4].HeaderText = i.ToString();
                    Tábla_Nappalos.Columns[i + 4].Width = 27;

                }
                // színezi az első és a második félévet
                for (int j = 1; j <= 12; j++)
                {
                    for (int i = 1; i <= 31; i++)
                    {

                        if (j > 6)
                        {
                            Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.SandyBrown;
                        }

                        else
                        {
                            Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.PeachPuff;
                        }
                    }
                }
                // tartalom feltöltés
                for (int j = 1; j <= 12; j++)
                {
                    Tábla_Nappalos.Rows[j - 1].Cells[0].Value = j;

                    DateTime képzettDátum = new DateTime(Dátumnappal.Value.Year, j, 1);
                    hónaputolsónapja = MyF.Hónap_utolsónapja(képzettDátum);


                    for (int i = 1; i <= 31; i++)
                    {
                        if (hónaputolsónapja.Day < i)
                        {
                            Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Value = "X";
                            Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.DimGray;
                        }
                        else
                        {
                            próbanap = new DateTime(Dátumnappal.Value.Year, j, i);
                            switch (próbanap.DayOfWeek)
                            {
                                case DayOfWeek.Saturday:
                                    {
                                        Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Value = "P";
                                        Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.MediumSeaGreen;
                                        break;
                                    }
                                case DayOfWeek.Sunday:
                                    {
                                        Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Value = "V";
                                        Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.OrangeRed;
                                        break;
                                    }

                                default:
                                    {
                                        Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Value = "1";
                                        break;
                                    }

                            }
                        }
                    }
                }

                Tábla_Nappalos.Refresh();
                Tábla_Nappalos.Visible = true;

                Alapszámolás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Munkarend8és12kiirás()
        {
            try
            {
                List<Adat_Kiegészítő_Munkaidő> Adatok = KézMunkaidő.Lista_Adatok();

                Munkarend12 = (int)(from a in Adatok
                                    where a.Munkarendelnevezés == "12"
                                    select a.Munkaidő).FirstOrDefault();
                Munkarend8 = (int)(from a in Adatok
                                   where a.Munkarendelnevezés == "8"
                                   select a.Munkaidő).FirstOrDefault();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tábla_Nappalos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (Tábla_Nappalos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToStrTrim() == "X")
                {
                    Nappaloslenyíló.Visible = false;
                    return;
                }
                if (e.RowIndex < 12 & e.ColumnIndex > 4)
                {
                    oszlop = e.ColumnIndex;
                    sor = e.RowIndex;
                    Választott.Text = Tábla_Nappalos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToStrTrim();
                }
                else
                {
                    Nappaloslenyíló.Visible = false;
                    return;
                }

                // érvényes sorokat engedünk kiválasztani
                if (e.RowIndex > 12) return;
                // érvényes oszlopokat engedünk kiválasztani
                if (e.ColumnIndex <= 4) return;
                Nappaloslenyíló.Visible = true;

                if (Width > 460 + 27 * (e.ColumnIndex - 3) - ScrollX + 150)
                {
                    Nappaloslenyíló.Left = 460 + 27 * (e.ColumnIndex - 3) - ScrollX;
                }
                else
                {
                    Nappaloslenyíló.Left = 460 + 27 * (e.ColumnIndex - 3) - ScrollX - 73;
                }
                Nappaloslenyíló.Top = 105 + 22 * e.RowIndex - ScrollY * 22;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Nappaloslenyíló_SelectedIndexChanged(object sender, EventArgs e)
        {
            Választott.Text = Nappaloslenyíló.Text;
            Nappaloslenyíló.Visible = false;
            Tábla_Nappalos.Rows[sor].Cells[oszlop].Value = Választott.Text.Substring(0, 1);
        }

        private void Nappaloslenyíló_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27) Nappaloslenyíló.Visible = false;

        }

        private void Tábla_Nappalos_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == 0)
                ScrollX = e.NewValue;
            else
                ScrollY = e.NewValue;
        }

        private void Nappal_Számol_Click(object sender, EventArgs e)
        {
            Alapszámolás();
        }

        private void Alapszámolás()
        {
            try
            {
                if (Tábla_Nappalos.Rows.Count < 1) return;

                int napokszáma;
                int pihenőnapokszáma;
                {
                    Tábla_Nappalos.Visible = false;
                    for (int j = 1; j <= 12; j++)
                    {
                        napokszáma = 0;
                        pihenőnapokszáma = 0;

                        for (int i = 1; i <= 31; i++)
                        {

                            if (Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Value != null && Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Value.ToString() == "1")
                                napokszáma += 1;
                            if (Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Value.ToString() == "Ü" ||
                                Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Value.ToString() == "P" ||
                                Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Value.ToString() == "V")
                                pihenőnapokszáma += 1;

                        }
                        Tábla_Nappalos.Rows[j - 1].Cells[1].Value = napokszáma;
                        Tábla_Nappalos.Rows[j - 1].Cells[2].Value = napokszáma * Munkarend8;
                        Tábla_Nappalos.Rows[j - 1].Cells[3].Value = napokszáma * Munkarend8 / 60;
                        Tábla_Nappalos.Columns[3].DefaultCellStyle.Format = "0.00";
                        Tábla_Nappalos.Rows[j - 1].Cells[4].Value = pihenőnapokszáma;
                    }

                    // Összesít félévre
                    napokszáma = 0;
                    pihenőnapokszáma = 0;
                    for (int j = 1; j <= 6; j++)
                    {
                        napokszáma += Tábla_Nappalos.Rows[j - 1].Cells[1].Value.ToÉrt_Int();
                        pihenőnapokszáma += Tábla_Nappalos.Rows[j - 1].Cells[4].Value.ToÉrt_Int();
                    }
                    Tábla_Nappalos.Rows[12].Cells[0].Value = "1 félév";
                    Tábla_Nappalos.Rows[12].Cells[1].Value = napokszáma;
                    Tábla_Nappalos.Rows[12].Cells[2].Value = napokszáma * Munkarend8;
                    Tábla_Nappalos.Rows[12].Cells[3].Value = napokszáma * Munkarend8 / 60;
                    Tábla_Nappalos.Rows[12].Cells[4].Value = pihenőnapokszáma;

                    napokszáma = 0;
                    pihenőnapokszáma = 0;
                    for (int j = 7; j <= 12; j++)
                    {
                        napokszáma += Tábla_Nappalos.Rows[j - 1].Cells[1].Value.ToÉrt_Int();
                        pihenőnapokszáma += Tábla_Nappalos.Rows[j - 1].Cells[4].Value.ToÉrt_Int();
                    }

                    Tábla_Nappalos.Rows[13].Cells[0].Value = "2 félév";
                    Tábla_Nappalos.Rows[13].Cells[1].Value = napokszáma;
                    Tábla_Nappalos.Rows[13].Cells[2].Value = napokszáma * Munkarend8;
                    Tábla_Nappalos.Rows[13].Cells[3].Value = napokszáma * Munkarend8 / 60;
                    Tábla_Nappalos.Rows[13].Cells[4].Value = pihenőnapokszáma;
                    // összesít évre
                    napokszáma = 0;
                    pihenőnapokszáma = 0;
                    for (int j = 1; j <= 12; j++)
                    {
                        napokszáma += Tábla_Nappalos.Rows[j - 1].Cells[1].Value.ToÉrt_Int();
                        pihenőnapokszáma += Tábla_Nappalos.Rows[j - 1].Cells[4].Value.ToÉrt_Int();
                    }
                    Tábla_Nappalos.Rows[14].Cells[0].Value = "Év";
                    Tábla_Nappalos.Rows[14].Cells[1].Value = napokszáma;
                    Tábla_Nappalos.Rows[14].Cells[2].Value = napokszáma * Munkarend8;
                    Tábla_Nappalos.Rows[14].Cells[3].Value = napokszáma * Munkarend8 / 60;
                    Tábla_Nappalos.Rows[14].Cells[4].Value = pihenőnapokszáma;

                    Tábla_Nappalos.Refresh();
                    Tábla_Nappalos.Visible = true;
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

        private void NappaloS_Tábla_Friss_Click(object sender, EventArgs e)
        {
            Rögzített_8();
        }

        private void Rögzített_8()
        {
            try
            {
                DateTime hónaputolsónapja;
                List<Adat_Váltós_Naptár> Adatok = KézVNaptár.Lista_Adatok(Dátumnappal.Value.Year, "");

                Tábla_Nappalos.Rows.Clear();
                Tábla_Nappalos.Columns.Clear();
                Tábla_Nappalos.Refresh();
                Tábla_Nappalos.Visible = false;
                Tábla_Nappalos.ColumnCount = 36;
                Tábla_Nappalos.RowCount = 15;

                // fejléc elkészítése
                Tábla_Nappalos.Columns[0].HeaderText = "Hónap";
                Tábla_Nappalos.Columns[0].Width = 100;
                Tábla_Nappalos.Columns[1].HeaderText = "Munkanapok";
                Tábla_Nappalos.Columns[1].Width = 105;
                Tábla_Nappalos.Columns[2].HeaderText = "idő [perc]";
                Tábla_Nappalos.Columns[2].Width = 100;
                Tábla_Nappalos.Columns[3].HeaderText = "idő [óra]";
                Tábla_Nappalos.Columns[3].Width = 100;
                Tábla_Nappalos.Columns[4].HeaderText = "Pihenőnapok száma";
                Tábla_Nappalos.Columns[4].Width = 110;

                for (int i = 1; i <= 31; i++)
                {
                    Tábla_Nappalos.Columns[i + 4].HeaderText = i.ToString();
                    Tábla_Nappalos.Columns[i + 4].Width = 27;

                }
                // színezi az első és a második félévet
                for (int j = 1; j <= 12; j++)
                {
                    Tábla_Nappalos.Rows[j - 1].Cells[0].Value = j;
                    for (int i = 1; i <= 31; i++)
                    {

                        if (j > 6)
                        {
                            Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.SandyBrown;
                        }

                        else
                        {
                            Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.PeachPuff;
                        }
                    }
                }



                DateTime eleje = new DateTime(Dátumnappal.Value.Year, 1, 1);
                DateTime vége = new DateTime(Dátumnappal.Value.Year, 12, 31);
                DateTime mikor = eleje;
                int sor;
                int oszlop;



                foreach (Adat_Váltós_Naptár rekord in Adatok)
                {
                    mikor = rekord.Dátum;
                    sor = mikor.Month - 1;
                    oszlop = mikor.Day + 4;
                    Tábla_Nappalos.Rows[sor].Cells[oszlop].Value = rekord.Nap;
                }


                // megformázzuk
                for (int j = 1; j <= 12; j++)
                {
                    DateTime ideignap = new DateTime((Dátumnappal.Value).Year, j, 1);
                    hónaputolsónapja = MyF.Hónap_utolsónapja(ideignap);

                    for (int i = 1; i <= 31; i++)
                    {
                        if (hónaputolsónapja.Day < i)
                        {
                            Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Value = "X";
                        }
                    }
                }

                for (int j = 1; j <= 12; j++)
                {
                    for (int i = 1; i <= 31; i++)
                    {
                        switch (Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Value)
                        {
                            case "P":
                                {
                                    Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.MediumSeaGreen;
                                    break;
                                }
                            case "V":
                                {
                                    Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.OrangeRed;
                                    break;
                                }
                            case "Ü":
                                {
                                    Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.Red;
                                    break;
                                }
                            case "X":
                                {
                                    Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.DimGray;
                                    break;
                                }
                        }

                    }
                }
                Tábla_Nappalos.Refresh();
                Tábla_Nappalos.Visible = true;
                Alapszámolás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Nappaloslenyílófeltöltés()
        {
            Nappaloslenyíló.Items.Clear();
            foreach (MyEn.Váltós_Naptár_Státusz elem in Enum.GetValues(typeof(MyEn.Váltós_Naptár_Státusz)))
            {
                string[] darabol = elem.ToString().Split('_');
                Nappaloslenyíló.Items.Add($"{darabol[1]} - {darabol[0]}");
            }
        }

        private void Nappal_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime próbanap;
                string napiérték;
                if (Tábla_Nappalos.Rows.Count < 1) return;
                // munkaidő naptár rögzítése
                List<Adat_Váltós_Naptár> Adatok = KézVNaptár.Lista_Adatok(Dátumnappal.Value.Year, "");

                Holtart.Be(32);
                List<Adat_Váltós_Naptár> AdatokMódosítás = new List<Adat_Váltós_Naptár>();
                List<Adat_Váltós_Naptár> AdatokRögzítés = new List<Adat_Váltós_Naptár>();
                for (int j = 1; j <= 12; j++)
                {
                    for (int i = 1; i <= 31; i++)
                    {
                        Holtart.Lép();
                        napiérték = Tábla_Nappalos.Rows[j - 1].Cells[4 + i].Value.ToStrTrim();
                        if (napiérték != "X")
                        {
                            próbanap = new DateTime((Dátumnappal.Value).Year, j, i);

                            Adat_Váltós_Naptár Elem = (from a in Adatok
                                                       where a.Dátum == próbanap
                                                       select a).FirstOrDefault();
                            Adat_Váltós_Naptár ADAT = new Adat_Váltós_Naptár(napiérték,
                                                                             próbanap);

                            if (Elem != null)
                                AdatokMódosítás.Add(ADAT);
                            else
                                AdatokRögzítés.Add(ADAT);
                        }
                    }
                }
                if (AdatokMódosítás.Count > 0) KézVNaptár.Módosítás(Dátumnappal.Value.Year, "", AdatokMódosítás);
                if (AdatokRögzítés.Count > 0) KézVNaptár.Rögzítés(Dátumnappal.Value.Year, "", AdatokRögzítés);


                Rögzített_8();
                List<Adat_Váltós_Összesítő> AdatokÖsszes = KézVáltÖsszesítő.Lista_Adatok(Dátumnappal.Value.Year, "");
                // Összesített adatok rögzítése
                List<Adat_Váltós_Összesítő> AdatokMódosításÖ = new List<Adat_Váltós_Összesítő>();
                List<Adat_Váltós_Összesítő> AdatokRögzítésÖ = new List<Adat_Váltós_Összesítő>();
                for (int i = 1; i <= 12; i++)
                {
                    Holtart.Lép();
                    próbanap = new DateTime((Dátumnappal.Value).Year, i, i);
                    Adat_Váltós_Összesítő Összes = (from a in AdatokÖsszes
                                                    where a.Dátum == próbanap
                                                    select a).FirstOrDefault();
                    Adat_Váltós_Összesítő ADAT = new Adat_Váltós_Összesítő(Tábla_Nappalos.Rows[i - 1].Cells[2].Value.ToÉrt_Int(),
                                                                            próbanap);


                    if (Összes != null)
                        AdatokMódosításÖ.Add(ADAT);
                    else
                        AdatokRögzítésÖ.Add(ADAT);

                    if (AdatokMódosításÖ.Count > 0) KézVáltÖsszesítő.Módosítás(Dátumnappal.Value.Year, "", AdatokMódosításÖ);
                    if (AdatokRögzítésÖ.Count > 0) KézVáltÖsszesítő.Rögzítés(Dátumnappal.Value.Year, "", AdatokRögzítésÖ);
                }

                Holtart.Ki();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (Dátumnappal.Value.Year == DateTime.Today.Year)
                {
                    Panel4.BackColor = Color.Red;
                    Nappal_Ok.Enabled = false;
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

        private void Nappalos_Excel_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_Nappalos.Rows.Count <= 0) return;
                string fájlexc;

                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = $"Nappalos_munkarend_{Program.PostásNév}-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                MyE.DataGridViewToExcel(fájlexc, Tábla_Nappalos);
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

        private void Panel4_MouseClick(object sender, MouseEventArgs e)
        {
            if (Chk_CTRL.Checked)
            {
                Panel4.BackColor = Color.Green;
                Nappal_Ok.Enabled = true;
            }
        }

        private void Dátumnappal_ValueChanged(object sender, EventArgs e)
        {
            Tábla_Nappalos.Rows.Clear();
            Tábla_Nappalos.Columns.Clear();
            Tábla_Nappalos.Refresh();
        }
        #endregion


        #region Váltós Naptár Kész
        private void Váltóslenyílófeltöltés()
        {
            VáltósLenyíló.Items.Clear();
            foreach (MyEn.Váltós_Naptár_Státusz_Váltó elem in Enum.GetValues(typeof(MyEn.Váltós_Naptár_Státusz_Váltó)))
            {
                string[] darabol = elem.ToString().Split('_');
                VáltósLenyíló.Items.Add($"{darabol[1]} - {darabol[0]}");
            }
        }

        private void Command36_Click(object sender, EventArgs e)
        {
            Váltósalapnaptár();
        }

        private void Váltósalapnaptár()
        {
            try
            {
                if (VváltósCsoport.Text.Trim() == "") return;

                AdatokKiegVáltóstábla = KézKiegVáltóstábla.Lista_Adatok();
                AdatokBeosztásciklusVáltó = KézBeosztásciklus.Lista_Adatok("beosztásciklus");
                AdatokÉjszakásBeoCiklus = KézBeosztásciklus.Lista_Adatok("éjszakásciklus");

                Adat_Kiegészítő_Váltóstábla Elem = (from a in AdatokKiegVáltóstábla
                                                    where a.Csoport == VváltósCsoport.Text.Trim()
                                                    select a).FirstOrDefault() ?? throw new HibásBevittAdat("Nincs ilyen csoport adat.");

                DateTime vkezdődátum = Elem.Kezdődátum;
                int ciklushossz = Elem.Ciklusnap;

                DateTime hónaputolsónapja;
                DateTime próbanap;
                int napokszáma;
                int vciklusnap;
                string jel;

                Tábla9.Rows.Clear();
                Tábla9.Columns.Clear();
                Tábla9.Refresh();
                Tábla9.Visible = false;
                Tábla9.ColumnCount = 36;
                Tábla9.RowCount = 21;
                // fejléc elkészítése
                Tábla9.Columns[0].HeaderText = "Hónap";
                Tábla9.Columns[0].Width = 145;
                Tábla9.Columns[1].HeaderText = "Mnapok";
                Tábla9.Columns[1].Width = 70;
                Tábla9.Columns[2].HeaderText = "idő [perc]";
                Tábla9.Columns[2].Width = 100;
                Tábla9.Columns[3].HeaderText = "idő [óra]";
                Tábla9.Columns[3].Width = 100;
                Tábla9.Columns[4].HeaderText = "Pihenőnapok száma";
                Tábla9.Columns[4].Width = 110;
                for (int i = 1; i <= 31; i++)
                {
                    Tábla9.Columns[i + 4].HeaderText = i.ToString();
                    Tábla9.Columns[i + 4].Width = 27;
                }
                // színezi az első és a második félévet
                for (int j = 1; j <= 12; j++)
                {
                    for (int i = 1; i <= 31; i++)
                    {
                        if (j > 6)
                        {
                            Tábla9.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.SandyBrown;
                        }
                        else
                        {
                            Tábla9.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.PeachPuff;
                        }
                    }
                }
                for (int j = 13; j <= 21; j++)
                {
                    for (int i = 0; i <= 4; i++)
                        Tábla9.Rows[j - 1].Cells[i].Style.BackColor = Color.Olive;
                }
                Holtart.Be(32);
                // ' tartalom feltöltés
                for (int j = 1; j <= 12; j++)
                {
                    Tábla9.Rows[j - 1].Cells[0].Value = j;
                    DateTime idiegdátum = new DateTime(Dátumnappal.Value.Year, j, 1);
                    hónaputolsónapja = MyF.Hónap_utolsónapja(idiegdátum);

                    for (int i = 1; i <= 31; i++)
                    {
                        Holtart.Lép();
                        if (hónaputolsónapja.Day < i)
                        {
                            Tábla9.Rows[j - 1].Cells[4 + i].Value = "X";
                            Tábla9.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.DimGray;
                        }
                        else
                        {
                            // váltós beosztás szerinti értéket beírja
                            próbanap = new DateTime(VáltósNaptár.Value.Year, j, i);
                            TimeSpan Különbözet = próbanap - vkezdődátum;

                            napokszáma = Különbözet.Days;
                            if (napokszáma % ciklushossz == 0)
                                vciklusnap = 1;
                            else
                                vciklusnap = (napokszáma % ciklushossz) + 1;

                            if (VváltósCsoport.Text.Substring(0, 1) == "6")
                            {
                                Adat_Kiegészítő_Beosztásciklus ElemV = (from a in AdatokBeosztásciklusVáltó
                                                                        where a.Id == vciklusnap
                                                                        select a).FirstOrDefault();
                                jel = ElemV.Beosztáskód;
                            }
                            else
                            {
                                Adat_Kiegészítő_Beosztásciklus ElemÉ = (from a in AdatokÉjszakásBeoCiklus
                                                                        where a.Id == vciklusnap
                                                                        select a).FirstOrDefault();
                                jel = ElemÉ.Beosztáskód;
                            }
                            if (jel != "_") Tábla9.Rows[j - 1].Cells[4 + i].Value = jel;
                        }
                    }
                }
                Tábla9.Refresh();
                Tábla9.Visible = true;
                Holtart.Ki();

                VáltóAlapszámolás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Nappalosszumma()
        {
            try
            {
                EfélévNappal = 0;
                MfélévNappal = 0;
                Egészév = 0;
                Epihenő = 0;
                Mpihenő = 0;
                Évpihenő = 0;

                List<Adat_Váltós_Összesítő> Adatok = KézVáltÖsszesítő.Lista_Adatok(Dátumnappal.Value.Year, "");

                int i = 1;

                foreach (Adat_Váltós_Összesítő rekord in Adatok)
                {
                    if (i <= 6)
                    {
                        EfélévNappal += rekord.Perc;
                    }
                    else
                    {
                        MfélévNappal += rekord.Perc;
                    }

                    i += 1;
                }

                Egészév = EfélévNappal + MfélévNappal;
                DateTime ideignap = new DateTime(Dátumnappal.Value.Year, 7, 1);

                List<Adat_Váltós_Naptár> AdatokNaptár = KézVNaptár.Lista_Adatok(Dátumnappal.Value.Year, "");
                if (AdatokNaptár != null)
                {
                    List<Adat_Váltós_Naptár> EpihenőLista = (from a in AdatokNaptár
                                                             where a.Dátum < ideignap && (a.Nap == "P" || a.Nap == "Ü" || a.Nap == "V")
                                                             select a).ToList();
                    if (EpihenőLista != null)
                        Epihenő = EpihenőLista.Count();
                    List<Adat_Váltós_Naptár> MpihenőLista = (from a in AdatokNaptár
                                                             where a.Dátum >= ideignap && (a.Nap == "P" || a.Nap == "Ü" || a.Nap == "V")
                                                             select a).ToList();
                    if (MpihenőLista != null)
                        Mpihenő = MpihenőLista.Count();
                    Évpihenő = Epihenő + Mpihenő;
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

        private void Tábla9_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (Tábla9.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToStrTrim() == "X")
                {
                    VáltósLenyíló.Visible = false;
                    return;
                }
                if (e.RowIndex < 12 & e.ColumnIndex > 4)
                {
                    OszlopVáltó = e.ColumnIndex;
                    SorVáltó = e.RowIndex;
                    VálasztottVáltó.Text = Tábla9.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToStrTrim();
                }
                else
                {
                    VáltósLenyíló.Visible = false;
                    return;
                }

                // érvényes sorokat engedünk kiválasztani
                if (e.RowIndex > 12) return;
                // érvényes oszlopokat engedünk kiválasztani
                if (e.ColumnIndex <= 4) return;
                VáltósLenyíló.Visible = true;

                if (Width > 460 + 27 * (e.ColumnIndex - 3) - VScrollX + 150)
                {
                    VáltósLenyíló.Left = 460 + 27 * (e.ColumnIndex - 3) - VScrollX;
                }
                else
                {
                    VáltósLenyíló.Left = 460 + 27 * (e.ColumnIndex - 3) - VScrollX - 73;
                }
                VáltósLenyíló.Top = 118 + 22 * e.RowIndex - VScrollY * 22;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tábla9_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == 0)
                VScrollX = e.NewValue;
            else
                VScrollY = e.NewValue;
        }

        private void Tábla9_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27) VáltósLenyíló.Visible = false;
        }

        private void Command33_Click(object sender, EventArgs e)
        {
            VáltóAlapszámolás();
        }

        private void VáltóAlapszámolás()
        {
            try
            {
                Nappalosszumma();
                int napokszáma;
                int pihenőnapokszáma;
                if (Tábla9.Rows.Count < 1)
                    return;

                Tábla9.Visible = false;
                for (int j = 1; j <= 12; j++)
                {
                    napokszáma = 0;
                    pihenőnapokszáma = 0;

                    for (int i = 1; i <= 31; i++)
                    {
                        if ((Tábla9.Rows[j - 1].Cells[4 + i].Value) != null)
                        {
                            string cellatartalma = Tábla9.Rows[j - 1].Cells[4 + i].Value.ToStrTrim();
                            if (cellatartalma == "7" || cellatartalma == "8")
                                napokszáma += 1;
                            if (cellatartalma == "Ü" || cellatartalma == "P")
                                pihenőnapokszáma += 1;
                        }
                    }
                    Tábla9.Rows[j - 1].Cells[1].Value = napokszáma;
                    Tábla9.Rows[j - 1].Cells[2].Value = napokszáma * Munkarend12;
                    Tábla9.Rows[j - 1].Cells[3].Value = napokszáma * Munkarend12 / 60;
                    Tábla9.Columns[3].DefaultCellStyle.Format = "0.00";
                    Tábla9.Rows[j - 1].Cells[4].Value = pihenőnapokszáma;
                }

                // Összesít félévre
                napokszáma = 0;
                pihenőnapokszáma = 0;
                for (int j = 1; j <= 6; j++)
                {
                    napokszáma += Tábla9.Rows[j - 1].Cells[1].Value.ToÉrt_Int();
                    pihenőnapokszáma += Tábla9.Rows[j - 1].Cells[4].Value.ToÉrt_Int();
                }
                Tábla9.Rows[12].Cells[0].Value = "1 félév váltós";
                Tábla9.Rows[12].Cells[1].Value = napokszáma;
                Tábla9.Rows[12].Cells[2].Value = napokszáma * Munkarend12;
                Tábla9.Rows[12].Cells[3].Value = napokszáma * Munkarend12 / 60;
                Tábla9.Columns[3].DefaultCellStyle.Format = "0.00";
                Tábla9.Rows[12].Cells[4].Value = pihenőnapokszáma;

                Tábla9.Rows[13].Cells[0].Value = "1 félév nappalos";
                Tábla9.Rows[13].Cells[1].Value = "";
                Tábla9.Rows[13].Cells[2].Value = EfélévNappal;
                Tábla9.Rows[13].Cells[3].Value = EfélévNappal / 60;
                Tábla9.Columns[3].DefaultCellStyle.Format = "0.00";

                Tábla9.Rows[13].Cells[4].Value = Epihenő;

                Tábla9.Rows[14].Cells[0].Value = "1 félév különbözet";
                Tábla9.Rows[14].Cells[1].Value = (napokszáma * Munkarend12 - EfélévNappal) / Munkarend12;
                Tábla9.Columns[1].DefaultCellStyle.Format = "0.00";
                Tábla9.Rows[14].Cells[2].Value = napokszáma * Munkarend12 - EfélévNappal;
                TextElső = napokszáma * Munkarend12 - (int)EfélévNappal;
                Tábla9.Rows[14].Cells[3].Value = (napokszáma * Munkarend12 - EfélévNappal) / 60;
                Tábla9.Rows[14].Cells[4].Value = pihenőnapokszáma - Epihenő;

                napokszáma = 0;
                pihenőnapokszáma = 0;
                for (int j = 7; j <= 12; j++)
                {
                    napokszáma += Tábla9.Rows[j - 1].Cells[1].Value.ToÉrt_Int();
                    pihenőnapokszáma += Tábla9.Rows[j - 1].Cells[4].Value.ToÉrt_Int();
                }
                Tábla9.Rows[15].Cells[0].Value = "2 félév váltós";
                Tábla9.Rows[15].Cells[1].Value = napokszáma;
                Tábla9.Rows[15].Cells[2].Value = napokszáma * Munkarend12;
                Tábla9.Rows[15].Cells[3].Value = napokszáma * Munkarend12 / 60;

                Tábla9.Rows[15].Cells[4].Value = pihenőnapokszáma;

                Tábla9.Rows[16].Cells[0].Value = "2 félév nappalos";
                Tábla9.Rows[16].Cells[1].Value = "";
                Tábla9.Rows[16].Cells[2].Value = MfélévNappal;
                Tábla9.Rows[16].Cells[3].Value = MfélévNappal / 60d;
                Tábla9.Columns[3].DefaultCellStyle.Format = "0.00";
                Tábla9.Rows[16].Cells[4].Value = Mpihenő;

                Tábla9.Rows[17].Cells[0].Value = "2 félév különbözet";
                Tábla9.Rows[17].Cells[1].Value = (napokszáma * Munkarend12 - MfélévNappal) / Munkarend12;
                Tábla9.Rows[17].Cells[2].Value = napokszáma * Munkarend12 - MfélévNappal;
                TextMásodik = napokszáma * Munkarend12 - (int)MfélévNappal;
                Tábla9.Rows[17].Cells[3].Value = (napokszáma * Munkarend12 - MfélévNappal) / 60d;
                Tábla9.Rows[17].Cells[4].Value = pihenőnapokszáma - Mpihenő;

                // összesít évre
                napokszáma = 0;
                pihenőnapokszáma = 0;
                for (int j = 1; j <= 12; j++)
                {
                    napokszáma += Tábla9.Rows[j - 1].Cells[1].Value.ToÉrt_Int();
                    pihenőnapokszáma += Tábla9.Rows[j - 1].Cells[4].Value.ToÉrt_Int();
                }
                Tábla9.Rows[18].Cells[0].Value = "Év váltós";
                Tábla9.Rows[18].Cells[1].Value = napokszáma;
                Tábla9.Rows[18].Cells[2].Value = napokszáma * Munkarend12;
                Tábla9.Rows[18].Cells[3].Value = napokszáma * Munkarend12 / 60;
                Tábla9.Rows[18].Cells[4].Value = pihenőnapokszáma;

                Tábla9.Rows[19].Cells[0].Value = "Év nappalos";
                Tábla9.Rows[19].Cells[1].Value = "";
                Tábla9.Rows[19].Cells[2].Value = Egészév;
                Tábla9.Rows[19].Cells[3].Value = Egészév / 60d;
                Tábla9.Rows[19].Cells[4].Value = Évpihenő;

                Tábla9.Rows[20].Cells[0].Value = "Év különbözet";
                Tábla9.Rows[20].Cells[1].Value = (napokszáma * Munkarend12 - Egészév) / Munkarend12;
                Tábla9.Rows[20].Cells[2].Value = napokszáma * Munkarend12 - Egészév;
                Tábla9.Rows[20].Cells[3].Value = (napokszáma * Munkarend12 - Egészév) / 60;
                Tábla9.Rows[20].Cells[4].Value = pihenőnapokszáma - Évpihenő;

                Tábla9.Refresh();
                Tábla9.Visible = true;

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Command34_Click(object sender, EventArgs e)
        {
            Váltós_Tábla_listázása();
        }

        private void VáltósLenyíló_SelectedIndexChanged(object sender, EventArgs e)
        {
            VálasztottVáltó.Text = VáltósLenyíló.Text;
            VáltósLenyíló.Visible = false;
            Tábla9.Rows[SorVáltó].Cells[OszlopVáltó].Value = VálasztottVáltó.Text.Substring(0, 1);
        }

        private void VáltósLenyíló_KeyDown(object sender, KeyEventArgs e)
        {
            // ESC
            if ((int)e.KeyCode == 27) VáltósLenyíló.Visible = false;
        }

        private void Váltós_Tábla_listázása()
        {
            try
            {
                DateTime hónaputolsónapja;
                if ((VváltósCsoport.Text) == "") return;
                Dátumnappal.Value = VáltósNaptár.Value;
                Tábla9.Rows.Clear();
                Tábla9.Columns.Clear();
                Tábla9.Refresh();
                Tábla9.Visible = false;
                Tábla9.ColumnCount = 36;
                Tábla9.RowCount = 21;
                // fejléc elkészítése
                Tábla9.Columns[0].HeaderText = "Hónap";
                Tábla9.Columns[0].Width = 145;
                Tábla9.Columns[1].HeaderText = "Mnapok";
                Tábla9.Columns[1].Width = 70;
                Tábla9.Columns[2].HeaderText = "idő [perc]";
                Tábla9.Columns[2].Width = 100;
                Tábla9.Columns[3].HeaderText = "idő [óra]";
                Tábla9.Columns[3].Width = 100;
                Tábla9.Columns[4].HeaderText = "Pihenőnapok száma";
                Tábla9.Columns[4].Width = 110;
                for (int i = 1; i <= 31; i++)
                {
                    Tábla9.Columns[i + 4].HeaderText = i.ToString();
                    Tábla9.Columns[i + 4].Width = 27;
                }
                // színezi az első és a második félévet
                for (int j = 1; j <= 12; j++)
                {
                    Tábla9.Rows[j - 1].Cells[0].Value = j;
                    for (int i = 1; i <= 31; i++)
                    {
                        if (j > 6)
                        {
                            Tábla9.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.SandyBrown;
                        }
                        else
                        {
                            Tábla9.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.PeachPuff;
                        }
                    }
                }
                for (int j = 13; j <= 21; j++)
                {
                    for (int i = 0; i <= 4; i++)
                        Tábla9.Rows[j - 1].Cells[i].Style.BackColor = Color.Olive;
                }
                for (int j = 16; j <= 18; j++)
                {
                    for (int i = 0; i <= 4; i++)
                        Tábla9.Rows[j - 1].Cells[i].Style.BackColor = Color.SandyBrown;
                }

                DateTime eleje = new DateTime(Dátumnappal.Value.Year, 1, 1);
                DateTime vége = new DateTime(Dátumnappal.Value.Year, 12, 31);
                DateTime mikor = eleje;
                int sor;
                int oszlop;
                string Tábla;
                if (!VváltósCsoport.Text.Contains("É"))
                    Tábla = VváltósCsoport.Text.Substring(VváltósCsoport.Text.Length - 1, 1).ToString();
                else
                    Tábla = VváltósCsoport.Text.Substring(VváltósCsoport.Text.Length - 1, 1).ToString();   // ha éjszakás

                List<Adat_Váltós_Naptár> Adatok = KézVNaptár.Lista_Adatok(VáltósNaptár.Value.Year, Tábla);

                foreach (Adat_Váltós_Naptár rekord in Adatok)
                {
                    mikor = rekord.Dátum;
                    sor = mikor.Month - 1;
                    oszlop = mikor.Day + 4;
                    Tábla9.Rows[sor].Cells[oszlop].Value = rekord.Nap;
                }


                // megformázzuk
                for (int j = 1; j <= 12; j++)
                {
                    DateTime idiegdátum = new DateTime(VáltósNaptár.Value.Year, j, 1);
                    hónaputolsónapja = MyF.Hónap_utolsónapja(idiegdátum);

                    for (int i = 1; i <= 31; i++)
                    {
                        if (hónaputolsónapja.Day < i)
                        {
                            Tábla9.Rows[j - 1].Cells[4 + i].Value = "X";
                            Tábla9.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.DimGray;
                        }
                        else
                        {

                            switch (Tábla9.Rows[j - 1].Cells[4 + i].Value)
                            {
                                case "Z":
                                    {
                                        Tábla9.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.Aqua;
                                        break;
                                    }
                                case "E":
                                    {
                                        Tábla9.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.Magenta;
                                        break;
                                    }
                                case "X":
                                    {
                                        Tábla9.Rows[j - 1].Cells[4 + i].Style.BackColor = Color.DimGray;
                                        break;
                                    }
                            }


                        }
                    }
                }
                Tábla9.Refresh();
                Tábla9.Visible = true;

                VáltóAlapszámolás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Panel5_MouseClick(object sender, MouseEventArgs e)
        {
            if (Chk_CTRL.Checked)
            {
                Panel5.BackColor = Color.Green;
                Command30.Visible = true;
                Command30.Enabled = true;
            }
        }

        private void Panel5_MouseMove(object sender, MouseEventArgs e)
        {
            if (Chk_CTRL.Checked)
            {
                Panel5.BackColor = Color.Green;
                Command30.Visible = true;
                Command30.Enabled = true;
            }
        }

        private void Command30_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime próbanap;
                string napiérték;
                if (Tábla9.Rows.Count < 1) return;

                string Tábla;
                if (!VváltósCsoport.Text.Contains("É"))
                    Tábla = VváltósCsoport.Text.Substring(VváltósCsoport.Text.Length - 1, 1).ToString();
                else
                    Tábla = (VváltósCsoport.Text.Substring(VváltósCsoport.Text.Length - 1, 1).ToÉrt_Int() + 4).ToString();

                List<Adat_Váltós_Naptár> Adatok = KézVNaptár.Lista_Adatok(VáltósNaptár.Value.Year, Tábla);

                Holtart.Be(32);
                List<Adat_Váltós_Naptár> AdatokMód = new List<Adat_Váltós_Naptár>();
                List<Adat_Váltós_Naptár> AdatokRögz = new List<Adat_Váltós_Naptár>();
                for (int j = 1; j <= 12; j++)
                {
                    for (int i = 1; i <= 31; i++)
                    {
                        Holtart.Lép();
                        napiérték = Tábla9.Rows[j - 1].Cells[4 + i].Value.ToString();
                        if (napiérték != "X")
                        {
                            próbanap = new DateTime(VáltósNaptár.Value.Year, j, i);

                            // ha váltós
                            Adat_Váltós_Naptár Elem = (from a in Adatok
                                                       where a.Dátum == próbanap
                                                       select a).FirstOrDefault();
                            Adat_Váltós_Naptár ADAT = new Adat_Váltós_Naptár(napiérték,
                                                                             próbanap);

                            if (Elem != null)
                                AdatokMód.Add(ADAT);
                            else
                                AdatokRögz.Add(ADAT);

                        }
                    }
                }
                if (AdatokRögz.Count > 0) KézVNaptár.Rögzítés(VáltósNaptár.Value.Year, Tábla, AdatokRögz);
                if (AdatokMód.Count > 0) KézVNaptár.Módosítás(VáltósNaptár.Value.Year, Tábla, AdatokMód);

                Váltós_Tábla_listázása();

                // Összesített adatok rögzítése


                List<Adat_Váltós_Összesítő> AdatokÖsszesítő = KézVáltÖsszesítő.Lista_Adatok(VáltósNaptár.Value.Year, Tábla);

                List<Adat_Váltós_Összesítő> AdatokMódÖ = new List<Adat_Váltós_Összesítő>();
                List<Adat_Váltós_Összesítő> AdatokRögzÖ = new List<Adat_Váltós_Összesítő>();
                for (int i = 1; i <= 12; i++)
                {
                    Holtart.Lép();
                    DateTime idegigdátum = new DateTime(VáltósNaptár.Value.Year, i, 1);
                    Adat_Váltós_Összesítő ElemÖ = (from a in AdatokÖsszesítő
                                                   where a.Dátum == idegigdátum
                                                   select a).FirstOrDefault();
                    Adat_Váltós_Összesítő ADAT = new Adat_Váltós_Összesítő(Tábla9.Rows[i - 1].Cells[2].Value.ToÉrt_Long(),
                                                                            idegigdátum);

                    if (ElemÖ != null)
                        AdatokRögzÖ.Add(ADAT);
                    else
                        AdatokRögzÖ.Add(ADAT);
                }
                if (AdatokRögzÖ.Count > 0) KézVáltÖsszesítő.Rögzítés(VáltósNaptár.Value.Year, Tábla, AdatokRögzÖ);
                if (AdatokMódÖ.Count > 0) KézVáltÖsszesítő.Módosítás(VáltósNaptár.Value.Year, Tábla, AdatokMódÖ);

                Holtart.Ki();
                Ált_Elvont_Generált("_");
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (VáltósNaptár.Value.Year == DateTime.Today.Year)
                {
                    Panel5.BackColor = Color.Red;
                    Command30.Enabled = false;
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

        private void VáltósNaptár_ValueChanged(object sender, EventArgs e)
        {
            Tábla9.Rows.Clear();
            Tábla9.Columns.Clear();
            Tábla9.Refresh();
        }

        private void VváltósCsoport_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tábla9.Rows.Clear();
            Tábla9.Columns.Clear();
            Tábla9.Refresh();
        }

        private void Excelkészítés_Click(object sender, EventArgs e)
        {
            try
            {
                string[] oszloptömb = new string[11];
                string ideigszöveg;
                string fájlexc;
                string munkalap;
                Tábla_Munkarend_kiirás();
                // kimeneti fájl helye és neve
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog
                {
                    InitialDirectory = "MyDocuments",
                    Title = "Listázott tartalom mentése Excel fájlba",
                    FileName = "Beosztás_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;
                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.ExcelLétrehozás();
                int ep;
                int zk;
                int utolsó;

                // megnyitjuk
                Holtart.Be(8);
                Dátumnappal.Value = VáltósNaptár.Value;

                Rögzített_8();
                // feltöltjük az tömböt
                oszloptömb[1] = "A";
                oszloptömb[2] = "G";
                oszloptömb[3] = "m";
                oszloptömb[4] = "S";
                oszloptömb[5] = "Y";
                oszloptömb[6] = "AE";
                oszloptömb[7] = "Ai";
                oszloptömb[8] = "AL";
                oszloptömb[9] = "AN";
                oszloptömb[10] = "AP";
                // ****************************************************
                // elkészítjük a lapokat
                // ****************************************************
                int loopTo = VváltósCsoport.Items.Count;
                for (int i = 1; i <= loopTo; i++)
                {
                    MyE.Új_munkalap(VváltósCsoport.Items[i - 1].ToString());
                }
                MyE.Munkalap_átnevezés("Munka1", "Összesítő");


                for (int jj = 1, loopTo1 = VváltósCsoport.Items.Count; jj <= loopTo1; jj++)
                {
                    Holtart.Lép();
                    MyE.Munkalap_aktív(VváltósCsoport.Items[jj - 1].ToString());
                    munkalap = VváltósCsoport.Items[jj - 1].ToString();
                    // táblázatot frissítjük
                    VváltósCsoport.Text = VváltósCsoport.Items[jj - 1].ToString();
                    Váltós_Tábla_listázása();
                    // ****************************************************
                    // megformázzuk a lapot
                    // ****************************************************
                    MyE.Munkalap_betű("Arial", 12);

                    MyE.Sormagasság("1:23", 25);
                    MyE.Sormagasság("1:1", 30);
                    MyE.Sormagasság("2:2", 50);
                    MyE.Sormagasság("5:5", 25);
                    MyE.Sormagasság("6:6", 70);
                    //Oszlopazélességet visszafelé állítjuk be

                    MyE.Oszlopszélesség(munkalap, "A:AR", 6);
                    MyE.Oszlopszélesség(munkalap, "a:ap", 5);
                    MyE.Oszlopszélesség(munkalap, "a:ao", 7);
                    MyE.Oszlopszélesség(munkalap, "a:am", 9);
                    MyE.Oszlopszélesség(munkalap, "A:AK", 10);
                    MyE.Oszlopszélesség(munkalap, "a:aj", 7);
                    MyE.Oszlopszélesség(munkalap, "A:AF", 3);
                    MyE.Oszlopszélesség(munkalap, "A:B", 3);
                    MyE.Oszlopszélesség(munkalap, "A:A", 12);

                    MyE.Egyesít(munkalap, "A1:AR1");
                    MyE.Kiir("Szolgálati beosztás " + VáltósNaptár.Value.ToString("yyyy"), "a1");
                    MyE.Betű("a1", 20);

                    MyE.Egyesít(munkalap, "A2:AR2");
                    MyE.Kiir("Karbantartó " + VváltósCsoport.Items[jj - 1].ToString() + " csoport", "a2");
                    MyE.Betű("A2", 16);
                    MyE.Egyesít(munkalap, "ag5:aj5");
                    MyE.Kiir("Váltós munkaidő percben:", "ag5");
                    MyE.Kiir(Tizenkétóra.ToString(), "ak5");
                    // csoportneveknek helye
                    MyE.Egyesít(munkalap, "A3:F3");
                    MyE.Egyesít(munkalap, "G3:L3");
                    MyE.Egyesít(munkalap, "M3:R3");
                    MyE.Egyesít(munkalap, "S3:X3");
                    MyE.Egyesít(munkalap, "Y3:AD3");
                    MyE.Egyesít(munkalap, "ae3:ah3");
                    MyE.Egyesít(munkalap, "ai3:ak3");
                    MyE.Egyesít(munkalap, "al3:am3");
                    MyE.Egyesít(munkalap, "an3:ao3");
                    MyE.Egyesít(munkalap, "ap3:ar3");
                    MyE.Egyesít(munkalap, "a4:f4");
                    MyE.Egyesít(munkalap, "g4:l4");
                    MyE.Egyesít(munkalap, "m4:r4");
                    MyE.Egyesít(munkalap, "s4:x4");
                    MyE.Egyesít(munkalap, "y4:ad4");
                    MyE.Egyesít(munkalap, "ae4:ah4");
                    MyE.Egyesít(munkalap, "ai4:ak4");
                    MyE.Egyesít(munkalap, "al4:am4");
                    MyE.Egyesít(munkalap, "an4:ao4");
                    MyE.Egyesít(munkalap, "ap4:ar4");

                    // csoportnevek kiírása
                    List<Adat_Váltós_Váltóscsopitábla> AdatokCsopi = KézVáltóscsopitábla.Lista_Adatok();
                    AdatokCsopi = AdatokCsopi.Where(a => a.Csoport == VváltósCsoport.Items[jj - 1].ToString()).OrderBy(a => a.Telephely).ToList();
                    utolsó = 1;

                    foreach (Adat_Váltós_Váltóscsopitábla rekord in AdatokCsopi)
                    {
                        MyE.Kiir(rekord.Telephely, oszloptömb[utolsó] + "3");
                        MyE.Kiir(rekord.Név, oszloptömb[utolsó] + "4");
                        utolsó += 1;
                    }


                    Holtart.Lép();
                    for (int i = 1; i <= 31; i++)
                        MyE.Kiir(i.ToString(), MyE.Oszlopnév(i + 1) + "6");
                    MyE.Betű("ag6:ar6", 10);

                    MyE.Kiir("Váltós\n munkanapok száma", "ag6");
                    MyE.Kiir("Nappalos\n munkanapok száma", "ah6");
                    MyE.Kiir("Váltós\n pihenőnapok száma", "ai6");
                    MyE.Kiir("Nappalos\n pihenőnapok száma", "aj6");
                    MyE.Kiir("Váltós \nmunkaórák \nszáma", "ak6");
                    MyE.Kiir("Váltós \nledolgozott \npercszám", "al6");
                    MyE.Kiir("Nappalos\n ledolgozott percszám", "am6");
                    MyE.Kiir("Váltós- \nNappalos különbség", "an6");
                    MyE.Kiir("Göngyölt különbség", "ao6");
                    MyE.Kiir("Kiadott \nszabadnap", "ap6");
                    MyE.Kiir("Elvont \npihenőnap", "aq6");
                    MyE.Kiir("Kifizetett \ntúlóra perc", "ar6");
                    // hónapok
                    MyE.Kiir("Január", "a7");
                    MyE.Kiir("Február", "a8");
                    MyE.Kiir("Március", "a9");
                    MyE.Kiir("Április", "a10");
                    MyE.Kiir("Május", "a11");
                    MyE.Kiir("Június", "a12");
                    MyE.Egyesít(munkalap, "a13:af13");
                    MyE.Kiir("I félév összesen:", "a13");
                    MyE.Kiir("Július", "a14");
                    MyE.Kiir("Augusztus", "a15");
                    MyE.Kiir("Szeptember", "a16");
                    MyE.Kiir("Október", "a17");
                    MyE.Kiir("November", "a18");
                    MyE.Kiir("December", "a19");
                    MyE.Egyesít(munkalap, "a20:af20");
                    MyE.Kiir("II félév összesen:", "a20");
                    MyE.Egyesít(munkalap, "a21:af21");
                    MyE.Kiir("Év összesen:", "a21");
                    MyE.SzövegIrány(munkalap, "AG6:AR6", 90);

                    MyE.Rácsoz("A6:ar21");
                    MyE.Vastagkeret("a6:a6");
                    MyE.Vastagkeret("b6:af6");
                    MyE.Vastagkeret("ag6:ao6");
                    MyE.Vastagkeret("ap6:ar6");

                    MyE.Vastagkeret("a7:a12");
                    MyE.Vastagkeret("b7:af12");
                    MyE.Vastagkeret("ag7:ao12");
                    MyE.Vastagkeret("ap7:ar12");

                    MyE.Vastagkeret("a13:a13");
                    MyE.Vastagkeret("b13:af13");
                    MyE.Vastagkeret("ag13:ao13");
                    MyE.Vastagkeret("ap13:ar13");

                    MyE.Vastagkeret("a14:a19");
                    MyE.Vastagkeret("b14:af19");
                    MyE.Vastagkeret("ag14:ao19");
                    MyE.Vastagkeret("ap14:ar19");

                    MyE.Vastagkeret("a20:a20");
                    MyE.Vastagkeret("b20:af20");
                    MyE.Vastagkeret("ag20:ao20");
                    MyE.Vastagkeret("ap20:ar20");

                    MyE.Vastagkeret("a21:a21");
                    MyE.Vastagkeret("b21:af21");
                    MyE.Vastagkeret("ag21:ao21");
                    MyE.Vastagkeret("ap21:ar21");
                    Holtart.Lép();
                    // **********************************
                    // ****Táblázatos rész kitöltése*****
                    // **********************************

                    // átmásoljuk a váltós tábla tartalmát
                    for (int oszlopv = 1; oszlopv <= 31; oszlopv++)
                    {
                        for (int sorv = 1; sorv <= 6; sorv++)
                        {
                            ideigszöveg = Tábla9.Rows[sorv - 1].Cells[oszlopv + 4].Value.ToStrTrim();

                            MyE.Kiir(ideigszöveg, MyE.Oszlopnév(oszlopv + 1) + (sorv + 6).ToString());

                            if (ideigszöveg == "7" | ideigszöveg == "Z")
                                MyE.Háttérszín(MyE.Oszlopnév(oszlopv + 1) + (sorv + 6).ToString(), 15773696d);

                            if (ideigszöveg == "Z")
                            {
                                if (VváltósCsoport.Text.Substring(0, 1) == "6")
                                {
                                    MyE.Kiir("7", MyE.Oszlopnév(oszlopv + 1) + (sorv + 6).ToString());
                                }
                                else
                                {
                                    MyE.Kiir("8", MyE.Oszlopnév(oszlopv + 1) + (sorv + 6).ToString());
                                    MyE.Háttérszíninverz(MyE.Oszlopnév(oszlopv + 1) + (sorv + 6).ToString(), 255d);
                                }
                            }
                            if (ideigszöveg == "E")
                            {
                                MyE.Háttérszín(MyE.Oszlopnév(oszlopv + 1) + (sorv + 6).ToString(), 15773696d);
                                MyE.Betű(MyE.Oszlopnév(oszlopv + 1) + (sorv + 6).ToString(), false, false, true);
                            }
                            if (ideigszöveg == "8")
                                MyE.Háttérszíninverz(MyE.Oszlopnév(oszlopv + 1) + (sorv + 6).ToString(), 255d);
                        }

                        for (int sorw = 7; sorw <= 12; sorw++)
                        {
                            ideigszöveg = Tábla9.Rows[sorw - 1].Cells[oszlopv + 4].Value.ToStrTrim();
                            MyE.Kiir(ideigszöveg, MyE.Oszlopnév(oszlopv + 1) + (sorw + 7).ToString());
                            if (ideigszöveg == "7" | ideigszöveg == "Z")
                                MyE.Háttérszín(MyE.Oszlopnév(oszlopv + 1) + (sorw + 7).ToString(), 15773696d);
                            if (ideigszöveg == "Z")
                            {
                                if (VváltósCsoport.Text.Substring(0, 1) == "6")
                                {
                                    MyE.Kiir("7", MyE.Oszlopnév(oszlopv + 1) + (sorw + 7).ToString());
                                }
                                else
                                {
                                    MyE.Kiir("8", MyE.Oszlopnév(oszlopv + 1) + (sorw + 7).ToString());
                                    MyE.Háttérszíninverz(MyE.Oszlopnév(oszlopv + 1) + (sorw + 7).ToString(), 255d);
                                }
                            }

                            if (ideigszöveg == "E")
                            {
                                MyE.Háttérszín(MyE.Oszlopnév(oszlopv + 1) + (sorw + 7).ToString(), 15773696d);
                                MyE.Betű(MyE.Oszlopnév(oszlopv + 1) + (sorw + 7).ToString(), false, false, true);
                            }
                            if (ideigszöveg == "8")
                                MyE.Háttérszíninverz(MyE.Oszlopnév(oszlopv + 1) + (sorw + 7).ToString(), 255d);
                        }

                    }

                    Holtart.Lép();
                    // összesített értékek
                    // ep és zk számolás
                    for (int sorw = 1; sorw <= 6; sorw++)
                    {
                        ep = 0;
                        zk = 0;
                        for (int oszlopw = 1; oszlopw <= 31; oszlopw++)
                        {
                            ideigszöveg = Tábla9.Rows[sorw - 1].Cells[oszlopw + 4].Value.ToStrTrim();
                            if (ideigszöveg.Trim() == "Z")
                                zk += 1;
                            if (ideigszöveg.Trim() == "E")
                                ep += 1;
                        }
                        if (zk != 0)
                            MyE.Kiir(zk.ToString(), "ap" + (sorw + 6).ToString());
                        if (ep != 0)
                            MyE.Kiir(ep.ToString(), "aq" + (sorw + 6).ToString());
                    }
                    ideigszöveg = Tábla9.Rows[14].Cells[2].Value.ToStrTrim();
                    MyE.Kiir(ideigszöveg, "ar12");
                    for (int sorw = 7; sorw <= 12; sorw++)
                    {
                        ep = 0;
                        zk = 0;
                        for (int oszlopw = 1; oszlopw <= 31; oszlopw++)
                        {
                            ideigszöveg = Tábla9.Rows[sorw - 1].Cells[oszlopw + 4].Value.ToStrTrim();
                            if (ideigszöveg.Trim() == "Z")
                                zk += 1;
                            if (ideigszöveg.Trim() == "E")
                                ep += 1;
                        }
                        if (zk != 0)
                            MyE.Kiir(zk.ToString(), "ap" + (sorw + 7).ToString());
                        if (ep != 0)
                            MyE.Kiir(ep.ToString(), "aq" + (sorw + 7).ToString());
                    }
                    ideigszöveg = Tábla9.Rows[17].Cells[2].Value.ToStrTrim();
                    MyE.Kiir(ideigszöveg, "ar19");

                    Holtart.Lép();
                    // munkanapok száma váltó
                    for (int sorw = 1; sorw <= 6; sorw++)
                    {
                        ideigszöveg = Tábla9.Rows[sorw - 1].Cells[1].Value.ToStrTrim();
                        MyE.Kiir("=" + ideigszöveg + "+RC[9]+RC[10]", "AG" + (sorw + 6).ToString());
                    }
                    for (int sorw = 7; sorw <= 12; sorw++)
                    {
                        ideigszöveg = Tábla9.Rows[sorw - 1].Cells[1].Value.ToStrTrim();
                        MyE.Kiir("=" + ideigszöveg + "+RC[9]+RC[10]", "AG" + (sorw + 7).ToString());
                    }
                    // munkanapok száma nappal
                    for (int sorw = 1; sorw <= 6; sorw++)
                    {
                        ideigszöveg = Tábla_Nappalos.Rows[sorw - 1].Cells[1].Value.ToStrTrim();
                        MyE.Kiir(ideigszöveg, "Ah" + (sorw + 6).ToString());
                    }
                    for (int sorw = 7; sorw <= 12; sorw++)
                    {
                        ideigszöveg = Tábla_Nappalos.Rows[sorw - 1].Cells[1].Value.ToStrTrim();
                        MyE.Kiir(ideigszöveg, "Ah" + (sorw + 7).ToString());
                    }
                    // pihenőnapok száma váltó
                    for (int sorw = 1; sorw <= 6; sorw++)
                    {
                        ideigszöveg = Tábla9.Rows[sorw - 1].Cells[4].Value.ToStrTrim();
                        MyE.Kiir(ideigszöveg, "AI" + (sorw + 6).ToString());
                    }
                    for (int sorw = 7; sorw <= 12; sorw++)
                    {
                        ideigszöveg = Tábla9.Rows[sorw - 1].Cells[4].Value.ToStrTrim();
                        MyE.Kiir(ideigszöveg, "AI" + (sorw + 7).ToString());
                    }
                    // pihenőnapok száma nappal
                    for (int sorw = 1; sorw <= 6; sorw++)
                    {
                        ideigszöveg = Tábla_Nappalos.Rows[sorw - 1].Cells[4].Value.ToStrTrim();
                        MyE.Kiir(ideigszöveg, "Aj" + (sorw + 6).ToString());
                    }
                    for (int sorw = 7; sorw <= 12; sorw++)
                    {
                        ideigszöveg = Tábla_Nappalos.Rows[sorw - 1].Cells[4].Value.ToStrTrim();
                        MyE.Kiir(ideigszöveg, "Aj" + (sorw + 7).ToString());
                    }
                    // munkaórák száma
                    for (int sorw = 1; sorw <= 6; sorw++)
                        MyE.Kiir("=RC[1]/60", "Ak" + (sorw + 6).ToString());
                    for (int sorw = 7; sorw <= 12; sorw++)
                        MyE.Kiir("=RC[1]/60", "Ak" + (sorw + 7).ToString());
                    Holtart.Lép();
                    // munkapercek száma váltó
                    for (int sorw = 1; sorw <= 6; sorw++)
                        MyE.Kiir("=R5C37*RC[-5]", "Al" + (sorw + 6).ToString());
                    for (int sorw = 7; sorw <= 12; sorw++)
                        MyE.Kiir("=R5C37*RC[-5]", "Al" + (sorw + 7).ToString());
                    // munkapercek száma nappal
                    for (int sorw = 1; sorw <= 6; sorw++)
                    {
                        ideigszöveg = Tábla_Nappalos.Rows[sorw - 1].Cells[2].Value.ToStrTrim();
                        MyE.Kiir(ideigszöveg, "Am" + (sorw + 6).ToString());
                    }
                    for (int sorw = 7; sorw <= 12; sorw++)
                    {
                        ideigszöveg = Tábla_Nappalos.Rows[sorw - 1].Cells[2].Value.ToStrTrim();
                        MyE.Kiir(ideigszöveg, "Am" + (sorw + 7).ToString());
                    }
                    // váltós-nappalos különbség
                    for (int sorw = 1; sorw <= 6; sorw++)
                        MyE.Kiir("=RC[-2]-RC[-1]", "An" + (sorw + 6).ToString());
                    for (int sorw = 7; sorw <= 12; sorw++)
                        MyE.Kiir("=RC[-2]-RC[-1]", "An" + (sorw + 7).ToString());
                    // göngyölt különbség
                    MyE.Kiir("=(RC[-1])-(RC[1]*R[-2]C[-4])-(RC[2]*R[-2]C[-4])-RC[3]", "Ao7");
                    for (int sorw = 2; sorw <= 6; sorw++)
                        MyE.Kiir("=(RC[-1]+R[-1]C)-(RC[1]*R5C37)-(RC[2]*R5C37)-RC[3]", "Ao" + (sorw + 6).ToString());
                    MyE.Kiir("=(RC[-1])-(RC[1]*R[-9]C[-4])-(RC[2]*R[-9]C[-4])-RC[3]", "Ao14");
                    for (int sorw = 8; sorw <= 12; sorw++)
                        MyE.Kiir("=(RC[-1]+R[-1]C)-(RC[1]*R5C37)-(RC[2]*R5C37)-RC[3]", "Ao" + (sorw + 7).ToString());
                    Holtart.Lép();
                    // féléves szumma
                    for (int oszlopw = 33; oszlopw <= 44; oszlopw++)
                    {

                        MyE.Kiir("=SUM(R[-6]C:R[-1]C)", MyE.Oszlopnév(oszlopw) + "13");
                        MyE.Kiir("=SUM(R[-6]C:R[-1]C)", MyE.Oszlopnév(oszlopw) + "20");
                        MyE.Kiir("=SUM(R[-1]C,R[-8]C)", MyE.Oszlopnév(oszlopw) + "21");

                    }
                    MyE.Kiir("=R[-1]C", "ao13");
                    MyE.Kiir("=R[-1]C", "ao20");
                    // jel magyarázat
                    MyE.Egyesít(munkalap, "a23:E23");
                    MyE.Kiir("7 - Nappalos műszak", "a23");
                    MyE.Háttérszín("a23", 15773696d);
                    MyE.Egyesít(munkalap, "f23:l23");
                    MyE.Kiir("8 - Éjszakás műszak", "f23");
                    MyE.Háttérszíninverz("f23", 255d);
                    MyE.Egyesít(munkalap, "m23:s23");
                    MyE.Kiir("E - Elvont pihenőnap", "m23");
                    MyE.Háttérszín("m23", 15773696d);
                    MyE.Betű("m23", false, false, true);
                    MyE.Egyesít(munkalap, "t23:z23");
                    MyE.Kiir("P - Pihenőnap", "t23");
                    MyE.Egyesít(munkalap, "aa23:ae23");
                    MyE.Kiir(" - Szabadnap", "aa23");
                    MyE.Egyesít(munkalap, "af23:ah23");

                    MyE.ExcelMentés(fájlexc);
                    MyE.Aktív_Cella(munkalap, "A1");
                    MyE.NyomtatásiTerület_részletes(VváltósCsoport.Items[jj - 1].ToString(), "A1:AR23", "", "", false);

                    // lapok ciklus vége
                    MyE.ExcelMentés(fájlexc);

                }
                // öSSZESÍTŐ TÁBLA
                MyE.Munkalap_aktív("Összesítő");
                munkalap = "Összesítő";
                MyE.Egyesít(munkalap, "B1:D1");
                MyE.Egyesít(munkalap, "E1:G1");
                MyE.Egyesít(munkalap, "H1:J1");
                MyE.Kiir("Első félév", "B1");
                MyE.Kiir("Második félév", "e1");
                MyE.Kiir("Év összesen", "h1");
                for (int i = 2; i <= 10; i++)
                    MyE.SzövegIrány(munkalap, MyE.Oszlopnév(i) + "2", 90);


                MyE.Kiir("Kiadott\nszabadnap", "b2");
                MyE.Kiir("Elvont\npihenőnap", "c2");
                MyE.Kiir("Kifizetett\ntúlóra perc", "d2");

                MyE.Kiir("Kiadott\nszabadnap", "e2");
                MyE.Kiir("Elvont\npihenőnap", "f2");
                MyE.Kiir("Kifizetett\ntúlóra perc", "g2");

                MyE.Kiir("Kiadott\nszabadnap", "h2");
                MyE.Kiir("Elvont\npihenőnap", "i2");
                MyE.Kiir("Kifizetett\ntúlóra perc", "j2");

                MyE.Sormagasság("2:2", 65);


                for (int i = 1; i <= VváltósCsoport.Items.Count; i++)
                {
                    MyE.Kiir(VváltósCsoport.Items[i - 1].ToString(), "a" + (i + 2).ToString());
                    MyE.Kiir("='" + VváltósCsoport.Items[i - 1].ToString() + "'!R13C42", "b" + (i + 2).ToString());
                    MyE.Kiir("='" + VváltósCsoport.Items[i - 1].ToString() + "'!R13C43", "c" + (i + 2).ToString());
                    MyE.Kiir("='" + VváltósCsoport.Items[i - 1].ToString() + "'!R13C44", "d" + (i + 2).ToString());
                    MyE.Kiir("='" + VváltósCsoport.Items[i - 1].ToString() + "'!R20C42", "e" + (i + 2).ToString());
                    MyE.Kiir("='" + VváltósCsoport.Items[i - 1].ToString() + "'!R20C43", "f" + (i + 2).ToString());
                    MyE.Kiir("='" + VváltósCsoport.Items[i - 1].ToString() + "'!R20C44", "g" + (i + 2).ToString());
                    MyE.Kiir("='" + VváltósCsoport.Items[i - 1].ToString() + "'!R21C42", "h" + (i + 2).ToString());
                    MyE.Kiir("='" + VváltósCsoport.Items[i - 1].ToString() + "'!R21C43", "i" + (i + 2).ToString());
                    MyE.Kiir("='" + VváltósCsoport.Items[i - 1].ToString() + "'!R21C44", "j" + (i + 2).ToString());
                }

                MyE.Rácsoz("a1:j" + (VváltósCsoport.Items.Count + 2).ToString());
                MyE.Vastagkeret("b1");
                MyE.Vastagkeret("e1");
                MyE.Vastagkeret("h1");
                MyE.Vastagkeret("b2:d" + (VváltósCsoport.Items.Count + 2).ToString());
                MyE.Vastagkeret("e2:g" + (VváltósCsoport.Items.Count + 2).ToString());
                MyE.Vastagkeret("h2:j" + (VváltósCsoport.Items.Count + 2).ToString());
                MyE.Vastagkeret("a3:a" + (VváltósCsoport.Items.Count + 2).ToString());

                MyE.NyomtatásiTerület_részletes("Összesítő", "a1:j" + (VváltósCsoport.Items.Count + 2).ToString(), "", "", false);

                MyE.Aktív_Cella(munkalap, "A1");
                Holtart.Ki();

                MyE.ExcelMentés(fájlexc);
                // alkalmazás leállítása
                MyE.ExcelBezárás();
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


        #region Elvont napok
        private void ElvontTelephelyfeltöltés()
        {
            try
            {
                List<Adat_Kiegészítő_Könyvtár> Adatok = KézKiegKönyvtár.Lista_Adatok();
                ElvontTelephely.Items.Clear();
                SzűrtTelephely.Items.Clear();
                ElvontTelephely.Items.Add("_");
                SzűrtTelephely.Items.Add("_");

                foreach (Adat_Kiegészítő_Könyvtár Elem in Adatok)
                {
                    ElvontTelephely.Items.Add(Elem.Név);
                    SzűrtTelephely.Items.Add(Elem.Név);
                }

                ElvontTelephely.Refresh();
                SzűrtTelephely.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Tábla_Elvont_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Tábla_Elvont.Rows[e.RowIndex].Selected = true;
        }

        private void Tábla_Elvont_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Tábla_Elvont.Rows[e.RowIndex].Selected = true;
        }

        private void Tábla_Elvont_SelectionChanged(object sender, EventArgs e)
        {
            if (Tábla_Elvont.SelectedRows.Count != 0)
            {
                ElvontCsoport.Text = Tábla_Elvont.Rows[Tábla_Elvont.SelectedRows[0].Index].Cells[1].Value.ToString();
                ElvontDátum.Value = Tábla_Elvont.Rows[Tábla_Elvont.SelectedRows[0].Index].Cells[2].Value.ToÉrt_DaTeTime();
                ElvontTelephely.Text = Tábla_Elvont.Rows[Tábla_Elvont.SelectedRows[0].Index].Cells[0].Value.ToString();
            }
        }

        private void Tábla_Elvont_kiirás()
        {
            try
            {
                if (!int.TryParse(ElvontÉv.Text, out int Év)) throw new HibásBevittAdat("Az év mező nem tartalmaz megfelelő értéket.");
                AdatokKijelöltnapok = KézKijelöltnapok.Lista_Adatok(Év);
                DateTime Éveleje = new DateTime(Év, 1, 1);
                DateTime Évvége = new DateTime(Év, 12, 31);
                List<Adat_Váltós_Kijelöltnapok> Adatok = (from a in AdatokKijelöltnapok
                                                          where a.Dátum >= Éveleje
                                                          && a.Dátum <= Évvége
                                                          && a.Telephely == SzűrtTelephely.Text.Trim()
                                                          orderby a.Csoport, a.Dátum
                                                          select a).ToList();
                Tábla_Elvont.Rows.Clear();
                Tábla_Elvont.Columns.Clear();
                Tábla_Elvont.Refresh();
                Tábla_Elvont.Visible = false;
                Tábla_Elvont.ColumnCount = 3;

                // fejléc elkészítése
                Tábla_Elvont.Columns[0].HeaderText = "Telephely";
                Tábla_Elvont.Columns[0].Width = 110;
                Tábla_Elvont.Columns[1].HeaderText = "Csoport";
                Tábla_Elvont.Columns[1].Width = 110;
                Tábla_Elvont.Columns[2].HeaderText = "Elvont nap";
                Tábla_Elvont.Columns[2].Width = 110;
                foreach (Adat_Váltós_Kijelöltnapok rekord in Adatok)
                {
                    Tábla_Elvont.RowCount++;
                    int i = Tábla_Elvont.RowCount - 1;
                    Tábla_Elvont.Rows[i].Cells[0].Value = rekord.Telephely;
                    Tábla_Elvont.Rows[i].Cells[1].Value = rekord.Csoport;
                    Tábla_Elvont.Rows[i].Cells[2].Value = rekord.Dátum.ToString("yyyy.MM.dd");
                }
                Tábla_Elvont.Visible = true;
                Tábla_Elvont.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ElvontCsoportfeltöltés()
        {
            ElvontCsoport.Items.Clear();
            List<Adat_Kiegészítő_Turnusok> Adatok = KézTurnusok.Lista_Adatok();
            foreach (Adat_Kiegészítő_Turnusok Elem in Adatok)
                ElvontCsoport.Items.Add(Elem.Csoport);

            ElvontCsoport.Refresh();
        }

        private void Elvont_Frissít_Click(object sender, EventArgs e)
        {
            if (ElvontÉv.Text.Trim() == "") ElvontÉv.Text = DateTime.Today.Year.ToString();
            if (!int.TryParse(ElvontÉv.Text, out int _)) ElvontÉv.Text = DateTime.Today.Year.ToString();
            if (SzűrtTelephely.Text.Trim() == "") SzűrtTelephely.Text = "_";
            Tábla_Elvont_kiirás();
            Elvontürítés();
        }

        private void Elvont_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (ElvontCsoport.Text.Trim() == "") throw new HibásBevittAdat("A Csoport mezőt ki kell tölteni.");
                if (ElvontTelephely.Text.Trim() == "") throw new HibásBevittAdat("A telephely mezőt ki kell tölteni.");
                AdatokKijelöltnapok = KézKijelöltnapok.Lista_Adatok(ElvontDátum.Value.Year);

                Adat_Váltós_Kijelöltnapok Elem = (from a in AdatokKijelöltnapok
                                                  where a.Telephely == ElvontTelephely.Text.Trim()
                                                  && a.Csoport == ElvontCsoport.Text.Trim()
                                                  && a.Dátum == ElvontDátum.Value
                                                  select a).FirstOrDefault() ?? throw new HibásBevittAdat("Nincs ilyen elem.");

                Adat_Váltós_Kijelöltnapok ADAT = new Adat_Váltós_Kijelöltnapok(ElvontTelephely.Text.Trim(),
                                                                               ElvontCsoport.Text.Trim(),
                                                                               ElvontDátum.Value);

                if (Elem == null)
                {
                    KézKijelöltnapok.Rögzítés(ElvontDátum.Value.Year, ADAT);
                    Tábla_Elvont_kiirás();
                    MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void Elvont_Új_Click(object sender, EventArgs e)
        {
            Elvontürítés();
        }

        private void Elvontürítés()
        {
            ElvontCsoport.Text = "";
            ElvontDátum.Value = DateTime.Today;
            ElvontTelephely.Text = "_";
        }

        private void Elvont_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (ElvontCsoport.Text.Trim() == "") throw new HibásBevittAdat("A Csoport mezőt ki kell tölteni.");
                if (ElvontTelephely.Text.Trim() == "") throw new HibásBevittAdat("A telephely mezőt ki kell tölteni.");
                AdatokKijelöltnapok = KézKijelöltnapok.Lista_Adatok(ElvontDátum.Value.Year);

                Adat_Váltós_Kijelöltnapok Elem = (from a in AdatokKijelöltnapok
                                                  where a.Telephely == ElvontTelephely.Text.Trim()
                                                  && a.Csoport == ElvontCsoport.Text.Trim()
                                                  && a.Dátum == ElvontDátum.Value
                                                  select a).FirstOrDefault() ?? throw new HibásBevittAdat("Nincs ilyen elem.");
                if (Elem != null)
                {
                    KézKijelöltnapok.Törlés(ElvontDátum.Value.Year, Elem);
                    Tábla_Elvont_kiirás();
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void Elvont_Generált_Click(object sender, EventArgs e)
        {
            Ált_Elvont_Generált(ElvontTelephely.Text.Trim());
            Tábla_Elvont_kiirás();
        }

        private void Ált_Elvont_Generált(string TelepElvont)
        {
            try
            {
                List<Adat_Kiegészítő_Turnusok> AdatokCsop = KézTurnusok.Lista_Adatok();
                List<Adat_Váltós_Kijelöltnapok> AdatokKij = KézKijelöltnapok.Lista_Adatok(ElvontDátum.Value.Year);

                int i = 0;
                List<Adat_Váltós_Kijelöltnapok> AdatokGy = new List<Adat_Váltós_Kijelöltnapok>();
                foreach (Adat_Kiegészítő_Turnusok Elem in AdatokCsop)
                {
                    List<Adat_Váltós_Naptár> Adatok = KézVNaptár.Lista_Adatok(ElvontDátum.Value.Year, $"{i + 1}");
                    Adatok = Adatok.Where(a => a.Nap == "E").ToList();

                    foreach (Adat_Váltós_Naptár rekord in Adatok)
                    {
                        string ideig = (from a in AdatokKij
                                        where a.Telephely == TelepElvont && a.Dátum == rekord.Dátum && a.Csoport == Elem.Csoport
                                        select a.Csoport).FirstOrDefault();

                        if (ideig == null)
                        {
                            Adat_Váltós_Kijelöltnapok ADAT = new Adat_Váltós_Kijelöltnapok(
                                                                ElvontTelephely.Text.Trim(),
                                                                ElvontCsoport.Text.Trim(),
                                                                ElvontDátum.Value);
                            AdatokGy.Add(ADAT);
                        }
                    }
                    i++;
                }
                if (AdatokGy.Count > 0) KézKijelöltnapok.Rögzítés(ElvontDátum.Value.Year, AdatokGy);
            }
            catch (HibásBevittAdat ex)
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


        #region Váltós munkarend
        private void Tábla_VáltMunka_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Tábla_VáltMunka.Rows[e.RowIndex].Selected = true;
        }

        private void Tábla_VáltMunka_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Tábla_VáltMunka.Rows[e.RowIndex].Selected = true;
        }

        private void Tábla_VáltMunka_SelectionChanged(object sender, EventArgs e)
        {
            if (Tábla_VáltMunka.SelectedRows.Count != 0)
            {
                Hétnapja.Text = Tábla_VáltMunka.Rows[Tábla_VáltMunka.SelectedRows[0].Index].Cells[1].Value.ToStrTrim();
                VáltMunkBeoKód.Text = Tábla_VáltMunka.Rows[Tábla_VáltMunka.SelectedRows[0].Index].Cells[2].Value.ToStrTrim();
                BeosztásSzöveg.Text = Tábla_VáltMunka.Rows[Tábla_VáltMunka.SelectedRows[0].Index].Cells[3].Value.ToStrTrim();
            }
        }

        private void VváltósCsoportfeltöltés()
        {
            VváltósCsoport.Items.Clear();
            List<Adat_Kiegészítő_Turnusok> Adatok = KézTurnusok.Lista_Adatok();
            foreach (Adat_Kiegészítő_Turnusok Elem in Adatok)
                VváltósCsoport.Items.Add(Elem.Csoport);
            VváltósCsoport.Refresh();
        }

        private void Tábla_VáltMunka_kiirás()
        {
            try
            {
                AdatokBeosztásciklusVáltó = KézBeosztásciklus.Lista_Adatok("beosztásciklus");
                Tábla_VáltMunka.Rows.Clear();
                Tábla_VáltMunka.Columns.Clear();
                Tábla_VáltMunka.Refresh();
                Tábla_VáltMunka.Visible = false;
                Tábla_VáltMunka.ColumnCount = 4;

                // fejléc elkészítése
                Tábla_VáltMunka.Columns[0].HeaderText = "id";
                Tábla_VáltMunka.Columns[0].Width = 120;
                Tábla_VáltMunka.Columns[1].HeaderText = "Hétnapja";
                Tábla_VáltMunka.Columns[1].Width = 120;
                Tábla_VáltMunka.Columns[2].HeaderText = "Beosztás kód";
                Tábla_VáltMunka.Columns[2].Width = 120;
                Tábla_VáltMunka.Columns[3].HeaderText = "Beosztás szöveg";
                Tábla_VáltMunka.Columns[3].Width = 120;

                foreach (Adat_Kiegészítő_Beosztásciklus rekord in AdatokBeosztásciklusVáltó)
                {
                    Tábla_VáltMunka.RowCount++;
                    int i = Tábla_VáltMunka.RowCount - 1;

                    Tábla_VáltMunka.Rows[i].Cells[0].Value = rekord.Id;
                    Tábla_VáltMunka.Rows[i].Cells[1].Value = rekord.Hétnapja;
                    Tábla_VáltMunka.Rows[i].Cells[2].Value = rekord.Beosztáskód;
                    Tábla_VáltMunka.Rows[i].Cells[3].Value = rekord.Beosztásszöveg;
                }
                Tábla_VáltMunka.Visible = true;
                Tábla_VáltMunka.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VáltMunka_OK_Click(object sender, EventArgs e)
        {
            try
            {
                if (Hétnapja.Text.Trim() == "") throw new HibásBevittAdat("A hét napját meg kell adni.");

                if (Tábla_VáltMunka.SelectedRows.Count < 1)
                {
                    Adat_Kiegészítő_Beosztásciklus ADAT = new Adat_Kiegészítő_Beosztásciklus(0,
                                                                                             VáltMunkBeoKód.Text.Trim(),
                                                                                             Hétnapja.Text.Trim(),
                                                                                             BeosztásSzöveg.Text.Trim());
                    KézBeosztásciklus.Rögzítés("beosztásciklus", ADAT);
                }
                else
                {
                    int Éj_Sor = Tábla_VáltMunka.SelectedRows[0].Index;
                    int ID = Tábla_VáltMunka.Rows[Éj_Sor].Cells[0].Value.ToÉrt_Int();
                    Adat_Kiegészítő_Beosztásciklus ADAT = new Adat_Kiegészítő_Beosztásciklus(ID,
                                                                                             VáltMunkBeoKód.Text.Trim(),
                                                                                             Hétnapja.Text.Trim(),
                                                                                             BeosztásSzöveg.Text.Trim());
                    KézBeosztásciklus.Módosítás("beosztásciklus", ADAT);
                }
                Tábla_VáltMunka_kiirás();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VáltMunka_Feljebb_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_VáltMunka.SelectedRows.Count > 1) throw new HibásBevittAdat("Csak egy sort lehet kijelölni.");
                if (Tábla_VáltMunka.SelectedRows[0].Index < 0) throw new HibásBevittAdat("Nincs kiválasztva sor.");
                if (Tábla_VáltMunka.SelectedRows[0].Index == 0) throw new HibásBevittAdat("Az első sort nem lehet előrébb vinni.");

                AdatokBeosztásciklusVáltó = KézBeosztásciklus.Lista_Adatok("beosztásciklus");
                int Éj_Sor = Tábla_VáltMunka.SelectedRows[0].Index;
                int ID = Tábla_VáltMunka.Rows[Éj_Sor].Cells[0].Value.ToÉrt_Int();

                Adat_Kiegészítő_Beosztásciklus Választott = (from a in AdatokBeosztásciklusVáltó
                                                             where a.Id == ID
                                                             select a).First();

                Adat_Kiegészítő_Beosztásciklus Előtte = (from a in AdatokBeosztásciklusVáltó
                                                         where a.Id == ID - 1
                                                         select a).First();

                Adat_Kiegészítő_Beosztásciklus ADAT = new Adat_Kiegészítő_Beosztásciklus(Választott.Id,
                                                                                         Előtte.Beosztáskód,
                                                                                         Előtte.Hétnapja,
                                                                                         Előtte.Beosztásszöveg);

                Adat_Kiegészítő_Beosztásciklus ADAT1 = new Adat_Kiegészítő_Beosztásciklus(Előtte.Id,
                                                                                          Választott.Beosztáskód,
                                                                                          Választott.Hétnapja,
                                                                                          Választott.Beosztásszöveg);

                KézBeosztásciklus.Módosítás("beosztásciklus", ADAT);
                KézBeosztásciklus.Módosítás("beosztásciklus", ADAT1);
                Tábla_VáltMunka_kiirás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VáltMunka_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_VáltMunka.SelectedRows.Count > 1) throw new HibásBevittAdat("Csak egy sort lehet kijelölni.");
                if (Tábla_VáltMunka.SelectedRows[0].Index < 0) throw new HibásBevittAdat("Nincs kiválasztva sor.");

                int Éj_Sor = Tábla_VáltMunka.SelectedRows[0].Index;
                int ID = Tábla_VáltMunka.Rows[Éj_Sor].Cells[0].Value.ToÉrt_Int();
                AdatokBeosztásciklusVáltó = KézBeosztásciklus.Lista_Adatok("beosztásciklus");

                Adat_Kiegészítő_Beosztásciklus Elem = (from a in AdatokBeosztásciklusVáltó
                                                       where a.Id == ID
                                                       select a).First();
                if (Elem != null)
                {
                    KézBeosztásciklus.Törlés("beosztásciklus", Elem);
                    Tábla_VáltMunka_kiirás();
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void VáltMunka_Új_Click(object sender, EventArgs e)
        {
            Hétnapja.Text = "";
            VáltMunkBeoKód.Text = "";
            BeosztásSzöveg.Text = "";
            Tábla_VáltMunka.ClearSelection();
        }
        #endregion


        #region Csopvez nevek
        private void Tábla_CsopVez_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Tábla_CsopVez.Rows[e.RowIndex].Selected = true;
        }

        private void Tábla_CsopVez_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Tábla_CsopVez.Rows[e.RowIndex].Selected = true;
        }

        private void Tábla_CsopVez_SelectionChanged(object sender, EventArgs e)
        {
            if (Tábla_CsopVez.SelectedRows.Count != 0)
            {
                CsoportVáltóCsop.Text = Tábla_CsopVez.Rows[Tábla_CsopVez.SelectedRows[0].Index].Cells[0].Value.ToString();
                CsopVezNév.Text = Tábla_CsopVez.Rows[Tábla_CsopVez.SelectedRows[0].Index].Cells[2].Value.ToString();
                TelephelyVáltóCsop.Text = Tábla_CsopVez.Rows[Tábla_CsopVez.SelectedRows[0].Index].Cells[1].Value.ToString();
            }
        }

        private void Tábla_CsopVez_kiirás()
        {
            try
            {
                AdatokVáltóscsopitábla = KézVáltóscsopitábla.Lista_Adatok();

                Tábla_CsopVez.Rows.Clear();
                Tábla_CsopVez.Columns.Clear();
                Tábla_CsopVez.Refresh();
                Tábla_CsopVez.Visible = false;
                Tábla_CsopVez.ColumnCount = 3;

                // fejléc elkészítése
                Tábla_CsopVez.Columns[0].HeaderText = "Csoport";
                Tábla_CsopVez.Columns[0].Width = 150;
                Tábla_CsopVez.Columns[1].HeaderText = "Telephely";
                Tábla_CsopVez.Columns[1].Width = 150;
                Tábla_CsopVez.Columns[2].HeaderText = "Név";
                Tábla_CsopVez.Columns[2].Width = 150;

                foreach (Adat_Váltós_Váltóscsopitábla rekord in AdatokVáltóscsopitábla)
                {
                    Tábla_CsopVez.RowCount++;
                    int i = Tábla_CsopVez.RowCount - 1;
                    Tábla_CsopVez.Rows[i].Cells[0].Value = rekord.Csoport;
                    Tábla_CsopVez.Rows[i].Cells[1].Value = rekord.Telephely;
                    Tábla_CsopVez.Rows[i].Cells[2].Value = rekord.Név;
                }
                Tábla_CsopVez.Visible = true;
                Tábla_CsopVez.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CsopVez_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (CsoportVáltóCsop.Text.Trim() == "") throw new HibásBevittAdat("Csoport elnevezésnek tartalmaznia kell adatot.");
                if (TelephelyVáltóCsop.Text.Trim() == "") throw new HibásBevittAdat("Telephelynek tartalmaznia kell adatot.");
                if (CsopVezNév.Text.Trim() == "") throw new HibásBevittAdat("Csoportvezető nevének tartalmaznia kell adatot.");

                AdatokVáltóscsopitábla = KézVáltóscsopitábla.Lista_Adatok();
                Adat_Váltós_Váltóscsopitábla Elem = (from a in AdatokVáltóscsopitábla
                                                     where a.Csoport == CsoportVáltóCsop.Text.Trim()
                                                     && a.Telephely == TelephelyVáltóCsop.Text.Trim()
                                                     select a).FirstOrDefault();
                Adat_Váltós_Váltóscsopitábla ADAT = new Adat_Váltós_Váltóscsopitábla(CsoportVáltóCsop.Text.Trim(),
                                                                                     TelephelyVáltóCsop.Text.Trim(),
                                                                                     CsopVezNév.Text.Trim());
                if (Elem != null)
                    KézVáltóscsopitábla.Módosítás(ADAT);
                else
                    KézVáltóscsopitábla.Rögzítés(ADAT);

                Tábla_CsopVez_kiirás();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CsopVez_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (CsoportVáltóCsop.Text.Trim() == "") throw new HibásBevittAdat("Csoport elnevezésnek tartalmaznia kell adatot.");
                if (TelephelyVáltóCsop.Text.Trim() == "") throw new HibásBevittAdat("Telephelynek tartalmaznia kell adatot.");
                if (CsopVezNév.Text.Trim() == "") throw new HibásBevittAdat("Csoportvezető nevének tartalmaznia kell adatot.");
                AdatokVáltóscsopitábla = KézVáltóscsopitábla.Lista_Adatok();

                Adat_Váltós_Váltóscsopitábla Elem = (from a in AdatokVáltóscsopitábla
                                                     where a.Csoport == CsoportVáltóCsop.Text.Trim()
                                                     && a.Telephely == TelephelyVáltóCsop.Text.Trim()
                                                     select a).FirstOrDefault();
                if (Elem != null)
                {
                    KézVáltóscsopitábla.Törlés(Elem);
                    Tábla_CsopVez_kiirás();
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void CSOPORTCsoportfeltöltés()
        {
            try
            {
                CsoportVáltóCsop.Items.Clear();
                List<Adat_Kiegészítő_Turnusok> Adatok = KézTurnusok.Lista_Adatok();

                foreach (Adat_Kiegészítő_Turnusok Elem in Adatok)
                    CsoportVáltóCsop.Items.Add(Elem.Csoport);
                CsoportVáltóCsop.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CsoportvezTelephelyfeltöltés()
        {
            TelephelyVáltóCsop.Items.Clear();
            TelephelyVáltóCsop.Items.Add("_");
            List<Adat_Kiegészítő_Könyvtár> Adatok = KézKiegKönyvtár.Lista_Adatok();
            foreach (Adat_Kiegészítő_Könyvtár Elem in Adatok)
                TelephelyVáltóCsop.Items.Add(Elem.Név);
            TelephelyVáltóCsop.Refresh();
        }
        #endregion


        #region Éjszakás munkarend
        private void Tábla_Éjszaka_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Tábla_Éjszaka.Rows[e.RowIndex].Selected = true;
        }

        private void Tábla_Éjszaka_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            Tábla_Éjszaka.Rows[e.RowIndex].Selected = true;
        }

        private void Tábla_Éjszaka_SelectionChanged(object sender, EventArgs e)
        {
            if (Tábla_Éjszaka.SelectedRows.Count != 0)
            {
                ÉhétNapja.Text = Tábla_Éjszaka.Rows[Tábla_Éjszaka.SelectedRows[0].Index].Cells[1].Value.ToString();
                ÉBeoKód.Text = Tábla_Éjszaka.Rows[Tábla_Éjszaka.SelectedRows[0].Index].Cells[2].Value.ToString();
                ÉBeosztásSzöveg.Text = Tábla_Éjszaka.Rows[Tábla_Éjszaka.SelectedRows[0].Index].Cells[3].Value.ToString();
            }
        }

        private void Tábla_Éjszaka_kiirás()
        {
            try
            {
                AdatokÉjszakásBeoCiklus = KézBeosztásciklus.Lista_Adatok("éjszakásciklus");

                Tábla_Éjszaka.Rows.Clear();
                Tábla_Éjszaka.Columns.Clear();
                Tábla_Éjszaka.Refresh();
                Tábla_Éjszaka.Visible = false;
                Tábla_Éjszaka.ColumnCount = 4;

                // fejléc elkészítése
                Tábla_Éjszaka.Columns[0].HeaderText = "id";
                Tábla_Éjszaka.Columns[0].Width = 120;
                Tábla_Éjszaka.Columns[1].HeaderText = "Hétnapja";
                Tábla_Éjszaka.Columns[1].Width = 120;
                Tábla_Éjszaka.Columns[2].HeaderText = "Beosztás kód";
                Tábla_Éjszaka.Columns[2].Width = 120;
                Tábla_Éjszaka.Columns[3].HeaderText = "Beosztás szöveg";
                Tábla_Éjszaka.Columns[3].Width = 120;

                foreach (Adat_Kiegészítő_Beosztásciklus rekord in AdatokÉjszakásBeoCiklus)
                {
                    Tábla_Éjszaka.RowCount++;
                    int i = Tábla_Éjszaka.RowCount - 1;
                    Tábla_Éjszaka.Rows[i].Cells[0].Value = rekord.Id;
                    Tábla_Éjszaka.Rows[i].Cells[1].Value = rekord.Hétnapja;
                    Tábla_Éjszaka.Rows[i].Cells[2].Value = rekord.Beosztáskód;
                    Tábla_Éjszaka.Rows[i].Cells[3].Value = rekord.Beosztásszöveg;
                }
                Tábla_Éjszaka.Visible = true;
                Tábla_Éjszaka.Refresh();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Éjszaka_Feljebb_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_Éjszaka.SelectedRows.Count > 1) throw new HibásBevittAdat("Csak egy sort lehet kijelölni.");
                if (Tábla_Éjszaka.SelectedRows[0].Index < 0) throw new HibásBevittAdat("Nincs kiválasztva sor.");
                if (Tábla_Éjszaka.SelectedRows[0].Index == 0) throw new HibásBevittAdat("Az első sort nem lehet előrébb vinni.");

                AdatokÉjszakásBeoCiklus = KézBeosztásciklus.Lista_Adatok("éjszakásciklus");
                int Éj_Sor = Tábla_Éjszaka.SelectedRows[0].Index;
                int ID = Tábla_Éjszaka.Rows[Éj_Sor].Cells[0].Value.ToÉrt_Int();

                Adat_Kiegészítő_Beosztásciklus Választott = (from a in AdatokÉjszakásBeoCiklus
                                                             where a.Id == ID
                                                             select a).First();
                Adat_Kiegészítő_Beosztásciklus Előtte = (from a in AdatokÉjszakásBeoCiklus
                                                         where a.Id == ID - 1
                                                         select a).First();

                Adat_Kiegészítő_Beosztásciklus ADAT = new Adat_Kiegészítő_Beosztásciklus(Választott.Id,
                                                                                         Előtte.Beosztáskód,
                                                                                         Előtte.Hétnapja,
                                                                                         Előtte.Beosztásszöveg);

                Adat_Kiegészítő_Beosztásciklus ADAT1 = new Adat_Kiegészítő_Beosztásciklus(Előtte.Id,
                                                                                          Választott.Beosztáskód,
                                                                                          Választott.Hétnapja,
                                                                                          Választott.Beosztásszöveg);
                KézBeosztásciklus.Módosítás("éjszakásciklus", ADAT);
                KézBeosztásciklus.Módosítás("éjszakásciklus", ADAT1);
                Tábla_Éjszaka_kiirás();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Éjszaka_ÚJ_Click(object sender, EventArgs e)
        {
            ÉhétNapja.Text = "";
            ÉBeoKód.Text = "";
            ÉBeosztásSzöveg.Text = "";
            Tábla_Éjszaka.ClearSelection();
        }

        private void Éjszaka_Töröl_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla_Éjszaka.SelectedRows.Count > 1) throw new HibásBevittAdat("Csak egy sort lehet kijelölni.");
                if (Tábla_Éjszaka.SelectedRows[0].Index < 0) throw new HibásBevittAdat("Nincs kiválasztva sor.");

                AdatokÉjszakásBeoCiklus = KézBeosztásciklus.Lista_Adatok("éjszakásciklus");
                int Éj_Sor = Tábla_Éjszaka.SelectedRows[0].Index;
                int ID = Tábla_Éjszaka.Rows[Éj_Sor].Cells[0].Value.ToÉrt_Int();

                Adat_Kiegészítő_Beosztásciklus Elem = (from a in AdatokÉjszakásBeoCiklus
                                                       where a.Id == ID
                                                       select a).FirstOrDefault() ?? throw new HibásBevittAdat("Nincs ilyen ciklus."); ;

                if (Elem != null)
                {
                    KézBeosztásciklus.Törlés("éjszakásciklus", Elem);
                    MessageBox.Show("Az adatok törlése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Tábla_Éjszaka_kiirás();
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

        private void Éjszaka_Ok_Click(object sender, EventArgs e)
        {
            try
            {
                if (ÉhétNapja.Text.Trim() == "") throw new HibásBevittAdat("A hét napját meg kell adni.");
                AdatokÉjszakásBeoCiklus = KézBeosztásciklus.Lista_Adatok("éjszakásciklus");
                if (Tábla_Éjszaka.SelectedRows.Count < 1)
                {
                    Adat_Kiegészítő_Beosztásciklus ADAT = new Adat_Kiegészítő_Beosztásciklus(0,
                                                                                             ÉBeoKód.Text.Trim(),
                                                                                             ÉhétNapja.Text.Trim(),
                                                                                             ÉBeosztásSzöveg.Text.Trim());

                    KézBeosztásciklus.Rögzítés("éjszakásciklus", ADAT);
                }
                else
                {
                    int Éj_Sor = Tábla_Éjszaka.SelectedRows[0].Index;
                    int ID = Tábla_Éjszaka.Rows[Éj_Sor].Cells[0].Value.ToÉrt_Int();

                    Adat_Kiegészítő_Beosztásciklus ADAT = new Adat_Kiegészítő_Beosztásciklus(ID,
                                                                                             ÉBeoKód.Text.Trim(),
                                                                                             ÉhétNapja.Text.Trim(),
                                                                                             ÉBeosztásSzöveg.Text.Trim());
                    KézBeosztásciklus.Módosítás("éjszakásciklus", ADAT);
                }
                Tábla_Éjszaka_kiirás();
                MessageBox.Show("Az adatok rögzítése megtörtént.", "Információ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HibásBevittAdat ex)
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