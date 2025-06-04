using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.Villamos_Adatbázis_Funkció;
using Villamos.Villamos_Adatszerkezet;
using MyE = Villamos.Module_Excel;
using MyEn = Villamos.V_MindenEgyéb.Enumok;
using MyF = Függvénygyűjtemény;

namespace Villamos.Villamos_Ablakok._4_Nyilvántartások.TTP
{
    public partial class Ablak_TTP : Form
    {
        string Azonosító = "";

        readonly Kezelő_Kiegészítő_Sérülés KézTelep = new Kezelő_Kiegészítő_Sérülés();
        readonly Kezelő_jármű_hiba KézHiba = new Kezelő_jármű_hiba();
        readonly Kezelő_Jármű KézJármű = new Kezelő_Jármű();
        readonly Kezelő_TTP_Naptár KézNaptár = new Kezelő_TTP_Naptár();

        readonly List<Adat_Jármű_hiba> AdatokHiba = new List<Adat_Jármű_hiba>();
        List<Adat_Kiegészítő_Sérülés> AdatokTelep = new List<Adat_Kiegészítő_Sérülés>();
        List<Adat_Jármű> AdatokJármű = new List<Adat_Jármű>();
        List<Adat_TTP_Tábla> AdatokTábla = new List<Adat_TTP_Tábla>();
        List<Adat_TTP_Naptár> NaptárLista = new List<Adat_TTP_Naptár>();

        string szűrő = "";
        string sorba = "";

        public Ablak_TTP()
        {
            InitializeComponent();


        }


        #region alap
        private void Ablak_TTP_Load(object sender, EventArgs e)
        {
            string hely = $@"{Application.StartupPath}/Főmérnökség/adatok/TTP";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            hely += "/TTP_Adatbázis.mdb";
            if (!File.Exists(hely)) Adatbázis_Létrehozás.TTP_Adatbázis(hely);

            hely = $@"{Application.StartupPath}/Főmérnökség/adatok/TTP/PDF";
            if (!Directory.Exists(hely)) Directory.CreateDirectory(hely);

            Telephelyekfeltöltése();
            TelephelyLista();
            HibákFeltöltése();
            TáblaLista();
            PályaszámListaFeltölt();
            Jogosultságkiosztás();
            Gombok_Ki();


        }

        private void Ablak_TTP_FormClosed(object sender, FormClosedEventArgs e)
        {
            Új_ablak_Év?.Close();
            Új_ablak_Naptár?.Close();
            Új_Ablak_TTP_Történet?.Close();
        }

        private void Súgó_gomb_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\TTP_súgó.html";
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

        private void Telephelyekfeltöltése()
        {
            try
            {
                Cmbtelephely.Items.Clear();
                foreach (string Elem in Listák.TelephelyLista_Jármű())
                    Cmbtelephely.Items.Add(Elem);
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

        private void Jogosultságkiosztás()
        {
            try
            {
                int melyikelem;

                // ide kell az összes gombot tenni amit szabályozni akarunk false
                Btn_TTP_Év.Enabled = false;
                BtnNaptár.Enabled = false;
                BtnAlapadat.Enabled = false;

                // csak főmérnökségi belépéssel törölhető
                if (Program.PostásTelephely.Trim() == "Főmérnökség")
                {
                    Btn_TTP_Év.Visible = true;
                    BtnNaptár.Visible = true;
                    BtnAlapadat.Visible = true;
                    Btn_Ütemez.Visible = true;
                    BtnKuka.Visible = true;
                }
                else
                {
                    Btn_TTP_Év.Visible = false;
                    BtnNaptár.Visible = false;
                    BtnAlapadat.Visible = false;
                    Btn_Ütemez.Visible = false;
                    BtnKuka.Visible = false;
                }

                melyikelem = 130;
                // módosítás 1 
                if (MyF.Vanjoga(melyikelem, 1))
                    Btn_TTP_Év.Enabled = true;

                // módosítás 2
                if (MyF.Vanjoga(melyikelem, 2))
                    BtnNaptár.Enabled = true;
                // módosítás 3 
                if (MyF.Vanjoga(melyikelem, 3))
                    BtnAlapadat.Enabled = true;
            }
            catch (HibásBevittAdat ex)
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


        #region Gombok
        private void Frissítés_gomb_Click(object sender, EventArgs e)
        {
            Táblalistázás();
            NaptárListázás();
            Gombok_Ki();
        }

        private void Gombok_Ki()
        {
            BtnTTPKész.Visible = false;
            BtnKészJav.Visible = false;
            BtnJavítva.Visible = false;
        }

        private void Gombok_Be(int státus, string Telephely)
        {
            switch (státus)
            {
                case 1:
                    if (Program.PostásTelephely == "Főmérnökség")
                    {
                        BtnTTPKész.Visible = true;
                        BtnKészJav.Visible = true;
                    }
                    break;
                case 5:
                    if (Program.PostásTelephely.Trim() == "Főmérnökség" || Program.PostásTelephely.Trim() == Telephely)
                        BtnJavítva.Visible = true;
                    break;
            }

        }

        private void BtnExcel_Click(object sender, EventArgs e)
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
                    FileName = "TTP_Vezénylés_" + Program.PostásNév.Trim() + "-" + DateTime.Now.ToString("yyyyMMddhhmmss"),
                    Filter = "Excel |*.xlsx"
                };
                // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
                if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                    fájlexc = SaveFileDialog1.FileName;
                else
                    return;

                fájlexc = fájlexc.Substring(0, fájlexc.Length - 5);
                MyE.EXCELtábla(fájlexc, Tábla, true);
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

        private void BtnTTPKész_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count < 1) return;
                DateTime DátumÜtem = new DateTime(1900, 1, 1);
                if (Tábla.SelectedRows.Count > 0) DátumÜtem = Tábla.Rows[Tábla.SelectedRows[0].Index].Cells[2].Value.ToÉrt_DaTeTime();
                if (DátumÜtem == new DateTime(1900, 1, 1)) throw new HibásBevittAdat("Nincs beütemezve a kocsi, így ez a funkció nem működik");

                Új_Ablak_TTP_Történet?.Close();
                Új_Ablak_TTP_Történet = new Ablak_TTP_Történet(Azonosító, AdatokJármű, AdatokTábla, "KészJó", DátumÜtem);
                Új_Ablak_TTP_Történet.FormClosed += Új_Ablak_TTP_Történet_Closed;
                Új_Ablak_TTP_Történet.Változás += Táblalistázás;
                Új_Ablak_TTP_Történet.Show();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnKészJav_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count < 1) return;
                DateTime DátumÜtem = new DateTime(1900, 1, 1);
                if (Tábla.SelectedRows.Count > 0) DátumÜtem = Tábla.Rows[Tábla.SelectedRows[0].Index].Cells[2].Value.ToÉrt_DaTeTime();
                if (DátumÜtem == new DateTime(1900, 1, 1)) throw new HibásBevittAdat("Nincs beütemezve a kocsi, így ez a funkció nem működik");
                Új_Ablak_TTP_Történet?.Close();
                Új_Ablak_TTP_Történet = new Ablak_TTP_Történet(Azonosító, AdatokJármű, AdatokTábla, "KészJav", DátumÜtem);
                Új_Ablak_TTP_Történet.FormClosed += Új_Ablak_TTP_Történet_Closed;
                Új_Ablak_TTP_Történet.Változás += Táblalistázás;
                Új_Ablak_TTP_Történet.Show();
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnJavítva_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count < 1) return;
                DateTime DátumÜtem = new DateTime(1900, 1, 1);
                if (Tábla.SelectedRows.Count > 0) DátumÜtem = Tábla.Rows[Tábla.SelectedRows[0].Index].Cells[2].Value.ToÉrt_DaTeTime();
                if (DátumÜtem == new DateTime(1900, 1, 1)) throw new HibásBevittAdat("Nincs beütemezve a kocsi, így ez a funkció nem működik");
                Új_Ablak_TTP_Történet?.Close();
                Új_Ablak_TTP_Történet = new Ablak_TTP_Történet(Azonosító, AdatokJármű, AdatokTábla, "JavKész", DátumÜtem);
                Új_Ablak_TTP_Történet.FormClosed += Új_Ablak_TTP_Történet_Closed;
                Új_Ablak_TTP_Történet.Változás += Táblalistázás;
                Új_Ablak_TTP_Történet.Show();
            }
            catch (HibásBevittAdat ex)
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


        #region Alsó Tábla
        private void HibákFeltöltése()
        {
            try
            {
                AdatokHiba.Clear();
                foreach (Adat_Kiegészítő_Sérülés rekord in AdatokTelep)
                {
                    List<Adat_Jármű_hiba> AdatokIdeig = KézHiba.Lista_Adatok(rekord.Név);
                    AdatokHiba.AddRange(AdatokIdeig);
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

        private void Táblalistázás()
        {
            try
            {
                Tábla.Refresh();
                Tábla.Visible = false;

                KötésiOsztály.DataSource = MyF.TTP_VezénylésFeltölt(AdatokTelep, AdatokHiba, Dátum.Value, ChkKötelezett.Checked);
                Tábla.DataSource = KötésiOsztály;

                OszlopSzélesség();
                if (!ChkSzűrés.Checked) szűrő = "";
                if (!ChkRendezés.Checked) sorba = "";

                Tábla.LoadFilterAndSort(szűrő, sorba);
                Tábla.TriggerSortStringChanged();
                Tábla.TriggerFilterStringChanged();

                Tábla_színezés();
                for (int i = 0; i < Tábla.Columns.Count; i++)
                {
                    Tábla.SetFilterEnabled(Tábla.Columns[i], true);
                    Tábla.SetSortEnabled(Tábla.Columns[i], true);
                    Tábla.SetFilterCustomEnabled(Tábla.Columns[i], true);
                }

                Tábla.Visible = true;
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

        private void Tábla_színezés()
        {
            if (Tábla.Rows.Count < 1) return;
            try
            {
                foreach (DataGridViewRow Sor in Tábla.Rows)
                {
                    DateTime lejár = Sor.Cells["Lejárat dátum"].Value.ToÉrt_DaTeTime();
                    if (lejár.Year == DateTime.Today.Year)
                        Sor.Cells["Lejárat dátum"].Style.BackColor = Color.Yellow;
                    if (lejár < DateTime.Today)
                        Sor.Cells["Lejárat dátum"].Style.BackColor = Color.OrangeRed;

                    switch (Sor.Cells["Jármű státusz"].Value.ToStrTrim())
                    {
                        case "Üzemképtelen":
                            Sor.Cells["Jármű státusz"].Style.BackColor = Color.OrangeRed;
                            break;
                        case "Beálló":
                            Sor.Cells["Jármű státusz"].Style.BackColor = Color.Yellow;
                            break;
                    }

                    switch (Sor.Cells["Státus"].Value.ToStrTrim())
                    {
                        case "Javítandó":
                            Sor.Cells["Státus"].Style.BackColor = Color.OrangeRed;
                            break;
                        case "Ütemezett":
                            Sor.Cells["Státus"].Style.BackColor = Color.CornflowerBlue;
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

        private void OszlopSzélesség()
        {
            Tábla.Columns["Pályaszám"].Width = 100;
            Tábla.Columns["Lejárat dátum"].Width = 110;
            Tábla.Columns["Ütemezés dátum"].Width = 110;
            Tábla.Columns["Jármű hiba"].Width = 300;
            Tábla.Columns["Jármű státusz"].Width = 150;
            Tábla.Columns["Típus"].Width = 110;
            Tábla.Columns["Telephely"].Width = 130;
            Tábla.Columns["TTP Kötelezés"].Width = 90;
            Tábla.Columns["Megjegyzés"].Width = 300;
            Tábla.Columns["Utolsó TTP dátum"].Width = 110;
            Tábla.Columns["Státus"].Width = 130;
        }

        private void Tábla_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex < 0) return;
            Gombok_Ki();

            Azonosító = Tábla.Rows[e.RowIndex].Cells[0].Value.ToStrTrim();
            int szám = Enum.Parse(typeof(MyEn.TTP_Státus), Tábla.Rows[e.RowIndex].Cells["Státus"].Value.ToStrTrim()).GetHashCode();

            Gombok_Be(szám, Tábla.Rows[e.RowIndex].Cells["Telephely"].Value.ToStrTrim());
        }

        private void Tábla_FilterStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.FilterEventArgs e)
        {
            szűrő = Tábla.FilterString;
        }

        private void Tábla_SortStringChanged(object sender, Zuby.ADGV.AdvancedDataGridView.SortEventArgs e)
        {
            sorba = Tábla.SortString;
        }

        private void KötésiOsztály_ListChanged(object sender, ListChangedEventArgs e)
        {
            Tábla_színezés();
        }
        #endregion


        #region Felső tábla
        private void NaptárListázás()
        {
            DtGvw_Naptár.Rows.Clear();
            DtGvw_Naptár.Columns.Clear();
            DtGvw_Naptár.Refresh();
            DtGvw_Naptár.Visible = false;
            DtGvw_Naptár.ColumnCount = 7;
            DtGvw_Naptár.RowCount = 1;

            DateTime HétElsőNapja = MyF.Hét_elsőnapja(Dátum.Value);
            NaptárLista = KézNaptár.Lista_Adatok();
            NaptárLista = (from a in NaptárLista
                           where a.Dátum >= MyF.Év_elsőnapja(Dátum.Value) && a.Dátum <= MyF.Év_utolsónapja(Dátum.Value)
                           orderby a.Dátum
                           select a).ToList();


            for (int i = 0; i < DtGvw_Naptár.ColumnCount; i++)
            {
                DtGvw_Naptár.Columns[i].HeaderText = $"{HétElsőNapja.AddDays(i):MM-dd}\n{HétElsőNapja.AddDays(i):dddd}";
                DtGvw_Naptár.Columns[i].Width = 150;

                Adat_TTP_Naptár AktualisNap = (from a in NaptárLista
                                               where a.Dátum.ToShortDateString() == HétElsőNapja.AddDays(i).ToShortDateString()
                                               select a).FirstOrDefault();
                Adat_TTP_Tábla Elem = (from a in AdatokTábla
                                       where a.Ütemezés_Dátum.ToShortDateString() == HétElsőNapja.AddDays(i).ToShortDateString()
                                       select a).FirstOrDefault();
                if (AktualisNap != null)
                {
                    if (!AktualisNap.Munkanap)
                        DtGvw_Naptár.Rows[0].Cells[i].Style.BackColor = Color.Red;
                    else
                        DtGvw_Naptár.Rows[0].Cells[i].Style.BackColor = Color.Green;
                }
                if (Elem != null)
                    DtGvw_Naptár.Rows[0].Cells[i].Value = Elem.Együtt;
            }

            DtGvw_Naptár.Visible = true;
            DtGvw_Naptár.ClearSelection();
        }
        #endregion


        #region Történet
        Ablak_TTP_Történet Új_Ablak_TTP_Történet;
        private void BtnTörténet_Click(object sender, EventArgs e)
        {
            DateTime DátumÜtem = new DateTime(1900, 1, 1);
            if (Tábla.SelectedRows.Count > 0) DátumÜtem = Tábla.Rows[Tábla.SelectedRows[0].Index].Cells[2].Value.ToÉrt_DaTeTime();

            Új_Ablak_TTP_Történet?.Close();
            Új_Ablak_TTP_Történet = new Ablak_TTP_Történet(Azonosító, AdatokJármű, AdatokTábla, "Összes", DátumÜtem);
            Új_Ablak_TTP_Történet.FormClosed += Új_Ablak_TTP_Történet_Closed;
            Új_Ablak_TTP_Történet.Változás += Táblalistázás;
            Új_Ablak_TTP_Történet.Show();
        }

        private void Új_Ablak_TTP_Történet_Closed(object sender, FormClosedEventArgs e)
        {
            Új_Ablak_TTP_Történet = null;
        }
        #endregion


        #region Alap adatok gyártás stb
        Ablak_TTP_Alapadat Új_ablak_Alapadat;
        private void BtnAlapadat_Click(object sender, EventArgs e)
        {
            if (Új_ablak_Alapadat == null)
            {
                Új_ablak_Alapadat = new Ablak_TTP_Alapadat(AdatokJármű);
                Új_ablak_Alapadat.FormClosed += Új_ablak_Alapadat_Closed;
                Új_ablak_Alapadat.TTP_Változás += Táblalistázás;
                Új_ablak_Alapadat.Show();
            }
            else
            {
                Új_ablak_Alapadat.Activate();
                Új_ablak_Alapadat.WindowState = FormWindowState.Maximized;
            }
        }

        private void Új_ablak_Alapadat_Closed(object sender, FormClosedEventArgs e)
        {
            Új_ablak_Alapadat = null;
        }



        #endregion 


        #region Ütemezés és törlés
        private void Btn_Ütemez_Click(object sender, EventArgs e)
        {
            try
            {
                if (Tábla.SelectedRows.Count < 1 || DtGvw_Naptár.SelectedCells.Count < 1) return;   //Ha nincs kiválasztva mindkét táblázatban elem akkor kilép
                string szöveg = "";
                int oszlop = DtGvw_Naptár.SelectedCells[0].ColumnIndex;
                DateTime ÜtemezésDátuma = DtGvw_Naptár.Columns[oszlop].HeaderText.ToÉrt_DaTeTime();
                if (DtGvw_Naptár.Rows[0].Cells[oszlop].Style.BackColor == Color.Red) throw new HibásBevittAdat("Erre a napra nem lehet ütemezni, mert nem munkanap.");
                if (DtGvw_Naptár.Rows[0].Cells[oszlop].Value.ToStrTrim() != "") throw new HibásBevittAdat("Erre a napra nem lehet ütemezni, mert már van ütemezve.");

                //Alsó és Felső táblázatba írjuk be
                foreach (DataGridViewRow sorok in Tábla.SelectedRows)
                {
                    int sor = sorok.Index;
                    szöveg += $"{Tábla.Rows[sor].Cells[0].Value} - ";
                    Tábla.Rows[sor].Cells[2].Value = ÜtemezésDátuma;
                }
                DtGvw_Naptár.Rows[0].Cells[oszlop].Value = szöveg.Substring(0, szöveg.Length - 3);

                //TTP_táblába rögzítjük
                foreach (DataGridViewRow sorok in Tábla.SelectedRows)
                {
                    int sor = sorok.Index;
                    Adat_Tábla_Vezénylés Adat = new Adat_Tábla_Vezénylés(
                                     Tábla.Rows[sor].Cells[0].Value.ToStrTrim(),          //azonosító
                                     Tábla.Rows[sor].Cells[1].Value.ToÉrt_DaTeTime(),     //lejárat dátuma
                                     Tábla.Rows[sor].Cells[2].Value.ToÉrt_DaTeTime(),     //ütemezés dátum
                                     Tábla.Rows[sor].Cells[3].Value.ToStrTrim(),          // Kocsi hibái
                                     Tábla.Rows[sor].Cells[4].Value.ToÉrt_Long(),         //kocsi státus
                                     Tábla.Rows[sor].Cells[5].Value.ToStrTrim(),          //kocsi típusa
                                     Tábla.Rows[sor].Cells[6].Value.ToStrTrim(),          //telephely
                                     Tábla.Rows[sor].Cells[7].Value.ToStrTrim(),          // TTP kötelezett
                                     Tábla.Rows[sor].Cells[8].Value.ToStrTrim(),          //Megjegyzés
                                     Tábla.Rows[sor].Cells[9].Value.ToÉrt_DaTeTime(),     //Utolsó ttp dátuma
                                     Tábla.Rows[sor].Cells[10].Value.ToÉrt_Int()
                                     );
                    MyF.TörténetbeRögzítés(ÜtemezésDátuma, Tábla.Rows[sor].Cells[0].Value.ToStrTrim(), AdatokTábla, Adat, szöveg.Substring(0, szöveg.Length - 3));
                }

                TáblaLista();
                Táblalistázás();
                MessageBox.Show("Az ütemezés megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnKuka_Click(object sender, EventArgs e)
        {
            try
            {
                if (DtGvw_Naptár.SelectedCells.Count <= 0) return;
                int oszlop = DtGvw_Naptár.SelectedCells[0].ColumnIndex;
                string szerelvény = DtGvw_Naptár.Rows[0].Cells[oszlop].Value.ToStrTrim();
                if (szerelvény == "") throw new HibásBevittAdat("Erre a napra nincs ütemezve jármű, ezért nem lehet törölni az ütemezést.");
                DateTime ÜtemezésDátuma = DtGvw_Naptár.Columns[oszlop].HeaderText.ToÉrt_DaTeTime();
                string[] darabol = szerelvény.Split('-');
                for (int i = 0; i < darabol.Length; i++)
                {
                    Adat_TTP_Tábla Elem = (from a in AdatokTábla
                                           where a.Azonosító == darabol[i].Trim() && a.Ütemezés_Dátum == ÜtemezésDátuma
                                           select a).FirstOrDefault();
                    if (Elem != null)
                    {
                        //vizsgálni kell hogy az ütemezésen túl van-e, ha igen nem lehet törölni.
                        //Csak azokat lehet törölni amik ütemezve vannak csak.
                        if (Elem.Státus == 1)
                            MyF.TTP_AdatTábla_Törlés(Elem);
                    }
                }
                TáblaLista();
                Táblalistázás();
                NaptárListázás();
                MessageBox.Show("Az ütemezés törlése megtörtént.", "Tájékoztatás", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (HibásBevittAdat ex)
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


        #region Korhoz Éveket Állít
        Ablak_TTP_Év Új_ablak_Év;
        private void Btn_TTP_Év_Click(object sender, EventArgs e)
        {
            if (Új_ablak_Év == null)
            {
                Új_ablak_Év = new Ablak_TTP_Év();
                Új_ablak_Év.FormClosed += Új_ablak_Év_Closed;
                Új_ablak_Év.Show();
            }
            else
            {
                Új_ablak_Év.Activate();
                Új_ablak_Év.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_ablak_Év_Closed(object sender, FormClosedEventArgs e)
        {
            Új_ablak_Év = null;
        }
        #endregion


        #region Naptár Beállítások
        Ablak_TTP_Naptár Új_ablak_Naptár;
        private void BtnNaptár_Click(object sender, EventArgs e)
        {
            if (Új_ablak_Naptár == null)
            {
                Új_ablak_Naptár = new Ablak_TTP_Naptár();
                Új_ablak_Naptár.FormClosed += Új_ablak_Naptár_Closed;
                Új_ablak_Naptár.Show();
            }
            else
            {
                Új_ablak_Naptár.Activate();
                Új_ablak_Naptár.WindowState = FormWindowState.Maximized;
            }
        }
        private void Új_ablak_Naptár_Closed(object sender, FormClosedEventArgs e)
        {
            Új_ablak_Naptár = null;
        }
        #endregion


        #region Listák feltöltése
        private void PályaszámListaFeltölt()
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Főmérnökség\Adatok\Villamos.mdb";
                string jelszó = "pozsgaii";
                string szöveg = "SELECT * FROM állománytábla ORDER BY Azonosító ";
                AdatokJármű.Clear();
                AdatokJármű = KézJármű.Lista_Adatok(hely, jelszó, szöveg);
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TelephelyLista()
        {
            try
            {
                AdatokTelep.Clear();
                List<Adat_Kiegészítő_Sérülés> AdatokTelepÖ = KézTelep.Lista_Adatok();
                AdatokTelep = (from a in AdatokTelepÖ
                               where a.Vezér1 == false
                               orderby a.ID
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

        private void TáblaLista()
        {
            try
            {
                AdatokTábla = MyF.TTP_Tábla_Lista_Feltöltés();
            }
            catch (HibásBevittAdat ex)
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
