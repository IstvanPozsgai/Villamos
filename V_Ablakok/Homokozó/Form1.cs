using ArrayToExcel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Villamos.Kezelők;
using Villamos.Villamos_Adatszerkezet;
using MyF = Függvénygyűjtemény;

namespace Villamos
{
    public partial class Form1 : Form
    {
        readonly Kezelő_T5C5_Kmadatok KézKmAdatok = new Kezelő_T5C5_Kmadatok("T5C5");
        DataTable _AdatTábla = new DataTable();
        string fájl = $@"{Application.StartupPath}\Temp".KönyvSzerk();


        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = GetTable();

            fájl += @"\próba.xlsx";
            if (File.Exists(fájl)) File.Delete(fájl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime Kezdet = DateTime.Now;

            Próba1();
            Module_Excel.Megnyitás(fájl);
            MessageBox.Show($"Excel created {fájl}\n idő:{DateTime.Now - Kezdet}");
        }

        private void Próba0()
        {
            //7:19
            Module_Excel.EXCELtábla(_AdatTábla, fájl);
        }

        private void Próba1()
        {
            //https://www.c-sharpcorner.com/uploadfile/deveshomar/exporting-datatable-to-excel-in-c-sharp-using-interop/
            //13:03
            Excel.ExcelUtlity obj = new Excel.ExcelUtlity();
            DateTime Kezd = DateTime.Now;
            obj.WriteDataTableToExcel(GetTable(), "Person Details", fájl, "Details");
        }

        private void Próba2()
        {
            //https://github.com/mustaddon/ArrayToExcel
            //0:03

            byte[] excel = GetTable().ToExcel();

            File.WriteAllBytes(fájl, excel);
        }

        DataTable GetTable()
        {


            List<Adat_T5C5_Kmadatok> Adatok = KézKmAdatok.Lista_Adatok();
            _AdatTábla = MyF.ToDataTable(Adatok);

            return _AdatTábla;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
