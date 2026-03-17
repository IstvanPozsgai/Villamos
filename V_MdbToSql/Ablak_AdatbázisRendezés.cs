using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MyA = Adatbázis;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Ablak_AdatbázisRendezés : Form
    {
        string fájl = "";
        string jelszó = "";
        string tábla = "";


        public Ablak_AdatbázisRendezés()
        {
            InitializeComponent();
            Start();
        }

        #region Alap
        private void Ablak_AdatbázisRendezés_Load(object sender, EventArgs e)
        {
            // kapcsoljuk a gombokat      Program.Postás_Felhasználó.GlobalAdmin;
        }

        private void Start()
        {

        }

        private void Btn_Súgó_Click(object sender, EventArgs e)
        {
            try
            {
                string hely = $@"{Application.StartupPath}\Súgó\VillamosLapok\alapegyéb.html";
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
        #endregion


        #region Fájlok
        private void BtnHozzaad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "MDB fájl (*.mdb)|*.mdb";
                ofd.Multiselect = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    foreach (var file in ofd.FileNames)
                    {
                        DvgFájlok.Rows.Add(file, MyF.GetPassword(file));
                    }
                }
            }
        }

        private void DvgFájlok_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DvgFájlok.Rows[e.RowIndex].Selected = true;
            FájlAdatTáblái();
        }
        #endregion


        #region Táblák
        private void FájlAdatTáblái()
        {
            try
            {
                ChkTáblák.Items.Clear();
                ChkMezők.Items.Clear();
                if (DvgFájlok.SelectedRows.Count < 1) return;

                fájl = DvgFájlok.SelectedRows[0].Cells[0].Value?.ToString() ?? "";
                jelszó = DvgFájlok.SelectedRows[0].Cells[1].Value?.ToString() ?? "";
                if (fájl == string.Empty || jelszó == string.Empty) return;
                ChkTáblák.Items.AddRange(MyA.Mdb_ABTáblák(fájl, jelszó).ToArray());
            }
            catch (HibásBevittAdat ex)
            {
                MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                HibaNapló.Log(ex.Message, this.ToString(), ex.StackTrace, ex.Source, ex.HResult);
                MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChkTáblák_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChkTáblák.SelectedItem == null) return;
            MezőkFeltöltése();
        }
        #endregion



        #region Mezők
        private void MezőkFeltöltése()
        {
            try
            {
                ChkMezők.Items.Clear();
                if (ChkTáblák.CheckedItems.Count < 1) return;
                tábla = ChkTáblák.CheckedItems[0].ToString();
                if (fájl == string.Empty || jelszó == string.Empty || tábla == string.Empty) return;
                ChkMezők.Items.AddRange(MyA.Mdb_ABMezők(fájl, jelszó, tábla).ToArray());
            }
            catch (HibásBevittAdat ex)
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






        #region Nem használt
        private void BtnTáblák_Click(object sender, EventArgs e)
        {

        }

        private void BtnTorol_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DvgFájlok.SelectedRows)
            {
                DvgFájlok.Rows.Remove(row);
            }
        }

        private void btnTallozCel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "SQLite DB (*.db)|*.db";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    txtCelFajl.Text = sfd.FileName;
                }
            }
        }

        private void btnIndit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCelFajl.Text) || string.IsNullOrWhiteSpace(txtCelJelszo.Text))
            {
                MessageBox.Show("Add meg a cél adatbázist és jelszót!");
                return;
            }

            var lista = new List<MdbToSqliteMigrator.MdbForras>();
            foreach (DataGridViewRow row in DvgFájlok.Rows)
            {
                if (row.Cells[0].Value == null) continue;
                string fajl = row.Cells[0].Value.ToString();
                string jelszo = row.Cells[1].Value?.ToString() ?? "";
                if (string.IsNullOrWhiteSpace(jelszo))
                {
                    MessageBox.Show("Minden MDB-hez kell jelszó!");
                    return;
                }
                lista.Add(new MdbToSqliteMigrator.MdbForras { Fajl = fajl, Jelszo = jelszo });
            }

            if (lista.Count == 0)
            {
                MessageBox.Show("Nincs kiválasztott MDB!");
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                MdbToSqliteMigrator.Migracio(lista, txtCelFajl.Text, txtCelJelszo.Text);
                Cursor = Cursors.Default;
                MessageBox.Show("Migráció kész!");
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("Hiba: " + ex.Message);
            }
        }
        #endregion

    }
}