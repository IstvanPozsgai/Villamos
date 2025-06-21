using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = GetTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Excel.ExcelUtlity obj = new Excel.ExcelUtlity();
            DateTime Kezd = DateTime.Now;
            obj.WriteDataTableToExcel(GetTable(), "Person Details", "c:\\testPersonExceldata.xlsx", "Details");

            MessageBox.Show($"Excel created c:\testPersonExceldata.xlsx\n idő:{DateTime.Now - Kezd}");
        }
        static DataTable GetTable()
        {
            Kezelő_T5C5_Kmadatok KézKmAdatok = new Kezelő_T5C5_Kmadatok("T5C5");
            DataTable _AdatTábla = new DataTable();

            List<Adat_T5C5_Kmadatok> Adatok = KézKmAdatok.Lista_Adatok();
            _AdatTábla = MyF.ToDataTable(Adatok);

            return _AdatTábla;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
