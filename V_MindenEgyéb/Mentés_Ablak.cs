using System.Windows.Forms;


public partial class Függvénygyűjtemény
{

    /// <summary>
    /// Egy általános Mentési eljárás, a mentési fájlnevet adja vissza.
    /// </summary>
    /// <param name="Könyvtár">Mentési helye alapértelmezés: "MyDocuments"</param>
    /// <param name="Title">Ablak fejléc szövege</param>
    /// <param name="Filter">Szűrő feltétel alapértelmezés: "Excel |*.xlsx"</param>
    /// <param name="Kínált"></param>
    /// <returns></returns>
    public static string Mentés_Fájlnév(string Title, string Kínált, string Filter = "Excel |*.xlsx", string Könyvtár = "MyDocuments")
    {
        string válasz = "";
        using (SaveFileDialog SaveFileDialog1 = new SaveFileDialog())
        {
            // kimeneti fájl helye és neve
            SaveFileDialog1.InitialDirectory = Könyvtár;
            SaveFileDialog1.Title = Title;
            SaveFileDialog1.FileName = Kínált;
            SaveFileDialog1.Filter = Filter;

            // bekérjük a fájl nevét és helyét ha mégse, akkor kilép
            if (SaveFileDialog1.ShowDialog() != DialogResult.Cancel)
                válasz = SaveFileDialog1.FileName;
        }
        return válasz;
    }
}

