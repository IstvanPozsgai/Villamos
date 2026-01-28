using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Adatszerkezet;
using Villamos.Kezelők;
using Villamos.V_Adatszerkezet;

namespace Villamos.V_Ablakok._1_Beállítások
{
    public partial class Ablak_SQLite : Form
    {
        Kezelő_SQLite Kezelő_SQL = new Kezelő_SQLite();
        int _selectedId;
        bool _formLoaded;
        string username = null;   

        public Ablak_SQLite(string Username)
        {
            InitializeComponent();
            username = Username;
        }
        private void Ablak_SQLite_Load(object sender, EventArgs e)
        {
            tb_username.Text = username;
            tb_datetime.Text = DateTime.Now.ToString();
            tb_tf.Text = "1";

            FillDGV();
            _formLoaded = true;
        }
        private void btn_AddData_Click(object sender, EventArgs e)
        {
            Kezelő_SQL.InsertData(username, DateTimeOffset.Now.ToUnixTimeSeconds().ToÉrt_Int(), tb_tf.Text.ToÉrt_Int());
            FillDGV();
        }
        private void btn_create_Click(object sender, EventArgs e)
        {
            Kezelő_SQL.CreateTable();
        }
        private void btn_Update_Click(object sender, EventArgs e)
        {
            Kezelő_SQL.UpdateData(Kezelő_SQL.ReadAllData().FirstOrDefault(x => x.ID == _selectedId), _selectedId);
            FillDGV();
        }
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            Kezelő_SQL.DeleteData(_selectedId);
            FillDGV();
        }
        private void dgv_ShowData_SelectionChanged(object sender, EventArgs e)
        {
            if (!_formLoaded)
                return;

            if (dgv_ShowData.CurrentRow == null)
                return;

            if (dgv_ShowData.CurrentRow.DataBoundItem is Adat_SQLite adat)
            {
                _selectedId = adat.ID;               
            }
        }
        private void FillDGV()
        {
            dgv_ShowData.AutoGenerateColumns = true;
            dgv_ShowData.DataSource = Kezelő_SQL.ReadAllData();
        }
    }
}
