using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Villamos.Kezelők;

namespace Villamos.V_Ablakok._1_Beállítások
{
    public partial class Ablak_SQLite : Form
    {
        Kezelő_SQLite Kezelő_SQL = new Kezelő_SQLite();
        public Ablak_SQLite()
        {
            InitializeComponent();
        }

        private void btn_AddData_Click(object sender, EventArgs e)
        {
            Kezelő_SQL.CreateTable();
        }
    }
}
