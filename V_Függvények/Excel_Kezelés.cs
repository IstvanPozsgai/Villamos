using ExcelDataReader;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;
using Villamos;
using Zuby.ADGV;


public partial class Függvénygyűjtemény
{

    /// <summary>
    /// Egy .xlsx fájl adatait olvassa be egy adattáblába.
    /// A megjegyzéssé alakított programkód segítségével több munkalapot is képes beolvasni.
    /// </summary>
    /// <param name="hely"></param>
    /// <returns></returns>
    public static DataTable Excel_Tábla_Beolvas(string hely)
    {
        DataTable EgyTábla = null;
        try
        {
            DataTableCollection Táblák;
            using (FileStream fájl = File.Open(hely, FileMode.Open, FileAccess.Read))
            {
                using (IExcelDataReader olvas = ExcelReaderFactory.CreateReader(fájl))
                {
                    DataSet result = olvas.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
                    });
                    Táblák = result.Tables;
                    //munkalap fülek beolvasása
                    //cboSheets.Items.Clear();
                    //foreach (DataTable  tábla in Táblák)
                    //{
                    //    cboSheets.Items.Add(tábla.TableName);
                    //}
                }
            }
            EgyTábla = Táblák[0];
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, $"Excel tábla beolvasás, fájl:{hely}", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return EgyTábla;
    }


    /// <summary>
    /// Ez a változat kísérlet volt, de lassabb mint a Excel_tábla_beolvas
    /// A kísérlet 64 bites!
    /// </summary>
    /// <param name="hely"></param>
    /// <returns></returns>
    public static DataTable Excel_Tábla_Beolvas_1(string hely)
    {
        DataTable Tábla = new DataTable();
        try
        {

            string KapcsolatiSzöveg = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source='" + hely + "'; Extended Properties='Excel 12.0 Xml;HDR=YES'";

            //KapcsolatiSzöveg = string.Format(KapcsolatiSzöveg, hely, "yes");
            using (OleDbConnection Kapcsolat = new OleDbConnection(KapcsolatiSzöveg))
            {
                Kapcsolat.Open();
                using (OleDbDataAdapter oda = new OleDbDataAdapter($"SELECT * from [Munka1$]", Kapcsolat))
                {
                    DataSet Ds = new DataSet();
                    oda.Fill(Tábla);
                }
            }
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "Excel_Tábla_BEolvas_1", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        return Tábla;
    }


    /// <summary>
    /// Ezt nem tudom minek készítettem
    /// </summary>
    /// <param name="Tábla"></param>
    /// <returns></returns>
    public static DataTable DataGridWiewNak(AdvancedDataGridView Tábla)
    {
        DataTable AdatTábla = new DataTable();

        try
        {
            AdatTábla.Clear();
            for (int oszlop = 0; oszlop < Tábla.Columns.Count; oszlop++)
            {
                AdatTábla.Columns.Add(Tábla.Columns[oszlop].HeaderText);
            }
            for (int sor = 0; sor < Tábla.Rows.Count; sor++)
            {
                DataRow Soradat = AdatTábla.NewRow();
                for (int oszlop = 0; oszlop < Tábla.Columns.Count; oszlop++)
                {

                    Soradat[Tábla.Columns[oszlop].HeaderText] = Tábla.Rows[sor].Cells[oszlop].Value.ToStrTrim();
                }
                AdatTábla.Rows.Add(Soradat);
            }
        }
        catch (HibásBevittAdat ex)
        {
            MessageBox.Show(ex.Message, "Információ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            HibaNapló.Log(ex.Message, "DataGridWiewNak", ex.StackTrace, ex.Source, ex.HResult);
            MessageBox.Show(ex.Message + "\n\n a hiba naplózásra került.", "A program hibára futott", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        return AdatTábla;
    }

}

