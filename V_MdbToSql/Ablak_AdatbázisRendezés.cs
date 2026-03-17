using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MyA = Adatbázis;

namespace Villamos
{
    public partial class Ablak_AdatbázisRendezés : Form
    {
        public Ablak_AdatbázisRendezés()
        {
            InitializeComponent();
        }

        private void btnHozzaad_Click(object sender, EventArgs e)
        {

        }

        private void btnTorol_Click(object sender, EventArgs e)
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





        #region Fájlok
        private void BtnHozzaad_Click(object sender, EventArgs e)
        {

        }

        private void DvgFájlok_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 1) return;
            DvgFájlok.Rows[e.RowIndex].Selected = true;
            FájlAdatTáblái();
        }

        private void BtnTáblák_Click(object sender, EventArgs e)
        {
            FájlAdatTáblái();
        }

        private void FájlAdatTáblái()
        {
            try
            {
                ChkTáblák.Items.Clear();
                if (DvgFájlok.SelectedRows.Count < 1) return;

                string fájl = DvgFájlok.SelectedRows[0].Cells[0].Value?.ToString() ?? "";
                string jelszó = DvgFájlok.SelectedRows[0].Cells[1].Value?.ToString() ?? "";
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



        #endregion

        #region Táblák

        #endregion

        private void Ablak_AdatbázisRendezés_Load(object sender, EventArgs e)
        {

        }
    }
}